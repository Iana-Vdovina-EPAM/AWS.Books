using Books.API.Database;
using Books.BusinessLogic.Contracts;
using Books.Models;
using Books.Sqs.Provider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.BusinessLogic
{
	public class BookSevice : IBookSevice
	{
		private readonly IBookRepository _bookRepository;
		private readonly IAmazonSqsProvider _sqsProvider;

		public BookSevice(IBookRepository bookRepository, IAmazonSqsProvider sqsProvider)
		{
			_bookRepository = bookRepository;
			_sqsProvider = sqsProvider;
		}

		public async Task CreateBook(Book book)
		{
			await _bookRepository.CreateBook(book).ContinueWith(async (t1) =>
			{
				if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
				{
					await _sqsProvider.SendBookMessage(new AmazonSqsBookMessage()
					{
						Book = book,
						OperationId = (int)Operations.Add
					});
				}
			});
		}

		public async Task DeleteBook(string isbn)
		{
			await _bookRepository.DeleteBook(isbn);
		}

		public async Task<Book> GetBook(string isbn)
		{
			return await _bookRepository.GetBook(isbn);
		}

		public async Task<IEnumerable<Book>> GetBooks()
		{
			return await _bookRepository.GetBooks();
		}

		public async Task UpdateBook(Book book)
		{
			await _bookRepository.UpdateBook(book);
		}
	}
}
