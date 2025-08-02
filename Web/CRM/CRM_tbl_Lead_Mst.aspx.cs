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
    public partial class CRM_tbl_Lead_Mst : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        bool flag = false;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        // Database.CallEMEntities DB = new Database.CallEMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (flag == false)
            {
                flag = true;
                Session["PageType"] = null;
                // Session["CampaignID"] = null;
            }
            Session["PageType"] = ActionMaster.Type.Lead.ToString();
            pnlSuccessMsg.Visible = false;
            if (!IsPostBack)
            {
                FistTimeLoad();
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                BindReference();
                //Session["CampaignID"] = null;
                Session["PageType"] = ActionMaster.Type.Lead.ToString();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                LastData();
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                Session["PageType"] = ActionMaster.Type.Lead.ToString();
                //lblLeadOwner1.Text = strUName;
                //string strRoleName = "";
                //if (strUName == "Ayo")
                //    strRoleName = DB.ERP_WEB_GEN_ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_NAME == "Super Admin").ROLE_NAME;
                //else
                //    strRoleName = DB.ERP_WEB_GEN_ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_NAME == "Simple User").ROLE_NAME;

                //lblRole.Text = "Role : " + strRoleName.ToString();

                string UID = (((USER_MST)Session["USER"]).USER_ID).ToString();
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID == UID).Count() > 0)
                {
                    string strCompanyName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.USERID == UID).COMPNAME;

                }

                if (Request.QueryString["View"] == "Add")
                {
                    Write();
                    Clear();
                    btnAdd.Text = "Add";
                    pnllist.Visible = false;
                    //paneloppref.Visible = true;
                    DivMain.Attributes["style"] = "display: none;";
                }
                if (btnAdd.Text == "Add")
                {
                    btnAdd.ValidationGroup = "s";
                    DivMain.Attributes["style"] = "display: none;";
                }
                //  txtFrom.Text = (((USER_MST)Session["USER"]).EmailAddress);



                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                    DivMain.Attributes["style"] = "display: block;";
                    BindCommand(ID);
                    Write();
                    btnAdd.Text = "Update";

                    pnlSearchbutton.Visible = false;
                    ViewState["Edit"] = ID;
                    btnAdd.ValidationGroup = "submit";



                    //Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                    //DivMain.Attributes["style"] = "display: none;";

                    //ViewState["Edit"] = ID;
                    //Database.tbl_Lead_Mst objtbl_Lead_Mst = DB.tbl_Lead_Mst.Single(p => p.ID == ID);
                    ////   drpID.SelectedValue = objtbl_Lead_Mst.ID.ToString();
                    ////if(objtbl_Lead_Mst.ID !=null || Convert.ToInt32(objtbl_Lead_Mst.ID)!=0)
                    ////{

                    ////}
                    ////drpConact_id.SelectedValue = objtbl_Lead_Mst.Conact_id.ToString();
                    //if (objtbl_Lead_Mst.Customer_id != null)
                    //{
                    //    // drpCustomer_id.SelectedValue = objtbl_Lead_Mst.Customer_id.ToString();
                    //}
                    //if (objtbl_Lead_Mst.Supplier_id != null)
                    //{
                    //    // drpSupplier_id.SelectedValue = objtbl_Lead_Mst.Supplier_id.ToString();
                    //}
                    //if (objtbl_Lead_Mst.Project_id != null)
                    //{
                    //    // drpProject_id.SelectedValue = objtbl_Lead_Mst.Project_id.ToString();
                    //}

                    //if (objtbl_Lead_Mst.Salutation != null)
                    //{
                    //    DrpSalutation.SelectedValue = objtbl_Lead_Mst.Salutation.ToString();
                    //}
                    ////txtSalutation.Text = objtbl_Lead_Mst.Salutation.ToString();
                    //txtTitle.Text = objtbl_Lead_Mst.Title.ToString();
                    //txtTwitterScreenName.Text = objtbl_Lead_Mst.TwitterScreenName.ToString();
                    //txtRefered_by.Text = objtbl_Lead_Mst.Refered_by.ToString();
                    //if (objtbl_Lead_Mst.Company_ID != null)
                    //{
                    //    DDLComapny.SelectedValue = objtbl_Lead_Mst.Company_ID.ToString();
                    //}
                    //if (objtbl_Lead_Mst.lead_source != null)
                    //{
                    //    DDLLeadSource.SelectedValue = objtbl_Lead_Mst.lead_source.ToString();
                    //}
                    //if (objtbl_Lead_Mst.Oppertunity_ID != null)
                    //{
                    //    DrpOppertunityName.SelectedValue = objtbl_Lead_Mst.Oppertunity_ID.ToString();
                    //}
                    //if (objtbl_Lead_Mst.Campaign_ID != null)
                    //{
                    //    Drpcampaign_name.SelectedValue = objtbl_Lead_Mst.Campaign_ID.ToString();
                    //}
                    //if (objtbl_Lead_Mst.RefNO != null)
                    //{
                    //    drpRefNo123.SelectedValue = objtbl_Lead_Mst.RefNO.ToString();
                    //}
                    //if (objtbl_Lead_Mst.QuestionGroup != null)
                    //{
                    //    DrpQuestion.SelectedValue = objtbl_Lead_Mst.QuestionGroup.ToString();
                    //}
                    //if (objtbl_Lead_Mst.SearchTitle != null)
                    //{
                    //    DrpSearchTitle.SelectedValue = objtbl_Lead_Mst.SearchTitle.ToString();
                    //}

                    //// txtlead_source.Text = objtbl_Lead_Mst.lead_source.ToString();
                    ////  txtStatus.Text = objtbl_Lead_Mst.Status.ToString();
                    //// txtDepartment.Text = objtbl_Lead_Mst.Department.ToString();
                    ////cbDo_not_call.Checked = (objtbl_Lead_Mst.Do_not_call == true) ? true : false;
                    //txtAssistant.Text = objtbl_Lead_Mst.Assistant.ToString();
                    //txtAssistant_phone.Text = objtbl_Lead_Mst.Assistant_phone.ToString();
                    //cbemail_opt_out.Checked = (objtbl_Lead_Mst.email_opt_out == true) ? true : false;
                    ////  cbinvalid_email.Checked = (objtbl_Lead_Mst.invalid_email == true) ? true : false;
                    //txtaccount_name.Text = objtbl_Lead_Mst.account_name.ToString();
                    //// txtopportunity_name.Text = objtbl_Lead_Mst.opportunity_name.ToString();
                    //txtopportunity_amount.Text = objtbl_Lead_Mst.opportunity_amount.ToString();
                    //// txtcampaign_name.Text = objtbl_Lead_Mst.campaign_name.ToString();
                    //txtdate_entered.Text = objtbl_Lead_Mst.date_entered.ToString();
                    //// txtdate_modified.Text = objtbl_Lead_Mst.date_modified.ToString();
                    //txtstatus_description.Text = objtbl_Lead_Mst.status_description.ToString();
                    //txtdescription.Text = objtbl_Lead_Mst.description.ToString();
                    //txtaccount_description.Text = objtbl_Lead_Mst.account_description.ToString();
                    ////  drpteam_name.SelectedValue = objtbl_Lead_Mst.team_name.ToString();
                    //txtlead_source_description.Text = objtbl_Lead_Mst.lead_source_description.ToString();
                    //// txtassigned_to_name.Text = objtbl_Lead_Mst.assigned_to_name.ToString();
                    ////  drpcreated_by.SelectedValue = objtbl_Lead_Mst.created_by.ToString();
                    //// drpmodified_by.SelectedValue = objtbl_Lead_Mst.modified_by.ToString();
                    ////cbActive.Checked = (objtbl_Lead_Mst.Active == true) ? true : false;
                    ////cbDeleted.Checked = (objtbl_Lead_Mst.Deleted == true) ? true : false;
                    //// txtWebsite.Text = objtbl_Lead_Mst.Website.ToString();
                    //cbSMS_Opt_In.Checked = (objtbl_Lead_Mst.SMS_Opt_In == true) ? true : false;
                    //txtCRUP_ID.Text = objtbl_Lead_Mst.CRUP_ID.ToString();
                    //txtNoofEmployees.Text = objtbl_Lead_Mst.NoofEmployees.ToString();



                    ////Edit

                    //txtlead1.Text = objtbl_Lead_Mst.LeadName1;
                    //txtLeadName2.Text = objtbl_Lead_Mst.LeadName2;
                    //txtLeadName3.Text = objtbl_Lead_Mst.LeadName3;
                    //txtinvalid_email.Text = objtbl_Lead_Mst.Email;
                    //txtID.Text = objtbl_Lead_Mst.Name;
                    //txtContactName.Text = objtbl_Lead_Mst.ContactPerson;
                    //txtAddress.Text = objtbl_Lead_Mst.Address;
                    //txtWebsite.Text = objtbl_Lead_Mst.Website;
                    //if (objtbl_Lead_Mst.Status != null)
                    //{
                    //    drpStatus.SelectedValue = objtbl_Lead_Mst.Status.ToString();
                    //}
                    //if (objtbl_Lead_Mst.PerformingDept != null)
                    //{
                    //    drpDepartment.SelectedValue = objtbl_Lead_Mst.PerformingDept.ToString();
                    //}

                    //objtbl_Lead_Mst.TeamLeaderName = txtteam_name.Text;
                    //if (objtbl_Lead_Mst.RefNO != null)
                    //{
                    //    drpRefNo123.SelectedValue = objtbl_Lead_Mst.RefNO.ToString();
                    //}


                    //btnAdd.Text = "Update";
                    //ViewState["Edit"] = ID;
                    //Write();
                    List<Database.tbl_Lead_Mst> List = DB.tbl_Lead_Mst.Where(p => p.Customer_Name != null && p.Supplier_Name != null && p.Project_id != null && p.Active == true && p.ID == ID && p.TenentID == TID).OrderByDescending(m => m.ID).Take(10).ToList();
                    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                    int Totalrec = List.Count();
                    ((Web.CRM.CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
                }

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

        #region only for Contact Textbox

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(ViewState["compId"]);
            Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CID);
            btnAdd.Text = "Update";
            txtContactName.Text = objtbl_CONTACT.PersName1.ToString();

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

        #region Step2
        public void BindData()
        {
            //List<Database.tbl_Lead_Mst> List = DB.tbl_Lead_Mst.OrderBy(m => m.ID).ToList();
            List<Database.tbl_Lead_Mst> List = DB.tbl_Lead_Mst.Where(p => p.Customer_Name != null && p.Supplier_Name != null && p.Project_id != null && p.Active == true && p.TenentID == TID).OrderByDescending(m => m.ID).Take(10).ToList();
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

        #region PAge Genarator
        
        public void GetShow()
        {

            lblID1s.Attributes["class"] = lblConact_id1s.Attributes["class"] = lblCustomer_id1s.Attributes["class"] = lblSupplier_id1s.Attributes["class"] = lblProject_id1s.Attributes["class"] = lblSalutation1s.Attributes["class"] = lblTitle1s.Attributes["class"] = lblTwitterScreenName1s.Attributes["class"] = lblRefered_by1s.Attributes["class"] = lbllead_source1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblDepartment1s.Attributes["class"] = lblDo_not_call1s.Attributes["class"] = lblAssistant1s.Attributes["class"] = lblAssistant_phone1s.Attributes["class"] = lblemail_opt_out1s.Attributes["class"] = lblinvalid_email1s.Attributes["class"] = lblaccount_name1s.Attributes["class"] = lblopportunity_name1s.Attributes["class"] = lblopportunity_amount1s.Attributes["class"] = lblcampaign_name1s.Attributes["class"] = lbldate_entered1s.Attributes["class"] = lblstatus_description1s.Attributes["class"] = lbldescription1s.Attributes["class"] = lblaccount_description1s.Attributes["class"] = lbllead_source_description1s.Attributes["class"] = lblWebsite1s.Attributes["class"] = lblSMS_Opt_In1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblID2h.Attributes["class"] = lblConact_id2h.Attributes["class"] = lblCustomer_id2h.Attributes["class"] = lblSupplier_id2h.Attributes["class"] = lblProject_id2h.Attributes["class"] = lblSalutation2h.Attributes["class"] = lblTitle2h.Attributes["class"] = lblTwitterScreenName2h.Attributes["class"] = lblRefered_by2h.Attributes["class"] = lbllead_source2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblDepartment2h.Attributes["class"] = lblDo_not_call2h.Attributes["class"] = lblAssistant2h.Attributes["class"] = lblAssistant_phone2h.Attributes["class"] = lblemail_opt_out2h.Attributes["class"] = lblinvalid_email2h.Attributes["class"] = lblaccount_name2h.Attributes["class"] = lblopportunity_name2h.Attributes["class"] = lblopportunity_amount2h.Attributes["class"] = lblcampaign_name2h.Attributes["class"] = lbldate_entered2h.Attributes["class"] = lblstatus_description2h.Attributes["class"] = lbldescription2h.Attributes["class"] = lblaccount_description2h.Attributes["class"] = lbllead_source_description2h.Attributes["class"] = lblWebsite2h.Attributes["class"] = lblSMS_Opt_In2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  gethide";
            //lblTenantID1s.Attributes["class"] =lbldate_modified1s.Attributes["class"] =lbldate_modified2h.Attributes["class"] =lblassigned_to_name1s.Attributes["class"] = lblcreated_by1s.Attributes["class"] = lblmodified_by1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] =
            //lblTenantID2h.Attributes["class"] = lblassigned_to_name2h.Attributes["class"] = lblcreated_by2h.Attributes["class"] = lblmodified_by2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblID1s.Attributes["class"] = lblConact_id1s.Attributes["class"] = lblCustomer_id1s.Attributes["class"] = lblSupplier_id1s.Attributes["class"] = lblProject_id1s.Attributes["class"] = lblSalutation1s.Attributes["class"] = lblTitle1s.Attributes["class"] = lblTwitterScreenName1s.Attributes["class"] = lblRefered_by1s.Attributes["class"] = lbllead_source1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblDepartment1s.Attributes["class"] = lblDo_not_call1s.Attributes["class"] = lblAssistant1s.Attributes["class"] = lblAssistant_phone1s.Attributes["class"] = lblemail_opt_out1s.Attributes["class"] = lblinvalid_email1s.Attributes["class"] = lblaccount_name1s.Attributes["class"] = lblopportunity_name1s.Attributes["class"] = lblopportunity_amount1s.Attributes["class"] = lblcampaign_name1s.Attributes["class"] = lbldate_entered1s.Attributes["class"] = lblstatus_description1s.Attributes["class"] = lbldescription1s.Attributes["class"] = lblaccount_description1s.Attributes["class"] = lbllead_source_description1s.Attributes["class"] = lblWebsite1s.Attributes["class"] = lblSMS_Opt_In1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblID2h.Attributes["class"] = lblConact_id2h.Attributes["class"] = lblCustomer_id2h.Attributes["class"] = lblSupplier_id2h.Attributes["class"] = lblProject_id2h.Attributes["class"] = lblSalutation2h.Attributes["class"] = lblTitle2h.Attributes["class"] = lblTwitterScreenName2h.Attributes["class"] = lblRefered_by2h.Attributes["class"] = lbllead_source2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblDepartment2h.Attributes["class"] = lblDo_not_call2h.Attributes["class"] = lblAssistant2h.Attributes["class"] = lblAssistant_phone2h.Attributes["class"] = lblemail_opt_out2h.Attributes["class"] = lblinvalid_email2h.Attributes["class"] = lblaccount_name2h.Attributes["class"] = lblopportunity_name2h.Attributes["class"] = lblopportunity_amount2h.Attributes["class"] = lblcampaign_name2h.Attributes["class"] = lbldate_entered2h.Attributes["class"] = lblstatus_description2h.Attributes["class"] = lbldescription2h.Attributes["class"] = lblaccount_description2h.Attributes["class"] = lbllead_source_description2h.Attributes["class"] = lblWebsite2h.Attributes["class"] = lblSMS_Opt_In2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  getshow";
            //lblTenantID1s.Attributes["class"] =lbldate_modified1s.Attributes["class"] =lblassigned_to_name1s.Attributes["class"] = lblcreated_by1s.Attributes["class"] = lblmodified_by1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] =
            //lblTenantID2h.Attributes["class"] =lbldate_modified2h.Attributes["class"] =lblassigned_to_name2h.Attributes["class"] = lblcreated_by2h.Attributes["class"] = lblmodified_by2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] =
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
            //drpRefNo123.SelectedIndex = 0;
            txtsercustomer.Text = "";
            txtsupplier.Text = "";
            drpProject_id.SelectedIndex = 0;
            DDLLeadSource.SelectedIndex = 0;
            DDLComapny.SelectedIndex = 0;
            DrpOppertunityName.SelectedIndex = 0;
            Drpcampaign_name.SelectedIndex = 0;
            DrpSalutation.SelectedIndex = 0;
            txtTitle.Text = "";
            txtTwitterScreenName.Text = "";
            txtRefered_by.Text = "";
            Chdonotcall.Checked = false;
            txtAssistant.Text = "";
            txtAssistant_phone.Text = "";
            txtinvalid_email.Text = "";
            txtaccount_name.Text = "";
            txtopportunity_amount.Text = "0";
            txtdate_entered.Text = DateTime.Now.ToShortDateString();
            //txtdate_entered.Text = "";
            txtstatus_description.Text = "";
            txtdescription.Text = "";
            txtaccount_description.Text = "";
            txtNoofEmployees.Text = "";
            txtlead1.Text = "";
            txtLeadName2.Text = "";
            txtLeadName3.Text = "";
            DrpQuestion.SelectedIndex = 0;
            DrpSearchTitle.SelectedIndex = 0;
            txtID.Text = "";
            txtContactName.Text = "";
            txtAddress.Text = "";
            txtLeadSourcefrom.Text = "";
            txtWebsite.Text = "";
            drpStatus.SelectedIndex = 0;
            drpDepartment.SelectedIndex = 0;
            drpTeamName.SelectedIndex = 0;
            DrpAssignedTeamLeader.SelectedIndex = 0;
            txtlead_source_description.Text = "";
            txtCRUP_ID.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int Reference = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //try
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

                    //btnAdd.ValidationGroup = "send";
                    int leadid = DB.tbl_Lead_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Lead_Mst.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                    btnAdd.ValidationGroup = "s";
                    Database.tbl_Lead_Mst objtbl_Lead_Mst = new Database.tbl_Lead_Mst();
                    pnlSearchbutton.Visible = false;

                    objtbl_Lead_Mst.TenentID = TID;
                    //Server Content Send data Yogesh
                    // objtbl_Lead_Mst.ID = Convert.ToInt32(drpID.SelectedValue);
                    //objtbl_Lead_Mst.Conact_id = Convert.ToInt32(drpConact_id.SelectedValue);
                    objtbl_Lead_Mst.ID = leadid;
                    ViewState["CampaignID"] = leadid;//DB.tbl_Lead_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_Lead_Mst.Max(p => p.ID) + 1) : 1;

                    objtbl_Lead_Mst.Customer_Name = txtsercustomer.Text;
                    objtbl_Lead_Mst.Supplier_Name = txtsupplier.Text;
                    if (Convert.ToInt32(DrpOppertunityName.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.Oppertunity_ID = Convert.ToInt32(DrpOppertunityName.SelectedValue);
                    }
                    if (Convert.ToInt32(Drpcampaign_name.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.Campaign_ID = Convert.ToInt32(Drpcampaign_name.SelectedValue);
                    }

                    if (Drpcampaign_name.SelectedItem.Text != null)
                    {
                        objtbl_Lead_Mst.campaign_name = Drpcampaign_name.SelectedItem.Text.ToString();
                    }
                    if (DrpOppertunityName.SelectedItem.Text != null)
                    {
                        objtbl_Lead_Mst.opportunity_name = DrpOppertunityName.SelectedItem.Text.ToString();
                    }



                    if (Convert.ToInt32(drpProject_id.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.Project_id = Convert.ToInt32(drpProject_id.SelectedValue);
                    }
                    if ((DrpSalutation.SelectedValue) != "0")
                    {
                        objtbl_Lead_Mst.Salutation = (DrpSalutation.SelectedValue);
                    }
                    if (Convert.ToInt32(DrpQuestion.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.QuestionGroup = Convert.ToInt32(DrpQuestion.SelectedValue);
                    }
                    if (Convert.ToInt32(DrpSearchTitle.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.SearchTitle = Convert.ToInt32(DrpSearchTitle.SelectedValue);
                    }
                    objtbl_Lead_Mst.Title = txtTitle.Text;
                    objtbl_Lead_Mst.TwitterScreenName = txtTwitterScreenName.Text;
                    objtbl_Lead_Mst.Refered_by = txtRefered_by.Text;
                    // objtbl_Lead_Mst.lead_source = txtlead_source.Text;
                    if (Convert.ToInt32(DDLComapny.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.Company_ID = Convert.ToInt32(DDLComapny.SelectedValue);
                    }
                    if (Convert.ToInt32(DDLLeadSource.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.lead_source = Convert.ToInt32(DDLLeadSource.SelectedValue);
                    }


                    objtbl_Lead_Mst.Do_not_call = Chdonotcall.Checked;
                    objtbl_Lead_Mst.email_opt_out = cbemail_opt_out.Checked;
                    objtbl_Lead_Mst.SMS_Opt_In = cbSMS_Opt_In.Checked;
                    objtbl_Lead_Mst.LeadSourcefrom = txtLeadSourcefrom.Text;
                    if (Convert.ToInt32(drpTeamName.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.team_name = Convert.ToInt32(drpTeamName.SelectedValue);
                    }
                    if (DrpAssignedTeamLeader.SelectedValue != "0")
                    {
                        objtbl_Lead_Mst.TeamLeaderName = DrpAssignedTeamLeader.SelectedValue;
                    }





                    //if (Convert.ToInt32(drpRefNo123.SelectedValue) != 0)
                    //{
                    //    objtbl_Lead_Mst.RefNO = Convert.ToInt32(drpRefNo123.SelectedValue);
                    //    Reference = Convert.ToInt32(drpRefNo123.SelectedValue);
                    //}
                    // objtbl_Lead_Mst.Status = txtStatus.Text;
                    //  objtbl_Lead_Mst.Department = txtDepartment.Text;
                    // objtbl_Lead_Mst.Do_not_call = cbDo_not_call.Checked;
                    objtbl_Lead_Mst.Assistant = txtAssistant.Text;
                    objtbl_Lead_Mst.Assistant_phone = txtAssistant_phone.Text;

                    // objtbl_Lead_Mst.invalid_email = cbinvalid_email.Checked;
                    objtbl_Lead_Mst.account_name = txtaccount_name.Text;
                    // objtbl_Lead_Mst.opportunity_name = txtopportunity_name.Text;
                    objtbl_Lead_Mst.opportunity_amount = Convert.ToDecimal(txtopportunity_amount.Text);
                    //  objtbl_Lead_Mst.campaign_name = txtcampaign_name.Text;
                    objtbl_Lead_Mst.date_entered = Convert.ToDateTime(txtdate_entered.Text);
                    // objtbl_Lead_Mst.date_modified = Convert.ToDateTime(txtdate_modified.Text);
                    objtbl_Lead_Mst.status_description = txtstatus_description.Text;
                    objtbl_Lead_Mst.description = txtdescription.Text;
                    objtbl_Lead_Mst.account_description = txtaccount_description.Text;
                    // objtbl_Lead_Mst.team_name = Convert.ToInt32(drpteam_name.SelectedValue);
                    objtbl_Lead_Mst.lead_source_description = txtlead_source_description.Text;
                    //  objtbl_Lead_Mst.assigned_to_name = txtassigned_to_name.Text;
                    //  objtbl_Lead_Mst.created_by = Convert.ToInt32(drpcreated_by.SelectedValue);
                    // objtbl_Lead_Mst.modified_by = Convert.ToInt32(drpmodified_by.SelectedValue);
                    // objtbl_Lead_Mst.Active = cbActive.Checked;
                    // objtbl_Lead_Mst.Deleted = cbDeleted.Checked;
                    //  objtbl_Lead_Mst.Website = txtWebsite.Text;

                    objtbl_Lead_Mst.NoofEmployees = txtNoofEmployees.Text;
                    //  objtbl_Lead_Mst.CRUP_ID = txtCRUP_ID.Text;
                    objtbl_Lead_Mst.Deleted = true;
                    objtbl_Lead_Mst.created_by = 1;
                    objtbl_Lead_Mst.modified_by = 1;
                    objtbl_Lead_Mst.LeadName1 = txtlead1.Text;
                    objtbl_Lead_Mst.LeadName2 = txtLeadName2.Text;
                    objtbl_Lead_Mst.LeadName3 = txtLeadName3.Text;
                    objtbl_Lead_Mst.Email = txtinvalid_email.Text;
                    objtbl_Lead_Mst.Name = txtID.Text;
                    objtbl_Lead_Mst.ContactPerson = txtContactName.Text;
                    objtbl_Lead_Mst.Address = txtAddress.Text;
                    objtbl_Lead_Mst.Website = txtWebsite.Text;
                    if (Convert.ToInt32(drpStatus.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.Status = Convert.ToInt32(drpStatus.SelectedValue);
                    }
                    if (Convert.ToInt32(drpDepartment.SelectedValue) != 0)
                    {
                        objtbl_Lead_Mst.PerformingDept = Convert.ToInt32(drpDepartment.SelectedValue);
                    }
                    objtbl_Lead_Mst.Active = true;
                    //objtbl_Lead_Mst.TeamLeaderName = txtteam_name.Text;
                    // objtbl_Lead_Mst.lea = txtlead1.Text;
                    int TID1 = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int CampID = 0;
                    //if (DB.tbl_Lead_Mst.Count() == 0)
                    //{
                    if (ViewState["CampaignID"] != null)
                    {

                        CampID = Convert.ToInt32(leadid);
                        //CRMMainActivity(TID1, Reference, CampID);
                        //string Refrction = drpRefNo123.SelectedValue;
                        string Name = txtlead1.Text;
                        string Discrption = txtContactName.Text;
                        //InsertCRMMainActivity(CampID, Refrction, Name, Discrption);
                    }

                    //}
                    //  CRMActivity(TID1, Reference);
                    DB.tbl_Lead_Mst.AddObject(objtbl_Lead_Mst);
                    DB.SaveChanges();
                    ViewState["LeadID"] = leadid;
                    //if (ViewState["RefID"] != null && ViewState["LeadID"] != null)
                    //{
                    //    int Refid = Convert.ToInt32(ViewState["RefID"]);
                    //    int LeadID = Convert.ToInt32(ViewState["LeadID"]);
                    //    Database.REFTABLE obj_Ref = DB.REFTABLE.SingleOrDefault(p => p.REFID == Refid);
                    //    obj_Ref.SWITCH3 = "220003Camp" + LeadID;
                    //    DB.SaveChanges();
                    //}
                    Clear();
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //btnAdd.ValidationGroup = "submit";
                    //  navigation.Visible = true;
                    btnAdd.ValidationGroup = "submit";
                    btnAdd.Text = "AddNew";
                    Readonly();
                    LastData();
                    ViewState["LeadID"] = objtbl_Lead_Mst.ID;
                }
                else if (btnAdd.Text == "Update")
                {

                    if (ViewState["Edit"] != null)
                    {

                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        Session["CampaignID"] = ID;
                        Database.tbl_Lead_Mst objtbl_Lead_Mst = DB.tbl_Lead_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                        // objtbl_Lead_Mst.ID = Convert.ToInt32(drpID.SelectedValue);
                        // objtbl_Lead_Mst.Conact_id = Convert.ToInt32(drpConact_id.SelectedValue);
                        objtbl_Lead_Mst.Customer_Name = txtsercustomer.Text;
                        objtbl_Lead_Mst.Supplier_Name = txtsupplier.Text;
                        objtbl_Lead_Mst.Project_id = Convert.ToInt32(drpProject_id.SelectedValue);
                        objtbl_Lead_Mst.Salutation = (DrpSalutation.SelectedValue);
                        //objtbl_Lead_Mst.Salutation = txtSalutation.Text;
                        objtbl_Lead_Mst.Title = txtTitle.Text;
                        objtbl_Lead_Mst.TwitterScreenName = txtTwitterScreenName.Text;
                        objtbl_Lead_Mst.Refered_by = txtRefered_by.Text;
                        // objtbl_Lead_Mst.lead_source = txtlead_source.Text;
                        if (Convert.ToInt32(Drpcampaign_name.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.Campaign_ID = Convert.ToInt32(Drpcampaign_name.SelectedValue);
                        }
                        if (Convert.ToInt32(DrpOppertunityName.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.Oppertunity_ID = Convert.ToInt32(DrpOppertunityName.SelectedValue);
                        }
                        if (Drpcampaign_name.SelectedItem.Text != null)
                        {
                            objtbl_Lead_Mst.campaign_name = Drpcampaign_name.SelectedItem.Text.ToString();
                        }
                        if (DrpOppertunityName.SelectedItem.Text != null)
                        {
                            objtbl_Lead_Mst.opportunity_name = DrpOppertunityName.SelectedItem.Text.ToString();
                        }
                        objtbl_Lead_Mst.Do_not_call = Chdonotcall.Checked;
                        objtbl_Lead_Mst.email_opt_out = cbemail_opt_out.Checked;
                        objtbl_Lead_Mst.SMS_Opt_In = cbSMS_Opt_In.Checked;
                        objtbl_Lead_Mst.LeadSourcefrom = txtLeadSourcefrom.Text;
                        //objtbl_Lead_Mst.TeamLeaderName = txtteam_name.Text;

                        if (Convert.ToInt32(DDLLeadSource.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.lead_source = Convert.ToInt32(DDLLeadSource.SelectedValue);
                        }
                        if (Convert.ToInt32(DDLComapny.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.Company_ID = Convert.ToInt32(DDLComapny.SelectedValue);
                        }
                        if (Convert.ToInt32(DrpQuestion.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.QuestionGroup = Convert.ToInt32(DrpQuestion.SelectedValue);
                        }
                        if (Convert.ToInt32(DrpSearchTitle.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.SearchTitle = Convert.ToInt32(DrpSearchTitle.SelectedValue);
                        }
                        if (Convert.ToInt32(drpTeamName.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.team_name = Convert.ToInt32(drpTeamName.SelectedValue);
                        }
                        if (DrpAssignedTeamLeader.SelectedValue != "0")
                        {
                            objtbl_Lead_Mst.TeamLeaderName = DrpAssignedTeamLeader.SelectedValue;
                        }



                        //if (Convert.ToInt32(drpRefNo123.SelectedValue) != 0)
                        //{
                        //    objtbl_Lead_Mst.RefNO = Convert.ToInt32(drpRefNo123.SelectedValue);
                        //}
                        objtbl_Lead_Mst.Assistant = txtAssistant.Text;
                        objtbl_Lead_Mst.Assistant_phone = txtAssistant_phone.Text;
                        objtbl_Lead_Mst.email_opt_out = cbemail_opt_out.Checked;
                        objtbl_Lead_Mst.account_name = txtaccount_name.Text;
                        objtbl_Lead_Mst.opportunity_amount = Convert.ToDecimal(txtopportunity_amount.Text);
                        objtbl_Lead_Mst.date_entered = Convert.ToDateTime(txtdate_entered.Text);
                        objtbl_Lead_Mst.status_description = txtstatus_description.Text;
                        objtbl_Lead_Mst.description = txtdescription.Text;
                        objtbl_Lead_Mst.account_description = txtaccount_description.Text;
                        objtbl_Lead_Mst.lead_source_description = txtlead_source_description.Text;
                        objtbl_Lead_Mst.SMS_Opt_In = cbSMS_Opt_In.Checked;
                        objtbl_Lead_Mst.NoofEmployees = txtNoofEmployees.Text;
                        objtbl_Lead_Mst.LeadName1 = txtlead1.Text;
                        objtbl_Lead_Mst.LeadName2 = txtLeadName2.Text;
                        objtbl_Lead_Mst.LeadName3 = txtLeadName3.Text;
                        objtbl_Lead_Mst.Email = txtinvalid_email.Text;
                        objtbl_Lead_Mst.Name = txtID.Text;
                        objtbl_Lead_Mst.Active = true;
                        objtbl_Lead_Mst.Deleted = true;
                        objtbl_Lead_Mst.created_by = 1;
                        objtbl_Lead_Mst.modified_by = 1;

                        objtbl_Lead_Mst.ContactPerson = txtContactName.Text;
                        objtbl_Lead_Mst.Address = txtAddress.Text;
                        objtbl_Lead_Mst.Website = txtWebsite.Text;
                        if (Convert.ToInt32(drpStatus.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.Status = Convert.ToInt32(drpStatus.SelectedValue);
                        }
                        if (Convert.ToInt32(drpDepartment.SelectedValue) != 0)
                        {
                            objtbl_Lead_Mst.PerformingDept = Convert.ToInt32(drpDepartment.SelectedValue);
                        }
                        //objtbl_Lead_Mst.TeamLeaderName = txtteam_name.Text;
                        // objtbl_Lead_Mst.CRUP_ID = txtCRUP_ID.Text;


                        ViewState["Edit"] = null;
                        btnAdd.ValidationGroup = "Submit";
                        btnAdd.Text = "AddNew";
                        DB.SaveChanges();
                        // Clear();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //  navigation.Visible = true;
                        Readonly();
                        pnlSearchbutton.Visible = false;
                        LastData();
                        Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                        DivMain.Attributes["style"] = "display: none;";

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
            LastData();
            Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
            DivMain.Attributes["style"] = "display: none;";
            btnAdd.Text = "AddNew";
            btnAdd.ValidationGroup = "";
            pnlSearchbutton.Visible = false;


            //Clear();
            //LastData();
            //Readonly();
            //Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
            //DivMain.Attributes["style"] = "display: none;";
            //pnlSearchbutton.Visible = false;
            //btnAdd.Text = "AddNew";

            //Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();
           //drpConact_id.DataSource = DB.TBLCONTACT.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY");
            //drpConact_id.DataTextField = "PersName1";
            //drpConact_id.DataValueField = "ContactMyID";
            //drpConact_id.DataBind();
            //drpConact_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpCustomer_id.DataSource = DB.TBLCOMPANYSETUP.Where(P => P.TenentID == TID && P.Approved == 1 && P.SALER == true);
            //drpCustomer_id.DataTextField = "COMPNAME";
            //drpCustomer_id.DataValueField = "COMPID";
            //drpCustomer_id.DataBind();
            //drpCustomer_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            //   Classes.CRMClass.getcrmdropdown(drpCustomer_id, TID, "", "", "", "TBLCOMPANYSETUP");

            //drpSupplier_id.DataSource = DB.TBLCOMPANYSETUP.Where(P => P.TenentID == TID && P.Approved == 1 && P.SALER == true);
            //drpSupplier_id.DataTextField = "COMPNAME";
            //drpSupplier_id.DataValueField = "COMPID";
            //drpSupplier_id.DataBind();
            //drpSupplier_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            //  Classes.CRMClass.getcrmdropdown(drpSupplier_id, TID, "", "", "", "TBLCOMPANYSETUP");
            // Classes.CRMClass.getCRMdropdown(drpSupplier_id, TID, "TBLCOMPANYSETUP");

            //drpProject_id.DataSource = DB.CRM_TBLPROJECT.Where(p => p.TenentID == TID);
            //drpProject_id.DataTextField = "PROJECTNAME1";
            //drpProject_id.DataValueField = "PROJECTID";
            //drpProject_id.DataBind();
            //drpProject_id.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpProject_id, TID, "", "", "", "TBLPROJECT");
            //select * from TBLPROJECT
            //Classes.CRMClass.getCRMdropdown(drpProject_id, TID, "CRM_TBLPROJECT");

            //DDLLeadSource.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "LEADSTATUS");
            //DDLLeadSource.DataTextField = "REFNAME1";
            //DDLLeadSource.DataValueField = "REFID";
            //DDLLeadSource.DataBind();
            //DDLLeadSource.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(DDLLeadSource, TID, "ACTVTY", "LEADSTATUS", "", "REFTABLE");
            //select * from REFTABLE where Active= 'Y' and REFTYPE = 'ACTVTY' and REFSUBTYPE = 'LEADSTATUS'

            //drpStatus.DataSource = DB.REFTABLE.Where(p =>p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS");
            //drpStatus.DataTextField = "REFNAME1";
            //drpStatus.DataValueField = "REFID";
            //drpStatus.DataBind();
            //drpStatus.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpStatus, TID, "ACTVTY", "STATUS", "", "REFTABLE");
            //select * from REFTABLE where Active= 'Y' and REFTYPE = 'ACTVTY' and REFSUBTYPE = 'STATUS'

            //DDLComapny.DataSource = DB.TBLCOMPANYSETUP.Where(P => P.TenentID == TID && P.Approved == 0);
            //DDLComapny.DataTextField = "COMPNAME";
            //DDLComapny.DataValueField = "COMPID";
            //DDLComapny.DataBind();
            //DDLComapny.Items.Insert(0, new ListItem("-- Select --", "0"));
            DDLComapny.Items.Insert(0, new ListItem("-- Select --", "0"));
            //Classes.CRMClass.getcrmdropdown(DDLComapny, TID, "", "", "", "TBLCOMPANYSETUP");

            //DrpOppertunityName.DataSource = DB.tbl_Opportunity_Mst.Where(P => P.TenentID == TID && P.Deleted==true && P.Active == true);
            //DrpOppertunityName.DataTextField = "Name";
            //DrpOppertunityName.DataValueField = "OpportID";
            //DrpOppertunityName.DataBind();
            //DrpOppertunityName.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(DrpOppertunityName, TID, "", "", "", "tbl_Opportunity_Mst");
            //select * from tbl_Opportunity_Mst

            //Drpcampaign_name.DataSource = DB.CRM_tbl_Campaign_Mst.Where(p => p.TenentID == TID && p.Active == true && p.Deleted == true);
            //Drpcampaign_name.DataTextField = "Name";
            //Drpcampaign_name.DataValueField = "ID";
            //Drpcampaign_name.DataBind();
            //Drpcampaign_name.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(Drpcampaign_name, TID, "", "", "", "tbl_Campaign_Mst");
            //select * from tbl_Campaign_Mst

            //drpDepartment.DataSource = DB.CRM_ISDepartment1.Where(p => p.TenentID == TID && p.Active == true && p.Deleted == true);
            //drpDepartment.DataTextField = "DepartmentName1";
            //drpDepartment.DataValueField = "DepartmentID";
            //drpDepartment.DataBind();
            //drpDepartment.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(drpDepartment, TID, "", "", "", "ISDepartment1");
            //select * from ISDepartment1

            Classes.EcommAdminClass.getdropdown(drpTeamName, TID, "TeamTitle", "TeamTitle", "", "REFTABLE");
            //select * from REFTABLE where Active= 'Y' and REFTYPE = 'TeamTitle' and REFSUBTYPE = 'TeamTitle'

            DrpAssignedTeamLeader.Items.Insert(0, new ListItem("-- Select --", "0"));

            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            var TitleData = (from TitleRef in DB.REFTABLEs
                             join Search in DB.ISSearchDetails on TitleRef.REFID equals Search.REFID
                             where Search.CreatedBy == UID && TitleRef.REFTYPE == RefType && Search.TenentID == TID && TitleRef.TenentID == TID
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

            //DrpQuestion.DataSource = DB.CRM_QuestionMaster.Where(p => p.TenentID == TID && p.Avtive == true && p.Deleted == true && p.CreateBy == UID);
            //DrpQuestion.DataTextField = "QuestionLang1";
            //DrpQuestion.DataValueField = "ID";
            //DrpQuestion.DataBind();
            //DrpQuestion.Items.Insert(0, new ListItem("-- Select --", "0"));
            Classes.EcommAdminClass.getdropdown(DrpQuestion, TID, "", "", "", "TBLGROUP");
            //select * from TBLGROUP
            //Classes.CRMClass.getcrmdropdown(DrpQuestion, TID, "", "", "", "CRM_QuestionMaster");
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
        int LIDID = 0;
        public void FirstData()
        {
            LIDID = DB.tbl_Lead_Mst.Where(p => p.Active == true && p.TenentID == TID).FirstOrDefault().ID;

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
            if (DB.tbl_Lead_Mst.Where(p => p.Active == true && p.TenentID == TID).Count() > 0)
            {
                LIDID = DB.tbl_Lead_Mst.Where(p => p.Active == true && p.TenentID == TID).Max(p => p.ID);
                BindCommand(LIDID);
                Listview1.SelectedIndex = LIDID;
                Readonly();
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
                    // lblTenantID2h.Visible = lblID2h.Visible = lblConact_id2h.Visible = lblCustomer_id2h.Visible = lblSupplier_id2h.Visible = lblProject_id2h.Visible = lblSalutation2h.Visible = lblTitle2h.Visible = lblTwitterScreenName2h.Visible = lblRefered_by2h.Visible = lbllead_source2h.Visible = lblStatus2h.Visible = lblDepartment2h.Visible = lblDo_not_call2h.Visible = lblAssistant2h.Visible = lblAssistant_phone2h.Visible = lblemail_opt_out2h.Visible = lblinvalid_email2h.Visible = lblaccount_name2h.Visible = lblopportunity_name2h.Visible = lblopportunity_amount2h.Visible = lblcampaign_name2h.Visible = lbldate_entered2h.Visible = lbldate_modified2h.Visible = lblstatus_description2h.Visible = lbldescription2h.Visible = lblaccount_description2h.Visible = lblteam_name2h.Visible = lbllead_source_description2h.Visible = lblassigned_to_name2h.Visible = lblcreated_by2h.Visible = lblmodified_by2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = lblWebsite2h.Visible = lblSMS_Opt_In2h.Visible = lblCRUP_ID2h.Visible = false;
                    lblID2h.Visible = lblConact_id2h.Visible = lblCustomer_id2h.Visible = lblSupplier_id2h.Visible = lblProject_id2h.Visible = lblSalutation2h.Visible = lblTitle2h.Visible = lblTwitterScreenName2h.Visible = lblRefered_by2h.Visible = lbllead_source2h.Visible = lblStatus2h.Visible = lblDepartment2h.Visible = lblDo_not_call2h.Visible = lblAssistant2h.Visible = lblAssistant_phone2h.Visible = lblemail_opt_out2h.Visible = lblinvalid_email2h.Visible = lblaccount_name2h.Visible = lblopportunity_name2h.Visible = lblopportunity_amount2h.Visible = lblcampaign_name2h.Visible = lbldate_entered2h.Visible = lblstatus_description2h.Visible = lbldescription2h.Visible = lblaccount_description2h.Visible = lbllead_source_description2h.Visible = lblWebsite2h.Visible = lblSMS_Opt_In2h.Visible = lblCRUP_ID2h.Visible = false;
                    //2true
                    // txtTenantID2h.Visible = txtID2h.Visible = txtConact_id2h.Visible = txtCustomer_id2h.Visible = txtSupplier_id2h.Visible = txtProject_id2h.Visible = txtSalutation2h.Visible = txtTitle2h.Visible = txtTwitterScreenName2h.Visible = txtRefered_by2h.Visible = txtlead_source2h.Visible = txtStatus2h.Visible = txtDepartment2h.Visible = txtDo_not_call2h.Visible = txtAssistant2h.Visible = txtAssistant_phone2h.Visible = txtemail_opt_out2h.Visible = txtinvalid_email2h.Visible = txtaccount_name2h.Visible = txtopportunity_name2h.Visible = txtopportunity_amount2h.Visible = txtcampaign_name2h.Visible = txtdate_entered2h.Visible = txtdate_modified2h.Visible = txtstatus_description2h.Visible = txtdescription2h.Visible = txtaccount_description2h.Visible = txtteam_name2h.Visible = txtlead_source_description2h.Visible = txtassigned_to_name2h.Visible = txtcreated_by2h.Visible = txtmodified_by2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = txtWebsite2h.Visible = txtSMS_Opt_In2h.Visible = txtCRUP_ID2h.Visible = true;
                    txtID2h.Visible = txtConact_id2h.Visible = txtCustomer_id2h.Visible = txtSupplier_id2h.Visible = txtProject_id2h.Visible = txtSalutation2h.Visible = txtTitle2h.Visible = txtTwitterScreenName2h.Visible = txtRefered_by2h.Visible = txtlead_source2h.Visible = txtStatus2h.Visible = txtDepartment2h.Visible = txtDo_not_call2h.Visible = txtAssistant2h.Visible = txtAssistant_phone2h.Visible = txtemail_opt_out2h.Visible = txtinvalid_email2h.Visible = txtaccount_name2h.Visible = txtopportunity_name2h.Visible = txtopportunity_amount2h.Visible = txtcampaign_name2h.Visible = txtdate_entered2h.Visible = txtstatus_description2h.Visible = txtdescription2h.Visible = txtaccount_description2h.Visible = txtlead_source_description2h.Visible = txtWebsite2h.Visible = txtSMS_Opt_In2h.Visible = txtCRUP_ID2h.Visible = true;

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
                    //lblTenantID2h.Visible = lblID2h.Visible = lblConact_id2h.Visible = lblCustomer_id2h.Visible = lblSupplier_id2h.Visible = lblProject_id2h.Visible = lblSalutation2h.Visible = lblTitle2h.Visible = lblTwitterScreenName2h.Visible = lblRefered_by2h.Visible = lbllead_source2h.Visible = lblStatus2h.Visible = lblDepartment2h.Visible = lblDo_not_call2h.Visible = lblAssistant2h.Visible = lblAssistant_phone2h.Visible = lblemail_opt_out2h.Visible = lblinvalid_email2h.Visible = lblaccount_name2h.Visible = lblopportunity_name2h.Visible = lblopportunity_amount2h.Visible = lblcampaign_name2h.Visible = lbldate_entered2h.Visible = lbldate_modified2h.Visible = lblstatus_description2h.Visible = lbldescription2h.Visible = lblaccount_description2h.Visible = lblteam_name2h.Visible = lbllead_source_description2h.Visible = lblassigned_to_name2h.Visible = lblcreated_by2h.Visible = lblmodified_by2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = lblWebsite2h.Visible = lblSMS_Opt_In2h.Visible = lblCRUP_ID2h.Visible = true;
                    lblID2h.Visible = lblConact_id2h.Visible = lblCustomer_id2h.Visible = lblSupplier_id2h.Visible = lblProject_id2h.Visible = lblSalutation2h.Visible = lblTitle2h.Visible = lblTwitterScreenName2h.Visible = lblRefered_by2h.Visible = lbllead_source2h.Visible = lblStatus2h.Visible = lblDepartment2h.Visible = lblDo_not_call2h.Visible = lblAssistant2h.Visible = lblAssistant_phone2h.Visible = lblemail_opt_out2h.Visible = lblinvalid_email2h.Visible = lblaccount_name2h.Visible = lblopportunity_name2h.Visible = lblopportunity_amount2h.Visible = lblcampaign_name2h.Visible = lbldate_entered2h.Visible = lblstatus_description2h.Visible = lbldescription2h.Visible = lblaccount_description2h.Visible = lbllead_source_description2h.Visible = lblWebsite2h.Visible = lblSMS_Opt_In2h.Visible = lblCRUP_ID2h.Visible = true;
                    //2false
                    //txtTenantID2h.Visible = txtID2h.Visible = txtConact_id2h.Visible = txtCustomer_id2h.Visible = txtSupplier_id2h.Visible = txtProject_id2h.Visible = txtSalutation2h.Visible = txtTitle2h.Visible = txtTwitterScreenName2h.Visible = txtRefered_by2h.Visible = txtlead_source2h.Visible = txtStatus2h.Visible = txtDepartment2h.Visible = txtDo_not_call2h.Visible = txtAssistant2h.Visible = txtAssistant_phone2h.Visible = txtemail_opt_out2h.Visible = txtinvalid_email2h.Visible = txtaccount_name2h.Visible = txtopportunity_name2h.Visible = txtopportunity_amount2h.Visible = txtcampaign_name2h.Visible = txtdate_entered2h.Visible = txtdate_modified2h.Visible = txtstatus_description2h.Visible = txtdescription2h.Visible = txtaccount_description2h.Visible = txtteam_name2h.Visible = txtlead_source_description2h.Visible = txtassigned_to_name2h.Visible = txtcreated_by2h.Visible = txtmodified_by2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = txtWebsite2h.Visible = txtSMS_Opt_In2h.Visible = txtCRUP_ID2h.Visible = false;
                    txtID2h.Visible = txtConact_id2h.Visible = txtCustomer_id2h.Visible = txtSupplier_id2h.Visible = txtProject_id2h.Visible = txtSalutation2h.Visible = txtTitle2h.Visible = txtTwitterScreenName2h.Visible = txtRefered_by2h.Visible = txtlead_source2h.Visible = txtStatus2h.Visible = txtDepartment2h.Visible = txtDo_not_call2h.Visible = txtAssistant2h.Visible = txtAssistant_phone2h.Visible = txtemail_opt_out2h.Visible = txtinvalid_email2h.Visible = txtaccount_name2h.Visible = txtopportunity_name2h.Visible = txtopportunity_amount2h.Visible = txtcampaign_name2h.Visible = txtdate_entered2h.Visible = txtstatus_description2h.Visible = txtdescription2h.Visible = txtaccount_description2h.Visible = txtlead_source_description2h.Visible = txtWebsite2h.Visible = txtSMS_Opt_In2h.Visible = txtCRUP_ID2h.Visible = false;

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
                    //lblTenantID1s.Visible = lblID1s.Visible = lblConact_id1s.Visible = lblCustomer_id1s.Visible = lblSupplier_id1s.Visible = lblProject_id1s.Visible = lblSalutation1s.Visible = lblTitle1s.Visible = lblTwitterScreenName1s.Visible = lblRefered_by1s.Visible = lbllead_source1s.Visible = lblStatus1s.Visible = lblDepartment1s.Visible = lblDo_not_call1s.Visible = lblAssistant1s.Visible = lblAssistant_phone1s.Visible = lblemail_opt_out1s.Visible = lblinvalid_email1s.Visible = lblaccount_name1s.Visible = lblopportunity_name1s.Visible = lblopportunity_amount1s.Visible = lblcampaign_name1s.Visible = lbldate_entered1s.Visible = lbldate_modified1s.Visible = lblstatus_description1s.Visible = lbldescription1s.Visible = lblaccount_description1s.Visible = lblteam_name1s.Visible = lbllead_source_description1s.Visible = lblassigned_to_name1s.Visible = lblcreated_by1s.Visible = lblmodified_by1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = lblWebsite1s.Visible = lblSMS_Opt_In1s.Visible = lblCRUP_ID1s.Visible = false;
                    lblID1s.Visible = lblConact_id1s.Visible = lblCustomer_id1s.Visible = lblSupplier_id1s.Visible = lblProject_id1s.Visible = lblSalutation1s.Visible = lblTitle1s.Visible = lblTwitterScreenName1s.Visible = lblRefered_by1s.Visible = lbllead_source1s.Visible = lblStatus1s.Visible = lblDepartment1s.Visible = lblDo_not_call1s.Visible = lblAssistant1s.Visible = lblAssistant_phone1s.Visible = lblemail_opt_out1s.Visible = lblinvalid_email1s.Visible = lblaccount_name1s.Visible = lblopportunity_name1s.Visible = lblopportunity_amount1s.Visible = lblcampaign_name1s.Visible = lbldate_entered1s.Visible = lblstatus_description1s.Visible = lbldescription1s.Visible = lblaccount_description1s.Visible = lbllead_source_description1s.Visible = lblWebsite1s.Visible = lblSMS_Opt_In1s.Visible = lblCRUP_ID1s.Visible = false;
                    //1true
                    // txtTenantID1s.Visible = txtID1s.Visible = txtConact_id1s.Visible = txtCustomer_id1s.Visible = txtSupplier_id1s.Visible = txtProject_id1s.Visible = txtSalutation1s.Visible = txtTitle1s.Visible = txtTwitterScreenName1s.Visible = txtRefered_by1s.Visible = txtlead_source1s.Visible = txtStatus1s.Visible = txtDepartment1s.Visible = txtDo_not_call1s.Visible = txtAssistant1s.Visible = txtAssistant_phone1s.Visible = txtemail_opt_out1s.Visible = txtinvalid_email1s.Visible = txtaccount_name1s.Visible = txtopportunity_name1s.Visible = txtopportunity_amount1s.Visible = txtcampaign_name1s.Visible = txtdate_entered1s.Visible = txtdate_modified1s.Visible = txtstatus_description1s.Visible = txtdescription1s.Visible = txtaccount_description1s.Visible = txtteam_name1s.Visible = txtlead_source_description1s.Visible = txtassigned_to_name1s.Visible = txtcreated_by1s.Visible = txtmodified_by1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = txtWebsite1s.Visible = txtSMS_Opt_In1s.Visible = txtCRUP_ID1s.Visible = true;
                    txtID1s.Visible = txtConact_id1s.Visible = txtCustomer_id1s.Visible = txtSupplier_id1s.Visible = txtProject_id1s.Visible = txtSalutation1s.Visible = txtTitle1s.Visible = txtTwitterScreenName1s.Visible = txtRefered_by1s.Visible = txtlead_source1s.Visible = txtStatus1s.Visible = txtDepartment1s.Visible = txtDo_not_call1s.Visible = txtAssistant1s.Visible = txtAssistant_phone1s.Visible = txtemail_opt_out1s.Visible = txtinvalid_email1s.Visible = txtaccount_name1s.Visible = txtopportunity_name1s.Visible = txtopportunity_amount1s.Visible = txtcampaign_name1s.Visible = txtdate_entered1s.Visible = txtstatus_description1s.Visible = txtdescription1s.Visible = txtaccount_description1s.Visible = txtlead_source_description1s.Visible = txtWebsite1s.Visible = txtSMS_Opt_In1s.Visible = txtCRUP_ID1s.Visible = true;
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
                    //lblTenantID1s.Visible = lblID1s.Visible = lblConact_id1s.Visible = lblCustomer_id1s.Visible = lblSupplier_id1s.Visible = lblProject_id1s.Visible = lblSalutation1s.Visible = lblTitle1s.Visible = lblTwitterScreenName1s.Visible = lblRefered_by1s.Visible = lbllead_source1s.Visible = lblStatus1s.Visible = lblDepartment1s.Visible = lblDo_not_call1s.Visible = lblAssistant1s.Visible = lblAssistant_phone1s.Visible = lblemail_opt_out1s.Visible = lblinvalid_email1s.Visible = lblaccount_name1s.Visible = lblopportunity_name1s.Visible = lblopportunity_amount1s.Visible = lblcampaign_name1s.Visible = lbldate_entered1s.Visible = lbldate_modified1s.Visible = lblstatus_description1s.Visible = lbldescription1s.Visible = lblaccount_description1s.Visible = lblteam_name1s.Visible = lbllead_source_description1s.Visible = lblassigned_to_name1s.Visible = lblcreated_by1s.Visible = lblmodified_by1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = lblWebsite1s.Visible = lblSMS_Opt_In1s.Visible = lblCRUP_ID1s.Visible = true;
                    lblID1s.Visible = lblConact_id1s.Visible = lblCustomer_id1s.Visible = lblSupplier_id1s.Visible = lblProject_id1s.Visible = lblSalutation1s.Visible = lblTitle1s.Visible = lblTwitterScreenName1s.Visible = lblRefered_by1s.Visible = lbllead_source1s.Visible = lblStatus1s.Visible = lblDepartment1s.Visible = lblDo_not_call1s.Visible = lblAssistant1s.Visible = lblAssistant_phone1s.Visible = lblemail_opt_out1s.Visible = lblinvalid_email1s.Visible = lblaccount_name1s.Visible = lblopportunity_name1s.Visible = lblopportunity_amount1s.Visible = lblcampaign_name1s.Visible = lbldate_entered1s.Visible = lblstatus_description1s.Visible = lbldescription1s.Visible = lblaccount_description1s.Visible = lbllead_source_description1s.Visible = lblWebsite1s.Visible = lblSMS_Opt_In1s.Visible = lblCRUP_ID1s.Visible = true;
                    //1false
                    // txtTenantID1s.Visible = txtID1s.Visible = txtConact_id1s.Visible = txtCustomer_id1s.Visible = txtSupplier_id1s.Visible = txtProject_id1s.Visible = txtSalutation1s.Visible = txtTitle1s.Visible = txtTwitterScreenName1s.Visible = txtRefered_by1s.Visible = txtlead_source1s.Visible = txtStatus1s.Visible = txtDepartment1s.Visible = txtDo_not_call1s.Visible = txtAssistant1s.Visible = txtAssistant_phone1s.Visible = txtemail_opt_out1s.Visible = txtinvalid_email1s.Visible = txtaccount_name1s.Visible = txtopportunity_name1s.Visible = txtopportunity_amount1s.Visible = txtcampaign_name1s.Visible = txtdate_entered1s.Visible = txtdate_modified1s.Visible = txtstatus_description1s.Visible = txtdescription1s.Visible = txtaccount_description1s.Visible = txtteam_name1s.Visible = txtlead_source_description1s.Visible = txtassigned_to_name1s.Visible = txtcreated_by1s.Visible = txtmodified_by1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = txtWebsite1s.Visible = txtSMS_Opt_In1s.Visible = txtCRUP_ID1s.Visible = false;
                    txtID1s.Visible = txtConact_id1s.Visible = txtCustomer_id1s.Visible = txtSupplier_id1s.Visible = txtProject_id1s.Visible = txtSalutation1s.Visible = txtTitle1s.Visible = txtTwitterScreenName1s.Visible = txtRefered_by1s.Visible = txtlead_source1s.Visible = txtStatus1s.Visible = txtDepartment1s.Visible = txtDo_not_call1s.Visible = txtAssistant1s.Visible = txtAssistant_phone1s.Visible = txtemail_opt_out1s.Visible = txtinvalid_email1s.Visible = txtaccount_name1s.Visible = txtopportunity_name1s.Visible = txtopportunity_amount1s.Visible = txtcampaign_name1s.Visible = txtdate_entered1s.Visible = txtstatus_description1s.Visible = txtdescription1s.Visible = txtaccount_description1s.Visible = txtlead_source_description1s.Visible = txtWebsite1s.Visible = txtSMS_Opt_In1s.Visible = txtCRUP_ID1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Lead_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenantID1s.ID == item.LabelID)
                //    txtTenantID1s.Text = lblTenantID1s.Text = item.LabelName;
                if (lblID1s.ID == item.LabelID)
                    txtID1s.Text = lblID1s.Text = item.LabelName;
                else if (lblConact_id1s.ID == item.LabelID)
                    txtConact_id1s.Text = lblConact_id1s.Text = item.LabelName;
                else if (lblCustomer_id1s.ID == item.LabelID)
                    txtCustomer_id1s.Text = lblCustomer_id1s.Text = item.LabelName;
                else if (lblSupplier_id1s.ID == item.LabelID)
                    txtSupplier_id1s.Text = lblSupplier_id1s.Text = item.LabelName;
                else if (lblProject_id1s.ID == item.LabelID)
                    txtProject_id1s.Text = lblProject_id1s.Text = item.LabelName;
                else if (lblSalutation1s.ID == item.LabelID)
                    txtSalutation1s.Text = lblSalutation1s.Text = item.LabelName;
                else if (lblTitle1s.ID == item.LabelID)
                    txtTitle1s.Text = lblTitle1s.Text = item.LabelName;
                else if (lblTwitterScreenName1s.ID == item.LabelID)
                    txtTwitterScreenName1s.Text = lblTwitterScreenName1s.Text = item.LabelName;
                else if (lblRefered_by1s.ID == item.LabelID)
                    txtRefered_by1s.Text = lblRefered_by1s.Text = item.LabelName;
                else if (lbllead_source1s.ID == item.LabelID)
                    txtlead_source1s.Text = lbllead_source1s.Text = item.LabelName;
                else if (lblStatus1s.ID == item.LabelID)
                    txtStatus1s.Text = lblStatus1s.Text = item.LabelName;
                else if (lblDepartment1s.ID == item.LabelID)
                    txtDepartment1s.Text = lblDepartment1s.Text = item.LabelName;
                else if (lblDo_not_call1s.ID == item.LabelID)
                    txtDo_not_call1s.Text = lblDo_not_call1s.Text = item.LabelName;
                else if (lblAssistant1s.ID == item.LabelID)
                    txtAssistant1s.Text = lblAssistant1s.Text = item.LabelName;
                else if (lblAssistant_phone1s.ID == item.LabelID)
                    txtAssistant_phone1s.Text = lblAssistant_phone1s.Text = item.LabelName;
                else if (lblemail_opt_out1s.ID == item.LabelID)
                    txtemail_opt_out1s.Text = lblemail_opt_out1s.Text = item.LabelName;
                else if (lblinvalid_email1s.ID == item.LabelID)
                    txtinvalid_email1s.Text = lblinvalid_email1s.Text = item.LabelName;
                else if (lblaccount_name1s.ID == item.LabelID)
                    txtaccount_name1s.Text = lblaccount_name1s.Text = item.LabelName;
                else if (lblopportunity_name1s.ID == item.LabelID)
                    txtopportunity_name1s.Text = lblopportunity_name1s.Text = item.LabelName;
                else if (lblopportunity_amount1s.ID == item.LabelID)
                    txtopportunity_amount1s.Text = lblopportunity_amount1s.Text = item.LabelName;
                else if (lblcampaign_name1s.ID == item.LabelID)
                    txtcampaign_name1s.Text = lblcampaign_name1s.Text = item.LabelName;
                else if (lbldate_entered1s.ID == item.LabelID)
                    txtdate_entered1s.Text = lbldate_entered1s.Text = item.LabelName;
                //else if (lbldate_modified1s.ID == item.LabelID)
                //    txtdate_modified1s.Text = lbldate_modified1s.Text = item.LabelName;
                else if (lblstatus_description1s.ID == item.LabelID)
                    txtstatus_description1s.Text = lblstatus_description1s.Text = item.LabelName;
                else if (lbldescription1s.ID == item.LabelID)
                    txtdescription1s.Text = lbldescription1s.Text = item.LabelName;
                else if (lblaccount_description1s.ID == item.LabelID)
                    txtaccount_description1s.Text = lblaccount_description1s.Text = item.LabelName;
                //else if (lblteam_name1s.ID == item.LabelID)
                //    txtteam_name1s.Text = lblteam_name1s.Text = item.LabelName;
                else if (lbllead_source_description1s.ID == item.LabelID)
                    txtlead_source_description1s.Text = lbllead_source_description1s.Text = item.LabelName;
                //else if (lblassigned_to_name1s.ID == item.LabelID)
                //    txtassigned_to_name1s.Text = lblassigned_to_name1s.Text = item.LabelName;
                //else if (lblcreated_by1s.ID == item.LabelID)
                //    txtcreated_by1s.Text = lblcreated_by1s.Text = item.LabelName;
                //else if (lblmodified_by1s.ID == item.LabelID)
                //    txtmodified_by1s.Text = lblmodified_by1s.Text = item.LabelName;
                //else if (lblActive1s.ID == item.LabelID)
                //    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    txtDeleted1s.Text = lblDeleted1s.Text = item.LabelName;
                else if (lblWebsite1s.ID == item.LabelID)
                    txtWebsite1s.Text = lblWebsite1s.Text = item.LabelName;
                else if (lblSMS_Opt_In1s.ID == item.LabelID)
                    txtSMS_Opt_In1s.Text = lblSMS_Opt_In1s.Text = item.LabelName;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                //else if (lblTenantID2h.ID == item.LabelID)
                //    txtTenantID2h.Text = lblTenantID2h.Text = item.LabelName;
                else if (lblID2h.ID == item.LabelID)
                    txtID2h.Text = lblID2h.Text = item.LabelName;
                else if (lblConact_id2h.ID == item.LabelID)
                    txtConact_id2h.Text = lblConact_id2h.Text = item.LabelName;
                else if (lblCustomer_id2h.ID == item.LabelID)
                    txtCustomer_id2h.Text = lblCustomer_id2h.Text = item.LabelName;
                else if (lblSupplier_id2h.ID == item.LabelID)
                    txtSupplier_id2h.Text = lblSupplier_id2h.Text = item.LabelName;
                else if (lblProject_id2h.ID == item.LabelID)
                    txtProject_id2h.Text = lblProject_id2h.Text = item.LabelName;
                else if (lblSalutation2h.ID == item.LabelID)
                    txtSalutation2h.Text = lblSalutation2h.Text = item.LabelName;
                else if (lblTitle2h.ID == item.LabelID)
                    txtTitle2h.Text = lblTitle2h.Text = item.LabelName;
                else if (lblTwitterScreenName2h.ID == item.LabelID)
                    txtTwitterScreenName2h.Text = lblTwitterScreenName2h.Text = item.LabelName;
                else if (lblRefered_by2h.ID == item.LabelID)
                    txtRefered_by2h.Text = lblRefered_by2h.Text = item.LabelName;
                else if (lbllead_source2h.ID == item.LabelID)
                    txtlead_source2h.Text = lbllead_source2h.Text = item.LabelName;
                else if (lblStatus2h.ID == item.LabelID)
                    txtStatus2h.Text = lblStatus2h.Text = item.LabelName;
                else if (lblDepartment2h.ID == item.LabelID)
                    txtDepartment2h.Text = lblDepartment2h.Text = item.LabelName;
                else if (lblDo_not_call2h.ID == item.LabelID)
                    txtDo_not_call2h.Text = lblDo_not_call2h.Text = item.LabelName;
                else if (lblAssistant2h.ID == item.LabelID)
                    txtAssistant2h.Text = lblAssistant2h.Text = item.LabelName;
                else if (lblAssistant_phone2h.ID == item.LabelID)
                    txtAssistant_phone2h.Text = lblAssistant_phone2h.Text = item.LabelName;
                else if (lblemail_opt_out2h.ID == item.LabelID)
                    txtemail_opt_out2h.Text = lblemail_opt_out2h.Text = item.LabelName;
                else if (lblinvalid_email2h.ID == item.LabelID)
                    txtinvalid_email2h.Text = lblinvalid_email2h.Text = item.LabelName;
                else if (lblaccount_name2h.ID == item.LabelID)
                    txtaccount_name2h.Text = lblaccount_name2h.Text = item.LabelName;
                else if (lblopportunity_name2h.ID == item.LabelID)
                    txtopportunity_name2h.Text = lblopportunity_name2h.Text = item.LabelName;
                else if (lblopportunity_amount2h.ID == item.LabelID)
                    txtopportunity_amount2h.Text = lblopportunity_amount2h.Text = item.LabelName;
                else if (lblcampaign_name2h.ID == item.LabelID)
                    txtcampaign_name2h.Text = lblcampaign_name2h.Text = item.LabelName;
                else if (lbldate_entered2h.ID == item.LabelID)
                    txtdate_entered2h.Text = lbldate_entered2h.Text = item.LabelName;
                //else if (lbldate_modified2h.ID == item.LabelID)
                //    txtdate_modified2h.Text = lbldate_modified2h.Text = item.LabelName;
                else if (lblstatus_description2h.ID == item.LabelID)
                    txtstatus_description2h.Text = lblstatus_description2h.Text = item.LabelName;
                else if (lbldescription2h.ID == item.LabelID)
                    txtdescription2h.Text = lbldescription2h.Text = item.LabelName;
                else if (lblaccount_description2h.ID == item.LabelID)
                    txtaccount_description2h.Text = lblaccount_description2h.Text = item.LabelName;
                //else if (lblteam_name2h.ID == item.LabelID)
                //    txtteam_name2h.Text = lblteam_name2h.Text = item.LabelName;
                else if (lbllead_source_description2h.ID == item.LabelID)
                    txtlead_source_description2h.Text = lbllead_source_description2h.Text = item.LabelName;
                //else if (lblassigned_to_name2h.ID == item.LabelID)
                //    txtassigned_to_name2h.Text = lblassigned_to_name2h.Text = item.LabelName;
                //else if (lblcreated_by2h.ID == item.LabelID)
                //    txtcreated_by2h.Text = lblcreated_by2h.Text = item.LabelName;
                //else if (lblmodified_by2h.ID == item.LabelID)
                //    txtmodified_by2h.Text = lblmodified_by2h.Text = item.LabelName;
                //else if (lblActive2h.ID == item.LabelID)
                //    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    txtDeleted2h.Text = lblDeleted2h.Text = item.LabelName;
                else if (lblWebsite2h.ID == item.LabelID)
                    txtWebsite2h.Text = lblWebsite2h.Text = item.LabelName;
                else if (lblSMS_Opt_In2h.ID == item.LabelID)
                    txtSMS_Opt_In2h.Text = lblSMS_Opt_In2h.Text = item.LabelName;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((Web.CRM.CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Lead_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\CRM_tbl_Lead_Mst.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Lead_Mst").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenantID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID1s.Text;
                if (lblID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtID1s.Text;
                else if (lblConact_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConact_id1s.Text;
                else if (lblCustomer_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomer_id1s.Text;
                else if (lblSupplier_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplier_id1s.Text;
                else if (lblProject_id1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProject_id1s.Text;
                else if (lblSalutation1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalutation1s.Text;
                else if (lblTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle1s.Text;
                else if (lblTwitterScreenName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTwitterScreenName1s.Text;
                else if (lblRefered_by1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRefered_by1s.Text;
                else if (lbllead_source1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlead_source1s.Text;
                else if (lblStatus1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus1s.Text;
                else if (lblDepartment1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDepartment1s.Text;
                else if (lblDo_not_call1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDo_not_call1s.Text;
                else if (lblAssistant1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssistant1s.Text;
                else if (lblAssistant_phone1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssistant_phone1s.Text;
                else if (lblemail_opt_out1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtemail_opt_out1s.Text;
                else if (lblinvalid_email1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtinvalid_email1s.Text;
                else if (lblaccount_name1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount_name1s.Text;
                else if (lblopportunity_name1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtopportunity_name1s.Text;
                else if (lblopportunity_amount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtopportunity_amount1s.Text;
                else if (lblcampaign_name1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtcampaign_name1s.Text;
                else if (lbldate_entered1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtdate_entered1s.Text;
                //else if (lbldate_modified1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtdate_modified1s.Text;
                else if (lblstatus_description1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtstatus_description1s.Text;
                else if (lbldescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtdescription1s.Text;
                else if (lblaccount_description1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount_description1s.Text;
                //else if (lblteam_name1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtteam_name1s.Text;
                else if (lbllead_source_description1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlead_source_description1s.Text;
                //else if (lblassigned_to_name1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtassigned_to_name1s.Text;
                //else if (lblcreated_by1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcreated_by1s.Text;
                //else if (lblmodified_by1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtmodified_by1s.Text;
                //else if (lblActive1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;
                else if (lblWebsite1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWebsite1s.Text;
                else if (lblSMS_Opt_In1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSMS_Opt_In1s.Text;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                //else if (lblTenantID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID2h.Text;
                else if (lblID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtID2h.Text;
                else if (lblConact_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConact_id2h.Text;
                else if (lblCustomer_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomer_id2h.Text;
                else if (lblSupplier_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplier_id2h.Text;
                else if (lblProject_id2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProject_id2h.Text;
                else if (lblSalutation2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalutation2h.Text;
                else if (lblTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2h.Text;
                else if (lblTwitterScreenName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTwitterScreenName2h.Text;
                else if (lblRefered_by2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRefered_by2h.Text;
                else if (lbllead_source2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlead_source2h.Text;
                else if (lblStatus2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus2h.Text;
                else if (lblDepartment2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDepartment2h.Text;
                else if (lblDo_not_call2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDo_not_call2h.Text;
                else if (lblAssistant2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssistant2h.Text;
                else if (lblAssistant_phone2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssistant_phone2h.Text;
                else if (lblemail_opt_out2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtemail_opt_out2h.Text;
                else if (lblinvalid_email2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtinvalid_email2h.Text;
                else if (lblaccount_name2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount_name2h.Text;
                else if (lblopportunity_name2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtopportunity_name2h.Text;
                else if (lblopportunity_amount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtopportunity_amount2h.Text;
                else if (lblcampaign_name2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtcampaign_name2h.Text;
                else if (lbldate_entered2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtdate_entered2h.Text;
                //else if (lbldate_modified2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtdate_modified2h.Text;
                else if (lblstatus_description2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtstatus_description2h.Text;
                else if (lbldescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtdescription2h.Text;
                else if (lblaccount_description2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount_description2h.Text;
                //else if (lblteam_name2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtteam_name2h.Text;
                else if (lbllead_source_description2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlead_source_description2h.Text;
                //else if (lblassigned_to_name2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtassigned_to_name2h.Text;
                //else if (lblcreated_by2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcreated_by2h.Text;
                //else if (lblmodified_by2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtmodified_by2h.Text;
                //else if (lblActive2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;
                else if (lblWebsite2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWebsite2h.Text;
                else if (lblSMS_Opt_In2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSMS_Opt_In2h.Text;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\CRM_tbl_Lead_Mst.xml"));

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
            txtlead1.Enabled = true;
            txtLeadName2.Enabled = true;
            txtLeadName3.Enabled = true;
            DrpQuestion.Enabled = true;
            DrpSearchTitle.Enabled = true;
            txtinvalid_email.Enabled = true;
            txtID.Enabled = true;
            txtContactName.Enabled = true;
            txtAddress.Enabled = true;
            txtLeadSourcefrom.Enabled = true;
            txtWebsite.Enabled = true;
            drpStatus.Enabled = true;
            drpDepartment.Enabled = true;
            drpTeamName.Enabled = true;
            DrpAssignedTeamLeader.Enabled = true;
            txtsercustomer.Enabled = true;
            txtsupplier.Enabled = true;
            DrpOppertunityName.Enabled = true;
            drpProject_id.Enabled = true;
            Drpcampaign_name.Enabled = true;
            DDLComapny.Enabled = true;
            DDLLeadSource.Enabled = true;
            DrpSalutation.Enabled = true;
            txtTitle.Enabled = true;
            txtNoofEmployees.Enabled = true;
            txtTwitterScreenName.Enabled = true;
            txtRefered_by.Enabled = true;
            txtAssistant.Enabled = true;
            txtAssistant_phone.Enabled = true;
            cbemail_opt_out.Enabled = true;
            txtaccount_name.Enabled = true;
            txtopportunity_amount.Enabled = true;
            txtdate_entered.Enabled = true;
            txtstatus_description.Enabled = true;
            txtdescription.Enabled = true;
            txtaccount_description.Enabled = true;
            Chdonotcall.Enabled = true;
            txtlead_source_description.Enabled = true;
            cbSMS_Opt_In.Enabled = true;
            txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtlead1.Enabled = false;
            txtLeadName2.Enabled = false;
            txtLeadName3.Enabled = false;
            DrpQuestion.Enabled = false;
            DrpSearchTitle.Enabled = false;
            txtinvalid_email.Enabled = false;
            txtID.Enabled = false;
            txtContactName.Enabled = false;
            txtAddress.Enabled = false;
            txtLeadSourcefrom.Enabled = false;
            txtWebsite.Enabled = false;
            drpStatus.Enabled = false;
            drpDepartment.Enabled = false;

            drpTeamName.Enabled = false;
            DrpAssignedTeamLeader.Enabled = false;

            txtsercustomer.Enabled = false;
            txtsupplier.Enabled = false;
            drpProject_id.Enabled = false;
            Drpcampaign_name.Enabled = false;
            DDLComapny.Enabled = false;
            DrpOppertunityName.Enabled = false;
            DrpSalutation.Enabled = false;
            txtTitle.Enabled = false;
            txtTwitterScreenName.Enabled = false;
            txtRefered_by.Enabled = false;
            DDLLeadSource.Enabled = false;
            txtAssistant.Enabled = false;
            txtAssistant_phone.Enabled = false;
            Chdonotcall.Enabled = false;
            cbemail_opt_out.Enabled = false;
            txtaccount_name.Enabled = false;
            txtopportunity_amount.Enabled = false;
            txtdate_entered.Enabled = false;
            txtstatus_description.Enabled = false;
            txtdescription.Enabled = false;
            txtaccount_description.Enabled = false;
            txtlead_source_description.Enabled = false;
            cbSMS_Opt_In.Enabled = false;
            txtCRUP_ID.Enabled = false;
            txtNoofEmployees.Enabled = false;

        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Lead_Mst.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name != null && p.Supplier_Name != null && p.Project_id != null && p.Active == true).OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tbl_Lead_Mst.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name != null && p.Supplier_Name != null && p.Project_id != null && p.Active == true).OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tbl_Lead_Mst.Count();
                take = Showdata;
                Skip = 0;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name != null && p.Supplier_Name != null && p.Project_id != null && p.Active == true).OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tbl_Lead_Mst.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Lead_Mst.Where(p => p.TenentID == TID && p.Customer_Name != null && p.Supplier_Name != null && p.Project_id != null && p.Active == true).OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
            //using(TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                Database.tbl_Lead_Mst objSOJobDesc = DB.tbl_Lead_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                objSOJobDesc.Active = false;
                DB.SaveChanges();
                BindData();

                //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                //((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Lead_Mst.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
            }
            if (e.CommandName == "btnView")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                BindCommand(ID);
                Readonly();

                Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                DivMain.Attributes["style"] = "display: none;";

            }
            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                BindCommand(ID);
                Write();
                btnAdd.Text = "Update";
                pnlSearchbutton.Visible = true;
                ViewState["Edit"] = ID;

                Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                DivMain.Attributes["style"] = "display: block;";
                btnAdd.ValidationGroup = "submit";
            }
            // scope.Complete(); //  To commit.
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //    throw;
            //}
        }

        public void BindCommand(int ID)
        {
            //paneloppref.Visible = false;
            //pnlresponsive.Visible = false;

            //string str1 = ID[0].ToString();
            //string str2 = ID[1].ToString();
            //   Session["CampaignID"] = ID;

            Database.tbl_Lead_Mst objtbl_Lead_Mst = DB.tbl_Lead_Mst.Single(p => p.ID == ID && p.TenentID == TID);

            btnAdd.ValidationGroup = "s";
            txtsercustomer.Text = objtbl_Lead_Mst.Customer_Name;
            txtsupplier.Text = objtbl_Lead_Mst.Supplier_Name;
            if (objtbl_Lead_Mst.Project_id != null && objtbl_Lead_Mst.Project_id != 0)
            {
                drpProject_id.SelectedValue = objtbl_Lead_Mst.Project_id.ToString();
            }
            if (objtbl_Lead_Mst.Campaign_ID != null)
            {
                Drpcampaign_name.SelectedValue = objtbl_Lead_Mst.Campaign_ID.ToString();
            }
            if (objtbl_Lead_Mst.Oppertunity_ID != null)
            {
                DrpOppertunityName.SelectedValue = objtbl_Lead_Mst.Oppertunity_ID.ToString();
            }
            Chdonotcall.Checked = (objtbl_Lead_Mst.Do_not_call == true) ? true : false;
            cbemail_opt_out.Checked = (objtbl_Lead_Mst.email_opt_out == true) ? true : false;
            cbSMS_Opt_In.Checked = (objtbl_Lead_Mst.SMS_Opt_In == true) ? true : false;
            if (objtbl_Lead_Mst.LeadSourcefrom != null)
            {
                txtLeadSourcefrom.Text = objtbl_Lead_Mst.LeadSourcefrom.ToString();
            }
            //if (objtbl_Lead_Mst.TeamLeaderName != null)
            //{
            //    txtteam_name.Text = objtbl_Lead_Mst.TeamLeaderName.ToString();
            //}


            if (objtbl_Lead_Mst.team_name != null)
            {
                int RID = Convert.ToInt32(objtbl_Lead_Mst.team_name);
                drpTeamName.SelectedValue = objtbl_Lead_Mst.team_name.ToString();
                BindTimeAssin(RID);
            }
            if (objtbl_Lead_Mst.TeamLeaderName != null)
            {

                DrpAssignedTeamLeader.SelectedValue = objtbl_Lead_Mst.TeamLeaderName.ToString();

            }

            if (objtbl_Lead_Mst.Salutation != null)
            {
                DrpSalutation.SelectedValue = objtbl_Lead_Mst.Salutation.ToString();
            }
            if (objtbl_Lead_Mst.Title != null)
            {

                txtTitle.Text = objtbl_Lead_Mst.Title.ToString();
            }
            if (objtbl_Lead_Mst.TwitterScreenName != null)
            {
                txtTwitterScreenName.Text = objtbl_Lead_Mst.TwitterScreenName.ToString();
            }
            if (objtbl_Lead_Mst.Refered_by != null)
            {
                txtRefered_by.Text = objtbl_Lead_Mst.Refered_by.ToString();
            }
            if (objtbl_Lead_Mst.Company_ID != null)
            {
                DDLComapny.SelectedValue = objtbl_Lead_Mst.Company_ID.ToString();
            }
            if (objtbl_Lead_Mst.lead_source != null)
            {
                DDLLeadSource.SelectedValue = objtbl_Lead_Mst.lead_source.ToString();
            }
            //if (objtbl_Lead_Mst.RefNO != null)
            //{
            //    drpRefNo123.SelectedValue = objtbl_Lead_Mst.RefNO.ToString();
            //}
            if (objtbl_Lead_Mst.QuestionGroup != null)
            {
                DrpQuestion.SelectedValue = objtbl_Lead_Mst.QuestionGroup.ToString();
            }
            if (objtbl_Lead_Mst.SearchTitle != null)
            {
                DrpSearchTitle.SelectedValue = objtbl_Lead_Mst.SearchTitle.ToString();
            }
            txtAssistant.Text = objtbl_Lead_Mst.Assistant;
            txtAssistant_phone.Text = objtbl_Lead_Mst.Assistant_phone;
            txtaccount_name.Text = objtbl_Lead_Mst.account_name;
            txtopportunity_amount.Text = objtbl_Lead_Mst.opportunity_amount.ToString();
            txtdate_entered.Text = Convert.ToDateTime(objtbl_Lead_Mst.date_entered).ToString("MM/dd/yyyy");
            //txtdate_entered.Text = objtbl_Lead_Mst.date_entered.ToString();
            txtstatus_description.Text = objtbl_Lead_Mst.status_description;
            txtdescription.Text = objtbl_Lead_Mst.description;
            txtaccount_description.Text = objtbl_Lead_Mst.account_description;
            txtlead_source_description.Text = objtbl_Lead_Mst.lead_source_description;
            txtCRUP_ID.Text = objtbl_Lead_Mst.CRUP_ID.ToString();
            txtNoofEmployees.Text = objtbl_Lead_Mst.NoofEmployees;
            txtlead1.Text = objtbl_Lead_Mst.LeadName1;
            txtLeadName2.Text = objtbl_Lead_Mst.LeadName2;
            txtLeadName3.Text = objtbl_Lead_Mst.LeadName3;
            txtinvalid_email.Text = objtbl_Lead_Mst.Email;
            txtID.Text = objtbl_Lead_Mst.Name;
            txtContactName.Text = objtbl_Lead_Mst.ContactPerson;
            txtAddress.Text = objtbl_Lead_Mst.Address;
            txtWebsite.Text = objtbl_Lead_Mst.Website;
            if (objtbl_Lead_Mst.Status != null)
            {
                drpStatus.SelectedValue = objtbl_Lead_Mst.Status.ToString();
            }
            if (objtbl_Lead_Mst.PerformingDept != null)
            {
                drpDepartment.SelectedValue = objtbl_Lead_Mst.PerformingDept.ToString();
            }

            //objtbl_Lead_Mst.TeamLeaderName = txtteam_name.Text;


        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Lead_Mst.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Lead_Mst.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
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

        public string GetCustomerName(int CustoID = 0)
        {
            string con = "";
            if (CustoID != 0)
            {
                int SID = Convert.ToInt32(CustoID);
                List<TBLCONTACT> List = DB.TBLCONTACTs.Where(p => p.TenentID == TID && p.ContactMyID == SID && p.Active == "Y" && p.PHYSICALLOCID != "HLY").ToList();
                if (List.Count() > 0)
                    con = List.Single(p => p.TenentID == TID && p.ContactMyID == SID && p.Active == "Y" && p.PHYSICALLOCID != "HLY").PersName1;


            }
            return con;

        }
        #endregion
        protected void Drpcampaign_name_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Drpcampaign_name.SelectedValue) != 0)
            {
                int CampainID = Convert.ToInt32(Drpcampaign_name.SelectedValue);
                string CID = CampainID.ToString();

                Classes.EcommAdminClass.getdropdown(DrpOppertunityName, TID, CID, "", "", "tbl_Opportunity_Mst");
                //select * from tbl_Opportunity_Mst where Active= 'true'

                //DrpOppertunityName.DataSource = DB.tbl_Opportunity_Mst.Where(p => p.TenentID == TID && p.Campaign_ID == CampainID && p.Deleted == true && p.Active == true).ToList();
                //DrpOppertunityName.DataTextField = "Name";
                //DrpOppertunityName.DataValueField = "OpportID";
                //DrpOppertunityName.DataBind();
                //DrpOppertunityName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                DrpOppertunityName.Items.Clear();
                DrpOppertunityName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        protected void ListViewConatctname_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                TBLCONTACT obj_Contact = DB.TBLCONTACTs.SingleOrDefault(p => p.ContactMyID == ID && p.TenentID == TID);
                txtContactName.Text = obj_Contact.PersName1;

            }
        }

        protected void lbButton1_Click(object sender, EventArgs e)
        {

        }


        protected void btnsavenote_Click(object sender, EventArgs e)
        {


        }



        protected void btnsavefile_Click(object sender, EventArgs e)
        {
            // pnlfile.Visible = true;
        }


        protected void btnAddnewFile_Click(object sender, EventArgs e)
        {
            //  ISActionMaster obj_Action = new ISActionMaster();
            //  string FileName = "";
            //  string path = "";
            //  if (fileupload.HasFile)
            //  {
            //      FileName = fileupload.FileName;
            // fileupload.SaveAs(Server.MapPath("~/Upload/" + FileName));
            //  }
            ////  obj_Action.FileName = FileName;
            //  int TID2 = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //  int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            //  obj_Action.ID = DB.ISActionMasters.Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Max(p => p.ID) + 1) : 1;
            //  obj_Action.TenantedID = TID2;
            //  obj_Action.Type = Convert.ToInt32(ActionMaster.Type.Lead);
            //  obj_Action.ActionType = Convert.ToInt32(ActionMaster.ActionType.File);
            //  obj_Action.CreatedDate = DateTime.Now;
            //  obj_Action.Active = true;
            //  obj_Action.Deleted = true;

            //  obj_Action.Enteredby = UID;
            //  //obj_Action.CrupID=
            //  DB.ISActionMasters.AddObject(obj_Action);
            //  DB.SaveChanges();
            //  BindFile();
            //  string display = "File Upload Successfully..";
            //  ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
        }

        public void BindReference()
        {
            //drpRefNo123.DataSource = DB1.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "Lead" && P.TenentID == TID && P.SWITCH2 == "02").OrderBy(a => a.REFNAME1);
            //drpRefNo123.DataTextField = "REFNAME1";
            //drpRefNo123.DataValueField = "REFID";
            //drpRefNo123.DataBind();
            //drpRefNo123.Items.Insert(0, new ListItem("-- Select --", "0"));

            //  Classes.CRMClass.getcrmdropdown(drpRefNo123, TID, "CRM", "Lead", "02", "REFTABLE");
        }

        public void BindData1()
        {
            Listview1.DataSource = DB.tbl_Task.Where(p => p.TenentID == TID && p.Active == true);
            Listview1.DataBind();
        }


        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    var exist = DB1.REFTABLE.Where(c => c.REFNAME1 == txtAddReference3.Text && c.REFSUBTYPE == "Lead");
        //    if (exist.Count() <= 0)
        //    {
        //        Database.REFTABLE objtbl_REFTABLE = new Database.REFTABLE();
        //        int RefID = DB1.REFTABLE.Count() > 0 ? Convert.ToInt32(DB1.REFTABLE.Max(p => p.REFID) + 1) : 1;
        //        objtbl_REFTABLE.TenentID = TID;
        //        objtbl_REFTABLE.REFID = RefID;//DB.REFTABLE.Count() > 0 ? Convert.ToInt32(DB.REFTABLE.Max(p => p.REFID) + 1) : 1;
        //        objtbl_REFTABLE.REFSUBTYPE = "Lead";
        //        objtbl_REFTABLE.REFTYPE = "CRM";
        //        objtbl_REFTABLE.SHORTNAME = txtShortName1.Text;
        //        objtbl_REFTABLE.REFNAME1 = txtAddReference3.Text;
        //        objtbl_REFTABLE.REFNAME2 = txtAddReference3.Text;
        //        objtbl_REFTABLE.REFNAME3 = txtAddReference3.Text;
        //        objtbl_REFTABLE.Remarks = txtREMARK2.Text;
        //        objtbl_REFTABLE.ACTIVE = "Y";
        //        //if (Convert.ToInt32(drpRefNo123.SelectedValue)!=0)
        //        //{
        //        //int ID = Convert.ToInt32(drpRefNo123.SelectedValue);//Convert.ToInt32(ViewState["CampaignID"]);
        //        // objtbl_REFTABLE.SWITCH3 = "220003Camp" + ID;
        //        // }
        //        //else
        //        //{
        //        //    ClientScript.RegisterStartupScript(this.GetType(), "User Name Is Duplicate!", "alert('Must Enter Ref Name');", true);
        //        //    //objtbl_REFTABLE.SWITCH3 = "220003OPP";
        //        //}
        //        objtbl_REFTABLE.SWITCH2 = "02";
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
            string ACTIVITYE = "Lead";
            string ACTIVITYA = "Lead";
            string ACTIVITYA2 = "CRM";
            bool AMIGLOBAL = true;
            bool MYPERSONNEL = true;
            int INTERVALDAYS = 3;
            bool REPEATFOREVER = true;
            DateTime REPEATTILL = DateTime.Now;
            string REMINDERNOTE = "Youer Lead is Pading";
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
            string BName = "Call Custmer";
            string P2 = "aa";
            string P3 = "aa";
            inserCrmproghw(TID, SID, BName, P2, P3, TransNo);

        }

        public void inserCrmproghw(int TID, int SID, string BName, string P2, string P3, int TRction)
        {
            // ACM_CRMMainActivities obj = DB1.ACM_CRMMainActivities.Single(p => p.TenentID == TID  && p.ID == TRction);
            int ACID = Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.MyLineNo));
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
        //    Database.CRMMainActivity objtbl_CRMMainActivity = new Database.CRMMainActivity();

        //    objtbl_CRMMainActivity.TenentID = TID;
        //    objtbl_CRMMainActivity.COMPID = Convert.ToInt32(1);
        //    objtbl_CRMMainActivity.RouteID = 1;
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
        //    DB.CRMMainActivities.AddObject(objtbl_CRMMainActivity);
        //    // ((DMSMaster)Page.Master).UpdateLog("From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), objtbl_CRMMainActivity.CRUP_ID,"CRMMainActivity", UID);
        //    DB.SaveChanges();

        //}
        //public void CRMActivity(int TID, int Reference)//loop rout
        //{
        //    string strStatus = "";
        //    if (ViewState["MyStatus"] != null)
        //    {
        //        strStatus = ViewState["MyStatus"].ToString();
        //    }

        //    int REFIDFromNewDOC = 0;
        //    string Type = Convert.ToString(ActionMaster.Type.Campaign);
        //    Database.CRMActivity objtbl_CRMActivity = new Database.CRMActivity();
        //    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
        //    int refID = Convert.ToInt32(Reference);// Convert.ToInt32(CommonList[4]);
        //    //string RefName = DB.REFTABLE.Single(p => p.REFID == refID).REFNAME1.ToString();


        //    objtbl_CRMActivity.TenentID = TID;
        //    objtbl_CRMActivity.COMPID = 0;

        //    // objtbl_CRMActivity.ActivityCompID = item.CompIDSEqNo;
        //    //objtbl_CRMActivity.DocID = DocID;

        //    objtbl_CRMActivity.ACTIVITYTYPE = "CRM";
        //    //objtbl_CRMActivity.REFTYPE = Convert.ToString(ActionMaster.Type.Lead);
        //    //  objtbl_CRMActivity.REFSUBTYPE = Convert.ToString(ActionMaster.Type.Lead);
        //    objtbl_CRMActivity.RouteID = "";
        //    // objtbl_CRMActivity.MyLineNo = item.LineNum;
        //    objtbl_CRMActivity.USERNAME = UID;
        //    objtbl_CRMActivity.NextUser = "1";

        //    objtbl_CRMActivity.NextRefNo = Reference.ToString(); ;//= DOCUMENT NAME DONE
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

        //    objtbl_CRMActivity.MyStatus = strStatus;
        //    DateTime CDate = DateTime.Now;
        //    // int NoOFDay = item.Days2Complete;
        //    //   CDate = CDate.AddDays(NoOFDay);
        //    objtbl_CRMActivity.DeadLineDate = CDate;

        //    objtbl_CRMActivity.REFTYPE = "test1";
        //    objtbl_CRMActivity.PerfReferenceNo = "test2";
        //    objtbl_CRMActivity.REFSUBTYPE = "test3";

        //    //objtbl_CRMActivity.Active = item.ActiveCRM;

        //    //objtbl_CRMActivity.Active = "F";

        //    //  objtbl_CRMActivity.GroupBy = item.REFID.ToString() + RouteName + item.SeqNo + refID;
        //    objtbl_CRMActivity.Type = 1;
        //    // objtbl_CRMActivity.FromColumn = Convert.ToInt32(item.FromColumn);
        //    // objtbl_CRMActivity.ToColumn = Convert.ToInt32(item.ToColumn);
        //    DB.CRMActivities.AddObject(objtbl_CRMActivity);
        //    DB.SaveChanges();

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
            return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == CID && p.TenentID == TID).COMPNAME1;
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
                Response.Redirect("SearchTitalPage.aspx?TID=" + TitleID + "&CampID=" + CamID + "&LeadName=" + txtlead1.Text + "&GrupID=" + GrupID);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Select Title ...');", true);
            }
        }



        protected void drpTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {

            int RID = Convert.ToInt32(drpTeamName.SelectedValue);
            BindTimeAssin(RID);
        }
        public void BindTimeAssin(int ID)
        {
            DrpAssignedTeamLeader.DataSource = DB.TeamNames.Where(p => p.TenentID == TID && p.REF_ID == ID);
            DrpAssignedTeamLeader.DataValueField = "TeamID";
            DrpAssignedTeamLeader.DataTextField = "MemberName";
            DrpAssignedTeamLeader.DataBind();
            DrpAssignedTeamLeader.Items.Insert(0, new ListItem("-- Select --", "0"));
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
        //    ModalPopupExtender2.Show();
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
        //    ModalPopupExtender2.Show();
        //    ViewState["Supplery"] = 1;
        //}
    }
}