using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DbContext;

public class LibraryContext : Microsoft.EntityFrameworkCore.DbContext
{
	public DbSet<Author> Authors { get; set; } = null!;
	public DbSet<Book> Books { get; set; } = null!;

	public LibraryContext() : this(new DbContextOptions<LibraryContext>()) { }
	public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
	{
		_ = this.Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		_ = optionsBuilder.UseSqlite($"Data Source={RootCliCommand.DatabaseDirectoryName}/database.sqlite");
	}
}
