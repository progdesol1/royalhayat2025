using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.CRM
{
    public partial class CampaignType : System.Web.UI.Page
    {
        int TID = 1;
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if(!IsPostBack)
            {

            }
        }

        protected void btntype1_Click(object sender, EventArgs e)
        {

            if (DB.REFTABLEs.Where(m => m.TenentID == TID && m.REFTYPE == "Loyality" && m.REFSUBTYPE == "CampaignType" && m.SWITCH1 == "1").Count() > 0)
            {
                Response.Redirect("SpendBasedCampaign.aspx");
            }
            else
            {
                Response.Redirect("CampaignType.aspx");
            }


        }

        protected void btntype2_Click(object sender, EventArgs e)
        {
            if (DB.REFTABLEs.Where(m => m.TenentID == TID && m.REFTYPE == "Loyality" && m.REFSUBTYPE == "CampaignType" && m.SWITCH1 == "2").Count() > 0)
            {
                Response.Redirect("ProductCampaign.aspx");
            }
            else
            {
                Response.Redirect("CampaignType.aspx");
            }

        }
    }
}