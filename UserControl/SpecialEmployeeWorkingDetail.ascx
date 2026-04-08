<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialEmployeeWorkingDetail.ascx.cs"
    Inherits="UserControl_SpecialEmployeeWorkingDetail" %>
<asp:GridView ID="gvDetail" runat="server" OnRowDataBound="gvDetail_RowDataBound"
    ForeColor="Black" Width="100%" AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField HeaderText="Sno">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                <asp:HiddenField ID="hfDate" runat="server" Value='<%# Bind("Date") %>'></asp:HiddenField>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Week" HeaderText="Week" />
        <asp:BoundField DataField="Status" HeaderText="Status" />
    </Columns>
</asp:GridView>
