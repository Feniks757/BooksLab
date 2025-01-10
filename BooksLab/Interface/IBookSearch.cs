namespace BooksLab.Interface;

using BooksLab.Books;

internal interface IBookSearch
{
     Task<List<Book>> Search(BookCatalog catalog, string query);
}
