<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="OutDutyEmployee.aspx.cs" Inherits="Employee_OutDutyEmployee" Title="Employee Out Duty" %>

<%@ Register Src="../UserControl/TimeSetup.ascx" TagName="TimeSetup" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript">
function Validation()
{
   if( document.getElementById( '<%=txtPurpose.ClientID%>' ).value == "" )
   {
      document.getElementById( '<%=txtPurpose.ClientID%>' ).focus();
      document.getElementById( 'ErrorR' ).style.display = '';
      return false;
   }   
   if( document.getElementById( '<%=txtDate.ClientID%>' ).value == "" )
   {
      document.getElementById( '<%=txtDate.ClientID%>' ).focus();
      document.getElementById( 'Error3' ).style.display = '';
      return false;
   }   
   
}

    </script>

    <br />
    <h1>
        Out Duty : Employee
    </h1>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 20%">
            </td>
            <td style="width: 60%">
                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                    <table width="95%" cellpadding="5px">
                        <tbody>
                            <tr>
                                <td valign="top" align="left" width="20%">
                                    Employee Id</td>
                                <td valign="top" align="left" width="30%">
                                    <asp:Label ID="lblEmployeeId" runat="server" Font-Bold="True"></asp:Label></td>
                                <td valign="top" align="left" width="20%">
                                    Location</td>
                                <td valign="top" align="left" width="20%">
                                    <asp:Label ID="lblLocation" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="20%">
                                    Employee Name</td>
                                <td valign="top" align="left" width="30%">
                                    <asp:Label ID="lblEmployeeName" runat="server" Font-Bold="True"></asp:Label></td>
                                <td valign="top" align="left" width="20%">
                                    Department</td>
                                <td valign="top" align="left" width="20%">
                                    <asp:Label ID="lblDepartmentName" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="20%">
                                    E-Mail ID</td>
                                <td valign="top" align="left" width="30%">
                                    <asp:Label ID="lblEmailID" runat="server" Font-Bold="True" CssClass="Email"></asp:Label></td>
                                <td valign="top" align="left" width="20%">
                                    Designation</td>
                                <td valign="top" align="left" width="20%">
                                    <asp:Label ID="lblDesignation" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="20%">
                                    Out Duty Date</td>
                                <td valign="top" align="left" width="30%">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                        onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox><br />
                                    <span id="Error3" style="display: none; color: red">* Required</span></td>
                                <td valign="top" align="left" width="20%">
                                </td>
                                <td valign="top" align="left" width="30%">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" width="20%">
                                    Time Out</td>
                                <td align="left" valign="top" width="30%">
                                    <uc1:TimeSetup ID="TimeOut" runat="server" />
                                </td>
                                <td align="left" valign="top" width="20%">
                                    Time In</td>
                                <td align="left" valign="top" width="30%">
                                    <uc1:TimeSetup ID="TimeIn" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" colspan="4">
                                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="20%">
                                    Purpose</td>
                                <td valign="top" align="left" colspan="3">
                                    <asp:TextBox onkeydown="textCounter(this,'Count',1000)" ID="txtPurpose" onkeyup="textCounter(this,'Count',1000)"
                                        runat="server" CssClass="text" Width="95%" Height="54px" TextMode="MultiLine"></asp:TextBox><br />
                                    <span id="Count">1000 Character Left</span>
                                    <br />
                                    <span id="ErrorR" style="display: none; color: red">* Required</span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left" width="20%">
                                    &nbsp;</td>
                                <td valign="top" align="left" width="30%">
                                    <asp:Button ID="btnApply" OnClick="btnApply_Click" runat="server" CssClass="text"
                                        Text="APPLY" OnClientClick="return Validation();"></asp:Button>
                                    <asp:Button ID="btnClear" runat="server" CssClass="text" Text="Clear" OnClientClick="return ClearAllOnMasterPage();">
                                    </asp:Button></td>
                                <td style="text-align: left" valign="top" align="left" colspan="2">
                                    <span style="color: red" id="Message" runat="server"></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </td>
            <td style="width: 20%">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 80%">
                            <asp:GridView ID="gvOutDuty" runat="server" Width="100%">
                            </asp:GridView>
                        </td>
                        <td style="width: 10%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
