using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;
using System.Web.Services;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;



namespace Web.Master
{
    public partial class GYMIIndex : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<ICTR_HD> ListOFHd = new List<ICTR_HD>();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                TODAYDate.Text = DateTime.Now.ToString("dd/MMM/yy");
                var Date = DateTime.Now.ToShortDateString();
                int ToDay = Convert.ToInt32(DateTime.Now.Day);
                int ToMonth = Convert.ToInt32(DateTime.Now.Month);
                int ToYear = Convert.ToInt32(DateTime.Now.Year);

                List<Database.ICTR_HD> itemList = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.MYSYSNAME == "GYM" && p.ENTRYDATE.Value.Day == ToDay && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT1 = Convert.ToDecimal(itemList.Sum(p => p.TOTAMT));
                lblbox1KD.Text = TOT1.ToString();
                //Convert.ToDateTime(objAPP[i].CompletionDate).ToString("dd/MM/yyyy")

                //Sale This Month
                List<Database.ICTR_HD> itemListmonth = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.MYSYSNAME == "GYM" && p.ENTRYDATE.Value.Month == ToMonth).ToList();
                decimal TOT2 = Convert.ToDecimal(itemListmonth.Sum(p => p.TOTAMT));
                lblbox2KD.Text = TOT2.ToString();
                string Month = DateTime.Now.ToString("MMM");
                Thismonth.Text = Month;

                //Sale This Year
                List<Database.ICTR_HD> itemListYear = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.MYSYSNAME == "GYM" && p.ENTRYDATE.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT3 = Convert.ToDecimal(itemListYear.Sum(p => p.TOTAMT));
                lblbox3KD.Text = TOT3.ToString();
                string Year = DateTime.Now.ToString("yyyy");
                ThisYear.Text = Year;

                ////Co-op Sale This Year
                //string CompanyType = GEt_REFID("COMP", "COMPTYPE", "Co-op Society").ToString();
                //int ProdTypeRefId = GEt_REFID("PRODTYPE", "PRODTYPE", "Co-op Society");
                //List<Database.View_Banana> itemCo_opListYear = DB.View_Banana.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4151 && p.transsubid == 415101 && p.ENTRYDATE.Value.Year == ToYear && p.CompanyType == CompanyType && p.ProdTypeRefId == ProdTypeRefId).ToList();
                //// Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                //decimal CO_OPTOT3 = Convert.ToDecimal(itemCo_opListYear.Sum(p => p.AMOUNT));
                //lblCo_opsales.Text = CO_OPTOT3.ToString();
                //string CO_OPYear = DateTime.Now.ToString("yyyy");
                //lblCo_opSalesYear.Text = CO_OPYear;

                ////Chapra Sale This Year
                //string CompanyTypeChapra = GEt_REFID("COMP", "COMPTYPE", "Chapra").ToString();
                //int ProdTypeRefIdchapra = GEt_REFID("PRODTYPE", "PRODTYPE", "Chapra");
                //List<Database.View_Banana> itemChapraListYear = DB.View_Banana.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4151 && p.transsubid == 415101 && p.ENTRYDATE.Value.Year == ToYear && p.CompanyType == CompanyTypeChapra && p.ProdTypeRefId == ProdTypeRefIdchapra).ToList();
                //// Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                //decimal ChapraTOT3 = Convert.ToDecimal(itemChapraListYear.Sum(p => p.AMOUNT));
                //lblChapra.Text = ChapraTOT3.ToString();
                //string ChapraYear = DateTime.Now.ToString("yyyy");
                //lblChaprayear.Text = ChapraYear;

                ////bakala Sale This Year
                //string CompanyTypebakala = GEt_REFID("COMP", "COMPTYPE", "Grocery Shop").ToString();
                //int ProdTypeRefIdbakala = GEt_REFID("PRODTYPE", "PRODTYPE", "Grocery Shop");
                //List<Database.View_Banana> itembakalaListYear = DB.View_Banana.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4151 && p.transsubid == 415101 && p.ENTRYDATE.Value.Year == ToYear && p.CompanyType == CompanyTypebakala && p.ProdTypeRefId == ProdTypeRefIdbakala).ToList();
                //// Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                //decimal bakalaTOT3 = Convert.ToDecimal(itembakalaListYear.Sum(p => p.AMOUNT));
                //lblChapra.Text = bakalaTOT3.ToString();
                //string bakalaYear = DateTime.Now.ToString("yyyy");
                //lblChaprayear.Text = bakalaYear;

                //sale Return this day
                List<Database.ICTR_HD> itemListYearReturn = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.MYSYSNAME == "GYM" && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT4 = Convert.ToDecimal(itemListYearReturn.Sum(p => p.TOTAMT));
                lblbox4KD.Text = "0";
                string returnyear = DateTime.Now.ToString("yyyy");
                ThisYearReturn.Text = returnyear;

                //purchase this days
                List<Database.ICTR_HD> itemListDatePurchase = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.MYSYSNAME == "GYM" && p.ENTRYDATE.Value.Day == ToDay && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT5 = Convert.ToDecimal(itemListDatePurchase.Sum(p => p.TOTAMT));
                lblbox5KD.Text = "0";
                TodayDatePurchase.Text = DateTime.Now.ToString("dd/MMM/yy");


                //purchase this Month
                List<Database.ICTR_HD> itemListMonthPurchase = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.MYSYSNAME == "GYM" && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT6 = Convert.ToDecimal(itemListMonthPurchase.Sum(p => p.TOTAMT));
                lblbox6KD.Text = "0";
                string MonthPur = DateTime.Now.ToString("MMM");
                ThisMonthPurchase.Text = MonthPur;

                //purchase this Year
                List<Database.ICTR_HD> itemListYearPurchase = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT7 = Convert.ToDecimal(itemListYearPurchase.Sum(p => p.TOTAMT));
                lblbox7KD.Text = "0";
                string Puryear = DateTime.Now.ToString("yyyy");
                ThisYearPurchase.Text = Puryear;

                //purchase Return year
                List<Database.ICTR_HD> itemListYearPurchaseReturn = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 2102 && p.transsubid == 21012 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT8 = Convert.ToDecimal(itemListYearPurchaseReturn.Sum(p => p.TOTAMT));
                lblbox8KD.Text = "0";
                string Purreturnyear = DateTime.Now.ToString("yyyy");
                ThisYearPurchaseReturn.Text = Purreturnyear;

            }

        }

        public void SessionLoad()
        {
            //string Ref = ((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            //string Ref = TID.ToString() + "," + LID.ToString() + "," + UID.ToString() + "," + EMPID.ToString() + "," + LangID;
            //return Ref;

            //string[] id = Ref.Split(',');
            //TID = Convert.ToInt32(id[0]);
            //LID = Convert.ToInt32(id[1]);
            //UID = Convert.ToInt32(id[2]);
            //EMPID = Convert.ToInt32(id[3]);
            //LangID = id[4].ToString();
            //userID1 = ((USER_MST)Session["USER"]).USER_ID;
            //userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }




    }
}