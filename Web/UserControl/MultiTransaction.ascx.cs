using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.UserControl
{
    public partial class MultiTransaction : System.Web.UI.UserControl
    {
        CallEntities DB = new CallEntities();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        
       
        List<Database.tblsetupsalesh> Listtblsetupsalesh = new List<tblsetupsalesh>();
        tblsetupsalesh objtblsetupsalesh = new tblsetupsalesh();
        MYCOMPANYSETUP objMyCompany = new MYCOMPANYSETUP();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        decimal CURRENTCONVRATE = Convert.ToDecimal(0);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if(!IsPostBack)
            {
               
            }
        }

        public void SessionLoad()
        {
            objMyCompany = ((MYCOMPANYSETUP)Session["objMYCOMPANYSETUP"]);
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            
            string USERID = UID.ToString();

            if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            {
                Transid = Convert.ToInt32(Request.QueryString["transid"]);
                Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
            }
            objtblsetupsalesh = Classes.Transaction.DEfaultSalesSetup(TID, LID, Transid, Transsubid, 10);
            //CURRENCY = objtblsetupsalesh.COUNTRYID.ToString();
           // CURRENTCONVRATE = DB.tblCOUNTRies.SingleOrDefault(p => p.COUNTRYID == objtblsetupsalesh.COUNTRYID).CURRENTCONVRATE;

            // Multi Process
            if (Session["MultiTransactionProdObj"] != null)
            {
                TBLPRODUCT objTBLPRODUCT = ((TBLPRODUCT)Session["MultiTransactionProdObj"]);

                Boolean Perishable = Convert.ToBoolean(objTBLPRODUCT.Perishable);
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


        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
            // Remain ACM Work
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
                        int UOM = 0;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
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
                        int UOM = 0;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
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
                        int UOM = 0;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
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
                        int TCID =0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice
                        int UomID =0;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int BinID = Convert.ToInt32(lblbinid.Text);

                        int TenentID = TID;
                        int MyProdID = PID;
                        string period_code = OICODID;
                        string MySysName = "SAL";
                        int UOM =0;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        //int BinID = 999999998;
                        string BatchNo ="0";// txtBatchNo.Text;come from invoice
                        string Serialize = "NO";
                        int MYTRANSID = TCID;
                        int LocationID = LID;
                        int NewQty = Convert.ToInt32(txtqty.Text);
                        string Reference ="0";// txtrefreshno.Text;come from invoice
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
            int QTY =0;// Convert.ToInt32(txtQuantity.Text);come from invoice
            //DateTime TACtionDate = Convert.ToDateTime(txtOrderDate.Text);
            string OICODID =DateTime.Now.ToString();// Pidalcode(TACtionDate).ToString();
            int Total = 0;
            string[] Seperate4 = tags_2.Text.Split(',');
            Total = Seperate4.Count();
            if (QTY == Total)
            {
                string Discpition ="";// txtDescription.Text;come from invoice
                string MainDecrption = tags_2.Text;
                MainDecrption = Discpition + "\n" + "Serialize : " + MainDecrption;
                //txtDescription.Text = MainDecrption;come from invoice

                string[] str = tags_2.Text.Split(',');
                int count5 = 0;
                string Sep5 = "";
                for (int i = 0; i <= str.Count() - 1; i++)
                {
                    int PID =0;// Convert.ToInt32(ddlProduct.SelectedValue);come from invoice
                    int TCID =0;// Convert.ToInt32(txtTraNoHD.Text);come from invoice

                    int TenentID = TID;
                    int MyProdID = PID;
                    string period_code = OICODID;
                    string MySysName = "SAL";
                    int UOM =0;// Convert.ToInt32(ddlUOM.SelectedValue);come from invoice
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
       
    }
}