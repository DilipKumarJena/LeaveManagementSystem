<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserDetails.ascx.cs" Inherits="web_controls_UserDetails" %>
<table style="background-color: #ab9c73; width: 100%; z-index: 100; vertical-align: middle;
    height: 1px; text-align: center;" cellpadding="0px;" cellspacing="0">
    <tr>
        <td align="center" valign="middle" class="upperLinkMenu" style="width: 10%;">
            &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"></asp:Label></td>
        <td align="center" valign="middle" class="upperLinkMenu" style="width: 10%;">
<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" Font-Bold="True"
    OnClick="LinkButton2_Click" Font-Names="Verdana" Font-Size="11px">Home</asp:LinkButton></td>
        <td align="center" valign="middle" class="upperLinkMenu" style="width: 30%;">
<table width="100%" align="center" cellspacing="0" cellpadding="0">
    <tr>
        <td width="50%" align="center" valign="middle">
            <%-- <img id="SingleImage" runat="server" align="middle" alt="Not Available" style="border-right: black 1px solid;
                            border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" />--%>
            <a id="a1" runat="server" class="Navigation" title="Image" onclick="return GB_showCenter('Preview', this.href,500,700)">
                <img id="Img1" runat="server" align="middle" alt="Not Available" style="border-right: black 1px solid;
                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" />
                <%-- <img id="Img1" runat="server" alt="Not Available" width="60" height="60" />--%>
            </a>
        </td>
        <td width="50%" align="center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"></asp:Label></td>
    </tr>
</table>
        </td>
        <td align="center" valign="middle" class="upperLinkMenu" style="width: 30%;"><asp:Label id="Label4" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="Maroon" Text="Last Login Time : " /><br />
            <asp:Label ID="Label3" runat="server"
        Font-Bold="True" Font-Names="Verdana" Font-Size="11px" ForeColor="Maroon"></asp:Label></td>
        <td align="center" class="upperLinkMenu" style="width: 10%;" valign="middle">
        <asp:LinkButton
            ID="LinkButton4" runat="server" CausesValidation="False" Font-Bold="True" Font-Names="Verdana"
            Font-Size="11px" OnClick="LinkButton4_Click">Central Home</asp:LinkButton></td>
        <td align="center" valign="middle" class="upperLinkMenu" style="width: 10%;">
        <asp:LinkButton
                ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="True"
                CausesValidation="False" Font-Names="Verdana" Font-Size="11px">Logout</asp:LinkButton></td>
    </tr>
</table>
&nbsp;&nbsp;
