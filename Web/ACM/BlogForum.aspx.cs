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
    public partial class BlogForum : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        int UID = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            if (!IsPostBack)
            {
                Readonly();
                Session["LANGUAGE"] = "en-US";
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
            List<Database.BlogForum> List = DB.BlogForums.Where(p => p.Deleted == false && p.UserID == UID && p.TenentID == TID).OrderBy(m => m.ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
           
        }
        #endregion

        public void GetShow()
        {

            lblForumType1s.Attributes["class"] = lblForumTopic1s.Attributes["class"] = lblForumTopic21s.Attributes["class"] = lblForumTopic31s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblDescription21s.Attributes["class"] = lblDescription31s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDisplayName1s.Attributes["class"] = lblAvtar1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblForumType2h.Attributes["class"] = lblForumTopic2h.Attributes["class"] = lblForumTopic22h.Attributes["class"] = lblForumTopic32h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblDescription22h.Attributes["class"] = lblDescription32h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDisplayName2h.Attributes["class"] = lblAvtar2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblForumType1s.Attributes["class"] = lblForumTopic1s.Attributes["class"] = lblForumTopic21s.Attributes["class"] = lblForumTopic31s.Attributes["class"] = lblDescription1s.Attributes["class"] = lblDescription21s.Attributes["class"] = lblDescription31s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDisplayName1s.Attributes["class"] = lblAvtar1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblForumType2h.Attributes["class"] = lblForumTopic2h.Attributes["class"] = lblForumTopic22h.Attributes["class"] = lblForumTopic32h.Attributes["class"] = lblDescription2h.Attributes["class"] = lblDescription22h.Attributes["class"] = lblDescription32h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDisplayName2h.Attributes["class"] = lblAvtar2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            drpForumType.SelectedIndex = 0;
            txtForumTopic.Text = "";
            txtForumTopic2.Text = "";
            txtForumTopic3.Text = "";
            txtDescription.Text = "";
            txtDescription2.Text = "";
            txtDescription3.Text = "";
            drpCategoryID.SelectedIndex = 0;
            txtDisplayName.Text = "";

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
                        Database.BlogForum objBlogForum = new Database.BlogForum();
                        //Server Content Send data Yogesh
                        objBlogForum.TenentID = TID;
                        objBlogForum.ForumType = Convert.ToInt32(drpForumType.SelectedValue);
                        objBlogForum.ForumTopic = txtForumTopic.Text;
                        objBlogForum.ForumTopic2 = txtForumTopic2.Text;
                        objBlogForum.ForumTopic3 = txtForumTopic3.Text;
                        objBlogForum.Description = txtDescription.Text;
                        objBlogForum.Description2 = txtDescription2.Text;
                        objBlogForum.Description3 = txtDescription3.Text;
                        objBlogForum.CategoryID = Convert.ToInt32(drpCategoryID.SelectedValue);
                        objBlogForum.UserID = UID;
                        objBlogForum.Active = cbActive.Checked == true ? true : false;
                        objBlogForum.DateTime = DateTime.Now;
                        objBlogForum.Deleted = false;
                        objBlogForum.Visited = 0;
                        objBlogForum.DisplayName = txtDisplayName.Text;
                        if (FileUpload1.HasFile)
                        {
                            FileUpload1.SaveAs(Server.MapPath("~/Gallery/") + FileUpload1.FileName);
                            objBlogForum.Avtar = FileUpload1.FileName;
                        }

                        DB.BlogForums.AddObject(objBlogForum);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.ValidationGroup = "ss";
                        btnAdd.Text = "Add New";
                        lblMsg.Text = "Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        Readonly();

                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.BlogForum objBlogForum = DB.BlogForums.Single(p => p.ID == ID);
                            objBlogForum.ForumType = Convert.ToInt32(drpForumType.SelectedValue);
                            objBlogForum.ForumTopic = txtForumTopic.Text;
                            objBlogForum.ForumTopic2 = txtForumTopic2.Text;
                            objBlogForum.ForumTopic3 = txtForumTopic3.Text;
                            objBlogForum.Description = txtDescription.Text;
                            objBlogForum.Description2 = txtDescription2.Text;
                            objBlogForum.Description3 = txtDescription3.Text;
                            objBlogForum.CategoryID = Convert.ToInt32(drpCategoryID.SelectedValue);

                            objBlogForum.Active = cbActive.Checked;

                            objBlogForum.Visited = 1;
                            objBlogForum.DisplayName = txtDisplayName.Text;
                            if (FileUpload1.HasFile)
                            {
                                FileUpload1.SaveAs(Server.MapPath("~/Gallery/") + FileUpload1.FileName);
                                objBlogForum.Avtar = FileUpload1.FileName;
                            }

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            Readonly();

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
            Classes.EcommAdminClass.getdropdown(drpForumType, TID, "Forum", "Type", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpCategoryID, TID, "Forum", "CategoryType", "", "REFTABLE");
            
        }

        //protected void btnFirst_Click(object sender, EventArgs e)
        //{
        //    FirstData();
        //}
        //protected void btnNext_Click(object sender, EventArgs e)
        //{
        //    NextData();
        //}
        //protected void btnPrev_Click(object sender, EventArgs e)
        //{
        //    PrevData();
        //}
        //protected void btnLast_Click(object sender, EventArgs e)
        //{
        //    LastData();
        //}
        //public void FirstData()
        //{
        //    int index = Convert.ToInt32(ViewState["Index"]);
        //    Listview1.SelectedIndex = 0;
        //    drpForumType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtForumTopic2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtForumTopic3.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtDescription2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtDescription3.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //    cbActive.Checked = Listview1.SelectedDataKey[0].ToString();

        //    txtDisplayName.Text = Listview1.SelectedDataKey[0].ToString();


        //}
        //public void NextData()
        //{

        //    if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
        //    {
        //        Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
        //        drpForumType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtForumTopic2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtForumTopic3.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtDescription2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtDescription3.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
        //        txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
        //        cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
        //        drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtDisplayName.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //    }

        //}
        //public void PrevData()
        //{
        //    if (Listview1.SelectedIndex == 0)
        //    {
        //        lblMsg.Text = "This is first record";
        //        pnlSuccessMsg.Visible = true;

        //    }
        //    else
        //    {
        //        pnlSuccessMsg.Visible = false;
        //        Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
        //        drpForumType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtForumTopic2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtForumTopic3.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtDescription2.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtDescription3.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
        //        txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
        //        cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
        //        drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //        txtDisplayName.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
        //        txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //        drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //    }
        //}
        //public void LastData()
        //{
        //    Listview1.SelectedIndex = Listview1.Items.Count - 1;
        //    drpForumType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtForumTopic.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtForumTopic2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtForumTopic3.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtDescription.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtDescription2.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtDescription3.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpCategoryID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    drpUserID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
        //    txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
        //    cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
        //    drpVisited.SelectedValue = Listview1.SelectedDataKey[0].ToString();
        //    txtDisplayName.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtAvtar.Text = Listview1.SelectedDataKey[0].ToString();
        //    txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();
        //    drpTenentID.SelectedValue = Listview1.SelectedDataKey[0].ToString();

        //}


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblForumType2h.Visible = lblForumTopic2h.Visible = lblForumTopic22h.Visible = lblForumTopic32h.Visible = lblDescription2h.Visible = lblDescription22h.Visible = lblDescription32h.Visible = lblCategoryID2h.Visible = lblActive2h.Visible = lblDisplayName2h.Visible = lblAvtar2h.Visible = false;
                    //2true
                    txtForumType2h.Visible = txtForumTopic2h.Visible = txtForumTopic22h.Visible = txtForumTopic32h.Visible = txtDescription2h.Visible = txtDescription22h.Visible = txtDescription32h.Visible = txtCategoryID2h.Visible = txtActive2h.Visible = txtDisplayName2h.Visible = txtAvtar2h.Visible = true;

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
                    lblForumType2h.Visible = lblForumTopic2h.Visible = lblForumTopic22h.Visible = lblForumTopic32h.Visible = lblDescription2h.Visible = lblDescription22h.Visible = lblDescription32h.Visible = lblCategoryID2h.Visible = lblActive2h.Visible = lblDisplayName2h.Visible = lblAvtar2h.Visible = true;
                    //2false
                    txtForumType2h.Visible = txtForumTopic2h.Visible = txtForumTopic22h.Visible = txtForumTopic32h.Visible = txtDescription2h.Visible = txtDescription22h.Visible = txtDescription32h.Visible = txtCategoryID2h.Visible = txtActive2h.Visible = txtDisplayName2h.Visible = txtAvtar2h.Visible = false;

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
                    lblForumType1s.Visible = lblForumTopic1s.Visible = lblForumTopic21s.Visible = lblForumTopic31s.Visible = lblDescription1s.Visible = lblDescription21s.Visible = lblDescription31s.Visible = lblCategoryID1s.Visible = lblActive1s.Visible = lblDisplayName1s.Visible = lblAvtar1s.Visible = false;
                    //1true
                    txtForumType1s.Visible = txtForumTopic1s.Visible = txtForumTopic21s.Visible = txtForumTopic31s.Visible = txtDescription1s.Visible = txtDescription21s.Visible = txtDescription31s.Visible = txtCategoryID1s.Visible = txtActive1s.Visible = txtDisplayName1s.Visible = txtAvtar1s.Visible = true;
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
                    lblForumType1s.Visible = lblForumTopic1s.Visible = lblForumTopic21s.Visible = lblForumTopic31s.Visible = lblDescription1s.Visible = lblDescription21s.Visible = lblDescription31s.Visible = lblCategoryID1s.Visible = lblActive1s.Visible = lblDisplayName1s.Visible = lblAvtar1s.Visible = true;
                    //1false
                    txtForumType1s.Visible = txtForumTopic1s.Visible = txtForumTopic21s.Visible = txtForumTopic31s.Visible = txtDescription1s.Visible = txtDescription21s.Visible = txtDescription31s.Visible = txtCategoryID1s.Visible = txtActive1s.Visible = txtDisplayName1s.Visible = txtAvtar1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("BlogForum").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblForumType1s.ID == item.LabelID)
                    txtForumType1s.Text = lblForumType1s.Text = lblhForumType.Text = item.LabelName;
                else if (lblForumTopic1s.ID == item.LabelID)
                    txtForumTopic1s.Text = lblForumTopic1s.Text = lblhForumTopic.Text = item.LabelName;
                else if (lblForumTopic21s.ID == item.LabelID)
                    txtForumTopic21s.Text = lblForumTopic21s.Text = item.LabelName;
                else if (lblForumTopic31s.ID == item.LabelID)
                    txtForumTopic31s.Text = lblForumTopic31s.Text = item.LabelName;
                else if (lblDescription1s.ID == item.LabelID)
                    txtDescription1s.Text = lblDescription1s.Text = item.LabelName;
                else if (lblDescription21s.ID == item.LabelID)
                    txtDescription21s.Text = lblDescription21s.Text = item.LabelName;
                else if (lblDescription31s.ID == item.LabelID)
                    txtDescription31s.Text = lblDescription31s.Text = item.LabelName;
                else if (lblCategoryID1s.ID == item.LabelID)
                    txtCategoryID1s.Text = lblCategoryID1s.Text = lblhCategoryID.Text = item.LabelName;
                //else if (lblUserID1s.ID == item.LabelID)
                //    txtUserID1s.Text = lblUserID1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblDisplayName1s.ID == item.LabelID)
                    txtDisplayName1s.Text = lblDisplayName1s.Text = item.LabelName;
                else if (lblAvtar1s.ID == item.LabelID)
                    txtAvtar1s.Text = lblAvtar1s.Text = lblhAvtar.Text = item.LabelName;

                else if (lblForumType2h.ID == item.LabelID)
                    txtForumType2h.Text = lblForumType2h.Text = lblhForumType.Text = item.LabelName;
                else if (lblForumTopic2h.ID == item.LabelID)
                    txtForumTopic2h.Text = lblForumTopic2h.Text = lblhForumTopic.Text = item.LabelName;
                else if (lblForumTopic22h.ID == item.LabelID)
                    txtForumTopic22h.Text = lblForumTopic22h.Text = item.LabelName;
                else if (lblForumTopic32h.ID == item.LabelID)
                    txtForumTopic32h.Text = lblForumTopic32h.Text = item.LabelName;
                else if (lblDescription2h.ID == item.LabelID)
                    txtDescription2h.Text = lblDescription2h.Text = item.LabelName;
                else if (lblDescription22h.ID == item.LabelID)
                    txtDescription22h.Text = lblDescription22h.Text = item.LabelName;
                else if (lblDescription32h.ID == item.LabelID)
                    txtDescription32h.Text = lblDescription32h.Text = item.LabelName;
                else if (lblCategoryID2h.ID == item.LabelID)
                    txtCategoryID2h.Text = lblCategoryID2h.Text = lblhCategoryID.Text = item.LabelName;
                //else if (lblUserID2h.ID == item.LabelID)
                //    txtUserID2h.Text = lblUserID2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblDisplayName2h.ID == item.LabelID)
                    txtDisplayName2h.Text = lblDisplayName2h.Text = item.LabelName;
                else if (lblAvtar2h.ID == item.LabelID)
                    txtAvtar2h.Text = lblAvtar2h.Text = lblhAvtar.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("BlogForum").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\BlogForum.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("BlogForum").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblForumType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumType1s.Text;
                else if (lblForumTopic1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic1s.Text;
                else if (lblForumTopic21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic21s.Text;
                else if (lblForumTopic31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic31s.Text;
                else if (lblDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription1s.Text;
                else if (lblDescription21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription21s.Text;
                else if (lblDescription31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription31s.Text;
                else if (lblCategoryID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID1s.Text;
                //else if (lblUserID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUserID1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblDisplayName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDisplayName1s.Text;
                else if (lblAvtar1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAvtar1s.Text;

                else if (lblForumType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumType2h.Text;
                else if (lblForumTopic2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic2h.Text;
                else if (lblForumTopic22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic22h.Text;
                else if (lblForumTopic32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtForumTopic32h.Text;
                else if (lblDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription2h.Text;
                else if (lblDescription22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription22h.Text;
                else if (lblDescription32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription32h.Text;
                else if (lblCategoryID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID2h.Text;
                //else if (lblUserID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUserID2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblDisplayName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDisplayName2h.Text;
                else if (lblAvtar2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAvtar2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\BlogForum.xml"));

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

            drpForumType.Enabled = true;
            txtForumTopic.Enabled = true;
            txtForumTopic2.Enabled = true;
            txtForumTopic3.Enabled = true;
            txtDescription.Enabled = true;
            txtDescription2.Enabled = true;
            txtDescription3.Enabled = true;
            drpCategoryID.Enabled = true;
            cbActive.Enabled = true;
            txtDisplayName.Enabled = true;
            FileUpload1.Enabled = true;
        }
        public void Readonly()
        {
            drpForumType.Enabled = false;
            txtForumTopic.Enabled = false;
            txtForumTopic2.Enabled = false;
            txtForumTopic3.Enabled = false;
            txtDescription.Enabled = false;
            txtDescription2.Enabled = false;
            txtDescription3.Enabled = false;
            drpCategoryID.Enabled = false;
            cbActive.Enabled = false;
            txtDisplayName.Enabled = false;
            FileUpload1.Enabled = false;
        }

        #region Listview

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

        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        Database.BlogForum objSOJobDesc = DB.BlogForums.Single(p => p.ID == ID && p.UserID == UID && p.TenentID == TID);
                        objSOJobDesc.Deleted = true;
                        DB.SaveChanges();
                        BindData();

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        Database.BlogForum objBlogForum = DB.BlogForums.Single(p => p.ID == ID && p.UserID == UID && p.TenentID == TID);
                        drpForumType.SelectedValue = objBlogForum.ForumType.ToString();
                        txtForumTopic.Text = objBlogForum.ForumTopic.ToString();
                        txtForumTopic2.Text = objBlogForum.ForumTopic2.ToString();
                        txtForumTopic3.Text = objBlogForum.ForumTopic3.ToString();
                        txtDescription.Text = objBlogForum.Description.ToString();
                        txtDescription2.Text = objBlogForum.Description2.ToString();
                        txtDescription3.Text = objBlogForum.Description3.ToString();
                        drpCategoryID.SelectedValue = objBlogForum.CategoryID.ToString();
                        //drpUserID.SelectedValue = objBlogForum.UserID.ToString();
                        cbActive.Checked = (objBlogForum.Active == true) ? true : false;
                        //txtDateTime.Text = objBlogForum.DateTime.ToString();
                        //cbDeleted.Checked = (objBlogForum.Deleted == true) ? true : false;
                        //drpVisited.SelectedValue = objBlogForum.Visited.ToString();
                        txtDisplayName.Text = objBlogForum.DisplayName.ToString();
                        //txtAvtar.Text = objBlogForum.Avtar.ToString();
                        //txtCRUP_ID.Text = objBlogForum.CRUP_ID.ToString();
                        // drpTenentID.SelectedValue = objBlogForum.TenentID.ToString();

                        btnAdd.ValidationGroup = "submit";
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

        #endregion
        public string getForumType(int ID)
        {            
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Forum" && p.REFSUBTYPE == "Type").Count() > 0)
            {
                string name = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Forum" && p.REFSUBTYPE == "Type").REFNAME1;
                return name;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getCategory(int ID)
        {
            if(DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Forum" && p.REFSUBTYPE == "CategoryType").Count() > 0)
            {
                string name = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID && p.REFTYPE == "Forum" && p.REFSUBTYPE == "CategoryType").REFNAME1;
                return name;
            }
            else
            {
                return "Not Found";
            }
            
        }



    }

}