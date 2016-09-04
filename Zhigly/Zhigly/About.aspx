<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Zhigly.About" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - About</title>
    <link rel="stylesheet" type="text/css" href="/Styles/AboutStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image runat="server" ImageUrl="~/Images/Banners/about_banner.png" CssClass="banner" AlternateText="About Zhigly Banner" />
    <div class="container">
        <h1>About</h1>
        <hr />
        <h2>Zhigly is a hub where you can connect with your college campus.</h2>
        <p>Zhigly allows you to look for items you'd like to buy or sell, browse upcoming on-campus events, search for jobs/internships/opportunities, look for housing, and much more.</p>
        <p>Zhigly is made with love in New York by a team of dedicated college students. If you would like to contact us, use our contact page, found below. We use your feedback and suggestions to further improve our service so we'd love to hear what you have to say!</p>
        <p>We hope you enjoy what we have to offer and find it useful to you!</p>
        <p class="love">With much love,</p>
        <p class="team">The Zhigly Team</p>
    </div>
</asp:Content>
