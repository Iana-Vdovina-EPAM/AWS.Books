using Books.Models;
using System;
using System.Threading.Tasks;

namespace Books.Sqs.Provider
{
	public interface IAmazonSqsProvider
	{
		Task SendBookMessage(AmazonSqsBookMessage message);
	}
}
