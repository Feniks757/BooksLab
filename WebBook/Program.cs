using BooksLab;
using BooksLab.Books;
using Microsoft.EntityFrameworkCore;
using WebBook.Services;

namespace WebBook;
class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string con = "server=localhost;user=root;password=password;database=books;";
        var version = new MySqlServerVersion(new Version(8, 0, 11));
        builder.Services.AddDbContextFactory<BookContext>(options => options.UseMySql(con, version));

        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddControllers();
        

        var app = builder.Build();
        app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseDefaultFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        app.Run();
    }
}
    