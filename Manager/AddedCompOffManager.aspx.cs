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

public partial class AddedCompOffManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

       
        if (Convert.ToDateTime(txtEndDate.Text) >= Convert.ToDateTime(Utility.GetServerDate()))
        {
            Label1.Text = "End Date Can Not Greater Than Current Date";
            return;
        }




        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchAddedCompensatoryLeaveForManager]", conStr);
        com.CommandType = CommandType.StoredProcedure;






        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = txtStartDate.Text;
        com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = txtEndDate.Text;

        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);


            if (Emp.Rows.Count > 0)
            {              

                Session["Emp"] = Emp;

                divReport.DataSource = Emp;
                divReport.DataBind();
                Utility.SetGridCss(divReport);
                Label1.Text = "Record Loaded.";
            }
            else
            {
                Label1.Text = "No Record Found.";

            }


        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }




    protected void btnExportToEXCEL_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, txtStartDate.Text + " To " + txtEndDate.Text + "_" + "AddedCompOffs.xls");
        Label1.Text = "Record Exported";
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }   
}
