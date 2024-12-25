using BooksLab.Books;

namespace BooksLab.ConsoleCommands;

internal static class Display
{
    public static void ShowBooks(List<Book> books)
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Книги не найдены.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}
