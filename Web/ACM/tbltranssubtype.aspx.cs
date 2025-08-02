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

namespace Web.ACM
{
    public partial class tbltranssubtype : System.Web.UI.Page
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
              //  FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
           int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
           var List = DB.tbltranssubtypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transsubid).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        #region PAge Genarator
        public void GetShow()
        {

            lbltransid1s.Attributes["class"] = lblMYSYSNAME1s.Attributes["class"] = lbltranssubtype11s.Attributes["class"] = lbltranssubtype21s.Attributes["class"] = lbltranssubtype31s.Attributes["class"] = lblOpQtyBeh1s.Attributes["class"] = lblOnHandBeh1s.Attributes["class"] = lblQtyOutBeh1s.Attributes["class"] = lblQtyConsumedBeh1s.Attributes["class"] = lblQtyReservedBeh1s.Attributes["class"] = lblQtyAtDestination1s.Attributes["class"] = lblQtyAtSource1s.Attributes["class"] = lblserialno1s.Attributes["class"] = lblyears1s.Attributes["class"] = lblActive1s.Attributes["class"] = lbltranssubtype1s.Attributes["class"] = "control-label col-md-4  getshow";
            lbltransid2h.Attributes["class"] = lblMYSYSNAME2h.Attributes["class"] = lbltranssubtype12h.Attributes["class"] = lbltranssubtype22h.Attributes["class"] = lbltranssubtype32h.Attributes["class"] = lblOpQtyBeh2h.Attributes["class"] = lblOnHandBeh2h.Attributes["class"] = lblQtyOutBeh2h.Attributes["class"] = lblQtyConsumedBeh2h.Attributes["class"] = lblQtyReservedBeh2h.Attributes["class"] = lblQtyAtDestination2h.Attributes["class"] = lblQtyAtSource2h.Attributes["class"] = lblserialno2h.Attributes["class"] = lblyears2h.Attributes["class"] = lblActive2h.Attributes["class"] = lbltranssubtype2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lbltransid1s.Attributes["class"] = lblMYSYSNAME1s.Attributes["class"] = lbltranssubtype11s.Attributes["class"] = lbltranssubtype21s.Attributes["class"] = lbltranssubtype31s.Attributes["class"] = lblOpQtyBeh1s.Attributes["class"] = lblOnHandBeh1s.Attributes["class"] = lblQtyOutBeh1s.Attributes["class"] = lblQtyConsumedBeh1s.Attributes["class"] = lblQtyReservedBeh1s.Attributes["class"] = lblQtyAtDestination1s.Attributes["class"] = lblQtyAtSource1s.Attributes["class"] = lblserialno1s.Attributes["class"] = lblyears1s.Attributes["class"] = lblActive1s.Attributes["class"] = lbltranssubtype1s.Attributes["class"] = "control-label col-md-4  gethide";
            lbltransid2h.Attributes["class"] = lblMYSYSNAME2h.Attributes["class"] = lbltranssubtype12h.Attributes["class"] = lbltranssubtype22h.Attributes["class"] = lbltranssubtype32h.Attributes["class"] = lblOpQtyBeh2h.Attributes["class"] = lblOnHandBeh2h.Attributes["class"] = lblQtyOutBeh2h.Attributes["class"] = lblQtyConsumedBeh2h.Attributes["class"] = lblQtyReservedBeh2h.Attributes["class"] = lblQtyAtDestination2h.Attributes["class"] = lblQtyAtSource2h.Attributes["class"] = lblserialno2h.Attributes["class"] = lblyears2h.Attributes["class"] = lblActive2h.Attributes["class"] = lbltranssubtype2h.Attributes["class"] = "control-label col-md-4  getshow";
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
        #endregion
        public void Clear()
        {
            drptransid.SelectedIndex = 0;
            drpMYSYSNAME.SelectedIndex = 0;
          //  drptranssubid.SelectedIndex = 0;
            txttranssubtype1.Text = "";
            txttranssubtype2.Text = "";
            txttranssubtype3.Text = "";
            txtOpQtyBeh.Text = "";
            txtOnHandBeh.Text = "";
            txtQtyOutBeh.Text = "";
            txtQtyConsumedBeh.Text = "";
            txtQtyReservedBeh.Text = "";
            txtQtyAtDestination.Text = "";
            txtQtyAtSource.Text = "";
            txtserialno.Text = "";
            txtyears.Text = "";
            cbActive.Checked = false;
            //txtCRUP_ID.Text = "";
            txttranssubtype.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                { int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.tbltranssubtype objtbltranssubtype = new Database.tbltranssubtype();
                        //Server Content Send data Yogesh+
                        objtbltranssubtype.TenentID = TID;
                        objtbltranssubtype.transid = Convert.ToInt32(drptransid.SelectedValue);
                        objtbltranssubtype.MYSYSNAME = drpMYSYSNAME.SelectedValue;
                        objtbltranssubtype.transsubid = DB.tbltranssubtypes.Count() > 0 ? Convert.ToInt32(DB.tbltranssubtypes.Max(p => p.transsubid) + 1) : 1;
                        objtbltranssubtype.transsubtype1 = txttranssubtype1.Text;
                        objtbltranssubtype.transsubtype2 = txttranssubtype2.Text;
                        objtbltranssubtype.transsubtype3 = txttranssubtype3.Text;
                        objtbltranssubtype.OpQtyBeh = txtOpQtyBeh.Text;
                        objtbltranssubtype.OnHandBeh = txtOnHandBeh.Text;
                        objtbltranssubtype.QtyOutBeh = txtQtyOutBeh.Text;
                        objtbltranssubtype.QtyConsumedBeh = txtQtyConsumedBeh.Text;
                        objtbltranssubtype.QtyReservedBeh = txtQtyReservedBeh.Text;
                        objtbltranssubtype.QtyAtDestination = txtQtyAtDestination.Text;
                        objtbltranssubtype.QtyAtSource = txtQtyAtSource.Text;
                        objtbltranssubtype.serialno = txtserialno.Text;
                        objtbltranssubtype.years = txtyears.Text;
                        objtbltranssubtype.Active = cbActive.Checked==true?"Y":"N";
                       // objtbltranssubtype.CRUP_ID = txtCRUP_ID.Text;
                        objtbltranssubtype.transsubtype = txttranssubtype.Text;


                        DB.tbltranssubtypes.AddObject(objtbltranssubtype);
                        DB.SaveChanges();
                       // Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                      //  BindData();
                       // navigation.Visible = true;
                        Readonly();
                      //  FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.transsubid == ID);
                            objtbltranssubtype.transid = Convert.ToInt32(drptransid.SelectedValue);
                            objtbltranssubtype.MYSYSNAME = drpMYSYSNAME.SelectedValue;
                           // objtbltranssubtype.transsubid = Convert.ToInt32(drptranssubid.SelectedValue);
                            objtbltranssubtype.transsubtype1 = txttranssubtype1.Text;
                            objtbltranssubtype.transsubtype2 = txttranssubtype2.Text;
                            objtbltranssubtype.transsubtype3 = txttranssubtype3.Text;
                            objtbltranssubtype.OpQtyBeh = txtOpQtyBeh.Text;
                            objtbltranssubtype.OnHandBeh = txtOnHandBeh.Text;
                            objtbltranssubtype.QtyOutBeh = txtQtyOutBeh.Text;
                            objtbltranssubtype.QtyConsumedBeh = txtQtyConsumedBeh.Text;
                            objtbltranssubtype.QtyReservedBeh = txtQtyReservedBeh.Text;
                            objtbltranssubtype.QtyAtDestination = txtQtyAtDestination.Text;
                            objtbltranssubtype.QtyAtSource = txtQtyAtSource.Text;
                            objtbltranssubtype.serialno = txtserialno.Text;
                            objtbltranssubtype.years = txtyears.Text;
                            objtbltranssubtype.Active = cbActive.Checked == true ? "Y" : "N";
                           // objtbltranssubtype.CRUP_ID = txtCRUP_ID.Text;
                            objtbltranssubtype.transsubtype = txttranssubtype.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();
                          //  Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                          //  BindData();
                          //  navigation.Visible = true;
                            Readonly();
                          //  FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        { 
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            Classes.EcommAdminClass.getdropdown(drpMYSYSNAME, TID, "", "", "", "TBLSYSTEMS");
            //select * from TBLSYSTEMS where ACTIVE = 1

            //drpMYSYSNAME.DataSource = DB.TBLSYSTEMS.Where(P => P.ACTIVE == "1");
            //drpMYSYSNAME.DataTextField = "SYSDESC1";
            //drpMYSYSNAME.DataValueField = "MYSYSNAME";
            //drpMYSYSNAME.DataBind();
            //drpMYSYSNAME.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select System--", "0"));

            Classes.EcommAdminClass.getdropdown(drptransid, TID, "", "", "", "tbltranssubtype");
            //select * from tbltranssubtype where Active = 'Y'

            //drptransid.DataSource = DB.tbltranssubtypes.Where(P => P.Active == "Y");
            //drptransid.DataTextField = "transsubtype1";
            //drptransid.DataValueField = "transid";
            //drptransid.DataBind();
            //drptransid.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        #region PAge Genarator navigation
       
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
            Listview1.SelectedIndex = 0;
            drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
           // txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
           // drptranssubid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype1.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype2.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype3.Text = Listview1.SelectedDataKey[0].ToString();
            txtOpQtyBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtOnHandBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyOutBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyConsumedBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyReservedBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyAtDestination.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyAtSource.Text = Listview1.SelectedDataKey[0].ToString();
            txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
            txtyears.Text = Listview1.SelectedDataKey[0].ToString();
           // txtActive.Text = Listview1.SelectedDataKey[0].ToString();
           // txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
              //  txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
              //  drptranssubid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype1.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype2.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype3.Text = Listview1.SelectedDataKey[0].ToString();
                txtOpQtyBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtOnHandBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyOutBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyConsumedBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyReservedBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyAtDestination.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyAtSource.Text = Listview1.SelectedDataKey[0].ToString();
                txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
                txtyears.Text = Listview1.SelectedDataKey[0].ToString();
              //  txtActive.Text = Listview1.SelectedDataKey[0].ToString();
              //  txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype.Text = Listview1.SelectedDataKey[0].ToString();

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
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
               // txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
               // drptranssubid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype1.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype2.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype3.Text = Listview1.SelectedDataKey[0].ToString();
                txtOpQtyBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtOnHandBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyOutBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyConsumedBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyReservedBeh.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyAtDestination.Text = Listview1.SelectedDataKey[0].ToString();
                txtQtyAtSource.Text = Listview1.SelectedDataKey[0].ToString();
                txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
                txtyears.Text = Listview1.SelectedDataKey[0].ToString();
               // txtActive.Text = Listview1.SelectedDataKey[0].ToString();
               // txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                txttranssubtype.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            drptransid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
           // txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
           // drptranssubid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype1.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype2.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype3.Text = Listview1.SelectedDataKey[0].ToString();
            txtOpQtyBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtOnHandBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyOutBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyConsumedBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyReservedBeh.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyAtDestination.Text = Listview1.SelectedDataKey[0].ToString();
            txtQtyAtSource.Text = Listview1.SelectedDataKey[0].ToString();
            txtserialno.Text = Listview1.SelectedDataKey[0].ToString();
            txtyears.Text = Listview1.SelectedDataKey[0].ToString();
           // txtActive.Text = Listview1.SelectedDataKey[0].ToString();
           // txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            txttranssubtype.Text = Listview1.SelectedDataKey[0].ToString();

        }
        #endregion

        #region PAge Genarator language

        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lbltransid2h.Visible = lblMYSYSNAME2h.Visible = lbltranssubtype12h.Visible = lbltranssubtype22h.Visible = lbltranssubtype32h.Visible = lblOpQtyBeh2h.Visible = lblOnHandBeh2h.Visible = lblQtyOutBeh2h.Visible = lblQtyConsumedBeh2h.Visible = lblQtyReservedBeh2h.Visible = lblQtyAtDestination2h.Visible = lblQtyAtSource2h.Visible = lblserialno2h.Visible = lblyears2h.Visible = lblActive2h.Visible = lbltranssubtype2h.Visible = false;
                    //2true
                    txttransid2h.Visible = txtMYSYSNAME2h.Visible = txttranssubtype12h.Visible = txttranssubtype22h.Visible = txttranssubtype32h.Visible = txtOpQtyBeh2h.Visible = txtOnHandBeh2h.Visible = txtQtyOutBeh2h.Visible = txtQtyConsumedBeh2h.Visible = txtQtyReservedBeh2h.Visible = txtQtyAtDestination2h.Visible = txtQtyAtSource2h.Visible = txtserialno2h.Visible = txtyears2h.Visible = txtActive2h.Visible = txttranssubtype2h.Visible = true;

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
                    lbltransid2h.Visible = lblMYSYSNAME2h.Visible = lbltranssubtype12h.Visible = lbltranssubtype22h.Visible = lbltranssubtype32h.Visible = lblOpQtyBeh2h.Visible = lblOnHandBeh2h.Visible = lblQtyOutBeh2h.Visible = lblQtyConsumedBeh2h.Visible = lblQtyReservedBeh2h.Visible = lblQtyAtDestination2h.Visible = lblQtyAtSource2h.Visible = lblserialno2h.Visible = lblyears2h.Visible = lblActive2h.Visible = lbltranssubtype2h.Visible = true;
                    //2false
                    txttransid2h.Visible = txtMYSYSNAME2h.Visible = txttranssubtype12h.Visible = txttranssubtype22h.Visible = txttranssubtype32h.Visible = txtOpQtyBeh2h.Visible = txtOnHandBeh2h.Visible = txtQtyOutBeh2h.Visible = txtQtyConsumedBeh2h.Visible = txtQtyReservedBeh2h.Visible = txtQtyAtDestination2h.Visible = txtQtyAtSource2h.Visible = txtserialno2h.Visible = txtyears2h.Visible = txtActive2h.Visible = txttranssubtype2h.Visible = false;

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
                    lbltransid1s.Visible = lblMYSYSNAME1s.Visible = lbltranssubtype11s.Visible = lbltranssubtype21s.Visible = lbltranssubtype31s.Visible = lblOpQtyBeh1s.Visible = lblOnHandBeh1s.Visible = lblQtyOutBeh1s.Visible = lblQtyConsumedBeh1s.Visible = lblQtyReservedBeh1s.Visible = lblQtyAtDestination1s.Visible = lblQtyAtSource1s.Visible = lblserialno1s.Visible = lblyears1s.Visible = lblActive1s.Visible = lbltranssubtype1s.Visible = false;
                    //1true
                    txttransid1s.Visible = txtMYSYSNAME1s.Visible = txttranssubtype11s.Visible = txttranssubtype21s.Visible = txttranssubtype31s.Visible = txtOpQtyBeh1s.Visible = txtOnHandBeh1s.Visible = txtQtyOutBeh1s.Visible = txtQtyConsumedBeh1s.Visible = txtQtyReservedBeh1s.Visible = txtQtyAtDestination1s.Visible = txtQtyAtSource1s.Visible = txtserialno1s.Visible = txtyears1s.Visible = txtActive1s.Visible = txttranssubtype1s.Visible = true;
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
                    lbltransid1s.Visible = lblMYSYSNAME1s.Visible = lbltranssubtype11s.Visible = lbltranssubtype21s.Visible = lbltranssubtype31s.Visible = lblOpQtyBeh1s.Visible = lblOnHandBeh1s.Visible = lblQtyOutBeh1s.Visible = lblQtyConsumedBeh1s.Visible = lblQtyReservedBeh1s.Visible = lblQtyAtDestination1s.Visible = lblQtyAtSource1s.Visible = lblserialno1s.Visible = lblyears1s.Visible = lblActive1s.Visible = lbltranssubtype1s.Visible = true;
                    //1false
                    txttransid1s.Visible = txtMYSYSNAME1s.Visible = txttranssubtype11s.Visible = txttranssubtype21s.Visible = txttranssubtype31s.Visible = txtOpQtyBeh1s.Visible = txtOnHandBeh1s.Visible = txtQtyOutBeh1s.Visible = txtQtyConsumedBeh1s.Visible = txtQtyReservedBeh1s.Visible = txtQtyAtDestination1s.Visible = txtQtyAtSource1s.Visible = txtserialno1s.Visible = txtyears1s.Visible = txtActive1s.Visible = txttranssubtype1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((ACMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("tbltranssubtype").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lbltransid1s.ID == item.LabelID)
                    txttransid1s.Text = lbltransid1s.Text = lblhtransid.Text = item.LabelName;
                else if (lblMYSYSNAME1s.ID == item.LabelID)
                    txtMYSYSNAME1s.Text = lblMYSYSNAME1s.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lbltranssubtype11s.ID == item.LabelID)
                    txttranssubtype11s.Text = lbltranssubtype11s.Text = lblhtranssubtype1.Text = item.LabelName;
                else if (lbltranssubtype21s.ID == item.LabelID)
                    txttranssubtype21s.Text = lbltranssubtype21s.Text =  item.LabelName;
                else if (lbltranssubtype31s.ID == item.LabelID)
                    txttranssubtype31s.Text = lbltranssubtype31s.Text =  item.LabelName;
                else if (lblOpQtyBeh1s.ID == item.LabelID)
                    txtOpQtyBeh1s.Text = lblOpQtyBeh1s.Text = lblhOpQtyBeh.Text = item.LabelName;
                else if (lblOnHandBeh1s.ID == item.LabelID)
                    txtOnHandBeh1s.Text = lblOnHandBeh1s.Text =  item.LabelName;
                else if (lblQtyOutBeh1s.ID == item.LabelID)
                    txtQtyOutBeh1s.Text = lblQtyOutBeh1s.Text =  item.LabelName;
                else if (lblQtyConsumedBeh1s.ID == item.LabelID)
                    txtQtyConsumedBeh1s.Text = lblQtyConsumedBeh1s.Text = item.LabelName;
                else if (lblQtyReservedBeh1s.ID == item.LabelID)
                    txtQtyReservedBeh1s.Text = lblQtyReservedBeh1s.Text =  item.LabelName;
                else if (lblQtyAtDestination1s.ID == item.LabelID)
                    txtQtyAtDestination1s.Text = lblQtyAtDestination1s.Text =  item.LabelName;
                else if (lblQtyAtSource1s.ID == item.LabelID)
                    txtQtyAtSource1s.Text = lblQtyAtSource1s.Text =  item.LabelName;
                else if (lblserialno1s.ID == item.LabelID)
                    txtserialno1s.Text = lblserialno1s.Text = lblhserialno.Text = item.LabelName;
                else if (lblyears1s.ID == item.LabelID)
                    txtyears1s.Text = lblyears1s.Text = lblhyears.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text =  item.LabelName;
                else if (lbltranssubtype1s.ID == item.LabelID)
                    txttranssubtype1s.Text = lbltranssubtype1s.Text =  item.LabelName;
               else if (lbltransid2h.ID == item.LabelID)
                    txttransid2h.Text = lbltransid2h.Text = lblhtransid.Text = item.LabelName;
                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    txtMYSYSNAME2h.Text = lblMYSYSNAME2h.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lbltranssubtype12h.ID == item.LabelID)
                    txttranssubtype12h.Text = lbltranssubtype12h.Text = lblhtranssubtype1.Text = item.LabelName;
                else if (lbltranssubtype22h.ID == item.LabelID)
                    txttranssubtype22h.Text = lbltranssubtype22h.Text = item.LabelName;
                else if (lbltranssubtype32h.ID == item.LabelID)
                    txttranssubtype32h.Text = lbltranssubtype32h.Text =  item.LabelName;
                else if (lblOpQtyBeh2h.ID == item.LabelID)
                    txtOpQtyBeh2h.Text = lblOpQtyBeh2h.Text = lblhOpQtyBeh.Text = item.LabelName;
                else if (lblOnHandBeh2h.ID == item.LabelID)
                    txtOnHandBeh2h.Text = lblOnHandBeh2h.Text =  item.LabelName;
                else if (lblQtyOutBeh2h.ID == item.LabelID)
                    txtQtyOutBeh2h.Text = lblQtyOutBeh2h.Text =  item.LabelName;
                else if (lblQtyConsumedBeh2h.ID == item.LabelID)
                    txtQtyConsumedBeh2h.Text = lblQtyConsumedBeh2h.Text =  item.LabelName;
                else if (lblQtyReservedBeh2h.ID == item.LabelID)
                    txtQtyReservedBeh2h.Text = lblQtyReservedBeh2h.Text =  item.LabelName;
                else if (lblQtyAtDestination2h.ID == item.LabelID)
                    txtQtyAtDestination2h.Text = lblQtyAtDestination2h.Text =  item.LabelName;
                else if (lblQtyAtSource2h.ID == item.LabelID)
                    txtQtyAtSource2h.Text = lblQtyAtSource2h.Text =  item.LabelName;
                else if (lblserialno2h.ID == item.LabelID)
                    txtserialno2h.Text = lblserialno2h.Text = lblhserialno.Text = item.LabelName;
                else if (lblyears2h.ID == item.LabelID)
                    txtyears2h.Text = lblyears2h.Text = lblhyears.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text =  item.LabelName;
                else if (lbltranssubtype2h.ID == item.LabelID)
                    txttranssubtype2h.Text = lbltranssubtype2h.Text =  item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("tbltranssubtype").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\tbltranssubtype.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("tbltranssubtype").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lbltransid1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttransid1s.Text;
                else if (lblMYSYSNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME1s.Text;
                else if (lbltranssubtype11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype11s.Text;
                else if (lbltranssubtype21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype21s.Text;
                else if (lbltranssubtype31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype31s.Text;
                else if (lblOpQtyBeh1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOpQtyBeh1s.Text;
                else if (lblOnHandBeh1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOnHandBeh1s.Text;
                else if (lblQtyOutBeh1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyOutBeh1s.Text;
                else if (lblQtyConsumedBeh1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyConsumedBeh1s.Text;
                else if (lblQtyReservedBeh1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyReservedBeh1s.Text;
                else if (lblQtyAtDestination1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyAtDestination1s.Text;
                else if (lblQtyAtSource1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyAtSource1s.Text;
                else if (lblserialno1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtserialno1s.Text;
                else if (lblyears1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtyears1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lbltranssubtype1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype1s.Text;

               else if (lbltransid2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttransid2h.Text;
                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME2h.Text;
                else if (lbltranssubtype12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype12h.Text;
                else if (lbltranssubtype22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype22h.Text;
                else if (lbltranssubtype32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype32h.Text;
                else if (lblOpQtyBeh2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOpQtyBeh2h.Text;
                else if (lblOnHandBeh2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOnHandBeh2h.Text;
                else if (lblQtyOutBeh2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyOutBeh2h.Text;
                else if (lblQtyConsumedBeh2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyConsumedBeh2h.Text;
                else if (lblQtyReservedBeh2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyReservedBeh2h.Text;
                else if (lblQtyAtDestination2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyAtDestination2h.Text;
                else if (lblQtyAtSource2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQtyAtSource2h.Text;
                else if (lblserialno2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtserialno2h.Text;
                else if (lblyears2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtyears2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lbltranssubtype2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txttranssubtype2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\tbltranssubtype.xml"));

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

        #endregion
        public void Write()
        {
          //  navigation.Visible = false;
            drptransid.Enabled = true;
            drpMYSYSNAME.Enabled = true;
          //  drptranssubid.Enabled = true;
            txttranssubtype1.Enabled = true;
            txttranssubtype2.Enabled = true;
            txttranssubtype3.Enabled = true;
            txtOpQtyBeh.Enabled = true;
            txtOnHandBeh.Enabled = true;
            txtQtyOutBeh.Enabled = true;
            txtQtyConsumedBeh.Enabled = true;
            txtQtyReservedBeh.Enabled = true;
            txtQtyAtDestination.Enabled = true;
            txtQtyAtSource.Enabled = true;
            txtserialno.Enabled = true;
            txtyears.Enabled = true;
            cbActive.Enabled = true;
           // txtCRUP_ID.Enabled = true;
            txttranssubtype.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drptransid.Enabled = false;
            drpMYSYSNAME.Enabled = false;
            txttranssubtype1.Enabled = false;
            txttranssubtype2.Enabled = false;
            txttranssubtype3.Enabled = false;
            txtOpQtyBeh.Enabled = false;
            txtOnHandBeh.Enabled = false;
            txtQtyOutBeh.Enabled = false;
            txtQtyConsumedBeh.Enabled = false;
            txtQtyReservedBeh.Enabled = false;
            txtQtyAtDestination.Enabled = false;
            txtQtyAtSource.Enabled = false;
            txtserialno.Enabled = false;
            txtyears.Enabled = false;
            cbActive.Enabled = false;
           // txtCRUP_ID.Enabled = false;
            txttranssubtype.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbltranssubtypes.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranssubtypes.Where(p=>p.TenentID==TID).OrderBy(m => m.transsubid).Take(take).Skip(Skip)).ToList());
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

            ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbltranssubtypes.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderBy(m => m.transsubid).Take(take).Skip(Skip)).ToList());
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
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbltranssubtypes.Count();
                take = Showdata;
                Skip = 0;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderBy(m => m.transsubid).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB.tbltranssubtypes.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderBy(m => m.transsubid).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            drptransid.DataSource = DB.tbltranssubtypes.Where(P => P.Active == "Y" && P.TenentID==TID);
            drptransid.DataTextField = "transsubtype1";
            drptransid.DataValueField = "transid";
            drptransid.DataBind();
            drptransid.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {

                        int ID =Convert.ToInt32(e.CommandArgument);


                        Database.tbltranssubtype objSOJobDesc = DB.tbltranssubtypes.Single(p => p.transsubid == ID && p.TenentID==TID);
                        objSOJobDesc.Active = "N";
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderBy(m => m.transsubid).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID =Convert.ToInt32(e.CommandArgument);

                        Database.tbltranssubtype objtbltranssubtype = DB.tbltranssubtypes.Single(p => p.transsubid == ID && p.TenentID==TID);
                        drptransid.SelectedValue = objtbltranssubtype.transid.ToString();
                        drpMYSYSNAME.SelectedValue = objtbltranssubtype.MYSYSNAME.ToString();
                       // drptranssubid.SelectedValue = objtbltranssubtype.transsubid.ToString();
                        txttranssubtype1.Text = objtbltranssubtype.transsubtype1.ToString();
                        txttranssubtype2.Text = objtbltranssubtype.transsubtype2.ToString();
                        txttranssubtype3.Text = objtbltranssubtype.transsubtype3.ToString();
                        txtOpQtyBeh.Text = objtbltranssubtype.OpQtyBeh.ToString();
                        txtOnHandBeh.Text = objtbltranssubtype.OnHandBeh.ToString();
                        txtQtyOutBeh.Text = objtbltranssubtype.QtyOutBeh.ToString();
                        txtQtyConsumedBeh.Text = objtbltranssubtype.QtyConsumedBeh.ToString();
                        txtQtyReservedBeh.Text = objtbltranssubtype.QtyReservedBeh.ToString();
                        txtQtyAtDestination.Text = objtbltranssubtype.QtyAtDestination.ToString();
                        txtQtyAtSource.Text = objtbltranssubtype.QtyAtSource.ToString();
                        txtserialno.Text = objtbltranssubtype.serialno.ToString();
                        txtyears.Text = objtbltranssubtype.years.ToString();
                        cbActive.Checked = objtbltranssubtype.Active=="Y"?true :false;
                       // txtCRUP_ID.Text = objtbltranssubtype.CRUP_ID.ToString();
                        txttranssubtype.Text = objtbltranssubtype.transsubtype.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        Write();
                    }
                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                    throw;
                }
            }
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbltranssubtypes.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderBy(m => m.transsubid).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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