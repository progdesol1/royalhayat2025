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


namespace Web.Sales
{
    public partial class Index : System.Web.UI.Page
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
                //bindMinMum();
                Customer();

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                TODAYDate.Text = DateTime.Now.ToString("dd/MMM/yy");
                var Date = DateTime.Now.ToShortDateString();
                int ToDay = Convert.ToInt32(DateTime.Now.Day);
                int ToMonth = Convert.ToInt32(DateTime.Now.Month);
                int ToYear = Convert.ToInt32(DateTime.Now.Year);

                List<Database.ICTR_HD> itemList = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4101 && p.transsubid == 410103 && p.ENTRYDATE.Value.Day == ToDay && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT1 = Convert.ToDecimal(itemList.Sum(p => p.TOTAMT));
                lblbox1KD.Text = TOT1.ToString();
                //Convert.ToDateTime(objAPP[i].CompletionDate).ToString("dd/MM/yyyy")

                //Sale This Month
                List<Database.ICTR_HD> itemListmonth = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4101 && p.transsubid == 410103 && p.ENTRYDATE.Value.Month == ToMonth).ToList();
                decimal TOT2 = Convert.ToDecimal(itemListmonth.Sum(p => p.TOTAMT));
                lblbox2KD.Text = TOT2.ToString();
                string Month = DateTime.Now.ToString("MMM");
                Thismonth.Text = Month;

                //Sale This Year
                List<Database.ICTR_HD> itemListYear = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4101 && p.transsubid == 410103 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT3 = Convert.ToDecimal(itemListYear.Sum(p => p.TOTAMT));
                lblbox3KD.Text = TOT3.ToString();
                string Year = DateTime.Now.ToString("yyyy");
                ThisYear.Text = Year;

                //Co-op Sale This Year
                string CompanyType = GEt_REFID("COMP", "COMPTYPE", "Co-op Society").ToString();
                int ProdTypeRefId = GEt_REFID("PRODTYPE", "PRODTYPE", "Co-op Society");

                List<Database.View_Banana> itemCo_opListYear = DB.View_Banana.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4151 && p.transsubid == 415101 && p.ENTRYDATE.Value.Year == ToYear && p.CompanyType == CompanyType && p.ProdTypeRefId == ProdTypeRefId).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal CO_OPTOT3 = Convert.ToDecimal(itemCo_opListYear.Sum(p => p.AMOUNT));
                lblCo_opsales.Text = CO_OPTOT3.ToString();
                string CO_OPYear = DateTime.Now.ToString("yyyy");
                lblCo_opSalesYear.Text = CO_OPYear;

                //Chapra Sale This Year
                string CompanyTypeChapra = GEt_REFID("COMP", "COMPTYPE", "Chapra").ToString();
                int ProdTypeRefIdchapra = GEt_REFID("PRODTYPE", "PRODTYPE", "Chapra");
                List<Database.View_Banana> itemChapraListYear = DB.View_Banana.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4151 && p.transsubid == 415101 && p.ENTRYDATE.Value.Year == ToYear && p.CompanyType == CompanyTypeChapra && p.ProdTypeRefId == ProdTypeRefIdchapra).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal ChapraTOT3 = Convert.ToDecimal(itemChapraListYear.Sum(p => p.AMOUNT));
                lblChapra.Text = ChapraTOT3.ToString();
                string ChapraYear = DateTime.Now.ToString("yyyy");
                lblChaprayear.Text = ChapraYear;

                //bakala Sale This Year
                string CompanyTypebakala = GEt_REFID("COMP", "COMPTYPE", "Grocery Shop").ToString();
                int ProdTypeRefIdbakala = GEt_REFID("PRODTYPE", "PRODTYPE", "Grocery Shop");
                List<Database.View_Banana> itembakalaListYear = DB.View_Banana.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4151 && p.transsubid == 415101 && p.ENTRYDATE.Value.Year == ToYear && p.CompanyType == CompanyTypebakala && p.ProdTypeRefId == ProdTypeRefIdbakala).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal bakalaTOT3 = Convert.ToDecimal(itembakalaListYear.Sum(p => p.AMOUNT));
                lblBakala.Text = bakalaTOT3.ToString();
                string bakalaYear = DateTime.Now.ToString("yyyy");
                lblbakalayear.Text = bakalaYear;

                //sale Return this day
                List<Database.ICTR_HD> itemListYearReturn = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 5101 && p.transsubid == 510101 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT4 = Convert.ToDecimal(itemListYearReturn.Sum(p => p.TOTAMT));
                lblbox4KD.Text = TOT4.ToString();
                string returnyear = DateTime.Now.ToString("yyyy");
                ThisYearReturn.Text = returnyear;

                //purchase this days
                List<Database.ICTR_HD> itemListDatePurchase = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.ENTRYDATE.Value.Day == ToDay && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT5 = Convert.ToDecimal(itemListDatePurchase.Sum(p => p.TOTAMT));
                lblbox5KD.Text = TOT5.ToString();
                TodayDatePurchase.Text = DateTime.Now.ToString("dd/MMM/yy");


                //purchase this Month
                List<Database.ICTR_HD> itemListMonthPurchase = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT6 = Convert.ToDecimal(itemListMonthPurchase.Sum(p => p.TOTAMT));
                lblbox6KD.Text = TOT6.ToString();
                string MonthPur = DateTime.Now.ToString("MMM");
                ThisMonthPurchase.Text = MonthPur;

                //purchase this Year
                List<Database.ICTR_HD> itemListYearPurchase = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                decimal TOT7 = Convert.ToDecimal(itemListYearPurchase.Sum(p => p.TOTAMT));
                lblbox7KD.Text = TOT7.ToString();
                string Puryear = DateTime.Now.ToString("yyyy");
                ThisYearPurchase.Text = Puryear;

                //purchase Return year
                List<Database.ICTR_HD> itemListYearPurchaseReturn = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 2102 && p.transsubid == 21012 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT8 = Convert.ToDecimal(itemListYearPurchaseReturn.Sum(p => p.TOTAMT));
                lblbox8KD.Text = TOT8.ToString();
                string Purreturnyear = DateTime.Now.ToString("yyyy");
                ThisYearPurchaseReturn.Text = Purreturnyear;

            }

        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
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
        public void bindMinMum()
        {
            listMinimumAndMax.DataSource = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && (p.MinQty == 0 || p.MinQty == null)).OrderBy(p => p.MYTRANSID);
            listMinimumAndMax.DataBind();
        }

        public int GEt_REFID(string REFTYPE, string REFSUBTYPE, string SHORTNAME)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.SHORTNAME == SHORTNAME).Count() > 0)
            {
                int REFID = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.SHORTNAME == SHORTNAME).FirstOrDefault().REFID;
                return REFID;
            }
            else
            {
                return 0;
            }
        }

        //protected void listMinimumAndMax_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "btnminqty")
        //    {
        //        string[] ID = e.CommandArgument.ToString().Split(',');
        //        int TenentID = Convert.ToInt32(ID[0]);
        //        int MyProdID = Convert.ToInt32(ID[1]);
        //        string period_code = ID[2];
        //        string MySysName = ID[3];
        //        int UOM = Convert.ToInt32(ID[4]);
        //        int LocationID = Convert.ToInt32(ID[5]);
        //        TextBox txtminqty = (TextBox)e.Item.FindControl("txtminqty");
        //        TextBox txtmaxqty = (TextBox)e.Item.FindControl("txtmaxqty");
        //        TextBox txtleadtime = (TextBox)e.Item.FindControl("txtleadtime");
        //        if (txtminqty.Text == "" || txtminqty.Text == null)
        //        {
        //            panelMsg.Visible = true;
        //            lblErreorMsg.Text = "Please Enter The Minimum Qty";
        //            return;
        //        }
        //        if (txtmaxqty.Text == "" || txtmaxqty.Text == null)
        //        {
        //            panelMsg.Visible = true;
        //            lblErreorMsg.Text = "Please Enter The Maximum Qty";
        //            return;
        //        }
        //        if (txtleadtime.Text == "" || txtleadtime.Text == null)
        //        {
        //            panelMsg.Visible = true;
        //            lblErreorMsg.Text = "Please Enter The Lead Time";
        //            return;
        //        }
        //        ICIT_BR objICIT_BR = DB.ICIT_BR.Single(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.UOM == UOM && p.period_code == period_code && p.MySysName == MySysName && p.LocationID == LocationID);
        //        objICIT_BR.MaxQty = Convert.ToInt32(txtmaxqty.Text);
        //        objICIT_BR.MinQty = Convert.ToInt32(txtminqty.Text);
        //        objICIT_BR.LeadTime = Convert.ToInt32(txtleadtime.Text);
        //        DB.SaveChanges();
        //        bindMinMum();

        //    }
        //}
        public string getproductcode(int PID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == PID && p.TenentID == TID).Count() > 0)
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).ProdName1;
            else
                return "";
        }
        public string getuom(int UID)
        {
            if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.RecTypeID == UID && p.TenentID == TID).Count() > 0)
                return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == UID && p.TenentID == TID).RecValue;
            else
                return DB.ICUOMs.Single(p => p.UOM == UID && p.TenentID == TID).UOMNAME1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //List<Database.tblsetupsalesh> listtblsetupsaleshes = DB.tblsetupsaleshes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
            Database.tblsetupsalesh objtblsetupsalesh = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103);
            //objtblsetupsalesh = listtblsetupsaleshes[0];
            if (objtblsetupsalesh.AllowMinusQty == false)
            {

                int count1 = 0;
                count1 = Classes.EcommAdminClass.BindAddvanserchBRDash(txtsearch.Text, listMinimumAndMax, TID, "en-US");
                lblcount.Text = "Product Found(" + count1.ToString() + ")";
                //string txtserchProduct, Repeater ddlProduct, int TID
            }
            else
            {

                int count1 = 0;
                count1 = Classes.EcommAdminClass.BindAddvanserchDash(txtsearch.Text, listMinimumAndMax, TID);
                lblcount.Text = "Product Found(" + count1.ToString() + ")";
            }

        }
        public void Customer()
        {
            List<ICTR_HD> ListItem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103).ToList();
            listCustomer.DataSource = ListItem.Take(20);
            listCustomer.DataBind();
        }
        public string getName(int NID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == NID).Count() > 0)
            {
                string CustVendID = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == NID).COMPNAME1;
                return CustVendID;
            }
            else
            {
                return "Not Found";
            }

        }

        protected void btnSearchCus_Click(object sender, EventArgs e)
        {
            string id1 = txtCustomer.Text.Trim().ToString();
            List<ICTR_HD> ListItemHD = new List<ICTR_HD>();
            id1 = id1.TrimStart('0');
            //int list = DB.TBLCOMPANYSETUPs.Single(p => p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) && p.TenentID == TID && p.BUYER == true).COMPID;           
            var ListItemCOM = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) && p.TenentID == TID && p.BUYER == true).ToList();
            foreach (TBLCOMPANYSETUP ComItem in ListItemCOM)
            {
                List<ICTR_HD> SearchHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103 && p.CUSTVENDID == ComItem.COMPID).ToList();
                foreach (ICTR_HD itemHD in SearchHD)
                {
                    ListItemHD.Add(itemHD);
                }
            }
            listCustomer.DataSource = ListItemHD;
            listCustomer.DataBind();
            //List<ICTR_HD> SearchHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103 && p.CUSTVENDID == list).ToList();
            //listCustomer.DataSource = SearchHD;
            //listCustomer.DataBind();
        }

        protected void btnFastCarReport_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ReporForIPS2.aspx");
            Response.Redirect("../Master/AllReportForHealthBar.aspx");
        }


    }
}