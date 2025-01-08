//using BooksLab.DataBase;
using BooksLab.Output;
using Microsoft.EntityFrameworkCore;


namespace BooksLab.Books;
using BooksLab.Interface;


internal class BookCatalog : DbContext
{
    public int UserId { get; }

    private bool CurrentUser { get; }

    public DbSet<Book> Books => Set<Book>();

    private readonly IBookLoader _bookLoader;
    private readonly IBookSaver _bookSaver;

    public BookCatalog()
    {
        Database.EnsureCreated();
        _bookLoader = new BookLoader();
        _bookSaver = new BookSaver();
        
    } 

    public BookCatalog(int id) : this(id, true)
    {
        Database.EnsureCreated();
    }

    public BookCatalog(int id, bool currentUser)
    {
        Database.EnsureCreated();
        UserId = id;
        CurrentUser = currentUser;
        //Books = new List<Book>();
        _bookLoader = new BookLoader();
        _bookSaver = new BookSaver();
    }

    // Асинхронная загрузка книг из файла, с фильтрацией по пользователю
    /*public async Task LoadBooksFromFileAsync()
    {
        Books = await _bookLoader.LoadAsync(UserId, CurrentUser); // Загружаем книги через BookLoader
    }*/

    // Асинхронное сохранение книг в файл
    /*public async Task SaveBooksToFileAsync()
    {
        await _bookSaver.SaveAsync(Books); // Сохраняем книги через BookSaver
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*optionsBuilder.UseMySql(
            "server=localhost;user=root;password=12345678;database=usersdb5;", 
            new MySqlServerVersion(new Version(8, 0, 11))
        );*/
        optionsBuilder.UseSqlite("Data Source=book.db");
    }
    
    public override int SaveChanges()
    {
        return base.SaveChanges();
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
        SaveChanges();
        //Books.Add(book);
        Console.WriteLine("Книга добавлена в каталог!");
    }
}
