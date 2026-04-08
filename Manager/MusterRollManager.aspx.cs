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
using System.Net.Mail;

public partial class MusterRollManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime ShowAfterDate = Convert.ToDateTime(command.ExecuteScalar("SELECT Max(ShowDate) FROM   ShowAfterDate").ToString());

        if (Convert.ToDateTime(txtStartDate.Text) < ShowAfterDate)
        {
            Label1.Text = "Please Select Date Greater Than Or Equal To " + ShowAfterDate.ToShortDateString();
            return;
        }
        if (Utility.DateDifference(txtStartDate.Text, txtEndDate.Text) > 31)
        {
            Label1.Text = "Please Select Date Range Not Greater Than 31 days";
            return;
        }
        if (Convert.ToDateTime(txtEndDate.Text) >= Convert.ToDateTime(Utility.GetServerDate()))
        {
            Label1.Text = "End Date Can Not Be Greater Than Current Date";
            return;
        }

        Label1.Text = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[ReportPresentAbsentAttendance_MusterRollManager]", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = txtStartDate.Text;
        com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = txtEndDate.Text;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            if (Emp.Rows.Count > 0)
            {
                Emp = AddExtraColumnMusterRoll.Add(ref Emp, txtStartDate.Text, txtEndDate.Text, false);

                Session["Emp"] = Emp;

                divReport.DataSource = Emp;
                divReport.DataBind();
                Utility.SetGridCss(divReport);
                Label1.Text = "Record Loaded.";

            }

            else
            {
                Label1.Text = "No Record Found.";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }



    protected void btnExportToEXCEL_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, txtStartDate.Text + " To " + txtEndDate.Text + "_" + "MasterRollForManager.xls");
        Label1.Text = "Record Exported";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void divReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string Width = (hfWidth.Value == "" ? "1000" : hfWidth.Value);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string EmployeeName = e.Row.Cells[1].Text;
            HtmlAnchor A = new HtmlAnchor();
            A.Attributes.Add("onclick", "return GB_showCenter('   *   OKS Group   *   Punch Detail', this.href,393," + Width + ")");
            A.Attributes.Add("href", "~/OtherForms/ShowPunchDetail.aspx?ID=" + e.Row.Cells[0].Text + "&S=" + txtStartDate.Text + "&E=" + txtEndDate.Text + "&Type=0");
            A.Attributes.Add("title", "Punch Detail");
            A.Attributes.Add("onMouseOver", "window.status='http://www.oksgroup.com';return true");
            A.InnerText = EmployeeName;
            e.Row.Cells[1].Controls.Add(A);







            string EmployeeCode = e.Row.Cells[0].Text;
            A = new HtmlAnchor();
            A.Attributes.Add("onclick", "return GB_showCenter('   *   OKS Group   *   Leave Detail', this.href,393," + Width + ")");
            A.Attributes.Add("href", "~/OtherForms/LeaveHistory.aspx?ID=" + e.Row.Cells[0].Text + "");
            A.Attributes.Add("title", "Leave Detail");
            A.Attributes.Add("onMouseOver", "window.status='http://www.oksgroup.com';return true");
            A.InnerText = EmployeeCode;
            e.Row.Cells[0].Controls.Add(A);

        } // if



    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string MailIDs = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchAllMailIDForMail]", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();

        SqlParameter Output = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 4000);
        Output.Direction = ParameterDirection.Output;
        com.Parameters.Add(Output);

        try
        {
            if (conStr.State != ConnectionState.Open)
                conStr.Open();
            com.ExecuteNonQuery();
            MailIDs = Output.Value.ToString();




            Attachment Attachment = ExportToEXCEL.DataTableAsAttchment((DataTable)Session["Emp"], "Muster Roll.xls");




            Label1.Text = SendEmailViaSMTP.SendEmailWithAttachment(command.ExecuteScalar("Select [GOLD].[EMailID](" + Session["LoginID"].ToString() + ")").ToString(), MailIDs, "", "Leave Management System : Muster Roll", "PFA", Attachment);


        }

        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
    }
}