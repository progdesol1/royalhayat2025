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
using System.Web.Mail;
using System.Net;
using System.IO;

namespace Web.CRM
{
    public partial class Campaign_Mst : System.Web.UI.Page
    {
        bool flag = false;
        bool flag1 = false;
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID,MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
      //  CallEntities DB1 = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            // Session["CampaignID"] = null;
            // Session["MySerial"] = null;
            if (flag == false)
            {
                flag = true;
                Session["PageType"] = null;
                //Session["CampaignID"] = null;
            }           
            Session["PageType"] = ActionMaster.Type.Campaign.ToString();
            pnlSuccessMsg.Visible = false;
            // MID = Convert.ToInt32(Session["MyID"]);
            if (!IsPostBack)
            {
                FistTimeLoad();
                // Session["CampaignID"] = null;
                Readonly();
                ManageLang();
               
                //DivMain.Style.Add("display", "block");
                pnlSuccessMsg.Visible = false;

                FillContractorID();
                BindReference();
                Clear();
               
                LastData();
                Session["PageType"] = ActionMaster.Type.Campaign.ToString();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);

                if (Request.QueryString["View"] == "Add")
                {
                    Write();
                    Clear();
                    btnAdd.Text = "Add";
                    pnllist.Visible = false;
                    //paneloppref.Visible = true;
                    DivMain.Attributes["style"] = "display: none;";
                }
                if (btnAdd.Text == "Add")
                {
                    btnAdd.ValidationGroup = "s";
                    DivMain.Attributes["style"] = "display: none;";
                }
                if (flag1 == true)
                {
                    //change
                    pnlSearchbutton.Visible = false;
                }
                BindData();
                if (DB.tbl_Campaign_Mst.Count() > 0)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        int ID = Convert.ToInt32(Request.QueryString["ID"]);
                        Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                        DivMain.Attributes["style"] = "display: block;";
                        //Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                        //DivMain.Attributes["style"] = "display: block;";
                        BindCommand(ID);
                        Write();
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        btnAdd.ValidationGroup = "submit";

                        //btnAdd.ValidationGroup = "s";
                        ////DivMain.Attributes.CssStyle.Add("display", "block");
                        //Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                        //DivMain.Attributes["style"] = "display: none;";
                       
                        //ViewState["Edit"] = ID;
                        //// Session["MySerial"]=
                        //Database.tbl_Campaign_Mst objtbl_Campaign_Mst = DB.tbl_Campaign_Mst.Single(p => p.ID == ID);
                        ////   txtname.SelectedValue = objtbl_Campaign_Mst.ID.ToString();
                        //txtTenet.Text = objtbl_Campaign_Mst.Tenet.ToString();
                        //txtName.Text = objtbl_Campaign_Mst.Name.ToString();
                        //txtName2.Text = objtbl_Campaign_Mst.Name2.ToString();
                        //txtName3.Text = objtbl_Campaign_Mst.Name3.ToString();
                        //txtMyFavorite.Text = objtbl_Campaign_Mst.MyFavorite.ToString();
                        //txtMyItems.Text = objtbl_Campaign_Mst.MyItems.ToString();
                        //txtActualStartDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.ActualStartDate).ToString("MM/dd/yyyy");
                        //txtActualEndDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.ActualEndDate).ToString("MM/dd/yyyy");
                        //txtContentsCampaignLanguage2.Text = objtbl_Campaign_Mst.ContentsLang2;
                        //txtContentsCampaignLanguage3.Text = objtbl_Campaign_Mst.ContentsLang3;
                        //if (objtbl_Campaign_Mst.Status != null)
                        //{
                        //    DrpStatus.SelectedValue = objtbl_Campaign_Mst.Status.ToString();
                        //}

                        //if (objtbl_Campaign_Mst.TypeID != null)
                        //{
                        //    drpTypeID.SelectedValue = objtbl_Campaign_Mst.TypeID.ToString();
                        //}
                        //if (objtbl_Campaign_Mst.AssignedTeamLeader != null)
                        //{
                        //    DrpAssignedTeamLeader.SelectedValue = objtbl_Campaign_Mst.AssignedTeamLeader.ToString();
                        //}
                        //if (objtbl_Campaign_Mst.Compaigntype != null)
                        //{
                        //    drpTypeID.SelectedValue = objtbl_Campaign_Mst.Compaigntype.ToString();
                        //}
                        //if (objtbl_Campaign_Mst.TeamName != null)
                        //{
                        //    drpTeamName.SelectedValue = objtbl_Campaign_Mst.TeamName.ToString();
                        //}
                        //if (objtbl_Campaign_Mst.RefNO != null)
                        //{
                        //    drpRefNo123.SelectedValue = objtbl_Campaign_Mst.RefNO.ToString();
                        //}
                        //txtBudget.Text = objtbl_Campaign_Mst.Budget.ToString();
                        //txtRevenue.Text = objtbl_Campaign_Mst.Revenue.ToString();
                        //txtTrackerText.Text = objtbl_Campaign_Mst.TrackerText.ToString();
                        //txtReferURL.Text = objtbl_Campaign_Mst.ReferURL.ToString();
                        //txtContents.Text = objtbl_Campaign_Mst.Contents.ToString();
                        //txtImpressions.Text = objtbl_Campaign_Mst.Impressions.ToString();
                        //txtActualCost.Text = objtbl_Campaign_Mst.ActualCost.ToString();
                        //txtExpectedCost.Text = objtbl_Campaign_Mst.ExpectedCost.ToString();
                        //txtObjective.Text = objtbl_Campaign_Mst.Objective.ToString();
                        //txtCurrency.Text = objtbl_Campaign_Mst.Currency.ToString();
                        //txtStartDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.StartDate).ToShortDateString();
                        //txtEndDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.EndDate).ToShortDateString();
                        //txtAssignedUser.Text = objtbl_Campaign_Mst.AssignedUser.ToString();
                        //txtCreatedDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.CreatedDate).ToShortDateString();
                        //cbActive.Checked = (objtbl_Campaign_Mst.Active == true) ? true : false;
                        //cbDeleted.Checked = (objtbl_Campaign_Mst.Deleted == true) ? true : false;
                        ////txtCRUP_ID.Text = objtbl_Campaign_Mst.CRUP_ID.ToString();

                        //btnAdd.Text = "Update";
                        //ViewState["Edit"] = ID;
                        //btnAdd.ValidationGroup = "submit";
                        //Write();
                        var List = DB.tbl_Campaign_Mst.Where(P => P.Active == true && P.ID == ID&&P.TenentID==TID).OrderByDescending(p => p.ID).ToList();

