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

public partial class OKS_Education_MusterRollEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime ShowAfterDate = Convert.ToDateTime(command.ExecuteScalar("SELECT Max(ShowDate) FROM   ShowAfterDate").ToString());

        if (Convert.ToDateTime(txtStartDate.Text) < ShowAfterDate)
        {
            Label1.Text = "Please Select Date Greater Than Or Equal To " + ShowAfterDate.ToShortDateString();
            return;
        }


        if (Utility.DateDifference(txtStartDate.Text, txtEndDate.Text) < 0)
        {
            Label1.Text = "Invalid Date Range.";
            return;
        }

        if (Utility.DateDifference(txtStartDate.Text, txtEndDate.Text) > 31)
        {
            Label1.Text = "Please Select Date Range Not Greater Than 31 days";
            return;
        }
        if (Convert.ToDateTime(txtEndDate.Text) >= Convert.ToDateTime(Utility.GetServerDate()))
        {
            Label1.Text = "End Date Can Not Greater Than Current Date";
            return;
        }
        command.ExecuteNonQuery("EXEC UpdateMusterRollByPunchCardInBulk  0,0,0," + Session["LoginID"].ToString() + ",'" + txtStartDate.Text + "','" + txtEndDate.Text + "'");
        Label1.Text = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("ReportPresentAbsentAttendance_MasterRoll", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@LocationID", SqlDbType.Int).Value = 0;
        com.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = 0;
        com.Parameters.Add("@DesignationID ", SqlDbType.Int).Value = 0;
        com.Parameters.Add("@EmployeeID ", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = txtStartDate.Text;
        com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = txtEndDate.Text;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            AddExtraColumnMusterRoll.Add(ref Emp, txtStartDate.Text, txtEndDate.Text, false);


            divReport.DataSource = Emp;
            divReport.DataBind();
            Utility.SetGridCss(divReport);
            Label1.Text = "Record Loaded.";
            Session["Emp"] = Emp;

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
    protected void btnExportToEXCEL_Click(object sender, EventArgs e)
    {

        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, txtStartDate.Text + " To " + txtEndDate.Text + "_" + "MasterRollEmployee.xls");



        Label1.Text = "Record Exported";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

}
