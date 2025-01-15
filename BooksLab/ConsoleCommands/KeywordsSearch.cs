using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksLab.ConsoleCommands;

public class KeywordSearch : IBookSearch
{
    //private IBookSearch _bookSearchImplementation;

    public async Task<List<Book>> SearchAsync(int userId, string query, Func<Book, string> field)
    {
        string[] keywords = query.Split(',')
            .Select(keyword => keyword.Trim().ToLower())
            .ToArray();
        List<Book> books;
        
        
        await using (BookContext context = new(userId))
        {
            return context.Books
                .AsEnumerable() //??
                .Where(book =>
                    keywords.Any(keyword => book.Annotation.Contains(keyword) ||
                                            book.Genres.Any(genre => genre.ToLower().Contains(keyword))))
                .ToList();
        }
    }
}