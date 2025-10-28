using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IAuthorService
{
	public Task<IEnumerable<AuthorDto>> SearchForAuthors(string nameFilter);
	public Task<Author> RetriveSpecificAuthorDetails(uint id);
	public Task CreateAuthor(string name, DateTime dateOfBirth);
	public Task ModifyAuthor(uint id, string name, DateTime dateOfBirth);
	public Task RemoveAuthor(uint id);
}
