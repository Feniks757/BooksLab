using System.Security.Cryptography;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionVisitors.Internal;
using SQLitePCL;

namespace BooksLab.Controllers;

[ApiController]
[Route("/api/[controller]")] // /api/[controller]
public class BookController : ControllerBase
{
    //private BookContext db;
    // /api/book/getbooks

    public BookController()
    {
        //db = new BookContext();
    }

    [HttpGet("getbooks")]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        int i = 0;
        await using (BookContext db = new(1))
        {
            return await db.Books.ToListAsync();
        }

    }
    //https://localhost:7242/api/book/Сталин
    
    // GET api/book/title
    [HttpGet("{title}")]
    public async Task<ActionResult<IEnumerable<Book>>> Get(int userId, string title)
    {
        return await new BookSearch().SearchByTitleAsync(userId, title);
    }

    [HttpGet("searchby")]
    public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(int userId, string searchType, string searchQuery)
    {
        var books = new List<Book>();
        switch (searchType)
        {
            case "title": 
                books = await new BookSearch().SearchAsync(userId, searchQuery, (Book book) => book.Title);
                break;
            case "author": 
                books =  await new BookSearch().SearchAsync(userId, searchQuery, (Book book) => book.Author);
                break;
            case "isbn": 
                books = await new BookSearch().SearchAsync(userId, searchQuery, (Book book) => book.ISBN);
                break;
            case "keywords":
                books =  await new KeywordSearch().SearchAsync(userId, searchQuery, (Book book) => "");
                break;
            default:
                return BadRequest("Invalid search type.");
        }
        
        return Ok(books);
    }

    // POST api/bookcatalog
    [HttpPost()]
    public async Task<ActionResult<Book>> Post(Book book)
    {
        Console.WriteLine("post request");
        if (book == null)
        {
            return BadRequest();
        }

        await using (BookContext db = new(0))
        {
            await db.Books.AddAsync(book);
            await db.SaveChangesAsync();
        }
        return Ok(book);
    }
    
    [HttpPost("addbook")]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        Console.WriteLine("adding book");
        if (book == null)
        {
            return BadRequest("Book data is invalid.");
        }

        await using (var db = new BookContext(0))
        {
            await db.Books.AddAsync(book);
            await db.SaveChangesAsync();
        }
        
        return Ok(book);
    }
    /*// GET
    public IActionResult Index()
    {
        return View();
    }*/
}