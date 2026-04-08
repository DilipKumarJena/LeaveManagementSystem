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

public partial class CompensatoryLeaveAccountsApproval : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

            NewMethod();
        }
    }

    private void NewMethod()
    {
        SqlConnection conStr = connection.CreateConneciton();

        //FetchPendingCompensatoryLeaveHR
        SqlCommand Com = new SqlCommand("FetchPendingCompensatoryLeaveAccounts", conStr);
        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            DataSet DS = command.ExecuteStoredProcedureReturnDataset(Com);

            lblCompensatoryLeave.Text = DS.Tables[0].Rows[0][0].ToString();

            gvCompOff.DataSource = DS.Tables[1];
            gvCompOff.DataBind();

            Utility.SetGridCss(gvCompOff);
        }
        catch (Exception ee)
        {

        }
        finally
        {
            conStr.Close();
        }
    }
}