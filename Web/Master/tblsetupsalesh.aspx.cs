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

namespace Web.Master
{
    public partial class tblsetupsalesh : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
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
                fillcontrolId();
                btnAdd.ValidationGroup = "ss";
                //  FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.tblsetupsalesh> List = DB.tblsetupsaleshes.Where(m => m.TenentID == TID).OrderBy(m => m.transid).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblmodule1s.Attributes["class"] = lblDeliveryLocation1s.Attributes["class"] = lblPaymentDays1s.Attributes["class"] = lblReminderDays1s.Attributes["class"] = lblDescWithWarantee1s.Attributes["class"] = lblDescWithSerial1s.Attributes["class"] = lblDescWithColor1s.Attributes["class"] = lblAllowMinusQty1s.Attributes["class"] = lblHeaderLine1s.Attributes["class"] = lblTagLine1s.Attributes["class"] = lblBottomTagLine1s.Attributes["class"] = lblPaymentDetails1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblSalesAdminID1s.Attributes["class"] = lblDeliveryPrintURL1s.Attributes["class"] = lblCounterName1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblTenentID2h.Attributes["class"] = lblmodule2h.Attributes["class"] = lblDeliveryLocation2h.Attributes["class"] = lblPaymentDays2h.Attributes["class"] = lblReminderDays2h.Attributes["class"] = lblDescWithWarantee2h.Attributes["class"] = lblDescWithSerial2h.Attributes["class"] = lblDescWithColor2h.Attributes["class"] = lblAllowMinusQty2h.Attributes["class"] = lblHeaderLine2h.Attributes["class"] = lblTagLine2h.Attributes["class"] = lblBottomTagLine2h.Attributes["class"] = lblPaymentDetails2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblSalesAdminID2h.Attributes["class"] = lblDeliveryPrintURL2h.Attributes["class"] = lblCounterName2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblmodule1s.Attributes["class"] = lblDeliveryLocation1s.Attributes["class"] = lblPaymentDays1s.Attributes["class"] = lblReminderDays1s.Attributes["class"] = lblDescWithWarantee1s.Attributes["class"] = lblDescWithSerial1s.Attributes["class"] = lblDescWithColor1s.Attributes["class"] = lblAllowMinusQty1s.Attributes["class"] = lblHeaderLine1s.Attributes["class"] = lblTagLine1s.Attributes["class"] = lblBottomTagLine1s.Attributes["class"] = lblPaymentDetails1s.Attributes["class"] = lblCOUNTRYID1s.Attributes["class"] = lblSalesAdminID1s.Attributes["class"] = lblDeliveryPrintURL1s.Attributes["class"] = lblCounterName1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblTenentID2h.Attributes["class"] = lblmodule2h.Attributes["class"] = lblDeliveryLocation2h.Attributes["class"] = lblPaymentDays2h.Attributes["class"] = lblReminderDays2h.Attributes["class"] = lblDescWithWarantee2h.Attributes["class"] = lblDescWithSerial2h.Attributes["class"] = lblDescWithColor2h.Attributes["class"] = lblAllowMinusQty2h.Attributes["class"] = lblHeaderLine2h.Attributes["class"] = lblTagLine2h.Attributes["class"] = lblBottomTagLine2h.Attributes["class"] = lblPaymentDetails2h.Attributes["class"] = lblCOUNTRYID2h.Attributes["class"] = lblSalesAdminID2h.Attributes["class"] = lblDeliveryPrintURL2h.Attributes["class"] = lblCounterName2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  getshow";
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

