using BooksLab.Books;
using Microsoft.EntityFrameworkCore;
using WebBook.Interfaces;

namespace WebBook.Services;

public class GetBooksService : IGetBooksService
{
    private readonly IDbContextFactory<BookContext> _contextFactory;

    public GetBooksService(IDbContextFactory<BookContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        await using var db = _contextFactory.CreateDbContext();
        return await db.Books.ToListAsync();
    }
}