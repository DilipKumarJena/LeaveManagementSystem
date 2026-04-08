<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="CabReport.aspx.cs" Inherits="CabReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../JavaScript/calendar.js"></script>

    <table style="width: 100%" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td colspan="3">
                    <br />
                    <h1>
                        Administration : Cab Report
                    </h1>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 30%">
                    <fieldset style="padding: 5px">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 25%" valign="top">
                                        Route
                                    </td>
                                    <td colspan="3" valign="top">
                                        <asp:DropDownList ID="ddlRoute" runat="server" CssClass="text">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="width: 25%" valign="top">
                                        Start Date</td>
                                    <td style="width: 25%" valign="top">
                                        <asp:TextBox ID="txtStart" runat="server" CssClass="text" Font-Bold="False"
                                            onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;" onkeydown="return false"
                                            onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox><br />
                                        <asp:Label ID="lblStart" runat="server" Text="* Required" CssClass="errorRed" Visible="false"></asp:Label>
                                    </td>
                                    <td style="width: 25%" valign="top">
                                        End Date</td>
                                    <td style="width: 25%" valign="top">
                                        <asp:TextBox ID="txtEnd" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                            onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox><br />
                                        <asp:Label ID="lblEnd" runat="server" Text="* Required" CssClass="errorRed" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4" valign="top">
                                        <asp:Button ID="btnShow" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                            Text="Show"></asp:Button>
                                        <asp:Button ID="btnExport" runat="server" CssClass="text" Text="Export" OnClick="btnRefresh_Click">
                                        </asp:Button></td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </td>
                <td style="width: 30%">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <h2>
                        <span id="spanMessage" runat="server"></span>
                    </h2>
                </td>
            </tr>
            <tr>
                <td style="height: 19px" colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <fieldset style="padding: 3px">
                        <asp:GridView ID="gvCabReport" runat="server" Width="100%">
                        </asp:GridView>
                    </fieldset>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
