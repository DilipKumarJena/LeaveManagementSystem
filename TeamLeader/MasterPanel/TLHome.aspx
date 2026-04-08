<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageNew.master" AutoEventWireup="true"
    CodeFile="TLHome.aspx.cs" Inherits="TLHome" Title="Team Leader : Home Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    var GB_ROOT_DIR = "../../greybox/";
    </script>

    <script type="text/javascript" src="../../greybox/AJS.js"></script>

    <script type="text/javascript" src="../../greybox/AJS_fx.js"></script>

    <script type="text/javascript" src="../../greybox/gb_scripts.js"></script>

    <script language="javascript" type="text/javascript" src="../../JavaScript/UpdateProgress.js"></script>

    <br />
    <h1 style="padding: 20px">
        Home Page
    </h1>
    <div id="columns">
        <ul id="column1" class="column">
            <li class="widget color-blue">
                <div class="widget-head">
                    <h3>
                        Leave Post
                    </h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <a id="aLeavePost" runat="server" onmouseover="window.status='http://www.oksgroup.com';return true"
                            href="../LeaveApprovalByTL.aspx">Pending Leave Post </a>
                    </h2>
                    <br />
                </div>
            </li>
            <li class="widget color-yellow">
                <div class="widget-head">
                    <h3>
                        Leave Not Posted</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <a id="aLeaveNotApplied" runat="server" onmouseover="window.status='http://www.oksgroup.com';return true"
                            href="../LeaveNotApplied.aspx" style="color: Yellow;" title="Leave Not Applied"
                            onclick="return GB_showCenter('   *   OKS Group   *   View Leave Not Applied', this.href,500,1300)">
                            Click To Show : Leave Not Applied </a>
                    </h2>
                    <br />
                </div>
            </li>
            <li class="widget color-yellow">
                <div class="widget-head">
                    <h3>
                        Leave Balance</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <div id="divLeaveBalance" runat="server">
                    </div>
                    <br />
                </div>
            </li>
            <li class="widget color-blue">
                <div class="widget-head">
                    <h3>
                        Pending Status</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <asp:GridView ID="gvPendingDetail" runat="server" Width="100%" ForeColor="Black"
                        AutoGenerateColumns="true">
                    </asp:GridView>
                    <br />
                </div>
            </li>
        </ul>
        <ul id="column2" class="column">
            <li class="widget color-yellow">
                <div class="widget-head">
                    <h3>
                        Out Duty</h3>
                </div>
                <div class="widget-content">
                    <br />
                    <h2>
                        <a id="aOutDuty" runat="server" href="../OutDutyTeamLeader.aspx" onmouseover="window.status='http://www.oksgroup.com';return true">
                            Pending Out Duty</a>
                    </h2>
                    <br />
                </div>
            </li>
        </ul>
        <ul id="column3" class="column">
            <li class="widget color-blue">
                <div class="widget-head">
                    <h3>
                        <asp:Label ID="lblLastWorkingDayStatus" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="widget-content">
                    <br />
                    <asp:GridView ID="gvLastWorkingDayStatus" runat="server" Width="100%" AutoGenerateColumns="true"
                        ForeColor="Black">
                    </asp:GridView>
                    <br />
                </div>
            </li>
        </ul>
    </div>
    <br />

    <script type="text/javascript" src="jquery-1.2.6.min.js"></script>

    <script type="text/javascript" src="jquery-ui-personalized-1.6rc2.min.js"></script>

    <script type="text/javascript" src="inettuts.js"></script>

</asp:Content>
