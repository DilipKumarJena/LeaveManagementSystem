<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="CabLateManagerApproval.aspx.cs" Inherits="Manager_CabLateManagerApproval"
    Title="Cab Late Manager Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <br />
    <h1>
        Cab Late : Manager Approval
    </h1>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 33%">
            </td>
            <td style="width: 33%">
                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 30%">
                                Date</td>
                            <td style="width: 70%">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnShow" runat="server" CssClass="text" OnClick="btnShow_Click" Text="Show" />
                                <div id="divError" runat="server" class="errorRed">
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <br />
            </td>
            <td style="width: 33%">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 5%">
                        </td>
                        <td style="width: 90%">
                            <asp:GridView ID="gvEmployeePunchDetail" runat="server" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" />
                                    <asp:BoundField DataField="DateIn" HeaderText="DateIn" />
                                    <asp:BoundField DataField="DateOut" HeaderText="DateOut" />
                                    <asp:BoundField DataField="OutDuty" HeaderText="OutDuty (In Minutes)" />
                                    <asp:TemplateField HeaderText="CabLate  (In Minutes)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCabLate" runat="server" onkeypress="return NumberOnly() " Width="50%"
                                                Text='<%# Bind("CabLate") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CabNotCame">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCanNotCame" runat="server" Checked='<%# Bind("CabNotCame") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="text" OnClick="btnUpdate_Click"
                                Text="Update" Visible="False" /></td>
                        <td style="width: 10%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
