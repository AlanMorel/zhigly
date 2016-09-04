<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Credits.aspx.cs" Inherits="Zhigly.Credits" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Credits</title>
    <link rel="stylesheet" type="text/css" href="/Styles/CreditsStyle.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image runat="server" ImageUrl="~/Images/Banners/credits_banner.png" CssClass="banner" AlternateText="Credits Banner"/>
    <div class="container">
        <h1>Credits</h1>
        <hr />
        <h2>Take advantage of additional features using credits!</h2>
        <div class="credit-banner1">
            <div class="credit-text1">Post anonymously.</div>
        </div>
        <p>When you enable anonymity, your name gets omitted from the listing. This feature is really useful in many cases, and you're free to decide when those cases are!</p>
        <p>Anonymity uses 5 credits per day.</p>
        <div class="credit-banner2">
            <div class="credit-text2">Boost your listing to the top.</div>
        </div>
        <p>When you boost your listing, a number of things happen. Your listing gets a golden background and star, ensuring it pops out. The listing is also shown at the top of whatever category it belongs to. Also, your listing gets additional visibility on the front page if your listing proves to be popular!</p>
        <p>Boosting your listing uses 10 credits per day.</p>
        <div class="credit-banner3">
            <div class="credit-text3">Support us.</div>
        </div>
        <p>Since we run no ads while providing a free service, this is our only source of revenue. Purchasing credits is an easy and direct way to help us cover server costs and expand further.</p>
        <p>We hope that these features are awesome while keeping the service useful without credits.</p>
        <div class="credit-banner4">
            <div class="credit-text4">
                <div class="button-container">
                    <asp:HyperLink ID="PurchaseCredits" runat="server" NavigateUrl="~/Home.aspx" CssClass="button">Purchase Credits</asp:HyperLink>
                </div>
            </div>
        </div>
        <p>If you would like to purchase credits to use for your listings, click above.</p>
        <p>Thank you!</p>
    </div>
</asp:Content>
