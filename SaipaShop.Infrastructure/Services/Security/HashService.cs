using System.Security.Cryptography;
using System.Text;
using SaipaShop.Application.Services.Security;

namespace SaipaShop.Infrastructure.Services.Security;

public class HashService:IHashService
{
    private readonly RandomNumberGenerator _rand = RandomNumberGenerator.Create();

    
    public string GetSha256Hash(string input)
    {
        if(String.IsNullOrEmpty(input))
            return String.Empty;
            
        using var hashAlgorithm = new SHA256CryptoServiceProvider();
        var byteValue = Encoding.UTF8.GetBytes(input);
        var byteHash = hashAlgorithm.ComputeHash(byteValue);
        return Convert.ToBase64String(byteHash);
    }
    
    public Guid CreateCryptographicallySecureGuid()
    {
        var bytes = new byte[16];
        _rand.GetBytes(bytes);
        return new Guid(bytes);
    }
}