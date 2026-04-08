<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword"
    Title="Forget Password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forget Password</title>

    <script language="javascript" type="text/javascript" src="../JavaScript/TextValidations.js"></script>

    <script language="javascript" type="text/javascript" src="../JavaScript/UpdateProgress.js"></script>

</head>
<body style="background-color: #ede7d0">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut= "360000" runat="server">
            </asp:ScriptManager>
            <div style="background-color: Transparent; position: absolute; display: none; width: 220px;
                height: 118px;" id="UpdateProgress" align="center">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="../Images/ajax-loader-new__.gif" /><br />
                        <img src="../Images/PleaseWait.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td colspan="3">
                                <h1>
                                    Forget Password
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="containerDark">
                                    <b class="rtopDark"><b class="r1Dark"></b><b class="r2Dark"></b><b class="r3Dark"></b>
                                        <b class="r4Dark"></b></b>
                                    <div style="padding: 15px">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="width: 20%">
                                                </td>
                                                <td style="width: 60%">
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                                                    Width="278px">
                                                                    <asp:ListItem Value="0" Selected="True">Employee Code</asp:ListItem>
                                                                    <asp:ListItem Value="2">E-MailID</asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 60%" align="center">
                                                                <asp:TextBox ID="txtBox" runat="server" CssClass="text" Width="75%"></asp:TextBox>
                                                                <br />
                                                                <span style="display: none" id="spanName" class="errorRed">Please Enter Value</span>
                                                                <span style="display: none" id="SpanInvalidEmail" class="errorRed">Invalid Email</span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                </td>
                                                <td align="center" style="width: 60%">
                                                    <asp:Button ID="btnSubmit" runat="server" OnClientClick="return validateForm()" OnClick="btnSubmit_Click"
                                                        Text="Submit" CssClass="text" />
                                                </td>
                                                <td style="width: 20%">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <b class="rbottomDark"><b class="r4Dark"></b><b class="r3Dark"></b><b class="r2Dark">
                                    </b><b class="r1Dark"></b></b>
                                </div>
                                <span id="spanMessage" runat="server"></span>
                            </td>
                        </tr>
                    </table>

                    <script language="javascript" type="text/javascript" src="../JavaScript/EmailValidation.js"></script>

                    <script language="javascript" type="text/javascript">
  function validateForm()
{
   hideSpan();

   if (document.getElementById('<%= txtBox.ClientID %>').value == "")
   {
      document.getElementById('spanName').style.display = ''
      document.getElementById('<%= txtBox.ClientID %>').focus();
      return false;
   }
   var rdo = document.getElementById ( '<%=RadioButtonList1.ClientID %>' );
   var RadioButtonS = rdo.getElementsByTagName( 'input' );

   if( RadioButtonS[2].type == 'radio' && RadioButtonS[2].checked == true )
   {
      var B = EMailCheck(document.getElementById('<%= txtBox.ClientID %>').value)
      if (B == false)
      {
         document.getElementById('SpanInvalidEmail').style.display = ''
         document.getElementById('<%= txtBox.ClientID %>').focus();
         return false;
      }
      var C = emailCheck(document.getElementById('<%= txtBox.ClientID %>').value)
      if (C == false)
      {
         document.getElementById('SpanInvalidEmail').style.display = ''
         document.getElementById('<%= txtBox.ClientID %>').focus();
         return false;
      }
   }
}
function hideSpan()
{
   document.getElementById('spanName').style.display = 'none'

}
                    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
