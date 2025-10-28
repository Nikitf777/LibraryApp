using LibraryApp.Exceptions;
using LibraryApp.Models;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
	private readonly IAuthorRepository repository = authorRepository;
	public async Task<IEnumerable<AuthorDto>> SearchForAuthors(string nameFilter)
	{
		return await this.repository.FetchAuthors(nameFilter);
	}

	public async Task<Author> RetriveSpecificAuthorDetails(uint id)
	{
		return await this.repository.FetchSpecificAuthor(id) ?? throw new NotFoundException($"An author with id {id} was not found");
	}

	public async Task CreateAuthor(string name, DateTime dateOfBirth)
	{
		await this.repository.InsertAuthor(name, dateOfBirth);
	}

	public async Task ModifyAuthor(uint id, string name, DateTime dateOfBirth)
	{
		await this.repository.UpdateAuthor(id, name, dateOfBirth);
	}

	public async Task RemoveAuthor(uint id)
	{
		await this.repository.RemoveAuthor(id);
	}
}
