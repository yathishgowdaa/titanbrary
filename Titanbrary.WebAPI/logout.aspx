<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="Titanbrary.WebAPI.logout" %>

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
                <h1 class="auto-style1"><strong style="text-align: center; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;">Titanbrary</strong></h1>
                <p class="auto-style1">
                    <asp:HyperLink ID="homeNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/login.aspx">Home</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="registerNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/register.aspx">Register</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="helpNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/help.aspx">Help</asp:HyperLink>
                </p>
            </asp:Panel>
        </div>
    </form>
    <p class="auto-style1">
        <strong>You have successfully logged out</strong></p>
</body>
</html>
