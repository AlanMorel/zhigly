<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Schools.aspx.cs" Inherits="Zhigly.Schools" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Styles/SchoolsStyle.css" />
    <title>Zhigly - Schools</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image runat="server" ImageUrl="/Images/Banners/schools_banner.png" CssClass="banner" AlternateText="Zhigly Schools"/>
    <div class="container">
        <h1>Schools</h1>
        <hr />
        <div id="SchoolContainers" runat="server"></div>
    </div>
</asp:Content>
