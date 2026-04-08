<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="PendingCompOffToBeAdd.aspx.cs" Inherits="PendingCompOffToBeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <br />

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/RollOverEffect.js"></script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
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
    <br />
    <h1>
        Pending Comp Off To Be Add</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <table width="100%">
        <tbody>
            <tr>
                <td style="width: 40%" align="center">
                </td>
                <td style="width: 20%" align="center">
                    <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                        <asp:Button ID="btnExportInExcel" runat="server" Text="Export" OnClick="btnExportInExcel_Click" /></fieldset>
                </td>
                <td style="width: 40%" align="center">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 90%">
                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                    <asp:GridView ID="gvDetail" runat="server" Width="100%">
                                    </asp:GridView>
                                </fieldset>
                            </td>
                            <td style="width: 5%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
