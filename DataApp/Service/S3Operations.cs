using Amazon.S3.Model;
using DataApp.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Service
{
    public class S3Operations
    {
        private readonly AwsClients _awsClients;

        public S3Operations(AwsClients awsClients)
        {
            _awsClients = awsClients;
        }

        public async Task UploadFileAsync(string bucketName, string keyName, Stream fileStream)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                InputStream = fileStream
            };
            await _awsClients.S3Client.PutObjectAsync(request);
        }

        public async Task<GetObjectResponse> DownloadFileAsync(string bucketName, string keyName)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };
            return await _awsClients.S3Client.GetObjectAsync(request);
        }
    }
}
