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

public partial class TimeSetup : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            string H = "";
            string MS = "";
            int i = 0;
            for (i = 0; i <= 23; i++)
            {
                if (i < 10)
                    H = "0" + i.ToString();
                else
                    H = i.ToString();
                ddlH.Items.Add(new ListItem(H, H));
            }

            for (i = 0; i <= 59; i++)
            {
                if (i < 10)
                    MS = "0" + i.ToString();
                else
                    MS = i.ToString();
                ddlM.Items.Add(new ListItem(MS, MS));
                ddlS.Items.Add(new ListItem(MS, MS));
            }
        }
        else
        {
            if (ddlH.Items.Count == 0)
            {
                string H = "";
                string MS = "";
                int i = 0;
                for (i = 0; i <= 23; i++)
                {
                    if (i < 10)
                        H = "0" + i.ToString();
                    else
                        H = i.ToString();
                    ddlH.Items.Add(new ListItem(H, H));
                }

                for (i = 0; i <= 59; i++)
                {
                    if (i < 10)
                        MS = "0" + i.ToString();
                    else
                        MS = i.ToString();
                    ddlM.Items.Add(new ListItem(MS, MS));
                    ddlS.Items.Add(new ListItem(MS, MS));
                }
            }
        }
    }

    public string ReturnTime()
    {
        string Time = ddlH.SelectedValue + ":" + ddlM.SelectedValue + ":" + ddlS.SelectedValue;
        return Time;
    }

    public void SetHourMinuteSecondInDropDown(string Time)
    {
        Page_Load(null, null);
        // Time Should Be In Format "00:00:00"
        string[] SeprateValueOfTime = Time.Split(':');

        ddlH.SelectedIndex = ddlH.Items.IndexOf(new ListItem(SeprateValueOfTime[0].PadLeft(2, '0'), SeprateValueOfTime[0].PadLeft(2, '0')));
        ddlM.SelectedIndex = ddlM.Items.IndexOf(new ListItem(SeprateValueOfTime[1].PadLeft(2, '0'), SeprateValueOfTime[1].PadLeft(2, '0')));
        ddlS.SelectedIndex = ddlS.Items.IndexOf(new ListItem(SeprateValueOfTime[2].PadLeft(2, '0'), SeprateValueOfTime[2].PadLeft(2, '0')));
    }
    public void ResetHourMinuteSecondDropdown()
    {
        ddlH.SelectedIndex = 0;
        ddlM.SelectedIndex = 0;
        ddlS.SelectedIndex = 0;
    }
}