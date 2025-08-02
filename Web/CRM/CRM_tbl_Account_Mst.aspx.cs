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
namespace Web.CRM
{
    public partial class CRM_tbl_Account_Mst : System.Web.UI.Page
    {

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
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                {
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                }
                BindData();
                GetShow();

                FirstData();

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
        public void BindData()
        {
            Listview1.DataSource = DB.tbl_Account_Mst.Where(p => p.TenentID == TID && p.Deleted == true);
            Listview1.DataBind();
        }
        #region PAge Genarator
        public void GetShow()
        {
            lblName1s.Attributes["class"] = "control-label col-md-4  getshow";  lblAnnual_Revenue1s.Attributes["class"] = "control-label col-md-4  getshow"; lblEmployee1s.Attributes["class"] = "control-label col-md-4  getshow"; lblIndustryID1s.Attributes["class"] = "control-label col-md-4  getshow"; lblOwnership1s.Attributes["class"] = "control-label col-md-4  getshow"; lblAccountType1s.Attributes["class"] = "control-label col-md-4  getshow"; lblTickerSymbol1s.Attributes["class"] = "control-label col-md-4  getshow"; lblRating1s.Attributes["class"] = "control-label col-md-4  getshow"; lblSicCode1s.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_Street1s.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_City1s.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_State1s.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_Postalcode1s.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_Country1s.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_Street1s.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_City1s.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_State1s.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_Postalcode1s.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_Country1s.Attributes["class"] = "control-label col-md-4  getshow"; lblParentName1s.Attributes["class"] = "control-label col-md-4  getshow"; lblDateEntered1s.Attributes["class"] = "control-label col-md-4  getshow"; lblDateModified1s.Attributes["class"] = "control-label col-md-4  getshow"; lblDescription1s.Attributes["class"] = "control-label col-md-4  getshow"; lblTeamName1s.Attributes["class"] = "control-label col-md-4  getshow"; lblAssigned_to_Name1s.Attributes["class"] = "control-label col-md-4  getshow"; lblContactName1s.Attributes["class"] = "control-label col-md-4  getshow"; ;
            lblName2h.Attributes["class"] = "control-label col-md-4  gethide";  lblAnnual_Revenue2h.Attributes["class"] = "control-label col-md-4  gethide"; lblEmployee2h.Attributes["class"] = "control-label col-md-4  gethide"; lblIndustryID2h.Attributes["class"] = "control-label col-md-4  gethide"; lblOwnership2h.Attributes["class"] = "control-label col-md-4  gethide"; lblAccountType2h.Attributes["class"] = "control-label col-md-4  gethide"; lblTickerSymbol2h.Attributes["class"] = "control-label col-md-4  gethide"; lblRating2h.Attributes["class"] = "control-label col-md-4  gethide"; lblSicCode2h.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_Street2h.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_City2h.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_State2h.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_Postalcode2h.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_Country2h.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_Street2h.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_City2h.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_State2h.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_Postalcode2h.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_Country2h.Attributes["class"] = "control-label col-md-4  gethide"; lblParentName2h.Attributes["class"] = "control-label col-md-4  gethide"; lblDateEntered2h.Attributes["class"] = "control-label col-md-4  gethide"; lblDateModified2h.Attributes["class"] = "control-label col-md-4  gethide"; lblDescription2h.Attributes["class"] = "control-label col-md-4  gethide"; lblTeamName2h.Attributes["class"] = "control-label col-md-4  gethide"; lblAssigned_to_Name2h.Attributes["class"] = "control-label col-md-4  gethide"; lblContactName2h.Attributes["class"] = "control-label col-md-4  gethide"; ;
        }

