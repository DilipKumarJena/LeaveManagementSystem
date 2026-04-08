<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaxDeclarationHRA.aspx.cs"
    Inherits="Employee_TaxDeclarationHRA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script language="javascript" type="text/javascript" src="../JavaScript/TextValidations.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/calendar.js"></script>

</head>
<body style="background-color: #ede7d0">
    <form id="form1" runat="server">
        <div>
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
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="3">
                        <h1>
                            Tax Declaration - HRA
                        </h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="containerDark">
                                    <b class="rtopDark"><b class="r1Dark"></b><b class="r2Dark"></b><b class="r3Dark"></b>
                                        <b class="r4Dark"></b></b>
                                    <div style="padding: 15px">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 96%">
                                                    <table style="width: 100%; padding: 2px">
                                                        <tr>
                                                            <td style="width: 15%">
                                                                From :
                                                            </td>
                                                            <td style="width: 15%">
                                                                UpTo :
                                                            </td>
                                                            <td style="width: 30%">
                                                                &nbsp;Name &amp; Address of the Landlord</td>
                                                            <td style="width: 30%">
                                                                <asp:LinkButton ID="lkbtnAddressOfAccomdation" runat="server" OnClick="lkbtnAddressOfAccomdation_Click">Address of Accommodation</asp:LinkButton></td>
                                                            <td width="25">
                                                                Amount Paid / Month</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%">
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);" Enabled="False">4/1/2014</asp:TextBox></td>
                                                            <td style="width: 15%">
                                                                <asp:TextBox ID="txtUpTo" runat="server" CssClass="text" Font-Bold="False" onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;"
                                                                    onkeydown="return false" onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);" Enabled="False">3/31/2015</asp:TextBox></td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtNameAndAddressOfTheLandlord" runat="server" Height="59px" TextMode="MultiLine"
                                                                    Width="98%" CssClass="text"></asp:TextBox></td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtAddressOfAccommodation" runat="server" Height="59px" TextMode="MultiLine"
                                                                    Width="98%" CssClass="text"></asp:TextBox></td>
                                                            <td width="25">
                                                                <asp:TextBox ID="txtRentPerMonth" runat="server" CssClass="text" onkeypress="return NumberOnly_New(event)"
                                                                    Width="99px">0</asp:TextBox><br />
                                                                <br />
                                                                LandLord Pan Card No.<asp:TextBox ID="txtPanCard" runat="server" CssClass="text"></asp:TextBox><br />
                                                                <asp:CheckBox ID="chkPanApplied" runat="server" AutoPostBack="True" OnCheckedChanged="chkPanApplied_CheckedChanged"
                                                                    Text="Check If Pan Not Available" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%">
                                                            </td>
                                                            <td style="width: 15%">
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:Button ID="btnInsert" runat="server" CssClass="text" OnClick="btnInsert_Click"
                                                                    Text="Insert" /></td>
                                                            <td style="width: 30%">
                                                                <asp:Label ID="lblMessage" runat="server" CssClass="cssYellow"></asp:Label></td>
                                                            <td width="25">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 2%">
                                                </td>
                                                <td style="width: 96%">
                                                    <asp:GridView ID="gvDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                        OnRowCommand="gvDetail_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="From" HeaderText="From" />
                                                            <asp:BoundField DataField="Upto" HeaderText="Upto" />
                                                            <asp:BoundField DataField="NameAddressOfTheLandlord" HeaderText="NameAddressOfTheLandlord" />
                                                            <asp:BoundField DataField="AddressOfAccommodation" HeaderText="AddressOfAccommodation" />
                                                            <asp:BoundField DataField="RentPM" HeaderText="RentPM" />
                                                            <asp:TemplateField HeaderText="LandLord Pan Card No">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtLandLordPanCardNo" runat="server" Text='<%# Bind("LandLordPanCardNo") %>'
                                                                        ReadOnly="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lkbtnUpdate" runat="server" CausesValidation="false" CommandName="UpdateRecord"
                                                                        Text="Update LandLord Pan Card No" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lkbtnDelete" runat="server" CausesValidation="false" CommandName="DeleteRecord"
                                                                        Text="Delete Record" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td style="width: 2%">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <b class="rbottomDark"><b class="r4Dark"></b><b class="r3Dark"></b><b class="r2Dark">
                                    </b><b class="r1Dark"></b></b>
                                </div>
                                <span id="spanMessage" runat="server"></span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
