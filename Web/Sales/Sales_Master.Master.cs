
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
using System.Transactions;
namespace Web.Sales
{
    public partial class Sales_Master : System.Web.UI.MasterPage
    {
        // Database.CallEntities DB1 = new Database.CallEntities();
        Database.CallEntities DB = new Database.CallEntities();
        List<Navigation> ChoiceList = new List<Navigation>();
        int TID, LID, UID, EMPID = 0;
        //string LangID = "";
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
        public string SessionLoad1(int TID,int LID,int UID,int EMPID,string LangID)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
            string Ref = TID.ToString() + "," + LID.ToString() + "," + UID.ToString() + "," + EMPID.ToString() + "," + LangID;
            return Ref;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                removeHD();
                if (Session["USER"] == null || string.IsNullOrEmpty(Session["USER"].ToString()))//error
                {
                    Response.Redirect("/ACM/SessionTimeOut.aspx");
                }
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
                string userID = ((USER_MST)Session["USER"]).LOGIN_ID;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int userID1 = ((USER_MST)Session["USER"]).USER_ID;
                int LocationID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);

                //lblFirstName.Text = userID.ToString();
                if (Request.QueryString["MID"] != null)
                {

                    string Menuid = Request.QueryString["MID"].ToString();
                    string MenuName = Classes.GlobleClass.EncryptionHelpers.Decrypt(Menuid);
                    string[] MenuidQwe = MenuName.Split('~');
                    int Meni = Convert.ToInt32(MenuidQwe[1]);

                    FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.MENU_ID == Meni);
                    lblpagename.Text = obj.MENU_NAME1;
                    lblpageid.Text = Meni.ToString();



                }
                else
                {
                    int MID = Convert.ToInt32(Request.QueryString["MID"]);
                    //GlobleClass.DeleteTempUser(TID, userID1, LocationID, MID);
                    // GlobleClass.getMenuGloble(TID, userID1, LocationID, MID);

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
                //    List<Database.tempUser> List = DB.tempUser.Where(p => p.TenentID == 1 && p.UserID == 1026 &&p.ROLE_ID !=116 && p.ACTIVEMENU == true).ToList();
                //     int RoleID =Convert .ToInt32 ( ((USER_MST)Session["USER"]).USER_TYPE) ;
                //    if(RoleID==116)
                //    {
                //        List<Database.tempUser> ListSuplier = DB.tempUser.Where(p => p.TenentID == 1 && p.UserID == 1026 && p.ROLE_ID ==116 && p.ACTIVEMENU == true).ToList();
                //        if (ListSuplier.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").Count() > 0)
                //        {
                //            int MID = Convert.ToInt32(Request.QueryString["MID"]);
                //            ltsMenu.DataSource = ListSuplier.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //            ltsMenu.DataBind();

                //            int MasterID = Convert.ToInt32(ListSuplier.First(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator").MASTER_ID);
                //            lstMaster.DataSource = ListSuplier.Where(p => p.MASTER_ID == MasterID && p.MENU_TYPE == "Left Menu" && p.MENU_NAME1 != "Dashboard");//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //            lstMaster.DataBind();
                //        }
                //        else
                //        {
                //            lstMaster.DataSource = ListSuplier.Where(p => p.MENU_TYPE == "Left Menu" ).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //            lstMaster.DataBind();
                //        }
                //    }
                //    else
                //    {
                //        if (List.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").Count() > 0)
                //        {
                //            int MID = Convert.ToInt32(Request.QueryString["MID"]);
                //            ltsMenu.DataSource = List.Where(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator" && p.SMALLTEXT == "main").OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //            ltsMenu.DataBind();

                //            int MasterID = Convert.ToInt32(List.First(p => p.ACTIVEMENU == true && p.ACTIVEMODULE == true && p.ACTIVEROLE == true && p.ACTIVETILLDATE >= DateTime.Now && p.MENU_TYPE == "Separator").MASTER_ID);
                //            lstMaster.DataSource = List.Where(p => p.MASTER_ID == MasterID && p.MENU_TYPE == "Left Menu" && p.MENU_NAME1 != "Dashboard");//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //            lstMaster.DataBind();
                //        }
                //        else
                //        {
                //            lstMaster.DataSource = List.Where(p => p.MENU_TYPE == "Left Menu" && p.MENU_NAME1 != "Dashboard").OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //            lstMaster.DataBind();
                //        }
                //    }
                //    lstisGloble.DataSource = List.Where(p => p.AMIGLOBALE == 1);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //    lstisGloble.DataBind();
                menubind(10);
                
            }

        }

        public void removeHD()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                List<Database.ICTR_HD> LISTHDRemove = DB.ICTR_HD.Where(p => p.TenentID == TID  && p.TransDocNo == null && p.transid == 4101 && p.transsubid == 410103).ToList();

                foreach (Database.ICTR_HD ITEMHD in LISTHDRemove)
                {
                    Database.ICTR_HD_TEMP objICTR_HD = new ICTR_HD_TEMP();

                    objICTR_HD.InvoiceNO = ITEMHD.InvoiceNO;
                    objICTR_HD.TenentID = ITEMHD.TenentID;
                    objICTR_HD.MYTRANSID = ITEMHD.MYTRANSID;
                    objICTR_HD.ToTenantID = ITEMHD.ToTenantID;
                    objICTR_HD.locationID = ITEMHD.locationID;
                    objICTR_HD.MainTranType = ITEMHD.MainTranType;
                    objICTR_HD.TransType = ITEMHD.TransType;
                    objICTR_HD.transid = ITEMHD.transid;
                    objICTR_HD.transsubid = ITEMHD.transsubid;
                    objICTR_HD.TranType = ITEMHD.TranType;
                    objICTR_HD.COMPID = ITEMHD.COMPID;
                    objICTR_HD.MYSYSNAME = ITEMHD.MYSYSNAME;
                    objICTR_HD.CUSTVENDID = ITEMHD.CUSTVENDID;
                    objICTR_HD.LF = ITEMHD.LF;
                    objICTR_HD.PERIOD_CODE = ITEMHD.PERIOD_CODE;
                    objICTR_HD.ACTIVITYCODE = ITEMHD.ACTIVITYCODE;
                    objICTR_HD.MYDOCNO = ITEMHD.MYDOCNO;
                    objICTR_HD.USERBATCHNO = ITEMHD.USERBATCHNO;
                    objICTR_HD.TOTAMT = ITEMHD.TOTAMT;
                    objICTR_HD.TOTQTY = ITEMHD.TOTQTY;
                    objICTR_HD.AmtPaid = ITEMHD.AmtPaid;
                    objICTR_HD.PROJECTNO = ITEMHD.PROJECTNO;
                    objICTR_HD.CounterID = ITEMHD.CounterID;
                    objICTR_HD.PrintedCounterInvoiceNo = ITEMHD.PrintedCounterInvoiceNo;
                    objICTR_HD.JOID = ITEMHD.JOID;
                    objICTR_HD.TRANSDATE = ITEMHD.TRANSDATE;
                    objICTR_HD.REFERENCE = ITEMHD.REFERENCE;
                    objICTR_HD.NOTES = ITEMHD.NOTES;
                    objICTR_HD.GLPOST = ITEMHD.GLPOST;
                    objICTR_HD.GLPOST1 = ITEMHD.GLPOST1;
                    objICTR_HD.GLPOSTREF = ITEMHD.GLPOSTREF;
                    objICTR_HD.GLPOSTREF1 = ITEMHD.GLPOSTREF1;
                    objICTR_HD.ICPOST = ITEMHD.ICPOST;
                    objICTR_HD.ICPOSTREF = ITEMHD.ICPOSTREF;
                    objICTR_HD.USERID = ITEMHD.USERID;
                    objICTR_HD.ACTIVE = ITEMHD.ACTIVE;
                    objICTR_HD.COMPANYID = ITEMHD.COMPANYID;
                    objICTR_HD.ENTRYDATE = ITEMHD.ENTRYDATE;
                    objICTR_HD.ENTRYTIME = ITEMHD.ENTRYTIME;
                    objICTR_HD.UPDTTIME = ITEMHD.UPDTTIME;
                    objICTR_HD.Discount = ITEMHD.Discount;
                    objICTR_HD.Status = ITEMHD.Status;
                    objICTR_HD.Terms = ITEMHD.Terms;
                    objICTR_HD.ExtraField1 = ITEMHD.ExtraField1;
                    objICTR_HD.ExtraField2 = ITEMHD.ExtraField2;
                    objICTR_HD.ExtraSwitch1 = ITEMHD.ExtraSwitch1;
                    objICTR_HD.RefTransID = ITEMHD.RefTransID;
                    objICTR_HD.TransDocNo = ITEMHD.TransDocNo;

                    DB.ICTR_HD_TEMP.AddObject(objICTR_HD);
                    DB.SaveChanges();

                    List<Database.ICTRPayTerms_HD> ListPAYHD = DB.ICTRPayTerms_HD.Where(p => p.TenentID == ITEMHD.TenentID && p.MyTransID == ITEMHD.MYTRANSID).ToList();

                    foreach (Database.ICTRPayTerms_HD ITEMPAY in ListPAYHD)
                    {

                        Database.ICTRPayTerms_HD_TEMP objICTRPayTerms_HD = new ICTRPayTerms_HD_TEMP();

                        objICTRPayTerms_HD.TenentID = ITEMPAY.TenentID;
                        objICTRPayTerms_HD.LocationID = ITEMPAY.LocationID;
                        objICTRPayTerms_HD.MyTransID = ITEMPAY.MyTransID;
                        objICTRPayTerms_HD.PaymentTermsId = ITEMPAY.PaymentTermsId;
                        objICTRPayTerms_HD.CashBankChequeID = ITEMPAY.CashBankChequeID;
                        objICTRPayTerms_HD.CounterID = ITEMPAY.CounterID;
                        objICTRPayTerms_HD.TransDate = ITEMPAY.TransDate;
                        objICTRPayTerms_HD.CheckOutDate = ITEMPAY.CheckOutDate;// come from Cash Delivery Popup
                        objICTRPayTerms_HD.AccountantID = ITEMPAY.AccountantID;// come from Cash Delivery Popup
                        objICTRPayTerms_HD.AccountID = ITEMPAY.AccountID;
                        objICTRPayTerms_HD.CRUP_ID = ITEMPAY.CRUP_ID;
                        objICTRPayTerms_HD.Notes = ITEMPAY.Notes;
                        objICTRPayTerms_HD.Amount = ITEMPAY.Amount;
                        objICTRPayTerms_HD.ReferenceNo = ITEMPAY.ReferenceNo;
                        objICTRPayTerms_HD.ApprovalID = ITEMPAY.ApprovalID;
                        objICTRPayTerms_HD.ChequeVerified = ITEMPAY.ChequeVerified;
                        objICTRPayTerms_HD.CashVerified = ITEMPAY.CashVerified;
                        objICTRPayTerms_HD.ATMVerified = ITEMPAY.ATMVerified;
                        objICTRPayTerms_HD.VoucharVerified = ITEMPAY.VoucharVerified;
                        objICTRPayTerms_HD.ChequeVerifiedDate = ITEMPAY.ChequeVerifiedDate;
                        objICTRPayTerms_HD.CashVerifiedDate = ITEMPAY.CashVerifiedDate;
                        objICTRPayTerms_HD.ATMVerifiedDate = ITEMPAY.ATMVerifiedDate;
                        objICTRPayTerms_HD.VoucharVerifiedDate = ITEMPAY.VoucharVerifiedDate;
                        objICTRPayTerms_HD.JVRefNo = ITEMPAY.JVRefNo;

                        DB.ICTRPayTerms_HD_TEMP.AddObject(objICTRPayTerms_HD);
                        DB.SaveChanges();


                    }

                    List<Database.ICTR_HD> LISTHDRemove1 = DB.ICTR_HD.Where(p => p.TenentID == TID  && p.TransDocNo == null && p.transid == 4101 && p.transsubid == 410103).ToList();

                    foreach (Database.ICTR_HD ITEMHD1 in LISTHDRemove1)
                    {
                        Database.ICTR_HD remove = DB.ICTR_HD.Single(p => p.TenentID == ITEMHD1.TenentID && p.MYTRANSID == ITEMHD1.MYTRANSID);
                        DB.ICTR_HD.DeleteObject(remove);
                        DB.SaveChanges();


                        List<Database.ICTRPayTerms_HD> ListPAYHD1 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == ITEMHD1.TenentID && p.MyTransID == ITEMHD1.MYTRANSID).ToList();

                        foreach (Database.ICTRPayTerms_HD ITEMPAY1 in ListPAYHD1)
                        {
                            Database.ICTRPayTerms_HD remove1 = DB.ICTRPayTerms_HD.Single(p => p.TenentID == ITEMPAY1.TenentID && p.MyTransID == ITEMPAY1.MyTransID);

                            DB.ICTRPayTerms_HD.DeleteObject(remove1);

                            DB.SaveChanges();
                        }

                        List<Database.TempListICTR_DT> removedt = DB.TempListICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ITEMHD1.MYTRANSID).ToList();

                        foreach (Database.TempListICTR_DT removeitem in removedt)
                        {
                            Database.TempListICTR_DT removedt1 = DB.TempListICTR_DT.Single(p => p.TenentID == removeitem.TenentID && p.MYTRANSID == removeitem.MYTRANSID && p.MYID == removeitem.MYID);

                            DB.TempListICTR_DT.DeleteObject(removedt1);

                            DB.SaveChanges();
                        }

                        List<Database.ICTR_DT> removedto = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ITEMHD1.MYTRANSID).ToList();

                        foreach (Database.ICTR_DT removeitem in removedto)
                        {
                            Database.ICTR_DT removedt1 = DB.ICTR_DT.Single(p => p.TenentID == removeitem.TenentID && p.MYTRANSID == removeitem.MYTRANSID && p.MYID == removeitem.MYID);

                            DB.ICTR_DT.DeleteObject(removedt1);

                            DB.SaveChanges();
                        }

                        List<Database.ICIT_BR_TMP> removedtmp = DB.ICIT_BR_TMP.Where(p => p.TenentID == TID && p.MYTRANSID == ITEMHD1.MYTRANSID && p.MySysName=="SAL" && p.LocationID==LID).ToList();

                        foreach (Database.ICIT_BR_TMP removeitemtmp in removedtmp)
                        {
                            Database.ICIT_BR_TMP removetmp = DB.ICIT_BR_TMP.Single(p => p.TenentID == removeitemtmp.TenentID && p.MYTRANSID == removeitemtmp.MYTRANSID && p.MySysName == "SAL" && p.LocationID == LID && p.Serial_Number == removeitemtmp.Serial_Number && p.UOM==removeitemtmp.UOM && p.period_code==removeitemtmp.period_code && p.MySysName==removeitemtmp.MySysName && p.SIZECODE==removeitemtmp.SIZECODE && p.COLORID==removeitemtmp.COLORID && p.BatchNo == removeitemtmp.BatchNo && p.Bin_ID ==removeitemtmp.Bin_ID);

                            DB.ICIT_BR_TMP.DeleteObject(removetmp);

                            DB.SaveChanges();
                        }
                    }

                }
                scope.Complete();
            }
        }
        public void bindModule()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            //lstmodule.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == 0 && p.TenentID == TID);
            //lstmodule.DataBind();

        }
        protected void lstmodule_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblHide = (Label)e.Item.FindControl("lblHide");
                int MID = Convert.ToInt32(lblHide.Text);
                ListView ltsProduct = (ListView)e.Item.FindControl("lstsubmodule");
                ltsProduct.DataSource = DB.MODULE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.Parent_Module_id == MID && p.TenentID == TID);
                ltsProduct.DataBind();
                //menubind(MID);
            }

        }
        public string getbarend(int BID)
        {
            return DB.REFTABLEs.Single(p => p.REFID == BID).REFNAME1;
        }

        public string ProductName(int PID)
        {
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID).ProdName1;
        }
        public string CATNAME(int CID)
        {
            return DB.CAT_MST.Single(p => p.CATID == CID).CAT_NAME1;
        }
        //public string GetLink(int menuID)
        //{// <span class="title"><%# Eval("MENU_NAME1")%> </span></a> <ul class="sub-menu" style="display: none;">
        //    string menustr = "";
        //    var obj = DB.tempUser.SingleOrDefault(p => p.MENUID == menuID);
        //    if (obj.MENU_TYPE == "Separator")
        //    {
        //        menustr = "<a href='#'><i class='" + obj.ICONPATH + "'></i><span class='title' style='font-size: 14px; font-weight: 600;'>" + obj.MENU_NAME1 + "</span><span class='arrow' style='color: #000;'></span></a>";


        //        menustr += " <ul class='sub-menu' style='display: none;'>";


        //        List<Database.tempUser> itemList = DB.tempUser.Where(p => p.MASTER_ID == menuID && p.ACTIVEMENU == true).OrderBy(a => a.MENU_ORDER).ToList();
        //        if (itemList.Count() > 0)
        //        {


        //            foreach (Database.tempUser item in itemList)
        //            {
        //                List<Database.tempUser> itemList1 = DB.tempUser.Where(p => p.MASTER_ID == item.MENUID && p.ACTIVEMENU == true).OrderBy(a => a.MENU_ORDER).ToList();
        //                if (itemList1.Count() > 0)
        //                {
        //                    menustr += "<li class='active'><a href='" + item.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item.ICONPATH + "'></i><span class='title' style='font-size: 14px; font-weight: 600;'>" + item.MENU_NAME1 + "</span><span class='arrow'></span></a>";
        //                    menustr += "<ul class='sub-menu' style='display: none;'>";
        //                    foreach (Database.tempUser item1 in itemList1)
        //                    {
        //                        List<Database.tempUser> itemList2 = DB.tempUser.Where(p => p.MASTER_ID == item1.MENUID && p.ACTIVEMENU == true).OrderBy(a => a.MENU_ORDER).ToList();
        //                        if (itemList2.Count() > 0)
        //                        {
        //                            menustr += "<li ><a href='" + item1.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item1.ICONPATH + "'></i><span class='title' style='font-size: 14px; font-weight: 600;'>" + item1.MENU_NAME1 + "</span><span class='arrow'></span></a>";
        //                            menustr += "<ul class='sub-menu' >";
        //                            foreach (Database.tempUser item2 in itemList2)
        //                            {
        //                                menustr += "<li><a href='" + item2.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item2.ICONPATH + "'></i><span class='title' style='font-size: 14px; font-weight: 600;'>" + item2.MENU_NAME1 + "</span></a></li>";
        //                            }
        //                            menustr += Displaysubmenu1(menuID);
        //                            menustr += "</li>";
        //                        }
        //                        else
        //                        {
        //                            menustr += "<li ><a href='" + item1.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item1.ICONPATH + "'></i><span class='title' style='font-size: 14px; font-weight: 600;'>" + item1.MENU_NAME1 + "</span></a></li>";
        //                        }
        //                    }
        //                    menustr += Displaysubmenu1(menuID);
        //                    menustr += "</li>";
        //                }
        //                else
        //                {
        //                    menustr += "<li class='active'><a href='" + item.URLREWRITE + "?MID=" + item.MASTER_ID + "'><i class='" + item.ICONPATH + "'></i><span class='title' style='font-size: 14px; font-weight: 600;'>" + item.MENU_NAME1 + "</span></a></li>";
        //                }
        //            }

        //        }


        //    }
        //    else
        //    {
        //        menustr = "<a href='" + obj.URLREWRITE + "'><i class='icon-home'></i><span class='title'>" + obj.MENU_NAME1 + "</span></a>";
        //    }
        //    return menustr;
        //}
        public string Displaysubmenu1(int menuID)
        {
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
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblMenuHide = (Label)e.Item.FindControl("lblMenuHide");
                int MID = Convert.ToInt32(lblMenuHide.Text);

                if (flag == false)
                    flag = true;
                else
                {
                    ListView ltsProduct = (ListView)e.Item.FindControl("lstsubMenu");
                    ltsProduct.DataSource = DB.tempUsers.Where(p => p.ACTIVEMENU == true && p.TenentID == 360 && p.MASTER_ID == MID);
                    ltsProduct.DataBind();
                }

            }

        }


        public void BindMeniList()
        {
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




        public string getOwnPage()
        {
            string PageID = "0";
            if (Request.Url.AbsolutePath.Contains("Invoice.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Invoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Quotation.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Invoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("Sales_Return.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Invoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("PaymentVouchar.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Invoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("invoicepos.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Invoice.aspx").TableName.ToString();
            }
            else if (Request.Url.AbsolutePath.Contains("invoiceposforStaff.aspx"))
            {
                PageID = DB.TBLLabelMSTs.Single(p => p.Active == true && p.PageName == "Invoice.aspx").TableName.ToString();
            }
            return PageID;
        }
        public List<Database.TBLLabelDTL> Bindxml(string pagename)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("//Sales//xml//" + pagename + ".xml"));
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
            Response.Redirect("/ECOMM/AdminIndex.aspx");
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
                //ltsMenu.DataSource = List.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.TenentID == MTID).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //ltsMenu.DataBind();

                int MasterID = Convert.ToInt32(List.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").MASTER_ID);
                //lstMaster.DataSource = List.Where(p => p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu" && p.MENU_NAME1 != "Dashboard" && p.TenentID == MTID);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //lstMaster.DataBind();
            }
            else
            {
                //lstMaster.DataSource = List.Where(p => p.MENU_LOCATION == "Left Menu" && p.MENU_NAME1 != "Dashboard" && p.TenentID == MTID && p.MODULE_ID == ModuleID).OrderBy(a => a.MENU_ORDER);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
                //lstMaster.DataBind();
            }
            //lstisGloble.DataSource = List.Where(p => p.AMIGLOBALE == 1 && p.TenentID == MTID && p.MODULE_ID == ModuleID);//Classes.Globle.EncryptionHelpers.getMenuGloble(TID, userID);
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

        public void BindProdu(int PID, DropDownList ddlUOM, TextBox txtDescription, TextBox txtserchProduct)
        {
            TBLPRODUCT objEco_TBLPRODUCT = DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID);
            string DIScrip = objEco_TBLPRODUCT.DescToprint;
            int MOU = Convert.ToInt32(objEco_TBLPRODUCT.UOM);
            ddlUOM.Enabled = false;
            txtserchProduct.Text = objEco_TBLPRODUCT.BarCode;
            txtDescription.Text = DIScrip.ToString();
            ddlUOM.SelectedValue = MOU.ToString();
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
    //[Serializable]
    //public class Navigation
    //{
    //    public string Choice { get; set; }
    //    public int ID { get; set; }

    //}
    #endregion
}
