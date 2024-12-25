using BooksLab.Output;

namespace BooksLab.Books;
using BooksLab.Interface;


internal class BookCatalog
{
    public int UserId { get; }

    private bool CurrentUser { get; }

    public List<Book> Books { get; private set; }
    private readonly IBookLoader _bookLoader;
    private readonly IBookSaver _bookSaver;

    public BookCatalog()
    {
        Books = new();
        _bookLoader = new BookLoader();
        _bookSaver = new BookSaver();
    }

    public BookCatalog(int id) : this(id, true) { }

    public BookCatalog(int id, bool currentUser)
    {
        UserId = id;
        CurrentUser = currentUser;
        Books = new List<Book>();
        _bookLoader = new BookLoader();
        _bookSaver = new BookSaver();
    }

    // Асинхронная загрузка книг из файла, с фильтрацией по пользователю
    public async Task LoadBooksFromFileAsync()
    {
        Books = await _bookLoader.LoadAsync(UserId, CurrentUser); // Загружаем книги через BookLoader
    }

    // Асинхронное сохранение книг в файл
    public async Task SaveBooksToFileAsync()
    {
        await _bookSaver.SaveAsync(Books); // Сохраняем книги через BookSaver
    }

    public static BookCatalog operator +(BookCatalog catalog1, BookCatalog catalog2)
    {
        if (catalog1 == null) return catalog2;
        if (catalog2 == null) return catalog1;

        BookCatalog combinedCatalog = new();
        combinedCatalog.Books.AddRange(catalog1.Books);
        combinedCatalog.Books.AddRange(catalog2.Books);

        return combinedCatalog;
    }

    // Прочие методы поиска книг
    public async Task<IEnumerable<Book>> FindBooksByTitleAsync(string title)
    {
        return await Task.Run(() => Books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<IEnumerable<Book>> FindBooksByAuthorAsync(string author)
    {
        return await Task.Run(() => Books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<Book> FindBookByISBNAsync(string isbn)
    {
        return await Task.Run(() => Books.FirstOrDefault(b => b.ISBN == isbn));
    }

    public async Task<IEnumerable<Book>> FindBooksByKeywordsAsync(string[] keywords)
    {
        return await Task.Run(() => Books.Where(b => b.Annotation.Contains(string.Join(" ", keywords), StringComparison.OrdinalIgnoreCase)));
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine("Книга добавлена в каталог!");
    }
}
