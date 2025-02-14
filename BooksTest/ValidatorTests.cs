using System.Collections.Generic;

namespace BooksTest;

public class ValidatorTests
{
    // Проверяет, что метод ValidateUserId правильно возвращает true для корректного userId.
    [Test]
    public void ValidateUserId_ShouldReturnTrueForValidUserId()
    {
        // Arrange
        var input = "123";
        int userId;

        // Act
        var result = Validator.ValidateUserId(input, out userId);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(userId, Is.EqualTo(123));
    }

    // Проверяет, что метод ValidateUserId возвращает false для некорректного userId.
    [Test]
    public void ValidateUserId_ShouldReturnFalseForInvalidUserId()
    {
        // Arrange
        var input = "abc";
        int userId;

        // Act
        var result = Validator.ValidateUserId(input, out userId);

        // Assert
        Assert.That(result, Is.False);
        Assert.That(userId, Is.EqualTo(0));
    }

    // Проверяет, что метод ValidateUserId возвращает false для userId меньше или равного 0.
    [Test]
    public void ValidateUserId_ShouldReturnFalseForZeroOrNegativeUserId()
    {
        // Arrange
        var input = "0";
        int userId;

        // Act
        var result = Validator.ValidateUserId(input, out userId);

        // Assert
        Assert.That(result, Is.False);
        Assert.That(userId, Is.EqualTo(0));
    }

    // Проверяет, что метод ValidateBookTitle правильно возвращает true для корректного названия книги.
    [Test]
    public void ValidateBookTitle_ShouldReturnTrueForValidTitle()
    {
        // Arrange
        var title = "Valid Title";

        // Act
        var result = Validator.ValidateBookTitle(title);

        // Assert
        Assert.That(result, Is.True);
    }

    // Проверяет, что метод ValidateBookTitle возвращает false для пустого или null названия книги.
    [Test]
    public void ValidateBookTitle_ShouldReturnFalseForEmptyOrNullTitle()
    {
        // Arrange
        var title = "";

        // Act
        var result = Validator.ValidateBookTitle(title);

        // Assert
        Assert.That(result, Is.False);
    }

    // Проверяет, что метод ValidateAuthorName правильно возвращает true для корректного имени автора.
    [Test]
    public void ValidateAuthorName_ShouldReturnTrueForValidAuthorName()
    {
        // Arrange
        var author = "Valid Author";

        // Act
        var result = Validator.ValidateAuthorName(author);

        // Assert
        Assert.That(result, Is.True);
    }

    // Проверяет, что метод ValidateAuthorName возвращает false для пустого или null имени автора.
    [Test]
    public void ValidateAuthorName_ShouldReturnFalseForEmptyOrNullAuthorName()
    {
        // Arrange
        var author = "";

        // Act
        var result = Validator.ValidateAuthorName(author);

        // Assert
        Assert.That(result, Is.False);
    }

    // Проверяет, что метод ValidateGenres правильно возвращает true для корректного списка жанров.
    [Test]
    public void ValidateGenres_ShouldReturnTrueForValidGenres()
    {
        // Arrange
        var genresInput = "Genre1, Genre2, Genre3";
        List<string> genres;

        // Act
        var result = Validator.ValidateGenres(genresInput, out genres);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(genres, Has.Count.EqualTo(3));
        Assert.That(genres, Contains.Item("Genre1"));
        Assert.That(genres, Contains.Item("Genre2"));
        Assert.That(genres, Contains.Item("Genre3"));
    }

    // Проверяет, что метод ValidateGenres возвращает false для пустого или null списка жанров.
    [Test]
    public void ValidateGenres_ShouldReturnFalseForEmptyOrNullGenres()
    {
        // Arrange
        var genresInput = "";
        List<string> genres;

        // Act
        var result = Validator.ValidateGenres(genresInput, out genres);

        // Assert
        Assert.That(result, Is.False);
        Assert.That(genres, Is.Empty);
    }

    // Проверяет, что метод ValidateAnnotation правильно возвращает true для корректной аннотации.
    [Test]
    public void ValidateAnnotation_ShouldReturnTrueForValidAnnotation()
    {
        // Arrange
        var annotation = "Valid Annotation";

        // Act
        var result = Validator.ValidateAnnotation(annotation);

        // Assert
        Assert.That(result, Is.True);
    }

    // Проверяет, что метод ValidateAnnotation возвращает false для пустого или null аннотации.
    [Test]
    public void ValidateAnnotation_ShouldReturnFalseForEmptyOrNullAnnotation()
    {
        // Arrange
        var annotation = "";

        // Act
        var result = Validator.ValidateAnnotation(annotation);

        // Assert
        Assert.That(result, Is.False);
    }

    // Проверяет, что метод ValidateISBN правильно возвращает true для корректного ISBN-10.
    [Test]
    public void ValidateISBN_ShouldReturnTrueForValidISBN10()
    {
        // Arrange
        var isbn = "123456789X";

        // Act
        var result = Validator.ValidateISBN(isbn);

        // Assert
        Assert.That(result, Is.True);
    }

    // Проверяет, что метод ValidateISBN правильно возвращает true для корректного ISBN-13.
    [Test]
    public void ValidateISBN_ShouldReturnTrueForValidISBN13()
    {
        // Arrange
        var isbn = "9783161484100";

        // Act
        var result = Validator.ValidateISBN(isbn);

        // Assert
        Assert.That(result, Is.True);
    }

    // Проверяет, что метод ValidateISBN возвращает false для некорректного ISBN.
    [Test]
    public void ValidateISBN_ShouldReturnFalseForInvalidISBN()
    {
        // Arrange
        var isbn = "12345";

        // Act
        var result = Validator.ValidateISBN(isbn);

        // Assert
        Assert.That(result, Is.False);
    }
}
