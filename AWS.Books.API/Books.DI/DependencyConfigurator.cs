using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Amazon.SQS;
using Books.API.Database;
using Books.BusinessLogic;
using Books.BusinessLogic.Contracts;
using Books.Data.Repositories;
using Books.Sqs.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Books.DI
{
	public static class DependencyConfigurator
	{

		public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddDefaultAWSOptions(configuration.GetAWSOptions());
			serviceCollection.AddAWSService<IAmazonS3>();
			serviceCollection.AddAWSService<IAmazonDynamoDB>();
			serviceCollection.AddAWSService<IAmazonSQS>();

			serviceCollection.AddScoped<IBookSevice, BookSevice>();

			serviceCollection.AddSingleton<IDynamoDBContext, DynamoDBContext>();
			serviceCollection.AddScoped<IBookRepository, BookRepository>();

			serviceCollection.AddSingleton<IAmazonSqsProvider, AmazonSqsProvider>();

			return serviceCollection;
		}		
	}
}
