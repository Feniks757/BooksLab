using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;
using WebBook.Interfaces;

namespace WebBook.Services;
 
public class SearchBooksByIsbnService : ISearchBooksService
{
    private readonly IDbContextFactory<BookContext> _contextFactory;
    private readonly IBookSearch _bookSearch;

    public SearchBooksByIsbnService(IDbContextFactory<BookContext> contextFactory, IBookSearch bookSearch)
    {
        _contextFactory = contextFactory;
        _bookSearch = bookSearch;
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(int userId, string searchQuery)
    {
        await using var db = _contextFactory.CreateDbContext();
        db.UserId = userId;
        return await _bookSearch.SearchAsync(db, searchQuery, (Book book) => book.ISBN);
    }
}