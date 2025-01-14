using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BooksLab.Books;
using BooksLab.ConsoleCommands;

namespace BooksLab.Controllers;

/*[ApiController]
[Route("api/[controller]")]
public class BookCatalogController: ControllerBase
{
    public BookCatalogController(BookContext bookContext)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        await using (BookContext db = new())
        {
            return await db.Books.ToListAsync();
        }
    }
    
    // GET api/bookcatalog/Мы
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
    
    
}*/