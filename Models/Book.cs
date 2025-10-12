namespace LibraryApp.Models;

public class Book(string title, int publishedYear, int authorId) : IId
{
	public int Id { get; set; }
	public string Title { get; } = title;
	public int PublishedYear { get; } = publishedYear;
	public int AuthroId { get; } = authorId;
}
