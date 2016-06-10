<%@ Page Language="C#" MasterPageFile="~/TopLeft.Master" AutoEventWireup="true" CodeBehind="404Error.aspx.cs"
    Inherits="eMall._04Error" Title="Untitled Page" %>
<%@OutputCache Duration="600" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Image ImageUrl="~/images/404.jpg" runat="server" />
</asp:Content>
