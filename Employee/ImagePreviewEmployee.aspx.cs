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
using System.IO;
using System.Drawing;

public partial class ImagePreviewEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        //Response.Write("");
        try
        {
            if (Session["LoginID"] != null)
            {
                string ID = Session["LoginID"].ToString();
                //SqlConnection con = Connection.CreateConneciton();
                string strQry = "select Photo from UserMasterOtherInfo where UserMasterID='" + ID + "'";




                DataTable dt = command.ExecuteQuery(strQry);
                byte[] image = (byte[])dt.Rows[0][0];
                MemoryStream mstream = new MemoryStream();
                mstream.Write(image, 0, image.Length);

                Bitmap bitmap = new Bitmap(mstream);
                Response.ContentType = "image/jpeg";
                bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                mstream.Close();


            }
        }

        catch
        {
            Response.Write("");
        }

    }
}
