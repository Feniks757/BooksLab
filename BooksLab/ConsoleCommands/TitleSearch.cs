using BooksLab.Books;
using BooksLab.Interface;

namespace BooksLab.ConsoleCommands;

internal class TitleSearch : IBookSearch
{
    public List<Book> Search(BookCatalog catalog, string query)
    {
        return catalog.Books
                    .AsEnumerable()
                    .Where(book => book.Title.ToLower().Contains(query.ToLower()))
                   .ToList();
    }
}
