using System.Collections.Generic;

namespace BooksTest;

public class KeywordSearchTests
{
	private KeywordSearch _keywordSearch;
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
		_keywordSearch = new KeywordSearch();
		userId = 1;
		var currentUser = true;
		
		_books = new List<Book>
			{
				new Book("Title1", "Author1", new List<string> { "Genre1", "Adventure" }, 2020, "Annotation1 with keywords", "ISBN1", userId),
				new Book("Title2", "Author2", new List<string> { "Genre2", "Fantasy" }, 2021, "Annotation2 without keywords", "ISBN2", userId),
				new Book("Title3", "Author3", new List<string> { "Genre3", "Science Fiction" }, 2022, "Annotation3 with different keywords", "ISBN3", userId)
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
	
	// Проверяет, что метод Search правильно возвращает книги по ключевым словам в аннотации.
	[Test]
	public void Search_ShouldReturnBooksByKeywordsInAnnotation()
	{
		// Act
		var result = _keywordSearch.SearchAsync(userId, "keywords").Result;

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Count.EqualTo(3));
		//Assert.That(result, Contains.Item(_books[0]));
		//Assert.That(result, Contains.Item(_books[2]));
	}

	// Проверяет, что метод Search правильно возвращает книги по ключевым словам в жанрах.
	[Test]
	public void Search_ShouldReturnBooksByKeywordsInGenres()
	{
		// Act
		var result = _keywordSearch.SearchAsync(userId, "Adventure, Fantasy").Result;

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Count.EqualTo(2));
		//Assert.That(result, Contains.Item(_books[0]));
		//Assert.That(result, Contains.Item(_books[1]));
	}

	// Проверяет, что метод Search возвращает пустой список, если ключевые слова не найдены.
	[Test]
	public void Search_ShouldReturnEmptyListIfKeywordsNotFound()
	{
		// Act
		var result = _keywordSearch.SearchAsync(userId, "NonexistentKeyword").Result;

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
	}

	// Проверяет, что метод Search не чувствителен к регистру.
	[Test]
	public void Search_ShouldBeCaseInsensitive()
	{
		// Act
		var result = _keywordSearch.SearchAsync(userId, "adventure, fantasy").Result;

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Has.Count.EqualTo(2));
		//Assert.That(result, Contains.Item(_books[0]));
		//Assert.That(result, Contains.Item(_books[1]));
	}
}