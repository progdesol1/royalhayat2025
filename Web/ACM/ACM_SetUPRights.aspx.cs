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
using Classes;


namespace Web.ACM
{
    public partial class ACM_SetUPRights : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var Datas = (from fun1 in DB.TBLLOCATIONs
                //             select new
                //             {
                //                 fun1.TenentID,
                //             }
                //        ).Distinct();
                //drpTenet.DataSource = Datas;
                //drpTenet.DataTextField = "TenentID";
                //drpTenet.DataValueField = "TenentID";
                //drpTenet.DataBind();
                //drpTenet.Items.Insert(0, new ListItem("--Select Tenent_ID--", "00"));
                bindTenent();
                drpUserMST.Items.Insert(0, new ListItem("-- select User--", "0"));
                drpPrevilegeMST.Items.Insert(0, new ListItem("-- select Previlege --", "0"));

            }
        }
        public void bindTenent()
        {
            var Datas = (from fun1 in DB.MODULE_MAP
                         select new
                         {
                             fun1.TenentID,
                         }
                       ).Distinct();
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.MODULE_MAP> Mlist = DB.MODULE_MAP.Where(p => p.TenentID == TID).GroupBy(p => p.TenentID).Select(p => p.FirstOrDefault()).ToList();
            drpTenet.DataSource = Mlist;//Datas.Where(p => p.TenentID != 0);
            drpTenet.DataTextField = "TenentID";
            drpTenet.DataValueField = "TenentID";
            drpTenet.DataBind();
            drpTenet.Items.Insert(0, new ListItem("--Select Tenent_ID--", "00"));

        }
        public void binddrop()
        {
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            drpUserMST.Items.Clear();
            drpUserMST.DataSource = DB.USER_MST.Where(p => p.TenentID == TID).OrderBy(a => a.FIRST_NAME); ;
            drpUserMST.DataTextField = "LOGIN_ID";
            drpUserMST.DataValueField = "USER_ID";
            drpUserMST.DataBind();
            drpUserMST.Items.Insert(0, new ListItem("-- select User--", "0"));
        }
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            //for (int j = 0; j < RoleListSeparate.Items.Count; j++)
            //{

            //    int RID = Convert.ToInt32(((Label)RoleListSeparate.Items[j].FindControl("lblRIGHTS_ID")).Text);
            //    int TID = Convert.ToInt32(((Label)RoleListSeparate.Items[j].FindControl("lblTID")).Text);
            //    int UID = Convert.ToInt32(((Label)RoleListSeparate.Items[j].FindControl("lblUID")).Text);
            //    int PID = Convert.ToInt32(((Label)RoleListSeparate.Items[j].FindControl("lblrightPRIVILEGE_ID")).Text);
            //    CheckBox chadd = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightadd");
            //    CheckBox chedit = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightedit");
            //    CheckBox chdelete = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightdelete");
            //    CheckBox chprint = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightprint");
            //    //CheckBox chLabel = (CheckBox)RoleListSeparate.Items[j].FindControl("chroleLabel");
            //    CheckBox chAdmin = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightAdmin");
            //    CheckBox chrightSP1 = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightSP1");
            //    CheckBox chrightSP2 = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightSP2");
            //    CheckBox chrightSP3 = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightSP3");
            //    CheckBox chrightSP4 = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightSP4");
            //    CheckBox chrightSP5 = (CheckBox)RoleListSeparate.Items[j].FindControl("chrightSP5");
            //    var objURight = DB.USER_RIGHTS.Where(p => p.PRIVILEGE_ID == PID && p.TenentID == TID && p.USER_ID == UID && p.RIGHTS_ID == RID).First();
            //    if (chAdmin.Checked == true)
            //    {
            //        objURight.ALL_FLAG = true;
            //    }
            //    if (chadd.Checked == true)
            //    {
            //        objURight.ADD_FLAG = true;
            //    }
            //    if (chedit.Checked == true)
            //    {
            //        objURight.MODIFY_FLAG = true;
            //    }
            //    if (chdelete.Checked == true)
            //    {
            //        objURight.DELETE_FLAG = true;
            //    }
            //    if (chprint.Checked == true)
            //    {
            //        objURight.VIEW_FLAG = true;
            //    }
            //    if (chrightSP1.Checked == true)
            //    {
            //        objURight.SP1 = true;
            //    }
            //    if (chrightSP2.Checked == true)
            //    {
            //        objURight.SP2 = true;
            //    }
            //    if (chrightSP3.Checked == true)
            //    {
            //        objURight.SP3 = true;
            //    }
            //    if (chrightSP4.Checked == true)
            //    {
            //        objURight.SP4 = true;
            //    }
            //    if (chrightSP5.Checked == true)
            //    {
            //        objURight.SP5 = true;
            //    }              
            //    objURight.status = "Final";
            //    DB.SaveChanges();
            //}
        }
        protected void userListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lbluserSeparateMENU_ID");
                int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                ListView userListLeft = (ListView)e.Item.FindControl("userListLeft");
                bindSubsys("3", userListLeft, MasterID);
            }
        }
        public void bindSubsys(string PrivilageFor, ListView CommonSubListview, int MasterID)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int UID = Convert.ToInt32(drpUserMST.SelectedValue);
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            //var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID); //comment by dipak
            //int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID); //comment by dipak
            int MODULE_ID = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UID && p.PRIVILEGE_ID == PRIVILEGE_ID).MODULE_ID;

            List<Database.tempUser1> List = DB.tempUser1.Where(p => p.PRIVILAGEID == PRIVILEGE_ID && p.PRIVILAGESOURCE == PrivilageFor && p.TenentID == TID && p.ACTIVEMENU == true && p.MASTER_ID == MasterID && p.MENU_LOCATION == "Left Menu" && p.UserID == UID).ToList();
            CommonSubListview.DataSource = List;
            CommonSubListview.DataBind();
        }

        public string GetMenuname(int MenuID)
        {
            if (DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuID).Count() > 0)
                return DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuID).First().MENU_NAME1;
            else
                return "Not Found";
        }
        public string GetPrivilagename(int PRIVILEGE_ID)
        {
            if (DB.PRIVILEGE_MST.Where(p => p.PRIVILEGE_ID == PRIVILEGE_ID).Count() > 0)
                return DB.PRIVILEGE_MST.Where(p => p.PRIVILEGE_ID == PRIVILEGE_ID).First().PRIVILEGE_NAME;
            else
                return "Not Found";
        }



        public void updatePM(ListView mainList, int PrivilageFor)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int UID = Convert.ToInt32(drpUserMST.SelectedValue);
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            //var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID); // comment by dipak
            //int MODULE_ID = Convert.ToInt32(PrivilageID.MODULE_ID); // comment by dipak
            int MODULE_ID = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UID && p.PRIVILEGE_ID == PRIVILEGE_ID).MODULE_ID;
            int PRIVILEGE_MENUID = 0;
            if (PrivilageFor == 1)
            {

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
                    CheckBox chrightSP1 = new CheckBox();
                    CheckBox chrightSP2 = new CheckBox();
                    CheckBox chrightSP3 = new CheckBox();
                    CheckBox chrightSP4 = new CheckBox();
                    CheckBox chrightSP5 = new CheckBox();

                    if (PrivilageFor == 1)
                    {
                        MenuID = Convert.ToInt32(((Label)subList.Items[j].FindControl("lblRoleListLeftMENU_ID")).Text);
                        chadd = (CheckBox)subList.Items[j].FindControl("chroleadd");
                        chedit = (CheckBox)subList.Items[j].FindControl("chroleedit");
                        chdelete = (CheckBox)subList.Items[j].FindControl("chroledelete");
                        chprint = (CheckBox)subList.Items[j].FindControl("chroleprint");
                        chLabel = (CheckBox)subList.Items[j].FindControl("chroleLabel");
                        chAdmin = (CheckBox)subList.Items[j].FindControl("chroleAdmin");
                        chrightSP1 = (CheckBox)subList.Items[j].FindControl("chrightSP1");
                        chrightSP2 = (CheckBox)subList.Items[j].FindControl("chrightSP2");
                        chrightSP3 = (CheckBox)subList.Items[j].FindControl("chrightSP3");
                        chrightSP4 = (CheckBox)subList.Items[j].FindControl("chrightSP4");
                        chrightSP5 = (CheckBox)subList.Items[j].FindControl("chrightSP5");
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
                        chrightSP1 = (CheckBox)subList.Items[j].FindControl("chrightSP1");
                        chrightSP2 = (CheckBox)subList.Items[j].FindControl("chrightSP2");
                        chrightSP3 = (CheckBox)subList.Items[j].FindControl("chrightSP3");
                        chrightSP4 = (CheckBox)subList.Items[j].FindControl("chrightSP4");
                        chrightSP5 = (CheckBox)subList.Items[j].FindControl("chrightSP5");
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
                        chrightSP1 = (CheckBox)subList.Items[j].FindControl("chusersp1");
                        chrightSP2 = (CheckBox)subList.Items[j].FindControl("chusersp2");
                        chrightSP3 = (CheckBox)subList.Items[j].FindControl("chusersp3");
                        chrightSP4 = (CheckBox)subList.Items[j].FindControl("chusersp4");
                        chrightSP5 = (CheckBox)subList.Items[j].FindControl("chusersp5");
                    }
                    //Label lblrleleftMENU_NAME1 = (Label)RoleListLeft.Items[j].FindControl("lblrleleftMENU_NAME1");




                    objPRIVILAGE_MENU = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == PRIVILEGE_MENUID && p.PRIVILEGE_ID == PRIVILEGE_ID && p.TenentID == TID && p.PRIVILAGEFOR == PrivilageFor && p.MENU_ID == MenuID).First();

                    objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                    objPRIVILAGE_MENU.MySerial = j;
                    if (chAdmin.Checked == true)
                    {
                        objPRIVILAGE_MENU.ALL_FLAG = "Y";
                    }
                    if (chadd.Checked == true)
                    {
                        objPRIVILAGE_MENU.ADD_FLAG = "Y";
                    }
                    if (chedit.Checked == true)
                    {
                        objPRIVILAGE_MENU.MODIFY_FLAG = "Y";
                    }
                    if (chdelete.Checked == true)
                    {
                        objPRIVILAGE_MENU.DELETE_FLAG = "Y";
                    }
                    if (chprint.Checked == true)
                    {
                        objPRIVILAGE_MENU.VIEW_FLAG = "Y";
                    }
                    if (chLabel.Checked == true)
                    {
                        objPRIVILAGE_MENU.LABEL_FLAG = "Y";
                    }
                    if (chrightSP1.Checked == true)
                    {
                        objPRIVILAGE_MENU.SP1 = "Y";
                    }
                    if (chrightSP2.Checked == true)
                    {
                        objPRIVILAGE_MENU.SP2 = "Y";
                    }
                    if (chrightSP3.Checked == true)
                    {
                        objPRIVILAGE_MENU.SP3 = "Y";
                    }
                    if (chrightSP4.Checked == true)
                    {
                        objPRIVILAGE_MENU.SP4 = "Y";
                    }
                    if (chrightSP5.Checked == true)
                    {
                        objPRIVILAGE_MENU.SP5 = "Y";
                    }
                    objPRIVILAGE_MENU.LastUpdate = DateTime.Now;
                    DB.SaveChanges();
                }



            }
        }

        protected void RoleListSeparate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                //Label lblSeparateMENU_ID = (Label)e.Item.FindControl("lblroleSeparateMENU_ID");
                //int MasterID = Convert.ToInt32(lblSeparateMENU_ID.Text);
                //ListView RoleListLeft = (ListView)e.Item.FindControl("RoleListLeft");


            }
        }



        protected void drpTenet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            List<Database.USER_MST> ULIST = new List<USER_MST>();
            var itemListPrivilage = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILAGEFOR == 3).Select(a => a.PRIVILEGE_MENU_ID).Distinct().ToList();
            // List<Database.PRIVILAGE_MENUDemon> listdemon = ((List<Database.PRIVILAGE_MENUDemon>)itemListPrivilage).ToList();
            foreach (var item in itemListPrivilage)
            {
                var OBJUserMST = DB.USER_MST.Single(p => p.USER_ID == item && p.TenentID == TID);
                ULIST.Add(OBJUserMST);
            }

            drpUserMST.DataSource = ULIST;
            drpUserMST.DataTextField = "LOGIN_ID";
            drpUserMST.DataValueField = "USER_ID";
            drpUserMST.DataBind();
            drpUserMST.Items.Insert(0, new ListItem("-- select User--", "0"));

        }
        protected void drproleMST_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        protected void drpPrevilegeMST_SelectedIndexChanged(object sender, EventArgs e)
        {
            //binddrop();

            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int UID = Convert.ToInt32(drpUserMST.SelectedValue);
            int PID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            var URList = DB.USER_RIGHTS.Where(p => p.TenentID == TID);


            if (URList.Where(a => a.PRIVILEGE_ID == PID && a.USER_ID == UID).Count() == 0 && PID != 0)
            {
                Database.USER_RIGHTS objAddRights = new Database.USER_RIGHTS();
                objAddRights.TenentID = TID;
                objAddRights.LOCATION_ID = LID;
                objAddRights.USER_ID = UID;
                objAddRights.PRIVILEGE_ID = PID;
                objAddRights.RIGHTS_ID = URList.Count() > 0 ? Convert.ToInt32(URList.Max(p => p.RIGHTS_ID) + 1) : 1;
                objAddRights.ADD_FLAG = false;
                objAddRights.MODIFY_FLAG = false;
                objAddRights.DELETE_FLAG = false;
                objAddRights.VIEW_FLAG = false;
                objAddRights.ALL_FLAG = false;
                objAddRights.SP1 = false;
                objAddRights.SP2 = false;
                objAddRights.SP3 = false;
                objAddRights.SP4 = false;
                objAddRights.SP5 = false;

                objAddRights.status = "Draft";
                DB.USER_RIGHTS.AddObject(objAddRights);
                DB.SaveChanges();

            }
            BindList();
            //lblforSystem.Text = 
            //lblBusinessSystem.Text = "Sytem For " + DB.MODULE_MST.Where(p => p.Module_Id == MODULE_ID).First().Module_Name; 

        }
        protected void drpUserMST_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            List<Database.PRIVILEGE_MST> PLIST = new List<Database.PRIVILEGE_MST>();
            var itemListPrivilage = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID && p.PRIVILAGEFOR == 3).Select(a => a.PRIVILEGE_ID).Distinct().ToList();
            foreach (var item in itemListPrivilage)
            {
                var OBJprivilageMst = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == item && p.TenentID == TID);
                PLIST.Add(OBJprivilageMst);
            }

            drpPrevilegeMST.Items.Clear();
            drpPrevilegeMST.DataSource = PLIST.OrderBy(p => p.PRIVILEGE_NAME);//DB.PRIVILEGE_MST.Where(p => p.TenentID == TID).OrderBy(a => a.PRIVILEGE_NAME);
            drpPrevilegeMST.DataTextField = "PRIVILEGE_NAME";
            drpPrevilegeMST.DataValueField = "PRIVILEGE_ID";
            drpPrevilegeMST.DataBind();
            drpPrevilegeMST.Items.Insert(0, new ListItem("-- select Previlege --", "0"));
            BindList();
            // lblBusinessUser.Text = "User For " + drpUserMST.SelectedItem;


        }



        protected void btnFinish_Click(object sender, EventArgs e)
        {

        }


        protected void BindList()
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int UID = Convert.ToInt32(drpUserMST.SelectedValue);
            int PID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            //if (PID == 0)
            //    RoleListSeparate.DataSource = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.USER_ID == UID && p.status == "Final");
            //else
            //    RoleListSeparate.DataSource = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.USER_ID == UID && p.PRIVILEGE_ID == PID);
            //RoleListSeparate.DataBind();
        }

        protected void btnGenerateTempUser_Click(object sender, EventArgs e)
        {
            //if (Session["MenuACm"] != null)
            //{
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int UID = Convert.ToInt32(drpUserMST.SelectedValue);
            int Privilageid = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            int Modulid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UID && p.PRIVILEGE_ID == Privilageid).MODULE_ID;
            int RID = DB.USER_ROLE.Where(p => p.USER_ID == UID && p.PRIVILEGE_ID == Privilageid && p.ACTIVE_FLAG == "Y").Count() > 0 ? DB.USER_ROLE.Where(p => p.USER_ID == UID && p.PRIVILEGE_ID == Privilageid && p.ACTIVE_FLAG == "Y").First().ROLE_ID : 0;
            GlobleClass.DeleteTempUser(TID, UID, LID, Modulid);
            GlobleClass.getMenuGloble(TID, UID, LID, Modulid,RID);

            //GlobleClass.DeleteTempUser(6, 10330, 1, 2044);
            //GlobleClass.getMenuGloble(6, 10330, 1, 2044);
            bindsys("3", userListSeparate);
            //Session["MenuACm"] = null;
            //}
            pnlErrorMsg.Visible = true;
            lblerrmsg.Text = "Final Temp User Save Successfully...";
        }
        public void bindsys(string PrivilageFor, ListView CommonListview)
        {
            int TID = Convert.ToInt32(drpTenet.SelectedValue);
            int UID = Convert.ToInt32(drpUserMST.SelectedValue);
            //int PRIVILEGE_ID = 5052; //comment by dipak
            //var PrivilageID = DB.PRIVILEGE_MST.OrderBy(p => p.MODULE_ID).FirstOrDefault(p => p.PRIVILEGE_ID == PRIVILEGE_ID);//comment by dipak
            int PRIVILEGE_ID = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
            int MODULE_ID = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UID && p.PRIVILEGE_ID == PRIVILEGE_ID).MODULE_ID;//Convert.ToInt32(PrivilageID.MODULE_ID); //comment by dipak
            List<Database.FUNCTION_MST> List = new List<FUNCTION_MST>();
            List<Database.tempUser1> FinalList = new List<tempUser1>();
            //lblforSystem.Text = DB.MODULE_MST.Where(p => p.Module_Id == MODULE_ID).First().Module_Name;


            List = DB.FUNCTION_MST.Where(p => p.MODULE_ID == MODULE_ID && p.ACTIVE_FLAG == 1).OrderBy(p => p.MENU_ORDER).OrderBy(p => p.MENU_NAME1).ToList();
            FinalList = DB.tempUser1.Where(p => p.PRIVILAGEID == PRIVILEGE_ID && p.TenentID == TID && p.ACTIVEMENU == true && p.PRIVILAGESOURCE == PrivilageFor).ToList();
            if (List.Count() != FinalList.Count())
            {
                foreach (Database.FUNCTION_MST item in List)
                {
                    if (FinalList.Where(p => p.MENUID == item.MENU_ID).Count() == 0)
                    {
                        //tempUser1 objPRIVILAGE_MENU = new tempUser1();                      
                        //objPRIVILAGE_MENU.TenentID = TID;
                        //objPRIVILAGE_MENU.PRIVILAGEID = PRIVILEGE_ID;
                        //objPRIVILAGE_MENU.PRIVILAGESOURCE = PrivilageFor;
                        //objPRIVILAGE_MENU.LocationID = 1;
                        //objPRIVILAGE_MENU.MENUID = item.MENU_ID;
                        //objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                        //objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                        //objPRIVILAGE_MENU.ACTIVETILLDATE = item.ACTIVETILLDATE;
                        //objPRIVILAGE_MENU.ACTIVEMENU = true;
                        //objPRIVILAGE_MENU.ALL_FLAG = "Y";
                        //objPRIVILAGE_MENU.ADD_FLAG = "N";
                        //objPRIVILAGE_MENU.MODIFY_FLAG = "N";
                        //objPRIVILAGE_MENU.DELETE_FLAG = "N";
                        //objPRIVILAGE_MENU.VIEW_FLAG = "N";
                        //objPRIVILAGE_MENU.SP1 = 0;
                        //objPRIVILAGE_MENU.SP2 = 0;
                        //objPRIVILAGE_MENU.SP3 = 0;
                        //objPRIVILAGE_MENU.SP4 = 0;
                        //objPRIVILAGE_MENU.SP5 = 0;

                        //DB.tempUser1.AddObject(objPRIVILAGE_MENU);
                        //DB.SaveChanges();
                    }

                }
                FinalList = DB.tempUser1.Where(p => p.PRIVILAGEID == PRIVILEGE_ID && p.TenentID == TID && p.ACTIVEMENU == true && p.PRIVILAGESOURCE == PrivilageFor).ToList();
            }
            if (FinalList.Count() > 0)
            {
                if (FinalList.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator").Count() > 0)
                {
                    CommonListview.DataSource = FinalList.Where(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator" && p.UserID == UID); // change by dipak
                    CommonListview.DataBind();
                    //int MasterID = Convert.ToInt32(FinalList.First(p => p.ACTIVETILLDATE >= DateTime.Now && p.MENU_LOCATION == "Separator")); // change by dipak
                }
                else
                {
                    CommonListview.DataSource = null;
                    CommonListview.DataBind();
                }

            }
            else
            {
                //string message = "alert('This " + ModielName + " No Menu')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //lbldiscription.Text = "This " + ModielName + " has No Fuction";
                //ModalPopupExtender1.Show();
            }

        }
        protected void RoleListSeparate_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                int ID = Convert.ToInt32(e.CommandArgument);
                int RID = Convert.ToInt32(((Label)e.Item.FindControl("lblRIGHTS_ID")).Text);
                int TID = Convert.ToInt32(((Label)e.Item.FindControl("lblTID")).Text);
                int UID = Convert.ToInt32(((Label)e.Item.FindControl("lblUID")).Text);
                int PID = Convert.ToInt32(((Label)e.Item.FindControl("lblrightPRIVILEGE_ID")).Text);
                Database.USER_RIGHTS objUSER_RIGHTS = DB.USER_RIGHTS.Where(p => p.PRIVILEGE_ID == PID && p.TenentID == TID && p.USER_ID == UID && p.RIGHTS_ID == RID).First();
                objUSER_RIGHTS.status = "Draff";
                DB.SaveChanges();
                BindList();

            }
        }

    }
}