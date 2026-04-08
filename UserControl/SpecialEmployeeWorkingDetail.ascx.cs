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

public partial class UserControl_SpecialEmployeeWorkingDetail : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            DataTable DT = command.ExecuteSP("Fetch45HourStatusLastOneMonth");
            gvDetail.DataSource = DT;
            gvDetail.DataBind();







            Utility.SetGridCss(gvDetail);
        }
    }
    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Status = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Status = UpdateWorkingStatus(Session["LoginID"].ToString(), ((HiddenField)e.Row.FindControl("hfDate")).Value.ToString());
            if (Status.Contains("Short"))
                e.Row.Cells[2].Text = Status;
        }
    }


    protected string UpdateWorkingStatus(string LoginID, string Date)
    {
        string FinalStatus = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchPunchingDetailFor45Hours", conStr);
        com.CommandType = CommandType.StoredProcedure;

        try
        {
            com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
            com.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;


            DataSet Emp = command.ExecuteStoredProcedureReturnDataset(com);

            FinalStatus = Emp.Tables[1].Rows[0][0].ToString();

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }

        return FinalStatus;
    }
}
