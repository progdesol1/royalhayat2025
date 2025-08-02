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
using System.Threading;
using System.Globalization;
using System.Text;
using System.Collections;
using System.Web.Services;
using Microsoft.Ajax.Utilities;
using Database;
using System.Net;
using System.IO;
using Web.CRM;


namespace Web.Master
{
    public partial class ContactMaster : System.Web.UI.Page
    {
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        //  CallEntities DB1 = new CallEntities();
        Database.CallEntities DB = new Database.CallEntities();

        bool flag = false;
        string languageId = "";
        bool Loaddata = true;
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();

            pnlSuccessMsg.Visible = false;
            PanelError.Visible = false;
            pnlwarning.Visible = false;
            lblMsg.Text = "";
            lblerror.Text = "";
            Session["Name"] = "Contact";
            if (!IsPostBack)
            {
                btninvoice1.Visible = false;
                txtCustoID.Enabled = false;
                FistTimeLoad();

                Session["Pagename"] = "Suppliers";
                Session["Name"] = "Contact";
                FillContractorID();

                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.userid == UID && p.StartupContactRefID != 0).Count() > 0)
                {

                }
                else
                {
                    LastData();
                    flag = true;
                }

                if (Loaddata == true)
                {
                    BindData();
                    bindata();
                    redonlyfales();
                }
                if (Request.QueryString["AddContect"] != null)
                {
                    string ADDNEW = Request.QueryString["AddContect"].ToString();
                    if (ADDNEW == "New")
                    {
                        // pnllistrecod.Visible = false;
                        redonlyture();
                        btnSubmit.Visible = true;
                        btnSubmit.Text = "Save";
                        clen();
                        Button1.Visible = false;
                        btnCancel.Text = "Exit";
                        Loaddata = false;
                    }
                }
                if (Request.QueryString["ContactMyID"] != null)
                {
                    string Sapmple = Request.QueryString["ContactMyID"].ToString();
                    decimal ContactMyID = Convert.ToDecimal(Request.QueryString["ContactMyID"]);
                    BindEditMode(ContactMyID);
                    //lblAttecmentcount.Text = Countatt(ContactMyID);
                    //btnattmentmst.Visible = true;
                    if (Request.QueryString["Mode"] != null)
                    {
                        string Mode = Request.QueryString["Mode"].ToString();
                        if (Mode == "Write")
                        {
                            redonlyture();
                            btnSubmit.Text = "Update";
                            btnSubmit.Visible = true;
                            Button1.Visible = false;
                            lblBusContactDe.Text = "GYM Customer Details -Edit Mode";
                        }
                        else
                        {

                            redonlyfales();
                            btnSubmit.Visible = false;
                            Button1.Visible = true;
                            //lblAttecmentcount.Text = Countatt(ContactMyID);
                            //btnattmentmst.Visible = true;
                            lblBusContactDe.Text = "GYM Customer Details -Read Mode";
                        }

                    }
                    //Listview2.DataSource = DB.TBLCONTACTs.Where(p => p.ContactMyID == ContactMyID && p.TenentID == TID);
                    //Listview2.DataBind();
                    var List = DB.TBLCONTACTs.Where(p => p.ContactMyID == ContactMyID && p.TenentID == TID).ToList();
                    ViewState["SaveList"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    Loaddata = false;
                }


                if (Request.QueryString["Sesstion"] != null)
                {
                    if (Session["SerchListContact"] != null)
                    {
                        var List = ((List<Database.TBLCONTACT>)Session["SerchListContact"]).OrderByDescending(p => p.UPDTTIME).ToList();
                        ViewState["SaveList"] = List;
                        //Listview2.DataSource = List;
                        //Listview2.DataBind();
                        int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                        int Totalrec = List.Count();
                        ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    }
                }


            }
            Session["Pagename"] = "Suppliers";
            if (!string.IsNullOrEmpty(Session["Language"] as string))
            {
                if (Session["Language"].ToString().StartsWith("ar-KW") == true)
                {
                    b.Attributes.Remove("dir");
                    b.Attributes.Add("dir", "rtl");
                    GetShow();
                }
                else
                {
                    b.Attributes.Remove("dir");
                    b.Attributes.Add("dir", "ltr");
                    GetHide();
                }
            }

        }
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
        public void GetShow()
        {
            lbl01.Attributes["class"] = "control-label col-md-4 gethide ";
            Label5.Attributes["class"] = "control-label col-md-2  getshow";
            lbl02.Attributes["class"] = "control-label col-md-4 gethide ";
            Label9.Attributes["class"] = "control-label col-md-2  getshow";
            lbl03.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label11.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl04.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label14.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl05.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label16.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl06.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label27.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl07.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label76.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl08.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label78.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl09.Attributes["class"] = "control-label col-md-4 gethide ";
            Label80.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl10.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label82.Attributes["class"] = "control-label col-md-4  getshow";
            lbl11.Attributes["class"] = "control-label col-md-4 gethide ";
            Label84.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl12.Attributes["class"] = "control-label col-md-2 gethide ";
            //Label86.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl13.Attributes["class"] = "control-label col-md-2 gethide ";
            //Label88.Attributes["class"] = "control-label col-md-2  getshow";
            lbl14.Attributes["class"] = "control-label col-md-4 gethide ";
            Label90.Attributes["class"] = "control-label col-md-2  getshow";
            lbl15.Attributes["class"] = "control-label col-md-2 gethide ";
            Label92.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl16.Attributes["class"] = "control-label col-md-2 gethide ";
            //Label94.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl17.Attributes["class"] = "control-label col-md-2 gethide ";
            //Label96.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl18.Attributes["class"] = "control-label col-md-2 gethide ";
            //Label98.Attributes["class"] = "control-label col-md-2  getshow";
            //lbl20.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label100.Attributes["class"] = "control-label col-md-4  getshow";
            //lbl21.Attributes["class"] = "control-label col-md-4 gethide ";
            //Label102.Attributes["class"] = "control-label col-md-4  getshow";
        }
        public void GetHide()
        {
            //Label102.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl21.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label100.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl20.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label98.Attributes["class"] = "control-label col-md-2  gethide";
            //lbl18.Attributes["class"] = "control-label col-md-2 getshow ";
            //Label96.Attributes["class"] = "control-label col-md-2  gethide";
            //lbl17.Attributes["class"] = "control-label col-md-2 getshow ";
            //Label94.Attributes["class"] = "control-label col-md-2  gethide";
            //lbl16.Attributes["class"] = "control-label col-md-2 getshow ";
            Label92.Attributes["class"] = "control-label col-md-2  gethide";
            lbl15.Attributes["class"] = "control-label col-md-2 getshow ";
            Label90.Attributes["class"] = "control-label col-md-2  gethide";
            lbl14.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label88.Attributes["class"] = "control-label col-md-2  gethide";
            //lbl13.Attributes["class"] = "control-label col-md-2 getshow ";
            //Label86.Attributes["class"] = "control-label col-md-2  gethide";
            //lbl12.Attributes["class"] = "control-label col-md-2 getshow ";
            Label84.Attributes["class"] = "control-label col-md-2  gethide";
            lbl11.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label82.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl10.Attributes["class"] = "control-label col-md-4 getshow ";
            Label80.Attributes["class"] = "control-label col-md-4  gethide";
            lbl09.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label78.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl08.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label76.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl07.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label27.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl06.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label16.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl05.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label14.Attributes["class"] = "control-label col-md-4  gethide";
            //lbl04.Attributes["class"] = "control-label col-md-4 getshow ";
            //Label11.Attributes["class"] = "control-label col-md-2  gethide";
            lbl03.Attributes["class"] = "control-label col-md-4 getshow ";
            Label9.Attributes["class"] = "control-label col-md-2  gethide";
            lbl02.Attributes["class"] = "control-label col-md-4 getshow ";
            lbl01.Attributes["class"] = "control-label col-md-4 getshow  ";
            Label5.Attributes["class"] = "control-label col-md-2 gethide ";
        }
        public void redonlyfales()
        {
            txtAddress.Enabled = false;
            //txtAddress2.Enabled = false;
            //txtCity.Enabled = false;
            //drpcity.Enabled = false;
            txtContact2.Enabled = false;
            txtContact3.Enabled = false;
            txtContactName.Enabled = false;
            txtMobileNo.Enabled = false;
            //txtPostalCode.Enabled = false;
            //txtRemark.Enabled = false;
            //txtSocial.Enabled = false;
            //txtZipCode.Enabled = false;
            // drpCompnay.Enabled = false;
            drpCountry.Enabled = false;
            //drpItManager.Enabled = false;
            //drpMyCounLocID.Enabled = false;
            //drpSomib.Enabled = false;
            //drpSates.Enabled = false;
            //txtBarcode.Enabled = false;

            //drpdatasource.Enabled = false;
            lkbContactName.Enabled = lkbContact2.Enabled = lkbContactnl3.Enabled = lkbEmail.Enabled = lkbMobile.Enabled = false;// lkbFax.Enabled = lkbBusPhone.Enabled =LinkButton5.Enabled =  
            lblBusContactDe.Text = "GYM Customer Details -Read Mode";

            ViewState["ModeData"] = "Read";

            txtBirthdate.Enabled = txtCivilID.Enabled = false;
        }
        public void redonlyture()
        {
            txtAddress.Enabled = true;
            //txtAddress2.Enabled = true;
            //txtCity.Enabled = true;
            //drpcity.Enabled = true;
            txtContact2.Enabled = true;
            txtContact3.Enabled = true;
            txtContactName.Enabled = true;
            txtMobileNo.Enabled = true;
            drpCountry.Enabled = true;

            lkbContactName.Enabled = lkbContact2.Enabled = lkbContactnl3.Enabled = lkbEmail.Enabled = lkbMobile.Enabled = true; //lkbFax.Enabled = lkbBusPhone.Enabled = LinkButton5.Enabled =
            lblBusContactDe.Text = "GYM Customer Details -Write Mode";
            ViewState["ModeData"] = "Write";

            txtBirthdate.Enabled = txtCivilID.Enabled = true;
        }
        protected override void InitializeCulture()
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Session["Language"] as string))
            {
                languageId = Session["Language"].ToString();
                SetCulture(languageId);
            }
            else
            {
                string language = Request.Form["__EventTarget"];
                if (language != null)
                {
                    if (language.EndsWith("Arabic") || language.EndsWith("France") || language.EndsWith("English"))
                    {
                        if (!string.IsNullOrEmpty(language))
                        {
                            if (language.EndsWith("Arabic"))
                            {
                                languageId = "ar-KW";
                            }
                            else if (language.EndsWith("France"))
                            {
                                languageId = "fr";
                            }
                            else if (language.EndsWith("English"))
                            {
                                languageId = "en-US";
                            }

                            Session["Language"] = languageId;
                            SetCulture(languageId);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["Language"] as string))
                        {
                            languageId = Session["Language"].ToString();
                            SetCulture(languageId);
                        }
                    }
                }

                if (Session["Language"] != null)
                {
                    if (!Session["Language"].ToString().StartsWith(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)) SetCulture(Session["Language"].ToString());
                }

                base.InitializeCulture();
            }
        }
        protected void SetCulture(string languageId)
        {
            Session["Language"] = languageId;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageId);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageId);
        }
        public void bindata()
        {


        }
        public void BindData()
        {
            if (ViewState["SerchListofContect"] == null)
            {

                List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
                ViewState["SaveList"] = List;
                //Listview2.DataSource = List;
                //Listview2.DataBind();
                if (List.Count() > 0)
                {
                    BindEditMode(List[0].ContactMyID);
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                }

            }
            else
            {
                var List = ((List<Database.TBLCONTACT>)ViewState["SerchListofContect"]).ToList();
                List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();
                foreach (Database.TBLCONTACT item in List)
                {
                    Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactMyID && p.TenentID == TID);
                    Con_List.Add(obj_Contact);
                }
                var List1 = Con_List.OrderBy(p => p.UPDTTIME).ToList();
                ViewState["SaveList"] = List1;
                ViewState["SerchListofContect"] = List1;
                BindEditMode(List1[0].ContactMyID);
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List1.Count();
                ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List1);
                //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                //Listview2.DataBind();
            }

            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        }

        public void BindDataActive()
        {
            if (ViewState["SerchListofContect"] == null)
            {

                List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(m => m.UPDTTIME).ToList();
                ViewState["SaveList"] = List;
                //Listview2.DataSource = List;
                //Listview2.DataBind();
                BindEditMode(List[0].ContactMyID);
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);

            }
            else
            {
                var List = ((List<Database.TBLCONTACT>)ViewState["SerchListofContect"]).ToList();
                List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();
                foreach(Database.TBLCONTACT item in List)
                {
                    Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactMyID && p.TenentID == TID && p.Active == "Y");
                    Con_List.Add(obj_Contact);
                }
                var List1 = Con_List.OrderBy(p => p.UPDTTIME).ToList();
                ViewState["SaveList"] = List1;
                ViewState["SerchListofContect"] = List1;
                BindEditMode(List1[0].ContactMyID);
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List1.Count();
                ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List1);
                //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                //Listview2.DataBind();
            }

            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        }
        public void clen()
        {
            lblCustomer1.Text = lblCustomerL1.Text = lblCustomerL2.Text = lblMobileNo.Text = txtAddress.Text = txtContact2.Text = txtContact3.Text = txtContactName.Text = txtMobileNo.Text = tags_2.Text = "";//txtBarcode.Text =txtCity.Text = tags_3.Text = tags_4.Text = txtPostalCode.Text = txtZipCode.Text = txtAddress2.Text =
            txtBirthdate.Text = txtCivilID.Text = "";
            drpCountry.SelectedIndex = 0;
        }
        protected void ListCustomerMaster_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                int ID = Convert.ToInt32(e.CommandArgument);
                Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID);
                objtbl_CONTACT.Active = "N";
                DB.SaveChanges();
                BindData();

                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Delete Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);

            }
            if (e.CommandName == "btnEdit")
            {
                txtCustoID.Enabled = false;
                decimal ContactMyID = Convert.ToDecimal(Request.QueryString["ContactMyID"]);
                btnSubmit.Text = "Update";
                btninvoice1.Visible = true;
                BindEditMode(ContactMyID);
                redonlyture();
                btnSubmit.Visible = true;
                Button1.Visible = false;
                //lblAttecmentcount.Text = Countatt(ContactMyID);
                //btnattmentmst.Visible = true;
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                lblBusContactDe.Text = "GYM Customer Details -Edit Mode";
                Packagebind();

            }
            if (e.CommandName == "btnview")
            {
                txtCustoID.Enabled = false;
                decimal ContactMyID = Convert.ToDecimal(Request.QueryString["ContactMyID"]);
                //  btnSubmit.Text = "Update";
                BindEditMode(ContactMyID);
                redonlyfales();
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                btnSubmit.Visible = false;
                Button1.Visible = true;
                //lblAttecmentcount.Text = Countatt(ContactMyID);
                //btnattmentmst.Visible = true;
                lblBusContactDe.Text = "GYM Customer Details -Read Mode";

            }
            //if (e.CommandName == "btnActive")
            //{
            //    int ID = Convert.ToInt32(e.CommandArgument);
            //    if (DB.TBLCONTACTs.Where(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
            //    {
            //        Database.TBLCONTACT objTBLCONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y");
            //        objTBLCONTACT.Active = "N";
            //        DB.SaveChanges();
            //    }
            //    else
            //    {
            //        Database.TBLCONTACT objTBLCONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "N");
            //        objTBLCONTACT.Active = "Y";
            //        DB.SaveChanges();
            //    }

            //    BindData();
            //}
        }
        public void BindEditMode(decimal ID)
        {
            txtCustoID.Text = ID.ToString();
            txtCustoID.Enabled = false;
            int SID = Convert.ToInt32(ID);
            Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == ID);
            txtContactName.Text = objtbl_CONTACT.PersName1;
            txtContact2.Text = objtbl_CONTACT.PersName2;
            txtContact3.Text = objtbl_CONTACT.PersName3;
            //   txtEmail.Text = objtbl_CONTACT.EMAIL1;
            txtMobileNo.Text = objtbl_CONTACT.MOBPHONE;

            txtAddress.Text = objtbl_CONTACT.ADDR1;
            //txtAddress2.Text = objtbl_CONTACT.ADDR2;
            DateTime BirthDate = Convert.ToDateTime(objtbl_CONTACT.BirthDate);
            txtBirthdate.Text = BirthDate.ToShortDateString();
            txtCivilID.Text = objtbl_CONTACT.CivilID;
            //txtCity.Text = objtbl_CONTACT.CITY;
            int COID = Convert.ToInt32(objtbl_CONTACT.COUNTRYID);
            drpCountry.SelectedValue = COID.ToString();
            drpMotiveTOJoin.SelectedValue = objtbl_CONTACT.REMARKS;

            tags_2.Text = objtbl_CONTACT.EMAIL1;

            if (objtbl_CONTACT.IMG != null && objtbl_CONTACT.IMG != "0" && objtbl_CONTACT.IMG != "")
            {
                Avatar.ImageUrl = "/CRM/Upload/" + objtbl_CONTACT.IMG;
            }
            else
            {
                Avatar.ImageUrl = "/CRM/Upload/defolt.png";
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtBirthdate.Text != "")
            {
                DateTime bday = Convert.ToDateTime(txtBirthdate.Text);
                DateTime today = DateTime.Now;
                if (bday <= today)
                { }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Enter Birthday not greater than today";
                    return;
                }
            }

            using (var scope = new System.Transactions.TransactionScope())
            {
                int LOID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                string UID = ((USER_MST)Session["USER"]).LOGIN_ID;

                int CID = Convert.ToInt32(ViewState["compId"]);
                if (Request.QueryString["ContactMyID"] != null || CID != 0)
                {
                    Database.TBLCONTACT objtblContact;

                    int ContactID = Convert.ToInt32(Request.QueryString["ContactMyID"]);

                    if (CID != 0)
                    {
                        objtblContact = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CID);
                    }
                    else
                    {
                        objtblContact = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == ContactID);
                    }

                    int CMID = Convert.ToInt32(objtblContact.CONTACTID);
                    objtblContact.TenentID = TID;
                    objtblContact.PHYSICALLOCID = "CRM";
                    objtblContact.PersName1 = txtContactName.Text;
                    objtblContact.PersName2 = txtContact2.Text;
                    objtblContact.PersName3 = txtContact3.Text;

                    objtblContact.EMAIL1 = getFirstCommaseprate(tags_2.Text);

                    objtblContact.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);
                    objtblContact.ADDR1 = txtAddress.Text;
                    if (txtBirthdate.Text != "")
                    {
                        objtblContact.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    }
                    objtblContact.CivilID = txtCivilID.Text;

                    objtblContact.CITY = "0";// drpcity.SelectedValue;
                    objtblContact.STATE = "0";// drpSates.SelectedValue;                  

                    objtblContact.COUNTRYID = 126;// Convert.ToInt32(drpCountry.SelectedValue);                    
                    objtblContact.Active = "Y";
                    objtblContact.UPDTTIME = DateTime.Now;
                    objtblContact.USERID = UID;
                    objtblContact.REMARKS = drpMotiveTOJoin.SelectedValue;
                    objtblContact.ContacType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").REFID : 0;

                    if (avatarUploadd.HasFile)
                    {
                        string path = objtblContact.ContactMyID + "_" + TID + "_" + txtContactName.Text + "_TBLCONTACT_" + avatarUploadd.FileName;
                        avatarUploadd.SaveAs(Server.MapPath("/CRM/Upload/" + path));
                        objtblContact.IMG = path;
                    }

                    DB.SaveChanges();
                    // email Edit 
                    if (tags_2.Text != "")
                    {
                        List<Tbl_RecordType_Mst> List5;
                        if (CID != 0)
                        {
                            List5 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.RecordType == "Email" && p.Recource == 5006).ToList();
                        }
                        else
                        {
                            List5 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == ContactID && p.RecordType == "Email" && p.Recource == 5006).ToList();
                        }


                        foreach (Database.Tbl_RecordType_Mst item in List5)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5001 && p.RecordType == "Email");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }
                        string[] Seperate = tags_2.Text.Split(',');
                        int count = 0;
                        string Sep = "";
                        for (int i = 0; i <= Seperate.Count() - 1; i++)
                        {
                            Sep = Seperate[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID);
                            if (exist.Count() < 1)
                            {
                                count++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Email";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = CMID;
                                obj.RunSerial = count;
                                obj.Recource = 5006;
                                obj.RecourceName = "Contact";
                                obj.RecValue = Seperate[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();
                            }
                            else
                            {
                                if (DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID).Count() < 1)
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



                    if (txtMobileNo.Text != "")
                    {
                        //BusPhonuber
                        List<Tbl_RecordType_Mst> List2;
                        if (CID != 0)
                        {
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();
                        }
                        else
                        {
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == ContactID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();
                        }

                        foreach (Database.Tbl_RecordType_Mst item in List2)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5003 && p.RecordType == "BusPhone");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }

                        string[] Seperate2 = txtMobileNo.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecordType == "MobileNO" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID);
                            if (exist.Count() < 1)
                            {
                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "MobileNO";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = CMID;
                                obj.RunSerial = count2;
                                obj.Recource = 5006;
                                obj.RecourceName = "Contact";
                                obj.RecValue = Seperate2[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();
                            }
                            else
                            {
                                if (DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecordType == "BusPhone" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID).Count() < 1)
                                {
                                    if (Sep2 != "00000")
                                    {
                                        string display = "Mobile NO Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Mobile NO Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);

                    scope.Complete();

                    Response.Redirect("ContactMaster.aspx?MID=5Rg9z~r1w9M=");
                    //  btnSubmit.Text = "Add";
                }
                else
                {
                    int CID1 = Convert.ToInt32(txtCustoID.Text);
                    if (DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == CID1).Count() > 0)
                    {
                        PanelError.Visible = true;
                        lblerror.Text = "Customer ID Already Exist";
                        return;
                    }
                    int COMPID = Convert.ToInt32(txtCustoID.Text);
                    int SeqCount = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count();
                    Database.TBLCONTACT objtblContact = new Database.TBLCONTACT();
                    objtblContact.TenentID = TID;
                    objtblContact.ContactMyID = CID1 != 0 ? CID1 : DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
                    objtblContact.CONTACTID = CID1 != 0 ? CID1 : DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.CONTACTID) + 1) : 1;
                    int CMID = Convert.ToInt32(objtblContact.CONTACTID);
                    objtblContact.PHYSICALLOCID = "CRM";
                    objtblContact.PersName1 = txtContactName.Text;
                    objtblContact.PersName2 = txtContact2.Text;
                    objtblContact.PersName3 = txtContact3.Text;
                    objtblContact.EMAIL1 = getFirstCommaseprate(tags_2.Text);
                    //objtblContact.FaxID = getFirstCommaseprate(tags_3.Text);
                    objtblContact.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);
                    objtblContact.ADDR1 = txtAddress.Text;
                    if (txtBirthdate.Text != "")
                    {
                        objtblContact.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    }

                    objtblContact.CivilID = txtCivilID.Text;
                    objtblContact.CITY = "0";// drpcity.SelectedValue;
                    objtblContact.STATE = "0";// drpSates.SelectedValue; 
                    string CNAME = drpCountry.SelectedItem.ToString();
                    objtblContact.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtblContact.Active = "Y";
                    objtblContact.ENTRYTIME = DateTime.Now;
                    objtblContact.UPDTTIME = DateTime.Now;
                    objtblContact.USERID = UID;
                    objtblContact.REMARKS = drpMotiveTOJoin.SelectedValue;
                    objtblContact.ContacType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").REFID : 0;

                    if (avatarUploadd.HasFile)
                    {
                        string path = objtblContact.ContactMyID + "_" + TID + "_" + txtContactName.Text + "_TBLCONTACT_" + avatarUploadd.FileName;
                        avatarUploadd.SaveAs(Server.MapPath("/CRM/Upload/" + path));
                        objtblContact.IMG = path;
                    }

                    DB.TBLCONTACTs.AddObject(objtblContact);
                    DB.SaveChanges();
                    if (tags_2.Text != "")
                    {
                        string[] Seperate = tags_2.Text.Split(',');
                        int count = 0;
                        string Sep = "";
                        for (int i = 0; i <= Seperate.Count() - 1; i++)
                        {
                            Sep = Seperate[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.RecordType == "Email" && c.Recource == 5007 && c.RecourceName == "Contact" && c.TenentID == TID);
                            if (exist.Count() <= 0)
                            {
                                count++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Email";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = CMID;
                                obj.RunSerial = count;
                                obj.Recource = 5007;
                                obj.RecourceName = "Contact";
                                obj.RecValue = Seperate[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            }

                            else
                            {
                                string display = "Email Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Email Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }

                    }

                    if (txtMobileNo.Text != "")
                    {
                        //BusPhonuber
                        List<Tbl_RecordType_Mst> List2;
                        if (CID != 0)
                        {
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();
                        }
                        else
                        {
                            List2 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5005 && p.RecordType == "BusPhone").ToList();
                        }

                        foreach (Database.Tbl_RecordType_Mst item in List2)
                        {
                            // var Deleteobj = DB.Tbl_RecordType_Mst.SingleOrDefault(p => p.CompniyID == COMPID && p.RecTypeID == 5003 && p.RecordType == "BusPhone");
                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }
                        string[] Seperate2 = txtMobileNo.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecordType == "MobileNO" && c.TenentID == TID && c.Recource == 5006);
                            if (exist.Count() <= 0)
                            {
                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "MobileNO";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = CMID;
                                obj.RunSerial = count2;
                                obj.Recource = 5006;
                                obj.RecourceName = "Contact";
                                obj.RecValue = Seperate2[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();
                            }
                            else
                            {
                                if (DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecordType == "MobileNO" && c.TenentID == TID && c.Recource == 5006).Count() < 1)
                                {
                                    if (Sep2 != "00000")
                                    {
                                        string display = "Mobile NO Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Mobile NO Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);

                }
                BindData();
                btnSubmit.Visible = false;
                Button1.Visible = true;
                redonlyfales();
                //  clen();
                //btnattmentmst.Visible = false;
                txtCustoID.Enabled = false;
                scope.Complete(); //  To commit.

            }

        }

        public string getFirstCommaseprate(string SeperateVal)
        {
            string[] Seperate = SeperateVal.Split(',');
            string FirstValue = Seperate[0];
            return FirstValue;
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            lblEmail12.Text = "";
            string[] Seperate = tags_2.Text.Split('/');

            string Sep = "";

            for (int i = 0; i <= Seperate.Count() - 1; i++)
            {

                Sep = Seperate[i];

                if (Sep.Contains(".") || Sep.Contains("@"))
                {
                    var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep && c.TenentID == TID);

                    if (exist.Count() <= 0)
                    {

                    }
                    else
                    {
                        int compname = DB.Tbl_RecordType_Mst.Single(c => c.RecValue == Sep && c.TenentID == TID).CompniyAndContactID;
                        lblEmail12.Text = "Email Is Duplicate and It's allready used for this " + DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == compname && p.TenentID == TID).COMPNAME;
                    }
                }
                else
                {
                    lblEmail12.Text = "Invalid Email: " + Sep;
                }
            }
        }

        protected void btnContact_Click(object sender, EventArgs e)
        {
            lblCustomer1.Text = "";
            var exist = DB.TBLCONTACTs.Where(c => c.PersName1 == txtContactName.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                lblCustomer1.Text = "Contact Name 1 Is Duplicate";
            }
        }
        protected void btncontactNL2_Click(object sender, EventArgs e)
        {
            lblCustomer1.Text = "";
            var exist = DB.TBLCONTACTs.Where(c => c.PersName2 == txtContact2.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                lblCustomer1.Text = "Contact Name 2 Is Duplicate";
            }
        }
        protected void btnContactnl3_Click(object sender, EventArgs e)
        {
            lblCustomer1.Text = "";
            var exist = DB.TBLCONTACTs.Where(c => c.PersName3 == txtContact3.Text && c.TenentID == TID);
            if (exist.Count() <= 0)
            {

            }
            else
            {
                lblCustomer1.Text = "Contact Name 3 Is Duplicate";
            }
        }

        protected void btnMobile_Click(object sender, EventArgs e)
        {
            lblMobileNo.Text = "";
            string CNAME = "Kuwait";// drpCountry.SelectedItem.Text;
            int CID = 126;// Convert.ToInt32(drpCountry.SelectedValue);
            int Flenth = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.COUNTRYID == CID && p.TenentID == TID).TelLength);

            if (txtMobileNo.Text.Length == Flenth)
            {
                var exist = DB.TBLCONTACTs.Where(c => c.MOBPHONE == txtMobileNo.Text && c.TenentID == TID);
                if (exist.Count() <= 0)
                {

                }
                else
                {
                    lblMobileNo.Text = "Mobile Number Is Duplicate";
                }
            }
            else
            {
                lblMobileNo.Text = CNAME + "Country is Maximun And Minimum " + Flenth + " Digit Requried " + txtMobileNo.Text;
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactMaster.aspx?MID=JopOdwUVhcI=");
        }


        public string getStste(int SID)
        {
            if (SID != 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == SID && p.TenentID == TID).CITY;
            else
                return "";
        }
        public void FillContractorID()
        {
            drpCountry.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == CID && p.Active == "Y");
            drpCountry.DataTextField = "COUNAME1";
            drpCountry.DataValueField = "COUNTRYID";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpSates.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpcity.Items.Insert(0, new ListItem("-- Select City--", "0"));

            drpSort.Items.Clear();
            drpSort.Items.Insert(0, new ListItem("---Sorting By---", "0"));
            drpSort.Items.Insert(1, new ListItem(Label66.Text, "1"));
            //drpSort.Items.Insert(2, new ListItem(Label67.Text, "2"));
            //drpSort.Items.Insert(2, new ListItem(Label68.Text, "3"));
            //drpSort.Items.Insert(3, new ListItem(Label69.Text, "4"));
            //drpSort.Items.Insert(4, new ListItem(Label70.Text, "5"));
            //drpSort.Items.Insert(5, new ListItem(Label71.Text, "6"));
            drpSort.Items.Insert(2, new ListItem(Label72.Text, "7"));
            //drpSort.Items.Insert(7, new ListItem(Label73.Text, "8"));
            drpPackage.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID);
            drpPackage.DataTextField = "ProdName1";
            drpPackage.DataValueField = "MYPRODID";
            drpPackage.DataBind();
            drpPackage.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpMotiveTOJoin.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "GYM" && p.REFSUBTYPE == "Motive");
            drpMotiveTOJoin.DataTextField = "REFNAME1";
            drpMotiveTOJoin.DataValueField = "REFID";
            drpMotiveTOJoin.DataBind();
            drpMotiveTOJoin.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            decimal CID = Convert.ToDecimal(ViewState["compId"]);
            btnSubmit.Text = "Update";
            BindEditMode(CID);
            //updCountry.Update();
            redonlyture();

            lblBusContactDe.Text = "GYM Customer Details -Edit Mode";
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {

        }

        protected void txtContactName_TextChanged(object sender, EventArgs e)
        {
            lblCustomer1.Text = "";
            if (!string.IsNullOrEmpty(txtContactName.Text))
            {
                var exist = DB.TBLCONTACTs.Where(c => c.PersName1 == txtContactName.Text && c.Active == "Y" && c.TenentID == TID);
                if (exist.Count() <= 0)
                {
                    lblCustomer1.Text = "Customer Name  Available";
                    txtContact2.Text = Translate(txtContactName.Text, "ar");
                    txtContact3.Text = Translate(txtContactName.Text, "fr");
                }
                else
                {
                    var UserList = DB.TBLCONTACTs.SingleOrDefault(p => p.PersName1 == txtContactName.Text && p.Active == "Y" && p.TenentID == TID);
                    ViewState["compId"] = UserList.ContactMyID;

                    ModalPopupExtender4.Show();
                    labelCopop.Text = UserList.PersName1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FaxID;
                    lblBuspop.Text = UserList.BUSPHONE1;
                }
            }
            else
            {
                lblCustomer1.Text = "Insert The Customer Name Available";
            }
        }
        public string Translate(string textvalue, string to)
        {
            string appId = "A70C584051881A30549986E65FF4B92B95B353A5";//go to http://msdn.microsoft.com/en-us/library/ff512386.aspx to obtain AppId.
            // string textvalue = "Translate this for me";
            string from = "en";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + textvalue + "&from=" + from + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        protected void txtContact3_TextChanged(object sender, EventArgs e)
        {
            lblCustomerL2.Text = "";
            if (!string.IsNullOrEmpty(txtContact3.Text))
            {
                var exist = DB.TBLCONTACTs.Where(c => c.PersName3 == txtContact3.Text && c.TenentID == TID);
                var UserList = DB.TBLCONTACTs.SingleOrDefault(p => p.PersName3 == txtContact3.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblCustomerL2.Text = "Customer Name  Available";
                }
                else
                {
                    ViewState["compId"] = UserList.ContactMyID;

                    ModalPopupExtender4.Show();
                    labelCopop.Text = UserList.PersName1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FaxID;
                    lblBuspop.Text = UserList.BUSPHONE1;
                }
            }
            else
            {
                lblCustomerL2.Text = "Insert The Customer Name Available";
            }
        }

        protected void txtMobileNo_TextChanged(object sender, EventArgs e)
        {
            lblCustomerL2.Text = "";
            if (!string.IsNullOrEmpty(txtMobileNo.Text))
            {
                var exist = DB.TBLCONTACTs.Where(c => c.MOBPHONE == txtMobileNo.Text && c.TenentID == TID);
                var UserList = DB.TBLCONTACTs.SingleOrDefault(p => p.MOBPHONE == txtMobileNo.Text && p.TenentID == TID);

                if (exist.Count() <= 0)
                {

                    lblMobileNo.Text = "Mobiel Number Available";
                }
                else
                {
                    ViewState["compId"] = UserList.ContactMyID;
                    ModalPopupExtender4.Show();
                    labelCopop.Text = UserList.PersName1;
                    lblmopop.Text = UserList.MOBPHONE;
                    lblEmailpop.Text = UserList.EMAIL1;
                    lblFaxpop.Text = UserList.FaxID;
                    lblBuspop.Text = UserList.BUSPHONE1;
                }
            }
            else
            {
                lblMobileNo.Text = "Insert The Mobil Number!";
            }
        }

        public string getshocial(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID && p.TenentID == TID).REFNAME1;
        }

        public string getcomniy(int COID)
        {
            if (DB.TBLCONTACTs.Where(p => p.ContactMyID == COID && p.TenentID == TID).Count() > 0)
                return DB.TBLCONTACTs.SingleOrDefault(p => p.ContactMyID == COID && p.TenentID == TID).PersName1;
            else
                return txtContactName.Text;
        }
        public string getremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID && p.TenentID == TID).Remarks;
        }
        public string getjobititel(int JID)
        {
            if (JID != 0)
                return DB.Tbl_Position_Mst.Single(p => p.PositionID == JID && p.TenentID == TID).PositionName;
            else
                return "";
        }

        public void LastData()
        {
           
            btnSubmit.Visible = false;
            DateTime Maxdate = Convert.ToDateTime(DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y").Max(p => p.UPDTTIME));
            if (DB.TBLCONTACTs.Where(p => p.Active == "Y" && p.TenentID == TID && p.UPDTTIME == Maxdate).Count() > 0)
            {
                decimal LIDID = Convert.ToDecimal(DB.TBLCONTACTs.Where(p => p.Active == "Y" && p.TenentID == TID && p.UPDTTIME == Maxdate).Max(p => p.ContactMyID));
                BindEditMode(LIDID);
            }
            Packagebind();
        }


        public string getContactName(int ID)
        {
            int ConID = Convert.ToInt32(ID);
            string Name = "";
            if (DB.TBLCONTACTs.Where(p => p.ContactMyID == ConID && p.TenentID == TID).Count() > 0)
            {
                Database.TBLCONTACT obj_con = DB.TBLCONTACTs.Single(p => p.ContactMyID == ConID && p.TenentID == TID);
                Name = obj_con.PersName1;
            }
            return Name;
        }

        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void drpSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            if (drpSort.SelectedValue == "1")
                List = List.OrderBy(m => m.PersName1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "2")
                List = List.OrderBy(m => m.ADDR1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "3")
                List = List.OrderBy(m => m.EMAIL1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "4")
                List = List.OrderBy(m => m.MOBPHONE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "5")
                List = List.OrderBy(m => m.STATE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "6")
                List = List.OrderBy(m => m.ZIPCODE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "7")
                List = List.OrderBy(m => m.CITY).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "8")
                List = List.OrderBy(m => m.REMARKS).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);

        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {
            var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            if (take == Totalrec && Skip == (Totalrec - Showdata))
                btnNext1.Enabled = false;
            else
                btnNext1.Enabled = true;
            if (take == Showdata && Skip == 0)
                btnPrevious1.Enabled = false;
            else
                btnPrevious1.Enabled = true;

            ChoiceID = take / Showdata;

            ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = List.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                if (take == Showdata && Skip == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;

                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;

                ChoiceID = take / Showdata;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {


        }
        protected void btnfirst1_Click(object sender, EventArgs e)
        {
            var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = List.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                if (Tvalue == Showdata && Svalue == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
            }
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            ViewState["SerchListTabel"] = null;
            string id1 = txtSearch.Text;
            List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.PersName2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID && p.Active == "Y").OrderBy(p => p.ContactMyID).ToList();
            ViewState["SaveList"] = List;
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
            ViewState["SerchListTabel"] = List;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["compId"] = null;
            txtCustoID.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? (DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1).ToString() : "1";
            txtCustoID.Enabled = true;
            redonlyture();
            btnSubmit.Visible = true;
            btnSubmit.Text = "Save";
            clen();
            Button1.Visible = false;


            Avatar.ImageUrl = "~/Gallery/defolt.png";
        }

        protected void btnOpportunity_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(ViewState["compId"]);
            int CMID = 0;
            if (Request.QueryString["ContactMyID"] != null || CID != 0)
            {
                int ContactID = Convert.ToInt32(Request.QueryString["ContactMyID"]);

                if (CID != 0)
                    CMID = CID;
                else
                    CMID = ContactID;
            }
            else
                CMID = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
            string Mode = ViewState["ModeData"].ToString();
            string URL = "AttachmentMst.aspx?AttachID=" + CMID + "&DID=Contact&Mode=" + Mode;
            Response.Redirect(URL);
        }
        public string Countatt(decimal COMPID)
        {
            var List = DB.tbl_DMSAttachmentMst.Where(p => p.AttachmentById == COMPID && p.AttachmentByName == "Contact" && p.TenentID == TID).ToList();
            int Count = List.Count();
            return Count.ToString();
        }

        protected void txtBirthdate_TextChanged(object sender, EventArgs e)
        {
            if (txtBirthdate.Text != "")
            {
                DateTime bday = Convert.ToDateTime(txtBirthdate.Text);
                DateTime today = DateTime.Now;
                if (bday <= today)
                { }
                else
                {
                    PanelError.Visible = true;
                    lblerror.Text = "Enter Birthday not greater than today";
                    return;
                }
            }
            else
            {
                PanelError.Visible = true;
                lblerror.Text = "Enter Valid Birthday";
                return;
            }
        }

        protected void chkactive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkactive.Checked == true)
            {
                BindDataActive();
            }
            else
            {
                BindData();
            }
        }
        List<Database.TBLCONTACT> ListTBLCONTACT = new List<Database.TBLCONTACT>();
        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (ListTBLCONTACT.Count() < 1)
            //{
            //    ListTBLCONTACT = DB.TBLCONTACTs.Where(p => p.TenentID == TID).ToList();

            //}

            //LinkButton lnkbtnActive = (LinkButton)e.Item.FindControl("lnkbtnActive");
            //Label lblContactID = (Label)e.Item.FindControl("lblContactID");
            //int ID = Convert.ToInt32(lblContactID.Text);

            //if (ListTBLCONTACT.Where(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
            //{
            //    lnkbtnActive.Text = "DeActivate";
            //    lnkbtnActive.CssClass = "btn btn-sm red filter-submit margin-bottom";
            //}
            //else
            //{
            //    lnkbtnActive.Text = "Active";
            //    lnkbtnActive.CssClass = "btn btn-sm green filter-submit margin-bottom";
            //}
        }

        public string getCityName(int City, int StateID)
        {
            if (DB.tblCityStatesCounties.Where(p => p.StateID == StateID && p.CityID == City).Count() > 0)
            {
                return DB.tblCityStatesCounties.Single(p => p.StateID == StateID && p.CityID == City).CityEnglish;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void txtCustoID_TextChanged(object sender, EventArgs e)
        {

        }

       
        protected void btninvoice1_Click(object sender, EventArgs e)
        {
            pnlpack.Visible = true;
        }

        protected void btnpacksave_Click(object sender, EventArgs e)
        {
            pnlwarning.Visible = false;
            int CustvendID = Convert.ToInt32(txtCustoID.Text);
            if(DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == CustvendID).Count() == 0)
            {
                string uid = ((USER_MST)Session["USER"]).LOGIN_ID;
                int mytransid = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;

                int TenentID = TID;
                int MYTRANSID = mytransid; //mytranceid
                int ToTenentID = 7;
                int TOLOCATIONID = 1;
                string MainTranType = "N";
                string TranType = "N";
                int transid = 1;
                int transsubid = 1;
                string MYSYSNAME = "GYM";
                decimal COMPID = 1;
                decimal CUSTVENDID = Convert.ToDecimal(txtCustoID.Text); //customerID
                string LF = "L";
                string PERIOD_CODE = "N";
                string ACTIVITYCODE = "N";
                decimal MYDOCNO = 0;
                string USERBATCHNO = "N";
                decimal TOTQTY1 = 0;
                decimal TOTAMT = Convert.ToDecimal(txtAmount.Text); //amount
                decimal AmtPaid = Convert.ToDecimal(txtAmount.Text);
                string PROJECTNO = "N";
                string CounterID = "N";
                string PrintedCounterInvoiceNo = "N";
                int JOID = 0;
                DateTime TRANSDATE = DateTime.Now;
                string REFERENCE = "N";
                string NOTES = "N";
                int CRUP_ID = 0;
                string GLPOST = "N";
                string GLPOST1 = "N";
                string GLPOSTREF1 = "N";
                string GLPOSTREF = "N";
                string ICPOST = "N";
                string ICPOSTREF = "N";
                string USERID = uid.ToString();
                bool ACTIVE = true;
                int COMPANYID = 0;
                DateTime ENTRYDATE = Convert.ToDateTime(txtpackStartdate.Text); //start date
                DateTime ENTRYTIME = Convert.ToDateTime(txtpackEnddate.Text); //end date
                DateTime UPDTTIME = DateTime.Now;
                int InvoiceNO = 0;
                decimal Discount = 0;
                DateTime SDT = DateTime.Now;
                string STD = "";
                if(SDT >= ENTRYDATE && SDT <= ENTRYTIME)
                {
                    STD = "Running";
                }
                else
                {
                    STD = "End Package";
                }
                string Status = STD;
                int Terms = 0;
                string DatainserStatest = "Add";
                string Custmerid = "N";
                string Swit1 = "N";
                string ExtraField2 = "N";
                int RefTransID = Convert.ToInt32(drpPackage.SelectedValue); // package GYM
                string Reason = "N";
                string TransDocNo = "N";
                int LinkTransID = 1;

                Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, ExtraField2, RefTransID, Reason, TransDocNo, LinkTransID);

                int TenentID_DT = TID;
                int MYTRANSID_DT = mytransid; //mytranceid
                int locationID_DT = 1;
                int MyProdID_DT = 1;
                string REFTYPE_DT = "0";
                string REFSUBTYPE_DT = "0";
                string PERIOD_CODE_DT = "1";
                string MYSYSNAME_DT = "GYM";
                int JOID_DT = 1;
                int JOBORDERDTMYID_DT = 1;
                int ACTIVITYID_DT = 1;
                string DESCRIPTION_DT = "GYM";
                string UOM_DT = "N";
                int QUANTITY_DT = Convert.ToInt32(drpPackage.SelectedValue); // package GYM
                decimal UNITPRICE_DT = 1;
                decimal AMOUNT_DT = Convert.ToDecimal(txtAmount.Text); //amount
                decimal OVERHEADAMOUNT_DT = 1;
                string BATCHNO_DT = "N";
                int BIN_ID_DT = 0;
                string BIN_TYPE_DT = "N";
                string GRNREF_DT = "N";
                decimal DISPER_DT = 0;
                decimal DISAMT_DT = 0;
                decimal TAXPER_DT = 0;
                decimal TAXAMT_DT = 0;
                decimal PROMOTIONAMT_DT = 0;
                int CRUP_ID_DT = 0;
                string GLPOST_DT = "N";
                string GLPOST1_DT = "N";
                string GLPOSTREF1_DT = "N";
                string GLPOSTREF_DT = "N";
                string ICPOST_DT = "N";
                string ICPOSTREF_DT = "N";
                DateTime EXPIRYDATE_DT = DateTime.Now;
                bool ACTIVE_DT = true;
                string SWITCH1_DT = "N";
                int COMPANYID1_DT = 1;
                int DelFlag_DT = 0;
                string ITEMID_DT = "N";
                DateTime StartDate = Convert.ToDateTime(txtpackStartdate.Text); //start date
                DateTime EndDate = Convert.ToDateTime(txtpackEnddate.Text); //end date

                Classes.EcommAdminClass.insertICTR_DT(TenentID_DT, MYTRANSID_DT, locationID_DT, MyProdID_DT, REFTYPE_DT, REFSUBTYPE_DT, PERIOD_CODE_DT, MYSYSNAME_DT, JOID_DT, JOBORDERDTMYID_DT, ACTIVITYID_DT, DESCRIPTION_DT, UOM_DT, QUANTITY_DT, UNITPRICE_DT, AMOUNT_DT, OVERHEADAMOUNT_DT, BATCHNO_DT, BIN_ID_DT, BIN_TYPE_DT, GRNREF_DT, DISPER_DT, DISAMT_DT, TAXPER_DT, TAXAMT_DT, PROMOTIONAMT_DT, CRUP_ID_DT, GLPOST_DT, GLPOST1_DT, GLPOSTREF1_DT, GLPOSTREF_DT, ICPOST_DT, ICPOSTREF_DT, EXPIRYDATE_DT, ACTIVE_DT, SWITCH1_DT, COMPANYID1_DT, DelFlag_DT, ITEMID_DT, StartDate, EndDate);
                Packagebind();
            }
            else
            {
                pnlwarning.Visible = true;
                lblmsgw.Text = "This Customer have a package allready exist";
            }
        }
        public void Packagebind()
        {
            List<Database.ICTR_HD> HDLIST = DB.ICTR_HD.Where(p => p.TenentID == TID).ToList();
            if(HDLIST.Count() > 0)
            {
                int CUSTVENDID = Convert.ToInt32(txtCustoID.Text); //customerID
                DateTime TodayDate = DateTime.Now;
                DateTime SDT = Convert.ToDateTime(HDLIST.Single(p => p.TenentID == TID && p.CUSTVENDID == CUSTVENDID).ENTRYDATE);
                DateTime EDT = Convert.ToDateTime(HDLIST.Single(p => p.TenentID == TID && p.CUSTVENDID == CUSTVENDID).ENTRYTIME);
                if (TodayDate >= SDT && TodayDate <= EDT)
                {

                }
                else
                {
                    Database.ICTR_HD objHD = DB.ICTR_HD.Single(p => p.TenentID == TID && p.CUSTVENDID == CUSTVENDID);
                    objHD.Status = "End Package";
                    DB.SaveChanges();
                }
                ListPackage.DataSource = HDLIST.Where(p => p.TenentID == TID && p.CUSTVENDID == CUSTVENDID);
                ListPackage.DataBind();
            }           
        }
        public string GetPack(int RID)
        {
            if(DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == RID).Count() > 0)
            {
                string Packname = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == RID).ProdName1;
                return Packname;
            }
            else
            {
                return "Not Found";
            }
        }
        //Attendence


    }
}