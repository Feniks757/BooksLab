using BooksLab.Output;
using BooksLab.Functions;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using BooksLab.Interface;
using BooksLab.Storage;


namespace BooksLab;

class Program
{
    public static void Main(string[] args)
    {
        /*var _books = new List<Book>
        {
            new Book("Title1", "Author1", new List<string> { "Genre1" }, 2020, "Annotation1", "ISBN1", 222),
            new Book("Title2", "Author2", new List<string> { "Genre2" }, 2021, "Annotation2", "ISBN2", 222),
            new Book("Title3", "Author3", new List<string> { "Genre3" }, 2022, "Annotation3", "ISBN3", 222)
        };

        using (var _bookCatalog = new BookCatalog())
        {
            var isbnsToRemove = _books
                .Select(b => b.ISBN)
                .ToHashSet();
            var removeBooks = _bookCatalog.Books.AsEnumerable().Where(b => isbnsToRemove.Contains(b.ISBN)).ToList();
            foreach (var book in removeBooks)
            {
                _bookCatalog.Books.Remove(book);
            }
            ;
            _bookCatalog.SaveChanges();
        }*/

        try
        {
            Console.WriteLine("Введите ваш идентификатор пользователя: ");
            int userId;

            while (!Validator.ValidateUserId(Console.ReadLine(), out userId))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите числовой идентификатор.");
            }

            int choice;
            do
            {
                MenusManager.MainMenu(userId);
                choice = Inp.Input(1, 6);

                using (BookCatalog userCatalog = new(userId))
                {
                    switch (choice)
                    {
                        case 1:
                            // Добавление книги
                            AddBookAsync(userCatalog, userId);
                            break;

                        case 2:
                            Console.Write("Введите название книги: ");
                            string title = Console.ReadLine()!;
                            IBookSearch titleSearch = new TitleSearch();
                            var booksByTitle = titleSearch.Search(userCatalog, title);
                            Display.ShowBooksAsync(booksByTitle);
                            break;

                        case 3:
                            Console.Write("Введите имя автора: ");
                            string author = Console.ReadLine()!;
                            IBookSearch authorSearch = new AuthorSearch();
                            var booksByAuthor = authorSearch.Search(userCatalog, author);
                            Display.ShowBooks(booksByAuthor.Result);
                            break;

                        case 4:
                            Console.Write("Введите ISBN: ");
                            string isbn = Console.ReadLine()!;
                            IBookSearch isbnSearch = new ISBNBookSearch();
                            var booksByIsbn = isbnSearch.Search(userCatalog, isbn);
                            Display.ShowBooks(booksByIsbn.Result);
                            break;
                        case 5:
                            Console.Write("Введите ключевые слова (через запятую): ");
                            string keywordQuery = Console.ReadLine()!;
                            IBookSearch keywordSearch = new KeywordSearch();
                            var booksByKeywords = keywordSearch.Search(userCatalog, keywordQuery);
                            Display.ShowBooksAsync(booksByKeywords);
                            break;
                    }

                    if (choice != 6)
                    {
                        Inp.Pause();
                    }
                }
               
            } while (choice != 6);
            
            Console.WriteLine("До свидания!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Произошло исключение.");
            Console.WriteLine(e.Message);
        }
    }

    private static async void AddBookAsync(BookCatalog catalog, int userId)
    {

        Console.Write("Введите название книги: ");
        string title;
        while (true)
        {
            title = Console.ReadLine()!;
            if (Validator.ValidateBookTitle(title))
                break;

            Console.WriteLine("Некорректное название. Пожалуйста, введите ещё раз:");
        }

        Console.Write("Введите имя автора: ");
        string author;
        while (true)
        {
            author = Console.ReadLine()!;
            if (Validator.ValidateAuthorName(author))
                break;

            Console.WriteLine("Некорректное имя автора. Пожалуйста, введите ещё раз:");
        }

        Console.Write("Введите жанры (через запятую): ");
        List<string> genres;
        while (!Validator.ValidateGenres(Console.ReadLine()!, out genres))
        {
            Console.WriteLine("Некорректные жанры. Пожалуйста, введите ещё раз:");
        }

        Console.Write("Введите дату публикации (ГГГГ): ");
        int publicationYear = Validator.ValidatePublicationYear();

        Console.Write("Введите аннотацию: ");
        string annotation = Console.ReadLine()!;
        while (!Validator.ValidateAnnotation(annotation))
        {
            Console.WriteLine("Некорректная аннотация. Пожалуйста, введите ещё раз:");
            annotation = Console.ReadLine()!;
        }

        Console.Write("Введите ISBN: ");
        string isbn = Console.ReadLine()!;
        while (!Validator.ValidateISBN(isbn))
        {
            Console.WriteLine("Некорректный ISBN. Пожалуйста, введите ещё раз:");
            isbn = Console.ReadLine()!;
        }

        Book book = new(title, author, genres, publicationYear, annotation, isbn, userId);
        catalog.AddBookAcync(book);
    }
}
