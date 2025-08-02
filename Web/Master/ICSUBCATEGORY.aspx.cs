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
    public partial class ICSUBCATEGORY : System.Web.UI.Page
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
                btnAdd.ValidationGroup = "ss";

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.ICSUBCATEGORY> List = DB.ICSUBCATEGORies.Where(p=>p.TenentID == TID).OrderBy(m => m.SUBCATNAME).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lblSHORTNAME1s.Attributes["class"] = lblSUBCATNAME1s.Attributes["class"] = lblSUBCATNAMEO1s.Attributes["class"] = lblSUBCATNAMEO21s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblSHORTNAME2h.Attributes["class"] = lblSUBCATNAME2h.Attributes["class"] = lblSUBCATNAMEO2h.Attributes["class"] = lblSUBCATNAMEO22h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblSHORTNAME1s.Attributes["class"] = lblSUBCATNAME1s.Attributes["class"] = lblSUBCATNAMEO1s.Attributes["class"] = lblSUBCATNAMEO21s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblSHORTNAME2h.Attributes["class"] = lblSUBCATNAME2h.Attributes["class"] = lblSUBCATNAMEO2h.Attributes["class"] = lblSUBCATNAMEO22h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtSUBCATNAME.Text = "";
            txtSUBCATNAMEO.Text = "";
            txtSUBCATNAMEO2.Text = "";
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
                        Database.ICSUBCATEGORY objICSUBCATEGORY = new Database.ICSUBCATEGORY();
                        objICSUBCATEGORY.TenentID = TID;
                        objICSUBCATEGORY.COMPANYID = 2;
                        objICSUBCATEGORY.SUBCATID = DB.ICSUBCATEGORies.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICSUBCATEGORies.Where(p => p.TenentID == TID).Max(p => p.SUBCATID) + 1) : 1;
                        objICSUBCATEGORY.SUBCATTYPE = "HelpDesk";
                        objICSUBCATEGORY.SHORTNAME = txtSHORTNAME.Text;
                        objICSUBCATEGORY.SUBCATNAME = txtSUBCATNAME.Text;
                        objICSUBCATEGORY.SUBCATNAMEO = txtSUBCATNAMEO.Text;
                        objICSUBCATEGORY.SUBCATNAMEO2 = txtSUBCATNAMEO2.Text;
                        objICSUBCATEGORY.REMARKS = txtREMARKS.Text;
                        objICSUBCATEGORY.CATID = 0;
                        objICSUBCATEGORY.Status = 1;
                        objICSUBCATEGORY.CRUP_ID = 0;


                        DB.ICSUBCATEGORies.AddObject(objICSUBCATEGORY);
                        DB.SaveChanges();

                        String url = "insert new record in ICSUBCATEGORY with " + "TenentID = " + TID + "COMPANYID = 2" + "SUBCATID = " + objICSUBCATEGORY.SUBCATID;
                        String evantname = "create";
                        String tablename = "ICSUBCATEGORY";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        pnlSuccessMsg.Visible = true;
                        BindData();

                        Readonly();
                        
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.ICSUBCATEGORY objICSUBCATEGORY = DB.ICSUBCATEGORies.Single(p => p.SUBCATID == ID);
                            objICSUBCATEGORY.SHORTNAME = txtSHORTNAME.Text;
                            objICSUBCATEGORY.SUBCATNAME = txtSUBCATNAME.Text;
                            objICSUBCATEGORY.SUBCATNAMEO = txtSUBCATNAMEO.Text;
                            objICSUBCATEGORY.SUBCATNAMEO2 = txtSUBCATNAMEO2.Text;
                            objICSUBCATEGORY.REMARKS = txtREMARKS.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "ss";
                            DB.SaveChanges();

                            String url = "update ICSUBCATEGORY with " + "TenentID = " + TID + "COMPANYID = 2" + "SUBCATID = " + ID;
                            String evantname = "update";
                            String tablename = "ICSUBCATEGORY";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            Readonly();
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
            //Response.Redirect(Session["Previous"].ToString());
            Response.Redirect("ICSUBCATEGORY.aspx");
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
        //    drpSUBCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATNAMEO2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpStatus.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        //}
        //public void NextData()
        //{

        //    if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
        //    {
        //        Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
        //        drpCOMPANYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpSUBCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATNAMEO2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
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
        //        drpSUBCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSUBCATNAMEO2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpStatus.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        //    }
        //}
        //public void LastData()
        //{
        //    Listview1.SelectedIndex = Listview1.Items.Count - 1;
        //    drpCOMPANYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpSUBCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATTYPE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATNAME.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATNAMEO.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSUBCATNAMEO2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpCATID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
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
                    lblSHORTNAME2h.Visible = lblSUBCATNAME2h.Visible = lblSUBCATNAMEO2h.Visible = lblSUBCATNAMEO22h.Visible = lblREMARKS2h.Visible = false;
                    //2true
                    txtSHORTNAME2h.Visible = txtSUBCATNAME2h.Visible = txtSUBCATNAMEO2h.Visible = txtSUBCATNAMEO22h.Visible = txtREMARKS2h.Visible = true;

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
                    lblSHORTNAME2h.Visible = lblSUBCATNAME2h.Visible = lblSUBCATNAMEO2h.Visible = lblSUBCATNAMEO22h.Visible = lblREMARKS2h.Visible = true;
                    //2false
                    txtSHORTNAME2h.Visible = txtSUBCATNAME2h.Visible = txtSUBCATNAMEO2h.Visible = txtSUBCATNAMEO22h.Visible = txtREMARKS2h.Visible = false;

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
                    lblSHORTNAME1s.Visible = lblSUBCATNAME1s.Visible = lblSUBCATNAMEO1s.Visible = lblSUBCATNAMEO21s.Visible = lblREMARKS1s.Visible = false;
                    //1true
                    txtSHORTNAME1s.Visible = txtSUBCATNAME1s.Visible = txtSUBCATNAMEO1s.Visible = txtSUBCATNAMEO21s.Visible = txtREMARKS1s.Visible = true;
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
                    lblSHORTNAME1s.Visible = lblSUBCATNAME1s.Visible = lblSUBCATNAMEO1s.Visible = lblSUBCATNAMEO21s.Visible = lblREMARKS1s.Visible = true;
                    //1false
                    txtSHORTNAME1s.Visible = txtSUBCATNAME1s.Visible = txtSUBCATNAMEO1s.Visible = txtSUBCATNAMEO21s.Visible = txtREMARKS1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICSUBCATEGORY").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblSHORTNAME1s.ID == item.LabelID)
                    txtSHORTNAME1s.Text = lblSHORTNAME1s.Text = item.LabelName;
                else if (lblSUBCATNAME1s.ID == item.LabelID)
                    txtSUBCATNAME1s.Text = lblSUBCATNAME1s.Text = lblhSUBCATNAME.Text = item.LabelName;
                else if (lblSUBCATNAMEO1s.ID == item.LabelID)
                    txtSUBCATNAMEO1s.Text = lblSUBCATNAMEO1s.Text = lblhSUBCATNAMEO.Text = item.LabelName;
                else if (lblSUBCATNAMEO21s.ID == item.LabelID)
                    txtSUBCATNAMEO21s.Text = lblSUBCATNAMEO21s.Text = lblhSUBCATNAMEO2.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;

                else if (lblSHORTNAME2h.ID == item.LabelID)
                    txtSHORTNAME2h.Text = lblSHORTNAME2h.Text = item.LabelName;
                else if (lblSUBCATNAME2h.ID == item.LabelID)
                    txtSUBCATNAME2h.Text = lblSUBCATNAME2h.Text = lblhSUBCATNAME.Text = item.LabelName;
                else if (lblSUBCATNAMEO2h.ID == item.LabelID)
                    txtSUBCATNAMEO2h.Text = lblSUBCATNAMEO2h.Text = lblhSUBCATNAMEO.Text = item.LabelName;
                else if (lblSUBCATNAMEO22h.ID == item.LabelID)
                    txtSUBCATNAMEO22h.Text = lblSUBCATNAMEO22h.Text = lblhSUBCATNAMEO2.Text = item.LabelName;
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
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ICSUBCATEGORY").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\ICSUBCATEGORY.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("ICSUBCATEGORY").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblSHORTNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSHORTNAME1s.Text;
                else if (lblSUBCATNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATNAME1s.Text;
                else if (lblSUBCATNAMEO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATNAMEO1s.Text;
                else if (lblSUBCATNAMEO21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATNAMEO21s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;

                else if (lblSHORTNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSHORTNAME2h.Text;
                else if (lblSUBCATNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATNAME2h.Text;
                else if (lblSUBCATNAMEO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATNAMEO2h.Text;
                else if (lblSUBCATNAMEO22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUBCATNAMEO22h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\ICSUBCATEGORY.xml"));

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
            txtSUBCATNAME.Enabled = true;
            txtSUBCATNAMEO.Enabled = true;
            txtSUBCATNAMEO2.Enabled = true;
            txtREMARKS.Enabled = true;
        }
        public void Readonly()
        {
            txtSHORTNAME.Enabled = false;
            txtSUBCATNAME.Enabled = false;
            txtSUBCATNAMEO.Enabled = false;
            txtSUBCATNAMEO2.Enabled = false;
            txtREMARKS.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICSUBCATEGORY.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICSUBCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
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

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.ICSUBCATEGORY.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICSUBCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
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
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.ICSUBCATEGORY.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICSUBCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
        //    int Totalrec = DB.ICSUBCATEGORY.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICSUBCATEGORY.OrderBy(m => m.JobId).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
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

                        int ID = Convert.ToInt32(e.CommandArgument);
                      
                        Database.ICSUBCATEGORY objSOJobDesc = DB.ICSUBCATEGORies.Single(p => p.SUBCATID == ID);
                        objSOJobDesc.Status = 0;
                        DB.SaveChanges();

                        String url = "delete ICSUBCATEGORY with " + "TenentID = " + TID + "COMPANYID = 2" + "SUBCATID = " + ID;
                        String evantname = "delete";
                        String tablename = "ICSUBCATEGORY";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                        BindData();
                       
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.ICSUBCATEGORY objICSUBCATEGORY = DB.ICSUBCATEGORies.Single(p => p.SUBCATID == ID);
                      
                        txtSHORTNAME.Text = objICSUBCATEGORY.SHORTNAME.ToString();
                        txtSUBCATNAME.Text = objICSUBCATEGORY.SUBCATNAME.ToString();
                        txtSUBCATNAMEO.Text = objICSUBCATEGORY.SUBCATNAMEO.ToString();
                        txtSUBCATNAMEO2.Text = objICSUBCATEGORY.SUBCATNAMEO2.ToString();
                        txtREMARKS.Text = objICSUBCATEGORY.REMARKS.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        Write();
                        btnAdd.ValidationGroup = "submit";
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

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ICSUBCATEGORY.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ICSUBCATEGORY.OrderBy(m => m.JobId).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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