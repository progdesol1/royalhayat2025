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
    public partial class ReportCategory : System.Web.UI.Page
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
            }
        }
        public void BindData()
        {

            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");



            string SQOCommad = " select Top(10) pi.Tenentid, pi.category_arabic as Name, sum(QTY) as QTY from Win_sales_item si left " +
                               " join Win_purchase pi ON pi.product_id = si.itemcode and si.Tenentid = pi.Tenentid " +
                             " where si.Tenentid ='" + TID + "' and pi.Tenentid = '" + TID + "' and si.sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                             " and si.status != 2 group by pi.Tenentid, pi.category_arabic order by QTY Desc";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }


        public void Binddatas()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

            string SQOCommad = " select Top(10) pi.Tenentid,  pi.category_arabic as Name, sum(Total) as Total" +
                              " from Win_sales_item si left " +
                              " join Win_purchase pi ON pi.product_id = si.itemcode and si.Tenentid = pi.Tenentid " +
                              " where si.Tenentid = '" + TID + "' and pi.Tenentid ='" + TID + "' and si.sales_time BETWEEN ' " +stdate + "' AND    '" +  etdate + "' and si.status != 2" +
                              " group by pi.Tenentid,pi.category_arabic order by Total Desc ";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView2.DataSource = dt;
            ListView2.DataBind();

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindData();
            Binddatas();
        }
        public decimal Gettotal(string ItemName)
        {
            decimal total = 0;
            List<Database.Win_sales_item> List = DB.Win_sales_item.Where(m => m.TenentID == TID && m.itemName == ItemName).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.Qty));
            }
            return total;
        }

        public decimal Getsum(string ItemName)
        {
            decimal total = 0;
            List<Database.Win_sales_item> List = DB.Win_sales_item.Where(m => m.TenentID == TID && m.itemName == ItemName).ToList();
            if (List.Count() > 0)
            {
                total = Convert.ToDecimal(List.Sum(m => m.Total));
            }
            return total;
        }


    }
}