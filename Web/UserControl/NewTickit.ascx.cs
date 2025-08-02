using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;

namespace Web.UserControl
{
    public partial class NewTickit : System.Web.UI.UserControl
    {
        CallEntities DB = new CallEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER"] != null)
                {
                    BindData();
                    bindModule();
                    panChat.Visible = false;
                    pnlTags.Visible = false;
                    string UIN = ((USER_MST)Session["USER"]).LOGIN_ID;
                    lblUserName.Text = UIN;
                    pnlTicki.Visible = false;
                    ToalCoun();
                    int LID = Convert.ToInt32(Session["USER_ID"]);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    if (DB.USER_MST.Where(p => p.USER_ID == LID).Count() > 0)
                    {
                        imgUser.ImageUrl = "Gallery/" + DB.USER_MST.Single(p => p.USER_ID == LID).Avtar;

                    }
                    if (Request.QueryString["TactionNo"] != null)
                    {
                        int TikitNo = Convert.ToInt32(Request.QueryString["TactionNo"]);

                        panChat.Visible = true;
                        listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.GroupBy == "Ticket" && p.MasterCODE == TikitNo);
                        listChet.DataBind();

                        ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.GroupBy == "Ticket" && p.MasterCODE == TikitNo);
                        ListHistoy.DataBind();

                        ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MasterCODE == TikitNo).OrderByDescending(p => p.UPDTTIME);
                        ltsRemainderNotes.DataBind();
                    }


                }
            }
        }
        public void ToalCoun()
        {
            if (Session["USER"] != null)
            {
                int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (RoleID == 4)
                {
                    int New = DB.CRMMainActivities.Where(p => p.MyStatus == "NEW").Count();
                    leblANew.Text = New.ToString();
                    int Pending = DB.CRMMainActivities.Where(p => p.MyStatus == "Pending").Count();
                    leblANew1.Text = Pending.ToString();
                    int Completed = DB.CRMMainActivities.Where(p => p.MyStatus == "Completed").Count();
                    leblANew2.Text = Completed.ToString();
                    int Progress = DB.CRMMainActivities.Where(p => p.MyStatus == "Progress").Count();
                    leblANew3.Text = Progress.ToString();
                    int Closed = DB.CRMMainActivities.Where(p => p.MyStatus == "Closed").Count();
                    leblANew4.Text = Closed.ToString();
                    int Delivered = DB.CRMMainActivities.Where(p => p.MyStatus == "Delivered").Count();
                    leblANew5.Text = Delivered.ToString();
                }
                else
                {
                    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == "NEW").Count();
                    leblANew.Text = New.ToString();
                    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == "Pending").Count();
                    leblANew1.Text = Pending.ToString();
                    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == "Completed").Count();
                    leblANew2.Text = Completed.ToString();
                    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == "Progress").Count();
                    leblANew3.Text = Progress.ToString();
                    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == "Closed").Count();
                    leblANew4.Text = Closed.ToString();
                    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == "Delivered").Count();
                    leblANew5.Text = Delivered.ToString();
                }
                int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.MyStatus != "Close").Count();
                // lblAttachment.Text = SeqCount1.ToString();
            }
        }
        public void bindModule()
        {
            if (Session["USER"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                string UIN = ((USER_MST)Session["USER"]).LOGIN_ID;
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                if (RoleID == 4)
                {
                    ltsProduct.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == 0);
                    ltsProduct.DataBind();
                }
                else
                {
                    ltsProduct.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TID && p.Parent_Module_id == 0);
                    ltsProduct.DataBind();
                }
            }
        }
        protected void lstmodule_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    Label lblHide = (Label)e.Item.FindControl("lblHide");
            //    int MID = Convert.ToInt32(lblHide.Text);
            //    ListView ltsProduct = (ListView)e.Item.FindControl("lstsubmodule");
            //    ltsProduct.DataSource = DB.ERP_WEB_MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == 360 && p.Parent_Module_id == MID);
            //    ltsProduct.DataBind();
            //}

        }
        public void BindData()
        {
            if (Session["USER"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                drpActivityName.DataSource = DB.REFTABLEs.Where(P => P.TenentID == TID && P.REFTYPE == "DMS" && P.REFSUBTYPE == "REFERENCE").OrderBy(p => p.REFNAME1);
                drpActivityName.DataTextField = "REFNAME1";
                drpActivityName.DataValueField = "REFID";
                drpActivityName.DataBind();
                drpActivityName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        public void BindRemainderNote()
        {
            if (Session["USER"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                if (RoleID == 4)
                {
                    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                    ltsRemainderNotes.DataBind();
                }
                else
                {
                    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                    ltsRemainderNotes.DataBind();
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (Session["USER"] != null)
                    {
                        string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                        int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                        int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);

                        Database.CRMMainActivity objCRMMainActivities = new Database.CRMMainActivity();
                        // objtbl_DMSTikit_Mst.ID = DB.tbl_DMSTikit_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSTikit_Mst.Max(p => p.ID) + 1) : 1;
                        objCRMMainActivities.TenentID = TID;
                        objCRMMainActivities.COMPID = CID;
                        objCRMMainActivities.Prefix = "ONL";
                        objCRMMainActivities.MasterCODE = DB.CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Max(p => p.MasterCODE) + 1) : 1;
                        objCRMMainActivities.RouteID = 1;
                        //objCRMMainActivities.ACTIVITYE = "DMS";
                        objCRMMainActivities.ACTIVITYA = "Reference";
                        objCRMMainActivities.ACTIVITYA2 = "TKT";
                        objCRMMainActivities.USERCODE = UID;
                        objCRMMainActivities.ACTIVITYE = "Ticket";

                        objCRMMainActivities.Reference = drpActivityName.SelectedValue;
                        objCRMMainActivities.AMIGLOBAL = true;
                        objCRMMainActivities.MYPERSONNEL = false;
                        objCRMMainActivities.INTERVALDAYS = 1;
                        objCRMMainActivities.REPEATFOREVER = false;
                        objCRMMainActivities.REPEATTILL = DateTime.Now;
                        objCRMMainActivities.ESTCOST = DB.CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Max(p => p.ESTCOST) + 1) : 1;
                        objCRMMainActivities.GROUPCODE = Convert.ToInt32(drpSDepartment.SelectedValue);
                        objCRMMainActivities.USERCODEENTERED = drppriority.SelectedValue;
                        objCRMMainActivities.UPDTTIME = DateTime.Now;
                        objCRMMainActivities.USERNAME = strUName;
                        objCRMMainActivities.Remarks = txtMessage.Text;
                        objCRMMainActivities.Version = txtSubject.Text;
                        objCRMMainActivities.MyStatus = "NEW";
                        DB.CRMMainActivities.AddObject(objCRMMainActivities);
                        DB.SaveChanges();

                        Database.CRMActivity objCRMActivity = new Database.CRMActivity();
                        objCRMActivity.COMPID = CID;
                        objCRMActivity.TenentID = TID;
                        objCRMActivity.MasterCODE = objCRMMainActivities.MasterCODE;
                        objCRMActivity.REFTYPE = "DMS";
                        objCRMActivity.REFSUBTYPE = "Reference";
                        objCRMActivity.PerfReferenceNo = "0";
                        objCRMActivity.ACTIVITYTYPE = "TKT";
                        objCRMActivity.MyLineNo = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.MyLineNo) + 1) : 1;
                        objCRMActivity.GroupBy = "Tikit";
                        //objCRMActivity.USERCODE = strUName;
                        //objCRMActivity.ReferenceNo = objCRMMainActivities.Reference;
                        objCRMActivity.NextRefNo = "N";
                        objCRMActivity.AMIGLOBAL = "Y";
                        objCRMActivity.MYPERSONNEL = "N";
                        objCRMActivity.ActivityPerform = objCRMMainActivities.Remarks;
                        objCRMActivity.REMINDERNOTE = objCRMMainActivities.Version;
                        objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.ESTCOST);
                        objCRMActivity.GROUPCODE = "1";
                        objCRMActivity.USERCODEENTERED = strUName;
                        objCRMActivity.UPDTTIME = DateTime.Now;
                        objCRMActivity.USERNAME = strUName;
                        objCRMActivity.RouteID = "Ticket";
                        objCRMActivity.MyStatus = "NEW";
                        objCRMActivity.Active = "Y";
                        objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
                        objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
                        objCRMActivity.Version = strUName + " Reply";
                        DB.CRMActivities.AddObject(objCRMActivity);
                        DB.SaveChanges();
                        clen();
                        ToalCoun();
                        BindRemainderNote();
                        string contant = " <span style = \"font-family:Arial;font-size:10pt\">Hello <b>" + "</b>,<br /><br />Youer Tikit Number Is   " + objCRMMainActivities.ESTCOST + "<br /><br /><a style = \"color:#22BCE5\" href = \"http://www.digital53.com\"> Digital Edge Solutions</a><br /><br />Digital Edge Solutions has pioneered the way in solving client’s real world of Information Technologies With a quality of services and sound technology. Digital Edge provides complete wide range of IT Solutions containing Website Design, Website re-Design, Web Development, Software Development, Outsourced Software Development, Custom Software Development ,E-Commerce Website Development ,Cloud Computer ,Search Engine Optimization(SEO) ,Outsourcing and many more...<br />Thanks<br />Support Team</span>";
                        string email2 = "johar@writeme.com";
                        sendEmail(contant, email2);
                        scope.Complete();
                        int TickitNo = Convert.ToInt32(objCRMMainActivities.ESTCOST);
                        string display = "Youer Ticket Number Is " + TickitNo;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }
        public void sendEmail(string body, string email)
        {
            try
            {
                //if (String.IsNullOrEmpty(email))
                //return;

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.Subject = "Thanks you for Visiting our site..";
                msg.From = new System.Net.Mail.MailAddress("digital53marketing@gmail.com");
                msg.To.Add(new System.Net.Mail.MailAddress(email));
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.Body = body;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
                smpt.UseDefaultCredentials = false;
                smpt.Host = "smtp.gmail.com";
                smpt.Port = 587;
                smpt.EnableSsl = true;
                smpt.Credentials = new System.Net.NetworkCredential("digital53marketing@gmail.com", "Digital123$");
                smpt.Send(msg);
            }
            catch
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clen();
            Response.Redirect("AdminIndex.aspx");
        }
        public void clen()
        {
            txtComent.Text = txtMessage.Text = txtSubject.Text = "";
            drpActivityName.SelectedIndex = 0;
            drppriority.SelectedIndex = 0;
            drpSDepartment.SelectedIndex = 0;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
                int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
                if (RoleID == 4)
                {
                    //Database.ACM_CRMActivities objCRMActivity = new Database.ACM_CRMActivities();
                    //objCRMActivity.COMPID = CID;
                    //objCRMActivity.TenentID = TID;
                    //objCRMActivity.MasterCODE = tiki;
                    //objCRMActivity.REFTYPE = "Eco";
                    //objCRMActivity.REFSUBTYPE = "Reference";
                    //objCRMActivity.ACTIVITYTYPE = "TKT";
                    //objCRMActivity.MyLineNo = TID;
                    ////objCRMActivity.USERCODE = strUName;
                    ////objCRMActivity.ReferenceNo = "Anser";
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
                    //objCRMActivity.MyStatus = drpStatus.SelectedValue;
                    //objCRMActivity.Active = "Y";
                    //objCRMActivity.PerfReferenceNo = "";
                    //objCRMActivity.ToColumn = DB.ACM_CRMActivities.Count() > 0 ? Convert.ToInt32(DB.ACM_CRMActivities.Max(p => p.ToColumn) + 1) : 1;
                    //objCRMActivity.FromColumn = DB.ACM_CRMActivities.Count() > 0 ? Convert.ToInt32(DB.ACM_CRMActivities.Max(p => p.FromColumn) + 1) : 1;
                    //objCRMActivity.Version = "Suppot Reply";
                    //DB.ACM_CRMActivities.AddObject(objCRMActivity);
                    //DB.SaveChanges();


                    int MsterCose = tiki;
                    int TenentID = TID;
                    int COMPID = 361;
                    int MasterCODE = tiki;
                    int LinkMasterCODE = tiki;
                    int MenuID = 0;
                    int ActivityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
                    string ACTIVITYTYPE = "Ticket";
                    string REFTYPE = "Ticket";
                    string REFSUBTYPE = "CRM";
                    string PerfReferenceNo = "CRM";
                    string EarlierRefNo = "A";
                    string NextUser = strUName;
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
                    string USERNAME = strUName;
                    int CRUP_ID = 0;
                    DateTime InitialDate = DateTime.Now;
                    DateTime DeadLineDate = DateTime.Now;
                    string RouteID = "A";
                    string Version1 = "Support Reply";
                    int Type = 0;
                    string MyStatus = drpStatus.SelectedValue;
                    string GroupBy = "Ticket";
                    int DocID = 0;
                    int ToColumn = 0;
                    int FromColumn = 0;
                    string Active = "Y";
                    int MainSubRefNo = 0;
                    string URL = Request.Url.AbsolutePath;
                    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL);
                    //int SID = 1240103;
                    //string BName = "Suppot Reply";
                    //string P2 = "aa";
                    //string P3 = "aa";
                    //inserCrmproghw(TID, SID, BName, P2, P3, tiki);


                    //Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.MasterCODE == tiki);
                    //objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
                    //DB.SaveChanges();
                    string Status = drpStatus.SelectedValue;
                    getStatusAll(Status);
                    getCommunicatinData();
                    ClenCat();

                }
                else
                {
                    int MsterCose = tiki;
                    int TenentID = TID;
                    int COMPID = CID;
                    int MasterCODE = tiki;
                    int LinkMasterCODE = 1;
                    int MenuID = 0;
                    int ActivityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
                    string ACTIVITYTYPE = "Ticket";
                    string REFTYPE = "Ticket";
                    string REFSUBTYPE = "CRM";
                    string PerfReferenceNo = "CRM";
                    string EarlierRefNo = "A";
                    string NextUser = strUName;
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
                    string USERNAME = strUName;
                    int CRUP_ID = 0;
                    DateTime InitialDate = DateTime.Now;
                    DateTime DeadLineDate = DateTime.Now;
                    string RouteID = "A";
                    string Version1 = "Suppot Reply";
                    int Type = 0;
                    string MyStatus = drpStatus.SelectedValue;
                    string GroupBy = "Ticket";
                    int DocID = 0;
                    int ToColumn = 0;
                    int FromColumn = 0;
                    string Active = "Y";
                    int MainSubRefNo = 0;
                    string URL = Request.Url.AbsolutePath;
                    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL);
                    //int SID = 1240103;
                    //string BName = "Suppot Reply";
                    //string P2 = "aa";
                    //string P3 = "aa";
                    //inserCrmproghw(TID, SID, BName, P2, P3, tiki);


                    //Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.MasterCODE == tiki);
                    //objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
                    //DB.SaveChanges();
                    string Status = drpStatus.SelectedValue;
                    getStatusAll(Status);
                    getCommunicatinData();
                    ClenCat();

                }
            }
        }

        protected void btnNewTickit_Click(object sender, EventArgs e)
        {
            pnlTicki.Visible = true;
        }

        protected void lstmodule_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void ltsProduct_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (Session["USER"] != null)
            {
                if (e.CommandName == "linModul123")
                {
                    if (e.Item.ItemType == ListViewItemType.DataItem)
                    {
                        int MID = Convert.ToInt32(e.CommandArgument);
                        int TIDM = DB.MODULE_MST.Single(p => p.Module_Id == MID).TenentID;
                        lbltenetid.Text = TIDM.ToString();

                        Label lblHide = (Label)e.Item.FindControl("lblHide");
                        pnlTags.Visible = true;
                        lblTagtitle.Text = lblHide.Text;
                        ViewState["ComoniName"] = lblHide.Text;
                        lblCompniName.Text = ViewState["ComoniName"].ToString();
                        int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                        if (RoleID == 4)
                        {
                            int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus != "Close").Count();
                            //  lblAttachment.Text = SeqCount1.ToString();
                            int New = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus == "NEW").Count();
                            lblNew.Text = New.ToString();
                            int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus == "Pending").Count();
                            lblPending.Text = Pending.ToString();
                            int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus == "Completed").Count();
                            lblCompleted.Text = Completed.ToString();
                            int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus == "Progress").Count();
                            lblProgress.Text = Progress.ToString();
                            int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus == "Closed").Count();
                            lblClosed.Text = Closed.ToString();
                            int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.ACTIVITYE == "Ticket" && p.MyStatus == "Delivered").Count();
                            lblDelivred.Text = Delivered.ToString();
                            //else if (lblHide.Text == "HRM CMBC")
                            //{
                            //    int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus != "Close").Count();
                            //    // lblAttachment.Text = SeqCount1.ToString();
                            //    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "NEW").Count();
                            //    lblNew.Text = New.ToString();
                            //    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Pending").Count();
                            //    lblPending.Text = Pending.ToString();
                            //    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Completed").Count();
                            //    lblCompleted.Text = Completed.ToString();
                            //    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Progress").Count();
                            //    lblProgress.Text = Progress.ToString();
                            //    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Closed").Count();
                            //    lblClosed.Text = Closed.ToString();
                            //    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Delivered").Count();
                            //    lblDelivred.Text = Delivered.ToString();
                            //}
                            //else if (lblHide.Text == "CRM CMBC")
                            //{
                            //    int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus != "Close").Count();
                            //    //  lblAttachment.Text = SeqCount1.ToString();
                            //    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "NEW").Count();
                            //    lblNew.Text = New.ToString();
                            //    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Pending").Count();
                            //    lblPending.Text = Pending.ToString();
                            //    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Completed").Count();
                            //    lblCompleted.Text = Completed.ToString();
                            //    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Progress").Count();
                            //    lblProgress.Text = Progress.ToString();
                            //    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Closed").Count();
                            //    lblClosed.Text = Closed.ToString();
                            //    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Delivered").Count();
                            //    lblDelivred.Text = Delivered.ToString();
                            //}
                            //else if (lblHide.Text == "Ecomm")
                            //{
                            //    int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus != "Close").Count();
                            //    // lblAttachment.Text = SeqCount1.ToString();
                            //    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "NEW").Count();
                            //    lblNew.Text = New.ToString();
                            //    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Pending").Count();
                            //    lblPending.Text = Pending.ToString();
                            //    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Completed").Count();
                            //    lblCompleted.Text = Completed.ToString();
                            //    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Progress").Count();
                            //    lblProgress.Text = Progress.ToString();
                            //    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Closed").Count();
                            //    lblClosed.Text = Closed.ToString();
                            //    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.MyStatus == "Delivered").Count();
                            //    lblDelivred.Text = Delivered.ToString();
                            //}
                        }
                        else
                        {

                            int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus != "Close").Count();
                            //  lblAttachment.Text = SeqCount1.ToString();
                            int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "NEW").Count();
                            lblNew.Text = New.ToString();
                            int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Pending").Count();
                            lblPending.Text = Pending.ToString();
                            int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Completed").Count();
                            lblCompleted.Text = Completed.ToString();
                            int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Progress").Count();
                            lblProgress.Text = Progress.ToString();
                            int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Closed").Count();
                            lblClosed.Text = Closed.ToString();
                            int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Delivered").Count();
                            lblDelivred.Text = Delivered.ToString();

                            //else if (lblHide.Text == "HRM CMBC")
                            //{
                            //    int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus != "Close").Count();
                            //    //  lblAttachment.Text = SeqCount1.ToString();
                            //    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "NEW").Count();
                            //    lblNew.Text = New.ToString();
                            //    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Pending").Count();
                            //    lblPending.Text = Pending.ToString();
                            //    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Completed").Count();
                            //    lblCompleted.Text = Completed.ToString();
                            //    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Progress").Count();
                            //    lblProgress.Text = Progress.ToString();
                            //    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Closed").Count();
                            //    lblClosed.Text = Closed.ToString();
                            //    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Delivered").Count();
                            //    lblDelivred.Text = Delivered.ToString();
                            //}
                            //else if (lblHide.Text == "CRM CMBC")
                            //{
                            //    int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus != "Close").Count();
                            //    //  lblAttachment.Text = SeqCount1.ToString();
                            //    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "NEW").Count();
                            //    lblNew.Text = New.ToString();
                            //    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Pending").Count();
                            //    lblPending.Text = Pending.ToString();
                            //    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Completed").Count();
                            //    lblCompleted.Text = Completed.ToString();
                            //    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Progress").Count();
                            //    lblProgress.Text = Progress.ToString();
                            //    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Closed").Count();
                            //    lblClosed.Text = Closed.ToString();
                            //    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Delivered").Count();
                            //    lblDelivred.Text = Delivered.ToString();
                            //}
                            //else if (lblHide.Text == "Ecomm")
                            //{
                            //    int SeqCount1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus != "Close").Count();
                            //    //  lblAttachment.Text = SeqCount1.ToString();
                            //    int New = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "NEW").Count();
                            //    lblNew.Text = New.ToString();
                            //    int Pending = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Pending").Count();
                            //    lblPending.Text = Pending.ToString();
                            //    int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Completed").Count();
                            //    lblCompleted.Text = Completed.ToString();
                            //    int Progress = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Progress").Count();
                            //    lblProgress.Text = Progress.ToString();
                            //    int Closed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Closed").Count();
                            //    lblClosed.Text = Closed.ToString();
                            //    int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "Ticket" && p.USERCODE == UIN && p.MyStatus == "Delivered").Count();
                            //    lblDelivred.Text = Delivered.ToString();
                            //}
                        }

                    }

                }
            }
        }

        protected void ltsRemainderNotes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (Session["USER"] != null)
            {
                if (e.CommandName == "btnclick123")
                {
                    if (e.Item.ItemType == ListViewItemType.DataItem)
                    {
                        int linkID = Convert.ToInt32(e.CommandArgument);
                        Label tikitID = (Label)e.Item.FindControl("tikitID");
                        int Tikitno = Convert.ToInt32(tikitID.Text);
                        ViewState["TIkitNumber"] = Tikitno;
                        panChat.Visible = true;

                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        string UIN = ((USER_MST)Session["USER"]).LOGIN_ID;
                        int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                        if (RoleID == 4)
                        {
                            listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                            listChet.DataBind();

                            ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                            ListHistoy.DataBind();
                        }
                        else
                        {
                            listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                            listChet.DataBind();

                            ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                            ListHistoy.DataBind();
                        }
                    }
                }
            }
        }
        public void getCommunicatinData()
        {
            if (Session["USER"] != null)
            {
                int Tikitno = Convert.ToInt32(ViewState["TIkitNumber"]);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                if (RoleID == 4)
                {
                    listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                    listChet.DataBind();
                    ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                    ListHistoy.DataBind();
                }
                else
                {
                    listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                    listChet.DataBind();
                    ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                    ListHistoy.DataBind();

                }
            }
        }

        public void ClenCat()
        {
            txtComent.Text = "";
        }
        public void getStatus(string Status)
        {
            if (Session["USER"] != null)
            {
                lblCompniName.Text = "";
                int TIDM = Convert.ToInt32(lbltenetid.Text);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                if (RoleID == 4)
                    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TIDM && p.MyStatus == Status && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                else
                    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == Status && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                ltsRemainderNotes.DataBind();
                ViewState["Status"] = " > " + Status;

                lblCompniName.Text = ViewState["ComoniName"] + ViewState["Status"].ToString();
                ViewState["Status"] = null;
            }
        }

        protected void linkNew_Click(object sender, EventArgs e)
        {

            getStatus("NEW");

        }
        protected void linkPending_Click(object sender, EventArgs e)
        {
            getStatus("Pending");

        }

        protected void linkCOmplet_Click(object sender, EventArgs e)
        {
            getStatus("Completed");

        }

        protected void linkProgress_Click(object sender, EventArgs e)
        {
            getStatus("Progress");


        }

        protected void linkClosed_Click(object sender, EventArgs e)
        {
            getStatus("Closed");

        }

        protected void linkDeliverd_Click(object sender, EventArgs e)
        {
            getStatus("Delivered");

        }

        public void getStatusAll(string StatusAll)
        {
            if (Session["USER"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                if (RoleID == 4)
                {
                    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.MyStatus == StatusAll && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                    ltsRemainderNotes.DataBind();
                }
                else
                {
                    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                    ltsRemainderNotes.DataBind();
                }
            }
        }

        protected void linkAllNew_Click(object sender, EventArgs e)
        {
            getStatusAll("NEW");
        }

        protected void linkAllPending_Click(object sender, EventArgs e)
        {
            getStatusAll("Pending");
        }

        protected void linkallComplet_Click(object sender, EventArgs e)
        {
            getStatusAll("Completed");
        }

        protected void linkAllProgress_Click(object sender, EventArgs e)
        {
            getStatusAll("Progress");
        }

        protected void linkAllClose_Click(object sender, EventArgs e)
        {
            getStatusAll("Closed");
        }

        protected void linkAllDeliverd_Click(object sender, EventArgs e)
        {
            getStatusAll("Delivered");
        }

        protected void btnclick_Click(object sender, EventArgs e)
        {
            panChat.Visible = true;
        }
        public void inserCrmproghw(int TID, int SID, string BName, string P2, string P3, int TRction)
        {
            // ACM_CRMMainActivities obj = DB1.ACM_CRMMainActivities.Single(p => p.TenentID == TID  && p.ID == TRction);
            int ACID = Convert.ToInt32(DB.CRMActivities.Max(p => p.MyLineNo));
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
        public string GetUName(int UID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == UID).Count() > 0)
            {
                string UName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
                return UName;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void lbllblattachments2_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
            int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string UID = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
            string Type = "Ticket";

            string Url = "../CRM/AttachmentMst.aspx?AttachID=" + tiki + "&DID=" + Type + "&RefNo=" + 11058 + "&UID=" + uid1;
            string s = "window.open('" + Url + "', 'popup_window', 'width=950,height=425,left=100,top=100,resizable=yes');";
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", s, true);
        }

        protected void btnAttch_Click(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                if (ViewState["TIkitNumber"] != null)
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
                    List<Database.tbl_DMSAttachmentMst> ttachOBJ = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.CATID == uid1 && p.AttachmentById == tiki).ToList();
                    int Coun = ttachOBJ.Count();
                    btnAttch.Text = Coun.ToString();
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            getCommunicatinData();
            UpdatePanel1.Update();
        }



    }
}