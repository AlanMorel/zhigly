<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="Zhigly.Listing" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Styles/ListingStyle.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/Sections.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/LightBox.css" />
    <script type="text/javascript" src="/JS/Listing.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <script src="/JS/Facebook.js"></script>
    <script src="/JS/Twitter.js"></script>
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <asp:Image ID="Banner" runat="server" CssClass="banner" AlternateText="School Banner"/>
    <div class="container">
        <asp:HiddenField ID="ListingId" runat="server" ClientIDMode="Static" />
        <div class="left" id="LeftSection" runat="server">
            <h3 class="left-header" runat="server" id="LeftHeader"></h3>
        </div>
        <div>
            <div class="listing-notice owner" id="Owner" runat="server"></div>
            <div class="listing-notice report-container">
                <div id="ReportContainer">
                    <label class="report-prompt">Why are you reporting this listing?</label>
                    <asp:DropDownList ID="Reason" runat="server" CssClass="report-dropdown" ClientIDMode="Static"></asp:DropDownList>
                    <asp:Button ID="ReportButton" runat="server" Text="Report" ClientIDMode="Static" />
                </div>
                <label class="report-result">Why are you reporting this listing?</label>
            </div>
            <div class="right" id="RightSection" runat="server">
                <div class="report-label" runat="server" ID="Report"></div>
                <h2 runat="server" id="AdTitle"></h2>
                <div id="AdBody" runat="server">
                    <div class="sub-header">
                        <p runat="server" id="PostDate" class="post-date"></p>
                        <p runat="server" id="ViewCount" class="view-count"></p>
                    </div>
                    <hr />
                    <p runat="server" id="Description" class="description"></p>
                    <div class="images" id="Images" runat="server"></div>
                    <p class="share">Share this listing</p>
                    <hr />
                </div>
            </div>
            <div class="comments" id="CommentSection" runat="server"></div>
        </div>
    </div>
    <script src="/JS/LightBox.js"></script>
</asp:Content>
