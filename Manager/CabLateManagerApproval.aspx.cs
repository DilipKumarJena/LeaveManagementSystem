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

public partial class Manager_CabLateManagerApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            if (Request.QueryString.Count != 0)
            {
                txtDate.Text = Request.QueryString[0].ToString();
                BindGrid();
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<PunchDetail>");

        foreach (GridViewRow gvR in gvEmployeePunchDetail.Rows)
        {
            HiddenField ID = (HiddenField)gvR.FindControl("hfID");
            TextBox CabLate = (TextBox)gvR.FindControl("txtCabLate");
            CheckBox CanNotCame = (CheckBox)gvR.FindControl("chkCanNotCame");


            //if (CabLate.Text != "0")
            XML.Append("<CabLate ID=\"" + ID.Value + "\" CabLate=\"" + CabLate.Text + "\" CabNotCame =\"" + CanNotCame.Checked + "\"  />");
        }

        XML.Append("</PunchDetail>");




        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("UpdatePunchCardDetailCabLateManager", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Cab Late Updated By Manager");


        try
        {
            conStr.Open();
            com.ExecuteNonQuery();
            divError.InnerText = "Record Updated";
            BindGrid();

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            divError.InnerText = "Some Error Occured";
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
    }


    private void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchPunchCardDetailForCabLateManager]", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@Date", SqlDbType.VarChar).Value = txtDate.Text;


        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            gvEmployeePunchDetail.DataSource = Emp;
            gvEmployeePunchDetail.DataBind();
            Utility.SetGridCss(gvEmployeePunchDetail);

            divError.InnerHtml = "";
            if (Emp.Rows.Count == 0)
            {
                btnUpdate.Visible = false;
                divError.InnerHtml = "No Punch Detail Found For Current Selection.";
            }
            else
            {
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
}
