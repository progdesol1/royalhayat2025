using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.Services;

namespace Web.Sales
{
    public partial class PaymentVouchar : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        PropertyFile objProFile = new PropertyFile();
        public static DataTable dt_PurQuat;
        bool flag = false;

        List<Database.tblsetupsalesh> Listtblsetupsalesh = new List<tblsetupsalesh>();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
        tblActSLSetup objtbltblActSLSetup = new tblActSLSetup();
        MYCOMPANYSETUP objMyCompany = new MYCOMPANYSETUP();

        bool FirstFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, MDUID = 0;
        string LangID, CURRENCY, USERID, Crypath, UseID, UNAME = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            
            if (!IsPostBack)
            {
                FistTimeLoad();
                ViewState["TempListICTR_DT"] = null;
                ViewState["AddnewItems"] = null;
                ViewState["StrMultiSize"] = null;
                BindData();
                GridData();
                //ViewState["ICIT_BR_Perishable"] = null;
                ManageLang();
            }

            pnlSuccessMsg123.Visible = false;
        }
        public void SessionLoad()
        {

            string Ref = ((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();

            objMyCompany = ((MYCOMPANYSETUP)Session["objMYCOMPANYSETUP"]);

            UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
            UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();

            if (Session["SiteModuleID"] != null)
                MDUID = Convert.ToInt32(Session["SiteModuleID"]);
            if (objMyCompany.StockTaking == true)
                btnnewAdd.Visible = false;

            string USERID = UID.ToString();
            if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            {
                Transid = Convert.ToInt32(Request.QueryString["transid"]);
                Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
            }
            //objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            objtbltblActSLSetup = Classes.Transaction.DEfaultPaymetSetup(TID, LID, Transid, Transsubid, 10);
        }
        public void FistTimeLoad()
        {

            FirstFlag = false;
        }
        public void GridData()
        {            
            //if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            //{
            //    int Transid = Convert.ToInt32(Request.QueryString["transid"]);
            //    int Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
            //}
            grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && (p.Status == "PO" || p.Status == "RDPOCT")).OrderByDescending(p => p.TRANSDATE);
            grdPO.DataBind();
            for (int i = 0; i < grdPO.Items.Count; i++)
            {
                if (i == 0)
                {
                    Label lblMYTRANSID = (Label)grdPO.Items[i].FindControl("lblMYTRANSID");
                    stMYTRANSID = Convert.ToInt32(lblMYTRANSID.Text);
                    BindHD(stMYTRANSID);
                    BindDT(stMYTRANSID);
                    readMode();
                }
                LinkButton Print = (LinkButton)grdPO.Items[i].FindControl("Print");
                LinkButton lnkbtnPurchase_Order = (LinkButton)grdPO.Items[i].FindControl("lnkbtnPurchase_Order");
                Label lbluserid = (Label)grdPO.Items[i].FindControl("lbluserid");
                Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                //int UIDHD = Convert.ToInt32(lbluserid.Text);
                string Steate = lblStatus.Text;
                if (Steate == "DPO")
                    Print.Visible = false;
                if (Steate == "SO") lnkbtnPurchase_Order.Visible = false;
            }
        }
        public void BindHD(int MYTRANSID)
        {
            string TranType = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid).transsubtype1;
            lbllabellist.Text = TranType + " List";
            lbllistname.Text = TranType;
            var OBJICTR_HD = DB.ICTR_HD.SingleOrDefault(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID && p.ACTIVE == true && (p.Status == "PO" || p.Status == "RDPOCT"));
            drpselsmen.SelectedValue = OBJICTR_HD.ExtraField2;
            int CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
            TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
            ddlSupplier.SelectedValue = onj.COMPNAME1;
            //HiddenField3.Value = CID.ToString();
            // ddlCrmAct.SelectedValue = OBJICTR_HD.ACTIVITYCODE.ToString();

            //txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
            txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
            txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
            txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
            txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
            drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
        }
        protected void CheckRole()
        {
            drpselsmen.SelectedValue = EMPID.ToString();
            if (objtbltblActSLSetup.SalesAdminID == UID)
                drpselsmen.Enabled = true;
            else
                drpselsmen.Enabled = false;
        }
        public void BindDT(int ID)
        {           
            var List = DB.ICTR_DT.Where(p => p.MYTRANSID == ID && p.ACTIVE == true && p.TenentID == TID).ToList();

            Repeater2.DataSource = List;
            Repeater2.DataBind();

            decimal Amount = Convert.ToDecimal(List.Sum(p => p.AMOUNT));
            lblUNPtotl.Text = Amount.ToString();

            decimal Amount2 = Convert.ToDecimal(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == ID).TOTAMT);
            decimal AMTPaid = Convert.ToDecimal(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == ID).AmtPaid);
            decimal Balance = (Amount - AMTPaid);
            lblTextotl.Text = Balance.ToString();




            //decimal UInPictTotal = Convert.ToDecimal(lblcr.Text);
            //decimal TottalSum = Convert.ToDecimal(List.Sum(p => p.AMOUNT));
            //txttotxl.Text = TottalSum.ToString();
            //int QTUtotal = Convert.ToInt32(List.Sum(p => p.QUANTITY));
            //lblqtytotl.Text = QTUtotal.ToString();
            //decimal UNPTOL = Convert.ToDecimal(List.Sum(p => p.UNITPRICE));
            //lblUNPtotl.Text = UNPTOL.ToString();
            //decimal TaxTol = Convert.ToDecimal(List.Sum(p => p.TAXPER));
            //lblTextotl.Text = TaxTol.ToString();
            //lblTotatotl.Text = TottalSum.ToString();
            //for (int i = 0; i < Repeater3.Items.Count; i++)
            //{
            //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
            //    txtammunt.Text = TottalSum.ToString();
            //}
            //if (UInPictTotal != 0 && TottalSum != 0)
            //{
            //    decimal FInelOC = UInPictTotal / TottalSum;
            //    txtuintOhCost.Text = "@ " + FInelOC.ToString("N3");
            //}
            ViewState["TempListICTR_DT"] = List;
        }
        public void GetShow()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-2  getshow";
            //lblProduct1s.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier1s.Attributes["class"] = lblLocalForeign1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblProjectNo1s.Attributes["class"] = /*lblQuantity1s.Attributes["class"] = lblUnitPriceForeign1s.Attributes["class"] = lblUnitPricelocal1s.Attributes["class"] = lblDiscount1s.Attributes["class"] = lblTax1s.Attributes["class"] = lblTotalCurrencyForeign1s.Attributes["class"] = lblTotalCurrencyLocal1s.Attributes["class"] =*/ lblReference1s.Attributes["class"] = lblTerms1s.Attributes["class"] = lblCRMActivity1s.Attributes["class"] = /*lblUnitofMeasure1s.Attributes["class"] =*/ lblNo1s.Attributes["class"] = "control-label col-md-3  getshow";
            lblOHCost1s.Attributes["class"] = lblPerUnitValue1s.Attributes["class"] = lblTotal1s.Attributes["class"] = "control-label col-md-12  getshow";
            lblProductCodeProductName1s.Attributes["class"] = lblQuantity11s.Attributes["class"] = lblUnitPrice1s.Attributes["class"] = lblTax11s.Attributes["class"] = /*lblTotal11s.Attributes["class"] =*/ lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblProductCodeProductName2h.Attributes["class"] = lblQuantity12h.Attributes["class"] = lblUnitPrice2h.Attributes["class"] = lblTax12h.Attributes["class"] = /*lblTotal12h.Attributes["class"] =*/ lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  gethide";
            //lblProduct2h.Attributes["class"] = "control-label col-md-2 gethide";
            lblSupplier2h.Attributes["class"] = lblLocalForeign2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblProjectNo2h.Attributes["class"] = lblReference2h.Attributes["class"] =/* lblQuantity2h.Attributes["class"] = lblUnitPriceForeign2h.Attributes["class"] = lblUnitPricelocal2h.Attributes["class"] = lblDiscount2h.Attributes["class"] = lblTax2h.Attributes["class"] = lblTotalCurrencyForeign2h.Attributes["class"] = lblTotalCurrencyLocal2h.Attributes["class"] =*/ lblTransactionDate2h.Attributes["class"] = /*lblUnitofMeasure2h.Attributes["class"] =*/ lblTerms2h.Attributes["class"] = lblCRMActivity2h.Attributes["class"] = lblNo2h.Attributes["class"] = "control-label col-md-3 gethide";
            lblOHCost2h.Attributes["class"] = lblPerUnitValue2h.Attributes["class"] = lblTotal2h.Attributes["class"] = "control-label col-md-12 gethide";
            lblNotes2h.Attributes["class"] = "control-label col-md-1 gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");
        }
        public void GetHide()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-1  gethide";
            //lblProduct1s.Attributes["class"] = "control-label col-md-2  gethide";
            lblSupplier1s.Attributes["class"] = lblLocalForeign1s.Attributes["class"] = lblNo1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblProjectNo1s.Attributes["class"] = /*lblQuantity1s.Attributes["class"] = lblUnitPriceForeign1s.Attributes["class"] = lblUnitPricelocal1s.Attributes["class"] = lblDiscount1s.Attributes["class"] = lblTax1s.Attributes["class"] = lblTotalCurrencyForeign1s.Attributes["class"] = lblTotalCurrencyLocal1s.Attributes["class"] = */lblReference1s.Attributes["class"] = /*lblUnitofMeasure1s.Attributes["class"] =*/ lblTerms1s.Attributes["class"] = lblCRMActivity1s.Attributes["class"] = "control-label col-md-3  gethide";
            lblOHCost1s.Attributes["class"] = lblPerUnitValue1s.Attributes["class"] = lblTotal1s.Attributes["class"] = "control-label col-md-12  gethide";
            lblProductCodeProductName1s.Attributes["class"] = lblQuantity11s.Attributes["class"] = lblUnitPrice1s.Attributes["class"] = lblTax11s.Attributes["class"] = /*lblTotal11s.Attributes["class"] =*/ lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblProductCodeProductName2h.Attributes["class"] = lblQuantity12h.Attributes["class"] = lblUnitPrice2h.Attributes["class"] = lblTax12h.Attributes["class"] =/* lblTotal12h.Attributes["class"] = */lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  getshow";
            //lblProduct2h.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier2h.Attributes["class"] = lblLocalForeign2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblProjectNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = /*lblQuantity2h.Attributes["class"] = lblUnitPriceForeign2h.Attributes["class"] = lblUnitPricelocal2h.Attributes["class"] = lblDiscount2h.Attributes["class"] = lblTax2h.Attributes["class"] = lblTotalCurrencyForeign2h.Attributes["class"] = lblTotalCurrencyLocal2h.Attributes["class"] =*/ lblTransactionDate2h.Attributes["class"] = /*lblUnitofMeasure2h.Attributes["class"] =*/ lblTerms2h.Attributes["class"] = lblCRMActivity2h.Attributes["class"] = lblNo2h.Attributes["class"] = "control-label col-md-3  getshow";
            lblOHCost2h.Attributes["class"] = lblPerUnitValue2h.Attributes["class"] = lblTotal2h.Attributes["class"] = "control-label col-md-12  getshow";
            lblNotes2h.Attributes["class"] = "control-label col-md-1  getshow";
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
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //2false
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblNo2h.Visible = lblLocalForeign2h.Visible = lblCurrency2h.Visible = lblBatchNo2h.Visible = lblProjectNo2h.Visible = lblReference2h.Visible = lblTerms2h.Visible = lblCRMActivity2h.Visible = lblOHCost2h.Visible = lblPerUnitValue2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblProductCodeProductName2h.Visible = lblQuantity12h.Visible = lblUnitPrice2h.Visible = lblTax12h.Visible = /*lblTotal12h.Visible = */lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = false;
                    //2true
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtNo2h.Visible = txtLocalForeign2h.Visible = txtCurrency2h.Visible = txtBatchNo2h.Visible = txtProjectNo2h.Visible = txtReference2h.Visible = txtTerms2h.Visible = txtCRMActivity2h.Visible = txtOHCost2h.Visible = txtPerUnitValue2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtProductCodeProductName2h.Visible = txtQuantity12h.Visible = txtUnitPrice2h.Visible = txtTax12h.Visible = /*txtTotal12h.Visible =*/ txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = true;
                    //header
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblNo2h.Visible = lblLocalForeign2h.Visible = lblCurrency2h.Visible = lblBatchNo2h.Visible = lblProjectNo2h.Visible = lblReference2h.Visible = lblTerms2h.Visible = lblCRMActivity2h.Visible = lblOHCost2h.Visible = lblPerUnitValue2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblProductCodeProductName2h.Visible = lblQuantity12h.Visible = lblUnitPrice2h.Visible = lblTax12h.Visible = /*lblTotal12h.Visible =*/ lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = true;
                    //2false
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtNo2h.Visible = txtLocalForeign2h.Visible = txtCurrency2h.Visible = txtBatchNo2h.Visible = txtProjectNo2h.Visible = txtReference2h.Visible = txtTerms2h.Visible = txtCRMActivity2h.Visible = txtOHCost2h.Visible = txtPerUnitValue2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtProductCodeProductName2h.Visible = txtQuantity12h.Visible = txtUnitPrice2h.Visible = txtTax12h.Visible =/* txtTotal12h.Visible =*/ txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = false;
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblNo1s.Visible = lblLocalForeign1s.Visible = lblCurrency1s.Visible = lblBatchNo1s.Visible = lblProjectNo1s.Visible = lblReference1s.Visible = lblTerms1s.Visible = lblCRMActivity1s.Visible = lblOHCost1s.Visible = lblPerUnitValue1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblProductCodeProductName1s.Visible = lblQuantity11s.Visible = lblUnitPrice1s.Visible = lblTax11s.Visible = /*lblTotal11s.Visible =*/ lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = false;
                    //1true
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtNo1s.Visible = txtLocalForeign1s.Visible = txtCurrency1s.Visible = txtBatchNo1s.Visible = txtProjectNo1s.Visible = txtReference1s.Visible = txtTerms1s.Visible = txtCRMActivity1s.Visible = txtOHCost1s.Visible = txtPerUnitValue1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtProductCodeProductName1s.Visible = txtQuantity11s.Visible = txtUnitPrice1s.Visible = txtTax11s.Visible = /*txtTotal11s.Visible =*/ txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = true;
                    //header
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblNo1s.Visible = lblLocalForeign1s.Visible = lblCurrency1s.Visible = lblBatchNo1s.Visible = lblProjectNo1s.Visible = lblReference1s.Visible = lblTerms1s.Visible = lblCRMActivity1s.Visible = lblOHCost1s.Visible = lblPerUnitValue1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblProductCodeProductName1s.Visible = lblQuantity11s.Visible = lblUnitPrice1s.Visible = lblTax11s.Visible = /*lblTotal11s.Visible =*/ lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = true;
                    //1false
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtNo1s.Visible = txtLocalForeign1s.Visible = txtCurrency1s.Visible = txtBatchNo1s.Visible = txtProjectNo1s.Visible = txtReference1s.Visible = txtTerms1s.Visible = txtCRMActivity1s.Visible = txtOHCost1s.Visible = txtPerUnitValue1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtProductCodeProductName1s.Visible = txtQuantity11s.Visible = txtUnitPrice1s.Visible = txtTax11s.Visible =/* txtTotal11s.Visible = */txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = false;
                    //header
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((Sales_Master)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((Sales_Master)this.Master).Bindxml("paymentvouchar").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblSupplier1s.ID == item.LabelID)
                    txtSupplier1s.Text = lblSupplier1s.Text = item.LabelName;
                else if (lblTransactionDate1s.ID == item.LabelID)
                    txtTransactionDate1s.Text = lblTransactionDate1s.Text = item.LabelName;
                else if (lblNo1s.ID == item.LabelID)
                    txtNo1s.Text = lblNo1s.Text = item.LabelName;
                else if (lblLocalForeign1s.ID == item.LabelID)
                    txtLocalForeign1s.Text = lblLocalForeign1s.Text = item.LabelName;
                else if (lblCurrency1s.ID == item.LabelID)
                    txtCurrency1s.Text = lblCurrency1s.Text = item.LabelName;
                else if (lblBatchNo1s.ID == item.LabelID)
                    txtBatchNo1s.Text = lblBatchNo1s.Text = item.LabelName;
                else if (lblProjectNo1s.ID == item.LabelID)
                    txtProjectNo1s.Text = lblProjectNo1s.Text = item.LabelName;
                else if (lblReference1s.ID == item.LabelID)
                    txtReference1s.Text = lblReference1s.Text = item.LabelName;
                else if (lblTerms1s.ID == item.LabelID)
                    txtTerms1s.Text = lblTerms1s.Text = item.LabelName;
                else if (lblCRMActivity1s.ID == item.LabelID)
                    txtCRMActivity1s.Text = lblCRMActivity1s.Text = item.LabelName;
                else if (lblOHCost1s.ID == item.LabelID)
                    txtOHCost1s.Text = lblOHCost1s.Text = item.LabelName;
                else if (lblPerUnitValue1s.ID == item.LabelID)
                    txtPerUnitValue1s.Text = lblPerUnitValue1s.Text = item.LabelName;
                else if (lblTotal1s.ID == item.LabelID)
                    txtTotal1s.Text = lblTotal1s.Text = item.LabelName;
                else if (lblNotes1s.ID == item.LabelID)
                    txtNotes1s.Text = lblNotes1s.Text = item.LabelName;

                else if (lblProductCodeProductName1s.ID == item.LabelID)
                    txtProductCodeProductName1s.Text = lblProductCodeProductName1s.Text = item.LabelName;
                else if (lblQuantity11s.ID == item.LabelID)
                    txtQuantity11s.Text = lblQuantity11s.Text = item.LabelName;
                else if (lblUnitPrice1s.ID == item.LabelID)
                    txtUnitPrice1s.Text = lblUnitPrice1s.Text = item.LabelName;
                else if (lblTax11s.ID == item.LabelID)
                    txtTax11s.Text = lblTax11s.Text = item.LabelName;
                //else if (lblTotal11s.ID == item.LabelID)
                //    txtTotal11s.Text = lblTotal11s.Text = item.LabelName;
                else if (lblSupplierName1s.ID == item.LabelID)
                    txtSupplierName1s.Text = lblSupplierName1s.Text = item.LabelName;
                else if (lblOrderDate1s.ID == item.LabelID)
                    txtOrderDate1s.Text = lblOrderDate1s.Text = item.LabelName;
                else if (lblTotalAmount1s.ID == item.LabelID)
                    txtTotalAmount1s.Text = lblTotalAmount1s.Text = item.LabelName;
                else if (lblStatus1s.ID == item.LabelID)
                    txtStatus1s.Text = lblStatus1s.Text = item.LabelName;
                else if (lblEdit11s.ID == item.LabelID)
                    txtEdit11s.Text = lblEdit11s.Text = item.LabelName;

                else if (lblAddReference1s.ID == item.LabelID)
                    txtAddReference1s.Text = lblAddReference1s.Text = item.LabelName;
                else if (lblReferenceType1s.ID == item.LabelID)
                    txtReferenceType1s.Text = lblReferenceType1s.Text = item.LabelName;
                else if (lblReferenceNo1s.ID == item.LabelID)
                    txtReferenceNo1s.Text = lblReferenceNo1s.Text = item.LabelName;

                else if (lblSendMail1s.ID == item.LabelID)
                    txtSendMail1s.Text = lblSendMail1s.Text = item.LabelName;
                else if (lblToRecipients1s.ID == item.LabelID)
                    txtToRecipients1s.Text = lblToRecipients1s.Text = item.LabelName;
                else if (lblCC1s.ID == item.LabelID)
                    txtCC1s.Text = lblCC1s.Text = item.LabelName;

                else if (lblSupplier2h.ID == item.LabelID)
                    txtSupplier2h.Text = lblSupplier2h.Text = item.LabelName;
                else if (lblTransactionDate2h.ID == item.LabelID)
                    txtTransactionDate2h.Text = lblTransactionDate2h.Text = item.LabelName;
                else if (lblNo2h.ID == item.LabelID)
                    txtNo2h.Text = lblNo2h.Text = item.LabelName;
                else if (lblLocalForeign2h.ID == item.LabelID)
                    txtLocalForeign2h.Text = lblLocalForeign2h.Text = item.LabelName;
                else if (lblCurrency2h.ID == item.LabelID)
                    txtCurrency2h.Text = lblCurrency2h.Text = item.LabelName;
                else if (lblBatchNo2h.ID == item.LabelID)
                    txtBatchNo2h.Text = lblBatchNo2h.Text = item.LabelName;
                else if (lblProjectNo2h.ID == item.LabelID)
                    txtProjectNo2h.Text = lblProjectNo2h.Text = item.LabelName;
                else if (lblReference2h.ID == item.LabelID)
                    txtReference2h.Text = lblReference2h.Text = item.LabelName;
                else if (lblTerms2h.ID == item.LabelID)
                    txtTerms2h.Text = lblTerms2h.Text = item.LabelName;
                else if (lblCRMActivity2h.ID == item.LabelID)
                    txtCRMActivity2h.Text = lblCRMActivity2h.Text = item.LabelName;
                else if (lblOHCost2h.ID == item.LabelID)
                    txtOHCost2h.Text = lblOHCost2h.Text = item.LabelName;
                else if (lblPerUnitValue2h.ID == item.LabelID)
                    txtPerUnitValue2h.Text = lblPerUnitValue2h.Text = item.LabelName;
                else if (lblTotal2h.ID == item.LabelID)
                    txtTotal2h.Text = lblTotal2h.Text = item.LabelName;
                else if (lblNotes2h.ID == item.LabelID)
                    txtNotes2h.Text = lblNotes2h.Text = item.LabelName;

                else if (lblProductCodeProductName2h.ID == item.LabelID)
                    txtProductCodeProductName2h.Text = lblProductCodeProductName2h.Text = item.LabelName;
                else if (lblQuantity12h.ID == item.LabelID)
                    txtQuantity12h.Text = lblQuantity12h.Text = item.LabelName;
                else if (lblUnitPrice2h.ID == item.LabelID)
                    txtUnitPrice2h.Text = lblUnitPrice2h.Text = item.LabelName;
                else if (lblTax12h.ID == item.LabelID)
                    txtTax12h.Text = lblTax12h.Text = item.LabelName;
                //else if (lblTotal12h.ID == item.LabelID)
                //    txtTotal12h.Text = lblTotal12h.Text = item.LabelName;

                else if (lblSupplierName2h.ID == item.LabelID)
                    txtSupplierName2h.Text = lblSupplierName2h.Text = item.LabelName;
                else if (lblOrderDate2h.ID == item.LabelID)
                    txtOrderDate2h.Text = lblOrderDate2h.Text = item.LabelName;
                else if (lblTotalAmount2h.ID == item.LabelID)
                    txtTotalAmount2h.Text = lblTotalAmount2h.Text = item.LabelName;
                else if (lblStatus2h.ID == item.LabelID)
                    txtStatus2h.Text = lblStatus2h.Text = item.LabelName;
                else if (lblEdit12h.ID == item.LabelID)
                    txtEdit12h.Text = lblEdit12h.Text = item.LabelName;

                else if (lblAddReference2h.ID == item.LabelID)
                    txtAddReference2h.Text = lblAddReference2h.Text = item.LabelName;
                else if (lblReferenceType2h.ID == item.LabelID)
                    txtReferenceType2h.Text = lblReferenceType2h.Text = item.LabelName;
                else if (lblReferenceNo2h.ID == item.LabelID)
                    txtReferenceNo2h.Text = lblReferenceNo2h.Text = item.LabelName;

                else if (lblSendMail2h.ID == item.LabelID)
                    txtSendMail2h.Text = lblSendMail2h.Text = item.LabelName;
                else if (lblToRecipients2h.ID == item.LabelID)
                    txtToRecipients2h.Text = lblToRecipients2h.Text = item.LabelName;
                else if (lblCC2h.ID == item.LabelID)
                    txtCC2h.Text = lblCC2h.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((Sales_Master)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((Sales_Master)this.Master).Bindxml("paymentvouchar").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Sales\\xml\\paymentvouchar.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((Sales_Master)this.Master).Bindxml("tbl_sleshoder").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblSupplier1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplier1s.Text;
                else if (lblTransactionDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransactionDate1s.Text;
                else if (lblNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNo1s.Text;
                else if (lblLocalForeign1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLocalForeign1s.Text;
                else if (lblCurrency1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCurrency1s.Text;
                else if (lblBatchNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBatchNo1s.Text;
                else if (lblProjectNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProjectNo1s.Text;
                else if (lblReference1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReference1s.Text;
                else if (lblTerms1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTerms1s.Text;
                else if (lblCRMActivity1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRMActivity1s.Text;
                else if (lblOHCost1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOHCost1s.Text;
                else if (lblPerUnitValue1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPerUnitValue1s.Text;
                else if (lblTotal1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotal1s.Text;
                else if (lblNotes1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes1s.Text;

                else if (lblProductCodeProductName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductCodeProductName1s.Text;
                else if (lblQuantity11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity11s.Text;
                else if (lblUnitPrice1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPrice1s.Text;
                else if (lblTax11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTax11s.Text;
                //else if (lblTotal11s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTotal11s.Text;

                else if (lblSupplierName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplierName1s.Text;
                else if (lblOrderDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOrderDate1s.Text;
                else if (lblTotalAmount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalAmount1s.Text;
                else if (lblStatus1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus1s.Text;
                else if (lblEdit11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEdit11s.Text;

                else if (lblAddReference1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAddReference1s.Text;
                else if (lblReferenceType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReferenceType1s.Text;
                else if (lblReferenceNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReferenceNo1s.Text;

                else if (lblSendMail1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSendMail1s.Text;
                else if (lblToRecipients1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToRecipients1s.Text;
                else if (lblCC1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCC1s.Text;

                else if (lblSupplier2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplier2h.Text;
                else if (lblTransactionDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransactionDate2h.Text;
                else if (lblNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNo2h.Text;
                else if (lblLocalForeign2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLocalForeign2h.Text;
                else if (lblCurrency2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCurrency2h.Text;
                else if (lblBatchNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBatchNo2h.Text;
                else if (lblProjectNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProjectNo2h.Text;
                else if (lblReference2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReference2h.Text;
                else if (lblTerms2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTerms2h.Text;
                else if (lblCRMActivity2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRMActivity2h.Text;
                else if (lblOHCost2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOHCost2h.Text;
                else if (lblPerUnitValue2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPerUnitValue2h.Text;
                else if (lblTotal2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotal2h.Text;
                else if (lblNotes2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes2h.Text;

                else if (lblProductCodeProductName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductCodeProductName2h.Text;
                else if (lblQuantity12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity12h.Text;
                else if (lblUnitPrice2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPrice2h.Text;
                else if (lblTax12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTax12h.Text;
                //else if (lblTotal12h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTotal12h.Text;

                else if (lblSupplierName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplierName2h.Text;
                else if (lblOrderDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOrderDate2h.Text;
                else if (lblTotalAmount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalAmount2h.Text;
                else if (lblStatus2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus2h.Text;
                else if (lblEdit12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEdit12h.Text;

                else if (lblAddReference2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAddReference2h.Text;
                else if (lblReferenceType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReferenceType2h.Text;
                else if (lblReferenceNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReferenceNo2h.Text;

                else if (lblSendMail2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSendMail2h.Text;
                else if (lblToRecipients2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToRecipients2h.Text;
                else if (lblCC2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCC2h.Text;
            }
            ds.WriteXml(Server.MapPath("\\Sales\\xml\\paymentvouchar.xml"));
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
        public void readMode()
        {
            drpterms.Enabled = drpPsle.Enabled = drppmethod.Enabled = false;
            chbDeliNote.Enabled = drpselsmen.Enabled = false;
            //txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = false;    dipak
            ddlLocalForeign.Enabled = /*txtLocationSearch.Enabled Dipak =*/ ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = false;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = txtOHCostHD.Enabled = txttotxl.Enabled = txtcashaccountID.Enabled = txtAmount.Enabled = false;
            ddlSupplier.Enabled = false;

            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                LinkButton lnkbtndelete = (LinkButton)Repeater2.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkbtnItem = (LinkButton)Repeater2.Items[i].FindControl("lnkbtnItem");
                //lnkbtnItem.Enabled = false;
                lnkbtndelete.Enabled = false;
            }
            //for (int i = 0; i < Repeater3.Items.Count; i++)
            //{
            //    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
            //TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
            //TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
            // txtCardReferenceBank.Enabled = txtammunt.Enabled = btnaddamunt.Enabled = drppaymentMeted.Enabled = false;
            //}

            btnSubmit.Visible = btnConfirmOrder.Visible = btnPrint.Visible = hlPeri.Visible = btnSaveData.Visible =/* Custmer.Visible Diapk=*/ BTNsAVEcONFsA.Visible = false;
            btnNew.Visible = true;
            btnnewAdd.Visible = true;
            btnrefesh.Visible = false;
            btnAddItemsIn.Enabled = btndiscartitems.Enabled = false;
        }
        public void WrietMode()
        {
            //ddlProduct.Enabled = ddlUOM.Enabled = true;   dipak
            drpterms.Enabled = drpPsle.Enabled = drppmethod.Enabled = true;
            chbDeliNote.Enabled = drpselsmen.Enabled = true;
            //txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = true;     dipak
            /*ddlLocalForeign.Enabled =*/
            /*txtLocationSearch.Enabled Dipak=*/
            ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = true;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = txtcashaccountID.Enabled = txtAmount.Enabled = true;
            ddlSupplier.Enabled = true;

            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                LinkButton lnkbtndelete = (LinkButton)Repeater2.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkbtnItem = (LinkButton)Repeater2.Items[i].FindControl("lnkbtnItem");
                //lnkbtnItem.Enabled = true;
                lnkbtndelete.Enabled = true;
            }
            //for (int i = 0; i < Repeater3.Items.Count; i++)
            //{
            //    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
            //    TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
            //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
            //txtCardReferenceBank.Enabled = txtammunt.Enabled = btnaddamunt.Enabled = drppaymentMeted.Enabled = true;
            //}
            btnSubmit.Visible = btnConfirmOrder.Visible = btnPrint.Visible = hlPeri.Visible = btnSaveData.Visible = /*Custmer.Visible Dipak=*/ BTNsAVEcONFsA.Visible = true;
            btnNew.Visible = false;
            btnnewAdd.Visible = false;
            btnrefesh.Visible = true;
            btnAddItemsIn.Enabled = btndiscartitems.Enabled = true;
        }
        public void LastData()
        {
            int Tarjictio = Convert.ToInt32(DB.ICTR_HD.Where(p => p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.TenentID == TID && p.Status == "RDPOCT").Max(p => p.MYTRANSID));
            int MYTID = Tarjictio;
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).Count() > 0)
            {
                ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                ddlLocalForeign.SelectedValue = OBJICTR_HD.LF == "L" ? "8866" : "8867";
                int SID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
                //txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COMPNAME1;
                //HiddenField3.Value = OBJICTR_HD.CUSTVENDID.ToString();dipak
                ddlSupplier.SelectedValue = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COMPNAME1;

                drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
                ddlCrmAct.SelectedValue = OBJICTR_HD.ACTIVITYCODE.ToString();
                txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
                txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
                if (OBJICTR_HD.COMPANYID == null)
                { }
                else
                    ddlCurrency.SelectedValue = OBJICTR_HD.COMPANYID.ToString();
                ddlProjectNo.SelectedValue = OBJICTR_HD.PROJECTNO.ToString();
                txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
                txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
                txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();

                //var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MYTID && p.TenentID == TID).ToList();
                //if (listcost.Count() > 0)
                //{
                //    ViewState["TempEco_ICCATEGORY"] = listcost;
                //    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                //    Repeater3.DataBind();
                //}
                //else
                //{
                //    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                //    Repeater3.DataBind();
                //}

                decimal Sum = Convert.ToDecimal(0.00);
                txtOHCostHD.Text = Sum.ToString();
                //lblcr.Text = Sum.ToString();

                btnAddItemsIn.Visible = true;
                btnAddDT.Visible = false;
                BindDT(MYTID);
                readMode();
                //panelRed.Visible = false;
            }
        }
        public void BindData()
        {
            ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;

            Classes.EcommAdminClass.getdropdown(ddlLocalForeign, TID, "LF", "OTH", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlCrmAct, TID, "ACTVTY", "Transactions", "", "REFTABLE");
            ddlCrmAct.SelectedValue = "Confirm Transaction";
            Classes.EcommAdminClass.getdropdown(drpterms, TID, "Terms", "Terms", "", "REFTABLE");
            //  Classes.EcommAdminClass.getdropdown(ddlcustmoerlist, TID, "1", "2", "", "TBLCOMPANYSETUP");
            Classes.EcommAdminClass.getdropdown(ddlCurrency, TID, "", "", "", "tblCOUNTRY");
            Classes.EcommAdminClass.getdropdown(ddlProjectNo, TID, "", "", "", "TBLPROJECT");
            ddlProjectNo.SelectedValue = "IC";
            Classes.EcommAdminClass.getdropdown(drpRefnstype, TID, "Reference", "RefSubType", "Purchase", "REFTABLE");
            //Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            //Repeater3.DataBind();
            Classes.EcommAdminClass.getdropdown(drpselsmen, TID, LID.ToString(), "", "", "tbl_Employee");

            Classes.EcommAdminClass.getdropdown(ddlSupplier, TID, "", "1", "", "TBLCOMPANYSETUP");
            CheckRole();
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCounrty(string prefixText, int count)
        {
            try
            {
                int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);

                string conStr;
                conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
                string sqlQuery = "SELECT [COMPNAME1],[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and BUYER = 'true' and COMPNAME1 like @COMPNAME  + '%'";
                SqlConnection conn = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@COMPNAME", prefixText);
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

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlLocalForeign.SelectedItem.Text == "Local")
            {
                ddlCurrency.Enabled = false;

            }
            else
            {
                ddlCurrency.Enabled = true;
                //dt = ViewState["SUPPLIER"] as DataTable;
                //DataRow[] drGetCurr = dt.Select("ContactMyID = '" + ddlSupplier.SelectedValue + "' and TenentID='" + Convert.ToInt32(Session["TenentID"].ToString()) + "'");
                // ddlCurrency.SelectedValue = Convert.ToString(drGetCurr[0]["COUNTRYID"].ToString());
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int COMPID = 826667;
                string Status = "";
                int TransNo = 0;
                
                string POSCounterID = "1";// coming from POSCounter Table where Parameter 3 from Invoice.aspx?CounterID

                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID && p.TenentID == TID).Count() == 1)
                {
                    COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID && p.TenentID == TID).COMPID;
                }
                if (ViewState["HDMYtrctionid"] != null)
                {
                    TransNo = Convert.ToInt32(ViewState["HDMYtrctionid"]);
                    var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    string PERIOD_CODE = OICODID;
                    string MYSYSNAME = "ACT".ToString();//dipak
                    int JOID = objProFile.JOID;
                    int CRUP_ID = Convert.ToInt32(0);
                    string GLPOST = objProFile.GLPOST;
                    string GLPOST1 = objProFile.GLPOST1;
                    string GLPOSTREF = objProFile.GLPOSTREF;
                    string GLPOSTREF1 = objProFile.GLPOSTREF1;
                    string ICPOST = objProFile.ICPOST;
                    string ICPOSTREF = objProFile.ICPOSTREF;
                    bool ACTIVE = Convert.ToBoolean(true);
                    int COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    foreach (Database.ICTR_DT item in List5)
                    {
                        DB.ICTR_DT.DeleteObject(item);
                        DB.SaveChanges();
                    }

                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
                        Label lbltransisbatchno = (Label)Repeater2.Items[i].FindControl("lbltransisbatchno");
                        //Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
                        TextBox txtpaidamounr = (TextBox)Repeater2.Items[i].FindControl("txtpaidamounr");
                        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
                        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
                        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
                        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
                        Label lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT");
                        Label lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER");
                        Label lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT");
                        Label lblMyProdID = (Label)Repeater2.Items[i].FindControl("lblMyProdID");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);
                        decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);
                        //int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        int MyProdID = Convert.ToInt32(lblMyProdID.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = Convert.ToInt32(ddlCrmAct.SelectedValue);
                        string DESCRIPTION = lbltransisbatchno.Text;
                        string UOM = "0";
                        string[] id = (lblDisQnty.Text).Split('.');
                        int QUANTITY = Convert.ToInt32(id[0]);
                        //int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        decimal AMOUNT = Convert.ToDecimal(txtpaidamounr.Text);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.00);
                        string BATCHNO = txtBatchNo.Text;
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(lblDisQnty.Text);
                        decimal TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        decimal PROMOTIONAMT = Convert.ToDecimal(lblTotalCurrency.Text);
                        DateTime EXPIRYDATE = Convert.ToDateTime(lblProductNameItem.Text);
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";
                        Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

                        //QTY = objICTR_DT.QUANTITY;
                        //AMUT = Convert.ToDecimal(objICTR_DT.AMOUNT);

                    }
                    //  int TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    string REFERENCE = txtrefreshno.Text;
                    // int TenentID = TID;
                    // int MYTRANSID = TransNo;
                    int ToTenantID = objProFile.ToTenantID; ;
                    int TOLOCATIONID = LID;
                    //int transid = Convert.ToInt32(Request.QueryString["transid"]);
                    //int transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                    tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == objtbltblActSLSetup.transid && p.transsubid == objtbltblActSLSetup.transsubid);

                    string MainTranType = "P";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    decimal CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue); //dipak
                    string LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    //if (LF == "L")
                    //    transsubid = 441;
                    //else
                    //    transsubid = 442;
                    //   string PERIOD_CODE = Pidalcode(TACtionDate).ToString();
                    string ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;//dipak
                    decimal TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    decimal TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);
                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = ddlProjectNo.SelectedValue;//dipak
                    string CounterID = txtcashaccountID.Text;//dipak
                    string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    string NOTES = txtNoteHD.Text;
                    string USERID = UseID.ToString();
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Convert.ToDateTime(txtDate.Text);
                    DateTime ENTRYTIME = Curentdatetime;
                    DateTime UPDTTIME = Curentdatetime;
                    int InvoiceNO = Convert.ToInt32(txtbankcheck.Text);//dipak
                    decimal Discount = Convert.ToDecimal(0);

                    Status = "FPV";
                    int Terms = Convert.ToInt32(drppmethod.SelectedValue);//dipak
                    string DatainserStatest = "Update";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (drpPsle.SelectedValue == "3")
                    {
                        Custmerid = ddlSupplier.SelectedValue;

                        Swit1 = "3";
                    }
                    else if (drpPsle.SelectedValue == "4")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "4";
                    }
                    else if (drpPsle.SelectedValue == "5")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "5";
                    }
                    string BillNo = drpselsmen.SelectedValue;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenantID, TOLOCATIONID, MainTranType, TranType, objtbltblActSLSetup.transid, objtbltblActSLSetup.transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);

                    var LIstTotal = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    decimal TOALSUM = Convert.ToDecimal(LIstTotal.Sum(p => p.AMOUNT));
                    int TOTQTY = Convert.ToInt32(LIstTotal.Sum(p => p.QUANTITY));
                    var List1 = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();

                    //var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    //foreach (Database.ICTRPayTerms_HD item in List2)
                    //{

                    //    DB.ICTRPayTerms_HD.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}
                    //decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    //decimal Total = 0;
                    //for (int i = 0; i < Repeater3.Items.Count; i++)
                    //{
                    //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    //    if (txtammunt.Text == "")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        decimal TXT = Convert.ToDecimal(txtammunt.Text);
                    //        Total = Total + TXT;
                    //    }
                    //}
                    //if (Payment == Total)
                    //{
                    //    for (int j = 0; j < Repeater3.Items.Count; j++)
                    //    {
                    //        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                    //        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                    //        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                    //        if (txtrefresh.Text == "" && txtammunt.Text == "")
                    //        {

                    //        }
                    //        else
                    //        {


                    //            string RFresh = txtrefresh.Text.ToString();
                    //            string[] id = RFresh.Split(',');
                    //            string IDRefresh = id[0].ToString();
                    //            string IdApprouv = id[1].ToString();
                    //            int PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);

                    //            ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                    //            objICTRPayTerms_HD.TenentID = TID;
                    //            objICTRPayTerms_HD.LocationID = LID;
                    //            objICTRPayTerms_HD.MyTransID = TransNo;
                    //            objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;

                    //            objICTRPayTerms_HD.CashBankChequeID = DB.REFTABLEs.Where(p => p.REFID == PaymentTermsId).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Single(p => p.REFID == PaymentTermsId).SWITCH3) : 1;
                    //            objICTRPayTerms_HD.CounterID = POSCounterID;
                    //            objICTRPayTerms_HD.TransDate =DB.ICTR_HD.Where(p => p.transid == TransNo).Count() > 0 ? DB.ICTR_HD.Single(p => p.transid == TransNo).TRANSDATE:DateTime.Now;
                    //           // objICTRPayTerms_HD.CheckOutDate// come from Cash Delivery Popup
                    //           //  objICTRPayTerms_HD.AccountantID// come from Cash Delivery Popup
                    //            objICTRPayTerms_HD.AccountID = 0;
                    //            objICTRPayTerms_HD.CRUP_ID = 1;
                    //            objICTRPayTerms_HD.Notes = IDRefresh + ", ";
                    //            objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                    //            objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    //            objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    //            DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    //            DB.SaveChanges();
                    //        }

                    //    }
                    //}
                    //else
                    //{
                    //    pnlSuccessMsg123.Visible = true;
                    //    lblMsg123.Text = "Total Amount not Macth";
                    //    return;
                    //}
                    btnSaveData.Text = "Confirm Order";
                    btnConfirmOrder.Text = "Confirm Order";
                    ViewState["HDMYtrctionid"] = null;

                }
                else
                {//Start Insert
                    TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    string REFERENCE = txtrefreshno.Text;
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    int ToTenantID = objProFile.ToTenantID; ;
                    int TOLOCATIONID = LID;
                    //int transid = Convert.ToInt32(Request.QueryString["transid"]);
                    //int transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                    tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == objtbltblActSLSetup.transid && p.transsubid == objtbltblActSLSetup.transsubid);

                    string MainTranType = "P";// Out Qty On Product //Dipak
                    string TransType = objProFile.TranType;
                    string TranType = "PaymentVoucher";// Out Qty On Product
                    int Invoiec = Convert.ToInt32(DB.tbltranssubtypes.Single(p => p.transsubid == objtbltblActSLSetup.transsubid && p.transid == objtbltblActSLSetup.transid && p.TenentID == TID).serialno);
                    decimal COMPID1 = COMPID;
                    string MYSYSNAME = "ACT".ToString();//dipak
                    //decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);dipak
                    decimal CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue); //dipak
                    string LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    string PERIOD_CODE = OICODID;
                    string ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;//dipak
                    //decimal TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    decimal TOTAMT = Convert.ToDecimal(txtAmount.Text);//dipak
                    decimal TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);
                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = ddlProjectNo.SelectedValue;//dipak                   
                    string CounterID = txtcashaccountID.Text;//dipak
                    string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    string NOTES = txtNoteHD.Text;
                    int JOID = objProFile.JOID;
                    int CRUP_ID = Convert.ToInt32(999999);
                    string GLPOST = objProFile.GLPOST;
                    string GLPOST1 = objProFile.GLPOST1;
                    string GLPOSTREF = objProFile.GLPOSTREF;
                    string GLPOSTREF1 = objProFile.GLPOSTREF1;
                    string ICPOST = objProFile.ICPOST;
                    string ICPOSTREF = objProFile.ICPOSTREF;
                    string USERID = UseID.ToString();
                    bool ACTIVE = Convert.ToBoolean(true);
                    int COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Convert.ToDateTime(txtDate.Text);
                    //DateTime ENTRYDATE = Convert.ToDateTime(txtDate.Text);//dipak
                    DateTime ENTRYTIME = Curentdatetime;
                    DateTime UPDTTIME = Curentdatetime;
                    string Custmerid = "";
                    string Swit1 = "";
                    if (drpPsle.SelectedValue == "3")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "3";
                    }
                    else if (drpPsle.SelectedValue == "4")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "4";
                    }
                    else if (drpPsle.SelectedValue == "5")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "5";
                    }

                    int InvoiceNoTaction = DB.ICTR_HD.Where(p => p.transid == objtbltblActSLSetup.transid && p.transsubid == objtbltblActSLSetup.transsubid && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.transid == objtbltblActSLSetup.transid && p.transsubid == objtbltblActSLSetup.transsubid && p.TenentID == TID).Max(p => p.InvoiceNO) + 1) : Invoiec;
                    //int InvoiceNO = InvoiceNoTaction;
                    int InvoiceNO = Convert.ToInt32(txtbankcheck.Text);//dipak
                    decimal Discount = Convert.ToDecimal(0);
                    Status = "DPV";
                    int Terms = Convert.ToInt32(drppmethod.SelectedValue);//dipak
                    string DatainserStatest = "Add";
                    string BillNo = drpselsmen.SelectedValue;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenantID, TOLOCATIONID, MainTranType, TranType, objtbltblActSLSetup.transid, objtbltblActSLSetup.transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
                        Label lbltransisbatchno = (Label)Repeater2.Items[i].FindControl("lbltransisbatchno");
                        //Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
                        TextBox txtpaidamounr = (TextBox)Repeater2.Items[i].FindControl("txtpaidamounr");
                        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
                        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
                        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
                        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
                        Label lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT");
                        Label lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER");
                        Label lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT");
                        Label lblMyProdID = (Label)Repeater2.Items[i].FindControl("lblMyProdID");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);
                        decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);
                        //int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        int MyProdID = Convert.ToInt32(lblMyProdID.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = Convert.ToInt32(ddlCrmAct.SelectedValue);
                        string DESCRIPTION = lbltransisbatchno.Text;
                        string UOM = "0";
                        string[] id = (lblDisQnty.Text).Split('.');
                        int QUANTITY = Convert.ToInt32(id[0]);
                        //int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        decimal AMOUNT = Convert.ToDecimal(txtpaidamounr.Text);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.00);
                        string BATCHNO = txtBatchNo.Text;
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(lblDisQnty.Text);
                        decimal TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        decimal PROMOTIONAMT = Convert.ToDecimal(lblTotalCurrency.Text);
                        DateTime EXPIRYDATE = Convert.ToDateTime(lblProductNameItem.Text);
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";

                        Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);


                    }

                    //decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    //decimal Total = 0;
                    //for (int i = 0; i < Repeater3.Items.Count; i++)
                    //{
                    //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    //    if (txtammunt.Text == "")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        decimal TXT = Convert.ToDecimal(txtammunt.Text);
                    //        Total = Total + TXT;
                    //    }
                    //}
                    //if (Payment == Total)
                    //{
                    //    for (int j = 0; j < Repeater3.Items.Count; j++)
                    //    {
                    //        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                    //        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                    //        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                    //        if (txtrefresh.Text == "" && txtammunt.Text == "")
                    //        {

                    //        }
                    //        else
                    //        {


                    //            string RFresh = txtrefresh.Text.ToString();
                    //            string[] id = RFresh.Split(',');
                    //            string IDRefresh = id[0].ToString();
                    //            string IdApprouv = id[1].ToString();
                    //            ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                    //            objICTRPayTerms_HD.TenentID = TID;
                    //            objICTRPayTerms_HD.MyTransID = TransNo;
                    //            objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                    //            objICTRPayTerms_HD.AccountID = 0;
                    //            objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                    //            objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    //            objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    //            DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    //            DB.SaveChanges();
                    //        }

                    //    }
                    //}
                    //else
                    //{
                    //    pnlSuccessMsg123.Visible = true;
                    //    lblMsg123.Text = "Total Amount not Macth";
                    //    return;
                    //}

                    string SupplierName = ddlSupplier.SelectedValue;
                    string ADDACTIvity = "Add";
                    string DAteTime = TRANSDATE.ToShortDateString();
                    string Discription = TranType + " , " + TransNo + " , " + DAteTime + " , " + SupplierName + " , " + NOTES.ToString();
                    InsertCRMMainActivity(COMPID, TransNo, Status, ADDACTIvity, TranType, Discription);
                    ////    InsertCRMMainActivity(COMPID, TransNo, Status, ADDACTIvity, TranType, NOTES);
                    //    int SID = 1240103;
                    //    string BName = "SO Final";
                    //    string P2 = "aa";
                    //    string P3 = "aa";
                    //    inserCrmproghw(TenentID, SID, BName, P2, P3, TransNo);
                }
                GridData();
                readMode();
                scope.Complete(); //  To commit.
            }





        }
        public int Pidalcode(DateTime Time)
        {
            int PODID = 0;
            if (DB.TBLPERIODS.Where(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.TenentID == TID).Count() > 0)
            {
                PODID = Convert.ToInt32(DB.TBLPERIODS.Single(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.TenentID == TID).PERIOD_CODE);

            }
            return PODID;
        }
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                int COMPID = 826667;
                string Status = "";
                int TransNo = 0;
                
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID && p.TenentID == TID).Count() == 1)
                {
                    COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID && p.TenentID == TID).COMPID;
                }
                if (ViewState["HDMYtrctionid"] != null)
                {
                    TransNo = Convert.ToInt32(ViewState["HDMYtrctionid"]);
                    var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    string PERIOD_CODE = OICODID;
                    string MYSYSNAME = "ACT".ToString();
                    int JOID = objProFile.JOID;
                    int CRUP_ID = Convert.ToInt32(0);
                    string GLPOST = objProFile.GLPOST;
                    string GLPOST1 = objProFile.GLPOST1;
                    string GLPOSTREF = objProFile.GLPOSTREF;
                    string GLPOSTREF1 = objProFile.GLPOSTREF1;
                    string ICPOST = objProFile.ICPOST;
                    string ICPOSTREF = objProFile.ICPOSTREF;
                    bool ACTIVE = Convert.ToBoolean(true);
                    int COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    foreach (Database.ICTR_DT item in List5)
                    {

                        DB.ICTR_DT.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    var List6 = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    foreach (Database.ICTR_DTEXT item in List6)
                    {

                        DB.ICTR_DTEXT.DeleteObject(item);
                        DB.SaveChanges();
                    }

                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
                        Label lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription");
                        Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
                        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
                        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
                        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
                        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
                        Label lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT");
                        Label lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER");
                        Label lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);
                        decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);
                        //int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        int MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = Convert.ToInt32(ddlCrmAct.SelectedValue);
                        string DESCRIPTION = lblDiscription.Text;
                        string UOM = lblUOM.Text;
                        string[] id = (lblDisQnty.Text).Split('.');
                        int QUANTITY = Convert.ToInt32(id[0]);

                        //int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.00);
                        string BATCHNO = "1";
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(lblTAXAMT.Text);
                        decimal TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        decimal PROMOTIONAMT = Convert.ToDecimal(10.00);
                        DateTime EXPIRYDATE = DateTime.Now;
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";
                        Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

                    }
                    string REFERENCE = txtrefreshno.Text;
                    // int TenentID = TID;
                    // int MYTRANSID = TransNo;
                    int ToTenantID = objProFile.ToTenantID; ;
                    int TOLOCATIONID = LID;
                    //int transid = Convert.ToInt32(Request.QueryString["transid"]);
                    //int transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                    tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == objtbltblActSLSetup.transid && p.transsubid == objtbltblActSLSetup.transsubid);

                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    //  string MYSYSNAME = "SAL".ToString();
                    //decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value); dipak
                    decimal CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue);//dipak
                    string LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    //if (LF == "L")
                    //    transsubid = 221;
                    //else
                    //    transsubid = 221;
                    //   string PERIOD_CODE = Pidalcode(TACtionDate).ToString();
                    string ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;
                    decimal TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    decimal TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);
                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = ddlProjectNo.SelectedValue;
                    string CounterID = objProFile.CounterID;
                    string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;

                    DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                    string NOTES = txtNoteHD.Text;
                    //   int JOID = objProFile.JOID;
                    //    int CRUP_ID = Convert.ToInt32(999999);
                    //   string GLPOST = objProFile.GLPOST;
                    //   string GLPOST1 = objProFile.GLPOST1;
                    //    string GLPOSTREF = objProFile.GLPOSTREF;
                    //   string GLPOSTREF1 = objProFile.GLPOSTREF1;
                    //   string ICPOST = objProFile.ICPOST;
                    //   string ICPOSTREF = objProFile.ICPOSTREF;
                    string USERID = UseID.ToString();
                    //   bool ACTIVE = Convert.ToBoolean(true);
                    //  int COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    DateTime UPDTTIME = Curentdatetime;
                    int InvoiceNO = 0;
                    decimal Discount = Convert.ToDecimal(0);
                    if (chbDeliNote.Checked == true)
                    {
                        Status = "RDSO";
                    }
                    else
                    {
                        Status = "SO";
                    }
                    int Terms = 0;
                    string DatainserStatest = "Update";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (drpPsle.SelectedValue == "3")
                    {
                        Custmerid = ddlSupplier.SelectedValue;

                        Swit1 = "3";
                    }
                    else if (drpPsle.SelectedValue == "4")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "4";
                    }
                    else if (drpPsle.SelectedValue == "5")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "5";
                    }
                    string BillNo = drpselsmen.SelectedValue;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenantID, TOLOCATIONID, MainTranType, TranType, objtbltblActSLSetup.transid, objtbltblActSLSetup.transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);


                    var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    foreach (Database.ICTRPayTerms_HD item in List2)
                    {

                        DB.ICTRPayTerms_HD.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    //decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    //decimal Total = 0;
                    //for (int i = 0; i < Repeater3.Items.Count; i++)
                    //{
                    //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    //if (txtammunt.Text != "")
                    //{ 
                    //    decimal TXT = Convert.ToDecimal(txtammunt.Text);
                    //    Total = Total + TXT;
                    //}
                    //}
                    //if (Payment == Total)
                    //{
                    //for (int j = 0; j < Repeater3.Items.Count; j++)
                    //{
                    //    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                    //    TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                    //    TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                    //if (txtCardReferenceBank.Text != "" && txtammunt.Text != "")                           
                    //    {
                    //        string RFresh = txtCardReferenceBank.Text.ToString();
                    //        string[] id = RFresh.Split(',');
                    //        string IDRefresh = id[0].ToString();
                    //        string IdApprouv = id[1].ToString();
                    //        ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                    //        objICTRPayTerms_HD.TenentID = TID;
                    //        objICTRPayTerms_HD.MyTransID = MYTRANSID;
                    //        objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                    //        objICTRPayTerms_HD.AccountID = 0;
                    //        objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                    //        objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    //        objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    //        DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    //        DB.SaveChanges();
                    //    }

                    //}
                    //}
                    //else
                    //{
                    //    pnlSuccessMsg123.Visible = true;
                    //    lblMsg123.Text = "Total Amount not Macth";
                    //    return;
                    //}
                    readMode();
                    GridData();
                    btnSaveData.Text = "Confirm Order";
                    btnConfirmOrder.Text = "Confirm Order";
                    ViewState["HDMYtrctionid"] = null;
                }
                else
                {
                    // insert deta is ICTR_HD
                    TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    int ToTenantID = objProFile.ToTenantID; ;
                    int TOLOCATIONID = LID;
                    //int transid = Convert.ToInt32(Request.QueryString["transid"]);
                    //int transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                    tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == objtbltblActSLSetup.transid && p.transsubid == objtbltblActSLSetup.transsubid);

                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    string MYSYSNAME = "SAL".ToString();
                    //decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);     dipak
                    decimal CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue);    //dipak
                    string LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    //if (LF == "L")
                    //    transsubid = 441;
                    //else
                    //    transsubid = 442;
                    string PERIOD_CODE = OICODID;
                    string ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;
                    decimal TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    decimal TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);
                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = ddlProjectNo.SelectedValue;
                    string CounterID = objProFile.CounterID;
                    string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;

                    DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    string REFERENCE = txtrefreshno.Text;
                    string NOTES = txtNoteHD.Text;
                    int JOID = objProFile.JOID;
                    int CRUP_ID = Convert.ToInt32(999999);
                    string GLPOST = objProFile.GLPOST;
                    string GLPOST1 = objProFile.GLPOST1;
                    string GLPOSTREF = objProFile.GLPOSTREF;
                    string GLPOSTREF1 = objProFile.GLPOSTREF1;
                    string ICPOST = objProFile.ICPOST;
                    string ICPOSTREF = objProFile.ICPOSTREF;
                    string USERID = UseID.ToString();
                    bool ACTIVE = Convert.ToBoolean(true);
                    int COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    int Terms = Convert.ToInt32(drpterms.SelectedValue);
                    DateTime UPDTTIME = Curentdatetime;
                    int InvoiceNO = 0;
                    decimal Discount = Convert.ToDecimal(0);

                    if (chbDeliNote.Checked == true)
                    {
                        Status = "RDSO";
                    }
                    else
                    {
                        Status = "SO";
                    }

                    string DatainserStatest = "Add";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (drpPsle.SelectedValue == "3")
                    {
                        Custmerid = ddlSupplier.SelectedValue;

                        Swit1 = "3";
                    }
                    else if (drpPsle.SelectedValue == "4")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "4";
                    }
                    else if (drpPsle.SelectedValue == "5")
                    {
                        Custmerid = ddlSupplier.SelectedValue;
                        Swit1 = "5";
                    }
                    string BillNo = drpselsmen.SelectedValue;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenantID, TOLOCATIONID, MainTranType, TranType, objtbltblActSLSetup.transid, objtbltblActSLSetup.transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    // insert deta is ICTR_DT
                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
                        Label lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription");
                        Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
                        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
                        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
                        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
                        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
                        Label lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT");
                        Label lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER");
                        Label lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);
                        decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);
                        //int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        int MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = Convert.ToInt32(ddlCrmAct.SelectedValue);
                        string DESCRIPTION = lblDiscription.Text;
                        string UOM = lblUOM.Text;
                        int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.00);
                        string BATCHNO = "1";
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(lblTAXAMT.Text);
                        decimal TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        decimal PROMOTIONAMT = Convert.ToDecimal(10.00);
                        DateTime EXPIRYDATE = DateTime.Now;
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";
                        Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

                    }


                    //decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    //decimal Total = 0;
                    //for (int i = 0; i < Repeater3.Items.Count; i++)
                    //{
                    //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    //    if (txtammunt.Text == "")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        decimal TXT = Convert.ToDecimal(txtammunt.Text);
                    //        Total = Total + TXT;
                    //    }
                    //}
                    //if (Payment == Total)
                    //{
                    //    for (int j = 0; j < Repeater3.Items.Count; j++)
                    //    {
                    //        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                    //        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                    //        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                    //        if (txtrefresh.Text == "" && txtammunt.Text == "")
                    //        {

                    //        }
                    //        else
                    //        {


                    //            string RFresh = txtrefresh.Text.ToString();
                    //            string[] id = RFresh.Split(',');
                    //            string IDRefresh = id[0].ToString();
                    //            string IdApprouv = id[1].ToString();
                    //            ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                    //            objICTRPayTerms_HD.TenentID = TID;
                    //            objICTRPayTerms_HD.MyTransID = MYTRANSID;
                    //            objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                    //            objICTRPayTerms_HD.AccountID = 0;
                    //            objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                    //            objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    //            objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    //            DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    //            DB.SaveChanges();
                    //        }

                    //    }
                    //}
                    //else
                    //{
                    //    pnlSuccessMsg123.Visible = true;
                    //    lblMsg123.Text = "Total Amount not Macth";
                    //    return;
                    //}

                    GridData();
                    readMode();
                    int MsterCose = 0;
                    string REFNO = "";
                    //if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.COMPID == COMPID && p.MyID == TransNo).Count() > 0)
                    //{
                    //    CRMMainActivity objCRMMainActivity = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.COMPID == COMPID && p.MyID == TransNo);
                    //    MsterCose = objCRMMainActivity.MasterCODE;
                    //    REFNO = objCRMMainActivity.Reference;
                    //    Classes.CRMClass.InsertActivity(TID, COMPID, MsterCose, MsterCose, REFNO, UseID, REFNO, 3, "Payment", UID);
                    //}
                    //else
                    //{
                    //    string SupplierName = ddlSupplier.SelectedValue;
                    //    string ADDACTIvity = "Add";
                    //    string DAteTime = TRANSDATE.ToShortDateString();
                    //    string Discription = TranType + " , " + TransNo + " , " + DAteTime + " , " + SupplierName + " , " + NOTES.ToString();
                    //    InsertCRMMainActivity(COMPID, TransNo, Status, ADDACTIvity, TranType, Discription);
                    //}
                }





                scope.Complete(); //  To commit.
                //Response.Redirect("PrintMDSF.aspx?Tranjestion=" + TransNo);
                //Response.Redirect("LocalInvoice.aspx?Tranjestion=" + TransNo);                
                //Response.Redirect("CorpInvoice.aspx?Tranjestion=" + TransNo + "&salesmen=" + salemen);
                Response.Redirect("LocalInvoice.aspx?Tranjestion=" + TransNo);

            }

        }
        public void InsertCRMMainActivity(int COMPID1, int TransNo, string Status, string ADDACTIvity, string CamName, string Description)
        {
            int TenentID = TID;
            int LocationID = LID;
            int USERCODE = UID;
            int COMPID = COMPID1;
            int ID = TransNo;
            int RouteID = 1;
            string ACTIVITYE = "Transaction";
            DateTime REPEATTILL = DateTime.Now;
            DateTime UPDTTIME = DateTime.Now;
            string USERNAME = UNAME;
            string Version1 = UNAME;
            string MyStatus = Status;
            string RecodType = ADDACTIvity;
            string URL = Request.Url.AbsolutePath;
            string CampynName = CamName + "  " + TransNo;
            string CampynDescription = Description;
            Classes.CRMClass.InserActivityMain(TenentID, COMPID, LocationID, ID, RouteID, UID, ACTIVITYE, UNAME, MDUID, 4, "Sales Invoice Final", CampynName, CampynDescription);

        }
        public void InserCrmAcivity(int TID, int COMPID1, int TransNo, string Status)
        {

            CRMMainActivity obj = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.COMPID == COMPID1 && p.MyID == TransNo);
            obj.MyStatus = Status;
            DB.SaveChanges();
            int MsterCose = obj.MasterCODE;
            int TenentID = TID;
            int COMPID = COMPID1;
            int MasterCODE = MsterCose;
            int LinkMasterCODE = 1;
            int MenuID = 0;
            int ActivityID = 0;
            string ACTIVITYTYPE = "Inventory";
            string REFTYPE = "Transaction";
            string REFSUBTYPE = "REFERENCE";
            string PerfReferenceNo = "A";
            string EarlierRefNo = "A";
            string NextUser = UNAME;
            string NextRefNo = "A";
            string AMIGLOBAL = "Y";
            string MYPERSONNEL = "Y";
            string ActivityPerform = Status;
            string REMINDERNOTE = "A";
            int ESTCOST = 0;
            string GROUPCODE = "A";
            string USERCODEENTERED = "A";
            DateTime UPDTTIME = DateTime.Now;
            DateTime UploadDate = DateTime.Now;
            string USERNAME = UNAME;
            int CRUP_ID = 0;
            DateTime InitialDate = DateTime.Now;
            DateTime DeadLineDate = DateTime.Now;
            string RouteID = "A";
            string Version1 = UNAME;
            int Type = 0;
            string MyStatus = Status;
            string GroupBy = "A";
            int DocID = 0;
            int ToColumn = 0;
            int FromColumn = 0;
            string Active = "A";
            int MainSubRefNo = 0;
            string URL = Request.Url.AbsolutePath;
            Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL);

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
        protected void btnExit_Click(object sender, EventArgs e)
        {
            //hidUPriceLocal.Value = hidTotalCurrencyForeign.Value = hidTotalCurrencyLocal.Value = "";      dipak
            Response.Redirect(Request.RawUrl, false);
        }
        protected void btnSendMail_Click1(object sender, EventArgs e)
        {

            MailMessage mailmsg = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mailmsg.From = new MailAddress("newblogtrick@gmail.com");
            mailmsg.Subject = "Purchase Quotation Details";
            if (txtRecipient.Text.Trim().Length > 0)
            {
                foreach (string addr in txtRecipient.Text.Trim().Split(','))
                {
                    mailmsg.To.Add(new MailAddress(addr));
                }
            }
            if (txtRecCC.Text.Trim().Length > 0)
            {
                foreach (string addr in txtRecCC.Text.Trim().Split(';'))
                {
                    mailmsg.CC.Add(new MailAddress(addr));
                }
            }
            mailmsg.Attachments.Add(new Attachment("D:/Purchase.pdf"));
            mailmsg.Body = "Please Find Attachment at below side";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("newblogtrick@gmail.com", "90380107059");
            smtp.EnableSsl = true;
            smtp.Send(mailmsg);
        }
        List<Database.ICTR_DT> TempListICTR_DT = new List<Database.ICTR_DT>();

        protected void grdPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ViewState["TempList"] = null;
                int MYTID = Convert.ToInt32(e.CommandArgument);
                EditSalesh(MYTID);


            }
            if (e.CommandName == "Btnview")
            {
                ViewState["TempList"] = null;
                int MYTID = Convert.ToInt32(e.CommandArgument);
                EditSalesh(MYTID);
                readMode();
            }
            if (e.CommandName == "Delete")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                OBJICTR_HD.ACTIVE = false;
                DB.SaveChanges();
                var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).ToList();
                for (int i = 0; i < List.Count - 1; i++)
                {
                    List[i].ACTIVE = false;
                    DB.SaveChanges();
                }
                var list1 = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).ToList();
                for (int i = 0; i < list1.Count - 1; i++)
                {
                    list1[i].ACTIVE = false;
                    DB.SaveChanges();
                }
                BindDT(MYTID);
                GridData();
            }
            if (e.CommandName == "ReceiveProduct")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("GoofsTransferNotes.aspx?Tranjestion=" + MYTID);
            }
            if (e.CommandName == "btnprient")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                //Response.Redirect("InvoicePrint.aspx?Tranjestion=" + MYTID);
                //Response.Redirect("LocalInvoice.aspx?Tranjestion=" + MYTID);                                              
                //Response.Redirect("CorpInvoice.aspx?Tranjestion=" + MYTID +"&salesmen="+salemen);
                Response.Redirect("LocalInvoice.aspx?Tranjestion=" + MYTID);
            }
        }
        public void EditSalesh(int ID)
        {
            ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID);
            //ddlLocalForeign.SelectedValue = OBJICTR_HD.LF == "L" ? "8866" : "8867"; dipak
            drpselsmen.SelectedValue = OBJICTR_HD.ExtraField2;
            int CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
            TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
            ddlSupplier.SelectedValue = onj.COMPNAME1; //dipak
            //txtLocationSearch.Text = onj.COMPNAME1; dipak
            //HiddenField3.Value = CID.ToString();  dipak
            ddlCrmAct.SelectedValue = OBJICTR_HD.ACTIVITYCODE.ToString();

            txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
            txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
            if (OBJICTR_HD.COMPANYID == null)
            { }
            else
                ddlCurrency.SelectedValue = OBJICTR_HD.COMPANYID.ToString();
            ddlProjectNo.SelectedValue = OBJICTR_HD.PROJECTNO.ToString();
            txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
            txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
            txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
            drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();

            //var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == ID && p.TenentID == TID).ToList();
            //if (listcost.Count() > 0)
            //{
            //    ViewState["TempEco_ICCATEGORY"] = listcost;
            //    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
            //    Repeater3.DataBind();
            //}
            //else
            //{
            //    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
            //    Repeater3.DataBind();
            //}
            decimal Sum = Convert.ToDecimal(0.00);
            txtOHCostHD.Text = Sum.ToString();
            //lblcr.Text = Sum.ToString();

            btnAddItemsIn.Visible = true;
            btnAddDT.Visible = false;
            BindDT(ID);

            ListItems.Visible = true;
            //panelRed.Visible = false;
            ViewState["HDMYtrctionid"] = ID;
            btnSaveData.Text = "Update Order";
            btnConfirmOrder.Text = "Update Order";
            btnSaveData.Visible = true;
            btnConfirmOrder.Visible = true;
            string Statesh = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID).Status;
            if (Statesh == "SO")
            {
                readMode();
            }
            else
            {

                WrietMode();
            }
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDT")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);
                ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                objICTR_DT.ACTIVE = false;
                DB.SaveChanges();
                BindDT(str1);
            }
            if (e.CommandName == "editDT")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);

                if (ViewState["TempListICTR_DT"] != null)
                {
                    List<Database.ICTR_DT> TempListICTR_DT123 = ((List<Database.ICTR_DT>)ViewState["TempListICTR_DT"]).ToList();
                    ICTR_DT objICTR_DT = TempListICTR_DT123.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                    //ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2);
                    //ddlProduct.SelectedValue = objICTR_DT.MyProdID.ToString();
                    //txtDescription.Text = objICTR_DT.DESCRIPTION.ToString();
                    //ddlUOM.SelectedValue = objICTR_DT.UOM.ToString();
                    //txtQuantity.Text = objICTR_DT.QUANTITY.ToString();
                    //txtUPriceForeign.Text = objICTR_DT.UNITPRICE.ToString();
                    //txtUPriceLocal.Text = objICTR_DT.UNITPRICE.ToString();
                    //txtDiscount.Text = objICTR_DT.DISAMT.ToString();
                    //txtTax.Text = objICTR_DT.TAXPER.ToString();
                    //txtTotalCurrencyForeign.Text = objICTR_DT.AMOUNT.ToString();
                    //txtTotalCurrencyLocal.Text = objICTR_DT.AMOUNT.ToString();
                    ViewState["DTTRCTIONID"] = str1;
                    ViewState["DTMYID"] = str2;
                    //int PID = Convert.ToInt32(ddlProduct.SelectedValue);

                    //TBLPRODUCT objTBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                    //txtserchProduct.Text = objTBLPRODUCT.BarCode;
                    //Boolean Perishable = Convert.ToBoolean(objTBLPRODUCT.Perishable);
                    //Boolean MultiUOM = Convert.ToBoolean(objTBLPRODUCT.MultiUOM);
                    //Boolean MultiColor = Convert.ToBoolean(objTBLPRODUCT.MultiColor);
                    //Boolean MultiSize = Convert.ToBoolean(objTBLPRODUCT.MultiSize);
                    //Boolean MultiBinStore = Convert.ToBoolean(objTBLPRODUCT.MultiBinStore);
                    //Boolean Serialized = Convert.ToBoolean(objTBLPRODUCT.Serialized);
                    //if (MultiUOM == Convert.ToBoolean(1))
                    //{
                    //    lblmultiuom.Visible = true;
                    //}
                    //else
                    //    lblmultiuom.Visible = false;
                    //if (Perishable == Convert.ToBoolean(1))
                    //{
                    //    lblmultiperishable.Visible = true;

                    //}
                    //else
                    //    lblmultiperishable.Visible = false;
                    //if (MultiColor == Convert.ToBoolean(1))
                    //{
                    //    lblmulticolor.Visible = true;

                    //}
                    //else
                    //    lblmulticolor.Visible = false;
                    //if (MultiSize == Convert.ToBoolean(1))
                    //{
                    //    lblmultisize.Visible = true;
                    //}
                    //else
                    //    lblmultisize.Visible = false;
                    //if (MultiBinStore == Convert.ToBoolean(1))
                    //{
                    //    lblmultibinstore.Visible = true;
                    //    // ViewState["MultiBinStore"] = "MultiBinStore";

                    //}
                    //else
                    //    lblmultibinstore.Visible = false;
                    //if (Serialized == Convert.ToBoolean(1))
                    //{
                    //    lblmultiserialize.Visible = true;
                    //}
                    //else
                    //    lblmultiserialize.Visible = false;
                }
                ViewState["AddnewItems"] = "Yes";
                btndiscartitems.Text = "Cansel";
                ListItems.Visible = false;
                //panelRed.Visible = true;
                //string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
                //ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
                btnAddItemsIn.Visible = false;
                //btnAddDT.Visible = true;
                btnAddDT.Text = "Update";
            }
        }
        public void clearItemsTab()
        {
            try
            {
                //ddlProduct.SelectedValue = ddlUOM.SelectedValue = "0";
                //txtDiscount.Text = "0";
                //txtQuantity.Text = "1";
                //txtDescription.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
                //hidUPriceLocal.Value = hidTotalCurrencyForeign.Value = hidTotalCurrencyLocal.Value = hidUOMId.Value = hidUOMText.Value = "";

            }
            catch (System.Exception ex)
            {
                //ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        public void clearHDPanel()
        {
            try
            {
                //ddlLocalForeign.SelectedValue = 
                ddlSupplier.SelectedValue = ddlProjectNo.SelectedValue = ddlCurrency.SelectedValue = ddlCrmAct.SelectedValue = "0";
                /*txtLocationSearch.Text =*/
                txtOrderDate.Text = txtBatchNo.Text = txtTraNoHD.Text = txtNoteHD.Text = txtrefreshno.Text = txtOHCostHD.Text = txttotxl.Text = txtuintOhCost.Text = "";
                btnSubmit.Visible = btnConfirmOrder.Visible = true;
            }
            catch (System.Exception ex)
            {
                // ERPNew.WebMsgBox.Show(ex.Message);
            }
        }

        protected void ddlSupplier_SelectedIndexChanged1(object sender, EventArgs e)
        {
            decimal supllierID = Convert.ToDecimal(ddlSupplier.SelectedValue);

            //int Tarjictio = Convert.ToInt32(DB.ICTR_HD.Single(p => p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.TenentID == TID && p.Status == "RDPOCT" && p.CUSTVENDID == supllierID).MYTRANSID);
            //int MYTID = Tarjictio;
            //yogesh
            List<ICTR_DT> DTLIST = new List<ICTR_DT>();
            foreach (ICTR_HD item in DB.ICTR_HD.Where(p => p.ACTIVE == true && p.transid == 2101 && p.transsubid == 21011 && p.TenentID == TID && p.Status == "RDPOCT" && p.CUSTVENDID == supllierID && p.TranType == "Goods Received Note - Purchase"))
            {
                ICTR_DT ObjDT = new ICTR_DT();

                ObjDT = DB.ICTR_DT.Single(p => p.MYTRANSID == item.MYTRANSID && p.ACTIVE == true && p.TenentID == TID);
                DTLIST.Add(ObjDT);
            }
            Repeater2.DataSource = DTLIST;
            Repeater2.DataBind();

            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                TextBox txtpaidamounr = (TextBox)Repeater2.Items[i].FindControl("txtpaidamounr");//paid amount texbox in listview
                Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");//amount
                Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");//balance

                decimal Amount = Convert.ToDecimal(DTLIST.Sum(p => p.AMOUNT));
                lblUNPtotl.Text = Amount.ToString();
                //decimal balance = Convert.ToDecimal(lblDisQnty.Text);

                decimal balance = Convert.ToDecimal(lblDisQnty.Text);
                lblTextotl.Text = balance.ToString();

            }
            Refill();

            
            //int userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            //int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);

            //int SID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID); //dipak           
            ////int SID = Convert.ToInt32(HiddenField3.Value); //dipak
            //int SIDUserDItl = Convert.ToInt32(DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COUNTRYID);

            //if (CUNTRYID == SIDUserDItl)
            //{
            //    ddlLocalForeign.SelectedValue = "8866";
            //    ddlCurrency.Enabled = false;
            //    //txtUPriceForeign.Enabled = false;     dipak
            //    //txtUPriceLocal.Enabled = true;
            //    ddlCurrency.SelectedValue = CUNTRYID.ToString();
            //    for (int i = 0; i < Repeater1.Items.Count; i++)
            //    {
            //        DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
            //        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
            //        TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
            //        TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
            //        LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
            //        ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = false;
            //    }
            //    ButtonAdd.Enabled = buttonUpdate.Enabled = btndiscorohcost.Enabled = false;
            //}
            //else
            //{
            //    ddlLocalForeign.SelectedValue = "8867";
            //    ddlCurrency.Enabled = true;
            //    //txtUPriceForeign.Enabled = true;      dipak
            //    //txtUPriceLocal.Enabled = false;
            //    ddlCurrency.SelectedValue = SIDUserDItl.ToString();
            //}

        }

        public string getprodname(int SID)
        {
            string ProductCode = DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).UserProdID;
            return ProductCode;
            //string transaction = DB.ICTR_HD.Single(p => p.MYTRANSID == SID && p.TenentID == TID).MYTRANSID.ToString();
            //return transaction;
        }

        public string getsuppername(int ID)
        {
            return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID).COMPNAME1;
        }


        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            //ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;  dipak
            //txtQuantity.Text = txtUPriceForeign.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
            btnAddItemsIn.Visible = false;
            //btnAddDT.Visible = true;
            ListItems.Visible = false;
            //panelRed.Visible = true;
            ViewState["AddnewItems"] = "Yes";
            btndiscartitems.Text = "Cancel";
            btnAddDT.Text = "Save";

            string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";

        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            bool Fleg = true;
            txtDate.Text = DateTime.Now.Date.ToShortDateString();            
            txtTraNoHD.Text = "0";
            DateTime Todate = DateTime.Now;
            Fleg = Classes.EcommAdminClass.BindStockTaking(Todate, TID, panelMsg, lblErreorMsg, Fleg);
            if (Fleg == true)
            {
                WrietMode();
                clearItemsTab();
                clearHDPanel();
                Refill();
                int MAXID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                txtTraNoHD.Text = MAXID.ToString();
                ViewState["TempListICTR_DT"] = null;
                txtOrderDate.Text = DateTime.Now.ToString("dd-MMM-yy");
                //Repeater2.DataSource = null;
                //Repeater2.DataBind();
                btnAddItemsIn.Visible = false;
                btnAddDT.Visible = true;
                //ListItems.Visible = false;
                //panelRed.Visible = true;
                CheckRole();
            }

            ddlCurrency.SelectedValue = "126";
            drpterms.SelectedValue = "2114";
            //txtLocationSearch.Text = "Cash";

        }

        protected void txtOHAmntLocal_TextChanged(object sender, EventArgs e)
        {

        }
        //protected void buttonUpdate_Click(object sender, EventArgs e)
        //{
        //    decimal VAlue = 0;
        //    decimal Total = 0;
        //    bool Fl = true;
        //    for (int i = 0; i < Repeater1.Items.Count; i++)
        //    {
        //        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
        //        VAlue = Convert.ToDecimal(txtOHAmntLocal.Text);
        //        if (Fl == true)
        //        {
        //            Total = VAlue;
        //            Fl = false;
        //        }
        //        else
        //        {
        //            Total = Total + VAlue;
        //        }


        //    }
        //    lblcr.Text = Total.ToString();
        //    txtOHCostHD.Text = Total.ToString();
        //}
        protected void btnExit1_Click(object sender, EventArgs e)
        {
            if (ViewState["AddnewItems"] != null)
            {
                ListItems.Visible = true;
                //panelRed.Visible = false;
                ViewState["AddnewItems"] = null;
                btndiscartitems.Text = "Exit";
                btnAddDT.Visible = false;
                btnAddItemsIn.Visible = true;
            }
            else
            {
                //ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;      dipak
                //txtQuantity.Text = txtUPriceForeign.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
            }
        }
        protected void btnaddrefresh_Click(object sender, EventArgs e)
        {
            int MYID = 0;
            ICIT_BR_ReferenceNo objICIT_BR_ReferenceNo = new ICIT_BR_ReferenceNo();
            int TCNID = Convert.ToInt32(txtTraNoHD.Text);
            objICIT_BR_ReferenceNo.TenentID = TID;
            objICIT_BR_ReferenceNo.RefID = Convert.ToInt32(drpRefnstype.SelectedValue);
            //if (DB.ICIT_BR_ReferenceNo.Where(p => p.MYTRANSID == TCNID).Count() == 0)
            //{
            //    MYID = DB.ICIT_BR_ReferenceNo.Count() > 0 ? Convert.ToInt32(DB.ICIT_BR_ReferenceNo.Max(p => p.ID) + 1) : 1;
            //}
            //else
            //{
            //    var list = DB.ICIT_BR_ReferenceNo.Where(p => p.MYTRANSID == TCNID).ToList();
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        MYID = list[i].ID;
            //    }

            //}
            //objICIT_BR_ReferenceNo.ID = MYID;
            objICIT_BR_ReferenceNo.RefType = "USERTYPE";
            objICIT_BR_ReferenceNo.MYTRANSID = TCNID;
            objICIT_BR_ReferenceNo.MySeq = DB.ICIT_BR_ReferenceNo.Where(p => p.MYTRANSID == TCNID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICIT_BR_ReferenceNo.Where(p => p.MYTRANSID == TCNID && p.TenentID == TID).Max(p => p.MySeq) + 1) : 1;
            objICIT_BR_ReferenceNo.Active = true;
            objICIT_BR_ReferenceNo.Deleted = true;
            objICIT_BR_ReferenceNo.DateTime = DateTime.Now;
            objICIT_BR_ReferenceNo.ReferenceNo = txtrefresh.Text;
            DB.ICIT_BR_ReferenceNo.AddObject(objICIT_BR_ReferenceNo);
            DB.SaveChanges();
            string SizeName = txtrefresh.Text;
            if (ViewState["StrMultiSize"] != null)
            {
                ViewState["StrMultiSize"] += "," + SizeName;
                txtrefreshno.Text = ViewState["StrMultiSize"].ToString();

            }
            else
            {
                txtrefreshno.Text = SizeName.ToString();
                ViewState["StrMultiSize"] = SizeName.ToString();
            }

        }

        protected void btnaddamunt_Click(object sender, EventArgs e)
        {
            ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            TempICTRPayTerms_HD = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            ICTRPayTerms_HD objEco_ICEXTRACOST = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICEXTRACOST);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            //Repeater3.DataSource = TempICTRPayTerms_HD;
            //Repeater3.DataBind();
        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");

                DropDownList drppaymentMeted = (DropDownList)e.Item.FindControl("drppaymentMeted");
                Classes.EcommAdminClass.getdropdown(drppaymentMeted, TID, "Payment", "Method", "Inventeri", "REFTABLE");
                //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "REFTABLE");     dipak
                //drppaymentMeted.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method");
                //drppaymentMeted.DataTextField = "REFNAME1";
                //drppaymentMeted.DataValueField = "REFID";
                //drppaymentMeted.DataBind();
                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    drppaymentMeted.SelectedValue = ID.ToString();
                }


            }
        }

        protected void btnserchcutmer_Click(object sender, EventArgs e)
        {
            //Classes.EcommAdminClass.BindSerchcutomer(txtserchCustmer, HiddenField3);
        }
        protected void drpPsle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drpPsle.SelectedValue == "3")
            //{

            //}           
        }


        protected void btnlistserch_Click(object sender, EventArgs e)
        {
            List<Database.viewTransaction> List = new List<Database.viewTransaction>();


            int Status1 = 0;

            int id1 = Convert.ToInt32(ddlcustmoerlist.SelectedValue);
            string Note = txtremarcklist.Text;
            string refnum = txtreferencrlist.Text;
            if (id1 != null && id1 != 0)
            {
                if (Status1 == 0)
                {
                    List = DB.viewTransactions.Where(p => p.CUSTVENDID == id1 && p.TenentID == TID).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.CUSTVENDID == id1 && p.TenentID == TID).ToList();
                    Status1 = 1;

                }
            }
            else
            {

            }
            if (txtsleshdate.Text != null && txtsleshdate.Text != "")
            {
                DateTime Date = Convert.ToDateTime(txtsleshdate.Text);
                if (Status1 == 0)
                {
                    List = DB.viewTransactions.Where(p => p.TRANSDATE == Date && p.TenentID == TID).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.TRANSDATE == Date && p.TenentID == TID).ToList();
                    Status1 = 1;

                }
            }
            else
            {

            }
            if (Note != "" && Note != null)
            {
                if (Status1 == 0)
                {
                    List = DB.viewTransactions.Where(p => p.NOTES.ToUpper().Contains(Note.ToUpper()) && p.TenentID == TID).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.NOTES.ToUpper().Contains(Note.ToUpper()) && p.TenentID == TID).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (refnum != "" && refnum != null)
            {
                if (Status1 == 0)
                {
                    List = DB.viewTransactions.Where(p => p.REFERENCE.ToUpper().Contains(refnum.ToUpper()) && p.TenentID == TID).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.REFERENCE.ToUpper().Contains(refnum.ToUpper()) && p.TenentID == TID).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            ViewState["SaveList"] = List;
            grdPO.DataSource = List.Where(p => p.TenentID == TID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO")).OrderByDescending(p => p.TRANSDATE);
            grdPO.DataBind();
            //grdPO.DataSource=DB.viewTransactions.Where()
        }

        protected void btnallserch_Click(object sender, EventArgs e)
        {
            string serch = txtallserchforview.Text;
            if (cbkseriz.Checked == true)
            {
                var List = DB.viewTransactions.Where(p => (p.DESCRIPTION.ToUpper().Contains(serch.ToUpper()) && p.TenentID == TID && p.locationID == LID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO"))).OrderBy(p => p.MYPRODID).ToList();
                grdPO.DataSource = List.OrderByDescending(p => p.TRANSDATE);
                grdPO.DataBind();
            }
            else
            {
                var List = DB.viewTransactions.Where(p => (p.UserProdID.ToUpper().Contains(serch.ToUpper()) || p.BarCode.ToUpper().Contains(serch.ToUpper()) || p.AlternateCode1.ToUpper().Contains(serch.ToUpper()) || p.AlternateCode2.ToUpper().Contains(serch.ToUpper()) || p.ShortName.ToUpper().Contains(serch.ToUpper()) || p.ProdName2.ToUpper().Contains(serch.ToUpper()) || p.ProdName3.ToUpper().Contains(serch.ToUpper()) || p.keywords.ToUpper().Contains(serch.ToUpper()) || p.REMARKS.ToUpper().Contains(serch.ToUpper()) || p.DescToprint.ToUpper().Contains(serch.ToUpper()) || p.ProdName1.ToUpper().Contains(serch.ToUpper())) && p.TenentID == TID).OrderBy(p => p.MYPRODID).ToList();
                grdPO.DataSource = List.Where(p => p.TenentID == TID && p.locationID == LID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO")).OrderByDescending(p => p.TRANSDATE);
                grdPO.DataBind();
            }

        }
        //work for ICTR_HD Listview function
        public string Entrydate(int Mytransid)
        {
            DateTime entrydate = Convert.ToDateTime(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == Mytransid).ENTRYDATE);
            return entrydate.ToString();
        }
        public decimal Amountpaids(int Mytransid)
        {
            decimal Amount = Convert.ToDecimal(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == Mytransid).TOTAMT);
            decimal AMTPaid = Convert.ToDecimal(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == Mytransid).AmtPaid);
            decimal Balance = (Amount - AMTPaid);
            return Balance;
            //string amopaid = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == Mytransid).AmtPaid.ToString();
            //return amopaid;
        }
        public decimal TopAmount()
        {
            return Convert.ToDecimal(txtAmount.Text);
        }
        public string gettransidandBatch(int Mytransid)
        {
            int trans = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == Mytransid).transid);
            string Batch = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == Mytransid).USERBATCHNO;
            return trans + "-" + Batch;
        }

        protected void drppmethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drppmethod.Text == "1")
            {
                txtbankcheck.Enabled = false;
                txtDate.Enabled = false;
                txtcashaccountID.Enabled = true;

            }
            if (drppmethod.Text == "2")
            {
                txtcashaccountID.Enabled = false;
                txtDate.Enabled = true;
                txtbankcheck.Enabled = true;
            }
            if (BTNsAVEcONFsA.Visible == true)
                btnnewAdd.Visible = false;


            //txtcashaccountID.text

        }

        protected void txtpaidamounr_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                TextBox txtpaidamounr = (TextBox)Repeater2.Items[i].FindControl("txtpaidamounr");//paid amount texbox in listview
                Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");//amount
                Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");//balance

                if (txtpaidamounr != null)
                {
                    decimal Amount = Convert.ToDecimal(lblTotalCurrency.Text);
                    decimal AMTPaid = Convert.ToDecimal(txtpaidamounr.Text);
                    decimal balance = (Amount - AMTPaid);
                }
            }
        }



        public void Refill()
        {
            decimal FinalTotalPayble = 0, PAmt = 0, TopRemailAmt = Convert.ToDecimal(txtAmount.Text);
            bool EquleFlag = true;

            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                TextBox txtpaidamounr = (TextBox)Repeater2.Items[i].FindControl("txtpaidamounr");//paid amount texbox in listview
                //Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");//amount
                Label lblMYTRANSID = (Label)Repeater2.Items[i].FindControl("lblMYTRANSID");//balance
                PAmt = Amountpaids(Convert.ToInt32(lblMYTRANSID.Text));

                if (EquleFlag)
                {
                    if (PAmt <= TopRemailAmt)
                    {
                        txtpaidamounr.Text = PAmt.ToString("N3");
                        TopRemailAmt -= PAmt;
                        FinalTotalPayble += PAmt;
                    }
                    else
                    {
                        txtpaidamounr.Text = TopRemailAmt.ToString("N3");
                        //TopRemailAmt -= TopRemailAmt;
                        FinalTotalPayble += TopRemailAmt;
                        if (FinalTotalPayble == TopRemailAmt)
                            EquleFlag = false;
                    }
                }
                else
                {
                    txtpaidamounr.Text = "0.000";
                }
                lblTotatotl.Text = txtAmount.Text = FinalTotalPayble.ToString("N3");//txtammunt.Text =

            }
        }

        protected void chReFill_CheckedChanged1(object sender, EventArgs e)
        {
            Refill();
        }
    }
}