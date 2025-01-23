using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.EntityFrameworkCore;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<IEnumerable<Book>> SearchBooksAsync(int userId, string searchType, string searchQuery);
    Task<Book> AddBookAsync(Book book, int userId);
}