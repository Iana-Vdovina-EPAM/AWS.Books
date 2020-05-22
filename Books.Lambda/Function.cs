using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Transfer;
using Books.Contracts;
using Books.Lambda.Models;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Books.Lambda
{
	public class Function
	{
		private static IAmazonS3 s3Client;

		private const string bucketName = "books-bucket-vdovina";
		// Specify your bucket region (an example region is shown).
		private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;

		/// <summary>
		/// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
		/// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
		/// region the Lambda function is executed in.
		/// </summary>
		public Function()
		{
			s3Client = new AmazonS3Client(bucketRegion);
		}

		/// <summary>
		/// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
		/// to respond to SQS messages.
		/// </summary>
		/// <param name="evnt"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
		{
			foreach (var message in evnt.Records)
			{
				await ProcessMessageAsync(message, context);
			}
		}

		private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
		{
			context.Logger.LogLine($"Processed message {message.Body}");

			var body = JsonSerializer.Deserialize<Message>(message.Body);
			switch (body.Operation)
			{
				case Operation.Add:
					await UploadFileAsync(body.Book);
					break;
				case Operation.Remove:
					await DeleteObjectNonVersionedBucketAsync(body.Book.ISBN);
					break;
				default:
					break;
			}
		}

		private async Task UploadFileAsync(Book book)
		{
			try
			{
				var fileTransferUtility = new TransferUtility(s3Client);				

				byte[] bytes = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(book));
				using (MemoryStream stream = new MemoryStream())
				{
					stream.Write(bytes);
					await fileTransferUtility.UploadAsync(stream, bucketName, book.ISBN);
				}
				Console.WriteLine("Upload 1 completed");
			}
			catch (AmazonS3Exception e)
			{
				Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
			}

		}

		private async Task DeleteObjectNonVersionedBucketAsync(string name)
		{
			try
			{
				Console.WriteLine("Deleting an object");
				await s3Client.DeleteObjectAsync(bucketName, name);
			}
			catch (AmazonS3Exception e)
			{
				Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
			}
		}
	}
}
