using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.IO;
using Classes;
using System.Drawing;
namespace Web.CRM
{
    public partial class CRMMaster : System.Web.UI.MasterPage
    {
        //   Database.CallEntities DB1 = new Database.CallEntities();
        List<Navigation> ChoiceList = new List<Navigation>();
        bool flag = false;
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Init(object sender, EventArgs e)
        {
            //CheckLogin();
        }
        public void CheckLogin()
        {
            if (Session["USER"] == null || Session["USER"] == "0")
            {
                Session.Abandon();
                Response.Redirect("/ACM/SessionTimeOut.aspx");
                
            }
        }
        public string SessionLoad1(int TID, int LID, int UID, int EMPID, string LangID)
        {
            TID = 10;//Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = 1;//Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = 11525;//Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            string Ref = TID.ToString() + "," + LID.ToString() + "," + UID.ToString() + "," + EMPID.ToString() + "," + LangID;
            return Ref;
        }
        public int GetLogginID()
        {
            return Convert.ToInt32(((Database.USER_MST)Session["USER"]).USER_ID);
        }
        public int GetLogginType()
        {
            return Convert.ToInt32(((Database.USER_MST)Session["USER"]).USER_TYPE);
        }
        public int GetHRMSLogginType()
        {
            return Convert.ToInt32(((DB.tblSetupCrms.Where(p=>p.IsAdmin==true).First())).UserID);
        }
       
