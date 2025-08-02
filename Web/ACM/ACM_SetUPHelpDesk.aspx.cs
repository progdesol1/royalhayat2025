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
using System.Net;
using System.IO;


namespace Web.ACM
{
    public partial class ACM_SetUPHelpDesk : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindTenent();
            }
        }
        public void binddrop()
        {
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int TID = Convert.ToInt32(drpTenet.SelectedValue);

            //drproleMST.DataSource = DB.ROLE_MST.Where(p => p.TenentID == TID).OrderBy(a => a.ROLE_NAME);
            //drproleMST.DataTextField = "ROLE_NAME";
            //drproleMST.DataValueField = "ROLE_ID";
            //drproleMST.DataBind();
            //drproleMST.Items.Insert(0, new ListItem("-- select Role--", "0"));
            List<Database.ROLE_MST> FinalRole = new List<Database.ROLE_MST>();
            List<Database.USER_ROLE> RoleList = DB.USER_ROLE.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y").GroupBy(p => p.ROLE_ID).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.USER_ROLE item in RoleList)
            {
                if (DB.ROLE_MST.Where(p => p.ROLE_ID == item.ROLE_ID && p.TenentID == TID).Count() > 0)
                {
                    Database.ROLE_MST obj = DB.ROLE_MST.Single(p => p.ROLE_ID == item.ROLE_ID && p.TenentID == TID);
                    FinalRole.Add(obj);
                }
            }
            //drproleMST.DataSource = FinalRole.OrderBy(a => a.ROLE_NAME);
            //drproleMST.DataTextField = "ROLE_NAME";
            //drproleMST.DataValueField = "ROLE_ID";
            //drproleMST.DataBind();
            //drproleMST.Items.Insert(0, new ListItem("-- select Role--", "0"));

            drpUserMST.DataSource = DB.USER_MST.Where(p => p.TenentID == TID).OrderBy(a => a.FIRST_NAME); ;
            drpUserMST.DataTextField = "FIRST_NAME";
            drpUserMST.DataValueField = "USER_ID";
            drpUserMST.DataBind();
            drpUserMST.Items.Insert(0, new ListItem("-- select User--", "0"));

        }
        protected void btnSaveRole_Click1(object sender, EventArgs e)
        {
            //updatePM(RoleListSeparate, 1);
            //int TID = Convert.ToInt32(drpTenet.SelectedValue);
            //int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            //var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID);
            //int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID);
            //int RoleID = Convert.ToInt32(drproleMST.SelectedValue);
            //PRIVILAGE_MENUn objPRIVILAGE_MENU = new PRIVILAGE_MENUn();
            //for (int i = 0; i < RoleListSeparate.Items.Count; i++)
            //{
            //    //CheckBox cheList = (CheckBox)RoleListSeparate.Items[i].FindControl("cheList");
            //    //Label lblMENU_NAME1 = (Label)RoleListSeparate.Items[i].FindControl("lblMENU_NAME1");
            //    Label lblSeparateMENU_ID = (Label)RoleListSeparate.Items[i].FindControl("lblroleSeparateMENU_ID");

            //    ListView RoleListLeft = (ListView)RoleListSeparate.Items[i].FindControl("RoleListLeft");


            //    for (int j = 0; j < RoleListLeft.Items.Count; j++)
            //    {
            //        //Label lblrleleftMENU_NAME1 = (Label)RoleListLeft.Items[j].FindControl("lblrleleftMENU_NAME1");
            //        Label lblRoleListLeftMENU_ID = (Label)RoleListLeft.Items[j].FindControl("lblRoleListLeftMENU_ID");
            //        int MenuID = Convert.ToInt32(lblRoleListLeftMENU_ID.Text);
            //        CheckBox chroleadd = (CheckBox)RoleListLeft.Items[j].FindControl("chroleadd");
            //        CheckBox chroleedit = (CheckBox)RoleListLeft.Items[j].FindControl("chroleedit");
            //        CheckBox chroledelete = (CheckBox)RoleListLeft.Items[j].FindControl("chroledelete");
            //        CheckBox chroleprint = (CheckBox)RoleListLeft.Items[j].FindControl("chroleprint");
            //        CheckBox chroleLabel = (CheckBox)RoleListLeft.Items[j].FindControl("chroleLabel");
            //        CheckBox chroleAdmin = (CheckBox)RoleListLeft.Items[j].FindControl("chroleAdmin");

            //        objPRIVILAGE_MENU = DB.PRIVILAGE_MENUn.Where(p => p.PRIVILEGE_MENU_ID == RoleID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == 1 && p.MENU_ID == MenuID).First();

            //        objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
            //        objPRIVILAGE_MENU.MySerial = j;
            //        if (chroleAdmin.Checked == true)
            //        {
            //            objPRIVILAGE_MENU.ALL_FLAG = "Y";
            //        }
            //        if (chroleadd.Checked == true)
            //        {
            //            objPRIVILAGE_MENU.ADD_FLAG = "Y";
            //        }
            //        if (chroleedit.Checked == true)
            //        {
            //            objPRIVILAGE_MENU.MODIFY_FLAG = "Y";
            //        }
            //        if (chroledelete.Checked == true)
            //        {
            //            objPRIVILAGE_MENU.DELETE_FLAG = "Y";
            //        }
            //        if (chroleprint.Checked == true)
            //        {
            //            objPRIVILAGE_MENU.VIEW_FLAG = "Y";
            //        }
            //        if (chroleLabel.Checked == true)
            //        {
            //            objPRIVILAGE_MENU.LABEL_FLAG = "Y";
            //        }
            //        objPRIVILAGE_MENU.LastUpdate = DateTime.Now;
            //        DB.SaveChanges();
            //    }



            //}
        }
        protected void btnSavesys_Click1(object sender, EventArgs e)
        {
            //updatePM(sysListSeparate, 2);

        }
        protected void btnSaveuser_Click1(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            Database.tblHelpDeskUserSet objset = new Database.tblHelpDeskUserSet();
            objset.TenentID = TID;
            int uid = Convert.ToInt32(drpUserMST.SelectedValue);
            if (DB.tblHelpDeskUserSets.Where(p => p.TenentID == TID && p.USER_ID == uid).Count() <= 0)
            {
                objset.USER_ID = uid;
            }
            else
            {
                lblmsg.Text = "Already Set for this user";
            }
            objset.Location_ID = 1;
            if(chkreopen.Checked == true)
            {
                objset.reopen = true;
            }
            else
            {
                objset.reopen = false;
            }
            if(chknewtickt.Checked == true)
            {
                objset.newticket = true;
            }
            else
            {
                objset.newticket = false;
            }
            if (chkdlttkt.Checked == true)
            {
                objset.deleteticket = true;
            }
            else
            {
                objset.deleteticket = false;
            }
            if (chkprint.Checked == true)
            {
                objset.AllowPrint = "Y";
            }
            else
            {
                objset.AllowPrint = "N";
            }
            DB.tblHelpDeskUserSets.AddObject(objset);
            DB.SaveChanges();
            lblmsh.Text = "Data Save Successfully";
            pnlSuccessMsg.Visible = true;
            chkreopen.Checked = false;
            chknewtickt.Checked = false;
            chkdlttkt.Checked = false;
            chkprint.Checked = false;
        }
        public string GetMenuname(int MenuID)
        {
            if (DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuID).Count() > 0)
                return DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuID).First().MENU_NAME1;
            else
                return "Not Found";

        }
        public void bindsys(int PrivilageFor, ListView CommonListview)
        {

            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID);
            int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID);
            List<Database.FUNCTION_MST> List = new List<FUNCTION_MST>();
            List<Database.PRIVILAGE_MENUDemon> FinalList = new List<PRIVILAGE_MENUDemon>();
            //lblforSystem.Text = DB.MODULE_MST.Where(p => p.Module_Id == MODULE_ID).First().Module_Name;
            int PRIVILEGE_MENU_ID = 0;
            if (PrivilageFor == 1)
                PRIVILEGE_MENU_ID = MODULE_ID;
            else if (PrivilageFor == 2)
                PRIVILEGE_MENU_ID = MODULE_ID;
            else if (PrivilageFor == 3)
                PRIVILEGE_MENU_ID = Convert.ToInt32(drpUserMST.SelectedValue);
        }
        public void bindSubsys(int PrivilageFor, ListView CommonSubListview, int MasterID)
        {
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID);
            int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID);
            int PRIVILEGE_MENUID = 0;
            if (PrivilageFor == 1)
            {
                //PRIVILEGE_MENUID = Convert.ToInt32(drproleMST.SelectedValue);
            }
            if (PrivilageFor == 2)
            {
                PRIVILEGE_MENUID = MODULE_ID;
            }
            if (PrivilageFor == 3)
            {
                PRIVILEGE_MENUID = Convert.ToInt32(drpUserMST.SelectedValue);
            }
            int TID = Convert.ToInt32(drpTenet.SelectedValue);


            List<Database.PRIVILAGE_MENUDemon> List = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.PRIVILAGEFOR == PrivilageFor && p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu").ToList();
            List<Database.PRIVILAGE_MENUDemon> PR = new List<Database.PRIVILAGE_MENUDemon>();
            foreach (Database.PRIVILAGE_MENUDemon itemss in List)
            {
                int MS = itemss.MENU_ID;
                if (DB.PRIVILAGE_MENUDemon.Where(p => p.MENU_LOCATION == "Left Menu" && p.MASTER_ID != 0 && p.MASTER_ID == MS).Count() > 0)
                {
                    PR = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.PRIVILAGEFOR == PrivilageFor && p.MASTER_ID == MS).ToList();
                }
            }
            foreach (Database.PRIVILAGE_MENUDemon SPR in PR)
            {
                Database.PRIVILAGE_MENUDemon objm = PR.Single(p => p.TenentID == TID && p.PRIVILEGE_MENU_ID == SPR.PRIVILEGE_MENU_ID && p.MENU_ID == SPR.MENU_ID && p.MASTER_ID == SPR.MASTER_ID && p.MENU_LOCATION == "Left Menu");
                List.Add(objm);
            }
            CommonSubListview.DataSource = List;
            CommonSubListview.DataBind();
            int Count = List.Count();
            int FinalCount = ViewState["rowscount"] != null ? Convert.ToInt32(ViewState["rowscount"]) : 0;
            FinalCount += Count;
            ViewState["rowscount"] = FinalCount;
            List<Database.PRIVILAGE_MENUDemon> PRIList = new List<Database.PRIVILAGE_MENUDemon>();
            if (PrivilageFor == 1)
            {
                PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILAGEFOR == PrivilageFor && p.TenentID == TID && p.Action == "Y").ToList();
                if (PRIList.Count() > 0)
                {
                    //CHKRoleActive.Checked = true;
                }
            }
            if (PrivilageFor == 2)
            {
                PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.PRIVILAGEFOR == PrivilageFor && p.TenentID == TID && p.ActiveModule == "Y").ToList();
                if (PRIList.Count() > 0)
                {
                    //CHKModule.Checked = true;
                }
            }
            if (PrivilageFor == 3)
            {
                PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILAGEFOR == PrivilageFor && p.TenentID == TID && p.Action == "Y").ToList();
                List<Database.PRIVILAGE_MENUDemon> userwiseModule = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.ActiveModule == "Y").ToList();
                //
                //if (PRIList.Count() > 0)
                //{
                //    CHKUserActive.Checked = true;
                //}
                //if (userwiseModule.Count() > 0)
                //{
                //    cheusewiseModule.Checked = true;
                //}
            }
        }

        public void updatePM(ListView mainList, int PrivilageFor)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID);
            int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID);
            //int RoleID = Convert.ToInt32(drproleMST.SelectedValue);
            int PRIVILEGE_MENUID = 0;
            if (PrivilageFor == 1)
            {
                //PRIVILEGE_MENUID = Convert.ToInt32(drproleMST.SelectedValue);
            }
            if (PrivilageFor == 2)
            {
                PRIVILEGE_MENUID = MODULE_ID;
            }
            if (PrivilageFor == 3)
            {
                PRIVILEGE_MENUID = Convert.ToInt32(drpUserMST.SelectedValue);
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

                        int UID = Convert.ToInt32(drpUserMST.SelectedValue);
                        var objURight = DB.USER_RIGHTS.Where(p => p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.USER_ID == UID).First();
                        if (chadd.Checked == true)
                            objURight.ALL_FLAG = true;
                        if (chadd.Checked == true)
                            objURight.ADD_FLAG = true;
                        if (chedit.Checked == true)
                            objURight.MODIFY_FLAG = true;
                        if (chdelete.Checked == true)
                            objURight.DELETE_FLAG = true;
                        if (chprint.Checked == true)
                            objURight.VIEW_FLAG = true;
                        if (chrolesp1.Checked == true)
                            objURight.SP1 = true;
                        if (chrolesp2.Checked == true)
                            objURight.SP2 = true;
                        if (chrolesp3.Checked == true)
                            objURight.SP3 = true;
                        if (chrolesp4.Checked == true)
                            objURight.SP4 = true;
                        if (chrolesp5.Checked == true)
                            objURight.SP5 = true;
                        objURight.status = "Final";
                        DB.SaveChanges();
                    }
                    //Label lblrleleftMENU_NAME1 = (Label)RoleListLeft.Items[j].FindControl("lblrleleftMENU_NAME1");




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

                List<Database.PRIVILAGE_MENUDemon> PRIList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor).ToList();
                if (PrivilageFor == 1)
                {
                    //string Activerole = CHKRoleActive.Checked == true ? "Y" : "N";
                    foreach (Database.PRIVILAGE_MENUDemon item in PRIList)
                    {
                        if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID).Count() == 1)
                        {
                            Database.PRIVILAGE_MENUDemon objrole = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID);
                            //objrole.Action = Activerole;
                            DB.SaveChanges();
                        }
                    }
                }
                if (PrivilageFor == 2)
                {
                    PRIList = PRIList.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor).ToList();
                    //string ActiveModule = CHKModule.Checked == true ? "Y" : "N";
                    int SYScount = 0;
                    foreach (Database.PRIVILAGE_MENUDemon item in PRIList)
                    {
                        SYScount = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID).Count();
                        //if (ActiveModule == "Y")
                        //{
                        //    if (SYScount == 1)
                        //    {
                        //        Database.PRIVILAGE_MENUDemon objModule = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID);
                        //        objModule.ActiveModule = ActiveModule;
                        //        DB.SaveChanges();
                        //    }
                        //}
                        //else
                        //{
                        //    if (SYScount == 1)
                        //    {
                        //        Database.PRIVILAGE_MENUDemon objModule = DB.PRIVILAGE_MENUDemon.Single(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == item.MENU_ID);
                        //        objModule.ActiveModule = ActiveModule;
                        //        DB.SaveChanges();
                        //    }
                        //}
                    }
                }
                if (PrivilageFor == 3)
                {


                }
            }
            if (PrivilageFor == 1)
            {
                //pnlErrorMsg.Visible = true;
                //lblerrmsg.Text = "Role Wise Menu Save Successfully...";
            }
            else if (PrivilageFor == 2)
            {
                //PanelSYS.Visible = true;
                //MsgSYS.Text = "System Wise Menu Save Successfully...";
            }
            else if (PrivilageFor == 3)
            {
                PanelUser.Visible = true;
                MsgUser.Text = "User Wise Menu Save Successfully...";
            }
        }

        protected void RoleListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lblroleSeparateMENU_ID");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView RoleListLeft = (ListView)e.Item.FindControl("RoleListLeft");
                bindSubsys(1, RoleListLeft, MasterID);

            }
        }
        protected void userListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                //Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lbluserSeparateMENU_ID");
                //int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                //ListView userListLeft = (ListView)e.Item.FindControl("userListLeft");
                //bindSubsys(3, userListLeft, MasterID);
            }
        }
        protected void sysListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lblsysSeparateMENU_ID");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView sysListLeft = (ListView)e.Item.FindControl("sysListLeft");
                bindSubsys(2, sysListLeft, MasterID);


            }
        }

        protected void drpTenet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
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
            drpPrevilegeMST.DataSource = PRIVILEGEList.OrderBy(a => a.PRIVILEGE_NAME);//DB.PRIVILEGE_MST.Where(p => p.TenentID == TID).OrderBy(a => a.PRIVILEGE_NAME);
            drpPrevilegeMST.DataTextField = "PRIVILEGE_NAME";
            drpPrevilegeMST.DataValueField = "PRIVILEGE_ID";
            drpPrevilegeMST.DataBind();
            drpPrevilegeMST.Items.Insert(0, new ListItem("-- select Previlege --", "0"));
        }
        public void bindTenent()
        {
            var Datas = (from fun1 in DB.MODULE_MAP
                         select new
                         {
                             fun1.TenentID,
                         }
                       ).Distinct();
            drpTenet.DataSource = Datas.Where(p => p.TenentID != 0);
            drpTenet.DataTextField = "TenentID";
            drpTenet.DataValueField = "TenentID";
            drpTenet.DataBind();
            drpTenet.Items.Insert(0, new ListItem("--Select Tenent_ID--", "00"));
        }
        protected void drproleMST_SelectedIndexChanged(object sender, EventArgs e)
        {

            ViewState["rowscount"] = null; //for checkbox using javascript// dipak
            //bindsys(1, RoleListSeparate);
            //lblBusinessRole.Text = "Role for " + drproleMST.SelectedItem;


        }
        protected void drpPrevilegeMST_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["rowscount"] = null;// for checkbox using javascript // dipak
            binddrop();
            //bindsys(2, sysListSeparate);
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);


            var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID);
            int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID);
            string modulefoleHeader = DB.MODULE_MST.Where(p => p.Module_Id == MODULE_ID).First().Module_Name;
            //lblBusinessSystem.Text = "Sytem For " + modulefoleHeader;
            lblforUser.Text = "User Function " + modulefoleHeader;
            //lblRolewise.Text = "Role Wise Menu For " + modulefoleHeader;

        }
        protected void drpUserMST_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["rowscount"] = null;// for checkbox using javascript // dipak
            int tenent = Convert.ToInt32(drpTenet.SelectedValue);
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            int userID = Convert.ToInt32(drpUserMST.SelectedValue);

            lblBusinessUser.Text = "User For " + drpUserMST.SelectedItem;
            lblreopen.Visible = true;
            lblnewtkt.Visible = true;
            lbldlttkt.Visible = true;
            lblptinr.Visible = true;
            chkreopen.Visible = true;
            chknewtickt.Visible = true;
            chkdlttkt.Visible = true;
            chkprint.Visible = true;
        }



        protected void btnFinish_Click(object sender, EventArgs e)
        {

        }

    }
}