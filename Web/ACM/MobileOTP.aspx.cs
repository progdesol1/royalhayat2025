using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.ACM
{
    public partial class MobileOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnotp_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConfirmOTP.aspx");
        }
    }
}