using System.Collections.Generic;

namespace BooksTest;

public class ISBNBookSearchTests
{
    private ISBNBookSearch _isbnBookSearch;
    private List<Book> _books;
    private Int32 userId;
    
    void clearDB()
    {
        var isbnsToRemove = _books
            .Select(b => b.ISBN)
            .ToHashSet();
        using (var _bookCatalog = new BookCatalog(userId, true))
        {
            var removeBooks = _bookCatalog.Books.AsEnumerable().Where(b => isbnsToRemove.Contains(b.ISBN)).ToList();
            foreach (var book in removeBooks)
            {
                _bookCatalog.Books.Remove(book);
            }

            _bookCatalog.SaveChanges();
        }
    }
    
    [SetUp]
    public void SetUp()
    {
        _isbnBookSearch = new ISBNBookSearch();
        userId = 1;
        var currentUser = true;
        

        _books = new List<Book>
            {
                new Book("Title1", "Author1", new List<string> { "Genre1" }, 2020, "Annotation1", "ISBN1", userId),
                new Book("Title2", "Author2", new List<string> { "Genre2" }, 2021, "Annotation2", "ISBN2", userId),
                new Book("Title3", "Author3", new List<string> { "Genre3" }, 2022, "Annotation3", "ISBN3", userId)
            };
        using (var _bookCatalog = new BookCatalog(userId, currentUser))
        {
            foreach (var book in _books)
            {
                _bookCatalog.AddBook(book);
            }
        }
        
    }
    
    [TearDown]
    public void TearDown()
    {
        clearDB();
    }

    // Проверяет, что метод Search правильно возвращает книги по ISBN.
    [Test]
    public void Search_ShouldReturnBooksByISBN()
    {
        // Act
        var result = _isbnBookSearch.SearchAsync(userId, "ISBN1").Result;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        //Assert.That(result, Contains.Item(_books[0]));
    }

    // Проверяет, что метод Search возвращает пустой список, если ISBN не найден.
    [Test]
    public void Search_ShouldReturnEmptyListIfISBNNotFound()
    {
        // Act
        var result = _isbnBookSearch.SearchAsync(userId, "ISBN4").Result;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    // Проверяет, что метод Search не чувствителен к регистру.
    [Test]
    public void Search_ShouldBeCaseInsensitive()
    {
        // Act
        var result = _isbnBookSearch.SearchAsync(userId, "isbn1").Result;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        //Assert.That(result, Contains.Item(_books[0]));
    }
}