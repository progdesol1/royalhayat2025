using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.CRM.UserControl
{
    public partial class ProjectUC : System.Web.UI.UserControl
    {
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbButton1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            var exist = DB.TBLPROJECTs.Where(c => c.ACTIVE == true && (c.PROJECTNAME1 == txtproject.Text || c.PROJECTNAME2 == txtproject.Text || c.PROJECTNAME3 == txtproject.Text && c.TenentID == TID));
            if (exist.Count() < 1)
            {
                Database.TBLPROJECT objtbl_REFTABLE = new Database.TBLPROJECT();
                objtbl_REFTABLE.TenentID = TID;
                objtbl_REFTABLE.PROJECTID = DB.TBLPROJECTs.Where(p=>p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLPROJECTs.Where(p=>p.TenentID == TID).Max(p => p.PROJECTID) + 1) : 1;
                objtbl_REFTABLE.PROJECTNAME1 = txtproject.Text;
                objtbl_REFTABLE.ACTIVE = true;
                DB.TBLPROJECTs.AddObject(objtbl_REFTABLE);
                DB.SaveChanges();

                //ContentPlaceHolder con = new ContentPlaceHolder();
                
                //((DMSMaster)this.Page.Master).BindListFromPopup(((ContentPlaceHolder)this.Parent), Database.Common.PopupOP.Project.ToString(), "drpProject_id");

                // Response.Redirect("NewDocument.aspx");
            }
            else
            {
                string display = "Projetc Name Is Allready Exist..!";
                // ClientScript.RegisterStartupScript(this.GetType(), "Project Is Duplicate!", "alert('" + display + "');", true);
                return;
            }
        }

      
    }
}