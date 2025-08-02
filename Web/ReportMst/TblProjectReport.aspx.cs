using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Microsoft.Reporting.WebForms;
namespace Web.ReportMst
{
    public partial class TblProjectReport : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                renderReport();
            }
        }
        private void renderReport()
        {
            int TID = Convert.ToInt32(Request.QueryString["TID"]);


            ReportViewer1.Reset();

            ReportViewer1.LocalReport.EnableExternalImages = true;

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportMst/TableProjectRpt.rdlc");

            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TableProject", DB.TBLPROJECTs.Where(p => p.TenentID == TID && p.ACTIVE == true)));
        }
    }
}