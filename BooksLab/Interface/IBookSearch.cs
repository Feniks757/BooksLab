namespace BooksLab.Interface;

using BooksLab.Books;

internal interface IBookSearch
{
    List<Book> Search(BookCatalog catalog, string query);
}
