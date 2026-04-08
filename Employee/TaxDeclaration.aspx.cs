using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Employee_TaxDeclaration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

            if (DateTime.Now >= Convert.ToDateTime("10/1/2014 00:00:00"))
            {
                divMain.Visible = false;
                divMessage.InnerHtml = "<br /><br />    <h1>       Tax Declaration Date Exceed.... </h1>";
            }

            else
            {
                string Exist = command.ExecuteScalar("Select COUNT(*) from TaxDeclaration where EmployeeID='" + Session["LoginID"] + "'");

                if (Exist == "0")
                    InsertUpdateTaxDeclaration(false);

                FetchEmployeeInformation();
                FetchTaxDeclaration();
                BindGrid();
            }
        }
    }


    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        InsertUpdateTaxDeclaration(false);
        Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Save As Draft.");
    }
    protected void btnSaveLock_Click(object sender, EventArgs e)
    {
        if (txtPanCardNo.Text == "")
        {
            Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Please Enter Pan Card No.");
            return;
        }
        if (Utility.ValidatePAN(txtPanCardNo.Text) == "Invalid PAN Number.")
        {
            Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Invalid PAN Number. Please Enter Valid Pan Card No.");
            return;
        }




        //BindGrid();

        //if (gvDetail.Rows.Count == 0)
        //{
        //    Utility.AlertOnAjaxPage(this, "UpdatePanel1", "No HRA Detail Filled. Please Enter HRA Details.");
        //    return;
        //}



        InsertUpdateTaxDeclaration(true);

        Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Save And Locked.");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Utility.ClearAllOnAjaxPage(this, "UpdatePanel1");
    }


    #region Main Functionality
    private void FetchEmployeeInformation()
    {
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchEmployeeRecord";

        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@ID", SqlDbType.Int).Value = Session["LoginID"].ToString();

        try
        {

            DataTable EmpDetail = command.ExecuteStoredProcedure(com);
            lblEmployeeId.Text = EmpDetail.Rows[0]["EMPCode"].ToString();
            lblEmployeeName.Text = EmpDetail.Rows[0]["Name"].ToString();
            lblLocation.Text = EmpDetail.Rows[0]["Location"].ToString();
            lblDepartmentName.Text = EmpDetail.Rows[0]["DepartmentName"].ToString();
            lblDesignation.Text = EmpDetail.Rows[0]["DesignationName"].ToString();
            lblEmailID.Text = EmpDetail.Rows[0]["EmailID"].ToString();
            lblCurrentDate.Text = Utility.GetServerDate();
            lblFirst.Text = EmpDetail.Rows[0]["TL"].ToString();
            lblSecond.Text = EmpDetail.Rows[0]["Manager"].ToString();
            lblPhone.Text = EmpDetail.Rows[0]["ContactNumber"].ToString();

            //txtAddress.Text = EmpDetail.Rows[0]["PermanentAddress"].ToString();






        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
        }
    }
    private void FetchTaxDeclaration()
    {
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchTaxDeclaration";

        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = Session["LoginID"].ToString();

        try
        {

            DataTable TaxDeclaration = command.ExecuteStoredProcedure(com);


            if (TaxDeclaration.Rows.Count != 0)
            {

                aTaxDeclarationHRADetails.Attributes.Add("href", "TaxDeclarationHRA.aspx?TaxDeclarationID=" + TaxDeclaration.Rows[0]["ID"].ToString() + "");

                hfTaxDeclarationID.Value = TaxDeclaration.Rows[0]["ID"].ToString();

                txtPanCardNo.Text = TaxDeclaration.Rows[0]["PanCardNo"].ToString();
                bool LockStatus = Convert.ToBoolean(TaxDeclaration.Rows[0]["LockStatus"].ToString());
                txtMediclaim80D.Text = TaxDeclaration.Rows[0]["Mediclaim80D"].ToString();
                txtMediclaim80D_P.Text = TaxDeclaration.Rows[0]["Mediclaim80D_P"].ToString();
                txtSection80DD.Text = TaxDeclaration.Rows[0]["Section80DD"].ToString();
                txtSection80DD_P.Text = TaxDeclaration.Rows[0]["Section80DD_P"].ToString();
                txtSection80DDB.Text = TaxDeclaration.Rows[0]["Section80DDB"].ToString();
                txtSection80DDB_P.Text = TaxDeclaration.Rows[0]["Section80DDB_P"].ToString();
                txtSection80U.Text = TaxDeclaration.Rows[0]["Section80U"].ToString();
                txtSection80U_P.Text = TaxDeclaration.Rows[0]["Section80U_P"].ToString();
                txtSection80E.Text = TaxDeclaration.Rows[0]["Section80E"].ToString();
                txtSection80E_P.Text = TaxDeclaration.Rows[0]["Section80E_P"].ToString();
                txtLifeInsurancePremium.Text = TaxDeclaration.Rows[0]["LifeInsurancePremium"].ToString();
                txtLifeInsurancePremium_P.Text = TaxDeclaration.Rows[0]["LifeInsurancePremium_P"].ToString();
                txtPublicProvidentFund.Text = TaxDeclaration.Rows[0]["PublicProvidentFund"].ToString();
                txtPublicProvidentFund_P.Text = TaxDeclaration.Rows[0]["PublicProvidentFund_P"].ToString();
                txtNationalSavingCertificate.Text = TaxDeclaration.Rows[0]["NationalSavingCertificate"].ToString();
                txtNationalSavingCertificate_P.Text = TaxDeclaration.Rows[0]["NationalSavingCertificate_P"].ToString();
                txtUnitLinkInsurancePremium.Text = TaxDeclaration.Rows[0]["UnitLinkInsurancePremium"].ToString();
                txtUnitLinkInsurancePremium_P.Text = TaxDeclaration.Rows[0]["UnitLinkInsurancePremium_P"].ToString();
                txtMutualFunds.Text = TaxDeclaration.Rows[0]["MutualFunds"].ToString();
                txtMutualFunds_P.Text = TaxDeclaration.Rows[0]["MutualFunds_P"].ToString();
                txtHousingLoan.Text = TaxDeclaration.Rows[0]["HousingLoan"].ToString();
                txtHousingLoan_P.Text = TaxDeclaration.Rows[0]["HousingLoan_P"].ToString();
                txtEquityLinkedSavingScheme.Text = TaxDeclaration.Rows[0]["EquityLinkedSavingScheme"].ToString();
                txtEquityLinkedSavingScheme_P.Text = TaxDeclaration.Rows[0]["EquityLinkedSavingScheme_P"].ToString();
                txtPremiumPaidForPensionPlan.Text = TaxDeclaration.Rows[0]["PremiumPaidForPensionPlan"].ToString();
                txtPremiumPaidForPensionPlan_P.Text = TaxDeclaration.Rows[0]["PremiumPaidForPensionPlan_P"].ToString();
                txtSchoolTuitionFees.Text = TaxDeclaration.Rows[0]["SchoolTuitionFees"].ToString();
                txtSchoolTuitionFees_P.Text = TaxDeclaration.Rows[0]["SchoolTuitionFees_P"].ToString();
                txtFiveYearFixedDepositWithScheduledBank.Text = TaxDeclaration.Rows[0]["FiveYearFixedDepositWithScheduledBank"].ToString();
                txtFiveYearFixedDepositWithScheduledBank_P.Text = TaxDeclaration.Rows[0]["FiveYearFixedDepositWithScheduledBank_P"].ToString();
                txtFiveYearPostOfficeTimeDepositAccount.Text = TaxDeclaration.Rows[0]["FiveYearPostOfficeTimeDepositAccount"].ToString();
                txtFiveYearPostOfficeTimeDepositAccount_P.Text = TaxDeclaration.Rows[0]["FiveYearPostOfficeTimeDepositAccount_P"].ToString();
                txtSeniorCitizensSavingsScheme.Text = TaxDeclaration.Rows[0]["SeniorCitizensSavingsScheme"].ToString();
                txtSeniorCitizensSavingsScheme_P.Text = TaxDeclaration.Rows[0]["SeniorCitizensSavingsScheme_P"].ToString();
                txtInfrastructureBonds.Text = TaxDeclaration.Rows[0]["InfrastructureBonds"].ToString();
                txtInfrastructureBonds_P.Text = TaxDeclaration.Rows[0]["InfrastructureBonds_P"].ToString();
                txtOtherThanEmployeeContributionToProvidentFund.Text = TaxDeclaration.Rows[0]["OtherThanEmployeeContributionToProvidentFund"].ToString();
                txtOtherThanEmployeeContributionToProvidentFund_P.Text = TaxDeclaration.Rows[0]["OtherThanEmployeeContributionToProvidentFund_P"].ToString();
                txtDateofSanctionOfLoan.Text = TaxDeclaration.Rows[0]["DateofSanctionOfLoan"].ToString();
                txtDateofSanctionOfLoan_P.Text = TaxDeclaration.Rows[0]["DateofSanctionOfLoan_P"].ToString();
                ddlHousingLoanType.Text = TaxDeclaration.Rows[0]["HousingLoanType"].ToString();
                txtHousingLoanType_P.Text = TaxDeclaration.Rows[0]["HousingLoanType_P"].ToString();



                txtHousingLoanAmountPaidTowardsInterest.Text = TaxDeclaration.Rows[0]["HousingLoanAmountPaidTowardsInterest"].ToString();
                txtHousingLoanAmountPaidTowardsInterest_P.Text = TaxDeclaration.Rows[0]["HousingLoanAmountPaidTowardsInterest_P"].ToString();
                txtHousingLoanAddress.Text = TaxDeclaration.Rows[0]["HousingLoanAddress"].ToString();
                txtHousingLoanAddress_P.Text = TaxDeclaration.Rows[0]["HousingLoanAddress_P"].ToString();
                txtPreviousEmployerGrossIncome.Text = TaxDeclaration.Rows[0]["PreviousEmployerGrossIncome"].ToString();
                txtPreviousEmployerGrossIncome_P.Text = TaxDeclaration.Rows[0]["PreviousEmployerGrossIncome_P"].ToString();



                txtPreviousEmployerProvidentFund.Text = TaxDeclaration.Rows[0]["PreviousEmployerProvidentFund"].ToString();
                txtPreviousEmployerProvidentFund_P.Text = TaxDeclaration.Rows[0]["PreviousEmployerProvidentFund_P"].ToString();
                txtPreviousEmployerProfessionalTaxDeducted.Text = TaxDeclaration.Rows[0]["PreviousEmployerProfessionalTaxDeducted"].ToString();
                txtPreviousEmployerProfessionalTaxDeducted_P.Text = TaxDeclaration.Rows[0]["PreviousEmployerProfessionalTaxDeducted_P"].ToString();
                txtPreviousEmployerIncomeTax.Text = TaxDeclaration.Rows[0]["PreviousEmployerIncomeTax"].ToString();
                txtPreviousEmployerIncomeTax_P.Text = TaxDeclaration.Rows[0]["PreviousEmployerIncomeTax_P"].ToString();
                ChangeToReadOnly(LockStatus);
            }
            else
            {
                aTaxDeclarationHRADetails.InnerText = "Click Save As Draft To Activate Link";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
        }
    }
    private void InsertUpdateTaxDeclaration(bool LockStatus)
    {
        try
        {
            SqlCommand com = new SqlCommand();
            com.CommandText = "InsertUpdateTaxDeclaration";
            com.CommandType = CommandType.StoredProcedure;




            com.Parameters.Add("@PanCardNo", SqlDbType.VarChar, 10).Value = txtPanCardNo.Text;
            com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = Session["LoginID"].ToString();

            com.Parameters.Add("@LockStatus", SqlDbType.Bit).Value = LockStatus;

            com.Parameters.Add("@Mediclaim80D", SqlDbType.Int).Value = Utility.CheckNullValue(txtMediclaim80D.Text);
            com.Parameters.Add("@Mediclaim80D_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtMediclaim80D_P.Text);
            com.Parameters.Add("@Section80DD", SqlDbType.Int).Value = Utility.CheckNullValue(txtSection80DD.Text);
            com.Parameters.Add("@Section80DD_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtSection80DD_P.Text);
            com.Parameters.Add("@Section80DDB", SqlDbType.Int).Value = Utility.CheckNullValue(txtSection80DDB.Text);
            com.Parameters.Add("@Section80DDB_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtSection80DDB_P.Text);
            com.Parameters.Add("@Section80U", SqlDbType.Int).Value = Utility.CheckNullValue(txtSection80U.Text);
            com.Parameters.Add("@Section80U_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtSection80U_P.Text);
            com.Parameters.Add("@Section80E", SqlDbType.Int).Value = Utility.CheckNullValue(txtSection80E.Text);
            com.Parameters.Add("@Section80E_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtSection80E_P.Text);
            com.Parameters.Add("@LifeInsurancePremium", SqlDbType.Int).Value = Utility.CheckNullValue(txtLifeInsurancePremium.Text);
            com.Parameters.Add("@LifeInsurancePremium_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtLifeInsurancePremium_P.Text);
            com.Parameters.Add("@PublicProvidentFund", SqlDbType.Int).Value = Utility.CheckNullValue(txtPublicProvidentFund.Text);
            com.Parameters.Add("@PublicProvidentFund_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtPublicProvidentFund_P.Text);
            com.Parameters.Add("@NationalSavingCertificate", SqlDbType.Int).Value = Utility.CheckNullValue(txtNationalSavingCertificate.Text);
            com.Parameters.Add("@NationalSavingCertificate_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtNationalSavingCertificate_P.Text);
            com.Parameters.Add("@UnitLinkInsurancePremium", SqlDbType.Int).Value = Utility.CheckNullValue(txtUnitLinkInsurancePremium.Text);
            com.Parameters.Add("@UnitLinkInsurancePremium_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtUnitLinkInsurancePremium_P.Text);
            com.Parameters.Add("@MutualFunds", SqlDbType.Int).Value = Utility.CheckNullValue(txtMutualFunds.Text);
            com.Parameters.Add("@MutualFunds_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtMutualFunds_P.Text);
            com.Parameters.Add("@HousingLoan", SqlDbType.Int).Value = Utility.CheckNullValue(txtHousingLoan.Text);
            com.Parameters.Add("@HousingLoan_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtHousingLoan_P.Text);
            com.Parameters.Add("@EquityLinkedSavingScheme", SqlDbType.Int).Value = Utility.CheckNullValue(txtEquityLinkedSavingScheme.Text);
            com.Parameters.Add("@EquityLinkedSavingScheme_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtEquityLinkedSavingScheme_P.Text);
            com.Parameters.Add("@PremiumPaidForPensionPlan", SqlDbType.Int).Value = Utility.CheckNullValue(txtPremiumPaidForPensionPlan.Text);
            com.Parameters.Add("@PremiumPaidForPensionPlan_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtPremiumPaidForPensionPlan_P.Text);
            com.Parameters.Add("@SchoolTuitionFees", SqlDbType.Int).Value = Utility.CheckNullValue(txtSchoolTuitionFees.Text);
            com.Parameters.Add("@SchoolTuitionFees_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtSchoolTuitionFees_P.Text);
            com.Parameters.Add("@FiveYearFixedDepositWithScheduledBank", SqlDbType.Int).Value = Utility.CheckNullValue(txtFiveYearFixedDepositWithScheduledBank.Text);
            com.Parameters.Add("@FiveYearFixedDepositWithScheduledBank_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtFiveYearFixedDepositWithScheduledBank_P.Text);
            com.Parameters.Add("@FiveYearPostOfficeTimeDepositAccount", SqlDbType.Int).Value = Utility.CheckNullValue(txtFiveYearPostOfficeTimeDepositAccount.Text);
            com.Parameters.Add("@FiveYearPostOfficeTimeDepositAccount_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtFiveYearPostOfficeTimeDepositAccount_P.Text);
            com.Parameters.Add("@SeniorCitizensSavingsScheme", SqlDbType.Int).Value = Utility.CheckNullValue(txtSeniorCitizensSavingsScheme.Text);
            com.Parameters.Add("@SeniorCitizensSavingsScheme_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtSeniorCitizensSavingsScheme_P.Text);
            com.Parameters.Add("@InfrastructureBonds", SqlDbType.Int).Value = Utility.CheckNullValue(txtInfrastructureBonds.Text);
            com.Parameters.Add("@InfrastructureBonds_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtInfrastructureBonds_P.Text);
            com.Parameters.Add("@OtherThanEmployeeContributionToProvidentFund", SqlDbType.Int).Value = Utility.CheckNullValue(txtOtherThanEmployeeContributionToProvidentFund.Text);
            com.Parameters.Add("@OtherThanEmployeeContributionToProvidentFund_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtOtherThanEmployeeContributionToProvidentFund_P.Text);
            com.Parameters.Add("@DateofSanctionOfLoan", SqlDbType.DateTime).Value = Utility.CheckNullValue(txtDateofSanctionOfLoan.Text);
            com.Parameters.Add("@DateofSanctionOfLoan_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtDateofSanctionOfLoan_P.Text);
            com.Parameters.Add("@HousingLoanType", SqlDbType.VarChar).Value = Utility.CheckNullValue(ddlHousingLoanType.Text);
            com.Parameters.Add("@HousingLoanType_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtHousingLoanType_P.Text);
            com.Parameters.Add("@HousingLoanAmountPaidTowardsInterest", SqlDbType.Int).Value = Utility.CheckNullValue(txtHousingLoanAmountPaidTowardsInterest.Text);
            com.Parameters.Add("@HousingLoanAmountPaidTowardsInterest_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtHousingLoanAmountPaidTowardsInterest_P.Text);
            com.Parameters.Add("@HousingLoanAddress", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtHousingLoanAddress.Text);
            com.Parameters.Add("@HousingLoanAddress_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtHousingLoanAddress_P.Text);
            com.Parameters.Add("@PreviousEmployerGrossIncome", SqlDbType.Int).Value = Utility.CheckNullValue(txtPreviousEmployerGrossIncome.Text);
            com.Parameters.Add("@PreviousEmployerGrossIncome_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtPreviousEmployerGrossIncome_P.Text);
            com.Parameters.Add("@PreviousEmployerProvidentFund", SqlDbType.Int).Value = Utility.CheckNullValue(txtPreviousEmployerProvidentFund.Text);
            com.Parameters.Add("@PreviousEmployerProvidentFund_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtPreviousEmployerProvidentFund_P.Text);
            com.Parameters.Add("@PreviousEmployerProfessionalTaxDeducted", SqlDbType.Int).Value = Utility.CheckNullValue(txtPreviousEmployerProfessionalTaxDeducted.Text);
            com.Parameters.Add("@PreviousEmployerProfessionalTaxDeducted_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtPreviousEmployerProfessionalTaxDeducted_P.Text);
            com.Parameters.Add("@PreviousEmployerIncomeTax", SqlDbType.Int).Value = Utility.CheckNullValue(txtPreviousEmployerIncomeTax.Text);
            com.Parameters.Add("@PreviousEmployerIncomeTax_P", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtPreviousEmployerIncomeTax_P.Text);
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Insert");


            command.ExecuteStoredProcedure(com);
            FetchTaxDeclaration();
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
        }
    }
    private void ChangeToReadOnly(bool ReadOnly)
    {

        txtMediclaim80D.ReadOnly = ReadOnly;
        txtMediclaim80D_P.ReadOnly = ReadOnly;
        txtSection80DD.ReadOnly = ReadOnly;
        txtSection80DD_P.ReadOnly = ReadOnly;
        txtSection80DDB.ReadOnly = ReadOnly;
        txtSection80DDB_P.ReadOnly = ReadOnly;
        txtSection80U.ReadOnly = ReadOnly;
        txtSection80U_P.ReadOnly = ReadOnly;
        txtSection80E.ReadOnly = ReadOnly;
        txtSection80E_P.ReadOnly = ReadOnly;
        txtLifeInsurancePremium.ReadOnly = ReadOnly;
        txtLifeInsurancePremium_P.ReadOnly = ReadOnly;
        txtPublicProvidentFund.ReadOnly = ReadOnly;
        txtPublicProvidentFund_P.ReadOnly = ReadOnly;
        txtNationalSavingCertificate.ReadOnly = ReadOnly;
        txtNationalSavingCertificate_P.ReadOnly = ReadOnly;
        txtUnitLinkInsurancePremium.ReadOnly = ReadOnly;
        txtUnitLinkInsurancePremium_P.ReadOnly = ReadOnly;
        txtMutualFunds.ReadOnly = ReadOnly;
        txtMutualFunds_P.ReadOnly = ReadOnly;
        txtHousingLoan.ReadOnly = ReadOnly;
        txtHousingLoan_P.ReadOnly = ReadOnly;
        txtEquityLinkedSavingScheme.ReadOnly = ReadOnly;
        txtEquityLinkedSavingScheme_P.ReadOnly = ReadOnly;
        txtPremiumPaidForPensionPlan.ReadOnly = ReadOnly;
        txtPremiumPaidForPensionPlan_P.ReadOnly = ReadOnly;
        txtSchoolTuitionFees.ReadOnly = ReadOnly;
        txtSchoolTuitionFees_P.ReadOnly = ReadOnly;
        txtFiveYearFixedDepositWithScheduledBank.ReadOnly = ReadOnly;
        txtFiveYearFixedDepositWithScheduledBank_P.ReadOnly = ReadOnly;
        txtFiveYearPostOfficeTimeDepositAccount.ReadOnly = ReadOnly;
        txtFiveYearPostOfficeTimeDepositAccount_P.ReadOnly = ReadOnly;
        txtSeniorCitizensSavingsScheme.ReadOnly = ReadOnly;
        txtSeniorCitizensSavingsScheme_P.ReadOnly = ReadOnly;
        txtInfrastructureBonds.ReadOnly = ReadOnly;
        txtInfrastructureBonds_P.ReadOnly = ReadOnly;
        txtOtherThanEmployeeContributionToProvidentFund.ReadOnly = ReadOnly;
        txtOtherThanEmployeeContributionToProvidentFund_P.ReadOnly = ReadOnly;
        txtDateofSanctionOfLoan.ReadOnly = ReadOnly;
        txtDateofSanctionOfLoan_P.ReadOnly = ReadOnly;
        ddlHousingLoanType.Enabled = !ReadOnly;
        txtHousingLoanType_P.ReadOnly = ReadOnly;



        txtHousingLoanAmountPaidTowardsInterest.ReadOnly = ReadOnly;
        txtHousingLoanAmountPaidTowardsInterest_P.ReadOnly = ReadOnly;
        txtHousingLoanAddress.ReadOnly = ReadOnly;
        txtHousingLoanAddress_P.ReadOnly = ReadOnly;
        txtPreviousEmployerGrossIncome.ReadOnly = ReadOnly;
        txtPreviousEmployerGrossIncome_P.ReadOnly = ReadOnly;



        txtPreviousEmployerProvidentFund.ReadOnly = ReadOnly;
        txtPreviousEmployerProvidentFund_P.ReadOnly = ReadOnly;
        txtPreviousEmployerProfessionalTaxDeducted.ReadOnly = ReadOnly;
        txtPreviousEmployerProfessionalTaxDeducted_P.ReadOnly = ReadOnly;
        txtPreviousEmployerIncomeTax.ReadOnly = ReadOnly;
        txtPreviousEmployerIncomeTax_P.ReadOnly = ReadOnly;



        if (ReadOnly)
        {
            btnRefresh.Visible = false;

            aTaxDeclarationHRADetails.Visible = false;
            aTaxDeclarationHRADetails.Attributes.Add("href", "#");
        }





        btnSaveDraft.Visible = !ReadOnly;
        btnSaveLock.Visible = !ReadOnly;





    }

    private void BindGrid()
    {
        if (hfTaxDeclarationID.Value == "")
        {
            hfTaxDeclarationID.Value = "0";
        }
        DataTable TaxDeclaration_HRA = command.ExecuteQuery("EXEC FetchTaxDeclaration_HRAByTaxDeclarationID " + hfTaxDeclarationID.Value + "");


        gvDetail.DataSource = TaxDeclaration_HRA;
        gvDetail.DataBind();
        Utility.SetGridCssSecond(gvDetail);




    }
    #endregion
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void chkPanApplied_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPanApplied.Checked)
        {
            txtPanCardNo.Text = "AAAAA0000A";

            txtPanCardNo.ReadOnly = true;

        }
        else
        {
            txtPanCardNo.Text = "";
            txtPanCardNo.ReadOnly = false;
        }
    }
}
