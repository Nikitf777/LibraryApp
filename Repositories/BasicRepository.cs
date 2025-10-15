#pragma warning disable CA1716 // Identifiers should not match keywords

using LibraryApp.Exceptions;
using LibraryApp.Models;

namespace LibraryApp.Repositories;

public interface IBasicRepository<TModel>
{
	public IEnumerable<TModel> GetAll();
	public TModel Get(int id);
	public int Create(TModel author);
	public void Update(TModel newAuthor);
	public void Delete(int id);
}

public class BasicRepository<TModel> : IBasicRepository<TModel> where TModel : IKeyedEntity
{
	private readonly List<TModel> data = [];
	private int nextId;

	public virtual IEnumerable<TModel> GetAll()
	{
		var authors = new TModel[this.data.Count];
		this.data.CopyTo(authors);
		return authors;
	}

	public virtual TModel Get(int id)
	{
		return this.data.Find((entry) => entry.Id == id) ?? throw new NotFoundException($"{nameof(TModel)} with id {id} was not found.");
	}

	public virtual int Create(TModel entry)
	{
		entry.Id = this.nextId;
		this.data.Add(entry);
		return this.nextId++;
	}

	public virtual void Update(TModel entry)
	{
		try {
			this.data[this.data.FindIndex((author) => author.Id == entry.Id)] = entry;
		} catch (IndexOutOfRangeException) {
			throw new NotFoundException($"{typeof(TModel).Name} with id {entry.Id} was not found.");
		}
	}

	public virtual void Delete(int id)
	{
		try {
			this.data.RemoveAt(this.data.FindIndex((author) => author.Id == id));
		} catch (ArgumentOutOfRangeException) {
			throw new NotFoundException($"{typeof(TModel).Name} with id {id} was not found.");
		}
	}
}
