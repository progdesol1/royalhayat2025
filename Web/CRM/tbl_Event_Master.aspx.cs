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

namespace Web.CRM
{
    public partial class tbl_Event_Master : System.Web.UI.Page
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
                Session["LANGUAGE"] = "en-US";
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "submit";
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.tbl_Event_Master> List = DB.tbl_Event_Master.Where(p=>p.Deleted == true).OrderBy(m => m.ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblTitle1s.Attributes["class"] = lblDescreption1s.Attributes["class"] = lblSize1s.Attributes["class"] = lblKeyWord1s.Attributes["class"] = lblExetation1s.Attributes["class"] = lblRate1s.Attributes["class"] = lblVisit1s.Attributes["class"] = lblCounterDounload1s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblAvtar1s.Attributes["class"] = lblFileUpload1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblTitle2h.Attributes["class"] = lblDescreption2h.Attributes["class"] = lblSize2h.Attributes["class"] = lblKeyWord2h.Attributes["class"] = lblExetation2h.Attributes["class"] = lblRate2h.Attributes["class"] = lblVisit2h.Attributes["class"] = lblCounterDounload2h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblAvtar2h.Attributes["class"] = lblFileUpload2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblTitle1s.Attributes["class"] = lblDescreption1s.Attributes["class"] = lblSize1s.Attributes["class"] = lblKeyWord1s.Attributes["class"] = lblExetation1s.Attributes["class"] = lblRate1s.Attributes["class"] = lblVisit1s.Attributes["class"] = lblCounterDounload1s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblAvtar1s.Attributes["class"] = lblFileUpload1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblTitle2h.Attributes["class"] = lblDescreption2h.Attributes["class"] = lblSize2h.Attributes["class"] = lblKeyWord2h.Attributes["class"] = lblExetation2h.Attributes["class"] = lblRate2h.Attributes["class"] = lblVisit2h.Attributes["class"] = lblCounterDounload2h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblAvtar2h.Attributes["class"] = lblFileUpload2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtTitle.Text = "";
            txtDescreption.Text = "";
            txtSize.Text = "";
            txtKeyWord.Text = "";
            txtExetation.Text = "";
            txtRate.Text = "";
            txtVisit.Text = "";
            txtCounterDounload.Text = "";
            drpCategoryID.SelectedIndex = 0;
            txtAvtar.Text = "";
            txtFileUpload.Text = "";
            //txtDeleted.Text = "";
            //txtActivated.Text = "";
            //drpCreatedBy.SelectedIndex = 0;
            //txtCreatedDate.Text = "";
            //txturlpath.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        //p.Visible = false;
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        
                        Database.tbl_Event_Master objtbl_Event_Master = new Database.tbl_Event_Master();
                        //Server Content Send data Yogesh
                        objtbl_Event_Master.Title = txtTitle.Text;
                        objtbl_Event_Master.Descreption = txtDescreption.Text;
                        objtbl_Event_Master.Size = txtSize.Text;
                        objtbl_Event_Master.KeyWord = txtKeyWord.Text;
                        objtbl_Event_Master.Exetation = txtExetation.Text;
                        objtbl_Event_Master.Rate = txtRate.Text;
                        objtbl_Event_Master.Visit = Convert.ToInt32(txtVisit.Text);
                        objtbl_Event_Master.CounterDounload = Convert.ToInt32(txtCounterDounload.Text);
                        objtbl_Event_Master.CategoryID = Convert.ToInt32(drpCategoryID.SelectedValue);
                        objtbl_Event_Master.Avtar = txtAvtar.Text;
                        objtbl_Event_Master.FileUpload = txtFileUpload.Text;
                        objtbl_Event_Master.Deleted = true;
                        objtbl_Event_Master.Activated = true;
                        objtbl_Event_Master.CreatedBy = 1;
                        objtbl_Event_Master.CreatedDate = DateTime.Now;
                        //objtbl_Event_Master.urlpath = txturlpath.Text;


                        DB.tbl_Event_Master.AddObject(objtbl_Event_Master);
                        DB.SaveChanges();
                        Clear();
                        //lblMsg.Text = "  Data Save Successfully";
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        btnAdd.Text = "Add New";
                        //pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.tbl_Event_Master objtbl_Event_Master = DB.tbl_Event_Master.Single(p => p.ID == ID && p.Deleted == true);
                            objtbl_Event_Master.Title = txtTitle.Text;
                            objtbl_Event_Master.Descreption = txtDescreption.Text;
                            objtbl_Event_Master.Size = txtSize.Text;
                            objtbl_Event_Master.KeyWord = txtKeyWord.Text;
                            objtbl_Event_Master.Exetation = txtExetation.Text;
                            objtbl_Event_Master.Rate = txtRate.Text;
                            objtbl_Event_Master.Visit = Convert.ToInt32(txtVisit.Text);
                            objtbl_Event_Master.CounterDounload = Convert.ToInt32(txtCounterDounload.Text);
                            objtbl_Event_Master.CategoryID = Convert.ToInt32(drpCategoryID.SelectedValue);
                            objtbl_Event_Master.Avtar = txtAvtar.Text;
                            objtbl_Event_Master.FileUpload = txtFileUpload.Text;
                            //objtbl_Event_Master.Deleted = cbDeleted.Checked;
                            //objtbl_Event_Master.Activated = cbActivated.Checked;
                            //objtbl_Event_Master.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                            //objtbl_Event_Master.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
                            //objtbl_Event_Master.urlpath = txturlpath.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();
                            Clear();
                            //lblMsg.Text = "  Data Edit Successfully";
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Info, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            //pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                            btnAdd.ValidationGroup = "submit";
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            Clear();
            btnAdd.Text = "Add New";
            BindData();
            Readonly();
        }

