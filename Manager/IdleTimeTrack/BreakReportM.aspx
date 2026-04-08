<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="BreakReportM.aspx.cs" Inherits="BreakReportM" Title="Break Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../JavaScript/calendar.js"></script>

    <br />
    <h1>
        Report : Break</h1>
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
                                            <td valign="top" colspan="2">
                                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 25%">
                                                                    Start Date</td>
                                                                <td style="width: 25%">
                                                                    <asp:TextBox onkeydown="return false" ID="txtStartDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                        onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                        runat="server" CssClass="text" Font-Bold="False"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errorRed"
                                                                        ErrorMessage="* Required" Display="Dynamic" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator></td>
                                                                <td style="width: 25%">
                                                                    End Date</td>
                                                                <td style="width: 25%">
                                                                    <asp:TextBox onkeydown="return false" ID="txtEndDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                        onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                        runat="server" CssClass="text" Font-Bold="False"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errorRed"
                                                                        ErrorMessage="* Required" Display="Dynamic" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </fieldset>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                    Text="Show"></asp:Button>
                                                <asp:Button ID="btnExportToEXCEL" runat="server" CssClass="text" OnClick="btnExportToEXCEL_Click"
                                                    Text="Export To EXCEL" /></td>
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
