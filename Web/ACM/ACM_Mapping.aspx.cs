using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.ACM
{
    public partial class ACM_Mapping : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGlobal();
            }

        }
        public void BindGlobal()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.USER_MST> UList = DB.USER_MST.Where(p => p.TenentID == TID).GroupBy(p => p.TenentID).Select(p => p.FirstOrDefault()).ToList();
            DrpTenent.DataSource = UList;
            DrpTenent.DataTextField = "TenentID";
            DrpTenent.DataValueField = "TenentID";
            DrpTenent.DataBind();
            DrpTenent.Items.Insert(0, new ListItem("---Select---", "00"));
        }
        protected void DrpTenent_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            DrpLocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenentID == TID);
            DrpLocation.DataTextField = "LOCNAME1";
            DrpLocation.DataValueField = "LOCATIONID";
            DrpLocation.DataBind();
            DrpLocation.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            int LID = Convert.ToInt32(DrpLocation.SelectedValue);
            List<Database.USER_MST> UList = DB.USER_MST.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.USER_TYPE != 4).ToList();
            DrpUSer.DataSource = UList;
            DrpUSer.DataTextField = "LOGIN_ID";
            DrpUSer.DataValueField = "USER_ID";
            DrpUSer.DataBind();
            DrpUSer.Items.Insert(0, new ListItem("---Select---", "0"));

            //DrpRole.DataSource = DB.ROLE_MST.Where(p => p.TenentID == TID);
            //DrpRole.DataTextField = "ROLE_NAME";
            //DrpRole.DataValueField = "ROLE_ID";
            //DrpRole.DataBind();
            //DrpRole.Items.Insert(0, new ListItem("---Select---", "0"));    
        }



        protected void btnprocess_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            int LID = Convert.ToInt32(DrpLocation.SelectedValue);
            int UID = Convert.ToInt32(DrpUSer.SelectedValue);
            if(TID == 15)
            {
                List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p => p.Parent_Module_id != 0 && p.Module_Id == 21).ToList();
                ListModule.DataSource = List;
                ListModule.DataBind();
            }
            else if(TID == 20)
            {
                List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p => p.Parent_Module_id != 0 && p.Module_Id == 21).ToList();
                ListModule.DataSource = List;
                ListModule.DataBind();
            }
            else if (TID == 130)
            {
                List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p => p.Parent_Module_id != 0 && p.Module_Id == 23).ToList();
                ListModule.DataSource = List;
                ListModule.DataBind();
            }

            else
            {
                List<Database.MODULE_MST> List = DB.MODULE_MST.Where(p => p.Parent_Module_id != 0 && p.Module_Id != 12 && p.Module_Id != 13).ToList();
                ListModule.DataSource = List;
                ListModule.DataBind();
            }
            
            List<Database.ROLE_MST> ListRoll = DB.ROLE_MST.Where(p => p.ACTIVE_FLAG == "Y" && p.TenentID == TID && (p.ROLE_NAME != "ACM Admin")).ToList();
            ListRole.DataSource = ListRoll;
            ListRole.DataBind();
            Rdorole.DataSource = ListRoll;
            Rdorole.DataTextField = "ROLE_NAME";
            Rdorole.DataValueField = "ROLE_ID";
            Rdorole.DataBind();
            ListRight.DataSource = DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == UID);
            ListRight.DataBind();
            BindMapList();
            pnlMapping.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            int LID = Convert.ToInt32(DrpLocation.SelectedValue);
            int UID = Convert.ToInt32(DrpUSer.SelectedValue);
            //Module_Mapping
            List<Database.MODULE_MAP> ListMap = new List<Database.MODULE_MAP>();
            for (int i = 0; i < ListModule.Items.Count; i++)
            {

                CheckBox CHKmodule = (CheckBox)ListModule.Items[i].FindControl("CHKmodule");
                Label lblMID = (Label)ListModule.Items[i].FindControl("lblMID");
                Label lblNameModule = (Label)ListModule.Items[i].FindControl("lblNameModule");
                int moduleID = Convert.ToInt32(lblMID.Text);
                string ModuleName = lblNameModule.Text.ToString();
                int PrivilageID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == moduleID && p.TenentID == TID).PRIVILEGE_ID;
                if (DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID && p.MODULE_ID == moduleID && p.PRIVILEGE_ID == PrivilageID).Count() > 0)
                {
                    Database.MODULE_MAP OBJEditMap = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UID && p.MODULE_ID == moduleID && p.PRIVILEGE_ID == PrivilageID);
                    if (CHKmodule.Checked == true)
                        OBJEditMap.ACTIVE_FLAG = "Y";
                    else
                        OBJEditMap.ACTIVE_FLAG = "N";

                    DB.SaveChanges();
                }
                else
                {
                    Database.MODULE_MAP OBJMap = new Database.MODULE_MAP();
                    OBJMap.TenentID = TID;
                    OBJMap.PRIVILEGE_ID = PrivilageID;
                    OBJMap.LOCATION_ID = LID;
                    OBJMap.MODULE_ID = moduleID;
                    OBJMap.MODULE_MAP_ID = DB.MODULE_MAP.Count() > 0 ? Convert.ToInt32(DB.MODULE_MAP.Max(p => p.MODULE_MAP_ID) + 1) : 1;
                    OBJMap.UserID = UID;
                    if (CHKmodule.Checked == true)
                        OBJMap.ACTIVE_FLAG = "Y";
                    else
                        OBJMap.ACTIVE_FLAG = "N";
                    OBJMap.TenantID = 0;
                    OBJMap.CRUP_ID = 0;
                    OBJMap.MySerial = 0;
                    OBJMap.ALL_FLAG = "N";
                    OBJMap.ADD_FLAG = "N";
                    OBJMap.MODIFY_FLAG = "N";
                    OBJMap.DELETE_FLAG = "N";
                    OBJMap.VIEW_FLAG = "N";
                    OBJMap.SP1 = 0;
                    OBJMap.SP2 = 0;
                    OBJMap.SP3 = 0;
                    OBJMap.SP4 = 0;
                    OBJMap.SP5 = 0;
                    OBJMap.SP1Name = null;
                    OBJMap.SP2Name = null;
                    OBJMap.SP3Name = null;
                    OBJMap.SP4Name = null;
                    OBJMap.SP5Name = null;
                    DB.MODULE_MAP.AddObject(OBJMap);
                    DB.SaveChanges();
                }
            }

            for (int j = 0; j < ListRole.Items.Count; j++)
            {
                int pri_ID = 0;
                //CheckBox CHKrole = (CheckBox)ListRole.Items[j].FindControl("CHKrole");
                Label lblRID = (Label)ListRole.Items[j].FindControl("lblRID");
                //Label lblNameRole = (Label)ListRole.Items[j].FindControl("lblNameRole");
                int RID = Convert.ToInt32(lblRID.Text);
                Database.ROLE_MST OBJR = DB.ROLE_MST.Single(p => p.ROLE_ID == RID && p.TenentID == TID);
                int rolecheck = Convert.ToInt32(Rdorole.SelectedValue);
                List<Database.MODULE_MAP> MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
                foreach (Database.MODULE_MAP item in MapList)
                {
                    int privilageid = MapList[pri_ID].PRIVILEGE_ID;
                    if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.ROLE_ID == RID && p.USER_ID == UID && p.PRIVILEGE_ID == privilageid).Count() > 0)
                    {
                        //Database.USER_ROLE OBJEditrole = DB.USER_ROLE.Single(p => p.TenentID == TID && p.ROLE_ID == RID && p.USER_ID == UID && p.PRIVILEGE_ID == privilageid);
                        //if (RID == rolecheck)
                        //    OBJEditrole.ACTIVE_FLAG = "Y";
                        //else
                        //    OBJEditrole.ACTIVE_FLAG = "N";

                        //DB.SaveChanges();
                    }
                    else
                    {
                        Database.USER_ROLE OBJrole = new Database.USER_ROLE();
                        OBJrole.TenentID = TID;
                        OBJrole.USER_ROLE_ID = DB.USER_ROLE.Count() > 0 ? Convert.ToInt32(DB.USER_ROLE.Max(p => p.USER_ROLE_ID) + 1) : 1;
                        OBJrole.PRIVILEGE_ID = MapList[pri_ID].PRIVILEGE_ID;
                        OBJrole.LOCATION_ID = LID;
                        OBJrole.USER_ID = UID;
                        OBJrole.ROLE_ID = RID;
                        if (RID == rolecheck)
                            OBJrole.ACTIVE_FLAG = "Y";
                        else
                            OBJrole.ACTIVE_FLAG = "N";
                        OBJrole.ACTIVE_FROM_DT = OBJR.ACTIVE_FROM_DT;
                        OBJrole.ACTIVE_TO_DT = OBJR.ACTIVE_TO_DT;
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
                    pri_ID++;
                }

            }
            for (int Z = 0; Z < ListRight.Items.Count; Z++)
            {
                int pri_ID = 0;
                CheckBox CHKRight = (CheckBox)ListRight.Items[Z].FindControl("CHKRight");
                List<Database.USER_RIGHTS> RightList = new List<Database.USER_RIGHTS>();
                List<Database.MODULE_MAP> MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
                foreach (Database.MODULE_MAP item in MapList)
                {
                    int privilageid = MapList[pri_ID].PRIVILEGE_ID;
                    if (DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == privilageid).Count() > 0)
                    {

                    }
                    else
                    {
                        Database.USER_RIGHTS Rightobj = new Database.USER_RIGHTS();
                        Rightobj.TenentID = TID;
                        Rightobj.RIGHTS_ID = DB.USER_RIGHTS.Count() > 0 ? Convert.ToInt32(DB.USER_RIGHTS.Max(p => p.RIGHTS_ID) + 1) : 1;
                        Rightobj.LOCATION_ID = LID;
                        Rightobj.USER_ID = UID;
                        Rightobj.PRIVILEGE_ID = MapList[pri_ID].PRIVILEGE_ID;
                        Rightobj.ADD_FLAG = false;
                        Rightobj.MODIFY_FLAG = false;
                        Rightobj.DELETE_FLAG = false;
                        Rightobj.VIEW_FLAG = false;
                        Rightobj.ALL_FLAG = false;
                        Rightobj.CRUP_ID = 0;
                        Rightobj.IsLabelUpdate = false;
                        Rightobj.SP1 = false;
                        Rightobj.SP2 = false;
                        Rightobj.SP3 = false;
                        Rightobj.SP4 = false;
                        Rightobj.SP5 = false;
                        Rightobj.SP1Name = null;
                        Rightobj.SP2Name = null;
                        Rightobj.SP3Name = null;
                        Rightobj.SP4Name = null;
                        Rightobj.SP5Name = null;
                        Rightobj.status = null;
                        DB.USER_RIGHTS.AddObject(Rightobj);
                        DB.SaveChanges();
                    }
                    pri_ID++;
                }
            }
            BindMapList();
        }
        public void BindMapList()
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            int LID = Convert.ToInt32(DrpLocation.SelectedValue);
            int UID = Convert.ToInt32(DrpUSer.SelectedValue);
            List<Database.MODULE_MAP> MapList = new List<Database.MODULE_MAP>();
            if (TID == 15)
            {
                 MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
                ListView1.DataSource = MapList;
                ListView1.DataBind();
            }
            else if (TID == 20)
            {
                 MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
                ListView1.DataSource = MapList;
                ListView1.DataBind();

            }
            else if (TID == 130)
            {
                 MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID && p.PRIVILEGE_ID == 1029).ToList();
                ListView1.DataSource = MapList;
                ListView1.DataBind();
            }
            
            else
            {
                MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
                ListView1.DataSource = MapList;
                ListView1.DataBind();
            }

            if(TID == 15)
            {
                List<Database.USER_ROLE> ROLElist = DB.USER_ROLE.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == 1028).ToList();
                ListView2.DataSource = ROLElist.OrderBy(p => p.ROLE_ID);
                ListView2.DataBind();
            }
            else if(TID == 20)
            {
                List<Database.USER_ROLE> ROLElist = DB.USER_ROLE.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == 1028).ToList();
                ListView2.DataSource = ROLElist.OrderBy(p => p.ROLE_ID);
                ListView2.DataBind();
            }
            else if (TID == 130)
            {
                List<Database.USER_ROLE> ROLElist = DB.USER_ROLE.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == 1029).ToList();
                ListView2.DataSource = ROLElist.OrderBy(p => p.ROLE_ID);
                ListView2.DataBind();
            }
            else
            {
                List<Database.USER_ROLE> ROLElist = DB.USER_ROLE.Where(p => p.TenentID == TID && p.USER_ID == UID).ToList();
                ListView2.DataSource = ROLElist.OrderBy(p => p.ROLE_ID);
                ListView2.DataBind();
            }
            if(TID == 15)
            {
                List<Database.USER_RIGHTS> RightList = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == 1028).ToList();
                ListView3.DataSource = RightList;
                ListView3.DataBind();
            }
            else if(TID == 20)
            {
                List<Database.USER_RIGHTS> RightList = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == 1028).ToList();
                ListView3.DataSource = RightList;
                ListView3.DataBind();
            }
            else if (TID == 130)
            {
                List<Database.USER_RIGHTS> RightList = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == UID && p.PRIVILEGE_ID == 1029).ToList();
                ListView3.DataSource = RightList;
                ListView3.DataBind();
            }
            else
            {
                List<Database.USER_RIGHTS> RightList = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == UID).ToList();
                ListView3.DataSource = RightList;
                ListView3.DataBind();
            }

            
           

            for (int i = 0; i < Rdorole.Items.Count; i++)
            {
                int role = Convert.ToInt32(Rdorole.Items[i].Value);
                //int PRIID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Module).PRIVILEGE_ID;
                if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.ROLE_ID == role && p.ACTIVE_FLAG == "Y").Count() > 0)
                {
                    Rdorole.Items[i].Selected = true;
                }
            }

            for (int i = 0; i < ListModule.Items.Count; i++)
            {
                CheckBox CHKmodule = (CheckBox)ListModule.Items[i].FindControl("CHKmodule");
                Label lblMID = (Label)ListModule.Items[i].FindControl("lblMID");
                int mid = Convert.ToInt32(lblMID.Text);
                List<Database.MODULE_MAP> MapListNew = MapList.Where(p => p.MODULE_ID == mid).ToList();
                foreach (Database.MODULE_MAP itemss in MapListNew)
                {
                    if (itemss.ACTIVE_FLAG == "Y")
                    {
                        CHKmodule.Checked = true;
                    }
                }
                //for (int j = 0; j < ListView1.Items.Count; j++)
                //{
                //    Label Label5 = (Label)ListView1.Items[j].FindControl("Label5");
                //    Label Label6 = (Label)ListView1.Items[j].FindControl("Label6");
                //    Label Label7 = (Label)ListView1.Items[j].FindControl("Label7");

                //    int ptivilagee = Convert.ToInt32(Label5.Text);
                //    int modilee = Convert.ToInt32(Label6.Text);
                //    int userr = Convert.ToInt32(Label7.Text);

                //    if (MapList.Where(p => p.TenentID == TID && p.PRIVILEGE_ID == ptivilagee && p.MODULE_ID == modilee && p.UserID == UID && p.ACTIVE_FLAG == "Y").Count() > 0)
                //        CHKmodule.Checked = true;
                //    else
                //        CHKmodule.Checked = false;
                //}
            }

        }
        public string Privilage(int ID)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            if (DB.PRIVILEGE_MST.Where(p => p.PRIVILEGE_ID == ID && p.TenentID == TID).Count() > 0)
            {
                string PRIName = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == ID && p.TenentID == TID).PRIVILEGE_NAME;
                return PRIName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string USER(int ID)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == ID).Count() > 0)
            {
                string userName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == ID).LOGIN_ID;
                return userName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string ROLE(int ID)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
            if (DB.ROLE_MST.Where(p => p.ROLE_ID == ID && p.TenentID == TID).Count() > 0)
            {
                string RoleNAME = DB.ROLE_MST.Single(p => p.ROLE_ID == ID && p.TenentID == TID).ROLE_NAME;
                return RoleNAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string Module(int ID)
        {
            int TID = Convert.ToInt32(DrpTenent.SelectedValue);
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





    }
}