<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompensatoryLeaveAdjustment.aspx.cs"
    Inherits="CompensatoryLeaveAdjustment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 15%; height: 19px">
                                    <strong>Start Date</strong></td>
                                <td style="width: 15%; height: 19px">
                                    <asp:Label ID="lblStart" runat="server" CssClass="text" Text="Label"></asp:Label></td>
                                <td style="width: 15%; height: 19px">
                                    <strong>EndDate</strong></td>
                                <td style="width: 15%; height: 19px">
                                    <asp:Label ID="lblEnd" runat="server" CssClass="text" Text="Label"></asp:Label></td>
                                <td style="width: 15%; height: 19px">
                                    <strong>Type</strong></td>
                                <td style="width: 15%; height: 19px">
                                    <asp:Label ID="lblType" runat="server" CssClass="text" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; height: 19px;">
                                    <strong>Requested Hour</strong></td>
                                <td style="width: 15%; height: 19px;">
                                    <asp:Label ID="lblRequestedHour" runat="server" CssClass="text" Text="Label"></asp:Label></td>
                                <td style="width: 15%; height: 19px;">
                                </td>
                                <td style="width: 15%; height: 19px;">
                                </td>
                                <td style="width: 15%; height: 19px;">
                                </td>
                                <td style="width: 15%; height: 19px;">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <asp:GridView ID="gvCompOffAdjustment" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvCompOffAdjustment_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TLComment" HeaderText="TLComment" />
                                <asp:BoundField DataField="Hours" HeaderText="Hours" />
                                <asp:BoundField DataField="RemainingBalance" HeaderText="RemainingBalance" />
                                <asp:TemplateField HeaderText="Requested Hour From CompOff">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtHours" runat="server" Width="46px" onkeypress="return NumberOnly()"
                                            CssClass="text" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkHours" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 60%">
                        <asp:Button ID="btnValidate" runat="server" CssClass="text" OnClick="btnValidate_Click"
                            Text="Validate" />&nbsp;
                        <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label></td>
                    <td style="width: 20%">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
