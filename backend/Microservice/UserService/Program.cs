using TextFileDataStore;
using UserService.Models;
using UserService.Repositories;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Set up file path for text storage
            var dataFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "user.txt");

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<ITextFileStorage<User>>(sp => new TextFileStorage<User>(dataFilePath));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.MapControllers();
            app.Run();
        }
    }
}
