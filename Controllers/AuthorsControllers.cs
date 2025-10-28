using LibraryApp.Models;
using LibraryApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
	private readonly IAuthorService authorService = authorService;
	[HttpGet]
	public async Task<IEnumerable<AuthorDto>> GetAll(string nameFilter = "")
	{
		return await this.authorService.SearchForAuthors(nameFilter);
	}

	[HttpGet("{id}")]
	public async Task<Author?> Get(uint id)
	{
		return await this.authorService.RetriveSpecificAuthorDetails(id);
	}

	[HttpPost]
	public async Task<string> Post(string name, DateTime dateOfBirth)
	{
		await this.authorService.CreateAuthor(name, dateOfBirth);
		return $"Added a new Author\n";
	}

	[HttpPut]
	public async Task<string?> Put(uint id, string name, DateTime dateOfBirth)
	{
		await this.authorService.ModifyAuthor(id, name, dateOfBirth);
		return "Updated the author\n";
	}

	[HttpDelete("{id}")]
	public async Task<string> DeleteAsync(uint id)
	{
		await this.authorService.RemoveAuthor(id);
		return "Deleted the author\n";
	}
}
