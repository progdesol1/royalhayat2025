using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Classes;

namespace Web.Sales
{
    public partial class SaleReturnHPSPrints : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Tranjestion"] != null && Request.QueryString["MYTRANSID"] != null)
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                    int TRCID = Convert.ToInt32(Request.QueryString["Tranjestion"]);
                    ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TRCID && p.TenentID == TID && p.locationID == LID);
                    int EID = Convert.ToInt32(objICTR_HD.ExtraField2);
                   // lblSalemen.Text = DB.tbl_Employee.Single(p => p.TanentId == TID && p.LocationID == LID && p.employeeID == EID).firstname;
                    //lblInvoiceNo.Text = objICTR_HD.InvoiceNO.ToString();
                    int CUTID = Convert.ToInt32(objICTR_HD.CUSTVENDID);
                    //lblreferno.Text = objICTR_HD.REFERENCE;
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
                    //if (objICTR_HD.ExtraSwitch1 == "1")
                    //{
                    //    lblcseandcredt.Text = "CREDIT";
                    //}
                    //else
                    //{
                    //    lblcseandcredt.Text = "CASH";
                    //}
                    //lblcseandcredt.Text = "CASH";yogesh20042017
                    TBLCOMPANYSETUP objcopm = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == CUTID && p.TenentID == TID);

                    // labelUSerNAme.Text = objcopm.COMPNAME1;yogesh20042017
                    string Add1 = objcopm.ADDR1;
                    string Add2 = objcopm.ADDR2;
                    //LabelUserAddresh.Text = lbladdrsh.Text = Add1+"<br/>" + Add2;
                    //lbltelemail.Text = lblemailshlipen.Text = objcopm.EMAIL;
                    DateTime TIme = objICTR_HD.TRANSDATE;
                    //LblDate.Text =  TIme.ToShortDateString();yogesh20042017
                    //tectionNo.Text =  TRCID.ToString() ;yogesh20042017
                    //lblPhone.Text = objcopm.MOBPHONE;yogesh20042017
                    BindProduct(TID, LID, TRCID);
                    tblsetupsalesh obj = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == 4101 && p.transsubid == 410103);
                    //lblpayment.Text = obj.PaymentDetails;
                    //lblhenderline.Text = obj.HeaderLine;
                    // lblteglin.Text = txtteglin.Text =  obj.TagLine;
                    // lblbottumline.Text =  obj.BottomTagLine;

                    

                }
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
//                    if (h > 0 || i == 0) sb.Append("");
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

        public void BindProduct(int TID, int LID, int TRCID)
        {

            //int UID = Convert.ToInt32(((TBLCONTACT)Session["USER"]).ContactMyID);
            var list = DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == TRCID).ToList();
            List<TenQtyClass1> FList = new List<TenQtyClass1>();
            foreach (ICTR_DT item in list)
            {
                TenQtyClass1 obj = new TenQtyClass1();
                obj.MyProdID = item.MyProdID.ToString();
                obj.QUANTITY = item.QUANTITY.ToString();
                obj.UNITPRICE = item.UNITPRICE.ToString();
                obj.AMOUNT = item.AMOUNT.ToString();
                if (item.DESCRIPTION.Length >= 100)
                    obj.DESCRIPTION = item.DESCRIPTION.Substring(0, 100) + "...";
                else
                    obj.DESCRIPTION = item.DESCRIPTION;
                FList.Add(obj);
            }
            if (list.Count() <= 10)
            {
                for (int i = list.Count; i <= 9; i++)
                {
                    TenQtyClass1 obj = new TenQtyClass1();
                    obj.MyProdID = "";
                    obj.QUANTITY = "";
                    obj.UNITPRICE = "";
                    obj.AMOUNT = "";
                    obj.DESCRIPTION = "";
                    FList.Add(obj);
                }
            }
            listProductst.DataSource = FList;
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
            List<CurrencyInfo1> currencies = new List<CurrencyInfo1>();
            currencies.Add(new CurrencyInfo1(CurrencyInfo1.Currencies.Tunisia));
            ToWord1 toWord = new ToWord1(Convert.ToDecimal(GalredTotal), currencies[0]);
            string WoredQ = toWord.ConvertToEnglish();// words(Total);
            lblword.Text = WoredQ ;
            var HDObj = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == TRCID);
            long AgainstID = Convert.ToInt64(Request.QueryString["MYTRANSID"]);//Dipak 
            ICTR_HD objAgains = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == AgainstID);
            lblAgains.Text = "(" + objAgains.TRANSDATE.ToString("dd/MMM/yyyy") + ")";
            lblInvoiceNo.Text = HDObj.TransDocNo.ToString();// +"/" + TRCID.ToString();            
            var CustObj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == HDObj.CUSTVENDID);
            lblCustomerName.Text = CustObj.COMPNAME1;
            lblCustomerMobile.Text = CustObj.MOBPHONE.ToString();
            lblInvoiceDate.Text = HDObj.TRANSDATE.ToString("dd/MMM/yyyy"); //DateTime.Now.ToShortDateString();
        }
        public string getprodname(string PID)
        {
            int SID = 0;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (PID == "")
                return "";
            else
                 SID=Convert.ToInt32(PID);
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == SID && p.TenentID == TID).Count() > 0)
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).AlternateCode1;
            else
                return "";
        }
        public string getProdID(string PID)
        {
            if (PID == "")
            {
                return "";
            }
            else
            {
                return PID;
            }

        }
        public string getprodPart(int SID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).AlternateCode1;
        }
        public string Getproductdescriptio(int PID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).REMARKS;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("Sales_Return.aspx?transid=5101&transsubid=510101");
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
public class TenQtyClass1
{
    public string MyProdID { get; set; }
    public string QUANTITY { get; set; }
    public string UNITPRICE { get; set; }
    public string AMOUNT { get; set; }
    public string DESCRIPTION { get; set; }

}
class ToWord1
{
    /// Group Levels: 987,654,321.234
    /// 234 : Group Level -1
    /// 321 : Group Level 0
    /// 654 : Group Level 1
    /// 987 : Group Level 2

