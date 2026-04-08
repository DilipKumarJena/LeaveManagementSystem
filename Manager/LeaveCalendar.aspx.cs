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

public partial class LeaveCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

            DataTable DT = command.ExecuteQuery("EXEC FetchEmployeeListFromManager '" + Session["LoginID"] + "'");
            ddlEmployeeList.DataSource = DT;
            ddlEmployeeList.DataTextField = "Name";
            ddlEmployeeList.DataValueField = "ID";
            ddlEmployeeList.DataBind();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataTable DT = new DataTable();
        DT.Columns.Add("Cal");
        lblMessage.Text = "";
        if (ddlEmployeeList.SelectedValue == "0")
        {
            lblMessage.Text = "Please Select Employee.";
            return;
        }
        if (txtStartDate.Text == "")
        {
            lblMessage.Text = "Please Select Start Date.";
            return;
        }
        if (txtEndDate.Text == "")
        {
            lblMessage.Text = "Please Select End Date.";
            return;
        }

        DateTime Start = Convert.ToDateTime(txtStartDate.Text);
        DateTime End = Convert.ToDateTime(txtEndDate.Text);


        Start = new DateTime(Start.Year, Start.Month, 1);
        End = new DateTime(End.Year, End.Month, 1).AddMonths(1).AddDays(-1);

        while (Start <= End)
        {
            DataRow Dr = DT.NewRow();
            Dr[0] = AvailedLeaveCalendarWithHolidays.Show(ddlEmployeeList.SelectedValue, Start.Month, Start.Year);
            DT.Rows.Add(Dr);
            Start = Start.AddMonths(1);
        }


        dlSource.DataSource = DT;
        dlSource.DataBind();


    }
}
