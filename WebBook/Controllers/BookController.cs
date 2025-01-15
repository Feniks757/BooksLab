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
    
    // POST api/bookcatalog
    [HttpPost]
    public async Task<ActionResult<Book>> Post(Book book)
    {
        if (book == null)
        {
            return BadRequest();
        }

        await using (BookContext db = new())
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