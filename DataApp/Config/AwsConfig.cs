using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Config
{
    internal class AwsConfig
    {
        public static string AccessKey { get; }
        public static string SecretKey { get;}
        public static string Region { get;}
    }
}
