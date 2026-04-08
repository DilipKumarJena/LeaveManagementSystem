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

public partial class MasterPage_MasterPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginID"] == null)
        {
            Session.Abandon();
            Response.Redirect(Session["ReturnURL"].ToString());
        }
        else
        {
            String RequestedPage = Request.PhysicalPath.Replace(Request.PhysicalApplicationPath, "");
            string Query = "Select Access from PageWiseSecurity where Path = '" + RequestedPage + "' And UserID= '" + Session["LoginID"].ToString() + "' ";
            DataTable Access = command.ExecuteQuery(Query);
            if (Access.Rows.Count != 0)
            {
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
}
