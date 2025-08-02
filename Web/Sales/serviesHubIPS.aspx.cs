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
    public partial class serviesHubIPS : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
                //ShowSaleReport();
                //ShowSaleReportDT();
                //paytermHD();
            }

        }
        public void binddata()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);


        }
        public void ShowSaleReport()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            ListProductbycustomer.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID);
            ListProductbycustomer.DataBind();
        }
        public void ShowSaleReportDT()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            //ListViewDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID);
            //ListViewDT.DataBind();
        }
        public void paytermHD()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            ListProductbyItems.DataSource = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID);
            ListProductbyItems.DataBind();
        }
        public string paidbypayterm(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if(ID != 0)
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


        public string itemDescription(int MyTransID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int MyProdID = Convert.ToInt32(DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MyTransID).MyProdID);
            if (MyProdID == 0)
            {
                return "IC";
            }
            else
            {
                string itemDescription = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID).DescToprint;
                return MyProdID+"-"+ itemDescription;
            }
        }
        public string CompAndUser(int MyTrnasID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int CompID = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrnasID).COMPANYID);
            string UserID = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MyTrnasID).USERID;
            return CompID + " / " + UserID;
        }
        public string PaidBy(int MyTrnasID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MyTrnasID).Count() > 0)
            {
                int Paidby = Convert.ToInt32(DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MyTrnasID).PaymentTermsId);
                if (Paidby != 0)
                {
                    string refid = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Paidby).REFNAME1;
                    return refid;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
       

        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //string System = (drpSystem.SelectedItem).ToString();

           // int Main = Convert.ToInt32(drpMainTranTypefrom.SelectedValue);
            //var maintrans = DB.tbltranstypes.Single(p => p.TenentID == TID && p.transid == Main).inoutSwitch;

           // int TransSubID = Convert.ToInt32(drpsupTranTypeFrom.SelectedValue);
            //var listtransid = DB.View_TransTypeDetail.Single(p => p.transid == Main);
            //var listtranssubid = DB.View_TransTypeDetail.Single(p => p.transsubid == TransSubID);
            
            DateTime Fromdate = Convert.ToDateTime(txtdateFrom.Text);
            DateTime todate = Convert.ToDateTime(txtdateTO.Text);
            if (Fromdate < DateTime.Now && todate < DateTime.Now)
            {
                int ID = Convert.ToInt32(drpReportName.SelectedValue);
                               
                if (ID == 1)
                {
                    PanelProductbycustomer.Visible = true;
                    PanelProductbyItems.Visible = false;

                    //ListProductbycustomer.DataSource = DB.viewTransByCustomers.Where(p => p.TenentID == TID && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate);
                    ListProductbycustomer.DataBind();

                }
                if (ID == 2)
                {
                    PanelProductbycustomer.Visible = false;
                    PanelProductbyItems.Visible = true;
                    //ListProductbyItems.DataSource = DB.viewTransByItems.Where(p => p.TenentID == TID && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate);
                    ListProductbyItems.DataBind();
                }
                //ListView1.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == System && p.MYSYSNAME != "PUR" && p.transid == Main && p.transsubid == TransSubID && p.MainTranType == maintrans && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate);
               // ListView1.DataBind();

                //ListViewDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYSYSNAME == System);
                //ListViewDT.DataBind();

                //ListViewPur.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == System && p.MYSYSNAME != "SAL" && p.transid == Main && p.transsubid == TransSubID && p.MainTranType == maintrans && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate);
                //ListViewPur.DataBind();

                //ListViewPurDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYSYSNAME == System);
                //ListViewPurDT.DataBind();
            }
           
        }
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "view")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                //ListViewDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ID);
                //ListViewDT.DataBind();
            }
        }      
        protected void ListViewPur_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "view")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                //ListViewPurDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ID);
                //ListViewPurDT.DataBind();
            }
        }
        public string Foreign(int MytranId)
        {
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MytranId && p.TranType == "Foreign Invoice").Count() > 0)
            {
                string foreign = DB.ICTR_HD.Single(p => p.TranType == "Foreign Invoice" && p.MYTRANSID == MytranId).TranType.ToString();
                return foreign;
            }
            else
            {
                return "";
            }
        }
        public string Local(int MytranId)
        {
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MytranId && p.TranType == "Cashier Board").Count() > 0)
            {
                string local = DB.ICTR_HD.Single(p => p.TranType == "Cashier Board" && p.MYTRANSID == MytranId).TranType.ToString();
                return local;
            }
            else
            {
                return "";
            }
        }
        public string LocalPiece(int MytranId)
        {
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MytranId && p.TranType == "Cashier Board").Count() > 0)
            {
                string localpiece = DB.ICTR_HD.Single(p => p.TranType == "Cashier Board" && p.MYTRANSID == MytranId).TranType.ToString();
                return localpiece;
            }
            else
            {
                return "";
            }
        }
        public string ForeignPiece(int MytranId)
        {
            if (DB.ICTR_HD.Where(p => p.MYTRANSID == MytranId && p.TranType == "Foreign Invoice").Count() > 0)
            {
                string Foreignpiece = DB.ICTR_HD.Single(p => p.TranType == "Foreign Invoice" && p.MYTRANSID == MytranId).TranType.ToString();
                return Foreignpiece;
            }
            else
            {
                return "";
            }
        }

        protected void drpReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int ID=Convert.ToInt32(drpReportName.SelectedValue);

            DateTime FromDate = Convert.ToDateTime(txtdateFrom.Text);
            DateTime ToDate = Convert.ToDateTime(txtdateTO.Text);

            if(ID==1)
            {
                PanelProductbycustomer.Visible = true;
                PanelProductbyItems.Visible = false;

                ListProductbycustomer.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ENTRYDATE >= FromDate && p.ENTRYDATE <= ToDate);
                ListProductbycustomer.DataBind();

            }
            if(ID==2)
            {
                PanelProductbycustomer.Visible = false;
                PanelProductbyItems.Visible = true;
                ListProductbyItems.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ENTRYDATE >= FromDate && p.ENTRYDATE <= ToDate);
                ListProductbyItems.DataBind();
            }
        }
          
    }
}