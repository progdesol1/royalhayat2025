using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.Sales
{
    public partial class InvoicePrint : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        bool FirstFlag = true;
        int TID, LID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if(!IsPostBack )
            {
                FistTimeLoad();
                if (Request.QueryString["Tranjestion"] != null)
                {                   
                    int TRCID = Convert.ToInt32(Request.QueryString["Tranjestion"]);
                    ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TRCID&&p.TenentID==TID&&p.locationID==LID);
                    int CUTID =Convert.ToInt32( objICTR_HD.CUSTVENDID);
                    TBLCOMPANYSETUP objcopm=DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == CUTID&&p.TenentID==TID);

                    labelUSerNAme.Text = lblname.Text = objcopm.COMPNAME1;
                    LabelUserAddresh.Text = lbladdrsh.Text = objcopm.ADDR1;
                    lbltelemail.Text = lblemailshlipen.Text = objcopm.EMAIL;
                    DateTime TIme = objICTR_HD.TRANSDATE;
                    LblDate.Text = "#" + TRCID + "/" + TIme.ToShortDateString();
                   
                    BindProduct(TRCID);
                    tblsetupsalesh obj = DB.tblsetupsaleshes.Single(p => p.TenentID == TID);
                    lblpayment.Text = obj.PaymentDetails;
                    lblhenderline.Text = obj.HeaderLine;
                    lblteglin.Text = obj.TagLine;
                    lblbottumline.Text = obj.BottomTagLine;
                }
                //string UIN = ((TBLCONTACT)Session["USER"]).PersName1;
                //string ADDRES = ((TBLCONTACT)Session["USER"]).ADDR1;
                //labelUSerNAme.Text = UIN;
                //LabelUserAddresh.Text = ADDRES;
               
               
            }
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
            // Remain ACM Work
        }

        public void SessionLoad()
        {
            string Ref = ((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();

        }

        public void BindProduct(int TRCID)
        {
           
           // int UID = Convert.ToInt32(((TBLCONTACT)Session["USER"]).ContactMyID);
            var list = DB.ICTR_DT.Where(p => p.TenentID == TID && p.locationID == LID && p.MYTRANSID == TRCID);
            listProductst.DataSource = list;
            listProductst.DataBind();
            decimal SUM = Convert.ToDecimal(list.Sum(m => m.AMOUNT));
            decimal DISCUNT = Convert.ToDecimal(list.Sum(m => m.DISAMT));
            lblDiscount.Text = DISCUNT.ToString();
           // decimal DISCOUNTTOTAL = (SUM -DISCUNT);
            decimal MINISUM = SUM - DISCUNT;
            decimal Vat = Convert.ToDecimal(lblVat.Text);
            decimal MianSub = (MINISUM* Vat )/100;
            decimal GalredTotal = MINISUM + Vat;
            lblSubtotal.Text = SUM.ToString();
            lblGalredTot.Text = GalredTotal.ToString();
        }
        public string getprodname(int SID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID&&p.TenentID==TID).BarCode;
        }
        public string Getproductdescriptio(int PID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).REMARKS;
        }
    }
}