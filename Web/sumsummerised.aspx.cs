using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web
{
    public partial class sumsummerised : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnsub_Click(object sender, EventArgs e)
        {
            Response.Redirect("Summerised.aspx?p=s");
        }
    }
}