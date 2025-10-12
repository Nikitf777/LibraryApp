using LibraryApp.Exceptions;
using LibraryApp.Models;
using LibraryApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IBasicRepository<Author> repository) : ControllerBase
{
	private readonly IBasicRepository<Author> repository = repository;

	[HttpGet]
	public IEnumerable<Author> GetAll()
	{
		return this.repository.GetAll();
	}

	[HttpGet("{id}")]
	public Author Get(int id)
	{
		return this.repository.Get(id);
	}

	[HttpPost]
	public string Post(string name, long dateOfBirth)
	{
		var id = this.repository.Create(new Author(name, DateTimeOffset.FromUnixTimeSeconds(dateOfBirth).DateTime));
		return $"Added a new Author with id {id}\n";
	}

	[HttpPut]
	public ActionResult<string> Put(string name, long dateOfBirth)
	{
		this.repository.Update(new Author(name, DateTimeOffset.FromUnixTimeSeconds(dateOfBirth).DateTime));
		return "Updated the author\n";
	}

	[HttpDelete("{id}")]
	public string Delete(int id)
	{
		try {
			this.repository.Delete(id);
			return "Deleted the author\n";
		} catch (NotFoundException e) {
			return e.Message;
		}
	}
}
