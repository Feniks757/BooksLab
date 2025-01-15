using BooksLab.Books;
using BooksLab.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using WebApiApp.Controllers;

namespace BooksLab;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        //string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
        // устанавливаем контекст данных
        
        
        services.AddDbContextFactory<BookContext>();
        
        services.AddSingleton<BookController>();
        services.AddControllers(); // используем контроллеры без представлений
    }
 
    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        
        //app.UsePathBase("/index.html"); 
        app.UseStaticFiles();
        app.UseDefaultFiles(); // Это позволяет использовать index.html как файл по умолчанию

        app.UseRouting();
 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
        });
    }
}