<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonFileUploader.ascx.cs"
    Inherits="ASTCWeb.User_Controls.CommonFileUploader" %>

<script type="text/javascript">
    var counter = 0;
    var rowCount = <%=this.UpperLimit%>;

    function AddFileUpload() {
        if (counter < rowCount) {
            var div = document.createElement('DIV');
            div.innerHTML = '<input id="file' + counter + '" name = "file' + counter +
                     '" type="file" />' +
                     '<input id="Button' + counter + '" type="button" ' +
                     'value="Remove" onclick = "RemoveFileUpload(this)" />';
                       
            document.getElementById("FileUploadContainer").appendChild(div);
            counter++;
        }
    }
    function RemoveFileUpload(div) {
        document.getElementById("FileUploadContainer").removeChild(div.parentNode);
        counter--;
    }        

</script>

<div style="border: solid 1px #E8E8E8; height: auto; width: 98%;">
    <%--<form id="form1" runat="server" enctype="multipart/form-data" method="post">--%>
    <table>
        <%--<tr>
            <td style="width: 270px; margin-left: 20px;">
                <span style="font-family: Arial">Click to add files</span>&nbsp;&nbsp;
            </td>
            <td style="width: 150px; margin-left: 20px;">
                <input id="Button1" type="button" value="add" onclick="AddFileUpload()" style="width: 100px;" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <div id="FileUploadContainer">
                    <!--FileUpload Controls will be added here -->
                    <input id="file0" name="file0" type="file" style="width:350px;"  />
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 250px; margin-left: 20px;" align="left">
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" CssClass="buttonStyle"
                    Width="100px" />
            </td>
            <td>
                <asp:Label ID="lblProgressLabel" runat="server" Text="File Uploading ...!!!" Font-Bold="true"
                    Visible="false" />
            </td>
        </tr>
    </table>
    <%--</form>--%>
</div>
