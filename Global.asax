<%@ Application Language="C#" %>

<script RunAt="server">
 
    
    private const string HR = "HR";
    //Live
    private const string DummyPageUrl = "http://192.168.165.22:100/DummyPageForGlobalForASAX.aspx";
    //Testing
    //private const string DummyPageUrl = "http://localhost:52925/LeaveManagementSystem/DummyPageForGlobalForASAX.aspx";


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        RegisterCacheEntry();
    }
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        // If the dummy page is hit, then it means we want to add another item
        // in cache

        if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
        {
            // Add the item in cache and when succesful, do the work.
            RegisterCacheEntry();
        }
    }
    void Application_End(object sender, EventArgs e)
    {
        //Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    private bool RegisterCacheEntry()
    {
        if (null != HttpContext.Current.Cache[HR])
            return false;

        HttpContext.Current.Cache.Add(HR, "HR", null,
            DateTime.MaxValue, TimeSpan.FromDays(1),
            CacheItemPriority.Normal,
            new CacheItemRemovedCallback(AutoUpdateLeaveBalance));
        return true;
    }


    public void AutoUpdateLeaveBalance(string key, object value, CacheItemRemovedReason reason)
    {
        // Uncomment When All Leave Are Updated.
        // Only If Live
        if (DateTime.Now.Day >= 3 && DateTime.Now.Day <= 5)
        {
            if (ExtraParameterForSecurity.GetIPAddress() != "127.0.0.1")
            {
                int Month = DateTime.Now.Month;
                int Year = DateTime.Now.Month;

                System.Data.SqlClient.SqlConnection sqlcon = connection.CreateConneciton();
                System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand();
                try
                {
                    sqlcmd.CommandText = "InsertUpdateLeaveBalanceUpdateTracker";
                    sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcmd.Connection = sqlcon;
                    sqlcmd.Parameters.Clear();

                    sqlcmd.Parameters.Add("@Month", System.Data.SqlDbType.VarChar).Value = DateTime.Now.Month;
                    sqlcmd.Parameters.Add("@Year", System.Data.SqlDbType.VarChar).Value = DateTime.Now.Year;


                    if (sqlcmd.Connection.State != System.Data.ConnectionState.Open)
                        sqlcmd.Connection.Open();
                    sqlcmd.ExecuteNonQuery();
                }
                catch (Exception EE)
                {

                    sqlcmd.CommandText = "InsertErrorLogs";
                    sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcmd.Connection = sqlcon;
                    sqlcmd.Parameters.Add("@ErrorPage", System.Data.SqlDbType.VarChar).Value = "Global.asax:AutoUpdate LeaveBalance";
                    sqlcmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.VarChar).Value = EE.Message;
                    sqlcmd.Parameters.Add("@ExceptionType", System.Data.SqlDbType.VarChar).Value = EE.ToString();
                    sqlcmd.Parameters.Add("@LoginID", System.Data.SqlDbType.VarChar).Value = "";
                    sqlcmd.Parameters.Add("@IP", System.Data.SqlDbType.VarChar).Value = "";

                    try
                    {
                        if (sqlcmd.Connection.State != System.Data.ConnectionState.Open)
                            sqlcmd.Connection.Open();
                        sqlcmd.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                    }
                    finally
                    {
                        sqlcon.Close();
                        sqlcon.Dispose();
                        sqlcmd.Dispose();
                    }
                }
                finally
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                    sqlcmd.Dispose();
                }

            }
            HitPage();
        }
    }
    private void HitPage()
    {
        System.Net.WebClient client = new System.Net.WebClient();
        client.DownloadData(DummyPageUrl);
    }
</script>

