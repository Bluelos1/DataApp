using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Config
{
    public class AwsConfig
    {
        public  string AccessKeyId { get; set; }
        public  string SecretAccessKey { get; set; }
        public  string Region { get; set; }
        public  string ServiceURL { get; set; }
    }
}
