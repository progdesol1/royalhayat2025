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
using System.Data.Objects.SqlClient;


namespace Web.Sales
{
    public partial class HBIndex : System.Web.UI.Page
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
                TODAYDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                var Date = DateTime.Now.ToShortDateString();
                int ToDay = Convert.ToInt32(DateTime.Now.Day);
                int ToMonth = Convert.ToInt32(DateTime.Now.Month);
                int ToYear = Convert.ToInt32(DateTime.Now.Year);

                //List<Database.ICTR_HD> itemList = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 4101 && p.transsubid == 410103 && p.ENTRYDATE.Value.Day == ToDay && p.ENTRYDATE.Value.Month == ToMonth && p.ENTRYDATE.Value.Year == ToYear).ToList();
                //// Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                //decimal TOT1 = Convert.ToDecimal(itemList.Sum(p => p.TOTAMT));
                //lblbox1KD.Text = TOT1.ToString();
                ////Convert.ToDateTime(objAPP[i].CompletionDate).ToString("dd/MM/yyyy")

                List<Database.planmealcustinvoiceHD> itemList = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.StartDate.Value.Day == ToDay && p.StartDate.Value.Month == ToMonth && p.StartDate.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT1 = Convert.ToDecimal(itemList.Sum(p => p.Total_price));
                lblbox1KD.Text = TOT1.ToString("N0");
                //Convert.ToDateTime(objAPP[i].CompletionDate).ToString("dd/MM/yyyy")

                var weeknumber = DateTime.Now.Weeknumber();
               
                List<Database.planmealcustinvoiceHD> itemListweek = DB.planmealcustinvoiceHDs.Where(x => SqlFunctions.DatePart("week", x.StartDate) == weeknumber).ToList(); 
                decimal TOTweek = Convert.ToDecimal(itemListweek.Sum(p => p.Total_price));
                lblBoxWeek.Text = TOTweek.ToString("0");
                //string week = DateTime.Now.ToString("MMM");
                lblWeek.Text = weeknumber.ToString();

