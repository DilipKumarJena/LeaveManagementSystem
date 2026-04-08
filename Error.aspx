<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="Error.aspx.cs" Inherits="Error" Title="Unauthorised Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="background-image: url(Images/MasterPage/body_bg.gif)">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table width="100%">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="border: black 1px solid; width: 80%;" valign="middle">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 150px;">
                        <tr>
                            <td style="width: 160px">
                                <div style="float: left; padding: 5px">
                                    <img src="Images/error.png" alt="Error" style="width: 150px; height: 150px" /></div>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="ErrorMessage" runat="server" Text="You Are Not<br />Authorised To See This Page"
                                    Font-Bold="True" Font-Names="Tahoma" ForeColor="#000099" Font-Size="25px"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%">
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
        <br />
    </div>
</asp:Content>
