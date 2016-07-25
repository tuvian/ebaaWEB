<%@ Page Title="" Language="C#" MasterPageFile="~/mySkool.Master" AutoEventWireup="true" CodeBehind="notification.aspx.cs" Inherits="eMall.notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ MasterType VirtualPath="~/mySkool.Master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<div class="adminEditContent">
        <table style="display: block; background-color: White; margin: 0px; padding: 2px; width: 100%; font-size: 11px; width: 100%;"
            cellpadding="5" cellspacing="2">
            <tr>
                <td>School Code
                    <asp:DropDownList ID="ddlSearchSchoolCodes" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="buttonStyle" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>--%>

    <%--<div>
        <table border="2" class="adminGridListTable">
            <tr>
                <td>
                    <asp:GridView runat="server" ID="GridItems" AutoGenerateColumns="False" PageSize="10"
                        Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333"
                        GridLines="None" Font-Size="13px" Font-Names="Verdana" OnPageIndexChanging="GridItems_PageIndexChanging"
                        DataKeyNames="id,school_id,from_id,from_type,subject,message,date,tmembers,smembers">
                        <RowStyle BackColor="#EFF3FB" Width="720" />
                        <Columns>
                            <asp:BoundField DataField="date" HeaderText="Date">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="subject" HeaderText="Subject">
                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="message" HeaderText="Message">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
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

    </div>--%>
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
                    <%--<asp:UpdatePanel ID="upschoollogin" runat="server">
                        <ContentTemplate>--%>
                    <table width="100%">
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label4" Text="School Code"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSchoolCode" runat="server" CssClass="adminCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolCode_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSchoolCode"
                                    ErrorMessage="Select a school" ValidationGroup="items" ToolTip="Please select a School" InitialValue="0" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label1" Text="Students In the Class :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:ListBox ID="lstClassStudent" runat="server" CssClass="adminCombo" AutoPostBack="false"></asp:ListBox>
                                <asp:Label ID="lblSelectedClassForStudent" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label2" Text="Students In the Class :"></asp:Label>
                            </td>
                            <td align="left">
                                <div style="width: 200px; height: 150px; padding: 2px; overflow: auto; border: 1px solid #ccc;">
                                    <asp:CheckBoxList class="BodyTxt" ID="chkClassForStudent" runat="server"></asp:CheckBoxList>
                                </div>
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label3" Text="Teachers In the Class :"></asp:Label>
                            </td>
                            <td align="left">
                                <div style="width: 200px; height: 150px; padding: 2px; overflow: auto; border: 1px solid #ccc;">
                                    <asp:CheckBoxList class="BodyTxt" ID="chkClassForTeacher" runat="server"></asp:CheckBoxList>
                                </div>
                                <asp:Label ID="lblSelectedClassForTeacher" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <%--<tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label1" Text="Students In the Class :"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlClass" runat="server" CssClass="adminCombo" AutoPostBack="false">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClass"
                                    ErrorMessage="* Required" ValidationGroup="items" ToolTip="Please select a Std" InitialValue="0" />
                            </td>
                        </tr>--%>

                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="lblQstnNo" Text="Subject"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtSubject" ValidationGroup="items" MaxLength="40" CssClass="adminText" />
                                <asp:RequiredFieldValidator ID="rqName" runat="server" ControlToValidate="txtSubject"
                                    ErrorMessage="Enter subject" ToolTip="Please enter Subject " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="lblMessage" Text="Message"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtMessage" ValidationGroup="items" MaxLength="400"
                                    TextMode="MultiLine" CssClass="adminTextLarge" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMessage"
                                    ErrorMessage="Enter Message" ToolTip="Please enter Message " ValidationGroup="items" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label10" Text="Attached File Name"></asp:Label>
                            </td>
                            <td align="left">
                                <%--<asp:Label runat="server" ID="lblItemImage" Font-Bold="true" />--%><br />
                                <asp:HiddenField ID="hdItemImage" runat="server" />
                                <asp:Label runat="server" ID="lblFileName" Font-Bold="true"></asp:Label>
                                <%--<asp:Image runat="server" ID="imgItem" Width="120" Height="100" />--%>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="left" class="adminLabel">
                                <asp:Label runat="server" ID="Label1" Text="Attach File"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:FileUpload runat="server" ID="fuTeacher" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnUpload" Text="Upload" OnClick="btnUpload_Click" />
                            </td>
                        </tr>


                    </table>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
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
                    <asp:Button ID="btnSave" runat="server" Text="SEND" ValidationGroup="items" CausesValidation="true"
                        CssClass="buttonStyle" OnClick="btnSave_Click" Width="100" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" OnClick="btnClear_Click"
                        CssClass="buttonStyle" Width="100" />

                    <%--OnClick="btnAddNew_Click"--%>
                </td>
            </tr>
        </table>
    </div>
    <%--</asp:Panel>
            <asp:CollapsiblePanelExtender ID="cpItemSettings" runat="Server" TargetControlID="panelItemSettings"
                ExpandControlID="panelItemSettingsHeader" SuppressPostBack="true" CollapseControlID="panelItemSettingsHeader"
                Collapsed="true" ExpandDirection="Vertical" />--%>
    <asp:HiddenField ID="hdItemID" runat="server" />
    <asp:HiddenField ID="hdReleaseDate" runat="server" />
    <script type="text/javascript">
        function ValidateForm() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtTitle").value == "") {
                alert('Please enter Title of the Event');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtStartDate").value == "") {
                alert('Please enter Start Date');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value == "") {
                alert('Please enter End Date');
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value == "") {
                alert('Please enter description');
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        document.getElementById("<%=btnUpload.ClientID %>").style.display = 'none';
        function UploadFile(fileUpload) {
            if (document.getElementById("<%=fuTeacher.ClientID %>").value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
    </script>


</asp:Content>
