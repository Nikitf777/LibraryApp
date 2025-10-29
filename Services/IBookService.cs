using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBookService
{
	public Task<IEnumerable<BookListDto>> SearchForBooks(int yearFrom = int.MinValue, int yearTo = int.MaxValue);
	public Task<BookDetailsDto> RetriveSpecificBookDetails(uint id);
	public Task CreateBook(string title, int publishedYear, uint authorId);
	public Task ModifyBook(uint id, string title, int publishedYear, uint authorId);
	public Task RemoveBook(uint id);
}
