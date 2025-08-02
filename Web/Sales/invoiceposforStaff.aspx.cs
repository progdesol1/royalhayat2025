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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace Web.Sales
{
    public partial class invoiceposforStaff : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        // List<ICIT_BR_BIN> ListICIT_BR_BIN = new List<ICIT_BR_BIN>();
        // List<ICIT_BR_Perishable> ListICIT_BR_Perishable = new List<ICIT_BR_Perishable>();
        PropertyFile objProFile = new PropertyFile();
        public static DataTable dt_PurQuat;
        List<Database.tblsetupsalesh> Listtblsetupsalesh = new List<tblsetupsalesh>();
        List<Database.ICTR_DT> TempListICTR_DT123 = new List<Database.ICTR_DT>();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
        MYCOMPANYSETUP objMyCompany = new MYCOMPANYSETUP();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, MYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        decimal CURRENTCONVRATE = Convert.ToDecimal(0);
        bool ItemOn = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            SessionLoad();
            //if (FirstFlag) FistTimeLoad();

            if (!IsPostBack)
            {
                string uidd = ((USER_MST)Session["USER"]).USER_ID.ToString();
                bool Isadmin = Classes.CRMClass.ISAdmin(TID, uidd);
                if (Isadmin == false)
                {
                    btnEditLable.Visible = false;
                }
                ((Sales_Master)Page.Master).removeHD();

                if (Request.QueryString["ID"] != null)
                {
                    int MyTransID = Convert.ToInt32(Request.QueryString["ID"]);
                    QueryString(MyTransID);
                    return;
                }

                FistTimeLoad();
                //removeHD();
                CalcCashDilevery(TID);
                ViewState["TempListICTR_DT"] = ViewState["AddnewItems"] = ViewState["StrMultiSize"] = null;
                BindData(); //calling the HD and Binding By Passing the Parameter First Row
                GridData();//Bind GridData
                ManageLang();
                //Add Click

                //Edit Click
                //Delete Click
                //Print Click
            }
            objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            CURRENCY = objtblsetupsalesh.COUNTRYID.ToString();
            CURRENTCONVRATE = DB.tblCOUNTRies.SingleOrDefault(p => p.COUNTRYID == objtblsetupsalesh.COUNTRYID && p.TenentID == TID).CURRENTCONVRATE;

            string ToplblAllowMinusQty = objtblsetupsalesh.AllowMinusQty == false ? "Quantity in Minus Not Allowed" : "Allow Minus Quantity";

            string TranType = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid).transsubtype1;
            lbllabellist.Text = TranType + " List";
            lbllistname.Text = TranType + " - " + ToplblAllowMinusQty;

        }




        public string getprodnameui(int SID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == SID && p.TenentID == TID).Count() > 0)
            {
                string ProductCode = DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).UserProdID;
                return ProductCode;
            }
            else
            {
                return "Record Not Found";
            }

        }

        public string getLocation(int LID1)
        {
            if (DB.TBLLOCATIONs.Where(p => p.LOCATIONID == LID1 && p.TenentID == TID).Count() > 0)
            {
                string Locationname = DB.TBLLOCATIONs.Single(p => p.LOCATIONID == LID1 && p.TenentID == TID).LOCNAME1;
                return Locationname;
            }
            else
            {
                return "Record Not Found";
            }
        }

        public string getUom(int UOM)
        {
            if (DB.ICUOMs.Where(p => p.UOM == UOM && p.TenentID == TID).Count() > 0)
            {
                string UomName = DB.ICUOMs.Single(p => p.UOM == UOM && p.TenentID == TID).UOMNAME1;
                return UomName;
            }
            else
            {
                return "Record Not Found";
            }

        }
        public void SessionLoad()
        {

            //TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            //UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            //EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            //LangID = Session["LANGUAGE"].ToString();
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

        }
        public void FistTimeLoad()
        {
            objMyCompany = ((MYCOMPANYSETUP)Session["objMYCOMPANYSETUP"]);
            if (objMyCompany.StockTaking == true)
            {
                btnnewAdd.Visible = false;
            }
            else
            {
                btnnewAdd.Visible = true;
            }

            //            Login global define
            //Lang 1/2/3, countryID, CurrencyId, USERID as Admin
            string USERID = UID.ToString();
            Session["Invoice"] = null;
            penalAddDTITem.Visible = false;
            ListITtemDT.Visible = true;
            objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            CURRENCY = objtblsetupsalesh.COUNTRYID.ToString();
            CURRENTCONVRATE = DB.tblCOUNTRies.SingleOrDefault(p => p.COUNTRYID == objtblsetupsalesh.COUNTRYID && p.TenentID == TID).CURRENTCONVRATE;
            FirstFlag = false;

            // Remain ACM Work
        }
        public void GridData()
        {
            SessionLoad();
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            // create glogle function
            //var list = Classes.EcommAdminClass.grdPOICTR_HD(TID, objtblsetupsalesh.transid, objtblsetupsalesh.transsubid);
            //grdPO.DataSource = list.Where(p => p.Status == "SO" || p.Status == "DSO").OrderByDescending(p => p.TRANSDATE);
            //grdPO.DataBind();

            List<Database.ICTR_HD> ListDraft = DB.ICTR_HD.Where(p => p.TenentID == TID && (p.Status == "DSO" && p.transid == Transid && p.transsubid == Transsubid)).ToList();

            if (ListDraft.Count() > 0)
            {
                string msg = "";
                panelMsg.Visible = true;
                foreach (Database.ICTR_HD items in ListDraft.OrderBy(p => p.MYTRANSID))
                {
                    msg += items.MYTRANSID + ", ";
                }
                lblErreorMsg.Text = "Your transaction " + msg + " Saved as Draft you may comeback to complete anytime.";
            }


            grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && ((p.Status == "SO" || p.Status == "DSO") && p.transid == Transid && p.transsubid == Transsubid)).OrderByDescending(p => p.MYTRANSID);// p.ACTIVE==true &&
            grdPO.DataBind();
            for (int i = 0; i < grdPO.Items.Count; i++)
            {
                Label lblMYTRANSID = (Label)grdPO.Items[i].FindControl("lblMYTRANSID");
                stMYTRANSID = Convert.ToInt32(lblMYTRANSID.Text);
                if (i == 0)
                {
                    BindHD(stMYTRANSID);

                    //BindDT(stMYTRANSID);yogesh
                    BindDTui(stMYTRANSID);
                    readMode();
                }
                LinkButton Print = (LinkButton)grdPO.Items[i].FindControl("Print");
                LinkButton lnkbtnPurchase_Order = (LinkButton)grdPO.Items[i].FindControl("lnkbtnPurchase_Order");
                Label lbluserid = (Label)grdPO.Items[i].FindControl("lbluserid");
                Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                LinkButton lnkbtndelete = (LinkButton)grdPO.Items[i].FindControl("lnkbtndelete");

                //Image Imagedelete = (Image)grdPO.Items[i].FindControl("Imagedelete");

                string Steate = Status(stMYTRANSID); //lblStatus.Text;
                if (Steate == "Draft")
                {
                    Print.Visible = true;
                    lnkbtnPurchase_Order.Visible = true;
                    lnkbtndelete.Visible = true;
                }

                if (Steate == "Final")
                {
                    Print.Visible = true;
                }
                //if (objtblsetupsalesh.SalesAdminID == UID)
                //{
                //    if (Steate == "SO") lnkbtnPurchase_Order.Visible = true;
                //}
                //else
                //{
                //    if (Steate == "SO") lnkbtnPurchase_Order.Visible = false;
                //}


                string Active = Status(stMYTRANSID);
                if (Active == "Deleted")
                {
                    Print.Visible = true;
                    lnkbtndelete.Visible = false;
                }

            }
            readMode();
        }
        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");

                DropDownList drppaymentMeted = (DropDownList)e.Item.FindControl("drppaymentMeted");
                Classes.EcommAdminClass.getdropdown(drppaymentMeted, TID, "Payment", "Method", "Inventeri", "REFTABLE");
                //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "REFTABLE");
                //drppaymentMeted.DataSource = DB.REFTABLEs.Where(p => p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method");
                //drppaymentMeted.DataTextField = "REFNAME1";
                //drppaymentMeted.DataValueField = "REFID";
                //drppaymentMeted.DataBind();
                drppaymentMeted.SelectedValue = "1250001";
                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    drppaymentMeted.SelectedValue = ID.ToString();
                }
            }
        }
        public void BindDTui(int MYTRANSID)
        {
            // create glogle function
            List<Database.ICTR_DT> List = Classes.EcommAdminClass.getListICTR_DT(TID, MYTRANSID).ToList();
            decimal TottalSum = 0;

            if (List.Count() > 0)
            {
                Repeater2.DataSource = List.Where(p => p.MYSYSNAME == "SAL");
                Repeater2.DataBind();

                TottalSum = Convert.ToDecimal(List.Sum(p => p.AMOUNT));
                int QTUtotal = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                lblqtytotl.Text = QTUtotal.ToString();
                decimal UNPTOL = Convert.ToDecimal(List.Sum(p => p.UNITPRICE));
                lblUNPtotl.Text = UNPTOL.ToString();
                decimal TaxTol = Convert.ToDecimal(List.Sum(p => p.TAXPER));
                lblTotatotl.Text = TottalSum.ToString();

                Repeater3.DataSource = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID);
                Repeater3.DataBind();

                ViewState["ListICTR_DT"] = List;
            }
            else
            {
                List<ICTR_DT> listtemp = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYSYSNAME == "SAL").ToList();

                Repeater2.DataSource = listtemp;
                Repeater2.DataBind();
                //Session["TempListICTR_DT"] = List;
                TottalSum = Convert.ToDecimal(listtemp.Sum(p => p.AMOUNT));
                //txttotxl.Text = TottalSum.ToString();yogesh
                int QTUtotal = Convert.ToInt32(listtemp.Sum(p => p.QUANTITY));
                lblqtytotl.Text = QTUtotal.ToString();
                decimal UNPTOL = Convert.ToDecimal(listtemp.Sum(p => p.UNITPRICE));
                lblUNPtotl.Text = UNPTOL.ToString();
                decimal TaxTol = Convert.ToDecimal(listtemp.Sum(p => p.TAXPER));
                lblTotatotl.Text = TottalSum.ToString();

                Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
                TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
                if (btnAddDT.Text == "Save")
                {
                    ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
                }

                Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                Repeater3.DataBind();


                for (int i = 0; i < Repeater3.Items.Count; i++)
                {
                    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    txtammunt.Text = TottalSum.ToString();
                    TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");


                    if (drppaymentMeted.SelectedValue == "1250001")
                    {
                        string payment = drppaymentMeted.SelectedItem.ToString();
                        txtrefresh.Text = payment + "," + MYTRANSID;
                    }
                }
                ViewState["TempListICTR_DT"] = listtemp;

            }
            //panelRed.Visible = false;
        }
        public void BindHD(int MYTRANSID)
        {
            //Move to globle function
            //remove functoin and create globle function to direct use
            //Session["Invoice"] = MYTRANSID+1;
            string ToplblAllowMinusQty = objtblsetupsalesh.AllowMinusQty == false ? "Quantity in Minus Not Allowed" : "Allow Minus Quantity";


            string TranType = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid).transsubtype1;
            lbllabellist.Text = TranType + " List";
            lbllistname.Text = TranType + " - " + ToplblAllowMinusQty;
            // create glogle function
            var list = Classes.EcommAdminClass.grdPOICTR_HD(TID, Transid, Transsubid).ToList();
            var OBJICTR_HD = list.SingleOrDefault(p => p.MYTRANSID == MYTRANSID && (p.Status == "SO" || p.Status == "DSO"));
            if (OBJICTR_HD != null)
            {
                drpselsmen.SelectedValue = OBJICTR_HD.ExtraField2;
                CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
                // create glogle function

                // TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                txtLocationSearch.Text = Classes.EcommAdminClass.CompnyName_TBLCOMPANYSETUP(TID, CID);
                HiddenField3.Value = CID.ToString();


                if (OBJICTR_HD.TransDocNo != null || OBJICTR_HD.TransDocNo != "")
                {
                    SubSerialNo.Text = OBJICTR_HD.TransDocNo.ToString();
                }

                if (OBJICTR_HD.USERBATCHNO != null || OBJICTR_HD.USERBATCHNO == "")
                {
                    txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
                }
                if (OBJICTR_HD.TRANSDATE != null)
                {
                    txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
                }
                if (OBJICTR_HD.MYTRANSID != null)
                {
                    txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
                }
                if (OBJICTR_HD.REFERENCE != null && OBJICTR_HD.REFERENCE == "")
                {
                    txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
                }
                if (OBJICTR_HD.NOTES != null && OBJICTR_HD.NOTES == "")
                {
                    txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
                }
                if (OBJICTR_HD.TOTAMT != null)
                {
                    txttotxl.Text = OBJICTR_HD.TOTAMT.ToString();
                }



                // TextBox2.Text = OBJICTR_HD.TransDocNo.ToString();
                //if (OBJICTR_HD.Terms != null && OBJICTR_HD.Terms == 0)
                //{
                //    drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
                //}
                //else
                //{
                //    drpterms.SelectedValue = "2114";
                //}
            }

        }

        protected void CheckRole()
        {
            if (EMPID == 0)
            {
                drpselsmen.Enabled = true;
            }
            else
            {
                drpselsmen.SelectedValue = EMPID.ToString();
                if (objtblsetupsalesh.SalesAdminID == UID)
                    drpselsmen.Enabled = true;
                else
                    drpselsmen.Enabled = false;
            }
        }

        public void GetShow()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-2  getshow";
            ///lblProduct1s.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblReference1s.Attributes["class"] =  "control-label col-md-3  getshow";// lblTerms1s.Attributes["class"] = //lblNo1s.Attributes["class"] = lblQuantity1s.Attributes["class"] = lblUnitofMeasure1s.Attributes["class"] = ;
             lblTotal1s.Attributes["class"] = "control-label col-md-12  getshow";
            lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  gethide";
            //lblProduct2h.Attributes["class"] = "control-label col-md-2 gethide";
            lblSupplier2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = lblTransactionDate2h.Attributes["class"] = lblNo2h.Attributes["class"] = "control-label col-md-3 gethide";// lblUnitofMeasure2h.Attributes["class"] = lblTerms2h.Attributes["class"] =
            lblTotal2h.Attributes["class"] = "control-label col-md-12 gethide";
            lblNotes2h.Attributes["class"] = "control-label col-md-1 gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");
        }
        public void GetHide()
        {
            lblNotes1s.Attributes["class"] = "control-label col-md-1  gethide";
            //lblProduct1s.Attributes["class"] = "control-label col-md-2  gethide";
            lblSupplier1s.Attributes["class"] = lblBatchNo1s.Attributes["class"] = lblTransactionDate1s.Attributes["class"] = lblReference1s.Attributes["class"] = "control-label col-md-3  gethide";//lblNo1s.Attributes["class"] = lblQuantity1s.Attributes["class"] =lblUnitofMeasure1s.Attributes["class"] = lblTerms1s.Attributes["class"] = 
            lblTotal1s.Attributes["class"] = "control-label col-md-12  gethide";
            lblSupplierName1s.Attributes["class"] = lblOrderDate1s.Attributes["class"] = lblTotalAmount1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblEdit11s.Attributes["class"] = lblAddReference1s.Attributes["class"] = lblReferenceType1s.Attributes["class"] = lblReferenceNo1s.Attributes["class"] = lblSendMail1s.Attributes["class"] = lblToRecipients1s.Attributes["class"] = lblCC1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblSupplierName2h.Attributes["class"] = lblOrderDate2h.Attributes["class"] = lblTotalAmount2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblEdit12h.Attributes["class"] = lblAddReference2h.Attributes["class"] = lblReferenceType2h.Attributes["class"] = lblReferenceNo2h.Attributes["class"] = lblSendMail2h.Attributes["class"] = lblToRecipients2h.Attributes["class"] = lblCC2h.Attributes["class"] = "control-label col-md-4  getshow";
            //lblProduct2h.Attributes["class"] = "control-label col-md-2  getshow";
            lblSupplier2h.Attributes["class"] = lblBatchNo2h.Attributes["class"] = lblReference2h.Attributes["class"] = lblTransactionDate2h.Attributes["class"] = lblNo2h.Attributes["class"] = "control-label col-md-3  getshow";//lblUnitofMeasure2h.Attributes["class"] = lblTerms2h.Attributes["class"] =
            lblTotal2h.Attributes["class"] = "control-label col-md-12  getshow";
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
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblNo2h.Visible = lblBatchNo2h.Visible = lblReference2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = false;//lblProduct2h.Visible = lblUnitofMeasure2h.Visible = lblTerms2h.Visible = 
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtNo2h.Visible = txtBatchNo2h.Visible = txtReference2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = true;//txtProduct2h.Visible = txtUnitofMeasure2h.Visible = txtTerms2h.Visible = 
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    lblSupplier2h.Visible = lblTransactionDate2h.Visible = lblNo2h.Visible = lblBatchNo2h.Visible = lblReference2h.Visible = lblTotal2h.Visible = lblNotes2h.Visible = lblSupplierName2h.Visible = lblOrderDate2h.Visible = lblTotalAmount2h.Visible = lblStatus2h.Visible = lblEdit12h.Visible = lblAddReference2h.Visible = lblReferenceType2h.Visible = lblReferenceNo2h.Visible = lblSendMail2h.Visible = lblToRecipients2h.Visible = lblCC2h.Visible = true;//lblProduct2h.Visible = lblUnitofMeasure2h.Visible = lblTerms2h.Visible = 
                    txtSupplier2h.Visible = txtTransactionDate2h.Visible = txtNo2h.Visible = txtBatchNo2h.Visible = txtReference2h.Visible = txtTotal2h.Visible = txtNotes2h.Visible = txtSupplierName2h.Visible = txtOrderDate2h.Visible = txtTotalAmount2h.Visible = txtStatus2h.Visible = txtEdit12h.Visible = txtAddReference2h.Visible = txtReferenceType2h.Visible = txtReferenceNo2h.Visible = txtSendMail2h.Visible = txtToRecipients2h.Visible = txtCC2h.Visible = false;//txtProduct2h.Visible = txtUnitofMeasure2h.Visible = txtTerms2h.Visible = 
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblBatchNo1s.Visible = lblReference1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = false;//lblNo1s.Visible = lblProduct1s.Visible = lblUnitofMeasure1s.Visible = lblQuantity1s.Visible = lblTerms1s.Visible = 
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtBatchNo1s.Visible = txtReference1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = true;//txtNo1s.Visible = txtProduct1s.Visible = txtUnitofMeasure1s.Visible = txtQuantity1s.Visible = txtTerms1s.Visible = 
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    lblSupplier1s.Visible = lblTransactionDate1s.Visible = lblBatchNo1s.Visible = lblReference1s.Visible = lblTotal1s.Visible = lblNotes1s.Visible = lblSupplierName1s.Visible = lblOrderDate1s.Visible = lblTotalAmount1s.Visible = lblStatus1s.Visible = lblEdit11s.Visible = lblAddReference1s.Visible = lblReferenceType1s.Visible = lblReferenceNo1s.Visible = lblSendMail1s.Visible = lblToRecipients1s.Visible = lblCC1s.Visible = true;//lblNo1s.Visible = lblProduct1s.Visible = lblUnitofMeasure1s.Visible = lblQuantity1s.Visible = lblTerms1s.Visible = 
                    txtSupplier1s.Visible = txtTransactionDate1s.Visible = txtBatchNo1s.Visible = txtReference1s.Visible = txtTotal1s.Visible = txtNotes1s.Visible = txtSupplierName1s.Visible = txtOrderDate1s.Visible = txtTotalAmount1s.Visible = txtStatus1s.Visible = txtEdit11s.Visible = txtAddReference1s.Visible = txtReferenceType1s.Visible = txtReferenceNo1s.Visible = txtSendMail1s.Visible = txtToRecipients1s.Visible = txtCC1s.Visible = false;// txtNo1s.Visible = txtProduct1s.Visible = txtUnitofMeasure1s.Visible = txtQuantity1s.Visible = txtTerms1s.Visible = 
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
                //else if (lblNo1s.ID == item.LabelID)
                //    txtNo1s.Text = lblNo1s.Text = item.LabelName;
                else if (lblReference1s.ID == item.LabelID)
                    txtReference1s.Text = lblReference1s.Text = item.LabelName;
                //else if (lblTerms1s.ID == item.LabelID)
                //    txtTerms1s.Text = lblTerms1s.Text = item.LabelName;
                else if (lblTotal1s.ID == item.LabelID)
                    txtTotal1s.Text = lblTotal1s.Text = item.LabelName;
                else if (lblNotes1s.ID == item.LabelID)
                    txtNotes1s.Text = lblNotes1s.Text = item.LabelName;
                //else if (lblProduct1s.ID == item.LabelID)
                //    txtProduct1s.Text = lblProduct1s.Text = item.LabelName;
                //else if (lblUnitofMeasure1s.ID == item.LabelID)
                //    txtUnitofMeasure1s.Text = lblUnitofMeasure1s.Text = item.LabelName;
                //else if (lblQuantity1s.ID == item.LabelID)
                //    txtQuantity1s.Text = lblQuantity1s.Text = item.LabelName;
                //else if (lblUnitPricelocal1s.ID == item.LabelID)
                //    txtUnitPricelocal1s.Text = lblUnitPricelocal1s.Text = item.LabelName;
                //else if (lblDiscount1s.ID == item.LabelID)
                //    txtDiscount1s.Text = lblDiscount1s.Text = item.LabelName;
                //else if (lblTotalCurrencyLocal1s.ID == item.LabelID)
                //    txtTotalCurrencyLocal1s.Text = lblTotalCurrencyLocal1s.Text = item.LabelName;
                //else if (lblProductCodeProductName1s.ID == item.LabelID)
                //    txtProductCodeProductName1s.Text = lblProductCodeProductName1s.Text = item.LabelName;
                //else if (lblQuantity11s.ID == item.LabelID)
                //    txtQuantity11s.Text = lblQuantity11s.Text = item.LabelName;
                //else if (lblUnitPrice1s.ID == item.LabelID)
                //    txtUnitPrice1s.Text = lblUnitPrice1s.Text = item.LabelName;
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
                else if (lblReference2h.ID == item.LabelID)
                    txtReference2h.Text = lblReference2h.Text = item.LabelName;
                //else if (lblTerms2h.ID == item.LabelID)
                //    txtTerms2h.Text = lblTerms2h.Text = item.LabelName;
                else if (lblTotal2h.ID == item.LabelID)
                    txtTotal2h.Text = lblTotal2h.Text = item.LabelName;
                else if (lblNotes2h.ID == item.LabelID)
                    txtNotes2h.Text = lblNotes2h.Text = item.LabelName;
                //else if (lblProduct2h.ID == item.LabelID)
                //    txtProduct2h.Text = lblProduct2h.Text = item.LabelName;
                //else if (lblUnitofMeasure2h.ID == item.LabelID)
                //    txtUnitofMeasure2h.Text = lblUnitofMeasure2h.Text = item.LabelName;
                //else if (lblQuantity2h.ID == item.LabelID)
                //    txtQuantity2h.Text = lblQuantity2h.Text = item.LabelName;
                //else if (lblUnitPricelocal2h.ID == item.LabelID)
                //    txtUnitPricelocal2h.Text = lblUnitPricelocal2h.Text = item.LabelName;
                //else if (lblDiscount2h.ID == item.LabelID)
                //    txtDiscount2h.Text = lblDiscount2h.Text = item.LabelName;
                //else if (lblTotalCurrencyLocal2h.ID == item.LabelID)
                //    txtTotalCurrencyLocal2h.Text = lblTotalCurrencyLocal2h.Text = item.LabelName;
                //else if (lblProductCodeProductName2h.ID == item.LabelID)
                //    txtProductCodeProductName2h.Text = lblProductCodeProductName2h.Text = item.LabelName;
                //else if (lblQuantity12h.ID == item.LabelID)
                //    txtQuantity12h.Text = lblQuantity12h.Text = item.LabelName;
                //else if (lblUnitPrice2h.ID == item.LabelID)
                //    txtUnitPrice2h.Text = lblUnitPrice2h.Text = item.LabelName;
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
                //else if (lblNo1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtNo1s.Text;
                else if (lblReference1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReference1s.Text;
                //else if (lblTerms1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTerms1s.Text;
                else if (lblTotal1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotal1s.Text;
                else if (lblNotes1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes1s.Text;
                //else if (lblProduct1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtProduct1s.Text;
                //else if (lblUnitofMeasure1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUnitofMeasure1s.Text;
                //else if (lblQuantity1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity1s.Text;
                //else if (lblUnitPricelocal1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPricelocal1s.Text;
                //else if (lblDiscount1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDiscount1s.Text;
                //else if (lblTotalCurrencyLocal1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTotalCurrencyLocal1s.Text;
                //else if (lblProductCodeProductName1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtProductCodeProductName1s.Text;
                //else if (lblQuantity11s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity11s.Text;
                //else if (lblUnitPrice1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPrice1s.Text;
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
                else if (lblReference2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReference2h.Text;
                //else if (lblTerms2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTerms2h.Text;
                else if (lblTotal2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotal2h.Text;
                else if (lblNotes2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNotes2h.Text;
                //else if (lblProduct2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtProduct2h.Text;
                //else if (lblUnitofMeasure2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUnitofMeasure2h.Text;
                //else if (lblQuantity2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity2h.Text;
                //else if (lblUnitPricelocal2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPricelocal2h.Text;
                //else if (lblDiscount2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDiscount2h.Text;
                //else if (lblTotalCurrencyLocal2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTotalCurrencyLocal2h.Text;
                //else if (lblProductCodeProductName2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtProductCodeProductName2h.Text;
                //else if (lblQuantity12h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtQuantity12h.Text;
                //else if (lblUnitPrice2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUnitPrice2h.Text;
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
            ds.WriteXml(Server.MapPath("\\Sales\\xml\\tbl_sleshoder.xml"));
        }
        public void ManageLang()
        {
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
            drpReceipe.Enabled = rbtPsle.Enabled = false;//ddlProduct.Enabled = ddlUOM.Enabled = drpterms.Enabled =
            chbDeliNote.Enabled = drpselsmen.Enabled = false;
            //txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTotalCurrencyLocal.Enabled = false;//txtDescription.Enabled = txtQuantity.Enabled = 
            txtLocationSearch.Enabled = false;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = SubSerialNo.Enabled = txtNoteHD.Enabled = txttotxl.Enabled = false;
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
            BTNsAVEcONFsA.Visible = btnSubmit.Visible = btnConfirmOrder.Visible = btnPrint.Visible = Custmer.Visible = btnSaveData.Visible = btnrefesh.Visible = BTnEdit.Visible = false;
            //BTNsAVEcONFsA.Visible = btnSubmit.Visible =
            btnaddamunt.Enabled = false;
            btnNew.Visible = btnnewAdd.Visible = true;
            btnAddItemsIn.Enabled = btndiscartitems.Enabled = false;// btnAddDT.Enabled =
        }
        public void WrietMode()
        {
            drpReceipe.Enabled = rbtPsle.Enabled = true;//ddlProduct.Enabled = ddlUOM.Enabled = drpterms.Enabled = 
            chbDeliNote.Enabled = drpselsmen.Enabled = true;
            //txtUPriceLocal.Enabled = txtDiscount.Enabled = txtTotalCurrencyLocal.Enabled = true;//txtDescription.Enabled = txtQuantity.Enabled = 
            txtLocationSearch.Enabled = true;
            txtOrderDate.Enabled = txtBatchNo.Enabled = txtrefreshno.Enabled = txtTraNoHD.Enabled = SubSerialNo.Enabled = txtNoteHD.Enabled = true;
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
            //btnConfirmOrder.Visible =btnSubmit.Visible=  BTNsAVEcONFsA.Visible= false;//yogesh
            btnPrint.Visible = btnSaveData.Visible = Custmer.Visible = btnSubmit.Visible = BTNsAVEcONFsA.Visible = true;
            btnNew.Visible = false;
            btnnewAdd.Visible = false;
            btnrefesh.Visible = true;
            btnAddItemsIn.Enabled = btndiscartitems.Enabled = true;//btnAddDT.Enabled =
        }
        public void QueryString(int ID)
        {
            int MYTID = ID;
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).Count() > 0)
            {
                Database.ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                int SID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
                txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COMPNAME1;
                HiddenField3.Value = OBJICTR_HD.CUSTVENDID.ToString();
                //drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
                txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
                txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
                txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
                SubSerialNo.Text = OBJICTR_HD.TransDocNo.ToString();
                txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
                txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
                var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MYTID && p.TenentID == TID).ToList();
                if (listcost.Count() > 0)
                {
                    Repeater3.DataSource = listcost;
                    Repeater3.DataBind();
                }
                BindDTui(MYTID);
                readMode();
            }
        }
        public void LastData()
        {
            if (DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && (p.Status == "SO" || p.Status == "DSO")).Count() > 0)
            {

                int Tarjictio = Convert.ToInt32(DB.ICTR_HD.Where(p => p.ACTIVE == true && p.TenentID == TID && (p.Status == "SO" || p.Status == "DSO")).Max(p => p.MYTRANSID));
                int MYTID = Tarjictio;
                if (DB.ICTR_HD.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).Count() > 0)
                {
                    Database.ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                    int SID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
                    txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COMPNAME1;
                    HiddenField3.Value = OBJICTR_HD.CUSTVENDID.ToString();
                    //drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
                    txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
                    txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
                    txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
                    SubSerialNo.Text = OBJICTR_HD.TransDocNo.ToString();
                    txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
                    txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
                    var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MYTID && p.TenentID == TID).ToList();
                    if (listcost.Count() > 0)
                    {
                        Repeater3.DataSource = listcost;
                        Repeater3.DataBind();
                    }

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
                    //btnAddItemsIn.Visible = true;
                    //btnAddDT.Visible = false;
                    //BindDT(MYTID);yogesh
                    BindDTui(MYTID);
                    readMode();
                    //panelRed.Visible = false;
                }
            }
            readMode();
        }
        public void BindData()
        {
            Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            //Classes.EcommAdminClass.getdropdown(drpterms, TID, "Terms", "Terms", "Eco", "REFTABLE");

            List<Database.tbl_Receipe> ListReceipe = new List<Database.tbl_Receipe>();
            List<Database.tbl_Receipe> Listrec = DB.tbl_Receipe.Where(p => p.TenentID == TID).ToList();

            foreach(Database.tbl_Receipe items in Listrec)
            {
                if(DB.Receipe_Menegement.Where(p=>p.TenentID==TID && p.recNo == items.recNo).Count()>0)
                {
                    ListReceipe.Add(items);
                }
            }

            drpReceipe.DataSource = ListReceipe.ToList();
            drpReceipe.DataTextField = "Receipe_English";
            drpReceipe.DataValueField = "recNo";
            drpReceipe.DataBind();
            drpReceipe.Items.Insert(0, new ListItem("-- Select --", "0"));

            //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
            Classes.EcommAdminClass.getdropdown(drpRefnstype, TID, "Reference", "RefSubType", "Sales", "REFTABLE");
            //Create a Glogle Fuction
            Classes.EcommAdminClass.getdropdown(drpselsmen, TID, LID.ToString(), "", "", "tbl_Employee");
            //drpselsmen.DataSource = DB.tbl_Employee.Where(p => p.TanentId == TID && p.LocationID == LID && p.Deleted == false);
            //drpselsmen.DataTextField = "firstname";
            //drpselsmen.DataValueField = "employeeID";
            //drpselsmen.DataBind();
            //drpselsmen.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Salesman--", "0"));
            Classes.EcommAdminClass.getdropdown(drpChkinEmp, TID, UID.ToString(), LID.ToString(), LangID.ToString(), "GlobleEmployee");
            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Repeater3.DataBind();
            CheckRole();
            //BindProduct();
            if (rbtPsle.SelectedValue == "2")
            {
                //drpterms.SelectedValue = "2114";
                int COMPID = Convert.ToInt32(objtblsetupsalesh.DeftCoustomer);
                txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == COMPID).COMPNAME1;
            }
            txtBatchNo.Text = "99999";
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCounrty(string prefixText, int count)
        {
            int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
            string conStr;
            conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
            string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
            //string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and BUYER = 'true' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
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

        protected void btnSubmit_Click(object sender, EventArgs e)// dRAFF BUTTON
        {

            ActionDraff();
            Session["GetMYTRANSID"] = null;
            Session["Invoice"] = null;
            Response.Redirect("invoiceposforStaff.aspx?transid=4101&transsubid=410106");

        }

        public void ActionDraff()
        {
            int MTID = Convert.ToInt32(Session["GetMYTRANSID"]);
            List<ICTR_DT> listTempDt = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MTID).ToList();
            List<ICTRPayTerms_HD> listicpayHD = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MTID).ToList();
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";


            if (listTempDt.Count() <= 0)
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "save atlest one item";
                return;
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                int COMPID = 826667;
                string Status = "";

                string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID && p.TenentID == TID).Count() == 1)
                    COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID && p.TenentID == TID).COMPID;
                if (Session["Invoice"] != null)
                {

                    TransNo = Convert.ToInt32(Session["Invoice"]);
                    var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    //var List5 = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
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
                    int COMPANYID = Convert.ToInt32(CURRENCY);
                    if (DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).Count() == 0)
                    {
                        panelMsg.Visible = true;
                        lblErreorMsg.Text = "Kindly please select minimum one Items; to save Invoice";
                        return;
                    }

                    //foreach (Database.ICTR_DT item in List5)
                    //{
                    //    DB.ICTR_DT.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}
                    //var List6 = Classes.EcommAdminClass.getListICTR_DTEXT(TID, TransNo).ToList();
                    //var List6 = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).ToList();
                    //foreach (Database.ICTR_DTEXT item in List6)
                    //{
                    //    DB.ICTR_DTEXT.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}

                    //List<ICTR_DT> ListDate = ((List<Database.ICTR_DT>)Session["TempListICTR_DT"]).ToList();
                    List<ICTR_DT> ListDate = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    //int DXMYID = DB.ICTR_DTEXT.Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Max(p => p.MyID) + 1) : 1;
                    int MyRunningSerial = 0;
                    var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID).ToList();
                    //int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                    //List<Database.ICTR_DTEXT> ListICTR_DTEXT = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).ToList();
                    //if (ListICTR_DTEXT.Count() > 0)
                    //{
                    //    MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MyID == MYIDDT).Count() > 0 ? Convert.ToInt32(List6.Where(p => p.MyID == MYIDDT).Max(p => p.MyRunningSerial) + 1) : 1;
                    //}
                    //else
                    //{
                    //    MyRunningSerial = 1;
                    //}

                    //foreach (Database.ICTR_DT items in ListDate)
                    //{

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
                        Label lblMYID = (Label)Repeater2.Items[i].FindControl("lblMYID");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);//lblDISPER.Text
                        decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);//lblDISAMT.Text
                        //var listICTR_DT = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
                        //int MYIDDT = listICTR_DT.Count() > 0 ? Convert.ToInt32(listICTR_DT.Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        int MyProdID = Convert.ToInt32(lblProductNameItem.Text);//lblProductNameItem.Text
                        int MYID = Convert.ToInt32(lblMYID.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = 0;
                        string DESCRIPTION = lblDiscription.Text;
                        string UOM = lblUOM.Text;
                        int QUANTITY = Convert.ToInt32(lblDisQnty.Text); //Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);//
                        decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);//lblTotalCurrency.Text
                        string BATCHNO = "1";
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(0.0);
                        decimal TAXPER = Convert.ToDecimal(0.0);
                        decimal PROMOTIONAMT = Convert.ToDecimal(0.000);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.000);
                        DateTime EXPIRYDATE = DateTime.Now;
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";

                        int Uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MyProdID).UOM);

                        decimal CostAmount = Classes.EcommAdminClass.CostAmount(TID, MyProdID, LID, Uom);

                        //start Insert in ICTR_DT

                        //Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                        //objICTR_DT.TenentID = TenentID;
                        //objICTR_DT.MYTRANSID = MYTRANSID;
                        //objICTR_DT.locationID = locationID;

                        //objICTR_DT.MYID = MYIDDT;
                        //objICTR_DT.MyProdID = MyProdID;
                        //objICTR_DT.REFTYPE = REFTYPE;
                        //objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                        //objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                        //objICTR_DT.MYSYSNAME = MYSYSNAME;
                        //objICTR_DT.JOID = JOID;
                        //objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                        //objICTR_DT.ACTIVITYID = ACTIVITYID;
                        //objICTR_DT.DESCRIPTION = DESCRIPTION;
                        //objICTR_DT.UOM = UOM;
                        //objICTR_DT.QUANTITY = QUANTITY;
                        //objICTR_DT.UNITPRICE = UNITPRICE;
                        //objICTR_DT.AMOUNT = AMOUNT;
                        //objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                        //objICTR_DT.BATCHNO = BATCHNO;
                        //objICTR_DT.BIN_ID = BIN_ID;
                        //objICTR_DT.BIN_TYPE = BIN_TYPE;
                        //objICTR_DT.GRNREF = GRNREF;
                        //objICTR_DT.DISPER = DISPER;
                        //objICTR_DT.DISAMT = DISAMT;
                        //objICTR_DT.TAXAMT = TAXAMT;
                        //objICTR_DT.TAXPER = TAXPER;
                        //objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                        //objICTR_DT.GLPOST = GLPOST;
                        //objICTR_DT.GLPOST1 = GLPOST1;
                        //objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                        //objICTR_DT.GLPOSTREF = GLPOSTREF;
                        //objICTR_DT.ICPOST = ICPOST;
                        //objICTR_DT.ICPOSTREF = ICPOSTREF;
                        //objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DT.COMPANYID = COMPANYID1;
                        //objICTR_DT.SWITCH1 = SWITCH1;
                        //objICTR_DT.ACTIVE = ACTIVE;
                        //objICTR_DT.DelFlag = DelFlag;
                        //objICTR_DT.CostAmount = CostAmount;


                        //DB.ICTR_DT.AddObject(objICTR_DT);
                        //DB.SaveChanges();

                        //insert complate
                        Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTRANSID, LID, MYID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                        //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);

                        int MyID = MYID;

                        decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                        int DELIVERDLOCATenentID = Convert.ToInt32(0);
                        int QUANTITYDELIVERD = Convert.ToInt32(0);
                        decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                        string ACCOUNTID = "1".ToString();
                        string Remark = "Data Insert formDtext".ToString();
                        int TransNo1 = Convert.ToInt32(0);

                        //ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        //objICTR_DTEXT.TenentID = TenentID;
                        //objICTR_DTEXT.MYTRANSID = MYTRANSID;
                        //objICTR_DTEXT.MyID = MyID;
                        //objICTR_DTEXT.MyRunningSerial = MyRunningSerial;
                        //objICTR_DTEXT.CURRENTCONVRATE = CURRENTCONVRATE;
                        //objICTR_DTEXT.CURRENCY = CURRENCY;
                        //objICTR_DTEXT.OTHERCURAMOUNT = OTHERCURAMOUNT;

                        //objICTR_DTEXT.QUANTITY = QUANTITY;
                        //objICTR_DTEXT.UNITPRICE = UNITPRICE;
                        //objICTR_DTEXT.AMOUNT = AMOUNT;
                        //objICTR_DTEXT.QUANTITYDELIVERD = QUANTITYDELIVERD;
                        //// objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        //objICTR_DTEXT.ACCOUNTID = ACCOUNTID;
                        //objICTR_DTEXT.GRNREF = GRNREF;
                        //objICTR_DTEXT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DTEXT.Remark = Remark;
                        //objICTR_DTEXT.TransNo = TransNo1;
                        //objICTR_DTEXT.ACTIVE = ACTIVE;
                        //DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        //DB.SaveChanges();
                        //Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                        //MYIDDT++;
                        MyRunningSerial++;
                    }

                    //}
                    string REFERENCE = txtrefreshno.Text;
                    //int ToTenentID = objProFile.ToTenentID; ;
                    int TOLOCATIONID = LID;
                    //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    string LF = "L";
                    string ACTIVITYCODE = "0";
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;
                    //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
                    //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                    decimal TOTAMT, TOTQTY1 = 0;


                    TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);

                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = "99999";
                    string CounterID = objProFile.CounterID;
                    string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    string NOTES = txtNoteHD.Text;
                    string USERID = UseID.ToString();
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    DateTime UPDTTIME = Curentdatetime;
                    //int InvoiceNO = 0;
                    decimal Discount = Convert.ToDecimal(0);
                    Status = "DSO";// chbDeliNote.Checked == true ? "RDSO" : "SO";
                    int Terms = 2114; // Convert.ToInt32(drpterms.SelectedValue);
                    string DatainserStatest = "Update";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "1";
                    }
                    else if (rbtPsle.SelectedValue == "2")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    }
                    string BillNo = drpselsmen.SelectedValue;
                    //string InvoiceNo = Classes.EcommAdminClass.TransDocNo(TID, Transid, Transsubid);
                    string TransDocNo = "";

                    List<Database.ICTR_HD> ListHDD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    int Doc = Convert.ToInt32(ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo);
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

                    ////////////////////////*****/////////////////
                    DateTime Todate = DateTime.Now;
                    Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();
                    bool flag = false;
                    if (DatainserStatest == "Add")
                    {
                        objICTR_HD = new Database.ICTR_HD();
                        flag = true;
                        objICTR_HD.InvoiceNO = InvoiceNo.ToString();

                    }
                    else
                    {
                        objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                    }
                    objICTR_HD.TenentID = TenentID;
                    objICTR_HD.MYTRANSID = MYTRANSID;
                    //objICTR_HD.ToTenentID = ToTenentID;
                    objICTR_HD.locationID = TOLOCATIONID;
                    objICTR_HD.MainTranType = MainTranType;
                    objICTR_HD.TransType = TranType;
                    objICTR_HD.transid = Transid;
                    objICTR_HD.transsubid = Transsubid;
                    objICTR_HD.TranType = TranType;
                    objICTR_HD.COMPID = COMPID;
                    objICTR_HD.MYSYSNAME = MYSYSNAME;
                    objICTR_HD.CUSTVENDID = Convert.ToDecimal(CUSTVENDID);
                    objICTR_HD.LF = LF;
                    objICTR_HD.PERIOD_CODE = PERIOD_CODE;
                    objICTR_HD.ACTIVITYCODE = ACTIVITYCODE;
                    objICTR_HD.MYDOCNO = MYDOCNO;
                    objICTR_HD.USERBATCHNO = USERBATCHNO;
                    objICTR_HD.TOTAMT = TOTAMT;
                    objICTR_HD.TOTQTY = TOTQTY1;
                    objICTR_HD.AmtPaid = AmtPaid;
                    objICTR_HD.PROJECTNO = PROJECTNO;
                    objICTR_HD.CounterID = CounterID;
                    objICTR_HD.PrintedCounterInvoiceNo = PrintedCounterInvoiceNo;
                    objICTR_HD.JOID = JOID;
                    objICTR_HD.TRANSDATE = Convert.ToDateTime(TRANSDATE);
                    objICTR_HD.REFERENCE = REFERENCE;
                    objICTR_HD.NOTES = NOTES;
                    objICTR_HD.GLPOST = GLPOST;
                    objICTR_HD.GLPOST1 = GLPOST1;
                    objICTR_HD.GLPOSTREF = GLPOSTREF;
                    objICTR_HD.GLPOSTREF1 = GLPOSTREF1;
                    objICTR_HD.ICPOST = ICPOST;
                    objICTR_HD.ICPOSTREF = ICPOSTREF;
                    objICTR_HD.USERID = USERID;
                    objICTR_HD.ACTIVE = true;
                    //objICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //objICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    objICTR_HD.COMPANYID = COMPANYID;
                    objICTR_HD.ENTRYDATE = ENTRYDATE;
                    objICTR_HD.ENTRYTIME = ENTRYTIME;
                    objICTR_HD.UPDTTIME = UPDTTIME;
                    objICTR_HD.Discount = Discount;
                    objICTR_HD.Status = Status;
                    objICTR_HD.Terms = Terms;
                    objICTR_HD.ExtraField1 = Custmerid;
                    objICTR_HD.ExtraField1 = Custmerid;
                    objICTR_HD.ExtraField2 = BillNo;
                    objICTR_HD.ExtraSwitch1 = Swit1;
                    objICTR_HD.RefTransID = MYTRANSID;
                    objICTR_HD.TransDocNo = InvoiceNo;

                    // objICTR_HD.ExtraSwitch2 = "";
                    if (flag == true)
                    {
                        DB.ICTR_HD.AddObject(objICTR_HD);
                    }
                    DB.SaveChanges();

                    ////////////////////////*****/////////////////
                    //List<Database.ICTR_HD> ListHDD1 = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

                    var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                    if (SirialNO == InvoiceNo)
                    {
                        Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                        objtblsubtype.serialno = SirialNO;
                        DB.SaveChanges();
                    }




                    //Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, Transid, Transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    //var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    //foreach (Database.ICTRPayTerms_HD item in List2)
                    //{
                    //    DB.ICTRPayTerms_HD.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}
                    //decimal Payment = Convert.ToDecimal(lblTotatotl1.Text);
                    //decimal Total = 0;


                    List<Database.ICTRPayTerms_HD> ListHD_TEMP = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                    decimal Payment = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Sum(p => p.AMOUNT));
                    decimal Total = 0;

                    //foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP)
                    //{
                    //    decimal Ammunt = Convert.ToDecimal(items.Amount);
                    //    if (Ammunt != null)
                    //    {
                    //        decimal TXT = Convert.ToDecimal(Ammunt);
                    //        Total = Total + TXT;
                    //    }
                    //}

                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text != "")
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }

                    List<Database.ICTRPayTerms_HD> ListHD_TEMP1 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                    if (Payment == Total)
                    {
                        //foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP1)
                        //{
                        //    int PaymentTermsId = items.PaymentTermsId;
                        //    string IDRefresh = items.ReferenceNo;
                        //    string IdApprouv = items.ApprovalID;
                        //    decimal ammunt = Convert.ToDecimal(items.Amount);

                        //    if (ammunt != null)
                        //    {

                        //        int AccountID = 0;
                        //        Decimal Amount = Convert.ToDecimal(ammunt);
                        //        **************************************//

                        //        Database.ICTRPayTerms_HD objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID);
                        //        objICTRPayTerms_HD.TenentID = TID;
                        //        objICTRPayTerms_HD.MyTransID = MYTRANSID;
                        //        objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(PaymentTermsId);
                        //        objICTRPayTerms_HD.AccountID = 0;
                        //        objICTRPayTerms_HD.Amount = Convert.ToDecimal(ammunt);
                        //        objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                        //        objICTRPayTerms_HD.ApprovalID = IdApprouv;
                        //        objICTRPayTerms_HD.Draft = true;
                        //        DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                        //        DB.SaveChanges();


                        //        ***************************************//
                        //        Classes.EcommAdminClass.insertICTRPayTerms_HD(TID1111, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                        //    }
                        //}
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
                            {
                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();

                                int PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                                int AccountID = 0;
                                Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                                Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
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
                    GridData();
                    btnSaveData.Text = "Confirm Order";
                    btnConfirmOrder.Text = "Confirm Order";
                    Session["Invoice"] = null;
                }
                else
                {
                    TransNo = Convert.ToInt32(Session["GetMYTRANSID"]);
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    //int ToTenentID = objProFile.ToTenentID;
                    int TOLOCATIONID = LID;
                    //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    Database.tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objtbltranssubtype.transsubtype1;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    string MYSYSNAME = "SAL".ToString();
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    string PERIOD_CODE = OICODID;
                    string ACTIVITYCODE = "0";
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;
                    //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
                    //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                    decimal TOTAMT, TOTQTY1 = 0;
                    if (DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).Count() == 0)
                    {
                        panelMsg.Visible = true;
                        lblErreorMsg.Text = "Kindly please select minimum one Items; to save Invoice";
                        return;
                    }

                    TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);

                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = "99999";
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
                    int COMPANYID = Convert.ToInt32(CURRENCY);
                    bool ACTIVE = Convert.ToBoolean(true);
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    int Terms = 2114; // Convert.ToInt32(drpterms.SelectedValue);
                    DateTime UPDTTIME = Curentdatetime;
                    //var inno = DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.InvoiceNO);
                    //long inno1 = Convert.ToInt64(inno);
                    //long inno2 = (inno1) + 1;
                    //string InvoiceNO = inno2.ToString();

                    //int InvoiceNO = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.InvoiceNO) + 1) : 1; 
                    decimal Discount = Convert.ToDecimal(0);
                    Status = "DSO";//chbDeliNote.Checked == true ? "RDSO" : "SO";
                    string DatainserStatest = "Add";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "1";
                    }
                    else if (rbtPsle.SelectedValue == "2")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    }
                    string BillNo = drpselsmen.SelectedValue;

                    //string TransDocNo = "";
                    //var listtbltranssubtypes1 = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    //if (listtbltranssubtypes1.Count() > 0)
                    //{
                    //    int SirialNO1 = (Convert.ToInt32(listtbltranssubtypes1.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1);
                    //    string SirialNO11 = SirialNO1.ToString();
                    //    TransDocNo = SirialNO11;
                    //}
                    //else
                    //    TransDocNo = "";
                    ////Dipak
                    //string InvoiceNo = TransDocNo;
                    string TransDocNo = "";
                    List<Database.ICTR_HD> ListHDD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    int Doc = Convert.ToInt32(ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo);
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

                    //***************************************************//

                    DateTime Todate = DateTime.Now;
                    //Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();
                    Database.ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                    bool flag = false;
                    if (DatainserStatest == "Add")
                    {
                        //objICTR_HD = new Database.ICTR_HD();
                        flag = true;
                        objICTR_HD.InvoiceNO = InvoiceNo.ToString();
                    }
                    else
                    {
                        objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                    }
                    objICTR_HD.TenentID = TenentID;
                    objICTR_HD.MYTRANSID = MYTRANSID;
                    //objICTR_HD.ToTenentID = ToTenentID;
                    objICTR_HD.locationID = TOLOCATIONID;
                    objICTR_HD.MainTranType = MainTranType;
                    objICTR_HD.TransType = TranType;
                    objICTR_HD.transid = Transid;
                    objICTR_HD.transsubid = Transsubid;
                    objICTR_HD.TranType = TranType;
                    objICTR_HD.COMPID = COMPID;
                    objICTR_HD.MYSYSNAME = MYSYSNAME;
                    objICTR_HD.CUSTVENDID = Convert.ToDecimal(CUSTVENDID);
                    objICTR_HD.LF = "L";
                    objICTR_HD.PERIOD_CODE = PERIOD_CODE;
                    objICTR_HD.ACTIVITYCODE = ACTIVITYCODE;
                    objICTR_HD.MYDOCNO = MYDOCNO;
                    objICTR_HD.USERBATCHNO = USERBATCHNO;
                    objICTR_HD.TOTAMT = TOTAMT;
                    objICTR_HD.TOTQTY = TOTQTY1;
                    objICTR_HD.AmtPaid = AmtPaid;
                    objICTR_HD.PROJECTNO = PROJECTNO;
                    objICTR_HD.CounterID = CounterID;
                    objICTR_HD.PrintedCounterInvoiceNo = PrintedCounterInvoiceNo;
                    objICTR_HD.JOID = JOID;
                    objICTR_HD.TRANSDATE = Convert.ToDateTime(TRANSDATE);
                    objICTR_HD.REFERENCE = REFERENCE;
                    objICTR_HD.NOTES = NOTES;
                    objICTR_HD.GLPOST = GLPOST;
                    objICTR_HD.GLPOST1 = GLPOST1;
                    objICTR_HD.GLPOSTREF = GLPOSTREF;
                    objICTR_HD.GLPOSTREF1 = GLPOSTREF1;
                    objICTR_HD.ICPOST = ICPOST;
                    objICTR_HD.ICPOSTREF = ICPOSTREF;
                    objICTR_HD.USERID = USERID;
                    objICTR_HD.ACTIVE = true;
                    //objICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //objICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    objICTR_HD.COMPANYID = COMPANYID;
                    objICTR_HD.ENTRYDATE = ENTRYDATE;
                    objICTR_HD.ENTRYTIME = ENTRYTIME;
                    objICTR_HD.UPDTTIME = UPDTTIME;
                    objICTR_HD.Discount = Discount;
                    objICTR_HD.Status = Status;
                    objICTR_HD.Terms = Terms;
                    objICTR_HD.ExtraField1 = Custmerid;
                    objICTR_HD.ExtraField2 = BillNo;
                    objICTR_HD.ExtraSwitch1 = Swit1;
                    objICTR_HD.RefTransID = MYTRANSID;
                    objICTR_HD.TransDocNo = InvoiceNo;

                    // objICTR_HD.ExtraSwitch2 = "";
                    if (flag == true)
                    {
                        //DB.ICTR_HD.AddObject(objICTR_HD);
                    }
                    DB.SaveChanges();
                    //***************************************************//

                    //Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, Transid, Transsubid, MYSYSNAME, COMPID1, CUSTVENDID, "L", PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    // insert deta is ICTR_DT

                    var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                    if (SirialNO == InvoiceNo)
                    {
                        Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                        objtblsubtype.serialno = SirialNO;
                        DB.SaveChanges();
                    }

                    //Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                    //objtblsubtype.serialno = SirialNO;

                    //DB.SaveChanges();


                    //List<ICTR_DT> ListDate = ((List<Database.ICTR_DT>)Session["TempListICTR_DT"]).ToList();
                    List<ICTR_DT> ListDate = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();

                    var listTCTR_DTEXT = Classes.EcommAdminClass.getListICTR_DTEXT(TID, TransNo).ToList();
                    var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID).ToList();
                    int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                    int DXMYID = listTCTR_DTEXT.Count() > 0 ? Convert.ToInt32(listTCTR_DTEXT.Max(p => p.MyID) + 1) : 1;
                    int MyRunningSerial = listTCTR_DTEXT.Where(p => p.MyID == DXMYID).Count() > 0 ? Convert.ToInt32(listTCTR_DTEXT.Where(p => p.MyID == DXMYID).Max(p => p.MyRunningSerial) + 1) : 1;

                    foreach (Database.ICTR_DT items in ListDate)
                    {
                        //for (int i = 0; i < Repeater2.Items.Count; i++)
                        //{
                        //Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem"),
                        // lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription"),
                        // lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM"),
                        // lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty"),
                        // lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis"),
                        // lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency"),
                        // lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE"),
                        // lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT"),
                        // lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER"),
                        // lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT");
                        decimal DISPER = Convert.ToDecimal(items.DISPER);//lblDISPER.Text
                        decimal DISAMT = Convert.ToDecimal(items.DISAMT);//lblDISAMT.Text
                        var listTCTR_DT = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
                        //int MYIDDT = listTCTR_DT.Count() > 0 ? Convert.ToInt32(listTCTR_DT.Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        //int MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        //string REFTYPE = "LF", REFSUBTYPE = "LF";

                        //int JOBORDERDTMYID = 1, ACTIVITYID = 0;

                        //string DESCRIPTION = lblDiscription.Text;
                        //string UOM = lblUOM.Text;
                        //int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        //decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        //decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);


                        int MyProdID = Convert.ToInt32(items.MyProdID);//lblProductNameItem.Text

                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = 0;
                        string DESCRIPTION = items.DESCRIPTION;//lblDiscription.Text;
                        string UOM = items.UOM;//lblUOM.Text;
                        int QUANTITY = Convert.ToInt32(items.QUANTITY); //Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(items.UNITPRICE);//
                        decimal AMOUNT = Convert.ToDecimal(items.AMOUNT);//lblTotalCurrency.Text
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.000);
                        string BATCHNO = "1";
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(0.0);
                        decimal TAXPER = Convert.ToDecimal(0.0);
                        decimal PROMOTIONAMT = Convert.ToDecimal(10.00);
                        DateTime EXPIRYDATE = DateTime.Now;
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";
                        int uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == MyProdID && p.LOCATION_ID == LID).UOM);
                        decimal CostAmount = Classes.EcommAdminClass.CostAmount(TID, MyProdID, LID, uom);

                        //******************************************//

                        Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                        objICTR_DT.TenentID = TenentID;
                        objICTR_DT.MYTRANSID = MYTRANSID;
                        objICTR_DT.locationID = locationID;

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
                        objICTR_DT.CostAmount = CostAmount;

                        DB.ICTR_DT.AddObject(objICTR_DT);
                        DB.SaveChanges();
                        //******************************************//
                        MYIDDT++;
                        //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);


                        int MyID = DXMYID;
                        decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                        int DELIVERDLOCATenentID = Convert.ToInt32(0);
                        int QUANTITYDELIVERD = Convert.ToInt32(0);
                        decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                        string ACCOUNTID = "1".ToString();
                        string Remark = "Data Insert formDtext".ToString();
                        int TransNo1 = Convert.ToInt32(0);


                        //ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        //objICTR_DTEXT.TenentID = TenentID;
                        //objICTR_DTEXT.MYTRANSID = MYTRANSID;
                        //objICTR_DTEXT.MyID = MyID;
                        //objICTR_DTEXT.MyRunningSerial = MyRunningSerial;
                        //objICTR_DTEXT.CURRENTCONVRATE = CURRENTCONVRATE;
                        //objICTR_DTEXT.CURRENCY = CURRENCY;
                        //objICTR_DTEXT.OTHERCURAMOUNT = OTHERCURAMOUNT;

                        //objICTR_DTEXT.QUANTITY = QUANTITY;
                        //objICTR_DTEXT.UNITPRICE = UNITPRICE;
                        //objICTR_DTEXT.AMOUNT = AMOUNT;
                        //objICTR_DTEXT.QUANTITYDELIVERD = QUANTITYDELIVERD;
                        //// objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        //objICTR_DTEXT.ACCOUNTID = ACCOUNTID;
                        //objICTR_DTEXT.GRNREF = GRNREF;
                        //objICTR_DTEXT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DTEXT.Remark = Remark;
                        //objICTR_DTEXT.TransNo = TransNo1;
                        //objICTR_DTEXT.ACTIVE = ACTIVE;
                        //DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        //DB.SaveChanges();
                        DXMYID++;
                        MyRunningSerial++;
                        //Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                    }
                    //decimal Payment = Convert.ToDecimal(lblTotatotl1.Text);
                    //decimal Total = 0;

                    List<Database.ICTRPayTerms_HD> ListHD_TEMP = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.Draft == false).ToList();

                    decimal Payment = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Sum(p => p.AMOUNT));
                    decimal Total = 0;

                    foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP)
                    {
                        decimal Ammunt = Convert.ToDecimal(items.Amount);
                        if (Ammunt != null)
                        {
                            decimal TXT = Convert.ToDecimal(Ammunt);
                            Total = Total + TXT;
                        }
                    }


                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text != "")
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }

                    List<Database.ICTRPayTerms_HD> ListHD_TEMP1 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.Draft == false).ToList();


                    //if (Payment == Total)
                    //{
                    //    foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP1)
                    //    {
                    //        int PaymentTermsId = items.PaymentTermsId;
                    //        string IDRefresh = items.ReferenceNo;
                    //        string IdApprouv = items.ApprovalID;
                    //        decimal ammunt = Convert.ToDecimal(items.Amount);

                    //        if (ammunt != null)
                    //        {

                    //            int AccountID = 0;
                    //            Decimal Amount = Convert.ToDecimal(ammunt);

                    //            Database.ICTRPayTerms_HD objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);
                    //            objICTRPayTerms_HD.TenentID = TID;
                    //            objICTRPayTerms_HD.LocationID = LID;
                    //            objICTRPayTerms_HD.MyTransID = MYTRANSID;
                    //            objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(PaymentTermsId);
                    //            objICTRPayTerms_HD.AccountID = 0;
                    //            objICTRPayTerms_HD.Amount = Convert.ToDecimal(ammunt);
                    //            objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    //            objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    //            objICTRPayTerms_HD.Draft = true;

                    //            objICTRPayTerms_HD.CounterID = CounterID;
                    //            objICTRPayTerms_HD.TransDate = Convert.ToDateTime(TRANSDATE);


                    //            //DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    //            DB.SaveChanges();






                    //            //Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                    //        }
                    //    }

                    //}


                    if (Payment == Total)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
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
                        //pnlSuccessMsg123.Visible = true;
                        //lblMsg123.Text = "Total Amount not Macth";
                        return;
                    }
                    GridData();
                    readMode();
                    int MsterCose = 0;
                    string REFNO = "";
                    //if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.COMPID == COMPID && p.MyID == TransNo).Count() > 0)
                    //{
                    //    Database.CRMMainActivity objCRMMainActivity = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.COMPID == COMPID && p.MyID == TransNo);
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
                BindHD(TransNo);
                scope.Complete();
            }

        }
        public int Pidalcode(DateTime Time)
        {
            int PODID = 0;
            var TBLPERIODS = DB.TBLPERIODS.Where(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).ToList();
            if (TBLPERIODS.Count() > 0)
                PODID = Convert.ToInt32(TBLPERIODS.Single(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).PERIOD_CODE);
            return PODID;
        }
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            FinalConfirm();
        }
        public void InsertCRMMainActivity(int COMPID1, int TransNo, string Status, string ADDACTIvity, string CamName, string Description)
        {
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
            int MDUID = 0;
            if (Session["SiteModuleID"] != null)
                MDUID = Convert.ToInt32(Session["SiteModuleID"]);
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
            Database.CRMMainActivity obj = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.COMPID == COMPID1 && p.MyID == TransNo);
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
            //hidUPriceLocal.Value = hidTotalCurrencyLocal.Value = "";
            //Response.Redirect(Request.RawUrl, false);
            Response.Redirect("../ECOMM/AdminIndex.aspx");
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
            DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = Pidalcode(TACtionDate).ToString();
            if (HiddenField3.Value == "0" || HiddenField3.Value == "")
            {
                //pnlSuccessMsg123.Visible = true;
                //lblMsg123.Text = "Select Customer";

            }
            else
            {

                //int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                // Classes.EcommAdminClass.BindProdu(PID, ddlUOM, txtDescription, txtserchProduct, TID);
                // var ObjProduct=DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                //decimal PRiesh = Convert.ToDecimal(ObjProduct.msrp);
                //Session["MultiTransactionProdObj"]=ObjProduct;
                //txtUPriceLocal.Text = PRiesh.ToString();
                //txtQuantity.Text = "1";
                //lblmultiuom.Visible = false;  //Come From Multitransaction
                //lblmulticolor.Visible = false;
                //lblmultisize.Visible = false;
                //lblmultiperishable.Visible = false;
                //lblmultibinstore.Visible = false;
                //lblmultiserialize.Visible = false;
                //TotalPrice();yogesh

                string FullDicption = "";
                string Woranti = "";
                lnkUomConversion.Visible = false;
                int PID = Convert.ToInt32(ddlProduct.SelectedValue);

                Database.TBLPRODUCT objprod = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID);
                string DIScrip = objprod.DescToprint;
                //if (objprod.Warranty != null)
                //{
                //    Woranti = objprod.Warranty;
                //    FullDicption = DIScrip + "\n " + " Warranty : " + Woranti;
                //}
                //else
                //{
                //    Woranti = "0";
                //    FullDicption = DIScrip + "\n " + " Warranty : " + Woranti + " Month; One Day to check in working condition Only";
                //}
                //FullDicption = DIScrip + "\n" + " Warranty : " + Woranti + " Month; One Day to check in working condition Only";
                txtserchProduct.Text = objprod.BarCode;
                txtDescription.Text = FullDicption!=""? FullDicption: DIScrip;


                int uom = Convert.ToInt32(objprod.UOM);
                Boolean MultiUOM = Convert.ToBoolean(objprod.MultiUOM);

                if (MultiUOM == Convert.ToBoolean(1))
                {
                    ddlUOM.Enabled = true;
                    ViewState["PID"] = PID;

                    //if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID ).Count() > 0)
                    //{
                    //    lnkUomConversion.Visible = true;
                    //}

                    //lnkUomConversion.Attributes.Add("target", "_blank");
                    //lnkUomConversion.PostBackUrl = "../ECOMM/UOM_Qty_Convertion.aspx?PID=" + PID;

                    List<Database.ICUOM> ListUOm1 = new List<ICUOM>();
                    List<Database.View_ICBR_Product> listmultiuom1 = DB.View_ICBR_Product.Where(p => p.TenentID == TID && p.MyProdID == PID && p.LocationID == LID && p.OnHand > 0).ToList();

                    Database.ICUOM obj = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == uom).FirstOrDefault();
                    ListUOm1.Add(obj);

                    foreach (Database.View_ICBR_Product Itemuo in listmultiuom1)
                    {
                        Database.ICUOM objuom = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == Itemuo.UOM);
                        ListUOm1.Add(objuom);
                    }
                    ddlUOM.DataSource = ListUOm1.Where(p => p.TenentID == TID);
                    ddlUOM.DataTextField = "UOMNAME1";
                    ddlUOM.DataValueField = "UOM";
                    ddlUOM.DataBind();
                    ddlUOM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Unit of Measure--", "0"));
                    //if(ddlUOM.Items.Count > 0)
                    //    ddlUOM.SelectedValue = ListUOm1.First().UOM.ToString();

                    if (ddlUOM.Items.Count > 1)
                        ddlUOM.SelectedIndex = 1;

                    int UOMSEL = Convert.ToInt32(ddlUOM.SelectedValue);
                    if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).Count() > 0)
                    {
                        lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).OnHand.ToString();
                    }
                }
                else
                {
                    Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
                    if (ddlUOM.Items.Count > 0)
                        ddlUOM.SelectedValue = uom.ToString();
                    ddlUOM.Enabled = false;

                    if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).Count() > 0)
                    {
                        lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).OnHand.ToString();
                    }
                    else
                    {
                        lblAvailableQty.Text = "Available Qty : 0";
                    }
                }

                txtQuantity.Text = "1";
                Boolean MultiBinStore = Convert.ToBoolean(objprod.MultiBinStore);
                Boolean Perishable = Convert.ToBoolean(objprod.Perishable);
                Boolean Serialized = Convert.ToBoolean(objprod.Serialized);
                Boolean MultiColor = Convert.ToBoolean(objprod.MultiColor);
                Boolean MultiSize = Convert.ToBoolean(objprod.MultiSize);
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

                if (Serialized == Convert.ToBoolean(1))
                {
                    lblmultiserialize.Visible = true;  //Come from MultiTransaction
                    if (txtQuantity.Text == "0")
                    { }
                    else
                    {
                        int TexQty = Convert.ToInt32(txtQuantity.Text);
                        int UOMSEL = Convert.ToInt32(ddlUOM.SelectedValue);
                        int QTY = Convert.ToInt32(DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).OnHand);
                        List<Database.ICIT_BR_Serialize> ListSerialize = new List<ICIT_BR_Serialize>();
                        List<Database.ICIT_BR_Serialize> Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.Active == "Y").OrderBy(p => p.Serial_Number).ToList();

                        int rqty = 0;
                        foreach (Database.ICIT_BR_Serialize items in Listserl)
                        {
                            ListSerialize.Add(items);
                        }
                        if (QTY <= TexQty)
                        {
                            int avelable = Listserl.Count();
                            rqty = TexQty - avelable;

                            for (int p = 0; p <= rqty - 1; p++)
                            {
                                Database.ICIT_BR_Serialize objseri = new ICIT_BR_Serialize();
                                ListSerialize.Add(objseri);
                            }
                        }


                        ViewState["ListofSerlNumber"] = ListSerialize;
                        listSerial.DataSource = ListSerialize;  //Come from MultiTransaction
                        listSerial.DataBind();

                        if (QTY <= TexQty)
                        {
                            for (int p = 1; p <= rqty; p++)
                            {
                                TextBox txtlistSerial = (TextBox)listSerial.Items[p].FindControl("txtlistSerial");
                                txtlistSerial.Enabled = true;
                            }
                        }
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
                //if (MultiUOM == Convert.ToBoolean(1))
                //{
                //    lblmultiuom.Visible = true;  //Come from MultiTransaction
                //    var Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                //    lidtUom.DataSource = Listserl;
                //    lidtUom.DataBind();

                //}
                //else
                //{
                //    lblmultiuom.Visible = false;  //Come from MultiTransaction
                //}

                int PIDPRICE = Convert.ToInt32(ddlProduct.SelectedValue);
                int UOMPRICE = Convert.ToInt32(ddlUOM.SelectedValue);
                List<Database.ICITEMS_PRICE> ListPrice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == PIDPRICE && p.UOM == UOMPRICE).ToList();

                if (ListPrice.Count() > 0)
                {
                    txtUPriceLocal.Text = ListPrice.Single(p => p.TenentID == TID && p.MYPRODID == PIDPRICE && p.UOM == UOMPRICE).onlinesale1.ToString();
                }
                else
                {
                    txtUPriceLocal.Text = "0";
                }
            }
        }

        protected void btndiscartitems_Click(object sender, EventArgs e)
        {
            if (btndiscartitems.Text == "Exit")
            {
                Response.Redirect(Request.RawUrl);
            }

            if (ViewState["AddnewItems"] != null)
            {
                penalAddDTITem.Visible = false;
                ListITtemDT.Visible = true;
                ViewState["AddnewItems"] = null;
                btndiscartitems.Text = "Exit";
                btnAddDT.Visible = false;
                btnAddItemsIn.Visible = true;
                txtserchProduct.Enabled = true;
                btnserchAdvans.Enabled = true;
                ddlProduct.Enabled = true;
                lnkBTNSAVE.Text = "Save";
                btnAddDT.Text = "Save";
                lblMsg123.Text = "";
                pnlSuccessMsg123.Visible = false;
                cleardt();
            }
            else
            {
                ddlProduct.Items.Clear();
                ddlUOM.SelectedIndex = 0;
                cleardt();
                txtQuantity.Text = txtUPriceLocal.Text = txtDescription.Text = txtTotalCurrencyLocal.Text = "";
                lblMsg123.Text = lblitemsearch.Text = "";
                txtDiscount.Text = "0";
                txtserchProduct.Text = "";
                pnlSuccessMsg123.Visible = false;

            }
        }
        
        protected void btnAddDT_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";

            int PID = Convert.ToInt32(ddlProduct.SelectedValue);
            int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
            int AVALABLE = 0;
            if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.UOM == UOM).Count() > 0)
                AVALABLE = Convert.ToInt32(DB.ICIT_BR.Single(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.UOM == UOM).OnHand);
            int ENTQTY = Convert.ToInt32(txtQuantity.Text);

            if (objtblsetupsalesh.AllowMinusQty == false)
            {
                if (ENTQTY > AVALABLE)
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Plase Eeter less or Equal AVALABLE Qty (" + AVALABLE + ")";
                    return;
                }
            }

            Session["UserTotal"] = null;
            MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);

            Database.TBLPRODUCT OBJRPOD = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID);

            if (OBJRPOD.Serialized == Convert.ToBoolean(1))
            {
                int sirialize = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MyProdID == PID && p.MYTRANSID == MYTRANSID && p.RecodName == "Serialize").Count();
                if (sirialize <= 0)
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Plase select Serialize from List";
                    return;
                }
            }

            if (OBJRPOD.MultiColor == Convert.ToBoolean(1))
            {
                int sirialize = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MyProdID == PID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiColor").Count();
                if (sirialize <= 0)
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Plase select MultiColor from List";
                    return;
                }
            }

            if (OBJRPOD.MultiSize == Convert.ToBoolean(1))
            {
                int sirialize = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MyProdID == PID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiSize").Count();
                if (sirialize <= 0)
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Plase select MultiSize from List";
                    return;
                }
            }

            if (OBJRPOD.MultiBinStore == Convert.ToBoolean(1))
            {
                int sirialize = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MyProdID == PID && p.MYTRANSID == MYTRANSID && p.RecodName == "MultiBIN").Count();
                if (sirialize <= 0)
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Plase select MultiBIN from List";
                    return;
                }
            }

            if (OBJRPOD.Perishable == Convert.ToBoolean(1))
            {
                int sirialize = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MyProdID == PID && p.MYTRANSID == MYTRANSID && p.RecodName == "Perishable").Count();
                if (sirialize <= 0)
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Plase select Perishable from List";
                    return;
                }
            }

            int COMPID = 826667;
            string Status = "DSO";

            string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
            DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
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
            string REFERENCE = txtrefreshno.Text;
            //int ToTenentID = objProFile.ToTenentID; ;
            int TOLOCATIONID = LID;
            //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
            tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
            string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
            decimal COMPID1 = COMPID;
            decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
            string LF = "L";
            string ACTIVITYCODE = "0";
            decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
            string USERBATCHNO = txtBatchNo.Text;
            //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
            //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
            decimal TOTAMT, TOTQTY1 = 0;

            decimal AmtPaid = objProFile.AmtPaid;
            string PROJECTNO = "99999";
            string CounterID = objProFile.CounterID;
            string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
            DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
            string NOTES = txtNoteHD.Text;
            string USERID = UseID.ToString();
            DateTime Curentdatetime = DateTime.Now;
            DateTime ENTRYDATE = Curentdatetime;
            DateTime ENTRYTIME = Curentdatetime;
            DateTime UPDTTIME = Curentdatetime;
            decimal Discount = Convert.ToDecimal(0);
            int Terms = 2114;// Convert.ToInt32(drpterms.SelectedValue);

            string Custmerid = "";
            string Swit1 = "";
            if (rbtPsle.SelectedValue == "1")
            {
                Custmerid = txtLocationSearch.Text;
                Swit1 = "1";
            }
            else if (rbtPsle.SelectedValue == "2")
            {
                Custmerid = txtLocationSearch.Text;
                Swit1 = "2";
            }
            string BillNo = drpselsmen.SelectedValue;

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


            if (ICTR_HDCount < 1)
            {
                Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();

                objICTR_HD.TenentID = TID;
                objICTR_HD.MYTRANSID = MYTRANSID;
                //objICTR_HD.ToTenentID = ToTenentID;
                objICTR_HD.locationID = LID;
                objICTR_HD.MainTranType = MainTranType;
                objICTR_HD.TransType = TranType;
                objICTR_HD.transid = Transid;
                objICTR_HD.transsubid = Transsubid;
                objICTR_HD.TranType = TranType;
                objICTR_HD.COMPID = COMPID;
                objICTR_HD.MYSYSNAME = MYSYSNAME;
                objICTR_HD.CUSTVENDID = Convert.ToDecimal(CUSTVENDID);
                objICTR_HD.LF = "L";
                objICTR_HD.PERIOD_CODE = PERIOD_CODE;
                objICTR_HD.ACTIVITYCODE = ACTIVITYCODE;
                objICTR_HD.MYDOCNO = MYDOCNO;
                objICTR_HD.USERBATCHNO = USERBATCHNO;
                objICTR_HD.TOTAMT = Convert.ToDecimal(lblTotatotl.Text); ;
                objICTR_HD.TOTQTY = Convert.ToInt32(lblqtytotl.Text); ;
                objICTR_HD.AmtPaid = AmtPaid;
                objICTR_HD.PROJECTNO = PROJECTNO;
                objICTR_HD.CounterID = CounterID;
                objICTR_HD.PrintedCounterInvoiceNo = PrintedCounterInvoiceNo;
                objICTR_HD.JOID = JOID;
                objICTR_HD.TRANSDATE = Convert.ToDateTime(TRANSDATE);
                objICTR_HD.REFERENCE = REFERENCE;
                objICTR_HD.NOTES = NOTES;
                objICTR_HD.GLPOST = GLPOST;
                objICTR_HD.GLPOST1 = GLPOST1;
                objICTR_HD.GLPOSTREF = GLPOSTREF;
                objICTR_HD.GLPOSTREF1 = GLPOSTREF1;
                objICTR_HD.ICPOST = ICPOST;
                objICTR_HD.ICPOSTREF = ICPOSTREF;
                objICTR_HD.USERID = USERID;
                objICTR_HD.ACTIVE = true;
                //objICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                //objICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                objICTR_HD.COMPANYID = COMPANYID;
                objICTR_HD.ENTRYDATE = ENTRYDATE;
                objICTR_HD.ENTRYTIME = ENTRYTIME;
                objICTR_HD.UPDTTIME = UPDTTIME;
                objICTR_HD.Discount = Discount;
                objICTR_HD.Status = Status;
                objICTR_HD.Terms = Terms;
                objICTR_HD.ExtraField1 = Custmerid;
                objICTR_HD.ExtraField1 = Custmerid;
                objICTR_HD.ExtraField2 = BillNo;
                objICTR_HD.ExtraSwitch1 = Swit1;
                objICTR_HD.RefTransID = MYTRANSID;
                objICTR_HD.TransDocNo = InvoiceNo;
                objICTR_HD.InvoiceNO = InvoiceNo.ToString();

                DB.ICTR_HD.AddObject(objICTR_HD);

                DB.SaveChanges();

                var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                if (SirialNO == InvoiceNo)
                {
                    Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                    objtblsubtype.serialno = SirialNO;
                    DB.SaveChanges();
                }
            }



            if (btnAddDT.Text == "Save")
            {

                // btnPenel.Visible = true;
                ViewState["btnPenel"] = true;

                // tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);




                decimal TotalAmut = 0;
                decimal LUINTP = 0;
                bool fleg = true;
                List<Database.ICTR_DT> TemplistICTR_DT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();//create By Parimal
                int MYIDCUNT = TemplistICTR_DT.Count() > 0 ? Convert.ToInt32(TemplistICTR_DT.Max(p => p.MYID) + 1) : 1;
                if (Session["Invoice"] != null)
                {
                    var ListTotle = DB.ICTR_HD.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).ToList();
                    lblqtytotl.Text = Convert.ToInt32(ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTQTY).ToString();
                    lblUNPtotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                    lblTotatotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                }
                List<Database.ICTR_DT> ListDate = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                if (ListDate.Count() > 0)
                {
                    TempListICTR_DT123 = ListDate;
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
                //MoviesDBDataContext.ObjectTrackingEnabled = false;

                Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();
                objICTR_DT.TenentID = TID;
                objICTR_DT.locationID = LID;
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
                objICTR_DT.MYSYSNAME = "SAL";

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

                TempListICTR_DT123.Add(objICTR_DT);
                DB.ICTR_DT.AddObject(objICTR_DT);
                DB.SaveChanges();
                //TempListICTR_DT123.Add(objICTR_DT);
                //Session["TempListICTR_DT"] = TempListICTR_DT;
                Repeater2.DataSource = TempListICTR_DT123;
                Repeater2.DataBind();

                //Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
                //TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
                ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
                int ItemCount = TempListICTR_DT123.Count();
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
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        txtammunt.Text = GTL1.ToString();
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
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        txtammunt.Text = GTLTOL.ToString();
                        TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");


                        if (drppaymentMeted.SelectedValue == "1250001")
                        {
                            string payment = drppaymentMeted.SelectedItem.ToString();
                            txtrefresh.Text = payment + "," + MYTRANSID;
                        }

                    }
                }

                BindTempDT(MYTRANSID);


                string UserTotal = lblqtytotl.Text + "," + lblUNPtotl.Text + "," + lblTotatotl.Text;

                ListITtemDT.Visible = true;
                penalAddDTITem.Visible = false;
                MYTRANSID = Convert.ToInt32(objICTR_DT.MYTRANSID);

            }
            if (btnAddDT.Text == "Update")
            {
                ListITtemDT.Visible = true;
                // btnPenel.Visible = true;
                ViewState["btnPenel"] = true;
                decimal TotalAmut = 0;
                decimal LUINTP = 0;
                bool fleg = true;
                //var listICTR_DT = DB.ICTR_DT.Where(p => p.TenentID == TID);//create By Parimal
                List<Database.ICTR_DT> TemplistICTR_DT = DB.ICTR_DT.Where(p => p.TenentID == TID).ToList();//create By Parimal
                //int MYIDCUNT = TemplistICTR_DT.Count() > 0 ? Convert.ToInt32(TemplistICTR_DT.Max(p => p.MYID) + 1) : 1;
                //int MYIDCUNT = listICTR_DT.Count() > 0 ? Convert.ToInt32(listICTR_DT.Max(p => p.MYID) + 1) : 1;
                //if (Session["Invoice"] != null)
                //{
                //    var ListTotle = DB.ICTR_HD.Where(p => p.MYTRANSID == MYTRANSID).ToList();
                //    lblqtytotl.Text = Convert.ToInt32(ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTQTY).ToString();
                //    lblUNPtotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                //    lblTotatotl.Text = ListTotle.Single(p => p.MYTRANSID == MYTRANSID).TOTAMT.ToString();
                //}

                List<Database.ICTR_DT> ListDate = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                if (ListDate.Count() > 0)
                {
                    TempListICTR_DT123 = ListDate;
                    fleg = false;
                    //MYIDCUNT += MYIDCUNT + 1;


                }
                if (ViewState["DTTRCTIONID"] != null && ViewState["DTMYID"] != null)
                {
                    int str1 = Convert.ToInt32(ViewState["DTTRCTIONID"]);
                    int str2 = Convert.ToInt32(ViewState["DTMYID"]);
                    //TempListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).ToList();
                    //var List = TempListICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2).ToList();
                    //if (List.Count() > 0)
                    //{
                    //    foreach (Database.ICTR_DT item in List)
                    //    {
                    //        DB.ICTR_DT.DeleteObject(item);
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
                Database.ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.TenentID == TID);
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
                DB.SaveChanges();
                TempListICTR_DT123.Add(objICTR_DT);
                // Session["TempListICTR_DT"] = TempListICTR_DT;
                //Repeater2.DataSource = TempListICTR_DT;
                //Repeater2.DataBind();

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
                    txttotxl.Text = GTL1.ToString();
                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");


                        if (drppaymentMeted.SelectedValue == "1250001")
                        {
                            string payment = drppaymentMeted.SelectedItem.ToString();
                            txtrefresh.Text = payment + "," + MYTRANSID;
                        }

                    }
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
                    txttotxl.Text = GTLTOL.ToString();
                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");
                        txtammunt.Text = GTLTOL.ToString();

                        if (drppaymentMeted.SelectedValue == "1250001")
                        {
                            string payment = drppaymentMeted.SelectedItem.ToString();
                            txtrefresh.Text = payment + "," + MYTRANSID;
                        }

                    }
                }
                btnAddItemsIn.Visible = true;


                //LnkBtnSavePayTerm.Visible = true;
                string UserTotal = lblqtytotl.Text + "," + lblUNPtotl.Text + "," + lblTotatotl.Text;
                BindTempDT(MYTRANSID);

            }
            //******************HD update*************************/
            Database.ICTR_HD objICTR_HD1 = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID);


            objICTR_HD1.TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
            objICTR_HD1.TOTQTY = Convert.ToInt32(lblqtytotl.Text);
            //objICTR_HD1.CUSTVENDID = 2114;
            objICTR_HD1.Status = "DSO";
            //objICTR_HD1.USERBATCHNO = "123";
            objICTR_HD1.TRANSDATE = DateTime.Now;

            objICTR_HD1.ExtraField2 = drpselsmen.SelectedValue;
            objICTR_HD1.ACTIVE = true;
            DB.SaveChanges();
            //******************HD update*************************/


            btnSubmit.Enabled = true;
            btnPrint.Enabled = true;
            BTNsAVEcONFsA.Enabled = true;
            btnSaveData.Enabled = true;

            btnSubmit.Attributes["class"] = "btn blue";
            btnPrint.Attributes["class"] = "btn red";
            BTNsAVEcONFsA.Attributes["class"] = "btn blue";
            btnSaveData.Attributes["class"] = "btn yellow";

            btnSubmit.Attributes.Remove("disabled");
            btnPrint.Attributes.Remove("disabled");
            BTNsAVEcONFsA.Attributes.Remove("disabled");
            btnSaveData.Attributes.Remove("disabled");

            UpdatePanel2.Update();
            UpdatePanel3.Update();
            //string confirmation = @" document.getElementById(""ContentPlaceHolder1_btnSubmit"").enabled = true; ";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HideButtonScript", confirmation, true);

            //string confirmation1 = @" document.getElementById(""ContentPlaceHolder1_btnPrint"").enabled = true; ";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HideButtonScript", confirmation1, true);

            //string confirmation2 = @" document.getElementById(""ContentPlaceHolder1_BTNsAVEcONFsA"").enabled = true; ";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HideButtonScript", confirmation2, true);

            //string confirmation3 = @" document.getElementById(""ContentPlaceHolder1_btnSaveData"").enabled = true; ";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HideButtonScript", confirmation3, true);


            ListITtemDT.Visible = true;
            penalAddDTITem.Visible = false;
            lnkBTNSAVE.Text = "Save";
            btnAddDT.Text = "Save";
            lnkBTNSAVE.Enabled = false;
            txtserchProduct.Enabled = true;
            btnserchAdvans.Enabled = true;
            ddlProduct.Enabled = true;
            btndiscartitems.Text = "Exit";
            lblMsg123.Text = "";
            pnlSuccessMsg123.Visible = false;
            cleardt();
            //    scope.Complete();
            //}
            //BindDTui(MYTRANSID);
            //string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
            //ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
        }

        public void cleardt()
        {
            txtQuantity.Text = "1";
            txtUPriceLocal.Text = "0";
            txtDiscount.Text = "0";
            lnkUomConversion.Visible = false;
            tags_2.Text = "";
        }
        public void BindTempDT(int MYTRANSID)
        {
            decimal TottalSum = 0;

            List<ICTR_DT> listtemp = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYSYSNAME == "SAL").ToList();


            Repeater2.DataSource = listtemp;
            Repeater2.DataBind();
            //Session["TempListICTR_DT"] = List;
            TottalSum = Convert.ToDecimal(listtemp.Sum(p => p.AMOUNT));
            //txttotxl.Text = TottalSum.ToString();yogesh
            int QTUtotal = Convert.ToInt32(listtemp.Sum(p => p.QUANTITY));
            lblqtytotl.Text = QTUtotal.ToString();
            decimal UNPTOL = Convert.ToDecimal(listtemp.Sum(p => p.UNITPRICE));
            lblUNPtotl.Text = UNPTOL.ToString();
            decimal TaxTol = Convert.ToDecimal(listtemp.Sum(p => p.TAXPER));
            lblTotatotl.Text = TottalSum.ToString();

            Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            if (btnAddDT.Text == "Save")
            {
                ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            }

            Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Repeater3.DataBind();


            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                txtammunt.Text = TottalSum.ToString();
                TextBox txtrefresh = (TextBox)Repeater3.Items[i].FindControl("txtrefresh");
                DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[i].FindControl("drppaymentMeted");


                if (drppaymentMeted.SelectedValue == "1250001")
                {
                    string payment = drppaymentMeted.SelectedItem.ToString();
                    txtrefresh.Text = payment + "," + MYTRANSID;
                }
            }
            ViewState["TempListICTR_DT"] = listtemp;
        }

        protected void grdPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                SubSerialNo.Text = "";
                txtTraNoHD.Text = "";
                int MYTID = Convert.ToInt32(e.CommandArgument);

                EditSalesh(MYTID);
                btnAddItemsIn.Visible = true;
            }
            if (e.CommandName == "Btnview")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                EditSalesh(MYTID);
                readMode();
            }
            if (e.CommandName == "Delete")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                Database.ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTID && p.TenentID == TID);
                OBJICTR_HD.ACTIVE = false;
                DB.SaveChanges();
                var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).ToList();
                for (int i = 0; i < List.Count - 1; i++)
                {
                    List[i].ACTIVE = false;
                    DB.SaveChanges();
                }
                //var list1 = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTID && p.TenentID == TID).ToList();
                //for (int i = 0; i < list1.Count - 1; i++)
                //{
                //    list1[i].ACTIVE = false;
                //    DB.SaveChanges();
                //}
                //BindDT(MYTID);yogesh
                BindDTui(MYTID);
                GridData();
            }
            if (e.CommandName == "ReceiveProduct")
            {
                int MYTID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("GoofsTransferNotes.aspx?Tranjestion=" + MYTID);
            }
            if (e.CommandName == "btnprient")
            {
                string PrintURL = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).InvoicePrintURL;
                int MYTID = Convert.ToInt32(e.CommandArgument);
                if (MYTID != 0)
                {
                    //scope.Complete(); //  To commit.
                    if (TID == 7)
                        Response.Redirect("ServiesHubPrints.aspx?Tranjestion=" + MYTID);
                    else if (TID == 6)
                    {

                        string s = "window.open('" + "http://" + Request.Url.Authority + "/Sales/" + PrintURL + MYTID + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


                    }
                    else if (TID == 600)
                        Response.Redirect(PrintURL + MYTID);
                    else
                        Response.Redirect("LocalInvoiceForStaff.aspx?Tranjestion=" + MYTID);
                    //}
                }

            }
        }
        public void EditSalesh(int ID)
        {
            clearHDPanel();
            //BindData();
            btnPenel.Visible = true;
            Database.ICTR_HD OBJICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == ID && p.TenentID == TID);
            if (OBJICTR_HD.ExtraField2 != "")
                drpselsmen.SelectedValue = OBJICTR_HD.ExtraField2;
            CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
            Database.TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
            txtLocationSearch.Text = onj.COMPNAME1;
            HiddenField3.Value = CID.ToString();
            if (OBJICTR_HD.USERBATCHNO != null || OBJICTR_HD.USERBATCHNO != "")
            {
                txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
            }
            if (OBJICTR_HD.TRANSDATE != null)
            {
                txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
            }
            if (OBJICTR_HD.MYTRANSID != null)
            {
                txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
            }
            if (OBJICTR_HD.REFERENCE != null && OBJICTR_HD.REFERENCE != "")
            {
                txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
            }
            if (OBJICTR_HD.NOTES != null && OBJICTR_HD.NOTES != "")
            {
                txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
            }
            if (OBJICTR_HD.TransDocNo != null)
            {
                SubSerialNo.Text = OBJICTR_HD.TransDocNo.ToString();
            }
            if (OBJICTR_HD.TOTAMT != null)
            {
                txttotxl.Text = OBJICTR_HD.TOTAMT.ToString();
            }


            // TextBox2.Text = OBJICTR_HD.TransDocNo.ToString();
            //if (OBJICTR_HD.Terms != null && OBJICTR_HD.Terms == 0)
            //{
            //    drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
            //}
            //else
            //{
            //    drpterms.SelectedValue = "2114";
            //}


            var listcost = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == ID && p.TenentID == TID).ToList();
            if (listcost.Count() > 0)
            {
                ViewState["TempEco_ICCATEGORY"] = listcost;
                //Session["Invoice"] = ID;
                Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                Repeater3.DataBind();
            }
            else
            {
                Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"]; ;
                Repeater3.DataBind();
            }
            //btnAddItemsIn.Visible = true;
            // btnAddDT.Visible = false;
            //BindDT(ID);yogesh
            //BindDTui(ID);
            BindTempDT(ID);
            //ListItems.Visible = true;
            //panelRed.Visible = true;

            Session["Invoice"] = ID;
            ViewState["HDMYtrctionid"] = ID;
            Session["GetMYTRANSID"] = ID;
            btnSaveData.Text = "Save";
            btnConfirmOrder.Text = "Save";
            btnSaveData.Visible = true;
            //btnConfirmOrder.Visible = true;yogesh20042017

            if (OBJICTR_HD.ACTIVE == true)
            {
                string Statesh = OBJICTR_HD.Status;
                if (Statesh == "SO")
                    readMode();
                else
                    WrietMode();
            }
            else
            {
                WrietMode();
            }


        }
        public void BindProductui()
        {
            //Classes.EcommAdminClass.getdropdown(ddlProduct, TID, LID.ToString(), "", "", "TBLPRODUCTWithICIT_BR");
            //GetProductDescription();yogesh
            //var result2 = (from pm in DB.ICIT_BR.Where(a => a.OnHand > 0 && a.TenentID == TID && a.LocationID == LID)
            //               join Module in DB.TBLPRODUCTs.Where(c => c.TenentID == TID && c.ACTIVE == "1" && c.LOCATION_ID == LID) on pm.MyProdID equals Module.MYPRODID
            //               select new { p1 = Module.MYPRODID, p2 = Module.ProdName1 }).ToList();
            //ddlProduct.DataSource = result2;
            //ddlProduct.DataValueField = "p1";
            //ddlProduct.DataTextField = "p2";
            //ddlProduct.DataBind();
            //ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Product--", "0"));
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDT")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);


                if (DB.ICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).Count() > 0)
                {
                    Database.ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                    DB.DeleteObject(objICTR_DT);
                    //objICTR_DT.ACTIVE = false;
                    DB.SaveChanges();
                }
                if (DB.ICTR_DT.Where(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID).Count() > 0)
                {
                    Database.ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                    DB.DeleteObject(objICTR_DT);
                    //objICTR_DT.ACTIVE = false;
                    DB.SaveChanges();
                }
                BindTempDT(str1);
                //BindDT(str1);yogesh
            }
            if (e.CommandName == "editDT")
            {
                //BindProduct();yogesh
                string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(ID[0]);
                int str2 = Convert.ToInt32(ID[1]);
                ViewState["ItemeditDTMYTRANSIDMYID"] = str1.ToString() + "," + str2.ToString();
                //int str3 = Convert.ToInt32(ID[2]);
                //Session["PID"] = str3;
                //ListItems.Visible = false;
                //panelRed.Visible = true;
                //Classes.EcommAdminClass.getdropdown(ddlProduct, TID, LID.ToString(), "", "", "TBLPRODUCTWithICIT_BR");
                Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
                lnkBTNSAVE.Enabled = true;

                btndiscartitems.Text = "Cancel";
                btnAddItemsIn.Visible = false;

                if (ViewState["TempListICTR_DT"] != null)
                {
                    ListITtemDT.Visible = false;
                    penalAddDTITem.Visible = true;
                    lnkBTNSAVE.Text = "Update";
                    btnAddDT.Text = "Update";

                    DateTime TACtionDate = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == str1).TRANSDATE;

                    string period_code = Pidalcode(TACtionDate).ToString();
                    List<Database.ICTR_DT> TempListICTR_DT12 = ((List<Database.ICTR_DT>)ViewState["TempListICTR_DT"]).ToList();
                    Database.ICTR_DT objICTR_DT = TempListICTR_DT12.Single(p => p.MYTRANSID == str1 && p.MYID == str2 && p.TenentID == TID);
                    int PID = Convert.ToInt32(objICTR_DT.MyProdID);

                    TBLPRODUCT objTBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                    txtserchProduct.Text = objTBLPRODUCT.BarCode;
                    txtDescription.Text = objICTR_DT.DESCRIPTION.ToString();
                    if (objtblsetupsalesh.AllowMinusQty == false)
                    {
                        Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                    }
                    else
                    {
                        Classes.EcommAdminClass.BindAddvanserch(txtserchProduct, ddlProduct, ddlUOM, txtDescription, TID).ToString();
                    }
                    ddlProduct.SelectedValue = objICTR_DT.MyProdID.ToString();
                    if (ddlUOM.Items.Count > 0)
                        ddlUOM.SelectedValue = objICTR_DT.UOM.ToString();
                    txtQuantity.Text = objICTR_DT.QUANTITY.ToString();
                    txtUPriceLocal.Text = objICTR_DT.UNITPRICE.ToString();
                    txtDiscount.Text = objICTR_DT.DISAMT.ToString();
                    txtTotalCurrencyLocal.Text = objICTR_DT.AMOUNT.ToString();
                    ViewState["DTTRCTIONID"] = str1;
                    ViewState["DTMYID"] = str2;

                    Boolean Perishable = Convert.ToBoolean(objTBLPRODUCT.Perishable);  //Come From MultiTransaction
                    Boolean MultiUOM = Convert.ToBoolean(objTBLPRODUCT.MultiUOM);
                    Boolean MultiColor = Convert.ToBoolean(objTBLPRODUCT.MultiColor);
                    Boolean MultiSize = Convert.ToBoolean(objTBLPRODUCT.MultiSize);
                    Boolean MultiBinStore = Convert.ToBoolean(objTBLPRODUCT.MultiBinStore);
                    Boolean Serialized = Convert.ToBoolean(objTBLPRODUCT.Serialized);
                    //if (MultiUOM == Convert.ToBoolean(1))
                    //    lblmultiuom.Visible = true;
                    //else
                    //    lblmultiuom.Visible = false;
                    //if (Perishable == Convert.ToBoolean(1))
                    //    lblmultiperishable.Visible = true;
                    //else
                    //    lblmultiperishable.Visible = false;
                    //if (MultiColor == Convert.ToBoolean(1))
                    //    lblmulticolor.Visible = true;
                    //else
                    //    lblmulticolor.Visible = false;
                    //if (MultiSize == Convert.ToBoolean(1))
                    //    lblmultisize.Visible = true;
                    //else
                    //    lblmultisize.Visible = false;
                    //if (MultiBinStore == Convert.ToBoolean(1))
                    //    lblmultibinstore.Visible = true;
                    //else
                    //    lblmultibinstore.Visible = false;
                    //if (Serialized == Convert.ToBoolean(1))
                    //    lblmultiserialize.Visible = true;
                    //else
                    //    lblmultiserialize.Visible = false;
                    if (MultiSize == Convert.ToBoolean(1))
                    {
                        lblmultisize.Visible = true;
                        List<Database.ICIT_BR_SIZECOLOR> Listserl = DB.ICIT_BR_SIZECOLOR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == period_code && p.COLORID == 999999998 && p.OnHand != 0).ToList();
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
                        List<Database.ICIT_BR_SIZECOLOR> Listserl = DB.ICIT_BR_SIZECOLOR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == period_code && p.SIZECODE == 999999998 && p.OnHand != 0).ToList();
                        listmulticoler.DataSource = Listserl;
                        listmulticoler.DataBind();
                    }
                    else
                    {
                        lblmulticolor.Visible = false; //Come from MultiTransaction
                    }
                    //if (MultiUOM == Convert.ToBoolean(1))
                    //{
                    //    lblmultiuom.Visible = true;  //Come from MultiTransaction
                    //    List<Database.ICIT_BR> Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == period_code && p.OnHand != 0).ToList();
                    //    lidtUom.DataSource = Listserl;
                    //    lidtUom.DataBind();

                    //}
                    //else
                    //{
                    //    lblmultiuom.Visible = false;  //Come from MultiTransaction
                    //}
                    if (Serialized == Convert.ToBoolean(1))
                    {
                        lblmultiserialize.Visible = true;  //Come from MultiTransaction
                        if (txtQuantity.Text == "0")
                        { }
                        else
                        {
                            int TexQty = Convert.ToInt32(txtQuantity.Text);
                            int UOMSEL = Convert.ToInt32(ddlUOM.SelectedValue);
                            int QTY = Convert.ToInt32(DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).OnHand);
                            List<Database.ICIT_BR_Serialize> ListSerialize = new List<ICIT_BR_Serialize>();
                            List<Database.ICIT_BR_Serialize> Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == period_code && p.Active == "Y").OrderBy(p => p.Serial_Number).ToList();

                            int rqty = 0;
                            foreach (Database.ICIT_BR_Serialize items in Listserl)
                            {
                                ListSerialize.Add(items);
                            }
                            if (QTY <= TexQty)
                            {
                                int avelable = Listserl.Count();
                                rqty = TexQty - avelable;

                                for (int p = 0; p <= rqty - 1; p++)
                                {
                                    Database.ICIT_BR_Serialize objseri = new ICIT_BR_Serialize();
                                    ListSerialize.Add(objseri);
                                }
                            }


                            ViewState["ListofSerlNumber"] = ListSerialize;
                            listSerial.DataSource = ListSerialize;  //Come from MultiTransaction
                            listSerial.DataBind();

                            if (QTY <= TexQty)
                            {
                                for (int p = 1; p <= rqty; p++)
                                {
                                    TextBox txtlistSerial = (TextBox)listSerial.Items[p].FindControl("txtlistSerial");
                                    txtlistSerial.Enabled = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        lblmultiserialize.Visible = false;  //Come from MultiTransaction
                    }
                    if (MultiBinStore == Convert.ToBoolean(1))
                    {
                        lblmultibinstore.Visible = true;  //Come from MultiTransaction
                        List<Database.ICIT_BR_BIN> Listserl = DB.ICIT_BR_BIN.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == period_code && p.OnHand != 0).ToList();
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
                        List<Database.ICIT_BR_Perishable> Listserl = DB.ICIT_BR_Perishable.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == period_code && p.OnHand != 0).ToList();
                        listperishibal.DataSource = Listserl;  //Come from MultiTransaction
                        listperishibal.DataBind();
                        ViewState["ListICIT_BR_Perishable"] = Listserl;
                    }
                    else
                    {
                        lblmultiperishable.Visible = false; //Come from MultiTransaction
                    }
                }
                ViewState["AddnewItems"] = "Yes";
                //btnSubmit.Enabled = false;
                //btnPrint.Enabled = false;
                //BTNsAVEcONFsA.Enabled = false;
                //btnSaveData.Enabled = false;

                // btndiscartitems.Text = "Cancle";
                //ListItems.Visible = false;
                //panelRed.Visible = true;
                //btnAddItemsIn.Visible = false;
                //btnAddDT.Visible = true;
                //btnAddDT.Text = "Update";
                txtserchProduct.Enabled = false;
                btnserchAdvans.Enabled = true;
                ddlProduct.Enabled = false;
            }
        }
        public void clearItemsTab()
        {
            //ddlProduct.SelectedValue = ddlUOM.SelectedValue = "0";
            // txtDiscount.Text = "0";
            // txtQuantity.Text = "1";
            //txtTotalCurrencyLocal.Text = ""; //txtDescription.Text =
            //hidUPriceLocal.Value = hidTotalCurrencyLocal.Value = ""; //hidUOMId.Value = hidUOMText.Value = "";
        }
        public void clearHDPanel()
        {
            txtLocationSearch.Text = txtOrderDate.Text = txtBatchNo.Text = txtTraNoHD.Text = SubSerialNo.Text = txtNoteHD.Text = txtrefreshno.Text = txttotxl.Text = "";
            // btnSubmit.Visible = false;//yogesh
            btnConfirmOrder.Visible = false;// btnConfirmOrder.Visible = true;yogesh20042017
        }

        protected void listBin_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList dropBacthCode = (DropDownList)e.Item.FindControl("dropBacthCode");
            Classes.EcommAdminClass.getdropdown(dropBacthCode, TID, "", "", "", "TBLBIN");
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
                        string UOMNAME = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == UomID && p.TenentID == TID && p.CompniyAndContactID == MyProdID).RecValue;
                        string UOMQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (UOMTata == true)
                        {
                            MainDecrption = Discpition + "\n " + " Uom : " + UOMNAME + " : " + UOMQTY;
                            UOMTata = false;
                        }

                        else
                            MainDecrption = Discpition + ", " + UOMNAME + " : " + UOMQTY;
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
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";

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
                        int TCID = Convert.ToInt32(Session["GetMYTRANSID"]);
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
                        string ColerName = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == COLID && p.TenentID == TID && p.CompniyAndContactID == MyProdID).RecValue;
                        string ColerNameQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (MULTIColoer == true)
                        {
                            MainDecrption = Discpition + "\n " + " Colors : " + ColerName + " : " + ColerNameQTY;
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
                return;
            }
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";
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
                        int TCID = Convert.ToInt32(Session["GetMYTRANSID"]);
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
                        string SizeNAME = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == SIZECODE && p.TenentID == TID && p.CompniyAndContactID == MyProdID).RecValue;
                        string SizeQTY = NewQty.ToString();
                        string MainDecrption = "";
                        if (Multisize == true)
                        {
                            MainDecrption = Discpition + "\n " + " Size : " + SizeNAME + " : " + SizeQTY;
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
                return;
            }
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {

            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";
            tags_2.Text = "";
            ViewState["SaveList1"] = null;
            int chkcount = 0;
            for (int i = 0; i <= listSerial.Items.Count - 1; i++)
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
                    chkcount++;
                    //List<ICIT_BR_Serialize> Listofsirlnumber = ((List<ICIT_BR_Serialize>)ViewState["ListofSerlNumber"]);
                    //ICIT_BR_Serialize TempList12 = Listofsirlnumber.Single(p => p.TenentID == TID && p.LocationID == LID && p.period_code == PICODE && p.MyProdID == PID && p.UOM == UOM && p.Serial_Number == drpval);

                    //Listofsirlnumber.Remove(TempList12);
                    //listSerial.DataSource = Listofsirlnumber;
                    //listSerial.DataBind();
                    //ViewState["ListofSerlNumber"] = Listofsirlnumber;
                }
            }
            int QTY = Convert.ToInt32(txtQuantity.Text);
            if (QTY != chkcount)
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Serialization selected Qty " + chkcount + " from " + QTY;
                return;
            }


            DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = Pidalcode(TACtionDate).ToString();
            int Total = 0;
            string[] Seperate4 = tags_2.Text.Split(',');
            Total = Seperate4.Count();
            if (QTY == Total)
            {
                string Discpition = txtDescription.Text;
                string MainDecrption = tags_2.Text;
                MainDecrption = Discpition + "\n " + "Serialize : " + MainDecrption;
                txtDescription.Text = MainDecrption;

                string[] str = tags_2.Text.Split(',');
                int count5 = 0;
                string Sep5 = "";
                for (int i = 0; i <= str.Count() - 1; i++)
                {
                    int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                    int TCID = Convert.ToInt32(Session["GetMYTRANSID"]);// Convert.ToInt32(txtTraNoHD.Text);

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

                    string UOMS = UOM.ToString();

                    if (DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.UOM == UOMS && p.Serial_Number == Serialize && p.MinusAllow == false).Count() > 0)
                    {
                        bool MinusAllow = false;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID, MinusAllow);
                    }
                    else
                    {
                        bool MinusAllow = true;
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TenentID, MyProdID, period_code, MySysName, UOM, SIZECODE, COLORID, BinID, BatchNo, Serialize, MYTRANSID, LocationID, NewQty, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active, CRUP_ID, MinusAllow);
                    }



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

            int userID1 = ((USER_MST)Session["USER"]).USER_ID;
            int userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            int CUNTRYID = Convert.ToInt32(CURRENCY);//Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            int SID = Convert.ToInt32(HiddenField3.Value);
            int SIDUserDItl = Convert.ToInt32(DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COUNTRYID);
            //if (CUNTRYID == SIDUserDItl)
            //    txtUPriceLocal.Enabled = true;
            //else
            //    txtUPriceLocal.Enabled = false;
        }
        protected void txtLocationSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtLocationSearch.Text.ToLower() == "cash" && rbtPsle.SelectedValue == "1")
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Invalid Customer');", true);
                return;
            }
            if (HiddenField3.Value != "")
            {
                int userID1 = ((USER_MST)Session["USER"]).USER_ID;
                int userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
                int CUNTRYID = Convert.ToInt32(CURRENCY);//Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
                int SID = Convert.ToInt32(HiddenField3.Value);
                int SIDUserDItl = Convert.ToInt32(DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == SID && p.TenentID == TID).COUNTRYID);

                //if (CUNTRYID == SIDUserDItl)
                //    txtUPriceLocal.Enabled = true;
                //else
                //    txtUPriceLocal.Enabled = false;

                BTnEdit.Visible = true;
            }
        }
        //public string getprodname(int SID)
        //{
        //    string ProductCode = DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).UserProdID;
        //    return ProductCode;
        //}
        public string getsuppername(int ID)
        {
            return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == ID && p.TenentID == TID).COMPNAME1;
        }
        public string getrecodtypename(int ID, int PID)
        {
            return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == ID && p.TenentID == TID && p.CompniyAndContactID == PID).RecValue;
        }
        public string getbinname(int BID)
        {
            return DB.TBLBINs.Single(p => p.BIN_ID == BID && p.TenentID == TID).BINDesc1;
        }

        protected void btnAddnew_Click(object sender, EventArgs e)
        {

            //BindProduct();yogesh

            //ddlUOM.SelectedIndex = ddlProduct.SelectedIndex = 0;
            txtQuantity.Text = txtDescription.Text = "0";
            txtUPriceLocal.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = "0";
            btnAddItemsIn.Visible = false;
            btnAddDT.Visible = true;
            ListITtemDT.Visible = false;
            penalAddDTITem.Visible = true;
            ViewState["AddnewItems"] = "Yes";
            btndiscartitems.Text = "Cancel";
            btnAddDT.Text = "Save";
            txtQuantity.Text = "";
            txtserchProduct.Text = "";
            lblAvailableQty.Text = "";

            //ddlProduct.DataSource = null;
            //ddlProduct.DataBind();
            ddlProduct.Items.Clear();
            ddlUOM.Items.Clear();
            ddlUOM.Enabled = false;

            btnSubmit.Enabled = false;
            btnPrint.Enabled = false;
            BTNsAVEcONFsA.Enabled = false;
            btnSaveData.Enabled = false;


            lblmultiuom.Visible = false; //Come from MultiTransaction
            lblmulticolor.Visible = false;
            lblmultisize.Visible = false;
            lblmultiperishable.Visible = false;
            lblmultibinstore.Visible = false;
            lblmultiserialize.Visible = false;
            cleardt();
            //GetProductDescription();yogesh

            //string script = "window.onload = function() { showhidepanelItems('ITEMS'); };";
            // ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelItems", script, true);
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            // ClickFlag = false;
            Session["GetMYTRANSID"] = null;
            Session["Invoice"] = null;
            //BindProduct();
            //GetProductDescription();yogesh
            //Classes.EcommAdminClass.getdropdown(ddlProduct, TID, LID.ToString(), "", "", "TBLPRODUCTWithICIT_BR");
            //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");

            int MYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
            //InvoiceItem.GetMYTRANSID(MYTRANSID);
            //Session["GetMYTRANSID"] = MYTRANSID;
            //int MYTRANSID = DB.ICTR_MYTRANSID.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_MYTRANSID.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
            Session["GetMYTRANSID"] = MYTRANSID;

            WrietMode();
            clearItemsTab();
            clearHDPanel();
            txtTraNoHD.Text = "0";
            //SubSerialNo.Text = Classes.EcommAdminClass.TransDocNo(TID, Transid, Transsubid);
            ViewState["TempListICTR_DT"] = null;
            txtOrderDate.Text = DateTime.Now.ToString("dd-MMM-yy");
            //Repeater2.DataSource = null;
            //Repeater2.DataBind();

            btnAddItemsIn.Visible = false;
            //btnAddDT.Visible = true;
            ListITtemDT.Visible = false;
            penalAddDTITem.Visible = true;
            btndiscartitems.Text = "Cancel";
            CheckRole();
            int COMPID = Convert.ToInt32(objtblsetupsalesh.DeftCoustomer);
            //drpterms.SelectedValue = "2114";
            txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == COMPID).COMPNAME1;
            HiddenField3.Value = COMPID.ToString();
            Session["MultiTransactionProdObj"] = null;

            txtBatchNo.Text = MYTRANSID.ToString();
            lnkUomConversion.Visible = false;
        }

        protected void txtOHAmntLocal_TextChanged(object sender, EventArgs e)
        {

        }




        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";
            int QTY = Convert.ToInt32(txtQuantity.Text);
            int TCID = Convert.ToInt32(Session["GetMYTRANSID"]);
            int PID = Convert.ToInt32(ddlProduct.SelectedValue);
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
                            MainDecrption = Discpition + "\n " + " Perishable : " + BatchNo + " : " + NewQty;
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
                return;
            }
        }
        protected void lbApproveIss_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";
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
                    TextBox txtqty = (TextBox)listBin.Items[i].FindControl("txtqty");
                    Label lblbinid = (Label)listBin.Items[i].FindControl("lblbinid");
                    if (txtqty.Text != "" && txtqty.Text != "0")
                    {
                        int TCID = Convert.ToInt32(Session["GetMYTRANSID"]);
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
                            MainDecrption = Discpition + "\n " + " Bin : " + UOMNAME + " : " + NewQty;
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

            lblMsg123.Text = "";
            pnlSuccessMsg123.Visible = false;
            Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");

            lnkUomConversion.Visible = false;
            DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = Pidalcode(TACtionDate).ToString();

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

                    lnkBTNSAVE.Enabled = true;

                    Database.TBLPRODUCT objprod = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID);

                    Boolean MultiUOM = Convert.ToBoolean(objprod.MultiUOM);
                    if (MultiUOM == Convert.ToBoolean(1))
                    {
                        ddlUOM.Enabled = true;
                        ViewState["PID"] = PID;

                        //if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID).Count() > 0)
                        //{
                        //    lnkUomConversion.Visible = true;
                        //}

                        //lnkUomConversion.Attributes.Add("target", "_blank");
                        //lnkUomConversion.PostBackUrl = "../ECOMM/UOM_Qty_Convertion.aspx?PID=" + PID;

                        List<Database.ICUOM> ListUOm1 = new List<ICUOM>();
                        List<Database.View_ICBR_Product> listmultiuom1 = DB.View_ICBR_Product.Where(p => p.TenentID == TID && p.MyProdID == PID && p.LocationID == LID && p.OnHand > 0).ToList();

                        foreach (Database.View_ICBR_Product Itemuo in listmultiuom1)
                        {
                            Database.ICUOM objuom = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == Itemuo.UOM);
                            ListUOm1.Add(objuom);
                        }

                        ddlUOM.DataSource = ListUOm1.Where(p => p.TenentID == TID);
                        ddlUOM.DataTextField = "UOMNAME1";
                        ddlUOM.DataValueField = "UOM";
                        ddlUOM.DataBind();
                        ddlUOM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Unit of Measure--", "0"));
                        if (ddlUOM.Items.Count > 0)
                            ddlUOM.SelectedValue = ListUOm1.First().UOM.ToString();
                        int UOMSEL = Convert.ToInt32(ddlUOM.SelectedValue);
                        if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).Count() > 0)
                        {
                            lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).OnHand.ToString();
                        }
                    }
                    else
                    {
                        Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "ICUOM");
                        if (ddlUOM.Items.Count > 0)
                            ddlUOM.SelectedValue = uom.ToString();
                        ddlUOM.Enabled = false;

                        if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).Count() > 0)
                        {
                            lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).OnHand.ToString();
                        }
                        else
                        {
                            lblAvailableQty.Text = "Available Qty : 0";
                        }
                    }

                    txtQuantity.Text = "1";
                    Boolean MultiBinStore = Convert.ToBoolean(objprod.MultiBinStore);
                    Boolean Perishable = Convert.ToBoolean(objprod.Perishable);
                    Boolean Serialized = Convert.ToBoolean(objprod.Serialized);
                    Boolean MultiColor = Convert.ToBoolean(objprod.MultiColor);
                    Boolean MultiSize = Convert.ToBoolean(objprod.MultiSize);
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

                    if (Serialized == Convert.ToBoolean(1))
                    {
                        lblmultiserialize.Visible = true;  //Come from MultiTransaction
                        if (txtQuantity.Text == "0")
                        { }
                        else
                        {
                            int TexQty = Convert.ToInt32(txtQuantity.Text);
                            int UOMSEL = Convert.ToInt32(ddlUOM.SelectedValue);
                            int QTY = Convert.ToInt32(DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).OnHand);
                            List<Database.ICIT_BR_Serialize> ListSerialize = new List<ICIT_BR_Serialize>();
                            List<Database.ICIT_BR_Serialize> Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.Active == "Y").OrderBy(p => p.Serial_Number).ToList();

                            int rqty = 0;
                            foreach (Database.ICIT_BR_Serialize items in Listserl)
                            {
                                ListSerialize.Add(items);
                            }
                            if (QTY <= TexQty)
                            {
                                int avelable = Listserl.Count();
                                rqty = TexQty - avelable;
                                for (int p = 0; p <= rqty - 1; p++)
                                {
                                    Database.ICIT_BR_Serialize objseri = new ICIT_BR_Serialize();
                                    ListSerialize.Add(objseri);
                                }
                            }



                            ViewState["ListofSerlNumber"] = ListSerialize;
                            listSerial.DataSource = ListSerialize;  //Come from MultiTransaction
                            listSerial.DataBind();

                            if (QTY <= TexQty)
                            {
                                for (int p = 1; p <= rqty; p++)
                                {
                                    TextBox txtlistSerial = (TextBox)listSerial.Items[p].FindControl("txtlistSerial");
                                    txtlistSerial.Enabled = true;
                                }
                            }
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
                    //if (MultiUOM == Convert.ToBoolean(1))
                    //{
                    //    lblmultiuom.Visible = true;  //Come from MultiTransaction
                    //    var Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    //    lidtUom.DataSource = Listserl;
                    //    lidtUom.DataBind();

                    //}
                    //else
                    //{
                    //    lblmultiuom.Visible = false;  //Come from MultiTransaction
                    //}

                }
                else
                {

                    lnkBTNSAVE.Enabled = false;
                    if (ddlUOM.Items.Count > 0)
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

                    lnkBTNSAVE.Enabled = true;

                    Database.TBLPRODUCT objprod = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.LOCATION_ID == LID);

                    int uo = Convert.ToInt32(objprod.UOM);

                    Boolean MultiUOM = Convert.ToBoolean(objprod.MultiUOM);
                    if (MultiUOM == Convert.ToBoolean(1))
                    {


                        ddlUOM.Enabled = true;
                        ViewState["PID"] = PID;

                        //if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID).Count() > 0)
                        //{
                        //    lnkUomConversion.Visible = true;
                        //}
                        //lnkUomConversion.Attributes.Add("target", "_blank");
                        //lnkUomConversion.PostBackUrl = "ECOMM/UOM_Qty_Convertion.aspx?PID=" + PID;

                        List<Database.ICUOM> ListUOm1 = new List<ICUOM>();

                        Database.ICUOM objUo = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == uo).FirstOrDefault();
                        ListUOm1.Add(objUo);

                        List<Database.Tbl_Multi_Color_Size_Mst> listmultiuom1 = DB.Tbl_Multi_Color_Size_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == PID && p.RecordType == "MultiUOM").ToList();

                        foreach (Database.Tbl_Multi_Color_Size_Mst Itemuo in listmultiuom1)
                        {
                            Database.ICUOM objuom = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == Itemuo.RecTypeID);
                            ListUOm1.Add(objuom);
                        }
                        ddlUOM.DataSource = ListUOm1.Where(p => p.TenentID == TID);
                        ddlUOM.DataTextField = "UOMNAME1";
                        ddlUOM.DataValueField = "UOM";
                        ddlUOM.DataBind();
                        ddlUOM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Unit of Measure--", "0"));
                    }
                    txtQuantity.Text = "1";
                    Boolean MultiBinStore = Convert.ToBoolean(objprod.MultiBinStore);
                    Boolean Perishable = Convert.ToBoolean(objprod.Perishable);
                    Boolean Serialized = Convert.ToBoolean(objprod.Serialized);
                    Boolean MultiColor = Convert.ToBoolean(objprod.MultiColor);
                    Boolean MultiSize = Convert.ToBoolean(objprod.MultiSize);
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

                    if (Serialized == Convert.ToBoolean(1))
                    {
                        lblmultiserialize.Visible = true;  //Come from MultiTransaction
                        if (txtQuantity.Text == "0")
                        { }
                        else
                        {
                            int TexQty = Convert.ToInt32(txtQuantity.Text);
                            int UOMSEL = Convert.ToInt32(ddlUOM.SelectedValue);
                            int QTY = Convert.ToInt32(DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == UOMSEL).OnHand);
                            List<Database.ICIT_BR_Serialize> ListSerialize = new List<ICIT_BR_Serialize>();
                            List<Database.ICIT_BR_Serialize> Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.Active == "Y").OrderBy(p => p.Serial_Number).ToList();

                            int rqty = 0;
                            foreach (Database.ICIT_BR_Serialize items in Listserl)
                            {
                                ListSerialize.Add(items);
                            }

                            if (QTY <= TexQty)
                            {
                                int avelable = Listserl.Count();
                                rqty = TexQty - avelable;

                                for (int p = 0; p <= rqty - 1; p++)
                                {
                                    Database.ICIT_BR_Serialize objseri = new ICIT_BR_Serialize();
                                    ListSerialize.Add(objseri);
                                }
                            }


                            ViewState["ListofSerlNumber"] = ListSerialize;
                            listSerial.DataSource = ListSerialize;  //Come from MultiTransaction
                            listSerial.DataBind();

                            if (QTY <= TexQty)
                            {
                                for (int p = 1; p <= rqty; p++)
                                {
                                    TextBox txtlistSerial = (TextBox)listSerial.Items[p].FindControl("txtlistSerial");
                                    txtlistSerial.Enabled = true;
                                }
                            }
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
                    //if (MultiUOM == Convert.ToBoolean(1))
                    //{
                    //    lblmultiuom.Visible = true;  //Come from MultiTransaction
                    //    var Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                    //    lidtUom.DataSource = Listserl;
                    //    lidtUom.DataBind();

                    //}
                    //else
                    //{
                    //    lblmultiuom.Visible = false;  //Come from MultiTransaction
                    //}
                }
                else
                {

                    lnkBTNSAVE.Enabled = false;
                    if (ddlUOM.Items.Count > 0)
                        ddlUOM.SelectedValue = "99999";
                    txtDescription.Text = lblProductCount.Text = " Product found 0 as a Query";
                    txtQuantity.Text = txtDiscount.Text = txtTotalCurrencyLocal.Text = txtUPriceLocal.Text = "0";
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Product not found in search";
                    return;
                }


            }




            int PIDPRICE = Convert.ToInt32(ddlProduct.SelectedValue);
            int UOMPRICE = Convert.ToInt32(ddlUOM.SelectedValue);
            List<Database.ICITEMS_PRICE> ListPrice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == PIDPRICE && p.UOM == UOMPRICE).ToList();

            if (ListPrice.Count() > 0)
            {
                txtUPriceLocal.Text = ListPrice.Single(p => p.TenentID == TID && p.MYPRODID == PIDPRICE && p.UOM == UOMPRICE).onlinesale3.ToString();
            }
            else
            {
                txtUPriceLocal.Text = "0";
            }
            TotalPriceui();
            //lblmultiuom.Visible = false;  //Come from MultiTransaction
            //lblmulticolor.Visible = false;
            //lblmultisize.Visible = false;
            //lblmultiperishable.Visible = false;
            //lblmultibinstore.Visible = false;
            //lblmultiserialize.Visible = false;

            ////}
        }


        protected void btnaddrefresh_Click(object sender, EventArgs e)
        {
            int MYID = 0;
            Database.ICIT_BR_ReferenceNo objICIT_BR_ReferenceNo = new ICIT_BR_ReferenceNo();
            int TCNID = Convert.ToInt32(txtTraNoHD.Text);
            objICIT_BR_ReferenceNo.TenentID = TID;
            objICIT_BR_ReferenceNo.RefID = Convert.ToInt32(drpRefnstype.SelectedValue);
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
            int userID1 = ((USER_MST)Session["USER"]).USER_ID;
            int userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            int CUNTRYID = Convert.ToInt32(CURRENCY);//Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            // txtUPriceLocal.Enabled = true;
        }
        protected void btnaddamunt_Click(object sender, EventArgs e)
        {
            Database.ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            TempICTRPayTerms_HD = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Database.ICTRPayTerms_HD objEco_ICEXTRACOST = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICEXTRACOST);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            Repeater3.DataSource = TempICTRPayTerms_HD;
            Repeater3.DataBind();


        }


        protected void btnserchcutmer_Click(object sender, EventArgs e)
        {
            //Classes.EcommAdminClass.BindSerchcutomer(txtserchCustmer, HiddenField3);
        }

        protected void rbtPsle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userID1 = ((USER_MST)Session["USER"]).USER_ID;
            int userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            int CUNTRYID = Convert.ToInt32(CURRENCY);//Convert.ToInt32(DB.USER_DTL.Single(p => p.USER_DETAIL_ID == userTypeid && p.TenentID == TID).COUNTRY);
            Drpdown.Visible = true;
            if (rbtPsle.SelectedValue == "2")//Credit=1
            {

                //drpterms.SelectedValue = "2114";
                int COMPID = Convert.ToInt32(objtblsetupsalesh.DeftCoustomer);
                txtLocationSearch.Text = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == COMPID).COMPNAME1; ;
            }
            else//Cash=2
            {
                //drpterms.SelectedValue = "0";
                txtLocationSearch.Text = "";
                //txtUPriceLocal.Enabled = true;
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
                    listSerial.DataSource = Listofsirlnumber.OrderBy(p => p.Serial_Number);
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
            { }
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
            ViewState["SaveList"] = List;
            grdPO.DataSource = List.Where(p => p.TenentID == TID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO")).OrderByDescending(p => p.MYTRANSID);
            grdPO.DataBind();
            //grdPO.DataSource=DB.viewTransactions.Where()
        }
        protected void btnallserch_Click(object sender, EventArgs e)
        {
            string serch = txtallserchforview.Text;
            if (cbkseriz.Checked == true)
            {
                var List = DB.viewTransactions.Where(p => (p.DESCRIPTION.ToUpper().Contains(serch.ToUpper()) && p.TenentID == TID && p.locationID == LID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO"))).OrderBy(p => p.MYPRODID).ToList();
                grdPO.DataSource = List.OrderByDescending(p => p.MYPRODID);
                grdPO.DataBind();
            }
            else
            {
                var List = DB.viewTransactions.Where(p => (p.UserProdID.ToUpper().Contains(serch.ToUpper()) || p.BarCode.ToUpper().Contains(serch.ToUpper()) || p.AlternateCode1.ToUpper().Contains(serch.ToUpper()) || p.AlternateCode2.ToUpper().Contains(serch.ToUpper()) || p.ShortName.ToUpper().Contains(serch.ToUpper()) || p.ProdName2.ToUpper().Contains(serch.ToUpper()) || p.ProdName3.ToUpper().Contains(serch.ToUpper()) || p.keywords.ToUpper().Contains(serch.ToUpper()) || p.REMARKS.ToUpper().Contains(serch.ToUpper()) || p.DescToprint.ToUpper().Contains(serch.ToUpper()) || p.ProdName1.ToUpper().Contains(serch.ToUpper())) && p.TenentID == TID).OrderBy(p => p.MYPRODID).ToList();
                grdPO.DataSource = List.Where(p => p.TenentID == TID && p.locationID == LID && p.ACTIVE == true && (p.Status == "SO" || p.Status == "DSO")).OrderByDescending(p => p.TRANSDATE).OrderByDescending(p => p.MYPRODID);
                grdPO.DataBind();
            }
        }
        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            //ICTR_DTEXT OBjICTR_DTEXT = new ICTR_DTEXT();
            //OBjICTR_DTEXT.TenentID = TID;
            //DB.ICTR_DTEXT.AddObject(OBjICTR_DTEXT);
            //DB.SaveChanges();
        }
        public void CalcCashDilevery(int TID)
        {
            var objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.AccountantID == null && p.CheckOutDate == null).ToList();
            decimal cashinhand = Convert.ToDecimal(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 1).Sum(a => a.Amount));
            decimal Chequeinhand = Convert.ToDecimal(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 2).Sum(a => a.Amount)); ;
            decimal BankPaySlip = Convert.ToDecimal(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 3).Sum(a => a.Amount));
            decimal GiftVoucher = Convert.ToDecimal(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 4).Sum(a => a.Amount));
            int cashinhandc = Convert.ToInt32(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 1).Count());
            int Chequeinhandc = Convert.ToInt32(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 2).Count());
            int BankPaySlipc = Convert.ToInt32(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 3).Count());
            int GiftVoucherc = Convert.ToInt32(objICTRPayTerms_HD.Where(p => p.CashBankChequeID == 4).Count());
            txtcashinhand.Text = cashinhand.ToString();
            txtChequeinhand.Text = Chequeinhand.ToString();
            txtBankPaySlip.Text = BankPaySlip.ToString();
            txtGiftVoucher.Text = GiftVoucher.ToString();
            txtcashinhandc.Text = cashinhandc.ToString();
            txtChequeinhandc.Text = Chequeinhandc.ToString();
            txtBankPaySlipc.Text = BankPaySlipc.ToString();
            txtGiftVoucherc.Text = GiftVoucherc.ToString();
            txtRefNo.Text = DateTime.Now.ToString("MMddyyyyhhmmss");
            txtcashinhandd.Text = DateTime.Now.ToShortDateString();
        }


        protected void BTnEdit_Click(object sender, EventArgs e)
        {
            string search = txtLocationSearch.Text;
            string[] id = search.Split('-');
            string COMPNAME1 = id[0].Trim().ToString();
            List<Database.TBLCOMPANYSETUP> ListTBLCOMPANYSETUP = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1 == COMPNAME1 && p.TenentID == TID).ToList();
            if (ListTBLCOMPANYSETUP.Count() > 0)
            {
                int CompID = ListTBLCOMPANYSETUP.Single(p => p.COMPNAME1 == COMPNAME1 && p.TenentID == TID).COMPID;

                int ComID = Convert.ToInt32(CompID);

                string url = "/CRM/CompanyMaster.aspx?COMPID=" + CompID + "&Mode=Write";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            }

            //    string url = "/CRM/CompanyMaster.aspx?COMPID=" + CID + "&Mode=Write";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
        }
        int TransNo = 0;

        protected void drpReceipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReceipe.SelectedValue != "0")
            {
                int recNo = Convert.ToInt32(drpReceipe.SelectedValue);

                List<Database.Receipe_Menegement> ListRecipe = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == recNo && p.IOSwitch== "Output").ToList();

                if (ListRecipe.Count() > 0)
                {

                    decimal? CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    DateTime trnsDate = txtOrderDate.Text != null ? Convert.ToDateTime(txtOrderDate.Text) : DateTime.Now;

                    if (Session["GetMYTRANSID"] != null)
                        MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);

                    if (MYTRANSID == 0)
                    {
                        MYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                    }

                    int TOLOCATIONID = LID != 0 ? LID : 1;
                    int Comp = Convert.ToInt32(CUSTVENDID);
                    string Custmerid = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == Comp).Count() > 0 ? DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == Comp).FirstOrDefault().COMPNAME1 : "Cash";
                    string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
                    string REFERENCE = txtrefreshno.Text != null ? txtrefreshno.Text : "";
                    string NOTES = txtNoteHD.Text != null ? txtNoteHD.Text : "";

                    decimal? TOTQTY1 = 0;
                    decimal? TOTAMT = 0;

                    string USERBATCHNO = MYTRANSID.ToString();

                    DateTime? TRANSDATE = trnsDate;
                    int ToTenentID = 99999;
                    string MainTranType = "O";
                    string TranType = "POS Invoice";                    
                    int transid = Transid;
                    int transsubid = Transsubid;
                    string MYSYSNAME = "SAL";
                    decimal? COMPID = 826667;
                    string LF = "L";
                    string PERIOD_CODE = Pidalcode(trnsDate).ToString();
                    string ACTIVITYCODE = "0";
                    decimal? MYDOCNO = 2;
                    decimal? AmtPaid = -1;
                    string PROJECTNO = "99999";
                    string CounterID = "0";
                    string PrintedCounterInvoiceNo = "5";
                    int JOID = 1;
                    int CRUP_ID = 0;
                    string GLPOST = "2";
                    string GLPOST1 = "2";
                    string GLPOSTREF1 = "2";
                    string GLPOSTREF = "2";
                    string ICPOST = "2";
                    string ICPOSTREF = "2";
                    bool ACTIVE = true;
                    int COMPANYID = 126;
                    DateTime? ENTRYDATE = DateTime.Now;
                    DateTime? ENTRYTIME = DateTime.Now;
                    DateTime? UPDTTIME = DateTime.Now;
                    string InvoiceNO = GetTransDocNo(TID, MYTRANSID, transid, transsubid);
                    decimal? Discount = 0;
                    string Status = "DSO";
                    int Terms = 2114;
                    string Swit1 = "2";
                    string ExtraField2 = drpselsmen.SelectedValue;
                    int RefTransID = 0;
                    string Reason = null;
                    string TransDocNo = InvoiceNO;
                    int LinkTransID = 0;
                    int invoice_Type = 0;
                    int invoice_Source = 0;

                    Classes.EcommAdminClass.insert_ICTR_HD(TID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, transid, transsubid, MYSYSNAME, COMPID, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, Custmerid, Swit1, ExtraField2, RefTransID, Reason, TransDocNo, LinkTransID, invoice_Type, invoice_Source);

                    updateTransdocno(TID, transid, transsubid, InvoiceNO);

                    foreach (Database.Receipe_Menegement item in ListRecipe)
                    {
                        int MyProdID = Convert.ToInt32(item.ItemCode);
                        int UOM = Convert.ToInt32(item.UOM);

                        int QUANTITY = Convert.ToInt32(item.Qty);
                        decimal UNITPRICE = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MyProdID && p.UOM == UOM && p.onlinesale1 != null).Count() > 0 ? Convert.ToDecimal(DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MyProdID && p.UOM == UOM && p.onlinesale1 != null).FirstOrDefault().onlinesale1) : 0;

                        string DESCRIPTION = drpReceipe.SelectedItem.ToString();
                        decimal DISPER = 0;
                        decimal DISAMT = 0;

                        decimal AMOUNT = QUANTITY * UNITPRICE;
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = 0;
                        int COMPANYID1 = Convert.ToInt32(COMPID);
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.000);
                        string BATCHNO = "1";
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        DateTime EXPIRYDATE = DateTime.Now;
                        string SWITCH1 = "0";
                        int DelFlag = 0;
                        string ITEMID = "1";
                        decimal TAXAMT = Convert.ToDecimal(0.0);
                        decimal TAXPER = Convert.ToDecimal(0.0);
                        decimal PROMOTIONAMT = Convert.ToDecimal(0.00);

                        int MYID = 0;

                        string UOMs = UOM.ToString();

                        List<ICTR_DT> Listmydt = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == MyProdID && p.UOM == UOMs).ToList();

                        if (Listmydt.Count() > 0)
                        {
                            MYID = Listmydt.FirstOrDefault().MYID;
                        }

                        Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTRANSID, TOLOCATIONID, MYID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM.ToString(), QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID, Status);

                    }

                    SubSerialNo.Text = "";
                    txtTraNoHD.Text = "";

                    EditSalesh(MYTRANSID);
                    btnAddItemsIn.Visible = true;
                    penalAddDTITem.Visible = false;
                    ListITtemDT.Visible = true;
                }

            }
        }

        public string GetTransDocNo(int TID, int MYTRANSID, int Transid, int Transsubid)
        {
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

            return TransDocNo;
        }

        public void updateTransdocno(int TID, int Transid, int Transsubid, string InvoiceNo)
        {
            var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
            string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
            if (SirialNO == InvoiceNo)
            {
                Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                objtblsubtype.serialno = SirialNO;
                DB.SaveChanges();
            }
        }

        public void FinalConfirm()
        {
            int MTID = Convert.ToInt32(Session["GetMYTRANSID"]);
            List<Database.ICTR_DT> listTempDt = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MTID).ToList();
            //List<Database.ICTRPayTerms_HD> listicpayHD = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MTID).ToList();
            decimal Amountpayment = 0;
            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");

                Amountpayment += Convert.ToDecimal(txtammunt.Text);

            }

            decimal Amountdt = Convert.ToDecimal(listTempDt.Sum(p => p.AMOUNT));

            if (Amountpayment == Amountdt)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Check Your Pamment Total is not match');", true);
                return;
            }




            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                int COMPID = 826667;
                string Status = "";

                string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
                DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
                string OICODID = Pidalcode(TACtionDate).ToString();
                if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID && p.TenentID == TID).Count() == 1)
                    COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID && p.TenentID == TID).COMPID;
                if (Session["Invoice"] != null)
                {

                    TransNo = Convert.ToInt32(Session["Invoice"]);
                    var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                    //var List5 = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
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
                    int COMPANYID = Convert.ToInt32(CURRENCY);
                    if (DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).Count() == 0)
                    {
                        panelMsg.Visible = true;
                        lblErreorMsg.Text = "Kindly please select minimum one Items; to save Invoice";
                        return;
                    }

                    //foreach (Database.ICTR_DT item in List5)
                    //{
                    //    DB.ICTR_DT.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}
                    //var List6 = Classes.EcommAdminClass.getListICTR_DTEXT(TID, TransNo).ToList();
                    //var List6 = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).ToList();
                    //foreach (Database.ICTR_DTEXT item in List6)
                    //{
                    //    DB.ICTR_DTEXT.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}

                    //List<ICTR_DT> ListDate = ((List<Database.ICTR_DT>)Session["TempListICTR_DT"]).ToList();
                    List<ICTR_DT> ListDate = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    //int DXMYID = DB.ICTR_DTEXT.Count() > 0 ? Convert.ToInt32(DB.ICTR_DTEXT.Max(p => p.MyID) + 1) : 1;
                    int MyRunningSerial = 0;
                    //var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID).ToList();
                    //int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                    //List<Database.ICTR_DTEXT> ListICTR_DTEXT = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).ToList();
                    //if (ListICTR_DTEXT.Count() > 0)
                    //{
                    //    MyRunningSerial = DB.ICTR_DTEXT.Where(p => p.MyID == MYIDDT).Count() > 0 ? Convert.ToInt32(List6.Where(p => p.MyID == MYIDDT).Max(p => p.MyRunningSerial) + 1) : 1;
                    //}
                    //else
                    //{
                    //    MyRunningSerial = 1;
                    //}

                    //foreach (Database.ICTR_DT items in ListDate)
                    //{

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
                        Label lblMYID = (Label)Repeater2.Items[i].FindControl("lblMYID");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);//lblDISPER.Text
                        decimal DISAMT = Convert.ToDecimal(lblDISPER.Text);//lblDISPER.Text
                        var listICTR_DT = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
                        //int MYIDDT = listICTR_DT.Count() > 0 ? Convert.ToInt32(listICTR_DT.Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        int MyProdID = Convert.ToInt32(lblProductNameItem.Text);//lblProductNameItem.Text
                        int MYID = Convert.ToInt32(lblMYID.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = 0;
                        string DESCRIPTION = lblDiscription.Text;
                        string UOM = lblUOM.Text;
                        int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);//
                        decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);//lblTotalCurrency.Text
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

                        //start Insert in ICTR_DT

                        //Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                        //objICTR_DT.TenentID = TenentID;
                        //objICTR_DT.MYTRANSID = MYTRANSID;
                        //objICTR_DT.locationID = locationID;

                        //objICTR_DT.MYID = MYIDDT;
                        //objICTR_DT.MyProdID = MyProdID;
                        //objICTR_DT.REFTYPE = REFTYPE;
                        //objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                        //objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                        //objICTR_DT.MYSYSNAME = MYSYSNAME;
                        //objICTR_DT.JOID = JOID;
                        //objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                        //objICTR_DT.ACTIVITYID = ACTIVITYID;
                        //objICTR_DT.DESCRIPTION = DESCRIPTION;
                        //objICTR_DT.UOM = UOM;
                        //objICTR_DT.QUANTITY = QUANTITY;
                        //objICTR_DT.UNITPRICE = UNITPRICE;
                        //objICTR_DT.AMOUNT = AMOUNT;
                        //objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                        //objICTR_DT.BATCHNO = BATCHNO;
                        //objICTR_DT.BIN_ID = BIN_ID;
                        //objICTR_DT.BIN_TYPE = BIN_TYPE;
                        //objICTR_DT.GRNREF = GRNREF;
                        //objICTR_DT.DISPER = DISPER;
                        //objICTR_DT.DISAMT = DISAMT;
                        //objICTR_DT.TAXAMT = TAXAMT;
                        //objICTR_DT.TAXPER = TAXPER;
                        //objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                        //objICTR_DT.GLPOST = GLPOST;
                        //objICTR_DT.GLPOST1 = GLPOST1;
                        //objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                        //objICTR_DT.GLPOSTREF = GLPOSTREF;
                        //objICTR_DT.ICPOST = ICPOST;
                        //objICTR_DT.ICPOSTREF = ICPOSTREF;
                        //objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DT.COMPANYID = COMPANYID1;
                        //objICTR_DT.SWITCH1 = SWITCH1;
                        //objICTR_DT.ACTIVE = ACTIVE;
                        //objICTR_DT.DelFlag = DelFlag;
                        //objICTR_DT.CostAmount = CostAmount;

                        //DB.ICTR_DT.AddObject(objICTR_DT);
                        //DB.SaveChanges();
                        //ViewState["postDT"] = objICTR_DT;
                        //insert complate
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

                        //int UOM1 = Convert.ToInt32(objDT.UOM);

                        DateTime trnsDate = Convert.ToDateTime(Objhd.TRANSDATE);
                        string MySysName = Objhd.MYSYSNAME.ToString();


                        bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, Transid, Transsubid, MYTRANSID, MYID, QUANTITY, Reference, trnsDate, MySysName, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
                        if (flag1 == false)
                        {
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "One of the Posting Parameter is Blank/Null/Zero!", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                            return;

                        }
                        //int PQTY = Convert.ToInt32(obj.QTYINHAND);
                        //int Total123 = PQTY - QUANTITY;
                        //obj.QTYINHAND = Total123;
                        //DB.SaveChanges();

                        //var listICIT_BR_TMP = Classes.EcommAdminClass.getListICIT_BR_TMP(TID, MYTRANSID, MyProdID).ToList();
                        //if (MultiBinStore == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "MultiBIN").ToList();
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
                        //        string pagename = "Sales";
                        //        string Fuctions = "ADD";
                        //        Classes.EcommAdminClass.insertICIT_BR_BIN(TenentID, MyProdID, period_code, MySysName, Bin_ID, UOM1, LID, BatchNo, MYTRANSID, NEWQTY, Reference, Active, CRUP_ID, pagename, Fuctions);
                        //    }
                        //}
                        //if (Serialized == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "Serialize").ToList();
                        //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                        //    {
                        //        string period_code = OICODID;
                        //        string MySysName = "SAL";
                        //        string Serial_Number = listmulticolor[ii].Serial_Number;
                        //        int MyTransID = Convert.ToInt32(listmulticolor[ii].MYTRANSID);
                        //        string Active = "Y";
                        //        string Flag_GRN_GTN = "N";
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_Serialize(TenentID, MyProdID, period_code, MySysName, UOM, Serial_Number, LID, MyTransID, Flag_GRN_GTN, Active, CRUP_ID, pagename);
                        //    }
                        //}
                        //if (MultiUOM != Convert.ToBoolean(1))
                        //{
                        //    int UOM1 = Convert.ToInt32(items.UOM);//lblUOM.Text
                        //    var listICIT_BR = DB.ICIT_BR.Where(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.MySysName == MYSYSNAME && p.UOM == UOM1).ToList();
                        //    string period_code = OICODID;
                        //    string MySysName = "IC";
                        //    decimal UnitCost = UNITPRICE;
                        //    int NewQty = QUANTITY;
                        //    string Reference = txtrefreshno.Text;
                        //    string Active = "Y";
                        //    string Bin_Per = "N";
                        //    string Fuctions = "ADD";
                        //    string pagename = "Sales";
                        //    Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //}
                        //if (MultiUOM == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "UOM").ToList();
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
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        //if (MultiColor == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "MultiColor").ToList();
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
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_SIZECOLOR(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        //if (MultiSize == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "MultiSize").ToList();
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
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_SIZE(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        //if (Perishable == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "Perishable").ToList();
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
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_Perishable(TenentID, MyProdID, period_code, MySysName, UOM1, BatchNo, LID, MYTRANSID, NewQty, ProdDate, ExpiryDate, LeadDays2Destroy, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        int MyID = MYID;

                        decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                        int DELIVERDLOCATenentID = Convert.ToInt32(0);
                        int QUANTITYDELIVERD = Convert.ToInt32(0);
                        decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                        string ACCOUNTID = "1".ToString();
                        string Remark = "Data Insert formDtext".ToString();
                        int TransNo1 = Convert.ToInt32(0);

                        //ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        //objICTR_DTEXT.TenentID = TenentID;
                        //objICTR_DTEXT.MYTRANSID = MYTRANSID;
                        //objICTR_DTEXT.MyID = MyID;
                        //objICTR_DTEXT.MyRunningSerial = MyRunningSerial;
                        //objICTR_DTEXT.CURRENTCONVRATE = CURRENTCONVRATE;
                        //objICTR_DTEXT.CURRENCY = CURRENCY;
                        //objICTR_DTEXT.OTHERCURAMOUNT = OTHERCURAMOUNT;

                        //objICTR_DTEXT.QUANTITY = QUANTITY;
                        //objICTR_DTEXT.UNITPRICE = UNITPRICE;
                        //objICTR_DTEXT.AMOUNT = AMOUNT;
                        //objICTR_DTEXT.QUANTITYDELIVERD = QUANTITYDELIVERD;
                        //// objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        //objICTR_DTEXT.ACCOUNTID = ACCOUNTID;
                        //objICTR_DTEXT.GRNREF = GRNREF;
                        //objICTR_DTEXT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DTEXT.Remark = Remark;
                        //objICTR_DTEXT.TransNo = TransNo1;
                        //objICTR_DTEXT.ACTIVE = ACTIVE;
                        //DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        //DB.SaveChanges();

                        //MYIDDT++;
                        MyRunningSerial++;

                        //Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                    }
                    string REFERENCE = txtrefreshno.Text;
                    //int ToTenentID = objProFile.ToTenentID; 
                    int TOLOCATIONID = LID;
                    //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    Database.tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objProFile.TranType;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    string LF = "L";
                    string ACTIVITYCODE = "0";
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;
                    //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
                    //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                    decimal TOTAMT, TOTQTY1 = 0;


                    TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);


                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = "99999";
                    string CounterID = objProFile.CounterID;
                    string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                    DateTime TRANSDATE = DateTime.ParseExact(txtOrderDate.Text, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    string NOTES = txtNoteHD.Text;
                    string USERID = UseID.ToString();
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    DateTime UPDTTIME = Curentdatetime;
                    //int InvoiceNO = 0;                    
                    Status = chbDeliNote.Checked == true ? "RDSO" : "SO";
                    decimal Discount = Convert.ToDecimal(0);
                    int Terms = 2114;// Convert.ToInt32(drpterms.SelectedValue);
                    string DatainserStatest = "Update";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "1";
                    }
                    else if (rbtPsle.SelectedValue == "2")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    }
                    string BillNo = drpselsmen.SelectedValue;
                    //string InvoiceNo = Classes.EcommAdminClass.TransDocNo(TID, Transid, Transsubid);
                    string TransDocNo = "";

                    List<Database.ICTR_HD> ListHDD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    int Doc = Convert.ToInt32(ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo);
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

                    ////////////////////////*****/////////////////
                    DateTime Todate = DateTime.Now;
                    Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();
                    bool flag = false;
                    if (DatainserStatest == "Add")
                    {
                        objICTR_HD = new Database.ICTR_HD();
                        flag = true;
                        objICTR_HD.InvoiceNO = InvoiceNo.ToString();

                    }
                    else
                    {
                        objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                    }
                    objICTR_HD.TenentID = TenentID;
                    objICTR_HD.MYTRANSID = MYTRANSID;
                    //objICTR_HD.ToTenentID = ToTenentID;
                    objICTR_HD.locationID = TOLOCATIONID;
                    objICTR_HD.MainTranType = MainTranType;
                    objICTR_HD.TransType = TranType;
                    objICTR_HD.transid = Transid;
                    objICTR_HD.transsubid = Transsubid;
                    objICTR_HD.TranType = TranType;
                    objICTR_HD.COMPID = COMPID;
                    objICTR_HD.MYSYSNAME = MYSYSNAME;
                    objICTR_HD.CUSTVENDID = Convert.ToDecimal(CUSTVENDID);
                    objICTR_HD.LF = LF;
                    objICTR_HD.PERIOD_CODE = PERIOD_CODE;
                    objICTR_HD.ACTIVITYCODE = ACTIVITYCODE;
                    objICTR_HD.MYDOCNO = MYDOCNO;
                    objICTR_HD.USERBATCHNO = USERBATCHNO;
                    objICTR_HD.TOTAMT = TOTAMT;
                    objICTR_HD.TOTQTY = TOTQTY1;
                    objICTR_HD.AmtPaid = AmtPaid;
                    objICTR_HD.PROJECTNO = PROJECTNO;
                    objICTR_HD.CounterID = CounterID;
                    objICTR_HD.PrintedCounterInvoiceNo = PrintedCounterInvoiceNo;
                    objICTR_HD.JOID = JOID;
                    objICTR_HD.TRANSDATE = Convert.ToDateTime(TRANSDATE);
                    objICTR_HD.REFERENCE = REFERENCE;
                    objICTR_HD.NOTES = NOTES;
                    objICTR_HD.GLPOST = GLPOST;
                    objICTR_HD.GLPOST1 = GLPOST1;
                    objICTR_HD.GLPOSTREF = GLPOSTREF;
                    objICTR_HD.GLPOSTREF1 = GLPOSTREF1;
                    objICTR_HD.ICPOST = ICPOST;
                    objICTR_HD.ICPOSTREF = ICPOSTREF;
                    objICTR_HD.USERID = USERID;
                    objICTR_HD.ACTIVE = true;
                    //objICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //objICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    objICTR_HD.COMPANYID = COMPANYID;
                    objICTR_HD.ENTRYDATE = ENTRYDATE;
                    objICTR_HD.ENTRYTIME = ENTRYTIME;
                    objICTR_HD.UPDTTIME = UPDTTIME;
                    objICTR_HD.Discount = Discount;
                    objICTR_HD.Status = Status;
                    objICTR_HD.Terms = Terms;
                    objICTR_HD.ExtraField1 = Custmerid;
                    objICTR_HD.ExtraField1 = Custmerid;
                    objICTR_HD.ExtraField2 = BillNo;
                    objICTR_HD.ExtraSwitch1 = Swit1;
                    objICTR_HD.RefTransID = MYTRANSID;
                    objICTR_HD.TransDocNo = InvoiceNo;

                    // objICTR_HD.ExtraSwitch2 = "";
                    if (flag == true)
                    {
                        DB.ICTR_HD.AddObject(objICTR_HD);
                    }
                    DB.SaveChanges();

                    ////////////////////////*****/////////////////
                    //List<Database.ICTR_HD> ListHDD1 = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

                    var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                    if (SirialNO == InvoiceNo)
                    {
                        Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                        objtblsubtype.serialno = SirialNO;
                        DB.SaveChanges();
                    }




                    //Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, Transid, Transsubid, MYSYSNAME, COMPID1, CUSTVENDID, LF, PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    //var List2 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == TransNo).ToList();
                    //foreach (Database.ICTRPayTerms_HD item in List2)
                    //{
                    //    DB.ICTRPayTerms_HD.DeleteObject(item);
                    //    DB.SaveChanges();
                    //}
                    //decimal Payment = Convert.ToDecimal(lblTotatotl1.Text);
                    //decimal Total = 0;


                    List<Database.ICTRPayTerms_HD> ListHD_TEMP = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                    decimal Payment = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Sum(p => p.AMOUNT));
                    decimal Total = 0;

                    //foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP)
                    //{
                    //    decimal Ammunt = Convert.ToDecimal(items.Amount);
                    //    if (Ammunt != null)
                    //    {
                    //        decimal TXT = Convert.ToDecimal(Ammunt);
                    //        Total = Total + TXT;
                    //    }
                    //}

                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text != "")
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }

                    List<Database.ICTRPayTerms_HD> ListHD_TEMP1 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                    if (Payment == Total)
                    {
                        //foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP1)
                        //{
                        //    int PaymentTermsId = items.PaymentTermsId;
                        //    string IDRefresh = items.ReferenceNo;
                        //    string IdApprouv = items.ApprovalID;
                        //    decimal ammunt = Convert.ToDecimal(items.Amount);

                        //    if (ammunt != null)
                        //    {

                        //        int AccountID = 0;
                        //        Decimal Amount = Convert.ToDecimal(ammunt);
                        //        //**************************************//

                        //        ICTRPayTerms_HD objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID);
                        //        objICTRPayTerms_HD.TenentID = TID;
                        //        objICTRPayTerms_HD.MyTransID = MYTRANSID;
                        //        objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(PaymentTermsId);
                        //        objICTRPayTerms_HD.AccountID = 0;
                        //        objICTRPayTerms_HD.Amount = Convert.ToDecimal(ammunt);
                        //        objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                        //        objICTRPayTerms_HD.ApprovalID = IdApprouv;
                        //        objICTRPayTerms_HD.Draft = true;
                        //        //DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                        //        DB.SaveChanges();


                        //        //***************************************//
                        //        //Classes.EcommAdminClass.insertICTRPayTerms_HD(TID1111, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                        //    }
                        //}
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
                            {
                                string RFresh = txtrefresh.Text.ToString();
                                string[] id = RFresh.Split(',');
                                string IDRefresh = id[0].ToString();
                                string IdApprouv = id[1].ToString();

                                int PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                                int AccountID = 0;
                                Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                                Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                            }
                        }
                    }
                    else
                    {
                        //pnlSuccessMsg123.Visible = true;
                        //lblMsg123.Text = "Total Amount not Macth";
                        return;
                    }

                    btnSaveData.Text = "Confirm Order";
                    btnConfirmOrder.Text = "Confirm Order";
                    Session["Invoice"] = null;
                }
                else
                {
                    TransNo = Convert.ToInt32(Session["GetMYTRANSID"]);
                    int TenentID = TID;
                    int MYTRANSID = TransNo;
                    //int ToTenentID = objProFile.ToTenentID;
                    int TOLOCATIONID = LID;
                    //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                    Database.tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
                    string MainTranType = "O";// Out Qty On Product
                    string TransType = objtbltranssubtype.transsubtype1;
                    string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                    decimal COMPID1 = COMPID;
                    string MYSYSNAME = "SAL".ToString();
                    decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                    string PERIOD_CODE = OICODID;
                    string ACTIVITYCODE = "0";
                    decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                    string USERBATCHNO = txtBatchNo.Text;
                    //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
                    //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                    decimal TOTAMT, TOTQTY1 = 0;
                    if (DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).Count() == 0)
                    {
                        panelMsg.Visible = true;
                        lblErreorMsg.Text = "Kindly please select minimum one Items; to save Invoice";
                        return;
                    }

                    TOTAMT = Convert.ToDecimal(lblTotatotl.Text);
                    TOTQTY1 = Convert.ToDecimal(lblqtytotl.Text);

                    decimal AmtPaid = objProFile.AmtPaid;
                    string PROJECTNO = "99999";
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
                    int COMPANYID = Convert.ToInt32(CURRENCY);
                    bool ACTIVE = Convert.ToBoolean(true);
                    DateTime Curentdatetime = DateTime.Now;
                    DateTime ENTRYDATE = Curentdatetime;
                    DateTime ENTRYTIME = Curentdatetime;
                    int Terms = 2114;// Convert.ToInt32(drpterms.SelectedValue);
                    DateTime UPDTTIME = Curentdatetime;
                    //var inno = DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.InvoiceNO);
                    //long inno1 = Convert.ToInt64(inno);
                    //long inno2 = (inno1) + 1;
                    //string InvoiceNO = inno2.ToString();

                    //int InvoiceNO = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.InvoiceNO) + 1) : 1; 
                    decimal Discount = Convert.ToDecimal(0);
                    Status = chbDeliNote.Checked == true ? "RDSO" : "SO";
                    string DatainserStatest = "Add";
                    string Custmerid = "";
                    string Swit1 = "";
                    if (rbtPsle.SelectedValue == "1")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "1";
                    }
                    else if (rbtPsle.SelectedValue == "2")
                    {
                        Custmerid = txtLocationSearch.Text;
                        Swit1 = "2";
                    }
                    string BillNo = drpselsmen.SelectedValue;
                    //var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    //string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                    //string Yearinvoice = listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).years.ToString();
                    //string CounterName = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).CounterName.ToString();
                    //string InvoiceNo = SirialNO + "/" + Yearinvoice + "/" + TID;
                    // string InvoiceNo = Classes.EcommAdminClass.TransDocNo(TID, Transid, Transsubid);
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
                    //Dipak
                    string InvoiceNo = TransDocNo;

                    //***************************************************//

                    DateTime Todate = DateTime.Now;
                    //Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();
                    Database.ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                    bool flag = false;
                    if (DatainserStatest == "Add")
                    {
                        //objICTR_HD = new Database.ICTR_HD();
                        flag = true;
                        objICTR_HD.InvoiceNO = InvoiceNo.ToString();
                    }
                    else
                    {
                        objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
                    }
                    objICTR_HD.TenentID = TenentID;
                    objICTR_HD.MYTRANSID = MYTRANSID;
                    //objICTR_HD.ToTenentID = ToTenentID;
                    objICTR_HD.locationID = TOLOCATIONID;
                    objICTR_HD.MainTranType = MainTranType;
                    objICTR_HD.TransType = TranType;
                    objICTR_HD.transid = Transid;
                    objICTR_HD.transsubid = Transsubid;
                    objICTR_HD.TranType = TranType;
                    objICTR_HD.COMPID = COMPID;
                    objICTR_HD.MYSYSNAME = MYSYSNAME;
                    objICTR_HD.CUSTVENDID = Convert.ToDecimal(CUSTVENDID);
                    objICTR_HD.LF = "L";
                    objICTR_HD.PERIOD_CODE = PERIOD_CODE;
                    objICTR_HD.ACTIVITYCODE = ACTIVITYCODE;
                    objICTR_HD.MYDOCNO = MYDOCNO;
                    objICTR_HD.USERBATCHNO = USERBATCHNO;
                    objICTR_HD.TOTAMT = TOTAMT;
                    objICTR_HD.TOTQTY = TOTQTY1;
                    objICTR_HD.AmtPaid = AmtPaid;
                    objICTR_HD.PROJECTNO = PROJECTNO;
                    objICTR_HD.CounterID = CounterID;
                    objICTR_HD.PrintedCounterInvoiceNo = PrintedCounterInvoiceNo;
                    objICTR_HD.JOID = JOID;
                    objICTR_HD.TRANSDATE = Convert.ToDateTime(TRANSDATE);
                    objICTR_HD.REFERENCE = REFERENCE;
                    objICTR_HD.NOTES = NOTES;
                    objICTR_HD.GLPOST = GLPOST;
                    objICTR_HD.GLPOST1 = GLPOST1;
                    objICTR_HD.GLPOSTREF = GLPOSTREF;
                    objICTR_HD.GLPOSTREF1 = GLPOSTREF1;
                    objICTR_HD.ICPOST = ICPOST;
                    objICTR_HD.ICPOSTREF = ICPOSTREF;
                    objICTR_HD.USERID = USERID;
                    objICTR_HD.ACTIVE = true;
                    //objICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    //objICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
                    objICTR_HD.COMPANYID = COMPANYID;
                    objICTR_HD.ENTRYDATE = ENTRYDATE;
                    objICTR_HD.ENTRYTIME = ENTRYTIME;
                    objICTR_HD.UPDTTIME = UPDTTIME;
                    objICTR_HD.Discount = Discount;
                    objICTR_HD.Status = Status;
                    objICTR_HD.Terms = Terms;
                    objICTR_HD.ExtraField1 = Custmerid;
                    objICTR_HD.ExtraField2 = BillNo;
                    objICTR_HD.ExtraSwitch1 = Swit1;
                    objICTR_HD.RefTransID = MYTRANSID;
                    objICTR_HD.TransDocNo = InvoiceNo;

                    // objICTR_HD.ExtraSwitch2 = "";
                    if (flag == true)
                    {
                        //DB.ICTR_HD.AddObject(objICTR_HD);
                    }
                    DB.SaveChanges();
                    //***************************************************//

                    //Classes.EcommAdminClass.insertICTR_HD(TenentID, MYTRANSID, ToTenentID, TOLOCATIONID, MainTranType, TranType, Transid, Transsubid, MYSYSNAME, COMPID1, CUSTVENDID, "L", PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO, Discount, Status, Terms, DatainserStatest, Custmerid, Swit1, BillNo, MYTRANSID);
                    // insert deta is ICTR_DT

                    var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                    Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                    objtblsubtype.serialno = SirialNO;
                    DB.SaveChanges();

                    //Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                    //objtblsubtype.serialno = SirialNO;

                    //DB.SaveChanges();


                    //List<ICTR_DT> ListDate = ((List<Database.ICTR_DT>)Session["TempListICTR_DT"]).ToList();
                    List<ICTR_DT> ListDate = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();

                    var listTCTR_DTEXT = Classes.EcommAdminClass.getListICTR_DTEXT(TID, TransNo).ToList();
                    var ListICTR_DT = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID).ToList();
                    int MYIDDT = ListICTR_DT.Count() > 0 ? Convert.ToInt32(ListICTR_DT.Max(p => p.MYID) + 1) : 1;
                    int DXMYID = listTCTR_DTEXT.Count() > 0 ? Convert.ToInt32(listTCTR_DTEXT.Max(p => p.MyID) + 1) : 1;
                    int MyRunningSerial = listTCTR_DTEXT.Where(p => p.MyID == DXMYID).Count() > 0 ? Convert.ToInt32(listTCTR_DTEXT.Where(p => p.MyID == DXMYID).Max(p => p.MyRunningSerial) + 1) : 1;

                    //foreach (Database.ICTR_DT items in ListDate)
                    //{
                    for (int i = 0; i < Repeater2.Items.Count; i++)
                    {
                        Label lblProductNameItem = (Label)Repeater2.Items[i].FindControl("lblProductNameItem"),
                         lblDiscription = (Label)Repeater2.Items[i].FindControl("lblDiscription"),
                         lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM"),
                         lblDisQnty = (Label)Repeater2.Items[i].FindControl("lblDisQnty"),
                         lblTaxDis = (Label)Repeater2.Items[i].FindControl("lblTaxDis"),
                         lblTotalCurrency = (Label)Repeater2.Items[i].FindControl("lblTotalCurrency"),
                         lblUNITPRICE = (Label)Repeater2.Items[i].FindControl("lblUNITPRICE"),
                         lblDISAMT = (Label)Repeater2.Items[i].FindControl("lblDISAMT"),
                         lblDISPER = (Label)Repeater2.Items[i].FindControl("lblDISPER"),
                         lblTAXAMT = (Label)Repeater2.Items[i].FindControl("lblTAXAMT"),
                         lblMYID = (Label)Repeater2.Items[i].FindControl("lblMYID");
                        decimal DISPER = Convert.ToDecimal(lblDISPER.Text);//lblDISPER.Text
                        decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);//lblDISAMT.Text
                        var listTCTR_DT = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
                        //int MYIDDT = listTCTR_DT.Count() > 0 ? Convert.ToInt32(listTCTR_DT.Max(p => p.MYID) + 1) : 1;
                        //int MYID = MYIDDT;
                        //int MyProdID = Convert.ToInt32(lblProductNameItem.Text);
                        //string REFTYPE = "LF", REFSUBTYPE = "LF";

                        //int JOBORDERDTMYID = 1, ACTIVITYID = 0;

                        //string DESCRIPTION = lblDiscription.Text;
                        //string UOM = lblUOM.Text;
                        //int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        //decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);
                        //decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);


                        int MyProdID = Convert.ToInt32(lblProductNameItem.Text);//lblProductNameItem.Text
                        int MYID = Convert.ToInt32(lblMYID.Text);
                        string REFTYPE = "LF";
                        string REFSUBTYPE = "LF";
                        int JOBORDERDTMYID = 1;
                        int ACTIVITYID = 0;
                        string DESCRIPTION = lblDiscription.Text;
                        string UOM = lblUOM.Text;
                        int QUANTITY = Convert.ToInt32(lblDisQnty.Text);
                        decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);//
                        decimal AMOUNT = Convert.ToDecimal(lblTotalCurrency.Text);//lblTotalCurrency.Text
                        decimal OVERHEADAMOUNT = Convert.ToDecimal(0.000);
                        string BATCHNO = "1";
                        int BIN_ID = 1;
                        string BIN_TYPE = "Bin";
                        string GRNREF = "2";
                        int COMPANYID1 = COMPID;
                        int locationID = LID;
                        decimal TAXAMT = Convert.ToDecimal(0.0);
                        decimal TAXPER = Convert.ToDecimal(0.0);
                        decimal PROMOTIONAMT = Convert.ToDecimal(10.00);
                        DateTime EXPIRYDATE = DateTime.Now;
                        string SWITCH1 = "0".ToString();
                        int DelFlag = 0;
                        string ITEMID = "1";

                        int uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == MyProdID && p.LOCATION_ID == LID).UOM);
                        decimal CostAmount = Classes.EcommAdminClass.CostAmount(TID, MyProdID, LID, uom);

                        //******************************************//

                        //Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();


                        //objICTR_DT.TenentID = TenentID;
                        //objICTR_DT.MYTRANSID = MYTRANSID;
                        //objICTR_DT.locationID = locationID;

                        //objICTR_DT.MYID = MYIDDT;
                        //objICTR_DT.MyProdID = MyProdID;
                        //objICTR_DT.REFTYPE = REFTYPE;
                        //objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                        //objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                        //objICTR_DT.MYSYSNAME = MYSYSNAME;
                        //objICTR_DT.JOID = JOID;
                        //objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                        //objICTR_DT.ACTIVITYID = ACTIVITYID;
                        //objICTR_DT.DESCRIPTION = DESCRIPTION;
                        //objICTR_DT.UOM = UOM;
                        //objICTR_DT.QUANTITY = QUANTITY;
                        //objICTR_DT.UNITPRICE = UNITPRICE;
                        //objICTR_DT.AMOUNT = AMOUNT;
                        //objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                        //objICTR_DT.BATCHNO = BATCHNO;
                        //objICTR_DT.BIN_ID = BIN_ID;
                        //objICTR_DT.BIN_TYPE = BIN_TYPE;
                        //objICTR_DT.GRNREF = GRNREF;
                        //objICTR_DT.DISPER = DISPER;
                        //objICTR_DT.DISAMT = DISAMT;
                        //objICTR_DT.TAXAMT = TAXAMT;
                        //objICTR_DT.TAXPER = TAXPER;
                        //objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                        //objICTR_DT.GLPOST = GLPOST;
                        //objICTR_DT.GLPOST1 = GLPOST1;
                        //objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                        //objICTR_DT.GLPOSTREF = GLPOSTREF;
                        //objICTR_DT.ICPOST = ICPOST;
                        //objICTR_DT.ICPOSTREF = ICPOSTREF;
                        //objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DT.COMPANYID = COMPANYID1;
                        //objICTR_DT.SWITCH1 = SWITCH1;
                        //objICTR_DT.ACTIVE = ACTIVE;
                        //objICTR_DT.DelFlag = DelFlag;
                        //objICTR_DT.CostAmount = CostAmount;


                        //DB.ICTR_DT.AddObject(objICTR_DT);
                        //DB.SaveChanges();
                        //ViewState["postDT"] = objICTR_DT;

                        //******************************************//
                        Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTRANSID, LID, MYID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                        //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                        Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
                        Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
                        Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
                        Boolean Perishable = Convert.ToBoolean(obj.Perishable);
                        Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
                        Boolean Serialized = Convert.ToBoolean(obj.Serialized);
                        Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);




                        Database.ICTR_DT objDT = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.locationID == LID && p.MYID == MYID);
                        Database.ICTR_HD Objhd = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);

                        int UOM1 = Convert.ToInt32(objDT.UOM);
                        string Reference = Objhd.REFERENCE.ToString();
                        DateTime trnsDate = Convert.ToDateTime(Objhd.TRANSDATE);
                        string MySysName = Objhd.MYSYSNAME.ToString();

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


                        bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, Transid, Transsubid, MYTRANSID, MYIDDT, QUANTITY, Reference, trnsDate, MySysName, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
                        if (flag1 == false)
                        {
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "One of the Posting Parameter is Blank/Null/Zero!", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                            return;

                        }

                        //int PQTY = Convert.ToInt32(obj.QTYINHAND);
                        //int Total123 = PQTY - QUANTITY;
                        //obj.QTYINHAND = Total123;
                        //DB.SaveChanges();
                        //var listICIT_BR_TMP = Classes.EcommAdminClass.getListICIT_BR_TMP(TID, MYTRANSID, MyProdID).ToList();
                        //if (MultiBinStore == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "MultiBIN").ToList();
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
                        //        string pagename = "Sales";
                        //        string Fuctions = "ADD";
                        //        Classes.EcommAdminClass.insertICIT_BR_BIN(TenentID, MyProdID, period_code, MySysName, Bin_ID, UOM1, LID, BatchNo, MYTRANSID, NewQty, Reference, Active, CRUP_ID, pagename, Fuctions);
                        //    }
                        //}
                        //if (Serialized == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "Serialize").ToList();
                        //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                        //    {
                        //        string period_code = OICODID;
                        //        string MySysName = "SAL";
                        //        string Serial_Number = listmulticolor[ii].Serial_Number;
                        //        int MyTransID = Convert.ToInt32(listmulticolor[ii].MYTRANSID);
                        //        string Active = "Y";
                        //        string Flag_GRN_GTN = "N";
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_Serialize(TenentID, MyProdID, period_code, MySysName, UOM, Serial_Number, LID, MyTransID, Flag_GRN_GTN, Active, CRUP_ID, pagename);
                        //    }
                        //}
                        //if (MultiUOM != Convert.ToBoolean(1))
                        //{
                        //    int UOM1 = Convert.ToInt32(items.UOM);//lblUOM.Text
                        //    var listICIT_BR = DB.ICIT_BR.Where(p => p.TenentID == TenentID && p.MyProdID == MyProdID && p.MySysName == MYSYSNAME && p.UOM == UOM1).ToList();
                        //    string period_code = OICODID;
                        //    string MySysName = "IC";
                        //    decimal UnitCost = UNITPRICE;
                        //    int NewQty = QUANTITY;
                        //    string Reference = txtrefreshno.Text;
                        //    string Active = "Y";
                        //    string Bin_Per = "N";
                        //    string Fuctions = "ADD";
                        //    string pagename = "Sales";
                        //    Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //}
                        //if (MultiUOM == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "UOM").ToList();
                        //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                        //    {
                        //        int UOM1 = listmulticolor[ii].UOM;
                        //        string period_code = listmulticolor[ii].period_code;
                        //        string MySysName = "IC";
                        //        decimal UnitCost = UNITPRICE;
                        //        int NewQty = Convert.ToInt32(listmulticolor[ii].NewQty);
                        //        string Reference = listmulticolor[ii].Reference;
                        //        string Active = "Y";
                        //        string Bin_Per = "N";
                        //        string Fuctions = "ADD";
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        //if (MultiColor == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "MultiColor").ToList();
                        //    for (int ii = 0; ii < listmulticolor.Count; ii++)
                        //    {
                        //        string period_code = OICODID;
                        //        string MySysName = "SAL";
                        //        int UO1M = listmulticolor[ii].UOM;
                        //        int SIZECODE = 999999998;
                        //        int COLORID = listmulticolor[ii].COLORID;
                        //        int Nweqty = Convert.ToInt32(listmulticolor[ii].NewQty);
                        //        string Reference = listmulticolor[ii].Reference;
                        //        string Active = "Y";
                        //        string Fuctions = "ADD";
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_SIZECOLOR(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, Nweqty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        //if (MultiSize == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "MultiSize").ToList();
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
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_SIZE(TenentID, MyProdID, period_code, MySysName, UO1M, SIZECODE, COLORID, LID, MYTRANSID, NewQty, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }
                        //}
                        //if (Perishable == Convert.ToBoolean(1))
                        //{
                        //    var listmulticolor = listICIT_BR_TMP.Where(p => p.RecodName == "Perishable").ToList();
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
                        //        string pagename = "Sales";
                        //        Classes.EcommAdminClass.insertICIT_BR_Perishable(TenentID, MyProdID, period_code, MySysName, UOM1, BatchNo, LID, MYTRANSID, NewQty, ProdDate, ExpiryDate, LeadDays2Destroy, Reference, Active, CRUP_ID, Fuctions, pagename);
                        //    }

                        //}


                        int MyID = MYID;
                        decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                        int DELIVERDLOCATenentID = Convert.ToInt32(0);
                        int QUANTITYDELIVERD = Convert.ToInt32(0);
                        decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                        string ACCOUNTID = "1".ToString();
                        string Remark = "Data Insert formDtext".ToString();
                        int TransNo1 = Convert.ToInt32(0);


                        //ICTR_DTEXT objICTR_DTEXT = new ICTR_DTEXT();
                        //objICTR_DTEXT.TenentID = TenentID;
                        //objICTR_DTEXT.MYTRANSID = MYTRANSID;
                        //objICTR_DTEXT.MyID = MyID;
                        //objICTR_DTEXT.MyRunningSerial = MyRunningSerial;
                        //objICTR_DTEXT.CURRENTCONVRATE = CURRENTCONVRATE;
                        //objICTR_DTEXT.CURRENCY = CURRENCY;
                        //objICTR_DTEXT.OTHERCURAMOUNT = OTHERCURAMOUNT;

                        //objICTR_DTEXT.QUANTITY = QUANTITY;
                        //objICTR_DTEXT.UNITPRICE = UNITPRICE;
                        //objICTR_DTEXT.AMOUNT = AMOUNT;
                        //objICTR_DTEXT.QUANTITYDELIVERD = QUANTITYDELIVERD;
                        //// objICTR_DTEXT.DELIVERDLOCATenentID = 0;
                        //objICTR_DTEXT.ACCOUNTID = ACCOUNTID;
                        //objICTR_DTEXT.GRNREF = GRNREF;
                        //objICTR_DTEXT.EXPIRYDATE = EXPIRYDATE;
                        //objICTR_DTEXT.Remark = Remark;
                        //objICTR_DTEXT.TransNo = TransNo1;
                        //objICTR_DTEXT.ACTIVE = ACTIVE;
                        //DB.ICTR_DTEXT.AddObject(objICTR_DTEXT);
                        //DB.SaveChanges();
                        //DXMYID++;
                        MyRunningSerial++;
                        //Classes.EcommAdminClass.insertICTR_DTEXT(TenentID, MYTRANSID, MyID, MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATenentID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, CRUP_ID, Remark, TransNo1, ACTIVE);
                    }
                    //decimal Payment = Convert.ToDecimal(lblTotatotl1.Text);
                    //decimal Total = 0;

                    List<Database.ICTRPayTerms_HD> ListHD_TEMP = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.Draft == false).ToList();

                    decimal Payment = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Sum(p => p.AMOUNT));
                    decimal Total = 0;

                    //foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP)
                    //{
                    //    decimal Ammunt = Convert.ToDecimal(items.Amount);
                    //    if (Ammunt != null)
                    //    {
                    //        decimal TXT = Convert.ToDecimal(Ammunt);
                    //        Total = Total + TXT;
                    //    }
                    //}


                    for (int i = 0; i < Repeater3.Items.Count; i++)
                    {
                        TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        if (txtammunt.Text != "")
                        {
                            decimal TXT = Convert.ToDecimal(txtammunt.Text);
                            Total = Total + TXT;
                        }
                    }

                    //List<Database.ICTRPayTerms_HD> ListHD_TEMP1 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.Draft == false).ToList();


                    //if (Payment == Total)
                    //{
                    //    foreach (Database.ICTRPayTerms_HD items in ListHD_TEMP1)
                    //    {
                    //        int PaymentTermsId = items.PaymentTermsId;
                    //        string IDRefresh = items.ReferenceNo;
                    //        string IdApprouv = items.ApprovalID;
                    //        decimal ammunt = Convert.ToDecimal(items.Amount);

                    //        if (ammunt != null)
                    //        {

                    //            int AccountID = 0;
                    //            Decimal Amount = Convert.ToDecimal(ammunt);

                    //            ICTRPayTerms_HD objICTRPayTerms_HD = DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MYTRANSID && p.PaymentTermsId == PaymentTermsId);
                    //            objICTRPayTerms_HD.TenentID = TID;
                    //            objICTRPayTerms_HD.LocationID = LID;
                    //            objICTRPayTerms_HD.MyTransID = MYTRANSID;
                    //            objICTRPayTerms_HD.PaymentTermsId = Convert.ToInt32(PaymentTermsId);
                    //            objICTRPayTerms_HD.AccountID = 0;
                    //            objICTRPayTerms_HD.Amount = Convert.ToDecimal(ammunt);
                    //            objICTRPayTerms_HD.ReferenceNo = IDRefresh;
                    //            objICTRPayTerms_HD.ApprovalID = IdApprouv;
                    //            objICTRPayTerms_HD.Draft = true;

                    //            objICTRPayTerms_HD.CounterID = CounterID;
                    //            objICTRPayTerms_HD.TransDate = Convert.ToDateTime(TRANSDATE);


                    //            DB.ICTRPayTerms_HD.AddObject(objICTRPayTerms_HD);
                    //            DB.SaveChanges();


                    //            Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                    //        }
                    //    }

                    //}


                    if (Payment == Total)
                    {
                        for (int j = 0; j < Repeater3.Items.Count; j++)
                        {
                            DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                            TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                            TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                            if (txtrefresh.Text != "" && txtammunt.Text != "")
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
                    //GridData();
                    //readMode();
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
                BindHD(TransNo);
                scope.Complete();

            }
            readMode();
            GridData();
        }

        //public void PostProcessWithReceipe(int RecNo,int MYTRANSID, string Reference,DateTime trnsDate,string MySysName, string ICPOST)
        //{
        //    List<Database.Receipe_Menegement> ListRecipe = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNo).ToList();

        //    List<Database.Receipe_Menegement> ListInput = ListRecipe.Where(p => p.IOSwitch == "Input").ToList();
        //    foreach(Database.Receipe_Menegement items in ListInput)
        //    {
        //        int QUANTITY = Convert.ToInt32(items.Qty);
        //        int MyProdID = Convert.ToInt32(items.ItemCode);
        //        int UOM1 = Convert.ToInt32(items.UOM);

        //        if(DB.TBLPRODUCTs.Where(p => p.MYPRODID == MyProdID && p.TenentID == TID).Count()>0)
        //        {
        //            Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);

        //            decimal UNITPRICE = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MyProdID && p.UOM == UOM1).Count() > 0 ? Convert.ToDecimal(DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MyProdID && p.UOM == UOM1).FirstOrDefault().onlinesale1) : 0;

        //            bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, Transid, Transsubid, MYTRANSID, 0, QUANTITY, Reference, trnsDate, MySysName, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
        //            if (flag1 == false)
        //            {
        //                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "One of the Posting Parameter is Blank/Null/Zero!", "Error!", Classes.Toastr.ToastPosition.TopCenter);
        //                return;
        //            }
        //        }
               
        //    }


        //    List<Database.Receipe_Menegement> ListOutput = ListRecipe.Where(p => p.IOSwitch == "Output").ToList();
        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string PrintURL = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).InvoicePrintURL;
            FinalConfirm();

            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo && p.Status == "SO").Count() > 0)
            {
                if (TransNo != 0)
                {
                    //scope.Complete(); //  To commit.
                    if (TID == 7)
                        Response.Redirect("ServiesHubPrints.aspx?Tranjestion=" + TransNo);
                    else if (TID == 6)
                        Response.Redirect(PrintURL + TransNo);
                    else if (TID == 600)
                        Response.Redirect(PrintURL + TransNo);
                    else
                        Response.Redirect("LocalInvoiceForStaff.aspx?Tranjestion=" + TransNo);
                    //}
                }

                Session["GetMYTRANSID"] = null;
                Session["Invoice"] = null;
            }
            else
            {
                return;
            }



        }

        protected void CheckgrdPO_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckgrdPO.Checked == true)
            {
                SessionLoad();
                // create glogle function
                //var list = Classes.EcommAdminClass.grdPOICTR_HD(TID, objtblsetupsalesh.transid, objtblsetupsalesh.transsubid);
                //grdPO.DataSource = list.Where(p => p.Status == "SO" || p.Status == "DSO").OrderByDescending(p => p.TRANSDATE);
                //grdPO.DataBind();

                grdPO.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == false && ((p.Status == "SO" || p.Status == "DSO") && p.transid == Transid && p.transsubid == Transsubid)).OrderByDescending(p => p.MYTRANSID);
                grdPO.DataBind();
                for (int ii = 0; ii < grdPO.Items.Count; ii++)
                {
                    Label lblMYTRANSID = (Label)grdPO.Items[ii].FindControl("lblMYTRANSID");
                    stMYTRANSID = Convert.ToInt32(lblMYTRANSID.Text);
                    if (ii == 0)
                    {


                        //BindHD(stMYTRANSID);

                        string ToplblAllowMinusQty = objtblsetupsalesh.AllowMinusQty == false ? "Quantity in Minus Not Allowed" : "Allow Minus Quantity";


                        string TranType = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid).transsubtype1;
                        lbllabellist.Text = TranType + " List";
                        lbllistname.Text = TranType + " - " + ToplblAllowMinusQty;
                        // create glogle function
                        //var list = Classes.EcommAdminClass.grdPOICTR_HD(TID, Transid, Transsubid).ToList();
                        var list = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == false).ToList();
                        var OBJICTR_HD = list.SingleOrDefault(p => p.MYTRANSID == stMYTRANSID && (p.Status == "SO" || p.Status == "DSO"));
                        if (OBJICTR_HD != null)
                        {
                            drpselsmen.SelectedValue = OBJICTR_HD.ExtraField2;
                            CID = Convert.ToInt32(OBJICTR_HD.CUSTVENDID);
                            // create glogle function

                            // TBLCOMPANYSETUP onj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID);
                            txtLocationSearch.Text = Classes.EcommAdminClass.CompnyName_TBLCOMPANYSETUP(TID, CID);
                            HiddenField3.Value = CID.ToString();
                            //txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");

                            if (OBJICTR_HD.USERBATCHNO != null && OBJICTR_HD.USERBATCHNO != "")
                            {
                                txtBatchNo.Text = OBJICTR_HD.USERBATCHNO.ToString();
                            }
                            if (OBJICTR_HD.TRANSDATE != null)
                            {
                                txtOrderDate.Text = OBJICTR_HD.TRANSDATE.ToString("dd-MMM-yy");
                            }
                            if (OBJICTR_HD.MYTRANSID != null)
                            {
                                txtTraNoHD.Text = OBJICTR_HD.MYTRANSID.ToString();
                            }
                            if (OBJICTR_HD.REFERENCE != null && OBJICTR_HD.REFERENCE != "")
                            {
                                txtrefreshno.Text = OBJICTR_HD.REFERENCE.ToString();
                            }
                            if (OBJICTR_HD.NOTES != null && OBJICTR_HD.NOTES != "")
                            {
                                txtNoteHD.Text = OBJICTR_HD.NOTES.ToString();
                            }
                            if (OBJICTR_HD.TOTAMT != null)
                            {
                                txttotxl.Text = OBJICTR_HD.TOTAMT.ToString();
                            }

                            // TextBox2.Text = OBJICTR_HD.TransDocNo.ToString();
                            //if (OBJICTR_HD.Terms != null && OBJICTR_HD.Terms == 0)
                            //{
                            //    drpterms.SelectedValue = OBJICTR_HD.Terms.ToString();
                            //}
                            //else
                            //{
                            //    drpterms.SelectedValue = "2114";
                            //}
                        }


                        //BindDT(stMYTRANSID);yogesh
                        //BindDTui(stMYTRANSID);

                        //var List = Classes.EcommAdminClass.getListICTR_DT(TID, MYTRANSID).ToList();
                        var List = DB.ICTR_DT.Where(p => p.MYTRANSID == stMYTRANSID && p.ACTIVE == false && p.TenentID == TID).ToList();
                        Repeater2.DataSource = List;
                        Repeater2.DataBind();
                        //Session["TempListICTR_DT"] = List;
                        decimal TottalSum = Convert.ToDecimal(List.Sum(p => p.AMOUNT));
                        //txttotxl.Text = TottalSum.ToString();yogesh
                        int QTUtotal = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                        lblqtytotl.Text = QTUtotal.ToString();
                        decimal UNPTOL = Convert.ToDecimal(List.Sum(p => p.UNITPRICE));
                        lblUNPtotl.Text = UNPTOL.ToString();
                        decimal TaxTol = Convert.ToDecimal(List.Sum(p => p.TAXPER));
                        lblTotatotl.Text = TottalSum.ToString();
                        //for (int i = 0; i < Repeater3.Items.Count; i++)
                        //{
                        //    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                        //    txtammunt.Text = TottalSum.ToString();
                        //}

                        Repeater3.DataSource = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == stMYTRANSID);
                        Repeater3.DataBind();

                        ViewState["TempListICTR_DT"] = List;
                        //panelRed.Visible = false;
                        readMode();
                    }
                    LinkButton Print = (LinkButton)grdPO.Items[ii].FindControl("Print");
                    LinkButton lnkbtnPurchase_Order = (LinkButton)grdPO.Items[ii].FindControl("lnkbtnPurchase_Order");
                    Label lbluserid = (Label)grdPO.Items[ii].FindControl("lbluserid");
                    Label lblStatus = (Label)grdPO.Items[ii].FindControl("lblStatus");
                    string Steate = lblStatus.Text;
                    if (Steate == "DSO") Print.Visible = false;
                    //if (Steate == "SO") lnkbtnPurchase_Order.Visible = true;
                    if (Steate == "DSO") lnkbtnPurchase_Order.Visible = true;
                    LinkButton lnkbtndelete = (LinkButton)grdPO.Items[ii].FindControl("lnkbtndelete");
                    string Active = Status(stMYTRANSID);
                    if (Active == "Deleted")
                    {
                        Print.Visible = true;
                        lnkbtndelete.Visible = false;
                        lnkbtnPurchase_Order.Visible = false;
                    }
                    //lnkbtnPurchase_Order.Visible = false;
                }
            }
            else
            {
                GridData();
            }

        }

        public string Status(int MYTRANSID)
        {
            var listHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
            if (listHD.Count() > 0)
            {
                string Status = listHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Status;
                bool Active = Convert.ToBoolean(listHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ACTIVE);
                if (Status == "SO" && Active == true)
                {
                    return "Final";
                }
                else if (Status == "DSO" && Active == true)
                {
                    return "Draft";
                }
                else
                {
                    return "Deleted";
                }
            }
            else
            {
                return "";
            }

        }

        protected void txtQuantity_TextChanged1(object sender, EventArgs e)
        {
            pnlSuccessMsg123.Visible = false;
            lblMsg123.Text = "";
            DateTime TACtionDate = DateTime.Now;//Convert.ToDateTime(txtOrderDate.Text);yogesh
            string OICODID = Pidalcode(TACtionDate).ToString();
            ViewState["SaveList1"] = null;
            if (ddlProduct.SelectedValue == "0" || ddlProduct.SelectedValue == "")
            {
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = "Select Product";

            }
            else
            {
                int uom = Convert.ToInt32(ddlUOM.SelectedValue);
                int PID = Convert.ToInt32(ddlProduct.SelectedValue);
                Database.ICIT_BR objbr = DB.ICIT_BR.Single(p => p.TenentID == TID && p.MyProdID == PID && p.LocationID == LID && p.UOM == uom);
                Database.TBLPRODUCT objTBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID);
                int QTY = Convert.ToInt32(objbr.OnHand);
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

                if (Serialized == Convert.ToBoolean(1))
                {
                    lblmultiserialize.Visible = true;  //Come from MultiTransaction
                    if (txtQuantity.Text == "0")
                    { }
                    else
                    {
                        List<Database.ICIT_BR_Serialize> ListSerialize = new List<ICIT_BR_Serialize>();
                        List<Database.ICIT_BR_Serialize> Listserl = DB.ICIT_BR_Serialize.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.Active == "Y").OrderBy(p => p.Serial_Number).ToList();

                        int rqty = 0;

                        foreach (Database.ICIT_BR_Serialize items in Listserl)
                        {
                            ListSerialize.Add(items);
                        }
                        if (QTY <= TexQty)
                        {
                            int avelable = Listserl.Count();
                            rqty = TexQty - avelable;

                            for (int p = 0; p <= rqty - 1; p++)
                            {
                                Database.ICIT_BR_Serialize objseri = new ICIT_BR_Serialize();
                                ListSerialize.Add(objseri);
                            }
                        }


                        ViewState["ListofSerlNumber"] = ListSerialize;
                        listSerial.DataSource = ListSerialize;  //Come from MultiTransaction
                        listSerial.DataBind();

                        if (QTY <= TexQty)
                        {
                            for (int p = 1; p <= rqty; p++)
                            {
                                TextBox txtlistSerial = (TextBox)listSerial.Items[p].FindControl("txtlistSerial");
                                txtlistSerial.Enabled = true;
                            }
                        }

                    }
                    ModalPopupExtender2.Show();
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
                //if (MultiUOM == Convert.ToBoolean(1))
                //{
                //    lblmultiuom.Visible = true;  //Come from MultiTransaction
                //    var Listserl = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID && p.period_code == OICODID && p.OnHand != 0).ToList();
                //    lidtUom.DataSource = Listserl;
                //    lidtUom.DataBind();

                //}
                //else
                //{
                //    lblmultiuom.Visible = false;  //Come from MultiTransaction
                //}

                lbltotalqty.Text = lbl1.Text = lbl2.Text = lbl3.Text = lbl4.Text = lbl5.Text = "   Total Qty " + TexQty.ToString();

                TotalPriceui();
                txtQuantity.Focus();
            }
        }
        protected void txtUPriceLocal_TextChanged1(object sender, EventArgs e)
        {

            //checkPRice();
            TotalPriceui();
        }

        protected void txtDiscount_TextChanged1(object sender, EventArgs e)
        {
            TotalPriceui();
        }

        protected void LnkBtnSavePayTerm_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                decimal total = 0;
                for (int j = 0; j < Repeater3.Items.Count; j++)
                {
                    DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                    TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                    TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                    total += Convert.ToDecimal(txtammunt.Text);
                }

                decimal Totallist = Convert.ToDecimal(lblTotatotl.Text);
                if (total == Totallist)
                {
                    stMYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);
                    pnlSuccessMsg123.Visible = false;


                    //foreach (Database.ICTRPayTerms_HD ItemHD in ListPayTemp)
                    //{

                    //    DB.ICTRPayTerms_HD.DeleteObject(ItemHD);
                    //    DB.SaveChanges();

                    //}

                    for (int j = 0; j < Repeater3.Items.Count; j++)
                    {
                        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                        if (txtrefresh.Text != "" && txtammunt.Text != "")
                        {
                            string RFresh = txtrefresh.Text.ToString();
                            string[] id = RFresh.Split(',');
                            string IDRefresh = id[0].ToString();
                            string IdApprouv = id[1].ToString();

                            int PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                            Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                            List<Database.ICTRPayTerms_HD> ListPayTemp = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == stMYTRANSID && p.PaymentTermsId == PaymentTermsId).ToList();

                            if (ListPayTemp.Count() > 0)
                            {
                                Database.ICTRPayTerms_HD objICTRPayTerms_HD = ListPayTemp.Single(p => p.TenentID == TID && p.MyTransID == stMYTRANSID && p.PaymentTermsId == PaymentTermsId);

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
                                objICTRPayTerms_HD.MyTransID = stMYTRANSID;
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
                    //LnkBtnSavePayTerm.Visible = false;
                }
                else
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Check Your Pamment Total is not match";
                }



                scope.Complete();
            }
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
            decimal qty = txtQuantity.Text != "" ? Convert.ToDecimal(txtQuantity.Text) : 0;
            if (txtDiscount.Text.Contains("%"))
            {
                str = txtDiscount.Text.Replace('%', ' ');
                Priesh = txtUPriceLocal.Text != "" ? Convert.ToDecimal(txtUPriceLocal.Text) : 0;
                Total = qty * Priesh;
                DICUNT = Convert.ToDecimal(str);
                DICUNTOTAL = Total - (Total * (DICUNT / 100));
                txtTotalCurrencyLocal.Text = DICUNTOTAL.ToString();
            }
            else
            {
                str = txtDiscount.Text;
                Priesh = txtUPriceLocal.Text != "" ? Convert.ToDecimal(txtUPriceLocal.Text) : 0;
                Total = qty * Priesh;
                DICUNT = Convert.ToDecimal(str);
                DICUNTOTAL = Total - DICUNT;
                txtTotalCurrencyLocal.Text = DICUNTOTAL.ToString();
            }
        }


        protected void lnkUomConversion_Click(object sender, EventArgs e)
        {
            //PenalUomConversion.Visible = true;
            int PID = Convert.ToInt32(ViewState["PID"]);

            //ListBR.DataSource = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.MyProdID == PID);
            //ListBR.DataBind();

            string Url = "../ECOMM/UOM_Qty_Convertion.aspx?PID=" + PID;
            string s = "window.open('" + Url + "', 'popup_window', 'width=950,height=225,left=100,top=100,resizable=yes');";

            ScriptManager.RegisterStartupScript(itemsupde, itemsupde.GetType(), "script", s, true);
        }


        protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            int uom = Convert.ToInt32(ddlUOM.SelectedValue);
            int PID = Convert.ToInt32(ddlProduct.SelectedValue);

            if (DB.ICIT_BR.Where(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).Count() > 0)
            {
                lblAvailableQty.Text = "Available Qty : " + DB.ICIT_BR.Single(p => p.MyProdID == PID && p.TenentID == TID && p.LocationID == LID && p.UOM == uom).OnHand.ToString();
            }
            else
            {
                lblAvailableQty.Text = "Available Qty : 0";
            }
            txtUPriceLocal.Text = DB.ICITEMS_PRICE.Single(p => p.TenentID == TID && p.MYPRODID == PID && p.UOM == uom).onlinesale1.ToString();
            TotalPriceui();
        }



        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Congrats! You have been won a cash sum of $5k", "Winner!", Classes.Toastr.ToastPosition.BottomLeft);
        //}


    }
}