using Database;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.POS
{
    public partial class POSPrintRpt : System.Web.UI.Page
    {
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                if (Request.QueryString["sales_id"] != null)
                {
                    int sales_id = Convert.ToInt32(Request.QueryString["sales_id"]);
                    renderReport(sales_id);
                }
            }
        }

        private void renderReport(int sales_id)
        {
            try
            {
                string sql = " SELECT  sp.sales_id AS salesid, sp.payment_type AS paytype, sp.payment_amount AS Payamount, " +
                             " sp.change_amount AS charAmt, sp.due_amount AS due, sp.dis,sp.Delivery_Cahrge, sp.vat, sp.sales_time AS s_time,  " +
                             " sp.c_id AS custID, sp.emp_id AS empID, sp.comment AS Note, sp.TrxType, si.sales_id,si.item_id,  " +
                             " si.itemName,si.UOM,si.product_name_print, si.Qty, si.RetailsPrice, si.Total,si.profit, " +
                             " si.sales_time ,si.BatchNo,si.ExpiryDate, sp.Shopid, tl.*,c.* , sc.LOGO ,  " +
                             " CASE     WHEN si.taxapply = 1 THEN 'TX'  ELSE ''  END 'TaxApply'  " +
                             " FROM   Win_sales_payment sp " +
                             " INNER JOIN   Win_sales_item si ON sp.sales_id  = si.sales_id and sp.TenentID  = si.TenentID " +
                             " INNER JOIN Win_tbl_terminalLocation tl ON sp.Shopid  = tl.Shopid and sp.TenentID  = tl.TenentID " +
                             " INNER JOIN Win_tbl_customer c ON  sp.c_id  = c.ID and sp.TenentID  = c.TenentID " +
                             " INNER JOIN Win_storeconfig sc ON  sp.TenentID  = sc.TenentID and sp.TenentID  = sc.TenentID  " +
                             " Where sp.TenentID = " + TID + " and sp.sales_id  = '" + sales_id + "' and return_id=0";
                DataTable dt = DataCon.GetDataTable(sql);
                string img = "POSsmall.png";
                if (dt!=null)
                {
                    img = dt.Rows[0]["LOGO"].ToString();
                }
                
                string sqlpay = " SELECT * from Win_sales_payment Where TenentID = " + TID + " and sales_id= '" + sales_id + "' and return_id=0 ";
                DataTable dtpay = DataCon.GetDataTable(sqlpay);

                ReportDataSource reportDSDetail = new ReportDataSource("DataSetPrint", dt);
                ReportDataSource reportDSPayment = new ReportDataSource("DataSet1", dtpay);

                ReportViewer1.Reset();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/pos/RptPOS.rdlc");
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(reportDSDetail);
                this.ReportViewer1.LocalReport.DataSources.Add(reportDSPayment);
                //this.reportViewer1.LocalReport.Refresh();                
                this.ReportViewer1.ZoomMode = ZoomMode.PageWidth;
                this.ReportViewer1.LocalReport.EnableExternalImages = true;
                string FilePath = "";
                try
                {
                    FilePath = @"file:\" + Server.MapPath("~/assets/" + img); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                }
                catch
                {
                    FilePath = @"file:\" + Server.MapPath("~/assets/POSsmall.png");
                }

                if(!File.Exists(FilePath))
                {
                    img =  "POSsmall.png";
                    FilePath = @"file:\" + Server.MapPath("~/assets/" + img);
                }

                ReportParameter[] param = new ReportParameter[1];
                param[0] = new ReportParameter("ImgPath", FilePath);
                this.ReportViewer1.LocalReport.SetParameters(param);
                

            }
            catch (Exception e)
            {
            }

        }
    }
}