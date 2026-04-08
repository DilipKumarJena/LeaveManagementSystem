<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TimeSetup.ascx.cs" Inherits="TimeSetup" %>
<fieldset style="padding: 3px">
    <table>
        <tr>
            <td align="left" class="text" colspan="3" style="text-align: center; border-right: #003399 thin solid; border-top: #003399 thin solid; border-left: #003399 thin solid; border-bottom: #003399 thin solid;" valign="top">
                (24 Hour Clock)</td>
        </tr>
        <tr>
            <td align="left" style="text-align: center" valign="top" class="text">
                Hour</td>
            <td align="left" style="text-align: center" valign="top" class="text">
                Minute</td>
            <td align="left" style="text-align: center" valign="top" class="text">
                Second</td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:DropDownList ID="ddlH" runat="server" CssClass="text">
                </asp:DropDownList></td>
            <td align="left" valign="top">
                <asp:DropDownList ID="ddlM" runat="server" CssClass="text">
                </asp:DropDownList></td>
            <td align="left" valign="top">
                <asp:DropDownList ID="ddlS" runat="server" CssClass="text">
                </asp:DropDownList></td>
        </tr>
    </table>
</fieldset>
