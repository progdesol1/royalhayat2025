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

namespace Web.Master
{
    public partial class tblCOUNTRY : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Session["LANGUAGE"] = "en-US";
                Readonly();
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "Submit";
            }
        }
        #region Step2
        public void BindData()
        {
            //List<Database.tblCOUNTRY> List = DB.tblCOUNTRies.Where(p=>p.TenentID == TID).OrderBy(m => m.COUNTRYID).ToList();
            Listview1.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID).OrderBy(m => m.COUNTRYID).OrderByDescending(m => m.COUNTRYID);
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion
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
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }
        public void GetShow()
        {

            lblTenantID1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblREGION11s.Attributes["class"] = lblCOUNAME11s.Attributes["class"] = lblCOUNAME21s.Attributes["class"] = lblCOUNAME31s.Attributes["class"] = lblCAPITAL1s.Attributes["class"] = lblNATIONALITY11s.Attributes["class"] = lblNATIONALITY21s.Attributes["class"] = lblNATIONALITY31s.Attributes["class"] = lblCURRENCYNAME11s.Attributes["class"] = lblCURRENCYNAME21s.Attributes["class"] = lblCURRENCYNAME31s.Attributes["class"] = lblCURRENTCONVRATE1s.Attributes["class"] = lblCURRENCYSHORTNAME11s.Attributes["class"] = lblCURRENCYSHORTNAME21s.Attributes["class"] = lblCURRENCYSHORTNAME31s.Attributes["class"] = lblCountryType1s.Attributes["class"] = lblCountryTSubType1s.Attributes["class"] = lblSovereignty1s.Attributes["class"] = lblISO4217CurCode1s.Attributes["class"] = lblISO4217CurName1s.Attributes["class"] = lblITUTTelephoneCode1s.Attributes["class"] = lblFaxLength1s.Attributes["class"] = lblTelLength1s.Attributes["class"] = lblISO3166_1_2LetterCode1s.Attributes["class"] = lblISO3166_1_3LetterCode1s.Attributes["class"] = lblISO3166_1Number1s.Attributes["class"] = lblIANACountryCodeTLD1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblTenantID2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblREGION12h.Attributes["class"] = lblCOUNAME12h.Attributes["class"] = lblCOUNAME22h.Attributes["class"] = lblCOUNAME32h.Attributes["class"] = lblCAPITAL2h.Attributes["class"] = lblNATIONALITY12h.Attributes["class"] = lblNATIONALITY22h.Attributes["class"] = lblNATIONALITY32h.Attributes["class"] = lblCURRENCYNAME12h.Attributes["class"] = lblCURRENCYNAME22h.Attributes["class"] = lblCURRENCYNAME32h.Attributes["class"] = lblCURRENTCONVRATE2h.Attributes["class"] = lblCURRENCYSHORTNAME12h.Attributes["class"] = lblCURRENCYSHORTNAME22h.Attributes["class"] = lblCURRENCYSHORTNAME32h.Attributes["class"] = lblCountryType2h.Attributes["class"] = lblCountryTSubType2h.Attributes["class"] = lblSovereignty2h.Attributes["class"] = lblISO4217CurCode2h.Attributes["class"] = lblISO4217CurName2h.Attributes["class"] = lblITUTTelephoneCode2h.Attributes["class"] = lblFaxLength2h.Attributes["class"] = lblTelLength2h.Attributes["class"] = lblISO3166_1_2LetterCode2h.Attributes["class"] = lblISO3166_1_3LetterCode2h.Attributes["class"] = lblISO3166_1Number2h.Attributes["class"] = lblIANACountryCodeTLD2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblTenantID1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblREGION11s.Attributes["class"] = lblCOUNAME11s.Attributes["class"] = lblCOUNAME21s.Attributes["class"] = lblCOUNAME31s.Attributes["class"] = lblCAPITAL1s.Attributes["class"] = lblNATIONALITY11s.Attributes["class"] = lblNATIONALITY21s.Attributes["class"] = lblNATIONALITY31s.Attributes["class"] = lblCURRENCYNAME11s.Attributes["class"] = lblCURRENCYNAME21s.Attributes["class"] = lblCURRENCYNAME31s.Attributes["class"] = lblCURRENTCONVRATE1s.Attributes["class"] = lblCURRENCYSHORTNAME11s.Attributes["class"] = lblCURRENCYSHORTNAME21s.Attributes["class"] = lblCURRENCYSHORTNAME31s.Attributes["class"] = lblCountryType1s.Attributes["class"] = lblCountryTSubType1s.Attributes["class"] = lblSovereignty1s.Attributes["class"] = lblISO4217CurCode1s.Attributes["class"] = lblISO4217CurName1s.Attributes["class"] = lblITUTTelephoneCode1s.Attributes["class"] = lblFaxLength1s.Attributes["class"] = lblTelLength1s.Attributes["class"] = lblISO3166_1_2LetterCode1s.Attributes["class"] = lblISO3166_1_3LetterCode1s.Attributes["class"] = lblISO3166_1Number1s.Attributes["class"] = lblIANACountryCodeTLD1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblTenantID2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblREGION12h.Attributes["class"] = lblCOUNAME12h.Attributes["class"] = lblCOUNAME22h.Attributes["class"] = lblCOUNAME32h.Attributes["class"] = lblCAPITAL2h.Attributes["class"] = lblNATIONALITY12h.Attributes["class"] = lblNATIONALITY22h.Attributes["class"] = lblNATIONALITY32h.Attributes["class"] = lblCURRENCYNAME12h.Attributes["class"] = lblCURRENCYNAME22h.Attributes["class"] = lblCURRENCYNAME32h.Attributes["class"] = lblCURRENTCONVRATE2h.Attributes["class"] = lblCURRENCYSHORTNAME12h.Attributes["class"] = lblCURRENCYSHORTNAME22h.Attributes["class"] = lblCURRENCYSHORTNAME32h.Attributes["class"] = lblCountryType2h.Attributes["class"] = lblCountryTSubType2h.Attributes["class"] = lblSovereignty2h.Attributes["class"] = lblISO4217CurCode2h.Attributes["class"] = lblISO4217CurName2h.Attributes["class"] = lblITUTTelephoneCode2h.Attributes["class"] = lblFaxLength2h.Attributes["class"] = lblTelLength2h.Attributes["class"] = lblISO3166_1_2LetterCode2h.Attributes["class"] = lblISO3166_1_3LetterCode2h.Attributes["class"] = lblISO3166_1Number2h.Attributes["class"] = lblIANACountryCodeTLD2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpCOUNTRYID.SelectedIndex = 0;
            txtREGION1.Text = "";
            txtCOUNAME1.Text = "";
            txtCOUNAME2.Text = "";
            txtCOUNAME3.Text = "";
            txtCAPITAL.Text = "";
            txtNATIONALITY1.Text = "";
            txtNATIONALITY2.Text = "";
            txtNATIONALITY3.Text = "";
            txtCURRENCYNAME1.Text = "";
            txtCURRENCYNAME2.Text = "";
            txtCURRENCYNAME3.Text = "";
            txtCURRENTCONVRATE.Text = "";
            txtCURRENCYSHORTNAME1.Text = "";
            txtCURRENCYSHORTNAME2.Text = "";
            txtCURRENCYSHORTNAME3.Text = "";
            drpCountryType.SelectedIndex = 0;
            txtCountryTSubType.Text = "";
            txtSovereignty.Text = "";
            txtISO4217CurCode.Text = "";
            txtISO4217CurName.Text = "";
            txtITUTTelephoneCode.Text = "";
            txtFaxLength.Text = "";
            txtTelLength.Text = "";
            txtISO3166_1_2LetterCode.Text = "";
            txtISO3166_1_3LetterCode.Text = "";
            txtISO3166_1Number.Text = "";
            txtIANACountryCodeTLD.Text = "";
            //cbActive.Checked = false;
            //txtCRUP_ID.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                if (btnAdd.Text == "Add New")
                {

                    Write();
                    Clear();
                    btnAdd.Text = "Save";
                    btnAdd.ValidationGroup = "s";
                }
                else if (btnAdd.Text == "Save")
                {
                    if (DB.tblCOUNTRies.Where(p => p.COUNAME1.ToUpper() == txtREGION1.Text.ToUpper() && p.TenentID == TID).Count() < 1)
                    {

                        Database.tblCOUNTRY objtblCOUNTRY = new Database.tblCOUNTRY();
                        //Server Content Send data Yogesh                        
                        objtblCOUNTRY.TenentID = TID;
                        objtblCOUNTRY.COUNTRYID = DB.tblCOUNTRies.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblCOUNTRies.Where(p => p.TenentID == TID).Max(p => p.COUNTRYID) + 1) : 1;
                        objtblCOUNTRY.REGION1 = txtREGION1.Text;
                        objtblCOUNTRY.COUNAME1 = txtCOUNAME1.Text;
                        objtblCOUNTRY.COUNAME2 = txtCOUNAME2.Text;
                        objtblCOUNTRY.COUNAME3 = txtCOUNAME3.Text;
                        objtblCOUNTRY.CAPITAL = txtCAPITAL.Text;
                        objtblCOUNTRY.NATIONALITY1 = txtNATIONALITY1.Text;
                        objtblCOUNTRY.NATIONALITY2 = txtNATIONALITY2.Text;
                        objtblCOUNTRY.NATIONALITY3 = txtNATIONALITY3.Text;
                        objtblCOUNTRY.CURRENCYNAME1 = txtCURRENCYNAME1.Text;
                        objtblCOUNTRY.CURRENCYNAME2 = txtCURRENCYNAME2.Text;
                        objtblCOUNTRY.CURRENCYNAME3 = txtCURRENCYNAME3.Text;
                        if (txtCURRENTCONVRATE.Text == null || txtCURRENTCONVRATE.Text == "") { } else { objtblCOUNTRY.CURRENTCONVRATE = Convert.ToDecimal(txtCURRENTCONVRATE.Text); }
                        objtblCOUNTRY.CURRENCYSHORTNAME1 = txtCURRENCYSHORTNAME1.Text;
                        objtblCOUNTRY.CURRENCYSHORTNAME2 = txtCURRENCYSHORTNAME2.Text;
                        objtblCOUNTRY.CURRENCYSHORTNAME3 = txtCURRENCYSHORTNAME3.Text;
                        objtblCOUNTRY.CountryType = drpCountryType.SelectedValue;
                        objtblCOUNTRY.CountryTSubType = txtCountryTSubType.Text;
                        objtblCOUNTRY.Sovereignty = txtSovereignty.Text;
                        objtblCOUNTRY.ISO4217CurCode = txtISO4217CurCode.Text;
                        objtblCOUNTRY.ISO4217CurName = txtISO4217CurName.Text;
                        objtblCOUNTRY.ITUTTelephoneCode = txtITUTTelephoneCode.Text;
                        if (txtFaxLength.Text == null || txtFaxLength.Text == "") { } else { objtblCOUNTRY.FaxLength = Convert.ToInt32(txtFaxLength.Text); }
                        if (txtTelLength.Text == null || txtTelLength.Text == "") { } else { objtblCOUNTRY.TelLength = Convert.ToInt32(txtTelLength.Text); }
                        objtblCOUNTRY.ISO3166_1_2LetterCode = txtISO3166_1_2LetterCode.Text;
                        objtblCOUNTRY.ISO3166_1_3LetterCode = txtISO3166_1_3LetterCode.Text;
                        objtblCOUNTRY.ISO3166_1Number = txtISO3166_1Number.Text;
                        objtblCOUNTRY.IANACountryCodeTLD = txtIANACountryCodeTLD.Text;
                        objtblCOUNTRY.Active = cbActive.Checked ? "Y" : "N";
                        objtblCOUNTRY.CRUP_ID = 1;
                        objtblCOUNTRY.UploadDate = DateTime.Now;
                        objtblCOUNTRY.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                        objtblCOUNTRY.SynID = 1;


                        DB.tblCOUNTRies.AddObject(objtblCOUNTRY);
                        DB.SaveChanges();

                        Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        Clear();
                        btnAdd.Text = "Add New";
                        //lblMsg.Text = "  Data Save Successfully";
                        //pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else
                    {
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Record Is All Ready Exist...", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                    }
                }
                else if (btnAdd.Text == "Update")
                {

                    if (ViewState["Edit"] != null)
                    {
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        Database.tblCOUNTRY objtblCOUNTRY = DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == ID);
                        //objtblCOUNTRY.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                        objtblCOUNTRY.REGION1 = txtREGION1.Text;
                        objtblCOUNTRY.COUNAME1 = txtCOUNAME1.Text;
                        objtblCOUNTRY.COUNAME2 = txtCOUNAME2.Text;
                        objtblCOUNTRY.COUNAME3 = txtCOUNAME3.Text;
                        objtblCOUNTRY.CAPITAL = txtCAPITAL.Text;
                        objtblCOUNTRY.NATIONALITY1 = txtNATIONALITY1.Text;
                        objtblCOUNTRY.NATIONALITY2 = txtNATIONALITY2.Text;
                        objtblCOUNTRY.NATIONALITY3 = txtNATIONALITY3.Text;
                        objtblCOUNTRY.CURRENCYNAME1 = txtCURRENCYNAME1.Text;
                        objtblCOUNTRY.CURRENCYNAME2 = txtCURRENCYNAME2.Text;
                        objtblCOUNTRY.CURRENCYNAME3 = txtCURRENCYNAME3.Text;
                        if (txtCURRENTCONVRATE.Text == null || txtCURRENTCONVRATE.Text == "") { } else { objtblCOUNTRY.CURRENTCONVRATE = Convert.ToDecimal(txtCURRENTCONVRATE.Text); }
                        objtblCOUNTRY.CURRENCYSHORTNAME1 = txtCURRENCYSHORTNAME1.Text;
                        objtblCOUNTRY.CURRENCYSHORTNAME2 = txtCURRENCYSHORTNAME2.Text;
                        objtblCOUNTRY.CURRENCYSHORTNAME3 = txtCURRENCYSHORTNAME3.Text;
                        objtblCOUNTRY.CountryType = drpCountryType.SelectedValue;
                        objtblCOUNTRY.CountryTSubType = txtCountryTSubType.Text;
                        objtblCOUNTRY.Sovereignty = txtSovereignty.Text;
                        objtblCOUNTRY.ISO4217CurCode = txtISO4217CurCode.Text;
                        objtblCOUNTRY.ISO4217CurName = txtISO4217CurName.Text;
                        objtblCOUNTRY.ITUTTelephoneCode = txtITUTTelephoneCode.Text;
                        if (txtFaxLength.Text == null || txtFaxLength.Text == "") { } else { objtblCOUNTRY.FaxLength = Convert.ToInt32(txtFaxLength.Text); }
                        if (txtTelLength.Text == null || txtTelLength.Text == "") { } else { objtblCOUNTRY.TelLength = Convert.ToInt32(txtTelLength.Text); }
                        objtblCOUNTRY.ISO3166_1_2LetterCode = txtISO3166_1_2LetterCode.Text;
                        objtblCOUNTRY.ISO3166_1_3LetterCode = txtISO3166_1_3LetterCode.Text;
                        objtblCOUNTRY.ISO3166_1Number = txtISO3166_1Number.Text;
                        objtblCOUNTRY.IANACountryCodeTLD = txtIANACountryCodeTLD.Text;
                        objtblCOUNTRY.Active = cbActive.Checked ? "Y" : "N";

                        objtblCOUNTRY.UploadDate = DateTime.Now;
                        objtblCOUNTRY.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                        objtblCOUNTRY.SynID = 2;

                        //objtblCOUNTRY.Active = cbActive.Checked ? "Y" : "N";
                        //objtblCOUNTRY.CRUP_ID = txtCRUP_ID.Text;

                        ViewState["Edit"] = null;
                        btnAdd.Text = "Add New";
                        DB.SaveChanges();

                        Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        Clear();
                        //lblMsg.Text = "  Data Edit Successfully";
                        //pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                }
                BindData();

                scope.Complete(); //  To commit.

                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();

            drpCountryType.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID).GroupBy(p => p.CountryType).Select(p => p.FirstOrDefault());
            drpCountryType.DataTextField = "CountryType";
            drpCountryType.DataValueField = "CountryType";
            drpCountryType.DataBind();
            drpCountryType.Items.Insert(0, new ListItem("-- Select Country Type --", "0"));
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
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;

            //drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            if (Listview1.SelectedDataKey[2] != null)
                txtREGION1.Text = Listview1.SelectedDataKey[2].ToString();
            if (Listview1.SelectedDataKey[3] != null)
                txtCOUNAME1.Text = Listview1.SelectedDataKey[3].ToString();
            if (Listview1.SelectedDataKey[4] != null)
                txtCOUNAME2.Text = Listview1.SelectedDataKey[4].ToString();
            if (Listview1.SelectedDataKey[5] != null)
                txtCOUNAME3.Text = Listview1.SelectedDataKey[5].ToString();
            if (Listview1.SelectedDataKey[6] != null)
                txtCAPITAL.Text = Listview1.SelectedDataKey[6].ToString();
            if (Listview1.SelectedDataKey[7] != null)
                txtNATIONALITY1.Text = Listview1.SelectedDataKey[7].ToString();
            if (Listview1.SelectedDataKey[8] != null)
                txtNATIONALITY2.Text = Listview1.SelectedDataKey[8].ToString();
            if (Listview1.SelectedDataKey[9] != null)
                txtNATIONALITY3.Text = Listview1.SelectedDataKey[9].ToString();
            if (Listview1.SelectedDataKey[10] != null)
                txtCURRENCYNAME1.Text = Listview1.SelectedDataKey[10].ToString();
            if (Listview1.SelectedDataKey[11] != null)
                txtCURRENCYNAME2.Text = Listview1.SelectedDataKey[11].ToString();
            if (Listview1.SelectedDataKey[12] != null)
                txtCURRENCYNAME3.Text = Listview1.SelectedDataKey[12].ToString();
            if (Listview1.SelectedDataKey[13] != null)
                txtCURRENTCONVRATE.Text = Listview1.SelectedDataKey[13].ToString();
            if (Listview1.SelectedDataKey[14] != null)
                txtCURRENCYSHORTNAME1.Text = Listview1.SelectedDataKey[14].ToString();
            if (Listview1.SelectedDataKey[15] != null)
                txtCURRENCYSHORTNAME2.Text = Listview1.SelectedDataKey[15].ToString();
            if (Listview1.SelectedDataKey[16] != null)
                txtCURRENCYSHORTNAME3.Text = Listview1.SelectedDataKey[16].ToString();
            if (Listview1.SelectedDataKey[17] != null)
                drpCountryType.SelectedValue = Listview1.SelectedDataKey[17].ToString();
            if (Listview1.SelectedDataKey[18] != null)
                txtCountryTSubType.Text = Listview1.SelectedDataKey[18].ToString();
            if (Listview1.SelectedDataKey[19] != null)
                txtSovereignty.Text = Listview1.SelectedDataKey[19].ToString();
            if (Listview1.SelectedDataKey[20] != null)
                txtISO4217CurCode.Text = Listview1.SelectedDataKey[20].ToString();
            if (Listview1.SelectedDataKey[21] != null)
                txtISO4217CurName.Text = Listview1.SelectedDataKey[21].ToString();
            if (Listview1.SelectedDataKey[22] != null)
                txtITUTTelephoneCode.Text = Listview1.SelectedDataKey[22].ToString();
            if (Listview1.SelectedDataKey[23] != null)
                txtFaxLength.Text = Listview1.SelectedDataKey[23].ToString();
            if (Listview1.SelectedDataKey[24] != null)
                txtTelLength.Text = Listview1.SelectedDataKey[24].ToString();
            if (Listview1.SelectedDataKey[25] != null)
                txtISO3166_1_2LetterCode.Text = Listview1.SelectedDataKey[25].ToString();
            if (Listview1.SelectedDataKey[26] != null)
                txtISO3166_1_3LetterCode.Text = Listview1.SelectedDataKey[26].ToString();
            if (Listview1.SelectedDataKey[27] != null)
                txtISO3166_1Number.Text = Listview1.SelectedDataKey[27].ToString();
            if (Listview1.SelectedDataKey[28] != null)
                txtIANACountryCodeTLD.Text = Listview1.SelectedDataKey[28].ToString();
            if (Listview1.SelectedDataKey[29] == "Y")
            {
                cbActive.Checked = true;
            }
            else
            {
                cbActive.Checked = false;
            }
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                if (Listview1.SelectedDataKey[2] != null)
                    txtREGION1.Text = Listview1.SelectedDataKey[2].ToString();
                if (Listview1.SelectedDataKey[3] != null)
                    txtCOUNAME1.Text = Listview1.SelectedDataKey[3].ToString();
                if (Listview1.SelectedDataKey[4] != null)
                    txtCOUNAME2.Text = Listview1.SelectedDataKey[4].ToString();
                if (Listview1.SelectedDataKey[5] != null)
                    txtCOUNAME3.Text = Listview1.SelectedDataKey[5].ToString();
                if (Listview1.SelectedDataKey[6] != null)
                    txtCAPITAL.Text = Listview1.SelectedDataKey[6].ToString();
                if (Listview1.SelectedDataKey[7] != null)
                    txtNATIONALITY1.Text = Listview1.SelectedDataKey[7].ToString();
                if (Listview1.SelectedDataKey[8] != null)
                    txtNATIONALITY2.Text = Listview1.SelectedDataKey[8].ToString();
                if (Listview1.SelectedDataKey[9] != null)
                    txtNATIONALITY3.Text = Listview1.SelectedDataKey[9].ToString();
                if (Listview1.SelectedDataKey[10] != null)
                    txtCURRENCYNAME1.Text = Listview1.SelectedDataKey[10].ToString();
                if (Listview1.SelectedDataKey[11] != null)
                    txtCURRENCYNAME2.Text = Listview1.SelectedDataKey[11].ToString();
                if (Listview1.SelectedDataKey[12] != null)
                    txtCURRENCYNAME3.Text = Listview1.SelectedDataKey[12].ToString();
                if (Listview1.SelectedDataKey[13] != null)
                    txtCURRENTCONVRATE.Text = Listview1.SelectedDataKey[13].ToString();
                if (Listview1.SelectedDataKey[14] != null)
                    txtCURRENCYSHORTNAME1.Text = Listview1.SelectedDataKey[14].ToString();
                if (Listview1.SelectedDataKey[15] != null)
                    txtCURRENCYSHORTNAME2.Text = Listview1.SelectedDataKey[15].ToString();
                if (Listview1.SelectedDataKey[16] != null)
                    txtCURRENCYSHORTNAME3.Text = Listview1.SelectedDataKey[16].ToString();
                if (Listview1.SelectedDataKey[17] != null)
                    drpCountryType.SelectedValue = Listview1.SelectedDataKey[17].ToString();
                if (Listview1.SelectedDataKey[18] != null)
                    txtCountryTSubType.Text = Listview1.SelectedDataKey[18].ToString();
                if (Listview1.SelectedDataKey[19] != null)
                    txtSovereignty.Text = Listview1.SelectedDataKey[19].ToString();
                if (Listview1.SelectedDataKey[20] != null)
                    txtISO4217CurCode.Text = Listview1.SelectedDataKey[20].ToString();
                if (Listview1.SelectedDataKey[21] != null)
                    txtISO4217CurName.Text = Listview1.SelectedDataKey[21].ToString();
                if (Listview1.SelectedDataKey[22] != null)
                    txtITUTTelephoneCode.Text = Listview1.SelectedDataKey[22].ToString();
                if (Listview1.SelectedDataKey[23] != null)
                    txtFaxLength.Text = Listview1.SelectedDataKey[23].ToString();
                if (Listview1.SelectedDataKey[24] != null)
                    txtTelLength.Text = Listview1.SelectedDataKey[24].ToString();
                if (Listview1.SelectedDataKey[25] != null)
                    txtISO3166_1_2LetterCode.Text = Listview1.SelectedDataKey[25].ToString();
                if (Listview1.SelectedDataKey[26] != null)
                    txtISO3166_1_3LetterCode.Text = Listview1.SelectedDataKey[26].ToString();
                if (Listview1.SelectedDataKey[27] != null)
                    txtISO3166_1Number.Text = Listview1.SelectedDataKey[27].ToString();
                if (Listview1.SelectedDataKey[28] != null)
                    txtIANACountryCodeTLD.Text = Listview1.SelectedDataKey[28].ToString();
                if (Listview1.SelectedDataKey[29] == "Y")
                {
                    cbActive.Checked = true;
                }
                else
                {
                    cbActive.Checked = false;
                }
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

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
                //drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                if (Listview1.SelectedDataKey[2] != null)
                    txtREGION1.Text = Listview1.SelectedDataKey[2].ToString();
                if (Listview1.SelectedDataKey[3] != null)
                    txtCOUNAME1.Text = Listview1.SelectedDataKey[3].ToString();
                if (Listview1.SelectedDataKey[4] != null)
                    txtCOUNAME2.Text = Listview1.SelectedDataKey[4].ToString();
                if (Listview1.SelectedDataKey[5] != null)
                    txtCOUNAME3.Text = Listview1.SelectedDataKey[5].ToString();
                if (Listview1.SelectedDataKey[6] != null)
                    txtCAPITAL.Text = Listview1.SelectedDataKey[6].ToString();
                if (Listview1.SelectedDataKey[7] != null)
                    txtNATIONALITY1.Text = Listview1.SelectedDataKey[7].ToString();
                if (Listview1.SelectedDataKey[8] != null)
                    txtNATIONALITY2.Text = Listview1.SelectedDataKey[8].ToString();
                if (Listview1.SelectedDataKey[9] != null)
                    txtNATIONALITY3.Text = Listview1.SelectedDataKey[9].ToString();
                if (Listview1.SelectedDataKey[10] != null)
                    txtCURRENCYNAME1.Text = Listview1.SelectedDataKey[10].ToString();
                if (Listview1.SelectedDataKey[11] != null)
                    txtCURRENCYNAME2.Text = Listview1.SelectedDataKey[11].ToString();
                if (Listview1.SelectedDataKey[12] != null)
                    txtCURRENCYNAME3.Text = Listview1.SelectedDataKey[12].ToString();
                if (Listview1.SelectedDataKey[13] != null)
                    txtCURRENTCONVRATE.Text = Listview1.SelectedDataKey[13].ToString();
                if (Listview1.SelectedDataKey[14] != null)
                    txtCURRENCYSHORTNAME1.Text = Listview1.SelectedDataKey[14].ToString();
                if (Listview1.SelectedDataKey[15] != null)
                    txtCURRENCYSHORTNAME2.Text = Listview1.SelectedDataKey[15].ToString();
                if (Listview1.SelectedDataKey[16] != null)
                    txtCURRENCYSHORTNAME3.Text = Listview1.SelectedDataKey[16].ToString();
                if (Listview1.SelectedDataKey[17] != null)
                    drpCountryType.SelectedValue = Listview1.SelectedDataKey[17].ToString();
                if (Listview1.SelectedDataKey[18] != null)
                    txtCountryTSubType.Text = Listview1.SelectedDataKey[18].ToString();
                if (Listview1.SelectedDataKey[19] != null)
                    txtSovereignty.Text = Listview1.SelectedDataKey[19].ToString();
                if (Listview1.SelectedDataKey[20] != null)
                    txtISO4217CurCode.Text = Listview1.SelectedDataKey[20].ToString();
                if (Listview1.SelectedDataKey[21] != null)
                    txtISO4217CurName.Text = Listview1.SelectedDataKey[21].ToString();
                if (Listview1.SelectedDataKey[22] != null)
                    txtITUTTelephoneCode.Text = Listview1.SelectedDataKey[22].ToString();
                if (Listview1.SelectedDataKey[23] != null)
                    txtFaxLength.Text = Listview1.SelectedDataKey[23].ToString();
                if (Listview1.SelectedDataKey[24] != null)
                    txtTelLength.Text = Listview1.SelectedDataKey[24].ToString();
                if (Listview1.SelectedDataKey[25] != null)
                    txtISO3166_1_2LetterCode.Text = Listview1.SelectedDataKey[25].ToString();
                if (Listview1.SelectedDataKey[26] != null)
                    txtISO3166_1_3LetterCode.Text = Listview1.SelectedDataKey[26].ToString();
                if (Listview1.SelectedDataKey[27] != null)
                    txtISO3166_1Number.Text = Listview1.SelectedDataKey[27].ToString();
                if (Listview1.SelectedDataKey[28] != null)
                    txtIANACountryCodeTLD.Text = Listview1.SelectedDataKey[28].ToString();
                if (Listview1.SelectedDataKey[29] == "Y")
                {
                    cbActive.Checked = true;
                }
                else
                {
                    cbActive.Checked = false;
                }
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            if (Listview1.SelectedDataKey[2] != null)
                txtREGION1.Text = Listview1.SelectedDataKey[2].ToString();
            if (Listview1.SelectedDataKey[3] != null)
                txtCOUNAME1.Text = Listview1.SelectedDataKey[3].ToString();
            if (Listview1.SelectedDataKey[4] != null)
                txtCOUNAME2.Text = Listview1.SelectedDataKey[4].ToString();
            if (Listview1.SelectedDataKey[5] != null)
                txtCOUNAME3.Text = Listview1.SelectedDataKey[5].ToString();
            if (Listview1.SelectedDataKey[6] != null)
                txtCAPITAL.Text = Listview1.SelectedDataKey[6].ToString();
            if (Listview1.SelectedDataKey[7] != null)
                txtNATIONALITY1.Text = Listview1.SelectedDataKey[7].ToString();
            if (Listview1.SelectedDataKey[8] != null)
                txtNATIONALITY2.Text = Listview1.SelectedDataKey[8].ToString();
            if (Listview1.SelectedDataKey[9] != null)
                txtNATIONALITY3.Text = Listview1.SelectedDataKey[9].ToString();
            if (Listview1.SelectedDataKey[10] != null)
                txtCURRENCYNAME1.Text = Listview1.SelectedDataKey[10].ToString();
            if (Listview1.SelectedDataKey[11] != null)
                txtCURRENCYNAME2.Text = Listview1.SelectedDataKey[11].ToString();
            if (Listview1.SelectedDataKey[12] != null)
                txtCURRENCYNAME3.Text = Listview1.SelectedDataKey[12].ToString();
            if (Listview1.SelectedDataKey[13] != null)
                txtCURRENTCONVRATE.Text = Listview1.SelectedDataKey[13].ToString();
            if (Listview1.SelectedDataKey[14] != null)
                txtCURRENCYSHORTNAME1.Text = Listview1.SelectedDataKey[14].ToString();
            if (Listview1.SelectedDataKey[15] != null)
                txtCURRENCYSHORTNAME2.Text = Listview1.SelectedDataKey[15].ToString();
            if (Listview1.SelectedDataKey[16] != null)
                txtCURRENCYSHORTNAME3.Text = Listview1.SelectedDataKey[16].ToString();
            if (Listview1.SelectedDataKey[17] != null)
                drpCountryType.SelectedValue = Listview1.SelectedDataKey[17].ToString();
            if (Listview1.SelectedDataKey[18] != null)
                txtCountryTSubType.Text = Listview1.SelectedDataKey[18].ToString();
            if (Listview1.SelectedDataKey[19] != null)
                txtSovereignty.Text = Listview1.SelectedDataKey[19].ToString();
            if (Listview1.SelectedDataKey[20] != null)
                txtISO4217CurCode.Text = Listview1.SelectedDataKey[20].ToString();
            if (Listview1.SelectedDataKey[21] != null)
                txtISO4217CurName.Text = Listview1.SelectedDataKey[21].ToString();
            if (Listview1.SelectedDataKey[22] != null)
                txtITUTTelephoneCode.Text = Listview1.SelectedDataKey[22].ToString();
            if (Listview1.SelectedDataKey[23] != null)
                txtFaxLength.Text = Listview1.SelectedDataKey[23].ToString();
            if (Listview1.SelectedDataKey[24] != null)
                txtTelLength.Text = Listview1.SelectedDataKey[24].ToString();
            if (Listview1.SelectedDataKey[25] != null)
                txtISO3166_1_2LetterCode.Text = Listview1.SelectedDataKey[25].ToString();
            if (Listview1.SelectedDataKey[26] != null)
                txtISO3166_1_3LetterCode.Text = Listview1.SelectedDataKey[26].ToString();
            if (Listview1.SelectedDataKey[27] != null)
                txtISO3166_1Number.Text = Listview1.SelectedDataKey[27].ToString();
            if (Listview1.SelectedDataKey[28] != null)
                txtIANACountryCodeTLD.Text = Listview1.SelectedDataKey[28].ToString();
            if (Listview1.SelectedDataKey[29] == "Y")
            {
                cbActive.Checked = true;
            }
            else
            {
                cbActive.Checked = false;
            }
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblTenantID2h.Visible = lblCOUNTRYID2h.Visible = lblREGION12h.Visible = lblCOUNAME12h.Visible = lblCOUNAME22h.Visible = lblCOUNAME32h.Visible = lblCAPITAL2h.Visible = lblNATIONALITY12h.Visible = lblNATIONALITY22h.Visible = lblNATIONALITY32h.Visible = lblCURRENCYNAME12h.Visible = lblCURRENCYNAME22h.Visible = lblCURRENCYNAME32h.Visible = lblCURRENTCONVRATE2h.Visible = lblCURRENCYSHORTNAME12h.Visible = lblCURRENCYSHORTNAME22h.Visible = lblCURRENCYSHORTNAME32h.Visible = lblCountryType2h.Visible = lblCountryTSubType2h.Visible = lblSovereignty2h.Visible = lblISO4217CurCode2h.Visible = lblISO4217CurName2h.Visible = lblITUTTelephoneCode2h.Visible = lblFaxLength2h.Visible = lblTelLength2h.Visible = lblISO3166_1_2LetterCode2h.Visible = lblISO3166_1_3LetterCode2h.Visible = lblISO3166_1Number2h.Visible = lblIANACountryCodeTLD2h.Visible = lblActive2h.Visible = false;
                    //2true
                    txtTenantID2h.Visible = txtCOUNTRYID2h.Visible = txtREGION12h.Visible = txtCOUNAME12h.Visible = txtCOUNAME22h.Visible = txtCOUNAME32h.Visible = txtCAPITAL2h.Visible = txtNATIONALITY12h.Visible = txtNATIONALITY22h.Visible = txtNATIONALITY32h.Visible = txtCURRENCYNAME12h.Visible = txtCURRENCYNAME22h.Visible = txtCURRENCYNAME32h.Visible = txtCURRENTCONVRATE2h.Visible = txtCURRENCYSHORTNAME12h.Visible = txtCURRENCYSHORTNAME22h.Visible = txtCURRENCYSHORTNAME32h.Visible = txtCountryType2h.Visible = txtCountryTSubType2h.Visible = txtSovereignty2h.Visible = txtISO4217CurCode2h.Visible = txtISO4217CurName2h.Visible = txtITUTTelephoneCode2h.Visible = txtFaxLength2h.Visible = txtTelLength2h.Visible = txtISO3166_1_2LetterCode2h.Visible = txtISO3166_1_3LetterCode2h.Visible = txtISO3166_1Number2h.Visible = txtIANACountryCodeTLD2h.Visible = txtActive2h.Visible = true;

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
                    lblTenantID2h.Visible = lblCOUNTRYID2h.Visible = lblREGION12h.Visible = lblCOUNAME12h.Visible = lblCOUNAME22h.Visible = lblCOUNAME32h.Visible = lblCAPITAL2h.Visible = lblNATIONALITY12h.Visible = lblNATIONALITY22h.Visible = lblNATIONALITY32h.Visible = lblCURRENCYNAME12h.Visible = lblCURRENCYNAME22h.Visible = lblCURRENCYNAME32h.Visible = lblCURRENTCONVRATE2h.Visible = lblCURRENCYSHORTNAME12h.Visible = lblCURRENCYSHORTNAME22h.Visible = lblCURRENCYSHORTNAME32h.Visible = lblCountryType2h.Visible = lblCountryTSubType2h.Visible = lblSovereignty2h.Visible = lblISO4217CurCode2h.Visible = lblISO4217CurName2h.Visible = lblITUTTelephoneCode2h.Visible = lblFaxLength2h.Visible = lblTelLength2h.Visible = lblISO3166_1_2LetterCode2h.Visible = lblISO3166_1_3LetterCode2h.Visible = lblISO3166_1Number2h.Visible = lblIANACountryCodeTLD2h.Visible = lblActive2h.Visible = true;
                    //2false
                    txtTenantID2h.Visible = txtCOUNTRYID2h.Visible = txtREGION12h.Visible = txtCOUNAME12h.Visible = txtCOUNAME22h.Visible = txtCOUNAME32h.Visible = txtCAPITAL2h.Visible = txtNATIONALITY12h.Visible = txtNATIONALITY22h.Visible = txtNATIONALITY32h.Visible = txtCURRENCYNAME12h.Visible = txtCURRENCYNAME22h.Visible = txtCURRENCYNAME32h.Visible = txtCURRENTCONVRATE2h.Visible = txtCURRENCYSHORTNAME12h.Visible = txtCURRENCYSHORTNAME22h.Visible = txtCURRENCYSHORTNAME32h.Visible = txtCountryType2h.Visible = txtCountryTSubType2h.Visible = txtSovereignty2h.Visible = txtISO4217CurCode2h.Visible = txtISO4217CurName2h.Visible = txtITUTTelephoneCode2h.Visible = txtFaxLength2h.Visible = txtTelLength2h.Visible = txtISO3166_1_2LetterCode2h.Visible = txtISO3166_1_3LetterCode2h.Visible = txtISO3166_1Number2h.Visible = txtIANACountryCodeTLD2h.Visible = txtActive2h.Visible = false;

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
                    lblTenantID1s.Visible = lblCOUNTRYID1s.Visible = lblREGION11s.Visible = lblCOUNAME11s.Visible = lblCOUNAME21s.Visible = lblCOUNAME31s.Visible = lblCAPITAL1s.Visible = lblNATIONALITY11s.Visible = lblNATIONALITY21s.Visible = lblNATIONALITY31s.Visible = lblCURRENCYNAME11s.Visible = lblCURRENCYNAME21s.Visible = lblCURRENCYNAME31s.Visible = lblCURRENTCONVRATE1s.Visible = lblCURRENCYSHORTNAME11s.Visible = lblCURRENCYSHORTNAME21s.Visible = lblCURRENCYSHORTNAME31s.Visible = lblCountryType1s.Visible = lblCountryTSubType1s.Visible = lblSovereignty1s.Visible = lblISO4217CurCode1s.Visible = lblISO4217CurName1s.Visible = lblITUTTelephoneCode1s.Visible = lblFaxLength1s.Visible = lblTelLength1s.Visible = lblISO3166_1_2LetterCode1s.Visible = lblISO3166_1_3LetterCode1s.Visible = lblISO3166_1Number1s.Visible = lblIANACountryCodeTLD1s.Visible = lblActive1s.Visible = false;
                    //1true
                    txtTenantID1s.Visible = txtCOUNTRYID1s.Visible = txtREGION11s.Visible = txtCOUNAME11s.Visible = txtCOUNAME21s.Visible = txtCOUNAME31s.Visible = txtCAPITAL1s.Visible = txtNATIONALITY11s.Visible = txtNATIONALITY21s.Visible = txtNATIONALITY31s.Visible = txtCURRENCYNAME11s.Visible = txtCURRENCYNAME21s.Visible = txtCURRENCYNAME31s.Visible = txtCURRENTCONVRATE1s.Visible = txtCURRENCYSHORTNAME11s.Visible = txtCURRENCYSHORTNAME21s.Visible = txtCURRENCYSHORTNAME31s.Visible = txtCountryType1s.Visible = txtCountryTSubType1s.Visible = txtSovereignty1s.Visible = txtISO4217CurCode1s.Visible = txtISO4217CurName1s.Visible = txtITUTTelephoneCode1s.Visible = txtFaxLength1s.Visible = txtTelLength1s.Visible = txtISO3166_1_2LetterCode1s.Visible = txtISO3166_1_3LetterCode1s.Visible = txtISO3166_1Number1s.Visible = txtIANACountryCodeTLD1s.Visible = txtActive1s.Visible = true;
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
                    lblTenantID1s.Visible = lblCOUNTRYID1s.Visible = lblREGION11s.Visible = lblCOUNAME11s.Visible = lblCOUNAME21s.Visible = lblCOUNAME31s.Visible = lblCAPITAL1s.Visible = lblNATIONALITY11s.Visible = lblNATIONALITY21s.Visible = lblNATIONALITY31s.Visible = lblCURRENCYNAME11s.Visible = lblCURRENCYNAME21s.Visible = lblCURRENCYNAME31s.Visible = lblCURRENTCONVRATE1s.Visible = lblCURRENCYSHORTNAME11s.Visible = lblCURRENCYSHORTNAME21s.Visible = lblCURRENCYSHORTNAME31s.Visible = lblCountryType1s.Visible = lblCountryTSubType1s.Visible = lblSovereignty1s.Visible = lblISO4217CurCode1s.Visible = lblISO4217CurName1s.Visible = lblITUTTelephoneCode1s.Visible = lblFaxLength1s.Visible = lblTelLength1s.Visible = lblISO3166_1_2LetterCode1s.Visible = lblISO3166_1_3LetterCode1s.Visible = lblISO3166_1Number1s.Visible = lblIANACountryCodeTLD1s.Visible = lblActive1s.Visible = true;
                    //1false
                    txtTenantID1s.Visible = txtCOUNTRYID1s.Visible = txtREGION11s.Visible = txtCOUNAME11s.Visible = txtCOUNAME21s.Visible = txtCOUNAME31s.Visible = txtCAPITAL1s.Visible = txtNATIONALITY11s.Visible = txtNATIONALITY21s.Visible = txtNATIONALITY31s.Visible = txtCURRENCYNAME11s.Visible = txtCURRENCYNAME21s.Visible = txtCURRENCYNAME31s.Visible = txtCURRENTCONVRATE1s.Visible = txtCURRENCYSHORTNAME11s.Visible = txtCURRENCYSHORTNAME21s.Visible = txtCURRENCYSHORTNAME31s.Visible = txtCountryType1s.Visible = txtCountryTSubType1s.Visible = txtSovereignty1s.Visible = txtISO4217CurCode1s.Visible = txtISO4217CurName1s.Visible = txtITUTTelephoneCode1s.Visible = txtFaxLength1s.Visible = txtTelLength1s.Visible = txtISO3166_1_2LetterCode1s.Visible = txtISO3166_1_3LetterCode1s.Visible = txtISO3166_1Number1s.Visible = txtIANACountryCodeTLD1s.Visible = txtActive1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblCOUNTRY").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblTenantID1s.ID == item.LabelID)
                    txtTenantID1s.Text = lblTenantID1s.Text = item.LabelName;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = item.LabelName;
                else if (lblREGION11s.ID == item.LabelID)
                    txtREGION11s.Text = lblREGION11s.Text = item.LabelName;
                else if (lblCOUNAME11s.ID == item.LabelID)
                    txtCOUNAME11s.Text = lblCOUNAME11s.Text = lblhCOUNAME1.Text = item.LabelName;
                else if (lblCOUNAME21s.ID == item.LabelID)
                    txtCOUNAME21s.Text = lblCOUNAME21s.Text = lblhCOUNAME2.Text = item.LabelName;
                else if (lblCOUNAME31s.ID == item.LabelID)
                    txtCOUNAME31s.Text = lblCOUNAME31s.Text = lblhCOUNAME3.Text = item.LabelName;
                else if (lblCAPITAL1s.ID == item.LabelID)
                    txtCAPITAL1s.Text = lblCAPITAL1s.Text = item.LabelName;
                else if (lblNATIONALITY11s.ID == item.LabelID)
                    txtNATIONALITY11s.Text = lblNATIONALITY11s.Text = lblhNATIONALITY1.Text = item.LabelName;
                else if (lblNATIONALITY21s.ID == item.LabelID)
                    txtNATIONALITY21s.Text = lblNATIONALITY21s.Text = lblhNATIONALITY2.Text = item.LabelName;
                else if (lblNATIONALITY31s.ID == item.LabelID)
                    txtNATIONALITY31s.Text = lblNATIONALITY31s.Text = lblhNATIONALITY3.Text = item.LabelName;
                else if (lblCURRENCYNAME11s.ID == item.LabelID)
                    txtCURRENCYNAME11s.Text = lblCURRENCYNAME11s.Text = item.LabelName;
                else if (lblCURRENCYNAME21s.ID == item.LabelID)
                    txtCURRENCYNAME21s.Text = lblCURRENCYNAME21s.Text = item.LabelName;
                else if (lblCURRENCYNAME31s.ID == item.LabelID)
                    txtCURRENCYNAME31s.Text = lblCURRENCYNAME31s.Text = item.LabelName;
                else if (lblCURRENTCONVRATE1s.ID == item.LabelID)
                    txtCURRENTCONVRATE1s.Text = lblCURRENTCONVRATE1s.Text = item.LabelName;
                else if (lblCURRENCYSHORTNAME11s.ID == item.LabelID)
                    txtCURRENCYSHORTNAME11s.Text = lblCURRENCYSHORTNAME11s.Text = item.LabelName;
                else if (lblCURRENCYSHORTNAME21s.ID == item.LabelID)
                    txtCURRENCYSHORTNAME21s.Text = lblCURRENCYSHORTNAME21s.Text = item.LabelName;
                else if (lblCURRENCYSHORTNAME31s.ID == item.LabelID)
                    txtCURRENCYSHORTNAME31s.Text = lblCURRENCYSHORTNAME31s.Text = item.LabelName;
                else if (lblCountryType1s.ID == item.LabelID)
                    txtCountryType1s.Text = lblCountryType1s.Text = item.LabelName;
                else if (lblCountryTSubType1s.ID == item.LabelID)
                    txtCountryTSubType1s.Text = lblCountryTSubType1s.Text = item.LabelName;
                else if (lblSovereignty1s.ID == item.LabelID)
                    txtSovereignty1s.Text = lblSovereignty1s.Text = item.LabelName;
                else if (lblISO4217CurCode1s.ID == item.LabelID)
                    txtISO4217CurCode1s.Text = lblISO4217CurCode1s.Text = item.LabelName;
                else if (lblISO4217CurName1s.ID == item.LabelID)
                    txtISO4217CurName1s.Text = lblISO4217CurName1s.Text = item.LabelName;
                else if (lblITUTTelephoneCode1s.ID == item.LabelID)
                    txtITUTTelephoneCode1s.Text = lblITUTTelephoneCode1s.Text = item.LabelName;
                else if (lblFaxLength1s.ID == item.LabelID)
                    txtFaxLength1s.Text = lblFaxLength1s.Text = item.LabelName;
                else if (lblTelLength1s.ID == item.LabelID)
                    txtTelLength1s.Text = lblTelLength1s.Text = item.LabelName;
                else if (lblISO3166_1_2LetterCode1s.ID == item.LabelID)
                    txtISO3166_1_2LetterCode1s.Text = lblISO3166_1_2LetterCode1s.Text = item.LabelName;
                else if (lblISO3166_1_3LetterCode1s.ID == item.LabelID)
                    txtISO3166_1_3LetterCode1s.Text = lblISO3166_1_3LetterCode1s.Text = item.LabelName;
                else if (lblISO3166_1Number1s.ID == item.LabelID)
                    txtISO3166_1Number1s.Text = lblISO3166_1Number1s.Text = item.LabelName;
                else if (lblIANACountryCodeTLD1s.ID == item.LabelID)
                    txtIANACountryCodeTLD1s.Text = lblIANACountryCodeTLD1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;

                else if (lblTenantID2h.ID == item.LabelID)
                    txtTenantID2h.Text = lblTenantID2h.Text = item.LabelName;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = item.LabelName;
                else if (lblREGION12h.ID == item.LabelID)
                    txtREGION12h.Text = lblREGION12h.Text = item.LabelName;
                else if (lblCOUNAME12h.ID == item.LabelID)
                    txtCOUNAME12h.Text = lblCOUNAME12h.Text = lblhCOUNAME1.Text = item.LabelName;
                else if (lblCOUNAME22h.ID == item.LabelID)
                    txtCOUNAME22h.Text = lblCOUNAME22h.Text = lblhCOUNAME2.Text = item.LabelName;
                else if (lblCOUNAME32h.ID == item.LabelID)
                    txtCOUNAME32h.Text = lblCOUNAME32h.Text = lblhCOUNAME3.Text = item.LabelName;
                else if (lblCAPITAL2h.ID == item.LabelID)
                    txtCAPITAL2h.Text = lblCAPITAL2h.Text = item.LabelName;
                else if (lblNATIONALITY12h.ID == item.LabelID)
                    txtNATIONALITY12h.Text = lblNATIONALITY12h.Text = lblhNATIONALITY1.Text = item.LabelName;
                else if (lblNATIONALITY22h.ID == item.LabelID)
                    txtNATIONALITY22h.Text = lblNATIONALITY22h.Text = lblhNATIONALITY2.Text = item.LabelName;
                else if (lblNATIONALITY32h.ID == item.LabelID)
                    txtNATIONALITY32h.Text = lblNATIONALITY32h.Text = lblhNATIONALITY3.Text = item.LabelName;
                else if (lblCURRENCYNAME12h.ID == item.LabelID)
                    txtCURRENCYNAME12h.Text = lblCURRENCYNAME12h.Text = item.LabelName;
                else if (lblCURRENCYNAME22h.ID == item.LabelID)
                    txtCURRENCYNAME22h.Text = lblCURRENCYNAME22h.Text = item.LabelName;
                else if (lblCURRENCYNAME32h.ID == item.LabelID)
                    txtCURRENCYNAME32h.Text = lblCURRENCYNAME32h.Text = item.LabelName;
                else if (lblCURRENTCONVRATE2h.ID == item.LabelID)
                    txtCURRENTCONVRATE2h.Text = lblCURRENTCONVRATE2h.Text = item.LabelName;
                else if (lblCURRENCYSHORTNAME12h.ID == item.LabelID)
                    txtCURRENCYSHORTNAME12h.Text = lblCURRENCYSHORTNAME12h.Text = item.LabelName;
                else if (lblCURRENCYSHORTNAME22h.ID == item.LabelID)
                    txtCURRENCYSHORTNAME22h.Text = lblCURRENCYSHORTNAME22h.Text = item.LabelName;
                else if (lblCURRENCYSHORTNAME32h.ID == item.LabelID)
                    txtCURRENCYSHORTNAME32h.Text = lblCURRENCYSHORTNAME32h.Text = item.LabelName;
                else if (lblCountryType2h.ID == item.LabelID)
                    txtCountryType2h.Text = lblCountryType2h.Text = item.LabelName;
                else if (lblCountryTSubType2h.ID == item.LabelID)
                    txtCountryTSubType2h.Text = lblCountryTSubType2h.Text = item.LabelName;
                else if (lblSovereignty2h.ID == item.LabelID)
                    txtSovereignty2h.Text = lblSovereignty2h.Text = item.LabelName;
                else if (lblISO4217CurCode2h.ID == item.LabelID)
                    txtISO4217CurCode2h.Text = lblISO4217CurCode2h.Text = item.LabelName;
                else if (lblISO4217CurName2h.ID == item.LabelID)
                    txtISO4217CurName2h.Text = lblISO4217CurName2h.Text = item.LabelName;
                else if (lblITUTTelephoneCode2h.ID == item.LabelID)
                    txtITUTTelephoneCode2h.Text = lblITUTTelephoneCode2h.Text = item.LabelName;
                else if (lblFaxLength2h.ID == item.LabelID)
                    txtFaxLength2h.Text = lblFaxLength2h.Text = item.LabelName;
                else if (lblTelLength2h.ID == item.LabelID)
                    txtTelLength2h.Text = lblTelLength2h.Text = item.LabelName;
                else if (lblISO3166_1_2LetterCode2h.ID == item.LabelID)
                    txtISO3166_1_2LetterCode2h.Text = lblISO3166_1_2LetterCode2h.Text = item.LabelName;
                else if (lblISO3166_1_3LetterCode2h.ID == item.LabelID)
                    txtISO3166_1_3LetterCode2h.Text = lblISO3166_1_3LetterCode2h.Text = item.LabelName;
                else if (lblISO3166_1Number2h.ID == item.LabelID)
                    txtISO3166_1Number2h.Text = lblISO3166_1Number2h.Text = item.LabelName;
                else if (lblIANACountryCodeTLD2h.ID == item.LabelID)
                    txtIANACountryCodeTLD2h.Text = lblIANACountryCodeTLD2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblCOUNTRY").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblCOUNTRY.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblCOUNTRY").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblTenantID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID1s.Text;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                else if (lblREGION11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREGION11s.Text;
                else if (lblCOUNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNAME11s.Text;
                else if (lblCOUNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNAME21s.Text;
                else if (lblCOUNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNAME31s.Text;
                else if (lblCAPITAL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCAPITAL1s.Text;
                else if (lblNATIONALITY11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNATIONALITY11s.Text;
                else if (lblNATIONALITY21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNATIONALITY21s.Text;
                else if (lblNATIONALITY31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNATIONALITY31s.Text;
                else if (lblCURRENCYNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYNAME11s.Text;
                else if (lblCURRENCYNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYNAME21s.Text;
                else if (lblCURRENCYNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYNAME31s.Text;
                else if (lblCURRENTCONVRATE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENTCONVRATE1s.Text;
                else if (lblCURRENCYSHORTNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYSHORTNAME11s.Text;
                else if (lblCURRENCYSHORTNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYSHORTNAME21s.Text;
                else if (lblCURRENCYSHORTNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYSHORTNAME31s.Text;
                else if (lblCountryType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCountryType1s.Text;
                else if (lblCountryTSubType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCountryTSubType1s.Text;
                else if (lblSovereignty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSovereignty1s.Text;
                else if (lblISO4217CurCode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO4217CurCode1s.Text;
                else if (lblISO4217CurName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO4217CurName1s.Text;
                else if (lblITUTTelephoneCode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITUTTelephoneCode1s.Text;
                else if (lblFaxLength1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFaxLength1s.Text;
                else if (lblTelLength1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTelLength1s.Text;
                else if (lblISO3166_1_2LetterCode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO3166_1_2LetterCode1s.Text;
                else if (lblISO3166_1_3LetterCode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO3166_1_3LetterCode1s.Text;
                else if (lblISO3166_1Number1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO3166_1Number1s.Text;
                else if (lblIANACountryCodeTLD1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIANACountryCodeTLD1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

                else if (lblTenantID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID2h.Text;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                else if (lblREGION12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREGION12h.Text;
                else if (lblCOUNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNAME12h.Text;
                else if (lblCOUNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNAME22h.Text;
                else if (lblCOUNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNAME32h.Text;
                else if (lblCAPITAL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCAPITAL2h.Text;
                else if (lblNATIONALITY12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNATIONALITY12h.Text;
                else if (lblNATIONALITY22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNATIONALITY22h.Text;
                else if (lblNATIONALITY32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNATIONALITY32h.Text;
                else if (lblCURRENCYNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYNAME12h.Text;
                else if (lblCURRENCYNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYNAME22h.Text;
                else if (lblCURRENCYNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYNAME32h.Text;
                else if (lblCURRENTCONVRATE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENTCONVRATE2h.Text;
                else if (lblCURRENCYSHORTNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYSHORTNAME12h.Text;
                else if (lblCURRENCYSHORTNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYSHORTNAME22h.Text;
                else if (lblCURRENCYSHORTNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCURRENCYSHORTNAME32h.Text;
                else if (lblCountryType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCountryType2h.Text;
                else if (lblCountryTSubType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCountryTSubType2h.Text;
                else if (lblSovereignty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSovereignty2h.Text;
                else if (lblISO4217CurCode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO4217CurCode2h.Text;
                else if (lblISO4217CurName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO4217CurName2h.Text;
                else if (lblITUTTelephoneCode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITUTTelephoneCode2h.Text;
                else if (lblFaxLength2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFaxLength2h.Text;
                else if (lblTelLength2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTelLength2h.Text;
                else if (lblISO3166_1_2LetterCode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO3166_1_2LetterCode2h.Text;
                else if (lblISO3166_1_3LetterCode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO3166_1_3LetterCode2h.Text;
                else if (lblISO3166_1Number2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtISO3166_1Number2h.Text;
                else if (lblIANACountryCodeTLD2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIANACountryCodeTLD2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblCOUNTRY.xml"));

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
            //drpCOUNTRYID.Enabled = true;
            txtREGION1.Enabled = true;
            txtCOUNAME1.Enabled = true;
            txtCOUNAME2.Enabled = true;
            txtCOUNAME3.Enabled = true;
            txtCAPITAL.Enabled = true;
            txtNATIONALITY1.Enabled = true;
            txtNATIONALITY2.Enabled = true;
            txtNATIONALITY3.Enabled = true;
            txtCURRENCYNAME1.Enabled = true;
            txtCURRENCYNAME2.Enabled = true;
            txtCURRENCYNAME3.Enabled = true;
            txtCURRENTCONVRATE.Enabled = true;
            txtCURRENCYSHORTNAME1.Enabled = true;
            txtCURRENCYSHORTNAME2.Enabled = true;
            txtCURRENCYSHORTNAME3.Enabled = true;
            drpCountryType.Enabled = true;
            txtCountryTSubType.Enabled = true;
            txtSovereignty.Enabled = true;
            txtISO4217CurCode.Enabled = true;
            txtISO4217CurName.Enabled = true;
            txtITUTTelephoneCode.Enabled = true;
            txtFaxLength.Enabled = true;
            txtTelLength.Enabled = true;
            txtISO3166_1_2LetterCode.Enabled = true;
            txtISO3166_1_3LetterCode.Enabled = true;
            txtISO3166_1Number.Enabled = true;
            txtIANACountryCodeTLD.Enabled = true;
            //cbActive.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpCOUNTRYID.Enabled = false;
            txtREGION1.Enabled = false;
            txtCOUNAME1.Enabled = false;
            txtCOUNAME2.Enabled = false;
            txtCOUNAME3.Enabled = false;
            txtCAPITAL.Enabled = false;
            txtNATIONALITY1.Enabled = false;
            txtNATIONALITY2.Enabled = false;
            txtNATIONALITY3.Enabled = false;
            txtCURRENCYNAME1.Enabled = false;
            txtCURRENCYNAME2.Enabled = false;
            txtCURRENCYNAME3.Enabled = false;
            txtCURRENTCONVRATE.Enabled = false;
            txtCURRENCYSHORTNAME1.Enabled = false;
            txtCURRENCYSHORTNAME2.Enabled = false;
            txtCURRENCYSHORTNAME3.Enabled = false;
            drpCountryType.Enabled = false;
            txtCountryTSubType.Enabled = false;
            txtSovereignty.Enabled = false;
            txtISO4217CurCode.Enabled = false;
            txtISO4217CurName.Enabled = false;
            txtITUTTelephoneCode.Enabled = false;
            txtFaxLength.Enabled = false;
            txtTelLength.Enabled = false;
            txtISO3166_1_2LetterCode.Enabled = false;
            txtISO3166_1_3LetterCode.Enabled = false;
            txtISO3166_1Number.Enabled = false;
            txtIANACountryCodeTLD.Enabled = false;
            //cbActive.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblCOUNTRies.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCOUNTRies.OrderBy(m => m.COUNTRYID).Take(take).Skip(Skip)).ToList());
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

            ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblCOUNTRies.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCOUNTRies.OrderBy(m => m.COUNTRYID).Take(take).Skip(Skip)).ToList());
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
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblCOUNTRies.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCOUNTRies.OrderBy(m => m.COUNTRYID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblCOUNTRies.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCOUNTRies.OrderBy(m => m.COUNTRYID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }
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
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //try
            //{
            if (e.CommandName == "btnDelete")
            {
                //string[] ID = e.CommandArgument.ToString().Split(',');
                //string str1 = ID[0].ToString();
                //string str2 = ID[1].ToString();
                int CID = Convert.ToInt16(e.CommandArgument);

                Database.tblCOUNTRY objtblCOUNTRY = DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == CID && p.Active == "Y");
                objtblCOUNTRY.Active = "N";
                objtblCOUNTRY.UploadDate = DateTime.Now;
                objtblCOUNTRY.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                objtblCOUNTRY.SynID = 3;

                DB.SaveChanges();

                Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                BindData();
                //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                //((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCOUNTRies.OrderBy(m => m.COUNTRYID).Take(Tvalue).Skip(Svalue)).ToList());

            }

            if (e.CommandName == "btnEdit")
            {
                //string[] ID = e.CommandArgument.ToString().Split(',');
                //string str1 = ID[0].ToString();
                //string str2 = ID[1].ToString();
                int ID = Convert.ToInt16(e.CommandArgument);

                Database.tblCOUNTRY objtblCOUNTRY = DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == ID);
                //drpCOUNTRYID.SelectedValue = objtblCOUNTRY.COUNTRYID.ToString();
                txtREGION1.Text = objtblCOUNTRY.REGION1.ToString();
                txtCOUNAME1.Text = objtblCOUNTRY.COUNAME1.ToString();
                txtCOUNAME2.Text = objtblCOUNTRY.COUNAME2.ToString();
                txtCOUNAME3.Text = objtblCOUNTRY.COUNAME3.ToString();
                txtCAPITAL.Text = objtblCOUNTRY.CAPITAL.ToString();
                txtNATIONALITY1.Text = objtblCOUNTRY.NATIONALITY1.ToString();
                txtNATIONALITY2.Text = objtblCOUNTRY.NATIONALITY2.ToString();
                txtNATIONALITY3.Text = objtblCOUNTRY.NATIONALITY3.ToString();
                txtCURRENCYNAME1.Text = objtblCOUNTRY.CURRENCYNAME1.ToString();
                txtCURRENCYNAME2.Text = objtblCOUNTRY.CURRENCYNAME2.ToString();
                txtCURRENCYNAME3.Text = objtblCOUNTRY.CURRENCYNAME3.ToString();
                txtCURRENTCONVRATE.Text = objtblCOUNTRY.CURRENTCONVRATE.ToString();
                if (objtblCOUNTRY.CURRENCYSHORTNAME1 == null || objtblCOUNTRY.CURRENCYSHORTNAME1 == "")
                {

                }
                else
                {
                    txtCURRENCYSHORTNAME1.Text = objtblCOUNTRY.CURRENCYSHORTNAME1.ToString();
                }
                if (objtblCOUNTRY.CURRENCYSHORTNAME2 == null || objtblCOUNTRY.CURRENCYSHORTNAME2 == "")
                {

                }
                else
                {
                    txtCURRENCYSHORTNAME2.Text = objtblCOUNTRY.CURRENCYSHORTNAME2.ToString();
                }
                if (objtblCOUNTRY.CURRENCYSHORTNAME3 == null || objtblCOUNTRY.CURRENCYSHORTNAME3 == "")
                {

                }
                else
                {
                    txtCURRENCYSHORTNAME3.Text = objtblCOUNTRY.CURRENCYSHORTNAME3.ToString();
                }
                drpCountryType.SelectedValue = objtblCOUNTRY.CountryType.ToString();
                if (objtblCOUNTRY.CountryTSubType != null)
                    txtCountryTSubType.Text = objtblCOUNTRY.CountryTSubType.ToString();

                if (objtblCOUNTRY.Sovereignty != null)
                    txtSovereignty.Text = objtblCOUNTRY.Sovereignty.ToString();
                txtISO4217CurCode.Text = objtblCOUNTRY.ISO4217CurCode.ToString();

                if (objtblCOUNTRY.ISO4217CurName != null)
                    txtISO4217CurName.Text = objtblCOUNTRY.ISO4217CurName.ToString();
                txtITUTTelephoneCode.Text = objtblCOUNTRY.ITUTTelephoneCode.ToString();
                txtFaxLength.Text = objtblCOUNTRY.FaxLength.ToString();
                txtTelLength.Text = objtblCOUNTRY.TelLength.ToString();
                txtISO3166_1_2LetterCode.Text = objtblCOUNTRY.ISO3166_1_2LetterCode.ToString();
                txtISO3166_1_3LetterCode.Text = objtblCOUNTRY.ISO3166_1_3LetterCode.ToString();

                if (objtblCOUNTRY.ISO3166_1Number != null)
                    txtISO3166_1Number.Text = objtblCOUNTRY.ISO3166_1Number.ToString();
                txtIANACountryCodeTLD.Text = objtblCOUNTRY.IANACountryCodeTLD.ToString();
                if (objtblCOUNTRY.Active == "Y")
                {
                    cbActive.Checked = true;
                }
                else
                {
                    cbActive.Checked = false;
                }
                //txtActive.Text = objtblCOUNTRY.Active.ToString();
                //txtCRUP_ID.Text = objtblCOUNTRY.CRUP_ID.ToString();

                btnAdd.Text = "Update";
                ViewState["Edit"] = ID;
                Write();
            }
            //scope.Complete(); //  To commit.
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //    throw;
            //}
            //}
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblCOUNTRies.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCOUNTRies.OrderBy(m => m.COUNTRYID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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


    }
}