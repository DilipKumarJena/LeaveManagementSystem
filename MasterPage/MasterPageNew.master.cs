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

public partial class MasterPageNew : System.Web.UI.MasterPage
{
    protected string MyFunction()
    {
        //        BannerSWF
        Random rnd = new Random();
        int File = Convert.ToInt32(rnd.Next(1, 20));

        string FileName = "http://192.168.165.22:100/Images/MasterPage/LMS_SWF/" + File.ToString() + ".swf";
        return FileName;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      


        foreach (DictionaryEntry de in HttpContext.Current.Cache)
        {
            HttpContext.Current.Cache.Remove(de.Key.ToString());
        }

        if (Session["LoginID"] == null)
        {
            Session.Abandon();
            ErrorLogs.logerrorsWithOutRedirect("In Master Page", HttpContext.Current.Request.Url.ToString(), "Master Page Redirect");
            Response.Redirect("http://192.168.165.22:85");
        }
        else
        {
            String RequestedPage = Request.PhysicalPath.Replace(Request.PhysicalApplicationPath, "");
            DataTable Access = FetchPageAccess(RequestedPage, Session["LoginID"].ToString(), ExtraParameterForSecurity.GetIPAddress());

            if (Access.Rows.Count != 0)
            {
                if (Convert.ToBoolean(Access.Rows[0][1].ToString()) == true)
                    HttpContext.Current.Response.Redirect("~/UnderConstruction.aspx");

                if (Convert.ToBoolean(Access.Rows[0][0].ToString()) == false)
                    HttpContext.Current.Response.Redirect("~/Error.aspx");
            }
            else
                HttpContext.Current.Response.Redirect("~/Error.aspx");
        }



        if (!IsPostBack)
        {
            HtmlLink css = new HtmlLink();
            css.Href = ResolveUrl("~/greybox/gb_styles.css");
            css.Attributes["rel"] = "stylesheet";
            css.Attributes["type"] = "text/css";
            css.Attributes["media"] = "all";
            this.Page.Header.Controls.Add(css);
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
       

    }

    private DataTable FetchPageAccess(string Path, string UserID, string IP)
    {
        Session["BrowserWidth"] = (hfBrowserWidth.Value == "" ? "1000" : hfBrowserWidth.Value);


        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchPageAccess]", conStr);
        DataTable DT = new DataTable();

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@Path", SqlDbType.VarChar).Value = Path;
            Com.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            Com.Parameters.Add("@IP", SqlDbType.VarChar, 15).Value = IP;
            DT = command.ExecuteStoredProcedure(Com);
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
        return DT;
    }
}
