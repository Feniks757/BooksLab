using BooksLab.Books;
using BooksLab.Interface;

namespace BooksLab.ConsoleCommands;

internal class AuthorSearch : IBookSearch
{
    public List<Book> Search(BookCatalog catalog, string query)
    {
        return catalog.Books
                   .AsEnumerable()
                   .Where(book => book.Author.ToLower().Contains(query.ToLower()))
                   .ToList();
    }
}
