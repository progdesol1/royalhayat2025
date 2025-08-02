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
using Web.ACM;

namespace Web.ACM
{
    public partial class ACM_ROLE_MST : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        int LOCATIONID = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Control c = this.Master.FindControl("pnelView");// "masterDiv"= the Id of the div.
            //c.Visible = true;
            //if (ViewState["TenenatID"] != null && ViewState["LocationID"] != null)
            //{
            //    TenentID = Convert.ToInt32(ViewState["TenenatID"]);
            //    LOCATIONID = Convert.ToInt32(ViewState["LocationID"]);
            //    // FillContractorID();
            //}
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
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
                btnAdd.ValidationGroup = "ss";
                if (DB.ROLE_MST.Count() > 0)
                {
                    FirstData();
                }
                if (TID == 0)
                {
                    PNALGOL.Visible = true;
                }
                //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                //if (TID == 0)
                //{
                //    PNALGOL.Visible = true;
                //}
                //else
                //{
                //    DrpTENANT_ID.SelectedValue = TID.ToString();
                //    drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
                //    drplocation.DataTextField = "LOCNAME1";
                //    drplocation.DataValueField = "LOCATIONID";
                //    drplocation.DataBind();
                //    drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));


                //    drplocation.SelectedValue = LID.ToString();
                //    ViewState["TenenatID"] = TID;
                //    ViewState["LocationID"] = LID;
                //}

            }
            if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 00)
            {
                TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.ROLE_MST> List = DB.ROLE_MST.Where(p => p.TenentID == TID).OrderBy(m => m.ROLE_ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        #endregion

        #region PAge Genarator

        public void GetShow()
        {
            //lblERP_TENANT_ID1s.Attributes["class"] =lblERP_TENANT_ID2h.Attributes["class"] =
            lblROLE_NAME1s.Attributes["class"] = lblROLE_NAME11s.Attributes["class"] = lblROLE_NAME21s.Attributes["class"] = lblROLE_DESC1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblACTIVE_FROM_DT1s.Attributes["class"] = lblACTIVE_TO_DT1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblROLE_NAME2h.Attributes["class"] = lblROLE_NAME12h.Attributes["class"] = lblROLE_NAME22h.Attributes["class"] = lblROLE_DESC2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblACTIVE_FROM_DT2h.Attributes["class"] = lblACTIVE_TO_DT2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblROLE_NAME1s.Attributes["class"] = lblROLE_NAME11s.Attributes["class"] = lblROLE_NAME21s.Attributes["class"] = lblROLE_DESC1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblACTIVE_FROM_DT1s.Attributes["class"] = lblACTIVE_TO_DT1s.Attributes["class"] = "control-label col-md-4  gethide";//lblERP_TENANT_ID1s.Attributes["class"] = 
            lblROLE_NAME2h.Attributes["class"] = lblROLE_NAME12h.Attributes["class"] = lblROLE_NAME22h.Attributes["class"] = lblROLE_DESC2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblACTIVE_FROM_DT2h.Attributes["class"] = lblACTIVE_TO_DT2h.Attributes["class"] = "control-label col-md-4  getshow";//lblERP_TENANT_ID2h.Attributes["class"]
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
            //drpROLE_ID.SelectedIndex = 0;
            txtROLE_NAME.Text = "";
            txtROLE_NAME1.Text = "";
            txtROLE_NAME2.Text = "";
            txtROLE_DESC.Text = "";
            cbACTIVE_FLAG.Checked = false;
            txtACTIVE_FROM_DT.Text = "";
            txtACTIVE_TO_DT.Text = "";
            //drpERP_TENANT_ID.SelectedIndex = 0;
            //txtCRUP_ID.Text = "";

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
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.ROLE_MST objROLE_MST = new Database.ROLE_MST();
                        //Server Content Send data Yogesh
                        objROLE_MST.TenentID = TID;
                        objROLE_MST.ROLE_ID = DB.ROLE_MST.Count() > 0 ? Convert.ToInt32(DB.ROLE_MST.Max(p => p.ROLE_ID) + 1) : 1;
                        objROLE_MST.ROLE_NAME = txtROLE_NAME.Text;
                        objROLE_MST.ROLE_NAME1 = txtROLE_NAME1.Text;
                        objROLE_MST.ROLE_NAME2 = txtROLE_NAME2.Text;
                        objROLE_MST.ROLE_DESC = txtROLE_DESC.Text;
                        objROLE_MST.ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                        objROLE_MST.ACTIVE_FROM_DT = Convert.ToDateTime(txtACTIVE_FROM_DT.Text);
                        objROLE_MST.ACTIVE_TO_DT = Convert.ToDateTime(txtACTIVE_TO_DT.Text);
                        objROLE_MST.ACTIVEROLE = true;
                        objROLE_MST.ROLLDATE = Convert.ToDateTime(txtACTIVE_TO_DT.Text);
                        //if (ViewState["TenenatID"] != null)
                        //{
                        //    objROLE_MST.TenentID = Convert.ToInt32(ViewState["TenenatID"]);//Convert.ToInt32(drpERP_TENANT_ID.SelectedValue);
                        //}
                        //if (ViewState["LocationID"]!=null )
                        //{
                        //   // objROLE_MST.LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                        //}

                        DB.ROLE_MST.AddObject(objROLE_MST);
                        DB.SaveChanges();
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        pnlSuccessMsg.Visible = true;
                        navigation.Visible = true;
                        Readonly();

                        //  FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);

                            Database.ROLE_MST objROLE_MST = DB.ROLE_MST.Single(p => p.ROLE_ID == ID && p.TenentID == TID);
                            //objROLE_MST.TenentID = TID;
                            //objROLE_MST.ROLE_ID = Convert.ToInt32(drpROLE_ID.SelectedValue);
                            objROLE_MST.ROLE_NAME = txtROLE_NAME.Text;
                            objROLE_MST.ROLE_NAME1 = txtROLE_NAME1.Text;
                            objROLE_MST.ROLE_NAME2 = txtROLE_NAME2.Text;
                            objROLE_MST.ROLE_DESC = txtROLE_DESC.Text;
                            objROLE_MST.ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            objROLE_MST.ACTIVE_FROM_DT = Convert.ToDateTime(txtACTIVE_FROM_DT.Text);
                            objROLE_MST.ACTIVE_TO_DT = Convert.ToDateTime(txtACTIVE_TO_DT.Text);
                            objROLE_MST.ROLLDATE = Convert.ToDateTime(txtACTIVE_TO_DT.Text);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "ss";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
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
            List<Database.USER_MST> UList = DB.USER_MST.GroupBy(p => p.TenentID).Select(p => p.FirstOrDefault()).ToList();
            DrpTENANT_ID.DataSource = UList;
            DrpTENANT_ID.DataTextField = "TenentID";
            DrpTENANT_ID.DataValueField = "TenentID";
            DrpTENANT_ID.DataBind();
            DrpTENANT_ID.Items.Insert(0, new ListItem("-- Select --", "00"));
            //    var Datas = (from fun1 in DB.TBLLOCATIONs
            //                 select new
            //                 {
            //                     fun1.TenentID,

            //                 }
            //).Distinct();
            //    DrpTENANT_ID.DataSource = Datas;
            //    DrpTENANT_ID.DataTextField = "TenentID";
            //    DrpTENANT_ID.DataValueField = "TenentID";
            //    DrpTENANT_ID.DataBind();
            //    DrpTENANT_ID.Items.Insert(0, new ListItem("---Select---", "0"));
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();
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
        public void ManageData()
        {
            txtROLE_NAME.Text = Listview1.SelectedDataKey[2].ToString();
            txtROLE_NAME1.Text = Listview1.SelectedDataKey[3].ToString();
            txtROLE_NAME2.Text = Listview1.SelectedDataKey[4].ToString();
            txtROLE_DESC.Text = Listview1.SelectedDataKey[5].ToString();
            cbACTIVE_FLAG.Checked = Listview1.SelectedDataKey[6].ToString() == "Y" ? true : false;
            txtACTIVE_FROM_DT.Text = Convert.ToDateTime(Listview1.SelectedDataKey[7]).ToString("MM/dd/yyyy");
            txtACTIVE_TO_DT.Text = Convert.ToDateTime(Listview1.SelectedDataKey[8]).ToString("MM/dd/yyyy");
        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            ManageData();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                ManageData();
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
                ManageData();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            ManageData();
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
                    lblROLE_NAME2h.Visible = lblROLE_NAME12h.Visible = lblROLE_NAME22h.Visible = lblROLE_DESC2h.Visible = lblACTIVE_FLAG2h.Visible = lblACTIVE_FROM_DT2h.Visible = lblACTIVE_TO_DT2h.Visible = false;//lblERP_TENANT_ID2h.Visible =
                    //2true
                    txtROLE_NAME2h.Visible = txtROLE_NAME12h.Visible = txtROLE_NAME22h.Visible = txtROLE_DESC2h.Visible = txtACTIVE_FLAG2h.Visible = txtACTIVE_FROM_DT2h.Visible = txtACTIVE_TO_DT2h.Visible = true;//txtERP_TENANT_ID2h.Visible =

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
                    lblROLE_NAME2h.Visible = lblROLE_NAME12h.Visible = lblROLE_NAME22h.Visible = lblROLE_DESC2h.Visible = lblACTIVE_FLAG2h.Visible = lblACTIVE_FROM_DT2h.Visible = lblACTIVE_TO_DT2h.Visible = true;//lblERP_TENANT_ID2h.Visible 
                    //2false
                    txtROLE_NAME2h.Visible = txtROLE_NAME12h.Visible = txtROLE_NAME22h.Visible = txtROLE_DESC2h.Visible = txtACTIVE_FLAG2h.Visible = txtACTIVE_FROM_DT2h.Visible = txtACTIVE_TO_DT2h.Visible = false;// txtERP_TENANT_ID2h.Visible = ;

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
                    lblROLE_NAME1s.Visible = lblROLE_NAME11s.Visible = lblROLE_NAME21s.Visible = lblROLE_DESC1s.Visible = lblACTIVE_FLAG1s.Visible = lblACTIVE_FROM_DT1s.Visible = lblACTIVE_TO_DT1s.Visible = false;// lblERP_TENANT_ID1s.Visible = false;
                    //1true
                    txtROLE_NAME1s.Visible = txtROLE_NAME11s.Visible = txtROLE_NAME21s.Visible = txtROLE_DESC1s.Visible = txtACTIVE_FLAG1s.Visible = txtACTIVE_FROM_DT1s.Visible = txtACTIVE_TO_DT1s.Visible = true;// txtERP_TENANT_ID1s.Visible = true;
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
                    lblROLE_NAME1s.Visible = lblROLE_NAME11s.Visible = lblROLE_NAME21s.Visible = lblROLE_DESC1s.Visible = lblACTIVE_FLAG1s.Visible = lblACTIVE_FROM_DT1s.Visible = lblACTIVE_TO_DT1s.Visible = true;// lblERP_TENANT_ID1s.Visible = true;
                    //1false
                    txtROLE_NAME1s.Visible = txtROLE_NAME11s.Visible = txtROLE_NAME21s.Visible = txtROLE_DESC1s.Visible = txtACTIVE_FLAG1s.Visible = txtACTIVE_FROM_DT1s.Visible = txtACTIVE_TO_DT1s.Visible = false;// txtERP_TENANT_ID1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_ROLE_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = lblhTENANT_ID.Text = item.LabelName;
                //if (lblROLE_ID1s.ID == item.LabelID)
                //   txtROLE_ID1s.Text = lblROLE_ID1s.Text = lblhROLE_ID.Text = item.LabelName;
                if (lblROLE_NAME1s.ID == item.LabelID)
                    txtROLE_NAME1s.Text = lblROLE_NAME1s.Text = lblhROLE_NAME.Text = item.LabelName;
                else if (lblROLE_NAME11s.ID == item.LabelID)
                    txtROLE_NAME11s.Text = lblROLE_NAME11s.Text = item.LabelName;
                else if (lblROLE_NAME21s.ID == item.LabelID)
                    txtROLE_NAME21s.Text = lblROLE_NAME21s.Text = lblhROLE_NAME2.Text = item.LabelName;
                else if (lblROLE_DESC1s.ID == item.LabelID)
                    txtROLE_DESC1s.Text = lblROLE_DESC1s.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                else if (lblACTIVE_FROM_DT1s.ID == item.LabelID)
                    txtACTIVE_FROM_DT1s.Text = lblACTIVE_FROM_DT1s.Text = lblhACTIVE_FROM_DT.Text = item.LabelName;
                else if (lblACTIVE_TO_DT1s.ID == item.LabelID)
                    txtACTIVE_TO_DT1s.Text = lblACTIVE_TO_DT1s.Text = lblhACTIVE_TO_DT.Text = item.LabelName;
                //else if (lblERP_TENANT_ID1s.ID == item.LabelID)
                //    txtERP_TENANT_ID1s.Text = lblERP_TENANT_ID1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    txtTENANT_ID2h.Text = lblTENANT_ID2h.Text = lblhTENANT_ID.Text = item.LabelName;
                //else if (lblROLE_ID2h.ID == item.LabelID)
                //    txtROLE_ID2h.Text = lblROLE_ID2h.Text = lblhROLE_ID.Text = item.LabelName;
                else if (lblROLE_NAME2h.ID == item.LabelID)
                    txtROLE_NAME2h.Text = lblROLE_NAME2h.Text = lblhROLE_NAME.Text = item.LabelName;
                else if (lblROLE_NAME12h.ID == item.LabelID)
                    txtROLE_NAME12h.Text = lblROLE_NAME12h.Text = item.LabelName;
                else if (lblROLE_NAME22h.ID == item.LabelID)
                    txtROLE_NAME22h.Text = lblROLE_NAME22h.Text = lblhROLE_NAME2.Text = item.LabelName;
                else if (lblROLE_DESC2h.ID == item.LabelID)
                    txtROLE_DESC2h.Text = lblROLE_DESC2h.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                else if (lblACTIVE_FROM_DT2h.ID == item.LabelID)
                    txtACTIVE_FROM_DT2h.Text = lblACTIVE_FROM_DT2h.Text = lblhACTIVE_FROM_DT.Text = item.LabelName;
                else if (lblACTIVE_TO_DT2h.ID == item.LabelID)
                    txtACTIVE_TO_DT2h.Text = lblACTIVE_TO_DT2h.Text = lblhACTIVE_TO_DT.Text = item.LabelName;
                //else if (lblERP_TENANT_ID2h.ID == item.LabelID)
                //    txtERP_TENANT_ID2h.Text = lblERP_TENANT_ID2h.Text = item.LabelName;
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
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_ROLE_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\xml\\ACM_ROLE_MST.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("ACM_ROLE_MST").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                //if (lblROLE_ID1s.ID == item.LabelID)
                //   ds.Tables[0].Rows[i]["LabelName"] = txtROLE_ID1s.Text;
                if (lblROLE_NAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_NAME1s.Text;
                else if (lblROLE_NAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_NAME11s.Text;
                else if (lblROLE_NAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_NAME21s.Text;
                else if (lblROLE_DESC1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_DESC1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                else if (lblACTIVE_FROM_DT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FROM_DT1s.Text;
                else if (lblACTIVE_TO_DT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_TO_DT1s.Text;
                //else if (lblERP_TENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtERP_TENANT_ID1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID2h.Text;
                //else if (lblROLE_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_ID2h.Text;
                else if (lblROLE_NAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_NAME2h.Text;
                else if (lblROLE_NAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_NAME12h.Text;
                else if (lblROLE_NAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_NAME22h.Text;
                else if (lblROLE_DESC2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtROLE_DESC2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                else if (lblACTIVE_FROM_DT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FROM_DT2h.Text;
                else if (lblACTIVE_TO_DT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_TO_DT2h.Text;
                //else if (lblERP_TENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtERP_TENANT_ID2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\xml\\ACM_ROLE_MST.xml"));

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
            //drpROLE_ID.Enabled = true;
            txtROLE_NAME.Enabled = true;
            txtROLE_NAME1.Enabled = true;
            txtROLE_NAME2.Enabled = true;
            txtROLE_DESC.Enabled = true;
            cbACTIVE_FLAG.Checked = true;
            txtACTIVE_FROM_DT.Enabled = true;
            txtACTIVE_TO_DT.Enabled = true;
            // drpERP_TENANT_ID.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //drpROLE_ID.Enabled = false;
            txtROLE_NAME.Enabled = false;
            txtROLE_NAME1.Enabled = false;
            txtROLE_NAME2.Enabled = false;
            txtROLE_DESC.Enabled = false;
            cbACTIVE_FLAG.Checked = false;
            txtACTIVE_FROM_DT.Enabled = false;
            txtACTIVE_TO_DT.Enabled = false;
            //  drpERP_TENANT_ID.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.ROLE_MST.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.ROLE_MST.OrderBy(m => m.ROLE_ID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.ROLE_MST.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.ROLE_MST.OrderBy(m => m.ROLE_ID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.ROLE_MST.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.ROLE_MST.OrderBy(m => m.ROLE_ID).Take(take).Skip(Skip)).ToList());
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
        //    int Totalrec = DB.ROLE_MST.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.ROLE_MST.OrderBy(m => m.ROLE_ID).Take(take).Skip(Skip)).ToList());
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
            //FillContractorID();
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
                        Database.ROLE_MST objSOJobDesc = DB.ROLE_MST.Single(p => p.ROLE_ID == ID && p.TenentID == TID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.ROLE_MST objROLE_MST = DB.ROLE_MST.Single(p => p.ROLE_ID == ID && p.TenentID == TID);
                        //drpROLE_ID.SelectedValue = objROLE_MST.ROLE_ID.ToString();
                        txtROLE_NAME.Text = objROLE_MST.ROLE_NAME.ToString();
                        txtROLE_NAME1.Text = objROLE_MST.ROLE_NAME1.ToString();
                        txtROLE_NAME2.Text = objROLE_MST.ROLE_NAME2.ToString();
                        txtROLE_DESC.Text = objROLE_MST.ROLE_DESC.ToString();
                        cbACTIVE_FLAG.Checked = (objROLE_MST.ACTIVE_FLAG == "Y") ? true : false;
                        txtACTIVE_FROM_DT.Text = Convert.ToDateTime(objROLE_MST.ACTIVE_FROM_DT).ToString("MM/dd/yyyy");
                        txtACTIVE_TO_DT.Text = Convert.ToDateTime(objROLE_MST.ACTIVE_TO_DT).ToString("MM/dd/yyyy");
                        Session["EditTENANTID"] = objROLE_MST.ERP_TENANT_ID.ToString();

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
        //    int Totalrec = DB.ROLE_MST.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.ROLE_MST.OrderBy(m => m.ROLE_ID).Take(Tvalue).Skip(Svalue)).ToList());
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
            ViewState["TenenatID"] = TID;
        }

        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 00)
            {
                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                ViewState["TenenatID"] = TID;
                ViewState["LocationID"] = LID;
                btnGO.Enabled = true;
            }

        }

        protected void btnGO_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(drplocation.SelectedValue) != 0 && Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 00)
            {
                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                BindData();
            }
        }

    }
}