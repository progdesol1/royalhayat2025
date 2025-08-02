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
    public partial class PurchaseHistory : System.Web.UI.Page
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
                //CustBind();
                //Clear();
                BindDropdown();
            }
        }

      
        public void CustBind()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

            if (drpcustomer.SelectedValue == "0")
            {
                string SQOCommad = " select id as ID, purchase_date as 'Date', pi.CustItemCode , phs.product_name as 'Name',phs.product_quantity as 'Qty' ,Cost_price as 'Cost Price',retail_price as 'Retail price', " +
                         " phs.category as 'Category' , phs.supplier , phs.Shopid , ptype as 'Product Type'  " +
                         " from Win_tbl_purchase_history phs INNER join Win_purchase pi ON pi.product_id = phs.product_id and pi.TenentID = phs.TenentID " +
                         " where pi.TenentID = " + TID + " and purchase_date   BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                         " and phs.status = 1 Order  by id desc";
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
                string SQOCommad = " select id as ID, purchase_date as 'Date', pi.CustItemCode , phs.product_name as 'Name',phs.product_quantity as 'Qty' ,Cost_price as 'Cost Price',retail_price as 'Retail price', " +
                         " phs.category as 'Category' , phs.supplier , phs.Shopid , ptype as 'Product Type'  " +
                         " from Win_tbl_purchase_history phs INNER join Win_purchase pi ON pi.product_id = phs.product_id and pi.TenentID = phs.TenentID " +
                         " where pi.TenentID = " + TID + " and purchase_date   BETWEEN '" + stdate + "' AND    '" + etdate + "' and pi.CustItemCode = '" + drpcustomer.SelectedItem + "' " +
                         " and phs.status = 1 Order  by id desc";
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
            
            string sql = " select (CustItemCode + ' - ' + pi.product_name) as 'product_name' " +
                            " from Win_purchase pi INNER join Win_tbl_purchase_history phs ON phs.product_id = pi.product_id and  phs.TenentID = pi.TenentID" +
                            " where pi.TenentID = " + TID + "" +
                            " group by pi.CustItemCode,pi.product_name";
            DataTable dt = DataCon.GetDataTable(sql);
            drpcustomer.DataSource = dt;
            drpcustomer.DataTextField = "product_name";
            drpcustomer.DataValueField = "product_name";
            drpcustomer.DataBind();
            drpcustomer.Items.Insert(0, new ListItem("--All items--", "0"));
        }

    }
   }