<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="CabLateEntry.aspx.cs" Inherits="CabLateEntry" Title="Cab Late Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
    <br />
    <h1>
        Employee Cab Late Entry</h1>
    <br />
    <table style="width: 100%">
        <tr>
            <td align="center">
                <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
                    height: 118px;" id="UpdateProgress" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <img src="../Images/ajax-loader-new__.gif" /><br />
                            <img src="../Images/PleaseWait.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 30%">
                                    </td>
                                    <td style="width: 30%" colspan="2">
                                        <fieldset>
                                            <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 25%">
                                                                Date</td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="Dynamic" ErrorMessage="* Required"></asp:RequiredFieldValidator></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%">
                                                                Vehicle No.</td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="text">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="4">
                                                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" CssClass="text" /><br />
                                                                <asp:Label ID="lblError" runat="server" CssClass="errorRed"></asp:Label></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </fieldset>
                                    </td>
                                    <td style="width: 30%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%" colspan="4">
                                        <div id="divVisible" runat="server" visible="false">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 30%">
                                                            <asp:TextBox ID="txtLateInMin" runat="server" Width="46px" onkeypress="return NumberOnly()"
                                                                CssClass="text"></asp:TextBox>&nbsp;
                                                            <asp:Button ID="btnUpdateAll" runat="server" OnClick="btnUpdateAll_Click" Text="Update All"
                                                                CssClass="text" />
                                                            <asp:CheckBox ID="chkCabNotCame" runat="server" CssClass="text" Text="Cab Not Came" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:GridView ID="gvLeaveDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                CssClass="text">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Late (In Min.)">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtEntry" CssClass="text" runat="server" onkeypress="return NumberOnly() "
                                                                                Width="46px" Text='<%# Bind ("CabLate") %>' />
                                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind ("ID") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="EMPCode" HeaderText="EMPCode" />
                                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                                                    <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                                                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                                    <asp:BoundField DataField="DateIn" HeaderText="DateIn" />
                                                                    <asp:BoundField DataField="DateOut" HeaderText="DateOut" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            Reason : &nbsp;<asp:TextBox ID="txtReason" runat="server" Height="54px" TextMode="MultiLine"
                                                                Width="351px"></asp:TextBox>
                                                            <asp:Label ID="lblReason" runat="server" CssClass="errorRed"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnUpdate" OnClick="btnExportToEXCEL_Click" runat="server" CssClass="text"
                                                                Text="Update"></asp:Button></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%" align="center" colspan="4">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
