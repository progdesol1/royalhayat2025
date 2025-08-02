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
    public partial class ACM_PRIVILAGE_MENU : System.Web.UI.Page
    {
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
                if (DB.PRIVILAGE_MENUDemon.Count() > 0)
                {
                    //  FirstData();
                }

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator

        public void GetShow()
        {
            // lblPRIVILEGE_ID1s.Attributes["class"] = 
            lblPRIVILAGEFOR1s.Attributes["class"] = lblMENU_ID1s.Attributes["class"] = lblIS_VISIBLE1s.Attributes["class"] = lblIS_ENABLE1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] =  lblALL_FLAG1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPRIVILEGE_ID2h.Attributes["class"] = lblPRIVILAGEFOR2h.Attributes["class"] = lblMENU_ID2h.Attributes["class"] = lblIS_VISIBLE2h.Attributes["class"] = lblIS_ENABLE2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"]  = lblALL_FLAG2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");
            //lblPRIVILEGE_MENU_ID1s.Attributes["class"] =lblPRIVILEGE_MENU_ID2h.Attributes["class"] = 
            //lblMySerial1s.Attributes["class"] == lblMySerial2h.Attributes["class"]
        }

        public void GetHide()
        {
            lblPRIVILEGE_ID1s.Attributes["class"] = lblPRIVILAGEFOR1s.Attributes["class"] = lblMENU_ID1s.Attributes["class"] = lblIS_VISIBLE1s.Attributes["class"] = lblIS_ENABLE1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblALL_FLAG1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = "control-label col-md-4  gethide";
             lblPRIVILEGE_ID2h.Attributes["class"] = lblPRIVILAGEFOR2h.Attributes["class"] = lblMENU_ID2h.Attributes["class"] = lblIS_VISIBLE2h.Attributes["class"] = lblIS_ENABLE2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] =  lblALL_FLAG2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");
            //lblMySerial2h.Attributes["class"] = lblPRIVILEGE_MENU_ID1s.Attributes["class"] =lblPRIVILEGE_MENU_ID2h.Attributes["class"] =
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
            //txtTENANT_ID.Text = "";
            drpPRIVILEGE_ID.SelectedIndex = 0;
            drpPRIVILAGEFOR.SelectedIndex = 0;
            drpMENU_ID.SelectedIndex = 0;
            cbVIEW_FLAG.Checked = false;
            cbIS_ENABLE.Checked = false;
            cbACTIVE_FLAG.Checked = false;
            //txtCRUP_ID.Text = "";
            //drpLocation.SelectedIndex = 0;
            cbALL_FLAG.Checked = false;
            cbADD_FLAG.Checked = false;
            cbMODIFY_FLAG.Checked = false;
            cbDELETE_FLAG.Checked = false;
            cbVIEW_FLAG.Checked = false;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                int PRIVILEGE_MENU_ID = 0;
                int TENANT_ID = 0;
                int LOCATION_ID = 0;
                int PRIVILEGE_ID = 0;
                int PRIVILAGEFOR = 0;
                int MENU_ID = 0;
                string IS_VISIBLE = "";
                string IS_ENABLE = "";
                string ACTIVE_FLAG = "";
                int CRUP_ID = 0;
                int MySerial = 0;
                string ALL_FLAG = "";
                string ADD_FLAG = "";
                string MODIFY_FLAG = "";
                string DELETE_FLAG = "";
                string VIEW_FLAG = "";

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
                        Database.PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new Database.PRIVILAGE_MENUDemon();
                        //Server Content Send data Yogesh
                        if (ViewState["TenenatID"] != null)
                        {
                            TENANT_ID = Convert.ToInt32(ViewState["TenenatID"]);
                        }
                        if (ViewState["LocationID"] != null)
                        {

                            LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                        }
                        //if (Convert.ToInt32(drpTentID.SelectedValue) != 0)
                        //{
                        //    TENANT_ID = Convert.ToInt32(drpTentID.SelectedValue);
                        //}
                        //if (Convert.ToInt32(drpLocation.SelectedValue) != 0)
                        //{
                        //    LOCATION_ID = Convert.ToInt32(drpLocation.SelectedValue);
                        //}
                        PRIVILEGE_MENU_ID = 0; //DB.PRIVILAGE_MENU.Count() > 0 ? Convert.ToInt32(DB.PRIVILAGE_MENU.Max(p => p.PRIVILEGE_MENU_ID) + 1) : 1;
                        if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                        {
                            PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drpPRIVILAGEFOR.SelectedValue) != 0)
                        {
                            PRIVILAGEFOR = Convert.ToInt32(drpPRIVILAGEFOR.SelectedValue);
                        }
                        if (Convert.ToInt32(drpMENU_ID.SelectedValue) != 0)
                        {
                            MENU_ID = Convert.ToInt32(drpMENU_ID.SelectedValue);
                        }

                        IS_VISIBLE = cbIS_ENABLE.Checked ? "Y" : "N";
                        IS_ENABLE = cbIS_ENABLE.Checked ? "Y" : "N";
                        ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                        //objPRIVILAGE_MENU.CRUP_ID = txtCRUP_ID.Text;
                     //   MySerial = Convert.ToInt32(drpLocation.SelectedValue);
                        ALL_FLAG = cbALL_FLAG.Checked ? "Y" : "N";
                        ADD_FLAG = cbADD_FLAG.Checked ? "Y" : "N";
                        MODIFY_FLAG = cbMODIFY_FLAG.Checked ? "Y" : "N";
                        DELETE_FLAG = cbDELETE_FLAG.Checked ? "Y" : "N";
                        VIEW_FLAG = cbVIEW_FLAG.Checked ? "Y" : "N";
                        Classes.ACMClass.InsertDataACMPRIVILAGEMENU(TENANT_ID, LOCATION_ID, PRIVILEGE_ID, MENU_ID, PRIVILEGE_MENU_ID, IS_VISIBLE, IS_ENABLE, ACTIVE_FLAG, CRUP_ID, MySerial, ALL_FLAG, ADD_FLAG, MODIFY_FLAG, DELETE_FLAG, VIEW_FLAG, PRIVILAGEFOR);
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                       // BindData();
                        navigation.Visible = true;
                        Readonly();
                        btnAdd.Text = "AddNew";
                        // FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            PRIVILEGE_MENU_ID = Convert.ToInt32(ViewState["Edit"]);

                            //objPRIVILAGE_MENU.TENANT_ID = txtTENANT_ID.Text;
                            //if (Convert.ToInt32(drpTentID.SelectedValue) != 0)
                            //{
                            //    TENANT_ID = Convert.ToInt32(drpTentID.SelectedValue);
                            //}
                            //if (Convert.ToInt32(drpLocation.SelectedValue) != 0)
                            //{
                            //    LOCATION_ID = Convert.ToInt32(drpLocation.SelectedValue);
                            //}
                            if (ViewState["TenenatID"] != null)
                            {
                                TENANT_ID = Convert.ToInt32(ViewState["TenenatID"]);
                            }
                            if (ViewState["LocationID"] != null)
                            {

                                LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                            }
                            if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                            {
                                PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                            }
                            if (Convert.ToInt32(drpPRIVILAGEFOR.SelectedValue) != 0)
                            {
                                PRIVILAGEFOR = Convert.ToInt32(drpPRIVILAGEFOR.SelectedValue);
                            }
                            if (Convert.ToInt32(drpMENU_ID.SelectedValue) != 0)
                            {
                                MENU_ID = Convert.ToInt32(drpMENU_ID.SelectedValue);
                            }
                            IS_VISIBLE = cbIS_ENABLE.Checked ? "Y" : "N";
                            IS_ENABLE = cbIS_ENABLE.Checked ? "Y" : "N";
                            ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            //objPRIVILAGE_MENU.CRUP_ID = txtCRUP_ID.Text;
                          //  MySerial = Convert.ToInt32(drpLocation.SelectedValue);
                            ALL_FLAG = cbALL_FLAG.Checked ? "Y" : "N";
                            ADD_FLAG = cbADD_FLAG.Checked ? "Y" : "N";
                            MODIFY_FLAG = cbMODIFY_FLAG.Checked ? "Y" : "N";
                            DELETE_FLAG = cbDELETE_FLAG.Checked ? "Y" : "N";
                            VIEW_FLAG = cbVIEW_FLAG.Checked ? "Y" : "N";
                            Classes.ACMClass.InsertDataACMPRIVILAGEMENU(TENANT_ID, LOCATION_ID, PRIVILEGE_ID, MENU_ID, PRIVILEGE_MENU_ID, IS_VISIBLE, IS_ENABLE, ACTIVE_FLAG, CRUP_ID, MySerial, ALL_FLAG, ADD_FLAG, MODIFY_FLAG, DELETE_FLAG, VIEW_FLAG, PRIVILAGEFOR);
                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                            //  Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                          //  BindData();
                            navigation.Visible = true;
                            Readonly();
                            // FirstData();
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
            //drpLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Loction--", "0"));
            drpPRIVILEGE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Privillage--", "0"));
            drpMENU_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Loction--", "0"));
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
            //txtTENANT_ID.Text = Listview1.SelectedDataKey[1].ToString();
            drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpPRIVILAGEFOR.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            drpMENU_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            cbIS_VISIBLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            cbIS_ENABLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
            cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
            //drpLocation.SelectedValue = Listview1.SelectedDataKey[10].ToString();
            cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[11]);
            cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[12]);
            cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[13]);
            cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[14]);
            cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[15]);

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //txtTENANT_ID.Text = Listview1.SelectedDataKey[1].ToString();
                drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpPRIVILAGEFOR.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                drpMENU_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                cbIS_VISIBLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
                cbIS_ENABLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
                cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
                //drpLocation.SelectedValue = Listview1.SelectedDataKey[10].ToString();
                cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[11]);
                cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[12]);
                cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[13]);
                cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[14]);
                cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[15]);

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
                //txtTENANT_ID.Text = Listview1.SelectedDataKey[1].ToString();
                drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpPRIVILAGEFOR.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                drpMENU_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                cbIS_VISIBLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
                cbIS_ENABLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
                cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
                //drpLocation.SelectedValue = Listview1.SelectedDataKey[10].ToString();
                cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[11]);
                cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[12]);
                cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[13]);
                cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[14]);
                cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[15]);

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //txtTENANT_ID.Text = Listview1.SelectedDataKey[1].ToString();
            drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpPRIVILAGEFOR.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            drpMENU_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            cbIS_VISIBLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            cbIS_ENABLE.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
            cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
            //drpLocation.SelectedValue = Listview1.SelectedDataKey[10].ToString();
            cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[11]);
            cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[12]);
            cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[13]);
            cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[14]);
            cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[15]);

        }
        #endregion

        #region PAge Genarator languaege

        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {
                   //lblMySerial2h.Visible = 
                    //2false
                     lblPRIVILEGE_ID2h.Visible = lblPRIVILAGEFOR2h.Visible = lblMENU_ID2h.Visible = lblIS_VISIBLE2h.Visible = lblIS_ENABLE2h.Visible = lblACTIVE_FLAG2h.Visible = lblALL_FLAG2h.Visible = lblADD_FLAG2h.Visible = lblMODIFY_FLAG2h.Visible = lblDELETE_FLAG2h.Visible = lblVIEW_FLAG2h.Visible = false;
                    //2true
                    txtPRIVILEGE_ID2h.Visible = txtPRIVILAGEFOR2h.Visible = txtMENU_ID2h.Visible = txtIS_VISIBLE2h.Visible = txtIS_ENABLE2h.Visible = txtACTIVE_FLAG2h.Visible =txtALL_FLAG2h.Visible = txtADD_FLAG2h.Visible = txtMODIFY_FLAG2h.Visible = txtDELETE_FLAG2h.Visible = txtVIEW_FLAG2h.Visible = true;
                    // txtMySerial2h.Visible = 
                    //headerlblPRIVILEGE_MENU_ID2h.Visible = txtPRIVILEGE_MENU_ID2h.Visible =
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true lblPRIVILEGE_MENU_ID2h.Visible =  txtPRIVILEGE_MENU_ID2h.Visible = lblMySerial2h.Visible = = txtMySerial2h.Visible
                   lblPRIVILEGE_ID2h.Visible = lblPRIVILAGEFOR2h.Visible = lblMENU_ID2h.Visible = lblIS_VISIBLE2h.Visible = lblIS_ENABLE2h.Visible = lblACTIVE_FLAG2h.Visible = lblALL_FLAG2h.Visible = lblADD_FLAG2h.Visible = lblMODIFY_FLAG2h.Visible = lblDELETE_FLAG2h.Visible = lblVIEW_FLAG2h.Visible = true;
                    //2false
                   txtPRIVILEGE_ID2h.Visible = txtPRIVILAGEFOR2h.Visible = txtMENU_ID2h.Visible = txtIS_VISIBLE2h.Visible = txtIS_ENABLE2h.Visible = txtACTIVE_FLAG2h.Visible  = txtALL_FLAG2h.Visible = txtADD_FLAG2h.Visible = txtMODIFY_FLAG2h.Visible = txtDELETE_FLAG2h.Visible = txtVIEW_FLAG2h.Visible = false;

                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1falselblPRIVILEGE_MENU_ID1s.Visible = txtPRIVILEGE_MENU_ID1s.Visible =lblMySerial1s.Visible == txtMySerial1s.Visible
                    lblPRIVILEGE_ID1s.Visible = lblPRIVILAGEFOR1s.Visible = lblMENU_ID1s.Visible = lblIS_VISIBLE1s.Visible = lblIS_ENABLE1s.Visible = lblACTIVE_FLAG1s.Visible =  lblALL_FLAG1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = false;
                    //1true
                     txtPRIVILEGE_ID1s.Visible = txtPRIVILAGEFOR1s.Visible = txtMENU_ID1s.Visible = txtIS_VISIBLE1s.Visible = txtIS_ENABLE1s.Visible = txtACTIVE_FLAG1s.Visible  = txtALL_FLAG1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = true;
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
                    //1truelblPRIVILEGE_MENU_ID1s.Visible = txtPRIVILEGE_MENU_ID1s.Visible =lblMySerial1s.Visible == txtMySerial1s.Visible 
                     lblPRIVILEGE_ID1s.Visible = lblPRIVILAGEFOR1s.Visible = lblMENU_ID1s.Visible = lblIS_VISIBLE1s.Visible = lblIS_ENABLE1s.Visible = lblACTIVE_FLAG1s.Visible =  lblALL_FLAG1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = true;
                    //1false
                    txtPRIVILEGE_ID1s.Visible = txtPRIVILAGEFOR1s.Visible = txtMENU_ID1s.Visible = txtIS_VISIBLE1s.Visible = txtIS_ENABLE1s.Visible = txtACTIVE_FLAG1s.Visible = txtALL_FLAG1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_PRIVILAGE_MENU").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblPRIVILEGE_MENU_ID1s.ID == item.LabelID)
                //    txtPRIVILEGE_MENU_ID1s.Text = lblPRIVILEGE_MENU_ID1s.Text = lblhPRIVILEGE_MENU_ID.Text = item.LabelName;
                //else if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = lblhTENANT_ID.Text = item.LabelName;
                 if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    txtPRIVILEGE_ID1s.Text = lblPRIVILEGE_ID1s.Text = lblhPRIVILEGE_ID.Text = item.LabelName;
                else if (lblPRIVILAGEFOR1s.ID == item.LabelID)
                    txtPRIVILAGEFOR1s.Text = lblPRIVILAGEFOR1s.Text = item.LabelName;
                else if (lblMENU_ID1s.ID == item.LabelID)
                    txtMENU_ID1s.Text = lblMENU_ID1s.Text = lblhMENU_ID.Text = item.LabelName;
                else if (lblIS_VISIBLE1s.ID == item.LabelID)
                    txtIS_VISIBLE1s.Text = lblIS_VISIBLE1s.Text = item.LabelName;
                else if (lblIS_ENABLE1s.ID == item.LabelID)
                    txtIS_ENABLE1s.Text = lblIS_ENABLE1s.Text = lblhIS_ENABLE.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;
                //else if (lblMySerial1s.ID == item.LabelID)
                //    txtMySerial1s.Text = lblMySerial1s.Text = item.LabelName;
                else if (lblALL_FLAG1s.ID == item.LabelID)
                    txtALL_FLAG1s.Text = lblALL_FLAG1s.Text = lblhALL_FLAG.Text = item.LabelName;
                else if (lblADD_FLAG1s.ID == item.LabelID)
                    txtADD_FLAG1s.Text = lblADD_FLAG1s.Text = item.LabelName;
                else if (lblMODIFY_FLAG1s.ID == item.LabelID)
                    txtMODIFY_FLAG1s.Text = lblMODIFY_FLAG1s.Text = lblhMODIFY_FLAG.Text = item.LabelName;
                else if (lblDELETE_FLAG1s.ID == item.LabelID)
                    txtDELETE_FLAG1s.Text = lblDELETE_FLAG1s.Text = item.LabelName;
                else if (lblVIEW_FLAG1s.ID == item.LabelID)
                    txtVIEW_FLAG1s.Text = lblVIEW_FLAG1s.Text = item.LabelName;
                //else if (lblPRIVILEGE_MENU_ID2h.ID == item.LabelID)
                //    txtPRIVILEGE_MENU_ID2h.Text = lblPRIVILEGE_MENU_ID2h.Text = lblhPRIVILEGE_MENU_ID.Text = item.LabelName;
                //else if (lblTENANT_ID2h.ID == item.LabelID)
                //    txtTENANT_ID2h.Text = lblTENANT_ID2h.Text = lblhTENANT_ID.Text = item.LabelName;
                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    txtPRIVILEGE_ID2h.Text = lblPRIVILEGE_ID2h.Text = lblhPRIVILEGE_ID.Text = item.LabelName;
                else if (lblPRIVILAGEFOR2h.ID == item.LabelID)
                    txtPRIVILAGEFOR2h.Text = lblPRIVILAGEFOR2h.Text = item.LabelName;
                else if (lblMENU_ID2h.ID == item.LabelID)
                    txtMENU_ID2h.Text = lblMENU_ID2h.Text = lblhMENU_ID.Text = item.LabelName;
                else if (lblIS_VISIBLE2h.ID == item.LabelID)
                    txtIS_VISIBLE2h.Text = lblIS_VISIBLE2h.Text = item.LabelName;
                else if (lblIS_ENABLE2h.ID == item.LabelID)
                    txtIS_ENABLE2h.Text = lblIS_ENABLE2h.Text = lblhIS_ENABLE.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;
                //else if (lblMySerial2h.ID == item.LabelID)
                //    txtMySerial2h.Text = lblMySerial2h.Text = item.LabelName;
                else if (lblALL_FLAG2h.ID == item.LabelID)
                    txtALL_FLAG2h.Text = lblALL_FLAG2h.Text = lblhALL_FLAG.Text = item.LabelName;
                else if (lblADD_FLAG2h.ID == item.LabelID)
                    txtADD_FLAG2h.Text = lblADD_FLAG2h.Text = item.LabelName;
                else if (lblMODIFY_FLAG2h.ID == item.LabelID)
                    txtMODIFY_FLAG2h.Text = lblMODIFY_FLAG2h.Text = lblhMODIFY_FLAG.Text = item.LabelName;
                else if (lblDELETE_FLAG2h.ID == item.LabelID)
                    txtDELETE_FLAG2h.Text = lblDELETE_FLAG2h.Text = item.LabelName;
                else if (lblVIEW_FLAG2h.ID == item.LabelID)
                    txtVIEW_FLAG2h.Text = lblVIEW_FLAG2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_PRIVILAGE_MENU").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\ACM_PRIVILAGE_MENU.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("ACM_PRIVILAGE_MENU").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblPRIVILEGE_MENU_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_MENU_ID1s.Text;
                //else if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                 if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID1s.Text;
                else if (lblPRIVILAGEFOR1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILAGEFOR1s.Text;
                else if (lblMENU_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMENU_ID1s.Text;
                else if (lblIS_VISIBLE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIS_VISIBLE1s.Text;
                else if (lblIS_ENABLE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIS_ENABLE1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                //else if (lblMySerial1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMySerial1s.Text;
                else if (lblALL_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtALL_FLAG1s.Text;
                else if (lblADD_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADD_FLAG1s.Text;
                else if (lblMODIFY_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODIFY_FLAG1s.Text;
                else if (lblDELETE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDELETE_FLAG1s.Text;
                else if (lblVIEW_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtVIEW_FLAG1s.Text;
                //else if (lblPRIVILEGE_MENU_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_MENU_ID2h.Text;
                //else if (lblTENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID2h.Text;
                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID2h.Text;
                else if (lblPRIVILAGEFOR2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILAGEFOR2h.Text;
                else if (lblMENU_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMENU_ID2h.Text;
                else if (lblIS_VISIBLE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIS_VISIBLE2h.Text;
                else if (lblIS_ENABLE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIS_ENABLE2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                //else if (lblMySerial2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMySerial2h.Text;
                else if (lblALL_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtALL_FLAG2h.Text;
                else if (lblADD_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADD_FLAG2h.Text;
                else if (lblMODIFY_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODIFY_FLAG2h.Text;
                else if (lblDELETE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDELETE_FLAG2h.Text;
                else if (lblVIEW_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtVIEW_FLAG2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\ACM_PRIVILAGE_MENU.xml"));

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
            //txtTENANT_ID.Enabled = true;
          //  drpTentID.Enabled = true;
            drpPRIVILEGE_ID.Enabled = true;
            drpPRIVILAGEFOR.Enabled = true;
            drpMENU_ID.Enabled = true;
            cbIS_VISIBLE.Checked = true;
            cbIS_ENABLE.Checked = true;
            cbACTIVE_FLAG.Checked = true;
            //txtCRUP_ID.Enabled = true;
            //drpLocation.Enabled = true;
            cbALL_FLAG.Checked = true;
            cbADD_FLAG.Checked = true;
            cbMODIFY_FLAG.Checked = true;
            cbDELETE_FLAG.Checked = true;
            cbVIEW_FLAG.Checked = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //txtTENANT_ID.Enabled = false;
            //drpTentID.Enabled = false;
            drpPRIVILEGE_ID.Enabled = false;
            drpPRIVILAGEFOR.Enabled = false;
            drpMENU_ID.Enabled = false;
            cbIS_VISIBLE.Checked = false;
            cbIS_ENABLE.Checked = false;
            cbACTIVE_FLAG.Checked = false;
            //txtCRUP_ID.Enabled = false;
          //  drpLocation.Enabled = false;
            cbALL_FLAG.Checked = false;
            cbADD_FLAG.Checked = false;
            cbMODIFY_FLAG.Checked = false;
            cbDELETE_FLAG.Checked = false;
            cbVIEW_FLAG.Checked = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.PRIVILAGE_MENUDemon.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.PRIVILAGE_MENUDemon.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.PRIVILAGE_MENUDemon.Count();
                take = Showdata;
                Skip = 0;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.PRIVILAGE_MENUDemon.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).Take(take).Skip(Skip)).ToList());
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

                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.PRIVILAGE_MENUDemon objSOJobDesc = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.PRIVILAGE_MENUDemon objPRIVILAGE_MENU = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == ID);
                        //txtTENANT_ID.Text = objPRIVILAGE_MENU.TENANT_ID.ToString();
                        drpPRIVILEGE_ID.SelectedValue = objPRIVILAGE_MENU.PRIVILEGE_ID.ToString();
                        drpPRIVILAGEFOR.SelectedValue = objPRIVILAGE_MENU.PRIVILAGEFOR.ToString();
                        drpMENU_ID.SelectedValue = objPRIVILAGE_MENU.MENU_ID.ToString();
                        cbIS_VISIBLE.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.IS_VISIBLE);
                        cbIS_ENABLE.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.IS_ENABLE);
                        cbACTIVE_FLAG.Text = objPRIVILAGE_MENU.ACTIVE_FLAG.ToString();
                        //txtCRUP_ID.Text = objPRIVILAGE_MENU.CRUP_ID.ToString();
                        //drpLocation.SelectedValue = objPRIVILAGE_MENU.MySerial.ToString();
                        cbALL_FLAG.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.ALL_FLAG);
                        cbADD_FLAG.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.ADD_FLAG);
                        cbMODIFY_FLAG.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.MODIFY_FLAG);
                        cbDELETE_FLAG.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.DELETE_FLAG);
                        cbVIEW_FLAG.Checked = Convert.ToBoolean(objPRIVILAGE_MENU.VIEW_FLAG);

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
            int Totalrec = DB.PRIVILAGE_MENUDemon.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILAGE_MENUDemon.OrderBy(m => m.PRIVILEGE_MENU_ID).Take(Tvalue).Skip(Svalue)).ToList());
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

        //protected void drpTentID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(drpTentID.SelectedValue);
        //    bindMenu(TID);
        //}
        public void bindMenu(int DTI)
        {
            // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TENANT_ID);
            //drpMENU_ID.DataSource = DB.FUNCTION_MST.Where(p => p.ACTIVE_FLAG == 1 && p.TENANT_ID == DTI);
            //drpMENU_ID.DataTextField = "MENU_NAME1";
            //drpMENU_ID.DataValueField = "MENU_ID";
            //drpMENU_ID.DataBind();
           


            //drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TENANT_ID == DTI);
            //drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
            //drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
            //drpPRIVILEGE_ID.DataBind();
           

            //drpLocation.DataSource = DB.ACM_TBLLOCATION.Where(p => p.Active == "Y" && p.TenantID == DTI);
            //drpLocation.DataTextField = "LOCNAME1";
            //drpLocation.DataValueField = "LOCATIONID";
            //drpLocation.DataBind();
           

        }
        protected void DrpTENANT_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);

            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
            //select * from TBLLOCATION 

            //drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenantID == TID);
            //drplocation.DataTextField = "LOCNAME1";
            //drplocation.DataValueField = "LOCATIONID";
            //drplocation.DataBind();
            //drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));
            ViewState["TenenatID"] = TID;
        }




        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(drplocation.SelectedValue) != 0 )
            {

                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                ViewState["TenenatID"] = TID;
                ViewState["LocationID"] = LID;


                Classes.EcommAdminClass.getdropdown(drpMENU_ID, TID, "", "", "", "FUNCTION_MST");
                //select * from FUNCTION_MST where TENANT_ID=1

                //drpMENU_ID.DataSource = DB.FUNCTION_MST.Where(p => p.ACTIVE_FLAG == 1 && p.TENANT_ID == TID );
                //drpMENU_ID.DataTextField = "MENU_NAME1";
                //drpMENU_ID.DataValueField = "MENU_ID";
                //drpMENU_ID.DataBind();
                //drpMENU_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                Classes.EcommAdminClass.getdropdown(drpPRIVILEGE_ID, TID, "", "", "", "PRIVILEGE_MST");
                //select * from PRIVILEGE_MST

                //drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TENANT_ID == TID );
                //drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
                //drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
                //drpPRIVILEGE_ID.DataBind();
                //drpPRIVILEGE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }

        }
    }
}