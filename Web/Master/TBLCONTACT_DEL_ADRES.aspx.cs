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

namespace Web.Master
{
    public partial class TBLCONTACT_DEL_ADRES : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        int menu = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["MID"] != null)
            {
                string Menuid = Request.QueryString["MID"].ToString();
                menu = ((AcmMaster)Page.Master).GetMenuID(Menuid);
            }

            if (!IsPostBack)
            {
                int TID = (((USER_MST)Session["USER"]).TenentID);
                string uidd = ((USER_MST)Session["USER"]).USER_ID.ToString();
                bool Isadmin = Classes.CRMClass.ISAdmin(TID, uidd);
                if (Isadmin == false)
                {
                    btnEditLable.Visible = false;
                }
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                //drpContactID.Enabled = false;
                drpContactMyID.Enabled = false;
                //drpSTATE.Enabled = false;
                //drpCity.Enabled = false;
                drpCOUNTRYID.Enabled = false;
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                Button1.Enabled = false;

                SetDefaultCitystateCountry();

                if (Request.QueryString["CID"] != null && Request.QueryString["Addid"] != null)
                {
                    if (Request.QueryString["PageMode"] != null)
                    {
                        string PageMode = Request.QueryString["PageMode"].ToString();
                        ViewState["PageModeCID"] = PageMode;
                    }
                    else
                    {
                        ViewState["PageModeCID"] = "View";
                    }

                    Button1.Enabled = true;
                    int LID = (((USER_MST)Session["USER"]).LOCATION_ID);
                    Clear();
                    int CID = Convert.ToInt32(Request.QueryString["CID"]);
                    int ADDID = Convert.ToInt32(Request.QueryString["Addid"]);

                    bindList1(CID);
                    //drpContactID.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);//.Take(500)
                    //drpContactID.DataTextField = "COMPNAME1";
                    //drpContactID.DataValueField = "COMPID";
                    //drpContactID.DataBind();
                    //drpContactID.Items.Insert(0, new ListItem("-- Select --", "0"));

                    //drpContactID.SelectedValue = CID.ToString();
                    drpContactMyID.SelectedValue = CID.ToString();
                    EditAddress(CID, ADDID);

                    //drpSTATE.SelectedValue = obj.State;
                    //int CityID = Convert.ToInt32(obj.Area);
                    //int COUNTRYID = Convert.ToInt32(obj.DeliveryCountry);
                    //int State=Convert.ToInt32(obj.State);

                    //drpCity.SelectedValue = obj.Area;// DB.tblCityStatesCounties.Single(p => p.CityID == CityID).CityEnglish;
                    Write();

                    if (ADDID != 0)
                    {
                        btnAdd.Text = "Update";
                        btnAdd.ValidationGroup = "s";
                        BindData(CID);
                        BindAddon1List(CID);
                    }
                    else
                    {
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "s";
                        GetCompContactData();
                        BindAddon1List(CID);
                    }


                }
                else
                {
                    GetMap(0, 0, "address", "");

                    List<Database.TBLCONTACT_DEL_ADRES> DelList = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.Active == "Y").OrderBy(p => p.ContactMyID).ToList();
                    List<Database.TBLCONTACT_DEL_ADRES> NewCustomerList = new List<Database.TBLCONTACT_DEL_ADRES>();
                    if (TID == 1)
                    {
                        NewCustomerList = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID).ToList();
                    }
                    else
                    {
                        List<Database.TBLCOMPANYSETUP> ListCustomer = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).ToList();
                        foreach (Database.TBLCOMPANYSETUP item in ListCustomer)
                        {
                            List<Database.TBLCONTACT_DEL_ADRES> DelList1 = DelList.Where(p => p.ContactMyID == item.COMPID).ToList();
                            foreach (Database.TBLCONTACT_DEL_ADRES itemAdd in DelList1)
                            {
                                Database.TBLCONTACT_DEL_ADRES obj = DelList1.Single(p => p.TenentID == TID && p.ContactMyID == itemAdd.ContactMyID && p.DeliveryAdressID == itemAdd.DeliveryAdressID && p.Active == "Y");
                                NewCustomerList.Add(obj);
                            }
                        }
                    }
                    Listview1.DataSource = NewCustomerList.OrderByDescending(p => p.ContactMyID);//DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID);
                    Listview1.DataBind();
                    FirstData();
                }

            }
        }

        public void SetDefaultCitystateCountry()
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            List<Database.TBLCONTACT_DEL_ADRES> ListAdd = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && ((p.CITY == null || p.CITY == "" || p.CITY == "null") || (p.STATE == null || p.STATE == "" || p.STATE == "null") || (p.COUNTRYID == null || p.COUNTRYID == 0))).ToList();

            foreach (Database.TBLCONTACT_DEL_ADRES items in ListAdd)
            {
                List<Database.TBLCOMPANYSETUP> ListComp = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == items.ContactMyID).ToList();

                Database.TBLCONTACT_DEL_ADRES Objaddr = items;

                if (items.CITY == null || items.CITY == "" || items.CITY == "null")
                {
                    Objaddr.CITY = ListComp.Where(p => p.CITY != "" && p.CITY != null).Count() > 0 ? ListComp.FirstOrDefault().CITY : "0";
                }

                if (items.STATE == null || items.STATE == "" || items.STATE == "null")
                {
                    Objaddr.STATE = ListComp.Where(p => p.STATE != "" && p.STATE != null).Count() > 0 ? ListComp.FirstOrDefault().STATE : "0";
                }

                if (items.COUNTRYID == null || items.COUNTRYID == 0)
                {
                    Objaddr.COUNTRYID = ListComp.Where(p => p.COUNTRYID != null && p.COUNTRYID != 0).Count() > 0 ? ListComp.FirstOrDefault().COUNTRYID : 126;
                }

                Objaddr.UploadDate = DateTime.Now;
                Objaddr.Uploadby = "System";
                Objaddr.SynID = 2;

                DB.SaveChanges();
            }


        }

        public void GetMap(double lat, double lng, string address, string googlee)
        {
            string sScript = "<script>\n";
            sScript += "function initialize() {\n";
            if (lat != null && lng != null && lat != 0 && lng != 0)
            {
                sScript += "var n = '" + lat + "';\n";
                sScript += " var n1 = '" + lng + "';\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtLatitute\").value = '" + lat + "';\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtLongitute\").value = '" + lng + "';\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtADDR2\").value = '" + address + "';\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtinfoWindow\").value = '" + address + "';\n";

            }
            else
            {
                sScript += "if (navigator.geolocation) {\n";
                sScript += "navigator.geolocation.getCurrentPosition(function (p) {\n";
                sScript += " var n = p.coords.latitude.toString();\n";
                sScript += "var n1 = p.coords.longitude.toString();\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtLatitute\").value = p.coords.latitude;\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtLongitute\").value = p.coords.longitude;\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtADDR2\").value = p.coords.formatted_address;\n";
                sScript += "document.getElementById(\"ContentPlaceHolder1_txtinfoWindow\").value = p.coords.formatted_address;\n";
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
            sScript += "var input = document.getElementById('searchInput123');\n";
            sScript += "map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);\n";
            sScript += "var geocoder = new google.maps.Geocoder();\n";
            sScript += "var autocomplete = new google.maps.places.Autocomplete(input);\n";
            sScript += "autocomplete.bindTo('bounds', map);\n";
            sScript += "var infowindow = new google.maps.InfoWindow();\n";
            sScript += "autocomplete.addListener('place_changed', function () {\n";
            sScript += "    infowindow.close();\n";
            sScript += "    marker.setVisible(false);\n";
            sScript += "    var place = autocomplete.getPlace();\n";
            sScript += "    if (!place.geometry) {\n";
            sScript += "        window.alert(\"Autocomplete's returned place contains no geometry\");\n";
            sScript += "        return;\n";
            sScript += "    }\n";
            sScript += "    if (place.geometry.viewport) {\n";
            sScript += "        map.fitBounds(place.geometry.viewport);\n";
            sScript += "    } else {\n";
            sScript += "        map.setCenter(place.geometry.location);\n";
            sScript += "        map.setZoom(17);\n";
            sScript += "    }\n";
            sScript += "    marker.setPosition(place.geometry.location);\n";
            sScript += "    marker.setVisible(true);\n";
            sScript += "    bindDataToForm(place.formatted_address, place.geometry.location.lat(), place.geometry.location.lng());\n";
            sScript += "    infowindow.setContent(place.formatted_address);\n";
            sScript += "    infowindow.open(map, marker);\n";
            sScript += "});\n";
            sScript += "google.maps.event.addListener(marker, 'dragend', function () {\n";
            sScript += "    geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {\n";
            sScript += "        if (status == google.maps.GeocoderStatus.OK) {\n";
            sScript += "            if (results[0]) {\n";
            sScript += "                bindDataToForm(results[0].formatted_address, marker.getPosition().lat(), marker.getPosition().lng());\n";
            sScript += "                infowindow.setContent(results[0].formatted_address);\n";
            sScript += "                infowindow.open(map, marker);\n";
            sScript += "            }\n";
            sScript += "        }\n";
            sScript += "    });\n";
            sScript += "});\n";
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
            sScript += "function bindDataToForm(address, lat, lng) {\n";
            sScript += "    document.getElementById('ContentPlaceHolder1_txtADDR2').value = address;\n";
            sScript += "    document.getElementById('ContentPlaceHolder1_txtLatitute').value = lat;\n";
            sScript += "    document.getElementById('ContentPlaceHolder1_txtLongitute').value = lng;\n";
            sScript += "    document.getElementById('ContentPlaceHolder1_txtinfoWindow').value = address;\n";

            sScript += "}\n";
            sScript += "google.maps.event.addDomListener(window, 'load', initialize);\n";
            sScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", sScript);
        }
        #region Step2
        public void BindAddon1List(int Contactid)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Addid = Convert.ToInt32(Request.QueryString["Addid"]);
            if (Request.QueryString["CID"] != null && Addid == 0)
            {
                if (ViewState["Add"] != null)
                {
                    int DelAddID = Convert.ToInt32(ViewState["Add"]);
                    List<Database.TBLCONTACT_DEL_ADRES> DelList = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.DeliveryAdressID == DelAddID && p.ContactID == Contactid && p.Active == "Y").ToList();
                    Listview1.DataSource = DelList.OrderByDescending(p => p.ContactMyID);
                    Listview1.DataBind();
                }
                else
                {
                    List<Database.TBLCONTACT_DEL_ADRES> DelList = null;
                    Listview1.DataSource = DelList;
                    Listview1.DataBind();
                }

            }
            else if (Request.QueryString["CID"] != null && Addid != 0)
            {
                if (Request.QueryString["Addid"] != null)
                {
                    int DelAddID = Convert.ToInt32(Request.QueryString["Addid"]);
                    List<Database.TBLCONTACT_DEL_ADRES> DelList = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.DeliveryAdressID == DelAddID && p.ContactID == Contactid && p.Active == "Y").ToList();
                    Listview1.DataSource = DelList.OrderByDescending(p => p.ContactMyID);
                    Listview1.DataBind();
                }

            }

        }

        public void bindList1(int ContactID)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int LID = (((USER_MST)Session["USER"]).LOCATION_ID);

            List<Database.TBLCONTACT_DEL_ADRES> DelList = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactID == ContactID && p.Active == "Y").ToList();
            Listview1.DataSource = DelList.OrderByDescending(p => p.ContactMyID);//DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID);
            Listview1.DataBind();
        }
        public void BindData(int ContactID)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int LID = (((USER_MST)Session["USER"]).LOCATION_ID);

            List<Database.TBLCONTACT_DEL_ADRES> DelList = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactID == ContactID && p.Active == "Y").ToList();
            Listview1.DataSource = DelList.OrderByDescending(p => p.ContactMyID);//DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID);
            Listview1.DataBind();

            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ContactID).Count() > 0)
            {
                Database.TBLCOMPANYSETUP obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ContactID);

                int COUNTRYID = Convert.ToInt32(obj.COUNTRYID);
                drpCOUNTRYID.SelectedValue = COUNTRYID.ToString();
                drpSTATE.DataSource = DB.tblStates.Where(p => p.COUNTRYID == COUNTRYID);
                drpSTATE.DataTextField = "MYNAME1";
                drpSTATE.DataValueField = "StateID";
                drpSTATE.DataBind();
                drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));

                drpSTATE.SelectedValue = obj.STATE;

                int state = Convert.ToInt32(obj.STATE);
                drpCity.DataSource = DB.tblCityStatesCounties;
                drpCity.DataTextField = "CityEnglish";
                drpCity.DataValueField = "CityID";
                drpCity.DataBind();
                drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));

                drpCity.SelectedValue = obj.CITY;
            }



            //List<TBLCONTACT_DEL_ADRES> List = DB.TBLCONTACT_DEL_ADRES.Where(p=>p.TenentID==TID).OrderBy(m => m.ContactMyID).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void EditAddress(int CID, int AddID)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int LID = (((USER_MST)Session["USER"]).LOCATION_ID);

            if (AddID != 0)
            {
                //int ID = Convert.ToInt32(e.CommandArgument);
                Database.TBLCONTACT_DEL_ADRES objTBLCONTACT_DEL_ADRES = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AddID && p.Active == "Y");
                int COUNTRYID = Convert.ToInt32(objTBLCONTACT_DEL_ADRES.COUNTRYID);
                int State = Convert.ToInt32(objTBLCONTACT_DEL_ADRES.STATE);
                bindstatecity(COUNTRYID, State);
                if (objTBLCONTACT_DEL_ADRES.ContactMyID != null && objTBLCONTACT_DEL_ADRES.ContactMyID != 0)
                {
                    drpContactMyID.SelectedValue = objTBLCONTACT_DEL_ADRES.ContactMyID.ToString();
                }

                //drpDeliveryAdressID.SelectedValue = objTBLCONTACT_DEL_ADRES.DeliveryAdressID.ToString();
                //txtinfoWindow.Text = objTBLCONTACT_DEL_ADRES.GoogleName != null && objTBLCONTACT_DEL_ADRES.GoogleName != "" ? objTBLCONTACT_DEL_ADRES.GoogleName.ToString() : "";
                txtLatitute.Text = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? objTBLCONTACT_DEL_ADRES.Latitute.ToString() : "29.346214";
                txtLongitute.Text = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? objTBLCONTACT_DEL_ADRES.Longitute.ToString() : "48.0913227";

                //if (objTBLCONTACT_DEL_ADRES.ContactID != null && objTBLCONTACT_DEL_ADRES.ContactID != 0)
                //{
                //    drpContactID.SelectedValue = objTBLCONTACT_DEL_ADRES.ContactID.ToString();
                //}

                //txtAdressShortName1.Text = objTBLCONTACT_DEL_ADRES.AdressShortName1 != null && objTBLCONTACT_DEL_ADRES.AdressShortName1 != "" ? objTBLCONTACT_DEL_ADRES.AdressShortName1.ToString() : "";
                txtAdressName1.Text = objTBLCONTACT_DEL_ADRES.AdressName1 != null && objTBLCONTACT_DEL_ADRES.AdressName1 != "" ? objTBLCONTACT_DEL_ADRES.AdressName1.ToString() : "";
                txtADDR1.Text = objTBLCONTACT_DEL_ADRES.ADDR1 != null && objTBLCONTACT_DEL_ADRES.ADDR1 != "" ? objTBLCONTACT_DEL_ADRES.ADDR1.ToString() : "";
                txtADDR2.Text = objTBLCONTACT_DEL_ADRES.ADDR2 != null && objTBLCONTACT_DEL_ADRES.ADDR2 != "" ? objTBLCONTACT_DEL_ADRES.ADDR2.ToString() : "";

                txtBlock.Text = objTBLCONTACT_DEL_ADRES.Block != null && objTBLCONTACT_DEL_ADRES.Block != "" ? objTBLCONTACT_DEL_ADRES.Block.ToString() : "";
                txtstreet.Text = objTBLCONTACT_DEL_ADRES.Street != null && objTBLCONTACT_DEL_ADRES.Street != "" ? objTBLCONTACT_DEL_ADRES.Street.ToString() : "";
                txtlane.Text = objTBLCONTACT_DEL_ADRES.Lane != null && objTBLCONTACT_DEL_ADRES.Lane != "" ? objTBLCONTACT_DEL_ADRES.Lane.ToString() : "";
                txtBuilding.Text = objTBLCONTACT_DEL_ADRES.Building != null && objTBLCONTACT_DEL_ADRES.Building != "" ? objTBLCONTACT_DEL_ADRES.Building.ToString() : "";
                txtFlat.Text = objTBLCONTACT_DEL_ADRES.ForFlat != null && objTBLCONTACT_DEL_ADRES.ForFlat != "" ? objTBLCONTACT_DEL_ADRES.ForFlat.ToString() : "";

                if (objTBLCONTACT_DEL_ADRES.CITY != null && objTBLCONTACT_DEL_ADRES.CITY != "" && objTBLCONTACT_DEL_ADRES.CITY != "0")
                {
                    drpCity.SelectedValue = objTBLCONTACT_DEL_ADRES.CITY.ToString();
                }
                if (objTBLCONTACT_DEL_ADRES.STATE != null && objTBLCONTACT_DEL_ADRES.STATE != "" && objTBLCONTACT_DEL_ADRES.STATE != "0")
                {
                    drpSTATE.SelectedValue = objTBLCONTACT_DEL_ADRES.STATE.ToString();
                }
                if (objTBLCONTACT_DEL_ADRES.COUNTRYID != null && objTBLCONTACT_DEL_ADRES.COUNTRYID != 0)
                {
                    drpCOUNTRYID.SelectedValue = objTBLCONTACT_DEL_ADRES.COUNTRYID.ToString();
                }

                txtREMARKS.Text = objTBLCONTACT_DEL_ADRES.REMARKS != null && objTBLCONTACT_DEL_ADRES.REMARKS != "" ? objTBLCONTACT_DEL_ADRES.REMARKS.ToString() : "";
                //txtCRUP_ID.Text = objTBLCONTACT_DEL_ADRES.CRUP_ID.ToString();
                //txtCUSERID.Text = objTBLCONTACT_DEL_ADRES.CUSERID.ToString();
                //txtENTRYDATE.Text = objTBLCONTACT_DEL_ADRES.ENTRYDATE.ToString();
                //txtENTRYTIME.Text = objTBLCONTACT_DEL_ADRES.ENTRYTIME.ToString();
                //txtUPDTTIME.Text = objTBLCONTACT_DEL_ADRES.UPDTTIME.ToString();
                //cbActive.Text = objTBLCONTACT_DEL_ADRES.Active.ToString();
                //cbDefualt.Checked = (objTBLCONTACT_DEL_ADRES.Defualt == true) ? true : false;

                double lat = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Latitute) : 29.346214;
                double lng = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Longitute) : 48.0913227;
                string add1 = objTBLCONTACT_DEL_ADRES.ADDR2.ToString();
                string Google = objTBLCONTACT_DEL_ADRES.GoogleName.ToString();
                if (lat != null && lng != null && lat != 0 && lng != 0)
                {
                    GetMap(lat, lng, add1, Google);
                }
                else
                {
                    GetMap(0, 0, "", "");
                }

                btnAdd.Text = "Update";
                ViewState["Edit"] = CID;
                ViewState["Addid"] = AddID;
                Write();
            }
            else
            {
                GetMap(0, 0, "", "");
            }

        }
        public void GetShow()
        {

            lblContactMyID1s.Attributes["class"] = lblLatitute1s.Attributes["class"] = lblLongitute1s.Attributes["class"] = lblAdressName11s.Attributes["class"] = lblADDR11s.Attributes["class"] = lblADDR21s.Attributes["class"] = lblCITY1s.Attributes["class"] = lblSTATE1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  getshow";// lblActive1s.Attributes["class"] = lblDefualt1s.Attributes["class"] =lblCUSERID1s.Attributes["class"] = lblENTRYDATE1s.Attributes["class"] = lblENTRYTIME1s.Attributes["class"] = lblUPDTTIME1s.Attributes["class"] =lblDeliveryAdressID1s.Attributes["class"] =  lblAdressShortName11s.Attributes["class"] =lblContactID1s.Attributes["class"] = 
            lblContactMyID2h.Attributes["class"] = lblLatitute2h.Attributes["class"] = lblLongitute2h.Attributes["class"] = lblAdressName12h.Attributes["class"] = lblADDR12h.Attributes["class"] = lblADDR22h.Attributes["class"] = lblCITY2h.Attributes["class"] = lblSTATE2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  gethide";//lblActive2h.Attributes["class"] = lblDefualt2h.Attributes["class"] = lblCUSERID2h.Attributes["class"] = lblENTRYDATE2h.Attributes["class"] = lblENTRYTIME2h.Attributes["class"] = lblUPDTTIME2h.Attributes["class"] =lblDeliveryAdressID2h.Attributes["class"]  = lblAdressShortName12h.Attributes["class"] =lblContactID2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblContactMyID1s.Attributes["class"] = lblLatitute1s.Attributes["class"] = lblLongitute1s.Attributes["class"] = lblAdressName11s.Attributes["class"] = lblADDR11s.Attributes["class"] = lblADDR21s.Attributes["class"] = lblCITY1s.Attributes["class"] = lblSTATE1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  gethide";// lblActive1s.Attributes["class"] = lblDefualt1s.Attributes["class"] =lblCUSERID1s.Attributes["class"] = lblENTRYDATE1s.Attributes["class"] = lblENTRYTIME1s.Attributes["class"] = lblUPDTTIME1s.Attributes["class"] = lblDeliveryAdressID1s.Attributes["class"] = lblAdressShortName11s.Attributes["class"] =lblContactID1s.Attributes["class"] = 
            lblContactMyID2h.Attributes["class"] = lblLatitute2h.Attributes["class"] = lblLongitute2h.Attributes["class"] = lblAdressName12h.Attributes["class"] = lblADDR12h.Attributes["class"] = lblADDR22h.Attributes["class"] = lblCITY2h.Attributes["class"] = lblSTATE2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  getshow";//lblActive2h.Attributes["class"] = lblDefualt2h.Attributes["class"] = lblCUSERID2h.Attributes["class"] = lblENTRYDATE2h.Attributes["class"] = lblENTRYTIME2h.Attributes["class"] = lblUPDTTIME2h.Attributes["class"] = lblDeliveryAdressID2h.Attributes["class"] = lblinfoWindow2h.Attributes["class"] = lblAdressShortName12h.Attributes["class"] =lblContactID2h.Attributes["class"] = 
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
            drpContactMyID.SelectedIndex = 0;
            txtAdressName1.Text = "";
            txtADDR1.Text = "";
            txtADDR2.Text = "";
            drpCity.SelectedIndex = 0;
            drpSTATE.SelectedIndex = 0;
            drpCOUNTRYID.SelectedIndex = 0;
            txtREMARKS.Text = "";
            txtBlock.Text = "";
            txtBuilding.Text = "";
            txtstreet.Text = "";
            txtlane.Text = "";
            txtFlat.Text = "";
            txtLongitute.Text = "48.0891287";
            txtLatitute.Text = "29.3462187";

            // searchInput123.Value = "HealthyBar, 102, Salmiya";
        }

        public void WriteNew()
        {
            //drpContactID.Enabled = true;
            drpContactMyID.Enabled = true;
            drpCOUNTRYID.Enabled = true;
            drpSTATE.Enabled = true;
            drpCity.Enabled = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";
            int uid = (((USER_MST)Session["USER"]).USER_ID);
            int TID = (((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (txtADDR2.Text == "" && txtinfoWindow.Text != "")
            {
                txtADDR2.Text = txtinfoWindow.Text;
            }
            if (btnAdd.Text == "Add New")
            {

                Write();
                WriteNew();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "s";
            }
            else if (btnAdd.Text == "Add")
            {
                int ContactMyID = Convert.ToInt32(drpContactMyID.SelectedValue);
                Database.TBLCONTACT_DEL_ADRES objTBLCONTACT_DEL_ADRES = new Database.TBLCONTACT_DEL_ADRES();
                //Server Content Send data Yogesh
                objTBLCONTACT_DEL_ADRES.TenentID = TID;
                objTBLCONTACT_DEL_ADRES.ContactMyID = Convert.ToInt32(drpContactMyID.SelectedValue);
                int delAddressID = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ContactMyID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ContactMyID).Max(p => p.DeliveryAdressID) + 1) : 1; //Convert.ToInt32(drpDeliveryAdressID.SelectedValue);
                objTBLCONTACT_DEL_ADRES.DeliveryAdressID = delAddressID;

                objTBLCONTACT_DEL_ADRES.GoogleName = txtinfoWindow.Text;//drpContactMyID.SelectedItem.ToString();
                //Latitute='29.346214' , Longitute='48.0913227
                if (txtLatitute.Text == "")
                    objTBLCONTACT_DEL_ADRES.Latitute = "29.346214";
                else
                    objTBLCONTACT_DEL_ADRES.Latitute = txtLatitute.Text;

                if (txtLongitute.Text == "")
                    objTBLCONTACT_DEL_ADRES.Longitute = "48.0913227";
                else
                    objTBLCONTACT_DEL_ADRES.Longitute = txtLongitute.Text;

                objTBLCONTACT_DEL_ADRES.ContactID = Convert.ToInt32(drpContactMyID.SelectedValue); //ContactMyID;                
                objTBLCONTACT_DEL_ADRES.AdressName1 = txtAdressName1.Text;
                objTBLCONTACT_DEL_ADRES.AdressShortName1 = drpCity.SelectedItem.ToString();
                objTBLCONTACT_DEL_ADRES.ADDR1 = txtADDR1.Text;
                objTBLCONTACT_DEL_ADRES.ADDR2 = txtADDR2.Text;
                objTBLCONTACT_DEL_ADRES.CITY = drpCity.SelectedValue;
                objTBLCONTACT_DEL_ADRES.STATE = (drpSTATE.SelectedValue).ToString();
                objTBLCONTACT_DEL_ADRES.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                objTBLCONTACT_DEL_ADRES.Block = txtBlock.Text;
                objTBLCONTACT_DEL_ADRES.Street = txtstreet.Text;
                objTBLCONTACT_DEL_ADRES.Lane = txtlane.Text;
                objTBLCONTACT_DEL_ADRES.Building = txtBuilding.Text;
                objTBLCONTACT_DEL_ADRES.ForFlat = txtFlat.Text;
                objTBLCONTACT_DEL_ADRES.REMARKS = txtREMARKS.Text;
                if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == uid).Count() > 0 && DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID != null).Count() > 0)
                    objTBLCONTACT_DEL_ADRES.CompID = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == uid).CompId;
                objTBLCONTACT_DEL_ADRES.Active = "Y";
                if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ContactMyID && p.Defualt == true).Count() > 0)
                {
                    objTBLCONTACT_DEL_ADRES.Defualt = false;
                }
                else
                {
                    objTBLCONTACT_DEL_ADRES.Defualt = true;
                }
                objTBLCONTACT_DEL_ADRES.UploadDate = DateTime.Now;
                objTBLCONTACT_DEL_ADRES.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                objTBLCONTACT_DEL_ADRES.SynID = 1;


                //int crup = GlobleClass.EncryptionHelpers.WriteLog("CONTACT_DEL_ADDRESS,INSERT:", "INSERT", "TBLCONTACT_DEL_ADRES", uid.ToString(), menu);
                //objTBLCONTACT_DEL_ADRES.CRUP_ID = crup;
                DB.TBLCONTACT_DEL_ADRES.AddObject(objTBLCONTACT_DEL_ADRES);
                DB.SaveChanges();

                //to map address checkin checkout
                //Database.EmployeeCheckIn CIN = new Database.EmployeeCheckIn();
                //CIN.TenentID = TID;
                //CIN.LocationID = 1;
                //CIN.EmployeeID = Convert.ToInt32(drpContactMyID.SelectedValue);
                //CIN.CheckInID = delAddressID;
                //CIN.GoogleName = txtinfoWindow.Text;
                //CIN.Latitute = txtLatitute.Text;
                //CIN.Longitute = txtLongitute.Text;
                //CIN.MeterDistAllowed = Convert.ToDecimal(0.0);
                //CIN.STATE = (drpSTATE.SelectedValue).ToString();
                //CIN.CITY = drpCity.SelectedValue;
                //CIN.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                //CIN.Block = txtBlock.Text;
                //CIN.Building = txtBlock.Text;
                //CIN.Street = txtstreet.Text;
                //CIN.Lane = txtlane.Text;
                //CIN.ForFlat = txtFlat.Text;
                //CIN.REMARKS = txtREMARKS.Text;
                //if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == uid).Count() > 0 && DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID != null).Count() > 0)
                //    CIN.CompID = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == uid).CompId;
                //CIN.Active = true;
                //CIN.UploadDate = DateTime.Now;
                //CIN.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                //DB.EmployeeCheckIns.AddObject(CIN);
                //DB.SaveChanges();

                ViewState["Add"] = objTBLCONTACT_DEL_ADRES.DeliveryAdressID;
                Clear();
                btnAdd.Text = "Add New";
                btnAdd.ValidationGroup = "submit";
                lblMsg.Text = "  Data Save Successfully";
                pnlSuccessMsg.Visible = true;
                BindData(ContactMyID);
                //navigation.Visible = true;
                Readonly();
                if (Request.QueryString["CID"] != null)
                {
                    BindAddon1List(ContactMyID);
                }
                //FirstData();

            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["Edit"] != null && ViewState["Addid"] != null)
                {
                    int ContactMyID = Convert.ToInt32(drpContactMyID.SelectedValue);
                    int CID = Convert.ToInt32(ViewState["Edit"]);
                    int ADDID = Convert.ToInt32(ViewState["Addid"]);
                    Database.TBLCONTACT_DEL_ADRES objTBLCONTACT_DEL_ADRES = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == ADDID);
                    objTBLCONTACT_DEL_ADRES.ContactMyID = Convert.ToInt32(drpContactMyID.SelectedValue);
                    //objTBLCONTACT_DEL_ADRES.DeliveryAdressID = Convert.ToInt32(drpDeliveryAdressID.SelectedValue);  
                    //Latitute='29.346214' , Longitute='48.0913227
                    if (txtLatitute.Text == "")
                        objTBLCONTACT_DEL_ADRES.Latitute = "29.346214";
                    else
                        objTBLCONTACT_DEL_ADRES.Latitute = txtLatitute.Text;

                    if (txtLongitute.Text == "")
                        objTBLCONTACT_DEL_ADRES.Longitute = "48.0913227";
                    else
                        objTBLCONTACT_DEL_ADRES.Longitute = txtLongitute.Text;

                    objTBLCONTACT_DEL_ADRES.ContactID = Convert.ToInt32(drpContactMyID.SelectedValue);
                    objTBLCONTACT_DEL_ADRES.AdressName1 = txtAdressName1.Text;
                    objTBLCONTACT_DEL_ADRES.GoogleName = txtinfoWindow.Text;
                    objTBLCONTACT_DEL_ADRES.ADDR1 = txtADDR1.Text;
                    objTBLCONTACT_DEL_ADRES.ADDR2 = txtADDR2.Text;
                    objTBLCONTACT_DEL_ADRES.CITY = drpCity.SelectedValue;
                    objTBLCONTACT_DEL_ADRES.STATE = (drpSTATE.SelectedValue).ToString();
                    objTBLCONTACT_DEL_ADRES.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                    objTBLCONTACT_DEL_ADRES.Block = txtBlock.Text;
                    objTBLCONTACT_DEL_ADRES.Street = txtstreet.Text;
                    objTBLCONTACT_DEL_ADRES.Lane = txtlane.Text;
                    objTBLCONTACT_DEL_ADRES.Building = txtBuilding.Text;
                    objTBLCONTACT_DEL_ADRES.ForFlat = txtFlat.Text;
                    objTBLCONTACT_DEL_ADRES.REMARKS = txtREMARKS.Text;

                    objTBLCONTACT_DEL_ADRES.UploadDate = DateTime.Now;
                    objTBLCONTACT_DEL_ADRES.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objTBLCONTACT_DEL_ADRES.SynID = 2;
                    //int crupID = Convert.ToInt32(objTBLCONTACT_DEL_ADRES.CRUP_ID);

                    DB.SaveChanges();
                    //GlobleClass.EncryptionHelpers.UpdateLog("CONTACT_DEL_ADDRESS,Update:", crupID, "TBLCONTACT_DEL_ADRES", uid.ToString());

                    //to map address checkin checkout
                    //Database.EmployeeCheckIn CIN = DB.EmployeeCheckIns.Single(p => p.TenentID == TID && p.EmployeeID == CID && p.CheckInID == ADDID);                                      
                    //CIN.GoogleName = txtinfoWindow.Text;
                    //CIN.Latitute = txtLatitute.Text;
                    //CIN.Longitute = txtLongitute.Text;
                    //CIN.MeterDistAllowed = Convert.ToDecimal(0.0);
                    //CIN.STATE = (drpSTATE.SelectedValue).ToString();
                    //CIN.CITY = drpCity.SelectedValue;
                    //CIN.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                    //CIN.Block = txtBlock.Text;
                    //CIN.Building = txtBlock.Text;
                    //CIN.Street = txtstreet.Text;
                    //CIN.Lane = txtlane.Text;
                    //CIN.ForFlat = txtFlat.Text;
                    //CIN.REMARKS = txtREMARKS.Text;
                    //if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == uid).Count() > 0 && DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID != null).Count() > 0)
                    //    CIN.CompID = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == uid).CompId;
                    //CIN.Active = true;
                    //CIN.UploadDate = DateTime.Now;
                    //CIN.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    //DB.EmployeeCheckIns.AddObject(CIN);
                    //DB.SaveChanges();

                    ViewState["Edit"] = null;
                    ViewState["Addid"] = ADDID;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "submit";
                    Clear();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData(CID);
                    //navigation.Visible = true;
                    Readonly();
                    if (Request.QueryString["CID"] != null)
                    {
                        BindAddon1List(ContactMyID);
                    }
                    //FirstData();
                }

            }
            //BindData();

            //        scope.Complete(); //  To commit.

            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
            GetMap(0, 0, "", "");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
        }

        public string Name(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.tblCOUNTRies.Where(p => p.COUNTRYID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.tblCOUNTRies.Single(p => p.COUNTRYID == ID && p.TenentID == TID).COUNAME1;
            }
            else
            {
                return "Not Found";
            }
        }

        public string nameCITY(int ID, int State)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.tblCityStatesCounties.Where(p => p.CityID == ID && p.StateID == State).Count() > 0)
            {
                return DB.tblCityStatesCounties.FirstOrDefault(p => p.CityID == ID && p.StateID == State).CityEnglish;
            }
            else
            {
                return "Not Found";
            }
        }

        public string GetState(int State, int COUNTRYID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.tblStates.Where(p => p.StateID == State && p.COUNTRYID == COUNTRYID).Count() > 0)
            {
                return DB.tblStates.FirstOrDefault(p => p.StateID == State && p.COUNTRYID == COUNTRYID).MYNAME1;
            }
            else
            {
                return "Not Found";
            }
        }

        public string Name1(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == ID && p.TenentID == TID).Count() > 0)
            {
                return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID).COMPNAME1;
            }
            else
            {
                return "Not Found";
            }
        }
        public void FillContractorID()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            if (TID == 1)
            {
                List<Database.TBLCOMPANYSETUP> ListCustomer = new List<Database.TBLCOMPANYSETUP>();
                List<Database.tblcontact_addon1> FinelCustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active == true).ToList();

                foreach (Database.tblcontact_addon1 Citem in FinelCustomer)
                {
                    if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == Citem.CustomerId && p.active == true).Count() > 0)
                    {
                        Database.TBLCOMPANYSETUP Cobj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == Citem.CustomerId);
                        ListCustomer.Add(Cobj);
                    }
                }
                drpContactMyID.DataSource = ListCustomer;
                drpContactMyID.DataTextField = "COMPNAME1";
                drpContactMyID.DataValueField = "COMPID";
                drpContactMyID.DataBind();
                drpContactMyID.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else if (TID == 9000007)
            {
                drpContactMyID.DataSource = DB.TBLCONTACTs.Where(p => p.TenentID == TID);
                drpContactMyID.DataTextField = "PersName1";
                drpContactMyID.DataValueField = "ContactMyID";
                drpContactMyID.DataBind();
                drpContactMyID.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                drpContactMyID.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);
                drpContactMyID.DataTextField = "COMPNAME1";
                drpContactMyID.DataValueField = "COMPID";
                drpContactMyID.DataBind();
                drpContactMyID.Items.Insert(0, new ListItem("-- Select --", "0"));
            }



            //drpContactID.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);//.Take(500)
            //drpContactID.DataTextField = "COMPNAME1";
            //drpContactID.DataValueField = "COMPID";
            //drpContactID.DataBind();
            //drpContactID.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpCOUNTRYID.DataSource = DB.tblCOUNTRies.Where(p => p.Active == "Y" && p.TenentID == TID);
            drpCOUNTRYID.DataTextField = "COUNAME1";
            drpCOUNTRYID.DataValueField = "COUNTRYID";
            drpCOUNTRYID.DataBind();
            drpCOUNTRYID.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpSTATE.DataSource = DB.tblStates;
            drpSTATE.DataTextField = "MYNAME1";
            drpSTATE.DataValueField = "StateID";
            drpSTATE.DataBind();
            drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));


            drpCity.DataSource = DB.tblCityStatesCounties;
            drpCity.DataTextField = "CityEnglish";
            drpCity.DataValueField = "CityID";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));


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
        public void ManageData()
        {
            if (Listview1.Items.Count > 0)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                ViewState["tempAID"] = Listview1.SelectedDataKey[2].ToString();
                if (Listview1.SelectedDataKey[1] == null)
                {

                }
                else
                {
                    int CID1 = Convert.ToInt32(Listview1.SelectedDataKey[1]);
                    if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID1).Count() > 0)
                    {
                        try
                        {
                            string CONID = Listview1.SelectedDataKey[1].ToString();
                            drpContactMyID.SelectedValue = CONID;
                        }
                        catch
                        {

                        }

                    }
                }
                if (Listview1.SelectedDataKey[4] != null)
                    txtLatitute.Text = Listview1.SelectedDataKey[4].ToString();
                if (Listview1.SelectedDataKey[5] != null)
                    txtLongitute.Text = Listview1.SelectedDataKey[5].ToString();
                if (Listview1.SelectedDataKey[8] != null)
                    txtAdressName1.Text = Listview1.SelectedDataKey[8].ToString();
                if (Listview1.SelectedDataKey[9] != null)
                    txtADDR1.Text = Listview1.SelectedDataKey[9].ToString();
                if (Listview1.SelectedDataKey[10] != null)
                    txtADDR2.Text = Listview1.SelectedDataKey[10].ToString();
                if (Listview1.SelectedDataKey[11] == null)
                { }
                else
                {
                    drpCity.SelectedValue = Listview1.SelectedDataKey[11].ToString();
                }

                if (Listview1.SelectedDataKey[12] == null)
                { }
                else
                {
                    drpSTATE.SelectedValue = Listview1.SelectedDataKey[12].ToString();
                }

                if (Listview1.SelectedDataKey[13] == null)
                { }
                else
                {
                    drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[13].ToString();
                }
                if (Listview1.SelectedDataKey[14] != null)
                    txtBlock.Text = Listview1.SelectedDataKey[14].ToString();
                if (Listview1.SelectedDataKey[15] != null)
                    txtBuilding.Text = Listview1.SelectedDataKey[15].ToString();
                if (Listview1.SelectedDataKey[16] != null)
                    txtstreet.Text = Listview1.SelectedDataKey[16].ToString();
                if (Listview1.SelectedDataKey[17] != null)
                    txtlane.Text = Listview1.SelectedDataKey[17].ToString();
                if (Listview1.SelectedDataKey[18] != null)
                    txtFlat.Text = Listview1.SelectedDataKey[18].ToString();
                if (Listview1.SelectedDataKey[19] != null)
                    txtREMARKS.Text = Listview1.SelectedDataKey[19].ToString();
            }
            else
            {
                txtLatitute.Text = "29.3462187";
                txtLongitute.Text = "48.0891287";
                // searchInput123.Value = "HealthyBar, 102, Salmiya";
            }


        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            ManageData();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                ManageData();
            }

        }
        public void PrevData()
        {
            //if (Listview1.SelectedIndex == 0)
            //{
            //    lblMsg.Text = "This is first record";
            //    pnlSuccessMsg.Visible = true;
            //}
            //else
            //{
            //    pnlSuccessMsg.Visible = false;
            //    Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
            //    ManageData();
            //}
        }
        public void LastData()
        {
            //Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //ManageData();
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblContactMyID2h.Visible = lblLatitute2h.Visible = lblLongitute2h.Visible = lblAdressName12h.Visible = lblADDR12h.Visible = lblADDR22h.Visible = lblCITY2h.Visible = lblSTATE2h.Visible = lblCOUNTRYID2h.Visible = lblREMARKS2h.Visible = false;//lblActive2h.Visible = lblDefualt2h.Visible = lblCUSERID2h.Visible = lblENTRYDATE2h.Visible = lblENTRYTIME2h.Visible = lblUPDTTIME2h.Visible = lblDeliveryAdressID2h.Visible  = lblAdressShortName12h.Visible = lblContactID2h.Visible = 
                    //2true
                    txtContactMyID2h.Visible = txtLatitute2h.Visible = txtLongitute2h.Visible = txtAdressName12h.Visible = txtADDR12h.Visible = txtADDR22h.Visible = txtCITY2h.Visible = txtSTATE2h.Visible = txtCOUNTRYID2h.Visible = txtREMARKS2h.Visible = true;//txtActive2h.Visible = txtDefualt2h.Visible = txtCUSERID2h.Visible = txtENTRYDATE2h.Visible = txtENTRYTIME2h.Visible = txtUPDTTIME2h.Visible =txtDeliveryAdressID2h.Visible =txtinfoWindow2h.Visible = txtAdressShortName12h.Visible =  txtContactID2h.Visible =

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
                    lblContactMyID2h.Visible = lblLatitute2h.Visible = lblLongitute2h.Visible = lblAdressName12h.Visible = lblADDR12h.Visible = lblADDR22h.Visible = lblCITY2h.Visible = lblSTATE2h.Visible = lblCOUNTRYID2h.Visible = lblREMARKS2h.Visible = true;//lblActive2h.Visible = lblDefualt2h.Visible =lblCUSERID2h.Visible = lblENTRYDATE2h.Visible = lblENTRYTIME2h.Visible = lblUPDTTIME2h.Visible =lblDeliveryAdressID2h.Visible  = lblAdressShortName12h.Visible = lblContactID2h.Visible =
                    //2false
                    txtContactMyID2h.Visible = txtLatitute2h.Visible = txtLongitute2h.Visible = txtAdressName12h.Visible = txtADDR12h.Visible = txtADDR22h.Visible = txtCITY2h.Visible = txtSTATE2h.Visible = txtCOUNTRYID2h.Visible = txtREMARKS2h.Visible = false;//txtActive2h.Visible = txtDefualt2h.Visible = txtCUSERID2h.Visible = txtENTRYDATE2h.Visible = txtENTRYTIME2h.Visible = txtUPDTTIME2h.Visible = txtDeliveryAdressID2h.Visible =txtinfoWindow2h.Visible =  txtAdressShortName12h.Visible =txtContactID2h.Visible = 

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
                    lblContactMyID1s.Visible = lblLatitute1s.Visible = lblLongitute1s.Visible = lblAdressName11s.Visible = lblADDR11s.Visible = lblADDR21s.Visible = lblCITY1s.Visible = lblSTATE1s.Visible = lblCOUNTRYID1s.Visible = lblREMARKS1s.Visible = false;//lblActive1s.Visible = lblDefualt1s.Visible = lblCUSERID1s.Visible = lblENTRYDATE1s.Visible = lblENTRYTIME1s.Visible = lblUPDTTIME1s.Visible =lblDeliveryAdressID1s.Visible = lblAdressShortName11s.Visible =lblContactID1s.Visible = 
                    //1true
                    txtContactMyID1s.Visible = txtLatitute1s.Visible = txtLongitute1s.Visible = txtAdressName11s.Visible = txtADDR11s.Visible = txtADDR21s.Visible = txtCITY1s.Visible = txtSTATE1s.Visible = txtCOUNTRYID1s.Visible = txtREMARKS1s.Visible = true;//txtActive1s.Visible = txtDefualt1s.Visible =txtCUSERID1s.Visible = txtENTRYDATE1s.Visible = txtENTRYTIME1s.Visible = txtUPDTTIME1s.Visible =txtDeliveryAdressID1s.Visible = txtinfoWindow1s.Visible = txtAdressShortName11s.Visible =txtContactID1s.Visible =
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
                    lblContactMyID1s.Visible = lblLatitute1s.Visible = lblLongitute1s.Visible = lblAdressName11s.Visible = lblADDR11s.Visible = lblADDR21s.Visible = lblCITY1s.Visible = lblSTATE1s.Visible = lblCOUNTRYID1s.Visible = lblREMARKS1s.Visible = true;//lblActive1s.Visible = lblDefualt1s.Visible =lblCUSERID1s.Visible = lblENTRYDATE1s.Visible = lblENTRYTIME1s.Visible = lblUPDTTIME1s.Visible =  lblDeliveryAdressID1s.Visible  = lblAdressShortName11s.Visible =lblContactID1s.Visible =
                    //1false
                    txtContactMyID1s.Visible = txtLatitute1s.Visible = txtLongitute1s.Visible = txtAdressName11s.Visible = txtADDR11s.Visible = txtADDR21s.Visible = txtCITY1s.Visible = txtSTATE1s.Visible = txtCOUNTRYID1s.Visible = txtREMARKS1s.Visible = false;//txtActive1s.Visible = txtDefualt1s.Visible = txtCUSERID1s.Visible = txtENTRYDATE1s.Visible = txtENTRYTIME1s.Visible = txtUPDTTIME1s.Visible =  txtDeliveryAdressID1s.Visible =  txtAdressShortName11s.Visible = txtinfoWindow1s.Visible =txtContactID1s.Visible =
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCONTACT_DEL_ADRES").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblContactMyID1s.ID == item.LabelID)
                    txtContactMyID1s.Text = lblContactMyID1s.Text = item.LabelName;
                //else if (lblDeliveryAdressID1s.ID == item.LabelID)
                //    txtDeliveryAdressID1s.Text = lblDeliveryAdressID1s.Text =  item.LabelName;               
                else if (lblLatitute1s.ID == item.LabelID)
                    txtLatitute1s.Text = lblLatitute1s.Text = item.LabelName;
                else if (lblLongitute1s.ID == item.LabelID)
                    txtLongitute1s.Text = lblLongitute1s.Text = item.LabelName;
                //else if (lblContactID1s.ID == item.LabelID)
                //    txtContactID1s.Text = lblContactID1s.Text = item.LabelName;
                //else if (lblAdressShortName11s.ID == item.LabelID)
                //    txtAdressShortName11s.Text = lblAdressShortName11s.Text = item.LabelName;
                else if (lblAdressName11s.ID == item.LabelID)
                    txtAdressName11s.Text = lblAdressName11s.Text = item.LabelName;
                else if (lblADDR11s.ID == item.LabelID)
                    txtADDR11s.Text = lblADDR11s.Text = item.LabelName;
                else if (lblADDR21s.ID == item.LabelID)
                    txtADDR21s.Text = lblADDR21s.Text = item.LabelName;
                else if (lblCITY1s.ID == item.LabelID)
                    txtCITY1s.Text = lblCITY1s.Text = item.LabelName;
                else if (lblSTATE1s.ID == item.LabelID)
                    txtSTATE1s.Text = lblSTATE1s.Text = item.LabelName;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;
                //else if (lblCUSERID1s.ID == item.LabelID)
                //    txtCUSERID1s.Text = lblCUSERID1s.Text = item.LabelName;
                //else if (lblENTRYDATE1s.ID == item.LabelID)
                //    txtENTRYDATE1s.Text = lblENTRYDATE1s.Text =  item.LabelName;
                //else if (lblENTRYTIME1s.ID == item.LabelID)
                //    txtENTRYTIME1s.Text = lblENTRYTIME1s.Text = item.LabelName;
                //else if (lblUPDTTIME1s.ID == item.LabelID)
                //    txtUPDTTIME1s.Text = lblUPDTTIME1s.Text =  item.LabelName;
                //else if (lblActive1s.ID == item.LabelID)
                //    txtActive1s.Text = lblActive1s.Text =  item.LabelName;
                //else if (lblDefualt1s.ID == item.LabelID)
                //    txtDefualt1s.Text = lblDefualt1s.Text =item.LabelName;

                else if (lblContactMyID2h.ID == item.LabelID)
                    txtContactMyID2h.Text = lblContactMyID2h.Text = item.LabelName;
                //else if (lblDeliveryAdressID2h.ID == item.LabelID)
                //    txtDeliveryAdressID2h.Text = lblDeliveryAdressID2h.Text = item.LabelName;               
                else if (lblLatitute2h.ID == item.LabelID)
                    txtLatitute2h.Text = lblLatitute2h.Text = item.LabelName;
                else if (lblLongitute2h.ID == item.LabelID)
                    txtLongitute2h.Text = lblLongitute2h.Text = item.LabelName;
                //else if (lblContactID2h.ID == item.LabelID)
                //    txtContactID2h.Text = lblContactID2h.Text = item.LabelName;
                //else if (lblAdressShortName12h.ID == item.LabelID)
                //    txtAdressShortName12h.Text = lblAdressShortName12h.Text = item.LabelName;
                else if (lblAdressName12h.ID == item.LabelID)
                    txtAdressName12h.Text = lblAdressName12h.Text = item.LabelName;
                else if (lblADDR12h.ID == item.LabelID)
                    txtADDR12h.Text = lblADDR12h.Text = item.LabelName;
                else if (lblADDR22h.ID == item.LabelID)
                    txtADDR22h.Text = lblADDR22h.Text = item.LabelName;
                else if (lblCITY2h.ID == item.LabelID)
                    txtCITY2h.Text = lblCITY2h.Text = item.LabelName;
                else if (lblSTATE2h.ID == item.LabelID)
                    txtSTATE2h.Text = lblSTATE2h.Text = item.LabelName;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = item.LabelName;
                //else if (lblCUSERID2h.ID == item.LabelID)
                //    txtCUSERID2h.Text = lblCUSERID2h.Text =  item.LabelName;
                //else if (lblENTRYDATE2h.ID == item.LabelID)
                //    txtENTRYDATE2h.Text = lblENTRYDATE2h.Text = item.LabelName;
                //else if (lblENTRYTIME2h.ID == item.LabelID)
                //    txtENTRYTIME2h.Text = lblENTRYTIME2h.Text =  item.LabelName;
                //else if (lblUPDTTIME2h.ID == item.LabelID)
                //    txtUPDTTIME2h.Text = lblUPDTTIME2h.Text = item.LabelName;
                //else if (lblActive2h.ID == item.LabelID)
                //    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblDefualt2h.ID == item.LabelID)
                //    txtDefualt2h.Text = lblDefualt2h.Text = item.LabelName;
                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCONTACT_DEL_ADRES").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLCONTACT_DEL_ADRES.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLCONTACT_DEL_ADRES").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblContactMyID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContactMyID1s.Text;
                //else if (lblDeliveryAdressID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryAdressID1s.Text;                
                else if (lblLatitute1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLatitute1s.Text;
                else if (lblLongitute1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLongitute1s.Text;
                //else if (lblContactID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtContactID1s.Text;
                //else if (lblAdressShortName11s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtAdressShortName11s.Text;
                else if (lblAdressName11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAdressName11s.Text;
                else if (lblADDR11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDR11s.Text;
                else if (lblADDR21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDR21s.Text;
                else if (lblCITY1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCITY1s.Text;
                else if (lblSTATE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSTATE1s.Text;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;
                //else if (lblCUSERID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCUSERID1s.Text;
                //else if (lblENTRYDATE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYDATE1s.Text;
                //else if (lblENTRYTIME1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYTIME1s.Text;
                //else if (lblUPDTTIME1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUPDTTIME1s.Text;
                //else if (lblActive1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblDefualt1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDefualt1s.Text;

                else if (lblContactMyID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContactMyID2h.Text;
                //else if (lblDeliveryAdressID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryAdressID2h.Text;               
                else if (lblLatitute2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLatitute2h.Text;
                else if (lblLongitute2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLongitute2h.Text;
                //else if (lblContactID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtContactID2h.Text;
                //else if (lblAdressShortName12h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtAdressShortName12h.Text;
                else if (lblAdressName12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAdressName12h.Text;
                else if (lblADDR12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDR12h.Text;
                else if (lblADDR22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDR22h.Text;
                else if (lblCITY2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCITY2h.Text;
                else if (lblSTATE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSTATE2h.Text;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;
                //else if (lblCUSERID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCUSERID2h.Text;
                //else if (lblENTRYDATE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYDATE2h.Text;
                //else if (lblENTRYTIME2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYTIME2h.Text;
                //else if (lblUPDTTIME2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUPDTTIME2h.Text;
                //else if (lblActive2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblDefualt2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDefualt2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLCONTACT_DEL_ADRES.xml"));

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
        public void Write()
        {
            //navigation.Visible = false;
            drpContactMyID.Enabled = true;
            drpCOUNTRYID.Enabled = true;
            drpSTATE.Enabled = true;
            drpCity.Enabled = true;
            txtAdressName1.Enabled = true;
            txtADDR1.Enabled = true;
            //txtADDR2.Enabled = true;
            txtBlock.Enabled = true;
            txtLatitute.Enabled = true;
            txtLongitute.Enabled = true;
            txtBuilding.Enabled = true;
            txtstreet.Enabled = true;
            txtlane.Enabled = true;
            txtFlat.Enabled = true;
            txtREMARKS.Enabled = true;
        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drpContactMyID.Enabled = false;
            drpCOUNTRYID.Enabled = false;
            drpSTATE.Enabled = false;
            drpCity.Enabled = false;
            txtAdressName1.Enabled = false;
            txtADDR1.Enabled = false;
            txtADDR2.Enabled = false;
            txtBlock.Enabled = false;
            txtLatitute.Enabled = false;
            txtLongitute.Enabled = false;
            txtBuilding.Enabled = false;
            txtstreet.Enabled = false;
            txtlane.Enabled = false;
            txtFlat.Enabled = false;
            txtREMARKS.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLCONTACT_DEL_ADRES.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID).OrderBy(m => m.ContactID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    if (take == Totalrec && Skip == (Totalrec - Showdata))
        //        btnNext1.Enabled = false;
        //    else
        //        btnNext1.Enabled = true;
        //    if (take == Showdata && Skip == 0)
        //        btnPrevious1.Enabled = false;
        //    else
        //        btnPrevious1.Enabled = true;

        //    ChoiceID = take / Showdata;

        //    //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.TBLCONTACT_DEL_ADRES.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID).OrderBy(m => m.ContactID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        if (take == Showdata && Skip == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;

        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;

        //        ChoiceID = take / Showdata;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.TBLCONTACT_DEL_ADRES.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID).OrderBy(m => m.ContactID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLCONTACT_DEL_ADRES.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID).OrderBy(m => m.ContactID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    //((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CID"] != null)
            {
                int MYid = Convert.ToInt32(Request.QueryString["CID"]);
                BindData(MYid);
            }
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
            if (Request.QueryString["CID"] != null)
            {
                int MYid = Convert.ToInt32(Request.QueryString["CID"]);
                BindData(MYid);
            }
            //FirstData();
        }

        public void bindstatecity(int contyid, int State)
        {
            drpSTATE.DataSource = DB.tblStates.Where(p => p.COUNTRYID == contyid);
            drpSTATE.DataTextField = "MYNAME1";
            drpSTATE.DataValueField = "StateID";
            drpSTATE.DataBind();
            drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));


            drpCity.DataSource = DB.tblCityStatesCounties;
            drpCity.DataTextField = "CityEnglish";
            drpCity.DataValueField = "CityID";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";



            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (e.CommandName == "btnDelete")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int CID = Convert.ToInt32(ID[0]);
                int AddID = Convert.ToInt32(ID[1]);
                //int ID = Convert.ToInt32(e.CommandArgument);
                Database.TBLCONTACT_DEL_ADRES objTBLCONTACT_DEL_ADRES = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AddID);
                objTBLCONTACT_DEL_ADRES.Active = "N";
                objTBLCONTACT_DEL_ADRES.UploadDate = DateTime.Now;
                objTBLCONTACT_DEL_ADRES.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                objTBLCONTACT_DEL_ADRES.SynID = 1;

                DB.SaveChanges();
                BindData(CID);
            }

            if (e.CommandName == "btnEdit")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                int CID = Convert.ToInt32(ID[0]);
                int AddID = Convert.ToInt32(ID[1]);
                //int ID = Convert.ToInt32(e.CommandArgument);
                Database.TBLCONTACT_DEL_ADRES objTBLCONTACT_DEL_ADRES = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == CID && p.DeliveryAdressID == AddID && p.Active == "Y");

                int COUNTRYID = Convert.ToInt32(objTBLCONTACT_DEL_ADRES.COUNTRYID);
                int State = Convert.ToInt32(objTBLCONTACT_DEL_ADRES.STATE);
                bindstatecity(COUNTRYID, State);
                if (objTBLCONTACT_DEL_ADRES.ContactMyID != null && objTBLCONTACT_DEL_ADRES.ContactMyID != 0)
                {
                    drpContactMyID.SelectedValue = objTBLCONTACT_DEL_ADRES.ContactMyID.ToString();
                }

                //drpDeliveryAdressID.SelectedValue = objTBLCONTACT_DEL_ADRES.DeliveryAdressID.ToString();
                txtinfoWindow.Text = objTBLCONTACT_DEL_ADRES.GoogleName != null && objTBLCONTACT_DEL_ADRES.GoogleName != "" ? objTBLCONTACT_DEL_ADRES.GoogleName.ToString() : "";
                txtLatitute.Text = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? objTBLCONTACT_DEL_ADRES.Latitute.ToString() : "29.3462187";
                txtLongitute.Text = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? objTBLCONTACT_DEL_ADRES.Longitute.ToString() : "48.0891287";
                // searchInput123.Value = "HealthyBar, 102, Salmiya";

                if (objTBLCONTACT_DEL_ADRES.Latitute == null || objTBLCONTACT_DEL_ADRES.Latitute == "")
                {
                    objTBLCONTACT_DEL_ADRES.Latitute = "29.3462187";
                }
                if (objTBLCONTACT_DEL_ADRES.Longitute == null || objTBLCONTACT_DEL_ADRES.Longitute == "")
                {
                    objTBLCONTACT_DEL_ADRES.Longitute = "48.0891287";
                }
                if (objTBLCONTACT_DEL_ADRES.GoogleName == null || objTBLCONTACT_DEL_ADRES.GoogleName == "")
                {
                    objTBLCONTACT_DEL_ADRES.GoogleName = "HealthyBar, 102, Salmiya";
                }

                txtBlock.Text = objTBLCONTACT_DEL_ADRES.Block != null && objTBLCONTACT_DEL_ADRES.Block != "" ? objTBLCONTACT_DEL_ADRES.Block.ToString() : "";
                txtstreet.Text = objTBLCONTACT_DEL_ADRES.Street != null && objTBLCONTACT_DEL_ADRES.Street != "" ? objTBLCONTACT_DEL_ADRES.Street.ToString() : "";
                txtlane.Text = objTBLCONTACT_DEL_ADRES.Lane != null && objTBLCONTACT_DEL_ADRES.Lane != "" ? objTBLCONTACT_DEL_ADRES.Lane.ToString() : "";
                txtBuilding.Text = objTBLCONTACT_DEL_ADRES.Building != null && objTBLCONTACT_DEL_ADRES.Building != "" ? objTBLCONTACT_DEL_ADRES.Building.ToString() : "";
                txtFlat.Text = objTBLCONTACT_DEL_ADRES.ForFlat != null && objTBLCONTACT_DEL_ADRES.ForFlat != "" ? objTBLCONTACT_DEL_ADRES.ForFlat.ToString() : "";

                //if (objTBLCONTACT_DEL_ADRES.ContactID != null && objTBLCONTACT_DEL_ADRES.ContactID != 0)
                //{
                //    drpContactID.SelectedValue = objTBLCONTACT_DEL_ADRES.ContactID.ToString();
                //}

                //txtAdressShortName1.Text = objTBLCONTACT_DEL_ADRES.AdressShortName1 != null && objTBLCONTACT_DEL_ADRES.AdressShortName1 != "" ? objTBLCONTACT_DEL_ADRES.AdressShortName1.ToString() : "";
                txtAdressName1.Text = objTBLCONTACT_DEL_ADRES.AdressName1 != null && objTBLCONTACT_DEL_ADRES.AdressName1 != "" ? objTBLCONTACT_DEL_ADRES.AdressName1.ToString() : "";
                txtADDR1.Text = objTBLCONTACT_DEL_ADRES.ADDR1 != null && objTBLCONTACT_DEL_ADRES.ADDR1 != "" ? objTBLCONTACT_DEL_ADRES.ADDR1.ToString() : "";
                txtADDR2.Text = objTBLCONTACT_DEL_ADRES.ADDR2 != null && objTBLCONTACT_DEL_ADRES.ADDR2 != "" ? objTBLCONTACT_DEL_ADRES.ADDR2.ToString() : "";

                if (objTBLCONTACT_DEL_ADRES.CITY != null && objTBLCONTACT_DEL_ADRES.CITY != "" && objTBLCONTACT_DEL_ADRES.CITY != "0")
                {
                    drpCity.SelectedValue = objTBLCONTACT_DEL_ADRES.CITY.ToString();
                }
                if (objTBLCONTACT_DEL_ADRES.STATE != null && objTBLCONTACT_DEL_ADRES.STATE != "" && objTBLCONTACT_DEL_ADRES.STATE != "0")
                {
                    drpSTATE.SelectedValue = objTBLCONTACT_DEL_ADRES.STATE.ToString();
                }
                if (objTBLCONTACT_DEL_ADRES.COUNTRYID != null && objTBLCONTACT_DEL_ADRES.COUNTRYID != 0)
                {
                    drpCOUNTRYID.SelectedValue = objTBLCONTACT_DEL_ADRES.COUNTRYID.ToString();
                }
                double lat = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Latitute) : 29.346214;
                double lng = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Longitute) : 48.0913227;
                string add1 = objTBLCONTACT_DEL_ADRES.ADDR2.ToString();
                string google = objTBLCONTACT_DEL_ADRES.GoogleName.ToString();
                if (lat != null && lng != null && lat != 0 && lng != 0)
                {
                    GetMap(lat, lng, add1, google);
                }
                else
                {
                    GetMap(0, 0, "", "");
                }
                txtREMARKS.Text = objTBLCONTACT_DEL_ADRES.REMARKS != null && objTBLCONTACT_DEL_ADRES.REMARKS != "" ? objTBLCONTACT_DEL_ADRES.REMARKS.ToString() : "";
                //txtCRUP_ID.Text = objTBLCONTACT_DEL_ADRES.CRUP_ID.ToString();
                //txtCUSERID.Text = objTBLCONTACT_DEL_ADRES.CUSERID.ToString();
                //txtENTRYDATE.Text = objTBLCONTACT_DEL_ADRES.ENTRYDATE.ToString();
                //txtENTRYTIME.Text = objTBLCONTACT_DEL_ADRES.ENTRYTIME.ToString();
                //txtUPDTTIME.Text = objTBLCONTACT_DEL_ADRES.UPDTTIME.ToString();
                //cbActive.Text = objTBLCONTACT_DEL_ADRES.Active.ToString();
                //cbDefualt.Checked = (objTBLCONTACT_DEL_ADRES.Defualt == true) ? true : false;


                DB.SaveChanges();
                btnAdd.Text = "Update";
                ViewState["Edit"] = CID;
                ViewState["Addid"] = AddID;
                Write();
            }
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
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLCONTACT_DEL_ADRES.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID).OrderBy(m => m.ContactID).Take(Tvalue).Skip(Svalue)).ToList());
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
        //protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["CID"] != null)
        //    {
        //        int MYid = Convert.ToInt32(Request.QueryString["CID"]);
        //        BindData(MYid);
        //    }

        //}
        //protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
        //    ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
        //    control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        //}
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //int MYid = Convert.ToInt32(drpContactID.SelectedValue);
            int MYid = Convert.ToInt32(drpContactMyID.SelectedValue);
            int Add = Convert.ToInt32(ViewState["Add"]);
            string PageMode = ViewState["PageModeCID"].ToString();
            if (TID == 9000007)
            {
                Response.Redirect("../People/ApostContactMaster.aspx");
            }

            Response.Redirect("tblcontact_addon1.aspx?CID=" + MYid + "&ADD=" + Add + "&PageMode=" + PageMode);


        }

        protected void drpCOUNTRYID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);

            int CID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
            drpSTATE.DataSource = DB.tblStates.Where(p => p.ACTIVE1 == "Y" && p.COUNTRYID == CID);
            drpSTATE.DataTextField = "MYNAME1";
            drpSTATE.DataValueField = "StateID";
            drpSTATE.DataBind();
            drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));

            drpCity.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == CID);
            drpCity.DataTextField = "CityEnglish";
            drpCity.DataValueField = "CityID";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void drpSTATE_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);

            int SID = Convert.ToInt32(drpSTATE.SelectedValue);

            drpCity.DataSource = DB.tblCityStatesCounties.Where(p => p.StateID == SID);
            drpCity.DataTextField = "CityEnglish";
            drpCity.DataValueField = "CityID";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void drpCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Country = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
            int City = Convert.ToInt32(drpCity.SelectedValue);
            drpSTATE.DataSource = DB.tblStates.Where(p => p.COUNTRYID == Country);
            drpSTATE.DataTextField = "MYNAME1";
            drpSTATE.DataValueField = "StateID";
            drpSTATE.DataBind();
            drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));
            if (Country != 0)
            {
                string state = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == Country && p.CityID == City).FirstOrDefault().StateID.ToString();
                drpSTATE.SelectedValue = state;
            }
            //txtAdressName1.Text = drpCity.SelectedItem.ToString();
        }

        protected void drpContactMyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCompContactData();
        }
        public void GetCompContactData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int ContactID = Convert.ToInt32(drpContactMyID.SelectedValue);
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ContactID).Count() > 0)
            {
                Database.TBLCOMPANYSETUP OBJ = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ContactID);
                txtADDR1.Text = "";
                txtADDR1.Text = OBJ.ADDR1 != null ? OBJ.ADDR1 : "";
                txtADDR2.Text = OBJ.ADDR2 != null ? OBJ.ADDR2 : "";

                int COUNTRYID = Convert.ToInt32(OBJ.COUNTRYID);
                drpCOUNTRYID.SelectedValue = OBJ.COUNTRYID.ToString();
                drpSTATE.DataSource = DB.tblStates.Where(p => p.COUNTRYID == COUNTRYID);
                drpSTATE.DataTextField = "MYNAME1";
                drpSTATE.DataValueField = "StateID";
                drpSTATE.DataBind();
                drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));

                int StateID = Convert.ToInt32(OBJ.STATE);
                drpSTATE.SelectedValue = OBJ.STATE != null ? OBJ.STATE.ToString() : "0";
                if (DB.tblCityStatesCounties.Where(p => p.StateID == StateID).Count() > 0)
                {
                    drpCity.Items.Clear();
                    drpCity.DataSource = DB.tblCityStatesCounties.Where(p => p.StateID == StateID);
                    drpCity.DataTextField = "CityEnglish";
                    drpCity.DataValueField = "CityID";
                    drpCity.DataBind();
                    drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
                else
                {
                    drpCity.Items.Clear();
                    drpCity.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == COUNTRYID);
                    drpCity.DataTextField = "CityEnglish";
                    drpCity.DataValueField = "CityID";
                    drpCity.DataBind();
                    drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
                drpCity.SelectedValue = OBJ.CITY != null ? OBJ.CITY.ToString() : "0";
                List<Database.TBLCONTACT_DEL_ADRES> List1 = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ContactID).ToList();
                int CountNo = List1.Count();
                CountNo = CountNo + 1;
                txtAdressName1.Text = drpContactMyID.SelectedItem.ToString().Replace(" ", "") + "-" + CountNo;
            }
            else if (DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == ContactID).Count() > 0)
            {
                Database.TBLCONTACT OBJ = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == ContactID);
                txtADDR1.Text = "";
                txtADDR1.Text = OBJ.ADDR1 != null ? OBJ.ADDR1 : "";
                txtADDR2.Text = OBJ.ADDR2 != null ? OBJ.ADDR2 : "";

                int COUNTRYID = Convert.ToInt32(OBJ.COUNTRYID);
                drpCOUNTRYID.SelectedValue = OBJ.COUNTRYID.ToString();
                drpSTATE.DataSource = DB.tblStates.Where(p => p.COUNTRYID == COUNTRYID);
                drpSTATE.DataTextField = "MYNAME1";
                drpSTATE.DataValueField = "StateID";
                drpSTATE.DataBind();
                drpSTATE.Items.Insert(0, new ListItem("-- Not Known --", "0"));

                int StateID = Convert.ToInt32(OBJ.STATE);
                drpSTATE.SelectedValue = OBJ.STATE != null ? OBJ.STATE.ToString() : "0";
                if (DB.tblCityStatesCounties.Where(p => p.StateID == StateID).Count() > 0)
                {
                    drpCity.Items.Clear();
                    drpCity.DataSource = DB.tblCityStatesCounties.Where(p => p.StateID == StateID);
                    drpCity.DataTextField = "CityEnglish";
                    drpCity.DataValueField = "CityID";
                    drpCity.DataBind();
                    drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
                else
                {
                    drpCity.Items.Clear();
                    drpCity.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == COUNTRYID);
                    drpCity.DataTextField = "CityEnglish";
                    drpCity.DataValueField = "CityID";
                    drpCity.DataBind();
                    drpCity.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
                drpCity.SelectedValue = OBJ.CITY != null ? OBJ.CITY.ToString() : "0";
                List<Database.TBLCONTACT_DEL_ADRES> List1 = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == ContactID).ToList();
                int CountNo = List1.Count();
                CountNo = CountNo + 1;
                txtAdressName1.Text = drpContactMyID.SelectedItem.ToString().Replace(" ", "") + "-" + CountNo;
            }


        }
        protected void btnLocation_Click(object sender, EventArgs e)
        {
            GetMap(0, 0, "address", "");
        }
        public string GetRemark(int contID, int DelAddID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == contID && p.DeliveryAdressID == DelAddID && p.REMARKS != null).Count() > 0)
            {
                string Remarkss = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == contID && p.DeliveryAdressID == DelAddID).REMARKS;
                Remarkss = Remarkss.Trim();
                if (Remarkss == "")
                    return "Not Found";
                else
                    return Remarkss;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetAdd2(int contID, int DelAddID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == contID && p.DeliveryAdressID == DelAddID && p.ADDR2 != null).Count() > 0)
            {
                string AddName = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == contID && p.DeliveryAdressID == DelAddID).ADDR2;
                AddName = AddName.Trim();
                if (AddName == "")
                    return "Not Found";
                else
                    return AddName;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void btnSaveLocation_Click(object sender, EventArgs e)
        {
            if (ViewState["tempAID"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int ContactMyID = Convert.ToInt32(drpContactMyID.SelectedValue);
                int ADDID = Convert.ToInt32(ViewState["tempAID"]);
                Database.TBLCONTACT_DEL_ADRES objTBLCONTACT_DEL_ADRES = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == ContactMyID && p.DeliveryAdressID == ADDID);
                objTBLCONTACT_DEL_ADRES.ContactMyID = Convert.ToInt32(drpContactMyID.SelectedValue);
                //objTBLCONTACT_DEL_ADRES.DeliveryAdressID = Convert.ToInt32(drpDeliveryAdressID.SelectedValue);  
                //Latitute='29.346214' , Longitute='48.0913227
                if (txtLatitute.Text == "")
                    objTBLCONTACT_DEL_ADRES.Latitute = "29.346214";
                else
                    objTBLCONTACT_DEL_ADRES.Latitute = txtLatitute.Text;
                if (txtLongitute.Text == "")
                    objTBLCONTACT_DEL_ADRES.Longitute = "48.0913227";
                else
                    objTBLCONTACT_DEL_ADRES.Longitute = txtLongitute.Text;
                objTBLCONTACT_DEL_ADRES.GoogleName = txtinfoWindow.Text;
                double lat = objTBLCONTACT_DEL_ADRES.Latitute != null && objTBLCONTACT_DEL_ADRES.Latitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Latitute) : 29.346214;
                double lng = objTBLCONTACT_DEL_ADRES.Longitute != null && objTBLCONTACT_DEL_ADRES.Longitute != "" ? Convert.ToDouble(objTBLCONTACT_DEL_ADRES.Longitute) : 48.0913227;
                string add = objTBLCONTACT_DEL_ADRES.ADDR2;
                DB.SaveChanges();

                BindData(ContactMyID);
                GetMap(lat, lng, add, "");
                lblMsg.Text = "  Data Edit Successfully";
                pnlSuccessMsg.Visible = true;
            }
        }
    }
}

