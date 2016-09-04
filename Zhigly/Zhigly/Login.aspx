<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Zhigly.Login" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Login</title>
    <link rel="stylesheet" type="text/css" href="/Styles/LoginStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="header">
        <asp:HyperLink ID="HomeHyperLink" runat="server" NavigateUrl="~/" CssClass="logo" ImageUrl="~/Images/Logo/white_logo.png"></asp:HyperLink>
    </div>
    <div>
        <label>Email:</label>
        <asp:TextBox ID="EmailTextbox" runat="server" TextMode="Email"></asp:TextBox>
    </div>
    <div>
        <label>Password:</label>
        <asp:TextBox ID="PasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
    </div>
    <div class="remember-me">
        <asp:CheckBox ID="RememberMeCheckbox" runat="server" Checked="True"/>
        Remember Me
    </div>
    <div>
        <label runat="server" id="ErrorLabel" class="error-label"></label>
    </div>
    <div>
        <asp:Button CssClass="button" ID="LogInButton" runat="server" Text="Log Into Zhigly" OnClick="logInButton_Click" />
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="footer" runat="server">
    <asp:HyperLink runat="server" NavigateUrl="~/Home.aspx">Home</asp:HyperLink>
    /
    <asp:HyperLink runat="server" NavigateUrl="~/Register.aspx">Need an account?</asp:HyperLink>
</asp:Content>
