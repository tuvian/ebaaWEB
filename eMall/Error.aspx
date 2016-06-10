<%@ Page Language="C#" MasterPageFile="~/TopLeft.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs"
    Inherits="eMall.Error" Title="Untitled Page" %>
<%@OutputCache Duration="600" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Image ID="Image1" ImageUrl="~/images/error.jpg" runat="server" />
</asp:Content>
