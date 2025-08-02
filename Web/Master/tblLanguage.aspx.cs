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
    
    public partial class tblLanguage : System.Web.UI.Page
    {
        int TTID = 0;
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
                binddrop();
            }
        }
        #region Step2
        public void BindData()
        {
            Listview1.DataSource = DB.tblLanguages.Where(p => p.ACTIVE == "Y" && p.TenentID == TID).OrderBy(m => m.MYCONLOCID).OrderByDescending(m=>m.MYCONLOCID);
            Listview1.DataBind();
            //List<Database.tblLanguage> List = DB.tblLanguages.Where(p=>p.ACTIVE == "Y" && p.TenentID == TID).OrderBy(m => m.MYCONLOCID).ToList();
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
        public void binddrop()
        {
            drpCOUNTRYID.DataSource = DB.tblCOUNTRies.Where(p=>p.TenentID == TTID && p.Active == "Y");
            drpCOUNTRYID.DataTextField = "COUNAME1";
            drpCOUNTRYID.DataValueField = "COUNTRYID";
            drpCOUNTRYID.DataBind();
            drpCOUNTRYID.Items.Insert(0, new ListItem("-- Select Country --", "0"));
        }
        public void GetShow()
        {

            lblCOUNTRYID1s.Attributes["class"] = lblLangName11s.Attributes["class"] = lblLangName21s.Attributes["class"] = lblLangName31s.Attributes["class"] = lblCULTUREOCDE1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblCOUNTRYID2h.Attributes["class"] = lblLangName12h.Attributes["class"] = lblLangName22h.Attributes["class"] = lblLangName32h.Attributes["class"] = lblCULTUREOCDE2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCOUNTRYID1s.Attributes["class"] = lblLangName11s.Attributes["class"] = lblLangName21s.Attributes["class"] = lblLangName31s.Attributes["class"] = lblCULTUREOCDE1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblCOUNTRYID2h.Attributes["class"] = lblLangName12h.Attributes["class"] = lblLangName22h.Attributes["class"] = lblLangName32h.Attributes["class"] = lblCULTUREOCDE2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpMYCONLOCID.SelectedIndex = 0;
            drpCOUNTRYID.SelectedIndex = 0;
            txtLangName1.Text = "";
            txtLangName2.Text = "";
            txtLangName3.Text = "";
            txtCULTUREOCDE.Text = "";
            //txtACTIVE.Text = "";
            txtREMARKS.Text = "";
            //txtCRUP_ID.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.tblLanguage objtblLanguage = new Database.tblLanguage();
                        //Server Content Send data Yogesh                        
                        objtblLanguage.TenentID = TID;                        
                        objtblLanguage.MYCONLOCID = DB.tblLanguages.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblLanguages.Where(p => p.TenentID == TID).Max(p => p.MYCONLOCID) + 1) : 1;
                        objtblLanguage.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                        objtblLanguage.LangName1 = txtLangName1.Text;
                        objtblLanguage.LangName2 = txtLangName2.Text;
                        objtblLanguage.LangName3 = txtLangName3.Text;
                        objtblLanguage.CULTUREOCDE = txtCULTUREOCDE.Text;
                        objtblLanguage.ACTIVE = "Y";
                        //objtblLanguage.ACTIVE = txtACTIVE.Text;
                        objtblLanguage.REMARKS = txtREMARKS.Text;
                        objtblLanguage.UploadDate = DateTime.Now;
                        objtblLanguage.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                        objtblLanguage.SynID = 1;

                        //objtblLanguage.CRUP_ID = txtCRUP_ID.Text;


                        DB.tblLanguages.AddObject(objtblLanguage);
                        DB.SaveChanges();

                        Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                        btnAdd.Text = "AddNew";
                        Clear();                        
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["TID"] != null && ViewState["MYconlocID"] != null)
                        {                            
                            int TTID = Convert.ToInt32(ViewState["TID"]);
                            int myconlocid = Convert.ToInt32(ViewState["MYconlocID"]);
                            Database.tblLanguage objtblLanguage = DB.tblLanguages.Single(p => p.TenentID == TTID && p.MYCONLOCID == myconlocid);
                            //objtblLanguage.MYCONLOCID = Convert.ToInt32(drpMYCONLOCID.SelectedValue);
                            objtblLanguage.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                            objtblLanguage.LangName1 = txtLangName1.Text;
                            objtblLanguage.LangName2 = txtLangName2.Text;
                            objtblLanguage.LangName3 = txtLangName3.Text;
                            objtblLanguage.CULTUREOCDE = txtCULTUREOCDE.Text;
                            //objtblLanguage.ACTIVE = txtACTIVE.Text;
                            objtblLanguage.REMARKS = txtREMARKS.Text;
                            objtblLanguage.UploadDate = DateTime.Now;
                            objtblLanguage.Uploadby= ((USER_MST)Session["USER"]).FIRST_NAME;
                            objtblLanguage.SynID = 2;
                            //objtblLanguage.CRUP_ID = txtCRUP_ID.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();

                            Classes.EcommAdminClass.update_SubcriptionSetup(TID);
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();
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
            //drpMYCONLOCID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtLangName1.Text = Listview1.SelectedDataKey[0].ToString();
            txtLangName2.Text = Listview1.SelectedDataKey[0].ToString();
            txtLangName3.Text = Listview1.SelectedDataKey[0].ToString();
            txtCULTUREOCDE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpMYCONLOCID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtLangName1.Text = Listview1.SelectedDataKey[0].ToString();
                txtLangName2.Text = Listview1.SelectedDataKey[0].ToString();
                txtLangName3.Text = Listview1.SelectedDataKey[0].ToString();
                txtCULTUREOCDE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
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
                //drpMYCONLOCID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtLangName1.Text = Listview1.SelectedDataKey[0].ToString();
                txtLangName2.Text = Listview1.SelectedDataKey[0].ToString();
                txtLangName3.Text = Listview1.SelectedDataKey[0].ToString();
                txtCULTUREOCDE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpMYCONLOCID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtLangName1.Text = Listview1.SelectedDataKey[0].ToString();
            txtLangName2.Text = Listview1.SelectedDataKey[0].ToString();
            txtLangName3.Text = Listview1.SelectedDataKey[0].ToString();
            txtCULTUREOCDE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblCOUNTRYID2h.Visible = lblLangName12h.Visible = lblLangName22h.Visible = lblLangName32h.Visible = lblCULTUREOCDE2h.Visible = lblACTIVE2h.Visible = lblREMARKS2h.Visible = lblCRUP_ID2h.Visible = false;
                    //2true
                    txtCOUNTRYID2h.Visible = txtLangName12h.Visible = txtLangName22h.Visible = txtLangName32h.Visible = txtCULTUREOCDE2h.Visible = txtACTIVE2h.Visible = txtREMARKS2h.Visible = txtCRUP_ID2h.Visible = true;

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
                    lblCOUNTRYID2h.Visible = lblLangName12h.Visible = lblLangName22h.Visible = lblLangName32h.Visible = lblCULTUREOCDE2h.Visible = lblACTIVE2h.Visible = lblREMARKS2h.Visible = lblCRUP_ID2h.Visible = true;
                    //2false
                    txtCOUNTRYID2h.Visible = txtLangName12h.Visible = txtLangName22h.Visible = txtLangName32h.Visible = txtCULTUREOCDE2h.Visible = txtACTIVE2h.Visible = txtREMARKS2h.Visible = txtCRUP_ID2h.Visible = false;

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
                    lblCOUNTRYID1s.Visible = lblLangName11s.Visible = lblLangName21s.Visible = lblLangName31s.Visible = lblCULTUREOCDE1s.Visible = lblREMARKS1s.Visible = false;
                    //1true
                    txtCOUNTRYID1s.Visible = txtLangName11s.Visible = txtLangName21s.Visible = txtLangName31s.Visible = txtCULTUREOCDE1s.Visible = txtREMARKS1s.Visible = true;
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
                    lblCOUNTRYID1s.Visible = lblLangName11s.Visible = lblLangName21s.Visible = lblLangName31s.Visible = lblCULTUREOCDE1s.Visible = lblREMARKS1s.Visible = true;
                    //1false
                    txtCOUNTRYID1s.Visible = txtLangName11s.Visible = txtLangName21s.Visible = txtLangName31s.Visible = txtCULTUREOCDE1s.Visible = txtREMARKS1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblLanguage").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = item.LabelName;
                else if (lblLangName11s.ID == item.LabelID)
                    txtLangName11s.Text = lblLangName11s.Text = lblhLangName1.Text = item.LabelName;
                else if (lblLangName21s.ID == item.LabelID)
                    txtLangName21s.Text = lblLangName21s.Text = lblhLangName2.Text = item.LabelName;
                else if (lblLangName31s.ID == item.LabelID)
                    txtLangName31s.Text = lblLangName31s.Text = lblhLangName3.Text = item.LabelName;
                else if (lblCULTUREOCDE1s.ID == item.LabelID)
                    txtCULTUREOCDE1s.Text = lblCULTUREOCDE1s.Text = item.LabelName;
                //else if (lblACTIVE1s.ID == item.LabelID)
                //    txtACTIVE1s.Text = lblACTIVE1s.Text = lblhACTIVE.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;

                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = item.LabelName;
                else if (lblLangName12h.ID == item.LabelID)
                    txtLangName12h.Text = lblLangName12h.Text = lblhLangName1.Text = item.LabelName;
                else if (lblLangName22h.ID == item.LabelID)
                    txtLangName22h.Text = lblLangName22h.Text = lblhLangName2.Text = item.LabelName;
                else if (lblLangName32h.ID == item.LabelID)
                    txtLangName32h.Text = lblLangName32h.Text = lblhLangName3.Text = item.LabelName;
                else if (lblCULTUREOCDE2h.ID == item.LabelID)
                    txtCULTUREOCDE2h.Text = lblCULTUREOCDE2h.Text = item.LabelName;
                //else if (lblACTIVE2h.ID == item.LabelID)
                //    txtACTIVE2h.Text = lblACTIVE2h.Text = lblhACTIVE.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblLanguage").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblLanguage.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblLanguage").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                else if (lblLangName11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLangName11s.Text;
                else if (lblLangName21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLangName21s.Text;
                else if (lblLangName31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLangName31s.Text;
                else if (lblCULTUREOCDE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCULTUREOCDE1s.Text;
                //else if (lblACTIVE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE1s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                else if (lblLangName12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLangName12h.Text;
                else if (lblLangName22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLangName22h.Text;
                else if (lblLangName32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLangName32h.Text;
                else if (lblCULTUREOCDE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCULTUREOCDE2h.Text;
                //else if (lblACTIVE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE2h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblLanguage.xml"));

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
            //drpMYCONLOCID.Enabled = true;
            drpCOUNTRYID.Enabled = true;
            txtLangName1.Enabled = true;
            txtLangName2.Enabled = true;
            txtLangName3.Enabled = true;
            txtCULTUREOCDE.Enabled = true;
            //txtACTIVE.Enabled = true;
            txtREMARKS.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpMYCONLOCID.Enabled = false;
            drpCOUNTRYID.Enabled = false;
            txtLangName1.Enabled = false;
            txtLangName2.Enabled = false;
            txtLangName3.Enabled = false;
            txtCULTUREOCDE.Enabled = false;
            //txtACTIVE.Enabled = false;
            txtREMARKS.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblLanguages.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblLanguages.OrderBy(m => m.MYCONLOCID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tblLanguages.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblLanguages.OrderBy(m => m.MYCONLOCID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tblLanguages.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblLanguages.OrderBy(m => m.MYCONLOCID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tblLanguages.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblLanguages.OrderBy(m => m.MYCONLOCID).Take(take).Skip(Skip)).ToList());
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
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {

                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TID = Convert.ToInt32(ID[0]);
                        int MYconlocID = Convert.ToInt32(ID[1]);

                        Database.tblLanguage objtblLanguage = DB.tblLanguages.Single(p => p.TenentID == TID && p.MYCONLOCID == MYconlocID);
                        objtblLanguage.ACTIVE = "N";
                        objtblLanguage.UploadDate = DateTime.Now;
                        objtblLanguage.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                        objtblLanguage.SynID = 2;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblLanguages.OrderBy(m => m.MYCONLOCID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TID = Convert.ToInt32(ID[0]);
                        int MYconlocID = Convert.ToInt32(ID[1]);

                        Database.tblLanguage objtblLanguage = DB.tblLanguages.Single(p => p.TenentID == TID && p.MYCONLOCID == MYconlocID);
                        //drpMYCONLOCID.SelectedValue = objtblLanguage.MYCONLOCID.ToString();
                        drpCOUNTRYID.SelectedValue = objtblLanguage.COUNTRYID.ToString();
                        txtLangName1.Text = objtblLanguage.LangName1.ToString();
                        txtLangName2.Text = objtblLanguage.LangName2.ToString();
                        txtLangName3.Text = objtblLanguage.LangName3.ToString();
                        txtCULTUREOCDE.Text = objtblLanguage.CULTUREOCDE.ToString();
                        //txtACTIVE.Text = objtblLanguage.ACTIVE.ToString();
                        txtREMARKS.Text = objtblLanguage.REMARKS.ToString();
                        //txtCRUP_ID.Text = objtblLanguage.CRUP_ID.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["TID"] = TID;
                        ViewState["MYconlocID"] = MYconlocID;
                        Write();
                    }
                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                    throw;
                }
            }
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblLanguages.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblLanguages.OrderBy(m => m.MYCONLOCID).Take(Tvalue).Skip(Svalue)).ToList());
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