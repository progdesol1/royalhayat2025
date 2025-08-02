using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class AMPMnewPrint : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
           
            if(!IsPostBack)
            {
                if (Session["TempList"] != null)
                {
                    List<Database.planmealcustinvoice> ListItemss = ((List<Database.planmealcustinvoice>)Session["TempList"]).ToList();
                    //Listview1.DataSource = ListItem;
                    //Listview1.DataBind();
                    ListItemss = ListItemss.GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).OrderBy(p=>p.ExpectedDelDate).ThenBy(p=>p.DriverID).ToList();                    
                    List<Database.planmealcustinvoice> TempPlanmealinvoice = new List<Database.planmealcustinvoice>();
                    bool checkdate = false;
                    DateTime? checkDT = null;
                    int Driver = 0;
                    foreach (Database.planmealcustinvoice itemmss in ListItemss)
                    {
                        if (checkDT == itemmss.ExpectedDelDate && Driver == itemmss.DriverID)
                        {
                            checkdate = true;
                        }
                        else
                        {
                            checkdate = false;
                        }
                        if (checkdate == false)
                        {
                            if (DB.planmealcustinvoices.Where(p => p.TenentID == itemmss.TenentID && p.CustomerID == itemmss.CustomerID && p.DeliveryID == itemmss.DeliveryID && p.ExpectedDelDate == itemmss.ExpectedDelDate).Count() > 0)
                            {
                                Database.planmealcustinvoice obj = DB.planmealcustinvoices.Single(p => p.TenentID == itemmss.TenentID && p.CustomerID == itemmss.CustomerID && p.DeliveryID == itemmss.DeliveryID && p.ExpectedDelDate == itemmss.ExpectedDelDate);
                                checkDT = obj.ExpectedDelDate;
                                Driver = Convert.ToInt32(obj.DriverID);
                                TempPlanmealinvoice.Add(obj);
                            }
                        }
                    }
                    ListViewDriverhead.DataSource = TempPlanmealinvoice; // ListItemss;
                    ListViewDriverhead.DataBind();
                }
            }
        }
        public string GetDriver(int ID)
        {
            if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == ID).Count() > 0)
            {
                string DriverNAme = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID).firstname;
                return DriverNAme;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getMealType1(int Type1)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Type1 && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").Count() > 0)
            {
                string TypeMeal = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Type1 && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").REFNAME1;
                return TypeMeal;
            }
            else
            {
                return "";
            }
        }
        public string GetCustomer(int ID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ID).Count() > 0)
            {
                string CusName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID).COMPNAME1;
                return ID + "-" + CusName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetPhone(int ID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ID).Count() > 0)
            {
                string Phone = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID).MOBPHONE;
                return Phone;
            }
            else
            {
                return "Not Found";
            }
        }
        //First Listview Head data
        public string GetTime(int ID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").Count() > 0)
            {
                string Time = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").REFNAME1;
                return Time;
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
                        string Building = obj.Building != null ? obj.Building.ToString() : "";
                        string Street = obj.Street != null ? obj.Street.ToString() : "";
                        string Lane = obj.Lane != null ? obj.Lane.ToString() : "";
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReportMst/AMPMnew123.aspx");
        }

        protected void ListViewDriverhead_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView Listview1 = (ListView)e.Item.FindControl("Listview1");
            Label lbldriverID = (Label)e.Item.FindControl("lbldriverID");
            Label lblcustomerID = (Label)e.Item.FindControl("lblcustomerID");
            Label lblplan = (Label)e.Item.FindControl("lblplan");
            Label lblExpDelDate = (Label)e.Item.FindControl("lblExpDelDate");
            System.Web.UI.WebControls.Image HealtybarLogo = (System.Web.UI.WebControls.Image)e.Item.FindControl("HealtybarLogo");
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;

            int driverID = Convert.ToInt32(lbldriverID.Text);
            int CID = Convert.ToInt32(lblcustomerID.Text);
            int plan = Convert.ToInt32(lblplan.Text);
            DateTime ExpDelDate = Convert.ToDateTime(lblExpDelDate.Text);
            if (Session["TempList"] != null)
            {

                List<Database.planmealcustinvoice> ListSecond = ((List<Database.planmealcustinvoice>)Session["TempList"]).ToList();
                ListSecond = ListSecond.Where(p => p.ExpectedDelDate == ExpDelDate && p.DriverID == driverID).ToList();
                var cats = ListSecond.GroupBy(i => new { i.CustomerID, i.planid, i.MYTRANSID }).Select(g => g.FirstOrDefault()).ToArray().OrderBy(p => p.CustomerID);
                Listview1.DataSource = cats;
                Listview1.DataBind();
            }
            
            //Listview1.DataSource = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == plan && p.ExpectedDelDate == ExpDelDate && p.DriverID == driverID);
            //Listview1.DataBind();

        }
        public string GetPlan(int PlanID)
        {
            if(DB.tblProduct_Plan.Where(p => p.planid == PlanID && p.TenentID == TID).Count() > 0)
            {
                string PlanName = DB.tblProduct_Plan.Single(p => p.planid == PlanID && p.TenentID == TID).planname1;
                return PlanName;
            }
            else
            {
                return "'Not Found'";
            }
        }
        public string getstasus(DateTime ADT,DateTime RDT)
        {
            string AADT = Convert.ToDateTime(ADT).ToShortDateString();
            string RRDT = Convert.ToDateTime(RDT).ToShortDateString();
            if (RRDT != "1/1/0001")
            {
                return "Return";
            }
            else if(AADT != "1/1/0001")
            {
                return "Delivered";
            }
            
            else
            {
                return "Pending";
            }
        }

    }
}