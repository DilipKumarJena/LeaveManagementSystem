<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="AddCab.aspx.cs" Inherits="AdminCabModule_AddCab" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JavaScript/TextValidations.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
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
            <table style="width: 100%" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <br />
                            <h1>
                                Administration : Cab Master
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                        </td>
                        <td style="width: 30%">
                            <fieldset style="padding: 5px">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%">
                                                Vehicle Number</td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="txtCabNumber" runat="server" CssClass="text" MaxLength="80"></asp:TextBox><br />
                                                <asp:Label ID="lblCabNumber" runat="server" class="errorRed"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Driver Name</td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="txtDriverName" runat="server" CssClass="text" MaxLength="80"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Driver Mobile Number</td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="text" MaxLength="80"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                Route</td>
                                            <td style="width: 50%">
                                                <asp:TextBox ID="txtRoute" runat="server" CssClass="text" MaxLength="40" Height="85px"
                                                    TextMode="MultiLine" Width="207px"></asp:TextBox><br />
                                                <span style="display: none" id="spanCity" class="errorRed"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                            </td>
                                            <td style="width: 50%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                    Text="Insert" OnClientClick="javascript:return validateForm();"></asp:Button>
                                                <asp:Button ID="btnRefresh" runat="server" CssClass="text" Text="Refresh" OnClientClick="return ClearAllOnMasterPage();"
                                                    OnClick="btnRefresh_Click"></asp:Button></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h2>
                                <span id="spanMessage" runat="server"></span>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px" colspan="3">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 70%">
                                            <fieldset style="padding: 5px">
                                                <asp:GridView ID="gvCabMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowCancelingEdit="gvCabMaster_RowCancelingEdit" OnRowEditing="gvCabMaster_RowEditing"
                                                    OnRowUpdating="gvCabMaster_RowUpdating" OnRowDeleting="gvCabMaster_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="VehicleNumber" ItemStyle-VerticalAlign="top">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtVehicleNumber" runat="server" Text='<%# Bind("VehicleNumber") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVehicleNumber" runat="server" Text='<%# Bind("VehicleNumber") %>'></asp:Label>
                                                                <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DriverName" ItemStyle-VerticalAlign="top">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtgDriverName" runat="server" Text='<%# Bind("DriverName") %>'
                                                                    CssClass="text"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("DriverName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MobileNumber" ItemStyle-VerticalAlign="top">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtgMobileNumber" runat="server" Text='<%# Bind("MobileNumber") %>'
                                                                    CssClass="text"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route" ItemStyle-VerticalAlign="top">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtgRoute" runat="server" Text='<%# Bind("Route") %>' Height="85px"
                                                                    TextMode="MultiLine" Width="207px" CssClass="text"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Route") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" />
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                            </fieldset>
                                        </td>
                                        <td style="width: 15%">
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
</asp:Content>
