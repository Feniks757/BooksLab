using BooksLab;

namespace WebBook;
class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .Build()
            .Run();
    }
}
    