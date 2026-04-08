<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="LeavePost.aspx.cs" Inherits="LeavePost" Title="Leave Post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/TextValidations.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/RollOverEffect.js"></script>

    <script type="text/javascript" language="javascript">
    
    
    
function Test(cb, div)
{

var check = document.getElementById ('ctl00$ContentPlaceHolder1$' + cb);

if(check.checked == true)
document.getElementById(div).style.display  = '';
else
   document.getElementById(div).style.display  = 'none';
} 
    
    
function Validation()
{
   if( document.getElementById( '<%=txtReason.ClientID %>' ).value == "" )
   {
      document.getElementById( '<%=txtReason.ClientID %>' ).focus();
      document.getElementById( 'ErrorR' ).style.display = '';
      return false;
   }
   if( document.getElementById( '<%=txtAddress.ClientID %>' ).value == "" )
   {
      document.getElementById( '<%=txtAddress.ClientID %>' ).focus();
      document.getElementById( 'ErrorA' ).style.display = '';
      return false;
   }
   
}



function ValidationAdd()
{
   document.getElementById( 'Error1' ).style.display = 'none';
   document.getElementById( 'Error2' ).style.display = 'none';
   document.getElementById( 'Error3' ).style.display = 'none';
   document.getElementById( 'Error4' ).style.display = 'none';


   if( document.getElementById( '<%=ddlHalfFullDay.ClientID %>' ).value == "0" )
   {
      document.getElementById( '<%=ddlHalfFullDay.ClientID %>' ).focus();
      document.getElementById( 'Error1' ).style.display = '';
      return false;
   }


   if( document.getElementById( '<%=ddlFirstSecond.ClientID %>' ).selectedIndex == 0 )
   {
      if( document.getElementById( '<%=ddlHalfFullDay.ClientID %>' ).selectedIndex == 2 )
      {
         document.getElementById( '<%=ddlFirstSecond.ClientID %>' ).focus();
         document.getElementById( 'Error2' ).style.display = '';
         return false;
      }
   }
   if( document.getElementById( '<%=txtFromDate.ClientID %>' ).value == "" )
   {
      document.getElementById( '<%=txtFromDate.ClientID %>' ).focus();
      document.getElementById( 'Error3' ).style.display = '';
      return false;
   }
   if( document.getElementById( '<%=ddlLeaveType.ClientID %>' ).value == "0" )
   {
      document.getElementById( '<%=ddlLeaveType.ClientID %>' ).focus();
      document.getElementById( 'Error4' ).style.display = '';
      return false;
   }
}



