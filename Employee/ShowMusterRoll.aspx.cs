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

public partial class Employee_ShowMusterRoll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            BindGrid(Request.QueryString[0].ToString());
        }
    }

    private void BindGrid(string EmpCode)
    {
        DataTable DT = command.ExecuteQuery("EXEC FetchLast30DaysAttendance '" + EmpCode + "'");

        gvMusterRoll.DataSource = DT;
        gvMusterRoll.DataBind();
        Utility.SetGridCss(gvMusterRoll);
    }
}
