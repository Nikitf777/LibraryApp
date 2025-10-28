using LibraryApp.Models;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
	private readonly IAuthorRepository authorRepository = authorRepository;
	public async Task<IEnumerable<AuthorDto>> SearchForAuthors(string nameFilter)
	{
		return await this.authorRepository.FetchAuthors(nameFilter);
	}

	public async Task<Author> RetriveSpecificAuthorDetails(uint id)
	{
		return await this.authorRepository.FetchSpecificAuthor(id);
	}

	public async Task CreateAuthor(string name, DateTime dateOfBirth)
	{
		await this.authorRepository.InsertAuthor(name, dateOfBirth);
	}

	public async Task ModifyAuthor(uint id, string name, DateTime dateOfBirth)
	{
		await this.authorRepository.UpdateAuthor(id, name, dateOfBirth);
	}

	public async Task RemoveAuthor(uint id)
	{
		await this.authorRepository.RemoveAuthor(id);
	}
}
