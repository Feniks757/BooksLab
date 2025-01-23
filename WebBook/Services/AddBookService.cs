using BooksLab.Books;
using Microsoft.EntityFrameworkCore;
using WebBook.Interfaces;

namespace WebBook.Services;

public class AddBookService : IAddBookService
{
    private readonly IDbContextFactory<BookContext> _contextFactory;

    public AddBookService(IDbContextFactory<BookContext> contextFactory)
    {
        _contextFactory = contextFactory;
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