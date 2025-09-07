using Amazon.S3;
using Amazon.S3.Model;
using SaipaShop.Application.Services.Storage;
using Microsoft.Extensions.Options;

namespace SaipaShop.Infrastructure.Services.Storage;

public class AWSSDKS3 : IStorageService
{
    private IAmazonS3 client;
    private StorageOptions options;

    public AWSSDKS3(IOptions<StorageOptions> _snapshot)
    {
        options = _snapshot.Value;

        var config = new AmazonS3Config
        {
            ServiceURL = options.EndPoint,
            ForcePathStyle = true,
        };
        var credentials = new Amazon.Runtime.BasicAWSCredentials(options.AccessKey, options.SecretKey);
        client = new AmazonS3Client(credentials,config);
        
    }

    public async Task<string> SaveFile(string bucket, string fileName, string contentType, Stream stream)
    {
        bucket = options.Bucket;
     
        PutObjectRequest request = new PutObjectRequest
        {
            BucketName = bucket,
            Key = fileName,
            InputStream = stream
        };

        var response=await client.PutObjectAsync(request);
        Console.WriteLine($"File '{fileName}' uploaded successfully.");
        return fileName;
    }

    public async Task<Stream> GetFile(string bucket, string fileName)
    {
        bucket = options.Bucket;

        GetObjectRequest request = new GetObjectRequest
        {
            BucketName = bucket,
            Key = fileName
        };

        using GetObjectResponse response = await client.GetObjectAsync(request);
        return response.ResponseStream;

    }

    public async Task RemoveFile(string bucket, string objectName)
    {
        bucket = options.Bucket;

        DeleteObjectRequest deleteRequest = new DeleteObjectRequest
        {
            BucketName = bucket,
            Key = objectName
        };

        await client.DeleteObjectAsync(deleteRequest);

        Console.WriteLine($"File '{objectName}' deleted successfully.");
  
    }

 
}