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

public partial class TLHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HtmlLink css = new HtmlLink();
        css.Href = ResolveUrl("~/Manager/MasterPanel/inettuts.css");
        css.Attributes["rel"] = "stylesheet";
        css.Attributes["type"] = "text/css";
        css.Attributes["media"] = "all";
        this.Page.Header.Controls.Add(css);


        if (!(IsPostBack))
        {
            FetchLeaveBalanceByEmployeeID();
            FetchPendingStatusManager();
            LastWorkingDayFinalStatus();
            FetchEmployeeAttStatusHomePage();

        } // if


    }

    protected void FetchLeaveBalanceByEmployeeID()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();

        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveBalanceByEmployeeID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@EmpID", SqlDbType.Int).Value = Session["LoginID"];

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            divLeaveBalance.InnerHtml = Utility.CreateHTMLTable(EmpLeaveDetail, "LeaveStatus");
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
        # endregion

    }

    protected void FetchPendingStatusManager()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();

        # region FetchPendingStatusManager


        com.CommandText = "FetchPendingStatusTL";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"];

        try
        {
            conStr.Open();
            DataTable EmpPendingDetail = command.ExecuteStoredProcedure(com);

            aLeavePost.InnerText = EmpPendingDetail.Rows[0]["Status"].ToString();
            aOutDuty.InnerText = EmpPendingDetail.Rows[1]["Status"].ToString();
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




    protected void LastWorkingDayFinalStatus()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();

        # region FetchPendingStatusManager


        com.CommandText = "FetchLastWorkingDayFinalStatusForTL";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"];

        SqlParameter Output = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50);
        Output.Direction = ParameterDirection.Output;
        com.Parameters.Add(Output);


        try
        {
            conStr.Open();
            DataTable PendingStatus = command.ExecuteStoredProcedure(com);
            gvLastWorkingDayStatus.DataSource = PendingStatus;
            gvLastWorkingDayStatus.DataBind();

            lblLastWorkingDayStatus.Text = "Status : " + Output.Value.ToString();

            Utility.SetGridCss(gvLastWorkingDayStatus);

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



    protected void FetchEmployeeAttStatusHomePage()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();

        # region Leave Status Of Employee


        com.CommandText = "FetchEmployeeAttStatusHomePage";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@EmpID", SqlDbType.Int).Value = Session["LoginID"];

        try
        {
            conStr.Open();
            DataTable Pending = command.ExecuteStoredProcedure(com);
            gvPendingDetail.DataSource = Pending;
            gvPendingDetail.DataBind();
            Utility.SetGridCss(gvPendingDetail);

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
        # endregion

    }
















}
