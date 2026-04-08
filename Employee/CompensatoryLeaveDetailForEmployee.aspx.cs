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

public partial class CompensatoryLeaveDetailForEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchCompensatoryLeaveByEmployeeIDForEmployee]", conStr);


        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@ID", SqlDbType.Int).Value = Request.QueryString["EmployeeID"].ToString();
            Com.Parameters.Add("@Type", SqlDbType.Int).Value = rblCompOffType.SelectedValue;


            DataTable DT = command.ExecuteStoredProcedure(Com);

            Utility.SetGridCss(gvDetail);
            gvDetail.DataSource = DT;
            gvDetail.DataBind();
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { conStr.Close(); }
    }
    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            DataTable DT = command.ExecuteQuery("EXEC FetchLeaveDetailByCompOffID " + e.CommandArgument + "");


            gvLeaveDetail.DataSource = DT;
            gvLeaveDetail.DataBind();
            Utility.SetGridCss(gvLeaveDetail);
            gvLeaveDetail.Focus();
        }
    }
    protected void rblCompOffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}