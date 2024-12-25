namespace BooksLab.Interface;

using BooksLab.Books;

internal interface IBookLoader
{
    Task<List<Book>> LoadAsync(int id, bool currentUser);
}
