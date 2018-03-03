<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Titanbrary.WebAPI.loginPage" %>

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
        <div style="text-align: center">
            <asp:Panel ID="header" runat="server" BackColor="#000066" ForeColor="#CCFFFF" Height="92px">
                <h1><strong style="text-align: center; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;">Titanbrary</strong></h1>
                <p class="auto-style1">
                    <asp:HyperLink ID="homeNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/login.aspx">Home</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="registerNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/register.aspx">Register</asp:HyperLink>
                    &nbsp;|
                    <asp:HyperLink ID="helpNvg" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#FFFFCC" NavigateUrl="~/help.aspx">Help</asp:HyperLink>
                </p>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Font-Bold="True" Font-Names="Arial">
                <br />
                Username :
                <asp:TextBox ID="usernameTextbox" runat="server"></asp:TextBox>
                <br />
                Password :
                <asp:TextBox ID="passwordTextbox" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="signinBtn" runat="server" Font-Bold="True" Font-Names="Arial" OnClick="signIn_Click" Text="Sign In" />
                &nbsp;
                <asp:Button ID="registerBtn" runat="server" Font-Bold="True" Font-Names="Arial" PostBackUrl="~/register.aspx" Text="Register" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
