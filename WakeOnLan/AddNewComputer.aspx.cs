using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

public partial class AddNewComputer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDisplayName.Text = System.Environment.UserName;
            
            try
            {
                // Try to resolve the hostname that the request has come from by 
                // doing a reverse DNS lookup on it
                txtHostnameOrAddress.Text = Dns.GetHostEntry(Request.UserHostName).HostName;
            }
            catch
            {
                // Dns lookup failed, just do it by IP address
                txtHostnameOrAddress.Text = Request.UserHostName;
            }
            
            lblError.Text = String.Empty;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string macAddress = Arp.GetMACAddress(this.txtHostnameOrAddress.Text);

        if (macAddress != null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WakeOnLanConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("CreateNewComputer", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@DisplayName", this.txtDisplayName.Text);
                sqlCommand.Parameters.AddWithValue("@hostName", this.txtHostnameOrAddress.Text);
                sqlCommand.Parameters.AddWithValue("@macAddress", macAddress);

                sqlCommand.ExecuteNonQuery();

                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            lblError.Text = "Unable to obtain MAC address";
        }
    }
}
