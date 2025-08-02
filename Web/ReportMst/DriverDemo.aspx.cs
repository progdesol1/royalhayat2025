using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using AjaxControlToolkit;

namespace Web.ReportMst
{
    public partial class DriverDemo : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 1;
        string FDELIVERY = "";
        string TDELIVERY = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            healthybar1.ImageUrl = "../assets/" + Loggo;
          
            if (!IsPostBack)
            {
               
                if (Request.QueryString["FDT"] != null && Request.QueryString["TDT"] != null)
                {
                    DRPBIND();
                    var FDT = Convert.ToDateTime(Request.QueryString["FDT"]).Date;
                    var TDT = Convert.ToDateTime(Request.QueryString["TDT"]).Date;
                    txtdateFrom.Text = FDT.ToString("dd/MMM/yyyy");
                    //txtdateTO1.Text = TDT.ToString("dd/MMM/yyyy");
                    LList();
                    FDELIVERY = DrpFromDelivery.SelectedItem.ToString();
                    //TDELIVERY = DrpToDelivery.SelectedItem.ToString();
                    Page.Header.Title = String.Format(FDELIVERY + "-" + TDELIVERY + " Driver checklist");
                }
                else if (Request.QueryString["DID"] != null)
                {

                    txtdateFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    DRPBIND();
                    LList();
                    FDELIVERY = DrpFromDelivery.SelectedItem.ToString();
                    Page.Header.Title = String.Format(FDELIVERY + "-" + TDELIVERY + " Driver checklist");
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

                    txtdateFrom.Text = DT.ToString("dd/MMM/yyyy");//DateTime.Now.ToString("dd/MMM/yyyy");
                    //txtdateTO1.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    DRPBIND();
                    if (DrpFromDelivery.SelectedItem != null)
                    FDELIVERY = DrpFromDelivery.SelectedItem.ToString();
                    //TDELIVERY = DrpToDelivery.SelectedItem.ToString();
                    Page.Header.Title = String.Format(FDELIVERY + "-" + TDELIVERY + " Driver checklist");                    
                }
                GoogleMapForASPNet1.GoogleMapObject.Width = "99%"; //map
                GoogleMapForASPNet1.GoogleMapObject.Height = "400px";//map
                GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;//map
            }
        }


        protected void btnDrawDirections_Click(object sender, EventArgs e)//map
        {
            GoogleMapForASPNet1.GoogleMapObject.Directions.ShowDirectionInstructions = chkShowDirections.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.HideMarkers = chkHideMarkers.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineColor = txtDirColor.Text;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineWeight = int.Parse(txtDirWidth.Text);
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineOpacity = double.Parse(txtDirOpacity.Text);
            //Provide addresses or postal codes in following lines
            GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Clear();
            GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtStartingpoint.Text);
            for (int i = 0; i <= ListView2.Items.Count - 1; i++)
            {

                TextBox txtFirstAddress = (TextBox)ListView2.Items[i].FindControl("txtFirstAddress");
                GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtFirstAddress.Text);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            FDELIVERY = DrpFromDelivery.SelectedItem.ToString();
            //TDELIVERY = DrpToDelivery.SelectedItem.ToString();
            Page.Header.Title = String.Format(FDELIVERY + "-" + TDELIVERY + " Driver checklist");
            LList();
            Sequence();

        }//DeliverTime
        public void DRPBIND()
        {
            List<Database.tbl_Employee> FinelDriver = DB.tbl_Employee.Where(p => p.TenentID == TID).ToList();
            List<Database.REFTABLE> FinelDeliveryTime = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").ToList();
            if (Request.QueryString["DID"] != null)
            {
                int DDID = Convert.ToInt32(Request.QueryString["DID"]);
                List<Database.tbl_Employee> ListEmployeedid = new List<Database.tbl_Employee>();
                List<Database.planmealcustinvoice> DriverListdid = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DriverID == DDID).GroupBy(p => p.DriverID).Select(p => p.FirstOrDefault()).ToList();
                foreach (Database.planmealcustinvoice itemEMP in DriverListdid)
                {
                    if (FinelDriver.Where(p => p.TenentID == TID && p.employeeID == itemEMP.DriverID).Count() > 0)
                    {
                        Database.tbl_Employee OBJEMP = FinelDriver.Single(p => p.TenentID == TID && p.employeeID == itemEMP.DriverID);
                        ListEmployeedid.Add(OBJEMP);
                    }
                }
                DrpFromDriver.DataSource = ListEmployeedid.Where(p => p.employeeID == DDID).OrderBy(p => p.employeeID);//DB.tbl_Employee.Where(p => p.TenentID == TID).OrderBy(p => p.employeeID);
                DrpFromDriver.DataTextField = "firstname";
                DrpFromDriver.DataValueField = "employeeID";
                DrpFromDriver.DataBind();
                DrpFromDriver.Items.Insert(0, new ListItem("-- Select --", "99999"));
            }
            else
            {
                List<Database.tbl_Employee> ListEmployee = new List<Database.tbl_Employee>();
                List<Database.planmealcustinvoice> DriverList = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.DriverID).Select(p => p.FirstOrDefault()).ToList();
                foreach (Database.planmealcustinvoice itemEMP in DriverList)
                {
                    if (FinelDriver.Where(p => p.TenentID == TID && p.employeeID == itemEMP.DriverID).Count() > 0)
                    {
                        Database.tbl_Employee OBJEMP = FinelDriver.Single(p => p.TenentID == TID && p.employeeID == itemEMP.DriverID);
                        ListEmployee.Add(OBJEMP);
                    }
                }
                DrpFromDriver.DataSource = ListEmployee.OrderBy(p => p.employeeID);//DB.tbl_Employee.Where(p => p.TenentID == TID).OrderBy(p => p.employeeID);
                DrpFromDriver.DataTextField = "firstname";
                DrpFromDriver.DataValueField = "employeeID";
                DrpFromDriver.DataBind();
                DrpFromDriver.Items.Insert(0, new ListItem("-- Select --", "99999"));

                //DrpToDriver.DataSource = ListEmployee.OrderByDescending(p => p.employeeID);//DB.tbl_Employee.Where(p => p.TenentID == TID).OrderByDescending(p => p.employeeID);
                //DrpToDriver.DataTextField = "firstname";
                //DrpToDriver.DataValueField = "employeeID";
                //DrpToDriver.DataBind();
            }

            List<Database.REFTABLE> ListDeliveriTime = new List<Database.REFTABLE>();
            List<Database.planmealcustinvoice> DeliveryTime = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.DeliveryTime).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.planmealcustinvoice itemEMP in DeliveryTime)
            {
                if (FinelDeliveryTime.Where(p => p.TenentID == TID && p.REFID == itemEMP.DeliveryTime && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").Count() > 0)
                {
                    Database.REFTABLE OBJTIME = FinelDeliveryTime.Single(p => p.TenentID == TID && p.REFID == itemEMP.DeliveryTime && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime");
                    ListDeliveriTime.Add(OBJTIME);
                }
            }

            DrpFromDelivery.DataSource = ListDeliveriTime.OrderBy(p => p.REFID);//DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").OrderBy(p => p.REFID);
            DrpFromDelivery.DataTextField = "REFNAME1";
            DrpFromDelivery.DataValueField = "REFID";
            DrpFromDelivery.DataBind();

            //DrpToDelivery.DataSource = ListDeliveriTime.OrderByDescending(p => p.REFID);//DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").OrderByDescending(p => p.REFID);
            //DrpToDelivery.DataTextField = "REFNAME1";
            //DrpToDelivery.DataValueField = "REFID";
            //DrpToDelivery.DataBind();           
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
        public string GetPlan(int PlanID)
        {
            if (DB.tblProduct_Plan.Where(p => p.planid == PlanID && p.TenentID == TID).Count() > 0)
            {
                string PlanName = DB.tblProduct_Plan.Single(p => p.planid == PlanID && p.TenentID == TID).planname1;
                return PlanName;
            }
            else
            {
                return "'Not Found'";
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
        public string GetCustomer(int ID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ID).Count() > 0)
            {
                string CusName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID).COMPNAME1;
                return ID + " - " + CusName;
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

        protected void CHKcheck_CheckedChanged(object sender, EventArgs e)
        {

            for (int j = 0; j < Listview1.Items.Count; j++)
            {
                CheckBox CHKPrint = (CheckBox)Listview1.Items[j].FindControl("CHKPrint");
                if (CHKcheck.Checked == true)
                {
                    CHKPrint.Checked = true;
                }
                else
                {
                    CHKPrint.Checked = false;
                }
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            List<Database.planmealcustinvoice> TempList = new List<Database.planmealcustinvoice>();
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox CHKPrint = (CheckBox)Listview1.Items[i].FindControl("CHKPrint");
                if (CHKPrint.Checked == true)
                {
                    Label lblCustomerID = (Label)Listview1.Items[i].FindControl("lblCustomerID");
                    Label lblmytransiid = (Label)Listview1.Items[i].FindControl("lblmytransiid");
                    Label lbldeliveryID = (Label)Listview1.Items[i].FindControl("lbldeliveryID");
                    Label lblDriver = (Label)Listview1.Items[i].FindControl("lblDriver");
                    Label lblExpdate = (Label)Listview1.Items[i].FindControl("lblExpdate");
                    //
                    int CID = Convert.ToInt32(lblCustomerID.Text);
                    int MYTRANCEID = Convert.ToInt32(lbldeliveryID.Text);
                    int DELIVERYID = Convert.ToInt32(lbldeliveryID.Text);
                    int Driver = Convert.ToInt32(lblDriver.Text);
                    DateTime Expdate = Convert.ToDateTime(lblExpdate.Text);

                    List<Database.planmealcustinvoice> OBJLIST = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID).ToList();//.GroupBy(p=>p.CustomerID).Select(p=>p.FirstOrDefault()).ToList();
                    foreach (Database.planmealcustinvoice item in OBJLIST)
                    {
                        if (OBJLIST.Where(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.DriverID == Driver && p.ExpectedDelDate == Expdate).Count() > 0)
                        {
                            Database.planmealcustinvoice Newobj = OBJLIST.Single(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.DriverID == Driver && p.ExpectedDelDate == Expdate);
                            TempList.Add(Newobj);
                        }

                    }
                }
            }
            if (TempList.Count() == 0)
            {
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = "Please Checked Atlist One Checkbox In Driver Checklist.....<br />Minimum one list in the Grid must be checked.....";
                return;
            }
            Session["TempList"] = TempList;
            Response.Redirect("/Master/AMPMnewPrint.aspx");
        }
        protected void btnSNNO_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                TextBox txtSerise = (TextBox)Listview1.Items[i].FindControl("txtSerise");
                Label lblCustomerID = (Label)Listview1.Items[i].FindControl("lblCustomerID");
                Label lbldeliveryID = (Label)Listview1.Items[i].FindControl("lbldeliveryID");
                Label lblmytransiid = (Label)Listview1.Items[i].FindControl("lblmytransiid");
                Label lblDriver = (Label)Listview1.Items[i].FindControl("lblDriver");
                Label lblExpdate = (Label)Listview1.Items[i].FindControl("lblExpdate");
                int CID = Convert.ToInt32(lblCustomerID.Text);
                int DelID = Convert.ToInt32(lbldeliveryID.Text);
                int DriverID = Convert.ToInt32(lblDriver.Text);
                int MyTrans = Convert.ToInt32(lblmytransiid.Text);
                DateTime EDT = Convert.ToDateTime(lblExpdate.Text);

                Database.planmealcustinvoice OBJ = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrans && p.DeliveryID == DelID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT);
                OBJ.DeliverySequence = Convert.ToInt32(txtSerise.Text);
                DB.SaveChanges();
            }
            LList();
            Sequence();

        }

        public void LList()
        {
            pnlWarningMsg.Visible = false;
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            else
                FDT = DateTime.Now;

            //if (txtdateTO1.Text != "")
            //    TDT = Convert.ToDateTime(txtdateTO1.Text);
            //else
            //    TDT = DateTime.Now;

            //if (TDT < FDT)
            //{
            //    //Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Warning, "Please Select Delivery To_date Is Greate Than From_date", "Warning!", Classes.Toastr.ToastPosition.TopCenter);
            //    pnlWarningMsg.Visible = true;
            //    lblWarningMsg.Text = "Please Select Delivery To_date Is Greate Than From_date.....";
            //    return;
            //}
            if (Request.QueryString["DID"] != null)
            {
                DateTime Todaydate = DateTime.Now;
                if (FDT <= Todaydate)
                {
                    pnlWarningMsg.Visible = true;
                    lblWarningMsg.Text = "You Don't Select Delivery Date Is Less Than Of Today Date.....";
                    return;
                }
            }
            int FDID = DrpFromDriver.SelectedValue !=""? Convert.ToInt32(DrpFromDriver.SelectedValue) : 99999;
            //int TDID = Convert.ToInt32(DrpToDriver.SelectedValue);
            int Fdelivery = Convert.ToInt32(DrpFromDelivery.SelectedValue);
            //int Tdelivery = Convert.ToInt32(DrpToDelivery.SelectedValue);

            List<Database.planmealcustinvoice> Listtemp = new List<planmealcustinvoice>();
            List<Database.planmealcustinvoice> List = new List<planmealcustinvoice>();//DB.planmealcustinvoices.Where(p => p.TenentID == TID).ToList();//DB.View_DriverCheckList.ToList();

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
            List = DTList;

            if (txtdateFrom.Text != "")// && txtdateTO1.Text != ""
            {
                Listview1.DataSource = List.Where(p => p.ExpectedDelDate.Value.Date == FDT.Value.Date && p.DriverID == FDID && p.DeliveryTime == Fdelivery).GroupBy(i => new { i.ExpectedDelDate, i.DriverID, i.CustomerID }).Select(p => p.FirstOrDefault()).ToArray().OrderBy(p => p.DeliverySequence).ToList();
                Listview1.DataBind();
                ListView2.DataSource = List.Where(p => p.ExpectedDelDate.Value.Date == FDT.Value.Date && p.DriverID == FDID && p.DeliveryTime == Fdelivery).GroupBy(i => new { i.ExpectedDelDate, i.DriverID, i.CustomerID }).Select(p => p.FirstOrDefault()).ToArray().OrderBy(p => p.DeliverySequence).ToList();
                ListView2.DataBind();

                //Listview1.DataSource = List.Where(p => p.ActualDelDate == null && ((p.ExpectedDelDate.Value.Date >= FDT.Value.Date && p.ExpectedDelDate.Value.Date <= TDT.Value.Date) && (p.DriverID >= FDID && p.DriverID <= TDID) && (p.DeliveryTime >= Fdelivery && p.DeliveryTime <= Tdelivery))).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).OrderBy(p => p.DeliverySequence).ToList();
                //Listview1.DataBind();
                //Listtemp = List;
                //ListView2.DataSource = List.Where(p => p.ActualDelDate == null && ((p.ExpectedDelDate.Value.Date >= FDT.Value.Date && p.ExpectedDelDate.Value.Date <= TDT.Value.Date) && (p.DriverID >= FDID && p.DriverID <= TDID) && (p.DeliveryTime >= Fdelivery && p.DeliveryTime <= Tdelivery))).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).OrderBy(p => p.DeliverySequence).ToList();
                //ListView2.DataBind();
            }
            else
            {
                Listview1.DataSource = List.Where(p => p.ExpectedDelDate.Value.Date == FDT.Value.Date && p.DriverID == FDID && p.DeliveryTime == Fdelivery).GroupBy(i => new { i.ExpectedDelDate, i.DriverID, i.CustomerID }).Select(p => p.FirstOrDefault()).ToArray().OrderBy(p => p.DeliverySequence).ToList();
                Listview1.DataBind();
                ListView2.DataSource = List.Where(p => p.ExpectedDelDate.Value.Date == FDT.Value.Date && p.DriverID == FDID && p.DeliveryTime == Fdelivery).GroupBy(i => new { i.ExpectedDelDate, i.DriverID, i.CustomerID }).Select(p => p.FirstOrDefault()).ToArray().OrderBy(p => p.DeliverySequence).ToList();
                ListView2.DataBind();

                //Listview1.DataSource = List.Where(p => p.ActualDelDate == null && ((p.ExpectedDelDate.Value.Date >= FDT.Value.Date && p.ExpectedDelDate.Value.Date <= TDT.Value.Date) && (p.DriverID >= FDID && p.DriverID <= TDID) && (p.DeliveryTime >= Fdelivery && p.DeliveryTime <= Tdelivery))).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).OrderBy(p => p.DeliverySequence).ToList();
                ////Listview1.DataSource = List.Where(p => p.ActualDelDate == null && ((p.DriverID >= FDID && p.DriverID <= TDID) || (p.DeliveryTime >= Fdelivery && p.DeliveryTime <= Tdelivery))).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).OrderBy(p => p.DeliverySequence).ToList();
                //Listview1.DataBind();
                //ListView2.DataSource = List.Where(p => p.ActualDelDate == null && ((p.ExpectedDelDate.Value.Date >= FDT.Value.Date && p.ExpectedDelDate.Value.Date <= TDT.Value.Date) && (p.DriverID >= FDID && p.DriverID <= TDID) && (p.DeliveryTime >= Fdelivery && p.DeliveryTime <= Tdelivery))).GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).OrderBy(p => p.DeliverySequence).ToList();
                //ListView2.DataBind();
            }


        }
        public string GetAdd1(int CID)
        {
            List<Database.TBLCONTACT_DEL_ADRES> ListItem = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
            string Address1 = "";
            string City = "";
            string Contry = "";
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
                        State = DB.tblStates.Where(p => p.COUNTRYID == countryID && p.StateID == stateID).Count() > 0 ? DB.tblStates.Single(p => p.COUNTRYID == countryID && p.StateID == stateID).MYNAME1 : "''";
                        City = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).Count() > 0 ? DB.tblCityStatesCounties.Single(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).CityEnglish : "''";
                        Contry = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == countryID).Count() > 0 ? DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == countryID).COUNAME1 : "''";
                        Address1 += obj.ADDR1 + " " + City + " " + State + " " + Contry;
                    }
                }
                return Address1;
            }
            else
            {
                return "''";
            }
        }
        public string GetMapAdd(int CID)//map
        {
            string add = "";
            List<Database.TBLCONTACT_DEL_ADRES> ListItem = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
            if (ListItem.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Defualt == true).Count() > 0)
            {
                foreach (Database.TBLCONTACT_DEL_ADRES itemon in ListItem)
                {
                    if (ListItem.Where(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true).Count() > 0)
                    {
                        Database.TBLCONTACT_DEL_ADRES obj = ListItem.Single(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true);
                        add = obj.Latitute + "," + obj.Longitute;
                    }
                }
                return add;
            }
            else
            {
                return "''";
            }
        }
        public void Sequence()
        {
            int J = 0;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                UpdatePanel UpdatePanel1 = (UpdatePanel)Listview1.Items[i].FindControl("UpdatePanel1");
                TextBox txtSerise = (TextBox)Listview1.Items[i].FindControl("txtSerise");
                Label lblCustomerID = (Label)Listview1.Items[i].FindControl("lblCustomerID");
                Label lbldeliveryID = (Label)Listview1.Items[i].FindControl("lbldeliveryID");
                Label lblmytransiid = (Label)Listview1.Items[i].FindControl("lblmytransiid");
                Label lblDriver = (Label)Listview1.Items[i].FindControl("lblDriver");
                Label lblExpdate = (Label)Listview1.Items[i].FindControl("lblExpdate");
                int CID = Convert.ToInt32(lblCustomerID.Text);
                int DelID = Convert.ToInt32(lbldeliveryID.Text);
                int DriverID = Convert.ToInt32(lblDriver.Text);
                int MyTrans = Convert.ToInt32(lblmytransiid.Text);
                DateTime EDT = Convert.ToDateTime(lblExpdate.Text);

                Database.planmealcustinvoice OBJ = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrans && p.DeliveryID == DelID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT);
                if (OBJ.DeliverySequence == null)
                {
                    J++;
                    txtSerise.Text = J.ToString();
                }
                else
                {
                    txtSerise.Text = OBJ.DeliverySequence.ToString();
                }
            }
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlWarningMsg.Visible = false;
            if (e.CommandName == "btnState")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int CID = Convert.ToInt32(ID[0]);
                int MyTrans = Convert.ToInt32(ID[1]);
                int DriverID = Convert.ToInt32(ID[2]);
                int DelID = Convert.ToInt32(ID[3]);
                DateTime EDT = Convert.ToDateTime(ID[4]);
                int Plan = Convert.ToInt32(ID[5]);
                string DName = DrpFromDriver.SelectedItem.ToString();
                string CNAME = GetCustomer(CID);

                //Database.planmealcustinvoice OBJ = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrans && p.DeliveryID == DelID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT);
                //OBJ.ActualDelDate = DateTime.Now;
                ////DB.SaveChanges();
                //int Deliveryid = DelID + 1;
                //if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MyTrans && p.DeliveryID == Deliveryid && p.CustomerID == CID && p.planid == Plan).Count() > 0)
                //{
                //    Database.planmealcustinvoice OBJ123 = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrans && p.DeliveryID == Deliveryid && p.CustomerID == CID && p.planid == Plan);
                //    DateTime NexDelDate = Convert.ToDateTime(OBJ123.ExpectedDelDate);

                //    if (OBJ.NExtDeliveryDate == null || OBJ.NExtDeliveryDate.Value.Date != NexDelDate.Date)
                //    {
                //        OBJ.NExtDeliveryDate = NexDelDate;
                //    }
                //}
                //DB.SaveChanges();
                List<Database.planmealcustinvoice> ListCount = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DriverID == DriverID && p.ExpectedDelDate == EDT && p.CustomerID == CID).ToList();
                List<Database.planmealcustinvoice> ListDeliverReturn = ListCount.ToList();//DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DriverID == DriverID && p.ExpectedDelDate == EDT && p.CustomerID == CID && p.ProductionDate != null).ToList();
                int DeliverCount = 0;
                foreach (Database.planmealcustinvoice item in ListDeliverReturn)
                {
                    if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT).Count() > 0)
                    {
                        Database.planmealcustinvoice OBJDRP = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT);
                        OBJDRP.ActualDelDate = DateTime.Now;
                        OBJDRP.Status = "Deliverd";
                        DB.SaveChanges();
                        DeliverCount++;
                    }
                }
                LList();
                Sequence();
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = DName + " have a '" + ListCount.Count() + "' Item For <u>" + CNAME + "</u> and Deliverd Item '" + DeliverCount + "'";

            }
            if (e.CommandName == "btnsGoReason")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int CID = Convert.ToInt32(ID[0]);
                int MyTrans = Convert.ToInt32(ID[1]);
                int DriverID = Convert.ToInt32(ID[2]);
                int DelID = Convert.ToInt32(ID[3]);
                DateTime EDT = Convert.ToDateTime(ID[4]);
                int Plan = Convert.ToInt32(ID[5]);
                DropDownList drpreson = (DropDownList)e.Item.FindControl("drpreson");
                TextBox txtresondate = (TextBox)e.Item.FindControl("txtresondate");
                int reasontype = Convert.ToInt32(drpreson.SelectedValue);
                DateTime RDT = Convert.ToDateTime(txtresondate.Text);
                string DName = DrpFromDriver.SelectedItem.ToString();
                string CNAME = GetCustomer(CID);

                //Database.planmealcustinvoice OBJ = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrans && p.DeliveryID == DelID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT);
                //OBJ.ReturnReason = reasontype;
                //OBJ.ReasonDate = RDT;
                //DB.SaveChanges();
                int DeliverCountr = 0;
                List<Database.planmealcustinvoice> ListReturncount = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DriverID == DriverID && p.ExpectedDelDate == EDT && p.CustomerID == CID).ToList();
                List<Database.planmealcustinvoice> ListDeliverReturn = ListReturncount.ToList();//DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DriverID == DriverID && p.ExpectedDelDate == EDT && p.CustomerID == CID).ToList();
                foreach (Database.planmealcustinvoice item in ListDeliverReturn)
                {
                    if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT).Count() > 0)
                    {
                        Database.planmealcustinvoice obj1 = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID && p.DeliveryID == item.DeliveryID && p.CustomerID == CID && p.DriverID == DriverID && p.ExpectedDelDate == EDT);
                        obj1.ReturnReason = reasontype;
                        obj1.ReasonDate = RDT;
                        obj1.Status = "Return";
                        DB.SaveChanges();
                        DeliverCountr++;
                    }
                }
                LList();
                Sequence();
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = DName + " have a '" + ListReturncount.Count() + "' Item For <u>" + CNAME + "</u> and Return Item '" + DeliverCountr + "'";
            }
        }
        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList drpreson = (DropDownList)e.Item.FindControl("drpreson");
            TextBox txtresondate = (TextBox)e.Item.FindControl("txtresondate");
            LinkButton btnState = (LinkButton)e.Item.FindControl("btnState");
            LinkButton lblReturnReason1 = (LinkButton)e.Item.FindControl("lblReturnReason1");
            Label lblmytransiid = (Label)e.Item.FindControl("lblmytransiid");
            Label lbldeliveryID = (Label)e.Item.FindControl("lbldeliveryID");
            Label lblCustomerID = (Label)e.Item.FindControl("lblCustomerID");
            Label lblDriver = (Label)e.Item.FindControl("lblDriver");
            Label Label19 = (Label)e.Item.FindControl("Label19");

            int mytranID = Convert.ToInt32(lblmytransiid.Text);
            int Deliverid = Convert.ToInt32(lbldeliveryID.Text);
            int custID = Convert.ToInt32(lblCustomerID.Text);
            int driver = Convert.ToInt32(lblDriver.Text);

            drpreson.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "ReturnReason");
            drpreson.DataTextField = "REFNAME1";
            drpreson.DataValueField = "REFID";
            drpreson.DataBind();
            drpreson.Items.Insert(0, new ListItem("-- Select --", "0"));

            txtresondate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            List<Database.planmealcustinvoice> tempDT = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == mytranID && p.DeliveryID == Deliverid && p.CustomerID == custID && p.DriverID == driver).ToList();
            Database.planmealcustinvoice obj = tempDT.Single(p => p.TenentID == TID && p.MYTRANSID == mytranID && p.DeliveryID == Deliverid && p.CustomerID == custID && p.DriverID == driver);
            if (obj.ReasonDate != null)
            {
                //lblReturnReason1.CssClass = "btn btn-sm yellow";
                Label19.Text = "Return";
                Label19.CssClass = "btn btn-sm purple dropdown-header";
                btnState.Visible = false;
                lblReturnReason1.Visible = false;
            }
            else if (obj.ActualDelDate != null)
            {
                //btnState.CssClass = "btn btn-sm green";
                Label19.Text = "Delivered";
                Label19.CssClass = "btn btn-sm green-jungle dropdown-header";
                btnState.Visible = false;
                lblReturnReason1.Visible = false;
            }


        }
        protected void btnCurrentlocation_Click(object sender, EventArgs e)//map
        {
        //    string sScript = "<script>\n";

        //    sScript += "if (navigator.geolocation) {\n";
        //    sScript += "navigator.geolocation.getCurrentPosition(function (p) {\n";
        //    sScript += " var n = p.coords.latitude.toString();\n";
        //    sScript += "var n1 = p.coords.longitude.toString();\n";
        //    sScript += "document.getElementById(\"txtStartingpoint\").value = p.coords.formatted_address;\n";
        //    sScript += "});\n";
        //    sScript += "} else {\n";
        //    sScript += "    alert('Geo Location feature is not supported in this browser.');}\n";
        //    sScript += "</script>\n";
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", sScript);

        //    GoogleMapForASPNet1.GoogleMapObject.Directions.ShowDirectionInstructions = chkShowDirections.Checked;
        //    GoogleMapForASPNet1.GoogleMapObject.Directions.HideMarkers = chkHideMarkers.Checked;
        //    GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineColor = txtDirColor.Text;
        //    GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineWeight = int.Parse(txtDirWidth.Text);
        //    GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineOpacity = double.Parse(txtDirOpacity.Text);
        //    //Provide addresses or postal codes in following lines
        //    GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Clear();
        //    GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtStartingpoint.Text);
        //    if (ListView2.Items.Count() > 0)
        //    {
        //        for (int i = 0; i <= ListView2.Items.Count - 1; i++)
        //        {

        //            TextBox txtFirstAddress = (TextBox)ListView2.Items[i].FindControl("txtFirstAddress");
        //            GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtFirstAddress.Text);
        //        }
        //    }


            string sScript = "<script type=\"text/javascript\">\n";
            sScript += "if (navigator.geolocation) {\n";
            sScript += "navigator.geolocation.getCurrentPosition(function (p) {\n";
            sScript += "var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);\n";
            sScript += "var mapOptions = {\n";
            sScript += "center: LatLng,\n";
            sScript += "zoom: 13,\n";
            sScript += "zoom: 13,\n";
            sScript += "mapTypeId: google.maps.MapTypeId.ROADMAP\n";
            sScript += "};\n";
            sScript += "var map = new google.maps.Map(document.getElementById(\"GoogleMapForASPNet1\"), mapOptions);\n";
            sScript += "var marker = new google.maps.Marker({\n";
            sScript += "position: LatLng,\n";
            sScript += " map: map,\n";
            sScript += "title: \"<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: \" + p.coords.latitude + \"<br />Longitude: \" + p.coords.longitude\n";
            sScript += "});\n";
            sScript += "google.maps.event.addListener(marker, \"click\", function (e) {\n";
            sScript += "var infoWindow = new google.maps.InfoWindow();\n";
            sScript += "infoWindow.setContent(marker.title);\n";
            sScript += "infoWindow.open(map, marker);\n";
            sScript += "});\n";
            sScript += "} else {\n";
            sScript += "alert('Geo Location feature is not supported in this browser.');\n";
            sScript += "}\n";
            sScript += "</script>\n";
            
//             <script type="text/javascript">
//if (navigator.geolocation) {
//    navigator.geolocation.getCurrentPosition(function (p) {
//        var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
//        var mapOptions = {
//            center: LatLng,
//            zoom: 13,
//            mapTypeId: google.maps.MapTypeId.ROADMAP
//        };
//        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
//        var marker = new google.maps.Marker({
//            position: LatLng,
//            map: map,
//            title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: " + p.coords.latitude + "<br />Longitude: " + p.coords.longitude
//        });
//        google.maps.event.addListener(marker, "click", function (e) {
//            var infoWindow = new google.maps.InfoWindow();
//            infoWindow.setContent(marker.title);
//            infoWindow.open(map, marker);
//        });
//    });
//} else {
//    alert('Geo Location feature is not supported in this browser.');
//}
//</script>


        }




    }
}
