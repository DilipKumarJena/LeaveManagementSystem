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

public partial class OtherForms_ShowPunchDetail : System.Web.UI.Page
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
            string LeaveType = Request.QueryString[3].ToString();



            FetchPunchCardDetailByEmployeeCode(EmpCode, Start, End);
            FetchLeaveDetail(EmpCode, Start, End, LeaveType);

        } // if

    }

    private void FetchPunchCardDetailByEmployeeCode(string EmpCode, string Start, string End)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchPunchCardDetailByEmployeeCode", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@EmployeeCode", SqlDbType.VarChar).Value = Utility.CheckNullValue(EmpCode);
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

        ExportToEXCEL.ExportDtToEXCEL(DT, Start + " To " + End + "_" + "PunchDetail.xls");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    private void FetchLeaveDetail(string EmployeeCode, string Start, string End, string LeaveType)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("FetchLeaveDetailByEmployeeIDAndDate", conStr);


        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = EmployeeCode;
            Com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Start;
            Com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = End;
            Com.Parameters.Add("@LeaveType", SqlDbType.Int).Value = Convert.ToInt32(LeaveType);

            DataTable DT = command.ExecuteStoredProcedure(Com);

            Utility.SetGridCss(gvLeaveDetail);
            gvLeaveDetail.DataSource = DT;
            gvLeaveDetail.DataBind();

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { conStr.Close(); }
    }
}

