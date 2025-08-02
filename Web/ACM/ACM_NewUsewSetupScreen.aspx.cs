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
using Database;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.IO;
using System.Data.OleDb;
using Classes;

namespace Web.ACM
{
    public partial class ACM_NewUsewSetupScreen : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string iddddd = ((USER_MST)Session["USER"]).TenentID.ToString();
            if (!IsPostBack)
            {
                FillContractorID();
                MycomapnysetupEditMode();
                userBind();
                commonbind();
            }
        }
        public void FillContractorID()
        {
            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
            Classes.EcommAdminClass.getdropdown(drpcompanttype, TID, "COMP", "COMPTYPE", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpcuntry, TID, "", "", "", "tblCOUNTRY");

        }
        public void bindSates(string CID)
        {
            Classes.EcommAdminClass.getdropdown(drpstate, TID, CID, "", "", "tblStates");
        }
        public void MycomapnysetupEditMode()
        {
            string UID = ((USER_MST)Session["USER"]).USER_ID.ToString();
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID == UID).Count() > 0)
            {
                Database.MYCOMPANYSETUP objMYCOMPANYSETUP = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID && p.USERID == UID);
                int compID = Convert.ToInt32(objMYCOMPANYSETUP.COMPANYID);
                ViewState["COMPID"] = compID;
                txtAssignTenant.Text = objMYCOMPANYSETUP.TenentID.ToString();
                drplocation.SelectedValue = objMYCOMPANYSETUP.PHYSICALLOCID;
                txtcompni1.Text = objMYCOMPANYSETUP.COMPNAME1;
                txtcompni2.Text = objMYCOMPANYSETUP.COMPNAME2;
                txtcompni3.Text = objMYCOMPANYSETUP.COMPNAME3;
                string COID = objMYCOMPANYSETUP.COUNTRYID.ToString();
                drpcuntry.SelectedValue = COID;//ObjMYCOMP.COUNTRYID.ToString();
                txtAddre1.Text = objMYCOMPANYSETUP.ADDR1;
                txtaddre2.Text = objMYCOMPANYSETUP.ADDR2;
                txtcity.Text = objMYCOMPANYSETUP.CITY;
                bindSates(COID);
                drpstate.SelectedValue = objMYCOMPANYSETUP.STATE;
                txtpostcod.Text = objMYCOMPANYSETUP.POSTALCODE;
                txtzipcod.Text = objMYCOMPANYSETUP.ZIPCODE;
                txtphon.Text = objMYCOMPANYSETUP.PHONE;
                txtfax.Text = objMYCOMPANYSETUP.FAX;
                txtarabic.Text = objMYCOMPANYSETUP.ARABIC;
                txtdcurreny.Text = objMYCOMPANYSETUP.DECIMALCURRENCY.ToString();
                txtreportDf.Text = objMYCOMPANYSETUP.REPORTDEFAULT;
                txtreportDire.Text = objMYCOMPANYSETUP.REPORTDIRECTORY;
                txtdatadirec.Text = objMYCOMPANYSETUP.DATADIRECTORY;
                txtbackdirect.Text = objMYCOMPANYSETUP.BACKUPDIRECTORY;
                txtexecutdirecto.Text = objMYCOMPANYSETUP.EXECUTABLEDIRECTORY;
                txtinvdatabsn.Text = objMYCOMPANYSETUP.INVDATABASENAME;
                txtactdata.Text = objMYCOMPANYSETUP.ACTDATABASENAME;
                drpcompanttype.SelectedValue = objMYCOMPANYSETUP.Companytype.ToString();
                if (objMYCOMPANYSETUP.LogotoDisplay != null)
                    Avatar.ImageUrl = "../assets/" + objMYCOMPANYSETUP.LogotoDisplay;
                else
                    Avatar.ImageUrl = "~/Gallery/defolt.png";
            }
        }
        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            string UID = ((USER_MST)Session["USER"]).USER_ID.ToString();
            Database.MYCOMPANYSETUP objMYCOMPANYSETUP = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID && p.USERID == UID);

            objMYCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
            objMYCOMPANYSETUP.COMPNAME1 = txtcompni1.Text;
            objMYCOMPANYSETUP.COMPNAME1 = txtcompni2.Text;
            objMYCOMPANYSETUP.COMPNAME1 = txtcompni3.Text;
            objMYCOMPANYSETUP.COUNTRYID = Convert.ToInt32(drpcuntry.SelectedValue);
            objMYCOMPANYSETUP.ADDR1 = txtAddre1.Text;
            objMYCOMPANYSETUP.ADDR2 = txtaddre2.Text;
            objMYCOMPANYSETUP.CITY = txtcity.Text;
            objMYCOMPANYSETUP.STATE = drpstate.SelectedValue; //txtstate.Text;
            objMYCOMPANYSETUP.POSTALCODE = txtpostcod.Text;
            objMYCOMPANYSETUP.ZIPCODE = txtzipcod.Text;
            objMYCOMPANYSETUP.PHONE = txtphon.Text;
            objMYCOMPANYSETUP.FAX = txtfax.Text;
            objMYCOMPANYSETUP.ARABIC = txtarabic.Text;
            objMYCOMPANYSETUP.DECIMALCURRENCY = Convert.ToDecimal(txtdcurreny.Text);
            objMYCOMPANYSETUP.REPORTDEFAULT = txtreportDf.Text;
            objMYCOMPANYSETUP.REPORTDIRECTORY = txtreportDire.Text;
            objMYCOMPANYSETUP.DATADIRECTORY = txtdatadirec.Text;
            objMYCOMPANYSETUP.BACKUPDIRECTORY = txtbackdirect.Text;
            objMYCOMPANYSETUP.EXECUTABLEDIRECTORY = txtexecutdirecto.Text;
            objMYCOMPANYSETUP.INVDATABASENAME = txtinvdatabsn.Text;
            objMYCOMPANYSETUP.ACTDATABASENAME = txtactdata.Text;
            objMYCOMPANYSETUP.UPDTTIME = DateTime.Now;
            objMYCOMPANYSETUP.Companytype = Convert.ToInt32(drpcompanttype.SelectedValue);
            if (avatarUploadd.HasFile)
            {
                string path = avatarUploadd.FileName;
                avatarUploadd.SaveAs(Server.MapPath("../assets/" + path));
                objMYCOMPANYSETUP.LogotoDisplay = path;
            }
            DB.SaveChanges();


        }
        public void userBind()
        {
            int compid = Convert.ToInt32(ViewState["COMPID"]);
            List<Database.USER_MST> userList = DB.USER_MST.Where(p => p.TenentID == TID && p.CompId == compid).ToList();

            drprole.DataSource = DB.ROLE_MST.Where(p => p.TenentID == TID);
            drprole.DataTextField = "ROLE_NAME";
            drprole.DataValueField = "ROLE_ID";
            drprole.DataBind();
            drprole.Items.Insert(0, new ListItem("--select Role--", "0"));

            drpdeletemodule.Items.Insert(0, new ListItem("--select Module--", "0"));
            drpaddmodule.Items.Insert(0, new ListItem("--select Module--", "0"));
            drpdeleteModuleR.Items.Insert(0, new ListItem("--select Module--", "0"));
            drpmodule.Items.Insert(0, new ListItem("--select Module--", "0"));

            //Listuser.DataSource = userList;
            //Listuser.DataBind();

            //Drpuserall.DataSource = userList;
            //Drpuserall.DataTextField = "LOGIN_ID";
            //Drpuserall.DataValueField = "USER_ID";
            //Drpuserall.DataBind();
            //Drpuserall.Items.Insert(0, new ListItem("--select User--", "0"));


            List<Database.USER_ROLE> ROLElist = DB.USER_ROLE.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y").GroupBy(p => p.ROLE_ID).Select(p => p.FirstOrDefault()).ToList();
            //roleList.DataSource = ROLElist.OrderBy(p => p.ROLE_ID);
            //roleList.DataBind();


            List<Database.PRIVILEGE_MST> PRIVILEGEList = new List<Database.PRIVILEGE_MST>();
            List<Database.MODULE_MAP> ListModule = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y").GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.MODULE_MAP item in ListModule)
            {
                if (DB.PRIVILEGE_MST.Where(p => p.PRIVILEGE_ID == item.PRIVILEGE_ID).Count() > 0)
                {
                    Database.PRIVILEGE_MST obj = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == item.PRIVILEGE_ID);
                    PRIVILEGEList.Add(obj);
                }

            }

            var Privilagedata = from item in PRIVILEGEList
                                select new
                                {
                                    PRIVILEGE_NAME = item.PRIVILEGE_NAME.Replace("Privilage", ""),
                                    PRIVILEGE_ID = item.PRIVILEGE_ID
                                };

            //drpUsermodule.DataSource = Privilagedata.OrderBy(a => a.PRIVILEGE_NAME);//DB.PRIVILEGE_MST.Where(p => p.TenentID == TID).OrderBy(a => a.PRIVILEGE_NAME);
            //drpUsermodule.DataTextField = "PRIVILEGE_NAME";
            //drpUsermodule.DataValueField = "PRIVILEGE_ID";
            //drpUsermodule.DataBind();
            //drpUsermodule.Items.Insert(0, new ListItem("--select Module--", "0"));

            //DrpRoleModule.DataSource = Privilagedata.OrderBy(a => a.PRIVILEGE_NAME);
            //DrpRoleModule.DataTextField = "PRIVILEGE_NAME";
            //DrpRoleModule.DataValueField = "PRIVILEGE_ID";
            //DrpRoleModule.DataBind();
            //DrpRoleModule.Items.Insert(0, new ListItem("--select Module--", "0"));

            //DrpModuleAll.DataSource = Privilagedata.OrderBy(a => a.PRIVILEGE_NAME);
            //DrpModuleAll.DataTextField = "PRIVILEGE_NAME";
            //DrpModuleAll.DataValueField = "PRIVILEGE_ID";
            //DrpModuleAll.DataBind();
            //DrpModuleAll.Items.Insert(0, new ListItem("--select Module--", "0"));

            //List<Database.ROLE_MST> rollist = new List<Database.ROLE_MST>();
            //foreach (Database.USER_ROLE itemss in ROLElist)
            //{
            //    Database.ROLE_MST obj = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_ID == itemss.ROLE_ID);
            //    rollist.Add(obj);
            //}
            //drproleMST.DataSource = rollist.OrderBy(p => p.ROLE_ID); ;
            //drproleMST.DataTextField = "ROLE_NAME";
            //drproleMST.DataValueField = "ROLE_ID";
            //drproleMST.DataBind();
            //drproleMST.Items.Insert(0, new ListItem("--select Role--", "0"));

            //drpadduser.DataSource = userList;
            //drpadduser.DataTextField = "LOGIN_ID";
            //drpadduser.DataValueField = "USER_ID";
            //drpadduser.DataBind();
            //drpadduser.Items.Insert(0, new ListItem("--select User--", "0"));

            //CHKModuleAdd.DataSource = Privilagedata.OrderBy(a => a.PRIVILEGE_NAME);
            //CHKModuleAdd.DataTextField = "PRIVILEGE_NAME";
            //CHKModuleAdd.DataValueField = "PRIVILEGE_ID";
            //CHKModuleAdd.DataBind();

            List<Database.MODULE_MST> modul = new List<Database.MODULE_MST>();
            List<Database.MODULE_MAP> ListModuleR = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y").GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.MODULE_MAP item in ListModuleR)
            {
                if (DB.MODULE_MST.Where(p => p.Module_Id == item.MODULE_ID).Count() > 0)
                {
                    Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == item.MODULE_ID);
                    modul.Add(obj);
                }

            }
            Checkmodule.DataSource = modul;
            Checkmodule.DataTextField = "Module_Name";
            Checkmodule.DataValueField = "Module_Id";
            Checkmodule.DataBind();


            RoleSetpanel("block", "collapse");
            UserRolepanel("none", "expand");
        }

        public void commonbind()
        {
            int compid = Convert.ToInt32(ViewState["COMPID"]);
            List<Database.USER_MST> userList = DB.USER_MST.Where(p => p.TenentID == TID && p.CompId == compid).ToList();

            drpuserr.DataSource = userList;
            drpuserr.DataTextField = "LOGIN_ID";
            drpuserr.DataValueField = "USER_ID";
            drpuserr.DataBind();
            drpuserr.Items.Insert(0, new ListItem("--select User--", "0"));

            drprole1.DataSource = DB.ROLE_MST.Where(p => p.TenentID == TID);
            drprole1.DataTextField = "ROLE_NAME";
            drprole1.DataValueField = "ROLE_ID";
            drprole1.DataBind();
            drprole1.Items.Insert(0, new ListItem("--select Role--", "0"));


            //For User(tab2)
            drppuseract.DataSource = userList;
            drppuseract.DataTextField = "LOGIN_ID";
            drppuseract.DataValueField = "USER_ID";
            drppuseract.DataBind();
            drppuseract.Items.Insert(0, new ListItem("--select User--", "0"));
            //for module(tab4)
            drpuserMod.DataSource = userList;
            drpuserMod.DataTextField = "LOGIN_ID";
            drpuserMod.DataValueField = "USER_ID";
            drpuserMod.DataBind();
            drpuserMod.Items.Insert(0, new ListItem("--select User--", "0"));
        }

        public string ROLE(int ID)
        {
            if (DB.ROLE_MST.Where(p => p.TenentID == TID && p.ROLE_ID == ID).Count() > 0)
            {
                string RoleNAME = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_ID == ID).ROLE_NAME;
                return RoleNAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string Module(int ID)
        {
            if (DB.MODULE_MST.Where(p => p.Module_Id == ID).Count() > 0)
            {
                string MODULENAME = DB.MODULE_MST.Single(p => p.Module_Id == ID).Module_Name;
                return MODULENAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetMenuname(int MenuID)
        {
            if (DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuID).Count() > 0)
                return DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuID).First().MENU_NAME1;
            else
                return "Not Found";
        }
        public void bindsys(int PrivilageFor, ListView CommonListview, int Mod, int UType)
        {
            List<Database.PRIVILAGE_MENUDemon> FinalList = new List<PRIVILAGE_MENUDemon>();
            FinalList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == UType && p.PRIVILEGE_ID == Mod && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == PrivilageFor).ToList();
            if (FinalList.Count() > 0)
            {
                if (FinalList.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").Count() > 0)
                {
                    CommonListview.DataSource = FinalList.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
                    CommonListview.DataBind();
                    int MasterID = Convert.ToInt32(FinalList.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").MASTER_ID);
                }
            }
            else
            {
                CommonListview.DataSource = null;
                CommonListview.DataBind();
            }
        }
        //protected void Drpuserall_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int privilageM = Convert.ToInt32(drpUsermodule.SelectedValue);
        //    int userM = Convert.ToInt32(Drpuserall.SelectedValue);
        //    if (userM != 0)
        //    {
        //        bindsys(3, userListSeparate, privilageM, userM);

        //        List<Database.PRIVILAGE_MENUDemon> PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == userM && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.Action == "Y").ToList();
        //        List<Database.PRIVILAGE_MENUDemon> userwiseModule = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILEGE_MENU_ID == userM && p.PRIVILEGE_ID == privilageM && p.ActiveModule == "Y").ToList();
        //        if (PRIList.Count() > 0)
        //        {
        //            CHKUserActive.Checked = true;
        //        }
        //        if (userwiseModule.Count() > 0)
        //        {
        //            cheusewiseModule.Checked = true;
        //        }
        //        btnSaveuser.Visible = true;
        //    }
        //    else
        //    {
        //        btnSaveuser.Visible = false;
        //    }
        //}

        //protected void userListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    int privilageM = Convert.ToInt32(drpUsermodule.SelectedValue);
        //    int userM = Convert.ToInt32(Drpuserall.SelectedValue);
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lbluserSeparateMENU_ID");
        //        int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
        //        ListView userListLeft = (ListView)e.Item.FindControl("userListLeft");
        //        //bindSubsys(3, userListLeft, MasterID);

        //        List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == userM && p.PRIVILEGE_ID == privilageM && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
        //        userListLeft.DataSource = List;
        //        userListLeft.DataBind();

        //    }
        //}

        //protected void drproleMST_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int privilageM = Convert.ToInt32(DrpRoleModule.SelectedValue);
        //    int RoleM = Convert.ToInt32(drproleMST.SelectedValue);
        //    if (RoleM != 0)
        //    {
        //        bindsys(1, RoleListSeparate, privilageM, RoleM);

        //        List<Database.PRIVILAGE_MENUDemon> PRIList = new List<Database.PRIVILAGE_MENUDemon>();
        //        PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == RoleM && p.PRIVILAGEFOR == privilageM && p.TenentID == TID && p.Action == "Y").ToList();
        //        if (PRIList.Count() > 0)
        //        {
        //            CHKRoleActive.Checked = true;
        //        }
        //        btnSaveRole.Visible = true;
        //    }
        //    else
        //    {
        //        btnSaveRole.Visible = false;
        //    }
        //}

        //protected void RoleListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    int privilageM = Convert.ToInt32(DrpRoleModule.SelectedValue);
        //    int RoleM = Convert.ToInt32(drproleMST.SelectedValue);
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lblroleSeparateMENU_ID");
        //        int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
        //        ListView RoleListLeft = (ListView)e.Item.FindControl("RoleListLeft");
        //        //bindSubsys(3, userListLeft, MasterID);

        //        List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == RoleM && p.PRIVILEGE_ID == privilageM && p.PRIVILAGEFOR == 1 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
        //        RoleListLeft.DataSource = List;
        //        RoleListLeft.DataBind();

        //    }
        //}

        //protected void DrpModuleAll_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int privilageM = Convert.ToInt32(DrpModuleAll.SelectedValue);
        //    lblforSystem1.Text = "Module Wise Menu " + privilageM;
        //    if (privilageM != 0)
        //    {
        //        int ModuleM = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == privilageM).MODULE_ID;
        //        bindsys(2, sysListSeparate, privilageM, ModuleM);
        //        List<Database.PRIVILAGE_MENUDemon> PRIList = new List<Database.PRIVILAGE_MENUDemon>();

        //        PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == ModuleM && p.PRIVILEGE_ID == privilageM && p.PRIVILAGEFOR == 2 && p.TenentID == TID && p.ActiveModule == "Y").ToList();
        //        if (PRIList.Count() > 0)
        //        {
        //            CHKModule.Checked = true;
        //        }
        //        btnSavesys.Visible = true;
        //    }
        //    else
        //    {
        //        btnSavesys.Visible = false;
        //    }
        //}



        //protected void btnSavesys_Click(object sender, EventArgs e)
        //{
        //    updatePM(sysListSeparate, 2);
        //}
        //protected void btnSaveRole_Click(object sender, EventArgs e)
        //{
        //    updatePM(RoleListSeparate, 1);
        //}
        //protected void btnSaveuser_Click(object sender, EventArgs e)
        //{
        //    updatePM(userListSeparate, 3);
        //}
        public void updatePM(ListView mainList, int PrivilageFor)
        {

            int PRIVILEGE_ID = 0;
            int PRIVILEGE_MENUID = 0;
            if (PrivilageFor == 1)
            {
                //PRIVILEGE_ID = Convert.ToInt32(DrpRoleModule.SelectedValue);
                //PRIVILEGE_MENUID = Convert.ToInt32(drproleMST.SelectedValue);
            }
            if (PrivilageFor == 2)
            {
                //PRIVILEGE_ID = Convert.ToInt32(DrpModuleAll.SelectedValue);
                PRIVILEGE_MENUID = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == PRIVILEGE_ID).MODULE_ID; ;
            }
            if (PrivilageFor == 3)
            {
                //PRIVILEGE_ID = Convert.ToInt32(drpUsermodule.SelectedValue);
                //PRIVILEGE_MENUID = Convert.ToInt32(Drpuserall.SelectedValue);
            }
            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
            for (int i = 0; i < mainList.Items.Count; i++)
            {
                ListView subList = new ListView();
                int MasterID = 0;
                if (PrivilageFor == 1)
                {
                    subList = (ListView)mainList.Items[i].FindControl("RoleListLeft");
                }
                if (PrivilageFor == 2)
                {
                    subList = (ListView)mainList.Items[i].FindControl("sysListLeft");
                }
                if (PrivilageFor == 3)
                {
                    subList = (ListView)mainList.Items[i].FindControl("userListLeft");
                }
                for (int j = 0; j < subList.Items.Count; j++)
                {

                    int MenuID = 0;
                    CheckBox chadd = new CheckBox();
                    CheckBox chedit = new CheckBox();
                    CheckBox chdelete = new CheckBox();
                    CheckBox chprint = new CheckBox();
                    CheckBox chLabel = new CheckBox();
                    CheckBox chAdmin = new CheckBox();
                    CheckBox chrolesp1 = new CheckBox();
                    CheckBox chrolesp2 = new CheckBox();
                    CheckBox chrolesp3 = new CheckBox();
                    CheckBox chrolesp4 = new CheckBox();
                    CheckBox chrolesp5 = new CheckBox();
                    CheckBox Menu = new CheckBox();

                    if (PrivilageFor == 1)
                    {
                        MenuID = Convert.ToInt32(((Label)subList.Items[j].FindControl("lblRoleListLeftMENU_ID")).Text);
                        chadd = (CheckBox)subList.Items[j].FindControl("chroleadd");
                        chedit = (CheckBox)subList.Items[j].FindControl("chroleedit");
                        chdelete = (CheckBox)subList.Items[j].FindControl("chroledelete");
                        chprint = (CheckBox)subList.Items[j].FindControl("chroleprint");
                        chLabel = (CheckBox)subList.Items[j].FindControl("chroleLabel");
                        chAdmin = (CheckBox)subList.Items[j].FindControl("chroleAdmin");
                        chrolesp1 = (CheckBox)subList.Items[j].FindControl("chrolesp1");
                        chrolesp2 = (CheckBox)subList.Items[j].FindControl("chrolesp2");
                        chrolesp3 = (CheckBox)subList.Items[j].FindControl("chrolesp3");
                        chrolesp4 = (CheckBox)subList.Items[j].FindControl("chrolesp4");
                        chrolesp5 = (CheckBox)subList.Items[j].FindControl("chrolesp5");
                        Menu = (CheckBox)subList.Items[j].FindControl("CHroleMenu");
                        //btnGenerateRoleTemp.Visible = true;
                    }
                    if (PrivilageFor == 2)
                    {
                        MenuID = Convert.ToInt32(((Label)subList.Items[j].FindControl("lblsysListLeftMENU_ID")).Text);
                        chadd = (CheckBox)subList.Items[j].FindControl("chsysadd");
                        chedit = (CheckBox)subList.Items[j].FindControl("chsysedit");
                        chdelete = (CheckBox)subList.Items[j].FindControl("chsysdelete");
                        chprint = (CheckBox)subList.Items[j].FindControl("chsysprint");
                        chLabel = (CheckBox)subList.Items[j].FindControl("chsysLabel");
                        chAdmin = (CheckBox)subList.Items[j].FindControl("chsysAdmin");
                        chrolesp1 = (CheckBox)subList.Items[j].FindControl("chsyssp1");
                        chrolesp2 = (CheckBox)subList.Items[j].FindControl("chsyssp2");
                        chrolesp3 = (CheckBox)subList.Items[j].FindControl("chsyssp3");
                        chrolesp4 = (CheckBox)subList.Items[j].FindControl("chsyssp4");
                        chrolesp5 = (CheckBox)subList.Items[j].FindControl("chsyssp5");
                        Menu = (CheckBox)subList.Items[j].FindControl("CHsysMenu");

                    }
                    if (PrivilageFor == 3)
                    {
                        MenuID = Convert.ToInt32(((Label)subList.Items[j].FindControl("lbluserListLeftMENU_ID")).Text);
                        chadd = (CheckBox)subList.Items[j].FindControl("chuseradd");
                        chedit = (CheckBox)subList.Items[j].FindControl("chuseredit");
                        chdelete = (CheckBox)subList.Items[j].FindControl("chuserdelete");
                        chprint = (CheckBox)subList.Items[j].FindControl("chuserprint");
                        chLabel = (CheckBox)subList.Items[j].FindControl("chuserLabel");
                        chAdmin = (CheckBox)subList.Items[j].FindControl("chuserAdmin");
                        chrolesp1 = (CheckBox)subList.Items[j].FindControl("chusersp1");
                        chrolesp2 = (CheckBox)subList.Items[j].FindControl("chusersp2");
                        chrolesp3 = (CheckBox)subList.Items[j].FindControl("chusersp3");
                        chrolesp4 = (CheckBox)subList.Items[j].FindControl("chusersp4");
                        chrolesp5 = (CheckBox)subList.Items[j].FindControl("chusersp5");
                        Menu = (CheckBox)subList.Items[j].FindControl("CHuserMenu");
                        //btnGenerateUserTemp.Visible = true;
                    }

                    objPRIVILAGE_MENU = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == MenuID).First();

                    objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                    objPRIVILAGE_MENU.MySerial = j;

                    objPRIVILAGE_MENU.ALL_FLAG = chAdmin.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ADD_FLAG = chadd.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.MODIFY_FLAG = chedit.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.DELETE_FLAG = chdelete.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.VIEW_FLAG = chprint.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.LABEL_FLAG = chLabel.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP1 = chrolesp1.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP2 = chrolesp2.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP3 = chrolesp3.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP4 = chrolesp4.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP5 = chrolesp5.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ActiveMenu = Menu.Checked == true ? "Y" : "N";

                    objPRIVILAGE_MENU.LastUpdate = DateTime.Now;
                    DB.SaveChanges();
                }
                if (PrivilageFor == 2)
                {
                    List<Database.PRIVILAGE_MENUDemon> PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor).ToList();
                    string ActiveModule = "";// CHKModule.Checked == true ? "Y" : "N";

                    foreach (Database.PRIVILAGE_MENUDemon item in PRIList)
                    {
                        if (ActiveModule == "Y")
                        {
                            Database.PRIVILAGE_MENUDemon objModule = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID);
                            objModule.ActiveModule = ActiveModule;
                            DB.SaveChanges();
                        }
                        else
                        {
                            Database.PRIVILAGE_MENUDemon objModule = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.MENU_ID == item.MENU_ID);
                            objModule.ActiveModule = ActiveModule;
                            DB.SaveChanges();
                        }
                    }
                }

                if (PrivilageFor == 1)
                {
                    List<Database.PRIVILAGE_MENUDemon> PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor).ToList();
                    //string Activerole = CHKRoleActive.Checked == true ? "Y" : "N";
                    foreach (Database.PRIVILAGE_MENUDemon item in PRIList)
                    {
                        Database.PRIVILAGE_MENUDemon objrole = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID);
                        //objrole.Action = Activerole;
                        DB.SaveChanges();
                    }
                }
                if (PrivilageFor == 3)
                {
                    List<Database.PRIVILAGE_MENUDemon> PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILAGEFOR == PrivilageFor && p.TenentID == TID && p.Action == "Y").ToList();
                    List<Database.PRIVILAGE_MENUDemon> userwiseModule = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.ActiveModule == "Y").ToList();
                    //
                    if (PRIList.Count() > 0)
                    {
                        //CHKUserActive.Checked = true;
                    }
                    if (userwiseModule.Count() > 0)
                    {
                        //cheusewiseModule.Checked = true;
                    }
                }
            }
        }

        //protected void drpUsermodule_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Name = drpUsermodule.SelectedItem.ToString();
        //    lblforUser.Text = "User Wise Menu For " + Name;
        //}

        //protected void DrpRoleModule_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Name = DrpRoleModule.SelectedItem.ToString();
        //    lblRolewise.Text = "Role Wise Menu For " + Name;
        //}

        //protected void btnGenerateUserTemp_Click(object sender, EventArgs e)
        //{
        //    int UIDD = Convert.ToInt32(Drpuserall.SelectedValue);
        //    int PrivilageID = Convert.ToInt32(drpUsermodule.SelectedValue);
        //    int Modulidd = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UIDD && p.PRIVILEGE_ID == PrivilageID).MODULE_ID;

        //    GlobleClass.DeleteTempUser(TID, UIDD, 1, Modulidd);
        //    GlobleClass.getMenuGloble(TID, UIDD, 1, Modulidd);
        //}

        //protected void btnGenerateRoleTemp_Click(object sender, EventArgs e)
        //{
        //    int PrivilageID = Convert.ToInt32(DrpRoleModule.SelectedValue);
        //    int RoleID = Convert.ToInt32(drproleMST.SelectedValue);
        //    List<Database.USER_ROLE> RoleUserList = DB.USER_ROLE.Where(p => p.TenentID == TID && p.PRIVILEGE_ID == PrivilageID).ToList();
        //    foreach (Database.USER_ROLE item in RoleUserList)
        //    {
        //        int UIDD = Convert.ToInt32(item.USER_ID);
        //        int Modulidd = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UIDD && p.PRIVILEGE_ID == PrivilageID).MODULE_ID;
        //        GlobleClass.DeleteTempUser(TID, UIDD, 1, Modulidd);
        //        GlobleClass.getMenuGloble(TID, UIDD, 1, Modulidd);
        //    }
        //}




        //protected void btnAddnewRole_Click(object sender, EventArgs e)
        //{
        //    Database.ROLE_MST objROLE_MST = new Database.ROLE_MST();

        //    objROLE_MST.TenentID = TID;
        //    objROLE_MST.ROLE_ID = DB.ROLE_MST.Count() > 0 ? Convert.ToInt32(DB.ROLE_MST.Max(p => p.ROLE_ID) + 1) : 1;
        //    objROLE_MST.ROLE_NAME = txtaddrole.Text;
        //    objROLE_MST.ROLE_NAME1 = txtaddrole.Text;
        //    objROLE_MST.ROLE_NAME2 = txtaddrole.Text;
        //    objROLE_MST.ROLE_DESC = txtaddrole.Text;
        //    objROLE_MST.ACTIVE_FLAG = "Y";
        //    objROLE_MST.ACTIVE_FROM_DT = DateTime.Now;
        //    objROLE_MST.ACTIVE_TO_DT = new DateTime(DateTime.Today.Year, 12, 1);
        //    objROLE_MST.ACTIVEROLE = true;
        //    objROLE_MST.ROLLDATE = new DateTime(DateTime.Today.Year, 12, 1);

        //    DB.ROLE_MST.AddObject(objROLE_MST);
        //    DB.SaveChanges();
        //    int RID = Convert.ToInt32(objROLE_MST.ROLE_ID);


        //    for (int j = 0; j < CHKModuleAdd.Items.Count; j++)
        //    {
        //        int PRIVILEGE_ID = Convert.ToInt32(CHKModuleAdd.Items[j].Value);
        //        Database.USER_ROLE OBJrole = new Database.USER_ROLE();
        //        OBJrole.TenentID = TID;
        //        OBJrole.USER_ROLE_ID = DB.USER_ROLE.Count() > 0 ? Convert.ToInt32(DB.USER_ROLE.Max(p => p.USER_ROLE_ID) + 1) : 1;
        //        OBJrole.PRIVILEGE_ID = PRIVILEGE_ID;
        //        OBJrole.LOCATION_ID = 1;
        //        OBJrole.USER_ID = Convert.ToInt32(drpadduser.SelectedValue);
        //        OBJrole.ROLE_ID = RID;
        //        if (CHKModuleAdd.Items[j].Selected)
        //        {
        //            OBJrole.ACTIVE_FLAG = "Y";
        //        }
        //        else
        //        {
        //            OBJrole.ACTIVE_FLAG = "N";
        //        }
        //        OBJrole.ACTIVE_FROM_DT = DateTime.Now;
        //        OBJrole.ACTIVE_TO_DT = new DateTime(DateTime.Today.Year, 12, 1);
        //        OBJrole.CRUP_ID = 0;
        //        OBJrole.ALL_FLAG = 0;
        //        OBJrole.ADD_FLAG = 0;
        //        OBJrole.MODIFY_FLAG = 0;
        //        OBJrole.DELETE_FLAG = 0;
        //        OBJrole.VIEW_FLAG = 0;
        //        OBJrole.SP1 = 0;
        //        OBJrole.SP2 = 0;
        //        OBJrole.SP3 = 0;
        //        OBJrole.SP4 = 0;
        //        OBJrole.SP5 = 0;
        //        OBJrole.SP1Name = null;
        //        OBJrole.SP2Name = null;
        //        OBJrole.SP3Name = null;
        //        OBJrole.SP4Name = null;
        //        OBJrole.SP5Name = null;
        //        DB.USER_ROLE.AddObject(OBJrole);
        //        DB.SaveChanges();
        //    }
        //    userBind();
        //}



        protected void LinkAddNewRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ACM/ACM_ROLE_MST.aspx?TID=" + TID);
        }

        protected void btnproccess_Click(object sender, EventArgs e)
        {
            List<Database.FUNCTION_MST> List = new List<FUNCTION_MST>();
            List<Database.PRIVILAGE_MENUDemon> FinalList = new List<PRIVILAGE_MENUDemon>();
            List<Database.PRIVILAGE_MENUDemon> DemoTempList = new List<PRIVILAGE_MENUDemon>();

            for (int j = 0; j < Checkmodule.Items.Count; j++)
            {
                if (Checkmodule.Items[j].Selected)
                {
                    int roleid = Convert.ToInt32(drprole.SelectedValue);
                    int moduleid = Convert.ToInt32(Checkmodule.Items[j].Value);
                    int privilageid = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == moduleid).PRIVILEGE_ID;
                    List = DB.FUNCTION_MST.Where(p => p.MODULE_ID == moduleid && p.ACTIVE_FLAG == 1).OrderBy(p => p.MENU_ORDER).OrderBy(p => p.MENU_NAME1).ToList();
                    DemoTempList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == roleid && p.PRIVILEGE_ID == privilageid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();

                    foreach (Database.FUNCTION_MST item in List)
                    {
                        if (DemoTempList.Where(p => p.MENU_ID == item.MENU_ID).Count() == 0)
                        {
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = roleid;
                            objPRIVILAGE_MENU.TenentID = TID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = privilageid;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 1;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = item.MENUDATE;
                            objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                            objPRIVILAGE_MENU.ALL_FLAG = "Y";
                            objPRIVILAGE_MENU.ADD_FLAG = "N";
                            objPRIVILAGE_MENU.MODIFY_FLAG = "N";
                            objPRIVILAGE_MENU.DELETE_FLAG = "N";
                            objPRIVILAGE_MENU.VIEW_FLAG = "N";
                            objPRIVILAGE_MENU.LABEL_FLAG = "N";
                            objPRIVILAGE_MENU.SP1 = "N";
                            objPRIVILAGE_MENU.SP2 = "N";
                            objPRIVILAGE_MENU.SP3 = "N";
                            objPRIVILAGE_MENU.SP4 = "N";
                            objPRIVILAGE_MENU.SP5 = "N";
                            objPRIVILAGE_MENU.ActiveMenu = "N";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.Action = "Y";
                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                            FinalList.Add(objPRIVILAGE_MENU);
                        }
                    }
                    //List<Database.PRIVILAGE_MENUDemon> listnew = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == moduleid && p.PRIVILEGE_ID == privilageid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 2 && p.MENU_LOCATION == "Separator").ToList();                    
                    //foreach(Database.PRIVILAGE_MENUDemon itemss in listnew)
                    //{
                    //    Database.PRIVILAGE_MENUDemon obj = DB.PRIVILAGE_MENUDemon.Single(p => p.MENU_ID == itemss.MENU_ID && p.PRIVILEGE_MENU_ID == moduleid && p.PRIVILEGE_ID == privilageid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 2 && p.MENU_LOCATION == "Separator");
                    //    FinalList.Add(obj);
                    //}
                }
            }
            if (FinalList.Count() > 0)
            {
                userListSeparate.DataSource = FinalList.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
                userListSeparate.DataBind();
                ViewState["FinalList"] = FinalList;
            }
        }

        protected void userListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lbluserSeparateMENU_ID");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView userListLeft = (ListView)e.Item.FindControl("userListLeft");
                Label lblprivilagemenuid = (Label)e.Item.FindControl("lblprivilagemenuid");
                Label lblprivilageid = (Label)e.Item.FindControl("lblprivilageid");
                int Pmenuid = Convert.ToInt32(lblprivilagemenuid.Text);
                int priid = Convert.ToInt32(lblprivilageid.Text);

                //List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == userM && p.PRIVILEGE_ID == privilageM && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == Pmenuid && p.PRIVILEGE_ID == priid && p.PRIVILAGEFOR == 1 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                userListLeft.DataSource = List;
                userListLeft.DataBind();

            }
        }

        protected void btnsave1_Click(object sender, EventArgs e)
        {
            //ViewState["FinalList"] = FinalList;
            List<Database.PRIVILAGE_MENUDemon> Final = ((List<Database.PRIVILAGE_MENUDemon>)ViewState["FinalList"]).ToList();
            int prid = Convert.ToInt32(drprole.SelectedValue);
            for (int j = 0; j < userListSeparate.Items.Count; j++)
            {
                ListView subList = (ListView)userListSeparate.Items[j].FindControl("userListLeft");
                for (int i = 0; i < subList.Items.Count; i++)
                {
                    int MenuID = Convert.ToInt32(((Label)subList.Items[i].FindControl("lbluserListLeftMENU_ID")).Text);
                    CheckBox chadd = (CheckBox)subList.Items[i].FindControl("chuseradd");
                    CheckBox chedit = (CheckBox)subList.Items[i].FindControl("chuseredit");
                    CheckBox chdelete = (CheckBox)subList.Items[i].FindControl("chuserdelete");
                    CheckBox chprint = (CheckBox)subList.Items[i].FindControl("chuserprint");
                    CheckBox chLabel = (CheckBox)subList.Items[i].FindControl("chuserLabel");
                    CheckBox chAdmin = (CheckBox)subList.Items[i].FindControl("chuserAdmin");
                    CheckBox chrolesp1 = (CheckBox)subList.Items[i].FindControl("chusersp1");
                    CheckBox chrolesp2 = (CheckBox)subList.Items[i].FindControl("chusersp2");
                    CheckBox chrolesp3 = (CheckBox)subList.Items[i].FindControl("chusersp3");
                    CheckBox chrolesp4 = (CheckBox)subList.Items[i].FindControl("chusersp4");
                    CheckBox chrolesp5 = (CheckBox)subList.Items[i].FindControl("chusersp5");
                    CheckBox Menu = (CheckBox)subList.Items[i].FindControl("CHuserMenu");
                    Label lblpriid = (Label)subList.Items[i].FindControl("lblpriid");
                    int priid = Convert.ToInt32(lblpriid.Text);

                    PRIVILAGE_MENUDemon objPRIVILAGE_MENU1 = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == prid && p.PRIVILEGE_ID == priid && p.TenentID == TID && p.PRIVILAGEFOR == 1 && p.MENU_ID == MenuID).First();
                    objPRIVILAGE_MENU1.ACTIVE_FLAG = "Y";
                    objPRIVILAGE_MENU1.MySerial = i;
                    objPRIVILAGE_MENU1.ALL_FLAG = chAdmin.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.ADD_FLAG = chadd.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.MODIFY_FLAG = chedit.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.DELETE_FLAG = chdelete.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.VIEW_FLAG = chprint.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.LABEL_FLAG = chLabel.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.SP1 = chrolesp1.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.SP2 = chrolesp2.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.SP3 = chrolesp3.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.SP4 = chrolesp4.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.SP5 = chrolesp5.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU1.ActiveMenu = Menu.Checked == true ? "Y" : "N";

                    objPRIVILAGE_MENU1.LastUpdate = DateTime.Now;
                    DB.SaveChanges();
                }
            }
        }



        protected void userListSeparateavove_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lbluserSeparateMENU_ID");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView userListLeftabove = (ListView)e.Item.FindControl("userListLeftabove");
                Label lblprivilagemenuid = (Label)e.Item.FindControl("lblprivilagemenuid");
                Label lblprivilageid = (Label)e.Item.FindControl("lblprivilageid");
                int Pmenuid = Convert.ToInt32(lblprivilagemenuid.Text);
                int priid = Convert.ToInt32(lblprivilageid.Text);

                //List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == userM && p.PRIVILEGE_ID == privilageM && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == Pmenuid && p.PRIVILEGE_ID == priid && p.PRIVILAGEFOR == 1 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                userListLeftabove.DataSource = List;
                userListLeftabove.DataBind();

            }
        }

        protected void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            //edit
            for (int i = 0; i < userListSeparateavove.Items.Count; i++)
            {
                ListView subList = (ListView)userListSeparateavove.Items[i].FindControl("userListLeftabove");
                for (int j = 0; j < subList.Items.Count; j++)
                {
                    CheckBox chuseredit = (CheckBox)subList.Items[j].FindControl("chuseredit");
                    if (CheckBox8.Checked == true)
                    {
                        chuseredit.Checked = true;
                    }
                    else
                    {
                        chuseredit.Checked = false;
                    }
                }
            }
        }
        public void ListBindd()
        {
            int roleid = Convert.ToInt32(drprole.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == roleid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            List<Database.PRIVILAGE_MENUDemon> ListFin = List;
            userListSeparate.DataSource = ListFin.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
            userListSeparate.DataBind();
        }
        protected void drprole_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roleid = Convert.ToInt32(drprole.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == roleid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            List<Database.PRIVILAGE_MENUDemon> ListFin = List;
            List = List.GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.PRIVILAGE_MENUDemon item in List)
            {
                int privilage = Convert.ToInt32(item.PRIVILEGE_ID);
                for (int i = 0; i < Checkmodule.Items.Count; i++)
                {
                    int Module = Convert.ToInt32(Checkmodule.Items[i].Value);
                    int PRIID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Module).PRIVILEGE_ID;
                    if (PRIID == privilage)
                    {
                        Checkmodule.Items[i].Selected = true;

                    }
                }
            }
            if (List.Count() > 0)
            {
                ListBindd();
                //userListSeparate.DataSource = ListFin.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
                //userListSeparate.DataBind();
                //btndeletemodule.Visible = true;
            }
            AddMod();
            DeleteMod();
        }
        public void AddMod()
        {
            int roleid = Convert.ToInt32(drprole.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == roleid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            List = List.GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();
            //add module
            List<Database.MODULE_MST> modul = new List<Database.MODULE_MST>();
            foreach (Database.PRIVILAGE_MENUDemon item in List)
            {
                int privilage = Convert.ToInt32(item.PRIVILEGE_ID);
                int mod = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == privilage).MODULE_ID;
                if (DB.MODULE_MST.Where(p => p.Module_Id == mod).Count() > 0)
                {
                    Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == mod);
                    modul.Add(obj);
                }
            }
            drpdeletemodule.DataSource = modul;
            drpdeletemodule.DataTextField = "Module_Name";
            drpdeletemodule.DataValueField = "Module_Id";
            drpdeletemodule.DataBind();
            drpdeletemodule.Items.Insert(0, new ListItem("--select Module--", "0"));


        }
        public void DeleteMod()
        {
            int roleid = Convert.ToInt32(drprole.SelectedValue);
            //delete module
            List<Database.MODULE_MST> modul1 = new List<Database.MODULE_MST>();

            List<Database.MODULE_MAP> ListModuleR = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y").GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.MODULE_MAP item in ListModuleR)
            {
                if (DB.MODULE_MST.Where(p => p.Module_Id == item.MODULE_ID).Count() > 0)
                {
                    int mid = Convert.ToInt32(item.MODULE_ID);
                    int pid = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == mid).PRIVILEGE_ID;
                    if (DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILEGE_MENU_ID == roleid && p.PRIVILEGE_ID == pid).Count() > 0)
                    {

                    }
                    else
                    {
                        Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == item.MODULE_ID);
                        modul1.Add(obj);
                    }

                }

            }

            drpaddmodule.DataSource = modul1;
            drpaddmodule.DataTextField = "Module_Name";
            drpaddmodule.DataValueField = "Module_Id";
            drpaddmodule.DataBind();
            drpaddmodule.Items.Insert(0, new ListItem("--select Module--", "0"));
        }
        //add or selete module wise data in privilage_menu
        protected void btnAddmodule_Click(object sender, EventArgs e)
        {
            List<Database.FUNCTION_MST> List = new List<FUNCTION_MST>();
            List<Database.PRIVILAGE_MENUDemon> FinalList = new List<PRIVILAGE_MENUDemon>();
            List<Database.PRIVILAGE_MENUDemon> DemoTempList = new List<PRIVILAGE_MENUDemon>();


            int roleid = Convert.ToInt32(drprole.SelectedValue);
            int moduleid = Convert.ToInt32(drpaddmodule.SelectedValue);
            int privilageid = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == moduleid).PRIVILEGE_ID;
            List = DB.FUNCTION_MST.Where(p => p.MODULE_ID == moduleid && p.ACTIVE_FLAG == 1).OrderBy(p => p.MENU_ORDER).OrderBy(p => p.MENU_NAME1).ToList();
            DemoTempList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == roleid && p.PRIVILEGE_ID == privilageid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();

            foreach (Database.FUNCTION_MST item in List)
            {
                if (DemoTempList.Where(p => p.MENU_ID == item.MENU_ID).Count() == 0)
                {
                    PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                    objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = roleid;
                    objPRIVILAGE_MENU.TenentID = TID;
                    objPRIVILAGE_MENU.PRIVILEGE_ID = privilageid;
                    objPRIVILAGE_MENU.PRIVILAGEFOR = 1;
                    objPRIVILAGE_MENU.LOCATION_ID = 1;
                    objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                    objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                    objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                    objPRIVILAGE_MENU.ACTIVETILLDATE = item.MENUDATE;
                    objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                    objPRIVILAGE_MENU.ALL_FLAG = "Y";
                    objPRIVILAGE_MENU.ADD_FLAG = "Y";
                    objPRIVILAGE_MENU.MODIFY_FLAG = "Y";
                    objPRIVILAGE_MENU.DELETE_FLAG = "Y";
                    objPRIVILAGE_MENU.VIEW_FLAG = "Y";
                    objPRIVILAGE_MENU.LABEL_FLAG = "Y";
                    objPRIVILAGE_MENU.SP1 = "N";
                    objPRIVILAGE_MENU.SP2 = "N";
                    objPRIVILAGE_MENU.SP3 = "N";
                    objPRIVILAGE_MENU.SP4 = "N";
                    objPRIVILAGE_MENU.SP5 = "N";
                    objPRIVILAGE_MENU.ActiveMenu = "Y";
                    objPRIVILAGE_MENU.ActiveModule = "Y";
                    objPRIVILAGE_MENU.Action = "Y";
                    objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                    DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                    DB.SaveChanges();
                    FinalList.Add(objPRIVILAGE_MENU);
                }
            }

            if (FinalList.Count() > 0)
            {

                //userListSeparate.DataSource = FinalList.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
                //userListSeparate.DataBind();
                ViewState["FinalList"] = FinalList;
            }
            ListBindd();
            AddMod();
            DeleteMod();
        }

        //delete or selete module wise data in privilage_menu
        protected void btndeletemodule_Click(object sender, EventArgs e)
        {
            int roleid = Convert.ToInt32(drprole.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == roleid && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            foreach (Database.PRIVILAGE_MENUDemon item in List)
            {
                int privilage = Convert.ToInt32(item.PRIVILEGE_ID);
                int Module = Convert.ToInt32(drpdeletemodule.SelectedValue);
                int PRIID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Module).PRIVILEGE_ID;
                if (DB.PRIVILAGE_MENUDemon.Where(p => p.MENU_ID == item.MENU_ID && p.PRIVILEGE_MENU_ID == roleid && p.PRIVILEGE_ID == PRIID && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).Count() > 0)
                {
                    Database.PRIVILAGE_MENUDemon objdel = DB.PRIVILAGE_MENUDemon.Single(p => p.MENU_ID == item.MENU_ID && p.PRIVILEGE_MENU_ID == roleid && p.PRIVILEGE_ID == PRIID && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1);
                    DB.PRIVILAGE_MENUDemon.DeleteObject(objdel);
                    DB.SaveChanges();
                }
            }
            ListBindd();
            AddMod();
            DeleteMod();
        }

        protected void btnadduserrolemodele_Click(object sender, EventArgs e)
        {
            int role = Convert.ToInt32(drprole1.SelectedValue);
            int modile = Convert.ToInt32(drpmodule.SelectedValue);
            int user = Convert.ToInt32(drpuserr.SelectedValue);
            int PRIVILEGE_ID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == modile).PRIVILEGE_ID;
            if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.ROLE_ID == role && p.USER_ID == user && p.PRIVILEGE_ID == PRIVILEGE_ID).Count() > 0)
            {

            }
            else
            {
                Database.USER_ROLE OBJrole = new Database.USER_ROLE();
                OBJrole.TenentID = TID;
                OBJrole.USER_ROLE_ID = DB.USER_ROLE.Count() > 0 ? Convert.ToInt32(DB.USER_ROLE.Max(p => p.USER_ROLE_ID) + 1) : 1;
                OBJrole.PRIVILEGE_ID = PRIVILEGE_ID;
                OBJrole.LOCATION_ID = 1;
                OBJrole.USER_ID = user;
                OBJrole.ROLE_ID = role;
                OBJrole.ACTIVE_FLAG = "Y";
                OBJrole.ACTIVE_FROM_DT = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_ID == role).ACTIVE_FROM_DT;
                OBJrole.ACTIVE_TO_DT = DB.ROLE_MST.Single(p => p.TenentID == TID && p.ROLE_ID == role).ACTIVE_TO_DT;
                OBJrole.CRUP_ID = 0;
                OBJrole.ALL_FLAG = 0;
                OBJrole.ADD_FLAG = 0;
                OBJrole.MODIFY_FLAG = 0;
                OBJrole.DELETE_FLAG = 0;
                OBJrole.VIEW_FLAG = 0;
                OBJrole.SP1 = 0;
                OBJrole.SP2 = 0;
                OBJrole.SP3 = 0;
                OBJrole.SP4 = 0;
                OBJrole.SP5 = 0;
                OBJrole.SP1Name = null;
                OBJrole.SP2Name = null;
                OBJrole.SP3Name = null;
                OBJrole.SP4Name = null;
                OBJrole.SP5Name = null;
                DB.USER_ROLE.AddObject(OBJrole);
                DB.SaveChanges();
            }
            AddROLEMod();
            DeleteROLEMod();
            BindURoleList();
            //List<Database.PRIVILAGE_MENUDemon> Demo = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == role && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            //userListSeparateavove.DataSource = Demo.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
            //userListSeparateavove.DataBind();
        }
        protected void btnDeleteuserrolemodule_Click(object sender, EventArgs e)
        {
            int role = Convert.ToInt32(drprole1.SelectedValue);
            int user = Convert.ToInt32(drpuserr.SelectedValue);
            int Module = Convert.ToInt32(drpdeleteModuleR.SelectedValue);
            int PRIVILEGE_ID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Module).PRIVILEGE_ID;
            if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.ROLE_ID == role && p.USER_ID == user && p.PRIVILEGE_ID == PRIVILEGE_ID).Count() > 0)
            {
                Database.USER_ROLE obj = DB.USER_ROLE.Single(p => p.TenentID == TID && p.ROLE_ID == role && p.USER_ID == user && p.PRIVILEGE_ID == PRIVILEGE_ID);
                DB.USER_ROLE.DeleteObject(obj);
                DB.SaveChanges();
            }
            AddROLEMod();
            DeleteROLEMod();
            BindURoleList();
        }
        public void AddROLEMod()
        {
            List<Database.MODULE_MST> Modulee = new List<Database.MODULE_MST>();
            int role = Convert.ToInt32(drprole1.SelectedValue);
            int user = Convert.ToInt32(drpuserr.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == role && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            List = List.GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();

            foreach (Database.PRIVILAGE_MENUDemon item in List)
            {
                int privilage = Convert.ToInt32(item.PRIVILEGE_ID);
                int mod = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == privilage).MODULE_ID;
                if (DB.MODULE_MST.Where(p => p.Module_Id == mod).Count() > 0)
                {
                    if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.PRIVILEGE_ID == privilage && p.USER_ID == user && p.ROLE_ID == role).Count() == 0)
                    {
                        Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == mod);
                        Modulee.Add(obj);
                    }
                }
            }
            drpmodule.DataSource = Modulee;
            drpmodule.DataTextField = "Module_Name";
            drpmodule.DataValueField = "Module_Id";
            drpmodule.DataBind();
            drpmodule.Items.Insert(0, new ListItem("--select Module--", "0"));

        }
        public void DeleteROLEMod()
        {
            List<Database.MODULE_MST> Delmodul = new List<Database.MODULE_MST>();
            int role = Convert.ToInt32(drprole1.SelectedValue);
            int user = Convert.ToInt32(drpuserr.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == role && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
            List = List.GroupBy(p => p.PRIVILEGE_ID).Select(p => p.FirstOrDefault()).ToList();
            //Delete
            foreach (Database.PRIVILAGE_MENUDemon item in List)
            {
                int privilage = Convert.ToInt32(item.PRIVILEGE_ID);
                int mod = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == privilage).MODULE_ID;
                if (DB.MODULE_MST.Where(p => p.Module_Id == mod).Count() > 0)
                {
                    if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.PRIVILEGE_ID == privilage && p.USER_ID == user && p.ROLE_ID == role).Count() > 0)
                    {
                        Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == mod);
                        Delmodul.Add(obj);
                    }
                }
            }
            drpdeleteModuleR.DataSource = Delmodul;
            drpdeleteModuleR.DataTextField = "Module_Name";
            drpdeleteModuleR.DataValueField = "Module_Id";
            drpdeleteModuleR.DataBind();
            drpdeleteModuleR.Items.Insert(0, new ListItem("--select Module--", "0"));
        }
        public void BindURoleList()
        {
            int role = Convert.ToInt32(drprole1.SelectedValue);
            int user = Convert.ToInt32(drpuserr.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> Finel = new List<Database.PRIVILAGE_MENUDemon>();
            List<Database.USER_ROLE> RoleLuserList = DB.USER_ROLE.Where(p => p.TenentID == TID && p.ROLE_ID == role && p.USER_ID == user).ToList();
            foreach (Database.USER_ROLE itemss in RoleLuserList)
            {
                int privilage = Convert.ToInt32(itemss.PRIVILEGE_ID);
                List<Database.PRIVILAGE_MENUDemon> Temp = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == role && p.PRIVILEGE_ID == privilage && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
                foreach (Database.PRIVILAGE_MENUDemon Tempitem in Temp)
                {
                    Database.PRIVILAGE_MENUDemon obj = DB.PRIVILAGE_MENUDemon.Single(p => p.MENU_ID == Tempitem.MENU_ID && p.PRIVILEGE_MENU_ID == Tempitem.PRIVILEGE_MENU_ID && p.PRIVILEGE_ID == Tempitem.PRIVILEGE_ID && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1);
                    Finel.Add(obj);
                }
            }
            userListSeparateavove.DataSource = Finel.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
            userListSeparateavove.DataBind();
        }
        protected void drpuserr_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddROLEMod();
            DeleteROLEMod();
            BindURoleList();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindURoleList();
        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            int PRIVILEGE_ID = 0;
            int PRIVILEGE_MENUID = 0;

            PRIVILEGE_MENUID = Convert.ToInt32(drprole1.SelectedValue);

            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
            for (int i = 0; i < userListSeparateavove.Items.Count; i++)
            {
                ListView subList = new ListView();
                int MasterID = 0;

                subList = (ListView)userListSeparateavove.Items[i].FindControl("userListLeftabove");

                for (int j = 0; j < subList.Items.Count; j++)
                {

                    int MenuID = 0;
                    CheckBox chadd = new CheckBox();
                    CheckBox chedit = new CheckBox();
                    CheckBox chdelete = new CheckBox();
                    CheckBox chprint = new CheckBox();
                    CheckBox chLabel = new CheckBox();
                    CheckBox chAdmin = new CheckBox();
                    CheckBox chrolesp1 = new CheckBox();
                    CheckBox chrolesp2 = new CheckBox();
                    CheckBox chrolesp3 = new CheckBox();
                    CheckBox chrolesp4 = new CheckBox();
                    CheckBox chrolesp5 = new CheckBox();
                    CheckBox Menu = new CheckBox();

                    Label lblpriid = (Label)subList.Items[j].FindControl("lblpriid");
                    PRIVILEGE_ID = Convert.ToInt32(lblpriid.Text);
                    MenuID = Convert.ToInt32(((Label)subList.Items[j].FindControl("lbluserListLeftMENU_ID")).Text);
                    chadd = (CheckBox)subList.Items[j].FindControl("chuseradd");
                    chedit = (CheckBox)subList.Items[j].FindControl("chuseredit");
                    chdelete = (CheckBox)subList.Items[j].FindControl("chuserdelete");
                    chprint = (CheckBox)subList.Items[j].FindControl("chuserprint");
                    chLabel = (CheckBox)subList.Items[j].FindControl("chuserLabel");
                    chAdmin = (CheckBox)subList.Items[j].FindControl("chuserAdmin");
                    chrolesp1 = (CheckBox)subList.Items[j].FindControl("chusersp1");
                    chrolesp2 = (CheckBox)subList.Items[j].FindControl("chusersp2");
                    chrolesp3 = (CheckBox)subList.Items[j].FindControl("chusersp3");
                    chrolesp4 = (CheckBox)subList.Items[j].FindControl("chusersp4");
                    chrolesp5 = (CheckBox)subList.Items[j].FindControl("chusersp5");
                    Menu = (CheckBox)subList.Items[j].FindControl("CHuserMenu");

                    objPRIVILAGE_MENU = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == 1 && p.MENU_ID == MenuID).First();

                    objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                    objPRIVILAGE_MENU.MySerial = j;

                    objPRIVILAGE_MENU.ALL_FLAG = chAdmin.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ADD_FLAG = chadd.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.MODIFY_FLAG = chedit.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.DELETE_FLAG = chdelete.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.VIEW_FLAG = chprint.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.LABEL_FLAG = chLabel.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP1 = chrolesp1.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP2 = chrolesp2.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP3 = chrolesp3.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP4 = chrolesp4.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP5 = chrolesp5.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ActiveMenu = Menu.Checked == true ? "Y" : "N";

                    objPRIVILAGE_MENU.LastUpdate = DateTime.Now;
                    DB.SaveChanges();
                }
            }
            btnGenerateRoleTemp.Visible = true;
        }

        protected void btnpnlrole_Click(object sender, EventArgs e)
        {
            RoleSetpanel("block", "collapse");
            UserRolepanel("none", "expand");
        }
        public void RoleSetpanel(string link, string panal)
        {
            pnlRole.Style.Add("display", link);
            pnlRoleCE.Attributes["class"] = panal;
        }

        protected void btnpnluserrole_Click(object sender, EventArgs e)
        {
            UserRolepanel("block", "collapse");
            RoleSetpanel("none", "expand");
        }
        public void UserRolepanel(string link, string panal)
        {
            pnlUserRole.Style.Add("display", link);
            pnluserRoleCE.Attributes["class"] = panal;
        }

        protected void btnGenerateRoleTemp_Click(object sender, EventArgs e)
        {
            int UIDD = Convert.ToInt32(drpuserr.SelectedValue);
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
            //SqlCommand command2 = new SqlCommand();
            //string Str = "";
            //Str += "delete from tempUser1 where TenentID=" + TID + " and UserID=" + UIDD + ";";
            //command2 = new SqlCommand(Str, con);
            //con.Open();
            //command2.ExecuteReader();
            //con.Close();
            var ListTempUser = DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == UIDD).ToList();
            if (ListTempUser.Count > 0)
            {
                foreach (Database.tempUser1 item in ListTempUser)
                {
                    DB.tempUser1.DeleteObject(item);
                    DB.SaveChanges();
                }
            }

            var RoleUserList = DB.USER_ROLE.Where(p => p.TenentID == TID && p.USER_ID == UIDD).ToList();

            foreach (Database.USER_ROLE item in RoleUserList)
            {
                int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                var moduleid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UIDD && p.PRIVILEGE_ID == PrivilageID);
                int Modulidd = moduleid.MODULE_ID;
                int RID = Convert.ToInt32(item.ROLE_ID);
                //GlobleClass.getUserMenuGloble(TID, UIDD, 1, Modulidd,RID);
                getUserMenuGloble(TID, UIDD, 1, Modulidd, RID);
            }
            btnGenerateRoleTemp.Visible = false;
        }

        public void getUserMenuGloble(int TenentId, int UserID, int LocationID, int ModuleID, int Roleid)
        {

            List<Database.tempUser1> userlist = new List<Database.tempUser1>();
            Database.tempUser1 obj = new Database.tempUser1();
            Database.FUNCTION_MST MenuObj = new Database.FUNCTION_MST();
            //for MODULE_MAP
            var result2 = (from pm in DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TenentId && p.LOCATION_ID == LocationID && p.PRIVILAGEFOR == 2 && p.PRIVILEGE_MENU_ID == ModuleID)
                           join
                                Module in DB.MODULE_MAP.Where(p => p.TenentID == TenentId && p.LOCATION_ID == LocationID && p.UserID == UserID && p.ACTIVE_FLAG == "Y" && p.MODULE_ID == ModuleID) on pm.PRIVILEGE_ID equals Module.PRIVILEGE_ID

                           select new { Module.TenentID, Module.PRIVILEGE_ID, Module.MODULE_ID, pm.PRIVILAGEFOR, pm.PRIVILEGE_MENU_ID, pm.MENU_ID, pm.LOCATION_ID, Module.UserID, pm.ADD_FLAG, pm.ALL_FLAG, pm.MODIFY_FLAG, pm.DELETE_FLAG, pm.VIEW_FLAG, pm.ActiveMenu, pm.ActiveModule }).ToList();//Module.ACTIVE_FLAG, //pm.ActiveMenu is Menu ,pm.ActiveModule is Module
            var List = result2.GroupBy(p => p.MENU_ID).Select(p => p.FirstOrDefault()).ToList();

            foreach (var item2 in List)
            {

                obj = new Database.tempUser1();
                if (DB.FUNCTION_MST.Where(p => p.MENU_ID == item2.MENU_ID && p.MODULE_ID == item2.MODULE_ID).Count() > 0)
                {
                    MenuObj = DB.FUNCTION_MST.Single(p => p.MENU_ID == item2.MENU_ID && p.MODULE_ID == item2.MODULE_ID);

                    obj.TenentID = item2.TenentID;
                    obj.LocationID = item2.LOCATION_ID;
                    obj.PRIVILAGEID = item2.PRIVILEGE_ID;
                    obj.PRIVILAGESOURCE = "2";
                    obj.MENUID = item2.MENU_ID;
                    obj.PRIVILAGE_MENUID = item2.PRIVILEGE_MENU_ID;
                    obj.MODULE_ID = item2.MODULE_ID;
                    obj.UserID = item2.UserID;
                    obj.ROLE_ID = Roleid;
                    obj.ADD_FLAG = item2.ADD_FLAG;
                    obj.MODIFY_FLAG = item2.MODIFY_FLAG;
                    obj.DELETE_FLAG = item2.DELETE_FLAG;
                    obj.VIEW_FLAG = item2.VIEW_FLAG;
                    obj.ALL_FLAG = item2.ALL_FLAG;
                    obj.PRINTFLAGE = item2.PRIVILEGE_ID;
                    obj.LINK = MenuObj.LINK;
                    obj.MASTER_ID = MenuObj.MASTER_ID;
                    obj.MENU_TYPE = MenuObj.MENU_TYPE;
                    obj.MENU_NAME1 = MenuObj.MENU_NAME1;
                    obj.MENU_NAME2 = MenuObj.MENU_NAME2;
                    obj.MENU_NAME3 = MenuObj.MENU_NAME3;
                    obj.URLREWRITE = MenuObj.URLREWRITE;
                    obj.MENU_LOCATION = MenuObj.MENU_LOCATION;
                    obj.MENU_ORDER = MenuObj.MENU_ORDER;
                    obj.DOC_PARENT = MenuObj.DOC_PARENT;
                    obj.AMIGLOBALE = MenuObj.AMIGLOBALE;
                    obj.MYPERSONAL = MenuObj.MYPERSONAL;
                    obj.SMALLTEXT = MenuObj.SMALLTEXT;
                    obj.ICONPATH = MenuObj.ICONPATH;
                    obj.METATITLE = MenuObj.METATITLE;
                    obj.METAKEYWORD = MenuObj.METAKEYWORD;
                    obj.METADESCRIPTION = MenuObj.METADESCRIPTION;
                    obj.HEADERVISIBLEDATA = MenuObj.HEADERVISIBLEDATA;
                    obj.HEADERINVISIBLEDATA = MenuObj.HEADERINVISIBLEDATA;
                    obj.FOOTERVISIBLEDATA = MenuObj.FOOTERVISIBLEDATA;
                    obj.FOOTERINVISIBLEDATA = MenuObj.FOOTERINVISIBLEDATA;
                    obj.SP1 = 0;
                    obj.SP2 = 0;
                    obj.SP3 = 0;
                    obj.SP4 = 0;
                    obj.SP5 = 0;
                    obj.REFID = MenuObj.REFID;
                    obj.MYBUSID = MenuObj.MYBUSID;
                    obj.ACTIVETILLDATE = MenuObj.ACTIVETILLDATE;
                    obj.MENUDATE = MenuObj.MENUDATE;
                    if (obj.MENU_LOCATION == "Separator")
                        obj.ACTIVEMENU = true;
                    else
                    {
                        obj.ACTIVEMENU = false;//item2.ActiveMenu == "Y" && MenuObj.ACTIVE_FLAG == 1 ? true : false;
                    }
                    obj.ACTIVEMODULE = item2.ActiveModule == "Y" ? true : false;
                    DB.tempUser1.AddObject(obj);
                    DB.SaveChanges();
                }
            }

            int MID = 0;
            //for USER_ROLE
            var result1 = (from pm in DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TenentId && p.LOCATION_ID == LocationID && p.PRIVILAGEFOR == 1 && p.PRIVILEGE_MENU_ID == Roleid)
                           join
                             ur in DB.USER_ROLE.Where(p => p.TenentID == TenentId && p.LOCATION_ID == LocationID && p.USER_ID == UserID && p.ACTIVE_FLAG == "Y" && p.ACTIVE_FROM_DT <= DateTime.Now && p.ACTIVE_TO_DT >= DateTime.Now) on pm.PRIVILEGE_ID equals ur.PRIVILEGE_ID

                           select new { ur.TenentID, ur.PRIVILEGE_ID, ur.ROLE_ID, pm.PRIVILAGEFOR, pm.MENU_ID, pm.PRIVILEGE_MENU_ID, ur.ACTIVE_FLAG, ur.USER_ID, pm.ADD_FLAG, pm.ACTIVETILLDATE, pm.MODIFY_FLAG, pm.DELETE_FLAG, pm.VIEW_FLAG, ur.ACTIVE_TO_DT, pm.Action, pm.ActiveMenu }).ToList();
            var List1 = result1.GroupBy(p => p.MENU_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (var item1 in List1)
            {
                MID = Convert.ToInt32(item1.MENU_ID);
                if (DB.tempUser1.Where(p => p.MENUID == MID && p.UserID == item1.USER_ID && p.ROLE_ID == Roleid).Count() > 0)
                {
                    var obj1 = DB.tempUser1.Single(p => p.MENUID == MID && p.TenentID == TenentId && p.LocationID == LocationID && p.UserID == item1.USER_ID && p.ROLE_ID == Roleid);
                    obj1.PRIVILAGESOURCE = "1";
                    //obj1.ROLE_ID = item1.ROLE_ID;
                    //obj1.ACTIVETILLDATE = item1.ACTIVETILLDATE;
                    //obj1.MENUDATE = item1.ACTIVE_TO_DT;
                    obj1.ACTIVEROLE = item1.Action == "Y" ? true : false; //item1.ACTIVE_FLAG == "Y" ? true : false;
                    if (obj1.MENU_LOCATION == "Separator")
                        obj1.ACTIVEMENU = true;
                    else
                    {
                        string MM = item1.ActiveMenu;
                        obj1.ACTIVEMENU = item1.ActiveMenu == "Y" ? true : false;
                    }
                    DB.SaveChanges();
                }
            }

            //for USER_RIGHT
            var result3 = (from pm in DB.PRIVILAGE_MENUDemon.Where(a => a.LOCATION_ID == LocationID && a.TenentID == TenentId && a.PRIVILAGEFOR == 3 && a.PRIVILEGE_MENU_ID == UserID)
                           join
                              URR in DB.USER_RIGHTS.Where(b => b.LOCATION_ID == LocationID && b.TenentID == TenentId && b.USER_ID == UserID && b.status == "Final") on pm.PRIVILEGE_ID equals URR.PRIVILEGE_ID

                           select new { URR.TenentID, URR.PRIVILEGE_ID, pm.PRIVILAGEFOR, pm.PRIVILEGE_MENU_ID, pm.MENU_ID, URR.USER_ID, URR.ADD_FLAG, URR.MODIFY_FLAG, pm.ACTIVETILLDATE, URR.DELETE_FLAG, URR.VIEW_FLAG, URR.ALL_FLAG, a = pm.ADD_FLAG, b = pm.MODIFY_FLAG, c = pm.DELETE_FLAG, d = pm.VIEW_FLAG, pm.Action, pm.ActiveModule, pm.SP5 }).ToList();
            var List2 = result3.GroupBy(p => p.MENU_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (var item3 in List2)
            {
                MID = Convert.ToInt32(item3.MENU_ID);
                if (DB.tempUser1.Where(p => p.MENUID == MID && p.UserID == UserID && p.ROLE_ID == Roleid).Count() > 0)
                {
                    var obj2 = DB.tempUser1.Single(p => p.MENUID == MID && p.TenentID == TenentId && p.LocationID == LocationID && p.UserID == UserID && p.ROLE_ID == Roleid);
                    obj2.PRIVILAGESOURCE = "3";
                    obj2.ACTIVEUSER = item3.Action == "Y" ? true : false;
                    if (obj2.MENU_LOCATION == "Separator")
                        obj2.ACTIVETILLDATE = item3.ACTIVETILLDATE;
                    else
                        obj2.ACTIVETILLDATE = item3.ACTIVETILLDATE;
                    obj2.SP5 = item3.SP5 == "Y" ? 1 : 0;
                    //obj2.ACTIVEMODULE = item3.ActiveModule == "Y" ? true : false;
                    //obj2.URADD_FLAG = item3.ADD_FLAG;
                    //obj2.URMODIFY_FLAG = item3.MODIFY_FLAG;
                    //obj2.URDELETE_FLAG = item3.DELETE_FLAG;
                    //obj2.URVIEW_FLAG = item3.VIEW_FLAG;
                    //obj2.URALL_FLAG = item3.ALL_FLAG;
                    DB.SaveChanges();
                }

            }

        }
        //Second tab4 work for user

        protected void drppuseract_SelectedIndexChanged(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(drppuseract.SelectedValue);
            if (user != 0)
            {
                pnlADT_user.Visible = true;
                bindUserList(user);
                Database.USER_MST obj = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == user);
                txtActivefromdate.Text = Convert.ToDateTime(obj.USERDATE).ToShortDateString();
                txtActiveTilldate.Text = Convert.ToDateTime(obj.Till_DT).ToShortDateString();
                if (obj.USERDATE.Value <= DateTime.Now.Date && obj.Till_DT.Value >= DateTime.Now.Date)
                {
                    chkuserAD.Checked = true;
                }
                else
                {
                    chkuserAD.Checked = false;
                }
            }
            else
            {
                pnlADT_user.Visible = false;
            }
        }
        public void bindUserList(int user)
        {
            List<Database.PRIVILAGE_MENUDemon> Finel = new List<Database.PRIVILAGE_MENUDemon>();
            List<Database.USER_RIGHTS> userList = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == user).ToList();
            foreach (Database.USER_RIGHTS itemss in userList)
            {
                int privilage = Convert.ToInt32(itemss.PRIVILEGE_ID);
                if(DB.MODULE_MAP.Where(p=>p.TenentID == TID && p.UserID == user && p.PRIVILEGE_ID == privilage && p.ACTIVE_FLAG == "Y").Count() > 0)
                {
                    List<Database.PRIVILAGE_MENUDemon> Temp = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == user && p.PRIVILEGE_ID == privilage && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 3).ToList();
                    foreach (Database.PRIVILAGE_MENUDemon Tempitem in Temp)
                    {
                        Database.PRIVILAGE_MENUDemon obj = DB.PRIVILAGE_MENUDemon.Single(p => p.MENU_ID == Tempitem.MENU_ID && p.PRIVILEGE_MENU_ID == Tempitem.PRIVILEGE_MENU_ID && p.PRIVILEGE_ID == Tempitem.PRIVILEGE_ID && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 3);
                        Finel.Add(obj);
                    }
                }               
            }
            UserListNew.DataSource = Finel.Where(p => p.MENU_LOCATION == "Separator");
            UserListNew.DataBind();
        }
        protected void btnsubmituser_Click(object sender, EventArgs e)
        {
            int PRIVILEGE_ID = 0;
            int PRIVILEGE_MENUID = 0;

            PRIVILEGE_MENUID = Convert.ToInt32(drppuseract.SelectedValue);

            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
            for (int i = 0; i < UserListNew.Items.Count; i++)
            {
                ListView subList = new ListView();
                int MasterID = 0;

                subList = (ListView)UserListNew.Items[i].FindControl("userListLeftNew");
                Label lblprivilageidN = (Label)UserListNew.Items[i].FindControl("lblprivilageidN");
                Label lbluserSeparateMENU_IDN = (Label)UserListNew.Items[i].FindControl("lbluserSeparateMENU_IDN");
                int mid = Convert.ToInt32(lbluserSeparateMENU_IDN.Text);
                int priidd = Convert.ToInt32(lblprivilageidN.Text);
                PRIVILAGE_MENUDemon Menupri = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == priidd && p.TenentID == TID && p.PRIVILAGEFOR == 3 && p.MENU_ID == mid).First();
                Menupri.ACTIVETILLDATE = Convert.ToDateTime(txtActiveTilldate.Text);
                DB.SaveChanges();
                for (int j = 0; j < subList.Items.Count; j++)
                {

                    int MenuID = 0;
                    CheckBox chadd = new CheckBox();
                    CheckBox chedit = new CheckBox();
                    CheckBox chdelete = new CheckBox();
                    CheckBox chprint = new CheckBox();
                    CheckBox chLabel = new CheckBox();
                    CheckBox chAdmin = new CheckBox();
                    CheckBox chrolesp1 = new CheckBox();
                    CheckBox chrolesp2 = new CheckBox();
                    CheckBox chrolesp3 = new CheckBox();
                    CheckBox chrolesp4 = new CheckBox();
                    CheckBox chrolesp5 = new CheckBox();
                    CheckBox Menu = new CheckBox();

                    Label lblpriid = (Label)subList.Items[j].FindControl("lblpriidN");
                    PRIVILEGE_ID = Convert.ToInt32(lblpriid.Text);
                    MenuID = Convert.ToInt32(((Label)subList.Items[j].FindControl("lbluserListLeftMENU_IDN")).Text);
                    chadd = (CheckBox)subList.Items[j].FindControl("chuseraddN");
                    chedit = (CheckBox)subList.Items[j].FindControl("chusereditN");
                    chdelete = (CheckBox)subList.Items[j].FindControl("chuserdeleteN");
                    chprint = (CheckBox)subList.Items[j].FindControl("chuserprintN");
                    chLabel = (CheckBox)subList.Items[j].FindControl("chuserLabelN");
                    chAdmin = (CheckBox)subList.Items[j].FindControl("chuserAdminN");
                    chrolesp1 = (CheckBox)subList.Items[j].FindControl("chusersp1N");
                    chrolesp2 = (CheckBox)subList.Items[j].FindControl("chusersp2N");
                    chrolesp3 = (CheckBox)subList.Items[j].FindControl("chusersp3N");
                    chrolesp4 = (CheckBox)subList.Items[j].FindControl("chusersp4N");
                    chrolesp5 = (CheckBox)subList.Items[j].FindControl("chusersp5N");
                    Menu = (CheckBox)subList.Items[j].FindControl("CHuserMenuN");

                    objPRIVILAGE_MENU = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == 3 && p.MENU_ID == MenuID).First();

                    objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                    objPRIVILAGE_MENU.MySerial = j;

                    objPRIVILAGE_MENU.ALL_FLAG = chAdmin.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ADD_FLAG = chadd.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.MODIFY_FLAG = chedit.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.DELETE_FLAG = chdelete.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.VIEW_FLAG = chprint.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.LABEL_FLAG = chLabel.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP1 = chrolesp1.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP2 = chrolesp2.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP3 = chrolesp3.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP4 = chrolesp4.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.SP5 = chrolesp5.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ActiveMenu = Menu.Checked == true ? "Y" : "N";
                    objPRIVILAGE_MENU.ACTIVETILLDATE = Convert.ToDateTime(txtActiveTilldate.Text);
                    objPRIVILAGE_MENU.LastUpdate = DateTime.Now;
                    DB.SaveChanges();
                }
            }
            Database.USER_MST objUser = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == PRIVILEGE_MENUID);
            objUser.USERDATE = Convert.ToDateTime(txtActivefromdate.Text);
            objUser.Till_DT = Convert.ToDateTime(txtActiveTilldate.Text);
            DB.SaveChanges();
            btnGenerateUserTemp.Visible = true;
        }
        protected void btnGenerateUserTemp_Click(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(drppuseract.SelectedValue);
            var ListTempUser = DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == user).ToList();
            if (ListTempUser.Count > 0)
            {
                foreach (Database.tempUser1 item in ListTempUser)
                {
                    DB.tempUser1.DeleteObject(item);
                    DB.SaveChanges();
                }
            }

            var serRigheList = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == user && p.status == "Final").ToList();
            foreach (Database.USER_RIGHTS item in serRigheList)
            {
                int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                var moduleid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == user && p.PRIVILEGE_ID == PrivilageID);
                int Modulidd = moduleid.MODULE_ID;

                int RID = DB.USER_ROLE.Where(p => p.USER_ID == user && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y").Count() > 0 ? DB.USER_ROLE.Where(p => p.USER_ID == user && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y").First().ROLE_ID : 0; //Convert.ToInt32(item.ROLE_ID);
                if (RID != 0)
                    getUserMenuGloble(TID, user, 1, Modulidd, RID);
                else
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "You have No Any Role", "Error!", Classes.Toastr.ToastPosition.TopCenter);
            }
            btnGenerateUserTemp.Visible = false;
        }
        protected void UserListNew_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lbluserSeparateMENU_IDN");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView userListLeftNew = (ListView)e.Item.FindControl("userListLeftNew");
                Label lblprivilagemenuid = (Label)e.Item.FindControl("lblprivilagemenuidN");
                Label lblprivilageid = (Label)e.Item.FindControl("lblprivilageidN");
                int Pmenuid = Convert.ToInt32(lblprivilagemenuid.Text);
                int priid = Convert.ToInt32(lblprivilageid.Text);

                //List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == userM && p.PRIVILEGE_ID == privilageM && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == Pmenuid && p.PRIVILEGE_ID == priid && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                userListLeftNew.DataSource = List;
                userListLeftNew.DataBind();

            }
        }
        protected void btnedituser_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ACM/Web_User_Mst.aspx?TID="+TID);
        }
        //Fourth tab3 work for Module
        protected void drpuserMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindmoduleuser();
            bindusermodulelist();
        }
        public void bindmoduleuser()
        {
            int user = Convert.ToInt32(drpuserMod.SelectedValue);
            List<Database.MODULE_MST> ModulActive = new List<Database.MODULE_MST>();
            List<Database.MODULE_MST> ModulDeActive = new List<Database.MODULE_MST>();
            List<Database.MODULE_MAP> ListModuleR = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == user).GroupBy(p => p.MODULE_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.MODULE_MAP item in ListModuleR)
            {
                string AD = item.ACTIVE_FLAG;
                if (DB.MODULE_MST.Where(p => p.Module_Id == item.MODULE_ID).Count() > 0)
                {
                    if (AD == "Y")
                    {
                        Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == item.MODULE_ID);
                        ModulActive.Add(obj);
                    }
                    else
                    {
                        Database.MODULE_MST obj1 = DB.MODULE_MST.Single(p => p.Module_Id == item.MODULE_ID);
                        ModulDeActive.Add(obj1);
                    }
                }

            }
            if (ModulActive.Count() > 0)
            {
                DrpModuleActive.DataSource = ModulActive;
                DrpModuleActive.DataTextField = "Module_Name";
                DrpModuleActive.DataValueField = "Module_Id";
                DrpModuleActive.DataBind();
                DrpModuleActive.Items.Insert(0, new ListItem("-- select --", "0"));

            }
            else
            {
                DrpModuleActive.Items.Clear();
                DrpModuleActive.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            if (ModulDeActive.Count() > 0)
            {
                drpModuleDeactive.DataSource = ModulDeActive;
                drpModuleDeactive.DataTextField = "Module_Name";
                drpModuleDeactive.DataValueField = "Module_Id";
                drpModuleDeactive.DataBind();
                drpModuleDeactive.Items.Insert(0, new ListItem("-- select --", "0"));
            }
            else
            {
                drpModuleDeactive.Items.Clear();
                drpModuleDeactive.Items.Insert(0, new ListItem("-- select --", "0"));
            }
        }

        protected void btnDeactiveM_Click(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(drpuserMod.SelectedValue);
            int ActiveModule = Convert.ToInt32(DrpModuleActive.SelectedValue);
            if (DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == user && p.MODULE_ID == ActiveModule && p.ACTIVE_FLAG == "Y").Count() > 0)
            {
                Database.MODULE_MAP obj = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == user && p.MODULE_ID == ActiveModule && p.ACTIVE_FLAG == "Y");
                obj.ACTIVE_FLAG = "N";
                DB.SaveChanges();
                btnGenerateModuleTemp.Visible = true;
            }
            bindmoduleuser();
            bindusermodulelist();
        }

        protected void btnActiveM_Click(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(drpuserMod.SelectedValue);
            int DeactiveModule = Convert.ToInt32(drpModuleDeactive.SelectedValue);
            if (DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == user && p.MODULE_ID == DeactiveModule && p.ACTIVE_FLAG == "N").Count() > 0)
            {
                Database.MODULE_MAP obj = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == user && p.MODULE_ID == DeactiveModule && p.ACTIVE_FLAG == "N");
                obj.ACTIVE_FLAG = "Y";
                DB.SaveChanges();
                btnGenerateModuleTemp.Visible = true;
            }
            bindmoduleuser();
            bindusermodulelist();
        }
        public void bindusermodulelist()
        {
            int user = Convert.ToInt32(drpuserMod.SelectedValue);
            int ActiveModule = Convert.ToInt32(DrpModuleActive.SelectedValue);
            List<Database.PRIVILAGE_MENUDemon> Finel = new List<Database.PRIVILAGE_MENUDemon>();
            List<Database.MODULE_MAP> ModuleuserList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == user && p.ACTIVE_FLAG == "Y").ToList();
            foreach (Database.MODULE_MAP itemss in ModuleuserList)
            {
                int privilage = Convert.ToInt32(itemss.PRIVILEGE_ID);
                List<Database.PRIVILAGE_MENUDemon> Temp = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == user && p.PRIVILEGE_ID == privilage && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 3).ToList();
                foreach (Database.PRIVILAGE_MENUDemon Tempitem in Temp)
                {
                    Database.PRIVILAGE_MENUDemon obj = DB.PRIVILAGE_MENUDemon.Single(p => p.MENU_ID == Tempitem.MENU_ID && p.PRIVILEGE_MENU_ID == Tempitem.PRIVILEGE_MENU_ID && p.PRIVILEGE_ID == Tempitem.PRIVILEGE_ID && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 3);
                    Finel.Add(obj);
                }
            }
            sysListSeparate.DataSource = Finel.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator");
            sysListSeparate.DataBind();
        }
        protected void sysListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lblsysSeparateMENU_ID");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView sysListLeft = (ListView)e.Item.FindControl("sysListLeft");
                Label lblprivilagemenuidM = (Label)e.Item.FindControl("lblprivilagemenuidM");
                Label lblprivilageidM = (Label)e.Item.FindControl("lblprivilageidM");
                int PrivilageMenuID = Convert.ToInt32(lblprivilagemenuidM.Text);
                int privilageID = Convert.ToInt32(lblprivilageidM.Text);

                List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PrivilageMenuID && p.PRIVILEGE_ID == privilageID && p.PRIVILAGEFOR == 3 && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
                sysListLeft.DataSource = List;
                sysListLeft.DataBind();
            }

        }

        protected void btnGenerateModuleTemp_Click(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(drpuserMod.SelectedValue);
            var ListTempUser = DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == user).ToList();
            if (ListTempUser.Count > 0)
            {
                foreach (Database.tempUser1 item in ListTempUser)
                {
                    DB.tempUser1.DeleteObject(item);
                    DB.SaveChanges();
                }
            }

            var ModuleUserList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == user && p.ACTIVE_FLAG == "Y").ToList();
            foreach (Database.MODULE_MAP item in ModuleUserList)
            {
                int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                var moduleid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == user && p.PRIVILEGE_ID == PrivilageID);
                int Modulidd = moduleid.MODULE_ID;

                int RID = DB.USER_ROLE.Where(p => p.USER_ID == user && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y").Count() > 0 ? DB.USER_ROLE.Where(p => p.USER_ID == user && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y").First().ROLE_ID : 0; //Convert.ToInt32(item.ROLE_ID);
                if (RID != 0)
                    getUserMenuGloble(TID, user, 1, Modulidd, RID);
                else
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "You have No Any Role", "Error!", Classes.Toastr.ToastPosition.TopCenter);

            }
            btnGenerateModuleTemp.Visible = false;
        }




















    }
}