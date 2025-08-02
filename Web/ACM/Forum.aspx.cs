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
using Web.ACM;

namespace Web.ACM
{
    public partial class Forum : System.Web.UI.Page
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
                BindDrp();


            }
        }
        #region Step2

        public void BindData()
        {

            var List = DB.BlogForums.Where(p => p.Active == true && p.Deleted == false).OrderBy(m => m.ID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        public void BindDrp()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);

            Classes.EcommAdminClass.getdropdown(drpCategoryID, TID, "Category", "Blog", "Sales", "REFTABLE");
            //select * from REFTABLE where REFTYPE='Blog' and REFSUBTYPE = 'Category' 

            //drpCategoryID.DataSource = DB.REFTABLEs.Where(p => p.REFTYPE == "Blog" && p.REFSUBTYPE == "Category");
            //drpCategoryID.DataTextField = "REFNAME1";
            //drpCategoryID.DataValueField = "REFID";
            //drpCategoryID.DataBind();
            //drpCategoryID.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        #endregion

        #region PAge Genarator
        
        public void GetShow()
        {

            lblForumTopic1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDisplayName1s.Attributes["class"] = lblAvtar1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblForumTopic2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDisplayName2h.Attributes["class"] = lblAvtar2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblForumTopic1s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDisplayName1s.Attributes["class"] = lblAvtar1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblForumTopic2h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDisplayName2h.Attributes["class"] = lblAvtar2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtForumTopic.Text = "";
            txtDescription.Text = "";
            drpCategoryID.SelectedIndex = 0;
            //drpUserID.SelectedIndex = 0;
            //txtActive.Text = "";
            //txtDateTime.Text = "";
            //txtDeleted.Text = "";
            //drpVisited.SelectedIndex = 0;
            txtDisplayName.Text = "";
            //txtAvtar.Text = "";


        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                if (btnAdd.Text == "AddNew")
                {

                    Write();
                    Clear();
                    btnAdd.Text = "Add";
                }
                else if (btnAdd.Text == "Add")
                {

                    Database.BlogForum objForum = new Database.BlogForum();
                    //Server Content Send data Yogesh
                    objForum.ForumTopic = txtForumTopic.Text;
                    objForum.Description = txtDescription.Text;
                    objForum.CategoryID = Convert.ToInt32(drpCategoryID.SelectedValue);
                    objForum.UserID = UID;
                    objForum.Active = cbActive.Checked;
                    objForum.DateTime = DateTime.Now;
                    objForum.Deleted = false;
                    objForum.Visited = 1;
                    objForum.DisplayName = txtDisplayName.Text;
                    //objForum.Avtar = txtAvtar.Text;
                    if (imgupload.HasFile)
                    {

                        imgupload.SaveAs(Server.MapPath("~/Gallery/") + imgupload.FileName);
                        objForum.Avtar = imgupload.FileName;

                    }

                    DB.BlogForums.AddObject(objForum);
                    DB.SaveChanges();
                    Clear();
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //navigation.Visible = true;
                    Readonly();
                    // FirstData();
                }
                else if (btnAdd.Text == "Update")
                {

                    if (ViewState["Edit"] != null)
                    {
                        int TID = Convert.ToInt32(ViewState["Edit"]);
                        Database.BlogForum objForum = DB.BlogForums.Single(p => p.ID == TID);
                        objForum.ForumTopic = txtForumTopic.Text;
                        objForum.Description = txtDescription.Text;
                        objForum.CategoryID = Convert.ToInt32(drpCategoryID.SelectedValue);
                        objForum.UserID = UID;
                        objForum.Active = cbActive.Checked;
                        objForum.DateTime = DateTime.Now;
                        objForum.Deleted = false;
                        objForum.Visited = 2;
                        objForum.DisplayName = txtDisplayName.Text;
                        if (imgupload.HasFile)
                        {

                            imgupload.SaveAs(Server.MapPath("~/Gallery/") + imgupload.FileName);
                            objForum.Avtar = imgupload.FileName;

                        }
                        //objForum.Avtar = imgupload.FileName;
                        ViewState["Edit"] = null;
                        btnAdd.Text = "Add New";
                        DB.SaveChanges();
                        Clear();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();


                        //navigation.Visible = true;
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
            //drpAvtar.Items.Insert(0, new ListItem("-- Select --", "0"));drpAvtar.DataSource = DB.0;drpAvtar.DataTextField = "0";drpAvtar.DataValueField = "0";drpAvtar.DataBind();
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
            txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[1].ToString();
            drpCategoryID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            //  drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //   cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //   txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //   cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //   drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtDisplayName.Text = Listview1.SelectedDataKey[3].ToString();


        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescription.Text = Listview1.SelectedDataKey[1].ToString();
                drpCategoryID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                // drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                // cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //  txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //  cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //  drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtDisplayName.Text = Listview1.SelectedDataKey[3].ToString();
                //txtAvtar.Text = Listview1.SelectedDataKey[4].ToString();

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
                txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
                txtDescription.Text = Listview1.SelectedDataKey[1].ToString();
                drpCategoryID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                //  drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //  cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //   txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //   cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //   drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtDisplayName.Text = Listview1.SelectedDataKey[3].ToString();
                //txtAvtar.Text = Listview1.SelectedDataKey[4].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
            txtDescription.Text = Listview1.SelectedDataKey[1].ToString();
            drpCategoryID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            //  drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //   cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //    txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //    cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //    drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtDisplayName.Text = Listview1.SelectedDataKey[3].ToString();
            //txtAvtar.Text = Listview1.SelectedDataKey[4].ToString();

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
                    lblForumTopic2h.Visible = lblDescription2h.Visible = lblCategoryID2h.Visible = lblActive2h.Visible = lblDisplayName2h.Visible = lblAvtar2h.Visible = false;
                    //2true
                    txtForumTopic2h.Visible = txtDescription2h.Visible = txtCategoryID2h.Visible = txtActive2h.Visible = txtDisplayName2h.Visible = txtAvtar2h.Visible = true;

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
                    lblForumTopic2h.Visible = lblDescription2h.Visible = lblCategoryID2h.Visible = lblActive2h.Visible = lblDisplayName2h.Visible = lblAvtar2h.Visible = true;
                    //2false
                    txtForumTopic2h.Visible = txtDescription2h.Visible = txtCategoryID2h.Visible = txtActive2h.Visible = txtDisplayName2h.Visible = txtAvtar2h.Visible = false;

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
                    lblForumTopic1s.Visible = lblDescription1s.Visible = lblCategoryID1s.Visible = lblActive1s.Visible = lblDisplayName1s.Visible = lblAvtar1s.Visible = false;
                    //1true
                    txtForumTopic1s.Visible = txtDescription1s.Visible = txtCategoryID1s.Visible = txtActive1s.Visible = txtDisplayName1s.Visible = txtAvtar1s.Visible = true;
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
                    lblForumTopic1s.Visible = lblDescription1s.Visible = lblCategoryID1s.Visible = lblActive1s.Visible = lblDisplayName1s.Visible = lblAvtar1s.Visible = true;
                    //1false
                    txtForumTopic1s.Visible = txtDescription1s.Visible = txtCategoryID1s.Visible = txtActive1s.Visible = txtDisplayName1s.Visible = txtAvtar1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("Forum").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblForumTopic1s.ID == item.LabelID)
                    txtForumTopic1s.Text = lblForumTopic1s.Text = lblhForumTopic.Text = item.LabelName;
                else if (lblDescription1s.ID == item.LabelID)
                    txtDescription1s.Text = lblDescription1s.Text = item.LabelName;
                else if (lblCategoryID1s.ID == item.LabelID)
                    txtCategoryID1s.Text = lblCategoryID1s.Text = lblhCategoryID.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblDisplayName1s.ID == item.LabelID)
                    txtDisplayName1s.Text = lblDisplayName1s.Text = item.LabelName;
                else if (lblAvtar1s.ID == item.LabelID)
                    txtAvtar1s.Text = lblAvtar1s.Text = item.LabelName;

                else if (lblForumTopic2h.ID == item.LabelID)
                    txtForumTopic2h.Text = lblForumTopic2h.Text = lblhForumTopic.Text = item.LabelName;
                else if (lblDescription2h.ID == item.LabelID)
                    txtDescription2h.Text = lblDescription2h.Text = item.LabelName;
                else if (lblCategoryID2h.ID == item.LabelID)
                    txtCategoryID2h.Text = lblCategoryID2h.Text = lblhCategoryID.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblDisplayName2h.ID == item.LabelID)
                    txtDisplayName2h.Text = lblDisplayName2h.Text = item.LabelName;
                else if (lblAvtar2h.ID == item.LabelID)
                    txtAvtar2h.Text = lblAvtar2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("Forum").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\Forum.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("Forum").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblForumTopic1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic1s.Text;
                else if (lblDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription1s.Text;
                else if (lblCategoryID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblDisplayName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDisplayName1s.Text;
                else if (lblAvtar1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAvtar1s.Text;

                else if (lblForumTopic2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic2h.Text;
                else if (lblDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription2h.Text;
                else if (lblCategoryID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblDisplayName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDisplayName2h.Text;
                else if (lblAvtar2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAvtar2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\Forum.xml"));

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
            txtForumTopic.Enabled = true;
            txtDescription.Enabled = true;
            drpCategoryID.Enabled = true;
            //drpUserID.Enabled = true;
            cbActive.Enabled = true;
            //txtDateTime.Enabled = true;
            // cbDeleted.Enabled = true;
            //drpVisited.Enabled = true;
            txtDisplayName.Enabled = true;
            //txtAvtar.Enabled = true;


        }
        public void Readonly()
        {
            // navigation.Visible = true;
            txtForumTopic.Enabled = false;
            txtDescription.Enabled = false;
            drpCategoryID.Enabled = false;
            // drpUserID.Enabled = false;
            cbActive.Enabled = false;
            //txtDateTime.Enabled = false;
            //cbDeleted.Enabled = false;
            // drpVisited.Enabled = false;
            txtDisplayName.Enabled = false;
            //txtAvtar.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.BlogForums.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((ACMMaster)Page.Master).BindList(Listview1, (DB.BlogForums.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

            ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.BlogForums.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.BlogForums.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.BlogForums.Count();
                take = Showdata;
                Skip = 0;
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.BlogForums.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
            int Totalrec = DB.BlogForums.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((ACMMaster)Page.Master).BindList(Listview1, (DB.BlogForums.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
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
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                //try
                //{ 

                if (e.CommandName == "btnDelete")
                {
                    int TID = Convert.ToInt32(e.CommandArgument);
                    //string[] ID = e.CommandArgument.ToString().Split(',');
                    //string str1 = ID[0].ToString();
                    //string str2 = ID[1].ToString();

                    Database.BlogForum objSOJobDesc = DB.BlogForums.Single(p => p.ID == TID && p.UserID == UID);
                    objSOJobDesc.Active = false;
                    DB.SaveChanges();
                    BindData();
                    //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                    //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                    //((ACMMaster)Page.Master).BindList(Listview1, (DB.BlogForums.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());

                }

                if (e.CommandName == "btnEdit")
                {

                    int ID = Convert.ToInt32(e.CommandArgument);
                    //string[] ID = e.CommandArgument.ToString().Split(',');
                    //string str1 = ID[0].ToString();
                    //string str2 = ID[1].ToString();

                    Database.BlogForum objForum = DB.BlogForums.Single(p => p.ID == ID);
                    txtForumTopic.Text = objForum.ForumTopic.ToString();
                    txtDescription.Text = objForum.Description.ToString();
                    drpCategoryID.SelectedValue = objForum.CategoryID.ToString();
                    //drpUserID.SelectedValue = objForum.UserID.ToString();
                    cbActive.Checked = (objForum.Active == true) ? true : false;
                    // txtDateTime.Text = objForum.DateTime.ToString();
                    // cbDeleted.Checked = (objForum.Deleted == true) ? true : false;
                    // drpVisited.SelectedValue = objForum.Visited.ToString();
                    txtDisplayName.Text = objForum.DisplayName.ToString();
                    //imgupload.FileName = objForum.Avtar.ToString();
                    ViewState["Edit"] = ID;
                    btnAdd.Text = "Update";

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

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.BlogForums.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((ACMMaster)Page.Master).BindList(Listview1, (DB.BlogForums.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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