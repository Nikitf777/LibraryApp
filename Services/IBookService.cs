using LibraryApp.Models;

namespace LibraryApp.Services;

public interface IBookService
{
	public Task<IEnumerable<BookListDto>> SearchForBooks(int fromYear = int.MinValue, int toYear = int.MaxValue);
	public Task<Book> RetriveSpecificBookDetails(uint id);
	public Task CreateBook(string title, int publishedYear, uint authorId);
	public Task ModifyBook(uint id, string title, int publishedYear, uint authorId);
	public Task RemoveBook(uint id);
}
