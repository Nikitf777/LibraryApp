namespace LibraryApp.Models;

public class Author(string name, DateTime dateOfBirth) : IKeyedEntity
{
	public int Id { get; set; }
	public string Name { get; } = name;
	public DateTime DateOfBirth { get; } = dateOfBirth;
}
