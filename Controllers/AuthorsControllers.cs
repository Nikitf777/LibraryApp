using Microsoft.EntityFrameworkCore;
using LibraryApp.DbContext;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController() : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<AuthorDto>> GetAll(string nameFilter = "")
	{
		using var context = new LibraryContext();
		return await (
			from author in context.Authors
			where author.Name.Contains(nameFilter)
			join book in context.Books on author.Id equals book.AuthorId into books
			select new AuthorDto {
				Id = author.Id,
				Name = author.Name,
				DateOfBirth = author.DateOfBirth,
				BookCount = books.Count(),
			}).ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<Author?> Get(int id)
	{
		using var context = new LibraryContext();
		return await context.Authors.FindAsync(id);
	}

	[HttpPost]
	public async Task<string> Post(string name, DateTime dateOfBirth)
	{
		using var context = new LibraryContext();
		_ = context.Authors.Add(new Author {
			Name = name,
			DateOfBirth = dateOfBirth
		});
		_ = await context.SaveChangesAsync();
		return $"Added a new Author\n";
	}

	[HttpPut]
	public async Task<string?> Put(int id, string name, long dateOfBirth)
	{
		using var context = new LibraryContext();
		var authorEntity = await context.Authors.FindAsync(id);
		if (authorEntity is null) {
			return null;
		}
		authorEntity.Name = name;
		authorEntity.DateOfBirth = DateTimeOffset.FromUnixTimeSeconds(dateOfBirth).DateTime;
		_ = context.Authors.Update(authorEntity);
		_ = await context.SaveChangesAsync();
		return "Updated the author\n";
	}

	[HttpDelete("{id}")]
	public async Task<string> DeleteAsync(int id)
	{
		using var context = new LibraryContext();
		_ = context.Authors.Remove(new Author { Id = id });
		_ = await context.SaveChangesAsync();
		return "Deleted the author\n";
	}
}
