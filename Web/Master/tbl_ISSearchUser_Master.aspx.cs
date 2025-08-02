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
using Web.CRM;

namespace Web.Master
{
    public partial class tbl_ISSearchUser_Master : System.Web.UI.Page
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
                Readonly();
                Session["LANGUAGE"] = "en-US";
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
            //List<Database.tbl_ISSearchUser_Master> List = DB.tbl_ISSearchUser_Master.Where(p=>p.TenentID == TID).OrderBy(m => m.LocationID).ToList();
            Listview1.DataSource=DB.tbl_ISSearchUser_Master.Where(p=>p.TenentID == TID).OrderBy(m => m.LocationID).OrderByDescending(m=>m.LocationID);
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

            lblSearchID1s.Attributes["class"] = lbluserID1s.Attributes["class"] = lblAllow1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblSearchID2h.Attributes["class"] = lbluserID2h.Attributes["class"] = lblAllow2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblSearchID1s.Attributes["class"] = lbluserID1s.Attributes["class"] = lblAllow1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblSearchID2h.Attributes["class"] = lbluserID2h.Attributes["class"] = lblAllow2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpLocationID.SelectedIndex = 0;
            drpSearchID.SelectedIndex = 0;
            drpuserID.SelectedIndex = 0;
            cbAllow.Checked = false;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.tbl_ISSearchUser_Master objtbl_ISSearchUser_Master = new Database.tbl_ISSearchUser_Master();
                        //Server Content Send data Yogesh
                        //objtbl_ISSearchUser_Master.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                        objtbl_ISSearchUser_Master.TenentID = TID;
                        objtbl_ISSearchUser_Master.LocationID = DB.tbl_ISSearchUser_Master.Where(p=>p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_ISSearchUser_Master.Where(p=>p.TenentID == TID).Max(p => p.LocationID) + 1) : 1;
                        objtbl_ISSearchUser_Master.SearchID = Convert.ToInt32(drpSearchID.SelectedValue);
                        objtbl_ISSearchUser_Master.userID = Convert.ToInt32(drpuserID.SelectedValue);
                        objtbl_ISSearchUser_Master.Allow = cbAllow.Checked? true : false;


                        DB.tbl_ISSearchUser_Master.AddObject(objtbl_ISSearchUser_Master);
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

