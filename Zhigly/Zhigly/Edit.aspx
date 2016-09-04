<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Zhigly.Edit" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Edit Listing</title>
    <link rel="stylesheet" type="text/css" href="/Styles/EditStyle.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/LightBox.css" />
    <script type="text/javascript" src="/JS/Validation.js"></script>
    <script src="/JS/Edit.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Image ID="Banner" runat="server" ImageUrl="~/Images/Banners/edit_banner.png" CssClass="banner" AlternateText="Edit Banner"/>
    <div class="container">
        <h1>Edit Listing</h1>
        <div class="error-div">
            <label class="error-label" runat="server" id="ErrorLabel" ClientIDMode="Static"></label>
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
        <div class="title">Expiration</div>
        <div class="expiration-text" id="ExpirationText" runat="server"> </div>
        <div class="title">Options</div>
        <div id="Visibility" runat="server">
            <div class="option-text" id="VisiblityText" runat="server"></div>
            <div id="Spy" runat="server"></div>
        </div>
       <div id="Boost" runat="server">
            <div class="option-text" id="BoostText" runat="server"></div>
            <div id="Star" runat="server"></div>
        </div>
        <div id="Images" runat="server"></div>
        <div class="post-div" runat="server" id="EditButtons">
            <asp:Button ID="SaveListing" runat="server" Text="Save Changes" CssClass="button save" OnClick="SaveListing_Click" />
            <asp:Button ID="DeleteListing" runat="server" Text="Delete Listing" CssClass="button delete" OnClientClick="return false;" />
            <asp:Button ID="AddAnotherImage" runat="server" Text="Add Image" CssClass="button add" OnClientClick="return false;" />
        </div>
        <div class="prompt-div" id="DeletePrompt" runat="server">
            Are you sure you want to delete this listing?
            <asp:Button ID="Yes" runat="server" Text="Yes" CssClass="prompt-button" OnClick="Yes_Click" />
            <asp:Button ID="No" runat="server" Text="No" CssClass="prompt-button no" OnClientClick="return false;" />
        </div>
    </div>
    <script src="/JS/LightBox.js"></script>
</asp:Content>
