using BooksLab.Books;

namespace WebBook.Interfaces;

public interface IAddBookService
{
    Task<Book> AddBookAsync(Book book, int userId);
}