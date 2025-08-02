using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Sales
{
    public partial class LocalInvoiceForStaff : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Tranjestion"] != null )
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                    int TRCID = Convert.ToInt32(Request.QueryString["Tranjestion"]);
                    ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TRCID && p.TenentID == TID && p.locationID == LID);
                    //int EID = Convert.ToInt32(objICTR_HD.ExtraField2);
                    //lblSalemen.Text = DB.tbl_Employee.Single(p => p.TanentId == TID && p.LocationID == LID && p.employeeID == EID).firstname;
                    int CUTID = Convert.ToInt32(objICTR_HD.CUSTVENDID);
                    lblreferno.Text = objICTR_HD.REFERENCE;
                    if (DB.ICIT_BR_ReferenceNo.Where(p => p.MYTRANSID == TRCID && p.TenentID == TID && p.RefID == 10512).Count() > 0)
                    {
                        ICIT_BR_ReferenceNo objbrre = DB.ICIT_BR_ReferenceNo.Single(p => p.MYTRANSID == TRCID && p.TenentID == TID&&p.RefID==10512);
                        //lbllponumber.Text = objbrre.ReferenceNo;
                    }
                    if (objICTR_HD.Terms!=0&&objICTR_HD.Terms!=null)
                    {
                        int RID = Convert.ToInt32(objICTR_HD.Terms);
                        //pnltrrms.Visible = true;
                        //lblterms.Text = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == RID).REFNAME1;
                    }
                    else
                    {
                        //pnltrrms.Visible = false;
                    }
                    if (objICTR_HD.ExtraSwitch1 == "1")
                    {
                        lblcseandcredt.Text = "CREDIT";
                    }
                    else
                    {
                        lblcseandcredt.Text = "CASH";
                    }
                    //else
                    //{
                    //    lblcseandcredt.Text = "Corp";
                    //}
                    TBLCOMPANYSETUP objcopm = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == CUTID && p.TenentID == TID);

                    labelUSerNAme.Text = objcopm.COMPNAME1;
                    string Add1 = objcopm.ADDR1;
                    string Add2 = objcopm.ADDR2;
                    //LabelUserAddresh.Text = lbladdrsh.Text = Add1+"<br/>" + Add2;
                    //lbltelemail.Text = lblemailshlipen.Text = objcopm.EMAIL;
                    DateTime TIme = objICTR_HD.TRANSDATE;
                    LblDate.Text =  TIme.ToShortDateString();
                    tectionNo.Text =  TRCID.ToString() ;
                   
                    BindProduct(TID, LID, TRCID);
                    tblsetupsalesh obj = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103);
                    //lblpayment.Text = obj.PaymentDetails;
                    lblhenderline.Text = obj.HeaderLine;
                    lblteglin.Text = txtteglin.Text =  obj.TagLine;
                    lblbottumline.Text =  obj.BottomTagLine;
                   
                }
            }
        }
        public string words(int numbers)
        {
            int number = numbers;
            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
"Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
"Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
"Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
       
        public void BindProduct(int TID, int LID, int TRCID)
        {

            //int UID = Convert.ToInt32(((TBLCONTACT)Session["USER"]).ContactMyID);
            var list = DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == TRCID);
            listProductst.DataSource = list;
            listProductst.DataBind();
            decimal SUM = Convert.ToDecimal(list.Sum(m => m.AMOUNT));
            decimal DISCUNT = Convert.ToDecimal(list.Sum(m => m.DISAMT));
            lblDiscount.Text = DISCUNT.ToString();
            // decimal DISCOUNTTOTAL = (SUM -DISCUNT);
            decimal MINISUM = SUM - DISCUNT;
            decimal Vat = Convert.ToDecimal(lblVat.Text);
            decimal MianSub = (MINISUM * Vat) / 100;
            decimal GalredTotal = MINISUM + Vat;
            int Total = Convert.ToInt32(GalredTotal);
            lblSubtotal.Text = SUM.ToString();
            lblGalredTot.Text = GalredTotal.ToString();
            string WoredQ=words(Total);
            lblword.Text = WoredQ + " <b> Kuwaiti Dinars Only</b>";
        }
        public string getprodname(int SID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).BarCode;
        }
        public string Getproductdescriptio(int PID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).REMARKS;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("invoicepos.aspx?transid=4101&transsubid=410106");
        }

        protected void btnteglin_Click(object sender, EventArgs e)
        {
            if (txtteglin.Visible == false)
            {
                txtteglin.Visible = true;
                lblteglin.Visible = false;
            }
            else
            {
                lblteglin.Text = txtteglin.Text;
                txtteglin.Visible = false;
                lblteglin.Visible = true;
            }
                

        }
    }
}