///
// Wake on LAN 
// Neil Rees - iMeta Technologies
// 2008
///

using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;


internal static class NativeMethods
{
    [DllImport("iphlpapi.dll", ExactSpelling = true)]
    internal static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);
}

/// <summary>
/// Summary description for ARP
/// </summary>
public static class Arp
{

    public static string GetMACAddress(IPAddress ipAddress)
    {        
        byte[] addressBytes = ipAddress.GetAddressBytes();
        int address = BitConverter.ToInt32(addressBytes, 0);

        byte[] macAddr = new byte[6];
        uint macAddrLen = (uint)macAddr.Length;

        if (NativeMethods.SendARP( address, 0, macAddr, ref macAddrLen) != 0)
        {
            return null;
        }

        StringBuilder macAddressString = new StringBuilder();
        for (int i = 0; i < macAddr.Length; i++)
        {
            if (macAddressString.Length > 0)
                macAddressString.Append(":");

            macAddressString.AppendFormat("{0:x2}", macAddr[i]);
        }

        return macAddressString.ToString();
    }

    public static string GetMACAddress(string hostName)
    {

        IPHostEntry hostEntry = null;
        try
        {
            hostEntry = Dns.GetHostEntry(hostName);
        }
        catch
        {
            return null;
        }

        if (hostEntry.AddressList.Length == 0)
        {
            return null;
        }

        // Find the first address IPV4 address for that host
        IPAddress ipAddress = null;

        foreach (IPAddress ip in hostEntry.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ipAddress = ip;
                break;
            }
        }

        // If running on .net 3.5 you can do it with LINQ :)
        //IPAddress ipAddress = hostEntry.AddressList.First<IPAddress>(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        return GetMACAddress(ipAddress);

    }
}
