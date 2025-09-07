namespace SaipaShop.Infrastructure.Services.Storage;

public class StorageOptions
{
    
    public string EndPoint { get; set; }
    public string Port { get; set; }
        
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Bucket { get; set; }

}