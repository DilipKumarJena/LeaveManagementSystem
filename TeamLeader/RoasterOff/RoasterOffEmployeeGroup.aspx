<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="RoasterOffEmployeeGroup.aspx.cs" Inherits="RoasterOffEmployeeGroup"
    Title="Roaster Off Employee Grouping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../../JavaScript/BrowserWidth.js"></script>

    <script language="javascript" type="text/javascript" src="../../JavaScript/UpdateProgress.js"></script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../../Images/ajax-loader-new__.gif" /><br />
                <img src="../../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <br />
    <h1>
        Set Roaster Off Employee Grouping</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 30%">
                                    </td>
                                    <td style="width: 30%" colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" colspan="2">
                                                    <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium"></asp:Label>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </td>
                                    <td style="width: 30%">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div id="aa" style="overflow: auto;">
                                        <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                            <asp:GridView ID="divDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                OnRowDataBound="divDetail_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="EmpCode">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                            <asp:HiddenField ID="hfRoGroup" runat="server" Value='<%# Bind("RoGroup") %>' />
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmpCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Detail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roaster Off Group">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlRoGroup" runat="server" CssClass="text">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </fieldset>
                                        <asp:Button ID="btnInsertUpdate" OnClick="btnInsertUpdate_Click" runat="server" CssClass="text"
                                            Text="Insert Update" />
                                    </div>
                                </td>
                            </tr>
                        </table>
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

    <script type="text/javascript" language="javascript">
        document.getElementById ('aa').style.width = ((GetBrowserWidth()) - 80 ) + 'px';   
    </script>

</asp:Content>
