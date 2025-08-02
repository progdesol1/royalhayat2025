using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class WebCKEDITORtest : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Itembind();
            }
        }

        protected void btntestCK_Click(object sender, EventArgs e)
        {
            Database.test obj = new Database.test();
            obj.Address = CKEProDtlOverview.Text.ToString();
            DB.tests.AddObject(obj);
            DB.SaveChanges();
        }
        public void Itembind()
        {
            Listview1.DataSource = DB.tests;
            Listview1.DataBind();
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.test obj = DB.tests.Single(p => p.id == ID);
                CKEProDtlOverview.Text = obj.Address.ToString();

            }
        }


    }
}