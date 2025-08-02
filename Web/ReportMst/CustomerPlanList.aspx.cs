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


namespace Web.ReportMst
{
    public partial class CustomerPlanList : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<Database.planmealcustinvoice> ListMain = new List<Database.planmealcustinvoice>();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {            
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;
            Page.Header.Title = String.Format("Customer Plan List");
            if (!IsPostBack)
            {
                if (Request.QueryString["FDT"] != null && Request.QueryString["TDT"] != null)
                {
                    BindDrpList();
                    var FDT = Convert.ToDateTime(Request.QueryString["FDT"]).Date;
                    var TDT = Convert.ToDateTime(Request.QueryString["TDT"]).Date;
                    txtdateFrom.Text = FDT.ToString("dd/MMM/yyyy");
                    txtdateTO.Text = TDT.ToString("dd/MMM/yyyy");
                    LBind();
                }
                else
                {
                    int maxdt = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1)).Day;
                    int pulsedt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).Day;
                    DateTime DT;
                    if (maxdt == pulsedt)
                        DT = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                    else
                        DT = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1);
                    txtdateFrom.Text = txtdateTO.Text = DT.ToString("dd/MMM/yyyy");//DateTime.Now.ToString("dd/MMM/yyyy");
                    BindDrpList();
                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            
            LBind();
        }
        public void LBind()
        {
            pnlWarningMsg.Visible = false;
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            else
                FDT = DateTime.Now;

            if (txtdateTO.Text != "")
                TDT = Convert.ToDateTime(txtdateTO.Text);
            else
                TDT = DateTime.Now;

            if (TDT < FDT)
            {
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = "Please Select Delivery To_date Is Greate Than From_date.....";
                return;
            }
            int FromCustomer = Convert.ToInt32(DrpFromCustomer.SelectedValue);
            int ToCustomer = Convert.ToInt32(DrpToCustomer.SelectedValue);
            int FromPlan = Convert.ToInt32(DrpFromPlan.SelectedValue);
            int ToPlan = Convert.ToInt32(DrpToPlan.SelectedValue);
            //int FromMeal = Convert.ToInt32(DrpFromMeal.SelectedValue);
            //int ToMeal = Convert.ToInt32(DrpToMeal.SelectedValue);

            List<Database.planmealcustinvoiceHD> HDList = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.SubscriptionOnHold == false).ToList();
            List<Database.planmealcustinvoice> DTList = new List<Database.planmealcustinvoice>();
            foreach (Database.planmealcustinvoiceHD HDItem in HDList)
            {
                ListMain = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == HDItem.MYTRANSID).ToList();
                foreach (Database.planmealcustinvoice DTitems in ListMain)
                {
                    if (ListMain.Where(p => p.TenentID == TID && p.MYTRANSID == DTitems.MYTRANSID && p.DeliveryID == DTitems.DeliveryID && p.MYPRODID == DTitems.MYPRODID).Count() > 0)
                    {
                        Database.planmealcustinvoice objDT = ListMain.Single(p => p.TenentID == TID && p.MYTRANSID == DTitems.MYTRANSID && p.DeliveryID == DTitems.DeliveryID && p.MYPRODID == DTitems.MYPRODID);
                        DTList.Add(objDT);
                    }
                }
            }
            //ListMain = DTList;
            List<Database.planmealcustinvoice> Plan = new List<Database.planmealcustinvoice>();
            //Plan = DTList.Where(p => p.TenentID == TID && (p.ExpectedDelDate >= FDT && p.ExpectedDelDate <= TDT) && (p.CustomerID >= FromCustomer && p.CustomerID <= ToCustomer) && (p.planid >= FromPlan && p.planid <= ToPlan)).OrderBy(p => p.CustomerID).ToList();
            Plan = DTList.Where(p => p.TenentID == TID ).OrderBy(p => p.CustomerID).ToList();
            Plan = Plan.Where(p => p.ExpectedDelDate >= FDT && p.ExpectedDelDate <= TDT).ToList();
            Plan = Plan.Where(p => p.CustomerID >= FromCustomer && p.CustomerID <= ToCustomer).ToList();
            Plan = Plan.Where(p => p.planid >= FromPlan && p.planid <= ToPlan).ToList();

            //ListMain = DB.planmealcustinvoices.Where(p => p.TenentID == TID && (p.ExpectedDelDate >= FDT && p.ExpectedDelDate <= TDT) && (p.CustomerID >= FromCustomer && p.CustomerID <= ToCustomer) && (p.planid >= FromPlan && p.planid <= ToPlan) && (p.DeliveryMeal >= FromMeal && p.DeliveryMeal <= ToMeal)).OrderBy(p => p.CustomerID).ToList();

            var cats = Plan.GroupBy(i => new { i.CustomerID, i.planid, i.MYTRANSID }).Select(g => g.FirstOrDefault()).ToArray().OrderBy(p => p.CustomerID);

            ListView1.DataSource = cats;
            ListView1.DataBind();          

           
        }
        public void BindDrpList()
        {
            //Bind Customer
            List<Database.planmealcustinvoice> ListCustomer = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
            List<Database.TBLCOMPANYSETUP> NewCustomerList = new List<Database.TBLCOMPANYSETUP>();

            foreach (Database.planmealcustinvoice item in ListCustomer)
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == item.CustomerID).Count() > 0)
                {
                    Database.TBLCOMPANYSETUP obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == item.CustomerID);
                    NewCustomerList.Add(obj);
                }
            }
            DrpFromCustomer.DataSource = NewCustomerList.OrderBy(p => p.COMPID);
            DrpFromCustomer.DataTextField = "COMPNAME1";
            DrpFromCustomer.DataValueField = "COMPID";
            DrpFromCustomer.DataBind();

            DrpToCustomer.DataSource = NewCustomerList.OrderByDescending(p => p.COMPID);
            DrpToCustomer.DataTextField = "COMPNAME1";
            DrpToCustomer.DataValueField = "COMPID";
            DrpToCustomer.DataBind();
            //Bind Plan
            List<Database.planmealcustinvoice> Listplan = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            foreach (Database.planmealcustinvoice pitem in Listplan)
            {
                if (DB.tblProduct_Plan.Where(p => p.planid == pitem.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == pitem.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Add(obj);
                }
            }

            DrpFromPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planid);
            DrpFromPlan.DataTextField = "planname1";
            DrpFromPlan.DataValueField = "planid";
            DrpFromPlan.DataBind();

            DrpToPlan.DataSource = ListtblProduct_Plan.OrderByDescending(p => p.planid);
            DrpToPlan.DataTextField = "planname1";
            DrpToPlan.DataValueField = "planid";
            DrpToPlan.DataBind();
            //Bind Meal
            //List<Database.planmealcustinvoice> ListMeal = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.DeliveryMeal).Select(p => p.FirstOrDefault()).ToList();
            //List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            //foreach (Database.planmealcustinvoice Mitem in ListMeal)
            //{
            //    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Mitem.MealType).Count() > 0)
            //    {
            //        Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Mitem.MealType);
            //        ListREFTABLE.Add(obj2);
            //    }
            //}
            //DrpFromMeal.DataSource = ListREFTABLE.OrderBy(p => p.REFID);
            //DrpFromMeal.DataTextField = "REFNAME1";
            //DrpFromMeal.DataValueField = "REFID";
            //DrpFromMeal.DataBind();

            //DrpToMeal.DataSource = ListREFTABLE.OrderByDescending(p => p.REFID);
            //DrpToMeal.DataTextField = "REFNAME1";
            //DrpToMeal.DataValueField = "REFID";
            //DrpToMeal.DataBind();
        }
        public string GetCustomer(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                string CName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COMPNAME1;
                return CName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string Delivered(int CID, int Pid)
        {
            List<Database.planmealcustinvoice> ListDelivered = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Pid).ToList();
            List<Database.planmealcustinvoice> NewList = new List<Database.planmealcustinvoice>();
            if (ListDelivered.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Pid).Count() > 0)
            {
                foreach (Database.planmealcustinvoice item in ListDelivered)
                {
                    Database.planmealcustinvoice obj = ListDelivered.Single(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Pid && p.MYPRODID == item.MYPRODID && p.DeliveryID == item.DeliveryID);
                    if (obj.ActualDelDate != null)
                    { NewList.Add(obj); }
                    else
                    { }
                }
                int Countt = NewList.Count();
                return Countt.ToString();
            }
            else
            {
                return "Not Found";
            }
        }
        public string Pending(int CID, int Pid)
        {
            List<Database.planmealcustinvoice> ListDelivered = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Pid).ToList();
            List<Database.planmealcustinvoice> NewList = new List<Database.planmealcustinvoice>();
            if (ListDelivered.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Pid).Count() > 0)
            {
                foreach (Database.planmealcustinvoice item in ListDelivered)
                {
                    Database.planmealcustinvoice obj = ListDelivered.Single(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Pid && p.MYPRODID == item.MYPRODID && p.DeliveryID == item.DeliveryID);
                    if (obj.ActualDelDate == null)
                    { NewList.Add(obj); }
                    else
                    { }
                }
                int Countt = NewList.Count();
                return Countt.ToString();
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetAdd(int CID)
        {
            List<Database.TBLCONTACT_DEL_ADRES> ListItem = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
            string Address1 = "";
            string City = "";
            string State = "";
            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Defualt == true).Count() > 0)
            {
                foreach (Database.TBLCONTACT_DEL_ADRES itemon in ListItem)
                {
                    if (ListItem.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true).Count() > 0)
                    {
                        Database.TBLCONTACT_DEL_ADRES obj = ListItem.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true);
                        int countryID = Convert.ToInt32(obj.COUNTRYID);
                        int stateID = Convert.ToInt32(obj.STATE);
                        int cityID = Convert.ToInt32(obj.CITY);
                        string Building = obj.Building != null ? obj.Building.ToString() : "''";
                        string Street = obj.Street != null ? obj.Street.ToString() : "''";
                        string Lane = obj.Lane != null ? obj.Lane.ToString() : "''";
                        State = DB.tblStates.Where(p => p.COUNTRYID == countryID && p.StateID == stateID).Count() > 0 ? DB.tblStates.Single(p => p.COUNTRYID == countryID && p.StateID == stateID).MYNAME1 : "'Not Found'";
                        City = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).Count() > 0 ? DB.tblCityStatesCounties.Single(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).CityEnglish : "'Not Found'";
                        Address1 += obj.ADDR1 + "," + " Building=" + Building + " Street=" + Street + " Lane=" + Lane + " City=" + City + "," + " State=" + State + "</br>";
                    }
                }
                return Address1;
            }
            else
            {
                return "'Not Found'";
            }
        }
        public string GetPlan(int ID)
        {
            if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == ID).Count() > 0)
            {
                string PlanName = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == ID).planname1;
                return PlanName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getMealName(int MealType,int cid,int pid)
        {
            List<Database.planmealcustinvoice> Listplanmeal = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == cid && p.planid == pid).GroupBy(p=>p.DeliveryMeal).Select(p=>p.FirstOrDefault()).ToList();
            if (Listplanmeal.Count() > 0)
            {
                string MealTname = "";
                foreach(Database.planmealcustinvoice item in Listplanmeal)
                {
                    int Mealname = Convert.ToInt32(item.MealType);
                    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Mealname).Count() > 0)
                    {                       
                          string Name = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Mealname).REFNAME1;
                          MealTname += Name + ", ";
                    }
                    else
                    {
                        return "Not Found";
                    }
                }
                return MealTname;
            }
            else
            {
                return "Not Found";
            }
        }

        


    }
}