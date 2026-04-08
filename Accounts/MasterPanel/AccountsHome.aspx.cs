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

public partial class AccountsHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HtmlLink css = new HtmlLink();
        css.Href = ResolveUrl("~/Manager/MasterPanel/inettuts.css");
        css.Attributes["rel"] = "stylesheet";
        css.Attributes["type"] = "text/css";
        css.Attributes["media"] = "all";
        this.Page.Header.Controls.Add(css);


    }
}