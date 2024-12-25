namespace BooksLab.Books;

public class Book
{
    public readonly string Title;        // название
    public readonly string Author;       // автор
    public readonly List<string> Genres; // жанры
    public readonly int PublicationYear; // год публикации
    public readonly string Annotation;   // аннотация
    public readonly string ISBN;         // ISBN
    public readonly int UserId;          // Идентификатор пользователя

    /* Конструктор класса Book
    Принимает в себя название, автора, жанры, дату публикации, аннотации,
    ISBN и идентификатор пользователя */
    public Book(string title, string author, List<string> genres, int publicationYear, string annotation, string isbn, int userId)
    {
        Title = title;
        Author = author;
        Genres = genres;
        PublicationYear = publicationYear;
        Annotation = annotation;
        ISBN = isbn;
        UserId = userId;
    }

    // Перегрузка функции ToString
    public override string ToString()
    {
        return $"{Title}, {Author}, {string.Join(", ", Genres)}, {PublicationYear}, ISBN: {ISBN}";
    }
}