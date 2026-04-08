<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePhotoEmployee.aspx.cs"
    Inherits="UpdatePhotoEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-position: right top; background-attachment: scroll; background-image: url(../Images/MasterPage/BG2.gif);
    background-repeat: no-repeat; background-color: White">
    <form id="form1" runat="server">
        <div>
            <div align="left">
                <h1>
                    Update Photo
                </h1>
                <h2>
                    Please Upload The Photo As Per Official Environment.</h2>
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 25%">
                    </td>
                    <td style="width: 50%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td style="width: 50%" align="center">
                                    <img id="SingleImage" runat="server" align="middle" alt="Not Available" style="border-right: black 1px solid;
                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" />
                                </td>
                                <td style="width: 25%" align="center">
                                    <asp:Button ID="btnRemove" runat="server" CssClass="text" Text="Remove" OnClick="btnRemove_Click"
                                        OnClientClick="return AlertOnDelete(this);" /></td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                </td>
                                <td align="center" style="width: 50%">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="errorRed"></asp:Label>&nbsp;</td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:FileUpload ID="fuUpload" runat="server" CssClass="text" Width="337px" /><asp:Button
                                        ID="btnUpload" runat="server" CssClass="text" Text="Upload" OnClick="btnUpload_Click" /></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 25%" align="left" valign="bottom">
                        <a href="#" onclick="parent.parent.GB_hide();">Close</a></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
