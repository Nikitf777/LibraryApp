using LibraryApp.Models;
using LibraryApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IBasicRepository<Author>, BasicRepository<Author>>();
builder.Services.AddSingleton<IBasicRepository<Book>, BasicRepository<Book>>();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
