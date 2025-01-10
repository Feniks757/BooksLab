using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksLab.ConsoleCommands;

internal class ISBNBookSearch : IBookSearch
{
    public async Task<List<Book>> SearchAsync(int userId, string query)
    {
        await using (BookCatalog catalog = new(userId))
        {
            return await catalog.Books
                .Where(book => book.ISBN.ToLower().Equals(query.ToLower()))
                .ToListAsync();
        }
    }
}
