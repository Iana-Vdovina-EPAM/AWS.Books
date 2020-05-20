using Amazon.DynamoDBv2.DataModel;
using Books.API.Database;
using Books.Models;
using System;
using System.Collections.Generic;
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

		public async Task CreateBook(Book book)
		{
			RepositoryBook dalBook = new RepositoryBook()
			{
				Title = book.Title,
				ISBN = book.ISBN,
				Description = book.Description,
				LastUpdated = DateTime.Now
			};

			//Save an book object
			await _dbContext.SaveAsync<RepositoryBook>(dalBook);
		}

		public async Task DeleteBook(string isbn)
		{
			await _dbContext.DeleteAsync<RepositoryBook>(isbn);
		}

		public async Task<Book> GetBook(string isbn)
		{
			var book = await _dbContext.LoadAsync<RepositoryBook>(isbn);
			return ConvertToBook(book);
		}

		public Task<IEnumerable<Book>> GetBooks()
		{
			throw new NotImplementedException();
		}

		public async Task UpdateBook(Book book)
		{
			await _dbContext.SaveAsync<Book>(book);
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
	}
}
