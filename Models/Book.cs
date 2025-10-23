namespace LibraryApp.Models;

public class Book
{
	public int Id { get; set; }
	public string Title { get; set; } = "";
	public int PublishedYear { get; set; }
	public int AuthorId { get; set; }
	public Author Author { get; set; } = null!;
}

public class BookListDto
{
	public int Id { get; set; }
	public string Title { get; set; } = "";
	public int PublishedYear { get; set; }
	public string Author { get; set; } = "";
}
