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
    
    //функция настройки сервисов. Здесь подключаются 
    public void ConfigureServices(IServiceCollection services)
    {
        // Регистрация фабрики контекста базы данных
        services.AddDbContextFactory<BookContext>();
        
        // Регистрация контроллера как синглтона
        services.AddSingleton<BookController>();
        
        // Регистрация всех контроллеров
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