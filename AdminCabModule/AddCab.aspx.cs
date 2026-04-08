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

public partial class AdminCabModule_AddCab : System.Web.UI.Page
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
        DataTable DT = command.ExecuteQuery("EXEC FetchCabMaster");
        gvCabMaster.DataSource = DT;
        gvCabMaster.DataBind();
        Utility.SetGridCss(gvCabMaster);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtCabNumber.Text == "")
        {
            lblCabNumber.Text = "Please Enter Cab Number";
            return;
        }


        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[InsertUpdateCabMaster]", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@VehicleNumber", SqlDbType.VarChar, 25).Value = txtCabNumber.Text;
            Com.Parameters.Add("@DriverName", SqlDbType.VarChar, 100).Value = txtDriverName.Text;
            Com.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 25).Value = txtMobile.Text;
            Com.Parameters.Add("@Route", SqlDbType.VarChar).Value = txtRoute.Text;
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref Com, "Insert Cab Master");

            Com.ExecuteNonQuery();
            spanMessage.InnerHtml = "Record Inserted";
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
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }
    protected void gvCabMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCabMaster.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gvCabMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCabMaster.EditIndex = -1;
        BindGrid();

    }
    protected void gvCabMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        Label lblVehicleNumber = (Label)gvCabMaster.Rows[e.RowIndex].FindControl("lblVehicleNumber");
        TextBox txtgDriverName = (TextBox)gvCabMaster.Rows[e.RowIndex].FindControl("txtgDriverName");
        TextBox txtgMobileNumber = (TextBox)gvCabMaster.Rows[e.RowIndex].FindControl("txtgMobileNumber");
        TextBox txtgRoute = (TextBox)gvCabMaster.Rows[e.RowIndex].FindControl("txtgRoute");







        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("InsertUpdateCabMaster", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@VehicleNumber", SqlDbType.VarChar, 25).Value = lblVehicleNumber.Text;
            Com.Parameters.Add("@DriverName", SqlDbType.VarChar, 100).Value = txtgDriverName.Text;
            Com.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 25).Value = txtgMobileNumber.Text;
            Com.Parameters.Add("@Route", SqlDbType.VarChar).Value = txtgRoute.Text;
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref Com, "Update Cab Master");
            Com.ExecuteNonQuery();

            gvCabMaster.EditIndex = -1;
            BindGrid();
            spanMessage.InnerHtml = "Record Updated";
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
    protected void gvCabMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblVehicleNumber = (Label)gvCabMaster.Rows[e.RowIndex].FindControl("lblVehicleNumber");


        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("DeleteCabMaster", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@VehicleNumber", SqlDbType.VarChar, 25).Value = lblVehicleNumber.Text;
            ExtraParameterForSecurity.AddExtraParameterForSecurity(ref Com, "Delete Cab Master");

            Com.ExecuteNonQuery();

            gvCabMaster.EditIndex = -1;
            BindGrid();
            spanMessage.InnerHtml = "Record Deleted";
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
