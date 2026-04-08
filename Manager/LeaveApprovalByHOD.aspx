<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="LeaveApprovalByHOD.aspx.cs" Inherits="LeaveApprovalByHOD" Title="Leave Approval : Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <br />
    <h1>
        Leave Approval : Head Of Department</h1>
    <br />
    <br />
    <h2>
        <div id="DivMessage" runat="server" style="text-align: center">
        </div>
    </h2>
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server">
    </asp:ScriptManager>
    <br />
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="ShodDiv" runat="server" style="padding: 3px">
                <fieldset style="padding: 5px">
                    <asp:GridView ID="gvLeaveDetail" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="gvLeaveDetail_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Emp Name">
                                <ItemTemplate>
                                    <a onmouseover="window.status='http://www.oksgroup.com';return true" class="Navigation"
                                        href='<%# "../OtherForms/LeaveBalanceAppliedAndApprovedLeave.aspx?ID="+Eval("ID") %>'
                                        title="View Leave Balance, Applied And Approved Leave" onclick="return GB_showCenter('   *   OKS Group   *   View Leave Balance And Applied Leave', this.href,500,<%= Session["BrowserWidth"] %>)">
                                        <%# Eval("Name") %>
                                    </a>
                                    <asp:HiddenField ID="hfID" runat="server" Value='<%#Bind("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                            <asp:BoundField DataField="Reason" HeaderText="Reason" />
                            <asp:BoundField DataField="TLName" HeaderText="TLName" />
                            <asp:BoundField DataField="ApprovedStatusTL" HeaderText="ApprovedStatusTL" />
                            <asp:TemplateField HeaderText="Applied Leave Detail">
                                <ItemTemplate>
                                    <asp:GridView ID="gvAppliedLeave" runat="server" Width="100%">
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlApproval" runat="server" AutoPostBack="true" CssClass="text"
                                        OnSelectedIndexChanged="ddlApproval_OnSelectedIndexChanged">
                                        <asp:ListItem Text="<-- Select -->" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <div id="divStatus" runat="server" visible="false">
                                        <asp:TextBox ID="txtRemarkHOD" TextMode="MultiLine" Width="100%" Height="50px" CssClass="text"
                                            runat="server"></asp:TextBox><br />
                                        <asp:Label ID="lblRequired" runat="server" Visible="false" CssClass="errorRed"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="text-align: center">
                        <asp:Button ID="btnApproval" runat="server" OnClick="btnApproval_Click" Text="Update Approval" /></div>
                </fieldset>
            </div>
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
</asp:Content>
