using Microsoft.EntityFrameworkCore;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors();

var app = builder.Build();
 app.UseCors(policy =>
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .WithOrigins("http://localhost:3000","https://localhost:3000")); // Adjust the origin as needed
          

app.MapControllers();
using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitalizer.SeedData(context);
}
catch (Exception ex)
{
    // Handle exceptions during database initialization
    Console.WriteLine(ex.Message);
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during database initialization");
    }

app.Run();
