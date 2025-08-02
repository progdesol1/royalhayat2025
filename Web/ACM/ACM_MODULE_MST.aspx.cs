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
    public partial class ACM_MODULE_MST : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                BindData();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                btnAdd.ValidationGroup = "ss";
                //BindData();
                //if (DB.MODULE_MST.Count() > 0)
                //{
                //    FirstData();
                //}

            }
        }
        #region Step2
        public void BindData()
        {
            //int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
            List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p=>p.TenentID == TID).OrderBy(m => m.Module_Id).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();

            List<Database.PRIVILEGE_MST> Listpivilage = DB.PRIVILEGE_MST.Where(p => p.TenentID == TID).ToList();
            ListPrvilage.DataSource = Listpivilage;
            ListPrvilage.DataBind();
        }
        #endregion

        #region PAge Genarator
    
        public void GetShow()
        {

            lblModule_Id1s.Attributes["class"] = lblMYSYSNAME1s.Attributes["class"] = lblModule_Name1s.Attributes["class"] = lblModule_NameO1s.Attributes["class"] = lblModule_NameT1s.Attributes["class"] = lblModule_Desc1s.Attributes["class"] = lblParent_Module_id1s.Attributes["class"] = lblModule_Order1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblModule_Location1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblModule_Id2h.Attributes["class"] = lblMYSYSNAME2h.Attributes["class"] = lblModule_Name2h.Attributes["class"] = lblModule_NameO2h.Attributes["class"] = lblModule_NameT2h.Attributes["class"] = lblModule_Desc2h.Attributes["class"] = lblParent_Module_id2h.Attributes["class"] = lblModule_Order2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblModule_Location2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblModule_Id1s.Attributes["class"] = lblMYSYSNAME1s.Attributes["class"] = lblModule_Name1s.Attributes["class"] = lblModule_NameO1s.Attributes["class"] = lblModule_NameT1s.Attributes["class"] = lblModule_Desc1s.Attributes["class"] = lblParent_Module_id1s.Attributes["class"] = lblModule_Order1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblModule_Location1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblModule_Id2h.Attributes["class"] = lblMYSYSNAME2h.Attributes["class"] = lblModule_Name2h.Attributes["class"] = lblModule_NameO2h.Attributes["class"] = lblModule_NameT2h.Attributes["class"] = lblModule_Desc2h.Attributes["class"] = lblParent_Module_id2h.Attributes["class"] = lblModule_Order2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblModule_Location2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            chIsParents.Checked = false;
            txtMYSYSNAME.Text = "";
            txtModule_Name.Text = "";
            txtModule_NameO.Text = "";
            txtModule_NameT.Text = "";
            txtModule_Desc.Text = "";
            txtTenant_ID.Text = "";
            drpParent_Module_id.SelectedIndex = 0;
            drpModule_Order.SelectedIndex = 0;
            cbACTIVE_FLAG.Checked = false;
            //txtCRUP_ID.Text = "";
            txtModule_Location.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TENANT_ID = 0;
                int LOCATION_ID = 0;
                int Module_Id = 0;
                string MYSYSNAME = "";
                string Module_Name = "";
                string Module_NameO = "";
                string Module_NameT = "";
                string Module_Desc = "";
                int Parent_Module_id = 0;
                int Module_Order = 0;
                string ACTIVE_FLAG = "";
                int CRUP_ID = 0;
                string Module_Location = "";
                //try
                //{
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        btnAdd.ValidationGroup = "s";
                        //Server Content Send data Yogesh

                        //Module_Id = DB.MODULE_MST.Count() > 0 ? Convert.ToInt32(DB.MODULE_MST.Max(p => p.Module_Id) + 1) : 1;//Convert.ToInt32(drpModule_Id.SelectedValue);
                        MYSYSNAME = txtMYSYSNAME.Text;
                        Module_Name = txtModule_Name.Text;
                        Module_NameO = txtModule_NameO.Text;
                        Module_NameT = txtModule_NameT.Text;
                        Module_Desc = txtModule_Desc.Text;

                        //if ( ViewState["TenenatID"] != null)
                        //{
                        //    TENANT_ID = Convert.ToInt32( ViewState["TenenatID"]);
                        //}

                        //if (ViewState["LocationID"] != null)
                        //{

                        //    LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                        //}

                        TENANT_ID = TID;
                        LOCATION_ID = 1;
                        if (Convert.ToInt32(drpParent_Module_id.SelectedValue) != 0)
                        {
                            if (chIsParents.Checked == false)
                            {
                                Parent_Module_id = Convert.ToInt32(drpParent_Module_id.SelectedValue);
                            }                            
                        }
                        if (Convert.ToInt32(drpModule_Order.SelectedValue) != 0)
                        {
                            Module_Order = Convert.ToInt32(drpModule_Order.SelectedValue);
                        }
                        //objMODULE_MST.Parent_Module_id = Convert.ToInt32(drpParent_Module_id.SelectedValue);
                        ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                        //objMODULE_MST.CRUP_ID = txtCRUP_ID.Text;
                        Module_Location = txtModule_Location.Text;

                        int MID = Classes.ACMClass.InsertDataACMMODULEMASTER(TENANT_ID, LOCATION_ID, Module_Id, MYSYSNAME, Module_Name, Module_NameO, Module_NameT, Module_Desc, Parent_Module_id, Module_Order, ACTIVE_FLAG, Module_Location, CRUP_ID);

                        if (chIsParents.Checked == false)
                        {
                            int TenentID = TID;
                            int LOCATION = 1;
                            int PRIVILEGE_ID = 0;
                            string PRIVILEGE_NAME = txtModule_Name.Text + " Privilage";
                            string PRIVILEGE_DESC = txtModule_Name.Text + " Privilage";
                            string FLAG = "Y";
                            int CRUP = 0;
                            int MODULE_ID = MID;
                            string PRIVILEGE_NAME1 = txtModule_Name.Text + " Privilage";
                            string PRIVILEGE_NAME2 = txtModule_Name.Text + " Privilage";

                            Classes.ACMClass.InsertDataACMPRIVILLEGEMASTER(TID, LOCATION, PRIVILEGE_ID, PRIVILEGE_NAME, PRIVILEGE_DESC, FLAG, CRUP, MODULE_ID, PRIVILEGE_NAME1, PRIVILEGE_NAME2);
                        }
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        pnlParent.Visible = true;
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        navigation.Visible = true;
                        Readonly();
                        FillContractorID();
                        //FirstData();
                        
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            Module_Id = Convert.ToInt32(ViewState["Edit"]);

                            //if (Convert.ToInt32(drpModule_Id.SelectedValue)!=0)
                            //{
                            //    objMODULE_MST.Module_Id = Convert.ToInt32(drpModule_Id.SelectedValue);
                            //}
                            MYSYSNAME = txtMYSYSNAME.Text;
                            Module_Name = txtModule_Name.Text;
                            Module_NameO = txtModule_NameO.Text;
                            Module_NameT = txtModule_NameT.Text;
                            Module_Desc = txtModule_Desc.Text;
                            if (Convert.ToInt32(drpParent_Module_id.SelectedValue) != 0)
                            {
                                if (chIsParents.Checked == false)
                                {
                                    Parent_Module_id = Convert.ToInt32(drpParent_Module_id.SelectedValue);
                                }
                            }

                            //if ( ViewState["TenenatID"] != null)
                            //{
                            //    TENANT_ID = Convert.ToInt32( ViewState["TenenatID"]);
                            //}

                            //if (ViewState["LocationID"] != null)
                            //{

                            //    LOCATION_ID = Convert.ToInt32(ViewState["LocationID"]);
                            //}
                            TENANT_ID = TID;
                            LOCATION_ID = 1;
                            ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            //CRUP_ID = txtCRUP_ID.Text;
                            Module_Location = txtModule_Location.Text;
                            Classes.ACMClass.InsertDataACMMODULEMASTER(TENANT_ID, LOCATION_ID, Module_Id, MYSYSNAME, Module_Name, Module_NameO, Module_NameT, Module_Desc, Parent_Module_id, Module_Order, ACTIVE_FLAG, Module_Location, CRUP_ID);
                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";

                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            pnlParent.Visible = true;
                            btnAdd.ValidationGroup = "ss";
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
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
           // drpModule_Id.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpParent_Module_id.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpModule_Order.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpModule_Location.Items.Insert(0, new ListItem("-- Select --", "0"));drpModule_Location.DataSource = DB.0;drpModule_Location.DataTextField = "0";drpModule_Location.DataValueField = "0";drpModule_Location.DataBind();

            drpParent_Module_id.Items.Clear();
            drpParent_Module_id.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TID && p.Parent_Module_id == 0);
            drpParent_Module_id.DataTextField = "Module_Name";
            drpParent_Module_id.DataValueField = "Module_Id";
            drpParent_Module_id.DataBind();
            drpParent_Module_id.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Parent Module Name--", "0"));
        }

        #region PAge Genarator navigtion
        
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
            //if (Listview1.SelectedDataKey[1].ToString()!=null)
            //{
            //    drpModule_Id.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            //}
            txtMYSYSNAME.Text = Listview1.SelectedDataKey[2].ToString();
            txtModule_Name.Text = Listview1.SelectedDataKey[3].ToString();
            txtModule_NameO.Text = Listview1.SelectedDataKey[4].ToString();
            txtModule_NameT.Text = Listview1.SelectedDataKey[5].ToString();
            txtModule_Desc.Text = Listview1.SelectedDataKey[6].ToString();
            
            if (Convert.ToInt32(drpParent_Module_id.SelectedValue) != 0)
            {
                drpParent_Module_id.SelectedValue = Listview1.SelectedDataKey[7] !=null?Listview1.SelectedDataKey[7].ToString():"0";
            }
            drpModule_Order.SelectedValue = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
            //if (Listview1.SelectedDataKey[8].ToString() != null)
            //{
            //    drpModule_Order.SelectedValue = Listview1.SelectedDataKey[8].ToString();
            //}


            if (Listview1.SelectedDataKey[9].ToString() == "Y")
            {
                cbACTIVE_FLAG.Checked = true;
            }
            else
            {
                cbACTIVE_FLAG.Checked = false;
            }

            //txtCRUP_ID.Text = Listview1.SelectedDataKey[10].ToString();
            if (Listview1.SelectedDataKey[11] != null)
            {
                txtModule_Location.Text = Listview1.SelectedDataKey[11].ToString();
            }


        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpModule_Id.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                txtMYSYSNAME.Text = Listview1.SelectedDataKey[2].ToString();
                txtModule_Name.Text = Listview1.SelectedDataKey[3].ToString();
                txtModule_NameO.Text = Listview1.SelectedDataKey[4].ToString();
                txtModule_NameT.Text = Listview1.SelectedDataKey[5].ToString();
                txtModule_Desc.Text = Listview1.SelectedDataKey[6].ToString();
                drpParent_Module_id.SelectedValue = Listview1.SelectedDataKey[7]!=null?Listview1.SelectedDataKey[7].ToString():"";
                if (Convert.ToInt32(drpModule_Order.SelectedValue) != 0)
                {
                    drpModule_Order.SelectedValue = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "0";
                }
                if (Listview1.SelectedDataKey[9].ToString() == "Y")
                {
                    cbACTIVE_FLAG.Checked = true;
                }
                else
                {
                    cbACTIVE_FLAG.Checked = false;
                }
                //  cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[9]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[10].ToString();
                if (Listview1.SelectedDataKey[11] != null)
                {
                    txtModule_Location.Text = Listview1.SelectedDataKey[11].ToString();
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
               // drpModule_Id.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                txtMYSYSNAME.Text = Listview1.SelectedDataKey[2].ToString();
                txtModule_Name.Text = Listview1.SelectedDataKey[3].ToString();
                txtModule_NameO.Text = Listview1.SelectedDataKey[4].ToString();
                txtModule_NameT.Text = Listview1.SelectedDataKey[5].ToString();
                txtModule_Desc.Text = Listview1.SelectedDataKey[6].ToString();
                if (Convert.ToInt32(drpParent_Module_id.SelectedValue) != 0)
                {
                    drpParent_Module_id.SelectedValue = Listview1.SelectedDataKey[7] != null ? Listview1.SelectedDataKey[7].ToString() : "0";
                }
                drpModule_Order.SelectedValue = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
                if (Listview1.SelectedDataKey[9].ToString() == "Y")
                {
                    cbACTIVE_FLAG.Checked = true;
                }
                else
                {
                    cbACTIVE_FLAG.Checked = false;
                }
                // cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[9]);
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[10].ToString();
                if (Listview1.SelectedDataKey[11] != null)
                {
                    txtModule_Location.Text = Listview1.SelectedDataKey[11].ToString();
                }
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpModule_Id.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            txtMYSYSNAME.Text = Listview1.SelectedDataKey[2].ToString();
            txtModule_Name.Text = Listview1.SelectedDataKey[3].ToString();
            txtModule_NameO.Text = Listview1.SelectedDataKey[4].ToString();
            txtModule_NameT.Text = Listview1.SelectedDataKey[5].ToString();
            txtModule_Desc.Text = Listview1.SelectedDataKey[6].ToString();
            drpParent_Module_id.SelectedValue = Listview1.SelectedDataKey[7] != null ? Listview1.SelectedDataKey[7].ToString() : "";
            if (Convert.ToInt32(drpModule_Order.SelectedValue) != 0)
            {
                drpModule_Order.SelectedValue = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
            }
            if (Listview1.SelectedDataKey[9].ToString() == "Y")
            {
                cbACTIVE_FLAG.Checked = true;
            }
            else
            {
                cbACTIVE_FLAG.Checked = false;
            }
            //cbACTIVE_FLAG.Checked = Convert.ToBoolean(Listview1.SelectedDataKey[9]);
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[10].ToString();
            if (Listview1.SelectedDataKey[11] != null)
            {
                txtModule_Location.Text = Listview1.SelectedDataKey[11].ToString();
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

                    //2false
                    lblModule_Id2h.Visible = lblMYSYSNAME2h.Visible = lblModule_Name2h.Visible = lblModule_NameO2h.Visible = lblModule_NameT2h.Visible = lblModule_Desc2h.Visible = lblParent_Module_id2h.Visible = lblModule_Order2h.Visible = lblACTIVE_FLAG2h.Visible = lblModule_Location2h.Visible = false;
                    //2true
                    txtModule_Id2h.Visible = txtMYSYSNAME2h.Visible = txtModule_Name2h.Visible = txtModule_NameO2h.Visible = txtModule_NameT2h.Visible = txtModule_Desc2h.Visible = txtParent_Module_id2h.Visible = txtModule_Order2h.Visible = txtACTIVE_FLAG2h.Visible = txtModule_Location2h.Visible = true;

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
                    lblModule_Id2h.Visible = lblMYSYSNAME2h.Visible = lblModule_Name2h.Visible = lblModule_NameO2h.Visible = lblModule_NameT2h.Visible = lblModule_Desc2h.Visible = lblParent_Module_id2h.Visible = lblModule_Order2h.Visible = lblACTIVE_FLAG2h.Visible = lblModule_Location2h.Visible = true;
                    //2false
                    txtModule_Id2h.Visible = txtMYSYSNAME2h.Visible = txtModule_Name2h.Visible = txtModule_NameO2h.Visible = txtModule_NameT2h.Visible = txtModule_Desc2h.Visible = txtParent_Module_id2h.Visible = txtModule_Order2h.Visible = txtACTIVE_FLAG2h.Visible = txtModule_Location2h.Visible = false;

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
                    lblModule_Id1s.Visible = lblMYSYSNAME1s.Visible = lblModule_Name1s.Visible = lblModule_NameO1s.Visible = lblModule_NameT1s.Visible = lblModule_Desc1s.Visible = lblParent_Module_id1s.Visible = lblModule_Order1s.Visible = lblACTIVE_FLAG1s.Visible = lblModule_Location1s.Visible = false;
                    //1true
                    txtModule_Id1s.Visible = txtMYSYSNAME1s.Visible = txtModule_Name1s.Visible = txtModule_NameO1s.Visible = txtModule_NameT1s.Visible = txtModule_Desc1s.Visible = txtParent_Module_id1s.Visible = txtModule_Order1s.Visible = txtACTIVE_FLAG1s.Visible = txtModule_Location1s.Visible = true;
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
                    lblModule_Id1s.Visible = lblMYSYSNAME1s.Visible = lblModule_Name1s.Visible = lblModule_NameO1s.Visible = lblModule_NameT1s.Visible = lblModule_Desc1s.Visible = lblParent_Module_id1s.Visible = lblModule_Order1s.Visible = lblACTIVE_FLAG1s.Visible = lblModule_Location1s.Visible = true;
                    //1false
                    txtModule_Id1s.Visible = txtMYSYSNAME1s.Visible = txtModule_Name1s.Visible = txtModule_NameO1s.Visible = txtModule_NameT1s.Visible = txtModule_Desc1s.Visible = txtParent_Module_id1s.Visible = txtModule_Order1s.Visible = txtACTIVE_FLAG1s.Visible = txtModule_Location1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_MODULE_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    txtTENANT_ID1s.Text = lblTENANT_ID1s.Text = lblhTENANT_ID.Text = item.LabelName;
                if (lblModule_Id1s.ID == item.LabelID)
                    txtModule_Id1s.Text = lblModule_Id1s.Text = lblhModule_Id.Text = item.LabelName;
                else if (lblMYSYSNAME1s.ID == item.LabelID)
                    txtMYSYSNAME1s.Text = lblMYSYSNAME1s.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lblModule_Name1s.ID == item.LabelID)
                    txtModule_Name1s.Text = lblModule_Name1s.Text = lblhModule_Name.Text = item.LabelName;
                else if (lblModule_NameO1s.ID == item.LabelID)
                    txtModule_NameO1s.Text = lblModule_NameO1s.Text = item.LabelName;
                else if (lblModule_NameT1s.ID == item.LabelID)
                    txtModule_NameT1s.Text = lblModule_NameT1s.Text = item.LabelName;
                else if (lblModule_Desc1s.ID == item.LabelID)
                    txtModule_Desc1s.Text = lblModule_Desc1s.Text = lblhModule_Desc.Text = item.LabelName;
                else if (lblParent_Module_id1s.ID == item.LabelID)
                    txtParent_Module_id1s.Text = lblParent_Module_id1s.Text = item.LabelName;
                else if (lblModule_Order1s.ID == item.LabelID)
                    txtModule_Order1s.Text = lblModule_Order1s.Text = lblhModule_Order.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblModule_Location1s.ID == item.LabelID)
                    txtModule_Location1s.Text = lblModule_Location1s.Text = lblhModule_Location.Text = item.LabelName;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    txtTENANT_ID2h.Text = lblTENANT_ID2h.Text = lblhTENANT_ID.Text = item.LabelName;
                else if (lblModule_Id2h.ID == item.LabelID)
                    txtModule_Id2h.Text = lblModule_Id2h.Text = lblhModule_Id.Text = item.LabelName;
                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    txtMYSYSNAME2h.Text = lblMYSYSNAME2h.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lblModule_Name2h.ID == item.LabelID)
                    txtModule_Name2h.Text = lblModule_Name2h.Text = lblhModule_Name.Text = item.LabelName;
                else if (lblModule_NameO2h.ID == item.LabelID)
                    txtModule_NameO2h.Text = lblModule_NameO2h.Text = item.LabelName;
                else if (lblModule_NameT2h.ID == item.LabelID)
                    txtModule_NameT2h.Text = lblModule_NameT2h.Text = item.LabelName;
                else if (lblModule_Desc2h.ID == item.LabelID)
                    txtModule_Desc2h.Text = lblModule_Desc2h.Text = lblhModule_Desc.Text = item.LabelName;
                else if (lblParent_Module_id2h.ID == item.LabelID)
                    txtParent_Module_id2h.Text = lblParent_Module_id2h.Text = item.LabelName;
                else if (lblModule_Order2h.ID == item.LabelID)
                    txtModule_Order2h.Text = lblModule_Order2h.Text = lblhModule_Order.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;
                else if (lblModule_Location2h.ID == item.LabelID)
                    txtModule_Location2h.Text = lblModule_Location2h.Text = lblhModule_Location.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("ACM_MODULE_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\ACM_MODULE_MST.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("ACM_MODULE_MST").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANT_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID1s.Text;
                if (lblModule_Id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Id1s.Text;
                else if (lblMYSYSNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME1s.Text;
                else if (lblModule_Name1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Name1s.Text;
                else if (lblModule_NameO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_NameO1s.Text;
                else if (lblModule_NameT1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_NameT1s.Text;
                else if (lblModule_Desc1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Desc1s.Text;
                else if (lblParent_Module_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtParent_Module_id1s.Text;
                else if (lblModule_Order1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Order1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                else if (lblModule_Location1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Location1s.Text;

                //if (lblTENANT_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANT_ID2h.Text;
                else if (lblModule_Id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Id2h.Text;
                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME2h.Text;
                else if (lblModule_Name2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Name2h.Text;
                else if (lblModule_NameO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_NameO2h.Text;
                else if (lblModule_NameT2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_NameT2h.Text;
                else if (lblModule_Desc2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Desc2h.Text;
                else if (lblParent_Module_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtParent_Module_id2h.Text;
                else if (lblModule_Order2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Order2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                else if (lblModule_Location2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModule_Location2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\ACM_MODULE_MST.xml"));

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
            chIsParents.Enabled = true;
            txtMYSYSNAME.Enabled = true;
            txtModule_Name.Enabled = true;
            txtModule_NameO.Enabled = true;
            txtModule_NameT.Enabled = true;
            txtModule_Desc.Enabled = true;
            drpParent_Module_id.Enabled = true;
            drpModule_Order.Enabled = true;
            cbACTIVE_FLAG.Checked = true;
            //txtCRUP_ID.Enabled = true;
            txtModule_Location.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            chIsParents.Enabled = false;
            txtMYSYSNAME.Enabled = false;
            txtModule_Name.Enabled = false;
            txtModule_NameO.Enabled = false;
            txtModule_NameT.Enabled = false;
            txtModule_Desc.Enabled = false;
            drpParent_Module_id.Enabled = false;
            drpModule_Order.Enabled = false;
            cbACTIVE_FLAG.Checked = false;
            //txtCRUP_ID.Enabled = false;
            txtModule_Location.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.MODULE_MST.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MST.OrderBy(m => m.Module_Id).Take(take).Skip(Skip)).ToList());
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

        //    ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.MODULE_MST.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MST.OrderBy(m => m.Module_Id).Take(take).Skip(Skip)).ToList());
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
        //        ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.MODULE_MST.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MST.OrderBy(m => m.Module_Id).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
        //    int Totalrec = DB.MODULE_MST.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MST.OrderBy(m => m.Module_Id).Take(take).Skip(Skip)).ToList());
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
           // FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                    if (e.CommandName == "btnDelete")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.MODULE_MST objSOJobDesc = DB.MODULE_MST.Single(p => p.Module_Id == ID && p.TenentID == TID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.MODULE_MST objMODULE_MST = DB.MODULE_MST.Single(p => p.Module_Id == ID && p.TenentID == TID);
                        // drpModule_Id.SelectedValue = objMODULE_MST.Module_Id.ToString();
                        txtMYSYSNAME.Text = objMODULE_MST.MYSYSNAME.ToString();
                        txtModule_Name.Text = objMODULE_MST.Module_Name.ToString();
                        txtModule_NameO.Text = objMODULE_MST.Module_NameO.ToString();
                        txtModule_NameT.Text = objMODULE_MST.Module_NameT.ToString();
                        txtModule_Desc.Text = objMODULE_MST.Module_Desc.ToString();
                        if (objMODULE_MST.Parent_Module_id == 0)
                        {
                            pnlParent.Visible = false;                            
                        }
                        else
                        {
                            pnlParent.Visible = true;
                            drpParent_Module_id.SelectedValue = objMODULE_MST.Parent_Module_id.ToString();
                        }
                        //if (objMODULE_MST.Module_Order.ToString() != "0" && objMODULE_MST.Module_Order.ToString() != null)
                        //{
                        //    drpModule_Order.SelectedValue = objMODULE_MST.Module_Order.ToString();
                        //}
                        if (objMODULE_MST.ACTIVE_FLAG == "Y")
                        {
                            cbACTIVE_FLAG.Checked = true;
                        }
                        else
                        {
                            cbACTIVE_FLAG.Checked = false;
                        }
                        // cbACTIVE_FLAG.Checked = Convert.ToBoolean(objMODULE_MST.ACTIVE_FLAG);
                        //txtCRUP_ID.Text = objMODULE_MST.CRUP_ID.ToString();
                        if (objMODULE_MST.Module_Location != "" && objMODULE_MST.Module_Location != null)
                        {
                            txtModule_Location.Text = objMODULE_MST.Module_Location.ToString();
                        }

                        txtTenant_ID.Text = objMODULE_MST.TenentID.ToString();
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        Write();
                    }
                    if (e.CommandName == "btncopy")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.MODULE_MST objMODULE_MST = DB.MODULE_MST.Single(p => p.Module_Id == ID && p.TenentID == TID);


                        int TENANT_ID = 0;
                        int LOCATION_ID = 0;
                        int Module_Id = 0;
                        string MYSYSNAME = "";
                        string Module_Name = "";
                        string Module_NameO = "";
                        string Module_NameT = "";
                        string Module_Desc = "";
                        int Parent_Module_id = 0;
                        int Module_Order = 0;
                        string ACTIVE_FLAG = "";
                        int CRUP_ID = 0;
                        string Module_Location = "";
                        Module_Id = 0;//DB.FUNCTION_MST.Count() > 0 ? Convert.ToInt32(DB.FUNCTION_MST.Max(p => p.MENU_ID) + 1) : 1;//Convert.ToInt32(drpModule_Id.SelectedValue);
                        MYSYSNAME = objMODULE_MST.MYSYSNAME;
                        Module_Name = objMODULE_MST.Module_Name;
                        Module_NameO = objMODULE_MST.Module_NameO;
                        Module_NameT = objMODULE_MST.Module_NameT;
                        Module_Desc = objMODULE_MST.Module_Desc;
                        TENANT_ID = objMODULE_MST.TenentID;
                        LOCATION_ID = 0;
                        Parent_Module_id =Convert.ToInt32( objMODULE_MST.Parent_Module_id);
                        ACTIVE_FLAG = objMODULE_MST.ACTIVE_FLAG;
                        Module_Location = objMODULE_MST.Module_Location;
                        Classes.ACMClass.InsertDataACMMODULEMASTER(TENANT_ID, LOCATION_ID, Module_Id, MYSYSNAME, Module_Name, Module_NameO, Module_NameT, Module_Desc, Parent_Module_id, Module_Order, ACTIVE_FLAG, Module_Location, CRUP_ID);                      
                        lblMsg.Text = "  Data Copy Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        navigation.Visible = true;
                        Readonly();                       
                        btnAdd.ValidationGroup = "s1";

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
        //    int Totalrec = DB.MODULE_MST.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.MODULE_MST.OrderBy(m => m.Module_Id).Take(Tvalue).Skip(Svalue)).ToList());
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

        protected void chIsParents_CheckedChanged(object sender, EventArgs e)
        {
            if (chIsParents.Checked == true)
            {
                pnlParent.Visible = false;
            }
            else
            {
                pnlParent.Visible = true;
            }
            
        }

        //protected void DrpTENANT_ID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);

        //    drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
        //    drplocation.DataTextField = "LOCNAME1";
        //    drplocation.DataValueField = "LOCATIONID";
        //    drplocation.DataBind();
        //    drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));
        //     ViewState["TenenatID"] = TID;
        //}

     
        //protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (Convert.ToInt32(drplocation.SelectedValue) != 0 && Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
        //    {


        //        if ((DrpTENANT_ID.SelectedItem.Text) != "--Select--" && Convert.ToInt32(drplocation.SelectedValue) != 0)
        //        {
        //            int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
        //            int LID = Convert.ToInt32(drplocation.SelectedValue);
        //            ViewState["TenenatID"] = TID;
        //            ViewState["LocationID"] = LID;
        //            btnGO.Enabled = true;
        //            //drpModule_Id.Items.Clear();
        //            //drpModule_Id.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TENANT_ID == TID );
        //            //drpModule_Id.DataTextField = "Module_Name";
        //            //drpModule_Id.DataValueField = "Module_Id";
        //            //drpModule_Id.DataBind();
        //            //drpModule_Id.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Module Name--", "0"));
        //            drpParent_Module_id.Items.Clear();
        //            drpParent_Module_id.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TID && p.Parent_Module_id == 0);
        //            drpParent_Module_id.DataTextField = "Module_Name";
        //            drpParent_Module_id.DataValueField = "Module_Id";
        //            drpParent_Module_id.DataBind();
        //            drpParent_Module_id.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Parent Module Name--", "0"));
        //        }

        //    }

        //}

        //protected void btnGO_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
        //    List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p=>p.TenentID == TID).OrderBy(m => m.Module_Id).ToList();
        //    Listview1.DataSource = List;
        //    Listview1.DataBind();
        //}


    }
}