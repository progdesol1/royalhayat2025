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
    public partial class Stockdetails : System.Web.UI.Page
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
                //DateTime EDT = DateTime.Now;
                //txtEnddate.Text = EDT.ToString("MM/dd/yyyy");
                //DateTime SDT = EDT.AddMonths(-1);
                //txtstartdate.Text = SDT.ToString("MM/dd/yyyy");
                CustBind();
                Clear();
            }
        }

      
        public void CustBind()
        {

         

            string SQOCommad = " select  product_id as 'Item Code' , product_name as 'Item Name' , UOMID as 'Uint Of Masuare' , OnHand as 'Quantity',  " +
                             " price as 'Buy Price' , msrp as 'Sales Price' ,  category as 'Category' , supplier as 'Supplier'   " +
                             " FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID  where Win_purchase.TenentID = " + TID +" order by OnHand ";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];


            ListView1.DataSource = dt; //DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_time == txtstartdate.Text && p.sales_time == txtEnddate.Text);
            ListView1.DataBind();
        }

        public void Clear()
        {
            txtsearch.Text = "";
            
        }

       

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string SQOCommad = " select  product_id as 'Item Code' , product_name as 'Item Name', UOMID as 'Uint Of Masuare' , OnHand as 'Quantity',  " +
                              " price as 'Buy Price' , msrp as 'Sales Price' ,   category as 'Category' , supplier as 'Supplier'   " +
                              " FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID where Win_purchase.TenentID =  " + TID + " and  product_id like  '" + txtsearch.Text + "%' or product_name like '" + txtsearch.Text + "%' or " +
                              " category like '" + txtsearch.Text + "%' or supplier like '" + txtsearch.Text + "%'";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];


            ListView1.DataSource = dt; //DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_time == txtstartdate.Text && p.sales_time == txtEnddate.Text);
            ListView1.DataBind();
        }
    }
   }