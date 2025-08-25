using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Web.Configuration;
using System.Configuration;
using Classes;
using Database;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;



namespace Web.ACM
{
    public partial class Login : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        // ERPDataLinqToSqlDataContext objLinqDataContext = new ERPDataLinqToSqlDataContext();
        string lFirstName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           
            USER_MST UserList = new USER_MST();
            if (Request.QueryString["RDNAD"] != null)
            {
                string[] QUSTRING = Request.QueryString["RDNAD"].ToString().Split('@');
                string TEN = QUSTRING[0];
                string User = QUSTRING[1];
                string PW = QUSTRING[2];
                Requestt(TEN, User,PW);

            }

            if (Request.QueryString["logout"] != null)
            {
                if (Request.Cookies["tgadmin"] != null)
                {
                    HttpCookie aCookie = new HttpCookie("tgadmin");
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
            }
            if (!IsPostBack)
            {
                BindLangDDL();
                binddata();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrmsg.Text = "";

            USER_MST UserList = GlobleClass.EncryptionHelpers.LoginVerified("10", txtUser.Text, txtPass.Text);
            if (UserList != null)
            {
                int TID = 10;
                if (UserList.USER_TYPE == 4)
                {
                    if (ddltenet.SelectedValue == "-1")
                    {
                        pnltentid.Visible = true;
                        pnlErrorMsg.Visible = true;
                        lblerrmsg.Text = "Select TenetID";
                    }
                    else
                    {
                        //  Session["SAUSER"] = UserList;
                        Session["SAFirstname"] = UserList.FIRST_NAME1.ToString();
                        int SubTenet = Convert.ToInt32(ddltenet.SelectedValue);
                        TenantGroup objtenetgroup = DB.TenantGroups.Single(p => p.MainTenantID == TID && p.TenentID == SubTenet);
                        int Userid = Convert.ToInt32(objtenetgroup.USERID);
                        var Subuser = DB.USER_MST.Single(p => p.USER_ID == Userid);
                        int Modulid = 0;
                        if (DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == Userid && p.SP1Name == "DefaultSet").Count() > 0)
                        {
                            Modulid = DB.MODULE_MAP.Single(p => p.UserID == Userid && p.TenentID == SubTenet && p.SP1Name == "DefaultSet").MODULE_ID;
                        }
                        else
                        {
                            pnlErrorMsg.Visible = true;
                            lblerrmsg.Text = "No Module for This User, please contact to Administrator...";
                            return;
                        }
                        Session["USER"] = Subuser;
                        Session["LANGUAGE"] = "English";
                        Session["SiteModuleID"] = Modulid;
                        if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == UserList.TenentID).Count() == 1)
                        {
                            var objMYCOMPANYSETUPs = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == UserList.TenentID);
                            Session["objMYCOMPANYSETUP"] = objMYCOMPANYSETUPs;
                        }

                        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Login").REFNAME1;
                        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFID;


                        GlobleClass.EncryptionHelpers.WriteLog("Login:", evantname, "USER_MST", Userid.ToString(), 0, auditno);
                        Response.Redirect("~/acm/DemoPrvilage.aspx");
                        //Response.Redirect("~/acm/demo.aspx");
                    }
                }
                else
                {
                    List<Database.tempUser1> List = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1).OrderBy(p => p.MENU_ORDER).ToList();
                    int ROLLID = 0;
                    int Modulid = 0;
                    int Userid = UserList.USER_ID;

                    if (Userid == 11367 && TID == 1)
                    {
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Response.Redirect("/Sales/Co_opSociety.aspx?transid=4151&transsubid=415101");
                    }
                    else if (Userid == 11368 && TID == 1)
                    {
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Response.Redirect("/Sales/sales_Chapra.aspx?transid=4151&transsubid=415101");
                    }
                    else if (Userid == 11369 && TID == 1)
                    {
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Response.Redirect("/Sales/sales_Bakala.aspx?transid=4151&transsubid=415101");
                    }
                    if (Userid == 11378 && TID == 1)
                    {
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Response.Redirect("/Sales/Co_opSociety.aspx?transid=4151&transsubid=415101");
                    }
                    else if (Userid == 11379 && TID == 1)
                    {
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Response.Redirect("/Sales/sales_Chapra.aspx?transid=4151&transsubid=415101");
                    }
                    else if (Userid == 11380 && TID == 1)
                    {
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Response.Redirect("/Sales/sales_Bakala.aspx?transid=4151&transsubid=415101");
                    }
                    if (UserList.USER_ID == Userid && UserList.ACC_LOCK == "E")
                    {
                        Modulid = 44;
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Session["Firstname"] = lFirstName.ToString();
                        Session["SiteModuleID"] = Modulid;
                        Session["SAFirstname"] = UserList.FIRST_NAME1.ToString();

                        Response.Redirect("~/acm/DemoPrvilage.aspx");
                    }
                    if (UserList.USER_ID == Userid && UserList.ACC_LOCK == "S" && UserList.USER_TYPE == 9)
                    {
                        Modulid = 45;
                        Session["USER"] = UserList;
                        Session["LANGUAGE"] = "English";
                        Session["Firstname"] = lFirstName.ToString();
                        Session["SiteModuleID"] = Modulid;
                        Session["SAFirstname"] = UserList.FIRST_NAME1.ToString();
                        Response.Redirect("~/acm/DemoPrvilage.aspx");
                    }
                    if (UserList.USERDATE.Value >= DateTime.Now.Date  && UserList.ACTIVEUSER == true)
                    {
                        List = List.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == UserList.USER_ID ).OrderBy(p => p.MENU_ORDER).ToList();
                        if (List.Count() > 0)
                        {

                        }
                        else
                        {
                            pnlErrorMsg.Visible = true;
                            lblerrmsg.Text = "User is Expired , please contact to Administrator...";
                            return;
                        }
                        List = List.Where(p => p.TenentID == TID && p.LocationID == 1 && p.ACTIVEROLE == true).OrderBy(p => p.MENU_ORDER).ToList();
                        if (List.Count() > 0)
                        {

                        }
                        else
                        {
                            pnlErrorMsg.Visible = true;
                            lblerrmsg.Text = "Your ROLE is Expired , please contact to Administrator...";
                            return;
                        }
                        List<Database.MODULE_MAP> moduleList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == Userid && p.ACTIVE_FLAG == "Y").ToList();
                        if (moduleList.Count() > 0)
                        {
                            //Modulid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == Userid && p.SP1Name == "DefaultSet").MODULE_ID;
                            foreach (Database.MODULE_MAP iitemm in moduleList)
                            {
                                Modulid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == Userid && p.SP1Name == "DefaultSet").MODULE_ID;
                                List<Database.tempUser1> TempModuleALL = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == Userid && p.ACTIVEMODULE == true).ToList();
                                if (TempModuleALL.Count() > 0)
                                {
                                    List<Database.tempUser1> TempModule = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == Userid && p.MODULE_ID == Modulid && p.ACTIVEMODULE == true).ToList();
                                    if (TempModule.Count() > 0)
                                    {

                                    }
                                    else
                                    {
                                        TempModule = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == Userid && p.MODULE_ID == iitemm.MODULE_ID && p.ACTIVEMODULE == true).ToList();
                                        if (TempModule.Count() > 0)
                                        {
                                            Modulid = iitemm.MODULE_ID;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    pnlErrorMsg.Visible = true;
                                    lblerrmsg.Text = "No Module for This User, please contact to Administrator...";
                                    return;
                                }
                            }
                        }
                        else
                        {
                            pnlErrorMsg.Visible = true;
                            lblerrmsg.Text = "No Module for This User, please contact to Administrator...";
                            return;
                        }

                    }
                    else
                    {
                        pnlErrorMsg.Visible = true;
                        lblerrmsg.Text = "Your Login / password is Expired , please contact to Administrator...";
                        return;
                    }
               
                        lFirstName = UserList.FIRST_NAME.ToString() + " " + UserList.LAST_NAME.ToString();



                    //List<Database.MODULE_MAP> ListMap = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == Userid).ToList();
                    ////List<Database.tempUser> ModulList = DB.tempUsers.Where(p => p.TenentID == TID && p.ROLE_ID == ROLLID).ToList(); by dipak
                    //List<Database.MODULE_MST> ListModual = new List<MODULE_MST>();
                    //foreach (Database.MODULE_MAP items in ListMap)
                    //{
                    //    List<Database.MODULE_MST> ListModual1 = DB.MODULE_MST.Where(p => p.TenentID == TID && p.Module_Id == items.MODULE_ID).ToList();

                    //    //if (ListModual1.Count() > 0 && ModulList.Count() > 0) by dipak
                    //    //{
                    //    Database.MODULE_MST OBJmodule = ListModual1.Single(p => p.TenentID == TID && p.Module_Id == items.MODULE_ID);
                    //    ListModual.Add(OBJmodule);
                    //    //} by dipak

                    //}

                    //if (ListModual.Count() > 0)
                    //{ }
                    //else
                    //{
                    //    pnlErrorMsg.Visible = true;
                    //    lblerrmsg.Text = "No Module for This User, please contact to Administrator...";
                    //    return;
                    //}



                    Database.USER_MST objLiggingAction = DB.USER_MST.Single(p => p.TenentID == UserList.TenentID && p.PASSWORD == UserList.PASSWORD && p.LOGIN_ID == UserList.LOGIN_ID && p.USER_ID == UserList.USER_ID);
                    if (objLiggingAction.LoginActive == null || objLiggingAction.LoginActive == 0)
                    {
                        objLiggingAction.LoginActive = 1;
                        DB.SaveChanges();
                    }
                    else
                    {
                        int action = Convert.ToInt32(objLiggingAction.LoginActive);
                        action = action + 1;
                        objLiggingAction.LoginActive = action;
                        DB.SaveChanges();
                    }
                    Session["USER"] = UserList;
                    Session["LANGUAGE"] = "English";
                    Session["Firstname"] = lFirstName.ToString();
                    Session["SiteModuleID"] = Modulid;
                    Session["SAFirstname"] = UserList.FIRST_NAME1.ToString();
                    Session["USERTID"] = UserList.TenentID;
                    HttpCookie aCookie = new HttpCookie("tgadmin");
                    aCookie.Values["UserName"] = UserList.USER_ID.ToString();
                    aCookie.Values["CLANGUAGE"] = "English";
                    if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == UserList.TenentID).Count() == 1)
                    {
                        var objMYCOMPANYSETUPs = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == UserList.TenentID);
                        Session["objMYCOMPANYSETUP"] = objMYCOMPANYSETUPs;
                        int comp = Convert.ToInt32(objMYCOMPANYSETUPs.COMPANYID);
                        if(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == comp).Count() > 0)
                        {
                            Database.TBLCOMPANYSETUP compobj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == comp);
                            Session["CustomerUser"] = compobj;
                            Session["CID"] = comp;
                        }
                        
                    }
                    aCookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(aCookie);

                    //GlobleClass.EncryptionHelpers.WriteLog("Login:", "Login", "USER_MST", objLiggingAction.USER_ID.ToString(), 0);
                    Classes.GlobleClass.InsertErrorLog1("USER_MST", "~/acm/Login.aspx", "Login:", "LineNo:0");
                    if (Modulid == 39)
                    {
                        Response.Redirect("~/acm/DemoPOS.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/acm/DemoPrvilage.aspx");
                    }

                }

            }

            else
            {
                pnlErrorMsg.Visible = true;
                lblerrmsg.Text = "User Name And Password Not Match";
                return;
            }

        }
        public void BindLangDDL()
        {
            //.Where(p => p.TENANTID == 361);
            //dtLanguage = objGetLogin.CommonMethodOderFnmTnmObTenant("prc_getAllTable_Act_OdrWise", "MYCONLOCID,LangName1,LangName2,LangName3,CULTUREOCDE", "dbo.tblLanguage", "LangName1", Convert.ToInt32(1));
            if (DB.tblLanguages.Count() > 0)
            {

            }
            else
            {

            }            
            //Ayo Bind User Role
            // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TENANT_ID); 


            ddltenet.DataSource = DB.TenantGroups.Where(p => p.MainTenantID == 0);
            ddltenet.DataTextField = "TenentID";
            ddltenet.DataValueField = "TenentID";
            ddltenet.DataBind();
            ddltenet.Items.Insert(0, new ListItem("---Select---", "-1"));
        }

        protected void txtTenantId_TextChanged(object sender, EventArgs e)
        {
            USER_MST UserList = GlobleClass.EncryptionHelpers.LoginVerified("10", txtUser.Text, txtPass.Text);
            if (UserList != null)
            {
                if (UserList.Language1 != null)
                {
                    string Lang = UserList.Language1.Trim();
                  //  DDLLanguage.SelectedValue = Lang;
                }
                if ( UserList != null)
                {
                    pnltentid.Visible = true;
                }
                else
                {
                    pnltentid.Visible = false;
                    pnlErrorMsg.Visible = false;
                    lblerrmsg.Text = "";
                    ddltenet.SelectedIndex = 0;
                }
                txtPass.Text = txtPass.Text;
            }
        }

        //protected void txtTenantId_TextChanged(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(txtTenantId.Text);
        //    DDLUserType.DataSource = DB.ROLE_MST.Where(p => p.TENANT_ID == TID);
        //    DDLUserType.DataTextField = "ROLE_NAME";
        //    DDLUserType.DataValueField = "ROLE_ID";
        //    DDLUserType.DataBind();
        //    DDLUserType.Items.Insert(0, new ListItem("---Select---", "0"));
        //    DDLUserType.Focus();
        //}

        public void Requestt(string Tenent,string user,string pw)
        {
            pnlErrorMsg.Visible = false;
            lblerrmsg.Text = "";

            USER_MST UserReq = GlobleClass.EncryptionHelpers.LoginVerified(Tenent, user, pw);
            int TID = Convert.ToInt32(Tenent);
            if (UserReq != null)
            {

                List<Database.tempUser1> List = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1).OrderBy(p => p.MENU_ORDER).ToList();
                int ROLLID = 0;
                int Modulid = 0;
                int Userid = UserReq.USER_ID;


                if (UserReq.USERDATE.Value <= DateTime.Now.Date && UserReq.Till_DT.Value >= DateTime.Now.Date && UserReq.ACTIVEUSER == true && UserReq.USERDATE != null && UserReq.Till_DT != null)
                {
                    List = List.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == UserReq.USER_ID && p.ACTIVETILLDATE >= DateTime.Now).OrderBy(p => p.MENU_ORDER).ToList();
                    if (List.Count() > 0)
                    {

                    }
                    else
                    {
                        pnlErrorMsg.Visible = true;
                        lblerrmsg.Text = "User is Expired , please contact to Administrator...";
                        return;
                    }
                    List = List.Where(p => p.TenentID == TID && p.LocationID == 1 && p.ACTIVEROLE == true).OrderBy(p => p.MENU_ORDER).ToList();
                    if (List.Count() > 0)
                    {

                    }
                    else
                    {
                        pnlErrorMsg.Visible = true;
                        lblerrmsg.Text = "Your ROLE is Expired , please contact to Administrator...";
                        return;
                    }
                    List<Database.MODULE_MAP> moduleList = DB.MODULE_MAP.Where(p => p.TenentID == TID && p.UserID == Userid && p.ACTIVE_FLAG == "Y").ToList();
                    if (moduleList.Count() > 0)
                    {
                        //Modulid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == Userid && p.SP1Name == "DefaultSet").MODULE_ID;
                        foreach (Database.MODULE_MAP iitemm in moduleList)
                        {
                            Modulid = DB.MODULE_MAP.Single(p => p.TenentID == TID && p.UserID == Userid && p.SP1Name == "DefaultSet").MODULE_ID;
                            List<Database.tempUser1> TempModuleALL = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == Userid && p.ACTIVEMODULE == true).ToList();
                            if (TempModuleALL.Count() > 0)
                            {
                                List<Database.tempUser1> TempModule = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == Userid && p.MODULE_ID == Modulid && p.ACTIVEMODULE == true).ToList();
                                if (TempModule.Count() > 0)
                                {

                                }
                                else
                                {
                                    TempModule = DB.tempUser1.Where(p => p.TenentID == TID && p.LocationID == 1 && p.UserID == Userid && p.MODULE_ID == iitemm.MODULE_ID && p.ACTIVEMODULE == true).ToList();
                                    if (TempModule.Count() > 0)
                                    {
                                        Modulid = iitemm.MODULE_ID;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                pnlErrorMsg.Visible = true;
                                lblerrmsg.Text = "No Module for This User, please contact to Administrator...";
                                return;
                            }
                        }
                    }
                    else
                    {
                        pnlErrorMsg.Visible = true;
                        lblerrmsg.Text = "No Module for This User, please contact to Administrator...";
                        return;
                    }

                }
                else
                {
                    pnlErrorMsg.Visible = true;
                    lblerrmsg.Text = "Your Login / password is Expired , please contact to Administrator...";
                    return;
                }
             
                    lFirstName = UserReq.FIRST_NAME.ToString() + " " + UserReq.LAST_NAME.ToString();


                Database.USER_MST objLiggingAction = DB.USER_MST.Single(p => p.TenentID == UserReq.TenentID && p.PASSWORD == UserReq.PASSWORD && p.LOGIN_ID == UserReq.LOGIN_ID && p.USER_ID == UserReq.USER_ID);
                if (objLiggingAction.LoginActive == null || objLiggingAction.LoginActive == 0)
                {
                    objLiggingAction.LoginActive = 1;
                    DB.SaveChanges();
                }
                else
                {
                    int action = Convert.ToInt32(objLiggingAction.LoginActive);
                    action = action + 1;
                    objLiggingAction.LoginActive = action;
                    DB.SaveChanges();
                }
                Session["USER"] = UserReq;
                Session["LANGUAGE"] = "English";
                Session["Firstname"] = lFirstName.ToString();
                Session["SiteModuleID"] = Modulid;
                Session["SAFirstname"] = UserReq.FIRST_NAME1.ToString();
                
                HttpCookie aCookie = new HttpCookie("tgadmin");
                aCookie.Values["UserName"] = UserReq.USER_ID.ToString();
                aCookie.Values["CLANGUAGE"] = "English";
                if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == UserReq.TenentID).Count() == 1)
                {
                    var objMYCOMPANYSETUPs = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == UserReq.TenentID);
                    Session["objMYCOMPANYSETUP"] = objMYCOMPANYSETUPs;
                }
                aCookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(aCookie);

                String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Login").REFNAME1;
                int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Feedback").REFID;


                GlobleClass.EncryptionHelpers.WriteLog("Login:", evantname, "USER_MST", objLiggingAction.USER_ID.ToString(), 0, auditno);
                if (Modulid == 39)
                {
                    Response.Redirect("~/acm/DemoPOS.aspx");
                }
                else
                {
                    Response.Redirect("~/acm/DemoPOS.aspx");
                }

            }

            else
            {
                pnlErrorMsg.Visible = true;
                lblerrmsg.Text = "User Name And Password Not Match";
                return;
            }

        }

        protected void drpterminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpterminal.SelectedValue == "0")
            {
                Button1.Visible = false;
            }
            else
            {
                Button1.Visible = true;
            }

        }
        public void binddata()
        {
            //  TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            drpterminal.DataSource = DB.REFTABLEs.Where(p => p.TenentID == 10 && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal");
            drpterminal.DataTextField = "SHORTNAME";
            drpterminal.DataValueField = "REFID";
            drpterminal.DataBind();
            drpterminal.Items.Insert(0, new ListItem("-- Select Terminal --", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int terminalID = Convert.ToInt32(drpterminal.SelectedValue);
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "window.open('http://localhost:81/Smiley.aspx?TerminalID=" + terminalID + "','_blank','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes')", true);
        }

    }
}
