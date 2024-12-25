namespace BooksLab.Interface;

using BooksLab.Books;

internal interface IBookSaver
{
    Task SaveAsync(List<Book> books);
}
