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
    public partial class tblCityStatesCounty : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        int TID = 0;
        string UID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            UID = ((USER_MST)Session["USER"]).LOGIN_ID;
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
                //FirstData();

            }
        }
        #region Step2
        public void BindData()
        {

            List<Database.tblCityStatesCounty> List = DB.tblCityStatesCounties.OrderBy(m => m.CityID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblStateID1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblCityEnglish1s.Attributes["class"] = lblCityArabic1s.Attributes["class"] = lblCityOther1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblStateID2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblCityEnglish2h.Attributes["class"] = lblCityArabic2h.Attributes["class"] = lblCityOther2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblStateID1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblCityEnglish1s.Attributes["class"] = lblCityArabic1s.Attributes["class"] = lblCityOther1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblStateID2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblCityEnglish2h.Attributes["class"] = lblCityArabic2h.Attributes["class"] = lblCityOther2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            drpStateID.SelectedIndex = 0;
            drpCOUNTRYID.SelectedIndex = 0;
            txtCityEnglish.Text = "";
            txtCityArabic.Text = "";
            txtCityOther.Text = "";


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
                        drpStateID.Items.Clear();
                        drpStateID.Items.Insert(0, new ListItem("-- State --", "0"));
                    }
                    else if (btnAdd.Text == "Save")
                    {
                        Database.tblCityStatesCounty objtblCityStatesCounty = new Database.tblCityStatesCounty();
                        //Server Content Send data Yogesh
                        int cityid = DB.tblCityStatesCounties.Count() > 0 ? Convert.ToInt32(DB.tblCityStatesCounties.Max(p => p.CityID) + 1) : 1;
                        objtblCityStatesCounty.CityID = cityid;
                        objtblCityStatesCounty.StateID = Convert.ToInt32(drpStateID.SelectedValue);
                        objtblCityStatesCounty.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                        objtblCityStatesCounty.CityEnglish = txtCityEnglish.Text;
                        objtblCityStatesCounty.CityArabic = txtCityArabic.Text;
                        objtblCityStatesCounty.CityOther = txtCityOther.Text;
                        objtblCityStatesCounty.UploadDate = DateTime.Now;
                        objtblCityStatesCounty.Uploadby = UID;
                       
                        DB.tblCityStatesCounties.AddObject(objtblCityStatesCounty);
                        DB.SaveChanges();
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        btnAdd.ValidationGroup = "ss";
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            string[] ID = ViewState["Edit"].ToString().Split('-');
                            int City = Convert.ToInt32(ID[0]);
                            int state = Convert.ToInt32(ID[1]);
                            int Country = Convert.ToInt32(ID[2]);
                            Database.tblCityStatesCounty objtblCityStatesCounty = DB.tblCityStatesCounties.Single(p => p.CityID == City && p.StateID == state && p.COUNTRYID == Country);
                            objtblCityStatesCounty.StateID = Convert.ToInt32(drpStateID.SelectedValue);
                            objtblCityStatesCounty.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                            objtblCityStatesCounty.CityEnglish = txtCityEnglish.Text;
                            objtblCityStatesCounty.CityArabic = txtCityArabic.Text;
                            objtblCityStatesCounty.CityOther = txtCityOther.Text;

                            DB.SaveChanges();
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "ss";
                            ViewState["Edit"] = null;
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
            //Response.Redirect(Session["Previous"].ToString());
            Response.Redirect("tblCityStatesCounty.aspx");
        }
        public void FillContractorID()
        {
            //drpSynID.Items.Insert(0, new ListItem("-- Select --", "0"));drpSynID.DataSource = DB.0;drpSynID.DataTextField = "0";drpSynID.DataValueField = "0";drpSynID.DataBind();
            drpCOUNTRYID.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.Active == "Y");
            drpCOUNTRYID.DataTextField = "COUNAME1";
            drpCOUNTRYID.DataValueField = "COUNTRYID";
            drpCOUNTRYID.DataBind();
            drpCOUNTRYID.Items.Insert(0, new ListItem("-- Country --", "0"));

            drpStateID.Items.Insert(0, new ListItem("-- State --", "0"));
        }
        protected void drpCOUNTRYID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CountryID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
            BindState(CountryID);
        }
        public void BindState(int SID)
        {
            drpStateID.DataSource = DB.tblStates.Where(p => p.COUNTRYID == SID);
            drpStateID.DataTextField = "MYNAME1";
            drpStateID.DataValueField = "StateID";
            drpStateID.DataBind();
            drpStateID.Items.Insert(0, new ListItem("-- State --", "0"));
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
        //    drpStateID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCityEnglish.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCityArabic.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCityOther.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtLandLine.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtAssignedRoute.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSHORTCODE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtZONE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtUploadDate.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtUploadby.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSyncby.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpSynID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //}
        //public void NextData()
        //{

        //    if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
        //    {
        //        Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
        //        drpStateID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCityEnglish.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCityArabic.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCityOther.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtLandLine.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtAssignedRoute.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSHORTCODE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtZONE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtUploadDate.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtUploadby.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSyncby.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpSynID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

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
        //        drpStateID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtCityEnglish.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCityArabic.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCityOther.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtLandLine.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtAssignedRoute.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSHORTCODE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtZONE.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtUploadDate.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtUploadby.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtSyncby.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpSynID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //    }
        //}
        //public void LastData()
        //{
        //    Listview1.SelectedIndex = Listview1.Items.Count - 1;
        //    drpStateID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtCityEnglish.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCityArabic.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCityOther.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtLandLine.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtAssignedRoute.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSHORTCODE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtZONE.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtUploadDate.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtUploadby.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtSyncby.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpSynID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //}


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblStateID2h.Visible = lblCOUNTRYID2h.Visible = lblCityEnglish2h.Visible = lblCityArabic2h.Visible = lblCityOther2h.Visible = false;
                    //2true
                    txtStateID2h.Visible = txtCOUNTRYID2h.Visible = txtCityEnglish2h.Visible = txtCityArabic2h.Visible = txtCityOther2h.Visible = true;

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
                    lblStateID2h.Visible = lblCOUNTRYID2h.Visible = lblCityEnglish2h.Visible = lblCityArabic2h.Visible = lblCityOther2h.Visible = true;
                    //2false
                    txtStateID2h.Visible = txtCOUNTRYID2h.Visible = txtCityEnglish2h.Visible = txtCityArabic2h.Visible = txtCityOther2h.Visible = false;

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
                    lblStateID1s.Visible = lblCOUNTRYID1s.Visible = lblCityEnglish1s.Visible = lblCityArabic1s.Visible = lblCityOther1s.Visible = false;
                    //1true
                    txtStateID1s.Visible = txtCOUNTRYID1s.Visible = txtCityEnglish1s.Visible = txtCityArabic1s.Visible = txtCityOther1s.Visible = true;
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
                    lblStateID1s.Visible = lblCOUNTRYID1s.Visible = lblCityEnglish1s.Visible = lblCityArabic1s.Visible = lblCityOther1s.Visible = true;
                    //1false
                    txtStateID1s.Visible = txtCOUNTRYID1s.Visible = txtCityEnglish1s.Visible = txtCityArabic1s.Visible = txtCityOther1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblCityStatesCounty").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblStateID1s.ID == item.LabelID)
                    txtStateID1s.Text = lblStateID1s.Text = lblhStateID.Text = item.LabelName;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = lblhCOUNTRYID.Text = item.LabelName;
                else if (lblCityEnglish1s.ID == item.LabelID)
                    txtCityEnglish1s.Text = lblCityEnglish1s.Text = lblhCityEnglish.Text = item.LabelName;
                else if (lblCityArabic1s.ID == item.LabelID)
                    txtCityArabic1s.Text = lblCityArabic1s.Text = lblhCityArabic.Text = item.LabelName;
                else if (lblCityOther1s.ID == item.LabelID)
                    txtCityOther1s.Text = lblCityOther1s.Text = lblhCityOther.Text = item.LabelName;

                else if (lblStateID2h.ID == item.LabelID)
                    txtStateID2h.Text = lblStateID2h.Text = lblhStateID.Text = item.LabelName;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = lblhCOUNTRYID.Text = item.LabelName;
                else if (lblCityEnglish2h.ID == item.LabelID)
                    txtCityEnglish2h.Text = lblCityEnglish2h.Text = lblhCityEnglish.Text = item.LabelName;
                else if (lblCityArabic2h.ID == item.LabelID)
                    txtCityArabic2h.Text = lblCityArabic2h.Text = lblhCityArabic.Text = item.LabelName;
                else if (lblCityOther2h.ID == item.LabelID)
                    txtCityOther2h.Text = lblCityOther2h.Text = lblhCityOther.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblCityStatesCounty").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblCityStatesCounty.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblCityStatesCounty").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblStateID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStateID1s.Text;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                else if (lblCityEnglish1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCityEnglish1s.Text;
                else if (lblCityArabic1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCityArabic1s.Text;
                else if (lblCityOther1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCityOther1s.Text;

                else if (lblStateID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStateID2h.Text;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                else if (lblCityEnglish2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCityEnglish2h.Text;
                else if (lblCityArabic2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCityArabic2h.Text;
                else if (lblCityOther2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCityOther2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblCityStatesCounty.xml"));

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
            drpStateID.Enabled = true;
            drpCOUNTRYID.Enabled = true;
            txtCityEnglish.Enabled = true;
            txtCityArabic.Enabled = true;
            txtCityOther.Enabled = true;
        }
        public void Readonly()
        {
            drpStateID.Enabled = false;
            drpCOUNTRYID.Enabled = false;
            txtCityEnglish.Enabled = false;
            txtCityArabic.Enabled = false;
            txtCityOther.Enabled = false;
        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblCityStatesCounties.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCityStatesCounties.OrderBy(m => m.CityID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tblCityStatesCounties.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCityStatesCounties.OrderBy(m => m.CityID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tblCityStatesCounties.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCityStatesCounties.OrderBy(m => m.CityID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tblCityStatesCounties.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCityStatesCounties.OrderBy(m => m.CityID).Take(take).Skip(Skip)).ToList());
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
            //FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                   

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split('-');
                        int City = Convert.ToInt32(ID[0]);
                        int state = Convert.ToInt32(ID[1]);
                        int Country = Convert.ToInt32(ID[2]);
                        BindState(Country);
                        Database.tblCityStatesCounty objtblCityStatesCounty = DB.tblCityStatesCounties.Single(p => p.CityID == City && p.StateID == state && p.COUNTRYID == Country);
                        drpStateID.SelectedValue = objtblCityStatesCounty.StateID.ToString();
                        drpCOUNTRYID.SelectedValue = objtblCityStatesCounty.COUNTRYID.ToString();
                        txtCityEnglish.Text = objtblCityStatesCounty.CityEnglish.ToString();
                        txtCityArabic.Text = objtblCityStatesCounty.CityArabic.ToString();
                        txtCityOther.Text = objtblCityStatesCounty.CityOther.ToString();
                        
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = City + "-" + state + "-" + Country;
                        Write();
                        drpStateID.Enabled = false;
                        drpCOUNTRYID.Enabled = false;
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

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblCityStatesCounties.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblCityStatesCounties.OrderBy(m => m.CityID).Take(Tvalue).Skip(Svalue)).ToList());
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

        protected void Linksearch_Click(object sender, EventArgs e)
        {

        }



    }
}