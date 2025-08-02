using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.ReportMst
{
    public partial class driverGPSMap : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            }
            else
            {

            }
            if(!IsPostBack)
            {
                ListSubcriber();
            }
            GoogleMapForASPNet1.GoogleMapObject.Width = "99%"; //map
            GoogleMapForASPNet1.GoogleMapObject.Height = "400px";//map
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;//map
        }
        public void ListSubcriber()
        {
             int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            List<Database.planmealcustinvoice> CList = DB.planmealcustinvoices.Where(p => p.ExpectedDelDate.Value.Day == ToDay && p.ExpectedDelDate.Value.Month == ToMonth && p.ExpectedDelDate.Value.Year == ToYear && p.DriverID == 37).ToList();
            ListView1.DataSource = CList;
            ListView1.DataBind();
            ListView2.DataSource = CList;
            ListView2.DataBind();
        }
        public string GetCustomer(int ID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ID).Count() > 0)
            {
                string CusName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID).COMPNAME1;
                return CusName;
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
        public string GetMapAdd(int CID)//map
        {
            string add = "";
            List<Database.TBLCONTACT_DEL_ADRES> ListItem = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Defualt == true).Count() > 0)
            {
                foreach (Database.TBLCONTACT_DEL_ADRES itemon in ListItem)
                {
                    if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true).Count() > 0)
                    {
                        Database.TBLCONTACT_DEL_ADRES obj = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true);
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
        public string GetDTime(int ID)
        {
            if(DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").Count() > 0)
            {
                string Dtime = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").REFNAME1;
                return Dtime;
            }
            else
            {
                return "Not Found";
            }
            
        }

        protected void btnDrawDirections_Click(object sender, EventArgs e)
        {
            Direction();
            ViewState["FlageMap"] = "MapFlage";
        }
        public void Direction()
        {


            GoogleMapForASPNet1.GoogleMapObject.Directions.ShowDirectionInstructions = chkShowDirections.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.HideMarkers = chkHideMarkers.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineColor = txtDirColor.Text;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineWeight = int.Parse(txtDirWidth.Text);
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineOpacity = double.Parse(txtDirOpacity.Text);
            //Provide addresses or postal codes in following lines
            GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Clear();
            if (ListView2.Items.Count() > 0)
            {
                for (int i = 0; i <= ListView2.Items.Count - 1; i++)
                {

                    TextBox txtFirstAddress = (TextBox)ListView2.Items[i].FindControl("txtFirstAddress");
                    GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtFirstAddress.Text);
                }
            }

        }

       







    }
}