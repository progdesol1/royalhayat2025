using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Admin
{
    public partial class Index : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int TID = Convert.ToInt32(((Win_usermgt)Session["USER"]).TenentID);
                
                List<Database.Win_sales_payment> Saleitem = DB.Win_sales_payment.Where(p => p.TenentID == TID).ToList();

            }
        }




    }
}