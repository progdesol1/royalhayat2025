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
    public partial class salesreport : System.Web.UI.Page
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
              //  Clear();
            }
        }

        public void CustBind()
        {

            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

           

            string SQOCommad = " select InvoiceNO as 'RptNo' , sales_time as 'Date' , (CustItemCode + ' - ' + itemName) as 'Name'," +
                               " UOM, sum(RetailsPrice) as 'Price', Qty,  Total,   ((profit * Qty) * 1.00) as 'Profit'   , sum(discount) as 'Dis Rate', Customer" +
                                " from Win_sales_item where TenentID = " + TID + " and sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "'  and status != 2" +
                                " group by sales_time, InvoiceNO,CustItemCode,itemName,Total,Qty, profit, UOM,Customer" +
                                " Order  by sales_time";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];


            ListView1.DataSource = dt; //DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_time == txtstartdate.Text && p.sales_time == txtEnddate.Text);
            ListView1.DataBind();

            

            decimal finalqty = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalqty = finalqty + Convert.ToDecimal(dt.Rows[i]["Qty"]);
            }
            lblqty.Text = finalqty.ToString();

            decimal finaltotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finaltotal = finaltotal + Convert.ToDecimal(dt.Rows[i]["Total"]);
            }
            lbltotal.Text = finaltotal.ToString();


            decimal finalpro = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalpro = finalpro + Convert.ToDecimal(dt.Rows[i]["Profit"]);
            }
            lblpro.Text = finalpro.ToString();



            lblfromdate.Text = stdate;
            lbltodate.Text = etdate;



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