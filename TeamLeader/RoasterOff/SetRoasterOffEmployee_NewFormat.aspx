<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="SetRoasterOffEmployee_NewFormat.aspx.cs" Inherits="SetRoasterOffEmployee_NewFormat"
    Title="Set Roaster Off Employee" %>

<%@ Register Src="../../UserControl/MonthYear.ascx" TagName="MonthYear" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../../JavaScript/TextValidations.js"></script>

    <script type="text/javascript" language="javascript" src="../../JavaScript/calendar.js"></script>

    <script type="text/javascript" language="javascript" src="../../JavaScript/BrowserWidth.js"></script>

    <script language="javascript" type="text/javascript" src="../../JavaScript/UpdateProgress.js"></script>

    <br />
    <h1>
        Set Roaster Off Employee : New Format</h1>
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
                                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 25%">
                                                                    <uc1:MonthYear ID="MonthYear1" runat="server"></uc1:MonthYear>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </fieldset>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="2">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="text"
                                                    Text="Show"></asp:Button>
                                                &nbsp;
                                                <asp:Button ID="btnExport" runat="server" CssClass="text" OnClick="btnRefresh_Click"
                                                    Text="Export" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="width: 30%">
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <div id="aa" runat="server" style="overflow: auto;">
                                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                    <asp:GridView ID="divDetail" runat="server" Width="100%" OnRowDataBound="divDetail_RowDataBound"
                                        AutoGenerateColumns="true">
                                        <HeaderStyle CssClass="GridviewScrollHeader" />
                                        <RowStyle CssClass="GridviewScrollItem" />
                                        <PagerStyle CssClass="GridviewScrollPager" />
                                    </asp:GridView>
                                </fieldset>
                            </div>
                            <asp:Button ID="btnInsertUpdate" OnClick="btnInsertUpdate_Click" runat="server" CssClass="text"
                                Text="Insert Update" OnClientClick="NavigatingDataGrid()" />
                            <asp:HiddenField ID="hfFinalString" runat="server" />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>

    <script type="text/javascript" src="../../JavaScript/gridviewScroll.min.js"></script>

    <script type="text/javascript" language="javascript">
        document.getElementById ('aa').style.width = ((GetBrowserWidth()) - 80 ) + 'px'; 
    </script>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <script type="text/javascript"> 
    $(document).ready(function () { 
        gridviewScroll(); 
    }); 
 
    function gridviewScroll() {  
        $('#<%=divDetail.ClientID%>').gridviewScroll({ 
            width: 1200,
            height: 550, 
            freezesize: 4 
        }); 
    } 
    </script>

    <script type="text/javascript" language="javascript">
    function NavigatingDataGrid() {
        var gvDrv = document.getElementById("ctl00_ContentPlaceHolder1_divDetail");
        var FinalString = ""
        var inputs = gvDrv.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {

            var ID = inputs[i].id.replace("ctl00_ContentPlaceHolder1_divDetail_", "");
            if (inputs[i].checked) {
            
                if (ID.indexOf("_Sun:") == -1) {
                
                    var Monday = ID.split(":")[1].replace("_", " ").replace("_", " ");
                    var FindEmpIDAndDate = ID.split(":")[0].split("_");
                    var EmpID = FindEmpIDAndDate[1];
                    var Date = FindEmpIDAndDate[2] + ' ' + FindEmpIDAndDate[3] + ' ' + FindEmpIDAndDate[4];
                    FinalString += EmpID + '*' + Date + '*' + Monday + '@'
                }
            }

        }
        
          document.getElementById("ctl00_ContentPlaceHolder1_hfFinalString").value = FinalString;
    }
    </script>

</asp:Content>