    #region Varaibles & Properties

    /// <summary>
    /// integer part
    /// </summary>
    private long _intergerValue;

    /// <summary>
    /// Decimal Part
    /// </summary>
    private int _decimalValue;

    /// <summary>
    /// Number to be converted
    /// </summary>
    public Decimal Number { get; set; }

    /// <summary>
    /// Currency to use
    /// </summary>
    public CurrencyInfo1 Currency { get; set; }

    /// <summary>
    /// English text to be placed before the generated text
    /// </summary>
    public String EnglishPrefixText { get; set; }

    /// <summary>
    /// English text to be placed after the generated text
    /// </summary>
    public String EnglishSuffixText { get; set; }

    /// <summary>
    /// Arabic text to be placed before the generated text
    /// </summary>
    public String ArabicPrefixText { get; set; }

    /// <summary>
    /// Arabic text to be placed after the generated text
    /// </summary>
    public String ArabicSuffixText { get; set; }
    #endregion

    #region General

    /// <summary>
    /// Constructor: short version
    /// </summary>
    /// <param name="number">Number to be converted</param>
    /// <param name="currency">Currency to use</param>
    public ToWord1(Decimal number, CurrencyInfo1 currency)
    {
        InitializeClass(number, currency, String.Empty, "only.", "فقط", "لا غير.");
    }

    /// <summary>
    /// Constructor: Full Version
    /// </summary>
    /// <param name="number">Number to be converted</param>
    /// <param name="currency">Currency to use</param>
    /// <param name="englishPrefixText">English text to be placed before the generated text</param>
    /// <param name="englishSuffixText">English text to be placed after the generated text</param>
    /// <param name="arabicPrefixText">Arabic text to be placed before the generated text</param>
    /// <param name="arabicSuffixText">Arabic text to be placed after the generated text</param>
    public ToWord1(Decimal number, CurrencyInfo1 currency, String englishPrefixText, String englishSuffixText, String arabicPrefixText, String arabicSuffixText)
    {
        InitializeClass(number, currency, englishPrefixText, englishSuffixText, arabicPrefixText, arabicSuffixText);
    }

    /// <summary>
    /// Initialize Class Varaibles
    /// </summary>
    /// <param name="number">Number to be converted</param>
    /// <param name="currency">Currency to use</param>
    /// <param name="englishPrefixText">English text to be placed before the generated text</param>
    /// <param name="englishSuffixText">English text to be placed after the generated text</param>
    /// <param name="arabicPrefixText">Arabic text to be placed before the generated text</param>
    /// <param name="arabicSuffixText">Arabic text to be placed after the generated text</param>
    private void InitializeClass(Decimal number, CurrencyInfo1 currency, String englishPrefixText, String englishSuffixText, String arabicPrefixText, String arabicSuffixText)
    {
        Number = number;
        Currency = currency;
        EnglishPrefixText = englishPrefixText;
        EnglishSuffixText = englishSuffixText;
        ArabicPrefixText = arabicPrefixText;
        ArabicSuffixText = arabicSuffixText;

        ExtractIntegerAndDecimalParts();
    }

