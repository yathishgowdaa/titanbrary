<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboardAdmin.aspx.cs" Inherits="Titanbrary.WebAPI.dashboardAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="header" runat="server" BackColor="#000066" ForeColor="#CCFFFF" Height="92px">
                <h1 class="auto-style1"><strong style="text-align: center; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;">Titanbrary<asp:Button ID="logoutBtn" runat="server" Align="right" Font-Bold="True" Font-Names="Arial" Text="Logout" PostBackUrl="~/logout.aspx"/>
                    </strong>
                </h1>
                <p class="auto-style1">
                    <asp:HyperLink ID="homeNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/dashboardAdmin.aspx">Home</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="modificationNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC">Modification</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="privilegeNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC">Grant Privilege</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="settingNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC">Settings</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="helpNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/help.aspx">Help</asp:HyperLink>
                </p>
            </asp:Panel>
        </div>
        <asp:Panel ID="contentTop" runat="server" Font-Names="Arial">
            <br />
            <strong>Hello, <em>Admin</em></strong></asp:Panel>
    </form>
</body>
</html>
