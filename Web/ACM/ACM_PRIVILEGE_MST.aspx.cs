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
    public partial class PRIVILEGE_MST : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TenentID = 0;
        int TID = 0;
        int LOCATIONID = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Control c = this.Master.FindControl("pnelView");// "masterDiv"= the Id of the div.
            //c.Visible = true;
            //if (ViewState["TenenatID"] != null)
            //{
            //    TenentID = Convert.ToInt32(ViewState["TenenatID"]);
            //}

            //if ( ViewState["LocationID"] != null)
            //{

            //    LOCATIONID = Convert.ToInt32( ViewState["LocationID"]);
            //    drpMODULE_ID.Items.Clear();
            //    drpMODULE_ID.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TenentID && p.LOCATION_ID == LOCATIONID);
            //    drpMODULE_ID.DataTextField = "Module_Name";
            //    drpMODULE_ID.DataValueField = "Module_Id";
            //    drpMODULE_ID.DataBind();
            //    drpMODULE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Module Name--", "0"));
            //}
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                if (DB.PRIVILEGE_MST.Count() > 0)
                {
                    //   FirstData();
                }
                BindData();

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
                    Classes.EcommAdminClass.getdropdown(drpMODULE_ID, TID, "0", "", "", "MODULE_MST");
                    ViewState["TenenatID"] = TID;
                    ViewState["LocationID"] = LID;
                }


            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            List<Database.PRIVILEGE_MST> List = DB.PRIVILEGE_MST.Where(p => p.TenentID == TID  && p.ACTIVE_FLAG == "Y").OrderBy(m => m.PRIVILEGE_ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator
      
        public void GetShow()
        {

            lblPRIVILEGE_NAME1s.Attributes["class"] = lblPRIVILEGE_DESC1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblMODULE_ID1s.Attributes["class"] = lblPRIVILEGE_NAME11s.Attributes["class"] = lblPRIVILEGE_NAME21s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPRIVILEGE_NAME2h.Attributes["class"] = lblPRIVILEGE_DESC2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblMODULE_ID2h.Attributes["class"] = lblPRIVILEGE_NAME12h.Attributes["class"] = lblPRIVILEGE_NAME22h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPRIVILEGE_NAME1s.Attributes["class"] = lblPRIVILEGE_DESC1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblMODULE_ID1s.Attributes["class"] = lblPRIVILEGE_NAME11s.Attributes["class"] = lblPRIVILEGE_NAME21s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPRIVILEGE_NAME2h.Attributes["class"] = lblPRIVILEGE_DESC2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblMODULE_ID2h.Attributes["class"] = lblPRIVILEGE_NAME12h.Attributes["class"] = lblPRIVILEGE_NAME22h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //  drpPRIVILEGE_ID.SelectedIndex = 0;
            txtPRIVILEGE_NAME.Text = "";
            txtPRIVILEGE_DESC.Text = "";
            cbACTIVE_FLAG.Checked = false;
            //txtCRUP_ID.Text = "";
            drpMODULE_ID.SelectedIndex = 0;
            txtPRIVILEGE_NAME1.Text = "";
            txtPRIVILEGE_NAME2.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TenentID = 0;
                int LOCATION_ID = 0;
                int PRIVILEGE_ID = 0;
                string PRIVILEGE_NAME = "";
                string PRIVILEGE_DESC = "";
                string ACTIVE_FLAG = "";
                int CRUP_ID = 0;
                int MODULE_ID = 0;
                string PRIVILEGE_NAME1 = "";
                string PRIVILEGE_NAME2 = "";
                try
                {
                    TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
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
                        //if ((drpTENANT_ID.SelectedItem.Text) != "--Select--")
                        //{
                        //    TenentID = Convert.ToInt32(drpTENANT_ID.SelectedValue);
                        //}
                        if (ViewState["TenenatID"] != null)
                        {
                            TID = Convert.ToInt32(ViewState["TenenatID"]);
                        }

                        if ( ViewState["LocationID"] != null)
                        {

                            LOCATION_ID = LID = Convert.ToInt32( ViewState["LocationID"]);
                        }
                        int privilageID = 0;//DB.PRIVILEGE_MST.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.PRIVILEGE_MST.Where(p => p.TenentID == TID).Max(p => p.PRIVILEGE_ID) + 1) : 1;
                        PRIVILEGE_ID = privilageID;//DB.PRIVILEGE_MST.Count() > 0 ? Convert.ToInt32(DB.PRIVILEGE_MST.Where (p=>p.LOCATION_ID ==LID&&p.TenentID ==TID).Max(p => p.PRIVILEGE_ID) + 1) : 1;
                        PRIVILEGE_NAME = txtPRIVILEGE_NAME.Text;
                        PRIVILEGE_DESC = txtPRIVILEGE_DESC.Text;
                        ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                        //CRUP_ID = txtCRUP_ID.Text;
                        if (drpMODULE_ID.SelectedValue!= "0")
                        {
                            MODULE_ID = Convert.ToInt32(drpMODULE_ID.SelectedValue);
                        }
                        PRIVILEGE_NAME1 = txtPRIVILEGE_NAME1.Text;
                        PRIVILEGE_NAME2 = txtPRIVILEGE_NAME2.Text;
                        Classes.ACMClass.InsertDataACMPRIVILLEGEMASTER(TenentID, LOCATION_ID, PRIVILEGE_ID, PRIVILEGE_NAME, PRIVILEGE_DESC, ACTIVE_FLAG, CRUP_ID, MODULE_ID, PRIVILEGE_NAME1, PRIVILEGE_NAME2);
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        navigation.Visible = true;
                        Readonly();
                        //  FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            PRIVILEGE_ID = Convert.ToInt32(ViewState["Edit"]);
                            //if ((drpTENANT_ID.SelectedItem.Text) != "--Select--")
                            //{
                            //    TenentID = Convert.ToInt32(drpTENANT_ID.SelectedValue);
                            //}
                            if (ViewState["TenenatID"] != null)
                            {
                                TenentID = Convert.ToInt32(ViewState["TenenatID"]);
                            }

                            if ( ViewState["LocationID"] != null)
                            {

                                LOCATION_ID = LID = Convert.ToInt32( ViewState["LocationID"]);
                            }

                            // PRIVILEGE_ID = Convert.ToInt32(drpPRIVILEGE_ID.SelectedValue);
                            PRIVILEGE_NAME = txtPRIVILEGE_NAME.Text;
                            PRIVILEGE_DESC = txtPRIVILEGE_DESC.Text;
                            ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            //CRUP_ID = txtCRUP_ID.Text;
                            MODULE_ID = Convert.ToInt32(drpMODULE_ID.SelectedValue);
                            PRIVILEGE_NAME1 = txtPRIVILEGE_NAME1.Text;
                            PRIVILEGE_NAME2 = txtPRIVILEGE_NAME2.Text;
                            Classes.ACMClass.InsertDataACMPRIVILLEGEMASTER(TenentID, LOCATION_ID, PRIVILEGE_ID, PRIVILEGE_NAME, PRIVILEGE_DESC, ACTIVE_FLAG, CRUP_ID, MODULE_ID, PRIVILEGE_NAME1, PRIVILEGE_NAME2);
                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            navigation.Visible = true;
                            Readonly();
                            //    FirstData ();
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
            DrpTENANT_ID.Items.Insert(0, new ListItem("---Select---", "0"));
            drpMODULE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Module Name--", "0"));
            drplocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Module Name--", "0"));
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
            //   drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPRIVILEGE_NAME.Text = Listview1.SelectedDataKey[3].ToString();
            txtPRIVILEGE_DESC.Text = Listview1.SelectedDataKey[4].ToString();
            cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[5]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[5].ToString();
            drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[7].ToString();
            txtPRIVILEGE_NAME1.Text = Listview1.SelectedDataKey[8].ToString();
            txtPRIVILEGE_NAME2.Text = Listview1.SelectedDataKey[9].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //    drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                txtPRIVILEGE_NAME.Text = Listview1.SelectedDataKey[2].ToString();
                txtPRIVILEGE_DESC.Text = Listview1.SelectedDataKey[3].ToString();
                cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[5].ToString();
                drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                txtPRIVILEGE_NAME1.Text = Listview1.SelectedDataKey[7].ToString();
                txtPRIVILEGE_NAME2.Text = Listview1.SelectedDataKey[8].ToString();

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
                //     drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                txtPRIVILEGE_NAME.Text = Listview1.SelectedDataKey[2].ToString();
                txtPRIVILEGE_DESC.Text = Listview1.SelectedDataKey[3].ToString();
                cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[5].ToString();
                drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
                txtPRIVILEGE_NAME1.Text = Listview1.SelectedDataKey[7].ToString();
                txtPRIVILEGE_NAME2.Text = Listview1.SelectedDataKey[8].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //  drpPRIVILEGE_ID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            txtPRIVILEGE_NAME.Text = Listview1.SelectedDataKey[2].ToString();
            txtPRIVILEGE_DESC.Text = Listview1.SelectedDataKey[3].ToString();
            cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[4]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[5].ToString();
            drpMODULE_ID.SelectedValue = Listview1.SelectedDataKey[6].ToString();
            txtPRIVILEGE_NAME1.Text = Listview1.SelectedDataKey[7].ToString();
            txtPRIVILEGE_NAME2.Text = Listview1.SelectedDataKey[8].ToString();

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
                    lblPRIVILEGE_NAME2h.Visible = lblPRIVILEGE_DESC2h.Visible = lblACTIVE_FLAG2h.Visible = lblMODULE_ID2h.Visible = lblPRIVILEGE_NAME12h.Visible = lblPRIVILEGE_NAME22h.Visible = false;
                    //2true
                    txtPRIVILEGE_NAME2h.Visible = txtPRIVILEGE_DESC2h.Visible = txtACTIVE_FLAG2h.Visible = txtMODULE_ID2h.Visible = txtPRIVILEGE_NAME12h.Visible = txtPRIVILEGE_NAME22h.Visible = true;

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
                    lblPRIVILEGE_NAME2h.Visible = lblPRIVILEGE_DESC2h.Visible = lblACTIVE_FLAG2h.Visible = lblMODULE_ID2h.Visible = lblPRIVILEGE_NAME12h.Visible = lblPRIVILEGE_NAME22h.Visible = true;
                    //2false
                    txtPRIVILEGE_NAME2h.Visible = txtPRIVILEGE_DESC2h.Visible = txtACTIVE_FLAG2h.Visible = txtMODULE_ID2h.Visible = txtPRIVILEGE_NAME12h.Visible = txtPRIVILEGE_NAME22h.Visible = false;

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
                    lblPRIVILEGE_NAME1s.Visible = lblPRIVILEGE_DESC1s.Visible = lblACTIVE_FLAG1s.Visible = lblMODULE_ID1s.Visible = lblPRIVILEGE_NAME11s.Visible = lblPRIVILEGE_NAME21s.Visible = false;
                    //1true
                    txtPRIVILEGE_NAME1s.Visible = txtPRIVILEGE_DESC1s.Visible = txtACTIVE_FLAG1s.Visible = txtMODULE_ID1s.Visible = txtPRIVILEGE_NAME11s.Visible = txtPRIVILEGE_NAME21s.Visible = true;
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
                    lblPRIVILEGE_NAME1s.Visible = lblPRIVILEGE_DESC1s.Visible = lblACTIVE_FLAG1s.Visible = lblMODULE_ID1s.Visible = lblPRIVILEGE_NAME11s.Visible = lblPRIVILEGE_NAME21s.Visible = true;
                    //1false
                    txtPRIVILEGE_NAME1s.Visible = txtPRIVILEGE_DESC1s.Visible = txtACTIVE_FLAG1s.Visible = txtMODULE_ID1s.Visible = txtPRIVILEGE_NAME11s.Visible = txtPRIVILEGE_NAME21s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_PRIVILEGE_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = lblhTENANT_ID.Text = item.LabelName;
                if (lblPRIVILEGE_NAME1s.ID == item.LabelID)
                    txtPRIVILEGE_NAME1s.Text = lblPRIVILEGE_NAME1s.Text = lblhPRIVILEGE_NAME.Text = item.LabelName;
                else if (lblPRIVILEGE_DESC1s.ID == item.LabelID)
                    txtPRIVILEGE_DESC1s.Text = lblPRIVILEGE_DESC1s.Text = lblhPRIVILEGE_DESC.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblMODULE_ID1s.ID == item.LabelID)
                    txtMODULE_ID1s.Text = lblMODULE_ID1s.Text = item.LabelName;
                else if (lblPRIVILEGE_NAME11s.ID == item.LabelID)
                    txtPRIVILEGE_NAME11s.Text = lblPRIVILEGE_NAME11s.Text = lblhPRIVILEGE_NAME1.Text = item.LabelName;
                else if (lblPRIVILEGE_NAME21s.ID == item.LabelID)
                    txtPRIVILEGE_NAME21s.Text = lblPRIVILEGE_NAME21s.Text = lblhPRIVILEGE_NAME2.Text = item.LabelName;

                else if (lblPRIVILEGE_NAME2h.ID == item.LabelID)
                    txtPRIVILEGE_NAME2h.Text = lblPRIVILEGE_NAME2h.Text = lblhPRIVILEGE_NAME.Text = item.LabelName;
                else if (lblPRIVILEGE_DESC2h.ID == item.LabelID)
                    txtPRIVILEGE_DESC2h.Text = lblPRIVILEGE_DESC2h.Text = lblhPRIVILEGE_DESC.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblMODULE_ID2h.ID == item.LabelID)
                    txtMODULE_ID2h.Text = lblMODULE_ID2h.Text = item.LabelName;
                else if (lblPRIVILEGE_NAME12h.ID == item.LabelID)
                    txtPRIVILEGE_NAME12h.Text = lblPRIVILEGE_NAME12h.Text = lblhPRIVILEGE_NAME1.Text = item.LabelName;
                else if (lblPRIVILEGE_NAME22h.ID == item.LabelID)
                    txtPRIVILEGE_NAME22h.Text = lblPRIVILEGE_NAME22h.Text = lblhPRIVILEGE_NAME2.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_PRIVILEGE_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\ACM_PRIVILEGE_MST.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("ACM_PRIVILEGE_MST").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                if (lblPRIVILEGE_NAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_NAME1s.Text;
                else if (lblPRIVILEGE_DESC1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_DESC1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblMODULE_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODULE_ID1s.Text;
                else if (lblPRIVILEGE_NAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_NAME11s.Text;
                else if (lblPRIVILEGE_NAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_NAME21s.Text;
                else if (lblPRIVILEGE_NAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_NAME2h.Text;
                else if (lblPRIVILEGE_DESC2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_DESC2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                else if (lblMODULE_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMODULE_ID2h.Text;
                else if (lblPRIVILEGE_NAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_NAME12h.Text;
                else if (lblPRIVILEGE_NAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPRIVILEGE_NAME22h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\xml\\ACM_PRIVILEGE_MST.xml"));

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
            //  drpPRIVILEGE_ID.Enabled = true;
            txtPRIVILEGE_NAME.Enabled = true;
            txtPRIVILEGE_DESC.Enabled = true;
            cbACTIVE_FLAG.Checked = true;
            //txtCRUP_ID.Enabled = true;
            drpMODULE_ID.Enabled = true;
            txtPRIVILEGE_NAME1.Enabled = true;
            txtPRIVILEGE_NAME2.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //  drpPRIVILEGE_ID.Enabled = false;
            txtPRIVILEGE_NAME.Enabled = false;
            txtPRIVILEGE_DESC.Enabled = false;
            cbACTIVE_FLAG.Checked = false;
            //txtCRUP_ID.Enabled = false;
            drpMODULE_ID.Enabled = false;
            txtPRIVILEGE_NAME1.Enabled = false;
            txtPRIVILEGE_NAME2.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.PRIVILEGE_MST.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILEGE_MST.OrderBy(m => m.MODULE_ID).Take(take).Skip(Skip)).ToList());
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

        //    ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.PRIVILEGE_MST.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILEGE_MST.OrderBy(m => m.MODULE_ID).Take(take).Skip(Skip)).ToList());
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
        //        ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.PRIVILEGE_MST.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILEGE_MST.OrderBy(m => m.MODULE_ID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        //    int Totalrec = DB.PRIVILEGE_MST.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILEGE_MST.OrderBy(m => m.MODULE_ID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
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
            //    FirstData();
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

                        Database.PRIVILEGE_MST objSOJobDesc = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.PRIVILEGE_MST objPRIVILEGE_MST = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == ID);
                        //  drpPRIVILEGE_ID.SelectedValue = objPRIVILEGE_MST.PRIVILEGE_ID.ToString();
                        txtPRIVILEGE_NAME.Text = objPRIVILEGE_MST.PRIVILEGE_NAME.ToString();
                        txtPRIVILEGE_DESC.Text = objPRIVILEGE_MST.PRIVILEGE_DESC.ToString();
                        cbACTIVE_FLAG.Text = objPRIVILEGE_MST.ACTIVE_FLAG.ToString();
                        //txtCRUP_ID.Text = objPRIVILEGE_MST.CRUP_ID.ToString();
                        drpMODULE_ID.SelectedValue = objPRIVILEGE_MST.MODULE_ID.ToString();
                        txtPRIVILEGE_NAME1.Text = objPRIVILEGE_MST.PRIVILEGE_NAME1.ToString();
                        txtPRIVILEGE_NAME2.Text = objPRIVILEGE_MST.PRIVILEGE_NAME2.ToString();

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

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.PRIVILEGE_MST.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.PRIVILEGE_MST.OrderBy(m => m.MODULE_ID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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


        //protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if ((drpTENANT_ID.SelectedItem.Text) != "--Select--" && Convert.ToInt32(DrpLocation.SelectedValue) != 0)
        //    {
        //        int TeneID = Convert.ToInt32(drpTENANT_ID.SelectedValue);
        //        int LocID = Convert.ToInt32(DrpLocation.SelectedValue);
        //        drpMODULE_ID.Items.Clear();
        //        drpMODULE_ID.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TeneID && p.LOCATION_ID == LocID);
        //        drpMODULE_ID.DataTextField = "Module_Name";
        //        drpMODULE_ID.DataValueField = "Module_Id";
        //        drpMODULE_ID.DataBind();
        //        drpMODULE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Module Name--", "0"));
        //    }
        //}
        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(drplocation.SelectedValue) != 0 && Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
            {

                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                ViewState["TenenatID"] = TID;
                ViewState["LocationID"] = LID;
                drpMODULE_ID.Items.Clear();

                Classes.EcommAdminClass.getdropdown(drpMODULE_ID, TID, "0", "", "", "MODULE_MST");
                //select * from MODULE_MST

                //drpMODULE_ID.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TID);
                //drpMODULE_ID.DataTextField = "Module_Name";
                //drpMODULE_ID.DataValueField = "Module_Id";
                //drpMODULE_ID.DataBind();
                //drpMODULE_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Module Name--", "0"));

            }

        }
    }
}