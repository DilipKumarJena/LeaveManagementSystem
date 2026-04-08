<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="BlockForm.aspx.cs" Inherits="BlockForm" Title="BlockForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td colspan="3">
                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                        <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                            <asp:GridView ID="gvFileList" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowDataBound="gvFileList_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Path" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPath" runat="server" Text='<%# Bind("Path") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="75%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                                TextMode="MultiLine" Width="98%" Height="75px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Block" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID_Second") %>' />
                                            <asp:CheckBox ID="chkBlock" runat="server" Checked='<%# (Eval("Block").ToString()=="True") ? true :  false %>'>
                                            </asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="Text"
                                Text="Submit"></asp:Button>
                            <asp:Label ID="Label1" runat="server" CssClass="errorRed"></asp:Label></fieldset>
                    </div>
                </td>
            </tr>
        </tbody>
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
    <br />
    <br />
</asp:Content>
