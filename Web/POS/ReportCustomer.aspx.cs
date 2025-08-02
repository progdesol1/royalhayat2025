using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web.Admin
{
    public partial class ReportCustomer : System.Web.UI.Page
    {
        //OleDbConnection Econ;
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLEntitiesNew"].ConnectionString);
        //SqlCommand command2 = new SqlCommand();

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
               // CustBind();
            }
        }
        public void CustBind()
        {
            DateTime SDT = Convert.ToDateTime(txtstartdate.Text);
            DateTime EDT = Convert.ToDateTime(txtEnddate.Text);
            EDT = EDT.AddDays(1);
            EDT = EDT.AddSeconds(-1);
            //string SQOCommad = "Select top(10) c_id,Customer,count(c_id) as NoofCID ,sum(Payment_Amount) as 'Total Amount' from Win_sales_payment where TenentID=" + TID + " AND sales_time BETWEEN '" + SDT + "' AND '" + EDT + "' group by c_id,Customer order by sum(Payment_Amount) desc;";
            //SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            //SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            //DataSet ds = new DataSet();
            //ADB.Fill(ds);
            //DataTable dt = ds.Tables[0];

            List<custamt> Liatcamt = new List<custamt>();

            List<Database.Win_sales_payment> List = DB.Win_sales_payment.Where(p => p.TenentID == TID).ToList();// && p.sales_time == SDT && p.sales_time <= EDT).ToList();
            List<Database.Win_sales_payment> ListAmu = List.GroupBy(p => p.c_id).Select(p => p.FirstOrDefault()).ToList();

            foreach (Database.Win_sales_payment item in ListAmu)
            {
                string cid = item.c_id;
                decimal amt = Convert.ToDecimal(List.Where(p => p.c_id == cid).Sum(p => p.payment_amount));
                custamt obj = new custamt();
                obj.C_id = cid;
                obj.CustomerName = item.Customer;
                obj.amt = amt;
                Liatcamt.Add(obj);
            }


           // List = DB.Win_sales_payment.Where(p => p.TenentID == TID && p.sales_time >= SDT && p.sales_time <= EDT).GroupBy(p => p.c_id).Select(p => p.FirstOrDefault()).Take(10).ToList();
            ListView1.DataSource = Liatcamt.OrderByDescending(p=>p.amt).Take(10);
            ListView1.DataBind();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            CustBind();
        }


    }

    public class custamt
    {
        public string C_id { get; set; }
        public string CustomerName { get; set; }
        public decimal amt { get; set; }
    }
}