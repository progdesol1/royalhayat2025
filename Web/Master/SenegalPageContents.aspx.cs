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
    public partial class SenegalPageContents : System.Web.UI.Page
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
                //FirstData();
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.SenegalPageContent> List = DB.SenegalPageContents.OrderBy(m => m.ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblPageName1s.Attributes["class"] = lblPageTitle1s.Attributes["class"] = lblMetaDescription1s.Attributes["class"] = lblMetaKeyword1s.Attributes["class"] = lblEPageContent1s.Attributes["class"] = lblAPageContent1s.Attributes["class"] = lblFPageContent1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPageName2h.Attributes["class"] = lblPageTitle2h.Attributes["class"] = lblMetaDescription2h.Attributes["class"] = lblMetaKeyword2h.Attributes["class"] = lblEPageContent2h.Attributes["class"] = lblAPageContent2h.Attributes["class"] = lblFPageContent2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPageName1s.Attributes["class"] = lblPageTitle1s.Attributes["class"] = lblMetaDescription1s.Attributes["class"] = lblMetaKeyword1s.Attributes["class"] = lblEPageContent1s.Attributes["class"] = lblAPageContent1s.Attributes["class"] = lblFPageContent1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPageName2h.Attributes["class"] = lblPageTitle2h.Attributes["class"] = lblMetaDescription2h.Attributes["class"] = lblMetaKeyword2h.Attributes["class"] = lblEPageContent2h.Attributes["class"] = lblAPageContent2h.Attributes["class"] = lblFPageContent2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtPageName.Text = "";
            txtPageTitle.Text = "";
            txtMetaDescription.Text = "";
            txtMetaKeyword.Text = "";
            CKEEPageContent.Text = "";
            CKEAPageContent.Text = "";
            CKEFPageContent.Text = "";
            
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
                        Database.SenegalPageContent objSenegalPageContents = new Database.SenegalPageContent();
                        //Server Content Send data Yogesh
                        objSenegalPageContents.PageName = txtPageName.Text;
                        objSenegalPageContents.PageTitle = txtPageTitle.Text;
                        objSenegalPageContents.MetaDescription = txtMetaDescription.Text;
                        objSenegalPageContents.MetaKeyword = txtMetaKeyword.Text;
                        objSenegalPageContents.EPageContent = CKEEPageContent.Text;
                        objSenegalPageContents.APageContent = CKEAPageContent.Text;
                        objSenegalPageContents.FPageContent = CKEFPageContent.Text;
                        objSenegalPageContents.CreatedDate = DateTime.Now;

                        DB.SenegalPageContents.AddObject(objSenegalPageContents);
                        DB.SaveChanges();
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        ////FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.SenegalPageContent objSenegalPageContents = DB.SenegalPageContents.Single(p => p.ID == ID);
                            objSenegalPageContents.PageName = txtPageName.Text;
                            objSenegalPageContents.PageTitle = txtPageTitle.Text;
                            objSenegalPageContents.MetaDescription = txtMetaDescription.Text;
                            objSenegalPageContents.MetaKeyword = txtMetaKeyword.Text;
                            objSenegalPageContents.EPageContent = CKEEPageContent.Text;
                            objSenegalPageContents.APageContent = CKEAPageContent.Text;
                            objSenegalPageContents.FPageContent = CKEFPageContent.Text;
                            

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
            //Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCreatedDate.Items.Insert(0, new ListItem("-- Select --", "0"));drpCreatedDate.DataSource = DB.0;drpCreatedDate.DataTextField = "0";drpCreatedDate.DataValueField = "0";drpCreatedDate.DataBind();
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            //FirstData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //NextData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //PrevData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            //LastData();
        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            txtPageName.Text = Listview1.SelectedDataKey[0].ToString();
            txtPageTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtMetaDescription.Text = Listview1.SelectedDataKey[0].ToString();
            txtMetaKeyword.Text = Listview1.SelectedDataKey[0].ToString();
            CKEEPageContent.Text = Listview1.SelectedDataKey[0].ToString();
            CKEAPageContent.Text = Listview1.SelectedDataKey[0].ToString();
            CKEFPageContent.Text = Listview1.SelectedDataKey[0].ToString();
            

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtPageName.Text = Listview1.SelectedDataKey[0].ToString();
                txtPageTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtMetaDescription.Text = Listview1.SelectedDataKey[0].ToString();
                txtMetaKeyword.Text = Listview1.SelectedDataKey[0].ToString();
                CKEEPageContent.Text = Listview1.SelectedDataKey[0].ToString();
                CKEAPageContent.Text = Listview1.SelectedDataKey[0].ToString();
                CKEFPageContent.Text = Listview1.SelectedDataKey[0].ToString();
                

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
                txtPageName.Text = Listview1.SelectedDataKey[0].ToString();
                txtPageTitle.Text = Listview1.SelectedDataKey[0].ToString();
                txtMetaDescription.Text = Listview1.SelectedDataKey[0].ToString();
                txtMetaKeyword.Text = Listview1.SelectedDataKey[0].ToString();
                CKEEPageContent.Text = Listview1.SelectedDataKey[0].ToString();
                CKEAPageContent.Text = Listview1.SelectedDataKey[0].ToString();
                CKEFPageContent.Text = Listview1.SelectedDataKey[0].ToString();
                

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtPageName.Text = Listview1.SelectedDataKey[0].ToString();
            txtPageTitle.Text = Listview1.SelectedDataKey[0].ToString();
            txtMetaDescription.Text = Listview1.SelectedDataKey[0].ToString();
            txtMetaKeyword.Text = Listview1.SelectedDataKey[0].ToString();
            CKEEPageContent.Text = Listview1.SelectedDataKey[0].ToString();
            CKEAPageContent.Text = Listview1.SelectedDataKey[0].ToString();
            CKEFPageContent.Text = Listview1.SelectedDataKey[0].ToString();
            

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblPageName2h.Visible = lblPageTitle2h.Visible = lblMetaDescription2h.Visible = lblMetaKeyword2h.Visible = lblEPageContent2h.Visible = lblAPageContent2h.Visible = lblFPageContent2h.Visible = false;
                    //2true
                    txtPageName2h.Visible = txtPageTitle2h.Visible = txtMetaDescription2h.Visible = txtMetaKeyword2h.Visible = txtEPageContent2h.Visible = txtAPageContent2h.Visible = txtFPageContent2h.Visible = true;

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
                    lblPageName2h.Visible = lblPageTitle2h.Visible = lblMetaDescription2h.Visible = lblMetaKeyword2h.Visible = lblEPageContent2h.Visible = lblAPageContent2h.Visible = lblFPageContent2h.Visible = true;
                    //2false
                    txtPageName2h.Visible = txtPageTitle2h.Visible = txtMetaDescription2h.Visible = txtMetaKeyword2h.Visible = txtEPageContent2h.Visible = txtAPageContent2h.Visible = txtFPageContent2h.Visible = false;

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
                    lblPageName1s.Visible = lblPageTitle1s.Visible = lblMetaDescription1s.Visible = lblMetaKeyword1s.Visible = lblEPageContent1s.Visible = lblAPageContent1s.Visible = lblFPageContent1s.Visible = false;
                    //1true
                    txtPageName1s.Visible = txtPageTitle1s.Visible = txtMetaDescription1s.Visible = txtMetaKeyword1s.Visible = txtEPageContent1s.Visible = txtAPageContent1s.Visible = txtFPageContent1s.Visible = true;
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
                    lblPageName1s.Visible = lblPageTitle1s.Visible = lblMetaDescription1s.Visible = lblMetaKeyword1s.Visible = lblEPageContent1s.Visible = lblAPageContent1s.Visible = lblFPageContent1s.Visible = true;
                    //1false
                    txtPageName1s.Visible = txtPageTitle1s.Visible = txtMetaDescription1s.Visible = txtMetaKeyword1s.Visible = txtEPageContent1s.Visible = txtAPageContent1s.Visible = txtFPageContent1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("SenegalPageContents").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblPageName1s.ID == item.LabelID)
                    txtPageName1s.Text = lblPageName1s.Text = lblhPageName.Text = item.LabelName;
                else if (lblPageTitle1s.ID == item.LabelID)
                    txtPageTitle1s.Text = lblPageTitle1s.Text = lblhPageTitle.Text = item.LabelName;
                else if (lblMetaDescription1s.ID == item.LabelID)
                    txtMetaDescription1s.Text = lblMetaDescription1s.Text = lblhMetaDescription.Text = item.LabelName;
                else if (lblMetaKeyword1s.ID == item.LabelID)
                    txtMetaKeyword1s.Text = lblMetaKeyword1s.Text = lblhMetaKeyword.Text = item.LabelName;
                else if (lblEPageContent1s.ID == item.LabelID)
                    txtEPageContent1s.Text = lblEPageContent1s.Text = lblhEPageContent.Text = item.LabelName;
                else if (lblAPageContent1s.ID == item.LabelID)
                    txtAPageContent1s.Text = lblAPageContent1s.Text = lblhAPageContent.Text = item.LabelName;
                else if (lblFPageContent1s.ID == item.LabelID)
                    txtFPageContent1s.Text = lblFPageContent1s.Text = lblhFPageContent.Text = item.LabelName;
                
                else if (lblPageName2h.ID == item.LabelID)
                    txtPageName2h.Text = lblPageName2h.Text = lblhPageName.Text = item.LabelName;
                else if (lblPageTitle2h.ID == item.LabelID)
                    txtPageTitle2h.Text = lblPageTitle2h.Text = lblhPageTitle.Text = item.LabelName;
                else if (lblMetaDescription2h.ID == item.LabelID)
                    txtMetaDescription2h.Text = lblMetaDescription2h.Text = lblhMetaDescription.Text = item.LabelName;
                else if (lblMetaKeyword2h.ID == item.LabelID)
                    txtMetaKeyword2h.Text = lblMetaKeyword2h.Text = lblhMetaKeyword.Text = item.LabelName;
                else if (lblEPageContent2h.ID == item.LabelID)
                    txtEPageContent2h.Text = lblEPageContent2h.Text = lblhEPageContent.Text = item.LabelName;
                else if (lblAPageContent2h.ID == item.LabelID)
                    txtAPageContent2h.Text = lblAPageContent2h.Text = lblhAPageContent.Text = item.LabelName;
                else if (lblFPageContent2h.ID == item.LabelID)
                    txtFPageContent2h.Text = lblFPageContent2h.Text = lblhFPageContent.Text = item.LabelName;
                
                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("SenegalPageContents").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\SenegalPageContents.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("SenegalPageContents").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblPageName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageName1s.Text;
                else if (lblPageTitle1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageTitle1s.Text;
                else if (lblMetaDescription1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMetaDescription1s.Text;
                else if (lblMetaKeyword1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMetaKeyword1s.Text;
                else if (lblEPageContent1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEPageContent1s.Text;
                else if (lblAPageContent1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAPageContent1s.Text;
                else if (lblFPageContent1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFPageContent1s.Text;
               
                else if (lblPageName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageName2h.Text;
                else if (lblPageTitle2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPageTitle2h.Text;
                else if (lblMetaDescription2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMetaDescription2h.Text;
                else if (lblMetaKeyword2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMetaKeyword2h.Text;
                else if (lblEPageContent2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEPageContent2h.Text;
                else if (lblAPageContent2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAPageContent2h.Text;
                else if (lblFPageContent2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFPageContent2h.Text;
                
                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\SenegalPageContents.xml"));

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
            txtPageName.Enabled = true;
            txtPageTitle.Enabled = true;
            txtMetaDescription.Enabled = true;
            txtMetaKeyword.Enabled = true;
            CKEEPageContent.Enabled = true;
            CKEAPageContent.Enabled = true;
            CKEFPageContent.Enabled = true;
           
        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtPageName.Enabled = false;
            txtPageTitle.Enabled = false;
            txtMetaDescription.Enabled = false;
            txtMetaKeyword.Enabled = false;
            CKEEPageContent.Enabled = false;
            CKEAPageContent.Enabled = false;
            CKEFPageContent.Enabled = false;
           
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.SenegalPageContents.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.SenegalPageContents.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2,Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.SenegalPageContents.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.SenegalPageContents.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2,Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.SenegalPageContents.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.SenegalPageContents.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2,Totalrec);
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
        //    int Totalrec = DB.SenegalPageContents.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.SenegalPageContents.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2,Totalrec);
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
                        Database.SenegalPageContent objSOJobDesc = DB.SenegalPageContents.Single(p => p.ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                       
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.SenegalPageContent objSenegalPageContents = DB.SenegalPageContents.Single(p => p.ID == ID);
                        txtPageName.Text = objSenegalPageContents.PageName.ToString();
                        txtPageTitle.Text = objSenegalPageContents.PageTitle.ToString();
                        txtMetaDescription.Text = objSenegalPageContents.MetaDescription.ToString();
                        txtMetaKeyword.Text = objSenegalPageContents.MetaKeyword.ToString();
                        CKEEPageContent.Text = objSenegalPageContents.EPageContent.ToString();
                        CKEAPageContent.Text = objSenegalPageContents.APageContent.ToString();
                        CKEFPageContent.Text = objSenegalPageContents.FPageContent.ToString();
                       
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
        //    int Totalrec = DB.SenegalPageContents.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.SenegalPageContents.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2,Totalrec);
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