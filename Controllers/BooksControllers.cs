using LibraryApp.Exceptions;
using LibraryApp.Models;
using LibraryApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBasicRepository<Author> authorsRepo, IBasicRepository<Book> booksRepo) : ControllerBase
{
	private readonly IBasicRepository<Author> authorsRepo = authorsRepo;
	private readonly IBasicRepository<Book> booksRepo = booksRepo;

	[HttpGet]
	public IEnumerable<Book> GetAll()
	{
		return this.booksRepo.GetAll();
	}

	[HttpGet("{id}")]
	public Book Get(int id)
	{
		return this.booksRepo.Get(id);
	}

	[HttpPost]
	public string Post(string title, int publishedYear, int authorId)
	{
		try {
			_ = this.authorsRepo.Get(authorId);
			var id = this.booksRepo.Create(new Book(title, publishedYear, authorId));
			return $"Added a new book with id {id}\n";
		} catch (NotFoundException e) {
			return e.Message;
		}
	}

	[HttpPut]
	public string Put(string title, int publishedYear, int authorId)
	{
		try {
			this.booksRepo.Update(new Book(title, publishedYear, authorId));
			return "Updated the book\n";
		} catch (NotFoundException e) {
			return e.Message;
		}
	}

	[HttpDelete("{id}")]
	public string Delete(int id)
	{
		try {
			this.booksRepo.Delete(id);
			return "Deleted the book\n";
		} catch (NotFoundException e) {
			return e.Message;
		}
	}
}