                        int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                        int Totalrec = List.Count();
                        ((Web.CRM.CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);

                       
                    }
                    if (Request.QueryString["REFID"] != null)
                    {

                    }

                }
               
            }


        }
        #region Step2
        public void BindData()
        {
            var List = DB.tbl_Campaign_Mst.Where(P => P.Active == true&&P.TenentID==TID).OrderByDescending(p=>p.ID).ToList();

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((Web.CRM.CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);

        }
        #endregion
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
        public void GetShow()
        {
            lblTenet1s.Attributes["class"] = lblName1s.Attributes["class"] = "control-label col-md-2  getshow";
             lblMyFavorite1s.Attributes["class"] = lblMyItems1s.Attributes["class"] = lblCompaigntype1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblTypeID1s.Attributes["class"] = lblBudget1s.Attributes["class"] = lblRevenue1s.Attributes["class"] = lblTrackerText1s.Attributes["class"] = lblReferURL1s.Attributes["class"] = lblContents1s.Attributes["class"] = lblImpressions1s.Attributes["class"] = lblActualCost1s.Attributes["class"] = lblExpectedCost1s.Attributes["class"] = lblObjective1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblStartDate1s.Attributes["class"] = lblEndDate1s.Attributes["class"] = lblTeamName1s.Attributes["class"] = lblAssignedUser1s.Attributes["class"] = lblCreatedDate1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  getshow";
             lblMyFavorite2h.Attributes["class"] = lblMyItems2h.Attributes["class"] = lblCompaigntype2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblTypeID2h.Attributes["class"] = lblBudget2h.Attributes["class"] = lblRevenue2h.Attributes["class"] = lblTrackerText2h.Attributes["class"] = lblReferURL2h.Attributes["class"] = lblContents2h.Attributes["class"] = lblImpressions2h.Attributes["class"] = lblActualCost2h.Attributes["class"] = lblExpectedCost2h.Attributes["class"] = lblObjective2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblStartDate2h.Attributes["class"] = lblEndDate2h.Attributes["class"] = lblTeamName2h.Attributes["class"] = lblAssignedUser2h.Attributes["class"] = lblCreatedDate2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  gethide";
             lblTenet2h.Attributes["class"] = lblName2h.Attributes["class"] = "control-label col-md-2  gethide";
                b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblTenet1s.Attributes["class"] = lblName1s.Attributes["class"] = "control-label col-md-2  gethide";
              lblMyFavorite1s.Attributes["class"] = lblMyItems1s.Attributes["class"] = lblCompaigntype1s.Attributes["class"] = lblStatus1s.Attributes["class"] = lblTypeID1s.Attributes["class"] = lblBudget1s.Attributes["class"] = lblRevenue1s.Attributes["class"] = lblTrackerText1s.Attributes["class"] = lblReferURL1s.Attributes["class"] = lblContents1s.Attributes["class"] = lblImpressions1s.Attributes["class"] = lblActualCost1s.Attributes["class"] = lblExpectedCost1s.Attributes["class"] = lblObjective1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblStartDate1s.Attributes["class"] = lblEndDate1s.Attributes["class"] = lblTeamName1s.Attributes["class"] = lblAssignedUser1s.Attributes["class"] = lblCreatedDate1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblDeleted1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  gethide";
           lblMyFavorite2h.Attributes["class"] = lblMyItems2h.Attributes["class"] = lblCompaigntype2h.Attributes["class"] = lblStatus2h.Attributes["class"] = lblTypeID2h.Attributes["class"] = lblBudget2h.Attributes["class"] = lblRevenue2h.Attributes["class"] = lblTrackerText2h.Attributes["class"] = lblReferURL2h.Attributes["class"] = lblContents2h.Attributes["class"] = lblImpressions2h.Attributes["class"] = lblActualCost2h.Attributes["class"] = lblExpectedCost2h.Attributes["class"] = lblObjective2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblStartDate2h.Attributes["class"] = lblEndDate2h.Attributes["class"] = lblTeamName2h.Attributes["class"] = lblAssignedUser2h.Attributes["class"] = lblCreatedDate2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblDeleted2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  getshow";
           lblTenet2h.Attributes["class"] = lblName2h.Attributes["class"] = "control-label col-md-2  getshow";
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
            //  txtname.SelectedIndex = 0;
            txtTenet.Text = "";
            txtName.Text = "";
            txtMyFavorite.Text = "";
            txtMyItems.Text = "";
            txtName2.Text = "";
            txtName3.Text = "";
            txtContentsCampaignLanguage2.Text = "";
            txtContentsCampaignLanguage3.Text="";
            //txtCompaigntype.Text = "";
            DrpStatus.SelectedIndex = 0;
            drpTypeID.SelectedIndex = 0;
            DrpCampaignType.SelectedIndex = 0;
            // txtBudget.Text = "";
            txtRevenue.Text = "";
            txtTrackerText.Text = "";
            txtReferURL.Text = "";
            txtContents.Text = "";
            txtImpressions.Text = "";
            // txtActualCost.Text = "";
            //  txtExpectedCost.Text = "";
            txtObjective.Text = "";
            txtCurrency.Text = "";
            // txtStartDate.Text = "";
            // txtEndDate.Text = "";
           
            txtActualStartDate.Text = "";
            txtActualEndDate.Text = "";
            drpTeamName.SelectedIndex = 0;
          //  DrpAssignedTeamLeader.SelectedIndex = 0;
            //drpRefNo123.SelectedIndex = 0;
            txtAssignedUser.Text = "";
            txtCreatedDate.Text = "";
            //txtActive.Text = "";
            //txtDeleted.Text = "";
            //txtCRUP_ID.Text = "";
            txtExpectedCost.Text = "0";
            txtCurrency.Text = "0";
            txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtCreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            txtCreatedDate.Text = "";

            txtActualCost.Text = "0";
            txtBudget.Text = "0";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //using (var scope = new System.Transactions.TransactionScope())
            //{
            int Reference = 0;

            if (btnAdd.Text == "AddNew")
            {

                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "s";
                //paneloppref.Visible = true;
            }
            else if (btnAdd.Text == "Add")
            {
                btnAdd.ValidationGroup = "s";
                Database.tbl_Campaign_Mst objtbl_Campaign_Mst = new Database.tbl_Campaign_Mst();
                //Server Content Send data Yogesh
                List<Database.tbl_Campaign_Mst> list = DB.tbl_Campaign_Mst.Where(p=>p.TenentID==TID).ToList();
                if (list.Count() > 0)
                {
                    int Max = list.Select(x => x.ID).Max();
                    objtbl_Campaign_Mst.ID = Max + 1;
                    ViewState["CampaignID"] = Max + 1;
                }
                else
                {
                    objtbl_Campaign_Mst.ID = 1;
                    ViewState["CampaignID"] = 1;
                }
                objtbl_Campaign_Mst.TenentID = TID;
                objtbl_Campaign_Mst.Tenet = txtTenet.Text;
                objtbl_Campaign_Mst.Name = txtName.Text;
                objtbl_Campaign_Mst.MyFavorite = txtMyFavorite.Text;
                objtbl_Campaign_Mst.MyItems = txtMyItems.Text;
                objtbl_Campaign_Mst.Name3 = txtName3.Text;
                objtbl_Campaign_Mst.Name2 = txtName2.Text;
                //objtbl_Campaign_Mst.ActualStartDate = Convert.ToDateTime(txtActualStartDate.Text);
                //objtbl_Campaign_Mst.ActualEndDate = Convert.ToDateTime(txtActualEndDate.Text);
                if (txtActualStartDate.Text != "")
                {
                    objtbl_Campaign_Mst.ActualStartDate = Convert.ToDateTime(txtActualStartDate.Text);
                }
                else
                {
                    objtbl_Campaign_Mst.ActualStartDate = DateTime.Now;
                }
                if (txtActualEndDate.Text != "")
                {
                    objtbl_Campaign_Mst.ActualEndDate = Convert.ToDateTime(txtActualEndDate.Text);
                }
                else
                {
                    objtbl_Campaign_Mst.ActualEndDate = DateTime.Now;
                }
                objtbl_Campaign_Mst.ContentsLang2 = txtContentsCampaignLanguage2.Text;
                objtbl_Campaign_Mst.ContentsLang3 = txtContentsCampaignLanguage3.Text;
                //objtbl_Campaign_Mst.Compaigntype = txtCompaigntype.Text;
                   
                if(DrpCampaignType.SelectedValue !="0")
                {
                    objtbl_Campaign_Mst.Compaigntype = DrpCampaignType.SelectedValue;
                }
                //if (drpTypeID.SelectedItem.Text != "-- Select --")
                //{
                //    objtbl_Campaign_Mst.TypeID = drpTypeID.SelectedValue;
                //}

                if (Convert.ToInt32(DrpStatus.SelectedValue) != 0)
                {
                    objtbl_Campaign_Mst.Status = Convert.ToInt32(DrpStatus.SelectedValue);
                }
                if (Convert.ToInt32(drpTypeID.SelectedValue) != 0)
                {
                    objtbl_Campaign_Mst.TypeID = Convert.ToInt32(drpTypeID.SelectedValue);
                }
                if (Convert.ToInt32(drpTeamName.SelectedValue) != 0)
                {
                    objtbl_Campaign_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                }
                if(DrpAssignedTeamLeader.SelectedValue != "0" )
                {
                    objtbl_Campaign_Mst.AssignedUser = DrpAssignedTeamLeader.SelectedValue;
                }
                //if (Convert.ToInt32(drpRefNo123.SelectedValue) != 0)
                //{
                //    objtbl_Campaign_Mst.RefNO = Convert.ToInt32(drpRefNo123.SelectedValue);
                //    Session["Common"] = Convert.ToInt32(drpRefNo123.SelectedValue);
                //    Reference = Convert.ToInt32(drpRefNo123.SelectedValue);

                //}
                //if (Convert.ToInt32(DrpAssignedTeamLeader.SelectedValue) != 0)
                //{
                //    objtbl_Campaign_Mst.AssignedTeamLeader = Convert.ToInt32(DrpAssignedTeamLeader.SelectedValue);
                //}
                objtbl_Campaign_Mst.Budget = Convert.ToDecimal(txtBudget.Text);
                objtbl_Campaign_Mst.Revenue = txtRevenue.Text;
                objtbl_Campaign_Mst.TrackerText = txtTrackerText.Text;
                objtbl_Campaign_Mst.ReferURL = txtReferURL.Text;
                objtbl_Campaign_Mst.Contents = txtContents.Text;
                objtbl_Campaign_Mst.Impressions = txtImpressions.Text;
                objtbl_Campaign_Mst.ActualCost = Convert.ToDecimal(txtActualCost.Text);
                objtbl_Campaign_Mst.ExpectedCost = Convert.ToDecimal(txtExpectedCost.Text);
                objtbl_Campaign_Mst.Objective = txtObjective.Text;
                objtbl_Campaign_Mst.Currency = txtCurrency.Text;
                objtbl_Campaign_Mst.StartDate = Convert.ToDateTime(txtStartDate.Text);
                objtbl_Campaign_Mst.EndDate = Convert.ToDateTime(txtEndDate.Text);
                //objtbl_Campaign_Mst.AssignedUser = txtAssignedUser.Text;
                objtbl_Campaign_Mst.CreatedDate = DateTime.Now;
                objtbl_Campaign_Mst.Active = cbActive.Checked;
                objtbl_Campaign_Mst.Deleted = cbDeleted.Checked;
                objtbl_Campaign_Mst.CRUP_ID = 1;
                objtbl_Campaign_Mst.Deleted = true;
                int CampID = 0;
                //if (DB.tbl_Campaign_Mst.Count() == 0)
                //{
                if (ViewState["CampaignID"] != null)
                {
                    //string Refrction = drpRefNo123.SelectedValue;
                    string Name = txtName.Text;
                    string Discrption = txtContents.Text;
                    CampID = Convert.ToInt32(ViewState["CampaignID"]);
                    // Haresh  //  CRMMainActivity(TID, Reference, CampID);
            //   Bhavesh  //   InsertCRMMainActivity(CampID, Refrction, Name, Discrption);
                }

                //}
                // Haresh
                // CRMActivity(TID, Reference);
                DB.tbl_Campaign_Mst.AddObject(objtbl_Campaign_Mst);
                DB.SaveChanges();


                String url = "insert new record in tbl_Campaign_Mst with " + "TenentID = " + TID + "ID =" + objtbl_Campaign_Mst.ID;
                String evantname = "create";
                String tablename = "tbl_Campaign_Mst";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                // Haresh
                //if (ViewState["RefID"] != null && ViewState["CampaignID"] != null)
                //{
                //    int Refid = Convert.ToInt32(ViewState["RefID"]);
                //    CampID = Convert.ToInt32(ViewState["CampaignID"]);
                //    Database.REFTABLE obj_Ref = DB1.REFTABLE.SingleOrDefault(p => p.REFID == Refid);
                //    obj_Ref.SWITCH3 = "220003Camp" + CampID;
                //    DB1.SaveChanges();
                //}
              //  Clear();
                lblMsg.Text = "  Data Save Successfully";
                pnlSuccessMsg.Visible = true;
                BindData();
                navigation.Visible = true;
                btnAdd.ValidationGroup = "submit";
                btnAdd.Text = "AddNew";
                Readonly();
                LastData();
            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.tbl_Campaign_Mst objtbl_Campaign_Mst = DB.tbl_Campaign_Mst.Single(p => p.ID == ID&&p.TenentID==TID);
                    //  objtbl_Campaign_Mst.ID = Convert.ToInt32(drpID.Text);
                    objtbl_Campaign_Mst.TenentID = TID;
                    objtbl_Campaign_Mst.Tenet = txtTenet.Text;
                    objtbl_Campaign_Mst.Name = txtName.Text;
                    objtbl_Campaign_Mst.Name3 = txtName3.Text;
                    objtbl_Campaign_Mst.Name2 = txtName2.Text;
                    //objtbl_Campaign_Mst.ActualStartDate = Convert.ToDateTime(txtActualStartDate.Text);
                    //objtbl_Campaign_Mst.ActualEndDate = Convert.ToDateTime(txtActualEndDate.Text);
                    if (txtActualStartDate.Text != "")
                    {
                        objtbl_Campaign_Mst.ActualStartDate = Convert.ToDateTime(txtActualStartDate.Text);
                    }
                    else
                    {
                        objtbl_Campaign_Mst.ActualStartDate = Convert.ToDateTime(txtActualStartDate.Text);
                    }
                    if (txtActualEndDate.Text != "")
                    {
                        objtbl_Campaign_Mst.ActualEndDate = Convert.ToDateTime(txtActualEndDate.Text);
                    }
                    else
                    {
                        objtbl_Campaign_Mst.ActualEndDate = Convert.ToDateTime(txtActualEndDate.Text);
                    }
                    //   Bhavesh  //  //if (Convert.ToInt32(drpRefNo123.SelectedValue) != 0)
                    //{
                    //    objtbl_Campaign_Mst.RefNO = Convert.ToInt32(drpRefNo123.SelectedValue);
                    //}
                    objtbl_Campaign_Mst.ContentsLang2 = txtContentsCampaignLanguage2.Text;
                    objtbl_Campaign_Mst.ContentsLang3 = txtContentsCampaignLanguage3.Text;
                    objtbl_Campaign_Mst.MyFavorite = txtMyFavorite.Text;
                    objtbl_Campaign_Mst.MyItems = txtMyItems.Text;
                    // objtbl_Campaign_Mst.Compaigntype = txtCompaigntype.Text;
                    if (DrpCampaignType.SelectedValue != "0")
                    {
                        objtbl_Campaign_Mst.Compaigntype = DrpCampaignType.SelectedValue;
                    }
                    //if (drpTypeID.SelectedItem.Text != "-- Select --")
                    //{
                    //    objtbl_Campaign_Mst.Compaigntype = drpTypeID.SelectedItem.Text;
                    //}
                    if (Convert.ToInt32(DrpStatus.SelectedValue) != 0)
                    {
                        objtbl_Campaign_Mst.Status = Convert.ToInt32(DrpStatus.SelectedValue);
                    }
                    if (Convert.ToInt32(drpTypeID.SelectedValue) != 0)
                    {
                        objtbl_Campaign_Mst.TypeID = Convert.ToInt32(drpTypeID.SelectedValue);
                    }
                    if (Convert.ToInt32(drpTeamName.SelectedValue) != 0)
                    {
                        objtbl_Campaign_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                    }
                    if (DrpAssignedTeamLeader.SelectedValue != "0")
                    {
                        objtbl_Campaign_Mst.AssignedUser = DrpAssignedTeamLeader.SelectedValue;
                    }
                    //objtbl_Campaign_Mst.TypeID = Convert.ToInt32(drpTypeID.SelectedValue);
                    objtbl_Campaign_Mst.Budget = Convert.ToDecimal(txtBudget.Text);
                    objtbl_Campaign_Mst.Revenue = txtRevenue.Text;
                    objtbl_Campaign_Mst.TrackerText = txtTrackerText.Text;
                    objtbl_Campaign_Mst.ReferURL = txtReferURL.Text;
                    objtbl_Campaign_Mst.Contents = txtContents.Text;
                    objtbl_Campaign_Mst.Impressions = txtImpressions.Text;
                    objtbl_Campaign_Mst.ActualCost = Convert.ToDecimal(txtActualCost.Text);
                    objtbl_Campaign_Mst.ExpectedCost = Convert.ToDecimal(txtExpectedCost.Text);
                    objtbl_Campaign_Mst.Objective = txtObjective.Text;
                    objtbl_Campaign_Mst.Currency = txtCurrency.Text;
                    objtbl_Campaign_Mst.StartDate = Convert.ToDateTime(txtStartDate.Text);
                    objtbl_Campaign_Mst.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    //objtbl_Campaign_Mst.TeamName = Convert.ToInt32(drpTeamName.SelectedValue);
                    //sobjtbl_Campaign_Mst.AssignedUser = txtAssignedUser.Text;
                    objtbl_Campaign_Mst.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
                    objtbl_Campaign_Mst.Active = cbActive.Checked;
                    objtbl_Campaign_Mst.Deleted = cbDeleted.Checked;
                    //objtbl_Campaign_Mst.CRUP_ID = txtCRUP_ID.Text;
                    ViewState["Edit"] = null;
                    btnAdd.Text = "AddNew";
                    DB.SaveChanges();


                    String url = "update tbl_Campaign_Mst with " + "TenentID = " + TID + "ID =" + ID;
                    String evantname = "Update";
                    String tablename = "tbl_Campaign_Mst";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                  //  Clear();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    navigation.Visible = true;
                    Readonly();
                    pnlSearchbutton.Visible = false;
                  //  FirstData();
                    Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                    DivMain.Attributes["style"] = "display: none;";
                }
            }
            BindData();

            //    scope.Complete(); //  To commit.


            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            LastData();
            Readonly();
            
            Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
            DivMain.Attributes["style"] = "display: none;";
            pnlSearchbutton.Visible = false;
            btnAdd.Text = "AddNew";
           // Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpTypeID.DataSource = DB.REFTABLE.Where(P => P.TenentID == TID && P.ACTIVE == "Y" && P.REFTYPE == "CATTYPE" && P.REFSUBTYPE == "CATTYPE");
            //drpTypeID.DataTextField = "REFNAME1";
            //drpTypeID.DataValueField = "REFID";
            //drpTypeID.DataBind();
            //drpTypeID.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpTypeID, TID, "CATTYPE", "CATTYPE", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'CATTYPE' and REFSUBTYPE = 'CATTYPE'
            //Classes.CRMClass.getcrmdropdown(drpTypeID, TID, "CATTYPE", "CATTYPE", "", "REFTABLE");

            //drpTeamName.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS");
            //drpTeamName.DataTextField = "REFNAME1";
            //drpTeamName.DataValueField = "REFID";
            //drpTeamName.DataBind();
            //drpTeamName.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpTeamName, TID, "TeamTitle", "TeamTitle", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'TeamTitle' and REFSUBTYPE = 'TeamTitle'
            //Classes.CRMClass.getcrmdropdown(drpTeamName, TID, "TeamTitle", "TeamTitle", "", "REFTABLE");


            //DrpCampaignType.DataSource = DB.REFTABLE.Where(p =>p.REFTYPE == "CRM" && p.REFSUBTYPE == "Campaign Type");
            //DrpCampaignType.DataTextField = "REFNAME1";
            //DrpCampaignType.DataValueField = "REFID";
            //DrpCampaignType.DataBind();
            //DrpCampaignType.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(DrpCampaignType, TID, "CRM", "Campaign Type", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE = 'CRM' and REFSUBTYPE = 'Campaign Type'
            //Classes.CRMClass.getcrmdropdown(DrpCampaignType, TID, "CRM", "Campaign Type", "", "REFTABLE");

            
            //DrpAssignedTeamLeader.Items.Insert(0, new ListItem("-- Select --", "0"));

            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            var TitleData = (from TitleRef in DB.REFTABLEs
                             join Search in DB.ISSearchDetails on TitleRef.REFID equals Search.REFID
                             where Search.CreatedBy == UID && TitleRef.REFTYPE == RefType && TitleRef.TenentID == TID&&Search.TenentID==TID
                             select new
                             {
                                 ID = TitleRef.REFID,
                                 TitleRef.REFNAME1
                             }).ToList().Distinct();

            DrpSearchTitle.DataSource = TitleData;
            DrpSearchTitle.DataTextField = "REFNAME1";
            DrpSearchTitle.DataValueField = "ID";
            DrpSearchTitle.DataBind();
            DrpSearchTitle.Items.Insert(0, new ListItem("-- Select --", "0"));






            //DrpQuestion.DataSource = DB.CRM_QuestionMaster.Where(p => p.TenentID == TID && p.Avtive == true && p.Deleted == true && p.CreateBy == UID);
            //DrpQuestion.DataTextField = "QuestionLang1";
            //DrpQuestion.DataValueField = "ID";
            //DrpQuestion.DataBind();
            //DrpQuestion.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpQutionGrup, TID, "", "", "", "TBLGROUP");
            //select * from TBLGROUP 

            //DrpStatus.DataSource = DB.CRM_ISCampignStatus.Where(p => p.Active == true && p.Deleted == true);
            //DrpStatus.DataTextField = "Status";
            //DrpStatus.DataValueField = "ID";
            //DrpStatus.DataBind();
            //DrpStatus.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.CRMClass.getcrmdropdown(DrpStatus, TID, "", "", "", "ISCampignStatus");
            //select * from ISCampignStatus where Active=1 and Deleted=1
        }
        public void BindReference()
        {
            //drpRefNo123.DataSource = DB1.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "Campaign" && P.TenentID == TID && P.SWITCH2 == "01").OrderBy(a => a.REFNAME1);
            //drpRefNo123.DataTextField = "REFNAME1";
            //drpRefNo123.DataValueField = "REFID";
            //drpRefNo123.DataBind();
            //drpRefNo123.Items.Insert(0, new ListItem("-- Select --", "0"));

            // Classes.CRMClass.getcrmdropdown(drpRefNo123, TID, "CRM", "Campaign", "01", "REFTABLE");
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
        int LIDID = 0;
        public void FirstData()
        {
            LIDID = DB.tbl_Campaign_Mst.Where(p => p.Active == true&&p.TenentID==TID).FirstOrDefault().ID;

            Listview1.SelectedIndex = LIDID;
            BindCommand(LIDID);
            Readonly();

        }
        public void NextData()
        {
            Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
            LIDID = Convert.ToInt32(Listview1.SelectedIndex);
            Listview1.SelectedIndex = LIDID;
            BindCommand(LIDID);
            Readonly();
           

        }
        public void PrevData()
        {
            Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
            LIDID = Convert.ToInt32(Listview1.SelectedIndex);
            LIDID = Listview1.SelectedIndex;
            BindCommand(LIDID);
            Readonly();

           
        }
        public void LastData()
        {
            if (DB.tbl_Campaign_Mst.Where(p => p.Active == true && p.TenentID == TID).Count()>0)
            {
                LIDID = DB.tbl_Campaign_Mst.Where(p => p.Active == true && p.TenentID == TID).Max(p => p.ID);
                BindCommand(LIDID);
                Listview1.SelectedIndex = LIDID;
                Readonly();
            }
           

           
        }
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblTenet2h.Visible = lblName2h.Visible = lblMyFavorite2h.Visible = lblMyItems2h.Visible = lblCompaigntype2h.Visible = lblStatus2h.Visible = lblTypeID2h.Visible = lblBudget2h.Visible = lblRevenue2h.Visible = lblTrackerText2h.Visible = lblReferURL2h.Visible = lblContents2h.Visible = lblImpressions2h.Visible = lblActualCost2h.Visible = lblExpectedCost2h.Visible = lblObjective2h.Visible = lblCurrency2h.Visible = lblStartDate2h.Visible = lblEndDate2h.Visible = lblTeamName2h.Visible = lblAssignedUser2h.Visible = lblCreatedDate2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = lblCRUP_ID2h.Visible = false;
                    //2true
                    txtTenet2h.Visible = txtName2h.Visible = txtMyFavorite2h.Visible = txtMyItems2h.Visible = txtCompaigntype2h.Visible = txtStatus2h.Visible = txtTypeID2h.Visible = txtBudget2h.Visible = txtRevenue2h.Visible = txtTrackerText2h.Visible = txtReferURL2h.Visible = txtContents2h.Visible = txtImpressions2h.Visible = txtActualCost2h.Visible = txtExpectedCost2h.Visible = txtObjective2h.Visible = txtCurrency2h.Visible = txtStartDate2h.Visible = txtEndDate2h.Visible = txtTeamName2h.Visible = txtAssignedUser2h.Visible = txtCreatedDate2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = txtCRUP_ID2h.Visible = true;

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
                    lblTenet2h.Visible = lblName2h.Visible = lblMyFavorite2h.Visible = lblMyItems2h.Visible = lblCompaigntype2h.Visible = lblStatus2h.Visible = lblTypeID2h.Visible = lblBudget2h.Visible = lblRevenue2h.Visible = lblTrackerText2h.Visible = lblReferURL2h.Visible = lblContents2h.Visible = lblImpressions2h.Visible = lblActualCost2h.Visible = lblExpectedCost2h.Visible = lblObjective2h.Visible = lblCurrency2h.Visible = lblStartDate2h.Visible = lblEndDate2h.Visible = lblTeamName2h.Visible = lblAssignedUser2h.Visible = lblCreatedDate2h.Visible = lblActive2h.Visible = lblDeleted2h.Visible = lblCRUP_ID2h.Visible = true;
                    //2false
                    txtTenet2h.Visible = txtName2h.Visible = txtMyFavorite2h.Visible = txtMyItems2h.Visible = txtCompaigntype2h.Visible = txtStatus2h.Visible = txtTypeID2h.Visible = txtBudget2h.Visible = txtRevenue2h.Visible = txtTrackerText2h.Visible = txtReferURL2h.Visible = txtContents2h.Visible = txtImpressions2h.Visible = txtActualCost2h.Visible = txtExpectedCost2h.Visible = txtObjective2h.Visible = txtCurrency2h.Visible = txtStartDate2h.Visible = txtEndDate2h.Visible = txtTeamName2h.Visible = txtAssignedUser2h.Visible = txtCreatedDate2h.Visible = txtActive2h.Visible = txtDeleted2h.Visible = txtCRUP_ID2h.Visible = false;

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
                    lblTenet1s.Visible = lblName1s.Visible = lblMyFavorite1s.Visible = lblMyItems1s.Visible = lblCompaigntype1s.Visible = lblStatus1s.Visible = lblTypeID1s.Visible = lblBudget1s.Visible = lblRevenue1s.Visible = lblTrackerText1s.Visible = lblReferURL1s.Visible = lblContents1s.Visible = lblImpressions1s.Visible = lblActualCost1s.Visible = lblExpectedCost1s.Visible = lblObjective1s.Visible = lblCurrency1s.Visible = lblStartDate1s.Visible = lblEndDate1s.Visible = lblTeamName1s.Visible = lblAssignedUser1s.Visible = lblCreatedDate1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = lblCRUP_ID1s.Visible = false;
                    //1true
                    txtTenet1s.Visible = txtName1s.Visible = txtMyFavorite1s.Visible = txtMyItems1s.Visible = txtCompaigntype1s.Visible = txtStatus1s.Visible = txtTypeID1s.Visible = txtBudget1s.Visible = txtRevenue1s.Visible = txtTrackerText1s.Visible = txtReferURL1s.Visible = txtContents1s.Visible = txtImpressions1s.Visible = txtActualCost1s.Visible = txtExpectedCost1s.Visible = txtObjective1s.Visible = txtCurrency1s.Visible = txtStartDate1s.Visible = txtEndDate1s.Visible = txtTeamName1s.Visible = txtAssignedUser1s.Visible = txtCreatedDate1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = txtCRUP_ID1s.Visible = true;
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
                    lblTenet1s.Visible = lblName1s.Visible = lblMyFavorite1s.Visible = lblMyItems1s.Visible = lblCompaigntype1s.Visible = lblStatus1s.Visible = lblTypeID1s.Visible = lblBudget1s.Visible = lblRevenue1s.Visible = lblTrackerText1s.Visible = lblReferURL1s.Visible = lblContents1s.Visible = lblImpressions1s.Visible = lblActualCost1s.Visible = lblExpectedCost1s.Visible = lblObjective1s.Visible = lblCurrency1s.Visible = lblStartDate1s.Visible = lblEndDate1s.Visible = lblTeamName1s.Visible = lblAssignedUser1s.Visible = lblCreatedDate1s.Visible = lblActive1s.Visible = lblDeleted1s.Visible = lblCRUP_ID1s.Visible = true;
                    //1false
                    txtTenet1s.Visible = txtName1s.Visible = txtMyFavorite1s.Visible = txtMyItems1s.Visible = txtCompaigntype1s.Visible = txtStatus1s.Visible = txtTypeID1s.Visible = txtBudget1s.Visible = txtRevenue1s.Visible = txtTrackerText1s.Visible = txtReferURL1s.Visible = txtContents1s.Visible = txtImpressions1s.Visible = txtActualCost1s.Visible = txtExpectedCost1s.Visible = txtObjective1s.Visible = txtCurrency1s.Visible = txtStartDate1s.Visible = txtEndDate1s.Visible = txtTeamName1s.Visible = txtAssignedUser1s.Visible = txtCreatedDate1s.Visible = txtActive1s.Visible = txtDeleted1s.Visible = txtCRUP_ID1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((Web.CRM.CRMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Campaign_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblTenet1s.ID == item.LabelID)
                    txtTenet1s.Text = lblTenet1s.Text = lblhTenet.Text = item.LabelName;
                else if (lblName1s.ID == item.LabelID)
                    txtName1s.Text = lblName1s.Text = lblhName.Text = item.LabelName;
                else if (lblMyFavorite1s.ID == item.LabelID)
                    txtMyFavorite1s.Text = lblMyFavorite1s.Text = item.LabelName;
                else if (lblMyItems1s.ID == item.LabelID)
                    txtMyItems1s.Text = lblMyItems1s.Text = lblhMyItems.Text = item.LabelName;
                else if (lblCompaigntype1s.ID == item.LabelID)
                    txtCompaigntype1s.Text = lblCompaigntype1s.Text = lblhCompaigntype.Text = item.LabelName;
                else if (lblStatus1s.ID == item.LabelID)
                    txtStatus1s.Text = lblStatus1s.Text = item.LabelName;
                else if (lblTypeID1s.ID == item.LabelID)
                    txtTypeID1s.Text = lblTypeID1s.Text = lblhTypeID.Text = item.LabelName;
                else if (lblBudget1s.ID == item.LabelID)
                    txtBudget1s.Text = lblBudget1s.Text = lblhBudget.Text = item.LabelName;
                else if (lblRevenue1s.ID == item.LabelID)
                    txtRevenue1s.Text = lblRevenue1s.Text = item.LabelName;
                else if (lblTrackerText1s.ID == item.LabelID)
                    txtTrackerText1s.Text = lblTrackerText1s.Text = item.LabelName;
                else if (lblReferURL1s.ID == item.LabelID)
                    txtReferURL1s.Text = lblReferURL1s.Text = item.LabelName;
                else if (lblContents1s.ID == item.LabelID)
                    txtContents1s.Text = lblContents1s.Text = item.LabelName;
                else if (lblImpressions1s.ID == item.LabelID)
                    txtImpressions1s.Text = lblImpressions1s.Text = item.LabelName;
                else if (lblActualCost1s.ID == item.LabelID)
                    txtActualCost1s.Text = lblActualCost1s.Text = item.LabelName;
                else if (lblExpectedCost1s.ID == item.LabelID)
                    txtExpectedCost1s.Text = lblExpectedCost1s.Text = item.LabelName;
                else if (lblObjective1s.ID == item.LabelID)
                    txtObjective1s.Text = lblObjective1s.Text = item.LabelName;
                else if (lblCurrency1s.ID == item.LabelID)
                    txtCurrency1s.Text = lblCurrency1s.Text = item.LabelName;
                else if (lblStartDate1s.ID == item.LabelID)
                    txtStartDate1s.Text = lblStartDate1s.Text = item.LabelName;
                else if (lblEndDate1s.ID == item.LabelID)
                    txtEndDate1s.Text = lblEndDate1s.Text = item.LabelName;
                else if (lblTeamName1s.ID == item.LabelID)
                    txtTeamName1s.Text = lblTeamName1s.Text = item.LabelName;
                else if (lblAssignedUser1s.ID == item.LabelID)
                    txtAssignedUser1s.Text = lblAssignedUser1s.Text = item.LabelName;
                else if (lblCreatedDate1s.ID == item.LabelID)
                    txtCreatedDate1s.Text = lblCreatedDate1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblDeleted1s.ID == item.LabelID)
                    txtDeleted1s.Text = lblDeleted1s.Text = item.LabelName;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;


                else if (lblTenet2h.ID == item.LabelID)
                    txtTenet2h.Text = lblTenet2h.Text = lblhTenet.Text = item.LabelName;
                else if (lblName2h.ID == item.LabelID)
                    txtName2h.Text = lblName2h.Text = lblhName.Text = item.LabelName;
                else if (lblMyFavorite2h.ID == item.LabelID)
                    txtMyFavorite2h.Text = lblMyFavorite2h.Text = item.LabelName;
                else if (lblMyItems2h.ID == item.LabelID)
                    txtMyItems2h.Text = lblMyItems2h.Text = lblhMyItems.Text = item.LabelName;
                else if (lblCompaigntype2h.ID == item.LabelID)
                    txtCompaigntype2h.Text = lblCompaigntype2h.Text = lblhCompaigntype.Text = item.LabelName;
                else if (lblStatus2h.ID == item.LabelID)
                    txtStatus2h.Text = lblStatus2h.Text = item.LabelName;
                else if (lblTypeID2h.ID == item.LabelID)
                    txtTypeID2h.Text = lblTypeID2h.Text = lblhTypeID.Text = item.LabelName;
                else if (lblBudget2h.ID == item.LabelID)
                    txtBudget2h.Text = lblBudget2h.Text = lblhBudget.Text = item.LabelName;
                else if (lblRevenue2h.ID == item.LabelID)
                    txtRevenue2h.Text = lblRevenue2h.Text = item.LabelName;
                else if (lblTrackerText2h.ID == item.LabelID)
                    txtTrackerText2h.Text = lblTrackerText2h.Text = item.LabelName;
                else if (lblReferURL2h.ID == item.LabelID)
                    txtReferURL2h.Text = lblReferURL2h.Text = item.LabelName;
                else if (lblContents2h.ID == item.LabelID)
                    txtContents2h.Text = lblContents2h.Text = item.LabelName;
                else if (lblImpressions2h.ID == item.LabelID)
                    txtImpressions2h.Text = lblImpressions2h.Text = item.LabelName;
                else if (lblActualCost2h.ID == item.LabelID)
                    txtActualCost2h.Text = lblActualCost2h.Text = item.LabelName;
                else if (lblExpectedCost2h.ID == item.LabelID)
                    txtExpectedCost2h.Text = lblExpectedCost2h.Text = item.LabelName;
                else if (lblObjective2h.ID == item.LabelID)
                    txtObjective2h.Text = lblObjective2h.Text = item.LabelName;
                else if (lblCurrency2h.ID == item.LabelID)
                    txtCurrency2h.Text = lblCurrency2h.Text = item.LabelName;
                else if (lblStartDate2h.ID == item.LabelID)
                    txtStartDate2h.Text = lblStartDate2h.Text = item.LabelName;
                else if (lblEndDate2h.ID == item.LabelID)
                    txtEndDate2h.Text = lblEndDate2h.Text = item.LabelName;
                else if (lblTeamName2h.ID == item.LabelID)
                    txtTeamName2h.Text = lblTeamName2h.Text = item.LabelName;
                else if (lblAssignedUser2h.ID == item.LabelID)
                    txtAssignedUser2h.Text = lblAssignedUser2h.Text = item.LabelName;
                else if (lblCreatedDate2h.ID == item.LabelID)
                    txtCreatedDate2h.Text = lblCreatedDate2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblDeleted2h.ID == item.LabelID)
                    txtDeleted2h.Text = lblDeleted2h.Text = item.LabelName;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((Web.CRM.CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Campaign_Mst").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\CRM_tbl_Campaign_Mst.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((Web.CRM.CRMMaster)this.Master).Bindxml("CRM_tbl_Campaign_Mst").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName1s.Text;
                else if (lblMyFavorite1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyFavorite1s.Text;
                else if (lblMyItems1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyItems1s.Text;
                else if (lblCompaigntype1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCompaigntype1s.Text;
                else if (lblStatus1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus1s.Text;
                else if (lblTypeID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTypeID1s.Text;
                else if (lblBudget1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBudget1s.Text;
                else if (lblRevenue1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRevenue1s.Text;
                else if (lblTrackerText1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTrackerText1s.Text;
                else if (lblReferURL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReferURL1s.Text;
                else if (lblContents1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContents1s.Text;
                else if (lblImpressions1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtImpressions1s.Text;
                else if (lblActualCost1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActualCost1s.Text;
                else if (lblExpectedCost1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtExpectedCost1s.Text;
                else if (lblObjective1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtObjective1s.Text;
                else if (lblCurrency1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCurrency1s.Text;
                else if (lblStartDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStartDate1s.Text;
                else if (lblEndDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEndDate1s.Text;
                else if (lblTeamName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTeamName1s.Text;
                else if (lblAssignedUser1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssignedUser1s.Text;
                else if (lblCreatedDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCreatedDate1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblDeleted1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;


                else if (lblTenet2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTenet2h.Text;
                else if (lblName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtName2h.Text;
                else if (lblMyFavorite2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyFavorite2h.Text;
                else if (lblMyItems2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyItems2h.Text;
                else if (lblCompaigntype2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCompaigntype2h.Text;
                else if (lblStatus2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus2h.Text;
                else if (lblTypeID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTypeID2h.Text;
                else if (lblBudget2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBudget2h.Text;
                else if (lblRevenue2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRevenue2h.Text;
                else if (lblTrackerText2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTrackerText2h.Text;
                else if (lblReferURL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReferURL2h.Text;
                else if (lblContents2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContents2h.Text;
                else if (lblImpressions2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtImpressions2h.Text;
                else if (lblActualCost2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActualCost2h.Text;
                else if (lblExpectedCost2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtExpectedCost2h.Text;
                else if (lblObjective2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtObjective2h.Text;
                else if (lblCurrency2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCurrency2h.Text;
                else if (lblStartDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStartDate2h.Text;
                else if (lblEndDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEndDate2h.Text;
                else if (lblTeamName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTeamName2h.Text;
                else if (lblAssignedUser2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAssignedUser2h.Text;
                else if (lblCreatedDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCreatedDate2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblDeleted2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\CRM_tbl_Campaign_Mst.xml"));

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
            
            navigation.Visible = false;
          //  drpRefNo123.Visible = true;
            //    txtname.Enabled = true;
            txtActualStartDate.Enabled = true;
            txtActualEndDate.Enabled = true;
            txtTenet.Enabled = true;
            txtName.Enabled = true;
            txtName2.Enabled = true;
            txtName3.Enabled = true;
            txtMyFavorite.Enabled = true;
            txtMyItems.Enabled = true;
            //txtCompaigntype.Enabled = true;
            DrpCampaignType.Enabled = true;
            DrpStatus.Enabled = true;
            drpTypeID.Enabled = true;
            txtBudget.Enabled = true;
            txtRevenue.Enabled = true;
            txtTrackerText.Enabled = true;
            txtReferURL.Enabled = true;
            txtContents.Enabled = true;
            txtContentsCampaignLanguage2.Enabled = true;
            txtContentsCampaignLanguage3.Enabled = true;
            txtImpressions.Enabled = true;
            txtActualCost.Enabled = true;
            txtExpectedCost.Enabled = true;
            txtObjective.Enabled = true;
            txtCurrency.Enabled = true;
            txtStartDate.Enabled = true;
            txtEndDate.Enabled = true;
            drpTeamName.Enabled = true;
            drpQutionGrup.Enabled = true;
            DrpSearchTitle.Enabled = true;
            txtAssignedUser.Enabled = true;
            txtCreatedDate.Enabled = true;
            cbActive.Enabled = true;
            cbDeleted.Enabled = true;
            DrpAssignedTeamLeader.Enabled = true;
            //txtCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
          //  drpRefNo123.Visible = false;
            //    txtname.Enabled = false;
            txtActualStartDate.Enabled = false;
            txtActualEndDate.Enabled = false;
            txtTenet.Enabled = false;
            txtName.Enabled = false;
            txtName2.Enabled = false;
            txtName3.Enabled = false;
            txtMyFavorite.Enabled = false;
            txtMyItems.Enabled = false;
            //txtCompaigntype.Enabled = false;
            DrpCampaignType.Enabled = false;
            DrpStatus.Enabled = false;
            drpTypeID.Enabled = false;
            txtBudget.Enabled = false;
            txtRevenue.Enabled = false;
            txtTrackerText.Enabled = false;
            txtReferURL.Enabled = false;
            txtContents.Enabled = false;
            txtContentsCampaignLanguage2.Enabled = false;
            txtContentsCampaignLanguage3.Enabled = false;
            drpQutionGrup.Enabled = false;
            DrpSearchTitle.Enabled = false;
            txtImpressions.Enabled = false;
            txtActualCost.Enabled = false;
            txtExpectedCost.Enabled = false;
            txtObjective.Enabled = false;
            txtCurrency.Enabled = false;
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
            drpTeamName.Enabled = false;
            txtAssignedUser.Enabled = false;
            txtCreatedDate.Enabled = false;
            cbActive.Enabled = false;
            cbDeleted.Enabled = false;
            DrpAssignedTeamLeader.Enabled = false;
            //txtCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Campaign_Mst.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Campaign_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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

            ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbl_Campaign_Mst.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Campaign_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
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
                ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbl_Campaign_Mst.Count();
                take = Showdata;
                Skip = 0;
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Campaign_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB.tbl_Campaign_Mst.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Campaign_Mst.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();

                        Database.tbl_Campaign_Mst objSOJobDesc = DB.tbl_Campaign_Mst.Single(p => p.ID == ID&&p.TenentID==TID);
                        objSOJobDesc.Active = false;
                        DB.SaveChanges();


                        String url = "Delete tbl_Campaign_Mst with " + "TenentID = " + TID + "ID =" + ID;
                        String evantname = "Delete";
                        String tablename = "tbl_Campaign_Mst";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Campaign_Mst.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }
                    if (e.CommandName == "btnView")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        BindCommand(ID);
                        Readonly();
                        Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-8", "col-md-12");
                        DivMain.Attributes["style"] = "display: none;";
                        pnlSearchbutton.Visible = false;
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        pnlSearchbutton.Visible = true;
                        Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                        DivMain.Attributes["style"] = "display: block;";
                        //Divmainsub.Attributes["class"] = Divmainsub.Attributes["class"].Replace("col-md-12", "col-md-8");
                        //DivMain.Attributes["style"] = "display: block;";
                        BindCommand(ID);
                        Write();
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        btnAdd.ValidationGroup = "submit";
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
        public void BindCommand(int ID)
        {
            flag1 = true;
          //  paneloppref.Visible = false;
            //pnlSearchbutton.Visible = true;
         //   pnlresponsive.Visible = false;
           
            btnAdd.ValidationGroup = "s";
            //DivMain.Attributes.CssStyle.Add("display", "block");
           
            //  int ID = Convert.ToInt32(Request.QueryString["ID"]);

            // Session["MySerial"]=

            Database.tbl_Campaign_Mst objtbl_Campaign_Mst = DB.tbl_Campaign_Mst.Single(p => p.ID == ID&&p.TenentID==TID);
            //   txtname.SelectedValue = objtbl_Campaign_Mst.ID.ToString();
            txtTenet.Text = objtbl_Campaign_Mst.Tenet.ToString();
            txtName.Text = objtbl_Campaign_Mst.Name.ToString();
            txtName2.Text = objtbl_Campaign_Mst.Name2.ToString();
            txtName3.Text = objtbl_Campaign_Mst.Name3.ToString();
            txtMyFavorite.Text = objtbl_Campaign_Mst.MyFavorite.ToString();
            txtMyItems.Text = objtbl_Campaign_Mst.MyItems.ToString();
            txtActualStartDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.ActualStartDate).ToString("MM/dd/yyyy");
            txtActualEndDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.ActualEndDate).ToString("MM/dd/yyyy");
            txtContentsCampaignLanguage2.Text = objtbl_Campaign_Mst.ContentsLang2;
            txtContentsCampaignLanguage3.Text = objtbl_Campaign_Mst.ContentsLang3;
            // txtCompaigntype.Text = objtbl_Campaign_Mst.Compaigntype.ToString();
            if (objtbl_Campaign_Mst.Compaigntype != null)
            {
                DrpCampaignType.SelectedValue = objtbl_Campaign_Mst.Compaigntype.ToString();
            }
            if (objtbl_Campaign_Mst.Status != null)
            {
                DrpStatus.SelectedValue = objtbl_Campaign_Mst.Status.ToString();
            }

            if (objtbl_Campaign_Mst.TypeID != null)
            {
                drpTypeID.SelectedValue = objtbl_Campaign_Mst.TypeID.ToString();
            }
            //if (objtbl_Campaign_Mst.RefNO != null)
            //{
            //    drpRefNo123.SelectedValue = objtbl_Campaign_Mst.RefNO.ToString();
            //}
            
            //if (objtbl_Campaign_Mst.Compaigntype != null)
            //{
            //    drpTypeID.SelectedValue = objtbl_Campaign_Mst.Compaigntype.ToString();
            //}
            if (objtbl_Campaign_Mst.TeamName != null)
            {
                int RID = Convert.ToInt32(objtbl_Campaign_Mst.TeamName);
                drpTeamName.SelectedValue = objtbl_Campaign_Mst.TeamName.ToString();
                BindTimeAssin(RID);
            }
            if (objtbl_Campaign_Mst.AssignedUser != null)
            {

                DrpAssignedTeamLeader.SelectedValue = objtbl_Campaign_Mst.AssignedUser.ToString();

            }
            txtBudget.Text = objtbl_Campaign_Mst.Budget.ToString();
            txtRevenue.Text = objtbl_Campaign_Mst.Revenue.ToString();
            txtTrackerText.Text = objtbl_Campaign_Mst.TrackerText.ToString();
            txtReferURL.Text = objtbl_Campaign_Mst.ReferURL.ToString();
            txtContents.Text = objtbl_Campaign_Mst.Contents.ToString();
            txtImpressions.Text = objtbl_Campaign_Mst.Impressions.ToString();
            txtActualCost.Text = objtbl_Campaign_Mst.ActualCost.ToString();
            txtExpectedCost.Text = objtbl_Campaign_Mst.ExpectedCost.ToString();
            txtObjective.Text = objtbl_Campaign_Mst.Objective.ToString();
            txtCurrency.Text = objtbl_Campaign_Mst.Currency.ToString();
            txtStartDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.StartDate).ToString("MM/dd/yyyy");
            txtEndDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.EndDate).ToString("MM/dd/yyyy");
            //txtAssignedUser.Text = objtbl_Campaign_Mst.AssignedUser.ToString();
            txtCreatedDate.Text = Convert.ToDateTime(objtbl_Campaign_Mst.CreatedDate).ToString("MM/dd/yyyy");
            cbActive.Checked = (objtbl_Campaign_Mst.Active == true) ? true : false;
            cbDeleted.Checked = (objtbl_Campaign_Mst.Deleted == true) ? true : false;
            //txtCRUP_ID.Text = objtbl_Campaign_Mst.CRUP_ID.ToString();
           
           
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Campaign_Mst.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((Web.CRM.CRMMaster)Page.Master).BindList(Listview1, (DB.tbl_Campaign_Mst.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((Web.CRM.CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            // BindData();
        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpSearchTitle.SelectedValue) != 0)
            {
                int GrupID = Convert.ToInt32(drpQutionGrup.SelectedValue);
                int TitleID = Convert.ToInt32(DrpSearchTitle.SelectedValue);
                int CamID = Convert.ToInt32(Session["CampaignID"]);
                Session["OppInset"] = 123;
                Response.Redirect("SearchTitalPage.aspx?TID=" + TitleID + "&CampID=" + CamID + "&LeadName=" + txtName.Text + "&GrupID=" + GrupID);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Select Title ...');", true);
            }
        }

       

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
        public void FillDatadocroute(int Reference, List<tbl_DMSroute> List, int Campid)//loop rout
        {
            string strStatus = ViewState["MyStatus"].ToString();
            int REFIDFromNewDOC = Convert.ToInt32(Reference);
            string strRouteID = Reference.ToString();
            //foreach (tbl_DMSroute item in List.ToList())
            //{
            //    Database.tbl_DMSdocroute objtbl_DMSdocroute = new Database.tbl_DMSdocroute();
            //    objtbl_DMSdocroute.TenentID = TID;
            //    objtbl_DMSdocroute.DocID = Reference;
            //    objtbl_DMSdocroute.ReferenceNo = Convert.ToInt32(CommonList[4]);
            //    objtbl_DMSdocroute.RouteID = strRouteID;//frpm new Document Page
            //    objtbl_DMSdocroute.REFID = Reference;
            //    objtbl_DMSdocroute.REFTYPE = item.REFTYPE;
            //    objtbl_DMSdocroute.REFSUBTYPE = item.REFSUBTYPE;
            //    objtbl_DMSdocroute.LineNum = item.LineNum;
            //    objtbl_DMSdocroute.SeqNo = item.SeqNo;
            //    objtbl_DMSdocroute.fieldID = item.FieldID;
            //    objtbl_DMSdocroute.ACTIVITYCODE = (item.ACTIVITYCODE).ToString();
            //    objtbl_DMSdocroute.CompID = item.CompID;
            //    objtbl_DMSdocroute.Days2Complete = item.Days2Complete;
            //    objtbl_DMSdocroute.NextSeqNo = item.NextSeqNo;
            //    objtbl_DMSdocroute.MyStatus = strStatus;
            //    objtbl_DMSdocroute.Active = true;
            //    objtbl_DMSdocroute.CrupId = ((DMSMaster)Page.Master).WriteLog("From New Document for DMSdocroute,ID:" + objtbl_DMSdocroute.RouteID.ToString(), "From New Document for DMSdocroute,ID:" + objtbl_DMSdocroute.RouteID.ToString(), "tbl_DMSdocroute", "");
            //    DB.tbl_DMSdocroute.AddObject(objtbl_DMSdocroute);
            //    DB.SaveChanges();
            //}


        }


        public void InsertCRMMainActivity(int TransNo, string ADDACTIvity, string CampinName, string Discrption)
        {
            string strStatus = ViewState["MyStatus"].ToString();
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
            int TenentID = TID;
            int LocationID = LID;
            int USERCODE = UID;
            int COMPID = 99999999;
            int LinkMasterCODE = 1;
            int ID = TransNo;
            int RouteID = 1;
            string ACTIVITYE = "Campaign";
            string ACTIVITYA = "Campaign";
            string ACTIVITYA2 = "CRM";
            bool AMIGLOBAL = true;
            bool MYPERSONNEL = true;
            int INTERVALDAYS = 3;
            bool REPEATFOREVER = true;
            DateTime REPEATTILL = DateTime.Now;
            string REMINDERNOTE = "Youer Tarction is Pading";
            int ESTCOST = 1;
            int GROUPCODE = 1;
            string USERCODEENTERED = "New";
            DateTime UPDTTIME = DateTime.Now;
            DateTime UploadDate = DateTime.Now;
            string USERNAME = UNAME;
            string Remarks = "Intert MainActivity Data";
            int CRUP_ID = 1;
            string Version1 = UNAME;
            int Type = 1;
            string MyStatus = strStatus;
            int MainID = 1;
            int ModuleID = 1;
            string RecodType = ADDACTIvity;
            string URL = Request.Url.AbsolutePath;
            string CampynName = CampinName;
            string CampynDescription = Discrption;
            int REFNUMber = Classes.ACMClass.InsertACM_CRMMainActivities(TenentID, COMPID, LinkMasterCODE, LocationID, ID, RouteID, USERCODE, ACTIVITYE, ACTIVITYA, ACTIVITYA2, AMIGLOBAL, MYPERSONNEL, INTERVALDAYS, REPEATFOREVER, REPEATTILL, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, Remarks, CRUP_ID, Version1, Type, MyStatus, MainID, ModuleID, URL, CampynName, CampynDescription);
            //Eco_ICTR_HD obj = DB.Eco_ICTR_HD.Single(p => p.MYTRANSID == TransNo);
            //obj.CrmActivityRefNo = REFNUMber;
            int SID = 1240103;
            string BName = "Create Opportunity";
            string P2 = "aa";
            string P3 = "aa";
            inserCrmproghw(SID, BName, P2, P3, TransNo);

        }

        public void inserCrmproghw(int SID, string BName, string P2, string P3, int TRction)
        {
            // ACM_CRMMainActivities obj = DB1.ACM_CRMMainActivities.Single(p => p.TenentID == TID  && p.ID == TRction);
            int ACID = Convert.ToInt32(DB.CRMActivities.Where(p=>p.TenentID==TID).Max(p => p.MyLineNo));
            int TenentID = TID;
            int ActivityID = ACID;
            int StatusID = SID;
            string ButtionName = BName;
            bool Allowed = true;
            string Parameter2 = P2;
            string Parameter3 = P3;
            bool Active = true;
            DateTime Datetime = DateTime.Now;
            int Crup_Id = 999999999;
            Classes.ACMClass.InsertDataCRMProgHW(TenentID, ActivityID, StatusID, ButtionName, Allowed, Parameter2, Parameter3, Active, Datetime, Crup_Id);
        }
        public void CRMMainActivity(int Reference, int CAMPID)
        {
            string strStatus = ViewState["MyStatus"].ToString();
            int REFIDFromNewDOC1 = REFIDFromNewDOC1 = Convert.ToInt32(Reference);
            //  string strRouteID = CommonList[5].ToString();
            string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
            int UID1 = (((USER_MST)Session["USER"]).USER_ID);

            Database.CRMMainActivity objtbl_CRMMainActivity = new Database.CRMMainActivity();

            objtbl_CRMMainActivity.TenentID = TID;
            objtbl_CRMMainActivity.COMPID = Convert.ToInt32(1);
            objtbl_CRMMainActivity.MasterCODE = DB.CRMMainActivities.Where(p=>p.TenentID==TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p=>p.TenentID==TID).Max(p => p.MasterCODE) + 1) : 1;
            objtbl_CRMMainActivity.USERCODE = UID1;//current user ID Done
            objtbl_CRMMainActivity.MyID = CAMPID;
            objtbl_CRMMainActivity.Prefix = "ONL";
            objtbl_CRMMainActivity.ACTIVITYE = "";
            objtbl_CRMMainActivity.ACTIVITYA = "";
            objtbl_CRMMainActivity.ACTIVITYA2 = "";
            objtbl_CRMMainActivity.Reference = Reference.ToString();
            objtbl_CRMMainActivity.AMIGLOBAL = false;
            objtbl_CRMMainActivity.MYPERSONNEL = false;
            objtbl_CRMMainActivity.INTERVALDAYS = 1;
            objtbl_CRMMainActivity.REPEATFOREVER = false;
            objtbl_CRMMainActivity.REPEATTILL = DateTime.Now;
            objtbl_CRMMainActivity.REMINDERNOTE = "";
            objtbl_CRMMainActivity.ESTCOST = 1;
            objtbl_CRMMainActivity.GROUPCODE = 1;//come from ACM
            objtbl_CRMMainActivity.USERCODEENTERED = "";
            objtbl_CRMMainActivity.UPDTTIME = DateTime.Now;
            objtbl_CRMMainActivity.USERNAME = UID;//current user name Done
            objtbl_CRMMainActivity.Remarks = "";
            objtbl_CRMMainActivity.MainID = CAMPID;
            //objtbl_CRMMainActivity.CRUP_ID = ((DMSMaster)Page.Master).WriteLog("From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), "From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), "CRMMainActivity", UID);
            objtbl_CRMMainActivity.MyStatus = strStatus;
            DB.CRMMainActivities.AddObject(objtbl_CRMMainActivity);
            // ((DMSMaster)Page.Master).UpdateLog("From New Document for CRMMainActivity,ID:" + objtbl_CRMMainActivity.RouteID.ToString(), objtbl_CRMMainActivity.CRUP_ID,"CRMMainActivity", UID);
            DB.SaveChanges();

            String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =" + objtbl_CRMMainActivity.COMPID + "MASTERCODE = " + objtbl_CRMMainActivity.MasterCODE + "LinkMasterCODE = 1" + "LocationID = 1" + "MyID = " + CAMPID + "Prefix = ONL ";
            String evantname = "create";
            String tablename = "CRMMainActivity";
            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

        }
        public void CRMActivity(int Reference)//loop rout
        {
            string strStatus = "";
            if (ViewState["MyStatus"] != null)
            {
                strStatus = ViewState["MyStatus"].ToString();
            }

            int REFIDFromNewDOC = 0;
            Database.CRMActivity objtbl_CRMActivity = new Database.CRMActivity();
            string Type = Convert.ToString(ActionMaster.Type.Campaign);
            string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
            int refID = Convert.ToInt32(Reference);
            objtbl_CRMActivity.TenentID = TID;
            objtbl_CRMActivity.Prefix = "ONL";
            objtbl_CRMActivity.COMPID = 0;
            objtbl_CRMActivity.REFTYPE = Convert.ToString(ActionMaster.Type.Campaign);
            objtbl_CRMActivity.REFSUBTYPE = Convert.ToString(ActionMaster.Type.Campaign);
            objtbl_CRMActivity.RouteID = "";
            // objtbl_CRMActivity.MyLineNo = item.LineNum;
            objtbl_CRMActivity.NextUser = "1";
            objtbl_CRMActivity.AMIGLOBAL = "Y";
            objtbl_CRMActivity.MYPERSONNEL = "N";
            objtbl_CRMActivity.ActivityPerform = "";
            objtbl_CRMActivity.REMINDERNOTE = " ";

            //int a = item.REFID;
            //int b = item.LineNum;
            //int newNumber = int.Parse(a.ToString() + b.ToString());
            //objtbl_CRMActivity.ESTCOST = newNumber;//Seq # Done
            //  objtbl_CRMActivity.ESTCOST = item.SeqNo;
            objtbl_CRMActivity.GROUPCODE = "1";
            objtbl_CRMActivity.USERCODEENTERED = UID.ToString();
            objtbl_CRMActivity.InitialDate = DateTime.Now;
            objtbl_CRMActivity.UPDTTIME = DateTime.Now;
            objtbl_CRMActivity.USERNAME = UID.ToString();

            objtbl_CRMActivity.MyStatus = strStatus;
            DateTime CDate = DateTime.Now;
            // int NoOFDay = item.Days2Complete;
            //   CDate = CDate.AddDays(NoOFDay);
            objtbl_CRMActivity.DeadLineDate = CDate;
            objtbl_CRMActivity.ACTIVITYTYPE = "testing1";
            objtbl_CRMActivity.PerfReferenceNo = "testing2";
            objtbl_CRMActivity.NextRefNo = "testing3";
            //objtbl_CRMActivity.Active = item.ActiveCRM;

            //objtbl_CRMActivity.Active = "F";

            //  objtbl_CRMActivity.GroupBy = item.REFID.ToString() + RouteName + item.SeqNo + refID;
            objtbl_CRMActivity.Type = 1;
            // objtbl_CRMActivity.FromColumn = Convert.ToInt32(item.FromColumn);
            // objtbl_CRMActivity.ToColumn = Convert.ToInt32(item.ToColumn);
            DB.CRMActivities.AddObject(objtbl_CRMActivity);
            DB.SaveChanges();

            String url1 = "insert new record in CRMActivity with " + "TenentID = " + TID + "COMPID =" + 0 + "MASTERCODE = " + objtbl_CRMActivity.MasterCODE + "MyLineNo = 2" + "LocationID = 1" + "LinkMasterCODE = 1 " + "ActivityID = 1" + "Prefix = ONL ";
            String evantname2 = "create";
            String tablename3 = "CRMActivity";
            string loginUserId4 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

            Classes.GlobleClass.EncryptionHelpers.WriteLog(url1, evantname2, tablename3, loginUserId4.ToString(), 0,0);

            //foreach (DMSRouteMST item in List123.ToList())
            //{
            //    Database.tbl_ISSREC_Mst objtbl_ISSREC_Mst = new Database.tbl_ISSREC_Mst();

            //    objtbl_ISSREC_Mst.TENANT_ID = TID;
            //    objtbl_ISSREC_Mst.COMPID = item.CompIDSEqNo;
            //    objtbl_ISSREC_Mst.DOCID = DocID;
            //    objtbl_ISSREC_Mst.USER_ID = item.UserIDSubmitBy;
            //    objtbl_ISSREC_Mst.REFERENCENO = refID;
            //    objtbl_ISSREC_Mst.SEQ = item.SeqNo;
            //    objtbl_ISSREC_Mst.DMS_USER_ID = Convert.ToInt32(item.UserIDSignBySubmit);
            //    objtbl_ISSREC_Mst.DATE = DateTime.Now;
            //    objtbl_ISSREC_Mst.DOCNO = item.REFID;
            //    objtbl_ISSREC_Mst.ACTIVE = false;
            //    objtbl_ISSREC_Mst.FromColumn = Convert.ToInt32(item.FromColumn);
            //    objtbl_ISSREC_Mst.ToColumn = Convert.ToInt32(item.ToColumn);
            //    if (item.SeqNo == 1)
            //    {
            //        objtbl_ISSREC_Mst.ActiveCRM = "Y";
            //    }

            //    objtbl_ISSREC_Mst.TYPE = "SubmitttedBy";
            //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_Mst);
            //    //Recevedby entry
            //    Database.tbl_ISSREC_Mst objtbl_ISSREC_Mst1 = new Database.tbl_ISSREC_Mst();
            //    objtbl_ISSREC_Mst1.TENANT_ID = TID;
            //    objtbl_ISSREC_Mst1.COMPID = Convert.ToInt32(item.CompIDRecBy);
            //    objtbl_ISSREC_Mst1.DOCID = DocID;
            //    objtbl_ISSREC_Mst1.USER_ID = Convert.ToInt32(item.UserIDReceiveBy);
            //    objtbl_ISSREC_Mst1.DMS_USER_ID = Convert.ToInt32(item.UserIDSignByReceive);
            //    objtbl_ISSREC_Mst1.REFERENCENO = refID;
            //    objtbl_ISSREC_Mst1.SEQ = item.SeqNo;
            //    objtbl_ISSREC_Mst1.DATE = DateTime.Now;
            //    objtbl_ISSREC_Mst1.ACTIVE = false;
            //    objtbl_ISSREC_Mst1.DOCNO = item.REFID;
            //    objtbl_ISSREC_Mst1.FromColumn = Convert.ToInt32(item.FromColumn);
            //    objtbl_ISSREC_Mst1.ToColumn = Convert.ToInt32(item.ToColumn);
            //    if (item.SeqNo == 1)
            //    {
            //        objtbl_ISSREC_Mst1.ActiveCRM = "Y";
            //    }
            //    objtbl_ISSREC_Mst1.TYPE = "ReceivedBy";
            //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_Mst1);

            //    //submit sign
            //    Database.tbl_ISSREC_Mst objtbl_ISSREC_MstSubmitSign = new Database.tbl_ISSREC_Mst();
            //    objtbl_ISSREC_MstSubmitSign.TENANT_ID = TID;
            //    objtbl_ISSREC_MstSubmitSign.COMPID = Convert.ToInt32(item.CompIDSEqNo);
            //    objtbl_ISSREC_MstSubmitSign.DOCID = DocID;
            //    objtbl_ISSREC_MstSubmitSign.USER_ID = Convert.ToInt32(item.UserIDSignBySubmit);
            //    objtbl_ISSREC_MstSubmitSign.DMS_USER_ID = Convert.ToInt32(item.UserIDSignBySubmit);
            //    objtbl_ISSREC_MstSubmitSign.REFERENCENO = refID;
            //    objtbl_ISSREC_MstSubmitSign.SEQ = item.SeqNo;
            //    objtbl_ISSREC_MstSubmitSign.DATE = DateTime.Now;
            //    objtbl_ISSREC_MstSubmitSign.ACTIVE = false;
            //    objtbl_ISSREC_MstSubmitSign.DOCNO = item.REFID;
            //    objtbl_ISSREC_MstSubmitSign.FromColumn = Convert.ToInt32(item.FromColumn);
            //    objtbl_ISSREC_MstSubmitSign.ToColumn = Convert.ToInt32(item.ToColumn);
            //    if (item.SeqNo == 1)
            //    {
            //        objtbl_ISSREC_MstSubmitSign.ActiveCRM = "Y";
            //    }
            //    objtbl_ISSREC_MstSubmitSign.TYPE = "SubmitSign";
            //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_MstSubmitSign);


            //    //Received Sign
            //    Database.tbl_ISSREC_Mst objtbl_ISSREC_MstSReceivedSign = new Database.tbl_ISSREC_Mst();
            //    objtbl_ISSREC_MstSReceivedSign.TENANT_ID = TID;
            //    objtbl_ISSREC_MstSReceivedSign.COMPID = Convert.ToInt32(item.CompIDRecBy);
            //    objtbl_ISSREC_MstSReceivedSign.DOCID = DocID;
            //    objtbl_ISSREC_MstSReceivedSign.USER_ID = Convert.ToInt32(item.UserIDSignByReceive);
            //    objtbl_ISSREC_MstSReceivedSign.DMS_USER_ID = Convert.ToInt32(item.UserIDSignByReceive);
            //    objtbl_ISSREC_MstSReceivedSign.REFERENCENO = refID;
            //    objtbl_ISSREC_MstSReceivedSign.SEQ = item.SeqNo;
            //    objtbl_ISSREC_MstSReceivedSign.DATE = DateTime.Now;
            //    objtbl_ISSREC_MstSReceivedSign.ACTIVE = false;
            //    objtbl_ISSREC_MstSReceivedSign.DOCNO = item.REFID;
            //    objtbl_ISSREC_MstSReceivedSign.FromColumn = Convert.ToInt32(item.FromColumn);
            //    objtbl_ISSREC_MstSReceivedSign.ToColumn = Convert.ToInt32(item.ToColumn);
            //    if (item.SeqNo == 1)
            //    {
            //        objtbl_ISSREC_MstSReceivedSign.ActiveCRM = "Y";
            //    }
            //    objtbl_ISSREC_MstSReceivedSign.TYPE = "ReceivedSign";
            //    DB.tbl_ISSREC_Mst.AddObject(objtbl_ISSREC_MstSReceivedSign);
            //    DB.SaveChanges();
            //}



            //foreach (DMSRouteMST item in List123.ToList())
            //{
            //    Database.CRMActivity objtbl_CRMActivity = new Database.CRMActivity();
            //    objtbl_CRMActivity.TenentID = TID;
            //    objtbl_CRMActivity.COMPID = item.CompIDSEqNo;
            //    objtbl_CRMActivity.ActivityCompID = item.CompIDSEqNo;
            //    objtbl_CRMActivity.DocID = DocID;
            //    objtbl_CRMActivity.ACTIVITYCODE = item.ACTIVITYCODE.ToString();
            //    objtbl_CRMActivity.ACTIVITYTYPE = "DMS";
            //    objtbl_CRMActivity.REFTYPE = item.REFTYPE;
            //    objtbl_CRMActivity.REFSUBTYPE = item.REFSUBTYPE;
            //    objtbl_CRMActivity.RouteID = RouteName;
            //    // objtbl_CRMActivity.MyLineNo = item.LineNum;
            //    objtbl_CRMActivity.USERCODE = " ";
            //    objtbl_CRMActivity.ReferenceNo = CommonList[4];//ref # from New Doc Done
            //    objtbl_CRMActivity.EarlierRefNo = RefName;
            //    objtbl_CRMActivity.NextUser = "1";

            //    objtbl_CRMActivity.NextRefNo = strDocName;//= DOCUMENT NAME DONE
            //    objtbl_CRMActivity.AMIGLOBAL = "Y";
            //    objtbl_CRMActivity.MYPERSONNEL = "N";
            //    objtbl_CRMActivity.ActivityPerform = item.CompIDSEqNo.ToString();
            //    objtbl_CRMActivity.REMINDERNOTE = " ";
            //    //int a = item.REFID;
            //    //int b = item.LineNum;
            //    //int newNumber = int.Parse(a.ToString() + b.ToString());
            //    //objtbl_CRMActivity.ESTCOST = newNumber;//Seq # Done
            //    objtbl_CRMActivity.ESTCOST = item.SeqNo;
            //    objtbl_CRMActivity.GROUPCODE = "1";
            //    objtbl_CRMActivity.USERCODEENTERED = UID.ToString();
            //    objtbl_CRMActivity.InitialDate = DateTime.Now;
            //    objtbl_CRMActivity.UPDTTIME = DateTime.Now;
            //    objtbl_CRMActivity.USERNAME = UID.ToString();

            //    objtbl_CRMActivity.MyStatus = strStatus;
            //    DateTime CDate = DateTime.Now;
            //    int NoOFDay = item.Days2Complete;
            //    CDate = CDate.AddDays(NoOFDay);
            //    objtbl_CRMActivity.DeadLineDate = CDate;

            //    objtbl_CRMActivity.Active = item.ActiveCRM;

            //    //objtbl_CRMActivity.Active = "F";

            //    objtbl_CRMActivity.GroupBy = item.REFID.ToString() + RouteName + item.SeqNo + refID;
            //    objtbl_CRMActivity.Type = 1;
            //    objtbl_CRMActivity.FromColumn = Convert.ToInt32(item.FromColumn);
            //    objtbl_CRMActivity.ToColumn = Convert.ToInt32(item.ToColumn);
            //    DB.CRMActivities.AddObject(objtbl_CRMActivity);
            //    DB.SaveChanges();
            //}
        }

        public string getCompintype(int CYID)
        {
            return DB.REFTABLEs.Single(p => p.REFID == CYID && p.TenentID == TID&&p.TenentID==TID).REFNAME1;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            string URL = Session["ADMInPrevious"].ToString();
            Response.Redirect(URL);
        }

        protected void drpTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RID= Convert.ToInt32(drpTeamName.SelectedValue);
            BindTimeAssin(RID);
           
        }

        public void BindTimeAssin(int ID)
        {
            DrpAssignedTeamLeader.DataSource = DB.TeamNames.Where(p => p.TenentID == TID && p.REF_ID == ID);
            DrpAssignedTeamLeader.DataValueField = "TeamID";
            DrpAssignedTeamLeader.DataTextField = "MemberName";
            DrpAssignedTeamLeader.DataBind();
            DrpAssignedTeamLeader.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            txtName2.Text = Translate(txtName.Text, "ar");
            txtName3.Text = Translate(txtName.Text, "fr");
        }
        public string Translate(string textvalue, string to)
        {
            string appId = "A70C584051881A30549986E65FF4B92B95B353A5";//go to http://msdn.microsoft.com/en-us/library/ff512386.aspx to obtain AppId.
            // string textvalue = "Translate this for me";
            string from = "en";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + textvalue + "&from=" + from + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
    }

}
