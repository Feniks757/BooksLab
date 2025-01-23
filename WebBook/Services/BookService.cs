using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.EntityFrameworkCore;

namespace WebBook.Services;

public class BookService : IBookService
{
    private readonly IDbContextFactory<BookContext> _contextFactory;

    public BookService(IDbContextFactory<BookContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        await using var db = _contextFactory.CreateDbContext();
        return await db.Books.ToListAsync();
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(int userId, string searchType, string searchQuery)
    {
        var books = new List<Book>();
        await using var db = _contextFactory.CreateDbContext();
        db.UserId = userId;

        switch (searchType)
        {
            case "title":
                books = await new BookSearch().SearchAsync(db, searchQuery, (Book book) => book.Title);
                break;
            case "author":
                books = await new BookSearch().SearchAsync(db, searchQuery, (Book book) => book.Author);
                break;
            case "isbn":
                books = await new BookSearch().SearchAsync(db, searchQuery, (Book book) => book.ISBN);
                break;
            case "keywords":
                books = await new KeywordSearch().SearchAsync(db, searchQuery, (Book book) => "");
                break;
            default:
                throw new ArgumentException("Invalid search type.");
        }

        return books;
    }

    public async Task<Book> AddBookAsync(Book book, int userId)
    {
        if (book == null)
        {
            throw new ArgumentException("Book data is invalid.");
        }

        book.UserId = userId;
        await using var db = _contextFactory.CreateDbContext();
        await db.AddBookAsync(book);
        return book;
    }
}
