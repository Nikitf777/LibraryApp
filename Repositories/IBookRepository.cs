using LibraryApp.Models;

namespace LibraryApp.Repositories;

public interface IBookRepository
{
	public Task<IEnumerable<BookListDto>> FetchBooks(int yearFrom = int.MinValue, int yearTo = int.MaxValue);
	public Task<BookDetailsDto> FetchSpecificBook(uint id);
	public Task InsertBook(string title, int publishedYear, uint authorId);
	public Task UpdateBook(uint id, string title, int publishedYear, uint authorId);
	public Task RemoveBook(uint id);
}
