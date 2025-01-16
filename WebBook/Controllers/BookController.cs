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
    private BookContext db;
    // /api/book/getbooks

    public BookController()
    {
        db = new BookContext();
    }

    [HttpGet("getbooks")]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        return await db.Books.ToListAsync();
    }
    //https://localhost:7242/api/book/Сталин
    
    // GET api/book/title
    [HttpGet("{title}")]
    public async Task<ActionResult<IEnumerable<Book>>> Get(int userId, string title)
    {
        db.UserId = userId;
        return await new BookSearch().SearchByTitleAsync(db, title);
    }

    [HttpGet("searchby")]
    public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(int userId, string searchType, string searchQuery)
    {
        db.UserId = userId;
        var books = new List<Book>();
        switch (searchType)
        {
            case "title": 
                books = await new BookSearch().SearchAsync(db, searchQuery, (Book book) => book.Title);
                break;
            case "author": 
                books =  await new BookSearch().SearchAsync(db, searchQuery, (Book book) => book.Author);
                break;
            case "isbn": 
                books = await new BookSearch().SearchAsync(db, searchQuery, (Book book) => book.ISBN);
                break;
            case "keywords":
                books =  await new KeywordSearch().SearchAsync(db, searchQuery, (Book book) => "");
                break;
            default:
                return BadRequest("Invalid search type.");
        }
        
        return Ok(books);
    }

    [HttpPost("addbook")]
    public async Task<IActionResult> AddBook([FromBody] Book book, [FromQuery] int userId)
    {
        Console.WriteLine("adding book");
        if (book == null)
        {
            return BadRequest("Book data is invalid.");
        }

        book.UserId = userId;
        db.UserId = userId;
        await db.AddBookAsync(book);

        return Ok(book);
    }
}