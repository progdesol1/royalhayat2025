using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web
{
    public partial class ShopIndexDemo : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                MainBind();
            }
            
        }
        public void MainBind()
        {
            ListView1.DataSource = DB.tests;
            ListView1.DataBind();
            
        }
    }
}