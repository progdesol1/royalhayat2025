using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Purchase
{
    public partial class ReportIPS2Print : System.Web.UI.Page
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
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            if (VendID == 0)
            {
                return "Not Found";
            }
            else
            {
                string CustomerVenderID = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == VendID && p.TenentID == TID).COMPNAME1;
                return CustomerVenderID;
            }
        }
        //        public string words(int numbers)
        //        {
        //            int number = numbers;
        //            if (number == 0) return "Zero";
        //            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
        //            int[] num = new int[4];
        //            int first = 0;
        //            int u, h, t;
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            if (number < 0)
        //            {
        //                sb.Append("Minus ");
        //                number = -number;
        //            }
        //            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
        //"Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        //            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
        //"Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        //            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
        //"Seventy ","Eighty ", "Ninety "};
        //            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
        //            num[0] = number % 1000; // units
        //            num[1] = number / 1000;
        //            num[2] = number / 100000;
        //            num[1] = num[1] - 100 * num[2]; // thousands
        //            num[3] = number / 10000000; // crores
        //            num[2] = num[2] - 100 * num[3]; // lakhs
        //            for (int i = 3; i > 0; i--)
        //            {
        //                if (num[i] != 0)
        //                {
        //                    first = i;
        //                    break;
        //                }
        //            }
        //            for (int i = first; i >= 0; i--)
        //            {
        //                if (num[i] == 0) continue;
        //                u = num[i] % 10; // ones
        //                t = num[i] / 10;
        //                h = num[i] / 100; // hundreds
        //                t = t - 10 * h; // tens
        //                if (h > 0) sb.Append(words0[h] + "Hundred ");
        //                if (u > 0 || t > 0)
        //                {
        //                    if (h > 0 || i == 0) sb.Append("and ");
        //                    if (t == 0)
        //                        sb.Append(words0[u]);
        //                    else if (t == 1)
        //                        sb.Append(words1[u]);
        //                    else
        //                        sb.Append(words2[t - 2] + words0[u]);
        //                }
        //                if (i != 0) sb.Append(words3[i - 1]);
        //            }
        //            return sb.ToString().TrimEnd();
        //        }

        //public void BindProduct(int TID, int LID, int TRCID)
        //{
        //    if (lblcseandcredt.Text == "LOCAL")
        //    {
        //        //int UID = Convert.ToInt32(((TBLCONTACT)Session["USER"]).ContactMyID);
        //        var list = DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == TRCID);
        //        listProductst.DataSource = list;
        //        listProductst.DataBind();
        //        decimal SUM = Convert.ToDecimal(list.Sum(m => m.AMOUNT));
        //        decimal DISCUNT = Convert.ToDecimal(list.Sum(m => m.DISAMT));
        //        lblDiscount.Text = DISCUNT.ToString();
        //        // decimal DISCOUNTTOTAL = (SUM -DISCUNT);
        //        decimal MINISUM = SUM - DISCUNT;
        //        //decimal Vat = Convert.ToDecimal(lblVat.Text);
        //        //decimal MianSub = (MINISUM * Vat) / 100;
        //        //decimal GalredTotal = MINISUM + Vat;
        //        int Total = Convert.ToInt32(MINISUM);
        //        lblSubtotal.Text = SUM.ToString();
        //        lblGalredTot.Text = MINISUM.ToString();
        //        string WoredQ = words(Total);
        //        lblword.Text = WoredQ + " <b> Kuwaiti Dinars Only</b>";
        //    }
        //    else
        //    {
        //        var listDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == TRCID);
        //        var ListEXT = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == TRCID);
        //        listProductst.DataSource = listDT;
        //        listProductst.DataBind();
        //        decimal SUM = Convert.ToDecimal(listDT.Sum(m => m.AMOUNT));
        //        decimal DISCUNT = Convert.ToDecimal(listDT.Sum(m => m.DISAMT));
        //        lblDiscount.Text = DISCUNT.ToString();                
        //        decimal MINISUM = SUM - DISCUNT;               
        //        int Total = Convert.ToInt32(MINISUM);
        //        lblSubtotal.Text = SUM.ToString();
        //        lblGalredTot.Text = MINISUM.ToString();
        //        string WoredQ = words(Total);
        //        lblword.Text = WoredQ + " <b> Kuwaiti Dinars Only</b>";
        //        decimal sum = Convert.ToDecimal(ListEXT.Sum(p => p.AMOUNT));
        //        decimal MINISUMEXT = sum - DISCUNT;
        //        int TotalEXT = Convert.ToInt32(MINISUMEXT);
        //        lblForeign.Text = MINISUMEXT.ToString();                
        //    }
        //}
        //public string getprodname(int SID)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    return DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).BarCode;
        //}
        //public string Getproductdescriptio(int PID)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).REMARKS;
        //}
        //public string AmountEXT(decimal MyTransid, int MYID)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    var ListICTR = DB.ICTR_DTEXT.Where(p => p.TenentID == TID && p.MYTRANSID == MyTransid).ToList();
        //    if(ListICTR.Count()>0)
        //    {
        //        var Amo = DB.ICTR_DTEXT.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransid && p.MyID == MYID).AMOUNT.ToString();
        //        return Amo;
        //    }
        //    else
        //        return "";
        //}
        //public string getDesc(int prodid)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    return DB.TBLPRODUCTs.Single(p => p.MYPRODID == prodid && p.TenentID == TID).ProdName1;
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporForIPS2.aspx?MID=KrZtWYpgq/rw7wDElL1Ydw==");
        }


    }
}