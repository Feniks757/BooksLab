using BooksLab.Books;
using BooksLab.Interface;

namespace BooksLab.ConsoleCommands;

internal class ISBNBookSearch : IBookSearch
{
    public List<Book> Search(BookCatalog catalog, string query)
    {
        return catalog.Books
                   .Where(book => book.ISBN.Equals(query, StringComparison.OrdinalIgnoreCase))
                   .ToList();
    }
}
