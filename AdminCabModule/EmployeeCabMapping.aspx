<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="EmployeeCabMapping.aspx.cs" Inherits="EmployeeCabMapping" Title="Employee Cab Mapping" %>

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
        Employee Cab Mapping</h1>
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
                                                            <td valign="top" colspan="2">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 25%">
                                                                                Vehicle No.</td>
                                                                            <td colspan="3">
                                                                                <asp:DropDownList ID="ddlVehicleNo" runat="server" OnSelectedIndexChanged="ddlVehicleNo_SelectedIndexChanged"
                                                                                    CssClass="text" AutoPostBack="True">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%">
                                                                                Location</td>
                                                                            <td style="width: 25%">
                                                                                <asp:DropDownList ID="ddlLocation" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"
                                                                                    AutoPostBack="True" CssClass="text">
                                                                                </asp:DropDownList><br />
                                                                                <span style="display: none; color: red" id="Error00"></span>
                                                                            </td>
                                                                            <td style="width: 25%">
                                                                                Department</td>
                                                                            <td style="width: 25%">
                                                                                <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                                                                    AutoPostBack="True" CssClass="text">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%">
                                                                                Designation</td>
                                                                            <td style="width: 25%">
                                                                                <asp:DropDownList ID="ddlDesignation" runat="server" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"
                                                                                    AutoPostBack="True" CssClass="text">
                                                                                </asp:DropDownList><br />
                                                                                <span style="display: none; color: red" id="Error2"></span>
                                                                            </td>
                                                                            <td style="width: 25%">
                                                                                Employee</td>
                                                                            <td style="width: 25%">
                                                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="text">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center" colspan="2">
                                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                                    Text="Show"></asp:Button>&nbsp;
                                                            </td>
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
                                    <td style="width: 30%">
                                    </td>
                                    <td style="width: 30%" colspan="2" align="center">
                                        <h2>
                                            <div id="Error" runat="server">
                                            </div>
                                        </h2>
                                    </td>
                                    <td style="width: 30%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%" colspan="4">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvLeaveDetail" runat="server" Width="100%" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Add" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAdd" runat="server" />
                                                                        <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind ("ID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="EMPCode" HeaderText="EMPCode" />
                                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                                                <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Button ID="btnAddSelectedEmployee" OnClick="btnExportToEXCEL_Click" runat="server"
                                                            CssClass="text" Text="Add Selected Employee" Visible="False"></asp:Button></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvCabMapping" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            OnRowCommand="gvCabMapping_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Remove" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="RemoveEmployee"
                                                                            CommandArgument='<%# Bind ("ID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="EMPCode" HeaderText="EMPCode" />
                                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                                                <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
