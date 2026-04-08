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

public partial class UserControl_MonthYear : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            Utility.BindAllMonths(ddlMonth);

            Utility.FillYearsInDropdown(ddlYear, 3);

            DataTable DT = command.ExecuteQuery("EXEC FetchLastMonthYear");

            if (DT.Rows.Count == 1)
            {
                ddlMonth.SelectedValue = DT.Rows[0]["Month"].ToString();
                ddlYear.SelectedValue = DT.Rows[0]["Year"].ToString();
            }
        }
    }

    public void Enable(bool Status)
    {
        if (Status == true)
        {
            ddlMonth.Enabled = true;
            ddlYear.Enabled = true;
        }
        else
        {
            ddlMonth.Enabled = false;
            ddlYear.Enabled = false;
        }
    }
    public string GetMonth()
    {
        return ddlMonth.SelectedValue;
    }

    public string GetYear()
    {
        return ddlYear.SelectedValue;
    }
    public bool Validation(ref Label MessageLabel)
    {
        bool Validation = true;
        if (ddlMonth.SelectedValue == "0")
        {
            MessageLabel.Text = "Please Select Month.";
            Validation = false;
        }
        if (ddlYear.SelectedValue == "0")
        {
            MessageLabel.Text = "Please Select Year.";
            Validation = false;
        }
        return Validation;
    }
}
