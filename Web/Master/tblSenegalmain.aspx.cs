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
    public partial class tblSenegalmain : System.Web.UI.Page
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
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.tblSenegalmain> List = DB.tblSenegalmains.OrderBy(m => m.ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblTitle1s.Attributes["class"] = lblArabicTitle1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblArabicDescription1s.Attributes["class"] = lblUCName1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblavtar1s.Attributes["class"] = lblSeq_Order1s.Attributes["class"] = lblPageID1s.Attributes["class"] = lblHTMLData1s.Attributes["class"] = lblArabicHTMLData1s.Attributes["class"] = lblPageType1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblTitle2h.Attributes["class"] = lblArabicTitle2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblArabicDescription2h.Attributes["class"] = lblUCName2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblavtar2h.Attributes["class"] = lblSeq_Order2h.Attributes["class"] = lblPageID2h.Attributes["class"] = lblHTMLData2h.Attributes["class"] = lblArabicHTMLData2h.Attributes["class"] = lblPageType2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblTitle1s.Attributes["class"] = lblArabicTitle1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblArabicDescription1s.Attributes["class"] = lblUCName1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblavtar1s.Attributes["class"] = lblSeq_Order1s.Attributes["class"] = lblPageID1s.Attributes["class"] = lblHTMLData1s.Attributes["class"] = lblArabicHTMLData1s.Attributes["class"] = lblPageType1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblTitle2h.Attributes["class"] = lblArabicTitle2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblArabicDescription2h.Attributes["class"] = lblUCName2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblavtar2h.Attributes["class"] = lblSeq_Order2h.Attributes["class"] = lblPageID2h.Attributes["class"] = lblHTMLData2h.Attributes["class"] = lblArabicHTMLData2h.Attributes["class"] = lblPageType2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtArabicTitle.Text = "";
            txtDescription.Text = "";
            txtArabicDescription.Text = "";
            txtUCName.Text = "";
            cbActive.Text = "";            
            txtavtar.Text = "";
            txtSeq_Order.Text = "";
            drpPageID.SelectedIndex = 0;
            CKEHTMLData.Text = "";
            CKEArabicHTMLData.Text = "";
            drpPageType.SelectedIndex = 0;

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
                        Database.tblSenegalmain objtblSenegalmain = new Database.tblSenegalmain();
                        //Server Content Send data Yogesh
                        objtblSenegalmain.Title = txtTitle.Text;
                        objtblSenegalmain.ArabicTitle = txtArabicTitle.Text;
                        objtblSenegalmain.Description = txtDescription.Text;
                        objtblSenegalmain.ArabicDescription = txtArabicDescription.Text;
                        objtblSenegalmain.UCName = txtUCName.Text;
                        objtblSenegalmain.Active = cbActive.Checked;
                        
                        objtblSenegalmain.avtar = txtavtar.Text;
                        objtblSenegalmain.Seq_Order = Convert.ToInt32(txtSeq_Order.Text);
                        objtblSenegalmain.PageID = Convert.ToInt32(drpPageID.SelectedValue);
                        objtblSenegalmain.HTMLData = CKEHTMLData.Text;
                        objtblSenegalmain.ArabicHTMLData = CKEArabicHTMLData.Text;
                        objtblSenegalmain.PageType = drpPageType.SelectedValue;
                        objtblSenegalmain.DateTime = DateTime.Now;
                        objtblSenegalmain.CreatedBy = "1";
                        objtblSenegalmain.Deleted = true;

                        DB.tblSenegalmains.AddObject(objtblSenegalmain);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "Add New";
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.ValidationGroup = "ss";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                       // navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.tblSenegalmain objtblSenegalmain = DB.tblSenegalmains.Single(p => p.ID == ID);
                            objtblSenegalmain.Title = txtTitle.Text;
                            objtblSenegalmain.ArabicTitle = txtArabicTitle.Text;
                            objtblSenegalmain.Description = txtDescription.Text;
                            objtblSenegalmain.ArabicDescription = txtArabicDescription.Text;
                            objtblSenegalmain.UCName = txtUCName.Text;
                            objtblSenegalmain.Active = cbActive.Checked == true ? true : false;
                            
                            objtblSenegalmain.avtar = txtavtar.Text;
                            objtblSenegalmain.Seq_Order = Convert.ToInt32(txtSeq_Order.Text);
                            objtblSenegalmain.PageID = Convert.ToInt32(drpPageID.SelectedValue);
                            objtblSenegalmain.HTMLData = CKEHTMLData.Text;
                            objtblSenegalmain.ArabicHTMLData = CKEArabicHTMLData.Text;
                            objtblSenegalmain.PageType = drpPageType.SelectedValue;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "ss";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
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
            //drpPageType.Items.Insert(0, new ListItem("-- Select --", "0"));drpPageType.DataSource = DB.0;drpPageType.DataTextField = "0";drpPageType.DataValueField = "0";drpPageType.DataBind();
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
            txtTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtArabicTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
            txtArabicDescription.Text = Listview1.SelectedDataKey[0].ToString();
            txtUCName.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();

            txtavtar.Text = Listview1.SelectedDataKey[0].ToString();
            txtSeq_Order.Text = Listview1.SelectedDataKey[0].ToString();
            drpPageID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            CKEHTMLData.Text = Listview1.SelectedDataKey[0].ToString();
            CKEArabicHTMLData.Text = Listview1.SelectedDataKey[0].ToString();
            drpPageType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
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
                    lblTitle2h.Visible = lblArabicTitle2h.Visible = lblDescription2h.Visible = lblArabicDescription2h.Visible = lblUCName2h.Visible = lblActive2h.Visible = lblavtar2h.Visible = lblSeq_Order2h.Visible = lblPageID2h.Visible = lblHTMLData2h.Visible = lblArabicHTMLData2h.Visible = lblPageType2h.Visible = false;
                    //2true
                    txtTitle2h.Visible = txtArabicTitle2h.Visible = txtDescription2h.Visible = txtArabicDescription2h.Visible = txtUCName2h.Visible = txtActive2h.Visible = txtavtar2h.Visible = txtSeq_Order2h.Visible = txtPageID2h.Visible = txtHTMLData2h.Visible = txtArabicHTMLData2h.Visible = txtPageType2h.Visible = true;

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
                    lblTitle2h.Visible = lblArabicTitle2h.Visible = lblDescription2h.Visible = lblArabicDescription2h.Visible = lblUCName2h.Visible = lblActive2h.Visible = lblavtar2h.Visible = lblSeq_Order2h.Visible = lblPageID2h.Visible = lblHTMLData2h.Visible = lblArabicHTMLData2h.Visible = lblPageType2h.Visible = true;
                    //2false
                    txtTitle2h.Visible = txtArabicTitle2h.Visible = txtDescription2h.Visible = txtArabicDescription2h.Visible = txtUCName2h.Visible = txtActive2h.Visible = txtavtar2h.Visible = txtSeq_Order2h.Visible = txtPageID2h.Visible = txtHTMLData2h.Visible = txtArabicHTMLData2h.Visible = txtPageType2h.Visible = false;

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
                    lblTitle1s.Visible = lblArabicTitle1s.Visible = lblDescription1s.Visible = lblArabicDescription1s.Visible = lblUCName1s.Visible = lblActive1s.Visible = lblavtar1s.Visible = lblSeq_Order1s.Visible = lblPageID1s.Visible = lblHTMLData1s.Visible = lblArabicHTMLData1s.Visible = lblPageType1s.Visible = false;
                    //1true
                    txtTitle1s.Visible = txtArabicTitle1s.Visible = txtDescription1s.Visible = txtArabicDescription1s.Visible = txtUCName1s.Visible = txtActive1s.Visible = txtavtar1s.Visible = txtSeq_Order1s.Visible = txtPageID1s.Visible = txtHTMLData1s.Visible = txtArabicHTMLData1s.Visible = txtPageType1s.Visible = true;
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
                    lblTitle1s.Visible = lblArabicTitle1s.Visible = lblDescription1s.Visible = lblArabicDescription1s.Visible = lblUCName1s.Visible = lblActive1s.Visible = lblavtar1s.Visible = lblSeq_Order1s.Visible = lblPageID1s.Visible = lblHTMLData1s.Visible = lblArabicHTMLData1s.Visible = lblPageType1s.Visible = true;
                    //1false
                    txtTitle1s.Visible = txtArabicTitle1s.Visible = txtDescription1s.Visible = txtArabicDescription1s.Visible = txtUCName1s.Visible = txtActive1s.Visible = txtavtar1s.Visible = txtSeq_Order1s.Visible = txtPageID1s.Visible = txtHTMLData1s.Visible = txtArabicHTMLData1s.Visible = txtPageType1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblSenegalmain").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblTitle1s.ID == item.LabelID)
                    txtTitle1s.Text = lblTitle1s.Text = lblhTitle.Text = item.LabelName;
                else if (lblArabicTitle1s.ID == item.LabelID)
                    txtArabicTitle1s.Text = lblArabicTitle1s.Text = lblhArabicTitle.Text = item.LabelName;
                else if (lblDescription1s.ID == item.LabelID)
                    txtDescription1s.Text = lblDescription1s.Text = lblhDescription.Text = item.LabelName;
                else if (lblArabicDescription1s.ID == item.LabelID)
                    txtArabicDescription1s.Text = lblArabicDescription1s.Text = lblhArabicDescription.Text = item.LabelName;
                else if (lblUCName1s.ID == item.LabelID)
                    txtUCName1s.Text = lblUCName1s.Text = lblhUCName.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblavtar1s.ID == item.LabelID)
                    txtavtar1s.Text = lblavtar1s.Text = item.LabelName;
                else if (lblSeq_Order1s.ID == item.LabelID)
                    txtSeq_Order1s.Text = lblSeq_Order1s.Text = item.LabelName;
                else if (lblPageID1s.ID == item.LabelID)
                    txtPageID1s.Text = lblPageID1s.Text = item.LabelName;
                else if (lblHTMLData1s.ID == item.LabelID)
                    txtHTMLData1s.Text = lblHTMLData1s.Text = item.LabelName;
                else if (lblArabicHTMLData1s.ID == item.LabelID)
                    txtArabicHTMLData1s.Text = lblArabicHTMLData1s.Text = item.LabelName;
                else if (lblPageType1s.ID == item.LabelID)
                    txtPageType1s.Text = lblPageType1s.Text = item.LabelName;

                else if (lblTitle2h.ID == item.LabelID)
                    txtTitle2h.Text = lblTitle2h.Text = lblhTitle.Text = item.LabelName;
                else if (lblArabicTitle2h.ID == item.LabelID)
                    txtArabicTitle2h.Text = lblArabicTitle2h.Text = lblhArabicTitle.Text = item.LabelName;
                else if (lblDescription2h.ID == item.LabelID)
                    txtDescription2h.Text = lblDescription2h.Text = lblhDescription.Text = item.LabelName;
                else if (lblArabicDescription2h.ID == item.LabelID)
                    txtArabicDescription2h.Text = lblArabicDescription2h.Text = lblhArabicDescription.Text = item.LabelName;
                else if (lblUCName2h.ID == item.LabelID)
                    txtUCName2h.Text = lblUCName2h.Text = lblhUCName.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblavtar2h.ID == item.LabelID)
                    txtavtar2h.Text = lblavtar2h.Text = item.LabelName;
                else if (lblSeq_Order2h.ID == item.LabelID)
                    txtSeq_Order2h.Text = lblSeq_Order2h.Text = item.LabelName;
                else if (lblPageID2h.ID == item.LabelID)
                    txtPageID2h.Text = lblPageID2h.Text = item.LabelName;
                else if (lblHTMLData2h.ID == item.LabelID)
                    txtHTMLData2h.Text = lblHTMLData2h.Text = item.LabelName;
                else if (lblArabicHTMLData2h.ID == item.LabelID)
                    txtArabicHTMLData2h.Text = lblArabicHTMLData2h.Text = item.LabelName;
                else if (lblPageType2h.ID == item.LabelID)
                    txtPageType2h.Text = lblPageType2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblSenegalmain").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblSenegalmain.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblSenegalmain").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle1s.Text;
                else if (lblArabicTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtArabicTitle1s.Text;
                else if (lblDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription1s.Text;
                else if (lblArabicDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtArabicDescription1s.Text;
                else if (lblUCName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUCName1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblavtar1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtavtar1s.Text;
                else if (lblSeq_Order1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSeq_Order1s.Text;
                else if (lblPageID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageID1s.Text;
                else if (lblHTMLData1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtHTMLData1s.Text;
                else if (lblArabicHTMLData1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtArabicHTMLData1s.Text;
                else if (lblPageType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageType1s.Text;

                else if (lblTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2h.Text;
                else if (lblArabicTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtArabicTitle2h.Text;
                else if (lblDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription2h.Text;
                else if (lblArabicDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtArabicDescription2h.Text;
                else if (lblUCName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUCName2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblavtar2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtavtar2h.Text;
                else if (lblSeq_Order2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSeq_Order2h.Text;
                else if (lblPageID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageID2h.Text;
                else if (lblHTMLData2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtHTMLData2h.Text;
                else if (lblArabicHTMLData2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtArabicHTMLData2h.Text;
                else if (lblPageType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageType2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblSenegalmain.xml"));

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
            txtArabicTitle.Enabled = true;
            txtDescription.Enabled = true;
            txtArabicDescription.Enabled = true;
            txtUCName.Enabled = true;
            cbActive.Enabled = true;
            
            txtavtar.Enabled = true;
            txtSeq_Order.Enabled = true;
            drpPageID.Enabled = true;
            CKEHTMLData.Enabled = true;
            CKEArabicHTMLData.Enabled = true;
            drpPageType.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtTitle.Enabled = false;
            txtArabicTitle.Enabled = false;
            txtDescription.Enabled = false;
            txtArabicDescription.Enabled = false;
            txtUCName.Enabled = false;
            cbActive.Enabled = false;
            
            txtavtar.Enabled = false;
            txtSeq_Order.Enabled = false;
            drpPageID.Enabled = false;
            CKEHTMLData.Enabled = false;
            CKEArabicHTMLData.Enabled = false;
            drpPageType.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tblSenegalmain.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSenegalmain.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tblSenegalmain.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSenegalmain.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tblSenegalmain.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSenegalmain.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
        //    int Totalrec = DB.tblSenegalmain.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSenegalmain.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
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
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.tblSenegalmain objSOJobDesc = DB.tblSenegalmains.Single(p => p.ID == ID);
                        objSOJobDesc.Deleted = false;
                        DB.SaveChanges();
                        BindData();
                      
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.tblSenegalmain objtblSenegalmain = DB.tblSenegalmains.Single(p => p.ID == ID);
                        txtTitle.Text = objtblSenegalmain.Title.ToString();
                        txtArabicTitle.Text = objtblSenegalmain.ArabicTitle.ToString();
                        txtDescription.Text = objtblSenegalmain.Description.ToString();
                        txtArabicDescription.Text = objtblSenegalmain.ArabicDescription.ToString();
                        txtUCName.Text = objtblSenegalmain.UCName.ToString();
                        cbActive.Checked = (objtblSenegalmain.Active == true) ? true : false;
                        
                        txtavtar.Text = objtblSenegalmain.avtar.ToString();
                        txtSeq_Order.Text = objtblSenegalmain.Seq_Order.ToString();
                        drpPageID.SelectedValue = objtblSenegalmain.PageID.ToString();
                        CKEHTMLData.Text = objtblSenegalmain.HTMLData.ToString();
                        CKEArabicHTMLData.Text = objtblSenegalmain.ArabicHTMLData.ToString();
                        drpPageType.SelectedValue = objtblSenegalmain.PageType.ToString();

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
        //    int Totalrec = DB.tblSenegalmain.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSenegalmain.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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