    /// <summary>
    /// Get Proper Decimal Value
    /// </summary>
    /// <param name="decimalPart">Decimal Part as a String</param>
    /// <returns></returns>
    private string GetDecimalValue(string decimalPart)
    {
        string result = String.Empty;

        if (Currency.PartPrecision != decimalPart.Length)
        {
            int decimalPartLength = decimalPart.Length;

            for (int i = 0; i < Currency.PartPrecision - decimalPartLength; i++)
            {
                decimalPart += "0"; //Fix for 1 number after decimal ( 10.5 , 1442.2 , 375.4 ) 
            }

            result = String.Format("{0}.{1}", decimalPart.Substring(0, Currency.PartPrecision), decimalPart.Substring(Currency.PartPrecision, decimalPart.Length - Currency.PartPrecision));

            result = (Math.Round(Convert.ToDecimal(result))).ToString();
        }
        else
            result = decimalPart;

        for (int i = 0; i < Currency.PartPrecision - result.Length; i++)
        {
            result += "0";
        }

        return result;
    }

    /// <summary>
    /// Eextract Interger and Decimal parts
    /// </summary>
    private void ExtractIntegerAndDecimalParts()
    {
        String[] splits = Number.ToString().Split('.');

        _intergerValue = Convert.ToInt32(splits[0]);

        if (splits.Length > 1)
            _decimalValue = Convert.ToInt32(GetDecimalValue(splits[1]));
    }
    #endregion

    #region English Number To Word

    #region Varaibles

    private static string[] englishOnes =
       new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

    private static string[] englishTens =
        new string[] {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

    private static string[] englishGroup =
        new string[] {
            "Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillian",
            "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion",
            "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion",
            "Vigintillion", "Unvigintillion", "Duovigintillion", "10^72", "10^75", "10^78", "10^81", "10^84", "10^87",
            "Vigintinonillion", "10^93", "10^96", "Duotrigintillion", "Trestrigintillion"
        };
    #endregion

    /// <summary>
    /// Process a group of 3 digits
    /// </summary>
    /// <param name="groupNumber">The group number to process</param>
    /// <returns></returns>
    private string ProcessGroup(int groupNumber)
    {
        int tens = groupNumber % 100;

        int hundreds = groupNumber / 100;

        string retVal = String.Empty;

        if (hundreds > 0)
        {
            retVal = String.Format("{0} {1}", englishOnes[hundreds], englishGroup[0]);
        }
        if (tens > 0)
        {
            if (tens < 20)
            {
                retVal += ((retVal != String.Empty) ? " " : String.Empty) + englishOnes[tens];
            }
            else
            {
                int ones = tens % 10;

                tens = (tens / 10) - 2; // 20's offset

                retVal += ((retVal != String.Empty) ? " " : String.Empty) + englishTens[tens];

                if (ones > 0)
                {
                    retVal += ((retVal != String.Empty) ? " " : String.Empty) + englishOnes[ones];
                }
            }
        }

        return retVal;
    }

    /// <summary>
    /// Convert stored number to words using selected currency
    /// </summary>
    /// <returns></returns>
    public string ConvertToEnglish()
    {
        Decimal tempNumber = Number;

        if (tempNumber == 0)
            return "Zero";

        string decimalString = ProcessGroup(_decimalValue);

        string retVal = String.Empty;

        int group = 0;

        if (tempNumber < 1)
        {
            retVal = englishOnes[0];
        }
        else
        {
            while (tempNumber >= 1)
            {
                int numberToProcess = (int)(tempNumber % 1000);

                tempNumber = tempNumber / 1000;

                string groupDescription = ProcessGroup(numberToProcess);

                if (groupDescription != String.Empty)
                {
                    if (group > 0)
                    {
                        retVal = String.Format("{0} {1}", englishGroup[group], retVal);
                    }

                    retVal = String.Format("{0} {1}", groupDescription, retVal);
                }

                group++;
            }
        }

        String formattedNumber = String.Empty;
        formattedNumber += (EnglishPrefixText != String.Empty) ? String.Format("{0} ", EnglishPrefixText) : String.Empty;
        formattedNumber += (retVal != String.Empty) ? retVal : String.Empty;
        formattedNumber += (retVal != String.Empty) ? (_intergerValue == 1 ? Currency.EnglishCurrencyName : Currency.EnglishPluralCurrencyName) : String.Empty;
        formattedNumber += (decimalString != String.Empty) ? " and " : String.Empty;
        formattedNumber += (decimalString != String.Empty) ? decimalString : String.Empty;
        formattedNumber += (decimalString != String.Empty) ? " " + (_decimalValue == 1 ? Currency.EnglishCurrencyPartName : Currency.EnglishPluralCurrencyPartName) : String.Empty;
        formattedNumber += (EnglishSuffixText != String.Empty) ? String.Format(" {0}", EnglishSuffixText) : String.Empty;

        return formattedNumber;
    }

    #endregion


}

public class CurrencyInfo1
{
    public enum Currencies { Syria = 0, UAE, SaudiArabia, Tunisia, Gold };

