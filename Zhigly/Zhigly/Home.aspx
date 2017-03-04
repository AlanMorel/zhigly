<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Zhigly.Home" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Listings for College Communities</title>
    <link rel="stylesheet" type="text/css" href="/Styles/HomeStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div id="LoggedOut" runat="server">
        <div class="video">
            <video id="bg" src="http://webm.land/media/W46Y.webm" autoplay loop muted></video>
        </div>
        <div class="homepage-logo2">
            <div class="homepagelogo"></div>
        </div>
        <div class="description">Zhigly is a hub where you can connect with your college campus.</div>
        <div class="home-section home-section1">
            <div class="section-text section-text1">Connect with your school</div>
            <div class="section-image section-image1"></div>
        </div>
        <div class="home-section home-section2">
            <div class="section-text section-text2">Browse listings</div>
            <div class="section-image section-image2"></div>
        </div>
        <div class="home-section home-section3">
            <div class="section-text section-text3">Discover events</div>
            <div class="section-image section-image3"></div>
        </div>
        <div class="home-section home-section4">
            <div class="section-text section-text4">What are you waiting for?</div>
            <div class="section-image section-image4">
                <div class="button-container">
                    <asp:HyperLink runat="server" NavigateUrl="~/Register.aspx" CssClass="button">Register for an Account</asp:HyperLink>
                    <div class="register-text">It's free!</div>
                </div>
            </div>
        </div>
    </div>
    <div id="LoggedIn" runat="server">
        <div class="homepage"></div>
        <div class="description2" id="SchoolContainer" runat="server">
            <div id="SchoolName" class="school-name" runat="server"></div>
            <div id="SchoolFilters" class="filters" runat="server"></div>
        </div>
        <div class="columns-container" id="Listings" runat="server">
            <div class="column-container" id="RecentListings" runat="server">
                <div class="column-title" id="RecentListingsTitle" runat="server">Recent Listings</div>
            </div>
            <div class="column-container" id="PopularListings" runat="server">
                <div class="column-title" id="PopularListingsTitle" runat="server">Popular Listings</div>
            </div>
            <div class="column-container" id="PopularBoostedListings" runat="server">
                <div class="column-title" id="PopularBooestedListingsTitle">Popular Boosted Listings</div>
            </div>
            <div class="column-container" id="RecentBlogPosts" runat="server">
                <div class="column-title">Recent Blog Posts</div>
            </div>
        </div>
    </div>
</asp:Content>
