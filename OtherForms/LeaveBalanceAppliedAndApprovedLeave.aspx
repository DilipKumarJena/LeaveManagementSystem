<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveBalanceAppliedAndApprovedLeave.aspx.cs"
    Inherits="LeaveBalanceAppliedAndApprovedLeave" %>

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
                    <td style="width: 33%">
                    </td>
                    <td style="width: 33%">
                        <fieldset>
                            <legend>Leave Balance</legend>
                            <div id="LeaveBalance" runat="server">
                            </div>
                        </fieldset>
                    </td>
                    <td style="width: 33%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%">
                    </td>
                    <td style="width: 33%">
                        <br />
                    </td>
                    <td style="width: 33%">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 20%">
                                </td>
                                <td style="width: 60%">
                                    <fieldset>
                                        <legend>Applied Leave</legend>
                                        <div id="LeaveDetail" runat="server">
                                        </div>
                                    </fieldset>
                                </td>
                                <td style="width: 20%">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <fieldset>
                                        <legend>All Applied Leave Detail</legend><span id="spanShowAllLeaveDetail" runat="server">
                                        </span>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <fieldset>
                                        <legend>Punch Card Detail</legend>
                                        <asp:GridView ID="gvDetail" runat="server" Width="100%">
                                        </asp:GridView>
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
