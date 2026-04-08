<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeaveRoaster.ascx.cs"
    Inherits="LeaveRoaster" %>


        <div style="padding: 3px;">
    <table cellpadding="3" cellspacing="0" style="width: 100%; border-right: #000000 thin groove; border-top: #000000 thin groove; border-left: #000000 thin groove; border-bottom: #000000 thin groove;" border=" black thin groove">
        <tr>
            <td align="center" colspan="3" style="border-bottom: #000099 thin solid">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Label" ForeColor="Blue"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 33.33%;" align="center">
                <asp:ImageButton ID="btnPrevious" runat="server" Width="16px" Height="16px" ImageUrl='~/Images/Previous.png'
                    OnClick="btnPrevious_Click" />
               </td>
            <td style="width: 33.33%" align="center">
                <asp:ImageButton ID="ImageButton1" runat="server" Width="16px" Height="16px" ImageUrl='~/Images/Reload.png'
                    OnClick="ImageButton1_Click" />
            </td>
            <td style="width: 33.33%" align="center">
                <asp:ImageButton ID="ImageButton2" runat="server" Width="16px" Height="16px" ImageUrl='~/Images/Next.png'
                    OnClick="ImageButton2_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="border-top: #000099 thin solid">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
        </tr>
    </table>
</div>
   
