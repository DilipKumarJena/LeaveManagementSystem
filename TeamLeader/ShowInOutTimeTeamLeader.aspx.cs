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

public partial class ShowInOutTimeTeamLeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {     

        if (!(IsPostBack))
        {
            #region FetchEmployeeList
            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand Com = new SqlCommand("FetchEmployeeListTeamLeader", conStr);

            Com.CommandType = CommandType.StoredProcedure;
            try
            {
                conStr.Open();
                Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();

                DataTable DT = command.ExecuteStoredProcedure(Com);
                ddlEmployeeList.DataSource = DT;
                ddlEmployeeList.DataTextField = "Detail";
                ddlEmployeeList.DataValueField = "ID";
                ddlEmployeeList.DataBind();
            }
            catch (Exception ee)
            {
                Session["Ex"] = ee.Message.ToString();
                ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
            finally
            {
                conStr.Close();
            }
            #endregion
        }

    }
    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("FetchPunchCardDetailTL_Manager", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = txtStart.Text;
            Com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = txtEnd.Text;
            Com.Parameters.Add("@EmpID", SqlDbType.Int).Value = ddlEmployeeList.SelectedValue;
            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();




            DataTable DT = command.ExecuteStoredProcedure(Com);

            gvDetail.DataSource = DT;
            gvDetail.DataBind();

            Utility.SetGridCss(gvDetail);

            Session["Emp"] = DT;
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, txtStart.Text + " To " + txtEnd.Text + "_" + "PunchCardDetail.xls");

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
