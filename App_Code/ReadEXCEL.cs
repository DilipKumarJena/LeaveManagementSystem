using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Runtime.InteropServices;

/// <summary>
/// Summary description for ReadEXCEL
/// </summary>
public class ReadEXCEL
{
    public ReadEXCEL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable ReadxlsFile(string strFileName)
    {
        OleDbConnection oleConn;
        OleDbCommand oleCmd;
        DataTable dtData = new DataTable();
        DataTable dt = new DataTable();

        if (!(string.IsNullOrEmpty(strFileName)))
        {
            oleConn = new OleDbConnection();
            oleConn.ConnectionString = ("Provider=Microsoft.Jet.OLEDB.4.0; data source=" + strFileName.Trim() + "; Extended Properties=Excel 8.0;");
            try
            {
                oleConn.Open();
                dt = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[1];
                excelSheets[0] = dt.Rows[0]["TABLE_NAME"].ToString();
                oleCmd = oleConn.CreateCommand();
                oleCmd.CommandText = "SELECT * FROM [" + excelSheets[0] + "]";
                dtData.Load(oleCmd.ExecuteReader());
            }

            catch (Exception ee)
            {
                HttpContext.Current.Session["Ex"] = ee.Message.ToString();

                ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
        }
        return dtData;
    }
}