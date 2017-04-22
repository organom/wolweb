///
// Wake on LAN 
// Neil Rees - iMeta Technologies
// 2008
///

using System.Net.NetworkInformation;

/// <summary>
/// Summary description for Ping
/// </summary>
public static class PingIt
{

    private const int PING_TIMEOUT = 1000;

    public static bool IsComputerAccessible(string hostNameOrAddress)
    {
        return IsComputerAccessible(hostNameOrAddress, PING_TIMEOUT);
    }

    public static bool IsComputerAccessible(string hostNameOrAddress, int timeout)
    {
        try
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(hostNameOrAddress, timeout);
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}
