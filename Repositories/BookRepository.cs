using LibraryApp.DbContext;
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

	public async Task<Book?> FetchSpecificBook(uint id)
	{
		using var context = new LibraryContext();
		return context.Books.Find(id);
	}

	public async Task InsertBook(string title, int publishedYear, uint authorId)
	{
		using var context = new LibraryContext();
		var authorEntiry = context.Authors.Find(authorId);
		if (authorEntiry is null) {
			return;
		}
		_ = context.Books.Add(new Book {
			Title = title,
			PublishedYear = publishedYear,
			Author = authorEntiry
		});
		_ = context.SaveChanges();
	}

	public async Task UpdateBook(uint id, string title, int publishedYear, uint authorId)
	{
		using var context = new LibraryContext();
		var bookEntity = context.Books.Find(id);
		if (bookEntity is null) {
			return;
		}

		var authorEntiry = context.Authors.Find(authorId);
		if (authorEntiry is null) {
			return;
		}

		bookEntity.Title = title;
		bookEntity.PublishedYear = publishedYear;
		bookEntity.Author = authorEntiry;

		_ = context.Books.Update(bookEntity);
		_ = context.SaveChanges();
	}

	public async Task RemoveBook(uint id)
	{
		using var context = new LibraryContext();
		_ = context.Books.Remove(new Book { Id = id });
		_ = context.SaveChanges();
	}
}
