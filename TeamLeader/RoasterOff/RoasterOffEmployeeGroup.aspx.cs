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
using System.Text;

public partial class RoasterOffEmployeeGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            FetchWeekDays();
            BindGrid();
        }
    }


    protected void BindGrid()
    {
        Label1.Text = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchEmployeeListForRoasterOffGroupingTeamLeader]", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            Session["Emp"] = Emp;



            divDetail.DataSource = Emp;
            divDetail.DataBind();
            Utility.SetGridCss(divDetail);
            Label1.Text = "Record Loaded.";



        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }





    public void FetchWeekDays()
    {
        DataTable DT = command.ExecuteQuery("EXEC FetchWeekDays");
        Session["Day"] = DT;
    }

    protected void divDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlRoGroup = (DropDownList)e.Row.FindControl("ddlRoGroup");
            HiddenField hfRoGroup = (HiddenField)e.Row.FindControl("hfRoGroup");



            ddlRoGroup.DataSource = (DataTable)Session["Day"];
            ddlRoGroup.DataTextField = "Day";
            ddlRoGroup.DataValueField = "ID";
            ddlRoGroup.DataBind();



            ddlRoGroup.SelectedValue = hfRoGroup.Value;

        }
    }
    protected void btnInsertUpdate_Click(object sender, EventArgs e)
    {

        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<Roaster>");
        foreach (GridViewRow gvR in divDetail.Rows)
        {
            HiddenField EmpID = (HiddenField)gvR.FindControl("hfID");
            DropDownList ddlRoGroup = (DropDownList)gvR.FindControl("ddlRoGroup");
            XML.Append("<Detail EmpID=\"" + EmpID.Value + "\" RoasterDayGroup=\"" + ddlRoGroup.SelectedValue + "\"  />");
        }
        XML.Append("</Roaster>");

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("UpdateRoasterOffGroup", conStr);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        try
        {
            if (conStr.State != ConnectionState.Open)
                conStr.Open();
            com.ExecuteNonQuery();
            BindGrid();
            Label1.Text = "Updated.";
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {

            if (conStr.State != ConnectionState.Closed)
                conStr.Close();
        }
    }
}
