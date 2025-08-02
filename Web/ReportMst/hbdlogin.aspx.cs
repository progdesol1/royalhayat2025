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
    public partial class hbdlogin : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 6;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            pnlerror.Visible = false;
            string Username = txtuserid.Text.ToUpper();
            string Password = txtpassword.Text;

            List<Database.TBLCOMPANYSETUP> ListUser = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.CUSERID.ToUpper() == Username && p.CPASSWRD == Password).ToList();
            if (ListUser.Count() > 0)
            {
                if (ListUser.Where(p => p.TenentID == TID && p.CUSERID.ToUpper() == Username && p.CPASSWRD == Password).Count() > 0)
                {
                    Database.TBLCOMPANYSETUP objDUSER = ListUser.Single(p => p.TenentID == TID && p.CUSERID.ToUpper() == Username && p.CPASSWRD == Password);
                    int contacmyid = Convert.ToInt32(objDUSER.COMPID);
                    if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.contactID == contacmyid).Count() > 0)
                    {
                        int EMPID = DB.tbl_Employee.Where(p => p.TenentID == TID && p.contactID == contacmyid).FirstOrDefault().employeeID;
                        Session["EMPDriver"] = ListUser[0];
                        Response.Redirect("../ReportMst/AMPMnew123.aspx?DID=" + EMPID);
                    }
                }
                else
                {
                    pnlerror.Visible = true;
                    lblErrorMSG.Text = "Your Login_ID Or Password Is Invalid";
                    return;
                }
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