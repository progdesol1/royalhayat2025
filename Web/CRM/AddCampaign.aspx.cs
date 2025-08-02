using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.CRM
{
    public partial class AddCampaign : System.Web.UI.Page
    {
        int TID = 1;
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                BindData();
            }
        }
        public void BindData()
        {
            List<Database.Campaign_Master> List = DB.Campaign_Master.Where(m => m.TenentID == TID && m.CActive == true).OrderBy(m => m.CampaignID).ToList();
            Listview2.DataSource = List;
            Listview2.DataBind();
        }
        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                int MAXID = Convert.ToInt32(e.CommandArgument);
                // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.Campaign_Master objSOJobDesc = DB.Campaign_Master.Single(p => p.CampaignID == MAXID && p.TenentID == TID);
                DB.Campaign_Master.DeleteObject(objSOJobDesc);
                // DB.SaveChanges();
                BindData();

            }

            if (e.CommandName == "btnEdit")
            {
                int MAXID = Convert.ToInt32(e.CommandArgument);
                //  int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.Campaign_Master objtblsetupsalesh = DB.Campaign_Master.Single(p => p.CampaignID == MAXID && p.TenentID == TID);

                ViewState["Edit"] = MAXID;
                if (objtblsetupsalesh.CampaignType == 1)
                {
                    Response.Redirect("SpendBasedCampaign.aspx?CampaignID=" + MAXID);
                }
                else if (objtblsetupsalesh.CampaignType == 2)
                {
                    Response.Redirect("Productcampaign.aspx?CampaignID=" + MAXID);
                }
                else
                {

                }

            }
        }


        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CampaignType.aspx");
        }



    }
}