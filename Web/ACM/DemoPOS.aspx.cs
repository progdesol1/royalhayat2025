using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Classes;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.Objects.SqlClient;
using System.Configuration;
using System.Transactions;


namespace Web.ACM
{
    public partial class DemoPOS : System.Web.UI.Page
    {
        List<Navigation> ChoiceList = new List<Navigation>();
        Database.CallEntities DB = new Database.CallEntities();
        int TID = 0;
        int userID = 0;
        int LocationID = 0;
        SqlConnection con;
        SqlCommand command2 = new SqlCommand();
        protected void Page_Init(object sender, EventArgs e)
        {

            CheckLogin();
        }
        public void CheckLogin()
        {
            if (Session["USER"] == null || Session["USER"] == "0")
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

         
            int comp = (((TBLCOMPANYSETUP)Session["CustomerUser"]).COMPID);
            Database.TBLCOMPANYSETUP OBJupdate = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == comp);

            imgprofile.ImageUrl = "~/ReportMst/assets/global/img/" + OBJupdate.Avtar; 
            if (Session["USER"] != null && string.IsNullOrEmpty(Session["USER"].ToString()))//error
            {
                Response.Redirect("Login.aspx");
            }
           
            if (!IsPostBack)
            {
              //  TicketBind();
                BindRemainderNote();
                getCommunicatinData();
                bindLog();
                binduserlog();

                Literal1.Text = "<a href=\'../ReportMst/Profile.aspx?CID=" + comp + "' onclick=\"return loadIframe('ifrm', this.href)\">My Profile</a>";
                CheckMaitanance();
                this.ifrm.Attributes.Add("onload", "setIframeHeight(document.getElementById('" + ifrm.ClientID + "'));");
                if (HeadOffieName() == "")
                {
                    if (Session["SAFirstname"] == null)
                        Session.Abandon();
                    //Response.Redirect("Login.aspx");
                }
                else
                {
                    HttpCookie aCookie = Request.Cookies["tgadmin"];
                    //int UID = Convert.ToInt32(aCookie.Values["UserName"]);
                    int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                    string Lang = aCookie.Values["CLANGUAGE"];
                    //int Modulid = DB.MODULE_MAP.Single(p =>p.TenentID==TID && p.UserID == UID && p.SP1Name == "DefaultSet").MODULE_ID;
                    USER_MST UserList = DB.USER_MST.Single(p => p.USER_ID == UID && p.TenentID == TID);
                    Session["USER"] = UserList;
                    Session["Firstname"] = UserList.FIRST_NAME.ToString();
                    Usernamee.Text = UserList.FIRST_NAME.ToString();
                    lbluser.Text = UserList.FIRST_NAME.ToString();
                    useremail.Text = UserList.EmailAddress.ToString();
                    if (Session["SiteModuleID"] != null)
                    {
                        int Modulid = Convert.ToInt32(Session["SiteModuleID"]);
                        Session["SiteModuleID"] = Modulid;
                    }
                    Session["LANGUAGE"] = Lang;
                }
                if (Session["USER"] == null || Session["USER"] == "0")
                {
                    Session.Abandon();
                    Response.Redirect("Login.aspx");

                }


                userID = ((USER_MST)Session["USER"]).USER_ID;
                LocationID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                if (Session["SAFirstname"] != null)
                {
                    string Username = Session["SAFirstname"].ToString();
                    //lblFirstName.Text = Username;
                }
                else
                {
                    string UserName = ((USER_MST)Session["USER"]).FIRST_NAME;
                    // lblFirstName.Text = UserName.ToString();
                }
                Session["Previous"] = Session["Current"];
                Session["Current"] = Request.RawUrl;
                if (Session["SiteModuleID"] != null)
                {
                    int MID = Convert.ToInt32(Session["SiteModuleID"]);
                    menubind(MID);
                    if (DB.MODULE_MST.Where(p => p.Module_Id == MID).Count() > 0)
                    {
                        //lblmodule.Text = "Module:" + DB.MODULE_MST.Single(p => p.Module_Id == MID).Module_Name;
                    }
                }

                int UTID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                string UNAME = ((USER_MST)Session["USER"]).FIRST_NAME;
                int LID = ((USER_MST)Session["USER"]).LOCATION_ID;
                //Hawally
                if (DB.TBLLOCATIONs.Where(p => p.TenentID == TID && p.LOCATIONID == LID).Count() > 0)
                {
                    string Location1 = DB.TBLLOCATIONs.Single(p => p.TenentID == TID && p.LOCATIONID == LID).LOCNAME1;
                    ViewState["Location"] = Location1;
                }
                else
                {
                    string Location1 = "Hawally";
                    ViewState["Location"] = Location1;
                }
                //string Location = ViewState["Location"].ToString();
                //lbltentid.Text = "TID:" + TID.ToString() + " : " + Location + "\n";

                //if (TID == 0)
                //{
                //    lbltentid.Visible = false;
                //    lblusername.Visible = true;
                //}
                //lblusername.Text = "User:" + UNAME.ToString();

                // For Attandance
                //int UserID = GetLogginID();
                //DateTime TodayDate = DateTime.Now.Date;
                //List<Attandance> ObjList = (from item in DB.Attandances where item.UserID == UserID && item.InTime.Value.Year == TodayDate.Year && item.InTime.Value.Month == TodayDate.Month && item.InTime.Value.Day == TodayDate.Day select item).ToList();
                //if (ObjList.Where(p => p.OutTime == null).Count() > 0)
                //{
                //    linkCheckIn.Visible = false;
                //    if (TID == 7)
                //        linkCheckOut.Visible = false;
                //    else
                //        linkCheckOut.Visible = true;
                //}
                //else
                //{
                //    //if (ObjList.Count() == 0)
                //    //{
                //    //    Attandance Obj = new Attandance();
                //    //    Obj.UserID = GetLogginID();
                //    //    Obj.InTime = DateTime.Now;
                //    //    Obj.isAbsent = false;
                //    //    Obj.Deleted = true;
                //    //    Obj.Active = true;
                //    //    DB.Attandances.AddObject(Obj);
                //    //    DB.SaveChanges();
                //    //    //linkCheckIn.Visible = false;
                //    //    //linkCheckOut.Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    //linkCheckIn.Visible = true;
                //    //    //btnSignin.CssClass = "stdbtn btn_red";
                //    //    //btnSignOut.CssClass = "stdbtn";
                //    //    //linkCheckIn.BackColor = Color.Red;
                //    //    //btnSignOut.ForeColor = Color.Black;
                //    //    //linkCheckOut.Visible = false;
                //    //}
                //}
                //string Loggo = Classes.EcommAdminClass.Logo(TID);
                //LOGOTODISPLAY.ImageUrl = "../assets/" + Loggo;


            }
        }