                //Sale This Month
                List<Database.planmealcustinvoiceHD> itemListmonth = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.StartDate.Value.Month == ToMonth).ToList();
                decimal TOT2 = Convert.ToDecimal(itemListmonth.Sum(p => p.Total_price));
                lblbox2KD.Text = TOT2.ToString("N0");
                string Month = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("yyyy");
                Thismonth.Text = Month;

                //Sale This Year
                List<Database.planmealcustinvoiceHD> itemListYear = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.StartDate.Value.Year == ToYear).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                decimal TOT3 = Convert.ToDecimal(itemListYear.Sum(p => p.Total_price));
                lblbox3KD.Text = TOT3.ToString("N0");
                string Year = DateTime.Now.ToString("yyyy");
                ThisYear.Text = Year;

                //sale Return this day
                //List<Database.ICTR_HD> itemListYearReturn = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && p.transid == 5101 && p.transsubid == 510101 && p.ENTRYDATE.Value.Year == ToYear).ToList();
                //decimal TOT4 = Convert.ToDecimal(itemListYearReturn.Sum(p => p.TOTAMT));
                //lblbox4KD.Text = TOT4.ToString();
                //string returnyear = DateTime.Now.ToString("yyyy");
                //ThisYearReturn.Text = returnyear;

                //purchase this days
                List<Database.planmealcustinvoiceHD> itemListDatePurchase = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).ToList();
                decimal TOT5 = Convert.ToDecimal(itemListDatePurchase.Sum(p => p.Total_price));
                lblbox5KD.Text = TOT5.ToString("N0");
               // TodayDatePurchase.Text = DateTime.Now.ToString("dd/MMM/yy");


                //purchase this Month
                List<Database.planmealcustinvoice> itemListMonthPurchase = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ActualDelDate != null).ToList();
                //decimal TOT6 = Convert.ToDecimal(itemListMonthPurchase.Sum(p => p.TOTAMT));
                lblbox6KD.Text = itemListMonthPurchase.Count().ToString();
                //string MonthPur = DateTime.Now.ToString("MMM");
                //ThisMonthPurchase.Text = MonthPur;

                //purchase this Year
                List<Database.planmealcustinvoice> itemListYearPurchase = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ReturnReason != null).ToList();
                decimal TOT7 = Convert.ToDecimal(itemListYearPurchase.Sum(p => p.Item_price));
                lblbox7KD.Text = TOT7.ToString("N0");
                //string Puryear = DateTime.Now.ToString("yyyy");
                //ThisYearPurchase.Text = Puryear;

                //purchase Return year
                List<Database.planmealcustinvoice> itemListYearPurchaseReturn = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ProductionDate != null).ToList();
                // Database.ICTR_HD OBJHD = DB.ICTR_HD.Where(p => p.TenentID == TID);
                int TOT8 = Convert.ToInt32(itemListYearPurchaseReturn.Sum(p => p.Qty));
                lblbox8KD.Text = TOT8.ToString();
                //string Purreturnyear = DateTime.Now.ToString("yyyy");
                //ThisYearPurchaseReturn.Text = Purreturnyear;
                DateTime Today = DateTime.Now;
                List<Database.planmealcustinvoiceHD> ListLatest10 = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.StartDate <= Today && p.EndDate >= Today).Take(10).ToList();
                //ListLatesttenOrder.DataSource = ListLatest10.OrderByDescending(p => p.MYTRANSID);
                //ListLatesttenOrder.DataBind();

                List<Database.planmealcustinvoiceHD> ListconractPending = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CStatus == "Started").ToList();
                ListContractPending.DataSource = ListconractPending.OrderByDescending(p => p.MYTRANSID);
                ListContractPending.DataBind();

                List<Database.planmealcustinvoiceHD> ListconractCompleted = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CStatus == "Completed").ToList();
                ListoederCompleted.DataSource = ListconractCompleted.OrderByDescending(p => p.MYTRANSID);
                ListoederCompleted.DataBind();               

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
            //List<Database.planmealcustinvoiceHD> ListItem = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).GroupBy(p => p.CustomerID).FirstOrDefault().ToList();
            //listCustomer.DataSource = ListItem.Take(20);
            //listCustomer.DataBind();
        }
        public string gettotalorder(int CustomerID)
        {
            if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CustomerID).Count() > 0)
            {
                string CustVendID = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CustomerID).Count().ToString();
                return CustVendID;
            }
            else
            {
                return "Not Found";
            }
        }

        public string gettotalAmaont(int CustomerID)
        {
            if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CustomerID).Count() > 0)
            {
                string CustVendID = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CustomerID).Sum(p => p.Total_price).ToString();
                return CustVendID;
            }
            else
            {
                return "Not Found";
            }
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
            //string id1 = txtCustomer.Text.Trim().ToString();
            //List<Database.planmealcustinvoiceHD> ListItemHD = new List<Database.planmealcustinvoiceHD>();
            //id1 = id1.TrimStart('0');

            ////int list = DB.TBLCOMPANYSETUPs.Single(p => p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) && p.TenentID == TID && p.BUYER == true).COMPID;           
            //var ListItemCOM = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) && p.TenentID == TID).ToList();
            //foreach (TBLCOMPANYSETUP ComItem in ListItemCOM)
            //{
            //    List<Database.planmealcustinvoiceHD> SearchHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == ComItem.COMPID).ToList();
            //    foreach (Database.planmealcustinvoiceHD itemHD in SearchHD)
            //    {
            //        ListItemHD.Add(itemHD);
            //    }
            //}
            //listCustomer.DataSource = ListItemHD;
            //listCustomer.DataBind();
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
public static class DateTimeExtensions
{
    // Extension method to get the weeknumber from a date
    public static int Weeknumber(this DateTime date)
    {
        var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
        return cal.GetWeekOfYear(date,
        System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }
}