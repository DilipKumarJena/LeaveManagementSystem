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
using System.Web.Mail;
using System.Text;

public partial class LeavePost : System.Web.UI.Page
{
    DataTable SelectedLeaves = new DataTable();
    DataTable EmpLeaveDetail = new DataTable();
    string LoginEmployeeID = "";
    protected void Page_Load(object sender, EventArgs e)
    {




        LoginEmployeeID = Session["LoginID"].ToString();



        if (!(IsPostBack))
        {
          



            aMusterRoll.Attributes.Add("href", "ShowMusterRoll.aspx?EmpCode=" + Session["EmployeeCode"].ToString() + "");

            command.ExecuteQuery("EXEC UpdateLockCompOff  " + LoginEmployeeID);
            command.ExecuteQuery("EXEC UpdateLockRoasteringOff  " + LoginEmployeeID);


            SelectedLeaves.Columns.Add("HalfFull", typeof(string));
            SelectedLeaves.Columns.Add("FirstSecond", typeof(string));
            SelectedLeaves.Columns.Add("From", typeof(string));
            SelectedLeaves.Columns.Add("To", typeof(string));
            SelectedLeaves.Columns.Add("LeaveType", typeof(string));
            SelectedLeaves.Columns.Add("CompOffDate", typeof(string));
            SelectedLeaves.Columns.Add("CompOffIds", typeof(string));




            Utility.SetGridCss(gvLeaveDetail);


            Session["SelectedLeaves"] = SelectedLeaves;


            # region Fetch Employee Record For Leave Post

            SqlCommand com = new SqlCommand();
            com.CommandText = "FetchEmployeeRecord";

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@ID", SqlDbType.Int).Value = LoginEmployeeID;

            try
            {

                DataTable EmpDetail = command.ExecuteStoredProcedure(com);
                lblEmployeeId.Text = EmpDetail.Rows[0]["EMPCode"].ToString();
                lblEmployeeName.Text = EmpDetail.Rows[0]["Name"].ToString();
                lblLocation.Text = EmpDetail.Rows[0]["Location"].ToString();
                lblDepartmentName.Text = EmpDetail.Rows[0]["DepartmentName"].ToString();
                lblDesignation.Text = EmpDetail.Rows[0]["DesignationName"].ToString();
                lblEmailID.Text = EmpDetail.Rows[0]["EmailID"].ToString();
                lblCurrentDate.Text = Utility.GetServerDate();
                lblFirst.Text = EmpDetail.Rows[0]["TL"].ToString();
                lblSecond.Text = EmpDetail.Rows[0]["Manager"].ToString();

                //txtAddress.Text = EmpDetail.Rows[0]["PermanentAddress"].ToString();

                Utility.BindDDLWithOtherOptionWithSP(ddlHalfFullDay, "FetchHalfFull", "ID", "HalfFull", false);
                Utility.BindDDLWithOtherOptionWithSP(ddlFirstSecond, "[FetchFirstSecond]", "ID", "FirstSecond", false);



                com.CommandText = "[FetchLeaveTypeByLoginID]";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Clear();


                com.Parameters.Add("@LoginID", SqlDbType.VarChar, 50).Value = Session["LoginID"].ToString();


                try
                {
                    DataTable dt = command.ExecuteStoredProcedure(com);

                    ddlLeaveType.DataSource = dt;
                    ddlLeaveType.DataValueField = "ID";
                    ddlLeaveType.DataTextField = "LeaveName";
                    ddlLeaveType.DataBind();
                }
                catch (Exception ee)
                {
                    Session["Ex"] = ee.Message.ToString();
                    ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
                }
                finally
                {

                }
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



            AllLeaveStatus();


            CancelPostedLeave();
            LeaveStatusCancel();
        }


    }

    protected void LeaveStatusCancel()
    {

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();



        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveBalanceByEmployeeID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@EmpID", SqlDbType.Int).Value = LoginEmployeeID;

        try
        {
            conStr.Open();
            EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            aa.InnerHtml = Utility.CreateHTMLTableOnAjaxPage(this, "UpdatePanel1", EmpLeaveDetail, "LeaveStatus");
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

    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)(Session["SelectedLeaves"]);
        foreach (DataRow Dr in DT.Rows)
        {
            if (Dr[4].ToString() == "C - Compensatory Leave (In Hours)")
            {
                string SessionName = Dr[2].ToString() + "~" + Dr[3].ToString() + "~" + Dr[0].ToString();
                if (Session[SessionName] == null)
                {
                    lblMessage.Text = "Please Adjust Comp Off Leave With Your Comp Off Balance.";

                    gvLeaveDetail.DataSource = DT;
                    gvLeaveDetail.DataBind();

                    return;
                }
                else
                {
                    Dr[6] = Session[SessionName].ToString();
                }
            }
            else if (Dr[4].ToString() == "RoL - Roastering Off Leave (In Hours)")
            {
                string SessionName = Dr[2].ToString() + "~" + Dr[3].ToString() + "~" + Dr[0].ToString();
                if (Session[SessionName] == null)
                {
                    lblMessage.Text = "Please Adjust Roastering Off Leave With Your Roastering Off Balance.";

                    gvLeaveDetail.DataSource = DT;
                    gvLeaveDetail.DataBind();

                    return;
                }
                else
                {
                    Dr[6] = Session[SessionName].ToString();
                }
            }
        }









        DataTable SelectedLeavesForHalfDayPunchValiodation = (DataTable)(Session["SelectedLeaves"]);
        foreach (DataRow Dr in SelectedLeavesForHalfDayPunchValiodation.Rows)
        {
            bool PunchFound = FetchPunchFound(LoginEmployeeID.ToString(), Dr["From"].ToString(), Dr["To"].ToString());
            string HalfDay = Dr["HalfFull"].ToString();


            int OldDays = ((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(Dr["From"].ToString()))).Days;

            if (HalfDay == "Half Day" && PunchFound == false && OldDays >= 1 && SelectedLeavesForHalfDayPunchValiodation.Rows.Count == 1)
            {
                Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Punch Not Found For Date : " + txtFromDate.Text + ". Please Update Your Punch Detail In HR For Half Day Leave Apply.");
                return;
            }
        }


        if (gvLeaveDetail.Rows.Count == 1)
        {
            string Fest = Convert.ToString(command.ExecuteScalar("Select gold.FetchOKSHolidayNameByEmployeeIDAndDate(" + LoginEmployeeID + ",'" + gvLeaveDetail.Rows[0].Cells[2].Text.ToString() + "')"));

            if (Fest != "")
            {
                lblMessage.Text = "Selected Day Is A Festival. Please Do Not Apply.";
                SelectedLeaves.Rows.Clear();
                gvLeaveDetail.DataSource = SelectedLeaves;
                gvLeaveDetail.DataBind();
                return;
            }

        }







        if (gvLeaveDetail.Rows.Count == 0)
        {
            lblMessage.Text = "Please Select Leave And Click On Add.";
            return;
        }

        string Message = CheckThreeDaysLeavePostValidation(Convert.ToDateTime(gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[3].Text), Convert.ToInt32(LoginEmployeeID.ToString()));
        if (Message != "Can Apply.")
        {
            lblMessage.Text = Message;
            return;
        }
        string SessionID = Session.SessionID + DateTime.Now.ToLongTimeString();

        if (txtToDate.Text == "")
            txtToDate.Text = txtFromDate.Text.Replace('/', '-');


        //DataTable DT = (DataTable)(Session["SelectedLeaves"]);

        if (DT.Rows.Count == 0)
        {
            flashingtext.Attributes.Add("style", "display:''");
            return;
        }
        btnApply.Visible = false;

        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<AppliedLeave>");

        foreach (DataRow Dr in DT.Rows)
        {
            XML.Append("<Leave SessionID=\"" + SessionID + "\"  FullHalf=\"" + Dr["HalfFull"].ToString().Replace("<--Select-->", "") +
                "\" FirstSecond=\"" + Dr["FirstSecond"].ToString().Replace("<--Select-->", "")
                + "\" LeaveType=\"" + Dr["LeaveType"].ToString().Replace("<--Select-->", "")
                + "\" From=\"" + Dr["From"].ToString()
                + "\" To=\"" + Dr["To"].ToString()
                + "\" CompOffIds=\"" + Dr["CompOffIds"].ToString() + "\"/>");
        }
        XML.Append("</AppliedLeave>");
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[InsertEmployeeLeaveDetail]", conStr);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        com.Parameters.Add("@LeavePostedBy", SqlDbType.Int).Value = LoginEmployeeID;
        com.Parameters.Add("@EmployeeID ", SqlDbType.Int).Value = LoginEmployeeID;
        com.Parameters.Add("@Reason ", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtReason.Text);
        com.Parameters.Add("@AddressDuringLeave ", SqlDbType.VarChar).Value = Utility.CheckNullValue(txtAddress.Text);
        com.Parameters.Add("@DetailedSessionID ", SqlDbType.VarChar).Value = SessionID;
        SqlParameter Output = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 50);
        Output.Direction = ParameterDirection.Output;
        com.Parameters.Add(Output);
        try
        {



            conStr.Open();
            btnApply.Visible = false;
            com.ExecuteNonQuery();

            LeavePostStatus.InnerHtml = "";
            MessageSentStatus.InnerHtml = "";

            if (Output.Value.ToString() == "Leave Already Applied.")
                LeavePostStatus.InnerHtml = Output.Value.ToString();
            else if (Output.Value.ToString() == "Leave Posted Successfully.")
                LeavePostStatus.InnerHtml = Output.Value.ToString() + " : " + SendMail();


            Utility.ClearAllOnAjaxPage(this, "UpdatePanel1");
            //CancelPostedLeave();



            btnClearAppliedLeave_Click(null, null);
            txtReason.Text = "";
            txtAddress.Text = "";

            AllLeaveStatus();
            CancelPostedLeave();
            LeaveStatusCancel();

            btnApply.Visible = true;

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            LeavePostStatus.InnerHtml = "Some Error Occured";
        }
        finally
        {
            conStr.Close();
            btnApply.Visible = true;
        }
    }


