using Books.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Database
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> Get();
		Task<Book> Get(string isbn);
		bool Exists(string isbn);
		Task Create(Book book);
		Task Update(Book book);
		Task Delete(string isbn);
	}
}
