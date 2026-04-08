<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="LeaveBalanceTeamLeader.aspx.cs" Inherits="LeaveBalanceTeamLeader"
    Title="Team Leave Balance : Team Leader " %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/BrowserWidth.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <br />
    <h1>
        Team Leave Balance : Team Leader</h1>
    <table style="width: 100%">
        <tr>
            <td>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%" colspan="2">
                                <table width="100%">
                                    <tbody>
                                        
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                    Text="Show"></asp:Button>
                                                <asp:Button ID="btnExportToEXCEL" runat="server" CssClass="text" OnClick="btnExportToEXCEL_Click"
                                                    Text="Export To EXCEL" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <div id="aa" style="overflow: auto;">
                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                    <asp:GridView ID="divReport" runat="server" Width="100%">
                                    </asp:GridView>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <script type="text/javascript" language="javascript">
        document.getElementById ('aa').style.width = ((GetBrowserWidth()) - 80 ) + 'px';   
    </script>

</asp:Content>
