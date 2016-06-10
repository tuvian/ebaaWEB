<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="eMall.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>DeviceID</td>
                <td>
                    <asp:TextBox ID="txtDeviceID" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Message</td>
                <td>
                    <asp:TextBox ID="txtMsg" runat="server" /></td>
            </tr>
            <tr>
                <td>SenderID</td>
                <td>
                    <asp:TextBox ID="txtSenderID" runat="server" /></td>
            </tr>
            <tr>
                <td>Google App ID</td>
                <td>
                    <asp:TextBox ID="txtAppID" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSebd" Text="SEND_Not" OnClick="btnSebd_Click" /></td>
            </tr>
        </table>
    </div>

    <div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>RegID : (Registration Id created by Android App i.e. DeviceId.) </td>
                <td>
                    <asp:TextBox ID="txtRegID_2" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Message</td>
                <td>
                    <asp:TextBox ID="txtMsg_2" runat="server" /></td>
            </tr>
            <tr>
                <td>SenderID (Project ID created in Google project.)</td>
                <td>
                    <asp:TextBox ID="txtSender_2" runat="server" /></td>
            </tr>
            <tr>
                <td>Application ID (API Key created in Google project )</td>
                <td>
                    <asp:TextBox ID="txtAppID_2" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="btnSend_2" Text="SEND_Not_2" OnClick="btnSend_2_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
