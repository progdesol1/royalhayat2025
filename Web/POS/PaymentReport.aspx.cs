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
    public partial class PaymentReport : System.Web.UI.Page
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

      
        public void CustBind()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);
       
            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");

            string SQOCommad = "select InvoiceNO as 'RptNo' , sales_time as 'Date' , sum(payment_amount) as 'Total' , emp_id as 'Sold by',  sum(dis) as 'Dis' ,  sum(vat) as 'TAX' ," +
                                 "payment_type as 'Pay type' ,  sum(due_amount) as 'Due', Customer as 'Customer' , Comment as 'Comments'" +
                                     " from Win_sales_payment where TenentID = " + TID + " and sales_time BETWEEN '" + stdate + "' AND    '" + etdate + "' and PaymentStutas = 'Success' and return_id = 0" +
                                     "group by sales_id,InvoiceNO,sales_time,emp_id,Customer,payment_type,Comment order by sales_time";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];


            ListView1.DataSource = dt; //DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_time == txtstartdate.Text && p.sales_time == txtEnddate.Text);
            ListView1.DataBind();
            decimal finalTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTotal = finalTotal + Convert.ToDecimal(dt.Rows[i]["Total"]);
            }
            lblFinalTotal.Text = finalTotal.ToString();

            decimal finaldis = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finaldis = finaldis + Convert.ToDecimal(dt.Rows[i]["Dis"]);
            }
            lbldis.Text = finaldis.ToString();

            decimal finalTAX = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTAX = finalTAX + Convert.ToDecimal(dt.Rows[i]["TAX"]);
            }
            lblTAX.Text = finalTAX.ToString();

            //decimal finalsalesTax = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    finalsalesTax = finalsalesTax + Convert.ToDecimal(dt.Rows[i]["Total" + "TAX"]);
            //}
            //lblsata.Text = finalsalesTax.ToString();

            decimal finalDue = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalDue = finalDue + Convert.ToDecimal(dt.Rows[i]["Due"]);
            }
            lbldue.Text = finalDue.ToString();
            lblfromdate.Text = stdate;
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