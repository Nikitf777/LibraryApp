namespace LibraryApp.Models;

public class Author
{
	public uint Id { get; set; }
	public string Name { get; set; } = "";
	public DateTime DateOfBirth { get; set; }
	public ICollection<Book> Books { get; set; } = [];
}

public class AuthorDto
{
	public uint Id { get; set; }
	public string Name { get; set; } = "";
	public DateTime DateOfBirth { get; set; }
	public int BookCount { get; set; }

	public AuthorDto() { }

	public AuthorDto(Author author)
	{
		this.Id = author.Id;
		this.Name = author.Name;
		this.DateOfBirth = author.DateOfBirth;
		this.BookCount = author.Books.Count;
	}
}
