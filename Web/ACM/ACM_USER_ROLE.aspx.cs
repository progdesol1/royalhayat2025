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

namespace Web.ACM
{
    public partial class ACM_USER_ROLE : System.Web.UI.Page
    {
        int TENANT_ID = 0;
        int LOCATIONID = 0;
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

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
                if (DB.USER_ROLE.Count() > 0)
                {
                    //  FirstData();
                }
            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            List<Database.USER_ROLE> List = DB.USER_ROLE.Where(p => p.TenentID == TID && p.LOCATION_ID == LID).OrderBy(m => m.USER_ROLE_ID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator

        public void GetShow()
        {

            lblPRIVILEGE_ID1s.Attributes["class"] = lblUSER_ID1s.Attributes["class"] = lblROLE_ID1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblACTIVE_FROM_DT1s.Attributes["class"] = lblACTIVE_TO_DT1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPRIVILEGE_ID2h.Attributes["class"] = lblUSER_ID2h.Attributes["class"] = lblROLE_ID2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblACTIVE_FROM_DT2h.Attributes["class"] = lblACTIVE_TO_DT2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPRIVILEGE_ID1s.Attributes["class"] = lblUSER_ID1s.Attributes["class"] = lblROLE_ID1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblACTIVE_FROM_DT1s.Attributes["class"] = lblACTIVE_TO_DT1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPRIVILEGE_ID2h.Attributes["class"] = lblUSER_ID2h.Attributes["class"] = lblROLE_ID2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblACTIVE_FROM_DT2h.Attributes["class"] = lblACTIVE_TO_DT2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            // drpUSER_ROLE_ID.SelectedIndex = 0;
            drpPRIVILEGE_ID.SelectedIndex = 0;
            drpUSER_ID.SelectedIndex = 0;
            drpROLE_ID.SelectedIndex = 0;
            cbACTIVE_FLAG.Checked = false;
            txtACTIVE_FROM_DT.Text = "";
            txtACTIVE_TO_DT.Text = "";
            //txtCRUP_ID.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TENANT_ID = 0;
                int LOCATION_ID = 0;
                int USER_ROLE_ID = 0;
                int PRIVILEGE_ID = 0;
                int USER_ID = 0;
                int ROLE_ID = 0;
                string ACTIVE_FLAG = "";
                DateTime ACTIVE_FROM_DT = DateTime.Now;
                DateTime ACTIVE_TO_DT = DateTime.Now;
                int CRUP_ID = 0;
                try
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                    }
                    else if (btnAdd.Text == "Add")
                    {

                        //Server Content Send data Yogesh
                        //   TENANT_ID = TID;
                        //  LOCATION_ID = LID;
                        // USER_ROLE_ID = DB.USER_ROLE.Count() > 0 ? Convert.ToInt32(DB.USER_ROLE.Max(p => p.USER_ROLE_ID) + 1) : 1;
                        if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
                        {
                            TENANT_ID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drplocation.SelectedValue) != 0)
                        {
                            LOCATION_ID = Convert.ToInt32(drplocation.SelectedValue);
                        }
                        if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                        {
                            PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drpUSER_ID.SelectedValue) != 0)
                        {
                            USER_ID = Convert.ToInt32(drpUSER_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drpROLE_ID.SelectedValue) != 0)
                        {
                            ROLE_ID = Convert.ToInt32(drpROLE_ID.SelectedValue);
                        }
                        ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                        if (txtACTIVE_FROM_DT.Text != string.Empty)
                        {
                            ACTIVE_FROM_DT = Convert.ToDateTime(txtACTIVE_FROM_DT.Text);
                        }
                        if (txtACTIVE_FROM_DT.Text != string.Empty)
                        {
                            ACTIVE_TO_DT = Convert.ToDateTime(txtACTIVE_TO_DT.Text);
                        }

                        //objUSER_ROLE.CRUP_ID = txtCRUP_ID.Text;
                        Classes.ACMClass.InsertDataACMUSERROLE(TENANT_ID, LOCATION_ID, PRIVILEGE_ID, USER_ID, ROLE_ID, USER_ROLE_ID, ACTIVE_FROM_DT, ACTIVE_TO_DT, CRUP_ID, ACTIVE_FLAG);
                        // Clear();
                        btnAdd.Text = "AddNew";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        //  BindData();
                        navigation.Visible = true;
                        Readonly();
                        //   FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            USER_ROLE_ID = Convert.ToInt32(ViewState["Edit"]);

