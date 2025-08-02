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
    public partial class MealTypeSetupReport : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                MealTypeList();
            }
        }

        public void MealTypeList()
        {            
            Listview1.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID);
            Listview1.DataBind();
        }
        public string GetPlane(int ID)
        {
            if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == ID).Count() > 0)
            {
                return DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == ID).planname1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetMEal(int ID)
        {            
            if (DB.REFTABLEs.Where(p => p.REFID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.REFID == ID && p.TenentID == TID).REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getPRODUCT(int ID)
        {

            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == ID).Count() > 0)
            {
                string ProdName = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ID).ProdName1;
                return ProdName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getBarCode(int ID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == ID).Count() > 0)
            {
                string BarCode = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ID).BarCode;
                return BarCode;
            }
            else
            {
                return "Not Found";
            }
        }

    }
}