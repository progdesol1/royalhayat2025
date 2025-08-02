using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Web.ReportMst
{
    public partial class Productiom_AddedStock : System.Web.UI.Page
    {
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        CallEntities DB = new CallEntities();

        int TID = 0;
        int LID = 1;
        DateTime DT = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            if (!IsPostBack)
            {
                DropDate();
                BindList();
            }
        }


        public void DropDate()
        {
            DateTime MaxDate = Convert.ToDateTime(DB.View_PlannedMealSum.Where(p => p.TenentID == TID).Max(p => p.ExpectedDelDate));

            string SQOCommad = "select ExpectedDelDate from View_PlannedMealSum where TenentID=" + TID + " and ExpectedDelDate between '" + DT + "' and '" + MaxDate + "' group by ExpectedDelDate order by ExpectedDelDate;";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            Dateformate.DataSource = dt;
            Dateformate.DataTextField = "ExpectedDelDate";
            Dateformate.DataValueField = "ExpectedDelDate";
            Dateformate.DataBind();


            if (dt.Rows.Count > 0)
                Dateformate.SelectedValue = DT.ToString();
            else
                Dateformate.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public void BindList()
        {
            DateTime? EXDate = null;
            if (Dateformate.SelectedValue != "0")
            {
                EXDate = Convert.ToDateTime(Dateformate.SelectedValue);
                List<Database.ICTR_HD> HDList = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate).ToList();
                List<Database.ICTR_DT> DTList = new List<Database.ICTR_DT>();
                if (HDList.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID).Count() > 0)
                {
                    Database.ICTR_HD HDobj = HDList.Single(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID);
                    int mytranceid = Convert.ToInt32(HDobj.MYTRANSID);
                    DTList = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.locationID == LID && p.EXPIRYDATE == EXDate && p.Stutas == "Accepted").ToList();
                }
                ListView1.DataSource = DTList;
                ListView1.DataBind();
            }
        }

        protected void Dateformate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }

        public string GetProd(int PID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
            {
                string ProdName = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID).ProdName1;
                return ProdName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetUOM(int UID)
        {
            if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UID).Count() > 0)
            {
                string UOMname = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == UID).UOMNAME1;
                return UOMname;
            }
            else
            {
                return "Not Found";
            }
        }





    }
}