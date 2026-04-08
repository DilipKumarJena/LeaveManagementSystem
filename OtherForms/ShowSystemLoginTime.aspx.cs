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

public partial class ShowSystemLoginTime : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
        {
            string PunchID = Request.QueryString[0].ToString();




            FetchManualAttendanceByPunchCardDetailID(PunchID);


        } // if

    }

    private void FetchManualAttendanceByPunchCardDetailID(string PunchID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchManualAttendanceByPunchCardDetailID", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@ID", SqlDbType.VarChar).Value = Utility.CheckNullValue(PunchID);

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

