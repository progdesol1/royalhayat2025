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
    public partial class Account_Mst : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
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
                // FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            var List = DB.tbl_Account_Mst.Where(p => p.Active == true && p.TenentID == TID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((Web.CRM.CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

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
        public void FistTimeLoad()
        {            
            FirstFlag = false;           
        }
        #region PAge Genarator
        

        public void GetShow()
        {

            lblName1s.Attributes["class"] = lblPhone_Office1s.Attributes["class"] = lblPhone1s.Attributes["class"] = lblPhone_Fax1s.Attributes["class"] = lblPhone_Alternate1s.Attributes["class"] = lblWebsite1s.Attributes["class"] = lblEmail11s.Attributes["class"] = lblEmail21s.Attributes["class"] = lblAnnual_Revenue1s.Attributes["class"] = lblEmployee1s.Attributes["class"] = lblIndustryID1s.Attributes["class"] = lblOwnership1s.Attributes["class"] = lblAccountType1s.Attributes["class"] = lblTickerSymbol1s.Attributes["class"] = lblRating1s.Attributes["class"] = lblSicCode1s.Attributes["class"] = lblBilling_Address_Street1s.Attributes["class"] = lblBilling_Address_City1s.Attributes["class"] = lblBilling_Address_State1s.Attributes["class"] = lblBilling_Address_Postalcode1s.Attributes["class"] = lblBilling_Address_Country1s.Attributes["class"] = lblShipping_Address_Street1s.Attributes["class"] = lblShipping_Address_City1s.Attributes["class"] = lblShipping_Address_State1s.Attributes["class"] = lblShipping_Address_Postalcode1s.Attributes["class"] = lblShipping_Address_Country1s.Attributes["class"] = lblParentName1s.Attributes["class"] = lblDateEntered1s.Attributes["class"] = lblDateModified1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblTeamName1s.Attributes["class"] = lblAssigned_to_Name1s.Attributes["class"] = lblCreatedBy1s.Attributes["class"] = lblModifiedBy1s.Attributes["class"] = lblContactName1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblName2h.Attributes["class"] = lblPhone_Office2h.Attributes["class"] = lblPhone2h.Attributes["class"] = lblPhone_Fax2h.Attributes["class"] = lblPhone_Alternate2h.Attributes["class"] = lblWebsite2h.Attributes["class"] = lblEmail12h.Attributes["class"] = lblEmail22h.Attributes["class"] = lblAnnual_Revenue2h.Attributes["class"] = lblEmployee2h.Attributes["class"] = lblIndustryID2h.Attributes["class"] = lblOwnership2h.Attributes["class"] = lblAccountType2h.Attributes["class"] = lblTickerSymbol2h.Attributes["class"] = lblRating2h.Attributes["class"] = lblSicCode2h.Attributes["class"] = lblBilling_Address_Street2h.Attributes["class"] = lblBilling_Address_City2h.Attributes["class"] = lblBilling_Address_State2h.Attributes["class"] = lblBilling_Address_Postalcode2h.Attributes["class"] = lblBilling_Address_Country2h.Attributes["class"] = lblShipping_Address_Street2h.Attributes["class"] = lblShipping_Address_City2h.Attributes["class"] = lblShipping_Address_State2h.Attributes["class"] = lblShipping_Address_Postalcode2h.Attributes["class"] = lblShipping_Address_Country2h.Attributes["class"] = lblParentName2h.Attributes["class"] = lblDateEntered2h.Attributes["class"] = lblDateModified2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblTeamName2h.Attributes["class"] = lblAssigned_to_Name2h.Attributes["class"] = lblCreatedBy2h.Attributes["class"] = lblModifiedBy2h.Attributes["class"] = lblContactName2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblName1s.Attributes["class"] = lblPhone_Office1s.Attributes["class"] = lblPhone1s.Attributes["class"] = lblPhone_Fax1s.Attributes["class"] = lblPhone_Alternate1s.Attributes["class"] = lblWebsite1s.Attributes["class"] = lblEmail11s.Attributes["class"] = lblEmail21s.Attributes["class"] = lblAnnual_Revenue1s.Attributes["class"] = lblEmployee1s.Attributes["class"] = lblIndustryID1s.Attributes["class"] = lblOwnership1s.Attributes["class"] = lblAccountType1s.Attributes["class"] = lblTickerSymbol1s.Attributes["class"] = lblRating1s.Attributes["class"] = lblSicCode1s.Attributes["class"] = lblBilling_Address_Street1s.Attributes["class"] = lblBilling_Address_City1s.Attributes["class"] = lblBilling_Address_State1s.Attributes["class"] = lblBilling_Address_Postalcode1s.Attributes["class"] = lblBilling_Address_Country1s.Attributes["class"] = lblShipping_Address_Street1s.Attributes["class"] = lblShipping_Address_City1s.Attributes["class"] = lblShipping_Address_State1s.Attributes["class"] = lblShipping_Address_Postalcode1s.Attributes["class"] = lblShipping_Address_Country1s.Attributes["class"] = lblParentName1s.Attributes["class"] = lblDateEntered1s.Attributes["class"] = lblDateModified1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblTeamName1s.Attributes["class"] = lblAssigned_to_Name1s.Attributes["class"] = lblCreatedBy1s.Attributes["class"] = lblModifiedBy1s.Attributes["class"] = lblContactName1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblName2h.Attributes["class"] = lblPhone_Office2h.Attributes["class"] = lblPhone2h.Attributes["class"] = lblPhone_Fax2h.Attributes["class"] = lblPhone_Alternate2h.Attributes["class"] = lblWebsite2h.Attributes["class"] = lblEmail12h.Attributes["class"] = lblEmail22h.Attributes["class"] = lblAnnual_Revenue2h.Attributes["class"] = lblEmployee2h.Attributes["class"] = lblIndustryID2h.Attributes["class"] = lblOwnership2h.Attributes["class"] = lblAccountType2h.Attributes["class"] = lblTickerSymbol2h.Attributes["class"] = lblRating2h.Attributes["class"] = lblSicCode2h.Attributes["class"] = lblBilling_Address_Street2h.Attributes["class"] = lblBilling_Address_City2h.Attributes["class"] = lblBilling_Address_State2h.Attributes["class"] = lblBilling_Address_Postalcode2h.Attributes["class"] = lblBilling_Address_Country2h.Attributes["class"] = lblShipping_Address_Street2h.Attributes["class"] = lblShipping_Address_City2h.Attributes["class"] = lblShipping_Address_State2h.Attributes["class"] = lblShipping_Address_Postalcode2h.Attributes["class"] = lblShipping_Address_Country2h.Attributes["class"] = lblParentName2h.Attributes["class"] = lblDateEntered2h.Attributes["class"] = lblDateModified2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblTeamName2h.Attributes["class"] = lblAssigned_to_Name2h.Attributes["class"] = lblCreatedBy2h.Attributes["class"] = lblModifiedBy2h.Attributes["class"] = lblContactName2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //   drpID.SelectedIndex = 0;
            txtName.Text = "";
            txtPhone_Office.Text = "";
            txtPhone.Text = "";
            txtPhone_Fax.Text = "";
            txtPhone_Alternate.Text = "";
            txtWebsite.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtAnnual_Revenue.Text = "";
            txtEmployee.Text = "";
            //  drpIndustryID.SelectedIndex = 0;
            txtOwnership.Text = "";
            //   drpAccountType.SelectedIndex = 0;
            txtTickerSymbol.Text = "";
            txtRating.Text = "";
            txtSicCode.Text = "";
            txtBilling_Address_Street.Text = "";
            txtBilling_Address_City.Text = "";
            //txtBilling_Address_State.Text = "";
            txtBilling_Address_Postalcode.Text = "";
            //txtBilling_Address_Country.Text = "";
            txtShipping_Address_Street.Text = "";
            txtShipping_Address_City.Text = "";
            //txtShipping_Address_State.Text = "";
            txtShipping_Address_Postalcode.Text = "";
            //txtShipping_Address_Country.Text = "";
            txtParentName.Text = "";
            txtDateEntered.Text = "";
            txtDateModified.Text = "";
            txtDescription.Text = "";
            //  drpTeamName.SelectedIndex = 0;
            txtAssigned_to_Name.Text = "";
            //drpCreatedBy.SelectedIndex = 0;
            //drpModifiedBy.SelectedIndex = 0;
            txtContactName.Text = "";
            //txtActive.Text = "";
            //txtDeleted.Text = "";
            //txtCRUP_ID.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (var scope = new System.Transactions.TransactionScope())
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
                        Database.tbl_Account_Mst objtbl_Account_Mst = new Database.tbl_Account_Mst();
                        //Server Content Send data Yogesh
                        //    objtbl_Account_Mst.ID = Convert.ToInt32(drpID.SelectedValue);
                        objtbl_Account_Mst.Name = txtName.Text;
                        objtbl_Account_Mst.Phone_Office = txtPhone_Office.Text;
                        objtbl_Account_Mst.Phone = txtPhone.Text;
                        objtbl_Account_Mst.Phone_Fax = txtPhone_Fax.Text;
                        objtbl_Account_Mst.Phone_Alternate = txtPhone_Alternate.Text;
                        objtbl_Account_Mst.Website = txtWebsite.Text;
                        objtbl_Account_Mst.Email1 = txtEmail1.Text;
                        objtbl_Account_Mst.Email2 = txtEmail2.Text;
                        objtbl_Account_Mst.Annual_Revenue = txtAnnual_Revenue.Text;
                        objtbl_Account_Mst.Employee = txtEmployee.Text;
                        objtbl_Account_Mst.IndustryID = Convert.ToInt32(drpIndustryID.SelectedValue);
                        objtbl_Account_Mst.Ownership = txtOwnership.Text;
                        objtbl_Account_Mst.AccountType = Convert.ToInt32(drpAccountType.SelectedValue);
                        objtbl_Account_Mst.TickerSymbol = txtTickerSymbol.Text;
                        objtbl_Account_Mst.Rating = txtRating.Text;
                        objtbl_Account_Mst.SicCode = txtSicCode.Text;
                        objtbl_Account_Mst.Billing_Address_Street = txtBilling_Address_Street.Text;
                        objtbl_Account_Mst.Billing_Address_City = txtBilling_Address_City.Text;
                        objtbl_Account_Mst.Billing_Address_State = drpBilling_Address_State.SelectedValue;
                        objtbl_Account_Mst.Billing_Address_Postalcode = txtBilling_Address_Postalcode.Text;
                        objtbl_Account_Mst.Billing_Address_Country = drpBilling_Address_Country.SelectedValue;
                        objtbl_Account_Mst.Shipping_Address_Street = txtShipping_Address_Street.Text;
                        objtbl_Account_Mst.Shipping_Address_City = txtShipping_Address_City.Text;
                        objtbl_Account_Mst.Shipping_Address_State = drpShipping_Address_State.SelectedValue;
                        objtbl_Account_Mst.Shipping_Address_Postalcode = txtShipping_Address_Postalcode.Text;
                        objtbl_Account_Mst.Shipping_Address_Country = drpShipping_Address_Country.SelectedValue;
                        objtbl_Account_Mst.ParentName = txtParentName.Text;
                        objtbl_Account_Mst.DateEntered = Convert.ToDateTime(txtDateEntered.Text);
                        objtbl_Account_Mst.DateModified = Convert.ToDateTime(txtDateModified.Text);
                        objtbl_Account_Mst.Description = txtDescription.Text;
                        objtbl_Account_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                        objtbl_Account_Mst.Assigned_to_Name = txtAssigned_to_Name.Text;
                        //objtbl_Account_Mst.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                        //objtbl_Account_Mst.ModifiedBy = Convert.ToInt32(drpModifiedBy.SelectedValue);
                        objtbl_Account_Mst.ContactName = txtContactName.Text;
                        objtbl_Account_Mst.Active = cbActive.Checked;
                        objtbl_Account_Mst.Deleted = cbDeleted.Checked;
                        //objtbl_Account_Mst.CRUP_ID = txtCRUP_ID.Text;


                        DB.tbl_Account_Mst.AddObject(objtbl_Account_Mst);
                        DB.SaveChanges();

                        String url = "insert new record in tbl_Account_Mst with " + "TenentID = " + TID ;
                        String evantname = "create";
                        String tablename = "tbl_Account_Mst";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        navigation.Visible = true;
                        Readonly();
                        btnAdd.ValidationGroup = "s1";
                        //   FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            btnAdd.ValidationGroup = "s";
                            Database.tbl_Account_Mst objtbl_Account_Mst = DB.tbl_Account_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                            // objtbl_Account_Mst.ID = Convert.ToInt32(drpID.SelectedValue);
                            objtbl_Account_Mst.Name = txtName.Text;
                            objtbl_Account_Mst.Phone_Office = txtPhone_Office.Text;
                            objtbl_Account_Mst.Phone = txtPhone.Text;
                            objtbl_Account_Mst.Phone_Fax = txtPhone_Fax.Text;
                            objtbl_Account_Mst.Phone_Alternate = txtPhone_Alternate.Text;
                            objtbl_Account_Mst.Website = txtWebsite.Text;
                            objtbl_Account_Mst.Email1 = txtEmail1.Text;
                            objtbl_Account_Mst.Email2 = txtEmail2.Text;
                            objtbl_Account_Mst.Annual_Revenue = txtAnnual_Revenue.Text;
                            objtbl_Account_Mst.Employee = txtEmployee.Text;
                            objtbl_Account_Mst.IndustryID = Convert.ToInt32(drpIndustryID.SelectedValue);
                            objtbl_Account_Mst.Ownership = txtOwnership.Text;
                            objtbl_Account_Mst.AccountType = Convert.ToInt32(drpAccountType.SelectedValue);
                            objtbl_Account_Mst.TickerSymbol = txtTickerSymbol.Text;
                            objtbl_Account_Mst.Rating = txtRating.Text;
                            objtbl_Account_Mst.SicCode = txtSicCode.Text;
                            objtbl_Account_Mst.Billing_Address_Street = txtBilling_Address_Street.Text;
                            objtbl_Account_Mst.Billing_Address_City = txtBilling_Address_City.Text;
                            objtbl_Account_Mst.Billing_Address_State = drpBilling_Address_State.SelectedValue;
                            objtbl_Account_Mst.Billing_Address_Postalcode = txtBilling_Address_Postalcode.Text;
                            objtbl_Account_Mst.Billing_Address_Country = drpBilling_Address_Country.SelectedValue;
                            objtbl_Account_Mst.Shipping_Address_Street = txtShipping_Address_Street.Text;
                            objtbl_Account_Mst.Shipping_Address_City = txtShipping_Address_City.Text;
                            objtbl_Account_Mst.Shipping_Address_State = drpShipping_Address_State.SelectedValue;
                            objtbl_Account_Mst.Shipping_Address_Postalcode = txtShipping_Address_Postalcode.Text;
                            objtbl_Account_Mst.Shipping_Address_Country = drpShipping_Address_Country.SelectedValue;
                            objtbl_Account_Mst.ParentName = txtParentName.Text;
                            objtbl_Account_Mst.DateEntered = Convert.ToDateTime(txtDateEntered.Text);
                            objtbl_Account_Mst.DateModified = Convert.ToDateTime(txtDateModified.Text);
                            objtbl_Account_Mst.Description = txtDescription.Text;
                            objtbl_Account_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                            objtbl_Account_Mst.Assigned_to_Name = txtAssigned_to_Name.Text;
                            //objtbl_Account_Mst.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                            //objtbl_Account_Mst.ModifiedBy = Convert.ToInt32(drpModifiedBy.SelectedValue);
                            objtbl_Account_Mst.ContactName = txtContactName.Text;
                            objtbl_Account_Mst.Active = cbActive.Checked;
                            objtbl_Account_Mst.Deleted = cbDeleted.Checked;
                            //objtbl_Account_Mst.CRUP_ID = txtCRUP_ID.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();

                            String url = "update tbl_Account_Mst with " + "TenentID = " + TID + "ID =" + ID;
                            String evantname = "Update";
                            String tablename = "tbl_Account_Mst";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
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
            Classes.EcommAdminClass.getdropdown(drpIndustryID, TID, "INDUSTRY", "LIST", "", "REFTABLE");
            //select * from REFTABLE where Active= 'Y' and REFTYPE = 'INDUSTRY' and REFSUBTYPE = 'LIST'

            //drpIndustryID.DataSource = DB.REFTABLEs.Where(P => P.TenentID == TID && P.ACTIVE == "Y" && P.REFTYPE == "INDUSTRY" && P.REFSUBTYPE == "LIST");
            //drpIndustryID.DataTextField = "REFNAME1";
            //drpIndustryID.DataValueField = "REFID";
            //drpIndustryID.DataBind();
            //drpIndustryID.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpAccountType, TID, "CATTYPE", "CATTYPE", "", "REFTABLE");
            //select * from REFTABLE where Active= 'Y' and REFTYPE = 'CATTYPE' and REFSUBTYPE = 'CATTYPE'

            //drpAccountType.DataSource = DB.REFTABLEs.Where(P => P.TenentID == TID && P.ACTIVE == "Y" && P.REFTYPE == "CATTYPE" && P.REFSUBTYPE == "CATTYPE");
            //drpAccountType.DataTextField = "REFNAME1";
            //drpAccountType.DataValueField = "REFID";
            //drpAccountType.DataBind();
            //drpAccountType.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpTeamName, TID, "ACTVTY", "STATUS", "", "REFTABLE");
            //select * from REFTABLE where Active= 'Y' and REFTYPE = 'ACTVTY' and REFSUBTYPE = 'STATUS'

            //drpTeamName.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS");
            //drpTeamName.DataTextField = "REFNAME1";
            //drpTeamName.DataValueField = "REFID";
            //drpTeamName.DataBind();
            //drpTeamName.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpBilling_Address_Country, TID, "", "", "", "tblCOUNTRY");
            //select * from tblCOUNTRY where Active= 'Y'

            //drpBilling_Address_Country.DataSource = DB.tblCOUNTRies.Where(p => p.Active == "Y" && p.TenentID == TID);
            //drpBilling_Address_Country.DataTextField = "COUNAME1";
            //drpBilling_Address_Country.DataValueField = "COUNTRYID";
            //drpBilling_Address_Country.DataBind();
            //drpBilling_Address_Country.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpShipping_Address_Country, TID, "", "", "", "tblCOUNTRY");
            //select * from tblCOUNTRY where Active= 'Y'

            //drpShipping_Address_Country.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.Active == "Y");
            //drpShipping_Address_Country.DataTextField = "COUNAME1";
            //drpShipping_Address_Country.DataValueField = "COUNTRYID";
            //drpShipping_Address_Country.DataBind();
            //drpShipping_Address_Country.Items.Insert(0, new ListItem("-- Select --", "0"));


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
            //   drpID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtName.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone_Office.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone_Fax.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone_Alternate.Text = Listview1.SelectedDataKey[0].ToString();
            txtWebsite.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmail1.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmail2.Text = Listview1.SelectedDataKey[0].ToString();
            txtAnnual_Revenue.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmployee.Text = Listview1.SelectedDataKey[0].ToString();
            drpIndustryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtOwnership.Text = Listview1.SelectedDataKey[0].ToString();
            drpAccountType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtTickerSymbol.Text = Listview1.SelectedDataKey[0].ToString();
            txtRating.Text = Listview1.SelectedDataKey[0].ToString();
            txtSicCode.Text = Listview1.SelectedDataKey[0].ToString();
            txtBilling_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
            txtBilling_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
            drpBilling_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtBilling_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
            drpBilling_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtShipping_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
            txtShipping_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
            drpShipping_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtShipping_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
            drpShipping_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtParentName.Text = Listview1.SelectedDataKey[0].ToString();
            txtDateEntered.Text = Listview1.SelectedDataKey[0].ToString();
            txtDateModified.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
            drpTeamName.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtAssigned_to_Name.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpModifiedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtContactName.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //     drpID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtName.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone_Office.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone_Fax.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone_Alternate.Text = Listview1.SelectedDataKey[0].ToString();
                txtWebsite.Text = Listview1.SelectedDataKey[0].ToString();
                txtEmail1.Text = Listview1.SelectedDataKey[0].ToString();
                txtEmail2.Text = Listview1.SelectedDataKey[0].ToString();
                txtAnnual_Revenue.Text = Listview1.SelectedDataKey[0].ToString();
                txtEmployee.Text = Listview1.SelectedDataKey[0].ToString();
                drpIndustryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtOwnership.Text = Listview1.SelectedDataKey[0].ToString();
                drpAccountType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtTickerSymbol.Text = Listview1.SelectedDataKey[0].ToString();
                txtRating.Text = Listview1.SelectedDataKey[0].ToString();
                txtSicCode.Text = Listview1.SelectedDataKey[0].ToString();
                txtBilling_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
                txtBilling_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
                drpBilling_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtBilling_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
                drpBilling_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtShipping_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
                txtShipping_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
                drpShipping_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtShipping_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
                drpShipping_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtParentName.Text = Listview1.SelectedDataKey[0].ToString();
                txtDateEntered.Text = Listview1.SelectedDataKey[0].ToString();
                txtDateModified.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
                drpTeamName.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtAssigned_to_Name.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpModifiedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtContactName.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

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
                //    drpID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtName.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone_Office.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone_Fax.Text = Listview1.SelectedDataKey[0].ToString();
                txtPhone_Alternate.Text = Listview1.SelectedDataKey[0].ToString();
                txtWebsite.Text = Listview1.SelectedDataKey[0].ToString();
                txtEmail1.Text = Listview1.SelectedDataKey[0].ToString();
                txtEmail2.Text = Listview1.SelectedDataKey[0].ToString();
                txtAnnual_Revenue.Text = Listview1.SelectedDataKey[0].ToString();
                txtEmployee.Text = Listview1.SelectedDataKey[0].ToString();
                drpIndustryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtOwnership.Text = Listview1.SelectedDataKey[0].ToString();
                drpAccountType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtTickerSymbol.Text = Listview1.SelectedDataKey[0].ToString();
                txtRating.Text = Listview1.SelectedDataKey[0].ToString();
                txtSicCode.Text = Listview1.SelectedDataKey[0].ToString();
                txtBilling_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
                txtBilling_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
                drpBilling_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtBilling_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
                drpBilling_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtShipping_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
                txtShipping_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
                drpShipping_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtShipping_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
                drpShipping_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtParentName.Text = Listview1.SelectedDataKey[0].ToString();
                txtDateEntered.Text = Listview1.SelectedDataKey[0].ToString();
                txtDateModified.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
                drpTeamName.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtAssigned_to_Name.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //drpModifiedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtContactName.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //  drpID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtName.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone_Office.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone_Fax.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone_Alternate.Text = Listview1.SelectedDataKey[0].ToString();
            txtWebsite.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmail1.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmail2.Text = Listview1.SelectedDataKey[0].ToString();
            txtAnnual_Revenue.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmployee.Text = Listview1.SelectedDataKey[0].ToString();
            drpIndustryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtOwnership.Text = Listview1.SelectedDataKey[0].ToString();
            drpAccountType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtTickerSymbol.Text = Listview1.SelectedDataKey[0].ToString();
            txtRating.Text = Listview1.SelectedDataKey[0].ToString();
            txtSicCode.Text = Listview1.SelectedDataKey[0].ToString();
            txtBilling_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
            txtBilling_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
            drpBilling_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtBilling_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
            drpBilling_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtShipping_Address_Street.Text = Listview1.SelectedDataKey[0].ToString();
            txtShipping_Address_City.Text = Listview1.SelectedDataKey[0].ToString();
            drpShipping_Address_State.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtShipping_Address_Postalcode.Text = Listview1.SelectedDataKey[0].ToString();
            drpShipping_Address_Country.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtParentName.Text = Listview1.SelectedDataKey[0].ToString();
            txtDateEntered.Text = Listview1.SelectedDataKey[0].ToString();
            txtDateModified.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
            drpTeamName.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtAssigned_to_Name.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpModifiedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtContactName.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

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
                    lblName2h.Visible = lblPhone_Office2h.Visible = lblPhone2h.Visible = lblPhone_Fax2h.Visible = lblPhone_Alternate2h.Visible = lblWebsite2h.Visible = lblEmail12h.Visible = lblEmail22h.Visible = lblAnnual_Revenue2h.Visible = lblEmployee2h.Visible = lblIndustryID2h.Visible = lblOwnership2h.Visible = lblAccountType2h.Visible = lblTickerSymbol2h.Visible = lblRating2h.Visible = lblSicCode2h.Visible = lblBilling_Address_Street2h.Visible = lblBilling_Address_City2h.Visible = lblBilling_Address_State2h.Visible = lblBilling_Address_Postalcode2h.Visible = lblBilling_Address_Country2h.Visible = lblShipping_Address_Street2h.Visible = lblShipping_Address_City2h.Visible = lblShipping_Address_State2h.Visible = lblShipping_Address_Postalcode2h.Visible = lblShipping_Address_Country2h.Visible = lblParentName2h.Visible = lblDateEntered2h.Visible = lblDateModified2h.Visible = lblDescription2h.Visible = lblTeamName2h.Visible = lblAssigned_to_Name2h.Visible = lblCreatedBy2h.Visible = lblModifiedBy2h.Visible = lblContactName2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = lblCRUP_ID2h.Visible = false;
                    //2true
                    txtName2h.Visible = txtPhone_Office2h.Visible = txtPhone2h.Visible = txtPhone_Fax2h.Visible = txtPhone_Alternate2h.Visible = txtWebsite2h.Visible = txtEmail12h.Visible = txtEmail22h.Visible = txtAnnual_Revenue2h.Visible = txtEmployee2h.Visible = txtIndustryID2h.Visible = txtOwnership2h.Visible = txtAccountType2h.Visible = txtTickerSymbol2h.Visible = txtRating2h.Visible = txtSicCode2h.Visible = txtBilling_Address_Street2h.Visible = txtBilling_Address_City2h.Visible = txtBilling_Address_State2h.Visible = txtBilling_Address_Postalcode2h.Visible = txtBilling_Address_Country2h.Visible = txtShipping_Address_Street2h.Visible = txtShipping_Address_City2h.Visible = txtShipping_Address_State2h.Visible = txtShipping_Address_Postalcode2h.Visible = txtShipping_Address_Country2h.Visible = txtParentName2h.Visible = txtDateEntered2h.Visible = txtDateModified2h.Visible = txtDescription2h.Visible = txtTeamName2h.Visible = txtAssigned_to_Name2h.Visible = txtCreatedBy2h.Visible = txtModifiedBy2h.Visible = txtContactName2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = txtCRUP_ID2h.Visible = true;

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
                    lblName2h.Visible = lblPhone_Office2h.Visible = lblPhone2h.Visible = lblPhone_Fax2h.Visible = lblPhone_Alternate2h.Visible = lblWebsite2h.Visible = lblEmail12h.Visible = lblEmail22h.Visible = lblAnnual_Revenue2h.Visible = lblEmployee2h.Visible = lblIndustryID2h.Visible = lblOwnership2h.Visible = lblAccountType2h.Visible = lblTickerSymbol2h.Visible = lblRating2h.Visible = lblSicCode2h.Visible = lblBilling_Address_Street2h.Visible = lblBilling_Address_City2h.Visible = lblBilling_Address_State2h.Visible = lblBilling_Address_Postalcode2h.Visible = lblBilling_Address_Country2h.Visible = lblShipping_Address_Street2h.Visible = lblShipping_Address_City2h.Visible = lblShipping_Address_State2h.Visible = lblShipping_Address_Postalcode2h.Visible = lblShipping_Address_Country2h.Visible = lblParentName2h.Visible = lblDateEntered2h.Visible = lblDateModified2h.Visible = lblDescription2h.Visible = lblTeamName2h.Visible = lblAssigned_to_Name2h.Visible = lblCreatedBy2h.Visible = lblModifiedBy2h.Visible = lblContactName2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = lblCRUP_ID2h.Visible = true;
                    //2false
                    txtName2h.Visible = txtPhone_Office2h.Visible = txtPhone2h.Visible = txtPhone_Fax2h.Visible = txtPhone_Alternate2h.Visible = txtWebsite2h.Visible = txtEmail12h.Visible = txtEmail22h.Visible = txtAnnual_Revenue2h.Visible = txtEmployee2h.Visible = txtIndustryID2h.Visible = txtOwnership2h.Visible = txtAccountType2h.Visible = txtTickerSymbol2h.Visible = txtRating2h.Visible = txtSicCode2h.Visible = txtBilling_Address_Street2h.Visible = txtBilling_Address_City2h.Visible = txtBilling_Address_State2h.Visible = txtBilling_Address_Postalcode2h.Visible = txtBilling_Address_Country2h.Visible = txtShipping_Address_Street2h.Visible = txtShipping_Address_City2h.Visible = txtShipping_Address_State2h.Visible = txtShipping_Address_Postalcode2h.Visible = txtShipping_Address_Country2h.Visible = txtParentName2h.Visible = txtDateEntered2h.Visible = txtDateModified2h.Visible = txtDescription2h.Visible = txtTeamName2h.Visible = txtAssigned_to_Name2h.Visible = txtCreatedBy2h.Visible = txtModifiedBy2h.Visible = txtContactName2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = txtCRUP_ID2h.Visible = false;

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
                    lblName1s.Visible = lblPhone_Office1s.Visible = lblPhone1s.Visible = lblPhone_Fax1s.Visible = lblPhone_Alternate1s.Visible = lblWebsite1s.Visible = lblEmail11s.Visible = lblEmail21s.Visible = lblAnnual_Revenue1s.Visible = lblEmployee1s.Visible = lblIndustryID1s.Visible = lblOwnership1s.Visible = lblAccountType1s.Visible = lblTickerSymbol1s.Visible = lblRating1s.Visible = lblSicCode1s.Visible = lblBilling_Address_Street1s.Visible = lblBilling_Address_City1s.Visible = lblBilling_Address_State1s.Visible = lblBilling_Address_Postalcode1s.Visible = lblBilling_Address_Country1s.Visible = lblShipping_Address_Street1s.Visible = lblShipping_Address_City1s.Visible = lblShipping_Address_State1s.Visible = lblShipping_Address_Postalcode1s.Visible = lblShipping_Address_Country1s.Visible = lblParentName1s.Visible = lblDateEntered1s.Visible = lblDateModified1s.Visible = lblDescription1s.Visible = lblTeamName1s.Visible = lblAssigned_to_Name1s.Visible = lblCreatedBy1s.Visible = lblModifiedBy1s.Visible = lblContactName1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = lblCRUP_ID1s.Visible = false;
                    //1true
                    txtName1s.Visible = txtPhone_Office1s.Visible = txtPhone1s.Visible = txtPhone_Fax1s.Visible = txtPhone_Alternate1s.Visible = txtWebsite1s.Visible = txtEmail11s.Visible = txtEmail21s.Visible = txtAnnual_Revenue1s.Visible = txtEmployee1s.Visible = txtIndustryID1s.Visible = txtOwnership1s.Visible = txtAccountType1s.Visible = txtTickerSymbol1s.Visible = txtRating1s.Visible = txtSicCode1s.Visible = txtBilling_Address_Street1s.Visible = txtBilling_Address_City1s.Visible = txtBilling_Address_State1s.Visible = txtBilling_Address_Postalcode1s.Visible = txtBilling_Address_Country1s.Visible = txtShipping_Address_Street1s.Visible = txtShipping_Address_City1s.Visible = txtShipping_Address_State1s.Visible = txtShipping_Address_Postalcode1s.Visible = txtShipping_Address_Country1s.Visible = txtParentName1s.Visible = txtDateEntered1s.Visible = txtDateModified1s.Visible = txtDescription1s.Visible = txtTeamName1s.Visible = txtAssigned_to_Name1s.Visible = txtCreatedBy1s.Visible = txtModifiedBy1s.Visible = txtContactName1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = txtCRUP_ID1s.Visible = true;
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
                    lblName1s.Visible = lblPhone_Office1s.Visible = lblPhone1s.Visible = lblPhone_Fax1s.Visible = lblPhone_Alternate1s.Visible = lblWebsite1s.Visible = lblEmail11s.Visible = lblEmail21s.Visible = lblAnnual_Revenue1s.Visible = lblEmployee1s.Visible = lblIndustryID1s.Visible = lblOwnership1s.Visible = lblAccountType1s.Visible = lblTickerSymbol1s.Visible = lblRating1s.Visible = lblSicCode1s.Visible = lblBilling_Address_Street1s.Visible = lblBilling_Address_City1s.Visible = lblBilling_Address_State1s.Visible = lblBilling_Address_Postalcode1s.Visible = lblBilling_Address_Country1s.Visible = lblShipping_Address_Street1s.Visible = lblShipping_Address_City1s.Visible = lblShipping_Address_State1s.Visible = lblShipping_Address_Postalcode1s.Visible = lblShipping_Address_Country1s.Visible = lblParentName1s.Visible = lblDateEntered1s.Visible = lblDateModified1s.Visible = lblDescription1s.Visible = lblTeamName1s.Visible = lblAssigned_to_Name1s.Visible = lblCreatedBy1s.Visible = lblModifiedBy1s.Visible = lblContactName1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = lblCRUP_ID1s.Visible = true;
                    //1false
                    txtName1s.Visible = txtPhone_Office1s.Visible = txtPhone1s.Visible = txtPhone_Fax1s.Visible = txtPhone_Alternate1s.Visible = txtWebsite1s.Visible = txtEmail11s.Visible = txtEmail21s.Visible = txtAnnual_Revenue1s.Visible = txtEmployee1s.Visible = txtIndustryID1s.Visible = txtOwnership1s.Visible = txtAccountType1s.Visible = txtTickerSymbol1s.Visible = txtRating1s.Visible = txtSicCode1s.Visible = txtBilling_Address_Street1s.Visible = txtBilling_Address_City1s.Visible = txtBilling_Address_State1s.Visible = txtBilling_Address_Postalcode1s.Visible = txtBilling_Address_Country1s.Visible = txtShipping_Address_Street1s.Visible = txtShipping_Address_City1s.Visible = txtShipping_Address_State1s.Visible = txtShipping_Address_Postalcode1s.Visible = txtShipping_Address_Country1s.Visible = txtParentName1s.Visible = txtDateEntered1s.Visible = txtDateModified1s.Visible = txtDescription1s.Visible = txtTeamName1s.Visible = txtAssigned_to_Name1s.Visible = txtCreatedBy1s.Visible = txtModifiedBy1s.Visible = txtContactName1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = txtCRUP_ID1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Account_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {

                if (lblName1s.ID == item.LabelID)
                    txtName1s.Text = lblName1s.Text = lblhName.Text = item.LabelName;
                else if (lblPhone_Office1s.ID == item.LabelID)
                    txtPhone_Office1s.Text = lblPhone_Office1s.Text = lblhPhone_Office.Text = item.LabelName;
                else if (lblPhone1s.ID == item.LabelID)
                    txtPhone1s.Text = lblPhone1s.Text = lblhPhone.Text = item.LabelName;
                else if (lblPhone_Fax1s.ID == item.LabelID)
                    txtPhone_Fax1s.Text = lblPhone_Fax1s.Text = item.LabelName;
                else if (lblPhone_Alternate1s.ID == item.LabelID)
                    txtPhone_Alternate1s.Text = lblPhone_Alternate1s.Text = item.LabelName;
                else if (lblWebsite1s.ID == item.LabelID)
                    txtWebsite1s.Text = lblWebsite1s.Text = lblhWebsite.Text = item.LabelName;
                else if (lblEmail11s.ID == item.LabelID)
                    txtEmail11s.Text = lblEmail11s.Text = lblhEmail1.Text = item.LabelName;
                else if (lblEmail21s.ID == item.LabelID)
                    txtEmail21s.Text = lblEmail21s.Text = item.LabelName;
                else if (lblAnnual_Revenue1s.ID == item.LabelID)
                    txtAnnual_Revenue1s.Text = lblAnnual_Revenue1s.Text = lblhAnnual_Revenue.Text = item.LabelName;
                else if (lblEmployee1s.ID == item.LabelID)
                    txtEmployee1s.Text = lblEmployee1s.Text = lblhEmployee.Text = item.LabelName;
                else if (lblIndustryID1s.ID == item.LabelID)
                    txtIndustryID1s.Text = lblIndustryID1s.Text  = item.LabelName;
                else if (lblOwnership1s.ID == item.LabelID)
                    txtOwnership1s.Text = lblOwnership1s.Text = item.LabelName;
                else if (lblAccountType1s.ID == item.LabelID)
                    txtAccountType1s.Text = lblAccountType1s.Text =  item.LabelName;
                else if (lblTickerSymbol1s.ID == item.LabelID)
                    txtTickerSymbol1s.Text = lblTickerSymbol1s.Text =  item.LabelName;
                else if (lblRating1s.ID == item.LabelID)
                    txtRating1s.Text = lblRating1s.Text = item.LabelName;
                else if (lblSicCode1s.ID == item.LabelID)
                    txtSicCode1s.Text = lblSicCode1s.Text = item.LabelName;
                else if (lblBilling_Address_Street1s.ID == item.LabelID)
                    txtBilling_Address_Street1s.Text = lblBilling_Address_Street1s.Text =  item.LabelName;
                else if (lblBilling_Address_City1s.ID == item.LabelID)
                    txtBilling_Address_City1s.Text = lblBilling_Address_City1s.Text = item.LabelName;
                else if (lblBilling_Address_State1s.ID == item.LabelID)
                    txtBilling_Address_State1s.Text = lblBilling_Address_State1s.Text =  item.LabelName;
                else if (lblBilling_Address_Postalcode1s.ID == item.LabelID)
                    txtBilling_Address_Postalcode1s.Text = lblBilling_Address_Postalcode1s.Text =  item.LabelName;
                else if (lblBilling_Address_Country1s.ID == item.LabelID)
                    txtBilling_Address_Country1s.Text = lblBilling_Address_Country1s.Text = item.LabelName;
                else if (lblShipping_Address_Street1s.ID == item.LabelID)
                    txtShipping_Address_Street1s.Text = lblShipping_Address_Street1s.Text = item.LabelName;
                else if (lblShipping_Address_City1s.ID == item.LabelID)
                    txtShipping_Address_City1s.Text = lblShipping_Address_City1s.Text = item.LabelName;
                else if (lblShipping_Address_State1s.ID == item.LabelID)
                    txtShipping_Address_State1s.Text = lblShipping_Address_State1s.Text = item.LabelName;
                else if (lblShipping_Address_Postalcode1s.ID == item.LabelID)
                    txtShipping_Address_Postalcode1s.Text = lblShipping_Address_Postalcode1s.Text = item.LabelName;
                else if (lblShipping_Address_Country1s.ID == item.LabelID)
                    txtShipping_Address_Country1s.Text = lblShipping_Address_Country1s.Text = item.LabelName;
                else if (lblParentName1s.ID == item.LabelID)
                    txtParentName1s.Text = lblParentName1s.Text =  item.LabelName;
                else if (lblDateEntered1s.ID == item.LabelID)
                    txtDateEntered1s.Text = lblDateEntered1s.Text = item.LabelName;
                else if (lblDateModified1s.ID == item.LabelID)
                    txtDateModified1s.Text = lblDateModified1s.Text = item.LabelName;
                else if (lblDescription1s.ID == item.LabelID)
                    txtDescription1s.Text = lblDescription1s.Text = item.LabelName;
                else if (lblTeamName1s.ID == item.LabelID)
                    txtTeamName1s.Text = lblTeamName1s.Text = item.LabelName;
                else if (lblAssigned_to_Name1s.ID == item.LabelID)
                    txtAssigned_to_Name1s.Text = lblAssigned_to_Name1s.Text =  item.LabelName;
                else if (lblCreatedBy1s.ID == item.LabelID)
                    txtCreatedBy1s.Text = lblCreatedBy1s.Text = item.LabelName;
                else if (lblModifiedBy1s.ID == item.LabelID)
                    txtModifiedBy1s.Text = lblModifiedBy1s.Text = item.LabelName;
                else if (lblContactName1s.ID == item.LabelID)
                    txtContactName1s.Text = lblContactName1s.Text =  item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblDeleted1s.ID == item.LabelID)
                    txtDeleted1s.Text = lblDeleted1s.Text = item.LabelName;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                else if (lblName2h.ID == item.LabelID)
                    txtName2h.Text = lblName2h.Text = lblhName.Text = item.LabelName;
                else if (lblPhone_Office2h.ID == item.LabelID)
                    txtPhone_Office2h.Text = lblPhone_Office2h.Text = lblhPhone_Office.Text = item.LabelName;
                else if (lblPhone2h.ID == item.LabelID)
                    txtPhone2h.Text = lblPhone2h.Text = lblhPhone.Text = item.LabelName;
                else if (lblPhone_Fax2h.ID == item.LabelID)
                    txtPhone_Fax2h.Text = lblPhone_Fax2h.Text = item.LabelName;
                else if (lblPhone_Alternate2h.ID == item.LabelID)
                    txtPhone_Alternate2h.Text = lblPhone_Alternate2h.Text = item.LabelName;
                else if (lblWebsite2h.ID == item.LabelID)
                    txtWebsite2h.Text = lblWebsite2h.Text = lblhWebsite.Text = item.LabelName;
                else if (lblEmail12h.ID == item.LabelID)
                    txtEmail12h.Text = lblEmail12h.Text = lblhEmail1.Text = item.LabelName;
                else if (lblEmail22h.ID == item.LabelID)
                    txtEmail22h.Text = lblEmail22h.Text = item.LabelName;
                else if (lblAnnual_Revenue2h.ID == item.LabelID)
                    txtAnnual_Revenue2h.Text = lblAnnual_Revenue2h.Text = lblhAnnual_Revenue.Text = item.LabelName;
                else if (lblEmployee2h.ID == item.LabelID)
                    txtEmployee2h.Text = lblEmployee2h.Text = lblhEmployee.Text = item.LabelName;
                else if (lblIndustryID2h.ID == item.LabelID)
                    txtIndustryID2h.Text = lblIndustryID2h.Text =  item.LabelName;
                else if (lblOwnership2h.ID == item.LabelID)
                    txtOwnership2h.Text = lblOwnership2h.Text = item.LabelName;
                else if (lblAccountType2h.ID == item.LabelID)
                    txtAccountType2h.Text = lblAccountType2h.Text =item.LabelName;
                else if (lblTickerSymbol2h.ID == item.LabelID)
                    txtTickerSymbol2h.Text = lblTickerSymbol2h.Text =item.LabelName;
                else if (lblRating2h.ID == item.LabelID)
                    txtRating2h.Text = lblRating2h.Text = item.LabelName;
                else if (lblSicCode2h.ID == item.LabelID)
                    txtSicCode2h.Text = lblSicCode2h.Text = item.LabelName;
                else if (lblBilling_Address_Street2h.ID == item.LabelID)
                    txtBilling_Address_Street2h.Text = lblBilling_Address_Street2h.Text = item.LabelName;
                else if (lblBilling_Address_City2h.ID == item.LabelID)
                    txtBilling_Address_City2h.Text = lblBilling_Address_City2h.Text =  item.LabelName;
                else if (lblBilling_Address_State2h.ID == item.LabelID)
                    txtBilling_Address_State2h.Text = lblBilling_Address_State2h.Text =  item.LabelName;
                else if (lblBilling_Address_Postalcode2h.ID == item.LabelID)
                    txtBilling_Address_Postalcode2h.Text = lblBilling_Address_Postalcode2h.Text = item.LabelName;
                else if (lblBilling_Address_Country2h.ID == item.LabelID)
                    txtBilling_Address_Country2h.Text = lblBilling_Address_Country2h.Text =  item.LabelName;
                else if (lblShipping_Address_Street2h.ID == item.LabelID)
                    txtShipping_Address_Street2h.Text = lblShipping_Address_Street2h.Text = item.LabelName;
                else if (lblShipping_Address_City2h.ID == item.LabelID)
                    txtShipping_Address_City2h.Text = lblShipping_Address_City2h.Text = item.LabelName;
                else if (lblShipping_Address_State2h.ID == item.LabelID)
                    txtShipping_Address_State2h.Text = lblShipping_Address_State2h.Text = item.LabelName;
                else if (lblShipping_Address_Postalcode2h.ID == item.LabelID)
                    txtShipping_Address_Postalcode2h.Text = lblShipping_Address_Postalcode2h.Text = item.LabelName;
                else if (lblShipping_Address_Country2h.ID == item.LabelID)
                    txtShipping_Address_Country2h.Text = lblShipping_Address_Country2h.Text = item.LabelName;
                else if (lblParentName2h.ID == item.LabelID)
                    txtParentName2h.Text = lblParentName2h.Text =item.LabelName;
                else if (lblDateEntered2h.ID == item.LabelID)
                    txtDateEntered2h.Text = lblDateEntered2h.Text = item.LabelName;
                else if (lblDateModified2h.ID == item.LabelID)
                    txtDateModified2h.Text = lblDateModified2h.Text = item.LabelName;
                else if (lblDescription2h.ID == item.LabelID)
                    txtDescription2h.Text = lblDescription2h.Text = item.LabelName;
                else if (lblTeamName2h.ID == item.LabelID)
                    txtTeamName2h.Text = lblTeamName2h.Text = item.LabelName;
                else if (lblAssigned_to_Name2h.ID == item.LabelID)
                    txtAssigned_to_Name2h.Text = lblAssigned_to_Name2h.Text = item.LabelName;
                else if (lblCreatedBy2h.ID == item.LabelID)
                    txtCreatedBy2h.Text = lblCreatedBy2h.Text = item.LabelName;
                else if (lblModifiedBy2h.ID == item.LabelID)
                    txtModifiedBy2h.Text = lblModifiedBy2h.Text = item.LabelName;
                else if (lblContactName2h.ID == item.LabelID)
                    txtContactName2h.Text = lblContactName2h.Text  = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblDeleted2h.ID == item.LabelID)
                    txtDeleted2h.Text = lblDeleted2h.Text = item.LabelName;
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
            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("tbl_Account_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\tbl_Account_Mst.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((Web.CRM.CRMMaster)this.Master).Bindxml("tbl_Account_Mst").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName1s.Text;
                else if (lblPhone_Office1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone_Office1s.Text;
                else if (lblPhone1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone1s.Text;
                else if (lblPhone_Fax1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone_Fax1s.Text;
                else if (lblPhone_Alternate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone_Alternate1s.Text;
                else if (lblWebsite1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWebsite1s.Text;
                else if (lblEmail11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmail11s.Text;
                else if (lblEmail21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmail21s.Text;
                else if (lblAnnual_Revenue1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAnnual_Revenue1s.Text;
                else if (lblEmployee1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmployee1s.Text;
                else if (lblIndustryID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIndustryID1s.Text;
                else if (lblOwnership1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOwnership1s.Text;
                else if (lblAccountType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAccountType1s.Text;
                else if (lblTickerSymbol1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTickerSymbol1s.Text;
                else if (lblRating1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRating1s.Text;
                else if (lblSicCode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSicCode1s.Text;
                else if (lblBilling_Address_Street1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_Street1s.Text;
                else if (lblBilling_Address_City1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_City1s.Text;
                else if (lblBilling_Address_State1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_State1s.Text;
                else if (lblBilling_Address_Postalcode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_Postalcode1s.Text;
                else if (lblBilling_Address_Country1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_Country1s.Text;
                else if (lblShipping_Address_Street1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_Street1s.Text;
                else if (lblShipping_Address_City1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_City1s.Text;
                else if (lblShipping_Address_State1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_State1s.Text;
                else if (lblShipping_Address_Postalcode1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_Postalcode1s.Text;
                else if (lblShipping_Address_Country1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_Country1s.Text;
                else if (lblParentName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtParentName1s.Text;
                else if (lblDateEntered1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateEntered1s.Text;
                else if (lblDateModified1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateModified1s.Text;
                else if (lblDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription1s.Text;
                else if (lblTeamName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTeamName1s.Text;
                else if (lblAssigned_to_Name1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssigned_to_Name1s.Text;
                else if (lblCreatedBy1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCreatedBy1s.Text;
                else if (lblModifiedBy1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModifiedBy1s.Text;
                else if (lblContactName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContactName1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblDeleted1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;


                else if (lblName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName2h.Text;
                else if (lblPhone_Office2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone_Office2h.Text;
                else if (lblPhone2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone2h.Text;
                else if (lblPhone_Fax2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone_Fax2h.Text;
                else if (lblPhone_Alternate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone_Alternate2h.Text;
                else if (lblWebsite2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWebsite2h.Text;
                else if (lblEmail12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmail12h.Text;
                else if (lblEmail22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmail22h.Text;
                else if (lblAnnual_Revenue2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAnnual_Revenue2h.Text;
                else if (lblEmployee2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmployee2h.Text;
                else if (lblIndustryID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIndustryID2h.Text;
                else if (lblOwnership2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOwnership2h.Text;
                else if (lblAccountType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAccountType2h.Text;
                else if (lblTickerSymbol2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTickerSymbol2h.Text;
                else if (lblRating2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRating2h.Text;
                else if (lblSicCode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSicCode2h.Text;
                else if (lblBilling_Address_Street2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_Street2h.Text;
                else if (lblBilling_Address_City2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_City2h.Text;
                else if (lblBilling_Address_State2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_State2h.Text;
                else if (lblBilling_Address_Postalcode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_Postalcode2h.Text;
                else if (lblBilling_Address_Country2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBilling_Address_Country2h.Text;
                else if (lblShipping_Address_Street2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_Street2h.Text;
                else if (lblShipping_Address_City2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_City2h.Text;
                else if (lblShipping_Address_State2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_State2h.Text;
                else if (lblShipping_Address_Postalcode2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_Postalcode2h.Text;
                else if (lblShipping_Address_Country2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShipping_Address_Country2h.Text;
                else if (lblParentName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtParentName2h.Text;
                else if (lblDateEntered2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateEntered2h.Text;
                else if (lblDateModified2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDateModified2h.Text;
                else if (lblDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription2h.Text;
                else if (lblTeamName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTeamName2h.Text;
                else if (lblAssigned_to_Name2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssigned_to_Name2h.Text;
                else if (lblCreatedBy2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCreatedBy2h.Text;
                else if (lblModifiedBy2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtModifiedBy2h.Text;
                else if (lblContactName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContactName2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblDeleted2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\tbl_Account_Mst.xml"));

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
            //  drpID.Enabled = true;
            txtName.Enabled = true;
            txtPhone_Office.Enabled = true;
            txtPhone.Enabled = true;
            txtPhone_Fax.Enabled = true;
            txtPhone_Alternate.Enabled = true;
            txtWebsite.Enabled = true;
            txtEmail1.Enabled = true;
            txtEmail2.Enabled = true;
            txtAnnual_Revenue.Enabled = true;
            txtEmployee.Enabled = true;
            drpIndustryID.Enabled = true;
            txtOwnership.Enabled = true;
            drpAccountType.Enabled = true;
            txtTickerSymbol.Enabled = true;
            txtRating.Enabled = true;
            txtSicCode.Enabled = true;
            txtBilling_Address_Street.Enabled = true;
            txtBilling_Address_City.Enabled = true;
            drpBilling_Address_State.Enabled = true;
            txtBilling_Address_Postalcode.Enabled = true;
            drpBilling_Address_Country.Enabled = true;
            txtShipping_Address_Street.Enabled = true;
            txtShipping_Address_City.Enabled = true;
            drpShipping_Address_State.Enabled = true;
            txtShipping_Address_Postalcode.Enabled = true;
            drpShipping_Address_Country.Enabled = true;
            txtParentName.Enabled = true;
            txtDateEntered.Enabled = true;
            txtDateModified.Enabled = true;
            txtDescription.Enabled = true;
            drpTeamName.Enabled = true;
            txtAssigned_to_Name.Enabled = true;
            //drpCreatedBy.Enabled = true;
            //drpModifiedBy.Enabled = true;
            txtContactName.Enabled = true;
            cbActive.Enabled = true;
            cbDeleted.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //   drpID.Enabled = false;
            txtName.Enabled = false;
            txtPhone_Office.Enabled = false;
            txtPhone.Enabled = false;
            txtPhone_Fax.Enabled = false;
            txtPhone_Alternate.Enabled = false;
            txtWebsite.Enabled = false;
            txtEmail1.Enabled = false;
            txtEmail2.Enabled = false;
            txtAnnual_Revenue.Enabled = false;
            txtEmployee.Enabled = false;
            drpIndustryID.Enabled = false;
            txtOwnership.Enabled = false;
            drpAccountType.Enabled = false;
            txtTickerSymbol.Enabled = false;
            txtRating.Enabled = false;
            txtSicCode.Enabled = false;
            txtBilling_Address_Street.Enabled = false;
            txtBilling_Address_City.Enabled = false;
            drpBilling_Address_State.Enabled = false;
            txtBilling_Address_Postalcode.Enabled = false;
            drpBilling_Address_Country.Enabled = false;
            txtShipping_Address_Street.Enabled = false;
            txtShipping_Address_City.Enabled = false;
            drpShipping_Address_State.Enabled = false;
            txtShipping_Address_Postalcode.Enabled = false;
            drpShipping_Address_Country.Enabled = false;
            txtParentName.Enabled = false;
            txtDateEntered.Enabled = false;
            txtDateModified.Enabled = false;
            txtDescription.Enabled = false;
            drpTeamName.Enabled = false;
            txtAssigned_to_Name.Enabled = false;
            //drpCreatedBy.Enabled = false;
            //drpModifiedBy.Enabled = false;
            txtContactName.Enabled = false;
            cbActive.Enabled = false;
            cbDeleted.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Account_Mst.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Account_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tbl_Account_Mst.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Account_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.tbl_Account_Mst.Count();
                take = Showdata;
                Skip = 0;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Account_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tbl_Account_Mst.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Account_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        Database.tbl_Account_Mst objSOJobDesc = DB.tbl_Account_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                        objSOJobDesc.Active = false;
                        DB.SaveChanges();

                        String url = "Delete tbl_Account_Mst with " + "TenentID = " + TID + "ID =" + ID;
                        String evantname = "Delete";
                        String tablename = "tbl_Account_Mst";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Account_Mst.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        Database.tbl_Account_Mst objtbl_Account_Mst = DB.tbl_Account_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                        //    drpID.SelectedValue = objtbl_Account_Mst.ID.ToString();
                        txtName.Text = objtbl_Account_Mst.Name.ToString();
                        txtPhone_Office.Text = objtbl_Account_Mst.Phone_Office.ToString();
                        txtPhone.Text = objtbl_Account_Mst.Phone.ToString();
                        txtPhone_Fax.Text = objtbl_Account_Mst.Phone_Fax.ToString();
                        txtPhone_Alternate.Text = objtbl_Account_Mst.Phone_Alternate.ToString();
                        txtWebsite.Text = objtbl_Account_Mst.Website.ToString();
                        txtEmail1.Text = objtbl_Account_Mst.Email1.ToString();
                        txtEmail2.Text = objtbl_Account_Mst.Email2.ToString();
                        txtAnnual_Revenue.Text = objtbl_Account_Mst.Annual_Revenue.ToString();
                        txtEmployee.Text = objtbl_Account_Mst.Employee.ToString();
                        drpIndustryID.SelectedValue = objtbl_Account_Mst.IndustryID.ToString();
                        txtOwnership.Text = objtbl_Account_Mst.Ownership.ToString();
                        drpAccountType.SelectedValue = objtbl_Account_Mst.AccountType.ToString();
                        txtTickerSymbol.Text = objtbl_Account_Mst.TickerSymbol.ToString();
                        txtRating.Text = objtbl_Account_Mst.Rating.ToString();
                        txtSicCode.Text = objtbl_Account_Mst.SicCode.ToString();
                        txtBilling_Address_Street.Text = objtbl_Account_Mst.Billing_Address_Street.ToString();
                        txtBilling_Address_City.Text = objtbl_Account_Mst.Billing_Address_City.ToString();
                        drpBilling_Address_State.SelectedValue = objtbl_Account_Mst.Billing_Address_State.ToString();
                        txtBilling_Address_Postalcode.Text = objtbl_Account_Mst.Billing_Address_Postalcode.ToString();
                        drpBilling_Address_Country.SelectedValue = objtbl_Account_Mst.Billing_Address_Country.ToString();
                        txtShipping_Address_Street.Text = objtbl_Account_Mst.Shipping_Address_Street.ToString();
                        txtShipping_Address_City.Text = objtbl_Account_Mst.Shipping_Address_City.ToString();
                        drpShipping_Address_State.SelectedValue = objtbl_Account_Mst.Shipping_Address_State.ToString();
                        txtShipping_Address_Postalcode.Text = objtbl_Account_Mst.Shipping_Address_Postalcode.ToString();
                        drpShipping_Address_Country.SelectedValue = objtbl_Account_Mst.Shipping_Address_Country.ToString();
                        txtParentName.Text = objtbl_Account_Mst.ParentName.ToString();
                        txtDateEntered.Text = objtbl_Account_Mst.DateEntered.ToString();
                        txtDateModified.Text = objtbl_Account_Mst.DateModified.ToString();
                        txtDescription.Text = objtbl_Account_Mst.Description.ToString();
                        drpTeamName.SelectedValue = objtbl_Account_Mst.TeamName.ToString();
                        txtAssigned_to_Name.Text = objtbl_Account_Mst.Assigned_to_Name.ToString();
                        //drpCreatedBy.SelectedValue = objtbl_Account_Mst.CreatedBy.ToString();
                        //drpModifiedBy.SelectedValue = objtbl_Account_Mst.ModifiedBy.ToString();
                        txtContactName.Text = objtbl_Account_Mst.ContactName.ToString();
                        cbActive.Checked = (objtbl_Account_Mst.Active == true) ? true : false;
                        cbDeleted.Checked = (objtbl_Account_Mst.Deleted == true) ? true : false;
                        //txtCRUP_ID.Text = objtbl_Account_Mst.CRUP_ID.ToString();

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
            int Totalrec = DB.tbl_Account_Mst.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Account_Mst.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
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

        protected void drpShipping_Address_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SACID = Convert.ToInt32(drpShipping_Address_Country.SelectedValue);
            string SID = SACID.ToString();

            Classes.EcommAdminClass.getdropdown(drpShipping_Address_State, TID, SID, "", "", "tblStates");
            //select * from tblStates where Active= 'Y'

            //drpShipping_Address_State.DataSource = DB.tblStates.Where(p => p.TenentID == TID && p.ACTIVE == "Y" && p.COUNTRYID == SACID);
            //drpShipping_Address_State.DataTextField = "MYNAME1";
            //drpShipping_Address_State.DataValueField = "StateID";
            //drpShipping_Address_State.DataBind();
            //drpShipping_Address_State.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void drpBilling_Address_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            int BACID = Convert.ToInt32(drpBilling_Address_Country.SelectedValue);
            string BID = BACID.ToString();

            Classes.EcommAdminClass.getdropdown(drpBilling_Address_State, TID, BID, "", "", "tblStates");
            //select * from tblStates where Active= 'Y'

            //drpBilling_Address_State.DataSource = DB.tblStates.Where(p => p.TenentID == TID && p.ACTIVE == "Y" && p.COUNTRYID == BACID);
            //drpBilling_Address_State.DataTextField = "MYNAME1";
            //drpBilling_Address_State.DataValueField = "StateID";
            //drpBilling_Address_State.DataBind();
            //drpBilling_Address_State.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

    }
}