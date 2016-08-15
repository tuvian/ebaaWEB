<%@ Page Title="" Language="C#" MasterPageFile="~/mastertest.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="eMall.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=tvTest] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            });
        })

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table width="100%">
        <tr>
            <%-- <td align="left" class="adminLabel">
                <asp:Label runat="server" ID="Label4" Text="School Code"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlSchoolCode" runat="server" CssClass="adminCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolCode_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSchoolCode"
                    ErrorMessage="Select a school" ValidationGroup="items" ToolTip="Please select a School" InitialValue="0" />
            </td>
        </tr>
        <tr>
            <td align="left" class="adminLabel">
                <asp:Label runat="server" ID="Label3" Text="Teachers In the Class :"></asp:Label>
            </td>--%>
            <td align="left">
                <%-- <div style="width: 200px; height: 150px; padding: 2px; overflow: auto; border: 1px solid #ccc;">
                    <asp:CheckBoxList class="BodyTxt" ID="chkClassForTeacher" runat="server"></asp:CheckBoxList>
                </div>
                <asp:Label ID="lblSelectedClassForTeacher" runat="server"></asp:Label>--%>
                <asp:TreeView runat="server" ID="tvTest" ShowCheckBoxes="Leaf" OnTreeNodeCheckChanged="tvTest_TreeNodeCheckChanged" />
                <asp:Button runat="server" Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" />

                <%--<asp:TreeView ID="tvwRegionCountry" runat="server" ShowCheckBoxes="All" ExpandDepth="0"   AfterClientCheck="CheckChildNodes();" PopulateNodesFromClient="true" ShowLines="true" ShowExpandCollapse="true" OnTreeNodeCheckChanged="tvwRegionCountry_TreeNodeCheckChanged"
                                                    onclick="OnTreeClick(event)">
                                                </asp:TreeView>--%>

            </td>
        </tr>
    </table>









    <%--<div class="adminEditContent">
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
    </div>--%>
</asp:Content>
