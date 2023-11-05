using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using DataApp.Clients;
using DataApp.Config;
using DataApp.Service;
using Microsoft.Extensions.Configuration;

public class Program
{
    private static S3Operations s3Operations; 
    private static DynamoDbOperations dynamoDbOperations;
    static void Main(string[] args)
    {
        
        var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

        var awsOptions = configuration.GetAWSOptions();
        var awsConfig = configuration.GetSection("AwsConfig").Get<AwsConfig>();
        var awsClients = new AwsClients(awsConfig);

   

        s3Operations = new S3Operations(awsClients.S3Client);
        dynamoDbOperations = new DynamoDbOperations(awsClients.DynamoDBClient);

        while (true)
        {
            Console.WriteLine("Chosse Operation:");
            Console.WriteLine("1. Upload file to S3");
            Console.WriteLine("2. Download file from S3");
            Console.WriteLine("3. Add item to DynamoDb");
            Console.WriteLine("4. Get item from DynamoDb");
            Console.WriteLine("5. Get All items from DynamoDb");
            Console.WriteLine("6. Delete item from DynamoDb");
            Console.WriteLine("0. Wyjście");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UploadFileToS3();
                    break;
                case "2":
                    DownloadFileFromS3().Wait();
                    break;
                case "3":
                    AddRecordToDynamoDB().Wait();
                    break;
                case "4":
                    GetItemFromDynamoDB().Wait();
                    break;
                case "5":
                     GetALlItemsFromDynamoDb().Wait();
                     break;
                case "6":
                    DeleteItemFromDynamoDB().Wait();
                    break;
                case "0":
                    return;
            }
        }
    }

    private static void UploadFileToS3()
    {
        Console.WriteLine("Enter file path");
        var path = Console.ReadLine();
        Console.WriteLine("Enter bucket name");
        var bucketName = Console.ReadLine();
        Console.WriteLine("Enter output file name");
        var keyName = Console.ReadLine();

        using var fileStream = new FileStream(path, FileMode.Open);
        try
        {
            s3Operations.UploadFileAsync(bucketName, keyName, fileStream).Wait();
            Console.WriteLine("File uploaded");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    private static async Task DownloadFileFromS3()
    {
        Console.WriteLine("Enter the bucket name:");
        var bucketName = Console.ReadLine();
        Console.WriteLine("Enter the key name for the file to download:");
        var keyName = Console.ReadLine();
        Console.WriteLine("Enter the path to save the downloaded file:");
        var downloadPath = Console.ReadLine();
        try
        {
            await s3Operations.DownloadFileAsync(bucketName, keyName, downloadPath);
            Console.WriteLine("File has been downloaded from S3!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    } 
    private static async Task AddRecordToDynamoDB()
    {
        Console.WriteLine("Enter table name:");
        var tableName = Console.ReadLine();
        Console.WriteLine("Enter field name");
        var fieldName = Console.ReadLine();
        var uniqueId = Guid.NewGuid().ToString();

        var itemData = new Dictionary<string, AttributeValue>
    {
        { "Id", new AttributeValue { S = uniqueId} },
        { "Name", new AttributeValue {S = fieldName.ToString() } }
    };

        await dynamoDbOperations.AddItemAsync(tableName, itemData);

        Console.WriteLine($"Item with ID{uniqueId} has been added to DynamoDB!");
    }
    private static async Task GetItemFromDynamoDB()
    {
        Console.WriteLine("Enter table name:");
        var tableName = Console.ReadLine();
        Console.WriteLine("Enter the item ID to retrieve:");
        var itemId = Console.ReadLine();

        var item = await dynamoDbOperations.GetItemAsync(tableName, itemId);

        if (item != null)
        {
            var name = item.ContainsKey("Name") ? item["Name"].S : "null";

            Console.WriteLine($"ID: {itemId}, Name: {name}");
        }
        else
        {
            Console.WriteLine("Item with the given ID not found");
        }

    }
    private static async Task GetALlItemsFromDynamoDb()
    {
        Console.WriteLine("Enter table name:");
        var tableName = Console.ReadLine();
        await dynamoDbOperations.GetAllItems(tableName);
    }

    private static async Task DeleteItemFromDynamoDB()
    {
        Console.WriteLine("Enter table name:");
        var tableName = Console.ReadLine();
        Console.WriteLine("Enter the item ID to delete:");
        var itemId = Console.ReadLine();

        await dynamoDbOperations.DeleteItemAsync(tableName, itemId);

        Console.WriteLine("Item has been deleted from DynamoDB!");
    }

}
