using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.S3;
using DataApp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Clients
{
    public class AwsClients
    {
        public AmazonDynamoDBClient DynamoDBClient { get; }
        public AmazonS3Client S3Client { get; }

        public AwsClients(AwsConfig awsConfig)
        {
            var credentials = new BasicAWSCredentials(awsConfig.AccessKeyId, awsConfig.SecretAccessKey);
            var region = RegionEndpoint.GetBySystemName(awsConfig.Region);
            var s3Config = new AmazonS3Config
            {
                ServiceURL = awsConfig.ServiceURL,
                ForcePathStyle = true 
            };
            var dynamoDbConfig = new AmazonDynamoDBConfig
            {
                ServiceURL = awsConfig.ServiceURL,

            };


            DynamoDBClient = new AmazonDynamoDBClient(credentials, dynamoDbConfig);
            S3Client = new AmazonS3Client(credentials, s3Config);
        }
    }
}
