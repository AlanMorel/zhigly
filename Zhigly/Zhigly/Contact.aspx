<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Zhigly.Contact" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Zhigly - Contact</title>
    <link rel="stylesheet" type="text/css" href="/Styles/ContactStyle.css" />
    <script type="text/javascript" src="/JS/Validation.js"></script>
    <script type="text/javascript" src="/JS/Contact.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image runat="server" ImageUrl="~/Images/Banners/contact_banner.png" CssClass="banner" AlternateText="Zhigly Contact Banner"/>
    <div class="container">
        <div class="contact" id="ContactForm" runat="server">
            <h1>Contact</h1>
            <hr />
            <p>Whether you have any questions or just want to give us your opinion on anything, we are always here to listen.</p>
            <p>You may reach out to us via any of our social media accounts, or send us a direct message by filling the form below!</p>
            <p>We look forward to hearing from you. We will reply back as soon as possible!</p>
            <a href="http://facebook.com/zhigly"><div class="facebook">Like us on Facebook.</div></a>
            <a href="http://twitter.com/zhigly"><div class="twitter">Follow us on Twitter.</div></a>
            <a href="https://plus.google.com/u/0/113590686498071198241"><div class="plus">Follow us on Google+.</div></a>
            <div class="message">Or send us a message!</div>
            <div class="form-element">
                <label>Name:</label><asp:TextBox ID="Name" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="form-element">
                <label>Email:</label><asp:TextBox ID="Email" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="form-element">
                <label>Reason:</label>
                <asp:DropDownList ID="Reason" runat="server">
                    <asp:ListItem Text="General" Value="General"></asp:ListItem>
                    <asp:ListItem Text="Help" Value="Help"></asp:ListItem>
                    <asp:ListItem Text="Bug/Glitch" Value="Bug/Glitch"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-element">
                <label>Message:</label><asp:TextBox ID="Message" Rows="5" TextMode="multiline" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div>
                <label runat="server" id="ErrorLabel" class="error-label" clientidmode="Static"></label>
            </div>
            <div class="form-element">
                <asp:Button ID="SendMessage" runat="server" Text="Send Message" CssClass="button" OnClick="SendMessage_Click" />
            </div>
        </div>
        <div class="sent" id="Sent" runat="server">
            <h1>Thank You</h1>
            <hr />
            <p>We have recieved your message and will respond back as soon as we can to the email you have provided!</p>
        </div>
    </div>
</asp:Content>
