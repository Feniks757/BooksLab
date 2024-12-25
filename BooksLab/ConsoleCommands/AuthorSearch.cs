using BooksLab.Books;
using BooksLab.Interface;

namespace BooksLab.ConsoleCommands;

internal class AuthorSearch : IBookSearch
{
    public List<Book> Search(BookCatalog catalog, string query)
    {
        return catalog.Books
                   .Where(book => book.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                   .ToList();
    }
}
