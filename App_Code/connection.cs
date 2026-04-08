using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Sql;
using System.Data.SqlClient;


/// <summary>
/// Summary description for connection
/// </summary>
public class connection
{
    public connection()
    {

    }
    public static SqlConnection CreateConneciton()
    {
        
        SqlConnection sqlcon = null;
        try
        {
            if (HttpContext.Current.Session["sqlcon"] == null)
            {
                string connectionstring = CryptorEngine.Decrypt(ConfigurationManager.ConnectionStrings["LMS"].ConnectionString, true, "LeaveManagementSystem");
                sqlcon = new SqlConnection(connectionstring);
            }
            else
            {
                sqlcon = (SqlConnection)HttpContext.Current.Session["sqlcon"];
            }
        }
        catch
        {
            HttpContext.Current.Response.Redirect("http://192.168.165.22:85");
        }
        return sqlcon;
    }

}
