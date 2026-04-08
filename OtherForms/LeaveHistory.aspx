<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveHistory.aspx.cs"
    Inherits="LeaveHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

</head>
<body style="background-image: url(../Images/MasterPage/body_bg.gif); background-repeat: no-repeat">
    <form id="form1" runat="server">
        <br />
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server">
        </asp:ScriptManager>
        <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
            height: 118px;" id="UpdateProgress" align="center">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <img src="../Images/ajax-loader-new__.gif" /><br />
                    <img src="../Images/PleaseWait.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="aa">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="3">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <fieldset>
                                        <legend>All Applied Leave Detail</legend><span id="spanShowAllLeaveDetail" runat="server">
                                        </span>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
