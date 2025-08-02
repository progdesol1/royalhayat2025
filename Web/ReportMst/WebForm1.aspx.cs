using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Classes;

namespace Web.ReportMst
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Listview1.DataSource = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active == true).OrderByDescending(p => p.datetime);
            Listview1.DataBind();
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
    }
}