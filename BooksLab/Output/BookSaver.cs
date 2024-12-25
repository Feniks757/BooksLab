using BooksLab.Books;
using BooksLab.Interface;
using Newtonsoft.Json;

namespace BooksLab.Output;

internal class BookSaver : IBookSaver
{
    private static string _filePath = "book_catalog.json";

    public async Task SaveAsync(List<Book> books)
    {
        // Очищаем файл перед сохранением
        await ClearJsonFileAsync(_filePath);

        // Сериализация книги в JSON и запись в файл
        string json = JsonConvert.SerializeObject(books, Formatting.Indented);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task ClearJsonFileAsync(string filePath)
    {
        // Создаем пустой список книг
        var emptyList = new List<Book>();

        // Сериализуем пустой список в JSON
        string emptyJson = JsonConvert.SerializeObject(emptyList, Formatting.Indented);

        // Записываем пустой JSON в файл
        await File.WriteAllTextAsync(filePath, emptyJson);
    }
}
