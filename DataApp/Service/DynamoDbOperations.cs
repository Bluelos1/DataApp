using Amazon.DynamoDBv2;
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
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public DynamoDbOperations(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task AddItemAsync(string tableName, Dictionary<string, AttributeValue> item)
        {
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = item
            };
            await _dynamoDbClient.PutItemAsync(request);
        }
        public async Task<Dictionary<string, AttributeValue>> GetItemAsync(string tableName, string itemId)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = itemId } }
            };
            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = key
            };

            var response = await _dynamoDbClient.GetItemAsync(request);
            return response.Item;
        }

        public async Task GetAllItems(string tableName)
        {
            var request = new ScanRequest
            {
                TableName = tableName
            };
            var response = await _dynamoDbClient.ScanAsync(request);
            foreach(var item in response.Items)
            {
                foreach (var kvp in item)
                {
                    Console.Write($"{kvp.Key}: ");
                    var attributeValue = kvp.Value;
                    if (attributeValue.S != null)
                    {
                        Console.Write(attributeValue.S);
                    }
                    else if (attributeValue.S != null)
                    {
                        Console.Write(attributeValue.N);
                    }
                    Console.Write(", ");
                }
                Console.WriteLine();
            }
        }
        public async Task DeleteItemAsync(string tableName, string itemId)
        {
            var key = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = itemId } }
            };
            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = key
            };

            await _dynamoDbClient.DeleteItemAsync(request);
        }
    }
}
