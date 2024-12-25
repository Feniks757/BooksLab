using BooksLab.Books;
using BooksLab.Interface;

namespace BooksLab.ConsoleCommands;

internal class KeywordSearch : IBookSearch
{
    private IBookSearch _bookSearchImplementation;

    public List<Book> Search(BookCatalog catalog, string query)
    {
        string[] keywords = query.Split(',')
            .Select(keyword => keyword.Trim())
            .ToArray();

        return catalog.Books.Where(book =>
                keywords.Any(keyword => book.Annotation.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                        book.Genres.Any(genre =>
                                            genre.Contains(keyword, StringComparison.OrdinalIgnoreCase))))
            .ToList();
    }
}