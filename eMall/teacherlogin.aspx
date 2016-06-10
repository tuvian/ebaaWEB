<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true" CodeBehind="teacherlogin.aspx.cs" Inherits="eMall.teacherlogin" %>

<%@ MasterType VirtualPath="~/mySkool.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>School
                    <asp:DropDownList ID="ddlSearchSchool" runat="server" />
                </td>
                <td>Search By
                </td>
                <td style="width: 110px;">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" Width="100">
                        <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="Code">Code</asp:ListItem>
                        <asp:ListItem Value="Name">Name</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
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
                        DataKeyNames="id,school_id,code,name,mobile,email,username,password,type,teacher_id">
                        <%-- OnRowEditing="GridItems_RowEditing"
                        OnPageIndexChanging="GridItems_PageIndexChanging"
                        OnRowDeleting="GridItems_RowDeleting" 
                        OnRowDataBound="GridItems_RowDataBound"
                        --%>
                        <RowStyle BackColor="#EFF3FB" Width="720" />
                        <Columns>
                            <asp:BoundField DataField="code" HeaderText="Teacher's ID">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="Name">
                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="username" HeaderText="UserName">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="password" HeaderText="Password">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="Email">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mobile" HeaderText="Mobile">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
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
                <td style="border: solid 1px #E8E8E8; width: 55%;">
                    <asp:UpdatePanel ID="upschoollogin" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label1" Text="School Code"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSchoolCode" runat="server" CssClass="adminCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSchoolCode"
                                            ErrorMessage="*" ValidationGroup="items" ToolTip="Please select a School" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label3" Text="Teacher ID"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlTeacherCode" runat="server" CssClass="adminCombo" AutoPostBack="true" OnSelectedIndexChanged="ddTeacherCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTeacherCode"
                                            ErrorMessage="*" ValidationGroup="items" ToolTip="Please select a Teacher Code" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label2" Text="Teacher's Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtTeacherName" ValidationGroup="items" MaxLength="40" CssClass="adminText" ReadOnly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label15" Text="Mobile"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtMobile" ValidationGroup="items" MaxLength="40" ReadOnly="true"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMobile"
                                            ErrorMessage="*" ToolTip="Please enter Mobile " ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label12" Text="Email"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtEmail" ValidationGroup="items" MaxLength="20" ReadOnly="true"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                            ErrorMessage="*" ToolTip="Please enter email address" ValidationGroup="items" />
                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                            ControlToValidate="txtEmail" ToolTip="Please enter valid email" ValidationGroup="items"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label8" Text="UserName"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtUserName" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqusername" runat="server" ControlToValidate="txtUserName"
                                            ErrorMessage="*" ToolTip="*" ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label7" Text="Password"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtPassword" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPassword"
                                            ErrorMessage="*" ToolTip="*" ValidationGroup="items" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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

    <asp:HiddenField ID="hdItemID" runat="server" Value="" />

    <script type="text/javascript">
        function ValidateForm() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlSchoolCode").value == "") {
                alert('Please click edit a Teacher to update username/password');
                return false;
            }
        }
    </script>

</asp:Content>
