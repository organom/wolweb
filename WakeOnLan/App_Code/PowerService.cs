using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

/// <summary>
/// Summary description for PowerService
/// </summary>
[WebService(Namespace = "http://www.codeplex.com/WOL/PowerService/1.0")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class PowerService : System.Web.Services.WebService
{

    public PowerService()
    {
    }


    [WebMethod]
    public bool PowerOn(string hostname)
    {
        bool retVal = false;

        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WakeOnLanConnectionString"].ConnectionString))
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("GetMACAddressByHostName", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@hostName", hostname);
            string macAddress = sqlCommand.ExecuteScalar() as string;

            if (macAddress != null)
            {
                //Wake up given PC
                Wol.Wake(macAddress);
                retVal = true;
            }
            
        }

        return retVal;
    }

    [WebMethod]
    public bool IsAvailable(string hostname)
    {
        return PingIt.IsComputerAccessible(hostname);
    }

}

