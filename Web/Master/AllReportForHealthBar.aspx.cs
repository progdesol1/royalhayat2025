using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

namespace Web.Master
{
    public partial class AllReportForHealthBar : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        string FROM = "";
        string TO = "";
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {               
                Default();

            }
        }
        public void Default()
        {                       
            txtdateTO.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtdateFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }

        protected void btnRepCustomer_Click(object sender, EventArgs e)
        {
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            else
                FDT = DateTime.Now;

            if (txtdateTO.Text != "")
                TDT = Convert.ToDateTime(txtdateTO.Text);
            else
                TDT = DateTime.Now;
            
            if (btnRepCustomer.Text == "Customer Report")
            {                
                Response.Redirect("../ReportMst/CustomerList.aspx?FDT=" + FDT + "&TDT=" + TDT);
            }

        }

        protected void btnRepPlan_Click(object sender, EventArgs e)
        {
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            else
                FDT = DateTime.Now;

            if (txtdateTO.Text != "")
                TDT = Convert.ToDateTime(txtdateTO.Text);
            else
                TDT = DateTime.Now;

            if (btnRepPlan.Text == "Plan Report")
            {
                Response.Redirect("../ReportMst/CustomerPlanList.aspx?FDT=" + FDT + "&TDT=" + TDT);
            }            
        }

        //protected void btnRepDriverCheckList_Click(object sender, EventArgs e)
        //{
        //    DateTime? FDT = null;
        //    DateTime? TDT = null;
        //    if (txtdateFrom.Text != "")
        //        FDT = Convert.ToDateTime(txtdateFrom.Text);
        //    else
        //        FDT = DateTime.Now;

        //    if (txtdateTO.Text != "")
        //        TDT = Convert.ToDateTime(txtdateTO.Text);
        //    else
        //        TDT = DateTime.Now;

        //    if(btnRepDriverCheckList.Text == "Driver Check List Report")
        //    {
        //        //Response.Redirect("../ReportMst/AMPMnew123.aspx");
            
        //    }
        //}

        protected void btnRepDeliveryCard_Click(object sender, EventArgs e)
        {
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            else
                FDT = DateTime.Now;

            if (txtdateTO.Text != "")
                TDT = Convert.ToDateTime(txtdateTO.Text);
            else
                TDT = DateTime.Now;

            if (btnRepDeliveryCard.Text == "Delivery Card Report")
            {
                Response.Redirect("../ReportMst/DeliveryCard.aspx?FDT=" + FDT + "&TDT=" + TDT);
            }
        }

        protected void btnRepOnHold_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ReportMst/CustomerOnHold.aspx");
        }

        protected void btnRepKitchen_Click(object sender, EventArgs e)
        {
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            else
                FDT = DateTime.Now;

            if (txtdateTO.Text != "")
                TDT = Convert.ToDateTime(txtdateTO.Text);
            else
                TDT = DateTime.Now;
            if (btnRepKitchen.Text == "Kitchen Report")
            {
                Response.Redirect("../ReportMst/AMPMDAY.aspx?FDT=" + FDT + "&TDT=" + TDT);
            }
        }      






    }
}