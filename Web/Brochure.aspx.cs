using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Brochure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["b"] == "CompleteERPSolution")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bcompleteERPSolution.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "POSSystem")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bPOSSystem.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "GroupyTrack")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bGroupyTrack.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "SubscriptionManagementSystem")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/hb.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "RecruitmentPortalSAASCloud")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bRecruitmentPortal.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "EcommerceonlineSAASBased")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bEcommerce.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "GYMManagementSoftware")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bGYM.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "DocumentManagementSystem")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bDMS.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "EventManagementSoftwareSAAS")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bEventManagement.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "SchoolManagementSoftware")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bSchoolManagement.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "MobileShopManagement")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bMobileShop.aspx");//List[0].LINK
                }
                else if (Request.QueryString["b"] == "HelpDeskManagementSystem")
                {
                    ifrm.Attributes.Add("src", "https://pos53.com/bHelpDesk.aspx");//List[0].LINK
                }
            }
        }
    }
}