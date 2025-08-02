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
    public partial class NewCompaniySetup : System.Web.UI.Page
    {
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        Database.CallEntities DB = new Database.CallEntities();
        // Database.CallEntities DB = new Database.CallEntities();
        List<CAT_MST> List = new List<CAT_MST>();

        bool flag = false;
        string languageId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string root = HostingEnvironment.ApplicationPhysicalPath;
            //String[] Files = Directory.GetFiles("D:\\Projects\\NewSaas1808\\Web\\", "*.aspx", SearchOption.AllDirectories);
            //foreach (String item in  Files)
            //    {
            //    Database.temp obj = new Database.temp();
            //    obj.Name = item;
            //    DB.temps.AddObject(obj);
            //    DB.SaveChanges();
            //}

            if (!IsPostBack)
            {
                Session["Pagename"] = "Suppliers";
                //  ModalPopupExtender4.Hide();
                //    ModalPopupExtender4.TargetControlID = "btntest4";
                FillContractorID();

                LblDate.Text = DateTime.Now.ToShortDateString();
                GridBind();
                binddata1();
                // redonlyfalse();
                // BindTitleData();

                if (flag == false)
                {
                    //  LastData();
                    flag = true;
                }
                Session["Pagename"] = "Suppliers";
                ViewState["tabs"] = "1";
                if (RDBONEMonth.Checked == true)
                {
                    txtFromDate.Text = DateTime.Now.ToShortDateString();
                    txtTillDate.Text = DateTime.Now.AddMonths(1).ToShortDateString();
                }

            }
        }
        protected override void InitializeCulture()
        {
            DataTable dt = new DataTable();

            string language = Request.Form["__EventTarget"];
            if (language != null)
            {
                if (language.EndsWith("Arabic") || language.EndsWith("France") || language.EndsWith("English"))
                {
                    if (!string.IsNullOrEmpty(language))
                    {
                        if (language.EndsWith("Arabic"))
                        {
                            languageId = "ar-KW";
                        }
                        else if (language.EndsWith("France"))
                        {
                            languageId = "fr";
                        }
                        else if (language.EndsWith("English"))
                        {
                            languageId = "en-US";
                        }

                        Session["Language"] = languageId;
                        SetCulture(languageId);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Session["Language"] as string))
                    {
                        languageId = Session["Language"].ToString();
                        SetCulture(languageId);
                    }
                }
            }

            if (Session["Language"] != null)
            {
                if (!Session["Language"].ToString().StartsWith(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)) SetCulture(Session["Language"].ToString());
            }

            base.InitializeCulture();
        }
        protected void SetCulture(string languageId)
        {
            Session["Language"] = languageId;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageId);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageId);
        }
        public void binddata1()
        {


        }
        public string getremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID).Remarks;
        }
        public void GridBind()
        {
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //GridView1.DataSource = DB.REFTABLE.Where(p => p.REFTYPE == "INDUSTRY" && p.REFSUBTYPE == "LIST");
            //GridView1.DataBind();
        }
        public void FillContractorID()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            Classes.EcommAdminClass.getdropdown(drpModulName, TID, "", "", "", "MODULE_MST");
            //select * from MODULE_MST where Parent_Module_id =0 

            //drpModulName.DataSource = DB.MODULE_MST.Where(P => P.ACTIVE_FLAG == "Y" && P.Parent_Module_id != 0);
            //drpModulName.DataTextField = "Module_Name";
            //drpModulName.DataValueField = "Module_Id";
            //drpModulName.DataBind();

            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
            //select * from TBLLOCATION where TenentID=1

            //drplocation.DataSource = DB.TBLLOCATIONs.Where(P => P.TenentID == TID && P.Active == "Y");
            //drplocation.DataTextField = "LOCNAME1";
            //drplocation.DataValueField = "LOCATIONID";
            //drplocation.DataBind();
            //drplocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Location--", "0"));

            Classes.EcommAdminClass.getdropdown(drpcompniy, TID, "HLY", "", "", "TBLCOMPANYSETUP");
            //select * from TBLCOMPANYSETUP where TenentID=1 and PHYSICALLOCID ='HLY'

            //drpcompniy.DataSource = DB.TBLCOMPANYSETUPs.Where(P => P.TenentID == 1 && P.Active == "Y");
            //drpcompniy.DataTextField = "COMPNAME1";
            //drpcompniy.DataValueField = "COMPID";
            //drpcompniy.DataBind();
            //drpcompniy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Company--", "0"));

            Classes.EcommAdminClass.getdropdown(drpcuntry, TID, "", "", "", "tblCOUNTRY");
            //select * from tblCOUNTRY where TenentID=1


            //drpcuntry.DataSource = DB.tblCOUNTRies.Where(P => P.TenentID == 1 && P.Active == "Y");
            //drpcuntry.DataTextField = "COUNAME1";
            //drpcuntry.DataValueField = "COUNTRYID";
            //drpcuntry.DataBind();
            //drpcuntry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Country--", "0"));


            Classes.EcommAdminClass.getdropdown(drpcompanttype, TID, "COMP", "COMPTYPE", "", "REFTABLE");
            //select * from REFTABLE where REFTYPE ='COMP' and REFSUBTYPE ='COMPTYPE'

            //drpcompanttype.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "COMP" && P.REFSUBTYPE == "COMPTYPE");
            //drpcompanttype.DataTextField = "REFNAME1";
            //drpcompanttype.DataValueField = "REFID";
            //drpcompanttype.DataBind();
            //drpcompanttype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Companiy Type--", "0"));
            Classes.EcommAdminClass.getdropdown(drpPackage, 0, "function", "financial", "", "REFTABLE");

        }
        public string getStste(int SID)
        {
            return DB.tblStates.SingleOrDefault(p => p.StateID == SID).MYNAME1;
        }
        public string getshocial(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID).REFNAME1;
        }
        public string getcompniy(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == CID).Count() > 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == CID).COMPNAME;
            else
                return "";

        }
        public string getcity(int GCID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == GCID).Count() > 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID).CITY;
            else
                return "";

        }
        public string getContactName(int ID)
        {
            int ConID = Convert.ToInt32(ID);
            string Name = "";
            if (DB.TBLCONTACTs.Where(p => p.ContactMyID == ConID).Count() > 0)
            {
                Database.TBLCONTACT obj_con = DB.TBLCONTACTs.Single(p => p.ContactMyID == ConID);
                Name = obj_con.PersName1;
            }
            return Name;
        }

        protected void drpModulName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int NewTID = Convert.ToInt32(ViewState["TID"]);
            int ID = Convert.ToInt32(drpModulName.SelectedValue);

            if (ID == 99999)// For All Module
            {
                int TID = 0;
                List<FUNCTION_MST> List = DB.FUNCTION_MST.OrderBy(m => m.MENU_ID).Where(p => p.ACTIVE_FLAG == 1 && p.TenentID == TID).ToList();
                //ListView1.DataSource = List;
                //ListView1.DataBind();
            }
            else
            {
                int TID = Convert.ToInt32(DB.MODULE_MST.Single(P => P.Module_Id == ID && P.TenentID == 0).TenentID);
                List<Database.FUNCTION_MST> List = DB.FUNCTION_MST.OrderBy(m => m.MENU_ID).Where(p => p.ACTIVE_FLAG == 1 && p.TenentID == TID && p.MODULE_ID == ID).ToList();
                //ListView1.DataSource = List;
                //ListView1.DataBind();
            }


            string ModuclName = drpModulName.SelectedItem.Text;
            ViewState["ModuclName"] = ModuclName;
            //ViewState["FUN"] = List;
            //ViewState["temp"] = Temp;
            // btncounty.Visible = true;
        }
        protected void drpPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(drpModulName.SelectedValue);
            int PAckID = Convert.ToInt32(drpPackage.SelectedValue);

            if (PAckID == 1005)
            {
                List<Database.MODULE_MST> ListSelectFull = DB.MODULE_MST.Where(p => p.TenentID == 0 && p.Parent_Module_id != 0 && p.Module_Id != 12 && p.Module_Id != 13).ToList();
                CHKSelectFull.DataSource = ListSelectFull;
                CHKSelectFull.DataTextField = "Module_Name";
                CHKSelectFull.DataValueField = "Module_Id";
                CHKSelectFull.DataBind();
                PnlSelectFull.Visible = true;
            }
            else
            {
                List<Database.PackageFunctionSetup> List1 = DB.PackageFunctionSetups.Where(p => p.PackageID == PAckID).ToList();
                List<Database.FUNCTION_MST> FUNCTION_MSTList = new List<FUNCTION_MST>();
                foreach (Database.PackageFunctionSetup item in List1)
                {
                    Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.TenentID == 0 && p.MENU_ID == item.FunMenuID);
                    FUNCTION_MSTList.Add(obj);
                }
                ListView1.DataSource = FUNCTION_MSTList.OrderBy(p => p.MENU_ORDER);
                ListView1.DataBind();

                for (int i = 0; i < ListView1.Items.Count; i++)
                {
                    if (PAckID == 1001 || PAckID == 1002 || PAckID == 1003)
                    {
                        CheckBox chk = (CheckBox)ListView1.Items[i].FindControl("cheList");
                        chk.Checked = true;
                        chk.Enabled = false;
                        ckbAll.Enabled = false;
                    }
                }
            }


            string ModuclName = drpModulName.SelectedItem.Text;
            ViewState["ModuclName"] = ModuclName;
        }
        protected void btnSelectFull_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(ViewState["TID"]);
            int numSelected = 0;
            foreach (ListItem li in CHKSelectFull.Items)
            {
                if (li.Selected)
                {
                    numSelected = numSelected + 1;
                }
            }
            List<Database.FUNCTION_MST> Final_FUNCTION_MSTList = new List<FUNCTION_MST>();
            List<Database.FUNCTION_MST> Temp_FUNCTION_MSTList = new List<FUNCTION_MST>();
            string selectedValue = "";
            int coo = 1;
            foreach (ListItem item in CHKSelectFull.Items)
            {
                int co = numSelected;
                if (item.Selected)
                {
                    string Value = item.Value;
                    if (coo == co)
                        selectedValue += Value;
                    else
                        selectedValue += Value + ",";
                    coo++;
                }
            }
            string[] svalue = selectedValue.ToString().Split(',');
            for (int i = 0; i < svalue.Count(); i++)
            {
                int MID = Convert.ToInt32(svalue[i]);
                Temp_FUNCTION_MSTList = DB.FUNCTION_MST.Where(p => p.MODULE_ID == MID).ToList();
                foreach (Database.FUNCTION_MST iitem in Temp_FUNCTION_MSTList)
                {
                    Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.MENU_ID == iitem.MENU_ID && p.MODULE_ID == MID);
                    Final_FUNCTION_MSTList.Add(obj);
                }
            }
            ListView1.DataSource = Final_FUNCTION_MSTList;
            ListView1.DataBind();

            for (int j = 0; j < svalue.Count(); j++)
            {
                int MMID = Convert.ToInt32(svalue[j]);
                if (ViewState["UID"] != null)
                {
                    string[] userr = ViewState["UID"].ToString().Split(',');
                    for (int K = 0; K < userr.Count(); K++)
                    {
                        int USERR = Convert.ToInt32(userr[K]);
                        int PrivilageID = DB.PRIVILEGE_MST.Single(p => p.MODULE_ID == MMID).PRIVILEGE_ID;
                        if (DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == USERR && p.MODULE_ID == MMID && p.PRIVILEGE_ID == PrivilageID).Count() > 0)
                        {

                        }
                        else
                        {
                            Database.MODULE_MAP OBJMap = new Database.MODULE_MAP();
                            OBJMap.TenentID = TID;
                            OBJMap.PRIVILEGE_ID = PrivilageID;
                            OBJMap.LOCATION_ID = 1;
                            OBJMap.MODULE_ID = MMID;
                            OBJMap.MODULE_MAP_ID = DB.MODULE_MAP.Count() > 0 ? Convert.ToInt32(DB.MODULE_MAP.Max(p => p.MODULE_MAP_ID) + 1) : 1;
                            OBJMap.UserID = USERR;
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
                            OBJMap.SP1Name = null;
                            OBJMap.SP2Name = null;
                            OBJMap.SP3Name = null;
                            OBJMap.SP4Name = null;
                            OBJMap.SP5Name = null;
                            if (DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == USERR).Count() == 0)
                                OBJMap.SP1Name = "DefaultSet";
                            DB.MODULE_MAP.AddObject(OBJMap);
                            DB.SaveChanges();
                        }
                    }
                }
            }
            for (int L = 0; L < svalue.Count(); L++)
            {
                int MMID = Convert.ToInt32(svalue[L]);
                if (ViewState["UID"] != null)
                {
                    string[] userr = ViewState["UID"].ToString().Split(',');
                    for (int N = 0; N < userr.Count(); N++)
                    {
                        int USERR = Convert.ToInt32(userr[N]);
                        int PrivilageID = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.MODULE_ID == MMID && p.UserID == USERR).PRIVILEGE_ID;
                        int RID = Convert.ToInt32(DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == USERR).USER_TYPE);
                        if (DB.USER_ROLE.Where(p => p.TenentID == TID && p.ROLE_ID == RID && p.USER_ID == USERR && p.PRIVILEGE_ID == PrivilageID).Count() > 0)
                        {

                        }
                        else
                        {
                            Database.USER_ROLE OBJrole = new Database.USER_ROLE();
                            OBJrole.TenentID = TID;
                            OBJrole.USER_ROLE_ID = DB.USER_ROLE.Count() > 0 ? Convert.ToInt32(DB.USER_ROLE.Max(p => p.USER_ROLE_ID) + 1) : 1;
                            OBJrole.PRIVILEGE_ID = PrivilageID;
                            OBJrole.LOCATION_ID = 1;
                            OBJrole.USER_ID = USERR;
                            OBJrole.ROLE_ID = RID;
                            OBJrole.ACTIVE_FLAG = "Y";
                            OBJrole.ACTIVE_FROM_DT = Convert.ToDateTime(txtFromDate.Text);
                            OBJrole.ACTIVE_TO_DT = Convert.ToDateTime(txtTillDate.Text);
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
                            if (DB.ROLE_MST.Where(p => p.TenentID == TID && p.ROLE_ID == RID).Count() == 0)
                            {
                                Database.ROLE_MST objEditROLE_MST = DB.ROLE_MST.Single(p => p.TenentID == 0 && p.ROLE_ID == RID);
                                Database.ROLE_MST objROLE_MST = new Database.ROLE_MST();
                                objROLE_MST.TenentID = TID;
                                objROLE_MST.ROLE_ID = RID;
                                objROLE_MST.ROLE_NAME = objEditROLE_MST.ROLE_NAME;
                                objROLE_MST.ROLE_NAME1 = objEditROLE_MST.ROLE_NAME1;
                                objROLE_MST.ROLE_NAME2 = objEditROLE_MST.ROLE_NAME2;
                                objROLE_MST.ROLE_DESC = objEditROLE_MST.ROLE_DESC;
                                objROLE_MST.ACTIVE_FLAG = "Y";
                                objROLE_MST.ACTIVE_FROM_DT = Convert.ToDateTime(txtFromDate.Text);
                                objROLE_MST.ACTIVE_TO_DT = Convert.ToDateTime(txtTillDate.Text);
                                objROLE_MST.ACTIVEROLE = true;
                                objROLE_MST.ROLLDATE = Convert.ToDateTime(txtTillDate.Text);

                                DB.ROLE_MST.AddObject(objROLE_MST);
                                DB.SaveChanges();
                            }

                        }
                    }
                }
            }
            if (ViewState["UID"] != null)
            {
                string[] alluser = ViewState["UID"].ToString().Split(',');
                for (int M = 0; M < alluser.Count(); M++)
                {
                    int pri_ID = 0;
                    int USERR = Convert.ToInt32(alluser[M]);
                    List<Database.MODULE_MAP> MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == USERR).ToList();
                    foreach (Database.MODULE_MAP item in MapList)
                    {
                        int privilageid = MapList[pri_ID].PRIVILEGE_ID;
                        if (DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == USERR && p.PRIVILEGE_ID == privilageid).Count() > 0)
                        {

                        }
                        else
                        {
                            Database.USER_RIGHTS Rightobj = new Database.USER_RIGHTS();
                            Rightobj.TenentID = TID;
                            Rightobj.RIGHTS_ID = DB.USER_RIGHTS.Count() > 0 ? Convert.ToInt32(DB.USER_RIGHTS.Max(p => p.RIGHTS_ID) + 1) : 1;
                            Rightobj.LOCATION_ID = 1;
                            Rightobj.USER_ID = USERR;
                            Rightobj.PRIVILEGE_ID = MapList[pri_ID].PRIVILEGE_ID;
                            Rightobj.ADD_FLAG = true;
                            Rightobj.MODIFY_FLAG = true;
                            Rightobj.DELETE_FLAG = true;
                            Rightobj.VIEW_FLAG = true;
                            Rightobj.ALL_FLAG = true;
                            Rightobj.CRUP_ID = 0;
                            Rightobj.IsLabelUpdate = true;
                            Rightobj.SP1 = true;
                            Rightobj.SP2 = true;
                            Rightobj.SP3 = true;
                            Rightobj.SP4 = true;
                            Rightobj.SP5 = true;
                            Rightobj.SP1Name = null;
                            Rightobj.SP2Name = null;
                            Rightobj.SP3Name = null;
                            Rightobj.SP4Name = null;
                            Rightobj.SP5Name = null;
                            Rightobj.status = "Final";
                            DB.USER_RIGHTS.AddObject(Rightobj);
                            DB.SaveChanges();
                        }
                        pri_ID++;
                    }
                }
            }

        }
        protected void btnNewComoany_Click(object sender, EventArgs e)
        {
            BindNewAddCompany();


        }
        public void BindNewAddCompany()
        {
            ////pnlAssignTenant.Style.Add("display", "block");
            //pnlCompnyExit.Style.Add("display", "block");//
            panFormUp.Style.Add("display", "block");
            pnlRegistration.Style.Add("display", "none");
            ButtonAdd.Visible = true;
            btndiscorohcost.Visible = true;
            txtAssignTenant.Enabled = true;
            txtAssignTenant.Text = DB.MYCOMPANYSETUPs.Count() > 0 ? (DB.MYCOMPANYSETUPs.Max(p => p.TenentID) + 1).ToString() : "1";
            //Comman Location
            drplocation.Enabled = false;
            drplocation.SelectedValue = "999";
        }

        protected void btnExitingCompany_Click(object sender, EventArgs e)
        {
            ////pnlAssignTenant.Style.Add("display", "block");
            ////pnlCompnyExit.Style.Add("display", "block");//
            ButtonAdd.Visible = false;
            btndiscorohcost.Visible = true;
            panFormUp.Style.Add("display", "none");
            panCompniDrp.Style.Add("display", "block");
            pnlRegistration.Style.Add("display", "none");
            txtAssignTenant.Enabled = false;

        }
        protected void btnNewRegistration_Click(object sender, EventArgs e)
        {
            ButtonAdd.Visible = false;
            ////pnlAssignTenant.Style.Add("display", "block");
            //pnlCompnyExit.Style.Add("display", "block");//
            panFormUp.Style.Add("display", "none");
            panCompniDrp.Style.Add("display", "none");
            pnlRegistration.Style.Add("display", "block");
            ListReg();
        }
        public void ListReg()
        {
            ListRegistration.DataSource = DB.NewCompaniySetup_Tryel.OrderByDescending(a => a.MyID);
            ListRegistration.DataBind();
        }
        public string Pack(int packID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == packID).Count() > 0)
            {
                string PackName = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == packID).REFNAME1;
                return PackName;
            }
            else
            {
                return "Not Used";
            }
        }

        protected void drpcompniy_SelectedIndexChanged(object sender, EventArgs e)
        {

            int PID = Convert.ToInt32(drpcompniy.SelectedValue);
            BindProdu(PID);
        }

        protected void btndiscorohcost_Click(object sender, EventArgs e)
        {
            ////pnlAssignTenant.Style.Add("display", "none");
            //pnlCompnyExit.Style.Add("display", "block");
            ButtonAdd.Visible = false;
            buttonUpdate.Visible = false;
        }

        protected void lkbCustomerN1_Click(object sender, EventArgs e)
        {
            List<TBLCOMPANYSETUP> list1 = DB.TBLCOMPANYSETUPs.Where(p => p.Active == "Y").ToList();
            List<MYCOMPANYSETUP> list2 = DB.MYCOMPANYSETUPs.ToList();
            string id1 = txtserchProduct.Text.ToString();

            list2 = list2.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper())
                || p.COMPNAME2.ToUpper().Contains(id1.ToUpper())
                || p.COMPNAME3.ToUpper().Contains(id1.ToUpper()))).ToList();
            if (list2.Count() > 0)
            {
                drpcompniy.DataSource = list2;
                drpcompniy.DataTextField = "COMPNAME1";
                drpcompniy.DataValueField = "COMPANYID";
                drpcompniy.DataBind();

                if (drpcompniy.SelectedValue == "0")
                {

                }
                else
                {
                    if (list2.Count() == 1)
                    {
                        int PID = Convert.ToInt32(drpcompniy.SelectedValue);
                        MyCompany(PID);
                    }
                }
            }
            else
            {
                list1 = list1.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper())
                || p.COMPNAME3.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())
                || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.STATE.ToUpper().Contains(id1.ToUpper()) || p.FAX.Contains(id1)
                || p.BUSPHONE1.Contains(id1) || p.MOBPHONE.Contains(id1)
                || p.ZIPCODE.Contains(id1) || p.POSTALCODE.Contains(id1)
                || p.CITY.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").ToList();//OrderBy(p => p.MYPRODID)
                drpcompniy.DataSource = list1;

                drpcompniy.DataTextField = "COMPNAME1";
                drpcompniy.DataValueField = "COMPID";
                drpcompniy.DataBind();
                if (drpcompniy.SelectedValue == "0")
                {

                }
                else
                {
                    if (list1.Count() == 1)
                    {
                        int PID = Convert.ToInt32(drpcompniy.SelectedValue);
                        BindProdu(PID);
                    }
                }
            }
            //
        }
        protected void btnsearchGO_Click(object sender, EventArgs e)
        {
            int PID = Convert.ToInt32(drpcompniy.SelectedValue);
            BindProdu(PID);
        }
        public void MyCompany(int CID)
        {
            buttonUpdate.Visible = true;
            ButtonAdd.Visible = false;
            panFormUp.Style.Add("display", "block");
            panCompniDrp.Style.Add("display", "none");
            Database.MYCOMPANYSETUP ObjMYCOMP = DB.MYCOMPANYSETUPs.Single(p => p.COMPANYID == CID);

            txtAssignTenant.Text = ObjMYCOMP.TenentID.ToString();
            drplocation.SelectedValue = ObjMYCOMP.PHYSICALLOCID;
            txtcompni1.Text = ObjMYCOMP.COMPNAME1;
            txtcompni2.Text = ObjMYCOMP.COMPNAME2;
            txtcompni3.Text = ObjMYCOMP.COMPNAME3;
            string COID = ObjMYCOMP.COUNTRYID.ToString();
            drpcuntry.SelectedValue = COID;//ObjMYCOMP.COUNTRYID.ToString();
            txtAddre1.Text = ObjMYCOMP.ADDR1;
            txtaddre2.Text = ObjMYCOMP.ADDR2;
            txtcity.Text = ObjMYCOMP.CITY;
            bindSates(COID);
            drpstate.SelectedValue = ObjMYCOMP.STATE;
            txtpostcod.Text = ObjMYCOMP.POSTALCODE;
            txtzipcod.Text = ObjMYCOMP.ZIPCODE;
            txtphon.Text = ObjMYCOMP.PHONE;
            txtfax.Text = ObjMYCOMP.FAX;
            txtarabic.Text = ObjMYCOMP.ARABIC;
            txtdcurreny.Text = ObjMYCOMP.DECIMALCURRENCY.ToString();
            txtreportDf.Text = ObjMYCOMP.REPORTDEFAULT;
            txtreportDire.Text = ObjMYCOMP.REPORTDIRECTORY;
            txtdatadirec.Text = ObjMYCOMP.DATADIRECTORY;
            txtbackdirect.Text = ObjMYCOMP.BACKUPDIRECTORY;
            txtexecutdirecto.Text = ObjMYCOMP.EXECUTABLEDIRECTORY;
            txtinvdatabsn.Text = ObjMYCOMP.INVDATABASENAME;
            txtactdata.Text = ObjMYCOMP.ACTDATABASENAME;

            drpcompanttype.SelectedValue = ObjMYCOMP.Companytype.ToString();
            txtAssignTenant.Enabled = false;
        }
        public void BindProdu(int PID)
        {
            buttonUpdate.Visible = true;
            ButtonAdd.Visible = false;
            panFormUp.Style.Add("display", "block");
            panCompniDrp.Style.Add("display", "none");
            Database.TBLCOMPANYSETUP Obj = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == PID);
            txtAssignTenant.Text = DB.MYCOMPANYSETUPs.Count() > 0 ? (DB.MYCOMPANYSETUPs.Max(p => p.TenentID) + 1).ToString() : "1";//Obj.TenentID.ToString();
            txtcompni1.Text = Obj.COMPNAME1;
            txtcompni2.Text = Obj.COMPNAME2;
            txtcompni3.Text = Obj.COMPNAME3;
            string COID = Obj.COUNTRYID.ToString();
            drpcuntry.SelectedValue = COID;//Obj.COUNTRYID.ToString();
            txtAddre1.Text = Obj.ADDR1;
            txtaddre2.Text = Obj.ADDR2;
            txtcity.Text = Obj.CITY;
            bindSates(COID);
            drpstate.SelectedValue = Obj.STATE;
            txtpostcod.Text = Obj.POSTALCODE;
            txtzipcod.Text = Obj.ZIPCODE;
            txtphon.Text = Obj.BUSPHONE1;
            txtfax.Text = Obj.FAX;
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == Obj.TenentID && p.COMPANYID == Obj.COMPID).Count() > 0)
            {
                Database.MYCOMPANYSETUP OBJCOM = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == Obj.TenentID && p.COMPANYID == Obj.COMPID);
                txtarabic.Text = OBJCOM.ARABIC;
                txtdcurreny.Text = OBJCOM.DECIMALCURRENCY.ToString();
                txtreportDf.Text = OBJCOM.REPORTDEFAULT;
                txtreportDire.Text = OBJCOM.REPORTDIRECTORY;
                txtdatadirec.Text = OBJCOM.DATADIRECTORY;
                txtbackdirect.Text = OBJCOM.BACKUPDIRECTORY;
                txtexecutdirecto.Text = OBJCOM.EXECUTABLEDIRECTORY;
                txtinvdatabsn.Text = OBJCOM.INVDATABASENAME;
                txtactdata.Text = OBJCOM.ACTDATABASENAME;
            }
            //drplocation.SelectedValue = Obj.PHYSICALLOCID;
            drpcompanttype.SelectedValue = Obj.CompanyType.ToString();
        }

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int PID = Convert.ToInt32(drpcompniy.SelectedValue);
            if (PID == 0)
                PID = Convert.ToInt32(ViewState["COMID"]);
            if (DB.MYCOMPANYSETUPs.Where(p => p.COMPANYID == PID).Count() == 1)
            {
                MYCOMPANYSETUP objMYCOMPANYSETUP = DB.MYCOMPANYSETUPs.Single(p => p.COMPANYID == PID);
                int Tenent = Convert.ToInt32(txtAssignTenant.Text);
                //if (txtAssignTenant.Text != "999999999");
                objMYCOMPANYSETUP.TenentID = Convert.ToInt32(txtAssignTenant.Text);
                objMYCOMPANYSETUP.COMPANYID = PID;
                objMYCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                objMYCOMPANYSETUP.COMPNAME = txtcompni1.Text;
                objMYCOMPANYSETUP.COMPNAMEO = txtcompni2.Text;
                objMYCOMPANYSETUP.COMPNAMECH = txtcompni3.Text;
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
                //objMYCOMPANYSETUP.USERID = UID.ToString();
                objMYCOMPANYSETUP.ENTRYDATE = DateTime.Now;
                objMYCOMPANYSETUP.ENTRYTIME = DateTime.Now;
                objMYCOMPANYSETUP.UPDTTIME = DateTime.Now;
                objMYCOMPANYSETUP.REC_RUNNING_SRL = 1;
                objMYCOMPANYSETUP.Approved = 0;
                if (avatarUploadd.HasFile)
                {
                    string path = avatarUploadd.FileName;
                    avatarUploadd.SaveAs(Server.MapPath("../assets/" + path));
                    objMYCOMPANYSETUP.LogotoDisplay = path;
                }
                objMYCOMPANYSETUP.Companytype = Convert.ToInt32(drpcompanttype.SelectedValue);
                DB.SaveChanges();

                int TTID = DB.MYCOMPANYSETUPs.Single(p => p.COMPANYID == PID).TenentID;
                Database.TBLCOMPANYSETUP objTBLCOMPANYSETUP = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == PID && p.TenentID == TTID);
                objTBLCOMPANYSETUP.TenentID = Tenent;
                objTBLCOMPANYSETUP.COMPID = Convert.ToInt32(objMYCOMPANYSETUP.COMPANYID);
                objTBLCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                objTBLCOMPANYSETUP.COMPNAME1 = txtcompni1.Text;
                objTBLCOMPANYSETUP.COMPNAME2 = txtcompni2.Text;
                objTBLCOMPANYSETUP.COMPNAME3 = txtcompni3.Text;
                objTBLCOMPANYSETUP.COUNTRYID = Convert.ToInt32(drpcuntry.SelectedValue);
                objTBLCOMPANYSETUP.ADDR1 = txtAddre1.Text;
                objTBLCOMPANYSETUP.ADDR2 = txtaddre2.Text;
                objTBLCOMPANYSETUP.CITY = txtcity.Text;
                objTBLCOMPANYSETUP.STATE = drpstate.SelectedValue;//txtstate.Text;
                objTBLCOMPANYSETUP.POSTALCODE = txtpostcod.Text;
                objTBLCOMPANYSETUP.ZIPCODE = txtzipcod.Text;
                objTBLCOMPANYSETUP.BUSPHONE1 = txtphon.Text;
                objTBLCOMPANYSETUP.MOBPHONE = txtphon.Text;
                objTBLCOMPANYSETUP.EMAIL = "NotUsed@gmail.com";
                objTBLCOMPANYSETUP.EMAIL1 = "NotUsed@gmail.com";
                objTBLCOMPANYSETUP.FAX = txtfax.Text;
                //objTBLCOMPANYSETUP.USERID = null;
                objTBLCOMPANYSETUP.UPDTTIME = DateTime.Now;
                objTBLCOMPANYSETUP.Approved = 0;
                objTBLCOMPANYSETUP.Active = "Y";
                objTBLCOMPANYSETUP.CompanyType = drpcompanttype.SelectedValue;
                DB.TBLCOMPANYSETUPs.AddObject(objTBLCOMPANYSETUP);
                DB.SaveChanges();

                ViewState["TID"] = Tenent;
            }
            else
            {
                Database.MYCOMPANYSETUP objMYCOMPANYSETUP = new Database.MYCOMPANYSETUP();
                int Tenent = 0;

                Tenent = Convert.ToInt32(txtAssignTenant.Text);
                if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == Tenent).Count() > 0)
                    Tenent = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID != 999999999).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID != 999999999).Max(p => p.TenentID) + 1) : 1;
                else
                    objMYCOMPANYSETUP.TenentID = Tenent;

                objMYCOMPANYSETUP.COMPANYID = PID;
                objMYCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                objMYCOMPANYSETUP.COMPNAME1 = txtcompni1.Text;
                objMYCOMPANYSETUP.COMPNAME2 = txtcompni2.Text;
                objMYCOMPANYSETUP.COMPNAME3 = txtcompni3.Text;
                objMYCOMPANYSETUP.COMPNAME = txtcompni1.Text;
                objMYCOMPANYSETUP.COMPNAMEO = txtcompni2.Text;
                objMYCOMPANYSETUP.COMPNAMECH = txtcompni3.Text;
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
                //objMYCOMPANYSETUP.USERID = UID.ToString();
                objMYCOMPANYSETUP.ENTRYDATE = DateTime.Now;
                objMYCOMPANYSETUP.ENTRYTIME = DateTime.Now;
                objMYCOMPANYSETUP.UPDTTIME = DateTime.Now;
                objMYCOMPANYSETUP.REC_RUNNING_SRL = 1;
                objMYCOMPANYSETUP.Approved = 0;
                if (avatarUploadd.HasFile)
                {
                    string path = avatarUploadd.FileName;
                    avatarUploadd.SaveAs(Server.MapPath("../assets/" + path));
                    objMYCOMPANYSETUP.LogotoDisplay = path;
                }
                objMYCOMPANYSETUP.Companytype = Convert.ToInt32(drpcompanttype.SelectedValue);
                DB.MYCOMPANYSETUPs.AddObject(objMYCOMPANYSETUP);
                DB.SaveChanges();

                Database.TBLCOMPANYSETUP objTBLCOMPANYSETUP = new Database.TBLCOMPANYSETUP();
                objTBLCOMPANYSETUP.TenentID = Tenent;
                objTBLCOMPANYSETUP.COMPID = Convert.ToInt32(objMYCOMPANYSETUP.COMPANYID);
                objTBLCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
                objTBLCOMPANYSETUP.COMPNAME1 = txtcompni1.Text;
                objTBLCOMPANYSETUP.COMPNAME2 = txtcompni2.Text;
                objTBLCOMPANYSETUP.COMPNAME3 = txtcompni3.Text;
                objTBLCOMPANYSETUP.COUNTRYID = Convert.ToInt32(drpcuntry.SelectedValue);
                objTBLCOMPANYSETUP.ADDR1 = txtAddre1.Text;
                objTBLCOMPANYSETUP.ADDR2 = txtaddre2.Text;
                objTBLCOMPANYSETUP.CITY = txtcity.Text;
                objTBLCOMPANYSETUP.STATE = drpstate.SelectedValue;//txtstate.Text;
                objTBLCOMPANYSETUP.POSTALCODE = txtpostcod.Text;
                objTBLCOMPANYSETUP.ZIPCODE = txtzipcod.Text;
                objTBLCOMPANYSETUP.BUSPHONE1 = txtphon.Text;
                objTBLCOMPANYSETUP.MOBPHONE = txtphon.Text;
                objTBLCOMPANYSETUP.EMAIL = "NotUsed@gmail.com";
                objTBLCOMPANYSETUP.EMAIL1 = "NotUsed@gmail.com";
                objTBLCOMPANYSETUP.FAX = txtfax.Text;
                //objTBLCOMPANYSETUP.USERID = null;
                objTBLCOMPANYSETUP.UPDTTIME = DateTime.Now;
                objTBLCOMPANYSETUP.Approved = 0;
                objTBLCOMPANYSETUP.Active = "Y";
                objTBLCOMPANYSETUP.CompanyType = drpcompanttype.SelectedValue;
                DB.TBLCOMPANYSETUPs.AddObject(objTBLCOMPANYSETUP);
                DB.SaveChanges();
                ViewState["TID"] = Tenent;


            }


            int MYID = Convert.ToInt32(ViewState["RMYID"]);
            if (DB.NewCompaniySetup_Tryel.Where(p => p.MyID == MYID).Count() > 0)
            {
                string NOOFUSER = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == MYID).NumberofUser.ToString();
                txtuserNumber.Text = NOOFUSER;
            }
            else
            {
                txtuserNumber.Enabled = true;
                txtuserNumber.Text = "1";
            }

            ViewState["CompnyName"] = txtcompni1.Text;
            ViewState["Tabs"] = 2;
            COnformTab1.Attributes.Add("aria-expanded", "false");
            COnformTab2.Attributes.Add("aria-expanded", "true");
            tab1.Attributes.Add("class", "tab-pane");
            tab2.Attributes.Add("class", "tab-pane active");
            tabCSL1.Attributes.Add("class", "");
            tabCSL2.Attributes.Add("class", "active");
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            int PID = DB.TBLCOMPANYSETUPs.Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Max(p => p.COMPID) + 1) : 1;
            Database.MYCOMPANYSETUP objMYCOMPANYSETUP = new Database.MYCOMPANYSETUP();
            objMYCOMPANYSETUP.TenentID = Convert.ToInt32(txtAssignTenant.Text);
            objMYCOMPANYSETUP.COMPANYID = PID;
            objMYCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
            objMYCOMPANYSETUP.COMPNAME1 = txtcompni1.Text;
            objMYCOMPANYSETUP.COMPNAME2 = txtcompni2.Text;
            objMYCOMPANYSETUP.COMPNAME3 = txtcompni3.Text;
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
            //objMYCOMPANYSETUP.USERID = null;
            objMYCOMPANYSETUP.ENTRYDATE = DateTime.Now;
            objMYCOMPANYSETUP.ENTRYTIME = DateTime.Now;
            objMYCOMPANYSETUP.UPDTTIME = DateTime.Now;
            objMYCOMPANYSETUP.REC_RUNNING_SRL = 1;
            objMYCOMPANYSETUP.Approved = 0;
            objMYCOMPANYSETUP.Companytype = Convert.ToInt32(drpcompanttype.SelectedValue);
            if (avatarUploadd.HasFile)
            {
                string path = avatarUploadd.FileName;
                avatarUploadd.SaveAs(Server.MapPath("../assets/" + path));
                objMYCOMPANYSETUP.LogotoDisplay = path;
            }
            DB.MYCOMPANYSETUPs.AddObject(objMYCOMPANYSETUP);
            DB.SaveChanges();
            Database.TBLCOMPANYSETUP objTBLCOMPANYSETUP = new Database.TBLCOMPANYSETUP();
            objTBLCOMPANYSETUP.TenentID = Convert.ToInt32(txtAssignTenant.Text);
            objTBLCOMPANYSETUP.COMPID = Convert.ToInt32(objMYCOMPANYSETUP.COMPANYID);
            objTBLCOMPANYSETUP.PHYSICALLOCID = drplocation.SelectedValue;
            objTBLCOMPANYSETUP.COMPNAME1 = txtcompni1.Text;
            objTBLCOMPANYSETUP.COMPNAME2 = txtcompni2.Text;
            objTBLCOMPANYSETUP.COMPNAME3 = txtcompni3.Text;
            objTBLCOMPANYSETUP.COUNTRYID = Convert.ToInt32(drpcuntry.SelectedValue);
            objTBLCOMPANYSETUP.ADDR1 = txtAddre1.Text;
            objTBLCOMPANYSETUP.ADDR2 = txtaddre2.Text;
            objTBLCOMPANYSETUP.CITY = txtcity.Text;
            objTBLCOMPANYSETUP.STATE = drpstate.SelectedValue;//txtstate.Text;
            objTBLCOMPANYSETUP.POSTALCODE = txtpostcod.Text;
            objTBLCOMPANYSETUP.ZIPCODE = txtzipcod.Text;
            objTBLCOMPANYSETUP.BUSPHONE1 = txtphon.Text;
            objTBLCOMPANYSETUP.MOBPHONE = txtphon.Text;
            objTBLCOMPANYSETUP.EMAIL = "NotUsed@gmail.com";
            objTBLCOMPANYSETUP.EMAIL1 = "NotUsed@gmail.com";
            objTBLCOMPANYSETUP.FAX = txtfax.Text;
            //objTBLCOMPANYSETUP.USERID = null;
            objTBLCOMPANYSETUP.UPDTTIME = DateTime.Now;
            objTBLCOMPANYSETUP.Approved = 0;
            objTBLCOMPANYSETUP.Active = "Y";
            objTBLCOMPANYSETUP.CompanyType = drpcompanttype.SelectedValue;
            DB.TBLCOMPANYSETUPs.AddObject(objTBLCOMPANYSETUP);
            DB.SaveChanges();

            int ID = Convert.ToInt32(ViewState["RMYID"]);
            Database.NewCompaniySetup_Tryel OBJtraial = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == ID);
            OBJtraial.TenentID = Convert.ToInt32(txtAssignTenant.Text);
            DB.SaveChanges();

            if (DB.NewCompaniySetup_Tryel.Where(p => p.MyID == ID).Count() > 0)
            {
                string NOOFUSER = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == ID).NumberofUser.ToString();
                txtuserNumber.Text = NOOFUSER;
            }
            else
            {
                txtuserNumber.Enabled = true;
                txtuserNumber.Text = "1";
            }

            ViewState["ComPNewID"] = PID;
            ViewState["CompnyName"] = txtcompni1.Text;
            ViewState["TID"] = txtAssignTenant.Text;
            ViewState["Tabs"] = 2;
            COnformTab1.Attributes.Add("aria-expanded", "false");
            COnformTab2.Attributes.Add("aria-expanded", "true");
            tab1.Attributes.Add("class", "tab-pane");
            tab2.Attributes.Add("class", "tab-pane active");
            tabCSL1.Attributes.Add("class", "");
            tabCSL2.Attributes.Add("class", "active");
            //            select* into tempreftable from REFTABLE where TenantID = 0
            //update tempreftable set TenantID = 604 where TenantID = 0
            //SELECT* FROM tempreftable;
            //            drop table tempreftable;

        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)ListView1.Items[i].FindControl("cheList");
                if (ckbAll.Checked == true)
                    chk.Checked = true;
                else
                    chk.Checked = false;
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(ViewState["TID"]);
            int mod = Convert.ToInt32(ViewState["MOD"]);
            string UserView = "";
            int usrCOUNT = 1;
            List<Database.MYCOMPANYSETUP> TempList = new List<Database.MYCOMPANYSETUP>();
            Database.MYCOMPANYSETUP objImageTable = new Database.MYCOMPANYSETUP();
            COnform.Visible = true;
            if (ViewState["CompnyName"] != null)
            {
                string CompnyName = ViewState["CompnyName"].ToString();
                //lblcompniyname.Text = CompnyName;
                labelUSerNAme.Text = CompnyName;
            }

            if (ViewState["ModuclName"] != null)
            {
                string Mod = ViewState["ModuclName"].ToString();
                //lblmodulname.Text = Mod;
                LabelUserAddresh.Text = Mod;
            }
            int Cunt = Convert.ToInt32(txtuserNumber.Text);
            for (int i = 0; i < Cunt; i++)
            {
                objImageTable = new Database.MYCOMPANYSETUP();
                TempList.Add(objImageTable);
            }
            listProductst.DataSource = TempList;
            listProductst.DataBind();

            if (DB.USER_MST.Where(p => p.TenentID == TID).Count() > 0 && ViewState["COMID"] != null)
            {
                int COMID = Convert.ToInt32(ViewState["COMID"]);

            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int K = 0; K < ListView2.Items.Count; K++)
                    {
                        int COU = ListView2.Items.Count();
                        TextBox txtuserNumber1 = (TextBox)ListView2.Items[K].FindControl("txtuserNumber1");
                        DropDownList ROLLLIST = (DropDownList)ListView2.Items[K].FindControl("drpRoll");
                        int RollID = Convert.ToInt32(ROLLLIST.SelectedValue);
                        string RollName = ROLLLIST.SelectedItem.ToString();
                        //Insert USER_DTL

                        int PID = 0;
                        if (ViewState["ComPNewID"] != null)
                        {
                            PID = Convert.ToInt32(ViewState["ComPNewID"]);
                        }
                        else if (ViewState["COMID"] != null)
                        {
                            PID = Convert.ToInt32(ViewState["COMID"]);
                        }
                        else
                        {
                            PID = Convert.ToInt32(drpcompniy.SelectedValue);
                        }
                        Database.MYCOMPANYSETUP objMYCOMPANYSETUP = DB.MYCOMPANYSETUPs.Single(p => p.COMPANYID == PID);
                        Database.USER_DTL OBJUSERDTL = new Database.USER_DTL();
                        OBJUSERDTL.TenentID = TID;
                        OBJUSERDTL.USER_DETAIL_ID = DB.USER_DTL.Count() > 0 ? Convert.ToInt32(DB.USER_DTL.Max(p => p.USER_DETAIL_ID) + 1) : 1;
                        OBJUSERDTL.LOCATION_ID = Convert.ToInt32(objMYCOMPANYSETUP.PHYSICALLOCID);
                        OBJUSERDTL.TITLE = "Not Found";
                        OBJUSERDTL.HOUSE_NO = "Not Found";
                        OBJUSERDTL.STREET = "Not Found";
                        OBJUSERDTL.ADDRESS1 = objMYCOMPANYSETUP.ADDR1.ToString();
                        OBJUSERDTL.ADDRESS2 = objMYCOMPANYSETUP.ADDR2.ToString();
                        OBJUSERDTL.CITY = objMYCOMPANYSETUP.CITY.ToString();
                        int Contryid = Convert.ToInt32(objMYCOMPANYSETUP.COUNTRYID);
                        var Con_Code = DB.tblCOUNTRies.Single(p => p.TenentID == 0 && p.COUNTRYID == Contryid).ITUTTelephoneCode;
                        OBJUSERDTL.COUNTRY = Contryid;
                        OBJUSERDTL.COUNTRY_CODE = Con_Code.ToString();
                        OBJUSERDTL.STATE = objMYCOMPANYSETUP.STATE.ToString();
                        OBJUSERDTL.ZIP = objMYCOMPANYSETUP.ZIPCODE.ToString();
                        OBJUSERDTL.PINCODE_NO = "Not Found";
                        OBJUSERDTL.POST_OFFICE = objMYCOMPANYSETUP.POSTALCODE.ToString();
                        OBJUSERDTL.EMAIL_ADDRESS = "Not Found";
                        OBJUSERDTL.MOBILE_NUM = Convert.ToDecimal(objMYCOMPANYSETUP.PHONE);
                        DB.USER_DTL.AddObject(OBJUSERDTL);
                        DB.SaveChanges();
                        int UserDTLID = OBJUSERDTL.USER_DETAIL_ID;
                        //Insert USER_MST Globle
                        //Database.USER_MST User1 = new Database.USER_MST();
                        //User1.TenentID = TID;
                        //User1.USER_ID = DB.USER_MST.Count() > 0 ? Convert.ToInt32(DB.USER_MST.Max(p => p.USER_ID) + 1) : 1;
                        //User1.LOCATION_ID = 1;
                        //User1.FIRST_NAME = txtuserNumber1.Text;
                        //User1.LAST_NAME = txtuserNumber1.Text;
                        //User1.FIRST_NAME1 = txtuserNumber1.Text;
                        //User1.LAST_NAME1 = txtuserNumber1.Text;
                        //User1.FIRST_NAME2 = txtuserNumber1.Text;
                        //User1.LAST_NAME2 = txtuserNumber1.Text;
                        //User1.LOGIN_ID = txtuserNumber1.Text;
                        //User1.PASSWORD = "S4vy0jf4g8o=";
                        //if (ViewState["RollID"] == null)
                        //{
                        //    User1.USER_TYPE = RollID;
                        //}
                        //else
                        //{
                        //    int Roll = Convert.ToInt32(ViewState["RollID"]);
                        //    User1.USER_TYPE = Roll;
                        //}
                        //User1.ACTIVE_FLAG = "Y";
                        //User1.USER_DETAIL_ID = UserDTLID;
                        //User1.USERDATE = Convert.ToDateTime(txtFromDate.Text);
                        //User1.Till_DT = Convert.ToDateTime(txtTillDate.Text);
                        //User1.ACTIVEUSER = true;
                        //User1.LAST_LOGIN_DT = DateTime.Now;
                        //User1.ACC_LOCK = "Y";
                        //User1.FIRST_TIME = "Y";
                        //DB.USER_MST.AddObject(User1);
                        //DB.SaveChanges();
                        ///////////////////
                        int UserTenent = TID;
                        int Userid = DB.USER_MST.Count() > 0 ? Convert.ToInt32(DB.USER_MST.Max(p => p.USER_ID) + 1) : 1;
                        int Locationid = 1;
                        string FIRST_NAME = txtuserNumber1.Text;
                        string LAST_NAME = txtuserNumber1.Text;
                        string FIRST_NAME1 = txtuserNumber1.Text;
                        string LAST_NAME1 = txtuserNumber1.Text;
                        string FIRST_NAME2 = txtuserNumber1.Text;
                        string LAST_NAME2 = txtuserNumber1.Text;
                        string LOGIN_ID = txtuserNumber1.Text;
                        string PASSWORD = "S4vy0jf4g8o=";
                        string PASSWORD_CHNG = "12345";
                        int USER_TYPE = RollID;
                        string REMARKS = txtuserNumber1.Text;
                        string ACTIVE_FLAG = "Y";
                        DateTime LAST_LOGIN_DT = DateTime.Now;
                        string ACC_LOCK = "N";
                        string FIRST_TIME = "N";
                        string EmailAddress = null;
                        string Avtar = null;
                        DateTime Till_DT = Convert.ToDateTime(txtTillDate.Text);
                        int CompId = PID;
                        int USER_DETAIL_ID = UserDTLID;
                        bool ACTIVEUSER = true;
                        DateTime USERDATE = Convert.ToDateTime(txtFromDate.Text);
                        string Lang1 = "en-US";
                        string Flage = "ADD";
                        Classes.ACMClass.InsertWeb_User_MST(UserTenent, Userid, Locationid, FIRST_NAME, LAST_NAME, FIRST_NAME1, LAST_NAME1, FIRST_NAME2, LAST_NAME2, LOGIN_ID, PASSWORD, PASSWORD_CHNG, USER_TYPE, REMARKS, ACTIVE_FLAG, LAST_LOGIN_DT, ACC_LOCK, FIRST_TIME, EmailAddress, Avtar, Till_DT, CompId, USER_DETAIL_ID, ACTIVEUSER, USERDATE, Flage, Lang1);
                        ///////////////////                       
                        int UserID = Userid;//User1.USER_ID;
                        if (usrCOUNT == COU)
                            UserView += UserID.ToString();
                        else
                            UserView += UserID.ToString() + ",";
                        ViewState["UID"] = UserView;
                        if (usrCOUNT == 1)
                        {
                            if (objMYCOMPANYSETUP.USERID == null)
                            {
                                objMYCOMPANYSETUP.USERID = UserID.ToString();
                                objMYCOMPANYSETUP.TenantGroupID = TID;
                                DB.SaveChanges();
                            }
                        }
                        usrCOUNT++;

                        //int ModID = Convert.ToInt32(DB.MODULE_MST.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y" && p.Parent_Module_id != 0).First().Module_Id);
                        ////Insert MODULE_MAP
                        //Database.MODULE_MAP ObjItemMAP = new Database.MODULE_MAP();
                        //ObjItemMAP.TenentID = TID;
                        //ObjItemMAP.MODULE_MAP_ID = DB.MODULE_MAP.Count() > 0 ? Convert.ToInt32(DB.MODULE_MAP.Max(p => p.MODULE_MAP_ID) + 1) : 1;
                        //ObjItemMAP.PRIVILEGE_ID = 3;
                        //ObjItemMAP.LOCATION_ID = 1;
                        //ObjItemMAP.MODULE_ID = ModID;
                        //ObjItemMAP.UserID = UserID;
                        //ObjItemMAP.ACTIVE_FLAG = "Y";
                        //ObjItemMAP.TenantID = 1;
                        //ObjItemMAP.CRUP_ID = 1;
                        //ObjItemMAP.MySerial = 999999999;
                        //ObjItemMAP.ALL_FLAG = "Y";
                        //ObjItemMAP.ADD_FLAG = "Y";
                        //ObjItemMAP.MODIFY_FLAG = "Y";
                        //ObjItemMAP.DELETE_FLAG = "Y";
                        //ObjItemMAP.VIEW_FLAG = "Y";
                        //ObjItemMAP.SP1Name = "DefaultSet";
                        //DB.MODULE_MAP.AddObject(ObjItemMAP);
                        //DB.SaveChanges();
                        //Insert ROLE_MST
                        //Database.ROLE_MST ObjRoll = new Database.ROLE_MST();
                        //ObjRoll.TenentID = TID;
                        //ObjRoll.ROLE_ID = RollID;
                        //ObjRoll.ROLE_NAME = RollName;
                        //ObjRoll.ROLE_NAME1 = RollName;
                        //ObjRoll.ROLE_NAME2 = RollName;
                        //ObjRoll.ROLE_DESC = RollName;
                        //ObjRoll.ACTIVE_FLAG = "Y";
                        //ObjRoll.ACTIVE_FROM_DT = DateTime.Now;
                        //ObjRoll.ACTIVE_TO_DT = DateTime.Now;
                        //ObjRoll.ACTIVEROLE = true;
                        //ObjRoll.ROLLDATE = Convert.ToDateTime(txtTillDate.Text);
                        //ObjRoll.ERP_TENANT_ID = 1;
                        //ObjRoll.CRUP_ID = 1;
                        //DB.ROLE_MST.AddObject(ObjRoll);
                        //DB.SaveChanges();

                        if (DB.TenantGroups.Where(p => p.TenentID == TID).Count() == 0)
                        {
                            Database.TenantGroup OBJTGroup = new Database.TenantGroup();
                            OBJTGroup.MainTenantID = 0;
                            OBJTGroup.TenentID = TID;
                            OBJTGroup.COMPNAME1 = objMYCOMPANYSETUP.COMPNAME1;
                            OBJTGroup.COMPNAME2 = objMYCOMPANYSETUP.COMPNAME2;
                            OBJTGroup.COMPNAME3 = objMYCOMPANYSETUP.COMPNAME3;
                            OBJTGroup.COUNTRYID = objMYCOMPANYSETUP.COUNTRYID;
                            OBJTGroup.ADDR1 = objMYCOMPANYSETUP.ADDR1;
                            OBJTGroup.ADDR2 = objMYCOMPANYSETUP.ADDR2;
                            OBJTGroup.CITY = objMYCOMPANYSETUP.CITY;
                            OBJTGroup.STATE = objMYCOMPANYSETUP.STATE;
                            OBJTGroup.POSTALCODE = objMYCOMPANYSETUP.POSTALCODE;
                            OBJTGroup.ZIPCODE = objMYCOMPANYSETUP.ZIPCODE;
                            OBJTGroup.PHONE = objMYCOMPANYSETUP.PHONE;
                            OBJTGroup.FAX = objMYCOMPANYSETUP.FAX;
                            OBJTGroup.ARABIC = objMYCOMPANYSETUP.ARABIC;
                            OBJTGroup.DECIMALCURRENCY = objMYCOMPANYSETUP.DECIMALCURRENCY;
                            OBJTGroup.REPORTDEFAULT = objMYCOMPANYSETUP.REPORTDEFAULT;
                            OBJTGroup.REPORTDIRECTORY = objMYCOMPANYSETUP.REPORTDIRECTORY;
                            OBJTGroup.DATADIRECTORY = objMYCOMPANYSETUP.DATADIRECTORY;
                            OBJTGroup.BACKUPDIRECTORY = objMYCOMPANYSETUP.BACKUPDIRECTORY;
                            OBJTGroup.EXECUTABLEDIRECTORY = objMYCOMPANYSETUP.EXECUTABLEDIRECTORY;
                            OBJTGroup.INVDATABASENAME = objMYCOMPANYSETUP.INVDATABASENAME;
                            OBJTGroup.ACTDATABASENAME = objMYCOMPANYSETUP.ACTDATABASENAME;
                            OBJTGroup.Language1 = objMYCOMPANYSETUP.Language1;
                            OBJTGroup.Language2 = objMYCOMPANYSETUP.Language2;
                            OBJTGroup.Language3 = objMYCOMPANYSETUP.Language3;
                            OBJTGroup.USERID = UserID.ToString();
                            OBJTGroup.ENTRYDATE = objMYCOMPANYSETUP.ENTRYDATE;
                            OBJTGroup.ENTRYTIME = objMYCOMPANYSETUP.ENTRYTIME;
                            OBJTGroup.UPDTTIME = objMYCOMPANYSETUP.UPDTTIME;
                            OBJTGroup.REC_RUNNING_SRL = objMYCOMPANYSETUP.REC_RUNNING_SRL;
                            OBJTGroup.CRUP_ID = objMYCOMPANYSETUP.CRUP_ID;
                            DB.TenantGroups.AddObject(OBJTGroup);
                            DB.SaveChanges();
                        }
                        //List<Database.tempUser> ItemNewTenent = DB.tempUsers.Where(p => p.TenentID == TID).ToList();
                        //foreach (Database.tempUser NewItem in ItemNewTenent)
                        //{
                        //    Database.tempUser OBJNewItem = DB.tempUsers.Single(p => p.TenentID == TID && p.MENUID == NewItem.MENUID && p.LocationID == NewItem.LocationID);
                        //    OBJNewItem.UserID = UserID;
                        //    if (ViewState["RollID"] == null)
                        //        OBJNewItem.ROLE_ID = RollID;
                        //    else
                        //        OBJNewItem.ROLE_ID = Convert.ToInt32(ViewState["RollID"]);
                        //    DB.SaveChanges();
                        //}
                    }
                    scope.Complete();
                }
                //string Str = "";
                //if (DB.TBLLOCATIONs.Where(p=>p.TenentID == TID).Count() == 0)
                //Str += "INSERT INTO [dbo].[TBLLOCATION]([TenentID],[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID]) SELECT " + TID + ",[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID] from [TBLLOCATION] where [TenentID]=0;";
                //if (DB.REFTABLEs.Where(p => p.TenentID == TID).Count() == 0)
                //Str += "INSERT INTO [dbo].[REFTABLE]([TenentID],[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate]) Select " + TID + ",[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate] from [REFTABLE] where [TenentID]=0;";
                //command2 = new SqlCommand(Str, con);
                //con.Open();
                //command2.ExecuteReader();
                //con.Close();
            }

            ViewState["Tabs3"] = 3;
            COnformTab2.Attributes.Add("aria-expanded", "false");
            COnformTab3.Attributes.Add("aria-expanded", "true");
            tab2.Attributes.Add("class", "tab-pane");
            tab3.Attributes.Add("class", "tab-pane active");
            tabCSL2.Attributes.Add("class", "");
            tabCSL3.Attributes.Add("class", "active");
            //Listuser.DataSource = TempList;
            //Listuser.DataBind();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            List<Database.tempUser> objTemp = DB.tempUsers.Where(p => p.TenentID == 0).ToList(); //((List<Database.tempUser>)ViewState["temp"]).ToList(); //ViewState["temp"] = Temp;
            List<Database.FUNCTION_MST> Fun = DB.FUNCTION_MST.Where(p => p.TenentID == 0).ToList(); //((List<Database.FUNCTION_MST>)ViewState["FUN"]).ToList(); //ViewState["FUN"] = List
            List<Database.FUNCTION_MST> tempfuction = new List<Database.FUNCTION_MST>();
            Database.FUNCTION_MST objFUNCTION_MST = new Database.FUNCTION_MST();
            int PackID = Convert.ToInt32(drpPackage.SelectedValue);
            int TID = Convert.ToInt32(ViewState["TID"]);
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                CheckBox cheList = (CheckBox)ListView1.Items[i].FindControl("cheList");
                Label lblMENU_NAME1 = (Label)ListView1.Items[i].FindControl("lblMENU_NAME1");
                Label lblMenu = (Label)ListView1.Items[i].FindControl("lblMenu");
                int MinID = Convert.ToInt32(lblMenu.Text);
                if (cheList.Checked == true)
                {
                    Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.MENU_ID == MinID);
                    tempfuction.Add(obj);
                }
            }
            // For Module Privilage Wise insert data in Privilage_Menu
            List<Database.PRIVILAGE_MENUDemon> Privilage_MenuList = DB.PRIVILAGE_MENUDemon.Where(p => p.TenentID == TID).ToList();
            string[] alluser = ViewState["UID"].ToString().Split(',');
            for (int M = 0; M < alluser.Count(); M++)
            {
                int USERR = Convert.ToInt32(alluser[M]);
                List<Database.MODULE_MAP> MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == USERR).ToList();
                foreach (Database.MODULE_MAP item in MapList)
                {
                    int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                    int ModuleID = Convert.ToInt32(item.MODULE_ID);
                    foreach (Database.FUNCTION_MST temfunitem in tempfuction.Where(p => p.MODULE_ID == ModuleID))
                    {
                        if (Privilage_MenuList.Where(p => p.PRIVILEGE_MENU_ID == ModuleID && p.TenentID == TID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 2 && p.MENU_ID == temfunitem.MENU_ID).Count() == 0)
                        {
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = ModuleID;
                            objPRIVILAGE_MENU.TenentID = TID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 2;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = temfunitem.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = temfunitem.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = temfunitem.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = temfunitem.MENUDATE;
                            objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                            objPRIVILAGE_MENU.ALL_FLAG = "Y";
                            objPRIVILAGE_MENU.ADD_FLAG = "Y";
                            objPRIVILAGE_MENU.MODIFY_FLAG = "Y";
                            objPRIVILAGE_MENU.DELETE_FLAG = "Y";
                            objPRIVILAGE_MENU.VIEW_FLAG = "Y";
                            objPRIVILAGE_MENU.LABEL_FLAG = "Y";
                            objPRIVILAGE_MENU.SP1 = "N";
                            objPRIVILAGE_MENU.SP2 = "Y";
                            objPRIVILAGE_MENU.SP3 = "N";
                            objPRIVILAGE_MENU.SP4 = "N";
                            objPRIVILAGE_MENU.SP5 = "N";
                            objPRIVILAGE_MENU.ActiveMenu = "Y";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                            Privilage_MenuList.Add(objPRIVILAGE_MENU);
                        }
                    }
                }
            }
            //For Role privilage wise insert Data in privilage_Menu
            for (int L = 0; L < alluser.Count(); L++)
            {
                int USERR = Convert.ToInt32(alluser[L]);
                List<Database.USER_ROLE> RoleList = DB.USER_ROLE.Where(p => p.TenentID == TID && p.USER_ID == USERR).ToList();
                foreach (Database.USER_ROLE item in RoleList)
                {
                    int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                    int Roleid = Convert.ToInt32(item.ROLE_ID);
                    foreach (Database.FUNCTION_MST temfunitem in tempfuction)
                    {
                        if (Privilage_MenuList.Where(p => p.PRIVILEGE_MENU_ID == Roleid && p.TenentID == TID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 1 && p.MENU_ID == temfunitem.MENU_ID).Count() == 0)
                        {
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = Roleid;
                            objPRIVILAGE_MENU.TenentID = TID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 1;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = temfunitem.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = temfunitem.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = temfunitem.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = temfunitem.MENUDATE;
                            objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                            objPRIVILAGE_MENU.ALL_FLAG = "Y";
                            objPRIVILAGE_MENU.ADD_FLAG = "Y";
                            objPRIVILAGE_MENU.MODIFY_FLAG = "Y";
                            objPRIVILAGE_MENU.DELETE_FLAG = "Y";
                            objPRIVILAGE_MENU.VIEW_FLAG = "Y";
                            objPRIVILAGE_MENU.LABEL_FLAG = "Y";
                            objPRIVILAGE_MENU.SP1 = "N";
                            objPRIVILAGE_MENU.SP2 = "Y";
                            objPRIVILAGE_MENU.SP3 = "N";
                            objPRIVILAGE_MENU.SP4 = "N";
                            objPRIVILAGE_MENU.SP5 = "N";
                            objPRIVILAGE_MENU.ActiveMenu = "Y";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.Action = "Y";
                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                            Privilage_MenuList.Add(objPRIVILAGE_MENU);
                        }
                    }
                }
            }
            // For user privilage wise insert data in privilage_Menu
            for (int o = 0; o < alluser.Count(); o++)
            {
                int USERR = Convert.ToInt32(alluser[o]);
                List<Database.USER_RIGHTS> RIGHTSist = DB.USER_RIGHTS.Where(p => p.TenentID == TID && p.USER_ID == USERR).ToList();
                foreach (Database.USER_RIGHTS item in RIGHTSist)
                {
                    int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                    int userid = Convert.ToInt32(item.USER_ID);
                    foreach (Database.FUNCTION_MST temfunitem in tempfuction)
                    {
                        if (Privilage_MenuList.Where(p => p.PRIVILEGE_MENU_ID == userid && p.TenentID == TID && p.PRIVILEGE_ID == PrivilageID && p.PRIVILAGEFOR == 3 && p.MENU_ID == temfunitem.MENU_ID).Count() == 0)
                        {
                            PRIVILAGE_MENUDemon objPRIVILAGE_MENU = new PRIVILAGE_MENUDemon();
                            objPRIVILAGE_MENU.PRIVILEGE_MENU_ID = userid;
                            objPRIVILAGE_MENU.TenentID = TID;
                            objPRIVILAGE_MENU.PRIVILEGE_ID = PrivilageID;
                            objPRIVILAGE_MENU.PRIVILAGEFOR = 3;
                            objPRIVILAGE_MENU.LOCATION_ID = 1;
                            objPRIVILAGE_MENU.MENU_ID = temfunitem.MENU_ID;
                            objPRIVILAGE_MENU.MASTER_ID = temfunitem.MASTER_ID;
                            objPRIVILAGE_MENU.MENU_LOCATION = temfunitem.MENU_LOCATION;
                            objPRIVILAGE_MENU.ACTIVETILLDATE = temfunitem.MENUDATE;
                            objPRIVILAGE_MENU.ACTIVE_FLAG = "Y";
                            objPRIVILAGE_MENU.ALL_FLAG = "Y";
                            objPRIVILAGE_MENU.ADD_FLAG = "Y";
                            objPRIVILAGE_MENU.MODIFY_FLAG = "Y";
                            objPRIVILAGE_MENU.DELETE_FLAG = "Y";
                            objPRIVILAGE_MENU.VIEW_FLAG = "Y";
                            objPRIVILAGE_MENU.LABEL_FLAG = "Y";
                            objPRIVILAGE_MENU.SP1 = "N";
                            objPRIVILAGE_MENU.SP2 = "Y";
                            objPRIVILAGE_MENU.SP3 = "N";
                            objPRIVILAGE_MENU.SP4 = "N";
                            objPRIVILAGE_MENU.SP5 = "Y";
                            objPRIVILAGE_MENU.ActiveMenu = "Y";
                            objPRIVILAGE_MENU.ActiveModule = "Y";
                            objPRIVILAGE_MENU.Action = "Y";
                            objPRIVILAGE_MENU.CreatedDate = DateTime.Now;
                            DB.PRIVILAGE_MENUDemon.AddObject(objPRIVILAGE_MENU);
                            DB.SaveChanges();
                            Privilage_MenuList.Add(objPRIVILAGE_MENU);
                        }
                    }
                }
            }
            // insert record in tempuser
            for (int p = 0; p < alluser.Count(); p++)
            {
                int USERR = Convert.ToInt32(alluser[p]);
                List<Database.MODULE_MAP> MapList = DB.MODULE_MAP.Where(a => a.TenentID == TID && a.UserID == USERR).ToList();
                foreach (Database.MODULE_MAP item in MapList)
                {
                    int PrivilageID = Convert.ToInt32(item.PRIVILEGE_ID);
                    int ModuleID = Convert.ToInt32(item.MODULE_ID);
                    int ridd = DB.USER_ROLE.Single(a => a.TenentID == TID && a.PRIVILEGE_ID == PrivilageID && a.USER_ID == USERR).ROLE_ID;
                    GlobleClass.DeleteTempUser(TID, USERR, 1, ModuleID);
                    GlobleClass.getMenuGloble(TID, USERR, 1, ModuleID, ridd);
                }
            }
            string Str = "";
            if (DB.TBLLOCATIONs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLLOCATION]([TenentID],[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID]) SELECT " + TID + ",[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID] from [TBLLOCATION] where [TenentID]=0;";
            if (DB.REFTABLEs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[REFTABLE]([TenentID],[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate]) Select " + TID + ",[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate] from [REFTABLE] where [TenentID]=0;";
            if (DB.tblCOUNTRies.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[tblCOUNTRY]([TenentID],[COUNTRYID],[REGION1],[COUNAME1],[COUNAME2],[COUNAME3],[CAPITAL],[NATIONALITY1],[NATIONALITY2],[NATIONALITY3],[CURRENCYNAME1],[CURRENCYNAME2],[CURRENCYNAME3],[CURRENTCONVRATE],[CURRENCYSHORTNAME1],[CURRENCYSHORTNAME2],[CURRENCYSHORTNAME3],[CountryType],[CountryTSubType],[Sovereignty],[ISO4217CurCode],[ISO4217CurName],[ITUTTelephoneCode],[FaxLength],[TelLength],[ISO3166_1_2LetterCode],[ISO3166_1_3LetterCode],[ISO3166_1Number],[IANACountryCodeTLD],[Active],[CRUP_ID]) select " + TID + ",[COUNTRYID],[REGION1],[COUNAME1],[COUNAME2],[COUNAME3],[CAPITAL],[NATIONALITY1],[NATIONALITY2],[NATIONALITY3],[CURRENCYNAME1],[CURRENCYNAME2],[CURRENCYNAME3],[CURRENTCONVRATE],[CURRENCYSHORTNAME1],[CURRENCYSHORTNAME2],[CURRENCYSHORTNAME3],[CountryType],[CountryTSubType],[Sovereignty],[ISO4217CurCode],[ISO4217CurName],[ITUTTelephoneCode],[FaxLength],[TelLength],[ISO3166_1_2LetterCode],[ISO3166_1_3LetterCode],[ISO3166_1Number],[IANACountryCodeTLD],[Active],[CRUP_ID] from [tblCOUNTRY] where [TenentID]=0;";
            if (DB.TBLCOLORs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLCOLOR]([TenentID],[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active]) select " + TID + ",[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active] from [TBLCOLOR] where TenentID=0;";
            if (DB.TBLSIZEs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLSIZE]([TenentID],[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE])select " + TID + ",[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE] from [TBLSIZE] where TenentID=0;";
            if (DB.ICUOMs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[ICUOM]([TenentID],[UOM],[UOMNAMESHORT],[UOMNAME1],[UOMNAME2],[UOMNAME3],[REMARKS],[CRUP_ID],[Active],[UOMNAME],[UOMNAMEO])VALUES(" + TID + ",99999,'Not Used','Not Used','Not Used','Not Used','Not Used',0,'Y','Not Used','Not Used');";
            if (DB.CAT_MST.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[CAT_MST]([TenentID],[CATID],[PARENT_CATID],[DefaultPic],[SHORT_NAME],[CAT_NAME1],[CAT_NAME2],[CAT_NAME3],[CAT_DESCRIPTION],[CAT_TYPE],[WARRANTY],[CRUP_ID],[Active],[SupplierPercent]) VALUES (" + TID + ",99999,0,'cc','Not Used','Not Used','Not Used','Not Used','Not Used','WEBSALE','0 Months',0,'1',0.0);";
            if (DB.TBLGROUPs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLGROUP]([TenentID],[LocationId],[ITGROUPID],[GroupType],[ITGROUPDESC1],[ITGROUPDESC2],[ITGROUPREMARKS],[ACTIVE_Flag],[USERCODE],[GROUPID],[remarks],[ACTIVE],[CRUP_ID],[Infastructure]) VALUES (" + TID + ",1,999999999,'','Not Used','Not Used','Not Used','True','','','','1',0,'False');";
            if (DB.DEPTOFSales.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[DEPTOFSale]([TenentID],[SalDeptID],[DeptDesc1],[DeptDesc2],[DeptDesc3],[DeptRemarks],[SalesAccountID],[Margin],[ExpenseAccountID],[PurchaseAccountID],[Active_Flag],[CRUP_ID],[DeptManagerID]) VALUES (" + TID + ",999999999,'Not Exist Yet','Not Exist Yet','Not Exist Yet','Not Exist Yet','0',0.0,'','','True',0,0);";
            if (DB.MYBUS.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[MYBUS]([TenentID],[BUSID],[BUSTYPE],[SHORTNAME],[BUSNAME1],[BUSNAME2],[BUSNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[Remarks],[COMPANID],[BUSNAME],[BUSNAMEO],[BUSNAMCHIN],[BUSYPE],[CRUP_ID]) Select " + TID + ",[BUSID],[BUSTYPE],[SHORTNAME],[BUSNAME1],[BUSNAME2],[BUSNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[Remarks],[COMPANID],[BUSNAME],[BUSNAMEO],[BUSNAMCHIN],[BUSYPE],[CRUP_ID] from [MYBUS] Where TenentID=0;";
            if (DB.RefLabelMSTs.Where(p => p.TenentID == TID).Count() == 0)
                Str += "INSERT INTO [dbo].[RefLabelMST]([TenentID],[RefLabelID],[RefType],[RefSubType],[LE1],[LE2],[LE3],[LE4],[LE5],[LE6],[LE7],[LE8],[LE9],[LE10],[LF1],[LF2],[LF3],[LF4],[LF5],[LF6],[LF7],[LF8],[LF9],[LF10],[LA1],[LA2],[LA3],[LA4],[LA5],[LA6],[LA7],[LA8],[LA9],[LA10]) Select " + TID + ",[RefLabelID],[RefType],[RefSubType],[LE1],[LE2],[LE3],[LE4],[LE5],[LE6],[LE7],[LE8],[LE9],[LE10],[LF1],[LF2],[LF3],[LF4],[LF5],[LF6],[LF7],[LF8],[LF9],[LF10],[LA1],[LA2],[LA3],[LA4],[LA5],[LA6],[LA7],[LA8],[LA9],[LA10] from [RefLabelMST] where TenentID=0;";
            if (DB.tbltranstypes.Where(p => p.TenentID == TID).Count() == 0)
                Str += "select * into TempCopy_tbltranstype from tbltranstype where TenentID = 0;update TempCopy_tbltranstype set TenentID = " + TID + " where TenentID = 0;INSERT INTO tbltranstype select * from TempCopy_tbltranstype where TenentID = " + TID + ";drop table TempCopy_tbltranstype;";
            if (DB.tbltranssubtypes.Where(p => p.TenentID == TID).Count() == 0)
                Str += "select * into TempCopy_tbltranssubtype from tbltranssubtype where TenentID = 0;update TempCopy_tbltranssubtype set TenentID = " + TID + " where TenentID = 0;INSERT INTO tbltranssubtype select * from TempCopy_tbltranssubtype where TenentID = " + TID + ";drop table TempCopy_tbltranssubtype;";
            if (Str != "")
            {
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
            BindMapList();





            //ViewState["ModeFun"] = null;
            //foreach (Database.FUNCTION_MST item in Fun)
            //{
            //    if (DB.FUNCTION_MST.Where(p => p.TenentID == TID && p.MENU_ID == item.MENU_ID).Count() > 0 && ViewState["ModeFun"] == null)
            //    {
            //        Database.FUNCTION_MST objEdit = DB.FUNCTION_MST.Single(p => p.TenentID == TID && p.MENU_ID == item.MENU_ID);
            //        for (int J = 0; J < ListView1.Items.Count; J++)//Text Dipak
            //        {
            //            CheckBox cheList = (CheckBox)ListView1.Items[J].FindControl("cheList");
            //            Label lblModule = (Label)ListView1.Items[J].FindControl("lblModule");
            //            int modul = Convert.ToInt32(lblModule.Text);
            //            if (modul == item.MENU_ID && cheList.Checked == true)
            //            {
            //                objEdit.ACTIVE_FLAG = 1;
            //                objEdit.ACTIVEMENU = true;
            //                objEdit.MENUDATE = Convert.ToDateTime(txtTillDate.Text);
            //            }
            //        }//Test Dipak
            //        DB.SaveChanges();
            //    }
            //    else
            //    {
            //        Database.FUNCTION_MST objj = DB.FUNCTION_MST.Single(p => p.TenentID == 0 && p.MENU_ID == item.MENU_ID);
            //        Database.FUNCTION_MST objfun = new Database.FUNCTION_MST();
            //        objfun.TenentID = TID;
            //        objfun.MENU_ID = item.MENU_ID;
            //        objfun.MASTER_ID = item.MASTER_ID;
            //        objfun.MODULE_ID = item.MODULE_ID;
            //        objfun.MENU_TYPE = item.MENU_TYPE;
            //        objfun.MENU_NAME1 = item.MENU_NAME1;
            //        objfun.MENU_NAME2 = item.MENU_NAME2;
            //        objfun.MENU_NAME3 = item.MENU_NAME3;
            //        objfun.LINK = item.LINK;
            //        objfun.Urloption = item.Urloption;
            //        objfun.URLREWRITE = item.URLREWRITE;
            //        objfun.MENU_LOCATION = item.MENU_LOCATION;
            //        objfun.MENU_ORDER = item.MENU_ORDER;
            //        objfun.DOC_PARENT = item.DOC_PARENT;
            //        objfun.CRUP_ID = item.CRUP_ID;
            //        objfun.ADDFLAGE = item.ADDFLAGE;
            //        objfun.EDITFLAGE = item.EDITFLAGE;
            //        objfun.DELFLAGE = item.DELFLAGE;
            //        objfun.PRINTFLAGE = item.PRINTFLAGE;
            //        objfun.AMIGLOBALE = item.AMIGLOBALE;
            //        objfun.MYPERSONAL = item.MYPERSONAL;
            //        objfun.SMALLTEXT = item.SMALLTEXT;
            //        objfun.ACTIVETILLDATE = item.ACTIVETILLDATE;
            //        objfun.ICONPATH = item.ICONPATH;
            //        objfun.COMMANLINE = item.COMMANLINE;
            //        objfun.METATITLE = item.METATITLE;
            //        objfun.METAKEYWORD = item.METAKEYWORD;
            //        objfun.METADESCRIPTION = item.METADESCRIPTION;
            //        objfun.HEADERVISIBLEDATA = item.HEADERVISIBLEDATA;
            //        objfun.HEADERINVISIBLEDATA = item.HEADERINVISIBLEDATA;
            //        objfun.FOOTERVISIBLEDATA = item.FOOTERVISIBLEDATA;
            //        objfun.FOOTERINVISIBLEDATA = item.FOOTERINVISIBLEDATA;
            //        objfun.REFID = item.REFID;
            //        objfun.MYBUSID = item.MYBUSID;
            //        objfun.LABLEFLAG = item.LABLEFLAG;
            //        objfun.ACTIVE_FLAG = 0;
            //        objfun.ACTIVEMENU = false;
            //        if (DB.PackageFunctionSetups.Where(p => p.PackageID == PackID && p.FunMenuID == item.MENU_ID).Count() > 0)
            //        {
            //            Database.PackageFunctionSetup OBJPACK = DB.PackageFunctionSetups.Single(p => p.PackageID == PackID && p.FunMenuID == item.MENU_ID);

            //            if (item.MENU_LOCATION == "Separator") //|| OBJPACK.FunMenuID == item.MENU_ID)
            //            {
            //                objfun.ACTIVE_FLAG = 1;
            //                objfun.ACTIVEMENU = true;
            //                objfun.MENUDATE = Convert.ToDateTime(txtTillDate.Text);
            //            }
            //            else
            //            {
            //                for (int J = 0; J < ListView1.Items.Count; J++)//Text Dipak
            //                {
            //                    CheckBox cheList = (CheckBox)ListView1.Items[J].FindControl("cheList");
            //                    Label lblModule = (Label)ListView1.Items[J].FindControl("lblModule");
            //                    int modul = Convert.ToInt32(lblModule.Text);
            //                    if (modul == item.MENU_ID && cheList.Checked == true)
            //                    {
            //                        objfun.ACTIVE_FLAG = 1;
            //                        objfun.ACTIVEMENU = true;
            //                        objfun.MENUDATE = Convert.ToDateTime(txtTillDate.Text);
            //                    }
            //                }//Test Dipak
            //            }
            //            // }                      
            //        }
            //        DB.FUNCTION_MST.AddObject(objfun);
            //        DB.SaveChanges();

            //        ViewState["ModeFun"] = "ADD";
            //        // for Module
            //        //int mod = Convert.ToInt32(lblMOD.Text);
            //        if (DB.MODULE_MST.Where(p => p.TenentID == TID && p.Module_Id == item.MODULE_ID).Count() == 0)
            //        {
            //            int ParentID = Convert.ToInt32(DB.MODULE_MST.Single(p => p.TenentID == 0 && p.Module_Id == item.MODULE_ID).Parent_Module_id);
            //            List<Database.MODULE_MST> MSTLIST = DB.MODULE_MST.Where(p => p.TenentID == 0 && (p.Module_Id == item.MODULE_ID || p.Module_Id == ParentID)).ToList();
            //            foreach (Database.MODULE_MST itemMST in MSTLIST)
            //            {
            //                if (itemMST.Module_Id == 13 || itemMST.Module_Id == 12)
            //                {

            //                }
            //                else
            //                {
            //                    Database.MODULE_MST ObjItemMST = new Database.MODULE_MST();
            //                    ObjItemMST.TenentID = TID;
            //                    ObjItemMST.Module_Id = itemMST.Module_Id;
            //                    ObjItemMST.MYSYSNAME = itemMST.MYSYSNAME;
            //                    ObjItemMST.Module_Name = itemMST.Module_Name;
            //                    ObjItemMST.Module_NameO = itemMST.Module_NameO;
            //                    ObjItemMST.Module_NameT = itemMST.Module_NameT;
            //                    ObjItemMST.Module_Desc = itemMST.Module_Desc;
            //                    ObjItemMST.Parent_Module_id = itemMST.Parent_Module_id;
            //                    ObjItemMST.Module_Order = itemMST.Module_Order;
            //                    ObjItemMST.ACTIVE_FLAG = "N";
            //                    if (DB.PackageFunctionSetups.Where(p => p.PackageID == PackID && p.FunMenuID == item.MENU_ID).Count() > 0)
            //                    {
            //                        Database.PackageFunctionSetup OBJPACK = DB.PackageFunctionSetups.Single(p => p.PackageID == PackID && p.FunMenuID == item.MENU_ID);
            //                        if (item.MENU_LOCATION == "Separator" || OBJPACK.FunMenuID == item.MENU_ID)
            //                        {
            //                            ObjItemMST.ACTIVE_FLAG = "Y";
            //                        }
            //                    }
            //                    ObjItemMST.CRUP_ID = itemMST.CRUP_ID;
            //                    ObjItemMST.Module_Location = itemMST.Module_Location;
            //                    DB.MODULE_MST.AddObject(ObjItemMST);
            //                    DB.SaveChanges();
            //                }
            //            }
            //        }
            //    }
            //}
            //ViewState["ModeFun"] = null;
            //ViewState["Modetemp"] = null;
            //foreach (Database.tempUser item in objTemp)
            //{
            //    if (DB.tempUsers.Where(p => p.TenentID == TID && p.LocationID == 1 && p.MENUID == item.MENUID).Count() > 0 && ViewState["Modetemp"] == null)
            //    {
            //        Database.tempUser objTEdit = DB.tempUsers.Single(p => p.TenentID == TID && p.LocationID == 1 && p.MENUID == item.MENUID);
            //        for (int J = 0; J < ListView1.Items.Count; J++)//Text Dipak
            //        {
            //            CheckBox cheList = (CheckBox)ListView1.Items[J].FindControl("cheList");
            //            Label lblModule = (Label)ListView1.Items[J].FindControl("lblModule");
            //            int TMenuID = Convert.ToInt32(lblModule.Text);
            //            if (TMenuID == item.MENUID && cheList.Checked == true)
            //            {
            //                objTEdit.ACTIVEMENU = true;
            //            }
            //        }//Test Dipak
            //        DB.SaveChanges();
            //    }
            //    else
            //    {
            //        Database.tempUser objT = DB.tempUsers.Single(p => p.TenentID == 0 && p.LocationID == 1 && p.MENUID == item.MENUID);
            //        Database.tempUser OBJTemp1 = new Database.tempUser();
            //        OBJTemp1.TenentID = TID;
            //        OBJTemp1.PRIVILAGEID = item.PRIVILAGEID;
            //        OBJTemp1.PRIVILAGESOURCE = item.PRIVILAGESOURCE;
            //        OBJTemp1.MENUID = item.MENUID;
            //        OBJTemp1.LocationID = item.LocationID;
            //        OBJTemp1.PRIVILAGE_MENUID = item.PRIVILAGE_MENUID;
            //        OBJTemp1.MODULE_ID = item.MODULE_ID;
            //        OBJTemp1.UserID = item.UserID;
            //        OBJTemp1.ROLE_ID = item.ROLE_ID;
            //        OBJTemp1.ADD_FLAG = item.ADD_FLAG;
            //        OBJTemp1.MODIFY_FLAG = item.MODIFY_FLAG;
            //        OBJTemp1.DELETE_FLAG = item.DELETE_FLAG;
            //        OBJTemp1.VIEW_FLAG = item.VIEW_FLAG;
            //        OBJTemp1.PRINTFLAGE = item.PRINTFLAGE;
            //        OBJTemp1.ALL_FLAG = item.ALL_FLAG;
            //        OBJTemp1.LINK = item.LINK;
            //        OBJTemp1.MASTER_ID = item.MASTER_ID;
            //        OBJTemp1.MENU_TYPE = item.MENU_TYPE;
            //        OBJTemp1.MENU_NAME1 = item.MENU_NAME1;
            //        OBJTemp1.MENU_NAME2 = item.MENU_NAME2;
            //        OBJTemp1.MENU_NAME3 = item.MENU_NAME3;
            //        OBJTemp1.URLREWRITE = item.URLREWRITE;
            //        OBJTemp1.MENU_LOCATION = item.MENU_LOCATION;
            //        OBJTemp1.MENU_ORDER = item.MENU_ORDER;
            //        OBJTemp1.DOC_PARENT = item.DOC_PARENT;
            //        OBJTemp1.AMIGLOBALE = item.AMIGLOBALE;
            //        OBJTemp1.MYPERSONAL = item.MYPERSONAL;
            //        OBJTemp1.SMALLTEXT = item.SMALLTEXT;
            //        OBJTemp1.ICONPATH = item.ICONPATH;
            //        OBJTemp1.METATITLE = item.METATITLE;
            //        OBJTemp1.METAKEYWORD = item.METAKEYWORD;
            //        OBJTemp1.METADESCRIPTION = item.METADESCRIPTION;
            //        OBJTemp1.HEADERVISIBLEDATA = item.HEADERVISIBLEDATA;
            //        OBJTemp1.HEADERINVISIBLEDATA = item.HEADERINVISIBLEDATA;
            //        OBJTemp1.FOOTERVISIBLEDATA = item.FOOTERVISIBLEDATA;
            //        OBJTemp1.FOOTERINVISIBLEDATA = item.FOOTERINVISIBLEDATA;
            //        OBJTemp1.REFID = item.REFID;
            //        OBJTemp1.MYBUSID = item.MYBUSID;
            //        OBJTemp1.ACTIVETILLDATE = item.ACTIVETILLDATE;
            //        OBJTemp1.ACTIVEMENU = item.ACTIVEMENU;
            //        OBJTemp1.ACTIVEPRIVILAGE = item.ACTIVEPRIVILAGE;
            //        OBJTemp1.ACTIVEMODULE = item.ACTIVEMODULE;
            //        OBJTemp1.ACTIVEROLE = item.ACTIVEROLE;
            //        OBJTemp1.URADD_FLAG = item.URADD_FLAG;
            //        OBJTemp1.URMODIFY_FLAG = item.URMODIFY_FLAG;
            //        OBJTemp1.URDELETE_FLAG = item.URDELETE_FLAG;
            //        OBJTemp1.URVIEW_FLAG = item.URVIEW_FLAG;
            //        OBJTemp1.URALL_FLAG = item.URALL_FLAG;
            //        OBJTemp1.IsLabelUpdate = item.IsLabelUpdate;
            //        OBJTemp1.ACTIVEMENU = false;
            //        if (DB.PackageFunctionSetups.Where(p => p.PackageID == PackID && p.FunMenuID == item.MENUID).Count() > 0)
            //        {
            //            Database.PackageFunctionSetup OBJPACK = DB.PackageFunctionSetups.Single(p => p.PackageID == PackID && p.FunMenuID == item.MENUID);
            //            if (item.MENU_LOCATION == "Separator")// || OBJPACK.FunMenuID == item.MENUID)
            //            {
            //                OBJTemp1.ACTIVEMENU = true;
            //            }
            //            else
            //            {
            //                for (int J = 0; J < ListView1.Items.Count; J++)//Text Dipak
            //                {
            //                    CheckBox cheList = (CheckBox)ListView1.Items[J].FindControl("cheList");
            //                    Label lblModule = (Label)ListView1.Items[J].FindControl("lblModule");
            //                    int TMenuID = Convert.ToInt32(lblModule.Text);
            //                    if (TMenuID == item.MENUID && cheList.Checked == true)
            //                    {
            //                        OBJTemp1.ACTIVEMENU = true;
            //                    }
            //                }//Test Dipak
            //            }
            //        }
            //        DB.tempUsers.AddObject(OBJTemp1);
            //        DB.SaveChanges();

            //        ViewState["Modetemp"] = "ADD";
            //    }//
            //}
            //ViewState["Modetemp"] = null;





            ListView3.DataSource = tempfuction;
            ListView3.DataBind();
            ViewState["Tabs2"] = 3;
            COnformTab3.Attributes.Add("aria-expanded", "false");
            COnformTab4.Attributes.Add("aria-expanded", "true");
            tab3.Attributes.Add("class", "tab-pane");//change by dipak
            tab4.Attributes.Add("class", "tab-pane active"); // change by dipak
            tabCSL3.Attributes.Add("class", "");
            tabCSL4.Attributes.Add("class", "active");

        }
        protected void LinkMapping_Click(object sender, EventArgs e)
        {
            COnformTab4.Attributes.Add("aria-expanded", "false");
            COnform.Attributes.Add("aria-expanded", "true");
            tab4.Attributes.Add("class", "tab-pane");//change by dipak
            tab5.Attributes.Add("class", "tab-pane active"); // change by dipak
            tabCSL4.Attributes.Add("class", "");
            tabCSL5.Attributes.Add("class", "active");
        }
        public void BindMapList()
        {
            int TID = Convert.ToInt32(ViewState["TID"]);
            List<Database.MODULE_MAP> MapList = DB.MODULE_MAP.Where(p => p.TenentID == TID).ToList();
            List<Database.USER_ROLE> ROLElist = DB.USER_ROLE.Where(p => p.TenentID == TID).ToList();
            List<Database.USER_RIGHTS> RightList = DB.USER_RIGHTS.Where(p => p.TenentID == TID).ToList();
            ListModuleMapping.DataSource = MapList;
            ListModuleMapping.DataBind();

            ListRoleMapping.DataSource = ROLElist.OrderBy(p => p.ROLE_ID);
            ListRoleMapping.DataBind();

            ListUserMapping.DataSource = RightList;
            ListUserMapping.DataBind();
        }
        public string Privilage(int ID)
        {

            if (DB.PRIVILEGE_MST.Where(p => p.PRIVILEGE_ID == ID).Count() > 0)
            {
                string PRIName = DB.PRIVILEGE_MST.Single(p => p.PRIVILEGE_ID == ID).PRIVILEGE_NAME;
                return PRIName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string USER(int ID)
        {

            if (DB.USER_MST.Where(p => p.USER_ID == ID).Count() > 0)
            {
                string userName = DB.USER_MST.Single(p => p.USER_ID == ID).LOGIN_ID;
                return userName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string ROLE(int ID)
        {
            int TID = Convert.ToInt32(ViewState["TID"]);
            if (DB.ROLE_MST.Where(p => p.ROLE_ID == ID).Count() > 0)
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
        public string Module_Name(int MODULE_ID)
        {
            if (DB.MODULE_MST.Where(p => p.Module_Id == MODULE_ID && p.TenentID == 0).Count() > 0)
            {
                string Module_Name = DB.MODULE_MST.Single(p => p.Module_Id == MODULE_ID && p.TenentID == 0).Module_Name;
                ViewState["Module"] = Module_Name;
                return Module_Name;
            }
            else
            {
                if (ViewState["Module"] != null)
                {
                    string Module_Name = ViewState["Module"].ToString();
                    return Module_Name;
                }
                else
                {
                    return null;
                }
            }
        }

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ROLLLIST = (DropDownList)e.Item.FindControl("drpRoll");

            ROLLLIST.DataSource = DB.ROLE_MST.Where(p => p.TenentID == 0 && p.ACTIVE_FLAG == "Y" && (p.ROLE_NAME != "ACM Admin"));
            ROLLLIST.DataTextField = "ROLE_NAME1";
            ROLLLIST.DataValueField = "ROLE_ID";
            ROLLLIST.DataBind();
            ROLLLIST.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select --", "0"));

        }

        protected void btnRoll_Click(object sender, EventArgs e)
        {
            List<USER_MST> itemList = new List<USER_MST>();
            int No = Convert.ToInt32(txtuserNumber.Text);
            for (int O = 0; O < No; O++)
            {
                USER_MST obj = new USER_MST();
                itemList.Add(obj);
            }
            ListView2.DataSource = itemList;
            ListView2.DataBind();
            int K = 0;
            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                K++;
                TextBox txtuserNumber1 = (TextBox)ListView2.Items[i].FindControl("txtuserNumber1");
                if (K == 1)
                    txtuserNumber1.Text = "User" + K + "(Admin)";
            }
        }

        protected void drpcuntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //Bind State
            string CID = drpcuntry.SelectedValue;
            bindSates(CID);
            //
            int CCID = Convert.ToInt32(CID);
            if (DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.COUNTRYID == CCID).Count() > 0)
            {
                decimal Currenct = Convert.ToDecimal(DB.tblCOUNTRies.Single(p => p.TenentID == TID && p.COUNTRYID == CCID).CURRENTCONVRATE);
                txtdcurreny.Text = Currenct.ToString();
            }
            else
            {
                txtdcurreny.Text = "0.00";
            }

        }
        public void bindSates(string CID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Classes.EcommAdminClass.getdropdown(drpstate, TID, CID, "", "", "tblStates");
        }

        protected void ListRegistration_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label listlblCompanyName = (Label)e.Item.FindControl("listlblCompanyName");
            Label ListLabelTenentID = (Label)e.Item.FindControl("ListLabelTenentID");
            Label ListLabelLocation = (Label)e.Item.FindControl("ListLabelLocation");
            Label listlblEmailID = (Label)e.Item.FindControl("listlblEmailID");
            Label listlblMobileNo = (Label)e.Item.FindControl("listlblMobileNo");
            LinkButton btnNew = (LinkButton)e.Item.FindControl("btnNew");
            DropDownList ListDrpCompany = (DropDownList)e.Item.FindControl("ListDrpCompany");
            List<TBLCOMPANYSETUP> List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1.ToUpper().Contains(listlblCompanyName.Text.ToUpper()) || p.EMAIL.ToUpper().Contains(listlblEmailID.Text.ToUpper()) || p.EMAIL1.ToUpper().Contains(listlblEmailID.Text.ToUpper()) || p.EMAIL2.ToUpper().Contains(listlblEmailID.Text.ToUpper()) || p.MOBPHONE == listlblMobileNo.Text && p.MOBPHONE != "00000" && p.MOBPHONE != "").ToList();
            int Count = Convert.ToInt32(List.Count());

            if (ListLabelTenentID.Text != "0")
            {
                btnNew.Text = "Edit";
            }
            else if (Count > 0)
            {
                ListDrpCompany.DataSource = List;
                ListDrpCompany.DataTextField = "COMPNAME1";
                ListDrpCompany.DataValueField = "COMPID";
                ListDrpCompany.DataBind();
                ListDrpCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                ListDrpCompany.Visible = true;
                btnNew.Text = "Go";
            }
            else
            {
                btnNew.Text = "New";
            }

        }

        protected void ListRegistration_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnNew")
            {
                LinkButton btnNew = (LinkButton)e.Item.FindControl("btnNew");
                DropDownList ListDrpCompany = (DropDownList)e.Item.FindControl("ListDrpCompany");
                int ID = 0;
                if (btnNew.Text == "New")
                {
                    ID = Convert.ToInt32(e.CommandArgument);
                    ViewState["RMYID"] = ID;
                    Database.NewCompaniySetup_Tryel Obj = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == ID);
                    BindNewAddCompany();
                    txtcompni1.Text = txtcompni3.Text = txtcompni2.Text = Obj.CompanyName;

                }
                else if (btnNew.Text == "Edit")
                {
                    //int TenantID = Convert.ToInt32(TextBoxSearchTID.Text);
                    int myid = Convert.ToInt32(e.CommandArgument);
                    ViewState["RMYID"] = myid;
                    int Tenent = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == myid).TenentID;
                    ViewState["MyTenant"] = Tenent;
                    List<MYCOMPANYSETUP> list1 = DB.MYCOMPANYSETUPs.Where(p => p.TenentID == Tenent).ToList();
                    if (list1.Count() == 1)
                    {
                        int CID = Convert.ToInt32(list1[0].COMPANYID);
                        ViewState["COMID"] = CID;
                        MyCompany(CID);
                        string Pack = DB.NewCompaniySetup_Tryel.Single(p => p.MyID == myid && p.TenentID == Tenent).Package.ToString();
                        drpPackage.SelectedValue = Pack;
                        BindPack();
                    }
                }
                else
                {
                    ID = Convert.ToInt32(ListDrpCompany.SelectedValue);
                    ViewState["COMID"] = ID;
                    BindProdu(ID);
                }

            }
        }
        public void BindPack()
        {
            int PAckID = Convert.ToInt32(drpPackage.SelectedValue);
            if (PAckID != 1005)
            {
                int Tenentid = Convert.ToInt32(ViewState["MyTenant"]);
                List<Database.PackageFunctionSetup> List1 = DB.PackageFunctionSetups.Where(p => p.PackageID == PAckID).ToList();
                List<Database.FUNCTION_MST> FUNCTION_MSTList = new List<FUNCTION_MST>();
                foreach (Database.PackageFunctionSetup item in List1)
                {
                    if (DB.FUNCTION_MST.Where(p => p.TenentID == Tenentid && p.MENU_ID == item.FunMenuID).Count() > 0)
                    {
                        Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.TenentID == Tenentid && p.MENU_ID == item.FunMenuID);
                        FUNCTION_MSTList.Add(obj);
                    }
                    else
                    {
                        Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.TenentID == 0 && p.MENU_ID == item.FunMenuID);
                        FUNCTION_MSTList.Add(obj);
                    }
                }
                ListView1.DataSource = FUNCTION_MSTList.OrderBy(p => p.MENU_ORDER);
                ListView1.DataBind();
                //cheList
                for (int i = 0; i < ListView1.Items.Count; i++)
                {
                    CheckBox cheList = (CheckBox)ListView1.Items[i].FindControl("cheList");
                    Label lblModule = (Label)ListView1.Items[i].FindControl("lblModule");
                    int Menu = Convert.ToInt32(lblModule.Text);
                    if (DB.FUNCTION_MST.Where(p => p.TenentID == Tenentid && p.MENU_ID == Menu).Count() > 0)
                    {
                        Database.FUNCTION_MST objlist = DB.FUNCTION_MST.Single(p => p.TenentID == Tenentid && p.MENU_ID == Menu);
                        if (objlist.ACTIVEMENU == true)
                            cheList.Checked = true;
                        else
                            cheList.Checked = false;
                    }
                    else
                        return;
                }
            }
        }
        protected void RDBMore_CheckedChanged(object sender, EventArgs e)
        {
            PNL11.Visible = true;
        }

        protected void RDBONEMonth_CheckedChanged(object sender, EventArgs e)
        {
            PNL11.Visible = false;
            txtFromDate.Text = DateTime.Now.ToShortDateString();
            txtTillDate.Text = DateTime.Now.AddMonths(1).ToShortDateString();
        }

        protected void LinkButtonSearch_Click(object sender, EventArgs e)
        {
            int TenantID = Convert.ToInt32(TextBoxSearchTID.Text);

            List<MYCOMPANYSETUP> list1 = DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TenantID).ToList();
            if (list1.Count() == 1)
            {

                int CID = Convert.ToInt32(list1[0].COMPANYID);
                MyCompany(CID);
            }
        }






    }
}



