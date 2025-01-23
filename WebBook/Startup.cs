using BooksLab.Books;
using Microsoft.EntityFrameworkCore;
using WebBook.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string con = "server=localhost;user=user;password=password;database=books;";
        var version = new MySqlServerVersion(new Version(8, 0, 11));
        services.AddDbContextFactory<BookContext>(options => options.UseMySql(con, version));

        services.AddTransient<IBookService, BookService>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseDefaultFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}