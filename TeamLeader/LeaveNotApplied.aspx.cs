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

public partial class LeaveNotApplied : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            DataTable DT = Utility.BindAllMonthsForHR();

            ddlMonth.DataSource = DT;
            ddlMonth.DataTextField = "Text";
            ddlMonth.DataValueField = "Value";
            ddlMonth.DataBind();
        }
    }





    private void Fetchleavenotappliedformanager()
    {
        DataTable DT = new DataTable();
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchleavenotappliedforTeamLeader]", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = Session["LoginID"].ToString();
            Com.Parameters.Add("@BetweenMonth", SqlDbType.VarChar).Value = ddlMonth.SelectedValue;
            DT = command.ExecuteStoredProcedure(Com);
            Label1.Text = DT.Rows.Count + " Leaves Pending For Posting For Current Month.";
            Label1.CssClass = "errorRed";
            gvDetail.DataSource = DT;
            gvDetail.DataBind();
            Utility.SetGridCss(gvDetail);
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
    protected void btnExport_Click(object sender, EventArgs e)
    {

        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, "LeaveNotApplied.xls");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        Fetchleavenotappliedformanager();
    }
}