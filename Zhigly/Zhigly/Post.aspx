<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="Zhigly.Post" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Post a Listing</title>
    <link rel="stylesheet" type="text/css" href="/Styles/PostStyle.css" />
    <script type="text/javascript" src="/JS/Validation.js"></script>
    <script src="/JS/Post.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image ID="Banner" runat="server" ImageUrl="~/Images/Banners/post_banner.png" CssClass="banner" AlternateText="Post Listing Banner"/>
    <div class="container">
        <h1>Post a Listing</h1>
        <div class="error-div">
            <label class="error-label" runat="server" id="ErrorLabel" clientidmode="Static"></label>
        </div>
        <hr />
        <div class="title">Title</div>
        <div>
            <asp:TextBox ID="TitleTextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="title">Category</div>
        <div>
            <asp:GroupedDropDownList ID="GroupedDropDownList" runat="server" />
        </div>
        <div class="title">Description</div>
        <div>
            <asp:TextBox ID="DescriptionTextBox" runat="server" Rows="5" TextMode="multiline" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="title">Duration</div>
        <div>
            <asp:DropDownList ID="DurationDropDownList" runat="server" ClientIDMode="Static"></asp:DropDownList>
        </div>
        <div class="title">Options</div>
        <div class="option-container" id="anonymity">
            <div class="option-icon disabled-anonymity"></div>
            <div class="option-description">
                <div class="option-title">This listing is not being posted anonymously.</div>
                <div class="option-message">Click here to toggle anonymity. Listings posted anonymously hide your name from the public.</div>
                <div class="option-cost">Enabling this costs 5 credits per day.</div>
            </div>
            <asp:CheckBox ID="AnonymityCheckbox" ClientIDMode="Static" runat="server" class="checkbox" />
        </div>
        <div class="option-container" id="boost">
            <div class="option-icon disabled-boost"></div>
            <div class="option-description">
                <div class="option-title">This listing is not being boosted.</div>
                <div class="option-message">Click here to toggle boost. Boosted listings are shown closer to the top and are given a custom golden background.</div>
                <div class="option-cost">Enabling this costs 10 credits per day.</div>
            </div>
            <asp:CheckBox ID="BoostCheckbox" ClientIDMode="Static" runat="server" class="checkbox" />
        </div>
        <div class="title">Cost</div>
        <div class="total">
            <div class="costs-container">
                <div class="costs-title">
                    Anonymity:
                </div>
                <div class="costs-amount" id="AnonymityCost">
                    0 Credits
                </div>
            </div>
            <div class="costs-container">
                <div class="costs-title">
                    Boost:
                </div>
                <div class="costs-amount" id="BoostCost">
                    0 Credits
                </div>
            </div>
            <div class="costs-container total-container">
                <div class="costs-title">
                    Total:
                </div>
                <div class="costs-amount" id="TotalCost">
                    Free
                </div>
            </div>
            <div class="user-credits">
                You have <span id="UserCredits" runat="server"></span>credits.
            </div>
            <div class="need">
                <asp:HyperLink runat="server" NavigateUrl="~/Credits.aspx">Need some more?</asp:HyperLink>
            </div>
        </div>
        <div id="FileUploads" runat="server">
        </div>
        <div class="post-div">
            <asp:Button ID="PostListing" runat="server" Text="Post Listing" CssClass="button post" OnClick="PostListing_Click" />
            <asp:Button ID="AddAnotherImage" runat="server" Text="Add Image" CssClass="button add" OnClientClick="return false;" />
        </div>
    </div>
</asp:Content>