//Database.PackageFunctionSetup OBJPACK = DB.PackageFunctionSetups.Single(p => p.PackageID == PackID && p.FunMenuID == modul);
//               if (DB.FUNCTION_MST.Where(p => p.MENU_ID == modul && p.TenentID == 0).Count() > 0)
//               {                                                                     
//                   Database.FUNCTION_MST objj = DB.FUNCTION_MST.Single(p => p.TenentID == 0);
//                   Database.FUNCTION_MST objfun = new Database.FUNCTION_MST();
//                   objfun.TenentID = TID;
//                   objfun.MENU_ID = objj.MENU_ID;
//                   objfun.MASTER_ID = objj.MASTER_ID;
//                   objfun.MODULE_ID = objj.MODULE_ID;
//                   objfun.MENU_TYPE = objj.MENU_TYPE;
//                   objfun.MENU_NAME1 = objj.MENU_NAME1;
//                   objfun.MENU_NAME2 = objj.MENU_NAME2;
//                   objfun.MENU_NAME3 = objj.MENU_NAME3;
//                   objfun.LINK = objj.LINK;
//                   objfun.Urloption = objj.Urloption;
//                   objfun.URLREWRITE = objj.URLREWRITE;
//                   objfun.MENU_LOCATION = objj.MENU_LOCATION;
//                   objfun.MENU_ORDER = objj.MENU_ORDER;
//                   objfun.DOC_PARENT = objj.DOC_PARENT;
//                   objfun.CRUP_ID = objj.CRUP_ID;
//                   objfun.ADDFLAGE = objj.ADDFLAGE;
//                   objfun.EDITFLAGE = objj.EDITFLAGE;
//                   objfun.DELFLAGE = objj.DELFLAGE;
//                   objfun.PRINTFLAGE = objj.PRINTFLAGE;
//                   objfun.AMIGLOBALE = objj.AMIGLOBALE;
//                   objfun.MYPERSONAL = objj.MYPERSONAL;
//                   objfun.SMALLTEXT = objj.SMALLTEXT;
//                   objfun.ACTIVETILLDATE = objj.ACTIVETILLDATE;
//                   objfun.ICONPATH = objj.ICONPATH;
//                   objfun.COMMANLINE = objj.COMMANLINE;
//                   objfun.METATITLE = objj.METATITLE;
//                   objfun.METAKEYWORD = objj.METAKEYWORD;
//                   objfun.METADESCRIPTION = objj.METADESCRIPTION;
//                   objfun.HEADERVISIBLEDATA = objj.HEADERVISIBLEDATA;
//                   objfun.HEADERINVISIBLEDATA = objj.HEADERINVISIBLEDATA;
//                   objfun.FOOTERVISIBLEDATA = objj.FOOTERVISIBLEDATA;
//                   objfun.FOOTERINVISIBLEDATA = objj.FOOTERINVISIBLEDATA;
//                   objfun.REFID = objj.REFID;
//                   objfun.MYBUSID = objj.MYBUSID;
//                   objfun.LABLEFLAG = objj.LABLEFLAG;
//                   if (cheList.Checked == true || objj.MENU_LOCATION == "Separator" || OBJPACK.FunMenuID != modul)
//                   {
//                       objfun.ACTIVE_FLAG = 1;
//                   }
//                   else
//                   {
//                       objfun.ACTIVE_FLAG = 0;
//                   }
//                   DB.FUNCTION_MST.AddObject(objfun);
//                   DB.SaveChanges();
//               }
//               if (DB.tempUsers.Where(p => p.MENUID == modul && p.TenentID == 0 && p.LocationID == 1).Count() > 0)
//               {                
//                   Database.tempUser objT = DB.tempUsers.Single(p => p.TenentID == 0 && p.LocationID == 1);
//                   Database.tempUser OBJTemp1 = new Database.tempUser();
//                   OBJTemp1.TenentID = TID;
//                   OBJTemp1.PRIVILAGEID = objT.PRIVILAGEID;
//                   OBJTemp1.PRIVILAGESOURCE = objT.PRIVILAGESOURCE;
//                   OBJTemp1.MENUID = objT.MENUID;
//                   OBJTemp1.LocationID = objT.LocationID;
//                   OBJTemp1.PRIVILAGE_MENUID = objT.PRIVILAGE_MENUID;
//                   OBJTemp1.MODULE_ID = objT.MODULE_ID;
//                   OBJTemp1.UserID = objT.UserID;
//                   OBJTemp1.ROLE_ID = objT.ROLE_ID;
//                   OBJTemp1.ADD_FLAG = objT.ADD_FLAG;
//                   OBJTemp1.MODIFY_FLAG = objT.MODIFY_FLAG;
//                   OBJTemp1.DELETE_FLAG = objT.DELETE_FLAG;
//                   OBJTemp1.VIEW_FLAG = objT.VIEW_FLAG;
//                   OBJTemp1.PRINTFLAGE = objT.PRINTFLAGE;
//                   OBJTemp1.ALL_FLAG = objT.ALL_FLAG;
//                   OBJTemp1.LINK = objT.LINK;
//                   OBJTemp1.MASTER_ID = objT.MASTER_ID;
//                   OBJTemp1.MENU_TYPE = objT.MENU_TYPE;
//                   OBJTemp1.MENU_NAME1 = objT.MENU_NAME1;
//                   OBJTemp1.MENU_NAME2 = objT.MENU_NAME2;
//                   OBJTemp1.MENU_NAME3 = objT.MENU_NAME3;
//                   OBJTemp1.URLREWRITE = objT.URLREWRITE;
//                   OBJTemp1.MENU_LOCATION = objT.MENU_LOCATION;
//                   OBJTemp1.MENU_ORDER = objT.MENU_ORDER;
//                   OBJTemp1.DOC_PARENT = objT.DOC_PARENT;
//                   OBJTemp1.AMIGLOBALE = objT.AMIGLOBALE;
//                   OBJTemp1.MYPERSONAL = objT.MYPERSONAL;
//                   OBJTemp1.SMALLTEXT = objT.SMALLTEXT;
//                   OBJTemp1.ICONPATH = objT.ICONPATH;
//                   OBJTemp1.METATITLE = objT.METATITLE;
//                   OBJTemp1.METAKEYWORD = objT.METAKEYWORD;
//                   OBJTemp1.METADESCRIPTION = objT.METADESCRIPTION;
//                   OBJTemp1.HEADERVISIBLEDATA = objT.HEADERVISIBLEDATA;
//                   OBJTemp1.HEADERINVISIBLEDATA = objT.HEADERINVISIBLEDATA;
//                   OBJTemp1.FOOTERVISIBLEDATA = objT.FOOTERVISIBLEDATA;
//                   OBJTemp1.FOOTERINVISIBLEDATA = objT.FOOTERINVISIBLEDATA;
//                   OBJTemp1.REFID = objT.REFID;
//                   OBJTemp1.MYBUSID = objT.MYBUSID;
//                   OBJTemp1.ACTIVETILLDATE = objT.ACTIVETILLDATE;

