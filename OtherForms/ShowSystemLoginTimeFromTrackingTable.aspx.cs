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

public partial class ShowSystemLoginTimeFromTrackingTable : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
        {
            string EmpCode = Request.QueryString[0].ToString();
            string Date = Request.QueryString[1].ToString();




            FetchManualAttendanceTrackingFromEmpCodeAndDate(EmpCode, Date);


        } // if

    }

    private void FetchManualAttendanceTrackingFromEmpCodeAndDate(string EmpCode, string Date)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchManualAttendanceTrackingFromEmpCodeAndDate", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = EmpCode;
        com.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;

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

