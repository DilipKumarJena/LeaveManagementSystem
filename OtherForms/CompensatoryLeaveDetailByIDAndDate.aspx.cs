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

public partial class CompensatoryLeaveDetailByIDAndDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {  Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
        {
          
            EarnedCompOff();
            TakenCompOff();


            if (Request.QueryString["Show"].ToString() != "T")
                divShow.Visible = false;
        }
    }

    private void EarnedCompOff()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchCompensatoryLeaveByEmployeeIDAndDate]", conStr);


        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = Request.QueryString["ID"].ToString();
            Com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Request.QueryString["S"].ToString();
            Com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Request.QueryString["E"].ToString();

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


    private void TakenCompOff()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("FetchLeaveDetailByEmployeeIDAndDate", conStr);


        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = Request.QueryString["ID"].ToString();
            Com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Request.QueryString["S"].ToString();
            Com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = Request.QueryString["E"].ToString();
            Com.Parameters.Add("@LeaveType", SqlDbType.Int).Value = Request.QueryString["T"].ToString();

            DataTable DT = command.ExecuteStoredProcedure(Com);

            Utility.SetGridCss(gvTaken);
            gvTaken.DataSource = DT;
            gvTaken.DataBind();

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { conStr.Close(); }
    }
}