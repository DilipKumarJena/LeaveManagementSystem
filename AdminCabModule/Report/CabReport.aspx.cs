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

public partial class CabReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            DataTable DT = command.ExecuteQuery("EXEC FetchCabMasterForReport");

            ddlRoute.DataSource = DT;
            ddlRoute.DataTextField = "Route";
            ddlRoute.DataValueField = "ID";
            ddlRoute.DataBind();

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        lblStart.Visible = false;
        lblEnd.Visible = false;
        if (txtStart.Text == "")
        {
            lblStart.Text = "Please Select Start Date";
            lblStart.Visible = true;
            return;
        }

        if (txtEnd.Text == "")
        {
            lblStart.Text = "Please Select End Date";
            lblEnd.Visible = true;
            return;
        }


        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchCabReport]", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@CabID", SqlDbType.Int).Value = ddlRoute.SelectedValue;
            Com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = txtStart.Text;
            Com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = txtEnd.Text;


            DataTable DT = command.ExecuteStoredProcedure(Com);
            gvCabReport.DataSource = DT;
            gvCabReport.DataBind();

            Utility.SetGridCss(gvCabReport);

            Session["Emp"] = DT;
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


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, txtStart.Text + "_To_" + txtEnd.Text + "_" + "Cab_Report.xls");
    }
}