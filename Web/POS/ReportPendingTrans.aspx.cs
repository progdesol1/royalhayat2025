using System;
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
    public partial class ReportPendingTrans : System.Web.UI.Page
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
                bindPending();
            }
        }
        public void bindPending()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

           


            string SQOCommad = " select InvoiceNO as 'Receipt' , (Win_tbl_customer.Name + ' - ' + Win_tbl_customer.NameArabic) as 'Customer'  ,OrderWay as 'Order Way' , OrderStutas as Status, SUM(total) as Total , Win_sales_item.sales_id as id " +
                               " from Win_sales_item left JOIN Win_tbl_customer on Win_sales_item.c_id = Win_tbl_customer.id and Win_sales_item.TenentID = Win_tbl_customer.TenentID  where Win_sales_item.TenentID =" + TID + "  and PaymentMode = 'Draft' " +
                               " and Win_sales_item.sales_time between  '" + stdate + "'  and '" + etdate + "' " +
                               " group by Win_sales_item.sales_id,InvoiceNO,Win_tbl_customer.Name, Win_tbl_customer.NameArabic,OrderWay,OrderStutas,sales_time " +
                               " order by win_sales_item.sales_time ";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            //   DataTable dt = DataCon.GetDataTable(SQOCommad);
            ListView1.DataSource = ds.Tables[0];
            ListView1.DataBind();

            //select  InvoiceNO as 'Receipt' , (tbl_customer.Name ||' - '|| tbl_customer.NameArabic) as 'Customer'  ,OrderWay as 'Order Way' , OrderStutas as Status, SUM(total) as Total , sales_item.sales_id as id  from sales_item  left JOIN tbl_customer on sales_item.c_id = tbl_customer.id   where sales_item.TenentID=9000003  and PaymentMode = 'Draft' and sales_item.sales_time between  '2018-08-18'  and '2018-08-25'  group by sales_item.sales_id order by sales_item.sales_time

            //List<Database.Win_sales_item> List = DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_time == SDT && p.sales_time == EDT && p.PaymentMode == "Draft").ToList();
            //List = List.GroupBy(p => p.sales_id).Select(p => p.FirstOrDefault()).OrderBy(p => p.sales_time).ToList();

            //ListView1.DataSource = List;
            //ListView1.DataBind();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindPending();
        }

        public string Getname(string id)
        {
            long CID = Convert.ToInt64(id);

            if(DB.Win_tbl_customer.Where(p=>p.TenentID == TID && p.ID == CID).Count() > 0)
            {
                Database.Win_tbl_customer obj = DB.Win_tbl_customer.Single(p => p.TenentID == TID && p.ID == CID);
                string name = obj.Name + " - " + obj.NameArabic;
                return name;
            }
            else
            {
                return "Opps Something Wrong";
            }
        }
        public string getTot(string ID)
        {
            var Total = DB.Win_sales_item.Where(p => p.TenentID == TID && p.InvoiceNO == ID);
            string ToT = Total.Sum(p => p.Total).ToString();
            return ToT;
        }




    }
}