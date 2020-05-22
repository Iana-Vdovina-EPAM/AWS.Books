using Books.API.Database;
using Books.BusinessLogic.Contracts;
using Books.Contracts;
using Books.Exceptions;
using Books.Sqs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Books.BusinessLogic
{
	public class BookSevice : IBookSevice
	{
		private readonly IBookRepository _bookRepository;
		private readonly IAmazonSqsProvider _sqsProvider;
		private readonly Regex _regex;

		public BookSevice(IBookRepository bookRepository, IAmazonSqsProvider sqsProvider)
		{
			_bookRepository = bookRepository;
			_sqsProvider = sqsProvider;
			_regex = new Regex("[0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*[-| ][0-9]*");
		}

		public async Task CreateBook(Book book)
		{
			ValidateBook(book);
			await _bookRepository
				.Create(book)
				.ContinueWith(async (t1) =>
			{
				if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
				{
					await _sqsProvider.SendBookMessage(new AmazonSqsBookMessage()
					{
						Book = book,
						Operation = Operation.Add
					});
				}
			});
		}

		private void ValidateBook(Book book)
		{
			if (book == null)
			{
				throw new ArgumentNullException(nameof(book));
			}

			List<string> errors = new List<string>();

			if (!_regex.IsMatch(book.ISBN))
			{
				errors.Add("ISBN doesn't match the expression");
			}
			if (string.IsNullOrEmpty(book.Title))
			{
				errors.Add("Title cannot be null or empty");
			}

			if (errors.Any())
			{
				throw new ValidationException(errors, nameof(book));
			}
		}

		public async Task DeleteBook(string isbn)
		{
			await _bookRepository
				.Delete(isbn)
				.ContinueWith(async (t1) =>
			{
				if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
				{
					await _sqsProvider.SendBookMessage(new AmazonSqsBookMessage()
					{
						Book = new Book() { ISBN = isbn },
						Operation = Operation.Remove
					});
				}
			});
		}

		public async Task<Book> GetBook(string isbn)
		{
			return await _bookRepository.Get(isbn);
		}

		public async Task<IEnumerable<Book>> GetBooks()
		{
			return await _bookRepository.Get();
		}

		public async Task UpdateBook(string isbn, Book book)
		{
			ValidateBook(book);
			if (_bookRepository.Exists(isbn))
			{
				await _bookRepository.Update(book);
			}
		}
	}
}
