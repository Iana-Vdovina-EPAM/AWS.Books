using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Books.API.Models
{
	[DynamoDBTable("book")]
	public class Book
	{
		[DynamoDBHashKey]
		public string ISBN { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public Book()
		{

		}

		public Book(string isbn, string title, string description)
		{
			ISBN = isbn;
			Title = title;
			Description = description;
		}
	}
}
