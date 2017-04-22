<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Legend.aspx.cs" Inherits="Legend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="Stylesheet" href="css/default.css" media="all" />
    <title>Wake On LAN : Legend</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainContent">
        <a href="http://www.imeta.co.uk" border="0"><img src="images/top_nav.jpg" /></a>
        <h2>
            ASP.NET Wake On LAN</h2>
        <br />
        The screen shows the computers that have been added to be monitored.&nbsp; The
        different icons represent different states for the PC:<br />
        <br />
        <img alt="Computer Off" longdesc="Computer Off" src="images/ComputerOff.gif" style="width: 55px;
            height: 49px" />
        means the computer is off or on standby, you can wake it up
        <br />
        <img alt="Computer Resuming" longdesc="Computer Resuming" src="images/ComputerPoweringUp.gif"
            style="width: 55px; height: 49px" />
        means the wake up message has been sent and it is resuming, should be powered up
        soon<br />
        <img alt="Computer On" longdesc="Computer On" src="images/ComputerRunning.gif" style="width: 55px;
            height: 49px" />
        means the computer is on and responding<br />
        <img alt="Computer Unknown" longdesc="Computer Unknown" src="images/ComputerUnknown.gif"
            style="width: 55px; height: 49px" />
        means the computer is in an unknown state, you can attempt to wake it
        <br />
        <br />
        When the computer is in standby mode you can click the <i>Wake</i> link next to
        it to wake it up again.
        <br />
        You can add additional computers to monitor by clicking the <i>Add New Computer</i>
        link.
        <br /><br />
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Back</asp:HyperLink>
    </div>
    </form>
</body>
</html>
