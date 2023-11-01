using DataApp.Clients;
using DataApp.Service;
class Program
{
    private static AwsClients _awsClients;
    private static S3Operations _s3Operations;
    private static DynamoDbOperations _dynamoDbOperations;
    static void Main(string[] args)
    {
        _awsClients = new AwsClients();
        _s3Operations = new S3Operations(_awsClients);
        _dynamoDbOperations = new DynamoDbOperations(_awsClients);

        while (true)
        {
            Console.WriteLine("Wybierz operację:");
            Console.WriteLine("1. Wgraj plik na S3");
            Console.WriteLine("2. Pobierz plik z S3");
            Console.WriteLine("3. Dodaj rekord do DynamoDB");
            Console.WriteLine("0. Wyjście");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UploadFileToS3();
                    break;
                case "2":
                   // DownloadFileFromS3();
                    break;
                case "3":
                    //AddRecordToDynamoDB();
                    break;
                case "0":
                    return;
            }
        }
    }

    private static void UploadFileToS3()
    {
        Console.WriteLine("Podaj ścieżkę do pliku:");
        var path = Console.ReadLine();
        Console.WriteLine("Podaj nazwę kubełka:");
        var bucketName = Console.ReadLine();
        Console.WriteLine("Podaj nazwę klucza (nazwę pliku w S3):");
        var keyName = Console.ReadLine();

        using var fileStream = new FileStream(path, FileMode.Open);

        _s3Operations.UploadFileAsync(bucketName, keyName, fileStream).Wait();

        Console.WriteLine("Plik został wgrany na S3!");
    }

    // ... (logika UI)
}
