<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="CompensatoryLeaveAccounts.aspx.cs" Inherits="CompensatoryLeaveAccounts"
    Title="Update Compensatory Leave Accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/BrowserWidth.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <br />
    <h1>
        Compensatory Leave : Accounts</h1>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="padding: 5px">
                        <fieldset style="padding: 5px">
                            <h2>
                                <div id="Error" runat="server">
                                </div>
                            </h2>
                            <br />
                            <br />
                            <div id="Hide" runat="server">
                                Select All &nbsp;&nbsp; : &nbsp;
                                <asp:DropDownList ID="ddlSelectAll" runat="server" CssClass="text" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSelectAll_SelectedIndexChanged">
                                    <asp:ListItem Text="<-- Select -->" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />
                                <div id="aa" style="overflow: auto;">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:GridView ID="gvEmployeeList" runat="server" Width="100%" CssClass="GridViewStyle"
                                                    GridLines="None" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Approval">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlApproval" runat="server" CssClass="text">
                                                                    <asp:ListItem Text="<-- Select -->" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("EMPName") %>'></asp:Label>
                                                                <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CompOffDate" HeaderText="Comp Off Date" />
                                                        <asp:BoundField DataField="WorkedForDepartment" HeaderText="Worked For Department" />
                                                        <asp:BoundField DataField="OKSLeaveName" HeaderText="OKS Leave Name" />
                                                        <asp:BoundField DataField="TLNAme" HeaderText="TL NAme" />
                                                        <asp:BoundField DataField="TLDuration" HeaderText="TL Duration" />
                                                        <asp:BoundField DataField="TLComment" HeaderText="TL Comment" />
                                                        <asp:BoundField DataField="ManagerName" HeaderText="Manager Name" />
                                                        <asp:BoundField DataField="MDuration" HeaderText="M Duration" />
                                                        <asp:BoundField DataField="MComment" HeaderText="M Comment" />
                                                        <asp:BoundField DataField="DateIn" HeaderText="Date In" />
                                                        <asp:BoundField DataField="DateOut" HeaderText="Date Out" />
                                                        <asp:BoundField DataField="CabLate" HeaderText="CabLate" />
                                                        <asp:BoundField DataField="OutDuty" HeaderText="OutDuty" />
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
                                                            <asp:TextBox ID="txtHRComment" runat="server" Height="116px" Width="98%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%">
                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="text" Text="Submit" OnClick="btnSubmit_Click" />
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                <ProgressTemplate>
                                                                    <img src="../Images/ajax-loader-new__.gif" /><br />
                                                                    <img src="../Images/PleaseWait.gif" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                        <td style="width: 80%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </fieldset>
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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <script type="text/javascript" language="javascript">
        document.getElementById ('aa').style.width = ((GetBrowserWidth()) - 60 ) + 'px';   
    </script>

</asp:Content>
