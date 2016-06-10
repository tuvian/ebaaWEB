<%@ Page Language="C#" MasterPageFile="~/ItemMaster.Master" AutoEventWireup="true"
    Title="Ebaa" CodeBehind="Default.aspx.cs"
    Inherits="eMall.WebForm1234" %>

<%@ MasterType VirtualPath="~/ItemMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 170px;
            height: 21px;
        }
        .style2
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <div style="background-color: #FFFFFF;">
            <div id="contentarea">
                <div id="loginbox" style="margin: 150px; margin-left: 250px;">
                    <table class="loginTable" cellspacing="0">
                        <tr>
                            <td class="style1">
                                <span class="loginLable">User Name</span>
                            </td>
                            <td class="style2">
                                <%--<asp:TextBox ID="txtUserName" runat="server" CssClass="loginTextBox"></asp:TextBox>--%>
                                <asp:TextBox ID="txtUserName" runat="server" Text="a" CssClass="loginTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="loginTD">
                                <span class="loginLable">Password</span>
                            </td>
                            <td class="loginTD">
                                <asp:TextBox ID="txtPassword" TextMode="Password" Text="a" runat="server" CssClass="loginTextBox"  ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 1px 0px 0px 3px;">
                            </td>
                            <td>
                                <asp:Button ID="btnLogin" runat="server" Text="Log In" CssClass="buttonStyle" 
                                    onclick="btnLogin_Click1" OnClientClick="return validate();"  />


                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <span runat="server" id="errorSpan" class="errorMessage"></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <%--    <script type="text/javascript" charset="utf-8" src="SlideShow/js/mootools-1.2.1-core-yc.js"></script>

    <script type="text/javascript" charset="utf-8" src="SlideShow/js/mootools-1.2.2.2-more.js"></script>

    <script type="text/javascript" charset="utf-8" src="SlideShow/js/Fx.MorphList.js"></script>

    <script type="text/javascript" charset="utf-8" src="SlideShow/js/BarackSlideshow.js"></script>

    <script type="text/javascript" charset="utf-8" src="SlideShow/js/demo.js"></script>--%>
    <script type="text/javascript">
        function validate() {
            if (document.getElementById('<%=txtUserName.ClientID%>').value == "") {
                document.getElementById('<%=errorSpan.ClientID%>').innerHTML = "Please enter username ";
                return false
            }
            if (document.getElementById('<%=txtPassword.ClientID%>').value == "") {
                document.getElementById('<%=errorSpan.ClientID%>').innerHTML = "Please enter password ";
                return false
            }
            return true;
        }
    </script>
</asp:Content>
