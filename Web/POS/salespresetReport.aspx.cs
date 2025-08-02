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
    public partial class salespresetReport : System.Web.UI.Page
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
              Clear();
            }
        }

        public void CustBind()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");
            

            string SQOCommad = " select si.InvoiceNO as 'RptNo' , si.sales_time as 'Date' , si.itemName as 'Name',si.UOM, si.RetailsPrice as 'Price', si.Qty, " +
                               " Total,   ((si.profit * si.Qty) * 1.00) as 'Profit'   , si.discount as 'Dis Rate',   si.Customer" +
                            " from Win_sales_item si   where si.TenentID = " + TID + " and si.sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "'    and si.status != 2   and si.discount != 0" +
                            " group by si.sales_time, si.InvoiceNO,si.itemName,si.UOM,si.RetailsPrice,si.Qty,si.Total,si.profit,si.discount,si.Customer" +
                            " Order  by si.sales_time";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            ListView1.DataSource = dt;  //DB.Win_sales_item.Where(p=> p.TenentID==TID && p.sales_time == txtstartdate.Text && p.sales_time == txtEnddate.Text);
            ListView1.DataBind();



        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            CustBind();
        }


        public void Clear()
        {
            txtstartdate.Text = "";
            txtEnddate.Text = "";
        }
    }
}