using System.Net;
using System.Net.Sockets;

namespace AIMobile.Services.Utilities
{
    public class NetworkHelper
    {
        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily==AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
        throw new Exception("No Network adapters with an IP24 address in  the  system");
        }
    }
}
