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

        public AwsClients()
        {
            var credentials = new BasicAWSCredentials(AwsConfig.AccessKey, AwsConfig.SecretKey);
            var region = RegionEndpoint.GetBySystemName(AwsConfig.Region);

            DynamoDBClient = new AmazonDynamoDBClient(credentials, region);
            S3Client = new AmazonS3Client(credentials, region);
        }
    }
}