        public void Clear()
        {
            //drplocation.SelectedIndex = 0;
            //txttransid.Text = "";
            //txttranssubid.Text = "";
            drpmodule.SelectedIndex = 0;
            drpdeliverylocation.SelectedIndex = 0;
            // txtCompniID.Text = "";
            //  txtLastClosePeriod.Text = "";
            // drpcurrentperiod.SelectedIndex = 0;
            txtPaymentDays.Text = "";
            txtReminderDays.Text = "";
            //  txtAcceptWarantee.Text = "";
            cbDescWithWarantee.Checked = false;
            cbDescWithSerial.Checked = false;
            cbDescWithColor.Checked = false;
            cbAllowMinusQty.Checked = false;
            txtHeaderLine.Text = "";
            txtTagLine.Text = "";
            txtBottomTagLine.Text = "";
            txtPaymentDetails.Text = "";
            // txtTCQuotation.Text = "";
            //  txtIntroLetter.Text = "";
            drpcountry.SelectedIndex = 0;
            drpsaleadmin.SelectedIndex = 0;
            // txtCRUP_ID.Text = "";
            // txtInvoicePrintURL.Text = "";
            txtDeliveryPrintURL.Text = "";
            txtCounterName.Text = "";
            //  txtEmployeeId.Text = "";
            //  txtDeftCoustomer.Text = "";
            //  cbCreated.Checked = false;
            //  txtDateTime.Text = "";
            cbActive.Checked = false;
            //cbDeleted.Checked = false;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //if (btnAdd.Text == "AddNew")
                //{

                //    Write();
                //    Clear();
                //    btnAdd.Text = "Add";
                //    btnAdd.ValidationGroup = "submit";
                //}
                //else if (btnAdd.Text == "Add")
                //{

                //    Database.tblsetupsalesh objtblsetupsalesh = new Database.tblsetupsalesh();
                //    int MAXID = DB.tblsetupsaleshes.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblsetupsaleshes.Where(p => p.TenentID == TID).Max(p => p.transid) + 1) : 1;
                //    //Server Content Send data Yogesh
                //    objtblsetupsalesh.TenentID = TID;
                //    //objtblsetupsalesh.locationID = Convert.ToInt32(drplocation.SelectedValue);
                //    objtblsetupsalesh.transid = MAXID;
                //    objtblsetupsalesh.transsubid = MAXID;
                //    objtblsetupsalesh.module = Convert.ToInt32(drpmodule.SelectedValue);
                //    objtblsetupsalesh.DeliveryLocation = Convert.ToInt32(drpdeliverylocation.SelectedValue);
                //    objtblsetupsalesh.CompniID = 826667;
                //    objtblsetupsalesh.LastClosePeriod = 124113;
                //    // objtblsetupsalesh.CurrentPeriod = Convert.ToInt32(drpcurrentperiod.SelectedValue);
                //    objtblsetupsalesh.PaymentDays = Convert.ToInt32(txtPaymentDays.Text);
                //    objtblsetupsalesh.ReminderDays = Convert.ToInt32(txtReminderDays.Text);
                //    objtblsetupsalesh.AcceptWarantee = 1;
                //    objtblsetupsalesh.DescWithWarantee = cbDescWithWarantee.Checked;
                //    objtblsetupsalesh.DescWithSerial = cbDescWithSerial.Checked;
                //    objtblsetupsalesh.DescWithColor = cbDescWithColor.Checked;
                //    objtblsetupsalesh.AllowMinusQty = cbAllowMinusQty.Checked;
                //    objtblsetupsalesh.HeaderLine = txtHeaderLine.Text;
                //    objtblsetupsalesh.TagLine = txtTagLine.Text;
                //    objtblsetupsalesh.BottomTagLine = txtBottomTagLine.Text;
                //    objtblsetupsalesh.PaymentDetails = txtPaymentDetails.Text;
                //    //  objtblsetupsalesh.TCQuotation = txtTCQuotation.Text;
                //    //  objtblsetupsalesh.IntroLetter = txtIntroLetter.Text;
                //    objtblsetupsalesh.COUNTRYID = Convert.ToInt32(drpcountry.SelectedValue);
                //    objtblsetupsalesh.SalesAdminID = Convert.ToInt32(drpsaleadmin.SelectedValue);
                //    //   objtblsetupsalesh.CRUP_ID =Convert.ToInt64 (txtCRUP_ID.Text);
                //    //  objtblsetupsalesh.InvoicePrintURL = txtInvoicePrintURL.Text;
                //    objtblsetupsalesh.DeliveryPrintURL = txtDeliveryPrintURL.Text;
                //    objtblsetupsalesh.CounterName = txtCounterName.Text;
                //    //  objtblsetupsalesh.EmployeeId = Convert.ToInt32(txtEmployeeId.Text);
                //    //  objtblsetupsalesh.DeftCoustomer = Convert.ToInt32(txtDeftCoustomer.Text);
                //    //  objtblsetupsalesh.Created = Convert.ToInt32(cbCreated.Checked);
                //    //   objtblsetupsalesh.DateTime = Convert.ToDateTime(txtDateTime.Text);
                //    objtblsetupsalesh.Active = cbActive.Checked;
                //    //  objtblsetupsalesh.Deleted = cbDeleted.Checked;
                //    DB.tblsetupsaleshes.AddObject(objtblsetupsalesh);
                //    DB.SaveChanges();
                //    Clear();
                //    btnAdd.Text = "AddNew";
                //    btnAdd.ValidationGroup = "ss";
                //    lblMsg.Text = "  Data Save Successfully";
                //    pnlSuccessMsg.Visible = true;
                //    BindData();
                //    // navigation.Visible = true;
                //    Readonly();
                //    // FirstData();
                //}
                 if (btnAdd.Text == "Update Setting")
                {

                    if (ViewState["Edit"] != null)
                    {

                        string[] IDd = ViewState["Edit"].ToString().Split(',');
                        int Location = Convert.ToInt32(IDd[0]);
                        int TransID = Convert.ToInt32(IDd[1]);
                        int TranssubID = Convert.ToInt32(IDd[2]);

                        Database.tblsetupsalesh objtblsetupsalesh = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.locationID == Location && p.transid == TransID && p.transsubid == TranssubID);
                        objtblsetupsalesh.module = 10;//Convert.ToInt32(drpmodule.SelectedValue);
                        objtblsetupsalesh.DeliveryLocation = Convert.ToInt32(drpdeliverylocation.SelectedValue);
                        objtblsetupsalesh.CompniID = 826667;
                        objtblsetupsalesh.LastClosePeriod = 124113;
                        //   objtblsetupsalesh.CurrentPeriod = Convert.ToInt32(drpcurrentperiod.SelectedValue);
                        objtblsetupsalesh.PaymentDays = Convert.ToInt32(txtPaymentDays.Text);
                        objtblsetupsalesh.ReminderDays = Convert.ToInt32(txtReminderDays.Text);
                        objtblsetupsalesh.AcceptWarantee = 1;
                        objtblsetupsalesh.DescWithWarantee = cbDescWithWarantee.Checked;
                        objtblsetupsalesh.DescWithSerial = cbDescWithSerial.Checked;
                        objtblsetupsalesh.DescWithColor = cbDescWithColor.Checked;
                        objtblsetupsalesh.AllowMinusQty = cbAllowMinusQty.Checked;
                        objtblsetupsalesh.HeaderLine = txtHeaderLine.Text;
                        objtblsetupsalesh.TagLine = txtTagLine.Text;
                        objtblsetupsalesh.BottomTagLine = txtBottomTagLine.Text;
                        objtblsetupsalesh.PaymentDetails = txtPaymentDetails.Text;
                        //  objtblsetupsalesh.TCQuotation = txtTCQuotation.Text;
                        //  objtblsetupsalesh.IntroLetter = txtIntroLetter.Text;
                        objtblsetupsalesh.COUNTRYID = Convert.ToInt32(drpcountry.SelectedValue);
                        objtblsetupsalesh.SalesAdminID = Convert.ToInt32(drpsaleadmin.SelectedValue);
                        //   objtblsetupsalesh.CRUP_ID = Convert.ToInt64(txtCRUP_ID.Text);
                        //   objtblsetupsalesh.InvoicePrintURL = txtInvoicePrintURL.Text;
                        objtblsetupsalesh.DeliveryPrintURL = txtDeliveryPrintURL.Text;
                        objtblsetupsalesh.CounterName = txtCounterName.Text;
                        //  objtblsetupsalesh.EmployeeId = Convert.ToInt32(txtEmployeeId.Text);
                        //  objtblsetupsalesh.DeftCoustomer = Convert.ToInt32(txtDeftCoustomer.Text);
                        //  objtblsetupsalesh.Created = Convert.ToInt32(cbCreated.Checked);
                        //  objtblsetupsalesh.DateTime = Convert.ToDateTime(txtDateTime.Text);
                        objtblsetupsalesh.Active = cbActive.Checked;
                        //objtblsetupsalesh.Deleted = cbDeleted.Checked;

                        ViewState["Edit"] = null;
                        btnAdd.Text = "Update Setting";
                        DB.SaveChanges();
                        //btnAdd.Visible = false;
                        btnAdd.ValidationGroup = "ss";
                        Clear();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        // navigation.Visible = true;
                        Readonly();
                        // FirstData();
                    }
                }
                BindData();