        public string gatcategory(int id)
        {
            string CatName = "";
            List<Database.tbl_Category_Master> catlist = DB.tbl_Category_Master.Where(p => p.ID == id).ToList();
            if(catlist.Count() > 0)
            {
                CatName = catlist.Single(p => p.ID == id).CategoryName.ToString();
                return CatName;
            }
            else
            {
                return "Not Found";
            }
                
        }
        public void FillContractorID()
        {
            //drpurlpath.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpurlpath.DataSource = DB.0;
            //drpurlpath.DataTextField = "0";
            //drpurlpath.DataValueField = "0";
            //drpurlpath.DataBind();

            drpCategoryID.DataSource = DB.tbl_Category_Master;
            drpCategoryID.DataTextField = "CategoryName";
            drpCategoryID.DataValueField = "ID";
            drpCategoryID.DataBind();
            drpCategoryID.Items.Insert(0, new ListItem("--- Select---", "0"));
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            //FirstData();
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
            txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescreption.Text = Listview1.SelectedDataKey[0].ToString();
            txtSize.Text = Listview1.SelectedDataKey[0].ToString();
            txtKeyWord.Text = Listview1.SelectedDataKey[0].ToString();
            txtExetation.Text = Listview1.SelectedDataKey[0].ToString();
            txtRate.Text = Listview1.SelectedDataKey[0].ToString();
            txtVisit.Text = Listview1.SelectedDataKey[0].ToString();
            txtCounterDounload.Text = Listview1.SelectedDataKey[0].ToString();
            drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
            txtFileUpload.Text = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txturlpath.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescreption.Text = Listview1.SelectedDataKey[0].ToString();
                txtSize.Text = Listview1.SelectedDataKey[0].ToString();
                txtKeyWord.Text = Listview1.SelectedDataKey[0].ToString();
                txtExetation.Text = Listview1.SelectedDataKey[0].ToString();
                txtRate.Text = Listview1.SelectedDataKey[0].ToString();
                txtVisit.Text = Listview1.SelectedDataKey[0].ToString();
                txtCounterDounload.Text = Listview1.SelectedDataKey[0].ToString();
                drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
                txtFileUpload.Text = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txturlpath.Text = Listview1.SelectedDataKey[0].ToString();

            }

        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Info, "This is first record", "Info!", Classes.Toastr.ToastPosition.TopCenter);
                //lblMsg.Text = "This is first record";
                //pnlSuccessMsg.Visible = true;

            }
            else
            {
                //pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescreption.Text = Listview1.SelectedDataKey[0].ToString();
                txtSize.Text = Listview1.SelectedDataKey[0].ToString();
                txtKeyWord.Text = Listview1.SelectedDataKey[0].ToString();
                txtExetation.Text = Listview1.SelectedDataKey[0].ToString();
                txtRate.Text = Listview1.SelectedDataKey[0].ToString();
                txtVisit.Text = Listview1.SelectedDataKey[0].ToString();
                txtCounterDounload.Text = Listview1.SelectedDataKey[0].ToString();
                drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
                txtFileUpload.Text = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txturlpath.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescreption.Text = Listview1.SelectedDataKey[0].ToString();
            txtSize.Text = Listview1.SelectedDataKey[0].ToString();
            txtKeyWord.Text = Listview1.SelectedDataKey[0].ToString();
            txtExetation.Text = Listview1.SelectedDataKey[0].ToString();
            txtRate.Text = Listview1.SelectedDataKey[0].ToString();
            txtVisit.Text = Listview1.SelectedDataKey[0].ToString();
            txtCounterDounload.Text = Listview1.SelectedDataKey[0].ToString();
            drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
            txtFileUpload.Text = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txturlpath.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblTitle2h.Visible = lblDescreption2h.Visible = lblSize2h.Visible = lblKeyWord2h.Visible = lblExetation2h.Visible = lblRate2h.Visible = lblVisit2h.Visible = lblCounterDounload2h.Visible = lblCategoryID2h.Visible = lblAvtar2h.Visible = lblFileUpload2h.Visible = false;
                    //2true
                    txtTitle2h.Visible = txtDescreption2h.Visible = txtSize2h.Visible = txtKeyWord2h.Visible = txtExetation2h.Visible = txtRate2h.Visible = txtVisit2h.Visible = txtCounterDounload2h.Visible = txtCategoryID2h.Visible = txtAvtar2h.Visible = txtFileUpload2h.Visible = true;

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
                    lblTitle2h.Visible = lblDescreption2h.Visible = lblSize2h.Visible = lblKeyWord2h.Visible = lblExetation2h.Visible = lblRate2h.Visible = lblVisit2h.Visible = lblCounterDounload2h.Visible = lblCategoryID2h.Visible = lblAvtar2h.Visible = lblFileUpload2h.Visible = true;
                    //2false
                    txtTitle2h.Visible = txtDescreption2h.Visible = txtSize2h.Visible = txtKeyWord2h.Visible = txtExetation2h.Visible = txtRate2h.Visible = txtVisit2h.Visible = txtCounterDounload2h.Visible = txtCategoryID2h.Visible = txtAvtar2h.Visible = txtFileUpload2h.Visible = false;

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
                    lblTitle1s.Visible = lblDescreption1s.Visible = lblSize1s.Visible = lblKeyWord1s.Visible = lblExetation1s.Visible = lblRate1s.Visible = lblVisit1s.Visible = lblCounterDounload1s.Visible = lblCategoryID1s.Visible = lblAvtar1s.Visible = lblFileUpload1s.Visible = false;
                    //1true
                    txtTitle1s.Visible = txtDescreption1s.Visible = txtSize1s.Visible = txtKeyWord1s.Visible = txtExetation1s.Visible = txtRate1s.Visible = txtVisit1s.Visible = txtCounterDounload1s.Visible = txtCategoryID1s.Visible = txtAvtar1s.Visible = txtFileUpload1s.Visible = true;
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
                    lblTitle1s.Visible = lblDescreption1s.Visible = lblSize1s.Visible = lblKeyWord1s.Visible = lblExetation1s.Visible = lblRate1s.Visible = lblVisit1s.Visible = lblCounterDounload1s.Visible = lblCategoryID1s.Visible = lblAvtar1s.Visible = lblFileUpload1s.Visible = true;
                    //1false
                    txtTitle1s.Visible = txtDescreption1s.Visible = txtSize1s.Visible = txtKeyWord1s.Visible = txtExetation1s.Visible = txtRate1s.Visible = txtVisit1s.Visible = txtCounterDounload1s.Visible = txtCategoryID1s.Visible = txtAvtar1s.Visible = txtFileUpload1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((CRMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("tbl_Event_Master").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblTitle1s.ID == item.LabelID)
                    txtTitle1s.Text = lblTitle1s.Text = lblhTitle.Text = item.LabelName;
                else if (lblDescreption1s.ID == item.LabelID)
                    txtDescreption1s.Text = lblDescreption1s.Text = lblhDescreption.Text = item.LabelName;
                else if (lblSize1s.ID == item.LabelID)
                    txtSize1s.Text = lblSize1s.Text = item.LabelName;
                else if (lblKeyWord1s.ID == item.LabelID)
                    txtKeyWord1s.Text = lblKeyWord1s.Text = item.LabelName;
                else if (lblExetation1s.ID == item.LabelID)
                    txtExetation1s.Text = lblExetation1s.Text = item.LabelName;
                else if (lblRate1s.ID == item.LabelID)
                    txtRate1s.Text = lblRate1s.Text = item.LabelName;
                else if (lblVisit1s.ID == item.LabelID)
                    txtVisit1s.Text = lblVisit1s.Text = lblhVisit.Text = item.LabelName;
                else if (lblCounterDounload1s.ID == item.LabelID)
                    txtCounterDounload1s.Text = lblCounterDounload1s.Text = lblhCounterDounload.Text = item.LabelName;
                else if (lblCategoryID1s.ID == item.LabelID)
                    txtCategoryID1s.Text = lblCategoryID1s.Text = lblhCategoryID.Text = item.LabelName;
                else if (lblAvtar1s.ID == item.LabelID)
                    txtAvtar1s.Text = lblAvtar1s.Text = item.LabelName;
                else if (lblFileUpload1s.ID == item.LabelID)
                    txtFileUpload1s.Text = lblFileUpload1s.Text = item.LabelName;

                else if (lblTitle2h.ID == item.LabelID)
                    txtTitle2h.Text = lblTitle2h.Text = lblhTitle.Text = item.LabelName;
                else if (lblDescreption2h.ID == item.LabelID)
                    txtDescreption2h.Text = lblDescreption2h.Text = lblhDescreption.Text = item.LabelName;
                else if (lblSize2h.ID == item.LabelID)
                    txtSize2h.Text = lblSize2h.Text = item.LabelName;
                else if (lblKeyWord2h.ID == item.LabelID)
                    txtKeyWord2h.Text = lblKeyWord2h.Text = item.LabelName;
                else if (lblExetation2h.ID == item.LabelID)
                    txtExetation2h.Text = lblExetation2h.Text = item.LabelName;
                else if (lblRate2h.ID == item.LabelID)
                    txtRate2h.Text = lblRate2h.Text = item.LabelName;
                else if (lblVisit2h.ID == item.LabelID)
                    txtVisit2h.Text = lblVisit2h.Text = lblhVisit.Text = item.LabelName;
                else if (lblCounterDounload2h.ID == item.LabelID)
                    txtCounterDounload2h.Text = lblCounterDounload2h.Text = lblhCounterDounload.Text = item.LabelName;
                else if (lblCategoryID2h.ID == item.LabelID)
                    txtCategoryID2h.Text = lblCategoryID2h.Text = lblhCategoryID.Text = item.LabelName;
                else if (lblAvtar2h.ID == item.LabelID)
                    txtAvtar2h.Text = lblAvtar2h.Text = item.LabelName;
                else if (lblFileUpload2h.ID == item.LabelID)
                    txtFileUpload2h.Text = lblFileUpload2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = LableList.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("tbl_Event_Master").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\tbl_Event_Master.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((CRMMaster)this.Master).Bindxml("tbl_Event_Master").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle1s.Text;
                else if (lblDescreption1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescreption1s.Text;
                else if (lblSize1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSize1s.Text;
                else if (lblKeyWord1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeyWord1s.Text;
                else if (lblExetation1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtExetation1s.Text;
                else if (lblRate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRate1s.Text;
                else if (lblVisit1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtVisit1s.Text;
                else if (lblCounterDounload1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCounterDounload1s.Text;
                else if (lblCategoryID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID1s.Text;
                else if (lblAvtar1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAvtar1s.Text;
                else if (lblFileUpload1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFileUpload1s.Text;

                else if (lblTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2h.Text;
                else if (lblDescreption2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescreption2h.Text;
                else if (lblSize2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSize2h.Text;
                else if (lblKeyWord2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeyWord2h.Text;
                else if (lblExetation2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtExetation2h.Text;
                else if (lblRate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRate2h.Text;
                else if (lblVisit2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtVisit2h.Text;
                else if (lblCounterDounload2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCounterDounload2h.Text;
                else if (lblCategoryID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID2h.Text;
                else if (lblAvtar2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAvtar2h.Text;
                else if (lblFileUpload2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFileUpload2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\tbl_Event_Master.xml"));

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
            txtTitle.Enabled = true;
            txtDescreption.Enabled = true;
            txtSize.Enabled = true;
            txtKeyWord.Enabled = true;
            txtExetation.Enabled = true;
            txtRate.Enabled = true;
            txtVisit.Enabled = true;
            txtCounterDounload.Enabled = true;
            drpCategoryID.Enabled = true;
            txtAvtar.Enabled = true;
            txtFileUpload.Enabled = true;
            //cbDeleted.Enabled = true;
            //cbActivated.Enabled = true;
            //drpCreatedBy.Enabled = true;
            //txtCreatedDate.Enabled = true;
            //txturlpath.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtTitle.Enabled = false;
            txtDescreption.Enabled = false;
            txtSize.Enabled = false;
            txtKeyWord.Enabled = false;
            txtExetation.Enabled = false;
            txtRate.Enabled = false;
            txtVisit.Enabled = false;
            txtCounterDounload.Enabled = false;
            drpCategoryID.Enabled = false;
            txtAvtar.Enabled = false;
            txtFileUpload.Enabled = false;
            //cbDeleted.Enabled = false;
            //cbActivated.Enabled = false;
            //drpCreatedBy.Enabled = false;
            //txtCreatedDate.Enabled = false;
            //txturlpath.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Event_Master.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Master.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

        //    ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tbl_Event_Master.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Master.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //        ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tbl_Event_Master.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Master.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        //    int Totalrec = DB.tbl_Event_Master.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Master.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
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


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tbl_Event_Master objSOJobDesc = DB.tbl_Event_Master.Single(p => p.ID == ID);
                        objSOJobDesc.Deleted = false;
                        DB.SaveChanges();
                        BindData();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Delete Successfully", "Warning!", Classes.Toastr.ToastPosition.TopCenter);
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tbl_Event_Master objtbl_Event_Master = DB.tbl_Event_Master.Single(p => p.ID == ID && p.Deleted == true);
                        txtTitle.Text = objtbl_Event_Master.Title.ToString();
                        txtDescreption.Text = objtbl_Event_Master.Descreption.ToString();
                        txtSize.Text = objtbl_Event_Master.Size.ToString();
                        txtKeyWord.Text = objtbl_Event_Master.KeyWord.ToString();
                        txtExetation.Text = objtbl_Event_Master.Exetation.ToString();
                        txtRate.Text = objtbl_Event_Master.Rate.ToString();
                        txtVisit.Text = objtbl_Event_Master.Visit.ToString();
                        txtCounterDounload.Text = objtbl_Event_Master.CounterDounload.ToString();
                        drpCategoryID.SelectedValue = objtbl_Event_Master.CategoryID.ToString();
                        txtAvtar.Text = objtbl_Event_Master.Avtar.ToString();
                        txtFileUpload.Text = objtbl_Event_Master.FileUpload.ToString();
                        //cbDeleted.Checked = (objtbl_Event_Master.Deleted == true) ? true : false;
                        //cbActivated.Checked = (objtbl_Event_Master.Activated == true) ? true : false;
                        //drpCreatedBy.SelectedValue = objtbl_Event_Master.CreatedBy.ToString();
                        //txtCreatedDate.Text = objtbl_Event_Master.CreatedDate.ToString();
                        //txturlpath.Text = objtbl_Event_Master.urlpath.ToString();

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

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Event_Master.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Master.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        //protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        //protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
        //    ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
        //    control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        //}
        #endregion

    }
}