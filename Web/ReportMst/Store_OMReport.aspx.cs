using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Web.ReportMst
{
    public partial class Store_OMReport : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            if (!IsPostBack)
            {
               
            }

        }







    }
}