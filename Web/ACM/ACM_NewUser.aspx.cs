using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Web.Services;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using Classes;
using System.Configuration;

namespace Web.ACM
{
    public partial class ACM_NewUser : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillContractorID();
                AllPenal();
            }
        }
        public void FillContractorID()
        {
            drpUserID.Items.Insert(0, new ListItem("---Select---", "0"));
            drpToUserID.Items.Insert(0, new ListItem("---Select---", "0"));
            drpTenentID.Items.Insert(0, new ListItem("---Select---", "00"));
            drpToTenentID.Items.Insert(0, new ListItem("---Select---", "00"));
            //drpFromModule.Items.Insert(0, new ListItem("---Select---", "0"));
            //drpToModule.Items.Insert(0, new ListItem("---Select---", "0"));

        }
        protected void drpTenentID_SelectedIndexChanged(object sender, EventArgs e)
        {


            //ViewState["TenenatID"] = TID;
            //if (drpTenentID.SelectedValue != "00")
            //    drpLocationID.Enabled = true;
            //else
            //    drpLocationID.Enabled = false;


            //var user = (from fun12 in DB.USER_MST.Where(p => p.TenentID == TID)
            //            select new
            //            {
            //                Name = fun12.FIRST_NAME,
            //                ID = fun12.USER_ID

            //            }).Distinct();
            //drpUserID.DataSource = DB.USER_MST.Where(p => p.TenentID == TID);
            //drpUserID.DataTextField = "FIRST_NAME";
            //drpUserID.DataValueField = "USER_ID";
            //drpUserID.DataBind();
            //drpUserID.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCustSearch(string prefixText, int count)
        {
            int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
            string conStr;
            conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
            string sqlQuery = "select LOGIN_ID,USER_ID from USER_MST where (LOGIN_ID like @CustName  + '%' or FIRST_NAME like @CustName  + '%' or LAST_NAME like @CustName  + '%')";
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@CustName", prefixText);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> custList = new List<string>();
            string custItem = string.Empty;
            while (dr.Read())
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
                custList.Add(custItem);
            }
            conn.Close();
            dr.Close();
            return custList.ToArray();
        }

        protected void linkFromSearch_Click(object sender, EventArgs e)
        {

        }

        protected void drpToUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(drpTenentID.SelectedValue);
            int uid = Convert.ToInt32(drpUserID.SelectedValue);
            string UName = drpToUserID.SelectedItem.ToString();
            drpToTenentID.DataSource = DB.USER_MST.Where(p => p.LOGIN_ID == UName);
            drpToTenentID.DataTextField = "TenentID";
            drpToTenentID.DataValueField = "TenentID";
            drpToTenentID.DataBind();
            drpToTenentID.Items.Insert(0, new ListItem("---Select---", "00"));



        }

        protected void drpTenentID_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        public string Module_Name(int MODULE_ID)
        {
            int TID = Convert.ToInt32(drpTenentID.SelectedValue);
            if (DB.MODULE_MST.Where(p => p.Module_Id == MODULE_ID && p.TenentID == TID).Count() > 0)
            {
                string Module_Name = DB.MODULE_MST.Single(p => p.Module_Id == MODULE_ID && p.TenentID == TID).Module_Name;
                return Module_Name;
            }
            else
            {
                return "";
            }
        }

        protected void drpFromModule_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int TID = Convert.ToInt32(drpTenentID.SelectedValue);
            //int ModuleID = Convert.ToInt32(drpFromModule.SelectedValue);

            //List<Database.FUNCTION_MST> ListMenu = DB.FUNCTION_MST.Where(p => p.TenentID == TID && p.MODULE_ID == ModuleID && p.ACTIVEMENU == true).ToList();
            //if(ListMenu.Count() > 0)
            //{
            //    pnlMenu.Visible = true;
            //    Listview1.DataSource = ListMenu;
            //    Listview1.DataBind();
            //}
        }

        protected void btnusergo_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(HiddenField3.Value);
            string UName = txtFromSearchUSer.Text.ToString();

            List<Database.USER_MST> UList = DB.USER_MST.Where(p => p.LOGIN_ID == UName).GroupBy(p => p.LOGIN_ID).Select(p => p.FirstOrDefault()).ToList();


            drpUserID.DataSource = UList;
            drpUserID.DataTextField = "LOGIN_ID";
            drpUserID.DataValueField = "USER_ID";
            drpUserID.DataBind();
            //drpUserID.Items.Insert(0, new ListItem("---Select---", "0"));


            drpTenentID.DataSource = DB.USER_MST.Where(p => p.LOGIN_ID == UName && p.TenentID != 0);
            drpTenentID.DataTextField = "TenentID";
            drpTenentID.DataValueField = "TenentID";
            drpTenentID.DataBind();
            drpTenentID.Items.Insert(0, new ListItem("---Select---", "00"));

            List<Database.USER_MST> UTList = DB.USER_MST.Where(p => p.LOGIN_ID != UName).GroupBy(p => p.LOGIN_ID).Select(p => p.FirstOrDefault()).ToList();


            drpToUserID.DataSource = UTList;
            drpToUserID.DataTextField = "LOGIN_ID";
            drpToUserID.DataValueField = "USER_ID";
            drpToUserID.DataBind();
            drpToUserID.Items.Insert(0, new ListItem("---Select---", "0"));

            btntenentGO.Enabled = true;
        }

        protected void btntenentGO_Click(object sender, EventArgs e)
        {
            btnmoduleGO.Enabled = true;
            string UName = drpUserID.SelectedItem.ToString();
            int TID = Convert.ToInt32(drpTenentID.SelectedValue);
            int uid = Convert.ToInt32(DB.USER_MST.Single(p => p.LOGIN_ID == UName && p.TenentID == TID).USER_ID);

            List<Database.MODULE_MAP> MapList1 = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == uid && p.ACTIVE_FLAG == "Y").ToList();
            List<Database.MODULE_MST> FromModule = new List<Database.MODULE_MST>();
            int MC = 0;
            if (MapList1.Count() > 0)
            {
                for (int i = 0; i < MapList1.Count(); i++)
                {
                    MC++;
                    int MID = Convert.ToInt32(MapList1[i].MODULE_ID);
                    Database.MODULE_MST obj = DB.MODULE_MST.Single(p => p.Module_Id == MID);
                    FromModule.Add(obj);
                }
            }
            ListModule.DataSource = FromModule;
            ListModule.DataTextField = "Module_Name";
            ListModule.DataValueField = "Module_Id";
            ListModule.DataBind();
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "LinkFromTrance")
        //    {
        //        int TID = Convert.ToInt32(drpTenentID.SelectedValue);
        //        int MID = Convert.ToInt32(e.CommandArgument);
        //        if (ViewState["Tomodule"] == null)
        //        {
        //            List<Database.MODULE_MST> TOModule = DB.MODULE_MST.Where(p => p.Module_Id == MID && p.Parent_Module_id != 0 && p.ACTIVE_FLAG == "Y").ToList();
        //            ListView3.DataSource = TOModule;
        //            ListView3.DataBind();
        //            ViewState["Tomodule"] = TOModule;
        //        }
        //        else
        //        {
        //            List<Database.MODULE_MST> ViewMOdule = ((List<Database.MODULE_MST>)ViewState["Tomodule"]);
        //            Database.MODULE_MST OBJ = DB.MODULE_MST.Single(p => p.Module_Id == MID && p.Parent_Module_id != 0 && p.ACTIVE_FLAG == "Y");
        //            ViewMOdule.Add(OBJ);
        //            ListView3.DataSource = ViewMOdule;
        //            ListView3.DataBind();
        //            ViewState["Tomodule"] = ViewMOdule;
        //        }
        //    }
        //}

        //copy Function
        public void AllPenal()
        {
            pnlblockmenu.Style.Add("display", "none");
            pnlmmenu.Attributes["class"] = "expand";
            pnlblockrole.Style.Add("display", "none");
            pnlrole.Attributes["class"] = "expand";
            pnlblockmodule.Style.Add("display", "none");
            pnlmodule.Attributes["class"] = "expand";
            pnlblockuser.Style.Add("display", "none");
            pnluser.Attributes["class"] = "expand";
            pnlblocktenent.Style.Add("display", "none");
            pnltenent.Attributes["class"] = "expand";
        }
        public void copyfunctionBindDrop()
        {

        }

        protected void Linkinput_Click(object sender, EventArgs e)
        {
            List<Database.MODULE_MST> TOModule = new List<Database.MODULE_MST>();
            if (ListModule.SelectedIndex >= 0)
            {
                for (int i = 0; i < ListModule.Items.Count; i++)
                {
                    if (ListModule.Items[i].Selected)
                    {
                        if (ViewState["Tomodule"] == null)
                        {
                            int mid = Convert.ToInt32(ListModule.Items[i].Value);
                            Database.MODULE_MST objm = DB.MODULE_MST.Single(p => p.Module_Id == mid && p.Parent_Module_id != 0);
                            TOModule.Add(objm);
                            ViewState["Tomodule"] = TOModule;
                        }
                        else
                        {
                            TOModule = ((List<Database.MODULE_MST>)ViewState["Tomodule"]).ToList();
                            int mid = Convert.ToInt32(ListModule.Items[i].Value);
                            if (TOModule.Where(p => p.Module_Id == mid && p.Parent_Module_id != 0).Count() < 1)
                            {
                                Database.MODULE_MST objm = DB.MODULE_MST.Single(p => p.Module_Id == mid && p.Parent_Module_id != 0);
                                TOModule.Add(objm);
                                ViewState["Tomodule"] = TOModule;
                            }

                        }
                    }
                }
            }
            ListView3.DataSource = TOModule;
            ListView3.DataBind();
        }

        protected void ListView3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkDelete")
            {
                int MID = Convert.ToInt32(e.CommandArgument);
                List<Database.MODULE_MST> TOModule = ((List<Database.MODULE_MST>)ViewState["Tomodule"]).ToList();
                Database.MODULE_MST objd = TOModule.Single(p => p.Module_Id == MID);
                TOModule.Remove(objd);

                ListView3.DataSource = TOModule;
                ListView3.DataBind();
                if (TOModule.Count() > 0)
                    ViewState["Tomodule"] = TOModule;
                else
                    ViewState["Tomodule"] = null;
            }
        }

        protected void btnmoduleGO_Click(object sender, EventArgs e)
        {
            string FUName = drpUserID.SelectedItem.ToString();
            int FromTID = Convert.ToInt32(drpTenentID.SelectedValue);
            int FromUserId = Convert.ToInt32(DB.USER_MST.Single(p => p.LOGIN_ID == FUName && p.TenentID == FromTID).USER_ID);


            string TUName = drpToUserID.SelectedItem.ToString();
            int ToTID = Convert.ToInt32(drpToTenentID.SelectedValue);
            int TouserId = Convert.ToInt32(DB.USER_MST.Single(p => p.LOGIN_ID == TUName && p.TenentID == ToTID).USER_ID);

            List<Database.FUNCTION_MST> FromListTempFun = new List<Database.FUNCTION_MST>();
            List<Database.FUNCTION_MST> TOListTempFun = new List<Database.FUNCTION_MST>();
            if (ViewState["Tomodule"] != null)
            {
                List<Database.MODULE_MST> ModuleList = ((List<Database.MODULE_MST>)ViewState["Tomodule"]).ToList();
                foreach (Database.MODULE_MST item in ModuleList)
                {
                    int Module = Convert.ToInt32(item.Module_Id);
                    List<Database.tempUser1> Tempuser = DB.tempUser1.Where(p => p.TenentID == FromTID && p.UserID == FromUserId && p.MODULE_ID == Module && p.MASTER_ID != 0).ToList();
                    foreach (Database.tempUser1 itemt in Tempuser)
                    {
                        int Menuid = Convert.ToInt32(itemt.MENUID);
                        if (DB.FUNCTION_MST.Where(p => p.MENU_ID == Menuid && p.MODULE_ID == Module).Count() > 0)
                        {
                            Database.FUNCTION_MST objF = DB.FUNCTION_MST.Single(p => p.MENU_ID == Menuid && p.MODULE_ID == Module);
                            FromListTempFun.Add(objF);
                        }
                    }
                    List<Database.tempUser1> TempuserTO = DB.tempUser1.Where(p => p.TenentID == ToTID && p.UserID == TouserId && p.MODULE_ID == Module && p.MASTER_ID != 0).ToList();
                    foreach (Database.tempUser1 itemtt in TempuserTO)
                    {
                        int MenuidTO = Convert.ToInt32(itemtt.MENUID);
                        if (DB.FUNCTION_MST.Where(p => p.MENU_ID == MenuidTO && p.MODULE_ID == Module).Count() > 0)
                        {
                            Database.FUNCTION_MST objT = DB.FUNCTION_MST.Single(p => p.MENU_ID == MenuidTO && p.MODULE_ID == Module);
                            TOListTempFun.Add(objT);
                        }
                    }
                }
            }
            fromListMenu.DataSource = FromListTempFun.OrderBy(p => p.MODULE_ID);
            fromListMenu.DataBind();
            ToListMenu.DataSource = TOListTempFun.OrderBy(p => p.MODULE_ID);
            ToListMenu.DataBind();
            ViewState["InputMenuu"] = TOListTempFun;
        }
        public string getmodule(int mid)
        {
            if (DB.MODULE_MST.Where(p => p.Module_Id == mid).Count() > 0)
            {
                return DB.MODULE_MST.Single(p => p.Module_Id == mid).Module_Name;
            }
            else
            {
                return "upss something wrong";
            }
        }

        protected void inputMenu_Click(object sender, EventArgs e)
        {
            List<Database.FUNCTION_MST> inputList = new List<Database.FUNCTION_MST>();
            bool flagee = false;
            if (ViewState["InputMenuu"] != null)
            {
                inputList = ((List<Database.FUNCTION_MST>)ViewState["InputMenuu"]).ToList();
                for (int i = 0; i < fromListMenu.Items.Count(); i++)
                {
                    CheckBox FromCheck = (CheckBox)fromListMenu.Items[i].FindControl("FromCheck");
                    Label lblFMenuid = (Label)fromListMenu.Items[i].FindControl("lblFMenuid");
                    Label lblFmodule = (Label)fromListMenu.Items[i].FindControl("lblFmodule");
                    if (FromCheck.Checked == true)
                    {
                        flagee = false;
                        int Fmenu = Convert.ToInt32(lblFMenuid.Text);
                        int FModule = Convert.ToInt32(lblFmodule.Text);
                        for (int j = 0; j < inputList.Count(); j++)
                        {
                            int TMenu = Convert.ToInt32(inputList[j].MENU_ID);
                            if (TMenu == Fmenu)
                            {
                                flagee = true;
                            }
                        }
                        if (flagee == false)
                        {
                            Database.FUNCTION_MST objF = DB.FUNCTION_MST.Single(p => p.MENU_ID == Fmenu && p.MODULE_ID == FModule);
                            inputList.Add(objF);
                            ViewState["InputMenuu"] = inputList;
                        }
                    }
                }

            }
            else
            {
                for (int i = 0; i < fromListMenu.Items.Count(); i++)
                {
                    CheckBox FromCheck = (CheckBox)fromListMenu.Items[i].FindControl("FromCheck");
                    Label lblFMenuid = (Label)fromListMenu.Items[i].FindControl("lblFMenuid");
                    Label lblFmodule = (Label)fromListMenu.Items[i].FindControl("lblFmodule");
                    if (FromCheck.Checked == true)
                    {
                        int Fmenu = Convert.ToInt32(lblFMenuid.Text);
                        int FModule = Convert.ToInt32(lblFmodule.Text);
                        Database.FUNCTION_MST objF = DB.FUNCTION_MST.Single(p => p.MENU_ID == Fmenu && p.MODULE_ID == FModule);
                        inputList.Add(objF);
                        ViewState["InputMenuu"] = inputList;
                    }

                }
            }
            if (inputList.Count() > 0)
                FinalCopy.Enabled = true;
            ToListMenu.DataSource = inputList.OrderBy(p => p.MODULE_ID);
            ToListMenu.DataBind();
            if (ViewState["TempRemove"] != null)
            {
                List<Database.FUNCTION_MST> TempRemove = ((List<Database.FUNCTION_MST>)ViewState["TempRemove"]).ToList();
                foreach (Database.FUNCTION_MST Ritem in inputList)
                {
                    if (TempRemove.Where(p => p.MENU_ID == Ritem.MENU_ID && p.MODULE_ID == Ritem.MODULE_ID).Count() > 0)
                    {
                        Database.FUNCTION_MST Robj = TempRemove.Single(p => p.MENU_ID == Ritem.MENU_ID && p.MODULE_ID == Ritem.MODULE_ID);
                        TempRemove.Remove(Robj);
                        ViewState["TempRemove"] = TempRemove;
                    }
                }
            }
        }

        protected void FinalCopy_Click(object sender, EventArgs e)
        {
            //First check all mapping
            int TTID = Convert.ToInt32(drpToTenentID.SelectedValue);
            string TUName = drpToUserID.SelectedItem.ToString();
            int TouserId = 0;
            DateTime ActiveDT = DateTime.Now;
            if (DB.USER_MST.Where(p => p.LOGIN_ID == TUName && p.TenentID == TTID).Count() > 0)
            {
                Database.USER_MST UserTillobj = DB.USER_MST.Single(p => p.LOGIN_ID == TUName && p.TenentID == TTID);
                TouserId = Convert.ToInt32(UserTillobj.USER_ID);
                ActiveDT = Convert.ToDateTime(UserTillobj.Till_DT);
            }


            List<Database.MODULE_MST> TOModule = ((List<Database.MODULE_MST>)ViewState["Tomodule"]).ToList();
            List<Database.FUNCTION_MST> CopyPrivilage = ((List<Database.FUNCTION_MST>)ViewState["InputMenuu"]).ToList();

            bool DefaultFlage = false;
            foreach (Database.MODULE_MST itemM in TOModule)
            {
                int MID = Convert.ToInt32(itemM.Module_Id);
                int PrivilageID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == MID).PRIVILEGE_ID;
                //check Module Mapping
                List<Database.MODULE_MAP> DefaultMap = DB.MODULE_MAP.Where(p => p.TenentID == TTID && p.UserID == TouserId).ToList();
                if (DefaultMap.Count() > 0)
                {
                    if (DefaultMap.Where(p => p.SP1Name == "DefaultSet").Count() > 0)
                    {
                        DefaultFlage = true;
                    }
                    else
                    {
                        DefaultFlage = false;
                    }
                }
                else
                {
                    DefaultFlage = false;
                }
                if (DB.MODULE_MAP.Where(p => p.TenentID == TTID && p.UserID == TouserId && p.MODULE_ID == MID && p.PRIVILEGE_ID == PrivilageID).Count() > 0)
                {
                    //Database.MODULE_MAP OBJEditMap = DB.MODULE_MAP.Single(p => p.TenentID == TTID && p.UserID == TouserId && p.MODULE_ID == MID && p.PRIVILEGE_ID == PrivilageID);
                    //OBJEditMap.ACTIVE_FLAG = "Y";
                    //DB.SaveChanges();
                }
                else
                {
                    Database.MODULE_MAP OBJMap = new Database.MODULE_MAP();
                    OBJMap.TenentID = TTID;
                    OBJMap.PRIVILEGE_ID = PrivilageID;
                    OBJMap.LOCATION_ID = 1;
                    OBJMap.MODULE_ID = MID;
                    OBJMap.MODULE_MAP_ID = DB.MODULE_MAP.Count() > 0 ? Convert.ToInt32(DB.MODULE_MAP.Max(p => p.MODULE_MAP_ID) + 1) : 1;
                    OBJMap.UserID = TouserId;
                    OBJMap.ACTIVE_FLAG = "Y";
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
                    if (DefaultFlage == false)
                        OBJMap.SP1Name = "DefaultSet";
                    else
                        OBJMap.SP1Name = null;
                    OBJMap.SP2Name = null;
                    OBJMap.SP3Name = null;
                    OBJMap.SP4Name = null;
                    OBJMap.SP5Name = null;
                    DB.MODULE_MAP.AddObject(OBJMap);
                    DB.SaveChanges();
                }
                //check Role mapping
                int RID = Convert.ToInt32(DB.USER_MST.Single(p => p.TenentID == TTID && p.USER_ID == TouserId).USER_TYPE);
                if (DB.ROLE_MST.Where(p => p.TenentID == TTID && p.ROLE_ID == RID).Count() == 0)
                {
                    Database.ROLE_MST rollobj = new Database.ROLE_MST();
                    rollobj.TenentID = TTID;
                    rollobj.ROLE_ID = RID;
                    rollobj.ROLE_NAME = "coman user";
                    rollobj.ROLE_NAME1 = "coman user";
                    rollobj.ROLE_NAME2 = "coman user";
                    rollobj.ROLE_DESC = "coman user";
                    rollobj.ACTIVE_FLAG = "Y";
                    rollobj.ACTIVE_FROM_DT = DateTime.Now;
                    rollobj.ACTIVE_TO_DT = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, 25);
                    rollobj.CRUP_ID = 0;
                    rollobj.ACTIVEROLE = true;
                    rollobj.ROLLDATE = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, 25);
                    DB.ROLE_MST.AddObject(rollobj);
                    DB.SaveChanges();
                }
                if (DB.USER_ROLE.Where(p => p.TenentID == TTID && p.ROLE_ID == RID && p.USER_ID == TouserId && p.PRIVILEGE_ID == PrivilageID).Count() < 1)
                {
                    Database.USER_ROLE OBJrole = new Database.USER_ROLE();
                    OBJrole.TenentID = TTID;
                    OBJrole.USER_ROLE_ID = DB.USER_ROLE.Count() > 0 ? Convert.ToInt32(DB.USER_ROLE.Max(p => p.USER_ROLE_ID) + 1) : 1;
                    OBJrole.PRIVILEGE_ID = PrivilageID;
                    OBJrole.LOCATION_ID = 1;
                    OBJrole.USER_ID = TouserId;
                    OBJrole.ROLE_ID = RID;
                    OBJrole.ACTIVE_FLAG = "Y";
                    OBJrole.ACTIVE_FROM_DT = DateTime.Now;
                    OBJrole.ACTIVE_TO_DT = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, 25);
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
                //right
                if (DB.USER_RIGHTS.Where(p => p.TenentID == TTID && p.USER_ID == TouserId && p.PRIVILEGE_ID == PrivilageID).Count() < 1)
                {
                    Database.USER_RIGHTS Rightobj = new Database.USER_RIGHTS();
                    Rightobj.TenentID = TTID;
                    Rightobj.RIGHTS_ID = DB.USER_RIGHTS.Count() > 0 ? Convert.ToInt32(DB.USER_RIGHTS.Max(p => p.RIGHTS_ID) + 1) : 1;
                    Rightobj.LOCATION_ID = 1;
                    Rightobj.USER_ID = TouserId;
                    Rightobj.PRIVILEGE_ID = PrivilageID;
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
                    Rightobj.status = "Final";
                    DB.USER_RIGHTS.AddObject(Rightobj);
                    DB.SaveChanges();
                }

                //copy Privilage
                List<Database.FUNCTION_MST> PrivilageUser = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == TouserId && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 3).Count() < 1)
                {

                    //CopyPrivilage = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                    foreach (Database.FUNCTION_MST item in PrivilageUser)
                    {
                        int PriMaster = Convert.ToInt32(item.MASTER_ID);
                        DateTime dt = Convert.ToDateTime(ActiveDT);
                        if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == TouserId && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 3 && p.MENU_ID == PriMaster).Count() != 1)
                        {
                            usermaster(TTID, TouserId, PrivilageID, PriMaster, dt);
                        }
                        PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                        objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = TouserId;
                        objPRIVILAGE_MENU.TenentID = TTID;
                        objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                        objPRIVILAGE_MENU.PRIVILAGEFOR = 3;
                        objPRIVILAGE_MENU.LOCATION_ID = 1;
                        objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                        objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                        objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                        objPRIVILAGE_MENU.ACTIVETILLDATE = ActiveDT;
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
                        objPRIVILAGE_MENU.SP5 = "Y";
                        objPRIVILAGE_MENU.ActiveMenu = "Y";
                        objPRIVILAGE_MENU.ActiveModule = "Y";
                        objPRIVILAGE_MENU.Action = "Y";

                        objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                        DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                        DB.SaveChanges();
                    }
                }
                else
                {
                    List<Database.PRIVILAGE_MENUDemon> FinalList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == TouserId && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 3).ToList();
                    //CopyPrivilage = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                    foreach (Database.FUNCTION_MST item in PrivilageUser)
                    {
                        if (FinalList.Where(p => p.MENU_ID == item.MENU_ID).Count() == 0)
                        {
                            int PriMaster = Convert.ToInt32(item.MASTER_ID);
                            DateTime dt = Convert.ToDateTime(ActiveDT);
                            if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == TouserId && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 3 && p.MENU_ID == PriMaster).Count() != 1)
                            {
                                usermaster(TTID, TouserId, PrivilageID, PriMaster, dt);
                            }
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = TouserId;
                            objPRIVILAGE_MENU.TenentID = TTID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 3;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = ActiveDT;
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
                            objPRIVILAGE_MENU.SP5 = "Y";
                            objPRIVILAGE_MENU.ActiveMenu = "Y";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.Action = "Y";

                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                        }
                    }
                    if (ViewState["TempRemove"] != null)
                    {
                        List<Database.FUNCTION_MST> TempRemove = ((List<Database.FUNCTION_MST>)ViewState["TempRemove"]).ToList();
                        for (int z = 0; z < TempRemove.Count(); z++)
                        {
                            int Menid = Convert.ToInt32(TempRemove[z].MENU_ID);
                            int Modid = Convert.ToInt32(TempRemove[z].MODULE_ID);
                            int PPRI = Convert.ToInt32(DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Modid).PRIVILEGE_ID);
                            if (DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TTID && p.PRIVILEGE_MENU_ID == TouserId && p.PRIVILEGE_ID == PPRI && p.MENU_ID == Menid).Count() > 0)
                            {
                                Database.PRIVILAGE_MENUDemon objPRIVILAGEremove = DB.PRIVILAGE_MENUDemon.Single(p => p.TenentID == TTID && p.PRIVILEGE_MENU_ID == TouserId && p.PRIVILEGE_ID == PPRI && p.MENU_ID == Menid);
                                DB.PRIVILAGE_MENUDemon.DeleteObject(objPRIVILAGEremove);
                                DB.SaveChanges();
                            }

                        }
                    }
                }
                //Copy Module                
                if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == MID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 2).Count() < 1)
                {
                    //CopyPrivilage = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                    foreach (Database.FUNCTION_MST item in PrivilageUser)
                    {
                        int PriMaster = Convert.ToInt32(item.MASTER_ID);
                        DateTime dt = Convert.ToDateTime(ActiveDT);
                        if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == MID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 2 && p.MENU_ID == PriMaster).Count() != 1)
                        {
                            MenuMaster(TTID, MID, PrivilageID, PriMaster, dt);
                        }
                        PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                        objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = MID;
                        objPRIVILAGE_MENU.TenentID = TTID;
                        objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                        objPRIVILAGE_MENU.PRIVILAGEFOR = 2;
                        objPRIVILAGE_MENU.LOCATION_ID = 1;
                        objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                        objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                        objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                        objPRIVILAGE_MENU.ACTIVETILLDATE = ActiveDT;
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
                        objPRIVILAGE_MENU.SP5 = "Y";
                        objPRIVILAGE_MENU.ActiveMenu = "Y";
                        objPRIVILAGE_MENU.ActiveModule = "Y";
                        objPRIVILAGE_MENU.Action = "Y";

                        objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                        DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                        DB.SaveChanges();
                    }

                }
                else
                {
                    List<Database.PRIVILAGE_MENUDemon> FinalList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == MID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 2).ToList();
                    //CopyPrivilage = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                    foreach (Database.FUNCTION_MST item in PrivilageUser)
                    {
                        if (FinalList.Where(p => p.MENU_ID == item.MENU_ID).Count() == 0)
                        {
                            int PriMaster = Convert.ToInt32(item.MASTER_ID);
                            DateTime dt = Convert.ToDateTime(ActiveDT);
                            if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == MID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 2 && p.MENU_ID == PriMaster).Count() != 1)
                            {
                                MenuMaster(TTID, MID, PrivilageID, PriMaster, dt);
                            }
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = MID;
                            objPRIVILAGE_MENU.TenentID = TTID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 2;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = ActiveDT;
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
                            objPRIVILAGE_MENU.SP5 = "Y";
                            objPRIVILAGE_MENU.ActiveMenu = "Y";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.Action = "Y";

                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                        }

                    }
                    if (ViewState["TempRemove"] != null)
                    {
                        List<Database.FUNCTION_MST> TempRemove = ((List<Database.FUNCTION_MST>)ViewState["TempRemove"]).ToList();
                        for (int z = 0; z < TempRemove.Count(); z++)
                        {
                            int Menid = Convert.ToInt32(TempRemove[z].MENU_ID);
                            int Modid = Convert.ToInt32(TempRemove[z].MODULE_ID);
                            int PPRI = Convert.ToInt32(DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Modid).PRIVILEGE_ID);
                            if (DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TTID && p.PRIVILEGE_MENU_ID == Modid && p.PRIVILEGE_ID == PPRI && p.MENU_ID == Menid).Count() > 0)
                            {
                                Database.PRIVILAGE_MENUDemon objPRIVILAGEremove = DB.PRIVILAGE_MENUDemon.Single(p => p.TenentID == TTID && p.PRIVILEGE_MENU_ID == Modid && p.PRIVILEGE_ID == PPRI && p.MENU_ID == Menid);
                                DB.PRIVILAGE_MENUDemon.DeleteObject(objPRIVILAGEremove);
                                DB.SaveChanges();
                            }

                        }
                    }
                }
                //Copy Roll
                if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == RID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).Count() < 1)
                {

                    //CopyPrivilage = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                    foreach (Database.FUNCTION_MST item in PrivilageUser)
                    {
                        int PriMaster = Convert.ToInt32(item.MASTER_ID);
                        DateTime dt = Convert.ToDateTime(ActiveDT);
                        if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == RID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 1 && p.MENU_ID == PriMaster).Count() != 1)
                        {
                            RoleMaster(TTID, RID, PrivilageID, PriMaster, dt);
                        }
                        PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                        objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = RID;
                        objPRIVILAGE_MENU.TenentID = TTID;
                        objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                        objPRIVILAGE_MENU.PRIVILAGEFOR = 1;
                        objPRIVILAGE_MENU.LOCATION_ID = 1;
                        objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                        objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                        objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                        objPRIVILAGE_MENU.ACTIVETILLDATE = ActiveDT;
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
                        objPRIVILAGE_MENU.SP5 = "Y";
                        objPRIVILAGE_MENU.ActiveMenu = "Y";
                        objPRIVILAGE_MENU.ActiveModule = "Y";
                        objPRIVILAGE_MENU.Action = "Y";

                        objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                        DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                        DB.SaveChanges();
                    }

                }
                else
                {
                    List<Database.PRIVILAGE_MENUDemon> FinalList = DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == RID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.ACTIVE_FLAG == "Y" && p.PRIVILAGEFOR == 1).ToList();
                    //CopyPrivilage = CopyPrivilage.Where(p => p.MODULE_ID == MID).ToList();
                    foreach (Database.FUNCTION_MST item in PrivilageUser)
                    {
                        if (FinalList.Where(p => p.MENU_ID == item.MENU_ID).Count() == 0)
                        {
                            int PriMaster = Convert.ToInt32(item.MASTER_ID);
                            DateTime dt = Convert.ToDateTime(ActiveDT);
                            if (DB.PRIVILAGE_MENUDemon.Where(p => p.PRIVILEGE_MENU_ID == RID && p.TenentID == TTID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 1 && p.MENU_ID == PriMaster).Count() != 1)
                            {
                                RoleMaster(TTID, RID, PrivilageID, PriMaster, dt);
                            }
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = RID;
                            objPRIVILAGE_MENU.TenentID = TTID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 1;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = item.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = item.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = item.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = ActiveDT;
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
                            objPRIVILAGE_MENU.SP5 = "Y";
                            objPRIVILAGE_MENU.ActiveMenu = "Y";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.Action = "Y";

                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                        }

                    }
                    if (ViewState["TempRemove"] != null)
                    {
                        List<Database.FUNCTION_MST> TempRemove = ((List<Database.FUNCTION_MST>)ViewState["TempRemove"]).ToList();
                        for (int z = 0; z < TempRemove.Count(); z++)
                        {
                            int Menid = Convert.ToInt32(TempRemove[z].MENU_ID);
                            int Modid = Convert.ToInt32(TempRemove[z].MODULE_ID);
                            int PPRI = Convert.ToInt32(DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == Modid).PRIVILEGE_ID);
                            if (DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TTID && p.PRIVILEGE_MENU_ID == RID && p.PRIVILEGE_ID == PPRI && p.MENU_ID == Menid).Count() > 0)
                            {
                                Database.PRIVILAGE_MENUDemon objPRIVILAGEremove = DB.PRIVILAGE_MENUDemon.Single(p => p.TenentID == TTID && p.PRIVILEGE_MENU_ID == RID && p.PRIVILEGE_ID == PPRI && p.MENU_ID == Menid);
                                DB.PRIVILAGE_MENUDemon.DeleteObject(objPRIVILAGEremove);
                                DB.SaveChanges();
                            }

                        }
                    }
                }
                //int TID = Convert.ToInt32(drpTenet.SelectedValue);
                //int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                //int UID = Convert.ToInt32(drpUserMST.SelectedValue);
                //int Privilageid = Convert.ToInt32(drpPrevilegeMST.SelectedValue);
                //int Modulid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == UID && p.PRIVILEGE_ID == Privilageid).MODULE_ID;
                //int RID = DB.USER_ROLE.Where(p => p.USER_ID == UID && p.PRIVILEGE_ID == Privilageid && p.ACTIVE_FLAG == "Y").Count() > 0 ? DB.USER_ROLE.Where(p => p.USER_ID == UID && p.PRIVILEGE_ID == Privilageid && p.ACTIVE_FLAG == "Y").First().ROLE_ID : 0;
                GlobleClass.DeleteTempUser(TTID, TouserId, 1, MID);
                GlobleClass.getMenuGloble(TTID, TouserId, 1, MID, RID);

                int FromTID = Convert.ToInt32(drpTenentID.SelectedValue);
                int Comp = 0;
                string Str = "";
                if (DB.TBLLOCATIONs.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[TBLLOCATION]([TenentID],[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID]) SELECT " + TTID + ",[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID] from [TBLLOCATION] where [TenentID]=" + FromTID + ";";
                //if (DB.REFTABLEs.Where(p => p.TenentID == TTID).Count() == 0)
                //    Str += "INSERT INTO [dbo].[REFTABLE]([TenentID],[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate]) Select " + TTID + ",[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate] from [REFTABLE] where [TenentID]=" + FromTID + ";";
                Str += "INSERT INTO [dbo].[REFTABLE]([TenentID],[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate]) Select " + TTID + ",[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate] from [REFTABLE] where [TenentID]=" + FromTID + " and REFID not in(select REFID from REFTABLE where TenentID = " + TTID + " and REFID = REFTABLE.REFID);";
                if (DB.tblCOUNTRies.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[tblCOUNTRY]([TenentID],[COUNTRYID],[REGION1],[COUNAME1],[COUNAME2],[COUNAME3],[CAPITAL],[NATIONALITY1],[NATIONALITY2],[NATIONALITY3],[CURRENCYNAME1],[CURRENCYNAME2],[CURRENCYNAME3],[CURRENTCONVRATE],[CURRENCYSHORTNAME1],[CURRENCYSHORTNAME2],[CURRENCYSHORTNAME3],[CountryType],[CountryTSubType],[Sovereignty],[ISO4217CurCode],[ISO4217CurName],[ITUTTelephoneCode],[FaxLength],[TelLength],[ISO3166_1_2LetterCode],[ISO3166_1_3LetterCode],[ISO3166_1Number],[IANACountryCodeTLD],[Active],[CRUP_ID]) select " + TTID + ",[COUNTRYID],[REGION1],[COUNAME1],[COUNAME2],[COUNAME3],[CAPITAL],[NATIONALITY1],[NATIONALITY2],[NATIONALITY3],[CURRENCYNAME1],[CURRENCYNAME2],[CURRENCYNAME3],[CURRENTCONVRATE],[CURRENCYSHORTNAME1],[CURRENCYSHORTNAME2],[CURRENCYSHORTNAME3],[CountryType],[CountryTSubType],[Sovereignty],[ISO4217CurCode],[ISO4217CurName],[ITUTTelephoneCode],[FaxLength],[TelLength],[ISO3166_1_2LetterCode],[ISO3166_1_3LetterCode],[ISO3166_1Number],[IANACountryCodeTLD],[Active],[CRUP_ID] from [tblCOUNTRY] where [TenentID]=" + FromTID + ";";
                if (DB.TBLCOLORs.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[TBLCOLOR]([TenentID],[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active]) select " + TTID + ",[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active] from [TBLCOLOR] where TenentID=" + FromTID + ";";
                if (DB.TBLSIZEs.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[TBLSIZE]([TenentID],[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE])select " + TTID + ",[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE] from [TBLSIZE] where TenentID=" + FromTID + ";";
                if (DB.ICUOMs.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[ICUOM]([TenentID],[UOM],[UOMNAMESHORT],[UOMNAME1],[UOMNAME2],[UOMNAME3],[REMARKS],[CRUP_ID],[Active],[UOMNAME],[UOMNAMEO])VALUES(" + TTID + ",99999,'Not Used','Not Used','Not Used','Not Used','Not Used',0,'Y','Not Used','Not Used');";
                if (DB.CAT_MST.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[CAT_MST]([TenentID],[CATID],[PARENT_CATID],[DefaultPic],[SHORT_NAME],[CAT_NAME1],[CAT_NAME2],[CAT_NAME3],[CAT_DESCRIPTION],[CAT_TYPE],[WARRANTY],[CRUP_ID],[Active],[SupplierPercent]) VALUES (" + TTID + ",99999,0,'cc','Not Used','Not Used','Not Used','Not Used','Not Used','WEBSALE','0 Months',0,'1',0.0);";
                if (DB.TBLGROUPs.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[TBLGROUP]([TenentID],[LocationId],[ITGROUPID],[GroupType],[ITGROUPDESC1],[ITGROUPDESC2],[ITGROUPREMARKS],[ACTIVE_Flag],[USERCODE],[GROUPID],[remarks],[ACTIVE],[CRUP_ID],[Infastructure]) VALUES (" + TTID + ",1,999999999,'','Not Used','Not Used','Not Used','True','','','','1',0,'False');";
                if (DB.DEPTOFSales.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[DEPTOFSale]([TenentID],[SalDeptID],[DeptDesc1],[DeptDesc2],[DeptDesc3],[DeptRemarks],[SalesAccountID],[Margin],[ExpenseAccountID],[PurchaseAccountID],[Active_Flag],[CRUP_ID],[DeptManagerID]) VALUES (" + TTID + ",999999999,'Not Exist Yet','Not Exist Yet','Not Exist Yet','Not Exist Yet','0',0.0,'','','True',0,0);";
                if (DB.MYBUS.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[MYBUS]([TenentID],[BUSID],[BUSTYPE],[SHORTNAME],[BUSNAME1],[BUSNAME2],[BUSNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[Remarks],[COMPANID],[BUSNAME],[BUSNAMEO],[BUSNAMCHIN],[BUSYPE],[CRUP_ID]) Select " + TTID + ",[BUSID],[BUSTYPE],[SHORTNAME],[BUSNAME1],[BUSNAME2],[BUSNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[Remarks],[COMPANID],[BUSNAME],[BUSNAMEO],[BUSNAMCHIN],[BUSYPE],[CRUP_ID] from [MYBUS] Where TenentID=" + FromTID + ";";
                if (DB.RefLabelMSTs.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[RefLabelMST]([TenentID],[RefLabelID],[RefType],[RefSubType],[LE1],[LE2],[LE3],[LE4],[LE5],[LE6],[LE7],[LE8],[LE9],[LE10],[LF1],[LF2],[LF3],[LF4],[LF5],[LF6],[LF7],[LF8],[LF9],[LF10],[LA1],[LA2],[LA3],[LA4],[LA5],[LA6],[LA7],[LA8],[LA9],[LA10]) Select " + TTID + ",[RefLabelID],[RefType],[RefSubType],[LE1],[LE2],[LE3],[LE4],[LE5],[LE6],[LE7],[LE8],[LE9],[LE10],[LF1],[LF2],[LF3],[LF4],[LF5],[LF6],[LF7],[LF8],[LF9],[LF10],[LA1],[LA2],[LA3],[LA4],[LA5],[LA6],[LA7],[LA8],[LA9],[LA10] from [RefLabelMST] where TenentID=" + FromTID + ";";
                //if (DB.tbltranstypes.Where(p => p.TenentID == TTID).Count() == 0)
                    //Str += "select * into TempCopy_tbltranstype from tbltranstype where TenentID = " + FromTID + ";update TempCopy_tbltranstype set TenentID = " + TTID + " where TenentID = " + FromTID + ";INSERT INTO tbltranstype select * from TempCopy_tbltranstype where TenentID = " + TTID + ";drop table TempCopy_tbltranstype;";                
                //if (DB.tbltranssubtypes.Where(p => p.TenentID == TTID).Count() == 0)
                    //Str += "select * into TempCopy_tbltranssubtype from tbltranssubtype where TenentID = " + FromTID + ";update TempCopy_tbltranssubtype set TenentID = " + TTID + " where TenentID = " + FromTID + ";INSERT INTO tbltranssubtype select * from TempCopy_tbltranssubtype where TenentID = " + TTID + ";drop table TempCopy_tbltranssubtype;";
                Str += "INSERT INTO [dbo].[tbltranstype]([TenentID],[transid],[MYSYSNAME],[inoutSwitch],[transtype1],[transtype2],[transtype3],[serialno],[years],[Active],[CRUP_ID],[transtype],[switch1]) select " + TTID + ",[transid],[MYSYSNAME],[inoutSwitch],[transtype1],[transtype2],[transtype3],[serialno],[years],[Active],[CRUP_ID],[transtype],[switch1] from tbltranstype where TenentID=" + FromTID + " and transid not in(select transid from tbltranstype where TenentID=" + TTID + " and transid=tbltranstype.transid);";
                Str += "INSERT INTO [dbo].[tbltranssubtype]([TenentID],[transid],[MYSYSNAME],[transsubid],[transsubtype1],[transsubtype2],[transsubtype3],[OpQtyBeh],[OnHandBeh],[QtyOutBeh],[QtyConsumedBeh],[QtyReservedBeh],[QtyAtDestination],[QtyAtSource],[serialno],[years],[Active],[CRUP_ID],[transsubtype],[CashBeh],[QtyReceivedBeh]) select " + TTID + ",[transid],[MYSYSNAME],[transsubid],[transsubtype1],[transsubtype2],[transsubtype3],[OpQtyBeh],[OnHandBeh],[QtyOutBeh],[QtyConsumedBeh],[QtyReservedBeh],[QtyAtDestination],[QtyAtSource],[serialno],[years],[Active],[CRUP_ID],[transsubtype],[CashBeh],[QtyReceivedBeh] from tbltranssubtype where TenentID = " + FromTID + " and transsubid not in(select transsubid from tbltranssubtype where TenentID=" + TTID + " and transid = tbltranssubtype.transid and transsubid = tbltranssubtype.transsubid);";
                if (DB.tblLanguages.Where(p => p.TenentID == TTID).Count() == 0)//
                    Str += "INSERT INTO [dbo].[tblLanguage]([TenentID],[MYCONLOCID],[COUNTRYID],[LangName1],[LangName2],[LangName3],[CULTUREOCDE],[ACTIVE],[REMARKS],[CRUP_ID],[Deleted],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID])select " + TTID + ",[MYCONLOCID],[COUNTRYID],[LangName1],[LangName2],[LangName3],[CULTUREOCDE],[ACTIVE],[REMARKS],[CRUP_ID],[Deleted],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID] from tblLanguage where TenentID=" + FromTID + ";";
                if (DB.tblsetupPurchases.Where(p => p.TenentID == TTID).Count() == 0)
                    Str += "INSERT INTO [dbo].[tblsetupPurchase]([TenentID],[locationID],[module],[DeliveryLocation],[BottomTagLine],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[AutoGeneratePO],[AutoGenerateGRN],[transid2],[transsubid2],[transid1],[transsubid1],[Created],[DateTime],[Active],[Deleted]) select " + TTID + ",[locationID],[module],[DeliveryLocation],[BottomTagLine],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[AutoGeneratePO],[AutoGenerateGRN],[transid2],[transsubid2],[transid1],[transsubid1],[Created],[DateTime],[Active],[Deleted] from tblsetupPurchase where TenentID = " + FromTID + ";";
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TTID && p.COMPNAME1.Contains("Cash")).Count() == 0)
                {
                    Comp = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TTID).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TTID).Max(p => p.COMPID) + 1) : 1;
                    Str += "INSERT INTO [dbo].[TBLCOMPANYSETUP]([TenentID],[COMPID],[OldCOMPid],[PHYSICALLOCID],[COMPNAME1],[COMPNAME2],[COMPNAME3],[BirthDate],[CivilID],[EMAIL],[EMAIL1],[EMAIL2],[ITMANAGER],[ADDR1],[ADDR2],[CITY],[STATE],[POSTALCODE],[ZIPCODE],[MYCONLOCID],[MYPRODID],[COUNTRYID],[BUSPHONE1],[BUSPHONE2],[BUSPHONE3],[BUSPHONE4],[MOBPHONE],[FAX],[FAX1],[FAX2],[PRIMLANGUGE],[WEBPAGE],[ISMINISTRY],[ISSMB],[ISCORPORATE],[INHAWALLY],[SALER],[BUYER],[SALEDEPROD],[EMAISUB],[EMAILSUBDATE],[PRODUCTDEALIN],[REMARKS],[Keyword],[COMPANYID],[BUSID],[MYCATSUBID],[COMPNAME],[COMPNAMEO],[COMPNAMECH],[Active],[CRUP_ID],[CUSERID],[CPASSWRD],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Approved],[CompanyType],[Images],[BARCODE],[Avtar],[Reload],[datasource],[PACI],[Marketting],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID]) VALUES (" + TTID + "," + Comp + ",0 ,'KWT','Cash','Cash' ,'Cash',NULL,'' ,'','','' ,'0','','' ,'1','1902','' ,'',0,0 ,126,'','' ,'','','' ,'','','' ,'1','',0 ,0,0,0 ,0,0,0 ,0,NULL,'' ,'','',0 ,0,0,'' ,'','','Y' ,0,'','' ,'',NULL,NULL ,NULL,0,'82005' ,NULL,'','' ,0,0,'' ,'',NULL,'' ,NULL,'',0);";
                }
                else
                {
                    Comp = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TTID && p.COMPNAME1.Contains("Cash")).COMPID;
                }
                Str += "INSERT INTO [dbo].[tblsetupsalesh]([TenentID],[locationID],[transid],[transsubid],[module],[DeliveryLocation],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[DescWithWarantee],[DescWithSerial],[DescWithColor],[AllowMinusQty],[HeaderLine],[TagLine],[BottomTagLine],[PaymentDetails],[TCQuotation],[IntroLetter],[COUNTRYID],[SalesAdminID],[CRUP_ID],[InvoicePrintURL],[DeliveryPrintURL],[CounterName],[EmployeeId],[DeftCoustomer],[Created],[DateTime],[Active],[Deleted]) select " + TTID + ",[locationID],[transid],[transsubid],[module],[DeliveryLocation],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[DescWithWarantee],[DescWithSerial],[DescWithColor],[AllowMinusQty],[HeaderLine],[TagLine],[BottomTagLine],[PaymentDetails],[TCQuotation],[IntroLetter],[COUNTRYID],[SalesAdminID],[CRUP_ID],[InvoicePrintURL],[DeliveryPrintURL],[CounterName],[EmployeeId]," + Comp + ",[Created],[DateTime],[Active],[Deleted] from [tblsetupsalesh] where TenentID=" + FromTID + " and transsubid not in ( SELECT transsubid from tblsetupsalesh  WHERE TenentID=" + TTID + " and locationID=1 and transid = tblsetupsalesh.transid and transsubid = tblsetupsalesh.transsubid);";
                if (DB.tblSetupInventories.Where(p => p.TenentID == TTID).Count() == 0)
                {
                    Str += "INSERT INTO [dbo].[tblSetupInventory]([TenentID],[locationID],[TransferOutTransType],[TransferOutTransSubType],[TransferInTransType],[TransferInTransSubType],[ConsumeTransType],[ConsumeTransSubType],[transidOut],[transsubidOut],[transidin],[transsubidIn],[transidConsume],[transsubidConsume],[MYSYSNAMEOut],[MYSYSNAMEIn],[StockTeking],[StockTakingPeriodBegin],[StockTakingPeriodEnd],[StockTakingTransTypeIn],[StockTakingTransTypeOut],[StockTakingTransSubTypeIn],[StockTakingTransSubTypeOut],[StockTakingtransidInLast],[StockTakingtransidOutLast],[DefaultCUSTVENDID],[Created],[DateTime],[Active],[Deleted]) VALUES (" + TTID + ",1,'Transfer Notes - Out','Transfer Notes - Out','Transfer Notes - In','Transfer Notes - In','Transfer Notes - Consume','Transfer Notes - Consume',21,221,11,111,31,331,'IC','IC',NULL,NULL,NULL,'In StockTaking','Out StockTaking','In StockTaking','Out StockTaking',301,401," + Comp + ",NULL,NULL,NULL,NULL);";
                }
                Str += "INSERT INTO [dbo].[TBLSYSTEMS]([TenentID],[SystemID],[MYSYSNAME],[SYSDESC1],[SYSDESC2],[SYSDESC3],[SHORTNAME],[REMARKS],[STARTDATE],[CRUP_ID],[ACTIVE],[SYSDESC],[SYSDESCO],[SYSDESCCH]) SELECT " + TTID + ",[SystemID],[MYSYSNAME],[SYSDESC1],[SYSDESC2],[SYSDESC3],[SHORTNAME],[REMARKS],[STARTDATE],[CRUP_ID],[ACTIVE],[SYSDESC],[SYSDESCO],[SYSDESCCH] from TBLSYSTEMS where TenentID=" + FromTID + " and SystemID not in(select SystemID from TBLSYSTEMS where TenentID=" + TTID + " and SystemID=TBLSYSTEMS.SystemID);";
                Str += "INSERT INTO [dbo].[ICEXTRACOST]([TenentID],[OVERHEADID],[OHNAME1],[OHNAME2],[OHNAME3],[ACCOUNTID],[Active],[CRUP_ID],[OHNAME],[OHNAMEO]) Select " + TTID + ",[OVERHEADID],[OHNAME1],[OHNAME2],[OHNAME3],[ACCOUNTID],[Active],[CRUP_ID],[OHNAME],[OHNAMEO] from ICEXTRACOST where TenentID=" + FromTID + " and OVERHEADID not in(select OVERHEADID from ICEXTRACOST where TenentID=" + TTID + " and OVERHEADID=ICEXTRACOST.OVERHEADID);";

                if (Str != "")
                {
                    command2 = new SqlCommand(Str, con);
                    con.Open();
                    command2.ExecuteReader();
                    con.Close();
                }
            }
            int RemoveCount = 0;
            int insertCount = CopyPrivilage.Count();
            if (ViewState["TempRemove"] != null)
            {
                List<Database.FUNCTION_MST> TempRemove = ((List<Database.FUNCTION_MST>)ViewState["TempRemove"]).ToList();
                RemoveCount = TempRemove.Count();
            }
            pnlErrorMsg.Visible = true;
            lblerrmsg.Text = "Copy data Successfully in <u style=\"color:green;font-weight:bold;\">Insert(" + insertCount + ")</u> & <u style=\"color:green;font-weight:bold;\">Remove(" + RemoveCount + ")</u>";
            ViewState["TempRemove"] = null;
        }

        public void usermaster(int TID, int PMI, int PRI, int Menu, DateTime MDT)
        {
            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = PMI;
            objPRIVILAGE_MENU.TenentID = TID;
            objPRIVILAGE_MENU.PRIVILEGE_ID = PRI;
            objPRIVILAGE_MENU.PRIVILAGEFOR = 3;
            objPRIVILAGE_MENU.LOCATION_ID = 1;
            objPRIVILAGE_MENU.MENU_ID = Menu;
            objPRIVILAGE_MENU.MASTER_ID = 0;
            objPRIVILAGE_MENU.MENU_LOCATION = "Separator";
            objPRIVILAGE_MENU.ACTIVETILLDATE = MDT;
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
            objPRIVILAGE_MENU.ActiveMenu = "Y";
            objPRIVILAGE_MENU.ActiveModule = "Y";
            objPRIVILAGE_MENU.Action = "Y";
            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
            DB.SaveChanges();
        }
        public void MenuMaster(int TID, int PMI, int PRI, int Menu, DateTime MDT)
        {
            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = PMI;
            objPRIVILAGE_MENU.TenentID = TID;
            objPRIVILAGE_MENU.PRIVILEGE_ID = PRI;
            objPRIVILAGE_MENU.PRIVILAGEFOR = 2;
            objPRIVILAGE_MENU.LOCATION_ID = 1;
            objPRIVILAGE_MENU.MENU_ID = Menu;
            objPRIVILAGE_MENU.MASTER_ID = 0;
            objPRIVILAGE_MENU.MENU_LOCATION = "Separator";
            objPRIVILAGE_MENU.ACTIVETILLDATE = MDT;
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
            objPRIVILAGE_MENU.ActiveMenu = "Y";
            objPRIVILAGE_MENU.ActiveModule = "Y";
            objPRIVILAGE_MENU.Action = "Y";
            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
            DB.SaveChanges();
        }
        public void RoleMaster(int TID, int PMI, int PRI, int Menu, DateTime MDT)
        {
            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();

            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = PMI;
            objPRIVILAGE_MENU.TenentID = TID;
            objPRIVILAGE_MENU.PRIVILEGE_ID = PRI;
            objPRIVILAGE_MENU.PRIVILAGEFOR = 1;
            objPRIVILAGE_MENU.LOCATION_ID = 1;
            objPRIVILAGE_MENU.MENU_ID = Menu;
            objPRIVILAGE_MENU.MASTER_ID = 0;
            objPRIVILAGE_MENU.MENU_LOCATION = "Separator";
            objPRIVILAGE_MENU.ACTIVETILLDATE = MDT;
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
            objPRIVILAGE_MENU.ActiveMenu = "Y";
            objPRIVILAGE_MENU.ActiveModule = "Y";
            objPRIVILAGE_MENU.Action = "Y";
            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
            DB.SaveChanges();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < fromListMenu.Items.Count(); i++)
            {
                CheckBox FromCheck = (CheckBox)fromListMenu.Items[i].FindControl("FromCheck");
                if (CheckBox1.Checked == true)
                {
                    FromCheck.Checked = true;
                }
                else
                {
                    FromCheck.Checked = false;
                }
            }

        }

        protected void ToListMenu_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            List<Database.FUNCTION_MST> RemovedMenu = ((List<Database.FUNCTION_MST>)ViewState["InputMenuu"]).ToList();
            if (e.CommandName == "DeleteMenu")
            {
                string[] ID = e.CommandArgument.ToString().Split('-');
                int MenuID = Convert.ToInt32(ID[0]);
                int ModuleID = Convert.ToInt32(ID[1]);

                Database.FUNCTION_MST Robj = RemovedMenu.Single(p => p.MENU_ID == MenuID && p.MODULE_ID == ModuleID);
                RemovedMenu.Remove(Robj);

                ToListMenu.DataSource = RemovedMenu;
                ToListMenu.DataBind();

                if (RemovedMenu.Count() > 0)
                    ViewState["InputMenuu"] = RemovedMenu;
                else
                    ViewState["InputMenuu"] = null;

                List<Database.FUNCTION_MST> TempRemove = new List<Database.FUNCTION_MST>();
                if (ViewState["TempRemove"] == null)
                {
                    TempRemove.Add(Robj);
                    ViewState["TempRemove"] = TempRemove;
                    FinalCopy.Enabled = true;
                }
                else
                {
                    TempRemove = ((List<Database.FUNCTION_MST>)ViewState["TempRemove"]).ToList();
                    if (TempRemove.Where(p => p.MENU_ID == MenuID && p.MODULE_ID == ModuleID).Count() < 1)
                    {
                        TempRemove.Add(Robj);
                        ViewState["TempRemove"] = TempRemove;
                        FinalCopy.Enabled = true;
                    }
                }
            }
        }





    }
}