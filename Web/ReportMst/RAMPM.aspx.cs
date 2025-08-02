using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.ReportMst
{
    public partial class RAMPM : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                AMPMRep();
            }
        }
        public void AMPMRep()
        {
            Listview1.DataSource = DB.View_DriverCheckList.GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault());
            Listview1.DataBind();
        }
        public void date()
        {
            int CID = Convert.ToInt32(DB.View_DriverCheckList.GroupBy(p => p.CustomerID).FirstOrDefault());

        }


    }
}