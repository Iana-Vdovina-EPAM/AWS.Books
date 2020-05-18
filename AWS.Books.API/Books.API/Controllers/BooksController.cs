using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private List<Book> _books = new List<Book>() {
			new Book("isbn1", "title1", "description1"),
			new Book("isbn2", "title2", "description1"),
		};

		// GET: api/Books
		[HttpGet]
		public IEnumerable<Book> Get()
		{
			return _books;
		}

		// GET: api/Books/5
		[HttpGet("{isbn}", Name = "Get")]
		public Book Get(string isbn)
		{
			return _books.FirstOrDefault(i => i.ISBN == isbn);
		}

		// POST: api/Books
		[HttpPost]
		public void Post([FromBody] Book value)
		{
		}

		// PUT: api/Books/5
		[HttpPut("{isbn}")]
		public void Put(string isbn, [FromBody] Book value)
		{
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{isbn}")]
		public void Delete(string isbn)
		{
		}
	}
}
