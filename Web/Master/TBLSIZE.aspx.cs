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
    public partial class TBLSIZE : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Session["LANGUAGE"] = "en-US";
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "s";
            }
        }
        #region Step2
        public void BindData()
        {
            //List<Database.TBLSIZE> List = DB.TBLSIZEs.Where(p=>p.ACTIVE == "Y"&&p.TENANTID==TID).OrderBy(m => m.SIZECODE).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
            Listview1.DataSource = DB.TBLSIZEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID).OrderBy(m => m.SIZECODE).OrderByDescending(m=>m.SIZECODE);
            Listview1.DataBind();
        }
        #endregion
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((AcmMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
        }

        #region PAge Genarator
        public void GetShow()
        {

            lblSIZETYPE1s.Attributes["class"] = lblSIZEDESC11s.Attributes["class"] = lblSIZEDESC21s.Attributes["class"] = lblSIZEDESC31s.Attributes["class"] = lblSIZEREMARKS1s.Attributes["class"] = lblACTIVE1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblSIZETYPE2h.Attributes["class"] = lblSIZEDESC12h.Attributes["class"] = lblSIZEDESC22h.Attributes["class"] = lblSIZEDESC32h.Attributes["class"] = lblSIZEREMARKS2h.Attributes["class"] = lblACTIVE2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblSIZETYPE1s.Attributes["class"] = lblSIZEDESC11s.Attributes["class"] = lblSIZEDESC21s.Attributes["class"] = lblSIZEDESC31s.Attributes["class"] = lblSIZEREMARKS1s.Attributes["class"] = lblACTIVE1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblSIZETYPE2h.Attributes["class"] = lblSIZEDESC12h.Attributes["class"] = lblSIZEDESC22h.Attributes["class"] = lblSIZEDESC32h.Attributes["class"] = lblSIZEREMARKS2h.Attributes["class"] = lblACTIVE2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpSIZECODE.SelectedIndex = 0;
            txtSIZETYPE.Text = "";
            txtSIZEDESC1.Text = "";
            txtSIZEDESC2.Text = "";
            txtSIZEDESC3.Text = "";
            txtSIZEREMARKS.Text = "";
            //drpCRUP_ID.SelectedIndex = 0;
            cbACTIVE.Checked = true;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        
                        if (DB.TBLSIZEs.Where(p => p.SIZETYPE.ToUpper() == txtSIZETYPE.Text.ToUpper() && p.TenentID == TID).Count() < 1)
                        {
                            Database.TBLSIZE objTBLSIZE = new Database.TBLSIZE();
                            //Server Content Send data Yogesh
                            objTBLSIZE.TenentID = TID;
                            objTBLSIZE.SIZECODE = DB.TBLSIZEs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLSIZEs.Where(p => p.TenentID == TID).Max(p => p.SIZECODE) + 1) : 1;
                            //objTBLSIZE.SIZECODE = Convert.ToInt32(drpSIZECODE.SelectedValue);
                            objTBLSIZE.SIZETYPE = txtSIZETYPE.Text;
                            objTBLSIZE.SIZEDESC1 = txtSIZEDESC1.Text;
                            objTBLSIZE.SIZEDESC2 = txtSIZEDESC2.Text;
                            objTBLSIZE.SIZEDESC3 = txtSIZEDESC3.Text;
                            objTBLSIZE.SIZEREMARKS = txtSIZEREMARKS.Text;
                            //objTBLSIZE.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            objTBLSIZE.ACTIVE = cbACTIVE.Checked ? "Y" : "N";


                            DB.TBLSIZEs.AddObject(objTBLSIZE);
                            DB.SaveChanges();
                            btnAdd.Text = "Add New";
                            Clear();
                            //lblMsg.Text = "  Data Save Successfully";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            BindData();
                        
                           
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                        else
                        {
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Record Is All Ready Exist...", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                        }
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["TIDD"] != null && ViewState["SID"] != null)
                        {
                            int tidd = Convert.ToInt32(ViewState["TIDD"]);
                            int sid = Convert.ToInt32(ViewState["SID"]);
                            //int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.TBLSIZE objTBLSIZE = DB.TBLSIZEs.Single(p => p.TenentID == tidd && p.SIZECODE == sid);
                            //objTBLSIZE.SIZECODE = Convert.ToInt32(drpSIZECODE.SelectedValue);
                            objTBLSIZE.SIZETYPE = txtSIZETYPE.Text;
                            objTBLSIZE.SIZEDESC1 = txtSIZEDESC1.Text;
                            objTBLSIZE.SIZEDESC2 = txtSIZEDESC2.Text;
                            objTBLSIZE.SIZEDESC3 = txtSIZEDESC3.Text;
                            objTBLSIZE.SIZEREMARKS = txtSIZEREMARKS.Text;
                            //objTBLSIZE.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            objTBLSIZE.ACTIVE = cbACTIVE.Checked ? "Y" : "N";

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();
                            Clear();
                            //lblMsg.Text = "  Data Edit Successfully";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
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
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Saving Data !<br>" + ex.ToString(), "Add", Classes.Toastr.ToastPosition.TopCenter);
                    //throw;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpACTIVE.Items.Insert(0, new ListItem("-- Select --", "0"));drpACTIVE.DataSource = DB.0;drpACTIVE.DataTextField = "0";drpACTIVE.DataValueField = "0";drpACTIVE.DataBind();
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
            //drpSIZECODE.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtSIZETYPE.Text = Listview1.SelectedDataKey[2] !=null? Listview1.SelectedDataKey[2].ToString():"";
            txtSIZEDESC1.Text = Listview1.SelectedDataKey[3] !=null? Listview1.SelectedDataKey[3].ToString():"";
            txtSIZEDESC2.Text = Listview1.SelectedDataKey[4] !=null? Listview1.SelectedDataKey[4].ToString():"";
            txtSIZEDESC3.Text = Listview1.SelectedDataKey[5] !=null? Listview1.SelectedDataKey[5].ToString():"";
            txtSIZEREMARKS.Text = Listview1.SelectedDataKey[6] !=null? Listview1.SelectedDataKey[6].ToString():"";            
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpSIZECODE.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtSIZETYPE.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
                txtSIZEDESC1.Text = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                txtSIZEDESC2.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtSIZEDESC3.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                txtSIZEREMARKS.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";              
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();

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
                //drpSIZECODE.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtSIZETYPE.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
                txtSIZEDESC1.Text = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                txtSIZEDESC2.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtSIZEDESC3.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                txtSIZEREMARKS.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";                 
                //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpSIZECODE.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtSIZETYPE.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
            txtSIZEDESC1.Text = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
            txtSIZEDESC2.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
            txtSIZEDESC3.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
            txtSIZEREMARKS.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";            
            //txtACTIVE.Text = Listview1.SelectedDataKey[0].ToString();

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
                    lblSIZETYPE2h.Visible = lblSIZEDESC12h.Visible = lblSIZEDESC22h.Visible = lblSIZEDESC32h.Visible = lblSIZEREMARKS2h.Visible = lblACTIVE2h.Visible = false;
                    //2true
                    txtSIZETYPE2h.Visible = txtSIZEDESC12h.Visible = txtSIZEDESC22h.Visible = txtSIZEDESC32h.Visible = txtSIZEREMARKS2h.Visible = txtACTIVE2h.Visible = true;

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
                    lblSIZETYPE2h.Visible = lblSIZEDESC12h.Visible = lblSIZEDESC22h.Visible = lblSIZEDESC32h.Visible = lblSIZEREMARKS2h.Visible = lblACTIVE2h.Visible = true;
                    //2false
                    txtSIZETYPE2h.Visible = txtSIZEDESC12h.Visible = txtSIZEDESC22h.Visible = txtSIZEDESC32h.Visible = txtSIZEREMARKS2h.Visible = txtACTIVE2h.Visible = false;

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
                    lblSIZETYPE1s.Visible = lblSIZEDESC11s.Visible = lblSIZEDESC21s.Visible = lblSIZEDESC31s.Visible = lblSIZEREMARKS1s.Visible = lblACTIVE1s.Visible = false;
                    //1true
                    txtSIZETYPE1s.Visible = txtSIZEDESC11s.Visible = txtSIZEDESC21s.Visible = txtSIZEDESC31s.Visible = txtSIZEREMARKS1s.Visible = txtACTIVE1s.Visible = true;
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
                    lblSIZETYPE1s.Visible = lblSIZEDESC11s.Visible = lblSIZEDESC21s.Visible = lblSIZEDESC31s.Visible = lblSIZEREMARKS1s.Visible = lblACTIVE1s.Visible = true;
                    //1false
                    txtSIZETYPE1s.Visible = txtSIZEDESC11s.Visible = txtSIZEDESC21s.Visible = txtSIZEDESC31s.Visible = txtSIZEREMARKS1s.Visible = txtACTIVE1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLSIZE").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTENANTID1s.ID == item.LabelID)
                //    txtTENANTID1s.Text = lblTENANTID1s.Text = item.LabelName;
                //else if (lblSIZECODE1s.ID == item.LabelID)
                //    txtSIZECODE1s.Text = lblSIZECODE1s.Text = item.LabelName;
                if (lblSIZETYPE1s.ID == item.LabelID)
                    txtSIZETYPE1s.Text = lblSIZETYPE1s.Text = lblhSIZETYPE.Text = item.LabelName;
                else if (lblSIZEDESC11s.ID == item.LabelID)
                    txtSIZEDESC11s.Text = lblSIZEDESC11s.Text = lblhSIZEDESC1.Text = item.LabelName;
                else if (lblSIZEDESC21s.ID == item.LabelID)
                    txtSIZEDESC21s.Text = lblSIZEDESC21s.Text = lblhSIZEDESC2.Text = item.LabelName;
                else if (lblSIZEDESC31s.ID == item.LabelID)
                    txtSIZEDESC31s.Text = lblSIZEDESC31s.Text = lblhSIZEDESC3.Text = item.LabelName;
                else if (lblSIZEREMARKS1s.ID == item.LabelID)
                    txtSIZEREMARKS1s.Text = lblSIZEREMARKS1s.Text = item.LabelName;
                
                else if (lblACTIVE1s.ID == item.LabelID)
                    txtACTIVE1s.Text = lblACTIVE1s.Text = item.LabelName;

                //else if (lblTENANTID2h.ID == item.LabelID)
                //    txtTENANTID2h.Text = lblTENANTID2h.Text = item.LabelName;
                //else if (lblSIZECODE2h.ID == item.LabelID)
                //    txtSIZECODE2h.Text = lblSIZECODE2h.Text = item.LabelName;
                else if (lblSIZETYPE2h.ID == item.LabelID)
                    txtSIZETYPE2h.Text = lblSIZETYPE2h.Text = lblhSIZETYPE.Text = item.LabelName;
                else if (lblSIZEDESC12h.ID == item.LabelID)
                    txtSIZEDESC12h.Text = lblSIZEDESC12h.Text = lblhSIZEDESC1.Text = item.LabelName;
                else if (lblSIZEDESC22h.ID == item.LabelID)
                    txtSIZEDESC22h.Text = lblSIZEDESC22h.Text = lblhSIZEDESC2.Text = item.LabelName;
                else if (lblSIZEDESC32h.ID == item.LabelID)
                    txtSIZEDESC32h.Text = lblSIZEDESC32h.Text = lblhSIZEDESC3.Text = item.LabelName;
                else if (lblSIZEREMARKS2h.ID == item.LabelID)
                    txtSIZEREMARKS2h.Text = lblSIZEREMARKS2h.Text = item.LabelName;
                
                else if (lblACTIVE2h.ID == item.LabelID)
                    txtACTIVE2h.Text = lblACTIVE2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLSIZE").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLSIZE.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLSIZE").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTENANTID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANTID1s.Text;
                //else if (lblSIZECODE1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSIZECODE1s.Text;
                if (lblSIZETYPE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZETYPE1s.Text;
                else if (lblSIZEDESC11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEDESC11s.Text;
                else if (lblSIZEDESC21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEDESC21s.Text;
                else if (lblSIZEDESC31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEDESC31s.Text;
                else if (lblSIZEREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEREMARKS1s.Text;
              
                else if (lblACTIVE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE1s.Text;

                //else if (lblTENANTID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTENANTID2h.Text;
                //else if (lblSIZECODE2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtSIZECODE2h.Text;
                else if (lblSIZETYPE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZETYPE2h.Text;
                else if (lblSIZEDESC12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEDESC12h.Text;
                else if (lblSIZEDESC22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEDESC22h.Text;
                else if (lblSIZEDESC32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEDESC32h.Text;
                else if (lblSIZEREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSIZEREMARKS2h.Text;
              
                else if (lblACTIVE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLSIZE.xml"));

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
            //navigation.Visible = false;
            //drpSIZECODE.Enabled = true;
            txtSIZETYPE.Enabled = true;
            txtSIZEDESC1.Enabled = true;
            txtSIZEDESC2.Enabled = true;
            txtSIZEDESC3.Enabled = true;
            txtSIZEREMARKS.Enabled = true;           
            cbACTIVE.Enabled = true;
        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpSIZECODE.Enabled = false;
            txtSIZETYPE.Enabled = false;
            txtSIZEDESC1.Enabled = false;
            txtSIZEDESC2.Enabled = false;
            txtSIZEDESC3.Enabled = false;
            txtSIZEREMARKS.Enabled = false;           
            cbACTIVE.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{         
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLSIZEs.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSIZEs.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(m => m.SIZECODE).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    if (take == Totalrec && Skip == (Totalrec - Showdata))
        //        btnNext1.Enabled = false;
        //    else
        //        btnNext1.Enabled = true;
        //    if (take == Showdata && Skip == 0)
        //        btnPrevious1.Enabled = false;
        //    else
        //        btnPrevious1.Enabled = true;

        //    ChoiceID = take / Showdata;

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.TBLSIZEs.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSIZEs.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(m => m.SIZECODE).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        if (take == Showdata && Skip == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;

        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;

        //        ChoiceID = take / Showdata;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.TBLSIZEs.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSIZEs.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(m => m.SIZECODE).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLSIZEs.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSIZEs.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(m => m.SIZECODE).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            //Readonly();
            //ManageLang();
            //pnlSuccessMsg.Visible = false;
            //FillContractorID();
            //int CurrentID = 1;
            //if (ViewState["Es"] != null)
            //    CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            //FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    if (e.CommandName == "btnDelete")
                    {

                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int SID = Convert.ToInt32(ID[1]);
                        Database.TBLSIZE objTBLSIZEe = DB.TBLSIZEs.Single(p => p.TenentID == TIDD && p.SIZECODE == SID);

                        objTBLSIZEe.ACTIVE = "N";
                        DB.SaveChanges();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();                       
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int SID = Convert.ToInt32(ID[1]);
                        Database.TBLSIZE objTBLSIZE = DB.TBLSIZEs.Single(p => p.TenentID == TIDD && p.SIZECODE == SID);
                      
                        //drpSIZECODE.SelectedValue = objTBLSIZE.SIZECODE.ToString();
                        txtSIZETYPE.Text = objTBLSIZE.SIZETYPE.ToString();
                        txtSIZEDESC1.Text = objTBLSIZE.SIZEDESC1.ToString();
                        txtSIZEDESC2.Text = objTBLSIZE.SIZEDESC2.ToString();
                        txtSIZEDESC3.Text = objTBLSIZE.SIZEDESC3.ToString();
                        txtSIZEREMARKS.Text = objTBLSIZE.SIZEREMARKS.ToString();
                        //drpCRUP_ID.SelectedValue = objTBLSIZE.CRUP_ID.ToString();
                        if(objTBLSIZE.ACTIVE == "Y")
                        {
                            cbACTIVE.Checked = true;
                        }
                        else
                        {
                            cbACTIVE.Checked = false;
                        }

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["TIDD"] = TIDD;
                        ViewState["SID"] = SID;
                        Write();
                    }
                    scope.Complete(); //  To commit.
                //}
                //catch (Exception ex)
                //{
                //    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                //    throw;
                //}
            }
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLSIZEs.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLSIZEs.OrderBy(m => m.SIZECODE).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        if (Tvalue == Showdata && Svalue == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //    }
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        //}

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