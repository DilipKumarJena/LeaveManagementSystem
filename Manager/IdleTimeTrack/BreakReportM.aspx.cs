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
using System.Text;

public partial class BreakReportM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Utility.DateDifference(txtStartDate.Text, txtEndDate.Text) > 31)
        {
            Label1.Text = "Please Select Date Range Not Greater Than 31 days";
            return;
        }
        if (Convert.ToDateTime(txtEndDate.Text) > Convert.ToDateTime(Utility.GetServerDate()))
        {
            Label1.Text = "End Date Can Not Greater Than Current Date";
            return;
        }

        Label1.Text = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchIdleTimeTrackBreakReportManager]", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"];
        com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = txtStartDate.Text;
        com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = txtEndDate.Text;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);
            Session["Emp"] = Emp;
            divReport.DataSource = Emp;
            divReport.DataBind();
            Utility.SetGridCss(divReport);

            if (Emp.Rows.Count != 0)
            {
                Label1.Text = "Record Loaded.";
            }
            else
            {
                Label1.Text = "No Record Found.";
            }


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
        ExportToEXCEL.ExportGridViewToEXCEL(divReport, txtStartDate.Text + " To " + txtEndDate.Text + "_" + "BreakReport.xls");

        Label1.Text = "Record Exported";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