//                   OBJTemp1.ACTIVEMENU = objT.ACTIVEMENU;
//                   OBJTemp1.ACTIVEPRIVILAGE = objT.ACTIVEPRIVILAGE;
//                   OBJTemp1.ACTIVEMODULE = objT.ACTIVEMODULE;
//                   OBJTemp1.ACTIVEROLE = objT.ACTIVEROLE;
//                   OBJTemp1.URADD_FLAG = objT.URADD_FLAG;
//                   OBJTemp1.URMODIFY_FLAG = objT.URMODIFY_FLAG;
//                   OBJTemp1.URDELETE_FLAG = objT.URDELETE_FLAG;
//                   OBJTemp1.URVIEW_FLAG = objT.URVIEW_FLAG;
//                   OBJTemp1.URALL_FLAG = objT.URALL_FLAG;
//                   OBJTemp1.IsLabelUpdate = objT.IsLabelUpdate;
//                   if (cheList.Checked == true || objT.MENU_LOCATION == "Separator" || OBJPACK.FunMenuID != modul)
//                   {
//                       OBJTemp1.ACTIVEMENU = true;
//                   }
//                   else
//                   {
//                       OBJTemp1.ACTIVEMENU = false;
//                   }
//                   DB.tempUsers.AddObject(OBJTemp1);
//                   DB.SaveChanges();
//               }

