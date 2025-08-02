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
    public partial class ICCATEGORY : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2
        public void BindData()
        {

            List<Database.ICCATEGORY> List = DB.ICCATEGORies.Where(p => p.TenentID == TID).OrderBy(m => m.CATNAME).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lblSHORTNAME1s.Attributes["class"] = lblCATNAME1s.Attributes["class"] = lblCATNAMEO1s.Attributes["class"] = lblCATNAMECH1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblSHORTNAME2h.Attributes["class"] = lblCATNAME2h.Attributes["class"] = lblCATNAMEO2h.Attributes["class"] = lblCATNAMECH2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblSHORTNAME1s.Attributes["class"] = lblCATNAME1s.Attributes["class"] = lblCATNAMEO1s.Attributes["class"] = lblCATNAMECH1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblSHORTNAME2h.Attributes["class"] = lblCATNAME2h.Attributes["class"] = lblCATNAMEO2h.Attributes["class"] = lblCATNAMECH2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  getshow";
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
           
            txtSHORTNAME.Text = "";
            txtCATNAME.Text = "";
            txtCATNAMEO.Text = "";
            txtCATNAMECH.Text = "";
            txtREMARKS.Text = "";
          

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "Add New")
                    {
                        Write();
                        Clear();
                        btnAdd.Text = "Save";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Save")
                    {
                        Database.ICCATEGORY objICCATEGORY = new Database.ICCATEGORY();
                        objICCATEGORY.TenentID = TID;
                        objICCATEGORY.COMPANYID = 2;
                        objICCATEGORY.CATID = DB.ICCATEGORies.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICCATEGORies.Where(p => p.TenentID == TID).Max(p => p.CATID) + 1) : 1;
                        objICCATEGORY.CATTYPE = "HelpDesk";
                        objICCATEGORY.SHORTNAME = txtSHORTNAME.Text;
                        objICCATEGORY.CATNAME = txtCATNAME.Text;
                        objICCATEGORY.CATNAMEO = txtCATNAMEO.Text;
                        objICCATEGORY.CATNAMECH = txtCATNAMECH.Text;
                        objICCATEGORY.REMARKS = txtREMARKS.Text;
                        objICCATEGORY.SORTORDER = DB.ICCATEGORies.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICCATEGORies.Where(p => p.TenentID == TID).Max(p => p.SORTORDER) + 1) : 1;
                        objICCATEGORY.Status = 1;
                        objICCATEGORY.CRUP_ID = 0;

                        String url = "insert new record in ICCATEGORY with " + "TenentID = " + TID + "COMPANYID = 2" + "CATID = " + objICCATEGORY.CATID;
                        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "CategoryAdd").REFNAME1;
                        String tablename = "ICCATEGORY";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "CategoryAdd").REFID;
                        objICCATEGORY.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno); ;
               


                        DB.ICCATEGORies.AddObject(objICCATEGORY);
                        DB.SaveChanges();

                        Clear();
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        lblMsg.Text = "Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();                       
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.ICCATEGORY objICCATEGORY = DB.ICCATEGORies.Single(p => p.CATID == ID);
                            objICCATEGORY.SHORTNAME = txtSHORTNAME.Text;
                            objICCATEGORY.CATNAME = txtCATNAME.Text;
                            objICCATEGORY.CATNAMEO = txtCATNAMEO.Text;
                            objICCATEGORY.CATNAMECH = txtCATNAMECH.Text;
                            objICCATEGORY.REMARKS = txtREMARKS.Text;

                            String url = "update ICCATEGORY with " + "TenentID = " + TID + "COMPANYID = 2" + "CATID = " + ID;
                            String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "CategoryAdd").REFNAME1;
                            String tablename = "ICCATEGORY";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                            int crupID = Convert.ToInt32(objICCATEGORY.CRUP_ID);

                            Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "ss";
                            DB.SaveChanges();

                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
                catch 
                {
                   
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            Response.Redirect("ICCATEGORY.aspx");
        }
        public void FillContractorID()
        {
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            //FirstData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //NextData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //PrevData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            //LastData();
        }
        //public void FirstData()
        //{
        //    int index = Convert.ToInt32(ViewState["Index"]);
        //    Listview1.SelectedIndex = 0;
        //    drpCOMPANYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCATNAMECH.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpSORTORDER.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpStatus.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        //}
        //public void NextData()
        //{

        //    if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
        //    {
        //        Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
        //        drpCOMPANYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCATNAMECH.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpSORTORDER.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpStatus.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        //    }

        //}
        //public void PrevData()
        //{
        //    if (Listview1.SelectedIndex == 0)
        //    {
        //        lblMsg.Text = "This is first record";
        //        pnlSuccessMsg.Visible = true;

        //    }
        //    else
        //    {
        //        pnlSuccessMsg.Visible = false;
        //        Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
        //        drpCOMPANYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCATNAMECH.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpSORTORDER.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpStatus.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        //    }
        //}
        //public void LastData()
        //{
        //    Listview1.SelectedIndex = Listview1.Items.Count - 1;
        //    drpCOMPANYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCATNAMECH.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpSORTORDER.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpStatus.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        //}


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblSHORTNAME2h.Visible = lblCATNAME2h.Visible = lblCATNAMEO2h.Visible = lblCATNAMECH2h.Visible = lblREMARKS2h.Visible = false;
                    //2true
                    txtSHORTNAME2h.Visible = txtCATNAME2h.Visible = txtCATNAMEO2h.Visible = txtCATNAMECH2h.Visible = txtREMARKS2h.Visible = true;

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
                    lblSHORTNAME2h.Visible = lblCATNAME2h.Visible = lblCATNAMEO2h.Visible = lblCATNAMECH2h.Visible = lblREMARKS2h.Visible = true;
                    //2false
                    txtSHORTNAME2h.Visible = txtCATNAME2h.Visible = txtCATNAMEO2h.Visible = txtCATNAMECH2h.Visible = txtREMARKS2h.Visible = false;

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
                    lblSHORTNAME1s.Visible = lblCATNAME1s.Visible = lblCATNAMEO1s.Visible = lblCATNAMECH1s.Visible = lblREMARKS1s.Visible = false;
                    //1true
                    txtSHORTNAME1s.Visible = txtCATNAME1s.Visible = txtCATNAMEO1s.Visible = txtCATNAMECH1s.Visible = txtREMARKS1s.Visible = true;
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
                    lblSHORTNAME1s.Visible = lblCATNAME1s.Visible = lblCATNAMEO1s.Visible = lblCATNAMECH1s.Visible = lblREMARKS1s.Visible = true;
                    //1false
                    txtSHORTNAME1s.Visible = txtCATNAME1s.Visible = txtCATNAMEO1s.Visible = txtCATNAMECH1s.Visible = txtREMARKS1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICCATEGORY").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblSHORTNAME1s.ID == item.LabelID)
                    txtSHORTNAME1s.Text = lblSHORTNAME1s.Text = item.LabelName;
                else if (lblCATNAME1s.ID == item.LabelID)
                    txtCATNAME1s.Text = lblCATNAME1s.Text = lblhCATNAME.Text = item.LabelName;
                else if (lblCATNAMEO1s.ID == item.LabelID)
                    txtCATNAMEO1s.Text = lblCATNAMEO1s.Text = lblhCATNAMEO.Text = item.LabelName;
                else if (lblCATNAMECH1s.ID == item.LabelID)
                    txtCATNAMECH1s.Text = lblCATNAMECH1s.Text = lblhCATNAMECH.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;

                else if (lblSHORTNAME2h.ID == item.LabelID)
                    txtSHORTNAME2h.Text = lblSHORTNAME2h.Text = item.LabelName;
                else if (lblCATNAME2h.ID == item.LabelID)
                    txtCATNAME2h.Text = lblCATNAME2h.Text = lblhCATNAME.Text = item.LabelName;
                else if (lblCATNAMEO2h.ID == item.LabelID)
                    txtCATNAMEO2h.Text = lblCATNAMEO2h.Text = lblhCATNAMEO.Text = item.LabelName;
                else if (lblCATNAMECH2h.ID == item.LabelID)
                    txtCATNAMECH2h.Text = lblCATNAMECH2h.Text = lblhCATNAMECH.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICCATEGORY").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\ICCATEGORY.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("ICCATEGORY").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblSHORTNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSHORTNAME1s.Text;
                else if (lblCATNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATNAME1s.Text;
                else if (lblCATNAMEO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATNAMEO1s.Text;
                else if (lblCATNAMECH1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATNAMECH1s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;

                else if (lblSHORTNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSHORTNAME2h.Text;
                else if (lblCATNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATNAME2h.Text;
                else if (lblCATNAMEO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATNAMEO2h.Text;
                else if (lblCATNAMECH2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCATNAMECH2h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\ICCATEGORY.xml"));

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
            txtSHORTNAME.Enabled = true;
            txtCATNAME.Enabled = true;
            txtCATNAMEO.Enabled = true;
            txtCATNAMECH.Enabled = true;
            txtREMARKS.Enabled = true;
        }
        public void Readonly()
        {
            txtSHORTNAME.Enabled = false;
            txtCATNAME.Enabled = false;
            txtCATNAMEO.Enabled = false;
            txtCATNAMECH.Enabled = false;
            txtREMARKS.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICCATEGORY.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((HRMMater)Page.Master).BindList(Listview1, (DB.ICCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
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

        //    ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.ICCATEGORY.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((HRMMater)Page.Master).BindList(Listview1, (DB.ICCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
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
        //        ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.ICCATEGORY.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((HRMMater)Page.Master).BindList(Listview1, (DB.ICCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
        //    int Totalrec = DB.ICCATEGORY.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((HRMMater)Page.Master).BindList(Listview1, (DB.ICCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((HRMMater)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
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
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {

                        //Database.ICCATEGORY objSOJobDesc = DB.ICCATEGORies.Single(p => p.JobId == str1 && p.SubDomainId == str2);
                        //objSOJobDesc.Active = false;
                        //DB.SaveChanges();
                        //BindData();
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.ICCATEGORY objICCATEGORY = DB.ICCATEGORies.Single(p => p.CATID == ID);
                        txtSHORTNAME.Text = objICCATEGORY.SHORTNAME.ToString();
                        txtCATNAME.Text = objICCATEGORY.CATNAME.ToString();
                        txtCATNAMEO.Text = objICCATEGORY.CATNAMEO.ToString();
                        txtCATNAMECH.Text = objICCATEGORY.CATNAMECH.ToString();
                        txtREMARKS.Text = objICCATEGORY.REMARKS.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        Write();
                        btnAdd.ValidationGroup = "submit";
                    }
                    scope.Complete(); //  To commit.
                }
                catch 
                {
                   
                }
            }
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICCATEGORY.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((HRMMater)Page.Master).BindList(Listview1, (DB.ICCATEGORY.OrderBy(m => m.JobId).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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

    }
}