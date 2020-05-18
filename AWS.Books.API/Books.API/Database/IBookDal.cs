using Books.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Database
{
	public interface IBookDal
	{
		Task<Book> GetBook(string isbn);
		Task CreateBook(Book book);
		Task UpdateBook(Book book);

		Task DeleteBook(string isbn);
	}
}
