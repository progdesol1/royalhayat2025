using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Configuration;
using Classes;
using Database;
using System.Web.UI.WebControls;
using System.Web;

namespace Web.ReportMst
{
    public partial class hbclogin : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 6;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            pnlerror.Visible = false;
            string Username = txtusername.Text;
            string pass = txtpassword.Text;

            List<Database.TBLCOMPANYSETUP> ListUser = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.CUSERID == Username && p.CPASSWRD == pass).ToList();
            if (ListUser.Count() > 0)
            {
                Database.TBLCOMPANYSETUP OBJ = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.CUSERID == Username && p.CPASSWRD == pass);
                int Compid = OBJ.COMPID;
                Session["CustomerUser"] = ListUser[0];
                Response.Redirect("../ReportMst/Profile.aspx?CID=" + Compid);
            }
            else
            {
                pnlerror.Visible = true;
                lblErrorMSG.Text = "Your Login_ID Or Password Is Invalid";
                return;
            }
        }





    }
}