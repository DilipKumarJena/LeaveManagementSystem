<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="LeaveCalendar.aspx.cs" Inherits="LeaveCalendar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <h1>
        Employee Leave Calendar
    </h1>
    <table cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="width: 30%">
            </td>
            <td style="width: 40%">
                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="width: 25%">
                                Employee List</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlEmployeeList" runat="server" CssClass="text">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Start Date</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox></td>
                            <td style="width: 25%">
                                End Date</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" CssClass="text" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td style="width: 30%">
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
            </td>
            <td align="center" style="width: 40%">
            </td>
            <td style="width: 30%">
            </td>
        </tr>
    </table>
    <asp:DataList ID="dlSource" runat="server" BorderColor="Black" BorderWidth="3px"
        CellPadding="10" CellSpacing="10" GridLines="Both" RepeatColumns="3" RepeatDirection="Vertical "
        RepeatLayout="Table" ShowFooter="False" ShowHeader="False" Width="100%">
        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemTemplate>
            <%# Eval("Cal")%>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
