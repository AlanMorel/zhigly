<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="Activation.aspx.cs" Inherits="Zhigly.Activation" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Account Activation</title>
    <link rel="stylesheet" type="text/css" href="/Styles/ActivationStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div id="Success" runat="server">
        <h2>Activation Successful!</h2>
        <p>Welcome to Zhigly, <span id="Name" runat="server"></span>!</p>
        <p>You can now post listings of your own.</p>
    </div>
    <div id="Failure" runat="server">
        <h2>Activation Failed.</h2>
        <p>The link you have followed is not valid.</p>
        <p>Please contact us directly if you feel this is an error.</p>
    </div>
    <div id="Activated" runat="server">
        <h2>Already Activated.</h2>
        <p>Your account has already been activated.</p>
    </div>
    <asp:HyperLink runat="server" NavigateUrl="~/" CssClass="button">Home</asp:HyperLink>
    <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx" CssClass="button">Login</asp:HyperLink>
</asp:Content>

<asp:Content ContentPlaceHolderID="footer" runat="server">
</asp:Content>
