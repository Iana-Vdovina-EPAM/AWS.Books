using Books.API.Database.DynamoContext;
using Books.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.API.Database
{
	public class BookDal : IBookDal
	{
		private readonly DBContext _dbContext;

		public BookDal(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task CreateBook(Book book)
		{
			//Save an book object
			await _dbContext.SaveAsync<Book>(book);
		}

		public async Task DeleteBook(string isbn)
		{
			await _dbContext.DeleteAsync<Book>(isbn);
		}

		public async Task<Book> GetBook(string isbn)
		{
			return await _dbContext.LoadAsync<Book>(isbn);
		}

		public async Task UpdateBook(Book book)
		{
			await _dbContext.SaveAsync<Book>(book);
		}
	}
}
