#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CA1852 // A type can be sealed because it has no subtypes in its containing assembly and is not externally visible

using DotMake.CommandLine;
using LibraryApp.DbContext;
using LibraryApp.Exceptions;
using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Services;
using Microsoft.EntityFrameworkCore;

Cli.Run<RootCliCommand>(args);

[CliCommand(Description = "A simple library manegement WEB API.")]
internal class RootCliCommand
{
	public const string DatabaseDirectoryName = "Database";
	public void Run()
	{
		var builder = WebApplication.CreateBuilder();
		_ = builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		_ = builder.Services.AddProblemDetails();
		_ = builder.Services.AddControllers();

		_ = builder.Services.AddDbContext<LibraryContext>();

		_ = builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
		_ = builder.Services.AddTransient<IBookRepository, BookRepository>();
		_ = builder.Services.AddTransient<IAuthorService, AuthorService>();
		_ = builder.Services.AddTransient<IBookService, BookService>();

		var app = builder.Build();

		_ = app.MapControllers();
		_ = app.UseExceptionHandler();

		app.Run();
	}

	[CliCommand(Description = "Fill the database with initial placeholder values.")]
	internal class SeedCommand
	{
		[CliOption(Description = "Delete database if it already exists.")]
		public bool Clean { get; set; }

		public void Run()
		{
			using var context = new LibraryContext(this.Clean);
			ReadOnlySpan<Type> classes = [typeof(Author), typeof(Book)];
			try {
				foreach (var item in classes) {
					_ = context.Database.ExecuteSqlRaw(File.ReadAllText($"{DatabaseDirectoryName}/{item.Name}sSeed.sql"));
				}
				_ = context.SaveChanges();
				Console.WriteLine("Filled seed data successfully.");
			} catch (Exception e) {
				Console.WriteLine($"[ Error ]\t{e.Message}");
			}
		}
	}
}
