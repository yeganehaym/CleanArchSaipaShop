using SaipaShop.Application.Services.Storage;

namespace SaipaShop.Infrastructure.Services.Storage;

public class FileStorageService:IStorageService
{
    private string MakeObjectName(string fileName)
    {
        var ext = Path.GetExtension(fileName);
        var objectName = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10) + ext;
        return objectName;
    }
    
    public async Task<string> SaveFile(string bucket, string fileName, string contentType, Stream stream)
    {

        Directory.CreateDirectory(bucket);
        var newFileName = MakeObjectName(fileName);
        stream.Position = 0;
        
        using (var fileStream=new FileStream(Path.Combine(bucket,newFileName),FileMode.Create))
        {
            await stream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            fileStream.Close();
        }

        return newFileName;
    }

    public async Task<Stream> GetFile(string bucket, string fileName)
    {
        if (!File.Exists(Path.Combine(bucket, fileName)))
        {
            return null;
        }
        var file =await File.ReadAllBytesAsync(Path.Combine(bucket, fileName));

        return new MemoryStream(file);
    }

    public Task RemoveFile(string bucket, string objectName)
    {
        File.Delete(Path.Combine(bucket,objectName));
        return Task.CompletedTask;
    }

}