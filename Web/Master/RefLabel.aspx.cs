using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class RefLabel : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               
            }
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            Response.Redirect("REFTABLE.aspx?REFTYPE=BATCH&REFSUBTYPE=OTH");


        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            Response.Redirect("REFTABLE.aspx?REFTYPE=INTERVAL&REFSUBTYPE=INTERVAL");
        }

        protected void btn3_Click(object sender, EventArgs e)
        {
            Response.Redirect("REFTABLE.aspx?REFTYPE=CATTYPE&REFSUBTYPE=CATTYPE");
        }
    }
}