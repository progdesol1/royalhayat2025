using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Microsoft.Reporting.WebForms;

namespace Web
{
    public partial class RAFTABLAERPT : System.Web.UI.Page
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
            int TID =Convert.ToInt32( Request.QueryString["TID"]);
            string REFTYPE = Request.QueryString["REFTYPE"];
            string REFSUBTYPE = Request.QueryString["REFSUBTYPE"];

            ReportViewer1.Reset();

            ReportViewer1.LocalReport.EnableExternalImages = true;

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/ReportMst/RafTableReport.rdlc");

            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RafReport", DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE)));
        }
    }
}