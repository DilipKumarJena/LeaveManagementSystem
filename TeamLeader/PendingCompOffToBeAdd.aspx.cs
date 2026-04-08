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

public partial class PendingCompOffToBeAdd : System.Web.UI.Page
{
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand();

        # region Leave Status Of Employee


        com.CommandText = "FetchPendingCompOffToBeAdd";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Clear();
        com.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session["LoginID"];

        try
        {
            conStr.Open();
            DataTable Detail = command.ExecuteStoredProcedure(com);
            gvDetail.DataSource = Detail;
            gvDetail.DataBind();
            Utility.SetGridCss(gvDetail);

            Session["Emp"] = Detail;

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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportInExcel_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, "PendingCompOffToBeAdd.xls");
    }
}