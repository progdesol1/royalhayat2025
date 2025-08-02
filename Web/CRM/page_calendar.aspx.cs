using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.CRM
{
    public partial class page_calendar : System.Web.UI.Page
    {
        CallEntities DB1 = new CallEntities();
        //CallEMEntities DB = new Database.CallEMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                listBind();
            }
            
        }

        protected void event_add_Click(object sender, EventArgs e)
        {
            Database.CalandarEventMaster obj_cal = new Database.CalandarEventMaster();
            obj_cal.EventName = txtevent.Text;
            obj_cal.Createdby = 1;
            obj_cal.DateTime = DateTime.Now;
            obj_cal.Deleted = true;


            DB1.CalandarEventMasters.AddObject(obj_cal);
            DB1.SaveChanges();
            clear();
            
        }
        public void clear()
        {
            txtevent.Text = "";

        }

        private void listBind()
        {
            listevent.DataSource = DB1.CalandarEventMasters;
            listevent.DataBind();

        }

    }
}