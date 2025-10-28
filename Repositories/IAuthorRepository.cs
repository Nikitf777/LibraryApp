using LibraryApp.Models;

namespace LibraryApp.Repositories;

public interface IAuthorRepository
{
	public Task<IEnumerable<AuthorDto>> FetchAuthors(string nameFilter = "");
	public Task<Author> FetchSpecificAuthor(uint id);
	public Task InsertAuthor(string name, DateTime dateOfBirth);
	public Task UpdateAuthor(uint id, string name, DateTime dateOfBirth);
	public Task RemoveAuthor(uint id);
}
