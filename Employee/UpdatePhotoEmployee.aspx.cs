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

public partial class UpdatePhotoEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            Utility.InvalidUserRedirectToLoginPage();
            SetImage();
        }
    }

    private void SetImage()
    {
        SingleImage.Src = "ImagePreviewEmployee.aspx";
        SingleImage.Width = 250;
        SingleImage.Height = 250;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuUpload.HasFile)
        {

            byte[] Arr = (byte[])PhotoVideoAlbum.ConvertInByteArray(fuUpload, "Equal");

            SqlConnection conStr = connection.CreateConneciton();
            SqlCommand com = new SqlCommand("UpdateUserMasterOtherInfoPhoto", conStr);
            com.CommandType = CommandType.StoredProcedure;


            com.Parameters.Add("@UserMasterID", SqlDbType.Int).Value = Session["LoginID"].ToString();
            com.Parameters.Add("@Photo", SqlDbType.Image).Value = Arr;


            try
            {
                if (conStr.State != ConnectionState.Open)
                    conStr.Open();
                com.ExecuteNonQuery();
                SetImage();
                lblMessage.Text = "Image Uploaded.";

            }
            catch { }
            finally
            {
                conStr.Close();

            }
        }
        else
        {
            lblMessage.Text = "Please Select Image File.";
        }

    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {

        command.ExecuteNonQuery("Update UserMasterOtherInfo Set Photo = Null Where UserMasterID = '" + Session["LoginID"].ToString() + "'");
        SetImage();
        lblMessage.Text = "Image Removed.";
    }
}
