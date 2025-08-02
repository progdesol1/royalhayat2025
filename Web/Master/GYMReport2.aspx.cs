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


namespace Web.Master
{
    public partial class GYMReport2 : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        string FROM = "";
        string TO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 1);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 2);
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
                    Response.Redirect("GYMReport2Print.aspx?ID=" + 3 + "&FROM=" + FROM + "&TO=" + TO);
                    //Response.Redirect("GYMReport2Print.aspx?ID=" + 2 + "&FROM" + Fromdate + "&TO" + todate);
                }
            }
        }
        protected void btnsaleToDTC_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? todate = null;
            Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            todate = Convert.ToDateTime(txtdateTO.Text);
            if (btnsaleToDTC.Text == "Sale Report Detailed")
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 4 + "&FROM=" + FROM + "&TO=" + TO);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 5);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 6);
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
                    Response.Redirect("GYMReport2Print.aspx?ID=" + 7 + "&FROM=" + FROM + "&TO=" + TO);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 8 + "&FROM=" + FROM + "&TO=" + TO);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 9);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 10);
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
                    Response.Redirect("GYMReport2Print.aspx?ID=" + 11 + "&FROM=" + FROM + "&TO=" + TO);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 12 + "&FROM=" + FROM + "&TO=" + TO);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 13);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 14);
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
                    Response.Redirect("GYMReport2Print.aspx?ID=" + 15 + "&FROM=" + FROM + "&TO=" + TO);
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
                Response.Redirect("GYMReport2Print.aspx?ID=" + 16 + "&FROM=" + FROM + "&TO=" + TO);
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










    }
}