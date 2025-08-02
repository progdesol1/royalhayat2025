using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;
namespace Web.CRM
{
    public partial class QuestionList : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        List<Database.QuestionMaster> Que_List = new List<Database.QuestionMaster>();
        int i = 0;
        int j = 0;
        public static int addf = 0;
        public static int addf4 = 0;
        int QueID = 0;
        int CampaignID = 0;
        int ChoiceType = 0;
        int CreteList = 0;
        string Name = "";
        int TitleID12 = 0;
        int a1 = 0;
        int b1 = 0;
        int count = 0;
        List<OptionMaster> Option_List = new List<OptionMaster>();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                ViewState["ConID"] = null;
                ViewState["CampID"] = null;
                ViewState["CompID"] = null;
                BindQuestionList();
                if (Request.QueryString["ConID"] != null && Request.QueryString["CampID"] != null && Request.QueryString["LeadName"] != null && Request.QueryString["TitleID11"] != null && Request.QueryString["GrupID"] != null)
                {
                    int QID = Convert.ToInt32(Request.QueryString["GrupID"]);
                    Bind_QUESTION_Master(QID);
                    int CMYID=Convert.ToInt32(Request.QueryString["ConID"]);
                    ViewState["ConID"] = CMYID;
                    ViewState["CampID"] = Request.QueryString["CampID"].ToString();
                    Name = Request.QueryString["LeadName"];
                    TitleID12 = Convert.ToInt32(Request.QueryString["TitleID11"]);
                    string ContecName = Request.QueryString["ContectName"];
                     Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CMYID);
                     lblInfo.Text = "This " + "<b>" + Name + "</b>" + " belongs to " + "<b>" + ContecName + "</b>" + "<br/><b> BUSPHONE</b> :- " + objtbl_CONTACT.BUSPHONE1 + " <b> Mobile </b>:- " + objtbl_CONTACT.MOBPHONE + "  <b>Email</b> :- " + objtbl_CONTACT.EMAIL1;

                }
                if (Request.QueryString["CompID"] != null && Request.QueryString["CampID"] != null && Request.QueryString["LeadName"] != null && Request.QueryString["TitleID11"] != null && Request.QueryString["GrupID"] != null)
                {
                    int QID = Convert.ToInt32(Request.QueryString["GrupID"]);
                    Bind_QUESTION_Master(QID);
                    int CMYID = Convert.ToInt32(Request.QueryString["CompID"]);
                    ViewState["CompID"] = CMYID;
                    ViewState["CampID"] = Request.QueryString["CampID"].ToString(); 
                    Name = Request.QueryString["LeadName"].ToString();
                    TitleID12 = Convert.ToInt32(Request.QueryString["TitleID11"]);
                    string ContecName = Request.QueryString["Compname"];
                    Database.TBLCOMPANYSETUP objtbl_CONTACT = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CMYID);
                    lblInfo.Text = "This " + "<b>" + Name + "</b>" + " belongs to "+"<b>" + ContecName +"</b>"+ "<br/> <b>BUSPHONE</b> :- " + objtbl_CONTACT.BUSPHONE1 + " <b> Mobile</b> :- " + objtbl_CONTACT.MOBPHONE + " <b> Email</b> :- " + objtbl_CONTACT.EMAIL1;
                }
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
        public void BindQuestionList()
        {
            Que_List = DB.QuestionMasters.Where(p => p.Deleted == true && p.Avtive == true && p.TenentID == TID).Take(1).ToList();
            if (Que_List.Count() >= i)
            {
                string Ques = Que_List[i].QuestionLang1.ToString().Replace("?", "");
                int ChType = Convert.ToInt32(Que_List[i].ChoiseType);
                int QueID = Convert.ToInt32(Que_List[i].ID);
                // lblshowQuestion.Text = Ques + "?";
                BindChoiceOptionData(ChType, QueID);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // pnltools.Visible = true;
        }


        public void BindChoiceOptionData(int ChoiceType, int QID)
        {

            //if (DB.OptionMaster.Where(p => p.QuestionID == QID).Count() > 0)
            //{
            //    List<Database.OptionMaster> AddOption_List = DB.OptionMaster.Where(p => p.QuestionID == QID).ToList();
            //    int CID = Convert.ToInt32(ChoiceType);
            //if (CID == 1)
            //{
            //    pnlcommasepanspreview.Visible = true;
            //    pnlquePreviewMultiple.Visible = false;
            //    pnlsingleanspreview.Visible = false;
            //    pnlanspreviewswaper.Visible = false;
            //    pnlRanking.Visible = false;

            //    ListAllOption.DataSource = AddOption_List;
            //    ListAllOption.DataTextField = "OptionName";
            //    ListAllOption.DataValueField = "ID";
            //    ListAllOption.DataBind();

            //}
            //else if (CID == 2)
            //{
            //    pnlcommasepanspreview.Visible = false;
            //    pnlquePreviewMultiple.Visible = true;
            //    pnlsingleanspreview.Visible = false;
            //    pnlanspreviewswaper.Visible = false;
            //    pnlRanking.Visible = false;//pnlquePreviewMultiple

            //    chmultipletpreview.DataSource = AddOption_List;
            //    chmultipletpreview.DataTextField = "OptionName";
            //    chmultipletpreview.DataValueField = "ID";
            //    chmultipletpreview.DataBind();
            //}
            //else if (CID == 3)
            //{
            //    pnlcommasepanspreview.Visible = false;
            //    pnlquePreviewMultiple.Visible = false;
            //    pnlsingleanspreview.Visible = false;
            //    pnlanspreviewswaper.Visible = true;
            //    pnlRanking.Visible = false;//pnlsingleanspreview

            //    RadiosinPreview.DataSource = AddOption_List;
            //    RadiosinPreview.DataTextField = "OptionName";
            //    RadiosinPreview.DataValueField = "ID";
            //    RadiosinPreview.DataBind();
            //}
            //else if (CID == 4)
            //{
            //    pnlcommasepanspreview.Visible = false;
            //    pnlquePreviewMultiple.Visible = false;
            //    pnlsingleanspreview.Visible = false;
            //    pnlanspreviewswaper.Visible = false;
            //    pnlRanking.Visible = true;
            //}
            //else if (CID == 5)
            //{
            //    pnlcommasepanspreview.Visible = false;
            //    pnlquePreviewMultiple.Visible = false;
            //    pnlsingleanspreview.Visible = false;
            //    pnlanspreviewswaper.Visible = false;
            //    pnlRanking.Visible = true;
            //}
            // }
        }

        protected void btnAnswer_Click(object sender, EventArgs e)
        {
            int QID = 0;
            int ChoiceID = 0;
            if (QID != 0 && ChoiceID != 0)
            {
                Database.RowMaster obj_Raw = new Database.RowMaster();

            }
        }
        protected void Bind_QUESTION_Master(int QID)
        {
            Que_List = DB.QuestionMasters.Where(p => p.Avtive == true && p.Deleted == true && p.GroupID == QID && p.Perent_QID == 0 && p.TenentID == TID).ToList();
            if (Que_List.Count() > 0)
            {
                repAccordian.DataSource = Que_List;
                repAccordian.DataBind();

                for (int j = 0; j < Que_List.Count(); j++)
                {
                    int MainQueID = Convert.ToInt32(Que_List[j].ID);
                    var ListofSub = DB.QuestionMasters.Where(p => p.Perent_QID == MainQueID && p.TenentID == TID).ToList();

                    if (ListofSub.Count() > 0)
                    {
                        ((Repeater)repAccordian.Items[j].FindControl("repAccordianSubQuion")).DataSource = ListofSub.OrderBy(p => p.SerialNo);
                        ((Repeater)repAccordian.Items[j].FindControl("repAccordianSubQuion")).DataBind();
                        for (int i = 0; i < ListofSub.Count(); i++)
                        {
                            addf = 1;
                            // addf4 = 1;
                            QueID = Convert.ToInt32(ListofSub[i].ID);
                            Option_List = DB.OptionMasters.Where(p => p.QuestionID == QueID).ToList();
                            if (Option_List.Count() > 0)
                            {
                                if (ListofSub[i].ChoiseType == 3)
                                {

                                    ((Repeater)repAccordian.Items[j].FindControl("Repeater1")).DataSource = Option_List;
                                    ((Repeater)repAccordian.Items[j].FindControl("Repeater1")).DataBind();
                                }
                                else if (ListofSub[i].ChoiseType == 2)
                                {

                                    ((Repeater)repAccordian.Items[j].FindControl("Repeater2")).DataSource = Option_List;
                                    ((Repeater)repAccordian.Items[j].FindControl("Repeater2")).DataBind();
                                }
                                else if (ListofSub[i].ChoiseType == 1)
                                {

                                    ((Repeater)repAccordian.Items[j].FindControl("Repeater4")).DataSource = Option_List;
                                    ((Repeater)repAccordian.Items[j].FindControl("Repeater4")).DataBind();
                                }
                            }
                            if (ListofSub[i].ChoiseType == 4)
                            {
                                if (Option_List.Count == 0)
                                {
                                    OptionMaster obj = new OptionMaster();
                                    Option_List.Add(obj);
                                }

                                ((Repeater)repAccordian.Items[j].FindControl("Repeater3")).DataSource = Option_List;
                                ((Repeater)repAccordian.Items[j].FindControl("Repeater3")).DataBind();
                            }
                            else if (ListofSub[i].ChoiseType == 5)
                            {
                                if (Option_List.Count == 0)
                                {
                                    OptionMaster obj = new OptionMaster();
                                    Option_List.Add(obj);
                                }

                                ((Repeater)repAccordian.Items[j].FindControl("Repeater5")).DataSource = Option_List;
                                ((Repeater)repAccordian.Items[j].FindControl("Repeater5")).DataBind();
                            }
                        }
                    }
                    else
                    {
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
        protected void BtnSaveOpin_Click(object sender, EventArgs e)
        {
            Button bn = ((Button)sender);
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

        protected void repAccordian_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnQueSave")
            {
                Database.RowMaster obj_Row = new Database.RowMaster();
                int QID1 = Convert.ToInt32(e.CommandArgument);
                int qid = 0, opinid = 0, opationid = 0;
                int chkmode = 0;
                string Type = "";
                int CompContID = 0;
                int CampID = 0;
                int AnsBy = 0;
                bool flag = false;
                //hiddenqueby
                HiddenField hiddenid = (HiddenField)e.Item.FindControl("hiddenqueby");
                int QuesBy = Convert.ToInt32(hiddenid.Value);
                if (Request.QueryString["ConID"] != null && Request.QueryString["CampID"] != null && Request.QueryString["LeadName"] != null)
                {
                    // Bind_QUESTION_Master();
                    CompContID = Convert.ToInt32(Request.QueryString["ConID"]);//ViewState["ConID"]
                    ViewState["CampID"] = Request.QueryString["CampID"].ToString();
                    CampID = Convert.ToInt32(Request.QueryString["CampID"]);
                    Name = Request.QueryString["LeadName"].ToString();
                    Type = "Contact";
                }
                if (Request.QueryString["CompID"] != null && Request.QueryString["CampID"] != null && Request.QueryString["LeadName"] != null)
                {
                    // Bind_QUESTION_Master();
                    CompContID = Convert.ToInt32(Request.QueryString["CompID"]); //ViewState["CompID"]
                    ViewState["CampID"] = Request.QueryString["CampID"].ToString();
                    CampID = Convert.ToInt32(Request.QueryString["CampID"]);
                    Name = Request.QueryString["LeadName"].ToString();
                    Type = "Company";
                }
                Panel rpvContact = ((Panel)e.Item.FindControl("pnlContactDrop"));
                Panel rpvpnlCompany = ((Panel)e.Item.FindControl("pnlCompany"));
                TextBox txtDate = (TextBox)e.Item.FindControl("txtAnsDatetime");
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemarks");
                Label lblQueID = (Label)e.Item.FindControl("lbl_QUESTION_id");
                DropDownList Drp_Contact = (DropDownList)rpvContact.FindControl("DrpContact");
                if ((Drp_Contact.SelectedValue) != "")
                {
                    AnsBy = Convert.ToInt32(Drp_Contact.SelectedValue);
                }
                DropDownList Drp_Company = (DropDownList)rpvpnlCompany.FindControl("DrpCompany");
                if ((Drp_Company.SelectedValue) != "")
                {
                    AnsBy = Convert.ToInt32(Drp_Company.SelectedValue);
                }
                var ListofSub = DB.QuestionMasters.Where(p => p.Perent_QID == QID1 && p.TenentID == TID).ToList();
                if (ListofSub.Count() > 0)
                {
                    Repeater repAccordianSubQuion = ((Repeater)e.Item.FindControl("repAccordianSubQuion"));
                    if (repAccordianSubQuion.Items.Count > 0)
                    {
                        for (int I = 0; I < repAccordianSubQuion.Items.Count; I++)
                        {
                            Label lblQutionID = (Label)repAccordianSubQuion.Items[I].FindControl("lblQutionID");
                            Repeater Repeater1 = (Repeater)repAccordianSubQuion.Items[I].FindControl("Repeater1");
                            Repeater listCheckBoxList = (Repeater)repAccordianSubQuion.Items[I].FindControl("listCheckBoxList");
                            Repeater Repeater3 = (Repeater)repAccordianSubQuion.Items[I].FindControl("Repeater3");
                            Repeater Repeater5 = (Repeater)repAccordianSubQuion.Items[I].FindControl("Repeater5");
                            Repeater Repeater4 = (Repeater)repAccordianSubQuion.Items[I].FindControl("Repeater4");
                            //   int ID = DB.RowMaster.Count() > 0 ? Convert.ToInt32(DB.RowMaster.Max(p => p.ID) + 1) : 1;
                            if (Repeater1.Items.Count > 0)
                            {
                                for (int ii = 0; ii < Repeater1.Items.Count; ii++)
                                {
                                    RadioButtonList rbl = (RadioButtonList)Repeater1.Items[ii].FindControl("QuestionsList");
                                    foreach (ListItem item in rbl.Items)
                                    {
                                        if (item.Selected)
                                        {
                                            string selectedValue = rbl.SelectedItem.Text;
                                            int OptionID = Convert.ToInt32(rbl.SelectedValue);
                                            RowMaster obj_Rowzxc = new Database.RowMaster();
                                            obj_Rowzxc.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                            obj_Rowzxc.ContactComapnyID = CompContID;
                                            obj_Rowzxc.AnswerBy = AnsBy;
                                            obj_Rowzxc.Answer = true;
                                            obj_Rowzxc.RowNameAnswer = selectedValue;
                                            obj_Rowzxc.Remarks = txtRemark.Text;
                                            obj_Rowzxc.QuestionBy = QuesBy;
                                            obj_Rowzxc.Deleted = true;
                                            obj_Rowzxc.Active = true;
                                            obj_Rowzxc.QuetionID = Convert.ToInt32(lblQutionID.Text);
                                            obj_Rowzxc.Type = Type;
                                            if (txtDate.Text != "")
                                            {
                                                obj_Rowzxc.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                            }
                                            else
                                            {
                                                obj_Rowzxc.AnsDateTime = DateTime.Now;
                                            }
                                            obj_Rowzxc.LocationID = LID;
                                            obj_Rowzxc.TenentID = TID;
                                            obj_Rowzxc.OptionMasterID = Convert.ToInt32(OptionID);
                                            DB.RowMasters.AddObject(obj_Rowzxc);
                                            DB.SaveChanges();
                                        }
                                    }

                                }
                            }
                            if (listCheckBoxList.Items.Count > 0)
                            {
                                for (int ii = 0; ii < listCheckBoxList.Items.Count; ii++)
                                {
                                    CheckBoxList chmultipletpreview = (CheckBoxList)listCheckBoxList.Items[ii].FindControl("chmultipletpreview");
                                    foreach (ListItem item in chmultipletpreview.Items)
                                    {
                                        if (item.Selected)
                                        {
                                            string selectedValue = item.Text;
                                            int OptionID = Convert.ToInt32(item.Value);
                                            // If the item is selected, add the value to the list.
                                            RowMaster obj_Rowzxc = new Database.RowMaster();
                                            obj_Rowzxc.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                            obj_Rowzxc.ContactComapnyID = CompContID;
                                            obj_Rowzxc.AnswerBy = AnsBy;
                                            obj_Rowzxc.Answer = true;
                                            obj_Rowzxc.RowNameAnswer = selectedValue;
                                            obj_Rowzxc.Remarks = txtRemark.Text;
                                            obj_Rowzxc.QuestionBy = QuesBy;
                                            obj_Rowzxc.Deleted = true;
                                            obj_Rowzxc.Active = true;
                                            obj_Rowzxc.QuetionID = Convert.ToInt32(lblQutionID.Text);
                                            obj_Rowzxc.Type = Type;

                                            obj_Row.TenentID = TID;
                                            if (txtDate.Text != "")
                                            {
                                                obj_Rowzxc.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                            }
                                            else
                                            {
                                                obj_Rowzxc.AnsDateTime = DateTime.Now;
                                            }
                                            obj_Rowzxc.LocationID = LID;
                                            obj_Rowzxc.TenentID = TID;
                                            obj_Rowzxc.OptionMasterID = Convert.ToInt32(OptionID);
                                            DB.RowMasters.AddObject(obj_Rowzxc);
                                            DB.SaveChanges();

                                        }
                                        else
                                        {
                                            // Item is not selected, do something else.
                                        }
                                    }
                                }
                            }
                            if (Repeater3.Items.Count > 0)
                            {
                                for (int ii = 0; ii < Repeater3.Items.Count; ii++)
                                {
                                    string AnserTrur = "";

                                    RadioButton radioswapYes = (RadioButton)Repeater3.Items[ii].FindControl("radioswapYes");
                                    RadioButton radioswapNo = (RadioButton)Repeater3.Items[ii].FindControl("radioswapNo");
                                    if (radioswapYes.Checked == true)
                                        AnserTrur = "Yes";
                                    else
                                        AnserTrur = "No";
                                    RowMaster obj_Rowzxc = new Database.RowMaster();
                                    obj_Rowzxc.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                    obj_Rowzxc.ContactComapnyID = CompContID;
                                    obj_Rowzxc.AnswerBy = AnsBy;
                                    obj_Rowzxc.Answer = true;
                                    obj_Rowzxc.RowNameAnswer = AnserTrur;
                                    obj_Rowzxc.Remarks = txtRemark.Text;
                                    obj_Rowzxc.QuestionBy = QuesBy;
                                    obj_Rowzxc.Deleted = true;
                                    obj_Rowzxc.Active = true;
                                    obj_Rowzxc.QuetionID = Convert.ToInt32(lblQutionID.Text);
                                    obj_Rowzxc.Type = Type;
                                    if (txtDate.Text != "")
                                    {
                                        obj_Rowzxc.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                    }
                                    else
                                    {
                                        obj_Rowzxc.AnsDateTime = DateTime.Now;
                                    }
                                    obj_Rowzxc.LocationID = LID;
                                    obj_Rowzxc.TenentID = TID;
                                    //   obj_Row.OptionMasterID = Convert.ToInt32(lblQueID.Text);
                                    DB.RowMasters.AddObject(obj_Rowzxc);
                                    DB.SaveChanges();
                                }
                            }
                            if (Repeater5.Items.Count > 0)
                            {
                                for (int ii = 0; ii < Repeater5.Items.Count; ii++)
                                {
                                    TextBox range_4 = (TextBox)Repeater5.Items[ii].FindControl("range_4");
                                    RowMaster obj_Rowtext = new Database.RowMaster();
                                    obj_Rowtext.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                    obj_Rowtext.ContactComapnyID = CompContID;
                                    obj_Rowtext.AnswerBy = AnsBy;
                                    obj_Rowtext.Answer = true;
                                    obj_Rowtext.RowNameAnswer = range_4.Text;
                                    obj_Rowtext.Remarks = txtRemark.Text;
                                    obj_Rowtext.QuestionBy = QuesBy;
                                    obj_Rowtext.Deleted = true;
                                    obj_Rowtext.Active = true;
                                    obj_Rowtext.QuetionID = Convert.ToInt32(lblQutionID.Text);
                                    obj_Rowtext.Type = Type;
                                    if (txtDate.Text != "")
                                    {
                                        obj_Rowtext.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                    }
                                    else
                                    {
                                        obj_Rowtext.AnsDateTime = DateTime.Now;
                                    }
                                    obj_Rowtext.LocationID = LID;
                                    obj_Rowtext.TenentID = TID;
                                    //  obj_Row.OptionMasterID = Convert.ToInt32(lblQueID.Text);
                                    DB.RowMasters.AddObject(obj_Rowtext);
                                    DB.SaveChanges();

                                }
                            }
                            if (Repeater4.Items.Count > 0)
                            {

                            }
                        }
                    }

                }
                else
                {
                    try
                    {

                        Repeater Repeater1 = ((Repeater)e.Item.FindControl("Repeater1"));
                        Repeater rpvCheckbox = ((Repeater)e.Item.FindControl("Repeater2"));
                        Repeater Repeater3 = ((Repeater)e.Item.FindControl("Repeater3"));
                        Repeater Repeater5 = ((Repeater)e.Item.FindControl("Repeater5"));
                        Repeater Repeater4 = ((Repeater)e.Item.FindControl("Repeater4"));
                        if (Repeater1.Items.Count > 0)
                        {
                            for (int ii = 0; ii < Repeater1.Items.Count; ii++)
                            {
                                RadioButtonList rbl = (RadioButtonList)Repeater1.Items[ii].FindControl("QuestionsList");
                                foreach (ListItem item in rbl.Items)
                                {
                                    if (item.Selected)
                                    {
                                        string selectedValue = rbl.SelectedItem.Text;
                                        int OptionID = Convert.ToInt32(rbl.SelectedValue);
                                        RowMaster obj_Rowzxc = new Database.RowMaster();
                                        obj_Rowzxc.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                        obj_Rowzxc.ContactComapnyID = CompContID;
                                        obj_Rowzxc.AnswerBy = AnsBy;
                                        obj_Rowzxc.Answer = true;
                                        obj_Rowzxc.RowNameAnswer = selectedValue;
                                        obj_Rowzxc.Remarks = txtRemark.Text;
                                        obj_Rowzxc.QuestionBy = QuesBy;
                                        obj_Rowzxc.Deleted = true;
                                        obj_Rowzxc.Active = true;
                                        obj_Rowzxc.QuetionID = Convert.ToInt32(lblQueID.Text);
                                        obj_Rowzxc.Type = Type;
                                        if (txtDate.Text != "")
                                        {
                                            obj_Rowzxc.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                        }
                                        else
                                        {
                                            obj_Rowzxc.AnsDateTime = DateTime.Now;
                                        }
                                        obj_Rowzxc.LocationID = LID;
                                        obj_Rowzxc.TenentID = TID;
                                        obj_Rowzxc.OptionMasterID = Convert.ToInt32(OptionID);
                                        DB.RowMasters.AddObject(obj_Rowzxc);
                                        DB.SaveChanges();
                                    }
                                }

                            }
                        }
                        if (rpvCheckbox.Items.Count > 0)
                        {
                            for (int ii = 0; ii < rpvCheckbox.Items.Count; ii++)
                            {
                                CheckBoxList chmultipletpreview = (CheckBoxList)rpvCheckbox.Items[ii].FindControl("chmultipletpreview");
                                foreach (ListItem item in chmultipletpreview.Items)
                                {
                                    if (item.Selected)
                                    {
                                        string selectedValue = item.Text;
                                        int OptionID = Convert.ToInt32(item.Value);
                                        // If the item is selected, add the value to the list.
                                        RowMaster obj_Rowzxc = new Database.RowMaster();
                                        obj_Rowzxc.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                        obj_Rowzxc.ContactComapnyID = CompContID;
                                        obj_Rowzxc.AnswerBy = AnsBy;
                                        obj_Rowzxc.Answer = true;
                                        obj_Rowzxc.RowNameAnswer = selectedValue;
                                        obj_Rowzxc.Remarks = txtRemark.Text;
                                        obj_Rowzxc.QuestionBy = QuesBy;
                                        obj_Rowzxc.Deleted = true;
                                        obj_Rowzxc.Active = true;
                                        obj_Rowzxc.QuetionID = Convert.ToInt32(lblQueID.Text);
                                        obj_Rowzxc.Type = Type;
                                        if (txtDate.Text != "")
                                        {
                                            obj_Rowzxc.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                        }
                                        else
                                        {
                                            obj_Rowzxc.AnsDateTime = DateTime.Now;
                                        }
                                        obj_Rowzxc.LocationID = LID;
                                        obj_Rowzxc.TenentID = TID;
                                        obj_Rowzxc.OptionMasterID = Convert.ToInt32(OptionID);
                                        DB.RowMasters.AddObject(obj_Rowzxc);
                                        DB.SaveChanges();

                                    }
                                    else
                                    {
                                        // Item is not selected, do something else.
                                    }
                                }
                            }
                        }
                        if (Repeater3.Items.Count > 0)
                        {
                            for (int ii = 0; ii < Repeater3.Items.Count; ii++)
                            {
                                string AnserTrur = "";

                                RadioButton radioswapYes = (RadioButton)Repeater3.Items[ii].FindControl("radioswapYes");
                                RadioButton radioswapNo = (RadioButton)Repeater3.Items[ii].FindControl("radioswapNo");
                                if (radioswapYes.Checked == true)
                                    AnserTrur = "Yes";
                                else
                                    AnserTrur = "No";
                                RowMaster obj_Rowzxc = new Database.RowMaster();
                                obj_Rowzxc.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                obj_Rowzxc.ContactComapnyID = CompContID;
                                obj_Rowzxc.AnswerBy = AnsBy;
                                obj_Rowzxc.Answer = true;
                                obj_Rowzxc.RowNameAnswer = AnserTrur;
                                obj_Rowzxc.Remarks = txtRemark.Text;
                                obj_Rowzxc.QuestionBy = QuesBy;
                                obj_Rowzxc.Deleted = true;
                                obj_Rowzxc.Active = true;
                                obj_Rowzxc.QuetionID = Convert.ToInt32(lblQueID.Text);
                                obj_Rowzxc.Type = Type;
                                if (txtDate.Text != "")
                                {
                                    obj_Rowzxc.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                }
                                else
                                {
                                    obj_Rowzxc.AnsDateTime = DateTime.Now;
                                }
                                obj_Rowzxc.LocationID = LID;
                                obj_Rowzxc.TenentID = TID;
                                //   obj_Row.OptionMasterID = Convert.ToInt32(lblQueID.Text);
                                DB.RowMasters.AddObject(obj_Rowzxc);
                                DB.SaveChanges();
                            }
                        }
                        if (Repeater5.Items.Count > 0)
                        {
                            for (int ii = 0; ii < Repeater5.Items.Count; ii++)
                            {
                                TextBox range_4 = (TextBox)Repeater5.Items[ii].FindControl("range_4");
                                RowMaster obj_Rowtext = new Database.RowMaster();
                                obj_Rowtext.ID = DB.RowMasters.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RowMasters.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                obj_Rowtext.ContactComapnyID = CompContID;
                                obj_Rowtext.AnswerBy = AnsBy;
                                obj_Rowtext.Answer = true;
                                obj_Rowtext.RowNameAnswer = range_4.Text;
                                obj_Rowtext.Remarks = txtRemark.Text;
                                obj_Rowtext.QuestionBy = QuesBy;
                                obj_Rowtext.Deleted = true;
                                obj_Rowtext.Active = true;
                                obj_Rowtext.QuetionID = Convert.ToInt32(lblQueID.Text);
                                obj_Rowtext.Type = Type;
                                if (txtDate.Text != "")
                                {
                                    obj_Rowtext.AnsDateTime = Convert.ToDateTime(txtDate.Text);
                                }
                                else
                                {
                                    obj_Rowtext.AnsDateTime = DateTime.Now;
                                }
                                obj_Rowtext.LocationID = LID;
                                obj_Rowtext.TenentID = TID;
                                //  obj_Row.OptionMasterID = Convert.ToInt32(lblQueID.Text);
                                DB.RowMasters.AddObject(obj_Rowtext);
                                DB.SaveChanges();

                            }
                        }
                        if (Repeater4.Items.Count > 0)
                        {

                        }





                        //Insert data in lead tabel


                    }
                    catch (Exception)
                    { }
                }
                int CAMINID = 0;
                string NAmeOfData = Request.QueryString["LeadName"].ToString();
                if (Session["OppInset"] != null)
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.Campaign_ID == CampID && p.TenentID == TID).Count() < 1)
                    {

                        Database.tbl_Opportunity_Mst objtbl_Lead_Mst = new Database.tbl_Opportunity_Mst();
                        CAMINID = DB.tbl_Lead_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Opportunity_Mst.Where(p => p.TenentID == TID).Max(p => p.OpportID) + 1) : 1;
                        objtbl_Lead_Mst.OpportID = CAMINID;
                        //  objtbl_Lead_Mst.Customer_id = CompContID;
                        objtbl_Lead_Mst.TenentID = TID;

                        //  objtbl_Lead_Mst.Type = Type;
                        objtbl_Lead_Mst.Name = NAmeOfData;
                        objtbl_Lead_Mst.OppName1 = NAmeOfData;
                        objtbl_Lead_Mst.Campaign_ID = CampID;
                        objtbl_Lead_Mst.Active = true;
                        objtbl_Lead_Mst.Deleted = true;
                        objtbl_Lead_Mst.CreatedBy = UID;
                        DB.tbl_Opportunity_Mst.AddObject(objtbl_Lead_Mst);
                        DB.SaveChanges();
                    }
                }
                if (Session["LesdInset"] != null)
                {
                    if (DB.tbl_Lead_Mst.Where(p => p.Campaign_ID == CampID && p.TenentID == TID).Count() < 1)
                    {
                        Database.tbl_Opportunity_Mst objCRM_Opportun = DB.tbl_Opportunity_Mst.Single(p => p.OpportID == CampID && p.TenentID == TID);
                        Database.tbl_Lead_Mst objtbl_Lead_Mst = new Database.tbl_Lead_Mst();
                        objtbl_Lead_Mst.ID = DB.tbl_Lead_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Lead_Mst.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1; ;
                        //  objtbl_Lead_Mst.Customer_id = CompContID;
                        objtbl_Lead_Mst.TenentID = TID;
                        //  objtbl_Lead_Mst.Type = Type;
                        objtbl_Lead_Mst.LeadName1 = NAmeOfData;
                        objtbl_Lead_Mst.Name = NAmeOfData;
                        objtbl_Lead_Mst.Campaign_ID = objCRM_Opportun.Campaign_ID;
                        objtbl_Lead_Mst.Oppertunity_ID = CampID;
                        objtbl_Lead_Mst.Active = true;
                        objtbl_Lead_Mst.Deleted = true;
                        objtbl_Lead_Mst.created_by = UID;
                        DB.tbl_Lead_Mst.AddObject(objtbl_Lead_Mst);
                        DB.SaveChanges();
                    }
                }


            }
            if (e.CommandName == "LinkContact")
            {
                if (ViewState["ConID"] != null)
                {
                    int ConID = Convert.ToInt32(ViewState["ConID"]);
                    string pageurl =  "/CRM/ContactMaster.aspx?ContactMyID=" + ConID + "&Mode=Write";

                    Page.ClientScript.RegisterStartupScript(
               this.GetType(), "OpenWindow", "window.open('"+pageurl+"','_newtab');", true);


                    
                    //  Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(pageurl)));
                    // Response.Redirect("ContactMaster.aspx?ContactMyID=" + ConID + "&Mode=Write");
                }

            }
            if (e.CommandName == "LinkCompany")
            {
                if (ViewState["CompID"]!=null)
                {
                    int ConID = Convert.ToInt32(ViewState["CompID"]);
                    string pageurl = "/CRM/CompanyMaster.aspx?COMPID=" + ConID + "&Mode=Write";

                    Page.ClientScript.RegisterStartupScript(
               this.GetType(), "OpenWindow", "window.open('" + pageurl + "','_newtab');", true);
                }
                
            }
        }

        protected void repAccordian_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item)
            //{

            // Label lblCType = (Label)e.Item.FindControl("lblChoiceType");
            TitleID12 = Convert.ToInt32(Request.QueryString["TitleID11"]);
            Label lblSingleType = (Label)e.Item.FindControl("lblNoteSingle");
            Label lblMultipleType = (Label)e.Item.FindControl("lblMultipleNote");

            TextBox txtDate = (TextBox)e.Item.FindControl("txtAnsDatetime");
            txtDate.Text = DateTime.Now.ToShortDateString();
            if (DB.REFTABLEs.Where(p => p.REFID == TitleID12 && p.REFTYPE == "Search" && p.TenentID == TID).Count() > 0)
            {
                Database.REFTABLE obj_Ref = DB.REFTABLEs.SingleOrDefault(p => p.REFID == TitleID12 && p.REFTYPE == "Search" && p.TenentID == TID);
                Panel pnlCompany = (Panel)e.Item.FindControl("pnlCompany");
                Panel pnlCpntact = (Panel)e.Item.FindControl("pnlContactDrop");
                if (obj_Ref.REFSUBTYPE == "Company")
                {

                    pnlCompany.Visible = true;
                    pnlCpntact.Visible = false;
                    if (Request.QueryString["CompID"] != null)
                    {
                        int CompID1 = Convert.ToInt32(Request.QueryString["CompID"]);
                        List<TBLCONTACT> obj_Bus1 = new List<TBLCONTACT>();
                        List<Database.tblCONTACTBu> List_ContactBus = DB.tblCONTACTBus.Where(p => p.CompID == CompID1 && p.TenentID == TID).ToList();

                        foreach (tblCONTACTBu subitem in List_ContactBus.Distinct())
                        {
                            Database.TBLCONTACT objsub_Contact = DB.TBLCONTACTs.SingleOrDefault(p => p.ContactMyID == subitem.ContactMyID && p.TenentID == TID);
                            obj_Bus1.Add(objsub_Contact);
                        }

                        var Data = (from item in DB.tblCONTACTBus where item.CompID == CompID1 select item).Distinct().ToList();
                        DropDownList Drp_Company = (DropDownList)pnlCompany.FindControl("DrpCompany");
                        Drp_Company.DataSource = obj_Bus1;//Data;//obj_Bus1;//DB.CRM_TBLCOMPANYSETUP.Where(p => p.TenentID == TID && p.Approved == 0);
                        Drp_Company.DataTextField = "PersName1";
                        Drp_Company.DataValueField = "ContactMyID";
                        Drp_Company.DataBind();
                        Drp_Company.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }
                else
                {

                    pnlCompany.Visible = false;
                    pnlCpntact.Visible = true;
                    if (Request.QueryString["ConID"] != null)
                    {
                        int CID12 = Convert.ToInt32(Request.QueryString["ConID"]);
                        List<TBLCONTACT> obj_Bus = new List<TBLCONTACT>();
                        List<Database.tblCONTACTBu> List_ContactBus = DB.tblCONTACTBus.Where(p => p.ContactMyID == CID12 && p.TenentID == TID).ToList();
                        foreach (tblCONTACTBu subitem in List_ContactBus.Distinct())
                        {
                            Database.TBLCONTACT objsub_Contact = DB.TBLCONTACTs.SingleOrDefault(p => p.ContactMyID == subitem.ContactMyID && p.TenentID == TID);
                            obj_Bus.Add(objsub_Contact);
                        }


                        DropDownList Drp_Contact = (DropDownList)pnlCpntact.FindControl("DrpContact");
                        Drp_Contact.DataSource = obj_Bus;
                        Drp_Contact.DataTextField = "PersName1";
                        Drp_Contact.DataValueField = "ContactMyID";
                        Drp_Contact.DataBind();
                        Drp_Contact.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }

                }

            }

            //int CType = Convert.ToInt32(lblCType.Text);
            //if (CType == 3 || CType == 4)
            //{
            //    lblSingleType.Text = " " + "(Select Single from All )";
            //}
            //else
            //{
            //    lblMultipleType.Text = " " + "(Select Multiple 2 from all)";
            //}
            // lblMultipleType.Text=a1 +","+b1;
        }

        protected void btnCompleteit_Click(object sender, EventArgs e)
        {
           
          
            if (Request.QueryString["CompID"] != null)
            {
                //CampaignID = Convert.ToInt32(Request.QueryString["CompID"]);
                //Response.Redirect("Campaign_Mst.aspx?ID=" + CampaignID);
                int GrupID = Convert.ToInt32(Request.QueryString["GrupID"]);
                int CamID = Convert.ToInt32(Request.QueryString["CampID"]);
                int ID = Convert.ToInt32(Request.QueryString["TitleID11"]);
                Response.Redirect("SearchTitalPage.aspx?TID=" + ID + "&CampID=" + CamID + "&LeadName=" + Name + "&GrupID=" + GrupID);
            }
        }

        protected void repAccordianSubQuion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblQutionID = (Label)e.Item.FindControl("lblQutionID");
            Label lblsubChoyesh = (Label)e.Item.FindControl("lblsubChoyesh");
            Repeater Repeater1 = (Repeater)e.Item.FindControl("Repeater1");
            Repeater Repeater2 = (Repeater)e.Item.FindControl("listCheckBoxList");
            Repeater Repeater3 = (Repeater)e.Item.FindControl("Repeater3");
            Repeater Repeater4 = (Repeater)e.Item.FindControl("Repeater4");
            Repeater Repeater5 = (Repeater)e.Item.FindControl("Repeater5");
            int QueID = Convert.ToInt32(lblQutionID.Text);
            int Choistype = Convert.ToInt32(lblsubChoyesh.Text);
            Option_List = DB.OptionMasters.Where(p => p.QuestionID == QueID && p.TenentID == TID).ToList();
            if (Option_List.Count() > 0)
            {
                if (Choistype == 3)
                {
                    addf = 1;
                    Repeater1.DataSource = Option_List;
                    Repeater1.DataBind();

                    //((Repeater)repAccordian.Items[i].FindControl("Repeater1")).DataSource = Option_List;
                    //((Repeater)repAccordian.Items[i].FindControl("Repeater1")).DataBind();
                }
                else if (Choistype == 2)
                {
                    addf = 1;
                    Repeater2.DataSource = Option_List;
                    Repeater2.DataBind();

                    ////((Repeater)repAccordian.Items[i].FindControl("Repeater2")).DataSource = Option_List;
                    //((Repeater)repAccordian.Items[i].FindControl("Repeater2")).DataBind();
                }
                else if (Choistype == 1)
                {
                    addf = 1;
                    Repeater4.DataSource = Option_List;
                    Repeater4.DataBind();

                    //((Repeater)repAccordian.Items[i].FindControl("Repeater4")).DataSource = Option_List;
                    //((Repeater)repAccordian.Items[i].FindControl("Repeater4")).DataBind();
                }
            }
            if (Choistype == 4)
            {
                if (Option_List.Count == 0)
                {
                    OptionMaster obj = new OptionMaster();
                    Option_List.Add(obj);
                }
                Repeater3.DataSource = Option_List;
                Repeater3.DataBind();
                //((Repeater)repAccordian.Items[i].FindControl("Repeater3")).DataSource = Option_List;
                //((Repeater)repAccordian.Items[i].FindControl("Repeater3")).DataBind();
            }
            else if (Choistype == 5)
            {
                if (Option_List.Count == 0)
                {
                    OptionMaster obj = new OptionMaster();
                    Option_List.Add(obj);
                }
                Repeater5.DataSource = Option_List;
                Repeater5.DataBind();
                //((Repeater)repAccordian.Items[i].FindControl("Repeater5")).DataSource = Option_List;
                //((Repeater)repAccordian.Items[i].FindControl("Repeater5")).DataBind();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButtonList questionsRadioButtonList = (RadioButtonList)e.Item.FindControl("QuestionsList");
                Label lbl = (Label)e.Item.FindControl("lblQutionID");
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

        protected void listCheckBoxList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBoxList questionsCheckboxButtonList = (CheckBoxList)e.Item.FindControl("chmultipletpreview");
                Label lbl = (Label)e.Item.FindControl("lblQutionID");
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

    }



}