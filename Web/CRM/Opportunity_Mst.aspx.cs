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
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;

namespace Web.CRM
{
    public partial class Opportunity_Mst : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        bool flag = false;
        #endregion
        int OID = 0;
       
        //CallEntities DB1 = new CallEntities();
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        bool Pageload = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (flag == false)
            {
                flag = true;
                Session["PageType"] = null;
                //Session["CampaignID"] = null;
            }
            Session["PageType"] = ActionMaster.Type.Opprtutnity.ToString();
            pnlSuccessMsg.Visible = false;
            if (!IsPostBack)
            {
                FistTimeLoad();
                ManageLang();

                pnlSuccessMsg.Visible = false;
                FillContractorID();
                BindReference();
                int CurrentID = 1;
                Session["PageType"] = ActionMaster.Type.Opprtutnity.ToString();
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);

                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                lblOpportunityOwner.Text = strUName;
                
                string UID = (((USER_MST)Session["USER"]).USER_ID).ToString();
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID == UID).Count() > 0)
                {
                    string strCompanyName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.USERID == UID).COMPNAME;
                    lblOpportunityOwner.Text = "Company : " + strCompanyName.ToString();
                }

                if (Request.QueryString["View"] == "Add")
                {
                    Write();
                    Clear();
                    btnAdd.Text = "Add";
                    //btnAdd.ValidationGroup = "send";
                    pnllist.Visible = false;
                    //paneloppref.Visible = true;
                    Pageload = false;
                    DivMain.Attributes["style"] = "display: none;";
                    DivAppoint.Attributes["style"] = "display: none;";
                }
                if (btnAdd.Text == "Add")
                {
                    btnAdd.ValidationGroup = "s";
                    DivMain.Attributes["style"] = "display: none;";
                    DivAppoint.Attributes["style"] = "display: none;";
                }
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                    DivMain.Attributes["style"] = "display: block;";
                    DivAppoint.Attributes["style"] = "display: block;";
                    BindCommand(ID);
                    Write();
                    //pnlSearchbutton.Visible = true;
                    btnAdd.Text = "Update";
                    ViewState["Edit"] = ID;
                    var List = DB.tbl_Opportunity_Mst.Where(p => p.Deleted == true && p.Active == true && p.OpportID == ID&&p.TenentID==TID).OrderByDescending(p => p.OpportID).ToList();
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    ((Web.CRM.CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                    Pageload = false;

                }

                if (Pageload == true)
                {
                    Readonly();
                    LastData();
                    BindData();
                }


                //string strRoleName = "";
                //if (strUName == "Ayo")
                //    strRoleName = DB.ERP_WEB_GEN_ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_NAME == "Super Admin").ROLE_NAME;
                //else
                //    strRoleName = DB.ERP_WEB_GEN_ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_NAME == "Simple User").ROLE_NAME;

                //lblRole.Text = "Role : " + strRoleName.ToString();


                //   FirstData();






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
            var List = DB.tbl_Opportunity_Mst.Where(p => p.Deleted == true && p.Active == true&&p.TenentID==TID).OrderByDescending(p => p.OpportID).Take(50).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((Web.CRM.CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        [System.Web.Services.WebMethod]

        public static string[] GetCustomer(string prefixText, int count)
        {

            try
            {

                string conStr;

                conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;

                string sqlQuery = "SELECT [COMPNAME1],[COMPID] FROM [TBLCOMPANYSETUP] WHERE" + " COMPNAME1 like @SearchTitle  + '%'";

                SqlConnection conn = new SqlConnection(conStr);

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                cmd.Parameters.AddWithValue("@SearchTitle", prefixText);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<string> custList = new List<string>();

                string custItem = string.Empty;
                while (dr.Read())
                {

                    custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());

                    custList.Add(custItem);

                }
                conn.Close();

                dr.Close();
                return custList.ToArray();
            }

            catch (Exception ex)
            {
                throw ex;

            }
        }
        [System.Web.Services.WebMethod]
        public static string[] GetSupplier(string prefixText, int count)
        {
            try
            {
                string conStr;
                conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
                string sqlQuery = "SELECT [COMPNAME1],[COMPID] FROM [TBLCOMPANYSETUP] WHERE" + " COMPNAME1 like @SearchTitle  + '%'";
                SqlConnection conn = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@SearchTitle", prefixText);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                List<string> custList = new List<string>();
                string custItem = string.Empty;
                while (dr.Read())
                {
                    custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
                    custList.Add(custItem);
                }
                conn.Close();
                dr.Close();
                return custList.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [System.Web.Services.WebMethod]
        public static string[] GetContact(string prefixText, int count)
        {
            try
            {
                string conStr;
                conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
                string sqlQuery = "SELECT [PersName1],[ContactMyID] FROM [TBLCONTACT] WHERE" + " PersName1 like @SearchTitle  + '%'";
                SqlConnection conn = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@SearchTitle", prefixText);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                List<string> custList = new List<string>();
                string custItem = string.Empty;
                while (dr.Read())
                {
                    custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
                    custList.Add(custItem);
                }
                conn.Close();
                dr.Close();
                return custList.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region PAge Genarator
        public void GetShow()
        {

            lblConact_id1s.Attributes["class"] = lblCustomer_id1s.Attributes["class"] = lblSupplier_id1s.Attributes["class"] = lblProject_id1s.Attributes["class"] = lblName1s.Attributes["class"] = lblopportunity_type1s.Attributes["class"] = lbllead_source1s.Attributes["class"] = lblAmount1s.Attributes["class"] = lblAmountBackup1s.Attributes["class"] = lblAmountUSdollar1s.Attributes["class"] = lblDateClosed1s.Attributes["class"] = lblNextStep1s.Attributes["class"] = lblSalesStage1s.Attributes["class"] = lblProbability1s.Attributes["class"] = lblDateEntered1s.Attributes["class"] = lblDateModified1s.Attributes["class"] = lblCampaignName1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblAccountName1s.Attributes["class"] = lblTeamName1s.Attributes["class"] = lblAssignedName1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblConact_id2h.Attributes["class"] = lblCustomer_id2h.Attributes["class"] = lblSupplier_id2h.Attributes["class"] = lblProject_id2h.Attributes["class"] = lblName2h.Attributes["class"] = lblopportunity_type2h.Attributes["class"] = lbllead_source2h.Attributes["class"] = lblAmount2h.Attributes["class"] = lblAmountBackup2h.Attributes["class"] = lblAmountUSdollar2h.Attributes["class"] = lblDateClosed2h.Attributes["class"] = lblNextStep2h.Attributes["class"] = lblSalesStage2h.Attributes["class"] = lblProbability2h.Attributes["class"] = lblDateEntered2h.Attributes["class"] = lblDateModified2h.Attributes["class"] = lblCampaignName2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblAccountName2h.Attributes["class"] = lblTeamName2h.Attributes["class"] = lblAssignedName2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblConact_id1s.Attributes["class"] = lblCustomer_id1s.Attributes["class"] = lblSupplier_id1s.Attributes["class"] = lblProject_id1s.Attributes["class"] = lblName1s.Attributes["class"] = lblopportunity_type1s.Attributes["class"] = lbllead_source1s.Attributes["class"] = lblAmount1s.Attributes["class"] = lblAmountBackup1s.Attributes["class"] = lblAmountUSdollar1s.Attributes["class"] = lblDateClosed1s.Attributes["class"] = lblNextStep1s.Attributes["class"] = lblSalesStage1s.Attributes["class"] = lblProbability1s.Attributes["class"] = lblDateEntered1s.Attributes["class"] = lblDateModified1s.Attributes["class"] = lblCampaignName1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblAccountName1s.Attributes["class"] = lblTeamName1s.Attributes["class"] = lblAssignedName1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblConact_id2h.Attributes["class"] = lblCustomer_id2h.Attributes["class"] = lblSupplier_id2h.Attributes["class"] = lblProject_id2h.Attributes["class"] = lblName2h.Attributes["class"] = lblopportunity_type2h.Attributes["class"] = lbllead_source2h.Attributes["class"] = lblAmount2h.Attributes["class"] = lblAmountBackup2h.Attributes["class"] = lblAmountUSdollar2h.Attributes["class"] = lblDateClosed2h.Attributes["class"] = lblNextStep2h.Attributes["class"] = lblSalesStage2h.Attributes["class"] = lblProbability2h.Attributes["class"] = lblDateEntered2h.Attributes["class"] = lblDateModified2h.Attributes["class"] = lblCampaignName2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblAccountName2h.Attributes["class"] = lblTeamName2h.Attributes["class"] = lblAssignedName2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpOpportID.SelectedIndex = 0;
            //drpConact_id.SelectedIndex = 0;
            txtsercustomer.Text = "";
            txtsupplier.Text = "";
            drpProject_id.SelectedIndex = 0;
            //drpRefNo123.SelectedIndex = 0;
            TxtContact.Text = "";
            //drocontect.SelectedIndex = 0;
            //DDLCampaingName.SelectedIndex = 0;
            DDLStage.SelectedIndex = 0;
            DDLLossReason.SelectedIndex = 0;
            txtName.Text = "";
            // txtopportunity_type.Text = "";
            ddlType.SelectedIndex = 0;
            txtlead_source.Text = "";
            txtAmount.Text = "0";
            txtAmountBackup.Text = "";
            txtAmountUSdollar.Text = "";
            txtDateClosed.Text = DateTime.Now.ToShortDateString();
            txtNextStep.Text = "";
            txtSalesStage.Text = "";
            txtProbability.Text = "";
            txtDateEntered.Text = DateTime.Now.ToShortDateString();
            txtDateModified.Text = "";
            //txtCampaignName.Text = "";
            txtDescription.Text = "";
            txtAccountName.Text = "";
            drpTeamName.SelectedIndex = 0;
            txtAssignedName.Text = "";
            txtoppname2.Text = "";
            txtoppname3.Text = "";
            txtDateClosed.Text = "";
        //    drpassingTeamLeader.SelectedIndex = 0;
            //DrpQuestion.SelectedIndex = 0;
            //DrpSearchTitle.SelectedIndex = 0;
           
            //drpCreatedBy.SelectedIndex = 0;
            //drpModifiedBy.SelectedIndex = 0;
            //txtActive.Text = "";
            //txtDeleted.Text = "";
            //txtCRUP_ID.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int Reference = 0;
            //using (var scope = new System.Transactions.TransactionScope())
            //{

            if (btnAdd.Text == "AddNew")
            {

                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "s";
                //paneloppref.Visible = true;

            }
            else if (btnAdd.Text == "Add")
            {
                btnAdd.ValidationGroup = "send";
                int OppID = DB.tbl_Opportunity_Mst.Where(p=>p.TenentID==TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Opportunity_Mst.Where(p=>p.TenentID==TID).Max(p => p.OpportID) + 1) : 1;
                Database.tbl_Opportunity_Mst objtbl_Opportunity_Mst = new Database.tbl_Opportunity_Mst();
                //Server Content Send data Yogesh
                int TID1 = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                objtbl_Opportunity_Mst.OpportID = OppID;
                ViewState["CampaignID"] = OppID;//DB.tbl_Opportunity_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_Opportunity_Mst.Max(p => p.OpportID) + 1) : 1;
                objtbl_Opportunity_Mst.Name = txtName.Text;
                //  objtbl_Opportunity_Mst.opportunity_type = txtopportunity_type.Text;
                //if (Convert.ToInt32(drpConact_id.SelectedValue) != 0)
                //{
                //    objtbl_Opportunity_Mst.Conact_id = Convert.ToInt32(drpConact_id.SelectedValue);
                //}

                objtbl_Opportunity_Mst.Customer_Name = txtsercustomer.Text;
                objtbl_Opportunity_Mst.Supplier_Name = txtsupplier.Text;
                if (Convert.ToInt32(drpProject_id.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.Project_id = Convert.ToInt32(drpProject_id.SelectedValue);
                }
                if (Convert.ToInt32(ddlType.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.opportunity_type = Convert.ToInt32(ddlType.SelectedValue);
                }
                if (Convert.ToInt32(DDLStage.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.Stage = Convert.ToInt32(DDLStage.SelectedValue);
                }
                if (Convert.ToInt32(DDLLossReason.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.LossReason = Convert.ToInt32(DDLLossReason.SelectedValue);
                }
                if (Convert.ToInt32(DDLCampaingName.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.Campaign_ID = Convert.ToInt32(DDLCampaingName.SelectedValue);
                }
                if (Convert.ToInt32(DDLCampaingName.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.CampaignName = DDLCampaingName.SelectedItem.Text.ToString();
                }

                // Bhavesh //if (Convert.ToInt32(drpRefNo123.SelectedValue) != 0)
                //{
                //    objtbl_Opportunity_Mst.RefNO = Convert.ToInt32(drpRefNo123.SelectedValue);
                //    Reference = Convert.ToInt32(drpRefNo123.SelectedValue);
                //}
                if (HiddenField2.Value != null && HiddenField2.Value!="")
                {
                    objtbl_Opportunity_Mst.Conact_id = Convert.ToInt32(HiddenField2.Value);
                }
                
                //objtbl_Opportunity_Mst.Conact_id = txtContactName.Text.ToString();
                objtbl_Opportunity_Mst.lead_source = txtlead_source.Text;
                objtbl_Opportunity_Mst.Amount = Convert.ToDecimal(txtAmount.Text);
                objtbl_Opportunity_Mst.AmountBackup = txtAmountBackup.Text;
                objtbl_Opportunity_Mst.AmountUSdollar = txtAmountUSdollar.Text;
                objtbl_Opportunity_Mst.DateClosed = Convert.ToDateTime(txtDateClosed.Text);
                objtbl_Opportunity_Mst.NextStep = txtNextStep.Text;
                objtbl_Opportunity_Mst.SalesStage = txtSalesStage.Text;
                objtbl_Opportunity_Mst.Probability = txtProbability.Text;
                objtbl_Opportunity_Mst.DateEntered = DateTime.Now;
                objtbl_Opportunity_Mst.DateModified = DateTime.Now;
                //objtbl_Opportunity_Mst.CampaignName = txtCampaignName.Text;
                objtbl_Opportunity_Mst.Description = txtDescription.Text;
                objtbl_Opportunity_Mst.AccountName = txtAccountName.Text;
                if (Convert.ToInt32(drpTeamName.SelectedValue) != 0)
                {
                    objtbl_Opportunity_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                }
                if (drpassingTeamLeader.SelectedValue != "0" && drpassingTeamLeader.SelectedValue != null)
                {
                    objtbl_Opportunity_Mst.TeamLeader =Convert.ToInt32(drpassingTeamLeader.SelectedValue);
                }

                objtbl_Opportunity_Mst.AssignedName = txtAssignedName.Text;
                //objtbl_Opportunity_Mst.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                //objtbl_Opportunity_Mst.ModifiedBy = Convert.ToInt32(drpModifiedBy.SelectedValue);
                // objtbl_Opportunity_Mst.Active = cbActive.Checked;
                //  objtbl_Opportunity_Mst.Deleted = cbDeleted.Checked;
                //objtbl_Opportunity_Mst.CRUP_ID = txtCRUP_ID.Text;
                objtbl_Opportunity_Mst.CreatedBy = 1;
                objtbl_Opportunity_Mst.ModifiedBy = 1;
                objtbl_Opportunity_Mst.Deleted = true;
                objtbl_Opportunity_Mst.Active = true;
                objtbl_Opportunity_Mst.TenentID = TID1;

                objtbl_Opportunity_Mst.OppName1 = txtName.Text;
                objtbl_Opportunity_Mst.OppName2 = txtoppname2.Text;
                objtbl_Opportunity_Mst.OppName3 = txtoppname3.Text;

                //objtbl_Opportunity_Mst.TeamLeader = txtTeamLeader.Text;
                DB.tbl_Opportunity_Mst.AddObject(objtbl_Opportunity_Mst);
                DB.SaveChanges();
                ViewState["OppID"] = OppID;
                //if (ViewState["RefID"] != null && ViewState["OppID"] != null)
                //{
                //    int Refid = Convert.ToInt32(ViewState["RefID"]);
                //    int OppID1 = Convert.ToInt32(ViewState["OppID"]);
                //    Database.REFTABLE obj_Ref = DB.REFTABLE.SingleOrDefault(p => p.REFID == Refid);
                //    obj_Ref.SWITCH3 = "220001Opp" + OppID1;
                //    DB.SaveChanges();
                //}
                int TID11 = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int CampID = 0;
                //if (DB.tbl_Opportunity_Mst.Count() == 0)
                //{
                if (ViewState["CampaignID"] != null)
                {

                    CampID = Convert.ToInt32(ViewState["CampaignID"]);
                    //string Refrction = drpRefNo123.SelectedValue;
                    string Name = txtName.Text;
                    string Discrption = txtDescription.Text;
                    //InsertCRMMainActivity(CampID, Refrction, Name, Discrption);
                    //  CRMMainActivity(TID11, Reference, CampID);
                }

                //}

                //  CRMActivity(TID11, Reference);
                //  Clear();
                lblMsg.Text = "  Data Save Successfully";
                pnlSuccessMsg.Visible = true;
                BindData();
                navigation.Visible = true;
                btnAdd.Text = "AddNew";
                Readonly();
                LastData();
            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.tbl_Opportunity_Mst objtbl_Opportunity_Mst = DB.tbl_Opportunity_Mst.Single(p => p.OpportID == ID&&p.TenentID==TID);

                    //objtbl_Opportunity_Mst.OpportID = ID;
                    //objtbl_Opportunity_Mst.OpportID = DB.tbl_Opportunity_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_Opportunity_Mst.Max(p => p.OpportID) + 1) : 1;
                    //if (Convert.ToInt32(drpConact_id.SelectedValue) != 0)
                    //{
                    //    objtbl_Opportunity_Mst.Conact_id = Convert.ToInt32(drpConact_id.SelectedValue);
                    //}
                    //if (Convert.ToInt32(drpCustomer_id.SelectedValue) != 0)
                    //{
                    objtbl_Opportunity_Mst.Customer_Name = txtsercustomer.Text;
                    //}

                    objtbl_Opportunity_Mst.Supplier_Name = txtsupplier.Text;

                    if (Convert.ToInt32(drpProject_id.SelectedValue) != 0)
                    {
                        objtbl_Opportunity_Mst.Project_id = Convert.ToInt32(drpProject_id.SelectedValue);
                    }
                    objtbl_Opportunity_Mst.Name = txtName.Text;
                    //   objtbl_Opportunity_Mst.opportunity_type = txtopportunity_type.Text;
                    if (Convert.ToInt32(ddlType.SelectedValue) != 0)
                    {
                        objtbl_Opportunity_Mst.opportunity_type = Convert.ToInt32(ddlType.SelectedValue);
                    }
                    //if (Convert.ToInt32(drpRefNo123.SelectedValue) != 0)
                    //{
                    //    objtbl_Opportunity_Mst.RefNO = Convert.ToInt32(drpRefNo123.SelectedValue);
                    //}
                    objtbl_Opportunity_Mst.lead_source = txtlead_source.Text;
                    objtbl_Opportunity_Mst.Amount = Convert.ToDecimal(txtAmount.Text);
                    objtbl_Opportunity_Mst.AmountBackup = txtAmountBackup.Text;
                    objtbl_Opportunity_Mst.AmountUSdollar = txtAmountUSdollar.Text;
                    objtbl_Opportunity_Mst.DateClosed = Convert.ToDateTime(txtDateClosed.Text);
                    objtbl_Opportunity_Mst.NextStep = txtNextStep.Text;
                    objtbl_Opportunity_Mst.SalesStage = txtSalesStage.Text;
                    objtbl_Opportunity_Mst.Probability = txtProbability.Text;
                    objtbl_Opportunity_Mst.DateEntered = Convert.ToDateTime(txtDateEntered.Text);
                    objtbl_Opportunity_Mst.DateModified = Convert.ToDateTime(txtDateModified.Text);
                    //objtbl_Opportunity_Mst.CampaignName = txtCampaignName.Text;

                    if (HiddenField2.Value != null && HiddenField2.Value != "")
                    {
                        objtbl_Opportunity_Mst.Conact_id = Convert.ToInt32(HiddenField2.Value);
                    }
                    
                    
                    //objtbl_Opportunity_Mst.CampaignName = Convert.ToInt32(DDLCampaingName.SelectedValue);
                    if (Convert.ToInt32(DDLCampaingName.SelectedValue) != 0)
                    {
                        objtbl_Opportunity_Mst.Campaign_ID = Convert.ToInt32(DDLCampaingName.SelectedValue);
                    }
                    if (DDLCampaingName.SelectedItem.Text != null)
                    {
                        objtbl_Opportunity_Mst.CampaignName = DDLCampaingName.SelectedItem.Text.ToString();
                    }
                    objtbl_Opportunity_Mst.Description = txtDescription.Text;
                    objtbl_Opportunity_Mst.AccountName = txtAccountName.Text;
                    if (Convert.ToInt32(drpTeamName.SelectedValue) != 0)
                    {
                        objtbl_Opportunity_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                    }
                    if (drpassingTeamLeader.SelectedValue != "0")
                    {
                        objtbl_Opportunity_Mst.TeamLeader = Convert.ToInt32(drpassingTeamLeader.SelectedValue);
                    }

                    //objtbl_Opportunity_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);

                    //objtbl_Opportunity_Mst.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                    //objtbl_Opportunity_Mst.ModifiedBy = Convert.ToInt32(drpModifiedBy.SelectedValue);
                    objtbl_Opportunity_Mst.Active = cbActive.Checked;
                    objtbl_Opportunity_Mst.OppName1 = txtName.Text;
                    objtbl_Opportunity_Mst.OppName2 = txtoppname2.Text;
                    objtbl_Opportunity_Mst.OppName3 = txtoppname3.Text;
                    //objtbl_Opportunity_Mst.TeamLeader = txtTeamLeader.Text;
                    objtbl_Opportunity_Mst.AssignedName = txtAssignedName.Text;

                    ViewState["Edit"] = null;
                    btnAdd.ValidationGroup = "submit";
                    btnAdd.Text = "AddNew";
                    DB.SaveChanges();
                    //  Clear();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    navigation.Visible = true;
                    Readonly();
                    pnlSearchbutton.Visible = false;    
                    //  FirstData();
                    Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                    DivMain.Attributes["style"] = "display: none;";
                    DivAppoint.Attributes["style"] = "display: none;";
                }
            }
            BindData();

            //    scope.Complete(); //  To commit.
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            LastData();
            Readonly();
            Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
            DivMain.Attributes["style"] = "display: none;";
            DivAppoint.Attributes["style"] = "display: none;";
            btnAdd.Text = "AddNew";
            pnlSearchbutton.Visible = false;  
            //Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpConact_id.DataSource = DB.TBLCONTACT.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY");
            //drpConact_id.DataTextField = "PersName1";
            //drpConact_id.DataValueField = "ContactMyID";
            //drpConact_id.DataBind();
            //drpConact_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpCustomer_id.DataSource = DB.TBLCONTACT.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY");
            //drpCustomer_id.DataTextField = "PersName1";
            //drpCustomer_id.DataValueField = "ContactMyID";
            //drpCustomer_id.DataBind();
            //drpCustomer_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            //    Classes.CRMClass.getcrmdropdown(drpCustomer_id, TID, "", "", "", "TBLCOMPANYSETUP");

            //drpSupplier_id.DataSource = DB.TBLCONTACT.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY");
            //drpSupplier_id.DataTextField = "PersName1";
            //drpSupplier_id.DataValueField = "ContactMyID";
            //drpSupplier_id.DataBind();
            //drpSupplier_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            //    Classes.CRMClass.getcrmdropdown(drpSupplier_id, TID, "", "", "", "TBLCOMPANYSETUP");

            //Classes.CRMClass.getcrmdropdown(drocontect, TID, "", "", "", "TBLCONTACT");

            //select * from TBLCONTACT where TenentID = 1

            //drpProject_id.DataSource = DB.CRM_TBLPROJECT.Where(p => p.TenentID == TID);
            //drpProject_id.DataTextField = "PROJECTNAME1";
            //drpProject_id.DataValueField = "PROJECTID";
            //drpProject_id.DataBind();
            //drpProject_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpProject_id, TID, "", "", "", "TBLPROJECT");
            //select * from TBLPROJECT where TenentID = 1

            //drpTeamName.DataSource = DB.REFTABLE.Where(p =>p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS");
            //drpTeamName.DataTextField = "REFNAME1";
            //drpTeamName.DataValueField = "REFID";
            //drpTeamName.DataBind();
            //drpTeamName.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpTeamName, TID, "TeamTitle", "TeamTitle", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'TeamTitle' and REFSUBTYPE = 'TeamTitle'

            //ddlType.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "CRM" && p.REFSUBTYPE == "OpportunityType");
            //ddlType.DataTextField = "REFNAME1";
            //ddlType.DataValueField = "REFID";
            //ddlType.DataBind();
            //ddlType.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(ddlType, TID, "CRM", "OpportunityType", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'CRM' and REFSUBTYPE = 'OpportunityStage'

            //DDLStage.DataSource = DB.REFTABLE.Where(p =>p.REFTYPE == "CRM" && p.REFSUBTYPE == "OpportunityStage");
            //DDLStage.DataTextField = "REFNAME1";
            //DDLStage.DataValueField = "REFID";
            //DDLStage.DataBind();
            //DDLStage.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(DDLStage, TID, "CRM", "OpportunityStage", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'CRM' and REFSUBTYPE = 'OpportunityStage'

            //DDLLossReason.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "CRM" && p.REFSUBTYPE == "OpportunityLoss");
            //DDLLossReason.DataTextField = "REFNAME1";
            //DDLLossReason.DataValueField = "REFID";
            //DDLLossReason.DataBind();
            //DDLLossReason.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(DDLLossReason, TID, "CRM", "OpportunityLoss", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'CRM' and REFSUBTYPE = 'OpportunityStage'

            //DDLCampaingName.DataSource = DB.CRM_tbl_Campaign_Mst.Where(p => p.TenentID == TID && p.Active == true && p.Deleted == true);
            //DDLCampaingName.DataTextField = "Name";
            //DDLCampaingName.DataValueField = "ID";
            //DDLCampaingName.DataBind();
            //DDLCampaingName.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(DDLCampaingName, TID, "", "", "", "tbl_Campaign_Mst");
            //select * from tbl_Campaign_Mst

            //drpLEADSOURCE.DataSource = DB.REFTABLE.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "LEADSTATUS");
            //drpLEADSOURCE.DataTextField = "REFNAME1";
            //drpLEADSOURCE.DataValueField = "REFID";
            //drpLEADSOURCE.DataBind();
            //drpLEADSOURCE.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drplblopportunity_type1s.DataSource = DB.REFTABLE.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "LEADSTATUS");
            //drplblopportunity_type1s.DataTextField = "REFNAME1";
            //drplblopportunity_type1s.DataValueField = "REFID";
            //drplblopportunity_type1s.DataBind();
            //drplblopportunity_type1s.Items.Insert(0, new ListItem("-- Select --", "0"));
            // Classes.CRMClass.getcrmdropdown(DDLCampaingName, TID, "", "", "", "CRM_tbl_Campaign_Mst");
            Classes.EcommAdminClass.getdropdown(DrpQuestion, TID, "", "", "", "TBLGROUP");
            //select * from TBLGROUP

            //drpassingTeamLeader.Items.Insert(0, new ListItem("-- Select --", "0"));

            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            var TitleData = (from TitleRef in DB.REFTABLEs
                             join Search in DB.ISSearchDetails on TitleRef.REFID equals Search.REFID
                             where Search.CreatedBy == UID && TitleRef.REFTYPE == RefType &&TitleRef.TenentID==TID&&Search.TenentID==TID
                             select new
                             {
                                 ID = TitleRef.REFID,
                                 TitleRef.REFNAME1
                             }).ToList().Distinct();

            DrpSearchTitle.DataSource = TitleData;
            DrpSearchTitle.DataTextField = "REFNAME1";
            DrpSearchTitle.DataValueField = "ID";
            DrpSearchTitle.DataBind();
            DrpSearchTitle.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        #region PAge Genarator navigation
        
        public string GetOppertunityType(int OppID)
        {
            string OppType = "";
            int OPID = Convert.ToInt32(OppID);
            if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == OPID&&p.TenentID==TID).Count() > 0)
            {
                Database.tbl_Opportunity_Mst obj_Opp = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == OPID&&p.TenentID==TID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "CRM" && p.REFSUBTYPE == "OpportunityType" && p.REFID == obj_Opp.opportunity_type).Count() > 0)
                {
                    Database.REFTABLE obj_Ref = DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFTYPE == "CRM" && p.REFSUBTYPE == "OpportunityType" && p.REFID == obj_Opp.opportunity_type);
                    OppType = obj_Ref.REFNAME1;
                }
            }
            return OppType;
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
        int LIDID = 0;
        public void FirstData()
        {
            LIDID = DB.tbl_Opportunity_Mst.Where(p => p.Active == true&&p.TenentID==TID).FirstOrDefault().OpportID;

            Listview1.SelectedIndex = LIDID;
            BindCommand(LIDID);
            Readonly();


        }
        public void NextData()
        {
            Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
            LIDID = Convert.ToInt32(Listview1.SelectedIndex);
            Listview1.SelectedIndex = LIDID;
            BindCommand(LIDID);
            Readonly();

        }
        public void PrevData()
        {

            Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
            LIDID = Convert.ToInt32(Listview1.SelectedIndex);
            Listview1.SelectedIndex = LIDID;
            BindCommand(LIDID);
            Readonly();

        }
        public void LastData()
        {
            if (DB.tbl_Opportunity_Mst.Where(p => p.Active == true && p.TenentID == TID).Count() > 0)
            {
                LIDID = DB.tbl_Opportunity_Mst.Where(p => p.Active == true && p.TenentID == TID).Max(p => p.OpportID);
                BindCommand(LIDID);
                Listview1.SelectedIndex = LIDID;
                Readonly();
            }
            
        }
        #endregion

        #region PAge Genarator
        
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblConact_id2h.Visible = lblCustomer_id2h.Visible = lblSupplier_id2h.Visible = lblProject_id2h.Visible = lblName2h.Visible = lblopportunity_type2h.Visible = lbllead_source2h.Visible = lblAmount2h.Visible = lblAmountBackup2h.Visible = lblAmountUSdollar2h.Visible = lblDateClosed2h.Visible = lblNextStep2h.Visible = lblSalesStage2h.Visible = lblProbability2h.Visible = lblDateEntered2h.Visible = lblDateModified2h.Visible = lblCampaignName2h.Visible = lblDescription2h.Visible = lblAccountName2h.Visible = lblTeamName2h.Visible = lblAssignedName2h.Visible = lblActive2h.Visible = false;
                    //2true
                    txtConact_id2h.Visible = txtCustomer_id2h.Visible = txtSupplier_id2h.Visible = txtProject_id2h.Visible = txtName2h.Visible = txtopportunity_type2h.Visible = txtlead_source2h.Visible = txtAmount2h.Visible = txtAmountBackup2h.Visible = txtAmountUSdollar2h.Visible = txtDateClosed2h.Visible = txtNextStep2h.Visible = txtSalesStage2h.Visible = txtProbability2h.Visible = txtDateEntered2h.Visible = txtDateModified2h.Visible = txtCampaignName2h.Visible = txtDescription2h.Visible = txtAccountName2h.Visible = txtTeamName2h.Visible = txtAssignedName2h.Visible = txtActive2h.Visible = true;

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
                    lblConact_id2h.Visible = lblCustomer_id2h.Visible = lblSupplier_id2h.Visible = lblProject_id2h.Visible = lblName2h.Visible = lblopportunity_type2h.Visible = lbllead_source2h.Visible = lblAmount2h.Visible = lblAmountBackup2h.Visible = lblAmountUSdollar2h.Visible = lblDateClosed2h.Visible = lblNextStep2h.Visible = lblSalesStage2h.Visible = lblProbability2h.Visible = lblDateEntered2h.Visible = lblDateModified2h.Visible = lblCampaignName2h.Visible = lblDescription2h.Visible = lblAccountName2h.Visible = lblTeamName2h.Visible = lblAssignedName2h.Visible = lblActive2h.Visible = true;
                    //2false
                    txtConact_id2h.Visible = txtCustomer_id2h.Visible = txtSupplier_id2h.Visible = txtProject_id2h.Visible = txtName2h.Visible = txtopportunity_type2h.Visible = txtlead_source2h.Visible = txtAmount2h.Visible = txtAmountBackup2h.Visible = txtAmountUSdollar2h.Visible = txtDateClosed2h.Visible = txtNextStep2h.Visible = txtSalesStage2h.Visible = txtProbability2h.Visible = txtDateEntered2h.Visible = txtDateModified2h.Visible = txtCampaignName2h.Visible = txtDescription2h.Visible = txtAccountName2h.Visible = txtTeamName2h.Visible = txtAssignedName2h.Visible = txtActive2h.Visible = false;

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
                    lblConact_id1s.Visible = lblCustomer_id1s.Visible = lblSupplier_id1s.Visible = lblProject_id1s.Visible = lblName1s.Visible = lblopportunity_type1s.Visible = lbllead_source1s.Visible = lblAmount1s.Visible = lblAmountBackup1s.Visible = lblAmountUSdollar1s.Visible = lblDateClosed1s.Visible = lblNextStep1s.Visible = lblSalesStage1s.Visible = lblProbability1s.Visible = lblDateEntered1s.Visible = lblDateModified1s.Visible = lblCampaignName1s.Visible = lblDescription1s.Visible = lblAccountName1s.Visible = lblTeamName1s.Visible = lblAssignedName1s.Visible = lblActive1s.Visible = false;
                    //1true
                    txtConact_id1s.Visible = txtCustomer_id1s.Visible = txtSupplier_id1s.Visible = txtProject_id1s.Visible = txtName1s.Visible = txtopportunity_type1s.Visible = txtlead_source1s.Visible = txtAmount1s.Visible = txtAmountBackup1s.Visible = txtAmountUSdollar1s.Visible = txtDateClosed1s.Visible = txtNextStep1s.Visible = txtSalesStage1s.Visible = txtProbability1s.Visible = txtDateEntered1s.Visible = txtDateModified1s.Visible = txtCampaignName1s.Visible = txtDescription1s.Visible = txtAccountName1s.Visible = txtTeamName1s.Visible = txtAssignedName1s.Visible = txtActive1s.Visible = true;
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
                    lblConact_id1s.Visible = lblCustomer_id1s.Visible = lblSupplier_id1s.Visible = lblProject_id1s.Visible = lblName1s.Visible = lblopportunity_type1s.Visible = lbllead_source1s.Visible = lblAmount1s.Visible = lblAmountBackup1s.Visible = lblAmountUSdollar1s.Visible = lblDateClosed1s.Visible = lblNextStep1s.Visible = lblSalesStage1s.Visible = lblProbability1s.Visible = lblDateEntered1s.Visible = lblDateModified1s.Visible = lblCampaignName1s.Visible = lblDescription1s.Visible = lblAccountName1s.Visible = lblTeamName1s.Visible = lblAssignedName1s.Visible = lblActive1s.Visible = true;
                    //1false
                    txtConact_id1s.Visible = txtCustomer_id1s.Visible = txtSupplier_id1s.Visible = txtProject_id1s.Visible = txtName1s.Visible = txtopportunity_type1s.Visible = txtlead_source1s.Visible = txtAmount1s.Visible = txtAmountBackup1s.Visible = txtAmountUSdollar1s.Visible = txtDateClosed1s.Visible = txtNextStep1s.Visible = txtSalesStage1s.Visible = txtProbability1s.Visible = txtDateEntered1s.Visible = txtDateModified1s.Visible = txtCampaignName1s.Visible = txtDescription1s.Visible = txtAccountName1s.Visible = txtTeamName1s.Visible = txtAssignedName1s.Visible = txtActive1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((Web.CRM.CRMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Opportunity_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblConact_id1s.ID == item.LabelID)
                    txtConact_id1s.Text = lblConact_id1s.Text = item.LabelName;
                else if (lblCustomer_id1s.ID == item.LabelID)
                    txtCustomer_id1s.Text = lblCustomer_id1s.Text = lblhCustomer_id.Text = item.LabelName;
                else if (lblSupplier_id1s.ID == item.LabelID)
                    txtSupplier_id1s.Text = lblSupplier_id1s.Text = lblhSupplier_id.Text = item.LabelName;
                else if (lblProject_id1s.ID == item.LabelID)
                    txtProject_id1s.Text = lblProject_id1s.Text = item.LabelName;
                else if (lblName1s.ID == item.LabelID)
                    txtName1s.Text = lblName1s.Text = lblhName.Text = item.LabelName;
                else if (lblopportunity_type1s.ID == item.LabelID)
                    txtopportunity_type1s.Text = lblopportunity_type1s.Text = lblhopportunity_type.Text = item.LabelName;
                else if (lbllead_source1s.ID == item.LabelID)
                    txtlead_source1s.Text = lbllead_source1s.Text = lblhlead_source.Text = item.LabelName;
                else if (lblAmount1s.ID == item.LabelID)
                    txtAmount1s.Text = lblAmount1s.Text = item.LabelName;
                else if (lblAmountBackup1s.ID == item.LabelID)
                    txtAmountBackup1s.Text = lblAmountBackup1s.Text = item.LabelName;
                else if (lblAmountUSdollar1s.ID == item.LabelID)
                    txtAmountUSdollar1s.Text = lblAmountUSdollar1s.Text = item.LabelName;
                else if (lblDateClosed1s.ID == item.LabelID)
                    txtDateClosed1s.Text = lblDateClosed1s.Text = item.LabelName;
                else if (lblNextStep1s.ID == item.LabelID)
                    txtNextStep1s.Text = lblNextStep1s.Text = item.LabelName;
                else if (lblSalesStage1s.ID == item.LabelID)
                    txtSalesStage1s.Text = lblSalesStage1s.Text = lblhSalesStage.Text = item.LabelName;
                else if (lblProbability1s.ID == item.LabelID)
                    txtProbability1s.Text = lblProbability1s.Text = item.LabelName;
                else if (lblDateEntered1s.ID == item.LabelID)
                    txtDateEntered1s.Text = lblDateEntered1s.Text = item.LabelName;
                else if (lblDateModified1s.ID == item.LabelID)
                    txtDateModified1s.Text = lblDateModified1s.Text = item.LabelName;
                else if (lblCampaignName1s.ID == item.LabelID)
                    txtCampaignName1s.Text = lblCampaignName1s.Text = item.LabelName;
                else if (lblDescription1s.ID == item.LabelID)
                    txtDescription1s.Text = lblDescription1s.Text = item.LabelName;
                else if (lblAccountName1s.ID == item.LabelID)
                    txtAccountName1s.Text = lblAccountName1s.Text = item.LabelName;
                else if (lblTeamName1s.ID == item.LabelID)
                    txtTeamName1s.Text = lblTeamName1s.Text = lblhTeamName.Text = item.LabelName;
                else if (lblAssignedName1s.ID == item.LabelID)
                    txtAssignedName1s.Text = lblAssignedName1s.Text = item.LabelName;


                else if (lblConact_id2h.ID == item.LabelID)
                    txtConact_id2h.Text = lblConact_id2h.Text = item.LabelName;
                else if (lblCustomer_id2h.ID == item.LabelID)
                    txtCustomer_id2h.Text = lblCustomer_id2h.Text = lblhCustomer_id.Text = item.LabelName;
                else if (lblSupplier_id2h.ID == item.LabelID)
                    txtSupplier_id2h.Text = lblSupplier_id2h.Text = lblhSupplier_id.Text = item.LabelName;
                else if (lblProject_id2h.ID == item.LabelID)
                    txtProject_id2h.Text = lblProject_id2h.Text = item.LabelName;
                else if (lblName2h.ID == item.LabelID)
                    txtName2h.Text = lblName2h.Text = lblhName.Text = item.LabelName;
                else if (lblopportunity_type2h.ID == item.LabelID)
                    txtopportunity_type2h.Text = lblopportunity_type2h.Text = lblhopportunity_type.Text = item.LabelName;
                else if (lbllead_source2h.ID == item.LabelID)
                    txtlead_source2h.Text = lbllead_source2h.Text = lblhlead_source.Text = item.LabelName;
                else if (lblAmount2h.ID == item.LabelID)
                    txtAmount2h.Text = lblAmount2h.Text = item.LabelName;
                else if (lblAmountBackup2h.ID == item.LabelID)
                    txtAmountBackup2h.Text = lblAmountBackup2h.Text = item.LabelName;
                else if (lblAmountUSdollar2h.ID == item.LabelID)
                    txtAmountUSdollar2h.Text = lblAmountUSdollar2h.Text = item.LabelName;
                else if (lblDateClosed2h.ID == item.LabelID)
                    txtDateClosed2h.Text = lblDateClosed2h.Text = item.LabelName;
                else if (lblNextStep2h.ID == item.LabelID)
                    txtNextStep2h.Text = lblNextStep2h.Text = item.LabelName;
                else if (lblSalesStage2h.ID == item.LabelID)
                    txtSalesStage2h.Text = lblSalesStage2h.Text = lblhSalesStage.Text = item.LabelName;
                else if (lblProbability2h.ID == item.LabelID)
                    txtProbability2h.Text = lblProbability2h.Text = item.LabelName;
                else if (lblDateEntered2h.ID == item.LabelID)
                    txtDateEntered2h.Text = lblDateEntered2h.Text = item.LabelName;
                else if (lblDateModified2h.ID == item.LabelID)
                    txtDateModified2h.Text = lblDateModified2h.Text = item.LabelName;
                else if (lblCampaignName2h.ID == item.LabelID)
                    txtCampaignName2h.Text = lblCampaignName2h.Text = item.LabelName;
                else if (lblDescription2h.ID == item.LabelID)
                    txtDescription2h.Text = lblDescription2h.Text = item.LabelName;
                else if (lblAccountName2h.ID == item.LabelID)
                    txtAccountName2h.Text = lblAccountName2h.Text = item.LabelName;
                else if (lblTeamName2h.ID == item.LabelID)
                    txtTeamName2h.Text = lblTeamName2h.Text = lblhTeamName.Text = item.LabelName;
                else if (lblAssignedName2h.ID == item.LabelID)
                    txtAssignedName2h.Text = lblAssignedName2h.Text = item.LabelName;

                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;


                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((Web.CRM.CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Opportunity_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\CRM_tbl_Opportunity_Mst.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Opportunity_Mst").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblConact_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConact_id1s.Text;
                else if (lblCustomer_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomer_id1s.Text;
                else if (lblSupplier_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplier_id1s.Text;
                else if (lblProject_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProject_id1s.Text;
                else if (lblName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName1s.Text;
                else if (lblopportunity_type1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtopportunity_type1s.Text;
                else if (lbllead_source1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlead_source1s.Text;
                else if (lblAmount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmount1s.Text;
                else if (lblAmountBackup1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmountBackup1s.Text;
                else if (lblAmountUSdollar1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmountUSdollar1s.Text;
                else if (lblDateClosed1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateClosed1s.Text;
                else if (lblNextStep1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNextStep1s.Text;
                else if (lblSalesStage1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalesStage1s.Text;
                else if (lblProbability1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProbability1s.Text;
                else if (lblDateEntered1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateEntered1s.Text;
                else if (lblDateModified1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateModified1s.Text;
                else if (lblCampaignName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCampaignName1s.Text;
                else if (lblDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription1s.Text;
                else if (lblAccountName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAccountName1s.Text;
                else if (lblTeamName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTeamName1s.Text;
                else if (lblAssignedName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssignedName1s.Text;

                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;



                else if (lblConact_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConact_id2h.Text;
                else if (lblCustomer_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomer_id2h.Text;
                else if (lblSupplier_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplier_id2h.Text;
                else if (lblProject_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProject_id2h.Text;
                else if (lblName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName2h.Text;
                else if (lblopportunity_type2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtopportunity_type2h.Text;
                else if (lbllead_source2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlead_source2h.Text;
                else if (lblAmount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmount2h.Text;
                else if (lblAmountBackup2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmountBackup2h.Text;
                else if (lblAmountUSdollar2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAmountUSdollar2h.Text;
                else if (lblDateClosed2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateClosed2h.Text;
                else if (lblNextStep2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNextStep2h.Text;
                else if (lblSalesStage2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalesStage2h.Text;
                else if (lblProbability2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProbability2h.Text;
                else if (lblDateEntered2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateEntered2h.Text;
                else if (lblDateModified2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateModified2h.Text;
                else if (lblCampaignName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCampaignName2h.Text;
                else if (lblDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription2h.Text;
                else if (lblAccountName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAccountName2h.Text;
                else if (lblTeamName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTeamName2h.Text;
                else if (lblAssignedName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssignedName2h.Text;

                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;


                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\CRM_tbl_Opportunity_Mst.xml"));

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
            //drpRefNo123.Enabled = true;
            TxtContact.Enabled = true;
            //drocontect.Enabled = true;
            txtoppname2.Enabled = true;
            txtoppname3.Enabled = true;
            //txtTeamLeader.Enabled = true;
            DrpQuestion.Enabled = true;
            DrpSearchTitle.Enabled = true;
            //  drpOpportID.Enabled = true;
            //drpConact_id.Enabled = true;
            txtsercustomer.Enabled = true;
            txtsupplier.Enabled = true;
            drpProject_id.Enabled = true;
            txtName.Enabled = true;
            ddlType.Enabled = true;
            DDLStage.Enabled = true;
            DDLLossReason.Enabled = true;
            // txtopportunity_type.Enabled = true;
            txtlead_source.Enabled = true;
            txtAmount.Enabled = true;
            txtAmountBackup.Enabled = true;
            txtAmountUSdollar.Enabled = true;
            txtDateClosed.Enabled = true;
            txtNextStep.Enabled = true;
            txtSalesStage.Enabled = true;
            txtProbability.Enabled = true;
            txtDateEntered.Enabled = true;
            txtDateModified.Enabled = true;
            // txtCampaignName.Enabled = true;
            DDLCampaingName.Enabled = true;
            txtDescription.Enabled = true;
            txtAccountName.Enabled = true;
            drpTeamName.Enabled = true;
            drpassingTeamLeader.Enabled = true;
            txtAssignedName.Enabled = true;
            //drpCreatedBy.Enabled = true;
            //drpModifiedBy.Enabled = true;
            cbActive.Enabled = true;
            //cbDeleted.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //drpRefNo123.Enabled = false;
            TxtContact.Enabled = false;
            //drocontect.Enabled = false;
            DrpQuestion.Enabled = false;
            DrpSearchTitle.Enabled = false;
            txtoppname2.Enabled = false;
            txtoppname3.Enabled = false;
            //txtTeamLeader.Enabled = false;
            drpassingTeamLeader.Enabled = false;
            //drpConact_id.Enabled = false;
            txtsercustomer.Enabled = false;
            txtsupplier.Enabled = false;
            drpProject_id.Enabled = false;
            ddlType.Enabled = false;
            txtName.Enabled = false;
            DDLStage.Enabled = false;
            DDLLossReason.Enabled = false;
            DDLCampaingName.Enabled = false;
            // txtopportunity_type.Enabled = false;
            txtlead_source.Enabled = false;
            txtAmount.Enabled = false;
            txtAmountBackup.Enabled = false;
            txtAmountUSdollar.Enabled = false;
            txtDateClosed.Enabled = false;
            txtNextStep.Enabled = false;
            txtSalesStage.Enabled = false;
            txtProbability.Enabled = false;
            txtDateEntered.Enabled = false;
            txtDateModified.Enabled = false;
            // txtCampaignName.Enabled = false;
            txtDescription.Enabled = false;
            txtAccountName.Enabled = false;
            drpTeamName.Enabled = false;
            txtAssignedName.Enabled = false;
            cbActive.Enabled = false;



        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Opportunity_Mst.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Opportunity_Mst.OrderBy(m => m.OpportID).Take(take).Skip(Skip)).ToList());
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

            ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbl_Opportunity_Mst.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Opportunity_Mst.OrderBy(m => m.OpportID).Take(take).Skip(Skip)).ToList());
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
                ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbl_Opportunity_Mst.Count();
                take = Showdata;
                Skip = 0;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Opportunity_Mst.OrderBy(m => m.OpportID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB.tbl_Opportunity_Mst.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Opportunity_Mst.OrderBy(m => m.OpportID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            using (var scope = new System.Transactions.TransactionScope())
            {
                //try
                //{
                    if (e.CommandName == "btnDelete")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        Database.tbl_Opportunity_Mst objtbl_Opportunity_Mst = DB.tbl_Opportunity_Mst.Single(p => p.OpportID == ID&&p.TenentID==TID);
                        objtbl_Opportunity_Mst.Active = false;
                        DB.SaveChanges();
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Opportunity_Mst.OrderBy(m => m.OpportID).Take(Tvalue).Skip(Svalue)).ToList());

                    }
                    if (e.CommandName == "btnView")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        BindCommand(ID);
                        Readonly();
                        Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                        DivMain.Attributes["style"] = "display: none;";
                        DivAppoint.Attributes["style"] = "display: none;";
                    }
                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                        DivMain.Attributes["style"] = "display: block;";
                        DivAppoint.Attributes["style"] = "display: block;";
                        //Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                        //DivMain.Attributes["style"] = "display: block;";
                        BindCommand(ID);
                        Write();
                        pnlSearchbutton.Visible = true;
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                    }
                    scope.Complete(); //  To commit.
                //}
                //catch (Exception ex)
                //{
                //    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                //    throw;
                //}
            }
        }

        public void BindCommand(int ID)
        {
            //paneloppref.Visible = false;
            //  pnlresponsive.Visible = false;

            //Session["CampaignID"] = ID;
            //string[] ID = e.CommandArgument.ToString().Split(',');
            //string str1 = ID[0].ToString();
            //string str2 = ID[1].ToString();

            Database.tbl_Opportunity_Mst objtbl_Opportunity_Mst = DB.tbl_Opportunity_Mst.Single(p => p.OpportID == ID&&p.TenentID==TID);
            //drpOpportID.SelectedValue = objtbl_Opportunity_Mst.OpportID.ToString();
            //if (objtbl_Opportunity_Mst.Conact_id != null)
            //{
            //    drpConact_id.SelectedValue = objtbl_Opportunity_Mst.Conact_id.ToString();
            //}

            txtsercustomer.Text = objtbl_Opportunity_Mst.Customer_Name;
            txtsupplier.Text = objtbl_Opportunity_Mst.Supplier_Name;
            if (objtbl_Opportunity_Mst.Project_id != null && objtbl_Opportunity_Mst.Project_id != 0)
            {
                drpProject_id.SelectedValue = objtbl_Opportunity_Mst.Project_id.ToString();
            }
            //if (objtbl_Opportunity_Mst.RefNO != null && objtbl_Opportunity_Mst.RefNO != 0)
            //{
            //    drpRefNo123.SelectedValue = objtbl_Opportunity_Mst.RefNO.ToString();
            //}
            txtName.Text = objtbl_Opportunity_Mst.Name;
            // txtopportunity_type.Text = objtbl_Opportunity_Mst.opportunity_type.ToString();
            //if (objtbl_Opportunity_Mst.opportunity_type!=null)
            //{
            //    ddlType.SelectedValue = objtbl_Opportunity_Mst.opportunity_type.ToString();
            //}
            if (objtbl_Opportunity_Mst.opportunity_type != null)
            {
                ddlType.SelectedValue = objtbl_Opportunity_Mst.opportunity_type.ToString();
            }
            if (objtbl_Opportunity_Mst.Stage != null)
            {
                DDLStage.SelectedValue = objtbl_Opportunity_Mst.Stage.ToString();
            }
            if (objtbl_Opportunity_Mst.LossReason != null)
            {
                DDLLossReason.SelectedValue = objtbl_Opportunity_Mst.LossReason.ToString();
            }
            //if (objtbl_Opportunity_Mst.Conact_id != null)
           
            int cont = Convert.ToInt32(objtbl_Opportunity_Mst.Conact_id);
            if (cont != 0)
                TxtContact.Text =DB.TBLCONTACTs.Where(p => p.ContactMyID == cont && p.TenentID == TID).Count()>0 ?DB.TBLCONTACTs.Single(p => p.ContactMyID == cont && p.TenentID == TID).PersName1:"";
            HiddenField2.Value = cont.ToString();
            txtlead_source.Text = objtbl_Opportunity_Mst.lead_source;
            txtAmount.Text = objtbl_Opportunity_Mst.Amount.ToString();
            txtAmountBackup.Text = objtbl_Opportunity_Mst.AmountBackup;
            txtAmountUSdollar.Text = objtbl_Opportunity_Mst.AmountUSdollar;
            txtDateClosed.Text = Convert.ToDateTime(objtbl_Opportunity_Mst.DateClosed).ToString("MM/dd/yyyy");
            txtNextStep.Text = objtbl_Opportunity_Mst.NextStep;
            txtSalesStage.Text = objtbl_Opportunity_Mst.SalesStage;
            txtProbability.Text = objtbl_Opportunity_Mst.Probability;
            txtDateEntered.Text = Convert.ToDateTime(objtbl_Opportunity_Mst.DateEntered).ToString("MM/dd/yyyy");
            txtDateModified.Text = Convert.ToDateTime(objtbl_Opportunity_Mst.DateModified).ToString("MM/dd/yyyy");
            // txtCampaignName.Text = objtbl_Opportunity_Mst.CampaignName.ToString();
            if (objtbl_Opportunity_Mst.Campaign_ID != null)
            {
                DDLCampaingName.SelectedValue = objtbl_Opportunity_Mst.Campaign_ID.ToString();
            }
            //if (objtbl_Opportunity_Mst.CampaignName != null)
            //{
            //    DDLCampaingName.SelectedItem.Text = objtbl_Opportunity_Mst.CampaignName.ToString();
            //}
            txtDescription.Text = objtbl_Opportunity_Mst.Description;
            txtAccountName.Text = objtbl_Opportunity_Mst.AccountName;
            if (objtbl_Opportunity_Mst.TeamName != null)
            {
                int RID = Convert.ToInt32(objtbl_Opportunity_Mst.TeamName);
                drpTeamName.SelectedValue = objtbl_Opportunity_Mst.TeamName.ToString();
                BindTimeAssin(RID);
            }
            if (objtbl_Opportunity_Mst.TeamLeader != null)
            {
                drpassingTeamLeader.SelectedValue = objtbl_Opportunity_Mst.TeamLeader.ToString();
            }

            //if (objtbl_Opportunity_Mst.TeamName != null)
            //    drpTeamName.SelectedValue = objtbl_Opportunity_Mst.TeamName.ToString();
            txtAssignedName.Text = objtbl_Opportunity_Mst.AssignedName;
            //drpCreatedBy.SelectedValue = objtbl_Opportunity_Mst.CreatedBy.ToString();
            //drpModifiedBy.SelectedValue = objtbl_Opportunity_Mst.ModifiedBy.ToString();
            cbActive.Checked = (objtbl_Opportunity_Mst.Active == true) ? true : false;
            txtName.Text = objtbl_Opportunity_Mst.OppName1;
            txtoppname2.Text = objtbl_Opportunity_Mst.OppName2;
            txtoppname3.Text = objtbl_Opportunity_Mst.OppName3;

            //txtTeamLeader.Text = objtbl_Opportunity_Mst.TeamLeader;
        }


        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Opportunity_Mst.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Opportunity_Mst.OrderBy(m => m.OpportID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        #region only for Contact Textbox

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(ViewState["compId"]);
            Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CID);
            // btnAdd.Text = "Update";
            TxtContact.Text = objtbl_CONTACT.ContactMyID.ToString();

            //   txtEmail.Text = objtbl_CONTACT.EMAIL1.ToString();
            //txtMobileNo.Text = objtbl_CONTACT.MOBPHONE.ToString();
            //drpItManager.SelectedValue = objtbl_CONTACT.ITMANAGER.ToString();
            //txtAddress.Text = objtbl_CONTACT.ADDR1.ToString();
            //txtAddress2.Text = objtbl_CONTACT.ADDR2.ToString();
            //txtCity.Text = objtbl_CONTACT.CITY.ToString();
            // int COID = Convert.ToInt32(objtbl_CONTACT.COUNTRYID);
            //drpCustomer_id.SelectedValue = DB.CRM_tblCOUNTRY.Single(p => p.COUNTRYID == COID).ISO3166_1_2LetterCode;
            //bindState();
            //drpState.SelectedValue = objtbl_CONTACT.STATE.ToString();
            //txtPostalCode.Text = objtbl_CONTACT.POSTALCODE.ToString();
            //txtZipCode.Text = objtbl_CONTACT.ZIPCODE.ToString();
            //txtPostalCode.Text = objtbl_CONTACT.POSTALCODE.ToString();
            //drpMyCounLocID.SelectedValue = objtbl_CONTACT.MYCONLOCID.ToString();
            //tags_2.Text = objtbl_CONTACT.EmailId.ToString();
            //tags_3.Text = objtbl_CONTACT.FaxID.ToString();
            //tags_4.Text = objtbl_CONTACT.BUSPHONE1.ToString();
            //txtRemark.Text = objtbl_CONTACT.REMARKS.ToString();
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {

        }
        #endregion

        //protected void btnsavetask_Click(object sender, EventArgs e)
        //{
        //    Database.tbl_Task objtbl_Task = new Database.tbl_Task();
        //    //Server Content Send data Yogesh
        //    //objtbl_Task.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
        //    objtbl_Task.TaskID = DB.CRM_tbl_Task.Count() > 0 ? DB.CRM_tbl_Task.Max(p => p.TaskID) + 1 : 1;
        //    //objtbl_Task.CerticateNo = Convert.ToInt32(drpCerticateNo.SelectedValue);
        //    //objtbl_Task.ForEmp_ID = Convert.ToInt32(drpForEmp_ID.SelectedValue);
        //  //  objtbl_Task.ActivityId = //drpactivity.SelectedValue;
        //    objtbl_Task.ProjectId = drpproject.SelectedValue;
        //    objtbl_Task.PerformingEmp_ID = Convert.ToInt32(drptofield.SelectedValue);
        //    objtbl_Task.Subject = txtSubject.Text;
        //    objtbl_Task.StartingDate = Convert.ToDateTime(txtStartingDate.Text);
        //    objtbl_Task.TanentId = 1;
        //    objtbl_Task.LocationID = 1;
        //    objtbl_Task.CerticateNo = 1;
        //    objtbl_Task.TaskType = drptasktype.SelectedValue;
        //    objtbl_Task.TaskStatus = drpTaskStatus.SelectedValue;
        //    objtbl_Task.DueDate = Convert.ToDateTime(txtDueDate.Text);
        //    objtbl_Task.Priority = DrpPriorityTask.SelectedValue;
        //    //objtbl_Task.ActualCompletionDate = Convert.ToDateTime(txtActualCompletionDate.Text);
        //    objtbl_Task.CompletedPerctange = Convert.ToInt32(drpCompletedPerctange.SelectedValue);
        //    //objtbl_Task.ReminderDate = Convert.ToDateTime(txtReminderDate.Text);
        //    // objtbl_Task.ReminderTime = Convert.ToDateTime(txtReminderTime.SelectedValue);
        //    //objtbl_Task.Categories = txtCategories.Text;
        //    //objtbl_Task.FollowUp = txtFollowUp.Text;
        //    //objtbl_Task.CustomFollowUpStartDate = Convert.ToDateTime(txtCustomFollowUpStartDate.Text);
        //    //objtbl_Task.CustomFollowUpEndDate = Convert.ToDateTime(txtCustomFollowUpEndDate.Text);
        //    //objtbl_Task.CustomFollowUpReminderDate = Convert.ToDateTime(txtCustomFollowUpReminderDate.Text);
        //    //objtbl_Task.ForwardToEmp_ID = Convert.ToInt32(drpForwardToEmp_ID.SelectedValue);
        //    //objtbl_Task.Occurance_ID = Convert.ToInt32(drpOccurance_ID.SelectedValue);
        //    objtbl_Task.Remarks = txtReminder.Text;
        //    objtbl_Task.Active = true;
        //    //objtbl_Task.CruipID = txtCruipID.Text;


        //    DB.CRM_tbl_Task.AddObject(objtbl_Task);
        //    DB.SaveChanges();
        //    //GetFiles();
        //  //  BindFile();
        //    Clear();
        //    lblMsg.Text = "  Data Save Successfully";
        //    pnlSuccessMsg.Visible = true;
        //    //BindData();
        //}

        public void BindData1()
        {
            Listview1.DataSource = DB.tbl_Task.Where(p => p.Active == true && p.TenentID == TID);
            Listview1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        public void BindReference()
        {
            //drpRefNo123.DataSource = DB.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "Oppertunity" && P.TenentID == TID && P.SWITCH2 == "04").OrderBy(a => a.REFNAME1);
            //drpRefNo123.DataTextField = "REFNAME1";
            //drpRefNo123.DataValueField = "REFID";
            //drpRefNo123.DataBind();
            //drpRefNo123.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpRefNo123.DataSource = DB1.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "Oppertunity" && P.TenentID == TID && P.SWITCH2 == "04").OrderBy(a => a.REFNAME1);
            //drpRefNo123.DataTextField = "REFNAME1";
            //drpRefNo123.DataValueField = "REFID";
            //drpRefNo123.DataBind();
            //drpRefNo123.Items.Insert(0, new ListItem("-- Select --", "0"));

            // Classes.CRMClass.getcrmdropdown(drpRefNo123, TID, "CRM", "Oppertunity", "04", "REFTABLE");
            //Classes.CRMClass.getCRMdropdown(drpRefNo123, TID, "REFTABLE");
        }
        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    var exist = DB1.REFTABLE.Where(c => c.REFNAME1 == txtAddReference3.Text && c.REFSUBTYPE == "Oppertunity");
        //    if (exist.Count() <= 0)
        //    {
        //        Database.REFTABLE objtbl_REFTABLE = new Database.REFTABLE();
        //        int RefID = DB1.REFTABLE.Count() > 0 ? Convert.ToInt32(DB1.REFTABLE.Max(p => p.REFID) + 1) : 1;
        //        objtbl_REFTABLE.TenentID = TID;
        //        objtbl_REFTABLE.REFID = RefID;//DB.REFTABLE.Count() > 0 ? Convert.ToInt32(DB.REFTABLE.Max(p => p.REFID) + 1) : 1;
        //        objtbl_REFTABLE.REFSUBTYPE = "Oppertunity";
        //        objtbl_REFTABLE.REFTYPE = "CRM";
        //        objtbl_REFTABLE.SHORTNAME = txtShortName1.Text;
        //        objtbl_REFTABLE.REFNAME1 = txtAddReference3.Text;
        //        objtbl_REFTABLE.REFNAME2 = txtAddReference3.Text;
        //        objtbl_REFTABLE.REFNAME3 = txtAddReference3.Text;
        //        objtbl_REFTABLE.Remarks = txtREMARK2.Text;
        //        objtbl_REFTABLE.ACTIVE = "Y";
        //if (Convert.ToInt32(drpRefNo123.SelectedValue)!=0)
        //{
        //int ID = Convert.ToInt32(drpRefNo123.SelectedValue);//Convert.ToInt32(ViewState["CampaignID"]);
        // objtbl_REFTABLE.SWITCH3 = "220003Camp" + ID;
        // }
        //else
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "User Name Is Duplicate!", "alert('Must Enter Ref Name');", true);
        //    //objtbl_REFTABLE.SWITCH3 = "220003OPP";
        //}
        //        objtbl_REFTABLE.SWITCH2 = "04";
        //        objtbl_REFTABLE.CRUP_ID = 1;//((DMSMaster)Page.Master).WriteLog("From New Document for Reference,ID:" + objtbl_REFTABLE.REFID.ToString(), "From New Document for Reference,ID:" + objtbl_REFTABLE.REFID.ToString(), "REFTABLE", "");
        //        DB1.REFTABLE.AddObject(objtbl_REFTABLE);
        //        DB1.SaveChanges();
        //        ViewState["RefID"] = RefID;
        //        BindReference();
        //        ViewState["MyStatus"] = "New";
        //    }
        //    else
        //    {
        //        string display = "REFERENCE Is Duplicate!";
        //        ClientScript.RegisterStartupScript(this.GetType(), "User Name Is Duplicate!", "alert('" + display + "');", true);
        //        return;
        //    }
        //}

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        public void InsertCRMMainActivity(int TransNo, string ADDACTIvity, string CampinName, string Discrption)
        {
            string strStatus = ViewState["MyStatus"].ToString();
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
            int TenentID = TID;
            int LocationID = LID;
            int USERCODE = UID;
            int COMPID = 99999999;
            int LinkMasterCODE = 1;
            int ID = TransNo;
            int RouteID = 1;
            string ACTIVITYE = "Opportunity";
            string ACTIVITYA = "Opportunity";
            string ACTIVITYA2 = "CRM";
            bool AMIGLOBAL = true;
            bool MYPERSONNEL = true;
            int INTERVALDAYS = 3;
            bool REPEATFOREVER = true;
            DateTime REPEATTILL = DateTime.Now;
            string REMINDERNOTE = "Youer Opportunity is Pading";
            int ESTCOST = 1;
            int GROUPCODE = 1;
            string USERCODEENTERED = "New";
            DateTime UPDTTIME = DateTime.Now;
            DateTime UploadDate = DateTime.Now;
            string USERNAME = UNAME;
            string Remarks = "Intert MainActivity Data";
            int CRUP_ID = 1;
            string Version1 = UNAME;
            int Type = 1;
            string MyStatus = strStatus;
            int MainID = 1;
            int ModuleID = 1;
            string RecodType = ADDACTIvity;
            string URL = Request.Url.AbsolutePath;
            string CampynName = CampinName;
            string CampynDescription = Discrption;
            int REFNUMber = Classes.ACMClass.InsertACM_CRMMainActivities(TenentID, COMPID, LinkMasterCODE, LocationID, ID, RouteID, USERCODE, ACTIVITYE, ACTIVITYA, ACTIVITYA2, AMIGLOBAL, MYPERSONNEL, INTERVALDAYS, REPEATFOREVER, REPEATTILL, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, Remarks, CRUP_ID, Version1, Type, MyStatus, MainID, ModuleID, URL, CampynName, CampynDescription);
            //Eco_ICTR_HD obj = DB.Eco_ICTR_HD.Single(p => p.MYTRANSID == TransNo);
            //obj.CrmActivityRefNo = REFNUMber;
            int SID = 1240103;
            string BName = "Create Lead";
            string P2 = "aa";
            string P3 = "aa";
            inserCrmproghw(TID, SID, BName, P2, P3, TransNo);

        }

        public void inserCrmproghw(int TID, int SID, string BName, string P2, string P3, int TRction)
        {
            // ACM_CRMMainActivities obj = DB1.ACM_CRMMainActivities.Single(p => p.TenentID == TID  && p.ID == TRction);
            int ACID = Convert.ToInt32(DB.CRMActivities.Where(p=>p.TenentID==TID).Max(p => p.MyLineNo));
            int TenentID = TID;
            int ActivityID = ACID;
            int StatusID = SID;
            string ButtionName = BName;
            bool Allowed = true;
            string Parameter2 = P2;
            string Parameter3 = P3;
            bool Active = true;
            DateTime Datetime = DateTime.Now;
            int Crup_Id = 999999999;
            Classes.ACMClass.InsertDataCRMProgHW(TenentID, ActivityID, StatusID, ButtionName, Allowed, Parameter2, Parameter3, Active, Datetime, Crup_Id);
        }
        //public void CRMMainActivity(int TID, int Reference, int CAMPID)
        //{
        //    string strStatus = ViewState["MyStatus"].ToString();
        //    int REFIDFromNewDOC1 = REFIDFromNewDOC1 = Convert.ToInt32(Reference);
        //    //  string strRouteID = CommonList[5].ToString();
        //    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
        //    int UID1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
        //    Database.ACM_CRMMainActivities objtbl_CRMMainActivity = new Database.ACM_CRMMainActivities();
        //    objtbl_CRMMainActivity.MasterCODE = DB1.ACM_CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB1.ACM_CRMMainActivities.Max(p => p.MasterCODE) + 1) : 1;
        //    objtbl_CRMMainActivity.TenentID = TID;
        //    objtbl_CRMMainActivity.COMPID = Convert.ToInt32(1);
        //    objtbl_CRMMainActivity.USERCODE = UID1;//current user ID Done
        //    objtbl_CRMMainActivity.ACTIVITYE = "";
        //    objtbl_CRMMainActivity.ACTIVITYA = "";
        //    objtbl_CRMMainActivity.ACTIVITYA2 = "";
        //    objtbl_CRMMainActivity.Reference = Reference.ToString();
        //    objtbl_CRMMainActivity.AMIGLOBAL = false;
        //    objtbl_CRMMainActivity.MYPERSONNEL = false;
        //    objtbl_CRMMainActivity.INTERVALDAYS = 1;
        //    objtbl_CRMMainActivity.REPEATFOREVER = false;
        //    objtbl_CRMMainActivity.REPEATTILL = DateTime.Now;
        //    objtbl_CRMMainActivity.REMINDERNOTE = "";
        //    objtbl_CRMMainActivity.ESTCOST = 1;
        //    objtbl_CRMMainActivity.GROUPCODE = 1;//come from ACM
        //    objtbl_CRMMainActivity.USERCODEENTERED = "";
        //    objtbl_CRMMainActivity.UPDTTIME = DateTime.Now;
        //    objtbl_CRMMainActivity.USERNAME = UID;//current user name Done
        //    objtbl_CRMMainActivity.Remarks = "";
        //    objtbl_CRMMainActivity.MainID = CAMPID;
        //    //objtbl_CRMMainActivity.CRUP_ID = ((DMSMaster)Page.Master).WriteLog("From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), "From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), "CRMMainActivity", UID);
        //    objtbl_CRMMainActivity.MyStatus = strStatus;
        //    DB1.ACM_CRMMainActivities.AddObject(objtbl_CRMMainActivity);
        //    // ((DMSMaster)Page.Master).UpdateLog("From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), objtbl_CRMMainActivity.CRUP_ID,"CRMMainActivity", UID);
        //    DB1.SaveChanges();

        //}
        //public void CRMActivity(int TID, int Reference)//loop rout
        //{
        //    string strStatus = "";
        //    if (ViewState["MyStatus"] != null)
        //    {
        //        strStatus = ViewState["MyStatus"].ToString();
        //    }

        //    //int REFIDFromNewDOC = 0;
        //    string Type = Convert.ToString(ActionMaster.Type.Campaign);
        //    Database.CRMActivity objtbl_CRMActivity = new Database.CRMActivity();
        //    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
        //    int refID = Convert.ToInt32(Reference);// Convert.ToInt32(CommonList[4]);
        //    //string RefName = DB.REFTABLE.Single(p => p.REFID == refID).REFNAME1.ToString();


        //    //objtbl_CRMActivity.TenentID = TID;
        //    objtbl_CRMActivity.COMPID = 0;

        //    objtbl_CRMActivity.TenentID = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.TenentID) + 1) : 1;
        //    //objtbl_CRMActivity.ActivityCompID = item.CompIDSEqNo;
        //    //objtbl_CRMActivity.DocID = DocID;

        //    objtbl_CRMActivity.ACTIVITYTYPE = "CRM";
        //    objtbl_CRMActivity.REFTYPE = Convert.ToString(ActionMaster.Type.Lead);
        //    objtbl_CRMActivity.REFSUBTYPE = Convert.ToString(ActionMaster.Type.Lead);
        //    objtbl_CRMActivity.RouteID = "";
        //    objtbl_CRMActivity.NextUser = "1";
        //    objtbl_CRMActivity.AMIGLOBAL = "Y";
        //    objtbl_CRMActivity.MYPERSONNEL = "N";
        //    objtbl_CRMActivity.ActivityPerform = "";
        //    objtbl_CRMActivity.REMINDERNOTE = " ";
        //    //int a = item.REFID;
        //    //int b = item.LineNum;
        //    //int newNumber = int.Parse(a.ToString() + b.ToString());
        //    //objtbl_CRMActivity.ESTCOST = newNumber;//Seq # Done
        //    //  objtbl_CRMActivity.ESTCOST = item.SeqNo;
        //    objtbl_CRMActivity.GROUPCODE = "1";
        //    objtbl_CRMActivity.USERCODEENTERED = UID.ToString();
        //    objtbl_CRMActivity.InitialDate = DateTime.Now;
        //    objtbl_CRMActivity.UPDTTIME = DateTime.Now;
        //    objtbl_CRMActivity.USERNAME = UID.ToString();

        //    objtbl_CRMActivity.PerfReferenceNo = "test1";
        //    objtbl_CRMActivity.NextRefNo = "test2";


        //    objtbl_CRMActivity.MyStatus = strStatus;
        //    DateTime CDate = DateTime.Now;
        //    // int NoOFDay = item.Days2Complete;
        //    //   CDate = CDate.AddDays(NoOFDay);
        //    objtbl_CRMActivity.DeadLineDate = CDate;

        //    //objtbl_CRMActivity.Active = item.ActiveCRM;

        //    //objtbl_CRMActivity.Active = "F";

        //    //  objtbl_CRMActivity.GroupBy = item.REFID.ToString() + RouteName + item.SeqNo + refID;
        //    objtbl_CRMActivity.Type = 1;
        //    // objtbl_CRMActivity.FromColumn = Convert.ToInt32(item.FromColumn);
        //    // objtbl_CRMActivity.ToColumn = Convert.ToInt32(item.ToColumn);
        //    DB.CRMActivities.AddObject(objtbl_CRMActivity);
        //    //DB.CRMActivities.AddObject(objtbl_CRMActivity);
        //    DB.SaveChanges();
        //    BindReference();

        //    //foreach (DMSRouteMST item in List123.ToList())
        //    //{
        //    //    Database.tbl_ISSREC_Mst objtbl_ISSREC_Mst = new Database.tbl_ISSREC_Mst();

        //    //    objtbl_ISSREC_Mst.TenentID = TID;
        //    //    objtbl_ISSREC_Mst.COMPID = item.CompIDSEqNo;
        //    //    objtbl_ISSREC_Mst.DOCID = DocID;
        //    //    objtbl_ISSREC_Mst.USER_ID = item.UserIDSubmitBy;
        //    //    objtbl_ISSREC_Mst.REFERENCENO = refID;
        //    //    objtbl_ISSREC_Mst.SEQ = item.SeqNo;
        //    //    objtbl_ISSREC_Mst.DMS_USER_ID = Convert.ToInt32(item.UserIDSignBySubmit);
        //    //    objtbl_ISSREC_Mst.DATE = DateTime.Now;
        //    //    objtbl_ISSREC_Mst.DOCNO = item.REFID;
        //    //    objtbl_ISSREC_Mst.ACTIVE = false;
        //    //    objtbl_ISSREC_Mst.FromColumn = Convert.ToInt32(item.FromColumn);
        //    //    objtbl_ISSREC_Mst.ToColumn = Convert.ToInt32(item.ToColumn);
        //    //    if (item.SeqNo == 1)
        //    //    {
        //    //        objtbl_ISSREC_Mst.ActiveCRM = "Y";
        //    //    }

        //    //    objtbl_ISSREC_Mst.TYPE = "SubmitttedBy";
        //    //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_Mst);
        //    //    //Recevedby entry
        //    //    Database.tbl_ISSREC_Mst objtbl_ISSREC_Mst1 = new Database.tbl_ISSREC_Mst();
        //    //    objtbl_ISSREC_Mst1.TenentID = TID;
        //    //    objtbl_ISSREC_Mst1.COMPID = Convert.ToInt32(item.CompIDRecBy);
        //    //    objtbl_ISSREC_Mst1.DOCID = DocID;
        //    //    objtbl_ISSREC_Mst1.USER_ID = Convert.ToInt32(item.UserIDReceiveBy);
        //    //    objtbl_ISSREC_Mst1.DMS_USER_ID = Convert.ToInt32(item.UserIDSignByReceive);
        //    //    objtbl_ISSREC_Mst1.REFERENCENO = refID;
        //    //    objtbl_ISSREC_Mst1.SEQ = item.SeqNo;
        //    //    objtbl_ISSREC_Mst1.DATE = DateTime.Now;
        //    //    objtbl_ISSREC_Mst1.ACTIVE = false;
        //    //    objtbl_ISSREC_Mst1.DOCNO = item.REFID;
        //    //    objtbl_ISSREC_Mst1.FromColumn = Convert.ToInt32(item.FromColumn);
        //    //    objtbl_ISSREC_Mst1.ToColumn = Convert.ToInt32(item.ToColumn);
        //    //    if (item.SeqNo == 1)
        //    //    {
        //    //        objtbl_ISSREC_Mst1.ActiveCRM = "Y";
        //    //    }
        //    //    objtbl_ISSREC_Mst1.TYPE = "ReceivedBy";
        //    //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_Mst1);

        //    //    //submit sign
        //    //    Database.tbl_ISSREC_Mst objtbl_ISSREC_MstSubmitSign = new Database.tbl_ISSREC_Mst();
        //    //    objtbl_ISSREC_MstSubmitSign.TenentID = TID;
        //    //    objtbl_ISSREC_MstSubmitSign.COMPID = Convert.ToInt32(item.CompIDSEqNo);
        //    //    objtbl_ISSREC_MstSubmitSign.DOCID = DocID;
        //    //    objtbl_ISSREC_MstSubmitSign.USER_ID = Convert.ToInt32(item.UserIDSignBySubmit);
        //    //    objtbl_ISSREC_MstSubmitSign.DMS_USER_ID = Convert.ToInt32(item.UserIDSignBySubmit);
        //    //    objtbl_ISSREC_MstSubmitSign.REFERENCENO = refID;
        //    //    objtbl_ISSREC_MstSubmitSign.SEQ = item.SeqNo;
        //    //    objtbl_ISSREC_MstSubmitSign.DATE = DateTime.Now;
        //    //    objtbl_ISSREC_MstSubmitSign.ACTIVE = false;
        //    //    objtbl_ISSREC_MstSubmitSign.DOCNO = item.REFID;
        //    //    objtbl_ISSREC_MstSubmitSign.FromColumn = Convert.ToInt32(item.FromColumn);
        //    //    objtbl_ISSREC_MstSubmitSign.ToColumn = Convert.ToInt32(item.ToColumn);
        //    //    if (item.SeqNo == 1)
        //    //    {
        //    //        objtbl_ISSREC_MstSubmitSign.ActiveCRM = "Y";
        //    //    }
        //    //    objtbl_ISSREC_MstSubmitSign.TYPE = "SubmitSign";
        //    //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_MstSubmitSign);


        //    //    //Received Sign
        //    //    Database.tbl_ISSREC_Mst objtbl_ISSREC_MstSReceivedSign = new Database.tbl_ISSREC_Mst();
        //    //    objtbl_ISSREC_MstSReceivedSign.TenentID = TID;
        //    //    objtbl_ISSREC_MstSReceivedSign.COMPID = Convert.ToInt32(item.CompIDRecBy);
        //    //    objtbl_ISSREC_MstSReceivedSign.DOCID = DocID;
        //    //    objtbl_ISSREC_MstSReceivedSign.USER_ID = Convert.ToInt32(item.UserIDSignByReceive);
        //    //    objtbl_ISSREC_MstSReceivedSign.DMS_USER_ID = Convert.ToInt32(item.UserIDSignByReceive);
        //    //    objtbl_ISSREC_MstSReceivedSign.REFERENCENO = refID;
        //    //    objtbl_ISSREC_MstSReceivedSign.SEQ = item.SeqNo;
        //    //    objtbl_ISSREC_MstSReceivedSign.DATE = DateTime.Now;
        //    //    objtbl_ISSREC_MstSReceivedSign.ACTIVE = false;
        //    //    objtbl_ISSREC_MstSReceivedSign.DOCNO = item.REFID;
        //    //    objtbl_ISSREC_MstSReceivedSign.FromColumn = Convert.ToInt32(item.FromColumn);
        //    //    objtbl_ISSREC_MstSReceivedSign.ToColumn = Convert.ToInt32(item.ToColumn);
        //    //    if (item.SeqNo == 1)
        //    //    {
        //    //        objtbl_ISSREC_MstSReceivedSign.ActiveCRM = "Y";
        //    //    }
        //    //    objtbl_ISSREC_MstSReceivedSign.TYPE = "ReceivedSign";
        //    //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_MstSReceivedSign);
        //    //    DB.SaveChanges();
        //    //}



        //    //foreach (DMSRouteMST item in List123.ToList())
        //    //{
        //    //    Database.CRMActivity objtbl_CRMActivity = new Database.CRMActivity();
        //    //    objtbl_CRMActivity.TenentID = TID;
        //    //    objtbl_CRMActivity.COMPID = item.CompIDSEqNo;
        //    //    objtbl_CRMActivity.ActivityCompID = item.CompIDSEqNo;
        //    //    objtbl_CRMActivity.DocID = DocID;
        //    //    objtbl_CRMActivity.ACTIVITYCODE = item.ACTIVITYCODE.ToString();
        //    //    objtbl_CRMActivity.ACTIVITYTYPE = "DMS";
        //    //    objtbl_CRMActivity.REFTYPE = item.REFTYPE;
        //    //    objtbl_CRMActivity.REFSUBTYPE = item.REFSUBTYPE;
        //    //    objtbl_CRMActivity.RouteID = RouteName;
        //    //    // objtbl_CRMActivity.MyLineNo = item.LineNum;
        //    //    objtbl_CRMActivity.USERCODE = " ";
        //    //    objtbl_CRMActivity.ReferenceNo = CommonList[4];//ref # from New Doc Done
        //    //    objtbl_CRMActivity.EarlierRefNo = RefName;
        //    //    objtbl_CRMActivity.NextUser = "1";

        //    //    objtbl_CRMActivity.NextRefNo = strDocName;//= DOCUMENT NAME DONE
        //    //    objtbl_CRMActivity.AMIGLOBAL = "Y";
        //    //    objtbl_CRMActivity.MYPERSONNEL = "N";
        //    //    objtbl_CRMActivity.ActivityPerform = item.CompIDSEqNo.ToString();
        //    //    objtbl_CRMActivity.REMINDERNOTE = " ";
        //    //    //int a = item.REFID;
        //    //    //int b = item.LineNum;
        //    //    //int newNumber = int.Parse(a.ToString() + b.ToString());
        //    //    //objtbl_CRMActivity.ESTCOST = newNumber;//Seq # Done
        //    //    objtbl_CRMActivity.ESTCOST = item.SeqNo;
        //    //    objtbl_CRMActivity.GROUPCODE = "1";
        //    //    objtbl_CRMActivity.USERCODEENTERED = UID.ToString();
        //    //    objtbl_CRMActivity.InitialDate = DateTime.Now;
        //    //    objtbl_CRMActivity.UPDTTIME = DateTime.Now;
        //    //    objtbl_CRMActivity.USERNAME = UID.ToString();

        //    //    objtbl_CRMActivity.MyStatus = strStatus;
        //    //    DateTime CDate = DateTime.Now;
        //    //    int NoOFDay = item.Days2Complete;
        //    //    CDate = CDate.AddDays(NoOFDay);
        //    //    objtbl_CRMActivity.DeadLineDate = CDate;

        //    //    objtbl_CRMActivity.Active = item.ActiveCRM;

        //    //    //objtbl_CRMActivity.Active = "F";

        //    //    objtbl_CRMActivity.GroupBy = item.REFID.ToString() + RouteName + item.SeqNo + refID;
        //    //    objtbl_CRMActivity.Type = 1;
        //    //    objtbl_CRMActivity.FromColumn = Convert.ToInt32(item.FromColumn);
        //    //    objtbl_CRMActivity.ToColumn = Convert.ToInt32(item.ToColumn);
        //    //    DB.CRMActivities.AddObject(objtbl_CRMActivity);
        //    //    DB.SaveChanges();
        //    //}
        //}


        public string getNameCompny(int CID)
        {
            if (CID != 0)
            {
                return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == CID && p.TenentID == TID).COMPNAME1;
            }
            else
            {
                return "";
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            string URL = Session["ADMInPrevious"].ToString();
            Response.Redirect(URL);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpSearchTitle.SelectedValue) != 0)
            {
                int GrupID = Convert.ToInt32(DrpQuestion.SelectedValue);
                int TitleID = Convert.ToInt32(DrpSearchTitle.SelectedValue);
                int CamID = Convert.ToInt32(Session["CampaignID"]);
                Session["LesdInset"] = 1254;
                Response.Redirect("SearchTitalPage.aspx?TID=" + TitleID + "&CampID=" + CamID + "&LeadName=" + txtName.Text + "&GrupID=" + GrupID);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Select Title ...');", true);
            }
        }
        protected void drpTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(drpTeamName.SelectedValue);
            BindTimeAssin(TID);

        }
        public void BindTimeAssin(int id)
        {
            drpassingTeamLeader.DataSource = DB.TeamNames.Where(p => p.TenentID == TID && p.REF_ID == id);
            drpassingTeamLeader.DataValueField = "TeamID";
            drpassingTeamLeader.DataTextField = "MemberName";
            drpassingTeamLeader.DataBind();
            drpassingTeamLeader.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        //protected void btnserchproduct_Click(object sender, EventArgs e)
        //{
        //    string id1 = txtsercustomer.Text;
        //    var list1 = DB.TBLCOMPANYSETUPs.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME3.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.COMPID).ToList().Take(50);
        //    drpcompniyname.DataSource = list1;
        //    drpcompniyname.DataTextField = "COMPNAME1";
        //    drpcompniyname.DataValueField = "COMPNAME1";
        //    drpcompniyname.DataBind();
        //    lblcompnyname.Text = "Select Customer";
        //    lblheder.Text = "Customer List";
        //    ModalPopupExtender1.Show();
        //    ViewState["Custmor"] = 1;
        //}

        protected void btnselect_Click(object sender, EventArgs e)
        {
            string Name = drpcompniyname.SelectedItem.Text;

            if (ViewState["Custmor"] != null)
            {
                txtsercustomer.Text = Name;
                ViewState["Custmor"] = null;
            }
            else if (ViewState["Supplery"] != null)
            {
                txtsupplier.Text = Name;
                ViewState["Supplery"] = null;
            }

        }

        //protected void btnserchsupplery_Click(object sender, EventArgs e)
        //{
        //    string id1 = txtsupplier.Text;
        //    var list1 = DB.TBLCOMPANYSETUPs.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME3.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.COMPID).ToList().Take(50);
        //    drpcompniyname.DataSource = list1;
        //    drpcompniyname.DataTextField = "COMPNAME1";
        //    drpcompniyname.DataValueField = "COMPNAME1";
        //    drpcompniyname.DataBind();
        //    lblcompnyname.Text = "Select Supplier";
        //    lblheder.Text = "Supplier List";
        //    ModalPopupExtender1.Show();
        //    ViewState["Supplery"] = 1;
        //}
    }
}