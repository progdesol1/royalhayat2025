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
    public partial class tblSetupInventory : System.Web.UI.Page
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
                // FirstData();
                btnAdd.ValidationGroup = "ss";
                Clear();
               
            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.tblSetupInventory> List = DB.tblSetupInventories.Where(m => m.TenentID == TID ).OrderBy(m => m.transidOut).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

             lblTransferOutTransType1s.Attributes["class"] = lblTransferOutTransSubType1s.Attributes["class"] = lblTransferInTransType1s.Attributes["class"] = lblTransferInTransSubType1s.Attributes["class"] = lblConsumeTransType1s.Attributes["class"] = lblConsumeTransSubType1s.Attributes["class"] = lbltransidin1s.Attributes["class"] = lbltranssubidIn1s.Attributes["class"] = lbltransidConsume1s.Attributes["class"] = lbltranssubidConsume1s.Attributes["class"] = lblMYSYSNAMEOut1s.Attributes["class"] = lblMYSYSNAMEIn1s.Attributes["class"] = lblStockTeking1s.Attributes["class"] = lblStockTakingPeriodBegin1s.Attributes["class"] = lblStockTakingPeriodEnd1s.Attributes["class"] = lblStockTakingTransTypeIn1s.Attributes["class"] = lblStockTakingTransTypeOut1s.Attributes["class"] = lblStockTakingTransSubTypeIn1s.Attributes["class"] = lblStockTakingTransSubTypeOut1s.Attributes["class"] = lblStockTakingtransidInLast1s.Attributes["class"] = lblStockTakingtransidOutLast1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  getshow";
           lblTransferOutTransType2h.Attributes["class"] = lblTransferOutTransSubType2h.Attributes["class"] = lblTransferInTransType2h.Attributes["class"] = lblTransferInTransSubType2h.Attributes["class"] = lblConsumeTransType2h.Attributes["class"] = lblConsumeTransSubType2h.Attributes["class"] = lbltransidin2h.Attributes["class"] = lbltranssubidIn2h.Attributes["class"] = lbltransidConsume2h.Attributes["class"] = lbltranssubidConsume2h.Attributes["class"] = lblMYSYSNAMEOut2h.Attributes["class"] = lblMYSYSNAMEIn2h.Attributes["class"] = lblStockTeking2h.Attributes["class"] = lblStockTakingPeriodBegin2h.Attributes["class"] = lblStockTakingPeriodEnd2h.Attributes["class"] = lblStockTakingTransTypeIn2h.Attributes["class"] = lblStockTakingTransTypeOut2h.Attributes["class"] = lblStockTakingTransSubTypeIn2h.Attributes["class"] = lblStockTakingTransSubTypeOut2h.Attributes["class"] = lblStockTakingtransidInLast2h.Attributes["class"] = lblStockTakingtransidOutLast2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblTransferOutTransType1s.Attributes["class"] = lblTransferOutTransSubType1s.Attributes["class"] = lblTransferInTransType1s.Attributes["class"] = lblTransferInTransSubType1s.Attributes["class"] = lblConsumeTransType1s.Attributes["class"] = lblConsumeTransSubType1s.Attributes["class"] = lbltransidin1s.Attributes["class"] = lbltranssubidIn1s.Attributes["class"] = lbltransidConsume1s.Attributes["class"] = lbltranssubidConsume1s.Attributes["class"] = lblMYSYSNAMEOut1s.Attributes["class"] = lblMYSYSNAMEIn1s.Attributes["class"] = lblStockTeking1s.Attributes["class"] = lblStockTakingPeriodBegin1s.Attributes["class"] = lblStockTakingPeriodEnd1s.Attributes["class"] = lblStockTakingTransTypeIn1s.Attributes["class"] = lblStockTakingTransTypeOut1s.Attributes["class"] = lblStockTakingTransSubTypeIn1s.Attributes["class"] = lblStockTakingTransSubTypeOut1s.Attributes["class"] = lblStockTakingtransidInLast1s.Attributes["class"] = lblStockTakingtransidOutLast1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  gethide";
             lblTransferOutTransType2h.Attributes["class"] = lblTransferOutTransSubType2h.Attributes["class"] = lblTransferInTransType2h.Attributes["class"] = lblTransferInTransSubType2h.Attributes["class"] = lblConsumeTransType2h.Attributes["class"] = lblConsumeTransSubType2h.Attributes["class"] = lbltransidin2h.Attributes["class"] = lbltranssubidIn2h.Attributes["class"] = lbltransidConsume2h.Attributes["class"] = lbltranssubidConsume2h.Attributes["class"] = lblMYSYSNAMEOut2h.Attributes["class"] = lblMYSYSNAMEIn2h.Attributes["class"] = lblStockTeking2h.Attributes["class"] = lblStockTakingPeriodBegin2h.Attributes["class"] = lblStockTakingPeriodEnd2h.Attributes["class"] = lblStockTakingTransTypeIn2h.Attributes["class"] = lblStockTakingTransTypeOut2h.Attributes["class"] = lblStockTakingTransSubTypeIn2h.Attributes["class"] = lblStockTakingTransSubTypeOut2h.Attributes["class"] = lblStockTakingtransidInLast2h.Attributes["class"] = lblStockTakingtransidOutLast2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //  txtTenentID.Text = "";
            //drplocation.SelectedIndex = 0;
            txttransferouttranstype.Text = "";
            txttransferoutsubtype.Text = "";
            txttransferintranstype.Text = "";
            txttransferintranssubtype.Text = "";
            txtconsumetranstype.Text = "";
            txtconsumetranssubtype.Text = "";
            //txttransidOut.Text = "";
            //txttranssubidOut.Text = "";
            txttransidin.Text = "";
            txttranssubidIn.Text = "";
            txttransidConsume.Text = "";
            txttranssubidConsume.Text = "";
            txtMYSYSNAMEOut.Text = "";
            txtMYSYSNAMEIn.Text = "";
            chkteking.Checked = false;
            chkstock.Checked = false;
            chkend.Checked = false;
            txtStockTakingTransTypeIn.Text = "";
            txtStockTakingTransTypeOut.Text = "";
            txtstocktranssubtypein.Text = "";
            txtstocktakingtranssubtypeout.Text = "";
            txtStockTakingtransidInLast.Text = "";
            txtStockTakingtransidOutLast.Text = "";
            //txtDefaultCUSTVENDID.Text = "";
            //txtCreated.Text = "";
            //txtDateTime.Text = "";
            cbActive.Checked = false;
            //cbDeleted.Checked = false;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                //if (btnAdd.Text == "AddNew")
                //{

                //    Write();
                //    Clear();
                //    btnAdd.Text = "Add";
                //    btnAdd.ValidationGroup = "Submit";
                //}
                //else if (btnAdd.Text == "Add")
                //{
                //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //    Database.tblSetupInventory objtblSetupInventory = new Database.tblSetupInventory();
                //    //Server Content Send data Yogesh
                //    int MAXID = DB.tblSetupInventories.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblSetupInventories.Where(p => p.TenentID == TID).Max(p => p.transidOut) + 1) : 1;
                //    objtblSetupInventory.TenentID = TID;
                //    //objtblSetupInventory.locationID = Convert.ToInt32(drplocation.SelectedValue);
                //    objtblSetupInventory.TransferOutTransType = txttransferouttranstype.Text;
                //    objtblSetupInventory.TransferOutTransSubType = txttransferoutsubtype.Text;
                //    objtblSetupInventory.TransferInTransType = txttransferintranstype.Text;
                //    objtblSetupInventory.TransferInTransSubType = txttransferintranssubtype.Text;
                //    objtblSetupInventory.ConsumeTransType = txtconsumetranstype.Text;
                //    objtblSetupInventory.ConsumeTransSubType = txtconsumetranssubtype.Text;
                //    objtblSetupInventory.transidOut = MAXID;
                //    objtblSetupInventory.transsubidOut = MAXID;
                //    objtblSetupInventory.transidin = Convert.ToInt32(txttransidin.Text);
                //    objtblSetupInventory.transsubidIn = Convert.ToInt32(txttranssubidIn.Text);
                // //   objtblSetupInventory.transidConsume = Convert.ToInt32(txttransidConsume);
                ////    objtblSetupInventory.transsubidConsume = Convert.ToInt32(txttranssubidConsume);
                //    //// objtblSetupInventory.transidin = Convert.ToInt32(txttransidin.Text);
                //    // objtblSetupInventory.transsubidIn = Convert.ToInt32(txttranssubidIn.Text);
                //    // objtblSetupInventory.transidConsume = Convert.ToInt32(txttransidConsume.Text);
                //    // objtblSetupInventory.transsubidConsume = Convert.ToInt32(txttranssubidConsume.Text);
                //    objtblSetupInventory.MYSYSNAMEOut = txtMYSYSNAMEOut.Text;
                //    objtblSetupInventory.MYSYSNAMEIn = txtMYSYSNAMEIn.Text;
                //    objtblSetupInventory.StockTeking = chkteking.Checked;
                //    objtblSetupInventory.StockTakingPeriodBegin = chkstock.Checked;
                //    objtblSetupInventory.StockTakingPeriodEnd = chkend.Checked;
                //    objtblSetupInventory.StockTakingTransTypeIn = txtStockTakingTransTypeIn.Text;
                //    objtblSetupInventory.StockTakingTransTypeOut = txtStockTakingTransTypeOut.Text;
                //    objtblSetupInventory.StockTakingTransSubTypeIn = txtstocktranssubtypein.Text;
                //    objtblSetupInventory.StockTakingTransSubTypeOut = txtstocktakingtranssubtypeout.Text;
                //    // objtblSetupInventory.StockTakingtransidInLast = Convert.ToInt32(txtStockTakingtransidInLast.Text);
                //    // objtblSetupInventory.StockTakingtransidOutLast = Convert.ToInt32(txtStockTakingtransidOutLast.Text);
                //    //  objtblSetupInventory.DefaultCUSTVENDID = Convert.ToInt32(txtDefaultCUSTVENDID.Text);
                //    //  objtblSetupInventory.Created = Convert.ToInt32(txtCreated.Text);
                //    //    objtblSetupInventory.DateTime = Convert.ToDateTime(txtDateTime.Text);
                //    objtblSetupInventory.Active = cbActive.Checked;
                //    //objtblSetupInventory.Deleted = cbDeleted.Checked;
                //    DB.tblSetupInventories.AddObject(objtblSetupInventory);
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
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        Database.tblSetupInventory objtblSetupInventory = DB.tblSetupInventories.Single(p => p.transidOut == ID && p.TenentID == TID);
                        objtblSetupInventory.TenentID = TID;
                        //objtblSetupInventory.locationID = Convert.ToInt32(drplocation.SelectedValue);
                        objtblSetupInventory.TransferOutTransType = txttransferouttranstype.Text;
                        objtblSetupInventory.TransferOutTransSubType = txttransferoutsubtype.Text;
                        objtblSetupInventory.TransferInTransType = txttransferintranstype.Text;
                        objtblSetupInventory.TransferInTransSubType = txttransferintranssubtype.Text;
                        objtblSetupInventory.ConsumeTransType = txtconsumetranstype.Text;
                        objtblSetupInventory.ConsumeTransSubType = txtconsumetranssubtype.Text;
                        objtblSetupInventory.transidOut = ID;
                        objtblSetupInventory.transsubidOut = ID;
                        objtblSetupInventory.transidin = Convert.ToInt32(txttransidin.Text);
                        objtblSetupInventory.transsubidIn = Convert.ToInt32(txttranssubidIn.Text);
                       // objtblSetupInventory.transidConsume = Convert.ToInt32(txttransidConsume);
                       // objtblSetupInventory.transsubidConsume = Convert.ToInt32(txttranssubidConsume);
                        objtblSetupInventory.MYSYSNAMEOut = txtMYSYSNAMEOut.Text;
                        objtblSetupInventory.MYSYSNAMEIn = txtMYSYSNAMEIn.Text;
                        objtblSetupInventory.StockTeking = chkteking.Checked;
                        objtblSetupInventory.StockTakingPeriodBegin = chkstock.Checked;
                        objtblSetupInventory.StockTakingPeriodEnd = chkend.Checked;
                        objtblSetupInventory.StockTakingTransTypeIn = txtStockTakingTransTypeIn.Text;
                        objtblSetupInventory.StockTakingTransTypeOut = txtStockTakingTransTypeOut.Text;
                        objtblSetupInventory.StockTakingTransSubTypeIn = txtstocktranssubtypein.Text;
                        objtblSetupInventory.StockTakingTransSubTypeOut = txtstocktakingtranssubtypeout.Text;
                        //  objtblSetupInventory.StockTakingtransidInLast = Convert.ToInt32(txtStockTakingtransidInLast.Text);
                        // objtblSetupInventory.StockTakingtransidOutLast = Convert.ToInt32(txtStockTakingtransidOutLast.Text);
                        // objtblSetupInventory.DefaultCUSTVENDID = Convert.ToInt32(txtDefaultCUSTVENDID.Text);
                        //   objtblSetupInventory.Created = Convert.ToInt32(txtCreated.Text);
                        // objtblSetupInventory.DateTime = Convert.ToDateTime(txtDateTime.Text);
                        objtblSetupInventory.Active = cbActive.Checked;
                        // objtblSetupInventory.Deleted = cbDeleted.Checked;

                        ViewState["Edit"] = null;
                       // btnAdd.Text = "Add New";
                       // btnAdd.Visible = false;
                        DB.SaveChanges();
                        Clear();
                        btnAdd.ValidationGroup = "ss";
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        // navigation.Visible = true;
                        Readonly();
                        //  FirstData();
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
            txttransferouttranstype.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferoutsubtype.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferintranstype.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferintranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
            txtconsumetranstype.Text = Listview1.SelectedDataKey[0].ToString();
            txtconsumetranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
            //txttransidOut.Text = Listview1.SelectedDataKey[0].ToString();
            //txttranssubidOut.Text = Listview1.SelectedDataKey[0].ToString();
            txttransidin.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubidIn.Text = Listview1.SelectedDataKey[0].ToString();
            txttransidConsume.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubidConsume.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYSYSNAMEOut.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYSYSNAMEIn.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStockTeking.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStockTakingPeriodBegin.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStockTakingPeriodEnd.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
            txtstocktranssubtypein.Text = Listview1.SelectedDataKey[0].ToString();
            txtstocktakingtranssubtypeout.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingtransidInLast.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCreated.Text = Listview1.SelectedDataKey[0].ToString();
            //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            cbActive.Checked = false;
            //cbDeleted.Checked = false;

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferouttranstype.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferoutsubtype.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferintranstype.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferintranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
                txtconsumetranstype.Text = Listview1.SelectedDataKey[0].ToString();
                txtconsumetranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
                //txttransidOut.Text = Listview1.SelectedDataKey[0].ToString();
                //txttranssubidOut.Text = Listview1.SelectedDataKey[0].ToString();
                txttransidin.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubidIn.Text = Listview1.SelectedDataKey[0].ToString();
                txttransidConsume.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubidConsume.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYSYSNAMEOut.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYSYSNAMEIn.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStockTeking.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStockTakingPeriodBegin.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStockTakingPeriodEnd.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
                txtstocktranssubtypein.Text = Listview1.SelectedDataKey[0].ToString();
                txtstocktakingtranssubtypeout.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingtransidInLast.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCreated.Text = Listview1.SelectedDataKey[0].ToString();
                //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                cbActive.Checked = false;
                //cbDeleted.Checked = false;
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
                pnlSuccessMsg.Visible = false;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferouttranstype.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferoutsubtype.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferintranstype.Text = Listview1.SelectedDataKey[0].ToString();
                txttransferintranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
                txtconsumetranstype.Text = Listview1.SelectedDataKey[0].ToString();
                txtconsumetranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
                //txttransidOut.Text = Listview1.SelectedDataKey[0].ToString();
                //txttranssubidOut.Text = Listview1.SelectedDataKey[0].ToString();
                txttransidin.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubidIn.Text = Listview1.SelectedDataKey[0].ToString();
                txttransidConsume.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubidConsume.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYSYSNAMEOut.Text = Listview1.SelectedDataKey[0].ToString();
                txtMYSYSNAMEIn.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStockTeking.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStockTakingPeriodBegin.Text = Listview1.SelectedDataKey[0].ToString();
                //txtStockTakingPeriodEnd.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
                txtstocktranssubtypein.Text = Listview1.SelectedDataKey[0].ToString();
                txtstocktakingtranssubtypeout.Text = Listview1.SelectedDataKey[0].ToString();
                txtStockTakingtransidInLast.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCreated.Text = Listview1.SelectedDataKey[0].ToString();
                //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                cbActive.Checked = false;
                //cbDeleted.Checked = false;

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //drplocation.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferouttranstype.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferoutsubtype.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferintranstype.Text = Listview1.SelectedDataKey[0].ToString();
            txttransferintranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
            txtconsumetranstype.Text = Listview1.SelectedDataKey[0].ToString();
            txtconsumetranssubtype.Text = Listview1.SelectedDataKey[0].ToString();
            //txttransidOut.Text = Listview1.SelectedDataKey[0].ToString();
            //txttranssubidOut.Text = Listview1.SelectedDataKey[0].ToString();
            txttransidin.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubidIn.Text = Listview1.SelectedDataKey[0].ToString();
            txttransidConsume.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubidConsume.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYSYSNAMEOut.Text = Listview1.SelectedDataKey[0].ToString();
            txtMYSYSNAMEIn.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStockTeking.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStockTakingPeriodBegin.Text = Listview1.SelectedDataKey[0].ToString();
            //txtStockTakingPeriodEnd.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeIn.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingTransTypeOut.Text = Listview1.SelectedDataKey[0].ToString();
            txtstocktranssubtypein.Text = Listview1.SelectedDataKey[0].ToString();
            txtstocktakingtranssubtypeout.Text = Listview1.SelectedDataKey[0].ToString();
            txtStockTakingtransidInLast.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCreated.Text = Listview1.SelectedDataKey[0].ToString();
            //txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            cbActive.Checked = false;
            //cbDeleted.Checked = false;

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                     lblTransferOutTransType2h.Visible = lblTransferOutTransSubType2h.Visible = lblTransferInTransType2h.Visible = lblTransferInTransSubType2h.Visible = lblConsumeTransType2h.Visible = lblConsumeTransSubType2h.Visible = lbltransidin2h.Visible = lbltranssubidIn2h.Visible = lbltransidConsume2h.Visible = lbltranssubidConsume2h.Visible = lblMYSYSNAMEOut2h.Visible = lblMYSYSNAMEIn2h.Visible = lblStockTeking2h.Visible = lblStockTakingPeriodBegin2h.Visible = lblStockTakingPeriodEnd2h.Visible = lblStockTakingTransTypeIn2h.Visible = lblStockTakingTransTypeOut2h.Visible = lblStockTakingTransSubTypeIn2h.Visible = lblStockTakingTransSubTypeOut2h.Visible = lblStockTakingtransidInLast2h.Visible = lblStockTakingtransidOutLast2h.Visible = lblActive2h.Visible = false;
                    //2true
                     txtTransferOutTransType2h.Visible = txtTransferOutTransSubType2h.Visible = txtTransferInTransType2h.Visible = txtTransferInTransSubType2h.Visible = txtConsumeTransType2h.Visible = txtConsumeTransSubType2h.Visible = txttransidin2h.Visible = txttranssubidIn2h.Visible = txttransidConsume2h.Visible = txttranssubidConsume2h.Visible = txtMYSYSNAMEOut2h.Visible = txtMYSYSNAMEIn2h.Visible = txtStockTeking2h.Visible = txtStockTakingPeriodBegin2h.Visible = txtStockTakingPeriodEnd2h.Visible = txtStockTakingTransTypeIn2h.Visible = txtStockTakingTransTypeOut2h.Visible = txtStockTakingTransSubTypeIn2h.Visible = txtStockTakingTransSubTypeOut2h.Visible = txtStockTakingtransidInLast2h.Visible = txtStockTakingtransidOutLast2h.Visible = txtActive2h.Visible = true;

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
                   lblTransferOutTransType2h.Visible = lblTransferOutTransSubType2h.Visible = lblTransferInTransType2h.Visible = lblTransferInTransSubType2h.Visible = lblConsumeTransType2h.Visible = lblConsumeTransSubType2h.Visible = lbltransidin2h.Visible = lbltranssubidIn2h.Visible = lbltransidConsume2h.Visible = lbltranssubidConsume2h.Visible = lblMYSYSNAMEOut2h.Visible = lblMYSYSNAMEIn2h.Visible = lblStockTeking2h.Visible = lblStockTakingPeriodBegin2h.Visible = lblStockTakingPeriodEnd2h.Visible = lblStockTakingTransTypeIn2h.Visible = lblStockTakingTransTypeOut2h.Visible = lblStockTakingTransSubTypeIn2h.Visible = lblStockTakingTransSubTypeOut2h.Visible = lblStockTakingtransidInLast2h.Visible = lblStockTakingtransidOutLast2h.Visible = lblActive2h.Visible = true;
                    //2false
                   txtTransferOutTransType2h.Visible = txtTransferOutTransSubType2h.Visible = txtTransferInTransType2h.Visible = txtTransferInTransSubType2h.Visible = txtConsumeTransType2h.Visible = txtConsumeTransSubType2h.Visible = txttransidin2h.Visible = txttranssubidIn2h.Visible = txttransidConsume2h.Visible = txttranssubidConsume2h.Visible = txtMYSYSNAMEOut2h.Visible = txtMYSYSNAMEIn2h.Visible = txtStockTeking2h.Visible = txtStockTakingPeriodBegin2h.Visible = txtStockTakingPeriodEnd2h.Visible = txtStockTakingTransTypeIn2h.Visible = txtStockTakingTransTypeOut2h.Visible = txtStockTakingTransSubTypeIn2h.Visible = txtStockTakingTransSubTypeOut2h.Visible = txtStockTakingtransidInLast2h.Visible = txtStockTakingtransidOutLast2h.Visible = txtActive2h.Visible = false;

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
                    lblTransferOutTransType1s.Visible = lblTransferOutTransSubType1s.Visible = lblTransferInTransType1s.Visible = lblTransferInTransSubType1s.Visible = lblConsumeTransType1s.Visible = lblConsumeTransSubType1s.Visible = lbltransidin1s.Visible = lbltranssubidIn1s.Visible = lbltransidConsume1s.Visible = lbltranssubidConsume1s.Visible = lblMYSYSNAMEOut1s.Visible = lblMYSYSNAMEIn1s.Visible = lblStockTeking1s.Visible = lblStockTakingPeriodBegin1s.Visible = lblStockTakingPeriodEnd1s.Visible = lblStockTakingTransTypeIn1s.Visible = lblStockTakingTransTypeOut1s.Visible = lblStockTakingTransSubTypeIn1s.Visible = lblStockTakingTransSubTypeOut1s.Visible = lblStockTakingtransidInLast1s.Visible = lblStockTakingtransidOutLast1s.Visible = lblActive1s.Visible = false;
                    //1true
                     txtTransferOutTransType1s.Visible = txtTransferOutTransSubType1s.Visible = txtTransferInTransType1s.Visible = txtTransferInTransSubType1s.Visible = txtConsumeTransType1s.Visible = txtConsumeTransSubType1s.Visible = txttransidin1s.Visible = txttranssubidIn1s.Visible = txttransidConsume1s.Visible = txttranssubidConsume1s.Visible = txtMYSYSNAMEOut1s.Visible = txtMYSYSNAMEIn1s.Visible = txtStockTeking1s.Visible = txtStockTakingPeriodBegin1s.Visible = txtStockTakingPeriodEnd1s.Visible = txtStockTakingTransTypeIn1s.Visible = txtStockTakingTransTypeOut1s.Visible = txtStockTakingTransSubTypeIn1s.Visible = txtStockTakingTransSubTypeOut1s.Visible = txtStockTakingtransidInLast1s.Visible = txtStockTakingtransidOutLast1s.Visible = txtActive1s.Visible = true;
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
                   lblTransferOutTransType1s.Visible = lblTransferOutTransSubType1s.Visible = lblTransferInTransType1s.Visible = lblTransferInTransSubType1s.Visible = lblConsumeTransType1s.Visible = lblConsumeTransSubType1s.Visible = lbltransidin1s.Visible = lbltranssubidIn1s.Visible = lbltransidConsume1s.Visible = lbltranssubidConsume1s.Visible = lblMYSYSNAMEOut1s.Visible = lblMYSYSNAMEIn1s.Visible = lblStockTeking1s.Visible = lblStockTakingPeriodBegin1s.Visible = lblStockTakingPeriodEnd1s.Visible = lblStockTakingTransTypeIn1s.Visible = lblStockTakingTransTypeOut1s.Visible = lblStockTakingTransSubTypeIn1s.Visible = lblStockTakingTransSubTypeOut1s.Visible = lblStockTakingtransidInLast1s.Visible = lblStockTakingtransidOutLast1s.Visible = lblActive1s.Visible = true;
                    //1false
                   txtTransferOutTransType1s.Visible = txtTransferOutTransSubType1s.Visible = txtTransferInTransType1s.Visible = txtTransferInTransSubType1s.Visible = txtConsumeTransType1s.Visible = txtConsumeTransSubType1s.Visible = txttransidin1s.Visible = txttranssubidIn1s.Visible = txttransidConsume1s.Visible = txttranssubidConsume1s.Visible = txtMYSYSNAMEOut1s.Visible = txtMYSYSNAMEIn1s.Visible = txtStockTeking1s.Visible = txtStockTakingPeriodBegin1s.Visible = txtStockTakingPeriodEnd1s.Visible = txtStockTakingTransTypeIn1s.Visible = txtStockTakingTransTypeOut1s.Visible = txtStockTakingTransSubTypeIn1s.Visible = txtStockTakingTransSubTypeOut1s.Visible = txtStockTakingtransidInLast1s.Visible = txtStockTakingtransidOutLast1s.Visible = txtActive1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblSetupInventory").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenentID1s.ID == item.LabelID)
                //    txtTenentID1s.Text = lblTenentID1s.Text = lblTenentID1s.Text = item.LabelName;
                //if (lbllocationID1s.ID == item.LabelID)
                //    txtlocationID1s.Text = lbllocationID1s.Text = lbllocationID1s.Text = item.LabelName;
                 if (lblTransferOutTransType1s.ID == item.LabelID)
                    txtTransferOutTransType1s.Text = lblTransferOutTransType1s.Text = lblhTransferOutTransType.Text = item.LabelName;
                else if (lblTransferOutTransSubType1s.ID == item.LabelID)
                    txtTransferOutTransSubType1s.Text = lblTransferOutTransSubType1s.Text = lblhTransferOutTransSubType.Text = item.LabelName;
                else if (lblTransferInTransType1s.ID == item.LabelID)
                    txtTransferInTransType1s.Text = lblTransferInTransType1s.Text = lblTransferInTransType1s.Text = item.LabelName;
                else if (lblTransferInTransSubType1s.ID == item.LabelID)
                    txtTransferInTransSubType1s.Text = lblTransferInTransSubType1s.Text = lblTransferInTransSubType1s.Text = item.LabelName;
                else if (lblConsumeTransType1s.ID == item.LabelID)
                    txtConsumeTransType1s.Text = lblConsumeTransType1s.Text = lblConsumeTransType1s.Text = item.LabelName;
                else if (lblConsumeTransSubType1s.ID == item.LabelID)
                    txtConsumeTransSubType1s.Text = lblConsumeTransSubType1s.Text = lblConsumeTransSubType1s.Text = item.LabelName;
                //else if (lbltransidOut1s.ID == item.LabelID)
                //    txttransidOut1s.Text = lbltransidOut1s.Text = lbltransidOut1s.Text = item.LabelName;
                //else if (lbltranssubidOut1s.ID == item.LabelID)
                //    txttranssubidOut1s.Text = lbltranssubidOut1s.Text = lbltranssubidOut1s.Text = item.LabelName;
                else if (lbltransidin1s.ID == item.LabelID)
                    txttransidin1s.Text = lbltransidin1s.Text = lbltransidin1s.Text = item.LabelName;
                else if (lbltranssubidIn1s.ID == item.LabelID)
                    txttranssubidIn1s.Text = lbltranssubidIn1s.Text = lbltranssubidIn1s.Text = item.LabelName;
                else if (lbltransidConsume1s.ID == item.LabelID)
                    txttransidConsume1s.Text = lbltransidConsume1s.Text = lbltransidConsume1s.Text = item.LabelName;
                else if (lbltranssubidConsume1s.ID == item.LabelID)
                    txttranssubidConsume1s.Text = lbltranssubidConsume1s.Text = lbltranssubidConsume1s.Text = item.LabelName;
                else if (lblMYSYSNAMEOut1s.ID == item.LabelID)
                    txtMYSYSNAMEOut1s.Text = lblMYSYSNAMEOut1s.Text = lblhMYSYSNAMEOut.Text = item.LabelName;
                else if (lblMYSYSNAMEIn1s.ID == item.LabelID)
                    txtMYSYSNAMEIn1s.Text = lblMYSYSNAMEIn1s.Text = lblhMYSYSNAMEIn.Text = item.LabelName;
                else if (lblStockTeking1s.ID == item.LabelID)
                    txtStockTeking1s.Text = lblStockTeking1s.Text = lblStockTeking1s.Text = item.LabelName;
                else if (lblStockTakingPeriodBegin1s.ID == item.LabelID)
                    txtStockTakingPeriodBegin1s.Text = lblStockTakingPeriodBegin1s.Text = lblStockTakingPeriodBegin1s.Text = item.LabelName;
                else if (lblStockTakingPeriodEnd1s.ID == item.LabelID)
                    txtStockTakingPeriodEnd1s.Text = lblStockTakingPeriodEnd1s.Text = lblStockTakingPeriodEnd1s.Text = item.LabelName;
                else if (lblStockTakingTransTypeIn1s.ID == item.LabelID)
                    txtStockTakingTransTypeIn1s.Text = lblStockTakingTransTypeIn1s.Text = lblStockTakingTransTypeIn1s.Text = item.LabelName;
                else if (lblStockTakingTransTypeOut1s.ID == item.LabelID)
                    txtStockTakingTransTypeOut1s.Text = lblStockTakingTransTypeOut1s.Text = lblStockTakingTransTypeOut1s.Text = item.LabelName;
                else if (lblStockTakingTransSubTypeIn1s.ID == item.LabelID)
                    txtStockTakingTransSubTypeIn1s.Text = lblStockTakingTransSubTypeIn1s.Text = lblStockTakingTransSubTypeIn1s.Text = item.LabelName;
                else if (lblStockTakingTransSubTypeOut1s.ID == item.LabelID)
                    txtStockTakingTransSubTypeOut1s.Text = lblStockTakingTransSubTypeOut1s.Text = lblStockTakingTransSubTypeOut1s.Text = item.LabelName;
                else if (lblStockTakingtransidInLast1s.ID == item.LabelID)
                    txtStockTakingtransidInLast1s.Text = lblStockTakingtransidInLast1s.Text = lblStockTakingtransidInLast1s.Text = item.LabelName;
                else if (lblStockTakingtransidOutLast1s.ID == item.LabelID)
                    txtStockTakingtransidOutLast1s.Text = lblStockTakingtransidOutLast1s.Text = lblStockTakingtransidOutLast1s.Text = item.LabelName;
                //else if (lblDefaultCUSTVENDID1s.ID == item.LabelID)
                //    txtDefaultCUSTVENDID1s.Text = lblDefaultCUSTVENDID1s.Text = lblDefaultCUSTVENDID1s.Text = item.LabelName;
                //else if (lblCreated1s.ID == item.LabelID)
                //    txtCreated1s.Text = lblCreated1s.Text = lblCreated1s.Text = item.LabelName;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    txtDateTime1s.Text = lblDateTime1s.Text = lblDateTime1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    txtDeleted1s.Text = lblDeleted1s.Text = lblDeleted1s.Text = item.LabelName;

                //else if (lblTenentID2h.ID == item.LabelID)
                //    txtTenentID2h.Text = lblTenentID2h.Text = lblTenentID2h.Text = item.LabelName;
                //else if (lbllocationID2h.ID == item.LabelID)
                //    txtlocationID2h.Text = lbllocationID2h.Text = lbllocationID2h.Text = item.LabelName;
                else if (lblTransferOutTransType2h.ID == item.LabelID)
                    txtTransferOutTransType2h.Text = lblTransferOutTransType2h.Text = lblhTransferOutTransType.Text = item.LabelName;
                else if (lblTransferOutTransSubType2h.ID == item.LabelID)
                    txtTransferOutTransSubType2h.Text = lblTransferOutTransSubType2h.Text = lblhTransferOutTransSubType.Text = item.LabelName;
                else if (lblTransferInTransType2h.ID == item.LabelID)
                    txtTransferInTransType2h.Text = lblTransferInTransType2h.Text = lblTransferInTransType2h.Text = item.LabelName;
                else if (lblTransferInTransSubType2h.ID == item.LabelID)
                    txtTransferInTransSubType2h.Text = lblTransferInTransSubType2h.Text = lblTransferInTransSubType2h.Text = item.LabelName;
                else if (lblConsumeTransType2h.ID == item.LabelID)
                    txtConsumeTransType2h.Text = lblConsumeTransType2h.Text = lblConsumeTransType2h.Text = item.LabelName;
                else if (lblConsumeTransSubType2h.ID == item.LabelID)
                    txtConsumeTransSubType2h.Text = lblConsumeTransSubType2h.Text = lblConsumeTransSubType2h.Text = item.LabelName;
                //else if (lbltransidOut2h.ID == item.LabelID)
                //    txttransidOut2h.Text = lbltransidOut2h.Text = lbltransidOut2h.Text = item.LabelName;
                //else if (lbltranssubidOut2h.ID == item.LabelID)
                //    txttranssubidOut2h.Text = lbltranssubidOut2h.Text = lbltranssubidOut2h.Text = item.LabelName;
                else if (lbltransidin2h.ID == item.LabelID)
                    txttransidin2h.Text = lbltransidin2h.Text = lbltransidin2h.Text = item.LabelName;
                else if (lbltranssubidIn2h.ID == item.LabelID)
                    txttranssubidIn2h.Text = lbltranssubidIn2h.Text = lbltranssubidIn2h.Text = item.LabelName;
                else if (lbltransidConsume2h.ID == item.LabelID)
                    txttransidConsume2h.Text = lbltransidConsume2h.Text = lbltransidConsume2h.Text = item.LabelName;
                else if (lbltranssubidConsume2h.ID == item.LabelID)
                    txttranssubidConsume2h.Text = lbltranssubidConsume2h.Text = lbltranssubidConsume2h.Text = item.LabelName;
                else if (lblMYSYSNAMEOut2h.ID == item.LabelID)
                    txtMYSYSNAMEOut2h.Text = lblMYSYSNAMEOut2h.Text = lblhMYSYSNAMEOut.Text = item.LabelName;
                else if (lblMYSYSNAMEIn2h.ID == item.LabelID)
                    txtMYSYSNAMEIn2h.Text = lblMYSYSNAMEIn2h.Text = lblhMYSYSNAMEIn.Text = item.LabelName;
                else if (lblStockTeking2h.ID == item.LabelID)
                    txtStockTeking2h.Text = lblStockTeking2h.Text = lblStockTeking2h.Text = item.LabelName;
                else if (lblStockTakingPeriodBegin2h.ID == item.LabelID)
                    txtStockTakingPeriodBegin2h.Text = lblStockTakingPeriodBegin2h.Text = lblStockTakingPeriodBegin2h.Text = item.LabelName;
                else if (lblStockTakingPeriodEnd2h.ID == item.LabelID)
                    txtStockTakingPeriodEnd2h.Text = lblStockTakingPeriodEnd2h.Text = lblStockTakingPeriodEnd2h.Text = item.LabelName;
                else if (lblStockTakingTransTypeIn2h.ID == item.LabelID)
                    txtStockTakingTransTypeIn2h.Text = lblStockTakingTransTypeIn2h.Text = lblStockTakingTransTypeIn2h.Text = item.LabelName;
                else if (lblStockTakingTransTypeOut2h.ID == item.LabelID)
                    txtStockTakingTransTypeOut2h.Text = lblStockTakingTransTypeOut2h.Text = lblStockTakingTransTypeOut2h.Text = item.LabelName;
                else if (lblStockTakingTransSubTypeIn2h.ID == item.LabelID)
                    txtStockTakingTransSubTypeIn2h.Text = lblStockTakingTransSubTypeIn2h.Text = lblStockTakingTransSubTypeIn2h.Text = item.LabelName;
                else if (lblStockTakingTransSubTypeOut2h.ID == item.LabelID)
                    txtStockTakingTransSubTypeOut2h.Text = lblStockTakingTransSubTypeOut2h.Text = lblStockTakingTransSubTypeOut2h.Text = item.LabelName;
                else if (lblStockTakingtransidInLast2h.ID == item.LabelID)
                    txtStockTakingtransidInLast2h.Text = lblStockTakingtransidInLast2h.Text = lblStockTakingtransidInLast2h.Text = item.LabelName;
                else if (lblStockTakingtransidOutLast2h.ID == item.LabelID)
                    txtStockTakingtransidOutLast2h.Text = lblStockTakingtransidOutLast2h.Text = lblStockTakingtransidOutLast2h.Text = item.LabelName;
                //else if (lblDefaultCUSTVENDID2h.ID == item.LabelID)
                //    txtDefaultCUSTVENDID2h.Text = lblDefaultCUSTVENDID2h.Text = lblDefaultCUSTVENDID2h.Text = item.LabelName;
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
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblSetupInventory").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblSetupInventory.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblSetupInventory").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenentID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenentID1s.Text;
                //if (lbllocationID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationID1s.Text;
                 if (lblTransferOutTransType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferOutTransType1s.Text;
                else if (lblTransferOutTransSubType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferOutTransSubType1s.Text;
                else if (lblTransferInTransType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferInTransType1s.Text;
                else if (lblTransferInTransSubType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferInTransSubType1s.Text;
                else if (lblConsumeTransType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConsumeTransType1s.Text;
                else if (lblConsumeTransSubType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConsumeTransSubType1s.Text;
                //else if (lbltransidOut1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttransidOut1s.Text;
                //else if (lbltranssubidOut1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttranssubidOut1s.Text;
                else if (lbltransidin1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttransidin1s.Text;
                else if (lbltranssubidIn1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubidIn1s.Text;
                else if (lbltransidConsume1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttransidConsume1s.Text;
                else if (lbltranssubidConsume1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubidConsume1s.Text;
                else if (lblMYSYSNAMEOut1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAMEOut1s.Text;
                else if (lblMYSYSNAMEIn1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAMEIn1s.Text;
                else if (lblStockTeking1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTeking1s.Text;
                else if (lblStockTakingPeriodBegin1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingPeriodBegin1s.Text;
                else if (lblStockTakingPeriodEnd1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingPeriodEnd1s.Text;
                else if (lblStockTakingTransTypeIn1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransTypeIn1s.Text;
                else if (lblStockTakingTransTypeOut1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransTypeOut1s.Text;
                else if (lblStockTakingTransSubTypeIn1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransSubTypeIn1s.Text;
                else if (lblStockTakingTransSubTypeOut1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransSubTypeOut1s.Text;
                else if (lblStockTakingtransidInLast1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingtransidInLast1s.Text;
                else if (lblStockTakingtransidOutLast1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingtransidOutLast1s.Text;
                //else if (lblDefaultCUSTVENDID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDefaultCUSTVENDID1s.Text;
                //else if (lblCreated1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCreated1s.Text;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;

                //else if (lblTenentID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenentID2h.Text;
                //else if (lbllocationID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationID2h.Text;
                else if (lblTransferOutTransType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferOutTransType2h.Text;
                else if (lblTransferOutTransSubType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferOutTransSubType2h.Text;
                else if (lblTransferInTransType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferInTransType2h.Text;
                else if (lblTransferInTransSubType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTransferInTransSubType2h.Text;
                else if (lblConsumeTransType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConsumeTransType2h.Text;
                else if (lblConsumeTransSubType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtConsumeTransSubType2h.Text;
                //else if (lbltransidOut2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttransidOut2h.Text;
                //else if (lbltranssubidOut2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttranssubidOut2h.Text;
                else if (lbltransidin2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttransidin2h.Text;
                else if (lbltranssubidIn2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubidIn2h.Text;
                else if (lbltransidConsume2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttransidConsume2h.Text;
                else if (lbltranssubidConsume2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubidConsume2h.Text;
                else if (lblMYSYSNAMEOut2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAMEOut2h.Text;
                else if (lblMYSYSNAMEIn2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAMEIn2h.Text;
                else if (lblStockTeking2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTeking2h.Text;
                else if (lblStockTakingPeriodBegin2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingPeriodBegin2h.Text;
                else if (lblStockTakingPeriodEnd2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingPeriodEnd2h.Text;
                else if (lblStockTakingTransTypeIn2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransTypeIn2h.Text;
                else if (lblStockTakingTransTypeOut2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransTypeOut2h.Text;
                else if (lblStockTakingTransSubTypeIn2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransSubTypeIn2h.Text;
                else if (lblStockTakingTransSubTypeOut2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingTransSubTypeOut2h.Text;
                else if (lblStockTakingtransidInLast2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingtransidInLast2h.Text;
                else if (lblStockTakingtransidOutLast2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStockTakingtransidOutLast2h.Text;
                //else if (lblDefaultCUSTVENDID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDefaultCUSTVENDID2h.Text;
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
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblSetupInventory.xml"));

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
            txttransferouttranstype.Enabled = true;
            txttransferoutsubtype.Enabled = true;
            txttransferintranstype.Enabled = true;
            txttransferintranssubtype.Enabled = true;
            txtconsumetranstype.Enabled = true;
            txtconsumetranssubtype.Enabled = true;
            //txttransidOut.Enabled = true;
            //txttranssubidOut.Enabled = true;
            txttransidin.Enabled = true;
            txttranssubidIn.Enabled = true;
            txttransidConsume.Enabled = true;
            txttranssubidConsume.Enabled = true;
            txtMYSYSNAMEOut.Enabled = true;
            txtMYSYSNAMEIn.Enabled = true;
            chkteking.Enabled = true;
            chkstock.Enabled = true;
            chkend.Enabled = true;
            txtStockTakingTransTypeIn.Enabled = true;
            txtStockTakingTransTypeOut.Enabled = true;
            txtstocktranssubtypein.Enabled = true;
            txtstocktakingtranssubtypeout.Enabled = true;
            txtStockTakingtransidInLast.Enabled = true;
            txtStockTakingtransidOutLast.Enabled = true;
            //txtDefaultCUSTVENDID.Enabled = true;
            //txtCreated.Enabled = true;
            //txtDateTime.Enabled = true;
            cbActive.Enabled = true;
            //cbDeleted.Enabled = true;

        }
        public void Readonly()
        {
            //  navigation.Visible = true;
            //drplocation.Enabled = false;
            txttransferouttranstype.Enabled = false;
            txttransferoutsubtype.Enabled = false;
            txttransferintranstype.Enabled = false;
            txttransferintranssubtype.Enabled = false;
            txtconsumetranstype.Enabled = false;
            txtconsumetranssubtype.Enabled = false;
            //txttransidOut.Enabled = false;
            //txttranssubidOut.Enabled = false;
            txttransidin.Enabled = false;
            txttranssubidIn.Enabled = false;
            txttransidConsume.Enabled = false;
            txttranssubidConsume.Enabled = false;
            txtMYSYSNAMEOut.Enabled = false;
            txtMYSYSNAMEIn.Enabled = false;
            chkteking.Enabled = false;
            chkstock.Enabled = false;
            chkend.Enabled = false;
            txtStockTakingTransTypeIn.Enabled = false;
            txtStockTakingTransTypeOut.Enabled = false;
            txtStockTakingTransTypeIn.Enabled = false;
            txtStockTakingTransTypeOut.Enabled = false;
            txtstocktranssubtypein.Enabled = false;
            txtstocktakingtranssubtypeout.Enabled = false;
            txtStockTakingtransidInLast.Enabled = false;
            //txtCreated.Enabled = false;
            //txtDateTime.Enabled = false;
            cbActive.Enabled = false;
            //cbDeleted.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblSetupInventories.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSetupInventories.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transidOut).Take(take).Skip(Skip)).ToList());
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
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblSetupInventories.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSetupInventories.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transidOut).Take(take).Skip(Skip)).ToList());
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
                //  ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int Totalrec = DB.tblSetupInventories.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSetupInventories.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transidOut).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.tblSetupInventories.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSetupInventories.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.transidOut).Take(take).Skip(Skip)).ToList());
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
            FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                if (e.CommandName == "btnDelete")
                {

                    int ID = Convert.ToInt32(e.CommandArgument);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    Database.tblSetupInventory objSOJobDesc = DB.tblSetupInventories.Single(p => p.TenentID == TID && p.transidOut == ID);
                 //   objSOJobDesc.Deleted = true;
                    DB.tblSetupInventories.DeleteObject(objSOJobDesc);
                    DB.SaveChanges();
                    BindData();
                    
                }

                if (e.CommandName == "btnEdit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                    Database.tblSetupInventory objtblSetupInventory = DB.tblSetupInventories.Single(p => p.TenentID == TID && p.transidOut == ID);

                    //drplocation.SelectedValue = objtblSetupInventory.locationID.ToString();
                    txttransferouttranstype.Text = objtblSetupInventory.TransferOutTransType.ToString();
                    txttransferoutsubtype.Text = objtblSetupInventory.TransferOutTransSubType.ToString();
                    txttransferintranstype.Text = objtblSetupInventory.TransferInTransType.ToString();
                    txttransferintranssubtype.Text = objtblSetupInventory.TransferInTransSubType.ToString();
                    txtconsumetranstype.Text = objtblSetupInventory.ConsumeTransType.ToString();
                    txtconsumetranssubtype.Text = objtblSetupInventory.ConsumeTransSubType.ToString();
                    //txttransidOut.Text = objtblSetupInventory.transidOut.ToString();
                    //txttranssubidOut.Text = objtblSetupInventory.transsubidOut.ToString();
                    txttransidin.Text = objtblSetupInventory.transidin.ToString();
                    txttranssubidIn.Text = objtblSetupInventory.transsubidIn.ToString();
                    txttransidConsume.Text = objtblSetupInventory.transidConsume.ToString();
                    txttranssubidConsume.Text = objtblSetupInventory.transsubidConsume.ToString();
                    txtMYSYSNAMEOut.Text = objtblSetupInventory.MYSYSNAMEOut.ToString();
                    txtMYSYSNAMEIn.Text = objtblSetupInventory.MYSYSNAMEIn.ToString();
                    chkteking.Checked = (objtblSetupInventory.StockTeking == true) ? true : false;
                    chkstock.Checked = (objtblSetupInventory.StockTakingPeriodBegin == true) ? true : false;
                    chkend.Checked = (objtblSetupInventory.StockTakingPeriodEnd == true) ? true : false;
                    txtStockTakingTransTypeIn.Text = objtblSetupInventory.StockTakingTransTypeIn.ToString();
                    txtStockTakingTransTypeOut.Text = objtblSetupInventory.StockTakingTransTypeOut.ToString();
                    txtstocktranssubtypein.Text = objtblSetupInventory.StockTakingTransSubTypeIn.ToString();
                    txtstocktakingtranssubtypeout.Text = objtblSetupInventory.StockTakingTransSubTypeOut.ToString();
                    txtStockTakingtransidInLast.Text = objtblSetupInventory.StockTakingtransidInLast.ToString();
                    txtStockTakingtransidOutLast.Text = objtblSetupInventory.StockTakingtransidOutLast.ToString();
                    //txtDefaultCUSTVENDID.Text = objtblSetupInventory.DefaultCUSTVENDID.ToString();
                    //txtCreated.Text = objtblSetupInventory.Created.ToString();
                    //  txtDateTime.Text = objtblSetupInventory.DateTime.ToString();
                    cbActive.Checked = (objtblSetupInventory.Active == true) ? true : false;
                    //cbDeleted.Checked = (objtblSetupInventory.Deleted == true) ? true : false;

                    btnAdd.Visible = true;
                    btnAdd.Text = "Update Setting";
                    ViewState["Edit"] = ID;
                    Write();
                }
                scope.Complete(); //  To commit.

            }
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblSetupInventories.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSetupInventories.OrderBy(m => m.transidOut).Take(Tvalue).Skip(Svalue)).ToList());
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
    }
}