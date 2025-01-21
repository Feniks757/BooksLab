using BooksLab.Books;
using Microsoft.EntityFrameworkCore;

namespace WebBook;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    
    //функция настройки сервисов. Здесь подключаются 
    public void ConfigureServices(IServiceCollection services)
    {
        
        string con = "server=localhost;user=user;password=password;database=books;";
        var version = new MySqlServerVersion(new Version(8, 0, 11));
        services.AddDbContextFactory<BookContext>(options => options.UseMySql(con, version));

        services.AddControllers();
    }
 
    public void Configure(IApplicationBuilder app)
    {
        //режим разработчика
        app.UseDeveloperExceptionPage();
        
        // делает http -> https
        app.UseHttpsRedirection();
        
        //смотрит файлы для веб страничек в wwwroot
        app.UseStaticFiles(); 
        // Это позволяет использовать index.html как файл по умолчанию
        app.UseDefaultFiles(); 

        //определяет какой контроллер и действие будут обрабатывать запрос на основе URL
        app.UseRouting();
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
        });
    }
}