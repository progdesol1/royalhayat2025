using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web.ReportMst
{
    public partial class ReportSmile : System.Web.UI.Page
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
                txtdateTO.Text = EDT.ToString("MM/dd/yyyy");
                DateTime SDT = EDT.AddMonths(-1);
                txtdateFrom.Text = SDT.ToString("MM/dd/yyyy");
                bindterminals();
                bindterminal();
            }
        }
  
        //public void bindterminal()
        //{
        //    TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    drpfrmterminal.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal").FirstOrDefault();
        //    drpfrmterminal.DataTextField = "SHORTNAME";
        //    drpfrmterminal.DataValueField = "REFID";
        //    drpfrmterminal.DataBind();
        //    drpfrmterminal.Items.Insert(0, new ListItem("         -- Select Terminal --          ", "0"));


        //    drptoterminal.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal").FirstOrDefault();
        //    drptoterminal.DataTextField = "SHORTNAME";
        //    drptoterminal.DataValueField = "REFID";
        //    drptoterminal.DataBind();
        //    drptoterminal.Items.Insert(0, new ListItem("         -- Select Terminal --          ", "0"));




        //}

        public void bindterminal()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select *  " +
                                    " from  REFTABLE " +
                                    " where TenentID =" + TID + " and REFTYPE = 'Survey' and REFSUBTYPE = 'SurveyIDTerminal' " +
                                    " order by REFID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpfrmterminal.DataSource = dt;
            drpfrmterminal.DataTextField = "SHORTNAME";
            drpfrmterminal.DataValueField = "REFID";
            drpfrmterminal.DataBind();
            //     drpdepartmentfrom.Items.Insert(0, new ListItem("--All Department--", "0"));

        }

        public void bindterminals()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select *  " +
                                    " from  REFTABLE " +
                                    " where TenentID =" + TID + " and REFTYPE = 'Survey' and REFSUBTYPE = 'SurveyIDTerminal' " +
                                    " order by REFID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drptoterminal.DataSource = dt;
            drptoterminal.DataTextField = "SHORTNAME";
            drptoterminal.DataValueField = "REFID";
            drptoterminal.DataBind();
            //     drpdepartmentfrom.Items.Insert(0, new ListItem("--All Department--", "0"));

        }

        public void binddata()
        {
            DateTime startdate = Convert.ToDateTime(txtdateFrom.Text);
            DateTime Enddate = Convert.ToDateTime(txtdateTO.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");
            DateTime end = Convert.ToDateTime(txtdateTO.Text);

            string SQOCommad = " select ID as 'Smiley No' , sum(ResultY) as 'Yellow' , sum(ResultG) as 'Green' , sum(ResultR) as 'Red' , TerminalID as 'TerminalID' , SurDateTime as 'Date' from ServeyTable" +
                                  " where TenentID = " + TID + " and SurDateTime BETWEEN '" + stdate + "' AND  ' " + etdate + " ' and TerminalID BETWEEN  '" + drpfrmterminal.SelectedValue + "' AND '" + drptoterminal.SelectedValue + "'" +
                                  " group by ID , TerminalID , SurDateTime ";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView1.DataSource = dt;
            ListView1.DataBind();

            int finalTotalGreen = 0;
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string g = dt.Rows[i]["Green"].ToString() == "" ? "0" : dt.Rows[i]["Green"].ToString();
                finalTotalGreen = finalTotalGreen + Convert.ToInt32(g);
            }
            lblFinalTotalGreen.Text = finalTotalGreen.ToString();

            int finalTotalyellow = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string y = dt.Rows[i]["Yellow"].ToString() == "" ? "0" : dt.Rows[i]["Yellow"].ToString();
                finalTotalyellow = finalTotalyellow + Convert.ToInt32(y);
            }
            lblFinalTotalyellow.Text = finalTotalyellow.ToString();

            int finalTotalred = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string r = dt.Rows[i]["Red"].ToString() == "" ? "0" : dt.Rows[i]["Red"].ToString();
                finalTotalred = finalTotalred + Convert.ToInt32(r);
            }
            lblFinalTotalRed.Text = finalTotalred.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            binddata();
        }

        public string getTerminalname(int TerminalID)
        {
            if (TerminalID == 0 || TerminalID == null)
            {
                return null;
            }
            else
            {
                return DB.REFTABLEs.SingleOrDefault(p => p.REFID == TerminalID && p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal").SHORTNAME;
            }

        }

    }
}