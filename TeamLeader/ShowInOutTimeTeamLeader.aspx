<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="ShowInOutTimeTeamLeader.aspx.cs" Inherits="ShowInOutTimeTeamLeader"
    Title="Team Leader : Show In Out Time" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <br />
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server">
    </asp:ScriptManager>
    <br />
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <progresstemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </progresstemplate>
        </asp:UpdateProgress>
    </div>
    <%--  <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>--%>
    <h1>
        In Out Time : Team Leader</h1>
    <br />
    <table style="width: 100%" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                    valign="middle">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 50%">
                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                    <table cellpadding="5px" cellspacing="5px" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 20%">
                                                    <strong>
                                                    Employee</strong></td>
                                                <td style="width: 80%">
                                                    <asp:DropDownList ID="ddlEmployeeList" runat="server" CssClass="text">
                                                    </asp:DropDownList>
                                                     &nbsp;&nbsp; <a class="Navigation" href="../OtherForms/FindEmployeeCode.aspx"
                                                        onclick="return GB_showCenter('   *   OKS Group   *   Find Employee Code', this.href,415,<%= Session["BrowserWidth"] %> )"
                                                        onmouseover="window.status='http://www.oksgroup.com';return true" style="color: black"
                                                        title="Find Employee Code">Find Employee</a></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 25%">
                                                                <strong>Start Date</strong></td>
                                                            <td style="width: 25%">
                                                                <asp:TextBox ID="txtStart" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStart"
                                                                    CssClass="errorRed" Display="Dynamic" ErrorMessage="* Please Select Date"></asp:RequiredFieldValidator></td>
                                                            <td style="width: 25%">
                                                                <strong>End Date</strong></td>
                                                            <td style="width: 25%">
                                                                <asp:TextBox ID="txtEnd" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEnd"
                                                                    CssClass="errorRed" Display="Dynamic" ErrorMessage="* Please Select Date"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnShow" OnClick="btnShowEmployee_Click" runat="server" Text="Show "
                                                        CssClass="text"></asp:Button>
                                                    <asp:Button ID="btnExport" runat="server" CssClass="text" Text="Export To EXCEL"
                                                        OnClick="btnExport_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                        <asp:GridView ID="gvDetail" runat="server" Width="100%">
                        </asp:GridView>
                    </fieldset>
                </td>
            </tr>
        </tbody>
    </table>
    <%--</contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
