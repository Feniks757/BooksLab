using System.Collections.Generic;

namespace BooksTest;

public class TitleSearchTests
{
    private TitleSearch _titleSearch;
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
        _titleSearch = new TitleSearch();
        userId = 1;
        var currentUser = true;
        
        _books = new List<Book>
            {
                new Book("Title1", "Author1", new List<string> { "Genre1" }, 2020, "Annotation1", "ISBN1", userId),
                new Book("Title2", "Author2", new List<string> { "Genre2" }, 2021, "Annotation2", "ISBN2", userId),
                new Book("Title3", "Author3", new List<string> { "Genre3" }, 2022, "Annotation3", "ISBN3", userId)
            };
        clearDB();

        using (var _bookCatalog = new BookCatalog(userId, true))
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

    // Проверяет, что метод Search правильно возвращает книги по названию.
    [Test]
    public void Search_ShouldReturnBooksByTitle()
    {
        // Act
        var result = _titleSearch.SearchAsync(userId, "Title1").Result;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        //Assert.That(result, Contains.Item(_books[0]));
    }

    // Проверяет, что метод Search возвращает пустой список, если название не найдено.
    [Test]
    public void Search_ShouldReturnEmptyListIfTitleNotFound()
    {
        // Act
        var result = _titleSearch.SearchAsync(userId, "NonexistentTitle").Result;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    // Проверяет, что метод Search не чувствителен к регистру.
    [Test]
    public void Search_ShouldBeCaseInsensitive()
    {
        // Act
        var result = _titleSearch.SearchAsync(userId, "title1").Result;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        //Assert.That(result, Contains.Item(_books[0]));
    }
}