        public void GetHide()
        {
            lblName1s.Attributes["class"] = "control-label col-md-4  gethide";  lblAnnual_Revenue1s.Attributes["class"] = "control-label col-md-4  gethide"; lblEmployee1s.Attributes["class"] = "control-label col-md-4  gethide"; lblIndustryID1s.Attributes["class"] = "control-label col-md-4  gethide"; lblOwnership1s.Attributes["class"] = "control-label col-md-4  gethide"; lblAccountType1s.Attributes["class"] = "control-label col-md-4  gethide"; lblTickerSymbol1s.Attributes["class"] = "control-label col-md-4  gethide"; lblRating1s.Attributes["class"] = "control-label col-md-4  gethide"; lblSicCode1s.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_Street1s.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_City1s.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_State1s.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_Postalcode1s.Attributes["class"] = "control-label col-md-4  gethide"; lblBilling_Address_Country1s.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_Street1s.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_City1s.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_State1s.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_Postalcode1s.Attributes["class"] = "control-label col-md-4  gethide"; lblShipping_Address_Country1s.Attributes["class"] = "control-label col-md-4  gethide"; lblParentName1s.Attributes["class"] = "control-label col-md-4  gethide"; lblDateEntered1s.Attributes["class"] = "control-label col-md-4  gethide"; lblDateModified1s.Attributes["class"] = "control-label col-md-4  gethide"; lblDescription1s.Attributes["class"] = "control-label col-md-4  gethide"; lblTeamName1s.Attributes["class"] = "control-label col-md-4  gethide"; lblAssigned_to_Name1s.Attributes["class"] = "control-label col-md-4  gethide"; lblContactName1s.Attributes["class"] = "control-label col-md-4  gethide"; ;
            lblName2h.Attributes["class"] = "control-label col-md-4  getshow";  lblAnnual_Revenue2h.Attributes["class"] = "control-label col-md-4  getshow"; lblEmployee2h.Attributes["class"] = "control-label col-md-4  getshow"; lblIndustryID2h.Attributes["class"] = "control-label col-md-4  getshow"; lblOwnership2h.Attributes["class"] = "control-label col-md-4  getshow"; lblAccountType2h.Attributes["class"] = "control-label col-md-4  getshow"; lblTickerSymbol2h.Attributes["class"] = "control-label col-md-4  getshow"; lblRating2h.Attributes["class"] = "control-label col-md-4  getshow"; lblSicCode2h.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_Street2h.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_City2h.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_State2h.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_Postalcode2h.Attributes["class"] = "control-label col-md-4  getshow"; lblBilling_Address_Country2h.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_Street2h.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_City2h.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_State2h.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_Postalcode2h.Attributes["class"] = "control-label col-md-4  getshow"; lblShipping_Address_Country2h.Attributes["class"] = "control-label col-md-4  getshow"; lblParentName2h.Attributes["class"] = "control-label col-md-4  getshow"; lblDateEntered2h.Attributes["class"] = "control-label col-md-4  getshow"; lblDateModified2h.Attributes["class"] = "control-label col-md-4  getshow"; lblDescription2h.Attributes["class"] = "control-label col-md-4  getshow"; lblTeamName2h.Attributes["class"] = "control-label col-md-4  getshow"; lblAssigned_to_Name2h.Attributes["class"] = "control-label col-md-4  getshow"; lblContactName2h.Attributes["class"] = "control-label col-md-4  getshow"; ;
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
        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.tbl_Account_Mst objtbl_Account_Mst = DB.tbl_Account_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                objtbl_Account_Mst.Deleted = false;

                DB.SaveChanges();
                BindData();

            }
            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.tbl_Account_Mst objtbl_Account_Mst = DB.tbl_Account_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                // drpID.SelectedValue = objtbl_Account_Mst.ID.ToString();
                txtName.Text = objtbl_Account_Mst.Name.ToString();
                //txtPhone_Office.Text = objtbl_Account_Mst.Phone_Office.ToString();
                //txtPhone.Text = objtbl_Account_Mst.Phone.ToString();
                //txtPhone_Fax.Text = objtbl_Account_Mst.Phone_Fax.ToString();
                //txtPhone_Alternate.Text = objtbl_Account_Mst.Phone_Alternate.ToString();
                //txtWebsite.Text = objtbl_Account_Mst.Website.ToString();
                //txtEmail1.Text = objtbl_Account_Mst.Email1.ToString();
                //txtEmail2.Text = objtbl_Account_Mst.Email2.ToString();
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
                txtBilling_Address_State.Text = objtbl_Account_Mst.Billing_Address_State.ToString();
                txtBilling_Address_Postalcode.Text = objtbl_Account_Mst.Billing_Address_Postalcode.ToString();
                txtBilling_Address_Country.Text = objtbl_Account_Mst.Billing_Address_Country.ToString();
                txtShipping_Address_Street.Text = objtbl_Account_Mst.Shipping_Address_Street.ToString();
                txtShipping_Address_City.Text = objtbl_Account_Mst.Shipping_Address_City.ToString();
                txtShipping_Address_State.Text = objtbl_Account_Mst.Shipping_Address_State.ToString();
                txtShipping_Address_Postalcode.Text = objtbl_Account_Mst.Shipping_Address_Postalcode.ToString();
                txtShipping_Address_Country.Text = objtbl_Account_Mst.Shipping_Address_Country.ToString();
                txtParentName.Text = objtbl_Account_Mst.ParentName.ToString();
                txtDateEntered.Text = objtbl_Account_Mst.DateEntered.ToString();
                txtDateModified.Text = objtbl_Account_Mst.DateModified.ToString();
                txtDescription.Text = objtbl_Account_Mst.Description.ToString();
                drpTeamName.SelectedValue = objtbl_Account_Mst.TeamName.ToString();
                txtAssigned_to_Name.Text = objtbl_Account_Mst.Assigned_to_Name.ToString();
                //drpCreatedBy.SelectedValue = objtbl_Account_Mst.CreatedBy.ToString();
                //drpModifiedBy.SelectedValue = objtbl_Account_Mst.ModifiedBy.ToString();
                txtContactName.Text = objtbl_Account_Mst.ContactName.ToString();
                //cbActive.Checked = (objtbl_Account_Mst.Active == true) ? true : false;
                //cbDeleted.Checked = (objtbl_Account_Mst.Deleted == true) ? true : false;
                //txtCRUP_ID.Text = objtbl_Account_Mst.CRUP_ID.ToString();
                btnAdd.Text = "Update";
                ViewState["Edit"] = ID;
            }
            //        scope.Complete(); //  To commit.
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        public string getStste(int SID)
        {
            return DB.tblStates.SingleOrDefault(p => p.StateID == SID).MYNAME1;
        }
        public string getcompniy(int CID)
        {
            return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == CID && p.TenentID == TID).COMPNAME;
        }
        public string getcity(int GCID)
        {
            return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID && p.TenentID == TID).CITY;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {          
            if (ViewState["Edit"] != null)
            {
                int ID = Convert.ToInt32(ViewState["Edit"]);
                Database.tbl_Account_Mst objtbl_Account_Mst = DB.tbl_Account_Mst.Single(p => p.ID == ID && p.TenentID == TID);
                // objtbl_Account_Mst.ID = Convert.ToInt32(drpID.SelectedValue);
                objtbl_Account_Mst.Name = txtName.Text;
                //objtbl_Account_Mst.Phone_Office = txtPhone_Office.Text;
                //objtbl_Account_Mst.Phone = txtPhone.Text;
                //objtbl_Account_Mst.Phone_Fax = txtPhone_Fax.Text;
                //objtbl_Account_Mst.Phone_Alternate = txtPhone_Alternate.Text;
                //objtbl_Account_Mst.Website = txtWebsite.Text;
                //objtbl_Account_Mst.Email1 = txtEmail1.Text;
                //objtbl_Account_Mst.Email2 = txtEmail2.Text;
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
                objtbl_Account_Mst.Billing_Address_State = txtBilling_Address_State.Text;
                objtbl_Account_Mst.Billing_Address_Postalcode = txtBilling_Address_Postalcode.Text;
                objtbl_Account_Mst.Billing_Address_Country = txtBilling_Address_Country.Text;
                objtbl_Account_Mst.Shipping_Address_Street = txtShipping_Address_Street.Text;
                objtbl_Account_Mst.Shipping_Address_City = txtShipping_Address_City.Text;
                objtbl_Account_Mst.Shipping_Address_State = txtShipping_Address_State.Text;
                objtbl_Account_Mst.Shipping_Address_Postalcode = txtShipping_Address_Postalcode.Text;
                objtbl_Account_Mst.Shipping_Address_Country = txtShipping_Address_Country.Text;
                objtbl_Account_Mst.ParentName = txtParentName.Text;
                objtbl_Account_Mst.DateEntered = Convert.ToDateTime(txtDateEntered.Text);
                objtbl_Account_Mst.DateModified = Convert.ToDateTime(txtDateModified.Text);
                objtbl_Account_Mst.Description = txtDescription.Text;
                objtbl_Account_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                objtbl_Account_Mst.Assigned_to_Name = txtAssigned_to_Name.Text;
                objtbl_Account_Mst.CreatedBy = 1;
                objtbl_Account_Mst.ModifiedBy = 1;
                objtbl_Account_Mst.ContactName = txtContactName.Text;
                objtbl_Account_Mst.Active = true;
                objtbl_Account_Mst.Deleted = true;



                ViewState["Edit"] = null;
                btnAdd.Text = "Add New";
                DB.SaveChanges();
                //lblMsg.Text = "  Data Edit Successfully";
                //pnlSuccessMsg.Visible = true;
            }
            else
            {
                Database.tbl_Account_Mst objtbl_Account_Mst = new Database.tbl_Account_Mst();
                //Server Content Send data Yogesh
                objtbl_Account_Mst.ID = DB.tbl_Account_Mst.Where(p=>p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Account_Mst.Where(p=>p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                objtbl_Account_Mst.Name = txtName.Text;
                objtbl_Account_Mst.TenentID = TID;
                //objtbl_Account_Mst.Phone_Office = txtPhone_Office.Text;
                //objtbl_Account_Mst.Phone = txtPhone.Text;
                //objtbl_Account_Mst.Phone_Fax = txtPhone_Fax.Text;
                //objtbl_Account_Mst.Phone_Alternate = txtPhone_Alternate.Text;
                //objtbl_Account_Mst.Website = txtWebsite.Text;
                //objtbl_Account_Mst.Email1 = txtEmail1.Text;
                //objtbl_Account_Mst.Email2 = txtEmail2.Text;
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
                objtbl_Account_Mst.Billing_Address_State = txtBilling_Address_State.Text;
                objtbl_Account_Mst.Billing_Address_Postalcode = txtBilling_Address_Postalcode.Text;
                objtbl_Account_Mst.Billing_Address_Country = txtBilling_Address_Country.Text;
                objtbl_Account_Mst.Shipping_Address_Street = txtShipping_Address_Street.Text;
                objtbl_Account_Mst.Shipping_Address_City = txtShipping_Address_City.Text;
                objtbl_Account_Mst.Shipping_Address_State = txtShipping_Address_State.Text;
                objtbl_Account_Mst.Shipping_Address_Postalcode = txtShipping_Address_Postalcode.Text;
                objtbl_Account_Mst.Shipping_Address_Country = txtShipping_Address_Country.Text;
                objtbl_Account_Mst.ParentName = txtParentName.Text;
                objtbl_Account_Mst.DateEntered = Convert.ToDateTime(txtDateEntered.Text);
                objtbl_Account_Mst.DateModified = Convert.ToDateTime(txtDateModified.Text);
                objtbl_Account_Mst.Description = txtDescription.Text;
                objtbl_Account_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                objtbl_Account_Mst.Assigned_to_Name = txtAssigned_to_Name.Text;
                objtbl_Account_Mst.CreatedBy = 1;
                objtbl_Account_Mst.ModifiedBy = 1;
                objtbl_Account_Mst.ContactName = txtContactName.Text;
                objtbl_Account_Mst.Active = true;
                objtbl_Account_Mst.Deleted = true;



                DB.tbl_Account_Mst.AddObject(objtbl_Account_Mst);
                DB.SaveChanges();
                //lblMsg.Text = "  Data Save Successfully";
                //pnlSuccessMsg.Visible = true;

            }
            BindData();
            clen();
            //        scope.Complete(); //  To commit.

            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        public void clen()
        {
            txtAnnual_Revenue.Text = txtAssigned_to_Name.Text = txtBilling_Address_City.Text = txtBilling_Address_Country.Text = txtBilling_Address_Postalcode.Text = txtBilling_Address_State.Text = txtBilling_Address_Street.Text = txtContactName.Text = txtDateEntered.Text = txtDateModified.Text = txtDescription.Text =  "";
            txtEmployee.Text = txtName.Text = txtOwnership.Text = txtParentName.Text =  txtRating.Text = txtShipping_Address_City.Text = txtShipping_Address_Country.Text = txtShipping_Address_Postalcode.Text = txtShipping_Address_State.Text = txtShipping_Address_Street.Text = txtSicCode.Text = txtTickerSymbol.Text =  "";
            drpAccountType.SelectedIndex = 0;
            drpIndustryID.SelectedIndex = 0;
            drpTeamName .SelectedIndex= 0;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
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
            //code
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //code
            }

        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                //lblMsg.Text = "This is first record";
                //pnlSuccessMsg.Visible = true;
                //code
            }
            else
            {
                //pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                //code
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //code
        }
        #endregion
    }
}
