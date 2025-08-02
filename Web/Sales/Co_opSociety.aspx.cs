using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using AjaxControlToolkit;

namespace Web.Sales
{
    public partial class Co_opSociety : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        PropertyFile objProFile = new PropertyFile();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();

        int TID, LID, UID, Transid, Transsubid, EMPID = 0;
        string LangID, CURRENCY = "";

        public void SessionLoad()
        {
            string Ref = ((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();

            if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            {
                Transid = Convert.ToInt32(Request.QueryString["transid"]);
                Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                Session["Transid"] = Transid + "," + Transsubid;
            }

            objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            CURRENCY = objtblsetupsalesh.COUNTRYID.ToString();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();

            panelMsg.Visible = false;
            lblErreorMsg.Text = "";

            lblDateSales_Co_op.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //lblDate_Sales_Bakala.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //lblDate_Sales_Chapra.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            if (!IsPostBack)
            {
                Databind();
            }

        }

        public int GEt_REFID(string REFTYPE, string REFSUBTYPE, string SHORTNAME)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.SHORTNAME == SHORTNAME).Count() > 0)
            {
                int REFID = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.SHORTNAME == SHORTNAME).FirstOrDefault().REFID;
                return REFID;
            }
            else
            {
                return 0;
            }
        }
        public void Databind()
        {
            string CompanyType = GEt_REFID("COMP", "COMPTYPE", "Co-op Society").ToString();
            int ProdTypeRefId = GEt_REFID("PRODTYPE", "PRODTYPE", "Co-op Society");

            List_CO_Operative.DataSource = DB.View_Customer_product.Where(p => p.TenentID == TID && p.CompanyType == CompanyType && p.ProdTypeRefId == ProdTypeRefId).OrderBy(p=>p.COMPNAME1);
            List_CO_Operative.DataBind();
        }

        public string ReffranceNO(int COMPID)
        {
            DateTime today = DateTime.Now;

            int Year = today.Year;
            int Month = today.Month;
            int Day = today.Day;

            int dayDayOfYear = today.DayOfYear;
            int Serial = 0;

            List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == COMPID && p.TRANSDATE.Day == Day && p.TRANSDATE.Month == Month && p.TRANSDATE.Year == Year).ToList();
            if (ListHD.Count() > 0)
            {
                Serial = ListHD.Count();
            }
            else
            {
                Serial = 1;
            }
            string Reffrance = TID + "-" + Year + "-" + dayDayOfYear + "-" + COMPID + "-" + Serial;
            return Reffrance;
        }
        public int getMYTRANSID(int Customer)
        {
            DateTime TODAY = DateTime.Now;
            int Day = TODAY.Day;
            int Month = TODAY.Month;
            int Year = TODAY.Year;
            int MYTRANSID = 0;
            string Reffrance = ReffranceNO(Customer);

            List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.CUSTVENDID == Customer && p.REFERENCE == Reffrance).ToList();
            if (ListHD.Count() > 0)
            {
                MYTRANSID = Convert.ToInt32(ListHD.FirstOrDefault().MYTRANSID);
            }
            else
            {
                MYTRANSID = 0;
            }

            return MYTRANSID;
        }

        public string getMSRP(int MYPRODID, int UOM, int COMPID)
        {
            string SUOM = UOM.ToString();
            int MYTRANSID = getMYTRANSID(COMPID);
            decimal amount = 0;
            if (MYTRANSID != 0)
            {
                List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == SUOM).ToList();
                if (ListDT.Count() > 0)
                {
                    amount = Convert.ToDecimal(DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == SUOM).UNITPRICE);
                }
                else
                {
                    List<Database.ICITEMS_PRICE> ListPrice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID && p.UOM == UOM).ToList();
                    if (ListPrice.Count() > 0)
                    {
                        amount = Convert.ToDecimal(DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID && p.UOM == UOM).FirstOrDefault().msrp);
                    }
                    else
                    {
                        amount = 0;
                    }
                }
            }
            else
            {
                List<Database.ICITEMS_PRICE> ListPrice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID && p.UOM == UOM).ToList();
                if (ListPrice.Count() > 0)
                {
                    amount = Convert.ToDecimal(DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID && p.UOM == UOM).FirstOrDefault().msrp);
                }
                else
                {
                    amount = 0;
                }
            }
            return amount.ToString();
        }
        public string getQty(int MYPRODID, string UOM, int COMPID)
        {
            int MYTRANSID = getMYTRANSID(COMPID);
            decimal QTY = 0;
            List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == UOM).ToList();
            if (ListDT.Count() > 0)
            {
                QTY = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == UOM).QUANTITY;
            }
            else
            {
                QTY = 1;
            }

            return QTY.ToString();
        }

        public int Pidalcode(DateTime Time)
        {
            int PODID = 0;
            var TBLPERIODS = DB.TBLPERIODS.Where(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).ToList();
            if (TBLPERIODS.Count() > 0)
                PODID = Convert.ToInt32(TBLPERIODS.Single(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).PERIOD_CODE);
            return PODID;
        }

        public string getprodname(int MYPRODID)
        {
            string prodname = "";

            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID).Count() > 0)
            {
                prodname = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == MYPRODID).ProdName1;
            }

            return prodname;
        }

        public void transection(int CustomerID, int MYPRODID, int UOMprod, int QTYProd, decimal unitprice, decimal totalamt)
        {
            string Product_Name = getprodname(MYPRODID);

            int MYTRANSID = getMYTRANSID(CustomerID);

            if (MYTRANSID == 0)
            {
                MYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID)) + 1 : 1;
            }


            int COMPID = CustomerID;
            string Status = "DSO";

            string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
            DateTime TACtionDate = DateTime.Now;
            string OICODID = Pidalcode(TACtionDate).ToString();
            if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID && p.TenentID == TID).Count() == 1)
                COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID && p.TenentID == TID).COMPID;

            int ICTR_HDCount = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count();
            string GLPOST = objProFile.GLPOST;
            string GLPOST1 = objProFile.GLPOST1;
            string ICPOST = objProFile.ICPOST;
            string TransType = objProFile.TranType;

            string MainTranType = "O";// Out Qty On Product
            string PERIOD_CODE = OICODID;
            string MYSYSNAME = "SAL".ToString();
            int JOID = objProFile.JOID;
            int CRUP_ID = Convert.ToInt32(0);
            string GLPOSTREF = objProFile.GLPOSTREF;
            string GLPOSTREF1 = objProFile.GLPOSTREF1;
            string ICPOSTREF = objProFile.ICPOSTREF;
            bool ACTIVE = Convert.ToBoolean(true);
            int COMPANYID = Convert.ToInt32(CURRENCY);
            string REFERENCE = ReffranceNO(CustomerID);
            //int ToTenentID = objProFile.ToTenentID; ;
            int TOLOCATIONID = LID;
            //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
            tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
            string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
            decimal COMPID1 = COMPID;
            decimal CUSTVENDID = CustomerID;
            string LF = "L";
            string ACTIVITYCODE = "0";
            decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
            string USERBATCHNO = "";
            //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
            //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
            decimal TOTAMT = 0;
            decimal TOTQTY1 = 0;

            decimal AmtPaid = objProFile.AmtPaid;
            string PROJECTNO = "99999";
            string CounterID = objProFile.CounterID;
            string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
            DateTime TRANSDATE = TACtionDate;// DateTime.ParseExact(TACtionDate.ToString(), "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
            string NOTES = "";
            string USERID = UseID.ToString();
            DateTime Curentdatetime = DateTime.Now;
            DateTime ENTRYDATE = Curentdatetime;
            DateTime ENTRYTIME = Curentdatetime;
            DateTime UPDTTIME = Curentdatetime;
            decimal Discount = Convert.ToDecimal(0);
            //(TenentID = 0) AND (REFTYPE = 'Terms') AND (REFSUBTYPE = 'Terms') AND (REFNAME1 = 'Cash')

            int Terms = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Terms" && p.REFSUBTYPE == "Terms" && p.REFNAME1 == "Cash").Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Terms" && p.REFSUBTYPE == "Terms" && p.REFNAME1 == "Cash").REFID) : 0;

            string Custmerid = CustomerID.ToString();
            string Swit1 = "1";

            string BillNo = UID.ToString();

            string TransDocNo = "";

            List<Database.ICTR_HD> ListHDD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
            int Doc = ListHDD.Count() > 0 ? Convert.ToInt32(ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo) : 0;
            if (Doc != 0 && Doc != null)
            {
                TransDocNo = ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo;
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

            //Dipak
            string InvoiceNo = TransDocNo;
            string InvoiceNO = TransDocNo;

            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() < 1)
            {

                Classes.EcommAdminClass.insert_ICTR_HD(TID, MYTRANSID, 0, TOLOCATIONID, MainTranType, TranType, Transid, Transsubid, MYSYSNAME, COMPID1, CUSTVENDID, "L", PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, Custmerid, Swit1, BillNo, MYTRANSID, "", TransDocNo, 0, 0, 0);

                var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                if (SirialNO == InvoiceNo)
                {
                    Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                    objtblsubtype.serialno = SirialNO;
                    DB.SaveChanges();
                }
            }

            decimal DISPER = Convert.ToDecimal(0);
            decimal DISAMT = Convert.ToDecimal(0);
            int MyProdID = MYPRODID;
            string UOM = UOMprod.ToString();
            int MYID = get_myid(MYTRANSID, MyProdID, UOM);

            string REFTYPE = "LF";
            string REFSUBTYPE = "LF";
            int JOBORDERDTMYID = 1;
            int ACTIVITYID = 0;
            string DESCRIPTION = Product_Name;
            int QUANTITY = QTYProd;
            decimal UNITPRICE = unitprice;
            decimal AMOUNT = totalamt;
            string BATCHNO = "1";
            int BIN_ID = 1;
            string BIN_TYPE = "Bin";
            string GRNREF = "2";
            int COMPANYID1 = COMPID;
            int locationID = LID;
            decimal TAXAMT = Convert.ToDecimal(0.0);
            decimal TAXPER = Convert.ToDecimal(0.0);
            decimal PROMOTIONAMT = Convert.ToDecimal(10.00);
            decimal OVERHEADAMOUNT = Convert.ToDecimal(0.000);
            DateTime EXPIRYDATE = DateTime.Now;
            string SWITCH1 = "0".ToString();
            int DelFlag = 0;
            string ITEMID = "1";
            int Uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MyProdID).UOM);

            decimal CostAmount = Classes.EcommAdminClass.CostAmount(TID, MyProdID, LID, Uom);
            Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTRANSID, LID, MYID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

            //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
            Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
            Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
            Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
            Boolean Perishable = Convert.ToBoolean(obj.Perishable);
            Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
            Boolean Serialized = Convert.ToBoolean(obj.Serialized);
            Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);

            Database.ICTR_DT objDT = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.locationID == LID);
            Database.ICTR_HD Objhd = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
            string Reference = Objhd.REFERENCE.ToString();

            int UOM1 = Convert.ToInt32(UOM);
            if (obj.MultiUOM == true)
            {
                string period_code1 = OICODID;
                int SIZECODE = 999999998;
                int COLORID = 999999998;
                int BinID = 999999998;
                string BatchNo = "999999998";
                string Serialize = "NO";
                string RecodName = "UOM";
                DateTime ProdDate = DateTime.Now;
                DateTime ExpiryDate = DateTime.Now;
                DateTime LeadDays2Destroy = DateTime.Now;
                string Active1 = "D";
                Classes.EcommAdminClass.insertICIT_BR_TMP(TID, MyProdID, period_code1, MYSYSNAME, UOM1, SIZECODE, COLORID, BIN_ID, BatchNo, Serialize, MYTRANSID, LID, QUANTITY, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active1, CRUP_ID);
            }

            DateTime trnsDate = Convert.ToDateTime(Objhd.TRANSDATE);
            string MySysName = Objhd.MYSYSNAME.ToString();


            bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, Transid, Transsubid, MYTRANSID, MYID, QUANTITY, Reference, trnsDate, MySysName, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
            if (flag1 == false)
            {
                panelMsg.Visible = true;
                lblErreorMsg.Text = "One of the Posting Parameter is Blank/Null/Zero!";
                return;
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            Button txtbox = (Button)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;
            //   lblCOMPID, lblMYPRODID, lblUOM

            Label lblCOMPID = (Label)item.FindControl("lblCOMPID");
            Label lblMYPRODID = (Label)item.FindControl("lblMYPRODID");
            Label lblUOM = (Label)item.FindControl("lblUOM");

            TextBox txtpopQty = (TextBox)item.FindControl("txtpopQty");
            TextBox txtpopamt = (TextBox)item.FindControl("txtpopamt");
            TextBox txttotalamt = (TextBox)item.FindControl("txttotalamt");

            int CustomerID = Convert.ToInt32(lblCOMPID.Text);
            int MYPRODID = Convert.ToInt32(lblMYPRODID.Text);
            int UOMprod = Convert.ToInt32(lblUOM.Text);
            int QTYProd = Convert.ToInt32(txtpopQty.Text);
            decimal unitprice = Convert.ToDecimal(txtpopamt.Text);
            decimal totalamt = Convert.ToDecimal(txttotalamt.Text);

            transection(CustomerID, MYPRODID, UOMprod, QTYProd, unitprice, totalamt);

            Databind();
        }
        protected void List_CO_Operative_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //if (e.CommandName == "Save")
            //{
            //    string[] id = e.CommandArgument.ToString().Split(',');
            //    int CustomerID = Convert.ToInt32(id[0]);
            //    int MYPRODID = Convert.ToInt32(id[1]);
            //    int UOMprod = Convert.ToInt32(id[2]);
            //    int QTYProd = Convert.ToInt32(id[3]);
            //    decimal unitprice = 0;

            //}
        }

        public int get_myid(int MYTRANSID, int MyProdID, string UOM)
        {
            int MYID = 0;
            if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == MYTRANSID && p.MyProdID == MyProdID && p.UOM == UOM).Count() > 0)
            {
                MYID = DB.ICTR_DT.Single(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == MYTRANSID && p.MyProdID == MyProdID && p.UOM == UOM).MYID;
            }
            else
            {
                MYID = DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == MYTRANSID).Count() > 0 ? DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == MYTRANSID).Max(p => p.MYID) + 1 : 1;
            }
            return MYID;
        }

        protected void txtpopQty_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;
            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            TextBox txtpopQty = (TextBox)item.FindControl("txtpopQty");
            TextBox txtpopamt = (TextBox)item.FindControl("txtpopamt");
            TextBox txttotalamt = (TextBox)item.FindControl("txttotalamt");
            ModalPopupExtender ModalPopupExtender5 = (ModalPopupExtender)item.FindControl("ModalPopupExtender5");

            txtQty.Text = txtpopQty.Text;
            int qty = Convert.ToInt32(txtpopQty.Text);
            decimal amt = Convert.ToDecimal(txtpopamt.Text);
            decimal Total = qty * amt;
            txttotalamt.Text = Total.ToString();
            ModalPopupExtender5.Show();
        }

        protected void txtpopQty_List_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;

            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            TextBox txtpopQty = (TextBox)item.FindControl("txtpopQty");
            TextBox txtpopamt = (TextBox)item.FindControl("txtpopamt");
            TextBox txttotalamt = (TextBox)item.FindControl("txttotalamt");
            ModalPopupExtender ModalPopupExtender5 = (ModalPopupExtender)item.FindControl("ModalPopupExtender5");

            int qty = Convert.ToInt32(txtpopQty.Text);
            decimal amt = Convert.ToDecimal(txtpopamt.Text);
            decimal Total = qty * amt;
            txttotalamt.Text = Total.ToString();
            ModalPopupExtender5.Show();
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;

            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            TextBox txtpopQty = (TextBox)item.FindControl("txtpopQty");
            TextBox txtpopamt = (TextBox)item.FindControl("txtpopamt");
            TextBox txttotalamt = (TextBox)item.FindControl("txttotalamt");
            ModalPopupExtender ModalPopupExtender5 = (ModalPopupExtender)item.FindControl("ModalPopupExtender5");
            if (txtQty.Text != "")
            {
                txtpopQty.Text = txtQty.Text;
                int qty = Convert.ToInt32(txtQty.Text);
                decimal amt = Convert.ToDecimal(txtpopamt.Text);
                decimal Total = qty * amt;
                txttotalamt.Text = Total.ToString();
                //ModalPopupExtender5.Show();
            }
        }

        List<Database.ICTR_DT> ListDT = new List<ICTR_DT>();

        protected void List_CO_Operative_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblCOMPID = (Label)e.Item.FindControl("lblCOMPID");
            int CustomerID = Convert.ToInt32(lblCOMPID.Text);

            Label lblMYPRODID = (Label)e.Item.FindControl("lblMYPRODID");
            Label lblUOM = (Label)e.Item.FindControl("lblUOM");
            TextBox txttotalamt = (TextBox)e.Item.FindControl("txttotalamt");
            TextBox txtpopQty = (TextBox)e.Item.FindControl("txtpopQty");
            TextBox txtpopamt = (TextBox)e.Item.FindControl("txtpopamt");

            int MYTRANSID = getMYTRANSID(CustomerID);
            int MYPRODID = Convert.ToInt32(lblMYPRODID.Text);
            string UOM = lblUOM.Text.ToString();

            List<Database.ICTR_DT> ListBIndDT = new List<ICTR_DT>();

            if (ListDT.Count() < 1)
            {
                ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID).ToList();
                ListBIndDT = ListDT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == UOM).ToList();
            }
            else
            {
                ListBIndDT = ListDT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == UOM).ToList();
            }

            if (ListBIndDT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == UOM).Count() > 0)
            {
                txttotalamt.Text = ListBIndDT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MYPRODID && p.UOM == UOM).AMOUNT.ToString();
            }
            else
            {
                int qty = Convert.ToInt32(txtpopQty.Text);
                decimal unitprice = Convert.ToDecimal(txtpopamt.Text);
                txttotalamt.Text = (qty * unitprice).ToString();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton txtbox = (LinkButton)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;
            ModalPopupExtender ModalPopupExtender5 = (ModalPopupExtender)item.FindControl("ModalPopupExtender5");
            ModalPopupExtender5.Show();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton txtbox = (LinkButton)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;
            ModalPopupExtender ModalPopupExtender5 = (ModalPopupExtender)item.FindControl("ModalPopupExtender5");
            ModalPopupExtender5.Show();
        }

        protected void lnkbtnItem_Click(object sender, EventArgs e)
        {
            LinkButton txtbox = (LinkButton)sender;
            ListViewDataItem item = (ListViewDataItem)txtbox.NamingContainer;
            //   lblCOMPID, lblMYPRODID, lblUOM

            Label lblCOMPID = (Label)item.FindControl("lblCOMPID");
            Label lblMYPRODID = (Label)item.FindControl("lblMYPRODID");
            Label lblUOM = (Label)item.FindControl("lblUOM");

            TextBox txtpopQty = (TextBox)item.FindControl("txtpopQty");
            TextBox txtpopamt = (TextBox)item.FindControl("txtpopamt");
            TextBox txttotalamt = (TextBox)item.FindControl("txttotalamt");

            int CustomerID = Convert.ToInt32(lblCOMPID.Text);
            int MYPRODID = Convert.ToInt32(lblMYPRODID.Text);
            int UOMprod = Convert.ToInt32(lblUOM.Text);
            int QTYProd = Convert.ToInt32(txtpopQty.Text);
            decimal unitprice = Convert.ToDecimal(txtpopamt.Text);
            decimal totalamt = Convert.ToDecimal(txttotalamt.Text);

            transection(CustomerID, MYPRODID, UOMprod, QTYProd, unitprice, totalamt);

            Databind();
        }

    }

}