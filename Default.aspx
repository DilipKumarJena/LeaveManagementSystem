<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
   <style>
    
    

.GridviewScrollHeader TH, .GridviewScrollHeader TD 
{ 
    padding: 5px; 
    font-weight: bold; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #EFEFEF; 
    text-align: left; 
    vertical-align: bottom; 
} 
.GridviewScrollItem TD 
{ 
    padding: 5px; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
} 
.GridviewScrollPager  
{ 
    border-top: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
} 
.GridviewScrollPager TD 
{ 
    padding-top: 3px; 
    font-size: 14px; 
    padding-left: 5px; 
    padding-right: 5px; 
} 
.GridviewScrollPager A 
{ 
    color: #666666; 
}

.GridviewScrollPager SPAN

{

    font-size: 16px;

    font-weight: bold;

}


    
    </style>

</head>
<body>
    <form id="form1" runat="server">
     
         <div>
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="true"
                GridLines="None">
              
                <HeaderStyle CssClass="GridviewScrollHeader" />
                <RowStyle CssClass="GridviewScrollItem" />
                <PagerStyle CssClass="GridviewScrollPager" />
            </asp:GridView>

            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>

            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>

            <script type="text/javascript" src="JavaScript/gridviewScroll.min.js"></script>

            <script type="text/javascript"> 
    $(document).ready(function () { 
        gridviewScroll(); 
    }); 
 
    function gridviewScroll() { 
        $('#<%=GridView1.ClientID%>').gridviewScroll({ 
            width: 660, 
            height: 200, 
            freezesize: 3 
        }); 
    } 
            </script>

        </div>
      
      
      
    
    </form>
</body>
</html>
