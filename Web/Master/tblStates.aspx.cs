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
    public partial class tblStates : System.Web.UI.Page
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
                binddata();
            }
        }
        #region Step2
        public void BindData()
        {
            Listview1.DataSource = DB.tblStates.Where(p => p.ACTIVE1 == "Y").OrderBy(m => m.StateID).OrderByDescending(m=>m.StateID);
                Listview1.DataBind();
            //List<Database.tblState> List = DB.tblStates.Where(p=>p.ACTIVE1 == "Y").OrderBy(m => m.StateID).ToList();
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
        public void binddata()
        {
            drpCOUNTRYID.DataSource = DB.tblCOUNTRies.Where(p=>p.TenentID == TID).OrderBy(p=>p.COUNTRYID);
            drpCOUNTRYID.DataTextField = "COUNAME1";
            drpCOUNTRYID.DataValueField = "COUNTRYID";
            drpCOUNTRYID.DataBind();
            drpCOUNTRYID.Items.Insert(0, new ListItem("-- Select Country --", "0"));
            
        }
        public void GetShow()
        {

            lblCOUNTRYID1s.Attributes["class"] = lblMYNAME11s.Attributes["class"] = lblMYNAME21s.Attributes["class"] = lblMYNAME31s.Attributes["class"] = "control-label col-md-4  getshow";
            lblCOUNTRYID2h.Attributes["class"] = lblMYNAME12h.Attributes["class"] = lblMYNAME22h.Attributes["class"] = lblMYNAME32h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCOUNTRYID1s.Attributes["class"] = lblMYNAME11s.Attributes["class"] = lblMYNAME21s.Attributes["class"] = lblMYNAME31s.Attributes["class"] = "control-label col-md-4  gethide";
            lblCOUNTRYID2h.Attributes["class"] = lblMYNAME12h.Attributes["class"] = lblMYNAME22h.Attributes["class"] = lblMYNAME32h.Attributes["class"] = "control-label col-md-4  getshow";
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
            drpCOUNTRYID.SelectedIndex = 0;
            txtMYNAME1.Text = "";
            txtMYNAME2.Text = "";
            txtMYNAME3.Text = "";
            //txtACTIVE1.Text = "";
            //txtACTIVE2.Text = "";
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
                        Database.tblState objtblStates = new Database.tblState();
                        //Server Content Send data Yogesh
                        int CID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                        objtblStates.COUNTRYID = CID;
                        objtblStates.StateID = DB.tblStates.Where(p=>p.COUNTRYID == CID).Count() > 0 ? Convert.ToInt32(DB.tblStates.Where(p=>p.COUNTRYID == CID).Max(p => p.StateID) + 1) : 1;
                        objtblStates.MYNAME1 = txtMYNAME1.Text;
                        objtblStates.MYNAME2 = txtMYNAME2.Text;
                        objtblStates.MYNAME3 = txtMYNAME3.Text;
                        objtblStates.ACTIVE1 = "Y";                        
                        //objtblStates.ACTIVE2 = txtACTIVE2.Text;
                        objtblStates.CRUP_ID = 1;


                        DB.tblStates.AddObject(objtblStates);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "AddNew";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["SID"] != null && ViewState["CID"] != null)
                        {                            
                            int sid = Convert.ToInt32(ViewState["SID"]);
                            int cid = Convert.ToInt32(ViewState["CID"]);
                            Database.tblState objtblStates = DB.tblStates.Single(p => p.StateID == sid && p.COUNTRYID == cid);
                            objtblStates.COUNTRYID = Convert.ToInt32(drpCOUNTRYID.SelectedValue);
                            objtblStates.MYNAME1 = txtMYNAME1.Text;
                            objtblStates.MYNAME2 = txtMYNAME2.Text;
                            objtblStates.MYNAME3 = txtMYNAME3.Text;
                            //objtblStates.ACTIVE1 = "Y";
                            //objtblStates.ACTIVE2 = txtACTIVE2.Text;
                            //objtblStates.CRUP_ID = 1;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
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
            drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtMYNAME1.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYNAME2.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYNAME3.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtMYNAME1.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYNAME2.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYNAME3.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
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
                drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtMYNAME1.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYNAME2.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYNAME3.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            drpCOUNTRYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtMYNAME1.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYNAME2.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYNAME3.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE1.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE2.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblCOUNTRYID2h.Visible = lblMYNAME12h.Visible = lblMYNAME22h.Visible = lblMYNAME32h.Visible = false;
                    //2true
                    txtCOUNTRYID2h.Visible = txtMYNAME12h.Visible = txtMYNAME22h.Visible = txtMYNAME32h.Visible = true;

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
                    lblCOUNTRYID2h.Visible = lblMYNAME12h.Visible = lblMYNAME22h.Visible = lblMYNAME32h.Visible = true;
                    //2false
                    txtCOUNTRYID2h.Visible = txtMYNAME12h.Visible = txtMYNAME22h.Visible = txtMYNAME32h.Visible = false;

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
                    lblCOUNTRYID1s.Visible = lblMYNAME11s.Visible = lblMYNAME21s.Visible = lblMYNAME31s.Visible = false;
                    //1true
                    txtCOUNTRYID1s.Visible = txtMYNAME11s.Visible = txtMYNAME21s.Visible = txtMYNAME31s.Visible = true;
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
                    lblCOUNTRYID1s.Visible = lblMYNAME11s.Visible = lblMYNAME21s.Visible = lblMYNAME31s.Visible = true;
                    //1false
                    txtCOUNTRYID1s.Visible = txtMYNAME11s.Visible = txtMYNAME21s.Visible = txtMYNAME31s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblStates").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = item.LabelName;
                else if (lblMYNAME11s.ID == item.LabelID)
                    txtMYNAME11s.Text = lblMYNAME11s.Text = lblhMYNAME1.Text = item.LabelName;
                else if (lblMYNAME21s.ID == item.LabelID)
                    txtMYNAME21s.Text = lblMYNAME21s.Text = lblhMYNAME2.Text = item.LabelName;
                else if (lblMYNAME31s.ID == item.LabelID)
                    txtMYNAME31s.Text = lblMYNAME31s.Text = lblhMYNAME3.Text = item.LabelName;

                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = item.LabelName;
                else if (lblMYNAME12h.ID == item.LabelID)
                    txtMYNAME12h.Text = lblMYNAME12h.Text = lblhMYNAME1.Text = item.LabelName;
                else if (lblMYNAME22h.ID == item.LabelID)
                    txtMYNAME22h.Text = lblMYNAME22h.Text = lblhMYNAME2.Text = item.LabelName;
                else if (lblMYNAME32h.ID == item.LabelID)
                    txtMYNAME32h.Text = lblMYNAME32h.Text = lblhMYNAME3.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblStates").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblStates.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblStates").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                else if (lblMYNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYNAME11s.Text;
                else if (lblMYNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYNAME21s.Text;
                else if (lblMYNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYNAME31s.Text;

                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                else if (lblMYNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYNAME12h.Text;
                else if (lblMYNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYNAME22h.Text;
                else if (lblMYNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYNAME32h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblStates.xml"));

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
            drpCOUNTRYID.Enabled = true;
            txtMYNAME1.Enabled = true;
            txtMYNAME2.Enabled = true;
            txtMYNAME3.Enabled = true;
            //txtACTIVE1.Enabled = true;
            //txtACTIVE2.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drpCOUNTRYID.Enabled = false;
            txtMYNAME1.Enabled = false;
            txtMYNAME2.Enabled = false;
            txtMYNAME3.Enabled = false;
            //txtACTIVE1.Enabled = false;
            //txtACTIVE2.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblStates.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblStates.OrderBy(m => m.StateID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tblStates.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblStates.OrderBy(m => m.StateID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tblStates.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblStates.OrderBy(m => m.StateID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tblStates.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblStates.OrderBy(m => m.StateID).Take(take).Skip(Skip)).ToList());
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
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tblState objSOJobDesc = DB.tblStates.Single(p => p.StateID == ID);
                        objSOJobDesc.ACTIVE1 = "N";
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblStates.OrderBy(m => m.StateID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int SID = Convert.ToInt32(ID[0]);
                        int CID = Convert.ToInt32(ID[1]);
                        
                        Database.tblState objtblStates = DB.tblStates.Single(p =>p.COUNTRYID == CID && p.StateID == SID);
                        drpCOUNTRYID.SelectedValue = objtblStates.COUNTRYID.ToString();
                        txtMYNAME1.Text = objtblStates.MYNAME1.ToString();
                        txtMYNAME2.Text = objtblStates.MYNAME2.ToString();
                        txtMYNAME3.Text = objtblStates.MYNAME3.ToString();
                        //txtACTIVE1.Text = objtblStates.ACTIVE1.ToString();
                        //txtACTIVE2.Text = objtblStates.ACTIVE2.ToString();
                        //txtCRUP_ID.Text = objtblStates.CRUP_ID.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["SID"] = SID;
                        ViewState["CID"] = CID;
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
            int Totalrec = DB.tblStates.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblStates.OrderBy(m => m.StateID).Take(Tvalue).Skip(Svalue)).ToList());
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