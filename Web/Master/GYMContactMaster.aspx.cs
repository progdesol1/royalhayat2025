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
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;


namespace Web.Master
{
    public partial class GYMContactMaster : System.Web.UI.Page
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
        List<Navigation> ChoiceList = new List<Navigation>();
        List<Database.TBLCONTACT> TempCont = new List<Database.TBLCONTACT>();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();

            pnlSuccessMsg.Visible = false;
            PanelError.Visible = false;
            pnlwarning.Visible = false;
            lblMsg.Text = "";
            lblerror.Text = "";
            Session["Name"] = "Contact";

            if (TID != 7)
            {
                btninvoice1.Visible = false;
                Panel4.Visible = false;
                Panel5.Visible = false;
                Panel6.Visible = false;

            }
            else
            {
                Panel7.Visible = false;
            }

            if (!IsPostBack)
            {
                //Session["LANGUAGE"] = "en-US";
                ManageLang();

                txtCustoID.Enabled = false;
                FistTimeLoad();

                Session["Pagename"] = "Suppliers";
                Session["Name"] = "Contact";
                FillContractorID();
                FillCUSPackage();

                //List<Database.TBLCONTACT> TotalActive = new List<Database.TBLCONTACT>();
                //List<Database.TBLCONTACT> TotalinActive = new List<Database.TBLCONTACT>();
                //var TotalRecordList = DB.TBLCONTACTs.Where(p => p.TenentID == TID).ToList();
                //TotalActive = TotalRecordList.Where(p => p.Active == "Y").ToList();
                //TotalinActive = TotalRecordList.Where(p => p.Active == "N").ToList();
                LinkTotalRecords.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count().ToString();
                LinkTotalActive.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).Count().ToString();
                LinkTotalInactive.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count().ToString();

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
                    //BindData();
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
                        LinkSave.Visible = false;
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
                            LinkSave.Text = "Update Customer";
                            btnSubmit.Visible = true;
                            Button1.Visible = false;
                            lblBusContactDe.Text = "(Edit Mode)";
                        }
                        else
                        {

                            redonlyfales();
                            btnSubmit.Visible = false;
                            Button1.Visible = true;
                            //lblAttecmentcount.Text = Countatt(ContactMyID);
                            //btnattmentmst.Visible = true;
                            lblBusContactDe.Text = "(Read Mode)";
                        }

                    }
                    //Listview2.DataSource = DB.TBLCONTACTs.Where(p => p.ContactMyID == ContactMyID && p.TenentID == TID);
                    //Listview2.DataBind();
                    int Listconu = DB.TBLCONTACTs.Where(p => p.ContactMyID == ContactMyID && p.TenentID == TID).Count();

                    //ViewState["SaveList"] = List;
                    int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = Listconu;
                    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, TempCont);
                    //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
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
                        int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                        int Totalrec = List.Count();
                        Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    }
                }


            }
            Session["Pagename"] = "Suppliers";


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
            lblBusContactDe.Text = "(Read Mode)";

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
            lblBusContactDe.Text = "(Write Mode)";
            ViewState["ModeData"] = "Write";

            txtBirthdate.Enabled = txtCivilID.Enabled = true;
        }

        public void BindData(int CID)
        {
            int countt = 0;
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                countt = SearchList.Count();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    countt = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").Count();
                }
                else if (Stat == "NO")
                {
                    countt = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count();
                }
                else
                {
                    countt = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count();
                }
            }
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = countt;
            Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, TempCont);

            //if (ViewState["SerchListofContect"] == null)
            //{

            //    List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.ContactMyID == CID).OrderByDescending(m => m.UPDTTIME).ToList();
            //    ViewState["SaveList"] = List;
            //    //Listview2.DataSource = List;
            //    //Listview2.DataBind();
            //    if (List.Count() > 0)
            //    {
            //        BindEditMode(List[0].ContactMyID);
            //        int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            //        int Totalrec = List.Count();
            //        Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
            //    }

            //}
            //else
            //{
            //    var List = ((List<Database.TBLCONTACT>)ViewState["SerchListofContect"]).ToList();
            //    List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();
            //    foreach (Database.TBLCONTACT item in List)
            //    {
            //        Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactMyID && p.TenentID == TID && p.ContacType == 82003);
            //        Con_List.Add(obj_Contact);
            //    }
            //    var List1 = Con_List.OrderBy(p => p.UPDTTIME).ToList();
            //    ViewState["SaveList"] = List1;
            //    ViewState["SerchListofContect"] = List1;
            //    BindEditMode(List1[0].ContactMyID);
            //    int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            //    int Totalrec = List1.Count();
            //    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List1);
            //    //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
            //    //Listview2.DataBind();
            //}

            ////int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            ////int Totalrec = List.Count();
            ////((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        }

        //public void BindDataActive()
        //{
        //    if (ViewState["SerchListofContect"] == null)
        //    {

        //        List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
        //        ViewState["SaveList"] = List;
        //        if (List.Count() > 0)
        //        {
        //            BindEditMode(List[0].ContactMyID);
        //            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
        //            int Totalrec = List.Count();
        //            Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        //        }
        //    }
        //    else
        //    {
        //        var List = ((List<Database.TBLCONTACT>)ViewState["SerchListofContect"]).ToList();
        //        List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();
        //        foreach (Database.TBLCONTACT item in List)
        //        {
        //            Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactMyID && p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003);
        //            Con_List.Add(obj_Contact);
        //        }
        //        var List1 = Con_List.OrderBy(p => p.UPDTTIME).ToList();
        //        ViewState["SaveList"] = List1;
        //        ViewState["SerchListofContect"] = List1;
        //        BindEditMode(List1[0].ContactMyID);
        //        int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
        //        int Totalrec = List1.Count();
        //        Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List1);
        //        //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
        //        //Listview2.DataBind();
        //    }

        //    //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    //int Totalrec = List.Count();
        //    //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
        //}
        public void BindDataall()
        {
            ViewState["Active"] = "ALL";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                int Showdata = 20;//Convert.ToInt32(drpShowGrid.SelectedValue);
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, DB.TBLCONTACTs.Where(p => p.TenentID == TID).Take(1).ToList());
            }
            ModalPopupExtender4.Hide();
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
                if(DB.TBLCONTACTs.Where(p => p.ContactMyID == ID && p.TenentID == TID && p.Active == "Y").Count() > 0)
                {
                    Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID);
                    objtbl_CONTACT.Active = "N";
                    DB.SaveChanges();
                }
                else
                {
                    Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.ContactMyID == ID && p.TenentID == TID);
                    objtbl_CONTACT.Active = "Y";
                    DB.SaveChanges();
                }
                BindData(ID);
                LinkTotalActive.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).Count().ToString();
                LinkTotalInactive.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count().ToString();
                string msg = "Customer Inactive Successfully...";
                string Function = "openModal('" + msg + "');";
                ClientScript.RegisterStartupScript(Page.GetType(), "openModal()", Function, true);

            }
            if (e.CommandName == "btnEdit")
            {
                txtCustoID.Enabled = false;
                decimal ContactMyID = Convert.ToDecimal(Request.QueryString["ContactMyID"]);
                btnSubmit.Text = "Update";
                LinkSave.Text = "Update Customer";
                pnlpack.Visible = false;

                BindEditMode(ContactMyID);
                redonlyture();
                btnSubmit.Visible = true;
                LinkSave.Visible = true;
                Button1.Visible = false;
                LinkAddnew.Visible = false;
                //lblAttecmentcount.Text = Countatt(ContactMyID);
                //btnattmentmst.Visible = true;
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                lblBusContactDe.Text = "(Edit Mode)";
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
                LinkSave.Visible = false;
                Button1.Visible = true;
                LinkAddnew.Visible = true;
                //lblAttecmentcount.Text = Countatt(ContactMyID);
                //btnattmentmst.Visible = true;
                lblBusContactDe.Text = "(Read Mode)";

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

            txtMobileNo.Text = objtbl_CONTACT.MOBPHONE;

            txtAddress.Text = objtbl_CONTACT.ADDR1;
            //txtAddress2.Text = objtbl_CONTACT.ADDR2;
            DateTime BirthDate = Convert.ToDateTime(objtbl_CONTACT.BirthDate);
            txtBirthdate.Text = BirthDate.ToShortDateString();
            txtCivilID.Text = objtbl_CONTACT.CivilID;
            //txtCity.Text = objtbl_CONTACT.CITY;
            int COID = Convert.ToInt32(objtbl_CONTACT.COUNTRYID);
            drpCountry.SelectedValue = COID.ToString();
            if (objtbl_CONTACT.Switch5 != "" && objtbl_CONTACT.Switch5 != null)
                drpMotiveTOJoin.SelectedValue = objtbl_CONTACT.Switch5;

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

                    objtblContact.EMAIL1 = (tags_2.Text);//getFirstCommaseprate(tags_2.Text);                   
                    objtblContact.MOBPHONE = (txtMobileNo.Text);//getFirstCommaseprate(txtMobileNo.Text);                    
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
                    objtblContact.Switch5 = drpMotiveTOJoin.SelectedValue;
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
                    BindData(ContactID);
                    //scope.Complete();                   
                    //Response.Redirect("GYMContactMaster.aspx");//?MID=JopOdwUVhcI=                   
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
                    int ContacMyiid = CID1 != 0 ? CID1 : DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1) : 1;
                    objtblContact.ContactMyID = ContacMyiid;
                    objtblContact.CONTACTID = CID1 != 0 ? CID1 : DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.CONTACTID) + 1) : 1;
                    int CMID = Convert.ToInt32(objtblContact.CONTACTID);
                    objtblContact.PHYSICALLOCID = "CRM";
                    objtblContact.PersName1 = txtContactName.Text;
                    objtblContact.PersName2 = txtContact2.Text;
                    objtblContact.PersName3 = txtContact3.Text;
                    objtblContact.EMAIL1 = (tags_2.Text);//getFirstCommaseprate(tags_2.Text);                    
                    objtblContact.MOBPHONE = (txtMobileNo.Text);// getFirstCommaseprate(txtMobileNo.Text);                    
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
                    objtblContact.Switch5 = drpMotiveTOJoin.SelectedValue;
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
                    LinkTotalRecords.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count().ToString();
                    LinkTotalActive.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).Count().ToString();
                    LinkTotalInactive.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count().ToString();
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    BindData(ContacMyiid);
                }

                btnSubmit.Visible = false;
                LinkSave.Text = "Add New Customer";
                LinkSave.Visible = false;
                Button1.Visible = true;
                LinkAddnew.Visible = true;
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
        public string getSecondCommaseprate(string sepval)
        {
            //string[] Seperate = sepval.Split(',');
            //if (!string.IsNullOrEmpty(Seperate[1]))
            //{

            //    string SecondValue = Seperate[1];
            //    return SecondValue;
            //}
            //else
            //{
            //    return null;
            //}
            string[] Seperate = tags_2.Text.Split(',');
            string Sep = "";
            int Coun = 0;
            for (int i = 0; i <= Seperate.Count() - 1; i++)
            {
                Coun++;
                if (Coun == 2)
                    Sep = Seperate[i];
                else
                    Sep = "000";
            }
            return Sep;
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
            Response.Redirect("GYMContactMaster.aspx");
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
            drpSort.Items.Insert(2, new ListItem(Label67.Text, "2"));
            drpSort.Items.Insert(2, new ListItem(Label690.Text, "3"));
            drpSort.Items.Insert(3, new ListItem(Label71.Text, "4"));
            //drpSort.Items.Insert(4, new ListItem(Label70.Text, "5"));
            //drpSort.Items.Insert(5, new ListItem(Label71.Text, "6"));
            //drpSort.Items.Insert(2, new ListItem(Label72.Text, "7"));
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


            //drppaymentmethod.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method" && p.SHORTNAME == "GYM");
            //drppaymentmethod.DataTextField = "REFNAME1";
            //drppaymentmethod.DataValueField = "REFID";
            //drppaymentmethod.DataBind();
            //drppaymentmethod.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            decimal CID = Convert.ToDecimal(ViewState["compId"]);
            btnSubmit.Text = "Update";
            LinkSave.Text = "Update Customer";
            BindEditMode(CID);
            //updCountry.Update();
            redonlyture();

            lblBusContactDe.Text = "(Edit Mode)";
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
                var UserList = DB.TBLCONTACTs.Where(p => p.PersName3 == txtContact3.Text && p.TenentID == TID).FirstOrDefault();

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
                var UserList = DB.TBLCONTACTs.Where(p => p.MOBPHONE == txtMobileNo.Text && p.TenentID == TID).FirstOrDefault();

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
            LinkSave.Visible = false;
            DateTime Maxdate = Convert.ToDateTime(DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y").Max(p => p.UPDTTIME));
            if (DB.TBLCONTACTs.Where(p => p.Active == "Y" && p.TenentID == TID && p.ContacType == 82003).Count() > 0)
            {
                decimal LIDID = Convert.ToDecimal(DB.TBLCONTACTs.Where(p => p.Active == "Y" && p.TenentID == TID && p.ContacType == 82003).Max(p => p.ContactMyID));
                BindEditMode(LIDID);
                int CID = Convert.ToInt32(LIDID);
                ViewState["Active"] = "YES";
                BindData(CID);

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
            return ConID.ToString() + "-" + Name;
        }

        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindDataall();
        }
        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindDataall();
        }

        protected void drpSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["SaveList"] != null)
            {
                var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
                //if(chkactive.Checked == true)    
                if (ViewState["Active"] != null)
                {
                    string Active = (ViewState["Active"]).ToString();
                    if (Active == "YES")
                    {
                        if (drpSort.SelectedValue == "1")
                            List = List.OrderBy(m => m.PersName1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        if (drpSort.SelectedValue == "2")
                            List = List.OrderBy(m => m.EMAIL1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        if (drpSort.SelectedValue == "3")
                            List = List.OrderBy(m => m.MOBPHONE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        if (drpSort.SelectedValue == "4")
                            List = List.OrderBy(m => m.CivilID).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        //if (drpSort.SelectedValue == "5")
                        //    List = List.OrderBy(m => m.STATE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        //if (drpSort.SelectedValue == "6")
                        //    List = List.OrderBy(m => m.ZIPCODE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        //if (drpSort.SelectedValue == "7")
                        //    List = List.OrderBy(m => m.CITY).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        //if (drpSort.SelectedValue == "8")
                        //    List = List.OrderBy(m => m.REMARKS).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
                        int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                        int Totalrec = List.Count();
                        Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    }
                    else
                    {
                        if (drpSort.SelectedValue == "1")
                            List = List.OrderBy(m => m.PersName1).Where(p => p.TenentID == TID).ToList();
                        if (drpSort.SelectedValue == "2")
                            List = List.OrderBy(m => m.EMAIL1).Where(p => p.TenentID == TID).ToList();
                        if (drpSort.SelectedValue == "3")
                            List = List.OrderBy(m => m.MOBPHONE).Where(p => p.TenentID == TID).ToList();
                        if (drpSort.SelectedValue == "4")
                            List = List.OrderBy(m => m.CivilID).Where(p => p.TenentID == TID).ToList();
                        int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                        int Totalrec = List.Count();
                        Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    }
                }
                else
                {
                    if (drpSort.SelectedValue == "1")
                        List = List.OrderBy(m => m.PersName1).Where(p => p.TenentID == TID).ToList();
                    if (drpSort.SelectedValue == "2")
                        List = List.OrderBy(m => m.EMAIL1).Where(p => p.TenentID == TID).ToList();
                    if (drpSort.SelectedValue == "3")
                        List = List.OrderBy(m => m.MOBPHONE).Where(p => p.TenentID == TID).ToList();
                    if (drpSort.SelectedValue == "4")
                        List = List.OrderBy(m => m.CivilID).Where(p => p.TenentID == TID).ToList();
                    int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                }
            }
        }

        protected void btnNext1_Click(object sender, EventArgs e)
        {
            //var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            int COUU = 0;
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                COUU = SearchList.Count();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").Count();
                }
                else if (Stat == "NO")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count();
                }
                else
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count();
                }
            }
            int Totalrec = COUU;
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                }
                else if (Stat == "NO")
                {
                    BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                }
                else
                {
                    BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                }
            }
            //BindList((List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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

            GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            //var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            int COUU = 0;
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                COUU = SearchList.Count();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").Count();
                }
                else if (Stat == "NO")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count();
                }
                else
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count();
                }
            }
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = COUU;
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {

                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                if (ViewState["SerchListTabel"] != null)
                {
                    SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                }
                else if (ViewState["Active"] != null)
                {
                    string Stat = ViewState["Active"].ToString();
                    if (Stat == "YES")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                    else if (Stat == "NO")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                    else
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                }
                //BindList((List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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
                GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {


        }
        protected void btnfirst1_Click(object sender, EventArgs e)
        {
            //var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            int COUU = 0;
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                COUU = SearchList.Count();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").Count();
                }
                else if (Stat == "NO")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count();
                }
                else
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count();
                }
            }
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = COUU;
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                take = Showdata;
                Skip = 0;
                if (ViewState["SerchListTabel"] != null)
                {
                    SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                }
                else if (ViewState["Active"] != null)
                {
                    string Stat = ViewState["Active"].ToString();
                    if (Stat == "YES")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                    else if (Stat == "NO")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                    else
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                }
                //BindList((List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            //var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            int COUU = 0;
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                COUU = SearchList.Count();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").Count();
                }
                else if (Stat == "NO")
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count();
                }
                else
                {
                    COUU = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count();
                }
            }
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = COUU;
            take = Totalrec;
            Skip = Totalrec - Showdata;

            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                }
                else if (Stat == "NO")
                {
                    BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                }
                else
                {
                    BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                }
            }
            //BindList((List.Where(p => p.TenentID == TID).OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            GetCurrentNavigationLast(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //var List = ((List<Database.TBLCONTACT>)ViewState["SaveList"]).ToList();
            //List<Database.TBLCONTACT> List111 = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            int countt = 0;
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                countt = SearchList.Count();
            }
            else if (ViewState["Active"] != null)
            {
                string Stat = ViewState["Active"].ToString();
                if (Stat == "YES")
                {
                    countt = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").Count();
                }
                else if (Stat == "NO")
                {
                    countt = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).Count();
                }
                else
                {
                    countt = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).Count();
                }
            }
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = countt;
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                if (ViewState["SerchListTabel"] != null)
                {
                    SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                }
                else if (ViewState["Active"] != null)
                {
                    string Stat = ViewState["Active"].ToString();
                    if (Stat == "YES")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003 && p.Active == "Y").OrderByDescending(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
                    }
                    else if (Stat == "NO")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
                    }
                    else
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
                    }
                }
                //BindList(Listview2, (List.Where(p => p.TenentID == TID).OrderByDescending(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
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

            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "BlockStop()", true);
        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            //if(chkactive.Checked == true)
            if (ViewState["Active"] != null)
            {
                string Active = (ViewState["Active"]).ToString();
                if (Active == "YES")
                {
                    ViewState["SerchListTabel"] = null;
                    string id1 = txtSearch.Text;
                    var List = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.PersName2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL2.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper()) || p.BUSPHONE1.ToUpper().Contains(id1.ToUpper()) || p.CivilID.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID && p.Active == "Y").OrderBy(p => p.ContactMyID).ToList();
                    ViewState["SaveListCount"] = List.Count();
                    ViewState["SerchListTabel"] = List;
                    int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    //ViewState["SerchListTabel"] = List;
                }
                else
                {
                    ViewState["SerchListTabel"] = null;
                    string id1 = txtSearch.Text;
                    var List = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.PersName2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL2.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper()) || p.BUSPHONE1.ToUpper().Contains(id1.ToUpper()) || p.CivilID.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderBy(p => p.ContactMyID).ToList();
                    ViewState["SaveListCount"] = List.Count();
                    ViewState["SerchListTabel"] = List;
                    int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                    //ViewState["SerchListTabel"] = List;
                }
            }
            else
            {
                ViewState["SerchListTabel"] = null;
                string id1 = txtSearch.Text;
                var List = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.PersName2.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.EMAIL2.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper()) || p.BUSPHONE1.ToUpper().Contains(id1.ToUpper()) || p.CivilID.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID).OrderBy(p => p.ContactMyID).ToList();
                ViewState["SaveListCount"] = List.Count();
                ViewState["SerchListTabel"] = List;
                int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
                //ViewState["SerchListTabel"] = List;
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["compId"] = null;
            txtCustoID.Text = DB.TBLCONTACTs.Where(p => p.TenentID == TID).Count() > 0 ? (DB.TBLCONTACTs.Where(p => p.TenentID == TID).Max(p => p.ContactMyID) + 1).ToString() : "1";
            txtCustoID.Enabled = true;
            redonlyture();
            btnSubmit.Visible = true;
            LinkSave.Visible = true;
            btnSubmit.Text = "Save";
            clen();
            Button1.Visible = false;
            LinkAddnew.Visible = false;

            Avatar.ImageUrl = "~/Gallery/defolt.png";
            pnlpack.Visible = false;
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

        //protected void chkactive_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkactive.Checked == true)
        //    {
        //        BindDataActive();
        //    }
        //    else
        //    {
        //        BindDataall();
        //    }
        //}
        List<Database.TBLCONTACT> ListTBLCONTACT = new List<Database.TBLCONTACT>();
        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton LinkButton2 = (LinkButton)e.Item.FindControl("LinkButton2");
            Label Label70 = (Label)e.Item.FindControl("Label70");
            Label lblCustomerName = (Label)e.Item.FindControl("lblCustomerName");
            Label lblAddress = (Label)e.Item.FindControl("lblAddress");
            Label lblMOBPHONE = (Label)e.Item.FindControl("lblMOBPHONE");
            Label lblZIPCODE = (Label)e.Item.FindControl("lblZIPCODE");

            int CID = Convert.ToInt32(Label70.Text);
            if (DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Active == "Y").Count() > 0)
            {
                LinkButton2.Text = "InActive";
            }
            else
            {
                LinkButton2.Text = "Active";
                Label70.Attributes["Style"] = "color:#a94442;";
                lblCustomerName.Attributes["Style"] = "color:#a94442;";
                lblAddress.Attributes["Style"] = "color:#a94442;";
                lblMOBPHONE.Attributes["Style"] = "color:#a94442;";
                lblZIPCODE.Attributes["Style"] = "color:#a94442;";
            }
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
            Setpanel("none", "expand");
            PackSetpanel("none", "expand");
            setpanelmain("none", "expand");
            SetPanelMainList("none", "expand");
            pnlpack.Visible = true;
            txtcont.Text = txtContactName.Text.ToString();
            lblHideID.Text = txtCustoID.Text;
            lblpackMSG.Text = "";
            PackClear();
        }
        protected void btninvoiceCancel_Click(object sender, EventArgs e)
        {
            Setpanel("block", "collapse");
            PackSetpanel("block", "collapse");
            setpanelmain("block", "collapse");
            SetPanelMainList("block", "collapse");
            pnlpack.Visible = false;
            FinalPayment.Visible = true;
            Packagebind();
        }
        public void PackClear()
        {
            drpPackage.SelectedIndex = 0;
            txtAmount.Text = "";
            txtpackStartdate.Text = "";
            txtpackEnddate.Text = "";
        }
        protected void btnpacksave_Click(object sender, EventArgs e)
        {
            saveandprintinvoice();
        }
        public void saveandprintinvoice()
        {

            pnlwarning.Visible = false;
            if (lblHideID.Text != "")
            {
                int CustvendID = Convert.ToInt32(lblHideID.Text);//Convert.ToInt32(txtCustoID.Text);
                int packid = Convert.ToInt32(drpPackage.SelectedValue);
                int Month = Convert.ToInt32(ViewState["Month"]);
                int mytransid = 0;
                if (btnpacksave.Text == "update")
                {
                    string Cuser = ViewState["SuppID"].ToString();
                    string PASSWORD = Classes.GlobleClass.EncryptionHelpers.Encrypt(ViewState["SuppPass"].ToString());
                    string Cpass = PASSWORD;
                    if (DB.USER_MST.Where(p => p.TenentID == TID && p.LOGIN_ID == Cuser && p.PASSWORD == Cpass && p.CompId == 999).Count() > 0)
                    {
                        //Database.TBLCONTACT Cuobj = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContacType == 82012 && p.CUSERID == Cuser && p.CPASSWRD == Cpass);
                        string uid = ((USER_MST)Session["USER"]).LOGIN_ID;
                        mytransid = Convert.ToInt32(ViewState["MyTranIDD"]);

                        int TenentID = TID;
                        int MYTRANSID = mytransid; //mytranceid
                        int ToTenentID = 7;
                        int TOLOCATIONID = 1;
                        string MainTranType = "N";
                        string TranType = "POS Invoice";
                        int transid = 4101;
                        int transsubid = 410103;
                        string MYSYSNAME = "GYM";
                        decimal COMPID = 1;
                        decimal CUSTVENDID = Convert.ToDecimal(lblHideID.Text);//Convert.ToDecimal(txtCustoID.Text); //customerID
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
                        if (SDT >= ENTRYDATE && SDT <= ENTRYTIME)
                        {
                            STD = "Running";
                        }
                        else
                        {
                            STD = "End Package";
                        }
                        string Status = STD;
                        int Terms = 0;
                        string DatainserStatest = "update";
                        string Custmerid = "N";
                        string Swit1 = "N";
                        string ExtraField2 = "N";
                        int RefTransID = Convert.ToInt32(drpPackage.SelectedValue); // package GYM
                        string Reason = "N";
                        string TransDocNo = "N";
                        int LinkTransID = 1;

                        Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, ExtraField2, RefTransID, Reason, TransDocNo, LinkTransID);

                        Database.ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytransid);

                        objICTR_DT.TenentID = TenentID;
                        objICTR_DT.MYTRANSID = mytransid;
                        objICTR_DT.locationID = 1;
                        objICTR_DT.MyProdID = 1;
                        objICTR_DT.REFTYPE = "0";
                        objICTR_DT.REFSUBTYPE = "0";
                        objICTR_DT.PERIOD_CODE = "1";
                        objICTR_DT.MYSYSNAME = "GYM";
                        objICTR_DT.JOID = Convert.ToInt32(lblHideID.Text);
                        objICTR_DT.JOBORDERDTMYID = 1;
                        objICTR_DT.ACTIVITYID = 1;
                        objICTR_DT.DESCRIPTION = "GYM";
                        objICTR_DT.UOM = "N";
                        objICTR_DT.QUANTITY = Convert.ToInt32(drpPackage.SelectedValue); // package GYM
                        objICTR_DT.UNITPRICE = 1;
                        objICTR_DT.AMOUNT = Convert.ToDecimal(txtAmount.Text); //amount
                        objICTR_DT.OVERHEADAMOUNT = 1;
                        objICTR_DT.BATCHNO = "N";
                        objICTR_DT.BIN_ID = Month;
                        objICTR_DT.BIN_TYPE = "N";
                        objICTR_DT.GRNREF = "N";
                        objICTR_DT.DISPER = 0;
                        objICTR_DT.DISAMT = 0;
                        objICTR_DT.TAXAMT = 0;
                        objICTR_DT.TAXPER = 0;
                        objICTR_DT.PROMOTIONAMT = 0;
                        objICTR_DT.GLPOST = "N";
                        objICTR_DT.GLPOST1 = "N";
                        objICTR_DT.GLPOSTREF1 = "N";
                        objICTR_DT.GLPOSTREF = "N";
                        objICTR_DT.ICPOST = "N";
                        objICTR_DT.ICPOSTREF = "N";
                        objICTR_DT.EXPIRYDATE = DateTime.Now;
                        objICTR_DT.COMPANYID = 1;
                        objICTR_DT.SWITCH1 = "N";
                        objICTR_DT.ACTIVE = true;
                        objICTR_DT.DelFlag = 0;
                        objICTR_DT.PlanStartDate = Convert.ToDateTime(txtpackStartdate.Text); //start date
                        objICTR_DT.PlanEndDate = Convert.ToDateTime(txtpackEnddate.Text); //end date
                        DateTime StartDated = Convert.ToDateTime(txtpackStartdate.Text); //start date
                        DateTime EndDated = Convert.ToDateTime(txtpackEnddate.Text); //end date
                        DateTime DTs = DateTime.Now;
                        string DTss = "";
                        if (DTs >= StartDated && DTs <= EndDated)
                        {
                            DTss = "Running";
                        }
                        else if (EndDated > DTs)
                        {
                            DTss = "Package not Active";
                        }
                        else
                        {
                            DTss = "End Package";
                        }
                        objICTR_DT.Stutas = DTss;
                        DB.SaveChanges();

                        btnpacksave.Text = "Save";
                        btnSaveANDPrint.Text = "Save & print";
                        //pnlSuppervicer.Visible = false;
                        string packName = (drpPackage.SelectedItem).ToString();
                        pnlwarning.Visible = true;
                        lblmsgw.Text = "Customer " + txtcont.Text + " having Package " + packName + " start " + StartDated.ToString("MM/dd/yyyy") + " ends " + EndDated.ToString("MM/dd/yyyy") + " elapsed with this current package " + DTss + "...";//"package Data Update Successfully";
                        FinalPayment.Visible = true;
                    }
                    else
                    {
                        pnlwarning.Visible = true;
                        lblmsgw.Text = "Suppervicer Login ID & Password Is Not Match";
                    }
                    Packagebind();
                }
                else
                {
                    if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == CustvendID && p.RefTransID == packid).Count() == 0)
                    {
                        string uid = ((USER_MST)Session["USER"]).LOGIN_ID;
                        mytransid = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;

                        int TenentID = TID;
                        int MYTRANSID = mytransid; //mytranceid
                        int ToTenentID = 7;
                        int TOLOCATIONID = 1;
                        string MainTranType = "N";
                        string TranType = "POS Invoice";
                        int transid = 4101;
                        int transsubid = 410103;
                        string MYSYSNAME = "GYM";
                        decimal COMPID = 1;
                        decimal CUSTVENDID = Convert.ToDecimal(lblHideID.Text);//Convert.ToDecimal(txtCustoID.Text); //customerID
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
                        if (SDT >= ENTRYDATE && SDT <= ENTRYTIME)
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


                        if (mytransid == 0)
                        {
                            Database.ICTR_HD objHD = DB.ICTR_HD.Single(p => p.TenentID == TID && p.CUSTVENDID == CustvendID);
                            int MytranID = Convert.ToInt32(objHD.MYTRANSID);
                            mytransid = MytranID;
                        }
                        int Package = Convert.ToInt32(drpPackage.SelectedValue);
                        if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == CustvendID && p.MYTRANSID == mytransid && p.QUANTITY == Package).Count() > 0)
                        {
                            pnlwarning.Visible = true;
                            lblmsgw.Text = "This Customer have a package allready exist";
                            return;
                        }
                        int TenentID_DT = TID;
                        int MYTRANSID_DT = mytransid; //mytranceid
                        int locationID_DT = 1;
                        int MyProdID_DT = Convert.ToInt32(drpPackage.SelectedValue);
                        string REFTYPE_DT = "0";
                        string REFSUBTYPE_DT = "0";
                        string PERIOD_CODE_DT = "1";
                        string MYSYSNAME_DT = "GYM";
                        int JOID_DT = Convert.ToInt32(lblHideID.Text);
                        int JOBORDERDTMYID_DT = 1;
                        int ACTIVITYID_DT = 1;
                        string DESCRIPTION_DT = "GYM";
                        string UOM_DT = "N";
                        int QUANTITY_DT = Convert.ToInt32(drpPackage.SelectedValue); // package GYM
                        decimal UNITPRICE_DT = 1;
                        decimal AMOUNT_DT = Convert.ToDecimal(txtAmount.Text); //amount
                        decimal OVERHEADAMOUNT_DT = 1;
                        string BATCHNO_DT = "N";
                        int BIN_ID_DT = Month;
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
                        DateTime DTs = DateTime.Now;
                        string DTss = "";
                        if (DTs >= StartDate && DTs <= EndDate)
                        {
                            DTss = "Running";
                        }
                        else if (EndDate > DTs)
                        {
                            DTss = "Package not Active";
                        }
                        else
                        {
                            DTss = "End Package";
                        }
                        string DTStatus = DTss;

                        Classes.EcommAdminClass.insertICTR_DT(TenentID_DT, MYTRANSID_DT, locationID_DT, MyProdID_DT, REFTYPE_DT, REFSUBTYPE_DT, PERIOD_CODE_DT, MYSYSNAME_DT, JOID_DT, JOBORDERDTMYID_DT, ACTIVITYID_DT, DESCRIPTION_DT, UOM_DT, QUANTITY_DT, UNITPRICE_DT, AMOUNT_DT, OVERHEADAMOUNT_DT, BATCHNO_DT, BIN_ID_DT, BIN_TYPE_DT, GRNREF_DT, DISPER_DT, DISAMT_DT, TAXPER_DT, TAXAMT_DT, PROMOTIONAMT_DT, CRUP_ID_DT, GLPOST_DT, GLPOST1_DT, GLPOSTREF1_DT, GLPOSTREF_DT, ICPOST_DT, ICPOSTREF_DT, EXPIRYDATE_DT, ACTIVE_DT, SWITCH1_DT, COMPANYID1_DT, DelFlag_DT, ITEMID_DT, StartDate, EndDate, DTStatus);
                        int PaymentTermsId = 2081;
                        // int paytermID = Convert.ToInt32(drppaymentmethod.SelectedValue);
                        // string ReferenceNo = drppaymentmethod.SelectedItem.ToString();
                        //if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID_DT && p.PaymentTermsId == PaymentTermsId).Count() < 1)
                        //{

                        Classes.EcommAdminClass.insertICTRPayTerms_HD(TenentID_DT, MYTRANSID_DT, PaymentTermsId, "1", locationID_DT, 1, AMOUNT_DT, "Cash", StartDate, EndDate, "GYM", 0, 0, "0", 1, false, false, false, false, EXPIRYDATE_DT, EXPIRYDATE_DT, EXPIRYDATE_DT, EXPIRYDATE_DT, "GYM");
                        //}                
                        Packagebind();

                        var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MYTRANSID_DT && p.TenentID == TID).ToList();
                        if (listcost.Count() > 0)
                        {
                            pnlPaymentterm.Visible = true;
                            ViewState["TempEco_ICCATEGORY"] = listcost;
                            //Session["Invoice"] = ID;
                            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                            Repeater3.DataBind();
                        }
                        else
                        {
                            pnlPaymentterm.Visible = true;
                            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                            Repeater3.DataBind();
                        }
                        Database.TBLCONTACT MYobj = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CustvendID);
                        string MYActive = MYobj.Active.ToString();
                        if (MYActive == "N")
                        {
                            MYobj.Active = "Y";
                            DB.SaveChanges();
                        }
                        Packagebind();

                        pnlwarning.Visible = true;
                        lblmsgw.Text = "Invoice Save Successfully";
                        ViewState["Mytranceidd"] = mytransid;
                        ViewState["Packidd"] = QUANTITY_DT;
                        //Response.Redirect("/Master/GYMPrint.aspx?Tranjestion=" + mytransid + "&Pack=" + QUANTITY_DT);
                    }
                    else
                    {
                        pnlwarning.Visible = true;
                        lblmsgw.Text = "This Customer have a package allready exist";
                        return;
                    }
                }
            }
            else
            {
                pnlwarning.Visible = true;
                lblmsgw.Text = "Please First search Customer...";
            }
        }
        protected void btnSaveANDPrint_Click(object sender, EventArgs e)
        {
            saveandprintinvoice();
            int MID = Convert.ToInt32(ViewState["Mytranceidd"]);
            int Packid = Convert.ToInt32(ViewState["Packidd"]);
            Response.Redirect("/Master/GYMPrint.aspx?Tranjestion=" + MID + "&Pack=" + Packid);
            //Response.Redirect("../ReportMst/GYMPrint.aspx?Tranjestion=" + MID + "&Pack=" + Packid);
        }
        protected void linkcontSearch_Click(object sender, EventArgs e)
        {
            pnlwarning.Visible = false;
            lblpackMSG.Text = "";
            PackClear();
            string val = HiddenField3.Value.ToString();
            string SearchID = txtcont.Text.Trim().ToString();
            int vendid = 0;
            if (SearchID != "")
            {
                List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && (p.PersName1.ToUpper().Contains(SearchID.ToUpper()) || p.PersName2.ToUpper().Contains(SearchID.ToUpper()) || p.PersName3.ToUpper().Contains(SearchID.ToUpper()) || p.CivilID.ToUpper().Contains(SearchID.ToUpper()) || p.MOBPHONE.Contains(SearchID))).ToList();
                foreach (Database.TBLCONTACT item in List)
                {
                    if (List.Where(p => p.TenentID == TID && p.PersName1 == item.PersName1).Count() == 1)
                    {
                        Database.TBLCONTACT obj = List.Single(p => p.TenentID == TID && p.PersName1 == item.PersName1);
                        if (val != "")
                        {
                            vendid = Convert.ToInt32(val);
                        }
                        else
                        {
                            vendid = Convert.ToInt32(obj.ContactMyID);
                            txtcont.Text = "";
                            txtcont.Text = obj.PersName1.ToString();
                        }
                        lblHideID.Text = vendid.ToString();
                        List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == vendid).ToList();
                        if (ListHD.Count() == 1)
                        {
                            List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == vendid).ToList();
                            var objDDT = ListDT.First();
                            drpPackage.SelectedValue = objDDT.QUANTITY.ToString();//objHD.RefTransID.ToString();
                            txtAmount.Text = objDDT.AMOUNT.ToString();//objHD.TOTAMT.ToString();
                            txtpackStartdate.Text = Convert.ToDateTime(objDDT.PlanStartDate).ToString("MMM/dd/yyyy");//Convert.ToDateTime(objHD.ENTRYDATE).ToShortDateString();
                            txtpackEnddate.Text = Convert.ToDateTime(objDDT.PlanEndDate).ToString("MMM/dd/yyyy");//Convert.ToDateTime(objHD.ENTRYTIME).ToShortDateString();
                            lblmonth.Text = "Package in Months" + " " + objDDT.BIN_ID.ToString();
                            lblpackMSG.Text = "Customer invoice Allready Exist...";
                            Packagebind();
                        }
                        else
                        {
                            PackClear();
                            lblpackMSG.Text = "Please Fill Data And Add Invoice";
                            ListPackage.DataSource = null;
                            ListPackage.DataBind();
                        }
                    }
                    else
                    {
                        lblpackMSG.Text = "Customer Not Found...";
                        ListPackage.DataSource = null;
                        ListPackage.DataBind();
                    }
                    Packagebind();
                }

            }
            else
            {
                lblpackMSG.Text = "Please Type Name of Customer & search...";
                ListPackage.DataSource = null;
                ListPackage.DataBind();
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCustSearch(string prefixText, int count)
        {
            //if (Session["LANGUAGE"].ToString() == "ar-KW")           
            string Lang = HttpContext.Current.Session["LANGUAGE"].ToString();
            int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
            string conStr;
            string sqlQuery = "";
            conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
            if (Lang == "en-US")
            {
                sqlQuery = "select PersName1,ContactMyID from TBLCONTACT where TenentID = " + TID + " and (PersName1 like @CustName  + '%' or PersName2 like @CustName  + '%' or PersName3 like @CustName  + '%' or CivilID like @CustName  + '%' or MOBPHONE like @CustName  + '%')";
            }
            else if (Lang == "ar-KW")
            {
                sqlQuery = "select PersName2,ContactMyID from TBLCONTACT where TenentID = " + TID + " and (PersName1 like @CustName  + '%' or PersName2 like @CustName  + '%' or PersName3 like @CustName  + '%' or CivilID like @CustName  + '%' or MOBPHONE like @CustName  + '%')";
            }
            //string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and BUYER = 'true' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@CustName", prefixText);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> custList = new List<string>();
            string custItem = string.Empty;
            while (dr.Read())
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
                custList.Add(custItem);
            }
            conn.Close();
            dr.Close();
            return custList.ToArray();
        }
        public void Packagebind()
        {
            int CUSTVENDID = 0;
            string CustID = txtCustoID.Text;
            if (CustID != "")
            {
                if (lblHideID.Text == "")
                    CUSTVENDID = Convert.ToInt32(CustID);
                else
                    CUSTVENDID = Convert.ToInt32(lblHideID.Text); //customerID
                List<Database.ICTR_HD> HDLIST = DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == CUSTVENDID).ToList();
                if (HDLIST.Count() > 0)
                {
                    //Database.ICTR_HD objHD = HDLIST.Single(p => p.TenentID == TID && p.CUSTVENDID == CUSTVENDID);
                    //int MytranID = Convert.ToInt32(objHD.MYTRANSID);
                    List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == CUSTVENDID).ToList();// && p.MYTRANSID == MytranID
                    ListPackage.DataSource = ListDT;
                    ListPackage.DataBind();
                    if (HDLIST.Count() > 0)
                    {
                        for (int i = 0; i < ListPackage.Items.Count; i++)
                        {

                            Label mytranceidd = (Label)ListPackage.Items[i].FindControl("mytranceidd");
                            Label lblmtid = (Label)ListPackage.Items[i].FindControl("lblmtid");
                            int MytranID = Convert.ToInt32(mytranceidd.Text);
                            int myid = Convert.ToInt32(lblmtid.Text);
                            DateTime TodayDate = DateTime.Now;

                            Database.ICTR_DT DTobj = ListDT.Single(p => p.TenentID == TID && p.JOID == CUSTVENDID && p.MYID == myid && p.MYTRANSID == MytranID);
                            DateTime SDT = Convert.ToDateTime(DTobj.PlanStartDate);
                            DateTime EDT = Convert.ToDateTime(DTobj.PlanEndDate);

                            if (TodayDate >= SDT && TodayDate <= EDT)
                            { }
                            else if (EDT > TodayDate)
                            {
                                DTobj.Stutas = "Package not Active";
                                DB.SaveChanges();
                            }
                            else
                            {
                                DTobj.Stutas = "End Package";
                                DB.SaveChanges();
                            }
                        }
                        ListPackage.DataSource = ListDT;
                        ListPackage.DataBind();
                    }
                }
                else
                {
                    List<Database.ICTR_DT> ListDTemp = new List<Database.ICTR_DT>();
                    ListPackage.DataSource = ListDTemp;
                    ListPackage.DataBind();
                }
            }
            else
            {
                List<Database.ICTR_DT> ListDTemp = new List<Database.ICTR_DT>();
                ListPackage.DataSource = ListDTemp;
                ListPackage.DataBind();
            }
        }
        public string GetPack(int RID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == RID).Count() > 0)
            {
                string Packname = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == RID).ProdName1;
                return Packname;
            }
            else
            {
                return "Not Found";
            }
        }
        //Language
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
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCONTACT").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {

                if (lblContactMyID1s.ID == item.LabelID)
                    txtContactMyID1s.Text = lblContactMyID1s.Text = item.LabelName;
                //else if (lblCONTACTID1s.ID == item.LabelID)
                //    txtCONTACTID1s.Text = lblCONTACTID1s.Text = item.LabelName;
                //else if (lblPHYSICALLOCID1s.ID == item.LabelID)
                //    txtPHYSICALLOCID1s.Text = lblPHYSICALLOCID1s.Text = item.LabelName;
                else if (lblPersName11s.ID == item.LabelID)
                    txtPersName11s.Text = lblPersName11s.Text = item.LabelName;
                else if (lblPersName21s.ID == item.LabelID)
                    txtPersName21s.Text = lblPersName21s.Text = item.LabelName;
                else if (lblPersName31s.ID == item.LabelID)
                    txtPersName31s.Text = lblPersName31s.Text = item.LabelName;
                else if (lblBirthDate1s.ID == item.LabelID)
                    txtBirthDate1s.Text = lblBirthDate1s.Text = item.LabelName;
                else if (lblCivilID1s.ID == item.LabelID)
                    txtCivilID1s.Text = lblCivilID1s.Text = item.LabelName;
                else if (lblEMAIL11s.ID == item.LabelID)
                    txtEMAIL11s.Text = lblEMAIL11s.Text = item.LabelName;
                else if (lblMOBPHONE1s.ID == item.LabelID)
                    txtMOBPHONE1s.Text = lblMOBPHONE1s.Text = item.LabelName;
                else if (lblITMANAGER1s.ID == item.LabelID)
                    txtITMANAGER1s.Text = lblITMANAGER1s.Text = item.LabelName;
                else if (lblADDR11s.ID == item.LabelID)
                    txtADDR11s.Text = lblADDR11s.Text = item.LabelName;
                //else if (lblADDR21s.ID == item.LabelID)
                //    txtADDR21s.Text = lblADDR21s.Text = item.LabelName;
                //else if (lblCITY1s.ID == item.LabelID)
                //    txtCITY1s.Text = lblCITY1s.Text = item.LabelName;
                //else if (lblSTATE1s.ID == item.LabelID)
                //    txtSTATE1s.Text = lblSTATE1s.Text = item.LabelName;
                //else if (lblPOSTALCODE1s.ID == item.LabelID)
                //    txtPOSTALCODE1s.Text = lblPOSTALCODE1s.Text = item.LabelName;
                //else if (lblZIPCODE1s.ID == item.LabelID)
                //    txtZIPCODE1s.Text = lblZIPCODE1s.Text = item.LabelName;
                //else if (lblMYCONLOCID1s.ID == item.LabelID)
                //    txtMYCONLOCID1s.Text = lblMYCONLOCID1s.Text = item.LabelName;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = item.LabelName;
                //else if (lblBUSPHONE11s.ID == item.LabelID)
                //    txtBUSPHONE11s.Text = lblBUSPHONE11s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                else if (lblCOMPANYID1s.ID == item.LabelID)
                    txtCOMPANYID1s.Text = lblCOMPANYID1s.Text = item.LabelName;
                else if (lblMYCATSUBID1s.ID == item.LabelID)
                    txtMYCATSUBID1s.Text = lblMYCATSUBID1s.Text = item.LabelName;
                else if (lblMYPRODID1s.ID == item.LabelID)
                    txtMYPRODID1s.Text = lblMYPRODID1s.Text = item.LabelName;
                else if (lblDESERIAL1s.ID == item.LabelID)
                    txtDESERIAL1s.Text = lblDESERIAL1s.Text = item.LabelName;
                else if (lblCATID1s.ID == item.LabelID)
                    txtCATID1s.Text = lblCATID1s.Text = item.LabelName;
                else if (lblCATTYPE1s.ID == item.LabelID)
                    txtCATTYPE1s.Text = lblCATTYPE1s.Text = item.LabelName;
                else if (lblSUBCATTYPE1s.ID == item.LabelID)
                    txtSUBCATTYPE1s.Text = lblSUBCATTYPE1s.Text = item.LabelName;
                else if (lblSUBCATID1s.ID == item.LabelID)
                    txtSUBCATID1s.Text = lblSUBCATID1s.Text = item.LabelName;
                else if (lblPERSNAME1s.ID == item.LabelID)
                    txtPERSNAME1s.Text = lblPERSNAME1s.Text = item.LabelName;
                else if (lblPERSNAMEO1s.ID == item.LabelID)
                    txtPERSNAMEO1s.Text = lblPERSNAMEO1s.Text = item.LabelName;
                else if (lblPERSNAMEO21s.ID == item.LabelID)
                    txtPERSNAMEO21s.Text = lblPERSNAMEO21s.Text = item.LabelName;
                //else if (lblEMAIL21s.ID == item.LabelID)
                //    txtEMAIL21s.Text = lblEMAIL21s.Text = item.LabelName;
                //else if (lblPRIMLANGUGE1s.ID == item.LabelID)
                //    txtPRIMLANGUGE1s.Text = lblPRIMLANGUGE1s.Text = item.LabelName;
                //else if (lblWEBPAGE1s.ID == item.LabelID)
                //    txtWEBPAGE1s.Text = lblWEBPAGE1s.Text = item.LabelName;
                //else if (lblISSMB1s.ID == item.LabelID)
                //    txtISSMB1s.Text = lblISSMB1s.Text = item.LabelName;
                //else if (lblINHAWALLY1s.ID == item.LabelID)
                //    txtINHAWALLY1s.Text = lblINHAWALLY1s.Text = item.LabelName;
                //else if (lblEMAISUB1s.ID == item.LabelID)
                //    txtEMAISUB1s.Text = lblEMAISUB1s.Text = item.LabelName;
                //else if (lblEMAILSUBDATE1s.ID == item.LabelID)
                //    txtEMAILSUBDATE1s.Text = lblEMAILSUBDATE1s.Text = item.LabelName;
                //else if (lblPRODUCTDEALIN1s.ID == item.LabelID)
                //    txtPRODUCTDEALIN1s.Text = lblPRODUCTDEALIN1s.Text = item.LabelName;
                //else if (lblSALER1s.ID == item.LabelID)
                //    txtSALER1s.Text = lblSALER1s.Text = item.LabelName;
                //else if (lblBUYER1s.ID == item.LabelID)
                //    txtBUYER1s.Text = lblBUYER1s.Text = item.LabelName;
                //else if (lblFREELANCER1s.ID == item.LabelID)
                //    txtFREELANCER1s.Text = lblFREELANCER1s.Text = item.LabelName;
                //else if (lblSALEDEPROD1s.ID == item.LabelID)
                //    txtSALEDEPROD1s.Text = lblSALEDEPROD1s.Text = item.LabelName;
                //else if (lblCUSERID1s.ID == item.LabelID)
                //    txtCUSERID1s.Text = lblCUSERID1s.Text = item.LabelName;
                //else if (lblCPASSWRD1s.ID == item.LabelID)
                //    txtCPASSWRD1s.Text = lblCPASSWRD1s.Text = item.LabelName;
                //else if (lblUSERID1s.ID == item.LabelID)
                //    txtUSERID1s.Text = lblUSERID1s.Text = item.LabelName;
                //else if (lblENTRYTIME1s.ID == item.LabelID)
                //    txtENTRYTIME1s.Text = lblENTRYTIME1s.Text = item.LabelName;
                //else if (lblUPDTTIME1s.ID == item.LabelID)
                //    txtUPDTTIME1s.Text = lblUPDTTIME1s.Text = item.LabelName;
                //else if (lblFaxID1s.ID == item.LabelID)
                //    txtFaxID1s.Text = lblFaxID1s.Text = item.LabelName;
                else if (lblIMG1s.ID == item.LabelID)
                    txtIMG1s.Text = lblIMG1s.Text = item.LabelName;
                //else if (lblInstuctor_Username1s.ID == item.LabelID)
                //    txtInstuctor_Username1s.Text = lblInstuctor_Username1s.Text = item.LabelName;
                //else if (lblContacType1s.ID == item.LabelID)
                //    txtContacType1s.Text = lblContacType1s.Text = item.LabelName;
                //else if (lblBARCODE1s.ID == item.LabelID)
                //    txtBARCODE1s.Text = lblBARCODE1s.Text = item.LabelName;

                //else if (lblTenentID2h.ID == item.LabelID)
                //    txtTenentID2h.Text = lblTenentID2h.Text = item.LabelName;
                else if (lblContactMyID2h.ID == item.LabelID)
                    txtContactMyID2h.Text = lblContactMyID2h.Text = item.LabelName;
                //else if (lblCONTACTID2h.ID == item.LabelID)
                //    txtCONTACTID2h.Text = lblCONTACTID2h.Text = item.LabelName;
                //else if (lblPHYSICALLOCID2h.ID == item.LabelID)
                //    txtPHYSICALLOCID2h.Text = lblPHYSICALLOCID2h.Text = item.LabelName;
                else if (lblPersName12h.ID == item.LabelID)
                    txtPersName12h.Text = lblPersName12h.Text = item.LabelName;
                else if (lblPersName22h.ID == item.LabelID)
                    txtPersName22h.Text = lblPersName22h.Text = item.LabelName;
                else if (lblPersName32h.ID == item.LabelID)
                    txtPersName32h.Text = lblPersName32h.Text = item.LabelName;
                else if (lblBirthDate2h.ID == item.LabelID)
                    txtBirthDate2h.Text = lblBirthDate2h.Text = item.LabelName;
                else if (lblCivilID2h.ID == item.LabelID)
                    txtCivilID2h.Text = lblCivilID2h.Text = item.LabelName;
                else if (lblEMAIL12h.ID == item.LabelID)
                    txtEMAIL12h.Text = lblEMAIL12h.Text = item.LabelName;
                else if (lblMOBPHONE2h.ID == item.LabelID)
                    txtMOBPHONE2h.Text = lblMOBPHONE2h.Text = item.LabelName;
                else if (lblITMANAGER2h.ID == item.LabelID)
                    txtITMANAGER2h.Text = lblITMANAGER2h.Text = item.LabelName;
                else if (lblADDR12h.ID == item.LabelID)
                    txtADDR12h.Text = lblADDR12h.Text = item.LabelName;
                //else if (lblADDR22h.ID == item.LabelID)
                //    txtADDR22h.Text = lblADDR22h.Text = item.LabelName;
                //else if (lblCITY2h.ID == item.LabelID)
                //    txtCITY2h.Text = lblCITY2h.Text = item.LabelName;
                //else if (lblSTATE2h.ID == item.LabelID)
                //    txtSTATE2h.Text = lblSTATE2h.Text = item.LabelName;
                //else if (lblPOSTALCODE2h.ID == item.LabelID)
                //    txtPOSTALCODE2h.Text = lblPOSTALCODE2h.Text = item.LabelName;
                //else if (lblZIPCODE2h.ID == item.LabelID)
                //    txtZIPCODE2h.Text = lblZIPCODE2h.Text = item.LabelName;
                //else if (lblMYCONLOCID2h.ID == item.LabelID)
                //    txtMYCONLOCID2h.Text = lblMYCONLOCID2h.Text = item.LabelName;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = item.LabelName;
                //else if (lblBUSPHONE12h.ID == item.LabelID)
                //    txtBUSPHONE12h.Text = lblBUSPHONE12h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = item.LabelName;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;
                else if (lblCOMPANYID2h.ID == item.LabelID)
                    txtCOMPANYID2h.Text = lblCOMPANYID2h.Text = item.LabelName;
                else if (lblMYCATSUBID2h.ID == item.LabelID)
                    txtMYCATSUBID2h.Text = lblMYCATSUBID2h.Text = item.LabelName;
                else if (lblMYPRODID2h.ID == item.LabelID)
                    txtMYPRODID2h.Text = lblMYPRODID2h.Text = item.LabelName;
                else if (lblDESERIAL2h.ID == item.LabelID)
                    txtDESERIAL2h.Text = lblDESERIAL2h.Text = item.LabelName;
                else if (lblCATID2h.ID == item.LabelID)
                    txtCATID2h.Text = lblCATID2h.Text = item.LabelName;
                else if (lblCATTYPE2h.ID == item.LabelID)
                    txtCATTYPE2h.Text = lblCATTYPE2h.Text = item.LabelName;
                else if (lblSUBCATTYPE2h.ID == item.LabelID)
                    txtSUBCATTYPE2h.Text = lblSUBCATTYPE2h.Text = item.LabelName;
                else if (lblSUBCATID2h.ID == item.LabelID)
                    txtSUBCATID2h.Text = lblSUBCATID2h.Text = item.LabelName;
                else if (lblPERSNAME2h.ID == item.LabelID)
                    txtPERSNAME2h.Text = lblPERSNAME2h.Text = item.LabelName;
                else if (lblPERSNAMEO2h.ID == item.LabelID)
                    txtPERSNAMEO2h.Text = lblPERSNAMEO2h.Text = item.LabelName;
                else if (lblPERSNAMEO22h.ID == item.LabelID)
                    txtPERSNAMEO22h.Text = lblPERSNAMEO22h.Text = item.LabelName;
                //else if (lblEMAIL22h.ID == item.LabelID)
                //    txtEMAIL22h.Text = lblEMAIL22h.Text = item.LabelName;
                //else if (lblPRIMLANGUGE2h.ID == item.LabelID)
                //    txtPRIMLANGUGE2h.Text = lblPRIMLANGUGE2h.Text = item.LabelName;
                //else if (lblWEBPAGE2h.ID == item.LabelID)
                //    txtWEBPAGE2h.Text = lblWEBPAGE2h.Text = item.LabelName;
                //else if (lblISSMB2h.ID == item.LabelID)
                //    txtISSMB2h.Text = lblISSMB2h.Text = item.LabelName;
                //else if (lblINHAWALLY2h.ID == item.LabelID)
                //    txtINHAWALLY2h.Text = lblINHAWALLY2h.Text = item.LabelName;
                //else if (lblEMAISUB2h.ID == item.LabelID)
                //    txtEMAISUB2h.Text = lblEMAISUB2h.Text = item.LabelName;
                //else if (lblEMAILSUBDATE2h.ID == item.LabelID)
                //    txtEMAILSUBDATE2h.Text = lblEMAILSUBDATE2h.Text = item.LabelName;
                //else if (lblPRODUCTDEALIN2h.ID == item.LabelID)
                //    txtPRODUCTDEALIN2h.Text = lblPRODUCTDEALIN2h.Text = item.LabelName;
                //else if (lblSALER2h.ID == item.LabelID)
                //    txtSALER2h.Text = lblSALER2h.Text = item.LabelName;
                //else if (lblBUYER2h.ID == item.LabelID)
                //    txtBUYER2h.Text = lblBUYER2h.Text = item.LabelName;
                //else if (lblFREELANCER2h.ID == item.LabelID)
                //    txtFREELANCER2h.Text = lblFREELANCER2h.Text = item.LabelName;
                //else if (lblSALEDEPROD2h.ID == item.LabelID)
                //    txtSALEDEPROD2h.Text = lblSALEDEPROD2h.Text = item.LabelName;
                //else if (lblCUSERID2h.ID == item.LabelID)
                //    txtCUSERID2h.Text = lblCUSERID2h.Text = item.LabelName;
                //else if (lblCPASSWRD2h.ID == item.LabelID)
                //    txtCPASSWRD2h.Text = lblCPASSWRD2h.Text = item.LabelName;
                //else if (lblUSERID2h.ID == item.LabelID)
                //    txtUSERID2h.Text = lblUSERID2h.Text = item.LabelName;
                //else if (lblENTRYTIME2h.ID == item.LabelID)
                //    txtENTRYTIME2h.Text = lblENTRYTIME2h.Text = item.LabelName;
                //else if (lblUPDTTIME2h.ID == item.LabelID)
                //    txtUPDTTIME2h.Text = lblUPDTTIME2h.Text = item.LabelName;
                //else if (lblFaxID2h.ID == item.LabelID)
                //    txtFaxID2h.Text = lblFaxID2h.Text = item.LabelName;
                else if (lblIMG2h.ID == item.LabelID)
                    txtIMG2h.Text = lblIMG2h.Text = item.LabelName;
                //else if (lblInstuctor_Username2h.ID == item.LabelID)
                //    txtInstuctor_Username2h.Text = lblInstuctor_Username2h.Text = item.LabelName;
                //else if (lblContacType2h.ID == item.LabelID)
                //    txtContacType2h.Text = lblContacType2h.Text = item.LabelName;
                //else if (lblBARCODE2h.ID == item.LabelID)
                //    txtBARCODE2h.Text = lblBARCODE2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCONTACT").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLCONTACT.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLCONTACT").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblContactMyID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContactMyID1s.Text;
                //else if (lblCONTACTID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCONTACTID1s.Text;
                //else if (lblPHYSICALLOCID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPHYSICALLOCID1s.Text;
                else if (lblPersName11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPersName11s.Text;
                else if (lblPersName21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPersName21s.Text;
                else if (lblPersName31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPersName31s.Text;
                else if (lblBirthDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBirthDate1s.Text;
                else if (lblCivilID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCivilID1s.Text;
                else if (lblEMAIL11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEMAIL11s.Text;
                else if (lblMOBPHONE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMOBPHONE1s.Text;
                else if (lblITMANAGER1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITMANAGER1s.Text;
                else if (lblADDR11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDR11s.Text;
                //else if (lblADDR21s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtADDR21s.Text;
                //else if (lblCITY1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCITY1s.Text;
                //else if (lblSTATE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSTATE1s.Text;
                //else if (lblPOSTALCODE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPOSTALCODE1s.Text;
                //else if (lblZIPCODE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtZIPCODE1s.Text;
                //else if (lblMYCONLOCID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMYCONLOCID1s.Text;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                //else if (lblBUSPHONE11s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBUSPHONE11s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblCOMPANYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOMPANYID1s.Text;
                else if (lblMYCATSUBID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYCATSUBID1s.Text;
                else if (lblMYPRODID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYPRODID1s.Text;
                else if (lblDESERIAL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDESERIAL1s.Text;
                else if (lblCATID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATID1s.Text;
                else if (lblCATTYPE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATTYPE1s.Text;
                else if (lblSUBCATTYPE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATTYPE1s.Text;
                else if (lblSUBCATID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATID1s.Text;
                else if (lblPERSNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPERSNAME1s.Text;
                else if (lblPERSNAMEO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPERSNAMEO1s.Text;
                else if (lblPERSNAMEO21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPERSNAMEO21s.Text;
                //else if (lblEMAIL21s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEMAIL21s.Text;
                //else if (lblPRIMLANGUGE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPRIMLANGUGE1s.Text;
                //else if (lblWEBPAGE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtWEBPAGE1s.Text;
                //else if (lblISSMB1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtISSMB1s.Text;
                //else if (lblINHAWALLY1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtINHAWALLY1s.Text;
                //else if (lblEMAISUB1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEMAISUB1s.Text;
                //else if (lblEMAILSUBDATE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEMAILSUBDATE1s.Text;
                //else if (lblPRODUCTDEALIN1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPRODUCTDEALIN1s.Text;
                //else if (lblSALER1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSALER1s.Text;
                //else if (lblBUYER1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBUYER1s.Text;
                //else if (lblFREELANCER1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtFREELANCER1s.Text;
                //else if (lblSALEDEPROD1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSALEDEPROD1s.Text;
                //else if (lblCUSERID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCUSERID1s.Text;
                //else if (lblCPASSWRD1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCPASSWRD1s.Text;
                //else if (lblUSERID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUSERID1s.Text;
                //else if (lblENTRYTIME1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYTIME1s.Text;
                //else if (lblUPDTTIME1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUPDTTIME1s.Text;
                //else if (lblFaxID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtFaxID1s.Text;
                else if (lblIMG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIMG1s.Text;
                //else if (lblInstuctor_Username1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtInstuctor_Username1s.Text;
                //else if (lblContacType1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtContacType1s.Text;
                //else if (lblBARCODE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBARCODE1s.Text;

                //else if (lblTenentID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenentID2h.Text;
                else if (lblContactMyID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContactMyID2h.Text;
                //else if (lblCONTACTID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCONTACTID2h.Text;
                //else if (lblPHYSICALLOCID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPHYSICALLOCID2h.Text;
                else if (lblPersName12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPersName12h.Text;
                else if (lblPersName22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPersName22h.Text;
                else if (lblPersName32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPersName32h.Text;
                else if (lblBirthDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBirthDate2h.Text;
                else if (lblCivilID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCivilID2h.Text;
                else if (lblEMAIL12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEMAIL12h.Text;
                else if (lblMOBPHONE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMOBPHONE2h.Text;
                else if (lblITMANAGER2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITMANAGER2h.Text;
                else if (lblADDR12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDR12h.Text;
                //else if (lblADDR22h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtADDR22h.Text;
                //else if (lblCITY2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCITY2h.Text;
                //else if (lblSTATE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSTATE2h.Text;
                //else if (lblPOSTALCODE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPOSTALCODE2h.Text;
                //else if (lblZIPCODE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtZIPCODE2h.Text;
                //else if (lblMYCONLOCID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMYCONLOCID2h.Text;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                //else if (lblBUSPHONE12h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBUSPHONE12h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                else if (lblCOMPANYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOMPANYID2h.Text;
                else if (lblMYCATSUBID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYCATSUBID2h.Text;
                else if (lblMYPRODID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYPRODID2h.Text;
                else if (lblDESERIAL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDESERIAL2h.Text;
                else if (lblCATID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATID2h.Text;
                else if (lblCATTYPE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATTYPE2h.Text;
                else if (lblSUBCATTYPE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATTYPE2h.Text;
                else if (lblSUBCATID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATID2h.Text;
                else if (lblPERSNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPERSNAME2h.Text;
                else if (lblPERSNAMEO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPERSNAMEO2h.Text;
                else if (lblPERSNAMEO22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPERSNAMEO22h.Text;
                //else if (lblEMAIL22h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEMAIL22h.Text;
                //else if (lblPRIMLANGUGE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPRIMLANGUGE2h.Text;
                //else if (lblWEBPAGE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtWEBPAGE2h.Text;
                //else if (lblISSMB2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtISSMB2h.Text;
                //else if (lblINHAWALLY2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtINHAWALLY2h.Text;
                //else if (lblEMAISUB2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEMAISUB2h.Text;
                //else if (lblEMAILSUBDATE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEMAILSUBDATE2h.Text;
                //else if (lblPRODUCTDEALIN2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPRODUCTDEALIN2h.Text;
                //else if (lblSALER2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSALER2h.Text;
                //else if (lblBUYER2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBUYER2h.Text;
                //else if (lblFREELANCER2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtFREELANCER2h.Text;
                //else if (lblSALEDEPROD2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSALEDEPROD2h.Text;
                //else if (lblCUSERID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCUSERID2h.Text;
                //else if (lblCPASSWRD2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCPASSWRD2h.Text;
                //else if (lblUSERID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUSERID2h.Text;
                //else if (lblENTRYTIME2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYTIME2h.Text;
                //else if (lblUPDTTIME2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUPDTTIME2h.Text;
                //else if (lblFaxID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtFaxID2h.Text;
                else if (lblIMG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIMG2h.Text;
                //else if (lblInstuctor_Username2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtInstuctor_Username2h.Text;
                //else if (lblContacType2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtContacType2h.Text;
                //else if (lblBARCODE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBARCODE2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLCONTACT.xml"));

        }
        public void GetShow()
        {

            lblContactMyID1s.Attributes["class"] = lblPersName11s.Attributes["class"] = lblPersName21s.Attributes["class"] = lblPersName31s.Attributes["class"] = lblBirthDate1s.Attributes["class"] = lblCivilID1s.Attributes["class"] = lblEMAIL11s.Attributes["class"] = lblMOBPHONE1s.Attributes["class"] = lblITMANAGER1s.Attributes["class"] = lblIMG1s.Attributes["class"] = lblADDR11s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblIMG1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = lblCOMPANYID1s.Attributes["class"] = lblMYCATSUBID1s.Attributes["class"] = lblMYPRODID1s.Attributes["class"] = lblDESERIAL1s.Attributes["class"] = lblCATID1s.Attributes["class"] = lblCATTYPE1s.Attributes["class"] = lblSUBCATID1s.Attributes["class"] = lblPERSNAME1s.Attributes["class"] = lblPERSNAMEO1s.Attributes["class"] = lblPERSNAMEO21s.Attributes["class"] = lblSUBCATTYPE1s.Attributes["class"] = "control-label col-md-4  getshow";//= lblCONTACTID1s.Attributes["class"] = lblPHYSICALLOCID1s.Attributes["class"] = lblADDR21s.Attributes["class"] = lblCITY1s.Attributes["class"] = lblSTATE1s.Attributes["class"] = lblPOSTALCODE1s.Attributes["class"] = lblZIPCODE1s.Attributes["class"] = lblMYCONLOCID1s.Attributes["class"] = lblBUSPHONE11s.Attributes["class"] = lblEMAIL21s.Attributes["class"] = lblPRIMLANGUGE1s.Attributes["class"] = lblWEBPAGE1s.Attributes["class"] = lblISSMB1s.Attributes["class"] = lblINHAWALLY1s.Attributes["class"] = lblEMAISUB1s.Attributes["class"] = lblEMAILSUBDATE1s.Attributes["class"] = lblPRODUCTDEALIN1s.Attributes["class"] = lblSALER1s.Attributes["class"] = lblBUYER1s.Attributes["class"] = lblFREELANCER1s.Attributes["class"] = lblSALEDEPROD1s.Attributes["class"] = lblCUSERID1s.Attributes["class"] = lblCPASSWRD1s.Attributes["class"] = lblUSERID1s.Attributes["class"] = lblENTRYTIME1s.Attributes["class"] = lblUPDTTIME1s.Attributes["class"] = lblFaxID1s.Attributes["class"] = lblInstuctor_Username1s.Attributes["class"] = lblContacType1s.Attributes["class"] = lblBARCODE1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblContactMyID2h.Attributes["class"] = lblPersName12h.Attributes["class"] = lblPersName22h.Attributes["class"] = lblPersName32h.Attributes["class"] = lblBirthDate2h.Attributes["class"] = lblCivilID2h.Attributes["class"] = lblEMAIL12h.Attributes["class"] = lblMOBPHONE2h.Attributes["class"] = lblITMANAGER2h.Attributes["class"] = lblIMG2h.Attributes["class"] = lblADDR12h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblIMG2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = lblCOMPANYID2h.Attributes["class"] = lblMYCATSUBID2h.Attributes["class"] = lblMYPRODID2h.Attributes["class"] = lblDESERIAL2h.Attributes["class"] = lblCATID2h.Attributes["class"] = lblCATTYPE2h.Attributes["class"] = lblSUBCATID2h.Attributes["class"] = lblPERSNAME2h.Attributes["class"] = lblPERSNAMEO2h.Attributes["class"] = lblPERSNAMEO22h.Attributes["class"] = lblSUBCATTYPE2h.Attributes["class"] = "control-label col-md-4  gethide";//= lblCONTACTID2h.Attributes["class"] = lblPHYSICALLOCID2h.Attributes["class"] = lblADDR22h.Attributes["class"] = lblCITY2h.Attributes["class"] = lblSTATE2h.Attributes["class"] = lblPOSTALCODE2h.Attributes["class"] = lblZIPCODE2h.Attributes["class"] = lblMYCONLOCID2h.Attributes["class"] = lblBUSPHONE12h.Attributes["class"] = lblEMAIL22h.Attributes["class"] = lblPRIMLANGUGE2h.Attributes["class"] = lblWEBPAGE2h.Attributes["class"] = lblISSMB2h.Attributes["class"] = lblINHAWALLY2h.Attributes["class"] = lblEMAISUB2h.Attributes["class"] = lblEMAILSUBDATE2h.Attributes["class"] = lblPRODUCTDEALIN2h.Attributes["class"] = lblSALER2h.Attributes["class"] = lblBUYER2h.Attributes["class"] = lblFREELANCER2h.Attributes["class"] = lblSALEDEPROD2h.Attributes["class"] = lblCUSERID2h.Attributes["class"] = lblCPASSWRD2h.Attributes["class"] = lblUSERID2h.Attributes["class"] = lblENTRYTIME2h.Attributes["class"] = lblUPDTTIME2h.Attributes["class"] = lblFaxID2h.Attributes["class"] = lblInstuctor_Username2h.Attributes["class"] = lblContacType2h.Attributes["class"] = lblBARCODE2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblContactMyID1s.Attributes["class"] = lblPersName11s.Attributes["class"] = lblPersName21s.Attributes["class"] = lblPersName31s.Attributes["class"] = lblBirthDate1s.Attributes["class"] = lblCivilID1s.Attributes["class"] = lblEMAIL11s.Attributes["class"] = lblMOBPHONE1s.Attributes["class"] = lblITMANAGER1s.Attributes["class"] = lblIMG1s.Attributes["class"] = lblADDR11s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblIMG1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = lblCOMPANYID1s.Attributes["class"] = lblMYCATSUBID1s.Attributes["class"] = lblMYPRODID1s.Attributes["class"] = lblDESERIAL1s.Attributes["class"] = lblCATID1s.Attributes["class"] = lblCATTYPE1s.Attributes["class"] = lblSUBCATID1s.Attributes["class"] = lblPERSNAME1s.Attributes["class"] = lblPERSNAMEO1s.Attributes["class"] = lblPERSNAMEO21s.Attributes["class"] = lblSUBCATTYPE1s.Attributes["class"] = "control-label col-md-4  gethide";//= lblCONTACTID1s.Attributes["class"] = lblPHYSICALLOCID1s.Attributes["class"] = lblADDR21s.Attributes["class"] = lblCITY1s.Attributes["class"] = lblSTATE1s.Attributes["class"] = lblPOSTALCODE1s.Attributes["class"] = lblZIPCODE1s.Attributes["class"] = lblMYCONLOCID1s.Attributes["class"] = lblBUSPHONE11s.Attributes["class"] = lblEMAIL21s.Attributes["class"] = lblPRIMLANGUGE1s.Attributes["class"] = lblWEBPAGE1s.Attributes["class"] = lblISSMB1s.Attributes["class"] = lblINHAWALLY1s.Attributes["class"] = lblEMAISUB1s.Attributes["class"] = lblEMAILSUBDATE1s.Attributes["class"] = lblPRODUCTDEALIN1s.Attributes["class"] = lblSALER1s.Attributes["class"] = lblBUYER1s.Attributes["class"] = lblFREELANCER1s.Attributes["class"] = lblSALEDEPROD1s.Attributes["class"] = lblCUSERID1s.Attributes["class"] = lblCPASSWRD1s.Attributes["class"] = lblUSERID1s.Attributes["class"] = lblENTRYTIME1s.Attributes["class"] = lblUPDTTIME1s.Attributes["class"] = lblFaxID1s.Attributes["class"] = lblInstuctor_Username1s.Attributes["class"] = lblContacType1s.Attributes["class"] = lblBARCODE1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblContactMyID2h.Attributes["class"] = lblPersName12h.Attributes["class"] = lblPersName22h.Attributes["class"] = lblPersName32h.Attributes["class"] = lblBirthDate2h.Attributes["class"] = lblCivilID2h.Attributes["class"] = lblEMAIL12h.Attributes["class"] = lblMOBPHONE2h.Attributes["class"] = lblITMANAGER2h.Attributes["class"] = lblIMG2h.Attributes["class"] = lblADDR12h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblIMG2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = lblCOMPANYID2h.Attributes["class"] = lblMYCATSUBID2h.Attributes["class"] = lblMYPRODID2h.Attributes["class"] = lblDESERIAL2h.Attributes["class"] = lblCATID2h.Attributes["class"] = lblCATTYPE2h.Attributes["class"] = lblSUBCATID2h.Attributes["class"] = lblPERSNAME2h.Attributes["class"] = lblPERSNAMEO2h.Attributes["class"] = lblPERSNAMEO22h.Attributes["class"] = lblSUBCATTYPE2h.Attributes["class"] = "control-label col-md-4  getshow";//= lblCONTACTID2h.Attributes["class"] = lblPHYSICALLOCID2h.Attributes["class"] = lblADDR22h.Attributes["class"] = lblCITY2h.Attributes["class"] = lblSTATE2h.Attributes["class"] = lblPOSTALCODE2h.Attributes["class"] = lblZIPCODE2h.Attributes["class"] = lblMYCONLOCID2h.Attributes["class"] = lblBUSPHONE12h.Attributes["class"] = lblEMAIL22h.Attributes["class"] = lblPRIMLANGUGE2h.Attributes["class"] = lblWEBPAGE2h.Attributes["class"] = lblISSMB2h.Attributes["class"] = lblINHAWALLY2h.Attributes["class"] = lblEMAISUB2h.Attributes["class"] = lblEMAILSUBDATE2h.Attributes["class"] = lblPRODUCTDEALIN2h.Attributes["class"] = lblSALER2h.Attributes["class"] = lblBUYER2h.Attributes["class"] = lblFREELANCER2h.Attributes["class"] = lblSALEDEPROD2h.Attributes["class"] = lblCUSERID2h.Attributes["class"] = lblCPASSWRD2h.Attributes["class"] = lblUSERID2h.Attributes["class"] = lblENTRYTIME2h.Attributes["class"] = lblUPDTTIME2h.Attributes["class"] = lblFaxID2h.Attributes["class"] = lblInstuctor_Username2h.Attributes["class"] = lblContacType2h.Attributes["class"] = lblBARCODE2h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }

        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblContactMyID2h.Visible = lblPersName12h.Visible = lblPersName22h.Visible = lblPersName32h.Visible = lblBirthDate2h.Visible = lblCivilID2h.Visible = lblEMAIL12h.Visible = lblMOBPHONE2h.Visible = lblITMANAGER2h.Visible = lblADDR12h.Visible = lblCOUNTRYID2h.Visible = lblIMG2h.Visible = lblActive2h.Visible = lblREMARKS2h.Visible = lblCRUP_ID2h.Visible = lblCOMPANYID2h.Visible = lblMYCATSUBID2h.Visible = lblMYPRODID2h.Visible = lblDESERIAL2h.Visible = lblCATID2h.Visible = lblCATTYPE2h.Visible = lblSUBCATID2h.Visible = lblPERSNAME2h.Visible = lblPERSNAMEO2h.Visible = lblPERSNAMEO22h.Visible = lblSUBCATTYPE2h.Visible = false;//= lblCONTACTID2h.Visible = lblPHYSICALLOCID2h.Visible = lblADDR22h.Visible = lblCITY2h.Visible = lblSTATE2h.Visible = lblPOSTALCODE2h.Visible = lblZIPCODE2h.Visible = lblMYCONLOCID2h.Visible = lblBUSPHONE12h.Visible = lblEMAIL22h.Visible = lblPRIMLANGUGE2h.Visible = lblWEBPAGE2h.Visible = lblISSMB2h.Visible = lblINHAWALLY2h.Visible = lblEMAISUB2h.Visible = lblEMAILSUBDATE2h.Visible = lblPRODUCTDEALIN2h.Visible = lblSALER2h.Visible = lblBUYER2h.Visible = lblFREELANCER2h.Visible = lblSALEDEPROD2h.Visible = lblCUSERID2h.Visible = lblCPASSWRD2h.Visible = lblUSERID2h.Visible = lblENTRYTIME2h.Visible = lblUPDTTIME2h.Visible = lblFaxID2h.Visible = lblInstuctor_Username2h.Visible = lblContacType2h.Visible = lblBARCODE2h.Visible = false;
                    //2true
                    txtContactMyID2h.Visible = txtPersName12h.Visible = txtPersName22h.Visible = txtPersName32h.Visible = txtBirthDate2h.Visible = txtCivilID2h.Visible = txtEMAIL12h.Visible = txtMOBPHONE2h.Visible = txtITMANAGER2h.Visible = txtADDR12h.Visible = txtCOUNTRYID2h.Visible = txtIMG2h.Visible = txtActive2h.Visible = txtREMARKS2h.Visible = txtCRUP_ID2h.Visible = txtCOMPANYID2h.Visible = txtMYCATSUBID2h.Visible = txtMYPRODID2h.Visible = txtDESERIAL2h.Visible = txtCATID2h.Visible = txtCATTYPE2h.Visible = txtSUBCATID2h.Visible = txtPERSNAME2h.Visible = txtPERSNAMEO2h.Visible = txtPERSNAMEO22h.Visible = txtSUBCATTYPE2h.Visible = true;//= txtCONTACTID2h.Visible = txtPHYSICALLOCID2h.Visible = txtADDR22h.Visible = txtCITY2h.Visible = txtSTATE2h.Visible = txtPOSTALCODE2h.Visible = txtZIPCODE2h.Visible = txtMYCONLOCID2h.Visible = txtBUSPHONE12h.Visible = txtEMAIL22h.Visible = txtPRIMLANGUGE2h.Visible = txtWEBPAGE2h.Visible = txtISSMB2h.Visible = txtINHAWALLY2h.Visible = txtEMAISUB2h.Visible = txtEMAILSUBDATE2h.Visible = txtPRODUCTDEALIN2h.Visible = txtSALER2h.Visible = txtBUYER2h.Visible = txtFREELANCER2h.Visible = txtSALEDEPROD2h.Visible = txtCUSERID2h.Visible = txtCPASSWRD2h.Visible = txtUSERID2h.Visible = txtENTRYTIME2h.Visible = txtUPDTTIME2h.Visible = txtFaxID2h.Visible = txtInstuctor_Username2h.Visible = txtContacType2h.Visible = txtBARCODE2h.Visible = true;

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
                    lblContactMyID2h.Visible = lblPersName12h.Visible = lblPersName22h.Visible = lblPersName32h.Visible = lblBirthDate2h.Visible = lblCivilID2h.Visible = lblEMAIL12h.Visible = lblMOBPHONE2h.Visible = lblITMANAGER2h.Visible = lblADDR12h.Visible = lblCOUNTRYID2h.Visible = lblIMG2h.Visible = lblActive2h.Visible = lblREMARKS2h.Visible = lblCRUP_ID2h.Visible = lblCOMPANYID2h.Visible = lblMYCATSUBID2h.Visible = lblMYPRODID2h.Visible = lblDESERIAL2h.Visible = lblCATID2h.Visible = lblCATTYPE2h.Visible = lblSUBCATID2h.Visible = lblPERSNAME2h.Visible = lblPERSNAMEO2h.Visible = lblPERSNAMEO22h.Visible = lblSUBCATTYPE2h.Visible = true;//= lblCONTACTID2h.Visible = lblPHYSICALLOCID2h.Visible = lblADDR22h.Visible = lblCITY2h.Visible = lblSTATE2h.Visible = lblPOSTALCODE2h.Visible = lblZIPCODE2h.Visible = lblMYCONLOCID2h.Visible = lblBUSPHONE12h.Visible = lblEMAIL22h.Visible = lblPRIMLANGUGE2h.Visible = lblWEBPAGE2h.Visible = lblISSMB2h.Visible = lblINHAWALLY2h.Visible = lblEMAISUB2h.Visible = lblEMAILSUBDATE2h.Visible = lblPRODUCTDEALIN2h.Visible = lblSALER2h.Visible = lblBUYER2h.Visible = lblFREELANCER2h.Visible = lblSALEDEPROD2h.Visible = lblCUSERID2h.Visible = lblCPASSWRD2h.Visible = lblUSERID2h.Visible = lblENTRYTIME2h.Visible = lblUPDTTIME2h.Visible = lblFaxID2h.Visible =  lblInstuctor_Username2h.Visible = lblContacType2h.Visible = lblBARCODE2h.Visible = true;
                    //2false
                    txtContactMyID2h.Visible = txtPersName12h.Visible = txtPersName22h.Visible = txtPersName32h.Visible = txtBirthDate2h.Visible = txtCivilID2h.Visible = txtEMAIL12h.Visible = txtMOBPHONE2h.Visible = txtITMANAGER2h.Visible = txtADDR12h.Visible = txtCOUNTRYID2h.Visible = txtIMG2h.Visible = txtActive2h.Visible = txtREMARKS2h.Visible = txtCRUP_ID2h.Visible = txtCOMPANYID2h.Visible = txtMYCATSUBID2h.Visible = txtMYPRODID2h.Visible = txtDESERIAL2h.Visible = txtCATID2h.Visible = txtCATTYPE2h.Visible = txtSUBCATID2h.Visible = txtPERSNAME2h.Visible = txtPERSNAMEO2h.Visible = txtPERSNAMEO22h.Visible = txtSUBCATTYPE2h.Visible = false;//= txtCONTACTID2h.Visible = txtPHYSICALLOCID2h.Visible = txtADDR22h.Visible = txtCITY2h.Visible = txtSTATE2h.Visible = txtPOSTALCODE2h.Visible = txtZIPCODE2h.Visible = txtMYCONLOCID2h.Visible = txtBUSPHONE12h.Visible = txtEMAIL22h.Visible = txtPRIMLANGUGE2h.Visible = txtWEBPAGE2h.Visible = txtISSMB2h.Visible = txtINHAWALLY2h.Visible = txtEMAISUB2h.Visible = txtEMAILSUBDATE2h.Visible = txtPRODUCTDEALIN2h.Visible = txtSALER2h.Visible = txtBUYER2h.Visible = txtFREELANCER2h.Visible = txtSALEDEPROD2h.Visible = txtCUSERID2h.Visible = txtCPASSWRD2h.Visible = txtUSERID2h.Visible = txtENTRYTIME2h.Visible = txtUPDTTIME2h.Visible = txtFaxID2h.Visible = txtInstuctor_Username2h.Visible = txtContacType2h.Visible = txtBARCODE2h.Visible = false;

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
                    lblContactMyID1s.Visible = lblPersName11s.Visible = lblPersName21s.Visible = lblPersName31s.Visible = lblBirthDate1s.Visible = lblCivilID1s.Visible = lblEMAIL11s.Visible = lblMOBPHONE1s.Visible = lblITMANAGER1s.Visible = lblADDR11s.Visible = lblCOUNTRYID1s.Visible = lblIMG1s.Visible = lblActive1s.Visible = lblREMARKS1s.Visible = lblCRUP_ID1s.Visible = lblCOMPANYID1s.Visible = lblMYCATSUBID1s.Visible = lblMYPRODID1s.Visible = lblDESERIAL1s.Visible = lblCATID1s.Visible = lblCATTYPE1s.Visible = lblSUBCATTYPE1s.Visible = lblSUBCATID1s.Visible = lblPERSNAME1s.Visible = lblPERSNAMEO1s.Visible = lblPERSNAMEO21s.Visible = false;//= lblCONTACTID1s.Visible = lblPHYSICALLOCID1s.Visible = lblADDR21s.Visible = lblCITY1s.Visible = lblSTATE1s.Visible = lblPOSTALCODE1s.Visible = lblZIPCODE1s.Visible = lblMYCONLOCID1s.Visible = lblBUSPHONE11s.Visible = lblEMAIL21s.Visible = lblPRIMLANGUGE1s.Visible = lblWEBPAGE1s.Visible = lblISSMB1s.Visible = lblINHAWALLY1s.Visible = lblEMAISUB1s.Visible = lblEMAILSUBDATE1s.Visible = lblPRODUCTDEALIN1s.Visible = lblSALER1s.Visible = lblBUYER1s.Visible = lblFREELANCER1s.Visible = lblSALEDEPROD1s.Visible = lblCUSERID1s.Visible = lblCPASSWRD1s.Visible = lblUSERID1s.Visible = lblENTRYTIME1s.Visible = lblUPDTTIME1s.Visible = lblFaxID1s.Visible = lblInstuctor_Username1s.Visible = lblContacType1s.Visible = lblBARCODE1s.Visible = false;
                    //1true
                    txtContactMyID1s.Visible = txtPersName11s.Visible = txtPersName21s.Visible = txtPersName31s.Visible = txtBirthDate1s.Visible = txtCivilID1s.Visible = txtEMAIL11s.Visible = txtMOBPHONE1s.Visible = txtITMANAGER1s.Visible = txtADDR11s.Visible = txtCOUNTRYID1s.Visible = txtIMG1s.Visible = txtActive1s.Visible = txtREMARKS1s.Visible = txtCRUP_ID1s.Visible = txtCOMPANYID1s.Visible = txtMYCATSUBID1s.Visible = txtMYPRODID1s.Visible = txtDESERIAL1s.Visible = txtCATID1s.Visible = txtCATTYPE1s.Visible = txtSUBCATTYPE1s.Visible = txtSUBCATID1s.Visible = txtPERSNAME1s.Visible = txtPERSNAMEO1s.Visible = txtPERSNAMEO21s.Visible = true;//= txtCONTACTID1s.Visible = txtPHYSICALLOCID1s.Visible = txtADDR21s.Visible = txtCITY1s.Visible = txtSTATE1s.Visible = txtPOSTALCODE1s.Visible = txtZIPCODE1s.Visible = txtMYCONLOCID1s.Visible = txtBUSPHONE11s.Visible = txtEMAIL21s.Visible = txtPRIMLANGUGE1s.Visible = txtWEBPAGE1s.Visible = txtISSMB1s.Visible = txtINHAWALLY1s.Visible = txtEMAISUB1s.Visible = txtEMAILSUBDATE1s.Visible = txtPRODUCTDEALIN1s.Visible = txtSALER1s.Visible = txtBUYER1s.Visible = txtFREELANCER1s.Visible = txtSALEDEPROD1s.Visible = txtCUSERID1s.Visible = txtCPASSWRD1s.Visible = txtUSERID1s.Visible = txtENTRYTIME1s.Visible = txtUPDTTIME1s.Visible = txtFaxID1s.Visible = txtInstuctor_Username1s.Visible = txtContacType1s.Visible = txtBARCODE1s.Visible = true;
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
                    lblContactMyID1s.Visible = lblPersName11s.Visible = lblPersName21s.Visible = lblPersName31s.Visible = lblBirthDate1s.Visible = lblCivilID1s.Visible = lblEMAIL11s.Visible = lblMOBPHONE1s.Visible = lblITMANAGER1s.Visible = lblADDR11s.Visible = lblCOUNTRYID1s.Visible = lblIMG1s.Visible = lblActive1s.Visible = lblREMARKS1s.Visible = lblCRUP_ID1s.Visible = lblCOMPANYID1s.Visible = lblMYCATSUBID1s.Visible = lblMYPRODID1s.Visible = lblDESERIAL1s.Visible = lblCATID1s.Visible = lblCATTYPE1s.Visible = lblSUBCATTYPE1s.Visible = lblSUBCATID1s.Visible = lblPERSNAME1s.Visible = lblPERSNAMEO1s.Visible = lblPERSNAMEO21s.Visible = true;//= lblCONTACTID1s.Visible = lblPHYSICALLOCID1s.Visible = lblADDR21s.Visible = lblCITY1s.Visible = lblSTATE1s.Visible = lblPOSTALCODE1s.Visible = lblZIPCODE1s.Visible = lblMYCONLOCID1s.Visible = lblBUSPHONE11s.Visible = lblEMAIL21s.Visible = lblPRIMLANGUGE1s.Visible = lblWEBPAGE1s.Visible = lblISSMB1s.Visible = lblINHAWALLY1s.Visible = lblEMAISUB1s.Visible = lblEMAILSUBDATE1s.Visible = lblPRODUCTDEALIN1s.Visible = lblSALER1s.Visible = lblBUYER1s.Visible = lblFREELANCER1s.Visible = lblSALEDEPROD1s.Visible = lblCUSERID1s.Visible = lblCPASSWRD1s.Visible = lblUSERID1s.Visible = lblENTRYTIME1s.Visible = lblUPDTTIME1s.Visible = lblFaxID1s.Visible = lblInstuctor_Username1s.Visible = lblContacType1s.Visible = lblBARCODE1s.Visible = true;
                    //1false
                    txtContactMyID1s.Visible = txtPersName11s.Visible = txtPersName21s.Visible = txtPersName31s.Visible = txtBirthDate1s.Visible = txtCivilID1s.Visible = txtEMAIL11s.Visible = txtMOBPHONE1s.Visible = txtITMANAGER1s.Visible = txtADDR11s.Visible = txtCOUNTRYID1s.Visible = txtIMG1s.Visible = txtActive1s.Visible = txtREMARKS1s.Visible = txtCRUP_ID1s.Visible = txtCOMPANYID1s.Visible = txtMYCATSUBID1s.Visible = txtMYPRODID1s.Visible = txtDESERIAL1s.Visible = txtCATID1s.Visible = txtCATTYPE1s.Visible = txtSUBCATTYPE1s.Visible = txtSUBCATID1s.Visible = txtPERSNAME1s.Visible = txtPERSNAMEO1s.Visible = txtPERSNAMEO21s.Visible = false;//= txtCONTACTID1s.Visible = txtPHYSICALLOCID1s.Visible = txtADDR21s.Visible = txtCITY1s.Visible = txtSTATE1s.Visible = txtPOSTALCODE1s.Visible = txtZIPCODE1s.Visible = txtMYCONLOCID1s.Visible = txtBUSPHONE11s.Visible = txtEMAIL21s.Visible = txtPRIMLANGUGE1s.Visible = txtWEBPAGE1s.Visible = txtISSMB1s.Visible = txtINHAWALLY1s.Visible = txtEMAISUB1s.Visible = txtEMAILSUBDATE1s.Visible = txtPRODUCTDEALIN1s.Visible = txtSALER1s.Visible = txtBUYER1s.Visible = txtFREELANCER1s.Visible = txtSALEDEPROD1s.Visible = txtCUSERID1s.Visible = txtCPASSWRD1s.Visible = txtUSERID1s.Visible = txtENTRYTIME1s.Visible = txtUPDTTIME1s.Visible = txtFaxID1s.Visible = txtInstuctor_Username1s.Visible = txtContacType1s.Visible = txtBARCODE1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }

        protected void LanguageEnglish_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }

        protected void LanguageArabic_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }

        protected void LanguageFrance_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
        }

        protected void drpPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlwarning.Visible = false;
            int vendid = 0;
            if (HiddenField3.Value == "")
            {
                vendid = Convert.ToInt32(lblHideID.Text);
            }
            else
            {
                vendid = Convert.ToInt32(HiddenField3.Value);
            }

            List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == vendid).ToList();
            int PackID = Convert.ToInt32(drpPackage.SelectedValue);
            if (ListDT.Count() > 0)
            {
                var obj = ListDT.Last();
                int MYID = Convert.ToInt32(obj.MYID);
                DateTime LDT1 = Convert.ToDateTime(obj.PlanEndDate);
                DateTime LDT2 = LDT1.AddDays(1);

                Database.TBLPRODUCT Prodobj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PackID);
                string MSRP = Prodobj.msrp.ToString();
                string[] MonthS = Prodobj.Warranty.ToString().Split(',');
                int ClacMonth = Convert.ToInt32(MonthS[0]);
                if (ClacMonth == 0)
                {
                    pnlwarning.Visible = true;
                    lblmsgw.Text = "Your package In Month is '0' Insert Month in package...";
                    return;
                }
                int Month = ClacMonth;//(ClacMonth - 1);

                lblmonth.Text = "Package in Months" + " " + ClacMonth.ToString();
                ViewState["Month"] = ClacMonth;

                txtAmount.Text = MSRP;

                DateTime StartDate = LDT2;
                txtpackStartdate.Text = StartDate.ToString("MMM/dd/yyyy");

                DateTime Enddate = StartDate.AddMonths(Month);
                int Eday = Convert.ToInt32(Enddate.Day);
                int Emonth = Convert.ToInt32(Enddate.Month);
                int Eyear = Convert.ToInt32(Enddate.Year);

                //int Esday = DateTime.DaysInMonth(Eyear, Emonth);
                //DateTime EDT = new DateTime(Eyear, Emonth, Esday);
                txtpackEnddate.Text = Enddate.ToString("MMM/dd/yyyy");//EDT.ToString("MM/dd/yyyy");
                Packagebind();
            }
            else
            {
                Database.TBLPRODUCT Prodobj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PackID);
                string MSRP = Prodobj.msrp.ToString();
                string[] MonthS = Prodobj.Warranty.ToString().Split(',');
                int ClacMonth = Convert.ToInt32(MonthS[0]);
                if (ClacMonth == 0)
                {
                    pnlwarning.Visible = true;
                    lblmsgw.Text = "Your package In Month is '0' Insert Month in package...";
                    return;
                }
                int Month = ClacMonth;//(ClacMonth - 1);

                lblmonth.Text = "Package in Months" + " " + ClacMonth.ToString();
                ViewState["Month"] = ClacMonth;

                txtAmount.Text = MSRP;

                //DateTime StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime StartDate = DateTime.Now;
                txtpackStartdate.Text = StartDate.ToString("MMM/dd/yyyy");

                DateTime Enddate = StartDate.AddMonths(Month);
                int Eday = Convert.ToInt32(Enddate.Day);
                int Emonth = Convert.ToInt32(Enddate.Month);
                int Eyear = Convert.ToInt32(Enddate.Year);

                //int Esday = DateTime.DaysInMonth(Eyear, Emonth);
                //DateTime EDT = new DateTime(Eyear, Emonth, Esday);
                txtpackEnddate.Text = Enddate.ToString("MMM/dd/yyyy");//EDT.ToString("MM/dd/yyyy");
                Packagebind();
            }

        }

        protected void txtpackStartdate_TextChanged(object sender, EventArgs e)
        {
            DateTime STD = Convert.ToDateTime(txtpackStartdate.Text);
            int Month = Convert.ToInt32(ViewState["Month"]);
            DateTime Enddate = STD.AddMonths(Month);
            txtpackEnddate.Text = Enddate.ToString("MMM/dd/yyyy");
        }
        public void Setpanel(string link, string panal)
        {
            pnlatt123.Style.Add("display", link);
            linkjAttendance.Attributes["class"] = panal;
        }
        public void setpanelmain(string link, string panal)
        {
            linkmain123.Attributes["class"] = panal;
            main123.Style.Add("display", link);
        }
        public void PackSetpanel(string link, string panal)
        {
            packHList123.Style.Add("display", link);
            packList123.Attributes["class"] = panal;
        }
        public void SetPanelMainList(string link, string panal)
        {
            MainListFC.Attributes["class"] = panal;
            MainListF.Style.Add("display", link);
        }
        protected void ListPackage_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnprient")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Mytranceid = Convert.ToInt32(ID[0]);
                int Packid = Convert.ToInt32(ID[1]);
                //int Mytranceid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("/Master/GYMPrint.aspx?Tranjestion=" + Mytranceid + "&Pack=" + Packid);
                //Response.Redirect("../ReportMst/GYMPrint.aspx?Tranjestion=" + Mytranceid + "&Pack=" + Packid);
            }
            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Mytranceid = Convert.ToInt32(ID[0]);
                int packID = Convert.ToInt32(ID[1]);
                int CIDD = Convert.ToInt32(ID[2]);
                TextBox txtsuppid = (TextBox)e.Item.FindControl("txtsuppid");
                TextBox txtsupppass = (TextBox)e.Item.FindControl("txtsupppass");

                string Cuser = txtsuppid.Text;
                string PASSWORD = Classes.GlobleClass.EncryptionHelpers.Encrypt(txtsupppass.Text);
                string Cpass = PASSWORD;
                if (DB.USER_MST.Where(p => p.TenentID == TID && p.LOGIN_ID == Cuser && p.PASSWORD == Cpass && p.CompId == 999).Count() > 0)
                {
                    int vendid = CIDD;
                    List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == vendid).ToList();
                    if (ListDT.Count() > 0)
                    {
                        lblHideID.Text = CIDD.ToString();
                        int PackID = packID;
                        //only Show
                        var obj = ListDT.Single(p => p.TenentID == TID && p.JOID == vendid && p.QUANTITY == PackID);
                        drpPackage.SelectedValue = PackID.ToString();
                        txtcont.Text = DB.TBLCONTACTs.Single(p => p.ContactMyID == CIDD && p.TenentID == TID).PersName1;//getContactName(Convert.ToInt32(CIDD));
                        txtAmount.Text = obj.AMOUNT.ToString();
                        txtpackStartdate.Text = Convert.ToDateTime(obj.PlanStartDate).ToString("MMM/dd/yyyy");
                        txtpackEnddate.Text = Convert.ToDateTime(obj.PlanEndDate).ToString("MMM/dd/yyyy");
                        lblmonth.Text = "Package in Months" + " " + obj.BIN_ID.ToString();
                        ViewState["MyTranIDD"] = Mytranceid;
                        ViewState["Month"] = obj.BIN_ID.ToString();

                        ViewState["SuppID"] = txtsuppid.Text;
                        ViewState["SuppPass"] = txtsupppass.Text;


                        btnpacksave.Text = "update";
                        btnSaveANDPrint.Text = "update & print";
                        //pnlSuppervicer.Visible = true;
                        pnlpack.Visible = true;


                        var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == Mytranceid && p.TenentID == TID).ToList();
                        if (listcost.Count() > 0)
                        {
                            pnlPaymentterm.Visible = true;
                            ViewState["TempEco_ICCATEGORY"] = listcost;
                            //Session["Invoice"] = ID;
                            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                            Repeater3.DataBind();
                        }
                        else
                        {
                            pnlPaymentterm.Visible = true;
                            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                            Repeater3.DataBind();
                        }
                        if (listcost.Where(p => p.MyTransID == Mytranceid && p.TenentID == TID && p.JVRefNo == "final").Count() > 0)
                        {
                            FinalPayment.Visible = false;
                        }
                        Setpanel("none", "expand");
                        //PackSetpanel("none", "expand");
                        setpanelmain("none", "expand");
                        SetPanelMainList("none", "expand");
                    }
                }
                else
                {
                    pnlwarning.Visible = true;
                    lblmsgw.Text = "Invalid Supperviser Login_ID & Password";
                    txtsuppid.Text = "";
                    txtsupppass.Text = "";
                }

            }
            if (e.CommandName == "btnNotPayedEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Mytranceid = Convert.ToInt32(ID[0]);
                int packID = Convert.ToInt32(ID[1]);
                int CIDD = Convert.ToInt32(ID[2]);

                int vendid = CIDD;
                List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == vendid).ToList();
                if (ListDT.Count() > 0)
                {
                    lblHideID.Text = CIDD.ToString();
                    int PackID = packID;
                    //only Show
                    var obj = ListDT.Single(p => p.TenentID == TID && p.JOID == vendid && p.QUANTITY == PackID);
                    drpPackage.SelectedValue = PackID.ToString();
                    txtcont.Text = DB.TBLCONTACTs.Single(p => p.ContactMyID == CIDD && p.TenentID == TID).PersName1;//getContactName(Convert.ToInt32(CIDD));
                    txtAmount.Text = obj.AMOUNT.ToString();
                    txtpackStartdate.Text = Convert.ToDateTime(obj.PlanStartDate).ToString("MMM/dd/yyyy");
                    txtpackEnddate.Text = Convert.ToDateTime(obj.PlanEndDate).ToString("MMM/dd/yyyy");
                    lblmonth.Text = "Package in Months" + " " + obj.BIN_ID.ToString();
                    ViewState["MyTranIDD"] = Mytranceid;
                    ViewState["Month"] = obj.BIN_ID.ToString();

                    btnpacksave.Text = "update";
                    btnSaveANDPrint.Text = "update & print";
                    pnlpack.Visible = true;

                    var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == Mytranceid && p.TenentID == TID).ToList();
                    if (listcost.Count() > 0)
                    {
                        pnlPaymentterm.Visible = true;
                        ViewState["TempEco_ICCATEGORY"] = listcost;
                        Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                        Repeater3.DataBind();
                    }
                    else
                    {
                        pnlPaymentterm.Visible = true;
                        Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                        Repeater3.DataBind();
                    }
                    if (listcost.Where(p => p.MyTransID == Mytranceid && p.TenentID == TID && p.JVRefNo == "final").Count() > 0)
                    {
                        FinalPayment.Visible = false;
                    }
                    Setpanel("none", "expand");
                    //PackSetpanel("none", "expand");
                    setpanelmain("none", "expand");
                    SetPanelMainList("none", "expand");
                }
            }
            if (e.CommandName == "btnDDelete")
            {
                pnlwarning.Visible = false;
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Mytranceid = Convert.ToInt32(ID[0]);
                int packID = Convert.ToInt32(ID[1]);
                int CIDD = Convert.ToInt32(ID[2]);
                if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == Mytranceid && p.JVRefNo == "final").Count() > 0)
                {
                    pnlwarning.Visible = true;
                    lblmsgw.Text = "Delete is not allowed for the PAID Customer...";
                }
                else
                {
                    Database.ICTR_DT DelobjDT = DB.ICTR_DT.Single(p => p.TenentID == TID && p.QUANTITY == packID && p.JOID == CIDD);
                    DB.ICTR_DT.DeleteObject(DelobjDT);
                    DB.SaveChanges();
                    Database.ICTR_HD DelobjHD = DB.ICTR_HD.Single(p => p.TenentID == TID && p.RefTransID == packID && p.CUSTVENDID == CIDD);
                    DB.ICTR_HD.DeleteObject(DelobjHD);
                    DB.SaveChanges();
                    if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == Mytranceid && p.JVRefNo == "GYM").Count() > 0) ;
                    {
                        Database.ICTRPayTerms_HD objpay = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == Mytranceid && p.JVRefNo == "GYM");
                        DB.ICTRPayTerms_HD.DeleteObject(objpay);
                        DB.SaveChanges();
                    }
                    Packagebind();
                    pnlwarning.Visible = true;
                    lblmsgw.Text = "Delete This package For This Customer...";
                }
            }
        }

        protected void btnaddamunt_Click(object sender, EventArgs e) // for Payment Method
        {
            List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
            Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            TempICTRPayTerms_HD = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Database.ICTRPayTerms_HD objEco_ICEXTRACOST = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICEXTRACOST);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            Repeater3.DataSource = TempICTRPayTerms_HD;
            Repeater3.DataBind();
        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e) // for Payment Method
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");

                DropDownList drppaymentmethod = (DropDownList)e.Item.FindControl("drppaymentmethod");
                drppaymentmethod.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method" && p.SHORTNAME == "GYM");
                drppaymentmethod.DataTextField = "REFNAME1";
                drppaymentmethod.DataValueField = "REFID";
                drppaymentmethod.DataBind();
                drppaymentmethod.Items.Insert(0, new ListItem("-- Select --", "0"));
                drppaymentmethod.SelectedValue = "2081";
                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    drppaymentmethod.SelectedValue = ID.ToString();
                }
            }
        }

        protected void Repeater3_ItemCommand(object source, RepeaterCommandEventArgs e) // for Payment Method
        {
            if (e.CommandName == "FinalPayment")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int mytranceid = Convert.ToInt32(ID[0]);
                //int PaymentTermsID = Convert.ToInt32(ID[1]);
                DropDownList drppaymentmethod = (DropDownList)e.Item.FindControl("drppaymentmethod");
                TextBox txtammunt = (TextBox)e.Item.FindControl("txtammunt");
                TextBox txtrefresh = (TextBox)e.Item.FindControl("txtrefresh");
                decimal AMOUNT = Convert.ToDecimal(txtammunt.Text);
                int PaymentTermsID = Convert.ToInt32(drppaymentmethod.SelectedValue);

                string RFresh = txtrefresh.Text.ToString();
                string[] id = RFresh.Split(',');
                string IDRefresh = id[0].ToString();
                string IdApprouv = id[1].ToString();

                if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == mytranceid && p.PaymentTermsId == PaymentTermsID).Count() > 0)
                {
                    ICTRPayTerms_HD objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == mytranceid && p.PaymentTermsId == PaymentTermsID);
                    objICTRPayTerms_HD.TenentID = TID;
                    objICTRPayTerms_HD.LocationID = 1;
                    objICTRPayTerms_HD.MyTransID = mytranceid;
                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsID;
                    objICTRPayTerms_HD.CashBankChequeID = 0;
                    objICTRPayTerms_HD.CounterID = "1";
                    objICTRPayTerms_HD.TransDate = DateTime.Now;
                    objICTRPayTerms_HD.CheckOutDate = null;// come from Cash Delivery Popup
                    objICTRPayTerms_HD.AccountantID = 0;// come from Cash Delivery Popup
                    objICTRPayTerms_HD.AccountID = 0;
                    objICTRPayTerms_HD.CRUP_ID = 0;
                    objICTRPayTerms_HD.Notes = "GYM";
                    objICTRPayTerms_HD.Amount = AMOUNT;
                    objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    objICTRPayTerms_HD.ChequeVerified = false;
                    objICTRPayTerms_HD.CashVerified = false;
                    objICTRPayTerms_HD.ATMVerified = false;
                    objICTRPayTerms_HD.VoucharVerified = false;
                    objICTRPayTerms_HD.ChequeVerifiedDate = null;
                    objICTRPayTerms_HD.CashVerifiedDate = null;
                    objICTRPayTerms_HD.ATMVerifiedDate = null;
                    objICTRPayTerms_HD.VoucharVerifiedDate = null;
                    objICTRPayTerms_HD.JVRefNo = "final";

                    DB.SaveChanges();
                }

                //Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, mytranceid, PaymentTermsID, "1", 1, 0, AMOUNT, IDRefresh, null, null, "GYM", 0, 0, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
            }
        }

        protected void FinalPayment_Click(object sender, EventArgs e)
        {
            bool falg = false;
            int mytranceid = 0;
            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                Label lblpaymytranceid = (Label)Repeater3.Items[i].FindControl("lblpaymytranceid");
                DropDownList drppaymentmethod = (DropDownList)Repeater3.Items[i].FindControl("drppaymentmethod");
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                decimal AMOUNT = Convert.ToDecimal(txtammunt.Text);
                int PaymentTermsID = Convert.ToInt32(drppaymentmethod.SelectedValue);
                if (falg == false)
                {
                    mytranceid = Convert.ToInt32(lblpaymytranceid.Text);
                    falg = true;
                }

                string RFresh = txtrefresh.Text.ToString();
                string[] id = RFresh.Split(',');
                string IDRefresh = id[0].ToString();
                string IdApprouv = id[1].ToString();
                if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == mytranceid && p.JVRefNo == "GYM").Count() > 0)
                {
                    Database.ICTRPayTerms_HD DELobj = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == mytranceid && p.JVRefNo == "GYM");
                    DB.ICTRPayTerms_HD.DeleteObject(DELobj);
                    DB.SaveChanges();
                }
                if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == mytranceid && p.PaymentTermsId == PaymentTermsID).Count() > 0)
                {
                    ICTRPayTerms_HD objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == mytranceid && p.PaymentTermsId == PaymentTermsID);
                    objICTRPayTerms_HD.TenentID = TID;
                    objICTRPayTerms_HD.LocationID = 1;
                    objICTRPayTerms_HD.MyTransID = mytranceid;
                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsID;
                    objICTRPayTerms_HD.CashBankChequeID = 0;
                    objICTRPayTerms_HD.CounterID = "1";
                    objICTRPayTerms_HD.TransDate = DateTime.Now;
                    objICTRPayTerms_HD.CheckOutDate = null;// come from Cash Delivery Popup
                    objICTRPayTerms_HD.AccountantID = 0;// come from Cash Delivery Popup
                    objICTRPayTerms_HD.AccountID = 0;
                    objICTRPayTerms_HD.CRUP_ID = 0;
                    objICTRPayTerms_HD.Notes = "GYM";
                    objICTRPayTerms_HD.Amount = AMOUNT;
                    objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    objICTRPayTerms_HD.ChequeVerified = false;
                    objICTRPayTerms_HD.CashVerified = false;
                    objICTRPayTerms_HD.ATMVerified = false;
                    objICTRPayTerms_HD.VoucharVerified = false;
                    objICTRPayTerms_HD.ChequeVerifiedDate = null;
                    objICTRPayTerms_HD.CashVerifiedDate = null;
                    objICTRPayTerms_HD.ATMVerifiedDate = null;
                    objICTRPayTerms_HD.VoucharVerifiedDate = null;
                    objICTRPayTerms_HD.JVRefNo = "final";

                    DB.SaveChanges();
                }
                else
                {
                    ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                    objICTRPayTerms_HD.TenentID = TID;
                    objICTRPayTerms_HD.LocationID = 1;
                    objICTRPayTerms_HD.MyTransID = mytranceid;
                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsID;
                    objICTRPayTerms_HD.CashBankChequeID = 0;
                    objICTRPayTerms_HD.CounterID = "1";
                    objICTRPayTerms_HD.TransDate = DateTime.Now;
                    objICTRPayTerms_HD.CheckOutDate = null;
                    objICTRPayTerms_HD.AccountantID = 0;
                    objICTRPayTerms_HD.AccountID = 0;
                    objICTRPayTerms_HD.CRUP_ID = 0;
                    objICTRPayTerms_HD.Notes = "GYM";
                    objICTRPayTerms_HD.Amount = AMOUNT;
                    objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    objICTRPayTerms_HD.ChequeVerified = false;
                    objICTRPayTerms_HD.CashVerified = false;
                    objICTRPayTerms_HD.ATMVerified = false;
                    objICTRPayTerms_HD.VoucharVerified = false;
                    objICTRPayTerms_HD.ChequeVerifiedDate = null;
                    objICTRPayTerms_HD.CashVerifiedDate = null;
                    objICTRPayTerms_HD.ATMVerifiedDate = null;
                    objICTRPayTerms_HD.VoucharVerifiedDate = null;
                    objICTRPayTerms_HD.JVRefNo = "final";

                    DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    DB.SaveChanges();
                }
            }
        }

        protected void ListPackage_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label mytranceidd = (Label)e.Item.FindControl("mytranceidd");
            LinkButton btnEdit1 = (LinkButton)e.Item.FindControl("btnEdit1");
            LinkButton btnNotPayedEdit = (LinkButton)e.Item.FindControl("btnNotPayedEdit");

            int MytranID = Convert.ToInt32(mytranceidd.Text);
            if (DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MytranID && p.JVRefNo == "final").Count() > 0)
            {
                btnEdit1.Visible = true;
                btnNotPayedEdit.Visible = false;
            }
            else
            {
                btnEdit1.Visible = false;
                btnNotPayedEdit.Visible = true;
            }
            Label lblmtid = (Label)e.Item.FindControl("lblmtid");
            Label lblCUSTIDD = (Label)e.Item.FindControl("lblCUSTIDD");
            int myid = Convert.ToInt32(lblmtid.Text);
            int CUSTVENDID = Convert.ToInt32(lblCUSTIDD.Text);
            DateTime TodayDate = DateTime.Now;
            Database.ICTR_DT DTobj = DB.ICTR_DT.Single(p => p.TenentID == TID && p.JOID == CUSTVENDID && p.MYID == myid && p.MYTRANSID == MytranID);
            DateTime SDT = Convert.ToDateTime(DTobj.PlanStartDate);
            DateTime EDT = Convert.ToDateTime(DTobj.PlanEndDate);
            HtmlTableRow packCID = e.Item.FindControl("packCID") as HtmlTableRow;//ContentPlaceHolder1_ListPackage_packCID_0
            if (TodayDate >= SDT && TodayDate <= EDT)
            {
                packCID.BgColor = "lightskyblue";
            }
            else if (EDT > TodayDate)
            {
                packCID.BgColor = "lightgoldenrodyellow"; //lightpink
            }
            else
            {
                packCID.BgColor = "lightpink";
            }
        }

        protected void txtammunt_TextChanged(object sender, EventArgs e)
        {
            decimal FAmount = 0;
            decimal TotAMT = Convert.ToDecimal(txtAmount.Text);
            int RCount = Convert.ToInt32(Repeater3.Items.Count);
            int CO = 1;

            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                if (txtammunt.Text != "")
                {
                    decimal payAmount = Convert.ToDecimal(txtammunt.Text);
                    FAmount = FAmount + payAmount;
                    CO = RCount - 1;
                }
                else
                {
                    decimal B_AMT = TotAMT - FAmount;
                    decimal Devide = B_AMT / CO;
                    txtammunt.Text = Devide.ToString("N3");
                }
            }
            if (FAmount > TotAMT)
            {
                lblpackMSG.Text = "AMount Is More Than To Actual Amount...";
            }
        }
        protected void btnTot_Click(object sender, EventArgs e)
        {
            //var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            //ViewState["SaveList"] = List;
            ViewState["Active"] = "ALL";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                // BindEditMode(List[0].ContactMyID);
                int Showdata = 20;//Convert.ToInt32(drpShowGrid.SelectedValue);

                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, DB.TBLCONTACTs.Where(p => p.TenentID == TID).Take(1).ToList());
            }
            //ViewState["Active"] = "ALL";
            ModalPopupExtender4.Hide();
        }

        protected void btnAct_Click(object sender, EventArgs e)
        {
            List<Database.TBLCONTACT> newlist = new List<Database.TBLCONTACT>();
            //var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            //ViewState["SaveList"] = List;            
            ViewState["Active"] = "YES";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                //BindEditMode(List[0].ContactMyID);
                int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);                
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, newlist);
            }
            //ViewState["Active"] = "YES";
            ModalPopupExtender4.Hide();
        }

        protected void btnInact_Click(object sender, EventArgs e)
        {
            List<Database.TBLCONTACT> newlist = new List<Database.TBLCONTACT>();
            //var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            //ViewState["SaveList"] = List;
            ViewState["Active"] = "NO";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                //BindEditMode(List[0].ContactMyID);
                int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);                
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, newlist);
            }
            //ViewState["Active"] = "NO";
            ModalPopupExtender4.Hide();
        }
        protected void LinkTotalRecords_Click(object sender, EventArgs e)
        {
            //var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            //ViewState["SaveList"] = List;
            ViewState["Active"] = "ALL";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                // BindEditMode(List[0].ContactMyID);
                int Showdata = 20;//Convert.ToInt32(drpShowGrid.SelectedValue);

                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, DB.TBLCONTACTs.Where(p => p.TenentID == TID).Take(1).ToList());
            }
            //ViewState["Active"] = "ALL";
            ModalPopupExtender4.Hide();
        }

        protected void LinkTotalActive_Click(object sender, EventArgs e)
        {
            List<Database.TBLCONTACT> newlist = new List<Database.TBLCONTACT>();
            //var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            //ViewState["SaveList"] = List;            
            ViewState["Active"] = "YES";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                //BindEditMode(List[0].ContactMyID);
                int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);                
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, newlist);
            }
            //ViewState["Active"] = "YES";
            ModalPopupExtender4.Hide();
        }

        protected void LinkTotalInactive_Click(object sender, EventArgs e)
        {
            List<Database.TBLCONTACT> newlist = new List<Database.TBLCONTACT>();
            //var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList();
            //ViewState["SaveList"] = List;
            ViewState["Active"] = "NO";
            ViewState["SerchListTabel"] = null;
            int Totalrec = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.UPDTTIME).ToList().Count();
            if (Totalrec > 0)
            {
                //BindEditMode(List[0].ContactMyID);
                int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);                
                Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, newlist);
            }
            //ViewState["Active"] = "NO";
            ModalPopupExtender4.Hide();
        }
        public void ReadOnlyPack(int VID)
        {
            txtCustoID.Enabled = false;
            decimal ContactMyID = Convert.ToDecimal(VID);
            BindEditMode(ContactMyID);
            redonlyfales();
            Session["ADMInPrevious"] = Session["ADMInCurrent"];
            Session["ADMInCurrent"] = Request.RawUrl;
            btnSubmit.Visible = false;
            LinkSave.Visible = false;
            Button1.Visible = true;
            LinkAddnew.Visible = true; ;
            lblBusContactDe.Text = "(Read Mode)";
        }

        protected void LinkCustomerSearch_Click(object sender, EventArgs e)
        {

            //pnlpack.Visible = true;
            //txtcont.Text = txtContactName.Text.ToString();
            lblHideID.Text = txtCustoID.Text;
            lblpackMSG.Text = "";
            PackClear();

            pnlwarning.Visible = false;
            lblpackMSG.Text = "";
            PackClear();
            string val = HiddenField3.Value.ToString();
            string SearchID = txtCustomerSearch.Text.Trim().ToString();
            txtcont.Text = SearchID;
            int vendid = 0;
            if (SearchID != "")
            {
                List<Database.TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && (p.PersName1.ToUpper().Contains(SearchID.ToUpper()) || p.PersName2.ToUpper().Contains(SearchID.ToUpper()) || p.PersName3.ToUpper().Contains(SearchID.ToUpper()) || p.CivilID.ToUpper().Contains(SearchID.ToUpper()) || p.MOBPHONE.Contains(SearchID))).ToList();
                foreach (Database.TBLCONTACT item in List)
                {
                    if (List.Where(p => p.TenentID == TID && p.PersName1 == item.PersName1).Count() == 1)
                    {
                        Database.TBLCONTACT obj = List.Single(p => p.TenentID == TID && p.PersName1 == item.PersName1);
                        if (val != "")
                        {
                            vendid = Convert.ToInt32(val);
                        }
                        else
                        {
                            vendid = Convert.ToInt32(obj.ContactMyID);
                            txtcont.Text = "";
                            txtcont.Text = obj.PersName1.ToString();
                        }
                        ReadOnlyPack(vendid);
                        lblHideID.Text = vendid.ToString();
                        List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == vendid).ToList();

                        if (ListHD.Count() == 1)
                        {
                            List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.JOID == vendid).ToList();
                            var objDDT = ListDT.First();
                            drpPackage.SelectedValue = objDDT.QUANTITY.ToString();//objHD.RefTransID.ToString();
                            txtAmount.Text = objDDT.AMOUNT.ToString();//objHD.TOTAMT.ToString();
                            txtpackStartdate.Text = Convert.ToDateTime(objDDT.PlanStartDate).ToString("MMM/dd/yyyy");//Convert.ToDateTime(objHD.ENTRYDATE).ToShortDateString();
                            txtpackEnddate.Text = Convert.ToDateTime(objDDT.PlanEndDate).ToString("MMM/dd/yyyy");//Convert.ToDateTime(objHD.ENTRYTIME).ToShortDateString();
                            lblmonth.Text = "Package in Months" + " " + objDDT.BIN_ID.ToString();
                            lblpackMSG.Text = "Customer invoice Allready Exist...";
                        }
                        else
                        {
                            PackClear();
                            lblpackMSG.Text = "Please Fill Data And Add Invoice";
                            ListPackage.DataSource = null;
                            ListPackage.DataBind();
                        }
                        Packagebind();
                    }
                    else
                    {
                        lblpackMSG.Text = "Customer Not Found...";
                        ListPackage.DataSource = null;
                        ListPackage.DataBind();
                    }
                }

            }
            else
            {
                lblpackMSG.Text = "Please Type Name of Customer & search...";
                ListPackage.DataSource = null;
                ListPackage.DataBind();
            }


        }

        public void FillCUSPackage()
        {
            //List<Database.TBLCOMPANYSETUP> ListCustomer = new List<Database.TBLCOMPANYSETUP>();
            //List<Database.tblcontact_addon1> FinelCustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active == true).ToList();

            //foreach (Database.tblcontact_addon1 Citem in FinelCustomer)
            //{
            //    if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == Citem.CustomerId && p.active == true).Count() > 0)
            //    {
            //        Database.TBLCOMPANYSETUP Cobj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == Citem.CustomerId);
            //        ListCustomer.Add(Cobj);
            //    }
            //}

            var result = (from AddOn in DB.tblcontact_addon1
                          join CUS in DB.TBLCOMPANYSETUPs on AddOn.CustomerId equals CUS.COMPID
                          where AddOn.TenentID == TID && CUS.TenentID == TID && AddOn.active == true
                          select new
                          {
                              CUS.COMPNAME1,
                              CUS.COMPID
                          }).ToList();
            var CLIST = result.GroupBy(p => p.COMPID).Select(p => p.FirstOrDefault()).ToList();
            drpCustomer.DataSource = CLIST;
            drpCustomer.DataTextField = "COMPNAME1";
            drpCustomer.DataValueField = "COMPID";
            drpCustomer.DataBind();
            drpCustomer.Items.Insert(0, new ListItem("-- select --", "0"));

            var plan = (from PP in DB.planmealcustinvoiceHDs
                        join pm in DB.tblProduct_Plan on PP.planid equals pm.planid
                        where PP.TenentID == TID && pm.TenentID == TID
                        select new
                        {
                            pm.planname1,
                            pm.planid
                        }).ToList();

            var Cplan = plan.GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
            drpCUSpackage.DataSource = plan;
            drpCUSpackage.DataTextField = "planname1";
            drpCUSpackage.DataValueField = "planid";
            drpCUSpackage.DataBind();
            drpCUSpackage.Items.Insert(0, new ListItem("-- select --", "0"));
        }

        protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CCID = Convert.ToInt32(drpCustomer.SelectedValue);
            var List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.COMPANYID == CCID).ToList();
            ViewState["SaveList"] = List;
            int Showdata = 20;// Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview2, ListView3, Totalrec, List);
            ViewState["SerchListTabel"] = List;
        }

        public void Loadlist<T>(int Showdata, int take, int Skip, int ChoiceID, Label lblShowinfEntry, LinkButton btnPrevious, LinkButton btnNext, ListView Listview1, ListView ListView2, int Totalrec, List<T> List)
        {
            Panel8.Visible = false;
            string serchlistcount = "";
            List<Database.TBLCONTACT> SearchList = new List<Database.TBLCONTACT>();
            if (ViewState["SerchListTabel"] != null)
            {
                SearchList = ((List<Database.TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                serchlistcount = ViewState["SaveListCount"].ToString();
            }
            btnPrevious.Enabled = false;
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                if (ViewState["SerchListTabel"] != null)
                {
                    BindList((SearchList.Take(take).Skip(Skip)).ToList());
                    Panel8.Visible = true;
                    Label4.Text = "Displaying Search" + serchlistcount + "Customer Records";
                }
                else if (ViewState["Active"] != null)
                {
                    string Stat = ViewState["Active"].ToString();
                    if (Stat == "YES")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                        Panel8.Visible = true;
                        Label4.Text = "Displaying Active <u>" + LinkTotalActive.Text + "</u> Customer Records";
                    }
                    else if (Stat == "NO")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                        Panel8.Visible = true;
                        Label4.Text = "Displaying Inactive <u>" + LinkTotalInactive.Text + "</u> Customer Records";
                    }
                    else
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                        Panel8.Visible = true;
                        Label4.Text = "Displaying Total <u>" + LinkTotalRecords.Text + "</u> Customer Records";
                    }
                    ViewState["Take"] = take;
                    ViewState["Skip"] = Skip;
                }
            }
            else
            {
                if (ViewState["SerchListTabel"] != null)
                {
                    BindList((SearchList.Take(Showdata).Skip(0)).ToList());
                    Panel8.Visible = true;
                    Label4.Text = "Displaying Search <u>" + serchlistcount + "</b> Customer Records";
                }
                else if (ViewState["Active"] != null)
                {
                    string Stat = ViewState["Active"].ToString();
                    if (Stat == "YES")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(Showdata).Skip(0)).ToList());
                        Panel8.Visible = true;
                        Label4.Text = "Displaying Active <u>" + LinkTotalActive.Text + "</u> Customer Records";
                    }
                    else if (Stat == "NO")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(Showdata).Skip(0)).ToList());
                        Panel8.Visible = true;
                        Label4.Text = "Displaying Inactive <u>" + LinkTotalInactive.Text + "</u> Customer Records";
                    }
                    else
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(Showdata).Skip(0)).ToList());
                        Panel8.Visible = true;
                        Label4.Text = "Displaying Total <u>" + LinkTotalRecords.Text + "</u> Customer Records";
                    }
                    ViewState["Take"] = Showdata;
                    ViewState["Skip"] = 0;
                }
            }
            ChoiceList.Clear();
            // int ChoiceID=
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();

            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                take = Showdata;
                Skip = 0;
                if (ViewState["SerchListTabel"] != null)
                {
                    BindList((SearchList.Take(take).Skip(Skip)).ToList());
                    //BindList((Titel.Take(take).Skip(Skip)).ToList());
                }
                else if (ViewState["Active"] != null)
                {
                    string Stat = ViewState["Active"].ToString();
                    if (Stat == "YES")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "Y" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                    else if (Stat == "NO")
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.Active == "N" && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                    else
                    {
                        BindList((DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContacType == 82003).OrderByDescending(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
                    }
                }
                //BindList(Listview1, (List.Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious.Enabled = false;
                ChoiceID = 0;
                GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext.Enabled = false;
                else
                    btnNext.Enabled = true;

            }
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        }
        #region Step5 Navigation
        public void Navigation()
        {
            //Navigati0n
            Listview2.Items.Clear();
            if (ViewState["NDATA"] != null)
            {
                ChoiceList = (List<Navigation>)ViewState["NDATA"];
            }
            else
            {
                ChoiceList = new List<Navigation>();
            }
            ListView3.DataSource = ChoiceList;
            ListView3.DataBind();
        }
        #endregion
        #region Step6 GetCurrentNavigation
        public void GetCurrentNavigation(int ChoiceID, int Showdata, ListView ListView2, int Totalrec)
        {
            int lastchoise = 0;
            ChoiceID = ChoiceID - 12;
            if (ChoiceID <= 0)
            {
                ChoiceID = 0;
            }
            ChoiceList.Clear();
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                lastchoise = Math.Abs(Totalrec / Showdata) + 2;
                if (lastchoise == ChoiceID)
                    break;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();
        }
        public void GetCurrentNavigationLast(int ChoiceID, int Showdata, ListView ListView2, int Totalrec)
        {
            int lastchoise = 0;
            ChoiceID = ChoiceID - Showdata;
            if (ChoiceID <= 0)
            {
                ChoiceID = 0;
            }
            ChoiceList.Clear();
            for (int i = 0; i < 5; i++)
            {
                ChoiceID++;
                lastchoise = Math.Abs(Totalrec / Showdata) + 2;
                if (lastchoise == ChoiceID)
                    break;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation();
        }
        #endregion
        //public void BindList<T>(ListView Listview1, List<T> List)
        //{

        //    Listview1.DataSource = List;
        //    Listview1.DataBind();

        //}

        public void BindList(List<Database.TBLCONTACT> List)
        {

            Listview2.DataSource = List;
            Listview2.DataBind();

        }









    }
}