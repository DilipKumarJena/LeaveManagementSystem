<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompensatoryLeaveAccountsApproval.ascx.cs"
    Inherits="CompensatoryLeaveAccountsApproval" %>
<br />
<table cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 5%">
        </td>
        <td style="width: 90%">
        </td>
        <td style="width: 5%">
        </td>
    </tr>
    <tr>
        <td style="width: 5%">
            &nbsp;</td>
        <td style="width: 90%">
            &nbsp;Pending Compensatory Leave: &nbsp;<asp:Label ID="lblCompensatoryLeave" runat="server"></asp:Label>
        </td>
        <td style="width: 5%">
        </td>
    </tr>
    <tr>
        <td style="width: 5%">
        </td>
        <td style="width: 90%">
            <asp:GridView ID="gvCompOff" runat="server" AutoGenerateColumns="False" ForeColor="Black"
                Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <a id="A1" runat="server" href='<%# "../../CompensatoryLeaveAccounts.aspx?DateBind="+Eval("CompOffDateB") + ";" + Eval("DepartmentName") %>'
                                onmouseover="window.status='http://www.oksgroup.com';return true" style="color: Maroon;">
                                <%# Eval("CompOffDate")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Pending" HeaderText="Pending" />
                </Columns>
            </asp:GridView>
        </td>
        <td style="width: 5%">
        </td>
    </tr>
</table>
<br />

<script type="text/javascript" language="javascript" src="~/JavaScript/TextValidations.js"></script>

