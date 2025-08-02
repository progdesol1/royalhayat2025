using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class ContractPrintsInvoice1 : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;
            if (!IsPostBack)
            {
                DRPBind();
                pnlhead.Visible = false;
            }
        }
        //<add name="CallEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=64.62.143.69;initial catalog=ayosofte_Anmol;user id=Ayo_Anmol;password=Anmol@123;multipleactiveresultsets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
        public void ListBind()
        {
            Label1.Visible = false;
            Label3.Visible = false;
            pnlWarningMsg.Visible = false;
            string MealTname = "";
           
            int CID = Convert.ToInt32(DrpCustomer.SelectedValue);
            if (CID == 0)
            {
                Label1.Visible = true;
                return;
            }
            int PID = 0;//Convert.ToInt32(DrpPlan.SelectedValue);
            int Mytran = 0;
            if (!String.IsNullOrEmpty(DrpPlan.SelectedValue))
            {
                string[] Blank = DrpPlan.SelectedValue.Split('-');
                PID = Convert.ToInt32(Blank[0]);
                Mytran = Convert.ToInt32(Blank[1]);

                List<Database.planmealcustinvoice> Listcon = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PID && p.MYTRANSID == Mytran && p.ACTIVE == true).OrderBy(p => p.DayNumber).ToList();
                ListContract.DataSource = Listcon;//DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PID && p.ACTIVE == true).OrderBy(p => p.DayNumber);
                ListContract.DataBind();
                if (Listcon.Count() < 1)
                    pnlhead.Visible = false;
                else
                {
                    if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PID && p.MYTRANSID == Mytran && p.ACTIVE == true).Count() > 0)
                    {
                        Database.planmealcustinvoiceHD ObjHD = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == Mytran && p.planid == PID && p.ACTIVE == true);
                        bool onhold = Convert.ToBoolean(ObjHD.SubscriptionOnHold);
                        if (onhold == true)
                        {
                            pnlWarningMsg.Visible = true;
                            string CIDName = DrpCustomer.SelectedItem.ToString();
                            lblWarningMsg.Text = CIDName + " Customer Is OnHold.....";
                        }
                        int Driver = Convert.ToInt32(ObjHD.DefaultDriverID);
                        int MYTRANSID = Convert.ToInt32(ObjHD.MYTRANSID);
                        /////////////////////////////////////HD
                        List<Database.planmealcustinvoice> ListDT = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PID).ToList();
                        var DT = ListDT.GroupBy(i => new { i.CustomerID, i.planid, i.MYTRANSID }).Select(g => g.FirstOrDefault()).ToArray();
                        string Weekofday = DT.FirstOrDefault().WeekofDay.ToString();
                        /////////////////////////////////////DT
                        lblcustomer.Text = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COMPNAME1 : "";
                        lblplan.Text = DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == PID).Count() > 0 ? DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == PID).planname1 : "";
                        lblcontractno.Text = ObjHD.MYTRANSID.ToString();
                        lblcontractDate.Text = Convert.ToDateTime(ObjHD.ContractDate).ToString("dd/MMM/yyyy");
                        lbldriver.Text = DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == Driver).Count() > 0 ? DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == Driver).firstname : "";
                        lblweekofday.Text = Weekofday;
                        lblBeginDate.Text = Convert.ToDateTime(ObjHD.StartDate).ToString("dd/MMM/yyyy");
                        lblenddate.Text = Convert.ToDateTime(ObjHD.EndDate).ToString("dd/MMM/yyyy");
                        ////////////////////////////////TotalDay
                        List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                        List<Database.planmealcustinvoice> ListtotaldelDay = List.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int totalDelDay = ListtotaldelDay.Count();
                        lbldaystotal.Text = ListtotaldelDay.Count().ToString();
                        ////////////////////////////////////Delivered & Remaining 
                        List<Database.planmealcustinvoice> ListdelDay = List.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int TimeCount = ListdelDay.Count();
                        lblRemaining.Text = TimeCount.ToString();

                        List<Database.planmealcustinvoice> ListTotaldeldone = List.Where(p => p.ActualDelDate != null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int DoneCount = ListTotaldeldone.Count();
                        lblDelivered.Text = DoneCount.ToString();


                        List<Database.planmealcustinvoice> ListDTTime = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PID).GroupBy(p => p.DeliveryMeal).Select(p => p.FirstOrDefault()).ToList();
                        if (ListDTTime.Count() > 0)
                        {
                            foreach (Database.planmealcustinvoice item in ListDTTime)
                            {
                                int Mealname = Convert.ToInt32(item.MealType);
                                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Mealname).Count() > 0)
                                {
                                    string Name = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Mealname).REFNAME1;
                                    MealTname += Name + ", ";
                                }
                            }
                        }
                        lblDeliveryTime.Text = MealTname;
                    }
                }
            }
        }
        public void DRPBind()
        {
            // Bind Customr
            List<Database.planmealcustinvoiceHD> CustomerList = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
            List<Database.TBLCOMPANYSETUP> NewCustomerList = new List<Database.TBLCOMPANYSETUP>();
            foreach (Database.planmealcustinvoiceHD item in CustomerList)
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == item.CustomerID).Count() > 0)
                {
                    Database.TBLCOMPANYSETUP obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == item.CustomerID);
                    NewCustomerList.Add(obj);
                }
            }
            DrpCustomer.DataSource = NewCustomerList.OrderBy(p => p.COMPNAME1);
            DrpCustomer.DataTextField = "COMPNAME1";
            DrpCustomer.DataValueField = "COMPID";
            DrpCustomer.DataBind();
            DrpCustomer.Items.Insert(0, new ListItem("-- Select --", "0"));

            //Bind Plan

            List<Database.planmealcustinvoiceHD> Listplan = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            foreach (Database.planmealcustinvoiceHD pitem in Listplan)
            {
                if (DB.tblProduct_Plan.Where(p => p.planid == pitem.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == pitem.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Add(obj);
                }
            }
            DrpPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planname1);
            DrpPlan.DataTextField = "planname1";
            DrpPlan.DataValueField = "planid";
            DrpPlan.DataBind();
            DrpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public string GetPlan(int PID)
        {
            if (DB.tblProduct_Plan.Where(p => p.planid == PID && p.TenentID == TID).Count() > 0)
                return DB.tblProduct_Plan.SingleOrDefault(p => p.planid == PID && p.TenentID == TID).planname1;
            else
                return "Not Found";
        }
        public string GetMeal1(int MID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == MID).Count() > 0)
                return DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFID == MID).REFNAME1;
            else
                return "Not Found";
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            pnlhead.Visible = true;
            ListBind();
        }

        protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cid = Convert.ToInt32(DrpCustomer.SelectedValue);
            List<Database.planmealcustinvoiceHD> Listplan = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == cid).ToList();
            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            List<PlanList> List = new List<PlanList>();
            foreach (Database.planmealcustinvoiceHD pitem in Listplan)
            {
                if (DB.tblProduct_Plan.Where(p => p.planid == pitem.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == pitem.planid && p.TenentID == TID);
                    PlanList blank = new PlanList();
                    blank.Plannname = obj.planname1;
                    blank.Planid = obj.planid;
                    blank.Mytranceid = pitem.MYTRANSID;
                    List.Add(blank);
                    
                }
            }
            if(List.Count() > 0)
            {
                var Datas = from item in List
                            select new
                            {
                                planname1 = item.Plannname + " (" + ((Int32)item.Mytranceid).ToString()+")", // + " - " + ((Int64)item.MYTRANSID).ToString() + " - " + (Convert.ToDateTime(item.ENTRYDATE).ToString("dd/MM/yyyy")) + " - " + item.TransDocNo,
                                ID = ((Int32)item.Planid).ToString() + " - " + ((Int32)item.Mytranceid).ToString()
                            };
                DrpPlan.DataSource = Datas;
                DrpPlan.DataTextField = "planname1";
                DrpPlan.DataValueField = "ID";
                DrpPlan.DataBind();
            }
            else
            {
                DrpPlan.DataSource = ListtblProduct_Plan;
                DrpPlan.DataTextField = "planname1";
                DrpPlan.DataValueField = "planid";
                DrpPlan.DataBind();
            }
            
            //int cid = Convert.ToInt32(DrpCustomer.SelectedValue);
            //List<Database.planmealcustinvoiceHD> Listplan = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == cid).GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
            //List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            //foreach (Database.planmealcustinvoiceHD pitem in Listplan)
            //{
            //    if (DB.tblProduct_Plan.Where(p => p.planid == pitem.planid && p.TenentID == TID).Count() > 0)
            //    {
            //        Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == pitem.planid && p.TenentID == TID);
            //        ListtblProduct_Plan.Add(obj);
            //    }
            //}
            //DrpPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planname1);
            //DrpPlan.DataTextField = "planname1";
            //DrpPlan.DataValueField = "planid";
            //DrpPlan.DataBind();
        }


        public class PlanList
        {
            public string Plannname { get; set; }
            public int Planid { get; set; }
            public int Mytranceid { get; set; }
        }

    }
}