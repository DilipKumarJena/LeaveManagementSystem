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




public partial class RoasteringOffLeaveTL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            //OverTime();
            //ddlOverTimeMain.DataSource = dtOverTime;
            //ddlOverTimeMain.DataTextField = "Time";
            //ddlOverTimeMain.DataValueField = "ID";
            //ddlOverTimeMain.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (CheckValidation() == false)
            return;

        lblMessage.Visible = false;
        lblMessage1.Visible = false;
        if (txtComment.Text == "")
        {
            lblMessage1.Text = "Please Enter Comment";
            lblMessage1.Visible = true;
            return;
        }
        if (Utility.HasSpecialCharacters(txtComment.Text))
        {
            lblMessage1.Text = "Comment Has Special Character. Please Remove It.";
            lblMessage1.Visible = true;
            return;
        }
        try
        {
            StringBuilder XML = new StringBuilder();
            XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            XML.Append("<RoasteringOff>");

            foreach (GridViewRow gvR in gvEmployeeList.Rows)
            {
                DropDownList ddlOverTime = (DropDownList)gvR.FindControl("ddlOverTime");
                DropDownList ddlDepartment = (DropDownList)gvR.FindControl("ddlDepartment");

                HiddenField ID = (HiddenField)gvR.FindControl("hfID");


                if (ddlOverTime.SelectedItem.Text != "0" && ddlOverTime.Enabled == true)
                {
                    XML.Append("<Leave EmployeeID=\"" + ID.Value.ToString() + "\" WorkedForDepartment=\"" + ddlDepartment.SelectedValue.ToString() + "\" TLID=\"" + Session["LoginID"].ToString() + "\" TLComment=\"" + txtComment.Text + "\" Date=\"" + Convert.ToDateTime(txtDate.Text) + "\" Duration=\"" + ddlOverTime.SelectedItem.Text + "\" />");
                }
            }
            XML.Append("</RoasteringOff>");


            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("[InsertRoasteringOffLeave]", conStr);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();


            try
            {
                if (conStr.State != ConnectionState.Open)
                    conStr.Open();
                com.ExecuteNonQuery();
                lblMessage1.Visible = true;
                lblMessage1.Text = "Record Updated.";
                btnShowEmployee_Click(null, null);

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




    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        //ddlOverTimeMain.SelectedIndex = 0;
        Utility.SetGridCss(gvEmployeeList);
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("FetchEmployeeListFromLoginIDForRoasteringOffLeave", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            OverTime();
            Department();
            conStr.Open();

            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
            Com.Parameters.Add("@Date", SqlDbType.VarChar).Value = txtDate.Text;

            SqlParameter Output = new SqlParameter("@ReturnValue", SqlDbType.VarChar, 100);
            Output.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Output);

            DataTable DT = command.ExecuteStoredProcedure(Com);
            gvEmployeeList.DataSource = DT;
            gvEmployeeList.DataBind();
            lblMessage.Visible = true;
            lblMessage.Text = Output.Value.ToString();
            if (DT.Rows.Count > 0)
            {
                divUpdate.Visible = true;
                lblMessage.Text = "";
                btnSubmit.Visible = true;
                ddlOverTimeMain.Visible = true;
                txtComment.Visible = true;
            }
            else
            {
                divUpdate.Visible = false;
                lblMessage.Text = "No Punch Detail Found.";
                btnSubmit.Visible = false;
                ddlOverTimeMain.Visible = false;
                txtComment.Visible = false;
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

    DataTable dtOverTime = new DataTable();
    DataTable dtDepartment = new DataTable();

    protected void Department()
    {
        try
        {
            dtDepartment = command.ExecuteQuery("EXEC FetchDepartment");
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

    protected void OverTime()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchOverTimeRoasteringOff]", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
            Com.Parameters.Add("@Date", SqlDbType.VarChar).Value = txtDate.Text;
            dtOverTime = command.ExecuteStoredProcedure(Com);


            ddlOverTimeMain.DataSource = dtOverTime;
            ddlOverTimeMain.DataTextField = "Time";
            ddlOverTimeMain.DataValueField = "ID";
            ddlOverTimeMain.DataBind();
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


    protected void ddlOverTimeMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvEmployeeList.Rows.Count == 0)
        {
            lblMessage.Text = "Please Select Date And Click On Show Employee.";
            return;
        }


        foreach (GridViewRow gvR in gvEmployeeList.Rows)
        {
            DropDownList OverTime = (DropDownList)gvR.FindControl("ddlOverTime");

            if (OverTime.Enabled == true && OverTime.SelectedItem.Text == "0")
                OverTime.SelectedItem.Text = ddlOverTimeMain.SelectedItem.Text;
        }
    }


    private bool CheckValidation()
    {
        bool IsOK = true;
        foreach (GridViewRow gvR in gvEmployeeList.Rows)
        {
            string Day = gvR.Cells[2].Text;
            DropDownList ddlOverTime = (DropDownList)gvR.FindControl("ddlOverTime");
            HiddenField WorkingMinutes = (HiddenField)gvR.FindControl("hfWorkingMinutes");
            HiddenField TotalWorkingMinute = (HiddenField)gvR.FindControl("hfTotalWorkingMinute");
            Label Name = (Label)gvR.FindControl("Label1");


            if (ddlOverTime.SelectedValue.ToString() != "0")
            {
                if (Day.Contains("Saturday") || Day.Contains("Sunday"))
                { }
                else
                {
                    if (gvR.Cells[3].Text == "No Holiday")
                    {
                        int AppliedCompOffInMinutes = Convert.ToInt32(ddlOverTime.SelectedValue) * 60;
                        if ((AppliedCompOffInMinutes + Convert.ToInt32(WorkingMinutes.Value)) - 15 > Convert.ToInt32(TotalWorkingMinute.Value))
                        {
                            Utility.AlertOnAjaxPage(this, "UpdatePanel1", "Please Check Applied CompOff Detail Of " + Name.Text + ".");
                            IsOK = false;
                            break;
                        }
                    }
                }
            }
        }
        return IsOK;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        CheckValidation();
    }

    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlOverTime = (DropDownList)e.Row.FindControl("ddlOverTime");
            ddlOverTime.DataSource = dtOverTime;
            ddlOverTime.DataTextField = "Time";
            ddlOverTime.DataValueField = "ID";
            ddlOverTime.DataBind();
            HiddenField Duration = (HiddenField)e.Row.FindControl("hfDuration");
            DropDownList Department = (DropDownList)e.Row.FindControl("ddlDepartment");

            ddlOverTime.SelectedItem.Text = Duration.Value.ToString();
            if (Duration.Value != "0")
            {
                ddlOverTime.Enabled = false;
                Department.Enabled = false;
            }


            DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");
            ddlDepartment.DataSource = dtDepartment;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "ID";
            ddlDepartment.DataBind();
            HiddenField hfDepartmentID = (HiddenField)e.Row.FindControl("hfDepartmentID");
            ddlDepartment.SelectedValue = hfDepartmentID.Value;
        }
    }
    protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "")
        {
            //DropDownList ddlOverTime = (DropDownList)e.FindControl("ddlOverTime");
            //HiddenField WorkingMinutes = (HiddenField)gvR.FindControl("hfWorkingMinutes");
            //HiddenField TotalWorkingMinute = (HiddenField)gvR.FindControl("hfTotalWorkingMinute");
            //Label Name = (Label)gvR.FindControl("Label1");
        }
    }
    protected void ddlOverTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        // get reference to the row
        GridViewRow gvR = (GridViewRow)(((Control)sender).NamingContainer);

        DropDownList ddlOverTime = (DropDownList)gvR.FindControl("ddlOverTime");
        HiddenField WorkingMinutes = (HiddenField)gvR.FindControl("hfWorkingMinutes");
        HiddenField TotalWorkingMinute = (HiddenField)gvR.FindControl("hfTotalWorkingMinute");
        Label Name = (Label)gvR.FindControl("Label1");

        //string DateIn



    }


}