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
    public partial class DriverDelRouting : System.Web.UI.Page
    {
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        int TID, LID, UID, EMPID, CID = 0;
        string LangID = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionLoad();
            if (!IsPostBack)
            {
                Button1.Visible = false;

                pnlSuccessMsg.Visible = false;
                lblMsg.Text = "";

                pnlErrorMsg.Visible = false;
                lblerrorMsg.Text = "";
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

            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (txtExpDate.Text != null && txtExpDate.Text != "")
            {
                DateTime ExpDate = Convert.ToDateTime(txtExpDate.Text);
                DateTime Today = DateTime.Now;

                if (ExpDate > Today)
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Closing process is allowed for today or before today ? ?  ?";
                    return;
                }
            }
            else
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Select Date";
                return;
            }

            string MSG = "";

            List<Database.planmealcustinvoiceHD> Listhd = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.SubscriptionOnHold == false).OrderByDescending(p => p.MYTRANSID).ToList();

            foreach (Database.planmealcustinvoiceHD items in Listhd)
            {
                DateTime ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                DateTime ActualDelDate = DateTime.Now;
                int MYTRANSID = items.MYTRANSID;
                DateTime StartDate = Convert.ToDateTime(items.StartDate);
                DateTime EndDate = Convert.ToDateTime(items.EndDate);
                int plan = items.planid;
                if (MYTRANSID != 0)
                {
                    var Day = ExpectedDelDate.Day;
                    var Month = ExpectedDelDate.Month;
                    var Year = ExpectedDelDate.Year;

                    List<Database.planmealcustinvoice> ListInvoice1 = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    if (ListInvoice1.Where(p => (p.ExpectedDelDate.Value.Day <= Day && p.ExpectedDelDate.Value.Month <= Month && p.ExpectedDelDate.Value.Year <= Year) && p.ActualDelDate == null).Count() > 0)
                    {
                        int NExtDeliveryNum = 1;

                        if (ListInvoice1.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year) && p.ActualDelDate == null).Count() > 0)
                        {
                            Database.planmealcustinvoice OBJ123 = ListInvoice1.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year) && p.ActualDelDate == null).FirstOrDefault();
                            NExtDeliveryNum = OBJ123.DayNumber + 1;
                        }

                        DateTime NexDelDate1 = ExpectedDelDate.AddDays(1);
                        if (NexDelDate1.DayOfWeek == DayOfWeek.Friday)
                        {
                            NexDelDate1 = NexDelDate1.AddDays(1);
                        }
                        List<Database.planmealcustinvoice> ListDt = ListInvoice1.Where(p => (p.ExpectedDelDate.Value.Day <= Day && p.ExpectedDelDate.Value.Month <= Month && p.ExpectedDelDate.Value.Year <= Year) && p.ActualDelDate == null && p.ReasonDate == null).ToList();

                        foreach (Database.planmealcustinvoice items1 in ListDt)
                        {
                            Database.planmealcustinvoice objdt = ListInvoice1.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items1.DeliveryID);
                            int chiefID = objdt.chiefID != null && objdt.chiefID != 0 ? Convert.ToInt32(objdt.chiefID) : UID;
                            DateTime ExpectedDel = Convert.ToDateTime(objdt.ExpectedDelDate);
                            NExtDeliveryNum = objdt.DayNumber + 1;
                            DateTime NexDelDate = ExpectedDel;
                            DateTime ProductionDate = objdt.ProductionDate != null ? Convert.ToDateTime(objdt.ProductionDate) : ExpectedDel;
                            List<Database.planmealcustinvoice> ListNext = ListInvoice1.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DayNumber == NExtDeliveryNum).ToList();

                            if (ListNext.Count() > 0)
                            {
                                NexDelDate = Convert.ToDateTime(ListNext.FirstOrDefault().ExpectedDelDate);
                            }


                            objdt.ActualDelDate = ExpectedDel;
                            objdt.NExtDeliveryDate = NexDelDate;
                            objdt.ProductionDate = ProductionDate;
                            objdt.chiefID = chiefID;
                            objdt.Status = "Delivered";
                            DB.SaveChanges();
                        }

                        List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                        List<Database.planmealcustinvoice> ListtotaldelDay = ListInvoice.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int totalDelDay = ListtotaldelDay.Count();

                        List<Database.planmealcustinvoice> ListdelDay = ListInvoice.Where(p => p.ActualDelDate != null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int DeliveredDays = ListdelDay.Count();

                        List<Database.planmealcustinvoice> ListPEndingDay = ListInvoice.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int PendingDays = ListPEndingDay.Count();

                        if (ListDt.Count() > 0)
                        {
                            Database.planmealcustinvoiceHD objhd = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                            objhd.TotalSubDays = totalDelDay;
                            objhd.DeliveredDays = DeliveredDays;
                            objhd.NExtDeliveryNum = NExtDeliveryNum;
                            objhd.NExtDeliveryDate = NexDelDate1;
                            if (PendingDays == 0)
                                objhd.CStatus = "Completed";
                            if (DeliveredDays == 0)
                                objhd.CStatus = "Started";
                            if (PendingDays != 0 && DeliveredDays != 0)
                                objhd.CStatus = "In Progress";
                            DB.SaveChanges();
                        }

                    }

                    Database.planmealcustinvoiceHD objhd1 = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                    if (objhd1.EndDate <= ExpectedDelDate)
                        objhd1.CStatus = "Completed";

                    DB.SaveChanges();
                }               
            }
            deleteduplicate();
            DeactiveCustomer();
        }

        public void DeactiveCustomer()
        {
            //select * from tblcontact_addon1 where TenentID=6 and CustomerId in (SELECT CustomerID FROM planmealcustinvoiceHD WHERE (TenentID = 6) AND (EndDate < '2018-02-09'))

            DateTime today = DateTime.Now;
            DateTime Day30ago = today.AddDays(-30);
            List<Database.planmealcustinvoiceHD> ListInvoice = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.EndDate < Day30ago).ToList();

            foreach(Database.planmealcustinvoiceHD items in ListInvoice)
            {
                if(DB.tblcontact_addon1.Where(p=>p.TenentID==TID && p.LocationID==LID && p.CustomerId==items.CustomerID).Count()>0)
                {
                    Database.tblcontact_addon1 obj = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.LocationID == LID && p.CustomerId == items.CustomerID);
                    obj.active = false;
                    DB.SaveChanges();
                }
            }

            foreach (Database.planmealcustinvoiceHD items in ListInvoice)
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == items.CustomerID).Count() > 0)
                {
                    Database.TBLCOMPANYSETUP obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == items.CustomerID);
                    obj.Active = "N";
                    DB.SaveChanges();
                }
            }
        }

        public void deleteduplicate()
        {
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ACTIVE == true).ToList();
            List<Database.planmealcustinvoice> finalList = new List<planmealcustinvoice>();
            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                List<Database.planmealcustinvoice> ListInvoice1 = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == items.MYTRANSID && p.MYPRODID == items.MYPRODID && p.DeliveryTime == items.DeliveryTime && p.DeliveryMeal == items.DeliveryMeal && p.DayNumber == items.DayNumber && p.ExpectedDelDate == items.ExpectedDelDate).ToList();
                if (ListInvoice1.Count() > 1)
                {
                    List<Database.planmealcustinvoice> ListMax = ListInvoice1.OrderByDescending(p => p.DeliveryID).ToList();
                    finalList = ListMax.Skip(1).ToList();
                    if (finalList.Count() > 0)
                    {
                        foreach (Database.planmealcustinvoice itemdt in finalList)
                        {
                            Database.planmealcustinvoice obj = itemdt;
                            obj.ACTIVE = false;
                            DB.SaveChanges();
                        }
                    }

                }

            }
            OrderOFMeal();
            Prementdeletedt();

        }

        public void OrderOFMeal()
        {
            if (DB.planmealcustinvoices.Where(p => p.TenentID == TID).Count() > 0)
            {
                string Str = "update planmealcustinvoice set Switch3=REFTABLE.SWITCH1 from REFTABLE where planmealcustinvoice.TenentID= " + TID + " and REFTABLE.TenentID=" + TID + " and  planmealcustinvoice.DeliveryMeal=REFTABLE.REFID";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
        }

        public void Prementdeletedt()
        {
            if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ACTIVE == false).Count() > 0)
            {                
                string Str = "delete from planmealcustinvoice where TenentID=" + TID + " and ACTIVE='false'";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
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

        protected void txtExpDate_TextChanged(object sender, EventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (txtExpDate.Text != null && txtExpDate.Text != "")
            {
                DateTime ExpDate = Convert.ToDateTime(txtExpDate.Text);
                DateTime Today = DateTime.Now;

                if (ExpDate > Today)
                {
                    Button1.Visible = false;
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Closing process is allowed for today or before today ? ?  ?";
                    return;
                }
                else
                {
                    Button1.Visible = true;
                }
            }
            else
            {
                Button1.Visible = false;
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Select Date";
                return;
            }

        }

    }
}