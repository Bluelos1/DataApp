using Amazon.DynamoDBv2.Model;
using DataApp.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Service
{
    public class DynamoDbOperations
    {
        private readonly AwsClients _awsClients;

        public DynamoDbOperations(AwsClients awsClients)
        {
            _awsClients = awsClients;
        }

        public async Task AddItemAsync(string tableName, Dictionary<string, AttributeValue> item)
        {
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = item
            };
            await _awsClients.DynamoDBClient.PutItemAsync(request);
        }
        public async Task<Dictionary<string, AttributeValue>> GetItemAsync(string tableName, Dictionary<string, AttributeValue> key)
        {
            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = key
            };

            var response = await _awsClients.DynamoDBClient.GetItemAsync(request);
            return response.Item;
        }
        public async Task DeleteItemAsync(string tableName, Dictionary<string, AttributeValue> key)
        {
            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = key
            };

            await _awsClients.DynamoDBClient.DeleteItemAsync(request);
        }
    }
}
