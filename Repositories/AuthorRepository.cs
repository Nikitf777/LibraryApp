using LibraryApp.DbContext;
using LibraryApp.Exceptions;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories;

public class AuthorRepository : IAuthorRepository
{
	public async Task<IEnumerable<AuthorDto>> FetchAuthors(string nameFilter = "")
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

	public async Task<Author> FetchSpecificAuthor(uint id)
	{
		using var context = new LibraryContext();
		return await context.Authors.FindAsync(id) ?? throw new NotFoundException($"Could not fetch a non-existing author with id {id}");
	}

	public async Task InsertAuthor(string name, DateTime dateOfBirth)
	{
		using var context = new LibraryContext();
		_ = context.Authors.Add(new Author {
			Name = name,
			DateOfBirth = dateOfBirth
		});
		_ = await context.SaveChangesAsync();
	}

	public async Task UpdateAuthor(uint id, string name, DateTime dateOfBirth)
	{
		using var context = new LibraryContext();
		var authorEntity = await context.Authors.FindAsync(id) ?? throw new NotFoundException($"Could not modify a non-existing author with id {id}");

		authorEntity.Name = name;
		authorEntity.DateOfBirth = dateOfBirth;
		_ = context.Authors.Update(authorEntity);
		_ = await context.SaveChangesAsync();
	}

	public async Task RemoveAuthor(uint id)
	{
		using var context = new LibraryContext();
		_ = await context.Authors.FindAsync(id) ?? throw new NotFoundException($"Could not remove a non-existing author with id {id}");
		var entity = context.Authors.Remove(new Author { Id = id });
		_ = await context.SaveChangesAsync();
	}
}
