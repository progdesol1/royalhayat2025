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
using Classes;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Web.Master
{
    public partial class tblcontact_addon1 : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion

        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        CallEntities DB = new CallEntities();

        bool FirstFlag, ClickFlag = true;
        bool newplan = false;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        string PageMode;

        protected void Page_Load(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";
            PanelListError.Visible = false;
            lbllistError.Text = "";

            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            if (ViewState["PageModeCID"] != null)
            {
                PageMode = ViewState["PageModeCID"].ToString();
            }
            SessionLoad();
            if (!IsPostBack)
            {
                string uidd = ((USER_MST)Session["USER"]).USER_ID.ToString();
                bool Isadmin = Classes.CRMClass.ISAdmin(TID, uidd);
                if (Isadmin == false)
                {
                    btnEditLable.Visible = false;
                }
                ManageLang();
                pnlSuccessMsg.Visible = false;
                BtnSaveandContinue.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //txtdate.Text = DateTime.Now.ToShortDateString();
                FirstData();
                Panel1.Visible = true;
                Readonly();
                readmode();
                btnAdd.ValidationGroup = "s";
                btnweight.Enabled = false;
                PageMode = "View";
                ViewState["PageModeCID"] = PageMode;
                if (Request.QueryString["CID"] != null)
                {
                    //Clear();
                    int CID = Convert.ToInt32(Request.QueryString["CID"]);

                    if (CID != 0)
                    {
                        string Mode = Request.QueryString["PageMode"] != null ? Request.QueryString["PageMode"].ToString() : "View";
                        if (Mode == "View")
                        {
                            ViewFood(CID);
                        }
                        if (Mode == "Edit")
                        {
                            EditFood(CID);
                        }
                        if (Mode == "ADD")
                        {

                        }
                    }
                }



            }

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
        #region Step2

        public void FistTimeLoad()
        {
            FirstFlag = false;
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
        public void BindData()
        {
            List<Database.tblcontact_addon1> ListAddon = DB.tblcontact_addon1.Where(p => p.TenentID == TID ).ToList();//&& p.active == true
            Listview1.DataSource = ListAddon.OrderByDescending(p => p.datetime);
            Listview1.DataBind();

            List<Database.tblcontact_addon1> ListAddon1 = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active==true).ToList();
            List<Database.TBLCOMPANYSETUP> ListComp=DB.TBLCOMPANYSETUPs.Where(p=>p.TenentID==TID).ToList();
            foreach (Database.tblcontact_addon1 item in ListAddon1)
            {
                if(ListComp.Where(p=>p.TenentID==TID && p.COMPID==item.CustomerId && p.Active!="Y").Count()>0)
                {
                    Database.TBLCOMPANYSETUP objcomp = ListComp.Single(p => p.TenentID == TID && p.COMPID == item.CustomerId);
                    objcomp.Active = "Y";
                    DB.SaveChanges();
                }
            }


            //List<tblcontact_addon1> List = DB.tblcontact_addon1.OrderBy(m => m.JobId).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion


        public void GetShow()
        {

            lblCustomerId1s.Attributes["class"] = lblAdvCollectionStatus1s.Attributes["class"] = lblPromotionOn1s.Attributes["class"] = "control-label col-md-4  getshow";//lbllatituet1s.Attributes["class"] = lbllongituet1s.Attributes["class"] =lblDeliveryTime1s.Attributes["class"] = lblDeliveryCountry1s.Attributes["class"] = lblArea1s.Attributes["class"] = 
            lblCustomerId2h.Attributes["class"] = lblAdvCollectionStatus2h.Attributes["class"] = lblPromotionOn2h.Attributes["class"] = "control-label col-md-4  gethide";//lbllatituet2h.Attributes["class"] = lbllongituet2h.Attributes["class"] = lblDeliveryTime2h.Attributes["class"] = lblDeliveryCountry2h.Attributes["class"] = lblArea2h.Attributes["class"] = 
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCustomerId1s.Attributes["class"] = lblAdvCollectionStatus1s.Attributes["class"] = lblPromotionOn1s.Attributes["class"] = "control-label col-md-4  gethide";// lbllatituet1s.Attributes["class"] = lbllongituet1s.Attributes["class"] =lblDeliveryTime1s.Attributes["class"] = lblDeliveryCountry1s.Attributes["class"] = lblArea1s.Attributes["class"] = 
            lblCustomerId2h.Attributes["class"] = lblAdvCollectionStatus2h.Attributes["class"] = lblPromotionOn2h.Attributes["class"] = "control-label col-md-4  getshow";// lbllatituet2h.Attributes["class"] = lbllongituet2h.Attributes["class"] =lblDeliveryTime2h.Attributes["class"] = lblDeliveryCountry2h.Attributes["class"] = lblArea2h.Attributes["class"] = 
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            GetHide();
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetShow();
        }

        public void Clear()
        {
            //drpLocationID.SelectedIndex = 0;
            //drpCustomerId.SelectedIndex = 0;
            drpAddress1.SelectedIndex = 0;
            //drpDeliveryTime.SelectedIndex = 0;
            //LstboxDeliveryTime.SelectedIndex = 0;
            drpAdvCollectionStatus.SelectedIndex = 3;
            drpPromotionOn.SelectedIndex = 0;
            drpAllergy.SelectedIndex = 0;
            drpitems.SelectedIndex = 0;
            drpItem.SelectedIndex = 0;
            drpcomplinid.SelectedIndex = 0;
            //tags_1.Text = "";
            txtdate.Text = "";
            txtwight.Text = "";
            txtRemarks.Text = "";
            txtremark.Text = "";
            txtremak.Text = "";
            txtrem.Text = "";
            txtrema.Text = "";
            txtHeight.Text = "";
            txtAgreedCabs.Text = "";
            drpCOUNTRYID.SelectedIndex = 0;
            //txtlatituet.Text = "";
            //txtlongituet.Text = "";
            clearGrid();
        }

        public void Clear1()
        {
            drpMealDeliver.SelectedIndex = 0;
            drpDelivieryTime1.SelectedIndex = 0;
            drpAllergy.SelectedIndex = 0;
            drpitems.SelectedIndex = 0;
            drpItem.SelectedIndex = 0;
            drpcomplinid.SelectedIndex = 0;
            drpPlan.SelectedIndex = 0;
            txtdate.Text = "";
            txtwight.Text = "";
            txtRemarks.Text = "";
            txtremark.Text = "";
            txtremak.Text = "";
            txtrem.Text = "";
            txtrema.Text = "";
            txtComplainDate.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            PanelError.Visible = false;
            lblerror.Text = "";


            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (btnAdd.Text == "Add New")
            {
                PageMode = "ADD";
                ViewState["PageModeCID"] = PageMode;
                List<Database.tblcontact_addon1> ListCoustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID).ToList();
                string CompanyType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").REFID.ToString() : "0";
                List<Database.TBLCOMPANYSETUP> ListCompnysetup = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y" && p.CompanyType == CompanyType).ToList();

                List<Database.TBLCOMPANYSETUP> FinalList = new List<TBLCOMPANYSETUP>();

                foreach (Database.TBLCOMPANYSETUP items in ListCompnysetup)
                {
                    if (ListCoustomer.Where(p => p.TenentID == TID && p.CustomerId == items.COMPID).Count() > 0)
                    {

                    }
                    else
                    {
                        Database.TBLCOMPANYSETUP objcomp = ListCompnysetup.Single(p => p.TenentID == TID && p.COMPID == items.COMPID);
                        FinalList.Add(objcomp);
                    }
                }

                drpCustomerId.DataSource = FinalList.OrderBy(p => p.COMPNAME1);
                drpCustomerId.DataTextField = "COMPNAME1";
                drpCustomerId.DataValueField = "COMPID";
                drpCustomerId.DataBind();
                drpCustomerId.Items.Insert(0, new ListItem("-- Select --", "0"));

                drpAddress1.Items.Clear();
                drpAddress1.Items.Insert(0, new ListItem("-- Select --", "0"));

                txtCustomerSearch.Enabled = true;
                LnkSearcustomer.Enabled = true;
                drpCustomerId.Enabled = true;
                drpAdvCollectionStatus.Enabled = true;
                drpPromotionOn.Enabled = true;
                txtAgreedCabs.Enabled = true;
                txtHeight.Enabled = true;
                drpCOUNTRYID.Enabled = true;
                //Write();
                Clear();
                Panel1.Visible = false;
                BtnSaveandContinue.Visible = true;
                BtnSaveandContinue.Text = "Save and Continue";
                txtCustomerSearch.Text = "";
                btnAdd.Text = "Save";
                btnAdd.ValidationGroup = "submit";
            }
            else if (btnAdd.Text == "Save")
            {
                int CID = Convert.ToInt32(drpCustomerId.SelectedValue);

                if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == CID && p.LocationID == LID).Count() < 1)
                {
                    if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                    {
                        DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                    }
                    Database.tblcontact_addon1 objtblcontact_addon1 = new Database.tblcontact_addon1();
                    //Server Content Send data Yogesh
                    //objtblcontact_addon1.MyID = DB.tblcontact_addon1.Count() > 0 ? Convert.ToInt32(DB.tblcontact_addon1.Max(p => p.MyID) + 1) : 1;
                    objtblcontact_addon1.TenentID = TID;
                    objtblcontact_addon1.LocationID = LID;
                    //objtblcontact_addon1.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                    objtblcontact_addon1.CustomerId = Convert.ToInt32(drpCustomerId.SelectedValue);
                    //objtblcontact_addon1.DeliveryCountry = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COUNTRYID.ToString() : null;
                    objtblcontact_addon1.State = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).STATE.ToString() : null;
                    objtblcontact_addon1.Area = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).CITY.ToString() : null;
                    //objtblcontact_addon1.DeliveryTime = (drpDeliveryTime.SelectedValue);
                    //objtblcontact_addon1.DeliveryTime = tags_1.Text;
                    if (drpCOUNTRYID.SelectedValue != "0")
                    {
                        objtblcontact_addon1.DeliveryCountry = drpCOUNTRYID.SelectedValue.ToString();
                    }
                    else
                    {
                        objtblcontact_addon1.DeliveryCountry = "126";
                    }
                    objtblcontact_addon1.AdvCollectionStatus = (drpAdvCollectionStatus.SelectedValue);
                    objtblcontact_addon1.PromotionOn = (drpPromotionOn.SelectedValue);
                    objtblcontact_addon1.agreedcabs = txtAgreedCabs.Text;
                    if (txtHeight.Text != "")
                        objtblcontact_addon1.Height = txtHeight.Text;
                    //objtblcontact_addon1.latituet = Convert.ToDecimal(txtlatituet.Text);
                    //objtblcontact_addon1.longituet = Convert.ToDecimal(txtlongituet.Text);
                    objtblcontact_addon1.active = true;
                    objtblcontact_addon1.datetime = DateTime.Now;

                    DB.tblcontact_addon1.AddObject(objtblcontact_addon1);
                    DB.SaveChanges();



                    btnAdd.Text = "Add New";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    gridbind(CID);
                    //navigation.Visible = true;
                    Readonly();
                    //FirstData();
                    btnAdd.ValidationGroup = "s";
                    Button1.Visible = true;
                    Button2.Visible = true;
                    PageMode = "View";
                    ViewState["PageModeCID"] = PageMode;
                }
                else
                {
                    Database.tblcontact_addon1 obj = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.CustomerId == CID && p.LocationID == LID);
                    obj.active = true;
                    DB.SaveChanges();
                    //PanelError.Visible = true;
                    //lblerror.Text = "Already Added this Coustomer";
                    //return;
                }


            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    //int MYID = Convert.ToInt32(ViewState["MYID"]);
                    Database.tblcontact_addon1 objtblcontact_addon1 = DB.tblcontact_addon1.Single(p => p.CustomerId == ID && p.TenentID == TID && p.active == true);
                    //objtblcontact_addon1.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                    objtblcontact_addon1.CustomerId = Convert.ToInt32(drpCustomerId.SelectedValue);
                    //objtblcontact_addon1.DeliveryCountry = drpDeliveryCountry.SelectedValue;
                    //objtblcontact_addon1.Area = drpArea.SelectedValue;
                    //objtblcontact_addon1.DeliveryTime = (drpDeliveryTime.SelectedValue);
                    //objtblcontact_addon1.DeliveryTime = tags_1.Text;

                    if (drpCOUNTRYID.SelectedValue != "0")
                    {
                        objtblcontact_addon1.DeliveryCountry = drpCOUNTRYID.SelectedValue.ToString();
                    }
                    else
                    {
                        objtblcontact_addon1.DeliveryCountry = "126";
                    }
                    objtblcontact_addon1.AdvCollectionStatus = (drpAdvCollectionStatus.SelectedValue);
                    objtblcontact_addon1.PromotionOn = (drpPromotionOn.SelectedValue);
                    //objtblcontact_addon1.State = (drpState.SelectedValue);
                    objtblcontact_addon1.agreedcabs = txtAgreedCabs.Text;
                    if(txtHeight.Text !="")
                        objtblcontact_addon1.Height = txtHeight.Text;
                    //objtblcontact_addon1.latituet = Convert.ToDecimal(txtlatituet.Text);
                    //objtblcontact_addon1.longituet = Convert.ToDecimal(txtlongituet.Text);

                    ViewState["Edit"] = null;
                    btnAdd.Text = "Add New";
                    DB.SaveChanges();
                    gridbind(ID);
                    Clear();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //navigation.Visible = true;
                    Readonly();
                    //FirstData();
                    btnAdd.ValidationGroup = "s";
                    PageMode = "View";
                    ViewState["PageModeCID"] = PageMode;
                }
            }
            BindData();

            //        scope.Complete(); //  To commit.

            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("tblcontact_addon1.aspx?MID=gT9lZ6RqnrgINVUGW5DoOw==");
            //Response.Redirect(Session["Previous"].ToString());
        }

        public string name(int ID)
        {

            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID).COMPNAME1;
            }
            else
            {
                return "Recored not Found";
            }
        }
        public string nameCOU(int ID)
        {

            if (DB.tblCOUNTRies.Where(p => p.COUNTRYID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.tblCOUNTRies.Single(p => p.COUNTRYID == ID && p.TenentID == TID).COUNAME1;
            }
            else
            {
                return "Recored not Found";
            }
        }
        public string nameCITY(int ID, int State)
        {

            if (DB.tblCityStatesCounties.Where(p => p.CityID == ID && p.StateID == State).Count() > 0)
            {
                return DB.tblCityStatesCounties.FirstOrDefault(p => p.CityID == ID && p.StateID == State).CityEnglish;
            }
            else
            {
                return "Recored not Found";
            }
        }
        public string getsuppername(int ID)
        {
            if (ID == 1)
            {
                return "Collected";
            }
            else if (ID == 2)
            {
                return "Released";
            }
            else if (ID == 3)
            {
                return "Not Apply";
            }
            else
            {
                return "Not Found";
            }
        }
        public string getsuppername1(int ID)
        {
            if (ID == 1)
            {
                return "On Promotion";
            }
            else if (ID == 2)
            {
                return "Normal";
            }
            else if (ID == 3)
            {
                return "Not Apply";
            }
            else
            {
                return "Not Found";
            }
        }
        public string getsuppername11(int ID)
        {
            if (ID == 1)
            {
                return "Morning";
            }
            else if (ID == 2)
            {
                return "Afternoon";
            }
            else if (ID == 3)
            {
                return "Evening";
            }
            else if (ID == 4)
            {
                return "Night";
            }
            else
            {
                return "Not Found";
            }
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
        public string nameAllergy(int ID)
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
        public string nameItems(int ID)
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
        public string nameComplian(int ID)
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
        public void FillContractorID()
        {
            //drpCustomerId.DataSource = DB.TBLCONTACTs.Where(p=>p.TenentID==TID).Take(500);
            //drpCustomerId.DataTextField = "PersName1";
            //drpCustomerId.DataValueField = "ContactMyID";
            //drpCustomerId.DataBind();
            //drpCustomerId.Items.Insert(0, new ListItem("-- Select --", "0"));

            string CompanyType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").REFID.ToString() : "0";

            drpCustomerId.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y" && p.CompanyType == CompanyType).OrderBy(p => p.COMPNAME1);
            drpCustomerId.DataTextField = "COMPNAME1";
            drpCustomerId.DataValueField = "COMPID";
            drpCustomerId.DataBind();
            drpCustomerId.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpMealDeliver, TID, "Food", "MealType", "", "REFTABLE");

            drpCOUNTRYID.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.Active=="Y").OrderBy(p => p.NATIONALITY1);
            drpCOUNTRYID.DataTextField = "NATIONALITY1";
            drpCOUNTRYID.DataValueField = "COUNTRYID";
            drpCOUNTRYID.DataBind();
            drpCOUNTRYID.Items.Insert(0, new ListItem("-- Select Nationality--", "0"));

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

            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            if (CID > 0)
            {
                drpAddress1.DataSource = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.ADDR1 != "").OrderBy(p => p.AdressName1);
                drpAddress1.DataTextField = "AdressName1";
                drpAddress1.DataValueField = "DeliveryAdressID";
                drpAddress1.DataBind();
            }
            drpAddress1.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpcomplinid.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Cust" && p.REFSUBTYPE == "Complain").OrderBy(p => p.REFNAME1);
            drpcomplinid.DataTextField = "REFNAME1";
            drpcomplinid.DataValueField = "REFID";
            drpcomplinid.DataBind();
            drpcomplinid.Items.Insert(0, new ListItem("---Select---", "0"));

            drpAllergy.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID).OrderBy(p => p.ProdName1);
            drpAllergy.DataTextField = "ProdName1";
            drpAllergy.DataValueField = "MYPRODID";
            drpAllergy.DataBind();
            drpAllergy.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpDelivieryTime1.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime" && p.ACTIVE == "Y").OrderBy(p => p.REFNAME1);
            drpDelivieryTime1.DataTextField = "REFNAME1";
            drpDelivieryTime1.DataValueField = "REFID";
            drpDelivieryTime1.DataBind();
            drpDelivieryTime1.Items.Insert(0, new ListItem("-- Select --", "0"));

            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
            foreach (Database.planmealsetup item in Listplanmealsetup.GroupBy(p => p.planid).Select(p => p.FirstOrDefault()))
            {
                if (DB.tblProduct_Plan.Where(p => p.planid == item.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == item.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Add(obj);
                    //foreach (Database.planmealsetup item2 in Listplanmealsetup.Where(a => a.planid == item.planid).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()))
                    //{
                    //    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "Mealtype" && p.REFID == item2.MealType).Count() > 0)
                    //    {
                    //        Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "Mealtype" && p.REFID == item2.MealType);
                    //        ListREFTABLE.Add(obj2);
                    //    }
                    //    foreach (Database.planmealsetup item3 in Listplanmealsetup.Where(a => a.planid == item.planid && a.MealType == item2.MealType).GroupBy(p => p.MYPRODID).Select(p => p.FirstOrDefault()))
                    //    {
                    //        if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == item3.MYPRODID).Count() > 0)
                    //        {
                    //            Database.TBLPRODUCT obj3 = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == item3.MYPRODID);
                    //            ListTBLPRODUCT.Add(obj3);
                    //        }
                    //    }
                    //}
                }
            }

            drpPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planname1);
            drpPlan.DataTextField = "planname1";
            drpPlan.DataValueField = "planid";
            drpPlan.DataBind();
            //if (ListtblProduct_Plan.Count() == 0) drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            FirstData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            NextData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            PrevData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            LastData();
        }
        public void FirstData()
        {
            PageMode = "View";
            ViewState["PageModeCID"] = PageMode;
            int index = Convert.ToInt32(ViewState["Index"]);
            if (Listview1.Items.Count > 0)
            {

                Listview1.SelectedIndex = 0;
                int COUNTRYID = Convert.ToInt32(Listview1.SelectedDataKey[1]);
                string COUNTRYIDs = COUNTRYID.ToString();
                drpCOUNTRYID.SelectedValue = COUNTRYIDs;
                int CID = Convert.ToInt32(Listview1.SelectedDataKey[0]);

                if(DB.TBLCOMPANYSETUPs.Where(p=>p.TenentID == TID && p.COMPID == CID).Count() > 0)
                {
                    txtCustomerSearch.Text = CID.ToString();
                    drpCustomerId.SelectedValue = CID.ToString();
                }
                
                drpAdvCollectionStatus.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                drpPromotionOn.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                if(Listview1.SelectedDataKey[7]!=null)
                    txtHeight.Text = Listview1.SelectedDataKey[7].ToString();
                txtAgreedCabs.Text = Listview1.SelectedDataKey[8] != null && Listview1.SelectedDataKey[8] != "" ? Listview1.SelectedDataKey[8].ToString() : "";

                gridbind(CID);
                bindaddress(CID);
                readwritedrig(false);
                drpPlan.Enabled = false;
                drpDelivieryTime1.Enabled = false;
                drpMealDeliver.Enabled = false;
                lnkaddplan.Enabled = false;
            }
            else
            {
                gridbind(0);
                Clear();
                Clear1();
            }
            btnDelivery.Enabled = false;
        }
        public void NextData()
        {
            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                int COUNTRYID = Convert.ToInt32(Listview1.SelectedDataKey[1]);
                string COUNTRYIDs = COUNTRYID.ToString();
                drpCOUNTRYID.SelectedValue = COUNTRYIDs;
                int CID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                {
                    txtCustomerSearch.Text = CID.ToString();
                    drpCustomerId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                }
                drpAdvCollectionStatus.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                drpPromotionOn.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                if (Listview1.SelectedDataKey[7] != null)
                    txtHeight.Text = Listview1.SelectedDataKey[7].ToString();
                txtAgreedCabs.Text = Listview1.SelectedDataKey[8] != null && Listview1.SelectedDataKey[8] != "" ? Listview1.SelectedDataKey[8].ToString() : "";
               
                
                gridbind(CID);
                bindaddress(CID);
                readwritedrig(false);
                drpPlan.Enabled = false;
                drpDelivieryTime1.Enabled = false;
                drpMealDeliver.Enabled = false;
                lnkaddplan.Enabled = false;
            }


        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                lblMsg.Text = "This is first record";
                pnlSuccessMsg.Visible = true;

            }
            else
            {
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                int COUNTRYID = Convert.ToInt32(Listview1.SelectedDataKey[1]);
                string COUNTRYIDs = COUNTRYID.ToString();
                drpCOUNTRYID.SelectedValue = COUNTRYIDs;
                int CID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                {
                    txtCustomerSearch.Text = CID.ToString();
                    drpCustomerId.SelectedValue = CID.ToString();
                }
                drpAdvCollectionStatus.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                drpPromotionOn.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                if (Listview1.SelectedDataKey[7] != null)
                    txtHeight.Text = Listview1.SelectedDataKey[7].ToString();
                txtAgreedCabs.Text = Listview1.SelectedDataKey[8] != null && Listview1.SelectedDataKey[8] != "" ? Listview1.SelectedDataKey[8].ToString() : "";
                
                
                gridbind(CID);
                bindaddress(CID);
                readwritedrig(false);
                drpPlan.Enabled = false;
                drpDelivieryTime1.Enabled = false;
                drpMealDeliver.Enabled = false;
                lnkaddplan.Enabled = false;
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            int COUNTRYID = Convert.ToInt32(Listview1.SelectedDataKey[1]);
            string COUNTRYIDs = COUNTRYID.ToString();
            drpCOUNTRYID.SelectedValue = COUNTRYIDs;
            int CID = Convert.ToInt32(Listview1.SelectedDataKey[0]);
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                txtCustomerSearch.Text = CID.ToString();
                drpCustomerId.SelectedValue = CID.ToString();
            }
            drpAdvCollectionStatus.SelectedValue = Listview1.SelectedDataKey[5].ToString();
            drpPromotionOn.SelectedValue = Listview1.SelectedDataKey[6].ToString();
            if (Listview1.SelectedDataKey[7] != null)
                txtHeight.Text = Listview1.SelectedDataKey[7].ToString();
            txtAgreedCabs.Text = Listview1.SelectedDataKey[8] != null && Listview1.SelectedDataKey[8] != "" ? Listview1.SelectedDataKey[8].ToString() : "";
            
            gridbind(CID);
            bindaddress(CID);
            readwritedrig(false);
            drpPlan.Enabled = false;
            drpDelivieryTime1.Enabled = false;
            drpMealDeliver.Enabled = false;
            lnkaddplan.Enabled = false;
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblCustomerId2h.Visible = lblAdvCollectionStatus2h.Visible = lblPromotionOn2h.Visible = false;//lbllatituet2h.Visible = lbllongituet2h.Visible =lblDeliveryTime2h.Visible = 
                    //2true
                    txtCustomerId2h.Visible = txtAdvCollectionStatus2h.Visible = txtPromotionOn2h.Visible = true;//txtlatituet2h.Visible = txtlongituet2h.Visible =txtDeliveryTime2h.Visible = 

                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblCustomerId2h.Visible = lblAdvCollectionStatus2h.Visible = lblPromotionOn2h.Visible = true;//lbllatituet2h.Visible = lbllongituet2h.Visible =lblDeliveryTime2h.Visible = 
                    //2false
                    txtCustomerId2h.Visible = txtAdvCollectionStatus2h.Visible = txtPromotionOn2h.Visible = false;//txtlatituet2h.Visible = txtlongituet2h.Visible =txtDeliveryTime2h.Visible = 

                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lblCustomerId1s.Visible = lblAdvCollectionStatus1s.Visible = lblPromotionOn1s.Visible = false;// lbllatituet1s.Visible = lbllongituet1s.Visible =lblDeliveryTime1s.Visible = 
                    //1true
                    txtCustomerId1s.Visible = txtAdvCollectionStatus1s.Visible = txtPromotionOn1s.Visible = true;//txtlatituet1s.Visible = txtlongituet1s.Visible = txtDeliveryTime1s.Visible = 
                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lblCustomerId1s.Visible = lblAdvCollectionStatus1s.Visible = lblPromotionOn1s.Visible = true;//lbllatituet1s.Visible = lbllongituet1s.Visible =lblDeliveryTime1s.Visible = 
                    //1false
                    txtCustomerId1s.Visible = txtAdvCollectionStatus1s.Visible = txtPromotionOn1s.Visible = false;//txtlatituet1s.Visible = txtlongituet1s.Visible =txtDeliveryTime1s.Visible = 
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblcontact_addon1").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblCustomerId1s.ID == item.LabelID)
                    txtCustomerId1s.Text = lblCustomerId1s.Text = item.LabelName;

                //else if (lblDeliveryTime1s.ID == item.LabelID)
                //    txtDeliveryTime1s.Text = lblDeliveryTime1s.Text = item.LabelName;
                else if (lblAdvCollectionStatus1s.ID == item.LabelID)
                    txtAdvCollectionStatus1s.Text = lblAdvCollectionStatus1s.Text = item.LabelName;
                else if (lblPromotionOn1s.ID == item.LabelID)
                    txtPromotionOn1s.Text = lblPromotionOn1s.Text = item.LabelName;
                //else if (lbllatituet1s.ID == item.LabelID)
                //    txtlatituet1s.Text = lbllatituet1s.Text  = item.LabelName;
                //else if (lbllongituet1s.ID == item.LabelID)
                //    txtlongituet1s.Text = lbllongituet1s.Text = item.LabelName;

                else if (lblCustomerId2h.ID == item.LabelID)
                    txtCustomerId2h.Text = lblCustomerId2h.Text = item.LabelName;

                //else if (lblDeliveryTime2h.ID == item.LabelID)
                //    txtDeliveryTime2h.Text = lblDeliveryTime2h.Text = item.LabelName;
                else if (lblAdvCollectionStatus2h.ID == item.LabelID)
                    txtAdvCollectionStatus2h.Text = lblAdvCollectionStatus2h.Text = item.LabelName;
                else if (lblPromotionOn2h.ID == item.LabelID)
                    txtPromotionOn2h.Text = lblPromotionOn2h.Text = item.LabelName;
                //else if (lbllatituet2h.ID == item.LabelID)
                //    txtlatituet2h.Text = lbllatituet2h.Text = item.LabelName;
                //else if (lbllongituet2h.ID == item.LabelID)
                //    txtlongituet2h.Text = lbllongituet2h.Text  = item.LabelName;
                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblcontact_addon1").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblcontact_addon1.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblcontact_addon1").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblCustomerId1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomerId1s.Text;

                //else if (lblDeliveryTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryTime1s.Text;
                else if (lblAdvCollectionStatus1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAdvCollectionStatus1s.Text;
                else if (lblPromotionOn1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPromotionOn1s.Text;
                //else if (lbllatituet1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlatituet1s.Text;
                //else if (lbllongituet1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlongituet1s.Text;

                else if (lblCustomerId2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomerId2h.Text;

                //else if (lblDeliveryTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryTime2h.Text;
                else if (lblAdvCollectionStatus2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAdvCollectionStatus2h.Text;
                else if (lblPromotionOn2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPromotionOn2h.Text;
                //else if (lbllatituet2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlatituet2h.Text;
                //else if (lbllongituet2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlongituet2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblcontact_addon1.xml"));

        }

        public void ManageLang()
        {
            //for Language

            if (Session["LANGUAGE"] != null)
            {
                RecieveLabel(Session["LANGUAGE"].ToString());
                if (Session["LANGUAGE"].ToString() == "ar-KW")
                    GetHide();
                else
                    GetShow();
            }

        }
        protected void LanguageFrance_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
        }
        protected void LanguageArabic_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }
        protected void LanguageEnglish_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }

        public void readwritedrig(bool flag)
        {
            for (int i = 0; i < DeliveryMealtype.Items.Count; i++)
            {
                LinkButton lnkbtnchangeTime = (LinkButton)DeliveryMealtype.Items[i].FindControl("lnkbtnchangeTime");
                LinkButton btnDeleteDelivery = (LinkButton)DeliveryMealtype.Items[i].FindControl("btnDeleteDelivery");
                btnDeleteDelivery.Visible = lnkbtnchangeTime.Visible = flag;//lnkbtnchangeTime.Visible =
            }

            for (int i = 0; i < CustomerComplainID.Items.Count; i++)
            {
                LinkButton btnEditComplain = (LinkButton)CustomerComplainID.Items[i].FindControl("btnEditComplain");
                LinkButton btnDeleteComplain = (LinkButton)CustomerComplainID.Items[i].FindControl("btnDeleteComplain");
                btnEditComplain.Visible = btnDeleteComplain.Visible = flag;
            }
            for (int i = 0; i < CustomerDisLike.Items.Count; i++)
            {
                LinkButton btnEditDisLike = (LinkButton)CustomerDisLike.Items[i].FindControl("btnEditDisLike");
                LinkButton btnDeleteDisLike = (LinkButton)CustomerDisLike.Items[i].FindControl("btnDeleteDisLike");
                btnDeleteDisLike.Visible = btnEditDisLike.Visible = flag;
            }

            for (int i = 0; i < CustomerLike.Items.Count; i++)
            {
                LinkButton btnEditLike = (LinkButton)CustomerLike.Items[i].FindControl("btnEditLike");
                LinkButton btnDeleteLike = (LinkButton)CustomerLike.Items[i].FindControl("btnDeleteLike");
                btnEditLike.Visible = btnDeleteLike.Visible = flag;
            }

            for (int i = 0; i < WeightMeasured.Items.Count; i++)
            {
                LinkButton btnDeleteWeight = (LinkButton)WeightMeasured.Items[i].FindControl("btnDeleteWeight");
                LinkButton btnEditWeight = (LinkButton)WeightMeasured.Items[i].FindControl("btnEditWeight");
                btnEditWeight.Visible = btnDeleteWeight.Visible = flag;
            }

            for (int i = 0; i < Allergy.Items.Count; i++)
            {
                LinkButton btnDeleteAlergie = (LinkButton)Allergy.Items[i].FindControl("btnDeleteAlergie");
                LinkButton btnEditAlergie = (LinkButton)Allergy.Items[i].FindControl("btnEditAlergie");
                btnEditAlergie.Visible = btnDeleteAlergie.Visible = flag;
            }
        }
        public void Write()
        {
            //navigation.Visible = false;
            //drpLocationID.Enabled = true;
            txtCustomerSearch.Enabled = true;
            LnkSearcustomer.Enabled = true;
            drpCustomerId.Enabled = true;
            drpAdvCollectionStatus.Enabled = true;
            drpPromotionOn.Enabled = true;
            txtHeight.Enabled = true;
            txtAgreedCabs.Enabled = true;
            drpCOUNTRYID.Enabled = true;
            readwritedrig(true);
        }
        public void Readonly()
        {

            txtCustomerSearch.Enabled = false;
            LnkSearcustomer.Enabled = false;
            drpCustomerId.Enabled = false;

            drpAdvCollectionStatus.Enabled = false;
            drpPromotionOn.Enabled = false;
            drpCOUNTRYID.Enabled = false;
            txtHeight.Enabled = false;
            txtAgreedCabs.Enabled = false;
            readwritedrig(false);
        }

        #region Listview

        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            Readonly();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            FirstData();
            //int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            //if(CID!=0)
            //{
            //    gridbind(CID);
            //}

        }

        public void readmode()
        {
            //txtdate.Enabled = false;
            //txtwight.Enabled = false;
            //txtRemarks.Enabled = false;
            //drpItem.Enabled = false;
            //txtremark.Enabled = false;
            //drpAllergy.Enabled = false;
            //txtremak.Enabled = false;
            //drpitems.Enabled = false;
            //txtrem.Enabled = false;
            //drpcomplinid.Enabled = false;
            //txtrema.Enabled = false;
            //Button2.Enabled = false;

        }

        public void writemode()
        {
            //txtdate.Enabled = true;
            //txtwight.Enabled = true;
            //txtRemarks.Enabled = true;
            //drpItem.Enabled = true;
            //txtremark.Enabled = true;
            //drpAllergy.Enabled = true;
            //txtremak.Enabled = true;
            //drpitems.Enabled = true;
            //txtrem.Enabled = true;
            //drpcomplinid.Enabled = true;
            //txtrema.Enabled = true;
            Button2.Enabled = true;

        }

        public void bindaddress(int CID)
        {
            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Active == "Y").Count() > 0)
            {
                drpAddress1.Enabled = true;
                Button1.Enabled = true;

                drpAddress1.DataSource = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).OrderBy(p => p.AdressName1);
                drpAddress1.DataTextField = "AdressName1";
                drpAddress1.DataValueField = "DeliveryAdressID";
                drpAddress1.DataBind();
                drpAddress1.Items.Insert(0, new ListItem("-- Select --", "0"));
                btneditAddress.Visible = true;
                List<Database.TBLCONTACT_DEL_ADRES> ListconAdr = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Active == "Y").ToList();
                if (ListconAdr.Where(p => p.Defualt == true).Count() > 0)
                {
                    var objTBLCONTACT_DEL_ADRES = ListconAdr.Single(p => p.Defualt == true);
                    drpAddress1.SelectedValue = objTBLCONTACT_DEL_ADRES.DeliveryAdressID != null ? objTBLCONTACT_DEL_ADRES.DeliveryAdressID.ToString() : "0";
                    double lat = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Latitute) : 0;
                    double lng = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Longitute) : 0;
                    string add1 = objTBLCONTACT_DEL_ADRES.ADDR1 != "" && objTBLCONTACT_DEL_ADRES.ADDR1 != null ? objTBLCONTACT_DEL_ADRES.ADDR1.ToString() : "";
                    if (lat != null && lng != null && lat != 0 && lng != 0)
                    {
                        GetMap(lat, lng, add1);
                    }
                    else
                    {
                        GetMap(0, 0, "");
                    }

                }
                else
                {
                    var objTBLCONTACT_DEL_ADRES = ListconAdr.FirstOrDefault();
                    drpAddress1.SelectedValue = objTBLCONTACT_DEL_ADRES.DeliveryAdressID != null ? objTBLCONTACT_DEL_ADRES.DeliveryAdressID.ToString() : "0";
                    double lat = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Latitute) : 0;
                    double lng = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Longitute) : 0;
                    string add1 = objTBLCONTACT_DEL_ADRES.ADDR1 != "" && objTBLCONTACT_DEL_ADRES.ADDR1 != null ? objTBLCONTACT_DEL_ADRES.ADDR1.ToString() : "";
                    if (lat != null && lng != null && lat != 0 && lng != 0)
                    {
                        GetMap(lat, lng, add1);
                    }
                    else
                    {
                        GetMap(0, 0, "");
                    }
                }

                int AID = Convert.ToInt32(drpAddress1.SelectedValue);
                if (ListconAdr.Where(p => p.DeliveryAdressID == AID && p.Defualt == true).Count() > 0)
                {
                    //Button1.Enabled = false;
                }
                else
                {
                    Button1.Enabled = true;
                }
                penalmap.Visible = true;
            }
            else
            {
                drpAddress1.Items.Clear();
                drpAddress1.Items.Insert(0, new ListItem("-- Select --", "0"));
                btneditAddress.Enabled = false;
                Button1.Enabled = false;
                penalmap.Visible = false;
            }
        }

        public void ViewFood(int CID)
        {
            PageMode = "View";
            ViewState["PageModeCID"] = PageMode;
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";
            if (CID > 0)
            {
                Database.tblcontact_addon1 objtblcontact_addon1 = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.CustomerId == CID && p.active == true);
                txtCustomerSearch.Text = CID.ToString();
                string Name = name(CID);
                lblADDLable.Text = "View Only " + Name;

                int COUNTRYID = Convert.ToInt32(objtblcontact_addon1.DeliveryCountry);
                string COUNTRYIDs = COUNTRYID.ToString();
                drpCOUNTRYID.SelectedValue = COUNTRYIDs;
                int state = Convert.ToInt32(objtblcontact_addon1.State);

                bindaddress(CID);

                drpCustomerId.SelectedValue = objtblcontact_addon1.CustomerId.ToString();

                drpAdvCollectionStatus.SelectedValue = objtblcontact_addon1.AdvCollectionStatus.ToString();
                drpPromotionOn.SelectedValue = objtblcontact_addon1.PromotionOn.ToString();
                if (objtblcontact_addon1.Height != null)
                    txtHeight.Text = objtblcontact_addon1.Height.ToString();
                txtAgreedCabs.Text = objtblcontact_addon1.agreedcabs != "" && objtblcontact_addon1.agreedcabs != null ? objtblcontact_addon1.agreedcabs.ToString() : "";

                btnAdd.Text = "Add New";
                ViewState["Edit"] = CID;

                //Write();
                readmode();
                drpCustomerId.Enabled = false;
                txtCustomerSearch.Enabled = false;
                LnkSearcustomer.Enabled = false;

                btnweight.Enabled = false;
                btnlike.Enabled = false;
                btnAllergy.Enabled = false;
                btnDislike.Enabled = false;
                btncomplain.Enabled = false;
                btnDelivery.Enabled = false;
                lnkaddplan.Enabled = false;

                btnweight.Text = "Add Weight";
                btnlike.Text = "Add Like";
                btnAllergy.Text = "Add Allergy";
                btnDislike.Text = "Add DisLike";
                btncomplain.Text = "Add Complain";
                btnDelivery.Text = "Add Delivery";

                Panel1.Visible = true;
                List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M").ToList();
                if (ListDeliveryMealtype.Count() > 0)
                {
                    Database.tblcontact_addon1_dtl objtblcon = ListDeliveryMealtype.FirstOrDefault();
                    drpPlan.SelectedValue = objtblcon.planid.ToString();
                    drpPlan.Enabled = false;

                    foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                    {
                        int Deliverytime = Convert.ToInt32(itms.DeliveryTime);
                        List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                        if (ListRef.Count() > 0)
                        {
                            //btnDelivery.Enabled = false;
                            drpDelivieryTime1.Enabled = false;
                        }
                        int planid = Convert.ToInt32(itms.planid);
                        string Deliverytimes = itms.DeliveryTime.ToString();
                    }
                }

                MCData(CID);
                readwritedrig(false);
            }
            else
            {
                pnlSuccessMsg.Visible = true;
                lblMsg.Text = "Select Customer";
            }

        }
        public void EditFood(int CID)
        {
            PageMode = "Edit";
            ViewState["PageModeCID"] = PageMode;
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";
            if (CID > 0)
            {
                btnallmeal.Visible = false;
                txtCustomerSearch.Text = CID.ToString();
                string Name = name(CID);
                lblADDLable.Text = "Edit " + Name;

                Database.tblcontact_addon1 objtblcontact_addon1 = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.CustomerId == CID && p.active == true);

                int COUNTRYID = Convert.ToInt32(objtblcontact_addon1.DeliveryCountry);
                string COUNTRYIDs = COUNTRYID.ToString();
                drpCOUNTRYID.SelectedValue = COUNTRYIDs;
                int state = Convert.ToInt32(objtblcontact_addon1.State);

                bindaddress(CID);

                drpCustomerId.SelectedValue = objtblcontact_addon1.CustomerId.ToString();

                drpAdvCollectionStatus.SelectedValue = objtblcontact_addon1.AdvCollectionStatus.ToString();
                drpPromotionOn.SelectedValue = objtblcontact_addon1.PromotionOn.ToString();
                if(objtblcontact_addon1.Height!=null)
                    txtHeight.Text = objtblcontact_addon1.Height.ToString();
                txtAgreedCabs.Text = objtblcontact_addon1.agreedcabs != "" && objtblcontact_addon1.agreedcabs != null ? objtblcontact_addon1.agreedcabs.ToString() : "";
                btnAdd.Text = "Update";
                ViewState["Edit"] = CID;

                Write();
                writemode();

                drpCustomerId.Enabled = false;
                txtCustomerSearch.Enabled = false;
                LnkSearcustomer.Enabled = false;

                btnweight.Enabled = true;
                btnlike.Enabled = true;
                btnAllergy.Enabled = true;
                btnDislike.Enabled = true;
                btncomplain.Enabled = true;
                btnDelivery.Enabled = true;
                lnkaddplan.Enabled = true;

                btnweight.Text = "Add Weight";
                btnlike.Text = "Add Like";
                btnAllergy.Text = "Add Allergy";
                btnDislike.Text = "Add DisLike";
                btncomplain.Text = "Add Complain";
                btnDelivery.Text = "Add Delivery";

                List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype1 = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M").ToList();


                if (ListDeliveryMealtype1.Count() < 0)
                {
                    drpPlan.Enabled = true;
                }

                int planid = Convert.ToInt32(drpPlan.SelectedValue);

                List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();

                List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.planid == planid && p.active == true && p.customerID == CID && p.LikeType == "M").ToList();
                if (ListDeliveryMealtype.Count() > 0)
                {
                    foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                    {
                        int Deliverytime = Convert.ToInt32(itms.DeliveryTime);
                        List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                        if (ListRef.Count() > 0)
                        {
                            Database.REFTABLE objreftime = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1");
                            drpDelivieryTime1.SelectedValue = objreftime.REFID.ToString();
                            drpDelivieryTime1.Enabled = false;
                            drpMealDeliver.Enabled = true;
                            btnallmeal.Visible = true;
                            //btnDelivery.Enabled = false;                        
                        }
                    }

                    foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                    {
                        int Deliverytime = Convert.ToInt32(itms.DeliveryTime);
                        List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                        if (ListRef.Count() < 1)
                        {
                            drpDelivieryTime1.Enabled = true;
                        }
                    }
                }
                List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
                if (DB.tblProduct_Plan.Where(p => p.planid == planid && p.TenentID == TID).Count() > 0)
                {
                    foreach (Database.planmealsetup item2 in Listplanmealsetup.Where(a => a.planid == planid).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()))
                    {
                        if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == item2.MealType).Count() > 0)
                        {
                            Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == item2.MealType);
                            ListREFTABLE.Add(obj2);
                        }
                    }
                }

                string Deliverytime1 = drpDelivieryTime1.SelectedValue.ToString();

                foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                {
                    if (ListREFTABLE.Where(p => p.TenentID == TID && p.REFID == itms.ItemCode).Count() > 0)
                    {
                        Database.REFTABLE objmealtype = ListREFTABLE.Single(p => p.TenentID == TID && p.REFID == itms.ItemCode);
                        ListREFTABLE.Remove(objmealtype);
                    }
                }

                drpMealDeliver.DataSource = ListREFTABLE.OrderBy(p => p.REFNAME1);
                drpMealDeliver.DataTextField = "REFNAME1";
                drpMealDeliver.DataValueField = "REFID";
                drpMealDeliver.DataBind();
                drpMealDeliver.Items.Insert(0, new ListItem("-- Select --", "0"));


                MCData(CID);
                readwritedrig(true);
            }
            else
            {
                pnlSuccessMsg.Visible = true;
                lblMsg.Text = "Select Customer";
            }
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            FillContractorID();

            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            PanelError.Visible = false;
            lblerror.Text = "";

            PanelListError.Visible = false;
            lbllistError.Text = "";
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {

            
            if (e.CommandName == "btnDelete")
            {
                int CID = Convert.ToInt32(e.CommandArgument);
                if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.CustomerID == CID).Count() < 1)
                {
                    Database.tblcontact_addon1 objSOJobDesc = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.LocationID == LID && p.CustomerId == CID);
                    objSOJobDesc.active = false;
                    DB.SaveChanges();
                    BindData();
                    FirstData();
                }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "This customer Not to be delete It Used in contract";
                    return;
                }
                

            }

            if (e.CommandName == "btnview")
            {
                BtnSaveandContinue.Visible = false;
                int CID = Convert.ToInt32(e.CommandArgument);

                if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.LocationID == LID && p.CustomerId == CID && p.active == true).Count() > 0)
                {
                    ViewFood(CID);
                }
                else
                {
                    PanelListError.Visible = true;
                    lbllistError.Text = "This customer Not to Active please Active First.....";
                    return;
                }
            }

            if (e.CommandName == "btnActive")
            {
                int CID = Convert.ToInt32(e.CommandArgument);
                if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.LocationID==LID && p.CustomerId == CID && p.active == false).Count() > 0)
                {
                    Database.tblcontact_addon1 objSOJobDesc = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.LocationID == LID && p.CustomerId == CID);
                    objSOJobDesc.active = true;
                    DB.SaveChanges();
                }                
            }



            if (e.CommandName == "btnEdit")
            {
                BtnSaveandContinue.Visible = true;
                BtnSaveandContinue.Text = "Update and Continue";
                int CID = Convert.ToInt32(e.CommandArgument);

                if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.LocationID == LID && p.CustomerId == CID && p.active == true).Count() > 0)
                {
                    EditFood(CID);
                }
                else
                {
                    PanelListError.Visible = true;
                    lbllistError.Text = "This customer Not to Active please Active First.....";
                    return;
                }
            }

            BindData();

            UpdatePanelDelivery.Update();
            //Button2.Enabled = true;
            //        scope.Complete(); //  To commit.
            //    }
            //    catch (Exception ex)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //        throw;
            //    }
            //}
        }


        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tblcontact_addon1.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active=).OrderBy(m => m.CustomerId).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (Tvalue == Showdata && Svalue == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //    }
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        //}

        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";
            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);

            if (CID != 0)
            {
                Response.Redirect("TBLCONTACT_DEL_ADRES.aspx?CID=" + CID + "&Addid=" + 0 + "&PageMode=" + PageMode);
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Please Select Customer...";
            }


        }
        public void addon1Dtl(char ch, int param)
        {

            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            char CH = ch;
            int parameter = param;
            decimal wight = txtwight.Text != "" ? Convert.ToDecimal(txtwight.Text) : 0;
            int LikeItem = drpItem.SelectedValue != "0" ? Convert.ToInt32(drpItem.SelectedValue) : 0;
            int DisLikeitem = drpitems.SelectedValue != "0" ? Convert.ToInt32(drpitems.SelectedValue) : 0;
            int ComplainItem = drpcomplinid.SelectedValue != "0" ? Convert.ToInt32(drpcomplinid.SelectedValue) : 0;
            int AllergyItem = drpAllergy.SelectedValue != "" ? Convert.ToInt32(drpAllergy.SelectedValue) : 0;
            string DelivieryTime1 = drpDelivieryTime1.SelectedValue != "0" ? drpDelivieryTime1.SelectedValue : "0";
            int Plan = drpPlan.SelectedValue != "0" ? Convert.ToInt32(drpPlan.SelectedValue) : 0;
            int MealDeliver = drpMealDeliver.SelectedValue != "0" ? Convert.ToInt32(drpMealDeliver.SelectedValue) : 0;
            string LikeRemark1 = txtremark.Text != "" ? txtremark.Text : "";
            string DisLikeRemark1 = txtrem.Text != "" ? txtrem.Text : "";
            string ComplainRemark1 = txtrema.Text != "" ? txtrema.Text : "";
            string Alergie = txtremak.Text != "" ? txtremak.Text : "";
            DateTime DateAdded = txtdate.Text != "" ? Convert.ToDateTime(txtdate.Text) : DateTime.Now;
            string WeightRemark = txtRemarks.Text != "" ? txtRemarks.Text : "";
            DateTime ComplainDate = txtComplainDate.Text != "" ? Convert.ToDateTime(txtComplainDate.Text) : DateTime.Now;
            bool Active = true;
            Classes.EcommAdminClass.FoodSubcriberChangeInsert(TID, CID, LID, CH, parameter, wight, LikeItem, DisLikeitem, ComplainItem, AllergyItem, DelivieryTime1, Plan, MealDeliver, LikeRemark1, DisLikeRemark1, ComplainRemark1, Alergie, DateAdded, WeightRemark, ComplainDate);

            //Clear1();
            lblMsg.Text = "  Data Save Successfully";
            BindData();
            MCData(CID);

            btnallmeal.Visible = false;
        }

        public void addonDtlEdit(char ch, int param, int CID, int MYID)
        {
            //int CID = Convert.ToInt32(ViewState["Edit"]);

            char CH = ch;
            int parameter = param;
            decimal wight = txtwight.Text != "" ? Convert.ToDecimal(txtwight.Text) : 0;
            int LikeItem = drpItem.SelectedValue != "0" ? Convert.ToInt32(drpItem.SelectedValue) : 0;
            int DisLikeitem = drpitems.SelectedValue != "0" ? Convert.ToInt32(drpitems.SelectedValue) : 0;
            int ComplainItem = drpcomplinid.SelectedValue != "0" ? Convert.ToInt32(drpcomplinid.SelectedValue) : 0;
            int AllergyItem = drpAllergy.SelectedValue != "" ? Convert.ToInt32(drpAllergy.SelectedValue) : 0;
            string DelivieryTime1 = drpDelivieryTime1.SelectedValue != "0" ? drpDelivieryTime1.SelectedValue : "0";
            int Plan = drpPlan.SelectedValue != "0" ? Convert.ToInt32(drpPlan.SelectedValue) : 0;
            int MealDeliver = drpMealDeliver.SelectedValue != "0" ? Convert.ToInt32(drpMealDeliver.SelectedValue) : 0;
            string LikeRemark1 = txtremark.Text != "" ? txtremark.Text : "";
            string DisLikeRemark1 = txtrem.Text != "" ? txtrem.Text : "";
            string ComplainRemark1 = txtrema.Text != "" ? txtrema.Text : "";
            string Alergie = txtremak.Text != "" ? txtremak.Text : "";
            DateTime DateAdded = txtdate.Text != "" ? Convert.ToDateTime(txtdate.Text) : DateTime.Now;
            string WeightRemark = txtRemarks.Text != "" ? txtRemarks.Text : "";
            DateTime ComplainDate = txtComplainDate.Text != "" ? Convert.ToDateTime(txtComplainDate.Text) : DateTime.Now;
            bool Active = true;
            Classes.EcommAdminClass.FoodSubcriberChangeUpdate(TID, CID, LID, CH, parameter, MYID, wight, LikeItem, DisLikeitem, ComplainItem, AllergyItem, DelivieryTime1, Plan, MealDeliver, LikeRemark1, DisLikeRemark1, ComplainRemark1, Alergie, DateAdded, WeightRemark, ComplainDate);

            if (ViewState["DeliveryTimeInvoice"] != null)
            {
                int Deltime = Convert.ToInt32(ViewState["DeliveryTimeInvoice"]);
                int DelivieryTimeinvo = Convert.ToInt32(drpDelivieryTime1.SelectedValue);
                DateTime Today = DateTime.Now;
                //Old Version 11122017 change by yogesh : why you are updating Delivery time to Delivery time : if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Plan && p.DeliveryTime == Deltime && p.DeliveryMeal == MealDeliver && p.ExpectedDelDate > Today).Count() > 0)
                
                    List<planmealcustinvoice> List=DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Plan && p.DeliveryMeal == MealDeliver && p.ExpectedDelDate >= Today).ToList();
                    int Count = List.Count;
                    if (List.Count() > 0)
                    {
                        string Str = "update planmealcustinvoice set DeliveryTime=" + DelivieryTime1 + " where TenentID=" + TID + "and CustomerID=" + CID + "and planid=" + Plan  + " and DeliveryMeal=" + MealDeliver + " and ExpectedDelDate>='" + Today + "'";
                        command2 = new SqlCommand(Str, con);
                        con.Open();
                        command2.ExecuteReader();
                        con.Close();
                        lblMsg.Text = " Delivery Time updated for " + Count + " Records...";
                    }
                    else
                    {
                        PanelError.Visible = true;
                        lblerror.Text = "There is no Deliver left for this Contract; Please verify the same ??";
                    }
               
            }

            ViewState["DeliveryTimeInvoice"] = null;

            lblMsg.Text = " Data Edit Successfully";
            BindData();
            MCData(CID);

            btnallmeal.Visible = false;
        }
        public void MCData(int CID)
        {
            List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M").ToList();
            //DO Not Change This 
            Panel1.Visible = true;
            DeliveryMealtype.DataSource = ListDeliveryMealtype;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M");
            DeliveryMealtype.DataBind();

            bidmeal();
            //DO Not Change This 
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
            UpdatePanelDelivery.Update();
        }

        public string getMeal(int Meal)
        {

            List<Database.REFTABLE> List = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Meal).ToList();
            if (List.Count() > 0)
            {
                return List.Single(p => p.REFID == Meal).REFNAME1;
            }
            else
            {
                return "Record Not Found";
            }
        }

        public string getDeliveryTime(int DeliveryTime)
        {

            List<Database.REFTABLE> List = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryTime).ToList();
            if (List.Count() > 0)
            {
                return List.Single(p => p.REFID == DeliveryTime).REFNAME1;
            }
            else
            {
                return "Record Not Found";
            }
        }

        protected void btnDelivery_Click(object sender, EventArgs e)
        {
            if (btnDelivery.Text == "Update Delivery")
            {
                if (ViewState["EditDeliveryCID"] != null && ViewState["EditDeliveryMYID"] != null)
                {
                    int CID = Convert.ToInt32(ViewState["EditDeliveryCID"]);
                    int MYID = Convert.ToInt32(ViewState["EditDeliveryMYID"]);
                    string dtime = drpDelivieryTime1.SelectedValue.ToString();
                    int Deliverytime = Convert.ToInt32(drpDelivieryTime1.SelectedValue);

                    List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                    if (ListRef.Count() > 0)
                    {
                        //List<Database.tblcontact_addon1_dtl> ListDtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.DeliveryTime != dtime).ToList();

                        //foreach (Database.tblcontact_addon1_dtl items in ListDtl)
                        //{
                        //    Database.tblcontact_addon1_dtl objdtl = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.myID == items.myID);
                        //    objdtl.active = false;
                        //    DB.SaveChanges();
                        //}

                        addonDtlEdit('M', 6, CID, MYID);
                    }
                    else
                    {
                        addonDtlEdit('M', 6, CID, MYID);
                    }

                    //addonDtlEdit('M', 6, CID, MYID);

                    btnDelivery.Text = "Add Delivery";
                    btnDelivery.OnClientClick = null;
                }

            }
            else
            {
                if (btnDelivery.Text == "Add Delivery")
                {
                    int Deliverytime = Convert.ToInt32(drpDelivieryTime1.SelectedValue);
                    string dtime = drpDelivieryTime1.SelectedValue.ToString();
                    int mealid = Convert.ToInt32(drpMealDeliver.SelectedValue);
                    int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
                    int Planid = Convert.ToInt32(drpPlan.SelectedValue);

                    if (DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.DeliveryTime == dtime && p.ItemCode == mealid && p.LikeType == "M" && p.planid == Planid).Count() < 1)
                    {
                        List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                        if (ListRef.Count() > 0)
                        {
                            //List<Database.tblcontact_addon1_dtl> ListDtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.DeliveryTime != dtime).ToList();

                            //foreach (Database.tblcontact_addon1_dtl items in ListDtl)
                            //{
                            //    Database.tblcontact_addon1_dtl objdtl = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.myID == items.myID);
                            //    objdtl.active = false;
                            //    DB.SaveChanges();
                            //}

                            addon1Dtl('M', 6);
                        }
                        else
                        {
                            addon1Dtl('M', 6);
                        }


                    }
                    else
                    {
                        Database.tblcontact_addon1_dtl objdtl = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.DeliveryTime == dtime && p.ItemCode == mealid && p.planid == Planid && p.LikeType == "M");
                        objdtl.active = true;
                        DB.SaveChanges();
                    }
                    btnDelivery.OnClientClick = null;
                    MCData(CID);
                    UpdatePanelDelivery.Update();
                }
            }

            ViewState["ChangeMode"] = null;
            drpMealDeliver.SelectedIndex = 0;
            drpAllergy.SelectedIndex = 0;
            drpitems.SelectedIndex = 0;
            drpItem.SelectedIndex = 0;
            drpcomplinid.SelectedIndex = 0;
            txtdate.Text = "";
            txtwight.Text = "";
            txtRemarks.Text = "";
            txtremark.Text = "";
            txtremak.Text = "";
            txtrem.Text = "";
            txtrema.Text = "";
            txtComplainDate.Text = "";
            //Clear1();
            drpPlan.Enabled = true;
            drpDelivieryTime1.Enabled = false;
            drpMealDeliver.Enabled = true;
            UpdatePanelDelivery.Update();
        }
        protected void btnweight_Click(object sender, EventArgs e)
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

        protected void btnDislike_Click(object sender, EventArgs e)
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

        protected void btnAllergy_Click(object sender, EventArgs e)
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

            //addon1Dtl('A', 5);
            Clear1();
        }

        protected void btncomplain_Click(object sender, EventArgs e)
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

            //addon1Dtl('C', 4);
            Clear1();
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";

            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            int AID = Convert.ToInt32(drpAddress1.SelectedValue);

            if (CID != 0 && CID != null)
            {
                if (AID != 0 && AID != null)
                {
                    List<Database.TBLCONTACT_DEL_ADRES> listadd = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
                    if (listadd.Where(p => p.DeliveryAdressID == AID).Count() > 0)
                    {
                        foreach (Database.TBLCONTACT_DEL_ADRES items in listadd)
                        {
                            Database.TBLCONTACT_DEL_ADRES objcontact = listadd.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == items.DeliveryAdressID);
                            objcontact.Defualt = false;
                            DB.SaveChanges();
                        }

                        Database.TBLCONTACT_DEL_ADRES objcontact1 = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AID);
                        objcontact1.Defualt = true;
                        DB.SaveChanges();
                    }
                    drpAddress1.SelectedValue = AID.ToString();
                    //Button1.Enabled = false;
                }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "select Address";
                    return;
                }
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "select Customer";
                return;
            }

        }

        public void gridbind(int CID)
        {
            if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.LocationID == LID && p.CustomerId == CID && p.active == true).Count() > 0)
            {
                MCData(CID);
            }
            else
            {
                clearGrid();
            }
        }

        public void clearGrid()
        {
            DeliveryMealtype.Items.Clear();
            DeliveryMealtype.DataSource = null;// DB.PlanDeliveryMeals.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.CustomerID == CID && p.ACTIVE == true);
            DeliveryMealtype.DataBind();
            Panel1.Visible = false;
            CustomerComplainID.Items.Clear();
            CustomerComplainID.DataSource = null;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "C");
            CustomerComplainID.DataBind();

            CustomerDisLike.Items.Clear();
            CustomerDisLike.DataSource = null;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "D");
            CustomerDisLike.DataBind();

            CustomerLike.Items.Clear();
            CustomerLike.DataSource = null;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "L");
            CustomerLike.DataBind();

            WeightMeasured.Items.Clear();
            WeightMeasured.DataSource = null;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "W");
            WeightMeasured.DataBind();

            Allergy.Items.Clear();
            Allergy.DataSource = null;// DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "A");
            Allergy.DataBind();
        }

        protected void WeightMeasured_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";


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
                BindData();
                gridbind(CID);

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

        protected void CustomerLike_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";


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
                BindData();
                gridbind(CID);
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

        protected void CustomerDisLike_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";


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
                BindData();
                gridbind(CID);
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

        protected void Allergy_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";


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
                BindData();
                gridbind(CID);
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

        protected void CustomerComplainID_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";


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
                BindData();
                gridbind(CID);
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

        protected void drpCustomerId_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";

            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            txtCustomerSearch.Text = CID.ToString();
            if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == CID).Count() > 1)
            {
                PanelError.Visible = true;
                lblerror.Text = "Already Added this Coustomer";
            }
        }

        protected void drpAddress1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            int AID = Convert.ToInt32(drpAddress1.SelectedValue);

            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AID && p.Defualt == true).Count() > 0)
            {
                //Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }
            if (CID != 0)
            {
                if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AID).Count() > 1)
                {
                    var objTBLCONTACT_DEL_ADRES = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AID);
                    double lat = Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Latitute);
                    double lng = Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Longitute);
                    string add1 = objTBLCONTACT_DEL_ADRES.ADDR1.ToString();
                    if (lat != null && lng != null && lat != 0 && lng != 0)
                    {
                        GetMap(lat, lng, add1);
                    }
                    else
                    {
                        GetMap(0, 0, "");
                    }
                }
            }

        }

        protected void DeliveryMealtype_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";

            

            if (e.CommandName == "btnDelete")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                Database.tblcontact_addon1_dtl objSOJobDesc = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "M");

                int Plan = Convert.ToInt32(objSOJobDesc.planid);
                int Time = Convert.ToInt32(objSOJobDesc.DeliveryTime);
                int meal = Convert.ToInt32(objSOJobDesc.ItemCode);

                if (DB.planmealcustinvoices.Where(p => p.CustomerID==CID && p.planid == Plan && p.DeliveryMeal == meal && p.DeliveryTime == Time).Count() > 0)
                {
                    PanelError.Visible = true;
                    lblerror.Text = "This Plan,Time and Meal is used in Contract to not allow to Delete";
                    return;
                }
                else
                {
                    objSOJobDesc.active = false;
                    DB.SaveChanges();
                    BindData();
                    gridbind(CID);
                }
            }

            if (e.CommandName == "btnChange")
            {
                FillContractorID();
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                drpCustomerId.SelectedValue = CID.ToString();
                Database.tblcontact_addon1_dtl objDelivery = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "M");

                DateTime DateAdded = Convert.ToDateTime(objDelivery.DateAdded);

                drpDelivieryTime1.SelectedValue = objDelivery.DeliveryTime;
                ViewState["DeliveryTimeInvoice"] = Convert.ToInt32(objDelivery.DeliveryTime);
                drpMealDeliver.SelectedValue = objDelivery.ItemCode.ToString();
                if (objDelivery.planid != null && objDelivery.planid != 0)
                {
                    drpPlan.SelectedValue = objDelivery.planid.ToString();
                }
                else
                {
                    drpPlan.Enabled = true;
                }

                int Deliverytime = Convert.ToInt32(objDelivery.DeliveryTime);
                string dtime = objDelivery.DeliveryTime.ToString();
                int mealid = Convert.ToInt32(objDelivery.ItemCode);
                if (DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.DeliveryTime == dtime && p.ItemCode == mealid).Count() < 1)
                {
                    List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                    if (ListRef.Count() > 0)
                    {
                        //string MSG1 = "You Select " + drpDelivieryTime1.SelectedItem + ", Your previous Record Are must be delete, Are You sure ?";
                        //btnDelivery.OnClientClick = "return ConfirmMsgdisp('" + MSG1 + "');";
                    }
                    else
                    {
                        btnDelivery.OnClientClick = null;
                    }
                }

                drpPlan.Enabled = false;
                drpDelivieryTime1.Enabled = true;
                drpMealDeliver.Enabled = false;
                ViewState["EditDeliveryCID"] = CID;
                ViewState["EditDeliveryMYID"] = MYID;

                ViewState["ChangeMode"] = true;

                btnDelivery.Text = "Update Delivery";

            }

            if (e.CommandName == "btnEdit")
            {
                FillContractorID();
                string[] ID = e.CommandArgument.ToString().Split(',');
                string str1 = ID[0].ToString();
                string str2 = ID[1].ToString();
                int CID = Convert.ToInt32(str1);
                int MYID = Convert.ToInt32(str2);
                drpCustomerId.SelectedValue = CID.ToString();
                Database.tblcontact_addon1_dtl objDelivery = DB.tblcontact_addon1_dtl.Single(p => p.TenentID == TID && p.customerID == CID && p.active == true && p.myID == MYID && p.LikeType == "M");

                DateTime DateAdded = Convert.ToDateTime(objDelivery.DateAdded);

                drpDelivieryTime1.SelectedValue = objDelivery.DeliveryTime;
                drpMealDeliver.SelectedValue = objDelivery.ItemCode.ToString();
                if (objDelivery.planid != null && objDelivery.planid != 0)
                {
                    drpPlan.SelectedValue = objDelivery.planid.ToString();
                }
                else
                {
                    drpPlan.Enabled = true;
                }

                int Deliverytime = Convert.ToInt32(objDelivery.DeliveryTime);
                string dtime = objDelivery.DeliveryTime.ToString();
                int mealid = Convert.ToInt32(objDelivery.ItemCode);
                if (DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.DeliveryTime == dtime && p.ItemCode == mealid).Count() < 1)
                {
                    List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                    if (ListRef.Count() > 0)
                    {
                        //string MSG1 = "You Select " + drpDelivieryTime1.SelectedItem + ", Your previous Record Are must be delete, Are You sure ?";
                        //btnDelivery.OnClientClick = "return ConfirmMsgdisp('" + MSG1 + "');";
                    }
                    else
                    {
                        btnDelivery.OnClientClick = null;
                    }
                }


                ViewState["EditDeliveryCID"] = CID;
                ViewState["EditDeliveryMYID"] = MYID;

                btnDelivery.Text = "Update Delivery";
                drpMealDeliver.Enabled = true;

            }
        }


        protected void drpDelivieryTime1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Deliverytime = Convert.ToInt32(drpDelivieryTime1.SelectedValue);
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            string dtime = drpDelivieryTime1.SelectedValue.ToString();
            int mealid = Convert.ToInt32(drpMealDeliver.SelectedValue);
            int CID = Convert.ToInt32(ViewState["Edit"]);

            int mdid = Convert.ToInt32(ViewState["EditDeliveryCID"]);

            if (ViewState["ChangeMode"] == null)
            {
                btnallmeal.Visible = true;
                drpMealDeliver.Enabled = true;
                //bidmeal();
                List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
                List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
                if (DB.tblProduct_Plan.Where(p => p.planid == planid && p.TenentID == TID).Count() > 0)
                {
                    foreach (Database.planmealsetup item2 in Listplanmealsetup.Where(a => a.planid == planid).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()))
                    {
                        if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "Mealtype" && p.REFID == item2.MealType).Count() > 0)
                        {
                            Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "Mealtype" && p.REFID == item2.MealType);
                            ListREFTABLE.Add(obj2);
                        }
                    }
                }
                string Deliverytime1 = drpDelivieryTime1.SelectedValue.ToString();
                List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.DeliveryTime == Deliverytime1 && p.LikeType == "M" && p.planid == planid).ToList();

                foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                {
                    if (ListREFTABLE.Where(p => p.TenentID == TID && p.REFID == itms.ItemCode).Count() > 0)
                    {
                        Database.REFTABLE objmealtype = ListREFTABLE.Single(p => p.TenentID == TID && p.REFID == itms.ItemCode);
                        ListREFTABLE.Remove(objmealtype);
                    }
                }

                drpMealDeliver.DataSource = ListREFTABLE.OrderBy(p => p.REFNAME1);
                drpMealDeliver.DataTextField = "REFNAME1";
                drpMealDeliver.DataValueField = "REFID";
                drpMealDeliver.DataBind();
                drpMealDeliver.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                drpMealDeliver.Enabled = false;
            }



        }
        protected void btnallmeal_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                int planid = Convert.ToInt32(drpPlan.SelectedValue);
                CID = Convert.ToInt32(drpCustomerId.SelectedValue);
                if (CID != 0)
                {
                    List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
                    List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
                    if (DB.tblProduct_Plan.Where(p => p.planid == planid && p.TenentID == TID).Count() > 0)
                    {
                        foreach (Database.planmealsetup item2 in Listplanmealsetup.Where(a => a.planid == planid).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()))
                        {
                            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "Mealtype" && p.REFID == item2.MealType).Count() > 0)
                            {
                                Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "Mealtype" && p.REFID == item2.MealType);
                                ListREFTABLE.Add(obj2);
                            }
                        }
                    }

                    foreach (Database.REFTABLE items in ListREFTABLE)
                    {
                        string DeliveryTime = drpDelivieryTime1.SelectedValue;

                        if (DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID && p.LikeType == "M" && p.planid == planid && p.DeliveryTime == DeliveryTime && p.ItemCode == items.REFID).Count() < 1)
                        {
                            Database.tblcontact_addon1_dtl objtblcontact_addon1_dtl = new tblcontact_addon1_dtl();
                            objtblcontact_addon1_dtl.TenentID = TID;
                            objtblcontact_addon1_dtl.LocationID = LID;
                            objtblcontact_addon1_dtl.customerID = CID;// Convert.ToInt32(drpCustomerId.SelectedValue);
                            objtblcontact_addon1_dtl.myID = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID).Count() > 0 ? DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LocationID == LID && p.customerID == CID).Max(p => p.myID) + 1 : 1;
                            objtblcontact_addon1_dtl.LikeType = "M";
                            objtblcontact_addon1_dtl.DeliveryTime = DeliveryTime;
                            objtblcontact_addon1_dtl.planid = planid;
                            objtblcontact_addon1_dtl.DateAdded = DateTime.Now;
                            objtblcontact_addon1_dtl.ItemCode = items.REFID;
                            objtblcontact_addon1_dtl.Remarks = items.REFID.ToString();
                            objtblcontact_addon1_dtl.active = true;
                            DB.tblcontact_addon1_dtl.AddObject(objtblcontact_addon1_dtl);
                            DB.SaveChanges();
                        }
                    }

                    btnallmeal.Visible = false;
                    MCData(CID);
                    drpMealDeliver.Enabled = false;
                    drpDelivieryTime1.Enabled = false;

                }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Plase Select Customer";
                }
                scope.Complete();
            }
            UpdatePanelDelivery.Update();
        }
        public string GetPlane(int ID)
        {
            if (DB.tblProduct_Plan.Where(p => p.planid == ID && p.TenentID == TID).Count() > 0)
            {
                string PlaneName = DB.tblProduct_Plan.Single(p => p.planid == ID && p.TenentID == TID).planname1;
                return PlaneName;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void LnkSearcustomer_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";

            int CustomerSearch = Convert.ToInt32(txtCustomerSearch.Text);
            List<Database.tblcontact_addon1> ListCID = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == CustomerSearch).ToList();//&& p.active == true
            if (ListCID.Count() > 0)
            {
                PanelError.Visible = true;
                lblerror.Text = "Customer Already Exist";
                return;
            }

            //List<Database.TBLCOMPANYSETUP> List = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y" && (p.COMPNAME1.ToUpper().Contains(CustomerSearch.ToUpper()) || p.COMPID.Contains(CustomerSearch.ToUpper()) || p.COMPNAME2.ToUpper().Contains(CustomerSearch.ToUpper()) || p.COMPNAME3.ToUpper().Contains(CustomerSearch.ToUpper()))).ToList();
            string CompanyType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").REFID.ToString() : "0";
            List<Database.TBLCOMPANYSETUP> List = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.Active == "Y" && p.COMPID == CustomerSearch && p.CompanyType == CompanyType).ToList();
            List<Database.tblcontact_addon1> ListCoustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID).ToList();
            List<Database.TBLCOMPANYSETUP> FinalList = new List<TBLCOMPANYSETUP>();

            foreach (Database.TBLCOMPANYSETUP items in List)
            {
                int Count = ListCoustomer.Where(p => p.TenentID == TID && p.CustomerId == items.COMPID).Count();
                if (Count < 1)
                {
                    Database.TBLCOMPANYSETUP objcomp = List.Single(p => p.TenentID == TID && p.COMPID == items.COMPID);
                    FinalList.Add(objcomp);
                }
            }

            drpCustomerId.DataSource = FinalList.OrderBy(p => p.COMPNAME1);
            drpCustomerId.DataTextField = "COMPNAME1";
            drpCustomerId.DataValueField = "COMPID";
            drpCustomerId.DataBind();
            drpCustomerId.Items.Insert(0, new ListItem("-- Select --", "0"));


            if (FinalList.Count() < 1)
            {
                PanelError.Visible = true;
                lblerror.Text = "Customer not found in search";
                return;
            }
            else
            {
                drpCustomerId.SelectedIndex = 1;
            }
        }

        protected void drpPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

            drpDelivieryTime1.Enabled = true;
            drpDelivieryTime1.SelectedIndex = 0;
            drpMealDeliver.Enabled = true;

            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
            if (DB.tblProduct_Plan.Where(p => p.planid == planid && p.TenentID == TID).Count() > 0)
            {
                foreach (Database.planmealsetup item2 in Listplanmealsetup.Where(a => a.planid == planid).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()))
                {
                    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == item2.MealType).Count() > 0)
                    {
                        Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == item2.MealType);
                        ListREFTABLE.Add(obj2);
                    }
                }
            }

            drpMealDeliver.DataSource = ListREFTABLE.OrderBy(p => p.REFNAME1);
            drpMealDeliver.DataTextField = "REFNAME1";
            drpMealDeliver.DataValueField = "REFID";
            drpMealDeliver.DataBind();
            drpMealDeliver.Items.Insert(0, new ListItem("-- Select --", "0"));

            List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M" && p.planid == planid).ToList();
            if (ListDeliveryMealtype.Count() > 0)
            {
                foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                {
                    int Deliverytime = Convert.ToInt32(itms.DeliveryTime);
                    List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                    if (ListRef.Count() > 0)
                    {
                        Database.REFTABLE objreftime = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1");
                        drpDelivieryTime1.SelectedValue = objreftime.REFID.ToString();
                        drpDelivieryTime1.Enabled = false;
                        drpMealDeliver.Enabled = true;
                        btnallmeal.Visible = true;
                        //btnDelivery.Enabled = false;                        
                    }
                }
            }

            string Deliverytime1 = drpDelivieryTime1.SelectedValue.ToString();

            foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
            {
                if (ListREFTABLE.Where(p => p.TenentID == TID && p.REFID == itms.ItemCode).Count() > 0)
                {
                    Database.REFTABLE objmealtype = ListREFTABLE.Single(p => p.TenentID == TID && p.REFID == itms.ItemCode);
                    ListREFTABLE.Remove(objmealtype);
                }
            }

            drpMealDeliver.DataSource = ListREFTABLE.OrderBy(p => p.REFNAME1);
            drpMealDeliver.DataTextField = "REFNAME1";
            drpMealDeliver.DataValueField = "REFID";
            drpMealDeliver.DataBind();
            drpMealDeliver.Items.Insert(0, new ListItem("-- Select --", "0"));

        }

        public void bidmeal()
        {
            CID = Convert.ToInt32(drpCustomerId.SelectedValue);

            int TMPplanid = Convert.ToInt32(drpPlan.SelectedValue);
            List<Database.tblcontact_addon1_dtl> ListDeliveryMealtype = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M" && p.planid == TMPplanid && p.planid != null && p.planid != 0).ToList();

            if (ListDeliveryMealtype.Count() > 0)
            {
                Database.tblcontact_addon1_dtl objtblcon = ListDeliveryMealtype.FirstOrDefault();
                drpPlan.SelectedValue = objtblcon.planid.ToString();
                drpPlan.Enabled = false;
                List<Database.REFTABLE> ListRaftable = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").ToList();
                List<Database.REFTABLE> ListTime = new List<Database.REFTABLE>();
                foreach (Database.tblcontact_addon1_dtl itms in ListDeliveryMealtype)
                {
                    int Deliverytime = Convert.ToInt32(itms.DeliveryTime);
                    List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1").ToList();
                    if (ListRef.Count() > 0)
                    {
                        Database.REFTABLE objreftime = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Deliverytime && p.SWITCH3 == "1");
                        ListTime.Add(objreftime);
                        drpDelivieryTime1.SelectedValue = objreftime.REFID.ToString();
                        drpDelivieryTime1.Enabled = false;
                        //btnDelivery.Enabled = false;                        
                    }
                    //int planid = Convert.ToInt32(itms.planid);
                    string Deliverytimes = drpDelivieryTime1.SelectedValue.ToString();

                    if (ListRaftable.Where(p => p.TenentID == TID && p.REFID == itms.ItemCode).Count() > 0)
                    {
                        Database.REFTABLE objmealtype = ListRaftable.Single(p => p.TenentID == TID && p.REFID == itms.ItemCode);
                        ListRaftable.Remove(objmealtype);
                    }
                }

                if (ListTime.Count() > 0)
                {

                    // drpDelivieryTime1.Enabled = false;
                }
                else
                {
                    //drpDelivieryTime1.Enabled = true;
                }

                drpMealDeliver.DataSource = ListRaftable.OrderBy(p => p.REFNAME1);
                drpMealDeliver.DataTextField = "REFNAME1";
                drpMealDeliver.DataValueField = "REFID";
                drpMealDeliver.DataBind();
                drpMealDeliver.Items.Insert(0, new ListItem("-- Select --", "0"));

                if (ListRaftable.Count() < 1)
                {
                    //btnDelivery.Enabled = false;
                    //drpMealDeliver.Enabled = false;
                }
                else
                {
                    //btnDelivery.Enabled = true;
                    //drpMealDeliver.Enabled = true;
                }

            }
            else
            {
                if (DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.active == true && p.customerID == CID && p.LikeType == "M" && p.planid != null && p.planid != 0).Count() > 0)
                {
                    btnDelivery.Enabled = true;
                    drpPlan.Enabled = true;
                }
                else
                {
                    // drpPlan.Enabled = false;
                }
            }


        }

        protected void lnkaddplan_Click(object sender, EventArgs e)
        {
            newplan = true;

            CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            drpPlan.Enabled = true;
            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
            /*
             tblcontact_addon1_dtl =DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.LikeType == "M").ToList();
             not in 
             */


            foreach (Database.planmealsetup item in Listplanmealsetup.GroupBy(p => p.planid).Select(p => p.FirstOrDefault()))
            {
                if (DB.tblProduct_Plan.Where(p => p.planid == item.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == item.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Add(obj);
                }
            }

            List<Database.tblcontact_addon1_dtl> Listdtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.LikeType == "M" && p.active == true).ToList();

            foreach (Database.tblcontact_addon1_dtl itms in Listdtl)
            {
                if (ListtblProduct_Plan.Where(p => p.planid == itms.planid).Count() > 0)
                {
                    Database.tblProduct_Plan objplan = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == itms.planid);
                    ListtblProduct_Plan.Remove(objplan);
                }
            }

            drpPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planname1);
            drpPlan.DataTextField = "planname1";
            drpPlan.DataValueField = "planid";
            drpPlan.DataBind();
            //if (ListtblProduct_Plan.Count() == 0) drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));
            btnallmeal.Enabled = true;
        }

        protected void btneditAddress_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblerror.Text = "";

            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            int ADDID = Convert.ToInt32(drpAddress1.SelectedValue);

            if (CID != 0)
            {
                if (ADDID != 0)
                {
                    Response.Redirect("TBLCONTACT_DEL_ADRES.aspx?CID=" + CID + "&Addid=" + ADDID + "&PageMode=" + PageMode);
                }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Please Select Address...";
                }
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Please Select Customer...";
            }
        }

        public void savedata()
        {
            int CID = Convert.ToInt32(drpCustomerId.SelectedValue);
            if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == CID && p.LocationID == LID).Count() < 1)
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
                {
                    DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                }
                Database.tblcontact_addon1 objtblcontact_addon1 = new Database.tblcontact_addon1();
                //Server Content Send data Yogesh
                //objtblcontact_addon1.MyID = DB.tblcontact_addon1.Count() > 0 ? Convert.ToInt32(DB.tblcontact_addon1.Max(p => p.MyID) + 1) : 1;
                objtblcontact_addon1.TenentID = TID;
                objtblcontact_addon1.LocationID = LID;
                //objtblcontact_addon1.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                objtblcontact_addon1.CustomerId = Convert.ToInt32(drpCustomerId.SelectedValue);
                //objtblcontact_addon1.DeliveryCountry = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COUNTRYID.ToString() : null;
                objtblcontact_addon1.State = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).STATE.ToString() : null;
                objtblcontact_addon1.Area = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).CITY.ToString() : null;
                //objtblcontact_addon1.DeliveryTime = (drpDeliveryTime.SelectedValue);
                //objtblcontact_addon1.DeliveryTime = tags_1.Text;
                if (drpCOUNTRYID.SelectedValue != "0")
                {
                    objtblcontact_addon1.DeliveryCountry = drpCOUNTRYID.SelectedValue.ToString();
                }
                else
                {
                    objtblcontact_addon1.DeliveryCountry = "126";
                }
                objtblcontact_addon1.AdvCollectionStatus = (drpAdvCollectionStatus.SelectedValue);
                objtblcontact_addon1.PromotionOn = (drpPromotionOn.SelectedValue);
                objtblcontact_addon1.agreedcabs = txtAgreedCabs.Text;
                if (txtHeight.Text != "")
                    objtblcontact_addon1.Height = txtHeight.Text;
                //objtblcontact_addon1.latituet = Convert.ToDecimal(txtlatituet.Text);
                //objtblcontact_addon1.longituet = Convert.ToDecimal(txtlongituet.Text);
                objtblcontact_addon1.active = true;
                objtblcontact_addon1.datetime = DateTime.Now;

                DB.tblcontact_addon1.AddObject(objtblcontact_addon1);
                DB.SaveChanges();
            }
            else
            {
                Database.tblcontact_addon1 objtblcontact_addon1 = DB.tblcontact_addon1.Single(p => p.TenentID == TID && p.CustomerId == CID && p.LocationID == LID);
                if (drpCOUNTRYID.SelectedValue != "0")
                {
                    objtblcontact_addon1.DeliveryCountry = drpCOUNTRYID.SelectedValue.ToString();
                }
                else
                {
                    objtblcontact_addon1.DeliveryCountry = "126";
                }
                objtblcontact_addon1.AdvCollectionStatus = (drpAdvCollectionStatus.SelectedValue);
                objtblcontact_addon1.PromotionOn = (drpPromotionOn.SelectedValue);
                objtblcontact_addon1.agreedcabs = txtAgreedCabs.Text;
                objtblcontact_addon1.Height = txtHeight.Text;
                DB.SaveChanges();
            }
        }

        protected void BtnSaveandContinue_Click(object sender, EventArgs e)
        {
            savedata();
            int CustomerId = Convert.ToInt32(drpCustomerId.SelectedValue);
            EditFood(CustomerId);
        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lnkbtnActive = (LinkButton)e.Item.FindControl("lnkbtnActive");
            Label lblCID = (Label)e.Item.FindControl("lblCID");
            int ID = Convert.ToInt32(lblCID.Text);            
            if ( DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == ID  && p.active == true).Count() > 0)
            {
                lnkbtnActive.Visible = false;
            }
            else
            {
                lnkbtnActive.Text = "Active";
                lnkbtnActive.CssClass = "btn btn-sm green filter-submit margin-bottom";
            }
        }


    }
}