    #region Constructors

    public CurrencyInfo1(Currencies currency)
    {
        switch (currency)
        {
            case Currencies.Syria:
                CurrencyID = 0;
                CurrencyCode = "SYP";
                IsCurrencyNameFeminine = true;
                EnglishCurrencyName = "Syrian Pound";
                EnglishPluralCurrencyName = "Syrian Pounds";
                EnglishCurrencyPartName = "Piaster";
                EnglishPluralCurrencyPartName = "Piasteres";
                Arabic1CurrencyName = "ليرة سورية";
                Arabic2CurrencyName = "ليرتان سوريتان";
                Arabic310CurrencyName = "ليرات سورية";
                Arabic1199CurrencyName = "ليرة سورية";
                Arabic1CurrencyPartName = "قرش";
                Arabic2CurrencyPartName = "قرشان";
                Arabic310CurrencyPartName = "قروش";
                Arabic1199CurrencyPartName = "قرشاً";
                PartPrecision = 2;
                IsCurrencyPartNameFeminine = false;
                break;

            case Currencies.UAE:
                CurrencyID = 1;
                CurrencyCode = "AED";
                IsCurrencyNameFeminine = false;
                EnglishCurrencyName = "UAE Dirham";
                EnglishPluralCurrencyName = "UAE Dirhams";
                EnglishCurrencyPartName = "Fils";
                EnglishPluralCurrencyPartName = "Fils";
                Arabic1CurrencyName = "درهم إماراتي";
                Arabic2CurrencyName = "درهمان إماراتيان";
                Arabic310CurrencyName = "دراهم إماراتية";
                Arabic1199CurrencyName = "درهماً إماراتياً";
                Arabic1CurrencyPartName = "فلس";
                Arabic2CurrencyPartName = "فلسان";
                Arabic310CurrencyPartName = "فلوس";
                Arabic1199CurrencyPartName = "فلساً";
                PartPrecision = 2;
                IsCurrencyPartNameFeminine = false;
                break;

            case Currencies.SaudiArabia:
                CurrencyID = 2;
                CurrencyCode = "SAR";
                IsCurrencyNameFeminine = false;
                EnglishCurrencyName = "Saudi Riyal";
                EnglishPluralCurrencyName = "Saudi Riyals";
                EnglishCurrencyPartName = "Halala";
                EnglishPluralCurrencyPartName = "Halalas";
                Arabic1CurrencyName = "ريال سعودي";
                Arabic2CurrencyName = "ريالان سعوديان";
                Arabic310CurrencyName = "ريالات سعودية";
                Arabic1199CurrencyName = "ريالاً سعودياً";
                Arabic1CurrencyPartName = "هللة";
                Arabic2CurrencyPartName = "هللتان";
                Arabic310CurrencyPartName = "هللات";
                Arabic1199CurrencyPartName = "هللة";
                PartPrecision = 2;
                IsCurrencyPartNameFeminine = true;
                break;

            case Currencies.Tunisia:
                CurrencyID = 3;
                CurrencyCode = "TND";
                IsCurrencyNameFeminine = false;
                EnglishCurrencyName = "Kuwait Dinar";
                EnglishPluralCurrencyName = "Kuwait Dinars";
                EnglishCurrencyPartName = "Fil";
                EnglishPluralCurrencyPartName = "Fils";
                Arabic1CurrencyName = "دينار تونسي";
                Arabic2CurrencyName = "ديناران تونسيان";
                Arabic310CurrencyName = "دنانير تونسية";
                Arabic1199CurrencyName = "ديناراً تونسياً";
                Arabic1CurrencyPartName = "مليم";
                Arabic2CurrencyPartName = "مليمان";
                Arabic310CurrencyPartName = "ملاليم";
                Arabic1199CurrencyPartName = "مليماً";
                PartPrecision = 3;
                IsCurrencyPartNameFeminine = false;
                break;

            case Currencies.Gold:
                CurrencyID = 4;
                CurrencyCode = "XAU";
                IsCurrencyNameFeminine = false;
                EnglishCurrencyName = "Gram";
                EnglishPluralCurrencyName = "Grams";
                EnglishCurrencyPartName = "Milligram";
                EnglishPluralCurrencyPartName = "Milligrams";
                Arabic1CurrencyName = "جرام";
                Arabic2CurrencyName = "جرامان";
                Arabic310CurrencyName = "جرامات";
                Arabic1199CurrencyName = "جراماً";
                Arabic1CurrencyPartName = "ملجرام";
                Arabic2CurrencyPartName = "ملجرامان";
                Arabic310CurrencyPartName = "ملجرامات";
                Arabic1199CurrencyPartName = "ملجراماً";
                PartPrecision = 2;
                IsCurrencyPartNameFeminine = false;
                break;

        }
    }

