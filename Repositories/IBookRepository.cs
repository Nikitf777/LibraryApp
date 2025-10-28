using LibraryApp.Models;

namespace LibraryApp.Repositories;

public interface IBookRepository
{
	public Task<IEnumerable<BookListDto>> FetchBooks(int fromYear = int.MinValue, int toYear = int.MaxValue);
	public Task<Book> FetchSpecificBook(uint id);
	public Task InsertBook(string title, int publishedYear, uint authorId);
	public Task UpdateBook(uint id, string title, int publishedYear, uint authorId);
	public Task RemoveBook(uint id);
}
