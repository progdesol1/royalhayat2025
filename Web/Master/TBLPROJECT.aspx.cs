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
    public partial class TBLPROJECT : System.Web.UI.Page
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
                btnAdd.ValidationGroup = "s";
                //bind();
                //FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            Listview1.DataSource = DB.TBLPROJECTs.Where(p => p.TenentID == TID && p.ACTIVE == true).OrderBy(m => m.PROJECTID).OrderByDescending(m=>m.PROJECTID);
            Listview1.DataBind();
            //List<Database.TBLPROJECT> List = DB.TBLPROJECTs.Where(p=>p.TenantID==TID&&p.ACTIVE==true).OrderBy(m => m.PROJECTID).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
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
        //public void bind()
        //{
        //    Listview1.DataSource = DB.TBLPROJECTs.Where(p => p.ACTIVE == true);
        //    Listview1.DataBind();
        //}
        public void GetShow()
        {

            lblPROJECTNAME11s.Attributes["class"] = lblPROJECTNAME21s.Attributes["class"] = lblPROJECTNAME31s.Attributes["class"] = lblPROJECTDESC1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPROJECTNAME12h.Attributes["class"] = lblPROJECTNAME22h.Attributes["class"] = lblPROJECTNAME32h.Attributes["class"] = lblPROJECTDESC2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPROJECTNAME11s.Attributes["class"] = lblPROJECTNAME21s.Attributes["class"] = lblPROJECTNAME31s.Attributes["class"] = lblPROJECTDESC1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPROJECTNAME12h.Attributes["class"] = lblPROJECTNAME22h.Attributes["class"] = lblPROJECTNAME32h.Attributes["class"] = lblPROJECTDESC2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drpPROJECTID.SelectedIndex = 0;
            txtPROJECTNAME1.Text = "";
            txtPROJECTNAME2.Text = "";
            txtPROJECTNAME3.Text = "";
            txtPROJECTDESC.Text = "";            
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
                        if (DB.TBLPROJECTs.Where(p => p.PROJECTNAME1.ToUpper() == txtPROJECTNAME1.Text.ToUpper() && p.TenentID == TID).Count() < 1)
                        {

                            Database.TBLPROJECT objTBLPROJECT = new Database.TBLPROJECT();
                            //Server Content Send data Yogesh
                            //objTBLPROJECT.PROJECTID = Convert.ToInt32(drpPROJECTID.SelectedValue);
                            objTBLPROJECT.TenentID = TID;
                            objTBLPROJECT.PROJECTID = DB.TBLPROJECTs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLPROJECTs.Where(p => p.TenentID == TID).Max(p => p.PROJECTID) + 1) : 1;
                            objTBLPROJECT.PROJECTNAME1 = txtPROJECTNAME1.Text;
                            objTBLPROJECT.PROJECTNAME2 = txtPROJECTNAME2.Text;
                            objTBLPROJECT.PROJECTNAME3 = txtPROJECTNAME3.Text;
                            objTBLPROJECT.PROJECTDESC = txtPROJECTDESC.Text;
                            objTBLPROJECT.CRUP_ID = 1;
                            //objTBLPROJECT.ACTIVE = cbACTIVE.Checked ? true : false;
                            objTBLPROJECT.ACTIVE = true;

                            DB.TBLPROJECTs.AddObject(objTBLPROJECT);
                            DB.SaveChanges();
                            Clear();
                            //lblMsg.Text = "  Data Save Successfully";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            BindData();
                        }
                        else 
                        {
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "This Record All Ready Exist...", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                        }
                        //navigation.Visible = true;
                        btnAdd.Text = "Add New";
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["TIDD"] != null && ViewState["PID"] != null)
                        {                           
                            int tidd = Convert.ToInt32(ViewState["TIDD"]);
                            int pid = Convert.ToInt32(ViewState["PID"]);
                            //int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.TBLPROJECT objTBLPROJECT = DB.TBLPROJECTs.Single(p => p.TenentID == tidd && p.PROJECTID == pid);
                            //objTBLPROJECT.PROJECTID = Convert.ToInt32(drpPROJECTID.SelectedValue);
                            objTBLPROJECT.PROJECTNAME1 = txtPROJECTNAME1.Text;
                            objTBLPROJECT.PROJECTNAME2 = txtPROJECTNAME2.Text;
                            objTBLPROJECT.PROJECTNAME3 = txtPROJECTNAME3.Text;
                            objTBLPROJECT.PROJECTDESC = txtPROJECTDESC.Text;
                            //objTBLPROJECT.CRUP_ID = txtCRUP_ID.Text;
                            //objTBLPROJECT.ACTIVE = cbACTIVE.Checked ? true : false;

                            //ViewState["Edit"] = null;
                            ViewState["TIDD"] = null;
                            ViewState["PID"] = null;
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
            //drpACTIVE.Items.Insert(0, new ListItem("-- Select --", "0"));drpACTIVE.DataSource = DB.0;drpACTIVE.DataTextField = "0";drpACTIVE.DataValueField = "0";drpACTIVE.DataBind();
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
            //drpPROJECTID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPROJECTNAME1.Text = Listview1.SelectedDataKey[0].ToString();
            txtPROJECTNAME2.Text = Listview1.SelectedDataKey[1].ToString();
            txtPROJECTNAME3.Text = Listview1.SelectedDataKey[2].ToString();
            txtPROJECTDESC.Text = Listview1.SelectedDataKey[3].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //cbACTIVE.Checked = true;

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpPROJECTID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtPROJECTNAME1.Text = Listview1.SelectedDataKey[0].ToString();
                txtPROJECTNAME2.Text = Listview1.SelectedDataKey[1].ToString();
                txtPROJECTNAME3.Text = Listview1.SelectedDataKey[2].ToString();
                txtPROJECTDESC.Text = Listview1.SelectedDataKey[3].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //cbACTIVE.Checked = Listview1.SelectedDataKey[0].ToString();

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
                //drpPROJECTID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtPROJECTNAME1.Text = Listview1.SelectedDataKey[0].ToString();
                txtPROJECTNAME2.Text = Listview1.SelectedDataKey[1].ToString();
                txtPROJECTNAME3.Text = Listview1.SelectedDataKey[2].ToString();
                txtPROJECTDESC.Text = Listview1.SelectedDataKey[3].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
                //cbACTIVE.Checked = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpPROJECTID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtPROJECTNAME1.Text = Listview1.SelectedDataKey[0].ToString();
            txtPROJECTNAME2.Text = Listview1.SelectedDataKey[1].ToString();
            txtPROJECTNAME3.Text = Listview1.SelectedDataKey[2].ToString();
            txtPROJECTDESC.Text = Listview1.SelectedDataKey[3].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
            //cbACTIVE.Checked = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblPROJECTNAME12h.Visible = lblPROJECTNAME22h.Visible = lblPROJECTNAME32h.Visible = lblPROJECTDESC2h.Visible = false;
                    //2true
                    txtPROJECTNAME12h.Visible = txtPROJECTNAME22h.Visible = txtPROJECTNAME32h.Visible = txtPROJECTDESC2h.Visible = true;

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
                    lblPROJECTNAME12h.Visible = lblPROJECTNAME22h.Visible = lblPROJECTNAME32h.Visible = lblPROJECTDESC2h.Visible = true;
                    //2false
                    txtPROJECTNAME12h.Visible = txtPROJECTNAME22h.Visible = txtPROJECTNAME32h.Visible = txtPROJECTDESC2h.Visible = false;

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
                    lblPROJECTNAME11s.Visible = lblPROJECTNAME21s.Visible = lblPROJECTNAME31s.Visible = lblPROJECTDESC1s.Visible = false;
                    //1true
                    txtPROJECTNAME11s.Visible = txtPROJECTNAME21s.Visible = txtPROJECTNAME31s.Visible = txtPROJECTDESC1s.Visible = true;
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
                    lblPROJECTNAME11s.Visible = lblPROJECTNAME21s.Visible = lblPROJECTNAME31s.Visible = lblPROJECTDESC1s.Visible = true;
                    //1false
                    txtPROJECTNAME11s.Visible = txtPROJECTNAME21s.Visible = txtPROJECTNAME31s.Visible = txtPROJECTDESC1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLPROJECT").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblPROJECTNAME11s.ID == item.LabelID)
                    txtPROJECTNAME11s.Text = lblPROJECTNAME11s.Text = lblhPROJECTNAME1.Text = item.LabelName;
                else if (lblPROJECTNAME21s.ID == item.LabelID)
                    txtPROJECTNAME21s.Text = lblPROJECTNAME21s.Text = lblhPROJECTNAME2.Text = item.LabelName;
                else if (lblPROJECTNAME31s.ID == item.LabelID)
                    txtPROJECTNAME31s.Text = lblPROJECTNAME31s.Text = lblhPROJECTNAME3.Text = item.LabelName;
                else if (lblPROJECTDESC1s.ID == item.LabelID)
                    txtPROJECTDESC1s.Text = lblPROJECTDESC1s.Text = lblhPROJECTDESC.Text = item.LabelName;
               

                else if (lblPROJECTNAME12h.ID == item.LabelID)
                    txtPROJECTNAME12h.Text = lblPROJECTNAME12h.Text = lblhPROJECTNAME1.Text = item.LabelName;
                else if (lblPROJECTNAME22h.ID == item.LabelID)
                    txtPROJECTNAME22h.Text = lblPROJECTNAME22h.Text = lblhPROJECTNAME2.Text = item.LabelName;
                else if (lblPROJECTNAME32h.ID == item.LabelID)
                    txtPROJECTNAME32h.Text = lblPROJECTNAME32h.Text = lblhPROJECTNAME3.Text = item.LabelName;
                else if (lblPROJECTDESC2h.ID == item.LabelID)
                    txtPROJECTDESC2h.Text = lblPROJECTDESC2h.Text = lblhPROJECTDESC.Text = item.LabelName;
               

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLPROJECT").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLPROJECT.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLPROJECT").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblPROJECTNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTNAME11s.Text;
                else if (lblPROJECTNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTNAME21s.Text;
                else if (lblPROJECTNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTNAME31s.Text;
                else if (lblPROJECTDESC1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTDESC1s.Text;
               

                else if (lblPROJECTNAME12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTNAME12h.Text;
                else if (lblPROJECTNAME22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTNAME22h.Text;
                else if (lblPROJECTNAME32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTNAME32h.Text;
                else if (lblPROJECTDESC2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPROJECTDESC2h.Text;
              

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLPROJECT.xml"));

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
            //drpPROJECTID.Enabled = true;
            txtPROJECTNAME1.Enabled = true;
            txtPROJECTNAME2.Enabled = true;
            txtPROJECTNAME3.Enabled = true;
            txtPROJECTDESC.Enabled = true;
           
        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpPROJECTID.Enabled = false;
            txtPROJECTNAME1.Enabled = false;
            txtPROJECTNAME2.Enabled = false;
            txtPROJECTNAME3.Enabled = false;
            txtPROJECTDESC.Enabled = false;
            
        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.TBLPROJECTs.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLPROJECTs.OrderBy(m => m.PROJECTID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.TBLPROJECTs.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLPROJECTs.OrderBy(m => m.PROJECTID).Take(take).Skip(Skip)).ToList());
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
                int Totalrec = DB.TBLPROJECTs.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLPROJECTs.OrderBy(m => m.PROJECTID).Take(take).Skip(Skip)).ToList());
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
            int Totalrec = DB.TBLPROJECTs.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLPROJECTs.OrderBy(m => m.PROJECTID).Take(take).Skip(Skip)).ToList());
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
            //FirstData();
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
                        int TIDD = Convert.ToInt32(ID[0]);
                        int PID = Convert.ToInt32(ID[1]);
                        Database.TBLPROJECT objSOJobDesc = DB.TBLPROJECTs.Single(p => p.TenentID == TIDD && p.PROJECTID == PID);


                        objSOJobDesc.ACTIVE = false;
                        DB.SaveChanges();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLPROJECTs.OrderBy(m => m.PROJECTID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int PID = Convert.ToInt32(ID[1]);
                        Database.TBLPROJECT objTBLPROJECT = DB.TBLPROJECTs.Single(p => p.TenentID == TIDD && p.PROJECTID == PID);

                        //drpPROJECTID.SelectedValue = objTBLPROJECT.PROJECTID.ToString();
                        txtPROJECTNAME1.Text = objTBLPROJECT.PROJECTNAME1.ToString();
                        txtPROJECTNAME2.Text = objTBLPROJECT.PROJECTNAME2.ToString();
                        txtPROJECTNAME3.Text = objTBLPROJECT.PROJECTNAME3.ToString();
                        txtPROJECTDESC.Text = objTBLPROJECT.PROJECTDESC.ToString();
                       
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["TIDD"] = TIDD;
                        ViewState["PID"] = PID;
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
            int Totalrec = DB.TBLPROJECTs.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.TBLPROJECTs.OrderBy(m => m.PROJECTID).Take(Tvalue).Skip(Svalue)).ToList());
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