using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Classes;

namespace Web.ReportMst
{
    public partial class CustomerOnHold : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;
            if (!IsPostBack)
            {
                Lbind();
            }
        }
        public void Lbind()
        {
            ListView1.DataSource = DB.planmealcustinvoiceHDs.Where(p=>p.TenentID == TID && p.SubscriptionOnHold == true);
            ListView1.DataBind();
        }
        public string GetPlan(int ID)
        {
            if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == ID).Count() > 0)
            {
                string PlanName = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == ID).planname1;
                return PlanName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetCustomer(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                string CName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COMPNAME1;
                return CID +" - "+ CName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetUser(int UID)
        {
            if(DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == UID).Count() > 0)
            {
                string username = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
                return username;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetStatus(bool sta)
        {
            if(sta == true)
            {
                return "Hold";
            }
            else
            {
                return "UnHold";
            }
        }

    }
}