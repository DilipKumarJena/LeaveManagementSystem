<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowMusterRoll.aspx.cs" Inherits="Employee_ShowMusterRoll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-image: url(../Images/MasterPage/body_bg.gif); background-color: White;
    background-repeat: no-repeat">
    <form id="form1" runat="server">
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                    <fieldset>
                        <asp:GridView ID="gvMusterRoll" runat="server" Width="100%">
                        </asp:GridView>
                    </fieldset>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
