using System.Security.Cryptography;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionVisitors.Internal;
using SQLitePCL;

namespace WebBook.Controllers;
/*
 *   Контроллер. Предназначен преимущественно для обработки запросов протокола HTTP:
 *  Get, Post, Put, Delete, Patch, Head, Options
 */
[ApiController]
[Route("/api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("getbooks")]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        var books = await _bookService.GetBooksAsync();
        return Ok(books);
    }

    [HttpGet("searchby")]
    public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(int userId, string searchType, string searchQuery)
    {
        try
        {
            var books = await _bookService.SearchBooksAsync(userId, searchType, searchQuery);
            return Ok(books);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("addbook")]
    public async Task<IActionResult> AddBook([FromBody] Book book, [FromQuery] int userId)
    {
        try
        {
            var addedBook = await _bookService.AddBookAsync(book, userId);
            return Ok(addedBook);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
