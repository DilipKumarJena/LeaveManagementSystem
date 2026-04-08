<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="OKS_Education_MusterRollEmployee.aspx.cs" Inherits="OKS_Education_MusterRollEmployee"
    Title="Muster Roll Employee" %>

<%@ Register Src="../../UserControl/SpecialEmployeeWorkingDetail.ascx" TagName="SpecialEmployeeWorkingDetail"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../../JavaScript/BrowserWidth.js"></script>

    <script language="javascript" type="text/javascript" src="../../JavaScript/UpdateProgress.js"></script>

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../../Images/ajax-loader-new__.gif" /><br />
                <img src="../../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <br />
    <h1>
        Report : Muster Roll Employee</h1>
    <table style="width: 100%">
        <tr>
            <td>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 30%" colspan="2">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 25%">
                                                                            Start Date</td>
                                                                        <td style="width: 25%">
                                                                            <asp:TextBox onkeydown="return false" ID="txtStartDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                                onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                                runat="server" CssClass="text" Font-Bold="False"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errorRed"
                                                                                ErrorMessage="* Required" Display="Dynamic" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator></td>
                                                                        <td style="width: 25%">
                                                                            End Date</td>
                                                                        <td style="width: 25%">
                                                                            <asp:TextBox onkeydown="return false" ID="txtEndDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                                onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                                                runat="server" CssClass="text" Font-Bold="False"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="errorRed"
                                                                                ErrorMessage="* Required" Display="Dynamic" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                    Text="Show"></asp:Button>
                                                <asp:Button ID="btnExportToEXCEL" runat="server" CssClass="text" OnClick="btnExportToEXCEL_Click"
                                                    Text="Export To EXCEL" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="width: 30%">
                                <uc1:SpecialEmployeeWorkingDetail ID="SpecialEmployeeWorkingDetail1" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <div id="aa" style="overflow: auto;" <%--onkeyup="scrollDiv(this, 50); return false;"--%>>
                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                    <asp:GridView ID="divReport" runat="server" Width="100%">
                                    </asp:GridView>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
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

    <script type="text/javascript">
    
    var scrollDiv = function(Div,px)
     { 
    
        if (event.keyCode = 39)
        {     
            Div.scrollLeft += px;
        }
        if (event.keyCode = 37)
        {       
            Div.scrollLeft =  Div.scrollLeft - px;
        }          
     }
    </script>

    <script type="text/javascript" language="javascript">
        document.getElementById ('aa').style.width = ((GetBrowserWidth()) - 80 ) + 'px';   
         document.getElementById ('aa').style.height = '200px';
    </script>

</asp:Content>
