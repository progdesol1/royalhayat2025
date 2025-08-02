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
using System.Net;
using System.IO;



namespace Web.ReportMst
{
    public partial class CustomerList : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<Database.TBLCOMPANYSETUP> FinelCustomerList = new List<Database.TBLCOMPANYSETUP>();
        int TID = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            healthybar1.ImageUrl = "../assets/" + Loggo;
            FinelCustomerList = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.CompanyType == "82003" && (p.Active != "" || p.Active != null)).ToList();
            Page.Header.Title = String.Format("Customer List");
            if (!IsPostBack)
            {
                if (Request.QueryString["FDT"] != null && Request.QueryString["TDT"] != null)
                {
                    DateTime FDT = Convert.ToDateTime(Request.QueryString["FDT"]);
                    DateTime TDT = Convert.ToDateTime(Request.QueryString["TDT"]);
                    txtdateFrom.Text = FDT.ToString("dd/MMM/yyyy");
                    txtdateTO.Text = TDT.ToString("dd/MMM/yyyy");
                    
                    List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID).ToList();
                    List = List.Where(p => (p.ExpectedDelDate.Value.Date >= FDT && p.ExpectedDelDate.Value.Date <= TDT)).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
                    List<Database.TBLCOMPANYSETUP> NewCustomerList = new List<Database.TBLCOMPANYSETUP>();
                    
                    foreach (Database.planmealcustinvoice item in List)
                    {
                        if (FinelCustomerList.Where(p => p.TenentID == TID && p.COMPID == item.CustomerID).Count() > 0)
                        {
                            Database.TBLCOMPANYSETUP obj = FinelCustomerList.Single(p => p.TenentID == TID && p.COMPID == item.CustomerID);
                            NewCustomerList.Add(obj);
                        }
                    }
                    ListView1.DataSource = NewCustomerList;//DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y");
                    ListView1.DataBind();
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
                }                
                //ListBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ListBind();
        }
        public void ListBind()
        {
            pnlWarningMsg.Visible = false;
            List<Database.TBLCOMPANYSETUP> NewCustomerList = new List<Database.TBLCOMPANYSETUP>();
            //List<Database.TBLCOMPANYSETUP> FinelCustomer = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).ToList();
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

            if (ChkAll.Checked == true || ChkActive.Checked == true || ChkDeactive.Checked == true)
            {
                if (ChkAll.Checked == true)
                {
                    NewCustomerList = FinelCustomerList.ToList();
                }
                else if(ChkActive.Checked == true)
                {
                    NewCustomerList = FinelCustomerList.Where(p => p.TenentID == TID && p.Active == "Y").ToList();
                }
                else if(ChkDeactive.Checked == true)
                {
                    NewCustomerList = FinelCustomerList.Where(p => p.TenentID == TID && p.Active == "N").ToList();
                }
                ListView1.DataSource = NewCustomerList;
                ListView1.DataBind();
            }
            else
            {
                if (TDT < FDT)
                {
                    pnlWarningMsg.Visible = true;
                    lblWarningMsg.Text = "Please Select Delivery To_date Is Greate Than From_date.....";
                    return;
                }
                List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID).ToList();
                List = List.Where(p => (p.ExpectedDelDate.Value.Date >= FDT.Value.Date && p.ExpectedDelDate.Value.Date <= TDT.Value.Date)).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
                
                foreach (Database.planmealcustinvoice item in List)
                {
                    if (FinelCustomerList.Where(p => p.TenentID == TID && p.COMPID == item.CustomerID).Count() > 0)
                    {
                        Database.TBLCOMPANYSETUP obj = FinelCustomerList.Single(p => p.TenentID == TID && p.COMPID == item.CustomerID);
                        NewCustomerList.Add(obj);
                    }
                }
                ListView1.DataSource = NewCustomerList;//DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y");
                ListView1.DataBind();
            }
        }
        public string Civil(int ComID)
        {
            if (FinelCustomerList.Where(p => p.TenentID == TID && p.COMPID == ComID).Count() > 0)
            {
                string Name = FinelCustomerList.Single(p => p.TenentID == TID && p.COMPID == ComID).CivilID;
                if (Name == null)
                {
                    return "Not Found";
                }
                else
                {
                    Name = Name.Trim();
                    if (Name == "")
                        return "Not Found";
                    else
                        return Name;
                }                
            }
            else
            {
                return "Not Found";
            }
        }
        public string Mail(int ComID)
        {
            if (FinelCustomerList.Where(p => p.TenentID == TID && p.COMPID == ComID).Count() > 0)
            {
                string Name = FinelCustomerList.Single(p => p.TenentID == TID && p.COMPID == ComID).EMAIL1;
                if (Name == null)
                {
                    return "Not Found";
                }
                else
                {
                    Name = Name.Trim();
                    if (Name == "")
                        return "Not Found";
                    else
                        return Name;
                }                
            }
            else
            {
                return "Not Found";
            }
        }
        public string STATE(int ComID)
        {
            if (FinelCustomerList.Where(p => p.TenentID == TID && p.COMPID == ComID).Count() > 0)
            {
                Database.TBLCOMPANYSETUP obj = FinelCustomerList.Single(p => p.TenentID == TID && p.COMPID == ComID);
                if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ComID && p.Defualt == true).Count() > 0)
                {
                    Database.TBLCONTACT_DEL_ADRES ObjST = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == ComID && p.Defualt == true);
                    int Country = Convert.ToInt32(ObjST.COUNTRYID);
                    int Stateid = Convert.ToInt32(ObjST.STATE);
                    if (DB.tblStates.Where(p => p.COUNTRYID == Country && p.StateID == Stateid).Count() > 0)
                    {
                        string Statename = DB.tblStates.Single(p => p.COUNTRYID == Country && p.StateID == Stateid).MYNAME1;
                        return Statename;
                    }
                    else
                    {
                        return "Not Found";
                    }
                }
                else
                {
                    return "Not Found";
                }
            }
            else
            {
                return "Not Found";
            }
        }
        public string CITY(int ComID)
        {
            if (FinelCustomerList.Where(p => p.TenentID == TID && p.COMPID == ComID).Count() > 0)
            {
                Database.TBLCOMPANYSETUP obj = FinelCustomerList.Single(p => p.TenentID == TID && p.COMPID == ComID);
                if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ComID && p.Defualt == true).Count() > 0)
                {
                    Database.TBLCONTACT_DEL_ADRES ObjST = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == ComID && p.Defualt == true);
                    int Country = Convert.ToInt32(ObjST.COUNTRYID);
                    int Stateid = Convert.ToInt32(ObjST.STATE);
                    int Cityid = Convert.ToInt32(ObjST.CITY);
                    if (DB.tblCityStatesCounties.Where(p => p.COUNTRYID == Country && p.StateID == Stateid && p.CityID == Cityid).Count() > 0)
                    {
                        string Statename = DB.tblCityStatesCounties.Single(p => p.COUNTRYID == Country && p.StateID == Stateid && p.CityID == Cityid).CityEnglish;
                        return Statename;
                    }
                    else
                    {
                        return "Not Found";
                    }
                }
                else
                {
                    return "Not Found";
                }
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
        public string GetPlan(int CID)
        {
            List<Database.planmealcustinvoice> Listplan = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID).GroupBy(i => new { i.CustomerID,i.planid,i.MYTRANSID}).Select(g=>g.FirstOrDefault()).ToList();
            string PlanName = "";
            string ret = "";
            int planID = 0;
            foreach(Database.planmealcustinvoice item in Listplan)
            {
                if (Listplan.Where(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID).Count() > 0)
                {
                    Database.planmealcustinvoice obj = Listplan.Single(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID);
                    planID = obj.planid;
                    if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == planID).Count() > 0)
                    {
                        PlanName = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == planID).planname1;
                        ret += PlanName + "(" + planID.ToString() + "), ";
                    }
                }
            }
            return ret;
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if(ChkAll.Checked == true)
            {
                ChkActive.Checked = false;
                ChkDeactive.Checked = false;
                ListBind();
               
            }
        }

        protected void ChkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkActive.Checked == true)
            {
                ChkAll.Checked = false;
                ChkDeactive.Checked = false;
                ListBind();
               
            }
        }

        protected void ChkDeactive_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDeactive.Checked == true)
            {
                ChkAll.Checked = false;
                ChkActive.Checked = false;
                ListBind();
                
            }
        }
        public string GetStatus(string sta)
        {
            if(sta == "Y")
            {
                return "Active";                
            }
            else
            {
                return "Deactive";
            }
        }
       



    }
}