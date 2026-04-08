<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthYear.ascx.cs" Inherits="UserControl_MonthYear" %>

<table cellpadding="5" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 25%">
            Month</td>
        <td style="width: 25%">
            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="text">
            </asp:DropDownList></td>
        <td style="width: 25%">
            Year</td>
        <td style="width: 25%">
            <asp:DropDownList ID="ddlYear" runat="server" CssClass="text">
            </asp:DropDownList></td>
    </tr>
</table>
