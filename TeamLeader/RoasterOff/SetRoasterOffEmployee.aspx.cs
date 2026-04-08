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

public partial class SetRoasterOffEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FetchWeekDaysByMonday(txtMondayDate.Text);
        Label1.Text = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchEmployeeListForRoasterOffTeamLeader]", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@MondayDate", SqlDbType.DateTime).Value = txtMondayDate.Text;

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





    public void FetchWeekDaysByMonday(string Monday)
    {
        DataTable DT = command.ExecuteQuery("EXEC FetchWeekDaysByMonday '" + Monday + "'");
        Session["Monday"] = DT;

    }

    protected void divDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlRoasterDay = (DropDownList)e.Row.FindControl("ddlRoasterDay");
            HiddenField hfRoasterDay = (HiddenField)e.Row.FindControl("hfRoasterDay");



            ddlRoasterDay.DataSource = (DataTable)Session["Monday"];
            ddlRoasterDay.DataTextField = "DateText";
            ddlRoasterDay.DataValueField = "Date";
            ddlRoasterDay.DataBind();


            if (hfRoasterDay.Value != "")
                ddlRoasterDay.SelectedValue = hfRoasterDay.Value;
            else
                ddlRoasterDay.SelectedIndex = (((DataTable)Session["Monday"]).Rows.Count - 1);
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
            DropDownList ddlRoasterDay = (DropDownList)gvR.FindControl("ddlRoasterDay");
            XML.Append("<Detail EmpID=\"" + EmpID.Value + "\" RoasterDay=\"" + ddlRoasterDay.SelectedValue + "\" Monday=\"" + txtMondayDate.Text + "\" />");
        }
        XML.Append("</Roaster>");

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("InsertRoasterOffEmployees", conStr);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Insert");
        try
        {
            if (conStr.State != ConnectionState.Open)
                conStr.Open();
            com.ExecuteNonQuery();
            Label1.Text = "Inserted / Updated.";
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally {

            if (conStr.State != ConnectionState.Closed)
                conStr.Close();
        }
    }
}
