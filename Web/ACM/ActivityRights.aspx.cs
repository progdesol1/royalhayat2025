using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.ACM
{
    public partial class ActivityRights : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
       
        int TID = 0;
        int LID = 0;
        int MID = 0;
        int UID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillContractorID();
            }
        }
        public void FillContractorID()
        {

            var Datas = (from fun1 in DB.TBLLOCATIONs
                         select new
                         {
                             fun1.TenentID,

                         }
        ).Distinct();
            DrpTENANT_ID.DataSource = Datas;
            DrpTENANT_ID.DataTextField = "TenentID";
            DrpTENANT_ID.DataValueField = "TenentID";
            DrpTENANT_ID.DataBind();
            DrpTENANT_ID.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        protected void DrpTENANT_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);

            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
            //select * from TBLLOCATION where TenantID=1

            //drplocation.DataSource = DB.TBLLOCATIONs.Where(p => p.Active == "Y" && p.TenantID == TID);
            //drplocation.DataTextField = "LOCNAME1";
            //drplocation.DataValueField = "LOCATIONID";
            //drplocation.DataBind();
            //drplocation.Items.Insert(0, new ListItem("---Select Location---", "0"));
            ViewState["TenenatID"] = TID;
        }
        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(drplocation.SelectedValue) != 0 && Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0)
            {

                int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                int LID = Convert.ToInt32(drplocation.SelectedValue);
                ViewState["TenenatID"] = TID;
                ViewState["LocationID"] = LID;

                drpMODULE_ID.Items.Clear();
                Classes.EcommAdminClass.getdropdown(drpMODULE_ID, TID, "", "", "", "MODULE_MST");
                //select * from MODULE_MST where Parent_Module_id =0 

                //drpMODULE_ID.DataSource = DB.MODULE_MST.Where(p => p.TENANT_ID == TID );
                //drpMODULE_ID.DataTextField = "Module_Name";
                //drpMODULE_ID.DataValueField = "Module_Id";
                //drpMODULE_ID.DataBind();
                //drpMODULE_ID.Items.Insert(0, new ListItem("---Select---", "0"));

                DrpUser.Items.Clear();
                var Datas = (from fun12 in DB.USER_MST.Where(p => p.TenentID == TID && p.LOCATION_ID == LID)
                             select new
                             {
                                 Name = fun12.FIRST_NAME + "" + fun12.LAST_NAME,
                                 ID = fun12.USER_ID

                             }
         ).Distinct();
                DrpUser.DataSource = Datas;
                DrpUser.DataTextField = "Name";
                DrpUser.DataValueField = "ID";
                DrpUser.DataBind();
                DrpUser.Items.Insert(0, new ListItem("---Select---", "0"));
            }

        }

        protected void btnSaveACTIVITYRights_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0 && Convert.ToInt32(drplocation.SelectedValue) != 0 && Convert.ToInt32(drpMODULE_ID.SelectedValue) != 0 && Convert.ToInt32(DrpUser.SelectedValue) != 0)
            {
                TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                LID = Convert.ToInt32(drplocation.SelectedValue);
                MID = Convert.ToInt32(drpMODULE_ID.SelectedValue);
                int ModuleName = Convert.ToInt32(drpMODULE_ID.SelectedValue);
                UID = Convert.ToInt32(DrpUser.SelectedValue);
                //k//ListViewActivity.DataSource = DBCALLEM.CRMMainActivities.Where(p => p.TenantID == TID && p.ACTIVITYTYPE == ModuleName);
                ListViewActivity.DataBind();
            }
        }

        protected void btnCancelACTIVITYRights_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveRights_Click(object sender, EventArgs e)
        {
           foreach(ListViewDataItem item in ListViewActivity.Items)
           {
               HiddenField hiddenactivitycode=(HiddenField)item.FindControl("hiddenActivityID");
               int ACCODE=Convert.ToInt32(hiddenactivitycode.Value);
               CheckBox chadd=(CheckBox)item.FindControl("AddCheckBox1");
               CheckBox chUpdate=(CheckBox)item.FindControl("UpdateCheckBox1");
               CheckBox chDelete=(CheckBox)item.FindControl("DeleteCheckBox1");
               if (Convert.ToInt32(drplocation.SelectedValue) != 0 && Convert.ToInt32(DrpTENANT_ID.SelectedValue) != 0  && Convert.ToInt32(DrpUser.SelectedValue) != 0)
               {
                   int TID = Convert.ToInt32(DrpTENANT_ID.SelectedValue);
                   int LID = Convert.ToInt32(drplocation.SelectedValue);
                   int UID1 = Convert.ToInt32(DrpUser.SelectedValue);
                   if (DB.ACTIVITYRIGHTS.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.Activity_ID == ACCODE).Count() > 0)
                   {
                       Database.ACTIVITYRIGHT obj_Rights = DB.ACTIVITYRIGHTS.SingleOrDefault(p => p.TenentID == TID && p.LOCATION_ID == LID && p.Activity_ID == ACCODE);
                       obj_Rights.ADD_FLAG = chadd.Checked;
                       obj_Rights.MODIFY_FLAG = chUpdate.Checked;
                       obj_Rights.DELETE_FLAG = chDelete.Checked;
                       obj_Rights.USER_ID = UID1;
                       DB.SaveChanges();
                   }
               }
             
           }
        }

        public void Clear()
        {
            drpMODULE_ID.SelectedIndex = 0;
            DrpUser.SelectedIndex = 0;
            DrpTENANT_ID.SelectedIndex = 0;
            drplocation.SelectedIndex = 0;
            ListViewActivity.DataSource = null;
        }
    }
}