    public void AllLeaveStatus()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchLeaveDetailByEmployeeID";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginEmployeeID;
        com.Parameters.Add("@Type", SqlDbType.Int).Value = rblLeave.SelectedValue;
        try
        {
            conStr.Open();
            spanShowAllLeaveDetail.InnerHtml = Utility.CreateHTMLTableOnAjaxPage(this, "UpdatePanel1", command.ExecuteStoredProcedure(com), "FetchLeaveDetailByEmployeeID");
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

    public void CancelPostedLeave()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchCancelPostedLeave";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginEmployeeID;
        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            repCancelPostedLeave.DataSource = EmpLeaveDetail;
            repCancelPostedLeave.DataBind();
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


    protected string SendMail()
    {
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
        Email += "        Applied Leave";
        Email += "    </h1>";
        Email += "    <fieldset style='padding: 5px'>";
        Email += "        <table style='width: 100%'>";
        Email += "            <tr>";
        Email += "                <td style='width: 20%'>";
        Email += "                </td>";
        Email += "                <td style='width: 60%'>";
        Email += "                    <table style='border: black thin solid;' width='95%' align='center'>";
        Email += "                        <tr>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>Employee Id</strong></td>";
        Email += "                            <td valign='top' align='left' width='30%'>";
        Email += lblEmployeeId.Text;
        Email += "                            </td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>Location</strong></td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += lblLocation.Text;
        Email += "                            </td>";
        Email += "                        </tr>";
        Email += "                        <tr>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>Employee Name</strong></td>";
        Email += "                            <td valign='top' align='left' width='30%'>";
        Email += lblEmployeeName.Text;
        Email += "                            </td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>Department</strong></td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += lblDepartmentName.Text;
        Email += "                            </td>";
        Email += "                        </tr>";
        Email += "                        <tr>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>E-Mail ID</strong></td>";
        Email += "                            <td valign='top' align='left' width='30%'>";
        Email += lblEmailID.Text;
        Email += "                            </td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>Designation</strong></td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += lblDesignation.Text;
        Email += "                            </td>";
        Email += "                        </tr>";
        Email += "                        <tr>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                            </td>";
        Email += "                            <td valign='top' align='left' width='30%'>";
        Email += "                            </td>";
        Email += "                            <td valign='top' align='left' width='20%'>";
        Email += "                                <strong>Current Date</strong></td>";
        Email += "                            <td valign='top' align='left' width='30%'>";
        Email += lblCurrentDate.Text;
        Email += "                            </td>";
        Email += "                        </tr>";
        Email += "                        <tr>";
        Email += "                            <td align='left'  valign='top' width='20%'>";
        Email += "                                <strong>Reason</strong></td>";
        Email += "                            <td align='left' colspan='3'  valign='top'>";
        Email += txtReason.Text;
        Email += "                            </td>";
        Email += "                        </tr>";
        Email += "                        <tr>";
        Email += "                            <td align='left' valign='top' width='20%'>";
        Email += "                                <strong>Address During Leave</strong></td>";
        Email += "                            <td align='left' colspan='3' valign='top'>";
        Email += txtAddress.Text;
        Email += "                            </td>";
        Email += "                        </tr>";
        Email += "                    </table>";
        Email += "                </td>";
        Email += "                <td style='width: 20%'>";
        Email += "                </td>";
        Email += "            </tr>";
        Email += "        </table>";
        Email += "        <br />";
        Email += Utility.CreateHTMLTable((DataTable)Session["SelectedLeaves"], "SelectedLeaves");
        string Path = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf('/'));
        Path = Path.Substring(0, Path.LastIndexOf('/'));
        string HREF = Session["ReturnURL"].ToString();
        Email += "<br/>";
        Email += "<br/>";
        Email += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + HREF + ">Click Here To Navigate Leave Management System</a>";
        Email += "</body>";
        Email += "</html>";
        #endregion

        DataTable EmailIDs = FetchEMailIDs(LoginEmployeeID.ToString());
        string Result = SendEmailViaSMTP.SendEmail(EmailIDs.Rows[0]["EmpMailID"].ToString(), EmailIDs.Rows[0]["TLMailID"].ToString(), EmailIDs.Rows[0]["MMailID"].ToString() + "," + EmailIDs.Rows[0]["EmpMailID"].ToString(), "Leave Management System : Leave Post", Email);

        return Result;
    }



