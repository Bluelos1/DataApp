using Amazon.S3;
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
        private readonly IAmazonS3 _awsClients;

        public S3Operations(IAmazonS3 awsClients)
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
            await _awsClients.PutObjectAsync(request);
        }

        public async Task DownloadFileAsync(string bucketName, string keyName, string downloadPath)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };
            using GetObjectResponse response = await _awsClients.GetObjectAsync(request);
            using Stream responseStream = response.ResponseStream;
            using var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write);
            await responseStream.CopyToAsync(fileStream);
        }
    }
}
