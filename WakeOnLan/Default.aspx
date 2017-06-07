<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <meta http-equiv="refresh" content="10"/>
        <link rel="Stylesheet" href="css/default.css" media="all" />
        <title>Wake On LAN</title>
    </head>

    <body>
        
        <div class="mainContent">
            <form id="form1" runat="server">    
            
                <asp:SqlDataSource ID="ComputersDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:WakeOnLanConnectionString %>" 
                    SelectCommand="GetStatusOfComputers" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="displayName" SessionField="username" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="OwnersDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:WakeOnLanConnectionString %>" 
                    SelectCommand="GetOwners" SelectCommandType="StoredProcedure">
                </asp:SqlDataSource>
                
                <!--<img src="images/top_nav.jpg" />-->
                <h2>Wake On LAN</h2> 
               
                <br />
<%--                Show computers added by:    
                <asp:DropDownList ID="lstOwners" runat="server"  AutoPostBack="True" 
                    DataSourceID="OwnersDataSource" DataTextField="DisplayName" 
                    DataValueField="DisplayName" ondatabound="lstOwners_DataBound" 
                    onselectedindexchanged="lstOwners_SelectedIndexChanged">
                </asp:DropDownList>--%>
                    
                    <asp:DataList  ID="lstComputers" runat="server" DataKeyField="ComputerId" DataSourceID="ComputersDataSource"
                         OnItemDataBound="lstComputers_ItemDataBound" 
                        OnItemCommand="lstComputers_ItemCommand">
                        <HeaderTemplate>
                            <tr class="computer">               
                                <th class="header">    
                                    Current State   
                                </th>
                                <th class="header">                        
                                    Host Name
                                </th>
                                <th class="header">
                                    MAC Address
                                </th>
                                <th class="header">                        
                                    Wake
                                </th>
                                <th>
                                </th>
                            </tr                

                        </HeaderTemplate>
                         
                        <ItemTemplate>
                            <tr class="computer">               
                                <td class="cell">    
                                    <asp:Image ID="imgComputer" runat="server" />
                                </td>
                                <td class="cell">                        
                                    <asp:Label ID="lblHostName" runat="server" Text='<%# Eval("HostName") %>'></asp:Label>                    
                                </td>
                                <td class="cell">
                                    <asp:Label ID="lblMacAddress" runat="server" Text='<%# Eval("macAddress").ToString().ToUpper() %>'></asp:Label>
                                </td>
                                <td class="cell">                        
                                    <asp:LinkButton ID="lnkWakeUp" runat="server" Visible="False" CommandName="WakeUp">Wake Up</asp:LinkButton>
                                </td>
                                <td class="cell" style="background-color: transparent;">                        
                                    <asp:LinkButton ID="lnkDelete" runat="server" Visible="True" CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>                
                        </ItemTemplate>
                        
                    </asp:DataList>
                    <p></p>
                   <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Legend.aspx">Help</asp:HyperLink>  -  
                   <asp:HyperLink ID="lnkAddNewComputer" runat="server" NavigateUrl="~/AddNewComputer.aspx">Add New Computer</asp:HyperLink>
            </form>
        </div>
    </body>
</html>