        public void CheckMaitanance()
        {
            try
            {
                if (!File.Exists(Server.MapPath("test.txt")))
                {
                    File.WriteAllText(Server.MapPath("test.txt"), "welcome" + DateTime.Now.ToString());
                }
                else
                {
                    if (File.GetLastWriteTime(Server.MapPath("test.txt")).Day != DateTime.Now.Day)
                    {
                        string qry = "";
                        con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
                        List<Database.TBLMaintanance> listTBLMaintanances = DB.TBLMaintanances.Where(p => p.Active == true && p.SwichType == 1).ToList();
                        foreach (Database.TBLMaintanance item in listTBLMaintanances)
                            qry += item.Query;
                        command2 = new SqlCommand(qry, con);
                        con.Open();
                        command2.ExecuteReader();
                        con.Close();
                        File.WriteAllText(Server.MapPath("test.txt"), "welcome" + DateTime.Now.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                File.WriteAllText(Server.MapPath("test.txt"), "welcome" + DateTime.Now.ToString() + "Erorr : " + ex.Message);
            }
        }
        public string HeadOffieName()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["tgadmin"] != null)
                {
                    return HttpContext.Current.Request.Cookies["tgadmin"]["UserName"];
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public void bindLog()
        {
          
            List<Database.tblAudit> list1 = DB.tblAudits.Where(p => p.TENANT_ID == TID).OrderByDescending(p=>p.CRUP_ID).Take(10).ToList();
            ListOrderTop10.DataSource = list1;
            ListOrderTop10.DataBind();
        }

        public void binduserlog()
        {
            List<Database.tblAudit> list1 = DB.tblAudits.Where(p => p.TENANT_ID == TID).OrderByDescending(p=>p.CRUP_ID).Take(10).ToList();
            listuserlog.DataSource = list1;
            listuserlog.DataBind();
            
        }


        public void menubind(int ModuleID)
        {
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int ROLLID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
            int uid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            if (DB.MODULE_MST.Where(p => p.Module_Id == ModuleID).Count() > 0)
            {
                ViewState["MTID"] = TID;
                List<Database.tempUser1> List = DB.tempUser1.Where(p => p.MODULE_ID == ModuleID && p.TenentID == TID && p.LocationID == LID && p.ACTIVEUSER == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.UserID == uid && p.ACTIVEMENU == true).OrderBy(p => p.MENU_ORDER).ToList();

                if (List.Count() > 0)
                {
                    List<Database.tempUser1> ListDash = List.Where(p => p.MENU_LOCATION == "Separator").OrderBy(p => p.MENU_ORDER).ToList();
                    string Dashname = ListDash[0].MENU_NAME1;
                    //lblDashboard.Text = " <a href=\"" + List[0].LINK + "\" onclick=\"return loadIframe('ifrm', this.href)\">" + List[0].MENU_NAME1.Replace(Dashname, "Dashboard") + "</span> </a>";
                    lblDashboard.Text = "<a href=\"" + ListDash[0].LINK + "\" onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'> <span class='m-menu__item-here'></span><span class='m-menu__link-text'>" + ListDash[0].MENU_NAME1.Replace(Dashname, "Dashboard") + "</span></a>";//" + List[0].MENU_NAME1.Replace(Dashname, "Dashboard") + "
                    ifrm.Attributes.Add("src", ListDash[0].LINK);

                    if (List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).Count() > 0)// && p.ACTIVEMENU == true                        
                    {
                        //
                        List<Database.tempUser1> MenuMain = List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).ToList();
                        List<Database.tempUser1> Temp = new List<Database.tempUser1>();
                        foreach (Database.tempUser1 mitem in MenuMain)
                        {
                            if (DB.tempUser1.Where(p => p.TenentID == TID && p.MASTER_ID == mitem.MENUID && p.MENU_LOCATION == "Left Menu" && p.SP5 == 1).Count() > 0)
                            {
                                Database.tempUser1 mobj = DB.tempUser1.Single(p => p.TenentID == TID && p.MENUID == mitem.MENUID && p.UserID == uid);
                                Temp.Add(mobj);
                            }
                        }
                        //
                        List = Temp;
                        ltsMenu.DataSource = List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).OrderBy(a => a.MENU_ORDER);// && p.ACTIVEMENU == true
                        ltsMenu.DataBind();

                        int MasterID = Convert.ToInt32(List.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").MASTER_ID);// && p.ACTIVEMENU == true
                        lstMaster.DataSource = List.Where(p => p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu" && p.MENU_NAME1 != "Dashboard" && p.TenentID == TID);// && p.ACTIVEMENU == true
                        lstMaster.DataBind();

                        //Admin Setup 
                        string uidd = uid.ToString();
                        if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID == uidd).Count() > 0)
                        {

                        }
                        else
                        {
                            foreach (Database.tempUser1 Adminitem in List)
                            {
                                if (Adminitem.URLREWRITE == "/ACM/ACM_NewUsewSetupScreen.aspx")
                                {
                                    int MIDD = Convert.ToInt32(Adminitem.MASTER_ID);
                                    List = List.Where(p => p.MENUID != MIDD && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).OrderBy(a => a.MENU_ORDER).ToList();// && p.ACTIVEMENU == true
                                    ltsMenu.DataSource = List;
                                    ltsMenu.DataBind();

                                    int MasterIDd = Convert.ToInt32(List.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").MASTER_ID);// && p.ACTIVEMENU == true
                                    lstMaster.DataSource = List.Where(p => p.MASTER_ID == MasterIDd && p.MENU_LOCATION == "Left Menu" && p.MENU_NAME1 != "Dashboard" && p.TenentID == TID);// && p.ACTIVEMENU == true
                                    lstMaster.DataBind();
                                }
                            }

                        }//Admin Setup ///////////////
                    }
                    else
                    {
                        //lbldiscription.Text = "This " + ModielName + "You has No Any Menu";
                        //ModalPopupExtender1.Show();
                    }
                    if(DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == uid).Count() > 0)
                    {
                        string dt = Convert.ToDateTime(DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == uid).Select(p => p.ACTIVETILLDATE).First()).ToShortDateString();
                        if (!String.IsNullOrEmpty(dt))
                        {
                            DateTime Expdt = Convert.ToDateTime(dt).Date;
                            DateTime TDT = DateTime.Now.Date;
                            int Diff = Convert.ToInt32((Expdt - TDT).TotalDays);
                            if (Diff <= 7)
                            {
                                lbldiscription.Text = "Your Login / password is Expired after " + Diff + " Day On " + Expdt.ToString("dd-MMM-yyyy") + ", please contact to Administrator...";
                                ModalPopupExtender1.Show();
                            }

                        }
                    }
                    List = List.Where(p => p.AMIGLOBALE == 1 && p.TenentID == TID && p.MODULE_ID == ModuleID).ToList();// && p.ACTIVEMENU == true
                    lstisGloble.DataSource = List;//List.Where(p => p.AMIGLOBALE == 1 && p.TenentID == TID && p.MODULE_ID == ModuleID && p.ACTIVEMENU == true);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                    lstisGloble.DataBind();

                    
                }
                else
                {
                    //lbldiscription.Text = "Your Login / password is Expired , please contact to Administrator...";
                    //ModalPopupExtender1.Show();
                }
            }


