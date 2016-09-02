<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true"
    CodeBehind="students.aspx.cs" Inherits="eMall.students" %>

<%@ Register TagName="commonFileUploader" TagPrefix="uc" Src="~/UserControls/CommonFileUploader.ascx" %>
<%@ MasterType VirtualPath="~/mySkool.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--   <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>
    <%--<div class="adminContentArea">
                <%--<asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>
    <div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>School Code
                    <asp:DropDownList ID="ddlSearchSchoolCodes" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Search By
                </td>
                <td style="width: 110px;">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" Width="100">
                        <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                        <%--<asp:ListItem Value="Code">StudentID</asp:ListItem>--%>
                        <asp:ListItem Value="Name">Name</asp:ListItem>
                        <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                        <asp:ListItem Value="Email">Email</asp:ListItem>
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
    <%--<asp:Panel ID="panelHeaderItems" runat="server" Width="750">--%>
    <%--</asp:Panel>--%>
    <%--<asp:Panel ID="panelItems" runat="server" Width="750">--%>
    <div>
        <table border="2" class="adminGridListTable">
            <tr>
                <td>
                    <asp:GridView runat="server" ID="GridItems" AutoGenerateColumns="False" PageSize="10"
                        Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333"
                        GridLines="None" Font-Size="13px" Font-Names="Verdana" OnPageIndexChanging="GridItems_PageIndexChanging"
                        OnRowDeleting="GridItems_RowDeleting" OnRowDataBound="GridItems_RowDataBound" OnRowCommand="GridItems_RowCommand"
                        DataKeyNames="id,student_id,school_id,class_id,class_std,class_division,first_name,mobile,email,present_address,permenant_address,nationality,wilayath,father_name,mother_name,contact_mobile,contact_email,gender,waynumber,middle_name,family_name,status,image_path,bus_id ">


                        <%-- OnRowEditing="GridItems_RowEditing"
                        OnPageIndexChanging="GridItems_PageIndexChanging"
                        OnRowDeleting="GridItems_RowDeleting" 
                        OnRowDataBound="GridItems_RowDataBound"
                        --%>
                        <RowStyle BackColor="#EFF3FB" Width="720" />
                        <Columns>
                            <asp:BoundField DataField="student_id" HeaderText="Student ID">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="class_std" HeaderText="Standard">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="class_division" HeaderText="Division">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="first_name" HeaderText="First Name">
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


                            <%-- </asp:BoundField>
                             <asp:BoundField DataField="qualification" HeaderText="Qualification">
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
    <%--</asp:Panel>
            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server" TargetControlID="panelItems"
                ExpandControlID="panelHeaderItems" SuppressPostBack="true" CollapseControlID="panelHeaderItems"
                Collapsed="false" ImageControlID="imgCollapse" CollapsedImage="~/images/arrow02.gif"
                ExpandedImage="~/images/arrow01.gif" ExpandDirection="Vertical" />--%>
    <%--<asp:Panel ID="panelItemSettingsHeader" runat="server">--%>
    <%--<div class="commonHeaderDiv">
                Settings</div>--%>
    <%-- </asp:Panel>
            <asp:Panel ID="panelItemSettings" runat="server">--%>
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
                                        <asp:Label runat="server" ID="Label1" Text="Standard"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="adminCombo" AutoPostBack="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClass"
                                            ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Std" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblStudentID" Text="Student ID"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtStudentID" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqStudentID" runat="server" ControlToValidate="txtStudentID"
                                            ErrorMessage="*" ToolTip="Please enter Student ID " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lbl" Text="Gender"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="adminCombo">
                                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqGender" runat="server" ControlToValidate="ddlGender"
                                            ErrorMessage="*" ValidationGroup="items" ToolTip="Please select Gender" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblFirstName" Text="First Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtFirstName" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqFirstName" runat="server" ControlToValidate="txtFirstName"
                                            ErrorMessage="*" ToolTip="Please enter First Name " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblMiddleName" Text="Middle Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtMiddleName" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqMiddleName" runat="server" ControlToValidate="txtMiddleName"
                                            ErrorMessage="*" ToolTip="Please enter Middle Name " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblFamilyName" Text="Family Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtFamilyName" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqFamilyName" runat="server" ControlToValidate="txtFamilyName"
                                            ErrorMessage="*" ToolTip="Please enter Family Name " ValidationGroup="items" />
                                    </td>
                                </tr>
                                <%--<tr>
                            <td align="left" class="adminLabel">Class</td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtStudentClass" ValidationGroup="items" MaxLength="40"
                                    CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="rqStudentClass" runat="server" ControlToValidate="txtStudentClass"
                                    ErrorMessage="*" ToolTip="Please enter Teacher's qualification " ValidationGroup="items" />
                            </td>
                        </tr>--%>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblMobile" Text="Mobile"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtMobile" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqMobile" runat="server" ControlToValidate="txtMobile"
                                            ErrorMessage="*" ToolTip="Please enter Mobile Number " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblWilayath" Text="Wilayath"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtWilayath" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqWilayath" runat="server" ControlToValidate="txtWilayath"
                                            ErrorMessage="*" ToolTip="Please enter Wilayath name " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblwaynumber" Text="Waynumber"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtwaynumber" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqwaynumber" runat="server" ControlToValidate="txtwaynumber"
                                            ErrorMessage="*" ToolTip="Please enter Waynumber " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblContactMobile" Text="Contact Mobile"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtContactMobile" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqcontactmobile" runat="server" ControlToValidate="txtMobile"
                                            ErrorMessage="*" ToolTip="Please enter Mobile Number " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblFatherName" Text="Father Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtFatherName" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqFatherName" runat="server" ControlToValidate="txtFatherName"
                                            ErrorMessage="*" ToolTip="Please enter Father Name  " ValidationGroup="items" />
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblMotherName" Text="Mother Name"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtMotherName" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqMotherName" runat="server" ControlToValidate="txtMotherName"
                                            ErrorMessage="*" ToolTip="Please enter Mother Name  " ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label12" Text="Email"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtEmail" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqEmail" runat="server" ControlToValidate="txtEmail"
                                            ErrorMessage="*" ToolTip="Please enter email address" ValidationGroup="items" />
                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                            ControlToValidate="txtEmail" ToolTip="Please enter valid email" ValidationGroup="items"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblContactEmail" Text="Contact Email"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtContactEmail" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqcontactemail" runat="server" ControlToValidate="txtEmail"
                                            ErrorMessage="*" ToolTip="Please enter email address" ValidationGroup="items" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtEmail" ToolTip="Please enter valid email" ValidationGroup="items"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblNationality" Text="Nationality"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtNationality" ValidationGroup="items" MaxLength="20"
                                            CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqNationality" runat="server" ControlToValidate="txtNationality"
                                            ErrorMessage="*" ToolTip="*" ValidationGroup="items" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblPresentAddress" Text="Present Address"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtPresentAddress" ValidationGroup="items" MaxLength="400"
                                            TextMode="MultiLine" CssClass="adminTextLarge" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblPermenantAddress" Text="Permenant Address"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPermenantAddress" runat="server" MaxLength="400" TextMode="MultiLine"
                                            CssClass="adminTextLarge" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 10px;"></td>
                <td style="border: solid 1px #E8E8E8; vertical-align: top; width: 45%;">
                    <div style="width: auto; vertical-align: top;">
                        <%--<uc:commonFileUploader runat="server" ID="ucFileUploader"/>--%>
                        <table width="100%">
                            <%--<tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label2" Text="Division"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="adminCombo" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDivision"
                                        ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Division" InitialValue="0" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label4" Text="Upload Image"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:FileUpload runat="server" ID="fuTeacher" />
                                    <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label10" Text="Image"></asp:Label>
                                </td>
                                <td align="left">
                                    <%--<asp:Label runat="server" ID="lblItemImage" Font-Bold="true" />--%><br />
                                    <asp:HiddenField ID="hdItemImage" runat="server" />
                                    <asp:Image runat="server" ID="imgItem" Width="120" Height="100" />
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
                                    <asp:Label runat="server" ID="Label2" Text="Bus"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBus" runat="server" CssClass="adminCombo">
                                    </asp:DropDownList>
                                    <%--AutoPostBack="true" OnSelectedIndexChanged="ddlBus_SelectedIndexChanged"
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBus"
                                            ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Bus" InitialValue="0" />--%>
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
                        CssClass="buttonStyle" OnClick="btnSave_Click" Width="100" />
                </td>
                <td>
                    <asp:Button ID="btnAddNew" runat="server" Text="Add New" ValidationGroup="" OnClick="btnAddNew_Click"
                        CssClass="buttonStyle" Width="100" />
                </td>
            </tr>
        </table>
    </div>
    <%--</asp:Panel>
            <asp:CollapsiblePanelExtender ID="cpItemSettings" runat="Server" TargetControlID="panelItemSettings"
                ExpandControlID="panelItemSettingsHeader" SuppressPostBack="true" CollapseControlID="panelItemSettingsHeader"
                Collapsed="true" ExpandDirection="Vertical" />--%>
    <div>
        <asp:HiddenField ID="hdItemID" runat="server" />
        <asp:HiddenField ID="hdReleaseDate" runat="server" />
        <script type="text/javascript">
            function ValidateForm() {
                var email = document.getElementById('ctl00_ContentPlaceHolder1_txtEmail');
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (document.getElementById("ctl00_ContentPlaceHolder1_txtcode").value == "") {
                    alert('Please enter Student ID');
                    return false;
                }
                else if (document.getElementById("ctl00_ContentPlaceHolder1_txtName").value == "") {
                    alert('Please enter Name');
                    return false;
                }
                else if (document.getElementById("ctl00_ContentPlaceHolder1_txtQualification").value == "") {
                    alert('Please enter Qualification');
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
            }
        </script>
    </div>
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ucFileUploader" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
