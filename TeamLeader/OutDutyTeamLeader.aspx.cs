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

public partial class TeamLeader_OutDutyTeamLeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            BindGrid();
        }
    }
    public void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();
        com.CommandText = "FetchOutDutyTeamLeader";
        com.Connection = conStr;
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@TLID", SqlDbType.Int).Value = Session["LoginID"];
        try
        {
            conStr.Open();
            DataTable Emp = command.ExecuteStoredProcedure(com);
            repApproval.DataSource = Emp;
            repApproval.DataBind();

            if (Emp.Rows.Count == 0)
            {
                lblMessage.Text = "No Approval Pending";
            }
            else
            {
                lblMessage.Text = "";
            }



            foreach (RepeaterItem gvR in repApproval.Items)
            {
                TimeSetup Out = (TimeSetup)gvR.FindControl("TimeOut");
                TimeSetup In = (TimeSetup)gvR.FindControl("TimeIn");

                HiddenField OutT = (HiddenField)gvR.FindControl("hfTimeOut");
                HiddenField InT = (HiddenField)gvR.FindControl("hfTimeIn");


                Out.SetHourMinuteSecondInDropDown(OutT.Value.ToString());
                In.SetHourMinuteSecondInDropDown(InT.Value.ToString());
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
    protected void gvOutDuty_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void repApproval_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {

            TimeSetup Out = (TimeSetup)e.Item.FindControl("TimeOut");
            TimeSetup In = (TimeSetup)e.Item.FindControl("TimeIn");
            TextBox Comment = (TextBox)e.Item.FindControl("txtComment");
            HiddenField Date = (HiddenField)e.Item.FindControl("hfDate");
            DropDownList Status = (DropDownList)e.Item.FindControl("ddlApproval");

            if (Comment.Text == "")
            {
                lblMessage.Text = "Please Enter Your Comment.";
                Comment.Focus();
                return;
            }

            if (Status.SelectedValue == "0")
            {
                lblMessage.Text = "Please Select Status.";
                Status.Focus();
                return;
            }

            DateTime Start = Convert.ToDateTime(Date.Value + " " + Out.ReturnTime());
            DateTime End = Convert.ToDateTime(Date.Value + " " + In.ReturnTime());
            if (Utility.CompareTime(Out.ReturnTime(), In.ReturnTime()) == false)
            {
                lblMessage.Text = "Please Select Correct Time. Time Is Not In Order.";
                return;
            }




            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand();
            com.CommandText = "InsertOutDutyTeamLeader";
            com.Connection = conStr;
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Clear();



            com.Parameters.Add("@ODIDEmployee", SqlDbType.Int).Value = e.CommandArgument.ToString();
            com.Parameters.Add("@TLComment", SqlDbType.VarChar).Value = Comment.Text;
            com.Parameters.Add("@Status", SqlDbType.Int).Value = Status.SelectedValue;
            com.Parameters.Add("@TimeOut", SqlDbType.DateTime).Value = Convert.ToDateTime(Date.Value + " " + Out.ReturnTime());
            com.Parameters.Add("@TimeIn", SqlDbType.DateTime).Value = Convert.ToDateTime(Date.Value + " " + In.ReturnTime());
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Team Leader Approval");

            try
            {
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
                conStr.Close();
            }

        }
    }

  

}
