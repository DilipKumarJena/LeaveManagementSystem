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

public partial class OtherForms_CompensatoryLeaveDetailInPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            Earned();
            Availed();
        }
    }

    private void Earned()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchEarnedCompensatoryLeaveDetailByEmpCodeMonthYear", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = Request.QueryString["ID"].ToString();
        com.Parameters.Add("@Month", SqlDbType.Int).Value = Request.QueryString["M"].ToString();
        com.Parameters.Add("@Year ", SqlDbType.Int).Value = Request.QueryString["Y"].ToString();

        Session["Emp"] = null;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            gvEarned.DataSource = Emp;
            gvEarned.DataBind();
            Utility.SetGridCss(gvEarned);
            Session["Emp"] = Emp;
            divError.InnerHtml = "";
            if (Emp.Rows.Count == 0)
            {
                divError.InnerHtml = "No Detail Found For Current Selection.";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }


    }

    private void Availed()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchAvailedCompensatoryLeaveDetailByEmpCodeMonthYear", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = Request.QueryString["ID"].ToString();
        com.Parameters.Add("@Month", SqlDbType.Int).Value = Request.QueryString["M"].ToString();
        com.Parameters.Add("@Year ", SqlDbType.Int).Value = Request.QueryString["Y"].ToString();

        Session["Emp"] = null;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            gvAvailed .DataSource = Emp;
            gvAvailed.DataBind();
            Utility.SetGridCss(gvAvailed);
            Session["Emp"] = Emp;
            divError.InnerHtml = "";
            if (Emp.Rows.Count == 0)
            {
                divError.InnerHtml = "No Detail Found For Current Selection.";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }


    }


}
