using Microsoft.EntityFrameworkCore;
using LibraryApp.DbContext;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController() : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<BookListDto>> GetAll(int fromYear = int.MinValue, int toYear = int.MaxValue)
	{
		var context = new LibraryContext();
		return await (
			from book in context.Books
			where book.PublishedYear >= fromYear && book.PublishedYear <= toYear
			join author in context.Authors
				on book.Author.Id equals author.Id
			select new BookListDto {
				Id = book.Id,
				Title = book.Title,
				PublishedYear = book.PublishedYear,
				Author = author.Name
			}).ToListAsync();
	}

	[HttpGet("{id}")]
	public Book? Get(int id)
	{
		using var context = new LibraryContext();
		return context.Books.Find(id);
	}

	[HttpPost]
	public string? Post(string title, int publishedYear, int authorId)
	{
		using var context = new LibraryContext();
		var authorEntiry = context.Authors.Find(authorId);
		if (authorEntiry is null) {
			return null;
		}
		_ = context.Books.Add(new Book {
			Title = title,
			PublishedYear = publishedYear,
			Author = authorEntiry
		});
		_ = context.SaveChanges();
		return "Added a new book\n";
	}

	[HttpPut]
	public string? Put(int id, string title, int publishedYear, int authorId)
	{
		using var context = new LibraryContext();
		var bookEntity = context.Books.Find(id);
		if (bookEntity is null) {
			return null;
		}

		var authorEntiry = context.Authors.Find(authorId);
		if (authorEntiry is null) {
			return null;
		}

		bookEntity.Title = title;
		bookEntity.PublishedYear = publishedYear;
		bookEntity.Author = authorEntiry;

		_ = context.Books.Update(bookEntity);
		_ = context.SaveChanges();
		return "Updated the author\n";
	}

	[HttpDelete("{id}")]
	public string Delete(int id)
	{
		using var context = new LibraryContext();
		_ = context.Books.Remove(new Book { Id = id });
		_ = context.SaveChanges();
		return "Deleted the book\n";
	}
}
