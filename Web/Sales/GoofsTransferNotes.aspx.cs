using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
namespace Web.Sales
{
    public partial class GoofsTransferNotes : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<ICIT_BR_SIZECOLOR> ListICIT_BR_SIZECOLOR = new List<ICIT_BR_SIZECOLOR>();
        PropertyFile objProFile = new PropertyFile();
        bool FirstFlag = true;
        int TID, LID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                BindData();
                BindDoropDown();
                if (Request.QueryString["Tranjestion"] != null)
                {

                    int TRCtion = Convert.ToInt32(Request.QueryString["Tranjestion"]);
                    int str2 = 0;
                    BibdEditmode(TRCtion);
                }
                else
                {
                    pnlCreateForm.Style.Add("display", "none");
                    pnlGridView.Style.Add("display", "block");
                    Button3.Visible = false;
                    btnSubmit.Visible = false;
                    btnRequestde.Visible = false;
                }


            }

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

        }
        public void BindDoropDown()
        {
            
            Classes.EcommAdminClass.getdropdown(ddlLocalForeign, TID, "LF", "OTH", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlCrmAct, TID, "ACTVTY", "POS", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlSupplier, TID, "1", "2", "", "TBLCOMPANYSETUP");
            Classes.EcommAdminClass.getdropdown(ddlCurrency, TID, "1", "", "", "tblCOUNTRies");
            Classes.EcommAdminClass.getdropdown(ddlProjectNo, TID, "", "", "", "Eco_TBLPROJECT");
            //Classes.EcommAdminClass.getdropdown(ddlProduct, TID, USERID, "", "", "TBLPRODUCT");
            //Classes.EcommAdminClass.getdropdown(ddlUOM, TID, "", "", "", "Eco_ICUOM");
            //dropRefNo.DataSource = DB.ICIT_BR_ReferenceNo.Where(p => p.Active == true && p.Deleted == true);
            //dropRefNo.DataTextField = "ReferenceNo";
            //dropRefNo.DataValueField = "ID";
            //dropRefNo.DataBind();
            //dropRefNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Reference No--", "0"));
        }
        public void BindData()
        {
            var result1 = (from pm in DB.ICTR_HD
                           join
                             ur in DB.ICTR_DT on pm.MYTRANSID equals ur.MYTRANSID
                           where pm.ACTIVE == true && pm.Status == "SO" || pm.Status == "DSO"
                           select new { pm.CUSTVENDID, pm.TRANSDATE, pm.TOTAMT, pm.MYTRANSID, pm.Status }).ToList();
            var List = result1.GroupBy(p => p.MYTRANSID).Select(p => p.FirstOrDefault()).ToList();
            grdPO.DataSource = List;
            grdPO.DataBind();
            for (int i = 0; i < grdPO.Items.Count; i++)
            {
                LinkButton lnkbtnPQ = (LinkButton)grdPO.Items[i].FindControl("lnkbtnPQ");
                LinkButton lnkbtndelete = (LinkButton)grdPO.Items[i].FindControl("lnkbtndelete");
                LinkButton btnDarf = (LinkButton)grdPO.Items[i].FindControl("btnDarf");
                Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                Label lblspnoo = (Label)grdPO.Items[i].FindControl("lblspnoo");
                
                string STS = lblStatus.Text;
                if (STS == "DSO")
                {
                    lblspnoo.Visible = false;
                    btnDarf.Visible = true;
                    lnkbtnPQ.Visible = false;
                    lnkbtndelete.Visible = false;
                }
            }
        }
        protected void lnkbtndelete1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        { using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
            int TRCtionNo = Convert.ToInt32(txtTraNoHD.Text);
            string Ref = txtrefreshno.Text .ToString();

            for (int i = 0; i < gvPurQuat2.Items.Count; i++)
            {
                Label lblproductid = (Label)gvPurQuat2.Items[i].FindControl("lblproductid");
                ListView listmulticoler = (ListView)gvPurQuat2.Items[i].FindControl("listmulticoler");
                ListView listSerial = (ListView)gvPurQuat2.Items[i].FindControl("listSerial");
                ListView listSize = (ListView)gvPurQuat2.Items[i].FindControl("listSize");
                ListView listMUOMLIST = (ListView)gvPurQuat2.Items[i].FindControl("listMUOMLIST");
                ListView listBin = (ListView)gvPurQuat2.Items[i].FindControl("listBin");
                int PRODUCTID = Convert.ToInt32(lblproductid.Text);
                if (listmulticoler.Items.Count() > 0)
                {
                    for (int j = 0; j < listmulticoler.Items.Count; j++)
                    {
                        Label LBLCOLERID = (Label)listmulticoler.Items[j].FindControl("LBLCOLERID");
                        Label LblMyid = (Label)listmulticoler.Items[j].FindControl("LblMyid");
                        int COLID = Convert.ToInt32(LBLCOLERID.Text);
                        int PID = Convert.ToInt32(LblMyid.Text);
                        int SID = Convert.ToInt32(999999998);
                        ICIT_BR_SIZECOLOR obj = DB.ICIT_BR_SIZECOLOR.Single(p => p.COLORID == COLID && p.MYTRANSID == TRCtionNo && p.MyProdID == PID && p.SIZECODE == SID && p.TenentID == TID);
                        obj.Active = "Y";
                        DB.SaveChanges();
                    }
                }
                if (listSize.Items.Count() > 0)
                {
                    for (int j = 0; j < listSize.Items.Count; j++)
                    {
                        Label LBLcOLERID = (Label)listSize.Items[j].FindControl("LBLcOLERID");
                        Label LblMyid = (Label)listSize.Items[j].FindControl("LblMyid");
                        int SID = Convert.ToInt32(LBLcOLERID.Text);
                        int PID = Convert.ToInt32(LblMyid.Text);
                        int COLID = Convert.ToInt32(999999998);
                        ICIT_BR_SIZECOLOR obj = DB.ICIT_BR_SIZECOLOR.Single(p => p.COLORID == COLID && p.MYTRANSID == TRCtionNo && p.MyProdID == PID && p.SIZECODE == SID && p.TenentID == TID);
                        obj.Active = "Y";
                        DB.SaveChanges();
                    }
                }
                if (listSerial.Items.Count() > 0)
                {
                    for (int j = 0; j < listSerial.Items.Count; j++)
                    {
                        Label LblMyid = (Label)listSerial.Items[j].FindControl("LblMyid");
                        TextBox txtlistSerial = (TextBox)listSerial.Items[j].FindControl("txtlistSerial");
                        int PID = Convert.ToInt32(LblMyid.Text);
                        string SIRNomber = txtlistSerial.Text;
                        if (DB.ICIT_BR_Serialize.Where(p => p.MyTransID == TRCtionNo && p.MyProdID == PID && p.TenentID == TID && p.Active == "D" && p.Serial_Number == SIRNomber).Count() == 1)
                        {
                            ICIT_BR_Serialize obj = DB.ICIT_BR_Serialize.Single(p => p.MyTransID == TRCtionNo && p.MyProdID == PID && p.TenentID == TID && p.Active == "D" && p.Serial_Number == SIRNomber);
                            obj.Active = "Y";
                            DB.SaveChanges();
                        }
                    }
                }
                if (listMUOMLIST.Items.Count() > 0)
                {
                    for (int j = 0; j < listMUOMLIST.Items.Count; j++)
                    {
                        Label LBLCOLERID = (Label)listMUOMLIST.Items[j].FindControl("LBLCOLERID");
                        Label LblMyid = (Label)listMUOMLIST.Items[j].FindControl("LblMyid");
                        int UOMID = Convert.ToInt32(LBLCOLERID.Text);
                        int PID = Convert.ToInt32(LblMyid.Text);
                        ICIT_BR obj = DB.ICIT_BR.Single(p => p.MYTRANSID == TRCtionNo && p.MyProdID == PID && p.TenentID == TID && p.Active == "D" && p.UOM == UOMID);
                        obj.Active = "Y";
                        DB.SaveChanges();
                    }
                }
                if (listBin.Items.Count() > 0)
                {
                    for (int j = 0; j < listBin.Items.Count; j++)
                    {
                        Label lblbinid = (Label)listBin.Items[j].FindControl("lblbinid");
                        Label LblMyid = (Label)listBin.Items[j].FindControl("LblMyid");
                        int BinID = Convert.ToInt32(lblbinid.Text);
                        int PID = Convert.ToInt32(LblMyid.Text);
                        if (DB.ICIT_BR_BIN.Where(p => p.MYTRANSID == TRCtionNo && p.MyProdID == PID && p.TenentID == TID && p.Active == "D" && p.Bin_ID == BinID).Count() == 1)
                        {
                            ICIT_BR_BIN obj = DB.ICIT_BR_BIN.Single(p => p.MYTRANSID == TRCtionNo && p.MyProdID == PID && p.TenentID == TID && p.Active == "D" && p.Bin_ID == BinID);
                            obj.Active = "Y";
                            DB.SaveChanges();
                        }

                    }
                }
                var ListICIT_BR_Perishable = DB.ICIT_BR_Perishable.Where(p => p.MYTRANSID == TRCtionNo && p.MyProdID == PRODUCTID && p.Active == "D").ToList();
                if (ListICIT_BR_Perishable.Count() == 1)
                {
                    ICIT_BR_Perishable obj = ListICIT_BR_Perishable.Single(p => p.MYTRANSID == TRCtionNo && p.MyProdID == PRODUCTID && p.Active == "D");
                    obj.Active = "Y";
                    DB.SaveChanges();

                }

            }
            var result1 = (from pm in DB.ICTR_DT
                           join
                             ur in DB.TBLPRODUCTs on pm.MyProdID equals ur.MYPRODID
                          
                           where pm.ACTIVE == true && pm.MYTRANSID == TRCtionNo
                           select new { pm.MYTRANSID, pm.MyProdID, pm.QUANTITY, pm.MYID, ur.MultiBinStore, ur.MultiColor, ur.MultiSize, ur.Serialized, ur.Perishable, ur.MultiUOM, ur.UOM,}).ToList();
            var LIstMain = result1.GroupBy(p => p.MyProdID).Select(p => p.FirstOrDefault()).ToList();
            if (LIstMain.Count() > 0)
            {
                for (int i = 0; i < LIstMain.Count; i++)
                {
                    int PID = Convert.ToInt32(LIstMain[i].MyProdID);
                    int Qty = Convert.ToInt32(LIstMain[i].QUANTITY);
                    TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID);
                    int PQTY = Convert.ToInt32(obj.QTYINHAND);
                    int Total = PQTY - Qty;
                    obj.QTYINHAND = Total;
                    DB.SaveChanges();
                }
            }

            var listDXT = DB.ICTR_DTEXT.Where(p => p.MYTRANSID == TRCtionNo && p.ACTIVE == true).ToList();
            for (int i = 0; i < listDXT.Count; i++)
            {
                int myid = Convert.ToInt32(listDXT[i].MyID);
                ICTR_DTEXT obj = DB.ICTR_DTEXT.Single(p => p.MyID == myid && p.MYTRANSID == TRCtionNo);
                obj.TransNo = 1;
                DB.SaveChanges();
            }
            ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TRCtionNo);
            objICTR_HD.ACTIVE = true;
            objICTR_HD.Status = "RDSO";
            DB.SaveChanges();
            btnSubmit.Visible = false;
            btnRequestde.Visible = true;
            scope.Complete(); //  To commit.
            }
        }

        protected void lnkbtnPQ_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnMCSU_Click(object sender, EventArgs e)
        {

        }

        protected void lnbMCSUPo_Click(object sender, EventArgs e)
        {

        }

        protected void btnMBin_Click(object sender, EventArgs e)
        {

        }

        protected void lnbBIn_Click(object sender, EventArgs e)
        {

        }

        protected void btnPeriSub_Click(object sender, EventArgs e)
        {

        }

        protected void btnUOMConv_Click(object sender, EventArgs e)
        {

        }

        protected void btnPost_Click(object sender, EventArgs e)
        {

        }
        public string getproductName(int PID)
        {
            string ProductName = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID).ProdName1;
            string ProductCode = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID).UserProdID;
            return ProductCode + "  -  " + ProductName;
        }
        public string getitemscode(int PID)
        {
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID).UserProdID;
        }
        public string getconpniname(int PID)
        {
            string Locatin = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == PID).PHYSICALLOCID;
            return DB.TBLLOCATIONs.Single(p => p.PHYSICALLOCID == Locatin).LOCNAME1;
        }
        public string getSuplierName(int UID)
        {
            return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == UID).COMPNAME1;
        }
        protected void grdPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditIncom")
            {
                // string[] ID = e.CommandArgument.ToString().Split(',');
                int str1 = Convert.ToInt32(e.CommandArgument);
                // int str2 = Convert.ToInt32(ID[1]);
                BibdEditmode(str1);
                ViewState["DelverRecviest"] = str1;

            }
            if (e.CommandName == "DeleteIncom")
            {

            }
        }
        public void BibdEditmode(int str1)
        {
            ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == str1);
            ddlLocalForeign.SelectedItem.Text = objICTR_HD.LF == "L" ? "Local" : "Foreign";
            txtOrderDate.Text = objICTR_HD.TRANSDATE.ToString("dd/MM/yyyy");
            ddlSupplier.SelectedValue = objICTR_HD.CUSTVENDID.ToString();
            ddlCurrency.SelectedValue = objICTR_HD.COMPANYID.ToString();
            txtBatchNo.Text = objICTR_HD.USERBATCHNO.ToString();
            ddlProjectNo.Text = objICTR_HD.PROJECTNO.ToString();
            txtrefreshno.Text = objICTR_HD.REFERENCE.ToString();
            txtTraNoHD.Text = objICTR_HD.MYTRANSID.ToString();
            ddlCrmAct.SelectedValue = objICTR_HD.ACTIVITYCODE.ToString();
            txtNoteHD.Text = objICTR_HD.NOTES.ToString();
            if (DB.ICOVERHEADCOSTs.Where(p => p.MYTRANSID == str1).Count() == 1)
            {
                ICOVERHEADCOST obj = DB.ICOVERHEADCOSTs.Single(p => p.MYTRANSID == str1);
                txtOHCostHD.Text = obj.TOTCOST.ToString();
            }
            else
                txtOHCostHD.Text = "0.00";
            var result1 = (from pm in DB.ICTR_DT
                           join
                             ur in DB.TBLPRODUCTs on pm.MyProdID equals ur.MYPRODID
                           where pm.ACTIVE == true && pm.MYTRANSID == str1
                           select new { pm.MYTRANSID, pm.MyProdID, pm.QUANTITY, pm.MYID, pm.COMPANYID, pm.AMOUNT, ur.MultiBinStore, ur.MultiColor, ur.MultiSize, ur.Serialized, ur.Perishable, ur.MultiUOM, ur.UOM }).ToList();
            var List = result1.GroupBy(p => p.MyProdID).Select(p => p.FirstOrDefault()).ToList();
            //listresivDelvry.DataSource = List;
            //listresivDelvry.DataBind();
            ViewState["MYTRANSIDNoum"] = str1;
            gvPurQuat2.DataSource = List;
            gvPurQuat2.DataBind();
            pnlCreateForm.Style.Add("display", "block");
            pnlGridView.Style.Add("display", "none");
            Button3.Visible = true;
            btnSubmit.Visible = true;
            btnRequestde.Visible = false;
        }

        List<ICIT_BR_BIN> ListICIT_BR_BIN = new List<ICIT_BR_BIN>();
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (ViewState["ListICIT_BR_BIN"] != null)
            {
                ListICIT_BR_BIN = (List<ICIT_BR_BIN>)ViewState["ListICIT_BR_BIN"];
                ICIT_BR_BIN obj = new ICIT_BR_BIN();
                ListICIT_BR_BIN.Add(obj);
                //  listBin.DataSource = ListICIT_BR_BIN;
            }
            else
            {
                ICIT_BR_BIN obj = new ICIT_BR_BIN();
                ListICIT_BR_BIN.Add(obj);
                //   listBin.DataSource = ListICIT_BR_BIN;
            }
            ViewState["ListICIT_BR_BIN"] = ListICIT_BR_BIN;
            //  listBin.DataBind();



        }
        protected void linkMulticoler_Click(object sender, EventArgs e)
        {
            ICIT_BR_SIZECOLOR obj = new ICIT_BR_SIZECOLOR();
            //foreach (ListViewDataItem item in listmulticoler.Items)
            //{
            //    Label lblID = (Label)item.FindControl("lblID");
            //    TextBox txtcoloerqty = (TextBox)item.FindControl("txtcoloerqty");
            //    Label LblMyid = (Label)item.FindControl("LblMyid");
            //    obj.COLORID = Convert.ToInt32(lblID.Text);
            //    obj.OnHand = Convert.ToInt32(txtcoloerqty.Text);
            //    obj.MyProdID = Convert.ToInt32(LblMyid.Text);
            //    ListICIT_BR_SIZECOLOR.Add(obj);

            //}
            ViewState["ICIT_BR_SIZECOLOR"] = ListICIT_BR_SIZECOLOR;

        }
        protected void gvPurQuat2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void gvPurQuat2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblproductid = (Label)e.Item.FindControl("lblproductid");
            Label labeltarjication = (Label)e.Item.FindControl("labeltarjication");
            TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
            ListView listmulticoler = (ListView)e.Item.FindControl("listmulticoler");
            ListView listBin = (ListView)e.Item.FindControl("listBin");
            ListView listMUOMLIST = (ListView)e.Item.FindControl("listMUOMLIST");
            ListView listSize = (ListView)e.Item.FindControl("listSize");
            ListView listSerial = (ListView)e.Item.FindControl("listSerial");
            int ProduID = Convert.ToInt32(lblproductid.Text);
            int TCID = Convert.ToInt32(labeltarjication.Text);
            List<TBLPRODUCT> list = DB.TBLPRODUCTs.Where(p => p.MYPRODID == ProduID && p.ACTIVE == "1").ToList();
            Boolean MultiColor = Convert.ToBoolean(list.Single().MultiColor);
            Boolean MultiUOM = Convert.ToBoolean(list.Single().MultiUOM);
            Boolean Perishable = Convert.ToBoolean(list.Single().Perishable);
            Boolean MultiSize = Convert.ToBoolean(list.Single().MultiSize);
            Boolean Serialized = Convert.ToBoolean(list.Single().Serialized);
            Boolean MultiBinStore = Convert.ToBoolean(list.Single().MultiBinStore);
            if (MultiColor == Convert.ToBoolean(1))
            {
                List<ICIT_BR_SIZECOLOR> List = DB.ICIT_BR_SIZECOLOR.Where(p => p.MYTRANSID == TCID && p.MyProdID == ProduID && (p.Active == "D" || p.Active == "Y") && p.COLORID != 999999998).ToList();
                listmulticoler.DataSource = List;
                listmulticoler.DataBind();
                ViewState["MultiColor"] = List;
            }
            if (MultiUOM == Convert.ToBoolean(1))
            {

                List<ICIT_BR> List3 = DB.ICIT_BR.Where(p => p.MYTRANSID == TCID && p.MyProdID == ProduID && (p.Active == "D" || p.Active == "Y")).ToList();

                listMUOMLIST.DataSource = List3;
                listMUOMLIST.DataBind();
                ViewState["MultiUOM"] = List3;
            }
            if (MultiSize == Convert.ToBoolean(1))
            {
                List<ICIT_BR_SIZECOLOR> List2 = DB.ICIT_BR_SIZECOLOR.Where(p => p.MYTRANSID == TCID && p.MyProdID == ProduID && (p.Active == "D" || p.Active == "Y") && p.SIZECODE != 999999998).ToList();
                listSize.DataSource = List2;
                listSize.DataBind();
                ViewState["MultiSize"] = List2;

            }
            if (Serialized == Convert.ToBoolean(1))
            {
                var ListSeril = DB.ICIT_BR_Serialize.Where(p => p.MyTransID == TCID && (p.Active == "D" || p.Active == "Y") && p.MyProdID == ProduID).ToList();
                listSerial.DataSource = ListSeril;
                listSerial.DataBind();
                ViewState["Serialized"] = ListSeril;
            }
            if (MultiBinStore == Convert.ToBoolean(1))
            {
                List<ICIT_BR_BIN> List = DB.ICIT_BR_BIN.Where(p => p.MyProdID == ProduID && p.MYTRANSID == TCID && (p.Active == "D" || p.Active == "Y")).ToList();
                listBin.DataSource = List;
                listBin.DataBind();
                ViewState["BindBin"] = List;
            }
            if (Perishable == Convert.ToBoolean(1))
            {
                TextBox txtPerBatchNo = (TextBox)e.Item.FindControl("txtPerBatchNo");
                TextBox txtProDate = (TextBox)e.Item.FindControl("txtProDate");
                TextBox txtExpDate = (TextBox)e.Item.FindControl("txtExpDate");
                TextBox txtLead2Dest = (TextBox)e.Item.FindControl("txtLead2Dest");
                ICIT_BR_Perishable obj = DB.ICIT_BR_Perishable.Single(p => p.MYTRANSID == TCID && p.MyProdID == ProduID && (p.Active == "D" || p.Active == "Y"));
                txtPerBatchNo.Text = obj.BatchNo;
                txtProDate.Text = obj.ProdDate.ToString();
                txtExpDate.Text = obj.ExpiryDate.ToString();
                txtLead2Dest.Text = obj.LeadDays2Destroy.ToString();

            }
        }
        public string getcolerName(int UID)
        {
            return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == UID).RecValue;
        }
        public int getSizeconti(int UID)
        {
            return Convert.ToInt32(DB.ICIT_BR_SIZECOLOR.Single(p => p.SIZECODE == UID).OpQty);
        }
        public string GetUom(int UID)
        {
            return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == UID).RecValue;
        }

        public string getUOMTable(int UID)
        {
            return DB.ICUOMs.Single(p => p.UOM == UID).UOMNAME1;
        }
        public string getbinName(int UID)
        {
            return DB.TBLBINs.Single(p => p.BIN_ID == UID).BINDesc1;
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            pnlCreateForm.Style.Add("display", "none");
            pnlGridView.Style.Add("display", "block");
            BindData();
            btnRequestde.Visible = false;
            btnSubmit.Visible = false;
            Button3.Visible = false;
        }

        protected void btnRequestde_Click(object sender, EventArgs e)
        {
            int Str1 = Convert.ToInt32(ViewState["DelverRecviest"]);
            Response.Redirect("GoofsTransferNotes.aspx?TactionNo=" + Str1);
        }

        protected void btnmultiuom_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < gvPurQuat2.Items.Count; i++)
            {
                Label labeltarjication = (Label)gvPurQuat2.Items[i].FindControl("labeltarjication");
                TextBox txtQty = (TextBox)gvPurQuat2.Items[i].FindControl("txtQty");

                int TCNID = Convert.ToInt32(labeltarjication.Text);
                int QTY = Convert.ToInt32(txtQty.Text);
                ListView listMUOMLIST = (ListView)gvPurQuat2.Items[i].FindControl("listMUOMLIST");
                int Total = 0;
                for (int j = 0; j < listMUOMLIST.Items.Count; j++)
                {
                    string text = ((TextBox)listMUOMLIST.Items[j].FindControl("Label38")).Text;
                    TextBox Label38 = listMUOMLIST.Items[j].FindControl("Label38") as TextBox;
                    //TextBox Label38 = (TextBox)listMUOMLIST.Items[j].FindControl("Label38");
                    int TXT = Convert.ToInt32(Label38.Text);
                    Total = Total + TXT;
                }
                if (QTY == Total)
                {
                    for (int j = 0; j < listMUOMLIST.Items.Count(); j++)
                    {

                        Label LBLCOLERID = (Label)listMUOMLIST.Items[j].FindControl("LBLCOLERID");
                        Label LblMyid = (Label)listMUOMLIST.Items[j].FindControl("LblMyid");
                        //TextBox txtuomQty = (TextBox)listMUOMLIST.Items[i].FindControl("txtuomQty");
                        TextBox Label38 = (TextBox)listMUOMLIST.Items[j].FindControl("Label38");
                        string Reference = "";
                        int TenentID = TID;
                        int MyProdID = Convert.ToInt32(LblMyid.Text);
                        string period_code = objProFile.PERIOD_CODE;
                        string MySysName = "IC";
                        int UOM = Convert.ToInt32(LBLCOLERID.Text);
                        string Bin_Per = "N";
                        int MYTRANSID = TCNID;
                        decimal UnitCost = 0;
                        int OnHand = 0;
                        int QtyOut = 0;
                        int QtyConsumed = 0;
                        int QtyReserved = 0;
                        int MinQty = 0;
                        int MaxQty = 0;
                        int LeadTime = 0;
                        int CRUP_ID = 0;
                        int OpQty = Convert.ToInt32(Label38.Text);

                        Reference = txtrefreshno.Text;
                        string Active = "D";
                        string Fuctions = "EDIT";
                        //Classes.EcommAdminClass.insertICIT_BR(TenentID, MyProdID, period_code, MySysName, UOM,LID, UnitCost, MYTRANSID, Bin_Per, OpQty, OnHand, QtyOut, QtyConsumed, QtyReserved, MinQty, MaxQty, LeadTime, Reference, Active, CRUP_ID, Fuctions);
                    }
                }
                else
                {
                    pnlSuccessMsg123.Visible = true;
                    lblMsg123.Text = "Multi UOM Quantity Are Not Same";
                }
            }

        }
    }
}