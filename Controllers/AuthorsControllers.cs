using LibraryApp.DbContext;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController() : ControllerBase
{
	[HttpGet]
	public IEnumerable<AuthorDto> GetAll()
	{
		using var context = new LibraryContext();
		var authors =
			from author in context.Authors
			join book in context.Books on author.Id equals book.AuthorId into books
			select new AuthorDto {
				Id = author.Id,
				Name = author.Name,
				DateOfBirth = author.DateOfBirth,
				Books = from book in books select book.Title,
			};
		return [.. authors];
	}

	[HttpGet("{id}")]
	public Author? Get(int id)
	{
		using var context = new LibraryContext();
		return context.Authors.Find(id);
	}

	[HttpPost]
	public string Post(string name, DateTime dateOfBirth)
	{
		using var context = new LibraryContext();
		_ = context.Authors.Add(new Author {
			Name = name,
			DateOfBirth = dateOfBirth
		});
		_ = context.SaveChanges();
		return $"Added a new Author\n";
	}

	[HttpPut]
	public string? Put(int id, string name, long dateOfBirth)
	{
		using var context = new LibraryContext();
		var authorEntity = context.Authors.Find(id);
		if (authorEntity is null) {
			return null;
		}
		authorEntity.Name = name;
		authorEntity.DateOfBirth = DateTimeOffset.FromUnixTimeSeconds(dateOfBirth).DateTime;
		_ = context.Authors.Update(authorEntity);
		_ = context.SaveChanges();
		return "Updated the author\n";
	}

	[HttpDelete("{id}")]
	public string Delete(int id)
	{
		using var context = new LibraryContext();
		_ = context.Authors.Remove(new Author { Id = id });
		_ = context.SaveChanges();
		return "Deleted the author\n";
	}
}
