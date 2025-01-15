/*var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();*/

using BooksLab;

/*var builder = WebApplication.CreateBuilder(args);
Startup startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
startup.Configure(app);*/
        


Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        //webBuilder.UseUrls("https://localhost:7151/"); // Задайте URL здесь
    })
    .Build()
    .Run();
    