using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Database.DynamoContext
{
	public class DBContext : DynamoDBContext
	{
		private const string accessKey = "AKIAQAH7TLETXZOSOAYO";
		private const string secretkey = "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";

		public DBContext() : base(GetClient())
		{

		}

		private static AmazonDynamoDBClient GetClient()
		{
			var credentials = new BasicAWSCredentials(accessKey, secretkey);
			return new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);
		}
	}
}
