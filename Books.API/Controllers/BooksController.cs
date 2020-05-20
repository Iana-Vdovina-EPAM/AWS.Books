using System;
using System.Threading.Tasks;
using Books.API.Database;
using Books.BusinessLogic.Contracts;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookSevice _bookService;

		public BooksController(IBookSevice bookService)
		{
			_bookService = bookService;
		}

		// GET: api/Books/5
		[HttpGet("{isbn}", Name = "Get")]
		public async Task<ActionResult<Book>> Get(string isbn)
		{
			try
			{
				return await _bookService.GetBook(isbn);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
			
		}

		// POST: api/Books
		[HttpPost]
		public async Task Post(Book value)
		{
			await _bookService.CreateBook(value);
		}

		// PUT: api/Books/5
		[HttpPut("{isbn}")]
		public async Task Put(string isbn, [FromBody] Book value)
		{
			value.ISBN = isbn;
			await _bookService.UpdateBook(value);
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{isbn}")]
		public async Task Delete(string isbn)
		{
			await _bookService.DeleteBook(isbn);
		}
	}
}
