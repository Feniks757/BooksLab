using System.Security.Cryptography;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionVisitors.Internal;
using SQLitePCL;

namespace BooksLab.Controllers;
/*
 *   Контроллер. Предназначен преимущественно для обработки запросов протокола HTTP:
 *  Get, Post, Put, Delete, Patch, Head, Options
 */
[ApiController]
/*
 * запросы к данному контроллеру через
 * /api/book
 */
[Route("/api/[controller]")] 
public class BookController : ControllerBase
{
    private BookContext _db;
    
    public BookController()
    {
        _db = new BookContext();
    }
    
    // GET запрос /api/bool/getbooks
    [HttpGet("getbooks")]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        return await _db.Books.ToListAsync();
    }

    // GET запрос api/book/searchby?userId=${userId}&searchType=${searchType}&searchQuery=${searchQuery}
    [HttpGet("searchby")]
    public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(int userId, string searchType, string searchQuery)
    {
        _db.UserId = userId;
        var books = new List<Book>();
        switch (searchType)
        {
            case "title": 
                books = await new BookSearch().SearchAsync(_db, searchQuery, (Book book) => book.Title);
                break;
            case "author": 
                books =  await new BookSearch().SearchAsync(_db, searchQuery, (Book book) => book.Author);
                break;
            case "isbn": 
                books = await new BookSearch().SearchAsync(_db, searchQuery, (Book book) => book.ISBN);
                break;
            case "keywords":
                books =  await new KeywordSearch().SearchAsync(_db, searchQuery, (Book book) => "");
                break;
            default:
                return BadRequest("Invalid search type.");
        }
        return Ok(books);
    }
    
    /*
     *  POST запрос
     *  fetch(`/api/book/addbook?userId=${userId}`, {
     *       method: 'POST',
     *       headers: {
     *           'Content-Type': 'application/json'
     *       },
     *       body: JSON.stringify(book)
     *   }
     */
    [HttpPost("addbook")]
    public async Task<IActionResult> AddBook([FromBody] Book book, [FromQuery] int userId)
    {
        Console.WriteLine("Adding book");
        if (book == null)
        {
            return BadRequest("Book data is invalid.");
        }

        book.UserId = userId;
        await _db.AddBookAsync(book);

        return Ok(book);
    }
}