using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Web.CRM.Class;
using Web.CRM.Class.Class;
using System.Transactions;

namespace Web.Master.UserContol
{
    public partial class Addappoiment : System.Web.UI.UserControl
    {
        int TID = 0;
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        }

        protected void btnsaveAppoint_Click(object sender, EventArgs e)
        {
            string Duscrption = txtTitleAP.Text;
            string Name = "";
            string Department = "0";
            int Priority = Convert.ToInt32(drpColor.SelectedValue);
            DateTime sdate = Convert.ToDateTime(txtSdateAP.Text);
            DateTime edate = Convert.ToDateTime(txtEnddateAP.Text);
            string URL = txtURLAP.Text;

            Classes.CRMClass.InsertAppointment(0, TID, 1, Duscrption, sdate, edate, Priority.ToString(), URL, true, true, "Clinic", "Insert");
        }


    }
}