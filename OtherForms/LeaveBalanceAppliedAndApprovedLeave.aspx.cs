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

public partial class LeaveBalanceAppliedAndApprovedLeave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
            FillDetails(Request.QueryString["ID"]);
    }

    protected void FillDetails(string LeaveID)
    {
        FetchLeaveBalanceByLeavePostID(LeaveID);
        FetchLeaveDetailByLeavePostID(LeaveID);
        FetchAllLeaveDetailByLeavePostID(LeaveID);
        FetchPunchCardDetailByLeavePostID(LeaveID);

    }

    private void FetchLeaveBalanceByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();



        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveBalanceByLeavePostID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            LeaveBalance.InnerHtml = Utility.CreateHTMLTable(EmpLeaveDetail, "LeaveStatus");
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
        # endregion
    }
    private void FetchLeaveDetailByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();



        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveDetailByLeavePostID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            LeaveDetail.InnerHtml = Utility.CreateHTMLTable(EmpLeaveDetail, "LeaveDetail");
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
        # endregion
    }

    public void FetchAllLeaveDetailByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchAllLeaveDetailByLeavePostID";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LeavePostID", SqlDbType.Int).Value = LeaveID;
        try
        {
            conStr.Open();
            spanShowAllLeaveDetail.InnerHtml = Utility.CreateHTMLTable(command.ExecuteStoredProcedure(com), "FetchLeaveDetailByEmployeeID");
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
    }
    private void FetchPunchCardDetailByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchPunchCardDetailByLeavePostID", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;



        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            gvDetail.DataSource = Emp;
            gvDetail.DataBind();
            Utility.SetGridCss(gvDetail);

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
}
