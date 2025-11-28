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
using System.Text;


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
            // ------- SESSION CHECK -------
            if (Session["USER"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            USER_MST currentUser = (USER_MST)Session["USER"];
            TID = currentUser.TenentID;
            userID = currentUser.USER_ID;
            LocationID = currentUser.LOCATION_ID;

            // ✅ CACHE COMPANY DATA IN SESSION
            int compID = ((TBLCOMPANYSETUP)Session["CustomerUser"]).COMPID;

            if (Session["CompanyAvtar"] == null)
            {
                var company = DB.TBLCOMPANYSETUPs
                    .Where(p => p.TenentID == TID && p.COMPID == compID)
                    .Select(p => p.Avtar)  // ✅ Only select needed column
                    .FirstOrDefault();

                Session["CompanyAvtar"] = company;
            }

            imgprofile.ImageUrl = "~/ReportMst/assets/global/img/" + Session["CompanyAvtar"];

            // ------- FIRST LOAD ONLY -------
            if (!IsPostBack)
            {
                // Independent operations
                BindRemainderNote();
                getCommunicatinData();
                bindLog();
                binduserlog();

                // Profile link
                Literal1.Text = $"<a href='../ReportMst/Profile.aspx?CID={compID}' " +
                               "onclick=\"return loadIframe('ifrm', this.href)\">My Profile</a>";

                CheckMaitanance();

                ifrm.Attributes.Add("onload",
                    $"setIframeHeight(document.getElementById('{ifrm.ClientID}'));");

                // ------- COOKIE & USER DATA -------
                HttpCookie cookie = Request.Cookies["tgadmin"];
                if (cookie != null)
                {
                    string lang = cookie.Values["CLANGUAGE"];
                    Session["LANGUAGE"] = lang;

                    // ✅ Only reload user if session data is old or missing
                    DateTime lastLoaded = Session["USER_LAST_LOADED"] != null
                        ? (DateTime)Session["USER_LAST_LOADED"]
                        : DateTime.MinValue;

                    if ((DateTime.Now - lastLoaded).TotalMinutes > 5)
                    {
                        // ✅ Select only needed columns
                        var userInfo = DB.USER_MST
                            .Where(p => p.USER_ID == currentUser.USER_ID && p.TenentID == TID)
                            .Select(p => new {
                                p.USER_ID,
                                p.FIRST_NAME,
                                p.EmailAddress,
                                p.LOCATION_ID,
                                p.TenentID
                                // Add other needed fields
                            })
                            .FirstOrDefault();

                        if (userInfo != null)
                        {
                            // Update only if changed
                            if (Session["Firstname"]?.ToString() != userInfo.FIRST_NAME)
                            {
                                Session["Firstname"] = userInfo.FIRST_NAME;
                            }
                            Session["USER_LAST_LOADED"] = DateTime.Now;
                        }
                    }
                }

                // ✅ Use Session data (already in memory)
                string firstName = Session["Firstname"]?.ToString() ?? currentUser.FIRST_NAME;
                Usernamee.Text = firstName;
                lbluser.Text = firstName;
                useremail.Text = currentUser.EmailAddress;

                // ------- MODULE BIND -------
                if (Session["SiteModuleID"] != null)
                {
                    int MID = Convert.ToInt32(Session["SiteModuleID"]);
                    menubind(MID);
                }

                // ------- LOCATION LOAD WITH CACHING -------
                if (ViewState["Location"] == null || string.IsNullOrEmpty(ViewState["Location"].ToString()))
                {
                    // ✅ Select only needed column
                    string locationName = DB.TBLLOCATIONs
                        .Where(p => p.TenentID == TID && p.LOCATIONID == currentUser.LOCATION_ID)
                        .Select(p => p.LOCNAME1)
                        .FirstOrDefault() ?? "Hawally";

                    ViewState["Location"] = locationName;
                }

                // ------- PAGE HISTORY -------
                Session["Previous"] = Session["Current"];
                Session["Current"] = Request.RawUrl;
            }
            // ✅ If PostBack, use cached ViewState location
            else
            {
                imgprofile.ImageUrl = "~/ReportMst/assets/global/img/" + Session["CompanyAvtar"];
            }
        }




        public void CheckMaitanance()
        {
            string filePath = Server.MapPath("test.txt");

            try
            {
                // If file does not exist, create it
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "welcome " + DateTime.Now);
                    return;
                }

                // Maintenance should run if last update was on a different day
                DateTime lastWrite = File.GetLastWriteTime(filePath);

                if (lastWrite.Date != DateTime.Now.Date)
                {
                    // Build SQL Query
                    var queries = DB.TBLMaintanances
                                    .Where(p => p.Active == true && p.SwichType == 1)
                                    .Select(p => p.Query)
                                    .ToList();

                    string finalQuery = string.Join(" ", queries);

                    using (SqlConnection con = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(finalQuery, con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();         // Correct method
                            con.Close();
                        }
                    }

                    // Update last write time after maintenance
                    File.WriteAllText(filePath, "welcome " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                // Log file with error
                File.WriteAllText(filePath,
                    "welcome " + DateTime.Now + " ERROR : " + ex.Message);

                Response.Write(ex.Message);
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
            try
            {
                var logs = DB.tblAudits
                             .Where(x => x.TENANT_ID == TID)
                             .OrderByDescending(x => x.CRUP_ID)
                             .Take(10)
                             .ToList();

                ListOrderTop10.DataSource = logs;
                ListOrderTop10.DataBind();
            }
            catch (Exception ex)
            {
                // Optional logging
                // Response.Write(ex.Message);
            }
        }


        public void binduserlog()
        {
            try
            {
                var logs = DB.tblAudits
                             .Where(x => x.TENANT_ID == TID)
                             .OrderByDescending(x => x.CRUP_ID)
                             .Take(10)
                             .ToList();

                listuserlog.DataSource = logs;
                listuserlog.DataBind();
            }
            catch (Exception)
            {
                // error handle if needed
            }
        }



        public void menubind(int ModuleID)
        {
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            long TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int uid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            DateTime today = DateTime.Now;

            // MODULE CHECK
            var module = DB.MODULE_MST.FirstOrDefault(p => p.Module_Id == ModuleID);
            if (module == null) return;

            // ✅ SINGLE DB CALL - Get all tempUser1 data at once
            var fullList = DB.tempUser1
                .Where(p => p.TenentID == TID &&
                            p.UserID == uid &&
                            p.ACTIVEUSER == true)
                .ToList(); // Load once in memory

            if (!fullList.Any()) return;

            // Filter for current module
            var moduleList = fullList
                .Where(p => p.MODULE_ID == ModuleID &&
                            p.LocationID == LID &&
                            p.ACTIVEMODULE == true &&
                            p.ACTIVEROLE == true &&
                            p.ACTIVEMENU == true)
                .OrderBy(p => p.MENU_ORDER)
                .ToList();

            if (!moduleList.Any()) return;

            // DASHBOARD
            var dashList = moduleList
                .Where(p => p.MENU_LOCATION == "Separator")
                .OrderBy(p => p.MENU_ORDER)
                .ToList();

            var firstDash = dashList.FirstOrDefault();
            if (firstDash != null)
            {
                string dashName = firstDash.MENU_NAME1;
                lblDashboard.Text =
                    $"<a href=\"{firstDash.LINK}\" onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'>" +
                    $"<span class='m-menu__item-here'></span>" +
                    $"<span class='m-menu__link-text'>{firstDash.MENU_NAME1.Replace(dashName, "Dashboard")}</span></a>";

                ifrm.Attributes.Add("src", firstDash.LINK);
            }

            // MENU SEPARATOR ITEMS VALID TILL DATE
            var activeMenu = dashList
                .Where(p => p.ACTIVETILLDATE >= today)
                .ToList();

            if (activeMenu.Any())
            {
                // ✅ NO DB CALLS in loop - use in-memory data
                var activeMenuIds = activeMenu.Select(m => m.MENUID).ToList();

                var childMenus = fullList
    .Where(p => p.MASTER_ID.HasValue &&
                activeMenuIds.Contains(p.MASTER_ID.Value) &&
                p.MENU_LOCATION == "Left Menu" &&
                p.SP5 == 1)
    .ToList();



                var uniqueMenu = activeMenu
                    .Where(m => childMenus.Any(c => c.MASTER_ID == m.MENUID))
                    .ToList();

                // SET LEFT MENU
                ltsMenu.DataSource = uniqueMenu.OrderBy(p => p.MENU_ORDER);
                ltsMenu.DataBind();

                var master = uniqueMenu.FirstOrDefault(p => p.MENU_LOCATION == "Separator");
                if (master != null)
                {
                    lstMaster.DataSource = fullList.Where(p =>
                        p.MASTER_ID == master.MASTER_ID &&
                        p.MENU_LOCATION == "Left Menu" &&
                        p.MENU_NAME1 != "Dashboard");
                    lstMaster.DataBind();
                }
            }

            // PASSWORD EXPIRY CHECK - Already have data
            var expiryDate = fullList
                .Select(p => p.ACTIVETILLDATE)
                .FirstOrDefault();

            if (expiryDate != null)
            {
                int diff = (expiryDate.Value.Date - today.Date).Days;
                if (diff <= 7)
                {
                    lbldiscription.Text = $"Your Login / password is Expired after {diff} Day On {expiryDate.Value:dd-MMM-yyyy}, please contact to Administrator...";
                    ModalPopupExtender1.Show();
                }
            }

            // GLOBAL MENU
            var globalMenus = moduleList
                .Where(p => p.AMIGLOBALE == 1)
                .ToList();

            lstisGloble.DataSource = globalMenus;
            lstisGloble.DataBind();

            // ✅ MODULE LIST - Single query with join
            var moduleIds = fullList
                .Select(p => p.MODULE_ID)
                .Distinct()
                .ToList();

            var allModules = DB.MODULE_MST
                .Where(p => moduleIds.Contains(p.Module_Id) ||
                            moduleIds.Contains((int)p.Parent_Module_id))
                .ToList();

            var parentModules = fullList
                .Select(m => m.MODULE_ID)
                .Distinct()
                .Select(modId => allModules.FirstOrDefault(p => p.Module_Id == modId))
                .Where(mod => mod != null && Convert.ToInt32(mod.Parent_Module_id) != 12)
                .Select(mod => allModules.FirstOrDefault(p =>
                    p.Module_Id == Convert.ToInt32(mod.Parent_Module_id) &&
                    p.ACTIVE_FLAG == "Y"))
                .Where(parent => parent != null)
                .GroupBy(p => p.Module_Id)
                .Select(g => g.First())
                .ToList();

            // lstmodule.DataSource = parentModules;
            // lstmodule.DataBind();
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
                    if (TID == 2 && obj.MENU_NAME1.ToString() == "POS")
                    {
                        MNAME = "Feedback";
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
                                        if (itemstr.Contains("POS/ClientTiketR.aspx"))
                                        {
                                            Session["complaint"] = itemstr;
                                            menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a id='lnkcmplnt' href='" + itemstr + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item.MENU_NAME1 + "</span><span class='badge badge" + OnOff + "'>&nbsp; </span></a></li>";
                                        }
                                        else 
                                        {
                                            
                                            menustr += "<li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a  href='" + itemstr + "' onclick=\"return loadIframe('ifrm', this.href)\" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text' >" + item.MENU_NAME1 + "</span><span class='badge badge" + OnOff + "'>&nbsp; </span></a></li>";
                                        }
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



        private string BuildUrl(string baseUrl, string encMID)
        {
            return baseUrl.Contains("?") ? $"{baseUrl}&MID={encMID}" : $"{baseUrl}?MID={encMID}";
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

            var lastTicket = DB.CRMMainActivities
                              .Where(p => p.TenentID == TID
                                       && p.USERCODE == UIN
                                       && p.ACTIVITYE == "Ticket")
                              .OrderByDescending(p => p.MasterCODE)
                              .FirstOrDefault();

            if (lastTicket != null)
            {
                ViewState["TickID"] = lastTicket.MasterCODE;

                // Agar future me bind karna ho
                // ltsRemainderNotes.DataSource = DB.CRMMainActivities
                //       .Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "Ticket")
                //       .OrderByDescending(p => p.UPDTTIME);
                // ltsRemainderNotes.DataBind();
                // UpdatePanel3.Update();
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