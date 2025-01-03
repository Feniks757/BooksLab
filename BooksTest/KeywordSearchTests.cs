using System.Collections.Generic;

namespace BooksTest;

public class KeywordSearchTests
{
	private KeywordSearch _keywordSearch;
	private BookCatalog _bookCatalog;
	private List<Book> _books;

	[SetUp]
	public void SetUp()
	{
		_keywordSearch = new KeywordSearch();
		var userId = 1;
		var currentUser = true;
		_bookCatalog = new BookCatalog(userId, currentUser);

		_books = new List<Book>
			{
				new Book("Title1", "Author1", new List<string> { "Genre1", "Adventure" }, 2020, "Annotation1 with keywords", "ISBN1", userId),
				new Book("Title2", "Author2", new List<string> { "Genre2", "Fantasy" }, 2021, "Annotation2 without keywords", "ISBN2", userId),
				new Book("Title3", "Author3", new List<string> { "Genre3", "Science Fiction" }, 2022, "Annotation3 with different keywords", "ISBN3", userId)
			};

		foreach (var book in _books)
		{
			_bookCatalog.AddBook(book);
		}
	}

	// Проверяет, что метод Search правильно возвращает книги по ключевым словам в аннотации.
	[Test]
	public void Search_ShouldReturnBooksByKeywordsInAnnotation()
	{
		// Act
		var result = _keywordSearch.Search(_bookCatalog, "keywords");

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Count.EqualTo(3));
		Assert.That(result, Contains.Item(_books[0]));
		Assert.That(result, Contains.Item(_books[2]));
	}

	// Проверяет, что метод Search правильно возвращает книги по ключевым словам в жанрах.
	[Test]
	public void Search_ShouldReturnBooksByKeywordsInGenres()
	{
		// Act
		var result = _keywordSearch.Search(_bookCatalog, "Adventure, Fantasy");

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Count.EqualTo(2));
		Assert.That(result, Contains.Item(_books[0]));
		Assert.That(result, Contains.Item(_books[1]));
	}

	// Проверяет, что метод Search возвращает пустой список, если ключевые слова не найдены.
	[Test]
	public void Search_ShouldReturnEmptyListIfKeywordsNotFound()
	{
		// Act
		var result = _keywordSearch.Search(_bookCatalog, "NonexistentKeyword");

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	// Проверяет, что метод Search не чувствителен к регистру.
	[Test]
	public void Search_ShouldBeCaseInsensitive()
	{
		// Act
		var result = _keywordSearch.Search(_bookCatalog, "adventure, fantasy");

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Count.EqualTo(2));
		Assert.That(result, Contains.Item(_books[0]));
		Assert.That(result, Contains.Item(_books[1]));
	}
}