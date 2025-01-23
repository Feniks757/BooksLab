using BooksLab.Books;

namespace WebBook.Interfaces;

public interface IGetBooksService
{
    Task<IEnumerable<Book>> GetBooksAsync();
}