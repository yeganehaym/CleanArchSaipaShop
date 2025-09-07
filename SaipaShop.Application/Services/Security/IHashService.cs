namespace SaipaShop.Application.Services.Security;

public interface IHashService
{
    string GetSha256Hash(string input);
    Guid CreateCryptographicallySecureGuid();
}