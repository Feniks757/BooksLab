//using BooksLab.DataBase;
using BooksLab.Output;
using Microsoft.EntityFrameworkCore;


namespace BooksLab.Books;
using BooksLab.Interface;


internal class BookCatalog : DbContext
{
    public int UserId { get; }

    private bool CurrentUser { get; }

    //Set через который происходит взаимодействие с БД
    public DbSet<Book> Books => Set<Book>();

    public BookCatalog() => Database.EnsureCreated();

    public BookCatalog(int id) : this(id, true) =>  Database.EnsureCreated();

    public BookCatalog(int id, bool currentUser)
    {
        Database.EnsureCreated();
        UserId = id;
        CurrentUser = currentUser;
    }

    //настройка Базы данных
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //MYSQL
        optionsBuilder.UseMySql(
            "server=localhost;user=user;password=password;database=books;", 
            new MySqlServerVersion(new Version(8, 0, 11))
        );
        //SQLite
        //optionsBuilder.UseSqlite("Data Source=book.db");
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

    //синхронное добавление книги в БД
    public void AddBook(Book book)
    {
        Books.Add(book);
        SaveChanges();
        Console.WriteLine("Книга добавлена в каталог!");
    }
    
    //Ассинхронное добавление книги в БД
    public async Task<Book> AddBookAcync(Book book)
    {
        await Books.AddAsync(book);
        await SaveChangesAsync();
        Console.WriteLine("Книга добавлена в каталог!");
        return book;
    }
}
