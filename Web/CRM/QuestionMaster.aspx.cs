using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.CRM
{
    public partial class QuestionMaster : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        List<Database.RowMaster> Raw_List = new List<RowMaster>();
        List<Database.OptionMaster> MultipleRaw_List = new List<OptionMaster>();
        List<Database.OptionMaster> SingleRaw_List = new List<OptionMaster>();
        List<OptionMaster> Option_List = new List<OptionMaster>();
        List<Database.QuestionMaster> Que_List = new List<Database.QuestionMaster>();
        public static int addf = 0;
        int QueID = 0;
        int CreteList = 0;
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                FillDropDown();
                btnAddMainoption.Text = "Add";
                Session["MainOption"] = null;
                Session["SubOption"] = null;
                Session["MultipleValue"] = null;
                Session["SingleValue"] = null;
                ViewState["PreviewID"] = null;
                Bindlistview();
                LastData();
            }

        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
        public void readMode()
        {
            txtquelang1.Enabled = txtquelang2.Enabled = txtquelang3.Enabled = txtqutionnumber.Enabled = txtWeitage.Enabled = drpCategory.Enabled = txtChoice.Enabled = DrpBeforeThisQuestion.Enabled = DrpAfterThisQuestion.Enabled = DrpGroup.Enabled = txtRemark.Enabled = txtOption.Enabled = btnAddMainoption.Enabled = btnCancelMainoption.Enabled = txtSubOption1.Enabled = btnAnswer.Enabled = false;
            btnAdd.Visible = true;
            for (int i = 0; i < ListviewMainOption111.Items.Count; i++)
            {
                LinkButton lnkMainedit = (LinkButton)ListviewMainOption111.Items[i].FindControl("lnkMainedit");
                LinkButton btnMainDelete = (LinkButton)ListviewMainOption111.Items[i].FindControl("btnMainDelete");
                lnkMainedit.Enabled = btnMainDelete.Enabled = false;
            }
            for (int i = 0; i < listsubqution.Items.Count(); i++)
            {
                TextBox txtser = (TextBox)listsubqution.Items[i].FindControl("txtser");
                TextBox txtqutionname = (TextBox)listsubqution.Items[i].FindControl("txtqutionname");
                TextBox txtTotalAns = (TextBox)listsubqution.Items[i].FindControl("txtTotalAns");
                TextBox txtAnsReq = (TextBox)listsubqution.Items[i].FindControl("txtAnsReq");
                TextBox txtAnswer = (TextBox)listsubqution.Items[i].FindControl("txtAnswer");
                TextBox txtPos = (TextBox)listsubqution.Items[i].FindControl("txtPos");
                TextBox txtNeg = (TextBox)listsubqution.Items[i].FindControl("txtNeg");
                DropDownList drpQutintype = (DropDownList)listsubqution.Items[i].FindControl("drpQutintype");
                txtser.Enabled = txtqutionname.Enabled = txtTotalAns.Enabled = txtAnsReq.Enabled = txtAnswer.Enabled = txtPos.Enabled = txtNeg.Enabled = drpQutintype.Enabled = false;
               
            }
            btnSubmit.Enabled = Button3.Enabled = btnSubmitForSing.Enabled = false;
        }
        public void WrietMode()
        {
            txtquelang1.Enabled = txtquelang2.Enabled = txtquelang3.Enabled = txtWeitage.Enabled = drpCategory.Enabled = txtChoice.Enabled = DrpBeforeThisQuestion.Enabled = DrpAfterThisQuestion.Enabled = DrpGroup.Enabled = txtRemark.Enabled = txtOption.Enabled = btnAddMainoption.Enabled = btnCancelMainoption.Enabled = txtSubOption1.Enabled = btnAnswer.Enabled = true;
            for (int i = 0; i < ListviewMainOption111.Items.Count; i++)
            {
                LinkButton lnkMainedit = (LinkButton)ListviewMainOption111.Items[i].FindControl("lnkMainedit");
                LinkButton btnMainDelete = (LinkButton)ListviewMainOption111.Items[i].FindControl("btnMainDelete");
                lnkMainedit.Enabled = btnMainDelete.Enabled = true;
            }
            for (int i = 0; i < listsubqution.Items.Count(); i++)
            {
                TextBox txtser = (TextBox)listsubqution.Items[i].FindControl("txtser");
                TextBox txtqutionname = (TextBox)listsubqution.Items[i].FindControl("txtqutionname");
                TextBox txtTotalAns = (TextBox)listsubqution.Items[i].FindControl("txtTotalAns");
                TextBox txtAnsReq = (TextBox)listsubqution.Items[i].FindControl("txtAnsReq");
                TextBox txtAnswer = (TextBox)listsubqution.Items[i].FindControl("txtAnswer");
                TextBox txtPos = (TextBox)listsubqution.Items[i].FindControl("txtPos");
                TextBox txtNeg = (TextBox)listsubqution.Items[i].FindControl("txtNeg");
                DropDownList drpQutintype = (DropDownList)listsubqution.Items[i].FindControl("drpQutintype");
                txtser.Enabled = txtqutionname.Enabled = txtTotalAns.Enabled = txtAnsReq.Enabled = txtAnswer.Enabled = txtPos.Enabled = txtNeg.Enabled = drpQutintype.Enabled = true;
            }
            btnPreview.Enabled = btnSubmit.Enabled = Button3.Enabled = Button4.Enabled = btnSubmitForSing.Enabled = true;
            btnAdd.Visible = false;
        }
        public void Bindlistview()
        {
            listqution.DataSource = DB.QuestionMasters.Where(p => p.Deleted == true && p.Perent_QID == 0&&p.TenentID==TID).OrderByDescending(p => p.ID);
            listqution.DataBind();
        }
        public void FillDropDown()
        {
            //DrpChoiceType.DataSource = DB.CRM_IsChoice.Where(P => P.TenentID == TID && P.Active == true);
            //DrpChoiceType.DataTextField = "ChoiceType";
            //DrpChoiceType.DataValueField = "ID";
            //DrpChoiceType.DataBind();
            //DrpChoiceType.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(DrpChoiceType, TID, "", "", "", "IsChoice");
            //select * from IsChoice

            Classes.CRMClass.getcrmdropdown(drpchoiceList, TID, "", "", "", "IsChoice");
            //select * from IsChoice

            //Classes.CRMClass.getcrmdropdown(drpChoiceTypeSta, TID, "", "", "", "CRM_IsChoice");
            //DrpBeforeThisQuestion.DataSource = DB.QuestionMaster.Where(P => P.TenentID == TID && P.Avtive == true && P.Deleted == true);
            //DrpBeforeThisQuestion.DataTextField = "QuestionLang1";
            //DrpBeforeThisQuestion.DataValueField = "ID";
            //DrpBeforeThisQuestion.DataBind();
            //DrpBeforeThisQuestion.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(DrpBeforeThisQuestion, TID, "", "", "", "QuestionMaster");
            //select * from QuestionMaster where TenentID = 362

            //DrpAfterThisQuestion.DataSource = DB.QuestionMaster.Where(P => P.TenentID == TID && P.Avtive == true && P.Deleted == true);
            //DrpAfterThisQuestion.DataTextField = "QuestionLang1";
            //DrpAfterThisQuestion.DataValueField = "ID";
            //DrpAfterThisQuestion.DataBind();
            //DrpAfterThisQuestion.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(DrpAfterThisQuestion, TID, "", "", "", "QuestionMaster");
            //select * from QuestionMaster where TenentID = 362

            //DrpGroup.DataSource = DB.TBLGROUP.Where(P => P.ACTIVE == "Y");
            //DrpGroup.DataTextField = "ITGROUPDESC1";
            //DrpGroup.DataValueField = "ITGROUPID";
            //DrpGroup.DataBind();
            //DrpGroup.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(DrpGroup, TID, "", "", "", "TBLGROUP");
            //select * from TBLGROUP

            //drpCategory.DataSource = DB.CAT_MST.Where(P => P.CAT_TYPE == "QUESTION" && P.Active == "1");
            //drpCategory.DataTextField = "cat_name1";
            //drpCategory.DataValueField = "catid";
            //drpCategory.DataBind();
            //drpCategory.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(drpCategory, TID, "QUESTION", "", "", "CAT_MST");
            //select * from CAT_MST
            DrpMainOption.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpChoiceType.SelectedValue) != 0)
            {
                if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 1)
                {
                    pnlOption.Visible = true;
                    pnlValueofdropdowncommaseperator.Visible = true;
                    pnlMutipleyeshowmany.Visible = false;
                    pnlMutipleknowOne.Visible = false;
                    pnlSwappersingledobl.Visible = false;
                    pnlRankingMultiple.Visible = false;
                }
                else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 2)
                {
                    pnlOption.Visible = true;
                    pnlMutipleyeshowmany.Visible = true;
                    pnlValueofdropdowncommaseperator.Visible = false;
                    pnlMutipleknowOne.Visible = false;
                    pnlSwappersingledobl.Visible = false;
                    pnlRankingMultiple.Visible = false;
                    pnlMultiple.Visible = false;
                }
                else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 3)
                {
                    pnlOption.Visible = true;
                    pnlMutipleknowOne.Visible = true;
                    pnlMutipleyeshowmany.Visible = false;
                    pnlValueofdropdowncommaseperator.Visible = false;
                    pnlSwappersingledobl.Visible = false;
                    pnlRankingMultiple.Visible = false;
                    pnlMultiple.Visible = false;
                }
                else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 4)
                {
                    pnlOption.Visible = false;
                    pnlSwappersingledobl.Visible = true;
                    pnlMutipleknowOne.Visible = false;
                    pnlMutipleyeshowmany.Visible = false;
                    pnlValueofdropdowncommaseperator.Visible = false;
                    pnlRankingMultiple.Visible = false;
                    pnlMultiple.Visible = false;
                }
                else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 5)
                {
                    pnlOption.Visible = true;
                    pnlValueofdropdowncommaseperator.Visible = true;
                    pnlMutipleyeshowmany.Visible = false;
                    pnlMutipleknowOne.Visible = false;
                    pnlSwappersingledobl.Visible = false;
                    pnlRankingMultiple.Visible = false;
                }

            }
        }
        protected void btnAddMainoption_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(DrpChoiceType.SelectedValue) != 0)
            //{
            //    if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 1 || Convert.ToInt32(DrpChoiceType.SelectedValue) == 5)
            //    {
            BindMainData();
            btnAnswer.Enabled = true;
            pnlValueofdropdowncommaseperator.Visible = true;
            btnAnswer.Attributes["class"] = btnAnswer.Attributes["class"].Replace("btn default btn-lg disabled", "btn default btn-lg");
            // }
            //else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 2)
            //{
            //    if (Session["MultipleValue"] != null)
            //    {
            //        MultipleRaw_List = (List<OptionMaster>)Session["MultipleValue"];
            //    }
            //    Database.OptionMaster Obj_MulOption = new OptionMaster();
            //    Obj_MulOption.ID = MultipleRaw_List.Count() + 1;
            //    Obj_MulOption.OptionName = txtOption.Text;
            //    MultipleRaw_List.Add(Obj_MulOption);
            //    Session["MultipleValue"] = MultipleRaw_List;
            //    txtOption.Text = "";
            //    CheckBoxListMultiple.DataSource = MultipleRaw_List;
            //    CheckBoxListMultiple.DataTextField = "OptionName";
            //    CheckBoxListMultiple.DataValueField = "ID";
            //    CheckBoxListMultiple.DataBind();
            //}
            //else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 3)
            //{
            //    if (Session["SingleValue"] != null)
            //    {
            //        SingleRaw_List = (List<OptionMaster>)Session["SingleValue"];
            //    }
            //    Database.OptionMaster Obj_SingleOption = new OptionMaster();
            //    Obj_SingleOption.ID = SingleRaw_List.Count() + 1;
            //    Obj_SingleOption.OptionName = txtOption.Text;
            //    SingleRaw_List.Add(Obj_SingleOption);
            //    Session["SingleValue"] = SingleRaw_List;
            //    txtOption.Text = "";
            //    RadioButtonListSingle.DataSource = SingleRaw_List;
            //    RadioButtonListSingle.DataTextField = "OptionName";
            //    RadioButtonListSingle.DataValueField = "ID";
            //    RadioButtonListSingle.DataBind();
            //}
            //else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 4)
            //{
            //}

            // }
        }
        public void BindMainData()
        {
            try
            {

                if (txtOption.Text != "")
                {
                    if (Session["MainOption"] != null)
                    {
                        Option_List = (List<OptionMaster>)Session["MainOption"];
                    }
                    //if(btnAddMainoption.Text!="Update")
                    //{
                    Database.OptionMaster Obj_Option = new OptionMaster();
                    Obj_Option.ID = Option_List.Count() + 1;
                    //if (ViewState["OptionName"] != null)
                    //{
                    //    if (ViewState["OptionName"].ToString() != txtOption.Text)
                    //    {
                    Obj_Option.OptionName = txtOption.Text;
                    ViewState["OptionName"] = txtOption.Text;
                    Obj_Option.SubOption = txtSubOption1.Text;
                    Option_List.Add(Obj_Option);
                    Session["MainOption"] = Option_List;
                    BindMainListOption111();
                    //  pnlMultiple.Visible = true;
                    BindMainOptionDropDown();
                    btnAddMainoption.Text = "Save";
                    txtOption.Text = "";
                    txtSubOption1.Text = "";
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "Option Already Exist..", "Showalert();", true);
                    //    }
                    //}
                    //else
                    //{
                    //    ViewState["OptionName"] = txtOption.Text;
                    //}
                }
            }
            catch (Exception ex) { }
        }
        public void BindMainListOption111()
        {
            List<OptionMaster> List_MainOption = (List<OptionMaster>)Session["MainOption"];
            ListviewMainOption111.DataSource = List_MainOption;
            ListviewMainOption111.DataBind();
        }
        public void BindSubListOption()
        {
            ListviewSubOption.DataSource = Session["SubOption"];
            ListviewSubOption.DataBind();

        }
        //public void BindListQuestion()
        //{
        //    Listview2Question.DataSource = Session["QueAnswer"];
        //    Listview2Question.DataBind();
        //}
        public void BindMainOptionDropDown()
        {
            if (Session["MainOption"] != null)
            {
                List<OptionMaster> List_MainOption = (List<OptionMaster>)Session["MainOption"];
                DrpMainOption.DataSource = List_MainOption;
                DrpMainOption.DataTextField = "OptionName";
                DrpMainOption.DataValueField = "ID";
                DrpMainOption.DataBind();
                DrpMainOption.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        protected void btnCancelMainoption_Click(object sender, EventArgs e)
        {
            txtOption.Text = "";
        }
        protected void btnAddsuboption_Click(object sender, EventArgs e)
        {
            string MainOpName = "";
            if (Session["SubOption"] != null)
            {
                Raw_List = (List<Database.RowMaster>)Session["SubOption"];
            }
            int MainOPID = Convert.ToInt32(DrpMainOption.SelectedValue);
            Database.RowMaster obj_Raw = new RowMaster();
            obj_Raw.ID = Raw_List.Count() + 1;
            obj_Raw.OptionMasterID = MainOPID;
            obj_Raw.RowNameAnswer = txtSubOption.Text;
            Raw_List.Add(obj_Raw);
            txtSubOption.Text = "";
            Session["SubOption"] = Raw_List;
            BindSubListOption();

        }

        protected void btnCancelsubOption_Click(object sender, EventArgs e)
        {
            DrpMainOption.SelectedIndex = 0;
            txtSubOption.Text = "";
        }

        protected void ListviewMainOption_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnMainEdit")
                {
                    int OPID = Convert.ToInt32(e.CommandArgument);
                    List<OptionMaster> List_MainOption1 = (List<OptionMaster>)Session["MainOption"];
                    Database.OptionMaster Obj_Option = List_MainOption1.SingleOrDefault(p => p.ID == OPID && p.TenentID == TID);
                    txtOption.Text = Obj_Option.OptionName;
                    if (Obj_Option.SubOption != null && Obj_Option.SubOption != "")
                    {
                        pnladdsuboption.Visible = true;
                        txtSubOption1.Text = Obj_Option.SubOption;
                    }
                    List_MainOption1.Remove(Obj_Option);
                    btnAddMainoption.Text = "Update";
                    Session["MainOption"] = List_MainOption1;
                    BindMainListOption111();
                    ViewState["MainID"] = OPID;
                    // BindMainData();
                }
                if (e.CommandName == "btnMainDelete")
                {
                    int OPID = Convert.ToInt32(e.CommandArgument);
                    List<OptionMaster> List_MainOption1 = (List<OptionMaster>)Session["MainOption"];
                    Database.OptionMaster Obj_Option = List_MainOption1.SingleOrDefault(p => p.ID == OPID && p.TenentID == TID);
                    List_MainOption1.Remove(Obj_Option);
                    btnAddMainoption.Text = "Add";
                    Session["MainOption"] = List_MainOption1;
                    BindMainListOption111();
                    //  ViewState["MainID"] = OPID;
                    BindMainData();
                }
            }
            catch (Exception ex)
            { }
        }

        public string GetMainOptionName(int ID)
        {
            int MID = Convert.ToInt32(ID);
            string Name = "";
            if (Session["MainOption"] != null)
            {
                List<OptionMaster> List_MainOption1 = (List<OptionMaster>)Session["MainOption"];
                Database.OptionMaster Obj_Option = List_MainOption1.SingleOrDefault(p => p.ID == MID && p.TenentID == TID);
                Name = Obj_Option.OptionName;
            }
            return Name;
        }

        //------------------------------------------------------
        //public string GetQuestion(int ID)
        //{
        //    int MID = Convert.ToInt32(ID);
        //    string Name = "";
        //    if (Session["MainOption"] != null)
        //    {
        //        List<QuestionMaster> List_Question = (List<QuestionMaster>)Session["MainOption"];
        //        Database.QuestionMaster Obj_Option = List_Question.SingleOrDefault(p => p.ID == MID);
        //        Name = Obj_Option.QuestionLang1;
        //    }
        //    return Name;
        //}

        //------------------------------------------------------
        protected void ListviewSubOption_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (Session["SubOption"] != null)
                {
                    if (e.CommandName == "btnSubEdit")
                    {
                        int OPSID = Convert.ToInt32(e.CommandArgument);
                        List<Database.RowMaster> List_MainsubOption1 = (List<Database.RowMaster>)Session["SubOption"];
                        Database.RowMaster Obj_Option = List_MainsubOption1.SingleOrDefault(p => p.ID == OPSID && p.TenentID == TID);
                        txtOption.Text = Obj_Option.RowNameAnswer;
                        List_MainsubOption1.Remove(Obj_Option);
                        btnAddsuboption.Text = "Update";
                        Session["SubOption"] = List_MainsubOption1;
                        BindSubListOption();
                    }
                    if (e.CommandName == "btnSubDelete")
                    {
                        int OPSID = Convert.ToInt32(e.CommandArgument);
                        List<Database.RowMaster> List_MainsubOption1 = (List<Database.RowMaster>)Session["SubOption"];
                        Database.RowMaster Obj_Option = List_MainsubOption1.SingleOrDefault(p => p.ID == OPSID && p.TenentID == TID);
                        List_MainsubOption1.Remove(Obj_Option);
                        btnAddsuboption.Text = "Add";
                        Session["SubOption"] = List_MainsubOption1;
                        BindSubListOption();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        ////----------------------------

        public void EditFuction(int QSID)
        {
            Database.QuestionMaster objCRM_Question = DB.QuestionMasters.Single(p => p.ID == QSID && p.TenentID == TID);
            txtqutionnumber.Text = QSID.ToString();
            // txtquelang1.Text = objCRM_Question.QuestionLang1;
            txtquelang1.Text = objCRM_Question.QuestionLang1;
            txtquelang2.Text = objCRM_Question.QuestionLang2;
            txtquelang3.Text = objCRM_Question.QuestionLang3;
            if (objCRM_Question.CategoryID != null)
            {
                drpCategory.SelectedValue = objCRM_Question.CategoryID.ToString();
                if (drpCategory.SelectedItem.Text == "Single")
                {
                    List<OptionMaster> QoptionList = DB.OptionMasters.Where(p => p.QuestionID == QSID&&p.TenentID==TID).ToList();
                    Session["MainOption"] = QoptionList;
                    pnlValueofdropdowncommaseperator.Visible = true;
                    BindMainListOption111();
                }
                else
                {
                    listsubqution.DataSource = DB.QuestionMasters.Where(p => p.Perent_QID == QSID && p.TenentID == TID);
                    listsubqution.DataBind();
                    pnlNumberQ.Visible = true;
                }
            }
            if (objCRM_Question.BeforeQID != null)
            {
                DrpBeforeThisQuestion.SelectedValue = objCRM_Question.BeforeQID.ToString();
            }
            if (objCRM_Question.AfterQID != null)
            {
                DrpAfterThisQuestion.SelectedValue = objCRM_Question.AfterQID.ToString();
            }
            if (objCRM_Question.GroupID != null)
            {
                DrpGroup.SelectedValue = objCRM_Question.GroupID.ToString();
            }
            txtChoice.Text = objCRM_Question.Choise;
            txtRemark.Text = objCRM_Question.Remarks;
            txtWeitage.Text = objCRM_Question.Weitage;
            if (objCRM_Question.ChoiseType != null)
            {
                DrpChoiceType.SelectedValue = objCRM_Question.ChoiseType.ToString();
                
            }
           
        }
        protected void Listview2QuestionAnswer_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnQutionEdit")
                {
                    int QSID = Convert.ToInt32(e.CommandArgument);
                    EditFuction(QSID);
                    ViewState["Edit"] = QSID;
                    WrietMode();
                    //  Bindlistview();
                }
                if (e.CommandName == "btnQutionDelet")
                {
                    int QPSID = Convert.ToInt32(e.CommandArgument);
                    Database.QuestionMaster objCRM_QuestionDelete = DB.QuestionMasters.Single(p => p.ID == QPSID && p.TenentID == TID);
                    objCRM_QuestionDelete.Deleted = false;
                    DB.SaveChanges();
                    Bindlistview();
                }
                if (e.CommandName == "btnqstView")
                {
                    int QPSID = Convert.ToInt32(e.CommandArgument);
                    ModalPopupExtender2.Show();

                    if (DB.QuestionMasters.Where(p => p.ID == QPSID && p.Deleted == true && p.Avtive == true && p.TenentID == TID).Count() > 0)
                    {
                        Database.QuestionMaster obj_Question = DB.QuestionMasters.SingleOrDefault(p => p.ID == QPSID && p.Deleted == true && p.Avtive == true && p.TenentID == TID);
                        lblshowQuestion.Text = obj_Question.QuestionLang1;

                        if (DB.OptionMasters.Where(p => p.QuestionID == obj_Question.ID && p.TenentID == TID).Count() > 0)
                        {
                            List<Database.OptionMaster> AddOption_List = DB.OptionMasters.Where(p => p.QuestionID == obj_Question.ID && p.TenentID == TID).ToList();
                            if (obj_Question.ChoiseType == 1)
                            {
                                pnlquePreviewMultiple.Visible = false;
                                pnlcommasepanspreview.Visible = true;
                                pnlsingleanspreview.Visible = false;
                                //string CommaName="";ListBoxcommasepAnswer
                                //foreach(OptionMaster item1 in AddOption_List)
                                //{
                                //    CommaName = item1.OptionName + ",";
                                //}
                                // lblcommasepAnswer.Text = CommaName;
                                ListBoxcommasepAnswer.DataSource = AddOption_List;
                                ListBoxcommasepAnswer.DataTextField = "OptionName";
                                ListBoxcommasepAnswer.DataValueField = "ID";
                                ListBoxcommasepAnswer.DataBind();
                            }
                            else if (obj_Question.ChoiseType == 2)
                            {
                                pnlcommasepanspreview.Visible = false;
                                pnlsingleanspreview.Visible = false;
                                pnlquePreviewMultiple.Visible = true;
                                chmultipletpreview.DataSource = AddOption_List;
                                chmultipletpreview.DataTextField = "OptionName";
                                chmultipletpreview.DataValueField = "ID";
                                chmultipletpreview.DataBind();
                                //chmultpreview
                            }
                            else if (obj_Question.ChoiseType == 3)
                            {
                                pnlcommasepanspreview.Visible = false;
                                pnlquePreviewMultiple.Visible = false;
                                pnlsingleanspreview.Visible = true;
                                RadiosinPreview.DataSource = AddOption_List;
                                RadiosinPreview.DataTextField = "OptionName";
                                RadiosinPreview.DataValueField = "ID";
                                RadiosinPreview.DataBind();
                            }
                            else if (obj_Question.ChoiseType == 4)
                            {
                                pnlanspreviewswaper.Visible = true;
                            }
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                throw;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text == "AddNew")
                {


                    Clear();
                    btnAdd.Text = "Add";
                }
                else if (btnAdd.Text == "Add")
                {
                    Database.QuestionMaster objCRM_Question = new Database.QuestionMaster();


                    btnAdd.Text = "AddNew";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            if (DB.QuestionMasters.Where(p => p.QuestionLang1 == txtquelang1.Text && p.TenentID == TID).Count() < 1)
            {
                ViewState["QID"] = null;
                ViewState["OPTID"] = null;
                ViewState["GetEnd"] = null;
                ViewState["GetStart"] = null;
              
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                Database.QuestionMaster obj_Que1 = new Database.QuestionMaster();
                ViewState["QID"] = DB.QuestionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.QuestionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                obj_Que1.ID = Convert.ToInt32(ViewState["QID"]);
                obj_Que1.QuestionLang1 = txtquelang1.Text;
                obj_Que1.QuestionLang2 = txtquelang2.Text;
                obj_Que1.QuestionLang3 = txtquelang3.Text;
                if (Convert.ToInt32(drpCategory.SelectedValue) != 0)
                {
                    obj_Que1.CategoryID = Convert.ToInt32(drpCategory.SelectedValue);
                }
                if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) != 0)
                {
                    obj_Que1.BeforeQID = Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue);
                }
                if (Convert.ToInt32(DrpAfterThisQuestion.SelectedValue) != 0)
                {
                    obj_Que1.AfterQID = Convert.ToInt32(DrpAfterThisQuestion.SelectedValue);
                }
                if (Convert.ToInt32(DrpGroup.SelectedValue) != 0)
                {
                    obj_Que1.GroupID = Convert.ToInt32(DrpGroup.SelectedValue);
                }
                obj_Que1.Choise = txtChoice.Text;
                obj_Que1.Remarks = txtRemark.Text;
                obj_Que1.Weitage = txtWeitage.Text;
                if (Convert.ToInt32(DrpChoiceType.SelectedValue) != 0)
                {
                    obj_Que1.ChoiseType = Convert.ToInt32(DrpChoiceType.SelectedValue);
                }
                obj_Que1.CreateDate = DateTime.Now;
                obj_Que1.TenentID = TID;
                obj_Que1.CreateBy = UID;
                obj_Que1.LocationID = TID;
                obj_Que1.Avtive = true;
                obj_Que1.Deleted = true;
                DB.QuestionMasters.AddObject(obj_Que1);
                DB.SaveChanges();
                if (Convert.ToInt32(DrpChoiceType.SelectedValue) != 0)
                {
                    if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 1)
                    {
                        foreach (ListViewItem item in ListviewMainOption111.Items)
                        {

                            Label lbloption = (Label)item.FindControl("lblOptionName");
                            Label lblSuboption = (Label)item.FindControl("lblsuboption12");
                            ViewState["OPTID"] = DB.OptionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                            Database.OptionMaster obj_Op1 = new Database.OptionMaster();
                            obj_Op1.ID = Convert.ToInt32(ViewState["OPTID"]);
                            obj_Op1.QuestionID = Convert.ToInt32(ViewState["QID"]);
                            obj_Op1.OptionName = lbloption.Text;
                            obj_Op1.SubOption = lblSuboption.Text;
                            obj_Op1.DateTime = DateTime.Now;
                            obj_Op1.CreatedBy = UID;
                            obj_Op1.TenentID = TID;
                            obj_Op1.Active = true;
                            obj_Op1.Deleted = true;
                            DB.OptionMasters.AddObject(obj_Op1);
                            DB.SaveChanges();

                            string OPNamw = "";
                            if (lblSuboption.Text != "")
                            {
                                OPNamw = lblSuboption.Text;
                            }
                            else
                            {
                                OPNamw = lbloption.Text;
                            }
                            if (ListSelectOption.Items.Count > 0)
                            {
                                for (int a = 0; a < ListSelectOption.Items.Count; a++)
                                {
                                    if (OPNamw == ListSelectOption.Items[a].Text)
                                    {
                                        if (ListSelectOption.Items[a].Selected)
                                        {
                                            Database.RowMaster obj_Raw = new Database.RowMaster();
                                            obj_Raw.QuetionID = Convert.ToInt32(ViewState["QID"]);
                                            obj_Raw.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                            obj_Raw.OptionMasterID = Convert.ToInt32(ViewState["OPTID"]);
                                            obj_Raw.RowNameAnswer = ListSelectOption.Items[a].Text;
                                            obj_Raw.Answer = ListSelectOption.Items[a].Selected;
                                            DB.RowMasters.AddObject(obj_Raw);
                                            DB.SaveChanges();

                                        }
                                        //return;
                                    }

                                }
                            }

                        }
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 2)
                    {
                        for (int i = 0; i < CheckBoxListMultiple.Items.Count; i++)
                        {
                            ViewState["OPTID"] = DB.OptionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                            Database.OptionMaster obj_Op1 = new Database.OptionMaster();
                            obj_Op1.ID = Convert.ToInt32(ViewState["OPTID"]);
                            obj_Op1.QuestionID = Convert.ToInt32(ViewState["QID"]);
                            obj_Op1.OptionName = CheckBoxListMultiple.Items[i].Text;
                            obj_Op1.SubOption = CheckBoxListMultiple.Items[i].Text;
                            // obj_Op1.Answer = CheckBoxListMultiple.Items[i].Selected;
                            obj_Op1.DateTime = DateTime.Now;
                            obj_Op1.CreatedBy = UID;
                            obj_Op1.TenentID = TID;
                            obj_Op1.Active = true;
                            obj_Op1.Deleted = true;
                            DB.OptionMasters.AddObject(obj_Op1);
                            DB.SaveChanges();
                        }
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 3)
                    {
                        for (int j = 0; j < RadioButtonListSingle.Items.Count; j++)
                        {
                            ViewState["OPTID"] = DB.OptionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                            Database.OptionMaster obj_Op1 = new Database.OptionMaster();
                            obj_Op1.ID = Convert.ToInt32(ViewState["OPTID"]);
                            obj_Op1.QuestionID = Convert.ToInt32(ViewState["QID"]);
                            obj_Op1.OptionName = RadioButtonListSingle.Items[j].Text;
                            obj_Op1.SubOption = RadioButtonListSingle.Items[j].Text;
                            //  obj_Op1.Answer = RadioButtonListSingle.Items[j].Selected;
                            obj_Op1.DateTime = DateTime.Now;
                            obj_Op1.CreatedBy = UID;
                            obj_Op1.TenentID = TID;
                            obj_Op1.Active = true;
                            obj_Op1.Deleted = true;
                            DB.OptionMasters.AddObject(obj_Op1);
                            DB.SaveChanges();
                        }
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 4)
                    {
                        ViewState["OPTID"] = DB.OptionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                        Database.OptionMaster obj_Op1 = new Database.OptionMaster();
                        obj_Op1.ID = Convert.ToInt32(ViewState["OPTID"]);
                        obj_Op1.QuestionID = Convert.ToInt32(ViewState["QID"]);
                        obj_Op1.OptionName = "Yes" + "," + "No";
                        obj_Op1.DateTime = DateTime.Now;
                        obj_Op1.CreatedBy = UID;
                        obj_Op1.TenentID = TID;
                        obj_Op1.Active = true;
                        obj_Op1.Deleted = true;
                        if (RadioYes.Checked)
                        {
                            obj_Op1.AnswerText = "Yes";

                        }
                        else
                        {
                            obj_Op1.AnswerText = "No";

                        }
                        DB.OptionMasters.AddObject(obj_Op1);
                        DB.SaveChanges();
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 5)
                    {
                        int count = 0;
                        foreach (ListViewItem item in ListviewMainOption111.Items)
                        {

                            Label lbloption = (Label)item.FindControl("lblOptionName");
                            Label lblSuboption = (Label)item.FindControl("lblsuboption12");
                            //if (System.Text.RegularExpressions.Regex.IsMatch(lbloption.Text, "^[0-9_]+$"))
                            //{
                            if (lbloption.Text.ToString().Contains("-"))
                            {
                                string[] serires = lbloption.Text.ToString().Split('-');
                                if (count == 0)
                                {
                                    ViewState["GetStart"] = serires[0];
                                    count++;

                                }
                                else
                                {
                                    if (count == ListviewMainOption111.Items.Count())
                                    {
                                        ViewState["GetEnd"] = serires[1];
                                    }

                                }
                            }
                            else
                            {
                                break;
                            }

                            ViewState["OPTID"] = DB.OptionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                            Database.OptionMaster obj_Op1 = new Database.OptionMaster();
                            obj_Op1.ID = Convert.ToInt32(ViewState["OPTID"]);
                            obj_Op1.QuestionID = Convert.ToInt32(ViewState["QID"]);
                            obj_Op1.OptionName = lbloption.Text;
                            ViewState["GetEnd"] = lbloption.Text;
                            obj_Op1.SubOption = lblSuboption.Text;
                            obj_Op1.DateTime = DateTime.Now;
                            obj_Op1.CreatedBy = UID;
                            obj_Op1.TenentID = TID;
                            obj_Op1.Active = true;
                            obj_Op1.Deleted = true;
                            DB.OptionMasters.AddObject(obj_Op1);
                            DB.SaveChanges();
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "Only Enter Series(10-15) in series..", "Showalert();", true);
                            //    break;
                            //}
                        }
                        int strat = Convert.ToInt32(ViewState["GetStart"]);
                        string b = ViewState["GetEnd"].ToString();
                        string[] a = b.Split('-');
                        int End = Convert.ToInt32(a[1]);
                        int Range = Convert.ToInt32(range_4.Text);

                        if (strat <= Range && End >= Range)
                        {
                            Database.RowMaster obj_Raw = new Database.RowMaster();
                            obj_Raw.QuetionID = Convert.ToInt32(ViewState["QID"]);
                            obj_Raw.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                            // obj_Raw.OptionMasterID = Convert.ToInt32(ViewState["OPTID"]);
                            obj_Raw.RowNameAnswer = range_4.Text;
                            DB.RowMasters.AddObject(obj_Raw);
                            DB.SaveChanges();
                            count++;
                            btnAnswerPreview.Attributes["class"] = btnAnswerPreview.Attributes["class"].Replace("btn default btn-lg disabled", "btn default btn-lg");

                            ViewState["PreviewID"] = ViewState["QID"];
                            Panel1.Visible = true;
                            Clear();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Answer Must be in Range..", "Showalert();", true);
                        }
                        // }
                    }
                }

            }
            else
            {
                Panel2.Visible = true;
            }
            btnAnswerPreview.Attributes["class"] = btnAnswerPreview.Attributes["class"].Replace("btn default btn-lg disabled", "btn default btn-lg");

            ViewState["PreviewID"] = ViewState["QID"];
            Panel1.Visible = true;
            readMode();
            //pnlValueofdropdowncommaseperator.Visible = false;
            //ListviewMainOption111.DataSource = null;
            //ListviewMainOption111.DataBind();
            // Clear();
        }

        protected void btnsubOption_Click(object sender, EventArgs e)
        {
            pnladdsuboption.Visible = true;
        }

        protected void btnAnswer_Click(object sender, EventArgs e)
        {
            pnlanswer.Visible = true;
            btnFinalSubmit.Enabled = true;
            btnFinalSubmit.Attributes["class"] = btnFinalSubmit.Attributes["class"].Replace("btn default btn-lg disabled", "btn default btn-lg");



        }

        protected void DrpChoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(DrpChoiceType.SelectedValue) != 0)
            {
                if (Session["MainOption"] != null)
                {

                    List<OptionMaster> List_MainOptionList = (List<OptionMaster>)Session["MainOption"];
                    int i = 0;
                    List<OptionAddValue> OPvalue = new List<OptionAddValue>();
                    foreach (ListViewDataItem optionitem in ListviewMainOption111.Items)
                    {
                        string Name = "";
                        int OPID = 0;
                        Label lblopname = (Label)optionitem.FindControl("lblOptionName");
                        Label lblSubopname = (Label)optionitem.FindControl("lblsuboption12");
                        HiddenField HID = (HiddenField)optionitem.FindControl("hidenopid");
                        if (Convert.ToInt32(HID.Value) != 0)
                        {
                            OPID = Convert.ToInt32(HID.Value);
                        }
                        if (lblopname.Text != "")
                        {
                            Name = lblopname.Text;
                        }
                        if (lblSubopname.Text != "")
                        {
                            Name += " " + lblSubopname.Text;
                        }

                        OPvalue.Insert(i, new OptionAddValue { Name = Name, OPID1 = OPID });
                        i++;
                    }
                    if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 1)
                    {
                        pnlCommaAnswer.Visible = true;
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = false;
                        ListAllOption.DataSource = OPvalue;
                        ListAllOption.DataTextField = "Name";
                        ListAllOption.DataValueField = "OPID1";
                        ListAllOption.DataBind();

                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 2)
                    {
                        pnlMutipleyeshowmany.Visible = true;
                        pnlMutipleknowOne.Visible = false;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = false;
                        CheckBoxListMultiple.DataSource = OPvalue;
                        CheckBoxListMultiple.DataTextField = "Name";
                        CheckBoxListMultiple.DataValueField = "OPID1";
                        CheckBoxListMultiple.DataBind();
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 3)
                    {
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = true;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = false;
                        RadioButtonListSingle.DataSource = OPvalue;
                        RadioButtonListSingle.DataTextField = "Name";
                        RadioButtonListSingle.DataValueField = "OPID1";
                        RadioButtonListSingle.DataBind();
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 4)
                    {
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = false;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = true;
                        pnlRankingMultiple.Visible = false;
                    }
                    else if (Convert.ToInt32(DrpChoiceType.SelectedValue) == 5)
                    {
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = false;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = true;
                    }

                }
                else
                {


                }

            }
        }

        protected void btnbefore_Click(object sender, EventArgs e)
        {
            if (ListAllOption.SelectedIndex >= 1)
            {
                for (int i = 0; i < ListAllOption.Items.Count; i++)
                {
                    if (ListAllOption.Items[i].Selected)
                    {
                        ListSelectOption.Items.Add(ListAllOption.Items[i]);
                        ListAllOption.Items.Remove(ListAllOption.Items[i]);
                    }
                }
            }
        }

        protected void btnAfter_Click(object sender, EventArgs e)
        {
            if (ListSelectOption.SelectedIndex >= 1)
            {
                for (int i = 0; i < ListSelectOption.Items.Count; i++)
                {
                    if (ListSelectOption.Items[i].Selected)
                    {
                        ListAllOption.Items.Add(ListSelectOption.Items[i]);
                        ListSelectOption.Items.Remove(ListSelectOption.Items[i]);
                    }
                }
            }
        }

        public void Clear()
        {
            Session["MainOption"] = null;
            Session["SubOption"] = null;
            Session["MultipleValue"] = null;
            Session["SingleValue"] = null;
            txtChoice.Text = "";
            txtOption.Text = "";
            txtSubOption.Text = "";
            txtSubOption1.Text = "";
            txtquelang1.Text = "";
            txtquelang2.Text = "";
            txtquelang3.Text = "";
            txtRemark.Text = "";
            txtWeitage.Text = "";
            drpCategory.SelectedIndex = 0;
            DrpBeforeThisQuestion.SelectedIndex = 0;
            DrpAfterThisQuestion.SelectedIndex = 0;
            DrpGroup.SelectedIndex = 0;
            BindMainListOption111();
            pnlCommaAnswer.Visible = false;
            pnlMutipleyeshowmany.Visible = false;
            pnlMutipleknowOne.Visible = false;
            //DrpChoiceType.SelectedIndex = 0;
            range_4.Text = "";

        }

        protected void btnAnswerPreview_Click(object sender, EventArgs e)
        {
            //ViewState["PreviewID"] = "22";
            if (ViewState["PreviewID"] != null)
            {
                ModalPopupExtender2.Show();
                int PreID = Convert.ToInt32(ViewState["PreviewID"]);
                if (DB.QuestionMasters.Where(p => p.ID == PreID && p.Deleted == true && p.Avtive == true && p.TenentID == TID).Count() > 0)
                {
                    Database.QuestionMaster obj_Question = DB.QuestionMasters.SingleOrDefault(p => p.ID == PreID && p.Deleted == true && p.Avtive == true && p.TenentID == TID);
                    lblshowQuestion.Text = obj_Question.QuestionLang1;

                    if (DB.OptionMasters.Where(p => p.QuestionID == obj_Question.ID && p.TenentID == TID).Count() > 0)
                    {
                        List<Database.OptionMaster> AddOption_List = DB.OptionMasters.Where(p => p.QuestionID == obj_Question.ID && p.TenentID == TID).ToList();
                        if (obj_Question.ChoiseType == 1)
                        {
                            pnlquePreviewMultiple.Visible = false;
                            pnlcommasepanspreview.Visible = true;
                            pnlsingleanspreview.Visible = false;
                            //string CommaName="";ListBoxcommasepAnswer
                            //foreach(OptionMaster item1 in AddOption_List)
                            //{
                            //    CommaName = item1.OptionName + ",";
                            //}
                            // lblcommasepAnswer.Text = CommaName;
                            ListBoxcommasepAnswer.DataSource = AddOption_List;
                            ListBoxcommasepAnswer.DataTextField = "OptionName";
                            ListBoxcommasepAnswer.DataValueField = "ID";
                            ListBoxcommasepAnswer.DataBind();
                        }
                        else if (obj_Question.ChoiseType == 2)
                        {
                            pnlcommasepanspreview.Visible = false;
                            pnlsingleanspreview.Visible = false;
                            pnlquePreviewMultiple.Visible = true;
                            chmultipletpreview.DataSource = AddOption_List;
                            chmultipletpreview.DataTextField = "OptionName";
                            chmultipletpreview.DataValueField = "ID";
                            chmultipletpreview.DataBind();
                            //chmultpreview
                        }
                        else if (obj_Question.ChoiseType == 3)
                        {
                            pnlcommasepanspreview.Visible = false;
                            pnlquePreviewMultiple.Visible = false;
                            pnlsingleanspreview.Visible = true;
                            RadiosinPreview.DataSource = AddOption_List;
                            RadiosinPreview.DataTextField = "OptionName";
                            RadiosinPreview.DataValueField = "ID";
                            RadiosinPreview.DataBind();
                        }
                        else if (obj_Question.ChoiseType == 4)
                        {
                            pnlanspreviewswaper.Visible = true;
                        }
                    }
                }

            }
        }

        protected void DrpAfterThisQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpAfterThisQuestion.SelectedValue) != 0)
            {
                if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) != 0)
                {
                    if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) == Convert.ToInt32(DrpAfterThisQuestion.SelectedValue))
                    {
                        Panel3.Visible = true;
                    }
                }
            }
        }

        protected void DrpBeforeThisQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) != 0)
            {
                if (Convert.ToInt32(DrpAfterThisQuestion.SelectedValue) != 0)
                {
                    if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) == Convert.ToInt32(DrpAfterThisQuestion.SelectedValue))
                    {
                        Panel3.Visible = true;
                    }
                }
            }
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(drpCategory.SelectedValue) != 0)
            {
                if (drpCategory.SelectedItem.Text == "MULTIPLE")
                {
                    // txtChoice.Text = "2";
                    txtChoice.Enabled = true;
                    pnlValueofdropdowncommaseperator.Visible = false;

                }
                else
                {
                    txtChoice.Text = "1";
                    txtChoice.Enabled = false;
                    PnelChoice.Visible = true;
                    pnlOption.Visible = true;
                    pnlValueofdropdowncommaseperator.Visible = true;
                    ListviewMainOption111.DataSource = null;
                    ListviewMainOption111.DataBind();
                    pnlNumberQ.Visible = false;
                }
            }

        }

        public void LastData()
        {
            if( DB.QuestionMasters.Where(p => p.Deleted == true && p.Perent_QID == 0 && p.TenentID == TID).Count()>0)
            {
                int CMPNID = DB.QuestionMasters.Where(p => p.Deleted == true && p.Perent_QID == 0 && p.TenentID == TID).Max(p => p.ID);
                EditFuction(CMPNID);
                readMode();
            }
           
        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            Clear();
            WrietMode();
            int MAXQ = DB.QuestionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.QuestionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
            txtqutionnumber.Text = MAXQ.ToString();
            txtqutionnumber.Enabled = false;
            pnlValueofdropdowncommaseperator.Visible = false;
            pnlNumberQ.Visible = false;
        }

        public string getcetegoryname(int ID)
        {
            if (ID != 0 && ID != null)
                return DB.CAT_MST.Single(p => p.CATID == ID && p.TenentID == TID).CAT_NAME1;
            else
                return "";
        }
        public string getchoisetype(int CID)
        {
            if (CID != 0 && CID != null)
                return DB.IsChoices.Single(p => p.ID == CID&&p.TenentID==TID).ChoiceType;
            else
                return "";
        }
        public string getgrupname(int GID)
        {
            if (GID != 0 && GID != null)
                return DB.TBLGROUPs.Single(p => p.ITGROUPID == GID && p.TenentID==TID).ITGROUPDESC1;
            else
                return "";

        }
        List<QuestionMaster> listQuestionMaster = new List<QuestionMaster>();
        protected void txtChoice_TextChanged(object sender, EventArgs e)
        {
            if (txtChoice.Text != "")
            {
                int ID = Convert.ToInt32(txtChoice.Text);
                for (int i = 0; i < ID; i++)
                {
                    QuestionMaster OBjQuestionMaster = new QuestionMaster();
                    listQuestionMaster.Add(OBjQuestionMaster);
                }
                listsubqution.DataSource = listQuestionMaster;
                listsubqution.DataBind();
                pnlNumberQ.Visible = true;
            }
        }

        protected void listsubqution_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList drpQutintype = (DropDownList)e.Item.FindControl("drpQutintype");
                Classes.CRMClass.getcrmdropdown(drpQutintype, TID, "", "", "", "IsChoice");
            }
        }

        protected void drpchoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(drpchoiceList.SelectedValue) != 0)
            {
                if (Session["MainOption"] != null)
                {

                    List<OptionMaster> List_MainOptionList = (List<OptionMaster>)Session["MainOption"];
                    int i = 0;
                    List<OptionAddValue> OPvalue = new List<OptionAddValue>();
                    foreach (ListViewDataItem optionitem in ListviewMainOption111.Items)
                    {
                        string Name = "";
                        int OPID = 0;
                        Label lblopname = (Label)optionitem.FindControl("lblOptionName");
                        Label lblSubopname = (Label)optionitem.FindControl("lblsuboption12");
                        HiddenField HID = (HiddenField)optionitem.FindControl("hidenopid");
                        if (Convert.ToInt32(HID.Value) != 0)
                        {
                            OPID = Convert.ToInt32(HID.Value);
                        }
                        if (lblopname.Text != "")
                        {
                            Name = lblopname.Text;
                        }
                        if (lblSubopname.Text != "")
                        {
                            Name += " " + lblSubopname.Text;
                        }

                        OPvalue.Insert(i, new OptionAddValue { Name = Name, OPID1 = OPID });
                        i++;
                    }
                    if (Convert.ToInt32(drpchoiceList.SelectedValue) == 1)
                    {
                        pnlCommaAnswer.Visible = true;
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = false;
                        ListAllOption.DataSource = OPvalue;
                        ListAllOption.DataTextField = "Name";
                        ListAllOption.DataValueField = "OPID1";
                        ListAllOption.DataBind();

                    }
                    else if (Convert.ToInt32(drpchoiceList.SelectedValue) == 2)
                    {
                        pnlMutipleyeshowmany.Visible = true;
                        pnlMutipleknowOne.Visible = false;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = false;
                        CheckBoxListMultiple.DataSource = OPvalue;
                        CheckBoxListMultiple.DataTextField = "Name";
                        CheckBoxListMultiple.DataValueField = "OPID1";
                        CheckBoxListMultiple.DataBind();
                    }
                    else if (Convert.ToInt32(drpchoiceList.SelectedValue) == 3)
                    {
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = true;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = false;
                        RadioButtonListSingle.DataSource = OPvalue;
                        RadioButtonListSingle.DataTextField = "Name";
                        RadioButtonListSingle.DataValueField = "OPID1";
                        RadioButtonListSingle.DataBind();
                    }
                    else if (Convert.ToInt32(drpchoiceList.SelectedValue) == 4)
                    {
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = false;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = true;
                        pnlRankingMultiple.Visible = false;
                    }
                    else if (Convert.ToInt32(drpchoiceList.SelectedValue) == 5)
                    {
                        pnlMutipleyeshowmany.Visible = false;
                        pnlMutipleknowOne.Visible = false;
                        pnlCommaAnswer.Visible = false;
                        pnlSwappersingledobl.Visible = false;
                        pnlRankingMultiple.Visible = true;
                    }

                }
                else
                {


                }

            }
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            Database.QuestionMaster obj_Que1 = new Database.QuestionMaster();
            ViewState["QID"] = txtqutionnumber.Text;
            obj_Que1.ID = Convert.ToInt32(ViewState["QID"]);
            obj_Que1.TenentID = TID;
            obj_Que1.LocationID = LID;
            obj_Que1.Perent_QID = 0;
            obj_Que1.QuestionLang1 = txtquelang1.Text;
            obj_Que1.QuestionLang2 = txtquelang2.Text;
            obj_Que1.QuestionLang3 = txtquelang3.Text;
            if (Convert.ToInt32(drpCategory.SelectedValue) != 0)
            {
                obj_Que1.CategoryID = Convert.ToInt32(drpCategory.SelectedValue);
            }
            if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) != 0)
            {
                obj_Que1.BeforeQID = Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue);
            }
            if (Convert.ToInt32(DrpAfterThisQuestion.SelectedValue) != 0)
            {
                obj_Que1.AfterQID = Convert.ToInt32(DrpAfterThisQuestion.SelectedValue);
            }
            if (Convert.ToInt32(DrpGroup.SelectedValue) != 0)
            {
                obj_Que1.GroupID = Convert.ToInt32(DrpGroup.SelectedValue);
            }
            obj_Que1.Choise = txtChoice.Text;
            obj_Que1.Remarks = txtRemark.Text;
            obj_Que1.Weitage = txtWeitage.Text;
            if (Convert.ToInt32(DrpChoiceType.SelectedValue) != 0)
            {
                obj_Que1.ChoiseType = Convert.ToInt32(DrpChoiceType.SelectedValue);
            }
            obj_Que1.CreateDate = DateTime.Now;

            obj_Que1.CreateBy = UID;

            obj_Que1.Avtive = true;
            obj_Que1.Deleted = true;
            DB.QuestionMasters.AddObject(obj_Que1);
            DB.SaveChanges();
            for (int i = 0; i < listsubqution.Items.Count(); i++)
            {
                TextBox txtser = (TextBox)listsubqution.Items[i].FindControl("txtser");
                TextBox txtqutionname = (TextBox)listsubqution.Items[i].FindControl("txtqutionname");
                DropDownList drpQutintype = (DropDownList)listsubqution.Items[i].FindControl("drpQutintype");
                TextBox txtTotalAns = (TextBox)listsubqution.Items[i].FindControl("txtTotalAns");
                TextBox txtAnsReq = (TextBox)listsubqution.Items[i].FindControl("txtAnsReq");
                TextBox txtAnswer = (TextBox)listsubqution.Items[i].FindControl("txtAnswer");
                TextBox txtPos = (TextBox)listsubqution.Items[i].FindControl("txtPos");
                TextBox txtNeg = (TextBox)listsubqution.Items[i].FindControl("txtNeg");
                Database.QuestionMaster obj_QuestionMaster = new Database.QuestionMaster();
                int MAXQ = DB.QuestionMasters.Where(p=>p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.QuestionMasters.Where(p=>p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                obj_QuestionMaster.SerialNo=Convert.ToInt32(txtser.Text);
                obj_QuestionMaster.ID = MAXQ;
                obj_QuestionMaster.TenentID = TID;
                obj_QuestionMaster.LocationID = LID;
                obj_QuestionMaster.Perent_QID = Convert.ToInt32(txtqutionnumber.Text);
                obj_QuestionMaster.QuestionLang1 = txtqutionname.Text;
                obj_QuestionMaster.QuestionLang2 = txtqutionname.Text;
                obj_QuestionMaster.QuestionLang3 = txtqutionname.Text;
                if (Convert.ToInt32(drpQutintype.SelectedValue) != 0)
                {
                    obj_QuestionMaster.ChoiseType = Convert.ToInt32(drpQutintype.SelectedValue);
                }
                if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) != 0)
                {
                    obj_QuestionMaster.BeforeQID = Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue);
                }
                if (Convert.ToInt32(DrpAfterThisQuestion.SelectedValue) != 0)
                {
                    obj_QuestionMaster.AfterQID = Convert.ToInt32(DrpAfterThisQuestion.SelectedValue);
                }
                if (Convert.ToInt32(DrpGroup.SelectedValue) != 0)
                {
                    obj_QuestionMaster.GroupID = Convert.ToInt32(DrpGroup.SelectedValue);
                }
                obj_QuestionMaster.Remarks = txtRemark.Text;
                obj_QuestionMaster.Weitage = txtPos.Text;
                obj_QuestionMaster.Choise = txtAnsReq.Text;
                obj_QuestionMaster.CreateDate = DateTime.Now;
                obj_QuestionMaster.CreateBy = UID;
                obj_QuestionMaster.Avtive = true;
                obj_QuestionMaster.Deleted = true;
                DB.QuestionMasters.AddObject(obj_QuestionMaster);
                DB.SaveChanges();
                if (txtAnswer.Text != "")
                {
                    string[] Seperate4 = txtAnswer.Text.Split(',');
                    int count4 = 0;
                    string Sep4 = "";
                    for (int j = 0; j <= Seperate4.Count() - 1; j++)
                    {
                        Sep4 = Seperate4[j];
                        count4++;
                        Database.OptionMaster obj = new Database.OptionMaster();
                        obj.ID = DB.OptionMasters.Where(p=>p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p=>p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                        obj.TenentID = TID;
                        obj.LocationID = LID;
                        obj.QuestionID = MAXQ;
                        obj.Active = true;
                        obj.Deleted = true;
                        obj.DateTime = DateTime.Now;
                        obj.CreatedBy = UID;
                        obj.OptionName = Sep4;
                        obj.Active = true;
                        DB.OptionMasters.AddObject(obj);
                        DB.SaveChanges();
                    }
                }
            }
            btnPreview.Visible = true;
            btnSubmit.Visible = false;
            readMode();
            btnAdd.Visible = true;
         
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            int QPSID = Convert.ToInt32(txtqutionnumber.Text);
            ModalPopupExtender1.Show();
            Bind_QUESTION_Master(QPSID);
            if (DB.QuestionMasters.Where(p => p.ID == QPSID && p.Deleted == true && p.Avtive == true && p.TenentID == TID).Count() > 0)
            {
                Database.QuestionMaster obj_Question = DB.QuestionMasters.SingleOrDefault(p => p.ID == QPSID && p.Deleted == true && p.Avtive == true && p.TenentID == TID);
                Label40.Text = obj_Question.QuestionLang1;

                //if (DB.OptionMaster.Where(p => p.QuestionID == obj_Question.ID).Count() > 0)
                //{
                //    List<Database.OptionMaster> AddOption_List = DB.OptionMaster.Where(p => p.QuestionID == obj_Question.ID).ToList();
                //    if (obj_Question.ChoiseType == 1)
                //    {
                //        Panel5.Visible = false;
                //        Panel6.Visible = true;
                //        Panel7.Visible = false;
                //        //string CommaName="";ListBoxcommasepAnswer
                //        //foreach(OptionMaster item1 in AddOption_List)
                //        //{
                //        //    CommaName = item1.OptionName + ",";
                //        //}
                //        // lblcommasepAnswer.Text = CommaName;
                //        ListBox1.DataSource = AddOption_List;
                //        ListBox1.DataTextField = "OptionName";
                //        ListBox1.DataValueField = "ID";
                //        ListBox1.DataBind();
                //    }
                //    else if (obj_Question.ChoiseType == 2)
                //    {
                //        Panel6.Visible = false;
                //        Panel7.Visible = false;
                //        Panel5.Visible = true;
                //        CheckBoxList1.DataSource = AddOption_List;
                //        CheckBoxList1.DataTextField = "OptionName";
                //        CheckBoxList1.DataValueField = "ID";
                //        CheckBoxList1.DataBind();
                //        //chmultpreview
                //    }
                //    else if (obj_Question.ChoiseType == 3)
                //    {
                //        Panel6.Visible = false;
                //        Panel5.Visible = false;
                //        Panel7.Visible = true;
                //        RadioButtonList1.DataSource = AddOption_List;
                //        RadioButtonList1.DataTextField = "OptionName";
                //        RadioButtonList1.DataValueField = "ID";
                //        RadioButtonList1.DataBind();
                //    }
                //    else if (obj_Question.ChoiseType == 4)
                //    {
                //        Panel8.Visible = true;
                //    }
                //    else if(obj_Question.ChoiseType==5)
                //    {
                //        Panel9.Visible = true;
                //    }
                //}
            }
        }
        protected void Bind_QUESTION_Master(int QPSID)
        {
            Que_List = DB.QuestionMasters.Where(p => p.Avtive == true && p.Deleted == true && p.Perent_QID == QPSID && p.TenentID == TID).ToList();
            if (Que_List.Count() > 0)
            {
                repAccordian.DataSource = Que_List;
                repAccordian.DataBind();

                for (int i = 0; i < Que_List.Count(); i++)
                {
                    addf = 1;
                    // addf4 = 1;
                    QueID = Convert.ToInt32(Que_List[i].ID);
                    Option_List = DB.OptionMasters.Where(p => p.QuestionID == QueID && p.TenentID == TID).ToList();
                    if (Option_List.Count() > 0)
                    {
                        if (Que_List[i].ChoiseType == 3 || Que_List[i].ChoiseType == 4)
                        {

                            ((Repeater)repAccordian.Items[i].FindControl("Repeater1")).DataSource = Option_List;
                            ((Repeater)repAccordian.Items[i].FindControl("Repeater1")).DataBind();
                        }
                        else if (Que_List[i].ChoiseType == 2 || Que_List[i].ChoiseType == 1 || Que_List[i].ChoiseType == 5)
                        {

                            ((Repeater)repAccordian.Items[i].FindControl("Repeater2")).DataSource = Option_List;
                            ((Repeater)repAccordian.Items[i].FindControl("Repeater2")).DataBind();
                        }

                        else if (Que_List[i].ChoiseType == 1)
                        {
                            CreteList = 1;
                            ((Repeater)repAccordian.Items[i].FindControl("Repeater4")).DataSource = Option_List;
                            ((Repeater)repAccordian.Items[i].FindControl("Repeater4")).DataBind();
                        }

                    }
                    if (Que_List[i].ChoiseType == 4)
                    {
                        if (Option_List.Count == 0)
                        {
                            OptionMaster obj = new OptionMaster();
                            Option_List.Add(obj);
                        }

                        ((Repeater)repAccordian.Items[i].FindControl("Repeater3")).DataSource = Option_List;
                        ((Repeater)repAccordian.Items[i].FindControl("Repeater3")).DataBind();
                    }
                    else if (Que_List[i].ChoiseType == 5)
                    {
                        if (Option_List.Count == 0)
                        {
                            OptionMaster obj = new OptionMaster();
                            Option_List.Add(obj);
                        }

                        ((Repeater)repAccordian.Items[i].FindControl("Repeater5")).DataSource = Option_List;
                        ((Repeater)repAccordian.Items[i].FindControl("Repeater5")).DataBind();
                    }
                }

            }
        }
        protected void Repeater1ItemEventHandler(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButtonList questionsRadioButtonList = (RadioButtonList)e.Item.FindControl("QuestionsList");
                Label lbl = (Label)e.Item.FindControl("lbl_QUESTION_id");
                //QueID = Convert.ToInt32(lbl.Text);
                if (Option_List.Count() > 0)
                {
                    if (addf == 1)
                    {
                        for (int ik = 0; ik < Option_List.Count(); ik++)
                        {
                            ListItem item = new ListItem();
                            item.Text = Option_List[ik].OptionName.ToString();
                            item.Value = Option_List[ik].ID.ToString();
                            questionsRadioButtonList.Items.Add(item);
                        }
                        addf = 0;
                    }
                }
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // chmultipletpreview
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBoxList questionsCheckboxButtonList = (CheckBoxList)e.Item.FindControl("chmultipletpreview");
                Label lbl = (Label)e.Item.FindControl("lbl_QUESTION_id");
                //QueID = Convert.ToInt32(lbl.Text);
                if (Option_List.Count() > 0)
                {
                    if (addf == 1)
                    {
                        for (int ik = 0; ik < Option_List.Count(); ik++)
                        {
                            ListItem item = new ListItem();
                            item.Text = Option_List[ik].OptionName.ToString();
                            item.Value = Option_List[ik].ID.ToString();
                            questionsCheckboxButtonList.Items.Add(item);
                        }
                        addf = 0;
                    }
                }
            }
        }

        protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Bind_QUESTION_Master();
                Label lbl = (Label)e.Item.FindControl("lbl_QUESTION_id");
                // QueID = Convert.ToInt32(lbl.Text);
                if (Option_List.Count() > 0)
                {
                    if (addf == 1)
                    {
                        ListBox ListAllOption1 = (ListBox)e.Item.FindControl("ListAllOption11");
                        for (int ik = 0; ik < Option_List.Count(); ik++)
                        {
                            ListItem item = new ListItem();
                            item.Text = Option_List[ik].OptionName.ToString();
                            item.Value = Option_List[ik].ID.ToString();
                            ListAllOption1.Items.Add(item);
                        }
                        //if (CreteList == 1)
                        //{
                        //    Label lbl = (Label)e.Item.FindControl("lbl_QUESTION_id");
                        //    int QuestionID = Convert.ToInt32(lbl.Text);
                        //    Repeater r2 = (Repeater)e.Item.FindControl("Repeater4");
                        //    List<OptionMaster> List_Option = DB.OptionMaster.Where(p => p.QuestionID == QuestionID).ToList();
                        //    r2.DataSource = List_Option;
                        //    r2.DataBind();
                        //}
                        //CreteList = 0;
                        addf = 0;
                    }
                }
            }
        }

        protected void btnSubmitForSing_Click(object sender, EventArgs e)
        {

           
                ViewState["QID"] = null;
                ViewState["OPTID"] = null;
                ViewState["GetEnd"] = null;
                ViewState["GetStart"] = null;
                Database.QuestionMaster obj_Que1 = new Database.QuestionMaster();
                ViewState["QID"] = DB.QuestionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.QuestionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                obj_Que1.ID = Convert.ToInt32(ViewState["QID"]);
                obj_Que1.QuestionLang1 = txtquelang1.Text;
                obj_Que1.QuestionLang2 = txtquelang2.Text;
                obj_Que1.QuestionLang3 = txtquelang3.Text;
                obj_Que1.Perent_QID = 0;
                if (Convert.ToInt32(drpCategory.SelectedValue) != 0)
                {
                    obj_Que1.CategoryID = Convert.ToInt32(drpCategory.SelectedValue);
                }
                if (Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue) != 0)
                {
                    obj_Que1.BeforeQID = Convert.ToInt32(DrpBeforeThisQuestion.SelectedValue);
                }
                if (Convert.ToInt32(DrpAfterThisQuestion.SelectedValue) != 0)
                {
                    obj_Que1.AfterQID = Convert.ToInt32(DrpAfterThisQuestion.SelectedValue);
                }
                if (Convert.ToInt32(DrpGroup.SelectedValue) != 0)
                {
                    obj_Que1.GroupID = Convert.ToInt32(DrpGroup.SelectedValue);
                }
                obj_Que1.Choise = txtChoice.Text;
                obj_Que1.Remarks = txtRemark.Text;
                obj_Que1.Weitage = txtWeitage.Text;
                if (Convert.ToInt32(drpchoiceList.SelectedValue) != 0)
                {
                    obj_Que1.ChoiseType = Convert.ToInt32(drpchoiceList.SelectedValue);
                }
                obj_Que1.CreateDate = DateTime.Now;
                obj_Que1.TenentID = TID;
                obj_Que1.CreateBy = UID;
                obj_Que1.LocationID = LID;
                obj_Que1.Avtive = true;
                obj_Que1.Deleted = true;
                DB.QuestionMasters.AddObject(obj_Que1);
                DB.SaveChanges();
                if (Convert.ToInt32(drpchoiceList.SelectedValue) != 0)
                {
                    //List<OptionMaster> List_MainOption1 = (List<OptionMaster>)Session["MainOption"];
                    for (int i = 0; i < ListviewMainOption111.Items.Count(); i++)
                    {
                        Label lblOptionName = (Label)ListviewMainOption111.Items[i].FindControl("lblOptionName");
                        Database.OptionMaster Obj_Option = new OptionMaster();
                        Obj_Option.ID = DB.OptionMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.OptionMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                        Obj_Option.TenentID = TID;
                        Obj_Option.LocationID = LID;
                        Obj_Option.QuestionID = obj_Que1.ID;
                        Obj_Option.Active = true;
                        Obj_Option.Deleted = true;
                        Obj_Option.DateTime = DateTime.Now;
                        Obj_Option.CreatedBy = UID;
                        Obj_Option.OptionName = lblOptionName.Text;
                        Obj_Option.Active = true;
                        DB.OptionMasters.AddObject(Obj_Option);
                        DB.SaveChanges();
                    }


                }

            //}
            //else
            //{
            //    Panel2.Visible = true;
            //}
            btnAnswerPreview.Attributes["class"] = btnAnswerPreview.Attributes["class"].Replace("btn default btn-lg disabled", "btn default btn-lg");

            ViewState["PreviewID"] = ViewState["QID"];
            Panel1.Visible = true;
            readMode();
            Button4.Visible = true;
            btnSubmitForSing.Visible = false;
            btnAdd.Visible = true;

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
            int PreID = Convert.ToInt32(txtqutionnumber.Text);
            if (DB.QuestionMasters.Where(p => p.ID == PreID && p.Deleted == true && p.Avtive == true && p.TenentID == TID).Count() > 0)
            {
                Database.QuestionMaster obj_Question = DB.QuestionMasters.SingleOrDefault(p => p.ID == PreID && p.Deleted == true && p.Avtive == true && p.TenentID == TID);
                lblshowQuestion.Text = obj_Question.QuestionLang1;

                if (DB.OptionMasters.Where(p => p.QuestionID == obj_Question.ID && p.TenentID == TID).Count() > 0)
                {
                    List<Database.OptionMaster> AddOption_List = DB.OptionMasters.Where(p => p.QuestionID == obj_Question.ID && p.TenentID == TID).ToList();
                    if (obj_Question.ChoiseType == 1)
                    {
                        pnlquePreviewMultiple.Visible = false;
                        pnlcommasepanspreview.Visible = true;
                        pnlsingleanspreview.Visible = false;
                        //string CommaName="";ListBoxcommasepAnswer
                        //foreach(OptionMaster item1 in AddOption_List)
                        //{
                        //    CommaName = item1.OptionName + ",";
                        //}
                        // lblcommasepAnswer.Text = CommaName;
                        ListBoxcommasepAnswer.DataSource = AddOption_List;
                        ListBoxcommasepAnswer.DataTextField = "OptionName";
                        ListBoxcommasepAnswer.DataValueField = "ID";
                        ListBoxcommasepAnswer.DataBind();
                    }
                    else if (obj_Question.ChoiseType == 2)
                    {
                        pnlcommasepanspreview.Visible = false;
                        pnlsingleanspreview.Visible = false;
                        pnlquePreviewMultiple.Visible = true;
                        chmultipletpreview.DataSource = AddOption_List;
                        chmultipletpreview.DataTextField = "OptionName";
                        chmultipletpreview.DataValueField = "ID";
                        chmultipletpreview.DataBind();
                        //chmultpreview
                    }
                    else if (obj_Question.ChoiseType == 3)
                    {
                        pnlcommasepanspreview.Visible = false;
                        pnlquePreviewMultiple.Visible = false;
                        pnlsingleanspreview.Visible = true;
                        RadiosinPreview.DataSource = AddOption_List;
                        RadiosinPreview.DataTextField = "OptionName";
                        RadiosinPreview.DataValueField = "ID";
                        RadiosinPreview.DataBind();
                    }
                    else if (obj_Question.ChoiseType == 4)
                    {
                        pnlanspreviewswaper.Visible = true;
                    }
                }
            }
        }

        protected void btnGrupSave_Click(object sender, EventArgs e)
        {
            TBLGROUP objTBLGROUP = new TBLGROUP();
            objTBLGROUP.ITGROUPID = DB.TBLGROUPs.Count() > 0 ? Convert.ToInt32(DB.TBLGROUPs.Max(p => p.ITGROUPID) + 1) : 1;
            objTBLGROUP.ITGROUPDESC1 = txtgrupname.Text;
            objTBLGROUP.ITGROUPDESC2 = txtgrupname.Text;
            objTBLGROUP.ITGROUPREMARKS = txtgrupname.Text;
            objTBLGROUP.GroupType = "QA";
            objTBLGROUP.ACTIVE = "1";
            objTBLGROUP.ACTIVE_Flag = true;
            DB.TBLGROUPs.AddObject(objTBLGROUP);
            DB.SaveChanges();
            Classes.EcommAdminClass.getdropdown(DrpGroup, TID, "", "", "", "TBLGROUP");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LastData();
        }

    }

    public class OptionAddValue
    {
        public string Name { get; set; }
        public int OPID1 { get; set; }
    }
}