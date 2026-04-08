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

public partial class CompensatoryLeaveDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
        {


            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand Com = new SqlCommand("[FetchCompensatoryLeaveByEmployeeID]", conStr);


            Com.CommandType = CommandType.StoredProcedure;
            try
            {
                conStr.Open();

                Com.Parameters.Add("@ID", SqlDbType.Int).Value = Request.QueryString["EmployeeID"].ToString();

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
    }
}