using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;

namespace Web
{
    public partial class TikitClient : System.Web.UI.MasterPage
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EcommUSER"] == null)
                {
                    Response.Redirect("Login.aspx");

                }
                 string UIN = ((TBLCONTACT)Session["EcommUSER"]).PersName1;
                 lblUserName.Text = UIN;
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void pnlresponsive1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyAccount.aspx");
        }
    }
}