<%@ Page Title="" Language="C#" MasterPageFile="~/ItemMaster.Master" AutoEventWireup="true"
    CodeBehind="adminhome.aspx.cs" Inherits="eMall.adminhome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div align="center">
        <div style="background-color: #FFFFFF;">
            <div id="contentarea">
                <div id="loginbox" style="margin: 150px; margin-left: 250px;">
                    <table class="loginTable" cellspacing="0">
                        <tr>
                            <td class="loginTD">
                                <span class="loginLable"> <asp:Label ID="lblWelcome" runat="server" > </span>
                            </td>                            
                        </tr>                       
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
