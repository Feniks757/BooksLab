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

        return catalog.Books
            .AsEnumerable()
            .Where(book =>
                keywords.Any(keyword => book.Annotation.ToLower().Contains(keyword.ToLower()) ||
                                        book.Genres.Any(genre =>
                                            genre.ToLower().Contains(keyword.ToLower()))))
            .ToList();
    }
}