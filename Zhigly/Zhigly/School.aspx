<%@ Page Title="" Language="C#" MasterPageFile="~/Primary.Master" AutoEventWireup="true" CodeBehind="School.aspx.cs" Inherits="Zhigly.School" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Styles/Sections.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/SchoolStyle.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/Tooltip.css" />
    <script src="/JS/School.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Image ID="Banner" runat="server" CssClass="banner" AlternateText="School Banner"/>
    <div class="container">
        <div class="left" id="LeftSection" runat="server">
            <h3 class="left-header" runat="server" id="LeftHeader"></h3>
        </div>
        <div class="SubcategorySection"></div>
        <div class="right" id="RightSection" runat="server">
            <div id="ListingsHeader" class="right-container" runat="server">
                <div id="ListingsSection" class="right-header" runat="server"></div>
                <div id="PageNumber" class="page-number" runat="server"></div>
                <div id="Subcategories" runat="server"></div>
            </div>
        </div>
    </div>
    <script src="/JS/Tooltip.js"></script>
</asp:Content>
