using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Web.CRM.Class;
using Web.CRM.Class.Class;
using System.Transactions;

namespace Web.CRM.UserControl
{
    public partial class RightPanelUC : System.Web.UI.UserControl
    {

        int TID, LID, UID, EMPID, userID1, userTypeid = 0;
        string LangID = "";
        int MID = 0;
        int Type = 0;
        int AType = 0;
        int MyIDNo = 0;
        int ID = 0;
        int NoteID = 0;
        int TicketID = 0;
        int FileID = 0;
        int EmailIDAction = 0;
        int MemoID = 0;
        Database.CallEntities DB = new Database.CallEntities();
        //   Database.CallEntities DB1 = new Database.CallEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (Request.QueryString["ID"] != null)
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
            {
                if (Request.QueryString["CampID"] != null)
                {
                    ID = Convert.ToInt32(Request.QueryString["CampID"]);
                }

            }

            if (Request.QueryString["ID"] != null || Request.QueryString["CampID"] != null)
            {

                Session["CampaignID"] = ID;
                LID = Convert.ToInt32(Session["Location"]);
                if (Session["PageType"] != null)
                {
                    if (Session["PageType"].ToString() == Convert.ToString(ActionMaster.Type.Opprtutnity))
                    {
                        Type = Convert.ToInt32(ActionMaster.Type.Opprtutnity);

                    }
                    else if (Session["PageType"].ToString() == Convert.ToString(ActionMaster.Type.Lead))
                    {
                        Type = Convert.ToInt32(ActionMaster.Type.Lead);

                    }
                    else
                    {
                        Type = Convert.ToInt32(ActionMaster.Type.Campaign);

                    }
                    BindNote();
                    BindTicket();
                    BindFile();
                    BindMail();
                    BindMemo();
                    BindAppointment();//appointment

                    
                    GetNotes();
                    GetFiles();
                    GetEmail();
                    GetMemo();
                    GetTicket();
                    GetAppointment();//appointment
                    FillContractorID();
                    txtFrom.Text = (((USER_MST)Session["USER"]).EmailAddress);
                }
                if (!IsPostBack)
                {
                    BindNote();
                    BindTicket();
                    BindFile();
                    BindMail();
                    BindMemo();
                    GetNotes();
                    GetFiles();
                    GetEmail();
                    GetMemo();
                    GetTicket();
                    FillContractorID();
                    txtFrom.Text = (((USER_MST)Session["USER"]).EmailAddress);


                }
            }
        }
        public void SessionLoad()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }
        public void FillContractorID()
        {
            drptofield.DataSource = DB.USER_MST.Where(p => p.USER_ID != UID && p.TenentID == TID);
            drptofield.DataTextField = "LOGIN_ID";
            drptofield.DataValueField = "USER_ID";
            drptofield.DataBind();
            drptofield.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpactivity.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID).OrderBy(a => a.ACTIVITYE);
            drpactivity.DataTextField = "ACTIVITYE";
            drpactivity.DataValueField = "ACTIVITYE";
            drpactivity.DataBind();
            drpactivity.Items.Insert(0, new ListItem("-- Select --", "0"));


            drpproject.DataSource = DB.tbl_Project_old.Where(p => p.Active == true);
            drpproject.DataTextField = "name";
            drpproject.DataValueField = "ID";
            drpproject.DataBind();
            drpproject.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpAttachmentType.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "DMS" && P.REFSUBTYPE == "Attachment" && P.TenentID == TID).OrderBy(a => a.REFNAME1);
            drpAttachmentType.DataTextField = "REFNAME1";
            drpAttachmentType.DataValueField = "REFID";
            drpAttachmentType.DataBind();
            drpAttachmentType.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpAttachmentDocument.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "DMS" && P.REFSUBTYPE == "DOCTYPE" && P.TenentID == TID).OrderBy(a => a.REFNAME1);
            drpAttachmentDocument.DataTextField = "REFNAME1";
            drpAttachmentDocument.DataValueField = "REFID";
            drpAttachmentDocument.DataBind();
            drpAttachmentDocument.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnsavenote_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PageType"] != null && Session["CampaignID"] != null)
                {

                    MID = Convert.ToInt32(Session["CampaignID"]);
                    AType = Convert.ToInt32(ActionMaster.ActionType.Note);
                    ActionGlobal.DatainsertActionMaster(Type, AType, txtTitle.Text, txtNote123.Text, 0, TID, UID, MID, LID, "", "", "", NoteID, TicketID, FileID, EmailIDAction, MemoID);
                    BindNote();
                    GetNotes();

                }
                else
                {
                    pnlother.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnTicketSave_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //try
            //{

            if (Session["PageType"] != null && Session["CampaignID"] != null)
            {
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                //int UID1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                //int LID1 = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                string Type1 = Session["PageType"].ToString();
                int RoleID = 0;
                int RID = 0;
                int CID = 1;
                int Locaionid = 0;
                //   string AType1 = ActionMaster.Type.Campaign.ToString();
                string AAType1 = ActionMaster.ActionType.Ticket.ToString();
                MID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.Ticket);
                RoleID = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                if (Type1 == Convert.ToString(ActionMaster.Type.Campaign))
                {
                    RID = Convert.ToInt32(CampgainGlobal.UpdateCamapainTable(RoleID, Type, AType, MID));
                }
                else if (Type1 == Convert.ToString(ActionMaster.Type.Lead))
                {
                    RID = Convert.ToInt32(LeadGlobal.UpdateLeadTable(RoleID, Type, AType, MID));
                }
                else
                {
                    RID = Convert.ToInt32(OppertunityGlobal.UpdateOppertunityTable(RoleID, Type, AType, MID));
                }
                //int ActivityCode = DB1.ACM_CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB1.ACM_CRMMainActivities.Max(p => p.MasterCODE) + 1) : 1;
                int REFID = DB.REFTABLEs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                //string REFNO = "TICKET_" + ActivityCode + "_" + REFID;

                string Name = txtSubject.Text;
                string Duscrption = txtMessage.Text;
                string Department = drppriority.SelectedValue;
                int Priority = Convert.ToInt32(drpSDepartment.SelectedValue);
                InsertCRMMainActivity(RID, Department, Priority, Name, Duscrption);




                //Database.ACM_CRMMainActivities objCRMMainActivities = new Database.ACM_CRMMainActivities();
                //int TicketID = ActivityCode;
                //objCRMMainActivities.ID = RID;//DB.CRMMainActivities.Where(p => p.MyID == MyID1).Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Where(p => p.MyID == MyID1).Max(p => p.MySerial) + 1) : 1;
                //objCRMMainActivities.MasterCODE = TicketID;
                //objCRMMainActivities.TenentID = TID;
                //objCRMMainActivities.COMPID = CID;
                //objCRMMainActivities.ACTIVITYE = "Ticket";
                //objCRMMainActivities.Reference = REFNO;
                //objCRMMainActivities.AMIGLOBAL = false;
                //objCRMMainActivities.MYPERSONNEL = false;
                //objCRMMainActivities.INTERVALDAYS = 1;
                //objCRMMainActivities.REPEATFOREVER = false;
                //objCRMMainActivities.REPEATTILL = DateTime.Now;
                //objCRMMainActivities.ESTCOST = DB.CRMMainActivities.Where(p => p.ESTCOST == RoleID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.ESTCOST == RoleID).Max(p => p.ESTCOST) + 1) : 1;
                //objCRMMainActivities.GROUPCODE = Convert.ToInt32(drpSDepartment.SelectedValue);
                //objCRMMainActivities.USERCODEENTERED = drppriority.SelectedValue;
                //objCRMMainActivities.UPDTTIME = DateTime.Now;
                //objCRMMainActivities.USERNAME = strUName;
                //objCRMMainActivities.Remarks = txtMessage.Text;
                //objCRMMainActivities.Version = txtSubject.Text;
                //objCRMMainActivities.MyStatus = "Pending";
                //objCRMMainActivities.CRUP_ID = 1;
                //objCRMMainActivities.ModuleID = 7;
                //objCRMMainActivities.Type = 1;
                //objCRMMainActivities.USERCODE = UID1;
                //DB1.ACM_CRMMainActivities.AddObject(objCRMMainActivities);
                //DB1.SaveChanges();

                //Database.ACM_CRMActivities objCRMActivity = new Database.ACM_CRMActivities();
                //objCRMActivity.COMPID = CID;
                //objCRMActivity.TenentID = TID;
                //objCRMActivity.MasterCODE = TicketID;
                //objCRMActivity.REFSUBTYPE = ActionMaster.ActionType.Ticket.ToString();
                //objCRMActivity.ACTIVITYTYPE = "CRM";
                //objCRMActivity.MyLineNo = objCRMMainActivities.ID;
                //objCRMActivity.GroupBy = "Ticket";
                //objCRMActivity.NextRefNo = "N";
                //objCRMActivity.AMIGLOBAL = "Y";
                //objCRMActivity.MYPERSONNEL = "N";
                //objCRMActivity.ActivityPerform = objCRMMainActivities.Remarks;
                //objCRMActivity.REMINDERNOTE = objCRMMainActivities.Version;
                //objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.ESTCOST);
                //objCRMActivity.GROUPCODE = "1";
                //objCRMActivity.USERCODEENTERED = strUName;
                //objCRMActivity.UPDTTIME = DateTime.Now;
                //objCRMActivity.USERNAME = strUName;
                //objCRMActivity.RouteID = "Ticket";
                //objCRMActivity.MyStatus = "Pending";
                //objCRMActivity.Active = "Y";
                //objCRMMainActivities.MainID = 7;//Insert Module ID
                //objCRMActivity.ToColumn = DB1.ACM_CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
                //objCRMActivity.FromColumn = DB1.ACM_CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
                //objCRMActivity.Version = strUName + " Reply";
                //objCRMActivity.PerfReferenceNo = "";

                //objCRMActivity.REFTYPE = "";
                ////objCRMActivity.PerfReferenceNo = "";

                //DB1.ACM_CRMActivities.AddObject(objCRMActivity);
                //DB1.SaveChanges();

                //insert data in action master
                //Database.REFTABLE obj_Reference11 = new Database.REFTABLE();
                //obj_Reference11.TenentID = TID;
                //obj_Reference11.ActivityID = REFID;
                //obj_Reference11.MainActivityType = "CRM";
                //obj_Reference11.SubActivityType = "Ticket";
                //obj_Reference11.SHORTNAME = "Ticket";
                //obj_Reference11.NameEng = "Ticket";
                //obj_Reference11.NameOth2 = "Ticket";
                //obj_Reference11.NameOth3 = "Ticket";
                //obj_Reference11.CRUP_ID = 1;
                //obj_Reference11.ACTIVE = "Y";
                //obj_Reference11.Parameter4 = ActivityCode;
                //obj_Reference11.Parameter7 = 7;//Insert module ID
                //obj_Reference11.Parameter1 = REFNO;  //Reference NO
                //DB1.REFTABLE.AddObject(obj_Reference11);
                //DB.SaveChanges();

                //insert data in rights table 
                //Database.ACTIVITYRIGHTS obj_ActiRights = new Database.ACTIVITYRIGHTS();
                //obj_ActiRights.TenentID = TID;
                //if (Locaionid != 0)
                //    obj_ActiRights.LOCATION_ID = LID1;
                //obj_ActiRights.USER_ID = UID1;
                //obj_ActiRights.Activity_ID = TicketID;
                //obj_ActiRights.Module_ID = 7;//Convert.ToInt32(AAType1);
                //obj_ActiRights.ADD_FLAG = false;
                //obj_ActiRights.MODIFY_FLAG = false;
                //obj_ActiRights.DELETE_FLAG = false;
                //obj_ActiRights.VIEW_FLAG = false;
                //obj_ActiRights.ALL_FLAG = false;
                //obj_ActiRights.CRUP_ID = 1;
                //DB1.ACTIVITYRIGHTS.AddObject(obj_ActiRights);
                //DB1.SaveChanges();
                //  Haresh
                //lblTicketUserName.Text = strUName;
                //lblTicketNumber.Text = REFID.ToString();
                //lblConfirmTicketEmail.Text = ((USER_MST)Session["USER"]).EmailAddress.ToString();
                //ModalPopupExtender4.Show();

                // Haresh
                BindTicket();
                GetTicket();
                //int TickitNo = Convert.ToInt32(objCRMMainActivities.ESTCOST);
                //string display = "Youer Ticket Number Is " + REFID;
                //return;
            }
            // scope.Complete();
            //ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);


            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            // }
        }

        protected void btnsavefile_Click(object sender, EventArgs e)
        {
            pnlfile.Visible = true;
        }
        public void BindNote()
        {
            try
            {
                if (Session["PageType"] != null && Session["CampaignID"] != null)
                {
                    int CID = Convert.ToInt32(Session["CampaignID"]);
                    // String Type22=Convert.ToString(Session["PageType"]);
                    AType = Convert.ToInt32(ActionMaster.ActionType.Note);
                    if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                    {
                        ListViewNote.DataSource = CampgainGlobal.DisplayCampgainNoteData(Type, AType, UID, CID, TID, LID);//DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == Type && p.ActionType == AType && p.Active == true && p.Deleted == true && p.Enteredby == UID);
                        ListViewNote.DataBind();
                    }
                    else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                    {
                        ListViewNote.DataSource = LeadGlobal.DisplayLeadNoteData(Type, AType, UID, CID, TID, LID);//DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == Type && p.ActionType == AType && p.Active == true && p.Deleted == true && p.Enteredby == UID);
                        ListViewNote.DataBind();
                    }
                    else
                    {
                        ListViewNote.DataSource = OppertunityGlobal.DisplayOppertunityNoteData(Type, AType, UID, CID, TID, LID);//DB.ISActionMasters.Where(p => p.TenantedID == TID && p.Type == Type && p.ActionType == AType && p.Active == true && p.Deleted == true && p.Enteredby == UID);
                        ListViewNote.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }

        }

        public void BindTicket()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                string RefType = Convert.ToString(ActionMaster.Type.Campaign);
                string RefAType = Convert.ToString(ActionMaster.ActionType.Ticket);
                AType = Convert.ToInt32(ActionMaster.ActionType.Ticket);
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    ListViewTicket.DataSource = CampgainGlobal.DisplayCampgainTicketData(Type, AType, UID, CID, TID, LID, strUName);//DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
                    ListViewTicket.DataBind();
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    ListViewTicket.DataSource = LeadGlobal.DisplayLeadTicketData(Type, AType, UID, CID, TID, LID, strUName);//DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
                    ListViewTicket.DataBind();
                }
                else
                {
                    ListViewTicket.DataSource = OppertunityGlobal.DisplayTicketData(Type, AType, UID, CID, TID, LID, strUName);//DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
                    ListViewTicket.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }

        public void BindMemo()
        {
            try
            {
                AType = Convert.ToInt32(ActionMaster.ActionType.Memo);
                int CID = Convert.ToInt32(Session["CampaignID"]);
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    ListViewMemo.DataSource = CampgainGlobal.DisplayCampgainMemoData(Type, AType, UID, CID, TID, LID);
                    ListViewMemo.DataBind();
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    ListViewMemo.DataSource = LeadGlobal.DisplayLeadMemoData(Type, AType, UID, CID, TID, LID);
                    ListViewMemo.DataBind();
                }
                else
                {
                    ListViewMemo.DataSource = OppertunityGlobal.DisplayOppertunityMemoData(Type, AType, UID, CID, TID, LID);
                    ListViewMemo.DataBind();
                }
            }
            catch (Exception ex)
            { }
        }

        public void BindMail()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.Email);
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    ListViewEmail.DataSource = CampgainGlobal.DisplayCampgainEmailData(Type, AType, UID, CID, TID, LID);
                    ListViewEmail.DataBind();
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    ListViewEmail.DataSource = LeadGlobal.DisplayLeadEmailData(Type, AType, UID, CID, TID, LID);
                    ListViewEmail.DataBind();
                }
                else
                {
                    ListViewEmail.DataSource = OppertunityGlobal.DisplayOpprtutnityEmailData(Type, AType, UID, CID, TID, LID);
                    ListViewEmail.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }
        public void BindFile()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.File);
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    ListViewFile.DataSource = CampgainGlobal.DisplayCampgainFileData(Type, AType, UID, CID, TID, LID, strUName);
                    ListViewFile.DataBind();
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    ListViewFile.DataSource = LeadGlobal.DisplayLeadFileData(Type, AType, UID, CID, TID, LID, strUName);
                    ListViewFile.DataBind();
                }
                else
                {
                    ListViewFile.DataSource = OppertunityGlobal.DisplayOppertunityFileData(Type, AType, UID, CID, TID, LID, strUName);
                    ListViewFile.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }

        protected void btnAddnewFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PageType"] != null && Session["CampaignID"] != null)
                {

                    int RepeteID = 0;
                    Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = new Database.tbl_DMSAttachmentMst();
                    int FileID = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID).Max(p => p.AttachID) + 1) : 1;
                    MID = Convert.ToInt32(Session["CampaignID"]);
                    AType = Convert.ToInt32(ActionMaster.ActionType.File);

                    if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                    {
                        RepeteID = Convert.ToInt32(CampgainGlobal.UpdateCamapainTable(FileID, Type, AType, MID));
                    }
                    else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                    {
                        RepeteID = Convert.ToInt32(LeadGlobal.UpdateLeadTable(FileID, Type, AType, MID));
                    }
                    else
                    {
                        RepeteID = Convert.ToInt32(OppertunityGlobal.UpdateOppertunityTable(FileID, Type, AType, MID));
                    }
                    objtbl_DMSAttachmentMst.AttachID = RepeteID;//DB.tbl_DMSAttachmentMst.Where(p => p.AttachID == RepeteID).Count() > 0 ? Convert.ToInt32(DB.tbl_DMSAttachmentMst.Where(p => p.AttachID == RepeteID).Max(p => p.Serialno) + 1) : 1; ;
                    objtbl_DMSAttachmentMst.AttachmentsDetail = txtAttachmentsDetail.Text;
                    objtbl_DMSAttachmentMst.ReferenceNo = Type;
                    if (FUDoc.HasFile)
                    {
                        FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                        objtbl_DMSAttachmentMst.AttachmentPath = FUDoc.FileName;
                    }
                    objtbl_DMSAttachmentMst.Serialno = DB.tbl_DMSAttachmentMst.Where(p => p.AttachID == RepeteID && p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_DMSAttachmentMst.Where(p => p.AttachID == RepeteID && p.TenentID == TID).Max(p => p.Serialno) + 1) : 1;
                    objtbl_DMSAttachmentMst.AttachmentById = Convert.ToInt32(drpAttachmentDocument.SelectedValue);
                    objtbl_DMSAttachmentMst.AttachmentType = drpAttachmentType.SelectedValue;
                    objtbl_DMSAttachmentMst.TenentID = TID;
                    objtbl_DMSAttachmentMst.CreatedBy = UID;
                    objtbl_DMSAttachmentMst.Actived = true;
                    objtbl_DMSAttachmentMst.CreatedDate = DateTime.Now;
                    objtbl_DMSAttachmentMst.Deleted = true;
                    DB.tbl_DMSAttachmentMst.AddObject(objtbl_DMSAttachmentMst);
                    DB.SaveChanges();

                    lblMsg.Text = "  Data Save Successfully";
                    BindFile();
                    GetFiles();
                }
            }
            catch (Exception ex)
            { }
        }

        protected void txtnoofday_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtStartingDate.Text);

                DateTime dt2 = dt.AddDays(Convert.ToInt32(txtnoofday.Text));
                txtDueDate.Text = dt2.ToString("dd/MM/yyyy hh:mm:ss tt");
            }
            catch (Exception ex)
            { }
        }
        protected void txtDueDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtStartingDate.Text);

                DateTime dt2 = Convert.ToDateTime(txtDueDate.Text);

                if (dt > dt2)
                {
                    lblMsg.Text = " Due Date less date of StartingDate..";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //pnlSuccessMsg.Visible = true;
                    txtDueDate.Text = "";
                }
                else
                {
                    //pnlSuccessMsg.Visible = false;
                }
            }
            catch (Exception ex)
            { }

        }
        protected void cbreminder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbreminder.Checked == true)
                {
                    txtReminderDate.Enabled = true;
                    // txtReminderTime.Enabled = true;
                }
                else
                {
                    txtReminderDate.Enabled = false;
                    // txtReminderTime.Enabled = false;
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btntaskpanal_Click(object sender, EventArgs e)
        {
            //pnltask.Visible = true;
            // pnlassigntask.Visible = false;
            // pnlcancleassignment.Visible = false;
            //pnldetail.Visible = false;
            //pnlmarkcomplaete.Visible = false;
            //pnlrecurrnce.Visible = false;
            //pnlsendstatus.Visible = false;

        }

        protected void btnsavetask_Click(object sender, EventArgs e)
        {
            try
            {
                Database.tbl_Task objtbl_Task = new Database.tbl_Task();
                //Server Content Send data Yogesh
                //objtbl_Task.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                objtbl_Task.TaskID = DB.tbl_Task.Where(p => p.TenentID == TID).Count() > 0 ? DB.tbl_Task.Where(p => p.TenentID == TID).Max(p => p.TaskID) + 1 : 1;
                //objtbl_Task.CerticateNo = Convert.ToInt32(drpCerticateNo.SelectedValue);
                //objtbl_Task.ForEmp_ID = Convert.ToInt32(drpForEmp_ID.SelectedValue);
                objtbl_Task.ActivityId = //drpactivity.SelectedValue;
                objtbl_Task.ProjectId = drpproject.SelectedValue;
                objtbl_Task.PerformingEmp_ID = Convert.ToInt32(drptofield.SelectedValue);
                objtbl_Task.Subject = txtSubject.Text;
                objtbl_Task.StartingDate = Convert.ToDateTime(txtStartingDate.Text);
                objtbl_Task.TenentID = TID;
                objtbl_Task.LocationID = 1;
                objtbl_Task.CerticateNo = 1;
                objtbl_Task.TaskType = drptasktype.SelectedValue;
                objtbl_Task.TaskStatus = drpTaskStatus.SelectedValue;
                objtbl_Task.DueDate = Convert.ToDateTime(txtDueDate.Text);
                objtbl_Task.Priority = DrpPriorityTask.SelectedValue;
                //objtbl_Task.ActualCompletionDate = Convert.ToDateTime(txtActualCompletionDate.Text);
                objtbl_Task.CompletedPerctange = Convert.ToInt32(drpCompletedPerctange.SelectedValue);

                objtbl_Task.Remarks = txtReminder.Text;
                objtbl_Task.Active = true;
                //objtbl_Task.CruipID = txtCruipID.Text;


                DB.tbl_Task.AddObject(objtbl_Task);
                DB.SaveChanges();
                if (Request.QueryString["NoteID"] != null && Request.QueryString["SerialID"] != null && Request.QueryString["TicketID"] != null && Request.QueryString["FileID"] != null && Request.QueryString["EmailIDAction"] != null && Request.QueryString["MemoID"] != null)
                {
                    int NoteID = Convert.ToInt32(Request.QueryString["NoteID"]);
                    int SerialID = Convert.ToInt32(Request.QueryString["SerialID"]);
                    int Type = Convert.ToInt32(ActionMaster.Type.Campaign);
                    int AType = Convert.ToInt32(ActionMaster.ActionType.Task);


                    int TicketID = Convert.ToInt32(Request.QueryString["SerialID"]);
                    int FileID = Convert.ToInt32(Request.QueryString["FileID"]);
                    int EmailIDAction = Convert.ToInt32(Request.QueryString["EmailIDAction"]);
                    int MemoID = Convert.ToInt32(Request.QueryString["MemoID"]);

                    MID = Convert.ToInt32(Session["MyID"]);
                    ActionGlobal.DatainsertActionMaster(Type, AType, "", "", SerialID, TID, UID, MID, LID, "", "", "", NoteID, TicketID, FileID, EmailIDAction, MemoID);
                }


                lblMsg.Text = "  Data Save Successfully";
            }
            catch (Exception ex)
            { }
            // pnlSuccessMsg.Visible = true;
            //BindData();
        }

        public void BindData1()
        {
            // Listview1.DataSource = DB.tbl_Task.Where(p => p.Active == true);
            // Listview1.DataBind();
        }



        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PageType"] != null && Session["CampaignID"] != null && Request.QueryString["NoteID"] != "" && Request.QueryString["SerialID"] != "" && Request.QueryString["TicketID"] != "" && Request.QueryString["FileID"] != "" && Request.QueryString["EmailIDAction"] != "" && Request.QueryString["MemoID"] != "")
                {
                    MID = Convert.ToInt32(Session["CampaignID"]);
                  
                    int EmailIDAction = Convert.ToInt32(Request.QueryString["EmailIDAction"]);
                   
                    AType = Convert.ToInt32(ActionMaster.ActionType.Email);
                    ActionGlobal.DatainsertActionMaster(Type, AType, txtTitle.Text, txtBody11.Text, 0, TID, UID, MID, LID, txtTo.Text, txtFrom.Text, txtSubject.Text, NoteID, TicketID, FileID, EmailIDAction, MemoID);
                    BindMail();
                    GetEmail();

                    sendEmail(txtBody11.Text, txtTo.Text, txtSubjectEmail1.Text, txtFrom.Text);

                    string display = "Mail Send Successfully..";
                }
            }
            catch (Exception ex)
            { }
            //  ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('" + display + "');", true);
        }

        public void sendEmail(string body, string email, string Subject, string From)
        {
            if (String.IsNullOrEmpty(email))
                return;

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.Subject = Subject;

            msg.From = new System.Net.Mail.MailAddress(From);

            msg.To.Add(new System.Net.Mail.MailAddress(email));
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.IsBodyHtml = true;
            //msg.Priority = MailPriority.High;

            System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            smpt.UseDefaultCredentials = false;
            smpt.Host = "mail.ayosoftech.com";

            smpt.Port = 587;

            smpt.EnableSsl = false;

            // smpt.Credentials = new System.Net.NetworkCredential(From, "johar123$");
            smpt.Credentials = new System.Net.NetworkCredential("info@ayosoftech.com", "P@ssw0rd123");

            smpt.Send(msg);




        }

        protected void btnsavememo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PageType"] != null && Session["CampaignID"] != null)
                {
                    //if (Request.QueryString["NoteID"] != "" && Request.QueryString["TicketID"] != "" && Request.QueryString["FileID"] != "" && Request.QueryString["EmailIDAction"] != "" && Request.QueryString["MemoID"] != "")
                    //{
                    MID = Convert.ToInt32(Session["CampaignID"]);
                    AType = Convert.ToInt32(ActionMaster.ActionType.Memo);
                    int NoteID = Convert.ToInt32(Request.QueryString["NoteID"]);
                    ActionGlobal.DatainsertActionMaster(Type, AType, txttitlememo.Text, txtMemoDescrption1.Text, 0, TID, UID, MID, LID, "", "", "", NoteID, TicketID, FileID, EmailIDAction, MemoID);
                    BindMemo();
                    GetMemo();
                    //}
                    string display = "Data Save Successfully..";
                }



            }
            catch (Exception ex)
            { }
            // ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('" + display + "');", true);
        }

        public void GetNotes()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                int c = 0;
                AType = Convert.ToInt32(ActionMaster.ActionType.Note);
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    c = CampgainGlobal.CountCampgainNoteData(Type, AType, UID, CID, TID, LID);
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    c = LeadGlobal.CountLeadNoteData(Type, AType, UID, CID, TID, LID);
                }
                else
                {
                    c = OppertunityGlobal.CountOppertunityNoteData(Type, AType, UID, CID, TID, LID);
                }
                if (c != 0)
                {
                    lblnotecount.Text = c.ToString();
                }
                else
                {
                    lblnotecount.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }
        public void GetFiles()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.File);
                int c = 0;
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    c = CampgainGlobal.CountCampgainFileData(Type, AType, UID, CID, TID, LID, strUName);
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    c = LeadGlobal.CountLeadFileData(Type, AType, UID, CID, TID, LID, strUName);
                }
                else
                {
                    c = OppertunityGlobal.CountOppertunityFileData(Type, AType, UID, CID, TID, LID, strUName);
                }


                if (c != 0)
                {
                    lblfiles.Text = c.ToString();
                }
                else
                {
                    lblfiles.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }
        public void GetTicket()
        {
            //try
            //{
            int d = 0;
            int CID = Convert.ToInt32(Session["CampaignID"]);
            string RefType = ActionMaster.Type.Campaign.ToString();
            string RefAType = ActionMaster.ActionType.Ticket.ToString();
            string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
            AType = Convert.ToInt32(ActionMaster.ActionType.Ticket);

            if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
            {
                d = CampgainGlobal.CountCampgainTicketData(Type, AType, UID, CID, TID, LID, strUName);
            }
            else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
            {
                d = LeadGlobal.CountLeadTicketData(Type, AType, UID, CID, TID, LID, strUName);
            }
            else
            {
                d = OppertunityGlobal.CountOppertunityTicketData(Type, AType, UID, CID, TID, LID, strUName);
            }

            //DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
            if (d != 0)
            {
                lblticketcount.Text = d.ToString();
            }
            else
            {
                lblticketcount.Text = "0";
            }
            //}
            //catch (Exception ex)
            //{ }
        }
        public void GetMemo()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.Memo);
                int c = 0;
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    c = CampgainGlobal.CountCampgainMemoData(Type, AType, UID, CID, TID, LID);
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    c = LeadGlobal.CountLeadMemoData(Type, AType, UID, CID, TID, LID);
                }
                else
                {
                    c = OppertunityGlobal.CountOppertunityMemoData(Type, AType, UID, CID, TID, LID);
                }
                if (c != 0)
                {
                    lblMemo.Text = c.ToString();
                }
                else
                {
                    lblMemo.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }
        public void GetEmail()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.Email);
                int c = 0;
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    c = CampgainGlobal.CountCampgainEmailData(Type, AType, UID, CID, TID, LID);
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    c = LeadGlobal.CountLeadEmailData(Type, AType, UID, CID, TID, LID);
                }
                else
                {
                    c = OppertunityGlobal.CountOppertunityEmailData(Type, AType, UID, CID, TID, LID);
                }

                if (c != 0)
                {
                    lblEmail.Text = c.ToString();
                }
                else
                {
                    lblEmail.Text = "0";
                }
            }
            catch (Exception ex)
            { }
        }
        protected void ltsRemainderNotes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnclick123")
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    LinkButton linkID = (LinkButton)e.Item.FindControl("btnclick123");
                    Label tikitID = (Label)e.Item.FindControl("tikitID");
                    int Tikitno = Convert.ToInt32(tikitID.Text);
                    ViewState["TIkitNumber"] = Tikitno;
                    panChat.Visible = true;

                    string UIN = ((USER_MST)Session["USER"]).FIRST_NAME; ;//((TBLCONTACT)Session["USER"]).PersName1;


                    listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno && p.TenentID == TID).OrderBy(p => p.MyLineNo);
                    listChet.DataBind();

                    ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno && p.TenentID == TID).OrderBy(p => p.MyLineNo);
                    ListHistoy.DataBind();

                }
            }

        }

        public void getCommunicatinData()
        {
            try
            {
                int Tikitno = Convert.ToInt32(ViewState["TIkitNumber"]);

                listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno && p.TenentID == TID).OrderBy(p => p.MyLineNo);
                listChet.DataBind();
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno && p.TenentID == TID).OrderBy(p => p.MyLineNo);
                ListHistoy.DataBind();
            }
            catch (Exception ex)
            { }
        }


        public void ClenCat()
        {
            txtComent.Text = "";
        }

        protected void btnSubmitchat_Click(object sender, EventArgs e)
        {
            try
            {
                string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;

                int RoleID = 1;
                int CID = 1;
                int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
                if (RoleID == 4)
                {
                    Database.CRMActivity objCRMActivity = new Database.CRMActivity();
                    objCRMActivity.COMPID = CID;
                    objCRMActivity.TenentID = TID;
                    objCRMActivity.PerfReferenceNo = "CRM";
                    objCRMActivity.REFTYPE = "CRM";
                    objCRMActivity.MasterCODE = tiki;
                    objCRMActivity.REFSUBTYPE = "Reference";
                    objCRMActivity.ACTIVITYTYPE = "TKT";
                    objCRMActivity.MyLineNo = TID;
                    objCRMActivity.NextRefNo = "N";
                    objCRMActivity.GroupBy = "Ticket";
                    objCRMActivity.AMIGLOBAL = "Y";
                    objCRMActivity.MYPERSONNEL = "N";
                    objCRMActivity.ActivityPerform = txtComent.Text;
                    objCRMActivity.REMINDERNOTE = txtComent.Text;
                    objCRMActivity.ESTCOST = tiki;
                    objCRMActivity.GROUPCODE = "1";
                    objCRMActivity.USERCODEENTERED = strUName;
                    objCRMActivity.UPDTTIME = DateTime.Now;
                    objCRMActivity.USERNAME = strUName;
                    objCRMActivity.RouteID = "Ticket";
                    objCRMActivity.MyStatus = "Pending";
                    objCRMActivity.Active = "Y";
                    objCRMActivity.ToColumn = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ToColumn) + 1) : 1;
                    objCRMActivity.FromColumn = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.FromColumn) + 1) : 1;
                    objCRMActivity.Version = "Suppot Reply";
                    DB.CRMActivities.AddObject(objCRMActivity);
                    DB.SaveChanges();
                    getCommunicatinData();
                    ClenCat();

                }
                else
                {

                    //Database.ACM_CRMActivities objCRMActivity = new Database.ACM_CRMActivities();
                    //objCRMActivity.COMPID = CID;
                    //objCRMActivity.MasterCODE = tiki;
                    //objCRMActivity.PerfReferenceNo = "CRM";
                    //objCRMActivity.TenentID = TID;
                    //objCRMActivity.REFTYPE = "CRM";
                    //objCRMActivity.REFSUBTYPE = "Reference";
                    //objCRMActivity.ACTIVITYTYPE = "TKT";
                    //objCRMActivity.MyLineNo = TID;
                    //objCRMActivity.NextRefNo = "N";
                    //objCRMActivity.GroupBy = "Ticket";
                    //objCRMActivity.AMIGLOBAL = "Y";
                    //objCRMActivity.MYPERSONNEL = "N";
                    //objCRMActivity.ActivityPerform = txtComent.Text;
                    //objCRMActivity.REMINDERNOTE = txtComent.Text;
                    //objCRMActivity.ESTCOST = tiki;
                    //objCRMActivity.GROUPCODE = "1";
                    //objCRMActivity.USERCODEENTERED = strUName;
                    //objCRMActivity.UPDTTIME = DateTime.Now;
                    //objCRMActivity.USERNAME = strUName;
                    //objCRMActivity.RouteID = "Ticket";
                    //objCRMActivity.MyStatus = "Pending";
                    //objCRMActivity.Active = "Y";
                    //objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
                    //objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
                    //objCRMActivity.Version = strUName + " Reply";
                    //DB1.ACM_CRMActivities.AddObject(objCRMActivity);
                    //DB1.SaveChanges();




                    int MsterCose = tiki;
                    int TenentID = TID;
                    int COMPID = CID;
                    int MasterCODE = MsterCose;
                    int LinkMasterCODE = 1;
                    int MenuID = 0;
                    int ActivityID = 0;
                    string ACTIVITYTYPE = "Ticket";
                    string REFTYPE = "Ticket";
                    string REFSUBTYPE = "CRM";
                    string PerfReferenceNo = "CRM";
                    string EarlierRefNo = "A";
                    string NextUser = UNAME;
                    string NextRefNo = "A";
                    string AMIGLOBAL = "Y";
                    string MYPERSONNEL = "Y";
                    string ActivityPerform = txtComent.Text;
                    string REMINDERNOTE = txtComent.Text;
                    int ESTCOST = 0;
                    string GROUPCODE = "A";
                    string USERCODEENTERED = "A";
                    DateTime UPDTTIME = DateTime.Now;
                    DateTime UploadDate = DateTime.Now;
                    string USERNAME = UNAME;
                    int CRUP_ID = 0;
                    DateTime InitialDate = DateTime.Now;
                    DateTime DeadLineDate = DateTime.Now;
                    string RouteID = "A";
                    string Version1 = UNAME + " Reply"; ;
                    int Type = 0;
                    string MyStatus = "Pending";
                    string GroupBy = "Ticket";
                    int DocID = 0;
                    int ToColumn = 0;
                    int FromColumn = 0;
                    string Active = "Y";
                    int MainSubRefNo = 0;
                    string URL = Request.Url.AbsolutePath;
                    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL);
                    int SID = 1240103;
                    string BName = "Reply Tikit";
                    string P2 = "aa";
                    string P3 = "aa";
                    inserCrmproghw(TID, SID, BName, P2, P3, tiki);
                    getCommunicatinData();
                    ClenCat();

                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btntask_Click(object sender, EventArgs e)
        {
            // Response.Redirect("AddTask1.aspx?Type=" + Type+ "&NoteID=" +);
        }
        public string GetTaskCount(int ID)
        {
            int Count = 0;
            try
            {
                string ID1 = Convert.ToString(ID);

                if (DB.tbl_Task.Where(p => p.ActivityId == ID1 && p.TenentID == TID).Count() > 0)
                {
                    Count = Convert.ToInt32(DB.tbl_Task.Where(p => p.ActivityId == ID1 && p.TenentID == TID).Count());

                }
            }
            catch (Exception ex)
            {

            }
            return Count.ToString();
        }
        public int GetNoteTaskCount(int MyID, int ATypeID, int Serial, int Type)
        {
            int TaskCount = 0;
            int SelfID = Convert.ToInt32(MyID);
            int MySerial = Convert.ToInt32(Serial);
            int Type1 = Convert.ToInt32(Type);
            int AType1 = Convert.ToInt32(ATypeID);
            if (Session["CampaignID"] != null)
            {
                int CampaignID = Convert.ToInt32(Session["CampaignID"]);

                List<tbl_Task> obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.TenentID == TID).ToList();
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    Database.tbl_Opportunity_Mst obj_Opportunity = DB.tbl_Opportunity_Mst.Single(p => p.OpportID == CampaignID && p.TenentID == TID);
                    if (obj_Opportunity.NoteID != null)
                    {

                        List<ISActionMaster> obj_List1 = DB.ISActionMasters.Where(p => p.MyIDSelf == SelfID && p.Type == Type1 && p.ActionType == 2 && p.MySelfSerialNumber == MySerial && p.TenantedID == TID).ToList();
                        // obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.MainTaskID == obj_Opportunity.NoteID && p.CampID==CampaignID).ToList();
                        TaskCount = obj_List1.Count();
                    }

                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    Database.tbl_Campaign_Mst obj_Camp = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CampaignID && p.TenentID == TID);

                    if (obj_Camp.NoteID != null)
                    {
                        List<ISActionMaster> obj_List2 = DB.ISActionMasters.Where(p => p.MyIDSelf == SelfID && p.Type == Type1 && p.ActionType == 2 && p.MySelfSerialNumber == MySerial && p.TenantedID == TID).ToList();
                        // obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.MainTaskID == obj_Opportunity.NoteID && p.CampID==CampaignID).ToList();
                        TaskCount = obj_List2.Count();
                    }
                }
                else
                {
                    Database.tbl_Lead_Mst obj_Lead = DB.tbl_Lead_Mst.Single(p => p.ID == CampaignID && p.TenentID == TID);
                    if (obj_Lead.NoteID != null)
                    {
                        List<ISActionMaster> obj_List3 = DB.ISActionMasters.Where(p => p.MyIDSelf == SelfID && p.Type == Type1 && p.ActionType == 2 && p.MySelfSerialNumber == MySerial && p.TenantedID == TID).ToList();
                        // obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.MainTaskID == obj_Opportunity.NoteID && p.CampID==CampaignID).ToList();
                        TaskCount = obj_List3.Count();
                    }

                }

            }
            return TaskCount;
        }
        public void BindTask()
        {
            if (Session["CampaignID"] != null)
            {
                int CampaignID = Convert.ToInt32(Session["CampaignID"]);

                List<tbl_Task> obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.TenentID == TID).ToList();
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    // Database.tbl_Opportunity_Mst obj_Opportunity = DB.tbl_Opportunity_Mst.Single(p => p.OpportID == CampaignID);
                    obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.MainTaskID == CampaignID && p.TenentID == TID).ToList();
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    // Database.CRM_tbl_Campaign_Mst obj_Campa = DB.CRM_tbl_Campaign_Mst.Single(p => p.ID == CampaignID);
                    obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.MainTaskID == CampaignID && p.TenentID == TID).ToList();
                }
                else
                {
                    // Database.CRM_tbl_Lead_Mst obj_Lead = DB.CRM_tbl_Lead_Mst.Single(p => p.ID == CampaignID);
                    obj_Task = DB.tbl_Task.Where(p => p.Type == Type && p.MainTaskID == CampaignID && p.TenentID == TID).ToList();
                }

            }

        }


        public void InsertCRMMainActivity(int TransNo, string ADDACTIvity, int Priority, string CampinName, string Discrption, DateTime? StartDate = null, DateTime? EndDate = null, string URLL = null)
        {
            // string strStatus = ViewState["MyStatus"].ToString();
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
            DateTime REPEATTILL = DateTime.Now;
            DateTime UPDTTIME = DateTime.Now;
            DateTime UploadDate = DateTime.Now;
            string URL = "";
            int TenentID = TID;
            int LocationID = LID;
            int USERCODE = UID;
            int COMPID = 99999999;
            int LinkMasterCODE = 1;
            int ID = TransNo;
            int RouteID = 1;
            string ACTIVITYE = "Ticket";
            string ACTIVITYA = "Ticket";
            string ACTIVITYA2 = "CRM";
            bool AMIGLOBAL = true;
            bool MYPERSONNEL = true;
            int INTERVALDAYS = 3;
            bool REPEATFOREVER = true;
            if(StartDate != null)
            {
                REPEATTILL = Convert.ToDateTime(StartDate);
            }                
            else
            {
                REPEATTILL = DateTime.Now;
            }                
            string REMINDERNOTE = CampinName;
            int ESTCOST = 1;
            int GROUPCODE = Convert.ToInt32(Priority);
            string USERCODEENTERED = ADDACTIvity;
            if(EndDate != null)
            {
                UPDTTIME = Convert.ToDateTime(EndDate);
                UploadDate = Convert.ToDateTime(EndDate);
            }
            else
            {
                UPDTTIME = DateTime.Now;
                UploadDate = DateTime.Now;
            }           
            string USERNAME = UNAME;
            string Remarks = Discrption;
            int CRUP_ID = 1;
            string Version1 = UNAME;
            int Type = 1;
            string MyStatus = "Pending";
            int MainID = 1;
            int ModuleID = 1;
            string RecodType = ADDACTIvity;
            if(URLL != null)
            {
                URL = URLL;
            }
            else
            {
                URL = "/ACM/TikitShow.aspx";
            }            
            string CampynName = CampinName;
            string CampynDescription = Discrption;
            int REFNUMber = Classes.ACMClass.InsertACM_CRMMainActivities(TenentID, COMPID, LinkMasterCODE, LocationID, ID, RouteID, USERCODE, ACTIVITYE, ACTIVITYA, ACTIVITYA2, AMIGLOBAL, MYPERSONNEL, INTERVALDAYS, REPEATFOREVER, REPEATTILL, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate , USERNAME, Remarks, CRUP_ID, Version1, Type, MyStatus, MainID, ModuleID, URL, CampynName, CampynDescription);
            //Eco_ICTR_HD obj = DB.Eco_ICTR_HD.Single(p => p.MYTRANSID == TransNo);
            //obj.CrmActivityRefNo = REFNUMber;
            int SID = 1240103;
            string BName = "Reply Tikit";
            string P2 = "aa";
            string P3 = "aa";
            inserCrmproghw(TID, SID, BName, P2, P3, TransNo);

        }

        public void inserCrmproghw(int TID, int SID, string BName, string P2, string P3, int TRction)
        {
            // ACM_CRMMainActivities obj = DB1.ACM_CRMMainActivities.Single(p => p.TenentID == TID  && p.ID == TRction);
            int ACID = Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.MyLineNo));
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
        //appointment by dipak
        protected void btnsaveAppoint_Click(object sender, EventArgs e)
        {
            if (Session["PageType"] != null && Session["CampaignID"] != null)
            {
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                //int UID1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                //int LID1 = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                string Type1 = Session["PageType"].ToString();
                int RoleID = 0;
                int AID = 0;
                int CID = 1;
                int Locaionid = 1;
                //   string AType1 = ActionMaster.Type.Campaign.ToString();
                string AAType1 = ActionMaster.ActionType.Appointment.ToString();
                MID = Convert.ToInt32(Session["CampaignID"]);
                AType = Convert.ToInt32(ActionMaster.ActionType.Appointment);
                RoleID = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                if (Type1 == Convert.ToString(ActionMaster.Type.Campaign))
                {
                    AID = Convert.ToInt32(CampgainGlobal.UpdateCamapainTable(RoleID, Type, AType, MID));
                }
                else if (Type1 == Convert.ToString(ActionMaster.Type.Lead))
                {
                    AID = Convert.ToInt32(LeadGlobal.UpdateLeadTable(RoleID, Type, AType, MID));
                }
                else
                {
                    AID = Convert.ToInt32(OppertunityGlobal.UpdateOppertunityTable(RoleID, Type, AType, MID));
                }
                //int ActivityCode = DB1.ACM_CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB1.ACM_CRMMainActivities.Max(p => p.MasterCODE) + 1) : 1;
                int REFID = DB.REFTABLEs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                //string REFNO = "TICKET_" + ActivityCode + "_" + REFID;

                
                string Duscrption = txtTitleAP.Text;
                string Name = "";
                string Department = "0";
                string Priority = drpColor.SelectedValue;
                DateTime sdate = Convert.ToDateTime(txtSdateAP.Text);
                DateTime edate = Convert.ToDateTime(txtEnddateAP.Text);
                string URL = txtURLAP.Text;
                //CRMMainActivityAppoint(RID, Department, Priority, Name, Duscrption, sdate, edate, URL);
                string reference = "1";//ViewState["Appoint"].ToString();

                Classes.CRMClass.InsertAppointment(0, TID, 1, Duscrption, sdate, edate, Priority.ToString(), URL, true, true, reference, "Insert",AID,Type,AType);
                BindAppointment();               
                GetAppointment();
            }
        }
        public void CRMMainActivityAppoint(int TransNo, string ADDACTIvity, int Priority, string CampinName, string Discrption, DateTime? StartDate = null, DateTime? EndDate = null, string URLL = null)
        {
            // string strStatus = ViewState["MyStatus"].ToString();
            string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME.ToString();
            DateTime REPEATTILL = DateTime.Now;
            DateTime UPDTTIME = DateTime.Now;
            DateTime UploadDate = DateTime.Now;
            string URL = "";
            int TenentID = TID;
            int LocationID = 1;
            int USERCODE = UID;
            int COMPID = 99999999;
            int LinkMasterCODE = 1;
            int ID = TransNo;
            int RouteID = 1;
            string ACTIVITYE = "AppoinMent";
            string ACTIVITYA = "AppoinMent";
            string ACTIVITYA2 = "CRM";
            bool AMIGLOBAL = true;
            bool MYPERSONNEL = true;
            int INTERVALDAYS = 3;
            bool REPEATFOREVER = true;
            if (StartDate != null)
            {
                REPEATTILL = Convert.ToDateTime(StartDate);
            }
            else
            {
                REPEATTILL = DateTime.Now;
            }
            string REMINDERNOTE = CampinName;
            int ESTCOST = 1;
            int GROUPCODE = Convert.ToInt32(Priority);
            string USERCODEENTERED = ADDACTIvity;
            if (EndDate != null)
            {
                UPDTTIME = Convert.ToDateTime(EndDate);
                UploadDate = Convert.ToDateTime(EndDate);
            }
            else
            {
                UPDTTIME = DateTime.Now;
                UploadDate = Convert.ToDateTime(EndDate);
            }
            string USERNAME = UNAME;
            string Remarks = Discrption;
            int CRUP_ID = 1;
            string Version1 = UNAME;
            int Type = 1;
            string MyStatus = "Appoint";
            int MainID = 1;
            int ModuleID = 1;
            string RecodType = ADDACTIvity;
            if (URLL != null)
            {
                URL = URLL;
            }
            else
            {
                URL = "/ACM/TikitShow.aspx";
            }
            string CampynName = CampinName;
            string CampynDescription = Discrption;
            int REFNUMber = Classes.ACMClass.InsertACM_CRMMainActivities(TenentID, COMPID, LinkMasterCODE, LocationID, ID, RouteID, USERCODE, ACTIVITYE, ACTIVITYA, ACTIVITYA2, AMIGLOBAL, MYPERSONNEL, INTERVALDAYS, REPEATFOREVER, REPEATTILL, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, Remarks, CRUP_ID, Version1, Type, MyStatus, MainID, ModuleID, URL, CampynName, CampynDescription);
            ViewState["Appoint"] = REFNUMber;
            //Eco_ICTR_HD obj = DB.Eco_ICTR_HD.Single(p => p.MYTRANSID == TransNo);
            //obj.CrmActivityRefNo = REFNUMber;
            int SID = 1240103;
            string BName = "Reply Appoint";
            string P2 = "aa";
            string P3 = "aa";
            inserCrmproghw(TID, SID, BName, P2, P3, TransNo);

        }
        
        public void BindAppointment()
        {
            try
            {
                int CID = Convert.ToInt32(Session["CampaignID"]);
                string RefType = Convert.ToString(ActionMaster.Type.Campaign);
                string RefAType = Convert.ToString(ActionMaster.ActionType.Appointment);
                AType = Convert.ToInt32(ActionMaster.ActionType.Appointment);
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    ListAppoint.DataSource = CampgainGlobal.DisplayCampgainappointData(Type, AType, UID, CID, TID, LID);//DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
                    ListAppoint.DataBind();
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    ListAppoint.DataSource = LeadGlobal.DisplayLeadappointData(Type, AType, UID, CID, TID, LID, strUName);//DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
                    ListAppoint.DataBind();
                }
                else
                {
                    ListAppoint.DataSource = OppertunityGlobal.DisplayoppappointData(Type, AType, UID, CID, TID, LID, strUName);//DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
                    ListAppoint.DataBind();
                }

            }
            catch (Exception ex)
            { }
        }
        public void GetAppointment()
        {
            int d = 0;
            int CID = Convert.ToInt32(Session["CampaignID"]);
            string RefType = ActionMaster.Type.Campaign.ToString();
            string RefAType = ActionMaster.ActionType.Appointment.ToString();
            string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
            AType = Convert.ToInt32(ActionMaster.ActionType.Appointment);

            if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
            {
                d = CampgainGlobal.CountCampgainAppointData(Type, AType, UID, CID, TID, LID);
            }
            else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
            {
                d = LeadGlobal.CountLeadAppointData(Type, AType, UID, CID, TID, LID, strUName);
            }
            else
            {
                d = OppertunityGlobal.CountOppertunityAppointData(Type, AType, UID, CID, TID, LID, strUName);
            }

            //DB.CRMMainActivities.Where(x => x.TenentID == TID && x.REFTYPE == RefType && x.ACTIVITYTYPE == "CRM" && x.REFSUBTYPE == RefAType && x.USERNAME == strUName);
            if (d != 0)
            {
                lblappoint.Text = d.ToString();
            }
            else
            {
                lblappoint.Text = "0";
            }
        }
        public string Getcolor(int ID)
        {
            if (ID == 1)
                return "Red";
            else if (ID == 2)
                return "Green";
            else if (ID == 3)
                return "Blue";
            else if (ID == 4)
                return "Yellow";
            else if (ID == 5)
                return "Purple";
            else
                return "Not Found";            
        }
        public string Geturl(int ID)
        {
            if(DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID).Count() > 0)
            {
                string urlname = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID).UrlRedirct.ToString();
                return urlname;
            }
            else
            {
                return "Not Used";
            }
        }

    }


}