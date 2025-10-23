using LibraryApp.DbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>();
new LibraryContext().Database.EnsureCreated();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
