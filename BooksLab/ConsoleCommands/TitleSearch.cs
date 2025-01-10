using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksLab.ConsoleCommands;

internal class TitleSearch : IBookSearch
{
    public async Task<List<Book>> SearchAsync(int userId, string query)
    {
        using (BookCatalog catalog = new(userId))
        {
            return await catalog.Books
                .AsQueryable()
                .Where(book => book.Title.ToLower().Contains(query.ToLower()))
                .ToListAsync();
        }
    }
}
