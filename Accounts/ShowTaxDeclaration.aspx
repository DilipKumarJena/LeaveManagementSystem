<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="ShowTaxDeclaration.aspx.cs" Inherits="Accounts_ShowTaxDeclaration"
    Title="Tax Declaration Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../JavaScript/BrowserWidth.js"></script>

    <br />
    <h1>
        Tax Declaration Report
    </h1>
    <table style="width: 100%">
        <tr>
            <td style="width: 30%">
            </td>
            <td style="width: 40%">
                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 50%">
                                Select Financial Year</td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="text">
                                    <asp:ListItem Value="0">&lt;-- Select --&gt;</asp:ListItem>
                                    <asp:ListItem Value="2014">2014-2015</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;<asp:Button ID="btnShow" runat="server" CssClass="text" OnClick="btnShow_Click"
                                    Text="Show" />&nbsp; &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="text"
                                        Text="Export To Excel" OnClick="btnExport_Click" /></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td style="width: 30%">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="aa" style="overflow: auto;" <%--onkeyup="scrollDiv(this, 50); return false;"--%>>
                    <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                        <asp:GridView ID="divReport" runat="server" Width="100%">
                        </asp:GridView>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        document.getElementById ('aa').style.width = ((GetBrowserWidth()) - 80 ) + 'px';   
         
    </script>

</asp:Content>
