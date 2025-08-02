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
    public partial class TBLSYSTEMS : System.Web.UI.Page
    {
        int TTID = 0;
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
                Session["LANGUAGE"] = "en-US";
                Readonly();
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            var List = DB.TBLSYSTEMS.Where(P => P.ACTIVE == "1").OrderBy(m => m.SystemID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblMYSYSNAME1s.Attributes["class"] = lblSYSDESC11s.Attributes["class"] = lblSYSDESC21s.Attributes["class"] = lblSYSDESC31s.Attributes["class"] = lblSHORTNAME1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = lblSTARTDATE1s.Attributes["class"] = lblSYSDESC1s.Attributes["class"] = lblSYSDESCO1s.Attributes["class"] = lblSYSDESCCH1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblMYSYSNAME2h.Attributes["class"] = lblSYSDESC12h.Attributes["class"] = lblSYSDESC22h.Attributes["class"] = lblSYSDESC32h.Attributes["class"] = lblSHORTNAME2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = lblSTARTDATE2h.Attributes["class"] = lblSYSDESC2h.Attributes["class"] = lblSYSDESCO2h.Attributes["class"] = lblSYSDESCCH2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblMYSYSNAME1s.Attributes["class"] = lblSYSDESC11s.Attributes["class"] = lblSYSDESC21s.Attributes["class"] = lblSYSDESC31s.Attributes["class"] = lblSHORTNAME1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = lblSTARTDATE1s.Attributes["class"] = lblSYSDESC1s.Attributes["class"] = lblSYSDESCO1s.Attributes["class"] = lblSYSDESCCH1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblMYSYSNAME2h.Attributes["class"] = lblSYSDESC12h.Attributes["class"] = lblSYSDESC22h.Attributes["class"] = lblSYSDESC32h.Attributes["class"] = lblSHORTNAME2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = lblSTARTDATE2h.Attributes["class"] = lblSYSDESC2h.Attributes["class"] = lblSYSDESCO2h.Attributes["class"] = lblSYSDESCCH2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpMYID.SelectedIndex = 0;
            txtMYSYSNAME.Text = "";
            txtSYSDESC1.Text = "";
            txtSYSDESC2.Text = "";
            txtSYSDESC3.Text = "";
            txtSHORTNAME.Text = "";
            txtREMARKS.Text = "";
            txtSTARTDATE.Text = "";
            //txtCRUP_ID.Text = "";
            //txtACTIVE.Text = "";
            txtSYSDESC.Text = "";
            txtSYSDESCO.Text = "";
            txtSYSDESCCH.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.TBLSYSTEM objTBLSYSTEMS = new Database.TBLSYSTEM();
                        //Server Content Send data Yogesh
                        objTBLSYSTEMS.TenentID = TTID;
                        objTBLSYSTEMS.SystemID = DB.TBLSYSTEMS.Where(p=>p.TenentID == TTID).Count() > 0 ? Convert.ToInt32(DB.TBLSYSTEMS.Where(p=>p.TenentID == TTID).Max(p => p.SystemID) + 1) : 1;                     
                        objTBLSYSTEMS.MYSYSNAME = txtMYSYSNAME.Text;
                        objTBLSYSTEMS.SYSDESC1 = txtSYSDESC1.Text;
                        objTBLSYSTEMS.SYSDESC2 = txtSYSDESC2.Text;
                        objTBLSYSTEMS.SYSDESC3 = txtSYSDESC3.Text;
                        objTBLSYSTEMS.SHORTNAME = txtSHORTNAME.Text;
                        objTBLSYSTEMS.REMARKS = txtREMARKS.Text;
                        objTBLSYSTEMS.STARTDATE = Convert.ToDateTime(txtSTARTDATE.Text);
                        objTBLSYSTEMS.CRUP_ID = 0;
                        objTBLSYSTEMS.ACTIVE = "1";                        
                        objTBLSYSTEMS.SYSDESC = txtSYSDESC.Text;
                        objTBLSYSTEMS.SYSDESCO = txtSYSDESCO.Text;
                        objTBLSYSTEMS.SYSDESCCH = txtSYSDESCCH.Text;

                        DB.TBLSYSTEMS.AddObject(objTBLSYSTEMS);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "AddNew";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["TID"] != null && ViewState["MID"] != null)
                        {
                            int tid = Convert.ToInt32(ViewState["TID"]);
                            int mid = Convert.ToInt32(ViewState["MID"]);
                            Database.TBLSYSTEM objTBLSYSTEMS = DB.TBLSYSTEMS.Single(p => p.TenentID == tid && p.SystemID == mid);
                           
                            objTBLSYSTEMS.MYSYSNAME = txtMYSYSNAME.Text;
                            objTBLSYSTEMS.SYSDESC1 = txtSYSDESC1.Text;
                            objTBLSYSTEMS.SYSDESC2 = txtSYSDESC2.Text;
                            objTBLSYSTEMS.SYSDESC3 = txtSYSDESC3.Text;
                            objTBLSYSTEMS.SHORTNAME = txtSHORTNAME.Text;
                            objTBLSYSTEMS.REMARKS = txtREMARKS.Text;
                            objTBLSYSTEMS.STARTDATE = Convert.ToDateTime(txtSTARTDATE.Text);                       
                            objTBLSYSTEMS.SYSDESC = txtSYSDESC.Text;
                            objTBLSYSTEMS.SYSDESCO = txtSYSDESCO.Text;
                            objTBLSYSTEMS.SYSDESCCH = txtSYSDESCCH.Text;

                            ViewState["TID"] = null;
                            ViewState["MID"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
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
            //drpSYSDESCCH.Items.Insert(0, new ListItem("-- Select --", "0"));drpSYSDESCCH.DataSource = DB.0;drpSYSDESCCH.DataTextField = "0";drpSYSDESCCH.DataValueField = "0";drpSYSDESCCH.DataBind();
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
            Listview1.SelectedIndex = 0;
            //drpMYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC1.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC2.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC3.Text = Listview1.SelectedDataKey[0].ToString();
            txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
            txtSTARTDATE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESCO.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESCCH.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpMYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC1.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC2.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC3.Text = Listview1.SelectedDataKey[0].ToString();
                txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
                txtSTARTDATE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESCO.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESCCH.Text = Listview1.SelectedDataKey[0].ToString();

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
                //drpMYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC1.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC2.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC3.Text = Listview1.SelectedDataKey[0].ToString();
                txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
                txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
                txtSTARTDATE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESC.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESCO.Text = Listview1.SelectedDataKey[0].ToString();
                txtSYSDESCCH.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpMYID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtMYSYSNAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC1.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC2.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC3.Text = Listview1.SelectedDataKey[0].ToString();
            txtSHORTNAME.Text = Listview1.SelectedDataKey[0].ToString();
            txtREMARKS.Text = Listview1.SelectedDataKey[0].ToString();
            txtSTARTDATE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESC.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESCO.Text = Listview1.SelectedDataKey[0].ToString();
            txtSYSDESCCH.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblMYSYSNAME2h.Visible = lblSYSDESC12h.Visible = lblSYSDESC22h.Visible = lblSYSDESC32h.Visible = lblSHORTNAME2h.Visible = lblREMARKS2h.Visible = lblSTARTDATE2h.Visible = lblSYSDESC2h.Visible = lblSYSDESCO2h.Visible = lblSYSDESCCH2h.Visible = false;
                    //2true
                    txtMYSYSNAME2h.Visible = txtSYSDESC12h.Visible = txtSYSDESC22h.Visible = txtSYSDESC32h.Visible = txtSHORTNAME2h.Visible = txtREMARKS2h.Visible = txtSTARTDATE2h.Visible = txtSYSDESC2h.Visible = txtSYSDESCO2h.Visible = txtSYSDESCCH2h.Visible = true;

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
                    lblMYSYSNAME2h.Visible = lblSYSDESC12h.Visible = lblSYSDESC22h.Visible = lblSYSDESC32h.Visible = lblSHORTNAME2h.Visible = lblREMARKS2h.Visible = lblSTARTDATE2h.Visible = lblSYSDESC2h.Visible = lblSYSDESCO2h.Visible = lblSYSDESCCH2h.Visible = true;
                    //2false
                    txtMYSYSNAME2h.Visible = txtSYSDESC12h.Visible = txtSYSDESC22h.Visible = txtSYSDESC32h.Visible = txtSHORTNAME2h.Visible = txtREMARKS2h.Visible = txtSTARTDATE2h.Visible = txtSYSDESC2h.Visible = txtSYSDESCO2h.Visible = txtSYSDESCCH2h.Visible = false;

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
                    lblMYSYSNAME1s.Visible = lblSYSDESC11s.Visible = lblSYSDESC21s.Visible = lblSYSDESC31s.Visible = lblSHORTNAME1s.Visible = lblREMARKS1s.Visible = lblSTARTDATE1s.Visible = lblSYSDESC1s.Visible = lblSYSDESCO1s.Visible = lblSYSDESCCH1s.Visible = false;
                    //1true
                    txtMYSYSNAME1s.Visible = txtSYSDESC11s.Visible = txtSYSDESC21s.Visible = txtSYSDESC31s.Visible = txtSHORTNAME1s.Visible = txtREMARKS1s.Visible = txtSTARTDATE1s.Visible = txtSYSDESC1s.Visible = txtSYSDESCO1s.Visible = txtSYSDESCCH1s.Visible = true;
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
                    lblMYSYSNAME1s.Visible = lblSYSDESC11s.Visible = lblSYSDESC21s.Visible = lblSYSDESC31s.Visible = lblSHORTNAME1s.Visible = lblREMARKS1s.Visible = lblSTARTDATE1s.Visible = lblSYSDESC1s.Visible = lblSYSDESCO1s.Visible = lblSYSDESCCH1s.Visible = true;
                    //1false
                    txtMYSYSNAME1s.Visible = txtSYSDESC11s.Visible = txtSYSDESC21s.Visible = txtSYSDESC31s.Visible = txtSHORTNAME1s.Visible = txtREMARKS1s.Visible = txtSTARTDATE1s.Visible = txtSYSDESC1s.Visible = txtSYSDESCO1s.Visible = txtSYSDESCCH1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLSYSTEMS").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblMYSYSNAME1s.ID == item.LabelID)
                    txtMYSYSNAME1s.Text = lblMYSYSNAME1s.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lblSYSDESC11s.ID == item.LabelID)
                    txtSYSDESC11s.Text = lblSYSDESC11s.Text = lblhSYSDESC1.Text = item.LabelName;
                else if (lblSYSDESC21s.ID == item.LabelID)
                    txtSYSDESC21s.Text = lblSYSDESC21s.Text = item.LabelName;
                else if (lblSYSDESC31s.ID == item.LabelID)
                    txtSYSDESC31s.Text = lblSYSDESC31s.Text = item.LabelName;
                else if (lblSHORTNAME1s.ID == item.LabelID)
                    txtSHORTNAME1s.Text = lblSHORTNAME1s.Text = lblhSHORTNAME.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = lblhREMARKS.Text = item.LabelName;
                else if (lblSTARTDATE1s.ID == item.LabelID)
                    txtSTARTDATE1s.Text = lblSTARTDATE1s.Text = lblhSTARTDATE.Text = item.LabelName;
                else if (lblSYSDESC1s.ID == item.LabelID)
                    txtSYSDESC1s.Text = lblSYSDESC1s.Text = item.LabelName;
                else if (lblSYSDESCO1s.ID == item.LabelID)
                    txtSYSDESCO1s.Text = lblSYSDESCO1s.Text = item.LabelName;
                else if (lblSYSDESCCH1s.ID == item.LabelID)
                    txtSYSDESCCH1s.Text = lblSYSDESCCH1s.Text = item.LabelName;

                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    txtMYSYSNAME2h.Text = lblMYSYSNAME2h.Text = lblhMYSYSNAME.Text = item.LabelName;
                else if (lblSYSDESC12h.ID == item.LabelID)
                    txtSYSDESC12h.Text = lblSYSDESC12h.Text = lblhSYSDESC1.Text = item.LabelName;
                else if (lblSYSDESC22h.ID == item.LabelID)
                    txtSYSDESC22h.Text = lblSYSDESC22h.Text = item.LabelName;
                else if (lblSYSDESC32h.ID == item.LabelID)
                    txtSYSDESC32h.Text = lblSYSDESC32h.Text = item.LabelName;
                else if (lblSHORTNAME2h.ID == item.LabelID)
                    txtSHORTNAME2h.Text = lblSHORTNAME2h.Text = lblhSHORTNAME.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = lblhREMARKS.Text = item.LabelName;
                else if (lblSTARTDATE2h.ID == item.LabelID)
                    txtSTARTDATE2h.Text = lblSTARTDATE2h.Text = lblhSTARTDATE.Text = item.LabelName;
                else if (lblSYSDESC2h.ID == item.LabelID)
                    txtSYSDESC2h.Text = lblSYSDESC2h.Text = item.LabelName;
                else if (lblSYSDESCO2h.ID == item.LabelID)
                    txtSYSDESCO2h.Text = lblSYSDESCO2h.Text = item.LabelName;
                else if (lblSYSDESCCH2h.ID == item.LabelID)
                    txtSYSDESCCH2h.Text = lblSYSDESCCH2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLSYSTEMS").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLSYSTEMS.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLSYSTEMS").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblMYSYSNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME1s.Text;
                else if (lblSYSDESC11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC11s.Text;
                else if (lblSYSDESC21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC21s.Text;
                else if (lblSYSDESC31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC31s.Text;
                else if (lblSHORTNAME1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSHORTNAME1s.Text;
                else if (lblREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS1s.Text;
                else if (lblSTARTDATE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSTARTDATE1s.Text;
                else if (lblSYSDESC1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC1s.Text;
                else if (lblSYSDESCO1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESCO1s.Text;
                else if (lblSYSDESCCH1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESCCH1s.Text;

                else if (lblMYSYSNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMYSYSNAME2h.Text;
                else if (lblSYSDESC12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC12h.Text;
                else if (lblSYSDESC22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC22h.Text;
                else if (lblSYSDESC32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC32h.Text;
                else if (lblSHORTNAME2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSHORTNAME2h.Text;
                else if (lblREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtREMARKS2h.Text;
                else if (lblSTARTDATE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSTARTDATE2h.Text;
                else if (lblSYSDESC2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESC2h.Text;
                else if (lblSYSDESCO2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESCO2h.Text;
                else if (lblSYSDESCCH2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSYSDESCCH2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLSYSTEMS.xml"));

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
            //navigation.Visible = false;
            //drpMYID.Enabled = true;
            txtMYSYSNAME.Enabled = true;
            txtSYSDESC1.Enabled = true;
            txtSYSDESC2.Enabled = true;
            txtSYSDESC3.Enabled = true;
            txtSHORTNAME.Enabled = true;
            txtREMARKS.Enabled = true;
            txtSTARTDATE.Enabled = true;
            //txtCRUP_ID.Enabled = true;
            //txtACTIVE.Enabled = true;
            txtSYSDESC.Enabled = true;
            txtSYSDESCO.Enabled = true;
            txtSYSDESCCH.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpMYID.Enabled = false;
            txtMYSYSNAME.Enabled = false;
            txtSYSDESC1.Enabled = false;
            txtSYSDESC2.Enabled = false;
            txtSYSDESC3.Enabled = false;
            txtSHORTNAME.Enabled = false;
            txtREMARKS.Enabled = false;
            txtSTARTDATE.Enabled = false;
            //txtCRUP_ID.Enabled = false;
            //txtACTIVE.Enabled = false;
            txtSYSDESC.Enabled = false;
            txtSYSDESCO.Enabled = false;
            txtSYSDESCCH.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLSYSTEMS.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSYSTEMS.OrderBy(m => m.SystemID).Take(take).Skip(Skip)).ToList());
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

            ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.TBLSYSTEMS.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSYSTEMS.OrderBy(m => m.SystemID).Take(take).Skip(Skip)).ToList());
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
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.TBLSYSTEMS.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSYSTEMS.OrderBy(m => m.SystemID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLSYSTEMS.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSYSTEMS.OrderBy(m => m.SystemID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TID = Convert.ToInt32(ID[0]);
                        int MID = Convert.ToInt32(ID[1]);

                        Database.TBLSYSTEM objSOJobDesc = DB.TBLSYSTEMS.Single(p => p.TenentID == TID && p.SystemID == MID);
                        objSOJobDesc.ACTIVE = "0";
                        DB.SaveChanges();
                        BindData();
                       
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TID = Convert.ToInt32(ID[0]);
                        int MID = Convert.ToInt32(ID[1]);

                        Database.TBLSYSTEM objTBLSYSTEMS = DB.TBLSYSTEMS.Single(p => p.TenentID == TID && p.SystemID == MID);
                        //drpMYID.SelectedValue = objTBLSYSTEMS.SystemID.ToString();
                        txtMYSYSNAME.Text = objTBLSYSTEMS.MYSYSNAME.ToString();
                        txtSYSDESC1.Text = objTBLSYSTEMS.SYSDESC1.ToString();
                        txtSYSDESC2.Text = objTBLSYSTEMS.SYSDESC2.ToString();
                        txtSYSDESC3.Text = objTBLSYSTEMS.SYSDESC3.ToString();
                        txtSHORTNAME.Text = objTBLSYSTEMS.SHORTNAME.ToString();
                        txtREMARKS.Text = objTBLSYSTEMS.REMARKS.ToString();
                        txtSTARTDATE.Text = Convert.ToDateTime(objTBLSYSTEMS.STARTDATE).ToString("dd/MM/yyyy");                        
                        txtSYSDESC.Text = objTBLSYSTEMS.SYSDESC.ToString();
                        txtSYSDESCO.Text = objTBLSYSTEMS.SYSDESCO.ToString();
                        txtSYSDESCCH.Text = objTBLSYSTEMS.SYSDESCCH.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["TID"] = TID;
                        ViewState["MID"] = MID;
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

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLSYSTEMS.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSYSTEMS.OrderBy(m => m.SystemID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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