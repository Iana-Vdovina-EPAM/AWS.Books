using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Queue
{
	public class AmazonSqs
	{
		//sqs-user
		private const string accessKey = "AKIAQAH7TLETR7OR5WJ7";
		private const string secretkey = "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";
		private readonly AmazonSQSClient _client;

		//public static void CreateQueue() {
		//	var sqsConfig = new AmazonSQSConfig();

		//	sqsConfig.ServiceURL = "http://sqs.us-west-2.amazonaws.com";

		//	var sqsClient = new AmazonSQSClient(sqsConfig);


		//	var createQueueRequest = new CreateQueueRequest();

		//	createQueueRequest.QueueName = "booksSQS";
		//	var attrs = new Dictionary<string, string>();
		//	attrs.Add(QueueAttributeName.VisibilityTimeout, "10");
		//	createQueueRequest.Attributes = attrs;

		//	var createQueueResponse = sqsClient.CreateQueueAsync(createQueueRequest);

		//}

		public AmazonSqs(AmazonSQSClient client)
		{
			_client = client;
		}

		public static async Task<string> GetUrl()
		{

			var sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = "http://sqs.us-east-2.amazonaws.com";

			var sqsClient = new AmazonSQSClient(sqsConfig);


			//var createQueueRequest = new CreateQueueRequest();

			//createQueueRequest.QueueName = "booksSQS";
			//var attrs = new Dictionary<string, string>();
			//attrs.Add(QueueAttributeName.VisibilityTimeout, "10");
			//createQueueRequest.Attributes = attrs;

			//var client = new AmazonSQSClient();

			var request = new GetQueueUrlRequest
			{
				QueueName = "booksSQS",
				QueueOwnerAWSAccountId = "000536041767"
			};
			var response = await sqsClient.GetQueueUrlAsync(request);
			return response.QueueUrl;

		}

		public static async Task Send()
		{
			var sqsConfig = new AmazonSQSConfig();

			sqsConfig.ServiceURL = "http://sqs.us-east-2.amazonaws.com";


			//var sqsClient = new AmazonSQSClient(accessKey, secretkey, RegionEndpoint.USEast2);
			var request = new SendMessageRequest();

			string url = "https://sqs.us-east-2.amazonaws.com/000536041767/booksSQS";

			request.QueueUrl = url;
			request.MessageBody = "{isbn:sqsTest1, title:sqsTestTitle2}";
			//_client.SendMessageAsync(request);
			//await sqsClient.SendMessageAsync(request);
		}
	}
}
