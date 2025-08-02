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
    public partial class Quotation : System.Web.UI.Page
    {
        static string Crypath = "";
        CallEntities DB = new CallEntities();
        //  Database.CallEntities DB1 = new Database.CallEntities();
        List<Database.ICOVERHEADCOST> TempList = new List<Database.ICOVERHEADCOST>();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        List<ICIT_BR_BIN> ListICIT_BR_BIN = new List<ICIT_BR_BIN>();
        List<ICIT_BR_Perishable> ListICIT_BR_Perishable = new List<ICIT_BR_Perishable>();
        PropertyFile objProFile = new PropertyFile();
        public static DataTable dt_PurQuat;
        bool flag = false;
        List<Database.tblsetupsalesh> Listtblsetupsalesh = new List<tblsetupsalesh>();
        bool FirstFlag = true;
        int TID, LID, UID, EMPID, Transid, Transsubid, userID1, userTypeid, CID = 0;
        string LangID, CURRENCY, USERID = "";
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
                BINDHDDATA();

                int MAXID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                txtTraNoHD.Text = MAXID.ToString();
                txtOrderDate.Text = DateTime.Now.ToString("dd-MMM-yy");
                if (Request.QueryString["MYPRODID"] != null)
                {
                    int Program = Convert.ToInt32(Request.QueryString["MYPRODID"]);
                    ddlProduct.SelectedValue = Program.ToString();
                    Classes.EcommAdminClass.BindProdu(Program, ddlUOM, txtDescription, txtserchProduct, TID);
                    ListItems.Visible = false;
                    string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
                    ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
                }
                else if (Request.QueryString["TactionNo"] != null)
                {
                    int Tazction = Convert.ToInt32(Request.QueryString["TactionNo"]);
                    EditSalesh(Tazction);

                }
                else
                {
                    if (flag == false)
                    {
                        if (DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && p.Status == "SO" || p.Status == "DSO").Count() > 0)
                        {
                            LastData();
                            flag = true;
                        }
                    }
                }
                //BindDRPRefNo();
                ViewState["ICIT_BR_Perishable"] = null;
                ManageLang();

            }

            pnlSuccessMsg123.Visible = false;
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
            // Remain ACM Work
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
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }
        public void GetShow()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-1  getshow";
            lblProduct1s.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier1s.Attributes["class"] = lblLocalForeign1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblProjectNo1s.Attributes["class"] = lblQuantity1s.Attributes["class"] = lblUnitPriceForeign1s.Attributes["class"] = lblUnitPricelocal1s.Attributes["class"] = lblDiscount1s.Attributes["class"] = lblTax1s.Attributes["class"] = lblTotalCurrencyForeign1s.Attributes["class"] = lblTotalCurrencyLocal1s.Attributes["class"] = lblReference1s.Attributes["class"] = lblTerms1s.Attributes["class"] = lblCRMActivity1s.Attributes["class"] = lblUnitofMeasure1s.Attributes["class"] = lblNo1s.Attributes["class"] = "control-label col-md-3  getshow";
            lblOHCost1s.Attributes["class"] = lblPerUnitValue1s.Attributes["class"] = lblTotal1s.Attributes["class"] = "control-label col-md-12  getshow";
            lblOverheadCost1s.Attributes["class"] = lblOverheadCost11s.Attributes["class"] = lblOverheadAmount1s.Attributes["class"] = lblNotes11s.Attributes["class"] = lblAccountID1s.Attributes["class"] = lblDel1s.Attributes["class"] = lblProductCodeProductName1s.Attributes["class"] = lblQuantity11s.Attributes["class"] = lblUnitPrice1s.Attributes["class"] = lblTax11s.Attributes["class"] = lblTotal11s.Attributes["class"] = lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblMultiUOM1s.Attributes["class"] = lblUOM1s.Attributes["class"] = lblUomNewQty1s.Attributes["class"] = lblMultiColoer1s.Attributes["class"] = lblMultiColoer11s.Attributes["class"] = lblColoerNewQty1s.Attributes["class"] = lblMultiSize1s.Attributes["class"] = lblSize1s.Attributes["class"] = lblSizeNewQty1s.Attributes["class"] = lblPerishable1s.Attributes["class"] = lblMultiBin1s.Attributes["class"] = lblBin1s.Attributes["class"] = lblQty1s.Attributes["class"] = lblSerialization1s.Attributes["class"] = lblSerializationNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblOverheadCost2h.Attributes["class"] = lblOverheadCost12h.Attributes["class"] = lblOverheadAmount2h.Attributes["class"] = lblNotes12h.Attributes["class"] = lblAccountID2h.Attributes["class"] = lblDel2h.Attributes["class"] = lblProductCodeProductName2h.Attributes["class"] = lblQuantity12h.Attributes["class"] = lblUnitPrice2h.Attributes["class"] = lblTax12h.Attributes["class"] = lblTotal12h.Attributes["class"] = lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblMultiUOM2h.Attributes["class"] = lblUOM2h.Attributes["class"] = lblUomNewQty2h.Attributes["class"] = lblMultiColoer2h.Attributes["class"] = lblMultiColoer12h.Attributes["class"] = lblColoerNewQty2h.Attributes["class"] = lblMultiSize2h.Attributes["class"] = lblSize2h.Attributes["class"] = lblSizeNewQty2h.Attributes["class"] = lblPerishable2h.Attributes["class"] = lblMultiBin2h.Attributes["class"] = lblBin2h.Attributes["class"] = lblQty2h.Attributes["class"] = lblSerialization2h.Attributes["class"] = lblSerializationNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  gethide";
            lblProduct2h.Attributes["class"] = "control-label col-md-2 gethide";
            lblSupplier2h.Attributes["class"] = lblLocalForeign2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblProjectNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = lblQuantity2h.Attributes["class"] = lblUnitPriceForeign2h.Attributes["class"] = lblUnitPricelocal2h.Attributes["class"] = lblDiscount2h.Attributes["class"] = lblTax2h.Attributes["class"] = lblTotalCurrencyForeign2h.Attributes["class"] = lblTotalCurrencyLocal2h.Attributes["class"] = lblTransactionDate2h.Attributes["class"] = lblUnitofMeasure2h.Attributes["class"] = lblTerms2h.Attributes["class"] = lblCRMActivity2h.Attributes["class"] = lblNo2h.Attributes["class"] = "control-label col-md-3 gethide";
            lblOHCost2h.Attributes["class"] = lblPerUnitValue2h.Attributes["class"] = lblTotal2h.Attributes["class"] = "control-label col-md-12 gethide";
            lblNotes2h.Attributes["class"] = "control-label col-md-1 gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }
        public void GetHide()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-1  gethide";
            lblProduct1s.Attributes["class"] = "control-label col-md-2  gethide";
            lblSupplier1s.Attributes["class"] = lblLocalForeign1s.Attributes["class"] = lblNo1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblProjectNo1s.Attributes["class"] = lblQuantity1s.Attributes["class"] = lblUnitPriceForeign1s.Attributes["class"] = lblUnitPricelocal1s.Attributes["class"] = lblDiscount1s.Attributes["class"] = lblTax1s.Attributes["class"] = lblTotalCurrencyForeign1s.Attributes["class"] = lblTotalCurrencyLocal1s.Attributes["class"] = lblReference1s.Attributes["class"] = lblUnitofMeasure1s.Attributes["class"] = lblTerms1s.Attributes["class"] = lblCRMActivity1s.Attributes["class"] = "control-label col-md-3  gethide";
            lblOHCost1s.Attributes["class"] = lblPerUnitValue1s.Attributes["class"] = lblTotal1s.Attributes["class"] = "control-label col-md-12  gethide";
            lblOverheadCost1s.Attributes["class"] = lblOverheadCost11s.Attributes["class"] = lblOverheadAmount1s.Attributes["class"] = lblNotes11s.Attributes["class"] = lblAccountID1s.Attributes["class"] = lblDel1s.Attributes["class"] = lblProductCodeProductName1s.Attributes["class"] = lblQuantity11s.Attributes["class"] = lblUnitPrice1s.Attributes["class"] = lblTax11s.Attributes["class"] = lblTotal11s.Attributes["class"] = lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblMultiUOM1s.Attributes["class"] = lblUOM1s.Attributes["class"] = lblUomNewQty1s.Attributes["class"] = lblMultiColoer1s.Attributes["class"] = lblMultiColoer11s.Attributes["class"] = lblColoerNewQty1s.Attributes["class"] = lblMultiSize1s.Attributes["class"] = lblSize1s.Attributes["class"] = lblSizeNewQty1s.Attributes["class"] = lblPerishable1s.Attributes["class"] = lblMultiBin1s.Attributes["class"] = lblBin1s.Attributes["class"] = lblQty1s.Attributes["class"] = lblSerialization1s.Attributes["class"] = lblSerializationNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblOverheadCost2h.Attributes["class"] = lblOverheadCost12h.Attributes["class"] = lblOverheadAmount2h.Attributes["class"] = lblNotes12h.Attributes["class"] = lblAccountID2h.Attributes["class"] = lblDel2h.Attributes["class"] = lblProductCodeProductName2h.Attributes["class"] = lblQuantity12h.Attributes["class"] = lblUnitPrice2h.Attributes["class"] = lblTax12h.Attributes["class"] = lblTotal12h.Attributes["class"] = lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblMultiUOM2h.Attributes["class"] = lblUOM2h.Attributes["class"] = lblUomNewQty2h.Attributes["class"] = lblMultiColoer2h.Attributes["class"] = lblMultiColoer12h.Attributes["class"] = lblColoerNewQty2h.Attributes["class"] = lblMultiSize2h.Attributes["class"] = lblSize2h.Attributes["class"] = lblSizeNewQty2h.Attributes["class"] = lblPerishable2h.Attributes["class"] = lblMultiBin2h.Attributes["class"] = lblBin2h.Attributes["class"] = lblQty2h.Attributes["class"] = lblSerialization2h.Attributes["class"] = lblSerializationNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  getshow";
            lblProduct2h.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier2h.Attributes["class"] = lblLocalForeign2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblProjectNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = lblQuantity2h.Attributes["class"] = lblUnitPriceForeign2h.Attributes["class"] = lblUnitPricelocal2h.Attributes["class"] = lblDiscount2h.Attributes["class"] = lblTax2h.Attributes["class"] = lblTotalCurrencyForeign2h.Attributes["class"] = lblTotalCurrencyLocal2h.Attributes["class"] = lblTransactionDate2h.Attributes["class"] = lblUnitofMeasure2h.Attributes["class"] = lblTerms2h.Attributes["class"] = lblCRMActivity2h.Attributes["class"] = lblNo2h.Attributes["class"] = "control-label col-md-3  getshow";
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
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblNo2h.Visible = lblLocalForeign2h.Visible = lblCurrency2h.Visible = lblBatchNo2h.Visible = lblProjectNo2h.Visible = lblReference2h.Visible = lblTerms2h.Visible = lblCRMActivity2h.Visible = lblOHCost2h.Visible = lblPerUnitValue2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblOverheadCost2h.Visible = lblOverheadCost12h.Visible = lblOverheadAmount2h.Visible = lblNotes12h.Visible = lblAccountID2h.Visible = lblDel2h.Visible = lblProduct2h.Visible = lblUnitofMeasure2h.Visible = lblQuantity2h.Visible = lblUnitPriceForeign2h.Visible = lblUnitPricelocal2h.Visible = lblDiscount2h.Visible = lblTax2h.Visible = lblTotalCurrencyForeign2h.Visible = lblTotalCurrencyLocal2h.Visible = lblProductCodeProductName2h.Visible = lblQuantity12h.Visible = lblUnitPrice2h.Visible = lblTax12h.Visible = lblTotal12h.Visible = lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblMultiUOM2h.Visible = lblUOM2h.Visible = lblUomNewQty2h.Visible = lblMultiColoer2h.Visible = lblMultiColoer12h.Visible = lblColoerNewQty2h.Visible = lblMultiSize2h.Visible = lblSize2h.Visible = lblSizeNewQty2h.Visible = lblPerishable2h.Visible = lblMultiBin2h.Visible = lblBin2h.Visible = lblQty2h.Visible = lblSerialization2h.Visible = lblSerializationNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = false;
                    //2true
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtNo2h.Visible = txtLocalForeign2h.Visible = txtCurrency2h.Visible = txtBatchNo2h.Visible = txtProjectNo2h.Visible = txtReference2h.Visible = txtTerms2h.Visible = txtCRMActivity2h.Visible = txtOHCost2h.Visible = txtPerUnitValue2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtOverheadCost2h.Visible = txtOverheadCost12h.Visible = txtOverheadAmount2h.Visible = txtNotes12h.Visible = txtAccountID2h.Visible = txtDel2h.Visible = txtProduct2h.Visible = txtUnitofMeasure2h.Visible = txtQuantity2h.Visible = txtUnitPriceForeign2h.Visible = txtUnitPricelocal2h.Visible = txtDiscount2h.Visible = txtTax2h.Visible = txtTotalCurrencyForeign2h.Visible = txtTotalCurrencyLocal2h.Visible = txtProductCodeProductName2h.Visible = txtQuantity12h.Visible = txtUnitPrice2h.Visible = txtTax12h.Visible = txtTotal12h.Visible = txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtMultiUOM2h.Visible = txtUOM2h.Visible = txtUomNewQty2h.Visible = txtMultiColoer2h.Visible = txtMultiColoer12h.Visible = txtColoerNewQty2h.Visible = txtMultiSize2h.Visible = txtSize2h.Visible = txtSizeNewQty2h.Visible = txtPerishable2h.Visible = txtMultiBin2h.Visible = txtBin2h.Visible = txtQty2h.Visible = txtSerialization2h.Visible = txtSerializationNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = true;

                    //header

                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblNo2h.Visible = lblLocalForeign2h.Visible = lblCurrency2h.Visible = lblBatchNo2h.Visible = lblProjectNo2h.Visible = lblReference2h.Visible = lblTerms2h.Visible = lblCRMActivity2h.Visible = lblOHCost2h.Visible = lblPerUnitValue2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblOverheadCost2h.Visible = lblOverheadCost12h.Visible = lblOverheadAmount2h.Visible = lblNotes12h.Visible = lblAccountID2h.Visible = lblDel2h.Visible = lblProduct2h.Visible = lblUnitofMeasure2h.Visible = lblQuantity2h.Visible = lblUnitPriceForeign2h.Visible = lblUnitPricelocal2h.Visible = lblDiscount2h.Visible = lblTax2h.Visible = lblTotalCurrencyForeign2h.Visible = lblTotalCurrencyLocal2h.Visible = lblProductCodeProductName2h.Visible = lblQuantity12h.Visible = lblUnitPrice2h.Visible = lblTax12h.Visible = lblTotal12h.Visible = lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblMultiUOM2h.Visible = lblUOM2h.Visible = lblUomNewQty2h.Visible = lblMultiColoer2h.Visible = lblMultiColoer12h.Visible = lblColoerNewQty2h.Visible = lblMultiSize2h.Visible = lblSize2h.Visible = lblSizeNewQty2h.Visible = lblPerishable2h.Visible = lblMultiBin2h.Visible = lblBin2h.Visible = lblQty2h.Visible = lblSerialization2h.Visible = lblSerializationNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = true;
                    //2false
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtNo2h.Visible = txtLocalForeign2h.Visible = txtCurrency2h.Visible = txtBatchNo2h.Visible = txtProjectNo2h.Visible = txtReference2h.Visible = txtTerms2h.Visible = txtCRMActivity2h.Visible = txtOHCost2h.Visible = txtPerUnitValue2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtOverheadCost2h.Visible = txtOverheadCost12h.Visible = txtOverheadAmount2h.Visible = txtNotes12h.Visible = txtAccountID2h.Visible = txtDel2h.Visible = txtProduct2h.Visible = txtUnitofMeasure2h.Visible = txtQuantity2h.Visible = txtUnitPriceForeign2h.Visible = txtUnitPricelocal2h.Visible = txtDiscount2h.Visible = txtTax2h.Visible = txtTotalCurrencyForeign2h.Visible = txtTotalCurrencyLocal2h.Visible = txtProductCodeProductName2h.Visible = txtQuantity12h.Visible = txtUnitPrice2h.Visible = txtTax12h.Visible = txtTotal12h.Visible = txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtMultiUOM2h.Visible = txtUOM2h.Visible = txtUomNewQty2h.Visible = txtMultiColoer2h.Visible = txtMultiColoer12h.Visible = txtColoerNewQty2h.Visible = txtMultiSize2h.Visible = txtSize2h.Visible = txtSizeNewQty2h.Visible = txtPerishable2h.Visible = txtMultiBin2h.Visible = txtBin2h.Visible = txtQty2h.Visible = txtSerialization2h.Visible = txtSerializationNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = false;

                    //header

                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblNo1s.Visible = lblLocalForeign1s.Visible = lblCurrency1s.Visible = lblBatchNo1s.Visible = lblProjectNo1s.Visible = lblReference1s.Visible = lblTerms1s.Visible = lblCRMActivity1s.Visible = lblOHCost1s.Visible = lblPerUnitValue1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblOverheadCost1s.Visible = lblOverheadCost11s.Visible = lblOverheadAmount1s.Visible = lblNotes11s.Visible = lblAccountID1s.Visible = lblDel1s.Visible = lblProduct1s.Visible = lblUnitofMeasure1s.Visible = lblQuantity1s.Visible = lblUnitPriceForeign1s.Visible = lblUnitPricelocal1s.Visible = lblDiscount1s.Visible = lblTax1s.Visible = lblTotalCurrencyForeign1s.Visible = lblTotalCurrencyLocal1s.Visible = lblProductCodeProductName1s.Visible = lblQuantity11s.Visible = lblUnitPrice1s.Visible = lblTax11s.Visible = lblTotal11s.Visible = lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblMultiUOM1s.Visible = lblUOM1s.Visible = lblUomNewQty1s.Visible = lblMultiColoer1s.Visible = lblMultiColoer11s.Visible = lblColoerNewQty1s.Visible = lblMultiSize1s.Visible = lblSize1s.Visible = lblSizeNewQty1s.Visible = lblPerishable1s.Visible = lblMultiBin1s.Visible = lblBin1s.Visible = lblQty1s.Visible = lblSerialization1s.Visible = lblSerializationNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = false;
                    //1true
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtNo1s.Visible = txtLocalForeign1s.Visible = txtCurrency1s.Visible = txtBatchNo1s.Visible = txtProjectNo1s.Visible = txtReference1s.Visible = txtTerms1s.Visible = txtCRMActivity1s.Visible = txtOHCost1s.Visible = txtPerUnitValue1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtOverheadCost1s.Visible = txtOverheadCost11s.Visible = txtOverheadAmount1s.Visible = txtNotes11s.Visible = txtAccountID1s.Visible = txtDel1s.Visible = txtProduct1s.Visible = txtUnitofMeasure1s.Visible = txtQuantity1s.Visible = txtUnitPriceForeign1s.Visible = txtUnitPricelocal1s.Visible = txtDiscount1s.Visible = txtTax1s.Visible = txtTotalCurrencyForeign1s.Visible = txtTotalCurrencyLocal1s.Visible = txtProductCodeProductName1s.Visible = txtQuantity11s.Visible = txtUnitPrice1s.Visible = txtTax11s.Visible = txtTotal11s.Visible = txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtMultiUOM1s.Visible = txtUOM1s.Visible = txtUomNewQty1s.Visible = txtMultiColoer1s.Visible = txtMultiColoer11s.Visible = txtColoerNewQty1s.Visible = txtMultiSize1s.Visible = txtSize1s.Visible = txtSizeNewQty1s.Visible = txtPerishable1s.Visible = txtMultiBin1s.Visible = txtBin1s.Visible = txtQty1s.Visible = txtSerialization1s.Visible = txtSerializationNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = true;
                    //header

                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblNo1s.Visible = lblLocalForeign1s.Visible = lblCurrency1s.Visible = lblBatchNo1s.Visible = lblProjectNo1s.Visible = lblReference1s.Visible = lblTerms1s.Visible = lblCRMActivity1s.Visible = lblOHCost1s.Visible = lblPerUnitValue1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblOverheadCost1s.Visible = lblOverheadCost11s.Visible = lblOverheadAmount1s.Visible = lblNotes11s.Visible = lblAccountID1s.Visible = lblDel1s.Visible = lblProduct1s.Visible = lblUnitofMeasure1s.Visible = lblQuantity1s.Visible = lblUnitPriceForeign1s.Visible = lblUnitPricelocal1s.Visible = lblDiscount1s.Visible = lblTax1s.Visible = lblTotalCurrencyForeign1s.Visible = lblTotalCurrencyLocal1s.Visible = lblProductCodeProductName1s.Visible = lblQuantity11s.Visible = lblUnitPrice1s.Visible = lblTax11s.Visible = lblTotal11s.Visible = lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblMultiUOM1s.Visible = lblUOM1s.Visible = lblUomNewQty1s.Visible = lblMultiColoer1s.Visible = lblMultiColoer11s.Visible = lblColoerNewQty1s.Visible = lblMultiSize1s.Visible = lblSize1s.Visible = lblSizeNewQty1s.Visible = lblPerishable1s.Visible = lblMultiBin1s.Visible = lblBin1s.Visible = lblQty1s.Visible = lblSerialization1s.Visible = lblSerializationNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = true;
                    //1false
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtNo1s.Visible = txtLocalForeign1s.Visible = txtCurrency1s.Visible = txtBatchNo1s.Visible = txtProjectNo1s.Visible = txtReference1s.Visible = txtTerms1s.Visible = txtCRMActivity1s.Visible = txtOHCost1s.Visible = txtPerUnitValue1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtOverheadCost1s.Visible = txtOverheadCost11s.Visible = txtOverheadAmount1s.Visible = txtNotes11s.Visible = txtAccountID1s.Visible = txtDel1s.Visible = txtProduct1s.Visible = txtUnitofMeasure1s.Visible = txtQuantity1s.Visible = txtUnitPriceForeign1s.Visible = txtUnitPricelocal1s.Visible = txtDiscount1s.Visible = txtTax1s.Visible = txtTotalCurrencyForeign1s.Visible = txtTotalCurrencyLocal1s.Visible = txtProductCodeProductName1s.Visible = txtQuantity11s.Visible = txtUnitPrice1s.Visible = txtTax11s.Visible = txtTotal11s.Visible = txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtMultiUOM1s.Visible = txtUOM1s.Visible = txtUomNewQty1s.Visible = txtMultiColoer1s.Visible = txtMultiColoer11s.Visible = txtColoerNewQty1s.Visible = txtMultiSize1s.Visible = txtSize1s.Visible = txtSizeNewQty1s.Visible = txtPerishable1s.Visible = txtMultiBin1s.Visible = txtBin1s.Visible = txtQty1s.Visible = txtSerialization1s.Visible = txtSerializationNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = false;
                    //header

                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((Sales_Master)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((Sales_Master)this.Master).Bindxml("tbl_sleshoder").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
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
                else if (lblOverheadCost1s.ID == item.LabelID)
                    txtOverheadCost1s.Text = lblOverheadCost1s.Text = item.LabelName;
                else if (lblOverheadCost11s.ID == item.LabelID)
                    txtOverheadCost11s.Text = lblOverheadCost11s.Text = item.LabelName;
                else if (lblOverheadAmount1s.ID == item.LabelID)
                    txtOverheadAmount1s.Text = lblOverheadAmount1s.Text = item.LabelName;
                else if (lblNotes11s.ID == item.LabelID)
                    txtNotes11s.Text = lblNotes11s.Text = item.LabelName;
                else if (lblAccountID1s.ID == item.LabelID)
                    txtAccountID1s.Text = lblAccountID1s.Text = item.LabelName;
                else if (lblDel1s.ID == item.LabelID)
                    txtDel1s.Text = lblDel1s.Text = item.LabelName;
                else if (lblProduct1s.ID == item.LabelID)
                    txtProduct1s.Text = lblProduct1s.Text = item.LabelName;
                else if (lblUnitofMeasure1s.ID == item.LabelID)
                    txtUnitofMeasure1s.Text = lblUnitofMeasure1s.Text = item.LabelName;
                else if (lblQuantity1s.ID == item.LabelID)
                    txtQuantity1s.Text = lblQuantity1s.Text = item.LabelName;
                else if (lblUnitPriceForeign1s.ID == item.LabelID)
                    txtUnitPriceForeign1s.Text = lblUnitPriceForeign1s.Text = item.LabelName;
                else if (lblUnitPricelocal1s.ID == item.LabelID)
                    txtUnitPricelocal1s.Text = lblUnitPricelocal1s.Text = item.LabelName;
                else if (lblDiscount1s.ID == item.LabelID)
                    txtDiscount1s.Text = lblDiscount1s.Text = item.LabelName;
                else if (lblTax1s.ID == item.LabelID)
                    txtTax1s.Text = lblTax1s.Text = item.LabelName;
                else if (lblTotalCurrencyForeign1s.ID == item.LabelID)
                    txtTotalCurrencyForeign1s.Text = lblTotalCurrencyForeign1s.Text = item.LabelName;
                else if (lblTotalCurrencyLocal1s.ID == item.LabelID)
                    txtTotalCurrencyLocal1s.Text = lblTotalCurrencyLocal1s.Text = item.LabelName;
                else if (lblProductCodeProductName1s.ID == item.LabelID)
                    txtProductCodeProductName1s.Text = lblProductCodeProductName1s.Text = item.LabelName;
                else if (lblQuantity11s.ID == item.LabelID)
                    txtQuantity11s.Text = lblQuantity11s.Text = item.LabelName;
                else if (lblUnitPrice1s.ID == item.LabelID)
                    txtUnitPrice1s.Text = lblUnitPrice1s.Text = item.LabelName;
                else if (lblTax11s.ID == item.LabelID)
                    txtTax11s.Text = lblTax11s.Text = item.LabelName;
                else if (lblTotal11s.ID == item.LabelID)
                    txtTotal11s.Text = lblTotal11s.Text = item.LabelName;


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
                else if (lblMultiUOM1s.ID == item.LabelID)
                    txtMultiUOM1s.Text = lblMultiUOM1s.Text = item.LabelName;
                else if (lblUOM1s.ID == item.LabelID)
                    txtUOM1s.Text = lblUOM1s.Text = item.LabelName;
                else if (lblUomNewQty1s.ID == item.LabelID)
                    txtUomNewQty1s.Text = lblUomNewQty1s.Text = item.LabelName;
                else if (lblMultiColoer1s.ID == item.LabelID)
                    txtMultiColoer1s.Text = lblMultiColoer1s.Text = item.LabelName;
                else if (lblMultiColoer11s.ID == item.LabelID)
                    txtMultiColoer11s.Text = lblMultiColoer11s.Text = item.LabelName;
                else if (lblColoerNewQty1s.ID == item.LabelID)
                    txtColoerNewQty1s.Text = lblColoerNewQty1s.Text = item.LabelName;
                else if (lblMultiSize1s.ID == item.LabelID)
                    txtMultiSize1s.Text = lblMultiSize1s.Text = item.LabelName;
                else if (lblSize1s.ID == item.LabelID)
                    txtSize1s.Text = lblSize1s.Text = item.LabelName;
                else if (lblSizeNewQty1s.ID == item.LabelID)
                    txtSizeNewQty1s.Text = lblSizeNewQty1s.Text = item.LabelName;
                else if (lblPerishable1s.ID == item.LabelID)
                    txtPerishable1s.Text = lblPerishable1s.Text = item.LabelName;

                else if (lblMultiBin1s.ID == item.LabelID)
                    txtMultiBin1s.Text = lblMultiBin1s.Text = item.LabelName;
                else if (lblBin1s.ID == item.LabelID)
                    txtBin1s.Text = lblBin1s.Text = item.LabelName;
                else if (lblQty1s.ID == item.LabelID)
                    txtQty1s.Text = lblQty1s.Text = item.LabelName;
                else if (lblSerialization1s.ID == item.LabelID)
                    txtSerialization1s.Text = lblSerialization1s.Text = item.LabelName;
                else if (lblSerializationNo1s.ID == item.LabelID)
                    txtSerializationNo1s.Text = lblSerializationNo1s.Text = item.LabelName;
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
                else if (lblOverheadCost2h.ID == item.LabelID)
                    txtOverheadCost2h.Text = lblOverheadCost2h.Text = item.LabelName;
                else if (lblOverheadCost12h.ID == item.LabelID)
                    txtOverheadCost12h.Text = lblOverheadCost12h.Text = item.LabelName;
                else if (lblOverheadAmount2h.ID == item.LabelID)
                    txtOverheadAmount2h.Text = lblOverheadAmount2h.Text = item.LabelName;
                else if (lblNotes12h.ID == item.LabelID)
                    txtNotes12h.Text = lblNotes12h.Text = item.LabelName;
                else if (lblAccountID2h.ID == item.LabelID)
                    txtAccountID2h.Text = lblAccountID2h.Text = item.LabelName;
                else if (lblDel2h.ID == item.LabelID)
                    txtDel2h.Text = lblDel2h.Text = item.LabelName;
                else if (lblProduct2h.ID == item.LabelID)
                    txtProduct2h.Text = lblProduct2h.Text = item.LabelName;
                else if (lblUnitofMeasure2h.ID == item.LabelID)
                    txtUnitofMeasure2h.Text = lblUnitofMeasure2h.Text = item.LabelName;
                else if (lblQuantity2h.ID == item.LabelID)
                    txtQuantity2h.Text = lblQuantity2h.Text = item.LabelName;
                else if (lblUnitPriceForeign2h.ID == item.LabelID)
                    txtUnitPriceForeign2h.Text = lblUnitPriceForeign2h.Text = item.LabelName;
                else if (lblUnitPricelocal2h.ID == item.LabelID)
                    txtUnitPricelocal2h.Text = lblUnitPricelocal2h.Text = item.LabelName;
                else if (lblDiscount2h.ID == item.LabelID)
                    txtDiscount2h.Text = lblDiscount2h.Text = item.LabelName;
                else if (lblTax2h.ID == item.LabelID)
                    txtTax2h.Text = lblTax2h.Text = item.LabelName;
                else if (lblTotalCurrencyForeign2h.ID == item.LabelID)
                    txtTotalCurrencyForeign2h.Text = lblTotalCurrencyForeign2h.Text = item.LabelName;
                else if (lblTotalCurrencyLocal2h.ID == item.LabelID)
                    txtTotalCurrencyLocal2h.Text = lblTotalCurrencyLocal2h.Text = item.LabelName;
                else if (lblProductCodeProductName2h.ID == item.LabelID)
                    txtProductCodeProductName2h.Text = lblProductCodeProductName2h.Text = item.LabelName;
                else if (lblQuantity12h.ID == item.LabelID)
                    txtQuantity12h.Text = lblQuantity12h.Text = item.LabelName;
                else if (lblUnitPrice2h.ID == item.LabelID)
                    txtUnitPrice2h.Text = lblUnitPrice2h.Text = item.LabelName;
                else if (lblTax12h.ID == item.LabelID)
                    txtTax12h.Text = lblTax12h.Text = item.LabelName;
                else if (lblTotal12h.ID == item.LabelID)
                    txtTotal12h.Text = lblTotal12h.Text = item.LabelName;


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
                else if (lblMultiUOM2h.ID == item.LabelID)
                    txtMultiUOM2h.Text = lblMultiUOM2h.Text = item.LabelName;
                else if (lblUOM2h.ID == item.LabelID)
                    txtUOM2h.Text = lblUOM2h.Text = item.LabelName;
                else if (lblUomNewQty2h.ID == item.LabelID)
                    txtUomNewQty2h.Text = lblUomNewQty2h.Text = item.LabelName;
                else if (lblMultiColoer2h.ID == item.LabelID)
                    txtMultiColoer2h.Text = lblMultiColoer2h.Text = item.LabelName;
                else if (lblMultiColoer12h.ID == item.LabelID)
                    txtMultiColoer12h.Text = lblMultiColoer12h.Text = item.LabelName;
                else if (lblColoerNewQty2h.ID == item.LabelID)
                    txtColoerNewQty2h.Text = lblColoerNewQty2h.Text = item.LabelName;
                else if (lblMultiSize2h.ID == item.LabelID)
                    txtMultiSize2h.Text = lblMultiSize2h.Text = item.LabelName;
                else if (lblSize2h.ID == item.LabelID)
                    txtSize2h.Text = lblSize2h.Text = item.LabelName;
                else if (lblSizeNewQty2h.ID == item.LabelID)
                    txtSizeNewQty2h.Text = lblSizeNewQty2h.Text = item.LabelName;
                else if (lblPerishable2h.ID == item.LabelID)
                    txtPerishable2h.Text = lblPerishable2h.Text = item.LabelName;

                else if (lblMultiBin2h.ID == item.LabelID)
                    txtMultiBin2h.Text = lblMultiBin2h.Text = item.LabelName;
                else if (lblBin2h.ID == item.LabelID)
                    txtBin2h.Text = lblBin2h.Text = item.LabelName;
                else if (lblQty2h.ID == item.LabelID)
                    txtQty2h.Text = lblQty2h.Text = item.LabelName;
                else if (lblSerialization2h.ID == item.LabelID)
                    txtSerialization2h.Text = lblSerialization2h.Text = item.LabelName;
                else if (lblSerializationNo2h.ID == item.LabelID)
                    txtSerializationNo2h.Text = lblSerializationNo2h.Text = item.LabelName;
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
            List<Database.TBLLabelDTL> List = ((Sales_Master)this.Master).Bindxml("tbl_sleshoder").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Sales\\xml\\tbl_sleshoder.xml"));
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
                else if (lblOverheadCost1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOverheadCost1s.Text;
                else if (lblOverheadCost11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOverheadCost11s.Text;
                else if (lblOverheadAmount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOverheadAmount1s.Text;
                else if (lblNotes11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes11s.Text;
                else if (lblAccountID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAccountID1s.Text;
                else if (lblDel1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDel1s.Text;
                else if (lblProduct1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProduct1s.Text;
                else if (lblUnitofMeasure1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitofMeasure1s.Text;
                else if (lblQuantity1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity1s.Text;
                else if (lblUnitPriceForeign1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPriceForeign1s.Text;
                else if (lblUnitPricelocal1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPricelocal1s.Text;
                else if (lblDiscount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDiscount1s.Text;
                else if (lblTax1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTax1s.Text;
                else if (lblTotalCurrencyForeign1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalCurrencyForeign1s.Text;
                else if (lblTotalCurrencyLocal1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalCurrencyLocal1s.Text;
                else if (lblProductCodeProductName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductCodeProductName1s.Text;
                else if (lblQuantity11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity11s.Text;
                else if (lblUnitPrice1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPrice1s.Text;
                else if (lblTax11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTax11s.Text;
                else if (lblTotal11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotal11s.Text;


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
                else if (lblMultiUOM1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiUOM1s.Text;
                else if (lblUOM1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOM1s.Text;
                else if (lblUomNewQty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUomNewQty1s.Text;
                else if (lblMultiColoer1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiColoer1s.Text;
                else if (lblMultiColoer11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiColoer11s.Text;
                else if (lblColoerNewQty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtColoerNewQty1s.Text;
                else if (lblMultiSize1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiSize1s.Text;
                else if (lblSize1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSize1s.Text;
                else if (lblSizeNewQty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSizeNewQty1s.Text;
                else if (lblPerishable1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPerishable1s.Text;

                else if (lblMultiBin1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiBin1s.Text;
                else if (lblBin1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBin1s.Text;
                else if (lblQty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQty1s.Text;
                else if (lblSerialization1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSerialization1s.Text;
                else if (lblSerializationNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSerializationNo1s.Text;
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
                else if (lblOverheadCost2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOverheadCost2h.Text;
                else if (lblOverheadCost12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOverheadCost12h.Text;
                else if (lblOverheadAmount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOverheadAmount2h.Text;
                else if (lblNotes12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes12h.Text;
                else if (lblAccountID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAccountID2h.Text;
                else if (lblDel2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDel2h.Text;
                else if (lblProduct2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProduct2h.Text;
                else if (lblUnitofMeasure2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitofMeasure2h.Text;
                else if (lblQuantity2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity2h.Text;
                else if (lblUnitPriceForeign2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPriceForeign2h.Text;
                else if (lblUnitPricelocal2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPricelocal2h.Text;
                else if (lblDiscount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDiscount2h.Text;
                else if (lblTax2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTax2h.Text;
                else if (lblTotalCurrencyForeign2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalCurrencyForeign2h.Text;
                else if (lblTotalCurrencyLocal2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalCurrencyLocal2h.Text;
                else if (lblProductCodeProductName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductCodeProductName2h.Text;
                else if (lblQuantity12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity12h.Text;
                else if (lblUnitPrice2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPrice2h.Text;
                else if (lblTax12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTax12h.Text;
                else if (lblTotal12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotal12h.Text;


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
                else if (lblMultiUOM2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiUOM2h.Text;
                else if (lblUOM2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUOM2h.Text;
                else if (lblUomNewQty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUomNewQty2h.Text;
                else if (lblMultiColoer2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiColoer2h.Text;
                else if (lblMultiColoer12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiColoer12h.Text;
                else if (lblColoerNewQty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtColoerNewQty2h.Text;
                else if (lblMultiSize2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiSize2h.Text;
                else if (lblSize2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSize2h.Text;
                else if (lblSizeNewQty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSizeNewQty2h.Text;
                else if (lblPerishable2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPerishable2h.Text;

                else if (lblMultiBin2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiBin2h.Text;
                else if (lblBin2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBin2h.Text;
                else if (lblQty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQty2h.Text;
                else if (lblSerialization2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSerialization2h.Text;
                else if (lblSerializationNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSerializationNo2h.Text;
                else if (lblSendMail2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSendMail2h.Text;
                else if (lblToRecipients2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToRecipients2h.Text;
                else if (lblCC2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCC2h.Text;


            }
            ds.WriteXml(Server.MapPath("\\Sales\\xml\\tbl_sleshoder.xml"));

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
            ddlProduct.Enabled = ddlUOM.Enabled = drpterms.Enabled = rbtPsle.Enabled = false;
            chbDeliNote.Enabled = drpselsmen.Enabled = false;
            txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = false;
            ddlLocalForeign.Enabled = txtLocationSearch.Enabled = ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = false;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = txtOHCostHD.Enabled = txttotxl.Enabled = false;

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
                ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = false;
            }
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                LinkButton lnkbtndelete = (LinkButton)Repeater2.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkbtnItem = (LinkButton)Repeater2.Items[i].FindControl("lnkbtnItem");
                lnkbtnItem.Enabled = lnkbtndelete.Enabled = false;
            }
            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
                TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                txtrefresh.Enabled = txtammunt.Enabled = btnaddamunt.Enabled = drppaymentMeted.Enabled = false;
            }

            btnSubmit.Visible = btnConfirmOrder.Visible = btnPrint.Visible = hlPeri.Visible = btnSaveData.Visible = Custmer.Visible = BTNsAVEcONFsA.Visible = false;
            btnNew.Visible = true;
            btnnewAdd.Visible = true;
            btnrefesh.Visible = false;
            ButtonAdd.Enabled = buttonUpdate.Enabled = btnAddItemsIn.Enabled = btnAddDT.Enabled = btndiscartitems.Enabled = btndiscorohcost.Enabled = false;

        }
        public void WrietMode()
        {
            ddlProduct.Enabled = ddlUOM.Enabled = drpterms.Enabled = rbtPsle.Enabled = true;
            chbDeliNote.Enabled = drpselsmen.Enabled = true;
            txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = true;
            ddlLocalForeign.Enabled = txtLocationSearch.Enabled = ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = true;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = true;

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
                ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = lnkbtndelete1.Enabled = true;
            }
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                LinkButton lnkbtndelete = (LinkButton)Repeater2.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkbtnItem = (LinkButton)Repeater2.Items[i].FindControl("lnkbtnItem");
                lnkbtnItem.Enabled = lnkbtndelete.Enabled = true;
            }
            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
                TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                txtrefresh.Enabled = txtammunt.Enabled = btnaddamunt.Enabled = drppaymentMeted.Enabled = true;
            }
            btnSubmit.Visible = btnConfirmOrder.Visible = btnPrint.Visible = hlPeri.Visible = btnSaveData.Visible = Custmer.Visible = BTNsAVEcONFsA.Visible = true;
            btnNew.Visible = false;
            btnnewAdd.Visible = false;
            btnrefesh.Visible = true;
            ButtonAdd.Enabled = buttonUpdate.Enabled = btnAddItemsIn.Enabled = btnAddDT.Enabled = btndiscartitems.Enabled = btndiscorohcost.Enabled = true;
        }
        public void LastData()
        {
            int Tarjictio = Convert.ToInt32(DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && p.Status == "SO" || p.Status == "DSO").Max(p => p.MYTRANSID));
            int MYTID = Tarjictio;
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).Count() > 0)
            {
                ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                ddlLocalForeign.SelectedValue = OBJICTR_HD.LF == "L" ? "8866" : "8867";
                int SID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
                txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COMPNAME1;
                HiddenField3.Value = OBJICTR_HD.CUSTVENDID.ToString();

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
                var ListOvercost = DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).ToList();
                if (ListOvercost.Count() > 0)
                {
                    ViewState["ListOverCost"] = ListOvercost;
                    Repeater1.DataSource = (List<ICOVERHEADCOST>)ViewState["ListOverCost"];
                    Repeater1.DataBind();
                    ViewState["TempList"] = ListOvercost;
                }
                else
                {
                    Repeater1.DataSource = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
                    Repeater1.DataBind();
                }
                var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MYTID && p.TenentID == TID).ToList();
                if (listcost.Count() > 0)
                {
                    ViewState["TempEco_ICCATEGORY"] = listcost;
                    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                    Repeater3.DataBind();
                }
                else
                {
                    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                    Repeater3.DataBind();
                }

                decimal Sum = Convert.ToDecimal(ListOvercost.Sum(p => p.NEWCOST));
                txtOHCostHD.Text = Sum.ToString();
                lblcr.Text = Sum.ToString();

                btnAddItemsIn.Visible = true;
                btnAddDT.Visible = false;
                BindDT(MYTID);
                readMode();
                panelRed.Visible = false;
            }

        }
        public void BindData()
        {
            ICOVERHEADCOST objEco_ICEXTRACOST = new ICOVERHEADCOST();
            TempList.Add(objEco_ICEXTRACOST);
            string USERID = UID.ToString();

            ViewState["TempList"] = TempList;
            Repeater1.DataSource = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
            Repeater1.DataBind();

            ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Repeater3.DataBind();
            //Repeater1.DataSource = null ;
            //Repeater1.DataBind();
            Classes.EcommAdminClass.getdropdown(ddlLocalForeign, TID, "LF", "OTH", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlCrmAct, TID, "ACTVTY", "Transactions", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpterms, TID, "Terms", "Terms", "", "REFTABLE");
            //  Classes.EcommAdminClass.getdropdown(ddlSupplier, TID, "1", "2", "", "TBLCOMPANYSETUP");
            //  Classes.EcommAdminClass.getdropdown(ddlcustmoerlist, TID, "1", "2", "", "TBLCOMPANYSETUP");
            Classes.EcommAdminClass.getdropdown(ddlCurrency, TID, "", "", "", "tblCOUNTRY");
            Classes.EcommAdminClass.getdropdown(ddlProjectNo, TID, "", "", "", "TBLPROJECT");
            BindProduct();
            // Classes.EcommAdminClass.getdropdown(ddlProduct, TID, "", "", "", "TBLPRODUCT");
            Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
            Classes.EcommAdminClass.getdropdown(drpRefnstype, TID, "Reference", "RefSubType", "Sales", "REFTABLE");

            drpselsmen.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID && p.LocationID == LID && p.Deleted == true);
            drpselsmen.DataTextField = "firstname";
            drpselsmen.DataValueField = "employeeID";
            drpselsmen.DataBind();
            drpselsmen.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Salesman--", "0"));


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
        public void BINDHDDATA()
        {
            grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO")).OrderByDescending(p => p.TRANSDATE);
            grdPO.DataBind();
            for (int i = 0; i < grdPO.Items.Count; i++)
            {
                LinkButton Print = (LinkButton)grdPO.Items[i].FindControl("Print");
                Label lbluserid = (Label)grdPO.Items[i].FindControl("lbluserid");
                Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                //int UIDHD = Convert.ToInt32(lbluserid.Text);
                string Steate = lblStatus.Text;
                if (Steate == "DSO")
                    Print.Visible = false;
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TempList = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
            ICOVERHEADCOST objEco_ICEXTRACOST = new ICOVERHEADCOST();
            objEco_ICEXTRACOST.TenentID = TID;
            objEco_ICEXTRACOST.MYTRANSID = Convert.ToInt32(txtTraNoHD.Text);
            objEco_ICEXTRACOST.MYID = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
            objEco_ICEXTRACOST.OVERHEADCOSTID = 0;
            objEco_ICEXTRACOST.OLDCOST = 0;
            objEco_ICEXTRACOST.NEWCOST = 0;
            objEco_ICEXTRACOST.TOTQTY = 0;
            objEco_ICEXTRACOST.TOTCOST = 0;
            objEco_ICEXTRACOST.OTHERCOST = 0;
            objEco_ICEXTRACOST.UNITCOST = 0;
            objEco_ICEXTRACOST.Note = "";
            TempList.Add(objEco_ICEXTRACOST);
            ViewState["TempList"] = TempList;
            Repeater1.DataSource = TempList;
            Repeater1.DataBind();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int COMPID = 826667;
                string Status = "";
                int TransNo = 0;               
                string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
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
                    string MYSYSNAME = "SAL".ToString();
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
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(lblcr.Text);
                        string BATCHNO = txtBatchNo.Text;
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
                        //ICIT_BR objICIT_BR = new ICIT_BR();
                        //objICIT_BR.TenentID = TID;
                        //objICIT_BR.MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        //objICIT_BR.period_code = objProFile.PERIOD_CODE;
                        //objICIT_BR.MySysName = "SAL";
                        //objICIT_BR.UOM = Convert.ToInt32(lblUOM.Text);
                        //objICIT_BR.UnitCost = Convert.ToDecimal(lblUNITPRICE.Text);
                        //objICIT_BR.MYTRANSID = TransNo;
                        //objICIT_BR.Bin_Per = "N";
                        //objICIT_BR.OpQty = Convert.ToInt32(lblDisQnty.Text);
                        //objICIT_BR.Reference = txtrefreshno.Text;
                        //objICIT_BR.Active = "Y";
                        //DB.ICIT_BR.AddObject(objICIT_BR);
                        //DB.SaveChanges();

                        if (ddlLocalForeign.SelectedValue == "8867")
                        {
                            var List6 = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                            foreach (Database.ICTR_DTEXT item in List6)
                            {

                                DB.ICTR_DTEXT.DeleteObject(item);
                                DB.SaveChanges();
                            }
                            int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                            int MyID = DXMYID;

                            int MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Max(p => p.MyRunningSerial) + 1) : 1;
                            decimal CURRENTCONVRATE = Convert.ToDecimal(0);
                            string CURRENCY = "";
                            decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                            if (ddlCurrency.SelectedValue == "0")
                            { }
                            else
                            {
                                CURRENTCONVRATE = Convert.ToDecimal(1);
                                CURRENCY = ddlCurrency.SelectedItem.Text;
                                OTHERCURAMOUNT = Convert.ToDecimal(1);
                            }
                            int DELIVERDLOCATenentID = Convert.ToInt32(0);
                            int QUANTITYDELIVERD = Convert.ToInt32(0);
                            decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                            string ACCOUNTID = "1".ToString();
                            string Remark = "Data Insert formDtext".ToString();
                            int TransNo1 = Convert.ToInt32(0);
                            Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                        }

                        //QTY = objICTR_DT.QUANTITY;
                        //AMUT = Convert.ToDecimal(objICTR_DT.AMOUNT);

                    }
                    //  int TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    string REFERENCE = txtrefreshno.Text;
                    // int TenentID = TID;
                    // int MYTRANSID = TransNo;
                    int ToTenentID = objProFile.ToTenantID; 
                    int TOLOCATIONID = LID;
                    int transid = 21;
                    int transsubid = 221;
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = "Transfer Notes - Out";// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    //  string MYSYSNAME = "SAL".ToString();
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    string LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    //if (LF == "L")
                    //    transsubid = 441;
                    //else
                    //    transsubid = 442;
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
                    string USERID = UseID.ToString();
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    DateTime UPDTTIME = Curentdatetime;
                    int InvoiceNO = 0;
                    decimal Discount = Convert.ToDecimal(0);

                    Status = "DSO";
                    int Terms = 0;
                    string DatainserStatest = "Update";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;

                        Swit1 = "1";
                    }
                    else
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    } string BillNo = "";
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);

                    var LIstTotal = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    decimal TOALSUM = Convert.ToDecimal(LIstTotal.Sum(p => p.AMOUNT));
                    int TOTQTY = Convert.ToInt32(LIstTotal.Sum(p => p.QUANTITY));
                    var List1 = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    foreach (Database.ICOVERHEADCOST item in List1)
                    {

                        DB.ICOVERHEADCOSTs.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    for (int j = 0; j < Repeater1.Items.Count; j++)
                    {
                        DropDownList ddlOHType = (DropDownList)Repeater1.Items[j].FindControl("ddlOHType");
                        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[j].FindControl("txtOHAmntLocal");
                        TextBox txtOHNote = (TextBox)Repeater1.Items[j].FindControl("txtOHNote");
                        TextBox txtOHAMT = (TextBox)Repeater1.Items[j].FindControl("txtOHAMT");
                        if (txtOHAmntLocal.Text == "")
                        {

                        }
                        else
                        {
                            int MYID = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                            int OVERHEADCOSTID = Convert.ToInt32(ddlOHType.SelectedValue);
                            decimal TOTCOST = 0;
                            decimal OLDCOST = 0;

                            if (txtOHCostHD.Text != "")
                            {
                                TOTCOST = Convert.ToDecimal(txtOHCostHD.Text);
                                OLDCOST = Convert.ToDecimal(txtOHCostHD.Text);
                            }
                            decimal OTHERCOST = 0;
                            decimal UNITCOST = 0;
                            decimal NEWCOST = Convert.ToDecimal(txtOHAmntLocal.Text);
                            int TOTQTY2 = Convert.ToInt32(lblqtytotl.Text);
                            decimal COMPANY_SEQUENCE = UID;
                            int ICT_COMPANYID = UID;
                            //  int COMPANYID = UID;
                            string Note = txtOHNote.Text;
                            Classes.EcommAdminClass.insertICOVERHEADCOST(TenentID, MYTRANSID, MYID, OVERHEADCOSTID, OLDCOST, NEWCOST, TOTQTY2, TOTCOST, OTHERCOST, UNITCOST, CRUP_ID, COMPANY_SEQUENCE, ICT_COMPANYID, COMPANYID, Note);
                        }
                    }
                    var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    foreach (Database.ICTRPayTerms_HD item in List2)
                    {

                        DB.ICTRPayTerms_HD.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text == "")
                        {

                        }
                        else
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }
                    if (Payment == Total)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text == "" && txtammunt.Text == "")
                            {

                            }
                            else
                            {


                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();
                                ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                objICTRPayTerms_HD.TenentID = TID;
                                objICTRPayTerms_HD.MyTransID = TransNo;
                                objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                                objICTRPayTerms_HD.AccountID = 0;
                                objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                DB.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = "Total Amount not Macth";
                        return;
                    }
                    btnSaveData.Text = "Confirm Order";
                    btnConfirmOrder.Text = "Confirm Order";
                    ViewState["HDMYtrctionid"] = null;

                }
                else
                {
                    TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    string REFERENCE = txtrefreshno.Text;
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    int ToTenentID = objProFile.ToTenantID; 
                    int TOLOCATIONID = LID;
                    int transid = 21;
                    int transsubid = 221;
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = "Transfer Notes - Out";// Out Qty On Product
                    int Invoiec = Convert.ToInt32(DB.tbltranssubtypes.Single(p => p.transsubid == transsubid && p.transid == transid && p.TenentID == TID).serialno);
                    decimal COMPID1 = COMPID;
                    string MYSYSNAME = "SAL".ToString();
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
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
                    DateTime UPDTTIME = Curentdatetime;
                    string Custmerid = "";
                    string Swit1 = "";
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;

                        Swit1 = "1";
                    }
                    else
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    }
                    int InvoiceNoTaction = DB.ICTR_HD.Where(p => p.transid == transid && p.transsubid == transsubid && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.transid == transid && p.transsubid == transsubid && p.TenentID == TID).Max(p => p.InvoiceNO) + 1) : Invoiec;
                    int InvoiceNO = InvoiceNoTaction;
                    decimal Discount = Convert.ToDecimal(0);
                    Status = "DSO";
                    int Terms = 0;
                    string DatainserStatest = "Add";
                    string BillNo = "";
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
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
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(lblcr.Text);
                        string BATCHNO = txtBatchNo.Text;
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
                        //ICIT_BR objICIT_BR = new ICIT_BR();
                        //objICIT_BR.TenentID = TID;
                        //objICIT_BR.MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        //objICIT_BR.period_code = objProFile.PERIOD_CODE;
                        //objICIT_BR.MySysName = "SAL";
                        //objICIT_BR.UOM = Convert.ToInt32(lblUOM.Text);
                        //objICIT_BR.UnitCost = Convert.ToDecimal(lblUNITPRICE.Text);
                        //objICIT_BR.MYTRANSID = TransNo;
                        //objICIT_BR.Bin_Per = "N";
                        //objICIT_BR.OpQty = Convert.ToInt32(lblDisQnty.Text);
                        //objICIT_BR.Reference = txtrefreshno.Text;
                        //objICIT_BR.Active = "Y";
                        //DB.ICIT_BR.AddObject(objICIT_BR);
                        //DB.SaveChanges();
                        if (ddlLocalForeign.SelectedValue == "8867")
                        {
                            int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                            int MyID = DXMYID;

                            int MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Max(p => p.MyRunningSerial) + 1) : 1;
                            decimal CURRENTCONVRATE = Convert.ToDecimal(0);
                            string CURRENCY = "";
                            decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                            if (ddlCurrency.SelectedValue == "0")
                            { }
                            else
                            {
                                CURRENTCONVRATE = Convert.ToDecimal(1);
                                CURRENCY = ddlCurrency.SelectedItem.Text;
                                OTHERCURAMOUNT = Convert.ToDecimal(1);
                            }
                            int DELIVERDLOCATenentID = Convert.ToInt32(0);
                            int QUANTITYDELIVERD = Convert.ToInt32(0);
                            decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                            string ACCOUNTID = "1".ToString();
                            string Remark = "Data Insert formDtext".ToString();
                            int TransNo1 = Convert.ToInt32(0);
                            Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                        }


                    }

                    for (int j = 0; j < Repeater1.Items.Count; j++)
                    {
                        DropDownList ddlOHType = (DropDownList)Repeater1.Items[j].FindControl("ddlOHType");
                        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[j].FindControl("txtOHAmntLocal");
                        TextBox txtOHNote = (TextBox)Repeater1.Items[j].FindControl("txtOHNote");
                        TextBox txtOHAMT = (TextBox)Repeater1.Items[j].FindControl("txtOHAMT");
                        if (txtOHAmntLocal.Text == "")
                        {

                        }
                        else
                        {
                            int MYID = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                            int OVERHEADCOSTID = Convert.ToInt32(ddlOHType.SelectedValue);
                            decimal TOTCOST = 0;
                            decimal OLDCOST = 0;

                            if (txtOHCostHD.Text != "")
                            {
                                TOTCOST = Convert.ToDecimal(txtOHCostHD.Text);
                                OLDCOST = Convert.ToDecimal(txtOHCostHD.Text);
                            }
                            decimal OTHERCOST = 0;
                            decimal UNITCOST = 0;
                            decimal NEWCOST = Convert.ToDecimal(txtOHAmntLocal.Text);
                            int TOTQTY = Convert.ToInt32(lblqtytotl.Text);
                            decimal COMPANY_SEQUENCE = UID;
                            int ICT_COMPANYID = UID;
                            //  int COMPANYID = UID;
                            string Note = txtOHNote.Text;
                            Classes.EcommAdminClass.insertICOVERHEADCOST(TenentID, MYTRANSID, MYID, OVERHEADCOSTID, OLDCOST, NEWCOST, TOTQTY, TOTCOST, OTHERCOST, UNITCOST, CRUP_ID, COMPANY_SEQUENCE, ICT_COMPANYID, COMPANYID, Note);
                        }
                    }
                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text == "")
                        {

                        }
                        else
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }
                    if (Payment == Total)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text == "" && txtammunt.Text == "")
                            {

                            }
                            else
                            {


                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();
                                ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                objICTRPayTerms_HD.TenentID = TID;
                                objICTRPayTerms_HD.MyTransID = TransNo;
                                objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                                objICTRPayTerms_HD.AccountID = 0;
                                objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                DB.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = "Total Amount not Macth";
                        return;
                    }

                    string SupplierName = txtLocationSearch.Text;
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
                BINDHDDATA();
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
                string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
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
                    string MYSYSNAME = "SAL".ToString();
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
                        int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(lblcr.Text);
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
                        TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
                        Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
                        Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
                        Boolean Perishable = Convert.ToBoolean(obj.Perishable);
                        Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
                        Boolean Serialized = Convert.ToBoolean(obj.Serialized);
                        Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);
                        int PQTY = Convert.ToInt32(obj.QTYINHAND);
                        int Total123 = PQTY - QUANTITY;
                        obj.QTYINHAND = Total123;
                        DB.SaveChanges();
                        if (MultiBinStore == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiBIN" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //int TenentID = TID;
                                // int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int Bin_ID = listmulticolor[ii].Bin_ID;
                                int UOM1 = listmulticolor[ii].UOM;
                                //int MYTRANSID = TCID;
                                int NEWQTY = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //  int OpQty = Convert.ToInt32(listmulticolor[ii].OpQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                string BatchNo = listmulticolor[ii].BatchNo; ;
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = Convert.ToInt32(listmulticolor[ii].QtyConsumed);
                                //int QtyReserved = Convert.ToInt32(listmulticolor[ii].QtyReserved);
                                //int MinQty = 0;
                                //int MaxQty = 0;
                                //int LeadTime = 0;
                                // int CRUP_ID = 0;
                                string pagename = "Sales";
                                string Fuctions = "ADD";
                                Classes.EcommAdminClass.insertICIT_BR_BIN(TenentID, MyProdID, period_code, MySysName, Bin_ID, UOM1, LID, BatchNo, MYTRANSID, NEWQTY, Reference, Active, CRUP_ID, pagename, Fuctions);
                            }
                        }
                        if (Serialized == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Serialize" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //int TenentID = TID;
                                //int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                //string UOM = ddlUOM.SelectedValue;
                                string Serial_Number = listmulticolor[ii].Serial_Number;
                                int MyTransID = Convert.ToInt32(listmulticolor[ii].MYTRANSID);
                                string Active = "Y";
                                string Flag_GRN_GTN = "N";
                                // int CRUP_ID = 0;
                                string pagename = "Sales";
                                Classes.EcommAdminClass.insertICIT_BR_Serialize(TenentID, MyProdID, period_code, MySysName, UOM, Serial_Number, LID, MyTransID, Flag_GRN_GTN, Active, CRUP_ID, pagename);
                            }
                        }
                        if (MultiUOM != Convert.ToBoolean(1))
                        {
                            int UOM1 = Convert.ToInt32(lblUOM.Text);
                            var listICIT_BR = DB.ICIT_BR.Where(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.MySysName == MYSYSNAME && p.UOM == UOM1).ToList();
                            //foreach (Database.ICIT_BR item in listICIT_BR)
                            //{

                            //    DB.ICIT_BR.DeleteObject(item);
                            //    DB.SaveChanges();
                            //}
                            string period_code = OICODID;
                            string MySysName = "IC";

                            decimal UnitCost = UNITPRICE;
                            int NewQty = QUANTITY;
                            //int OpQty = QUANTITY;
                            string Reference = txtrefreshno.Text;
                            string Active = "Y";
                            //int OnHand = QUANTITY;
                            //int QtyOut = QUANTITY;
                            //int QtyConsumed = QUANTITY;
                            //int QtyReserved = QUANTITY;
                            //int MinQty = 0;
                            //int MaxQty = 0;
                            string Bin_Per = "N";
                            // int LeadTime = 0;
                            string Fuctions = "ADD";
                            string pagename = "Sales";
                            Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        }
                        if (MultiUOM == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "UOM" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                int UOM1 = listmulticolor[ii].UOM;

                                string period_code = listmulticolor[ii].period_code;
                                string MySysName = "IC";
                                decimal UnitCost = UNITPRICE;
                                //  int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = 0;
                                //int QtyReserved = 0;
                                //int MinQty = 0;
                                //int MaxQty = 0;
                                string Bin_Per = "N";
                                //  int LeadTime = 0;
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }
                        }

                        if (MultiColor == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiColor" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //string Reference = "";
                                // int TenentID = TID;
                                // int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int UO1M = listmulticolor[ii].UOM;
                                int SIZECODE = 999999998;
                                int COLORID = listmulticolor[ii].COLORID;
                                // int MYTRANSID = TCID;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                //int OnHand_Q = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut_Q = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = Convert.ToInt32(listmulticolor[ii].QtyConsumed);
                                //int QtyReserved = Convert.ToInt32(listmulticolor[ii].QtyReserved);
                                //int MinQty = Convert.ToInt32(listmulticolor[ii].MinQty);
                                //int MaxQty = Convert.ToInt32(listmulticolor[ii].MaxQty);
                                //int LeadTime = Convert.ToInt32(listmulticolor[ii].LeadTime);
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                // int CRUP_ID = 0;
                                Classes.EcommAdminClass.insertICIT_BR_SIZECOLOR(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }

                        }
                        if (MultiSize == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiSize" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //string Reference = "";
                                // int TenentID = TID;
                                // int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int UO1M = listmulticolor[ii].UOM;
                                int SIZECODE = listmulticolor[ii].SIZECODE;
                                int COLORID = 999999998;
                                // int MYTRANSID = TCID;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //  int OpQty =Convert.ToInt32( listmulticolor[ii].NewQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                //int OnHand_Q = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut_Q = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = Convert.ToInt32(listmulticolor[ii].QtyConsumed);
                                //int QtyReserved = Convert.ToInt32(listmulticolor[ii].QtyReserved);
                                //int MinQty = Convert.ToInt32(listmulticolor[ii].MinQty);
                                //int MaxQty = Convert.ToInt32(listmulticolor[ii].MaxQty);
                                //int LeadTime = Convert.ToInt32(listmulticolor[ii].LeadTime);
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                // int CRUP_ID = 0;
                                Classes.EcommAdminClass.insertICIT_BR_SIZE(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }

                        }
                        if (Perishable == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Perishable" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //int TenentID = TID;
                                //int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int UOM1 = listmulticolor[ii].UOM;
                                string BatchNo = listmulticolor[ii].BatchNo;
                                // int MYTRANSID = TCID;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //int OnHand = 0;
                                DateTime ProdDate = Convert.ToDateTime(listmulticolor[ii].ProdDate);
                                DateTime ExpiryDate = Convert.ToDateTime(listmulticolor[ii].ExpiryDate);
                                DateTime LeadDays2Destroy = Convert.ToDateTime(listmulticolor[ii].LeadDays2Destroy);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                //int CRUP_ID = 0;
                                Classes.EcommAdminClass.insertICIT_BR_Perishable(TenentID, MyProdID, period_code, MySysName, UOM1, BatchNo, LID, MYTRANSID, NewQty, ProdDate, ExpiryDate, LeadDays2Destroy, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }

                        }
                        // insert deta is ICTR_DTEXT
                        if (ddlLocalForeign.SelectedValue == "8867")
                        {



                            int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                            int MyID = DXMYID;

                            int MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Max(p => p.MyRunningSerial) + 1) : 1;
                            decimal CURRENTCONVRATE = Convert.ToDecimal(0);
                            string CURRENCY = "";
                            decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                            if (ddlCurrency.SelectedValue == "0")
                            { }
                            else
                            {
                                CURRENTCONVRATE = Convert.ToDecimal(1);
                                CURRENCY = ddlCurrency.SelectedItem.Text;
                                OTHERCURAMOUNT = Convert.ToDecimal(1);
                            }
                            int DELIVERDLOCATenentID = Convert.ToInt32(0);
                            int QUANTITYDELIVERD = Convert.ToInt32(0);
                            decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                            string ACCOUNTID = "1".ToString();
                            string Remark = "Data Insert formDtext".ToString();
                            int TransNo1 = Convert.ToInt32(0);
                            Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);

                        }
                    }
                    string REFERENCE = txtrefreshno.Text;
                    // int TenentID = TID;
                    // int MYTRANSID = TransNo;
                    int ToTenentID = objProFile.ToTenantID; 
                    int TOLOCATIONID = LID;
                    int transid = 21;
                    int transsubid = 221;
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = "Transfer Notes - Out";// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    //  string MYSYSNAME = "SAL".ToString();
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
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
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;

                        Swit1 = "1";
                    }
                    else
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    } string BillNo = "";
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    var List1 = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    foreach (Database.ICOVERHEADCOST item in List1)
                    {

                        DB.ICOVERHEADCOSTs.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    for (int j = 0; j < Repeater1.Items.Count; j++)
                    {
                        DropDownList ddlOHType = (DropDownList)Repeater1.Items[j].FindControl("ddlOHType");
                        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[j].FindControl("txtOHAmntLocal");
                        TextBox txtOHNote = (TextBox)Repeater1.Items[j].FindControl("txtOHNote");
                        TextBox txtOHAMT = (TextBox)Repeater1.Items[j].FindControl("txtOHAMT");
                        if (txtOHAmntLocal.Text == "")
                        {

                        }
                        else
                        {
                            int MYID = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                            int OVERHEADCOSTID = Convert.ToInt32(ddlOHType.SelectedValue);
                            decimal TOTCOST = 0;
                            decimal OLDCOST = 0;

                            if (txtOHCostHD.Text != "")
                            {
                                TOTCOST = Convert.ToDecimal(txtOHCostHD.Text);
                                OLDCOST = Convert.ToDecimal(txtOHCostHD.Text);
                            }
                            decimal OTHERCOST = 0;
                            decimal UNITCOST = 0;
                            decimal NEWCOST = Convert.ToDecimal(txtOHAmntLocal.Text);
                            int TOTQTY2 = Convert.ToInt32(lblqtytotl.Text);
                            decimal COMPANY_SEQUENCE = UID;
                            int ICT_COMPANYID = UID;
                            //  int COMPANYID = UID;
                            string Note = txtOHNote.Text;
                            Classes.EcommAdminClass.insertICOVERHEADCOST(TenentID, MYTRANSID, MYID, OVERHEADCOSTID, OLDCOST, NEWCOST, TOTQTY2, TOTCOST, OTHERCOST, UNITCOST, CRUP_ID, COMPANY_SEQUENCE, ICT_COMPANYID, COMPANYID, Note);

                        }
                    }
                    var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    foreach (Database.ICTRPayTerms_HD item in List2)
                    {

                        DB.ICTRPayTerms_HD.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text == "")
                        {

                        }
                        else
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }
                    if (Payment == Total)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text == "" && txtammunt.Text == "")
                            {

                            }
                            else
                            {


                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();
                                ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                objICTRPayTerms_HD.TenentID = TID;
                                objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                                objICTRPayTerms_HD.AccountID = 0;
                                objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                DB.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = "Total Amount not Macth";
                        return;
                    }
                    readMode();
                    BINDHDDATA();
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
                    int ToTenentID = objProFile.ToTenantID; 
                    int TOLOCATIONID = LID;
                    int transid = 21;
                    int transsubid = 221;
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = "Transfer Notes - Out";// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    string MYSYSNAME = "SAL".ToString();
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
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
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;

                        Swit1 = "1";
                    }
                    else
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    } string BillNo = "";
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
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
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(lblcr.Text);
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

                        TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
                        Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
                        Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
                        Boolean Perishable = Convert.ToBoolean(obj.Perishable);
                        Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
                        Boolean Serialized = Convert.ToBoolean(obj.Serialized);
                        Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);
                        int PQTY = Convert.ToInt32(obj.QTYINHAND);
                        int Total123 = PQTY - QUANTITY;
                        obj.QTYINHAND = Total123;
                        DB.SaveChanges();
                        if (MultiBinStore == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiBIN" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //int TenentID = TID;
                                // int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int Bin_ID = listmulticolor[ii].Bin_ID;
                                int UOM1 = listmulticolor[ii].UOM;
                                //int MYTRANSID = TCID;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].OpQty);
                                //    int OpQty = Convert.ToInt32(listmulticolor[ii].OpQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                string BatchNo = listmulticolor[ii].BatchNo; ;
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = Convert.ToInt32(listmulticolor[ii].QtyConsumed);
                                //int QtyReserved = Convert.ToInt32(listmulticolor[ii].QtyReserved);
                                //int MinQty = 0;
                                //int MaxQty = 0;
                                //int LeadTime = 0;
                                // int CRUP_ID = 0;
                                string pagename = "Sales";
                                string Fuctions = "ADD";
                                Classes.EcommAdminClass.insertICIT_BR_BIN(TenentID, MyProdID, period_code, MySysName, Bin_ID, UOM1, LID, BatchNo, MYTRANSID, NewQty, Reference, Active, CRUP_ID, pagename, Fuctions);
                            }
                        }
                        if (Serialized == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Serialize" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //int TenentID = TID;
                                //int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                //string UOM = ddlUOM.SelectedValue;
                                string Serial_Number = listmulticolor[ii].Serial_Number;
                                int MyTransID = Convert.ToInt32(listmulticolor[ii].MYTRANSID);
                                string Active = "Y";
                                string Flag_GRN_GTN = "N";
                                // int CRUP_ID = 0;
                                string pagename = "Sales";
                                Classes.EcommAdminClass.insertICIT_BR_Serialize(TenentID, MyProdID, period_code, MySysName, UOM, Serial_Number, LID, MyTransID, Flag_GRN_GTN, Active, CRUP_ID, pagename);
                            }
                        }
                        if (MultiUOM != Convert.ToBoolean(1))
                        {
                            int UOM1 = Convert.ToInt32(lblUOM.Text);
                            var listICIT_BR = DB.ICIT_BR.Where(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.MySysName == MYSYSNAME && p.UOM == UOM1).ToList();
                            //foreach (Database.ICIT_BR item in listICIT_BR)
                            //{

                            //    DB.ICIT_BR.DeleteObject(item);
                            //    DB.SaveChanges();
                            //}
                            string period_code = OICODID;
                            string MySysName = "IC";

                            decimal UnitCost = UNITPRICE;
                            int NewQty = QUANTITY;
                            //  int OpQty = QUANTITY;
                            string Reference = txtrefreshno.Text;
                            string Active = "Y";
                            //   int OnHand = QUANTITY;
                            //   int QtyOut = QUANTITY;
                            //   int QtyConsumed = QUANTITY;
                            //   int QtyReserved = QUANTITY;
                            //  int MinQty = 0;
                            //   int MaxQty = 0;
                            string Bin_Per = "N";
                            //  int LeadTime = 0;
                            string Fuctions = "ADD";
                            string pagename = "Sales";
                            Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        }
                        if (MultiUOM == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "UOM" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                int UOM1 = listmulticolor[ii].UOM;

                                string period_code = listmulticolor[ii].period_code;
                                string MySysName = "IC";
                                decimal UnitCost = UNITPRICE;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                // int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                //  int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //   int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //    int QtyConsumed = 0;
                                //    int QtyReserved = 0;
                                //    int MinQty = 0;
                                //    int MaxQty = 0;
                                string Bin_Per = "N";
                                //    int LeadTime = 0;
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }
                        }

                        if (MultiColor == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiColor" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //string Reference = "";
                                // int TenentID = TID;
                                // int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int UO1M = listmulticolor[ii].UOM;
                                int SIZECODE = 999999998;
                                int COLORID = listmulticolor[ii].COLORID;
                                // int MYTRANSID = TCID;
                                int Nweqty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //  int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                //int OnHand_Q = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut_Q = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = Convert.ToInt32(listmulticolor[ii].QtyConsumed);
                                //int QtyReserved = Convert.ToInt32(listmulticolor[ii].QtyReserved);
                                //int MinQty = Convert.ToInt32(listmulticolor[ii].MinQty);
                                //int MaxQty = Convert.ToInt32(listmulticolor[ii].MaxQty);
                                //int LeadTime = Convert.ToInt32(listmulticolor[ii].LeadTime);
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                // int CRUP_ID = 0;
                                Classes.EcommAdminClass.insertICIT_BR_SIZECOLOR(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, Nweqty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }

                        }
                        if (MultiSize == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiSize" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //string Reference = "";
                                // int TenentID = TID;
                                // int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int UO1M = listmulticolor[ii].UOM;
                                int SIZECODE = listmulticolor[ii].SIZECODE;
                                int COLORID = 999999998;
                                // int MYTRANSID = TCID;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //  int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                //int OnHand_Q = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut_Q = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int OnHand = Convert.ToInt32(listmulticolor[ii].OnHand);
                                //int QtyOut = Convert.ToInt32(listmulticolor[ii].QtyOut);
                                //int QtyConsumed = Convert.ToInt32(listmulticolor[ii].QtyConsumed);
                                //int QtyReserved = Convert.ToInt32(listmulticolor[ii].QtyReserved);
                                //int MinQty = Convert.ToInt32(listmulticolor[ii].MinQty);
                                //int MaxQty = Convert.ToInt32(listmulticolor[ii].MaxQty);
                                //int LeadTime = Convert.ToInt32(listmulticolor[ii].LeadTime);
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                // int CRUP_ID = 0;
                                Classes.EcommAdminClass.insertICIT_BR_SIZE(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }

                        }
                        if (Perishable == Convert.ToBoolean(1))
                        {
                            var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Perishable" && p.MyProdID == MyProdID).ToList();
                            for (int ii = 0; ii < listmulticolor.Count; ii++)
                            {
                                //int TenentID = TID;
                                //int MyProdID = PID;
                                string period_code = OICODID;
                                string MySysName = "SAL";
                                int UOM1 = listmulticolor[ii].UOM;
                                string BatchNo = listmulticolor[ii].BatchNo;
                                // int MYTRANSID = TCID;
                                int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                                //int OnHand = 0;
                                DateTime ProdDate = Convert.ToDateTime(listmulticolor[ii].ProdDate);
                                DateTime ExpiryDate = Convert.ToDateTime(listmulticolor[ii].ExpiryDate);
                                DateTime LeadDays2Destroy = Convert.ToDateTime(listmulticolor[ii].LeadDays2Destroy);
                                string Reference = listmulticolor[ii].Reference;
                                string Active = "Y";
                                string Fuctions = "ADD";
                                string pagename = "Sales";
                                //int CRUP_ID = 0;
                                Classes.EcommAdminClass.insertICIT_BR_Perishable(TenentID, MyProdID, period_code, MySysName, UOM1, BatchNo, LID, MYTRANSID, NewQty, ProdDate, ExpiryDate, LeadDays2Destroy, Reference, Active, CRUP_ID, Fuctions, pagename);
                            }

                        }
                        if (ddlLocalForeign.SelectedValue == "8867")
                        {
                            int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                            int MyID = DXMYID;

                            int MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID && p.TenentID == TID).Max(p => p.MyRunningSerial) + 1) : 1;
                            decimal CURRENTCONVRATE = Convert.ToDecimal(0);
                            string CURRENCY = "";
                            decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                            if (ddlCurrency.SelectedValue == "0")
                            { }
                            else
                            {
                                CURRENTCONVRATE = Convert.ToDecimal(1);
                                CURRENCY = ddlCurrency.SelectedItem.Text;
                                OTHERCURAMOUNT = Convert.ToDecimal(1);
                            }
                            int DELIVERDLOCATenentID = Convert.ToInt32(0);
                            int QUANTITYDELIVERD = Convert.ToInt32(0);
                            decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                            string ACCOUNTID = "1".ToString();
                            string Remark = "Data Insert formDtext".ToString();
                            int TransNo1 = Convert.ToInt32(0);
                            Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                        }

                    }

                    for (int j = 0; j < Repeater1.Items.Count; j++)
                    {
                        DropDownList ddlOHType = (DropDownList)Repeater1.Items[j].FindControl("ddlOHType");
                        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[j].FindControl("txtOHAmntLocal");
                        TextBox txtOHNote = (TextBox)Repeater1.Items[j].FindControl("txtOHNote");
                        TextBox txtOHAMT = (TextBox)Repeater1.Items[j].FindControl("txtOHAMT");
                        if (txtOHAmntLocal.Text == "")
                        {

                        }
                        else
                        {
                            //ICOVERHEADCOST objICOVERHEADCOST = new ICOVERHEADCOST();
                            //objICOVERHEADCOST.TenentID = TID;
                            //objICOVERHEADCOST.MYTRANSID = TransNo;
                            int MYID = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID).Max(p => p.MYID) + 1) : 1;
                            int OVERHEADCOSTID = Convert.ToInt32(ddlOHType.SelectedValue);
                            decimal TOTCOST = 0;
                            decimal OLDCOST = 0;

                            if (txtOHCostHD.Text != "")
                            {
                                TOTCOST = Convert.ToDecimal(txtOHCostHD.Text);
                                OLDCOST = Convert.ToDecimal(txtOHCostHD.Text);
                            }
                            decimal OTHERCOST = 0;
                            decimal UNITCOST = 0;
                            decimal NEWCOST = Convert.ToDecimal(txtOHAmntLocal.Text);
                            int TOTQTY = Convert.ToInt32(lblqtytotl.Text);
                            decimal COMPANY_SEQUENCE = UID;
                            int ICT_COMPANYID = UID;
                            //  int COMPANYID = UID;
                            string Note = txtOHNote.Text;
                            Classes.EcommAdminClass.insertICOVERHEADCOST(TenentID, MYTRANSID, MYID, OVERHEADCOSTID, OLDCOST, NEWCOST, TOTQTY, TOTCOST, OTHERCOST, UNITCOST, CRUP_ID, COMPANY_SEQUENCE, ICT_COMPANYID, COMPANYID, Note);
                        }
                    }
                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text == "")
                        {

                        }
                        else
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }
                    if (Payment == Total)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text == "" && txtammunt.Text == "")
                            {

                            }
                            else
                            {


                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();
                                ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                objICTRPayTerms_HD.TenentID = TID;
                                objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                                objICTRPayTerms_HD.AccountID = 0;
                                objICTRPayTerms_HD.Amount = Convert.ToDecimal(txtammunt.Text);
                                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                DB.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = "Total Amount not Macth";
                        return;
                    }

                    BINDHDDATA();
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
                    //    string SupplierName = txtLocationSearch.Text;
                    //    string ADDACTIvity = "Add";
                    //    string DAteTime = TRANSDATE.ToShortDateString();
                    //    string Discription = TranType + " , " + TransNo + " , " + DAteTime + " , " + SupplierName + " , " + NOTES.ToString();
                    //    InsertCRMMainActivity(COMPID, TransNo, Status, ADDACTIvity, TranType, Discription);
                    //}
                }





                scope.Complete(); //  To commit.
                Response.Redirect("PrintMDSF.aspx?Tranjestion=" + TransNo);
            }

        }
        public void InsertCRMMainActivity(int COMPID1, int TransNo, string Status, string ADDACTIvity, string CamName, string Description)
        {
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
            int MDUID = 0;
            if (Session["SiteModuleID"] != null)
            {
                MDUID = Convert.ToInt32(Session["SiteModuleID"]);
            }
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
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
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
            hidUPriceLocal.Value = hidTotalCurrencyForeign.Value = hidTotalCurrencyLocal.Value = "";
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
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");
                DropDownList ddlOHType = (DropDownList)e.Item.FindControl("ddlOHType");
                Classes.EcommAdminClass.getdropdown(ddlOHType, TID, "", "", "", "ICEXTRACOST");

                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    ddlOHType.SelectedValue = ID.ToString();
                }

            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if
                (HiddenField3.Value == "0" || HiddenField3.Value == "")
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Select Customer";
            }
            else
            {
                int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                Classes.EcommAdminClass.BindProdu(PID, ddlUOM, txtDescription, txtserchProduct, TID);
                decimal PRiesh = Convert.ToDecimal(DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).price);
                int DDLFID = Convert.ToInt32(ddlLocalForeign.SelectedValue);
                if (DDLFID == 8866)
                    txtUPriceLocal.Text = PRiesh.ToString();
                else
                    txtUPriceForeign.Text = PRiesh.ToString();
                txtQuantity.Text = "";
                lblmultiuom.Visible = false;
                lblmulticolor.Visible = false;
                lblmultisize.Visible = false;
                lblmultiperishable.Visible = false;
                lblmultibinstore.Visible = false;
                lblmultiserialize.Visible = false;
            }

        }
        public void BindProduct()
        {
            var result2 = (from Module in DB.TBLPRODUCTs
                           join
                               pm in DB.ICIT_BR on Module.MYPRODID equals pm.MyProdID
                           where Module.TenentID == TID && pm.TenentID == TID && pm.LocationID == LID && Module.ACTIVE == "1"
                           select new { Module.MYPRODID, Module.ProdName1 }).ToList();

            ddlProduct.DataSource = result2;
            ddlProduct.DataValueField = "MYPRODID";
            ddlProduct.DataTextField = "ProdName1";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Product--", "0"));
        }
        List<Database.ICTR_DT> TempListICTR_DT = new List<Database.ICTR_DT>();
        protected void btnAddDT_Click(object sender, EventArgs e)
        {
            decimal TotalAmut = 0;
            decimal LUINTP = 0;
            bool fleg = true;
            int MYIDCUNT = DB.ICTR_DT.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID).Max(p => p.MYID) + 1) : 1; ;
            if (ViewState["TempListICTR_DT"] == null)
            {

            }
            else
            {
                TempListICTR_DT = (List<Database.ICTR_DT>)ViewState["TempListICTR_DT"];
                fleg = false;
                MYIDCUNT += MYIDCUNT + 1;
            }
            if (ViewState["DTTRCTIONID"] != null && ViewState["DTMYID"] != null)
            {
                int str1 = Convert.ToInt32(ViewState["DTTRCTIONID"]);
                int str2 = Convert.ToInt32(ViewState["DTMYID"]);
                TempListICTR_DT = ((List<Database.ICTR_DT>)ViewState["TempListICTR_DT"]).Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).ToList();
                var List = DB.ICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).ToList();
                if (List.Count() > 0)
                {
                    foreach (Database.ICTR_DT item in List)
                    {
                        DB.ICTR_DT.DeleteObject(item);
                        DB.SaveChanges();
                    }
                }
                for (int i = TempListICTR_DT.Count - 1; i >= 0; i--)
                {
                    TempListICTR_DT.RemoveAt(i);
                }
                BindDT(str1);
                ViewState["DTTRCTIONID"] = null;
                ViewState["DTMYID"] = null;
            }

            Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();
            objICTR_DT.MYTRANSID = Convert.ToInt32(txtTraNoHD.Text);
            objICTR_DT.MYID = MYIDCUNT;
            objICTR_DT.MyProdID = Convert.ToInt32(ddlProduct.SelectedValue);
            objICTR_DT.DESCRIPTION = txtDescription.Text;
            objICTR_DT.UOM = ddlUOM.SelectedValue;
            objICTR_DT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
            if (ddlLocalForeign.SelectedValue == "8867")
            {
                LUINTP = Convert.ToDecimal(txtUPriceForeign.Text);
                TotalAmut = Convert.ToDecimal(hidTotalCurrencyForeign.Value);
                objICTR_DT.UNITPRICE = LUINTP;
                objICTR_DT.AMOUNT = TotalAmut;
            }
            else
            {
                LUINTP = Convert.ToDecimal(txtUPriceLocal.Text);
                TotalAmut = Convert.ToDecimal(hidTotalCurrencyLocal.Value);
                objICTR_DT.UNITPRICE = LUINTP;
                objICTR_DT.AMOUNT = TotalAmut;
            }
            string str = "";
            str = txtDiscount.Text.Replace('%', ' ');
            if (txtOHCostHD.Text != "")
                objICTR_DT.OVERHEADAMOUNT = Convert.ToDecimal(txtOHCostHD.Text);
            decimal TEX = Convert.ToDecimal(txtTax.Text);
            decimal TexAmunt = 0;
            decimal DICUNTOTAL = 0;
            decimal Total = 0;
            decimal Priesh = 0;
            decimal DICUNT = 0;
            decimal DicuntLest = 0;
            decimal qty = Convert.ToDecimal(txtQuantity.Text);

            if (txtDiscount.Text.Contains("%"))
            {
                if (txtUPriceForeign.Text != "" && txtUPriceForeign.Text != "0.00")
                {
                    Priesh = Convert.ToDecimal(txtUPriceForeign.Text);
                    Total = qty * Priesh;
                    DICUNT = Convert.ToInt32(str);
                    DICUNTOTAL = Total - (Total * (DICUNT / 100));
                    DicuntLest = Total - DICUNTOTAL;
                }
                else
                {
                    Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                    Total = qty * Priesh;
                    DICUNT = Convert.ToInt32(str);
                    DICUNTOTAL = Total - (Total * (DICUNT / 100));
                    DicuntLest = Total - DICUNTOTAL;
                }
                objICTR_DT.DISPER = Convert.ToDecimal(str);
                objICTR_DT.DISAMT = Convert.ToDecimal(DicuntLest);

                TexAmunt = DICUNTOTAL * TEX / 100;
                objICTR_DT.TAXAMT = TexAmunt;

            }
            else
            {
                if (txtUPriceForeign.Text != "" && txtUPriceForeign.Text != "0.00")
                {
                    Priesh = Convert.ToDecimal(txtUPriceForeign.Text);
                    Total = qty * Priesh;
                    DICUNT = Convert.ToInt32(str);
                    DICUNTOTAL = Total - DICUNT;
                    DicuntLest = Total - DICUNTOTAL;
                }
                else
                {
                    Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                    Total = qty * Priesh;
                    DICUNT = Convert.ToInt32(str);
                    DICUNTOTAL = Total - DICUNT;
                    DicuntLest = Total - DICUNTOTAL;
                }
                objICTR_DT.DISPER = Convert.ToDecimal(0.000);
                objICTR_DT.DISAMT = Convert.ToDecimal(str);
                decimal DicuntAmut = Convert.ToDecimal(str);
                DicuntLest = Total - DicuntAmut;
                TexAmunt = DicuntLest * TEX / 100;
                objICTR_DT.TAXAMT = TexAmunt;
            }
            objICTR_DT.TAXPER = TEX;
            TempListICTR_DT.Add(objICTR_DT);
            ViewState["TempListICTR_DT"] = TempListICTR_DT;
            Repeater2.DataSource = TempListICTR_DT;
            Repeater2.DataBind();

            clearItemsTab();
            btnAddItemsIn.Visible = true;
            btnAddDT.Visible = false;
            //btnAddDT.Text = "Add Items";
            int QTY1 = Convert.ToInt32(objICTR_DT.QUANTITY);
            decimal UNP1 = Convert.ToDecimal(objICTR_DT.UNITPRICE);
            decimal TXP1 = Convert.ToDecimal(objICTR_DT.TAXPER);
            decimal GTL1 = Convert.ToDecimal(objICTR_DT.AMOUNT);
            decimal UInPictTotal = Convert.ToDecimal(lblcr.Text);
            decimal FInel = UInPictTotal / GTL1;
            if (fleg == true)
            {
                lblqtytotl.Text = QTY1.ToString();
                lblUNPtotl.Text = UNP1.ToString();
                lblTextotl.Text = TXP1.ToString();
                lblTotatotl.Text = GTL1.ToString();
                txttotxl.Text = GTL1.ToString();
                txtuintOhCost.Text = "@ " + FInel.ToString("N3");
            }
            else
            {
                int QTY = Convert.ToInt32(lblqtytotl.Text);
                decimal UNP = Convert.ToDecimal(lblUNPtotl.Text);
                decimal TXP = Convert.ToDecimal(lblTextotl.Text);
                decimal GTL = Convert.ToDecimal(lblTotatotl.Text);
                int QTYTOtal = QTY + QTY1;
                decimal UPTOTAL = UNP + UNP1;
                decimal TAXTOTL = TXP + TXP1;
                decimal GTLTOL = GTL + GTL1;
                decimal FinelUni = UInPictTotal / GTLTOL;
                lblqtytotl.Text = QTYTOtal.ToString();
                lblUNPtotl.Text = UPTOTAL.ToString();
                lblTextotl.Text = TAXTOTL.ToString();
                lblTotatotl.Text = GTLTOL.ToString();
                txttotxl.Text = GTLTOL.ToString();
                txtuintOhCost.Text = "@ " + FinelUni.ToString("N3");
            }

            ListItems.Visible = true;
            panelRed.Visible = false;


            string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);

        }
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
                BINDHDDATA();
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
                Response.Redirect("PrintMDSF.aspx?Tranjestion=" + MYTID);

            }
        }
        public void EditSalesh(int ID)
        {
            ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID);
            ddlLocalForeign.SelectedValue = OBJICTR_HD.LF == "L" ? "8866" : "8867";
            int CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
            TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
            txtLocationSearch.Text = onj.COMPNAME1;
            HiddenField3.Value = CID.ToString();
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
            var ListOvercost = DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == ID && p.TenentID == TID).ToList();
            if (ListOvercost.Count() > 0)
            {
                ViewState["ListOverCost"] = ListOvercost;
                Repeater1.DataSource = (List<ICOVERHEADCOST>)ViewState["ListOverCost"];
                Repeater1.DataBind();
                ViewState["TempList"] = ListOvercost;
            }

            var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == ID && p.TenentID == TID).ToList();
            if (listcost.Count() > 0)
            {
                ViewState["TempEco_ICCATEGORY"] = listcost;
                Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                Repeater3.DataBind();
            }
            else
            {
                Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                Repeater3.DataBind();
            }
            decimal Sum = Convert.ToDecimal(ListOvercost.Sum(p => p.NEWCOST));
            txtOHCostHD.Text = Sum.ToString();
            lblcr.Text = Sum.ToString();

            btnAddItemsIn.Visible = true;
            btnAddDT.Visible = false;
            BindDT(ID);

            ListItems.Visible = true;
            panelRed.Visible = false;
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
        public void BindDT(int ID)
        {
            var List = DB.ICTR_DT.Where(p => p.MYTRANSID == ID && p.ACTIVE == true && p.TenentID == TID).ToList();

            Repeater2.DataSource = List;
            Repeater2.DataBind();
            decimal UInPictTotal = Convert.ToDecimal(lblcr.Text);
            decimal TottalSum = Convert.ToDecimal(List.Sum(p => p.AMOUNT));
            txttotxl.Text = TottalSum.ToString();
            int QTUtotal = Convert.ToInt32(List.Sum(p => p.QUANTITY));
            lblqtytotl.Text = QTUtotal.ToString();
            decimal UNPTOL = Convert.ToDecimal(List.Sum(p => p.UNITPRICE));
            lblUNPtotl.Text = UNPTOL.ToString();
            decimal TaxTol = Convert.ToDecimal(List.Sum(p => p.TAXPER));
            lblTextotl.Text = TaxTol.ToString();
            lblTotatotl.Text = TottalSum.ToString();
            if (UInPictTotal != 0 && TottalSum != 0)
            {
                decimal FInelOC = UInPictTotal / TottalSum;
                txtuintOhCost.Text = "@ " + FInelOC.ToString("N3");
            }

            ViewState["TempListICTR_DT"] = List;
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
                    ddlProduct.SelectedValue = objICTR_DT.MyProdID.ToString();
                    txtDescription.Text = objICTR_DT.DESCRIPTION.ToString();
                    ddlUOM.SelectedValue = objICTR_DT.UOM.ToString();
                    txtQuantity.Text = objICTR_DT.QUANTITY.ToString();
                    txtUPriceForeign.Text = objICTR_DT.UNITPRICE.ToString();
                    txtUPriceLocal.Text = objICTR_DT.UNITPRICE.ToString();
                    txtDiscount.Text = objICTR_DT.DISAMT.ToString();
                    txtTax.Text = objICTR_DT.TAXPER.ToString();
                    txtTotalCurrencyForeign.Text = objICTR_DT.AMOUNT.ToString();
                    txtTotalCurrencyLocal.Text = objICTR_DT.AMOUNT.ToString();
                    ViewState["DTTRCTIONID"] = str1;
                    ViewState["DTMYID"] = str2;
                    int PID = Convert.ToInt32(ddlProduct.SelectedValue);

                    TBLPRODUCT objTBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                    txtserchProduct.Text = objTBLPRODUCT.BarCode;
                    Boolean Perishable = Convert.ToBoolean(objTBLPRODUCT.Perishable);
                    Boolean MultiUOM = Convert.ToBoolean(objTBLPRODUCT.MultiUOM);
                    Boolean MultiColor = Convert.ToBoolean(objTBLPRODUCT.MultiColor);
                    Boolean MultiSize = Convert.ToBoolean(objTBLPRODUCT.MultiSize);
                    Boolean MultiBinStore = Convert.ToBoolean(objTBLPRODUCT.MultiBinStore);
                    Boolean Serialized = Convert.ToBoolean(objTBLPRODUCT.Serialized);
                    if (MultiUOM == Convert.ToBoolean(1))
                    {
                        lblmultiuom.Visible = true;
                    }
                    else
                        lblmultiuom.Visible = false;
                    if (Perishable == Convert.ToBoolean(1))
                    {
                        lblmultiperishable.Visible = true;

                    }
                    else
                        lblmultiperishable.Visible = false;
                    if (MultiColor == Convert.ToBoolean(1))
                    {
                        lblmulticolor.Visible = true;

                    }
                    else
                        lblmulticolor.Visible = false;
                    if (MultiSize == Convert.ToBoolean(1))
                    {
                        lblmultisize.Visible = true;
                    }
                    else
                        lblmultisize.Visible = false;
                    if (MultiBinStore == Convert.ToBoolean(1))
                    {
                        lblmultibinstore.Visible = true;
                        // ViewState["MultiBinStore"] = "MultiBinStore";

                    }
                    else
                        lblmultibinstore.Visible = false;
                    if (Serialized == Convert.ToBoolean(1))
                    {
                        lblmultiserialize.Visible = true;
                    }
                    else
                        lblmultiserialize.Visible = false;
                }
                ViewState["AddnewItems"] = "Yes";
                btndiscartitems.Text = "Cansel";
                ListItems.Visible = false;
                panelRed.Visible = true;
                //string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
                //ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
                btnAddItemsIn.Visible = false;
                btnAddDT.Visible = true;
                btnAddDT.Text = "Update";
            }
        }
        public void clearItemsTab()
        {
            try
            {
                ddlProduct.SelectedValue = ddlUOM.SelectedValue = "0";
                txtDescription.Text = txtQuantity.Text = txtUPriceForeign.Text = txtUPriceLocal.Text = txtDiscount.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
                hidUPriceLocal.Value = hidTotalCurrencyForeign.Value = hidTotalCurrencyLocal.Value = hidUOMId.Value = hidUOMText.Value = "";

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
                ddlLocalForeign.SelectedValue = ddlProjectNo.SelectedValue = ddlCurrency.SelectedValue = ddlCrmAct.SelectedValue = "0";
                txtLocationSearch.Text = txtOrderDate.Text = txtBatchNo.Text = txtTraNoHD.Text = txtNoteHD.Text = txtrefreshno.Text = txtOHCostHD.Text = txttotxl.Text = txtuintOhCost.Text = "";
                btnSubmit.Visible = btnConfirmOrder.Visible = true;
            }
            catch (System.Exception ex)
            {
                // ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        public void clenoverhedcost()
        {
            ICOVERHEADCOST objEco_ICEXTRACOST = new ICOVERHEADCOST();
            TempList.Add(objEco_ICEXTRACOST);
            ViewState["TempList"] = TempList;
            Repeater1.DataSource = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
            Repeater1.DataBind();
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                txtOHAmntLocal.Text = txtOHNote.Text = txtOHAMT.Text = "";
                ddlOHType.SelectedIndex = 0;
            }
            lblcr.Text = "0";
            lblqtytotl.Text = lblUNPtotl.Text = lblTextotl.Text = lblTotatotl.Text = "0";
        }
        protected void listBin_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList dropBacthCode = (DropDownList)e.Item.FindControl("dropBacthCode");
            Classes.EcommAdminClass.getdropdown(dropBacthCode, TID, "", "", "", "TBLBIN");
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = Pidalcode(TACtionDate).ToString();
            ViewState["SaveList1"] = null;
            if (ddlProduct.SelectedValue == "0" && ddlProduct.SelectedValue == "")
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Select Product";
            }
            else
            {
                int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                TBLPRODUCT objTBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                int QTY = Convert.ToInt32(objTBLPRODUCT.QTYINHAND);
                int TexQty = Convert.ToInt32(txtQuantity.Text);
                if (QTY < TexQty)
                {
                    if (DB.tblsetupsaleshes.Single(p => p.TenentID == TID).AllowMinusQty == false)
                    {
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = "The available Qty is not enough; to allow Sales in Minus contact Administration";
                        return;
                    }
                }
                //   Classes.EcommAdminClass.BindMultiMultiUOM(PID, lidtUom, lblmultiuom);
                //Classes.EcommAdminClass.BindPerishable(PID, LinkButton5);
                //  Classes.EcommAdminClass.BindMultiMultiColor(PID, listmulticoler, lblmulticolor);
                //  Classes.EcommAdminClass.BindMultiMultiSize(PID, listSize, lblmultisize);

                //  Classes.EcommAdminClass.BindMultiSerializedInvoice(TID,PID,LID,OICODID, listSerial, lblmultiserialize, txtQuantity);

                Boolean MultiBinStore = Convert.ToBoolean(objTBLPRODUCT.MultiBinStore);
                Boolean Perishable = Convert.ToBoolean(objTBLPRODUCT.Perishable);
                Boolean Serialized = Convert.ToBoolean(objTBLPRODUCT.Serialized);
                Boolean MultiUOM = Convert.ToBoolean(objTBLPRODUCT.MultiUOM);
                Boolean MultiColor = Convert.ToBoolean(objTBLPRODUCT.MultiColor);
                Boolean MultiSize = Convert.ToBoolean(objTBLPRODUCT.MultiSize);
                if (MultiSize == Convert.ToBoolean(1))
                {
                    lblmultisize.Visible = true;
                    var Listserl = DB.ICIT_BR_SIZECOLOR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.COLORID == 999999998 && p.OnHand != 0).ToList();
                    //   List<Tbl_Multi_Color_Size_Mst> List = Classes.EcommAdminClass.getDataTbl_Multi_Color_Size_Mst().Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiSize" && p.RecValue != "All Size").ToList();
                    listSize.DataSource = Listserl;
                    listSize.DataBind();
                }
                else
                {
                    lblmultisize.Visible = false;
                }
                if (MultiColor == Convert.ToBoolean(1))
                {
                    lblmulticolor.Visible = true;
                    var Listserl = DB.ICIT_BR_SIZECOLOR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.SIZECODE == 999999998 && p.OnHand != 0).ToList();
                    //  List<ICIT_BR_SIZECOLOR> List = Classes.EcommAdminClass.getDataTbl_Multi_Color_Size_Mst().Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiColor" && p.RecValue != "All Colors").ToList();
                    listmulticoler.DataSource = Listserl;
                    listmulticoler.DataBind();
                }
                else
                {
                    lblmulticolor.Visible = false;
                }
                if (MultiUOM == Convert.ToBoolean(1))
                {
                    lblmultiuom.Visible = true;
                    var Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    //  List<Tbl_Multi_Color_Size_Mst> List = Classes.EcommAdminClass.getDataTbl_Multi_Color_Size_Mst().Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiUOM" && p.RecValue != "All Colors").ToList();
                    lidtUom.DataSource = Listserl;
                    lidtUom.DataBind();

                }
                else
                {
                    lblmultiuom.Visible = false;
                }
                if (Serialized == Convert.ToBoolean(1))
                {
                    lblmultiserialize.Visible = true;
                    if (txtQuantity.Text == "0")
                    { }
                    else
                    {
                        var Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.Active == "Y").ToList();
                        ViewState["ListofSerlNumber"] = Listserl;
                        listSerial.DataSource = Listserl;
                        listSerial.DataBind();
                        //  ViewState["ListofSerlNumber"] = Listserl;
                        // ViewState["Serialized"] = "Serialized";
                    }
                }
                else
                {
                    lblmultiserialize.Visible = false;
                }
                if (MultiBinStore == Convert.ToBoolean(1))
                {
                    lblmultibinstore.Visible = true;
                    var Listserl = DB.ICIT_BR_BIN.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    //ICIT_BR_BIN List = new ICIT_BR_BIN();
                    //ListICIT_BR_BIN.Add(List);
                    listBin.DataSource = Listserl;
                    listBin.DataBind();
                    ViewState["ListICIT_BR_BIN"] = Listserl;
                }
                else
                {
                    lblmultibinstore.Visible = false;
                }
                if (Perishable == Convert.ToBoolean(1))
                {
                    lblmultiperishable.Visible = true;
                    var Listserl = DB.ICIT_BR_Perishable.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    //ICIT_BR_Perishable List = new ICIT_BR_Perishable();
                    //ListICIT_BR_Perishable.Add(List);
                    listperishibal.DataSource = Listserl;
                    listperishibal.DataBind();
                    ViewState["ListICIT_BR_Perishable"] = Listserl;
                }
                else
                {
                    lblmultiperishable.Visible = false;
                }
                lbltotalqty.Text = lbl1.Text = lbl2.Text = lbl3.Text = lbl4.Text = lbl5.Text = "   Total Qty" + TexQty.ToString();
            }

            txtQuantity.Focus();

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int QTY = Convert.ToInt32(txtQuantity.Text);
            int Total = 0;
            bool UOMTata = true;
            for (int i = 0; i < lidtUom.Items.Count; i++)
            {
                TextBox txtuomQty = (TextBox)lidtUom.Items[i].FindControl("txtuomQty");
                if (txtuomQty.Text != "" && txtuomQty.Text != "0")
                {
                    int TXT = Convert.ToInt32(txtuomQty.Text);
                    Total = Total + TXT;
                }
            }
            if (QTY == Total)
            {
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                for (int i = 0; i < lidtUom.Items.Count; i++)
                {
                    Label lbltidser = (Label)lidtUom.Items[i].FindControl("lbltidser");
                    Label lblpidsril = (Label)lidtUom.Items[i].FindControl("lblpidsril");
                    Label lblpdcodeser = (Label)lidtUom.Items[i].FindControl("lblpdcodeser");
                    Label lblmysysser = (Label)lidtUom.Items[i].FindControl("lblmysysser");
                    Label lbluomser = (Label)lidtUom.Items[i].FindControl("lbluomser");
                    Label lbllocaser = (Label)lidtUom.Items[i].FindControl("lbllocaser");
                    //Label LblColoername = (Label)lidtUom.Items[i].FindControl("LblColoername");
                    //Label LblMyid = (Label)lidtUom.Items[i].FindControl("LblMyid");
                    //Label lblID = (Label)lidtUom.Items[i].FindControl("lblID");
                    TextBox txtuomQty = (TextBox)lidtUom.Items[i].FindControl("txtuomQty");
                    if (txtuomQty.Text != "" && txtuomQty.Text != "0")
                    {
                        int PID = Convert.ToInt32(lblpidsril.Text);
                        int TCID = Convert.ToInt32(txtTraNoHD.Text);
                        int UomID = Convert.ToInt32(lbluomser.Text);
                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = UomID;
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        int BinID = 999999998;
                        string BatchNo = "999999998";
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtuomQty.Text);

                        string Reference = txtrefreshno.Text;
                        string RecodName = "UOM";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = txtDescription.Text;
                        string UOMNAME = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == UomID && p.TenentID == TID).RecValue;
                        string UOMQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (UOMTata == true)
                        {
                            MainDecrption = Discpition + "\n" + " Uom : " + UOMNAME + " : " + UOMQTY;
                            UOMTata = false;
                        }

                        else
                            MainDecrption = Discpition + " , " + UOMNAME + " : " + UOMQTY;
                        txtDescription.Text = MainDecrption;
                    }


                }
                ViewState["MultiUOM"] = null;
            }
            else
            {
                pnlSuccessMsg123.Visible = true;

                lblMsg123.Text = "UOM selected Qty " + Total + " from " + QTY;
            }
        }
        protected void linkMulticoler_Click(object sender, EventArgs e)
        {
            int QTY = Convert.ToInt32(txtQuantity.Text);
            bool MULTIColoer = true;
            int Total = 0;
            for (int i = 0; i < listmulticoler.Items.Count; i++)
            {
                TextBox txtcoloerqty = (TextBox)listmulticoler.Items[i].FindControl("txtcoloerqty");
                if (txtcoloerqty.Text != "" && txtcoloerqty.Text != "0")
                {
                    int TXT = Convert.ToInt32(txtcoloerqty.Text);
                    Total = Total + TXT;
                }
            }
            if (QTY == Total)
            {
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                for (int i = 0; i < listmulticoler.Items.Count; i++)
                {
                    Label LblColoername = (Label)listmulticoler.Items[i].FindControl("LblColoername");
                    Label lblpidsril = (Label)listmulticoler.Items[i].FindControl("lblpidsril");
                    Label lblcolerid = (Label)listmulticoler.Items[i].FindControl("lblcolerid");
                    TextBox txtcoloerqty = (TextBox)listmulticoler.Items[i].FindControl("txtcoloerqty");
                    if (txtcoloerqty.Text != "" && txtcoloerqty.Text != "0")
                    {
                        int PID = Convert.ToInt32(lblpidsril.Text);
                        int TCID = Convert.ToInt32(txtTraNoHD.Text);
                        int COLID = Convert.ToInt32(lblcolerid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                        int SIZECODE = 999999998;
                        int COLORID = COLID;
                        int BinID = 999999998;
                        string BatchNo = "999999998";
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtcoloerqty.Text);

                        string Reference = txtrefreshno.Text;
                        string RecodName = "MultiColor";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = txtDescription.Text;
                        string ColerName = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == COLID && p.TenentID == TID).RecValue;
                        string ColerNameQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (MULTIColoer == true)
                        {
                            MainDecrption = Discpition + "\n" + " Colors : " + ColerName + " : " + ColerNameQTY;
                            MULTIColoer = false;
                        }
                        else
                            MainDecrption = Discpition + " , " + ColerName + " : " + ColerNameQTY;

                        txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["MultiColor"] = null;
            }
            else
            {
                pnlSuccessMsg123.Visible = true;

                lblMsg123.Text = "Colors selected Qty " + Total + " from " + QTY;
            }

        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            int QTY = Convert.ToInt32(txtQuantity.Text);
            bool Multisize = true;
            int Total = 0;
            for (int i = 0; i < listSize.Items.Count; i++)
            {
                TextBox txtmultisze = (TextBox)listSize.Items[i].FindControl("txtmultisze");
                if (txtmultisze.Text != "" && txtmultisze.Text != "0")
                {
                    int TXT = Convert.ToInt32(txtmultisze.Text);
                    Total = Total + TXT;
                }
            }
            if (QTY == Total)
            {
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                for (int i = 0; i < listSize.Items.Count; i++)
                {
                    Label LblColoername = (Label)listSize.Items[i].FindControl("LblColoername");
                    Label lblpidsril = (Label)listSize.Items[i].FindControl("lblpidsril");
                    Label lblcolerid = (Label)listSize.Items[i].FindControl("lblcolerid");
                    TextBox txtmultisze = (TextBox)listSize.Items[i].FindControl("txtmultisze");
                    if (txtmultisze.Text != "" && txtmultisze.Text != "0")
                    {
                        int PID = Convert.ToInt32(lblpidsril.Text);
                        int TCID = Convert.ToInt32(txtTraNoHD.Text);
                        int COLID = Convert.ToInt32(lblcolerid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                        int SIZECODE = Convert.ToInt32(COLID);
                        int COLORID = 999999998;
                        int BinID = 999999998;
                        string BatchNo = "999999998";
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtmultisze.Text);

                        string Reference = txtrefreshno.Text;
                        string RecodName = "MultiSize";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = txtDescription.Text;
                        string SizeNAME = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == SIZECODE && p.TenentID == TID).RecValue;
                        string SizeQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (Multisize == true)
                        {
                            MainDecrption = Discpition + "\n" + " Size : " + SizeNAME + " : " + SizeQTY;
                            Multisize = false;
                        }
                        else
                            MainDecrption = Discpition + " , " + SizeNAME + " : " + SizeQTY;

                        txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["MultiSize"] = null;
            }
            else
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Multi Size selected Qty " + Total + " from " + QTY;
            }
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            int QTY = Convert.ToInt32(txtQuantity.Text);
            DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = Pidalcode(TACtionDate).ToString();
            int Total = 0;
            string[] Seperate4 = tags_2.Text.Split(',');
            Total = Seperate4.Count();
            if (QTY == Total)
            {
                string Discpition = txtDescription.Text;
                string MainDecrption = tags_2.Text;
                MainDecrption = Discpition + "\n" + "Serialize : " + MainDecrption;
                txtDescription.Text = MainDecrption;

                string[] str = tags_2.Text.Split(',');
                int count5 = 0;
                string Sep5 = "";
                for (int i = 0; i <= str.Count() - 1; i++)
                {
                    int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                    int TCID = Convert.ToInt32(txtTraNoHD.Text);
                    //int COLID = Convert.ToInt32(lblcolerid.Text);

                    int TenentID = TID;
                    int MyProdID = PID;
                    string period_code = OICODID;
                    string MySysName = "SAL";
                    int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                    int SIZECODE = 999999998;
                    int COLORID = 999999998;
                    int BinID = 999999998;
                    string BatchNo = "999999998";
                    string Serialize = str[i];
                    int MYTRANSID = TCID;
                    int LocationID = LID;
                    int NewQty = Convert.ToInt32(1);

                    string Reference = txtrefreshno.Text;
                    string RecodName = "Serialize";
                    DateTime ProdDate = DateTime.Now;
                    DateTime ExpiryDate = DateTime.Now;
                    DateTime LeadDays2Destroy = DateTime.Now;
                    string Active = "D";
                    int CRUP_ID = 999999998;
                    Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                }
            }
            else
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Serialization selected Qty " + Total + " from " + QTY;
            }


            ViewState["Serialized"] = null;
        }
        protected void ddlSupplier_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            int SID = Convert.ToInt32(HiddenField3.Value);
            int SIDUserDItl = Convert.ToInt32(DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COUNTRYID);

            if (CUNTRYID == SIDUserDItl)
            {
                ddlLocalForeign.SelectedValue = "8866";
                ddlCurrency.Enabled = false;
                txtUPriceForeign.Enabled = false;
                txtUPriceLocal.Enabled = true;
                ddlCurrency.SelectedValue = CUNTRYID.ToString();
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                    TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                    TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                    TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                    LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
                    ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = false;
                }
                ButtonAdd.Enabled = buttonUpdate.Enabled = btndiscorohcost.Enabled = false;
            }
            else
            {
                ddlLocalForeign.SelectedValue = "8867";
                ddlCurrency.Enabled = true;
                txtUPriceForeign.Enabled = true;
                txtUPriceLocal.Enabled = false;
                ddlCurrency.SelectedValue = SIDUserDItl.ToString();
            }

        }
        protected void txtLocationSearch_TextChanged(object sender, EventArgs e)
        {
            if (HiddenField3.Value != "")
            {
                int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
                int SID = Convert.ToInt32(HiddenField3.Value);
                int SIDUserDItl = Convert.ToInt32(DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COUNTRYID);

                if (CUNTRYID == SIDUserDItl)
                {
                    ddlLocalForeign.SelectedValue = "8866";
                    ddlCurrency.Enabled = false;
                    txtUPriceForeign.Enabled = false;
                    txtUPriceLocal.Enabled = true;
                    ddlCurrency.SelectedValue = CUNTRYID.ToString();
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                        TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                        TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                        TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                        LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
                        ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = false;
                    }
                    ButtonAdd.Enabled = buttonUpdate.Enabled = btndiscorohcost.Enabled = false;
                }
                else
                {
                    ddlLocalForeign.SelectedValue = "8867";
                    ddlCurrency.Enabled = true;
                    txtUPriceForeign.Enabled = true;
                    txtUPriceLocal.Enabled = false;
                    ddlCurrency.SelectedValue = SIDUserDItl.ToString();
                }

            }

        }
        public string getprodname(int SID)
        {
            string ProductCode = DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).UserProdID;
            return ProductCode;
        }
        public string getsuppername(int ID)
        {
            return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID).COMPNAME1;
        }
        public string getrecodtypename(int ID)
        {
            return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == ID && p.TenentID == TID).RecValue;
        }
        public string getbinname(int BID)
        {
            return DB.TBLBINs.Single(p => p.BIN_ID == BID && p.TenentID == TID).BINDesc1;
        }
        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;
            txtQuantity.Text = txtUPriceForeign.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
            btnAddItemsIn.Visible = false;
            btnAddDT.Visible = true;
            ListItems.Visible = false;
            panelRed.Visible = true;
            ViewState["AddnewItems"] = "Yes";
            btndiscartitems.Text = "Cancel";
            btnAddDT.Text = "Save";
            txtQuantity.Text = "";
            txtserchProduct.Text = "";
            lblmultiuom.Visible = false;
            lblmulticolor.Visible = false;
            lblmultisize.Visible = false;
            lblmultiperishable.Visible = false;
            lblmultibinstore.Visible = false;
            lblmultiserialize.Visible = false;
            Classes.EcommAdminClass.getdropdown(ddlProduct, TID, "", "", "", "TBLPRODUCT");
            string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            bool Fleg = true;          
            DateTime Todate = DateTime.Now;
            Fleg = Classes.EcommAdminClass.BindStockTaking(Todate, TID, panelMsg, lblErreorMsg, Fleg);
            if (Fleg == true)
            {
                WrietMode();
                clearItemsTab();
                clearHDPanel();
                clenoverhedcost();
                int MAXID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                txtTraNoHD.Text = MAXID.ToString();
                ViewState["TempListICTR_DT"] = null;
                txtOrderDate.Text = DateTime.Now.ToString("dd-MMM-yy");
                Repeater2.DataSource = null;
                Repeater2.DataBind();
                btnAddItemsIn.Visible = false;
                btnAddDT.Visible = true;
                ListItems.Visible = false;
                panelRed.Visible = true;
                //   BindProduct();
            }
            //  btnSaveData.Visible = false;
            //    btnConfirmOrder.Visible = false;

        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteOverHed")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int MYTRANSID = Convert.ToInt32(ID[0]);
                int MYID = Convert.ToInt32(ID[1]);
                TempList = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
                ICOVERHEADCOST TempList12 = TempList.First(p => p.MYTRANSID == MYTRANSID && p.MYID == MYID);
                TempList.Remove(TempList12);
                var List = DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.TenentID == TID).ToList();
                if (List.Count() > 0)
                {
                    foreach (Database.ICOVERHEADCOST item in List)
                    {
                        DB.ICOVERHEADCOSTs.DeleteObject(item);
                        DB.SaveChanges();
                    }
                }

                if (TempList.Count() > 0)
                {
                    Repeater1.DataSource = TempList;
                    Repeater1.DataBind();
                }
                else
                {
                    clenoverhedcost();
                }

                decimal Sum = Convert.ToDecimal(TempList.Sum(p => p.NEWCOST));
                txtOHCostHD.Text = Sum.ToString();
                lblcr.Text = Sum.ToString();
            }
        }
        protected void txtOHAmntLocal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            decimal VAlue = 0;
            decimal Total = 0;
            bool Fl = true;
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                VAlue = Convert.ToDecimal(txtOHAmntLocal.Text);
                if (Fl == true)
                {
                    Total = VAlue;
                    Fl = false;
                }
                else
                {
                    Total = Total + VAlue;
                }


            }
            lblcr.Text = Total.ToString();
            txtOHCostHD.Text = Total.ToString();
        }
        protected void btnExit1_Click(object sender, EventArgs e)
        {
            if (ViewState["AddnewItems"] != null)
            {
                ListItems.Visible = true;
                panelRed.Visible = false;
                ViewState["AddnewItems"] = null;
                btndiscartitems.Text = "Exit";
                btnAddDT.Visible = false;
                btnAddItemsIn.Visible = true;
            }
            else
            {
                ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;
                txtQuantity.Text = txtUPriceForeign.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
            }
        }
        protected void btndiscorohcost_Click(object sender, EventArgs e)
        {
            clenoverhedcost();
        }
        protected void linkBinAddNew_Click(object sender, EventArgs e)
        {
            if (ViewState["ListICIT_BR_BIN"] != null)
            {
                ListICIT_BR_BIN = (List<ICIT_BR_BIN>)ViewState["ListICIT_BR_BIN"];
                ICIT_BR_BIN obj = new ICIT_BR_BIN();
                ListICIT_BR_BIN.Add(obj);
                listBin.DataSource = ListICIT_BR_BIN;
            }
            else
            {
                ICIT_BR_BIN obj = new ICIT_BR_BIN();
                ListICIT_BR_BIN.Add(obj);
                listBin.DataSource = ListICIT_BR_BIN;
            }
            ViewState["ListICIT_BR_BIN"] = ListICIT_BR_BIN;
            listBin.DataBind();
            ModalPopupExtender6.Show();
        }
        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            int QTY = Convert.ToInt32(txtQuantity.Text);
            int TCID = Convert.ToInt32(txtTraNoHD.Text);
            int PID = Convert.ToInt32(ddlProduct.SelectedValue);
            int Total = 0;
            bool multiperisebal = true;
            for (int i = 0; i < listperishibal.Items.Count; i++)
            {
                TextBox txtnewqty = (TextBox)listperishibal.Items[i].FindControl("txtnewqty");
                //TextBox txtqty = (TextBox)listperishibal.Items[i].FindControl("txtqty");
                if (txtnewqty.Text != "" && txtnewqty.Text != "0")
                {
                    int TXT = Convert.ToInt32(txtnewqty.Text);
                    Total = Total + TXT;
                }
            }
            if (QTY == Total)
            {
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                for (int i = 0; i < listperishibal.Items.Count; i++)
                {
                    TextBox txtBatchno = (TextBox)listperishibal.Items[i].FindControl("txtBatchno");
                    TextBox txtqty = (TextBox)listperishibal.Items[i].FindControl("txtqty");
                    TextBox txtproductdate = (TextBox)listperishibal.Items[i].FindControl("txtproductdate");
                    TextBox txtexpirydate = (TextBox)listperishibal.Items[i].FindControl("txtexpirydate");
                    TextBox txtlead2destroydate = (TextBox)listperishibal.Items[i].FindControl("txtlead2destroydate");
                    TextBox txtnewqty = (TextBox)listperishibal.Items[i].FindControl("txtnewqty");



                    if (txtnewqty.Text != "" && txtnewqty.Text != "0")
                    {
                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "PUR";
                        int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        int BinID = 999999998;
                        string BatchNo = txtBatchno.Text;
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtnewqty.Text);

                        string Reference = txtrefreshno.Text;
                        string RecodName = "Perishable";
                        DateTime ProdDate = Convert.ToDateTime(txtproductdate.Text);
                        DateTime ExpiryDate = Convert.ToDateTime(txtexpirydate.Text);
                        DateTime LeadDays2Destroy = Convert.ToDateTime(txtlead2destroydate.Text);
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = txtDescription.Text;
                        string MainDecrption = "";
                        if (multiperisebal == true)
                        {
                            MainDecrption = Discpition + "\n" + " Perishable : " + BatchNo + " : " + NewQty;
                            multiperisebal = false;
                        }
                        else
                            MainDecrption = Discpition + " , " + BatchNo + " : " + NewQty;
                        txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["Perishable"] = null;
            }
            else
            {
                pnlSuccessMsg123.Visible = true;

                lblMsg123.Text = "Perishibal selected Qty " + Total + " from " + QTY;
            }

        }
        protected void lbApproveIss_Click(object sender, EventArgs e)
        {
            int PID = Convert.ToInt32(ddlProduct.SelectedValue);
            int QTY = Convert.ToInt32(txtQuantity.Text);
            int Total = 0;
            bool multibin = true;
            for (int i = 0; i < listBin.Items.Count; i++)
            {
                TextBox txtqty = (TextBox)listBin.Items[i].FindControl("txtqty");
                if (txtqty.Text != "" && txtqty.Text != "0")
                {
                    int TXT = Convert.ToInt32(txtqty.Text);
                    Total = Total + TXT;
                }

            }
            if (QTY == Total)
            {
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                for (int i = 0; i < listBin.Items.Count; i++)
                {
                    //  DropDownList dropBacthCode = (DropDownList)listBin.Items[i].FindControl("dropBacthCode");
                    TextBox txtqty = (TextBox)listBin.Items[i].FindControl("txtqty");
                    //TextBox txtnewqty = (TextBox)listBin.Items[i].FindControl("txtnewqty");

                    Label lblbinid = (Label)listBin.Items[i].FindControl("lblbinid");
                    if (txtqty.Text != "" && txtqty.Text != "0")
                    {
                        int TCID = Convert.ToInt32(txtTraNoHD.Text);
                        int UomID = Convert.ToInt32(ddlUOM.SelectedValue);
                        int BinID = Convert.ToInt32(lblbinid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        //int BinID = 999999998;
                        string BatchNo = txtBatchNo.Text;
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtqty.Text);
                        string Reference = txtrefreshno.Text;
                        string RecodName = "MultiBIN";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = txtDescription.Text;
                        string UOMNAME = DB.TBLBINs.Single(p => p.BIN_ID == BinID && p.TenentID == TID).BINDesc1;
                        string UOMQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (multibin == true)
                        {
                            MainDecrption = Discpition + "\n" + " Bin : " + UOMNAME + " : " + NewQty;
                            multibin = false;
                        }
                        else
                            MainDecrption = Discpition + " , " + UOMNAME + " : " + NewQty;
                        txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["MultiBinStore"] = null;
            }
            else
            {
                pnlSuccessMsg123.Visible = true;

                lblMsg123.Text = "Multi Bin selected Qty " + Total + " from " + QTY;
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
        protected void ddlLocalForeign_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            int DDLFID = Convert.ToInt32(ddlLocalForeign.SelectedValue);
            if (DDLFID == 8866)
            {
                ddlLocalForeign.SelectedValue = "8866";
                ddlCurrency.Enabled = false;
                txtUPriceForeign.Enabled = false;
                txtUPriceLocal.Enabled = true;
                ddlCurrency.SelectedValue = CUNTRYID.ToString();
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
            Repeater3.DataSource = TempICTRPayTerms_HD;
            Repeater3.DataBind();
        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");
                DropDownList drppaymentMeted = (DropDownList)e.Item.FindControl("drppaymentMeted");
                Classes.EcommAdminClass.getdropdown(drppaymentMeted, TID, "Payment", "Method", "Inventeri", "REFTABLE");
                //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "REFTABLE");
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
        protected void btnserchAdvans_Click(object sender, EventArgs e)
        {
            if (HiddenField3.Value == "0" || HiddenField3.Value == "")
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Enter the Customer";
            }
            else
            {
                if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
                {
                    int Transid = Convert.ToInt32(Request.QueryString["transid"]);
                    int Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);

                    if (DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).AllowMinusQty == false)
                    {
                        string counts1;
                        Label AvailableQty=new Label();
                        counts1 = Classes.EcommAdminClass.BindAddvanserchBR(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID, LID, AvailableQty, "en-US").ToString();
                        if (counts1 == "1")
                        {
                            lblitemsearch.Visible = false;
                        }
                        else
                        {
                            lblitemsearch.Text = "Search found " + counts1 + " Records Use Drop Down to select one                ";
                            lblitemsearch.Visible = true;
                        }
                        txtQuantity.Text = "";
                        lblmultiuom.Visible = false;
                        lblmulticolor.Visible = false;
                        lblmultisize.Visible = false;
                        lblmultiperishable.Visible = false;
                        lblmultibinstore.Visible = false;
                        lblmultiserialize.Visible = false;
                    }
                    else
                    {
                        string counts2;
                        counts2 = Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                        if (counts2 == "1")
                        {
                            lblitemsearch.Visible = false;
                        }
                        else
                        {
                            lblitemsearch.Text = "Search found "+ counts2 +" Records Use Drop Down to select one                ";
                            lblitemsearch.Visible = true;
                        }
                        txtQuantity.Text = "";
                        lblmultiuom.Visible = false;
                        lblmulticolor.Visible = false;
                        lblmultisize.Visible = false;
                        lblmultiperishable.Visible = false;
                        lblmultibinstore.Visible = false;
                        lblmultiserialize.Visible = false;
                    }
                }

            }

        }
        protected void btnserchcutmer_Click(object sender, EventArgs e)
        {
            //Classes.EcommAdminClass.BindSerchcutomer(txtserchCustmer, HiddenField3);
        }

        protected void btnAddperishable_Click(object sender, EventArgs e)
        {
            if (ViewState["ListICIT_BR_Perishable"] != null)
            {
                ListICIT_BR_Perishable = (List<ICIT_BR_Perishable>)ViewState["ListICIT_BR_Perishable"];
                ICIT_BR_Perishable obj = new ICIT_BR_Perishable();
                ListICIT_BR_Perishable.Add(obj);
                listperishibal.DataSource = ListICIT_BR_Perishable;
            }
            else
            {
                ICIT_BR_Perishable obj = new ICIT_BR_Perishable();
                ListICIT_BR_Perishable.Add(obj);
                listperishibal.DataSource = ListICIT_BR_Perishable;
            }
            ViewState["ListICIT_BR_Perishable"] = ListICIT_BR_Perishable;
            listperishibal.DataBind();
            ModalPopupExtender4.Show();
        }

        protected void rbtPsle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            if (rbtPsle.SelectedValue == "1")
            {

                Drpdown.Visible = true;
                doptext.Visible = false;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                    TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                    TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                    TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                    LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
                    ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = true;
                }
                ButtonAdd.Enabled = buttonUpdate.Enabled = btndiscorohcost.Enabled = true;
            }
            else
            {

                Drpdown.Visible = false;
                doptext.Visible = true;
                ddlLocalForeign.SelectedValue = "8866";
                ddlCurrency.Enabled = false;
                txtUPriceForeign.Enabled = false;
                txtUPriceLocal.Enabled = true;
                ddlCurrency.SelectedValue = CUNTRYID.ToString();
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                    TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                    TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                    TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                    LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
                    ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = false;
                }
                ButtonAdd.Enabled = buttonUpdate.Enabled = btndiscorohcost.Enabled = false;
            }

        }

        protected void cbslectsernumber_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listSerial.Items.Count(); i++)
            {
                TextBox txtlistSerial = (TextBox)listSerial.Items[i].FindControl("txtlistSerial");
                CheckBox cbslectsernumber = (CheckBox)listSerial.Items[i].FindControl("cbslectsernumber");
                Label lbltidser = (Label)listSerial.Items[i].FindControl("lbltidser");
                Label lblpidsril = (Label)listSerial.Items[i].FindControl("lblpidsril");
                Label lblpdcodeser = (Label)listSerial.Items[i].FindControl("lblpdcodeser");
                Label lblmysysser = (Label)listSerial.Items[i].FindControl("lblmysysser");
                Label lbluomser = (Label)listSerial.Items[i].FindControl("lbluomser");
                Label lbllocaser = (Label)listSerial.Items[i].FindControl("lbllocaser");
                int TID = Convert.ToInt32(lbltidser.Text);
                int LID = Convert.ToInt32(lbllocaser.Text);
                int PID = Convert.ToInt32(lblpidsril.Text);
                string PICODE = lblpdcodeser.Text;
                string UOM = lbluomser.Text;


                if (cbslectsernumber.Checked == true)
                {
                    string drpval = txtlistSerial.Text;


                    if (ViewState["SaveList1"] != null)
                    {
                        ViewState["SaveList1"] += "," + drpval;
                        tags_2.Text = ViewState["SaveList1"].ToString();
                    }
                    else
                    {
                        ViewState["SaveList1"] = drpval;
                        tags_2.Text = ViewState["SaveList1"].ToString();
                    }
                    ViewState["SaveList1"] = tags_2.Text;
                    List<ICIT_BR_Serialize> Listofsirlnumber = ((List<ICIT_BR_Serialize>)ViewState["ListofSerlNumber"]);
                    ICIT_BR_Serialize TempList12 = Listofsirlnumber.Single(p => p.TenentID == TID && p.LocationID == LID && p.period_code == PICODE && p.MyProdID == PID && p.UOM == UOM && p.Serial_Number == drpval);

                    Listofsirlnumber.Remove(TempList12);
                    listSerial.DataSource = Listofsirlnumber;
                    listSerial.DataBind();
                    ViewState["ListofSerlNumber"] = Listofsirlnumber;
                }

            }
            ModalPopupExtender2.Show();

        }

        protected void btnlistserch_Click(object sender, EventArgs e)
        {
            List<Database.viewTransaction> List = new List<Database.viewTransaction>();


            int Status1 = 0;
            int TID = ((USER_MST)Session["USER"]).TenentID;
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
            int TID = ((USER_MST)Session["USER"]).TenentID;
            int LID = ((USER_MST)Session["USER"]).LOCATION_ID;
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
    }
}