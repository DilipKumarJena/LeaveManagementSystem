<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="CompensatoryLeaveManager.aspx.cs" Inherits="CompensatoryLeaveManager"
    Title="Update Compensatory Leave Manager" %>

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
    <h1>
        Compensatory Leave : Manager</h1>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding: 5px">
                <fieldset style="padding: 5px">
                    <h2>
                        <div id="Error" runat="server">
                        </div>
                    </h2>
                    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
                    </asp:ScriptManager>
                    <div id="UpdateProgress" align="center" style="background-color: Transparent; position: absolute;
                        display: none; width: 220px; height: 118px;">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="../Images/ajax-loader-new__.gif" /><br />
                                <img src="../Images/PleaseWait.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="Hide" runat="server">
                                Select All :
                                <asp:DropDownList ID="ddlApproval1" CssClass="text" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlApproval1_SelectedIndexChanged">
                                    <asp:ListItem Text="<-- Select -->" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:GridView ID="gvEmployeeList" runat="server" Width="100%" CssClass="GridViewStyle"
                                                GridLines="None" AutoGenerateColumns="False" OnRowDataBound="gvEmployeeList_RowDataBound">
                                                <RowStyle CssClass="RowStyle" />
                                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                <HeaderStyle CssClass="HeaderStyle" />
                                                <EditRowStyle CssClass="EditRowStyle" />
                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Approval">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlApproval" CssClass="text" runat="server">
                                                                <asp:ListItem Text="<-- Select -->" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <a href="../OtherForms/CompensatoryLeaveDetail.aspx"></a><a class="Navigation" onmouseover="window.status='http://www.oksgroup.com';return true"
                                                                href='<%# "../OtherForms/CompensatoryLeaveDetail.aspx?EmployeeID="+Eval("EmployeeID") %>'
                                                                title="View Compensatory Leave Detail" onclick="return GB_showCenter('   *   OKS Group   *   View Compensatory Leave Detail', this.href,393,<%= Session["BrowserWidth"] %>)">
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("EMPName") %>'></asp:Label>
                                                            </a>
                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TLNAme" HeaderText="TL NAme" />
                                                    <asp:TemplateField HeaderText="Comp Off Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompOffDate" runat="server" Text='<%# Bind("CompOffDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WorkedForDepartment" HeaderText="Worked For Department" />
                                                    <asp:BoundField DataField="TLComment" HeaderText="TL Comment" />
                                                    <asp:TemplateField HeaderText="Approved Duration TL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Bind("Duration") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approval">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlApprovalDurationManager" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DateIn" HeaderText="Date In" />
                                                    <asp:BoundField DataField="DateOut" HeaderText="Date Out" />
                                                    <asp:BoundField DataField="TotalWorkingHour" HeaderText="Total Working Hour" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="width: 20%">
                                                        Comment :
                                                    </td>
                                                    <td style="width: 80%">
                                                        <asp:TextBox ID="txtManagerComment" runat="server" Height="116px" Width="98%" CssClass="text"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                            CssClass="text" /></td>
                                                    <td style="width: 80%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
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
    <br />
    <br />
    <br />
    <br />
</asp:Content>
