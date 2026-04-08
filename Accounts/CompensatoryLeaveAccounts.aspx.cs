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

public partial class CompensatoryLeaveAccounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            if (Request.QueryString.Count != 0)
            {
                BindGrid(Request.QueryString[0].ToString());
            }
        }
    }

    private void BindGrid(string DateAndDepartment)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchEmployeeListForCompensatoryLeaveAccountsApproval]", conStr);
        Com.Parameters.Add("@Date", SqlDbType.VarChar).Value = DateAndDepartment;

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            DataTable DT = command.ExecuteStoredProcedure(Com);
            gvEmployeeList.DataSource = DT;
            gvEmployeeList.DataBind();
            Utility.SetGridCss(gvEmployeeList);
            if (DT.Rows.Count != 0)
            {
                Hide.Visible = true;
                Error.InnerHtml = "";
            }
            else
            {
                Hide.Visible = false;
                Error.InnerHtml = "No Compensatory Leave Pending For Approval.";
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        string IDs = "";
        try
        {
            StringBuilder XML = new StringBuilder();
            XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            XML.Append("<CompensatoryLeave>");

            foreach (GridViewRow gvR in gvEmployeeList.Rows)
            {
                DropDownList ddlApproval = (DropDownList)gvR.FindControl("ddlApproval");

                HiddenField ID = (HiddenField)gvR.FindControl("hfID");

                if (ddlApproval.SelectedValue != "0")
                {
                    IDs += ID.Value + ",";
                    XML.Append("<Leave ID=\"" + ID.Value.ToString() + "\" AccID=\"" + Session["LoginID"].ToString() + "\" AccApproval=\"" + ddlApproval.SelectedValue + "\" AccComment =\"" + txtHRComment.Text + "\" />");
                }
            }
            XML.Append("</CompensatoryLeave>");


            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("[UpdateCompensatoryLeaveAccounts]", conStr);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "CompOff Updated By Accounts From Form");


            try
            {
                if (conStr.State != ConnectionState.Open)
                    conStr.Open();
                com.ExecuteNonQuery();
                SendMail(IDs);
                Error.InnerHtml = "Record Updated.";

            }
            catch (Exception ee)
            {
                Session["Ex"] = ee.Message.ToString();
                ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
            finally
            {
                BindGrid(Request.QueryString[0].ToString());
                conStr.Close();
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {
            btnSubmit.Visible = true;
        }
    }

    private void SendMail(string IDs)
    {

        string SenderMailID = command.ExecuteScalar("SELECT EmailID FROM   UserMaster WHERE  ID = " + Session["LoginID"] + "");


        if (IDs.Length > 0)
            IDs = IDs.Substring(0, IDs.Length - 1);


        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("FetchTLDetailCompensatoryLeaveDetailForMail", conStr);
        DataTable TLIDs = new DataTable();
        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@Ids", SqlDbType.VarChar).Value = IDs;
            TLIDs = command.ExecuteStoredProcedure(Com);
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



        foreach (DataRow Dr in TLIDs.Rows)
        {
            try
            {
                Com.CommandText = "FetchCompensatoryLeaveDetailForMailAccounts";
                Com.Parameters.Clear();
                Com.Parameters.Add("@Ids", SqlDbType.VarChar).Value = IDs;
                Com.Parameters.Add("@TLID", SqlDbType.Int).Value = Dr["TLID"].ToString();

                string MailDetails = Utility.CreateHTMLTable(command.ExecuteStoredProcedure(Com), "TT");
               

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
                Email += "        Compensatory Leave Approval Detail From Accounts";
                Email += "    </h1>";
                Email += "    <fieldset style='padding: 5px'>";
                Email += "        <table style='width: 100%'>";
                Email += "            <tr>";
                Email += "                <td style='width: 20%'>";
                Email += "                </td>";
                Email += "                <td style='width: 60%'>";
                Email += "                    <table style='border: black thin solid;' width='95%' align='center'>";
                Email += "                        <tr>";
                Email += "                            <td>";
                Email += "                                Dear :" + Dr["Name"].ToString() + "</td>";

                Email += "                        <tr>";
                Email += "                            <td >";
                Email += "Following Is The Detail Of Compensatory Leave</td>";
                Email += "                        </tr>";
                Email += "                        <tr>";
                Email += "                            <td >";
                Email += MailDetails + "</td>";
                Email += "                        </tr>";
                Email += "                        <tr>";
                Email += "                            <td >";
                Email += "Regards : " + Dr["MName"].ToString() + "</td>";
                Email += "                        </tr>";
                Email += "                    </table>";
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
                Email += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + HREF + ">Click Here To Navigate Single Sign On</a>";
                Email += "</body>";
                Email += "</html>";
                #endregion
                string Result = SendEmailViaSMTP.SendEmail(SenderMailID, Dr["EmailID"].ToString(), ConfigurationManager.AppSettings["HRMailID"].ToString() + "," + Dr["MEmailID"].ToString() + "," + SenderMailID, "Leave Management System : Approved Compensatory Leave Detail From Accounts", Email);
            }
            catch
            { }
            finally
            {
                conStr.Close();
            }
        }




    }
    protected void ddlSelectAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvEmployeeList.Rows)
        {
            DropDownList ddlApproval = (DropDownList)gvR.FindControl("ddlApproval");
            ddlApproval.SelectedValue = ddlSelectAll.SelectedValue;
        }
    }
}