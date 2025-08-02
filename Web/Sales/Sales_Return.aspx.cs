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
    public partial class Sales_Return : System.Web.UI.Page
    {
        static string Crypath = "";
        CallEntities DB = new CallEntities();
        //  Database.CallEntities DB1 = new Database.CallEntities();
        List<Database.ICOVERHEADCOST> TempList = new List<Database.ICOVERHEADCOST>();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        List<ICIT_BR_BIN> ListICIT_BR_BIN = new List<ICIT_BR_BIN>();
        List<ICIT_BR_Perishable> ListICIT_BR_Perishable = new List<ICIT_BR_Perishable>();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
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
                string uidd = ((USER_MST)Session["USER"]).USER_ID.ToString();
                bool Isadmin = Classes.CRMClass.ISAdmin(TID, uidd);
                if (Isadmin == false)
                {
                    btnEditLable.Visible = false;
                }
                FistTimeLoad();
                ViewState["TempListICTR_DT"] = null;
                ViewState["AddnewItems"] = null;
                ViewState["StrMultiSize"] = null;
                BindData();
                BINDHDDATA();

                int MAXID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                txttanno.Text = MAXID.ToString();
                txtOrderDate.Text = DateTime.Now.ToString("dd-MMM-yy");
                if (Request.QueryString["MYPRODID"] != null)
                {
                    int Program = Convert.ToInt32(Request.QueryString["MYPRODID"]);
                    //   ddlProduct.SelectedValue = Program.ToString();
                    //   Classes.EcommAdminClass.BindProdu(Program, ddlUOM, txtDescription, txtserchProduct, TID);
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
                        if (DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && (p.Status == "SR" || p.Status == "DSR")).Count() > 0)
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
            objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            CURRENCY = objtblsetupsalesh.COUNTRYID.ToString();
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
            if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            {
                Transid = Convert.ToInt32(Request.QueryString["transid"]);
                Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                Session["Transid"] = Transid + "," + Transsubid;
            }

        }
        public void GetShow()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-1  getshow";
            //   lblProduct1s.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier1s.Attributes["class"] = lblLocalForeign1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblProjectNo1s.Attributes["class"] = lblReference1s.Attributes["class"] = lblTerms1s.Attributes["class"] = lblCRMActivity1s.Attributes["class"] = "control-label col-md-3  getshow";
            lblOHCost1s.Attributes["class"] = lblPerUnitValue1s.Attributes["class"] = lblTotal1s.Attributes["class"] = "control-label col-md-12  getshow";
            lblProductCodeProductName1s.Attributes["class"] = lblQuantity11s.Attributes["class"] = lblUnitPrice1s.Attributes["class"] = lblTax11s.Attributes["class"] = lblTotal11s.Attributes["class"] = lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblProductCodeProductName2h.Attributes["class"] = lblQuantity12h.Attributes["class"] = lblUnitPrice2h.Attributes["class"] = lblTax12h.Attributes["class"] = lblTotal12h.Attributes["class"] = lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  gethide";
            // lblProduct2h.Attributes["class"] = "control-label col-md-2 gethide";
            lblSupplier2h.Attributes["class"] = lblLocalForeign2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblProjectNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = lblTransactionDate2h.Attributes["class"] = lblTerms2h.Attributes["class"] = lblCRMActivity2h.Attributes["class"] = "control-label col-md-3 gethide";
            lblOHCost2h.Attributes["class"] = lblPerUnitValue2h.Attributes["class"] = lblTotal2h.Attributes["class"] = "control-label col-md-12 gethide";
            lblNotes2h.Attributes["class"] = "control-label col-md-1 gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }
        public void GetHide()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-1  gethide";
            //  lblProduct1s.Attributes["class"] = "control-label col-md-2  gethide";
            lblSupplier1s.Attributes["class"] = lblLocalForeign1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblProjectNo1s.Attributes["class"] = lblReference1s.Attributes["class"] = lblTerms1s.Attributes["class"] = lblCRMActivity1s.Attributes["class"] = "control-label col-md-3  gethide";
            lblOHCost1s.Attributes["class"] = lblPerUnitValue1s.Attributes["class"] = lblTotal1s.Attributes["class"] = "control-label col-md-12  gethide";
            lblProductCodeProductName1s.Attributes["class"] = lblQuantity11s.Attributes["class"] = lblUnitPrice1s.Attributes["class"] = lblTax11s.Attributes["class"] = lblTotal11s.Attributes["class"] = lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblProductCodeProductName2h.Attributes["class"] = lblQuantity12h.Attributes["class"] = lblUnitPrice2h.Attributes["class"] = lblTax12h.Attributes["class"] = lblTotal12h.Attributes["class"] = lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  getshow";
            //  lblProduct2h.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier2h.Attributes["class"] = lblLocalForeign2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblProjectNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = lblTransactionDate2h.Attributes["class"] = lblTerms2h.Attributes["class"] = lblCRMActivity2h.Attributes["class"] = "control-label col-md-3  getshow";
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
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblLocalForeign2h.Visible = lblCurrency2h.Visible = lblBatchNo2h.Visible = lblProjectNo2h.Visible = lblReference2h.Visible = lblTerms2h.Visible = lblCRMActivity2h.Visible = lblOHCost2h.Visible = lblPerUnitValue2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblProductCodeProductName2h.Visible = lblQuantity12h.Visible = lblUnitPrice2h.Visible = lblTax12h.Visible = lblTotal12h.Visible = lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = false;
                    //2true
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtLocalForeign2h.Visible = txtCurrency2h.Visible = txtBatchNo2h.Visible = txtProjectNo2h.Visible = txtReference2h.Visible = txtTerms2h.Visible = txtCRMActivity2h.Visible = txtOHCost2h.Visible = txtPerUnitValue2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtProductCodeProductName2h.Visible = txtQuantity12h.Visible = txtUnitPrice2h.Visible = txtTax12h.Visible = txtTotal12h.Visible = txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = true;

                    //header

                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblLocalForeign2h.Visible = lblCurrency2h.Visible = lblBatchNo2h.Visible = lblProjectNo2h.Visible = lblReference2h.Visible = lblTerms2h.Visible = lblCRMActivity2h.Visible = lblOHCost2h.Visible = lblPerUnitValue2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblProductCodeProductName2h.Visible = lblQuantity12h.Visible = lblUnitPrice2h.Visible = lblTax12h.Visible = lblTotal12h.Visible = lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = true;
                    //2false
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtLocalForeign2h.Visible = txtCurrency2h.Visible = txtBatchNo2h.Visible = txtProjectNo2h.Visible = txtReference2h.Visible = txtTerms2h.Visible = txtCRMActivity2h.Visible = txtOHCost2h.Visible = txtPerUnitValue2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtProductCodeProductName2h.Visible = txtQuantity12h.Visible = txtUnitPrice2h.Visible = txtTax12h.Visible = txtTotal12h.Visible = txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = false;

                    //header

                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblLocalForeign1s.Visible = lblCurrency1s.Visible = lblBatchNo1s.Visible = lblProjectNo1s.Visible = lblReference1s.Visible = lblTerms1s.Visible = lblCRMActivity1s.Visible = lblOHCost1s.Visible = lblPerUnitValue1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblProductCodeProductName1s.Visible = lblQuantity11s.Visible = lblUnitPrice1s.Visible = lblTax11s.Visible = lblTotal11s.Visible = lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = false;
                    //1true
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtLocalForeign1s.Visible = txtCurrency1s.Visible = txtBatchNo1s.Visible = txtProjectNo1s.Visible = txtReference1s.Visible = txtTerms1s.Visible = txtCRMActivity1s.Visible = txtOHCost1s.Visible = txtPerUnitValue1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtProductCodeProductName1s.Visible = txtQuantity11s.Visible = txtUnitPrice1s.Visible = txtTax11s.Visible = txtTotal11s.Visible = txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = true;

                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblLocalForeign1s.Visible = lblCurrency1s.Visible = lblBatchNo1s.Visible = lblProjectNo1s.Visible = lblReference1s.Visible = lblTerms1s.Visible = lblCRMActivity1s.Visible = lblOHCost1s.Visible = lblPerUnitValue1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblProductCodeProductName1s.Visible = lblQuantity11s.Visible = lblUnitPrice1s.Visible = lblTax11s.Visible = lblTotal11s.Visible = lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = true;
                    //1false
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtLocalForeign1s.Visible = txtCurrency1s.Visible = txtBatchNo1s.Visible = txtProjectNo1s.Visible = txtReference1s.Visible = txtTerms1s.Visible = txtCRMActivity1s.Visible = txtOHCost1s.Visible = txtPerUnitValue1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtProductCodeProductName1s.Visible = txtQuantity11s.Visible = txtUnitPrice1s.Visible = txtTax11s.Visible = txtTotal11s.Visible = txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = false;

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
        protected void drpinvoicno_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TRCNNO = Convert.ToInt32(drpinvoicno.SelectedValue);
            EditSalesh(TRCNNO);
            int MAXID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
            txttanno.Text = MAXID.ToString();
        }
        public void readMode()
        {
            drpterms.Enabled = rbtPsle.Enabled = drpinvoicno.Enabled = drpReason.Enabled = false;
            chbDeliNote.Enabled = drpselsmen.Enabled = false;
            //txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = false;
            ddlLocalForeign.Enabled = txtLocationSearch.Enabled = ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = false;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = txtOHCostHD.Enabled = txttotxl.Enabled = false;

            //for (int i = 0; i < Repeater1.Items.Count; i++)
            //{
            //    DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
            //    TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
            //    TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
            //    TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
            //    LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
            //    ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = lnkbtndelete1.Enabled = false;
            //}
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");

                txtreturnqty.Enabled = false;
            }
            //for (int i = 0; i < Repeater3.Items.Count; i++)
            //{
            //    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
            //    TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
            //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
            //    txtrefresh.Enabled = txtammunt.Enabled = btnaddamunt.Enabled = drppaymentMeted.Enabled = false;
            //}

            btnSubmit.Visible = btnConfirmOrder.Visible = hlPeri.Visible = btnSaveData.Visible = Custmer.Visible = BTNsAVEcONFsA.Visible = false;
            btnNew.Visible = true;
            btnnewAdd.Visible = true;
            btnrefesh.Visible = false;
            btnPrint.Visible = true;
            // btnAddItemsIn.Enabled = btnAddDT.Enabled = btndiscartitems.Enabled =  false;

        }
        public void WrietMode()
        {
            drpterms.Enabled = rbtPsle.Enabled = drpinvoicno.Enabled = drpReason.Enabled = true;
            chbDeliNote.Enabled = drpselsmen.Enabled = true;
            // txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = true;
            ddlLocalForeign.Enabled = txtLocationSearch.Enabled = ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = true;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = true;

            //for (int i = 0; i < Repeater1.Items.Count; i++)
            //{
            //    DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
            //    TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
            //    TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
            //    TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
            //    LinkButton lnkbtndelete1 = (LinkButton)Repeater1.Items[i].FindControl("lnkbtndelete1");
            //    ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = lnkbtndelete1.Enabled = true;
            //}
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");

                txtreturnqty.Enabled = true;
            }
            //for (int i = 0; i < Repeater3.Items.Count; i++)
            //{
            //    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
            //    TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
            //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
            //    txtrefresh.Enabled = txtammunt.Enabled = btnaddamunt.Enabled = drppaymentMeted.Enabled = true;
            //}
            btnSubmit.Visible = btnConfirmOrder.Visible = btnPrint.Visible = hlPeri.Visible = btnSaveData.Visible = Custmer.Visible = BTNsAVEcONFsA.Visible = true;
            btnNew.Visible = false;
            btnnewAdd.Visible = false;
            btnrefesh.Visible = true;
            //  btnAddItemsIn.Enabled = btnAddDT.Enabled = btndiscartitems.Enabled = true;
        }
        public void LastData()
        {
            if (DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && (p.Status == "SR" || p.Status == "DSR")).Count() > 0)
            {
                int Tarjictio = Convert.ToInt32(DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && (p.Status == "SR" || p.Status == "DSR")).Max(p => p.MYTRANSID));
                int MYTID = Tarjictio;
                if (DB.ICTR_HD.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).Count() > 0)
                {
                    ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                    drpinvoicno.SelectedValue = OBJICTR_HD.RefTransID.ToString();
                    txtTraNoHD.Text = OBJICTR_HD.ExtraSwitch2;
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
                    txttanno.Text = OBJICTR_HD.MYTRANSID.ToString();
                    txtTrnsdoc.Text = OBJICTR_HD.TransDocNo.ToString() + " / " + OBJICTR_HD.MYTRANSID.ToString();
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

                    decimal Sum = Convert.ToDecimal(0);
                    txtOHCostHD.Text = Sum.ToString();
                    // lblcr.Text = Sum.ToString();

                    //  btnAddItemsIn.Visible = true;
                    //  btnAddDT.Visible = false;
                    BindDT(MYTID);
                    readMode();
                    //  panelRed.Visible = false;
                    btnPrint.Visible = false;
                }
            }
        }
        public void BindData()
        {
            ICOVERHEADCOST objEco_ICEXTRACOST = new ICOVERHEADCOST();
            TempList.Add(objEco_ICEXTRACOST);
            string USERID = UID.ToString();

            //ViewState["TempList"] = TempList;
            //Repeater1.DataSource = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
            //Repeater1.DataBind();

            //ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            //TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            //ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            //Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            //Repeater3.DataBind();
            //Repeater1.DataSource = null ;
            //Repeater1.DataBind();

            Classes.EcommAdminClass.getdropdown(drpinvoicno, TID, "4101", "", "SAL", "ICTR_HD");
            Classes.EcommAdminClass.getdropdown(ddlLocalForeign, TID, "LF", "OTH", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlCrmAct, TID, "ACTVTY", "Transactions", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpterms, TID, "Terms", "Terms", "Eco", "REFTABLE");
            //  Classes.EcommAdminClass.getdropdown(ddlSupplier, TID, "1", "2", "", "TBLCOMPANYSETUP");
            //  Classes.EcommAdminClass.getdropdown(ddlcustmoerlist, TID, "1", "2", "", "TBLCOMPANYSETUP");
            Classes.EcommAdminClass.getdropdown(ddlCurrency, TID, "", "", "", "tblCOUNTRY");
            Classes.EcommAdminClass.getdropdown(ddlProjectNo, TID, "", "", "", "TBLPROJECT");

            // Classes.EcommAdminClass.getdropdown(ddlProduct, TID, "", "", "", "TBLPRODUCT");
            //   Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
            Classes.EcommAdminClass.getdropdown(drpRefnstype, TID, "Reference", "RefSubType", "Sales", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpselsmen, TID, LID.ToString(), "", "", "tbl_Employee");
            CheckRole();
            //drpselsmen.DataSource = DB.tbl_Employee.Where(p => p.TanentId == TID && p.LocationID == LID && p.Deleted == true);
            //drpselsmen.DataTextField = "firstname";
            //drpselsmen.DataValueField = "employeeID";
            //drpselsmen.DataBind();
            //drpselsmen.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Salesman--", "0"));

            drpReason.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Reason" && p.REFSUBTYPE == "ReturnReason");
            drpReason.DataTextField = "REFNAME1";
            drpReason.DataValueField = "REFID";
            drpReason.DataBind();
            drpReason.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Reason--", "0"));
        }
        //for saleman
        protected void CheckRole()
        {
            drpselsmen.SelectedValue = EMPID.ToString();
            if (objtblsetupsalesh.SalesAdminID == UID)
                drpselsmen.Enabled = true;
            else
                drpselsmen.Enabled = false;
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
            grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && (p.Status == "SR" || p.Status == "DSR")).OrderByDescending(p => p.TRANSDATE);
            grdPO.DataBind();
            for (int i = 0; i < grdPO.Items.Count; i++)
            {
                LinkButton btnprient = (LinkButton)grdPO.Items[i].FindControl("btnprient");
                LinkButton lnkbtndelete = (LinkButton)grdPO.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkbtnPurchase_Order = (LinkButton)grdPO.Items[i].FindControl("lnkbtnPurchase_Order");
                Label lbluserid = (Label)grdPO.Items[i].FindControl("lbluserid");
                Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                Label lblActive = (Label)grdPO.Items[i].FindControl("lblActive");
                bool Act = Convert.ToBoolean(lblActive.Text);
                bool Active = Convert.ToBoolean(Act);
                if (Active == false)
                {                   
                    btnprient.Visible = true;
                }
                else
                {
                    btnprient.Visible = true;
                }
                //string Steate = lblStatus.Text;
                //if (Steate == "DSO")
                //    btnprient.Visible = false;                
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
            ICOVERHEADCOST objEco_ICEXTRACOST = new ICOVERHEADCOST();
            objEco_ICEXTRACOST.TenentID = TID;
            objEco_ICEXTRACOST.MYTRANSID = Convert.ToInt32(txttanno.Text);
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
            //  Repeater1.DataSource = TempList;
            //  Repeater1.DataBind();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
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
                int transid = Convert.ToInt32(Request.QueryString["transid"]);
                int transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid);
                string MainTranType = "O";// Out Qty On Product
                string TransType = objProFile.TranType;
                string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
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
                        TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");
                        Label lblAMOUNT = (Label)Repeater2.Items[i].FindControl("lblAMOUNT");
                        int NewQty = Convert.ToInt32(txtreturnqty.Text);
                        int Oldqty = Convert.ToInt32(lblDisQnty.Text);
                        if (Oldqty >= NewQty)
                        {
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
                            int QUANTITY = Convert.ToInt32(NewQty);
                            //decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                            //decimal AMOUNT = Convert.ToDecimal(QUANTITY * UNITPRICE);
                            decimal AMOUNT = Convert.ToDecimal(lblAMOUNT.Text);
                            decimal UNITPRICE = Convert.ToDecimal(AMOUNT / QUANTITY);
                            decimal OVERHEADAMOUNT = Convert.ToDecimal(0);
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

                            //==========================================Start ICTR_DT
                            Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                            objICTR_DT.TenentID = TenentID;
                            objICTR_DT.MYTRANSID = MYTRANSID;
                            objICTR_DT.locationID = locationID;
                            var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                            int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                            objICTR_DT.MYID = MYIDDT;
                            objICTR_DT.MyProdID = MyProdID;
                            objICTR_DT.REFTYPE = REFTYPE;
                            objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                            objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                            objICTR_DT.MYSYSNAME = MYSYSNAME;
                            objICTR_DT.JOID = JOID;
                            objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                            objICTR_DT.ACTIVITYID = ACTIVITYID;
                            objICTR_DT.DESCRIPTION = DESCRIPTION;
                            objICTR_DT.UOM = UOM;
                            objICTR_DT.QUANTITY = QUANTITY;
                            objICTR_DT.UNITPRICE = UNITPRICE;
                            objICTR_DT.AMOUNT = AMOUNT;
                            objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                            objICTR_DT.BATCHNO = BATCHNO;
                            objICTR_DT.BIN_ID = BIN_ID;
                            objICTR_DT.BIN_TYPE = BIN_TYPE;
                            objICTR_DT.GRNREF = GRNREF;
                            objICTR_DT.DISPER = DISPER;
                            objICTR_DT.DISAMT = DISAMT;
                            objICTR_DT.TAXAMT = TAXAMT;
                            objICTR_DT.TAXPER = TAXPER;
                            objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                            objICTR_DT.GLPOST = GLPOST;
                            objICTR_DT.GLPOST1 = GLPOST1;
                            objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                            objICTR_DT.GLPOSTREF = GLPOSTREF;
                            objICTR_DT.ICPOST = ICPOST;
                            objICTR_DT.ICPOSTREF = ICPOSTREF;
                            objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                            objICTR_DT.COMPANYID = COMPANYID1;
                            objICTR_DT.SWITCH1 = SWITCH1;
                            objICTR_DT.ACTIVE = ACTIVE;
                            objICTR_DT.DelFlag = DelFlag;

                            DB.ICTR_DT.AddObject(objICTR_DT);
                            DB.SaveChanges();
                            //==========================================End ICTR_DT
                            //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

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

                        }
                        else
                        {
                            panelMsg.Visible = true;
                            lblErreorMsg.Text = "New Qty are not greterthen of Old Qty";
                            return;
                        }
                    }

                    string REFERENCE = txtrefreshno.Text;

                    int ToTenentID = objProFile.ToTenantID;
                    int TOLOCATIONID = LID;

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

                    var invoiceHD = DB.ICTR_HD.Single(p => p.MYTRANSID == TransNo && p.transid == transid && p.transsubid == transsubid).InvoiceNO;
                    int InvoiceNO = Convert.ToInt32(invoiceHD);
                    decimal Discount = Convert.ToDecimal(0);

                    Status = "DSR";
                    int Terms = Convert.ToInt32(drpterms.SelectedValue);
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
                    }
                    int invoisno = Convert.ToInt32(drpinvoicno.SelectedValue);
                    string BillNo = txtTraNoHD.Text;
                    string Reason = drpReason.SelectedValue;

                    var DocNo = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == TransNo && p.transid == transid && p.transsubid == transsubid).TransDocNo;
                    string TransDocNo = DocNo;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, invoisno, Reason, TransDocNo);

                    var LIstTotal = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    decimal TOALSUM = Convert.ToDecimal(LIstTotal.Sum(p => p.AMOUNT));
                    int TOTQTY = Convert.ToInt32(LIstTotal.Sum(p => p.QUANTITY));


                    var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    foreach (Database.ICTRPayTerms_HD item in List2)
                    {

                        DB.ICTRPayTerms_HD.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
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
                    btnSaveData.Text = "Confirm Sales Return";
                    btnConfirmOrder.Text = "Confirm Sales Return";
                    ViewState["HDMYtrctionid"] = null;

                    //For Print Dipak
                    btnPrint.Visible = true;
                    ViewState["Print"] = TransNo;
                    ViewState["AgainsID"] = invoisno;
                    //Response.Redirect("SaleReturnHPSPrints.aspx?Tranjestion=" + TransNo + "&MYTRANSID=" + invoisno);
                }
                else
                {
                    int InvoiceNoTaction = 0;
                    TransNo = Convert.ToInt32(txttanno.Text);
                    string REFERENCE = txtrefreshno.Text;
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    int ToTenentID = objProFile.ToTenantID;
                    int TOLOCATIONID = LID;

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
                    if (DB.ICTR_HD.Where(p => p.transid == transid && p.transsubid == transsubid && p.TenentID == TID).Count() > 0)
                    {
                        string invoiceNOHD = DB.ICTR_HD.Where(p => p.transid == transid && p.transsubid == transsubid && p.TenentID == TID).Max(p => p.InvoiceNO);
                        InvoiceNoTaction = (Convert.ToInt32(invoiceNOHD) + 1);
                    }
                    else
                    {
                        InvoiceNoTaction = Invoiec;
                    }

                    //int InvoiceNoTaction = DB.ICTR_HD.Where(p => p.transid == transid && p.transsubid == transsubid && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.transid == transid && p.transsubid == transsubid && p.TenentID == TID).Max(p => p.InvoiceNO) + 1) : Invoiec;
                    int InvoiceNO = InvoiceNoTaction;
                    decimal Discount = Convert.ToDecimal(0);
                    Status = "DSR";
                    int Terms = Convert.ToInt32(drpterms.SelectedValue); ;
                    string DatainserStatest = "Add";
                    int invoisno = Convert.ToInt32(drpinvoicno.SelectedValue);
                    string BillNo = txtTraNoHD.Text;
                    string Reason = drpReason.SelectedValue;

                    //
                    Classes.EcommAdminClass.TransDocNo(TID, transid, transsubid);
                    var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid).ToList();
                    string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid).serialno) + 1).ToString();
                    Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid);
                    objtblsubtype.serialno = SirialNO;
                    DB.SaveChanges();
                    //
                    string TransDocNo = SirialNO;
                    Classes.EcommAdminClass.insert_ICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO.ToString(), Discount, Status, Terms, Custmerid, Swit1, BillNo, invoisno, Reason, TransDocNo, 0, 0, 0);
                    //Dipak
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
                        TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");
                        Label lblAMOUNT = (Label)Repeater2.Items[i].FindControl("lblAMOUNT");
                        int NewQty = Convert.ToInt32(txtreturnqty.Text);
                        int Oldqty = Convert.ToInt32(lblDisQnty.Text);
                        if (Oldqty >= NewQty)
                        {
                            if (NewQty != 0)
                            {
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
                                int QUANTITY = Convert.ToInt32(NewQty);
                                //decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                                //decimal AMOUNT = Convert.ToDecimal(QUANTITY * UNITPRICE);
                                decimal AMOUNT = Convert.ToDecimal(lblAMOUNT.Text);
                                decimal UNITPRICE = Convert.ToDecimal(AMOUNT / QUANTITY);
                                decimal OVERHEADAMOUNT = Convert.ToDecimal(0);
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

                                int MYID = 0;

                                List<Database.ICTR_DT> Listmydt = DB.ICTR_DT.Where(p => p.TenentID == TenentID && p.MYTRANSID == MYTRANSID && p.MyProdID == MyProdID && p.UOM == UOM).ToList();

                                if (Listmydt.Count() > 0)
                                {
                                    MYID = Listmydt.FirstOrDefault().MYID;
                                }

                                //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                                Classes.EcommAdminClass.insert_ICTR_DT(TenentID, MYTRANSID, LID, MYID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID, null);
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
                        }
                        else
                        {
                            panelMsg.Visible = true;
                            lblErreorMsg.Text = "New Qty are not greterthen of Old Qty";
                            return;
                        }
                    }


                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
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

                    string SupplierName = txtLocationSearch.Text;
                    string ADDACTIvity = "Add";
                    string DAteTime = TRANSDATE.ToShortDateString();
                    string Discription = TranType + " , " + TransNo + " , " + DAteTime + " , " + SupplierName + " , " + NOTES.ToString();
                    //InsertCRMMainActivity(COMPID, TransNo, Status, ADDACTIvity, TranType, Discription);
                    ////    InsertCRMMainActivity(COMPID, TransNo, Status, ADDACTIvity, TranType, NOTES);
                    //    int SID = 1240103;
                    //    string BName = "SO Final";
                    //    string P2 = "aa";
                    //    string P3 = "aa";
                    //    inserCrmproghw(TenentID, SID, BName, P2, P3, TransNo);

                    //For Print Dipak
                    btnPrint.Visible = true;
                    ViewState["Print"] = TransNo;
                    ViewState["AgainsID"] = invoisno;
                    //Response.Redirect("SaleReturnHPSPrints.aspx?Tranjestion=" + TransNo + "&MYTRANSID=" + invoisno);
                }
                BINDHDDATA();
                readMode();
            //    scope.Complete(); //  To commit.

            //}
            
        }
        public int Pidalcode(DateTime Time)
        {
            int PODID = 0;
            if (DB.TBLPERIODS.Where(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.TenentID == TID && p.MYSYSNAME == "SAL").Count() > 0)
            {
                PODID = Convert.ToInt32(DB.TBLPERIODS.Single(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.TenentID == TID && p.MYSYSNAME == "SAL").PERIOD_CODE);

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
                int transid = Convert.ToInt32(Request.QueryString["transid"]);
                int transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid);
                string MainTranType = "O";// Out Qty On Product
                string TransType = objProFile.TranType;
                string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
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
                        TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");
                        Label lblAMOUNT = (Label)Repeater2.Items[i].FindControl("lblAMOUNT");
                        int NewQty1 = Convert.ToInt32(txtreturnqty.Text);
                        int Oldqty = Convert.ToInt32(lblDisQnty.Text);
                        if (Oldqty >= NewQty1)
                        {
                            decimal DISPER = Convert.ToDecimal(lblDISPER.Text);
                            decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);
                            var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo && p.TenentID == TID);
                            //int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                            //int MYID = MYIDDT;
                            int MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                            string REFTYPE = "LF";
                            string REFSUBTYPE = "LF";
                            int JOBORDERDTMYID = 1;
                            int ACTIVITYID = Convert.ToInt32(ddlCrmAct.SelectedValue);
                            string DESCRIPTION = lblDiscription.Text;
                            string UOM = lblUOM.Text;
                            int QUANTITY = Convert.ToInt32(NewQty1);
                            //decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                            //decimal AMOUNT = Convert.ToDecimal(QUANTITY * UNITPRICE);
                            decimal AMOUNT = Convert.ToDecimal(lblAMOUNT.Text);
                            decimal UNITPRICE = Convert.ToDecimal(AMOUNT / QUANTITY);
                            decimal OVERHEADAMOUNT = Convert.ToDecimal(0);
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

                            Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                            objICTR_DT.TenentID = TenentID;
                            objICTR_DT.MYTRANSID = MYTRANSID;
                            objICTR_DT.locationID = locationID;
                            //var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                            int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                            objICTR_DT.MYID = MYIDDT;
                            objICTR_DT.MyProdID = MyProdID;
                            objICTR_DT.REFTYPE = REFTYPE;
                            objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                            objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                            objICTR_DT.MYSYSNAME = MYSYSNAME;
                            objICTR_DT.JOID = JOID;
                            objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                            objICTR_DT.ACTIVITYID = ACTIVITYID;
                            objICTR_DT.DESCRIPTION = DESCRIPTION;
                            objICTR_DT.UOM = UOM;
                            objICTR_DT.QUANTITY = QUANTITY;
                            objICTR_DT.UNITPRICE = UNITPRICE;
                            objICTR_DT.AMOUNT = AMOUNT;
                            objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                            objICTR_DT.BATCHNO = BATCHNO;
                            objICTR_DT.BIN_ID = BIN_ID;
                            objICTR_DT.BIN_TYPE = BIN_TYPE;
                            objICTR_DT.GRNREF = GRNREF;
                            objICTR_DT.DISPER = DISPER;
                            objICTR_DT.DISAMT = DISAMT;
                            objICTR_DT.TAXAMT = TAXAMT;
                            objICTR_DT.TAXPER = TAXPER;
                            objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                            objICTR_DT.GLPOST = GLPOST;
                            objICTR_DT.GLPOST1 = GLPOST1;
                            objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                            objICTR_DT.GLPOSTREF = GLPOSTREF;
                            objICTR_DT.ICPOST = ICPOST;
                            objICTR_DT.ICPOSTREF = ICPOSTREF;
                            objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                            objICTR_DT.COMPANYID = COMPANYID1;
                            objICTR_DT.SWITCH1 = SWITCH1;
                            objICTR_DT.ACTIVE = ACTIVE;
                            objICTR_DT.DelFlag = DelFlag;

                            DB.ICTR_DT.AddObject(objICTR_DT);
                            DB.SaveChanges();
                            //==========================================================

                            //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                            TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
                            Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
                            Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
                            Boolean Perishable = Convert.ToBoolean(obj.Perishable);
                            Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
                            Boolean Serialized = Convert.ToBoolean(obj.Serialized);
                            Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);
                            //int PQTY = Convert.ToInt32(obj.QTYINHAND);
                            //int Total123 = PQTY + QUANTITY;
                            //obj.QTYINHAND = Total123;
                            //DB.SaveChanges();

                            string REFERENCE123 = txtrefreshno.Text;
                            int UOM1 = Convert.ToInt32(objICTR_DT.UOM);

                            bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, transid, transsubid, MYTRANSID, MYIDDT, QUANTITY, REFERENCE123, TACtionDate, MYSYSNAME, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
                            if (flag1 == false)
                            {
                                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "One of the Posting Parameter is Blank/Null/Zero!", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                                return;

                            }

                            //if (MultiBinStore == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiBIN" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int Bin_ID = listmulticolor[ii].Bin_ID;
                            //        int UOM1 = listmulticolor[ii].UOM;

                            //        int NEWQTY = Convert.ToInt32(listmulticolor[ii].NewQty);

                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";
                            //        string BatchNo = listmulticolor[ii].BatchNo; ;

                            //        string pagename = "Parchesh";
                            //        string Fuctions = "ADD";
                            //        Classes.EcommAdminClass.insertICIT_BR_BIN(TenentID, MyProdID, period_code, MySysName, Bin_ID, UOM1, LID, BatchNo, MYTRANSID, NEWQTY, Reference, Active, CRUP_ID, pagename, Fuctions);
                            //    }
                            //}
                            //if (Serialized == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Serialize" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";

                            //        string Serial_Number = listmulticolor[ii].Serial_Number;
                            //        int MyTransID = Convert.ToInt32(listmulticolor[ii].MYTRANSID);
                            //        string Active = "Y";
                            //        string Flag_GRN_GTN = "N";

                            //        string pagename = "Parchesh";
                            //        Classes.EcommAdminClass.insertICIT_BR_Serialize(TenentID, MyProdID, period_code, MySysName, UOM, Serial_Number, LID, MyTransID, Flag_GRN_GTN, Active, CRUP_ID, pagename);
                            //    }
                            //}
                            //if (MultiUOM != Convert.ToBoolean(1))
                            //{
                            //    int UOM1 = Convert.ToInt32(lblUOM.Text);
                            //    var listICIT_BR = DB.ICIT_BR.Where(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.MySysName == MYSYSNAME && p.UOM == UOM1).ToList();

                            //    string period_code = OICODID;
                            //    string MySysName = "IC";

                            //    decimal UnitCost = UNITPRICE;
                            //    int NewQty = QUANTITY;

                            //    string Reference = txtrefreshno.Text;
                            //    string Active = "Y";

                            //    string Bin_Per = "N";

                            //    string Fuctions = "ADD";
                            //    string pagename = "Parchesh";
                            //    Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //}
                            //if (MultiUOM == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "UOM" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {
                            //        int UOM1 = listmulticolor[ii].UOM;

                            //        string period_code = listmulticolor[ii].period_code;
                            //        string MySysName = "IC";
                            //        decimal UnitCost = UNITPRICE;

                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";
                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);

                            //        string Bin_Per = "N";

                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";
                            //        Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }
                            //}

                            //if (MultiColor == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiColor" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int UO1M = listmulticolor[ii].UOM;
                            //        int SIZECODE = 999999998;
                            //        int COLORID = listmulticolor[ii].COLORID;

                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);

                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";

                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";

                            //        Classes.EcommAdminClass.insertICIT_BR_SIZECOLOR(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }

                            //}
                            //if (MultiSize == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiSize" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int UO1M = listmulticolor[ii].UOM;
                            //        int SIZECODE = listmulticolor[ii].SIZECODE;
                            //        int COLORID = 999999998;

                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);

                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";

                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";

                            //        Classes.EcommAdminClass.insertICIT_BR_SIZE(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }

                            //}
                            //if (Perishable == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Perishable" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int UOM1 = listmulticolor[ii].UOM;
                            //        string BatchNo = listmulticolor[ii].BatchNo;

                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);

                            //        DateTime ProdDate = Convert.ToDateTime(listmulticolor[ii].ProdDate);
                            //        DateTime ExpiryDate = Convert.ToDateTime(listmulticolor[ii].ExpiryDate);
                            //        DateTime LeadDays2Destroy = Convert.ToDateTime(listmulticolor[ii].LeadDays2Destroy);
                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";
                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";

                            //        Classes.EcommAdminClass.insertICIT_BR_Perishable(TenentID, MyProdID, period_code, MySysName, UOM1, BatchNo, LID, MYTRANSID, NewQty, ProdDate, ExpiryDate, LeadDays2Destroy, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }

                            //}
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
                        else
                        {
                            panelMsg.Visible = true;
                            lblErreorMsg.Text = "New Qty are not greterthen of Old Qty";
                            return;
                        }
                    }
                    string REFERENCE = txtrefreshno.Text;

                    int ToTenentID = objProFile.ToTenantID;
                    int TOLOCATIONID = LID;

                    decimal COMPID1 = COMPID;

                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    string LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";

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
                    if (chbDeliNote.Checked == true)
                    {
                        Status = "RDSR";
                    }
                    else
                    {
                        Status = "SR";
                    }
                    int Terms = Convert.ToInt32(drpterms.SelectedValue);
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
                    }
                    int invoisno = Convert.ToInt32(drpinvoicno.SelectedValue);
                    string BillNo = txtTraNoHD.Text;
                    string Reason = drpReason.SelectedValue;
                    var DocNo = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == TransNo && transid == transid && p.transsubid == transsubid).TransDocNo;
                    string TransDocNo = DocNo;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, invoisno, Reason, TransDocNo);
                    var List1 = DB.ICOVERHEADCOSTs.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    foreach (Database.ICOVERHEADCOST item in List1)
                    {

                        DB.ICOVERHEADCOSTs.DeleteObject(item);
                        DB.SaveChanges();
                    }

                    var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    foreach (Database.ICTRPayTerms_HD item in List2)
                    {

                        DB.ICTRPayTerms_HD.DeleteObject(item);
                        DB.SaveChanges();
                    }
                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
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
                    readMode();
                    BINDHDDATA();
                    btnSaveData.Text = "Confirm Sales Return";
                    btnConfirmOrder.Text = "Confirm Sales Return";
                    ViewState["HDMYtrctionid"] = null;

                    //For Print Dipak 
                    btnPrint.Visible = true;
                    ViewState["Print"] = TransNo;
                    ViewState["AgainsID"] = invoisno;
                    //Response.Redirect("SaleReturnHPSPrints.aspx?Tranjestion=" + TransNo + "&MYTRANSID=" + invoisno);
                }
                else
                {
                    // insert deta is ICTR_HD
                    TransNo = Convert.ToInt32(txttanno.Text);
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    int ToTenentID = objProFile.ToTenantID;
                    int TOLOCATIONID = LID;

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
                        Status = "RDSR";
                    }
                    else
                    {
                        Status = "SR";
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
                    }
                    int invoisno = Convert.ToInt32(drpinvoicno.SelectedValue);
                    string BillNo = txtTraNoHD.Text;
                    string Reason = drpReason.SelectedValue;
                    //
                    Classes.EcommAdminClass.TransDocNo(TID, transid, transsubid);
                    var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid).ToList();
                    string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid).serialno) + 1).ToString();
                    Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == transid && p.transsubid == transsubid);
                    objtblsubtype.serialno = SirialNO;
                    DB.SaveChanges();
                    //
                    string TransDocNo = SirialNO;
                    Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, invoisno, Reason, TransDocNo);
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
                        TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");
                        Label lblAMOUNT = (Label)Repeater2.Items[i].FindControl("lblAMOUNT");
                        int NewQty1 = Convert.ToInt32(txtreturnqty.Text);
                        int Oldqty = Convert.ToInt32(lblDisQnty.Text);
                        if (Oldqty >= NewQty1)
                        {
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
                            int QUANTITY = Convert.ToInt32(NewQty1);
                            //decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                            //decimal AMOUNT = Convert.ToDecimal(QUANTITY * UNITPRICE);
                            decimal AMOUNT = Convert.ToDecimal(lblAMOUNT.Text);
                            decimal UNITPRICE = Convert.ToDecimal(AMOUNT / QUANTITY);
                            decimal OVERHEADAMOUNT = Convert.ToDecimal(0);
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
                            //***************************//
                            Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                            objICTR_DT.TenentID = TenentID;
                            objICTR_DT.MYTRANSID = MYTRANSID;
                            objICTR_DT.locationID = locationID;
                            var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                            int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                            objICTR_DT.MYID = MYIDDT;
                            objICTR_DT.MyProdID = MyProdID;
                            objICTR_DT.REFTYPE = REFTYPE;
                            objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                            objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                            objICTR_DT.MYSYSNAME = MYSYSNAME;
                            objICTR_DT.JOID = JOID;
                            objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                            objICTR_DT.ACTIVITYID = ACTIVITYID;
                            objICTR_DT.DESCRIPTION = DESCRIPTION;
                            objICTR_DT.UOM = UOM;
                            objICTR_DT.QUANTITY = QUANTITY;
                            objICTR_DT.UNITPRICE = UNITPRICE;
                            objICTR_DT.AMOUNT = AMOUNT;
                            objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                            objICTR_DT.BATCHNO = BATCHNO;
                            objICTR_DT.BIN_ID = BIN_ID;
                            objICTR_DT.BIN_TYPE = BIN_TYPE;
                            objICTR_DT.GRNREF = GRNREF;
                            objICTR_DT.DISPER = DISPER;
                            objICTR_DT.DISAMT = DISAMT;
                            objICTR_DT.TAXAMT = TAXAMT;
                            objICTR_DT.TAXPER = TAXPER;
                            objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                            objICTR_DT.GLPOST = GLPOST;
                            objICTR_DT.GLPOST1 = GLPOST1;
                            objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                            objICTR_DT.GLPOSTREF = GLPOSTREF;
                            objICTR_DT.ICPOST = ICPOST;
                            objICTR_DT.ICPOSTREF = ICPOSTREF;
                            objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                            objICTR_DT.COMPANYID = COMPANYID1;
                            objICTR_DT.SWITCH1 = SWITCH1;
                            objICTR_DT.ACTIVE = ACTIVE;
                            objICTR_DT.DelFlag = DelFlag;

                            DB.ICTR_DT.AddObject(objICTR_DT);
                            DB.SaveChanges();
                            //**********************************//
                            //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

                            TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
                            Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
                            Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
                            Boolean Perishable = Convert.ToBoolean(obj.Perishable);
                            Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
                            Boolean Serialized = Convert.ToBoolean(obj.Serialized);
                            Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);
                            //int PQTY = Convert.ToInt32(obj.QTYINHAND);
                            //int Total123 = PQTY + QUANTITY;
                            //obj.QTYINHAND = Total123;
                            //DB.SaveChanges();
                            int UOM1 = Convert.ToInt32(objICTR_DT.UOM);

                            bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, transid, transsubid, MYTRANSID, MYIDDT, QUANTITY, REFERENCE, TACtionDate, MYSYSNAME, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
                            if (flag1 == false)
                            {
                                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "One of the Posting Parameter is Blank/Null/Zero!", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                                return;

                            }



                            //if (MultiBinStore == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiBIN" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int Bin_ID = listmulticolor[ii].Bin_ID;
                            //        int UOM1 = listmulticolor[ii].UOM;

                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].OpQty);

                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";
                            //        string BatchNo = listmulticolor[ii].BatchNo; ;

                            //        string pagename = "Parchesh";
                            //        string Fuctions = "ADD";
                            //        Classes.EcommAdminClass.insertICIT_BR_BIN(TenentID, MyProdID, period_code, MySysName, Bin_ID, UOM1, LID, BatchNo, MYTRANSID, NewQty, Reference, Active, CRUP_ID, pagename, Fuctions);
                            //    }
                            //}
                            //if (Serialized == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Serialize" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        //string UOM = ddlUOM.SelectedValue;
                            //        string Serial_Number = listmulticolor[ii].Serial_Number;
                            //        int MyTransID = Convert.ToInt32(listmulticolor[ii].MYTRANSID);
                            //        string Active = "Y";
                            //        string Flag_GRN_GTN = "N";
                            //        // int CRUP_ID = 0;
                            //        string pagename = "Parchesh";
                            //        Classes.EcommAdminClass.insertICIT_BR_Serialize(TenentID, MyProdID, period_code, MySysName, UOM, Serial_Number, LID, MyTransID, Flag_GRN_GTN, Active, CRUP_ID, pagename);
                            //    }
                            //}
                            //if (MultiUOM != Convert.ToBoolean(1))
                            //{
                            //    int UOM1 = Convert.ToInt32(lblUOM.Text);
                            //    var listICIT_BR = DB.ICIT_BR.Where(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.MySysName == MYSYSNAME && p.UOM == UOM1).ToList();

                            //    string period_code = OICODID;
                            //    string MySysName = "IC";

                            //    decimal UnitCost = UNITPRICE;
                            //    int NewQty = QUANTITY;
                            //    //  int OpQty = QUANTITY;
                            //    string Reference = txtrefreshno.Text;
                            //    string Active = "Y";

                            //    string Bin_Per = "N";
                            //    //  int LeadTime = 0;
                            //    string Fuctions = "ADD";
                            //    string pagename = "Parchesh";
                            //    Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //}
                            //if (MultiUOM == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "UOM" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {
                            //        int UOM1 = listmulticolor[ii].UOM;

                            //        string period_code = listmulticolor[ii].period_code;
                            //        string MySysName = "IC";
                            //        decimal UnitCost = UNITPRICE;
                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                            //        // int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";

                            //        string Bin_Per = "N";
                            //        //    int LeadTime = 0;
                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";
                            //        Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }
                            //}

                            //if (MultiColor == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiColor" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int UO1M = listmulticolor[ii].UOM;
                            //        int SIZECODE = 999999998;
                            //        int COLORID = listmulticolor[ii].COLORID;
                            //        // int MYTRANSID = TCID;
                            //        int Nweqty = Convert.ToInt32(listmulticolor[ii].NewQty);
                            //        //  int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";

                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";
                            //        // int CRUP_ID = 0;
                            //        Classes.EcommAdminClass.insertICIT_BR_SIZECOLOR(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, Nweqty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }

                            //}
                            //if (MultiSize == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiSize" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int UO1M = listmulticolor[ii].UOM;
                            //        int SIZECODE = listmulticolor[ii].SIZECODE;
                            //        int COLORID = 999999998;
                            //        // int MYTRANSID = TCID;
                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                            //        //  int OpQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";

                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";
                            //        // int CRUP_ID = 0;
                            //        Classes.EcommAdminClass.insertICIT_BR_SIZE(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }

                            //}
                            //if (Perishable == Convert.ToBoolean(1))
                            //{
                            //    var listmulticolor = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.RecodName == "Perishable" && p.MyProdID == MyProdID).ToList();
                            //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                            //    {

                            //        string period_code = OICODID;
                            //        string MySysName = "SAL";
                            //        int UOM1 = listmulticolor[ii].UOM;
                            //        string BatchNo = listmulticolor[ii].BatchNo;

                            //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);

                            //        DateTime ProdDate = Convert.ToDateTime(listmulticolor[ii].ProdDate);
                            //        DateTime ExpiryDate = Convert.ToDateTime(listmulticolor[ii].ExpiryDate);
                            //        DateTime LeadDays2Destroy = Convert.ToDateTime(listmulticolor[ii].LeadDays2Destroy);
                            //        string Reference = listmulticolor[ii].Reference;
                            //        string Active = "Y";
                            //        string Fuctions = "ADD";
                            //        string pagename = "Parchesh";
                            //        //int CRUP_ID = 0;
                            //        Classes.EcommAdminClass.insertICIT_BR_Perishable(TenentID, MyProdID, period_code, MySysName, UOM1, BatchNo, LID, MYTRANSID, NewQty, ProdDate, ExpiryDate, LeadDays2Destroy, Reference, Active, CRUP_ID, Fuctions, pagename);
                            //    }

                            //}
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
                        else
                        {
                            panelMsg.Visible = true;
                            lblErreorMsg.Text = "New Qty are not greterthen of Old Qty";
                            return;
                        }
                    }


                    decimal Payment = Convert.ToDecimal(lblTotatotl.Text);
                    decimal Total = 0;
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
                    //For Print Dipak 
                    btnPrint.Visible = true;
                    ViewState["Print"] = TransNo;
                    ViewState["AgainsID"] = invoisno;
                    //Response.Redirect("SaleReturnHPSPrints.aspx?Tranjestion=" + TransNo + "&MYTRANSID=" + invoisno);
                }
                scope.Complete(); //  To commit.                               
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
            Classes.CRMClass.InserActivityMain(TenentID, COMPID, LocationID, ID, RouteID, UID, ACTIVITYE, UNAME, MDUID, 6, "Sales Return Final", CampynName, CampynDescription);

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
            // hidUPriceLocal.Value = hidTotalCurrencyForeign.Value = hidTotalCurrencyLocal.Value = "";
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


        List<Database.ICTR_DT> TempListICTR_DT = new List<Database.ICTR_DT>();

        protected void grdPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ViewState["TempList"] = null;
                int MYTID = Convert.ToInt32(e.CommandArgument);
                EditSalesreturn(MYTID);
                //Dipak
                string[] MYTRANSID = drpinvoicno.SelectedItem.ToString().Split('-');
                string AgainsID = MYTRANSID[1];
                ViewState["Print"] = MYTID;
                ViewState["AgainsID"] = AgainsID;
            }
            if (e.CommandName == "Btnview")
            {
                ViewState["TempList"] = null;
                int MYTID = Convert.ToInt32(e.CommandArgument);
                EditSalesreturn(MYTID);
                readMode();

                string[] MYTRANSID = drpinvoicno.SelectedItem.ToString().Split('-');
                string AgainsID = MYTRANSID[1];
                ViewState["Print"] = MYTID;
                ViewState["AgainsID"] = AgainsID;
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
                string[] MYTRANSID = drpinvoicno.SelectedItem.ToString().Split('-');
                string AgainsID = MYTRANSID[1];
                Response.Redirect("SaleReturnHPSPrints.aspx?Tranjestion=" + MYTID + "&MYTRANSID=" + AgainsID);
            }
        }
        public void EditSalesreturn(int ID)
        {
            ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID);
            txtTrnsdoc.Text = OBJICTR_HD.TransDocNo.ToString() + " / " + ID.ToString();
            drpinvoicno.SelectedValue = OBJICTR_HD.RefTransID.ToString();
            txtTraNoHD.Text = OBJICTR_HD.ExtraSwitch2;
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
            //txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
            txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
            txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
            drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
            decimal Sum = Convert.ToDecimal(0);
            txtOHCostHD.Text = Sum.ToString();
            if(OBJICTR_HD.ExtraSwitch2!=null && OBJICTR_HD.ExtraSwitch2!="")
                drpReason.SelectedValue = OBJICTR_HD.ExtraSwitch2.ToString();
            BindDT(ID);
            ViewState["HDMYtrctionid"] = ID;
            ListItems.Visible = true;
            btnConfirmOrder.Visible = true;
            btnSaveData.Visible = true;
            WrietMode();
            string stast = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID).Status;
            if (stast == "SR")
            {
                readMode();
            }

        }
        public void EditSalesh(int ID)
        {
            ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID);
            txtTrnsdoc.Text = OBJICTR_HD.TransDocNo.ToString() + " / " + ID.ToString();
            ddlLocalForeign.SelectedValue = OBJICTR_HD.LF == "L" ? "8866" : "8867";
            int CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
            TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
            txtLocationSearch.Text = onj.COMPNAME1;
            HiddenField3.Value = CID.ToString();
            if (OBJICTR_HD.ACTIVITYCODE == null || OBJICTR_HD.ACTIVITYCODE == "0")
                ddlCrmAct.SelectedValue = "10607";
            else
                ddlCrmAct.SelectedValue = OBJICTR_HD.ACTIVITYCODE.ToString();
            if (OBJICTR_HD.USERBATCHNO == null || OBJICTR_HD.USERBATCHNO == "")
                txtBatchNo.Text = ID.ToString();
            else
                txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
            txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
            if (OBJICTR_HD.COMPANYID == null || OBJICTR_HD.COMPANYID == 0)
            { ddlCurrency.SelectedValue = "126"; }
            else
                ddlCurrency.SelectedValue = OBJICTR_HD.COMPANYID.ToString();
            ddlProjectNo.SelectedValue = "4";
            txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
            if (OBJICTR_HD.REFERENCE == null || OBJICTR_HD.REFERENCE == "")
                txtrefreshno.Text = ID.ToString();
            else
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
            decimal Sum = Convert.ToDecimal(0);
            txtOHCostHD.Text = Sum.ToString();
            BindDT(ID);
            ListItems.Visible = true;
            btnSaveData.Visible = false;
            btnConfirmOrder.Visible = false;
            //WrietMode();
            ddlCurrency.Enabled = ddlProjectNo.Enabled = drpterms.Enabled = ddlCrmAct.Enabled = ddlLocalForeign.Enabled = false;
            //rbtPsle.Enabled = drpinvoicno.Enabled = drpterms.Enabled = drpselsmen.Enabled = false;
            //txtOrderDate.Enabled = 
            txtLocationSearch.Enabled = false;
        }
        public void BindDT(int ID)
        {
            var List = DB.ICTR_DT.Where(p => p.MYTRANSID == ID && p.ACTIVE == true && p.TenentID == TID).ToList();

            Repeater2.DataSource = List;
            Repeater2.DataBind();
            decimal UInPictTotal = Convert.ToDecimal(0);
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

        public void clearItemsTab()
        {
            try
            {
                //ddlProduct.SelectedValue = ddlUOM.SelectedValue = "0";
                //txtDescription.Text = txtQuantity.Text = txtUPriceForeign.Text = txtUPriceLocal.Text = txtDiscount.Text = txtTax.Text = txtTotalCurrencyForeign.Text = txtTotalCurrencyLocal.Text = "";
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
                ddlLocalForeign.SelectedValue = ddlProjectNo.SelectedValue = ddlCurrency.SelectedValue = ddlCrmAct.SelectedValue = "0";
                txtLocationSearch.Text = txtOrderDate.Text = txtBatchNo.Text = txtTraNoHD.Text = txtNoteHD.Text = txtrefreshno.Text = txtOHCostHD.Text = txttotxl.Text = txtuintOhCost.Text = "";
                btnSubmit.Visible = btnConfirmOrder.Visible = true;
            }
            catch (System.Exception ex)
            {
                // ERPNew.WebMsgBox.Show(ex.Message);
            }
        }

        protected void listBin_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList dropBacthCode = (DropDownList)e.Item.FindControl("dropBacthCode");
            Classes.EcommAdminClass.getdropdown(dropBacthCode, TID, "", "", "", "TBLBIN");
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
                    //  txtUPriceForeign.Enabled = false;
                    //  txtUPriceLocal.Enabled = true;
                    ddlCurrency.SelectedValue = CUNTRYID.ToString();

                }
                else
                {
                    ddlLocalForeign.SelectedValue = "8867";
                    ddlCurrency.Enabled = true;
                    //    txtUPriceForeign.Enabled = true;
                    //    txtUPriceLocal.Enabled = false;
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

                int MAXID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                txttanno.Text = MAXID.ToString();

                ViewState["TempListICTR_DT"] = null;
                txtOrderDate.Text = DateTime.Now.ToString("dd-MMM-yy");
                Repeater2.DataSource = null;
                Repeater2.DataBind();
                //   btnAddItemsIn.Visible = false;
                //   btnAddDT.Visible = true;
                ListItems.Visible = false;
                //    panelRed.Visible = true;
                //   BindProduct();
            }
            //  btnSaveData.Visible = false;
            //    btnConfirmOrder.Visible = false;

        }

        protected void grdPO_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblspnoo = (Label)e.Item.FindControl("lblspnoo");
            LinkButton lnkbtnPurchase_Order = (LinkButton)e.Item.FindControl("lnkbtnPurchase_Order");
            LinkButton lnkbtndelete = (LinkButton)e.Item.FindControl("lnkbtndelete");

            if (lblspnoo.Text == "Final")
            {
                lnkbtnPurchase_Order.Visible = false;
                lnkbtndelete.Visible = false;
            }
            else
            {
                lnkbtnPurchase_Order.Visible = true;
                lnkbtndelete.Visible = true;
            }

        }

        protected void txtOHAmntLocal_TextChanged(object sender, EventArgs e)
        {

        }






        protected void btnaddrefresh_Click(object sender, EventArgs e)
        {
            int MYID = 0;
            ICIT_BR_ReferenceNo objICIT_BR_ReferenceNo = new ICIT_BR_ReferenceNo();
            int TCNID = Convert.ToInt32(txttanno.Text);
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





        protected void rbtPsle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CUNTRYID = Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            if (rbtPsle.SelectedValue == "1")
            {

                Drpdown.Visible = true;
                doptext.Visible = false;

            }
            else
            {
                Drpdown.Visible = true;
                doptext.Visible = false;
                //Drpdown.Visible = false;
                //doptext.Visible = true;
                //ddlLocalForeign.SelectedValue = "8866";
                //ddlCurrency.Enabled = false;
            }

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
        public string TransDocNO(int TransID)
        {
            //ICTR_HD OBJTransDoc = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == TransID);
            var mytransid = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == TransID).MYTRANSID;
            var TransDocNO = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == TransID).TransDocNo;
            string TransANDdoc = TransDocNO.ToString() + " / " + mytransid.ToString();
            return TransANDdoc;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int MyTransID = Convert.ToInt32(ViewState["Print"]);
            string AGAID = (ViewState["AgainsID"]).ToString();
            Response.Redirect("SaleReturnHPSPrints.aspx?Tranjestion=" + MyTransID + "&MYTRANSID=" + AGAID);
        }

        protected void CHKDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKDelete.Checked == true)
            {
                grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == false && (p.Status == "SR" || p.Status == "DSR")).OrderByDescending(p => p.TRANSDATE);
                grdPO.DataBind();
                for (int j = 0; j < grdPO.Items.Count; j++)
                {
                    LinkButton lnkbtndelete = (LinkButton)grdPO.Items[j].FindControl("lnkbtndelete");
                    LinkButton lnkbtnPurchase_Order = (LinkButton)grdPO.Items[j].FindControl("lnkbtnPurchase_Order");
                    LinkButton btnprient = (LinkButton)grdPO.Items[j].FindControl("btnprient");
                    lnkbtndelete.Visible = false;
                    lnkbtnPurchase_Order.Visible = false;
                    btnprient.Visible = true;
                }
            }
            else
            {
                BINDHDDATA();
                //grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && (p.Status == "SR" || p.Status == "DSR")).OrderByDescending(p => p.TRANSDATE);
                //grdPO.DataBind();
                //for (int i = 0; i < grdPO.Items.Count; i++)
                //{
                //    LinkButton btnprient = (LinkButton)grdPO.Items[i].FindControl("btnprient");
                //    LinkButton lnkbtndelete = (LinkButton)grdPO.Items[i].FindControl("lnkbtndelete");
                //    LinkButton lnkbtnPurchase_Order = (LinkButton)grdPO.Items[i].FindControl("lnkbtnPurchase_Order");
                //    Label lbluserid = (Label)grdPO.Items[i].FindControl("lbluserid");
                //    Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                //    Label lblActive = (Label)grdPO.Items[i].FindControl("lblActive");
                //    bool Act = Convert.ToBoolean(lblActive.Text);
                //    bool Active = Convert.ToBoolean(Act);
                //    if (Active == false)
                //    {
                //        lnkbtndelete.Visible = false;
                //        lnkbtnPurchase_Order.Visible = false;
                //        btnprient.Visible = true;
                //    }
                //    else
                //    {
                //        lnkbtndelete.Visible = true;
                //        lnkbtnPurchase_Order.Visible = true;
                //        btnprient.Visible = true;
                //    }                    
                //}
            }
        }
        //protected void BtnReload_Click(object sender, EventArgs e)
        //{
        //    List<ICTR_DT> ItemList = new List<ICTR_DT>();
        //    for (int i = 0; i < Repeater2.Items.Count; i++)
        //    {
        //        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
        //        Label lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription");
        //        Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
        //        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
        //        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
        //        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
        //        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
        //        Label lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT");
        //        Label lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER");
        //        Label lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT");
        //        TextBox txtreturnqty = (TextBox)Repeater2.Items[i].FindControl("txtreturnqty");
        //        string QTY = txtreturnqty.Text;
        //        if (QTY != "0")
        //        {
        //            ICTR_DT obj = new ICTR_DT();
        //            obj.MyProdID = Convert.ToInt64(lblProductNameItem.Text);
        //            obj.DESCRIPTION = lblDiscription.Text.ToString();
        //            obj.QUANTITY = Convert.ToInt32(lblDisQnty.Text);
        //            obj.UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
        //            obj.TAXPER = Convert.ToDecimal(lblTaxDis.Text);
        //            obj.AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
        //            obj.SWITCH1 = txtreturnqty.Text.ToString();
        //            ItemList.Add(obj);
        //        }
        //    }
        //    Repeater2.DataSource = ItemList;
        //    Repeater2.DataBind();

        //    decimal UInPictTotal = Convert.ToDecimal(0);
        //    decimal TottalSum = Convert.ToDecimal(ItemList.Sum(p => p.AMOUNT));
        //    txttotxl.Text = TottalSum.ToString();
        //    int QTUtotal = Convert.ToInt32(ItemList.Sum(p => p.QUANTITY));
        //    lblqtytotl.Text = QTUtotal.ToString();
        //    decimal UNPTOL = Convert.ToDecimal(ItemList.Sum(p => p.UNITPRICE));
        //    lblUNPtotl.Text = UNPTOL.ToString();
        //    decimal TaxTol = Convert.ToDecimal(ItemList.Sum(p => p.TAXPER));
        //    lblTextotl.Text = TaxTol.ToString();
        //    lblTotatotl.Text = TottalSum.ToString();
        //    if (UInPictTotal != 0 && TottalSum != 0)
        //    {
        //        decimal FInelOC = UInPictTotal / TottalSum;
        //        txtuintOhCost.Text = "@ " + FInelOC.ToString("N3");
        //    }

        //    ViewState["TempListICTR_DT"] = ItemList;
        //}
    }
}