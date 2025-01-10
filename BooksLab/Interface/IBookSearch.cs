namespace BooksLab.Interface;

using BooksLab.Books;

internal interface IBookSearch
{
     Task<List<Book>> SearchAsync(int userId, string query);
}
