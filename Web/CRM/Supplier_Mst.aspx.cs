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

namespace Web.CRM
{
    public partial class Supplier_Mst : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
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
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
        #region Step2
        public void BindData()
        {
            List<ERP_SUPPLIER_MST> List = DB.ERP_SUPPLIER_MST.Where(p=>p.ACTIVE_FLAG=="Y" && p.TENANT_ID == TID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator
        public void GetShow()
        {

            lblLOCATIONID1s.Attributes["class"] = lblSALUTATION_ID1s.Attributes["class"] = lblSUPPLIER_NAME1s.Attributes["class"] = lblSUPPLIER_NAMEO1s.Attributes["class"] = lblIS_COMPANY1s.Attributes["class"] = lblCOMPANY_ID1s.Attributes["class"] = lblRATING1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblIMG_URL1s.Attributes["class"] = lblJOB_POSITION1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblDESCRIPTION1s.Attributes["class"] = lblADDRESS_ID1s.Attributes["class"] = "control-label col-md-2  getshow";
            lblLOCATIONID2h.Attributes["class"] = lblSALUTATION_ID2h.Attributes["class"] = lblSUPPLIER_NAME2h.Attributes["class"] = lblSUPPLIER_NAMEO2h.Attributes["class"] = lblIS_COMPANY2h.Attributes["class"] = lblCOMPANY_ID2h.Attributes["class"] = lblRATING2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblIMG_URL2h.Attributes["class"] = lblJOB_POSITION2h.Attributes["class"] = "control-label col-md-4  gethide";
            lblDESCRIPTION2h.Attributes["class"] = lblADDRESS_ID2h.Attributes["class"] = "control-label col-md-2  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblLOCATIONID1s.Attributes["class"] = lblSALUTATION_ID1s.Attributes["class"] = lblSUPPLIER_NAME1s.Attributes["class"] = lblSUPPLIER_NAMEO1s.Attributes["class"] = lblIS_COMPANY1s.Attributes["class"] = lblCOMPANY_ID1s.Attributes["class"] = lblRATING1s.Attributes["class"] = lblACTIVE_FLAG1s.Attributes["class"] = lblIMG_URL1s.Attributes["class"] = lblJOB_POSITION1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblDESCRIPTION1s.Attributes["class"] = lblADDRESS_ID1s.Attributes["class"] = "control-label col-md-2  gethide";
            lblLOCATIONID2h.Attributes["class"] = lblSALUTATION_ID2h.Attributes["class"] = lblSUPPLIER_NAME2h.Attributes["class"] = lblSUPPLIER_NAMEO2h.Attributes["class"] = lblIS_COMPANY2h.Attributes["class"] = lblCOMPANY_ID2h.Attributes["class"] = lblRATING2h.Attributes["class"] = lblACTIVE_FLAG2h.Attributes["class"] = lblIMG_URL2h.Attributes["class"] = lblJOB_POSITION2h.Attributes["class"] = "control-label col-md-4  getshow";
            lblDESCRIPTION2h.Attributes["class"] = lblADDRESS_ID2h.Attributes["class"] = "control-label col-md-2  getshow";
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
            //  txtTENANT_ID.Text = "";
            drpLOCATIONID.SelectedIndex = 0;
            drpSALUTATION_ID.SelectedIndex = 0;
            txtSUPPLIER_NAME.Text = "";
            txtSUPPLIER_NAMEO.Text = "";
            txtDESCRIPTION.Text = "";
            cbIS_COMPANY.Checked = false;
            drpCOMPANY_ID.SelectedIndex = 0;
            txtRATING.Text = "";
            cbACTIVE_FLAG.Checked = false;
            txtIMG_URL.Text = "";
            drpJOB_POSITION.SelectedIndex = 0;
            txtADDRESS_ID.Text = "";
            //  txtCRUP_ID.Text = "";

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
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.ERP_SUPPLIER_MST objERP_SUPPLIER_MST = new Database.ERP_SUPPLIER_MST();
                        //Server Content Send data Yogesh
                        objERP_SUPPLIER_MST.TENANT_ID = TID;
                        objERP_SUPPLIER_MST.LOCATIONID = Convert.ToInt32(drpLOCATIONID.SelectedValue);
                        objERP_SUPPLIER_MST.SALUTATION_ID = Convert.ToInt32(drpSALUTATION_ID.SelectedValue);
                        objERP_SUPPLIER_MST.SUPPLIER_NAME = txtSUPPLIER_NAME.Text;
                        objERP_SUPPLIER_MST.SUPPLIER_NAMEO = txtSUPPLIER_NAMEO.Text;
                        objERP_SUPPLIER_MST.DESCRIPTION = txtDESCRIPTION.Text;
                        objERP_SUPPLIER_MST.IS_COMPANY = cbIS_COMPANY.Checked ? "Y" : "N";
                        objERP_SUPPLIER_MST.COMPANY_ID = Convert.ToInt32(drpCOMPANY_ID.SelectedValue);
                        objERP_SUPPLIER_MST.RATING = txtRATING.Text;
                        objERP_SUPPLIER_MST.ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                        objERP_SUPPLIER_MST.IMG_URL = txtIMG_URL.Text;
                        objERP_SUPPLIER_MST.JOB_POSITION = drpJOB_POSITION.SelectedValue;
                        objERP_SUPPLIER_MST.ADDRESS_ID = txtADDRESS_ID.Text;
                        //  objERP_SUPPLIER_MST.CRUP_ID = txtCRUP_ID.Text;


                        DB.ERP_SUPPLIER_MST.AddObject(objERP_SUPPLIER_MST);
                        DB.SaveChanges();
                     //   Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                      //  BindData();
                        //  navigation.Visible = true;
                        Readonly();
                        //  FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.ERP_SUPPLIER_MST objERP_SUPPLIER_MST = DB.ERP_SUPPLIER_MST.Single(p => p.SUP_ID == ID && p.TENANT_ID == TID);
                            objERP_SUPPLIER_MST.TENANT_ID = TID;
                            objERP_SUPPLIER_MST.LOCATIONID = Convert.ToInt32(drpLOCATIONID.SelectedValue);
                            objERP_SUPPLIER_MST.SALUTATION_ID = Convert.ToInt32(drpSALUTATION_ID.SelectedValue);
                            objERP_SUPPLIER_MST.SUPPLIER_NAME = txtSUPPLIER_NAME.Text;
                            objERP_SUPPLIER_MST.SUPPLIER_NAMEO = txtSUPPLIER_NAMEO.Text;
                            objERP_SUPPLIER_MST.DESCRIPTION = txtDESCRIPTION.Text;
                            objERP_SUPPLIER_MST.IS_COMPANY = cbIS_COMPANY.Checked ? "Y" : "N";
                            objERP_SUPPLIER_MST.COMPANY_ID = Convert.ToInt32(drpCOMPANY_ID.SelectedValue);
                            objERP_SUPPLIER_MST.RATING = txtRATING.Text;
                            objERP_SUPPLIER_MST.ACTIVE_FLAG = cbACTIVE_FLAG.Checked ? "Y" : "N";
                            objERP_SUPPLIER_MST.IMG_URL = txtIMG_URL.Text;
                            objERP_SUPPLIER_MST.JOB_POSITION = drpJOB_POSITION.SelectedValue;
                            objERP_SUPPLIER_MST.ADDRESS_ID = txtADDRESS_ID.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();
                          //  Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                           // BindData();
                            //  navigation.Visible = true;
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
            Response.Redirect(Session["ADMInPrevious"].ToString());
        }
        public void FillContractorID()
        {
            //  Classes.EcommAdminClass.getdropdown(drpLOCATIONID, TID, "", "", "", "Eco_TBLLOCATION");
            // Classes.EcommAdminClass.getdropdown(drpCOMPANY_ID, TID, "HLY", "", "", "Eco_TBLCOMPANYSETUP");

            Classes.EcommAdminClass.getdropdown(drpLOCATIONID, TID, "", "", "", "TBLLOCATION");
            //select * from TBLLOCATION where Active='Y'

            //drpLOCATIONID.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
            //drpLOCATIONID.DataTextField = "LOCNAME1";
            //drpLOCATIONID.DataValueField = "LOCATIONID";
            //drpLOCATIONID.DataBind();
            //drpLOCATIONID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Location--", "0"));

            drpSALUTATION_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Salution--", "0"));

            Classes.EcommAdminClass.getdropdown(drpCOMPANY_ID, TID, "HLY", "", "", "TBLCOMPANYSETUP");
            //select * from TBLCOMPANYSETUP where TenentID = 1 and PHYSICALLOCID = 'HLY'

            //drpCOMPANY_ID.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);
            //drpCOMPANY_ID.DataTextField = "COMPNAME";
            //drpCOMPANY_ID.DataValueField = "COMPID";
            //drpCOMPANY_ID.DataBind();
            //drpCOMPANY_ID.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Company--", "0"));

            Classes.EcommAdminClass.getdropdown(drpJOB_POSITION, TID, "HLY", "", "", "tblCONTACTBus");
            //select * from tblCONTACTBus 

            //drpJOB_POSITION.DataSource = DB.tblCONTACTBus.Where(P => P.TenentID == TID && P.Active == "Y");
            //drpJOB_POSITION.DataTextField = "JobTitle";
            //drpJOB_POSITION.DataValueField = "ContactMyID";
            //drpJOB_POSITION.DataBind();
            //drpJOB_POSITION.Items.Insert(0, new ListItem("-- Select --", "0"));


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
            //  txtTENANT_ID.Text = Listview1.SelectedDataKey[0].ToString();
            drpLOCATIONID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpSALUTATION_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtSUPPLIER_NAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtSUPPLIER_NAMEO.Text = Listview1.SelectedDataKey[0].ToString();
            txtDESCRIPTION.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtIS_COMPANY.Text = Listview1.SelectedDataKey[0].ToString();
            drpCOMPANY_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtRATING.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtACTIVE_FLAG.Text = Listview1.SelectedDataKey[0].ToString();
            txtIMG_URL.Text = Listview1.SelectedDataKey[0].ToString();
            //    txtJOB_POSITION.Text = Listview1.SelectedDataKey[0].ToString();
            txtADDRESS_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //  txtTENANT_ID.Text = Listview1.SelectedDataKey[0].ToString();
                drpLOCATIONID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpSALUTATION_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtSUPPLIER_NAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtSUPPLIER_NAMEO.Text = Listview1.SelectedDataKey[0].ToString();
                txtDESCRIPTION.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtIS_COMPANY.Text = Listview1.SelectedDataKey[0].ToString();
                drpCOMPANY_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtRATING.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtACTIVE_FLAG.Text = Listview1.SelectedDataKey[0].ToString();
                txtIMG_URL.Text = Listview1.SelectedDataKey[0].ToString();
                //    txtJOB_POSITION.Text = Listview1.SelectedDataKey[0].ToString();
                txtADDRESS_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
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
                //  txtTENANT_ID.Text = Listview1.SelectedDataKey[0].ToString();
                drpLOCATIONID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpSALUTATION_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtSUPPLIER_NAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtSUPPLIER_NAMEO.Text = Listview1.SelectedDataKey[0].ToString();
                txtDESCRIPTION.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtIS_COMPANY.Text = Listview1.SelectedDataKey[0].ToString();
                drpCOMPANY_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtRATING.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtACTIVE_FLAG.Text = Listview1.SelectedDataKey[0].ToString();
                txtIMG_URL.Text = Listview1.SelectedDataKey[0].ToString();
                //    txtJOB_POSITION.Text = Listview1.SelectedDataKey[0].ToString();
                txtADDRESS_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //  txtTENANT_ID.Text = Listview1.SelectedDataKey[0].ToString();
            drpLOCATIONID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpSALUTATION_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtSUPPLIER_NAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtSUPPLIER_NAMEO.Text = Listview1.SelectedDataKey[0].ToString();
            txtDESCRIPTION.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtIS_COMPANY.Text = Listview1.SelectedDataKey[0].ToString();
            drpCOMPANY_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtRATING.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtACTIVE_FLAG.Text = Listview1.SelectedDataKey[0].ToString();
            txtIMG_URL.Text = Listview1.SelectedDataKey[0].ToString();
            //    txtJOB_POSITION.Text = Listview1.SelectedDataKey[0].ToString();
            txtADDRESS_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
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
                    lblLOCATIONID2h.Visible = lblSALUTATION_ID2h.Visible = lblSUPPLIER_NAME2h.Visible = lblSUPPLIER_NAMEO2h.Visible = lblDESCRIPTION2h.Visible = lblIS_COMPANY2h.Visible = lblCOMPANY_ID2h.Visible = lblRATING2h.Visible = lblACTIVE_FLAG2h.Visible = lblIMG_URL2h.Visible = lblJOB_POSITION2h.Visible = lblADDRESS_ID2h.Visible = false;
                    //2true
                    txtLOCATIONID2h.Visible = txtSALUTATION_ID2h.Visible = txtSUPPLIER_NAME2h.Visible = txtSUPPLIER_NAMEO2h.Visible = txtDESCRIPTION2h.Visible = txtIS_COMPANY2h.Visible = txtCOMPANY_ID2h.Visible = txtRATING2h.Visible = txtACTIVE_FLAG2h.Visible = txtIMG_URL2h.Visible = txtJOB_POSITION2h.Visible = txtADDRESS_ID2h.Visible = true;

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
                    lblLOCATIONID2h.Visible = lblSALUTATION_ID2h.Visible = lblSUPPLIER_NAME2h.Visible = lblSUPPLIER_NAMEO2h.Visible = lblDESCRIPTION2h.Visible = lblIS_COMPANY2h.Visible = lblCOMPANY_ID2h.Visible = lblRATING2h.Visible = lblACTIVE_FLAG2h.Visible = lblIMG_URL2h.Visible = lblJOB_POSITION2h.Visible = lblADDRESS_ID2h.Visible = true;
                    //2false
                    txtLOCATIONID2h.Visible = txtSALUTATION_ID2h.Visible = txtSUPPLIER_NAME2h.Visible = txtSUPPLIER_NAMEO2h.Visible = txtDESCRIPTION2h.Visible = txtIS_COMPANY2h.Visible = txtCOMPANY_ID2h.Visible = txtRATING2h.Visible = txtACTIVE_FLAG2h.Visible = txtIMG_URL2h.Visible = txtJOB_POSITION2h.Visible = txtADDRESS_ID2h.Visible = false;

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
                    lblLOCATIONID1s.Visible = lblSALUTATION_ID1s.Visible = lblSUPPLIER_NAME1s.Visible = lblSUPPLIER_NAMEO1s.Visible = lblDESCRIPTION1s.Visible = lblIS_COMPANY1s.Visible = lblCOMPANY_ID1s.Visible = lblRATING1s.Visible = lblACTIVE_FLAG1s.Visible = lblIMG_URL1s.Visible = lblJOB_POSITION1s.Visible = lblADDRESS_ID1s.Visible = false;
                    //1true
                    txtLOCATIONID1s.Visible = txtSALUTATION_ID1s.Visible = txtSUPPLIER_NAME1s.Visible = txtSUPPLIER_NAMEO1s.Visible = txtDESCRIPTION1s.Visible = txtIS_COMPANY1s.Visible = txtCOMPANY_ID1s.Visible = txtRATING1s.Visible = txtACTIVE_FLAG1s.Visible = txtIMG_URL1s.Visible = txtJOB_POSITION1s.Visible = txtADDRESS_ID1s.Visible = true;
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
                    lblLOCATIONID1s.Visible = lblSALUTATION_ID1s.Visible = lblSUPPLIER_NAME1s.Visible = lblSUPPLIER_NAMEO1s.Visible = lblDESCRIPTION1s.Visible = lblIS_COMPANY1s.Visible = lblCOMPANY_ID1s.Visible = lblRATING1s.Visible = lblACTIVE_FLAG1s.Visible = lblIMG_URL1s.Visible = lblJOB_POSITION1s.Visible = lblADDRESS_ID1s.Visible = true;
                    //1false
                    txtLOCATIONID1s.Visible = txtSALUTATION_ID1s.Visible = txtSUPPLIER_NAME1s.Visible = txtSUPPLIER_NAMEO1s.Visible = txtDESCRIPTION1s.Visible = txtIS_COMPANY1s.Visible = txtCOMPANY_ID1s.Visible = txtRATING1s.Visible = txtACTIVE_FLAG1s.Visible = txtIMG_URL1s.Visible = txtJOB_POSITION1s.Visible = txtADDRESS_ID1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((CRMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("ERP_SUPPLIER_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblLOCATIONID1s.ID == item.LabelID)
                    txtLOCATIONID1s.Text = lblLOCATIONID1s.Text = lblhLOCATIONID.Text = item.LabelName;
                else if (lblSALUTATION_ID1s.ID == item.LabelID)
                    txtSALUTATION_ID1s.Text = lblSALUTATION_ID1s.Text = item.LabelName;
                else if (lblSUPPLIER_NAME1s.ID == item.LabelID)
                    txtSUPPLIER_NAME1s.Text = lblSUPPLIER_NAME1s.Text = lblhSUPPLIER_NAME.Text = item.LabelName;
                else if (lblSUPPLIER_NAMEO1s.ID == item.LabelID)
                    txtSUPPLIER_NAMEO1s.Text = lblSUPPLIER_NAMEO1s.Text = item.LabelName;
                else if (lblDESCRIPTION1s.ID == item.LabelID)
                    txtDESCRIPTION1s.Text = lblDESCRIPTION1s.Text = item.LabelName;
                else if (lblIS_COMPANY1s.ID == item.LabelID)
                    txtIS_COMPANY1s.Text = lblIS_COMPANY1s.Text = item.LabelName;
                else if (lblCOMPANY_ID1s.ID == item.LabelID)
                    txtCOMPANY_ID1s.Text = lblCOMPANY_ID1s.Text = lblhCOMPANY_ID.Text = item.LabelName;
                else if (lblRATING1s.ID == item.LabelID)
                    txtRATING1s.Text = lblRATING1s.Text = item.LabelName;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    txtACTIVE_FLAG1s.Text = lblACTIVE_FLAG1s.Text = item.LabelName;
                else if (lblIMG_URL1s.ID == item.LabelID)
                    txtIMG_URL1s.Text = lblIMG_URL1s.Text = item.LabelName;
                else if (lblJOB_POSITION1s.ID == item.LabelID)
                    txtJOB_POSITION1s.Text = lblJOB_POSITION1s.Text = lblhJOB_POSITION.Text = item.LabelName;
                else if (lblADDRESS_ID1s.ID == item.LabelID)
                    txtADDRESS_ID1s.Text = lblADDRESS_ID1s.Text = item.LabelName;

                else if (lblLOCATIONID2h.ID == item.LabelID)
                    txtLOCATIONID2h.Text = lblLOCATIONID2h.Text = lblhLOCATIONID.Text = item.LabelName;
                else if (lblSALUTATION_ID2h.ID == item.LabelID)
                    txtSALUTATION_ID2h.Text = lblSALUTATION_ID2h.Text = item.LabelName;
                else if (lblSUPPLIER_NAME2h.ID == item.LabelID)
                    txtSUPPLIER_NAME2h.Text = lblSUPPLIER_NAME2h.Text = lblhSUPPLIER_NAME.Text = item.LabelName;
                else if (lblSUPPLIER_NAMEO2h.ID == item.LabelID)
                    txtSUPPLIER_NAMEO2h.Text = lblSUPPLIER_NAMEO2h.Text = item.LabelName;
                else if (lblDESCRIPTION2h.ID == item.LabelID)
                    txtDESCRIPTION2h.Text = lblDESCRIPTION2h.Text = item.LabelName;
                else if (lblIS_COMPANY2h.ID == item.LabelID)
                    txtIS_COMPANY2h.Text = lblIS_COMPANY2h.Text = item.LabelName;
                else if (lblCOMPANY_ID2h.ID == item.LabelID)
                    txtCOMPANY_ID2h.Text = lblCOMPANY_ID2h.Text = lblhCOMPANY_ID.Text = item.LabelName;
                else if (lblRATING2h.ID == item.LabelID)
                    txtRATING2h.Text = lblRATING2h.Text = item.LabelName;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    txtACTIVE_FLAG2h.Text = lblACTIVE_FLAG2h.Text = item.LabelName;
                else if (lblIMG_URL2h.ID == item.LabelID)
                    txtIMG_URL2h.Text = lblIMG_URL2h.Text = item.LabelName;
                else if (lblJOB_POSITION2h.ID == item.LabelID)
                    txtJOB_POSITION2h.Text = lblJOB_POSITION2h.Text = lblhJOB_POSITION.Text = item.LabelName;
                else if (lblADDRESS_ID2h.ID == item.LabelID)
                    txtADDRESS_ID2h.Text = lblADDRESS_ID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("ERP_SUPPLIER_MST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Admin\\xml\\ERP_SUPPLIER_MST.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((CRMMaster)this.Master).Bindxml("ERP_SUPPLIER_MST").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblLOCATIONID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCATIONID1s.Text;
                else if (lblSALUTATION_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSALUTATION_ID1s.Text;
                else if (lblSUPPLIER_NAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUPPLIER_NAME1s.Text;
                else if (lblSUPPLIER_NAMEO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUPPLIER_NAMEO1s.Text;
                else if (lblDESCRIPTION1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDESCRIPTION1s.Text;
                else if (lblIS_COMPANY1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIS_COMPANY1s.Text;
                else if (lblCOMPANY_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOMPANY_ID1s.Text;
                else if (lblRATING1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRATING1s.Text;
                else if (lblACTIVE_FLAG1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG1s.Text;
                else if (lblIMG_URL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIMG_URL1s.Text;
                else if (lblJOB_POSITION1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtJOB_POSITION1s.Text;
                else if (lblADDRESS_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDRESS_ID1s.Text;

                else if (lblLOCATIONID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLOCATIONID2h.Text;
                else if (lblSALUTATION_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSALUTATION_ID2h.Text;
                else if (lblSUPPLIER_NAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUPPLIER_NAME2h.Text;
                else if (lblSUPPLIER_NAMEO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSUPPLIER_NAMEO2h.Text;
                else if (lblDESCRIPTION2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDESCRIPTION2h.Text;
                else if (lblIS_COMPANY2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIS_COMPANY2h.Text;
                else if (lblCOMPANY_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOMPANY_ID2h.Text;
                else if (lblRATING2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRATING2h.Text;
                else if (lblACTIVE_FLAG2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE_FLAG2h.Text;
                else if (lblIMG_URL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIMG_URL2h.Text;
                else if (lblJOB_POSITION2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtJOB_POSITION2h.Text;
                else if (lblADDRESS_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtADDRESS_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\xml\\ERP_SUPPLIER_MST.xml"));

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
            // navigation.Visible = false;
            //  txtTENANT_ID.Enabled = true;
            drpLOCATIONID.Enabled = true;
            drpSALUTATION_ID.Enabled = true;
            txtSUPPLIER_NAME.Enabled = true;
            txtSUPPLIER_NAMEO.Enabled = true;
            txtDESCRIPTION.Enabled = true;
            cbIS_COMPANY.Enabled = true;
            drpCOMPANY_ID.Enabled = true;
            txtRATING.Enabled = true;
            cbACTIVE_FLAG.Enabled = true;
            txtIMG_URL.Enabled = true;
            drpJOB_POSITION.Enabled = true;
            txtADDRESS_ID.Enabled = true;
            // txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            // navigation.Visible = true;
            // txtTENANT_ID.Enabled = false;
            drpLOCATIONID.Enabled = false;
            drpSALUTATION_ID.Enabled = false;
            txtSUPPLIER_NAME.Enabled = false;
            txtSUPPLIER_NAMEO.Enabled = false;
            txtDESCRIPTION.Enabled = false;
            cbIS_COMPANY.Enabled = false;
            drpCOMPANY_ID.Enabled = false;
            txtRATING.Enabled = false;
            cbACTIVE_FLAG.Enabled = false;
            txtIMG_URL.Enabled = false;
            drpJOB_POSITION.Enabled = false;
            txtADDRESS_ID.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.ERP_SUPPLIER_MST.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((CRMMaster)Page.Master).BindList(Listview1, (DB.ERP_SUPPLIER_MST.OrderBy(m => m.SUP_ID).Take(take).Skip(Skip)).ToList());
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

            ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.ERP_SUPPLIER_MST.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((CRMMaster)Page.Master).BindList(Listview1, (DB.ERP_SUPPLIER_MST.OrderBy(m => m.SUP_ID).Take(take).Skip(Skip)).ToList());
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
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.ERP_SUPPLIER_MST.Count();
                take = Showdata;
                Skip = 0;
                ((CRMMaster)Page.Master).BindList(Listview1, (DB.ERP_SUPPLIER_MST.OrderBy(m => m.SUP_ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB.ERP_SUPPLIER_MST.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((CRMMaster)Page.Master).BindList(Listview1, (DB.ERP_SUPPLIER_MST.OrderBy(m => m.SUP_ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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

                        // string[] ID = e.CommandArgument.ToString().Split(',');
                        int str1 = Convert.ToInt32(e.CommandArgument);
                        // string str2 = ID[1].ToString();

                        Database.ERP_SUPPLIER_MST objSOJobDesc = DB.ERP_SUPPLIER_MST.Single(p => p.SUP_ID == str1 && p.TENANT_ID==TID);
                        objSOJobDesc.ACTIVE_FLAG = "N";
                        DB.SaveChanges();
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((CRMMaster)Page.Master).BindList(Listview1, (DB.ERP_SUPPLIER_MST.OrderBy(m => m.SUP_ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int str1 = Convert.ToInt32(e.CommandArgument);

                        Database.ERP_SUPPLIER_MST objERP_SUPPLIER_MST = DB.ERP_SUPPLIER_MST.Single(p => p.SUP_ID == str1 && p.TENANT_ID == TID);
                        //  txtTENANT_ID.Text = objERP_SUPPLIER_MST.TENANT_ID.ToString();
                        drpLOCATIONID.SelectedValue = objERP_SUPPLIER_MST.LOCATIONID.ToString();
                        drpSALUTATION_ID.SelectedValue = objERP_SUPPLIER_MST.SALUTATION_ID.ToString();
                        txtSUPPLIER_NAME.Text = objERP_SUPPLIER_MST.SUPPLIER_NAME.ToString();
                        txtSUPPLIER_NAMEO.Text = objERP_SUPPLIER_MST.SUPPLIER_NAMEO.ToString();
                        txtDESCRIPTION.Text = objERP_SUPPLIER_MST.DESCRIPTION.ToString();
                        cbIS_COMPANY.Checked = objERP_SUPPLIER_MST.IS_COMPANY == "Y" ? true : false;
                        drpCOMPANY_ID.SelectedValue = objERP_SUPPLIER_MST.COMPANY_ID.ToString();
                        txtRATING.Text = objERP_SUPPLIER_MST.RATING.ToString();
                        cbACTIVE_FLAG.Checked = objERP_SUPPLIER_MST.ACTIVE_FLAG == "Y" ? true : false;
                        txtIMG_URL.Text = objERP_SUPPLIER_MST.IMG_URL.ToString();
                        drpJOB_POSITION.SelectedValue = objERP_SUPPLIER_MST.JOB_POSITION.ToString();
                        txtADDRESS_ID.Text = objERP_SUPPLIER_MST.ADDRESS_ID.ToString();
                        //  txtCRUP_ID.Text = objERP_SUPPLIER_MST.CRUP_ID.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = str1;
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
            int Totalrec = DB.ERP_SUPPLIER_MST.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((CRMMaster)Page.Master).BindList(Listview1, (DB.ERP_SUPPLIER_MST.OrderBy(m => m.SUP_ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        public string getlocation(int Id)
        {
            string LID = Id.ToString();
            return DB.TBLLOCATIONs.Single(p => p.LOCATIONID == Id && p.TenentID == TID).LOCNAME1;
        }
        public string getcompnyname(int Id)
        {
            if (Id > 0)
                return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == Id && p.TenentID == TID).COMPNAME;
            else
                return "";
        }
        public string getjobposion(int Id)
        {
            return DB.tblCONTACTBus.Single(p => p.ContactMyID == Id && p.TenentID == TID).JobTitle;
        }
    }
}