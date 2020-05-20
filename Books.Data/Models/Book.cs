using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Books.Data.Models
{
	[DynamoDBTable("book")]
	internal class Book
	{
		[DynamoDBHashKey]
		public string ISBN { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
