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

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ex"] != null)
        {

            ErrorMessage.Font.Bold = false;
            ErrorMessage.Font.Size = FontUnit.Small;
            ErrorMessage.Text = Session["Ex"].ToString();
        }
        else
        {
            ErrorMessage.Font.Bold = true;
            ErrorMessage.Font.Size = FontUnit.Large;
            ErrorMessage.Text = "You Are <Br> Not <Br>Authorised To See This Page.";
        }
        Session["Ex"] = null;
    }
}
