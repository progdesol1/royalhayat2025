using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.ReportMst
{
    public partial class ProductListReport : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                ProductList();
            }
        }
        public void ProductList()
        {
            Listview1.DataSource = DB.TBLPRODUCTs.Where(p=>p.TenentID == TID);
            Listview1.DataBind();
        }
        public string GetUOM(int ID)
        {
            if(DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == ID).Count() > 0)
            {
                string UOMNAME = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == ID).UOMNAME1;
                return UOMNAME;
            }
            else
            {
                return "Not Found";
            }
        }


    }
}