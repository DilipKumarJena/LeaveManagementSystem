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
using System.Globalization;

public partial class LeaveRoaster : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

            ViewState["Day"] = DateTime.Now.Day;
            ViewState["Month"] = DateTime.Now.Month;
            ViewState["Year"] = DateTime.Now.Year;

            Label1.Text = BuildCalendar(FetchMonthLeave(Session["LoginID"].ToString(), Int32.Parse(ViewState["Month"].ToString()), Int32.Parse(ViewState["Year"].ToString())), Int32.Parse(ViewState["Year"].ToString()), Int32.Parse(ViewState["Month"].ToString()));
            Label2.Text = "Holiday Calendar " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Int32.Parse(ViewState["Month"].ToString())) + " " + ViewState["Year"].ToString();
        }
    }

    private DataTable FetchMonthLeave(string LoginID, int Month, int Year)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchOKSHoliday]", conStr);

        DataTable DT = new DataTable();
        Com.CommandType = CommandType.StoredProcedure;
        try
        {
            conStr.Open();

            Com.Parameters.Add("@LoginID", SqlDbType.Int).Value = LoginID;
            Com.Parameters.Add("@Month", SqlDbType.Int).Value = Month;
            Com.Parameters.Add("@Year", SqlDbType.Int).Value = Year;

            DT = command.ExecuteStoredProcedure(Com);

        }
        catch (Exception ee)
        {
            Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { conStr.Close(); }
        return DT;
    }

    public string BuildCalendar(DataTable Holidays, int Year, int Month)
    {
        int DayInt = 0;
        int LastDayInt = 0;
        int AddRow = 5;

        DateTime firstDayOfMonth = new DateTime(Year, Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);
        string firstDay = firstDayOfMonth.DayOfWeek.ToString();
        LastDayInt = lastDayOfMonth.Day;

        if (firstDay == "Sunday")
        {
            if (lastDayOfMonth.Day == 28)
            {
                DayInt = 1;
                AddRow--;
            }
            else
            {
                DayInt = 1;
            }
        }

        if (firstDay == "Monday")
            DayInt = 0;
        if (firstDay == "Tuesday")
            DayInt = -1;
        if (firstDay == "Wednesday")
            DayInt = -2;
        if (firstDay == "Thursday")
            DayInt = -3;
        if (firstDay == "Friday")
        {
            if (lastDayOfMonth.Day >= 30)
            {
                DayInt = -4;
                AddRow++;
            }
            else
            {
                DayInt = -4;
            }
        }
        if (firstDay == "Saturday")
        {
            if (lastDayOfMonth.Day >= 30)
            {
                DayInt = -5;
                AddRow++;
            }
            else
            {
                DayInt = -5;
            }
        }
        string BorderStyleCalendar = "border: black thin solid;";
        string Table = "<table width='100%'  style='" + BorderStyleCalendar + "'>";
        Table += "<tr height='10%' valign='middle'>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:pink'>Sunday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Monday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Tuesday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Wednesday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Thursday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Friday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:pink'>Saturday</td>";
        Table += "</tr>";
        int LeaveCount = Holidays.Rows.Count;
        int LeaveCountCounter = 0;
        for (int Row = 0; Row < AddRow; Row++)
        {
            Table += "<tr height='20px' valign='middle'>";
            for (int Column = 0; Column <= 6; Column++)
            {
                BorderStyleCalendar = "border: black thin solid;";
                if (LeaveCount != 0)
                {
                    string HolidayName = Holidays.Rows[LeaveCountCounter][0].ToString();
                    string HolidayDate = Holidays.Rows[LeaveCountCounter][1].ToString();


                    string TitleInHolidayDate = HolidayDate + " - " + HolidayName;
                    string HolidayColorInWords = "Yellow";
                    if (Convert.ToDateTime(HolidayDate).Day == DayInt)
                    {
                        if (LeaveCountCounter < LeaveCount - 1)
                            LeaveCountCounter++;

                        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + ";background-color :" + HolidayColorInWords + ";font-weight: bold; font-size: large' title = '" + TitleInHolidayDate + "'>";
                        if ((!(DayInt <= 0)) && (DayInt <= LastDayInt))
                            Table += DayInt.ToString();
                        DayInt++;
                        Table += "</td>";
                    }
                    else
                    {
                        if ((!(DayInt <= 0)) && (DayInt <= LastDayInt))
                        {
                            DateTime DD = new DateTime(Year, Month, DayInt);
                            if (DD.DayOfWeek.ToString() == "Saturday" || DD.DayOfWeek.ToString() == "Sunday")
                            {
                                BorderStyleCalendar += "background-color :Pink";
                            }
                            else
                            {
                                if (DD.Day.ToString() == ViewState["Day"].ToString() && DD.Month.ToString() == ViewState["Month"].ToString() && DD.Year.ToString() == ViewState["Year"].ToString())
                                {
                                    BorderStyleCalendar += "background-color : Green; ";
                                }
                            }
                        }

                        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "'>";
                        if ((!(DayInt <= 0)) && (DayInt <= LastDayInt))
                        {

                            Table += DayInt.ToString();

                        }
                        DayInt++;
                        Table += "</td>";
                    }
                }
                else
                {
                    Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "'>";
                    if ((!(DayInt <= 0)) && (DayInt <= LastDayInt))
                        Table += DayInt.ToString();
                    DayInt++;
                    Table += "</td>";
                }
            }
            Table += "</tr>";
        }
        Table += "</table>";
        return Table;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Month"] = DateTime.Now.Month;
        ViewState["Year"] = DateTime.Now.Year;

        Label1.Text = BuildCalendar(FetchMonthLeave(Session["LoginID"].ToString(), Int32.Parse(ViewState["Month"].ToString()), Int32.Parse(ViewState["Year"].ToString())), Int32.Parse(ViewState["Year"].ToString()), Int32.Parse(ViewState["Month"].ToString()));
        Label2.Text = "Holiday Calenday " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Int32.Parse(ViewState["Month"].ToString())) + " " + ViewState["Year"].ToString();
    }
    protected void btnPrevious_Click(object sender, ImageClickEventArgs e)
    {

        int Month = Int32.Parse(ViewState["Month"].ToString());
        int Year = Int32.Parse(ViewState["Year"].ToString());
        if (Month == 1)
        {
            Month = 12;
            Year = Year - 1;
        }
        else
            Month = Month - 1;

        Label1.Text = BuildCalendar(FetchMonthLeave(Session["LoginID"].ToString(), Month, Year), Year, Month);
        ViewState["Month"] = Month;
        ViewState["Year"] = Year;
        Label2.Text = "Holiday Calenday " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month) + " " + Year;

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        int Month = Int32.Parse(ViewState["Month"].ToString());
        int Year = Int32.Parse(ViewState["Year"].ToString());
        if (Month == 12)
        {
            Month = 1;
            Year = Year + 1;
        }
        else
            Month = Month + 1;

        Label1.Text = BuildCalendar(FetchMonthLeave(Session["LoginID"].ToString(), Month, Year), Year, Month);
        ViewState["Month"] = Month;
        ViewState["Year"] = Year;
        Label2.Text = "Holiday Calenday " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month) + " " + Year;
    }
}
