
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
using System.Web.Configuration;
using System.Threading;
using Classes;

namespace Web.Master
{
    public partial class AcmMaster : System.Web.UI.MasterPage
    {
        Database.CallEntities DB = new Database.CallEntities();
       // Database.CallEntities DB1 = new Database.CallEntities();
        List<Navigation> ChoiceList = new List<Navigation>();
        protected void Page_Init(object sender, EventArgs e)
        {
            CheckLogin();
        }
        public void CheckLogin()
        {
            if (Session["USER"] == null || Session["USER"] == "0")
            {
                Session.Abandon();
                Response.Redirect("/ACM/SessionTimeOut.aspx");

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////Cacheyogesh
                //Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
                //Response.Cache.SetCacheability(
                //HttpCacheability.ServerAndPrivate);
                //Response.Cache.SetValidUntilExpires(true);

                //int UserID=
                Session["Previous"] = Session["Current"];
                Session["Current"] = Request.RawUrl;
                Session["ADMInPrevious"] = Session["ADMInCurrent"];
                Session["ADMInCurrent"] = Request.RawUrl;
                //string userID = ((USER_MST)Session["USER"]).LOGIN_ID;
                //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                 int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //string Password = (((USER_MST)Session["USER"]).PASSWORD);
                int userID1 = 3031;
                //int userID1 = ((USER_MST)Session["USER"]).USER_ID;
                int usertype = 1;
                //int usertype = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                int LocationID = 1;
                //int LocationID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);



                
                //string NewLogText = ((USER_MST)Session["USER"]).FIRST_NAME;
                //string UID1 = ((USER_MST)Session["USER"]).LAST_NAME;
                //int CrupID = Convert.ToInt32(((USER_MST)Session["USER"]).CRUP_ID);
                //string table = ((USER_MST)Session["USER"]).FIRST_NAME;
                //string UserName = ((USER_MST)Session["USER"]).FIRST_NAME;
                //GlobleClass.UpdateLog(NewLogText, CrupID, table, UserName, TID, UID1, LocationID);


                //lblFirstName.Text = userID.ToString();
                if (Request.QueryString["MID"] != null)
                {
                        string Menuid = Request.QueryString["MID"].ToString();
                        GetMenuID(Menuid);
                        //string MenuName = Classes.GlobleClass.EncryptionHelpers.Decrypt(Menuid);
                        //string[] MenuidQwe = MenuName.Split('~');
                        //int Meni = Convert.ToInt32(MenuidQwe[1]);

                        //FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.TenentID == TID && p.MENU_ID == Meni);
                        //lblpagename.Text = obj.MENU_NAME1;
                        //lblpageid.Text = Meni.ToString();                      

                }
                else
                {
                    //int MID = Convert.ToInt32(Request.QueryString["MID"]);
                    //Database.USER_MST obj_USER_MST=DB1.USER_MST.SingleOrDefault(p=>p.LOGIN_ID==userID && p.PASSWORD==Password);
                    //if(DB1.ROLE_MST.Where(p=>p.u==))
                    if (usertype==15)
                    {
                        //GlobleClass.DeleteTempUser(TID, userID1, LocationID, 10);
                        //GlobleClass.getMenuGloble(TID, userID1, LocationID, 10);
                    }
                    else
                    {
                        if (usertype==1005)
                        {
                            //GlobleClass.DeleteTempUser(TID, userID1, LocationID, 9);
                            //GlobleClass.getMenuGloble(TID, userID1, LocationID, 9);
                        }
                       
                      
                    }
                   
                   
                }
                bindModule();
                BindMeniList();
                //string sourceDirectory = Server.MapPath("~/Admin");
                //DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirectory);

                ////var aspxFiles = Directory.EnumerateFiles(sourceDirectory, "*.aspx", SearchOption.TopDirectoryOnly).Select(Path.GetFileName);
                ////  var results = DB.MetadataWorkspace.GetItems<EntityType>(System.Data.Metadata.Edm.DataSpace.CSpace).Select(p => p.Name).ToList();
                //var config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
                //var section = (UrlMappingsSection)config.GetSection("system.web/urlMappings");
                //var catArray = new ArrayList();
                //var arcArray = new ArrayList();
                //bool AnyChanges = false;
                //foreach (url strCategory in DB.urls.ToList())
                //{

                //    if (!catArray.Contains(strCategory) && !section.UrlMappings.AllKeys.Contains("~/" + strCategory))
                //    {
                //        catArray.Add(strCategory);
                //        section.UrlMappings.Add(new UrlMapping("~/Admin/" + strCategory.short_url, "~/Admin/" + strCategory.real_url));
                //        AnyChanges = true;
                //        //url obj = new url();
                //        //obj.short_url = strCategory.Replace(".aspx", "");
                //        //obj.real_url = strCategory;
                //        //obj.create_date = DateTime.Now;
                //        //obj.created_by = "1";
                //        //DB.urls.AddObject(obj);
                //        //DB.SaveChanges();
                //    }
                //}
                //if (AnyChanges) config.Save();
                //List<Database.tempUser> List = DB.tempUser.Where(p => p.TenentID == 1 && p.UserID == 1026 && p.ROLE_ID != 116 && p.ACTIVEMENU == true).ToList();
                //int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
                //if(RoleID==116)
                //{
                //    List<Database.tempUser> ListSuplier = DB.tempUser.Where(p => p.TenentID == 1 && p.UserID == 1026 && p.ROLE_ID == 116 && p.ACTIVEMENU == true).ToList();
                //    if (ListSuplier.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").Count() > 0)
                //    {
                //        int MID = Convert.ToInt32(Request.QueryString["MID"]);
                //        ltsMenu.DataSource = ListSuplier.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //        ltsMenu.DataBind();

                //        int MasterID = Convert.ToInt32(ListSuplier.First(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator").MASTER_ID);
                //        lstMaster.DataSource = ListSuplier.Where(p => p.MASTER_ID == MasterID && p.MENU_TYPE == "Left Menu" && p.MENU_NAME1 != "Dashboard");//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //        lstMaster.DataBind();
                //    }
                //    else
                //    {
                //        lstMaster.DataSource = ListSuplier.Where(p => p.MENU_TYPE == "Left Menu" ).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //        lstMaster.DataBind();
                //    }
                //}
                //else
                //{
                //    if (List.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").Count() > 0)
                //    {
                //        int MID = Convert.ToInt32(Request.QueryString["MID"]);
                //        ltsMenu.DataSource = List.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //        ltsMenu.DataBind();

                //        int MasterID = Convert.ToInt32(List.First(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator").MASTER_ID);
                //        lstMaster.DataSource = List.Where(p => p.MASTER_ID == MasterID && p.MENU_TYPE == "Left Menu" && p.MENU_NAME1 != "Dashboard");//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //        lstMaster.DataBind();
                //    }
                //    else
                //    {
                //        lstMaster.DataSource = List.Where(p => p.MENU_TYPE == "Left Menu" && p.MENU_NAME1 != "Dashboard").OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //        lstMaster.DataBind();
                //    }
                //}
                //lstisGloble.DataSource = List.Where(p => p.AMIGLOBALE == 1);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //lstisGloble.DataBind();
                menubind(10);
            }

        }

