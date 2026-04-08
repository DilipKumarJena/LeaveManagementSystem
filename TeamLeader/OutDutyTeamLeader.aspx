<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="OutDutyTeamLeader.aspx.cs" Inherits="TeamLeader_OutDutyTeamLeader"
    Title="Team Leader : Out Duty Approval" %>

<%@ Register Src="../UserControl/TimeSetup.ascx" TagName="TimeSetup" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <h1>
        Out Duty Approval : Team Leader
    </h1>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td colspan="3">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 50%" align="center">
                            <h2>
                                <asp:Label ID="lblMessage" runat="server" CssClass="errorRed"></asp:Label>
                            </h2>
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 50%">
                            <br />
                            <asp:Repeater ID="repApproval" runat="server" OnItemCommand="repApproval_ItemCommand">
                                <ItemTemplate>
                                    <fieldset style="padding: 5px">
                                        <fieldset style="padding: 5px">
                                            <legend>Out Duty Employee Detail</legend>
                                            <table width="100%" cellpadding="5" id="EmployeeDetail" runat="server">
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        Employee Code</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("EmpCode")%>
                                                    </td>
                                                    <td style="width: 15%" class="heading">
                                                        Employee Name</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("Name")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        Location</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("Location")%>
                                                    </td>
                                                    <td style="width: 15%" class="heading">
                                                        DepartmentName</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("DepartmentName")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        DesignationName</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("DesignationName")%>
                                                    </td>
                                                    <td style="width: 15%" class="heading">
                                                        AppliedDate</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("AppliedDate")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        Out Duty Date</td>
                                                    <td colspan="3">
                                                        <%# Eval("Date")%>
                                                        <asp:HiddenField ID="hfDate" runat="server" Value='<%# Bind("Date")%>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        Purpose</td>
                                                    <td colspan="3">
                                                        <%# Eval("Purpose")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        TIME OUT</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("TIMEOUT")%>
                                                        <asp:HiddenField ID="hfTimeOut" runat="server" Value='<%# Bind("TIMEOUTBind")%>' />
                                                    </td>
                                                    <td style="width: 15%" class="heading">
                                                        TIME In</td>
                                                    <td style="width: 35%">
                                                        <%# Eval("TIMEIn")%>
                                                        <asp:HiddenField ID="hfTimeIn" runat="server" Value='<%# Bind("TIMEInBind")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <fieldset style="padding: 5px">
                                            <legend>Team Leader Approval </legend>
                                            <table width="100%" cellpadding="5px">
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        TIME OUT</td>
                                                    <td style="width: 35%">
                                                        <uc1:TimeSetup ID="TimeOut" runat="server" />
                                                    </td>
                                                    <td style="width: 15%" class="heading">
                                                        TIME In</td>
                                                    <td style="width: 35%">
                                                        <uc1:TimeSetup ID="TimeIn" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        Team Leader<br />
                                                        Comment</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Height="50px" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" class="heading">
                                                        Approval</td>
                                                    <td style="width: 35%">
                                                        <asp:DropDownList ID="ddlApproval" runat="server" CssClass="text">
                                                            <asp:ListItem Text="<-- Select -->" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 15%" class="heading">
                                                    </td>
                                                    <td style="width: 35%">
                                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="text" CommandName="Update"
                                                            CommandArgument='<%# Bind("ID")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <br />
                                    </fieldset>
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
