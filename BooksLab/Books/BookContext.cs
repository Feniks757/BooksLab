//using BooksLab.DataBase;
using Microsoft.EntityFrameworkCore;


namespace BooksLab.Books;
using BooksLab.Interface;


public class BookContext : DbContext
{
    public int UserId { get; set; }

    private bool CurrentUser { get; }

    //Set через который происходит взаимодействие с БД
    public DbSet<Book> Books => Set<Book>();

    public BookContext() => Database.EnsureCreated();

    public BookContext(int id) : this(id, true) =>  Database.EnsureCreated();

    public BookContext(int id, bool currentUser)
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
}
