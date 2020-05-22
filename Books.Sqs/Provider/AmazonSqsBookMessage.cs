using Books.Contracts;

namespace Books.Sqs.Provider
{
	public class AmazonSqsBookMessage
	{
		public Book Book { get; set; }
		public Operation Operation { get; set; }
	}
}
