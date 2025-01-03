using System.Collections.Generic;

namespace BooksTest;

public class ISBNBookSearchTests
{
    private ISBNBookSearch _isbnBookSearch;
    private BookCatalog _bookCatalog;
    private List<Book> _books;

    [SetUp]
    public void SetUp()
    {
        _isbnBookSearch = new ISBNBookSearch();
        var userId = 1;
        var currentUser = true;
        _bookCatalog = new BookCatalog(userId, currentUser);

        _books = new List<Book>
            {
                new Book("Title1", "Author1", new List<string> { "Genre1" }, 2020, "Annotation1", "ISBN1", userId),
                new Book("Title2", "Author2", new List<string> { "Genre2" }, 2021, "Annotation2", "ISBN2", userId),
                new Book("Title3", "Author3", new List<string> { "Genre3" }, 2022, "Annotation3", "ISBN3", userId)
            };

        foreach (var book in _books)
        {
            _bookCatalog.AddBook(book);
        }
    }

    // Проверяет, что метод Search правильно возвращает книги по ISBN.
    [Test]
    public void Search_ShouldReturnBooksByISBN()
    {
        // Act
        var result = _isbnBookSearch.Search(_bookCatalog, "ISBN1");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result, Contains.Item(_books[0]));
    }

    // Проверяет, что метод Search возвращает пустой список, если ISBN не найден.
    [Test]
    public void Search_ShouldReturnEmptyListIfISBNNotFound()
    {
        // Act
        var result = _isbnBookSearch.Search(_bookCatalog, "ISBN4");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    // Проверяет, что метод Search не чувствителен к регистру.
    [Test]
    public void Search_ShouldBeCaseInsensitive()
    {
        // Act
        var result = _isbnBookSearch.Search(_bookCatalog, "isbn1");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result, Contains.Item(_books[0]));
    }
}