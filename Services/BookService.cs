using LibraryApp.Models;
using LibraryApp.Repositories;

namespace LibraryApp.Services;

public class BookService(IBookRepository bookRepository) : IBookService
{
	private readonly IBookRepository bookRepository = bookRepository;
	public async Task<IEnumerable<BookListDto>> SearchForBooks(int fromYear = int.MinValue, int toYear = int.MaxValue)
	{
		return await this.bookRepository.FetchBooks(fromYear, toYear);
	}

	public async Task<BookDetailsDto> RetriveSpecificBookDetails(uint id)
	{
		return await this.bookRepository.FetchSpecificBook(id);
	}

	public async Task CreateBook(string title, int publishedYear, uint authorId)
	{
		await this.bookRepository.InsertBook(title, publishedYear, authorId);
	}

	public async Task ModifyBook(uint id, string title, int publishedYear, uint authorId)
	{
		await this.bookRepository.UpdateBook(id, title, publishedYear, authorId);
	}

	public async Task RemoveBook(uint id)
	{
		await this.bookRepository.RemoveBook(id);
	}
}
