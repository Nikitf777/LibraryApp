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
		).FirstOrDefaultAsync() ?? throw new NotFoundException($"Could not fetch a non-existing book with id {id}");
	}

	public async Task InsertBook(string title, int publishedYear, uint authorId)
	{
		await this.EnsureAuthorExists(authorId);

		_ = this.context.Books.Add(new Book {
			Title = title,
			PublishedYear = publishedYear,
			AuthorId = authorId
		});
		_ = await this.context.SaveChangesAsync();
	}

	public async Task UpdateBook(uint id, string title, int publishedYear, uint authorId)
	{
		await this.EnsureAuthorExists(authorId);

		if (await this.context.Books.ExecuteUpdateAsync(
			setters => setters
				.SetProperty(book => book.Title, title)
				.SetProperty(book => book.PublishedYear, publishedYear)
				.SetProperty(book => book.AuthorId, authorId)
		) == 0) {
			throw new NotFoundException($"Could not update a non-existing book with id {id}");
		}
	}

	public async Task RemoveBook(uint id)
	{
		if (await this.context.Books.Where(book => book.Id == id).ExecuteDeleteAsync() == 0) {
			throw new NotFoundException($"Could not remove a non-existing book with id {id}");
		}
	}

	private async Task EnsureAuthorExists(uint authorId)
	{
		if (!await this.context.Authors.Where(author => author.Id == authorId).AnyAsync()) {
			throw new NotFoundException($"Cound not found an author by the provided foreign key {authorId}");
		}
	}
}
