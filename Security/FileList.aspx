<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="FileList.aspx.cs" Inherits="Security_FileList" Title="File List For Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    &nbsp;<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server">
    </asp:ScriptManager>
    <div id="UpdateProgress" align="center" style="display: none; width: 220px; position: absolute;
        height: 118px; background-color: transparent">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td style="height: 40px" width="10%">
                        </td>
                        <td style="height: 40px" valign="middle" align="center" width="80%">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 30%">
                                        </td>
                                        <td style="width: 30%" colspan="2">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" colspan="2">
                                                            <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 50%" align="left">
                                                                                Location</td>
                                                                            <td style="width: 50%" align="left">
                                                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="text" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                                                </asp:DropDownList><br />
                                                                                <span style="display: none; color: red" id="Error00"></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 50%" align="left">
                                                                                Department</td>
                                                                            <td style="width: 50%" align="left">
                                                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="text" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                                                </asp:DropDownList><br />
                                                                                <span style="display: none; color: red" id="Error1"></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 50%" align="left">
                                                                                Designation</td>
                                                                            <td style="width: 50%" align="left">
                                                                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="text" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                                                                </asp:DropDownList><br />
                                                                                <span style="display: none; color: red" id="Error2"></span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 50%" align="left">
                                                                                Employee</td>
                                                                            <td style="width: 50%" align="left">
                                                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="text" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td style="width: 30%">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td style="height: 40px" width="10%">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px" width="10%">
                        </td>
                        <td style="height: 40px" valign="middle" align="center" width="80%">
                            <asp:CheckBox ID="chkTeamLeader" runat="server" Text="TeamLeader" CssClass="text"
                                OnCheckedChanged="chkTeamLeader_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                            <asp:CheckBox ID="chkManager" runat="server" Text="Manager" CssClass="text" OnCheckedChanged="chkManager_CheckedChanged" AutoPostBack="True">
                            </asp:CheckBox>
                            <asp:CheckBox ID="chkHR" runat="server" Text="HR" CssClass="text" OnCheckedChanged="chkHR_CheckedChanged" AutoPostBack="True">
                            </asp:CheckBox>
                            <asp:CheckBox ID="chkOtherForms" runat="server" Text="OtherForms" CssClass="text"
                                OnCheckedChanged="chkOtherForms_CheckedChanged" AutoPostBack="True"></asp:CheckBox></td>
                        <td style="height: 40px" width="10%">
                        </td>
                    </tr>
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
                                                        TextMode="MultiLine" Width="98%" Height="30px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Access" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID_Second") %>' />
                                                    <asp:CheckBox ID="chkAccess" runat="server" Checked='<%# (Eval("Access_Second").ToString()=="True") ? true :  false %>'>
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="Text"
                                        Text="Submit" Visible="false"></asp:Button>
                                    <asp:Label ID="Label1" runat="server" CssClass="errorRed"></asp:Label></fieldset>
                            </div>
                        </td>
                    </tr>
                </tbody>
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
    <br />
    <br />
</asp:Content>
