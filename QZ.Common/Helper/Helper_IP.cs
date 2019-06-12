using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Linq;

namespace QZ.Common.Helper
{
    public static class Helper_IP
    {
        public static string GetServiceIP()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Select(p => p.GetIPProperties())
                .SelectMany(p => p.UnicastAddresses)
                .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
                .FirstOrDefault()?.Address.ToString();
        }
    }
}
