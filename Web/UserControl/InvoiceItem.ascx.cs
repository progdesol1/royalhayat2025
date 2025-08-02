using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;
using System.Net;
using System.Transactions;
using System.IO;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.Services;
using Web.Sales;

namespace Web.UserControl
{
    public partial class InvoiceItem : System.Web.UI.UserControl
    {
        CallEntities DB = new CallEntities();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        // List<ICIT_BR_BIN> ListICIT_BR_BIN = new List<ICIT_BR_BIN>();
        // List<ICIT_BR_Perishable> ListICIT_BR_Perishable = new List<ICIT_BR_Perishable>();
        PropertyFile objProFile = new PropertyFile();
        public static DataTable dt_PurQuat;
        List<Database.tblsetupsalesh> Listtblsetupsalesh = new List<tblsetupsalesh>();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
        MYCOMPANYSETUP objMyCompany = new MYCOMPANYSETUP();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, MYTRANSID, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        decimal CURRENTCONVRATE = Convert.ToDecimal(0);
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();

            }
            //ListItems.Visible = false;


        }
        public void visible(int Mode)
        {
            if (Mode == 0)
            {
                panelRed.Visible = false;
                ListItems.Visible = true;

            }
            if (Mode == 1)
            {
                panelRed.Visible = true;
                ListItems.Visible = false;
            }

        }
        public void GetMYTRANSID(int MYTRANSID1)
        {
            MYTRANSID = MYTRANSID1;
        }
        public void SessionLoad()
        {

            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            MYTRANSID = Convert.ToInt32(Session["Invoice"]);
            string tran = Session["Transid"].ToString();
            string[] id = tran.Split(',');
            Transid = Convert.ToInt32(id[0]);
            Transsubid = Convert.ToInt32(id[1]);
            List<Database.tblsetupsalesh> listtblsetupsaleshes = DB.tblsetupsaleshes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
            objtblsetupsalesh = listtblsetupsaleshes[0];
            btndiscartitems.Visible = ViewState["AddnewItems"] != null ? true : false;
            pnlSuccessMsg123.Visible = false;
        }
        public void FistTimeLoad()
        {

            string USERID = UID.ToString();

            if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            {
                Transid = Convert.ToInt32(Request.QueryString["transid"]);
                Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
            }
            objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            CURRENCY = objtblsetupsalesh.COUNTRYID.ToString();
            CURRENTCONVRATE = DB.tblCOUNTRies.SingleOrDefault(p => p.COUNTRYID == objtblsetupsalesh.COUNTRYID && p.TenentID == TID).CURRENTCONVRATE;
            FirstFlag = false;
            //Session["TempListICTR_DT"] = null;


            //BindDTui(MYTRANSID);

            // Remain ACM Work
        }
        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            //BindProduct();
            //ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;

            LinkButton11.Enabled = false;


            lblMode.Text = "UC-Add Mode";
            txtQuantity.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = "0";
            btnAddItemsIn.Visible = false;
            btnAddDT.Visible = false;//btnAddDT.Visible = true;yogesh20042017
            ListItems.Visible = false;
            panelRed.Visible = true;
            ViewState["AddnewItems"] = "Yes";
            btndiscartitems.Text = "Cancel";
            btnAddDT.Text = "Save";
            txtQuantity.Text = "";
            txtserchProduct.Text = "";
            lblmultiuom.Visible = false; //Come from MultiTransaction
            lblmulticolor.Visible = false;
            lblmultisize.Visible = false;
            lblmultiperishable.Visible = false;
            lblmultibinstore.Visible = false;
            lblmultiserialize.Visible = false;
            //GetProductDescription();
            //Classes.EcommAdminClass.getdropdown(ddlProduct, TID, LID.ToString(), "", "", "TBLPRODUCTWithICIT_BR");
            //string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
            //ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);

        }
        public int Pidalcodeui(DateTime Time)
        {
            int PODID = 0;
            var TBLPERIODS = DB.TBLPERIODS.Where(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID);
            if (TBLPERIODS.Count() > 0)
                PODID = Convert.ToInt32(TBLPERIODS.Single(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).PERIOD_CODE);
            return PODID;
        }


        public void BindProduct()
        {
            BindProductui();
            GetProductDescriptionui();

        }
        public void GetProductDescriptionui()
        {
            string[] counts2;
            counts2 = Classes.EcommAdminClass.BindItemCount(txtserchProduct, TID).ToString().Split('~');

            if (counts2[0] != "0")
            {
                lblProductCount.Text = "Product found " + counts2[0] + " as a Query.";
                int PID = Convert.ToInt32(counts2[1]);

                Classes.EcommAdminClass.BindProdu(PID, ddlUOM, txtDescription, txtserchProduct, TID);
                var ObjProduct = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                decimal PRiesh = Convert.ToDecimal(ObjProduct.msrp);
                Session["MultiTransactionProdObj"] = ObjProduct;
                txtUPriceLocal.Text = PRiesh.ToString();
                txtQuantity.Text = "1";

                TotalPriceui();


            }
            IsMinuscheck();


        }

        //protected void txtserchProduct_TextChanged(object sender, EventArgs e)
        //{
        //    ViewState["OriginalSearch"] = txtserchProduct.Text;
        //    //GetProductDescription();

        //}
        public void BindDTui(int MYTRANSID)
        {
            // create glogle function
            //var List = Classes.EcommAdminClass.getListICTR_DT(TID, MYTRANSID);
            //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
            panelRed.Visible = false;
            btnAddItemsIn.Visible = true;
            btnAddDT.Visible = false;
            //List<ICTR_DT> ListDate = ((List<Database.ICTR_DT>)Session["TempListICTR_DT"]).ToList();
            //Repeater2.DataSource = ListDate;
            //Repeater2.DataBind();

            List<TempListICTR_DT> ListDate = DB.TempListICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
            Repeater2.DataSource = ListDate;
            Repeater2.DataBind();

            lblSaveCount.Text = (ListDate.Count() + " Item Save").ToString();

            decimal TottalSum = Convert.ToDecimal(ListDate.Sum(p => p.AMOUNT));
            //txttotxl.Text = TottalSum.ToString();yogesh
            int QTUtotal = Convert.ToInt32(ListDate.Sum(p => p.QUANTITY));
            lblqtytotl.Text = QTUtotal.ToString();
            decimal UNPTOL = Convert.ToDecimal(ListDate.Sum(p => p.UNITPRICE));
            lblUNPtotl.Text = UNPTOL.ToString();
            decimal TaxTol = Convert.ToDecimal(ListDate.Sum(p => p.TAXPER));
            lblTotatotl.Text = TottalSum.ToString();

            Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            if (btnAddDT.Text == "Save")
            {
                ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            }

            var listPayHD = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Repeater3.DataBind();
            //Repeater3.DataSource = listPayHD;
            //Repeater3.DataBind();

            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                TextBox txtammunt1 = (TextBox)Repeater3.Items[i].FindControl("txtammunt1");
                TextBox txtrefresh1 = (TextBox)Repeater3.Items[i].FindControl("txtrefresh1");

                txtammunt1.Text = TottalSum.ToString();
            }


            //Session["TempListICTR_DT"] = ListDate;
            //panelRed.Visible = false;
        }
        protected void txtUPriceLocal_TextChanged(object sender, EventArgs e)
        {
            TotalPriceui();
        }
        public void TotalPriceui()
        {
            string str = "";
            decimal DICUNTOTAL = 0;
            decimal Total = 0;
            decimal Priesh = 0;
            decimal DICUNT = 0;
            decimal qty = Convert.ToDecimal(txtQuantity.Text);
            if (txtDiscount.Text.Contains("%"))
            {
                str = txtDiscount.Text.Replace('%', ' ');
                Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                Total = qty * Priesh;
                DICUNT = Convert.ToDecimal(str);
                DICUNTOTAL = Total - (Total * (DICUNT / 100));
                txtTotalCurrencyLocal.Text = DICUNTOTAL.ToString();
            }
            else
            {
                str = txtDiscount.Text;
                Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                Total = qty * Priesh;
                DICUNT = Convert.ToDecimal(str);
                DICUNTOTAL = Total - DICUNT;
                txtTotalCurrencyLocal.Text = DICUNTOTAL.ToString();
            }
        }
        public void IsMinuscheckui()
        {
            if (objtblsetupsalesh.AllowMinusQty == false)
            {
                //lblitemsearch.Text;
                if (ViewState["OriginalSearch"] != null)
                    txtserchProduct.Text = ViewState["OriginalSearch"].ToString();
                string counts1;
                counts1 = Classes.EcommAdminClass.BindAddvanserchBR(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID, LID, lblAvailableQty, "en-US").ToString();
                if (counts1 == "1")
                    lblitemsearch.Visible = false;
                else
                {
                    lblitemsearch.Text = "Search found " + counts1 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                }
            }
            else
            {
                string counts2;
                counts2 = Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                if (counts2 == "1")
                    lblitemsearch.Visible = false;
                else
                {
                    lblitemsearch.Text = "Search found " + counts2 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                }
                int PID = Convert.ToInt32(ddlProduct.SelectedValue);

                if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID).Count() > 0)
                    lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID).OnHand.ToString();

            }
        }
        List<Database.TempListICTR_DT> TempListICTR_DT = new List<Database.TempListICTR_DT>();
        protected void btnAddDT_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);

                int ICTR_HDCount = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count();
                string GLPOST = objProFile.GLPOST;
                string GLPOST1 = objProFile.GLPOST1;
                string ICPOST = objProFile.ICPOST;
                string TransType = objProFile.TranType;
                string TranType = "POS Invoice";// Out Qty On Product
                string MainTranType = "O";// Out Qty On Product
                if (ICTR_HDCount < 1)
                {
                    Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();
                    objICTR_HD.TenentID = TID;
                    objICTR_HD.MYTRANSID = MYTRANSID;
                    objICTR_HD.locationID = LID;
                    objICTR_HD.transid = 4101;
                    objICTR_HD.transsubid = 410103;
                    objICTR_HD.MainTranType = MainTranType;
                    objICTR_HD.TransType = TranType;
                    objICTR_HD.TranType = TranType;
                    objICTR_HD.TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    objICTR_HD.TOTQTY = Convert.ToInt32(lblqtytotl.Text);
                    objICTR_HD.CUSTVENDID = 2114;
                    objICTR_HD.Status = "DSO";
                    objICTR_HD.USERBATCHNO = "123";
                    objICTR_HD.TRANSDATE = DateTime.Now;
                    objICTR_HD.GLPOST = GLPOST;
                    objICTR_HD.GLPOST1 = GLPOST1;
                    objICTR_HD.ICPOST = ICPOST;
                    objICTR_HD.ExtraField2 = "1";
                    objICTR_HD.ACTIVE = false;
                    DB.ICTR_HD.AddObject(objICTR_HD);
                    DB.SaveChanges();
                }
                else
                {
                    Database.ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID);


                    objICTR_HD.TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    objICTR_HD.TOTQTY = Convert.ToInt32(lblqtytotl.Text);
                    objICTR_HD.CUSTVENDID = 2114;
                    objICTR_HD.Status = "DSO";
                    objICTR_HD.USERBATCHNO = "123";
                    objICTR_HD.TRANSDATE = DateTime.Now;

                    objICTR_HD.ExtraField2 = "1";
                    objICTR_HD.ACTIVE = false;
                    DB.SaveChanges();
                }


                if (btnAddDT.Text == "Save")
                {
                    ListItems.Visible = true;
                    // btnPenel.Visible = true;
                    ViewState["btnPenel"] = true;

                    // tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);




                    decimal TotalAmut = 0;
                    decimal LUINTP = 0;
                    bool fleg = true;
                    var TemplistICTR_DT = DB.TempListICTR_DT.Where(p => p.TenentID == TID);//create By Parimal
                    int MYIDCUNT = TemplistICTR_DT.Count() > 0 ? Convert.ToInt32(TemplistICTR_DT.Max(p => p.MYID) + 1) : 1;
                    if (Session["Invoice"] != null)
                    {
                        var ListTotle = DB.ICTR_HD.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).ToList();
                        lblqtytotl.Text = Convert.ToInt32(ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTQTY).ToString();
                        lblUNPtotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                        lblTotatotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                    }
                    List<TempListICTR_DT> ListDate = DB.TempListICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    if (ListDate.Count() > 0)
                    {
                        TempListICTR_DT = ListDate;
                        fleg = false;
                        //MYIDCUNT += MYIDCUNT + 1;


                    }
                    //if (ViewState["DTTRCTIONID"] != null && ViewState["DTMYID"] != null)
                    //{
                    //    int str1 = Convert.ToInt32(ViewState["DTTRCTIONID"]);
                    //    int str2 = Convert.ToInt32(ViewState["DTMYID"]);
                    //    TempListICTR_DT = ((List<Database.ICTR_DT>)Session["TempListICTR_DT"]).Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).ToList();
                    //    var List = listICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2).ToList();
                    //    if (List.Count() > 0)
                    //    {
                    //        foreach (Database.ICTR_DT item in List)
                    //        {
                    //            DB.ICTR_DT.DeleteObject(item);
                    //            DB.SaveChanges();
                    //        }
                    //    }
                    //    for (int i = TempListICTR_DT.Count - 1; i >= 0; i--)
                    //    {
                    //        TempListICTR_DT.RemoveAt(i);
                    //    }
                    //    BindDTui(str1);
                    //    ViewState["DTTRCTIONID"] = null;
                    //    ViewState["DTMYID"] = null;

                    //}

                    Database.TempListICTR_DT objICTR_DT = new Database.TempListICTR_DT();
                    objICTR_DT.TenentID = TID;
                    objICTR_DT.MYTRANSID = MYTRANSID; //Convert.ToInt32(txtTraNoHD.Text);//yogesh
                    objICTR_DT.MYID = MYIDCUNT;
                    objICTR_DT.MyProdID = Convert.ToInt32(ddlProduct.SelectedValue);
                    objICTR_DT.DESCRIPTION = txtDescription.Text;
                    objICTR_DT.UOM = ddlUOM.SelectedValue;
                    objICTR_DT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
                    LUINTP = Convert.ToDecimal(txtUPriceLocal.Text);
                    TotalAmut = Convert.ToDecimal(txtTotalCurrencyLocal.Text);
                    objICTR_DT.UNITPRICE = LUINTP;
                    objICTR_DT.AMOUNT = TotalAmut;

                    string str = "";
                    str = txtDiscount.Text.Replace('%', ' ');
                    decimal TexAmunt = 0;
                    decimal DICUNTOTAL = 0;
                    decimal Total = 0;
                    decimal Priesh = 0;
                    decimal DICUNT = 0;
                    decimal DicuntLest = 0;
                    decimal qty = Convert.ToDecimal(txtQuantity.Text);
                    if (txtDiscount.Text.Contains("%"))
                    {
                        Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                        Total = qty * Priesh;
                        DICUNT = Convert.ToDecimal(str);
                        DICUNTOTAL = Total - (Total * (DICUNT / 100));
                        DicuntLest = Total - DICUNTOTAL;
                        objICTR_DT.DISPER = Convert.ToDecimal(str);
                        objICTR_DT.DISAMT = Convert.ToDecimal(DicuntLest);
                        TexAmunt = DICUNTOTAL / 100;
                        objICTR_DT.TAXAMT = TexAmunt;
                    }
                    else
                    {
                        Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                        Total = qty * Priesh;
                        DICUNT = Convert.ToDecimal(str);
                        DICUNTOTAL = Total - DICUNT;
                        DicuntLest = Total - DICUNTOTAL;
                        objICTR_DT.DISPER = Convert.ToDecimal(0.000);
                        objICTR_DT.DISAMT = Convert.ToDecimal(str);
                        decimal DicuntAmut = Convert.ToDecimal(str);
                        DicuntLest = Total - DicuntAmut;
                        TexAmunt = DicuntLest / 100;
                        objICTR_DT.TAXAMT = TexAmunt;
                    }
                    objICTR_DT.GLPOST = "2";
                    objICTR_DT.GLPOST1 = "2";
                    objICTR_DT.ICPOST = "2";
                    objICTR_DT.ACTIVE = true;
                    
                    TempListICTR_DT.Add(objICTR_DT);
                    DB.TempListICTR_DT.AddObject(objICTR_DT);
                    DB.SaveChanges();

                    //Session["TempListICTR_DT"] = TempListICTR_DT;
                    Repeater2.DataSource = TempListICTR_DT;
                    Repeater2.DataBind();

                    //Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
                    //TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
                    ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
                    int ItemCount = TempListICTR_DT.Count();
                    lblSaveCount.Text = (ItemCount + "Item Save").ToString();
                    Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                    Repeater3.DataBind();
                    //BindDTui(str1);
                    clearItemsTab();
                    btnAddItemsIn.Visible = true;
                    btnAddDT.Visible = false;
                    int QTY1 = Convert.ToInt32(objICTR_DT.QUANTITY);
                    decimal UNP1 = Convert.ToDecimal(objICTR_DT.UNITPRICE);
                    decimal TXP1 = Convert.ToDecimal(objICTR_DT.TAXPER);
                    decimal GTL1 = Convert.ToDecimal(objICTR_DT.AMOUNT);
                    if (fleg == true)
                    {
                        lblqtytotl.Text = QTY1.ToString();
                        lblUNPtotl.Text = UNP1.ToString();
                        lblTotatotl.Text = GTL1.ToString();
                        //txttotxl.Text = GTL1.ToString();
                        for (int i = 0; i < Repeater3.Items.Count; i++)
                        {
                            TextBox txtammunt1 = (TextBox)Repeater3.Items[i].FindControl("txtammunt1");
                            txtammunt1.Text = GTL1.ToString();
                        }
                    }
                    else
                    {
                        int QTY = Convert.ToInt32(lblqtytotl.Text);
                        decimal UNP = Convert.ToDecimal(lblUNPtotl.Text);
                        decimal GTL = Convert.ToDecimal(lblTotatotl.Text);
                        int QTYTOtal = QTY + QTY1;
                        decimal UPTOTAL = UNP + UNP1;
                        decimal GTLTOL = GTL + GTL1;
                        lblqtytotl.Text = QTYTOtal.ToString();
                        lblUNPtotl.Text = UPTOTAL.ToString();
                        lblTotatotl.Text = GTLTOL.ToString();
                        //txttotxl.Text = GTLTOL.ToString();
                        for (int i = 0; i < Repeater3.Items.Count; i++)
                        {
                            TextBox txtammunt1 = (TextBox)Repeater3.Items[i].FindControl("txtammunt1");
                            txtammunt1.Text = GTLTOL.ToString();
                        }
                    }

                    BindDTui(MYTRANSID);

                    List<Database.ICTRPayTerms_HD> ListPayTemp = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                    if (ListPayTemp.Count() < 1)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {

                            DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");

                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
                            {
                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();

                                int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
                                int AccountID = 0;
                                Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                                if (PaymentTermsId == 1250001)
                                {
                                    string TransDocNo = "";
                                    var listtbltranssubtypes1 = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                                    if (listtbltranssubtypes1.Count() > 0)
                                    {
                                        int SirialNO1 = (Convert.ToInt32(listtbltranssubtypes1.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1);
                                        string SirialNO11 = SirialNO1.ToString();
                                        TransDocNo = SirialNO11;
                                    }
                                    else
                                        TransDocNo = "";
                                    //Parimal
                                    string InvoiceNo = TransDocNo;

                                    txtrefresh.Text = MYTRANSID + "," + InvoiceNo;


                                    ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                    objICTRPayTerms_HD.TenentID = TID;
                                    objICTRPayTerms_HD.LocationID = LID;
                                    objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
                                    objICTRPayTerms_HD.CashBankChequeID = 0;
                                    objICTRPayTerms_HD.CounterID = UID.ToString();

                                    objICTRPayTerms_HD.AccountID = AccountID;
                                    objICTRPayTerms_HD.CRUP_ID = 1;

                                    objICTRPayTerms_HD.Amount = Amount;
                                    objICTRPayTerms_HD.ReferenceNo = InvoiceNo;
                                    objICTRPayTerms_HD.ApprovalID = MYTRANSID.ToString();
                                    objICTRPayTerms_HD.Draft = false;


                                    DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                    DB.SaveChanges();
                                }
                                else
                                {
                                    ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                    objICTRPayTerms_HD.TenentID = TID;
                                    objICTRPayTerms_HD.LocationID = LID;
                                    objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
                                    objICTRPayTerms_HD.CashBankChequeID = 0;
                                    objICTRPayTerms_HD.CounterID = UID.ToString();
                                    objICTRPayTerms_HD.AccountID = AccountID;
                                    objICTRPayTerms_HD.CRUP_ID = 1;
                                    objICTRPayTerms_HD.Amount = Amount;
                                    objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                    objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                    objICTRPayTerms_HD.Draft = false;
                                    DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                    DB.SaveChanges();
                                }


                                //Classes.EcommAdminClass.insertICTRPayTerms_HD_TEMP(TID, MYTRANSID, PaymentTermsId, UID.ToString(), LID, 0, Amount, IDRefresh, null, null, null, AccountID, 1, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
                            {
                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();

                                int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
                                int AccountID = 0;
                                Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                                if (PaymentTermsId == 1250001)
                                {
                                    string TransDocNo = "";
                                    var ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                                    Database.ICTR_HD OBJHD = ListHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                                    if (OBJHD.TransDocNo != null)
                                    {
                                        TransDocNo = OBJHD.TransDocNo.ToString();
                                    }
                                    else
                                    {
                                        var listtbltranssubtypes1 = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                                        if (listtbltranssubtypes1.Count() > 0)
                                        {
                                            int SirialNO1 = (Convert.ToInt32(listtbltranssubtypes1.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1);
                                            string SirialNO11 = SirialNO1.ToString();
                                            TransDocNo = SirialNO11;
                                        }
                                        else
                                            TransDocNo = "";
                                    }
                                    //Parimal
                                    string InvoiceNo = TransDocNo;

                                    txtrefresh.Text = MYTRANSID + "," + InvoiceNo;

                                    Database.ICTRPayTerms_HD OBJTemp = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);
                                    OBJTemp.PaymentTermsId = PaymentTermsId;
                                    OBJTemp.ReferenceNo = InvoiceNo;
                                    OBJTemp.ApprovalID = MYTRANSID.ToString();
                                    OBJTemp.Amount = Amount;
                                    DB.SaveChanges();

                                }
                                else
                                {
                                    Database.ICTRPayTerms_HD OBJTemp = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);
                                    OBJTemp.PaymentTermsId = PaymentTermsId;
                                    OBJTemp.ReferenceNo = IDRefresh;
                                    OBJTemp.ApprovalID = IdApprouv;
                                    OBJTemp.Amount = Amount;
                                    DB.SaveChanges();
                                }




                            }
                        }

                    }



                    string UserTotal = lblqtytotl.Text + "," + lblUNPtotl.Text + "," + lblTotatotl.Text;
                    Session["UserTotal"] = UserTotal.ToString();
                    ListItems.Visible = true;
                    panelRed.Visible = false;
                    MYTRANSID = Convert.ToInt32(objICTR_DT.MYTRANSID);

                }
                if (btnAddDT.Text == "Update")
                {
                    ListItems.Visible = true;
                    // btnPenel.Visible = true;
                    ViewState["btnPenel"] = true;
                    decimal TotalAmut = 0;
                    decimal LUINTP = 0;
                    bool fleg = true;
                    //var listICTR_DT = DB.ICTR_DT.Where(p => p.TenentID == TID);//create By Parimal
                    var TemplistICTR_DT = DB.TempListICTR_DT.Where(p => p.TenentID == TID);//create By Parimal
                    //int MYIDCUNT = TemplistICTR_DT.Count() > 0 ? Convert.ToInt32(TemplistICTR_DT.Max(p => p.MYID) + 1) : 1;
                    //int MYIDCUNT = listICTR_DT.Count() > 0 ? Convert.ToInt32(listICTR_DT.Max(p => p.MYID) + 1) : 1;
                    //if (Session["Invoice"] != null)
                    //{
                    //    var ListTotle = DB.ICTR_HD.Where(p => p.MYTRANSID == MYTRANSID).ToList();
                    //    lblqtytotl.Text = Convert.ToInt32(ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTQTY).ToString();
                    //    lblUNPtotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                    //    lblTotatotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                    //}

                    List<TempListICTR_DT> ListDate = DB.TempListICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    if (ListDate.Count() > 0)
                    {
                        TempListICTR_DT = ListDate;
                        fleg = false;
                        //MYIDCUNT += MYIDCUNT + 1;


                    }
                    if (ViewState["DTTRCTIONID"] != null && ViewState["DTMYID"] != null)
                    {
                        int str1 = Convert.ToInt32(ViewState["DTTRCTIONID"]);
                        int str2 = Convert.ToInt32(ViewState["DTMYID"]);
                        //TempListICTR_DT = DB.TempListICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).ToList();
                        //var List = TempListICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2).ToList();
                        //if (List.Count() > 0)
                        //{
                        //    foreach (Database.TempListICTR_DT item in List)
                        //    {
                        //        DB.TempListICTR_DT.DeleteObject(item);
                        //        DB.SaveChanges();
                        //    }
                        //}
                        //for (int i = TempListICTR_DT.Count - 1; i >= 0; i--)
                        //{
                        //    TempListICTR_DT.RemoveAt(i);
                        //}
                        BindDTui(str1);
                        ViewState["DTTRCTIONID"] = null;
                        ViewState["DTMYID"] = null;

                    }
                    string[] ID = ViewState["ItemeditDTMYTRANSIDMYID"].ToString().Split(',');
                    MYTRANSID = Convert.ToInt32(ID[0]);
                    int MYID = Convert.ToInt32(ID[1]);
                    Database.TempListICTR_DT objICTR_DT = DB.TempListICTR_DT.Single(p => p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.TenentID == TID);
                    //objICTR_DT.MYTRANSID = 1; //Convert.ToInt32(txtTraNoHD.Text);//yogesh
                    //objICTR_DT.MYID = MYIDCUNT;
                    //objICTR_DT.MyProdID = Convert.ToInt32(ddlProduct.SelectedValue);
                    decimal TmpUNITPRICE = Convert.ToDecimal(objICTR_DT.UNITPRICE);
                    decimal TmpAMOUNT = Convert.ToDecimal(objICTR_DT.AMOUNT);
                    int TmpQUANTITY = Convert.ToInt32(objICTR_DT.QUANTITY);
                    objICTR_DT.DESCRIPTION = txtDescription.Text;
                    objICTR_DT.UOM = ddlUOM.SelectedValue;
                    objICTR_DT.QUANTITY = Convert.ToInt32(txtQuantity.Text);
                    LUINTP = Convert.ToDecimal(txtUPriceLocal.Text);
                    TotalAmut = Convert.ToDecimal(txtTotalCurrencyLocal.Text);
                    objICTR_DT.UNITPRICE = LUINTP;
                    objICTR_DT.AMOUNT = TotalAmut;
                    string str = "";
                    str = txtDiscount.Text.Replace('%', ' ');
                    decimal TexAmunt = 0;
                    decimal DICUNTOTAL = 0;
                    decimal Total = 0;
                    decimal Priesh = 0;
                    decimal DICUNT = 0;
                    decimal DicuntLest = 0;
                    decimal qty = Convert.ToDecimal(txtQuantity.Text);
                    if (txtDiscount.Text.Contains("%"))
                    {
                        Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                        Total = qty * Priesh;
                        DICUNT = Convert.ToDecimal(str);
                        DICUNTOTAL = Total - (Total * (DICUNT / 100));
                        DicuntLest = Total - DICUNTOTAL;
                        objICTR_DT.DISPER = Convert.ToDecimal(str);
                        objICTR_DT.DISAMT = Convert.ToDecimal(DicuntLest);
                        TexAmunt = DICUNTOTAL / 100;
                        objICTR_DT.TAXAMT = TexAmunt;
                    }
                    else
                    {
                        Priesh = Convert.ToDecimal(txtUPriceLocal.Text);
                        Total = qty * Priesh;
                        DICUNT = Convert.ToDecimal(str);
                        DICUNTOTAL = Total - DICUNT;
                        DicuntLest = Total - DICUNTOTAL;
                        objICTR_DT.DISPER = Convert.ToDecimal(0.000);
                        objICTR_DT.DISAMT = Convert.ToDecimal(str);
                        decimal DicuntAmut = Convert.ToDecimal(str);
                        DicuntLest = Total - DicuntAmut;
                        TexAmunt = DicuntLest / 100;
                        objICTR_DT.TAXAMT = TexAmunt;
                    }

                    TempListICTR_DT.Add(objICTR_DT);
                    // Session["TempListICTR_DT"] = TempListICTR_DT;
                    //Repeater2.DataSource = TempListICTR_DT;
                    //Repeater2.DataBind();
                    DB.SaveChanges();
                    //Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
                    //TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
                    //ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
                    //Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                    //Repeater3.DataBind();
                    //BindDTui(str1);
                    clearItemsTab();
                    btnAddItemsIn.Visible = true;
                    btnAddDT.Visible = false;
                    int QTY1 = Convert.ToInt32(objICTR_DT.QUANTITY);
                    decimal UNP1 = Convert.ToDecimal(objICTR_DT.UNITPRICE);
                    decimal TXP1 = Convert.ToDecimal(objICTR_DT.TAXPER);
                    decimal GTL1 = Convert.ToDecimal(objICTR_DT.AMOUNT);
                    if (fleg == true)
                    {
                        lblqtytotl.Text = QTY1.ToString();
                        lblUNPtotl.Text = UNP1.ToString();
                        lblTotatotl.Text = GTL1.ToString();
                        //txttotxl.Text = GTL1.ToString();
                        //for (int i = 0; i < Repeater3.Items.Count; i++)
                        //{
                        //    TextBox txtammunt1 = (TextBox)Repeater3.Items[i].FindControl("txtammunt1");
                        //    txtammunt1.Text = GTL1.ToString();
                        //}
                    }
                    else
                    {
                        int QTY = Convert.ToInt32(lblqtytotl.Text);
                        decimal UNP = Convert.ToDecimal(lblUNPtotl.Text);
                        decimal GTL = Convert.ToDecimal(lblTotatotl.Text);

                        QTY = QTY - TmpQUANTITY;
                        QTY = QTY + Convert.ToInt32(txtQuantity.Text);

                        UNP = UNP - TmpUNITPRICE;
                        UNP = UNP + LUINTP;

                        GTL = GTL - TmpAMOUNT;
                        GTL = GTL + TotalAmut;

                        int QTYTOtal = QTY;
                        decimal UPTOTAL = UNP;
                        decimal GTLTOL = GTL;
                        lblqtytotl.Text = QTYTOtal.ToString();
                        lblUNPtotl.Text = UPTOTAL.ToString();
                        lblTotatotl.Text = GTLTOL.ToString();
                        //txttotxl.Text = GTLTOL.ToString();
                        //for (int i = 0; i < Repeater3.Items.Count; i++)
                        //{
                        //    TextBox txtammunt1 = (TextBox)Repeater3.Items[i].FindControl("txtammunt1");
                        //    txtammunt1.Text = GTLTOL.ToString();
                        //}
                    }

                    BindDTui(MYTRANSID);
                    List<Database.ICTRPayTerms_HD> ListPayTemp = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                    if (ListPayTemp.Count() < 1)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {

                            DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");

                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
                            {
                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();

                                int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
                                int AccountID = 0;
                                Decimal Amount = Convert.ToDecimal(txtammunt.Text);



                                if (PaymentTermsId == 1250001)
                                {
                                    string TransDocNo = "";

                                    var ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                                    Database.ICTR_HD OBJHD = ListHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                                    if(OBJHD.TransDocNo != null)
                                    {
                                        TransDocNo = OBJHD.TransDocNo.ToString();
                                    }
                                    else
                                    {
                                        var listtbltranssubtypes1 = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                                        if (listtbltranssubtypes1.Count() > 0)
                                        {
                                            int SirialNO1 = (Convert.ToInt32(listtbltranssubtypes1.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1);
                                            string SirialNO11 = SirialNO1.ToString();
                                            TransDocNo = SirialNO11;
                                        }
                                        else
                                            TransDocNo = "";
                                    }

                                    
                                    //Parimal
                                    string InvoiceNo = TransDocNo;

                                    txtrefresh.Text = MYTRANSID + "," + InvoiceNo;


                                    ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                    objICTRPayTerms_HD.TenentID = TID;
                                    objICTRPayTerms_HD.LocationID = LID;
                                    objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
                                    objICTRPayTerms_HD.CashBankChequeID = 0;
                                    objICTRPayTerms_HD.CounterID = UID.ToString();

                                    objICTRPayTerms_HD.AccountID = AccountID;
                                    objICTRPayTerms_HD.CRUP_ID = 1;

                                    objICTRPayTerms_HD.Amount = Amount;
                                    objICTRPayTerms_HD.ReferenceNo = InvoiceNo;
                                    objICTRPayTerms_HD.ApprovalID = MYTRANSID.ToString();
                                    objICTRPayTerms_HD.Draft = false;


                                    DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                    DB.SaveChanges();
                                }
                                else
                                {
                                    ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();
                                    objICTRPayTerms_HD.TenentID = TID;
                                    objICTRPayTerms_HD.LocationID = LID;
                                    objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                    objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
                                    objICTRPayTerms_HD.CashBankChequeID = 0;
                                    objICTRPayTerms_HD.CounterID = UID.ToString();
                                    objICTRPayTerms_HD.AccountID = AccountID;
                                    objICTRPayTerms_HD.CRUP_ID = 1;
                                    objICTRPayTerms_HD.Amount = Amount;
                                    objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                    objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                    objICTRPayTerms_HD.Draft = false;
                                    DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                    DB.SaveChanges();
                                }


                                //Classes.EcommAdminClass.insertICTRPayTerms_HD_TEMP(TID, MYTRANSID, PaymentTermsId, UID.ToString(), LID, 0, Amount, IDRefresh, null, null, null, AccountID, 1, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
                            {
                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();

                                int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
                                int AccountID = 0;
                                Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                                if (PaymentTermsId == 1250001)
                                {
                                    string TransDocNo = "";
                                    var listtbltranssubtypes1 = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                                    if (listtbltranssubtypes1.Count() > 0)
                                    {
                                        int SirialNO1 = (Convert.ToInt32(listtbltranssubtypes1.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1);
                                        string SirialNO11 = SirialNO1.ToString();
                                        TransDocNo = SirialNO11;
                                    }
                                    else
                                        TransDocNo = "";
                                    //Parimal
                                    string InvoiceNo = TransDocNo;

                                    txtrefresh.Text = MYTRANSID + "," + InvoiceNo;

                                    Database.ICTRPayTerms_HD OBJTemp = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);
                                    OBJTemp.PaymentTermsId = PaymentTermsId;
                                    OBJTemp.ReferenceNo = InvoiceNo;
                                    OBJTemp.ApprovalID = MYTRANSID.ToString();
                                    OBJTemp.Amount = Amount;
                                    DB.SaveChanges();

                                }
                                else
                                {
                                    Database.ICTRPayTerms_HD OBJTemp = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);
                                    OBJTemp.PaymentTermsId = PaymentTermsId;
                                    OBJTemp.ReferenceNo = IDRefresh;
                                    OBJTemp.ApprovalID = IdApprouv;
                                    OBJTemp.Amount = Amount;
                                    DB.SaveChanges();
                                }




                            }
                        }

                    }



                    LnkBtnSavePayTerm.Visible = true;
                    string UserTotal = lblqtytotl.Text + "," + lblUNPtotl.Text + "," + lblTotatotl.Text;
                    Session["UserTotal"] = UserTotal.ToString();
                    ListItems.Visible = true;
                    panelRed.Visible = false;
                    LinkButton11.Text = "Save";
                    txtserchProduct.Enabled = true;
                    btnserchAdvans.Enabled = true;
                    ddlProduct.Enabled = true;


                }
                scope.Complete();
            }
            //BindDTui(MYTRANSID);
            //string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
            //ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
        }

        int R3count = 0;
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType1 = (Label)e.Item.FindControl("lblOHType1");

                DropDownList drppaymentMeted1 = (DropDownList)e.Item.FindControl("drppaymentMeted1");
                Classes.EcommAdminClass.getdropdown(drppaymentMeted1, TID, "Payment", "Method", "Inventeri", "REFTABLE");
                // Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "REFTABLE");
                //drppaymentMeted1.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method");
                //drppaymentMeted1.DataTextField = "REFNAME1";
                //drppaymentMeted1.DataValueField = "REFID";
                //drppaymentMeted1.DataBind();
                drppaymentMeted1.SelectedValue = "1250001";
                if (lblOHType1.Text != "" && lblOHType1.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType1.Text);
                    drppaymentMeted1.SelectedValue = ID.ToString();
                }
                //if (ViewState["R3count"] != null)
                //{
                //    R3count = Convert.ToInt32(ViewState["R3count"]);
                //    R3count++;
                //    ViewState["R3count"] = R3count;
                //}
                //else
                //{
                //    R3count++;
                //    ViewState["R3count"] = R3count;
                //}

                //if (ViewState["R3count"].ToString() == "2")
                //{
                //    drppaymentMeted1.Items.Remove(drppaymentMeted1.Items[0]);
                //}
            }
        }
        public void clearItemsTab()
        {
            //ddlProduct.SelectedValue = ddlUOM.SelectedValue = "0";
            txtDiscount.Text = "0";
            txtQuantity.Text = "1";
            txtDescription.Text = txtTotalCurrencyLocal.Text = "";
            hidUPriceLocal.Value = hidTotalCurrencyLocal.Value = "";
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
                txtserchProduct.Enabled = true;
                btnserchAdvans.Enabled = true;
                ddlProduct.Enabled = true;
            }
            else
            {
                ddlUOM.SelectedIndex = 99999;
                ddlProduct.SelectedIndex = 0;
                txtQuantity.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = "";

            }
        }
        public void GetProductDescription()
        {
            string[] counts2;
            counts2 = Classes.EcommAdminClass.BindItemCount(txtserchProduct, TID).ToString().Split('~');

            if (counts2[0] != "0")
            {
                Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
                lblProductCount.Text = "Product found " + counts2[0] + " as a Query.";
                int PID = Convert.ToInt32(counts2[1]);
                Classes.EcommAdminClass.BindProdu(PID, ddlUOM, txtDescription, txtserchProduct, TID);
                var ObjProduct = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                //decimal PRiesh = Convert.ToDecimal(ObjProduct.msrp);
                int Uom = Convert.ToInt32(ObjProduct.UOM);
                decimal PRiesh = Classes.EcommAdminClass.CostAmount(TID, PID, LID, Uom);
                Session["MultiTransactionProdObj"] = ObjProduct;
                txtUPriceLocal.Text = PRiesh.ToString();
                txtQuantity.Text = "1";

                TotalPriceui();


            }
            IsMinuscheck();


        }
        public void IsMinuscheck()
        {
            if (objtblsetupsalesh.AllowMinusQty == false)
            {
                //lblitemsearch.Text;
                if (ViewState["OriginalSearch"] != null)
                    txtserchProduct.Text = ViewState["OriginalSearch"].ToString();
                string counts1;
                counts1 = Classes.EcommAdminClass.BindAddvanserchBR(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID, LID, lblAvailableQty, Session["LANGUAGE"].ToString()).ToString();
                if (counts1 == "1")
                    lblitemsearch.Visible = false;
                else
                {
                    lblitemsearch.Text = "Search found " + counts1 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                }
            }
            else
            {
                string counts2;
                counts2 = Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                if (counts2 == "1")
                    lblitemsearch.Visible = false;
                else
                {
                    lblitemsearch.Text = "Search found " + counts2 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                }
                if (ddlProduct.SelectedValue != "" && ddlProduct.SelectedValue != null)
                {
                    int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                    if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID).Count() > 0)
                        lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID).OnHand.ToString();
                }

            }
        }

        public void BindProductui()
        {
            Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
            Classes.EcommAdminClass.getdropdown(ddlProduct, TID, LID.ToString(), "", "", "TBLPRODUCTWithICIT_BR");

            GetProductDescriptionui();
            //var result2 = (from pm in DB.ICIT_BR.Where(a => a.OnHand > 0 && a.TenentID == TID && a.LocationID == LID)
            //               join Module in DB.TBLPRODUCTs.Where(c => c.TenentID == TID && c.ACTIVE == "1" && c.LOCATION_ID == LID) on pm.MyProdID equals Module.MYPRODID
            //               select new { p1 = Module.MYPRODID, p2 = Module.ProdName1 }).ToList();
            //ddlProduct.DataSource = result2;
            //ddlProduct.DataValueField = "p1";
            //ddlProduct.DataTextField = "p2";
            //ddlProduct.DataBind();
            //ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Product--", "0"));
        }
        protected void btnserchAdvans_Click(object sender, EventArgs e)
        {

            //if (HiddenField3.Value == "0" || HiddenField3.Value == "")
            ////if (txtLocationSearch.Text == "" || txtLocationSearch.Text == null)
            ////{
            ////    GetProductDescription();
            ////    pnlSuccessMsg123.Visible = true;
            ////    lblMsg123.Text = "Enter the Customer";
            ////}
            ////else
            ////{

            if (objtblsetupsalesh.AllowMinusQty == false)
            {
                //lblitemsearch.Text;
                string counts1;
                counts1 = Classes.EcommAdminClass.BindAddvanserchBR(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID, LID, lblAvailableQty, "en-US").ToString();
                if (counts1 == "1")
                    lblitemsearch.Visible = false;
                else
                {
                    lblitemsearch.Text = "Search found " + counts1 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                }

            }
            else
            {
                string counts2;
                counts2 = Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                if (counts2 == "1")
                    lblitemsearch.Visible = false;
                else
                {
                    lblitemsearch.Text = "Search found " + counts2 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                }

                int PID = Convert.ToInt32(ddlProduct.SelectedValue);

                if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID).Count() > 0)
                    lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID).OnHand.ToString();
            }
            txtQuantity.Text = "1";


            //lblmultiuom.Visible = false;  //Come from MultiTransaction
            //lblmulticolor.Visible = false;
            //lblmultisize.Visible = false;
            //lblmultiperishable.Visible = false;
            //lblmultibinstore.Visible = false;
            //lblmultiserialize.Visible = false;

            ////}
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (HiddenField3.Value == "0" || HiddenField3.Value == "")
            ////{
            ////    pnlSuccessMsg123.Visible = true;
            ////    lblMsg123.Text = "Select Customer";

            ////}
            ////else
            ////{
            Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");

            int PID = Convert.ToInt32(ddlProduct.SelectedValue);
            Classes.EcommAdminClass.BindProdu(PID, ddlUOM, txtDescription, txtserchProduct, TID);
            var ObjProduct = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
            //decimal PRiesh = Convert.ToDecimal(ObjProduct.msrp);
            int uom = Convert.ToInt32(ObjProduct.UOM);
            decimal PRiesh = Classes.EcommAdminClass.CostAmount(TID, PID, LID, uom);
            Session["MultiTransactionProdObj"] = ObjProduct;
            txtUPriceLocal.Text = PRiesh.ToString();
            txtQuantity.Text = "1";

            //int uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID).UOM);

            if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).Count() > 0)
                lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).OnHand.ToString();
            LinkButton11.Enabled = true;

            //lblmultiuom.Visible = false;  //Come From Multitransaction
            //lblmulticolor.Visible = false;
            //lblmultisize.Visible = false;
            //lblmultiperishable.Visible = false;
            //lblmultibinstore.Visible = false;
            //lblmultiserialize.Visible = false;
            TotalPriceui();
            ////}
        }
        protected void txtQuantity_TextChanged1(object sender, EventArgs e)
        {
            DateTime TACtionDate = DateTime.Now;//Convert.ToDateTime(txtOrderDate.Text);yogesh
            string OICODID = Pidalcodeui(TACtionDate).ToString();
            ViewState["SaveList1"] = null;
            if (ddlProduct.SelectedValue == "0" || ddlProduct.SelectedValue == "")
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
                    bool Flag = Convert.ToBoolean(DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == objtblsetupsalesh.transid && p.transsubid == objtblsetupsalesh.transsubid).AllowMinusQty);
                    if (Flag == false)
                    {
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = "The available Qty is " + QTY + " not enough; to allow Sales in Minus contact Administration";
                        return;
                    }
                }
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
                    listSize.DataSource = Listserl;
                    listSize.DataBind();   //Come from MultiTransaction
                }
                else
                {
                    lblmultisize.Visible = false;
                }
                if (MultiColor == Convert.ToBoolean(1))
                {
                    lblmulticolor.Visible = true;  //Come from MultiTransaction
                    var Listserl = DB.ICIT_BR_SIZECOLOR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.SIZECODE == 999999998 && p.OnHand != 0).ToList();
                    listmulticoler.DataSource = Listserl;
                    listmulticoler.DataBind();
                }
                else
                {
                    lblmulticolor.Visible = false; //Come from MultiTransaction
                }
                if (MultiUOM == Convert.ToBoolean(1))
                {
                    lblmultiuom.Visible = true;  //Come from MultiTransaction
                    var Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    lidtUom.DataSource = Listserl;
                    lidtUom.DataBind();

                }
                else
                {
                    lblmultiuom.Visible = false;  //Come from MultiTransaction
                }
                if (Serialized == Convert.ToBoolean(1))
                {
                    lblmultiserialize.Visible = true;  //Come from MultiTransaction
                    if (txtQuantity.Text == "0")
                    { }
                    else
                    {
                        var Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.Active == "Y").ToList();
                        ViewState["ListofSerlNumber"] = Listserl;
                        listSerial.DataSource = Listserl;  //Come from MultiTransaction
                        listSerial.DataBind();
                    }
                }
                else
                {
                    lblmultiserialize.Visible = false;  //Come from MultiTransaction
                }
                if (MultiBinStore == Convert.ToBoolean(1))
                {
                    lblmultibinstore.Visible = true;  //Come from MultiTransaction
                    var Listserl = DB.ICIT_BR_BIN.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    listBin.DataSource = Listserl;  //Come from MultiTransaction
                    listBin.DataBind();
                    ViewState["ListICIT_BR_BIN"] = Listserl;
                }
                else
                {
                    lblmultibinstore.Visible = false;  //Come from MultiTransaction
                }
                if (Perishable == Convert.ToBoolean(1))
                {
                    lblmultiperishable.Visible = true;  //Come from MultiTransaction
                    var Listserl = DB.ICIT_BR_Perishable.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    listperishibal.DataSource = Listserl;  //Come from MultiTransaction
                    listperishibal.DataBind();
                    ViewState["ListICIT_BR_Perishable"] = Listserl;
                }
                else
                {
                    lblmultiperishable.Visible = false; //Come from MultiTransaction
                }
                lbltotalqty.Text = lbl1.Text = lbl2.Text = lbl3.Text = lbl4.Text = lbl5.Text = "   Total Qty" + TexQty.ToString();

                TotalPriceui();
                txtQuantity.Focus();
            }
        }



        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDT")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);
                Database.TempListICTR_DT objICTR_DT = DB.TempListICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                //objICTR_DT.ACTIVE = false;
                DB.DeleteObject(objICTR_DT);
                DB.SaveChanges();
                BindDTui(str1);
            }
            if (e.CommandName == "editDT")
            {
                //BindProduct();
                lblMode.Text = "UC-Edit Mode";
                LinkButton11.Enabled = true;
                Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
                //Classes.EcommAdminClass.getdropdown(ddlProduct, TID, LID.ToString(), "", "", "TBLPRODUCTWithICIT_BR");
                txtserchProduct.Enabled = false;
                btnserchAdvans.Enabled = false;
                ddlProduct.Enabled = false;
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);
                ViewState["ItemeditDTMYTRANSIDMYID"] = str1.ToString() + "," + str2.ToString();
                List<TempListICTR_DT> ListDate = DB.TempListICTR_DT.Where(p => p.MYTRANSID == str1 && p.ACTIVE == true && p.TenentID == TID).ToList();
                if (ListDate.Count() > 0)
                {
                    List<Database.TempListICTR_DT> TempListICTR_DT123 = ListDate.ToList();
                    TempListICTR_DT objICTR_DT = TempListICTR_DT123.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                    //ddlProduct.SelectedValue = objICTR_DT.MyProdID.ToString();
                    txtDescription.Text = objICTR_DT.DESCRIPTION.ToString();
                    ddlUOM.SelectedValue = objICTR_DT.UOM.ToString();
                    txtQuantity.Text = objICTR_DT.QUANTITY.ToString();
                    txtUPriceLocal.Text = objICTR_DT.UNITPRICE.ToString();
                    txtDiscount.Text = objICTR_DT.DISAMT.ToString();
                    txtTotalCurrencyLocal.Text = objICTR_DT.AMOUNT.ToString();
                    ViewState["DTTRCTIONID"] = str1;
                    ViewState["DTMYID"] = str2;
                    int PID = Convert.ToInt32(objICTR_DT.MyProdID);
                    //int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                    TBLPRODUCT objTBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                    txtserchProduct.Text = objTBLPRODUCT.BarCode;
                    Boolean Perishable = Convert.ToBoolean(objTBLPRODUCT.Perishable);  //Come From MultiTransaction
                    Boolean MultiUOM = Convert.ToBoolean(objTBLPRODUCT.MultiUOM);
                    Boolean MultiColor = Convert.ToBoolean(objTBLPRODUCT.MultiColor);
                    Boolean MultiSize = Convert.ToBoolean(objTBLPRODUCT.MultiSize);
                    Boolean MultiBinStore = Convert.ToBoolean(objTBLPRODUCT.MultiBinStore);
                    Boolean Serialized = Convert.ToBoolean(objTBLPRODUCT.Serialized);
                    if (MultiUOM == Convert.ToBoolean(1))
                        lblmultiuom.Visible = true;
                    else
                        lblmultiuom.Visible = false;
                    if (Perishable == Convert.ToBoolean(1))
                        lblmultiperishable.Visible = true;
                    else
                        lblmultiperishable.Visible = false;
                    if (MultiColor == Convert.ToBoolean(1))
                        lblmulticolor.Visible = true;
                    else
                        lblmulticolor.Visible = false;
                    if (MultiSize == Convert.ToBoolean(1))
                        lblmultisize.Visible = true;
                    else
                        lblmultisize.Visible = false;
                    if (MultiBinStore == Convert.ToBoolean(1))
                        lblmultibinstore.Visible = true;
                    else
                        lblmultibinstore.Visible = false;
                    if (Serialized == Convert.ToBoolean(1))
                        lblmultiserialize.Visible = true;
                    else
                        lblmultiserialize.Visible = false;
                }
                ViewState["AddnewItems"] = "Yes";
                btndiscartitems.Text = "Cancel";
                ListItems.Visible = false;
                panelRed.Visible = true;
                btnAddItemsIn.Visible = false;
                btnAddDT.Visible = false;//btnAddDT.Visible = true;yogesh20042017
                btnAddDT.Text = "Update";
                LinkButton11.Text = "Update";
            }
        }
        public string getprodnameui(int SID)
        {
            string ProductCode = DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).UserProdID;
            return ProductCode;
        }
        protected void btnaddamunt1_Click(object sender, EventArgs e)
        {
            //LnkBtnSavePayTerm.Visible = false;
            ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            TempICTRPayTerms_HD = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            ICTRPayTerms_HD objEco_ICEXTRACOST = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICEXTRACOST);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            Repeater3.DataSource = TempICTRPayTerms_HD;
            Repeater3.DataBind();
        }



        protected void btndiscartitems_Click(object sender, EventArgs e)
        {
            if (ViewState["AddnewItems"] != null)
            {
                ListItems.Visible = true;
                panelRed.Visible = false;
                ViewState["AddnewItems"] = null;
                btndiscartitems.Text = "Exit";
                btnAddDT.Visible = false;
                btnAddItemsIn.Visible = true;
                txtserchProduct.Enabled = true;
                btnserchAdvans.Enabled = true;
                ddlProduct.Enabled = true;
                LinkButton11.Text = "Save";
            }
            else
            {
                //ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;
                txtQuantity.Text = txtUPriceLocal.Text = txtDescription.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = "";

            }
        }

        public void checkPRice()
        {
            string[] counts3;
            counts3 = Classes.EcommAdminClass.BindItemCount(txtserchProduct, TID).ToString().Split('~');
            if (counts3[0] != "0")
            {
                int PID = Convert.ToInt32(counts3[1]);
                var listprod = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == PID).ToList();
                if (listprod.Count() > 0)
                {
                    Database.TBLPRODUCT ObjProduct = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == PID);

                    int selesAdmin = Convert.ToInt32(DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).SalesAdminID);
                    if (selesAdmin != UID)
                    {
                        decimal PRiesh = Convert.ToDecimal(ObjProduct.onlinesale1);
                        decimal msrp=Convert.ToDecimal(ObjProduct.msrp);
                        decimal InputPRiesh = Convert.ToDecimal(txtUPriceLocal.Text);
                        if (InputPRiesh >= PRiesh && InputPRiesh <= msrp)
                        {
                            pnlSuccessMsg123.Visible = true;
                            lblMsg123.Text = "You can input price between "+PRiesh+" and "+ msrp +" if you wont to add plase contact superviser  ";
                            LinkButton11.Enabled = false;
                            return;
                        }
                        else
                        {
                            LinkButton11.Enabled = true;
                        }
                    }

                    if (selesAdmin == UID)
                    {
                        decimal PRiesh = Convert.ToDecimal(ObjProduct.onlinesale3);
                        decimal msrp = Convert.ToDecimal(ObjProduct.msrp);
                        decimal InputPRiesh = Convert.ToDecimal(txtUPriceLocal.Text);
                        if (InputPRiesh >= PRiesh && InputPRiesh <= msrp)
                        {
                            pnlSuccessMsg123.Visible = true;
                            lblMsg123.Text = "You can input price between " + PRiesh + " and " + msrp;
                            LinkButton11.Enabled = false;
                            return;
                        }
                        else
                        {
                            LinkButton11.Enabled = true;
                        }
                    }

                }

            }
        }

        protected void txtUPriceLocal_TextChanged1(object sender, EventArgs e)
        {

            //checkPRice();
            TotalPriceui();
        }

        protected void btnserchAdvans_Click1(object sender, EventArgs e)
        {
            //if (txtLocationSearch.Text == "" || txtLocationSearch.Text == null)
            //{
            //    GetProductDescription();
            //    pnlSuccessMsg123.Visible = true;
            //    lblMsg123.Text = "Enter the Customer";
            //}
            //else
            //{

            Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");

            //string[] counts3;
            //counts3 = Classes.EcommAdminClass.BindItemCount(txtserchProduct, TID).ToString().Split('~');

            //if (counts3[0] != "0")
            //{
            //    Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
            //    lblProductCount.Text = "Product found " + counts3[0] + " as a Query.";
            //    //int PID = Convert.ToInt32(counts3[1]);
            //    //Classes.EcommAdminClass.BindProdu(PID, ddlUOM, txtDescription, txtserchProduct, TID);
            //    //var ObjProduct = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
            //    //Session["MultiTransactionProdObj"] = ObjProduct;

            //    //int selesAdmin = Convert.ToInt32(DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).SalesAdminID);
            //    //decimal PRiesh = 0;
            //    //if (selesAdmin == UID)
            //    //{
            //    //    PRiesh = Convert.ToDecimal(ObjProduct.onlinesale3);
            //    //    txtUPriceLocal.Text = PRiesh.ToString();
            //    //}
            //    //else
            //    //{
            //    //    PRiesh = Convert.ToDecimal(ObjProduct.onlinesale1);
            //    //    txtUPriceLocal.Text = PRiesh.ToString();
            //    //}
            //    decimal PRiesh = Convert.ToDecimal(ObjProduct.msrp);
            //    txtUPriceLocal.Text = PRiesh.ToString();
            //    txtQuantity.Text = "1";

            //    TotalPriceui();


            //}


            if (objtblsetupsalesh.AllowMinusQty == false)
            {
                //lblitemsearch.Text;
                string counts1;
                counts1 = Classes.EcommAdminClass.BindAddvanserchBR(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID, LID, lblAvailableQty, "en-US").ToString();
                //if (counts1 == "1")
                //    lblitemsearch.Visible = false;
                //else
                //{
                    lblitemsearch.Text = "Search found " + counts1 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                //}


                if (counts1 != "0")
                {
                    int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                    

                    int uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID).UOM);

                    ddlUOM.SelectedValue = uom.ToString();

                    if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).Count() > 0)
                    {
                        lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).OnHand.ToString();
                    }
                    else
                    {
                        lblAvailableQty.Text = "Available Qty : 0";
                    } 
                    LinkButton11.Enabled = true;
                }
                else
                {

                    LinkButton11.Enabled = false;
                    ddlUOM.SelectedValue = "99999";
                    txtDescription.Text = lblProductCount.Text = " Product found 0 as a Query";
                    txtQuantity.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = txtUPriceLocal.Text = "0";
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Product not found in search";
                    return;
                }

            }
            else
            {
                string counts2;
                counts2 = Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                //if (counts2 == "1")
                //    lblitemsearch.Visible = false;
                //else
                //{
                    lblitemsearch.Text = "Search found " + counts2 + " Records Use Drop Down to select one";
                    lblitemsearch.Visible = true;
                //}

                if (counts2 != "0")
                {
                    int PID = Convert.ToInt32(ddlProduct.SelectedValue);

                    int uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID).UOM);

                    if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).Count() > 0)
                    {
                        lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).OnHand.ToString();
                    }
                    else
                    {
                        lblAvailableQty.Text = "Available Qty : 0";
                    }
                        
                    LinkButton11.Enabled = true;
                }
                else
                {

                    LinkButton11.Enabled = false;
                    ddlUOM.SelectedValue = "99999";
                    txtDescription.Text = lblProductCount.Text = " Product found 0 as a Query";
                    txtQuantity.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = txtUPriceLocal.Text = "0";
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Product not found in search";
                    return;
                }


            }
            txtQuantity.Text = "1";

            //lblmultiuom.Visible = false;  //Come from MultiTransaction
            //lblmulticolor.Visible = false;
            //lblmultisize.Visible = false;
            //lblmultiperishable.Visible = false;
            //lblmultibinstore.Visible = false;
            //lblmultiserialize.Visible = false;

            ////}
        }

        protected void txtDiscount_TextChanged1(object sender, EventArgs e)
        {
            TotalPriceui();
        }

        public string getrecodtypename(int ID)
        {
            return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == ID && p.TenentID == TID).RecValue;
        }
        public string getbinname(int BID)
        {
            return DB.TBLBINs.Single(p => p.BIN_ID == BID && p.TenentID == TID).BINDesc1;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int QTY = 0;//Convert.ToInt32(txtQuantity.Text);come from invoice
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
                // DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);come from invoice
                string OICODID = "0";//Pidalcode(TACtionDate).ToString();
                for (int i = 0; i < lidtUom.Items.Count; i++)
                {
                    Label lbltidser = (Label)lidtUom.Items[i].FindControl("lbltidser");
                    Label lblpidsril = (Label)lidtUom.Items[i].FindControl("lblpidsril");
                    Label lblpdcodeser = (Label)lidtUom.Items[i].FindControl("lblpdcodeser");
                    Label lblmysysser = (Label)lidtUom.Items[i].FindControl("lblmysysser");
                    Label lbluomser = (Label)lidtUom.Items[i].FindControl("lbluomser");
                    Label lbllocaser = (Label)lidtUom.Items[i].FindControl("lbllocaser");

                    TextBox txtuomQty = (TextBox)lidtUom.Items[i].FindControl("txtuomQty");
                    if (txtuomQty.Text != "" && txtuomQty.Text != "0")
                    {
                        int PID = Convert.ToInt32(lblpidsril.Text);
                        int TCID = 0;//Convert.ToInt32(txtTraNoHD.Text);come from invoice
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

                        string Reference = "";//txtrefreshno.Text;come from invoice
                        string RecodName = "UOM";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = "";//txtDescription.Text;come from invoice
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
                        // txtDescription.Text = MainDecrption;come from invoice
                    }
                }
                ViewState["MultiUOM"] = null;
            }
            else
            {
                // pnlSuccessMsg123.Visible = true;come from invoice
                // lblMsg123.Text = "UOM selected Qty " + Total + " from " + QTY;come from invoice
            }
        }
        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            int QTY = 0;//Convert.ToInt32(txtQuantity.Text);//come from invoice
            int TCID = 0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice
            int PID = 0;// Convert.ToInt32(ddlProduct.SelectedValue);come from invoice
            int Total = 0;
            bool multiperisebal = true;
            for (int i = 0; i < listperishibal.Items.Count; i++)
            {
                TextBox txtnewqty = (TextBox)listperishibal.Items[i].FindControl("txtnewqty");
                if (txtnewqty.Text != "" && txtnewqty.Text != "0")
                {
                    int TXT = Convert.ToInt32(txtnewqty.Text);
                    Total = Total + TXT;
                }
            }
            if (QTY == Total)
            {
                // DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);come from invoice
                string OICODID = DateTime.Now.ToString();//Pidalcode(TACtionDate).ToString();//come from invoice
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
                        int UOM = 99999;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        int BinID = 999999998;
                        string BatchNo = txtBatchno.Text;
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtnewqty.Text);

                        string Reference = "0";// txtrefreshno.Text;come from invoice
                        string RecodName = "Perishable";
                        DateTime ProdDate = Convert.ToDateTime(txtproductdate.Text);
                        DateTime ExpiryDate = Convert.ToDateTime(txtexpirydate.Text);
                        DateTime LeadDays2Destroy = Convert.ToDateTime(txtlead2destroydate.Text);
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = "";// txtDescription.Text;come from invoice
                        string MainDecrption = "";
                        if (multiperisebal == true)
                        {
                            MainDecrption = Discpition + "\n" + " Perishable : " + BatchNo + " : " + NewQty;
                            multiperisebal = false;
                        }
                        else
                            MainDecrption = Discpition + " , " + BatchNo + " : " + NewQty;
                        //txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["Perishable"] = null;
            }
            else
            {
                //pnlSuccessMsg123.Visible = true;
                //lblMsg123.Text = "Perishibal selected Qty " + Total + " from " + QTY;
            }
        }
        protected void linkMulticoler_Click(object sender, EventArgs e)
        {
            int QTY = 0;// Convert.ToInt32(txtQuantity.Text);come from invoice
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
                //DateTime TACtionDate = 0;// Convert.ToDateTime(txtOrderDate.Text);come from invoice
                string OICODID = DateTime.Now.ToString();// Pidalcode(TACtionDate).ToString();come from invoice
                for (int i = 0; i < listmulticoler.Items.Count; i++)
                {
                    Label LblColoername = (Label)listmulticoler.Items[i].FindControl("LblColoername");
                    Label lblpidsril = (Label)listmulticoler.Items[i].FindControl("lblpidsril");
                    Label lblcolerid = (Label)listmulticoler.Items[i].FindControl("lblcolerid");
                    TextBox txtcoloerqty = (TextBox)listmulticoler.Items[i].FindControl("txtcoloerqty");
                    if (txtcoloerqty.Text != "" && txtcoloerqty.Text != "0")
                    {
                        int PID = Convert.ToInt32(lblpidsril.Text);
                        int TCID = 0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice
                        int COLID = Convert.ToInt32(lblcolerid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = 99999;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int SIZECODE = 999999998;
                        int COLORID = COLID;
                        int BinID = 999999998;
                        string BatchNo = "999999998";
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtcoloerqty.Text);

                        string Reference = "0";// txtrefreshno.Text;come from invoice
                        string RecodName = "M1ultiColor";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = "";// txtDescription.Text;come from invoice
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

                        //txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["MultiColor"] = null;
            }
            else
            {
                //    pnlSuccessMsg123.Visible = true;
                //    lblMsg123.Text = "Colors selected Qty " + Total + " from " + QTY;
            }
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            int QTY = 0;// Convert.ToInt32(txtQuantity.Text);come from invoice
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
                // DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = DateTime.Now.ToString();// Pidalcode(TACtionDate).ToString();come from invoice
                for (int i = 0; i < listSize.Items.Count; i++)
                {
                    Label LblColoername = (Label)listSize.Items[i].FindControl("LblColoername");
                    Label lblpidsril = (Label)listSize.Items[i].FindControl("lblpidsril");
                    Label lblcolerid = (Label)listSize.Items[i].FindControl("lblcolerid");
                    TextBox txtmultisze = (TextBox)listSize.Items[i].FindControl("txtmultisze");
                    if (txtmultisze.Text != "" && txtmultisze.Text != "0")
                    {
                        int PID = Convert.ToInt32(lblpidsril.Text);
                        int TCID = 0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice
                        int COLID = Convert.ToInt32(lblcolerid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = 99999;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int SIZECODE = Convert.ToInt32(COLID);
                        int COLORID = 999999998;
                        int BinID = 999999998;
                        string BatchNo = "999999998";
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtmultisze.Text);

                        string Reference = "";// txtrefreshno.Text;come from invoice
                        string RecodName = "MultiSize";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = "";// txtDescription.Text;come from invoice
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

                        //txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["MultiSize"] = null;
            }
            else
            {
                //pnlSuccessMsg123.Visible = true;
                //lblMsg123.Text = "Multi Size selected Qty " + Total + " from " + QTY;
            }
        }
        protected void lbApproveIss_Click(object sender, EventArgs e)
        {
            int PID = 0;// Convert.ToInt32(ddlProduct.SelectedValue);come from invoice
            int QTY = 0;//Convert.ToInt32(txtQuantity.Text);come from invoice
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
                //DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = DateTime.Now.ToString();///Pidalcode(TACtionDate).ToString();//come from invoice
                for (int i = 0; i < listBin.Items.Count; i++)
                {
                    TextBox txtqty = (TextBox)listBin.Items[i].FindControl("txtqty");
                    Label lblbinid = (Label)listBin.Items[i].FindControl("lblbinid");
                    if (txtqty.Text != "" && txtqty.Text != "0")
                    {
                        int TCID = 0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice
                        int UomID = 99999;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int BinID = Convert.ToInt32(lblbinid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM = 99999;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        //int BinID = 999999998;
                        string BatchNo = "0";// txtBatchNo.Text;come from invoice
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtqty.Text);
                        string Reference = "0";// txtrefreshno.Text;come from invoice
                        string RecodName = "MultiBIN";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active = "D";
                        int CRUP_ID = 999999998;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID);
                        string Discpition = "";//txtDescription.Text;come from invoice
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
                        //txtDescription.Text = MainDecrption;
                    }
                }
                ViewState["MultiBinStore"] = null;
            }
            else
            {
                //pnlSuccessMsg123.Visible = true;
                //lblMsg123.Text = "Multi Bin selected Qty " + Total + " from " + QTY;
            }
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            int QTY = 0;// Convert.ToInt32(txtQuantity.Text);come from invoice
            //DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = DateTime.Now.ToString();// Pidalcode(TACtionDate).ToString();
            int Total = 0;
            string[] Seperate4 = tags_2.Text.Split(',');
            Total = Seperate4.Count();
            if (QTY == Total)
            {
                string Discpition = "";// txtDescription.Text;come from invoice
                string MainDecrption = tags_2.Text;
                MainDecrption = Discpition + "\n" + "Serialize : " + MainDecrption;
                //txtDescription.Text = MainDecrption;come from invoice

                string[] str = tags_2.Text.Split(',');
                int count5 = 0;
                string Sep5 = "";
                for (int i = 0; i <= str.Count() - 1; i++)
                {
                    int PID = 0;// Convert.ToInt32(ddlProduct.SelectedValue);come from invoice
                    int TCID = 0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice

                    int TenentID = TID;
                    int MyProdID = PID;
                    string period_code = OICODID;
                    string MySysName = "SAL";
                    int UOM = 99999;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                    int SIZECODE = 999999998;
                    int COLORID = 999999998;
                    int BinID = 999999998;
                    string BatchNo = "999999998";
                    string Serialize = str[i];
                    int MYTRANSID = TCID;
                    int LocationID = LID;
                    int NewQty = Convert.ToInt32(1);
                    string Reference = "";//txtrefreshno.Text;come from invoice
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
                //pnlSuccessMsg123.Visible = true;
                //lblMsg123.Text = "Serialization selected Qty " + Total + " from " + QTY;
            }
            ViewState["Serialized"] = null;
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
                //int LID = Convert.ToInt32(lbllocaser.Text);
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
            //ModalPopupExtender2.Show();
        }

        protected void LnkBtnSavePayTerm_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                decimal total = 0;
                for (int j = 0; j < Repeater3.Items.Count; j++)
                {
                    DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");
                    TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
                    TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
                    total += Convert.ToDecimal(txtammunt.Text);
                }

                decimal Totallist = Convert.ToDecimal(lblTotatotl.Text);
                if (total == Totallist)
                {
                    MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);
                    pnlSuccessMsg123.Visible = false;


                    //foreach (Database.ICTRPayTerms_HD ItemHD in ListPayTemp)
                    //{

                    //    DB.ICTRPayTerms_HD.DeleteObject(ItemHD);
                    //    DB.SaveChanges();

                    //}

                    for (int j = 0; j < Repeater3.Items.Count; j++)
                    {
                        DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");
                        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
                        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
                        if (txtrefresh.Text != "" && txtammunt.Text != "")
                        {
                            string RFresh = txtrefresh.Text.ToString();
                            string[] id = RFresh.Split(',');
                            string IDRefresh = id[0].ToString();
                            string IdApprouv = id[1].ToString();

                            int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
                            Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                            List<Database.ICTRPayTerms_HD> ListPayTemp = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId).ToList();

                            if (ListPayTemp.Count() > 0)
                            {
                                Database.ICTRPayTerms_HD objICTRPayTerms_HD = ListPayTemp.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);

                                decimal AmountOld = Convert.ToDecimal(objICTRPayTerms_HD.Amount);

                                //decimal amountnew = Amount + AmountOld;
                                //amountnew = amountnew - Amount;

                                objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
                                objICTRPayTerms_HD.Amount = Amount;
                                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                objICTRPayTerms_HD.Draft = false;

                                DB.SaveChanges();
                            }
                            else
                            {
                                Database.ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();

                                objICTRPayTerms_HD.TenentID = TID;
                                objICTRPayTerms_HD.LocationID = LID;
                                objICTRPayTerms_HD.MyTransID = MYTRANSID;
                                objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
                                objICTRPayTerms_HD.CashBankChequeID = 0;
                                objICTRPayTerms_HD.CounterID = UID.ToString();

                                objICTRPayTerms_HD.AccountID = 0;
                                objICTRPayTerms_HD.CRUP_ID = 1;

                                objICTRPayTerms_HD.Amount = Amount;
                                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                                objICTRPayTerms_HD.ApprovalID = IdApprouv;
                                objICTRPayTerms_HD.Draft = false;


                                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                                DB.SaveChanges();
                            }


                        }
                        else
                        {
                            pnlSuccessMsg123.Visible = true;
                            lblMsg123.Text = "Enter Refrance No or Amount";
                        }

                    }
                    LnkBtnSavePayTerm.Visible = false;
                }
                else
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Check Your Pamment Total is not match";
                }



                scope.Complete();
            }
        }

        //protected void txtrefresh1_TextChanged(object sender, EventArgs e)
        //{
        //    MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);

        //    for (int j = 0; j < Repeater3.Items.Count; j++)
        //    {
        //        DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");
        //        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
        //        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
        //        if (txtrefresh.Text != "" && txtammunt.Text!="")
        //        {
        //            string RFresh = txtrefresh.Text.ToString();
        //            string[] id = RFresh.Split(',');
        //            string IDRefresh = id[0].ToString();
        //            string IdApprouv = id[1].ToString();
        //            int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
        //            Decimal Amount = Convert.ToDecimal(txtammunt.Text);

        //            var listpay = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId).ToList();
        //            if(listpay.Count()>0)
        //            {
        //                Database.ICTRPayTerms_HD OBJTemp = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);

        //                OBJTemp.PaymentTermsId = PaymentTermsId;
        //                OBJTemp.ReferenceNo = IDRefresh;
        //                OBJTemp.ApprovalID = IdApprouv;
        //                OBJTemp.Amount = Amount;

        //                DB.SaveChanges();
        //            }
        //            else
        //            {
        //                Database.ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();

        //                objICTRPayTerms_HD.TenentID = TID;
        //                objICTRPayTerms_HD.LocationID = LID;
        //                objICTRPayTerms_HD.MyTransID = MYTRANSID;
        //                objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
        //                objICTRPayTerms_HD.CashBankChequeID = 0;
        //                objICTRPayTerms_HD.CounterID = UID.ToString();

        //                objICTRPayTerms_HD.AccountID = 0;
        //                objICTRPayTerms_HD.CRUP_ID = 1;

        //                objICTRPayTerms_HD.Amount = Amount;
        //                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
        //                objICTRPayTerms_HD.ApprovalID = IdApprouv;
        //                objICTRPayTerms_HD.Draft = false;


        //                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
        //                DB.SaveChanges();

        //            }


        //        }
        //        else
        //        {
        //            pnlSuccessMsg123.Visible = true;
        //            lblMsg123.Text = "Enter Refrance No or Amount";
        //        }
        //    }
        //}




        //protected void txtammunt1_TextChanged(object sender, EventArgs e)
        //{
        //    MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);
        //    pnlSuccessMsg123.Visible = false;

        //    for (int j = 0; j < Repeater3.Items.Count; j++)
        //    {
        //        DropDownList drppaymentMeted1 = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted1");
        //        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh1");
        //        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt1");
        //        if (txtrefresh.Text != "" && txtammunt.Text != "")
        //        {
        //            string RFresh = txtrefresh.Text.ToString();
        //            string[] id = RFresh.Split(',');
        //            string IDRefresh = id[0].ToString();
        //            string IdApprouv = id[1].ToString();
        //            int PaymentTermsId = Convert.ToInt32(drppaymentMeted1.SelectedValue);
        //            Decimal Amount = Convert.ToDecimal(txtammunt.Text);

        //            var listpay = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId).ToList();
        //            if (listpay.Count() > 0)
        //            {
        //                Database.ICTRPayTerms_HD OBJTemp = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);

        //                OBJTemp.PaymentTermsId = PaymentTermsId;
        //                OBJTemp.ReferenceNo = IDRefresh;
        //                OBJTemp.ApprovalID = IdApprouv;
        //                OBJTemp.Amount = Amount;

        //                DB.SaveChanges();
        //            }
        //            else
        //            {
        //                Database.ICTRPayTerms_HD objICTRPayTerms_HD = new ICTRPayTerms_HD();

        //                objICTRPayTerms_HD.TenentID = TID;
        //                objICTRPayTerms_HD.LocationID = LID;
        //                objICTRPayTerms_HD.MyTransID = MYTRANSID;
        //                objICTRPayTerms_HD.PaymentTermsId = PaymentTermsId;
        //                objICTRPayTerms_HD.CashBankChequeID = 0;
        //                objICTRPayTerms_HD.CounterID = UID.ToString();

        //                objICTRPayTerms_HD.AccountID = 0;
        //                objICTRPayTerms_HD.CRUP_ID = 1;

        //                objICTRPayTerms_HD.Amount = Amount;
        //                objICTRPayTerms_HD.ReferenceNo = IDRefresh;
        //                objICTRPayTerms_HD.ApprovalID = IdApprouv;
        //                objICTRPayTerms_HD.Draft = false;


        //                DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
        //                DB.SaveChanges();

        //            }

        //        }
        //        else
        //        {
        //            pnlSuccessMsg123.Visible = true;
        //            lblMsg123.Text = "Enter Refrance No or Amount";
        //        }
        //    }
        //}

    }
}