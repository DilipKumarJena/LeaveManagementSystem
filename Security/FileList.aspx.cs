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

public partial class Security_FileList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            DataTable DT = command.ExecuteQuery("Select 0 ID,'<-- Select -->'Location union Select ID,Location+'-'+City from Location Where IsDel=0");

            ddlLocation.DataTextField = "Location";
            ddlLocation.DataValueField = "ID";
            ddlLocation.DataSource = DT;
            ddlLocation.DataBind();





        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFileList.Visible = false;
        btnSubmit.Visible = false;
        Label1.Text = "";

        string id = ddlLocation.SelectedValue;

        DataTable DT = command.ExecuteQuery("Select 0 ID,'<-- Select -->'Location union Select ID,DepartmentName from Department where LocationID='" + id + "' And IsDel=0 ");

        ddlDepartment.DataTextField = "Location";
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataSource = DT;
        ddlDepartment.DataBind();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFileList.Visible = false;
        btnSubmit.Visible = false;
        Label1.Text = "";

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


    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFileList.Visible = false;
        btnSubmit.Visible = false;
        Label1.Text = "";
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

    private DataTable GetFileList()
    {

        DataTable DTFileList = new DataTable();
        DTFileList.Columns.Add("ID", typeof(string));
        DTFileList.Columns.Add("Path", typeof(string));
        DTFileList.Columns.Add("Access", typeof(bool));

        String rootPath = Server.MapPath("~");
        string[] FileLists = Directory.GetFiles(rootPath, "*.aspx", SearchOption.AllDirectories);


        DataRow DrFileList;
        string ABC = "";
        for (int i = 0; i < FileLists.Length; i++)
        {
            DrFileList = DTFileList.NewRow();
            DrFileList["Path"] = FileLists[i].Replace(rootPath, "");
            if (DrFileList["Path"].ToString().Substring(0, 2) == "\\")
                DrFileList["Path"] = DrFileList["Path"].ToString().Substring(2, DrFileList["Path"].ToString().Length - 2);
            if (DrFileList["Path"].ToString().Substring(0, 1) == @"\")
                DrFileList["Path"] = DrFileList["Path"].ToString().Substring(1, DrFileList["Path"].ToString().Length - 1);
            DTFileList.Rows.Add(DrFileList);
        }
        return DTFileList;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StringBuilder XML = new StringBuilder();
        XML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        XML.Append("<Security>");
        foreach (GridViewRow gvR in gvFileList.Rows)
        {
            string ID = ((HiddenField)gvR.FindControl("hfID")).Value;
            string Path = ((Label)gvR.FindControl("lblPath")).Text;
            string Description = ((TextBox)gvR.FindControl("txtDescription")).Text;
            bool Access = ((CheckBox)gvR.FindControl("chkAccess")).Checked;

            XML.Append("<Path ID=\"" + ID + "\" Path=\"" + Path + "\" Access=\"" + Access + "\" UserID=\"" + ddlEmployee.SelectedValue + "\" Description=\"" + Description + "\"/>");
        }
        XML.Append("</Security>");





        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("InsertUpdatePageWiseSecurity", conStr);

        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();
            Com.Parameters.Add("@XMLString", SqlDbType.VarChar).Value = XML.ToString();

            Com.ExecuteNonQuery();

            Label1.Text = "Record Updated";
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

    string PreviousPath = "";
    string Path = "";
    Color col1 = Color.Yellow;
    Color col2 = Color.Pink;
    Color col = Color.Yellow;




    protected void gvFileList_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            //Path = ((DataRowView)e.Row.DataItem).Row.ItemArray[1].ToString().Split(new char[] { '\\' })[0];

            string FullPath = ((DataRowView)e.Row.DataItem).Row.ItemArray[1].ToString();

            int Length = FullPath.IndexOf('\\');
            if (Length == -1)
                Length = FullPath.Length;



            Path = FullPath.Substring(0, Length).Trim();

            //if (PreviousPath == "")
            //    PreviousPath = Path;

            if (PreviousPath != Path)
            {
                if (col == col1)
                {
                    col = col2;
                }
                else
                {
                    col = col1;
                }
                PreviousPath = Path;
            }

            e.Row.BackColor = col;

        }
    }


    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        string Query = " SELECT P.ID , ";
        Query += " UserID , ";
        Query += " D.Path , ";
        Query += " ISNULL(DESCRIPTION, '') DESCRIPTION , ";
        Query += " ISNULL(Access,0) Access ";
        Query += " FROM   Gold.PageDescription D ";
        Query += " LEFT OUTER JOIN  PageWiseSecurity P ON P.PATH = D.Path ";
        Query += " AND UserID = '" + ddlEmployee.SelectedValue + "' ";
        Query += " ORDER BY Path ";

        DataTable UserPageAccess = command.ExecuteQuery(Query);
        DataTable FileList = GetFileList();


        DataTable TableAfterJoin = JoinsInDatatable.Join(FileList, UserPageAccess, FileList.Columns["Path"].ColumnName, UserPageAccess.Columns["Path"].ColumnName);
        DataView DVTableAfterJoin = TableAfterJoin.DefaultView;
        DVTableAfterJoin.Sort = "Path Asc";
        gvFileList.DataSource = DVTableAfterJoin;
        gvFileList.DataBind();
        //Utility.SetGridCss(gvFileList);


        gvFileList.CssClass = "GridViewStyle";
        gvFileList.GridLines = GridLines.Both;
        gvFileList.PagerStyle.CssClass = "PagerStyle";
        gvFileList.HeaderStyle.CssClass = "HeaderStyle";



        gvFileList.Visible = true;
        btnSubmit.Visible = true;

        Label1.Text = "";

        chkTeamLeader.Checked = false;
        chkManager.Checked = false;
        chkHR.Checked = false;
        chkOtherForms.Checked = false;
    }


    protected void chkTeamLeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvFileList.Rows)
        {
            Label L = (Label)gvR.FindControl("lblPath");
            CheckBox c = (CheckBox)gvR.FindControl("chkAccess");

            if (L.Text.Contains(((CheckBox)sender).Text))
            {
                if (((CheckBox)sender).Checked)
                {
                    c.Checked = true;
                }
                else
                {
                    c.Checked = false;
                }
            }
        }
    }
    protected void chkManager_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvFileList.Rows)
        {
            Label L = (Label)gvR.FindControl("lblPath");
            CheckBox c = (CheckBox)gvR.FindControl("chkAccess");

            if (L.Text.Contains(((CheckBox)sender).Text))
            {
                if (((CheckBox)sender).Checked)
                {
                    c.Checked = true;
                }
                else
                {
                    c.Checked = false;
                }
            }
        }

    }
    protected void chkHR_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvFileList.Rows)
        {
            Label L = (Label)gvR.FindControl("lblPath");
            CheckBox c = (CheckBox)gvR.FindControl("chkAccess");

            if (L.Text.Contains(((CheckBox)sender).Text))
            {
                if (((CheckBox)sender).Checked)
                {
                    c.Checked = true;
                }
                else
                {
                    c.Checked = false;
                }
            }
        }

    }
    protected void chkOtherForms_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvR in gvFileList.Rows)
        {
            Label L = (Label)gvR.FindControl("lblPath");
            CheckBox c = (CheckBox)gvR.FindControl("chkAccess");

            if (L.Text.Contains(((CheckBox)sender).Text))
            {
                if (((CheckBox)sender).Checked)
                {
                    c.Checked = true;
                }
                else
                {
                    c.Checked = false;
                }
            }
        }
    }
}
