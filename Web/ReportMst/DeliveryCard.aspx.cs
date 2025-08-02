using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.Services;

namespace Web.ReportMst
{
    public partial class DeliveryCard : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<Database.planmealcustinvoice> List = new List<Database.planmealcustinvoice>();

        int TID = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            healtybar1.ImageUrl = "../assets/" + Loggo;            
            if (!IsPostBack)
            {
                if (Request.QueryString["FDT"] != null && Request.QueryString["TDT"] != null)
                {
                    Client();
                    var FDT = Convert.ToDateTime(Request.QueryString["FDT"]).Date;
                    var TDT = Convert.ToDateTime(Request.QueryString["TDT"]).Date;
                    txtdateFrom.Text = FDT.ToString("dd/MMM/yyyy");
                    txtdateTO.Text = TDT.ToString("dd/MMM/yyyy");
                    Page.Header.Title = String.Format("Delivery Card");
                    Listdata();
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

                    txtdateFrom.Text = txtdateTO.Text = DT.ToString("dd/MMM/yyyy");//Convert.ToDateTime(DateTime.Now).ToString("dd/MMM/yyyy");
                    Page.Header.Title = String.Format("Delivery Card");
                    Client();
                }
            }
        }
        public void CardBind()
        {
            List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        public void Client()
        {
            List<Database.TBLCOMPANYSETUP> ClientFinalList = new List<Database.TBLCOMPANYSETUP>();
            List<Database.planmealcustinvoice> ClientList = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
            
            foreach (Database.planmealcustinvoice item in ClientList)
            {
                if(item.CustomerID != 0)
                {
                    if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == item.CustomerID).Count() > 0)
                    {
                        Database.TBLCOMPANYSETUP obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == item.CustomerID);
                        ClientFinalList.Add(obj);
                    }
                } 
            }
            ViewState["SearchFT"] = ClientFinalList;
           
            DrpFromClient.DataSource = ClientFinalList.OrderBy(p => p.COMPID);
            DrpFromClient.DataTextField = "COMPNAME1";
            DrpFromClient.DataValueField = "COMPID";
            DrpFromClient.DataBind();           

            DrpToClient.DataSource = ClientFinalList.OrderByDescending(p => p.COMPID);
            DrpToClient.DataTextField = "COMPNAME1";
            DrpToClient.DataValueField = "COMPID";
            DrpToClient.DataBind();

            List<Database.TBLCOMPANYSETUP> searchList = ClientFinalList.OrderBy(p => p.COMPID).ToList();
            string Max = searchList.Max(p => p.COMPID).ToString();
            string min = searchList.Min(p => p.COMPID).ToString();
            txtFromSearchByID.Text = min;
            txttToSearchByID.Text = Max;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.ToList();
            Listdata();
        }
        public void Listdata()
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
            int FromClient = Convert.ToInt32(DrpFromClient.SelectedValue);
            int Toclient = Convert.ToInt32(DrpToClient.SelectedValue);

            List<Database.planmealcustinvoiceHD> HDList = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.SubscriptionOnHold == false).ToList();
            List<Database.planmealcustinvoice> DTList = new List<Database.planmealcustinvoice>();
            foreach (Database.planmealcustinvoiceHD HDItem in HDList)
            {
                List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == HDItem.MYTRANSID).ToList();
                foreach (Database.planmealcustinvoice DTitems in List)
                {
                    if (List.Where(p => p.TenentID == TID && p.MYTRANSID == DTitems.MYTRANSID && p.DeliveryID == DTitems.DeliveryID && p.MYPRODID == DTitems.MYPRODID).Count() > 0)
                    {
                        Database.planmealcustinvoice objDT = List.Single(p => p.TenentID == TID && p.MYTRANSID == DTitems.MYTRANSID && p.DeliveryID == DTitems.DeliveryID && p.MYPRODID == DTitems.MYPRODID);
                        DTList.Add(objDT);
                    }
                }
            }
            List = DTList; //DB.planmealcustinvoices.Where(p => p.TenentID == TID).ToList();

            List = List.Where(p => (p.ExpectedDelDate.Value.Date >= FDT.Value.Date && p.ExpectedDelDate.Value.Date <= TDT.Value.Date) && (p.CustomerID >= FromClient && p.CustomerID <= Toclient)).ToList();//.GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();           
            ViewState["all"] = List.GroupBy(i => new { i.CustomerID, i.DayNumber, i.planid, i.MYTRANSID }).Select(g => g.FirstOrDefault()).ToArray().OrderBy(p => p.CustomerID).ToList();
            var cats = List.GroupBy(i => new { i.CustomerID, i.DayNumber, i.planid, i.MYTRANSID }).Select(g => g.FirstOrDefault()).ToArray().OrderBy(p => p.CustomerID);

            Listview1.DataSource = cats;
            Listview1.DataBind();
        }
        public string GetCNAME(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                string CustomerNAME = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COMPNAME1;
                return CID + " - " + CustomerNAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetPHONE(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                string phoneNO = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).MOBPHONE;
                string BusNO = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).BUSPHONE1;
                return phoneNO + "," + BusNO;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetDriver(int ID)
        {
            if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == ID).Count() > 0)
            {
                string DriverName = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID).firstname;
                return DriverName;
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
                    if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true).Count() > 0)
                    {
                        Database.TBLCONTACT_DEL_ADRES obj = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true);
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
        //public string GetAdd(int CID)
        //{
        //    if(DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).Count() > 0)
        //    {
        //        Database.TBLCONTACT_DEL_ADRES obj = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).FirstOrDefault();
        //        string Address1 = obj.ADDR1;//DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID).ADDR1;
        //        string Address2 = obj.ADDR2;//DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID).ADDR2;
        //        return Address1 + "," + Address2;
        //    }
        //    else
        //    {
        //        return "Not Found";
        //    }
        //}

        protected void lblAtion_Click(object sender, EventArgs e)
        {
            pnlWarningMsg.Visible = false;
            List<Database.planmealcustinvoice> TempList = new List<Database.planmealcustinvoice>();
            List<Database.planmealcustinvoice> ListALL = ((List<Database.planmealcustinvoice>)ViewState["all"]).ToList();
            if (CHKALLPrint.Checked == true)
            {
                foreach (Database.planmealcustinvoice item in ListALL)
                {
                    //Database.planmealcustinvoice Newobj = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.CustomerID == item.CustomerID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.ExpectedDelDate == item.ExpectedDelDate);
                    TempList.Add(item);
                }
            }
            else
            {
                for (int i = 0; i < Listview1.Items.Count; i++)
                {
                    CheckBox CHKAction = (CheckBox)Listview1.Items[i].FindControl("CHKAction");
                    if (CHKAction.Checked == true)
                    {
                        Label lblCustomerID = (Label)Listview1.Items[i].FindControl("lblCustomerID");
                        Label lblpaln = (Label)Listview1.Items[i].FindControl("lblpaln");
                        Label lblMealType = (Label)Listview1.Items[i].FindControl("lblMealType");
                        Label lblmytransiid = (Label)Listview1.Items[i].FindControl("lblmytransiid");
                        Label lbldeliveryID = (Label)Listview1.Items[i].FindControl("lbldeliveryID");
                        Label lblexpdate = (Label)Listview1.Items[i].FindControl("lblexpdate");
                        //
                        int CID = Convert.ToInt32(lblCustomerID.Text);
                        int MYTRANCEID = Convert.ToInt32(lblmytransiid.Text);
                        int DELIVERYID = Convert.ToInt32(lbldeliveryID.Text);
                        var ExpDate = Convert.ToDateTime(lblexpdate.Text).Date;

                        List<Database.planmealcustinvoice> OBJLIST = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == MYTRANCEID && p.DeliveryID == DELIVERYID && p.ExpectedDelDate == ExpDate).ToList();//.GroupBy(p=>p.CustomerID).Select(p=>p.FirstOrDefault()).ToList();
                        foreach (Database.planmealcustinvoice item in OBJLIST)
                        {
                            //Database.planmealcustinvoice Newobj = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.ExpectedDelDate == ExpDate);
                            //TempList.Add(Newobj);
                            TempList.Add(item);
                        }
                    }
                }
            }
            if (TempList.Count() == 0)
            {
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = "Please Checked Atlist One Checkbox In Delivery Card List.....";
                return;
            }
            ViewState["all"] = null;
            Session["TempList"] = TempList;
            Response.Redirect("/Master/HB_DeliveryCard.aspx");
        }

        protected void CHKAction_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox CHKAction = (CheckBox)Listview1.Items[i].FindControl("CHKAction");
                if (CHKALLPrint.Checked == true)
                {
                    CHKAction.Checked = true;
                }
                else
                {
                    CHKAction.Checked = false;
                }
            }
        }

        protected void txtFromSearchByID_TextChanged(object sender, EventArgs e)
        {
            int FromSearch = 0;
            int TOSearch = 0;
            pnlWarningMsg.Visible = false;
            if (txtFromSearchByID.Text != "")
            {
                FromSearch = Convert.ToInt32(txtFromSearchByID.Text);
                txttToSearchByID.Text = FromSearch.ToString();
                if (txttToSearchByID.Text != "")
                {
                    TOSearch = Convert.ToInt32(txttToSearchByID.Text);
                    if (FromSearch <= TOSearch)
                    { }
                    else
                    {
                        pnlWarningMsg.Visible = true;
                        lblWarningMsg.Text = "Please Subscriber From_ID is Less Than Or Equeal to TO_ID";
                        return;
                    }
                }
                List<Database.TBLCOMPANYSETUP> list = ((List<Database.TBLCOMPANYSETUP>)ViewState["SearchFT"]);
                list = list.OrderBy(p => p.COMPID).ToList();
                if (list.Where(p => p.COMPID == FromSearch && p.TenentID == TID).Count() < 1)
                {
                    pnlWarningMsg.Visible = true;
                    lblWarningMsg.Text = "You Have No Subscriber For This ID";
                }
                else
                {
                    DrpFromClient.SelectedValue = FromSearch.ToString();
                    DrpToClient.SelectedValue = TOSearch.ToString();
                }

            }
        }

        protected void txttToSearchByID_TextChanged(object sender, EventArgs e)
        {
            if (txttToSearchByID.Text != "")
            {
                pnlWarningMsg.Visible = false;
                int Tosearch = Convert.ToInt32(txttToSearchByID.Text);
                if (txtFromSearchByID.Text != "")
                {
                    int Fromsearch = Convert.ToInt32(txtFromSearchByID.Text);
                    if (Tosearch >= Fromsearch)
                    {

                    }
                    else
                    {
                        pnlWarningMsg.Visible = true;
                        lblWarningMsg.Text = "Please Subscriber To_ID is Greater Than Or Equeal to From_ID";
                        return;
                    }
                }
                List<Database.TBLCOMPANYSETUP> list = ((List<Database.TBLCOMPANYSETUP>)ViewState["SearchFT"]);
                list = list.OrderBy(p => p.COMPID).ToList();
                if (list.Where(p => p.COMPID == Tosearch && p.TenentID == TID).Count() < 1)
                {
                    pnlWarningMsg.Visible = true;
                    lblWarningMsg.Text = "You Have No Subscriber For This ID";
                }
                else
                {
                    DrpToClient.SelectedValue = Tosearch.ToString();
                }
            }
        }



    }
}