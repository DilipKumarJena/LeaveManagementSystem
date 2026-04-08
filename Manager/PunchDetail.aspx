<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="PunchDetail.aspx.cs" Inherits="PunchDetail" Title="Show Employee Punch Card Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>
    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <br />
    <h1>
        Punch Details</h1>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td valign="middle" style="padding: 5px">
                <fieldset style="padding: 5px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 20%">
                                <b>For The Date :</b>
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtDate" runat="server" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox>
                                <asp:Button ID="btnShowEmployee" runat="server" OnClick="btnShowEmployee_Click" Text="Show Employee" />
                                &nbsp;
                                <asp:Label ID="lblMessage" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 5px">
                                <asp:GridView ID="gvEmployeeList" runat="server" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="OKSLeaveName" HeaderText="OKSLeaveName" />
                                        <asp:BoundField DataField="DateIn" HeaderText="DateIn" />
                                        <asp:BoundField DataField="DateOut" HeaderText="DateOut" />
                                        <asp:BoundField DataField="CabLate" HeaderText="CabLate" />
                                        <asp:BoundField DataField="OutDuty" HeaderText="OutDuty" />
                                        
                                        <asp:BoundField DataField="TotalWorkingHour" HeaderText="TotalWorkingHour" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="3">
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
    <br />
    <br />
    <br />
    <br />
</asp:Content>
