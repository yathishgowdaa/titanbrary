<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Titanbrary.WebAPI.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1 {
            text-align: center;
        }
        .auto-style3 {
            height: 23px;
            width: 32px;
        }
        .auto-style5 {
            height: 23px;
            width: 388px;
        }
        .auto-style13 {
            width: 388px;
            height: 18px;
        }
        .auto-style15 {
            width: 32px;
        }
        .auto-style16 {
            width: 32px;
            height: 22px;
        }
        .auto-style17 {
            width: 32px;
            height: 18px;
        }
        .auto-style18 {
            height: 23px;
            width: 198px;
        }
        .auto-style19 {
            width: 198px;
        }
        .auto-style20 {
            width: 198px;
            height: 22px;
        }
        .auto-style21 {
            width: 198px;
            height: 18px;
        }
        .auto-style24 {
            width: 388px;
        }
        .auto-style25 {
            width: 388px;
            height: 22px;
        }
        .auto-style26 {
            width: 32px;
            height: 24px;
        }
        .auto-style27 {
            width: 198px;
            height: 24px;
        }
        .auto-style28 {
            width: 388px;
            height: 24px;
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
        </div>
        <asp:Panel ID="content" runat="server" Font-Names="Arial" Height="523px">
            <table style="width:100%;">
                <tr>
                    <td class="auto-style3"><strong>
                        <br />
                        Register</strong></td>
                    <td class="auto-style18"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Username : * </td>
                    <td class="auto-style24">
                        <asp:TextBox ID="usernameTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Password : * </td>
                    <td class="auto-style24">
                        <asp:TextBox ID="passwordTextbox" runat="server" TextMode="Password" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Confirm Password : * </td>
                    <td class="auto-style24">
                        <asp:TextBox ID="confirmPasswordTextbox" runat="server" TextMode="Password" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16"></td>
                    <td class="auto-style20">Student ID : * </td>
                    <td class="auto-style25">
                        <asp:TextBox ID="studentIDTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">First Name : * </td>
                    <td class="auto-style24">
                        <asp:TextBox ID="firstnameTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style20">Last Name : * </td>
                    <td class="auto-style25">
                        <asp:TextBox ID="lastnameTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16"></td>
                    <td class="auto-style20">Date of Birth : </td>
                    <td class="auto-style25">
                        <asp:TextBox ID="datePickedTextbox" runat="server" Width="282px"></asp:TextBox>
                        &nbsp;<asp:Button ID="pickDateBtn" runat="server" Font-Bold="True" OnClick="pickDateBtn_Click" Text="Pick Date" />
                        <asp:Calendar ID="calendar" runat="server" EnableTheming="True" OnSelectionChanged="calendar_SelectionChanged" ShowGridLines="True" Visible="False" Width="286px"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style26"></td>
                    <td class="auto-style27">Address :</td>
                    <td class="auto-style28">
                        <asp:TextBox ID="addressTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16"></td>
                    <td class="auto-style20">City : </td>
                    <td class="auto-style25">
                        <asp:TextBox ID="cityTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style20">State : </td>
                    <td class="auto-style25">
                        <asp:DropDownList ID="stateDropdown" runat="server">
                            <asp:ListItem>Alabama</asp:ListItem>
                            <asp:ListItem>Alaska</asp:ListItem>
                            <asp:ListItem>Arizona</asp:ListItem>
                            <asp:ListItem>Arkansas</asp:ListItem>
                            <asp:ListItem>California</asp:ListItem>
                            <asp:ListItem>Colorado</asp:ListItem>
                            <asp:ListItem>Connecticut</asp:ListItem>
                            <asp:ListItem>Delaware</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Georgia</asp:ListItem>
                            <asp:ListItem>Hawaii</asp:ListItem>
                            <asp:ListItem>Idaho</asp:ListItem>
                            <asp:ListItem>Illinois</asp:ListItem>
                            <asp:ListItem>Indiana</asp:ListItem>
                            <asp:ListItem>Iowa</asp:ListItem>
                            <asp:ListItem>Kansas</asp:ListItem>
                            <asp:ListItem>Kentucky</asp:ListItem>
                            <asp:ListItem>Louisiana</asp:ListItem>
                            <asp:ListItem>Maine</asp:ListItem>
                            <asp:ListItem>Maryland</asp:ListItem>
                            <asp:ListItem>Massachusetts</asp:ListItem>
                            <asp:ListItem>Michigan</asp:ListItem>
                            <asp:ListItem>Minnesota</asp:ListItem>
                            <asp:ListItem>Mississippi</asp:ListItem>
                            <asp:ListItem>Missouri</asp:ListItem>
                            <asp:ListItem>Montana</asp:ListItem>
                            <asp:ListItem>Nebraska</asp:ListItem>
                            <asp:ListItem>Nevada</asp:ListItem>
                            <asp:ListItem>New Hampshire</asp:ListItem>
                            <asp:ListItem>New Jersey</asp:ListItem>
                            <asp:ListItem>New Mexico</asp:ListItem>
                            <asp:ListItem>New York</asp:ListItem>
                            <asp:ListItem>North Carolina</asp:ListItem>
                            <asp:ListItem>North Dakota</asp:ListItem>
                            <asp:ListItem>Ohio</asp:ListItem>
                            <asp:ListItem>Oklahoma</asp:ListItem>
                            <asp:ListItem>Oregon</asp:ListItem>
                            <asp:ListItem>Pennsylvania</asp:ListItem>
                            <asp:ListItem>Rhode Island</asp:ListItem>
                            <asp:ListItem>South Carolina</asp:ListItem>
                            <asp:ListItem>South Dakota</asp:ListItem>
                            <asp:ListItem>Tennessee</asp:ListItem>
                            <asp:ListItem>Texas</asp:ListItem>
                            <asp:ListItem>Utah</asp:ListItem>
                            <asp:ListItem>Vermont</asp:ListItem>
                            <asp:ListItem>Virginia</asp:ListItem>
                            <asp:ListItem>Washington</asp:ListItem>
                            <asp:ListItem>West Virginia</asp:ListItem>
                            <asp:ListItem>Wisconsin</asp:ListItem>
                            <asp:ListItem>Wyoming</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Zipcode : </td>
                    <td class="auto-style24">
                        <asp:TextBox ID="zipcodeTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16"></td>
                    <td class="auto-style20">Phone : *</td>
                    <td class="auto-style25">
                        <asp:TextBox ID="phoneTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16"></td>
                    <td class="auto-style20">E-mail : *</td>
                    <td class="auto-style25">
                        <asp:TextBox ID="emailTextbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Gender :</td>
                    <td class="auto-style24">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Width="323px">
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Prefer not to say</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">&nbsp;</td>
                    <td class="auto-style21">Security Question 1 : *</td>
                    <td class="auto-style13">
                        <asp:DropDownList ID="question1Dropdown" runat="server" Width="288px">
                            <asp:ListItem>Q1</asp:ListItem>
                            <asp:ListItem>Q2</asp:ListItem>
                            <asp:ListItem>Q3</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Answer : *</td>
                    <td class="auto-style24">
                        <asp:TextBox ID="answer1Textbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">&nbsp;</td>
                    <td class="auto-style21">Security Question 2 : *</td>
                    <td class="auto-style24">
                        <asp:DropDownList ID="question2Dropdown" runat="server" Width="288px">
                            <asp:ListItem>Q1</asp:ListItem>
                            <asp:ListItem>Q2</asp:ListItem>
                            <asp:ListItem>Q3</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Answer : *</td>
                    <td class="auto-style24">
                        <asp:TextBox ID="answer2Textbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">&nbsp;</td>
                    <td class="auto-style21">Security Question 3 : *</td>
                    <td class="auto-style24">
                        <asp:DropDownList ID="question3Dropdown" runat="server" Width="288px">
                            <asp:ListItem>Q1</asp:ListItem>
                            <asp:ListItem>Q2</asp:ListItem>
                            <asp:ListItem>Q3</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style19">Answer : *</td>
                    <td class="auto-style24">
                        <asp:TextBox ID="answer3Textbox" runat="server" Width="282px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="auto-style1">
                <br />
                <asp:Button ID="submitBtn" runat="server" Font-Bold="True" Text="Submit" />
                &nbsp;
                <asp:Button ID="clearBtn" runat="server" Font-Bold="True" Text="Clear All" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>
