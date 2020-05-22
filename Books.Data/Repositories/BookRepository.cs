using Amazon.DynamoDBv2.DataModel;
using Books.API.Database;
using Books.Contracts;
using Books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepositoryBook = Books.Data.Models.Book;

namespace Books.Data.Repositories
{
	internal class BookRepository : IBookRepository
	{
		private readonly IDynamoDBContext _dbContext;

		public BookRepository(IDynamoDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Create(Book book)
		{
			RepositoryBook dalBook = ConvertToRepositoryBook(book);

			//Save an book object
			await _dbContext.SaveAsync<RepositoryBook>(dalBook);
		}

		public async Task Delete(string isbn)
		{
			await _dbContext.DeleteAsync<RepositoryBook>(isbn);
		}

		public async Task<Book> Get(string isbn)
		{
			var book = await _dbContext.LoadAsync<RepositoryBook>(isbn);
			if (book == null)
			{
				throw new NotFoundException(isbn);
			}
			return ConvertToBook(book);
		}

		public async Task<IEnumerable<Book>> Get()
		{
			var conditions = new List<ScanCondition>();
			// you can add scan conditions, or leave empty
			var books = await _dbContext.ScanAsync<RepositoryBook>(conditions).GetRemainingAsync();

			return books.Select(book => ConvertToBook(book)).ToList();
		}

		public async Task Update(Book book)
		{
			await _dbContext.SaveAsync<Book>(book);
		}

		private RepositoryBook ConvertToRepositoryBook(Book book)
		{
			return new RepositoryBook()
			{
				Title = book.Title,
				ISBN = book.ISBN,
				Description = book.Description,
				LastUpdated = DateTime.Now
			};
		}

		private Book ConvertToBook(RepositoryBook book)
		{
			return new Book()
			{
				ISBN = book.ISBN,
				Title = book.Title,
				Description = book.Description
			};
		}

		public bool Exists(string isbn)
		{
			try
			{
				var book = Get(isbn);

				return book != null;
			}
			catch (NotFoundException)
			{
				return false;
			}
		}
	}
}