            List<Database.tempUser1> ModulList = DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == uid).ToList();
            List<Database.MODULE_MST> ListMST = new List<MODULE_MST>();
            if (ModulList.Where(p => p.TenentID == TID).Count() > 0)
            {
                var List1 = ModulList.GroupBy(p => p.MODULE_ID).Select(p => p.FirstOrDefault()).ToList();

                foreach (Database.tempUser1 items in List1)
                {
                    if (TID == 0)
                    {
                        if (DB.MODULE_MST.Where(p => p.Module_Id == items.MODULE_ID).Count() > 0)
                        {
                            int perent = Convert.ToInt32(DB.MODULE_MST.Single(p => p.Module_Id == items.MODULE_ID).Parent_Module_id);
                            if (DB.MODULE_MST.Where(p => p.Module_Id == items.MODULE_ID && items.ACTIVEMODULE == true).Count() > 0)//items.ACTIVEMODULE == true
                            {
                                Database.MODULE_MST objMST = DB.MODULE_MST.Single(p => p.ACTIVE_FLAG == "Y" && p.Module_Id == perent);
                                ListMST.Add(objMST);
                            }

                        }
                    }
                    else
                    {
                        if (DB.MODULE_MST.Where(p => p.Module_Id == items.MODULE_ID).Count() > 0)
                        {
                            int perent = Convert.ToInt32(DB.MODULE_MST.Single(p => p.Module_Id == items.MODULE_ID).Parent_Module_id);
                            if (perent != 12)
                            {
                                if (DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Module_Id == perent).Count() > 0)
                                {
                                    if (DB.MODULE_MST.Where(p => p.Module_Id == items.MODULE_ID && items.ACTIVEMODULE == true).Count() > 0)//items.ACTIVEMODULE == true
                                    {
                                        Database.MODULE_MST objMST = DB.MODULE_MST.Single(p => p.ACTIVE_FLAG == "Y" && p.Module_Id == perent);
                                        ListMST.Add(objMST);
                                    }
                                }
                            }
                        }
                    }
                }
                //lstmodule.DataSource = ListMST.GroupBy(p => p.Module_Id).Select(p => p.FirstOrDefault()).ToList();// DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == 0 && p.TenentID == TID && p.Module_Id != 12);
                //lstmodule.DataBind();
            }

        }

        //protected void lstmodule_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        Label lblHide = (Label)e.Item.FindControl("lblHide");
        //        int MID = Convert.ToInt32(lblHide.Text);
        //        ListView ltsProduct = (ListView)e.Item.FindControl("lstsubmodule");
        //        List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == MID).ToList();
        //        ltsProduct.DataSource = List;
        //        ltsProduct.DataBind();
        //        //menubind(MID);
        //    }
        //}

        protected void lstsubmodule_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkModule_Id")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Session["SiteModuleID"] = ID;
                menubind(ID);
            }
        }
        public string GetLname(int MMID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string MNAME = "";
            if (DB.FUNCTION_MST.Where(p => p.MENU_ID == MMID).Count() > 0)
            {
                var obj = DB.FUNCTION_MST.SingleOrDefault(p => p.MENU_ID == MMID);
                if (obj.MENU_LOCATION == "Separator")
                {
                    MNAME = obj.MENU_NAME1.ToString();
                    if(TID == 2 && obj.MENU_NAME1.ToString() == "POS")
                    {
                        MNAME = "Complain";
                    }
                }
            }
            return MNAME;
        }
        public string GetLink(int menuID)
        {
            string menustr = "";
            int MTID = Convert.ToInt32(ViewState["MTID"]);
            string OnOff = "";
            if (DB.FUNCTION_MST.Where(p => p.MENU_ID == menuID).Count() > 0) //&& p.TenentID == MTID  && p.ACTIVE_FLAG == 1
            {

                var obj = DB.FUNCTION_MST.SingleOrDefault(p => p.MENU_ID == menuID); // && p.TenentID == MTID && p.ACTIVE_FLAG == 1
                //OnOff = obj.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp";
                string MID1 = obj.MASTER_ID.ToString();
                string MenuID1 = obj.MENU_ID.ToString();
                string ENCMID1 = Classes.GlobleClass.EncryptionHelpers.Encrypt(MID1 + "~" + MenuID1).ToString();
                string itemstr1 = obj.URLREWRITE.ToString().Contains("?") ? obj.URLREWRITE.ToString() + "&MID=" + ENCMID1 : obj.URLREWRITE.ToString() + "?MID=" + ENCMID1;
                if (obj.MENU_LOCATION == "Separator")
                {
                    //OnOff = obj.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp";//&& obj.ACTIVEMENU == true && obj.MENUDATE.Value >= DateTime.Now.Date                    
                    //menustr = "<a href='#' class='m-menu__link m-menu__toggle'><span class='m-menu__item-here'></span><span class='m-menu__link-text'>" + obj.MENU_NAME1 + "<i class='m-menu__hor-arrow la la-angle-down'></i><i class='m-menu__ver-arrow la la-angle-right'></i></a>";
                    menustr += "<div class='m-menu__submenu m-menu__submenu--classic m-menu__submenu--left'><span class='m-menu__arrow m-menu__arrow--adjust'></span>";
                    menustr += " <ul class='m-menu__subnav'>";
                    List<Database.FUNCTION_MST> itemList = DB.FUNCTION_MST.Where(p => p.MASTER_ID == menuID).OrderBy(a => a.MENU_ORDER).ToList(); // && p.TenentID == MTID && p.ACTIVE_FLAG == 1
                    //if (OnOff == "-success'>&nbsp;")
                    //{
                    if (itemList.Count() > 0)
                    {
                        foreach (Database.FUNCTION_MST item in itemList)
                        {
                            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                            int uid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                            if (DB.tempUser1.Where(p => p.TenentID == TID && p.MENUID == item.MENU_ID && p.UserID == uid && p.SP5 == 1).Count() > 0)// && p.ACTIVEMENU == true
                            {
                                Database.tempUser1 Tempobj = DB.tempUser1.Single(p => p.TenentID == TID && p.MENUID == item.MENU_ID && p.UserID == uid);
                                bool MT = Convert.ToBoolean(Tempobj.ACTIVEMENU);
                                OnOff = MT == true ? "-success" : "-danger";
                                string itemstr = "";
                                string MID = item.MASTER_ID.ToString();
                                string MenuID = item.MENU_ID.ToString();
                                string ENCMID = Classes.GlobleClass.EncryptionHelpers.Encrypt(MID + "~" + MenuID).ToString();
                                //OnOff = item.ACTIVE_FLAG == 1 && MT == true ? "-success'>&nbsp;" : "-danger'>&nbsp"; //&& item.ACTIVEMENU == true && item.MENUDATE.Value >= DateTime.Now.Date 
                                List<Database.FUNCTION_MST> itemList1 = DB.FUNCTION_MST.Where(p => p.MASTER_ID == item.MENU_ID).OrderBy(a => a.MENU_ORDER).ToList(); // && p.TenentID == MTID
                                itemstr = OnOff == "-success" ? item.URLREWRITE.ToString().Contains("?") ? item.URLREWRITE.ToString() + "&MID=" + ENCMID : item.URLREWRITE.ToString() + "?MID=" + ENCMID : "/ACM/Expired1.aspx";
                                if (itemList1.Count() > 0)
                                {
                                    menustr += "<li class='m-menu__item' data-redirect='true' data-menu-submenu-toggle='hover' aria-haspopup='true'><a href='" + itemstr + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item.MENU_NAME1 + "</span></span></a></li>";
                                    menustr += "<div class='m-menu__submenu m-menu__submenu--classic m-menu__submenu--right'><span class='m-menu__arrow '></span>";
                                    menustr += "<ul class='m-menu__subnav'>";
                                    foreach (Database.FUNCTION_MST item1 in itemList1)
                                    {
                                        //OnOff = item1.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp";
                                        List<Database.FUNCTION_MST> itemList2 = DB.FUNCTION_MST.Where(p => p.MASTER_ID == item1.MENU_ID).OrderBy(a => a.MENU_ORDER).ToList();// && p.TenentID == MTID
                                        string item1str = item1.URLREWRITE.ToString().Contains("?") ? item.URLREWRITE.ToString() + "&MID=" + ENCMID : item.URLREWRITE.ToString() + "?MID=" + ENCMID;
                                        if (itemList2.Count() > 0)
                                        {
                                            menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='" + item1str + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item1.MENU_NAME1 + "</a></li>";
                                            menustr += "<ul class='m-menu__subnav'>";
                                            foreach (Database.FUNCTION_MST item2 in itemList2)
                                            {
                                                string item2str = item2.URLREWRITE.ToString().Contains("?") ? item.URLREWRITE.ToString() + "&MID=" + ENCMID : item.URLREWRITE.ToString() + "?MID=" + ENCMID;
                                                //OnOff = item2.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp";                                                    
                                                menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='" + item2str + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item2.MENU_NAME1 + "</span></a></li>";
                                            }
                                            menustr += Displaysubmenu1(menuID);
                                            menustr += "</li>";
                                        }
                                        else
                                        {
                                            menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='" + item1str + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item1.MENU_NAME1 + "</span></a></li>";
                                        }
                                    }
                                    menustr += Displaysubmenu1(menuID);
                                    menustr += "</li>";
                                }
                                else
                                {
                                    if (itemstr.Contains("/ReportMst/Profile"))
                                    {
                                        int comp = (((TBLCOMPANYSETUP)Session["CustomerUser"]).COMPID);//
                                        menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='" + itemstr + "?CID=" + comp + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item.MENU_NAME1 + "</span></a></li>";
                                    }
                                    else
                                    {
                                        menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='" + itemstr + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item.MENU_NAME1 + "</span><span class='badge badge" + OnOff + "'>&nbsp; </span></a></li>";
                                    }
                                }
                            }
                        }

                    }


                }
                else
                {
                    menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='" + itemstr1 + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + obj.MENU_NAME1 + "</span></a></li>";
                }
            }

            return menustr;
        }

        public string Displaysubmenu1(int menuID)
        {
            int MTID = Convert.ToInt32(ViewState["MTID"]);
            var obj = DB.FUNCTION_MST.SingleOrDefault(p => p.MENU_ID == menuID); // && p.TenentID == MTID  && p.ACTIVE_FLAG == 1
            if (obj.MENU_LOCATION == "Left Menu")
                return "";
            else
                return " </ul></div> ";
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx?logout=logout");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            int comp = (((TBLCOMPANYSETUP)Session["CustomerUser"]).COMPID);//
            string itemstr = "../ReportMst/Profile.aspx?CID=" + comp;
            //string Prof = "<a href='" + itemstr + "' onclick=\"return loadIframe('ifrm', this.href)\"></a>";
            //Response.Redirect("../ReportMst/Profile.aspx?CID=" + comp);
            ifrm.Attributes.Add("src", itemstr);
        }

        public string getdepartmentname(int Department)
        {
            if (Department == 0 || Department == null)
            {
                return null;
            }
            else
            {
                return DB.DeptITSupers.SingleOrDefault(p => p.DeptID == Department && p.TenentID == TID).DeptName;
            }

        }


        protected void LinkButton1_Click1(object sender, EventArgs e)
        {

          //  Classes.POSSynchronization.Synchonizes();
            Response.Redirect("../POS/ClientTiketR.aspx");
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
       
        public string GetCat(string Catid)
        {
            int Cid = Convert.ToInt32(Catid);
            string cname = DB.CAT_MST.Single(p => p.TenentID == TID && p.CATID == Cid).CAT_NAME1;
            return cname;
        }

        public string getname(int ID)
        {
            int Cid = Convert.ToInt32(ID);
            string name = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == Cid).FIRST_NAME;
            return name;

        }
        public void BindRemainderNote()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").Count() > 0)
            {
                var result = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.MasterCODE);
                var resu1 = result.FirstOrDefault();
                int TickID = resu1.MasterCODE;
                ViewState["TickID"] = TickID;

                //ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                //ltsRemainderNotes.DataBind();
                //UpdatePanel3.Update();
            }
        }
        protected void ltsRemainderNotes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "TicketNO")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);

                string[] ID = e.CommandArgument.ToString().Split('~');
                int Mastercode = Convert.ToInt32(ID[1]);
                ViewState["TickID"] = Mastercode;

                //ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "Ticket").OrderByDescending(p => p.UPDTTIME);
                //ltsRemainderNotes.DataBind();
                //UpdatePanel3.Update();
            }
        }
        protected void ltsRemainderNotes_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton LinkTicketon = (LinkButton)e.Item.FindControl("LinkTicketon");
            int TICK = Convert.ToInt32(LinkTicketon.Text);
            int TickID = Convert.ToInt32(ViewState["TickID"]);

            if (TickID == TICK)
            {
                LinkTicketon.Attributes["class"] = "m-badge m-badge--info m-badge--wide";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            getCommunicatinData();
        }
        public void getCommunicatinData()
        {
            if (ViewState["TickID"] != null)
            {
                int Tikitno = Convert.ToInt32(ViewState["TickID"]);
                //int Tikitno = Convert.ToInt32(303);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                List<Database.CRMActivity> CRMACTList = DB.CRMActivities.Where(p => (p.TenentID == TID || p.TenentID == 0) && p.GroupBy == "Ticket" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME).ToList();
                listChet.DataSource = CRMACTList;
                listChet.DataBind();

                UpdatePanel1.Update();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
            if (ViewState["TickID"] != null)
            {
                int tiki = Convert.ToInt32(ViewState["TickID"]);
                //int tiki = Convert.ToInt32(303);


                int MsterCose = tiki;
                int TenentID = TID;
                int COMPID = CID;
                int MasterCODE = MsterCose;
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
                string Version1 = strUName;
                string MyStatus = "Pending";
                string GroupBy = "Ticket";
                int DocID = 0;
                string Active = "Y";
                int MainSubRefNo = 0;
                string URL = Request.Url.AbsolutePath;
                int Type = 0;
                int ToColumn = 0;
                int FromColumn = 0;
                if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.COMPID == CID && MasterCODE == tiki).Count() == 1)
                {
                    Database.CRMMainActivity objdeptcat = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.COMPID == CID && MasterCODE == tiki);
                    Type = Convert.ToInt32(objdeptcat.Type);
                    ToColumn = Convert.ToInt32(objdeptcat.MainID);
                    FromColumn = Convert.ToInt32(objdeptcat.ModuleID);
                }
                Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL);

                //Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
                //objCRMMainActivities.MyStatus = "Pending";
                //DB.SaveChanges();
                txtComent.Text = "";
                getCommunicatinData();


            }
        }
        public string getclassess(int id)
        {
            if (id == 0)
            {
                return "m-messenger__message m-messenger__message--out";
            }
            else
            {
                return "m-messenger__message m-messenger__message--in";
            }
        }




















    }
}