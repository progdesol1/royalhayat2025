using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Net;
using System.IO;

namespace Web.Sales
{
    public partial class LocalInvoice : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Tranjestion"] != null)
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
                        ICIT_BR_ReferenceNo objbrre = DB.ICIT_BR_ReferenceNo.Single(p => p.MYTRANSID == TRCID && p.TenentID == TID && p.RefID == 10512);
                        //lbllponumber.Text = objbrre.ReferenceNo;
                    }
                    if (objICTR_HD.Terms != 0 && objICTR_HD.Terms != null)
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
                        lblcseandcredtarabic.Text = Translate(lblcseandcredt.Text, "ar");
                    }
                    else
                    {
                        lblcseandcredt.Text = "CASH";
                        lblcseandcredtarabic.Text = Translate(lblcseandcredt.Text, "ar");
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
                    LblDate.Text = TIme.ToString("dd/MMM/yyyy");
                    tectionNo.Text = TRCID.ToString();
                    BindProduct(TID, LID, TRCID);
                    tblsetupsalesh obj = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103);
                    //lblpayment.Text = obj.PaymentDetails;                 
                    //lblteglin.Text = txtteglin.Text =  obj.TagLine;
                    lblteglin.Text = CKEditTagline.Text = obj.TagLine;
                    lblbottumline.Text = obj.BottomTagLine;
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
            decimal MINISUM = SUM - DISCUNT;
            MINISUM = Math.Round(MINISUM, 3);
            //decimal Vat = Convert.ToDecimal(lblVat.Text);
            //decimal MianSub = (MINISUM * Vat) / 100;
            //decimal GalredTotal = MINISUM;// +Vat;
            //int Total = Convert.ToInt32(GalredTotal);
            lblSubtotal.Text = SUM.ToString();
            lblGalredTot.Text = MINISUM.ToString();//GalredTotal.ToString();

            string[] FunWord = MINISUM.ToString().Split('.');
            int W1 = Convert.ToInt32(FunWord[0]);
            int W2 = Convert.ToInt32(FunWord[1]);

            string WoredQ = words(W1);
            string WordPoint = "";
            if (W2 == 0 || W2 == 00 || W2 == 000)
            { }
            else
            {
                WordPoint = words(W2);
            }
            if (WordPoint == "")
                lblword.Text = " Kuwaiti Dinars <b>" + WoredQ + "</b> Only";
            else
                lblword.Text = " Kuwaiti Dinars <b>" + WoredQ + " & 0." + W2 + "/1000</b> Fils Only";

            //Kuwaiti Dinars Zero & 0.500/1000 Fils Only
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
        public string GetDesc(string DSC)
        {
            string Descrip = DSC.Replace("\n", "<br />");
            return Descrip;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("invoicepos.aspx?transid=4101&transsubid=410103");
        }

        protected void btnteglin_Click(object sender, EventArgs e)
        {
            //if (txtteglin.Visible == false)
            //{
            //    txtteglin.Visible = true;
            //    lblteglin.Visible = false;
            //}
            //else
            //{
            //    lblteglin.Text = txtteglin.Text;
            //    txtteglin.Visible = false;
            //    lblteglin.Visible = true;
            //}
            if (CKEditTagline.Visible == false)
            {
                CKEditTagline.Visible = true;
                lblteglin.Visible = false;
            }
            else
            {
                lblteglin.Text = CKEditTagline.Text;
                CKEditTagline.Visible = false;
                lblteglin.Visible = true;
            }

        }
        public string Translate(string textvalue, string to)
        {
            string appId = "A70C584051881A30549986E65FF4B92B95B353A5";//go to http://msdn.microsoft.com/en-us/library/ff512386.aspx to obtain AppId.
            // string textvalue = "Translate this for me";
            string from = "en";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + textvalue + "&from=" + from + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }


    }
}