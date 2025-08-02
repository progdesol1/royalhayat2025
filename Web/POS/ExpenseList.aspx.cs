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
    public partial class ExpenseList : System.Web.UI.Page
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
                BindDropdown();
                CustBind();
            }
        }

      
        public void CustBind()
        {

            string SQOCommad = " select  ID, Date , ReferenceNo as 'Refer No' , Category ,	Amount , Note ,	Createdby as 'Posted by', Attachment , fileextension from Win_tbl_expense where TenentID = " + TID + " ";

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
            //txtstartdate.Text = "";
            //txtEnddate.Text = "";
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