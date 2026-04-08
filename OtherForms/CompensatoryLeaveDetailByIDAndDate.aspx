<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompensatoryLeaveDetailByIDAndDate.aspx.cs"
    Inherits="CompensatoryLeaveDetailByIDAndDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divShow" runat="server">
            <fieldset>
                <legend>Earned CompOff Detail</legend>
                <asp:GridView ID="gvDetail" runat="server" Width="100%">
                </asp:GridView>
            </fieldset>
        </div>
        <br />
        <div>
            <fieldset>
                <legend>CompOff Leave Taken Detail</legend>
                <asp:GridView ID="gvTaken" runat="server" Width="100%">
                </asp:GridView>
            </fieldset>
        </div>
    </form>
</body>
</html>
