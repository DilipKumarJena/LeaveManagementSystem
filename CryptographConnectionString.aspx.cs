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

public partial class CryptographConnectionString : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        string clearText = txtConnectionstring.Text.Trim();
        string cipherText = CryptorEngine.Encrypt(clearText, true, txtKey.Text);

        txtEncrypt.Text = cipherText;

    }
    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        string cipherText = txtEncrypt.Text.Trim();
        string decryptedText = CryptorEngine.Decrypt(cipherText, true, txtKey.Text);
        txtDecrypt.Text = decryptedText;

    }
}
