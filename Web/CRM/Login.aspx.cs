using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using Web.CRM;
using Database;
using Classes;


namespace Web.CRM
{
    public partial class Login : System.Web.UI.Page
    {
       // Database.CallEMEntities DB = new Database.CallEMEntities();
        Database.CallEntities DB = new Database.CallEntities();
        string lFirstName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //GLOBAL
            //string ErrorMsg=Classes.Globle.EncryptionHelpers.CheckLogin(txtTenantId.Text, txtUser.Text, txtPass.Text, "");

            //if (ErrorMsg == "")
            //{
            // ERP_WEB_USER_MST UserList = Classes.Globle.EncryptionHelpers.LoginVerified(txtTenantId.Text, txtUser.Text, txtPass.Text, "");
            USER_MST UserList = GlobleClass.EncryptionHelpers.LoginVerified(txtTenantId.Text, txtUser.Text, txtPass.Text);
            if (UserList != null)
            {
                lFirstName = UserList.FIRST_NAME.ToString() + " " + UserList.LAST_NAME.ToString();
                Session["LANGUAGE"] = "en-US";
                Session["USER"] = UserList;
                Session["LoginID"] = UserList.LOGIN_ID.ToString();
                Session["Firstname"] = lFirstName.ToString();
                Session["Location"] = 1;
                Session["MenuACm"] = 1;
               
                Response.Redirect("DashBoard.aspx");
            }
            else
            {
                pnlErrorMsg.Visible = true;
                lblerrmsg.Text = "User Name And Password Not Match";
                return;
            }
            // }

        }

    }
}