        public void PutAbsant()
        {
            int UserID = GetLogginID();
            foreach (Attandance item in from item in DB.Attandances where item.UserID == UserID && item.OutTime == null select item)
            {
                item.isAbsent = true;
            }
            DB.SaveChanges();

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["Previous"] = Session["Current"];
                Session["Current"] = Request.RawUrl;
                //  PutAbsant();
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
                //lblUserName.Text = strUName;
                int TID = 10;//Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int userID = ((USER_MST)Session["USER"]).USER_ID;
                int LocationID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (Session["MenuACm"] != null)
                {
                    //GlobleClass.DeleteTempUser(TID, userID, LocationID, 7);
                    //GlobleClass.getMenuGloble(TID, userID, LocationID, 7);
                    //Session["MenuACm"] = null;
                }

                //string strRoleName = "";
                //if (strUName == "Ayo")
                //    strRoleName = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_NAME == "Super Admin").ROLE_NAME;
                //  else if(TID==0)
                //    strRoleName = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_ID==1 ).ROLE_NAME;
                //else
                //    strRoleName = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_NAME == "Simple User").ROLE_NAME;

                //lblRole.Text = "Role : " + strRoleName.ToString();

                //string UID = (((USER_MST)Session["USER"]).USER_ID).ToString();
                //if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID == UID).Count() > 0)
                //{
                //    string strCompanyName = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.USERID == UID).COMPNAME;
                //    //lblCompanyName.Text = "Company : " + strCompanyName.ToString();
                //}
                //  menubind(7);
                //lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");

                int UserID = GetLogginID();
                DateTime TodayDate = DateTime.Now.Date;
                List<Attandance> ObjList = (from item in DB.Attandances where item.UserID == UserID && item.InTime.Value.Year == TodayDate.Year && item.InTime.Value.Month == TodayDate.Month && item.InTime.Value.Day == TodayDate.Day  select item).ToList();
                if (ObjList.Where(p=>p.OutTime == null).Count() > 0)
                {
                    linkCheckIn.Visible = false;
                    //btnSignin.CssClass = "stdbtn";
                    linkCheckOut.Visible = true;
                    //btnSignOut.CssClass = "stdbtn btn_blue";
                    //linkCheckIn.BackColor = Color.Green;
                }
                else
                {
                    if (ObjList.Count() == 0)
                    {
                        Attandance Obj = new Attandance();
                        Obj.UserID = GetLogginID();
                        Obj.InTime = DateTime.Now;
                        Obj.isAbsent = false;
                        Obj.Deleted = true;
                        Obj.Active = true;
                        DB.Attandances.AddObject(Obj);
                        DB.SaveChanges();
                        linkCheckIn.Visible = false;
                        linkCheckOut.Visible = true;
                    }
                    else
                    {
                        linkCheckIn.Visible = true;
                        //btnSignin.CssClass = "stdbtn btn_red";
                        //btnSignOut.CssClass = "stdbtn";
                        //linkCheckIn.BackColor = Color.Red;
                        //btnSignOut.ForeColor = Color.Black;
                        linkCheckOut.Visible = false;
                    }
                }
                //int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                //string NewLogText = ((USER_MST)Session["USER"]).FIRST_NAME;
                //string UID1 = ((USER_MST)Session["USER"]).LAST_NAME;
                //int CrupID = Convert.ToInt32(((USER_MST)Session["USER"]).CRUP_ID);
                //string table = ((USER_MST)Session["USER"]).FIRST_NAME;
                //string UserName = ((USER_MST)Session["USER"]).FIRST_NAME;
                //GlobleClass.UpdateLog(NewLogText, CrupID, table, UserName, TID, UID1, LID);

                if (Request.QueryString["MID"] != null)
                {
                    string Menuid = Request.QueryString["MID"].ToString();
                    string MenuName = Classes.GlobleClass.EncryptionHelpers.Decrypt(Menuid);
                    string[] MenuidQwe = MenuName.Split('~');
                    int Meni = Convert.ToInt32(MenuidQwe[1]);

                    FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.MENU_ID == Meni);//p.TenentID == TID &&
                    lblpagename.Text = obj.MENU_NAME1;
                    lblpageid.Text = Meni.ToString();

                    //   haresh    // ltsMenu.DataSource = DB.tempUser.Where(p => p.ACTIVEMENU == true && p.MENU_TYPE == "Separator" && p.MODULE_ID == MID || p.AMIGLOBALE == 1).OrderBy(a => a.MENU_ORDER);
                    // ltsMenu.DataBind();

                }
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Wel Come To CRM DashBoard", "Winner!", Classes.Toastr.ToastPosition.TopCenter);
            }


        }


        public void BindMeniList()
        {
            /// int TID = Convert.ToInt32(((USER_MST)Session["User"]).TenentID);
            //List<tempUser> result1 = Classes.Globle.EncryptionHelpers.getMenu(TID);

            //ROLE To user fine out how 
            // Read ERP_WEB_GEN_USER_ROLE_MAP using session userid and for that role privilage_id+userID locate how many allowed menu in ERP_WEB_PRIVILAGE_MENU source=R
            ///MODULE To user fine out how 
            // Read ERP_WEB_MODULE_MAP using session userid and for that role privilage_id+userID locate how many allowed menu in ERP_WEB_PRIVILAGE_MENU source=R
            // Privilage for Menu to know how many menu allowed to this user
            // USER RIGHT to user fine out how 
            // Read ERP_WEB_USER_RIGHTS using session userid and for that role privilage_id+userID locate how many allowed menu in ERP_WEB_PRIVILAGE_MENU source=R
            // ERP_WEB_MENU_MST where AMIGLOBAL=Y

            ////var result1 = (from ur in DB.ERP_WEB_GEN_USER_ROLE_MAP join                              
            ////                   rlist in DB.ERP_WEB_PRIVILAGE_MENU on ur.PRIVILEGE_ID equals rlist.PRIVILEGE_ID into rolelist
            ////               from rlist in rolelist.DefaultIfEmpty()
            ////               join pmm in DB.ERP_WEB_MODULE_MAP on rlist.PRIVILEGE_ID equals pmm.PRIVILEGE_ID into modulelist
            ////               from pmm in modulelist.DefaultIfEmpty()
            ////               join userr in DB.ERP_WEB_USER_RIGHTS on pmm.PRIVILEGE_ID equals userr.PRIVILEGE_ID 

            ////               join menu in DB.ERP_WEB_MENU_MST on pmm.MODULE_ID equals menu.MODULE_ID

            ////               where rlist.PRIVILAGEFOR == 1 && menu.ACTIVE_FLAG==1
            ////               select new { menu.MENU_NAME,menu.LINK }).ToList().Distinct();

            //ltsMenu.DataSource = result1;
            //ltsMenu.DataBind();
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/ACM/SessionTimeOut.aspx");
        }
        //((DMSMaster)Page.Master).WriteLog("MaterialDelivary(11001),ID:" + DocUniqueID.ToString(), "tbl_DMSMaterialDelivary");
        // public int WriteLog(string LogText,string MstText, string table,string UserID )
        // {

        //     int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //     string UID = ((USER_MST)Session["USER"]).LOGIN_ID;
        //   //  int NewCID = DB.tbl_DMSMaterialDelivary.Count() > 0 ? Convert.ToInt32(DB.ERP_CRUP_MST.Max(p => p.CRUP_ID) + 1) : 1;
        //     ERP_CRUP_MST objUserLog = new ERP_CRUP_MST();
        //     objUserLog.TenentID = TID;
        ////     objUserLog.CRUP_ID = NewCID;
        //     objUserLog.MySerial = 1;
        //     objUserLog.PHYSICALLOCID = table;//come from to tblPHYSICALLOCation table
        //     objUserLog.CREATED_DT = DateTime.Now;
        //     objUserLog.CREATED_BY = UID;
        //     objUserLog.ActivityNote = LogText;
        //     //objUserLog.CREATED_DT = DateTime.Now;
        //     //objUserLog.UPDATED_BY = ((Database.USER_MST)Session["USER"]).ID;
        //     DB.ERP_CRUP_MST.AddObject(objUserLog);
        //     DB.SaveChanges();

        //     tblAudit objtblAudit = new tblAudit();
        //     objtblAudit.TenentID = TID;
        // //    objtblAudit.CRUP_ID = NewCID;
        //     objtblAudit.MySerial = 1;
        //     objtblAudit.TableName = table;
        //     objtblAudit.AuditType = "Create";
        //     objtblAudit.NewValue = LogText;
        //     objtblAudit.CreatedDate = DateTime.Now;
        //     objtblAudit.CreatedUserName = UserID;
        //     DB.tblAudits.AddObject(objtblAudit);
        //     DB.SaveChanges();
        //   //  return NewCID;
        // }
        public void UpdateLog(string NewLogText, int CrupID, string table, string UIDD)
        {
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //string UID = ((USER_MST)Session["USER"]).LOGIN_ID;
            //tblAudit objtblAudit = new tblAudit();
            //objtblAudit.TenentID = TID;
            //objtblAudit.CRUP_ID = CrupID;
            //objtblAudit.MySerial += 1;
            //objtblAudit.TableName = table;
            //objtblAudit.AuditType = "Update";
            //objtblAudit.NewValue = NewLogText;
            //objtblAudit.UpdateDate = DateTime.Now;
            //objtblAudit.UpdateUserName = UIDD;
            //DB.tblAudits.AddObject(objtblAudit);
            //DB.SaveChanges();
            //ERP_CRUP_MST objUserLog = DB.ERP_CRUP_MST.SingleOrDefault(p => p.CRUP_ID == CrupID);
            //objUserLog.MySerial += 1;
            //objUserLog.UPDATED_DT = DateTime.Now;
            //objUserLog.UPDATED_BY = UID;
            //DB.SaveChanges();
        }
        public string getOwnPage()
        {
            string PageID = "0";
            if (Request.Url.AbsolutePath.Contains("Job_Title.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Job_Title.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Opportunity_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Opportunity_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("CRM_tbl_Lead_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "CRM_tbl_Lead_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Contact_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Contact_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Account_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Account_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Campaign_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Campaign_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("CRM_tbl_Lead_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "CRM_tbl_Lead_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Supplier_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Supplier_Mst.aspx" && p.Remark == "CRM").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("CRMMainActivityRole.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "CRMMainActivityRole.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ICEXTRACOST.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ICEXTRACOST.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tbl_Event_Master.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tbl_Event_Master.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TaskAppointment.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TaskAppointment.aspx").TableName.ToString();
            }
            return PageID;
        }
        public List<Database.TBLLabelDTL> Bindxml(string pagename)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("//CRM//xml//" + pagename + ".xml"));
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
        public void Loadlist<T>(int Showdata, int take, int Skip, int ChoiceID, Label lblShowinfEntry, LinkButton btnPrevious, LinkButton btnNext, ListView Listview1, ListView ListView2, int Totalrec, List<T> List)
        {

            btnPrevious.Enabled = false;
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                BindList(Listview1, (List.Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
            }
            else
            {
                BindList(Listview1, (List.Take(Showdata).Skip(0)).ToList());
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            ChoiceList.Clear();
            // int ChoiceID=
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation(ListView2);

            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                take = Showdata;
                Skip = 0;
                BindList(Listview1, (List.Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious.Enabled = false;
                ChoiceID = 0;
                GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext.Enabled = false;
                else
                    btnNext.Enabled = true;

            }
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        }
        #region Step5 Navigation
        public void Navigation(ListView ListView2)
        {
            //Navigati0n

            if (ViewState["NDATA"] != null)
            {
                ChoiceList = (List<Navigation>)ViewState["NDATA"];
            }
            else
            {
                ChoiceList = new List<Navigation>();
            }
            ListView2.DataSource = ChoiceList;
            ListView2.DataBind();
        }
        #endregion
        #region Step6 GetCurrentNavigation
        public void GetCurrentNavigation(int ChoiceID, int Showdata, ListView ListView2, int Totalrec)
        {
            int lastchoise = 0;
            ChoiceID = ChoiceID - 5;
            if (ChoiceID <= 0)
            {
                ChoiceID = 0;
            }
            ChoiceList.Clear();
            for (int i = 0; i < 10; i++)
            {
                ChoiceID++;
                lastchoise = Math.Abs(Totalrec / Showdata) + 2;
                if (lastchoise == ChoiceID)
                    break;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation(ListView2);
        }
        public void GetCurrentNavigationLast(int ChoiceID, int Showdata, ListView ListView2, int Totalrec)
        {
            int lastchoise = 0;
            ChoiceID = ChoiceID - Showdata;
            if (ChoiceID <= 0)
            {
                ChoiceID = 0;
            }
            ChoiceList.Clear();
            for (int i = 0; i < 5; i++)
            {
                ChoiceID++;
                lastchoise = Math.Abs(Totalrec / Showdata) + 2;
                if (lastchoise == ChoiceID)
                    break;
                Navigation Obj = new Navigation();
                Obj.Choice = "";
                Obj.ID = ChoiceID;
                ChoiceList.Add(Obj);
            }
            ViewState["NDATA"] = ChoiceList;
            Navigation(ListView2);
        }
        #endregion
        public void BindList<T>(ListView Listview1, List<T> List)
        {

            Listview1.DataSource = List;
            Listview1.DataBind();

        }

        public void BindListFromPopup(ContentPlaceHolder Obj123, string OP, string DropDownID)
        {
            //if (OP == Database.Common.PopupOP.Project.ToString())
            //{
            //    DropDownList Drp = (DropDownList)Obj123.FindControl(DropDownID);
            //    Drp.DataSource = DB.TBLPROJECT.Where(p => p.ACTIVE == true).OrderByDescending(p => p.PROJECTID);
            //    Drp.DataBind();
            //}
            //if (OP == Database.Common.PopupOP.Supplier.ToString())
            //{
            //    DropDownList Drp = (DropDownList)Obj123.FindControl(DropDownID);
            //    Drp.DataSource = DB.TBLCOMPANYSETUP.Where(P => P.Approved == 1 && P.SALER == true);
            //    Drp.DataBind();
            //}
        }

        public string GetCustomerName(int CusID = 0)
        {
            string CustomerName = "";
            if (CusID != 0)
            {
                int CID = Convert.ToInt32(CusID);

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (DB.TBLCONTACTs.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY" && P.ContactMyID == CID).Count() > 0)
                {
                    Database.TBLCONTACT obj_Conatcat = DB.TBLCONTACTs.SingleOrDefault(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY" && P.ContactMyID == CID);
                    CustomerName = obj_Conatcat.PersName1;
                }
            }
            return CustomerName;
        }

        public string GetSupplierName(int SupID = 0)
        {
            string SupplierName = "";
            if (SupID != 0)
            {
                int SID = Convert.ToInt32(SupID);

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (DB.TBLCONTACTs.Where(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY" && P.ContactMyID == SID).Count() > 0)
                {
                    Database.TBLCONTACT obj_Conatcat = DB.TBLCONTACTs.SingleOrDefault(P => P.TenentID == TID && P.Active == "Y" && P.PHYSICALLOCID != "HLY" && P.ContactMyID == SID);
                    SupplierName = obj_Conatcat.PersName1;
                }
            }
            return SupplierName;
        }

        public string GetProjectName(int Project = 0)
        {
            string ProjectName = "";
            if (Project != 0)
            {
                int PID = Convert.ToInt32(Project);

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (DB.TBLPROJECTs.Where(P => P.TenentID == TID && P.ACTIVE == true && P.PROJECTID == PID).Count() > 0)
                {
                    Database.TBLPROJECT obj_Project = DB.TBLPROJECTs.SingleOrDefault(P => P.TenentID == TID && P.ACTIVE == true && P.PROJECTID == PID);
                    ProjectName = obj_Project.PROJECTNAME1;
                }
            }
            return ProjectName;
        }

        public string GetTeamName(int TeamID)
        {
            string TeamName = "";
            if (TeamID != 0)
            {
                int TeamID1 = Convert.ToInt32(TeamID);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS" && p.REFID == TeamID1).Count() > 0)
                {
                    Database.REFTABLE obj_Ref = DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS" && p.REFID == TeamID1);
                    TeamName = obj_Ref.REFNAME1;
                }
            }
            return TeamName;
        }


        public string GetLeadSource(int LeadSouID)
        {
            string LeadSource = "";
            if (LeadSouID != 0)
            {
                int LeadSID1 = Convert.ToInt32(LeadSouID);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "LEADSTATUS" && p.REFID == LeadSID1).Count() > 0)
                {
                    Database.REFTABLE obj_Ref = DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "LEADSTATUS" && p.REFID == LeadSID1);
                    LeadSource = obj_Ref.REFNAME1;
                }
            }
            return LeadSource;
        }


        public string GetLeadStatus(int SID)
        {
            string Status = "";
            if (SID != 0)
            {
                int SID1 = Convert.ToInt32(SID);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS").Count() > 0)
                {
                    Database.REFTABLE obj_Ref = DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFID == SID1 && p.REFTYPE == "ACTVTY" && p.REFSUBTYPE == "STATUS");
                    Status = obj_Ref.REFNAME1;
                }
            }
            return Status;
        }

        public string GetLink(int menuID)
        {// <span class="title"><%# Eval("MENU_NAME1")%> </span></a> <ul class="sub-menu" style="display: none;">
            string menustr = "";
            int MTID = Convert.ToInt32(ViewState["MTID"]);
            string OnOff = "";
            var obj = DB.FUNCTION_MST.SingleOrDefault(p => p.MENU_ID == menuID && p.TenentID == MTID);
            if (obj.MENU_LOCATION == "Separator")
            {
                OnOff = obj.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                menustr = "<a href='#' style='font-weight: bold;'><i class='" + obj.ICONPATH + "'></i><span class='title' >" + obj.MENU_NAME1 + "</span><span class='arrow' style='color: #000;'></span></a>";
                menustr += " <ul class='sub-menu' style='display: none;'>";
                List<Database.FUNCTION_MST> itemList = DB.FUNCTION_MST.Where(p => p.MASTER_ID == menuID && p.TenentID == MTID).OrderBy(a => a.MENU_ORDER).ToList();

                if (itemList.Count() > 0)
                {
                    foreach (Database.FUNCTION_MST item in itemList)
                    {
                        OnOff = item.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                        List<Database.FUNCTION_MST> itemList1 = DB.FUNCTION_MST.Where(p => p.MASTER_ID == item.MENU_ID && p.TenentID == MTID).OrderBy(a => a.MENU_ORDER).ToList();
                        if (itemList1.Count() > 0)
                        {
                            menustr += "<li class='active'><a href='" + item.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item.ICONPATH + "' ></i><span class='title' >" + item.MENU_NAME1 + "</span><span class='arrow'></span></a>";
                            menustr += "<ul class='sub-menu' style='display: none;'>";
                            foreach (Database.FUNCTION_MST item1 in itemList1)
                            {
                                OnOff = item1.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                                List<Database.FUNCTION_MST> itemList2 = DB.FUNCTION_MST.Where(p => p.MASTER_ID == item1.MENU_ID && p.TenentID == MTID).OrderBy(a => a.MENU_ORDER).ToList();
                                if (itemList2.Count() > 0)
                                {
                                    menustr += "<li ><a href='" + item1.URLREWRITE + "?MID=" + item.MASTER_ID + "' ><i class='" + item1.ICONPATH + "'></i><span class='title' >" + item1.MENU_NAME1 + "</span><span class='arrow'></span></a>";
                                    menustr += "<ul class='sub-menu' >";
                                    foreach (Database.FUNCTION_MST item2 in itemList2)
                                    {
                                        OnOff = item2.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                                        menustr += "<li><a href='" + item2.URLREWRITE + "?MID=" + item.MASTER_ID + "' ><i class='" + item2.ICONPATH + "'></i><span class='title' >" + item2.MENU_NAME1 + "</span></a></li>";
                                    }
                                    menustr += Displaysubmenu1(menuID);
                                    menustr += "</li>";
                                }
                                else
                                {
                                    menustr += "<li ><a href='" + item1.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item1.ICONPATH + "'></i><span class='title' >" + item1.MENU_NAME1 + "</span></a></li>";
                                }
                            }
                            menustr += Displaysubmenu1(menuID);
                            menustr += "</li>";
                        }
                        else
                        {
                            menustr += "<li class='active'><a href='" + item.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item.ICONPATH + "'></i><span class='title' >" + item.MENU_NAME1 + "</span></a></li>";
                        }
                    }

                }
            }
            else
            {
                menustr = "<a href='" + obj.URLREWRITE + "'><i class='icon-home'></i><span class='title'>" + obj.MENU_NAME1 + "</span></a>";
            }
            return menustr;
        }
        public string Displaysubmenu1(int menuID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            var obj = DB.tempUsers.SingleOrDefault(p => p.MENUID == menuID && p.TenentID == TID);
            if (obj.MENU_TYPE == "Left Menu")
                return "";
            else
                return " </ul> ";
        }
        public void menubind(int ModuleID)
        {
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //string userID = ((USER_MST)Session["USER"]).LOGIN_ID;
            // int MTID = Convert.ToInt32(DB.MODULE_MST.SingleOrDefault(p => p.Module_Id == ModuleID).TenentID);
            ViewState["MTID"] = TID;
            List<Database.tempUser> List = DB.tempUsers.Where(p => p.MODULE_ID == ModuleID && p.TenentID == TID && p.LocationID == LID).ToList();
            if (List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).Count() > 0)
            {
                //int MID = Convert.ToInt32(Request.QueryString["MID"]);
                //ltsMenu.DataSource = List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //ltsMenu.DataBind();

                int MasterID = Convert.ToInt32(List.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").MASTER_ID);
                //lstMaster.DataSource = List.Where(p => p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu" && p.MENU_NAME1 != "Dashboard" && p.TenentID == TID);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //lstMaster.DataBind();
            }
            else
            {
                //lstMaster.DataSource = List.Where(p => p.MENU_LOCATION == "Left Menu" && p.MENU_NAME1 != "Dashboard" && p.TenentID == TID && p.MODULE_ID == ModuleID).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //lstMaster.DataBind();
            }
            //lstisGloble.DataSource = List.Where(p => p.AMIGLOBALE == 1 && p.TenentID == TID && p.MODULE_ID == ModuleID);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
            //lstisGloble.DataBind();
        }

        protected void linkCheckIn_Click(object sender, EventArgs e)
        {
            int UserID = GetLogginID();
            Attandance Obj = new Attandance();
            Obj.UserID = GetLogginID();
            Obj.InTime = DateTime.Now;
            Obj.isAbsent = false;
            Obj.Deleted = true;
            Obj.Active = true;
            DB.Attandances.AddObject(Obj);
            DB.SaveChanges();
            linkCheckIn.Visible = false;
            linkCheckOut.Visible = true;
            //linkCheckOut.CssClass = "stdbtn btn_blue";
            //linkCheckIn.CssClass = "stdbtn";
            //linkCheckIn.ForeColor = Color.Black;
            Response.Redirect("Attendance.aspx");
        }

        protected void linkCheckOut_Click(object sender, EventArgs e)
        {
            int UserID = GetLogginID();
            DateTime TodayDate = DateTime.Now.Date;
            List<Attandance> ObjList = (from item in DB.Attandances where item.UserID == UserID && item.InTime.Value.Year == TodayDate.Year && item.InTime.Value.Month == TodayDate.Month && item.InTime.Value.Day == TodayDate.Day && item.OutTime == null select item).ToList();
            if (ObjList.Count() > 0)
            {
                int AttandanceID = ObjList[0].ID;
                Attandance Obj = DB.Attandances.Single(p => p.ID == AttandanceID);
                Obj.UserID = GetLogginID();
                Obj.OutTime = DateTime.Now;
                DB.SaveChanges();
                linkCheckIn.Visible = true;
                linkCheckOut.Visible = false;
                //linkCheckIn.CssClass = "stdbtn btn_red";
                //linkCheckOut.CssClass = "stdbtn";
                //linkCheckOut.ForeColor = Color.Black;
                Response.Redirect("Attendance.aspx");
            }
        }
        protected void btnSignin_Click(object sender, EventArgs e)
        {
            int UserID = GetLogginID();
            Attandance Obj = new Attandance();
            Obj.UserID = GetLogginID();
            Obj.InTime = DateTime.Now;
            Obj.isAbsent = false;
            Obj.Deleted = true;
            Obj.Active = true;
            DB.Attandances.AddObject(Obj);
            DB.SaveChanges();
            //btnSignin.Enabled = false;
            //btnSignOut.Enabled = true;
            //btnSignOut.CssClass = "stdbtn btn_blue";
            //btnSignin.CssClass = "stdbtn";
            //btnSignin.ForeColor = Color.Black;
            Response.Redirect("Attendance.aspx");
        }

        protected void btnSignOut_Click1(object sender, EventArgs e)
        {
            int UserID = GetLogginID();
            DateTime TodayDate = DateTime.Now.Date;
            List<Attandance> ObjList = (from item in DB.Attandances where item.UserID == UserID && item.InTime.Value.Year == TodayDate.Year && item.InTime.Value.Month == TodayDate.Month && item.InTime.Value.Day == TodayDate.Day && item.OutTime == null select item).ToList();
            if (ObjList.Count() > 0)
            {
                int AttandanceID = ObjList[0].ID;
                Attandance Obj = DB.Attandances.Single(p => p.ID == AttandanceID);
                Obj.UserID = GetLogginID();
                Obj.OutTime = DateTime.Now;
                DB.SaveChanges();
                //btnSignin.Enabled = true;
                //btnSignOut.Enabled = false;
                //btnSignin.CssClass = "stdbtn btn_red";
                //btnSignOut.CssClass = "stdbtn";
                //btnSignOut.ForeColor = Color.Black;
                Response.Redirect("Attendance.aspx");
            }
        }



    }
    #region Step2
    [Serializable]
    public class Navigation
    {
        public string Choice { get; set; }
        public int ID { get; set; }

    }
    #endregion
}