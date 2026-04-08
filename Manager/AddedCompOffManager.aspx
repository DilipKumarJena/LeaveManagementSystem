<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true" CodeFile="AddedCompOffManager.aspx.cs" Inherits="AddedCompOffManager" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>


    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/BrowserWidth.js"></script>
<script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server">
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
        Report : Added CompOffs </h1>
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
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <contenttemplate>
<FIELDSET style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"><TABLE width="100%"><TBODY><TR><TD style="WIDTH: 25%">Start Date</TD><TD style="WIDTH: 25%"><asp:TextBox onkeydown="return false" id="txtStartDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);" runat="server" CssClass="text" Font-Bold="False"></asp:TextBox> <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="errorRed" ErrorMessage="* Required" Display="Dynamic" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator></TD><TD style="WIDTH: 25%">End Date</TD><TD style="WIDTH: 25%"><asp:TextBox onkeydown="return false" id="txtEndDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);" runat="server" CssClass="text" Font-Bold="False"></asp:TextBox> <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="errorRed" ErrorMessage="* Required" Display="Dynamic" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator></TD></TR></TBODY></TABLE></FIELDSET> 
</contenttemplate>
                                                </asp:UpdatePanel><asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="Medium"></asp:Label></td>
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
                                    <asp:GridView ID="divReport" runat="server" >
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