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

public partial class PunchDetailOnOKSHolidays : System.Web.UI.Page
{
    string Start = "";
    string End = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
        {
            string EmpCode = Request.QueryString[0].ToString();
            Start = Request.QueryString[1].ToString();
            End = Request.QueryString[2].ToString();



            EmployeeOKSLeaveDetail(EmpCode, Start, End);
            //FetchLeaveDetail(EmpCode, Start, End, LeaveType);

        } // if

    }

    private void EmployeeOKSLeaveDetail(string ID, string Start, string End)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("EmployeeOKSLeaveDetail", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
        com.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = Start;
        com.Parameters.Add("@EndDate", SqlDbType.VarChar).Value = End;

        Session["Emp"] = null;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);
            if (Emp.Rows.Count != 0)
            {
                gvDetail.DataSource = Emp;
                gvDetail.DataBind();
                Utility.SetGridCss(gvDetail);
                Session["Emp"] = Emp;
            }
            else
            {
                //Error.InnerHtml = "No Punch Detail Found For Current Selection.";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, Start + " To " + End + "_" + "Detail.xls");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }



}

