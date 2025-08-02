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
    public partial class TBLLOCATION : System.Web.UI.Page
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
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "s";

            }
        }
        #region Step2
        public void BindData()
        {

            Listview1.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.LOCATIONID).OrderByDescending(a => a.LOCATIONID);
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

        #region PAge Genarator

        public void GetShow()
        {

            lblPHYSICALLOCID1s.Attributes["class"] = lblLOCNAME11s.Attributes["class"] = lblLOCNAME21s.Attributes["class"] = lblLOCNAME31s.Attributes["class"] = lblADDRESS1s.Attributes["class"] = lblDEPT1s.Attributes["class"] = lblSECTIONCODE1s.Attributes["class"] = lblACCOUNT1s.Attributes["class"] = lblMAXDISCOUNT1s.Attributes["class"] = lblUSERID1s.Attributes["class"] = lblENTRYDATE1s.Attributes["class"] = lblENTRYTIME1s.Attributes["class"] = lblUPDTTIME1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblLOCNAME1s.Attributes["class"] = lblLOCNAMEO1s.Attributes["class"] = lblLOCNAMECH1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPHYSICALLOCID2h.Attributes["class"] = lblLOCNAME12h.Attributes["class"] = lblLOCNAME22h.Attributes["class"] = lblLOCNAME32h.Attributes["class"] = lblADDRESS2h.Attributes["class"] = lblDEPT2h.Attributes["class"] = lblSECTIONCODE2h.Attributes["class"] = lblACCOUNT2h.Attributes["class"] = lblMAXDISCOUNT2h.Attributes["class"] = lblUSERID2h.Attributes["class"] = lblENTRYDATE2h.Attributes["class"] = lblENTRYTIME2h.Attributes["class"] = lblUPDTTIME2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblLOCNAME2h.Attributes["class"] = lblLOCNAMEO2h.Attributes["class"] = lblLOCNAMECH2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPHYSICALLOCID1s.Attributes["class"] = lblLOCNAME11s.Attributes["class"] = lblLOCNAME21s.Attributes["class"] = lblLOCNAME31s.Attributes["class"] = lblADDRESS1s.Attributes["class"] = lblDEPT1s.Attributes["class"] = lblSECTIONCODE1s.Attributes["class"] = lblACCOUNT1s.Attributes["class"] = lblMAXDISCOUNT1s.Attributes["class"] = lblUSERID1s.Attributes["class"] = lblENTRYDATE1s.Attributes["class"] = lblENTRYTIME1s.Attributes["class"] = lblUPDTTIME1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblLOCNAME1s.Attributes["class"] = lblLOCNAMEO1s.Attributes["class"] = lblLOCNAMECH1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPHYSICALLOCID2h.Attributes["class"] = lblLOCNAME12h.Attributes["class"] = lblLOCNAME22h.Attributes["class"] = lblLOCNAME32h.Attributes["class"] = lblADDRESS2h.Attributes["class"] = lblDEPT2h.Attributes["class"] = lblSECTIONCODE2h.Attributes["class"] = lblACCOUNT2h.Attributes["class"] = lblMAXDISCOUNT2h.Attributes["class"] = lblUSERID2h.Attributes["class"] = lblENTRYDATE2h.Attributes["class"] = lblENTRYTIME2h.Attributes["class"] = lblUPDTTIME2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblLOCNAME2h.Attributes["class"] = lblLOCNAMEO2h.Attributes["class"] = lblLOCNAMECH2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  getshow";
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
        #endregion
        public void Clear()
        {
            //txtLOCATIONID.Text = "";
            txtPHYSICALLOCID.Text = "";
            txtLOCNAME1.Text = "";
            txtLOCNAME2.Text = "";
            txtLOCNAME3.Text = "";
            txtADDRESS.Text = "";
            txtDEPT.Text = "";
            txtSECTIONCODE.Text = "";
            txtACCOUNT.Text = "";
            // drpMAXDISCOUNT.SelectedIndex = 0;
            txtUSERID.Text = "";
            txtENTRYDATE.Text = "";
            txtENTRYTIME.Text = "";
            txtUPDTTIME.Text = "";
            cbActive.Checked = false;
            txtLOCNAME.Text = "";
            txtLOCNAMEO.Text = "";
            txtLOCNAMECH.Text = "";
            //drpCRUP_ID.SelectedIndex = 0;

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
                    btnAdd.ValidationGroup = "submit";
                }
                else if (btnAdd.Text == "Save")
                {
                    Database.TBLLOCATION objTBLLOCATION = new Database.TBLLOCATION();
                    //Server Content Send data Yogesh
                    objTBLLOCATION.TenentID = TID;
                    objTBLLOCATION.LOCATIONID = DB.TBLLOCATIONs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLLOCATIONs.Where(p => p.TenentID == TID).Max(p => p.LOCATIONID) + 1) : 1;
                    //objTBLLOCATION.LOCATIONID = txtLOCATIONID.Text;
                    objTBLLOCATION.PHYSICALLOCID = txtPHYSICALLOCID.Text;
                    objTBLLOCATION.LOCNAME1 = txtLOCNAME1.Text;
                    objTBLLOCATION.LOCNAME2 = txtLOCNAME2.Text;
                    objTBLLOCATION.LOCNAME3 = txtLOCNAME3.Text;
                    objTBLLOCATION.ADDRESS = txtADDRESS.Text;
                    objTBLLOCATION.DEPT = txtDEPT.Text;
                    objTBLLOCATION.SECTIONCODE = txtSECTIONCODE.Text;
                    objTBLLOCATION.ACCOUNT = txtACCOUNT.Text;
                    //objTBLLOCATION.MAXDISCOUNT = Convert.ToInt32(drpMAXDISCOUNT.SelectedValue);
                    objTBLLOCATION.USERID = txtUSERID.Text;
                    objTBLLOCATION.ENTRYDATE = Convert.ToDateTime(txtENTRYDATE.Text);
                    objTBLLOCATION.ENTRYTIME = Convert.ToDateTime(txtENTRYTIME.Text);
                    objTBLLOCATION.UPDTTIME = Convert.ToDateTime(txtUPDTTIME.Text);
                    objTBLLOCATION.Active = "Y";
                    //objTBLLOCATION.Active = cbActive.Checked ? "Y" : "N";
                    objTBLLOCATION.LOCNAME = txtLOCNAME.Text;
                    objTBLLOCATION.LOCNAMEO = txtLOCNAMEO.Text;
                    objTBLLOCATION.LOCNAMECH = txtLOCNAMECH.Text;
                    objTBLLOCATION.UploadDate = DateTime.Now;
                    objTBLLOCATION.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objTBLLOCATION.SynID = 1;
                    //objTBLLOCATION.CRUP_ID =Convert.ToInt64(drpCRUP_ID.SelectedValue);


                    DB.TBLLOCATIONs.AddObject(objTBLLOCATION);
                    DB.SaveChanges();

                    Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                    Clear();
                    //lblMsg.Text = "  Data Save Successfully";
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "";
                    //pnlSuccessMsg.Visible = true;
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    BindData();
                    //navigation.Visible = true;
                    Readonly();

                    //FirstData();
                }
                else if (btnAdd.Text == "Update")
                {

                    if (ViewState["TIDD"] != null && ViewState["LID"] != null)
                    {
                        string ID = ViewState["Edit"].ToString();
                        //int ID = Convert.ToInt32(ViewState["Edit"]);
                        int tidd = Convert.ToInt32(ViewState["TIDD"]);
                        int lid = Convert.ToInt32(ViewState["LID"]);
                        Database.TBLLOCATION objTBLLOCATION = DB.TBLLOCATIONs.Single(p => p.LOCATIONID == lid && p.TenentID == tidd);
                        //objTBLLOCATION.LOCATIONID = txtLOCATIONID.Text;
                        objTBLLOCATION.PHYSICALLOCID = txtPHYSICALLOCID.Text;
                        objTBLLOCATION.LOCNAME1 = txtLOCNAME1.Text;
                        objTBLLOCATION.LOCNAME2 = txtLOCNAME2.Text;
                        objTBLLOCATION.LOCNAME3 = txtLOCNAME3.Text;
                        objTBLLOCATION.ADDRESS = txtADDRESS.Text;
                        objTBLLOCATION.DEPT = txtDEPT.Text;
                        objTBLLOCATION.SECTIONCODE = txtSECTIONCODE.Text;
                        objTBLLOCATION.ACCOUNT = txtACCOUNT.Text;
                        // objTBLLOCATION.MAXDISCOUNT = Convert.ToInt32(drpMAXDISCOUNT.SelectedValue);
                        objTBLLOCATION.USERID = txtUSERID.Text;
                        objTBLLOCATION.ENTRYDATE = Convert.ToDateTime(txtENTRYDATE.Text);
                        objTBLLOCATION.ENTRYTIME = Convert.ToDateTime(txtENTRYTIME.Text);
                        objTBLLOCATION.UPDTTIME = Convert.ToDateTime(txtUPDTTIME.Text);
                        objTBLLOCATION.Active = "Y";
                        //objTBLLOCATION.Active = cbActive.Checked ? "Y" : "N";
                        objTBLLOCATION.LOCNAME = txtLOCNAME.Text;
                        objTBLLOCATION.LOCNAMEO = txtLOCNAMEO.Text;
                        objTBLLOCATION.LOCNAMECH = txtLOCNAMECH.Text;
                        objTBLLOCATION.UploadDate = DateTime.Now;
                        objTBLLOCATION.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                        objTBLLOCATION.SynID = 2;
                        //objTBLLOCATION.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);

                        ViewState["Edit"] = null;
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = " ";
                        DB.SaveChanges();

                        Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                        Clear();
                        //lblMsg.Text = "  Data Edit Successfully";
                        //pnlSuccessMsg.Visible = true;
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                }
                BindData();

                scope.Complete(); //  To commit.

            }
            //    catch (Exception ex)
            //    {
            //        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Saving Data !<br>" + ex.ToString(), "Add", Classes.Toastr.ToastPosition.TopCenter);
            //        //throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();
        }

        #region PAge Genarator navigation
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
            //txtLOCATIONID.Text = Listview1.SelectedDataKey[0].ToString();
            txtPHYSICALLOCID.Text = Listview1.SelectedDataKey[0].ToString();
            txtLOCNAME1.Text = Listview1.SelectedDataKey[1].ToString();
            txtLOCNAME2.Text = Listview1.SelectedDataKey[2].ToString();
            txtLOCNAME3.Text = Listview1.SelectedDataKey[3].ToString();
            txtADDRESS.Text = Listview1.SelectedDataKey[4].ToString();
            txtDEPT.Text = Listview1.SelectedDataKey[5].ToString();
            txtSECTIONCODE.Text = Listview1.SelectedDataKey[6].ToString();
            txtACCOUNT.Text = Listview1.SelectedDataKey[7].ToString();
            drpMAXDISCOUNT.SelectedValue = Listview1.SelectedDataKey[8].ToString();
            txtUSERID.Text = Listview1.SelectedDataKey[9].ToString();
            txtENTRYDATE.Text = Listview1.SelectedDataKey[10].ToString();
            txtENTRYTIME.Text = Listview1.SelectedDataKey[11].ToString();
            txtUPDTTIME.Text = Listview1.SelectedDataKey[12].ToString();
            //txtActive.Text = Listview1.SelectedDataKey[0].ToString();
            txtLOCNAME.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
            txtLOCNAMEO.Text = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
            txtLOCNAMECH.Text = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
            drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //txtLOCATIONID.Text = Listview1.SelectedDataKey[0].ToString();
                txtPHYSICALLOCID.Text = Listview1.SelectedDataKey[0].ToString();
                txtLOCNAME1.Text = Listview1.SelectedDataKey[1].ToString();
                txtLOCNAME2.Text = Listview1.SelectedDataKey[2].ToString();
                txtLOCNAME3.Text = Listview1.SelectedDataKey[3].ToString();
                txtADDRESS.Text = Listview1.SelectedDataKey[4].ToString();
                txtDEPT.Text = Listview1.SelectedDataKey[5].ToString();
                txtSECTIONCODE.Text = Listview1.SelectedDataKey[6].ToString();
                txtACCOUNT.Text = Listview1.SelectedDataKey[7].ToString();
                drpMAXDISCOUNT.SelectedValue = Listview1.SelectedDataKey[8].ToString();
                txtUSERID.Text = Listview1.SelectedDataKey[9].ToString();
                txtENTRYDATE.Text = Listview1.SelectedDataKey[10].ToString();
                txtENTRYTIME.Text = Listview1.SelectedDataKey[11].ToString();
                txtUPDTTIME.Text = Listview1.SelectedDataKey[12].ToString();
                //txtActive.Text = Listview1.SelectedDataKey[0].ToString();
                txtLOCNAME.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
                txtLOCNAMEO.Text = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
                txtLOCNAMECH.Text = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
                drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";

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
                //txtLOCATIONID.Text = Listview1.SelectedDataKey[0].ToString();
                txtPHYSICALLOCID.Text = Listview1.SelectedDataKey[0].ToString();
                txtLOCNAME1.Text = Listview1.SelectedDataKey[1].ToString();
                txtLOCNAME2.Text = Listview1.SelectedDataKey[2].ToString();
                txtLOCNAME3.Text = Listview1.SelectedDataKey[3].ToString();
                txtADDRESS.Text = Listview1.SelectedDataKey[4].ToString();
                txtDEPT.Text = Listview1.SelectedDataKey[5].ToString();
                txtSECTIONCODE.Text = Listview1.SelectedDataKey[6].ToString();
                txtACCOUNT.Text = Listview1.SelectedDataKey[7].ToString();
                drpMAXDISCOUNT.SelectedValue = Listview1.SelectedDataKey[8].ToString();
                txtUSERID.Text = Listview1.SelectedDataKey[9].ToString();
                txtENTRYDATE.Text = Listview1.SelectedDataKey[10].ToString();
                txtENTRYTIME.Text = Listview1.SelectedDataKey[11].ToString();
                txtUPDTTIME.Text = Listview1.SelectedDataKey[12].ToString();
                //txtActive.Text = Listview1.SelectedDataKey[0].ToString();
                txtLOCNAME.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
                txtLOCNAMEO.Text = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
                txtLOCNAMECH.Text = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
                drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //txtLOCATIONID.Text = Listview1.SelectedDataKey[0].ToString();
            txtPHYSICALLOCID.Text = Listview1.SelectedDataKey[0].ToString();
            txtLOCNAME1.Text = Listview1.SelectedDataKey[1].ToString();
            txtLOCNAME2.Text = Listview1.SelectedDataKey[2].ToString();
            txtLOCNAME3.Text = Listview1.SelectedDataKey[3].ToString();
            txtADDRESS.Text = Listview1.SelectedDataKey[4].ToString();
            txtDEPT.Text = Listview1.SelectedDataKey[5].ToString();
            txtSECTIONCODE.Text = Listview1.SelectedDataKey[6].ToString();
            txtACCOUNT.Text = Listview1.SelectedDataKey[7].ToString();
            drpMAXDISCOUNT.SelectedValue = Listview1.SelectedDataKey[8].ToString();
            txtUSERID.Text = Listview1.SelectedDataKey[9].ToString();
            txtENTRYDATE.Text = Listview1.SelectedDataKey[10].ToString();
            txtENTRYTIME.Text = Listview1.SelectedDataKey[11].ToString();
            txtUPDTTIME.Text = Listview1.SelectedDataKey[12].ToString();
            //txtActive.Text = Listview1.SelectedDataKey[0].ToString();
            txtLOCNAME.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
            txtLOCNAMEO.Text = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
            txtLOCNAMECH.Text = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
            drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";

        }

        #endregion

        #region PAge Genarator language


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblPHYSICALLOCID2h.Visible = lblLOCNAME12h.Visible = lblLOCNAME22h.Visible = lblLOCNAME32h.Visible = lblADDRESS2h.Visible = lblDEPT2h.Visible = lblSECTIONCODE2h.Visible = lblACCOUNT2h.Visible = lblMAXDISCOUNT2h.Visible = lblUSERID2h.Visible = lblENTRYDATE2h.Visible = lblENTRYTIME2h.Visible = lblUPDTTIME2h.Visible = lblActive2h.Visible = lblLOCNAME2h.Visible = lblLOCNAMEO2h.Visible = lblLOCNAMECH2h.Visible = lblCRUP_ID2h.Visible = false;
                    //2true
                    txtPHYSICALLOCID2h.Visible = txtLOCNAME12h.Visible = txtLOCNAME22h.Visible = txtLOCNAME32h.Visible = txtADDRESS2h.Visible = txtDEPT2h.Visible = txtSECTIONCODE2h.Visible = txtACCOUNT2h.Visible = txtMAXDISCOUNT2h.Visible = txtUSERID2h.Visible = txtENTRYDATE2h.Visible = txtENTRYTIME2h.Visible = txtUPDTTIME2h.Visible = txtActive2h.Visible = txtLOCNAME2h.Visible = txtLOCNAMEO2h.Visible = txtLOCNAMECH2h.Visible = txtCRUP_ID2h.Visible = true;

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
                    lblPHYSICALLOCID2h.Visible = lblLOCNAME12h.Visible = lblLOCNAME22h.Visible = lblLOCNAME32h.Visible = lblADDRESS2h.Visible = lblDEPT2h.Visible = lblSECTIONCODE2h.Visible = lblACCOUNT2h.Visible = lblMAXDISCOUNT2h.Visible = lblUSERID2h.Visible = lblENTRYDATE2h.Visible = lblENTRYTIME2h.Visible = lblUPDTTIME2h.Visible = lblActive2h.Visible = lblLOCNAME2h.Visible = lblLOCNAMEO2h.Visible = lblLOCNAMECH2h.Visible = lblCRUP_ID2h.Visible = true;
                    //2false
                    txtPHYSICALLOCID2h.Visible = txtLOCNAME12h.Visible = txtLOCNAME22h.Visible = txtLOCNAME32h.Visible = txtADDRESS2h.Visible = txtDEPT2h.Visible = txtSECTIONCODE2h.Visible = txtACCOUNT2h.Visible = txtMAXDISCOUNT2h.Visible = txtUSERID2h.Visible = txtENTRYDATE2h.Visible = txtENTRYTIME2h.Visible = txtUPDTTIME2h.Visible = txtActive2h.Visible = txtLOCNAME2h.Visible = txtLOCNAMEO2h.Visible = txtLOCNAMECH2h.Visible = txtCRUP_ID2h.Visible = false;

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
                    lblPHYSICALLOCID1s.Visible = lblLOCNAME11s.Visible = lblLOCNAME21s.Visible = lblLOCNAME31s.Visible = lblADDRESS1s.Visible = lblDEPT1s.Visible = lblSECTIONCODE1s.Visible = lblACCOUNT1s.Visible = lblMAXDISCOUNT1s.Visible = lblUSERID1s.Visible = lblENTRYDATE1s.Visible = lblENTRYTIME1s.Visible = lblUPDTTIME1s.Visible = lblActive1s.Visible = lblLOCNAME1s.Visible = lblLOCNAMEO1s.Visible = lblLOCNAMECH1s.Visible = lblCRUP_ID1s.Visible = false;
                    //1true
                    txtPHYSICALLOCID1s.Visible = txtLOCNAME11s.Visible = txtLOCNAME21s.Visible = txtLOCNAME31s.Visible = txtADDRESS1s.Visible = txtDEPT1s.Visible = txtSECTIONCODE1s.Visible = txtACCOUNT1s.Visible = txtMAXDISCOUNT1s.Visible = txtUSERID1s.Visible = txtENTRYDATE1s.Visible = txtENTRYTIME1s.Visible = txtUPDTTIME1s.Visible = txtActive1s.Visible = txtLOCNAME1s.Visible = txtLOCNAMEO1s.Visible = txtLOCNAMECH1s.Visible = txtCRUP_ID1s.Visible = true;
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
                    lblPHYSICALLOCID1s.Visible = lblLOCNAME11s.Visible = lblLOCNAME21s.Visible = lblLOCNAME31s.Visible = lblADDRESS1s.Visible = lblDEPT1s.Visible = lblSECTIONCODE1s.Visible = lblACCOUNT1s.Visible = lblMAXDISCOUNT1s.Visible = lblUSERID1s.Visible = lblENTRYDATE1s.Visible = lblENTRYTIME1s.Visible = lblUPDTTIME1s.Visible = lblActive1s.Visible = lblLOCNAME1s.Visible = lblLOCNAMEO1s.Visible = lblLOCNAMECH1s.Visible = lblCRUP_ID1s.Visible = true;
                    //1false
                    txtPHYSICALLOCID1s.Visible = txtLOCNAME11s.Visible = txtLOCNAME21s.Visible = txtLOCNAME31s.Visible = txtADDRESS1s.Visible = txtDEPT1s.Visible = txtSECTIONCODE1s.Visible = txtACCOUNT1s.Visible = txtMAXDISCOUNT1s.Visible = txtUSERID1s.Visible = txtENTRYDATE1s.Visible = txtENTRYTIME1s.Visible = txtUPDTTIME1s.Visible = txtActive1s.Visible = txtLOCNAME1s.Visible = txtLOCNAMEO1s.Visible = txtLOCNAMECH1s.Visible = txtCRUP_ID1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLLOCATION").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenantID1s.ID == item.LabelID)
                //    txtTenantID1s.Text = lblTenantID1s.Text = item.LabelName;
                //else if (lblLOCATIONID1s.ID == item.LabelID)
                //    txtLOCATIONID1s.Text = lblLOCATIONID1s.Text = item.LabelName;
                if (lblPHYSICALLOCID1s.ID == item.LabelID)
                    txtPHYSICALLOCID1s.Text = lblPHYSICALLOCID1s.Text = item.LabelName;
                else if (lblLOCNAME11s.ID == item.LabelID)
                    txtLOCNAME11s.Text = lblLOCNAME11s.Text = lblhLOCNAME1.Text = item.LabelName;
                else if (lblLOCNAME21s.ID == item.LabelID)
                    txtLOCNAME21s.Text = lblLOCNAME21s.Text = lblhLOCNAME2.Text = item.LabelName;
                else if (lblLOCNAME31s.ID == item.LabelID)
                    txtLOCNAME31s.Text = lblLOCNAME31s.Text = lblhLOCNAME3.Text = item.LabelName;
                else if (lblADDRESS1s.ID == item.LabelID)
                    txtADDRESS1s.Text = lblADDRESS1s.Text = item.LabelName;
                else if (lblDEPT1s.ID == item.LabelID)
                    txtDEPT1s.Text = lblDEPT1s.Text = item.LabelName;
                else if (lblSECTIONCODE1s.ID == item.LabelID)
                    txtSECTIONCODE1s.Text = lblSECTIONCODE1s.Text = item.LabelName;
                else if (lblACCOUNT1s.ID == item.LabelID)
                    txtACCOUNT1s.Text = lblACCOUNT1s.Text = item.LabelName;
                else if (lblMAXDISCOUNT1s.ID == item.LabelID)
                    txtMAXDISCOUNT1s.Text = lblMAXDISCOUNT1s.Text = item.LabelName;
                else if (lblUSERID1s.ID == item.LabelID)
                    txtUSERID1s.Text = lblUSERID1s.Text = item.LabelName;
                else if (lblENTRYDATE1s.ID == item.LabelID)
                    txtENTRYDATE1s.Text = lblENTRYDATE1s.Text = item.LabelName;
                else if (lblENTRYTIME1s.ID == item.LabelID)
                    txtENTRYTIME1s.Text = lblENTRYTIME1s.Text = item.LabelName;
                else if (lblUPDTTIME1s.ID == item.LabelID)
                    txtUPDTTIME1s.Text = lblUPDTTIME1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblLOCNAME1s.ID == item.LabelID)
                    txtLOCNAME1s.Text = lblLOCNAME1s.Text = item.LabelName;
                else if (lblLOCNAMEO1s.ID == item.LabelID)
                    txtLOCNAMEO1s.Text = lblLOCNAMEO1s.Text = item.LabelName;
                else if (lblLOCNAMECH1s.ID == item.LabelID)
                    txtLOCNAMECH1s.Text = lblLOCNAMECH1s.Text = item.LabelName;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;

                //else if (lblTenantID2h.ID == item.LabelID)
                //    txtTenantID2h.Text = lblTenantID2h.Text = item.LabelName;
                //else if (lblLOCATIONID2h.ID == item.LabelID)
                //    txtLOCATIONID2h.Text = lblLOCATIONID2h.Text = item.LabelName;
                else if (lblPHYSICALLOCID2h.ID == item.LabelID)
                    txtPHYSICALLOCID2h.Text = lblPHYSICALLOCID2h.Text = item.LabelName;
                else if (lblLOCNAME12h.ID == item.LabelID)
                    txtLOCNAME12h.Text = lblLOCNAME12h.Text = lblhLOCNAME1.Text = item.LabelName;
                else if (lblLOCNAME22h.ID == item.LabelID)
                    txtLOCNAME22h.Text = lblLOCNAME22h.Text = lblhLOCNAME2.Text = item.LabelName;
                else if (lblLOCNAME32h.ID == item.LabelID)
                    txtLOCNAME32h.Text = lblLOCNAME32h.Text = lblhLOCNAME3.Text = item.LabelName;
                else if (lblADDRESS2h.ID == item.LabelID)
                    txtADDRESS2h.Text = lblADDRESS2h.Text = item.LabelName;
                else if (lblDEPT2h.ID == item.LabelID)
                    txtDEPT2h.Text = lblDEPT2h.Text = item.LabelName;
                else if (lblSECTIONCODE2h.ID == item.LabelID)
                    txtSECTIONCODE2h.Text = lblSECTIONCODE2h.Text = item.LabelName;
                else if (lblACCOUNT2h.ID == item.LabelID)
                    txtACCOUNT2h.Text = lblACCOUNT2h.Text = item.LabelName;
                else if (lblMAXDISCOUNT2h.ID == item.LabelID)
                    txtMAXDISCOUNT2h.Text = lblMAXDISCOUNT2h.Text = item.LabelName;
                else if (lblUSERID2h.ID == item.LabelID)
                    txtUSERID2h.Text = lblUSERID2h.Text = item.LabelName;
                else if (lblENTRYDATE2h.ID == item.LabelID)
                    txtENTRYDATE2h.Text = lblENTRYDATE2h.Text = item.LabelName;
                else if (lblENTRYTIME2h.ID == item.LabelID)
                    txtENTRYTIME2h.Text = lblENTRYTIME2h.Text = item.LabelName;
                else if (lblUPDTTIME2h.ID == item.LabelID)
                    txtUPDTTIME2h.Text = lblUPDTTIME2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblLOCNAME2h.ID == item.LabelID)
                    txtLOCNAME2h.Text = lblLOCNAME2h.Text = item.LabelName;
                else if (lblLOCNAMEO2h.ID == item.LabelID)
                    txtLOCNAMEO2h.Text = lblLOCNAMEO2h.Text = item.LabelName;
                else if (lblLOCNAMECH2h.ID == item.LabelID)
                    txtLOCNAMECH2h.Text = lblLOCNAMECH2h.Text = item.LabelName;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLLOCATION").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLLOCATION.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLLOCATION").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenantID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID1s.Text;
                //else if (lblLOCATIONID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLOCATIONID1s.Text;
                if (lblPHYSICALLOCID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPHYSICALLOCID1s.Text;
                else if (lblLOCNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME11s.Text;
                else if (lblLOCNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME21s.Text;
                else if (lblLOCNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME31s.Text;
                else if (lblADDRESS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDRESS1s.Text;
                else if (lblDEPT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDEPT1s.Text;
                else if (lblSECTIONCODE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSECTIONCODE1s.Text;
                else if (lblACCOUNT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACCOUNT1s.Text;
                else if (lblMAXDISCOUNT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMAXDISCOUNT1s.Text;
                else if (lblUSERID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSERID1s.Text;
                else if (lblENTRYDATE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYDATE1s.Text;
                else if (lblENTRYTIME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYTIME1s.Text;
                else if (lblUPDTTIME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUPDTTIME1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblLOCNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME1s.Text;
                else if (lblLOCNAMEO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAMEO1s.Text;
                else if (lblLOCNAMECH1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAMECH1s.Text;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

                //else if (lblTenantID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID2h.Text;
                //else if (lblLOCATIONID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLOCATIONID2h.Text;
                else if (lblPHYSICALLOCID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPHYSICALLOCID2h.Text;
                else if (lblLOCNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME12h.Text;
                else if (lblLOCNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME22h.Text;
                else if (lblLOCNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME32h.Text;
                else if (lblADDRESS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDRESS2h.Text;
                else if (lblDEPT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDEPT2h.Text;
                else if (lblSECTIONCODE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSECTIONCODE2h.Text;
                else if (lblACCOUNT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACCOUNT2h.Text;
                else if (lblMAXDISCOUNT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMAXDISCOUNT2h.Text;
                else if (lblUSERID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSERID2h.Text;
                else if (lblENTRYDATE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYDATE2h.Text;
                else if (lblENTRYTIME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtENTRYTIME2h.Text;
                else if (lblUPDTTIME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUPDTTIME2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblLOCNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAME2h.Text;
                else if (lblLOCNAMEO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAMEO2h.Text;
                else if (lblLOCNAMECH2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCNAMECH2h.Text;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLLOCATION.xml"));

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

        #endregion
        public void Write()
        {
            //navigation.Visible = false;
            //txtLOCATIONID.Enabled = true;
            txtPHYSICALLOCID.Enabled = true;
            txtLOCNAME1.Enabled = true;
            txtLOCNAME2.Enabled = true;
            txtLOCNAME3.Enabled = true;
            txtADDRESS.Enabled = true;
            txtDEPT.Enabled = true;
            txtSECTIONCODE.Enabled = true;
            txtACCOUNT.Enabled = true;
            drpMAXDISCOUNT.Enabled = true;
            txtUSERID.Enabled = true;
            txtENTRYDATE.Enabled = true;
            txtENTRYTIME.Enabled = true;
            txtUPDTTIME.Enabled = true;
            cbActive.Enabled = true;
            txtLOCNAME.Enabled = true;
            txtLOCNAMEO.Enabled = true;
            txtLOCNAMECH.Enabled = true;
            drpCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //txtLOCATIONID.Enabled = false;
            txtPHYSICALLOCID.Enabled = false;
            txtLOCNAME1.Enabled = false;
            txtLOCNAME2.Enabled = false;
            txtLOCNAME3.Enabled = false;
            txtADDRESS.Enabled = false;
            txtDEPT.Enabled = false;
            txtSECTIONCODE.Enabled = false;
            txtACCOUNT.Enabled = false;
            drpMAXDISCOUNT.Enabled = false;
            txtUSERID.Enabled = false;
            txtENTRYDATE.Enabled = false;
            txtENTRYTIME.Enabled = false;
            txtUPDTTIME.Enabled = false;
            //txtActive.Enabled = false;
            txtLOCNAME.Enabled = false;
            txtLOCNAMEO.Enabled = false;
            txtLOCNAMECH.Enabled = false;
            drpCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLLOCATIONs.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.LOCATIONID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.TBLLOCATIONs.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.LOCATIONID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.TBLLOCATIONs.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.LOCATIONID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.TBLLOCATIONs.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.LOCATIONID).Take(take).Skip(Skip)).ToList());
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
            //Readonly();
            //ManageLang();
            //pnlSuccessMsg.Visible = false;
            //FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            //BindData();
            // FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                if (e.CommandName == "btnDelete")
                {

                    string[] ID = e.CommandArgument.ToString().Split(',');
                    int TIDD = Convert.ToInt32(ID[0]);
                    int LID = Convert.ToInt32(ID[1]);
                    Database.TBLLOCATION objTBLLOCATION = DB.TBLLOCATIONs.Single(p => p.TenentID == TIDD && p.LOCATIONID == LID);
                    //int ID = Convert.ToInt32(e.CommandArgument);
                    //Database.TBLLOCATION objSOJobDesc = DB.TBLLOCATIONs.Single(p => p.LOCATIONID == ID);
                    objTBLLOCATION.Active = "N";
                    objTBLLOCATION.UploadDate = DateTime.Now;
                    objTBLLOCATION.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objTBLLOCATION.SynID = 3;

                    DB.SaveChanges();
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    BindData();
                    //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                    //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                    //((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLLOCATIONs.OrderBy(m => m.LOCATIONID).Take(Tvalue).Skip(Svalue)).ToList());

                }

                if (e.CommandName == "btnEdit")
                {
                    string[] ID = e.CommandArgument.ToString().Split(',');
                    int TIDD = Convert.ToInt32(ID[0]);
                    int LID = Convert.ToInt32(ID[1]);
                    //string LID = ID[1].ToString();
                    Database.TBLLOCATION objTBLLOCATION = DB.TBLLOCATIONs.Single(p => p.TenentID == TIDD && p.LOCATIONID == LID);

                    //txtLOCATIONID.Text = objTBLLOCATION.LOCATIONID.ToString();
                    txtPHYSICALLOCID.Text = objTBLLOCATION.PHYSICALLOCID;
                    txtLOCNAME1.Text = objTBLLOCATION.LOCNAME1;
                    txtLOCNAME2.Text = objTBLLOCATION.LOCNAME2;
                    txtLOCNAME3.Text = objTBLLOCATION.LOCNAME3;
                    txtADDRESS.Text = objTBLLOCATION.ADDRESS;
                    txtDEPT.Text = objTBLLOCATION.DEPT;
                    txtSECTIONCODE.Text = objTBLLOCATION.SECTIONCODE;
                    txtACCOUNT.Text = objTBLLOCATION.ACCOUNT;
                    //drpMAXDISCOUNT.SelectedValue = objTBLLOCATION.MAXDISCOUNT.ToString();
                    txtUSERID.Text = objTBLLOCATION.USERID;
                    txtENTRYDATE.Text = objTBLLOCATION.ENTRYDATE.ToShortDateString();
                    txtENTRYTIME.Text = objTBLLOCATION.ENTRYTIME.ToShortDateString();
                    txtUPDTTIME.Text = objTBLLOCATION.UPDTTIME.ToShortDateString();
                    if (objTBLLOCATION.Active == "Y")
                    {
                        cbActive.Checked = true;
                    }
                    else
                    {
                        cbActive.Checked = false;
                    }
                    txtLOCNAME.Text = objTBLLOCATION.LOCNAME;
                    txtLOCNAMEO.Text = objTBLLOCATION.LOCNAMEO;
                    txtLOCNAMECH.Text = objTBLLOCATION.LOCNAMECH;
                    //drpCRUP_ID.SelectedValue = objTBLLOCATION.CRUP_ID.ToString();

                    btnAdd.Text = "Update";
                    ViewState["Edit"] = ID;
                    ViewState["TIDD"] = TIDD;
                    ViewState["LID"] = LID;
                    Write();
                    BindData();
                }
                scope.Complete(); //  To commit.
            }
            //    catch (Exception ex)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //        throw;
            //    }
            //}
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLLOCATIONs.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLLOCATIONs.OrderBy(m => m.LOCATIONID).Take(Tvalue).Skip(Svalue)).ToList());
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