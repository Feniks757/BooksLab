using BooksLab.Books;

namespace WebBook.Interfaces;

public interface ISearchBooksService
{
    Task<IEnumerable<Book>> SearchBooksAsync(int userId, string searchQuery);
}