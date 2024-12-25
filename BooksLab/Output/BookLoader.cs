using BooksLab.Books;
using BooksLab.Interface;
using Newtonsoft.Json;

namespace BooksLab.Output;

internal class BookLoader : IBookLoader
{
    private static string _filePath = "book_catalog.json";

    public async Task<List<Book>> LoadAsync(int userId, bool currentUser)
    {
        if (File.Exists(_filePath))
        {
            string json = await File.ReadAllTextAsync(_filePath);
            var allBooks = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

            // Фильтруем книги в зависимости от того, какой пользователь запрашивает
            return currentUser
                ? allBooks.Where(book => book.UserId == userId).ToList()  // Книги текущего пользователя
                : allBooks.Where(book => book.UserId != userId).ToList(); // Книги других пользователей
        }

        return new List<Book>();
    }
}
