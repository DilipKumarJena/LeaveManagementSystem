using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Net.Mail;


/// <summary>
/// Summary description for ExportToEXCEL
/// </summary>
public class ExportToEXCEL
{
    public ExportToEXCEL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void ExportGridViewToEXCEL(GridView gv, string FileNameWithEXT)
    {
        string attachment = "attachment; filename=" + FileNameWithEXT.Replace(" ", "_") + "";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.RenderControl(htextw);

        HttpContext.Current.Response.Write(stw.ToString());
        HttpContext.Current.Response.End();
    }


    public static void ExportDtToEXCEL(DataTable DT, string FileNameWithEXT)
    {
        DataTable dt = DT;
        string attachment = "attachment; filename=" + FileNameWithEXT.Replace(" ", "_") + "";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            HttpContext.Current.Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        HttpContext.Current.Response.Write("\n");

        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(tab + Utility.RemoveSpecialCharactersAddress(dr[i].ToString()).Replace("\n", "").Replace("“", ""));
                tab = "\t";
            }
            HttpContext.Current.Response.Write("\n");
        }
        HttpContext.Current.Response.End();
    }
    public static void ExportDtToXML(DataTable DT, string DataTableName)
    {
        DataTable dt = DT;
        dt.TableName = DataTableName.Replace(" ", "_");
        dt.WriteXml(HttpContext.Current.Server.MapPath(".") + @"\finalData.xls", XmlWriteMode.IgnoreSchema);
        HttpContext.Current.Response.Redirect("finalData.xls");
    }
    private void ExcelExport(HtmlGenericControl DivHTMLTable, string FileNameWithEXT)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileNameWithEXT.Replace(" ", "_") + "");
        HttpContext.Current.Response.Charset = "";

        // If you want the option to open the Excel file without saving than un comment  the line below
        // Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DivHTMLTable.RenderControl(htmlWrite);
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }



    private static System.IO.MemoryStream ExportToStream(DataTable DT)
    {
        string attachment = "attachment; filename=ABC.XLS";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);


        HtmlForm form = new HtmlForm();
        GridView gv = new GridView();
        gv.DataSource = DT;
        gv.DataBind();
        form.Controls.Add(gv);

        gv.RenderControl(hw);
        string content = sw.ToString();
        byte[] byteData = Encoding.Default.GetBytes(content);
        System.IO.MemoryStream mem = new System.IO.MemoryStream();
        mem.Write(byteData, 0, byteData.Length);
        mem.Flush();
        mem.Position = 0; //reset position to the begining of the stream
        return mem;
    }

    public static Attachment DataTableAsAttchment(DataTable DT, string FileNameWithEXT)
    {
        System.IO.MemoryStream ms = ExportToStream(DT);
        Attachment attachFile = new Attachment(ms, FileNameWithEXT.Replace(" ", "_"), "application/vnd.ms-excel");
        return attachFile;
    }


    public static Attachment DataTableAsAttchmentForGlobalASAX(DataTable DT, string FileNameWithEXT)
    {
        System.IO.MemoryStream ms = ExportToStreamForGlobalASAX(DT);
        Attachment attachFile = new Attachment(ms, FileNameWithEXT, "application/vnd.ms-excel");
        return attachFile;
    }

    private static System.IO.MemoryStream ExportToStreamForGlobalASAX(DataTable DT)
    {
        string attachment = "attachment; filename=ABC.XLS";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        HtmlForm form = new HtmlForm();
        GridView gv = new GridView();
        gv.DataSource = DT;
        gv.DataBind();
        form.Controls.Add(gv);
        gv.RenderControl(hw);
        string content = sw.ToString();
        byte[] byteData = Encoding.Default.GetBytes(content);
        System.IO.MemoryStream mem = new System.IO.MemoryStream();
        mem.Write(byteData, 0, byteData.Length);
        mem.Flush();
        mem.Position = 0; //reset position to the begining of the stream
        return mem;
    }
}
