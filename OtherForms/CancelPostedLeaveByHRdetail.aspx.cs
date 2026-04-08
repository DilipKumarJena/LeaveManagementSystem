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

public partial class CancelPostedLeaveByHRdetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.InvalidUserRedirectToLoginPage();
        if (!(IsPostBack))
            FillDetails(Request.QueryString["ID"]);
    }
    protected void FillDetails(string LeaveID)
    {
        FetchLeaveBalanceByLeavePostID(LeaveID);
        FetchLeaveDetailByLeavePostID(LeaveID);
        FetchAllLeaveDetailByLeavePostID(LeaveID);
    }
    private void FetchLeaveBalanceByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();



        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveBalanceByLeavePostID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            LeaveBalance.InnerHtml = Utility.CreateHTMLTable(EmpLeaveDetail, "LeaveStatus");
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
        # endregion
    }
    private void FetchLeaveDetailByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();



        # region Leave Status Of Employee


        com.CommandText = "FetchLeaveDetailByLeavePostID";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveID;

        try
        {
            conStr.Open();
            DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);
            LeaveDetail.InnerHtml = Utility.CreateHTMLTable(EmpLeaveDetail, "LeaveDetail");
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
        # endregion
    }
    public void FetchAllLeaveDetailByLeavePostID(string LeaveID)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchAllLeaveDetailByLeavePostID";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LeavePostID", SqlDbType.Int).Value = LeaveID;
        try
        {
            conStr.Open();
            spanShowAllLeaveDetail.InnerHtml = Utility.CreateHTMLTable(command.ExecuteStoredProcedure(com), "FetchLeaveDetailByEmployeeID");
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        int Exist = Convert.ToInt32(command.ExecuteScalar("Select Count(*) from LeavePost Where ID='" + Request.QueryString["ID"].ToString() + "' And CancelStatus=0"));
        if (Exist == 1)
        {

            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand();

            com.CommandText = "FetchLeaveDetailByLeavePostIDForHRCancel";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Clear();
            com.Parameters.Add("@ID", SqlDbType.Int).Value = Request.QueryString["ID"].ToString();

            try
            {
                conStr.Open();
                DataTable EmpLeaveDetail = command.ExecuteStoredProcedure(com);

                foreach (DataRow Dr in EmpLeaveDetail.Rows)
                {
                    string EmployeeID = Dr["EmployeeID"].ToString();
                    string FullHalf = Dr["FullHalf"].ToString();
                    string LeaveType = Dr["LeaveType"].ToString();
                    string CompOffIDs = Dr["CompOffIDs"].ToString();
                    DateTime From = Convert.ToDateTime(Dr["From"].ToString());
                    DateTime To = Convert.ToDateTime(Dr["To"].ToString());

                    float Days = ((TimeSpan)To.Subtract(From)).Days;
                    Days++;
                    string Query = "";
                    string Query1 = "";
                    //Update Status Of LeavePost
                    Query = "Update LeavePost Set CancelOnHR = GetDate(),CancelByHR='" + Session["LoginID"] + "',CancelStatus=1,CancelRemark='" + txtReasonOfCancel.Text + "' Where ID='" + Request.QueryString["ID"].ToString() + "'";
                    command.ExecuteNonQuery(Query);

                    //Update Leave Balance
                    if (FullHalf == "1" && LeaveType != "4" && LeaveType != "11")
                    {
                        Query = "Update LeaveBalance Set Balance = Balance + " + Days + ",AddedBy = '" + Session["LoginID"] + "',AddedOn=GetDate(),IP='" + ExtraParameterForSecurity.GetIPAddress() + "',Type = 'Balance Updated Due To Leave Cancel' where EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    }
                    if (FullHalf == "2" && LeaveType != "4" && LeaveType != "11")                    
                    {
                        Query = "Update LeaveBalance Set Balance = Balance + " + Days / 2 + ",AddedBy = '" + Session["LoginID"] + "',AddedOn=GetDate(),IP='" + ExtraParameterForSecurity.GetIPAddress() + "',Type = 'Balance Updated Due To Leave Cancel' where EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    }
                    if (FullHalf == "1" && LeaveType == "4")
                    {
                        Query = "Update LeaveBalance Set Balance = Balance + " + (Days * 8) + ",AddedBy = '" + Session["LoginID"] + "',AddedOn=GetDate(),IP='" + ExtraParameterForSecurity.GetIPAddress() + "',Type = 'Balance Updated Due To Leave Cancel' where EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    }
                    if (FullHalf == "2" && LeaveType == "4")
                    {
                        Query = "Update LeaveBalance Set Balance = Balance + " + (Days * 4) + ",AddedBy = '" + Session["LoginID"] + "',AddedOn=GetDate(),IP='" + ExtraParameterForSecurity.GetIPAddress() + "',Type = 'Balance Updated Due To Leave Cancel' where EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    }
                    if (FullHalf == "1" && LeaveType == "11")
                    {
                        Query = "Update LeaveBalance Set Balance = Balance + " + (Days * 8) + ",AddedBy = '" + Session["LoginID"] + "',AddedOn=GetDate(),IP='" + ExtraParameterForSecurity.GetIPAddress() + "',Type = 'Balance Updated Due To Leave Cancel' where EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    }
                    if (FullHalf == "2" && LeaveType == "11")
                    {
                        Query = "Update LeaveBalance Set Balance = Balance + " + (Days * 4) + ",AddedBy = '" + Session["LoginID"] + "',AddedOn=GetDate(),IP='" + ExtraParameterForSecurity.GetIPAddress() + "',Type = 'Balance Updated Due To Leave Cancel' where EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    }

                    command.ExecuteNonQuery(Query);
                    if (LeaveType == "4")
                    {
                        Query1 = "";
                        Query1 += " UPDATE C ";
                        Query1 += " SET    RemainingBalance = RemainingBalance + X.UsedHour ";
                        Query1 += " FROM   CompensatoryLeave C ";
                        Query1 += " INNER JOIN (SELECT [GOLD].Split(Items, '~', 1) ID, ";
                        Query1 += " [GOLD].Split(Items, '~', 2) UsedHour ";
                        Query1 += " FROM   Gold.Splitstringintable('" + CompOffIDs + "', ','))X ";
                        Query1 += " ON C.ID = X.ID ";

                        Query1 += " UPDATE C ";
                        Query1 += " SET    Marked = CASE ";
                        Query1 += " WHEN RemainingBalance = 0 THEN 3 ";
                        Query1 += " ELSE 1 ";
                        Query1 += " END ";
                        Query1 += " FROM   CompensatoryLeave C ";
                        Query1 += " INNER JOIN (SELECT [GOLD].Split(Items, '~', 1) ID, ";
                        Query1 += " [GOLD].Split(Items, '~', 2) UsedHour ";
                        Query1 += " FROM   Gold.Splitstringintable('" + CompOffIDs + "', ','))X ";
                        Query1 += " ON C.ID = X.ID ";

                        command.ExecuteNonQuery(Query1);
                    }


                    if (LeaveType == "11")
                    {
                        Query1 = "";
                        Query1 += " UPDATE C ";
                        Query1 += " SET    RemainingBalance = RemainingBalance + X.UsedHour ";
                        Query1 += " FROM   RoasteringOffLeave C ";
                        Query1 += " INNER JOIN (SELECT [GOLD].Split(Items, '~', 1) ID, ";
                        Query1 += " [GOLD].Split(Items, '~', 2) UsedHour ";
                        Query1 += " FROM   Gold.Splitstringintable('" + CompOffIDs + "', ','))X ";
                        Query1 += " ON C.ID = X.ID ";

                        Query1 += " UPDATE C ";
                        Query1 += " SET    Marked = CASE ";
                        Query1 += " WHEN RemainingBalance = 0 THEN 3 ";
                        Query1 += " ELSE 1 ";
                        Query1 += " END ";
                        Query1 += " FROM   RoasteringOffLeave C ";
                        Query1 += " INNER JOIN (SELECT [GOLD].Split(Items, '~', 1) ID, ";
                        Query1 += " [GOLD].Split(Items, '~', 2) UsedHour ";
                        Query1 += " FROM   Gold.Splitstringintable('" + CompOffIDs + "', ','))X ";
                        Query1 += " ON C.ID = X.ID ";

                        command.ExecuteNonQuery(Query1);

                    }




                    Query = "INSERT INTO LeaveBalanceLog SELECT * FROM LeaveBalance WHERE EmployeeID = '" + EmployeeID + "' And LeaveTypeID = '" + LeaveType + "'";
                    command.ExecuteNonQuery(Query);



                    TimeSpan aa = To - From;
                    int kk = aa.Days;

                    for (int i = 0; i <= kk; i++)
                    {
                        DateTime abc = From.AddDays(i);

                        int Day = abc.Day;
                        int Month = abc.Month;
                        int Year = abc.Year;


                        DataTable DDT = command.ExecuteQuery("Select * from PunchCardDetail where EmployeeID='" + EmployeeID + "' And Date = '" + abc + "'");

                        if (DDT.Rows.Count == 1)
                        {
                            com = new SqlCommand("UpdatePunchCardDetail", conStr);
                            com.CommandType = CommandType.StoredProcedure;
                            com.Parameters.Clear();
                            com.Parameters.Add("@ID", SqlDbType.Int).Value = DDT.Rows[0]["ID"].ToString();
                            com.Parameters.Add("@DateIn", SqlDbType.VarChar).Value = Utility.CheckNullValue(DDT.Rows[0]["DateIn"].ToString());
                            com.Parameters.Add("@DateOut", SqlDbType.VarChar).Value = Utility.CheckNullValue(DDT.Rows[0]["DateOut"].ToString());
                            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Update UpdatePunchCardDetail For MasterRoll After CancelPostedByLeaveHR ");
                            try
                            {
                                command.ExecuteStoredProcedure(com);
                            }
                            catch (Exception ee)
                            {
                                Session["Ex"] = ee.Message.ToString();
                                ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
                            }
                            finally { }

                        }
                    }
                }

                lblMessage.Text = "Leave Canceled";
            }
            catch (Exception ee)
            {
                lblMessage.Text = ee.Message.ToString();

                Session["Ex"] = ee.Message.ToString();
                ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            }
            finally
            {
                conStr.Close();
            }
        }
        else
        {
            lblMessage.Text = "Leave Already Cancelled.";
        }
    }
}
