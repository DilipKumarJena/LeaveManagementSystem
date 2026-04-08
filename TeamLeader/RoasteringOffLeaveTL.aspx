<%@ Page AutoEventWireup="true" CodeFile="RoasteringOffLeaveTL.aspx.cs" Inherits="RoasteringOffLeaveTL"
    Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" Title="Define Roastering Off Leave : Team Leader" Culture="auto"  UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <br />
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server">
    </asp:ScriptManager>
    <br />
    <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
        height: 118px;" id="UpdateProgress" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="../Images/ajax-loader-new__.gif" /><br />
                <img src="../Images/PleaseWait.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>
                Roastering Off Leave : Team Leader</h1>
            <br />
            <table style="width: 100%" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                            valign="middle">
                            <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                <table style="width: 100%" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 20%">
                                                <b>For The Date :</b>
                                            </td>
                                            <td style="width: 80%">
                                                <asp:TextBox onkeydown="return false" ID="txtDate" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                    onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"
                                                    runat="server" Font-Bold="False" CssClass="text" ></asp:TextBox>
                                                <asp:Button ID="btnShowEmployee" OnClick="btnShowEmployee_Click" runat="server" Text="Show Employee"
                                                    CssClass="text" ></asp:Button>
                                                &nbsp;
                                                <asp:Label ID="lblMessage" runat="server" CssClass="ErrorRed" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>Select Time : </strong>
                                            </td>
                                            <td style="width: 80%">
                                                <asp:DropDownList ID="ddlOverTimeMain" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOverTimeMain_SelectedIndexChanged"
                                                    CssClass="text" Visible="False"  />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div id="divUpdate" runat="server" visible="False">
                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px"
                                                    colspan="2">
                                                    <asp:GridView ID="gvEmployeeList" runat="server" CssClass="text" OnRowDataBound="gvEmployeeList_RowDataBound"
                                                        AutoGenerateColumns="False" Width="100%" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Over Time" >
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlOverTime" runat="server" CssClass="text" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlOverTime_SelectedIndexChanged"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Working Department" >
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="text"  />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name" >
                                                                <ItemTemplate>
                                                                    <a onmouseover="window.status='http://www.oksgroup.com';return true" href="../OtherForms/CompensatoryLeaveDetail.aspx">
                                                                    </a><a class="Navigation" href='<%# "../OtherForms/CompensatoryLeaveDetail.aspx?EmployeeID="+Eval("ID") %>'
                                                                        title="View Compensatory Leave Detail" onclick="return GB_showCenter('   *   OKS Group   *   View Compensatory Leave Detail', this.href,393,)">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>' ></asp:Label>
                                                                    </a>
                                                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("ID") %>' />
                                                                    <asp:HiddenField ID="hfDuration" runat="server" Value='<%# Bind("Duration") %>' />
                                                                    <asp:HiddenField ID="hfWorkingMinutes" runat="server" Value='<%# Bind("WorkingMinutes") %>' />
                                                                    <asp:HiddenField ID="hfTotalWorkingMinute" runat="server" Value='<%# Bind("TotalWorkingMinute") %>' />
                                                                    <asp:HiddenField ID="hfDepartmentID" runat="server" Value='<%# Bind("DepartmentID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Date" HeaderText="Date" ></asp:BoundField>
                                                            <asp:BoundField DataField="OKSLeaveName" HeaderText="OKSLeaveName" ></asp:BoundField>
                                                            <asp:BoundField DataField="DateIn" HeaderText="DateIn" ></asp:BoundField>
                                                            <asp:BoundField DataField="DateOut" HeaderText="DateOut" ></asp:BoundField>
                                                            <asp:BoundField DataField="PunchTime" HeaderText="PunchTime" ></asp:BoundField>
                                                            <asp:BoundField DataField="CabLate" HeaderText="CabLate" ></asp:BoundField>
                                                            <asp:BoundField DataField="OutDuty" HeaderText="OutDuty" ></asp:BoundField>
                                                            <asp:BoundField DataField="TotalWorkingHour" HeaderText="TotalWorkingHour" ></asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    Comment :
                                                                </td>
                                                                <td style="width: 80%">
                                                                    <asp:TextBox ID="txtComment" runat="server" CssClass="text" Width="98%" Height="116px"
                                                                        Visible="False" ></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit"
                                                                        CssClass="text" Visible="False" ></asp:Button>
                                                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Button" Visible="False"  /></td>
                                                                <td style="width: 80%">
                                                                    <asp:Label ID="lblMessage1" runat="server" CssClass="ErrorRed" ></asp:Label></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <br />
    <br />
    <br />
    <br />
</asp:Content>
