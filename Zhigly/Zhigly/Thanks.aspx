<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="Thanks.aspx.cs" Inherits="Zhigly.Thanks" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Thank You!</title>
    <link rel="stylesheet" type="text/css" href="/Styles/ThanksStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="thank-you">Thank you for registering!</div>
    <div id="ThankYouMessage" runat="server"></div>
    <asp:HyperLink runat="server" NavigateUrl="~/Home.aspx" CssClass="button">Go Home</asp:HyperLink>
    <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx" CssClass="button">Login</asp:HyperLink>
</asp:Content>

<asp:Content ContentPlaceHolderID="footer" runat="server">
</asp:Content>
