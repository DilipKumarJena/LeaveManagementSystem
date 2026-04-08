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

public partial class CompensatoryLeaveManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchEmployeeListForManagerApproval]", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();

            DataTable DT = command.ExecuteStoredProcedure(Com);
            if (DT.Rows.Count != 0)
            {
                gvEmployeeList.DataSource = DT;
                gvEmployeeList.DataBind();
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
        string IDs = "";
        //ID,ManagerID,ManagerApproval,ManagerComment
        try
        {
            StringBuilder XML = new StringBuilder();
            XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            XML.Append("<CompensatoryLeave>");

            foreach (GridViewRow gvR in gvEmployeeList.Rows)
            {
                DropDownList ddlApproval = (DropDownList)gvR.FindControl("ddlApproval");
                DropDownList ddlApprovalDurationManager = (DropDownList)gvR.FindControl("ddlApprovalDurationManager");
                HiddenField ID = (HiddenField)gvR.FindControl("hfID");



                if (ddlApproval.SelectedValue != "0")
                {
                    XML.Append("<Leave ID=\"" + ID.Value.ToString() + "\" ManagerID=\"" + Session["LoginID"].ToString() + "\" ManagerApproval=\"" + ddlApproval.SelectedValue + "\" ManagerComment =\"" + txtManagerComment.Text + "\" ApprovalDurationManager =\"" + ddlApprovalDurationManager.SelectedItem.Text + "\" />");
                    IDs += ID.Value + ",";
                }
            }
            XML.Append("</CompensatoryLeave>");


            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("[UpdateCompensatoryLeaveManager]", conStr);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Comp Off Updated By Manager");


            try
            {
                if (conStr.State != ConnectionState.Open)
                    conStr.Open();
                com.ExecuteNonQuery();
                SendMail(IDs);
                BindGrid();
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
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }

    }

    private void SendMail(string IDs)
    {
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
                Com.CommandText = "FetchCompensatoryLeaveDetailForMail";
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
                Email += "        Compensatory Leave Detail";
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
                Email += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + HREF + ">Click Here To Navigate Leave Management System</a>";
                Email += "</body>";
                Email += "</html>";
                #endregion

                string Result = SendEmailViaSMTP.SendEmail(Dr["MEmailID"].ToString(), Dr["EmailID"].ToString(), "", "Leave Management System : Approved Compensatory Leave Detail From Manager", Email);
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




    }






    protected DataTable OverTime(string Date)
    {
        DataTable dtOverTime = new DataTable();
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchOverTime]", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
            Com.Parameters.Add("@Date", SqlDbType.VarChar).Value = Date;
            dtOverTime = command.ExecuteStoredProcedure(Com);
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
        return dtOverTime;
    }

    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlApprovalDurationManager = (DropDownList)e.Row.FindControl("ddlApprovalDurationManager");
            Label CompOffDate = (Label)e.Row.FindControl("lblCompOffDate");
            Label Duration = (Label)e.Row.FindControl("lblDuration");


            DataTable dtOverTime = OverTime(CompOffDate.Text.Split(':')[1]);

            ddlApprovalDurationManager.DataSource = dtOverTime;
            ddlApprovalDurationManager.DataTextField = "Time";
            ddlApprovalDurationManager.DataValueField = "ID";
            ddlApprovalDurationManager.DataBind();


            ddlApprovalDurationManager.SelectedItem.Text = Duration.Text;
        }
    }
    protected void ddlApproval1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvEmployeeList.Rows)
        {
            DropDownList Approval = (DropDownList)gvR.FindControl("ddlApproval");
            Approval.SelectedValue = ddlApproval1.SelectedValue;
        }
    }
}
