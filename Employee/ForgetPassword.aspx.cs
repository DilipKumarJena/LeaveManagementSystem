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

public partial class ForgetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!(IsPostBack))
        {
            string SecurityKey = "LeaveManagementSystem";
            HttpContext.Current.Session["SecurityKey"] = SecurityKey;

        } // if

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            DataTable Password = new DataTable();
            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand Com = new SqlCommand("FetchPassword", conStr);


            Com.CommandType = CommandType.StoredProcedure;
            try
            {
                conStr.Open();

                Com.Parameters.Add("@Parameter", SqlDbType.VarChar).Value = txtBox.Text;


                Password = command.ExecuteStoredProcedure(Com);
            }
            catch (Exception ee)
            {
                Session["Ex"] = ee.Message.ToString();
                ErrorLogs.logerrorsWithOutRedirect (ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
            finally
            {
                conStr.Close();
            }


            if (Password.Rows.Count != 0)
            {
                try
                {
                    string mailText = EmailToManager(Password.Rows[0]["Password"].ToString(), Password.Rows[0]["Name"].ToString());

                    spanMessage.InnerText = SendEmailViaSMTP.SendEmail(Password.Rows[0]["EmailID"].ToString(), Password.Rows[0]["EmailID"].ToString(), "", "Leave Management System : Forgot Password", mailText);
                    //"Password Successfully Sent On Your Mail ID";
                }
                catch (Exception ee)
                {
                    Session["Ex"] = ee.Message.ToString();
                    ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
                    spanMessage.InnerText = "Some Error Occured.";
                }
            }
            else
            {
                spanMessage.InnerText = "Un Registered " + RadioButtonList1.SelectedItem.Text + ".";
            }

        }

    }
    public string EmailToManager(string Password, string Name)
    {

        #region Mail To Employee
        string Email = @"<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'> ";
        Email += @" <html xmlns='http://www.w3.org/1999/xhtml' > ";
        Email += @" <head> ";
        Email += @" <title>Recruitment Request</title> ";
        Email += Utility.GetCSSInString();
        Email += @" </head> ";
        Email += "<body style='background-color: #4396CA'>";
        Email += "    <br />";
        Email += "    <br />";
        Email += "    <br />";
        Email += "    <br />";
        Email += "    <table width='100%'>";
        Email += "        <tr>";
        Email += "            <td style='width: 20%'>";
        Email += "            </td>";
        Email += "            <td style='width: 60%'>";
        Email += "                <table style='width: 100%'>";
        Email += "                    <tr>";
        Email += "                        <td>";
        Email += "                            <div class='container'>";
        Email += "                                <b class='rtop'><b class='r1'></b><b class='r2'></b><b class='r3'></b><b class='r4'>";
        Email += "                                </b></b>";
        Email += "                                <div style='padding: 15px'>";
        Email += "                                    <div style='border: #000000 thin solid;'>";
        Email += "                                        <table width='100%'>";
        Email += "                                            <tr>";
        Email += "                                                <td colspan='2' style='background-color: Aqua; text-transform : none ;'>";
        Email += "                                                    Password Of User " + Name + " In Leave Management System Is : " + Password + "</td>";
        Email += "                                            </tr>";
        Email += "                                        </table>";
        Email += "                                    </div>";
        Email += "                                </div>";
        Email += "                                <b class='rbottom'><b class='r4'></b><b class='r3'></b><b class='r2'></b><b class='r1'>";
        Email += "                                </b></b>";
        Email += "                            </div>";
        Email += "                        </td>";
        Email += "                    </tr>";
        Email += "                </table>";
        Email += "            </td>";
        Email += "            <td style='width: 20%'>";
        Email += "            </td>";
        Email += "        </tr>";
        Email += "    </table>";
        string Path = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf('/'));
        Path = Path.Substring(0, Path.LastIndexOf('/'));

        string HREF = Session["ReturnURL"].ToString();

        Email += "<a href=" + HREF + ">Click To Navigate To Leave Management System</a>";

        Email += @" </body> ";
        Email += @" </html> ";
        #endregion

        return Email;
    }
}