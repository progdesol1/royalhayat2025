using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ReportArea : System.Web.UI.Page
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
                 BindData();
             }
        }

        public void BindData()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

           

            string SQOCommad = "  Select distinct Top(10) City,sum(payment_amount) as Amount " +
                               " from Win_sales_payment Inner join Win_tbl_customer ON Win_sales_payment.c_id = Win_tbl_customer.ID " +
                               " where Win_sales_payment.Tenentid = " + TID + " and sales_time  BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                               "  group by City order by Amount desc ";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView1.DataSource = dt;
            ListView1.DataBind();
           

        }

        //public void AreaBind()
        //{
        //    List<Database.Win_sales_payment> TempList = DB.Win_sales_payment.Where(p => p.TenentID == TID).ToList();
        //    List<CLCustpay> Liatcamt = new List<CLCustpay>();
        //    foreach (Database.Win_sales_payment items in TempList)
        //    {
        //        CLCustpay onj = new CLCustpay();
        //        onj.TenentID = Convert.ToInt32(items.TenentID);
        //        onj.c_id = Convert.ToInt64(items.c_id);
        //        onj.payment_amount = Convert.ToDecimal(TempList.Where(p=>p.TenentID == TID && p.c_id == items.c_id).Sum(p=>p.payment_amount));
        //        onj.sales_time = Convert.ToDateTime(items.sales_time);
        //        Liatcamt.Add(onj);
        //    }
        //    DateTime SDT = Convert.ToDateTime(txtstartdate.Text);
        //    DateTime EDT = Convert.ToDateTime(txtEnddate.Text);
        //    EDT = EDT.AddDays(1);
        //    EDT = EDT.AddSeconds(-1);
        //    long slid = 0;
        //    //Select distinct City,sum(payment_amount) as Amount from sales_payment Inner join tbl_customer ON sales_payment.c_id = tbl_customer.ID  where sales_time  BETWEEN '2018-07-25' AND    '2018-08-25'  group by City order by Amount desc limit 10
        //    var result = (from sl in Liatcamt.Where(p => p.TenentID == TID)
        //                  join cl in DB.Win_tbl_customer.Where(p => p.TenentID == TID) on sl.c_id equals cl.ID
        //                  where sl.sales_time >= SDT && sl.sales_time <= EDT
        //                  select new { sl.c_id, sl.payment_amount, cl.City, cl.ID }
        //                  ).ToList();
        //    var List1 = result.GroupBy(p => p.City).Select(p => p.FirstOrDefault()).OrderByDescending(p=>p.payment_amount).ToList();
        //    var fin = List1.Take(10);

        //    ListView1.DataSource = fin;
        //    ListView1.DataBind();
        //}


        public string getPay(long id)
        {
            string IDD = id.ToString();
            if(DB.Win_sales_payment.Where(p => p.TenentID == TID && p.c_id == IDD).Count() > 0)
            {
                var List = DB.Win_sales_payment.Where(p => p.TenentID == TID && p.c_id == IDD);
                string Payam = List.Sum(m => m.payment_amount).ToString();
                return Payam;
            }
            else
            {
                return "0";
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            // AreaBind();
            BindData();
        }
    }
    public class CLCustpay
    {
        public int TenentID { get; set; }
        public long c_id { get; set; }
        public decimal payment_amount { get; set; }
        public DateTime sales_time { get; set; }
    }
  
}