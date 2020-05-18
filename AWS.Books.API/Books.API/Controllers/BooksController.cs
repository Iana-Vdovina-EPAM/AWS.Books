using System.Threading.Tasks;
using Books.API.Database;
using Books.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookDal _bookDal;

		public BooksController(IBookDal bookDal)
		{
			_bookDal = bookDal;
		}

		// GET: api/Books/5
		[HttpGet("{isbn}", Name = "Get")]
		public async Task<Book> Get(string isbn)
		{
			return await _bookDal.GetBook(isbn);
		}

		// POST: api/Books
		[HttpPost]
		public async Task Post(Book value)
		{
			await _bookDal.CreateBook(value);
		}

		// PUT: api/Books/5
		[HttpPut("{isbn}")]
		public async Task Put(string isbn, [FromBody] Book value)
		{
			value.ISBN = isbn;
			await _bookDal.UpdateBook(value);
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{isbn}")]
		public async Task Delete(string isbn)
		{
			await _bookDal.DeleteBook(isbn);
		}
	}
}