                            TENANT_ID = TID;
                            LOCATION_ID = LID;
                            // objUSER_ROLE.USER_ROLE_ID = Convert.ToInt32(drpUSER_ROLE_ID.SelectedValue);
                            if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                            {
                                PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                            }
                            if (Convert.ToInt32(drpUSER_ID.SelectedValue) != 0)
                            {
                                USER_ID = Convert.ToInt32(drpUSER_ID.SelectedValue);
                            }
                            if (Convert.ToInt32(drpROLE_ID.SelectedValue) != 0)
                            {
                                ROLE_ID = Convert.ToInt32(drpROLE_ID.SelectedValue);
                            }
                            ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            if (txtACTIVE_FROM_DT.Text != string.Empty)
                            {
                                ACTIVE_FROM_DT = Convert.ToDateTime(txtACTIVE_FROM_DT.Text);
                            }
                            if (txtACTIVE_TO_DT.Text != string.Empty)
                            {
                                ACTIVE_TO_DT = Convert.ToDateTime(txtACTIVE_TO_DT.Text);
                            }
                            //objUSER_ROLE.CRUP_ID = txtCRUP_ID.Text;
                            Classes.ACMClass.InsertDataACMUSERROLE(TENANT_ID, LOCATIONID, PRIVILEGE_ID, USER_ID, ROLE_ID, USER_ROLE_ID, ACTIVE_FROM_DT, ACTIVE_TO_DT, CRUP_ID, ACTIVE_FLAG);
                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            // Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            //  BindData();
                            navigation.Visible = true;
                            Readonly();
                            //  FirstData();
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

            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            var Datas = (from fun1 in DB.TBLLOCATIONs
                         select new
                         {
                             fun1.TenentID,

                         }
        ).Distinct();
            DrpTENANT_ID.DataSource = Datas;
            DrpTENANT_ID.DataTextField = "TenentID";
            DrpTENANT_ID.DataValueField = "TenentID";
            DrpTENANT_ID.DataBind();
            DrpTENANT_ID.Items.Insert(0, new ListItem("---Select---", "00"));
            drpUSER_ID.Items.Insert(0, new ListItem("---Select---", "0"));
            drpPRIVILEGE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Privilege--", "0"));
            drpROLE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Role--", "0"));
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
            //  drpUSER_ROLE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            drpROLE_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            txtACTIVE_FROM_DT.Text = Listview1.SelectedDataKey[6].ToString();
            txtACTIVE_TO_DT.Text = Listview1.SelectedDataKey[7].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[8].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //  drpUSER_ROLE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                drpROLE_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
                txtACTIVE_FROM_DT.Text = Listview1.SelectedDataKey[6].ToString();
                txtACTIVE_TO_DT.Text = Listview1.SelectedDataKey[7].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[8].ToString();

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
                //  drpUSER_ROLE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                drpROLE_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
                txtACTIVE_FROM_DT.Text = Listview1.SelectedDataKey[6].ToString();
                txtACTIVE_TO_DT.Text = Listview1.SelectedDataKey[7].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[8].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //  drpUSER_ROLE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            drpROLE_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            txtACTIVE_FROM_DT.Text = Listview1.SelectedDataKey[6].ToString();
            txtACTIVE_TO_DT.Text = Listview1.SelectedDataKey[7].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[8].ToString();

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
                    lblPRIVILEGE_ID2h.Visible = lblUSER_ID2h.Visible = lblROLE_ID2h.Visible = lblACTIVE_FLAG2h.Visible = lblACTIVE_FROM_DT2h.Visible = lblACTIVE_TO_DT2h.Visible = false;
                    //2true
                    txtPRIVILEGE_ID2h.Visible = txtUSER_ID2h.Visible = txtROLE_ID2h.Visible = txtACTIVE_FLAG2h.Visible = txtACTIVE_FROM_DT2h.Visible = txtACTIVE_TO_DT2h.Visible = true;

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
                    lblPRIVILEGE_ID2h.Visible = lblUSER_ID2h.Visible = lblROLE_ID2h.Visible = lblACTIVE_FLAG2h.Visible = lblACTIVE_FROM_DT2h.Visible = lblACTIVE_TO_DT2h.Visible = true;
                    //2false
                    txtPRIVILEGE_ID2h.Visible = txtUSER_ID2h.Visible = txtROLE_ID2h.Visible = txtACTIVE_FLAG2h.Visible = txtACTIVE_FROM_DT2h.Visible = txtACTIVE_TO_DT2h.Visible = false;

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
                    lblPRIVILEGE_ID1s.Visible = lblUSER_ID1s.Visible = lblROLE_ID1s.Visible = lblACTIVE_FLAG1s.Visible = lblACTIVE_FROM_DT1s.Visible = lblACTIVE_TO_DT1s.Visible = false;
                    //1true
                    txtPRIVILEGE_ID1s.Visible = txtUSER_ID1s.Visible = txtROLE_ID1s.Visible = txtACTIVE_FLAG1s.Visible = txtACTIVE_FROM_DT1s.Visible = txtACTIVE_TO_DT1s.Visible = true;
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
                    lblPRIVILEGE_ID1s.Visible = lblUSER_ID1s.Visible = lblROLE_ID1s.Visible = lblACTIVE_FLAG1s.Visible = lblACTIVE_FROM_DT1s.Visible = lblACTIVE_TO_DT1s.Visible = true;
                    //1false
                    txtPRIVILEGE_ID1s.Visible = txtUSER_ID1s.Visible = txtROLE_ID1s.Visible = txtACTIVE_FLAG1s.Visible = txtACTIVE_FROM_DT1s.Visible = txtACTIVE_TO_DT1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((ACMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_USER_ROLE").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = lblhTENANT_ID.Text = item.LabelName;
                if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    txtPRIVILEGE_ID1s.Text = lblPRIVILEGE_ID1s.Text = item.LabelName;
                else if (lblUSER_ID1s.ID == item.LabelID)
                    txtUSER_ID1s.Text = lblUSER_ID1s.Text = lblhUSER_ID.Text = item.LabelName;
                else if (lblROLE_ID1s.ID == item.LabelID)
                    txtROLE_ID1s.Text = lblROLE_ID1s.Text = lblhROLE_ID.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                else if (lblACTIVE_FROM_DT1s.ID == item.LabelID)
                    txtACTIVE_FROM_DT1s.Text = lblACTIVE_FROM_DT1s.Text = lblhACTIVE_FROM_DT.Text = item.LabelName;
                else if (lblACTIVE_TO_DT1s.ID == item.LabelID)
                    txtACTIVE_TO_DT1s.Text = lblACTIVE_TO_DT1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    txtTENANT_ID2h.Text = lblTENANT_ID2h.Text = lblhTENANT_ID.Text = item.LabelName;

                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    txtPRIVILEGE_ID2h.Text = lblPRIVILEGE_ID2h.Text = item.LabelName;
                else if (lblUSER_ID2h.ID == item.LabelID)
                    txtUSER_ID2h.Text = lblUSER_ID2h.Text = lblhUSER_ID.Text = item.LabelName;
                else if (lblROLE_ID2h.ID == item.LabelID)
                    txtROLE_ID2h.Text = lblROLE_ID2h.Text = lblhROLE_ID.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                else if (lblACTIVE_FROM_DT2h.ID == item.LabelID)
                    txtACTIVE_FROM_DT2h.Text = lblACTIVE_FROM_DT2h.Text = lblhACTIVE_FROM_DT.Text = item.LabelName;
                else if (lblACTIVE_TO_DT2h.ID == item.LabelID)
                    txtACTIVE_TO_DT2h.Text = lblACTIVE_TO_DT2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_USER_ROLE").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\ACM_USER_ROLE.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("ACM_USER_ROLE").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID1s.Text;
                else if (lblUSER_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSER_ID1s.Text;
                else if (lblROLE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_ID1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                else if (lblACTIVE_FROM_DT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FROM_DT1s.Text;
                else if (lblACTIVE_TO_DT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_TO_DT1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID2h.Text;

                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID2h.Text;
                else if (lblUSER_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSER_ID2h.Text;
                else if (lblROLE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_ID2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                else if (lblACTIVE_FROM_DT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FROM_DT2h.Text;
                else if (lblACTIVE_TO_DT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_TO_DT2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\ACM_USER_ROLE.xml"));

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
            navigation.Visible = false;
            //  drpUSER_ROLE_ID.Enabled = true;
            drpPRIVILEGE_ID.Enabled = true;
            drpUSER_ID.Enabled = true;
            drpROLE_ID.Enabled = true;
            cbACTIVE_FLAG.Checked = true;
            txtACTIVE_FROM_DT.Enabled = true;
            txtACTIVE_TO_DT.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //   drpUSER_ROLE_ID.Enabled = false;
            drpPRIVILEGE_ID.Enabled = false;
            drpUSER_ID.Enabled = false;
            drpROLE_ID.Enabled = false;
            cbACTIVE_FLAG.Checked = false;
            txtACTIVE_FROM_DT.Enabled = false;
            txtACTIVE_TO_DT.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }



        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.USER_ROLE.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_ROLE.OrderBy(m => m.USER_ROLE_ID).Take(take).Skip(Skip)).ToList());
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

            ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.USER_ROLE.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_ROLE.OrderBy(m => m.USER_ROLE_ID).Take(take).Skip(Skip)).ToList());
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
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.USER_ROLE.Count();
                take = Showdata;
                Skip = 0;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_ROLE.OrderBy(m => m.USER_ROLE_ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB.USER_ROLE.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_ROLE.OrderBy(m => m.USER_ROLE_ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            //  FirstData();
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

                        Database.USER_ROLE objSOJobDesc = DB.USER_ROLE.Single(p => p.USER_ROLE_ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_ROLE.OrderBy(m => m.USER_ROLE_ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.USER_ROLE objUSER_ROLE = DB.USER_ROLE.Single(p => p.USER_ROLE_ID == ID);
                        //   drpUSER_ROLE_ID.SelectedValue = objUSER_ROLE.USER_ROLE_ID.ToString();
                        drpPRIVILEGE_ID.SelectedValue = objUSER_ROLE.PRIVILEGE_ID.ToString();
                        drpUSER_ID.SelectedValue = objUSER_ROLE.USER_ID.ToString();
                        drpROLE_ID.SelectedValue = objUSER_ROLE.ROLE_ID.ToString();
                        cbACTIVE_FLAG.Checked = objUSER_ROLE.ACTIVE_FLAG == "Y" ? true : false;
                        DateTime ACTIVE_FROM_DT = Convert.ToDateTime(objUSER_ROLE.ACTIVE_FROM_DT);
                        DateTime ACTIVE_TO_DT = Convert.ToDateTime(objUSER_ROLE.ACTIVE_TO_DT);
                        txtACTIVE_FROM_DT.Text = ACTIVE_FROM_DT.ToString("MM/dd/yyyy");
                        txtACTIVE_TO_DT.Text = ACTIVE_TO_DT.ToString("MM/dd/yyyy");
                        //txtCRUP_ID.Text = objUSER_ROLE.CRUP_ID.ToString();

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

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.USER_ROLE.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_ROLE.OrderBy(m => m.USER_ROLE_ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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


        protected void DrpTENANT_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);

            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
            //select * from TBLLOCATION where TenantID=1

            //drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenantID == TID);
            //drplocation.DataTextField = "LOCNAME1";
            //drplocation.DataValueField = "LOCATIONID";
            //drplocation.DataBind();
            //drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));
            ViewState["TenenatID"] = TID;


        }




        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(drplocation.SelectedValue) != 0)
            {

                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                ViewState["TenenatID"] = TID;
                ViewState["LocationID"] = LID;
                TENANT_ID = Convert.ToInt32(ViewState["TenenatID"]);
                LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);

                drpUSER_ID.Items.Clear();
                var Datas = (from fun12 in DB.USER_MST.Where(p => p.TenentID == TENANT_ID && p.LOCATION_ID == LOCATIONID)
                             select new
                             {
                                 Name = fun12.FIRST_NAME + "" + fun12.LAST_NAME,
                                 ID = fun12.USER_ID

                             }
         ).Distinct();
                drpUSER_ID.DataSource = Datas;
                drpUSER_ID.DataTextField = "Name";
                drpUSER_ID.DataValueField = "ID";
                drpUSER_ID.DataBind();
                drpUSER_ID.Items.Insert(0, new ListItem("---Select---", "0"));

                Classes.EcommAdminClass.getdropdown(drpPRIVILEGE_ID, TENANT_ID, "", "", "", "PRIVILEGE_MST");
                //select * from PRIVILEGE_MST

                //drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TENANT_ID == TENANT_ID );
                //drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
                //drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
                //drpPRIVILEGE_ID.DataBind();
                //drpPRIVILEGE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Privilege--", "0"));

                Classes.EcommAdminClass.getdropdown(drpROLE_ID, TENANT_ID, "", "", "", "ROLE_MST");
                //select * from ROLE_MST


                //drpROLE_ID.DataSource = DB.ROLE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TENANT_ID == TENANT_ID );
                //drpROLE_ID.DataTextField = "ROLE_NAME";
                //drpROLE_ID.DataValueField = "ROLE_ID";
                //drpROLE_ID.DataBind();
                //drpROLE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Role--", "0"));
                listmenu.DataSource = DB.FUNCTION_MST.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == 1);
                listmenu.DataBind();
                menulistpnl.Visible = true;

            }

        }

    }
}