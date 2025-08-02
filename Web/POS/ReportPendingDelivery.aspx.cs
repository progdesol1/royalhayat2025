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
    public partial class ReportPendingDelivery : System.Web.UI.Page
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
                DeleBind();
            }
        }
        public void DeleBind()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");




            string SQOCommad = "  SELECT si.InvoiceNO as 'Receipt' ,  si.orderTotal as 'Order Amount' ,  " +
                               " si.sales_time as 'Date', CASE WHEN si.driver = '0' THEN ''   WHEN si.driver != '0' THEN si.driver " +
                               " END 'driver' , CASE WHEN si.COD = 0 THEN 'Paid'   WHEN si.COD = '1' THEN 'COD'    END 'COD' , CASE " +
                               " WHEN si.OrderStutas = 'Paid-Ready to Delivery' THEN 'Pending'  WHEN si.orderStutas = 'COD-Ready to Delivery' THEN 'Pending' " +
                               " WHEN si.OrderStutas = 'Paid - Delivered' THEN 'Deliverd' " +
                               " WHEN si.orderStutas = 'Deliverd & Cash Recived' THEN 'Deliverd' " +
                               " WHEN si.orderStutas = 'Deliverd' THEN 'Deliverd'  WHEN si.orderStutas = 'Return' THEN 'Return' END 'Status' " +
                               " FROM Win_sales_item si left join Win_sales_payment sp " +
                               " ON si.sales_id = sp.sales_id and si.TenentID = sp.TenentID  left join Win_purchase p  ON p.product_id = si.itemcode " +
                               " and p.TenentID = si.TenentID  left join  Win_tbl_item_uom_price tiu  ON tiu.itemID = si.itemcode and tiu.TenentID = si.TenentID " +
                               " where si.status = 1  and(si.orderStutas != 'Deliverd' and si.orderStutas != 'Deliverd & Cash Recived')  and si.Qty != 0 and " +
                               " si.TenentID = " + TID + " and si.sales_time between  '" + stdate + "'  and '" + etdate + "' " +
                               " group by si.sales_id,si.InvoiceNO,si.orderTotal,si.sales_time,si.driver,si.COD,si.OrderStutas order by si.sales_time asc ";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            //   DataTable dt = DataCon.GetDataTable(SQOCommad);
            ListView1.DataSource = ds.Tables[0];
            ListView1.DataBind();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DeleBind();
        }
        public string GetDriver(string DD)
        {
            if(DD == "0")
            {
                return "";
            }
            else if(DD == null)
            { return ""; }
            else
            {
                return DD;
            }
        }
        public string getStatus(string ID)
        {
            if (ID == "Paid-Ready to Delivery")
            {
                return "Pending";
            }
            else if (ID == "COD-Ready to Delivery")
            {
                return "Pending";
            }
            else if (ID == "Paid - Delivered")
            {
                return "Deliverd";
            }
            else if (ID == "Deliverd & Cash Recived")
            {
                return "Deliverd";
            }
            else if (ID == "Deliverd")
            {
                return "Deliverd";
            }
            else if (ID == "Return")
            {
                return "Return";
            }
            else
            {
                return "";
            }
        }


    }
}