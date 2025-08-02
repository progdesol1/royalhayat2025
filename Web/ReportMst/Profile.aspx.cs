using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Database;
using Classes;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web.ReportMst
{
    public partial class Profile : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);            
            TID = Convert.ToInt32(((TBLCOMPANYSETUP)Session["CustomerUser"]).TenentID);
            if (Request.QueryString["CID"] != null)
            {
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Defualt == true).Count() > 0)
                {
                    Database.TBLCONTACT_DEL_ADRES ObjLatLag = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.Defualt == true);
                    double Lat = Convert.ToDouble(ObjLatLag.Latitute);//double lat, double lng
                    double Lag = Convert.ToDouble(ObjLatLag.Longitute);
                    string ADD = ObjLatLag.ADDR2;
                    GetMap(Lat, Lag, "");
                }

            }

            if (!IsPostBack)
            {
                if (Request.QueryString["CID"] != null)
                {
                    int CID = Convert.ToInt32(Request.QueryString["CID"]);
                    //BindList();
                    SaveChangeData();
                    MCData(CID);
                    binddrop();
                    Total();

                    SoiclMediya(CID);
                    Database.TBLCOMPANYSETUP OBJ = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                    if (OBJ.Avtar == "0" || OBJ.Avtar == null)
                        IMGAvtar.ImageUrl = "../CRM/images/No_image.png";
                    else
                        IMGAvtar.ImageUrl = "~/ReportMst/assets/global/img/" + OBJ.Avtar;
                    lblCustomerName.Text = OBJ.COMPNAME1.ToString();
                    lblmyWebside.Text = OBJ.WEBPAGE == null ? "" : OBJ.WEBPAGE.ToString();
                    lblBirthDate.Text = OBJ.BirthDate == null ? "" : Convert.ToDateTime(OBJ.BirthDate).ToString("dd-MMM-yyyy");
                    string Location = GetLocation(CID);
                    lbllocation.Text = Location;
                    string plan = GetPlan(CID);
                    lblplan.Text = plan;

                    txtfirstname.Text = OBJ.COMPNAME1 == null ? "" : OBJ.COMPNAME1.ToString();
                    txtlastname.Text = OBJ.COMPNAME2 == null ? "" : OBJ.COMPNAME2.ToString();
                    txtmobile.Text = OBJ.MOBPHONE == null ? "" : OBJ.MOBPHONE.ToString();
                    txtemail.Text = OBJ.EMAIL1 == null ? "" : OBJ.EMAIL1.ToString();
                    txtweburl.Text = OBJ.WEBPAGE == null ? "" : OBJ.WEBPAGE.ToString();

                    personalrecord();
                }
            }
        }
        public void BindList(int PlanID)
        {
            if (Request.QueryString["CID"] != null)
            {
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                ListView1.DataSource = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PlanID).OrderByDescending(p => p.ExpectedDelDate);
                ListView1.DataBind();
            }

        }
        public void binddrop()
        {
            int CID = Convert.ToInt32(Request.QueryString["CID"]);
            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            List<Database.planmealcustinvoice> Listplanmealinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID).GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.planmealcustinvoice item in Listplanmealinvoice)
            {
                if (DB.planmealcustinvoices.Where(p => p.planid == item.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj1 = DB.tblProduct_Plan.Single(p => p.planid == item.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Add(obj1);
                }
            }
            ListtblProduct_Plan = ListtblProduct_Plan.OrderBy(p => p.planid).ToList();
            DrpPlan.DataSource = ListtblProduct_Plan;
            DrpPlan.DataTextField = "planname1";
            DrpPlan.DataValueField = "planid";
            DrpPlan.DataBind();
            DrpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));

            if (ListtblProduct_Plan.Count() > 0)
            {
                Database.tblProduct_Plan obj123 = ListtblProduct_Plan.Where(p => p.TenentID == TID).First();
                int planid = Convert.ToInt32(obj123.planid);
                DrpPlan.SelectedValue = planid.ToString();
                BindList(planid);
            }
            Classes.EcommAdminClass.getdropdown(drpSomib, TID, "social", "media", "", "REFTABLE");
        }
        public void SaveChangeData()//used SaveChangeData
        {
            drpItem.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID).OrderBy(p => p.ProdName1);
            drpItem.DataTextField = "ProdName1";
            drpItem.DataValueField = "MYPRODID";
            drpItem.DataBind();
            drpItem.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpitems.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID).OrderBy(p => p.ProdName1);
            drpitems.DataTextField = "ProdName1";
            drpitems.DataValueField = "MYPRODID";
            drpitems.DataBind();
            drpitems.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpAllergy.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID).OrderBy(p => p.ProdName1);
            drpAllergy.DataTextField = "ProdName1";
            drpAllergy.DataValueField = "MYPRODID";
            drpAllergy.DataBind();
            drpAllergy.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpcomplinid.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Cust" && p.REFSUBTYPE == "Complain").OrderBy(p => p.REFNAME1);
            drpcomplinid.DataTextField = "REFNAME1";
            drpcomplinid.DataValueField = "REFID";
            drpcomplinid.DataBind();
            drpcomplinid.Items.Insert(0, new ListItem("---Select---", "0"));

            drpNationality.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID).OrderBy(p => p.NATIONALITY1);
            drpNationality.DataTextField = "NATIONALITY1";
            drpNationality.DataValueField = "COUNTRYID";
            drpNationality.DataBind();
            drpNationality.Items.Insert(0, new ListItem("-- Select Nationality--", "0"));
        }
        public string GetMeal(int MealType)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == MealType && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == MealType && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string Deliverytime(int ID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }

        public string GetPlan(int CID)
        {
            List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID).GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
            string Allplan = "";
            if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID).Count() > 0)
            {
                foreach (Database.planmealcustinvoice item in List)
                {
                    Database.planmealcustinvoice obj = List.Single(p => p.TenentID == TID && p.CustomerID == CID && p.planid == item.planid);//DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.CustomerID == CID);
                    string Planname = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == obj.planid).planname1;
                    Allplan += Planname + ",";
                }
                return Allplan;
            }
            else
            {
                return "' '";
            }
        }
        public string GetLocation(int CID)
        {
            List<Database.TBLCONTACT_DEL_ADRES> ListItem = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
            string Address1 = "";
            string City = "";
            string State = "";
            string Contry = "";
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
                        Contry = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == countryID).Count() > 0 ? DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == countryID).COUNAME1 : "' '";
                        State = DB.tblStates.Where(p => p.COUNTRYID == countryID && p.StateID == stateID).Count() > 0 ? DB.tblStates.Single(p => p.COUNTRYID == countryID && p.StateID == stateID).MYNAME1 : "' '";
                        City = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).Count() > 0 ? DB.tblCityStatesCounties.Single(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).CityEnglish : "' '";
                        Address1 += Contry + "," + City + "," + State;

                    }
                }
                return Address1;
            }
            else
            {
                return "'Not Found'";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CID"] != null)
            {
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                Database.TBLCOMPANYSETUP OBJupdate = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                if (FileAvtar.HasFile)
                {
                    string path = FileAvtar.FileName;
                    FileAvtar.SaveAs(Server.MapPath("~/ReportMst/assets/global/img/" + path));
                    OBJupdate.Avtar = path;
                }
                DB.SaveChanges();
                if (OBJupdate.Avtar == "0" || OBJupdate.Avtar == null)
                    IMGAvtar.ImageUrl = "../CRM/images/No_image.png";
                else
                    IMGAvtar.ImageUrl = "~/ReportMst/assets/global/img/" + OBJupdate.Avtar;
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CID"] != null)
            {
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                Database.TBLCOMPANYSETUP OBJupdate = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                OBJupdate.CPASSWRD = txtNewPassword.Text;
                DB.SaveChanges();
                txtCurrentPassword.Text = "";
                txtNewPassword.Text = "";
                txtRnewPassword.Text = "";
            }


        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CID"] != null)
            {
                string[] phone = txtmobile.Text.ToString().Split(',');
                string mobile = phone[0];
                string[] Mail = txtemail.Text.ToString().Split(',');
                string AllMail = Mail[0];
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                Database.TBLCOMPANYSETUP OBJupdate = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                OBJupdate.COMPNAME1 = txtfirstname.Text;
                OBJupdate.COMPNAME2 = txtlastname.Text;
                OBJupdate.MOBPHONE = mobile;//txtmobile.Text;
                OBJupdate.EMAIL1 = AllMail;
                OBJupdate.WEBPAGE = txtweburl.Text;
                DB.SaveChanges();
                //Mobile Phone
                if (txtmobile.Text != "")
                {
                    List<Database.Tbl_RecordType_Mst> List2 = new List<Database.Tbl_RecordType_Mst>();
                    if (CID != null && CID != 0)
                        List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();

                    foreach (Database.Tbl_RecordType_Mst item in List2)
                    {
                        // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5003 && p.RecordType == "BusPhone");
                        DB.Tbl_RecordType_Mst.DeleteObject(item);
                        DB.SaveChanges();
                    }

                    string[] Seperate2 = txtmobile.Text.Split(',');
                    int count2 = 0;
                    string Sep2 = "";
                    for (int i = 0; i <= Seperate2.Count() - 1; i++)
                    {
                        Sep2 = Seperate2[i];
                        var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.CompniyAndContactID == CID && c.RecordType == "BusPhone" && c.TenentID == TID && c.Recource == 5005);
                        if (exist.Count() <= 0)
                        {
                            count2++;
                            Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                            obj.TenentID = TID;
                            obj.RecordType = "BusPhone";
                            obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                            obj.CompniyAndContactID = OBJupdate.COMPID;
                            obj.RunSerial = count2;
                            obj.Recource = 5005;
                            obj.RecourceName = "Company";
                            obj.RecValue = Seperate2[i];
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();

                        }
                        else
                        {
                            if (exist.Count() <= 0)
                            {
                                if (Sep2 != "00000")
                                {
                                    string display = "Bus Number Is Duplicate!";
                                    ClientScript.RegisterStartupScript(this.GetType(), "Bus Number Is Duplicate!", "alert('" + display + "');", true);
                                    return;
                                }
                            }
                        }
                    }
                }
                //Email
                if (txtemail.Text != "")
                {
                    List<Database.Tbl_RecordType_Mst> List5 = new List<Database.Tbl_RecordType_Mst>();
                    if (CID != null && CID != 0)
                        List5 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.RecordType == "Email" && p.Recource == 5005 && p.TenentID == TID).ToList();

                    foreach (Database.Tbl_RecordType_Mst item in List5)
                    {
                        // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5001 && p.RecordType == "Email");
                        DB.Tbl_RecordType_Mst.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    string[] Seperate = txtemail.Text.Split(',');
                    int count = 0;
                    string Sep = "";
                    for (int i = 0; i <= Seperate.Count() - 1; i++)
                    {
                        Sep = Seperate[i];
                        var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5005);
                        if (exist.Count() <= 0)
                        {
                            count++;
                            Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                            obj.TenentID = TID;
                            obj.RecordType = "Email";
                            obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                            obj.CompniyAndContactID = OBJupdate.COMPID;
                            obj.RunSerial = count;
                            obj.Recource = 5005;
                            obj.RecourceName = "Company";
                            obj.RecValue = Seperate[i];
                            obj.Active = true;
                            // obj.Rremark = "AutomatedProcess";
                            DB.Tbl_RecordType_Mst.AddObject(obj);
                            DB.SaveChanges();
                        }

                        else
                        {
                            if (DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5005).Count() < 1)
                            {
                                if (Sep != "NotUsed@gmail.com")
                                {
                                    string display = "Email Is Duplicate!";
                                    ClientScript.RegisterStartupScript(this.GetType(), "Email Is Duplicate!", "alert('" + display + "');", true);
                                    return;
                                }
                            }

                        }
                    }

                }
            }
        }

        protected void DrpPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Planid = Convert.ToInt32(DrpPlan.SelectedValue);
            Total();
            BindList(Planid);

        }
        public void Total()
        {
            if (Request.QueryString["CID"] != null)
            {
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                int Plan = Convert.ToInt32(DrpPlan.SelectedValue);
                List<Database.planmealcustinvoice> ListPlan = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.planid == Plan && p.CustomerID == CID).ToList();
                int TotalDelivery = ListPlan.Count();
                lblTotalDelivery.Text = TotalDelivery.ToString();
                ListPlan = ListPlan.Where(p => p.ActualDelDate != null).ToList();
                int Delevred = ListPlan.Count();
                lblDelivered.Text = Delevred.ToString();
            }
        }

        protected void btnsocial_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(Request.QueryString["CID"]);
            Database.TBLCOMPANYSETUP OBJupdate = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
            int COMPID123 = CID;

            int SID = Convert.ToInt32(drpSomib.SelectedValue);
            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == txtSocial.Text && c.Recource == SID && c.TenentID == TID);
            if (exist.Count() <= 0)
            {
                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                obj.TenentID = TID;
                obj.RecordType = "socialmedia";
                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                obj.CompniyAndContactID = COMPID123;
                obj.RecourceName = "Company";
                obj.Recource = Convert.ToInt32(drpSomib.SelectedValue);
                obj.RunSerial = 1;
                obj.RecValue = txtSocial.Text;
                obj.Active = true;
                // obj.Rremark = "AutomatedProcess";
                DB.Tbl_RecordType_Mst.AddObject(obj);
                DB.SaveChanges();
                SoiclMediya(COMPID123);
            }
            else
            {
                string display = "Social Media Is Duplicate!";
                //panelMsg.Visible = true;
                //lblErreorMsg.Text = display;

                ClientScript.RegisterStartupScript(this.GetType(), "Social Media Is Duplicate!", "alert('" + display + "');", true);
                return;
            }
        }
        public string getshocial(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID && p.TenentID == TID).REFNAME1;
        }
        public string getremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID && p.TenentID == TID).Remarks;
        }
        public void SoiclMediya(int CID)
        {
            listSocialMedia.DataSource = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Active == true && p.RecordType == "socialmedia");
            listSocialMedia.DataBind();
        }
        public void GetMap(double lat, double lng, string address)
        {
            string sScript = "<script>\n";
            sScript += "function initialize() {\n";
            if (lat != null && lng != null && lat != 0 && lng != 0)
            {
                sScript += "var n = '" + lat + "';\n";
                sScript += " var n1 = '" + lng + "';\n";

            }
            else
            {
                sScript += "if (navigator.geolocation) {\n";
                sScript += "navigator.geolocation.getCurrentPosition(function (p) {\n";
                sScript += " var n = p.coords.latitude.toString();\n";
                sScript += "var n1 = p.coords.longitude.toString();\n";

            }

            sScript += "var latlng = new google.maps.LatLng(n, n1);\n";
            sScript += "var map = new google.maps.Map(document.getElementById('map123'), {\n";
            sScript += "    center: latlng,\n";
            sScript += "    zoom: 15\n";
            sScript += "});\n";
            sScript += "var marker = new google.maps.Marker({\n";
            sScript += "    map: map,\n";
            sScript += "    position: latlng,\n";
            sScript += "    draggable: true,\n";
            sScript += "   anchorPoint: new google.maps.Point(0, -29)\n";
            sScript += "});\n";
            //sScript += "var input = document.getElementById('searchInput123');\n";
            //sScript += "map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);\n";
            //sScript += "var geocoder = new google.maps.Geocoder();\n";
            //sScript += "var autocomplete = new google.maps.places.Autocomplete(input);\n";
            //sScript += "autocomplete.bindTo('bounds', map);\n";
            //sScript += "var infowindow = new google.maps.InfoWindow();\n";
            //sScript += "autocomplete.addListener('place_changed', function () {\n";
            //sScript += "    infowindow.close();\n";
            //sScript += "    marker.setVisible(false);\n";
            //sScript += "    var place = autocomplete.getPlace();\n";
            //sScript += "    if (!place.geometry) {\n";
            //sScript += "        window.alert(\"Autocomplete's returned place contains no geometry\");\n";
            //sScript += "        return;\n";
            //sScript += "    }\n";
            //sScript += "    if (place.geometry.viewport) {\n";
            //sScript += "        map.fitBounds(place.geometry.viewport);\n";
            //sScript += "    } else {\n";
            //sScript += "        map.setCenter(place.geometry.location);\n";
            //sScript += "        map.setZoom(17);\n";
            //sScript += "    }\n";
            //sScript += "    marker.setPosition(place.geometry.location);\n";
            //sScript += "    marker.setVisible(true);\n";
            //sScript += "    bindDataToForm(place.formatted_address, place.geometry.location.lat(), place.geometry.location.lng());\n";
            //sScript += "    infowindow.setContent(place.formatted_address);\n";
            //sScript += "    infowindow.open(map, marker);\n";
            //sScript += "});\n";
            //sScript += "google.maps.event.addListener(marker, 'dragend', function () {\n";
            //sScript += "    geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {\n";
            //sScript += "        if (status == google.maps.GeocoderStatus.OK) {\n";
            //sScript += "            if (results[0]) {\n";
            //sScript += "                bindDataToForm(results[0].formatted_address, marker.getPosition().lat(), marker.getPosition().lng());\n";
            //sScript += "                infowindow.setContent(results[0].formatted_address);\n";
            //sScript += "                infowindow.open(map, marker);\n";
            //sScript += "            }\n";
            //sScript += "        }\n";
            //sScript += "    });\n";
            //sScript += "});\n";
            if (lat != null && lng != null && lat != 0 && lng != 0)
            {

            }
            else
            {
                sScript += "});\n";
                sScript += "} else {\n";
                sScript += "    alert('Geo Location feature is not supported in this browser.');\n";
                sScript += "}\n";
            }
            sScript += "}\n";
            //sScript += "function bindDataToForm(address, lat, lng) {\n";
            //sScript += "    document.getElementById('ContentPlaceHolder1_txtADDR1').value = address;\n";
            //sScript += "    document.getElementById('ContentPlaceHolder1_txtLatitute').value = lat;\n";
            //sScript += "    document.getElementById('ContentPlaceHolder1_txtLongitute').value = lng;\n";

            //sScript += "}\n";
            sScript += "google.maps.event.addDomListener(window, 'load', initialize);\n";
            sScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", sScript);
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (Request.QueryString["CID"] != null)
            {
                int CID = Convert.ToInt32(Request.QueryString["CID"]);
                Label lblMytranssID = (Label)e.Item.FindControl("lblMytranssID");
                Label lbldeliveryyID = (Label)e.Item.FindControl("lbldeliveryyID");
                Label lblexpDatee = (Label)e.Item.FindControl("lblexpDatee");
                Label lblstatuss = (Label)e.Item.FindControl("lblstatuss");

                var Expdate = Convert.ToDateTime(lblexpDatee.Text).Date;
                int deliveryyID = Convert.ToInt32(lbldeliveryyID.Text);
                int MytranssID = Convert.ToInt32(lblMytranssID.Text);

                Database.planmealcustinvoice OBJ = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.CustomerID == CID && p.MYTRANSID == MytranssID && p.DeliveryID == deliveryyID && p.ExpectedDelDate == Expdate);
                if (OBJ.ActualDelDate != null)
                {
                    lblstatuss.CssClass = "label label-success label-sm"; //CssClass="label label-warning label-sm"
                    lblstatuss.Text = "Delivered";
                }
                else if (OBJ.ReasonDate != null)
                {
                    lblstatuss.CssClass = "label label-warning label-sm";
                    lblstatuss.Text = "Return";
                }
                else
                {
                    lblstatuss.CssClass = "label label-danger label-sm";
                    lblstatuss.Text = "Pending";
                }
            }
        }
        //process of savechange Like,Dislike.coomplain,alergy
        public void Clear1()
        {
            //drpMealDeliver.SelectedIndex = 0;
            //drpDelivieryTime1.SelectedIndex = 0;
            drpAllergy.SelectedIndex = 0;
            drpitems.SelectedIndex = 0;
            drpItem.SelectedIndex = 0;
            drpcomplinid.SelectedIndex = 0;
            //drpPlan.SelectedIndex = 0;
            txtdate.Text = "";
            txtwight.Text = "";
            txtRemarks.Text = "";
            txtremark.Text = "";
            txtremak.Text = "";
            txtrem.Text = "";
            txtrema.Text = "";
            txtComplainDate.Text = "";
        }
        protected void btnlike_Click(object sender, EventArgs e)
        {

            if (btnlike.Text == "Update Like")
            {
                if (ViewState["EditLIKECID"] != null && ViewState["EditLIKEMYID"] != null)
                {
                    int CID = Convert.ToInt32(ViewState["EditLIKECID"]);
                    int MYID = Convert.ToInt32(ViewState["EditLIKEMYID"]);
                    addonDtlEdit('L', 2, CID, MYID);
                    btnlike.Text = "Add Like";
                }
            }
            else
            {
                if (btnlike.Text == "Add Like")
                {

                    addon1Dtl('L', 2);
                }
            }

            Clear1();
        }
        public void addon1Dtl(char ch, int param)
        {
            int CID = Convert.ToInt32(Request.QueryString["CID"]);
            int LID = 1;
            char CH = ch;
            int parameter = param;
            decimal wight = txtwight.Text != "" ? Convert.ToDecimal(txtwight.Text) : Convert.ToDecimal(0.00);
            int LikeItem = drpItem.SelectedValue != "" ? Convert.ToInt32(drpItem.SelectedValue) : 0;
            int DisLikeitem = drpitems.SelectedValue != "" ? Convert.ToInt32(drpitems.SelectedValue) : 0;
            int ComplainItem = drpcomplinid.SelectedValue != "" ? Convert.ToInt32(drpcomplinid.SelectedValue) : 0;
            int AllergyItem = drpAllergy.SelectedValue != "" ? Convert.ToInt32(drpAllergy.SelectedValue) : 0;
            string DelivieryTime1 = "0";
            int Plan = 0;
            int MealDeliver = 0;
            string LikeRemark1 = txtremark.Text != "" ? txtremark.Text : "";
            string DisLikeRemark1 = txtrem.Text != "" ? txtrem.Text : "";
            string ComplainRemark1 = txtrema.Text != "" ? txtrema.Text : "";
            string Alergie = txtremak.Text != "" ? txtremak.Text : "";
            DateTime DateAdded = txtdate.Text != "" ? Convert.ToDateTime(txtdate.Text) : DateTime.Now;
            DateTime ComplainDate = txtComplainDate.Text != "" ? Convert.ToDateTime(txtComplainDate.Text) : DateTime.Now;
            string WeightRemark = txtRemarks.Text != "" ? txtRemarks.Text : "";
            bool Active = true;
            Classes.EcommAdminClass.FoodSubcriberChangeInsert(TID, CID, LID, CH, parameter, wight, LikeItem, DisLikeitem, ComplainItem, AllergyItem, DelivieryTime1, Plan, MealDeliver, LikeRemark1, DisLikeRemark1, ComplainRemark1, Alergie, DateAdded, WeightRemark, ComplainDate);




            //Database.tblcontact_addon1_dtl objtblcontact_addon1_dtl = new tblcontact_addon1_dtl();
            //objtblcontact_addon1_dtl.TenentID = TID;
            //objtblcontact_addon1_dtl.LocationID = 1;
            //objtblcontact_addon1_dtl.customerID = CID;// Convert.ToInt32(drpCustomerId.SelectedValue);
            //objtblcontact_addon1_dtl.myID = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == 1 && p.customerID == CID).Count() > 0 ? DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == 1 && p.customerID == CID).Max(p => p.myID) + 1 : 1;
            //objtblcontact_addon1_dtl.LikeType = ch.ToString();
            //if (param == 1)
            //{
            //    //objtblcontact_addon1_dtl.Weight = Convert.ToDecimal(txtwight.Text);
            //}
            //else if (param == 2)
            //{
            //    objtblcontact_addon1_dtl.LikeId = Convert.ToInt32(drpItem.SelectedValue);
            //}
            //else if (param == 3)
            //{
            //    objtblcontact_addon1_dtl.LikeId = Convert.ToInt32(drpitems.SelectedValue);
            //}
            //else if (param == 4)
            //{
            //    objtblcontact_addon1_dtl.ComplinID = Convert.ToInt32(drpcomplinid.SelectedValue);
            //}
            //else if (param == 5)
            //{
            //    objtblcontact_addon1_dtl.Allergy = Convert.ToInt32(drpAllergy.SelectedValue);
            //}
            //else if (param == 6)
            //{
            //    //objtblcontact_addon1_dtl.DeliveryTime = drpDelivieryTime1.SelectedValue;
            //    //objtblcontact_addon1_dtl.planid = Convert.ToInt32(drpPlan.SelectedValue);
            //}
            //else
            //{

            //}
            //objtblcontact_addon1_dtl.DateAdded = DateTime.Now;
            //if (ch == 'L')
            //{
            //    objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpItem.SelectedValue);
            //}
            //if (ch == 'A')
            //{
            //    objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpAllergy.SelectedValue);
            //}
            //if (ch == 'D')
            //{
            //    objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpitems.SelectedValue);
            //}
            //if (ch == 'M')
            //{
            //    //objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpMealDeliver.SelectedValue);
            //}

            //if (param == 1)
            //{
            //    //objtblcontact_addon1_dtl.DateAdded = Convert.ToDateTime(txtdate.Text);
            //    //objtblcontact_addon1_dtl.Remarks = txtRemarks.Text;
            //}
            //else if (param == 2)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtremark.Text;
            //}
            //else if (param == 3)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtrem.Text;
            //}
            //else if (param == 4)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtrema.Text;
            //}
            //else if (param == 5)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtremak.Text;
            //}
            //else if (param == 6)
            //{
            //    //objtblcontact_addon1_dtl.Remarks = drpMealDeliver.SelectedValue;
            //}
            //else
            //{

            //}
            //objtblcontact_addon1_dtl.active = true;
            //DB.tblcontact_addon1_dtl.AddObject(objtblcontact_addon1_dtl);
            //DB.SaveChanges();

            //Clear1();
            //lblMsg.Text = "  Data Save Successfully";
            //BindData();
            MCData(CID);

            //btnallmeal.Visible = false;
        }
        public void addonDtlEdit(char ch, int param, int CID, int MYID)
        {
            char CH = ch;
            int Parameter = param;
            int myid = MYID;
            int LID = 1;
            decimal wight = txtwight.Text != "" ? Convert.ToDecimal(txtwight.Text) : Convert.ToDecimal(0.00);
            int LikeItem = drpItem.SelectedValue != "" ? Convert.ToInt32(drpItem.SelectedValue) : 0;
            int DisLikeitem = drpitems.SelectedValue != "" ? Convert.ToInt32(drpitems.SelectedValue) : 0;
            int ComplainItem = drpcomplinid.SelectedValue != "" ? Convert.ToInt32(drpcomplinid.SelectedValue) : 0;
            int AllergyItem = drpAllergy.SelectedValue != "" ? Convert.ToInt32(drpAllergy.SelectedValue) : 0;
            string DelivieryTime1 = "0";
            int Plan = 0;
            int MealDeliver = 0;
            string LikeRemark1 = txtremark.Text != "" ? txtremark.Text : "";
            string DisLikeRemark1 = txtrem.Text != "" ? txtrem.Text : "";
            string ComplainRemark1 = txtrema.Text != "" ? txtrema.Text : "";
            string Alergie = txtremak.Text != "" ? txtremak.Text : "";
            DateTime DateAdded = txtdate.Text != "" ? Convert.ToDateTime(txtdate.Text) : DateTime.Now;
            DateTime ComplainDate = txtComplainDate.Text != "" ? Convert.ToDateTime(txtComplainDate.Text) : DateTime.Now;
            string WeightRemark = txtRemarks.Text != "" ? txtRemarks.Text : "";
            bool Active = true;

            Classes.EcommAdminClass.FoodSubcriberChangeUpdate(TID, CID, LID, CH, Parameter, myid, wight, LikeItem, DisLikeitem, ComplainItem, AllergyItem, DelivieryTime1, Plan, MealDeliver, LikeRemark1, DisLikeRemark1, ComplainRemark1, Alergie, DateAdded, WeightRemark, ComplainDate);


            ////int CID = Convert.ToInt32(ViewState["Edit"]);
            //Database.tblcontact_addon1_dtl objtblcontact_addon1_dtl = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.LocationID == 1 && p.customerID == CID && p.myID == MYID);

            //if (param == 1)
            //{
            //    //objtblcontact_addon1_dtl.Weight = Convert.ToDecimal(txtwight.Text);
            //}
            //else if (param == 2)
            //{
            //    objtblcontact_addon1_dtl.LikeId = Convert.ToInt32(drpItem.SelectedValue);
            //}
            //else if (param == 3)
            //{
            //    objtblcontact_addon1_dtl.LikeId = Convert.ToInt32(drpitems.SelectedValue);
            //}
            //else if (param == 4)
            //{
            //    objtblcontact_addon1_dtl.ComplinID = Convert.ToInt32(drpcomplinid.SelectedValue);
            //}
            //else if (param == 5)
            //{
            //    objtblcontact_addon1_dtl.Allergy = Convert.ToInt32(drpAllergy.SelectedValue);
            //}
            //else if (param == 6)
            //{
            //    //objtblcontact_addon1_dtl.DeliveryTime = drpDelivieryTime1.SelectedValue;
            //    //objtblcontact_addon1_dtl.planid = Convert.ToInt32(drpPlan.SelectedValue);
            //}
            //else
            //{

            //}
            //objtblcontact_addon1_dtl.DateAdded = DateTime.Now;
            //if (ch == 'L')
            //{
            //    objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpItem.SelectedValue);
            //}
            //if (ch == 'A')
            //{
            //    objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpAllergy.SelectedValue);
            //}
            //if (ch == 'D')
            //{
            //    objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpitems.SelectedValue);
            //}
            //if (ch == 'M')
            //{
            //    //objtblcontact_addon1_dtl.ItemCode = Convert.ToInt32(drpMealDeliver.SelectedValue);
            //}

            //if (param == 1)
            //{
            //    //objtblcontact_addon1_dtl.DateAdded = Convert.ToDateTime(txtdate.Text);
            //    //objtblcontact_addon1_dtl.Remarks = txtRemarks.Text;
            //}
            //else if (param == 2)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtremark.Text;
            //}
            //else if (param == 3)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtrem.Text;
            //}
            //else if (param == 4)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtrema.Text;
            //}
            //else if (param == 5)
            //{
            //    objtblcontact_addon1_dtl.Remarks = txtremak.Text;
            //}
            //else if (param == 6)
            //{
            //    //objtblcontact_addon1_dtl.Remarks = drpMealDeliver.SelectedValue;
            //}
            //else
            //{

            //}
            //objtblcontact_addon1_dtl.active = true;
            //DB.SaveChanges();

            //Clear1();
            //lblMsg.Text = " Data Edit Successfully";
            //BindData();
            MCData(CID);


            //btnallmeal.Visible = false;
        }
        public void MCData(int CID)
        {
            List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M").ToList();

            //Panel1.Visible = true;
            //DeliveryMealtype.DataSource = ListDeliveryMealtype;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M");
            //DeliveryMealtype.DataBind();

            //bidmeal();

            CustomerComplainID.DataSource = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "C");
            CustomerComplainID.DataBind();

            CustomerDisLike.DataSource = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "D");
            CustomerDisLike.DataBind();

            CustomerLike.DataSource = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "L");
            CustomerLike.DataBind();

            WeightMeasured.DataSource = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "W");
            WeightMeasured.DataBind();

            Allergy.DataSource = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "A");
            Allergy.DataBind();
            //UpdatePanelDelivery.Update();
        }

        public string nameItem(int ID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == ID && p.TenentID == TID).ProdName1;
            }
            else
            {
                return "Recored not Found";
            }
        }

        protected void CustomerLike_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objSOJobDesc = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "L");
                objSOJobDesc.active = false;
                DB.SaveChanges();
                //BindData();
                MCData(CID);
            }

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objLIKE = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "L");

                DateTime DateAdded = Convert.ToDateTime(objLIKE.DateAdded);

                drpItem.SelectedValue = objLIKE.ItemCode.ToString();
                txtremark.Text = objLIKE.Remarks.ToString();

                ViewState["EditLIKECID"] = CID;
                ViewState["EditLIKEMYID"] = MYID;

                btnlike.Text = "Update Like";
            }
        }

        protected void btnDislike_Click(object sender, EventArgs e)//DisLike
        {
            if (btnDislike.Text == "Update DisLike")
            {
                if (ViewState["EditDislikeCID"] != null && ViewState["EditDislikeMYID"] != null)
                {
                    int CID = Convert.ToInt32(ViewState["EditDislikeCID"]);
                    int MYID = Convert.ToInt32(ViewState["EditDislikeMYID"]);
                    addonDtlEdit('D', 3, CID, MYID);
                    btnDislike.Text = "Add DisLike";
                }

            }
            else
            {
                if (btnDislike.Text == "Add DisLike")
                {
                    addon1Dtl('D', 3);
                }
            }
            //addon1Dtl('D', 3);
            Clear1();
        }

        protected void CustomerDisLike_ItemCommand(object sender, ListViewCommandEventArgs e)//DisLike
        {
            if (e.CommandName == "btnDelete")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objSOJobDesc = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "D");
                objSOJobDesc.active = false;
                DB.SaveChanges();
                MCData(CID);
            }

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objDislike = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "D");

                DateTime DateAdded = Convert.ToDateTime(objDislike.DateAdded);

                drpitems.SelectedValue = objDislike.ItemCode.ToString();
                txtrem.Text = objDislike.Remarks.ToString();

                ViewState["EditDislikeCID"] = CID;
                ViewState["EditDislikeMYID"] = MYID;

                btnDislike.Text = "Update DisLike";


            }
        }
        public string nameItems(int ID)//DisLike
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == ID && p.TenentID == TID).ProdName1;
            }
            else
            {
                return "Recored not Found";
            }
        }

        protected void btnAllergy_Click(object sender, EventArgs e)//Alergie
        {
            if (btnAllergy.Text == "Update Allergy")
            {
                if (ViewState["EditAllergyCID"] != null && ViewState["EditAllergyMYID"] != null)
                {
                    int CID = Convert.ToInt32(ViewState["EditAllergyCID"]);
                    int MYID = Convert.ToInt32(ViewState["EditAllergyMYID"]);
                    addonDtlEdit('A', 5, CID, MYID);
                    btnAllergy.Text = "Add Allergy";
                }
            }
            else
            {
                if (btnAllergy.Text == "Add Allergy")
                {
                    addon1Dtl('A', 5);
                }
            }
            Clear1();
        }
        public string nameAllergy(int ID)//Alergie
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == ID && p.TenentID == TID).ProdName1;
            }
            else
            {
                return "Recored not Found";
            }

        }

        protected void Allergy_ItemCommand(object sender, ListViewCommandEventArgs e)//Alergie
        {
            if (e.CommandName == "btnDelete")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objSOJobDesc = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "A");
                objSOJobDesc.active = false;
                DB.SaveChanges();
                MCData(CID);
            }
            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objAllergy = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "A");
                DateTime DateAdded = Convert.ToDateTime(objAllergy.DateAdded);
                drpAllergy.SelectedValue = objAllergy.ItemCode.ToString();
                txtremak.Text = objAllergy.Remarks.ToString();

                ViewState["EditAllergyCID"] = CID;
                ViewState["EditAllergyMYID"] = MYID;
                btnAllergy.Text = "Update Allergy";
            }
        }

        protected void btncomplain_Click(object sender, EventArgs e)//Complain
        {
            if (btncomplain.Text == "Update Complain")
            {
                if (ViewState["EditComplainCID"] != null && ViewState["EditComplainMYID"] != null)
                {
                    int CID = Convert.ToInt32(ViewState["EditComplainCID"]);
                    int MYID = Convert.ToInt32(ViewState["EditComplainMYID"]);
                    addonDtlEdit('C', 4, CID, MYID);
                    btnDislike.Text = "Add Complain";
                }
            }
            else
            {
                if (btncomplain.Text == "Add Complain")
                {
                    addon1Dtl('C', 4);
                }
            }
            Clear1();
        }
        public string nameComplian(int ID)//Complain
        {
            if (DB.REFTABLEs.Where(p => p.REFID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.REFID == ID && p.TenentID == TID).REFSUBTYPE;
            }
            else
            {
                return "Recored not Found";
            }
        }

        protected void CustomerComplainID_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objSOJobDesc = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "C");
                objSOJobDesc.active = false;
                DB.SaveChanges();
                MCData(CID);
            }

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objComplain = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "C");
                DateTime DateAdded = Convert.ToDateTime(objComplain.DateAdded);
                drpcomplinid.SelectedValue = objComplain.ComplinID.ToString();
                txtComplainDate.Text = DateAdded.ToShortDateString();
                txtrema.Text = objComplain.Remarks.ToString();
                ViewState["EditComplainCID"] = CID;
                ViewState["EditComplainMYID"] = MYID;
                btncomplain.Text = "Update Complain";
            }
        }
        public void personalrecord()
        {
            int CID = Convert.ToInt32(Request.QueryString["CID"]);
            if(DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == CID).Count() > 0)
            {
                Database.tblcontact_addon1 OBJsavechange = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.CustomerId == CID);
                txtHeight.Text = OBJsavechange.Height != null && OBJsavechange.Height != "" ? OBJsavechange.Height.ToString() : "0";
                txtAgreedCabs.Text = OBJsavechange.agreedcabs != null && OBJsavechange.agreedcabs != "" ? OBJsavechange.agreedcabs.ToString() : "0";
                drpNationality.SelectedValue = OBJsavechange.DeliveryCountry != null && OBJsavechange.DeliveryCountry != "" ? OBJsavechange.DeliveryCountry.ToString() : "0";
            }
        }

        protected void btnperrecord_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(Request.QueryString["CID"]);
            Database.tblcontact_addon1 OBJsavechange = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.CustomerId == CID);
            OBJsavechange.Height = txtHeight.Text;
            OBJsavechange.agreedcabs = txtAgreedCabs.Text;
            OBJsavechange.DeliveryCountry = drpNationality.SelectedValue;
            DB.SaveChanges();
            personalrecord();
        }

        protected void btnweight_Click(object sender, EventArgs e)//Weight
        {
            if (btnweight.Text == "Update Weight")
            {
                if (ViewState["EditWeightCID"] != null && ViewState["EditWeightMYID"] != null)
                {
                    int CID = Convert.ToInt32(ViewState["EditWeightCID"]);
                    int MYID = Convert.ToInt32(ViewState["EditWeightMYID"]);
                    addonDtlEdit('W', 1, CID, MYID);
                    btnweight.Text = "Add Weight";
                }

            }
            else
            {
                if (btnweight.Text == "Add Weight")
                {
                    addon1Dtl('W', 1);
                }
            }
            Clear1();
        }

        protected void WeightMeasured_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)//Weight
        {
            if (e.CommandName == "btnDelete")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objSOJobDesc = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "W");
                objSOJobDesc.active = false;
                DB.SaveChanges();
                MCData(CID);
            }

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objWeight = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "W");

                DateTime DateAdded = Convert.ToDateTime(objWeight.DateAdded);

                txtdate.Text = DateAdded.ToShortDateString();
                txtwight.Text = objWeight.Weight.ToString();
                txtRemarks.Text = objWeight.Remarks.ToString();

                ViewState["EditWeightCID"] = CID;
                ViewState["EditWeightMYID"] = MYID;

                btnweight.Text = "Update Weight";
            }
        }



    }
}