<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewComputer.aspx.cs" Inherits="AddNewComputer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Wake On LAN : Add New Computer</title>
    <link rel="Stylesheet" href="css/default.css" media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainContent">
    
      <a href="http://www.google.com" border="0"><img src="images/top_nav.jpg" /></a>
                
      <h2>Add New Computer</h2>
      Add a new PC to appear on your monitoring page.  If the PC you want to add is the PC you are currently using you probably don't need to do anything on this screen other than click 'add'.
      <br />
      <br />
    	    <div class="formField text">
			    <div class="formLabel">
			      <label>Added By</label> 
			    </div>
			    <div class="formInputText">
			        <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="50" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldDisplayName" runat="server" ControlToValidate="txtDisplayName"
                        ErrorMessage="Enter Computer Display Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
			    <div class="formErrorMsg">						    
			    </div>
			 </div>
			 
			  <div class="formField text">
			    <div class="formLabel">
			      <label>PC Hostname or IP Address</label> 
			    </div>
			    <div class="formInputText">
			        <asp:TextBox ID="txtHostnameOrAddress" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHostnameOrAddress"
                        ErrorMessage="Enter Hostname or Address" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
			    <div class="formErrorMsg">						    
			    </div>
			 </div>
			 
			   <div class="formField text">			   
			       <asp:Button ID="btnSubmit" runat="server" Text="Add Computer" OnClick="btnSubmit_Click" />			   
			       &nbsp; <asp:Label ID="lblError" runat="server" Text="Label" ForeColor="Red"></asp:Label>
			 </div>    
        
        
     </div>
    </form>
</body>
</html>
