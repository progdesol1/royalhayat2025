using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;



namespace Web.POS
{
    public partial class ProfitLossReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                DateTime EDT = DateTime.Now;
                txtEnddate.Text = EDT.ToString("MM/dd/yyyy");
                DateTime SDT = EDT.AddMonths(-1);
                txtstartdate.Text = SDT.ToString("MM/dd/yyyy");
                //// CustBind();
                //Clear();
               // CustBind();

            }
        }


        public void CustBind()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

            string SQOCommad = " select SUM(payment_amount), SUM(dis), SUM(vat) , SUM(due_amount)  from Win_sales_payment " +
                                  " where TenentID = " + TID + " and sales_time   >='" + stdate + "' AND  sales_time <='" + etdate + "' ";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

        }

        public void Clear()
        {
            txtstartdate.Text = "";
            txtEnddate.Text = "";
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            CustBind();
        }

        public decimal Gettotal(string InvoiceNo)
        {
            decimal total = 0;
            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvoiceNo).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.payment_amount));
            }
             return total;
        }

        public decimal Getsum(string InvoiceNo)
        {
            decimal total = 0;
            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvoiceNo).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.dis));
            }
            return total;
        }
        public decimal Getvat(string InvoiceNo)
        {
            decimal total = 0;
            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvoiceNo).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.vat));
            }
            return total;
        }
        public decimal Getsumchange(string InvoiceNo)
        {
            decimal total = 0;
            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvoiceNo).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.change_amount));
            }
            return total;
        }

    }
}