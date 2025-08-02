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
    public partial class Tbl_Event_Main : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
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
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.Tbl_Event_Main> List = DB.Tbl_Event_Main.Where(m => m.TenantID == TID).OrderBy(m => m.EventID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion
        public void GetShow()
        {

            lblEventID1s.Attributes["class"] = lblTitle2Print1s.Attributes["class"] = lblFromDate1s.Attributes["class"] = lblToDate1s.Attributes["class"] = lblRegURL1s.Attributes["class"] = "control-label col-md-4  getshow"; //lblActivated1s.Attributes["class"] = lblActivatedBy1s.Attributes["class"] =
            lblEventID2h.Attributes["class"] = lblTitle2Print2h.Attributes["class"] = lblFromDate2h.Attributes["class"] = lblToDate2h.Attributes["class"] = lblRegURL2h.Attributes["class"] = "control-label col-md-4  gethide"; //lblActivated2h.Attributes["class"] = lblActivatedBy2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblEventID1s.Attributes["class"] = lblTitle2Print1s.Attributes["class"] = lblFromDate1s.Attributes["class"] = lblToDate1s.Attributes["class"] = lblRegURL1s.Attributes["class"] = "control-label col-md-4  gethide";// lblActivated1s.Attributes["class"] = lblActivatedBy1s.Attributes["class"] = 
            lblEventID2h.Attributes["class"] = lblTitle2Print2h.Attributes["class"] = lblFromDate2h.Attributes["class"] = lblToDate2h.Attributes["class"] = lblRegURL2h.Attributes["class"] = "control-label col-md-4  getshow";//lblActivated2h.Attributes["class"] = lblActivatedBy2h.Attributes["class"] =
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
            //drpEventID.SelectedIndex = 0;
            txtTitle2Print.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //txtRegURL.Text = "";
            //txtDeleted.Text = "";
            //cbActivated.Checked = false;
            //txtActivatedDate.Text = "";
            //txtActivatedBy.Text = "";
            //drpCreatedBy.SelectedIndex = 0;
            //txtCreatedDate.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //try
            //{
            string url = "";
            if (btnAdd.Text == "Add New")
            {
                pnlSuccessMsg.Visible = false;
                txtEventID.Text = DB.Tbl_Event_Main.Where(p => p.TenantID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_Event_Main.Where(p => p.TenantID == TID).Max(p => p.EventID) + 1).ToString() : "1";
                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "submit";
                txtRegURL.Text = txtRegURL.Text + "?event=" + txtEventID.Text;
            }
            else if (btnAdd.Text == "Add")
            {

                if (DB.Tbl_Event_Main.Where(p => p.Title2Print.ToUpper() == txtTitle2Print.Text.ToUpper() && p.TenantID == TID).Count() < 1)
                {
                   
                    Database.Tbl_Event_Main objTbl_Event_Main = new Database.Tbl_Event_Main();
                    int IDD = DB.Tbl_Event_Main.Where(p => p.TenantID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_Event_Main.Where(p => p.TenantID == TID).Max(p => p.EventID) + 1) : 1;                    
                    objTbl_Event_Main.EventID = DB.Tbl_Event_Main.Where(p => p.TenantID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_Event_Main.Where(p => p.TenantID == TID).Max(p => p.EventID) + 1) : 1;
                    objTbl_Event_Main.TenantID = TID;
                    objTbl_Event_Main.Title2Print = txtTitle2Print.Text;
                    objTbl_Event_Main.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    objTbl_Event_Main.ToDate = Convert.ToDateTime(txtToDate.Text);
                    objTbl_Event_Main.RegURL = txtRegURL.Text;
                    //objTbl_Event_Main.Deleted = cbDeleted.Checked;
                    //objTbl_Event_Main.Activated = cbActivated.Checked;
                    //objTbl_Event_Main.ActivatedDate = Convert.ToDateTime(txtActivatedDate.Text);
                    //objTbl_Event_Main.ActivatedBy = txtActivatedBy.Text;
                    //objTbl_Event_Main.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                    //objTbl_Event_Main.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);

                    objTbl_Event_Main.Deleted = true;
                    DB.Tbl_Event_Main.AddObject(objTbl_Event_Main);
                    DB.SaveChanges();
                    Clear();
                    //lblMsg.Text = "  Data Save Successfully";
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    //btnAdd.ValidationGroup = "";
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

                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.Tbl_Event_Main objTbl_Event_Main = DB.Tbl_Event_Main.Single(p => p.EventID == ID && p.TenantID == TID);
                    objTbl_Event_Main.EventID = Convert.ToInt32(txtEventID.Text);
                    objTbl_Event_Main.Title2Print = txtTitle2Print.Text;
                    objTbl_Event_Main.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    objTbl_Event_Main.ToDate = Convert.ToDateTime(txtToDate.Text);
                    //objTbl_Event_Main.RegURL = txtRegURL.Text;
                    //objTbl_Event_Main.Deleted = cbDeleted.Checked;
                    //objTbl_Event_Main.Activated = cbActivated.Checked;
                    //objTbl_Event_Main.ActivatedDate = Convert.ToDateTime(txtActivatedDate.Text);
                    //objTbl_Event_Main.ActivatedBy = txtActivatedBy.Text;
                    //objTbl_Event_Main.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                    //objTbl_Event_Main.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);

                    ViewState["Edit"] = null;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
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

            //    scope.Complete(); //  To commit.

            //}
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCreatedDate.DataSource = DB.0;
            //drpCreatedDate.DataTextField = "0";
            //drpCreatedDate.DataValueField = "0";
            //drpCreatedDate.DataBind();
            //drpCreatedDate.Items.Insert(0, new ListItem("-- Select --", "0"));
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
        public void ManageData()
        {
            txtEventID.Text = Listview1.SelectedDataKey[0].ToString();
            txtTitle2Print.Text = Listview1.SelectedDataKey[0].ToString();
            txtFromDate.Text = Listview1.SelectedDataKey[0].ToString();
            txtToDate.Text = Listview1.SelectedDataKey[0].ToString();
            txtRegURL.Text = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtActivatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txtActivatedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            ManageData();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                ManageData();
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
                ManageData();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            ManageData();
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblEventID2h.Visible = lblTitle2Print2h.Visible = lblFromDate2h.Visible = lblToDate2h.Visible = lblRegURL2h.Visible = false; //lblActivated2h.Visible = lblActivatedBy2h.Visible =
                    //2true
                    txtEventID2h.Visible = txtTitle2Print2h.Visible = txtFromDate2h.Visible = txtToDate2h.Visible = txtRegURL2h.Visible = true; // txtActivated2h.Visible = txtActivatedBy2h.Visible =

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
                    lblEventID2h.Visible = lblTitle2Print2h.Visible = lblFromDate2h.Visible = lblToDate2h.Visible = lblRegURL2h.Visible = true; //lblActivated2h.Visible = lblActivatedBy2h.Visible =
                    //2false
                    txtEventID2h.Visible = txtTitle2Print2h.Visible = txtFromDate2h.Visible = txtToDate2h.Visible = txtRegURL2h.Visible = false;// txtActivated2h.Visible = txtActivatedBy2h.Visible =

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
                    lblEventID1s.Visible = lblTitle2Print1s.Visible = lblFromDate1s.Visible = lblToDate1s.Visible = lblRegURL1s.Visible = false; //lblActivated1s.Visible = lblActivatedBy1s.Visible =
                    //1true
                    txtEventID1s.Visible = txtTitle2Print1s.Visible = txtFromDate1s.Visible = txtToDate1s.Visible = txtRegURL1s.Visible = true; //txtActivated1s.Visible = txtActivatedBy1s.Visible = 
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
                    lblEventID1s.Visible = lblTitle2Print1s.Visible = lblFromDate1s.Visible = lblToDate1s.Visible = lblRegURL1s.Visible = true; //lblActivated1s.Visible = lblActivatedBy1s.Visible =
                    //1false
                    txtEventID1s.Visible = txtTitle2Print1s.Visible = txtFromDate1s.Visible = txtToDate1s.Visible = txtRegURL1s.Visible = false; //txtActivated1s.Visible = txtActivatedBy1s.Visible = 
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Tbl_Event_Main").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblEventID1s.ID == item.LabelID)
                    txtEventID1s.Text = lblEventID1s.Text = item.LabelName;
                else if (lblTitle2Print1s.ID == item.LabelID)
                    txtTitle2Print1s.Text = lblTitle2Print1s.Text = lblhTitle2Print.Text = item.LabelName;
                else if (lblFromDate1s.ID == item.LabelID)
                    txtFromDate1s.Text = lblFromDate1s.Text = lblhFromDate.Text = item.LabelName;
                else if (lblToDate1s.ID == item.LabelID)
                    txtToDate1s.Text = lblToDate1s.Text = lblhToDate.Text = item.LabelName;
                else if (lblRegURL1s.ID == item.LabelID)
                    txtRegURL1s.Text = lblRegURL1s.Text = lblhRegURL.Text = item.LabelName;
                //else if (lblActivated1s.ID == item.LabelID)
                //    txtActivated1s.Text = lblActivated1s.Text = lblhActivated.Text = item.LabelName;
                //else if (lblActivatedBy1s.ID == item.LabelID)
                //    txtActivatedBy1s.Text = lblActivatedBy1s.Text = lblhActivatedBy.Text = item.LabelName;

                else if (lblEventID2h.ID == item.LabelID)
                    txtEventID2h.Text = lblEventID2h.Text = item.LabelName;
                else if (lblTitle2Print2h.ID == item.LabelID)
                    txtTitle2Print2h.Text = lblTitle2Print2h.Text = lblhTitle2Print.Text = item.LabelName;
                else if (lblFromDate2h.ID == item.LabelID)
                    txtFromDate2h.Text = lblFromDate2h.Text = lblhFromDate.Text = item.LabelName;
                else if (lblToDate2h.ID == item.LabelID)
                    txtToDate2h.Text = lblToDate2h.Text = lblhToDate.Text = item.LabelName;
                else if (lblRegURL2h.ID == item.LabelID)
                    txtRegURL2h.Text = lblRegURL2h.Text = lblhRegURL.Text = item.LabelName;
                //else if (lblActivated2h.ID == item.LabelID)
                //    txtActivated2h.Text = lblActivated2h.Text = lblhActivated.Text = item.LabelName;
                //else if (lblActivatedBy2h.ID == item.LabelID)
                //    txtActivatedBy2h.Text = lblActivatedBy2h.Text = lblhActivatedBy.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("Tbl_Event_Main").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\Tbl_Event_Main.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("Tbl_Event_Main").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblEventID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventID1s.Text;
                else if (lblTitle2Print1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2Print1s.Text;
                else if (lblFromDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFromDate1s.Text;
                else if (lblToDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToDate1s.Text;
                else if (lblRegURL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegURL1s.Text;
                //else if (lblActivated1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActivated1s.Text;
                //else if (lblActivatedBy1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActivatedBy1s.Text;

                else if (lblEventID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventID2h.Text;
                else if (lblTitle2Print2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2Print2h.Text;
                else if (lblFromDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFromDate2h.Text;
                else if (lblToDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToDate2h.Text;
                else if (lblRegURL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegURL2h.Text;
                //else if (lblActivated2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActivated2h.Text;
                //else if (lblActivatedBy2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActivatedBy2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\Tbl_Event_Main.xml"));

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
            //drpEventID.Enabled = true;
            txtTitle2Print.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtRegURL.Enabled = false;
            //cbDeleted.Enabled = true;
            //cbActivated.Enabled = true;
            //txtActivatedDate.Enabled = true;
            //txtActivatedBy.Enabled = true;
            //drpCreatedBy.Enabled = true;
            //txtCreatedDate.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpEventID.Enabled = false;
            txtTitle2Print.Enabled = false;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            txtRegURL.Enabled = false;
            //cbDeleted.Enabled = false;
            //cbActivated.Enabled = false;
            //txtActivatedDate.Enabled = false;
            //txtActivatedBy.Enabled = false;
            //drpCreatedBy.Enabled = false;
            //txtCreatedDate.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.Tbl_Event_Main.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Main.Where(p => p.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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

            ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Showdata);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.Tbl_Event_Main.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Main.Where(p => p.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Showdata);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.Tbl_Event_Main.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Main.Where(p => p.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Showdata);
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
            int Totalrec = DB.Tbl_Event_Main.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Main.Where(p => p.TenantID == TID).OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Showdata);
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

            //try
            //{
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "btnDelete")
            {
                //string[] ID = e.CommandArgument.ToString().Split(',');
                //string str1 = ID[0].ToString();
                //string str2 = ID[1].ToString();
                int TenantID = 0;
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Tbl_Event_Main objSOJobDesc = DB.Tbl_Event_Main.Single(p => p.EventID == ID && p.TenantID == TenantID);
                objSOJobDesc.Deleted = false;
                DB.SaveChanges();
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                BindData();

            }

            if (e.CommandName == "btnEdit")
            {

                int ID = Convert.ToInt32(e.CommandArgument);

                Database.Tbl_Event_Main objTbl_Event_Main = DB.Tbl_Event_Main.Single(p => p.EventID == ID && p.TenantID == TID);
                txtEventID.Text = objTbl_Event_Main.EventID.ToString();
                txtTitle2Print.Text = objTbl_Event_Main.Title2Print.ToString();
                txtFromDate.Text = Convert.ToDateTime(objTbl_Event_Main.FromDate).ToShortDateString();
                txtToDate.Text = Convert.ToDateTime(objTbl_Event_Main.ToDate).ToShortDateString();
                txtRegURL.Text = objTbl_Event_Main.RegURL.ToString();
                //cbDeleted.Checked = (objTbl_Event_Main.Deleted == true) ? true : false;
                //cbActivated.Checked = (objTbl_Event_Main.Activated == true) ? true : false;
                //txtActivatedDate.Text = objTbl_Event_Main.ActivatedDate.ToString();
                //txtActivatedBy.Text = objTbl_Event_Main.ActivatedBy.ToString();
                //drpCreatedBy.SelectedValue = objTbl_Event_Main.CreatedBy.ToString();
                //txtCreatedDate.Text = objTbl_Event_Main.CreatedDate.ToString();
                if (objTbl_Event_Main.Activated == true)
                {
                    btnActivated.Text = "Deactivate";
                    btnAddEvent.Enabled = true;
                    lblActivatedStatusDate.Text = "Activated Status and Date / Time " + objTbl_Event_Main.ActivatedDate.ToString() + " by " + objTbl_Event_Main.ActivatedBy.ToString() + ". ";
                }
                else
                {
                    if (objTbl_Event_Main.Activated == false)
                    {
                        lblActivatedStatusDate.Text = "Deactivated Status and Date / Time " + objTbl_Event_Main.ActivatedDate + " by " + objTbl_Event_Main.ActivatedBy.ToString() + ". ";
                    }
                    btnActivated.Text = "Activate";
                }
                btnActivated.Enabled = true;
                btnAdd.Text = "Update";
                Label11.Text = DB.Tbl_Event_Main.Single(p => p.EventID == ID && p.TenantID == TID).Title2Print;
                Listview3.DataSource = DB.tbl_Event_Detail.Where(p => p.EventID == ID && p.TenantID == TID);
                Listview3.DataBind();

                ViewState["Edit"] = ID;
                Write();
            }

        }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
        //        throw;
        //    }
        //}


        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.Tbl_Event_Main.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.Tbl_Event_Main.Where(p => p.TenantID == TID).OrderBy(m => m.EventID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Showdata);
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

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            //if (Request.QueryString.Count>0)
            //{
            //int ID = Convert.ToInt32(Request.QueryString["EventID"]);
            //Database.Tbl_Event_Main objTbl_Event_Main = DB.Tbl_Event_Main.Single(p => p.EventID == ID && p.TenantID == TenantID);
            // txtTitle2Print.Text = objTbl_Event_Main.Title2Print.ToString();
            // DB.SaveChanges();
            if (ViewState["Edit"] != null)
            {
                int MEID = Convert.ToInt32(ViewState["Edit"]);
                Response.Redirect("tbl_Event_Detail.aspx?MainEventID=" + MEID);
            }

            //}
        }

        protected void btnActivated_Click(object sender, EventArgs e)
        {
            if (ViewState["Edit"] != null)
            {
                int ID = Convert.ToInt32(ViewState["Edit"]);
                Database.Tbl_Event_Main objTbl_Event_Main = DB.Tbl_Event_Main.Single(p => p.EventID == ID && p.TenantID == TID);
                objTbl_Event_Main.Activated = true;
                objTbl_Event_Main.ActivatedDate = DateTime.Now;
                objTbl_Event_Main.ActivatedBy = ((USER_MST)Session["USER"]).FIRST_NAME;
                if (btnActivated.Text == "Activate")
                {
                    btnActivated.Text = "Deactivate";
                    btnAddEvent.Enabled = true;
                    lblActivatedStatusDate.Text = "Activated Status and Date / Time " + DateTime.Now + " by " + ((USER_MST)Session["USER"]).FIRST_NAME + ". ";
                    BindData();
                }
                else
                {
                    btnAddEvent.Enabled = false;
                    btnActivated.Text = "Activate";
                    lblActivatedStatusDate.Text = "Deactivated Status and Date / Time " + DateTime.Now + " by " + ((USER_MST)Session["USER"]).FIRST_NAME + ". ";
                    BindData();
                }
                DB.SaveChanges();

            }

        }

        protected void Listview3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton btnRegister = (LinkButton)e.Item.FindControl("btnRegister");
                Label lblhideMyID = (Label)e.Item.FindControl("lblhideMyID");
                Label lblhideEventID = (Label)e.Item.FindControl("lblhideEventID");

                int SEID = Convert.ToInt32(lblhideMyID.Text);
                int MEID = Convert.ToInt32(lblhideEventID.Text);
                if (DB.tbl_Event_Detail.Where(a => a.MyID == SEID && a.EventID == MEID && a.Activated == true && a.TenantID == TID).Count() > 0)
                {
                    btnRegister.Visible = true;
                }
                else
                {
                    btnRegister.Visible = false;
                }
            }
        }

        public string getEventType(int ID)
        {
            //Classes.EcommAdminClass.getdropdown(drpEventType, TID, "EVN", "EventType", "", "REFTABLE");
            //Classes.EcommAdminClass.getdropdown(drpEventTopic, TID, "EVN", "EventTopic", "", "REFTABLE");
            return Classes.EcommAdminClass.getDataREFTABLE(TID).Single(P => P.REFTYPE == "EVN" && P.REFSUBTYPE == "EventType" && P.REFID == ID).REFNAME1;
        }
        public string getEventTopic(int ID)
        {
            return Classes.EcommAdminClass.getDataREFTABLE(TID).Single(P => P.REFTYPE == "EVN" && P.REFSUBTYPE == "EventTopic" && P.REFID == ID).REFNAME1;
        }
    }
}