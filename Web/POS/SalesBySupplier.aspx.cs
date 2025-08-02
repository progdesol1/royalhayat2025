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

namespace Web
{
    public partial class SalesBySupplier : System.Web.UI.Page
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
                BindDropdown();
            }
        }

        public void CustBind()
        {

            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

            if(drpcustomer.SelectedValue == "0")
            {             

                string SQOCommad = " select si.InvoiceNO as 'RptNo' , si.sales_time as 'Date' ,(si.CustItemCode + ' - ' + si.itemName) as 'Name',si.UOM, si.RetailsPrice as 'Price', " +
                                   " si.Qty,  Total,  ((si.profit * si.Qty) * 1.00) as 'Profit'   , si.discount as 'Dis Rate',  si.Customer, pi.category" +
                                    " from Win_sales_item si left join Win_purchase pi ON pi.product_id = si.itemcode " +
                                    " where si.TenentID =  " + TID + " and si.sales_time BETWEEN '+ stdate +' AND    '" + etdate + "'   and si.status != 2 " +
                                    " Order by si.sales_time ";
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

                string SQOCommad = " select si.InvoiceNO as 'RptNo' , si.sales_time as 'Date' ,(si.CustItemCode + ' - ' + si.itemName) as 'Name'," +
                                    " si.UOM, si.RetailsPrice as 'Price', si.Qty,  Total,  ((si.profit * si.Qty) * 1.00) as 'Profit'   , si.discount as 'Dis Rate', " +
                                    " si.Customer,pi.category " +
                                       " from Win_sales_item si left join Win_purchase pi ON pi.product_id = si.itemcode and pi.TenentID = si.TenentID " +
                                " where si.TenentID = " + TID + " and si.sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "'   and si.status != 2  and pi.supplier = 'Crepiano' " +
                                " Order by si.sales_time ";
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

        public void BindDropdown()
        {
            drpcustomer.DataSource = DB.Win_purchase.Where(p => p.TenentID == TID).GroupBy(p => p.supplier).FirstOrDefault().Take(1);
            drpcustomer.DataTextField = "supplier";
            drpcustomer.DataValueField = "supplier";
            drpcustomer.DataBind();
            drpcustomer.Items.Insert(0, new ListItem("--All supplier--", "0"));
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            CustBind();
        }
    }
}