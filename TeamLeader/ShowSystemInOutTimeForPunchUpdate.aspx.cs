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

public partial class ShowSystemInOutTimeForPunchUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            txtComment.Visible = false;
            btnUpdate.Visible = false;
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchPunchCardDetailTrackingLoginFoundTL", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Utility.CheckNullValue(Session["LoginID"].ToString());
        com.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = txtStartDate.Text;

        Session["Emp"] = null;
        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            gvEmployeePunchDetail.DataSource = Emp;
            gvEmployeePunchDetail.DataBind();
            Utility.SetGridCss(gvEmployeePunchDetail);
            Session["Emp"] = Emp;

            Error.InnerHtml = "";
            if (Emp.Rows.Count == 0)
            {
                txtComment.Visible = false;
                btnUpdate.Visible = false;
                Error.InnerHtml = "No Punch Detail Found For Current Selection.";
            }
            else
            {
                txtComment.Visible = true;
                btnUpdate.Visible = true;
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtComment.Text == "")
        {
            Error.InnerHtml = "Please Enter Comment.";
            return;
        }

        bool Return = true;
        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<PunchDetail>");

        foreach (GridViewRow gvR in gvEmployeePunchDetail.Rows)
        {
            HiddenField ID = (HiddenField)gvR.FindControl("hfID");
            TextBox In = (TextBox)gvR.FindControl("txtDateIn");
            TextBox Out = (TextBox)gvR.FindControl("txtDateOut");
            CheckBox Check = (CheckBox)gvR.FindControl("chkCheck");


            if (In.Text != "" && Out.Text != "" && Check.Checked == true)
            {
                Return = false;
                XML.Append("<Punch PunchID=\"" + ID.Value + "\" In=\"" + In.Text + "\" Out=\"" + Out.Text + "\" />");
            }
        }
        XML.Append("</PunchDetail>");


        if (Return)
        {
            Error.InnerHtml = "Please Put Atleast One Both Start And End Time And Check That CheckBox To Update.";
            return;
        }
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("InsertManualAttendancePunchCardUpdate", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        com.Parameters.Add("@TL_ID", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@TLComment", SqlDbType.VarChar).Value = txtComment.Text;


        try
        {
            if (conStr.State != ConnectionState.Open)
                conStr.Open();
            com.ExecuteNonQuery();
            BindGrid();
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            if (conStr.State != ConnectionState.Closed)
                conStr.Close();
        }
    }
}
