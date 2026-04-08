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

public partial class Employee_TaxDeclarationHRA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            BindGrid();
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (ValidateForm())
        {
            InsertUpdateTaxDeclaration_HRA();
        }
    }


    private bool ValidateForm()
    {
        if (txtFromDate.Text == "")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Please Enter From.");
            return false;
        }

        if (txtUpTo.Text == "")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Please Enter UpTo.");
            return false;
        }
        if (txtNameAndAddressOfTheLandlord.Text == "")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Please Enter Name And Address Of The Landlord.");
            return false;
        }
        if (txtAddressOfAccommodation.Text == "")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Please Enter Address Of Accommodation.");
            return false;
        }
        if (txtRentPerMonth.Text == "")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Please Enter Rent/Month.");
            return false;
        }


        //int EnteredRent = Int32.Parse(command.ExecuteScalar("EXEC ValidateTaxDeclaration_HRA '" + Request.QueryString[0].ToString() + "'"));

        //DateTime From = Convert.ToDateTime(txtFromDate.Text);
        //DateTime To = Convert.ToDateTime(txtUpTo.Text);
        int RentPM = Convert.ToInt32(txtRentPerMonth.Text);


        //int MonthDuration = ((To.Year - From.Year) * 12) + To.Month - From.Month + 1;


        //if (MonthDuration > 12)
        //{
        //    Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Duration Must Be 12 Months.");
        //    return false;
        //}

        //int Rent = MonthDuration * RentPM;

        if (RentPM >= 8333 && txtPanCard.Text == "")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Limit Exceed....Please Enter LandLord Pan Card No In Textbox.");
            return false;
        }

        if (RentPM >= 8333 && Utility.ValidatePAN(txtPanCard.Text) == "Invalid PAN Number.")
        {
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Invalid PAN Number. Please Enter Valid Pan Card No.");
            return false;
        }




        //if (EnteredRent + Rent >= 100000)
        //{
        //    foreach (GridViewRow gvR in gvDetail.Rows)
        //    {
        //        TextBox LandLordPanCardNo = (TextBox)gvR.FindControl("txtLandLordPanCardNo");

        //        if (LandLordPanCardNo.Text.Trim() == "")
        //        {
        //            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Limit Exceed....Please Enter LandLord Pan Card No In Each Textbox In Grid.");
        //            return false;
        //        }
        //    }
        //}
        return true;
    }



    private void InsertUpdateTaxDeclaration_HRA()
    {
        try
        {
            SqlCommand com = new SqlCommand();
            com.CommandText = "InsertUpdateTaxDeclaration_HRA";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@TaxDeclarationID", SqlDbType.Int).Value = Request.QueryString[0].ToString();
            com.Parameters.Add("@NameAddressOfTheLandlord", SqlDbType.VarChar).Value = txtNameAndAddressOfTheLandlord.Text;
            com.Parameters.Add("@AddressOfAccommodation", SqlDbType.VarChar).Value = txtAddressOfAccommodation.Text;
            com.Parameters.Add("@From", SqlDbType.DateTime).Value = txtFromDate.Text;
            com.Parameters.Add("@Upto", SqlDbType.DateTime).Value = txtUpTo.Text;
            com.Parameters.Add("@RentPM", SqlDbType.Int).Value = txtRentPerMonth.Text;
            com.Parameters.Add("@LandLordPanCardNo", SqlDbType.VarChar, 10).Value = txtPanCard.Text;

            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Insert");


            command.ExecuteStoredProcedure(com);
            BindGrid();

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
    protected void lkbtnAddressOfAccomdation_Click(object sender, EventArgs e)
    {
        DataTable PersonalInfo = command.ExecuteQuery("Select * from UserMasterOtherInfo Where UserMasterID=" + Session["LoginID"].ToString() + "");
        txtAddressOfAccommodation.Text = PersonalInfo.Rows[0]["CurrentAddress"].ToString();
    }

    private void BindGrid()
    {
        DataTable TaxDeclaration_HRA = command.ExecuteQuery("EXEC FetchTaxDeclaration_HRAByTaxDeclarationID " + Request.QueryString[0].ToString() + "");


        gvDetail.DataSource = TaxDeclaration_HRA;
        gvDetail.DataBind();
        Utility.SetGridCssSecond(gvDetail);




    }
    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateRecord")
        {

            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            TextBox LandLordPanCardNo = (TextBox)row.FindControl("txtLandLordPanCardNo");

            command.ExecuteNonQuery("EXEC UpdateTaxDeclaration_HRALandLordPanCardNo " + e.CommandArgument.ToString() + ",'" + LandLordPanCardNo.Text + "'");
            lblMessage.Text = "Record Updated.";



        }

        else if (e.CommandName == "DeleteRecord")
        {
            command.ExecuteNonQuery("EXEC DeleteTaxDeclaration_HRA " + e.CommandArgument.ToString() + "");
            lblMessage.Text = "Record Updated.";


        }

        BindGrid();
    }
    protected void chkPanApplied_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPanApplied.Checked)
        {
            txtPanCard.Text = "AAAAA0000A";
            Utility.AlertOnAjaxNormalPageWithOutMaster(UpdatePanel1, "Please Update PAN No. Of Landlord, Otherwise HRA Will Not Be Considered During Final Tax Calculation!!!");
            txtPanCard.ReadOnly = true;
        }
        else
        {
            txtPanCard.Text = "";
            txtPanCard.ReadOnly = false;
        }
    }
}
