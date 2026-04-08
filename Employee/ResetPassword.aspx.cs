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

public partial class ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[ResetPassword]", conStr);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@OldPassword", SqlDbType.VarChar).Value = txtOldPassword.Text;
        com.Parameters.Add("@NewPassword", SqlDbType.VarChar).Value = txtNewPassword.Text;
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"];


        SqlParameter Output = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50);
        Output.Direction = ParameterDirection.Output;
        com.Parameters.Add(Output);
        try
        {

            conStr.Open();
            com.ExecuteNonQuery();


            DivMessage.InnerHtml = Output.Value.ToString();

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            DivMessage.InnerHtml = "Some Error Occured";
        }
        finally
        {
            conStr.Close();
        }
    }
}