                        if (ViewState["TID"] != null && ViewState["LID"] != null)
                        {
                            int Tenetid = Convert.ToInt32(ViewState["TID"]);
                            int Locationid = Convert.ToInt32(ViewState["LID"]);
                            Database.tbl_ISSearchUser_Master objtbl_ISSearchUser_Master = DB.tbl_ISSearchUser_Master.Single(p => p.TenentID == Tenetid && p.LocationID == Locationid);
                            
                            //objtbl_ISSearchUser_Master.SearchID = Convert.ToInt32(drpSearchID.SelectedValue);
                            //objtbl_ISSearchUser_Master.userID = Convert.ToInt32(drpuserID.SelectedValue);
                            objtbl_ISSearchUser_Master.Allow = cbAllow.Checked ? true : false;

                            ViewState["TID"] = null;
                            ViewState["LID"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                           // navigation.Visible = true;
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
            string REFtype = Convert.ToString(SearchManagement.SearchType.Search);
            string REFSubtype = Convert.ToString(SearchManagement.SearchType.Company);
            drpSearchID.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == REFtype && P.REFSUBTYPE == REFSubtype && P.TenentID == TID && P.ACTIVE == "Y").OrderBy(a => a.REFNAME1);
            drpSearchID.DataTextField = "REFNAME1";
            drpSearchID.DataValueField = "REFID";
            drpSearchID.DataBind();
            drpSearchID.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpuserID.DataSource = DB.USER_MST.Where(p=>p.TenentID == TID);
            drpuserID.DataTextField = "FIRST_NAME";
            drpuserID.DataValueField = "USER_ID";
            drpuserID.DataBind();
            drpuserID.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpSearchID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpuserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //cbAllow.Checked = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpSearchID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpuserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //cbAllow.Checked = Listview1.SelectedDataKey[0].ToString();

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
                //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpSearchID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpuserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //cbAllow.Checked = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpLocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpSearchID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpuserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //cbAllow.Checked = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblSearchID2h.Visible = lbluserID2h.Visible = lblAllow2h.Visible = false;
                    //2true
                    txtSearchID2h.Visible = txtuserID2h.Visible = txtAllow2h.Visible = true;

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
                    lblSearchID2h.Visible = lbluserID2h.Visible = lblAllow2h.Visible = true;
                    //2false
                    txtSearchID2h.Visible = txtuserID2h.Visible = txtAllow2h.Visible = false;

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
                    lblSearchID1s.Visible = lbluserID1s.Visible = lblAllow1s.Visible = false;
                    //1true
                    txtSearchID1s.Visible = txtuserID1s.Visible = txtAllow1s.Visible = true;
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
                    lblSearchID1s.Visible = lbluserID1s.Visible = lblAllow1s.Visible = true;
                    //1false
                    txtSearchID1s.Visible = txtuserID1s.Visible = txtAllow1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_ISSearchUser_Master").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblSearchID1s.ID == item.LabelID)
                    txtSearchID1s.Text = lblSearchID1s.Text = lblhSearchID.Text = item.LabelName;
                else if (lbluserID1s.ID == item.LabelID)
                    txtuserID1s.Text = lbluserID1s.Text = lblhuserID.Text = item.LabelName;
                else if (lblAllow1s.ID == item.LabelID)
                    txtAllow1s.Text = lblAllow1s.Text = item.LabelName;

                else if (lblSearchID2h.ID == item.LabelID)
                    txtSearchID2h.Text = lblSearchID2h.Text = lblhSearchID.Text = item.LabelName;
                else if (lbluserID2h.ID == item.LabelID)
                    txtuserID2h.Text = lbluserID2h.Text = lblhuserID.Text = item.LabelName;
                else if (lblAllow2h.ID == item.LabelID)
                    txtAllow2h.Text = lblAllow2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_ISSearchUser_Master").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tbl_ISSearchUser_Master.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tbl_ISSearchUser_Master").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblSearchID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSearchID1s.Text;
                else if (lbluserID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtuserID1s.Text;
                else if (lblAllow1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAllow1s.Text;

                else if (lblSearchID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSearchID2h.Text;
                else if (lbluserID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtuserID2h.Text;
                else if (lblAllow2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAllow2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tbl_ISSearchUser_Master.xml"));

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
            //drpLocationID.Enabled = true;
            drpSearchID.Enabled = true;
            drpuserID.Enabled = true;
            cbAllow.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpLocationID.Enabled = false;
            drpSearchID.Enabled = false;
            drpuserID.Enabled = false;
            cbAllow.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_ISSearchUser_Master.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_ISSearchUser_Master.OrderBy(m => m.LocationID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tbl_ISSearchUser_Master.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_ISSearchUser_Master.OrderBy(m => m.LocationID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tbl_ISSearchUser_Master.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_ISSearchUser_Master.OrderBy(m => m.LocationID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tbl_ISSearchUser_Master.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_ISSearchUser_Master.OrderBy(m => m.LocationID).Take(take).Skip(Skip)).ToList());
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
                        int str1 = Convert.ToInt32(ID[0]);
                        int str2 = Convert.ToInt32(ID[1]);

                        Database.tbl_ISSearchUser_Master objSOJobDesc = DB.tbl_ISSearchUser_Master.Single(p => p.TenentID == str1 && p.LocationID == str2);
                        objSOJobDesc.Allow = false;
                        DB.SaveChanges();
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_ISSearchUser_Master.OrderBy(m => m.JobId).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int str1 = Convert.ToInt32(ID[0]);
                        int str2 = Convert.ToInt32(ID[1]);

                        Database.tbl_ISSearchUser_Master objtbl_ISSearchUser_Master = DB.tbl_ISSearchUser_Master.Single(p => p.TenentID == str1 && p.LocationID == str2);
                        //drpLocationID.SelectedValue = objtbl_ISSearchUser_Master.LocationID.ToString();
                        drpSearchID.SelectedValue = objtbl_ISSearchUser_Master.SearchID.ToString();
                        drpuserID.SelectedValue = objtbl_ISSearchUser_Master.userID.ToString();
                        cbAllow.Checked = (objtbl_ISSearchUser_Master.Allow == true) ? true : false;

                        btnAdd.Text = "Update";
                        ViewState["TID"] = str1;
                        ViewState["LID"] = str2;
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
            int Totalrec = DB.tbl_ISSearchUser_Master.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_ISSearchUser_Master.OrderBy(m => m.LocationID).Take(Tvalue).Skip(Svalue)).ToList());
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