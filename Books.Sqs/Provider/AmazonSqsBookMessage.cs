using Books.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Sqs.Provider
{
	public class AmazonSqsBookMessage
	{
		public Book Book { get; set; }
		public int OperationId { get; set; }
	}
}
