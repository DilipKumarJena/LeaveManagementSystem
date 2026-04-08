<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorNormalPage.aspx.cs" Inherits="ErrorNormalPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
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
                                <asp:Label ID="ErrorMessage" runat="server" Text="Please Enable Java Script <br />To Use This Web Site"
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
    </form>
</body>
</html>
