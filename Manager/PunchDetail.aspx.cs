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
using System.Text;

public partial class PunchDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        Utility.SetGridCss(gvEmployeeList);
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("FetchEmployeeListFromLoginIDForCompensatoryLeaveManager", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
            Com.Parameters.Add("@Date", SqlDbType.VarChar).Value = txtDate.Text;

            DataTable DT = command.ExecuteStoredProcedure(Com);
            if (DT.Rows.Count != 0)
            {
                lblMessage.Text = "";
                gvEmployeeList.DataSource = DT;
                gvEmployeeList.DataBind();
            }
            else
            {
                lblMessage.Text = "Punch Not Imported Yet.";
            }



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





}