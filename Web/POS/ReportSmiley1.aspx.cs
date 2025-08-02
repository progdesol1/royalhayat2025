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
    public partial class ReportSmiley1 : System.Web.UI.Page
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
                bindterminal();
               
            }
        }

        public void bindterminal()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            drpfrmterminal.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal");
            drpfrmterminal.DataTextField = "SHORTNAME";
            drpfrmterminal.DataValueField = "REFID";
            drpfrmterminal.DataBind();
            drpfrmterminal.Items.Insert(0, new ListItem("         -- Select Terminal --          ", "0"));


            drptoterminal.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal");
            drptoterminal.DataTextField = "SHORTNAME";
            drptoterminal.DataValueField = "REFID";
            drptoterminal.DataBind();
            drptoterminal.Items.Insert(0, new ListItem("         -- Select Smiley --          ", "0"));

            //drpfrmsmiley.DataSource = DB.ServeyTables.Where(p => p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal");
            //drpfrmsmiley.DataTextField = "SHORTNAME";
            //drpfrmsmiley.DataValueField = "REFID";
            //drpfrmsmiley.DataBind();
            //drpfrmsmiley.Items.Insert(0, new ListItem("         -- Select Smiley --          ", "0"));

        }

     
      public void binddata()
        {
            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtEnddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");
            DateTime end = Convert.ToDateTime(txtstartdate.Text);

            string SQOCommad = " select ID as 'Smiley No' , ResultY as 'Yellow' , ResultG as 'Green' , ResultR as 'Red' , TerminalID as 'TerminalID' , SurDateTime as 'Date' from ServeyTable" +
                                  " where TenentID = " + TID + " and SurDateTime BETWEEN '" + stdate + "' AND  ' " + etdate + " ' and TerminalID BETWEEN  '"+ drpfrmterminal.SelectedValue+"' AND '"+drptoterminal.SelectedValue+"'";

            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblTotal = (Label)e.Item.FindControl("lblTotal");
            Label discount = (Label)e.Item.FindControl("Label20");
            Label lblcgst = (Label)e.Item.FindControl("lblcgst");
            Label lblsgst = (Label)e.Item.FindControl("lblsgst"); 
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