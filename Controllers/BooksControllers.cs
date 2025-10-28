using LibraryApp.Models;
using LibraryApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
	private readonly IBookService bookService = bookService;
	[HttpGet]
	public async Task<IEnumerable<BookListDto>> GetAll(int fromYear = int.MinValue, int toYear = int.MaxValue)
	{
		return await this.bookService.SearchForBooks(fromYear, toYear);
	}

	[HttpGet("{id}")]
	public async Task<Book?> Get(uint id)
	{
		return await this.bookService.RetriveSpecificBookDetails(id);
	}

	[HttpPost]
	public async Task<string?> Post(string title, int publishedYear, uint authorId)
	{
		await this.bookService.CreateBook(title, publishedYear, authorId);
		return "Added a new book\n";
	}

	[HttpPut]
	public async Task<string?> Put(uint id, string title, int publishedYear, uint authorId)
	{
		await this.bookService.ModifyBook(id, title, publishedYear, authorId);
		return "Updated the author\n";
	}

	[HttpDelete("{id}")]
	public async Task<string> Delete(uint id)
	{
		await this.bookService.RemoveBook(id);
		return "Deleted the book\n";
	}
}
