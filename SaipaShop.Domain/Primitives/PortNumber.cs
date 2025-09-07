namespace SaipaShop.Domain.Primitives;

public class PortNumber
{
    public int PortNo { get; private set; }

    private PortNumber(int port)
    {
        PortNo = port;
    }

    public static PortNumber FromNumber(int port) => new PortNumber(port);

    public static PortNumber Create(int port)
    {
        if (port < 1 || port > 65535)
        {
            throw new Exception(nameof(PortNo));
        }
        
        return new PortNumber(port);
    }

}