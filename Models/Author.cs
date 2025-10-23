namespace LibraryApp.Models;

public class Author : IKeyedEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public DateTime DateOfBirth { get; set; }
	public ICollection<Book> Books { get; set; } = [];
}

public class AuthorDto : IKeyedEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public DateTime DateOfBirth { get; set; }
	public IEnumerable<string> Books { get; set; } = [];
}
