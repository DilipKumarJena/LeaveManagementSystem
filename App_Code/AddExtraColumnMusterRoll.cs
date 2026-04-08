using System;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for AddExtraColumnMusterRoll
/// </summary>
public class AddExtraColumnMusterRoll
{
    public AddExtraColumnMusterRoll()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    static double Total = 0; static double P = 0; static double W = 0; static double O = 0;
    static double CL = 0; static double SL = 0; static double PL = 0; static double CompOff = 0; static double LWP = 0;
    static double ML = 0; static double OT = 0; static double ESI = 0; static double MiL = 0; static double W_O = 0;
    static double RoL = 0;

    static double Pending = 0; static double ApplyFullDay = 0; static double ApplyHalfDay = 0; static double Warning = 0;
    static double MisPunch = 0; static double LeaveNotApplied = 0; static double ExtraWork = 0;
    static double TotalWork = 0;

    static DateTime Date = new DateTime();

    public static DataTable Add(ref DataTable Emp, string StartDate, string EndDate, bool HR)
    {


        DataRow NewHeaderRow = Emp.NewRow();
        Date = Convert.ToDateTime(StartDate);




        if (Emp.Rows.Count != 0)
        {
            Emp.Columns.Add("Total");
            Emp.Columns.Add("P");
            Emp.Columns.Add("W");
            Emp.Columns.Add("O");

            Emp.Columns.Add("CL");
            Emp.Columns.Add("SL");
            Emp.Columns.Add("PL");
            Emp.Columns.Add("CompOff");
            Emp.Columns.Add("RoL");
            Emp.Columns.Add("LWP");
            Emp.Columns.Add("ML");
            Emp.Columns.Add("OT");
            Emp.Columns.Add("ESI");
            Emp.Columns.Add("MiL");
            Emp.Columns.Add("W/O");


            Emp.Columns.Add("Pending");
            Emp.Columns.Add("ApplyFullDay");
            Emp.Columns.Add("ApplyHalfDay");
            Emp.Columns.Add("Warning");

            Emp.Columns.Add("MisPunch");
            Emp.Columns.Add("LeaveNotApplied");

            if (HR == true)
            {
                Emp.Columns.Add("TotalWork");
                Emp.Columns.Add("ExtraWork");
            }

            for (int i = 6; i < Emp.Columns["EarnedCompOff"].Ordinal; i++)
            {
                if (Utility.IsNumeric(Emp.Columns[i].ColumnName))
                {
                    NewHeaderRow[i] = Date.ToShortDateString();
                    Date = Date.AddDays(1);
                }
            }
            Total = Utility.DateDifference(StartDate, EndDate) + 1;

            //Casual Leave-Second Half-Half Day
            foreach (DataRow Dr in Emp.Rows)
            {

                if (Dr[0].ToString() == "AA027")
                { }

                P = 0; W = 0; O = 0; CL = 0; SL = 0; PL = 0; CompOff = 0;
                LWP = 0; ML = 0; OT = 0; ESI = 0; MiL = 0; W_O = 0; RoL = 0;
                ApplyFullDay = 0; ApplyHalfDay = 0; Warning = 0; MisPunch = 0; LeaveNotApplied = 0; Pending = 0; ExtraWork = 0;
                TotalWork = 0;



                for (int i = 6; i < Emp.Columns["EarnedCompOff"].Ordinal; i++)
                {


                    if (Utility.IsNumeric(Emp.Columns[i].ColumnName))
                    {
                        if (Dr[i].ToString() == "P")
                            P++;
                        else if (Dr[i].ToString() == "W")
                            W++;
                        else if (Dr[i].ToString() == "O")
                            O++;
                        else if (Dr[i].ToString().Contains("Warning"))
                        {
                            P++;
                            Warning++;
                        }
                        else if (Dr[i].ToString().Contains("Apply Full Day"))
                            ApplyFullDay++;
                        else if (Dr[i].ToString().Contains("Apply Half Day"))
                            ApplyHalfDay++;
                        else if (Dr[i].ToString() == "Leave Not Applied")
                            LeaveNotApplied++;
                        else if (Dr[i].ToString() == "MisPunch")
                            MisPunch++;
                        else if (Dr[i].ToString().Contains("Pending"))
                            Pending++;


                        SetLeaveStatusInVariable(ref CL, Dr[i].ToString(), "Casual Leave");
                        SetLeaveStatusInVariable(ref SL, Dr[i].ToString(), "Sick Leave");
                        SetLeaveStatusInVariable(ref PL, Dr[i].ToString(), "Privilege Leave");
                        SetLeaveStatusInVariable(ref CompOff, Dr[i].ToString(), "Compensatory Leave (In Hours)");
                        SetLeaveStatusInVariable(ref LWP, Dr[i].ToString(), "Leave With Out Pay");
                        SetLeaveStatusInVariable(ref ML, Dr[i].ToString(), "Maternity Leave");
                        SetLeaveStatusInVariable(ref OT, Dr[i].ToString(), "On Tour Leave");
                        SetLeaveStatusInVariable(ref ESI, Dr[i].ToString(), "ESI Leave");
                        SetLeaveStatusInVariable(ref MiL, Dr[i].ToString(), "Marriage Leave");
                        SetLeaveStatusInVariable(ref W_O, Dr[i].ToString(), "Week End / OKS Holiday");
                        SetLeaveStatusInVariable(ref RoL, Dr[i].ToString(), "Roastering Off Leave (In Hours)");
                    }
                    if (Emp.Columns[i].ColumnName.Contains("_T"))
                    {
                        if (Date.DayOfWeek != DayOfWeek.Saturday && Date.DayOfWeek != DayOfWeek.Sunday)
                            TotalWork += ConvertHourMinute(Dr[i].ToString());
                    }
                    if (Emp.Columns[i].ColumnName.Contains("_Diff"))
                    {
                        if (Date.DayOfWeek != DayOfWeek.Saturday && Date.DayOfWeek != DayOfWeek.Sunday)
                            ExtraWork += Convert.ToInt32(Dr[i].ToString());
                        Date = Date.AddDays(1);
                    }
                }


                Dr["Total"] = Total;
                Dr["P"] = P;
                Dr["W"] = W;
                Dr["O"] = O;
                Dr["CL"] = CL;
                Dr["SL"] = SL;
                Dr["PL"] = PL;
                Dr["CompOff"] = CompOff;
                Dr["LWP"] = LWP;
                Dr["ML"] = ML;
                Dr["OT"] = OT;
                Dr["ESI"] = ESI;
                Dr["MiL"] = MiL;
                Dr["W/O"] = W_O;
                Dr["RoL"] = RoL;

                Dr["Pending"] = Pending;
                Dr["ApplyFullDay"] = ApplyFullDay;
                Dr["ApplyHalfDay"] = ApplyHalfDay;
                Dr["Warning"] = Warning;
                Dr["MisPunch"] = MisPunch;
                Dr["LeaveNotApplied"] = LeaveNotApplied;

                if (HR == true)
                {
                    Dr["TotalWork"] = TotalWork;
                    Dr["ExtraWork"] = ExtraWork;
                }

            }
        }
        Emp.Rows.InsertAt(NewHeaderRow, 0);
        return Emp;
    }

    private static int ConvertHourMinute(string Value)
    {
        int Val = 0;
        if (Value != "")
        {
            string[] Ar = Value.Split(':');
            int Hour = Convert.ToInt32(Ar[0]);
            int Minute = Convert.ToInt32(Ar[1]);
            Val = (Hour * 60) + Minute;
        }

        return Val;

    }

    private static void SetLeaveStatusInVariable(ref double LeaveVariable, string CurrentLeaveStatus, string LeaveType)
    {
        string[] AllLeaves = CurrentLeaveStatus.Split('|');
        for (int i = 0; i < AllLeaves.Length; i++)
        {
            if ((AllLeaves[i].Contains(LeaveType) == true) && (AllLeaves[i].Contains("Half Day") == true))
            {
                if (AllLeaves.Length == 2)
                {
                    LeaveVariable = LeaveVariable + .5;
                    P = P + .5;
                }
                else
                {
                    LeaveVariable = LeaveVariable + .5;
                }
            }
            else if ((AllLeaves[i].Contains(LeaveType) == true) && (AllLeaves[i].Contains("Full Day") == true))
                LeaveVariable = LeaveVariable + 1;
        }

    }
}