        public int GetMenuID(string Menuid)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string MenuName = Classes.GlobleClass.EncryptionHelpers.Decrypt(Menuid);
            string[] MenuidQwe = MenuName.Split('~');
            int Meni = Convert.ToInt32(MenuidQwe[1]);

            Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.MENU_ID == Meni);// p.TenentID == TID && 
            lblpagename.Text = obj.MENU_NAME1;
            lblpageid.Text = Meni.ToString();

            return Meni;
        }
        public string SessionLoad1(int TID, int LID, int UID, int EMPID, string LangID)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            string Ref = TID.ToString() + "," + LID.ToString() + "," + UID.ToString() + "," + EMPID.ToString() + "," + LangID;
            return Ref;
        }
        public void bindModule()
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
           
                //lstmodule.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == 0&&p.TenentID==TID);
                //lstmodule.DataBind();
           
        }
        protected void lstmodule_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int TID = 1;
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblHide = (Label)e.Item.FindControl("lblHide");
                int MID = Convert.ToInt32(lblHide.Text);
                ListView ltsProduct = (ListView)e.Item.FindControl("lstsubmodule");
                ltsProduct.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == MID&&p.TenentID==TID);
                ltsProduct.DataBind();
                //menubind(MID);
            }

        }

        public string getbarend(int BID)
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.REFTABLEs.Single(p => p.REFID == BID&&p.TenentID==TID).REFNAME1;
        }

        public string ProductName(int PID)
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).ProdName1;
        }
        public string CATNAME(int CID)
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.CAT_MST.Single(p => p.CATID == CID && p.TenentID == TID).CAT_NAME1;
        }       
        public string Displaysubmenu1(int menuID)
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            var obj = DB.FUNCTION_MST.SingleOrDefault(p => p.MENU_ID == menuID);
            if (obj.MENU_TYPE == "Left Menu")
                return "";
            else
                return " </ul> ";
        }
       

      
        bool flag = false;
        protected void lstmenu_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblMenuHide = (Label)e.Item.FindControl("lblMenuHide");
                int MID = Convert.ToInt32(lblMenuHide.Text);

                if (flag == false)
                    flag = true;
                else
                {
                    ListView ltsProduct = (ListView)e.Item.FindControl("lstsubMenu");
                    ltsProduct.DataSource = DB.tempUsers.Where(p => p.ACTIVEMENU == true && p.TenentID == TID && p.MASTER_ID == MID);
                    ltsProduct.DataBind();
                }

            }

        }


        public void BindMeniList()
        {
            //int TID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            // List<tempUser> result1 = Classes.Globle.EncryptionHelpers.getMenu(TID);

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

            //Yogesh menu
            //   haresh     //  ltsMenu.DataSource = DB.tempUser.Where(p => p.ACTIVEMENU == true && p.MENU_TYPE == "Separator" || p.AMIGLOBALE == 1).OrderBy(a => a.MENU_ORDER);
            // ltsMenu.DataBind();
        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/ECOMM/Login.aspx");
        }

        public int GetHRMSLogginType()
        {
            return Convert.ToInt32(((DB.tblSetupCrms.Where(p => p.IsAdmin == true).First())).UserID);
        }
        public int GetLogginID()
        {
            return Convert.ToInt32(((Database.USER_MST)Session["USER"]).USER_ID);
        }

        public string getOwnPage()
        {
            string PageID = "0";
            if (Request.Url.AbsolutePath.Contains("Job_Title.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Job_Title.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Opportunity_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Opportunity_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Lead_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Lead_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Contact_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Contact_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Account_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Account_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Campaign_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Campaign_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("WebProduct.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "WebProduct.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("MYBUS.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "MYBUS.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Fetaures_List_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Fetaures_List_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("PRODUCTMASTER.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "PRODUCTMASTER.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("defultvalue.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "defultvalue.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ACMManageBanners.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ACMManageBanners.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Banners.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Banners.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Transaction.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Transaction.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLPERIODS.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLPERIODS.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Supplier_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Supplier_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Web_User_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Web_User_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("SupplierProductMaster.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "PRODUCTMASTER.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("SupplierRge.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Web_User_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ICUOM.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ICUOM.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLCOLOR.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLCOLOR.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLLOCATION.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLLOCATION.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLSIZE.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLSIZE.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLPROJECT.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLPROJECT.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLGROUP.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLGROUP.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblCOUNTRY.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblCOUNTRY.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ISCampignStatus.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ISCampignStatus.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblLanguage.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblLanguage.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblStates.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblStates.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Tbl_Position_Mst.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Tbl_Position_Mst.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLSYSTEMS.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLSYSTEMS.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tbl_ISSearchUser_Master.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tbl_ISSearchUser_Master.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("test.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "test.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Img_upload_Label.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Img_upload_Label.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ICUOMCONV.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ICUOMCONV.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLMaintanance.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLMaintanance.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Tbl_Event_Main.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Tbl_Event_Main.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tbl_Event_Detail.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tbl_Event_Detail.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Tbl_Event_Register.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Tbl_Event_Register.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblContract_driver.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblContract_driver.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblProduct_Plan.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblProduct_Plan.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblProduct_SubPlan.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblProduct_SubPlan.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblcontact_addon1.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblcontact_addon1.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLCONTACT_DEL_ADRES.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLCONTACT_DEL_ADRES.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("planmealsetup.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "planmealsetup.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("PlanmealsetupUOM.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "planmealsetup.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("TBLGROUP_USER.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "TBLGROUP_USER.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("HBInvoice.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "HBInvoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("HBContract.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "HBInvoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("GYMContactMaster.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "GYMContactMaster.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("GYMInvoice.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "GYMContactMaster.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Appointment.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Appointment.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("SenegalPageContents.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "SenegalPageContents.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblSenegalmain.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblSenegalmain.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tbl_Employee.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tbl_Employee.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblSetupInventory.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblSetupInventory.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblsetupsalesh.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblsetupsalesh.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblSubscriptionSetup.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblSubscriptionSetup.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tbl_Receipe.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tbl_Receipe.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("tblCityStatesCounty.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "tblCityStatesCounty.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("DeptITSuper.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "DeptITSuper.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ICCATEGORY.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ICCATEGORY.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("ICSUBCATEGORY.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "ICSUBCATEGORY.aspx").TableName.ToString();
            }
            //
            return PageID;
        }
        public List<Database.TBLLabelDTL> Bindxml(string pagename)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("//Master//xml//" + pagename + ".xml"));
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
            ChoiceID = ChoiceID - 12;
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
        //public void GetCurrentNavigation(int ChoiceID, int Showdata, ListView ListView2)
        //{

        //    ChoiceID = ChoiceID - 5;
        //    if (ChoiceID <= 0)
        //    {
        //        ChoiceID = 0;
        //    }
        //    ChoiceList.Clear();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        ChoiceID++;
        //        Navigation Obj = new Navigation();
        //        Obj.Choice = "";
        //        Obj.ID = ChoiceID;
        //        ChoiceList.Add(Obj);
        //    }
        //    ViewState["NDATA"] = ChoiceList;
        //    Navigation(ListView2);
        //}

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
        //public void GetCurrentNavigationLast(int ChoiceID, int Showdata, ListView ListView2)
        //{

        //    ChoiceID = ChoiceID - Showdata;
        //    if (ChoiceID <= 0)
        //    {
        //        ChoiceID = 0;
        //    }
        //    ChoiceList.Clear();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        ChoiceID++;
        //        Navigation Obj = new Navigation();
        //        Obj.Choice = "";
        //        Obj.ID = ChoiceID;
        //        ChoiceList.Add(Obj);
        //    }
        //    ViewState["NDATA"] = ChoiceList;
        //    Navigation(ListView2);
        //}
        #endregion
        public void BindList<T>(ListView Listview1, List<T> List)
        {

            Listview1.DataSource = List;
            Listview1.DataBind();

        }

        protected void btnindex_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/index.aspx");
        }

        public void menubind(int ModuleID)
        {
            int LID = 1;
            //int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            //string userID = ((USER_MST)Session["USER"]).LOGIN_ID;
            int TID = 0;
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            ViewState["MTID"] = TID;
            List<Database.tempUser> List = DB.tempUsers.Where(p => p.MODULE_ID == ModuleID && p.TenentID == TID && p.LocationID == LID).ToList();
            if (List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).Count() > 0)
            {
                //int MID = Convert.ToInt32(Request.QueryString["MID"]);
                //ltsMenu.DataSource = List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == TID).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //ltsMenu.DataBind();

                //int MasterID = Convert.ToInt32(List.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").MASTER_ID);
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
        public string GetLink(int menuID)
        {// <span class="title"><%# Eval("MENU_NAME1")%> </span></a> <ul class="sub-menu" style="display: none;">
            string menustr = "";
            int MTID = Convert.ToInt32(ViewState["MTID"]);
            string OnOff = "";
            var obj = DB.FUNCTION_MST.SingleOrDefault(p => p.MENU_ID == menuID && p.TenentID == MTID);
            if (obj.MENU_LOCATION == "Separator")
            {
                OnOff = obj.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                menustr = "<a href='#'><i class='" + obj.ICONPATH + "'></i><span class='title' >" + obj.MENU_NAME1 + "</span><span class='arrow' style='color: #000;'></span></a>";
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
                            menustr += "<li class='active'><a href='" + item.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item.ICONPATH + "'></i><span class='title' >" + item.MENU_NAME1 + "</span><span class='arrow'></span></a>";
                            menustr += "<ul class='sub-menu' style='display: none;'>";
                            foreach (Database.FUNCTION_MST item1 in itemList1)
                            {
                                OnOff = item1.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                                List<Database.FUNCTION_MST> itemList2 = DB.FUNCTION_MST.Where(p => p.MASTER_ID == item1.MENU_ID && p.TenentID == MTID).OrderBy(a => a.MENU_ORDER).ToList();
                                if (itemList2.Count() > 0)
                                {
                                    menustr += "<li ><a href='" + item1.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item1.ICONPATH + "'></i><span class='title' >" + item1.MENU_NAME1 + "</span><span class='arrow'></span></a>";
                                    menustr += "<ul class='sub-menu' >";
                                    foreach (Database.FUNCTION_MST item2 in itemList2)
                                    {
                                        OnOff = item2.ACTIVE_FLAG == 1 ? "-success'>&nbsp;" : "-danger'>&nbsp;";
                                        menustr += "<li><a href='" + item2.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item2.ICONPATH + "'></i><span class='title' >" + item2.MENU_NAME1 + "</span></a></li>";
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
        public void ExportToExcel<T>(List<T> List, string FileName)
        {
            if (List.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName + ".xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView gvdetails = new GridView();
                gvdetails.ShowHeader = false;
                gvdetails.DataSource = List;
                gvdetails.AllowPaging = false;
                gvdetails.DataBind();
                gvdetails.HeaderRow.Style.Add("font-weight", "bold");
                gvdetails.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
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