using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

namespace Web.Sales
{
    public partial class ReporForIPS2 : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        string FROM = "";
        string TO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // binddata();
                //ShowSaleReport();
                //ShowSaleReportDT();
                //paytermHD();
                 Default();
            }

        }
        public void Default()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
           
            //drpReportName.SelectedValue = "2";
            txtdateTO.Text = DateTime.Now.ToString("M/d/yyyy");
            txtdateFrom.Text = DateTime.Now.ToString("M/d/yyyy");            
        }      
        public string paidbypayterm(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (ID != 0)
            {
                string refid = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID).REFNAME1;
                return refid;
            }
            else
            {
                return null;
            }
        }
        public string CustVendID(int VendID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            if (VendID == 0)
            {
                return "IC";
            }
            else
            {
                string CustomerVenderID = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == VendID && p.TenentID == TID).COMPNAME1;
                return CustomerVenderID;
            }
        }
        public string CompAndUser(int MyTrnasID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int mytransID1 = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrnasID).MYTRANSID);
            string UserID = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrnasID).USERID;
            //string TransDocNo = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrnasID).TransDocNo;
            return mytransID1.ToString() + " / " + UserID;
        }
        public string PaidBy(int MyTrnasID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            var List = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MyTrnasID);
            if (List.Count() > 0)
            {
                //var List = result1.GroupBy(p => p.MyProdID).Select(p => p.FirstOrDefault()).ToList();
                //int ListPayTerm = Convert.ToInt32(List.GroupBy(p=>p.MyTransID).Select(p => p.FirstOrDefault()));
                //int payterm = Convert.ToInt32(ListPayTerm);
                int Paidby = Convert.ToInt32(DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MyTrnasID).PaymentTermsId);
                if (Paidby != 0)
                {
                    string refid = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Paidby).REFNAME1;
                    return refid;
                }
                else
                {
                    string refidd = "Cash";
                    return refidd;
                }
            }
            else
            {
                string refidd = "Cash";
                return refidd;
            }

        }      
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            if (txtdateFrom.Text != "")
            {
                Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            }

            if (txtdateTO.Text != "")
            {
                todate = Convert.ToDateTime(txtdateTO.Text);
            }
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);
            //if (Fromdate == DateTime.Now && todate == DateTime.Now)
            //{
                if (btnAdd.Text == "Sale Report (Today)")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.MainTranType == "O" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.TransDocNo).ToList();
                    Session["SaleHD"] = listitem;
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 1);                                                            
                }
            //}          
        }
        protected void btnSaleToDT_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            if (txtdateFrom.Text != "")
            {
                Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            }

            if (txtdateTO.Text != "")
            {
                todate = Convert.ToDateTime(txtdateTO.Text);
            }
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);
            //if (Fromdate == DateTime.Now && todate == DateTime.Now)
            //{
                if (btnSaleToDT.Text == "Sale Report Detailed (Today)")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    List<Database.ICTR_DT> List = new List<ICTR_DT>();
                    List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.MainTranType == "O" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                    foreach (Database.ICTR_HD item in listitem)
                    {
                        List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                        foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                        {
                            List.Add(item2);
                        }
                    }
                    Session["SaleDT"] = List;
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 2);
                }
            //}
        }

        protected void btnsaleToHDC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            if (txtdateFrom.Text != "")
            {
                Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            }

            if (txtdateTO.Text != "")
            {
                todate = Convert.ToDateTime(txtdateTO.Text);
            }
            if (Fromdate < DateTime.Now && todate < DateTime.Now)
            {
                if (btnsaleToHDC.Text == "Sale Report")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.MainTranType == "O" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.TransDocNo).ToList();
                    Session["SaleHD"] = listitem;
                    FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                    TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 3 + "&FROM=" + FROM + "&TO=" + TO);                   
                    //Response.Redirect("ReportIPS2Print.aspx?ID=" + 2 + "&FROM" + Fromdate + "&TO" + todate);
                }
            }
        }
        protected void btnsaleToDTC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            todate = Convert.ToDateTime(txtdateTO.Text);
            if(btnsaleToDTC.Text == "Sale Report Detailed")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.MainTranType == "O" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 4 + "&FROM=" + FROM + "&TO=" + TO);
            }
        }
        protected void btnsReturnHDCT_Click(object sender, EventArgs e)
        {          
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            if (btnsReturnHDCT.Text == "Sale Return (Today)")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 5101 && p.transsubid == 510101 && p.TransType == "Sales Return" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 5);
            }
        }
        protected void btnsReturnDTCT_Click(object sender, EventArgs e)
        {            
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            if (btnsReturnDTCT.Text == "Sale Return Detailed (Today)")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 5101 && p.transsubid == 510101 && p.TransType == "Sales Return" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 6);
            }
        }
        protected void btnsReturnHDC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            if (txtdateFrom.Text != "")
            {
                Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            }

            if (txtdateTO.Text != "")
            {
                todate = Convert.ToDateTime(txtdateTO.Text);
            }
            if (Fromdate <= DateTime.Now && todate <= DateTime.Now)
            {
                if (btnsReturnHDC.Text == "Sale Return")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 5101 && p.transsubid == 510101 && p.TransType == "Sales Return" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                    Session["SaleHD"] = listitem;
                    FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                    TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 7 + "&FROM=" + FROM + "&TO=" + TO);                    
                }
            }
        }
        protected void btnsReturnDTC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            todate = Convert.ToDateTime(txtdateTO.Text);
            if (btnsReturnDTC.Text == "Sale Return Detailed")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 5101 && p.transsubid == 510101 && p.TransType == "Sales Return" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 8 + "&FROM=" + FROM + "&TO=" + TO);
            }
        }
        protected void btnPURHDCT_Click(object sender, EventArgs e)
        {
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            if (btnPURHDCT.Text == "Purchase Report (Today)")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2101 && p.transsubid == 21011 && p.TransType == "Goods Received Note - Purchase" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 9);
            }
        }
        protected void btnPURDTCT_Click(object sender, EventArgs e)
        {
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            if (btnPURDTCT.Text == "Purchase Report Detailed (Today)")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2101 && p.transsubid == 21011 && p.TransType == "Goods Received Note - Purchase" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 10);
            }
        }
        protected void btnPURHDC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            if (txtdateFrom.Text != "")
            {
                Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            }

            if (txtdateTO.Text != "")
            {
                todate = Convert.ToDateTime(txtdateTO.Text);
            }
            if (Fromdate <= DateTime.Now && todate <= DateTime.Now)
            {
                if (btnPURHDC.Text == "Purchase Report")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2101 && p.transsubid == 21011 && p.TransType == "Goods Received Note - Purchase" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                    Session["SaleHD"] = listitem;
                    FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                    TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 11 + "&FROM=" + FROM + "&TO=" + TO);
                }
            }
        }
        protected void btnPURDTC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            todate = Convert.ToDateTime(txtdateTO.Text);
            if (btnPURDTC.Text == "Purchase Report Detailed")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2101 && p.transsubid == 21011 && p.TransType == "Goods Received Note - Purchase" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 12 + "&FROM=" + FROM + "&TO=" + TO);
            }
        }
        protected void btnPurReportHDCT_Click(object sender, EventArgs e)
        {
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            if (btnPurReportHDCT.Text == "Purchase Return (Today)")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2102 && p.transsubid == 21012 && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 13);
            }
        }
        protected void btnPurReportDTCT_Click(object sender, EventArgs e)
        {
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            if (btnPurReportDTCT.Text == "Purchase Return Detailed (Today)")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2102 && p.transsubid == 21012 && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 14);
            }
        }
        protected void btnPurReportHDC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            if (txtdateFrom.Text != "")
            {
                Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            }

            if (txtdateTO.Text != "")
            {
                todate = Convert.ToDateTime(txtdateTO.Text);
            }
            if (Fromdate <= DateTime.Now && todate <= DateTime.Now)
            {
                if (btnPurReportHDC.Text == "Purchase Return")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2102 && p.transsubid == 21012 && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                    Session["SaleHD"] = listitem;
                    FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                    TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 15 + "&FROM=" + FROM + "&TO=" + TO);
                }
            }
        }
        protected void btnPurReportDTC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            todate = Convert.ToDateTime(txtdateTO.Text);
            if (btnPurReportDTC.Text == "Purchase Return Detailed")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                List<Database.ICTR_DT> List = new List<ICTR_DT>();
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2102 && p.transsubid == 21012 && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;
                FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 16 + "&FROM=" + FROM + "&TO=" + TO);
            }
        }
        public string Foreign(int MytranId)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MytranId && p.TranType == "Foreign Invoice" && p.ACTIVE == true).Count() > 0)
            {
                string foreign = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "Foreign Invoice" && p.MYTRANSID == MytranId && p.ACTIVE == true).TranType.ToString();
                return foreign;
            }
            else
            {
                return "";
            }
        }
        public string Local(int MytranId)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MytranId && p.TranType == "POS Invoice" && p.ACTIVE == true).Count() > 0)
            {
                string local = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "POS Invoice" && p.MYTRANSID == MytranId && p.ACTIVE == true).TranType.ToString();
                return local;
            }
            else
            {
                return "";
            }
        }
        public string LocalPiece(int MytranId)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MytranId && p.TranType == "POS Invoice" && p.ACTIVE == true).Count() > 0)
            {
                string localpiece = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "POS Invoice" && p.MYTRANSID == MytranId && p.ACTIVE == true).TranType.ToString();
                return localpiece;
            }
            else
            {
                return "";
            }
        }
        public string ForeignPiece(int MytranId)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MytranId && p.TranType == "Foreign Invoice" && p.ACTIVE == true).Count() > 0)
            {
                string Foreignpiece = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "Foreign Invoice" && p.MYTRANSID == MytranId && p.ACTIVE == true).TranType.ToString();
                return Foreignpiece;
            }
            else
            {
                return "";
            }
        }
        public string InvoiceNODT(int MyTransID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MyTransID && p.ACTIVE == true).Count() > 0)
            {
                string transDocno = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransID && p.ACTIVE == true).TransDocNo;
                return transDocno;
            }
            else
            {
                return "";
            }
        }
        public string BaseCost(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == ID && p.ACTIVE == true).Count() > 0)
            {
                var ProdID = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ID && p.ACTIVE == true).FirstOrDefault().MyProdID;
                string basecost = (DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ProdID).basecost).ToString();
                return basecost.ToString();
            }
            else
            {
                return "0.00";
            }
        }

       

        

        

       

       
        //protected void lblExport_Click(object sender, EventArgs e)
        //{
        //    decimal TotAmt = 0;
        //    List<ViewData1> List = new List<ViewData1>();
        //    if (ViewState["Exele"] != null)
        //    {
        //        //blank
        //        ViewData1 blank = new ViewData1();
        //        blank.TransDate = "";
        //        blank.Trnsaction = "";
        //        blank.Invoice = "";
        //        blank.Amount = "";
        //        blank.Paidby = "";
        //        blank.Reference = "";
        //        blank.CusVender = "";
        //        List.Add(blank);
        //        //MAINHEADER
        //        ViewData1 MAINHEADER = new ViewData1();
        //        string Todate = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();
        //        string Fromdate = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();
        //        MAINHEADER.TransDate = "";
        //        MAINHEADER.Trnsaction = "";
        //        MAINHEADER.Invoice = "Sales Report" + "\n" + " Daily Sales Transactions" + " " + "From Date :-" + Todate + "To Date :-" + Fromdate + " All Transactions";
        //        //MAINHEADER.Invoice = "Daily Sales Transactions" + " From Date:-(" + Todate + ") To Date:-(" + Fromdate + ")";
        //        MAINHEADER.Amount = "";
        //        MAINHEADER.Paidby = "";
        //        MAINHEADER.Reference = "";
        //        MAINHEADER.CusVender = "";
        //        List.Add(MAINHEADER);
        //        //Header
        //        ViewData1 Header = new ViewData1();
        //        Header.TransDate = "DATE";
        //        Header.Trnsaction = "TRANSACTION";
        //        Header.Invoice = "TransDocNo";
        //        Header.Amount = "AMOUNT";
        //        Header.Paidby = "PAID BY";
        //        Header.Reference = "REFERENCE";
        //        Header.CusVender = "CUSTOMER/VENDER";
        //        List.Add(Header);
        //        //BLANKHEADER
        //        var date1 = DateTime.Now.ToString("dd/MMM/yyyy");
        //        var searial1 = DB.tbltranssubtypes.Single(p => p.transid == 4101 && p.transsubid == 410103 && p.transsubtype1 == "POS Invoice").serialno;
        //        ViewData1 BLANKHeader = new ViewData1();
        //        BLANKHeader.TransDate = date1 + "(" + date1 + ")";
        //        BLANKHeader.Trnsaction = "";
        //        BLANKHeader.Invoice = "";
        //        BLANKHeader.Amount = "";
        //        BLANKHeader.Paidby = "";
        //        BLANKHeader.Reference = "";
        //        BLANKHeader.CusVender = "";
        //        List.Add(blank);
        //        //MainHD
        //        List<ICTR_HD> Listitem = ((List<ICTR_HD>)ViewState["Exele"]).ToList();
        //        foreach (ICTR_HD Items in Listitem.Where(p=>p.ACTIVE == true))
        //        {
        //            ViewData1 obj = new ViewData1();
        //            obj.TransDate = Items.TRANSDATE.ToShortDateString();
        //            obj.Trnsaction = CompAndUser(Convert.ToInt32(Items.MYTRANSID)).ToString();
        //            obj.Invoice = Items.TransDocNo.ToString();
        //            obj.Amount = Items.TOTAMT.ToString();
        //            obj.Paidby = PaidBy(Convert.ToInt32(Items.MYTRANSID)).ToString();
        //            obj.Reference = Items.REFERENCE.ToString();
        //            obj.CusVender = CustVendID(Convert.ToInt32(Items.CUSTVENDID)).ToString();
        //            List.Add(obj);
        //            TotAmt += Convert.ToDecimal(Items.TOTAMT);
        //            ViewState["TotAMO"] = TotAmt;
        //            //List<ICTR_HD> List = DB.ICTR_HD.OrderBy(m => m.MYTRANSID).ToList();                     
        //        }
        //        decimal AMT = Convert.ToDecimal(ViewState["TotAMO"]);
        //        ViewData1 obj1 = new ViewData1();
        //        obj1.TransDate = "";
        //        obj1.Trnsaction = "";
        //        obj1.Invoice = "Total:->";
        //        obj1.Amount = AMT.ToString();
        //        obj1.Paidby = "";
        //        obj1.Reference = "";
        //        obj1.CusVender = "";
        //        List.Add(obj1);

        //        var date = DateTime.Now.ToString("dd/MMM/yyyy");
        //        var searial = DB.tbltranssubtypes.Single(p => p.transid == 4101 && p.transsubid == 410103 && p.transsubtype1 == "POS Invoice").serialno;
        //        ((Sales_Master)this.Master).ExportToExcel<ViewData>(List, date + "(" + searial + ")");
        //    }

        //}
        //protected void lblexportDT_Click(object sender, EventArgs e)
        //{

        //    decimal AMTTOT = 0;
        //    decimal UNIT = 0;
        //    decimal Costs = 0;
        //    decimal GrossPr = 0;
        //    List<ViewDataDT> List = new List<ViewDataDT>();
        //    ViewDataDT Blank = new ViewDataDT();
        //    Blank.Trans1 = "";
        //    Blank.TransDate1 = "";
        //    //Blank.Reference = "";
        //    Blank.InvoiceTotal1 = "";
        //    Blank.MYTRANSID1 = "";
        //    Blank.invoiceno1 = "";
        //    Blank.DESCRIPTION1 = "";
        //    Blank.QUANTITY1 = "";
        //    Blank.UNITPRICELocal1 = "";
        //    Blank.Cost1 = "";
        //    Blank.GrossProfit1 = "";
        //    Blank.AMOUNTLocal1 = "";
        //    List.Add(Blank);
        //    //Step1
        //    ViewDataDT obj = new ViewDataDT();
        //    List<ICTR_HD> ListitemHD = ((List<ICTR_HD>)ViewState["Exele"]).ToList();
        //    long MYTrans = Convert.ToInt64(ViewState["HD"]);
        //    string Fromdate = Convert.ToDateTime(txtdateFrom.Text).ToShortDateString();
        //    string Todate = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();            
        //    obj.Trans1 = "";
        //    obj.TransDate1 = "";
        //    //obj.Reference = "";
        //    obj.InvoiceTotal1 = "";
        //    obj.MYTRANSID1 = "";
        //    obj.invoiceno1 = "";
        //    obj.DESCRIPTION1 = "____________Sales Report" + "" + "________ _Daily Sales Transactions______From Date :-" + Fromdate + "______ _________To Date :-" + Todate + "__________________ All Transactions___________";
        //        //"____________Sales Report ___________________Daily Sales Transactions________"+""+"______From Date :-" +MYTrans+ "_______________To Date :- "++"_______"___________All Transactions___________"
        //        //For The Transaction:-" + MYTrans + "/2017 " + "All Transaction";
        //    obj.QUANTITY1 = "";
        //    obj.UNITPRICELocal1 = "";
        //    obj.Cost1 = "";
        //    obj.GrossProfit1 = "";
        //    obj.AMOUNTLocal1 = "";
        //    List.Add(obj);
        //    //DT Header
        //    ViewDataDT objDTHeader = new ViewDataDT();
        //    objDTHeader.Trans1 = "SALInvoice#";
        //    objDTHeader.TransDate1 = "";
        //    //objDTHeader.Reference = "";
        //    objDTHeader.InvoiceTotal1 = "";
        //    objDTHeader.MYTRANSID1 = "";
        //    objDTHeader.invoiceno1 = "";
        //    objDTHeader.DESCRIPTION1 = "";
        //    objDTHeader.QUANTITY1 = "";
        //    objDTHeader.UNITPRICELocal1 = "";
        //    objDTHeader.Cost1 = "";
        //    objDTHeader.GrossProfit1 = "";
        //    objDTHeader.AMOUNTLocal1 = "";
        //    List.Add(objDTHeader);
        //    //DT Header2
        //    ViewDataDT HeaderDT = new ViewDataDT();
        //    HeaderDT.Trans1 = "Trans";
        //    HeaderDT.TransDate1 = "TransDate";
        //    //HeaderDT.Reference = "Reference";
        //    HeaderDT.InvoiceTotal1 = "Customer";
        //    HeaderDT.MYTRANSID1 = "ITEM CODE";
        //    HeaderDT.invoiceno1 = "TransDocNo#";
        //    HeaderDT.DESCRIPTION1 = "DESCRIPTION";
        //    HeaderDT.QUANTITY1 = "QTY";
        //    HeaderDT.UNITPRICELocal1 = "Unit Price";
        //    HeaderDT.Cost1 = "Cost";
        //    HeaderDT.GrossProfit1 = "GrossProfit";
        //    HeaderDT.AMOUNTLocal1 = "Amount";
        //    List.Add(HeaderDT);

        //    //Step2
        //    if (ChkAllHDDT.Checked == true)
        //    {
        //        List<ICTR_HD> Listitemhd = ((List<ICTR_HD>)ViewState["Exele"]).ToList();
        //        foreach (ICTR_HD itemshd in Listitemhd.Where(p=>p.ACTIVE == true))
        //        {
        //            //Step first four field HD in DT an after DT data all
        //            if (ViewState["Exele"] != null)
        //            {
        //                if (ViewState["HD"] != null)
        //                {
        //                    long step5MYTRANSID = Convert.ToInt64(itemshd.MYTRANSID);
        //                    List<ICTR_DT> Listitemdt = DB.ICTR_DT.Where(p => p.MYTRANSID == step5MYTRANSID && p.ACTIVE == true).ToList();
        //                    foreach (ICTR_DT Itemsdt in Listitemdt)
        //                    {
        //                        //
        //                        ViewDataDT objHD = new ViewDataDT();
        //                        objHD.MYTRANSID1 = itemshd.TRANSDATE.ToShortDateString();
        //                        objHD.invoiceno1 = CompAndUser(Convert.ToInt32(itemshd.MYTRANSID)).ToString();
        //                        objHD.DESCRIPTION1 = itemshd.REFERENCE.ToString();
        //                        objHD.QUANTITY1 = CustVendID(Convert.ToInt32(itemshd.CUSTVENDID)).ToString();
        //                        //objHD.QUANTITY = itemshd.TransDocNo.ToString();

        //                        ViewDataDT obj5 = new ViewDataDT();
        //                        obj5.Trans1 = itemshd.TransDocNo.ToString() + "/" + objHD.invoiceno.ToString() + "/" + objHD.DESCRIPTION.ToString();
        //                        obj5.TransDate1 = objHD.MYTRANSID.ToString();
        //                        //obj5.Reference = objHD.DESCRIPTION.ToString();
        //                        obj5.InvoiceTotal1 = objHD.QUANTITY.ToString();
        //                        obj5.MYTRANSID1 = Itemsdt.MyProdID.ToString();
        //                        obj5.invoiceno1 = InvoiceNODT(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString();
        //                        obj5.DESCRIPTION1 = Itemsdt.DESCRIPTION.ToString();
        //                        obj5.QUANTITY1 = Itemsdt.QUANTITY.ToString();                                
        //                        obj5.UNITPRICELocal1 = LocalPiece(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(Itemsdt.UNITPRICE)).ToString() : "";
        //                        //var costDT = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.MYTRANSID == Itemsdt.MYTRANSID).FirstOrDefault().CostAmount);
        //                        var costDT = Convert.ToDecimal(Itemsdt.CostAmount);
        //                        obj5.Cost1 = costDT.ToString(); //BaseCost(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString();
        //                        double UnitPR = obj5.UNITPRICELocal == "" ? 0 : Convert.ToDouble(obj5.UNITPRICELocal);//Convert.ToDouble(Itemsdt.QUANTITY);
        //                        double Cossts = Convert.ToDouble(obj5.Cost);
        //                        double Gross = UnitPR - Cossts;
        //                        obj5.GrossProfit1 = Gross.ToString();
        //                        obj5.AMOUNTLocal1 = Local(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(Itemsdt.AMOUNT)).ToString() : "";
        //                        List.Add(obj5);
        //                        AMTTOT += obj5.AMOUNTLocal == "" ? 0 : Convert.ToDecimal(obj5.AMOUNTLocal);
        //                        UNIT += obj5.UNITPRICELocal == "" ? 0 : Convert.ToDecimal(obj5.UNITPRICELocal);
        //                        Costs += Convert.ToDecimal(obj5.Cost);
        //                        GrossPr += Convert.ToDecimal(Gross);
        //                        //ViewState["step5MYTRANSID"] = items.MYTRANSID.ToString();
        //                        ViewState["AMTTOT"] = AMTTOT;
        //                        ViewState["Unit"] = UNIT;
        //                        ViewState["Costs"] = Costs;
        //                        ViewState["GrossPr"] = GrossPr;
        //                    }
        //                }
        //            }
        //        }
        //        //Total(Amount&UnitPrice)
        //        decimal AMTHD = Convert.ToDecimal(ViewState["AMTTOT"]);
        //        decimal UnitHD = Convert.ToDecimal(ViewState["Unit"]);
        //        decimal Cosst = Convert.ToDecimal(ViewState["Costs"]);
        //        decimal groos = Convert.ToDecimal(ViewState["GrossPr"]);
        //        ViewDataDT Total = new ViewDataDT();
        //        Total.MYTRANSID1 = "";
        //        Total.invoiceno1 = "";
        //        Total.DESCRIPTION1= "";
        //        Total.QUANTITY1 = "Total :->";
        //        Total.UNITPRICELocal1 = UnitHD.ToString();
        //        Total.Cost1 = Cosst.ToString();
        //        Total.GrossProfit1 = groos.ToString();
        //        Total.AMOUNTLocal1 = AMTHD.ToString();
        //        List.Add(Total);
        //    }
        //    else
        //    {
        //        if (ViewState["exportDT"] != null)
        //        {

        //            List<ICTR_DT> ListitemDT = ((List<ICTR_DT>)ViewState["exportDT"]).ToList();
        //            foreach (ICTR_DT items in ListitemDT)
        //            {
        //                //Step first four field HD in DT an after DT data all
        //                if (ViewState["Exele"] != null)
        //                {
        //                    if (ViewState["HD"] != null)
        //                    {
        //                        long step5MYTRANSID = Convert.ToInt64(ViewState["HD"]);
        //                        List<ICTR_HD> Listitem = DB.ICTR_HD.Where(p => p.MYTRANSID == step5MYTRANSID).ToList();
        //                        foreach (ICTR_HD Items in Listitem)
        //                        {
        //                            //
        //                            ViewDataDT obj5 = new ViewDataDT();
        //                            obj5.MYTRANSID1 = Items.TRANSDATE.ToShortDateString();
        //                            obj5.invoiceno1 = CompAndUser(Convert.ToInt32(Items.MYTRANSID)).ToString();
        //                            obj5.DESCRIPTION1 = Items.REFERENCE.ToString();
        //                            obj5.QUANTITY1 = CustVendID(Convert.ToInt32(Items.CUSTVENDID)).ToString();
        //                            //obj5.QUANTITY = Items.TransDocNo.ToString();
        //                            //List.Add(obj5);
        //                            //                                
        //                            ViewDataDT objDT = new ViewDataDT();
        //                            objDT.Trans1 = Items.TransDocNo.ToString() + "/" + obj5.invoiceno.ToString() + "/" + obj5.DESCRIPTION.ToString();
        //                            objDT.TransDate1 = obj5.MYTRANSID.ToString();
        //                            //objDT.Reference = obj5.DESCRIPTION.ToString();
        //                            objDT.InvoiceTotal1 = obj5.QUANTITY.ToString();
        //                            objDT.MYTRANSID1 = items.MyProdID.ToString();
        //                            objDT.invoiceno1 = InvoiceNODT(Convert.ToInt32(items.MYTRANSID)).ToString();
        //                            objDT.DESCRIPTION1 = items.DESCRIPTION.ToString();
        //                            objDT.QUANTITY1 = items.QUANTITY.ToString();
        //                            objDT.UNITPRICELocal1 = LocalPiece(Convert.ToInt32(items.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(items.UNITPRICE)).ToString() : "";
        //                            //var costDT = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.MYTRANSID == items.MYTRANSID).FirstOrDefault().CostAmount);
        //                            var costDT = Convert.ToDecimal(items.CostAmount);
        //                            objDT.Cost1 = costDT.ToString(); //BaseCost(Convert.ToInt32(items.MYTRANSID)).ToString();
        //                            double unitPR = objDT.UNITPRICELocal == "" ? 0 : Convert.ToDouble(objDT.UNITPRICELocal);//Convert.ToDouble(objDT.QUANTITY);                                    
        //                            double Cossts = Convert.ToDouble(objDT.Cost);
        //                            double Gross = unitPR - Cossts;                                   
        //                            objDT.GrossProfit1 = Gross.ToString();
        //                            objDT.AMOUNTLocal1 = Local(Convert.ToInt32(items.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(items.AMOUNT)).ToString() : "";
        //                            List.Add(objDT);
        //                            AMTTOT += objDT.AMOUNTLocal == "" ? 0 : Convert.ToDecimal(objDT.AMOUNTLocal);
        //                            UNIT += objDT.UNITPRICELocal == "" ? 0 : Convert.ToDecimal(objDT.UNITPRICELocal);
        //                            Costs += Convert.ToDecimal(objDT.Cost);
        //                            GrossPr += Convert.ToDecimal(objDT.GrossProfit);
        //                            //ViewState["step5MYTRANSID"] = items.MYTRANSID.ToString();
        //                            ViewState["AMTTOT"] = AMTTOT;
        //                            ViewState["Unit"] = UNIT;
        //                            ViewState["Costs"] = Costs;
        //                            ViewState["GrossPr"] = GrossPr;
        //                        }
        //                    }
        //                }
        //            }
        //            //Total(Amount&UnitPrice)
        //            decimal AMTHD = Convert.ToDecimal(ViewState["AMTTOT"]);
        //            decimal UnitHD = Convert.ToDecimal(ViewState["Unit"]);
        //            decimal Cosst = Convert.ToDecimal(ViewState["Costs"]);
        //            decimal groos = Convert.ToDecimal(ViewState["GrossPr"]);
        //            ViewDataDT Total = new ViewDataDT();
        //            Total.Trans1 = "";
        //            Total.TransDate1 = "";
        //            //HeaderDT.Reference = "Reference";
        //            Total.InvoiceTotal1 = "";
        //            Total.MYTRANSID1 = "";
        //            Total.invoiceno1 = "";
        //            Total.DESCRIPTION1 = "";
        //            Total.QUANTITY1 = "Total :->";
        //            Total.UNITPRICELocal1 = UnitHD.ToString();
        //            Total.Cost1 = Cosst.ToString();
        //            Total.GrossProfit1 = groos.ToString();
        //            Total.AMOUNTLocal1 = AMTHD.ToString();
        //            List.Add(Total);

        //        }
        //    }

        //    //Step3
        //    for (int i = 1; i <= 2; i++)
        //    {
        //        ViewDataDT obj3 = new ViewDataDT();
        //        obj3.MYTRANSID1 = "";
        //        obj3.invoiceno1 = "";
        //        obj3.DESCRIPTION1 = "";
        //        obj3.QUANTITY1 = "";
        //        obj3.UNITPRICELocal1 = "";
        //        obj3.AMOUNTLocal1 = "";
        //        List.Add(obj3);
        //    }
        //    //Step4
        //    //ViewDataDT obj4 = new ViewDataDT();
        //    //var searial = DB.tbltranssubtypes.Single(p => p.transid == 4101 && p.transsubid == 410103 && p.transsubtype1 == "POS Invoice").serialno;
        //    //var date = DateTime.Now.ToString("dd/MMM/yyyy");
        //    //obj4.MYTRANSID = date + "(" + searial + ")";
        //    //obj4.invoiceno = "";
        //    //obj4.DESCRIPTION = "";
        //    //obj4.QUANTITY = "";
        //    //obj4.UNITPRICELocal = "";
        //    //obj4.AMOUNTLocal = "";
        //    //List.Add(obj4);
        //    //HD Header 
        //    //ViewDataDT objHDHeader = new ViewDataDT();
        //    //objHDHeader.MYTRANSID = "Date";
        //    //objHDHeader.invoiceno = "Transaction";
        //    //objHDHeader.DESCRIPTION = "Reference";
        //    //objHDHeader.QUANTITY = "Custermer/vender";
        //    //objHDHeader.UNITPRICELocal = "";
        //    //objHDHeader.AMOUNTLocal = "";
        //    //List.Add(objHDHeader);
        //    //step5

        //    //if (ViewState["Exele"] != null)
        //    //{
        //    //    if (ViewState["step5MYTRANSID"] != null)
        //    //    {
        //    //        long step5MYTRANSID = Convert.ToInt64(ViewState["step5MYTRANSID"]);
        //    //        List<ICTR_HD> Listitem = DB.ICTR_HD.Where(p => p.MYTRANSID == step5MYTRANSID).ToList();
        //    //        foreach (ICTR_HD Items in Listitem)
        //    //        {
        //    //            ViewDataDT obj5 = new ViewDataDT();
        //    //            obj5.MYTRANSID = Items.TRANSDATE.ToShortDateString();
        //    //            obj5.invoiceno = CompAndUser(Convert.ToInt32(Items.MYTRANSID)).ToString();
        //    //            obj5.DESCRIPTION = Items.REFERENCE.ToString();
        //    //            obj5.QUANTITY = CustVendID(Convert.ToInt32(Items.CUSTVENDID)).ToString();
        //    //            obj5.UNITPRICELocal = "";
        //    //            obj5.AMOUNTLocal = "";
        //    //            List.Add(obj5);
        //    //        }
        //    //    }
        //    //}

        //    ((Sales_Master)this.Master).ExportToExcel<ViewDataDT>(List, "SALInvoice#");
        //}

    }
    //public class ViewData1
    //{
    //    public string TransDate1 { get; set; }
    //    public string Trnsaction1 { get; set; }
    //    public string Invoice1 { get; set; }
    //    public string Amount1 { get; set; }
    //    public string Paidby1 { get; set; }
    //    public string Reference1 { get; set; }
    //    public string CusVender1 { get; set; }
    //}
    //public class ViewDataDT1
    //{
    //    public string Trans1 { get; set; }
    //    public string TransDate1 { get; set; }
    //    //public string Reference { get; set; }
    //    public string InvoiceTotal1 { get; set; }
    //    public string MYTRANSID1 { get; set; }
    //    public string invoiceno1 { get; set; }
    //    public string DESCRIPTION1 { get; set; }
    //    public string QUANTITY1 { get; set; }
    //    public string UNITPRICELocal1 { get; set; }
    //    public string Cost1 { get; set; }
    //    public string GrossProfit1 { get; set; }
    //    public string AMOUNTLocal1 { get; set; }

    //}
}