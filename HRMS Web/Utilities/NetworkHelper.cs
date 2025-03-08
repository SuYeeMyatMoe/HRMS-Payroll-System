using System.Net.Sockets;
using System.Net;

namespace HRMS_Web.Utilities
{
    public static class NetworkHelper
    {
        public static async Task<string> GetIPAddress()
        {
            string ip = "unknown";
            string url = "https://api.ipify.org";
            try
            {
                //using HttpClient to get public IP address
                using (HttpClient client = new HttpClient())//using HttpClient to get public IP address
                {
                    ip = await client.GetStringAsync(url);//getting IP address from the url with Async method (concurrent programming style method)
                }
            }
            catch (Exception)
            {
                //if there is an error, try to get the local IP address
                ip = GetLocalIPAddress();
            }
            return ip;
        }

        //get the local IP address
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
