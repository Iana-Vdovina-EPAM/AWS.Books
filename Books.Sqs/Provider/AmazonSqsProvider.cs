using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Sqs.Provider
{
	internal class AmazonSqsProvider : IAmazonSqsProvider
	{
		private readonly IAmazonSQS _amazonService;
		private const string SQS_NAME = "booksSQS";

		public AmazonSqsProvider(IAmazonSQS amazonService)
		{
			_amazonService = amazonService;
		}

		public async Task SendBookMessage(AmazonSqsBookMessage message)
		{
			await _amazonService.GetQueueUrlAsync(SQS_NAME).ContinueWith(async (t1) =>
			{
				if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled)
				{
					await SendMessageInternal(message, t1.Result.QueueUrl);
				}
			});
		}

		private async Task SendMessageInternal(AmazonSqsBookMessage message, string url)
		{
			var request = new SendMessageRequest();
			request.QueueUrl = url;
			request.MessageBody = JsonSerializer.Serialize(message);
			await _amazonService.SendMessageAsync(request);
		}
	}
}
