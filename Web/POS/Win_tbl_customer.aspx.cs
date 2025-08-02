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

namespace Web.POS
{
    public partial class Win_tbl_customer : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        string Uname = "";
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Uname = (((USER_MST)Session["USER"]).LOGIN_ID);
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
                btnAdd.ValidationGroup = "ss";//CRD
            }
        }
        #region Step2
        public void BindData()
        {
            List<Database.Win_tbl_customer> List = DB.Win_tbl_customer.Where(p=>p.TenentID == TID).OrderBy(m => m.ID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lblName1s.Attributes["class"] = lblEmailAddress1s.Attributes["class"] = lblPhone1s.Attributes["class"] = lblAddress1s.Attributes["class"] = lblCity1s.Attributes["class"] = lblPeopleType1s.Attributes["class"] = lblFacebook1s.Attributes["class"] = lblTwitter1s.Attributes["class"] = lblInsta1s.Attributes["class"] = lblNameArabic1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblName2h.Attributes["class"] = lblEmailAddress2h.Attributes["class"] = lblPhone2h.Attributes["class"] = lblAddress2h.Attributes["class"] = lblCity2h.Attributes["class"] = lblPeopleType2h.Attributes["class"] = lblFacebook2h.Attributes["class"] = lblTwitter2h.Attributes["class"] = lblInsta2h.Attributes["class"] = lblNameArabic2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblName1s.Attributes["class"] = lblEmailAddress1s.Attributes["class"] = lblPhone1s.Attributes["class"] = lblAddress1s.Attributes["class"] = lblCity1s.Attributes["class"] = lblPeopleType1s.Attributes["class"] = lblFacebook1s.Attributes["class"] = lblTwitter1s.Attributes["class"] = lblInsta1s.Attributes["class"] = lblNameArabic1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblName2h.Attributes["class"] = lblEmailAddress2h.Attributes["class"] = lblPhone2h.Attributes["class"] = lblAddress2h.Attributes["class"] = lblCity2h.Attributes["class"] = lblPeopleType2h.Attributes["class"] = lblFacebook2h.Attributes["class"] = lblTwitter2h.Attributes["class"] = lblInsta2h.Attributes["class"] = lblNameArabic2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //txtID.Text = "";
            txtName.Text = "";
            txtEmailAddress.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            drpPeopleType.SelectedIndex = 0;            
            txtFacebook.Text = "";
            txtTwitter.Text = "";
            txtInsta.Text = "";
            txtNameArabic.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "Add Customer")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Save";
                        btnAdd.ValidationGroup = "CRD";
                    }
                    else if (btnAdd.Text == "Save")
                    {
                        Database.Win_tbl_customer objWin_tbl_customer = new Database.Win_tbl_customer();
                        objWin_tbl_customer.TenentID = TID;
                        objWin_tbl_customer.ID = DB.Win_tbl_customer.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Win_tbl_customer.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                        objWin_tbl_customer.Name = txtName.Text;
                        objWin_tbl_customer.EmailAddress = txtEmailAddress.Text;
                        objWin_tbl_customer.Phone = txtPhone.Text;
                        objWin_tbl_customer.Address = txtAddress.Text;
                        objWin_tbl_customer.City = txtCity.Text;
                        objWin_tbl_customer.PeopleType = drpPeopleType.SelectedValue;
                        objWin_tbl_customer.UploadDate = DateTime.Now;
                        objWin_tbl_customer.Uploadby = Uname;
                        objWin_tbl_customer.SyncDate = DateTime.Now;
                        objWin_tbl_customer.Syncby = Uname;
                        objWin_tbl_customer.SynID = 9;
                        objWin_tbl_customer.Facebook = txtFacebook.Text;
                        objWin_tbl_customer.Twitter = txtTwitter.Text;
                        objWin_tbl_customer.Insta = txtInsta.Text;
                        objWin_tbl_customer.NameArabic = txtNameArabic.Text;


                        DB.Win_tbl_customer.AddObject(objWin_tbl_customer);
                        DB.SaveChanges();

                        String url = "insert new record in Win_tbl_customer with " + "TenentID = " + TID + "ID =" + objWin_tbl_customer.ID;
                        String evantname = "create";
                        String tablename = "Win_tbl_customer";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                        Clear();
                        btnAdd.Text = "Add Customer";
                        lblMsg.Text = "  Data Save Successfully";
                        btnAdd.ValidationGroup = "ss";
                        pnlSuccessMsg.Visible = true;
                        //BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.Win_tbl_customer objWin_tbl_customer = DB.Win_tbl_customer.Single(p =>p.TenentID == TID && p.ID == ID);
                            objWin_tbl_customer.Name = txtName.Text;
                            objWin_tbl_customer.EmailAddress = txtEmailAddress.Text;
                            objWin_tbl_customer.Phone = txtPhone.Text;
                            objWin_tbl_customer.Address = txtAddress.Text;
                            objWin_tbl_customer.City = txtCity.Text;
                            objWin_tbl_customer.PeopleType = drpPeopleType.SelectedValue;                           
                            objWin_tbl_customer.Facebook = txtFacebook.Text;
                            objWin_tbl_customer.Twitter = txtTwitter.Text;
                            objWin_tbl_customer.Insta = txtInsta.Text;
                            objWin_tbl_customer.NameArabic = txtNameArabic.Text;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add Customer";
                            DB.SaveChanges();
                            String url = "update Win_tbl_customer with " + "TenentID = " + TID + "ID =" + ID;
                            String evantname = "Update";
                            String tablename = "Win_tbl_customer";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                            btnAdd.ValidationGroup = "ss";
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            //BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
                catch
                {
                    
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            Response.Redirect("Win_tbl_customer.aspx");
        }
        public void FillContractorID()
        {
            //drpNameArabic.Items.Insert(0, new ListItem("-- Select --", "0"));drpNameArabic.DataSource = DB.0;drpNameArabic.DataTextField = "0";drpNameArabic.DataValueField = "0";drpNameArabic.DataBind();
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
        public void navigate()
        {
            txtName.Text = Listview1.SelectedDataKey[0].ToString();
            txtEmailAddress.Text = Listview1.SelectedDataKey[0].ToString();
            txtPhone.Text = Listview1.SelectedDataKey[0].ToString();
            txtAddress.Text = Listview1.SelectedDataKey[0].ToString();
            txtCity.Text = Listview1.SelectedDataKey[0].ToString();
            drpPeopleType.Text = Listview1.SelectedDataKey[0].ToString();
            txtFacebook.Text = Listview1.SelectedDataKey[0].ToString();
            txtTwitter.Text = Listview1.SelectedDataKey[0].ToString();
            txtInsta.Text = Listview1.SelectedDataKey[0].ToString();
            txtNameArabic.Text = Listview1.SelectedDataKey[0].ToString();
        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            navigate();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                navigate();
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
                navigate();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            navigate();
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblName2h.Visible = lblEmailAddress2h.Visible = lblPhone2h.Visible = lblAddress2h.Visible = lblCity2h.Visible = lblPeopleType2h.Visible = lblFacebook2h.Visible = lblTwitter2h.Visible = lblInsta2h.Visible = lblNameArabic2h.Visible = false;
                    //2true
                    txtName2h.Visible = txtEmailAddress2h.Visible = txtPhone2h.Visible = txtAddress2h.Visible = txtCity2h.Visible = txtPeopleType2h.Visible = txtFacebook2h.Visible = txtTwitter2h.Visible = txtInsta2h.Visible = txtNameArabic2h.Visible = true;

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
                    lblName2h.Visible = lblEmailAddress2h.Visible = lblPhone2h.Visible = lblAddress2h.Visible = lblCity2h.Visible = lblPeopleType2h.Visible = lblFacebook2h.Visible = lblTwitter2h.Visible = lblInsta2h.Visible = lblNameArabic2h.Visible = true;
                    //2false
                    txtName2h.Visible = txtEmailAddress2h.Visible = txtPhone2h.Visible = txtAddress2h.Visible = txtCity2h.Visible = txtPeopleType2h.Visible = txtFacebook2h.Visible = txtTwitter2h.Visible = txtInsta2h.Visible = txtNameArabic2h.Visible = false;

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
                    lblName1s.Visible = lblEmailAddress1s.Visible = lblPhone1s.Visible = lblAddress1s.Visible = lblCity1s.Visible = lblPeopleType1s.Visible = lblFacebook1s.Visible = lblTwitter1s.Visible = lblInsta1s.Visible = lblNameArabic1s.Visible = false;
                    //1true
                    txtName1s.Visible = txtEmailAddress1s.Visible = txtPhone1s.Visible = txtAddress1s.Visible = txtCity1s.Visible = txtPeopleType1s.Visible = txtFacebook1s.Visible = txtTwitter1s.Visible = txtInsta1s.Visible = txtNameArabic1s.Visible = true;
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
                    lblName1s.Visible = lblEmailAddress1s.Visible = lblPhone1s.Visible = lblAddress1s.Visible = lblCity1s.Visible = lblPeopleType1s.Visible = lblFacebook1s.Visible = lblTwitter1s.Visible = lblInsta1s.Visible = lblNameArabic1s.Visible = true;
                    //1false
                    txtName1s.Visible = txtEmailAddress1s.Visible = txtPhone1s.Visible = txtAddress1s.Visible = txtCity1s.Visible = txtPeopleType1s.Visible = txtFacebook1s.Visible = txtTwitter1s.Visible = txtInsta1s.Visible = txtNameArabic1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public string getOwnPage()
        {
            string PageID = "0";
            if (Request.Url.AbsolutePath.Contains("Win_tbl_customer.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Win_tbl_customer.aspx").TableName.ToString();
            }
            return PageID;
        }
        public List<Database.TBLLabelDTL> Bindxml(string pagename)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("//POS//xml//" + pagename + ".xml"));
            List<Database.TBLLabelDTL> LblList = new List<Database.TBLLabelDTL>();
            if (ds != null && ds.HasChanges())
            {
                //ID,LabelMstID,FieldName,LabelID,Labe  lName,LangID,COUNTRYID,LANGDISP,Active

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Database.TBLLabelDTL obj = new Database.TBLLabelDTL();
                    LblList.Add(new Database.TBLLabelDTL
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        LabelMstID = dr["LabelMstID"].ToString(),
                        FieldName = dr["FieldName"].ToString(),
                        LabelID = dr["LabelID"].ToString(),
                        LabelName = dr["LabelName"].ToString(),
                        LangID = Convert.ToInt32(dr["LangID"]),
                        COUNTRYID = Convert.ToInt32(dr["COUNTRYID"]),
                        LANGDISP = dr["LANGDISP"].ToString(),
                        Active = Convert.ToBoolean(dr["Active"])
                    });
                }
                //string lang = "en-US";
                //int PID = ((AcmMaster)this.Master).getOwnPage();
                //List<TBLLabelDTL> List = LblList.Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();

            }
            return LblList;
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = getOwnPage();

            List<Database.TBLLabelDTL> List = Bindxml("Win_tbl_customer").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblName1s.ID == item.LabelID)
                    txtName1s.Text = lblName1s.Text = lblhName.Text = item.LabelName;
                else if (lblEmailAddress1s.ID == item.LabelID)
                    txtEmailAddress1s.Text = lblEmailAddress1s.Text = lblhEmailAddress.Text = item.LabelName;
                else if (lblPhone1s.ID == item.LabelID)
                    txtPhone1s.Text = lblPhone1s.Text = lblhPhone.Text = item.LabelName;
                else if (lblAddress1s.ID == item.LabelID)
                    txtAddress1s.Text = lblAddress1s.Text = lblhAddress.Text = item.LabelName;
                else if (lblCity1s.ID == item.LabelID)
                    txtCity1s.Text = lblCity1s.Text = lblhCity.Text = item.LabelName;
                else if (lblPeopleType1s.ID == item.LabelID)
                    txtPeopleType1s.Text = lblPeopleType1s.Text = lblhPeopleType.Text = item.LabelName;
                else if (lblFacebook1s.ID == item.LabelID)
                    txtFacebook1s.Text = lblFacebook1s.Text = item.LabelName;
                else if (lblTwitter1s.ID == item.LabelID)
                    txtTwitter1s.Text = lblTwitter1s.Text = item.LabelName;
                else if (lblInsta1s.ID == item.LabelID)
                    txtInsta1s.Text = lblInsta1s.Text = item.LabelName;
                else if (lblNameArabic1s.ID == item.LabelID)
                    txtNameArabic1s.Text = lblNameArabic1s.Text = item.LabelName;

                else if (lblName2h.ID == item.LabelID)
                    txtName2h.Text = lblName2h.Text = lblhName.Text = item.LabelName;
                else if (lblEmailAddress2h.ID == item.LabelID)
                    txtEmailAddress2h.Text = lblEmailAddress2h.Text = lblhEmailAddress.Text = item.LabelName;
                else if (lblPhone2h.ID == item.LabelID)
                    txtPhone2h.Text = lblPhone2h.Text = lblhPhone.Text = item.LabelName;
                else if (lblAddress2h.ID == item.LabelID)
                    txtAddress2h.Text = lblAddress2h.Text = lblhAddress.Text = item.LabelName;
                else if (lblCity2h.ID == item.LabelID)
                    txtCity2h.Text = lblCity2h.Text = lblhCity.Text = item.LabelName;
                else if (lblPeopleType2h.ID == item.LabelID)
                    txtPeopleType2h.Text = lblPeopleType2h.Text = lblhPeopleType.Text = item.LabelName;
                else if (lblFacebook2h.ID == item.LabelID)
                    txtFacebook2h.Text = lblFacebook2h.Text = item.LabelName;
                else if (lblTwitter2h.ID == item.LabelID)
                    txtTwitter2h.Text = lblTwitter2h.Text = item.LabelName;
                else if (lblInsta2h.ID == item.LabelID)
                    txtInsta2h.Text = lblInsta2h.Text = item.LabelName;
                else if (lblNameArabic2h.ID == item.LabelID)
                    txtNameArabic2h.Text = lblNameArabic2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = Bindxml("Win_tbl_customer").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\POS\\xml\\Win_tbl_customer.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                //var obj = Bindxml().Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                var obj = List.Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName1s.Text;
                else if (lblEmailAddress1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmailAddress1s.Text;
                else if (lblPhone1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone1s.Text;
                else if (lblAddress1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAddress1s.Text;
                else if (lblCity1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCity1s.Text;
                else if (lblPeopleType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPeopleType1s.Text;
                else if (lblFacebook1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFacebook1s.Text;
                else if (lblTwitter1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTwitter1s.Text;
                else if (lblInsta1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInsta1s.Text;
                else if (lblNameArabic1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNameArabic1s.Text;

                else if (lblName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName2h.Text;
                else if (lblEmailAddress2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEmailAddress2h.Text;
                else if (lblPhone2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPhone2h.Text;
                else if (lblAddress2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAddress2h.Text;
                else if (lblCity2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCity2h.Text;
                else if (lblPeopleType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPeopleType2h.Text;
                else if (lblFacebook2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFacebook2h.Text;
                else if (lblTwitter2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTwitter2h.Text;
                else if (lblInsta2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInsta2h.Text;
                else if (lblNameArabic2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtNameArabic2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\POS\\xml\\Win_tbl_customer.xml"));

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
            txtName.Enabled = true;
            txtEmailAddress.Enabled = true;
            txtPhone.Enabled = true;
            txtAddress.Enabled = true;
            txtCity.Enabled = true;
            drpPeopleType.Enabled = true;           
            txtFacebook.Enabled = true;
            txtTwitter.Enabled = true;
            txtInsta.Enabled = true;
            txtNameArabic.Enabled = true;
        }
        public void Readonly()
        {
            //navigation.Visible = true;           
            txtName.Enabled = false;
            txtEmailAddress.Enabled = false;
            txtPhone.Enabled = false;
            txtAddress.Enabled = false;
            txtCity.Enabled = false;
            drpPeopleType.Enabled = false;           
            txtFacebook.Enabled = false;
            txtTwitter.Enabled = false;
            txtInsta.Enabled = false;
            txtNameArabic.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.Win_tbl_customer.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((HRMMater)Page.Master).BindList(Listview1, (DB.Win_tbl_customer.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

        //    ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.Win_tbl_customer.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((HRMMater)Page.Master).BindList(Listview1, (DB.Win_tbl_customer.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
        //        ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.Win_tbl_customer.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((HRMMater)Page.Master).BindList(Listview1, (DB.Win_tbl_customer.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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
        //    int Totalrec = DB.Win_tbl_customer.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((HRMMater)Page.Master).BindList(Listview1, (DB.Win_tbl_customer.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((HRMMater)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
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

                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.Win_tbl_customer objSOJobDesc = DB.Win_tbl_customer.Single(p =>p.TenentID == TID && p.ID == ID);
                        //objSOJobDesc.Active = false;
                        DB.SaveChanges();

                        String url = "Delete Win_tbl_customer with " + "TenentID = " + TID + "ID =" + ID;
                        String evantname = "Delete";
                        String tablename = "Win_tbl_customer";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                        BindData();
                       
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);

                        Database.Win_tbl_customer objWin_tbl_customer = DB.Win_tbl_customer.Single(p => p.TenentID == TID && p.ID == ID);
                        txtName.Text = objWin_tbl_customer.Name.ToString();
                        txtEmailAddress.Text = objWin_tbl_customer.EmailAddress.ToString();
                        txtPhone.Text = objWin_tbl_customer.Phone.ToString();
                        txtAddress.Text = objWin_tbl_customer.Address.ToString();
                        txtCity.Text = objWin_tbl_customer.City.ToString();
                        drpPeopleType.SelectedValue = objWin_tbl_customer.PeopleType.ToString();
                        if (objWin_tbl_customer.Facebook != null)
                        txtFacebook.Text = objWin_tbl_customer.Facebook.ToString();
                        if (objWin_tbl_customer.Twitter != null)
                        txtTwitter.Text = objWin_tbl_customer.Twitter.ToString();
                        if (objWin_tbl_customer.Insta != null)
                        txtInsta.Text = objWin_tbl_customer.Insta.ToString();
                        txtNameArabic.Text = objWin_tbl_customer.NameArabic.ToString();
                        btnAdd.ValidationGroup = "CRD";
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
        //    int Totalrec = DB.Win_tbl_customer.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((HRMMater)Page.Master).BindList(Listview1, (DB.Win_tbl_customer.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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