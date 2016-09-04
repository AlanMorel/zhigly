<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Zhigly.Settings" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Account Settings</title>
    <link rel="stylesheet" type="text/css" href="/Styles/Sections.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/SettingsStyle.css" />
    <script type="text/javascript" src="/JS/Validation.js"></script>
    <script src="/JS/Settings.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image ID="Banner" runat="server" CssClass="banner" ImageUrl="~/Images/Banners/settings_banner.png" AlternateText="Settings Banner"/>
    <div class="container">
        <div class="left" id="LeftSection" runat="server">
            <h3 class="left-header" runat="server" id="LeftHeader"></h3>
        </div>
        <div class="right" id="RightSection" runat="server">
            <div id="RightHeader" class="right-header" runat="server"></div>
            <div id="Default" class="default" runat="server">
                <p>Welcome to your settings page.</p>
                <p>If you would like to change some basic information like your name or contact information, select on basic information.</p>
                <p>If you would like to change your password, select on change password.</p>
                <p>If you would like to view your all your listings, both past and present, select on listings.</p>
            </div>
            <div id="Info" class="default" runat="server">
                <div>
                    <label>School:</label>
                    <asp:TextBox ID="SchoolTextbox" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label>Email:</label>
                    <asp:TextBox ID="EmailTextbox" runat="server" TextMode="Email"></asp:TextBox>
                </div>
                <div>
                    <label id="PrimaryNameLabel" runat="server" ClientIDMode="Static">First Name:</label>
                    <asp:TextBox ID="PrimaryNameTextbox" runat="server" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div>
                    <label id="SecondaryNameLabel" runat="server" >Last Name:</label>
                    <asp:TextBox ID="SecondaryNameTextbox" runat="server" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div>
                    <label>Phone Number:</label>
                    <asp:TextBox ID="PhoneTextbox" runat="server" TextMode="Phone" ClientIDMode="Static"></asp:TextBox>
                </div>
                <label class="error-label" runat="server" id="InfoError" ClientIDMode="Static"></label>
                <asp:Button CssClass="button" ID="Button1" runat="server" Text="Save Information" OnClick="saveButton_Click" />
            </div>
            <div id="ChangePassword" class="change-password" runat="server">
                <div>
                    <label>Current Password:</label>
                    <asp:TextBox ID="CurrentPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <label>New Password:</label>
                    <asp:TextBox ID="NewPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <label>Confirm Password:</label>
                    <asp:TextBox ID="ConfirmPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <label class="ErrorLabel" runat="server" id="PasswordError"></label>
                <asp:Button class="button" ID="ChangePasswordButton" runat="server" Text="Change Password" OnClick="changePassword_Click" />
            </div>
            <div id="Listings" class="listings" runat="server"></div>
            <div class="bottom-settings">
                <asp:HyperLink runat="server" ImageUrl="~/Images/Settings/info.png" NavigateUrl="~/Settings/info"></asp:HyperLink>
                <asp:HyperLink runat="server" ImageUrl="~/Images/Settings/listings.png" NavigateUrl="~/Settings/listings"></asp:HyperLink>
                <asp:HyperLink runat="server" ImageUrl="~/Images/Settings/change_password.png" NavigateUrl="~/Settings/password"></asp:HyperLink>
                <p class="icon-text">Basic Information</p>
                <p class="icon-text">My Listings</p>
                <p class="icon-text">Change Password</p>
            </div>
        </div>
    </div>
</asp:Content>