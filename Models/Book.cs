namespace LibraryApp.Models;

public class Book
{
	public uint Id { get; set; }
	public string Title { get; set; } = "";
	public int PublishedYear { get; set; }
	public uint AuthorId { get; set; }
	public Author Author { get; set; } = null!;
}

public class BookListDto
{
	public uint Id { get; set; }
	public string Title { get; set; } = "";
	public int PublishedYear { get; set; }
	public string Author { get; set; } = "";
}
