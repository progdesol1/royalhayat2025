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
    public partial class slowmoving : System.Web.UI.Page
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

            }
        }

        public void BindData()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");


           



            string SQOCommad = " select Top(10)  itemName as 'Name', SUM(Total) as 'Total' " +
                               " from Win_sales_item where Tenentid = " + TID + " and sales_time between  '" + startdate + "'  and  '" + Enddate + "'  and(status = 1   or status = 3) " +
                               " GROUP BY    itemName order by  SUM(Total) asc ";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView2.DataSource = dt;
            ListView2.DataBind();

        }


        public void Binddatas()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

           string SQOCommad = "  select Top(10)   itemName as 'Name', SUM(Qty) as 'QTY' " +
                              " from win_sales_item  where Tenentid = " + TID + " and sales_time   between  '" + stdate + "'  and '" + etdate + "'  " +
                               " and(status = 1  or status = 3)  GROUP BY    itemName order by  SUM(Qty)  asc ";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView1.DataSource = dt;
            ListView1.DataBind();

            //List<Database.Win_sales_item> List = DB.Win_sales_item.Where(m => m.TenentID == TID && m.sales_time == startdate && m.sales_time == Enddate && (m.status == 1 || m.status == 3)).GroupBy(m => m.itemName).Select(m => m.FirstOrDefault()).OrderBy(p => p.Qty).ToList();
            //ListView2.DataSource = List.Take(10);
            //ListView2.DataBind();
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