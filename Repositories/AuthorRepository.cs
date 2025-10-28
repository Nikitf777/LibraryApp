using LibraryApp.DbContext;
using LibraryApp.Exceptions;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories;

public class AuthorRepository(LibraryContext context) : IAuthorRepository
{
	private readonly LibraryContext context = context;
	public async Task<IEnumerable<AuthorDto>> FetchAuthors(string nameFilter = "")
	{
		return await (
			from author in this.context.Authors
			where author.Name.Contains(nameFilter)
			join book in this.context.Books on author.Id equals book.AuthorId into books
			select new AuthorDto {
				Id = author.Id,
				Name = author.Name,
				DateOfBirth = author.DateOfBirth,
				BookCount = books.Count(),
			}).ToListAsync();
	}

	public async Task<Author> FetchSpecificAuthor(uint id)
	{
		return await this.context.Authors.FindAsync(id) ?? throw new NotFoundException($"Could not fetch a non-existing author with id {id}");
	}

	public async Task InsertAuthor(string name, DateTime dateOfBirth)
	{
		_ = this.context.Authors.Add(new Author {
			Name = name,
			DateOfBirth = dateOfBirth
		});
		_ = await this.context.SaveChangesAsync();
	}

	public async Task UpdateAuthor(uint id, string name, DateTime dateOfBirth)
	{
		if (await this.context.Authors.Where(author => author.Id == id).ExecuteUpdateAsync(
			setters => setters
				.SetProperty(author => author.Name, name)
				.SetProperty(author => author.DateOfBirth, dateOfBirth)) == 0
		) {
			throw new NotFoundException($"Could not modify a non-existing author with id {id}");
		}
	}

	public async Task RemoveAuthor(uint id)
	{
		if (await this.context.Authors.Where(author => author.Id == id).ExecuteDeleteAsync() == 0) {
			throw new NotFoundException($"Could not delete a non-existing author with id {id}");
		}
	}
}
