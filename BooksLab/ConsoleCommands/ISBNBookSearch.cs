using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksLab.ConsoleCommands;

internal class ISBNBookSearch : IBookSearch
{
    public async Task<List<Book>> Search(BookCatalog catalog, string query)
    {
        return await catalog.Books
                   .Where(book => book.ISBN.ToLower().Equals(query.ToLower()))
                   .ToListAsync();
    }
}
