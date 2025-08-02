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
    public partial class TBLGROUP : System.Web.UI.Page
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

            }
        }
        #region Step2
        public void BindData()
        {
            //List<Database.TBLGROUP> List= DB.TBLGROUPs.Where(p=>p.ACTIVE == "1" && p.TenentID == TID).OrderBy(m => m.ITGROUPID).ToList();
            Listview1.DataSource = DB.TBLGROUPs.Where(p => p.ACTIVE == "1" && p.TenentID == TID).OrderBy(m => m.ITGROUPID).OrderByDescending(m=>m.ITGROUPID); 
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

             lblITGROUPDESC11s.Attributes["class"] = lblITGROUPDESC21s.Attributes["class"] = lblITGROUPREMARKS1s.Attributes["class"] = lblACTIVE_Flag1s.Attributes["class"] = lblUSERCODE1s.Attributes["class"] = lblGROUPID1s.Attributes["class"] = lblremarks1s.Attributes["class"] = lblACTIVE1s.Attributes["class"]  = lblGroupType1s.Attributes["class"] = "control-label col-md-4  getshow";
             lblITGROUPDESC12h.Attributes["class"] = lblITGROUPDESC22h.Attributes["class"] = lblITGROUPREMARKS2h.Attributes["class"] = lblACTIVE_Flag2h.Attributes["class"] = lblUSERCODE2h.Attributes["class"] = lblGROUPID2h.Attributes["class"] = lblremarks2h.Attributes["class"] = lblACTIVE2h.Attributes["class"]  = lblGroupType2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
             lblITGROUPDESC11s.Attributes["class"] = lblITGROUPDESC21s.Attributes["class"] = lblITGROUPREMARKS1s.Attributes["class"] = lblACTIVE_Flag1s.Attributes["class"] = lblUSERCODE1s.Attributes["class"] = lblGROUPID1s.Attributes["class"] = lblremarks1s.Attributes["class"] = lblACTIVE1s.Attributes["class"]  = lblGroupType1s.Attributes["class"] = "control-label col-md-4  gethide";
             lblITGROUPDESC12h.Attributes["class"] = lblITGROUPDESC22h.Attributes["class"] = lblITGROUPREMARKS2h.Attributes["class"] = lblACTIVE_Flag2h.Attributes["class"] = lblUSERCODE2h.Attributes["class"] = lblGROUPID2h.Attributes["class"] = lblremarks2h.Attributes["class"] = lblACTIVE2h.Attributes["class"] = lblGroupType2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtITGROUPDESC1.Text = "";
            txtITGROUPDESC2.Text = "";
            txtITGROUPREMARKS.Text = "";
            cbACTIVE_Flag.Checked = false;
            drpvaliduser.SelectedIndex = 0;
            txtGROUPID.Text = "";
            txtremarks.Text = "";
            cbACTIVE.Checked = false;
            //drpCRUP_ID.SelectedIndex = 0;
            txtGroupType.Text = "";

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
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        if (DB.TBLGROUPs.Where(p => p.ITGROUPDESC1.ToUpper() == txtITGROUPDESC1.Text.ToUpper() && p.TenentID == TID).Count() < 1)
                        {
                            Database.TBLGROUP objTBLGROUP = new Database.TBLGROUP();
                            //Server Content Send data Yogesh
                            objTBLGROUP.TenentID = TID;
                            objTBLGROUP.ITGROUPID = DB.TBLGROUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLGROUPs.Where(p => p.TenentID == TID).Max(p => p.ITGROUPID) + 1) : 1;
                            objTBLGROUP.ITGROUPDESC1 = txtITGROUPDESC1.Text;
                            objTBLGROUP.ITGROUPDESC2 = txtITGROUPDESC2.Text;
                            objTBLGROUP.ITGROUPREMARKS = txtITGROUPREMARKS.Text;
                            objTBLGROUP.ACTIVE_Flag = cbACTIVE_Flag.Checked == true ? true : false;
                            objTBLGROUP.USERCODE = drpvaliduser.SelectedValue;
                            objTBLGROUP.GROUPID = txtGROUPID.Text;
                            objTBLGROUP.remarks = txtremarks.Text;
                            objTBLGROUP.ACTIVE = "1";
                            objTBLGROUP.CRUP_ID = 1;
                            objTBLGROUP.GroupType = txtGroupType.Text;


                            DB.TBLGROUPs.AddObject(objTBLGROUP);
                            DB.SaveChanges();

                            String url = "insert new record in TBLGROUP with " + "TenentID = " + TID + "ITGROUPID =" + objTBLGROUP.ITGROUPID;
                            String evantname = "create";
                            String tablename = "TBLGROUP";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                            Clear();
                            btnAdd.Text = "Add New";
                            //lblMsg.Text = "Data Save Successfully";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
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
                            Database.TBLGROUP objTBLGROUP = DB.TBLGROUPs.Single(p => p.ITGROUPID == ID && p.TenentID==TID);
                            objTBLGROUP.ITGROUPDESC1 = txtITGROUPDESC1.Text;
                            objTBLGROUP.ITGROUPDESC2 = txtITGROUPDESC2.Text;
                            objTBLGROUP.ITGROUPREMARKS = txtITGROUPREMARKS.Text;
                            objTBLGROUP.ACTIVE_Flag = cbACTIVE_Flag.Checked;
                            objTBLGROUP.USERCODE = drpvaliduser.SelectedValue;
                            objTBLGROUP.GROUPID = txtGROUPID.Text;
                            objTBLGROUP.remarks = txtremarks.Text;
                            //objTBLGROUP.ACTIVE = cbACTIVE.Checked;
                            //objTBLGROUP.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            objTBLGROUP.GroupType = txtGroupType.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();

                            String url = "update TBLGROUP with " + "TenentID = " + TID + "ITGROUPID =" + ID;
                            String evantname = "update";
                            String tablename = "TBLGROUP";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
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
                catch (Exception ex)
                {
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Saving Data !<br>" + ex.ToString(), "Add", Classes.Toastr.ToastPosition.TopCenter);
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
            //drpGroupType.Items.Insert(0, new ListItem("-- Select --", "0"));drpGroupType.DataSource = DB.0;drpGroupType.DataTextField = "0";drpGroupType.DataValueField = "0";drpGroupType.DataBind();
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int AUID = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
            if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == AUID).Count() == 1)
            {
                int CompID = Convert.ToInt32(DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == AUID).CompId);
                drpvaliduser.DataSource = DB.USER_MST.Where(p => p.TenentID == TID && p.CompId == CompID);
                drpvaliduser.DataTextField = "FIRST_NAME";
                drpvaliduser.DataValueField = "USER_ID";
                drpvaliduser.DataBind();
                drpvaliduser.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
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
            txtITGROUPDESC1.Text = Listview1.SelectedDataKey[0] != null ? Listview1.SelectedDataKey[0].ToString() : "";
            txtITGROUPDESC2.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
            txtITGROUPREMARKS.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
            //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
            drpvaliduser.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
            txtGROUPID.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
            txtremarks.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            txtGroupType.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtITGROUPDESC1.Text = Listview1.SelectedDataKey[0] != null ? Listview1.SelectedDataKey[0].ToString() : "";
                txtITGROUPDESC2.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
                txtITGROUPREMARKS.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
                //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
                drpvaliduser.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                txtGROUPID.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtremarks.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                txtGroupType.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";

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
                txtITGROUPDESC1.Text = Listview1.SelectedDataKey[0] != null ? Listview1.SelectedDataKey[0].ToString() : "";
                txtITGROUPDESC2.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
                txtITGROUPREMARKS.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
                //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
                drpvaliduser.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                txtGROUPID.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtremarks.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                txtGroupType.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtITGROUPDESC1.Text = Listview1.SelectedDataKey[0] != null ? Listview1.SelectedDataKey[0].ToString() : "";
            txtITGROUPDESC2.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
            txtITGROUPREMARKS.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
            //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
            drpvaliduser.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
            txtGROUPID.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
            txtremarks.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            txtGroupType.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";

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
                     lblITGROUPDESC12h.Visible = lblITGROUPDESC22h.Visible = lblITGROUPREMARKS2h.Visible = lblACTIVE_Flag2h.Visible = lblUSERCODE2h.Visible = lblGROUPID2h.Visible = lblremarks2h.Visible = lblACTIVE2h.Visible =  lblGroupType2h.Visible = false;
                    //2true
                     txtITGROUPDESC12h.Visible = txtITGROUPDESC22h.Visible = txtITGROUPREMARKS2h.Visible = txtACTIVE_Flag2h.Visible = txtUSERCODE2h.Visible = txtGROUPID2h.Visible = txtremarks2h.Visible = txtACTIVE2h.Visible =  txtGroupType2h.Visible = true;

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
                    lblITGROUPDESC12h.Visible = lblITGROUPDESC22h.Visible = lblITGROUPREMARKS2h.Visible = lblACTIVE_Flag2h.Visible = lblUSERCODE2h.Visible = lblGROUPID2h.Visible = lblremarks2h.Visible = lblACTIVE2h.Visible = lblGroupType2h.Visible = true;
                    //2false
                    txtITGROUPDESC12h.Visible = txtITGROUPDESC22h.Visible = txtITGROUPREMARKS2h.Visible = txtACTIVE_Flag2h.Visible = txtUSERCODE2h.Visible = txtGROUPID2h.Visible = txtremarks2h.Visible = txtACTIVE2h.Visible = txtGroupType2h.Visible = false;

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
                    lblITGROUPDESC11s.Visible = lblITGROUPDESC21s.Visible = lblITGROUPREMARKS1s.Visible = lblACTIVE_Flag1s.Visible = lblUSERCODE1s.Visible = lblGROUPID1s.Visible = lblremarks1s.Visible = lblACTIVE1s.Visible =  lblGroupType1s.Visible = false;
                    //1true
                     txtITGROUPDESC11s.Visible = txtITGROUPDESC21s.Visible = txtITGROUPREMARKS1s.Visible = txtACTIVE_Flag1s.Visible = txtUSERCODE1s.Visible = txtGROUPID1s.Visible = txtremarks1s.Visible = txtACTIVE1s.Visible = txtGroupType1s.Visible = true;
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
                     lblITGROUPDESC11s.Visible = lblITGROUPDESC21s.Visible = lblITGROUPREMARKS1s.Visible = lblACTIVE_Flag1s.Visible = lblUSERCODE1s.Visible = lblGROUPID1s.Visible = lblremarks1s.Visible = lblACTIVE1s.Visible =  lblGroupType1s.Visible = true;
                    //1false
                     txtITGROUPDESC11s.Visible = txtITGROUPDESC21s.Visible = txtITGROUPREMARKS1s.Visible = txtACTIVE_Flag1s.Visible = txtUSERCODE1s.Visible = txtGROUPID1s.Visible = txtremarks1s.Visible = txtACTIVE1s.Visible =  txtGroupType1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLGROUP").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
               
                 if (lblITGROUPDESC11s.ID == item.LabelID)
                    txtITGROUPDESC11s.Text = lblITGROUPDESC11s.Text = lblhITGROUPDESC1.Text = item.LabelName;
                else if (lblITGROUPDESC21s.ID == item.LabelID)
                    txtITGROUPDESC21s.Text = lblITGROUPDESC21s.Text = lblhITGROUPDESC2.Text = item.LabelName;
                else if (lblITGROUPREMARKS1s.ID == item.LabelID)
                    txtITGROUPREMARKS1s.Text = lblITGROUPREMARKS1s.Text = lblhITGROUPREMARKS.Text = item.LabelName;
                else if (lblACTIVE_Flag1s.ID == item.LabelID)
                    txtACTIVE_Flag1s.Text = lblACTIVE_Flag1s.Text = item.LabelName;
                else if (lblUSERCODE1s.ID == item.LabelID)
                    txtUSERCODE1s.Text = lblUSERCODE1s.Text = item.LabelName;
                else if (lblGROUPID1s.ID == item.LabelID)
                    txtGROUPID1s.Text = lblGROUPID1s.Text = item.LabelName;
                else if (lblremarks1s.ID == item.LabelID)
                    txtremarks1s.Text = lblremarks1s.Text = item.LabelName;
                else if (lblACTIVE1s.ID == item.LabelID)
                    txtACTIVE1s.Text = lblACTIVE1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                else if (lblGroupType1s.ID == item.LabelID)
                    txtGroupType1s.Text = lblGroupType1s.Text = item.LabelName;

                //else if (lblITGROUPID2h.ID == item.LabelID)
                //    txtITGROUPID2h.Text = lblITGROUPID2h.Text = item.LabelName;
                else if (lblITGROUPDESC12h.ID == item.LabelID)
                    txtITGROUPDESC12h.Text = lblITGROUPDESC12h.Text = lblhITGROUPDESC1.Text = item.LabelName;
                else if (lblITGROUPDESC22h.ID == item.LabelID)
                    txtITGROUPDESC22h.Text = lblITGROUPDESC22h.Text = lblhITGROUPDESC2.Text = item.LabelName;
                else if (lblITGROUPREMARKS2h.ID == item.LabelID)
                    txtITGROUPREMARKS2h.Text = lblITGROUPREMARKS2h.Text = lblhITGROUPREMARKS.Text = item.LabelName;
                else if (lblACTIVE_Flag2h.ID == item.LabelID)
                    txtACTIVE_Flag2h.Text = lblACTIVE_Flag2h.Text = item.LabelName;
                else if (lblUSERCODE2h.ID == item.LabelID)
                    txtUSERCODE2h.Text = lblUSERCODE2h.Text = item.LabelName;
                else if (lblGROUPID2h.ID == item.LabelID)
                    txtGROUPID2h.Text = lblGROUPID2h.Text = item.LabelName;
                else if (lblremarks2h.ID == item.LabelID)
                    txtremarks2h.Text = lblremarks2h.Text = item.LabelName;
                else if (lblACTIVE2h.ID == item.LabelID)
                    txtACTIVE2h.Text = lblACTIVE2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;
                else if (lblGroupType2h.ID == item.LabelID)
                    txtGroupType2h.Text = lblGroupType2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLGROUP").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLGROUP.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLGROUP").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

               
                 if (lblITGROUPDESC11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPDESC11s.Text;
                else if (lblITGROUPDESC21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPDESC21s.Text;
                else if (lblITGROUPREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPREMARKS1s.Text;
                else if (lblACTIVE_Flag1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_Flag1s.Text;
                else if (lblUSERCODE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSERCODE1s.Text;
                else if (lblGROUPID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGROUPID1s.Text;
                else if (lblremarks1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtremarks1s.Text;
                else if (lblACTIVE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblGroupType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGroupType1s.Text;

                //else if (lblITGROUPID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPID2h.Text;
                else if (lblITGROUPDESC12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPDESC12h.Text;
                else if (lblITGROUPDESC22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPDESC22h.Text;
                else if (lblITGROUPREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPREMARKS2h.Text;
                else if (lblACTIVE_Flag2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_Flag2h.Text;
                else if (lblUSERCODE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSERCODE2h.Text;
                else if (lblGROUPID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGROUPID2h.Text;
                else if (lblremarks2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtremarks2h.Text;
                else if (lblACTIVE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                else if (lblGroupType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGroupType2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLGROUP.xml"));

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
            txtITGROUPDESC1.Enabled = true;
            txtITGROUPDESC2.Enabled = true;
            txtITGROUPREMARKS.Enabled = true;
            cbACTIVE_Flag.Enabled = true;
            drpvaliduser.Enabled = true;
            txtGROUPID.Enabled = true;
            txtremarks.Enabled = true;
            cbACTIVE.Enabled = true;
            //drpCRUP_ID.Enabled = true;
            txtGroupType.Enabled = true;
            //drpITGROUPID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtITGROUPDESC1.Enabled = false;
            txtITGROUPDESC2.Enabled = false;
            txtITGROUPREMARKS.Enabled = false;
            cbACTIVE_Flag.Enabled = false;
            drpvaliduser.Enabled = false;
            txtGROUPID.Enabled = false;
            txtremarks.Enabled = false;
            cbACTIVE.Enabled = false;
            //drpCRUP_ID.Enabled = false;
            txtGroupType.Enabled = false;
            //drpITGROUPID.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLGROUPs.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLGROUPs.OrderBy(m => m.ITGROUPID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.TBLGROUPs.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLGROUPs.OrderBy(m => m.ITGROUPID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.TBLGROUPs.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLGROUPs.OrderBy(m => m.ITGROUPID).Take(take).Skip(Skip)).ToList());
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
        //    int Totalrec = DB.TBLGROUPs.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLGROUPs.OrderBy(m => m.ITGROUPID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    if (e.CommandName == "btnDelete")
                    {

                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.TBLGROUP objSOJobDesc = DB.TBLGROUPs.Single(p => p.ITGROUPID == ID  &&  p.TenentID==TID);
                        objSOJobDesc.ACTIVE = "0";
                        DB.SaveChanges();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                       
                        String url = "delete TBLGROUP with " + "TenentID = " + TID + "ITGROUPID =" + ID;
                        String evantname = "delete";
                        String tablename = "TBLGROUP";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLGROUPs.OrderBy(m => m.ITGROUPID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.TBLGROUP objTBLGROUP = DB.TBLGROUPs.Single(p => p.ITGROUPID == ID && p.TenentID==TID);
                        if(objTBLGROUP.ITGROUPDESC1 != null)
                        txtITGROUPDESC1.Text = objTBLGROUP.ITGROUPDESC1.ToString();
                        if (objTBLGROUP.ITGROUPDESC2 != null)
                        txtITGROUPDESC2.Text = objTBLGROUP.ITGROUPDESC2.ToString();
                        if (objTBLGROUP.ITGROUPREMARKS != null)
                        txtITGROUPREMARKS.Text = objTBLGROUP.ITGROUPREMARKS.ToString();
                        cbACTIVE_Flag.Checked = (objTBLGROUP.ACTIVE_Flag == true) ? true : false;
                        if (objTBLGROUP.USERCODE != null)
                        drpvaliduser.SelectedValue = objTBLGROUP.USERCODE.ToString();
                        if (objTBLGROUP.GROUPID != null)
                        txtGROUPID.Text = objTBLGROUP.GROUPID.ToString();
                        if (objTBLGROUP.remarks != null)
                        txtremarks.Text = objTBLGROUP.remarks.ToString();
                        //txtACTIVE.Text = objTBLGROUP.ACTIVE.ToString();
                        //txtCRUP_ID.Text = objTBLGROUP.CRUP_ID.ToString();
                        txtGroupType.Text = objTBLGROUP.GroupType.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        Write();
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

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLGROUPs.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLGROUPs.OrderBy(m => m.ITGROUPID).Take(Tvalue).Skip(Svalue)).ToList());
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

    }
}