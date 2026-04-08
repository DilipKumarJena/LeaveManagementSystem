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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] == null)
            Response.Redirect(Session["ReturnURL"].ToString());

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
