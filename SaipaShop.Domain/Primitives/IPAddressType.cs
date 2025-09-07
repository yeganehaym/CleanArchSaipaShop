using System.Net;
using SaipaShop.Domain.Exceptions;

namespace SaipaShop.Domain.Primitives;

public class IPAddressType
{
    public string IP { get; private set; }

    private  IPAddressType(string ip)
    {
        IP = ip;
    }

    public static IPAddressType Create(string ipAddress)
    {
        if (String.IsNullOrEmpty(ipAddress))
        {
            throw new EmptyException(nameof(IP));
        }

        if (!IsIpValid(ipAddress))
        {
            throw new InvalidException(nameof(IP));
        }
        
        return new IPAddressType(ipAddress);
    }

    public static bool IsIpValid(string ip)
    {
        IPAddress ipAddress;
        return IPAddress.TryParse(ip, out ipAddress);
    }
   
}