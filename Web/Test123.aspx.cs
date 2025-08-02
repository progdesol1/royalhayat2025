using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web
{
    public partial class Test123 : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            
            
           


            DateTime? CheckOutDate = null;
           

            int AccountantID = 0;
            bool ChequeVerified = false;
            bool CashVerified = false;
            bool ATMVerified = false;
            bool VoucharVerified = false;
            DateTime? ChequeVerifiedDate=null;
            

            DateTime? CashVerifiedDate = null;
            DateTime? ATMVerifiedDate = null;
            DateTime? VoucharVerifiedDate = null;
            int TenantID = 0;
            int MYTRANSID = 0;
            int MyID = 0;
            int MyRunningSerial = 0;
            string CURRENCY = "abc";
             decimal CURRENTCONVRATE =0;
            decimal OTHERCURAMOUNT = 0;
            int QUANTITY = 0;
            decimal UNITPRICE = 0;
            decimal AMOUNT = 0;
            int QUANTITYDELIVERD = 0;
            int DELIVERDLOCATENANTID = 0;
            decimal AMOUNTDELIVERD = 0;
            string ACCOUNTID = "abc";
            string GRNREF = "abc";
            DateTime EXPIRYDATE = DateTime.Now;
            string Remark = "abc";
            int TransNo1 = 0;
            bool ACTIVE = true;

            //insertICTR_DTEXT(int TenantID, int MYTRANSID, int MyID, int MyRunningSerial, string CURRENCY, decimal CURRENTCONVRATE, decimal OTHERCURAMOUNT, int QUANTITY, decimal UNITPRICE, decimal AMOUNT, int QUANTITYDELIVERD, int DELIVERDLOCATENANTID, decimal AMOUNTDELIVERD, string ACCOUNTID, string GRNREF, DateTime EXPIRYDATE, int CRUP_ID, string Remark, int TransNo1, bool ACTIVE)
            Classes.EcommAdminClass.insertICTR_DTEXT( TenantID,  MYTRANSID,  MyID,  MyRunningSerial, CURRENCY, CURRENTCONVRATE, OTHERCURAMOUNT, QUANTITY, UNITPRICE, AMOUNT, QUANTITYDELIVERD, DELIVERDLOCATENANTID, AMOUNTDELIVERD, ACCOUNTID, GRNREF, EXPIRYDATE, 0, Remark, TransNo1, ACTIVE);
           // Classes.EcommAdminClass.insertICTRPayTerms_HD( TID, MyTransID, PaymentTermsId, CounterID, LocationID, CashBankChequeID, Amount, ReferenceNo, TransDate, CheckOutDate, Notes, AccountID, CRUP_ID, ApprovalID, AccountantID , ChequeVerified, CashVerified, ATMVerified, VoucharVerified, ChequeVerifiedDate, CashVerifiedDate, ATMVerifiedDate, VoucharVerifiedDate, JVRefNo);
            //Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MyTransID, PaymentTermsId, CounterID, LocationID, CashBankChequeID, Amount, ReferenceNo, TransDate, CheckOutDate, Notes, AccountID, CRUP_ID, ApprovalID, AccountantID, ChequeVerified, CashVerified, ATMVerified, VoucharVerified, ChequeVerifiedDate, CashVerifiedDate, ATMVerifiedDate, VoucharVerifiedDate, JVRefNo);
        }
    }
}