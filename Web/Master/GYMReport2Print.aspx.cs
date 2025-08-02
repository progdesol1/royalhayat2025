using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;


namespace Web.Master
{
    public partial class GYMReport2Print : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        decimal TotalGross = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PNLSaleHD.Visible = false;
                PNLSaleDT.Visible = false;
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                int TRCID = Convert.ToInt32(Request.QueryString["ID"]);
                string TodayDate = DateTime.Now.ToString("dd/MMM/yyyy");
                string From = (Request.QueryString["FROM"]);
                string To = (Request.QueryString["TO"]);
                if (Request.QueryString["ID"] != null)
                {
                    if (TRCID == 1)
                    {
                        lblHeader.Text = "Daily Sales Report Consolildated (" + TodayDate + ")";
                    }
                    else if (TRCID == 3)
                    {
                        lblHeader.Text = "Daily Sales Report Consolildated";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 5)
                    {
                        lblHeader.Text = "Daily Sale Return Consolildated (" + TodayDate + ")";
                    }
                    else if (TRCID == 7)
                    {
                        lblHeader.Text = "Daily Sales Return Consolildated";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 9)
                    {
                        lblHeader.Text = "Daily Purchase Report Consolildated (" + TodayDate + ")";

                    }
                    else if (TRCID == 11)
                    {
                        lblHeader.Text = "Daily Purchase Report Consolildated";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 13)
                    {
                        lblHeader.Text = "Daily Purchase Return Consolildated (" + TodayDate + ")";
                    }
                    else if (TRCID == 15)
                    {
                        lblHeader.Text = "Daily Purchase Return Consolildated";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 17)
                    {
                        lblHeader.Text = "Sales Report";
                    }
                    else if (TRCID == 18)
                    {
                        lblHeader.Text = "Sales Return";
                    }
                    //string from1 = (Request.QueryString["FROM"]).ToString();
                    //string to1 = (Request.QueryString["TO"]).ToString();
                    if (Session["SaleHD"] != null)
                    {
                        PNLSaleHD.Visible = true;
                        //lblinvoice.Text = "From Date:-" + from1 + "To Date:-" + to1;
                        List<Database.ICTR_HD> ListItem = ((List<Database.ICTR_HD>)Session["SaleHD"]).ToList();
                        listProductst.DataSource = ListItem;
                        listProductst.DataBind();
                        decimal TOT = Convert.ToDecimal(ListItem.Sum(p => p.TOTAMT));
                        lblTotAMT.Text = TOT.ToString();
                        Session["SaleHD"] = null;
                    }
                }
                if (Request.QueryString["ID"] != null)
                {
                    if (TRCID == 2)
                    {

                        lblHeader.Text = "Daily Sales Report Detailed (" + TodayDate + ")";
                    }
                    else if (TRCID == 4)
                    {
                        lblHeader.Text = "Sales Report Detailed";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 6)
                    {
                        lblHeader.Text = "Daily Sales Return Detailed (" + TodayDate + ")";
                    }
                    else if (TRCID == 8)
                    {
                        lblHeader.Text = "Sales Return Detailed";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 10)
                    {
                        lblHeader.Text = "Daily Purchase Return Detailed (" + TodayDate + ")";
                    }
                    else if (TRCID == 12)
                    {
                        lblHeader.Text = "Purchase Report Detailed";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    else if (TRCID == 14)
                    {
                        lblHeader.Text = "Daily Purchase Return Detailed (" + TodayDate + ")";
                    }
                    else if (TRCID == 16)
                    {
                        lblHeader.Text = "Purchase Return Detailed";
                        lblinvoice.Text = "From (" + From + ") To (" + To + ")";
                    }
                    if (Session["SaleDT"] != null)
                    {
                        PNLSaleDT.Visible = true;
                        List<Database.ICTR_DT> ListItemDT = ((List<Database.ICTR_DT>)Session["SaleDT"]).ToList();
                        ListSaleDT.DataSource = ListItemDT;
                        ListSaleDT.DataBind();
                        //Calculation
                        decimal QTY = ListItemDT.Sum(p => p.QUANTITY);
                        lblqty.Text = QTY.ToString();
                        decimal UnitPrice = Convert.ToDecimal(ListItemDT.Sum(p => p.UNITPRICE));
                        lblUnitPrice.Text = UnitPrice.ToString();
                        decimal Cost = Convert.ToDecimal(ListItemDT.Sum(p => p.CostAmount));
                        lblCost.Text = Cost.ToString();
                        decimal Amount = Convert.ToDecimal(ListItemDT.Sum(p => p.AMOUNT));
                        lblAmount.Text = Amount.ToString();
                        Session["SaleDT"] = null;
                    }
                }
            }
        }

        public string Gross(int MyTransid, int MyID)
        {

            decimal UnitPrice = Convert.ToDecimal(DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid && p.MYID == MyID).UNITPRICE);
            decimal CostAmount = Convert.ToDecimal(DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid && p.MYID == MyID).CostAmount);
            decimal GrossProfit = UnitPrice - CostAmount;
            TotalGross += GrossProfit;
            lblGross.Text = TotalGross.ToString();
            return GrossProfit.ToString();

        }
        public string TransDate(int MyTransid)
        {
            string date = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid).TRANSDATE.ToShortDateString();
            return date;
        }
        public string TransDoc(int MyTransid)
        {
            string DocNo = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid).TransDocNo;
            return DocNo.ToString();
        }
        public string CusVendID(int MyTransid)
        {
            string ID = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid).CUSTVENDID.ToString();
            int VenderID = Convert.ToInt32(ID);
            string Customer = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == VenderID).COMPNAME1;
            return Customer.ToString();
        }
        public string Ref(int MyTransid)
        {
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MyTransid && (p.REFERENCE != null && p.REFERENCE != "")).Count() > 0)
            {
                string Reference = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid).REFERENCE;
                return Reference.ToString();
            }
            else
            {
                return "";
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
        public string CustVendID(int VendID)
        {
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            //if (VendID == 0)
            //{
            //    return "Not Found";
            //}
            //else
            //{
            //    string CustomerVenderID = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == VendID && p.TenentID == TID).COMPNAME1;
            //    return CustomerVenderID;
            //}
            return "GYM";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("GYMReport2.aspx");
        }





    }
}