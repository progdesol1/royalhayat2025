using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Classes;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.Configuration;
using System.Transactions;

namespace Web.Master
{
    public partial class recalculateContract : System.Web.UI.Page
    {
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        int TID, LID, UID, EMPID, CID = 0;
        string LangID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            btnStartagain.Visible = false;
            SessionLoad();

            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            List<Database.plan_reclculate> ListReccount = DB.plan_reclculate.Where(p => p.TenentID == TID).ToList();
            List<Database.planmealcustinvoiceHD> Listhd = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).ToList();
            int reccount = ListReccount.Count();
            int hdcount = Listhd.Count();
            if (reccount == hdcount)
            {
                deleteplan_rec();
            }

            List<Database.plan_reclculate> ListReccount1 = DB.plan_reclculate.Where(p => p.TenentID == TID).ToList();
            int reccount1 = ListReccount.Count();
            if (reccount > 0)
            {
                btnStartagain.Visible = true;
            }

        }

        public void SessionLoad()
        {
            string Ref = ((AcmMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
              

        protected void Button1_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";
            string MSG = "";

            List<Database.planmealcustinvoiceHD> Listhd = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).OrderByDescending(p => p.MYTRANSID).ToList();

            foreach (Database.planmealcustinvoiceHD items in Listhd)
            {
                int MYTRANSID = items.MYTRANSID;
                DateTime StartDate = Convert.ToDateTime(items.StartDate);
                DateTime EndDate = Convert.ToDateTime(items.EndDate);
                int Week = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.TotalWeek != null && p.TotalWeek != 0).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.TotalWeek != null && p.TotalWeek != 0).FirstOrDefault().TotalWeek) : 1;
                int plan = items.planid;
                if (MYTRANSID != 0)
                {                   
                    Copyfullplan(MYTRANSID, Week, plan, StartDate);
                    recalculate(MYTRANSID, StartDate, EndDate);
                    List<Database.plan_reclculate> Listrecal = DB.plan_reclculate.Where(p => p.TenentID == TID && p.mytransid == MYTRANSID).ToList();
                    Database.plan_reclculate objrecal = new plan_reclculate();
                    objrecal.TenentID = TID;
                    objrecal.mytransid = MYTRANSID;
                    objrecal.recalculateid = Listrecal.Count() > 0 ? Listrecal.Where(p => p.TenentID == TID && p.mytransid == MYTRANSID).Max(p => p.recalculateid) + 1 : 1;
                    objrecal.reclculatedate = DateTime.Now;
                    objrecal.successflag = true;
                    DB.plan_reclculate.AddObject(objrecal);
                    DB.SaveChanges();

                    MSG += "Recalculation of Contract ID= " + MYTRANSID + " is Complate";
                    if (MSG != "")
                    {
                        pnlSuccessMsg.Visible = true;
                        lblMsg.Text = MSG;                       
                    }
                }
            }



        }

        public void Copyfullplan(int MYTRANSID, int Week, int plan, DateTime EndDate)
        {
            string WeekofDay = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.WeekofDay != "" && p.WeekofDay != null).FirstOrDefault().WeekofDay.ToString();
            int daycount = 0;
            string[] days1 = WeekofDay.Split(',');
            //foreach (string Val in days1)
            //{
            //    daycount++;
            //}
            daycount = days1.Length;
            List<Database.planmealcustinvoiceHD> ListHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();



            //if (ListHD.Count() > 0)
            //{
            //    Database.planmealcustinvoiceHD objhd = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
            //    objhd.StartDate = Convert.ToDateTime(txtBeingDate.Text);
            //    objhd.EndDate = Convert.ToDateTime(txtEndDate.Text);
            //    DB.SaveChanges();
            //}

            List<Database.planmealcustinvoice> Listinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();// && p.CustomerID == CID
            if (Listinvoice.Count() > 0)
            {

                //foreach (Database.planmealcustinvoice items in Listinvoice)
                //{
                //    Database.planmealcustinvoice objupdatainvoice = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                //    objupdatainvoice.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                //    objupdatainvoice.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                //    objupdatainvoice.EndDate = Convert.ToDateTime(txtEndDate.Text);
                //    DB.SaveChanges();
                //}

                for (int i = 2; i <= Week; i++)
                {
                    int ADDweek = i - 1;

                    int dayst = daycount;
                    List<Database.planmealcustinvoice> Listweek = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.DisplayWeek == 1).ToList();//&& p.CustomerID == CID
                    if (Listweek.Count() > 0)
                    {
                        foreach (Database.planmealcustinvoice items in Listweek)
                        {
                            DateTime DTexp = Convert.ToDateTime(items.ExpectedDelDate);
                            int Tday = ADDweek * 7;
                            DateTime Fexp = DTexp.AddDays(Tday);
                            //DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                            EndDate = EndDate.AddDays(1);
                            if (EndDate >= Fexp)
                            {
                                int day = Convert.ToInt32(items.DayNumber);
                                int fday = dayst * ADDweek;
                                int Final = fday + day;
                                List<Database.planmealcustinvoice> ListExist = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == items.MYPRODID && p.DeliveryMeal == items.DeliveryMeal && p.DayNumber == Final).ToList();
                                if (ListExist.Count() < 1)
                                {
                                    Database.planmealcustinvoice objinvice = new planmealcustinvoice();
                                    objinvice.TenentID = TID;
                                    objinvice.LOCATION_ID = LID;
                                    objinvice.MYTRANSID = MYTRANSID;
                                    objinvice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.DeliveryID) + 1) : 1;
                                    objinvice.CustomerID = items.CustomerID;
                                    objinvice.planid = plan;
                                    objinvice.MealType = items.MealType;
                                    objinvice.MYPRODID = items.MYPRODID;
                                    objinvice.ProdName1 = items.ProdName1;

                                    objinvice.DayNumber = Final;
                                    objinvice.OprationDay = items.OprationDay;
                                    objinvice.TransID = items.TransID;
                                    objinvice.ContractID = items.ContractID;
                                    objinvice.WeekofDay = items.WeekofDay;
                                    objinvice.TotalWeek = items.TotalWeek;
                                    objinvice.NameOfDay = items.NameOfDay;
                                    objinvice.NoOfWeek = i;
                                    objinvice.TotalDeliveryDay = items.TotalDeliveryDay;
                                    objinvice.ActualDeliveryDay = items.ActualDeliveryDay;
                                    objinvice.ExpectedDeliveryDay = items.ExpectedDeliveryDay;
                                    objinvice.DeliveryTime = items.DeliveryTime;
                                    objinvice.DeliveryMeal = items.DeliveryMeal;
                                    objinvice.DriverID = items.DriverID;
                                    objinvice.StartDate = items.StartDate;
                                    objinvice.EndDate = items.EndDate;

                                    objinvice.ExpectedDelDate = Fexp;
                                    objinvice.Status = "Pending";
                                    objinvice.NExtDeliveryDate = Fexp.AddDays(1);
                                    objinvice.SubscriptonDayNumber = items.SubscriptonDayNumber;
                                    objinvice.Calories = items.Calories;
                                    objinvice.Carbs = items.Carbs;
                                    objinvice.Protein = items.Protein;
                                    objinvice.Fat = items.Fat;
                                    objinvice.ItemWeight = items.ItemWeight;
                                    objinvice.Qty = items.Qty;
                                    objinvice.Item_cost = items.Item_cost;
                                    objinvice.Item_price = items.Item_price;
                                    objinvice.Total_price = items.Total_price;
                                    objinvice.ShortRemark = items.ShortRemark;
                                    objinvice.ACTIVE = items.ACTIVE;
                                    objinvice.ChangesDate = items.ChangesDate;
                                    objinvice.Switch3 = items.Switch3;
                                    //string Log = "MYTRANSID=" + MYTRANSID + ", CustomerID=" + items.CustomerID + ", planid=" + plan + ", DeliveryMeal=" + items.DeliveryMeal + ", MYPRODID=" + items.MYPRODID;
                                    //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                                    //objinvice.CRUP_ID = CRUP_ID;

                                    DB.planmealcustinvoices.AddObject(objinvice);
                                    DB.SaveChanges();
                                }
                            }
                        }

                    }
                }
            }
        }
        public void recalculate(int MYTRANSID, DateTime StartDate, DateTime EndDate)
        {
            if (MYTRANSID != 0)
            {
                DateTime Today = DateTime.Now;

                List<Database.planmealcustinvoice> Listplan = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.StartDate >= StartDate && p.EndDate <= EndDate).ToList();

                if (Listplan.Count() > 0)
                {
                    DayAgoDelevery(MYTRANSID);

                    UpdatedaysHD(MYTRANSID, StartDate, EndDate);

                    DeleteDTFRomTo(MYTRANSID, StartDate, EndDate);

                    Datewiceinsert(MYTRANSID, StartDate, EndDate);

                    Prementdeletedt(MYTRANSID);
                }

            }
        }

        public void DayAgoDelevery(int MYTRANSID)
        {
            DateTime Today = DateTime.Now;
            Today = Today.AddDays(-1);
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && p.ExpectedDelDate < Today).ToList();
            if (ListInvoice.Count() > 0)
            {
                foreach (Database.planmealcustinvoice items in ListInvoice)
                {
                    Database.planmealcustinvoice Objinvoice = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                    Objinvoice.ActualDelDate = items.ExpectedDelDate;
                    Objinvoice.ProductionDate = items.ExpectedDelDate;
                    Objinvoice.chiefID = UID;
                    Objinvoice.Status = "Delivered";
                    DB.SaveChanges();
                }
            }
        }

        public void UpdatedaysHD(int MYTRANSID, DateTime StartDate, DateTime EndDate)
        {
            List<Database.planmealcustinvoice> ListDTinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

            List<Database.planmealcustinvoice> ListtotaldelDay = ListDTinvoice.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int totalDelDay = ListtotaldelDay.Count();

            List<Database.planmealcustinvoice> ListdelDay = ListDTinvoice.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int DeliveredDays = ListdelDay.Count();

            Database.planmealcustinvoiceHD ObjHD = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
            ObjHD.TotalSubDays = totalDelDay;
            ObjHD.DeliveredDays = DeliveredDays;
            ObjHD.StartDate = StartDate; //Convert.ToDateTime(txtBeingDate.Text);
            ObjHD.EndDate = EndDate;// Convert.ToDateTime(txtEndDate.Text);

            if (ListDTinvoice.Where(p => p.ActualDelDate != null).Count() > 0)
            {
                if (ListDTinvoice.Where(p => p.ActualDelDate == null).Count() > 0)
                {
                    ObjHD.CStatus = "In Progress";
                }
                else
                {
                    ObjHD.CStatus = "Completed";
                }
            }
            else
            {
                ObjHD.CStatus = "Started";
            }

            if (ListDTinvoice.Where(p => p.ActualDelDate != null).Count() > 0)
            {
                int nextdayno = ListDTinvoice.Where(p => p.ActualDelDate != null).Max(p => p.DayNumber) + 1;
                DateTime NextDate = ListDTinvoice.Where(p => p.DayNumber == nextdayno).Count() > 0 ? Convert.ToDateTime(ListDTinvoice.Where(p => p.DayNumber == nextdayno).FirstOrDefault().ExpectedDelDate) : Convert.ToDateTime(ListDTinvoice.Where(p => p.ActualDelDate != null).FirstOrDefault().ExpectedDelDate);
                ObjHD.NExtDeliveryNum = nextdayno;
                ObjHD.NExtDeliveryDate = NextDate;
            }

            DB.SaveChanges();
        }

        public void DeleteDTFRomTo(int MYTRANSID, DateTime StartDate, DateTime EndDate)
        {
            EndDate = EndDate.AddDays(1);
            List<Database.planmealcustinvoice> Listdt = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && (p.ExpectedDelDate < StartDate || p.ExpectedDelDate >= EndDate)).ToList();

            foreach (Database.planmealcustinvoice items in Listdt)
            {
                Database.planmealcustinvoice objdt = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                objdt.ACTIVE = false;
                //DB.planmealcustinvoices.DeleteObject(objdt);
                DB.SaveChanges();
            }
        }

        public void Datewiceinsert(int MYTRANSID, DateTime Start_Date, DateTime End_Date)
        {
            DateTime Today = DateTime.Now;


            DateTime ContractEnddate = End_Date;
            int Totaldafday1 = Convert.ToInt32((ContractEnddate - Today).TotalDays);

            int EnterDay = Totaldafday1;// Convert.ToInt32(txtDelTotalDay.Text);            
            DateTime Beingdate = Start_Date;
            DateTime Enddate = Today;
            DateTime Enddate1 = Today;

            string WeekofDay = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).FirstOrDefault().WeekofDay.ToString();
            int DayCount1 = 0;
            string[] dayscount = WeekofDay.Split(',');
            //foreach (string Val in dayscount)
            //{
            //    DayCount1++;
            //}
            DayCount1 = dayscount.Length;
            int HowManyDay = DayCount1;

            List<Database.planmealcustinvoice> ListInvoiceadd = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null).ToList();

            ContractEnddate = ContractEnddate.AddDays(1);
            while (Enddate <= ContractEnddate)
            {
                if (Enddate.DayOfWeek == DayOfWeek.Friday)
                {
                    Enddate = Enddate.AddDays(1);
                }
                string TEmpDate = Enddate.ToShortDateString();
                DateTime Enddatecheck = Convert.ToDateTime(TEmpDate);

                var Day = Enddatecheck.Day;
                var Month = Enddatecheck.Month;
                var Year = Enddatecheck.Year;

                List<Database.planmealcustinvoice> ListInvoiceAdd1 = ListInvoiceadd.Where(p => (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year)).ToList();
                int ListInvoiceAdd1Count = ListInvoiceAdd1.Count();
                foreach (Database.planmealcustinvoice Itemsdt in ListInvoiceAdd1)
                {
                    int Cloop = 0;
                    int OprationDay = 0;
                    string NameOfDay = null;
                    string[] days = WeekofDay.Split(','); ;// tags_1.Text.Split(',');
                    foreach (string Val1 in days)
                    {
                        if (Cloop == 0)
                        {
                            if (Val1 == "Sat" && Enddate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                Cloop++;
                                OprationDay = 1;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Sun" && Enddate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                Cloop++;
                                OprationDay = 2;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Mon" && Enddate.DayOfWeek == DayOfWeek.Monday)
                            {
                                Cloop++;
                                OprationDay = 3;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Tue" && Enddate.DayOfWeek == DayOfWeek.Tuesday)
                            {
                                Cloop++;
                                OprationDay = 4;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Wed" && Enddate.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                Cloop++;
                                OprationDay = 5;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Thu" && Enddate.DayOfWeek == DayOfWeek.Thursday)
                            {
                                Cloop++;
                                OprationDay = 6;
                                NameOfDay = Val1;
                            }
                        }
                    }

                    if (Cloop > 0)
                    {
                        Enddate1 = Enddate;

                        int Totaldafday = Convert.ToInt32((Enddate1.Date - Beingdate.Date).TotalDays);

                        decimal Week1 = Totaldafday / 6;
                        decimal Remainder = Totaldafday % 6;

                        int week = 0;
                        if (Remainder > 0)
                        {
                            Week1 = Week1 + 1;
                            week = Convert.ToInt32(Math.Ceiling(Week1));
                        }
                        else
                        {
                            week = Convert.ToInt32(Math.Ceiling(Week1));
                        }

                        int No_OfWeek = week != 0 ? week : 1;

                        int planid = Itemsdt.planid;
                        int CustomerID = Itemsdt.CustomerID;
                        long MYPRODID = Itemsdt.MYPRODID;
                        string ProdName1 = Itemsdt.ProdName1;
                        //string WeekofDay = tags_1.Text;
                        int TotalWeek = Convert.ToInt32(Itemsdt.TotalWeek);
                        int TotalDeliveryDay = Convert.ToInt32(Itemsdt.TotalDeliveryDay);
                        int ActualDeliveryDay = Convert.ToInt32(Itemsdt.ActualDeliveryDay);
                        int ExpectedDeliveryDay = Convert.ToInt32(Itemsdt.ExpectedDeliveryDay);
                        int DeliveryTime = Convert.ToInt32(Itemsdt.DeliveryTime);
                        int DeliveryMeal = Convert.ToInt32(Itemsdt.DeliveryMeal);
                        int DriverID = Convert.ToInt32(Itemsdt.DriverID);

                        DateTime ExpectedDelDate = Enddate1;
                        decimal Calories = Convert.ToDecimal(Itemsdt.Calories);
                        decimal Carbs = Convert.ToDecimal(Itemsdt.Carbs);
                        decimal Protein = Convert.ToDecimal(Itemsdt.Protein);
                        decimal Fat = Convert.ToDecimal(Itemsdt.Fat);
                        decimal ItemWeight = Convert.ToDecimal(Itemsdt.ItemWeight);
                        int Qty = Convert.ToInt32(Itemsdt.Qty);
                        decimal Item_cost = Convert.ToDecimal(Itemsdt.Item_cost);
                        decimal Item_price = Convert.ToDecimal(Itemsdt.Item_price);
                        decimal Total_price = Convert.ToDecimal(Itemsdt.Total_price);
                        string ShortRemark = Itemsdt.ShortRemark;


                        //DateTime Checkdate = Beingdate;
                        //int FridayCount = 0;
                        //for (int i = 1; i <= Totaldafday; i++)
                        //{
                        //    if (Checkdate.DayOfWeek == DayOfWeek.Friday)
                        //    {
                        //        FridayCount++;
                        //    }
                        //    Checkdate = Checkdate.AddDays(1);
                        //}

                        int FridayCount = CountDays(DayOfWeek.Friday, Beingdate, Enddate);

                        int temDaynumber = Totaldafday - FridayCount;
                        int DayNumber = temDaynumber != 0 ? temDaynumber : 1;// Convert.ToInt32(Itemsdt.DayNumber);

                        AddNewDT(MYTRANSID, planid, CustomerID, MYPRODID, ProdName1, DayNumber, OprationDay, WeekofDay, TotalWeek, NameOfDay, No_OfWeek, TotalDeliveryDay, ActualDeliveryDay, ExpectedDeliveryDay, DeliveryTime, DeliveryMeal, DriverID, Start_Date, End_Date, ExpectedDelDate, Calories, Carbs, Protein, Fat, ItemWeight, Qty, Item_cost, Item_price, Total_price, ShortRemark);

                    }
                    else
                    {
                        if (Enddate.DayOfWeek != DayOfWeek.Friday)
                        {
                            Enddate = Enddate.AddDays(1);
                        }
                    }
                }

                Enddate = Enddate.AddDays(1);


            }
            updatedaynumber(MYTRANSID, Start_Date);
            updatenoofweek(MYTRANSID, Start_Date);
            UpdateDTrecord(MYTRANSID, Start_Date);
        }

        static int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;
            int count = (int)Math.Floor(ts.TotalDays / 7);
            int remainder = (int)(ts.TotalDays % 7);
            int sinceLastDay = (int)(end.DayOfWeek - day);
            if (sinceLastDay < 0) sinceLastDay += 7;

            if (remainder >= sinceLastDay) count++;

            return count > 0 ? count : 0;
        }


        public void updatenoofweek(int MYTRANSID, DateTime StartDate)
        {
            //DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
            string start_date = StartDate.ToShortDateString();
            int Noofweek = 1;// DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Max(p => p.NoOfWeek)) : 0;
            string dateflag = "";
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true).OrderBy(p => p.DayNumber).ToList();//&& p.ActualDelDate == null

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {

                DateTime Checkdate = Convert.ToDateTime(items.ExpectedDelDate);
                string Check_date = Checkdate.ToShortDateString();
                if (Checkdate.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (Check_date != start_date && dateflag != Check_date)
                    {
                        dateflag = Check_date;
                        Noofweek = Noofweek + 1;
                    }
                }
                Database.planmealcustinvoice objdt = items;
                objdt.NoOfWeek = Noofweek;
                DB.SaveChanges();
            }
        }

        public void updatedaynumber(int MYTRANSID, DateTime StartDate)
        {
            string start_date = StartDate.ToShortDateString();
            int dayNumber = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Max(p => p.DayNumber)) : 1;
            string dateflag = "";
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.ActualDelDate == null).ToList();

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                DateTime Checkdate = Convert.ToDateTime(items.ExpectedDelDate);
                string Check_date = Checkdate.ToShortDateString();

                if (Check_date != start_date)
                {
                    if (Check_date != dateflag)
                    {
                        dateflag = Check_date;
                        dayNumber = dayNumber + 1;
                    }

                }

                Database.planmealcustinvoice objdt = items;
                objdt.DayNumber = dayNumber;
                DB.SaveChanges();
            }
        }

        public void updatedisplayweek(int MYTRANSID, DateTime StartDate)
        {
            StartDate = StartDate.AddDays(6);

            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ExpectedDelDate <= StartDate && p.ActualDelDate == null && p.ACTIVE == true).ToList();

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                Database.planmealcustinvoice objdt = items;
                objdt.DisplayWeek = 1;
                DB.SaveChanges();
            }
        }

        public void UpdateDTOPrationDay(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true).OrderBy(p => p.DayNumber).ToList();

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                DateTime expdate = Convert.ToDateTime(items.ExpectedDelDate);
                Database.planmealcustinvoice objdt = items;
                if (expdate.DayOfWeek == DayOfWeek.Saturday)
                {
                    objdt.OprationDay = 1;
                    objdt.NameOfDay = "Sat";
                }
                if (expdate.DayOfWeek == DayOfWeek.Sunday)
                {
                    objdt.OprationDay = 2;
                    objdt.NameOfDay = "Sun";
                }
                if (expdate.DayOfWeek == DayOfWeek.Monday)
                {
                    objdt.OprationDay = 3;
                    objdt.NameOfDay = "Mon";
                }
                if (expdate.DayOfWeek == DayOfWeek.Tuesday)
                {
                    objdt.OprationDay = 4;
                    objdt.NameOfDay = "Tue";
                }
                if (expdate.DayOfWeek == DayOfWeek.Wednesday)
                {
                    objdt.OprationDay = 5;
                    objdt.NameOfDay = "Wed";
                }
                if (expdate.DayOfWeek == DayOfWeek.Thursday)
                {
                    objdt.OprationDay = 6;
                    objdt.NameOfDay = "Thu";
                }
                DB.SaveChanges();
            }
        }
        public void UpdateDTrecord(int MYTRANSID, DateTime StartDate)
        {
            updatedisplayweek(MYTRANSID, StartDate);

            UpdateDTOPrationDay(MYTRANSID);

            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && p.ACTIVE == true).ToList();

            int Totalweek = Convert.ToInt32(ListInvoice.Max(p => p.NoOfWeek));

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                Database.planmealcustinvoice objdt = items;
                if (Totalweek > 1)
                {
                    objdt.TotalWeek = Totalweek;
                }

                DB.SaveChanges();
            }

        }

        public void Prementdeletedt(int MYTRANSID)
        {
            if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == false).Count() > 0)
            {
                string Str = "delete from planmealcustinvoice where TenentID=" + TID + " and MYTRANSID=" + MYTRANSID + " and ACTIVE='false'";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
        }

        public void deleteplan_rec()
        {
            if (DB.plan_reclculate.Where(p => p.TenentID == TID && p.successflag == true).Count() > 0)
            {
                string Str = "delete from plan_reclculate where TenentID=" + TID + " and successflag='true'";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
        }

        public void AddNewDT(int MYTRANSID, int planid, int CustomerID, long MYPRODID, string ProdName1, int DayNumber, int OprationDay, string WeekofDay, int TotalWeek, string NameOfDay, int No_OfWeek, int TotalDeliveryDay, int ActualDeliveryDay, int ExpectedDeliveryDay, int DeliveryTime, int DeliveryMeal, int DriverID, DateTime StartDate, DateTime EndDate, DateTime ExpectedDelDate, decimal Calories, decimal Carbs, decimal Protein, decimal Fat, decimal ItemWeight, int Qty, decimal Item_cost, decimal Item_price, decimal Total_price, string ShortRemark)
        {
            var Day = ExpectedDelDate.Day;
            var Month = ExpectedDelDate.Month;
            var Year = ExpectedDelDate.Year;
            List<Database.planmealcustinvoice> ListRemoved = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == DeliveryMeal && p.MYPRODID == MYPRODID && (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year)).ToList();

            foreach (Database.planmealcustinvoice items in ListRemoved)
            {
                Database.planmealcustinvoice obj = items;
                obj.ACTIVE = false;
                DB.SaveChanges();
            }

            if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == DeliveryMeal && p.MYPRODID == MYPRODID && (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year) && p.ACTIVE == true).Count() < 1)
            {
                Database.planmealcustinvoice objinvice = new planmealcustinvoice();
                objinvice.TenentID = TID;
                objinvice.MYTRANSID = MYTRANSID;
                objinvice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.DeliveryID) + 1) : 1;
                objinvice.LOCATION_ID = LID;
                objinvice.TransID = MYTRANSID;// Convert.ToInt32(txtInvoiceNumber.Text);//Convert.ToInt32(txtInvoiceNumber1s.Text);
                objinvice.ContractID = MYTRANSID.ToString();
                objinvice.planid = planid;//Convert.ToInt32(drpPlan.SelectedValue);
                objinvice.CustomerID = CustomerID;// Convert.ToInt32(drpCustomer.SelectedValue);
                objinvice.MYPRODID = MYPRODID;
                objinvice.ProdName1 = ProdName1;
                //int day = Convert.ToInt32(DayNumber);
                //int fday = dayst * ADDweek;
                //int Final = fday + day;
                objinvice.DayNumber = DayNumber;
                objinvice.OprationDay = OprationDay;
                objinvice.WeekofDay = WeekofDay;//tags_1.Text;
                objinvice.TotalWeek = TotalWeek;// Convert.ToInt32(txtTotalWeek.Text);
                objinvice.NameOfDay = NameOfDay;
                objinvice.NoOfWeek = No_OfWeek;
                objinvice.TotalDeliveryDay = TotalDeliveryDay;// Convert.ToInt32(txtTotalDeliveryDay.Text);
                objinvice.ActualDeliveryDay = ActualDeliveryDay;// Convert.ToInt32(txtDelivered.Text);
                objinvice.ExpectedDeliveryDay = ExpectedDeliveryDay;// Convert.ToInt32(txtRemainingDay.Text);
                objinvice.DeliveryTime = DeliveryTime;
                objinvice.DeliveryMeal = DeliveryMeal;
                objinvice.MealType = DeliveryMeal;
                objinvice.DriverID = DriverID;
                objinvice.StartDate = StartDate;// Convert.ToDateTime(txtBeingDate.Text);
                objinvice.EndDate = EndDate;// Convert.ToDateTime(txtEndDate.Text);

                objinvice.ExpectedDelDate = ExpectedDelDate; // Fexp;
                objinvice.Status = "Pending";
                objinvice.NExtDeliveryDate = ExpectedDelDate.AddDays(1);
                objinvice.Calories = Calories;
                objinvice.Carbs = Carbs;
                objinvice.Protein = Protein;
                objinvice.Fat = Fat;
                objinvice.ItemWeight = ItemWeight;
                objinvice.Qty = Qty;
                objinvice.Item_cost = Item_cost;
                objinvice.Item_price = Item_price;
                objinvice.Total_price = Total_price;
                objinvice.ShortRemark = ShortRemark;
                objinvice.ACTIVE = true;
                List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryMeal).ToList();
                objinvice.Switch3 = ListRef.Where(p => p.SWITCH1 != null && p.SWITCH1 != "").Count() > 0 ? ListRef.Single(p => p.TenentID == TID && p.REFID == DeliveryMeal && p.SWITCH1 != null && p.SWITCH1 != "").SWITCH1.ToString() : "";
                //string Log = "MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + plan + ", DeliveryMeal=" + items.DeliveryMeal + ", MYPRODID=" + items.MYPRODID;
                //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                //objinvice.CRUP_ID = CRUP_ID;

                DB.planmealcustinvoices.AddObject(objinvice);
                DB.SaveChanges();
            }

        }

        protected void btnStartagain_Click(object sender, EventArgs e)
        {


            List<Database.planmealcustinvoiceHD> Listhd = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).ToList();
            List<Database.planmealcustinvoiceHD> ListfinalHD = new List<planmealcustinvoiceHD>();
            List<Database.plan_reclculate> Listrecalculate = DB.plan_reclculate.Where(p => p.TenentID == TID).ToList();
            foreach (Database.planmealcustinvoiceHD itemhd in Listhd)
            {
                if (Listrecalculate.Where(p => p.TenentID == TID && p.mytransid == itemhd.MYTRANSID).Count() < 1)
                {
                    Database.planmealcustinvoiceHD obj = Listhd.Single(p => p.TenentID == TID && p.MYTRANSID == itemhd.MYTRANSID);
                    ListfinalHD.Add(obj);
                }
            }

            foreach (Database.planmealcustinvoiceHD items in ListfinalHD)
            {

                int MYTRANSID = items.MYTRANSID;
                DateTime StartDate = Convert.ToDateTime(items.StartDate);
                DateTime EndDate = Convert.ToDateTime(items.EndDate);
                int Week = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.TotalWeek != null && p.TotalWeek != 0).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.TotalWeek != null && p.TotalWeek != 0).FirstOrDefault().TotalWeek) : 1;
                int plan = items.planid;
                if (MYTRANSID != 0)
                {
                    //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 10, 0)))
                    //{
                    Copyfullplan(MYTRANSID, Week, plan, EndDate);
                    recalculate(MYTRANSID, StartDate, EndDate);
                    List<Database.plan_reclculate> Listrecal = DB.plan_reclculate.Where(p => p.TenentID == TID && p.mytransid == MYTRANSID).ToList();
                    Database.plan_reclculate objrecal = new plan_reclculate();
                    objrecal.TenentID = TID;
                    objrecal.mytransid = MYTRANSID;
                    objrecal.recalculateid = Listrecal.Count() > 0 ? Listrecal.Where(p => p.TenentID == TID && p.mytransid == MYTRANSID).Max(p => p.recalculateid) + 1 : 1;
                    objrecal.reclculatedate = DateTime.Now;
                    objrecal.successflag = true;
                    DB.plan_reclculate.AddObject(objrecal);
                    DB.SaveChanges();
                    //}
                }

            }
        }
    }
}