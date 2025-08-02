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

namespace NewHRM
{
    public partial class MODULE_MAP : System.Web.UI.Page
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
                if (DB.MODULE_MAP.Count() > 0)
                {
                    FirstData();
                }
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (TID == 0)
                {
                   // PNALGOL.Visible = true;
                }
                else
                {
                    DrpTENANT_ID.SelectedValue = TID.ToString();
                    drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
                    drplocation.DataTextField = "LOCNAME1";
                    drplocation.DataValueField = "LOCATIONID";
                    drplocation.DataBind();
                    drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));
                    drpUserID.DataSource = DB.USER_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TID && p.LOCATION_ID == LID); ;
                    drpUserID.DataTextField = "FIRST_NAME";
                    drpUserID.DataValueField = "USER_ID";
                    drpUserID.DataBind();
                    drpUserID.Items.Insert(0, new ListItem("---Select---", "0"));

                    drplocation.SelectedValue = LID.ToString();
                    ViewState["TenenatID"] = TID;
                    ViewState["LocationID"] = LID;
                }

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.MODULE_MAP> List = DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((Web.ACM.ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator
       

        public void GetShow()
        {
            // lblVIEW_FLAG1s.Attributes["class"]  //lblALL_FLAG1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] =
            lblPRIVILEGE_ID1s.Attributes["class"] = lblMODULE_ID1s.Attributes["class"] = lblUserID1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = "control-label col-md-4  getshow";
            //lblMODULE_MAP_ID1s.Attributes["class"] = lblTenantID1s.Attributes["class"] ==  lblMySerial1s.Attributes["class"] 
            lblPRIVILEGE_ID2h.Attributes["class"] = lblMODULE_ID2h.Attributes["class"] = lblUserID2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblMySerial2h.Attributes["class"] = lblALL_FLAG2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = "control-label col-md-4  gethide";
            //lblMODULE_MAP_ID2h.Attributes["class"] =lblTenantID2h.Attributes["class"] =

            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");
        }

        public void GetHide()
        {
            // lblPRIVILEGE_ID1s.Attributes["class"] = lblMODULE_ID1s.Attributes["class"] = lblMODULE_MAP_ID1s.Attributes["class"] = lblUserID1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblTenantID1s.Attributes["class"] = lblMySerial1s.Attributes["class"] = lblALL_FLAG1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = "control-label col-md-4  gethide";
            //lblPRIVILEGE_ID2h.Attributes["class"] = lblMODULE_ID2h.Attributes["class"] = lblMODULE_MAP_ID2h.Attributes["class"] = lblUserID2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblTenantID2h.Attributes["class"] = lblMySerial2h.Attributes["class"] = lblALL_FLAG2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = "control-label col-md-4  getshow";

            //lblTenantID1s.Attributes["class"] =
            lblPRIVILEGE_ID1s.Attributes["class"] = lblMODULE_ID1s.Attributes["class"] = lblUserID1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblMySerial1s.Attributes["class"] = lblALL_FLAG1s.Attributes["class"] = lblADD_FLAG1s.Attributes["class"] = lblMODIFY_FLAG1s.Attributes["class"] = lblDELETE_FLAG1s.Attributes["class"] = lblVIEW_FLAG1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPRIVILEGE_ID2h.Attributes["class"] = lblMODULE_ID2h.Attributes["class"] = lblUserID2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblMySerial2h.Attributes["class"] = lblALL_FLAG2h.Attributes["class"] = lblADD_FLAG2h.Attributes["class"] = lblMODIFY_FLAG2h.Attributes["class"] = lblDELETE_FLAG2h.Attributes["class"] = lblVIEW_FLAG2h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");
            // lblTenantID2h.Attributes["class"] =
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
            drpPRIVILEGE_ID.SelectedIndex = 0;
            drpMODULE_ID.SelectedIndex = 0;
            // drpMODULE_MAP_ID.SelectedIndex = 0;
            drpUserID.SelectedIndex = 0;
            cbACTIVE_FLAG.Checked = false;
            //drpTenantID.SelectedIndex = 0;
            //txtCRUP_ID.Text = "";
            drpMySerial.SelectedIndex = 0;
            cbALL_FLAG.Checked = false;
            cbADD_FLAG.Checked = false;
            cbDELETE_FLAG.Checked = false;
            cbVIEW_FLAG.Checked = false;
            cbMODIFY_FLAG.Checked = false;


        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            //int TenentID = 0;
            //int LOCATION_ID = 0;
            //int MENU_ID = 0;
            //int MASTER_ID = 0;
            //int MODULE_ID = 0;
            //string MENU_TYPE = "";
            //string MENU_NAME1 = "";
            //string MENU_NAME2 = "";
            //string MENU_NAME3 = "";
            //string LINK = "";
            //string URLREWRITE = "";
            //string MENU_LOCATION = "";
            //decimal MENU_ORDER = 0;
            //string DOC_PARENT = "";
            //int CRUP_ID = 0;
            //int ADDFLAGE = 0;
            //int EDITFLAGE = 0;
            //int DELFLAGE = 0;
            //int PRINTFLAGE = 0;
            //int AMIGLOBALE = 0;
            //int MYPERSONAL = 0;
            //string SMALLTEXT = "";
            //DateTime ACTIVETILLDATE =DateTime.Now;
            //string ICONPATH = "";
            //string COMMANLINE = "";
            //int ACTIVE_FLAG = 0;
            //string METATITLE = "";
            //string METAKEYWORD = "";
            //string METADESCRIPTION = "";
            //string HEADERVISIBLEDATA = "";
            //string HEADERINVISIBLEDATA = "";
            //string FOOTERVISIBLEDATA = "";
            //string FOOTERINVISIBLEDATA = "";
            //int REFID = 0;
            //int MYBUSID = 0;

            int TenentID = 0;
            int LOCATION_ID = 1;
            int PRIVILEGE_ID = 0;
            int MODULE_ID = 0;
            int MODULE_MAP_ID = 0;
            int UserID = 0;
            string ACTIVE_FLAG = "";
            int TenantID = 0;
            int CRUP_ID = 0;
            int MySerial = 0;
            string ALL_FLAG = "";
            string ADD_FLAG = "";
            string MODIFY_FLAG = "";
            string DELETE_FLAG = "";
            string VIEW_FLAG = "";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        btnAdd.ValidationGroup = "s";
                        //Server Content Send data Yogesh
                        if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                        {
                            PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drpMODULE_ID.SelectedValue) != 0)
                        {
                            MODULE_ID = Convert.ToInt32(drpMODULE_ID.SelectedValue);
                        }
                        if (Convert.ToInt32(drpUserID.SelectedValue) != 0)
                        {
                            UserID = Convert.ToInt32(drpUserID.SelectedValue);
                        }
                        // MODULE_MAP_ID = 0;//DB.MODULE_MAP.Count() > 0 ? Convert.ToInt32(DB.MODULE_MAP.Max(p => p.MODULE_MAP_ID) + 1) : 1;

                        ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";

                        if ((ViewState["TenenatID"]) != null)
                        {
                            TenantID = Convert.ToInt32(ViewState["TenenatID"]);
                        }

                        if (ViewState["LocationID"] != null)
                        {

                            LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                        }

                        //objMODULE_MAP.CRUP_ID = txtCRUP_ID.Text;
                        if (Convert.ToInt32(drpMySerial.SelectedValue) != 0)
                        {
                            MySerial = Convert.ToInt32(drpMySerial.SelectedValue);
                        }

                        ALL_FLAG = cbALL_FLAG.Checked ? "Y" : "N";
                        ADD_FLAG = cbADD_FLAG.Checked ? "Y" : "N";
                        MODIFY_FLAG = cbMODIFY_FLAG.Checked ? "Y" : "N";
                        DELETE_FLAG = cbDELETE_FLAG.Checked ? "Y" : "N";
                        VIEW_FLAG = cbVIEW_FLAG.Checked ? "Y" : "N";
                        Classes.ACMClass.InsertDataACMMODULEMAP(TenantID, LOCATION_ID, PRIVILEGE_ID, MODULE_ID, UserID, MODULE_MAP_ID, ACTIVE_FLAG, CRUP_ID, MySerial, ALL_FLAG, ADD_FLAG, MODIFY_FLAG, DELETE_FLAG, VIEW_FLAG);

                     //   Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                      //  BindData();
                        navigation.Visible = true;
                        Readonly();
                       // FirstData();
                        btnAdd.Text = "AddNew";
                        btnAdd.ValidationGroup = "s1";
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            MODULE_MAP_ID = Convert.ToInt32(ViewState["Edit"]);
                            if (Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue) != 0)
                            {
                                PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                            }
                            if (Convert.ToInt32(drpMODULE_ID.SelectedValue) != 0)
                            {
                                MODULE_ID = Convert.ToInt32(drpMODULE_ID.SelectedValue);
                            }
                            if (Convert.ToInt32(drpUserID.SelectedValue) != 0)
                            {
                                UserID = Convert.ToInt32(drpUserID.SelectedValue);
                            }
                            // objMODULE_MAP.MODULE_MAP_ID = Convert.ToInt32(drpMODULE_MAP_ID.SelectedValue);

                            ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            //if (Convert.ToInt32(drpTenantID.SelectedValue) != 0)
                            //{
                            //    TenantID = Convert.ToInt32(drpTenantID.SelectedValue);
                            //}
                            if ((ViewState["TenenatID"]) != null)
                            {
                                TenantID = Convert.ToInt32(ViewState["TenenatID"]);
                            }

                            if (ViewState["LocationID"] != null)
                            {

                                LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                            }
                            //objMODULE_MAP.CRUP_ID = txtCRUP_ID.Text;
                            if (Convert.ToInt32(drpMySerial.SelectedValue) != 0)
                            {
                                MySerial = Convert.ToInt32(drpMySerial.SelectedValue);
                            }
                            ALL_FLAG = cbALL_FLAG.Checked ? "Y" : "N";
                            ADD_FLAG = cbADD_FLAG.Checked ? "Y" : "N";
                            MODIFY_FLAG = cbMODIFY_FLAG.Checked ? "Y" : "N";
                            DELETE_FLAG = cbDELETE_FLAG.Checked ? "Y" : "N";
                            VIEW_FLAG = cbVIEW_FLAG.Checked ? "Y" : "N";
                            Classes.ACMClass.InsertDataACMMODULEMAP(TenantID, LOCATION_ID, PRIVILEGE_ID, MODULE_ID, UserID, MODULE_MAP_ID, ACTIVE_FLAG, CRUP_ID, MySerial, ALL_FLAG, ADD_FLAG, MODIFY_FLAG, DELETE_FLAG, VIEW_FLAG);
                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                          //  Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                          //  BindData();
                            // navigation.Visible = true;
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




            //drpVIEW_FLAG.Items.Insert(0, new ListItem("-- Select --", "0"));drpVIEW_FLAG.DataSource = DB.0;drpVIEW_FLAG.DataTextField = "0";drpVIEW_FLAG.DataValueField = "0";drpVIEW_FLAG.DataBind();
        }
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

            if (Convert.ToInt32(drplocation.SelectedValue) != 0 )
            {

                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                ViewState["TenenatID"] = TID;
                ViewState["LocationID"] = LID;
                int TenentID = 0;
                int LOCATIONID = 0;

                if (ViewState["TenenatID"] != null)
                {
                    TenentID = Convert.ToInt32(ViewState["TenenatID"]);
                }

                if (ViewState["LocationID"] != null)
                {

                    LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);
                }

                var Datas = (from fun11 in DB.USER_MST.Where(p => p.TenentID == TenentID && p.LOCATION_ID == LOCATIONID)
                             select new
                             {
                                 Name = fun11.FIRST_NAME + "" + fun11.LAST_NAME,
                                 ID = fun11.USER_ID

                             }
               ).Distinct();


                //Classes.EcommAdminClass.getdropdown(drpUserID, TID, "", "", "", "USER_MST");
                //select * from USER_MST

                drpUserID.DataSource = DB.USER_MST.Where(p => p.TenentID == TenentID && p.LOCATION_ID == LOCATIONID);
                drpUserID.DataTextField = "FIRST_NAME";
                drpUserID.DataValueField = "USER_ID";
                drpUserID.DataBind();
                drpUserID.Items.Insert(0, new ListItem("---Select User Name---", "0"));


                // var Datas = (from fun1 in DB.ACM_TBLLOCATION
                //              select new
                //              {
                //                  fun1.TenantID,

                //              }
                //).Distinct();
                // drpTenantID.DataSource = Datas;
                // drpTenantID.DataTextField = "TenantID";
                // drpTenantID.DataValueField = "TenantID";
                // drpTenantID.DataBind();
                // drpTenantID.Items.Insert(0, new ListItem("---Select---", "0"));

                Classes.EcommAdminClass.getdropdown(drpMODULE_ID, TID, "0", "", "", "MODULE_MST");
                //select * from MODULE_MST

                //drpMODULE_ID.DataSource = DB.MODULE_MST.Where(p => p.TenentID == TenentID );
                //drpMODULE_ID.DataTextField = "Module_Name";
                //drpMODULE_ID.DataValueField = "Module_Id";
                //drpMODULE_ID.DataBind();
                //drpMODULE_ID.Items.Insert(0, new ListItem("---Select User Name---", "0"));


                //drpMODULE_ID.DataSource = DB.MODULE_MAP;
                //drpMODULE_ID.DataTextField = "Module_Name";
                //drpMODULE_ID.DataValueField = "MODULE_MAP_ID";
                //drpMODULE_ID.DataBind();
                // drpMODULE_MAP_ID.Items.Insert(0, new ListItem("---Select User Name---", "0"));

                Classes.EcommAdminClass.getdropdown(drpMySerial, TID, "", "", "", "MODULE_MAP");
                //select * from MODULE_MAP


                //drpMySerial.DataSource = DB.MODULE_MAP.Where(p => p.TenentID == TenentID && p.LOCATION_ID == LOCATIONID);
                //drpMySerial.DataTextField = "MySerial";
                //drpMySerial.DataValueField = "MODULE_MAP_ID";
                //drpMySerial.DataBind();
                //drpMySerial.Items.Insert(0, new ListItem("---Select User Name---", "0"));

                Classes.EcommAdminClass.getdropdown(drpPRIVILEGE_ID, TID, "", "", "", "PRIVILEGE_MST");
                //select * from PRIVILEGE_MST


                //drpPRIVILEGE_ID.DataSource = DB.PRIVILEGE_MST.Where(p => p.TenentID == TenentID); ;
                //drpPRIVILEGE_ID.DataTextField = "PRIVILEGE_NAME";
                //drpPRIVILEGE_ID.DataValueField = "PRIVILEGE_ID";
                //drpPRIVILEGE_ID.DataBind();
                //drpPRIVILEGE_ID.Items.Insert(0, new ListItem("---Select User Name---", "0"));

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
            //drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            //// drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            // drpMODULE_MAP_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            //drpUserID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            //if ((Listview1.SelectedDataKey[5]).ToString() == "Y")
            //{
            //    cbACTIVE_FLAG.Checked = true;
            //}
            //else
            //{
            //    cbACTIVE_FLAG.Checked = false;
            //}
            //if (Listview1.SelectedDataKey[6] != null)
            //{
            //    drpTenantID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
            //}
            //if (Listview1.SelectedDataKey[8].ToString() != null)
            //{
            //    drpMySerial.SelectedValue = Listview1.SelectedDataKey[8].ToString();
            //}

            //txtCRUP_ID.Text = Listview1.SelectedDataKey[6].ToString();

            if (Listview1.SelectedDataKey[9] == "1")
            {
                cbALL_FLAG.Checked = true;
            }
            else
            {
                cbALL_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[10] == "1")
            {
                cbADD_FLAG.Checked = true;
            }
            else
            {
                cbALL_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[11] == "1")
            {
                cbMODIFY_FLAG.Checked = true;
            }
            else
            {
                cbMODIFY_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[12] == "1")
            {
                cbDELETE_FLAG.Checked = true;
            }
            else
            {
                cbDELETE_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[13] == "1")
            {
                cbVIEW_FLAG.Checked = true;
            }
            else
            {
                cbVIEW_FLAG.Checked = false;
            }

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                // drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                // drpMODULE_MAP_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                //drpUserID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                if ((Listview1.SelectedDataKey[5]).ToString() == "Y")
                {
                    cbACTIVE_FLAG.Checked = true;
                }
                else
                {
                    cbACTIVE_FLAG.Checked = false;
                }
                //if (Listview1.SelectedDataKey[6] != null)
                //{
                //    drpTenantID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                //}
                //if (Listview1.SelectedDataKey[8].ToString() != null)
                //{
                //    drpMySerial.SelectedValue = Listview1.SelectedDataKey[8].ToString();
                //}

                //txtCRUP_ID.Text = Listview1.SelectedDataKey[6].ToString();

                if (Listview1.SelectedDataKey[9] == "1")
                {
                    cbALL_FLAG.Checked = true;
                }
                else
                {
                    cbALL_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[10] == "1")
                {
                    cbADD_FLAG.Checked = true;
                }
                else
                {
                    cbALL_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[11] == "1")
                {
                    cbMODIFY_FLAG.Checked = true;
                }
                else
                {
                    cbMODIFY_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[12] == "1")
                {
                    cbDELETE_FLAG.Checked = true;
                }
                else
                {
                    cbDELETE_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[13] == "1")
                {
                    cbVIEW_FLAG.Checked = true;
                }
                else
                {
                    cbVIEW_FLAG.Checked = false;
                }

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
                //drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                // drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                // drpMODULE_MAP_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                //drpUserID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                if ((Listview1.SelectedDataKey[5]).ToString() == "Y")
                {
                    cbACTIVE_FLAG.Checked = true;
                }
                else
                {
                    cbACTIVE_FLAG.Checked = false;
                }
                //if (Listview1.SelectedDataKey[6] != null)
                //{
                //    drpTenantID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                //}
                //if (Listview1.SelectedDataKey[8].ToString() != null)
                //{
                //    drpMySerial.SelectedValue = Listview1.SelectedDataKey[8].ToString();
                //}

                //txtCRUP_ID.Text = Listview1.SelectedDataKey[6].ToString();

                if (Listview1.SelectedDataKey[9] == "1")
                {
                    cbALL_FLAG.Checked = true;
                }
                else
                {
                    cbALL_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[10] == "1")
                {
                    cbADD_FLAG.Checked = true;
                }
                else
                {
                    cbALL_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[11] == "1")
                {
                    cbMODIFY_FLAG.Checked = true;
                }
                else
                {
                    cbMODIFY_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[12] == "1")
                {
                    cbDELETE_FLAG.Checked = true;
                }
                else
                {
                    cbDELETE_FLAG.Checked = false;
                }
                if (Listview1.SelectedDataKey[13] == "1")
                {
                    cbVIEW_FLAG.Checked = true;
                }
                else
                {
                    cbVIEW_FLAG.Checked = false;
                }

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            // drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            //   drpMODULE_MAP_ID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            //drpUserID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            if ((Listview1.SelectedDataKey[5]).ToString() == "Y")
            {
                cbACTIVE_FLAG.Checked = true;
            }
            else
            {
                cbACTIVE_FLAG.Checked = false;
            }
            //if (Listview1.SelectedDataKey[6] != null)
            //{
            //    drpTenantID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
            //}
            //if (Listview1.SelectedDataKey[8].ToString() != null)
            //{
            //    drpMySerial.SelectedValue = Listview1.SelectedDataKey[8].ToString();
            //}

            //txtCRUP_ID.Text = Listview1.SelectedDataKey[6].ToString();

            if (Listview1.SelectedDataKey[9] == "1")
            {
                cbALL_FLAG.Checked = true;
            }
            else
            {
                cbALL_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[10] == "1")
            {
                cbADD_FLAG.Checked = true;
            }
            else
            {
                cbALL_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[11] == "1")
            {
                cbMODIFY_FLAG.Checked = true;
            }
            else
            {
                cbMODIFY_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[12] == "1")
            {
                cbDELETE_FLAG.Checked = true;
            }
            else
            {
                cbDELETE_FLAG.Checked = false;
            }
            if (Listview1.SelectedDataKey[13] == "1")
            {
                cbVIEW_FLAG.Checked = true;
            }
            else
            {
                cbVIEW_FLAG.Checked = false;
            }

        }

        #endregion

        #region PAge Genarator language
 
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false lblMODULE_MAP_ID2h.Visible=lblTenantID2h.Visible = 
                    lblPRIVILEGE_ID2h.Visible = lblMODULE_ID2h.Visible = lblUserID2h.Visible = lblACTIVE_FLAG2h.Visible = lblMySerial2h.Visible = lblALL_FLAG2h.Visible = lblADD_FLAG2h.Visible = lblMODIFY_FLAG2h.Visible = lblDELETE_FLAG2h.Visible = lblVIEW_FLAG2h.Visible = false;
                    //2true txtMODULE_MAP_ID2h.Visible = 
                    txtPRIVILEGE_ID2h.Visible = txtMODULE_ID2h.Visible = txtUserID2h.Visible = txtACTIVE_FLAG2h.Visible = txtMySerial2h.Visible = txtALL_FLAG2h.Visible = txtADD_FLAG2h.Visible = txtMODIFY_FLAG2h.Visible = txtDELETE_FLAG2h.Visible = txtVIEW_FLAG2h.Visible = true;
                    // = txtTenantID2h.Visible 
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
                    //2true= lblTenantID2h.Visible
                    lblPRIVILEGE_ID2h.Visible = lblMODULE_ID2h.Visible = lblUserID2h.Visible = lblACTIVE_FLAG2h.Visible = lblMySerial2h.Visible = lblALL_FLAG2h.Visible = lblADD_FLAG2h.Visible = lblMODIFY_FLAG2h.Visible = lblDELETE_FLAG2h.Visible = lblVIEW_FLAG2h.Visible = true;
                    //lblMODULE_MAP_ID2h.Visible =
                    //2false= txtTenantID2h.Visible
                    txtPRIVILEGE_ID2h.Visible = txtMODULE_ID2h.Visible = txtUserID2h.Visible = txtACTIVE_FLAG2h.Visible = txtMySerial2h.Visible = txtALL_FLAG2h.Visible = txtADD_FLAG2h.Visible = txtMODIFY_FLAG2h.Visible = txtDELETE_FLAG2h.Visible = txtVIEW_FLAG2h.Visible = false;
                    //= txtMODULE_MAP_ID2h.Visible
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
                    //  lblMODULE_MAP_ID1s.Visible =lblTenantID1s.Visible =
                    lblPRIVILEGE_ID1s.Visible = lblMODULE_ID1s.Visible = lblUserID1s.Visible = lblACTIVE_FLAG1s.Visible = lblMySerial1s.Visible = lblALL_FLAG1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = false;
                    //1true
                    txtPRIVILEGE_ID1s.Visible = txtMODULE_ID1s.Visible = txtUserID1s.Visible = txtACTIVE_FLAG1s.Visible = txtMySerial1s.Visible = txtALL_FLAG1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = true;
                    //   txtMODULE_MAP_ID1s.Visible == txtTenantID1s.Visible
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
                    //lblMODULE_MAP_ID1s.VisiblelblTenantID1s.Visible = = txtTenantID1s.Visible
                    lblPRIVILEGE_ID1s.Visible = lblMODULE_ID1s.Visible = lblUserID1s.Visible = lblACTIVE_FLAG1s.Visible = lblMySerial1s.Visible = lblALL_FLAG1s.Visible = lblADD_FLAG1s.Visible = lblMODIFY_FLAG1s.Visible = lblDELETE_FLAG1s.Visible = lblVIEW_FLAG1s.Visible = true;
                    //1false
                    txtPRIVILEGE_ID1s.Visible = txtMODULE_ID1s.Visible = txtUserID1s.Visible = txtACTIVE_FLAG1s.Visible = txtMySerial1s.Visible = txtALL_FLAG1s.Visible = txtADD_FLAG1s.Visible = txtMODIFY_FLAG1s.Visible = txtDELETE_FLAG1s.Visible = txtVIEW_FLAG1s.Visible = false;
                    //txtMODULE_MAP_ID1s.Visible
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((Web.ACM.ACMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((Web.ACM.ACMMaster)this.Master).Bindxml("ACM_MODULE_MAP").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = item.LabelName;
                if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    txtPRIVILEGE_ID1s.Text = lblPRIVILEGE_ID1s.Text = item.LabelName;
                else if (lblMODULE_ID1s.ID == item.LabelID)
                    txtMODULE_ID1s.Text = lblMODULE_ID1s.Text = lblhMODULE_ID.Text = item.LabelName;
                //else if (lblMODULE_MAP_ID1s.ID == item.LabelID)
                //    txtMODULE_MAP_ID1s.Text = lblMODULE_MAP_ID1s.Text = lblhMODULE_MAP_ID.Text = item.LabelName;
                else if (lblUserID1s.ID == item.LabelID)
                    txtUserID1s.Text = lblUserID1s.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                //else if (lblTenantID1s.ID == item.LabelID)
                //    txtTenantID1s.Text = lblTenantID1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                else if (lblMySerial1s.ID == item.LabelID)
                    txtMySerial1s.Text = lblMySerial1s.Text = lblhMySerial.Text = item.LabelName;
                else if (lblALL_FLAG1s.ID == item.LabelID)
                    txtALL_FLAG1s.Text = lblALL_FLAG1s.Text = item.LabelName;
                else if (lblADD_FLAG1s.ID == item.LabelID)
                    txtADD_FLAG1s.Text = lblADD_FLAG1s.Text = lblhADD_FLAG.Text = item.LabelName;
                else if (lblMODIFY_FLAG1s.ID == item.LabelID)
                    txtMODIFY_FLAG1s.Text = lblMODIFY_FLAG1s.Text = item.LabelName;
                else if (lblDELETE_FLAG1s.ID == item.LabelID)
                    txtDELETE_FLAG1s.Text = lblDELETE_FLAG1s.Text = lblhDELETE_FLAG.Text = item.LabelName;
                else if (lblVIEW_FLAG1s.ID == item.LabelID)
                    txtVIEW_FLAG1s.Text = lblVIEW_FLAG1s.Text = item.LabelName;

                //else if (lblTENANT_ID2h.ID == item.LabelID)
                //    txtTENANT_ID2h.Text = lblTENANT_ID2h.Text = item.LabelName;
                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    txtPRIVILEGE_ID2h.Text = lblPRIVILEGE_ID2h.Text = item.LabelName;
                else if (lblMODULE_ID2h.ID == item.LabelID)
                    txtMODULE_ID2h.Text = lblMODULE_ID2h.Text = lblhMODULE_ID.Text = item.LabelName;
                //else if (lblMODULE_MAP_ID2h.ID == item.LabelID)
                //    txtMODULE_MAP_ID2h.Text = lblMODULE_MAP_ID2h.Text = lblhMODULE_MAP_ID.Text = item.LabelName;
                else if (lblUserID2h.ID == item.LabelID)
                    txtUserID2h.Text = lblUserID2h.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                //else if (lblTenantID2h.ID == item.LabelID)
                //    txtTenantID2h.Text = lblTenantID2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;
                else if (lblMySerial2h.ID == item.LabelID)
                    txtMySerial2h.Text = lblMySerial2h.Text = lblhMySerial.Text = item.LabelName;
                else if (lblALL_FLAG2h.ID == item.LabelID)
                    txtALL_FLAG2h.Text = lblALL_FLAG2h.Text = item.LabelName;
                else if (lblADD_FLAG2h.ID == item.LabelID)
                    txtADD_FLAG2h.Text = lblADD_FLAG2h.Text = lblhADD_FLAG.Text = item.LabelName;
                else if (lblMODIFY_FLAG2h.ID == item.LabelID)
                    txtMODIFY_FLAG2h.Text = lblMODIFY_FLAG2h.Text = item.LabelName;
                else if (lblDELETE_FLAG2h.ID == item.LabelID)
                    txtDELETE_FLAG2h.Text = lblDELETE_FLAG2h.Text = lblhDELETE_FLAG.Text = item.LabelName;
                else if (lblVIEW_FLAG2h.ID == item.LabelID)
                    txtVIEW_FLAG2h.Text = lblVIEW_FLAG2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((Web.ACM.ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((Web.ACM.ACMMaster)this.Master).Bindxml("ACM_MODULE_MAP").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\ACM_MODULE_MAP.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((Web.ACM.ACMMaster)this.Master).Bindxml("ACM_MODULE_MAP").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                if (lblPRIVILEGE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID1s.Text;
                else if (lblMODULE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODULE_ID1s.Text;
                //else if (lblMODULE_MAP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMODULE_MAP_ID1s.Text;
                else if (lblUserID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUserID1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                //else if (lblTenantID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblMySerial1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMySerial1s.Text;
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

                //else if (lblTENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID2h.Text;
                else if (lblPRIVILEGE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_ID2h.Text;
                else if (lblMODULE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODULE_ID2h.Text;
                //else if (lblMODULE_MAP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtMODULE_MAP_ID2h.Text;
                else if (lblUserID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUserID2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                //else if (lblTenantID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                else if (lblMySerial2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMySerial2h.Text;
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
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\ACM_MODULE_MAP.xml"));

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
            drpPRIVILEGE_ID.Enabled = true;
            drpMODULE_ID.Enabled = true;
            //  drpMODULE_MAP_ID.Enabled = true;
            drpUserID.Enabled = true;
            cbACTIVE_FLAG.Enabled = true;
            //drpTenantID.Enabled = true;
            //txtCRUP_ID.Enabled = true;
            drpMySerial.Enabled = true;
            cbALL_FLAG.Enabled = true;
            cbADD_FLAG.Enabled = true;
            cbMODIFY_FLAG.Enabled = true;
            cbDELETE_FLAG.Enabled = true;
            cbVIEW_FLAG.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drpPRIVILEGE_ID.Enabled = false;
            drpMODULE_ID.Enabled = false;
            //  drpMODULE_MAP_ID.Enabled = false;
            drpUserID.Enabled = false;
            cbACTIVE_FLAG.Enabled = false;
            //drpTenantID.Enabled = false;
            //txtCRUP_ID.Enabled = false;
            drpMySerial.Enabled = false;
            cbALL_FLAG.Enabled = false;
            cbADD_FLAG.Enabled = false;
            cbMODIFY_FLAG.Enabled = false;
            cbDELETE_FLAG.Enabled = false;
            cbVIEW_FLAG.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.MODULE_MAP.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((Web.ACM.ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).Take(take).Skip(Skip)).ToList());
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

            //((Web.ACM.ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.MODULE_MAP.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((Web.ACM.ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).Take(take).Skip(Skip)).ToList());
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
                //((Web.ACM.ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.MODULE_MAP.Count();
                take = Showdata;
                Skip = 0;
                ((Web.ACM.ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                //((Web.ACM.ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB.MODULE_MAP.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((Web.ACM.ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            //((Web.ACM.ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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

                        Database.MODULE_MAP objSOJobDesc = DB.MODULE_MAP.Single(p => p.MODULE_MAP_ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((Web.ACM.ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        btnAdd.ValidationGroup = "s";
                        Database.MODULE_MAP objMODULE_MAP = DB.MODULE_MAP.Single(p => p.MODULE_MAP_ID == ID);
                        if (objMODULE_MAP.PRIVILEGE_ID != null)
                        {
                            drpPRIVILEGE_ID.SelectedValue = objMODULE_MAP.PRIVILEGE_ID.ToString();
                        }
                        if (objMODULE_MAP.MODULE_ID != null)
                        {
                            drpMODULE_ID.SelectedValue = objMODULE_MAP.MODULE_ID.ToString();
                        }
                        if (objMODULE_MAP.UserID != null)
                        {
                            drpUserID.SelectedValue = objMODULE_MAP.UserID.ToString();
                        }
                        //if (objMODULE_MAP.TenantID != null)
                        //{
                        //    drpTenantID.SelectedValue = objMODULE_MAP.TenantID.ToString();
                        //}
                        //if (objMODULE_MAP.MySerial != null)
                        //{
                        //    drpMySerial.SelectedValue = objMODULE_MAP.MySerial.ToString();
                        //}
                        if (objMODULE_MAP.ACTIVE_FLAG != "Y")
                        {
                            cbACTIVE_FLAG.Checked = true;
                        }
                        else
                        {
                            cbACTIVE_FLAG.Checked = false;
                        }

                        if (objMODULE_MAP.ALL_FLAG != "Y")
                        {
                            cbALL_FLAG.Checked = true;
                        }
                        else
                        {
                            cbALL_FLAG.Checked = false;
                        }

                        if (objMODULE_MAP.ADD_FLAG != "Y")
                        {
                            cbALL_FLAG.Checked = true;
                        }
                        else
                        {
                            cbADD_FLAG.Checked = false;
                        }
                        if (objMODULE_MAP.MODIFY_FLAG != "Y")
                        {
                            cbMODIFY_FLAG.Checked = true;
                        }
                        else
                        {
                            cbMODIFY_FLAG.Checked = false;
                        }
                        if (objMODULE_MAP.DELETE_FLAG != "Y")
                        {
                            cbDELETE_FLAG.Checked = true;
                        }
                        else
                        {
                            cbDELETE_FLAG.Checked = false;
                        }
                        if (objMODULE_MAP.VIEW_FLAG != "Y")
                        {
                            cbVIEW_FLAG.Checked = true;
                        }
                        else
                        {
                            cbVIEW_FLAG.Checked = false;
                        }
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
            int Totalrec = DB.MODULE_MAP.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((Web.ACM.ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MAP.OrderBy(m => m.MODULE_MAP_ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                //((Web.ACM.ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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