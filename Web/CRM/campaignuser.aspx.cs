using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database; 

namespace Web.CRM
{
    public partial class campaignuser : System.Web.UI.Page
    {
        int TID = 1;
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();

            }
        }
        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {

                int MyPRODID = Convert.ToInt32(e.CommandArgument);
                DropDownList drptype = (DropDownList)e.Item.FindControl("drptype");
                int ID = DB.Campaign_MasterCust.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_MasterCust.Where(p => p.TenentID == TID).Max(p => p.MySerialNumber) + 1) : 1;
                Database.Campaign_MasterCust objCampaign_Master = new Database.Campaign_MasterCust();
                objCampaign_Master.TenentID = TID;
                objCampaign_Master.LocationID = 1;
                objCampaign_Master.CampaignID = Convert.ToInt32(drptype.SelectedValue);
                objCampaign_Master.MySerialNumber = ID;
                objCampaign_Master.CustomerID = MyPRODID;
                //Button btnEdit = (Button)e.Item.FindControl("btnEdit");
                //btnEdit.Text = "Applied";
                DB.Campaign_MasterCust.AddObject(objCampaign_Master);
                DB.SaveChanges();

                String url = "insert new record in Campaign_MasterCust with " + "TenentID = " + TID + "ID =" + ID;
                String evantname = "create";
                String tablename = "Campaign_MasterCust";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                BindData();
            }

        }
        public void BindData()
        {


            List<Database.USER_MST> List = DB.USER_MST.Where(m => m.TenentID == 6).OrderBy(m => m.USER_ID).ToList();
            Listview2.DataSource = List;
            Listview2.DataBind();
        }

        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //btnEdit
            LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
            Label lbluser = (Label)e.Item.FindControl("lbluser");
            int CustomerID = Convert.ToInt32(lbluser.Text);
            DropDownList drptype = (DropDownList)e.Item.FindControl("drptype");


            if (DB.Campaign_MasterCust.Where(p => p.TenentID == TID && p.CustomerID == CustomerID).Count() > 0)
            {
                var obj = DB.Campaign_MasterCust.Single(p => p.TenentID == TID && p.CustomerID == CustomerID);
                drptype.SelectedValue = obj.CampaignID.ToString();
                btnEdit.Text = "Applied";
            }
            else
            {
                btnEdit.Text = "Apply";
            }


        }



    }
}