function ShowFirstSecond()
{
document.getElementById( '<%=ddlFirstSecond.ClientID %>' ).value = 0
if( document.getElementById( '<%=ddlHalfFullDay.ClientID %>' ).selectedIndex == 2 )
   {
      document.getElementById('SpanFirstSecond').style.display  = '';
      document.getElementById('DivFirstSecond').style.display  = '';
      
      
       document.getElementById('SpanFromDate').style.display  = 'none'; 
        document.getElementById('DivToDate').style.display  = 'none'; 
      
   }
   else
   {
      document.getElementById('SpanFirstSecond').style.display  = 'none';
      document.getElementById('DivFirstSecond').style.display  = 'none';
      
      
       document.getElementById('SpanFromDate').style.display  = ''; 
        document.getElementById('DivToDate').style.display  = ''; 
   }    
}    
    </script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <br />
    <h1>
        Post Leave</h1>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <br />
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td width="15%">
                                    </td>
                                    <td style="width: 70%" align="center">
                                        <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                            <table width="95%">
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
                                                        </td>
                                                        <td valign="top" align="left" width="30%">
                                                        </td>
                                                        <td valign="top" align="left" width="20%">
                                                            Current Date</td>
                                                        <td valign="top" align="left" width="30%">
                                                            <asp:Label ID="lblCurrentDate" runat="server" Font-Bold="True"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" width="20%">
                                                            First Level</td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:Label ID="lblFirst" runat="server" Font-Bold="True"></asp:Label></td>
                                                        <td align="left" valign="top" width="20%">
                                                            Second Level</td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:Label ID="lblSecond" runat="server" Font-Bold="True"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" width="20%">
                                                            Status</td>
                                                        <td valign="top" align="left" width="30%">
                                                            <asp:DropDownList ID="ddlHalfFullDay" runat="server" CssClass="text" onchange="ShowFirstSecond();">
                                                            </asp:DropDownList><br />
                                                            <span style="display: none; color: red" id="Error1">* Required</span>
                                                        </td>
                                                        <td valign="top" align="left" width="20%">
                                                            <span style="display: none" id="SpanFirstSecond">First Half / Second Half</span></td>
                                                        <td valign="top" align="left" width="30%">
                                                            <div style="display: none" id="DivFirstSecond">
                                                                <asp:DropDownList ID="ddlFirstSecond" runat="server" CssClass="text">
                                                                </asp:DropDownList><br />
                                                                <span style="display: none; color: red" id="Error2">* Required</span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" width="20%">
                                                            From Date</td>
                                                        <td valign="top" align="left" width="30%">
                                                            <asp:TextBox onkeydown="return false" ID="txtFromDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                runat="server" Font-Bold="False" CssClass="text"></asp:TextBox><br />
                                                            <span style="display: none; color: red" id="Error3">* Required</span> <span style="display: none;
                                                                color: red" id="Error5"></span>
                                                        </td>
                                                        <td valign="top" align="left" width="20%">
                                                            <span id="SpanFromDate">To Date</span>
                                                        </td>
                                                        <td valign="top" align="left" width="30%">
                                                            <div id="DivToDate">
                                                                <asp:TextBox onkeydown="return false" ID="txtToDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                    onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                    runat="server" Font-Bold="False" CssClass="text"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" width="20%">
                                                            Leave Type</td>
                                                        <td valign="top" align="left" width="30%">
                                                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="text">
                                                            </asp:DropDownList><br />
                                                            <span style="display: none; color: red" id="Error4">* Required</span>
                                                        </td>
                                                        <td valign="middle" align="center" colspan="2">
                                                            &nbsp;<asp:Button ID="btnAddLeave" OnClick="btnAddLeave_Click" runat="server" CssClass="text"
                                                                Text="  Add  " OnClientClick="return ValidationAdd();"></asp:Button>&nbsp;<asp:Button
                                                                    ID="btnClearAppliedLeave" OnClick="btnClearAppliedLeave_Click" runat="server"
                                                                    CssClass="text" Text="Clear Applied Leave"></asp:Button>
                                                            &nbsp; &nbsp; <a id="aMusterRoll" runat="server" class="Navigation" onmouseover="window.status='http://www.oksgroup.com';return true"
                                                                title="Show Muster Roll" onclick="return GB_showCenter('   *   OKS Group   *   Show Muster Roll', this.href,400,500 )"
                                                                style="color: Black">
                                                                <asp:HiddenField ID="hfApplyHalfDayLWP" runat="server" Value="0" />
                                                                Show Muster Roll </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" width="20%">
                                                        </td>
                                                        <td align="left" valign="top" width="30%">
                                                        </td>
                                                        <td align="center" colspan="2" valign="middle">
                                                            <div id="flashingtext" runat="server">
                                                                * Click On Add Button To Show Apply Button
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" colspan="4">
                                                            <asp:Label ID="lblMessage" runat="server" BackColor="Black" BorderColor="Green" BorderStyle="Groove"
                                                                BorderWidth="2px" Font-Bold="True" ForeColor="#FFFFC0" Font-Size="Medium"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" colspan="4">
                                                            <div style="border-right: #000000 thin solid; padding-right: 5px; border-top: #000000 thin solid;
                                                                padding-left: 5px; padding-bottom: 5px; border-left: #000000 thin solid; padding-top: 5px;
                                                                border-bottom: #000000 thin solid" id="CompensatoryLeave">
                                                                <asp:GridView ID="gvLeaveDetail" runat="server" Width="100%" OnRowDataBound="gvLeaveDetail_RowDataBound">
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" colspan="4">
                                                            <strong><span style="color: red">If Applied For One Day Plz Do Not Fill (To Date)</span></strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" width="20%">
                                                            Reason</td>
                                                        <td valign="top" align="left" colspan="3">
                                                            <asp:TextBox onkeydown="textCounter(this,'Count',100)" ID="txtReason" onkeyup="textCounter(this,'Count',100)"
                                                                runat="server" CssClass="text" Width="95%" Height="54px" TextMode="MultiLine"></asp:TextBox><br />
                                                            <span id="Count">100 Character Left</span>
                                                            <br />
                                                            <span id="ErrorR" style="display: none; color: red">* Required</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" width="20%">
                                                            Address During Leave Period<br />
                                                            <asp:DropDownList ID="ddlAddress" runat="server" AutoPostBack="True" CssClass="text"
                                                                OnSelectedIndexChanged="ddlAddress_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">&lt;-- Select --&gt;</asp:ListItem>
                                                                <asp:ListItem Value="1">Permanent Address</asp:ListItem>
                                                                <asp:ListItem Value="2">Current Address</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td valign="top" align="left" colspan="3">
                                                            <asp:TextBox onkeydown="textCounter(this,'Span1',100)" ID="txtAddress" onkeyup="textCounter(this,'Span1',100)"
                                                                runat="server" CssClass="text" Width="95%" Height="54px" TextMode="MultiLine"></asp:TextBox>
                                                            <br />
                                                            <span id="Span1">100 Character Left</span>
                                                            <br />
                                                            <span id="ErrorA" style="display: none; color: red">* Required</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left" width="20%">
                                                            &nbsp;</td>
                                                        <td valign="top" align="left" width="30%">
                                                            <asp:Button ID="btnApply" OnClick="btnApply_Click" runat="server" CssClass="text"
                                                                Text="APPLY" OnClientClick="return Validation();" Visible="False"></asp:Button>
                                                            <asp:Button ID="btnClear" runat="server" CssClass="text" Text="Clear" OnClientClick="return ClearAllOnMasterPage();">
                                                            </asp:Button></td>
                                                        <td style="text-align: left" valign="top" align="left" colspan="2">
                                                            <span style="color: red" id="LeavePostStatus" runat="server"></span>
                                                            <br />
                                                            <span style="color: red" id="MessageSentStatus" runat="server"></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            <strong>Note:- This Form Detail Will Send to HOD via E-Mail</strong></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td width="15%">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 5%" align="center" rowspan="3" valign="top">
                                                    <img alt="Click Checkbox To Show Details" src="../Images/RightArrow.png" style="width: 54px;
                                                        height: 54px" title="Click Checkbox To Show Details" /></td>
                                                <td style="width: 90%">
                                                    <asp:CheckBox ID="chkShowDetail" runat="server" Text="ShowDetail" OnCheckedChanged="chkShowDetail_CheckedChanged"
                                                        AutoPostBack="True"></asp:CheckBox><br />
                                                    <div id="TableShowLeaveBalance" style="width: 100%" runat="server" visible="false">
                                                        <span id="aa" runat="server"></span>
                                                        <asp:LinkButton ID="lkbtnDetail" runat="server" OnClick="lkbtnDetail_Click"> Compensatory Leave Detail</asp:LinkButton>
                                                    </div>
                                                </td>
                                                <td style="width: 5%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 90%">
                                                    <asp:CheckBox ID="chkDeletePostLeave" runat="server" Text="Show Posted Leave" OnCheckedChanged="chkDeletePostLeave_CheckedChanged"
                                                        AutoPostBack="True"></asp:CheckBox>
                                                    <asp:Label ID="lblCancelMessage" runat="server" CssClass="errorRed"></asp:Label><br />
                                                    <div style="width: 100%" id="DeletePostedLeave" runat="server" visible="false">
                                                        <table style="width: 100%; border: solid 1px black">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Repeater ID="repCancelPostedLeave" runat="server" OnItemCommand="repCancelPostedLeave_ItemCommand">
                                                                            <HeaderTemplate>
                                                                                <tr style="background-color: #5D7B9D; font-weight: bold; color: Wheat;">
                                                                                    <td align="center">
                                                                                        AppliedOn
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Leave Name
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        HalfFull
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        FirstSecond
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        From
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        To
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Deducted
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <%#Eval("AppliedOn")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <%#Eval("LeaveName")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <%#Eval("HalfFull")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <%#Eval("FirstSecond")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <%#Eval("F")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <%#Eval("T")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <%#Eval("Deducted")%>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <div id="divDelete" runat="server" align="center">
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete.png" CommandName="CancelPostedLeave"
                                                                                                CommandArgument='<%#Eval("ID") %>' OnClientClick="return AlertOnDelete(this);"
                                                                                                ToolTip="Click To Cancel Leave" />
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="width: 5%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 90%">
                                                    <asp:CheckBox ID="chkShowAllLeaveDetail" runat="server" Text="Show All Leave Detail"
                                                        OnCheckedChanged="chkShowAllLeaveDetail_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                    <br />
                                                    <div style="width: 100%" id="ShowAllLeaveDetail" runat="server" visible="false">
                                                        <asp:RadioButtonList ID="rblLeave" runat="server" AutoPostBack="True" CssClass="text"
                                                            OnSelectedIndexChanged="rblLeave_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="0">Current Year</asp:ListItem>
                                                            <asp:ListItem Value="1">All</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <span id="spanShowAllLeaveDetail" runat="server"></span>
                                                    </div>
                                                </td>
                                                <td style="width: 5%">
                                                </td>
                                            </tr>
                                        </table>
                                       
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />

    <script language="javascript" type="text/javascript">
var hexvalues = Array( "A", "B", "C", "D", "E", "F", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" );          
function flashtext() 
{
   var colour = '#';            
    for( var counter = 1; counter <= 6; counter ++ ) 
    {                
        var hexvalue = hexvalues[ Math.floor( hexvalues.length * Math.random() ) ];                
        colour = colour + hexvalue;                
    } 
    
     var Backcolour = '#';            
    for( var counter = 1; counter <= 6; counter ++ ) 
    {                
        var hexvalue = hexvalues[ Math.floor( hexvalues.length * Math.random() ) ];                
        Backcolour = Backcolour + hexvalue;                
    }                
    
                   
    document.getElementById('<%= flashingtext.ClientID %>').style.color = colour;            
    document.getElementById('<%= flashingtext.ClientID %>').style.fontSize = '15px'
    document.getElementById('<%= flashingtext.ClientID %>').style.fontWeight = 'bold'
    //document.getElementById('<%= flashingtext.ClientID %>').style.backgroundColor  = Backcolour
    

}             
setInterval( 'flashtext()', 500 );
        
    </script>

</asp:Content>
