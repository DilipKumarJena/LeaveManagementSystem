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
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;

public partial class CabLateEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            DataTable VehicleNumber = command.ExecuteQuery("EXEC FetchCabMasterVehicles");
            ddlVehicleNo.DataTextField = "VehicleNumber";
            ddlVehicleNo.DataValueField = "ID";
            ddlVehicleNo.DataSource = VehicleNumber;
            ddlVehicleNo.DataBind();



        }

    }

    protected void btnExportToEXCEL_Click(object sender, EventArgs e)
    {
        lblReason.Text = "";
        if (txtReason.Text == "")
        {
            lblReason.Text = "* Please Enter Reason";
            return;
        }


        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<CabLate>");

        foreach (GridViewRow gvR in gvLeaveDetail.Rows)
        {
            TextBox T = (TextBox)gvR.FindControl("txtEntry");
            if (T.Text != "")
            {
                HiddenField hf = (HiddenField)gvR.FindControl("hfID");
                XML.Append("<Detail EmpID=\"" + hf.Value + "\" Late=\"" + T.Text + "\" />");
            }
        }
        XML.Append("</CabLate>");

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("UpdatePunchCardDetailCabLateAdmin", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();

        com.Parameters.Add("@Date", SqlDbType.VarChar, 25).Value = txtFromDate.Text.ToString();
        com.Parameters.Add("@CabNotCame", SqlDbType.VarChar).Value = chkCabNotCame.Checked;
        ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Cab Late Updated By Admin");
        com.Parameters.Add("@Reason", SqlDbType.VarChar).Value = txtReason.Text;

        try
        {
            conStr.Open();
            com.ExecuteNonQuery();
            lblError.Text = "Record Updated.";
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchEmployeeListCabMapping", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@CabID", SqlDbType.Int).Value = ddlVehicleNo.SelectedValue;
        com.Parameters.Add("@Date", SqlDbType.DateTime).Value = txtFromDate.Text;

        try
        {
            DataTable Cab = command.ExecuteStoredProcedure(com);
            gvLeaveDetail.DataSource = Cab;
            gvLeaveDetail.DataBind();
            Utility.SetGridCss(gvLeaveDetail);

            lblError.Text = "";
            if (Cab.Rows.Count == 0)
            {
                lblError.Text = "No Employee Found For This Cab.";
            }
            else
            {
                divVisible.Visible = true;
                txtReason.Text = Cab.Rows[0]["CabLateReason"].ToString();
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvLeaveDetail.Rows)
        {
            TextBox T = (TextBox)gvR.FindControl("txtEntry");
            T.Text = txtLateInMin.Text;
        }
    }
}