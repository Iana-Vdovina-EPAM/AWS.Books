using Books.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.BusinessLogic.Contracts
{
	public interface IBookSevice
	{
		Task<IEnumerable<Book>> GetBooks();
		Task<Book> GetBook(string isbn);
		Task CreateBook(Book book);
		Task UpdateBook(string isbn, Book book);
		Task DeleteBook(string isbn);
	}
}
