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
/// Summary description for command
/// </summary>
public class CommandForTracking
{

    public CommandForTracking()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable ExecuteQuery(string Query)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = connectionForTracking.CreateConneciton();

        try
        {
            SqlDataAdapter sqlda = new SqlDataAdapter(Query, sqlcon);
            sqlda.Fill(dt);

        }
        catch (Exception ee)
        {
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        return dt;
    }
    // by anudeep
    public static DataTable ExecuteSP(string Spname)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = connectionForTracking.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandText = Spname;
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Connection = sqlcon;
        try
        {
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
            sqlda.Fill(dt);
        }
        catch (Exception ee)
        {
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        return dt;

    }

    public static string ExecuteScalar(string query)
    {
        string output = string.Empty;
        SqlConnection sqlcon = connectionForTracking.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            output = sqlcmd.ExecuteScalar().ToString();
        }
        catch (Exception ee)
        {
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            output = "0";
        }
        return output;

    }

    public static void ExecuteNonQuery(string query)
    {

        SqlConnection sqlcon = connectionForTracking.CreateConneciton();
        SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);

        }


    }

    public static DataTable ExecuteStoredProcedure(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = connectionForTracking.CreateConneciton();
        cmd.Connection = sqlcon;
        try
        {
            cmd.CommandTimeout = 60000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ee)
        {
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        return dt;
    }
    public static DataSet ExecuteStoredProcedureReturnDataset(SqlCommand cmd)
    {
        DataSet DS = new DataSet();
        SqlConnection sqlcon = connectionForTracking.CreateConneciton();
        cmd.Connection = sqlcon;
        try
        {
            cmd.CommandTimeout = 60000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(DS);
        }
        catch (Exception ee)
        {
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        return DS;
    }
}
