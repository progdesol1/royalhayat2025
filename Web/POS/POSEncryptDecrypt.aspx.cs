using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Classes;
namespace Web.POS
{
    public partial class POSEncryptDecrypt : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string value = TextBox1.Text.ToString().Trim();
            string Encr = Classes.EncryptionClass.Encrypt(value);
            Label1.Text = Encr.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string value = TextBox1.Text.ToString().Trim();
            string Encr = Classes.EncryptionClass.Decrypt(value);
            Label1.Text = Encr.ToString();
        }


    }
}