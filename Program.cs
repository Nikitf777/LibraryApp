using LibraryApp.DbContext;
using LibraryApp.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>();

new LibraryContext().Database.EnsureCreated();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
