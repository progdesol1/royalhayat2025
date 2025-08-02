using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Database;
using System.Collections.Generic;
using System.Transactions;
using Database;
using Web.CRM.Class;
namespace Web.CRM.UserControl
{
    public partial class AddReference : System.Web.UI.UserControl
    {
        Database.CallEntities DB = new Database.CallEntities();
        int Type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            if (Session["PageType"] != null)
            {
                Type = Convert.ToInt32(Session["PageType"]);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Web.CRM.Class.AddReference.InsertDataRefTable(TID, txtAddReference3.Text, Type);
            }
           
           
        }
        protected void btnCancel2_Click(object sender, EventArgs e)
        {
           // Response.Redirect("index.aspx");
        }
    }
}