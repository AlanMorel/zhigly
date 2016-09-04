<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Zhigly.Error" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - An error has occured.</title>
    <link rel="stylesheet" type="text/css" href="/Styles/ErrorStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image runat="server" ImageUrl="~/Images/error.png" AlternateText="Zhigly Error"/>
    <div>An error has occurred.</div>
    <div id="ErrorMessage" runat="server">Click below to navigate back to home.</div>
    <asp:HyperLink runat="server" NavigateUrl="~/Home.aspx" CssClass="button">Go Home</asp:HyperLink>
</asp:Content>

<asp:Content ContentPlaceHolderID="footer" runat="server">
</asp:Content>
