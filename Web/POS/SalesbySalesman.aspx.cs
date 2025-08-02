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
    public partial class SalesbySalesman : System.Web.UI.Page
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
                BindDropdown();
            }
        }

        public void CustBind()
        {

            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

            if(drpcustomer.SelectedValue=="0")
            {
                string SQOCommad = " select  InvoiceNO as 'RptNo' , sales_time as 'Date' , ( CustItemCode + ' - ' +  itemName) as 'Name',UOM, RetailsPrice  as 'Price', Qty,  Total,  " +
                                        " ((profit * Qty) * 1.00) as 'Profit'   , discount as 'Dis Rate', Customer " +
                                        " from Win_sales_item where TenentID = " + TID + " and  sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                                        " and status != 2  Order  by sales_time";

                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];
                ListView1.DataSource = dt;
                ListView1.DataBind();
            }

            else
            {
                string SQOCommad = " select  InvoiceNO as 'RptNo' , sales_time as 'Date' , ( CustItemCode + ' - ' +  itemName) as 'Name',UOM, RetailsPrice  as 'Price', Qty,  Total,  " +
                                        " ((profit * Qty) * 1.00) as 'Profit'   , discount as 'Dis Rate', Customer " +
                                        " from Win_sales_item where TenentID = " + TID + " and sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "' and SoldBy='" + drpcustomer.SelectedItem + "'  " +
                                        " and status != 2  Order  by sales_time";

                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];
                ListView1.DataSource = dt;
                ListView1.DataBind();
            }
           
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

        public void BindDropdown()
        {
            drpcustomer.DataSource = DB.Win_sales_item.Where(p => p.TenentID == TID).GroupBy(p => p.SoldBy).FirstOrDefault().Take(1);
            drpcustomer.DataTextField = "SoldBy";
            drpcustomer.DataValueField = "SoldBy";
            drpcustomer.DataBind();
            drpcustomer.Items.Insert(0, new ListItem("--All SalesMan--", "0"));
        }

    }
}