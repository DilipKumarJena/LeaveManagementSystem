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
/// Summary description for ErrorLogs
/// </summary>
public class ErrorLogs
{
    static string msgs;
    public string err
    {
        get
        {
            return msgs;
        }

        set
        {
            msgs = value;
        }
    }

    public ErrorLogs()
    {
    }
    public static void logerrors(Exception e, string Pageurl, string ErrorMessage)
    {
        if (HttpContext.Current.Session["LoginID"] == null)
        {
            HttpContext.Current.Response.Redirect("http://192.168.165.22:85");
            return;
        }


        SqlConnection sqlcon = connection.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandText = "InsertErrorLogs";
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Connection = sqlcon;
        sqlcmd.Parameters.Add("@ErrorPage", SqlDbType.VarChar).Value = Pageurl;
        sqlcmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar).Value = e.Message;
        sqlcmd.Parameters.Add("@ExceptionType", SqlDbType.VarChar).Value = e.ToString();
        sqlcmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = HttpContext.Current.Session["LoginID"].ToString();
        sqlcmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = ExtraParameterForSecurity.GetIPAddress();

        try
        {
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteNonQuery();

        }
        catch (Exception ee)
        {

            throw ee;
        }
        finally
        {
            sqlcon.Close();
            sqlcon.Dispose();
            sqlcmd.Dispose();
            HttpContext.Current.Response.Redirect("~/Error.aspx");
        }
    }

    public static void logerrorsWithOutRedirect(Exception e, string Pageurl, string ErrorMessage)
    {

        SqlConnection sqlcon = connection.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandText = "InsertErrorLogs";
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Connection = sqlcon;
        sqlcmd.Parameters.Add("@ErrorPage", SqlDbType.VarChar).Value = Pageurl;
        sqlcmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar).Value = e.Message;
        sqlcmd.Parameters.Add("@ExceptionType", SqlDbType.VarChar).Value = e.ToString();
        sqlcmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = "0";
        sqlcmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = ExtraParameterForSecurity.GetIPAddress();

        try
        {
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteNonQuery();

        }
        catch (Exception ee)
        {

            throw ee;
        }
        finally
        {
            sqlcon.Close();
            sqlcon.Dispose();
            sqlcmd.Dispose();
        }
    }


    public static void logerrorsWithOutRedirect(string e, string Pageurl, string ErrorMessage)
    {
        SqlConnection sqlcon = connection.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandText = "InsertErrorLogs";
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Connection = sqlcon;
        sqlcmd.Parameters.Add("@ErrorPage", SqlDbType.VarChar).Value = Pageurl;
        sqlcmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar).Value = ErrorMessage;
        sqlcmd.Parameters.Add("@ExceptionType", SqlDbType.VarChar).Value = e;
        sqlcmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = "0";
        sqlcmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = ExtraParameterForSecurity.GetIPAddress();

        try
        {
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteNonQuery();

        }
        catch (Exception ee)
        {

            throw ee;
        }
        finally
        {
            sqlcon.Close();
            sqlcon.Dispose();
            sqlcmd.Dispose();
        }
    }
    public static void logerrors(SqlException sqlex, string pageurl, string ErrorMessage)
    {
        if (HttpContext.Current.Session["LoginID"] == null)
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Session["ReturnURL"].ToString());
            return;
        }
        SqlConnection sqlcon = connection.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandText = "InsertErrorLogs";
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Connection = sqlcon;
        sqlcmd.Parameters.Add("@errorpage", SqlDbType.VarChar).Value = pageurl;
        sqlcmd.Parameters.Add("@errorMessage", SqlDbType.VarChar).Value = sqlex.Message;
        sqlcmd.Parameters.Add("@Exceptiontype", SqlDbType.VarChar).Value = sqlex.ToString();
        sqlcmd.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = HttpContext.Current.Session["LoginID"].ToString();
        sqlcmd.Parameters.Add("@IP", SqlDbType.VarChar).Value = ExtraParameterForSecurity.GetIPAddress();
        try
        {
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            throw ee;
        }
        finally
        {
            sqlcon.Close();
            sqlcon.Dispose();
            sqlcmd.Dispose();
            HttpContext.Current.Response.Redirect("~/Error.aspx");
        }
    }

    public static void ErrorResponse(Exception ex)
    {

        HttpContext.Current.Session["Ex"] = ex;

        HttpContext.Current.Response.Redirect("~/Error.aspx");

    }

    public static void ErrorResponse(string ErrorMessage)
    {
        HttpContext.Current.Session["Ex"] = ErrorMessage;
        HttpContext.Current.Server.Transfer("~/Error.aspx");


    }



    public static void CreateMessageAlert(System.Web.UI.Page senderpage, string alertMsg)
    {
        string alertKey = "alertKey";
        string strScript;
        strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";
        if (!senderpage.IsStartupScriptRegistered(alertKey))
        {
            senderpage.RegisterStartupScript(alertKey, strScript);
        }
    }



}//end class
