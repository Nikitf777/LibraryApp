using LibraryApp.DbContext;
using LibraryApp.Exceptions;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories;

public class BookRepository : IBookRepository
{
	public async Task<IEnumerable<BookListDto>> FetchBooks(int fromYear = int.MinValue, int toYear = int.MaxValue)
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

	public async Task<Book> FetchSpecificBook(uint id)
	{
		using var context = new LibraryContext();
		return await context.Books.FindAsync(id) ?? throw new NotFoundException($"Could not fetch a non-existing book with id {id}");
	}

	public async Task InsertBook(string title, int publishedYear, uint authorId)
	{
		using var context = new LibraryContext();
		var authorEntiry = await context.Authors.FindAsync(authorId) ?? throw new NotFoundException($"Cound not found an author from the provided foreign key {authorId}");

		_ = context.Books.Add(new Book {
			Title = title,
			PublishedYear = publishedYear,
			Author = authorEntiry
		});
		_ = await context.SaveChangesAsync();
	}

	public async Task UpdateBook(uint id, string title, int publishedYear, uint authorId)
	{
		using var context = new LibraryContext();
		var bookEntity = await context.Books.FindAsync(id) ?? throw new NotFoundException($"Could not update a non-existing book with id {id}");

		var authorEntiry = await context.Authors.FindAsync(authorId) ?? throw new NotFoundException($"Cound not found an author from the provided foreign key {authorId}");

		bookEntity.Title = title;
		bookEntity.PublishedYear = publishedYear;
		bookEntity.Author = authorEntiry;

		_ = context.Books.Update(bookEntity);
		_ = await context.SaveChangesAsync();
	}

	public async Task RemoveBook(uint id)
	{
		using var context = new LibraryContext();
		var bookEntity = await context.Books.FindAsync(id) ?? throw new NotFoundException($"Could not remove a non-existing book with id {id}");
		_ = context.Books.Remove(new Book { Id = id });
		_ = context.SaveChangesAsync();
	}
}
