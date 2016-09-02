<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true" CodeBehind="library.aspx.cs" Inherits="eMall.library" %>

<%@ Register TagName="commonFileUploader" TagPrefix="uc" Src="~/UserControls/CommonFileUploader.ascx" %>
<%@ MasterType VirtualPath="~/mySkool.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>School Code
                    <asp:DropDownList ID="ddlSearchSchoolCodes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchSchoolCodes_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>Standard
                </td>
                <td style="width: 110px;">
                    <asp:DropDownList ID="ddlSeachClass" runat="server">
                    </asp:DropDownList>
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
                        OnRowDeleting="GridItems_RowDeleting" OnRowCommand="GridItems_RowCommand"
                        DataKeyNames="id, std, school_id, subject_id, title, file_path, file_type, subject, status">
                        <RowStyle BackColor="#EFF3FB" Width="720" />
                        <Columns>
                            <asp:BoundField DataField="std" HeaderText="Class">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="subject" HeaderText="Subject">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="title" HeaderText="Topic Name">
                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:ButtonField CommandName="Change" ButtonType="Button" HeaderText="Edit" Text="Edit">
                                <ControlStyle CssClass="buttonStyle" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:ButtonField>
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
                                        <asp:Label runat="server" ID="Label1" Text="Standard"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="adminCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClass"
                                            ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Std" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblStudentID" Text="Subject"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="adminCombo" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubject"
                                            ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Subject" InitialValue="0" />
                                    </td>
                                </tr>


                                <%-- <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblFirstName" Text="Lesson"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlLesson" runat="server" CssClass="adminCombo" AutoPostBack="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlLesson"
                                            ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Lesson" InitialValue="0" />
                                    </td>
                                </tr>--%>

                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="lblTitle" Text="File Title"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtFileTitle" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                        <asp:RequiredFieldValidator ID="rqMiddleName" runat="server" ControlToValidate="txtFileTitle"
                                            ErrorMessage="*" ToolTip="Please enter File Title " ValidationGroup="items" />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="left" class="adminLabel">
                                        <asp:CheckBox ID="chkYoutube" runat="server" Text="Add Youtube URL" OnClick="checkedYoutube();" />                                       
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtYoutubeURL" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td align="left" class="adminLabel">
                                        <asp:Label runat="server" ID="Label6" Text="Status"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 10px;"></td>
                <td style="border: solid 1px #E8E8E8; vertical-align: top; width: 45%;">
                    <div style="width: auto; vertical-align: top;">
                        <table runat="server" id="tblfileUpload" visible="false" width="100%">

                            <tr>
                                <%-- <td align="left" class="adminLabel">
                                    <asp:Label runat="server" ID="Label4" Text="Upload File"></asp:Label>
                                </td>--%>
                                <td align="left">
                                    <asp:FileUpload runat="server" ID="fuTeacher" accept="application/pdf,video/pdf,video/mp4,video/flv,video/avi,video/mpeg,video/swf,video/mkv,video/mov,video/3gp" />
                                </td>
                            </tr>
                            <tr>
                                <%--<td align="left" class="adminLabel">
                                    <asp:Label ID="lblUploadedFile" runat="server" Text="Uplaoded File" />
                                </td>--%>
                                <td align="left" class="adminLabel">
                                    <asp:HiddenField ID="hdItemImage" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txUploadedFileName"
                                        ErrorMessage="*" ToolTip="Please upload a file" ValidationGroup="items" />
                                    <asp:TextBox runat="server" ID="txUploadedFileName" ValidationGroup="items" MaxLength="40" ReadOnly="true" CssClass="adminText" />
                                    <asp:Button runat="server" ID="btnUpload" Text="Upload" Font-Bold="true" OnClick="btnUpload_Click" />
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
    <div>
        <asp:HiddenField ID="hdItemID" runat="server" />
        <asp:HiddenField ID="hdVideoUniqueID" runat="server" />
        <asp:HiddenField ID="hdReleaseDate" runat="server" />
        <script type="text/javascript">
            function checkedYoutube() {
                if (document.getElementById("ctl00_ContentPlaceHolder1_chkYoutube").checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_txtYoutubeURL").disabled = false;
                    return false;
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_txtYoutubeURL").disabled = true;
                    return false;
                }
            }
        </script>
    </div>
</asp:Content>
