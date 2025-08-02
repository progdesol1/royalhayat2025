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

namespace Web.Admin
{
    public partial class ReportTodaySale : System.Web.UI.Page
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
                txtstartdate.Text = EDT.ToString("yyyy-MM-dd");
                BindData();
            }
        }
        public void BindData()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");



            string SQOCommad = " select sales_id as 'Sales ID', InvoiceNO as 'Recipt No' , sales_time as Date , SUM(payment_amount) as Total , emp_id as 'Sold by', " +
                                " SUM(dis) as Discount , SUM(vat) as TAX ,sum(change_amount) as 'change amount', SUM(due_amount) as Due, " +
                                " Comment as Comments " +
                                " from Win_sales_payment where Tenentid = "+ TID+ "  and sales_time   like '%" + stdate + "%'  and return_id = 0 and " +
                                " PaymentStutas = 'Success' group by InvoiceNO,sales_id,sales_time,emp_id,Comment order by sales_id ";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView1.DataSource = dt;
            ListView1.DataBind();
            decimal finalTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTotal = finalTotal + Convert.ToDecimal(dt.Rows[i]["Total"]);
            }
            lblFinalTotal.Text = finalTotal.ToString();

            decimal finaldis = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finaldis = finaldis + Convert.ToDecimal(dt.Rows[i]["Discount"]);
            }
            lbldis.Text = finaldis.ToString();

            decimal finalTAX = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTAX = finalTAX + Convert.ToDecimal(dt.Rows[i]["TAX"]);
            }
            lblTAX.Text = finalTAX.ToString();

            //decimal finalsalesTax = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    finalsalesTax = finalsalesTax + Convert.ToDecimal(dt.Rows[i]["Total" + "TAX"]);
            //}
            //lblsata.Text = finalsalesTax.ToString();

            decimal finalDue = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalDue = finalDue + Convert.ToDecimal(dt.Rows[i]["Due"]);
            }
            lblDue.Text = finalDue.ToString();



            lblfromdate.Text = stdate;
           // lbltodate.Text = etdate;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {

            BindData();
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
        public decimal Getduechange(string InvoiceNo)
        {
            decimal total = 0;
            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvoiceNo).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.due_amount));
            }
            return total;
        }

        public decimal sumtotalTAX(string InvoiceNo)
        {
            decimal total = 0;
            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvoiceNo).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.payment_amount + m.vat));
            }
            return total;
        }
        //public string CGST(string InvNo)
        //{
        //    string Total = "0";
        //    List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvNo).ToList();
        //    if (List.Count() > 0)
        //    {
        //        decimal TempTotal = Convert.ToDecimal(List.Sum(m => m.CGST));
        //        Total = TempTotal.ToString();
        //    }
        //    return Total;
        //}
        //public string SGST(string InvNo)
        //{
        //    string Total = "0";
        //    List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(m => m.TenentID == TID && m.InvoiceNO == InvNo).ToList();
        //    if (List.Count() > 0)
        //    {
        //        decimal TempTotal = Convert.ToDecimal(List.Sum(m => m.SGST));
        //        Total = TempTotal.ToString();
        //    }
        //    return Total;
        //}

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            Label discount = (Label)e.Item.FindControl("Label20");
            Label lblcgst = (Label)e.Item.FindControl("lblcgst");
            Label lblsgst = (Label)e.Item.FindControl("lblsgst");
                
                
                    
        }

    }
}