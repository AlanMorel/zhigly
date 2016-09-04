<%@ Page Title="" Language="C#" MasterPageFile="~/Secondary.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Zhigly.Register" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Register</title>
    <link rel="stylesheet" type="text/css" href="/Styles/RegisterStyle.css" />
    <script type="text/javascript" src="/JS/Validation.js"></script>
    <script type="text/javascript" src="/JS/Register.js"></script>
    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="header">
        <asp:HyperLink ID="HomeHyperLink" runat="server" NavigateUrl="~/" CssClass="logo" ImageUrl="~/Images/Logo/white_logo.png"></asp:HyperLink>
        <div class="title">Registration</div>
    </div>
    <label>I am:</label>
    <div class="type">
        <asp:RadioButtonList ID="TypeRadioButtonList" runat="server" ClientIDMode="Static">
            <asp:ListItem Text="A Student" Value="0" Selected="True"></asp:ListItem>
            <asp:ListItem Text="A Faculty member" Value="1"></asp:ListItem>
            <asp:ListItem Text="A Staff member" Value="2"></asp:ListItem>
            <asp:ListItem Text="Representing an Organization, Club, Group, or other Institution" Value="3"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
        <label>Email:</label>
        <asp:TextBox ID="EmailTextbox" runat="server" CssClass="email" Width="156px" ClientIDMode="Static"></asp:TextBox>
        <asp:GroupedDropDownList ID="EmailDropdown" runat="server"></asp:GroupedDropDownList>
    </div>
    <div>
        <label id="PrimaryName">First Name:</label>
        <asp:TextBox ID="PrimaryNameTextbox" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div>
        <label id="SecondaryName">Last Name:</label>
        <asp:TextBox ID="SecondaryNameTextbox" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div>
        <label>Password:</label>
        <asp:TextBox ID="PasswordTextbox" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div>
        <label>Confirm:</label>
        <asp:TextBox ID="ConfirmPasswordTextbox" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div id="RecaptchaDiv" runat="server"></div>
    <label class="error-label" runat="server" id="ErrorLabel" clientidmode="Static"></label>
    <asp:Button class="button" ID="RegisterButton" runat="server" Text="Register" OnClick="registerButton_Click" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <asp:HyperLink ID="FooterHomeHyperLink" runat="server" NavigateUrl="~/Home.aspx">Home</asp:HyperLink>
    /
    <asp:HyperLink ID="FooterLoginHyperLink" runat="server" NavigateUrl="~/Login.aspx">Already have an account?</asp:HyperLink>
</asp:Content>
