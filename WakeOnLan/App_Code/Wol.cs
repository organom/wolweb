///
// Wake on LAN 
// Neil Rees - iMeta Technologies
// 2008
///

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text.RegularExpressions;


/// <summary>
/// Class for sending Wake On LAN Magic Packets
/// </summary>
public static class Wol
{

    /// <summary>
    /// Wake up the device by sending a 'magic' packet
    /// </summary>
    /// <param name="macAddress"></param>
    public static void Wake(string macAddress)
    {
        string[] byteStrings = macAddress.Split(':');

        byte[] bytes = new byte[6];

        for (int i = 0; i < 6; i++)
            bytes[i] = (byte)Int32.Parse(byteStrings[i], System.Globalization.NumberStyles.HexNumber );

        Wake(bytes);
    }

    /// <summary>
    /// Send a magic packet
    /// </summary>
    /// <param name="macAddress"></param>
    public static void Wake(byte[] macAddress)
    {
        if (macAddress == null)
        {
            throw new ArgumentNullException("macAddress", "MAC Address must be provided");
        }

        if (macAddress.Length != 6)
        {
            throw new ArgumentOutOfRangeException("macAddress", "MAC Address must have 6 bytes");
        }

        // A Wake on LAN magic packet contains a 6 byte header and
        // the MAC address of the target MAC address (6 bytes) 16 times
        byte[] wolPacket = new byte[17 * 6];

        MemoryStream ms = new MemoryStream(wolPacket, true);

        // Write the 6 byte 0xFF header
        for (int i = 0; i < 6; i++)
        {
            ms.WriteByte(0xFF);
        }

        // Write the MAC Address 16 times
        for (int i = 0; i < 16; i++)
        {
            ms.Write(macAddress, 0, macAddress.Length);
        }

        // Broadcast the magic packet
        UdpClient udp = new UdpClient();
        udp.Connect(IPAddress.Broadcast, 0);
        udp.Send(wolPacket, wolPacket.Length);
    }
	
}
