namespace LibraryApp.Models;

public class Author
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public DateTime DateOfBirth { get; set; }
	public ICollection<Book> Books { get; set; } = [];
}

public class AuthorDto
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public DateTime DateOfBirth { get; set; }
	public int BookCount { get; set; }
}
