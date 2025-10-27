using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DbContext;

public class LibraryContext : Microsoft.EntityFrameworkCore.DbContext
{
	public DbSet<Author> Authors { get; set; } = null!;
	public DbSet<Book> Books { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		_ = optionsBuilder.UseSqlite("Data Source=database.sqlite");
	}
}
