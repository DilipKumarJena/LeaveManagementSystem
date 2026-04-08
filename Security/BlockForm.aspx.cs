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
using System.Drawing;
using System.Text;
using System.IO;
using System.Data.SqlClient;

public partial class BlockForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            GetFileList();
        }
    }



    private void GetFileList()
    {
        DataTable PageDescription = command.ExecuteQuery("Select ID,Path,Description,Block from PageDescription");










        DataTable DTFileList = new DataTable();
        DTFileList.Columns.Add("ID", typeof(string));
        DTFileList.Columns.Add("Path", typeof(string));
        DTFileList.Columns.Add("Access", typeof(bool));

        String rootPath = Server.MapPath("~");
        string[] FileLists = Directory.GetFiles(rootPath, "*.aspx", SearchOption.AllDirectories);


        DataRow DrFileList;
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



        DataTable TableAfterJoin = JoinsInDatatable.Join(DTFileList, PageDescription, DTFileList.Columns["Path"].ColumnName, PageDescription.Columns["Path"].ColumnName);
        DataView DVTableAfterJoin = TableAfterJoin.DefaultView;
        DVTableAfterJoin.Sort = "Path Asc";
        gvFileList.DataSource = DVTableAfterJoin;
        gvFileList.DataBind();
        //Utility.SetGridCss(gvFileList);


        gvFileList.CssClass = "GridViewStyle";
        gvFileList.GridLines = GridLines.Both;
        gvFileList.PagerStyle.CssClass = "PagerStyle";
        gvFileList.HeaderStyle.CssClass = "HeaderStyle";
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
            bool Block = ((CheckBox)gvR.FindControl("chkBlock")).Checked;

            XML.Append("<Path ID=\"" + ID + "\" Path=\"" + Path + "\" Block=\"" + Block + "\" Description=\"" + Description + "\"/>");
        }
        XML.Append("</Security>");





        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[InsertUpdatePageDecriptionBlockPage]", conStr);

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
            string FullPath = ((DataRowView)e.Row.DataItem).Row.ItemArray[1].ToString();
            int Length = FullPath.IndexOf('\\');
            if (Length == -1)
                Length = FullPath.Length;
            Path = FullPath.Substring(0, Length).Trim();
            if (PreviousPath != Path)
            {
                if (col == col1)
                    col = col2;
                else
                    col = col1;
                PreviousPath = Path;
            }
            e.Row.BackColor = col;
        }
    }
}