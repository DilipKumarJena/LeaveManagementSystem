<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" Title="Reset Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <br />
    <h1>
        Reset Password
    </h1>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 25%">
            </td>
            <td style="width: 50%">
                <fieldset style="padding: 5px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 25%">
                                Old Password</td>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Required"
                                    ControlToValidate="txtOldPassword" CssClass="errorRed" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 25%;">
                                New Password</td>
                            <td style="width: 75%;">
                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Required"
                                    ControlToValidate="txtNewPassword" CssClass="errorRed" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Confirm New Password</td>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtConNewPassword" runat="server" TextMode="Password" MaxLength="10"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mis Match"
                                    ControlToCompare="txtNewPassword" ControlToValidate="txtConNewPassword" CssClass="errorRed"
                                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Required"
                                    ControlToValidate="txtConNewPassword" CssClass="errorRed" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 75%">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Reset" /></td>
                        </tr>
                    </table>
                </fieldset>
                <div id="DivMessage" runat="server" style="text-align: center">
                </div>
            </td>
            <td style="width: 25%">
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
