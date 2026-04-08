using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ExtraParameterForSecurity
/// </summary>
public class ExtraParameterForSecurity
{
    public ExtraParameterForSecurity()
    {

    }

    public static void AddExtraParameterForSecurity(ref SqlCommand Com, string Type)
    {
        Com.Parameters.Add("@AddedBy", SqlDbType.Int).Value = HttpContext.Current.Session["LoginID"].ToString();
        Com.Parameters.Add("@IP", SqlDbType.VarChar, 15).Value = GetIPAddress();
        Com.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetIPAddress()
    {
        string IPAddress = "";
        if (HttpContext.Current != null)
        {
            IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (IPAddress == "" || IPAddress == null)
            {
                IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
        }
        return IPAddress;
    } // method: GetIPAddress

}
