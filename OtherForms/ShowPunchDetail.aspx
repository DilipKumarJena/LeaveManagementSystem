<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowPunchDetail.aspx.cs"
    Inherits="OtherForms_ShowPunchDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnExport" runat="server" CssClass="text" OnClick="btnExport_Click"
                Text="Export To Excel (Punch)" /><br />
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 90%">
                        <fieldset>
                            <legend>Punch Card Detail</legend>
                            <asp:GridView ID="gvDetail" runat="server" Width="100%">
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%">
                        <fieldset>
                            <legend>Applied Leave Status</legend>
                            <asp:GridView ID="gvLeaveDetail" runat="server" Width="100%">
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
