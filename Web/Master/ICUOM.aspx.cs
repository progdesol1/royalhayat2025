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
using System.Net;
using System.IO;

namespace Web.Master
{
    public partial class ICUOM : System.Web.UI.Page
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
                BindConvList();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "s";
                pnlConversion.Visible = false;
            }

        }
        #region Step2
        public void BindData()
        {
            //List<Database.ICUOM> List = DB.ICUOMs.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(m => m.UOM).OrderByDescending(p=>p.UOM);
            Listview1.DataSource = DB.ICUOMs.Where(p => p.TenentID == TID && p.Active == "Y").OrderBy(m => m.UOM).OrderByDescending(p => p.UOM);
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

            lblUOMNAMESHORT1s.Attributes["class"] = lblUOMNAME11s.Attributes["class"] = lblUOMNAME21s.Attributes["class"] = lblUOMNAME31s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  getshow"; //lblActive1s.Attributes["class"] = 
            lblUOMNAMESHORT2h.Attributes["class"] = lblUOMNAME12h.Attributes["class"] = lblUOMNAME22h.Attributes["class"] = lblUOMNAME32h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  gethide"; //lblActive2h.Attributes["class"] = 
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblUOMNAMESHORT1s.Attributes["class"] = lblUOMNAME11s.Attributes["class"] = lblUOMNAME21s.Attributes["class"] = lblUOMNAME31s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  gethide"; // lblActive1s.Attributes["class"] = 
            lblUOMNAMESHORT2h.Attributes["class"] = lblUOMNAME12h.Attributes["class"] = lblUOMNAME22h.Attributes["class"] = lblUOMNAME32h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  getshow"; //lblActive2h.Attributes["class"] = 
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
            //drpUOM.SelectedIndex = 0;
            txtUOMNAMESHORT.Text = "";
            txtUOMNAME1.Text = "";
            txtUOMNAME2.Text = "";
            txtUOMNAME3.Text = "";
            txtREMARKS.Text = "";
            //drpCRUP_ID.SelectedIndex = 0;
            //cbActive.Checked = true;
            //txtUOMNAME.Text = "";
            //txtUOMNAMEO.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            rt();
            if (btnAdd.Text == "Add New")
            {

                Write();
                Clear();
                btnAdd.Text = "Save";
                btnAdd.ValidationGroup = "submit";

            }
            else if (btnAdd.Text == "Save")
            {
                if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOMNAME1.ToUpper() == txtUOMNAME1.Text.ToUpper()).Count() < 1)
                {
                    int MDUID = 0;
                    if (Session["SiteModuleID"] != null)
                    {
                        MDUID = Convert.ToInt32(Session["SiteModuleID"]);
                    }
                    Database.ICUOM objICUOM = new Database.ICUOM();
                    //Server Content Send data Yogesh
                    //int tenant = Convert.ToInt32(objICUOM.TenantID);

                    objICUOM.TenentID = TID;
                    objICUOM.UOM = DB.ICUOMs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICUOMs.Where(p => p.TenentID == TID).Max(p => p.UOM) + 1) : 1;
                    objICUOM.UOMNAMESHORT = txtUOMNAMESHORT.Text;
                    objICUOM.UOMNAME1 = txtUOMNAME1.Text;//Translate(txtSupplierName.Text, "ar");
                    objICUOM.UOMNAME2 = Translate(txtUOMNAME2.Text, "ar");
                    objICUOM.UOMNAME3 = Translate(txtUOMNAME3.Text, "fr");
                    objICUOM.REMARKS = txtREMARKS.Text;
                    string VL = RdoAlloMulti.SelectedValue.ToString();
                    objICUOM.MultiUOMAllow = VL == "1" ? true : false;
                    objICUOM.CalculateAspectRatio = RdoAllowRatio.SelectedValue == "1" ? true : false;
                    //objICUOM.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                    objICUOM.Active = "Y";
                    objICUOM.UploadDate = DateTime.Now;
                    objICUOM.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objICUOM.SynID = 1;

                    DB.ICUOMs.AddObject(objICUOM);
                    DB.SaveChanges();

                    Classes.EcommAdminClass.update_SubcriptionSetup(TID);


                    Clear();
                    //lblMsg.Text = "  Data Save Successfully";
                    //pnlSuccessMsg.Visible = true;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "s";
                    BindData();
                    BindConvList();
                    //navigation.Visible = true;
                    Readonly();
                    //FirstData();
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                }
                else
                {
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "'Unit Of Measure Is All Ready Exist...'", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                }
            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["tanant"] != null && ViewState["uom"] != null)
                {
                    //ViewState["tanant"] = tanant;
                    //ViewState["uom"] = uom;
                    int tanant1 = Convert.ToInt32(ViewState["tanant"]);
                    int uom1 = Convert.ToInt32(ViewState["uom"]);
                    //int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.ICUOM objICUOM = DB.ICUOMs.Single(p => p.UOM == uom1 && p.TenentID == tanant1);
                    // objICUOM.UOM = Convert.ToInt32(drpUOM.SelectedValue);
                    objICUOM.UOMNAMESHORT = txtUOMNAMESHORT.Text;
                    objICUOM.UOMNAME1 = txtUOMNAME1.Text;
                    objICUOM.UOMNAME2 = txtUOMNAME2.Text;
                    objICUOM.UOMNAME3 = txtUOMNAME3.Text;
                    objICUOM.REMARKS = txtREMARKS.Text;
                    string VL = RdoAlloMulti.SelectedValue.ToString();
                    objICUOM.MultiUOMAllow = VL == "1" ? true : false;
                    objICUOM.CalculateAspectRatio = RdoAllowRatio.SelectedValue == "1" ? true : false;
                    //objICUOM.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                    //objICUOM.Active = cbActive.Checked ? "Y" : "N";
                    objICUOM.UploadDate = DateTime.Now;
                    objICUOM.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objICUOM.SynID = 2;
                    //objICUOM.UOMNAME = txtUOMNAME.Text;
                    //objICUOM.UOMNAMEO = txtUOMNAMEO.Text;

                    ViewState["tanant"] = null;
                    ViewState["uom"] = null;
                    //ViewState["Edit"] = null;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "s";
                    DB.SaveChanges();

                    Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                    Clear();
                    //lblMsg.Text = "  Data Edit Successfully";
                    //pnlSuccessMsg.Visible = true;
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    BindData();
                    BindConvList();
                    //navigation.Visible = true;
                    Readonly();
                    //FirstData();
                }
            }

            //        scope.Complete(); //  To commit.

            //    }
            //    catch (Exception ex)
            //    {
            //        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Saving Data !<br>" + ex.ToString(), "Add", Classes.Toastr.ToastPosition.TopCenter);
            //        //throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ICUOM.aspx");
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
        public void ManageData()
        {
            txtUOMNAMESHORT.Text = Listview1.SelectedDataKey[2].ToString();
            txtUOMNAME1.Text = Listview1.SelectedDataKey[3].ToString();
            txtUOMNAME2.Text = Listview1.SelectedDataKey[4].ToString();
            txtUOMNAME3.Text = Listview1.SelectedDataKey[5].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[6].ToString();

        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            //drpUOM.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            ManageData();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpUOM.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                ManageData();
            }

        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                //lblMsg.Text = "This is first record";
                // pnlSuccessMsg.Visible = true;
            }
            else
            {
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                //drpUOM.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                ManageData();
            }
        }
        public void LastData()
        {

            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpUOM.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            ManageData();
        }
        #endregion

        #region PAge Genarator Language
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblUOMNAMESHORT2h.Visible = lblUOMNAME12h.Visible = lblUOMNAME22h.Visible = lblUOMNAME32h.Visible = lblREMARKS2h.Visible = false; //lblActive2h.Visible = 
                    //2true
                    txtUOMNAMESHORT2h.Visible = txtUOMNAME12h.Visible = txtUOMNAME22h.Visible = txtUOMNAME32h.Visible = txtREMARKS2h.Visible = true; //txtActive2h.Visible =

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
                    lblUOMNAMESHORT2h.Visible = lblUOMNAME12h.Visible = lblUOMNAME22h.Visible = lblUOMNAME32h.Visible = lblREMARKS2h.Visible = true; //lblActive2h.Visible = 
                    //2false
                    txtUOMNAMESHORT2h.Visible = txtUOMNAME12h.Visible = txtUOMNAME22h.Visible = txtUOMNAME32h.Visible = txtREMARKS2h.Visible = false; //txtActive2h.Visible = 

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
                    lblUOMNAMESHORT1s.Visible = lblUOMNAME11s.Visible = lblUOMNAME21s.Visible = lblUOMNAME31s.Visible = lblREMARKS1s.Visible = false; //lblActive1s.Visible = 
                    //1true
                    txtUOMNAMESHORT1s.Visible = txtUOMNAME11s.Visible = txtUOMNAME21s.Visible = txtUOMNAME31s.Visible = txtREMARKS1s.Visible = true; //txtActive1s.Visible = 
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
                    lblUOMNAMESHORT1s.Visible = lblUOMNAME11s.Visible = lblUOMNAME21s.Visible = lblUOMNAME31s.Visible = lblREMARKS1s.Visible = true; //lblActive1s.Visible = 
                    //1false
                    txtUOMNAMESHORT1s.Visible = txtUOMNAME11s.Visible = txtUOMNAME21s.Visible = txtUOMNAME31s.Visible = txtREMARKS1s.Visible = false; //txtActive1s.Visible = 
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICUOM").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenantID1s.ID == item.LabelID)
                //    txtTenantID1s.Text = lblTenantID1s.Text = item.LabelName;
                //else if (lblUOM1s.ID == item.LabelID)
                //    txtUOM1s.Text = lblUOM1s.Text = item.LabelName;
                if (lblUOMNAMESHORT1s.ID == item.LabelID)
                    txtUOMNAMESHORT1s.Text = lblUOMNAMESHORT1s.Text = item.LabelName;
                else if (lblUOMNAME11s.ID == item.LabelID)
                    txtUOMNAME11s.Text = lblUOMNAME11s.Text = lblhUOMNAME1.Text = item.LabelName;
                else if (lblUOMNAME21s.ID == item.LabelID)
                    txtUOMNAME21s.Text = lblUOMNAME21s.Text = lblhUOMNAME2.Text = item.LabelName;
                else if (lblUOMNAME31s.ID == item.LabelID)
                    txtUOMNAME31s.Text = lblUOMNAME31s.Text = lblhUOMNAME3.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;

                //else if (lblActive1s.ID == item.LabelID)
                //    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblUOMNAME1s.ID == item.LabelID)
                //    txtUOMNAME1s.Text = lblUOMNAME1s.Text = item.LabelName;
                //else if (lblUOMNAMEO1s.ID == item.LabelID)
                //    txtUOMNAMEO1s.Text = lblUOMNAMEO1s.Text = item.LabelName;

                //else if (lblTenantID2h.ID == item.LabelID)
                //    txtTenantID2h.Text = lblTenantID2h.Text = item.LabelName;
                //else if (lblUOM2h.ID == item.LabelID)
                //    txtUOM2h.Text = lblUOM2h.Text = item.LabelName;
                else if (lblUOMNAMESHORT2h.ID == item.LabelID)
                    txtUOMNAMESHORT2h.Text = lblUOMNAMESHORT2h.Text = item.LabelName;
                else if (lblUOMNAME12h.ID == item.LabelID)
                    txtUOMNAME12h.Text = lblUOMNAME12h.Text = lblhUOMNAME1.Text = item.LabelName;
                else if (lblUOMNAME22h.ID == item.LabelID)
                    txtUOMNAME22h.Text = lblUOMNAME22h.Text = lblhUOMNAME2.Text = item.LabelName;
                else if (lblUOMNAME32h.ID == item.LabelID)
                    txtUOMNAME32h.Text = lblUOMNAME32h.Text = lblhUOMNAME3.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = item.LabelName;

                //else if (lblActive2h.ID == item.LabelID)
                //    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblUOMNAME2h.ID == item.LabelID)
                //    txtUOMNAME2h.Text = lblUOMNAME2h.Text = item.LabelName;
                //else if (lblUOMNAMEO2h.ID == item.LabelID)
                //    txtUOMNAMEO2h.Text = lblUOMNAMEO2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICUOM").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\ICUOM.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("ICUOM").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenantID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID1s.Text;
                //else if (lblUOM1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUOM1s.Text;
                if (lblUOMNAMESHORT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAMESHORT1s.Text;
                else if (lblUOMNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME11s.Text;
                else if (lblUOMNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME21s.Text;
                else if (lblUOMNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME31s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;

                //else if (lblActive1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblUOMNAME1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME1s.Text;
                //else if (lblUOMNAMEO1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAMEO1s.Text;

                //else if (lblTenantID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID2h.Text;
                //else if (lblUOM2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUOM2h.Text;
                else if (lblUOMNAMESHORT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAMESHORT2h.Text;
                else if (lblUOMNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME12h.Text;
                else if (lblUOMNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME22h.Text;
                else if (lblUOMNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME32h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;

                //else if (lblActive2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblUOMNAME2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAME2h.Text;
                //else if (lblUOMNAMEO2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUOMNAMEO2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\ICUOM.xml"));

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
            //drpUOM.Enabled = true;
            txtUOMNAMESHORT.Enabled = true;
            txtUOMNAME1.Enabled = true;
            txtUOMNAME2.Enabled = true;
            txtUOMNAME3.Enabled = true;
            txtREMARKS.Enabled = true;
            //cbActive.Enabled = true;
            //txtUOMNAME.Enabled = true;
            //txtUOMNAMEO.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpUOM.Enabled = false;
            txtUOMNAMESHORT.Enabled = false;
            txtUOMNAME1.Enabled = false;
            txtUOMNAME2.Enabled = false;
            txtUOMNAME3.Enabled = false;
            txtREMARKS.Enabled = false;
            //cbActive.Enabled = false;
            //txtUOMNAME.Enabled = false;
            // txtUOMNAMEO.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICUOMs.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMs.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(m => m.UOM).Take(take).Skip(Skip)).ToList());
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

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.ICUOMs.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMs.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(m => m.UOM).Take(take).Skip(Skip)).ToList());
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
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.ICUOMs.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMs.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(m => m.UOM).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{           
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICUOMs.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMs.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(m => m.UOM).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            Readonly();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            // FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //try
            //{

            if (e.CommandName == "btnDelete")
            {
                rt();

                string[] ID = e.CommandArgument.ToString().Split(',');
                int uom = Convert.ToInt32(ID[0]);
                int tanant = Convert.ToInt32(ID[1]);
                if (DB.ICIT_BR.Where(q => q.TenentID == TID && q.UOM == uom).Count() < 1)
                {
                    Database.ICUOM objICUOM = DB.ICUOMs.Single(p => p.UOM == uom && p.TenentID == tanant);
                    objICUOM.Active = "N";
                    objICUOM.UploadDate = DateTime.Now;
                    objICUOM.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objICUOM.SynID = 3;
                    DB.SaveChanges();
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    BindData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "UOM Conversion!", "alert('This UOM IS In The Stock Used By Product, You Can't Delete This UOM);", true);
                    return;
                }
            }

            if (e.CommandName == "btnEdit")
            {
                rt();
                string[] ID = e.CommandArgument.ToString().Split(',');
                int uom = Convert.ToInt32(ID[0]);
                int tanant = Convert.ToInt32(ID[1]);

                Database.ICUOM objICUOM = DB.ICUOMs.Single(p => p.UOM == uom && p.TenentID == tanant);

                //int ID = Convert.ToInt32(e.CommandArgument);
                //Database.ICUOM objICUOM = DB.ICUOMs.Single(p => p.UOM == ID);
                //drpUOM.SelectedValue = objICUOM.UOM.ToString();
                txtUOMNAMESHORT.Text = objICUOM.UOMNAMESHORT.ToString();
                txtUOMNAME1.Text = objICUOM.UOMNAME1.ToString();
                txtUOMNAME2.Text = objICUOM.UOMNAME2.ToString();
                txtUOMNAME3.Text = objICUOM.UOMNAME3.ToString();
                txtREMARKS.Text = objICUOM.REMARKS.ToString();
                string vl = Convert.ToBoolean(objICUOM.MultiUOMAllow).ToString();
                RdoAlloMulti.SelectedValue = objICUOM.MultiUOMAllow == true ? "1" : "0";
                RdoAllowRatio.SelectedValue = objICUOM.CalculateAspectRatio == true ? "1" : "0";

                btnAdd.Text = "Update";
                ViewState["Edit"] = ID;
                ViewState["tanant"] = tanant;
                ViewState["uom"] = uom;
                Write();
                if (DB.ICIT_BR.Where(q => q.TenentID == TID && q.UOM == uom).Count() > 0)
                {
                    RdoAlloMulti.Enabled = false;
                    RdoAllowRatio.Enabled = false;
                }
            }
            if (e.CommandName == "Conversion")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int uom = Convert.ToInt32(ID[0]);
                int tanant = Convert.ToInt32(ID[1]);
                BindConvList();
                DrpConvFromUOM.SelectedValue = uom.ToString();

                List<Database.ICUOM> ConvList = DB.ICUOMs.Where(p => p.TenentID == TID && (p.UOM != uom && p.UOM != 99999)).ToList();
                List<Database.ICUOM> VonvFinal = new List<Database.ICUOM>();
                foreach (Database.ICUOM item in ConvList)
                {
                    if (DB.ICUOMCONVs.Where(p => p.TenentID == TID && p.TUOM == item.UOM).Count() < 1)
                    {
                        VonvFinal.Add(item);
                    }
                }

                DrpConvToUOM.DataSource = VonvFinal;//DB.ICUOMs.Where(p => p.TenentID == tanant && (p.UOM != uom && p.UOM != 99999));
                DrpConvToUOM.DataTextField = "UOMNAME1";
                DrpConvToUOM.DataValueField = "UOM";
                DrpConvToUOM.DataBind();
                //DrpConvToUOM.Items.Insert(0, new ListItem("-- Select --", "0"));

                pnlConversion.Visible = true;
            }
            if (e.CommandName == "View")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int uom = Convert.ToInt32(ID[0]);
                int tanant = Convert.ToInt32(ID[1]);

                BaseUOM.Text = "Of " + GetUOMName(uom);
                List<Database.ICUOMCONV> ConvList = DB.ICUOMCONVs.Where(p => p.TenentID == tanant && p.FUOM == uom).ToList();
                ListView2.DataSource = ConvList;
                ListView2.DataBind();
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
        public void BindConvList()
        {
            DrpConvFromUOM.DataSource = DB.ICUOMs.Where(p => p.TenentID == TID);
            DrpConvFromUOM.DataTextField = "UOMNAME1";
            DrpConvFromUOM.DataValueField = "UOM";
            DrpConvFromUOM.DataBind();
        }
        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label MultiallowYN = (Label)e.Item.FindControl("MultiallowYN");
            LinkButton LinkConversation = (LinkButton)e.Item.FindControl("LinkConversation");
            bool MultiYN = Convert.ToBoolean(MultiallowYN.Text);
            if (MultiYN == false)
            {
                LinkConversation.Visible = false;
            }

        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{  
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICUOMs.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICUOMs.Where(p => p.TenantID == TID && p.Active == "Y").Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        //    BindData();
        //}
        //protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
        //    ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
        //    control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        //}
        #endregion
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

        protected void txtUOMNAME1_TextChanged(object sender, EventArgs e)
        {
            txtUOMNAME2.Text = Translate(txtUOMNAME1.Text, "ar");
            txtUOMNAME3.Text = Translate(txtUOMNAME1.Text, "fr");
        }

        protected void txtConv_TextChanged(object sender, EventArgs e)
        {
            string Fromuom = DrpConvFromUOM.SelectedItem.ToString();
            string Touom = DrpConvToUOM.SelectedItem.ToString();
            string Conversion = txtConv.Text;
            txtConvRemark.Text = Fromuom + " 1 equal to " + Touom + " " + Conversion;
        }

        protected void btnConvSave_Click(object sender, EventArgs e)
        {
            int Fromuom = Convert.ToInt32(DrpConvFromUOM.SelectedValue);
            int Touom = Convert.ToInt32(DrpConvToUOM.SelectedValue);
            decimal Conversion = Convert.ToDecimal(txtConv.Text);
            decimal ratio = (1 / Conversion);
            ratio = Math.Round(ratio, 6);
            string UIDBY = ((USER_MST)Session["USER"]).LOGIN_ID;

            if (btnConvSave.Text == "Save")
            {
                if (DB.ICUOMCONVs.Where(p => p.TenentID == TID && p.FUOM == Fromuom && p.TUOM == Touom).Count() > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "UOM Conversion!", "alert('UOM Conversion is open either save or cancel Conversion');", true);
                    return;
                }
                else
                {
                    Database.ICUOMCONV icuom = new Database.ICUOMCONV();
                    icuom.TenentID = TID;
                    icuom.FUOM = Fromuom;
                    icuom.TUOM = Touom;
                    icuom.CONVERSION = Convert.ToDouble(Conversion);
                    icuom.ConvRatio = ratio;
                    icuom.REMARKS = txtConvRemark.Text;
                    icuom.CRUP_ID = 0;
                    icuom.USERID = UID.ToString();
                    icuom.ENTRYDATE = DateTime.Now;
                    icuom.ENTRYTIME = DateTime.Now;
                    icuom.UploadDate = DateTime.Now;
                    icuom.Uploadby = UIDBY;
                    DB.ICUOMCONVs.AddObject(icuom);
                    DB.SaveChanges();
                    pnlConversion.Visible = false;
                    txtConv.Text = "";
                    txtConvRemark.Text = "";
                }
            }
            else if (btnConvSave.Text == "Update")
            {
                Database.ICUOMCONV icuomobj = DB.ICUOMCONVs.Single(p => p.TenentID == TID && p.FUOM == Fromuom && p.TUOM == Touom);
                icuomobj.CONVERSION = Convert.ToDouble(Conversion);
                icuomobj.ConvRatio = ratio;
                icuomobj.REMARKS = txtConvRemark.Text;
                DB.SaveChanges();

                DrpConvToUOM.Enabled = true;
                btnConvSave.Text = "Save";
                pnlConversion.Visible = false;
            }
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkConvEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int FUOM = Convert.ToInt32(ID[0]);
                int TUOM = Convert.ToInt32(ID[1]);
                if (DB.ICIT_BR.Where(q => q.TenentID == TID && q.UOM == TUOM).Count() < 1)
                {
                    BindConvList();
                    List<Database.ICUOM> ConvList = DB.ICUOMs.Where(p => p.TenentID == TID && (p.UOM != FUOM && p.UOM != 99999)).ToList();
                    List<Database.ICUOM> VonvFinal = new List<Database.ICUOM>();
                    foreach (Database.ICUOM item in ConvList)
                    {
                        if (DB.ICUOMCONVs.Where(p => p.TenentID == TID && p.TUOM == item.UOM).Count() < 1)
                        {
                            VonvFinal.Add(item);
                        }
                    }
                    DrpConvToUOM.DataSource = VonvFinal;//DB.ICUOMs.Where(p => p.TenentID == TID && (p.UOM != FUOM && p.UOM != 99999));
                    DrpConvToUOM.DataTextField = "UOMNAME1";
                    DrpConvToUOM.DataValueField = "UOM";
                    DrpConvToUOM.DataBind();

                    Database.ICUOMCONV icuomobj = DB.ICUOMCONVs.Single(p => p.TenentID == TID && p.FUOM == FUOM && p.TUOM == TUOM);

                    DrpConvFromUOM.SelectedValue = icuomobj.FUOM.ToString();
                    DrpConvToUOM.SelectedValue = icuomobj.TUOM.ToString();
                    txtConv.Text = icuomobj.CONVERSION.ToString();
                    txtConvRemark.Text = icuomobj.REMARKS.ToString();

                    DrpConvToUOM.Enabled = false;
                    btnConvSave.Text = "Update";
                    pnlConversion.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "UOM Conversion!", "alert('This Conversion UOM IS In The Stock Used By Product, You Can't Edit This Conversion UOM);", true);
                    return;
                }
            }
            if (e.CommandName == "LinkConvDelete")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int FUOM = Convert.ToInt32(ID[0]);
                int TUOM = Convert.ToInt32(ID[1]);
                if (DB.ICIT_BR.Where(q => q.TenentID == TID && q.UOM == TUOM).Count() < 1)
                {

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "UOM Conversion!", "alert('This Conversion UOM IS In The Stock Used By Product, You Can't Delete This Conversion UOM);", true);
                    return;
                }
            }
        }
        public void rt()
        {
            if (pnlConversion.Visible == true)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UOM Conversion!", "alert('UOM Conversion is open either save or cancel Conversion');", true);
                return;
            }
        }
        protected void btnConvCancel_Click(object sender, EventArgs e)
        {
            pnlConversion.Visible = false;
        }
        public string GetUOMName(int ID)
        {
            if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == ID).Count() > 0)
            {
                string name = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == ID).UOMNAME1;
                return name;
            }
            else
            {
                return "Not Found";
            }
        }





    }
}