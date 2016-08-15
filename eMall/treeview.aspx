<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="treeview.aspx.cs" Inherits="eMall.treeview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All">
        </asp:TreeView>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
            $(function () {
                $("[id*=TreeView1] input[type=checkbox]").bind("click", function () {
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
        <asp:Button ID="btnSend" runat="server" Text="Click" OnClick="btnSend_Click" />
    </form>
</body>
</html>
