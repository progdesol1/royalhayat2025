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
namespace Web.Sales
{
    public partial class Sales_Quotations : System.Web.UI.Page
    {
        static string Crypath = "";
        CallEntities DB = new CallEntities();
        List<Database.ICOVERHEADCOST> TempList = new List<Database.ICOVERHEADCOST>();
        PropertyFile objProFile = new PropertyFile();
        public static DataTable dt_PurQuat;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                BINDHDDATA();
                int MAXID = DB.ICTR_HD.Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Max(p => p.MYTRANSID) + 1) : 1;
                txtTraNoHD.Text = MAXID.ToString();
                ViewState["TempListICTR_DT"] = null;
            }
        }


        public void BindData()
        {
            ICOVERHEADCOST objICEXTRACOST = new ICOVERHEADCOST();
            TempList.Add(objICEXTRACOST);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string USERID = UID.ToString();

            ViewState["TempList"] = TempList;
            Repeater1.DataSource = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
            Repeater1.DataBind();
            //Repeater1.DataSource = null ;
            //Repeater1.DataBind();
            Classes.EcommAdminClass.getdropdown(ddlLocalForeign, TID, "LF", "OTH", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlCrmAct, TID, "ACTVTY", "LEADSTATUS", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlSupplier, TID, "116", "", "", "USER_MST");
            Classes.EcommAdminClass.getdropdown(ddlCurrency, TID, "1", "", "", "tblCOUNTRies");
            Classes.EcommAdminClass.getdropdown(ddlProjectNo, TID, "", "", "", "Eco_TBLPROJECT");
            Classes.EcommAdminClass.getdropdown(ddlProduct, TID, "", "", "", "Eco_TBLPRODUCT");
            Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "Eco_ICUOM");
        }
        public void BINDHDDATA()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MainTranType == "SQ" && p.ACTIVE == true);
            grdPO.DataBind();
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

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TempList = (List<Database.ICOVERHEADCOST>)ViewState["TempList"];
            ICOVERHEADCOST objICEXTRACOST = new ICOVERHEADCOST();
            TempList.Add(objICEXTRACOST);
            ViewState["TempList"] = TempList;
            Repeater1.DataSource = TempList;
            Repeater1.DataBind();

        }

        protected void lnkbtnItem_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (ViewState["HDMYtrctionid"] != null)
                {
                    int TransNo = Convert.ToInt32(ViewState["HDMYtrctionid"]);
                    ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TransNo);
                    OBJICTR_HD.TenentID = TID;
                    OBJICTR_HD.MYTRANSID = TransNo;
                    OBJICTR_HD.ToTenantID = objProFile.ToTenantID; ;
                    OBJICTR_HD.locationID = LID;
                    OBJICTR_HD.MainTranType = "SQ";
                    OBJICTR_HD.TransType = objProFile.TranType;
                    OBJICTR_HD.transid = objProFile.transid;
                    OBJICTR_HD.transsubid = objProFile.transsubid;
                    OBJICTR_HD.TranType = objProFile.TranType;
                    OBJICTR_HD.COMPID = objProFile.COMPID;
                    OBJICTR_HD.MYSYSNAME = "ECM";
                    OBJICTR_HD.CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    OBJICTR_HD.LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    OBJICTR_HD.PERIOD_CODE = objProFile.PERIOD_CODE;
                    OBJICTR_HD.ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    OBJICTR_HD.MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    OBJICTR_HD.USERBATCHNO = txtBatchNo.Text;
                    OBJICTR_HD.TOTQTY = Convert.ToInt32(txtQuantity.Text);
                    if (hidTotalCurrencyLocal.Value != "" && hidTotalCurrencyLocal.Value != "0")
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyLocal.Value);
                    else
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyForeign.Value);
                    OBJICTR_HD.AmtPaid = objProFile.AmtPaid;
                    OBJICTR_HD.PROJECTNO = ddlProjectNo.SelectedValue;
                    OBJICTR_HD.CounterID = objProFile.CounterID;
                    OBJICTR_HD.PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    OBJICTR_HD.JOID = objProFile.JOID;
                    OBJICTR_HD.TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    OBJICTR_HD.REFERENCE = txtRef.Text;
                    OBJICTR_HD.NOTES = txtNoteHD.Text;
                    OBJICTR_HD.GLPOST = objProFile.GLPOST;
                    OBJICTR_HD.GLPOST1 = objProFile.GLPOST1;
                    OBJICTR_HD.GLPOSTREF = objProFile.GLPOSTREF;
                    OBJICTR_HD.GLPOSTREF1 = objProFile.GLPOSTREF1;
                    OBJICTR_HD.ICPOST = objProFile.ICPOST;
                    OBJICTR_HD.ICPOSTREF = objProFile.ICPOSTREF;
                    OBJICTR_HD.USERID = UID.ToString();
                    OBJICTR_HD.ACTIVE = true;
                    //OBJICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //OBJICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    OBJICTR_HD.COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DB.SaveChanges();

                    var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
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
                        ICTR_DT objICTR_DT = new ICTR_DT();
                        objICTR_DT.TenentID = TID;
                        objICTR_DT.MYTRANSID = TransNo;//The cast to value type 'Int64' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.
                        int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MYID) + 1) : 1;
                        objICTR_DT.MYID = MYIDDT;
                        objICTR_DT.MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        objICTR_DT.REFTYPE = "LF";
                        objICTR_DT.REFSUBTYPE = "LF";
                        objICTR_DT.PERIOD_CODE = "5";
                        objICTR_DT.MYSYSNAME = "ECM";
                        objICTR_DT.JOID = 1;
                        objICTR_DT.JOBORDERDTMYID = 1;
                        objICTR_DT.ACTIVITYID = 1;
                        objICTR_DT.DESCRIPTION = lblDiscription.Text;
                        objICTR_DT.UOM = lblUOM.Text;
                        objICTR_DT.QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        objICTR_DT.UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        objICTR_DT.AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.OVERHEADAMOUNT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.BATCHNO = "1";
                        objICTR_DT.BIN_ID = 1;
                        objICTR_DT.BIN_TYPE = "Bin";
                        objICTR_DT.GRNREF = "2";
                        objICTR_DT.DISPER = Convert.ToDecimal(0.00);
                        objICTR_DT.DISAMT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.TAXAMT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.PROMOTIONAMT = Convert.ToDecimal(10.00);
                        objICTR_DT.GLPOST = "2";
                        objICTR_DT.GLPOST1 = "2";
                        objICTR_DT.GLPOSTREF1 = "2";
                        objICTR_DT.GLPOSTREF = "2";
                        objICTR_DT.ICPOST = "2";
                        objICTR_DT.ICPOSTREF = "2";
                        objICTR_DT.EXPIRYDATE = DateTime.Now;
                        objICTR_DT.ACTIVE = true;
                        DB.ICTR_DT.AddObject(objICTR_DT);
                        DB.SaveChanges();
                        ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        objICTR_DTEXT.TenentID = TID;
                        objICTR_DTEXT.MYTRANSID = TransNo;
                        int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MyID) + 1) : 1;
                        objICTR_DTEXT.MyID = DXMYID;

                        objICTR_DTEXT.MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Max(p => p.MyRunningSerial) + 1) : 1;
                        if (ddlCurrency.SelectedValue == "0")
                        { }
                        else
                        {
                            objICTR_DTEXT.CURRENTCONVRATE = 1;
                            objICTR_DTEXT.CURRENCY = ddlCurrency.SelectedItem.Text;
                            objICTR_DTEXT.OTHERCURAMOUNT = 1;
                        }

                        objICTR_DTEXT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
                        objICTR_DTEXT.UNITPRICE = objICTR_DT.UNITPRICE;
                        objICTR_DTEXT.AMOUNT = objICTR_DT.AMOUNT;
                        objICTR_DTEXT.QUANTITYDELIVERD = 0;
                        // objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        objICTR_DTEXT.ACCOUNTID = "1";
                        objICTR_DTEXT.GRNREF = "2";
                        objICTR_DTEXT.EXPIRYDATE = DateTime.Now;
                        objICTR_DTEXT.TransNo = 0;
                        objICTR_DTEXT.ACTIVE = true;
                        DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        DB.SaveChanges();

                    }

                    BINDHDDATA();
                    clearHDPanel();
                    clearItemsTab();
                }
                else
                {

                    int TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    ICTR_HD OBJICTR_HD = new ICTR_HD();
                    OBJICTR_HD.TenentID = TID;
                    OBJICTR_HD.MYTRANSID = TransNo;
                    OBJICTR_HD.ToTenantID = objProFile.ToTenantID; ;
                    OBJICTR_HD.locationID = LID;
                    OBJICTR_HD.MainTranType = "SQ";
                    OBJICTR_HD.TransType = objProFile.TranType;
                    OBJICTR_HD.transid = objProFile.transid;
                    OBJICTR_HD.transsubid = objProFile.transsubid;
                    OBJICTR_HD.TranType = objProFile.TranType;
                    OBJICTR_HD.COMPID = objProFile.COMPID;
                    OBJICTR_HD.MYSYSNAME = "ECM";
                    OBJICTR_HD.CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    OBJICTR_HD.LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    OBJICTR_HD.PERIOD_CODE = objProFile.PERIOD_CODE;
                    OBJICTR_HD.ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    OBJICTR_HD.MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    OBJICTR_HD.USERBATCHNO = txtBatchNo.Text;
                    OBJICTR_HD.TOTQTY = Convert.ToInt32(txtQuantity.Text);
                    if (hidTotalCurrencyLocal.Value != "" && hidTotalCurrencyLocal.Value != "0")
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyLocal.Value);
                    else
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyForeign.Value);
                    OBJICTR_HD.AmtPaid = objProFile.AmtPaid;
                    OBJICTR_HD.PROJECTNO = ddlProjectNo.SelectedValue;
                    OBJICTR_HD.CounterID = objProFile.CounterID;
                    OBJICTR_HD.PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    OBJICTR_HD.JOID = objProFile.JOID;
                    OBJICTR_HD.TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    OBJICTR_HD.REFERENCE = txtRef.Text;
                    OBJICTR_HD.NOTES = txtNoteHD.Text;
                    OBJICTR_HD.GLPOST = objProFile.GLPOST;
                    OBJICTR_HD.GLPOST1 = objProFile.GLPOST1;
                    OBJICTR_HD.GLPOSTREF = objProFile.GLPOSTREF;
                    OBJICTR_HD.GLPOSTREF1 = objProFile.GLPOSTREF1;
                    OBJICTR_HD.ICPOST = objProFile.ICPOST;
                    OBJICTR_HD.ICPOSTREF = objProFile.ICPOSTREF;
                    OBJICTR_HD.USERID = UID.ToString();
                    OBJICTR_HD.ACTIVE = true;
                    //OBJICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //OBJICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    OBJICTR_HD.COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DB.ICTR_HD.AddObject(OBJICTR_HD);
                    DB.SaveChanges();
                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
                        Label lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription");
                        Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
                        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
                        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
                        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
                        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
                        ICTR_DT objICTR_DT = new ICTR_DT();
                        objICTR_DT.TenentID = TID;
                        objICTR_DT.MYTRANSID = TransNo;//The cast to value type 'Int64' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.
                        int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MYID) + 1) : 1;
                        objICTR_DT.MYID = MYIDDT;
                        objICTR_DT.MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        objICTR_DT.REFTYPE = "LF";
                        objICTR_DT.REFSUBTYPE = "LF";
                        objICTR_DT.PERIOD_CODE = "5";
                        objICTR_DT.MYSYSNAME = "ECM";
                        objICTR_DT.JOID = 1;
                        objICTR_DT.JOBORDERDTMYID = 1;
                        objICTR_DT.ACTIVITYID = 1;
                        objICTR_DT.DESCRIPTION = lblDiscription.Text;
                        objICTR_DT.UOM = lblUOM.Text;
                        objICTR_DT.QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        objICTR_DT.UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        objICTR_DT.AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.OVERHEADAMOUNT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.BATCHNO = "1";
                        objICTR_DT.BIN_ID = 1;
                        objICTR_DT.BIN_TYPE = "Bin";
                        objICTR_DT.GRNREF = "2";
                        objICTR_DT.DISPER = Convert.ToDecimal(0.00);
                        objICTR_DT.DISAMT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.TAXAMT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.PROMOTIONAMT = Convert.ToDecimal(10.00);
                        objICTR_DT.GLPOST = "2";
                        objICTR_DT.GLPOST1 = "2";
                        objICTR_DT.GLPOSTREF1 = "2";
                        objICTR_DT.GLPOSTREF = "2";
                        objICTR_DT.ICPOST = "2";
                        objICTR_DT.ICPOSTREF = "2";
                        objICTR_DT.EXPIRYDATE = DateTime.Now;
                        objICTR_DT.ACTIVE = true;
                        DB.ICTR_DT.AddObject(objICTR_DT);
                        DB.SaveChanges();
                        ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        objICTR_DTEXT.TenentID = TID;
                        objICTR_DTEXT.MYTRANSID = TransNo;
                        int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MyID) + 1) : 1;
                        objICTR_DTEXT.MyID = DXMYID;

                        objICTR_DTEXT.MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Max(p => p.MyRunningSerial) + 1) : 1;
                        if (ddlCurrency.SelectedValue == "0")
                        { }
                        else
                        {
                            objICTR_DTEXT.CURRENTCONVRATE = 1;
                            objICTR_DTEXT.CURRENCY = ddlCurrency.SelectedItem.Text;
                            objICTR_DTEXT.OTHERCURAMOUNT = 1;
                        }

                        objICTR_DTEXT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
                        objICTR_DTEXT.UNITPRICE = objICTR_DT.UNITPRICE;
                        objICTR_DTEXT.AMOUNT = objICTR_DT.AMOUNT;
                        objICTR_DTEXT.QUANTITYDELIVERD = 0;
                        // objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        objICTR_DTEXT.ACCOUNTID = "1";
                        objICTR_DTEXT.GRNREF = "2";
                        objICTR_DTEXT.EXPIRYDATE = DateTime.Now;
                        objICTR_DTEXT.TransNo = 0;
                        objICTR_DTEXT.ACTIVE = true;
                        DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        DB.SaveChanges();
                        BINDHDDATA();
                        clearHDPanel();
                        clearItemsTab();
                    }
                }

                scope.Complete(); //  To commit.
            }

        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (ViewState["HDMYtrctionid"] != null)
                {
                    int TransNo = Convert.ToInt32(ViewState["HDMYtrctionid"]);
                    ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TransNo);
                    OBJICTR_HD.TenentID = TID;
                    OBJICTR_HD.MYTRANSID = TransNo;
                    OBJICTR_HD.ToTenantID = objProFile.ToTenantID; ;
                    OBJICTR_HD.locationID = LID;
                    OBJICTR_HD.MainTranType = "SQ";
                    OBJICTR_HD.TransType = objProFile.TranType;
                    OBJICTR_HD.transid = objProFile.transid;
                    OBJICTR_HD.transsubid = objProFile.transsubid;
                    OBJICTR_HD.TranType = objProFile.TranType;
                    OBJICTR_HD.COMPID = objProFile.COMPID;
                    OBJICTR_HD.MYSYSNAME = "ECM";
                    OBJICTR_HD.CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    OBJICTR_HD.LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    OBJICTR_HD.PERIOD_CODE = objProFile.PERIOD_CODE;
                    OBJICTR_HD.ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    OBJICTR_HD.MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    OBJICTR_HD.USERBATCHNO = txtBatchNo.Text;
                    OBJICTR_HD.TOTQTY = Convert.ToInt32(txtQuantity.Text);
                    if (hidTotalCurrencyLocal.Value != "" && hidTotalCurrencyLocal.Value != "0")
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyLocal.Value);
                    else
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyForeign.Value);
                    OBJICTR_HD.AmtPaid = objProFile.AmtPaid;
                    OBJICTR_HD.PROJECTNO = ddlProjectNo.SelectedValue;
                    OBJICTR_HD.CounterID = objProFile.CounterID;
                    OBJICTR_HD.PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    OBJICTR_HD.JOID = objProFile.JOID;
                    OBJICTR_HD.TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    OBJICTR_HD.REFERENCE = txtRef.Text;
                    OBJICTR_HD.NOTES = txtNoteHD.Text;
                    OBJICTR_HD.GLPOST = objProFile.GLPOST;
                    OBJICTR_HD.GLPOST1 = objProFile.GLPOST1;
                    OBJICTR_HD.GLPOSTREF = objProFile.GLPOSTREF;
                    OBJICTR_HD.GLPOSTREF1 = objProFile.GLPOSTREF1;
                    OBJICTR_HD.ICPOST = objProFile.ICPOST;
                    OBJICTR_HD.ICPOSTREF = objProFile.ICPOSTREF;
                    OBJICTR_HD.USERID = UID.ToString();
                    OBJICTR_HD.ACTIVE = true;
                    //OBJICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //OBJICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    OBJICTR_HD.COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DB.SaveChanges();

                    var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
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
                        ICTR_DT objICTR_DT = new ICTR_DT();
                        objICTR_DT.TenentID = TID;
                        objICTR_DT.MYTRANSID = TransNo;//The cast to value type 'Int64' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.
                        int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MYID) + 1) : 1;
                        objICTR_DT.MYID = MYIDDT;
                        objICTR_DT.MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        objICTR_DT.REFTYPE = "LF";
                        objICTR_DT.REFSUBTYPE = "LF";
                        objICTR_DT.PERIOD_CODE = "5";
                        objICTR_DT.MYSYSNAME = "ECM";
                        objICTR_DT.JOID = 1;
                        objICTR_DT.JOBORDERDTMYID = 1;
                        objICTR_DT.ACTIVITYID = 1;
                        objICTR_DT.DESCRIPTION = lblDiscription.Text;
                        objICTR_DT.UOM = lblUOM.Text;
                        objICTR_DT.QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        objICTR_DT.UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        objICTR_DT.AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.OVERHEADAMOUNT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.BATCHNO = "1";
                        objICTR_DT.BIN_ID = 1;
                        objICTR_DT.BIN_TYPE = "Bin";
                        objICTR_DT.GRNREF = "2";
                        objICTR_DT.DISPER = Convert.ToDecimal(0.00);
                        objICTR_DT.DISAMT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.TAXAMT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.PROMOTIONAMT = Convert.ToDecimal(10.00);
                        objICTR_DT.GLPOST = "2";
                        objICTR_DT.GLPOST1 = "2";
                        objICTR_DT.GLPOSTREF1 = "2";
                        objICTR_DT.GLPOSTREF = "2";
                        objICTR_DT.ICPOST = "2";
                        objICTR_DT.ICPOSTREF = "2";
                        objICTR_DT.EXPIRYDATE = DateTime.Now;
                        objICTR_DT.ACTIVE = true;
                        DB.ICTR_DT.AddObject(objICTR_DT);
                        DB.SaveChanges();
                        ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        objICTR_DTEXT.TenentID = TID;
                        objICTR_DTEXT.MYTRANSID = TransNo;
                        int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MyID) + 1) : 1;
                        objICTR_DTEXT.MyID = DXMYID;

                        objICTR_DTEXT.MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Max(p => p.MyRunningSerial) + 1) : 1;
                        if (ddlCurrency.SelectedValue == "0")
                        { }
                        else
                        {
                            objICTR_DTEXT.CURRENTCONVRATE = 1;
                            objICTR_DTEXT.CURRENCY = ddlCurrency.SelectedItem.Text;
                            objICTR_DTEXT.OTHERCURAMOUNT = 1;
                        }

                        objICTR_DTEXT.QUANTITY = objICTR_DT.QUANTITY;
                        objICTR_DTEXT.UNITPRICE = objICTR_DT.UNITPRICE;
                        objICTR_DTEXT.AMOUNT = objICTR_DT.AMOUNT;
                        objICTR_DTEXT.QUANTITYDELIVERD = 0;
                        // objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        objICTR_DTEXT.ACCOUNTID = "1";
                        objICTR_DTEXT.GRNREF = "2";
                        objICTR_DTEXT.EXPIRYDATE = DateTime.Now;
                        objICTR_DTEXT.TransNo = 0;
                        objICTR_DTEXT.ACTIVE = true;
                        DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        DB.SaveChanges();

                    }
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
                            ICOVERHEADCOST objICOVERHEADCOST = new ICOVERHEADCOST();
                            objICOVERHEADCOST.TenentID = TID;
                            objICOVERHEADCOST.MYTRANSID = TransNo;
                            objICOVERHEADCOST.MYID = DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == TransNo).Max(p => p.MYID) + 1) : 1;
                            objICOVERHEADCOST.OVERHEADCOSTID = Convert.ToInt32(ddlOHType.SelectedValue);
                            objICOVERHEADCOST.OLDCOST = Convert.ToDecimal(txtOHCostHD.Text);
                            objICOVERHEADCOST.NEWCOST = Convert.ToDecimal(txtOHAmntLocal.Text);
                            objICOVERHEADCOST.TOTQTY = TOTQTY;
                            objICOVERHEADCOST.TOTCOST = TOALSUM;
                            objICOVERHEADCOST.COMPANY_SEQUENCE = UID;
                            objICOVERHEADCOST.ICT_COMPANYID = UID;
                            objICOVERHEADCOST.COMPANYID = UID;
                            objICOVERHEADCOST.Note = txtOHNote.Text;
                            DB.ICOVERHEADCOSTs.AddObject(objICOVERHEADCOST);
                            DB.SaveChanges();
                        }
                    }
                    BINDHDDATA();
                    clearHDPanel();
                    clearItemsTab();
                }
                else
                {

                    int TransNo = Convert.ToInt32(txtTraNoHD.Text);
                    ICTR_HD OBJICTR_HD = new ICTR_HD();
                    OBJICTR_HD.TenentID = TID;
                    OBJICTR_HD.MYTRANSID = TransNo;
                    OBJICTR_HD.ToTenantID = objProFile.ToTenantID; ;
                    OBJICTR_HD.locationID = LID;
                    OBJICTR_HD.MainTranType = "SQ";
                    OBJICTR_HD.TransType = objProFile.TranType;
                    OBJICTR_HD.transid = objProFile.transid;
                    OBJICTR_HD.transsubid = objProFile.transsubid;
                    OBJICTR_HD.TranType = objProFile.TranType;
                    OBJICTR_HD.COMPID = objProFile.COMPID;
                    OBJICTR_HD.MYSYSNAME = "ECM";
                    OBJICTR_HD.CUSTVENDID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    OBJICTR_HD.LF = ddlLocalForeign.SelectedItem.Text == "Local" ? "L" : "F";
                    OBJICTR_HD.PERIOD_CODE = objProFile.PERIOD_CODE;
                    OBJICTR_HD.ACTIVITYCODE = ddlCrmAct.SelectedValue;
                    OBJICTR_HD.MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    OBJICTR_HD.USERBATCHNO = txtBatchNo.Text;
                    // OBJICTR_HD.TOTQTY = Convert.ToInt32(txtQuantity.Text);
                    if (hidTotalCurrencyLocal.Value != "" && hidTotalCurrencyLocal.Value != "0")
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyLocal.Value);
                    else
                        OBJICTR_HD.TOTAMT = Convert.ToDecimal(hidTotalCurrencyForeign.Value);
                    OBJICTR_HD.AmtPaid = objProFile.AmtPaid;
                    OBJICTR_HD.PROJECTNO = ddlProjectNo.SelectedValue;
                    OBJICTR_HD.CounterID = objProFile.CounterID;
                    OBJICTR_HD.PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    OBJICTR_HD.JOID = objProFile.JOID;
                    OBJICTR_HD.TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    OBJICTR_HD.REFERENCE = txtRef.Text;
                    OBJICTR_HD.NOTES = txtNoteHD.Text;
                    OBJICTR_HD.GLPOST = objProFile.GLPOST;
                    OBJICTR_HD.GLPOST1 = objProFile.GLPOST1;
                    OBJICTR_HD.GLPOSTREF = objProFile.GLPOSTREF;
                    OBJICTR_HD.GLPOSTREF1 = objProFile.GLPOSTREF1;
                    OBJICTR_HD.ICPOST = objProFile.ICPOST;
                    OBJICTR_HD.ICPOSTREF = objProFile.ICPOSTREF;
                    OBJICTR_HD.USERID = UID.ToString();
                    OBJICTR_HD.ACTIVE = true;
                    //OBJICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //OBJICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    OBJICTR_HD.COMPANYID = Convert.ToInt32(ddlCurrency.SelectedValue);
                    DB.ICTR_HD.AddObject(OBJICTR_HD);
                    DB.SaveChanges();
                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem");
                        Label lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription");
                        Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
                        Label lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty");
                        Label lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis");
                        Label lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency");
                        Label lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE");
                        ICTR_DT objICTR_DT = new ICTR_DT();
                        objICTR_DT.TenentID = TID;
                        objICTR_DT.MYTRANSID = TransNo;//The cast to value type 'Int64' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.
                        int MYIDDT = DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MYID) + 1) : 1;
                        objICTR_DT.MYID = MYIDDT;
                        objICTR_DT.MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        objICTR_DT.REFTYPE = "LF";
                        objICTR_DT.REFSUBTYPE = "LF";
                        objICTR_DT.PERIOD_CODE = "5";
                        objICTR_DT.MYSYSNAME = "ECM";
                        objICTR_DT.JOID = 1;
                        objICTR_DT.JOBORDERDTMYID = 1;
                        objICTR_DT.ACTIVITYID = 1;
                        objICTR_DT.DESCRIPTION = lblDiscription.Text;
                        objICTR_DT.UOM = lblUOM.Text;
                        objICTR_DT.QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        objICTR_DT.UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        objICTR_DT.AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.OVERHEADAMOUNT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.BATCHNO = "1";
                        objICTR_DT.BIN_ID = 1;
                        objICTR_DT.BIN_TYPE = "Bin";
                        objICTR_DT.GRNREF = "2";
                        objICTR_DT.DISPER = Convert.ToDecimal(0.00);
                        objICTR_DT.DISAMT = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.TAXAMT = Convert.ToDecimal(lblTotalCurrency.Text);
                        objICTR_DT.TAXPER = Convert.ToDecimal(lblTaxDis.Text);
                        objICTR_DT.PROMOTIONAMT = Convert.ToDecimal(10.00);
                        objICTR_DT.GLPOST = "2";
                        objICTR_DT.GLPOST1 = "2";
                        objICTR_DT.GLPOSTREF1 = "2";
                        objICTR_DT.GLPOSTREF = "2";
                        objICTR_DT.ICPOST = "2";
                        objICTR_DT.ICPOSTREF = "2";
                        objICTR_DT.EXPIRYDATE = DateTime.Now;
                        objICTR_DT.ACTIVE = true;
                        DB.ICTR_DT.AddObject(objICTR_DT);
                        DB.SaveChanges();
                        ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        objICTR_DTEXT.TenentID = TID;
                        objICTR_DTEXT.MYTRANSID = TransNo;
                        int DXMYID = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo).Max(p => p.MyID) + 1) : 1;
                        objICTR_DTEXT.MyID = DXMYID;

                        objICTR_DTEXT.MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TransNo && p.MyID == DXMYID).Max(p => p.MyRunningSerial) + 1) : 1;
                        if (ddlCurrency.SelectedValue == "0")
                        { }
                        else
                        {
                            objICTR_DTEXT.CURRENTCONVRATE = 1;
                            objICTR_DTEXT.CURRENCY = ddlCurrency.SelectedItem.Text;
                            objICTR_DTEXT.OTHERCURAMOUNT = 1;
                        }

                        objICTR_DTEXT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
                        objICTR_DTEXT.UNITPRICE = objICTR_DT.UNITPRICE;
                        objICTR_DTEXT.AMOUNT = objICTR_DT.AMOUNT;
                        objICTR_DTEXT.QUANTITYDELIVERD = 0;
                        // objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        objICTR_DTEXT.ACCOUNTID = "1";
                        objICTR_DTEXT.GRNREF = "2";
                        objICTR_DTEXT.EXPIRYDATE = DateTime.Now;
                        objICTR_DTEXT.TransNo = 0;
                        objICTR_DTEXT.ACTIVE = true;
                        DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        DB.SaveChanges();



                    }
                    var LIstTotal = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    decimal TOALSUM = Convert.ToDecimal(LIstTotal.Sum(p => p.AMOUNT));
                    int TOTQTY = Convert.ToInt32(LIstTotal.Sum(p => p.QUANTITY));
                    ICTR_HD obj = DB.ICTR_HD.Single(p => p.MYTRANSID == TransNo);
                    obj.TOTQTY = TOTQTY;
                    obj.TOTAMT = TOALSUM;
                    DB.SaveChanges();
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
                            ICOVERHEADCOST objICOVERHEADCOST = new ICOVERHEADCOST();
                            objICOVERHEADCOST.TenentID = TID;
                            objICOVERHEADCOST.MYTRANSID = TransNo;
                            objICOVERHEADCOST.MYID = DB.ICOVERHEADCOSTs.Count() > 0 ? Convert.ToInt32(DB.ICOVERHEADCOSTs.Max(p => p.MYID) + 1) : 1;
                            objICOVERHEADCOST.OVERHEADCOSTID = Convert.ToInt32(ddlOHType.SelectedValue);
                            if (txtOHCostHD.Text != "")
                                objICOVERHEADCOST.OLDCOST = Convert.ToDecimal(txtOHCostHD.Text);
                            objICOVERHEADCOST.NEWCOST = Convert.ToDecimal(txtOHAmntLocal.Text);
                            objICOVERHEADCOST.TOTQTY = TOTQTY;
                            objICOVERHEADCOST.TOTCOST = TOALSUM;
                            objICOVERHEADCOST.COMPANY_SEQUENCE = UID;
                            objICOVERHEADCOST.ICT_COMPANYID = UID;
                            objICOVERHEADCOST.COMPANYID = UID;
                            objICOVERHEADCOST.Note = txtOHNote.Text;
                            DB.ICOVERHEADCOSTs.AddObject(objICOVERHEADCOST);
                            DB.SaveChanges();
                        }
                    }


                    BINDHDDATA();
                    clearHDPanel();
                    clearItemsTab();
                }

                scope.Complete(); //  To commit.
            }

        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            hidUPriceLocal.Value = hidTotalCurrencyForeign.Value = hidTotalCurrencyLocal.Value = "";
            Response.Redirect(Request.RawUrl, false);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

            //dt_PurQuat = ViewState["ProDetails"] as DataTable;
            Crypath = Server.MapPath("Report/Quotation.rpt").ToString();
        }

        protected void lnkbtnPurchase_Order_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {

        }

        protected void lnbMCSUPo_Click(object sender, EventArgs e)
        {

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
                ddlOHType.DataSource = DB.ICEXTRACOSTs.Where(p => p.Active == "Y");
                ddlOHType.DataTextField = "OHNAME1";
                ddlOHType.DataValueField = "OVERHEADID";
                ddlOHType.DataBind();
                ddlOHType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Overhend Cost--", "0"));
                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    ddlOHType.SelectedValue = ID.ToString();
                }

            }
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int LFVALUE = Convert.ToInt32(ddlLocalForeign.SelectedValue);
            if (LFVALUE == 0)
            {
                string display = "Select Local/Foreign !";
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = display;
                return;
            }
            else
            {
                int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                TBLPRODUCT objEco_TBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID);
                string DIScrip = objEco_TBLPRODUCT.DescToprint;
                int MOU = Convert.ToInt32(objEco_TBLPRODUCT.UOM);
                ddlUOM.Enabled = false;
                txtDescription.Text = DIScrip.ToString();
                ddlUOM.SelectedValue = MOU.ToString();
                Boolean Perishable = Convert.ToBoolean(objEco_TBLPRODUCT.Perishable);
                Boolean MultiUOM = Convert.ToBoolean(objEco_TBLPRODUCT.MultiUOM);
                Boolean MultiColor = Convert.ToBoolean(objEco_TBLPRODUCT.MultiColor);
                Boolean MultiSize = Convert.ToBoolean(objEco_TBLPRODUCT.MultiSize);
                Boolean MultiBinStore = Convert.ToBoolean(objEco_TBLPRODUCT.MultiBinStore);
                Boolean Serialized = Convert.ToBoolean(objEco_TBLPRODUCT.Serialized);

                if (MultiUOM == Convert.ToBoolean(1))
                {
                    Button7.Visible = true;
                    List<Tbl_Multi_Color_Size_Mst> List = Classes.EcommAdminClass.getDataTbl_Multi_Color_Size_Mst(TID).Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiUOM" && p.RecValue != "All Colors").ToList();
                    lidtUom.DataSource = List;
                    lidtUom.DataBind();


                }
                if (Perishable == Convert.ToBoolean(1))
                {
                    Button4.Visible = true;

                }
                if (MultiColor == Convert.ToBoolean(1))
                {
                    Button2.Visible = true;
                    List<Tbl_Multi_Color_Size_Mst> List = Classes.EcommAdminClass.getDataTbl_Multi_Color_Size_Mst(TID).Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiColor" && p.RecValue != "All Colors").ToList();
                    listmulticoler.DataSource = List;
                    listmulticoler.DataBind();

                }
                if (MultiSize == Convert.ToBoolean(1))
                {
                    Button3.Visible = true;
                    List<Tbl_Multi_Color_Size_Mst> List = Classes.EcommAdminClass.getDataTbl_Multi_Color_Size_Mst(TID).Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiSize" && p.RecValue != "All Size").ToList();
                    listSize.DataSource = List;
                    listSize.DataBind();

                }
                if (MultiBinStore == Convert.ToBoolean(1))
                {
                    Button6.Visible = true;

                }
                if (Serialized == Convert.ToBoolean(1))
                {
                    Button8.Visible = false;
                    ViewState["SerializedTrue"] = 1;

                }




            }

        }
        List<Database.ICTR_DT> TempListICTR_DT = new List<Database.ICTR_DT>();
        protected void btnAddDT_Click(object sender, EventArgs e)
        {
            decimal TotalAmut = 0;
            decimal LUINTP = 0;
            if (ViewState["TempListICTR_DT"] == null)
            {

            }
            else
            {
                TempListICTR_DT = (List<Database.ICTR_DT>)ViewState["TempListICTR_DT"];
            }

            Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();
            objICTR_DT.MyProdID = Convert.ToInt32(ddlProduct.SelectedValue);
            objICTR_DT.DESCRIPTION = txtDescription.Text;
            objICTR_DT.UOM = ddlUOM.SelectedValue;
            objICTR_DT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
            if (txtUPriceLocal.Text == "" && txtTotalCurrencyLocal.Text == "")
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

            objICTR_DT.OVERHEADAMOUNT = Convert.ToDecimal(txtDiscount.Text);
            objICTR_DT.DISPER = Convert.ToDecimal(0.00);
            objICTR_DT.DISAMT = Convert.ToDecimal(txtDiscount.Text);
            objICTR_DT.TAXAMT = Convert.ToDecimal(txtTax.Text);
            objICTR_DT.TAXPER = Convert.ToDecimal(txtTax.Text);
            TempListICTR_DT.Add(objICTR_DT);
            ViewState["TempListICTR_DT"] = TempListICTR_DT;
            Repeater2.DataSource = TempListICTR_DT;
            Repeater2.DataBind();
            pnlItemAdd.Style.Add("display", "none");
            pnlItemShow.Style.Add("display", "block");
            clearItemsTab();
        }

        protected void grdPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID);
                ddlLocalForeign.SelectedItem.Text = OBJICTR_HD.LF == "L" ? "Local" : "Foreign";
                ddlSupplier.SelectedValue = OBJICTR_HD.CUSTVENDID.ToString();
                ddlCrmAct.SelectedValue = OBJICTR_HD.ACTIVITYCODE.ToString();
                txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
                txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd/MM/yyyy");
                if (OBJICTR_HD.COMPANYID==null )
                { }else 
                ddlCurrency.SelectedValue = OBJICTR_HD.COMPANYID.ToString();
                ddlProjectNo.SelectedValue = OBJICTR_HD.PROJECTNO.ToString();
                txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
                txtRef.Text = OBJICTR_HD.REFERENCE.ToString();
                txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
                var ListOvercost = DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == MYTID).ToList();
                ViewState["ListOverCost"] = ListOvercost;
                Repeater1.DataSource = (List<ICOVERHEADCOST>)ViewState["ListOverCost"];
                Repeater1.DataBind();
                //for (int i = 0; i < ListOvercost.Count-1; i++)
                //{
                //    int ID = Convert.ToInt32(ListOvercost[i].MYID);
                //    ICOVERHEADCOST obj = DB.ICOVERHEADCOST.Single(p => p.MYTRANSID == MYTID && p.MYID == ID);
                //    DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                //    TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                //    TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                //    TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                //    ddlOHType.SelectedValue = obj.OVERHEADCOSTID.ToString();
                //    txtOHAmntLocal.Text = obj.NEWCOST.ToString();
                //    txtOHNote.Text = obj.Note.ToString();

                //}
                pnlItemAdd.Style.Add("display", "none");
                pnlItemShow.Style.Add("display", "block");
                BindDT(MYTID);
                ViewState["HDMYtrctionid"] = MYTID;
                disable();
            }
            if (e.CommandName == "Delete")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID);
                OBJICTR_HD.ACTIVE = false;
                DB.SaveChanges();
                var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTID).ToList();
                for (int i = 0; i < List.Count - 1; i++)
                {
                    List[i].ACTIVE = false;
                    DB.SaveChanges();
                }
                var list1 = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTID).ToList();
                for (int i = 0; i < list1.Count - 1; i++)
                {
                    list1[i].ACTIVE = false;
                    DB.SaveChanges();
                }
                BindDT(MYTID);
                BINDHDDATA();
            }
        }

        public void BindDT(int ID)
        {
            Repeater2.DataSource = DB.ICTR_DT.Where(p => p.MYTRANSID == ID && p.ACTIVE == true);
            Repeater2.DataBind();
        }
        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDT")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);
                ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2);
                objICTR_DT.ACTIVE = false;
                DB.SaveChanges();
                BindDT(str1);
            }
            if (e.CommandName == "editDT")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);
                pnlItemAdd.Style.Add("display", "block");
                pnlItemShow.Style.Add("display", "none");
                ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2);
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
                ddlLocalForeign.SelectedValue = ddlSupplier.SelectedValue = ddlProjectNo.SelectedValue = ddlCurrency.SelectedValue = ddlCrmAct.SelectedValue = "0";
                txtOrderDate.Text = txtBatchNo.Text = txtRef.Text = txtTraNoHD.Text = txtNoteHD.Text = txtOHCostHD.Text = "";
                btnSubmit.Visible = btnConfirmOrder.Visible = true;
            }
            catch (System.Exception ex)
            {
                // ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        public void disable()
        {
            ddlProduct.Enabled = ddlUOM.Enabled = false;
            txtDescription.Enabled = txtQuantity.Enabled = txtUPriceForeign.Enabled = txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTax.Enabled = txtTotalCurrencyForeign.Enabled = txtTotalCurrencyLocal.Enabled = false;
            ddlLocalForeign.Enabled = ddlSupplier.Enabled = ddlProjectNo.Enabled = ddlCurrency.Enabled = ddlCrmAct.Enabled = false;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtRef.Enabled = txtTraNoHD.Enabled = txtNoteHD.Enabled = txtOHCostHD.Enabled = false;

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                DropDownList ddlOHType = (DropDownList)Repeater1.Items[i].FindControl("ddlOHType");
                TextBox txtOHAmntLocal = (TextBox)Repeater1.Items[i].FindControl("txtOHAmntLocal");
                TextBox txtOHNote = (TextBox)Repeater1.Items[i].FindControl("txtOHNote");
                TextBox txtOHAMT = (TextBox)Repeater1.Items[i].FindControl("txtOHAMT");
                ddlOHType.Enabled = txtOHAmntLocal.Enabled = txtOHNote.Enabled = txtOHAMT.Enabled = false;
            }
            btnSubmit.Visible = btnConfirmOrder.Visible = false;

        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (ViewState["SerializedTrue"] != null)
            {
                Button8.Visible = true;
                int PQTY = Convert.ToInt32(txtQuantity.Text);
                List<ICIT_BR_Serialize> ListICIT_BR_Serialize = new List<ICIT_BR_Serialize>();
                for (int i = 0; i <= PQTY - 1; i++)
                {
                    ICIT_BR_Serialize obj = new ICIT_BR_Serialize();
                    ListICIT_BR_Serialize.Add(obj);
                }
                listSerial.DataSource = ListICIT_BR_Serialize;
                listSerial.DataBind();
                ViewState["SerializedTrue"] = null;
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void linkMulticoler_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            for (int i = 0; i < listmulticoler.Items.Count; i++)
            {
                Label LblColoername = (Label)listmulticoler.Items[i].FindControl("LblColoername");
                Label LblMyid = (Label)listmulticoler.Items[i].FindControl("LblMyid");
                Label lblID = (Label)listmulticoler.Items[i].FindControl("lblID");
                TextBox txtcoloerqty = (TextBox)listmulticoler.Items[i].FindControl("txtcoloerqty");
                ICIT_BR_SIZECOLOR objICIT_BR_SIZECOLOR = new ICIT_BR_SIZECOLOR();
                objICIT_BR_SIZECOLOR.TenentID = TID;
                objICIT_BR_SIZECOLOR.MyProdID = Convert.ToInt32(LblMyid.Text);
                objICIT_BR_SIZECOLOR.period_code = "125015";
                objICIT_BR_SIZECOLOR.MySysName = "ECM";
                objICIT_BR_SIZECOLOR.UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                objICIT_BR_SIZECOLOR.SIZECODE = 999999998;
                objICIT_BR_SIZECOLOR.COLORID = Convert.ToInt32(lblID.Text);
                objICIT_BR_SIZECOLOR.MYTRANSID = Convert.ToInt32(txtTraNoHD.Text);
                objICIT_BR_SIZECOLOR.OpQty = Convert.ToInt32(txtcoloerqty.Text);
                if (txtRef.Text == "")
                { }
                else
                    objICIT_BR_SIZECOLOR.Reference = txtRef.Text;
                objICIT_BR_SIZECOLOR.Active = "Y";
                DB.ICIT_BR_SIZECOLOR.AddObject(objICIT_BR_SIZECOLOR);
                DB.SaveChanges();
            }
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            for (int i = 0; i < listSize.Items.Count; i++)
            {
                Label LblColoername = (Label)listSize.Items[i].FindControl("LblColoername");
                Label LblMyid = (Label)listSize.Items[i].FindControl("LblMyid");
                Label lblID = (Label)listSize.Items[i].FindControl("lblID");
                TextBox txtmultisze = (TextBox)listSize.Items[i].FindControl("txtmultisze");
                ICIT_BR_SIZECOLOR objICIT_BR_SIZECOLOR = new ICIT_BR_SIZECOLOR();
                objICIT_BR_SIZECOLOR.TenentID = TID;
                objICIT_BR_SIZECOLOR.MyProdID = Convert.ToInt32(LblMyid.Text);
                objICIT_BR_SIZECOLOR.period_code = "125015";
                objICIT_BR_SIZECOLOR.MySysName = "ECM";
                objICIT_BR_SIZECOLOR.UOM = Convert.ToInt32(ddlUOM.SelectedValue);
                objICIT_BR_SIZECOLOR.SIZECODE = Convert.ToInt32(lblID.Text);
                objICIT_BR_SIZECOLOR.COLORID =999999998 ;
                objICIT_BR_SIZECOLOR.MYTRANSID = Convert.ToInt32(txtTraNoHD.Text);
                objICIT_BR_SIZECOLOR.OpQty = Convert.ToInt32(txtmultisze.Text);
                if (txtRef.Text == "")
                { }
                else
                    objICIT_BR_SIZECOLOR.Reference = txtRef.Text;
                objICIT_BR_SIZECOLOR.Active = "Y";
                DB.ICIT_BR_SIZECOLOR.AddObject(objICIT_BR_SIZECOLOR);
                DB.SaveChanges();
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
             int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            for (int i = 0; i < listSerial.Items.Count; i++)
            {
                TextBox txtlistSerial = (TextBox)listSize.Items[i].FindControl("txtlistSerial");
                ICIT_BR_Serialize objICIT_BR_Serialize = new ICIT_BR_Serialize();
                objICIT_BR_Serialize.TenentID = TID;
                objICIT_BR_Serialize.MyProdID =Convert .ToInt32 ( ddlProduct.SelectedValue);
                objICIT_BR_Serialize.period_code = "125015";
                objICIT_BR_Serialize.MySysName = "ECM";
                objICIT_BR_Serialize.UOM =ddlUOM.SelectedValue;
                objICIT_BR_Serialize.Serial_Number = txtlistSerial.Text;
                objICIT_BR_Serialize.MyTransID =Convert .ToInt32 ( txtTraNoHD.Text);
                objICIT_BR_Serialize.Active = "Y";
                DB.ICIT_BR_Serialize.AddObject(objICIT_BR_Serialize);
                DB.SaveChanges();

            }
        }


    }
}