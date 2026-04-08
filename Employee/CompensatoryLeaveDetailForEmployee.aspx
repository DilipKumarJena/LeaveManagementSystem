<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="CompensatoryLeaveDetailForEmployee.aspx.cs" Inherits="CompensatoryLeaveDetailForEmployee"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <br />
    <h1>
        Compensatory Leave Detail
    </h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="background-color: Transparent; position: absolute; width: 220px; height: 118px;"
        id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            &nbsp;&nbsp; Status&nbsp; :&nbsp;
            <asp:RadioButtonList ID="rblCompOffType" runat="server" AutoPostBack="True" CssClass="text"
                RepeatDirection="Horizontal" OnSelectedIndexChanged="rblCompOffType_SelectedIndexChanged" Font-Bold="True">
                <asp:ListItem Selected="True" Value="1">Can Avail</asp:ListItem>
                <asp:ListItem Value="2">Locked Intermediate</asp:ListItem>
                <asp:ListItem Value="3">Availed</asp:ListItem>
               <%-- <asp:ListItem Value="4">Paid</asp:ListItem>--%>
                <asp:ListItem Value="0">All</asp:ListItem>
            </asp:RadioButtonList>
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 5%">
                        </td>
                        <td style="width: 90%">
                            <asp:GridView ID="gvDetail" runat="server" Width="100%" OnRowCommand="gvDetail_RowCommand"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="CompOffDate">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbtnDetail" runat="server" Text='<%# Bind("CompOffDate") %>'
                                                CommandName="Detail" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CompOffStatus" HeaderText="Comp Off Status" />
                                    <asp:BoundField DataField="TLName" HeaderText="TL Name" />
                                    <asp:BoundField DataField="TLComment" HeaderText="TL Comment" />                                    
                                    <asp:BoundField DataField="WorkedForDepartment" HeaderText="Worked For Department" />
                                    <asp:BoundField DataField="RemainingBalance" HeaderText="Remaining Balance" />
                                    <asp:BoundField DataField="MDuration" HeaderText="M Duration" />                                    
                                    <asp:BoundField DataField="MName" HeaderText="M Name" />
                                    <asp:BoundField DataField="MStatus" HeaderText="M Status" />
                                    <asp:BoundField DataField="MApprovedOn" HeaderText="M Approved On" />
                                    <asp:BoundField DataField="MComment" HeaderText="M Comment" />
                                    <asp:BoundField DataField="HRApproval" HeaderText="HR Approval" />
                                    <asp:BoundField DataField="HRApprovedOn" HeaderText="HR ApprovedOn" />
                                    <asp:BoundField DataField="HRComment" HeaderText="HR Comment" />
                                    
                                    
                                    
                                    
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                        </td>
                        <td style="width: 90%">
                            <fieldset>
                                <legend>CompOff Leave Detail </legend>
                                <asp:GridView ID="gvLeaveDetail" runat="server" Width="100%">
                                </asp:GridView>
                            </fieldset>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
