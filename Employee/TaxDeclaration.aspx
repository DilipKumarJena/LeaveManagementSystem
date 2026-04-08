<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="TaxDeclaration.aspx.cs" Inherits="Employee_TaxDeclaration" Title="Tax Declaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
    </script>

    <script type="text/javascript" src="../greybox/AJS.js"></script>

    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/TextValidations.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

    <br />
    <div id="divMessage" runat="server">
        <h1>
            Tax Declaration
        </h1>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divMain" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 5%">
                        </td>
                        <td style="width: 90%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 60%">
                                        <fieldset>
                                            <table width="95%">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" valign="top" width="20%">
                                                            Employee Id</td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:Label ID="lblEmployeeId" runat="server" Font-Bold="True"></asp:Label></td>
                                                        <td align="left" valign="top" width="20%">
                                                            Location</td>
                                                        <td align="left" valign="top" width="20%">
                                                            <asp:Label ID="lblLocation" runat="server" Font-Bold="True"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" width="20%">
                                                            Employee Name</td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:Label ID="lblEmployeeName" runat="server" Font-Bold="True"></asp:Label></td>
                                                        <td align="left" valign="top" width="20%">
                                                            Department</td>
                                                        <td align="left" valign="top" width="20%">
                                                            <asp:Label ID="lblDepartmentName" runat="server" Font-Bold="True"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" width="20%">
                                                            E-Mail ID</td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:Label ID="lblEmailID" runat="server" CssClass="Email" Font-Bold="True"></asp:Label></td>
                                                        <td align="left" valign="top" width="20%">
                                                            Designation</td>
                                                        <td align="left" valign="top" width="20%">
                                                            <asp:Label ID="lblDesignation" runat="server" Font-Bold="True"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" width="20%">
                                                            Phone</td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:Label ID="lblPhone" runat="server" CssClass="Email" Font-Bold="True"></asp:Label></td>
                                                        <td align="left" valign="top" width="20%">
                                                            Current Date</td>
                                                        <td align="left" valign="top" width="30%">
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
                                                        <td align="left" valign="top" width="20%">
                                                        </td>
                                                        <td align="left" valign="top" width="30%">
                                                        </td>
                                                        <td align="left" valign="top" width="20%">
                                                            <strong>Pan Card No. (<span style="color: #ff0066">Mandatory</span>)</strong></td>
                                                        <td align="left" valign="top" width="30%">
                                                            <asp:TextBox ID="txtPanCardNo" runat="server" CssClass="text"></asp:TextBox><br />
                                                            <asp:CheckBox ID="chkPanApplied" runat="server" AutoPostBack="True" OnCheckedChanged="chkPanApplied_CheckedChanged"
                                                                Text="Check If Pan Not Available" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%">
                        </td>
                        <td style="width: 90%">
                            <fieldset>
                                <table width="100%">
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td style="width: 65%">
                                        </td>
                                        <td style="width: 15%" align="center">
                                        </td>
                                        <td style="width: 15%" align="center">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <table width="100%">
                                    <tr style="background-color: gray">
                                        <td style="width: 5%" align="center" valign="middle">
                                            <strong>A</strong></td>
                                        <td style="width: 65%" align="center">
                                            <strong>Contributions / Payments</strong></td>
                                        <td style="width: 15%; text-align: center;">
                                            <strong>Amount</strong></td>
                                        <td style="width: 15%; text-align: center;">
                                            <strong>Type of Proof Attached</strong></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            1</td>
                                        <td style="width: 65%">
                                            Mediclaim u/s 80-D ( In case of Mediclaim has been taken for Senior Citizens please
                                            specify as "SC" )
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtMediclaim80D" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtMediclaim80D_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            2</td>
                                        <td style="width: 65%">
                                            Section 80 DD (Maintenance/medical treatment of handicapped dependents.)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80DD" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80DD_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            3</td>
                                        <td style="width: 65%">
                                            Section 80 DDB (Medical treatment of specified diseases for self/dependents.)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80DDB" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80DDB_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            4</td>
                                        <td style="width: 65%">
                                            Section 80 U (Deductions in case of self being totally blind or physically handicapped.)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80U" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80U_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            5</td>
                                        <td style="width: 65%">
                                            Section 80 E (Repayment of interest only on loan for higher education for Self,
                                            Spouse and Children only)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80E" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSection80E_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <table width="100%">
                                    <tr style="background-color: gray">
                                        <td align="center" style="width: 5%" valign="middle">
                                            <strong>B</strong></td>
                                        <td style="width: 65%" align="center">
                                            <strong>Investment Details u/s 80-C (Limit - 1,50,000/-) </strong>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            1</td>
                                        <td style="width: 65%">
                                            Life Insurance Premium - LIP (For Self, Spouse and Children only)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtLifeInsurancePremium" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtLifeInsurancePremium_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            2</td>
                                        <td style="width: 65%">
                                            Public Provident Fund - PPF (For Self, Spouse and Children only)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPublicProvidentFund" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPublicProvidentFund_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            3</td>
                                        <td style="width: 65%">
                                            National Saving Certificate -NSC
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtNationalSavingCertificate" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtNationalSavingCertificate_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            4</td>
                                        <td style="width: 65%">
                                            Unit Link Insurance Premium - ULIP (For Self, Spouse and Children only)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtUnitLinkInsurancePremium" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtUnitLinkInsurancePremium_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            5</td>
                                        <td style="width: 65%">
                                            Mutual Funds - MF ( Only Acknowledgements of Investments will not be accepted)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtMutualFunds" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtMutualFunds_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            6</td>
                                        <td style="width: 65%">
                                            Housing Loan - (Amount repaid towards Principal portion only)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtHousingLoan" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtHousingLoan_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            7</td>
                                        <td style="width: 65%">
                                            Equity Linked Saving Scheme (ELSS) ( Only Acknowledgements of Investments will not
                                            be accepted)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtEquityLinkedSavingScheme" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtEquityLinkedSavingScheme_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            8</td>
                                        <td style="width: 65%">
                                            Premium paid for Pension Plan - 80CCC
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPremiumPaidForPensionPlan" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPremiumPaidForPensionPlan_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            9</td>
                                        <td style="width: 65%">
                                            School Tuition Fees - Only School Fees (of Maximum 2 children)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSchoolTuitionFees" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSchoolTuitionFees_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            10</td>
                                        <td style="width: 65%">
                                            5 year Fixed Deposit with Scheduled Bank
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtFiveYearFixedDepositWithScheduledBank" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtFiveYearFixedDepositWithScheduledBank_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            11</td>
                                        <td style="width: 65%">
                                            5 year Post Office Time Deposit Account
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtFiveYearPostOfficeTimeDepositAccount" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtFiveYearPostOfficeTimeDepositAccount_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            12</td>
                                        <td style="width: 65%">
                                            Senior Citizens Savings Scheme
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSeniorCitizensSavingsScheme" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtSeniorCitizensSavingsScheme_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            13</td>
                                        <td style="width: 65%">
                                            Infrastructure Bonds - If Any
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtInfrastructureBonds" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtInfrastructureBonds_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            14</td>
                                        <td style="width: 65%">
                                            Others Please specify ( Other than Employee's Contribution to Provident Fund)
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtOtherThanEmployeeContributionToProvidentFund" runat="server"
                                                onkeypress="return NumberOnly_New(event)" CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtOtherThanEmployeeContributionToProvidentFund_P" runat="server"
                                                CssClass="text"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <table width="100%">
                                    <tr style="background-color: gray">
                                        <td align="center" style="width: 5%" valign="middle">
                                            <strong>C</strong></td>
                                        <td style="width: 65%" align="center">
                                            <strong>Housing Loan Details u/s 24(2) (Limit - 2,00,000/-) </strong>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            1</td>
                                        <td style="width: 65%">
                                            (i) Date of Sanction of Loan :
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtDateofSanctionOfLoan" runat="server" CssClass="text" Font-Bold="False"
                                                onfocus="popUpCalendar(this, this,'mm/dd/yyyy');return false;" onkeydown="return false"
                                                onkeypress=" return false" onkeyup="return false" onmousedown="return noCopyMouse(event);"></asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtDateofSanctionOfLoan_P" runat="server" CssClass="text" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td style="width: 65%">
                                            (ii) Under Construction ( ) / Self Occupied ( )
                                        </td>
                                        <td style="width: 15%">
                                            <asp:DropDownList ID="ddlHousingLoanType" runat="server" CssClass="text">
                                                <asp:ListItem>None</asp:ListItem>
                                                <asp:ListItem>Under Construction</asp:ListItem>
                                                <asp:ListItem>Self Occupied</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtHousingLoanType_P" runat="server" CssClass="text" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            2</td>
                                        <td style="width: 65%">
                                            Housing Loan - amount paid towards Interest
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtHousingLoanAmountPaidTowardsInterest" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtHousingLoanAmountPaidTowardsInterest_P" runat="server" CssClass="text"
                                                Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            3</td>
                                        <td style="width: 65%">
                                            Address :
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtHousingLoanAddress" runat="server" CssClass="text" Height="47px"
                                                TextMode="MultiLine" Width="98%"></asp:TextBox><asp:TextBox ID="txtHousingLoanAddress_P"
                                                    runat="server" CssClass="text" Visible="False"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <table width="100%">
                                    <tr style="background-color: gray">
                                        <td align="center" style="width: 5%" valign="middle">
                                            <strong>D</strong></td>
                                        <td style="width: 65%" align="center">
                                            <strong>HRA Rebate Details u/s 10 </strong>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td style="width: 65%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <a id="aTaxDeclarationHRADetails" runat="server" class="Navigation" onclick="return GB_showCenter('   *   OKS Group   *   Tax Declaration - HRA', this.href,500,900 )"
                                                            onmouseover="window.status='http://www.oksgroup.com';return true" style="color: black"
                                                            title="Show Tax Declaration - HRA">Click Here To Fill HRA Detail </a>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click">Refresh Grid</asp:LinkButton></td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="hfTaxDeclarationID" runat="server" />
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td colspan="3">
                                            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="From" HeaderText="From" />
                                                    <asp:BoundField DataField="Upto" HeaderText="Upto" />
                                                    <asp:BoundField DataField="NameAddressOfTheLandlord" HeaderText="NameAddressOfTheLandlord" />
                                                    <asp:BoundField DataField="AddressOfAccommodation" HeaderText="AddressOfAccommodation" />
                                                    <asp:BoundField DataField="RentPM" HeaderText="RentPM" />
                                                    <asp:BoundField DataField="LandLordPanCardNo" HeaderText="LandLordPanCardNo" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <table width="100%">
                                    <tr style="background-color: gray">
                                        <td align="center" style="width: 5%" valign="middle">
                                            <strong>E</strong></td>
                                        <td style="width: 65%" align="center">
                                            <strong>Previous Employer Details (i.e. Salary from other Company)</strong></td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            1</td>
                                        <td style="width: 65%">
                                            Gross Income ( Gross Salary PLUS Perks &amp; Other Income LESS HRA Rebate LESS Exempt
                                            Conveyance)</td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerGrossIncome" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerGrossIncome_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            2</td>
                                        <td style="width: 65%">
                                            Provident Fund deducted - own Contribution</td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerProvidentFund" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerProvidentFund_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            3</td>
                                        <td style="width: 65%">
                                            Professional Tax deducted</td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerProfessionalTaxDeducted" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerProfessionalTaxDeducted_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                            4</td>
                                        <td style="width: 65%">
                                            Income Tax / TDS deducted</td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerIncomeTax" runat="server" onkeypress="return NumberOnly_New(event)"
                                                CssClass="text">0</asp:TextBox></td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtPreviousEmployerIncomeTax_P" runat="server" CssClass="text"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td style="width: 65%">
                                            <strong>* Please attach copy of salary certificate / form 16 from previous employer</strong></td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <fieldset>
                                <table width="100%">
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td style="width: 65%">
                                            <strong>Declaration :</strong></td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="width: 5%" valign="middle">
                                        </td>
                                        <td colspan="3" style="text-align: justify">
                                            I hereby confirm that I have invested/ contributed the above amounts for the purpose
                                            of rebate/deduction to be considered in calculating my income tax for the FY 2014-2015
                                            . I further undertake that wherever eligible investments are made in the name of
                                            spouse/children/dependent parents, the same have been made out of my income and
                                            claim thereof shall not be made elsewhere to get Income Tax benefit.I will produce
                                            the tenancy/lease agreement in respect of rents paid, when called for by the company.
                                            I hereby declare that all the information given by me is true and correct. I undertake
                                            to notify you immediately of any change in the above facts. Any Income Tax liability
                                            arising out of a wrong declaration will be my responsibility, and I undertake to
                                            indemnify the Company and its officers from all consequences,monetary and otherwise,
                                            arising out of any incorrect and/orincomplete information provided in this declaration.</td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle" colspan="4">
                                            <div style="background-color: Transparent; position: absolute; width: 220px; height: 118px;"
                                                id="UpdateProgress" align="center">
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <img src="../Images/ajax-loader-new__.gif" /><br />
                                                        <img src="../Images/PleaseWait.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                            <asp:Button ID="btnSaveDraft" runat="server" CssClass="text" Text="Save As Draft"
                                                OnClick="btnSaveDraft_Click" />
                                            <asp:Button ID="btnSaveLock" runat="server" CssClass="text" Text="Save & Lock" OnClick="btnSaveLock_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
