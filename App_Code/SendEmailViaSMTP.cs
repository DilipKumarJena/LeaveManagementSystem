using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendEmailViaSMTP
{
    public SendEmailViaSMTP()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string SendEmail(string From, string To, string CC, string Subject, string MailBodyInHTML)
    {
        string ReturnMessage = "";
        try
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            SetMailAddress(ref msg, To, "To");
            SetMailAddress(ref msg, From, "From");
            SetMailAddress(ref msg, CC, "CC");



            msg.Subject = Subject;
            msg.Body = MailBodyInHTML;



            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient cli = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["MailServer"]);
            cli.Port = Int32.Parse(ConfigurationManager.AppSettings["MailServerPort"].ToString());
            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpUser"], ConfigurationManager.AppSettings["SmtpPass"]);
            cli.UseDefaultCredentials = false;
            cli.Credentials = basicAuthenticationInfo;

            if (ExtraParameterForSecurity.GetIPAddress() != "127.0.0.1")
                cli.Send(msg);
            ReturnMessage = "Mail Sent. ";
        }
        catch (Exception ee)
        {
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            ReturnMessage = "Some Error Occured. Mail Not Sent. ";
        }
        return ReturnMessage;
    }

    public static string SendEmailWithAttachment(string From, string To, string CC, string Subject, string MailBodyInHTML, System.Net.Mail.Attachment Attachment)
    {
        string ReturnMessage = "";
        try
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            SetMailAddress(ref msg, To, "To");
            SetMailAddress(ref msg, From, "From");
            SetMailAddress(ref msg, CC, "CC");



            msg.Subject = Subject;
            msg.Body = MailBodyInHTML;

            msg.Attachments.Add(Attachment);

            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient cli = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["MailServer"]);
            cli.Port = Int32.Parse(ConfigurationManager.AppSettings["MailServerPort"].ToString());
            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpUser"], ConfigurationManager.AppSettings["SmtpPass"]);
            cli.UseDefaultCredentials = false;
            cli.Credentials = basicAuthenticationInfo;

            if (ExtraParameterForSecurity.GetIPAddress() != "127.0.0.1")
                cli.Send(msg);
            ReturnMessage = "Mail Sent. ";
        }
        catch (Exception ee)
        {
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            ReturnMessage = "Some Error Occured. Mail Not Sent. ";
        }
        return ReturnMessage;
    }



    public static string SendEmailWithAttachment(string From, string To, string CC, string Subject, string MailBodyInHTML, ArrayList Attachment)
    {
        string ReturnMessage = "";
        try
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            SetMailAddress(ref msg, To, "To");
            SetMailAddress(ref msg, From, "From");
            SetMailAddress(ref msg, CC, "CC");



            msg.Subject = Subject;
            msg.Body = MailBodyInHTML;

            for (int i = 0; i < Attachment.Count; i++)
            {
                System.Net.Mail.Attachment Att = (System.Net.Mail.Attachment)Attachment[i];
                msg.Attachments.Add(Att);
            }



            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient cli = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["MailServer"]);
            cli.Port = Int32.Parse(ConfigurationManager.AppSettings["MailServerPort"].ToString());
            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpUser"], ConfigurationManager.AppSettings["SmtpPass"]);
            cli.UseDefaultCredentials = false;
            cli.Credentials = basicAuthenticationInfo;

            if (ExtraParameterForSecurity.GetIPAddress() != "127.0.0.1")
                cli.Send(msg);
            ReturnMessage = "Mail Sent. ";
        }
        catch (Exception ee)
        {
            ErrorLogs.logerrorsWithOutRedirect(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
            ReturnMessage = "Some Error Occured. Mail Not Sent. ";
        }
        return ReturnMessage;
    }


    private static void SetMailAddress(ref System.Net.Mail.MailMessage msg, string IDs, string Type)
    {
        string[] ID = IDs.Split(',');
        for (int i = 0; i < ID.Length; i++)
        {
            if (ID[i] != "")
            {
                if (Type == "To")
                {
                    if (msg.To.Contains(new System.Net.Mail.MailAddress(ID[i])) == false && msg.CC.Contains(new System.Net.Mail.MailAddress(ID[i])) == false)
                        msg.To.Add(ID[i]);
                }
                if (Type == "CC")
                {
                    if (msg.CC.Contains(new System.Net.Mail.MailAddress(ID[i])) == false && msg.To.Contains(new System.Net.Mail.MailAddress(ID[i])) == false)
                        msg.CC.Add(ID[i]);
                }
                if (Type == "From")
                {

                    msg.From = new System.Net.Mail.MailAddress(ID[i]);
                }
            }
        }
    }
}
