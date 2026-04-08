<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="ShowSystemInOutTimeForPunchUpdate.aspx.cs" Inherits="ShowSystemInOutTimeForPunchUpdate"
    Title="System Login Found : Punch Not Found" %>

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
    <div style="background-color: Transparent; position: absolute; width: 220px; height: 118px;"
        id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <h1>
        System Login Found : Punch Not Found</h1>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tbody>
                    <tr>
                        <td align="center">
                        </td>
                    </tr>
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
                                                                                    Date</td>
                                                                                <td style="width: 25%">
                                                                                    <asp:TextBox onkeydown="return false" ID="txtStartDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                                        onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                                        runat="server" CssClass="text" Font-Bold="False"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required"
                                                                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center" colspan="2">
                                                                    &nbsp;
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
                                        <td style="width: 30%" align="center" colspan="2">
                                            <h2>
                                                <div id="Error" runat="server">
                                                </div>
                                            </h2>
                                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                Text="Show"></asp:Button>
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
                                                            <asp:GridView ID="gvEmployeePunchDetail" runat="server" AutoGenerateColumns="False"
                                                                Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="EmpCode">
                                                                        <ItemTemplate>
                                                                            <a class="Navigation" onmouseover="window.status='http://www.oksgroup.com';return true"
                                                                                href='<%# "../OtherForms/ShowSystemLoginTime.aspx?ID="+Eval("ID") %>' title="Show System Login Time"
                                                                                onclick="return GB_showCenter('   *   OKS Group   *   Show System Login Time', this.href,415,<%= Session["BrowserWidth"] %> )"
                                                                                style="color: Black">
                                                                                <%# Eval("EmpCode")%>
                                                                            </a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <a class="Navigation" onmouseover="window.status='http://www.oksgroup.com';return true"
                                                                                href='<%# "../HR/FloorMovement.aspx?ID="+Eval("ID") %>' title="Show Floor Movement"
                                                                                onclick="return GB_showCenter('   *   OKS Group   *   Show Floor Movement', this.href,415,<%= Session["BrowserWidth"] %> )"
                                                                                style="color: Black">
                                                                                <%# Eval("Name") %>
                                                                                -
                                                                                <%# Eval("FM") %>
                                                                            </a>
                                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Location" DataField="Location" />
                                                                    <asp:BoundField HeaderText="Department" DataField="Department" />
                                                                    <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                                                    <asp:BoundField HeaderText="Shift" DataField="Shift" />
                                                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                                                    <asp:BoundField HeaderText="HolidayName" DataField="HolidayName" />
                                                                    <asp:TemplateField HeaderText="SystemLogInTime">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDateIn" runat="server" Text='<%# Bind("SystemLogInTime") %>'
                                                                                CssClass="text"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SystemLogOutTime">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDateOut" runat="server" Text='<%# Bind("SystemLogOutTime") %>'
                                                                                CssClass="text"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkCheck" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle">
                                                            Comment :
                                                            <asp:TextBox ID="txtComment" runat="server" Height="75px" TextMode="MultiLine" Width="377px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnUpdate" runat="server" CssClass="text" OnClick="btnUpdate_Click"
                                                                Text="UpdateAll" /></td>
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
                </tbody>
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