    protected void repCancelPostedLeave_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        #region Delete Posted Leave
        if (e.CommandName == "CancelPostedLeave")
        {
            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("CancelPostedLeave", conStr);
            com.CommandType = CommandType.StoredProcedure;

            string arg = e.CommandArgument.ToString();

            com.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = arg;

            try
            {
                conStr.Open();
                com.ExecuteNonQuery();
                AllLeaveStatus();
                CancelPostedLeave();
                LeaveStatusCancel();
                lblMessage.Text = "Cancel Successfully.";
            }
            catch (Exception ee)
            {
                Session["Ex"] = ee.Message.ToString();
                lblMessage.Text = "Some Error Occured";
                ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
            finally
            {
                conStr.Close();
            }
        }
        #endregion
    }





    protected void btnAddLeave_Click(object sender, EventArgs e)
    {
        if (lblDepartmentName.Text == "Voice")
        {
            if (lblDesignation.Text == "Asst Manager" || lblDesignation.Text == "Manager")
            {
                //NonVoiceEmployee();
                VoiceEmployee();
            }
            else
            {
                VoiceEmployee();
            }
        }
        else
        {
            NonVoiceEmployee();
        }


        if (gvLeaveDetail.Rows.Count > 1)
        {
            DateTime First, Second;
            for (int i = 0; i < gvLeaveDetail.Rows.Count - 1; i++)
            {
                First = Convert.ToDateTime(gvLeaveDetail.Rows[i].Cells[3].Text.ToString());
                Second = Convert.ToDateTime(gvLeaveDetail.Rows[i + 1].Cells[3].Text.ToString());

                if ((((TimeSpan)(Convert.ToDateTime(Second) - Convert.ToDateTime(First))).Days) < 0)
                {
                    SelectedLeaves.Rows[SelectedLeaves.Rows.Count - 1].Delete();
                    SelectedLeaves.Rows.Clear();
                    gvLeaveDetail.DataSource = SelectedLeaves;
                    gvLeaveDetail.DataBind();

                    Session["SelectedLeaves"] = SelectedLeaves;

                    lblMessage.Text = "Selected Dated Are Not In Order.";
                    return;
                }


                if ((((TimeSpan)(Convert.ToDateTime(First) - Convert.ToDateTime(Second))).Days) > 0 && gvLeaveDetail.Rows[i].Cells[0].Text == "Half Day")
                {
                    SelectedLeaves.Rows[SelectedLeaves.Rows.Count - 1].Delete();
                    SelectedLeaves.Rows.Clear();
                    gvLeaveDetail.DataSource = SelectedLeaves;
                    gvLeaveDetail.DataBind();

                    Session["SelectedLeaves"] = SelectedLeaves;

                    lblMessage.Text = "Selected Dated Are Not In Order.";
                    return;
                }
                else if ((((TimeSpan)(Convert.ToDateTime(First) - Convert.ToDateTime(Second))).Days) == 0 && gvLeaveDetail.Rows[i].Cells[0].Text == "Full Day")
                {
                    SelectedLeaves.Rows[SelectedLeaves.Rows.Count - 1].Delete();
                    SelectedLeaves.Rows.Clear();
                    gvLeaveDetail.DataSource = SelectedLeaves;
                    gvLeaveDetail.DataBind();

                    Session["SelectedLeaves"] = SelectedLeaves;

                    lblMessage.Text = "Selected Dated Are Not In Order.";
                    return;
                }
            }
        }
        ddlFirstSecond.SelectedIndex = 0;
        ddlHalfFullDay.SelectedIndex = 0;
    }

