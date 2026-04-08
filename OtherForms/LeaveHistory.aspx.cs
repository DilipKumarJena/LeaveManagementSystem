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

public partial class LeaveHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
            
            FillDetails(Request.QueryString["ID"]);
    }

    protected void FillDetails(string LeaveID)
    {
        FetchAllLeaveDetailByEmployeeID(LeaveID);
    }



    public void FetchAllLeaveDetailByEmployeeID(string EmployeeID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchAllLeaveDetailByEmployeeID";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 10).Value = EmployeeID;
        try
        {
            conStr.Open();
            spanShowAllLeaveDetail.InnerHtml = Utility.CreateHTMLTable(command.ExecuteStoredProcedure(com), "FetchAllLeaveDetailByEmployeeID");
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
}
