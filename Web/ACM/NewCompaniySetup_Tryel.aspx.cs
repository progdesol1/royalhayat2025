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
    public partial class NewCompaniySetup_Tryel : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                btnAdd.ValidationGroup = "ss";
                ////FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.NewCompaniySetup_Tryel> List = DB.NewCompaniySetup_Tryel.Where(p => p.Deleteby == true).OrderBy(m => m.MyID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((ACMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblCompanyName1s.Attributes["class"] = lblUserName1s.Attributes["class"] = lblPackage1s.Attributes["class"] = lblNumberofUser1s.Attributes["class"] = lblEmailID1s.Attributes["class"] = lblMobileNo1s.Attributes["class"] = lblCountry1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblCompanyName2h.Attributes["class"] = lblUserName2h.Attributes["class"] = lblPackage2h.Attributes["class"] = lblNumberofUser2h.Attributes["class"] = lblEmailID2h.Attributes["class"] = lblMobileNo2h.Attributes["class"] = lblCountry2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCompanyName1s.Attributes["class"] = lblUserName1s.Attributes["class"] = lblPackage1s.Attributes["class"] = lblNumberofUser1s.Attributes["class"] = lblEmailID1s.Attributes["class"] = lblMobileNo1s.Attributes["class"] = lblCountry1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblCompanyName2h.Attributes["class"] = lblUserName2h.Attributes["class"] = lblPackage2h.Attributes["class"] = lblNumberofUser2h.Attributes["class"] = lblEmailID2h.Attributes["class"] = lblMobileNo2h.Attributes["class"] = lblCountry2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtCompanyName.Text = "";
            txtUserName.Text = "";
            drpPackage.SelectedIndex = 0;
            txtNumberofUser.Text = "";
            txtEmailID.Text = "";
            txtMobile.Text = "";
            drpCountry.SelectedIndex = 0;

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
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        int Myid = 0;
                        string Copmany = txtCompanyName.Text;
                        string UserName = txtUserName.Text;
                        int Packge = Convert.ToInt32(drpPackage.SelectedValue);
                        int NumOfUser = Convert.ToInt32(txtNumberofUser.Text);
                        string email = txtEmailID.Text;
                        string Mobile = txtMobile.Text;
                        int CountryID = Convert.ToInt32(drpCountry.SelectedValue);
                        string DatainserStatest = "Insert";
                        Classes.EcommAdminClass.insertNewComapnySetupTry(Myid, Copmany, UserName, Packge, NumOfUser, email, Mobile, CountryID, DatainserStatest);

                        Clear();
                        btnAdd.Text = "Add New";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        btnAdd.ValidationGroup = "ss";
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            int Myid = ID;
                            string Copmany = txtCompanyName.Text;
                            string UserName = txtUserName.Text;
                            int Packge = Convert.ToInt32(drpPackage.SelectedValue);
                            int NumOfUser = Convert.ToInt32(txtNumberofUser.Text);
                            string email = txtEmailID.Text;
                            string Mobile = txtMobile.Text;
                            int CountryID = Convert.ToInt32(drpCountry.SelectedValue);
                            string DatainserStatest = "Update";
                            Classes.EcommAdminClass.insertNewComapnySetupTry(Myid, Copmany, UserName, Packge, NumOfUser, email, Mobile, CountryID, DatainserStatest);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            btnAdd.ValidationGroup = "ss";
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
            //drpcrupID.Items.Insert(0, new ListItem("-- Select --", "0"));drpcrupID.DataSource = DB.0;drpcrupID.DataTextField = "0";drpcrupID.DataValueField = "0";drpcrupID.DataBind();
            //drpCountry.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == 0);
            //drpCountry.DataTextField = "COUNAME1";
            //drpCountry.DataValueField = "COUNTRYID";
            //drpCountry.DataBind();
            //drpCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            Classes.EcommAdminClass.getdropdown(drpCountry, 0, "", "", "", "tblCOUNTRY");

            //drpPackage.DataSource = DB.REFTABLEs.Where(p => p.TenentID == 0 && p.REFTYPE == "function" && p.REFSUBTYPE == "financial");
            //drpPackage.DataTextField = "REFNAME1";
            //drpPackage.DataValueField = "REFID";
            //drpPackage.DataBind();
            //drpPackage.Items.Insert(0, new ListItem("-- Select --", "0"));
            Classes.EcommAdminClass.getdropdown(drpPackage, 0, "function", "financial", "", "REFTABLE");
        }
        public string GetContry(int ConID)
        {
            var Cont = DB.tblCOUNTRies.Where(p => p.TenentID == 0 && p.COUNTRYID == ConID);
            if (Cont.Count() > 0)
            {
                if (ConID == 0)
                {
                    return "Not Found";
                }
                else
                {
                    string Conname = DB.tblCOUNTRies.Single(p => p.TenentID == 0 && p.COUNTRYID == ConID).COUNAME1;
                    return Conname;
                }
            }
            else
            {
                return "Not Found";
            }

        }
        public string GetPack(int pack)
        {
            var package = DB.REFTABLEs.Where(p => p.TenentID == 0 && p.REFID == pack);
            if (package.Count() > 0)
            {
                if (pack == 0)
                {
                    return "Not Found";
                }
                else
                {
                    string PackName = DB.REFTABLEs.Single(p => p.TenentID == 0 && p.REFID == pack).REFNAME1;
                    return PackName;
                }
            }
            else
            {
                return "Not Found";
            }
            
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
        public void ManageData()
        {
            if (Listview1.SelectedDataKey[3] != null)
                txtCompanyName.Text = Listview1.SelectedDataKey[3].ToString();
            if (Listview1.SelectedDataKey[4] != null)
                txtUserName.Text = Listview1.SelectedDataKey[4].ToString();
            if (Listview1.SelectedDataKey[5] != null)
                drpPackage.SelectedValue = Listview1.SelectedDataKey[5].ToString();
            if (Listview1.SelectedDataKey[6] != null)
                txtNumberofUser.Text = Listview1.SelectedDataKey[6].ToString();
            if (Listview1.SelectedDataKey[7] != null)
                txtEmailID.Text = Listview1.SelectedDataKey[7].ToString();
            if (Listview1.SelectedDataKey[8] != null)
                txtMobile.Text = Listview1.SelectedDataKey[8].ToString();
            if (Listview1.SelectedDataKey[9] != null)
                drpCountry.SelectedValue = Listview1.SelectedDataKey[9].ToString();
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
                    lblCompanyName2h.Visible = lblUserName2h.Visible = lblPackage2h.Visible = lblNumberofUser2h.Visible = lblEmailID2h.Visible = lblMobileNo2h.Visible = lblCountry2h.Visible = false;
                    //2true
                    txtCompanyName2h.Visible = txtUserName2h.Visible = txtPackage2h.Visible = txtNumberofUser2h.Visible = txtEmailID2h.Visible = txtMobileNo2h.Visible = txtCountry2h.Visible = true;

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
                    lblCompanyName2h.Visible = lblUserName2h.Visible = lblPackage2h.Visible = lblNumberofUser2h.Visible = lblEmailID2h.Visible = lblMobileNo2h.Visible = lblCountry2h.Visible = true;
                    //2false
                    txtCompanyName2h.Visible = txtUserName2h.Visible = txtPackage2h.Visible = txtNumberofUser2h.Visible = txtEmailID2h.Visible = txtMobileNo2h.Visible = txtCountry2h.Visible = false;

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
                    lblCompanyName1s.Visible = lblUserName1s.Visible = lblPackage1s.Visible = lblNumberofUser1s.Visible = lblEmailID1s.Visible = lblMobileNo1s.Visible = lblCountry1s.Visible = false;
                    //1true
                    txtCompanyName1s.Visible = txtUserName1s.Visible = txtPackage1s.Visible = txtNumberofUser1s.Visible = txtEmailID1s.Visible = txtMobileNo1s.Visible = txtCountry1s.Visible = true;
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
                    lblCompanyName1s.Visible = lblUserName1s.Visible = lblPackage1s.Visible = lblNumberofUser1s.Visible = lblEmailID1s.Visible = lblMobileNo1s.Visible = lblCountry1s.Visible = true;
                    //1false
                    txtCompanyName1s.Visible = txtUserName1s.Visible = txtPackage1s.Visible = txtNumberofUser1s.Visible = txtEmailID1s.Visible = txtMobileNo1s.Visible = txtCountry1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("NewCompaniySetup_Tryel").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblCompanyName1s.ID == item.LabelID)
                    txtCompanyName1s.Text = lblCompanyName1s.Text = lblhCompanyName.Text = item.LabelName;
                else if (lblUserName1s.ID == item.LabelID)
                    txtUserName1s.Text = lblUserName1s.Text = lblhUserName.Text = item.LabelName;
                else if (lblPackage1s.ID == item.LabelID)
                    txtPackage1s.Text = lblPackage1s.Text = lblhPackage.Text = item.LabelName;
                else if (lblNumberofUser1s.ID == item.LabelID)
                    txtNumberofUser1s.Text = lblNumberofUser1s.Text = lblhNumberofUser.Text = item.LabelName;
                else if (lblEmailID1s.ID == item.LabelID)
                    txtEmailID1s.Text = lblEmailID1s.Text = lblhEmailID.Text = item.LabelName;
                else if (lblMobileNo1s.ID == item.LabelID)
                    txtMobileNo1s.Text = lblMobileNo1s.Text = lblhMobileNo.Text = item.LabelName;
                else if (lblCountry1s.ID == item.LabelID)
                    txtCountry1s.Text = lblCountry1s.Text = lblhCountry.Text = item.LabelName;

                else if (lblCompanyName2h.ID == item.LabelID)
                    txtCompanyName2h.Text = lblCompanyName2h.Text = lblhCompanyName.Text = item.LabelName;
                else if (lblUserName2h.ID == item.LabelID)
                    txtUserName2h.Text = lblUserName2h.Text = lblhUserName.Text = item.LabelName;
                else if (lblPackage2h.ID == item.LabelID)
                    txtPackage2h.Text = lblPackage2h.Text = lblhPackage.Text = item.LabelName;
                else if (lblNumberofUser2h.ID == item.LabelID)
                    txtNumberofUser2h.Text = lblNumberofUser2h.Text = lblhNumberofUser.Text = item.LabelName;
                else if (lblEmailID2h.ID == item.LabelID)
                    txtEmailID2h.Text = lblEmailID2h.Text = lblhEmailID.Text = item.LabelName;
                else if (lblMobileNo2h.ID == item.LabelID)
                    txtMobileNo2h.Text = lblMobileNo2h.Text = lblhMobileNo.Text = item.LabelName;
                else if (lblCountry2h.ID == item.LabelID)
                    txtCountry2h.Text = lblCountry2h.Text = lblhCountry.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((ACMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((ACMMaster)this.Master).Bindxml("NewCompaniySetup_Tryel.xml").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\ACM\\xml\\NewCompaniySetup_Tryel.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((ACMMaster)this.Master).Bindxml("NewCompaniySetup_Tryel").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblCompanyName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCompanyName1s.Text;
                else if (lblUserName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUserName1s.Text;
                else if (lblPackage1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPackage1s.Text;
                else if (lblNumberofUser1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNumberofUser1s.Text;
                else if (lblEmailID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmailID1s.Text;
                else if (lblMobileNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMobileNo1s.Text;
                else if (lblCountry1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCountry1s.Text;

                else if (lblCompanyName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCompanyName2h.Text;
                else if (lblUserName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtUserName2h.Text;
                else if (lblPackage2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPackage2h.Text;
                else if (lblNumberofUser2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNumberofUser2h.Text;
                else if (lblEmailID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmailID2h.Text;
                else if (lblMobileNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMobileNo2h.Text;
                else if (lblCountry2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCountry2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\ACM\\xml\\NewCompaniySetup_Tryel.xml"));

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

            txtCompanyName.Enabled = true;
            txtUserName.Enabled = true;
            drpPackage.Enabled = true;
            txtNumberofUser.Enabled = true;
            txtEmailID.Enabled = true;
            txtMobile.Enabled = true;
            drpCountry.Enabled = true;

        }
        public void Readonly()
        {

            txtCompanyName.Enabled = false;
            txtUserName.Enabled = false;
            drpPackage.Enabled = false;
            txtNumberofUser.Enabled = false;
            txtEmailID.Enabled = false;
            txtMobile.Enabled = false;
            drpCountry.Enabled = false;

        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.NewCompaniySetup_Tryel.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.NewCompaniySetup_Tryel.OrderBy(m => m.MyID).Take(take).Skip(Skip)).ToList());
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

        //    ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.NewCompaniySetup_Tryel.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.NewCompaniySetup_Tryel.OrderBy(m => m.MyID).Take(take).Skip(Skip)).ToList());
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
        //        ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.NewCompaniySetup_Tryel.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.NewCompaniySetup_Tryel.OrderBy(m => m.MyID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
        //    int Totalrec = DB.NewCompaniySetup_Tryel.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((ACMMaster)Page.Master).BindList(Listview1, (DB.NewCompaniySetup_Tryel.OrderBy(m => m.MyID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((ACMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
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

                        Database.NewCompaniySetup_Tryel objSOJobDesc = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == ID);
                        objSOJobDesc.Deleteby = false;
                        DB.SaveChanges();
                        BindData();
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.NewCompaniySetup_Tryel objNewCompaniySetup_Tryel = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == ID);
                        txtCompanyName.Text = objNewCompaniySetup_Tryel.CompanyName.ToString();
                        txtUserName.Text = objNewCompaniySetup_Tryel.UserName.ToString();
                        drpPackage.SelectedValue = objNewCompaniySetup_Tryel.Package.ToString();
                        txtNumberofUser.Text = objNewCompaniySetup_Tryel.NumberofUser.ToString();
                        txtEmailID.Text = objNewCompaniySetup_Tryel.EmailID.ToString();
                        txtMobile.Text = objNewCompaniySetup_Tryel.MobileNo.ToString();
                        drpCountry.SelectedValue = objNewCompaniySetup_Tryel.Country.ToString();

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
        //    int Totalrec = DB.NewCompaniySetup_Tryel.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((ACMMaster)Page.Master).BindList(Listview1, (DB.NewCompaniySetup_Tryel.OrderBy(m => m.MyID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((ACMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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