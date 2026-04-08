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

public partial class CompensatoryLeaveAdjustment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            lblStart.Text = Request.QueryString["S"].ToString();
            lblEnd.Text = Request.QueryString["E"].ToString();
            lblType.Text = Request.QueryString["T"].ToString();

            int Days = ((TimeSpan)Convert.ToDateTime(lblEnd.Text).Subtract(Convert.ToDateTime(lblStart.Text))).Days + 1;

            if (lblType.Text == "Full Day")
                lblRequestedHour.Text = (Days * 8).ToString();
            else
                lblRequestedHour.Text = (Days * 4).ToString();




            DataTable DT = command.ExecuteQuery("EXEC FetchRemainingCompOffBalance '" + Request.QueryString["EmpCode"].ToString() + "','" + Request.QueryString["E"].ToString() + "'");
            gvCompOffAdjustment.DataSource = DT;
            gvCompOffAdjustment.DataBind();
            Utility.SetGridCss(gvCompOffAdjustment);


            if (DT.Rows.Count == 0)
                btnValidate.Visible = false;
            else
                btnValidate.Visible = true;
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        string IDs = "";

        string IDsWithHours = "";

        int TotalSelectedHours = 0;
        foreach (GridViewRow gvR in gvCompOffAdjustment.Rows)
        {
            CheckBox ch = (CheckBox)gvR.FindControl("chkHours");
            HiddenField hf = (HiddenField)gvR.FindControl("hfID");
            TextBox T = (TextBox)gvR.FindControl("txtHours");
            if (ch.Checked)
            {
                if (Convert.ToInt32(T.Text) > Convert.ToInt32(gvR.Cells[3].Text.ToString()))
                {
                    lblMessage.Text = "Requested Hour Can Not Greater Than Remaining Balance.";
                    return;
                }
                else
                {
                    IDs += hf.Value + ",";
                    IDsWithHours += hf.Value + "~" + T.Text + ",";
                    TotalSelectedHours += Convert.ToInt16(T.Text);
                }
            }
        }

        if (TotalSelectedHours == Convert.ToUInt32(lblRequestedHour.Text))
        {
            string SessionName = lblStart.Text + "~" + lblEnd.Text + "~" + lblType.Text;
            command.ExecuteQuery("UpdateLocakCompOffRows '" + IDs + "'");
            Session[SessionName] = IDsWithHours;

            lblMessage.Text = "Comp Off Adjustment Validated";
        }
        else
        {
            lblMessage.Text = "Invalid Comp Off Adjustment. Please Check Requested Hour And Selected Hour.";
        }
    }
    protected void gvCompOffAdjustment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox T = (TextBox)e.Row.FindControl("txtHours");
            T.Text = e.Row.Cells[3].Text;
        }
    }
}