                scope.Complete(); //  To commit.


            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpDeleted.Items.Insert(0, new ListItem("-- Select --", "0"));drpDeleted.DataSource = DB.0;drpDeleted.DataTextField = "0";drpDeleted.DataValueField = "0";drpDeleted.DataBind();
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
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Listview1.SelectedIndex = 0;
            //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
            //txttransid.Text = Listview1.SelectedDataKey[0].ToString();
            //txttranssubid.Text = Listview1.SelectedDataKey[0].ToString();
            drpmodule.Text = Listview1.SelectedDataKey[0].ToString();
            drpdeliverylocation.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtCompniID.Text = Listview1.SelectedDataKey[0].ToString();
            // txtLastClosePeriod.Text = Listview1.SelectedDataKey[0].ToString();
            //  drpcurrentperiod.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPaymentDays.Text = Listview1.SelectedDataKey[0].ToString();
            txtReminderDays.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtAcceptWarantee.Text = Listview1.SelectedDataKey[0].ToString();
            cbDescWithWarantee.Checked = false;
            cbDescWithSerial.Checked = false;
            cbDescWithColor.Checked = false;
            cbAllowMinusQty.Checked = false;
            txtHeaderLine.Text = Listview1.SelectedDataKey[0].ToString();
            txtTagLine.Text = Listview1.SelectedDataKey[0].ToString();
            txtBottomTagLine.Text = Listview1.SelectedDataKey[0].ToString();
            txtPaymentDetails.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtTCQuotation.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtIntroLetter.Text = Listview1.SelectedDataKey[0].ToString();
            drpcountry.Text = Listview1.SelectedDataKey[0].ToString();
            drpsaleadmin.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtInvoicePrintURL.Text = Listview1.SelectedDataKey[0].ToString();
            txtDeliveryPrintURL.Text = Listview1.SelectedDataKey[0].ToString();
            txtCounterName.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtEmployeeId.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtDeftCoustomer.Text = Listview1.SelectedDataKey[0].ToString();
            //  cbCreated.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            cbActive.Checked = false;
            //  cbDeleted.Checked = false;

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
                //txttransid.Text = Listview1.SelectedDataKey[0].ToString();
                //txttranssubid.Text = Listview1.SelectedDataKey[0].ToString();
                drpmodule.Text = Listview1.SelectedDataKey[0].ToString();
                drpdeliverylocation.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtCompniID.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtLastClosePeriod.Text = Listview1.SelectedDataKey[0].ToString();
                //  drpcurrentperiod.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtPaymentDays.Text = Listview1.SelectedDataKey[0].ToString();
                txtReminderDays.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtAcceptWarantee.Text = Listview1.SelectedDataKey[0].ToString();
                cbDescWithWarantee.Checked = false;
                cbDescWithSerial.Checked = false;
                cbDescWithColor.Checked = false;
                cbAllowMinusQty.Checked = false;
                txtHeaderLine.Text = Listview1.SelectedDataKey[0].ToString();
                txtTagLine.Text = Listview1.SelectedDataKey[0].ToString();
                txtBottomTagLine.Text = Listview1.SelectedDataKey[0].ToString();
                txtPaymentDetails.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtTCQuotation.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtIntroLetter.Text = Listview1.SelectedDataKey[0].ToString();
                drpcountry.Text = Listview1.SelectedDataKey[0].ToString();
                drpsaleadmin.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtInvoicePrintURL.Text = Listview1.SelectedDataKey[0].ToString();
                txtDeliveryPrintURL.Text = Listview1.SelectedDataKey[0].ToString();
                txtCounterName.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtEmployeeId.Text = Listview1.SelectedDataKey[0].ToString();
                // txtDeftCoustomer.Text = Listview1.SelectedDataKey[0].ToString();
                // cbCreated.Text = Listview1.SelectedDataKey[0].ToString();
                // txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                cbActive.Checked = false;
                //  cbDeleted.Checked = false;

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
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
                //txttransid.Text = Listview1.SelectedDataKey[0].ToString();
                //txttranssubid.Text = Listview1.SelectedDataKey[0].ToString();
                drpmodule.Text = Listview1.SelectedDataKey[0].ToString();
                drpdeliverylocation.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtCompniID.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtLastClosePeriod.Text = Listview1.SelectedDataKey[0].ToString();
                //  drpcurrentperiod.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtPaymentDays.Text = Listview1.SelectedDataKey[0].ToString();
                txtReminderDays.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtAcceptWarantee.Text = Listview1.SelectedDataKey[0].ToString();
                cbDescWithWarantee.Checked = false;
                cbDescWithSerial.Checked = false;
                cbDescWithColor.Checked = false;
                cbAllowMinusQty.Checked = false;
                txtHeaderLine.Text = Listview1.SelectedDataKey[0].ToString();
                txtTagLine.Text = Listview1.SelectedDataKey[0].ToString();
                txtBottomTagLine.Text = Listview1.SelectedDataKey[0].ToString();
                txtPaymentDetails.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtTCQuotation.Text = Listview1.SelectedDataKey[0].ToString();
                // txtIntroLetter.Text = Listview1.SelectedDataKey[0].ToString();
                drpcountry.Text = Listview1.SelectedDataKey[0].ToString();
                drpsaleadmin.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtInvoicePrintURL.Text = Listview1.SelectedDataKey[0].ToString();
                txtDeliveryPrintURL.Text = Listview1.SelectedDataKey[0].ToString();
                txtCounterName.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtEmployeeId.Text = Listview1.SelectedDataKey[0].ToString();
                // txtDeftCoustomer.Text = Listview1.SelectedDataKey[0].ToString();
                //  cbCreated.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                cbActive.Checked = false;
                //cbDeleted.Checked = false;

            }
        }
        public void LastData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
            //txttransid.Text = Listview1.SelectedDataKey[0].ToString();
            //txttranssubid.Text = Listview1.SelectedDataKey[0].ToString();
            drpmodule.Text = Listview1.SelectedDataKey[0].ToString();
            drpdeliverylocation.Text = Listview1.SelectedDataKey[0].ToString();
            // txtCompniID.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtLastClosePeriod.Text = Listview1.SelectedDataKey[0].ToString();
            //  drpcurrentperiod.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPaymentDays.Text = Listview1.SelectedDataKey[0].ToString();
            txtReminderDays.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtAcceptWarantee.Text = Listview1.SelectedDataKey[0].ToString();
            cbDescWithWarantee.Checked = false;
            cbDescWithSerial.Checked = false;
            cbDescWithColor.Checked = false;
            cbAllowMinusQty.Checked = false;
            txtHeaderLine.Text = Listview1.SelectedDataKey[0].ToString();
            txtTagLine.Text = Listview1.SelectedDataKey[0].ToString();
            txtBottomTagLine.Text = Listview1.SelectedDataKey[0].ToString();
            txtPaymentDetails.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtTCQuotation.Text = Listview1.SelectedDataKey[0].ToString();
            // txtIntroLetter.Text = Listview1.SelectedDataKey[0].ToString();
            drpcountry.Text = Listview1.SelectedDataKey[0].ToString();
            drpsaleadmin.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtInvoicePrintURL.Text = Listview1.SelectedDataKey[0].ToString();
            txtDeliveryPrintURL.Text = Listview1.SelectedDataKey[0].ToString();
            txtCounterName.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtEmployeeId.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtDeftCoustomer.Text = Listview1.SelectedDataKey[0].ToString();
            //  cbCreated.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            cbActive.Checked = false;
            //  cbDeleted.Checked = false;

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblTenentID2h.Visible = lblmodule2h.Visible = lblDeliveryLocation2h.Visible = lblAllowMinusQty2h.Visible = lblHeaderLine2h.Visible = lblTagLine2h.Visible = lblBottomTagLine2h.Visible = lblPaymentDetails2h.Visible = lblCOUNTRYID2h.Visible = lblSalesAdminID2h.Visible = lblDeliveryPrintURL2h.Visible = lblCounterName2h.Visible = lblActive2h.Visible = false;
                    //2true
                    txtTenentID2h.Visible = txtmodule2h.Visible = txtDeliveryLocation2h.Visible = txtPaymentDays2h.Visible = txtReminderDays2h.Visible = txtDescWithWarantee2h.Visible = txtDescWithSerial2h.Visible = txtDescWithColor2h.Visible = txtAllowMinusQty2h.Visible = txtHeaderLine2h.Visible = txtTagLine2h.Visible = txtBottomTagLine2h.Visible = txtPaymentDetails2h.Visible = txtCOUNTRYID2h.Visible = txtSalesAdminID2h.Visible = txtDeliveryPrintURL2h.Visible = txtCounterName2h.Visible = txtActive2h.Visible = true;

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
                    lblTenentID2h.Visible = lblmodule2h.Visible = lblDeliveryLocation2h.Visible = lblPaymentDays2h.Visible = lblReminderDays2h.Visible = lblDescWithWarantee2h.Visible = lblDescWithSerial2h.Visible = lblDescWithColor2h.Visible = lblAllowMinusQty2h.Visible = lblHeaderLine2h.Visible = lblTagLine2h.Visible = lblBottomTagLine2h.Visible = lblPaymentDetails2h.Visible = lblCOUNTRYID2h.Visible = lblSalesAdminID2h.Visible = lblDeliveryPrintURL2h.Visible = lblCounterName2h.Visible = lblActive2h.Visible = true;
                    //2false
                    txtTenentID2h.Visible = txtmodule2h.Visible = txtDeliveryLocation2h.Visible = txtPaymentDays2h.Visible = txtReminderDays2h.Visible = txtDescWithWarantee2h.Visible = txtDescWithSerial2h.Visible = txtDescWithColor2h.Visible = txtAllowMinusQty2h.Visible = txtHeaderLine2h.Visible = txtTagLine2h.Visible = txtBottomTagLine2h.Visible = txtPaymentDetails2h.Visible = txtCOUNTRYID2h.Visible = txtSalesAdminID2h.Visible = txtDeliveryPrintURL2h.Visible = txtCounterName2h.Visible = txtActive2h.Visible = false;

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
                    lblmodule1s.Visible = lblDeliveryLocation1s.Visible = lblPaymentDays1s.Visible = lblReminderDays1s.Visible = lblDescWithWarantee1s.Visible = lblDescWithSerial1s.Visible = lblDescWithColor1s.Visible = lblAllowMinusQty1s.Visible = lblHeaderLine1s.Visible = lblTagLine1s.Visible = lblBottomTagLine1s.Visible = lblPaymentDetails1s.Visible = lblCOUNTRYID1s.Visible = lblSalesAdminID1s.Visible = lblDeliveryPrintURL1s.Visible = lblCounterName1s.Visible = lblActive1s.Visible = false;
                    //1true
                    txtmodule1s.Visible = txtDeliveryLocation1s.Visible = txtPaymentDays1s.Visible = txtReminderDays1s.Visible = txtDescWithWarantee1s.Visible = txtDescWithSerial1s.Visible = txtDescWithColor1s.Visible = txtAllowMinusQty1s.Visible = txtHeaderLine1s.Visible = txtTagLine1s.Visible = txtBottomTagLine1s.Visible = txtPaymentDetails1s.Visible = txtCOUNTRYID1s.Visible = txtSalesAdminID1s.Visible = txtDeliveryPrintURL1s.Visible = txtCounterName1s.Visible = txtActive1s.Visible = true;
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
                    lblmodule1s.Visible = lblDeliveryLocation1s.Visible = lblPaymentDays1s.Visible = lblReminderDays1s.Visible = lblDescWithWarantee1s.Visible = lblDescWithSerial1s.Visible = lblDescWithColor1s.Visible = lblAllowMinusQty1s.Visible = lblHeaderLine1s.Visible = lblTagLine1s.Visible = lblBottomTagLine1s.Visible = lblPaymentDetails1s.Visible = lblCOUNTRYID1s.Visible = lblSalesAdminID1s.Visible = lblDeliveryPrintURL1s.Visible = lblCounterName1s.Visible = lblActive1s.Visible = true;
                    //1false
                    txtmodule1s.Visible = txtDeliveryLocation1s.Visible = txtPaymentDays1s.Visible = txtReminderDays1s.Visible = txtDescWithWarantee1s.Visible = txtDescWithSerial1s.Visible = txtDescWithColor1s.Visible = txtAllowMinusQty1s.Visible = txtHeaderLine1s.Visible = txtTagLine1s.Visible = txtBottomTagLine1s.Visible = txtPaymentDetails1s.Visible = txtCOUNTRYID1s.Visible = txtSalesAdminID1s.Visible = txtDeliveryPrintURL1s.Visible = txtCounterName1s.Visible = txtActive1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblsetupsalesh").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {

                //if (lbllocationID1s.ID == item.LabelID)
                //    txtlocationID1s.Text = lbllocationID1s.Text = lbllocationID1s.Text = item.LabelName;
                //else if (lbltransid1s.ID == item.LabelID)
                //    txttransid1s.Text = lbltransid1s.Text = lbltransid1s.Text = item.LabelName;
                //else if (lbltranssubid1s.ID == item.LabelID)
                //    txttranssubid1s.Text = lbltranssubid1s.Text = lbltranssubid1s.Text = item.LabelName;
                if (lblmodule1s.ID == item.LabelID)
                    txtmodule1s.Text = lblmodule1s.Text = lblmodule1s.Text = item.LabelName;
                else if (lblDeliveryLocation1s.ID == item.LabelID)
                    txtDeliveryLocation1s.Text = lblDeliveryLocation1s.Text = lblDeliveryLocation1s.Text = item.LabelName;
                //else if (lblCompniID1s.ID == item.LabelID)
                //    txtCompniID1s.Text = lblCompniID1s.Text = lblhCompniID.Text = item.LabelName;
                //else if (lblLastClosePeriod1s.ID == item.LabelID)
                //    txtLastClosePeriod1s.Text = lblLastClosePeriod1s.Text = lblLastClosePeriod1s.Text = item.LabelName;
                //else if (lblCurrentPeriod1s.ID == item.LabelID)
                //    txtCurrentPeriod1s.Text = lblCurrentPeriod1s.Text = lblCurrentPeriod1s.Text = item.LabelName;
                else if (lblPaymentDays1s.ID == item.LabelID)
                    txtPaymentDays1s.Text = lblPaymentDays1s.Text = lblPaymentDays1s.Text = item.LabelName;
                else if (lblReminderDays1s.ID == item.LabelID)
                    txtReminderDays1s.Text = lblReminderDays1s.Text = lblhReminderDays.Text = item.LabelName;
                //else if (lblAcceptWarantee1s.ID == item.LabelID)
                //    txtAcceptWarantee1s.Text = lblAcceptWarantee1s.Text = lblAcceptWarantee1s.Text = item.LabelName;
                else if (lblDescWithWarantee1s.ID == item.LabelID)
                    txtDescWithWarantee1s.Text = lblDescWithWarantee1s.Text = lblDescWithWarantee1s.Text = item.LabelName;
                else if (lblDescWithSerial1s.ID == item.LabelID)
                    txtDescWithSerial1s.Text = lblDescWithSerial1s.Text = lblDescWithSerial1s.Text = item.LabelName;
                else if (lblDescWithColor1s.ID == item.LabelID)
                    txtDescWithColor1s.Text = lblDescWithColor1s.Text = lblDescWithColor1s.Text = item.LabelName;
                else if (lblAllowMinusQty1s.ID == item.LabelID)
                    txtAllowMinusQty1s.Text = lblAllowMinusQty1s.Text = lblAllowMinusQty1s.Text = item.LabelName;
                else if (lblHeaderLine1s.ID == item.LabelID)
                    txtHeaderLine1s.Text = lblHeaderLine1s.Text = lblHeaderLine1s.Text = item.LabelName;
                else if (lblTagLine1s.ID == item.LabelID)
                    txtTagLine1s.Text = lblTagLine1s.Text = lblTagLine1s.Text = item.LabelName;
                else if (lblBottomTagLine1s.ID == item.LabelID)
                    txtBottomTagLine1s.Text = lblBottomTagLine1s.Text = lblBottomTagLine1s.Text = item.LabelName;
                else if (lblPaymentDetails1s.ID == item.LabelID)
                    txtPaymentDetails1s.Text = lblPaymentDetails1s.Text = lblPaymentDetails1s.Text = item.LabelName;
                //else if (lblTCQuotation1s.ID == item.LabelID)
                //    txtTCQuotation1s.Text = lblTCQuotation1s.Text = lblTCQuotation1s.Text = item.LabelName;
                //else if (lblIntroLetter1s.ID == item.LabelID)
                //    txtIntroLetter1s.Text = lblIntroLetter1s.Text = lblIntroLetter1s.Text = item.LabelName;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    txtCOUNTRYID1s.Text = lblCOUNTRYID1s.Text = lblhCOUNTRYID.Text = item.LabelName;
                else if (lblSalesAdminID1s.ID == item.LabelID)
                    txtSalesAdminID1s.Text = lblSalesAdminID1s.Text = lblhSalesAdminID.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                //else if (lblInvoicePrintURL1s.ID == item.LabelID)
                //    txtInvoicePrintURL1s.Text = lblInvoicePrintURL1s.Text = lblInvoicePrintURL1s.Text = item.LabelName;
                else if (lblDeliveryPrintURL1s.ID == item.LabelID)
                    txtDeliveryPrintURL1s.Text = lblDeliveryPrintURL1s.Text = lblDeliveryPrintURL1s.Text = item.LabelName;
                else if (lblCounterName1s.ID == item.LabelID)
                    txtCounterName1s.Text = lblCounterName1s.Text = lblhCounterName.Text = item.LabelName;
                //else if (lblEmployeeId1s.ID == item.LabelID)
                //    txtEmployeeId1s.Text = lblEmployeeId1s.Text = lblEmployeeId1s.Text = item.LabelName;
                //else if (lblDeftCoustomer1s.ID == item.LabelID)
                //    txtDeftCoustomer1s.Text = lblDeftCoustomer1s.Text = lblDeftCoustomer1s.Text = item.LabelName;
                //else if (lblCreated1s.ID == item.LabelID)
                //    txtCreated1s.Text = lblCreated1s.Text = lblCreated1s.Text = item.LabelName;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    txtDateTime1s.Text = lblDateTime1s.Text = lblDateTime1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    txtDeleted1s.Text = lblDeleted1s.Text = lblDeleted1s.Text = item.LabelName;

                else if (lblTenentID2h.ID == item.LabelID)
                    txtTenentID2h.Text = lblTenentID2h.Text = lblTenentID2h.Text = item.LabelName;
                //else if (lbllocationID2h.ID == item.LabelID)
                //    txtlocationID2h.Text = lbllocationID2h.Text = lbllocationID2h.Text = item.LabelName;
                //else if (lbltransid2h.ID == item.LabelID)
                //    txttransid2h.Text = lbltransid2h.Text = lbltransid2h.Text = item.LabelName;
                //else if (lbltranssubid2h.ID == item.LabelID)
                //    txttranssubid2h.Text = lbltranssubid2h.Text = lbltranssubid2h.Text = item.LabelName;
                else if (lblmodule2h.ID == item.LabelID)
                    txtmodule2h.Text = lblmodule2h.Text = lblmodule2h.Text = item.LabelName;
                else if (lblDeliveryLocation2h.ID == item.LabelID)
                    txtDeliveryLocation2h.Text = lblDeliveryLocation2h.Text = lblDeliveryLocation2h.Text = item.LabelName;
                //else if (lblCompniID2h.ID == item.LabelID)
                //    txtCompniID2h.Text = lblCompniID2h.Text = lblhCompniID.Text = item.LabelName;
                //else if (lblLastClosePeriod2h.ID == item.LabelID)
                //    txtLastClosePeriod2h.Text = lblLastClosePeriod2h.Text = lblLastClosePeriod2h.Text = item.LabelName;
                //else if (lblCurrentPeriod2h.ID == item.LabelID)
                //    txtCurrentPeriod2h.Text = lblCurrentPeriod2h.Text = lblCurrentPeriod2h.Text = item.LabelName;
                else if (lblPaymentDays2h.ID == item.LabelID)
                    txtPaymentDays2h.Text = lblPaymentDays2h.Text = lblPaymentDays2h.Text = item.LabelName;
                else if (lblReminderDays2h.ID == item.LabelID)
                    txtReminderDays2h.Text = lblReminderDays2h.Text = lblhReminderDays.Text = item.LabelName;
                //else if (lblAcceptWarantee2h.ID == item.LabelID)
                //    txtAcceptWarantee2h.Text = lblAcceptWarantee2h.Text = lblAcceptWarantee2h.Text = item.LabelName;
                else if (lblDescWithWarantee2h.ID == item.LabelID)
                    txtDescWithWarantee2h.Text = lblDescWithWarantee2h.Text = lblDescWithWarantee2h.Text = item.LabelName;
                else if (lblDescWithSerial2h.ID == item.LabelID)
                    txtDescWithSerial2h.Text = lblDescWithSerial2h.Text = lblDescWithSerial2h.Text = item.LabelName;
                else if (lblDescWithColor2h.ID == item.LabelID)
                    txtDescWithColor2h.Text = lblDescWithColor2h.Text = lblDescWithColor2h.Text = item.LabelName;
                else if (lblAllowMinusQty2h.ID == item.LabelID)
                    txtAllowMinusQty2h.Text = lblAllowMinusQty2h.Text = lblAllowMinusQty2h.Text = item.LabelName;
                else if (lblHeaderLine2h.ID == item.LabelID)
                    txtHeaderLine2h.Text = lblHeaderLine2h.Text = lblHeaderLine2h.Text = item.LabelName;
                else if (lblTagLine2h.ID == item.LabelID)
                    txtTagLine2h.Text = lblTagLine2h.Text = lblTagLine2h.Text = item.LabelName;
                else if (lblBottomTagLine2h.ID == item.LabelID)
                    txtBottomTagLine2h.Text = lblBottomTagLine2h.Text = lblBottomTagLine2h.Text = item.LabelName;
                else if (lblPaymentDetails2h.ID == item.LabelID)
                    txtPaymentDetails2h.Text = lblPaymentDetails2h.Text = lblPaymentDetails2h.Text = item.LabelName;
                //else if (lblTCQuotation2h.ID == item.LabelID)
                //    txtTCQuotation2h.Text = lblTCQuotation2h.Text = lblTCQuotation2h.Text = item.LabelName;
                //else if (lblIntroLetter2h.ID == item.LabelID)
                //    txtIntroLetter2h.Text = lblIntroLetter2h.Text = lblIntroLetter2h.Text = item.LabelName;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    txtCOUNTRYID2h.Text = lblCOUNTRYID2h.Text = lblhCOUNTRYID.Text = item.LabelName;
                else if (lblSalesAdminID2h.ID == item.LabelID)
                    txtSalesAdminID2h.Text = lblSalesAdminID2h.Text = lblhSalesAdminID.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;
                //else if (lblInvoicePrintURL2h.ID == item.LabelID)
                //    txtInvoicePrintURL2h.Text = lblInvoicePrintURL2h.Text = lblInvoicePrintURL2h.Text = item.LabelName;
                else if (lblDeliveryPrintURL2h.ID == item.LabelID)
                    txtDeliveryPrintURL2h.Text = lblDeliveryPrintURL2h.Text = lblDeliveryPrintURL2h.Text = item.LabelName;
                else if (lblCounterName2h.ID == item.LabelID)
                    txtCounterName2h.Text = lblCounterName2h.Text = lblhCounterName.Text = item.LabelName;
                //else if (lblEmployeeId2h.ID == item.LabelID)
                //    txtEmployeeId2h.Text = lblEmployeeId2h.Text = lblEmployeeId2h.Text = item.LabelName;
                //else if (lblDeftCoustomer2h.ID == item.LabelID)
                //    txtDeftCoustomer2h.Text = lblDeftCoustomer2h.Text = lblDeftCoustomer2h.Text = item.LabelName;
                //else if (lblCreated2h.ID == item.LabelID)
                //    txtCreated2h.Text = lblCreated2h.Text = lblCreated2h.Text = item.LabelName;
                //else if (lblDateTime2h.ID == item.LabelID)
                //    txtDateTime2h.Text = lblDateTime2h.Text = lblDateTime2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    txtDeleted2h.Text = lblDeleted2h.Text = lblDeleted2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblsetupsalesh").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblsetupsalesh.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblsetupsalesh").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;


                //if (lbllocationID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationID1s.Text;
                //else if (lbltransid1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttransid1s.Text;
                //else if (lbltranssubid1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttranssubid1s.Text;
                if (lblmodule1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtmodule1s.Text;
                else if (lblDeliveryLocation1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryLocation1s.Text;
                //else if (lblCompniID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCompniID1s.Text;
                //else if (lblLastClosePeriod1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLastClosePeriod1s.Text;
                //else if (lblCurrentPeriod1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCurrentPeriod1s.Text;
                else if (lblPaymentDays1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaymentDays1s.Text;
                else if (lblReminderDays1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReminderDays1s.Text;
                //else if (lblAcceptWarantee1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtAcceptWarantee1s.Text;
                else if (lblDescWithWarantee1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescWithWarantee1s.Text;
                else if (lblDescWithSerial1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescWithSerial1s.Text;
                else if (lblDescWithColor1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescWithColor1s.Text;
                else if (lblAllowMinusQty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAllowMinusQty1s.Text;
                else if (lblHeaderLine1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeaderLine1s.Text;
                else if (lblTagLine1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTagLine1s.Text;
                else if (lblBottomTagLine1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBottomTagLine1s.Text;
                else if (lblPaymentDetails1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaymentDetails1s.Text;
                //else if (lblTCQuotation1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTCQuotation1s.Text;
                //else if (lblIntroLetter1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtIntroLetter1s.Text;
                else if (lblCOUNTRYID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID1s.Text;
                else if (lblSalesAdminID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalesAdminID1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;
                //else if (lblInvoicePrintURL1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtInvoicePrintURL1s.Text;
                else if (lblDeliveryPrintURL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryPrintURL1s.Text;
                else if (lblCounterName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCounterName1s.Text;
                //else if (lblEmployeeId1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEmployeeId1s.Text;
                //else if (lblDeftCoustomer1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeftCoustomer1s.Text;
                //else if (lblCreated1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCreated1s.Text;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;

                else if (lblTenentID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTenentID2h.Text;
                //else if (lbllocationID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationID2h.Text;
                //else if (lbltransid2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttransid2h.Text;
                //else if (lbltranssubid2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttranssubid2h.Text;
                else if (lblmodule2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtmodule2h.Text;
                else if (lblDeliveryLocation2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryLocation2h.Text;
                //else if (lblCompniID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCompniID2h.Text;
                //else if (lblLastClosePeriod2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLastClosePeriod2h.Text;
                //else if (lblCurrentPeriod2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCurrentPeriod2h.Text;
                else if (lblPaymentDays2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaymentDays2h.Text;
                else if (lblReminderDays2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReminderDays2h.Text;
                //else if (lblAcceptWarantee2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtAcceptWarantee2h.Text;
                else if (lblDescWithWarantee2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescWithWarantee2h.Text;
                else if (lblDescWithSerial2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescWithSerial2h.Text;
                else if (lblDescWithColor2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescWithColor2h.Text;
                else if (lblAllowMinusQty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAllowMinusQty2h.Text;
                else if (lblHeaderLine2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeaderLine2h.Text;
                else if (lblTagLine2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTagLine2h.Text;
                else if (lblBottomTagLine2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBottomTagLine2h.Text;
                else if (lblPaymentDetails2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPaymentDetails2h.Text;
                //else if (lblTCQuotation2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTCQuotation2h.Text;
                //else if (lblIntroLetter2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtIntroLetter2h.Text;
                else if (lblCOUNTRYID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOUNTRYID2h.Text;
                else if (lblSalesAdminID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalesAdminID2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;
                //else if (lblInvoicePrintURL2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtInvoicePrintURL2h.Text;
                else if (lblDeliveryPrintURL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeliveryPrintURL2h.Text;
                else if (lblCounterName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCounterName2h.Text;
                //else if (lblEmployeeId2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEmployeeId2h.Text;
                //else if (lblDeftCoustomer2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeftCoustomer2h.Text;
                //else if (lblCreated2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCreated2h.Text;
                //else if (lblDateTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblsetupsalesh.xml"));

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
        public void Write()
        {
            // navigation.Visible = false;
            //drplocation.Enabled = true;
            //txttransid.Enabled = true;
            //txttranssubid.Enabled = true;
            drpmodule.Enabled = true;
            drpdeliverylocation.Enabled = true;
            // txtCompniID.Enabled = true;
            // txtLastClosePeriod.Enabled = true;
            //  drpcurrentperiod.Enabled = true;
            txtPaymentDays.Enabled = true;
            txtReminderDays.Enabled = true;
            //  txtAcceptWarantee.Enabled = true;
            cbDescWithWarantee.Enabled = true;
            cbDescWithSerial.Enabled = true;
            cbDescWithColor.Enabled = true;
            cbAllowMinusQty.Enabled = true;
            txtHeaderLine.Enabled = true;
            txtTagLine.Enabled = true;
            txtBottomTagLine.Enabled = true;
            txtPaymentDetails.Enabled = true;
            //txtTCQuotation.Enabled = true;
            //txtIntroLetter.Enabled = true;
            drpcountry.Enabled = true;
            drpsaleadmin.Enabled = true;
            //txtCRUP_ID.Enabled = true;
            //txtInvoicePrintURL.Enabled = true;
            txtDeliveryPrintURL.Enabled = true;
            txtCounterName.Enabled = true;
            //txtEmployeeId.Enabled = true;
            //txtDeftCoustomer.Enabled = true;
            //cbCreated.Enabled = true;
            //txtDateTime.Enabled = true;
            cbActive.Enabled = true;
            // cbDeleted.Enabled = true;

        }
        public void Readonly()
        {
            // navigation.Visible = true;
            //drplocation.Enabled = false;
            //txttransid.Enabled = false;
            //txttranssubid.Enabled = false;
            drpmodule.Enabled = false;
            drpdeliverylocation.Enabled = false;
            //  txtCompniID.Enabled = false;
            //  txtLastClosePeriod.Enabled = false;
            // drpcurrentperiod.Enabled = false;
            txtPaymentDays.Enabled = false;
            txtReminderDays.Enabled = false;
            //  txtAcceptWarantee.Enabled = false;
            cbDescWithWarantee.Enabled = false;
            cbDescWithSerial.Enabled = false;
            cbDescWithColor.Enabled = false;
            cbAllowMinusQty.Enabled = false;
            txtHeaderLine.Enabled = false;
            txtTagLine.Enabled = false;
            txtBottomTagLine.Enabled = false;
            txtPaymentDetails.Enabled = false;
            //txtTCQuotation.Enabled = false;
            //txtIntroLetter.Enabled = false;
            drpcountry.Enabled = false;
            drpsaleadmin.Enabled = false;
            //txtCRUP_ID.Enabled = false;
            //txtInvoicePrintURL.Enabled = false;
            txtDeliveryPrintURL.Enabled = false;
            txtCounterName.Enabled = false;
            //txtEmployeeId.Enabled = false;
            //txtDeftCoustomer.Enabled = false;
            //cbCreated.Enabled = false;
            //txtDateTime.Enabled = false;
            cbActive.Enabled = false;
            //  cbDeleted.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblsetupsaleshes.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblsetupsaleshes.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
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

            // ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int Totalrec = DB.tblsetupsaleshes.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblsetupsaleshes.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
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
                // ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int Totalrec = DB.tblsetupsaleshes.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblsetupsaleshes.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                //  ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblsetupsaleshes.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblsetupsaleshes.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transid).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            // ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
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
            // FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (e.CommandName == "btnDelete")
                {

                    int MAXID = Convert.ToInt32(e.CommandArgument);

                    Database.tblsetupsalesh objSOJobDesc = DB.tblsetupsaleshes.Single(p => p.transid == MAXID && p.TenentID == TID);
                    DB.tblsetupsaleshes.DeleteObject(objSOJobDesc);
                    //  objSOJobDesc.Deleted = true;
                    DB.SaveChanges();
                    BindData();

                }

                if (e.CommandName == "btnEdit")
                {
                    string[] ID = e.CommandArgument.ToString().Split('-');

                    int Location = Convert.ToInt32(ID[0]);
                    int transID = Convert.ToInt32(ID[1]);
                    int transSubID = Convert.ToInt32(ID[2]);

                    Database.tblsetupsalesh objtblsetupsalesh = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == transID && p.transsubid == transSubID && p.locationID == Location);
                    drpdeliverylocation.SelectedValue = objtblsetupsalesh.DeliveryLocation.ToString();
                    // txtCompniID.Text = objtblsetupsalesh.CompniID.ToString();
                    // txtLastClosePeriod.Text = objtblsetupsalesh.LastClosePeriod.ToString();
                    // drpcurrentperiod.SelectedValue = objtblsetupsalesh.CurrentPeriod.ToString();
                    txtPaymentDays.Text = objtblsetupsalesh.PaymentDays.ToString();
                    txtReminderDays.Text = objtblsetupsalesh.ReminderDays.ToString();
                    // txtAcceptWarantee.Text = objtblsetupsalesh.AcceptWarantee.ToString();
                    cbDescWithWarantee.Checked = (objtblsetupsalesh.DescWithWarantee == true) ? true : false;
                    cbDescWithSerial.Checked = (objtblsetupsalesh.DescWithSerial == true) ? true : false;
                    cbDescWithColor.Checked = (objtblsetupsalesh.DescWithColor == true) ? true : false;
                    cbAllowMinusQty.Checked = (objtblsetupsalesh.AllowMinusQty == true) ? true : false;
                    txtHeaderLine.Text = objtblsetupsalesh.HeaderLine.ToString();
                    txtTagLine.Text = objtblsetupsalesh.TagLine.ToString();
                    txtBottomTagLine.Text = objtblsetupsalesh.BottomTagLine.ToString();
                    txtPaymentDetails.Text = objtblsetupsalesh.PaymentDetails.ToString();
                    //txtTCQuotation.Text = objtblsetupsalesh.TCQuotation.ToString();
                    //txtIntroLetter.Text = objtblsetupsalesh.IntroLetter.ToString();
                    drpcountry.SelectedValue = objtblsetupsalesh.COUNTRYID.ToString();
                    if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == objtblsetupsalesh.SalesAdminID).Count() > 0)
                        drpsaleadmin.SelectedValue = objtblsetupsalesh.SalesAdminID.ToString();
                    //txtCRUP_ID.Text = objtblsetupsalesh.CRUP_ID.ToString();
                    //txtInvoicePrintURL.Text = objtblsetupsalesh.InvoicePrintURL.ToString();
                    if (objtblsetupsalesh.DeliveryPrintURL != null)
                    txtDeliveryPrintURL.Text = objtblsetupsalesh.DeliveryPrintURL.ToString();
                    if (objtblsetupsalesh.CounterName != null)
                    txtCounterName.Text = objtblsetupsalesh.CounterName.ToString();
                    //txtEmployeeId.Text = objtblsetupsalesh.EmployeeId.ToString();
                    //txtDeftCoustomer.Text = objtblsetupsalesh.DeftCoustomer.ToString();
                    //cbCreated.Checked = false;
                    //txtDateTime.Text = objtblsetupsalesh.DateTime.ToString();
                    cbActive.Checked = (objtblsetupsalesh.Active == true) ? true : false;
                    //cbDeleted.Checked = (objtblsetupsalesh.Deleted == true) ? true : false;
                    btnAdd.Visible = true;
                    btnAdd.Text = "Update Setting";
                    ViewState["Edit"] = Location + "," + transID + "," + transSubID;
                    Write();
                }
                scope.Complete(); //  To commit.

            }
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblsetupsaleshes.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblsetupsaleshes.Where(m => m.TenentID == TID).OrderBy(m => m.transid).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                // ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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

        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int LID = Convert.ToInt32(drplocation.SelectedValue);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

        }

        protected void drpmodule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void fillcontrolId()
        {

            drpmodule.DataSource = DB.MODULE_MST.Where(p => p.TenentID == 0 && p.ACTIVE_FLAG == "Y");
            drpmodule.DataTextField = "Module_Name";
            drpmodule.DataValueField = "Module_Id";
            drpmodule.DataBind();
            drpmodule.Items.Insert(0, new ListItem("---Select ---", "0"));

            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            drpcountry.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID);
            drpcountry.DataTextField = "COUNAME1";
            drpcountry.DataValueField = "COUNTRYID";
            drpcountry.DataBind();
            drpcountry.Items.Insert(0, new ListItem("---Select ---", "0"));


            drpsaleadmin.DataSource = DB.USER_MST.Where(p => p.TenentID == TID);
            drpsaleadmin.DataTextField = "FIRST_NAME";
            drpsaleadmin.DataValueField = "USER_ID";
            drpsaleadmin.DataBind();
            drpsaleadmin.Items.Insert(0, new ListItem("---Select ---", "0"));

            drpdeliverylocation.DataSource = DB.TBLLOCATIONs.Where(p => p.TenentID == TID);
            drpdeliverylocation.DataTextField = "LOCNAME1";
            drpdeliverylocation.DataValueField = "LOCATIONID";
            drpdeliverylocation.DataBind();
            drpdeliverylocation.Items.Insert(0, new ListItem("---Select ---", "0"));
        }

        protected void drpdeliverylocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int LID = Convert.ToInt32(drplocation.SelectedValue);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

        }
        public string Catname(int COUNTRYID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.tblCOUNTRies.Where(p => p.COUNTRYID == COUNTRYID && p.TenentID == TID).Count() > 0)
            {
                return DB.tblCOUNTRies.SingleOrDefault(p => p.COUNTRYID == COUNTRYID && p.TenentID == TID).COUNAME1;
            }
            else
            {
                return "Not Found";
            }
        }

        public string catadmin(int adminID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.USER_MST.Where(p => p.USER_ID == adminID && p.TenentID == TID).Count() > 0)
            {
                return DB.USER_MST.SingleOrDefault(p => p.USER_ID == adminID && p.TenentID == TID).FIRST_NAME1;
            }
            else
            {
                return "Not Found";
            }

        }
    }
}