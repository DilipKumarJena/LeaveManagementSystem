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

public partial class Accounts_ShowTaxDeclaration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            btnExport.Visible = false;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid(ddlFinancialYear.SelectedValue);
    }

    private void BindGrid(string FinancialYear)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchTaxDeclarationReport]", conStr);
        Com.Parameters.Add("@FinancialYear ", SqlDbType.Int).Value = FinancialYear;

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            DataTable DT = command.ExecuteStoredProcedure(Com);
            divReport.DataSource = DT;
            divReport.DataBind();
            Utility.SetGridCssSecond(divReport);

            Session["Emp"] = DT;

            if (DT.Rows.Count == 0)
                btnExport.Visible = false;
            else
                btnExport.Visible = true;
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, "TaxDeclaration-" + ddlFinancialYear.SelectedItem.Text + ".xls");
      
    }
}
