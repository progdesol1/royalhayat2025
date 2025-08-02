using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class TerminalSmiley : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                binddata();
               
            }
            
        }

        public void binddata()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            drpterminal.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Survey" && p.REFSUBTYPE == "SurveyIDTerminal");
            drpterminal.DataTextField = "SHORTNAME";
            drpterminal.DataValueField = "REFID";
            drpterminal.DataBind();
            drpterminal.Items.Insert(0, new ListItem("         -- Select Terminal --          ", "0"));
        }


        protected void drpterminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int terminalID =Convert.ToInt32(drpterminal.SelectedValue);
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            int terminalID = Convert.ToInt32(drpterminal.SelectedValue);
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "window.open('Smiley.aspx?TerminalID=" + terminalID + "','_blank','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes')", true);

        }

    }
}