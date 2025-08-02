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
    public partial class Kitchenreport : System.Web.UI.Page
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
                // CustBind();
                Clear();
            }
        }

      
        public void CustBind()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);
       
            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");



            string SQOCommad = " SELECT si.InvoiceNO, si.sales_time as 'Date',   sp.emp_id ,  CASE WHEN si.status = 3 THEN 'Pending'  WHEN si.status = 1 THEN 'Served'   END 'Status' " +
                               " FROM Win_sales_item si left join Win_sales_payment sp ON si.sales_id = sp.sales_id and si.TenentID = sp.TenentID " +
                               " left join Win_purchase p ON p.product_id = si.itemcode and p.TenentID = si.TenentID " +
                               " left join  Win_tbl_item_uom_price tiu  ON tiu.itemID = si.itemcode and tiu.TenentID = si.TenentID " +
                               " where si.status = 3 and si.sales_time between '" + stdate + "' and  '" + etdate + "'  and si.Qty != 0 and si.paymentmode != 'Draft' and si.TenentID = " + TID + "" +
                               " group by si.sales_id,si.InvoiceNO,si.sales_time,sp.emp_id,si.status,si.item_id order by si.item_id asc ";

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
            txtstartdate.Text = "";
            txtEnddate.Text = "";
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            CustBind();
        }
    }
   }