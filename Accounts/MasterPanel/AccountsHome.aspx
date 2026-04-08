<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="AccountsHome.aspx.cs" Inherits="AccountsHome" Title="AccountsHome : Home Page" %>

<%@ Register Src="UC/CompensatoryLeaveAccountsApproval.ascx" TagName="CompensatoryLeaveAccountsApproval"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../../greybox/";
    </script>

    <script type="text/javascript" src="../../greybox/AJS.js"></script>

    <script type="text/javascript" src="../../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../../greybox/gb_scripts.js"></script>

    <br />
    <h1 style="padding: 20px">
        Accounts : Home Page
    </h1>
    <div id="columns">
        <ul id="column1" class="column">
            <li class="widget color-red">
                <div class="widget-head">
                    <h3>
                        Compensatory Leave</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <uc1:CompensatoryLeaveAccountsApproval ID="CompensatoryLeaveAccountsApproval1" runat="server" />
                    <br />
                </div>
            </li>
        </ul>
        <%--  <ul id="column2" class="column">
            <li class="widget color-yellow">
                <div class="widget-head">
                    <h3>
                        Out Duty</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <a id="aOutDuty" runat="server" href="../OutDutyManager.aspx" onmouseover="window.status='http://www.oksgroup.com';return true">
                            Pending Out Duty</a>
                    </h2>
                    <br />
                </div>
            </li>
            <li class="widget color-orange">
                <div class="widget-head">
                    <h3>
                        Pending Team Leader Status</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <asp:GridView ID="gvPendingStatus" runat="server" Width="100%" ForeColor="Black">
                    </asp:GridView>
                    <br />
                </div>
            </li>
            <li class="widget color-yellow">
                <div class="widget-head">
                    <h3>
                        Weekly Working Status</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <span id="spanWorkingStatus" runat="server">Weekly Working Status</span>
                        <br />
                        <br />
                    </h2>
                </div>
            </li>
            <li class="widget color-orange">
                <div class="widget-head">
                    <h3>
                        If Also Team Leader (In LMS)</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <a href="../../TeamLeader/MasterPanel/TLHome.aspx">Click Here To Navigate TL's Home
                            Page</a>
                    </h2>
                    <br />
                </div>
            </li>
        </ul>
        <ul id="column3" class="column">
            <li class="widget color-red">
                <div class="widget-head">
                    <h3>
                        Leave Post</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <a id="aLeavePost" runat="server" onmouseover="window.status='http://www.oksgroup.com';return true"
                            href="../LeaveApprovalByHOD.aspx">Pending Leave Post </a>
                    </h2>
                    <br />
                </div>
            </li>
            <li class="widget color-white">
                <div class="widget-head">
                    <h3>
                        Status : <span id="Date" runat="server"></span>
                    </h3>
                </div>
                <div class="widget-content">
                    <br />
                    <asp:GridView ID="gvLastWorkingDayStatus" runat="server" Width="100%" ForeColor="Black">
                    </asp:GridView>
                    <br />
                </div>
            </li>
        </ul>--%>
    </div>
    <br />

    <script type="text/javascript" src="jquery-1.2.6.min.js"></script>

    <script type="text/javascript" src="jquery-ui-personalized-1.6rc2.min.js"></script>

    <script type="text/javascript" src="inettuts.js"></script>

</asp:Content>
