<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true" CodeBehind="school.aspx.cs" Inherits="eMall.school" %>

<%@ MasterType VirtualPath="~/mySkool.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />--%>
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
                <td>Package
                    <asp:DropDownList ID="ddlSearchPackage" runat="server" />
                </td>
                <td>Search By
                </td>
                <td style="width: 110px;">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" Width="100">
                        <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="Code">Code</asp:ListItem>
                        <asp:ListItem Value="Name">Name</asp:ListItem>
                        <asp:ListItem Value="Mobile">Nationality</asp:ListItem>
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
                        DataKeyNames="id,code,name,site_url,address,contact_person,mobile,email,phone,contact_address,nationality, package_id, register_date, expire_date, logo, status, notes, wilayath, waynumber">
                        <%-- OnRowEditing="GridItems_RowEditing"
                        OnPageIndexChanging="GridItems_PageIndexChanging"
                        OnRowDeleting="GridItems_RowDeleting" 
                        OnRowDataBound="GridItems_RowDataBound"
                        --%>
                        <RowStyle BackColor="#EFF3FB" Width="720" />
                        <Columns>
                            <asp:BoundField DataField="code" HeaderText="School's Code">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="Name">
                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mobile" HeaderText="Mobile">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="Email">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nationality" HeaderText="Nationality">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="package" HeaderText="Package">
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
                    <table width="100%">
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="lblQstnNo" Text="School ID"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtcode" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="rqName" runat="server" ControlToValidate="txtcode"
                                    ErrorMessage="*" ToolTip="Please enter School ID " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label2" Text="School's Name"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtName" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="*" ToolTip="Please enter School's Name " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label8" Text="Site URL"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtSiteURL" ValidationGroup="items" MaxLength="40"
                                    CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiteURL"
                                    ErrorMessage="*" ToolTip="Please enter Site URL " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label7" Text="Nationality"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtNationality" ValidationGroup="items" MaxLength="20"
                                    CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNationality"
                                    ErrorMessage="*" ToolTip="*" ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label11" Text="School's Address"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSchoolAddress" runat="server" MaxLength="400" TextMode="MultiLine"
                                    CssClass="adminTextLarge" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label9" Text="Wilayath"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtWilayath" runat="server" MaxLength="20"
                                    CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtWilayath"
                                    ErrorMessage="*" ToolTip="Please enter Wilayath " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label18" Text="Way Number"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtWayNymber" runat="server" MaxLength="20"
                                    CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWayNymber"
                                    ErrorMessage="*" ToolTip="Please enter Way Number " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label14" Text="Contact Person"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtContactPerson" ValidationGroup="items" MaxLength="40"
                                    CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtContactPerson"
                                    ErrorMessage="*" ToolTip="Please enter Contact Person's Name " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label15" Text="Mobile"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtMobile" ValidationGroup="items" MaxLength="40"
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
                                <asp:TextBox runat="server" ID="txtEmail" ValidationGroup="items" MaxLength="20"
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
                                <asp:Label runat="server" ID="Label5" Text="Phone"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtPhone" ValidationGroup="items" MaxLength="40"
                                    CssClass="adminText" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label3" Text="Contact Address"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtContactAddres" runat="server" MaxLength="400" TextMode="MultiLine"
                                    CssClass="adminTextLarge" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label1" Text="Package"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPackage" runat="server" CssClass="adminCombo">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPackage"
                                    ErrorMessage="*" ValidationGroup="items" ToolTip="Please select department" InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label16" Text="Register Date "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRegisterDate" runat="server" />
                                <ajax:CalendarExtender ID="CalendarExtender1ScriptManager" TargetControlID="txtRegisterDate" Format="dd/MM/yyyy" runat="server">
                                </ajax:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtRegisterDate"
                                    ErrorMessage="*" ToolTip="Please enter register date" ValidationGroup="items" />
                                <%--<asp:UpdatePanel ID="upregisterDate" runat="server">
                                    <ContentTemplate>
                                        Year
                                        <asp:DropDownList ID="ddlRegisterYear" runat="server" CssClass="adminComboNoWidth" Width="70" AutoPostBack="true" OnSelectedIndexChanged="ddlRegisterYear_SelectedIndexChanged" />
                                        Month
                                <asp:DropDownList ID="ddlRegisterMonth" runat="server" CssClass="adminComboNoWidth" Width="45" AutoPostBack="true" OnSelectedIndexChanged="ddlRegisterMonth_SelectedIndexChanged" />
                                        Day
                                <asp:DropDownList ID="ddlRegisterDay" runat="server" CssClass="adminComboNoWidth" Width="45" />--%>
                                        <%--<asp:TextBox runat="server" ID="txtRegisterDate" ValidationGroup="items" MaxLength="20"
                                    CssClass="adminText" ReadOnly="true" Text="2015-01-01" Visible="false"/>
                                <asp:RequiredFieldValidator ID="rqrgdate" runat="server" ControlToValidate="txtRegisterDate"
                                    ErrorMessage="*" ToolTip="*" ValidationGroup="items" />--%>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                                    ControlToValidate="txtRegisterDate" ValidationGroup="items" ErrorMessage="*" ToolTip="Date format in DD/MM/YYYY"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d$"></asp:RegularExpressionValidator>--%>
                                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server"
                                    ControlToValidate="txtRegisterDate" ErrorMessage="*" ToolTip="Please enter valid date"
                                    Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                                   <%-- </ContentTemplate>
                                    <Triggers>--%>
                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlRegisterYear"
                                            EventName="SelectedIndexChanged" />--%>
                                    <%--</Triggers>
                                </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label17" Text="Expire Date"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExpireDate" runat="server" />
                                <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtExpireDate" Format="dd/MM/yyyy" runat="server">
                                </ajax:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtExpireDate"
                                    ErrorMessage="*" ToolTip="Please enter expire date" ValidationGroup="items" />
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Calendar ID="cal" runat="server" />
                                        Year
                                <asp:DropDownList ID="ddlExpireYear" runat="server" CssClass="adminComboNoWidth" Width="70" AutoPostBack="true" OnSelectedIndexChanged="ddlExpireYear_SelectedIndexChanged" />
                                        Month
                                <asp:DropDownList ID="ddlExpireMonth" runat="server" CssClass="adminComboNoWidth" Width="45" AutoPostBack="true" OnSelectedIndexChanged="ddlExpireMonth_SelectedIndexChanged" />
                                        Day
                                <asp:DropDownList ID="ddlExpireDay" runat="server" CssClass="adminComboNoWidth" Width="45" />--%>

                                        <%--<asp:TextBox runat="server" ID="txtExpire" ValidationGroup="items" MaxLength="20"
                                    CssClass="adminText" ReadOnly="true" Text="2016-01-01" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExpire"
                                    ErrorMessage="*" ToolTip="*" ValidationGroup="items" />--%>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Red"
                                    ControlToValidate="txtExpire" ValidationGroup="items" ErrorMessage="*" ToolTip="Date format in DD/MM/YYYY"
                                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d$"></asp:RegularExpressionValidator>--%>
                                   <%-- </ContentTemplate>
                                    <Triggers>--%>
                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlRegisterYear"
                                            EventName="SelectedIndexChanged" />--%>
                                    <%--</Triggers>
                                </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px;"></td>
                <td style="border: solid 1px #E8E8E8; vertical-align: top; width: 45%;">
                    <div style="width: auto; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label4" Text="Upload Logo"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:FileUpload runat="server" ID="fuSchooLogo" />
                                    <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label10" Text="Image"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblItemImage" Font-Bold="true" Visible="false" /><br />
                                    <asp:Image runat="server" ID="imgItem" Width="180" Height="160" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label6" Text="Status"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label13" Text="Notes"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtNotes" runat="server" MaxLength="400" TextMode="MultiLine"
                                        CssClass="adminTextLarge" />
                                </td>
                            </tr>
                        </table>
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
            var email = document.getElementById('ctl00_ContentPlaceHolder1_txtEmail');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtcode").value == "") {
                alert('Please enter School ID');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtName").value == "") {
                alert('Please enter Name');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtSiteURL").value == "") {
                alert('Please enter Site URL');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtNationality").value == "") {
                alert('Please enter Nationality');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtWilayath").value == "") {
                alert('Please enter Wilayath');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtWayNymber").value == "") {
                alert('Please enter Way Number');
                return false;
            }

            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtContactPerson").value == "") {
                alert('Please enter name of Contact Person');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlPackage").value == "0") {
                alert('Please select a package');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtMobile").value == "") {
                alert('Please enter Mobile');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtEmail").value == "") {
                alert('Please enter Email');
                return false;
            }
            else if (!filter.test(document.getElementById("ctl00_ContentPlaceHolder1_txtEmail").value)) {
                alert('Please provide a valid email address');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtNationality").value == "") {
                alert('Please enter Nationality');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtRegisterDate").value == "") {
                alert('Please enter register date');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtExpireDate").value == "") {
                alert('Please enter expire date');
                return false;
            }
            else {
                return true;
            }
        }
    </script>

</asp:Content>