//     int mod = Convert.ToInt32(lblMOD.Text);
//if (DB.MODULE_MST.Where(p => p.TenentID == TID && p.Module_Id == mod).Count() == 0)
//{
//    //string MNAME = DB.MODULE_MST.Single(p => p.TenentID == 0 && p.Module_Id == mod).Module_Name;
//    int ParentID = Convert.ToInt32(DB.MODULE_MST.Single(p => p.TenentID == 0 && p.Module_Id == mod).Parent_Module_id);
//    List<Database.MODULE_MST> MSTLIST = DB.MODULE_MST.Where(p => p.TenentID == 0 && (p.Module_Id == mod || p.Module_Id == ParentID)).ToList();
//    foreach (Database.MODULE_MST item in MSTLIST)
//    {
//        if (item.Module_Id == 13 || item.Module_Id == 12)
//        {

//        }
//        else
//        {
//            Database.MODULE_MST ObjItemMST = new Database.MODULE_MST();
//            ObjItemMST.TenentID = TID;
//            ObjItemMST.Module_Id = item.Module_Id;
//            ObjItemMST.MYSYSNAME = item.MYSYSNAME;
//            ObjItemMST.Module_Name = item.Module_Name;
//            ObjItemMST.Module_NameO = item.Module_NameO;
//            ObjItemMST.Module_NameT = item.Module_NameT;
//            ObjItemMST.Module_Desc = item.Module_Desc;
//            ObjItemMST.Parent_Module_id = item.Parent_Module_id;
//            ObjItemMST.Module_Order = item.Module_Order;
//            ObjItemMST.ACTIVE_FLAG = item.ACTIVE_FLAG;
//            ObjItemMST.CRUP_ID = item.CRUP_ID;
//            ObjItemMST.Module_Location = item.Module_Location;
//            DB.MODULE_MST.AddObject(ObjItemMST);
//            DB.SaveChanges();
//        }

//    }
//}