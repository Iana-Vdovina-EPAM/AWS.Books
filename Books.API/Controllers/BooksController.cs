using System.Threading.Tasks;
using Books.BusinessLogic.Contracts;
using Books.Contracts;
using Books.Exceptions;
using Microsoft.AspNetCore.Http;
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Book>> Get(string isbn)
		{
			try
			{
				return await _bookService.GetBook(isbn);
			}
			catch (NotFoundException e)
			{
				return BadRequest(e);
			}
		}

		// POST: api/Books
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Post([FromBody]Book value)
		{
			try
			{
				if (value == null)
				{
					return BadRequest("Input book cannot null");
				}

				await _bookService.CreateBook(value);

				return CreatedAtAction(nameof(Get), value);
			}
			catch (ValidationException e)
			{
				return BadRequest(e.Message);
			}
		}

		// PUT: api/Books/5
		[HttpPut("{isbn}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Put(string isbn, [FromBody] Book value)
		{
			try
			{
				await _bookService.UpdateBook(isbn, value);
				return Ok();
			}
			catch (ValidationException e)
			{
				return BadRequest(e.Message);
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{isbn}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Delete(string isbn)
		{
			try
			{
				await _bookService.DeleteBook(isbn);
				return Ok();
			}
			catch (NotFoundException e)
			{
				return BadRequest(e);
			}
		}
	}
}
