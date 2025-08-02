using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.ACM
{
    public partial class SessionTimeOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] == null || string.IsNullOrEmpty(Session["USER"].ToString()))//error
            {
                pnlSuccessMsg.Visible = true;
                lblMsg.Text = "Session Is Time Out for This System so Please Logout And Login Again... ";
            }
        }
    }
}