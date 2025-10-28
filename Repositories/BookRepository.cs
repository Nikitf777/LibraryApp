using LibraryApp.DbContext;
using LibraryApp.Exceptions;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories;

public class BookRepository(LibraryContext context) : IBookRepository
{
	private readonly LibraryContext context = context;
	public async Task<IEnumerable<BookListDto>> FetchBooks(int fromYear = int.MinValue, int toYear = int.MaxValue)
	{
		return await (
			from book in this.context.Books
			where book.PublishedYear >= fromYear && book.PublishedYear <= toYear
			join author in this.context.Authors
				on book.Author.Id equals author.Id
			select new BookListDto {
				Id = book.Id,
				Title = book.Title,
				PublishedYear = book.PublishedYear,
				Author = author.Name
			}).ToListAsync();
	}

	public async Task<BookDetailsDto> FetchSpecificBook(uint id)
	{
		return await (
			from book in this.context.Books
			where book.Id == id
			join author in this.context.Authors
				on book.Author.Id equals author.Id
			select new BookDetailsDto(book) {
				Author = new AuthorDto(author)
			}
		).FirstAsync();
	}

	public async Task InsertBook(string title, int publishedYear, uint authorId)
	{
		var authorEntiry = await this.context.Authors.FindAsync(authorId) ?? throw new NotFoundException($"Cound not found an author from the provided foreign key {authorId}");

		_ = this.context.Books.Add(new Book {
			Title = title,
			PublishedYear = publishedYear,
			Author = authorEntiry
		});
		_ = await this.context.SaveChangesAsync();
	}

	public async Task UpdateBook(uint id, string title, int publishedYear, uint authorId)
	{
		var bookEntity = await this.context.Books.FindAsync(id) ?? throw new NotFoundException($"Could not update a non-existing book with id {id}");

		var authorEntiry = await this.context.Authors.FindAsync(authorId) ?? throw new NotFoundException($"Cound not found an author from the provided foreign key {authorId}");

		bookEntity.Title = title;
		bookEntity.PublishedYear = publishedYear;
		bookEntity.Author = authorEntiry;

		_ = this.context.Books.Update(bookEntity);
		_ = await this.context.SaveChangesAsync();
	}

	public async Task RemoveBook(uint id)
	{
		var bookEntity = await this.context.Books.FindAsync(id) ?? throw new NotFoundException($"Could not remove a non-existing book with id {id}");
		_ = this.context.Books.Remove(new Book { Id = id });
		_ = this.context.SaveChangesAsync();
	}
}
