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

public partial class FindEmployeeCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        FetchEmployeeDetailByName();
    }


    public void FetchEmployeeDetailByName()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchEmployeeDetailByName";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = txtName.Text;

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            gvEmployeeDetail.DataSource = EmpLeaveDetail;
            gvEmployeeDetail.DataBind();

            Utility.SetGridCss(gvEmployeeDetail);

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
