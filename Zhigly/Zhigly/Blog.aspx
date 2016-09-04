<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="Zhigly.Blog" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Blog</title>
    <link rel="stylesheet" type="text/css" href="/Styles/BlogStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <script src="/JS/Facebook.js"></script>
    <script src="/JS/Twitter.js"></script>
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <asp:Image runat="server" ImageUrl="~/Images/Banners/blog_banner.png" CssClass="banner" AlternateText="Zhigly Blog Banner"/>
    <div class="container" runat="server" id="Container"></div>
</asp:Content>
