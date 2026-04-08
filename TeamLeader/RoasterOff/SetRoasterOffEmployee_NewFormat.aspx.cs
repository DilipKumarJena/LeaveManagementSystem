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
using System.Drawing;

public partial class SetRoasterOffEmployee_NewFormat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Label1.Text = "";
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("[FetchEmployeeListForRoasterOffTeamLeader_NewFormat]", conStr);
        com.CommandType = CommandType.StoredProcedure;


        com.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = Session["LoginID"].ToString();
        com.Parameters.Add("@Month", SqlDbType.Int).Value = MonthYear1.GetMonth();
        com.Parameters.Add("@Year", SqlDbType.Int).Value = MonthYear1.GetYear();


        try
        {
            DataTable Emp = command.ExecuteStoredProcedure(com);

            Session["Emp"] = Emp;



            divDetail.DataSource = Emp;
            divDetail.DataBind();
            //Utility.SetGridCssSecond(divDetail);
            Label1.Text = "Record Loaded.";


            //hfDataForFreeze.Value = DataForFreeze(Emp);

            //divDetail.HeaderRow.Height = 100;

            aa.Style.Add("width", Convert.ToInt32(Session["BrowserWidth"]) - 80 + "px");
            //divDetail.Width = Unit.Pixel(Convert.ToInt32(Session["BrowserWidth"]) - 80);







        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { }
    }

    private string DataForFreeze(DataTable Emp)
    {
        StringBuilder sb = new StringBuilder();
        foreach (DataRow Dr in Emp.Rows)
        {
            for (int i = 0; i < Dr.ItemArray.Length; i++)
            {


                sb.Append(Dr[i].ToString() + "|");
            }
            sb.Append("*");
        }

        return sb.ToString();
    }







    protected void divDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable DT = (DataTable)Session["Emp"];
            string MondayDate = "";
            bool RB_Checked = false;

            for (int i = 4; i < e.Row.Cells.Count; i++)
            {
                string ID = e.Row.Cells[0].Text.ToString();
                RadioButton RB = new RadioButton();
                string ColumnName = DT.Columns[i].ColumnName;
                RB.Checked = Convert.ToBoolean((e.Row.Cells[i].Text == "1" ? true : false));

                if (ID == "772" && ColumnName == "14_Apr_2014_Mon")
                {

                }

                if (RB.Checked)
                {
                    RB_Checked = true;
                }
                if (ColumnName.Substring(ColumnName.Length - 3, 3) == "Mon")
                {
                    MondayDate = ColumnName.Replace("_Mon", "");
                }
                RB.ID = ID + "_" + ColumnName + ":" + MondayDate;
                if (ColumnName.Substring(ColumnName.Length - 3, 3) == "Sat")
                {
                    if (RB_Checked == false)
                    {
                        RB.Checked = true;
                    }
                }
                if (ColumnName.Substring(ColumnName.Length - 3, 3) == "Sun")
                {
                    MondayDate = ColumnName.Replace("_Sun", "");
                    RB.Checked = true;

                }


                RB.GroupName = MondayDate;

                RB.ToolTip = e.Row.Cells[2].Text + "       :       " + DT.Columns[i].ColumnName;
                e.Row.Cells[i].Controls.Add(RB);


                if (Convert.ToInt32(e.Row.Cells[3].Text.ToString()) == 1)
                {
                    e.Row.Cells[0].BackColor = Color.Aqua;
                    e.Row.Cells[1].BackColor = Color.Aqua;
                    e.Row.Cells[2].BackColor = Color.Aqua;
                    e.Row.Cells[3].BackColor = Color.Aqua;
                    e.Row.Cells[i].BackColor = Color.Aqua;

                }
                else if (Convert.ToInt32(e.Row.Cells[3].Text.ToString()) == 2)
                {
                    e.Row.Cells[0].BackColor = Color.Gold;
                    e.Row.Cells[1].BackColor = Color.Gold;
                    e.Row.Cells[2].BackColor = Color.Gold;
                    e.Row.Cells[3].BackColor = Color.Gold;
                    e.Row.Cells[i].BackColor = Color.Gold;
                }
                else if (Convert.ToInt32(e.Row.Cells[3].Text.ToString()) == 3)
                {
                    e.Row.Cells[0].BackColor = Color.Silver;
                    e.Row.Cells[1].BackColor = Color.Silver;
                    e.Row.Cells[2].BackColor = Color.Silver;
                    e.Row.Cells[3].BackColor = Color.Silver;
                    e.Row.Cells[i].BackColor = Color.Silver;
                }
                else if (Convert.ToInt32(e.Row.Cells[3].Text.ToString()) == 4)
                {
                    e.Row.Cells[0].BackColor = Color.Wheat;
                    e.Row.Cells[1].BackColor = Color.Wheat;
                    e.Row.Cells[2].BackColor = Color.Wheat;
                    e.Row.Cells[3].BackColor = Color.Wheat;
                    e.Row.Cells[i].BackColor = Color.Wheat;
                }
                else if (Convert.ToInt32(e.Row.Cells[3].Text.ToString()) == 5)
                {
                    e.Row.Cells[0].BackColor = Color.Beige;
                    e.Row.Cells[1].BackColor = Color.Beige;
                    e.Row.Cells[2].BackColor = Color.Beige;
                    e.Row.Cells[3].BackColor = Color.Beige;
                    e.Row.Cells[i].BackColor = Color.Beige;
                }
                else if (Convert.ToInt32(e.Row.Cells[3].Text.ToString()) == 6)
                {
                    e.Row.Cells[0].BackColor = Color.DeepPink;
                    e.Row.Cells[1].BackColor = Color.DeepPink;
                    e.Row.Cells[2].BackColor = Color.DeepPink;
                    e.Row.Cells[3].BackColor = Color.DeepPink;
                    e.Row.Cells[i].BackColor = Color.DeepPink;
                }
            }


        }
    }


    protected void btnInsertUpdate_Click(object sender, EventArgs e)
    {
        string[] FinalString = hfFinalString.Value.ToString().Split('@');
        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<Roaster>");

        for (int i = 0; i < FinalString.Length; i++)
        {
            string[] Detail = FinalString[i].Split('*');
            if (Detail.Length > 1)
                XML.Append("<Detail EmpID=\"" + Detail[0] + "\" RoasterDay=\"" + Detail[1] + "\" Monday=\"" + Detail[2] + "\" />");
        }
        XML.Append("</Roaster>");

        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand com = new SqlCommand("InsertRoasterOffEmployees", conStr);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();
        ExtraParameterForSecurity.AddExtraParameterForSecurity(ref com, "Insert");
        try
        {
            if (conStr.State != ConnectionState.Open)
                conStr.Open();
            com.ExecuteNonQuery();
            Label1.Text = "Inserted / Updated.";
            btnSubmit_Click(null, null);
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {

            if (conStr.State != ConnectionState.Closed)
                conStr.Close();
        }
    }




    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DataTable DT = (DataTable)Session["Emp"];
        if (DT.Columns.Contains("ID"))
            DT.Columns.Remove("ID");

        ExportToEXCEL.ExportDtToEXCEL(DT, MonthYear1.GetMonth() + "_" + MonthYear1.GetYear() + "_" + "_RoasterReport.xls");
    }
}
