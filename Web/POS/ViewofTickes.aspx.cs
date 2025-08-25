using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Web.POS
{
    public partial class ViewofTickes : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               // DrpSubCat.SelectedIndex = 0;
                lblUser.Text = "Pending Issues";
                DateTime dt = DateTime.Now;
                txtdates.Text = dt.ToString();
                txtdates.Enabled = false;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                if (Session["USER"] == null || Session["USER"] == "0")
                {
                    Session.Abandon();
                    Response.Redirect("/ACM/SessionTimeOut.aspx");
                    drpinvestigation.SelectedIndex = 0;
                    
                }

                if(Request.QueryString!=null)
                {
                    if (Request.QueryString["status"] != null && Request.QueryString["status"]=="pending")
                    {
                        int admin = 0;
                        int UIR = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                        if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                            admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);



                        if (admin == UIR)
                        {
                            
                            BindData();
                            //ToalCoun();
                            ViewState["StatusAll"] = "pending";
                            //ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "pending" && p.ACTIVITYE == "helpdesk").OrderByDescending(p => p.UploadDate);
                            //ltsRemainderNotes.DataBind();
                           // PnlBindTick.Visible = true;
                            pnlTicki.Visible = true;                           
                            return;
                            
                        }
                    }                    
                }


                int adm = 0;
                int URI = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                    adm = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);



                    BindData();
                    //ToalCoun();
                    ViewState["StatusAll"] = "pending";
                    //ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "pending" && p.ACTIVITYE == "helpdesk").OrderByDescending(p => p.UploadDate);
                    //ltsRemainderNotes.DataBind();
                   // PnlBindTick.Visible = true;
                    pnlTicki.Visible = true;                    
                    return;                   
                 
                BindData();
                Feedbackyear();
                panChat.Visible = false;
                //pnlinfo.Visible = false;
                //pnlGroupChat.Visible = false;
                string UIN = ((USER_MST)Session["USER"]).FIRST_NAME;


                pnlTicki.Visible = true;
                //ToalCoun();
                if (Request.QueryString["ACT"] != null)
                {
                    int ACTID = Convert.ToInt32(Request.QueryString["ACT"]);
                    string STDD = Request.QueryString["STD"].ToString();
                    ViewState["TIkitNumber"] = ACTID;
                    //getStatusQUERY(STDD);
                    getCommunicatinData();
                    panChat.Visible = true;
                }

                //if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == uid1).Count() > 0)
                //{
                //    Database.CRMMainActivity MainObj = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.USERCODE == uid1);
                //    int tiki = Convert.ToInt32(MainObj.MasterCODE);

                //List<Database.tbl_DMSAttachmentMst> ttachOBJ = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.CATID == uid1 && p.AttachmentById == tiki).ToList();
                //int Coun = ttachOBJ.Count();
                //btnAttch.Text = Coun.ToString();
                //}

            }
        }

        public void Feedbackyear()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select distinct Year(UploadDate) as showyear  from CRMMainActivities where TenentID =" + TID + " and UploadDate is not NULL";
            DataTable dt = DataCon.GetDataTable(sql);
            drpyear1.DataSource = dt;
            drpyear1.DataTextField = "showyear";
            drpyear1.DataValueField = "showyear";
            drpyear1.DataBind();
            drpyear1.Items.Insert(0, new ListItem("--All Years--", "0"));
        }
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);

            var Department = (from Dept in DB.DeptITSupers.Where(p => p.TenentID == TID).OrderBy(p=>p.DeptName)
                              select new
                              {
                                  Dep1 = Dept.DeptName + " - " + Dept.SuperVisorName,
                                  Dep2 = Dept.DeptID
                              }
                );

            drpSDepartment.DataSource = Department;//DB.DeptITSupers.Where(p => p.TenentID == TID);
            drpSDepartment.DataTextField = "Dep1"; //"DeptName";
            drpSDepartment.DataValueField = "Dep2"; //"DeptID";
            drpSDepartment.DataBind();
            drpSDepartment.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpusermt.DataSource = DB.USER_MST.Where(p => p.TenentID == TID).OrderBy(p=>p.FIRST_NAME);
            drpusermt.DataTextField = "FIRST_NAME";
            drpusermt.DataValueField = "USER_ID";
            drpusermt.DataBind();
            drpusermt.Items.Insert(0, new ListItem("-- Select --", "0"));

            /*drpFeedbackype.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "Feedback").OrderBy(p=>p.REFNAME1);
            drpFeedbackype.DataTextField = "REFNAME1";
            drpFeedbackype.DataValueField = "REFID";
            drpFeedbackype.DataBind();
            drpFeedbackype.Items.Insert(0, new ListItem("-- Select --", "0"));*/

            DrpPhysicalLocation.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").OrderBy(p=>p.REFNAME1);
            DrpPhysicalLocation.DataTextField = "REFNAME1";
            DrpPhysicalLocation.DataValueField = "REFID";
            DrpPhysicalLocation.DataBind();
            DrpPhysicalLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
            //Main Category
            DrpTCatSubCate.DataSource = DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk").OrderBy(p=>p.CATNAME);
            DrpTCatSubCate.DataTextField = "CATNAME";
            DrpTCatSubCate.DataValueField = "CATID";
            DrpTCatSubCate.DataBind();
           
            //Sub Category
            int CID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
          
            List<ICCATSUBCAT> list = DB.ICCATSUBCATs.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.CATID == CID).ToList();
            List<ICSUBCATEGORY> tempList = new List<ICSUBCATEGORY>();
            foreach (ICCATSUBCAT item in list)
            {
                var obj = DB.ICSUBCATEGORies.FirstOrDefault(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == item.SUBCATID);
                tempList.Add(obj);

            }
            DrpSubCat.DataSource = DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk").OrderBy(p => p.SUBCATNAME);
            DrpSubCat.DataTextField = "SUBCATNAME";
            DrpSubCat.DataValueField = "SUBCATID";
            DrpSubCat.DataBind();
            //if (list.Count <= 0)
            DrpSubCat.Items.Insert(0, new ListItem("-- Select --", "0"));

            //var result2 = (from pm in DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE == "Ticket" && p.Active == "1" && p.PARENT_CATID != 0)
            //               select new
            //               {
            //                   p1 = pm.CAT_DESCRIPTION + " - " + pm.CAT_NAME1,
            //                   p2 = pm.CATID// + "." + pm.PARENT_CATID 
            //               });
            //DrpTCatSubCate.DataSource = result2;
            //DrpTCatSubCate.DataTextField = "p1";
            //DrpTCatSubCate.DataValueField = "p2";
            //DrpTCatSubCate.DataBind();
            //DrpTCatSubCate.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
       
       
      
  
        public void maxFeedbackID()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int maxid = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
            DateTime startdate = DateTime.Now;
            string stdate = startdate.ToString("yyyy-MM");
           
        }
      
        public void sendEmail(string body, string email)
        {
            //if (String.IsNullOrEmpty(email))
            //    return;
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //msg.Subject = "Thanks you for Visiting our site..";
            //msg.From = new System.Net.Mail.MailAddress("supportteam@digital53.com");
            //msg.To.Add(new System.Net.Mail.MailAddress(email));
            //msg.BodyEncoding = System.Text.Encoding.UTF8;
            //msg.Body = body;
            //msg.IsBodyHtml = true;
            //msg.Priority = MailPriority.High;
            //System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            //smpt.UseDefaultCredentials = false;
            //smpt.Host = "mail.ayosoftech.com";
            //smpt.Port = 587;
            //smpt.EnableSsl = false;
            //smpt.Credentials = new System.Net.NetworkCredential("info@ayosoftech.com", "P@ssw0rd123");
            //smpt.Send(msg);
        }
        public void clen()
        {
            txtpatientname.Text = "";
            txtMRN.Text = "";
            txtstaffname.Text = "";
            chklist.Checked = false;
            chkirno.Checked = false;
            txtComent.Text = txtMessage.Text = txtSubject.Text = "";
          //  drpActivityName.SelectedValue = "Help Desk";
           // drpFeedbackype.SelectedIndex = 0;
            DrpPhysicalLocation.SelectedIndex = 0;
            DrpTCatSubCate.SelectedIndex = 0;
            DrpSubCat.SelectedIndex = 0;
            drpSDepartment.SelectedIndex = 0;
            txtcontact.Text = "";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{

            if (btnSave.Text == "Submit")
            {

            
                string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);

                Database.CRMMainActivity objCRMMainActivities = new Database.CRMMainActivity();
                // objtbl_DMSTikit_Mst.ID = DB.tbl_DMSTikit_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSTikit_Mst.Max(p => p.ID) + 1) : 1;
                objCRMMainActivities.TenentID = TID;
                objCRMMainActivities.COMPID = CID;
                objCRMMainActivities.Prefix = "ONL";
                objCRMMainActivities.MasterCODE = DB.CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Max(p => p.MasterCODE) + 1) : 1;
                objCRMMainActivities.LinkMasterCODE = 1;
                objCRMMainActivities.LocationID = 1;
                objCRMMainActivities.MyID = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                objCRMMainActivities.RouteID = 1;
                objCRMMainActivities.USERCODE = UID;
                objCRMMainActivities.ACTIVITYA = "Reference";
                objCRMMainActivities.ACTIVITYA2 = "TKT";
                objCRMMainActivities.ACTIVITYE = "helpdesk";
                objCRMMainActivities.Reference = "helpdesk";
                objCRMMainActivities.AMIGLOBAL = true;
                objCRMMainActivities.MYPERSONNEL = false;
                objCRMMainActivities.INTERVALDAYS = 1;
                objCRMMainActivities.REPEATFOREVER = false;
                objCRMMainActivities.REPEATTILL = DateTime.Now;
                objCRMMainActivities.ESTCOST = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.ESTCOST) + 1) : 1;
                objCRMMainActivities.USERCODEENTERED = "";               
                objCRMMainActivities.UPDTTIME =Convert.ToDateTime(txtdates.Text);
                objCRMMainActivities.USERNAME = strUName;
                objCRMMainActivities.Remarks = txtMessage.Text;
                objCRMMainActivities.Version = txtSubject.Text;
                objCRMMainActivities.MyStatus = "Pending";
                objCRMMainActivities.UploadDate = Convert.ToDateTime(txtdates.Text);
                objCRMMainActivities.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
                //objCRMMainActivities.TickFeedbackype = Convert.ToInt32(drpFeedbackype.SelectedValue);
                objCRMMainActivities.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
                objCRMMainActivities.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
                objCRMMainActivities.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
                int Deptidd = Convert.ToInt32(drpSDepartment.SelectedValue);
                int Suppid = Convert.ToInt32(DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Deptidd).SuperVisorID);
                objCRMMainActivities.Emp_ID = Suppid;
                string ITGroupUser = DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == Suppid).Count() == 1 ? DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == Suppid).userID : "0";

                int ITGroupID = DB.TBLGROUPs.Where(p => p.TenentID == TID && p.USERCODE == ITGroupUser).Count() == 1 ? Convert.ToInt32(DB.TBLGROUPs.Single(p => p.TenentID == TID && p.USERCODE == ITGroupUser).ITGROUPID) : 0;
                objCRMMainActivities.GROUPCODE = ITGroupID;
                objCRMMainActivities.REMINDERNOTE = txtComent.Text;
                objCRMMainActivities.Patient_Name = txtpatientname.Text;
                if (txtMRN.Text != "")
                {
                    objCRMMainActivities.MRN = txtMRN.Text;
                }
                objCRMMainActivities.StaffInvoice =true;
                objCRMMainActivities.IRDone = true;
                objCRMMainActivities.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
                //objCRMMainActivities.FeedbackNumber = objCRMMainActivities.MasterCODE.ToString();
                objCRMMainActivities.Contact =  txtcontact.Text;
                objCRMMainActivities.IncidentReport = true;
                String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = 1" + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
                String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFNAME1;
                String tablename = "CRMMainActivity";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFID;
                objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno); ;
                DB.CRMMainActivities.AddObject(objCRMMainActivities);
                DB.SaveChanges();             

                

                lblMsg.Text = "Data Save Successfully";
                pnlSuccessMsg.Visible = true;

                Database.CRMActivity objCRMActivity = new Database.CRMActivity();
                objCRMActivity.TenentID = TID;
                objCRMActivity.COMPID = CID;
                objCRMActivity.Prefix = "ONL";
                objCRMActivity.MasterCODE = objCRMMainActivities.MasterCODE;
                objCRMActivity.MyLineNo = TID;
                int activityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
                objCRMActivity.ActivityID = activityID;
            objCRMActivity.LocationID = 0;
                objCRMActivity.LinkMasterCODE = 1;
                objCRMActivity.MenuID = Convert.ToInt32(0);
                objCRMActivity.ACTIVITYTYPE = "TKT";
                objCRMActivity.REFTYPE = "DMS";
                objCRMActivity.REFSUBTYPE = "Reference";
                objCRMActivity.PerfReferenceNo = objCRMMainActivities.Reference;
                objCRMActivity.NextRefNo = "N";
                objCRMActivity.GroupBy = "helpdesk";
                objCRMActivity.USERCODE = UID.ToString();
                objCRMActivity.AMIGLOBAL = "Y";
                objCRMActivity.MYPERSONNEL = "N";
                objCRMActivity.ActivityPerform = objCRMMainActivities.Remarks;
                objCRMActivity.REMINDERNOTE = txtComent.Text;
                objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.ESTCOST);
                objCRMActivity.GROUPCODE = drpSDepartment.SelectedValue;
                objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.GROUPCODE);
                objCRMActivity.USERCODEENTERED = strUName;
                objCRMActivity.UPDTTIME = Convert.ToDateTime(txtdates.Text);
                objCRMActivity.UploadDate = Convert.ToDateTime(txtdates.Text);
                objCRMActivity.InitialDate = DateTime.Now;
                objCRMActivity.DeadLineDate = DateTime.Now;
                objCRMActivity.USERNAME = strUName;
                objCRMActivity.RouteID = "helpdesk";
                objCRMActivity.MyStatus = "Pending";
                objCRMActivity.Active = "Y";
                objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
                objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
                objCRMActivity.Version = strUName;

                objCRMActivity.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
                //objCRMActivity.TickFeedbackype = Convert.ToInt32(drpFeedbackype.SelectedValue);
                objCRMActivity.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
                objCRMActivity.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
                objCRMActivity.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
                objCRMActivity.Patient_Name = txtpatientname.Text;
                if (txtMRN.Text != "")
                {
                    objCRMActivity.MRN = txtMRN.Text;
                }
                objCRMActivity.StaffInvoice = true;
                objCRMActivity.IRDone = true;
                objCRMActivity.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
                //objCRMActivity.FeedbackNumber = objCRMMainActivities.MasterCODE.ToString();
                objCRMActivity.Contact = txtcontact.Text;
                objCRMActivity.IncidentReport = true;
                objCRMActivity.aspcomment = 1;
                objCRMActivity.CRUP_ID = objCRMMainActivities.CRUP_ID;

                Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Reply", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
                Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Close", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
                Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Forward", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);

                DB.CRMActivities.AddObject(objCRMActivity);
                DB.SaveChanges();


                clen();
                 

                //scope.Complete();
                int TickitNo = Convert.ToInt32(objCRMMainActivities.MasterCODE);
                string display = "Your Ticket Number Is " + TickitNo;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
                //return;
                string msg = "Your Ticket Number Is " + TickitNo;
                string Function = "openModalsmall2('" + msg + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "openModalsmall2();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
                maxFeedbackID();
            //}
            }

            else if (btnSave.Text == "Update")
            {

                if (ViewState["Edit"] != null)
                {
                    string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                    int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID); objCRMMainActivities.RouteID = 1;
                    objCRMMainActivities.USERCODE = UID;
                    objCRMMainActivities.USERNAME = strUName;
                    objCRMMainActivities.Remarks = txtMessage.Text;
                    objCRMMainActivities.Version = txtSubject.Text;
                    objCRMMainActivities.MyStatus = "Pending";
                    objCRMMainActivities.UploadDate = Convert.ToDateTime(txtdates.Text);
                    objCRMMainActivities.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
                    //objCRMMainActivities.TickFeedbackype = Convert.ToInt32(drpFeedbackype.SelectedValue);
                    objCRMMainActivities.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
                    objCRMMainActivities.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
                    objCRMMainActivities.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
                    int Deptidd = Convert.ToInt32(drpSDepartment.SelectedValue);
                    int Suppid = Convert.ToInt32(DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Deptidd).SuperVisorID);
                    objCRMMainActivities.Emp_ID = Suppid;
                    objCRMMainActivities.Patient_Name = txtpatientname.Text;
                    if (txtMRN.Text != "")
                    {
                        objCRMMainActivities.MRN = txtMRN.Text;
                    }
                    objCRMMainActivities.StaffInvoice = true;
                    objCRMMainActivities.IRDone = true;
                    objCRMMainActivities.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
                    //objCRMMainActivities.FeedbackNumber = objCRMMainActivities.MasterCODE.ToString();
                    objCRMMainActivities.Contact = txtcontact.Text;
                    objCRMMainActivities.IncidentReport = true;
                    DB.SaveChanges();

                    String url = "Update record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = 1" + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
                    String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFNAME3;
                    String tablename = "CRMMainActivity";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                    int crupID = Convert.ToInt32(objCRMMainActivities.CRUP_ID);

                    Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);

                    lblMsg.Text = "Data Update Successfully";
                    pnlSuccessMsg.Visible = true;

                    Database.CRMActivity objCRMActivity = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);                         
                    
                    objCRMActivity.USERCODEENTERED = strUName;
                    objCRMActivity.UPDTTIME = Convert.ToDateTime(txtdates.Text);
                    objCRMActivity.UploadDate = Convert.ToDateTime(txtdates.Text);
                    objCRMActivity.InitialDate = DateTime.Now;
                    objCRMActivity.DeadLineDate = DateTime.Now;
                    objCRMActivity.USERNAME = strUName;
                    objCRMActivity.RouteID = "helpdesk";
                    objCRMActivity.MyStatus = "Pending";
                    objCRMActivity.Active = "Y";
                    objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
                    objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
                    objCRMActivity.Version = strUName;

                    objCRMActivity.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
                    //objCRMActivity.TickFeedbackype = Convert.ToInt32(drpFeedbackype.SelectedValue);
                    objCRMActivity.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
                    objCRMActivity.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
                    objCRMActivity.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
                    objCRMActivity.Patient_Name = txtpatientname.Text;
                    if (txtMRN.Text != "")
                    {
                        objCRMActivity.MRN = txtMRN.Text;
                    }
                    objCRMActivity.StaffInvoice = true;
                    objCRMActivity.IRDone = true;
                    objCRMActivity.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
                    //objCRMActivity.FeedbackNumber = objCRMMainActivities.MasterCODE.ToString();
                    objCRMActivity.Contact = txtcontact.Text;
                    objCRMActivity.IncidentReport = true;

                    DB.SaveChanges();

                    //String url1 = "Update record in CRMActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMActivity.MasterCODE + "MyLineNo = 2" + "LocationID = 1" + "LinkMasterCODE = 1 " + "ActivityID = 1" + "Prefix = ONL ";
                    //String evantname2 = "Update";
                    //String tablename3 = "CRMActivity";
                    //string loginUserId4 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    //Classes.GlobleClass.EncryptionHelpers.WriteLog(url1, evantname2, tablename3, loginUserId4.ToString(), 0,0);

                    clen();
                 

                    //scope.Complete();
                    int TickitNo = Convert.ToInt32(objCRMMainActivities.MasterCODE);
                    string display = "Your Ticket Number Is " + TickitNo;
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
                    //return;
                    string msg = "Your Ticket Number Is " + TickitNo;
                    string Function = "openModalsmall2('" + msg + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "openModalsmall2();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
                    maxFeedbackID();
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clen();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if(btnSubmit.Text=="Submit")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);

                //Ratting insert



                int admin = 0;
                string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
                int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
                // int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
                if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                    admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);

                if (admin == UID)
                {

                    int MsterCose = tiki;
                    int TenentID = TID;
                    int COMPID = CID;
                    int MasterCODE = MsterCose;
                    int LinkMasterCODE = 1;
                    int MenuID = 0;
                    int ActivityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
                    string ACTIVITYTYPE = "helpdesk";
                    string REFTYPE = "Ticket";
                    string REFSUBTYPE = "HRM";
                    string PerfReferenceNo = "HRM";
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
                    string Version1 = strUName + " - " + aspcomment.SelectedItem;
                    int Type = 0;
                    string MyStatus = drpStatus.SelectedValue;
                    string GroupBy = "helpdesk";
                    int DocID = 0;
                    int ToColumn = 0;
                    int FromColumn = 0;
                    string Active = "Y";
                    int MainSubRefNo = 0;
                    string URL = Request.Url.AbsolutePath;
                    int HelpDept = 0;
                    int HelpFeedback = 0;
                    int HelpLocation = 0;
                    int HelpCatID = 0;
                    int HelpSubID = 0;
                    int aspcomment1 = Convert.ToInt32(aspcomment.SelectedValue);
                    int investigation = 0;
                    string Ratting = "";
                    if (aspcomment1 == 4)
                        investigation = Convert.ToInt32(drpinvestigation.SelectedValue);
                    if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki).Count() == 1)
                    {
                        Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.MasterCODE == tiki);
                        HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
                        //HelpFeedback = Convert.ToInt32(objCRMMainActivities.TickFeedbackype);
                        HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
                        HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
                        HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
                        objCRMMainActivities.MyStatus = drpStatus.SelectedValue;

                        // objCRMMainActivities.Ratting =Convert.ToInt32();
                        if (drpStatus.SelectedValue == "Closed")
                        {
                            objCRMMainActivities.USERNAME = strUName;
                        }
                        DB.SaveChanges();
                        

                       
                        String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = 1" + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
                        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFNAME1;
                        String tablename = "CRMMainActivity";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFID;
                        objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno); ;
               


                        List<Database.CRMProgHW> HWList = DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == tiki && p.Parameter3 == "helpdesk").ToList();
                        if (HWList.Count() > 0)
                        {
                            foreach (Database.CRMProgHW itemhw in HWList)
                            {
                                Database.CRMProgHW hwobj = DB.CRMProgHWs.Single(p => p.TenentID == TID && p.ActivityID == tiki && p.RunningSerial == itemhw.RunningSerial);
                                hwobj.Parameter2 = drpStatus.SelectedValue;
                                DB.SaveChanges();


                            }
                        }
                    }
                    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME,UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL, HelpDept, HelpFeedback, HelpLocation, HelpCatID, HelpSubID, "", aspcomment1, investigation);



                    string Status = drpStatus.SelectedValue;
                   
                    getCommunicatinData();
                    ClenCat();



                }
                else
                {
                    int MsterCose = tiki;
                    int TenentID = TID;
                    int COMPID = CID;
                    int MasterCODE = MsterCose;
                    int LinkMasterCODE = 1;
                    int MenuID = 0;
                    int ActivityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
                    string ACTIVITYTYPE = "helpdesk";
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
                    string Version1 = strUName + " - " + aspcomment.SelectedItem;
                    int Type = 0;
                    string MyStatus = drpStatus.SelectedValue;
                    string GroupBy = "helpdesk";
                    int DocID = 0;
                    int ToColumn = 0;
                    int FromColumn = 0;
                    string Active = "Y";
                    int MainSubRefNo = 0;
                    string URL = Request.Url.AbsolutePath;

                    int HelpDept = 0;
                    int HelpFeedback = 0;
                    int HelpLocation = 0;
                    int HelpCatID = 0;
                    int HelpSubID = 0;
                    int investigation = 0;
                    int aspcomment1 = Convert.ToInt32(aspcomment.SelectedValue);
                    if (aspcomment1 == 4)
                        investigation = Convert.ToInt32(drpinvestigation.SelectedValue);
                   
                    if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki).Count() == 1)
                    {
                        Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
                        HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
                        //HelpFeedback = Convert.ToInt32(objCRMMainActivities.TickFeedbackype);
                        HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
                        HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
                        HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);

                        objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
                        if (drpStatus.SelectedValue == "Closed")
                        {
                            objCRMMainActivities.USERNAME = strUName;
                        }
                        DB.SaveChanges();

                        String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = 1" + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
                        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFNAME1;
                        String tablename = "CRMMainActivity";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFID;
                        objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno); ;
               

                       

                        List<Database.CRMProgHW> HWList = DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == tiki && p.Parameter3 == "helpdesk").ToList();
                        if (HWList.Count() > 0)
                        {
                            foreach (Database.CRMProgHW itemhw in HWList)
                            {
                                Database.CRMProgHW hwobj = DB.CRMProgHWs.Single(p => p.TenentID == TID && p.ActivityID == tiki && p.RunningSerial == itemhw.RunningSerial);
                                hwobj.Parameter2 = drpStatus.SelectedValue;
                                DB.SaveChanges();
                            }
                        }
                    }
                    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate , USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL, HelpDept, HelpFeedback, HelpLocation, HelpCatID, HelpSubID, "", aspcomment1, investigation);

                    string Status = drpStatus.SelectedValue;
                     
                    getCommunicatinData();
                    ClenCat();

            }

            //Ratting insert
           
            }

            else if (btnSubmit.Text == "Update")
            {
                if (ViewState["Edit"] != null)
                    {
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
                        Database.CRMActivity objtbl_Employee = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);

                        objtbl_Employee.aspcomment = Convert.ToInt32(aspcomment.SelectedValue);
                        objtbl_Employee.ActivityPerform = txtComent.Text;
                        objtbl_Employee.REMINDERNOTE = txtComent.Text;
                        objtbl_Employee.MyStatus = drpStatus.SelectedValue;
                        objtbl_Employee.Version = strUName + " - " + aspcomment.SelectedItem; ;
                        DB.SaveChanges();

                        Database.CRMMainActivity objtbl = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);

                       // objtbl.aspcomment = Convert.ToInt32(aspcomment.SelectedValue);
                       // objtbl.ActivityPerform = txtComent.Text;
                        objtbl.REMINDERNOTE = txtComent.Text;
                        objtbl.MyStatus = drpStatus.SelectedValue;
                        objtbl.Version = strUName + " - " + aspcomment.SelectedItem; ;
                        DB.SaveChanges();

                        ViewState["Edit"] = null;
                        btnSubmit.Text = "Add New";
                        btnSubmit.ValidationGroup = "ss";
                        ClenCat();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();

            }
            }
        }
        protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
            if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki).Count() == 1)
            {
                Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
                objCRMMainActivities.Ratting = Convert.ToInt32(e.Value);
                DB.SaveChanges();

                String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = 1" + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
                String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFNAME1;
                String tablename = "CRMMainActivity";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
                int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFID;
                objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno); ;
               

            }
           lblRatingStatus.Text = "5";
        }
        public void getCommunicatinData()
        {
            int admin = 0;
            int Tikitno = Convert.ToInt32(ViewState["TIkitNumber"]);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);

            if (admin == UID)
            {
                listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                listChet.DataBind();
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                ListHistoy.DataBind();
            }
            else
            {
                List<Database.CRMActivity> CRMACTList = DB.CRMActivities.Where(p => (p.TenentID == TID || p.TenentID == 0) && p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME).ToList();
                listChet.DataSource = CRMACTList;
                listChet.DataBind();
                ListHistoy.DataSource = CRMACTList;
                ListHistoy.DataBind();

            }
        }
        public void ClenCat()
        {
            txtComent.Text = "";
            aspcomment.SelectedIndex = 0;
            drpinvestigation.SelectedIndex = 0;
        }

        protected void ltsRemainderNotes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnclick123")
            {
                
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {

                    LinkButton btnclick123 = (LinkButton)e.Item.FindControl("btnclick123");
                    int linkID = Convert.ToInt32(e.CommandArgument);
                    Label tikitID = (Label)e.Item.FindControl("tikitID");
                    Label Label11 = (Label)e.Item.FindControl("Label11");
                    Label Label12 = (Label)e.Item.FindControl("Label12");
                    
                    int alternetTENENT = Convert.ToInt32(Label12.Text);
                    int myidd = Convert.ToInt32(Label11.Text);
                    int Tikitno = Convert.ToInt32(tikitID.Text);
                    ViewState["TIkitNumber"] = Tikitno;
                    panChat.Visible = true;

                    int admin = 0;
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);

                    if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                        admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
                    if (admin == UID)
                    {
                        listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                        listChet.DataBind();

                        ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                        ListHistoy.DataBind();

                        Database.CRMMainActivity infoObj = DB.CRMMainActivities.Single(p => p.TenentID == alternetTENENT && p.MyID == myidd && p.ACTIVITYE == "helpdesk" && p.MasterCODE== Tikitno);
                        int did = Convert.ToInt32(infoObj.TickDepartmentID);
                        int LOCid = Convert.ToInt32(infoObj.TickPhysicalLocation);
                        //int compid = Convert.ToInt32(infoObj.TickFeedbackype);
                       
                    }
                    else
                    {
                        List<Database.CRMActivity> CRMACTList1 = DB.CRMActivities.Where(p => (p.TenentID == TID || p.TenentID == 0) && p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME).ToList();
                        listChet.DataSource = CRMACTList1;
                        listChet.DataBind();

                        ListHistoy.DataSource = CRMACTList1;
                        ListHistoy.DataBind();
                        
                    }
                    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Tikitno);
                  //  Rating1.CurrentRating = Convert.ToInt32(objCRMMainActivities.Ratting);
                    lblRatingStatus.Text = objCRMMainActivities.Ratting.ToString();

                }

            }

            else if (e.CommandName == "btneditticket")
            {
                //DrpSubCat.SelectedIndex = 0;
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objICCATEGORY = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);

               /* if (objICCATEGORY.TickFeedbackype != 0 && objICCATEGORY.TickFeedbackype != null)
                {
                    drpFeedbackype.SelectedValue = objICCATEGORY.TickFeedbackype.ToString();
                }*/
                if (objICCATEGORY.TickDepartmentID != 0 && objICCATEGORY.TickDepartmentID != null)
                {
                    drpSDepartment.SelectedValue = objICCATEGORY.TickDepartmentID.ToString();
                }
                if (objICCATEGORY.TickPhysicalLocation != 0 && objICCATEGORY.TickPhysicalLocation != null)
                {
                    DrpPhysicalLocation.SelectedValue = objICCATEGORY.TickPhysicalLocation.ToString();
                }
                    if (objICCATEGORY.Patient_Name != "" && objICCATEGORY.Patient_Name!=null)
                    {
                        txtpatientname.Text = objICCATEGORY.Patient_Name.ToString();     
                    }
                    if (objICCATEGORY.MRN != "" && objICCATEGORY.MRN != null)
                    {

                        txtMRN.Text = objICCATEGORY.MRN.ToString();
                    }
                    if (objICCATEGORY.Contact != "" && objICCATEGORY.Contact != null)
                    {
                        txtcontact.Text = objICCATEGORY.Contact.ToString();
                    }
                    if (objICCATEGORY.TickCatID != 0 && objICCATEGORY.TickCatID != null)
                    {
                        DrpTCatSubCate.SelectedValue = objICCATEGORY.TickCatID.ToString();
                    }
                    if (objICCATEGORY.TickSubCatID != 0 && objICCATEGORY.TickSubCatID != null)
                    {
                        DrpSubCat.SelectedValue = objICCATEGORY.TickSubCatID.ToString();
                    }

                    if (objICCATEGORY.Version != "" && objICCATEGORY.Version != null)
                    {
                        txtSubject.Text = objICCATEGORY.Version.ToString();
                    }
                    if (objICCATEGORY.Remarks != "" && objICCATEGORY.Remarks != null)
                    {
                        txtMessage.Text = objICCATEGORY.Remarks.ToString();
                    }
                    if (objICCATEGORY.ReportedBy != 0 && objICCATEGORY.ReportedBy != null)
                    {
                        drpusermt.SelectedValue = objICCATEGORY.ReportedBy.ToString();
                    }

                    pnlTicki.Visible = true;
                //PnlBindTick.Visible = false;
                panChat.Visible = false;
                btnSave.Text = "Update";
                ViewState["Edit"] = ID;
                
            }


            else if (e.CommandName == "btnticketes")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btnticketes = (LinkButton)e.Item.FindControl("btnticketes");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
                listChet.DataBind();

            }

            else if (e.CommandName == "btnnames")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btnnames = (LinkButton)e.Item.FindControl("btnnames");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                listChet.DataBind();
            }
            else if (e.CommandName == "btnremarks")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btnremarks = (LinkButton)e.Item.FindControl("btnremarks");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                listChet.DataBind();
            }
            else if (e.CommandName == "btndates")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btndates = (LinkButton)e.Item.FindControl("btndates");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                listChet.DataBind();
            }
            else if (e.CommandName == "btnpendings")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btnpendings = (LinkButton)e.Item.FindControl("btnpendings");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                listChet.DataBind();
            }
            else if (e.CommandName == "btninprogress")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btninprogress = (LinkButton)e.Item.FindControl("btninprogress");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                listChet.DataBind();
            }
            else if (e.CommandName == "btnclosed")
            {
                listChet.Visible = true;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LinkButton btnclosed = (LinkButton)e.Item.FindControl("btninprogress");
                Label tikitID = (Label)e.Item.FindControl("tikitID");
                int Tikitno = Convert.ToInt32(tikitID.Text);
                ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                ListHistoy.DataBind();
                listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
                listChet.DataBind();
            }

        }

        protected void btnTikitClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("/CRM/HelpDeskSchedule.aspx");

            //string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
            //int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
            //int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
            //int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
            //if (RoleID == 4)
            //{

            //    int MsterCose = tiki;
            //    int TenentID = TID;
            //    int COMPID = CID;
            //    int MasterCODE = MsterCose;
            //    int LinkMasterCODE = 1;
            //    int MenuID = 0;
            //    int ActivityID = 0;
            //    string ACTIVITYTYPE = "helpdesk";
            //    string REFTYPE = "Ticket";
            //    string REFSUBTYPE = "CRM";
            //    string PerfReferenceNo = "CRM";
            //    string EarlierRefNo = "A";
            //    string NextUser = strUName;
            //    string NextRefNo = "A";
            //    string AMIGLOBAL = "Y";
            //    string MYPERSONNEL = "Y";
            //    string ActivityPerform = txtComent.Text;
            //    string REMINDERNOTE = txtComent.Text;
            //    int ESTCOST = 0;
            //    string GROUPCODE = "A";
            //    string USERCODEENTERED = "A";
            //    DateTime UPDTTIME = DateTime.Now;
            //    string USERNAME = strUName;
            //    int CRUP_ID = 0;
            //    DateTime InitialDate = DateTime.Now;
            //    DateTime DeadLineDate = DateTime.Now;
            //    string RouteID = "A";
            //    string Version1 = "Suppot Reply";
            //    int Type = 0;
            //    string MyStatus = drpStatus.SelectedValue;
            //    string GroupBy = "helpdesk";
            //    int DocID = 0;
            //    int ToColumn = 0;
            //    int FromColumn = 0;
            //    string Active = "Y";
            //    int MainSubRefNo = 0;
            //    string URL = Request.Url.AbsolutePath;
            //    int HelpDept = 0;
            //    int HelpFeedback = 0;
            //    int HelpLocation = 0;
            //    int HelpCatID = 0;
            //    int HelpSubID = 0;


            //    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
            //    HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
            //    HelpFeedback = Convert.ToInt32(objCRMMainActivities.TickFeedbackype);
            //    HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
            //    HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
            //    HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
            //    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL,HelpDept, HelpFeedback, HelpLocation, HelpCatID, HelpSubID);


            //    objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
            //    objCRMMainActivities.USERNAME = strUName;
            //    DB.SaveChanges();
            //    string Status = drpStatus.SelectedValue;
            //    getCommunicatinData();
            //    ClenCat();
            //    getStatusAll("Closed");

            //}
            //else
            //{
            //    int MsterCose = tiki;
            //    int TenentID = TID;
            //    int COMPID = CID;
            //    int MasterCODE = MsterCose;
            //    int LinkMasterCODE = 1;
            //    int MenuID = 0;
            //    int ActivityID = 0;
            //    string ACTIVITYTYPE = "helpdesk";
            //    string REFTYPE = "Ticket";
            //    string REFSUBTYPE = "CRM";
            //    string PerfReferenceNo = "CRM";
            //    string EarlierRefNo = "A";
            //    string NextUser = strUName;
            //    string NextRefNo = "A";
            //    string AMIGLOBAL = "Y";
            //    string MYPERSONNEL = "Y";
            //    string ActivityPerform = txtComent.Text;
            //    string REMINDERNOTE = txtComent.Text;
            //    int ESTCOST = 0;
            //    string GROUPCODE = "A";
            //    string USERCODEENTERED = "A";
            //    DateTime UPDTTIME = DateTime.Now;
            //    string USERNAME = strUName;
            //    int CRUP_ID = 0;
            //    DateTime InitialDate = DateTime.Now;
            //    DateTime DeadLineDate = DateTime.Now;
            //    string RouteID = "A";
            //    string Version1 = "Suppot Reply";
            //    int Type = 0;
            //    string MyStatus = drpStatus.SelectedValue;
            //    string GroupBy = "helpdesk";
            //    int DocID = 0;
            //    int ToColumn = 0;
            //    int FromColumn = 0;
            //    string Active = "Y";
            //    int MainSubRefNo = 0;
            //    string URL = Request.Url.AbsolutePath;

            //    int HelpDept = 0;
            //    int HelpFeedback = 0;
            //    int HelpLocation = 0;
            //    int HelpCatID = 0;
            //    int HelpSubID = 0;


            //    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
            //    HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
            //    HelpFeedback = Convert.ToInt32(objCRMMainActivities.TickFeedbackype);
            //    HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
            //    HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
            //    HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
            //    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL, HelpDept, HelpFeedback, HelpLocation, HelpCatID, HelpSubID);

            //    objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
            //    objCRMMainActivities.USERNAME = strUName;
            //    DB.SaveChanges();
            //    string Status = drpStatus.SelectedValue;
            //    getCommunicatinData();
            //    ClenCat();
            //    getStatusAll("Closed");
            //}

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

        //public string getstatus(int UID)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "pending"))
        //    {
        //        string UName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
        //        return UName;
        //    }
        //    else
        //    {
        //        return "Not Found";
        //    }
        //}

        protected void lblattachments_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string UID = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
            if (ViewState["TIkitNumber"] != null)
            {
                int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
                string Type = "Ticket";
                string Url = "../CRM/AttachmentMst.aspx?AttachID=" + tiki + "&DID=" + Type + "&RefNo=" + 11058 + "&UID=" + uid1;//+ "&recodInsert=" + tiki;
                string s = "window.open('" + Url + "', 'popup_window', 'width=950,height=225,left=100,top=100,resizable=yes');";
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", s, true);
            }

            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            //string URL = "AttachmentMst.aspx?AttachID=" + Contectid + "&DID=Contact&Mode=" + Mode + "&recodInsert=" + CompnyID;
            //Response.Redirect(URL);
        }

        protected void btnAttch_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            if (ViewState["TIkitNumber"] != null)
            {
                int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
                List<Database.tbl_DMSAttachmentMst> ttachOBJ = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.CATID == uid1 && p.AttachmentById == tiki).ToList();
                int Coun = ttachOBJ.Count();
                btnAttch.Text = Coun.ToString();
            }
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            getCommunicatinData();
            UpdatePanel1.Update();
        }

        protected void chklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chklist.Checked)
            {
                txtstaffname.Visible = true;
            }
            else
            {
                txtstaffname.Visible = false;
            }

        }

        protected void drpSDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpSDepartment.SelectedValue=="99999")
            {
                txtMessage.Text = drpSDepartment.SelectedItem + " = ";
            }
            else
            {
                txtMessage.Text = "";
            }
            
        }

        protected void aspcomment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aspcomment.SelectedValue == "4")
            {
                drpinvestigation.Visible = true;
                //Label20.Visible = true;
                //Label20.Text = "Risk";
                Rating1.Visible = true;
            }
            else
            {
                drpinvestigation.Visible = false;
                //Label20.Visible = false;
                Rating1.Visible = false;
            }

        }

        protected void chklist_CheckedChanged(object sender, EventArgs e)
        {
            if (chklist.Checked == true)
            {
                txtstaffname.Visible = true;
            }
            else
            {
                txtstaffname.Visible = false;
            }
        }

        protected void drpinvestigation_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void DrpTCatSubCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<ICCATSUBCAT> list=DB.ICCATSUBCATs.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.CATID == CID).ToList();
            List<ICSUBCATEGORY> tempList = new List<ICSUBCATEGORY>();
            foreach (ICCATSUBCAT item in list)
            {
                var obj = DB.ICSUBCATEGORies.FirstOrDefault(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == item.SUBCATID);
                tempList.Add(obj);
               
            }
            DrpSubCat.DataSource = tempList;
            DrpSubCat.DataTextField = "SUBCATNAME";
            DrpSubCat.DataValueField = "SUBCATID";
            DrpSubCat.DataBind();
            if (list.Count <=0)
            DrpSubCat.Items.Insert(0, new ListItem("-- Select --", "0"));
                
        }

        

        protected void btnsub2_Click(object sender, EventArgs e)
        {

        }

        protected void btnsub3_Click(object sender, EventArgs e)
        {

        }

        protected void btnsub4_Click(object sender, EventArgs e)
        {

        }

        protected void btnsub5_Click(object sender, EventArgs e)
        {

        }

        protected void btn2015_Click(object sender, EventArgs e)
        {
            //DateTime s = Convert.ToDateTime("2015-01-01 11:28:41.077");
            //DateTime e = Convert.ToDateTime("2015-12-01 11:28:41.077");
            //getStatusAll();
        }

        protected void listChet_DataBound(object sender, EventArgs e)
        {

        }

        protected void listChet_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {

                Label lblcompid = (Label)e.Item.FindControl("lblcompid");
                Label lblmastercode = (Label)e.Item.FindControl("lblmastercode");
                Label lblMyLineNo = (Label)e.Item.FindControl("lblMyLineNo");
                Label lblLocationID = (Label)e.Item.FindControl("lblLocationID");
                Label lblLinkMasterCODE = (Label)e.Item.FindControl("lblLinkMasterCODE");
                Label lblActivityID = (Label)e.Item.FindControl("lblActivityID");
                Label lblPrefix = (Label)e.Item.FindControl("lblPrefix");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int ActivityID = Convert.ToInt32(lblActivityID.Text);
                int ComId = Convert.ToInt32(lblcompid.Text);
                int mylineno = Convert.ToInt32(lblMyLineNo.Text);
                int location = Convert.ToInt32(lblLocationID.Text);
                int linkmaster = Convert.ToInt32(lblLinkMasterCODE.Text);
                string prefix = lblPrefix.Text;
                Database.CRMActivity objREFTABLE = DB.CRMActivities.Single(p => p.TenentID == TID && p.COMPID == ComId && p.MasterCODE == ID && p.LocationID == location && p.LinkMasterCODE == linkmaster && p.ActivityID == ActivityID && p.Prefix == prefix);
                if (aspcomment.SelectedValue != "0" && aspcomment.SelectedValue!=null)
                {
                    aspcomment.SelectedValue = objREFTABLE.aspcomment.ToString();
                }               
                txtComent.Text = objREFTABLE.ActivityPerform.ToString();
               // txtComent.Text = objREFTABLE.REMINDERNOTE.ToString();
                if(drpStatus.SelectedValue != null && drpStatus.SelectedValue !="0")
                {
                    drpStatus.SelectedValue = objREFTABLE.MyStatus.ToString();
                }                
                btnSubmit.Text = "Update";
                ViewState["Edit"] = ID;


            }
        }

        protected void ltsRemainderNotes_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string status = ViewState["StatusAll"].ToString();
            if (e.Item.ItemType == ListViewItemType.DataItem && status == "Closed" )
            {
                LinkButton btnclick123 = (LinkButton)e.Item.FindControl("btnclick123");
                LinkButton btneditticket = (LinkButton)e.Item.FindControl("btneditticket");
                btnclick123.Visible = false;
                btneditticket.Visible = false;
                aspcomment.Visible = false;
                txtComent.Visible = false;
                drpStatus.Visible = false;
                //Label6.Visible = false;
                btnAttch.Visible = false;
                btnSubmit.Visible = false;
                btnTikitClose.Visible = false;
                lbllblattachments2.Visible = false;
            }
            else
            {
                LinkButton btnclick123 = (LinkButton)e.Item.FindControl("btnclick123");
                LinkButton btneditticket = (LinkButton)e.Item.FindControl("btneditticket");
                btnclick123.Visible = true;
                btneditticket.Visible = true;
                aspcomment.Visible = true;
                txtComent.Visible = true;
                drpStatus.Visible = true;
                //Label6.Visible = true;
                btnAttch.Visible = true;
                btnSubmit.Visible = true;
                btnTikitClose.Visible = true;
                lbllblattachments2.Visible = true;
            }
        }

        

        //protected void linkreopen_Click(object sender, EventArgs e)
        //{
        //    lblticket.Visible = true;
        //    txtticket.Visible = true;
        //    lblFeedback.Visible = true;
        //    txtFeedback.Visible = true;
        //    btnsearch.Visible = true;
        //}

        
    }
}