    #endregion

    #region Properties

    /// <summary>
    /// Currency ID
    /// </summary>
    public int CurrencyID { get; set; }

    /// <summary>
    /// Standard Code
    /// Syrian Pound: SYP
    /// UAE Dirham: AED
    /// </summary>
    public string CurrencyCode { get; set; }

    /// <summary>
    /// Is the currency name feminine ( Mua'anath مؤنث)
    /// ليرة سورية : مؤنث = true
    /// درهم : مذكر = false
    /// </summary>
    public Boolean IsCurrencyNameFeminine { get; set; }

    /// <summary>
    /// English Currency Name for single use
    /// Syrian Pound
    /// UAE Dirham
    /// </summary>
    public string EnglishCurrencyName { get; set; }

    /// <summary>
    /// English Plural Currency Name for Numbers over 1
    /// Syrian Pounds
    /// UAE Dirhams
    /// </summary>
    public string EnglishPluralCurrencyName { get; set; }

    /// <summary>
    /// Arabic Currency Name for 1 unit only
    /// ليرة سورية
    /// درهم إماراتي
    /// </summary>
    public string Arabic1CurrencyName { get; set; }

    /// <summary>
    /// Arabic Currency Name for 2 units only
    /// ليرتان سوريتان
    /// درهمان إماراتيان
    /// </summary>
    public string Arabic2CurrencyName { get; set; }

    /// <summary>
    /// Arabic Currency Name for 3 to 10 units
    /// خمس ليرات سورية
    /// خمسة دراهم إماراتية
    /// </summary>
    public string Arabic310CurrencyName { get; set; }

    /// <summary>
    /// Arabic Currency Name for 11 to 99 units
    /// خمس و سبعون ليرةً سوريةً
    /// خمسة و سبعون درهماً إماراتياً
    /// </summary>
    public string Arabic1199CurrencyName { get; set; }

    /// <summary>
    /// Decimal Part Precision
    /// for Syrian Pounds: 2 ( 1 SP = 100 parts)
    /// for Kuwait Dinars: 3 ( 1 TND = 1000 parts)
    /// </summary>
    public Byte PartPrecision { get; set; }

    /// <summary>
    /// Is the currency part name feminine ( Mua'anath مؤنث)
    /// هللة : مؤنث = true
    /// قرش : مذكر = false
    /// </summary>
    public Boolean IsCurrencyPartNameFeminine { get; set; }

    /// <summary>
    /// English Currency Part Name for single use
    /// Piaster
    /// Fils
    /// </summary>
    public string EnglishCurrencyPartName { get; set; }

    /// <summary>
    /// English Currency Part Name for Plural
    /// Piasters
    /// Fils
    /// </summary>
    public string EnglishPluralCurrencyPartName { get; set; }

    /// <summary>
    /// Arabic Currency Part Name for 1 unit only
    /// قرش
    /// هللة
    /// </summary>
    public string Arabic1CurrencyPartName { get; set; }

    /// <summary>
    /// Arabic Currency Part Name for 2 unit only
    /// قرشان
    /// هللتان
    /// </summary>
    public string Arabic2CurrencyPartName { get; set; }

    /// <summary>
    /// Arabic Currency Part Name for 3 to 10 units
    /// قروش
    /// هللات
    /// </summary>
    public string Arabic310CurrencyPartName { get; set; }

    /// <summary>
    /// Arabic Currency Part Name for 11 to 99 units
    /// قرشاً
    /// هللةً
    /// </summary>
    public string Arabic1199CurrencyPartName { get; set; }
    #endregion
}