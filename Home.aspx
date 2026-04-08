<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" Title="Welcome" %>

<%@ Register Src="UserControl/LeaveCalendar.ascx" TagName="LeaveCalendar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />

    <script language="javascript" type="text/javascript" src="JavaScript/UpdateProgress.js"></script>

    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" AsyncPostBackTimeout="360000"
        runat="server">
    </asp:ScriptManager>
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="Images/ajax-loader-new__.gif" /><br />
                <img src="Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
          <%--  <marquee style="font-size: medium; font-weight: bolder; color: Blue">
                Please Check Out Your Attendance Status Regularly. After 25<sup>th</sup> HR Will
                Not Be Responsible For Any Changes...</marquee>--%>
            <br />
            <br />
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 10%;">
                    </td>
                    <td colspan="2">
                        <uc1:LeaveCalendar ID="LeaveCalendar1" runat="server"></uc1:LeaveCalendar>
                        <br />
                    </td>
                    <td style="width: 10%;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td colspan="2" valign="top">
                        <table cellpadding="10" cellspacing="10" style="width: 100%">
                            <tr>
                                <td style="width: 50%" valign="top">
                                    <fieldset>
                                        <legend>
                                            <h3>
                                                Pending Status
                                            </h3>
                                        </legend>
                                        <asp:GridView ID="gvPendingDetail" runat="server" Width="100%" ForeColor="Black"
                                            AutoGenerateColumns="true">
                                        </asp:GridView>
                                    </fieldset>
                                </td>
                                <td style="width: 50%" valign="top">
                                    <div id="divLeaveBalance" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
            </table>
            <br />
            <br />

            <script type="text/javascript" src="Manager/MasterPanel/jquery-1.2.6.min.js"></script>

            <script type="text/javascript" src="Manager/MasterPanel/jquery-ui-personalized-1.6rc2.min.js"></script>

            <script type="text/javascript" src="Manager/MasterPanel/inettuts.js"></script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
