//using BooksLab.DataBase;
using Microsoft.EntityFrameworkCore;


namespace BooksLab.Books;
using BooksLab.Interface;


public class BookContext : DbContext
{
    public int UserId { get; set; }

    private bool CurrentUser { get; }
    
    private readonly DbContextOptions<BookContext> _configureOptions;

    //Set через который происходит взаимодействие с БД
    public DbSet<Book> Books => Set<Book>();

    public BookContext()
    {
        Database.EnsureCreated();
    }

    public BookContext(DbContextOptions<BookContext> configureOptions) : base(configureOptions)
    {
        _configureOptions = configureOptions;
        Database.EnsureCreated();
    }

    //настройка Базы данных
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //MYSQL
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(
            "server=localhost;user=user;password=password;database=books;", 
            new MySqlServerVersion(new Version(8, 0, 11))
        );
        //SQLite
        //optionsBuilder.UseSqlite("Data Source=book.db");
    }

    //синхронное добавление книги в БД
    public void AddBook(Book book)
    {
        Books.Add(book);
        SaveChanges();
        Console.WriteLine("Книга добавлена в каталог!");
    }
    
    //Ассинхронное добавление книги в БД
    public async Task<Book> AddBookAsync(Book book)
    {
        await Books.AddAsync(book);
        await SaveChangesAsync();
        Console.WriteLine("Книга добавлена в каталог!");
        return book;
    }
    
    // Метод для получения всех книг из базы данных
    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await Books.ToListAsync();  
    }
}
