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
    public partial class Tbl_Position_Mst : System.Web.UI.Page
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
            List<Database.Tbl_Position_Mst> List = DB.Tbl_Position_Mst.Where(p=>p.Active == true && p.TenentID == TID).OrderByDescending(m => m.PositionID).ToList();
           
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
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

            lblPositionName1s.Attributes["class"] = lblDatetime1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPositionName2h.Attributes["class"] = lblDatetime2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPositionName1s.Attributes["class"] = lblDatetime1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPositionName2h.Attributes["class"] = lblDatetime2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpPositionID.SelectedIndex = 0;
            txtPositionName.Text = "";            
            txtDatetime.Text = "";

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
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.Tbl_Position_Mst objTbl_Position_Mst = new Database.Tbl_Position_Mst();
                        //Server Content Send data Yogesh
                       
                        objTbl_Position_Mst.TenentID = TID;
                        objTbl_Position_Mst.PositionID = DB.Tbl_Position_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_Position_Mst.Where(p => p.TenentID == TID).Max(p => p.PositionID) + 1) : 1;
                        objTbl_Position_Mst.PositionName = txtPositionName.Text;
                        objTbl_Position_Mst.PositionNameAR = txtPositionName.Text;
                        objTbl_Position_Mst.PositionNameFR = txtPositionName.Text;
                        objTbl_Position_Mst.Active = true;
                        objTbl_Position_Mst.Datetime = DateTime.Now;
                        //objTbl_Position_Mst.Active = cbActive.Checked;
                        //objTbl_Position_Mst.Datetime = Convert.ToDateTime(txtDatetime.Text);


                        DB.Tbl_Position_Mst.AddObject(objTbl_Position_Mst);
                        DB.SaveChanges();
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        btnAdd.Text = "AddNew";
                        btnAdd.ValidationGroup = "ss";
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["TID"] != null && ViewState["PID"] != null)
                        {                           
                            int tid = Convert.ToInt32(ViewState["TID"]);
                            int pid = Convert.ToInt32(ViewState["PID"]);
                            Database.Tbl_Position_Mst objTbl_Position_Mst = DB.Tbl_Position_Mst.Single(p => p.TenentID == tid && p.PositionID == pid);
                            //objTbl_Position_Mst.PositionID = Convert.ToInt32(drpPositionID.SelectedValue);
                            objTbl_Position_Mst.PositionName = txtPositionName.Text;
                            objTbl_Position_Mst.PositionNameAR = txtPositionName.Text;
                            objTbl_Position_Mst.PositionNameFR = txtPositionName.Text;
                            //objTbl_Position_Mst.Active = cbActive.Checked;
                            //objTbl_Position_Mst.Datetime = Convert.ToDateTime(txtDatetime.Text);

                            ViewState["TID"] = null;
                            ViewState["PID"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            btnAdd.ValidationGroup = "ss";
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
            //drpDatetime.Items.Insert(0, new ListItem("-- Select --", "0"));drpDatetime.DataSource = DB.0;drpDatetime.DataTextField = "0";drpDatetime.DataValueField = "0";drpDatetime.DataBind();
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
            //drpPositionID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPositionName.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            txtDatetime.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpPositionID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtPositionName.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                txtDatetime.Text = Listview1.SelectedDataKey[0].ToString();

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
                //drpPositionID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtPositionName.Text = Listview1.SelectedDataKey[0].ToString();
               // cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                txtDatetime.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpPositionID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPositionName.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            txtDatetime.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblPositionName2h.Visible = lblDatetime2h.Visible = false;
                    //2true
                    txtPositionName2h.Visible = txtDatetime2h.Visible = true;

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
                    lblPositionName2h.Visible = lblDatetime2h.Visible = true;
                    //2false
                    txtPositionName2h.Visible = txtDatetime2h.Visible = false;

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
                    lblPositionName1s.Visible = lblDatetime1s.Visible = false;
                    //1true
                    txtPositionName1s.Visible = txtDatetime1s.Visible = true;
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
                    lblPositionName1s.Visible = lblDatetime1s.Visible = true;
                    //1false
                    txtPositionName1s.Visible = txtDatetime1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Tbl_Position_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblPositionName1s.ID == item.LabelID)
                    txtPositionName1s.Text = lblPositionName1s.Text = lblhPositionName.Text = item.LabelName;
                
                else if (lblDatetime1s.ID == item.LabelID)
                    txtDatetime1s.Text = lblDatetime1s.Text = lblhDatetime.Text = item.LabelName;

                else if (lblPositionName2h.ID == item.LabelID)
                    txtPositionName2h.Text = lblPositionName2h.Text = lblhPositionName.Text = item.LabelName;
               
                else if (lblDatetime2h.ID == item.LabelID)
                    txtDatetime2h.Text = lblDatetime2h.Text = lblhDatetime.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Tbl_Position_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\Tbl_Position_Mst.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("Tbl_Position_Mst").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblPositionName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPositionName1s.Text;
               
                else if (lblDatetime1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDatetime1s.Text;

                else if (lblPositionName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPositionName2h.Text;
                
                else if (lblDatetime2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDatetime2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\Tbl_Position_Mst.xml"));

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
            //drpPositionID.Enabled = true;
            txtPositionName.Enabled = true;
           
            txtDatetime.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpPositionID.Enabled = false;
            txtPositionName.Enabled = false;
           
            txtDatetime.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.Tbl_Position_Mst.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID).OrderByDescending(m => m.PositionID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.Tbl_Position_Mst.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID).OrderByDescending(m => m.PositionID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.Tbl_Position_Mst.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID).OrderByDescending(m => m.PositionID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.Tbl_Position_Mst.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID).OrderByDescending(m => m.PositionID).Take(take).Skip(Skip)).ToList());
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
                        int PID = Convert.ToInt32(e.CommandArgument);
                        
                        Database.Tbl_Position_Mst objSOJobDesc = DB.Tbl_Position_Mst.Single(p => p.TenentID == TID && p.PositionID == PID);
                        objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Position_Mst.OrderBy(m => m.PositionID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int PID = Convert.ToInt32(e.CommandArgument);
                        
                        Database.Tbl_Position_Mst objTbl_Position_Mst = DB.Tbl_Position_Mst.Single(p => p.TenentID == TID && p.PositionID == PID);
                        //drpPositionID.SelectedValue = objTbl_Position_Mst.PositionID.ToString();
                        txtPositionName.Text = objTbl_Position_Mst.PositionName.ToString();
                        //cbActive.Checked = (objTbl_Position_Mst.Active == true) ? true : false;
                        //txtDatetime.Text = objTbl_Position_Mst.Datetime.ToString();

                        btnAdd.Text = "Update";
                       
                        ViewState["TID"] = TID;
                        ViewState["PID"] = PID;
                        btnAdd.ValidationGroup = "submit";
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
            int Totalrec = DB.Tbl_Position_Mst.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Position_Mst.Where(p => p.Active == true && p.TenentID == TID).OrderByDescending(m => m.PositionID).Take(Tvalue).Skip(Svalue)).ToList());
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