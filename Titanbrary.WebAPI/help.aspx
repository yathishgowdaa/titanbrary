<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="Titanbrary.WebAPI.help" %>

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
        <asp:Panel ID="header" runat="server" BackColor="#000066" ForeColor="#CCFFFF" Height="92px">
            <h1 class="auto-style1"><strong style="text-align: center; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;">Titanbrary</strong></h1>
            <p class="auto-style1">
                <asp:HyperLink ID="homeNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/login.aspx">Home</asp:HyperLink>
            </p>
        </asp:Panel>
        <asp:Panel ID="contentTop" runat="server" Font-Names="Arial">
            <br />
            <strong>Help<br /> </strong>Manual of how to use this website<br /> Project name<br /> Group member<br /> Contact information<br /> How do we do this project<br /> Etc.</asp:Panel>
    </form>
</body>
</html>
