<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveNotApplied.aspx.cs"
    Inherits="LeaveNotApplied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-image: url(../Images/MasterPage/BG2.gif); background-attachment: scroll;
            background-repeat: no-repeat">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 15%; height: 22px;">
                    </td>
                    <td style="width: 70%; height: 22px;">
                        Select Month :
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="text">
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp;<asp:Button ID="btnShow" runat="server" CssClass="text" OnClick="btnShow_Click"
                            Text="Show" />&nbsp;
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:Button ID="btnExport" runat="server" CssClass="text" OnClick="btnExport_Click"
                            Text="Export To Excel" /></td>
                    <td style="width: 15%; height: 22px;">
                        &nbsp;<a href="#" onclick="parent.parent.GB_hide();">Close</a></td>
                </tr>
                <tr>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 70%">
                        <asp:GridView ID="gvDetail" runat="server" Width="100%">
                        </asp:GridView>
                    </td>
                    <td style="width: 15%">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
