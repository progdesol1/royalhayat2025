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
using System.Data.SqlClient;


namespace Web.CRM
{
    public partial class ContactMaster : System.Web.UI.Page
    {
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        //  CallEntities DB1 = new CallEntities();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        Database.CallEntities DB = new Database.CallEntities();
        List<tblCONTACTBu> ListtblCONTACTBus = new List<tblCONTACTBu>();
        bool flag = false;
        string languageId = "";
        bool Loaddata = true;
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            panelMsg.Visible = false;
            pnlSuccessMsg.Visible = false;
            PanelError.Visible = false;
            lblMsg.Text = "";
            lblerror.Text = "";
            Session["Name"] = "Contact";
            if (!IsPostBack)
            {
                Maitanance();
                txtCustoID.Enabled = false;
                FistTimeLoad();
                Session["Pagename"] = "Suppliers";
                Session["Name"] = "Contact";
                FillContractorID();
                BindTitleData();
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.userid == UID && p.StartupContactRefID != 0).Count() > 0)
                {

                    CRMUserSetup obj = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.userid == UID && p.StartupContactRefID != 0);
                    chDefaultSet.Checked = obj.Active == true ? true : false;
                    if (obj.Active == true)
                    {
                        DrpTitle.SelectedValue = obj.StartupContactRefID.ToString();
                        SetDefaultGrid();
                    }

                }
                else
                {
                    LastData();
                    flag = true;
                }

                if (Loaddata == true)
                {
                    //  BindData();
                    bindata();
                    redonlyfales();
                }
                if (Request.QueryString["AddContect"] != null)
                {
                    string ADDNEW = Request.QueryString["AddContect"].ToString();
                    if (ADDNEW == "New")
                    {
                        // pnllistrecod.Visible = false;
                        txtCustoID.Text = (DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1).ToString();
                        redonlyture();
                        btnSubmit.Visible = true;
                        btnSubmit.Text = "Save";
                        clen();
                        Button1.Visible = false;
                        btnCancel.Text = "Exit";
                        Loaddata = false;
                        if (Request.QueryString["CCID"] != null)
                        {
                            ViewState["CCID"] = Request.QueryString["CCID"];
                        }
                    }
                }
                if (Request.QueryString["ContactMyID"] != null)
                {
                    decimal ContactMyID = Convert.ToDecimal(Request.QueryString["ContactMyID"]);
                    BindEditMode(ContactMyID);
                    lblAttecmentcount.Text = Countatt(ContactMyID);
                    btnattmentmst.Text = "Attachment";
                    btnattmentmst.Visible = true;
                    if (Request.QueryString["Mode"] != null)
                    {
                        string Mode = Request.QueryString["Mode"].ToString();
                        if (Mode == "Write")
                        {
                            redonlyture();
                            btnSubmit.Text = "Update";
                            btnSubmit.Visible = true;
                            Button1.Visible = false;
                            lblBusContactDe.Text = "Business Contact Details -Edit Mode";
                            lblBCDeta.Text = "Business Contact Details -Edit Mode";
                            lblwExistance.Text = "Web Existance -Edit Mode";
                            lblWEmp.Text = "Working Company -Edit Mode";
                        }
                        else
                        {

                            redonlyfales();
                            btnSubmit.Visible = false;
                            Button1.Visible = true;
                            lblAttecmentcount.Text = Countatt(ContactMyID);
                            btnattmentmst.Text = "Attachment";
                            btnattmentmst.Visible = true;
                            lblBusContactDe.Text = "Business Contact Details -Read Mode";
                            lblBCDeta.Text = "Business Contact Details -Read Mode";
                            lblwExistance.Text = "Web Existance -Read Mode";
                            lblWEmp.Text = "Working Company -Read Mode";
                        }

                    }
                    //Listview2.DataSource = DB.TBLCONTACTs.Where(p => p.ContactMyID == ContactMyID && p.TenentID == TID);
                    //Listview2.DataBind();
                    var List = DB.TBLCONTACTs.Where(p => p.ContactMyID == ContactMyID && p.TenentID == TID).ToList();
                    ViewState["SaveList"] = List;
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
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
                        ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
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
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
        public void GetShow()
        {
            lbl01.Attributes["class"] = "control-label col-md-2 gethide ";
            Label5.Attributes["class"] = "control-label col-md-2  getshow";
            lbl02.Attributes["class"] = "control-label col-md-2 gethide ";
            Label9.Attributes["class"] = "control-label col-md-2  getshow";
            lbl03.Attributes["class"] = "control-label col-md-2 gethide ";
            Label11.Attributes["class"] = "control-label col-md-2  getshow";
            lbl04.Attributes["class"] = "control-label col-md-4 gethide ";
            Label14.Attributes["class"] = "control-label col-md-4  getshow";
            lbl05.Attributes["class"] = "control-label col-md-4 gethide ";
            Label16.Attributes["class"] = "control-label col-md-4  getshow";
            lbl06.Attributes["class"] = "control-label col-md-4 gethide ";
            Label27.Attributes["class"] = "control-label col-md-4  getshow";
            lbl07.Attributes["class"] = "control-label col-md-4 gethide ";
            Label76.Attributes["class"] = "control-label col-md-4  getshow";
            lbl08.Attributes["class"] = "control-label col-md-4 gethide ";
            Label78.Attributes["class"] = "control-label col-md-4  getshow";
            lbl09.Attributes["class"] = "control-label col-md-4 gethide ";
            Label80.Attributes["class"] = "control-label col-md-4  getshow";
            lbl10.Attributes["class"] = "control-label col-md-4 gethide ";
            Label82.Attributes["class"] = "control-label col-md-4  getshow";
            lbl11.Attributes["class"] = "control-label col-md-2 gethide ";
            Label84.Attributes["class"] = "control-label col-md-2  getshow";
            lbl12.Attributes["class"] = "control-label col-md-2 gethide ";
            Label86.Attributes["class"] = "control-label col-md-2  getshow";
            lbl13.Attributes["class"] = "control-label col-md-2 gethide ";
            Label88.Attributes["class"] = "control-label col-md-2  getshow";
            lbl14.Attributes["class"] = "control-label col-md-2 gethide ";
            Label90.Attributes["class"] = "control-label col-md-2  getshow";
            lbl15.Attributes["class"] = "control-label col-md-2 gethide ";
            Label92.Attributes["class"] = "control-label col-md-2  getshow";
            lbl16.Attributes["class"] = "control-label col-md-2 gethide ";
            Label94.Attributes["class"] = "control-label col-md-2  getshow";
            lbl17.Attributes["class"] = "control-label col-md-2 gethide ";
            Label96.Attributes["class"] = "control-label col-md-2  getshow";
            lbl18.Attributes["class"] = "control-label col-md-2 gethide ";
            Label98.Attributes["class"] = "control-label col-md-2  getshow";
            lbl20.Attributes["class"] = "control-label col-md-4 gethide ";
            Label100.Attributes["class"] = "control-label col-md-4  getshow";
            lbl21.Attributes["class"] = "control-label col-md-4 gethide ";
            Label102.Attributes["class"] = "control-label col-md-4  getshow";
        }
        public void GetHide()
        {
            Label102.Attributes["class"] = "control-label col-md-4  gethide";
            lbl21.Attributes["class"] = "control-label col-md-4 getshow ";
            Label100.Attributes["class"] = "control-label col-md-4  gethide";
            lbl20.Attributes["class"] = "control-label col-md-4 getshow ";
            Label98.Attributes["class"] = "control-label col-md-2  gethide";
            lbl18.Attributes["class"] = "control-label col-md-2 getshow ";
            Label96.Attributes["class"] = "control-label col-md-2  gethide";
            lbl17.Attributes["class"] = "control-label col-md-2 getshow ";
            Label94.Attributes["class"] = "control-label col-md-2  gethide";
            lbl16.Attributes["class"] = "control-label col-md-2 getshow ";
            Label92.Attributes["class"] = "control-label col-md-2  gethide";
            lbl15.Attributes["class"] = "control-label col-md-2 getshow ";
            Label90.Attributes["class"] = "control-label col-md-2  gethide";
            lbl14.Attributes["class"] = "control-label col-md-2 getshow ";
            Label88.Attributes["class"] = "control-label col-md-2  gethide";
            lbl13.Attributes["class"] = "control-label col-md-2 getshow ";
            Label86.Attributes["class"] = "control-label col-md-2  gethide";
            lbl12.Attributes["class"] = "control-label col-md-2 getshow ";
            Label84.Attributes["class"] = "control-label col-md-2  gethide";
            lbl11.Attributes["class"] = "control-label col-md-2 getshow ";
            Label82.Attributes["class"] = "control-label col-md-4  gethide";
            lbl10.Attributes["class"] = "control-label col-md-4 getshow ";
            Label80.Attributes["class"] = "control-label col-md-4  gethide";
            lbl09.Attributes["class"] = "control-label col-md-4 getshow ";
            Label78.Attributes["class"] = "control-label col-md-4  gethide";
            lbl08.Attributes["class"] = "control-label col-md-4 getshow ";
            Label76.Attributes["class"] = "control-label col-md-4  gethide";
            lbl07.Attributes["class"] = "control-label col-md-4 getshow ";
            Label27.Attributes["class"] = "control-label col-md-4  gethide";
            lbl06.Attributes["class"] = "control-label col-md-4 getshow ";
            Label16.Attributes["class"] = "control-label col-md-4  gethide";
            lbl05.Attributes["class"] = "control-label col-md-4 getshow ";
            Label14.Attributes["class"] = "control-label col-md-4  gethide";
            lbl04.Attributes["class"] = "control-label col-md-4 getshow ";
            Label11.Attributes["class"] = "control-label col-md-2  gethide";
            lbl03.Attributes["class"] = "control-label col-md-2 getshow ";
            Label9.Attributes["class"] = "control-label col-md-2  gethide";
            lbl02.Attributes["class"] = "control-label col-md-2 getshow ";
            lbl01.Attributes["class"] = "control-label col-md-2 getshow  ";
            Label5.Attributes["class"] = "control-label col-md-2 gethide ";
        }
        public void redonlyfales()
        {
            txtAddress.Enabled = false;
            txtAddress2.Enabled = false;
            //txtCity.Enabled = false;
            drpcity.Enabled = false;
            txtContact2.Enabled = false;
            txtContact3.Enabled = false;
            txtContactName.Enabled = false;
            txtMobileNo.Enabled = false;
            txtPostalCode.Enabled = false;
            txtRemark.Enabled = false;
            txtSocial.Enabled = false;
            txtZipCode.Enabled = false;
            drpCompnay.Enabled = false;
            drpCountry.Enabled = false;
            drpItManager.Enabled = false;
            drpMyCounLocID.Enabled = false;
            drpSomib.Enabled = false;
            drpSates.Enabled = false;
            txtBarcode.Enabled = false;
            drpType.Enabled = false;
            //drpdatasource.Enabled = false;
            lkbContactName.Enabled = lkbContact2.Enabled = lkbContactnl3.Enabled = lkbEmail.Enabled = lkbFax.Enabled = lkbBusPhone.Enabled = lkbMobile.Enabled = LinkButton5.Enabled = false;
            lblBusContactDe.Text = "Business Contact Details -Read Mode";
            lblBCDeta.Text = "Business Contact Details -Read Mode";
            lblwExistance.Text = "Web Existance -Read Mode";
            lblWEmp.Text = "Working Company -Read Mode";
            ViewState["ModeData"] = "Read";
            txtPACI.Enabled = false;
            txtBirthdate.Enabled = txtCivilID.Enabled = false;
        }
        public void redonlyture()
        {
            txtAddress.Enabled = true;
            txtAddress2.Enabled = true;
            //txtCity.Enabled = true;
            drpcity.Enabled = true;
            txtContact2.Enabled = true;
            txtContact3.Enabled = true;
            txtContactName.Enabled = true;
            txtMobileNo.Enabled = true;
            txtPostalCode.Enabled = true;
            txtRemark.Enabled = true;
            txtSocial.Enabled = true;
            txtZipCode.Enabled = true;
            drpCompnay.Enabled = true;
            drpCountry.Enabled = true;
            drpItManager.Enabled = true;
            drpMyCounLocID.Enabled = true;
            drpSomib.Enabled = true;
            drpSates.Enabled = true;
            txtBarcode.Enabled = true;
            drpType.Enabled = true;
            //drpdatasource.Enabled = true;
            lkbContactName.Enabled = lkbContact2.Enabled = lkbContactnl3.Enabled = lkbEmail.Enabled = lkbFax.Enabled = lkbBusPhone.Enabled = lkbMobile.Enabled = LinkButton5.Enabled = true;
            lblBusContactDe.Text = "Business Contact Details -Write Mode";
            lblBCDeta.Text = "Business Contact Details -Write Mode";
            lblwExistance.Text = "Web Existance -Write Mode";
            lblWEmp.Text = "Working Company -Write Mode";
            ViewState["ModeData"] = "Write";
            txtPACI.Enabled = true;
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

                List<TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID).OrderByDescending(m => m.UPDTTIME).ToList();
                ViewState["SaveList"] = List;
                //Listview2.DataSource = List;
                //Listview2.DataBind();
                BindEditMode(List[0].ContactMyID);
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);

            }
            else
            {
                var List = ((List<Database.TBLCONTACT>)ViewState["SerchListofContect"]).ToList();
                List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();
                foreach (TBLCONTACT item in List)
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
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List1);
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

                List<TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y").OrderByDescending(m => m.UPDTTIME).ToList();
                ViewState["SaveList"] = List;
                //Listview2.DataSource = List;
                //Listview2.DataBind();
                BindEditMode(List[0].ContactMyID);
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);

            }
            else
            {
                var List = ((List<Database.TBLCONTACT>)ViewState["SerchListofContect"]).ToList();
                List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();
                foreach (TBLCONTACT item in List)
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
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List1);
                //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                //Listview2.DataBind();
            }

            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        }
        //public void BindData()
        //{
        //    Listview2.DataSource = DB.TBLCONTACT.Where(p => p.TenentID == TID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
        //    Listview2.DataBind();
        //}
        public void clen()
        {
            lblcountserch.Text = lblCustomer1.Text = lblCustomerL1.Text = lblCustomerL2.Text = lblMobileNo.Text = txtAddress.Text = txtAddress2.Text = txtContact2.Text = txtContact3.Text = txtContactName.Text = txtMobileNo.Text = txtPostalCode.Text = txtRemark.Text = txtZipCode.Text = tags_2.Text = tags_3.Text = tags_4.Text = txtBarcode.Text = txtPACI.Text = "";//txtCity.Text = 
            txtBirthdate.Text = txtCivilID.Text = "";
            drpCountry.SelectedIndex = 0;
            drpItManager.SelectedIndex = 0;
            drpMyCounLocID.SelectedIndex = 0;
            drpSates.SelectedIndex = 0;
            drpcity.SelectedIndex = 0;
            drpType.SelectedIndex = 0;
            //drpdatasource.SelectedIndex = 0;
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
                BindEditMode(ContactMyID);
                redonlyture();
                btnSubmit.Visible = true;
                Button1.Visible = false;
                lblAttecmentcount.Text = Countatt(ContactMyID);
                btnattmentmst.Text = "Attachment";
                btnattmentmst.Visible = true;
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                lblBusContactDe.Text = "Business Contact Details -Edit Mode";
                lblBCDeta.Text = "Business Contact Details -Edit Mode";
                lblwExistance.Text = "Web Existance -Edit Mode";
                lblWEmp.Text = "Working Company -Edit Mode";

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
                lblAttecmentcount.Text = Countatt(ContactMyID);
                btnattmentmst.Text = "Attachment";
                btnattmentmst.Visible = true;
                lblBusContactDe.Text = "Business Contact Details -Read Mode";
                lblBCDeta.Text = "Business Contact Details -Read Mode";
                lblwExistance.Text = "Web Existance -Read Mode";
                lblWEmp.Text = "Working Company -Read Mode";
            }
            if (e.CommandName == "btnActive")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                if (DB.TBLCONTACTs.Where(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
                {
                    Database.TBLCONTACT objTBLCONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y");
                    objTBLCONTACT.Active = "N";
                    DB.SaveChanges();
                }
                else
                {
                    Database.TBLCONTACT objTBLCONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "N");
                    objTBLCONTACT.Active = "Y";
                    DB.SaveChanges();
                }

                BindData();
            }
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
            if (objtbl_CONTACT.ITMANAGER != "" && objtbl_CONTACT.ITMANAGER != null)
                drpItManager.SelectedValue = objtbl_CONTACT.ITMANAGER;
            txtAddress.Text = objtbl_CONTACT.ADDR1;
            txtAddress2.Text = objtbl_CONTACT.ADDR2;
            DateTime BirthDate = Convert.ToDateTime(objtbl_CONTACT.BirthDate);
            txtBirthdate.Text = BirthDate.ToShortDateString();
            txtCivilID.Text = objtbl_CONTACT.CivilID;
            //txtCity.Text = objtbl_CONTACT.CITY;
            int COID = Convert.ToInt32(objtbl_CONTACT.COUNTRYID);
            drpCountry.SelectedValue = COID.ToString();
            bindSates(COID);
            if (objtbl_CONTACT.STATE != null && objtbl_CONTACT.STATE != "0" && objtbl_CONTACT.STATE != "")
            {
                drpSates.SelectedValue = objtbl_CONTACT.STATE;
            }
            txtPostalCode.Text = objtbl_CONTACT.POSTALCODE;
            txtZipCode.Text = objtbl_CONTACT.ZIPCODE;
            txtPostalCode.Text = objtbl_CONTACT.POSTALCODE;
            if (objtbl_CONTACT.BARCODE != null && objtbl_CONTACT.BARCODE != "")
            {
                txtBarcode.Text = objtbl_CONTACT.BARCODE;
            }

            if (objtbl_CONTACT.MYCONLOCID != null && objtbl_CONTACT.MYCONLOCID != 0)
            {
                drpMyCounLocID.SelectedValue = objtbl_CONTACT.MYCONLOCID.ToString();
            }

            if (objtbl_CONTACT.ContacType != null && Convert.ToInt32(objtbl_CONTACT.ContacType) != 0)
            {
                drpType.SelectedValue = (objtbl_CONTACT.ContacType).ToString();
            }
            else
            {
                drpType.SelectedValue = "82005".ToString();
            }

            //if (objtbl_CONTACT.datasource != 0 && objtbl_CONTACT.datasource != null)
            //{
            //    drpdatasource.SelectedValue = objtbl_CONTACT.datasource.ToString();
            //}
            //else
            //{
            //    drpdatasource.SelectedValue = "0";
            //}

            tags_2.Text = objtbl_CONTACT.EMAIL1;
            tags_3.Text = objtbl_CONTACT.FaxID;
            tags_4.Text = objtbl_CONTACT.BUSPHONE1;
            txtRemark.Text = objtbl_CONTACT.REMARKS;
            SoiclMediya(SID);
            BindWorkingCompany(SID);
            int state = Convert.ToInt32(drpSates.SelectedValue);
            bindcity(state);
            if (objtbl_CONTACT.CITY != null && objtbl_CONTACT.CITY != "0" && objtbl_CONTACT.CITY != "")
            {
                drpcity.SelectedValue = objtbl_CONTACT.CITY;
            }

            if (objtbl_CONTACT.IMG != null && objtbl_CONTACT.IMG != "0" && objtbl_CONTACT.IMG != "")
            {
                Avatar.ImageUrl = "/CRM/Upload/" + objtbl_CONTACT.IMG;
            }
            else
            {
                Avatar.ImageUrl = "/CRM/Upload/defolt.png";
            }
            if (objtbl_CONTACT.PACI != null)
            {
                txtPACI.Text = objtbl_CONTACT.PACI.ToString();
            }
        }

        public void bindcity(int State)
        {
            drpcity.DataSource = DB.tblCityStatesCounties.OrderBy(p => p.CityEnglish);
            drpcity.DataTextField = "CityEnglish";
            drpcity.DataValueField = "CityID";
            drpcity.DataBind();
            drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
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
                    objtblContact.FaxID = getFirstCommaseprate(tags_3.Text);
                    objtblContact.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);
                    objtblContact.ITMANAGER = drpItManager.SelectedValue;
                    objtblContact.ADDR1 = txtAddress.Text;
                    objtblContact.ADDR2 = txtAddress2.Text;
                    if (txtBirthdate.Text != "")
                    {
                        objtblContact.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    }
                    objtblContact.CivilID = txtCivilID.Text;
                    //objtblContact.CITY = txtCity.Text;
                    objtblContact.CITY = drpcity.SelectedValue;
                    objtblContact.STATE = drpSates.SelectedValue;
                    objtblContact.POSTALCODE = txtPostalCode.Text;
                    objtblContact.ZIPCODE = txtZipCode.Text;
                    objtblContact.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                    //  string CNAME = drpCountry.SelectedItem.ToString();
                    objtblContact.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtblContact.BUSPHONE1 = getFirstCommaseprate(tags_4.Text);
                    objtblContact.BARCODE = txtBarcode.Text;
                    objtblContact.Active = "Y";
                    objtblContact.REMARKS = txtRemark.Text;
                    objtblContact.UPDTTIME = DateTime.Now;
                    objtblContact.USERID = UID;
                    objtblContact.ContacType = Convert.ToInt32(drpType.SelectedValue);
                    objtblContact.PACI = txtPACI.Text;
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

                    // Edit Fax
                    if (tags_3.Text != "")
                    {
                        List<Tbl_RecordType_Mst> List1;
                        if (CID != 0)
                        {
                            List1 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Recource == 5006 && p.RecordType == "Fax").ToList();
                        }
                        else
                        {
                            List1 = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == ContactID && p.Recource == 5006 && p.RecordType == "Fax").ToList();
                        }

                        foreach (Database.Tbl_RecordType_Mst item in List1)
                        {

                            DB.Tbl_RecordType_Mst.DeleteObject(item);
                            DB.SaveChanges();
                        }

                        string[] Seperate1 = tags_3.Text.Split(',');
                        int count1 = 0;
                        string Sep1 = "";
                        for (int i = 0; i <= Seperate1.Count() - 1; i++)
                        {
                            Sep1 = Seperate1[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.RecordType == "Fax" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID);
                            if (exist.Count() < 1)
                            {
                                count1++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Fax";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = CMID;
                                obj.RunSerial = count1;
                                obj.Recource = 5006;
                                obj.RecourceName = "Contact";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                            }
                            else
                            {
                                if (DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.RecordType == "Fax" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID).Count() < 1)
                                {
                                    if (Sep1 != "00000")
                                    {
                                        string display = "Fax Number Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Fax Number Is Duplicate!", "alert('" + display + "');", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (tags_4.Text != "")
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

                        string[] Seperate2 = tags_4.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecordType == "BusPhone" && c.TenentID == TID && c.Recource == 5006 && c.CompniyAndContactID != ContactID);
                            if (exist.Count() < 1)
                            {
                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "BusPhone";
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
                                        string display = "Bus Number Is Duplicate!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "Bus Number Is Duplicate!", "alert('" + display + "');", true);
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

                    if (ViewState["ListtblCONTACTBus"] != null)
                    {
                        var Listbus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
                        for (int i = 0; i < Listbus.Count(); i++)
                        {
                            int Tenent = Convert.ToInt32(Listbus[i].TenentID);
                            int Contact = Convert.ToInt32(Listbus[i].ContactMyID);
                            int Compp = Convert.ToInt32(Listbus[i].CompID);
                            if (DB.tblCONTACTBus.Where(p => p.TenentID == Tenent && p.ContactMyID == Contact && p.CompID == Compp).Count() == 0)
                            {
                                Database.tblCONTACTBu objtbl_CONTACTBus = new Database.tblCONTACTBu();
                                objtbl_CONTACTBus.TenentID = Listbus[i].TenentID;
                                objtbl_CONTACTBus.ContactMyID = Listbus[i].ContactMyID;
                                objtbl_CONTACTBus.CompID = Listbus[i].CompID;
                                objtbl_CONTACTBus.PhysicalLocID = Listbus[i].PhysicalLocID;
                                objtbl_CONTACTBus.Country = Listbus[i].Country;
                                objtbl_CONTACTBus.JobTitle = Listbus[i].JobTitle;
                                objtbl_CONTACTBus.BusPhone1 = Listbus[i].BusPhone1;
                                objtbl_CONTACTBus.email2 = Listbus[i].email2;
                                objtbl_CONTACTBus.Fax = Listbus[i].Fax;
                                objtbl_CONTACTBus.PrimLanguge = Listbus[i].PrimLanguge;
                                objtbl_CONTACTBus.webpage = Listbus[i].webpage;
                                objtbl_CONTACTBus.IsSMB = Listbus[i].IsSMB;
                                objtbl_CONTACTBus.InHawally = Listbus[i].InHawally;
                                objtbl_CONTACTBus.emaiSub = Listbus[i].emaiSub;
                                objtbl_CONTACTBus.EmailSubDate = Listbus[i].EmailSubDate;
                                objtbl_CONTACTBus.saler = Listbus[i].saler;
                                objtbl_CONTACTBus.buyer = Listbus[i].buyer;
                                objtbl_CONTACTBus.saledeprod = Listbus[i].saledeprod;
                                objtbl_CONTACTBus.CUserID = Listbus[i].CUserID;
                                objtbl_CONTACTBus.Cpasswrd = Listbus[i].Cpasswrd;
                                objtbl_CONTACTBus.remarks = Listbus[i].remarks;
                                objtbl_CONTACTBus.Active = "Y";
                                DB.tblCONTACTBus.AddObject(objtbl_CONTACTBus);
                                DB.SaveChanges();
                            }
                        }
                        ViewState["ListtblCONTACTBus"] = null;
                    }
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);

                    scope.Complete();

                    Response.Redirect("ContactMaster.aspx");
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
                    objtblContact.FaxID = getFirstCommaseprate(tags_3.Text);
                    objtblContact.MOBPHONE = getFirstCommaseprate(txtMobileNo.Text);
                    objtblContact.ITMANAGER = drpItManager.SelectedValue;
                    objtblContact.ADDR1 = txtAddress.Text;
                    objtblContact.ADDR2 = txtAddress2.Text;
                    if (txtBirthdate.Text != "")
                    {
                        objtblContact.BirthDate = Convert.ToDateTime(txtBirthdate.Text);
                    }

                    objtblContact.CivilID = txtCivilID.Text;
                    //objtblContact.CITY = txtCity.Text;
                    objtblContact.CITY = drpcity.SelectedValue;
                    objtblContact.STATE = drpSates.SelectedValue;
                    objtblContact.POSTALCODE = txtPostalCode.Text;
                    objtblContact.ZIPCODE = txtZipCode.Text;
                    objtblContact.MYCONLOCID = Convert.ToInt32(drpMyCounLocID.SelectedValue);
                    // string CNAME = drpCountry.SelectedItem.ToString();
                    objtblContact.COUNTRYID = Convert.ToInt32(drpCountry.SelectedValue);
                    objtblContact.BUSPHONE1 = getFirstCommaseprate(tags_4.Text);
                    objtblContact.BARCODE = txtBarcode.Text;
                    objtblContact.Active = "Y";
                    objtblContact.REMARKS = txtRemark.Text;
                    objtblContact.ENTRYTIME = DateTime.Now;
                    objtblContact.UPDTTIME = DateTime.Now;
                    objtblContact.USERID = UID;
                    objtblContact.ContacType = Convert.ToInt32(drpType.SelectedValue);
                    objtblContact.PACI = txtPACI.Text;
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
                    if (tags_3.Text != "")
                    {
                        string[] Seperate1 = tags_3.Text.Split(',');
                        int count1 = 0;
                        string Sep1 = "";
                        for (int i = 0; i <= Seperate1.Count() - 1; i++)
                        {
                            Sep1 = Seperate1[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.RecordType == "Fax" && c.RecourceName == "Contact" && c.TenentID == TID && c.Recource == 5006);
                            if (exist.Count() <= 0)
                            {
                                count1++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "Fax";
                                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = CMID;
                                obj.RunSerial = count1;
                                obj.Recource = 5006;
                                obj.RecourceName = "Contact";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Tbl_RecordType_Mst.AddObject(obj);
                                DB.SaveChanges();

                            }
                            else
                            {
                                string display = "Fax Number Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Fax Number Is Duplicate!", "alert('" + display + "');", true);
                                return;
                            }
                        }
                    }
                    if (tags_4.Text != "")
                    {
                        string[] Seperate2 = tags_4.Text.Split(',');
                        int count2 = 0;
                        string Sep2 = "";
                        for (int i = 0; i <= Seperate2.Count() - 1; i++)
                        {
                            Sep2 = Seperate2[i];
                            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.RecordType == "BusPhone" && c.RecourceName == "Contact" && c.TenentID == TID && c.Recource == 5005);
                            if (exist.Count() <= 0)
                            {
                                count2++;
                                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                                obj.TenentID = TID;
                                obj.RecordType = "BusPhone";
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
                                string display = "Bus Number Is Duplicate!";
                                ClientScript.RegisterStartupScript(this.GetType(), "Bus Number Is Duplicate!", "alert('" + display + "');", true);
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

                    if (ViewState["ListtblCONTACTBus"] != null)
                    {
                        var Listbus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
                        for (int i = 0; i < Listbus.Count(); i++)
                        {
                            int Tenent = Convert.ToInt32(Listbus[i].TenentID);
                            int Contact = Convert.ToInt32(Listbus[i].ContactMyID);
                            int Compp = Convert.ToInt32(Listbus[i].CompID);
                            if (DB.tblCONTACTBus.Where(p => p.TenentID == Tenent && p.ContactMyID == Contact && p.CompID == Compp).Count() == 0)
                            {
                                Database.tblCONTACTBu objtbl_CONTACTBus = new Database.tblCONTACTBu();
                                objtbl_CONTACTBus.TenentID = Listbus[i].TenentID;
                                objtbl_CONTACTBus.ContactMyID = Listbus[i].ContactMyID;
                                objtbl_CONTACTBus.CompID = Listbus[i].CompID;
                                objtbl_CONTACTBus.PhysicalLocID = Listbus[i].PhysicalLocID;
                                objtbl_CONTACTBus.Country = Listbus[i].Country;
                                objtbl_CONTACTBus.JobTitle = Listbus[i].JobTitle;
                                objtbl_CONTACTBus.BusPhone1 = Listbus[i].BusPhone1;
                                objtbl_CONTACTBus.email2 = Listbus[i].email2;
                                objtbl_CONTACTBus.Fax = Listbus[i].Fax;
                                objtbl_CONTACTBus.PrimLanguge = Listbus[i].PrimLanguge;
                                objtbl_CONTACTBus.webpage = Listbus[i].webpage;
                                objtbl_CONTACTBus.IsSMB = Listbus[i].IsSMB;
                                objtbl_CONTACTBus.InHawally = Listbus[i].InHawally;
                                objtbl_CONTACTBus.emaiSub = Listbus[i].emaiSub;
                                objtbl_CONTACTBus.EmailSubDate = Listbus[i].EmailSubDate;
                                objtbl_CONTACTBus.saler = Listbus[i].saler;
                                objtbl_CONTACTBus.buyer = Listbus[i].buyer;
                                objtbl_CONTACTBus.saledeprod = Listbus[i].saledeprod;
                                objtbl_CONTACTBus.CUserID = Listbus[i].CUserID;
                                objtbl_CONTACTBus.Cpasswrd = Listbus[i].Cpasswrd;
                                objtbl_CONTACTBus.remarks = Listbus[i].remarks;

                                objtbl_CONTACTBus.Active = "Y";
                                DB.tblCONTACTBus.AddObject(objtbl_CONTACTBus);
                                DB.SaveChanges();
                            }
                        }
                        ViewState["ListtblCONTACTBus"] = null;
                    }
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);

                }
                BindData();
                btnSubmit.Visible = false;
                Button1.Visible = true;
                redonlyfales();
                //  clen();
                btnattmentmst.Visible = false;
                txtCustoID.Enabled = false;
                scope.Complete(); //  To commit.
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
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

        protected void btnFax_Click(object sender, EventArgs e)
        {
            Label21.Text = "";
            string CNAME = drpCountry.SelectedItem.Text;
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            int Flenth = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.COUNTRYID == CID && p.TenentID == TID).FaxLength);
            string[] Seperate1 = tags_3.Text.Split('/');

            string Sep1 = "";
            for (int i = 0; i <= Seperate1.Count() - 1; i++)
            {
                Sep1 = Seperate1[i];
                if (Sep1.Length == Flenth)
                {

                    var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep1 && c.TenentID == TID);
                    if (exist.Count() <= 0)
                    {



                    }
                    else
                    {
                        int compname = DB.Tbl_RecordType_Mst.Single(c => c.RecValue == Sep1 && c.TenentID == TID).CompniyAndContactID;
                        Label21.Text = "Fax Number Is Duplicate and It's allready used for this  " + DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == compname && p.TenentID == TID).COMPNAME;
                    }
                }
                else
                {
                    Label21.Text = CNAME + "Country is Maximun And Minimum " + Flenth + " Digit Requried " + Sep1;
                }
            }
        }

        protected void btnBusPhone_Click(object sender, EventArgs e)
        {
            Label22.Text = "";
            string CNAME = drpCountry.SelectedItem.Text;
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            int Flenth = Convert.ToInt32(DB.tblCOUNTRies.Single(p => p.COUNTRYID == CID && p.TenentID == TID).TelLength);
            string[] Seperate2 = tags_4.Text.Split('/');

            string Sep2 = "";
            for (int i = 0; i <= Seperate2.Count() - 1; i++)
            {
                Sep2 = Seperate2[i];
                if (Sep2.Length == Flenth)
                {

                    var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == Sep2 && c.TenentID == TID);
                    if (exist.Count() <= 0)
                    {



                    }
                    else
                    {
                        int compname = DB.Tbl_RecordType_Mst.Single(c => c.RecValue == Sep2 && c.TenentID == TID).CompniyAndContactID;
                        Label22.Text = "Bus Phone Is Duplicate and It's allready used for this " + DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == compname && p.TenentID == TID).COMPNAME;
                    }
                }
                else
                {
                    Label22.Text = CNAME + "Country is Maximun And Minimum " + Flenth + " Digit Requried " + Sep2;
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
                lblCustomer1.Text = "Contact Name 1 Is Duplicate";
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
                lblCustomer1.Text = "Contact Name 1 Is Duplicate";
            }
        }

        protected void btnMobile_Click(object sender, EventArgs e)
        {
            lblMobileNo.Text = "";
            string CNAME = drpCountry.SelectedItem.Text;
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
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
            txtCustoID.Enabled = false;
            if (btnCancel.Text == "Exit")
            {
                if (ViewState["CCID"] != null)
                {
                    int CCID = Convert.ToInt32(ViewState["CCID"]);
                    Response.Redirect("CompanyMaster.aspx?NewContect=Show&COMPID=" + CCID);

                }
                else
                {
                    Response.Redirect("CompanyMaster.aspx?NewContect=Show");
                }
            }
            else
            {
                clen();
                LastData();
                redonlyfales();
                btnSubmit.Visible = false;
                Button1.Visible = true;
                btnattmentmst.Visible = false;
            }
            Response.Redirect("ContactMaster.aspx");

        }


        public string getStste(int SID)
        {
            //if (SID != 0)
            //    return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == SID && p.TenentID == TID).CITY;
            //else
            //    return "";
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == SID && p.TenentID == TID).Count() > 0)
            {
                //return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID).CITY;
                Database.TBLCOMPANYSETUP COBJ = DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == SID && p.TenentID == TID);
                int Cityidd = Convert.ToInt32(COBJ.CITY);
                int Statett = Convert.ToInt32(COBJ.STATE);
                int countrry = Convert.ToInt32(COBJ.COUNTRYID);
                List<Database.tblCityStatesCounty> CityList = DB.tblCityStatesCounties.Where(p => p.CityID == Cityidd && p.StateID == Statett && p.COUNTRYID == countrry).ToList();
                if (CityList.Count() == 1)
                {
                    return CityList[0].CityEnglish;
                }
                else
                {
                    return drpcity.SelectedItem.ToString();
                }
            }
            else
            {
                return drpcity.SelectedItem.ToString();// txtCity.Text;
            }
        }
        public void FillContractorID()
        {
            drpCompnay.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpType, TID, "COMP", "COMPTYPE", "", "REFTABLE");

            Classes.EcommAdminClass.getdropdown(drpMyCounLocID, TID, "", "", "", "TBLCOMPANY_LOCATION");
            //select * from TBLCOMPANY_LOCATION where Active= 'Y'

            //drpMyCounLocID.DataSource = DB.TBLCOMPANY_LOCATION.Where(P => P.TenentID == TID && P.ACTIVE == "Y");
            //drpMyCounLocID.DataTextField = "LOCATION_NAME1";
            //drpMyCounLocID.DataValueField = "LOCATIONID";
            //drpMyCounLocID.DataBind();
            //drpMyCounLocID.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpCountry, TID, "", "", "", "tblCOUNTRY");
            //select * from tblCOUNTRY where Active= 'Y' 

            //drpCountry.DataSource = DB.tblCOUNTRies.Where(p => p.Active == "Y");
            //drpCountry.DataTextField = "COUNAME1";
            //drpCountry.DataValueField = "COUNTRYID";
            //drpCountry.DataBind();
            //drpCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpSates.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpcity.Items.Insert(0, new ListItem("-- Select City--", "0"));

            Classes.EcommAdminClass.getdropdown(drpItManager, TID, "", "", "", "Tbl_Position_Mst");
            //select * from Tbl_Position_Mst where Active= 'true' and TenentID = 1

            //drpItManager.DataSource = DB.Tbl_Position_Mst.Where(P => P.TenentID == TID && P.Active == true);
            //drpItManager.DataTextField = "PositionName";
            //drpItManager.DataValueField = "PositionID";
            //drpItManager.DataBind();
            //drpItManager.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpSomib, TID, "social", "media", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'social'  and REFSUBTYPE= 'media'

            //drpSomib.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "social" && P.REFSUBTYPE == "media" && P.ACTIVE == "Y");
            //drpSomib.DataTextField = "REFNAME1";
            //drpSomib.DataValueField = "REFID";
            //drpSomib.DataBind();
            //drpSomib.Items.Insert(0, new ListItem("-- Select --", "0"));

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

            //drpdatasource.DataSource = DB.TBLCONTACTs.Where(p => p.TenentID == TID).OrderBy(p => p.PersName1);
            //drpdatasource.DataTextField = "PersName1";
            //drpdatasource.DataValueField = "ContactMyID";
            //drpdatasource.DataBind();
            //drpdatasource.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        public void BindTitleData()
        {
            //DrpTitle.SelectedItem = 

            string REFtype = Convert.ToString(SearchManagement.SearchType.Search);
            string REFSubtype = Convert.ToString(SearchManagement.SearchType.Contact);
            DrpTitle.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == REFtype && P.REFSUBTYPE == REFSubtype && P.TenentID == TID && P.ACTIVE == "Y").OrderBy(p => p.REFNAME1);
            DrpTitle.DataTextField = "REFNAME1";
            DrpTitle.DataValueField = "REFID";
            DrpTitle.DataBind();
            DrpTitle.Items.Insert(0, new ListItem("-- Select --", "0"));
            DrpTitle.Items.Insert(1, new ListItem("Show All", "999999"));
            DrpTitle.SelectedValue = "999999";
        }



        protected void btnYes_Click(object sender, EventArgs e)
        {
            decimal CID = Convert.ToDecimal(ViewState["compId"]);
            btnSubmit.Text = "Update";
            BindEditMode(CID);
            updCountry.Update();
            redonlyture();

            lblBusContactDe.Text = "Business Contact Details -Edit Mode";
            lblBCDeta.Text = "Business Contact Details -Edit Mode";
            lblwExistance.Text = "Web Existance -Edit Mode";
            lblWEmp.Text = "Working Company -Edit Mode";

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
                    updCountry.Update();
                }
                else
                {
                    var UserList = DB.TBLCONTACTs.SingleOrDefault(p => p.PersName1 == txtContactName.Text && p.Active == "Y" && p.TenentID == TID);
                    ViewState["compId"] = UserList.ContactMyID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
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
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
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
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
                    //  ModalPopupExtender4.TargetControlID = btntest4.ID;
                    ModalPopupExtender4.Show();
                    //BindData1();
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

        protected void btnSocial_Click(object sender, EventArgs e)
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


            int RTID = Convert.ToInt32(drpSomib.SelectedValue);
            var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == txtSocial.Text && c.Recource == RTID && c.TenentID == TID);
            if (exist.Count() <= 0)
            {
                Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                obj.TenentID = TID;
                obj.RecordType = "socialmedia";
                obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                obj.CompniyAndContactID = CMID;
                obj.RecourceName = "Contact";
                obj.Recource = Convert.ToInt32(drpSomib.SelectedValue);
                obj.RunSerial = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CMID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CMID && p.TenentID == TID).Max(p => p.RunSerial) + 1) : 1; ; ;
                obj.RecValue = txtSocial.Text;
                obj.Active = true;
                // obj.Rremark = "AutomatedProcess";
                DB.Tbl_RecordType_Mst.AddObject(obj);
                DB.SaveChanges();
                SoiclMediya(CMID);



                //Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                //obj.TenentID = TID;
                //obj.RecordType = "socialmedia";
                //obj.RecTypeID = Convert.ToInt32(drpSomib.SelectedValue);
                //obj.CompniyAndContactID = CMID;
                //obj.RunSerial = DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CMID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.CompniyAndContactID == CMID).Max(p => p.RunSerial) + 1) : 1; ;
                //obj.Recource = 5006;
                //obj.RecourceName = "Contact";
                //obj.RecValue = txtSocial.Text;
                //obj.Active = true;
                //// obj.Rremark = "AutomatedProcess";
                //DB.Tbl_RecordType_Mst.AddObject(obj);
                //DB.SaveChanges();
                //SoiclMediya(CMID);

            }

            else
            {
                string display = "Social Media Is Duplicate!";
                panelMsg.Visible = true;
                lblErreorMsg.Text = display;
                return;
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
        public string workingcompany(int ID)
        {
            List<Database.TBLCOMPANYSETUP> CompList = DB.TBLCOMPANYSETUPs.Where(o => o.TenentID == TID && o.COMPID == ID).ToList();
            if (CompList.Count() > 0)
            {
                return CompList.SingleOrDefault(o => o.TenentID == TID && o.COMPID == ID).COMPNAME1;
            }
            else
            {
                return "Not Found";
            }
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
        public void SoiclMediya(int CID)
        {
            listSocialMedia.DataSource = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.RecourceName == "Contact" && p.CompniyAndContactID == CID && p.Active == true && p.RecordType == "socialmedia");
            listSocialMedia.DataBind();
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
        }

        protected void drpCompnay_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(drpCompnay.SelectedValue);
            bindContectcompniy(CID);

        }
        public void bindContectcompniy(int CID)
        {
            ListView1.DataSource = DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.Active == "Y" && p.CompID == CID);
            ListView1.DataBind();
        }
        protected void btnCompniyadd_Click(object sender, EventArgs e)
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
                CMID = DB.tblCONTACTBus.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblCONTACTBus.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;

            int CID123 = Convert.ToInt32(drpCompnay.SelectedValue);
            var obj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID123);
            if (ViewState["ListtblCONTACTBus"] != null)
            {
                ListtblCONTACTBus = (List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"];
            }
            tblCONTACTBu LIst = new tblCONTACTBu();
            //  Database.tblCONTACTBus objtbl_CONTACTBus = new Database.tblCONTACTBus();
            LIst.TenentID = TID;
            LIst.ContactMyID = CMID;
            LIst.CompID = CID123;
            LIst.PhysicalLocID = obj.PHYSICALLOCID;
            LIst.Country = obj.COUNTRYID.ToString();
            LIst.JobTitle = drpItManager.SelectedValue;
            LIst.BusPhone1 = obj.BUSPHONE1;
            LIst.email2 = obj.EMAIL;
            LIst.Fax = obj.FAX1;
            LIst.PrimLanguge = obj.PRIMLANGUGE;
            LIst.webpage = obj.WEBPAGE;
            LIst.IsSMB = obj.ISSMB;
            LIst.InHawally = obj.INHAWALLY;
            LIst.emaiSub = obj.EMAISUB;
            LIst.EmailSubDate = obj.EMAILSUBDATE;
            LIst.saler = obj.SALER;
            LIst.buyer = obj.BUYER;
            LIst.saledeprod = obj.SALEDEPROD;
            LIst.CUserID = obj.CUSERID;
            LIst.Cpasswrd = obj.CPASSWRD;
            LIst.remarks = obj.REMARKS;
            LIst.Active = "Y";
            ListtblCONTACTBus.Add(LIst);
            ViewState["ListtblCONTACTBus"] = ListtblCONTACTBus;
            BindtimeData();


            //Database.tblCONTACTBus objtbl_CONTACTBus = new Database.tblCONTACTBus();
            //objtbl_CONTACTBus.TenentID = TID;
            //objtbl_CONTACTBus.ContactMyID = CID123;
            //objtbl_CONTACTBus.CompID = CID;
            //objtbl_CONTACTBus.PhysicalLocID = obj.PHYSICALLOCID;
            //objtbl_CONTACTBus.Country = obj.COUNTRYID.ToString();
            //objtbl_CONTACTBus.JobTitle = drpItManager.SelectedValue;
            //objtbl_CONTACTBus.BusPhone1 = obj.BUSPHONE1;
            //objtbl_CONTACTBus.email2 = obj.EMAIL;
            //objtbl_CONTACTBus.Fax = obj.FAX1;
            //objtbl_CONTACTBus.PrimLanguge = obj.PRIMLANGUGE;
            //objtbl_CONTACTBus.webpage = obj.WEBPAGE;
            //objtbl_CONTACTBus.IsSMB = obj.ISSMB;
            //objtbl_CONTACTBus.InHawally = obj.INHAWALLY;
            //objtbl_CONTACTBus.emaiSub = obj.EMAISUB;
            //objtbl_CONTACTBus.EmailSubDate = obj.EMAILSUBDATE;
            //objtbl_CONTACTBus.saler = obj.SALER;
            //objtbl_CONTACTBus.buyer = obj.BUYER;
            //objtbl_CONTACTBus.saledeprod = obj.SALEDEPROD;
            //objtbl_CONTACTBus.CUserID = obj.CUSERID;
            //objtbl_CONTACTBus.Cpasswrd = obj.CPASSWRD;
            //objtbl_CONTACTBus.remarks = obj.REMARKS;
            //objtbl_CONTACTBus.Active = "Y";
            //DB.tblCONTACTBus.AddObject(objtbl_CONTACTBus);
            //DB.SaveChanges();
            //bindContectcompniy(CID123);
        }
        public void BindtimeData()
        {
            ListtblCONTACTBus = ((List<Database.tblCONTACTBu>)ViewState["ListtblCONTACTBus"]).ToList();
            ListView1.DataSource = ListtblCONTACTBus.Where(p => p.TenentID == TID);
            ListView1.DataBind();
        }
        public void BindWorkingCompany(int ID)
        {
            List<Database.tblCONTACTBu> CompanyList = DB.tblCONTACTBus.Where(p => p.TenentID == TID && p.ContactMyID == ID).ToList();
            ListView1.DataSource = CompanyList;
            ListView1.DataBind();
        }

        protected void btnSearchSave_Click(object sender, EventArgs e)
        {
            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            string RefSubType = Convert.ToString(SearchManagement.SearchType.Contact.ToString());

            if (Convert.ToInt32(DrpTitle.SelectedValue) == 0)
            {
                if (Listview2.Items.Count() > 0)
                {
                    if (txtTitle.Text != "")
                    {
                        List<Database.REFTABLE> List_Ref = DB.REFTABLEs.ToList();
                        if (List_Ref.Where(p => p.REFNAME1 == txtTitle.Text && p.REFTYPE == RefType && p.REFSUBTYPE == RefSubType && p.TenentID == TID).Count() < 1)
                        {
                            int RID = List_Ref.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(List_Ref.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                            int Con = 0;
                            bool flag = false;
                            List<TBLCONTACT> LISTSerch = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
                            for (int i = 0; i < LISTSerch.Count(); i++)
                            {
                                Con = Convert.ToInt32(LISTSerch[i].ContactMyID);
                                ISSearchDetail obj_Detail = new ISSearchDetail();
                                obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                obj_Detail.TenentID = TID;
                                obj_Detail.LocationID = LID;
                                obj_Detail.REFID = RID;
                                obj_Detail.ContactID = Con;
                                obj_Detail.CreatedBy = UID;
                                obj_Detail.Active = true;
                                obj_Detail.Deleted = true;
                                if (DB.ISSearchDetails.Where(p => p.ContactID == Con && p.CreatedBy == UID && p.REFID == RID && p.TenentID == TID).Count() < 1)
                                {
                                    DB.ISSearchDetails.AddObject(obj_Detail);
                                    DB.SaveChanges();
                                    flag = true;
                                }

                            }
                            //foreach (ListViewItem item in Listview2.Items)
                            //{
                            //    HiddenField HID = (HiddenField)item.FindControl("hidecontactid");
                            //    Con = Convert.ToInt32(HID.Value);
                            //    ISSearchDetail obj_Detail = new ISSearchDetail();
                            //    obj_Detail.ID = DB.ISSearchDetail.Count() > 0 ? Convert.ToInt32(DB.ISSearchDetail.Max(p => p.ID) + 1) : 1;
                            //    obj_Detail.TenentID =TID;
                            //    obj_Detail.LocationID = 1;
                            //    obj_Detail.REFID = RID;
                            //    obj_Detail.ContactCompanyID = Con;
                            //    obj_Detail.CreatedBy = UID;
                            //    obj_Detail.Active = true;
                            //    obj_Detail.Deleted = true;
                            //    if (DB.ISSearchDetail.Where(p => p.ContactCompanyID == Con && p.CreatedBy == UID).Count() < 1)
                            //    {
                            //        DB.ISSearchDetail.AddObject(obj_Detail);
                            //        DB.SaveChanges();
                            //    }
                            //    else
                            //    {
                            //        flag = true;
                            //    }

                            //}
                            if (flag == true)
                            {
                                Database.REFTABLE obj_Ref = new Database.REFTABLE();
                                obj_Ref.TenentID = TID;
                                obj_Ref.REFID = RID;
                                obj_Ref.REFTYPE = RefType;
                                obj_Ref.REFSUBTYPE = RefSubType;
                                obj_Ref.SHORTNAME = RefType;
                                obj_Ref.REFNAME1 = txtTitle.Text;
                                obj_Ref.REFNAME2 = txtTitle.Text;
                                obj_Ref.REFNAME3 = txtTitle.Text;
                                obj_Ref.ACTIVE = "Y";
                                DB.REFTABLEs.AddObject(obj_Ref);
                                DB.SaveChanges();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data Save Sucessfully...');", true);
                            }

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Title Name Already Exist...');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Must Enter Title...');", true);

                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('No Search Found...');", true);
                }
            }
            BindTitleData();
            txtTitle.Text = "";
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int title = Convert.ToInt32(DrpTitle.SelectedValue);
            //Listview2
            SetDefaultGrid();
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);
            Database.CRMUserSetup obj = null;
            if (chDefaultSet.Checked)
            {
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.StartupContactRefID != 0 && p.userid == UID).Count() > 0)
                {
                    obj = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.StartupContactRefID != 0 && p.userid == UID);
                    obj.StartupContactRefID = ID;
                    obj.Active = true;

                }
                else
                {
                    obj = new CRMUserSetup();
                    obj.TenentID = TID;
                    obj.userid = UID;
                    obj.StartupContactRefID = ID;
                    obj.startupCompRefID = 0;
                    obj.DateTime = DateTime.Now;
                    obj.Active = true;
                    DB.CRMUserSetups.AddObject(obj);

                }
                DB.SaveChanges();
            }
            else
            {
                if (DB.CRMUserSetups.Where(p => p.TenentID == TID && p.userid == UID && p.StartupContactRefID != 0).Count() > 0)
                {
                    obj = DB.CRMUserSetups.Single(p => p.TenentID == TID && p.userid == UID && p.StartupContactRefID != 0);
                    obj.Active = false;
                    DB.SaveChanges();
                }
            }
        }
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(DrpTitle.SelectedValue) != 0)
        //    {
        //        int TitleID = Convert.ToInt32(DrpTitle.SelectedValue);

        //        List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
        //        List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();

        //        foreach (ISSearchDetail item in Search_List)
        //        {
        //            Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactID && p.TenentID == TID);
        //            Con_List.Add(obj_Contact);
        //        }
        //        //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
        //        //Listview2.DataBind();
        //        var List = Con_List;
        //        ViewState["SaveList"] = List;
        //        ViewState["SerchListofContect"] = List;
        //        //Listview2.DataSource = List;
        //        //Listview2.DataBind();
        //        int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //        int Totalrec = List.Count();
        //        ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        //    }
        //}

        protected void lkbCustomerN1_Click(object sender, EventArgs e)
        {
            string id1 = txtcompneySerch.Text.ToString();

            List<TBLCOMPANYSETUP> list1 = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.COMPID).ToList();
            drpCompnay.DataSource = list1.OrderBy(p => p.COMPNAME1);
            drpCompnay.DataTextField = "COMPNAME1";
            drpCompnay.DataValueField = "COMPID";
            drpCompnay.DataBind();
            drpCompnay.Items.Insert(0, new ListItem("-- Select --", "0"));
            int Count = list1.Count();
            if (Count > 0)
                lblcountserch.Text = Count + " Records Founds from Drop Down";
            else
                lblcountserch.Text = " No Records Founds";
        }
        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void drpSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chkactive.Checked == true)
            {
                var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
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
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);

            }
            else
            {
                var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
                if (drpSort.SelectedValue == "1")
                    List = List.OrderBy(m => m.PersName1).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "2")
                    List = List.OrderBy(m => m.ADDR1).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "3")
                    List = List.OrderBy(m => m.EMAIL1).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "4")
                    List = List.OrderBy(m => m.MOBPHONE).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "5")
                    List = List.OrderBy(m => m.STATE).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "6")
                    List = List.OrderBy(m => m.ZIPCODE).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "7")
                    List = List.OrderBy(m => m.CITY).Where(p => p.TenentID == TID).ToList();
                if (drpSort.SelectedValue == "8")
                    List = List.OrderBy(m => m.REMARKS).Where(p => p.TenentID == TID).ToList();
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);

            }
            
        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
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

            ((CRMMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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

            ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = List.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((CRMMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //if (ViewState["Take"] != null && ViewState["Skip"] != null)
            //{
            //    int Totalrec = DB.TBLCONTACTs.Count();
            //    take = Showdata;
            //    Skip = 0;
            //    ((CRMMaster)Page.Master).BindList(Listview2, (DB.TBLCONTACTs.OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
            //    ViewState["Take"] = take;
            //    ViewState["Skip"] = Skip;
            //    btnPrevious1.Enabled = false;
            //    ChoiceID = 0;
            //    ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
            //    if (take == Totalrec && Skip == (Totalrec - Showdata))
            //        btnNext1.Enabled = false;
            //    else
            //        btnNext1.Enabled = true;
            //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            // }
        }
        protected void btnfirst1_Click(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = List.Count();
                take = Showdata;
                Skip = 0;
                ((CRMMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((CRMMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((CRMMaster)Page.Master).BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
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
            List<TBLCONTACT> List = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.PersName2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID && p.Active == "Y").OrderBy(p => p.ContactMyID).ToList();
            ViewState["SaveList"] = List;
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
            ViewState["SerchListTabel"] = List;
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactSearch.aspx");
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

            if (Request.QueryString["Type"] != null)
            {
                string Comptype = Request.QueryString["Type"].ToString();
                drpType.SelectedValue = Comptype;
                drpType.Enabled = false;
            }
            else
            {
                drpType.SelectedValue = "82003";
                drpType.Enabled = true;
            }

            Avatar.ImageUrl = "~/Gallery/defolt.png";
        }
        protected void btnAppend_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);

            List<TBLCONTACT> LISTSerch = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            for (int i = 0; i < LISTSerch.Count; i++)
            {

                int Con = Convert.ToInt32(LISTSerch[i].ContactMyID);
                Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                obj_Detail.TenentID = TID;
                obj_Detail.LocationID = 1;
                obj_Detail.REFID = ID;
                obj_Detail.ContactID = Con;
                obj_Detail.CreatedBy = UID;
                obj_Detail.Active = true;
                obj_Detail.Deleted = true;
                if (DB.ISSearchDetails.Where(p => p.ContactID == Con && p.CreatedBy == UID && p.REFID == ID && p.TenentID == TID).Count() < 1)
                {
                    DB.ISSearchDetails.AddObject(obj_Detail);
                    DB.SaveChanges();
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Append Successfully...');", true);
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

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnshare")
            {
                int Contectid = Convert.ToInt32(e.CommandArgument);
                int CompnyID = Convert.ToInt32(drpCompnay.SelectedValue);
                string Mode = ViewState["ModeData"].ToString();
                string URL = "AttachmentMst.aspx?AttachID=" + Contectid + "&DID=Contact&Mode=" + Mode + "&recodInsert=" + CompnyID;
                Response.Redirect(URL);
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            bindSates(CID);

            drpcity.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == CID).OrderBy(p => p.CityEnglish);
            drpcity.DataTextField = "CityEnglish";
            drpcity.DataValueField = "CityID";
            drpcity.DataBind();
            drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        public void bindSates(int CID)
        {
            string Catid = CID.ToString();
            Classes.EcommAdminClass.getdropdown(drpSates, TID, Catid, "", "", "tblStates");

        }
        public void SetDefaultGrid()
        {
            if (Convert.ToInt32(DrpTitle.SelectedValue) == 999999)
            {
                BindData();
            }
            else if (Convert.ToInt32(DrpTitle.SelectedValue) != 0)
            {
                int TitleID = Convert.ToInt32(DrpTitle.SelectedValue);


                //var Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                //List<Database.TBLCONTACT> Con_List = DB.TBLCONTACTs.Where(p => p.ISSearchDetails.Any(a => a.ContactID == item.ContactMyID && a.REFID == TitleID && a.TenentID == TID) && p.TenentID == TID && p.Active == "Y").ToList();



                List<Database.TBLCONTACT> Con_List = new List<TBLCONTACT>();

                List<Database.TBLCONTACT> listcompany = DB.TBLCONTACTs.Where(p => p.TenentID == TID).ToList();

                foreach (Database.TBLCONTACT item in listcompany)
                {
                    List<Database.ISSearchDetail> listissearch = DB.ISSearchDetails.Where(p => p.ContactID == item.ContactMyID && p.REFID == TitleID && p.TenentID == TID).ToList();
                    foreach (Database.ISSearchDetail itemsearch in listissearch)
                    {
                        if (itemsearch.ContactID == item.ContactMyID)
                        {
                            Database.TBLCONTACT objcomp = item;
                            Con_List.Add(objcomp);
                        }
                    }
                }



                foreach (ISSearchDetail item in Search_List)
                {

                    Database.TBLCONTACT cust = DB.TBLCONTACTs.Where(p => p.ContactMyID == item.ContactID && p.TenentID == TID).FirstOrDefault();
                    if (cust != null)
                    {
                        Database.TBLCONTACT obj_Contact = cust;
                        Con_List.Add(obj_Contact);
                    }

                }

                ViewState["SaveList"] = Con_List;
                //  ViewState["SerchListofCompny"] = List;
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = Con_List.Count();
                if (Totalrec > 0)
                    BindEditMode(Con_List[0].ContactMyID);
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, Con_List);
                //Listview1.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                //Listview1.DataBind();

            }

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

        protected void drpSates_SelectedIndexChanged(object sender, EventArgs e)
        {
            int state = Convert.ToInt32(drpSates.SelectedValue);
            drpcity.DataSource = DB.tblCityStatesCounties.Where(p => p.StateID == state).OrderBy(p => p.CityEnglish);
            drpcity.DataTextField = "CityEnglish";
            drpcity.DataValueField = "CityID";
            drpcity.DataBind();
            drpcity.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void drpcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Country = Convert.ToInt32(drpCountry.SelectedValue);
            int City = Convert.ToInt32(drpcity.SelectedValue);

            if (Country != 0)
            {
                drpSates.SelectedValue = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == Country && p.CityID == City).FirstOrDefault().StateID.ToString();
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
        List<Database.TBLCONTACT> ListTBLCONTACT = new List<TBLCONTACT>();
        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (ListTBLCONTACT.Count() < 1)
            {
                ListTBLCONTACT = DB.TBLCONTACTs.Where(p => p.TenentID == TID).ToList();

            }

            LinkButton lnkbtnActive = (LinkButton)e.Item.FindControl("lnkbtnActive");
            Label lblContactID = (Label)e.Item.FindControl("lblContactID");
            int ID = Convert.ToInt32(lblContactID.Text);

            if (ListTBLCONTACT.Where(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
            {
                lnkbtnActive.Text = "DeActivate";
                lnkbtnActive.CssClass = "btn btn-sm red filter-submit margin-bottom";
            }
            else
            {
                lnkbtnActive.Text = "Active";
                lnkbtnActive.CssClass = "btn btn-sm green filter-submit margin-bottom";
            }
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
        public string GetPosition(int ID)
        {
            List<Database.Tbl_Position_Mst> List = DB.Tbl_Position_Mst.Where(p => p.TenentID == TID).ToList();
            if (ID != 0)
            {
                return List[0].PositionName;
            }
            else
            {
                return "Not Found";
            }
        }
        public void Maitanance()
        {
            //try
            //{
            //    if (!File.Exists(Server.MapPath("Maintain.txt")))
            //    {
            //        File.WriteAllText(Server.MapPath("Maintain.txt"), "Maintain" + DateTime.Now.ToString());
            //    }
            //    else
            //    {
            //        if (File.GetLastWriteTime(Server.MapPath("test.txt")).Day != DateTime.Now.Day)
            //        {
            //            string qry = "";
                        
            //            List<Database.TBLMaintanance> listTBLMaintanances = DB.TBLMaintanances.Where(p => p.Active == true && p.SwichType == 1).ToList();
            //            foreach (Database.TBLMaintanance item in listTBLMaintanances)
            //                qry += item.Query;
            //            command2 = new SqlCommand(qry, con);
            //            con.Open();
            //            command2.ExecuteReader();
            //            con.Close();
            //            File.WriteAllText(Server.MapPath("Maintain.txt"), "Maintain" + DateTime.Now.ToString());
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //    File.WriteAllText(Server.MapPath("Maintain.txt"), "Maintain" + DateTime.Now.ToString() + "Erorr : " + ex.Message);
            //}
        }




    }
}