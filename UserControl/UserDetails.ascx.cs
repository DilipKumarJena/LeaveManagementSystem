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
using System.Security;

public partial class web_controls_UserDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (command.ExecuteScalar("Select PersonelInfo from UserMaster where ID = '" + Session["LoginID"] + "'") == "False")
            Response.Redirect("~/UpdateRegistrationOtherInfoAfterLogin.aspx");



        if (!IsPostBack)
        {
            //SingleImage.Src = "~/ImagePreview.aspx?ID=" + Session["LoginID"];
            //SingleImage.Width = 60;
            //SingleImage.Height = 60;

            a1.HRef = "~/Employee/UpdatePhotoEmployee.aspx";
            Img1.Src = "~/Employee/ImagePreviewEmployee.aspx";
            Img1.Width = 60;
            Img1.Height = 60;

            Label1.Text = Session["LoginName"].ToString();

            Label2.Text = DateTime.Today.ToLongDateString();

            Label3.Text = "Last IP : " + Session["LastIP"].ToString();

            Label4.Text = "Last Login Time : " + Session["LastLoginTime"].ToString();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string ReturnURL = "";
        if (Session["ReturnURL"] == null)
            ReturnURL = "~/LogInLMS.aspx";
        else
            ReturnURL = Session["ReturnURL"].ToString();


        Session.Abandon();
        Session.Clear();
        Session["LoginID"] = null;
        Session["LoginName"] = null;
        Response.Redirect(ReturnURL);
        //FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/" + Session["Home"].ToString());
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        try
        {
            string ReturnURL = "";
            if (Session["ReturnURL"] == null)
                ReturnURL = "http://192.168.165.22:85";
            else
                ReturnURL = Session["ReturnURL"].ToString() + "/RedirectingFromOtherSite.aspx?EmpCode=" + Session["EmployeeCode"];
            Response.Redirect(ReturnURL);
        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message;
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally
        {

        }
    }
}




//<table style="width: 100%" cellpadding="0" cellspacing="0">
//    <tr>
//        <td align="center" valign="baseline" class="upperLinkMenu">

//            <asp:Label ID="Label2" runat="server" Font-Bold="False"></asp:Label>
//            |<asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="False" NavigateUrl="~/Home.aspx">Home</asp:HyperLink>
//            |
//            <asp:Label ID="Label1" runat="server" Font-Bold="False"></asp:Label>
//            |
//            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" NavigateUrl="~/Registration/UpdateRegistration.aspx">Account Setting </asp:HyperLink>
//            |
//            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Font-Bold="False"
//                CausesValidation="False">Logout</asp:LinkButton>
//        </td>
//    </tr>
//</table>