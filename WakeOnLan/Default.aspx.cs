///
// Wake on LAN 
// Neil Rees - iMeta Technologies
// 2008
///

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private enum PCState { Off, On, Waking, Unknown };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Session.Add("username", System.Environment.UserName);
        }
    }

    protected void lstComputers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
               e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
            
            string hostName = (string)drv.Row["hostName"];
            string computerId = drv.Row["computerId"].ToString();

            PCState status = PCState.Unknown;
            try
            {
                if (PingIt.IsComputerAccessible(hostName))
                {
                    // It's on
                    status = PCState.On;
                    Session.Remove(computerId);
                }
                else
                {
                    // It's not answering pings, but maybe we've already told it to wake,
                    // in which case its state is 'waking'
                    status = Session[computerId] == null ? PCState.Off : PCState.Waking;
                }
            }
            catch
            {
                // Failed to check status
                status = PCState.Unknown;
            }

            Image imgComputer = (Image)e.Item.FindControl("imgComputer");
            switch (status)
            {
                case PCState.On:
                    {
                        imgComputer.ImageUrl = "images/ComputerRunning.gif";
                        imgComputer.AlternateText = "Computer Running";
                        break;
                    }
                case PCState.Waking:
                    {
                        imgComputer.ImageUrl = "images/ComputerPoweringUp.gif";
                        imgComputer.AlternateText = "Computer Powering Up";
                        break;
                    }
                case PCState.Off:
                    {
                        imgComputer.ImageUrl = "images/ComputerOff.gif";
                        imgComputer.AlternateText = "Computer Off";

                        //Show wake up button
                        LinkButton lnkWakeUp = (LinkButton)e.Item.FindControl("lnkWakeUp");
                        lnkWakeUp.Visible = true;
                        lnkWakeUp.CommandArgument = drv.Row["ComputerID"].ToString();
                        lnkWakeUp.Text += " " + drv.Row["hostName"] as string;
                        break;
                    }
                case PCState.Unknown:
                    {
                        imgComputer.ImageUrl = "images/ComputerUnknown.gif";
                        imgComputer.AlternateText = "Computer Unknown";

                        //Show wake up button
                        LinkButton lnkWakeUp = (LinkButton)e.Item.FindControl("lnkWakeUp");
                        lnkWakeUp.Visible = true;
                        lnkWakeUp.CommandArgument = drv.Row["ComputerID"].ToString();
                        lnkWakeUp.Text += " " + drv.Row["hostName"] as string;
                        break;
                    }
            }

            LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
            lnkDelete.CommandArgument = drv.Row["ComputerID"].ToString();
        }
    }


    protected void lstComputers_ItemCommand(object source, DataListCommandEventArgs e)
    {
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WakeOnLanConnectionString"].ConnectionString))
        {
            sqlConnection.Open();

            int computerId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "WakeUp")
            {
                SqlCommand sqlCommand = new SqlCommand("GetMACAddress", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ComputerId", computerId);
                string macAddress = sqlCommand.ExecuteScalar() as string;

                //Wake up given PC
                Wol.Wake(macAddress);
                Session.Add(computerId.ToString(), "waking");
            }

            if (e.CommandName == "Delete")
            {
                SqlCommand sqlCommand = new SqlCommand("DeleteComputer", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ComputerId", computerId);
                sqlCommand.ExecuteNonQuery();
            }
        }

        // Refresh the list of computers
        lstComputers.DataBind();
    }



    //protected void lstOwners_DataBound(object sender, EventArgs e)
    //{
    //    bool found = false;

    //    // Loop through all the computer owners
    //    foreach (ListItem li in lstOwners.Items)
    //    {
    //        // If we find ourselves select ourself
    //        if (li.Value.Equals(Session["username"]))
    //        {
    //            li.Selected = true;
    //            found = true;
    //            break;
    //        }
    //    }

    //    // If we're not in the list (maybe we have no computers yet)
    //    if (!found)
    //    {
    //        // Add ourselves in
    //        ListItem li = new ListItem((string)Session["username"], (string)Session["username"]);
    //        li.Selected = true;
    //        lstOwners.Items.Add(li);
    //    }

    //}

    //protected void lstOwners_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    // If someone changes the current "owner", then repopulate the computers list with their PCs
    //    Session.Add("username", lstOwners.SelectedValue);
    //    lstOwners.DataBind();
    //}
}
