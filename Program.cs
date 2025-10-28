using LibraryApp.DbContext;
using LibraryApp.Exceptions;
using LibraryApp.Repositories;
using LibraryApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>();

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IBookService, BookService>();

new LibraryContext().Database.EnsureCreated();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
