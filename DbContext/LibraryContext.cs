using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DbContext;

public class LibraryContext : Microsoft.EntityFrameworkCore.DbContext
{
	public DbSet<Author> Authors { get; set; } = null!;
	public DbSet<Book> Books { get; set; } = null!;

	public LibraryContext(bool delete = false) : this(new DbContextOptions<LibraryContext>(), delete) { }
	public LibraryContext(DbContextOptions<LibraryContext> options, bool delete = false) : base(options)
	{
		if (delete) {
			_ = this.Database.EnsureDeleted();
		}
		_ = this.Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		_ = optionsBuilder.UseSqlite($"Data Source={RootCliCommand.DatabaseDirectoryName}/database.sqlite");
	}
}
