using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for AvailedLeaveCalendarWithHolidays
/// </summary>
public class AvailedLeaveCalendarWithHolidays
{
    public AvailedLeaveCalendarWithHolidays()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string Show(string EmpID, int Month, int Year)
    {

        string zz = BuildCalendar(FetchMonthLeave(EmpID, Month, Year), Year, Month);
        return zz;


    }



    private static DataTable FetchMonthLeave(string LoginID, int Month, int Year)
    {
        SqlConnection conStr = connection.CreateConneciton();
        SqlCommand Com = new SqlCommand("[FetchLeaveAndHoliday]", conStr);

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
            HttpContext.Current.Session["Ex"] = ee.Message.ToString();
            ErrorLogs.logerrors(ee, HttpContext.Current.Request.Url.ToString(), ee.Message);
        }
        finally { conStr.Close(); }
        return DT;
    }

    private static string BuildCalendar(DataTable DT, int Year, int Month)
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
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string BorderStyleCalendar = "border: black thin solid;";
        string Header = "Calendar For The Month " + mfi.GetMonthName(Month).ToString() + "," + Year;
        string Table = Header;
        Table += "<table width='100%'  style='" + BorderStyleCalendar + "'>";
        Table += "<tr height='10%' valign='middle'>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:pink'>Sunday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Monday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Tuesday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Wednesday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Thursday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:Black;color: #ffffff;'>Friday</td>";
        Table += "<td width='14.28%' align='center' style='" + BorderStyleCalendar + "background-color:pink'>Saturday</td>";
        Table += "</tr>";
        int LeaveCount = DT.Rows.Count;
        int LeaveCountCounter = 0;
        for (int Row = 0; Row < AddRow; Row++)
        {
            Table += "<tr height='20px' valign='middle'>";
            for (int Column = 0; Column <= 6; Column++)
            {
                BorderStyleCalendar = "border: black thin solid;";
                if (LeaveCount != 0)
                {
                    string HolidayName = DT.Rows[LeaveCountCounter][0].ToString();
                    string HolidayDate = DT.Rows[LeaveCountCounter][1].ToString();
                    string Type = DT.Rows[LeaveCountCounter][2].ToString();


                    string TitleInHolidayDate = HolidayDate + " - " + HolidayName;
                    string HolidayColorInWords = "";
                    if (Convert.ToDateTime(HolidayDate).Day == DayInt)
                    {
                        if (LeaveCountCounter < LeaveCount - 1)
                            LeaveCountCounter++;

                        if (Type == "Holiday")
                        {
                            HolidayColorInWords = "Yellow";
                            BorderStyleCalendar += "background-color : Aqua; ";
                        }
                        if (Type == "Leave" && HolidayName.Contains ("Half Day"))
                        {
                            HolidayColorInWords = "Aqua";
                            BorderStyleCalendar += "background-color : Gold; ";
                        }
                        if (Type == "Leave" && HolidayName.Contains("Full Day"))
                        {
                            HolidayColorInWords = "DarkGray";
                            BorderStyleCalendar += "background-color : Gold; ";
                        }

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
                            if (DD.Day.ToString() == DateTime.Now.Day.ToString() && DD.Month.ToString() == DateTime.Now.Month.ToString() && DD.Year.ToString() == DateTime.Now.Year.ToString())
                            {
                                BorderStyleCalendar += "background-color : Green; ";
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

}
