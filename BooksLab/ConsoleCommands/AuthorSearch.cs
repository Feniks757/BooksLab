/*
using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksLab.ConsoleCommands;

internal class AuthorSearch : IBookSearch
{
    public async Task<List<Book>> SearchAsync(int userId, string query)
    {
        await using(BookCatalog catalog = new(userId))
        {
            return await catalog.Books
                .Where(book => book.Author.ToLower().Contains(query.ToLower()))
                .ToListAsync();
        }
    }
}
*/
