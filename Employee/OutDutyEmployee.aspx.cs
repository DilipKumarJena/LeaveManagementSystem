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

public partial class Employee_OutDutyEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            # region Fetch Employee Record

            SqlCommand com = new SqlCommand();
            com.CommandText = "FetchEmployeeRecord";

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@ID", SqlDbType.Int).Value = Session["LoginID"];

            try
            {
                DataTable EmpDetail = command.ExecuteStoredProcedure(com);

                lblEmployeeId.Text = EmpDetail.Rows[0]["EMPCode"].ToString();
                lblEmployeeName.Text = EmpDetail.Rows[0]["Name"].ToString();
                lblLocation.Text = EmpDetail.Rows[0]["Location"].ToString();
                lblDepartmentName.Text = EmpDetail.Rows[0]["DepartmentName"].ToString();
                lblDesignation.Text = EmpDetail.Rows[0]["DesignationName"].ToString();
                lblEmailID.Text = EmpDetail.Rows[0]["EmailID"].ToString();

            }
            catch (Exception ee)
            {
                Session["Ex"] = ee.Message.ToString();
                ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
            finally
            {
            }
            # endregion
            BindGrid();
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime Start = Convert.ToDateTime(txtDate.Text + " " + TimeOut.ReturnTime());
            DateTime End = Convert.ToDateTime(txtDate.Text + " " + TimeIn.ReturnTime());
            if (Utility.CompareTime(TimeOut.ReturnTime(), TimeIn.ReturnTime()) == false)
            {
                lblMessage.Text = "Please Select Correct Time. Time Is Not In Order.";
                return;
            }
            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("InsertOutDutyEmployee", conStr);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = Session["LoginID"].ToString();
            com.Parameters.Add("@Purpose", SqlDbType.VarChar).Value = txtPurpose.Text;
            com.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text);
            com.Parameters.Add("@TimeOut", SqlDbType.DateTime).Value = Start;
            com.Parameters.Add("@TimeIn", SqlDbType.DateTime).Value = End;
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Employee Out Duty Form");
            try
            {
                conStr.Open();
                int i = com.ExecuteNonQuery();
                if (i == 0)
                {
                    Message.InnerText = "Out Duty Already Applied.";
                }
                else
                {
                    Message.InnerText = "Posted Successfully.";
                    DataTable EmailIDs = FetchEMailIDs(Session["LoginID"].ToString());
                    string Result = SendEmailViaSMTP.SendEmail(EmailIDs.Rows[0]["EmpMailID"].ToString(), EmailIDs.Rows[0]["TLMailID"].ToString(), EmailIDs.Rows[0]["MMailID"].ToString() + "," + EmailIDs.Rows[0]["EmpMailID"].ToString(), "Leave Management System : Out Duty", MailBody());

                    Message.InnerText += Result;

                }
                txtPurpose.Text = "";
                txtDate.Text = "";
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

    }


    public void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchOutDutyEmployee";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = Session["LoginID"];
        try
        {
            conStr.Open();
            DataTable Emp = command.ExecuteStoredProcedure(com);
            gvOutDuty.DataSource = Emp;
            gvOutDuty.DataBind();
            Utility.SetGridCss(gvOutDuty);
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



    public string MailBody()
    {
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
        Email += "        Out Duty : Employee";
        Email += "    </h1>";
        Email += "<table cellpadding='0' cellspacing='0' style='width: 100%'>";
        Email += "        <tr>";
        Email += "            <td style='width: 20%'>";
        Email += "            </td>";
        Email += "            <td style='width: 60%'>";
        Email += "                <fieldset style='padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px'>";
        Email += "                    <table width='95%' cellpadding='5px'>";
        Email += "                        <tbody>";
        Email += "                            <tr>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Employee Id</td>";
        Email += "                                <td valign='top' align='left' width='30%'>";
        Email += lblEmployeeId.Text + "</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Location</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += lblLocation.Text + "</td>";
        Email += "                            </tr>";
        Email += "                            <tr>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Employee Name</td>";
        Email += "                                <td valign='top' align='left' width='30%'>";
        Email += lblEmployeeName.Text + "</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Department</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += lblDepartmentName.Text + "</td>";
        Email += "                            </tr>";
        Email += "                            <tr>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    E-Mail ID</td>";
        Email += "                                <td valign='top' align='left' width='30%'>";
        Email += lblEmailID.Text + "</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Designation</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += lblDesignation.Text + "</td>";
        Email += "                            </tr>";
        Email += "                            <tr>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Out Duty Date</td>";
        Email += "                                <td valign='top' align='left' width='30%'>";
        Email += txtDate.Text + "</td>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                </td>";
        Email += "                                <td valign='top' align='left' width='30%'>";
        Email += "                                </td>";
        Email += "                            </tr>";
        Email += "                            <tr>";
        Email += "                                <td align='left' valign='top' width='20%'>";
        Email += "                                    Time Out</td>";
        Email += "                                <td align='left' valign='top' width='30%'>";
        Email += TimeOut.ReturnTime() + "</td>";
        Email += "                                <td align='left' valign='top' width='20%'>";
        Email += "                                    Time In</td>";
        Email += "                                <td align='left' valign='top' width='30%'>";
        Email += TimeIn.ReturnTime() + "</td>";
        Email += "                            </tr>";
        Email += "                            <tr>";
        Email += "                                <td valign='top' align='left' width='20%'>";
        Email += "                                    Purpose</td>";
        Email += "                                <td valign='top' align='left' colspan='3'>";
        Email += txtPurpose.Text + "</td>";
        Email += "                            </tr>                           ";
        Email += "                        </tbody>";
        Email += "                    </table>";
        Email += "                </fieldset>";
        Email += "            </td>";
        Email += "            <td style='width: 20%'>";
        Email += "            </td>";
        Email += "        </tr>";
        Email += "    </table>";
        string Path = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf('/'));
        Path = Path.Substring(0, Path.LastIndexOf('/'));
        string HREF = Session["ReturnURL"].ToString();
        Email += "<br/>";
        Email += "<br/>";
        Email += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + HREF + ">Click Here To Navigate Leave Management System</a>";
        Email += "</body>";
        Email += "</html>";
        return Email;
    }

    private DataTable FetchEMailIDs(string LoginID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchMailIDLeavePosting", conStr);
        com.CommandType = CommandType.StoredProcedure;
        DataTable EMailIDs = new DataTable();


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

        try
        {
            conStr.Open();
            EMailIDs = command.ExecuteStoredProcedure(com);
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
        return EMailIDs;
    }
}