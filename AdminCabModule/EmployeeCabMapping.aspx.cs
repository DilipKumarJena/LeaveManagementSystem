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
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;

public partial class EmployeeCabMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            DataTable VehicleNumber = command.ExecuteQuery("Select 0 ID,'<-- Select -->'VehicleNumber  union Select ID , VehicleNumber  from [CabMaster] where IsDel=0");
            ddlVehicleNo.DataTextField = "VehicleNumber";
            ddlVehicleNo.DataValueField = "ID";
            ddlVehicleNo.DataSource = VehicleNumber;
            ddlVehicleNo.DataBind();




            DataTable DT = command.ExecuteQuery("Select 0 ID,'<-- Select -->'Location union Select ID,Location+'-'+City from Location Where IsDel=0");
            ddlLocation.DataTextField = "Location";
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataSource = DT;
            ddlLocation.DataBind();
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = ddlLocation.SelectedValue;

        DataTable DT = command.ExecuteQuery("Select 0 ID,'<-- Select -->'Location union Select ID,DepartmentName from Department where LocationID='" + id + "' And IsDel=0 ");

        ddlDepartment.DataTextField = "Location";
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataSource = DT;
        ddlDepartment.DataBind();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Designation = command.ExecuteQuery("Select 0 ID,'<-- Select -->' DesignationName Union Select ID,DesignationName+'-'+Abbr from Designation where DepartmentID='" + ddlDepartment.SelectedValue + "' And IsDel=0 ");

        try
        {
            ddlDesignation.DataTextField = "DesignationName";
            ddlDesignation.DataValueField = "ID";
            ddlDesignation.DataSource = Designation;
            ddlDesignation.DataBind();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchEmployeeList", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@LocationID", SqlDbType.Int).Value = Utility.CheckNullValue(ddlLocation.SelectedValue);
        com.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = Utility.CheckNullValue(ddlDepartment.SelectedValue);
        com.Parameters.Add("@DesignationID ", SqlDbType.Int).Value = Utility.CheckNullValue(ddlDesignation.SelectedValue);
        com.Parameters.Add("@EmployeeID ", SqlDbType.Int).Value = Utility.CheckNullValue(ddlEmployee.SelectedValue);
        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);


            gvLeaveDetail.DataSource = Emp;
            gvLeaveDetail.DataBind();
            Utility.SetGridCss(gvLeaveDetail);

            btnAddSelectedEmployee.Visible = true;
            Session["Emp"] = Emp;
            Error.InnerHtml = "";
            if (Emp.Rows.Count == 0)
            {
                Error.InnerHtml = "No Employee Found For Current Selection.";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Employee = command.ExecuteQuery("Select 0 ID,'<-- Select -->' EmployeeName Union Select ID,Name +'-'+EmpCode from UserMaster where DesignationID='" + ddlDesignation.SelectedValue + "' And IsDel=0");

        try
        {
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "ID";
            ddlEmployee.DataSource = Employee;
            ddlEmployee.DataBind();
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

    protected void btnExportToEXCEL_Click(object sender, EventArgs e)
    {
        string EmpID = "";
        foreach (GridViewRow gvR in gvLeaveDetail.Rows)
        {
            CheckBox CB = ((CheckBox)gvR.FindControl("chkAdd"));
            if (CB.Checked == true)
            {
                HiddenField hfID = ((HiddenField)gvR.FindControl("hfID"));
                EmpID += hfID.Value + ",";
            }
        }
        if (EmpID == "")
        {
            Error.InnerHtml = "please Select Employee From List.";
            return;
        }


        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("InsertUpdateCabEmployeeMapping", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@CabID", SqlDbType.Int).Value = ddlVehicleNo.SelectedValue;
        com.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = EmpID;
        ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "IU");
        try
        {
            if (conStr.State != ConnectionState.Open)
                conStr.Open();

            com.ExecuteNonQuery();
            Error.InnerHtml = "Record Inserted.";

            ddlVehicleNo_SelectedIndexChanged(null, null);
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void ddlVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("FetchEmployeeListCabMapping_Mapping", conStr);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@CabID", SqlDbType.Int).Value = ddlVehicleNo.SelectedValue;

        try
        {
            DataTable Cab = command.ExecuteStoredProcedure(com);
            gvCabMapping.DataSource = Cab;
            gvCabMapping.DataBind();
            Utility.SetGridCss(gvCabMapping);

            if (Cab.Rows.Count == 0)
            {
                Error.InnerHtml = "No Employee Found For This Cab.";
            }
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }
    protected void gvCabMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveEmployee")
        {
            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("InsertUpdateCabEmployeeMapping", conStr);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@CabID", SqlDbType.Int).Value = ddlVehicleNo.SelectedValue;
            com.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = e.CommandArgument;
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "D");
            try
            {
                if (conStr.State != ConnectionState.Open)
                    conStr.Open();

                com.ExecuteNonQuery();
                Error.InnerHtml = "Employee Removed.";
                ddlVehicleNo_SelectedIndexChanged(null, null);
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
}