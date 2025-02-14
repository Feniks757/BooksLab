using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
namespace BooksTest;

public class DisplayTests
{
    private StringWriter _stringWriter;
    private TextWriter _originalOutput;

    [SetUp]
    public void SetUp()
    {
        _stringWriter = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }

    // Проверяет, что метод ShowBooks правильно выводит список книг.
    [Test]
    public void ShowBooks_ShouldDisplayBooks()
    {
        // Arrange
        var books = new List<Book>
            {
                new Book("Title1", "Author1", new List<string> { "Genre1" }, 2020, "Annotation1", "ISBN1", 1),
                new Book("Title2", "Author2", new List<string> { "Genre2" }, 2021, "Annotation2", "ISBN2", 1)
            };

        // Act
        Display.ShowBooks(books);

        // Assert
        var expectedOutput = new StringBuilder();
        expectedOutput.AppendLine("Title1, Author1, Genre1, 2020, ISBN: ISBN1");
        expectedOutput.AppendLine("Title2, Author2, Genre2, 2021, ISBN: ISBN2");
        Assert.That(_stringWriter.ToString(), Is.EqualTo(expectedOutput.ToString()));
    }

    // Проверяет, что метод ShowBooks выводит сообщение "Книги не найдены.", если список книг пуст.
    [Test]
    public void ShowBooks_ShouldDisplayNoBooksFoundMessage()
    {
        // Arrange
        var books = new List<Book>();

        // Act
        Display.ShowBooks(books);

        // Assert
        var expectedOutput = "Книги не найдены.\r\n";
        Assert.That(_stringWriter.ToString(), Is.EqualTo(expectedOutput));
    }
}