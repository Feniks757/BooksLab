using BooksLab.Books;
using BooksLab.Interface;

namespace BooksLab.ConsoleCommands;

internal class TitleSearch : IBookSearch
{
    public List<Book> Search(BookCatalog catalog, string query)
    {
        return catalog.Books
                   .Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                   .ToList();
    }
}
