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
    public partial class USER_RIGHTS : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TenentID = 0;
        int LOCATIONID = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        //    if (ViewState["TenenatID"] != null && ViewState["LocationID"] != null)
        //    {
        //        TenentID = Convert.ToInt32(ViewState["TenenatID"]);
        //        LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);
        //        drpUSER_ID.Items.Clear();
        //        var Datas = (from fun1 in DB.USER_MST
        //                     where fun1.TenentID == TenentID && fun1.LOCATION_ID == LOCATIONID
        //                     select new
        //                     {
        //                         Name = fun1.FIRST_NAME + "" + fun1.LAST_NAME,
        //                         ID = fun1.USER_ID

        //                     }
        //).ToList();
        //        drpUSER_ID.DataSource = Datas;
        //        drpUSER_ID.DataTextField = "Name";
        //        drpUSER_ID.DataValueField = "ID";
        //        drpUSER_ID.DataBind();
        //        drpUSER_ID.Items.Insert(0, new ListItem("---Select---", "0"));

        //        drpPRIVILEGE_ID.Items.Clear();
        //        drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.TenentID == TenentID && p.LOCATION_ID == LOCATIONID);
        //        drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
        //        drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
        //        drpPRIVILEGE_ID.DataBind();
        //        drpPRIVILEGE_ID.Items.Insert(0, new ListItem("---Select---", "0"));
        //    }
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
                if (DB.USER_RIGHTS.Count() > 0)
                {
                    FirstData();
                }
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (TID == 0)
                {
                    PNALGOL.Visible = true;
                }
                else
                {
                    DrpTENANT_ID.SelectedValue = TID.ToString();
                    drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
                    drplocation.DataTextField = "LOCNAME1";
                    drplocation.DataValueField = "LOCATIONID";
                    drplocation.DataBind();
                    drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));


                    drplocation.SelectedValue = LID.ToString();

                    drpUSER_ID.Items.Clear();
                    var Datas = (from fun1 in DB.USER_MST
                                 where fun1.TenentID == TID
                                 select new
                                 {
                                     Name = fun1.FIRST_NAME + "" + fun1.LAST_NAME,
                                     ID = fun1.USER_ID

                                 }
            ).ToList();
                    drpUSER_ID.DataSource = Datas;
                    drpUSER_ID.DataTextField = "Name";
                    drpUSER_ID.DataValueField = "ID";
                    drpUSER_ID.DataBind();
                    drpUSER_ID.Items.Insert(0, new ListItem("---Select---", "0"));

                    drpPRIVILEGE_ID.Items.Clear();
                    Classes.EcommAdminClass.getdropdown(drpPRIVILEGE_ID, TID, "", "", "", "PRIVILEGE_MST");
                    ViewState["TenenatID"] = TID;
                    ViewState["LocationID"] = LID;
                }

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.USER_RIGHTS> List = DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            // ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
            ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator
       
        public void GetShow()
        {

            lblUSER_ID1s.Attributes["class"] = lblPRIVILEGE_ID1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = lblALL_FLAG1s.Attributes["class"] = lblIsLabelUpdate1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblUSER_ID2h.Attributes["class"] = lblPRIVILEGE_ID2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = lblALL_FLAG2h.Attributes["class"] = lblIsLabelUpdate2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");
            //lblRIGHTS_ID1s.Attributes["class"]=lblRIGHTS_ID2h.Attributes["class"]
        }

        public void GetHide()
        {
            //lblRIGHTS_ID1s.Attributes["class"] = lblUSER_ID1s.Attributes["class"] = lblPRIVILEGE_ID1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = lblALL_FLAG1s.Attributes["class"] = lblIsLabelUpdate1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblUSER_ID1s.Attributes["class"] = lblPRIVILEGE_ID1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = lblALL_FLAG1s.Attributes["class"] = lblIsLabelUpdate1s.Attributes["class"] = "control-label col-md-4  gethide";
            //lblRIGHTS_ID2h.Attributes["class"] = lblUSER_ID2h.Attributes["class"] = lblPRIVILEGE_ID2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = lblALL_FLAG2h.Attributes["class"] = lblIsLabelUpdate2h.Attributes["class"] = "control-label col-md-4  getshow";
            lblUSER_ID2h.Attributes["class"] = lblPRIVILEGE_ID2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = lblALL_FLAG2h.Attributes["class"] = lblIsLabelUpdate2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            // drpRIGHTS_ID.SelectedIndex = 0;
            drpUSER_ID.SelectedIndex = 0;
            drpPRIVILEGE_ID.SelectedIndex = 0;
            cbADD_FLAG.Text = "";
            cbMODIFY_FLAG.Text = "";
            cbDELETE_FLAG.Text = "";
            cbVIEW_FLAG.Text = "";
            cbALL_FLAG.Text = "";
            //txtCRUP_ID.Text = "";
            cbIsLabelUpdate.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TenentID = 0;
                int LOCATION_ID = 1;
                int RIGHTS_ID = 0;
                int USER_ID = 0;
                int PRIVILEGE_ID = 0;
                bool ADD_FLAG = false;
                bool MODIFY_FLAG = false;
                bool DELETE_FLAG = false;
                bool VIEW_FLAG = false;
                bool ALL_FLAG = false;
                int CRUP_ID = 0;
                bool IsLabelUpdate = false;

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

                        //Server Content Send data Yogesh
                        //if (Convert.ToInt32(drpRIGHTS_ID.SelectedValue) != 0)
                        //{
                        //    objUSER_RIGHTS.RIGHTS_ID = Convert.ToInt32(drpRIGHTS_ID.SelectedValue);
                        //}
                        if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
                        {
                            TenentID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drplocation.SelectedValue) != 0)
                        {
                            LOCATION_ID = Convert.ToInt32(drplocation.SelectedValue);
                        }
                        if (Convert.ToInt32(drpUSER_ID.SelectedValue) != 0)
                        {
                            USER_ID = Convert.ToInt32(drpUSER_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                        {
                            PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                        }
                        //if (DrpTENANT_ID.SelectedItem.Text != "---Select---")
                        //{
                        //    TenentID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                        //}
                        if (ViewState["TenenatID"] != null && ViewState["LocationID"] != null)
                        {
                            TenentID = Convert.ToInt32(ViewState["TenenatID"]);
                            LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);
                        }

                        if (ViewState["LocationID"] != null)
                        {

                            LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);
                        }
                        RIGHTS_ID = 0; //DB.USER_RIGHT.Count() > 0 ? Convert.ToInt32(DB.USER_RIGHT.Max(p => p.RIGHTS_ID) + 1) : 1;
                        ADD_FLAG = cbADD_FLAG.Checked;
                        MODIFY_FLAG = cbMODIFY_FLAG.Checked;
                        DELETE_FLAG = cbDELETE_FLAG.Checked;
                        VIEW_FLAG = cbVIEW_FLAG.Checked;
                        ALL_FLAG = cbALL_FLAG.Checked;
                        //objUSER_RIGHTS.CRUP_ID = txtCRUP_ID.Text;
                        IsLabelUpdate = cbIsLabelUpdate.Checked;
                        Classes.ACMClass.InsertDataACMUSERRIGHTS(TenentID, LOCATION_ID, USER_ID, PRIVILEGE_ID, RIGHTS_ID, ADD_FLAG, MODIFY_FLAG, DELETE_FLAG, VIEW_FLAG, ALL_FLAG, CRUP_ID, IsLabelUpdate);
                      //  Clear();
                        lblMsg.Text = "Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                      //  BindData();
                        navigation.Visible = true;
                        Readonly();
                       // FirstData();
                        btnAdd.Text = "AddNew";
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.USER_RIGHTS objUSER_RIGHTS = DB.USER_RIGHTS.Single(p => p.RIGHTS_ID == ID);
                            //if (Convert.ToInt32(drpRIGHTS_ID.SelectedValue) != 0)
                            //{
                            //    objUSER_RIGHTS.RIGHTS_ID = Convert.ToInt32(drpRIGHTS_ID.SelectedValue);
                            //}
                            if (Convert.ToInt32(drpUSER_ID.SelectedValue) != 0)
                            {
                                USER_ID = Convert.ToInt32(drpUSER_ID.SelectedValue);
                            }
                            if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                            {
                                PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                            }
                            //if (DrpTENANT_ID.SelectedItem.Text != "---Select---")
                            //{
                            //    TenentID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                            //}
                            if (ViewState["TenenatID"] != null && ViewState["LocationID"] != null)
                            {
                                TenentID = Convert.ToInt32(ViewState["TenenatID"]);
                                LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);
                            }

                            //if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
                            //{
                            //    objUSER_RIGHTS.TenentID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                            //}
                            ADD_FLAG = cbADD_FLAG.Checked;
                            MODIFY_FLAG = cbMODIFY_FLAG.Checked;
                            DELETE_FLAG = cbDELETE_FLAG.Checked;
                            VIEW_FLAG = cbVIEW_FLAG.Checked;
                            ALL_FLAG = cbALL_FLAG.Checked;
                            //objUSER_RIGHTS.CRUP_ID = txtCRUP_ID.Text;
                            IsLabelUpdate = cbIsLabelUpdate.Checked;
                            Classes.ACMClass.InsertDataACMUSERRIGHTS(TenentID, LOCATION_ID, USER_ID, PRIVILEGE_ID, RIGHTS_ID, ADD_FLAG, MODIFY_FLAG, DELETE_FLAG, VIEW_FLAG, ALL_FLAG, CRUP_ID, IsLabelUpdate);
                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                          //  Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                          //  BindData();
                            navigation.Visible = true;
                            Readonly();
                         //   FirstData();
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
            // drpRIGHTS_ID.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpUSER_ID.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpPRIVILEGE_ID.Items.Insert(0, new ListItem("-- Select --", "0"));
            //   drpRIGHTS_ID.Items.Insert(0, new ListItem("-- Select --", "0"));

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

            // drpRIGHTS_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            // drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            //  drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
            cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
            cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[7]);
            cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
            cbIsLabelUpdate.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[10]);

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                // drpRIGHTS_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                // drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                // drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
                cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
                cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
                cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[7]);
                cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
                cbIsLabelUpdate.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[10]);

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
                // drpRIGHTS_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                //  drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                // drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
                cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
                cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
                cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[7]);
                cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
                cbIsLabelUpdate.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[10]);

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            // drpRIGHTS_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            // drpUSER_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            // drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            cbADD_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
            cbMODIFY_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            cbDELETE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[6]);
            cbVIEW_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[7]);
            cbALL_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[8]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[9].ToString();
            cbIsLabelUpdate.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[10]);

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
                    // lblRIGHTS_ID2h.Visible = 
                    lblUSER_ID2h.Visible = lblPRIVILEGE_ID2h.Visible = lblADD_FLAG2h.Visible = lblMODIFY_FLAG2h.Visible = lblDELETE_FLAG2h.Visible = lblVIEW_FLAG2h.Visible = lblALL_FLAG2h.Visible = lblIsLabelUpdate2h.Visible = false;
                    //2true
                    //txtRIGHTS_ID2h.Visible = 
                    txtUSER_ID2h.Visible = txtPRIVILEGE_ID2h.Visible = txtADD_FLAG2h.Visible = txtMODIFY_FLAG2h.Visible = txtDELETE_FLAG2h.Visible = txtVIEW_FLAG2h.Visible = txtALL_FLAG2h.Visible = txtIsLabelUpdate2h.Visible = true;

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
                    //  lblRIGHTS_ID2h.Visible = 
                    lblUSER_ID2h.Visible = lblPRIVILEGE_ID2h.Visible = lblADD_FLAG2h.Visible = lblMODIFY_FLAG2h.Visible = lblDELETE_FLAG2h.Visible = lblVIEW_FLAG2h.Visible = lblALL_FLAG2h.Visible = lblIsLabelUpdate2h.Visible = true;
                    //2false
                    //txtRIGHTS_ID2h.Visible = 
                    txtUSER_ID2h.Visible = txtPRIVILEGE_ID2h.Visible = txtADD_FLAG2h.Visible = txtMODIFY_FLAG2h.Visible = txtDELETE_FLAG2h.Visible = txtVIEW_FLAG2h.Visible = txtALL_FLAG2h.Visible = txtIsLabelUpdate2h.Visible = false;

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
                    // lblRIGHTS_ID1s.Visible = lblUSER_ID1s.Visible = lblPRIVILEGE_ID1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = lblALL_FLAG1s.Visible = lblIsLabelUpdate1s.Visible = false;
                    lblUSER_ID1s.Visible = lblPRIVILEGE_ID1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = lblALL_FLAG1s.Visible = lblIsLabelUpdate1s.Visible = false;
                    //1true
                    // txtRIGHTS_ID1s.Visible = txtUSER_ID1s.Visible = txtPRIVILEGE_ID1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = txtALL_FLAG1s.Visible = txtIsLabelUpdate1s.Visible = true;
                    txtUSER_ID1s.Visible = txtPRIVILEGE_ID1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = txtALL_FLAG1s.Visible = txtIsLabelUpdate1s.Visible = true;
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
                    // lblRIGHTS_ID1s.Visible = 
                    lblUSER_ID1s.Visible = lblPRIVILEGE_ID1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = lblALL_FLAG1s.Visible = lblIsLabelUpdate1s.Visible = true;
                    //1false
                    // txtRIGHTS_ID1s.Visible = 
                    txtUSER_ID1s.Visible = txtPRIVILEGE_ID1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = txtALL_FLAG1s.Visible = txtIsLabelUpdate1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_USER_RIGHTS").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = lblhTENANT_ID.Text = item.LabelName;
                //if (lblRIGHTS_ID1s.ID == item.LabelID)
                //    txtRIGHTS_ID1s.Text = lblRIGHTS_ID1s.Text = lblhRIGHTS_ID.Text = item.LabelName;
                if (lblUSER_ID1s.ID == item.LabelID)
                    txtUSER_ID1s.Text = lblUSER_ID1s.Text = item.LabelName;
                else if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    txtPRIVILEGE_ID1s.Text = lblPRIVILEGE_ID1s.Text = lblhPRIVILEGE_ID.Text = item.LabelName;
                else if (lblADD_FLAG1s.ID == item.LabelID)
                    txtADD_FLAG1s.Text = lblADD_FLAG1s.Text = item.LabelName;
                else if (lblMODIFY_FLAG1s.ID == item.LabelID)
                    txtMODIFY_FLAG1s.Text = lblMODIFY_FLAG1s.Text = lblhMODIFY_FLAG.Text = item.LabelName;
                else if (lblDELETE_FLAG1s.ID == item.LabelID)
                    txtDELETE_FLAG1s.Text = lblDELETE_FLAG1s.Text = item.LabelName;
                else if (lblVIEW_FLAG1s.ID == item.LabelID)
                    txtVIEW_FLAG1s.Text = lblVIEW_FLAG1s.Text = item.LabelName;
                else if (lblALL_FLAG1s.ID == item.LabelID)
                    txtALL_FLAG1s.Text = lblALL_FLAG1s.Text = lblhALL_FLAG.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblIsLabelUpdate1s.ID == item.LabelID)
                    txtIsLabelUpdate1s.Text = lblIsLabelUpdate1s.Text = item.LabelName;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    txtTENANT_ID2h.Text = lblTENANT_ID2h.Text = lblhTENANT_ID.Text = item.LabelName;
                //else if (lblRIGHTS_ID2h.ID == item.LabelID)
                //    txtRIGHTS_ID2h.Text = lblRIGHTS_ID2h.Text = lblhRIGHTS_ID.Text = item.LabelName;
                else if (lblUSER_ID2h.ID == item.LabelID)
                    txtUSER_ID2h.Text = lblUSER_ID2h.Text = item.LabelName;
                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    txtPRIVILEGE_ID2h.Text = lblPRIVILEGE_ID2h.Text = lblhPRIVILEGE_ID.Text = item.LabelName;
                else if (lblADD_FLAG2h.ID == item.LabelID)
                    txtADD_FLAG2h.Text = lblADD_FLAG2h.Text = item.LabelName;
                else if (lblMODIFY_FLAG2h.ID == item.LabelID)
                    txtMODIFY_FLAG2h.Text = lblMODIFY_FLAG2h.Text = lblhMODIFY_FLAG.Text = item.LabelName;
                else if (lblDELETE_FLAG2h.ID == item.LabelID)
                    txtDELETE_FLAG2h.Text = lblDELETE_FLAG2h.Text = item.LabelName;
                else if (lblVIEW_FLAG2h.ID == item.LabelID)
                    txtVIEW_FLAG2h.Text = lblVIEW_FLAG2h.Text = item.LabelName;
                else if (lblALL_FLAG2h.ID == item.LabelID)
                    txtALL_FLAG2h.Text = lblALL_FLAG2h.Text = lblhALL_FLAG.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblIsLabelUpdate2h.ID == item.LabelID)
                    txtIsLabelUpdate2h.Text = lblIsLabelUpdate2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_USER_RIGHTS").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\ACM_USER_RIGHTS.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("ACM_USER_RIGHTS").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                //if (lblRIGHTS_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtRIGHTS_ID1s.Text;
                if (lblUSER_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSER_ID1s.Text;
                else if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID1s.Text;
                else if (lblADD_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADD_FLAG1s.Text;
                else if (lblMODIFY_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODIFY_FLAG1s.Text;
                else if (lblDELETE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDELETE_FLAG1s.Text;
                else if (lblVIEW_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtVIEW_FLAG1s.Text;
                else if (lblALL_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtALL_FLAG1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblIsLabelUpdate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIsLabelUpdate1s.Text;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID2h.Text;
                //else if (lblRIGHTS_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtRIGHTS_ID2h.Text;
                else if (lblUSER_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUSER_ID2h.Text;
                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID2h.Text;
                else if (lblADD_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADD_FLAG2h.Text;
                else if (lblMODIFY_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODIFY_FLAG2h.Text;
                else if (lblDELETE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDELETE_FLAG2h.Text;
                else if (lblVIEW_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtVIEW_FLAG2h.Text;
                else if (lblALL_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtALL_FLAG2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                else if (lblIsLabelUpdate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIsLabelUpdate2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\ACM_USER_RIGHTS.xml"));

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
            // drpRIGHTS_ID.Enabled = true;
            drpUSER_ID.Enabled = true;
            drpPRIVILEGE_ID.Enabled = true;
            cbADD_FLAG.Checked = true;
            cbMODIFY_FLAG.Checked = true;
            cbDELETE_FLAG.Checked = true;
            cbVIEW_FLAG.Checked = true;
            cbALL_FLAG.Checked = true;
            //txtCRUP_ID.Enabled = true;
            cbIsLabelUpdate.Checked = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            // drpRIGHTS_ID.Enabled = false;
            drpUSER_ID.Enabled = false;
            drpPRIVILEGE_ID.Enabled = false;
            cbADD_FLAG.Checked = false;
            cbMODIFY_FLAG.Checked = false;
            cbDELETE_FLAG.Checked = false;
            cbVIEW_FLAG.Checked = false;
            cbALL_FLAG.Checked = false;
            //txtCRUP_ID.Enabled = false;
            cbIsLabelUpdate.Checked = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.USER_RIGHTS.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.USER_RIGHTS.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.USER_RIGHTS.Count();
                take = Showdata;
                Skip = 0;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.USER_RIGHTS.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).Take(take).Skip(Skip)).ToList());
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

                        Database.USER_RIGHTS objSOJobDesc = DB.USER_RIGHTS.Single(p => p.RIGHTS_ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.USER_RIGHTS objUSER_RIGHTS = DB.USER_RIGHTS.Single(p => p.RIGHTS_ID == ID);
                        //if (objUSER_RIGHTS.RIGHTS_ID != null)
                        //{
                        //    drpRIGHTS_ID.SelectedValue = objUSER_RIGHTS.RIGHTS_ID.ToString();
                        //}
                        if (objUSER_RIGHTS.USER_ID != null && objUSER_RIGHTS.USER_ID != 0)
                        {
                            //drpUSER_ID.SelectedValue = objUSER_RIGHTS.USER_ID.ToString();
                        }
                        if (objUSER_RIGHTS.PRIVILEGE_ID != null && objUSER_RIGHTS.PRIVILEGE_ID != 0)
                        {
                            // drpPRIVILEGE_ID.SelectedValue = objUSER_RIGHTS.PRIVILEGE_ID.ToString();
                        }
                        if (objUSER_RIGHTS.TenentID != null && objUSER_RIGHTS.TenentID != 0)
                        {
                            // DrpTENANT_ID.SelectedValue = objUSER_RIGHTS.TenentID.ToString();
                        }
                        cbADD_FLAG.Checked = (objUSER_RIGHTS.ADD_FLAG == true) ? true : false;
                        cbMODIFY_FLAG.Checked = (objUSER_RIGHTS.MODIFY_FLAG == true) ? true : false;
                        cbDELETE_FLAG.Checked = (objUSER_RIGHTS.DELETE_FLAG == true) ? true : false;
                        cbVIEW_FLAG.Checked = (objUSER_RIGHTS.VIEW_FLAG == true) ? true : false;
                        cbALL_FLAG.Checked = (objUSER_RIGHTS.ALL_FLAG == true) ? true : false;
                        //txtCRUP_ID.Text = objUSER_RIGHTS.CRUP_ID.ToString();
                        cbIsLabelUpdate.Checked = (objUSER_RIGHTS.IsLabelUpdate == true) ? true : false;

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
            int Totalrec = DB.USER_RIGHTS.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.USER_RIGHTS.OrderBy(m => m.RIGHTS_ID).Take(Tvalue).Skip(Svalue)).ToList());
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

        //protected void DrpTENANT_ID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
        //    {
        //        int TID1 = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
        //        drpUSER_ID.Items.Clear();
        //        var Datas = (from fun1 in DB.USER_MST
        //                     where fun1.TenentID == TID1
        //                     select new
        //                     {
        //                         Name = fun1.FIRST_NAME + "" + fun1.LAST_NAME,
        //                         ID = fun1.USER_ID

        //                     }
        //).ToList();
        //        drpUSER_ID.DataSource = Datas;
        //        drpUSER_ID.DataTextField = "Name";
        //        drpUSER_ID.DataValueField = "ID";
        //        drpUSER_ID.DataBind();
        //        drpUSER_ID.Items.Insert(0, new ListItem("---Select---", "0"));

        //        drpPRIVILEGE_ID.Items.Clear();
        //        drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.TenentID == TID1);
        //        drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
        //        drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
        //        drpPRIVILEGE_ID.DataBind();
        //        drpPRIVILEGE_ID.Items.Insert(0, new ListItem("---Select---", "0"));
        //    }
        //}

        protected void DrpTENANT_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);

            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
            //select * from TBLLOCATION where TenentID=1

            //drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
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

                int TID1 = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                drpUSER_ID.Items.Clear();
                var Datas = (from fun1 in DB.USER_MST
                             where fun1.TenentID == TID1
                             select new
                             {
                                 Name = fun1.FIRST_NAME + "" + fun1.LAST_NAME,
                                 ID = fun1.USER_ID

                             }
        ).ToList();
                drpUSER_ID.DataSource = Datas;
                drpUSER_ID.DataTextField = "Name";
                drpUSER_ID.DataValueField = "ID";
                drpUSER_ID.DataBind();
                drpUSER_ID.Items.Insert(0, new ListItem("---Select---", "0"));

                drpPRIVILEGE_ID.Items.Clear();
                Classes.EcommAdminClass.getdropdown(drpPRIVILEGE_ID, TID1, "", "", "", "PRIVILEGE_MST");
                //select * from PRIVILEGE_MST

                //drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.TenentID == TID1);
                //drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
                //drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
                //drpPRIVILEGE_ID.DataBind();
                //drpPRIVILEGE_ID.Items.Insert(0, new ListItem("---Select---", "0"));

            }

        }
    }
}