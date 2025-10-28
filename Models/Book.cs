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

public class BookDetailsDto
{
	public uint Id { get; set; }
	public string Title { get; set; } = "";
	public int PublishedYear { get; set; }
	public AuthorDto Author { get; set; } = null!;

	public BookDetailsDto() { }

	public BookDetailsDto(Book book)
	{
		this.Id = book.Id;
		this.Title = book.Title;
		this.PublishedYear = book.PublishedYear;
		this.Author = new AuthorDto(book.Author);
	}
}
