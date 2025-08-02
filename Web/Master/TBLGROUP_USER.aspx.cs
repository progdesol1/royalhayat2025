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
    public partial class TBLGROUP_USER : System.Web.UI.Page
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
            PanelErrorMsg.Visible = false;
            lblErrormsg.Text = "";
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Readonly();
                Read_User();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                FirstData();

            }
        }

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
        #region Step2
        public void BindData()
        {
            Listview1.DataSource = DB.TBLGROUPs.Where(p => p.TenentID == TID && p.LocationId == LID && p.ACTIVE == "1").OrderByDescending(p => p.ITGROUPID);
            Listview1.DataBind();

            Listview2.DataSource = DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.LocationId == LID && p.ACTIVE == "1").OrderByDescending(p => p.ITGROUPID);
            Listview2.DataBind();
            //List<Database.TBLGROUP> List = DB.TBLGROUPs.ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {
            lblGroupname1s.Attributes["class"] = lblUSERCODE1s.Attributes["class"] = lblInfastructure1s.Attributes["class"] = lblGroupname11s.Attributes["class"] = "control-label col-md-4  getshow";
            //lblTenentId1s.Attributes["class"] =lblLocationId1s.Attributes["class"] =lblITGROUPID1s.Attributes["class"] =lblGroupType1s.Attributes["class"] =lblACTIVE_Flag1s.Attributes["class"] =lblGROUPID1s.Attributes["class"] =lblremarks1s.Attributes["class"] =lblACTIVE1s.Attributes["class"] =lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  getshow" ;
            lblTenentId2h.Attributes["class"] = lblLocationId2h.Attributes["class"] = lblGroupname2h.Attributes["class"] = lblUSERCODE2h.Attributes["class"] = lblInfastructure2h.Attributes["class"] = lblGroupname12h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblGroupname1s.Attributes["class"] = lblUSERCODE1s.Attributes["class"] = lblInfastructure1s.Attributes["class"] = lblGroupname11s.Attributes["class"] = "control-label col-md-4  gethide";
            //lblTenentId1s.Attributes["class"] =lblLocationId1s.Attributes["class"] =lblITGROUPID1s.Attributes["class"] =lblGroupType1s.Attributes["class"] =lblACTIVE_Flag1s.Attributes["class"] =lblGROUPID1s.Attributes["class"] =lblremarks1s.Attributes["class"] =lblACTIVE1s.Attributes["class"] =lblCRUP_ID1s.Attributes["class"] ="control-label col-md-4  gethide";
            lblTenentId2h.Attributes["class"] = lblLocationId2h.Attributes["class"] = lblGroupname2h.Attributes["class"] = lblUSERCODE2h.Attributes["class"] = lblInfastructure2h.Attributes["class"] = lblGroupname12h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpLocationId.SelectedIndex = 0;
            // drpITGROUPID.SelectedIndex = 0;
            //txtGroupType.Text = "";
            txtGroupname.Text = "";
            //txtACTIVE_Flag.Text = "";
            //txtUSERCODE.Text = "";
            //txtGROUPID.Text = "";
            //txtremarks.Text = "";
            //txtACTIVE.Text = "";
            //txtCRUP_ID.Text = "";
            //txtInfastructure.Text = "";
            //txtGroupname1.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PanelErrorMsg.Visible = false;
            lblErrormsg.Text = "";
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (btnAdd.Text == "AddNew")
                {

                    Write();
                    Clear();
                    btnAdd.Text = "Add";
                    btnAdd.ValidationGroup = "s";
                }
                else if (btnAdd.Text == "Add")
                {
                    if (txtGroupname.Text != "")
                    {
                        string groupname = txtGroupname.Text;
                        if (DB.TBLGROUPs.Where(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPDESC1.ToUpper() == groupname.ToUpper()).Count() > 0)
                        {
                            PanelErrorMsg.Visible = true;
                            lblErrormsg.Text = "Group Name Already inserted";
                            return;
                        }
                    }
                    else
                    {
                        PanelErrorMsg.Visible = true;
                        lblErrormsg.Text = "plase Enter Group Name";
                        return;
                    }

                    Database.TBLGROUP objTBLGROUP = new Database.TBLGROUP();

                    int ITGROUPID = DB.TBLGROUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLGROUPs.Where(p => p.TenentID == TID).Max(p => p.ITGROUPID) + 1) : 1;
                    //Server Content Send data Yogesh
                    objTBLGROUP.TenentID = TID;
                    objTBLGROUP.LocationId = LID;
                    objTBLGROUP.ITGROUPID = ITGROUPID;
                    objTBLGROUP.ITGROUPDESC1 = txtGroupname.Text;
                    objTBLGROUP.ITGROUPDESC2 = txtGroupname.Text;
                    objTBLGROUP.ITGROUPREMARKS = txtGroupname.Text;
                    objTBLGROUP.ACTIVE_Flag = true;
                    objTBLGROUP.GROUPID = ITGROUPID.ToString();
                    objTBLGROUP.ACTIVE = "1";
                    objTBLGROUP.CRUP_ID = 1;
                    objTBLGROUP.Infastructure = cbInfastructure.Checked == true ? true : false;

                    DB.TBLGROUPs.AddObject(objTBLGROUP);
                    DB.SaveChanges();

                    Clear();
                    lblMsg.Text = "  Data Save Successfully";
                    btnAdd.Text = "AddNew";
                    btnAdd.ValidationGroup = "submit";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //navigation.Visible = true;
                    Readonly();
                    Read_User();
                    FirstData();
                }
                else if (btnAdd.Text == "Update")
                {

                    if (ViewState["Edit"] != null)
                    {
                        int ID = Convert.ToInt32(ViewState["Edit"]);

                        string groupname = txtGroupname.Text;
                        if (DB.TBLGROUPs.Where(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID != ID && p.ITGROUPDESC1.ToUpper() == groupname.ToUpper()).Count() > 0)
                        {
                            PanelErrorMsg.Visible = true;
                            lblErrormsg.Text = "Group Name Already inserted";
                            return;
                        }

                        Database.TBLGROUP objTBLGROUP = DB.TBLGROUPs.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ID);

                        objTBLGROUP.ITGROUPDESC1 = txtGroupname.Text;
                        objTBLGROUP.ITGROUPDESC2 = txtGroupname.Text;
                        objTBLGROUP.ITGROUPREMARKS = txtGroupname.Text;
                        objTBLGROUP.ACTIVE_Flag = true;
                        objTBLGROUP.Infastructure = cbInfastructure.Checked == true ? true : false;
                        DB.SaveChanges();
                        ViewState["Edit"] = null;
                        btnAdd.Text = "AddNew";
                        btnAdd.ValidationGroup = "submit";

                        Clear();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        Read_User();
                        FirstData();
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

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            PanelErrorMsg.Visible = false;
            lblErrormsg.Text = "";
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (btnAddUser.Text == "AddNew")
                {

                    //Write();
                    Write_User();
                    Clear();
                    btnAddUser.Text = "Add";
                    btnAddUser.ValidationGroup = "s1";
                }
                else if (btnAddUser.Text == "Add")
                {
                    int ITGROUPID = Convert.ToInt32(drpGroupname1.SelectedValue);
                    string UserCode=drpUSERCODE.SelectedValue;
                    if(DB.TBLGROUP_USER.Where(p=>p.TenentID==TID && p.LocationId==LID && p.ITGROUPID==ITGROUPID && p.USERCODE==UserCode).Count()<1)
                    {
                        Database.TBLGROUP_USER objTBLGROUP_USER = new Database.TBLGROUP_USER();

                        // ITGROUPID = DB.TBLGROUPs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLGROUPs.Where(p => p.TenentID == TID).Max(p => p.ITGROUPID) + 1) : 1;
                        //Server Content Send data Yogesh
                        objTBLGROUP_USER.TenentID = TID;
                        objTBLGROUP_USER.LocationId = LID;
                        objTBLGROUP_USER.ITGROUPID = ITGROUPID;
                        objTBLGROUP_USER.USERCODE = drpUSERCODE.SelectedValue;
                        objTBLGROUP_USER.ACTIVE_Flag = true;
                        objTBLGROUP_USER.GROUPID = ITGROUPID.ToString();
                        objTBLGROUP_USER.ACTIVE = "1";
                        objTBLGROUP_USER.CRUP_ID = 1;

                        DB.TBLGROUP_USER.AddObject(objTBLGROUP_USER);
                        DB.SaveChanges();
                    }
                    else
                    {
                        Database.TBLGROUP_USER objTBLGROUP_USER = DB.TBLGROUP_USER.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ITGROUPID && p.USERCODE == UserCode);
                        objTBLGROUP_USER.ACTIVE = "1";
                        DB.SaveChanges();
                    }
                   

                    Clear();
                    lblMsg.Text = "  Data Save Successfully";
                    btnAddUser.Text = "AddNew";
                    btnAddUser.ValidationGroup = "submit";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //navigation.Visible = true;
                    Readonly();
                    Read_User();
                    FirstData();
                }
                else if (btnAddUser.Text == "Update")
                {
                    int ITGROUPID = Convert.ToInt32(drpGroupname1.SelectedValue);


                    if (ViewState["EditGroup"] != null && ViewState["EditUser"] != null)
                    {
                        int ID = Convert.ToInt32(ViewState["EditGroup"]);
                        string UserCode = ViewState["EditUser"].ToString();
                        Database.TBLGROUP_USER objTBLGROUP_USER = DB.TBLGROUP_USER.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ID && p.USERCODE == UserCode);

                        objTBLGROUP_USER.ITGROUPID = ITGROUPID;
                        objTBLGROUP_USER.GROUPID = ITGROUPID.ToString();
                        objTBLGROUP_USER.USERCODE = drpUSERCODE.SelectedValue;

                        DB.SaveChanges();
                        ViewState["EditGroup"] = null;
                        ViewState["EditUser"] = null;
                        btnAddUser.Text = "AddNew";
                        btnAddUser.ValidationGroup = "submit";

                        Clear();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        Read_User();
                        FirstData();
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
        protected void BtnCancelUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            drpGroupname1.DataSource = DB.TBLGROUPs.Where(p => p.TenentID == TID && p.ACTIVE == "1");
            drpGroupname1.DataTextField = "ITGROUPDESC1";
            drpGroupname1.DataValueField = "ITGROUPID";
            drpGroupname1.DataBind();
            drpGroupname1.Items.Insert(0, new ListItem("-- Select --", "0"));
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
            //drpLocationId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpITGROUPID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtGroupType.Text = Listview1.SelectedDataKey[0].ToString();
            //txtGroupname.Text = Listview1.SelectedDataKey[0].ToString();
            //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtUSERCODE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtGROUPID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtremarks.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //cbInfastructure.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtGroupname1.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpLocationId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpITGROUPID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtGroupType.Text = Listview1.SelectedDataKey[0].ToString();
                //txtGroupname.Text = Listview1.SelectedDataKey[0].ToString();
                //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtUSERCODE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtGROUPID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtremarks.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //cbInfastructure.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtGroupname1.Text = Listview1.SelectedDataKey[0].ToString();

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
                //drpLocationId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpITGROUPID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtGroupType.Text = Listview1.SelectedDataKey[0].ToString();
                //txtGroupname.Text = Listview1.SelectedDataKey[0].ToString();
                //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtUSERCODE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtGROUPID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtremarks.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //cbInfastructure.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtGroupname1.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpLocationId.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpITGROUPID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtGroupType.Text = Listview1.SelectedDataKey[0].ToString();
            //txtGroupname.Text = Listview1.SelectedDataKey[0].ToString();
            //cbACTIVE_Flag.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtUSERCODE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtGROUPID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtremarks.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //cbInfastructure.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtGroupname1.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblTenentId2h.Visible = lblLocationId2h.Visible = lblGroupname2h.Visible = lblUSERCODE2h.Visible = lblInfastructure2h.Visible = lblGroupname12h.Visible = false;
                    //2true
                    txtTenentId2h.Visible = txtLocationId2h.Visible = txtGroupname2h.Visible = txtUSERCODE2h.Visible = txtInfastructure2h.Visible = txtGroupname12h.Visible = true;

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
                    lblTenentId2h.Visible = lblLocationId2h.Visible = lblGroupname2h.Visible = lblUSERCODE2h.Visible = lblInfastructure2h.Visible = lblGroupname12h.Visible = true;
                    //2false
                    txtTenentId2h.Visible = txtLocationId2h.Visible = txtGroupname2h.Visible = txtUSERCODE2h.Visible = txtInfastructure2h.Visible = txtGroupname12h.Visible = false;

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
                    lblGroupname1s.Visible = lblUSERCODE1s.Visible = lblInfastructure1s.Visible = lblGroupname11s.Visible = false;
                    //1true
                    txtGroupname1s.Visible = txtUSERCODE1s.Visible = txtInfastructure1s.Visible = txtGroupname11s.Visible = true;
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
                    lblGroupname1s.Visible = lblUSERCODE1s.Visible = lblInfastructure1s.Visible = lblGroupname11s.Visible = true;
                    //1false
                    txtGroupname1s.Visible = txtUSERCODE1s.Visible = txtInfastructure1s.Visible = txtGroupname11s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLGROUP_USER").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenentId1s.ID == item.LabelID)
                //    txtTenentId1s.Text = lblTenentId1s.Text = lblhTenentId.Text = item.LabelName;
                //else if (lblLocationId1s.ID == item.LabelID)
                //    txtLocationId1s.Text = lblLocationId1s.Text = lblhLocationId.Text = item.LabelName;
                //else if (lblITGROUPID1s.ID == item.LabelID)
                //    txtITGROUPID1s.Text = lblITGROUPID1s.Text = lblhITGROUPID.Text = item.LabelName;
                //else if (lblGroupType1s.ID == item.LabelID)
                //    txtGroupType1s.Text = lblGroupType1s.Text = lblhGroupType.Text = item.LabelName;
                if (lblGroupname1s.ID == item.LabelID)
                    txtGroupname1s.Text = lblGroupname1s.Text = lblhGroupname.Text = item.LabelName;
                //else if (lblACTIVE_Flag1s.ID == item.LabelID)
                //    txtACTIVE_Flag1s.Text = lblACTIVE_Flag1s.Text = lblhACTIVE_Flag.Text = item.LabelName;
                else if (lblUSERCODE1s.ID == item.LabelID)
                    txtUSERCODE1s.Text = lblUSERCODE1s.Text = item.LabelName;//lblhUSERCODE.Text =
                //else if (lblGROUPID1s.ID == item.LabelID)
                //    txtGROUPID1s.Text = lblGROUPID1s.Text = lblhGROUPID.Text = item.LabelName;
                //else if (lblremarks1s.ID == item.LabelID)
                //    txtremarks1s.Text = lblremarks1s.Text = lblhremarks.Text = item.LabelName;
                //else if (lblACTIVE1s.ID == item.LabelID)
                //    txtACTIVE1s.Text = lblACTIVE1s.Text = lblhACTIVE.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblInfastructure1s.ID == item.LabelID)
                    txtInfastructure1s.Text = lblInfastructure1s.Text = lblhInfastructure.Text = item.LabelName;
                else if (lblGroupname11s.ID == item.LabelID)
                    txtGroupname11s.Text = lblGroupname11s.Text = item.LabelName;//lblhGroupname1.Text =

                else if (lblTenentId2h.ID == item.LabelID)
                    txtTenentId2h.Text = lblTenentId2h.Text = item.LabelName;
                else if (lblLocationId2h.ID == item.LabelID)
                    txtLocationId2h.Text = lblLocationId2h.Text = item.LabelName;
                else if (lblGroupname2h.ID == item.LabelID)
                    txtGroupname2h.Text = lblGroupname2h.Text = lblhGroupname.Text = item.LabelName;
                else if (lblUSERCODE2h.ID == item.LabelID)
                    txtUSERCODE2h.Text = lblUSERCODE2h.Text = item.LabelName;//lblhUSERCODE.Text =               
                else if (lblInfastructure2h.ID == item.LabelID)
                    txtInfastructure2h.Text = lblInfastructure2h.Text = lblhInfastructure.Text = item.LabelName;
                else if (lblGroupname12h.ID == item.LabelID)
                    txtGroupname12h.Text = lblGroupname12h.Text = item.LabelName;//lblhGroupname1.Text = 

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLGROUP_USER").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLGROUP_USER.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLGROUP_USER").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenentId1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenentId1s.Text;
                //else if (lblLocationId1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLocationId1s.Text;
                //else if (lblITGROUPID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtITGROUPID1s.Text;
                //else if (lblGroupType1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtGroupType1s.Text;
                if (lblGroupname1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGroupname1s.Text;
                //else if (lblACTIVE_Flag1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_Flag1s.Text;
                else if (lblUSERCODE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSERCODE1s.Text;
                //else if (lblGROUPID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtGROUPID1s.Text;
                //else if (lblremarks1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtremarks1s.Text;
                //else if (lblACTIVE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblInfastructure1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInfastructure1s.Text;
                else if (lblGroupname11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGroupname11s.Text;

                else if (lblTenentId2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTenentId2h.Text;
                else if (lblLocationId2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLocationId2h.Text;

                else if (lblGroupname2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGroupname2h.Text;

                else if (lblUSERCODE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSERCODE2h.Text;
                else if (lblInfastructure2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInfastructure2h.Text;
                else if (lblGroupname12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtGroupname12h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLGROUP_USER.xml"));

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
            //drpLocationId.Enabled = true;
            //drpITGROUPID.Enabled = true;
            //txtGroupType.Enabled = true;
            txtGroupname.Enabled = true;
            //cbACTIVE_Flag.Enabled = true;
            //txtUSERCODE.Enabled = true;
            //txtGROUPID.Enabled = true;
            //txtremarks.Enabled = true;
            //txtACTIVE.Enabled = true;
            //txtCRUP_ID.Enabled = true;
            cbInfastructure.Enabled = true;
            //txtGroupname1.Enabled = true;

        }

        public void Write_User()
        {
            drpGroupname1.Enabled = true;
            drpUSERCODE.Enabled = true;
        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpLocationId.Enabled = false;
            //drpITGROUPID.Enabled = false;
            //txtGroupType.Enabled = false;
            txtGroupname.Enabled = false;
            //cbACTIVE_Flag.Enabled = false;
            //txtUSERCODE.Enabled = false;
            //txtGROUPID.Enabled = false;
            //txtremarks.Enabled = false;
            //txtACTIVE.Enabled = false;
            //txtCRUP_ID.Enabled = false;
            cbInfastructure.Enabled = false;
            //txtGroupname1.Enabled = false;
        }

        public void Read_User()
        {
            drpGroupname1.Enabled = false;
            drpUSERCODE.Enabled = false;
        }

        #region Listview


        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            Readonly();
            Read_User();
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
                        List<Database.TBLGROUP> ListGroup = DB.TBLGROUPs.Where(p => p.TenentID == TID && p.LocationId == LID).ToList();

                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.TBLGROUP ObjGroup = ListGroup.Single(p => p.ITGROUPID == ID);
                        if (ObjGroup.Infastructure != true)
                        {
                            ObjGroup.ACTIVE = "0";
                            ObjGroup.ACTIVE_Flag = false;

                        }
                        else
                        {
                            PanelErrorMsg.Visible = true;
                            lblErrormsg.Text = "This group is not delete Plase contack to admin ";
                        }
                        DB.SaveChanges();

                        BindData();

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.TBLGROUP onjgroup = DB.TBLGROUPs.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ID);
                        txtGroupname.Text = onjgroup.ITGROUPDESC1;
                        cbInfastructure.Checked = onjgroup.Infastructure == true ? true : false;


                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
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

        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    if (e.CommandName == "btnDelete")
                    {
                        List<Database.TBLGROUP_USER> ListGroup_USER = DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.LocationId == LID).ToList();

                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int ITGROUPID = Convert.ToInt32(ID[0]);
                        string UserCode = ID[1].ToString();

                        Database.TBLGROUP_USER ObjGroup = ListGroup_USER.Single(p => p.ITGROUPID == ITGROUPID && p.USERCODE == UserCode);
                        ObjGroup.ACTIVE = "0";
                        ObjGroup.ACTIVE_Flag = false;
                        DB.SaveChanges();

                        BindData();

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int ITGROUPID = Convert.ToInt32(ID[0]);
                        string UserCode = ID[1].ToString();

                        Database.TBLGROUP_USER onjgroup_user = DB.TBLGROUP_USER.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ITGROUPID && p.USERCODE == UserCode);

                        drpGroupname1.SelectedValue = onjgroup_user.ITGROUPID.ToString();
                        drpUSERCODE.SelectedValue = onjgroup_user.USERCODE;

                        btnAddUser.Text = "Update";
                        ViewState["EditGroup"] = ITGROUPID;
                        ViewState["EditUser"] = UserCode;
                        //Write();
                        Write_User();
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

        public string getUsername(int UserCode)
        {
            if (DB.USER_MST.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.USER_ID == UserCode).Count() > 0)
            {
                return DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UserCode).FIRST_NAME;
            }
            else
            {
                return "Not Found";
            }
        }

        public string getgroupName(int group)
        {
            if (DB.TBLGROUPs.Where(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == group).Count() > 0)
            {
                return DB.TBLGROUPs.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == group).ITGROUPDESC1;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void Listview2_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    if (e.CommandName == "btnDelete")
                    {
                        List<Database.TBLGROUP_USER> ListGroup_USER = DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.LocationId == LID).ToList();

                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int ITGROUPID = Convert.ToInt32(ID[0]);
                        string UserCode = ID[1].ToString();

                        Database.TBLGROUP_USER ObjGroup = ListGroup_USER.Single(p => p.ITGROUPID == ITGROUPID && p.USERCODE == UserCode);
                        ObjGroup.ACTIVE = "0";
                        ObjGroup.ACTIVE_Flag = false;
                        DB.SaveChanges();

                        BindData();

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int ITGROUPID = Convert.ToInt32(ID[0]);
                        string UserCode = ID[1].ToString();

                        Database.TBLGROUP_USER onjgroup_user = DB.TBLGROUP_USER.Single(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ITGROUPID && p.USERCODE == UserCode);

                        drpGroupname1.SelectedValue = onjgroup_user.ITGROUPID.ToString();
                        drpUSERCODE.SelectedValue = onjgroup_user.USERCODE;

                        btnAdd.Text = "Update";
                        ViewState["EditGroup"] = ITGROUPID;
                        ViewState["EditUser"] = UserCode;
                        Write();
                    }
                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(source as Control, this.GetType(), "alert", ex.Message, true);
                    throw;
                }
            }
        }

        protected void drpGroupname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ITGROUPID = Convert.ToInt32(drpGroupname1.SelectedValue);
            if (drpGroupname1.SelectedValue != "0")
            {
                List<Database.USER_MST> Listuser = DB.USER_MST.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.ACTIVEUSER == true).ToList();
                List<Database.TBLGROUP_USER> ListGropUser = DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.LocationId == LID && p.ITGROUPID == ITGROUPID).ToList();
                List<Database.USER_MST> ListFinal = new List<USER_MST>();

                foreach (Database.USER_MST items in Listuser)
                {
                    string UID1=items.USER_ID.ToString();

                    List<Database.TBLGROUP_USER> Listguser = ListGropUser.Where(p => p.USERCODE == UID1 && p.ACTIVE == "1").ToList();
                    
                    if(Listguser.Count()<1)
                    {
                        ListFinal.Add(items);
                    }
                }

                drpUSERCODE.DataSource = ListFinal;
                drpUSERCODE.DataTextField = "FIRST_NAME";
                drpUSERCODE.DataValueField = "USER_ID";
                drpUSERCODE.DataBind();
            }

        }



    }
}