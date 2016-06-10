<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true" CodeBehind="bus.aspx.cs" Inherits="eMall.bus" %>

<%@ MasterType VirtualPath="~/mySkool.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>School Code
                    <asp:DropDownList ID="ddlSearchSchoolCodes" runat="server">
                    </asp:DropDownList>
                </td>
                <%--<td>Status
                    <asp:DropDownList ID="ddlSearchStatus" runat="server">
                        <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="2">In Active</asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                <%--<td>Search By
                </td>
                <td style="width: 110px;">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" Width="100">
                        <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="Title">Code</asp:ListItem>
                        <asp:ListItem Value="Name">Name</asp:ListItem>
                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                        <asp:ListItem Value="Email">Email</asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                <td>Bus NO/ Rout
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="adminSearchBox" />
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="buttonStyle" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>


    <div>
        <table border="2" class="adminGridListTable">
            <tr>
                <td>
                    <asp:GridView runat="server" ID="GridItems" AutoGenerateColumns="False" PageSize="10"
                        Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333"
                        GridLines="None" Font-Size="13px" Font-Names="Verdana" OnPageIndexChanging="GridItems_PageIndexChanging"
                        OnRowDeleting="GridItems_RowDeleting" OnRowDataBound="GridItems_RowDataBound" OnRowCommand="GridItems_RowCommand"
                        DataKeyNames="id,school_id,bus_number,driver,driver_mobile,rout,driver_id">
                        <%-- OnRowEditing="GridItems_RowEditing"
                        OnPageIndexChanging="GridItems_PageIndexChanging"
                        OnRowDeleting="GridItems_RowDeleting" 
                        OnRowDataBound="GridItems_RowDataBound"
                        --%>
                        <RowStyle BackColor="#EFF3FB" Width="720" />
                        <Columns>
                            <asp:BoundField DataField="bus_number" HeaderText="Bus Number">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rout" HeaderText="Rout Description">
                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="driver_mobile" HeaderText="Mobile" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>--%>
                            <%--<asp:BoundField DataField="toDate" HeaderText="To Date" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Status">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>--%>
                            <%--  <asp:BoundField DataField="qualification" HeaderText="Qualification">
                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>--%>
                            <asp:ButtonField CommandName="Change" ButtonType="Button" HeaderText="Edit" Text="Edit">
                                <ControlStyle CssClass="buttonStyle" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:ButtonField>
                            <%-- <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Delete">
                                <ControlStyle CssClass="buttonStyle" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:CommandField>--%>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="deleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="buttonStyle"
                                        OnClientClick="return confirm('Are you sure you want to delete this entry?');" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ControlStyle CssClass="buttonStyle" />
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#4b6c9e" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>

    <div class="adminEditContent">
        <table>
            <tr>
                <td>
                    <asp:ValidationSummary
                        ID="valSum"
                        ValidationGroup="items"
                        DisplayMode="BulletList"
                        ShowSummary="false"
                        runat="server"
                        HeaderText="Please enter below valid details"
                        Font-Names="verdana"
                        Font-Size="09" />
                    <asp:Label runat="server" ID="lblError" CssClass="errorMessage" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="border: solid 1px #E8E8E8; width: 55%;">
                    <asp:UpdatePanel ID="upschoollogin" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label3" Text="School Code"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSchoolCode" runat="server" CssClass="adminCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSchoolCode"
                                            ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a School" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label4" Text="Bus Number"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtBusNumber" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBusNumber"
                                            ErrorMessage="* Required" ToolTip="Please enter Bus Number " ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblQstnNo" Text="Rout Description"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtRout" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqName" runat="server" ControlToValidate="txtRout"
                                            ErrorMessage="* Required" ToolTip="Please enter Rout Description" ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label5" Text="Driver"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDriver" runat="server" CssClass="adminCombo">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDriver"
                                            ErrorMessage="*" ValidationGroup="items" ToolTip="Please select driver" InitialValue="0" />
                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label1" Text="Driver Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDriver" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDriver"
                                            ErrorMessage="* Required" ToolTip="Please enter Bus Number " ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label2" Text="Mobile Number"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtMobile" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMobile"
                                            ErrorMessage="* Required" ToolTip="Please enter Bus Number " ValidationGroup="items" />
                                    </td>
                                </tr>--%>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 10px;"></td>
                <td style="border: solid 1px #E8E8E8; vertical-align: top; width: 45%;">
                    <div style="width: auto; vertical-align: top;">
                        <%--<uc:commonFileUploader runat="server" ID="ucFileUploader"/>--%>
                    </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="padding-left: 180px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="items" CausesValidation="true"
                        CssClass="buttonStyle" OnClick="btnSave_Click" Width="100" OnClientClick="ValidateForm()" />
                </td>
                <td>
                    <asp:Button ID="btnAddNew" runat="server" Text="Add New" ValidationGroup="" OnClick="btnAddNew_Click"
                        CssClass="buttonStyle" Width="100" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdItemID" runat="server" />
    <script type="text/javascript">
        function ValidateForm() {
            return true;
        }
    </script>





</asp:Content>
