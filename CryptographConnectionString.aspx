<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CryptographConnectionString.aspx.cs"
    Inherits="CryptographConnectionString" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Page-Enter" content="revealTrans(Duration=0.5,Transition=23)" />
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=0.5,Transition=23)" />
    <link rel="shortcut icon" href="~/favicon.ico" />
    <link rel="icon" href="~/animated_favicon1.gif" type="image/gif" />

    <script type="text/javascript">
    var GB_ROOT_DIR = "greybox/";
    </script>

    <script type="text/javascript" src="greybox/AJS.js"></script>

    <script type="text/javascript" src="greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="greybox/gb_scripts.js"></script>

    <script type="text/javascript" src="JavaScript/BrowserWidth.js"></script>

    <title>OKS - Leave Management System</title>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 60%">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 20%">
                                Key</td>
                            <td style="width: 60%">
                                <asp:TextBox ID="txtKey" runat="server" CssClass="text"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                Connection String</td>
                            <td style="width: 60%">
                                <asp:TextBox ID="txtConnectionstring" runat="server" Height="68px" TextMode="MultiLine"
                                    Width="98%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                Encrypy</td>
                            <td style="width: 60%">
                                <asp:TextBox ID="txtEncrypt" runat="server" Height="68px" TextMode="MultiLine" Width="98%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                Decrypt</td>
                            <td style="width: 60%">
                                <asp:TextBox ID="txtDecrypt" runat="server" Height="68px" TextMode="MultiLine" Width="98%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnEncrypt" runat="server" OnClick="btnEncrypt_Click" Text="Encrypt" />
                                <asp:Button ID="btnDecrypt" runat="server" OnClick="btnDecrypt_Click" Text="Decrypt" /></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20%">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