    private void VoiceEmployee()
    {
        lblMessage.Text = "";
        DateTime ServerDate = DateTime.Parse(lblCurrentDate.Text);

        bool Error = false;

        SelectedLeaves = (DataTable)Session["SelectedLeaves"];

        string LWPLeaveType = "W/O - Week End / OKS Holiday";
        string LeaveType = ddlLeaveType.SelectedItem.Text.Replace("<--Select-->", "");
        string HalfFullDay = ddlHalfFullDay.SelectedItem.Text.Replace("<--Select-->", "");
        string FirstSecond = ddlFirstSecond.SelectedItem.Text.Replace("<--Select-->", "");
        string From = txtFromDate.Text;
        string To = txtToDate.Text;
        string AppliedLeaveAsPerVoiceRule = "";

        if (To == "")
            To = From;

        TimeSpan aa = Convert.ToDateTime(To) - Convert.ToDateTime(From);
        for (int i = 0; i <= aa.Days; i++)
        {
            DateTime abc = Convert.ToDateTime(From).AddDays(i);

            string CheckDate = command.ExecuteScalar("Select Gold.FetchOKSHolidays( '" + Convert.ToInt32(LoginEmployeeID.ToString()) + "','" + abc + "')");

            if (CheckDate == "O" || CheckDate == "W")
            {
                AppliedLeaveAsPerVoiceRule = LWPLeaveType;
            }
            else
            {
                AppliedLeaveAsPerVoiceRule = LeaveType;
            }
            string ReturnMessageFinal = CheckValidityOfAppliedLeave(abc, abc, Convert.ToInt32(LoginEmployeeID.ToString()));
            if (ReturnMessageFinal != "Can Apply.")
            {
                lblMessage.Text = ReturnMessageFinal;
                btnClearAppliedLeave_Click(null, null); return;
            }

            if (((TimeSpan)(Convert.ToDateTime(From) - Convert.ToDateTime(To))).Days > 0)
            {
                lblMessage.Text = "Selected Dated Are Not In Order.";
                btnClearAppliedLeave_Click(null, null); return;
            }
            if (gvLeaveDetail.Rows.Count != 0)
            {
                string LastRowTo = gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[3].Text.ToString();
                string LastRowFirstSecond = gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[1].Text.ToString();
                string LastRowHalfFullDay = gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[0].Text.ToString();



                if ((((TimeSpan)(Convert.ToDateTime(LastRowTo) - Convert.ToDateTime(From))).Days + 1) < 0)
                {
                    lblMessage.Text = "Selected Dated Are Not In Order.";
                    btnClearAppliedLeave_Click(null, null); return;
                }

                if (!(LastRowHalfFullDay == "Half Day" && LastRowFirstSecond == "First Half"))
                {
                    if (HalfFullDay == "Half Day" && FirstSecond == "Second Half")
                    {
                        lblMessage.Text = "You Can Not Apply Second Half.";
                        btnClearAppliedLeave_Click(null, null); return;
                    }
                }
            }



            if (AppliedLeaveAsPerVoiceRule == "SL - Sick Leave")
            {
                if (((TimeSpan)(Convert.ToDateTime(From) - ServerDate)).Days > 0)
                {
                    lblMessage.Text = "Can Not Post Sick Leave In Advance.";
                    btnClearAppliedLeave_Click(null, null); return;
                }
            }




            if (SelectedLeaves.Rows.Count >= 2)
            {

            }






            int CheckAlreadyApplied = 0;

            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("CheckAlreadyApplied", conStr);
            com.CommandType = CommandType.StoredProcedure;



            com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginEmployeeID.ToString();
            com.Parameters.Add("@From", SqlDbType.VarChar).Value = From;
            com.Parameters.Add("@To", SqlDbType.VarChar).Value = To;

            try
            {
                conStr.Open();
                CheckAlreadyApplied = Int32.Parse(com.ExecuteScalar().ToString());
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

            if (CheckAlreadyApplied != 0)
            {
                lblMessage.Text = "Can Not Apply Leave : Leave Already Applied";
                btnClearAppliedLeave_Click(null, null); return;
            }

            float ConsecutiveLeave = FetchConsecutiveLeave(LoginEmployeeID.ToString(), AppliedLeaveAsPerVoiceRule);
            float LeaveBalance = FetchLeaveBalanceByLoginID(LoginEmployeeID.ToString(), AppliedLeaveAsPerVoiceRule);




            float TotalDays = 0;
            if (HalfFullDay == "Half Day")
                TotalDays = .5f;
            else
                TotalDays = ((TimeSpan)Convert.ToDateTime(abc).Subtract(Convert.ToDateTime(abc))).Days + 1;




            //CL - Casual Leave
            //SL - Sick Leave
            //PL - Privilege Leave
            //C - Compensatory Leave

            #region Validation


            float AppliedLeave = 0;
            float TotalDaysCLApplied = 0;
            float TotalDaysSLApplied = 0;
            float TotalDaysPLApplied = 0;
            float TotalDaysCApplied = 0;

            bool CLApplied = false;
            bool SLApplied = false;
            bool PLApplied = false;
            bool CompOffApplied = false;




            foreach (DataRow DrSelectedLeaves in SelectedLeaves.Rows)
            {
                if (DrSelectedLeaves["HalfFull"].ToString() == "Half Day")
                    AppliedLeave = .5f;
                else
                    AppliedLeave = ((TimeSpan)Convert.ToDateTime(DrSelectedLeaves["To"].ToString()).Subtract(Convert.ToDateTime(DrSelectedLeaves["From"].ToString()))).Days + 1;

                if (DrSelectedLeaves["LeaveType"].ToString() == "CL - Casual Leave")
                {
                    CLApplied = true;
                    TotalDaysCLApplied += AppliedLeave;
                }
                if (DrSelectedLeaves["LeaveType"].ToString() == "SL - Sick Leave")
                {
                    SLApplied = true;
                    TotalDaysSLApplied += AppliedLeave;
                }
                if (DrSelectedLeaves["LeaveType"].ToString() == "PL - Privilege Leave")
                {
                    PLApplied = true;
                    TotalDaysPLApplied += AppliedLeave;
                }
                if (DrSelectedLeaves["LeaveType"].ToString() == "C - Compensatory Leave (In Hours)")
                {
                    CompOffApplied = true;
                    TotalDaysCApplied += AppliedLeave;
                }
            }

            #region Revoke Mixture OF SL,CL,PL & CompOff
            if ((CLApplied == true) && ((LeaveType == "SL - Sick Leave") || (LeaveType == "PL - Privilege Leave") || (LeaveType == "C - Compensatory Leave (In Hours)")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;

            }
            if ((SLApplied == true) && ((LeaveType == "CL - Casual Leave") || (LeaveType == "PL - Privilege Leave") || (LeaveType == "C - Compensatory Leave (In Hours)")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((PLApplied == true) && ((LeaveType == "SL - Sick Leave") || (LeaveType == "CL - Casual Leave") || (LeaveType == "C - Compensatory Leave (In Hours)")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((CompOffApplied == true) && ((LeaveType == "SL - Sick Leave") || (LeaveType == "CL - Casual Leave") || (LeaveType == "PL - Privilege Leave")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;
            }


            #endregion

            #region Can Not Apply ConsecutiveLeave
            if ((AppliedLeaveAsPerVoiceRule == "CL - Casual Leave") && ((TotalDaysCLApplied + TotalDays) > ConsecutiveLeave))
            {
                lblMessage.Text = "Can Not Apply 'CL - Casual Leave' More Than " + ConsecutiveLeave + " Day.";
                btnClearAppliedLeave_Click(null, null); return;
            }

            if ((AppliedLeaveAsPerVoiceRule == "SL - Sick Leave") && ((TotalDaysSLApplied + TotalDays) > ConsecutiveLeave))
            {
                //lblMessage.Text = "Can Not Apply 'SL - Sick Leave' More Than " + ConsecutiveLeave + " Day.";
                //btnClearAppliedLeave_Click(null, null); return;
            }
            #endregion

            #region Can Not Apply, No Sufficient Balance
            if ((AppliedLeaveAsPerVoiceRule == "CL - Casual Leave") && ((TotalDaysCLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'CL - Casual Leave'.";


                btnClearAppliedLeave_Click(null, null); return;
            }

            if ((AppliedLeaveAsPerVoiceRule == "SL - Sick Leave") && ((TotalDaysSLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'SL - Sick Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }

            if ((AppliedLeaveAsPerVoiceRule == "PL - Privilege Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'PL - Privilege Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }
            if ((AppliedLeaveAsPerVoiceRule == "C - Compensatory Leave (In Hours)") && (((TotalDaysCApplied + TotalDays) * 8) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'C - Compensatory Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }
            if ((AppliedLeaveAsPerVoiceRule == "LWP - Leave With Out Pay") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'Leave With Out Pay'.";
                btnClearAppliedLeave_Click(null, null); return;
            }

            if ((AppliedLeaveAsPerVoiceRule == "ML - Maternity Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'ML - Maternity Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }

            if ((AppliedLeaveAsPerVoiceRule == "OT - On Tour Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'On Tour Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }
            if ((AppliedLeaveAsPerVoiceRule == "ESI - ESI Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'ESI Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }
            if ((AppliedLeaveAsPerVoiceRule == "MiL - Marriage Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'Marriage Leave'.";
                btnClearAppliedLeave_Click(null, null); return;
            }




            #endregion







            #endregion
            if (Error == true)
            {

            }
            else
            {
                DataRow Dr = SelectedLeaves.NewRow();
                Dr["HalfFull"] = ddlHalfFullDay.SelectedItem.Text.Replace("<--Select-->", "");
                Dr["FirstSecond"] = ddlFirstSecond.SelectedItem.Text.Replace("<--Select-->", "");
                Dr["From"] = abc.ToShortDateString();
                Dr["To"] = abc.ToShortDateString();
                Dr["LeaveType"] = AppliedLeaveAsPerVoiceRule;

                SelectedLeaves.Rows.Add(Dr);

                gvLeaveDetail.DataSource = SelectedLeaves;
                gvLeaveDetail.DataBind();

                Session["SelectedLeaves"] = SelectedLeaves;

                lblMessage.Text = "";

                btnApply.Visible = true;
                flashingtext.Visible = false;

            }
        }


    }

    private void NonVoiceEmployee()
    {
        lblMessage.Text = "";
        DateTime ServerDate = DateTime.Parse(lblCurrentDate.Text);

        bool Error = false;

        SelectedLeaves = (DataTable)Session["SelectedLeaves"];

        string LeaveType = ddlLeaveType.SelectedItem.Text.Replace("<--Select-->", "");
        string HalfFullDay = ddlHalfFullDay.SelectedItem.Text.Replace("<--Select-->", "");
        string FirstSecond = ddlFirstSecond.SelectedItem.Text.Replace("<--Select-->", "");
        string From = txtFromDate.Text;
        string To = txtToDate.Text;
        if (To == "")
            To = From;

        TimeSpan aa = Convert.ToDateTime(To) - Convert.ToDateTime(From);
        for (int i = 0; i <= aa.Days; i++)
        {
            DateTime abc = Convert.ToDateTime(From).AddDays(i);

            string ReturnMessageFinal = CheckValidityOfAppliedLeave(abc, abc, Convert.ToInt32(LoginEmployeeID.ToString()));

            if (ReturnMessageFinal != "Can Apply.")
            {
                lblMessage.Text = ReturnMessageFinal;
                return;
            }

            if (((TimeSpan)(Convert.ToDateTime(From) - Convert.ToDateTime(To))).Days > 0)
            {
                lblMessage.Text = "Selected Dated Are Not In Order.";
                return;
            }
            if (gvLeaveDetail.Rows.Count != 0)
            {
                string LastRowTo = gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[3].Text.ToString();
                string LastRowFirstSecond = gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[1].Text.ToString();
                string LastRowHalfFullDay = gvLeaveDetail.Rows[gvLeaveDetail.Rows.Count - 1].Cells[0].Text.ToString();



                if ((((TimeSpan)(Convert.ToDateTime(LastRowTo) - Convert.ToDateTime(From))).Days + 1) < 0)
                {
                    lblMessage.Text = "Selected Dated Are Not In Order.";
                    return;
                }

                if (!(LastRowHalfFullDay == "Half Day" && LastRowFirstSecond == "First Half"))
                {
                    if (HalfFullDay == "Half Day" && FirstSecond == "Second Half")
                    {
                        lblMessage.Text = "You Can Not Apply Second Half.";
                        return;
                    }
                }
            }



            if (LeaveType == "SL - Sick Leave")
            {
                if (((TimeSpan)(Convert.ToDateTime(From) - ServerDate)).Days > 0)
                {
                    lblMessage.Text = "Can Not Post Sick Leave In Advance.";
                    return;
                }
            }




            if (SelectedLeaves.Rows.Count >= 2)
            {

            }






            int CheckAlreadyApplied = 0;

            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("CheckAlreadyApplied", conStr);
            com.CommandType = CommandType.StoredProcedure;



            com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginEmployeeID.ToString();
            com.Parameters.Add("@From", SqlDbType.VarChar).Value = abc;
            com.Parameters.Add("@To", SqlDbType.VarChar).Value = abc;

            try
            {
                conStr.Open();
                CheckAlreadyApplied = Int32.Parse(com.ExecuteScalar().ToString());
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

            if (CheckAlreadyApplied != 0)
            {
                lblMessage.Text = "Can Not Apply Leave : Leave Already Applied";
                return;
            }

            float ConsecutiveLeave = FetchConsecutiveLeave(LoginEmployeeID.ToString(), LeaveType);
            float LeaveBalance = FetchLeaveBalanceByLoginID(LoginEmployeeID.ToString(), LeaveType);




            float TotalDays = 0;
            if (HalfFullDay == "Half Day")
                TotalDays = .5f;
            else
                TotalDays = ((TimeSpan)Convert.ToDateTime(abc).Subtract(Convert.ToDateTime(abc))).Days + 1;




            //CL - Casual Leave
            //SL - Sick Leave
            //PL - Privilege Leave
            //C - Compensatory Leave

            #region Validation


            float AppliedLeave = 0;
            float TotalDaysCLApplied = 0;
            float TotalDaysSLApplied = 0;
            float TotalDaysPLApplied = 0;
            float TotalDaysCApplied = 0;

            bool CLApplied = false;
            bool SLApplied = false;
            bool PLApplied = false;
            bool CompOffApplied = false;




            foreach (DataRow DrSelectedLeaves in SelectedLeaves.Rows)
            {
                if (DrSelectedLeaves["HalfFull"].ToString() == "Half Day")
                    AppliedLeave = .5f;
                else
                    AppliedLeave = ((TimeSpan)Convert.ToDateTime(DrSelectedLeaves["To"].ToString()).Subtract(Convert.ToDateTime(DrSelectedLeaves["From"].ToString()))).Days + 1;

                if (DrSelectedLeaves["LeaveType"].ToString() == "CL - Casual Leave")
                {
                    CLApplied = true;
                    TotalDaysCLApplied += AppliedLeave;
                }
                if (DrSelectedLeaves["LeaveType"].ToString() == "SL - Sick Leave")
                {
                    SLApplied = true;
                    TotalDaysSLApplied += AppliedLeave;
                }
                if (DrSelectedLeaves["LeaveType"].ToString() == "PL - Privilege Leave")
                {
                    PLApplied = true;
                    TotalDaysPLApplied += AppliedLeave;
                }
                if (DrSelectedLeaves["LeaveType"].ToString() == "C - Compensatory Leave (In Hours)")
                {
                    CompOffApplied = true;
                    TotalDaysCApplied += AppliedLeave;
                }
            }

            #region Revoke Mixture OF SL,CL,PL & CompOff
            if ((CLApplied == true) && ((LeaveType == "SL - Sick Leave") || (LeaveType == "PL - Privilege Leave") || (LeaveType == "C - Compensatory Leave (In Hours)")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;

            }
            if ((SLApplied == true) && ((LeaveType == "CL - Casual Leave") || (LeaveType == "PL - Privilege Leave") || (LeaveType == "C - Compensatory Leave (In Hours)")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((PLApplied == true) && ((LeaveType == "SL - Sick Leave") || (LeaveType == "CL - Casual Leave") || (LeaveType == "C - Compensatory Leave (In Hours)")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((CompOffApplied == true) && ((LeaveType == "SL - Sick Leave") || (LeaveType == "CL - Casual Leave") || (LeaveType == "PL - Privilege Leave")))
            {
                lblMessage.Text = "Can Not Mix 'CL - Casual Leave' ,'SL - Sick Leave', 'PL - Privilege Leave' & 'C - Compensatory Leave (In Hours)'  ";
                btnClearAppliedLeave_Click(null, null);
                return;
            }


            #endregion

            #region Can Not Apply ConsecutiveLeave
            if ((LeaveType == "CL - Casual Leave") && ((TotalDaysCLApplied + TotalDays) > ConsecutiveLeave))
            {
                lblMessage.Text = "Can Not Apply 'CL - Casual Leave' More Than " + ConsecutiveLeave + " Day.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }

            if ((LeaveType == "SL - Sick Leave") && ((TotalDaysSLApplied + TotalDays) > ConsecutiveLeave))
            {
                //lblMessage.Text = "Can Not Apply 'SL - Sick Leave' More Than " + ConsecutiveLeave + " Day.";
                //return;
            }
            #endregion

            #region Can Not Apply, No Sufficient Balance
            if ((LeaveType == "CL - Casual Leave") && ((TotalDaysCLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'CL - Casual Leave'.";

                btnClearAppliedLeave_Click(null, null);
                return;
            }

            if ((LeaveType == "SL - Sick Leave") && ((TotalDaysSLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'SL - Sick Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }

            if ((LeaveType == "PL - Privilege Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'PL - Privilege Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((LeaveType == "C - Compensatory Leave (In Hours)") && (((TotalDaysCApplied + TotalDays) * 8) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'C - Compensatory Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((LeaveType == "LWP - Leave With Out Pay") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'Leave With Out Pay'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }

            if ((LeaveType == "ML - Maternity Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'ML - Maternity Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }

            if ((LeaveType == "OT - On Tour Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'On Tour Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((LeaveType == "ESI - ESI Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'ESI Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }
            if ((LeaveType == "MiL - Marriage Leave") && ((TotalDaysPLApplied + TotalDays) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'Marriage Leave'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }

            if ((LeaveType == "RoL - Roastering Off Leave (In Hours)") && (((TotalDaysPLApplied + TotalDays) * 8) > LeaveBalance))
            {
                lblMessage.Text = "You Do Not Have Sufficient Balance. Can Not Apply 'RoL - Roastering Off Leave (In Hours)'.";
                btnClearAppliedLeave_Click(null, null);
                return;
            }


            #endregion



            bool PunchFound = FetchPunchFound(LoginEmployeeID.ToString(), abc.ToShortDateString(), abc.ToShortDateString());
            string HalfDay = ddlHalfFullDay.SelectedItem.Text.Replace("<--Select-->", "");

            int OldDays = ((TimeSpan)DateTime.Now.Subtract(Convert.ToDateTime(txtFromDate.Text))).Days;


            if (HalfDay == "Half Day" && PunchFound == false && OldDays >= 1)
            {
                Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Punch Not Found For Date : " + txtFromDate.Text + ". Please Update Your Punch Detail In HR For Half Day Leave Apply.");

                hfApplyHalfDayLWP.Value = "ApplyHalfDayLWP";

            }


            #endregion
            if (Error == true)
            {

            }
            else
            {
                DataRow Dr = SelectedLeaves.NewRow();
                Dr["HalfFull"] = ddlHalfFullDay.SelectedItem.Text.Replace("<--Select-->", "");
                Dr["FirstSecond"] = ddlFirstSecond.SelectedItem.Text.Replace("<--Select-->", "");
                Dr["From"] = abc.ToShortDateString().ToString();
                if (ddlHalfFullDay.SelectedItem.Text == "Half Day")
                {
                    txtToDate.Text = txtFromDate.Text;
                }


                if (txtToDate.Text != "")
                    Dr["To"] = abc.ToShortDateString().ToString();
                else
                    Dr["To"] = abc.ToShortDateString().ToString();
                Dr["LeaveType"] = LeaveType;




                SelectedLeaves.Rows.Add(Dr);

                if (hfApplyHalfDayLWP.Value == "ApplyHalfDayLWP")
                {
                    string FirstSecondLWP = "";

                    if (ddlFirstSecond.SelectedItem.Text == "First Half")
                        FirstSecondLWP = "Second Half";
                    else if (ddlFirstSecond.SelectedItem.Text == "Second Half")
                        FirstSecondLWP = "First Half";

                    DataRow DrLWP = SelectedLeaves.NewRow();
                    DrLWP["HalfFull"] = "Half Day";
                    DrLWP["FirstSecond"] = FirstSecondLWP;
                    DrLWP["From"] = abc.ToShortDateString().ToString();
                    if (ddlHalfFullDay.SelectedItem.Text == "Half Day")
                    {
                        txtToDate.Text = txtFromDate.Text;
                    }


                    if (txtToDate.Text != "")
                        DrLWP["To"] = abc.ToShortDateString().ToString();
                    else
                        DrLWP["To"] = abc.ToShortDateString().ToString();
                    DrLWP["LeaveType"] = "LWP - Leave With Out Pay";


                    SelectedLeaves.Rows.Add(DrLWP);
                    hfApplyHalfDayLWP.Value = "";
                }


                gvLeaveDetail.DataSource = SelectedLeaves;
                gvLeaveDetail.DataBind();





                Session["SelectedLeaves"] = SelectedLeaves;


                lblMessage.Text = "";

                btnApply.Visible = true;
                flashingtext.Visible = false;
            }
        }
    }



    private bool FetchPunchFound(string LoginID, string StartDate, string EndDate)
    {
        bool Found = false;
        if (EndDate == "")
            EndDate = StartDate;

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchPunchFound", conStr);
        com.CommandType = CommandType.StoredProcedure;



        com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate;
        com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate;
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;

        try
        {
            conStr.Open();
            DataTable DT = command.ExecuteStoredProcedure(com);


            if (DT.Rows.Count != 0 && DT.Rows[0]["TotalMinutes"].ToString() != "0")
            {
                Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Punch Found For " + DT.Rows[0]["Date"] + " " + DT.Rows[0]["day"] + "");
                Found = true;
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
        return Found;
    }















    private float FetchConsecutiveLeave(string LoginID, string LeaveType)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchConsecutiveLeave", conStr);
        com.CommandType = CommandType.StoredProcedure;
        float ConsecutiveLeave = 0;


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
        com.Parameters.Add("@LeaveName", SqlDbType.VarChar).Value = LeaveType;
        try
        {
            conStr.Open();
            ConsecutiveLeave = float.Parse(com.ExecuteScalar().ToString());
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
        return ConsecutiveLeave;
    }

    private string CheckValidityOfAppliedLeave(DateTime StartDate, DateTime EndDate, int EmployeeID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("CheckValidityOfAppliedLeave", conStr);
        com.CommandType = CommandType.StoredProcedure;
        string ReturnMessageFinal = "";


        com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate;
        com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate;
        com.Parameters.Add("@LeaveType", SqlDbType.Int).Value = ddlLeaveType.SelectedValue;
        com.Parameters.Add("@HalfFull", SqlDbType.Int).Value = Utility.CheckNullValue(ddlHalfFullDay.SelectedValue);
        com.Parameters.Add("@FirstSecond", SqlDbType.Int).Value = Utility.CheckNullValue(ddlFirstSecond.SelectedValue);
        com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeID;


        SqlParameter Output = new SqlParameter("@ReturnMessageFinal", SqlDbType.VarChar, 4000);
        Output.Direction = ParameterDirection.Output;
        com.Parameters.Add(Output);


        try
        {
            conStr.Open();
            com.ExecuteNonQuery();

            ReturnMessageFinal = Output.Value.ToString();
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
        return ReturnMessageFinal;
    }



    private string CheckThreeDaysLeavePostValidation(DateTime Date, int EmployeeID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchLastThreeWorkingDate", conStr);
        com.CommandType = CommandType.StoredProcedure;
        string ReturnMessageFinal = "";


        com.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;
        com.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeID;

        SqlParameter Output = new SqlParameter("@ReturnMessage", SqlDbType.VarChar, 50);
        Output.Direction = ParameterDirection.Output;
        com.Parameters.Add(Output);


        try
        {
            conStr.Open();
            com.ExecuteNonQuery();
            ReturnMessageFinal = Output.Value.ToString();
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
        return ReturnMessageFinal;
    }






    private float FetchLeaveBalanceByLoginID(string LoginID, string LeaveType)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchLeaveBalanceByLoginID", conStr);
        com.CommandType = CommandType.StoredProcedure;
        float Balance = 0;


        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
        com.Parameters.Add("@LeaveName", SqlDbType.VarChar).Value = LeaveType;
        try
        {
            conStr.Open();
            Balance = float.Parse(com.ExecuteScalar().ToString());
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
        return Balance;
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


    protected void btnClearAppliedLeave_Click(object sender, EventArgs e)
    {
        command.ExecuteQuery("EXEC UpdateLockCompOff  " + LoginEmployeeID);
        command.ExecuteQuery("EXEC UpdateLockRoasteringOff  " + LoginEmployeeID);

        SelectedLeaves = (DataTable)Session["SelectedLeaves"];
        SelectedLeaves.Rows.Clear();
        gvLeaveDetail.DataSource = SelectedLeaves;
        gvLeaveDetail.DataBind();
        Session["SelectedLeaves"] = SelectedLeaves;

        btnApply.Visible = false;
        flashingtext.Visible = true;
    }
    protected void chkShowDetail_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowDetail.Checked == true)
            TableShowLeaveBalance.Visible = true;
        else
            TableShowLeaveBalance.Visible = false;
    }
    protected void chkDeletePostLeave_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDeletePostLeave.Checked == true)
            DeletePostedLeave.Visible = true;
        else
            DeletePostedLeave.Visible = false;
    }
    protected void chkShowAllLeaveDetail_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowAllLeaveDetail.Checked == true)
            ShowAllLeaveDetail.Visible = true;
        else
            ShowAllLeaveDetail.Visible = false;
    }
    protected void gvLeaveDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Visible = false;

            if (e.Row.Cells[4].Text.ToString() == "C - Compensatory Leave (In Hours)")
            {
                HtmlAnchor A = new HtmlAnchor();
                A.Attributes.Add("onclick", "return GB_showCenter('   *   OKS Group   *   Compensatory Leave Adjustment', this.href,393," + Session["BrowserWidth"] + ")");
                A.Attributes.Add("href", "~/Employee/CompensatoryLeaveAdjustment.aspx?S=" + e.Row.Cells[2].Text + "&E=" + e.Row.Cells[3].Text + "&T=" + e.Row.Cells[0].Text + "&EmpCode=" + LoginEmployeeID + "");
                A.Attributes.Add("title", "Compensatory Leave Adjustment");
                A.Attributes.Add("onMouseOver", "window.status='http://www.oksgroup.com';return true");
                A.InnerText = "Select";
                e.Row.Cells[5].Controls.Add(A);
            }
            else if (e.Row.Cells[4].Text.ToString() == "RoL - Roastering Off Leave (In Hours)")
            {
                HtmlAnchor A = new HtmlAnchor();
                A.Attributes.Add("onclick", "return GB_showCenter('   *   OKS Group   *   Roastering Off Leave Adjustment', this.href,393," + Session["BrowserWidth"] + ")");
                A.Attributes.Add("href", "~/Employee/RoasteringOffLeaveAdjustment.aspx?S=" + e.Row.Cells[2].Text + "&E=" + e.Row.Cells[3].Text + "&T=" + e.Row.Cells[0].Text + "&EmpCode=" + LoginEmployeeID + "");
                A.Attributes.Add("title", "Compensatory Leave Adjustment");
                A.Attributes.Add("onMouseOver", "window.status='http://www.oksgroup.com';return true");
                A.InnerText = "Select";
                e.Row.Cells[5].Controls.Add(A);
            }


        }
    }
    protected void lkbtnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompensatoryLeaveDetailForEmployee.aspx?EmployeeID=" + LoginEmployeeID);
    }
    protected void rblLeave_SelectedIndexChanged(object sender, EventArgs e)
    {
        AllLeaveStatus();
    }
    protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable DT = command.ExecuteQuery("Select PermanentAddress ,CurrentAddress from UserMasterOtherInfo where UserMasterID =  '" + LoginEmployeeID + "'");

        if (DT.Rows.Count == 1)
        {
            if (ddlAddress.SelectedValue.ToString() == "0")
            {
                txtAddress.Text = "";
            }
            else if (ddlAddress.SelectedValue.ToString() == "1")
            {
                txtAddress.Text = DT.Rows[0]["PermanentAddress"].ToString();
            }
            else if (ddlAddress.SelectedValue.ToString() == "2")
            {
                txtAddress.Text = DT.Rows[0]["CurrentAddress"].ToString();
            }
        }
    }
}