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

public partial class LeaveApprovalByTL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            FetchLeave();

        }
    }

    private void FetchLeave()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchPostedLeaveForApprovalTL]", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"];
        try
        {
            conStr.Open();
            DataTable DT = command.ExecuteStoredProcedure(com);
            if (DT.Rows.Count != 0)
            {
                gvLeaveDetail.DataSource = DT;
                gvLeaveDetail.DataBind();

                Utility.SetGridCss(gvLeaveDetail);


                DivMessage.InnerHtml = "";
                ShodDiv.Visible = true;
            }
            else
            {
                DivMessage.InnerHtml = "No Leaves Pending For Approval.";
                ShodDiv.Visible = false;
            }
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

    private DataTable FetchLeaveDetailByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();



        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveDetailByLeavePostID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;
        DataTable Return = new DataTable();

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            Return = EmpLeaveDetail;
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            conStr.Close();
        }
        return Return;
        # endregion
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<Approval>");


        foreach (GridViewRow RR in gvLeaveDetail.Rows)
        {
            HiddenField hfID = (HiddenField)RR.FindControl("hfID");
            TextBox txtRemarkTL = (TextBox)RR.FindControl("txtRemarkTL");
            DropDownList ddlApproval = (DropDownList)RR.FindControl("ddlApproval");
            if (ddlApproval.SelectedValue != "0")
            {
                XML.Append("<Leave ID=\"" + hfID.Value + "\" RemarkTL=\"" + txtRemarkTL.Text + "\" ApprovedStatusTL=\"" + ddlApproval.SelectedValue + "\" />");
                SendMail(hfID.Value, ddlApproval.SelectedValue);
            }
        }

        XML.Append("</Approval>");




        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("UpdateLeavePostApprovalTL", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();



        try
        {
            conStr.Open();
            com.ExecuteNonQuery();
            FetchLeave();

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





    protected string SendMail(string ID, string Status)
    {
        string FinalStatus = "";

        if (Status == "1")
            FinalStatus = "Approved";
        else if (Status == "-1")
            FinalStatus = "Not Approved";


        DataTable Detail = FetchLeaveDetailByLeavePostIDForMail(ID);
        string E = Detail.Rows[0]["E"].ToString();
        string TL = Detail.Rows[0]["TL"].ToString();
        string M = Detail.Rows[0]["M"].ToString();

        Detail.Columns.Remove("E");
        Detail.Columns.Remove("TL");
        Detail.Columns.Remove("M");


        #region Email Text
        string Email = "";
        Email += "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
        Email += "<html xmlns=\"http://www.w3.org/1999/xhtml\" >";
        Email += "<head>";
        Email += "    <title></title>";
        Email += Utility.GetCSSInString();
        Email += "</head>";
        Email += "<body>";
        Email += "        <br />";
        Email += "    <h1>";
        Email += "        Leave Status By Team Leader : " + FinalStatus;
        Email += "    </h1>";
        Email += "    <fieldset style='padding: 5px'>";
        Email += "        <table style='width: 100%'>";
        Email += "            <tr>";
        Email += "                <td style='width: 20%'>";
        Email += "                </td>";
        Email += "                <td style='width: 60%'>";
        Email += Utility.CreateHTMLTable(Detail, "Detail");
        Email += "                </td>";
        Email += "                <td style='width: 20%'>";
        Email += "                </td>";
        Email += "            </tr>";
        Email += "        </table>";
        Email += "        <br />";

        string Path = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf('/'));
        Path = Path.Substring(0, Path.LastIndexOf('/'));
        string HREF = Session["ReturnURL"].ToString();
        Email += "<br/>";
        Email += "<br/>";
        Email += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + HREF + ">Click Here To Navigate Leave Management System</a>";
        Email += "</body>";
        Email += "</html>";
        #endregion
        string Result = SendEmailViaSMTP.SendEmail(TL, M, E, "Leave Management System : Leave Approved By Team Leader", Email);
        return Result;
    }
    private DataTable FetchLeaveDetailByLeavePostIDForMail(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        DataTable EmpLeaveDetail = new DataTable();


        # region Leave Detail Of Employee


        com.CommandText = "FetchLeaveDetailByLeavePostIDForMail";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;

        try
        {
            conStr.Open();
            EmpLeaveDetail = command.ExecuteStoredProcedure(com);


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
        # endregion
        return EmpLeaveDetail;
    }
    protected void gvLeaveDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            string ID = ((HiddenField)e.Row.FindControl("hfID")).Value.ToString();
            GridView AppliedLeave = (GridView)e.Row.FindControl("gvAppliedLeave");

            AppliedLeave.DataSource = FetchLeaveDetailByLeavePostID(ID);
            AppliedLeave.DataBind();
            Utility.SetGridCss(AppliedLeave);


        }
    }
    protected void ddlApproval_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((DropDownList)sender).NamingContainer);
        DropDownList ddlStatus = (DropDownList)row.FindControl("ddlApproval");
        HtmlGenericControl divStatus = (HtmlGenericControl)row.FindControl("divStatus");
        if (ddlStatus.SelectedValue == "-1")
        {
            divStatus.Visible = true;
        }
        else
        {
            divStatus.Visible = false;
        }

    }
}