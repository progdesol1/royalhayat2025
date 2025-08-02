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
    public partial class ReporForIPS : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        DateTime? Fromdate = null;
        DateTime? todate = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
                //ShowSaleReport();
                //ShowSaleReportDT();
                paytermHD();
                Default();
            }

        }
        public void Default()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            drpSystem.SelectedValue = "31";
            drpToSystem.SelectedValue = "31";
            drpMainTranTypefrom.SelectedValue = "4101";
            drpMainTranTypeTo.SelectedValue = "4101";
            drpsupTranTypeFrom.SelectedValue = "410103";
            drpSubTranTypeTo.SelectedValue = "410103";
            //drpReportName.SelectedValue = "2";
            txtdateTO.Text = DateTime.Now.ToShortDateString();
            txtdateFrom.Text = DateTime.Now.ToShortDateString();

            drpMainTranTypefrom.DataSource = DB.tbltranstypes.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL");
            drpMainTranTypefrom.DataTextField = "transtype1";
            drpMainTranTypefrom.DataValueField = "transid";
            drpMainTranTypefrom.DataBind();
            drpMainTranTypefrom.Items.Insert(0, new ListItem("-- Select MainTrans --", "0"));

            drpsupTranTypeFrom.DataSource = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == 4101);
            drpsupTranTypeFrom.DataTextField = "transsubtype1";
            drpsupTranTypeFrom.DataValueField = "transsubid";
            drpsupTranTypeFrom.DataBind();
            drpsupTranTypeFrom.Items.Insert(0, new ListItem("-- Select subTrans --", "0"));
         
        }
        public void binddata()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            drpSystem.DataSource = DB.TBLSYSTEMS.Where(p => p.TenentID == TID && (p.MYSYSNAME == "SAL" || p.MYSYSNAME == "PUR")).OrderBy(a => a.MYSYSNAME);
            drpSystem.DataTextField = "MYSYSNAME";
            drpSystem.DataValueField = "SystemID";
            drpSystem.DataBind();
            drpSystem.Items.Insert(0, new ListItem("-- Select System --", "0"));

            drpToSystem.DataSource = DB.TBLSYSTEMS.Where(p => p.TenentID == TID && (p.MYSYSNAME == "SAL" || p.MYSYSNAME == "PUR")).OrderByDescending(a => a.MYSYSNAME);
            drpToSystem.DataTextField = "MYSYSNAME";
            drpToSystem.DataValueField = "SystemID";
            drpToSystem.DataBind();
            drpToSystem.Items.Insert(0, new ListItem("-- Select System --", "0"));

            drpMainTranTypefrom.DataSource = DB.tbltranstypes.Where(p => p.TenentID == TID).OrderBy(a => a.transtype1); ;
            drpMainTranTypefrom.DataTextField = "transtype1";
            drpMainTranTypefrom.DataValueField = "transid";
            drpMainTranTypefrom.DataBind();
            drpMainTranTypefrom.Items.Insert(0, new ListItem("-- Select MainTransFrom --", "0"));

            drpMainTranTypeTo.DataSource = DB.tbltranstypes.Where(p => p.TenentID == TID).OrderByDescending(a => a.transtype1); ;
            drpMainTranTypeTo.DataTextField = "transtype1";
            drpMainTranTypeTo.DataValueField = "transid";
            drpMainTranTypeTo.DataBind();
            drpMainTranTypeTo.Items.Insert(0, new ListItem("-- Select MainTransTO --", "0"));

            drpsupTranTypeFrom.DataSource = DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderBy(a => a.transsubtype1); ;
            drpsupTranTypeFrom.DataTextField = "transsubtype1";
            drpsupTranTypeFrom.DataValueField = "transsubid";
            drpsupTranTypeFrom.DataBind();
            drpsupTranTypeFrom.Items.Insert(0, new ListItem("-- Select subTransFrom --", "0"));

            drpSubTranTypeTo.DataSource = DB.tbltranssubtypes.Where(p => p.TenentID == TID).OrderByDescending(a => a.transsubtype1); ;
            drpSubTranTypeTo.DataTextField = "transsubtype1";
            drpSubTranTypeTo.DataValueField = "transsubid";
            drpSubTranTypeTo.DataBind();
            drpSubTranTypeTo.Items.Insert(0, new ListItem("-- Select subTransTO --", "0"));
        }
        public void ShowSaleReport()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            ListView1.DataSource = DB.ICTR_HD.Where(p => p.TenentID == TID && p.ACTIVE == true);
            ListView1.DataBind();
        }
        public void ShowSaleReportDT()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            ListViewDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID && p.ACTIVE == true);
            ListViewDT.DataBind();
        }
        public void paytermHD()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            ListViewICTRPayTermHD.DataSource = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID);
            ListViewICTRPayTermHD.DataBind();
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
            //var List = DB.ICTRPayTerms_HD.Where(p => p.MyTransID == MyTrnasID);
            List<Database.ICTRPayTerms_HD> List = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MyTrnasID).ToList();
            if (List.Count() > 0)
            {
                string paidBy = " ";
                foreach(Database.ICTRPayTerms_HD PayTermItem in List)
                {
                    int Paidby = Convert.ToInt32(PayTermItem.PaymentTermsId);
                    if (Paidby != 0)
                    {
                        string refid = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Paidby).REFNAME1;
                        paidBy+= " "+refid+" ";
                    }
                    else
                    {
                        string refidd = "Cash";
                        paidBy += " " + refidd + " ";
                    }
                }
                return paidBy;
                //int Paidby = Convert.ToInt32(DB.ICTRPayTerms_HD.Single(p => p.TenentID == TID && p.MyTransID == MyTrnasID).PaymentTermsId);
                //if (Paidby != 0)
                //{
                //    string refid = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Paidby).REFNAME1;
                //    return refid;
                //}
                //else
                //{
                //    string refidd = "Cash";
                //    return refidd;
                //}
            }
            else
            {
                string refidd = "Cash";
                return refidd;
            }

        }
        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            string MYSYS = (drpSystem.SelectedItem).ToString();
            drpMainTranTypefrom.DataSource = DB.tbltranstypes.Where(p => p.TenentID == TID && p.MYSYSNAME == MYSYS);
            drpMainTranTypefrom.DataTextField = "transtype1";
            drpMainTranTypefrom.DataValueField = "transid";
            drpMainTranTypefrom.DataBind();
            drpMainTranTypefrom.Items.Insert(0, new ListItem("-- Select MainTrans --", "0"));

            drpMainTranTypeTo.DataSource = DB.tbltranstypes.Where(p => p.TenentID == TID && p.MYSYSNAME == MYSYS);
            drpMainTranTypeTo.DataTextField = "transtype1";
            drpMainTranTypeTo.DataValueField = "transid";
            drpMainTranTypeTo.DataBind();
            drpMainTranTypeTo.Items.Insert(0, new ListItem("-- Select MainTransTO --", "0"));
        }

        protected void drpMainTranTypefrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int ID = Convert.ToInt32(drpMainTranTypefrom.SelectedValue);

            drpsupTranTypeFrom.DataSource = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == ID);
            drpsupTranTypeFrom.DataTextField = "transsubtype1";
            drpsupTranTypeFrom.DataValueField = "transsubid";
            drpsupTranTypeFrom.DataBind();
            drpsupTranTypeFrom.Items.Insert(0, new ListItem("-- Select subTrans --", "0"));
        }
        protected void drpMainTranTypeTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int ID = Convert.ToInt32(drpMainTranTypeTo.SelectedValue);

            drpSubTranTypeTo.DataSource = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == ID);
            drpSubTranTypeTo.DataTextField = "transsubtype1";
            drpSubTranTypeTo.DataValueField = "transsubid";
            drpSubTranTypeTo.DataBind();
            drpSubTranTypeTo.Items.Insert(0, new ListItem("-- Select subTransTO --", "0"));
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDailySale.Visible = false;
            pnlDailySaleDTL.Visible = false;
            pnlPurchase.Visible = false;
            pnlPurchaseDTL.Visible = false;
            pnlCollectionRPT.Visible = false;
            lblSaleRet.Visible = false;
            lblsaleRetDet.Visible = false;
            lblpurRet.Visible = false;            
            lblPurretDet.Visible = false;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string System = "";
            int Main = 0;
            string maintrans = "";
            int TransSubID = 0;
            //DateTime? Fromdate = null;
            //DateTime? todate = null;
            if (drpSystem.SelectedValue == "0")
            { }
            else
            { System = (drpSystem.SelectedItem).ToString(); }

            if (drpMainTranTypefrom.SelectedValue == "0")
            { }
            else
            {
                Main = Convert.ToInt32(drpMainTranTypefrom.SelectedValue);
                maintrans = DB.tbltranstypes.Single(p => p.TenentID == TID && p.transid == Main).inoutSwitch;
            }
            if (drpsupTranTypeFrom.SelectedValue == "0")
            { }
            else
            { TransSubID = Convert.ToInt32(drpsupTranTypeFrom.SelectedValue); }

            //var listtransid = DB.View_TransTypeDetail.Single(p => p.transid == Main);
            //var listtranssubid = DB.View_TransTypeDetail.Single(p => p.transsubid == TransSubID);
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
                if (System == "SAL")
                {

                    if (Main == 4101 && TransSubID == 410103)
                    {
                        List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == System && p.MYSYSNAME != "PUR" && p.transid == Main && p.transsubid == TransSubID && p.MainTranType == maintrans && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.TransDocNo).ToList();
                        ListView1.DataSource = listitem;
                        ListView1.DataBind();
                        pnlDailySale.Visible = true;
                        ViewState["Exele"] = listitem; 
                    }
                    else if (Main == 5101 && TransSubID == 510101)                  
                    {
                        List<Database.ICTR_HD> listitemSR = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == System && p.transid == Main && p.transsubid == TransSubID && p.TransType == "Sales Return" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.TransDocNo).ToList();
                        ListView1.DataSource = listitemSR;
                        ListView1.DataBind();
                        pnlDailySale.Visible = true;
                        lblSaleRet.Visible = true;
                    }
                }
                else if (System == "PUR")
                {
                    if (Main == 2101 && TransSubID == 21011)
                    {
                        List<Database.ICTR_HD> listitemPUR = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == System && p.MYSYSNAME != "SAL" && p.transid == Main && p.transsubid == TransSubID && p.MainTranType == maintrans && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.TransDocNo).ToList();
                        ListViewPur.DataSource = listitemPUR;
                        ListViewPur.DataBind();
                        pnlPurchase.Visible = true;
                    }
                    else if (Main == 2102 && TransSubID == 21012)
                    {
                        List<Database.ICTR_HD> listitemPURReturn= DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == System && p.MYSYSNAME != "SAL" && p.transid == Main && p.transsubid == TransSubID && p.MainTranType == maintrans && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.TransDocNo).ToList();
                        ListViewPur.DataSource = listitemPURReturn;
                        ListViewPur.DataBind();
                        pnlPurchase.Visible = true;
                        lblpurRet.Visible = true;                        
                    }
                    
                }


            }
            //for (int i = 0; i < ListView1.Items.Count; i++)
            //{
            //    DropDownList DrpsaleHDList = (DropDownList)ListView1.Items[i].FindControl("DrpsaleHDList");
            //    DrpsaleHDList.Items[0].Attributes.Add("style", "background-color:#4A8BC2");
            //    DrpsaleHDList.Items[1].Attributes.Add("style", "background-color:#f2f2f2");
            //    DrpsaleHDList.Items[2].Attributes.Add("style", "background-color:#f2f2f2");
            //    DrpsaleHDList.Items[3].Attributes.Add("style", "background-color:#f2f2f2");
            //}
        }
        protected void drpReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrints.Visible = true;
        }
        protected void btnPrints_Click(object sender, EventArgs e)
        {
            // sale invoice HD-PRINT
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //List<Database.ICTR_HD> listitem = ((List<Database.ICTR_HD>)ViewState["Exele"]).ToList();            

            List<Database.ICTR_DT> List = new List<ICTR_DT>();

            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);
            Fromdate = Convert.ToDateTime(txtdateFrom.Text); ;
            todate = Convert.ToDateTime(txtdateTO.Text);
            if (drpReportName.SelectedValue == "1")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 1);
            }
            else if (drpReportName.SelectedValue == "3")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 3 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //sale invoice DT-PRINT           
            else if (drpReportName.SelectedValue == "2")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
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
            else if (drpReportName.SelectedValue == "4")
            {
                //ViewState["Exele"] = listitem;     
                //List<Database.ICTR_HD> listitem = ((List<Database.ICTR_HD>)ViewState["Exele"]).ToList();

                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 4101 && p.transsubid == 410103 && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                foreach (Database.ICTR_HD item in listitem)
                {
                    List<Database.ICTR_DT> ICTR_DTlistitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == item.MYTRANSID).ToList();
                    foreach (Database.ICTR_DT item2 in ICTR_DTlistitem)
                    {
                        List.Add(item2);
                    }
                }
                Session["SaleDT"] = List;

                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 4 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //Sale Return HD Prints
            else if (drpReportName.SelectedValue == "5")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 5101 && p.transsubid == 510101 && p.TransType == "Sales Return" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 5);

            }
            else if (drpReportName.SelectedValue == "7")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "SAL" && p.transid == 5101 && p.transsubid == 510101 && p.TransType == "Sales Return" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 7 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //Sale Return DT Prints
            else if (drpReportName.SelectedValue == "6")
            {
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
            else if (drpReportName.SelectedValue == "8")
            {
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
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 8 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //Purchase HD prints
            else if (drpReportName.SelectedValue == "9")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2101 && p.transsubid == 21011 && p.TransType == "Goods Received Note - Purchase" && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 9);
            }
            else if (drpReportName.SelectedValue == "11")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2101 && p.transsubid == 21011 && p.TransType == "Goods Received Note - Purchase" && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 11 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //Purchase DT prints
            else if (drpReportName.SelectedValue == "10")
            {
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
            else if (drpReportName.SelectedValue == "12")
            {
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
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 12 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //Purchase Return HD prints
            else if (drpReportName.SelectedValue == "13")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2102 && p.transsubid == 21012 && p.TRANSDATE.Day == ToDay && p.TRANSDATE.Month == ToMonth && p.TRANSDATE.Year == ToYear && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 13);
            }
            else if (drpReportName.SelectedValue == "15")
            {
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYSYSNAME == "PUR" && p.transid == 2102 && p.transsubid == 21012 && p.TRANSDATE >= Fromdate && p.TRANSDATE <= todate && p.ACTIVE == true).OrderBy(p => p.MYTRANSID).ToList();
                Session["SaleHD"] = listitem;
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 15 + "&FROM=" + FROM + "&TO=" + TO);
            }
            //Purchase Return DT prints
            else if (drpReportName.SelectedValue == "14")
            {
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
            else if (drpReportName.SelectedValue == "16")
            {
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
                string FROM = Convert.ToDateTime(txtdateFrom.Text).ToString("dd/MMM/yyyy");
                string TO = Convert.ToDateTime(txtdateTO.Text).ToString("dd/MMM/yyyy");
                Response.Redirect("ReportIPS2Print.aspx?ID=" + 16 + "&FROM=" + FROM + "&TO=" + TO);
            }

            //END
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "view")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                List<Database.ICTR_DT> listitem = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ID && p.ACTIVE == true).ToList();
                ListViewDT.DataSource = listitem;
                ListViewDT.DataBind();
                pnlDailySaleDTL.Visible = true;                
                LoadButton(ID);
                ViewState["HD"] = ID;
                ViewState["exportDT"] = listitem;
                List<ViewData> List = new List<ViewData>();
                foreach (ICTR_HD items in DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == ID && p.ACTIVE == true).ToList())
                {
                    ViewData obj = new ViewData();
                    obj.Invoice = items.TransDocNo.ToString();
                    obj.CusVender = CustVendID(Convert.ToInt32(items.CUSTVENDID)).ToString();
                    obj.Amount = items.TOTAMT.ToString();
                    var concat = "( " + obj.Invoice + " / " + obj.CusVender + " / " + obj.Amount + " )";
                    //List.Add(obj);
                    lblDetailDT.Text = concat;
                }                
            }
            if (e.CommandName == "Prints")
            {
                int IDPrint = Convert.ToInt32(e.CommandArgument);
                List<Database.ICTR_HD> listitem = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == IDPrint && p.ACTIVE == true).ToList();
                Session["SaleHD"] = listitem;
                LoadButton(IDPrint);
                Database.ICTR_HD list123 = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == IDPrint && p.ACTIVE == true);
                if (list123.transid == 4101)
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 17);
                else
                    Response.Redirect("ReportIPS2Print.aspx?ID=" + 18);
            }
            if(e.CommandName == "Trans")
            {
                int IDTrans = Convert.ToInt32(e.CommandArgument);
                LoadButton(IDTrans);
                //Response.Redirect("Invoice.aspx?ID=" + IDTrans);
            }
            lblsaleRetDet.Visible = true;
        }
        public void LoadButton(int Load)
        {
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                DropDownList DrpsaleHDList = (DropDownList)ListView1.Items[i].FindControl("DrpsaleHDList");
                LinkButton lblview = (LinkButton)ListView1.Items[i].FindControl("lblview");
                LinkButton print = (LinkButton)ListView1.Items[i].FindControl("btnprient");
                LinkButton Trans = (LinkButton)ListView1.Items[i].FindControl("btnTrans");
                lblview.Visible = false;
                print.Visible = false;
                Trans.Visible = false;
                DrpsaleHDList.SelectedValue = "0";
            }
        }
        protected void DrpsaleHDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ListView1.Items.Count; i++)
            {                       
                DropDownList DrpsaleHDList = (DropDownList)ListView1.Items[i].FindControl("DrpsaleHDList");               
                LinkButton lblview = (LinkButton)ListView1.Items[i].FindControl("lblview");
                LinkButton print = (LinkButton)ListView1.Items[i].FindControl("btnprient");
                LinkButton Trans = (LinkButton)ListView1.Items[i].FindControl("btnTrans");
                if (DrpsaleHDList.SelectedValue == "1")
                {
                    lblview.Visible = true;                    
                }
                if (DrpsaleHDList.SelectedValue == "2")
                {
                    print.Visible = true;                   
                }
                if (DrpsaleHDList.SelectedValue == "3")
                {
                    Trans.Visible = true;
                }
            }
        }
        protected void ListViewPur_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "view")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ListViewPurDT.DataSource = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == ID && p.ACTIVE == true);
                ListViewPurDT.DataBind();
                pnlPurchaseDTL.Visible = true;                            
            }            
            lblPurretDet.Visible = true;
        }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ListViewPur.Items.Count; i++)
            {
                DropDownList DrpListPur = (DropDownList)ListView1.Items[i].FindControl("DropDownListPur");
                LinkButton lblPurview = (LinkButton)ListView1.Items[i].FindControl("lblPurview");
                LinkButton lblPurPrints = (LinkButton)ListView1.Items[i].FindControl("lblPurPrints");
                LinkButton lblPurTrans = (LinkButton)ListView1.Items[i].FindControl("lblPurTrans");
                if (DrpListPur.SelectedValue == "1")
                {
                    lblPurview.Visible = true;
                }
                if (DrpListPur.SelectedValue == "2")
                {
                    lblPurPrints.Visible = true;
                }
                if (DrpListPur.SelectedValue == "3")
                {
                    lblPurTrans.Visible = true;
                }
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
        protected void lblExport_Click(object sender, EventArgs e)
        {
            decimal TotAmt = 0;
            List<ViewData> List = new List<ViewData>();
            if (ViewState["Exele"] != null)
            {
                //blank
                ViewData blank = new ViewData();
                blank.TransDate = "";
                blank.Trnsaction = "";
                blank.Invoice = "";
                blank.Amount = "";
                blank.Paidby = "";
                blank.Reference = "";
                blank.CusVender = "";
                List.Add(blank);
                //MAINHEADER
                ViewData MAINHEADER = new ViewData();
                string Todate = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();
                string Fromdate = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();
                MAINHEADER.TransDate = "";
                MAINHEADER.Trnsaction = "";
                MAINHEADER.Invoice = "Sales Report" + "\n" + " Daily Sales Transactions" + " " + "From Date :-" + Todate + "To Date :-" + Fromdate + " All Transactions";
                //MAINHEADER.Invoice = "Daily Sales Transactions" + " From Date:-(" + Todate + ") To Date:-(" + Fromdate + ")";
                MAINHEADER.Amount = "";
                MAINHEADER.Paidby = "";
                MAINHEADER.Reference = "";
                MAINHEADER.CusVender = "";
                List.Add(MAINHEADER);
                //Header
                ViewData Header = new ViewData();
                Header.TransDate = "DATE";
                Header.Trnsaction = "TRANSACTION";
                Header.Invoice = "TransDocNo";
                Header.Amount = "AMOUNT";
                Header.Paidby = "PAID BY";
                Header.Reference = "REFERENCE";
                Header.CusVender = "CUSTOMER/VENDER";
                List.Add(Header);
                //BLANKHEADER
                var date1 = DateTime.Now.ToString("dd/MMM/yyyy");
                var searial1 = DB.tbltranssubtypes.Single(p => p.transid == 4101 && p.transsubid == 410103 && p.transsubtype1 == "POS Invoice").serialno;
                ViewData BLANKHeader = new ViewData();
                BLANKHeader.TransDate = date1 + "(" + date1 + ")";
                BLANKHeader.Trnsaction = "";
                BLANKHeader.Invoice = "";
                BLANKHeader.Amount = "";
                BLANKHeader.Paidby = "";
                BLANKHeader.Reference = "";
                BLANKHeader.CusVender = "";
                List.Add(blank);
                //MainHD
                List<ICTR_HD> Listitem = ((List<ICTR_HD>)ViewState["Exele"]).ToList();
                foreach (ICTR_HD Items in Listitem.Where(p => p.ACTIVE == true))
                {
                    ViewData obj = new ViewData();
                    obj.TransDate = Items.TRANSDATE.ToShortDateString();
                    obj.Trnsaction = CompAndUser(Convert.ToInt32(Items.MYTRANSID)).ToString();
                    obj.Invoice = Items.TransDocNo.ToString();
                    obj.Amount = Items.TOTAMT.ToString();
                    obj.Paidby = PaidBy(Convert.ToInt32(Items.MYTRANSID)).ToString();
                    obj.Reference = Items.REFERENCE.ToString();
                    obj.CusVender = CustVendID(Convert.ToInt32(Items.CUSTVENDID)).ToString();
                    List.Add(obj);
                    TotAmt += Convert.ToDecimal(Items.TOTAMT);
                    ViewState["TotAMO"] = TotAmt;
                    //List<ICTR_HD> List = DB.ICTR_HD.OrderBy(m => m.MYTRANSID).ToList();                     
                }
                decimal AMT = Convert.ToDecimal(ViewState["TotAMO"]);
                ViewData obj1 = new ViewData();
                obj1.TransDate = "";
                obj1.Trnsaction = "";
                obj1.Invoice = "Total:->";
                obj1.Amount = AMT.ToString();
                obj1.Paidby = "";
                obj1.Reference = "";
                obj1.CusVender = "";
                List.Add(obj1);

                var date = DateTime.Now.ToString("dd/MMM/yyyy");
                var searial = DB.tbltranssubtypes.Single(p => p.transid == 4101 && p.transsubid == 410103 && p.transsubtype1 == "POS Invoice").serialno;
                ((Sales_Master)this.Master).ExportToExcel<ViewData>(List, date + "(" + searial + ")");
            }

        }

        protected void lblexportDT_Click(object sender, EventArgs e)
        {

            decimal AMTTOT = 0;
            decimal UNIT = 0;
            decimal Costs = 0;
            decimal GrossPr = 0;
            List<ViewDataDT> List = new List<ViewDataDT>();
            ViewDataDT Blank = new ViewDataDT();
            Blank.Trans = "";
            Blank.TransDate = "";
            //Blank.Reference = "";
            Blank.InvoiceTotal = "";
            Blank.MYTRANSID = "";
            Blank.invoiceno = "";
            Blank.DESCRIPTION = "";
            Blank.QUANTITY = "";
            Blank.UNITPRICELocal = "";
            Blank.Cost = "";
            Blank.GrossProfit = "";
            Blank.AMOUNTLocal = "";
            List.Add(Blank);
            //Step1
            ViewDataDT obj = new ViewDataDT();
            List<ICTR_HD> ListitemHD = ((List<ICTR_HD>)ViewState["Exele"]).ToList();
            long MYTrans = Convert.ToInt64(ViewState["HD"]);
            string Fromdate = Convert.ToDateTime(txtdateFrom.Text).ToShortDateString();
            string Todate = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();
            obj.Trans = "";
            obj.TransDate = "";
            //obj.Reference = "";
            obj.InvoiceTotal = "";
            obj.MYTRANSID = "";
            obj.invoiceno = "";
            obj.DESCRIPTION = "____________Sales Report" + "" + "________ _Daily Sales Transactions______From Date :-" + Fromdate + "______ _________To Date :-" + Todate + "__________________ All Transactions___________";
            //"____________Sales Report ___________________Daily Sales Transactions________"+""+"______From Date :-" +MYTrans+ "_______________To Date :- "++"_______"___________All Transactions___________"
            //For The Transaction:-" + MYTrans + "/2017 " + "All Transaction";
            obj.QUANTITY = "";
            obj.UNITPRICELocal = "";
            obj.Cost = "";
            obj.GrossProfit = "";
            obj.AMOUNTLocal = "";
            List.Add(obj);
            //DT Header
            ViewDataDT objDTHeader = new ViewDataDT();
            objDTHeader.Trans = "SALInvoice#";
            objDTHeader.TransDate = "";
            //objDTHeader.Reference = "";
            objDTHeader.InvoiceTotal = "";
            objDTHeader.MYTRANSID = "";
            objDTHeader.invoiceno = "";
            objDTHeader.DESCRIPTION = "";
            objDTHeader.QUANTITY = "";
            objDTHeader.UNITPRICELocal = "";
            objDTHeader.Cost = "";
            objDTHeader.GrossProfit = "";
            objDTHeader.AMOUNTLocal = "";
            List.Add(objDTHeader);
            //DT Header2
            ViewDataDT HeaderDT = new ViewDataDT();
            HeaderDT.Trans = "Trans";
            HeaderDT.TransDate = "TransDate";
            //HeaderDT.Reference = "Reference";
            HeaderDT.InvoiceTotal = "Customer";
            HeaderDT.MYTRANSID = "ITEM CODE";
            HeaderDT.invoiceno = "TransDocNo#";
            HeaderDT.DESCRIPTION = "DESCRIPTION";
            HeaderDT.QUANTITY = "QTY";
            HeaderDT.UNITPRICELocal = "Unit Price";
            HeaderDT.Cost = "Cost";
            HeaderDT.GrossProfit = "GrossProfit";
            HeaderDT.AMOUNTLocal = "Amount";
            List.Add(HeaderDT);

            //Step2
            if (ChkAllHDDT.Checked == true)
            {
                List<ICTR_HD> Listitemhd = ((List<ICTR_HD>)ViewState["Exele"]).ToList();
                foreach (ICTR_HD itemshd in Listitemhd.Where(p => p.ACTIVE == true))
                {
                    //Step first four field HD in DT an after DT data all
                    if (ViewState["Exele"] != null)
                    {
                        if (ViewState["HD"] != null)
                        {
                            long step5MYTRANSID = Convert.ToInt64(itemshd.MYTRANSID);
                            List<ICTR_DT> Listitemdt = DB.ICTR_DT.Where(p => p.MYTRANSID == step5MYTRANSID && p.ACTIVE == true).ToList();
                            foreach (ICTR_DT Itemsdt in Listitemdt)
                            {
                                //
                                ViewDataDT objHD = new ViewDataDT();
                                objHD.MYTRANSID = itemshd.TRANSDATE.ToShortDateString();
                                objHD.invoiceno = CompAndUser(Convert.ToInt32(itemshd.MYTRANSID)).ToString();
                                objHD.DESCRIPTION = itemshd.REFERENCE.ToString();
                                objHD.QUANTITY = CustVendID(Convert.ToInt32(itemshd.CUSTVENDID)).ToString();
                                //objHD.QUANTITY = itemshd.TransDocNo.ToString();

                                ViewDataDT obj5 = new ViewDataDT();
                                obj5.Trans = itemshd.TransDocNo.ToString() + "/" + objHD.invoiceno.ToString() + "/" + objHD.DESCRIPTION.ToString();
                                obj5.TransDate = objHD.MYTRANSID.ToString();
                                //obj5.Reference = objHD.DESCRIPTION.ToString();
                                obj5.InvoiceTotal = objHD.QUANTITY.ToString();
                                obj5.MYTRANSID = Itemsdt.MyProdID.ToString();
                                obj5.invoiceno = InvoiceNODT(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString();
                                obj5.DESCRIPTION = Itemsdt.DESCRIPTION.ToString();
                                obj5.QUANTITY = Itemsdt.QUANTITY.ToString();
                                obj5.UNITPRICELocal = LocalPiece(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(Itemsdt.UNITPRICE)).ToString() : "";
                                //var costDT = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.MYTRANSID == Itemsdt.MYTRANSID).FirstOrDefault().CostAmount);
                                var costDT = Convert.ToDecimal(Itemsdt.CostAmount);
                                obj5.Cost = costDT.ToString(); //BaseCost(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString();
                                double UnitPR = obj5.UNITPRICELocal == "" ? 0 : Convert.ToDouble(obj5.UNITPRICELocal);//Convert.ToDouble(Itemsdt.QUANTITY);
                                double Cossts = Convert.ToDouble(obj5.Cost);
                                double Gross = UnitPR - Cossts;
                                obj5.GrossProfit = Gross.ToString();
                                obj5.AMOUNTLocal = Local(Convert.ToInt32(Itemsdt.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(Itemsdt.AMOUNT)).ToString() : "";
                                List.Add(obj5);
                                AMTTOT += obj5.AMOUNTLocal == "" ? 0 : Convert.ToDecimal(obj5.AMOUNTLocal);
                                UNIT += obj5.UNITPRICELocal == "" ? 0 : Convert.ToDecimal(obj5.UNITPRICELocal);
                                Costs += Convert.ToDecimal(obj5.Cost);
                                GrossPr += Convert.ToDecimal(Gross);
                                //ViewState["step5MYTRANSID"] = items.MYTRANSID.ToString();
                                ViewState["AMTTOT"] = AMTTOT;
                                ViewState["Unit"] = UNIT;
                                ViewState["Costs"] = Costs;
                                ViewState["GrossPr"] = GrossPr;
                            }
                        }
                    }
                }
                //Total(Amount&UnitPrice)
                decimal AMTHD = Convert.ToDecimal(ViewState["AMTTOT"]);
                decimal UnitHD = Convert.ToDecimal(ViewState["Unit"]);
                decimal Cosst = Convert.ToDecimal(ViewState["Costs"]);
                decimal groos = Convert.ToDecimal(ViewState["GrossPr"]);
                ViewDataDT Total = new ViewDataDT();
                Total.MYTRANSID = "";
                Total.invoiceno = "";
                Total.DESCRIPTION = "";
                Total.QUANTITY = "Total :->";
                Total.UNITPRICELocal = UnitHD.ToString();
                Total.Cost = Cosst.ToString();
                Total.GrossProfit = groos.ToString();
                Total.AMOUNTLocal = AMTHD.ToString();
                List.Add(Total);
            }
            else
            {
                if (ViewState["exportDT"] != null)
                {

                    List<ICTR_DT> ListitemDT = ((List<ICTR_DT>)ViewState["exportDT"]).ToList();
                    foreach (ICTR_DT items in ListitemDT)
                    {
                        //Step first four field HD in DT an after DT data all
                        if (ViewState["Exele"] != null)
                        {
                            if (ViewState["HD"] != null)
                            {
                                long step5MYTRANSID = Convert.ToInt64(ViewState["HD"]);
                                List<ICTR_HD> Listitem = DB.ICTR_HD.Where(p => p.MYTRANSID == step5MYTRANSID).ToList();
                                foreach (ICTR_HD Items in Listitem)
                                {
                                    //
                                    ViewDataDT obj5 = new ViewDataDT();
                                    obj5.MYTRANSID = Items.TRANSDATE.ToShortDateString();
                                    obj5.invoiceno = CompAndUser(Convert.ToInt32(Items.MYTRANSID)).ToString();
                                    obj5.DESCRIPTION = Items.REFERENCE.ToString();
                                    obj5.QUANTITY = CustVendID(Convert.ToInt32(Items.CUSTVENDID)).ToString();
                                    //obj5.QUANTITY = Items.TransDocNo.ToString();
                                    //List.Add(obj5);
                                    //                                
                                    ViewDataDT objDT = new ViewDataDT();
                                    objDT.Trans = Items.TransDocNo.ToString() + "/" + obj5.invoiceno.ToString() + "/" + obj5.DESCRIPTION.ToString();
                                    objDT.TransDate = obj5.MYTRANSID.ToString();
                                    //objDT.Reference = obj5.DESCRIPTION.ToString();
                                    objDT.InvoiceTotal = obj5.QUANTITY.ToString();
                                    objDT.MYTRANSID = items.MyProdID.ToString();
                                    objDT.invoiceno = InvoiceNODT(Convert.ToInt32(items.MYTRANSID)).ToString();
                                    objDT.DESCRIPTION = items.DESCRIPTION.ToString();
                                    objDT.QUANTITY = items.QUANTITY.ToString();
                                    objDT.UNITPRICELocal = LocalPiece(Convert.ToInt32(items.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(items.UNITPRICE)).ToString() : "";
                                    //var costDT = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.MYTRANSID == items.MYTRANSID).FirstOrDefault().CostAmount);
                                    var costDT = Convert.ToDecimal(items.CostAmount);
                                    objDT.Cost = costDT.ToString(); //BaseCost(Convert.ToInt32(items.MYTRANSID)).ToString();
                                    double unitPR = objDT.UNITPRICELocal == "" ? 0 : Convert.ToDouble(objDT.UNITPRICELocal);//Convert.ToDouble(objDT.QUANTITY);                                    
                                    double Cossts = Convert.ToDouble(objDT.Cost);
                                    double Gross = unitPR - Cossts;
                                    objDT.GrossProfit = Gross.ToString();
                                    objDT.AMOUNTLocal = Local(Convert.ToInt32(items.MYTRANSID)).ToString() == "POS Invoice" ? (Convert.ToDecimal(items.AMOUNT)).ToString() : "";
                                    List.Add(objDT);
                                    AMTTOT += objDT.AMOUNTLocal == "" ? 0 : Convert.ToDecimal(objDT.AMOUNTLocal);
                                    UNIT += objDT.UNITPRICELocal == "" ? 0 : Convert.ToDecimal(objDT.UNITPRICELocal);
                                    Costs += Convert.ToDecimal(objDT.Cost);
                                    GrossPr += Convert.ToDecimal(objDT.GrossProfit);
                                    //ViewState["step5MYTRANSID"] = items.MYTRANSID.ToString();
                                    ViewState["AMTTOT"] = AMTTOT;
                                    ViewState["Unit"] = UNIT;
                                    ViewState["Costs"] = Costs;
                                    ViewState["GrossPr"] = GrossPr;
                                }
                            }
                        }
                    }
                    //Total(Amount&UnitPrice)
                    decimal AMTHD = Convert.ToDecimal(ViewState["AMTTOT"]);
                    decimal UnitHD = Convert.ToDecimal(ViewState["Unit"]);
                    decimal Cosst = Convert.ToDecimal(ViewState["Costs"]);
                    decimal groos = Convert.ToDecimal(ViewState["GrossPr"]);
                    ViewDataDT Total = new ViewDataDT();
                    Total.Trans = "";
                    Total.TransDate = "";
                    //HeaderDT.Reference = "Reference";
                    Total.InvoiceTotal = "";
                    Total.MYTRANSID = "";
                    Total.invoiceno = "";
                    Total.DESCRIPTION = "";
                    Total.QUANTITY = "Total :->";
                    Total.UNITPRICELocal = UnitHD.ToString();
                    Total.Cost = Cosst.ToString();
                    Total.GrossProfit = groos.ToString();
                    Total.AMOUNTLocal = AMTHD.ToString();
                    List.Add(Total);

                }
            }

            //Step3
            for (int i = 1; i <= 2; i++)
            {
                ViewDataDT obj3 = new ViewDataDT();
                obj3.MYTRANSID = "";
                obj3.invoiceno = "";
                obj3.DESCRIPTION = "";
                obj3.QUANTITY = "";
                obj3.UNITPRICELocal = "";
                obj3.AMOUNTLocal = "";
                List.Add(obj3);
            }
            //Step4
            //ViewDataDT obj4 = new ViewDataDT();
            //var searial = DB.tbltranssubtypes.Single(p => p.transid == 4101 && p.transsubid == 410103 && p.transsubtype1 == "POS Invoice").serialno;
            //var date = DateTime.Now.ToString("dd/MMM/yyyy");
            //obj4.MYTRANSID = date + "(" + searial + ")";
            //obj4.invoiceno = "";
            //obj4.DESCRIPTION = "";
            //obj4.QUANTITY = "";
            //obj4.UNITPRICELocal = "";
            //obj4.AMOUNTLocal = "";
            //List.Add(obj4);
            //HD Header 
            //ViewDataDT objHDHeader = new ViewDataDT();
            //objHDHeader.MYTRANSID = "Date";
            //objHDHeader.invoiceno = "Transaction";
            //objHDHeader.DESCRIPTION = "Reference";
            //objHDHeader.QUANTITY = "Custermer/vender";
            //objHDHeader.UNITPRICELocal = "";
            //objHDHeader.AMOUNTLocal = "";
            //List.Add(objHDHeader);
            //step5

            //if (ViewState["Exele"] != null)
            //{
            //    if (ViewState["step5MYTRANSID"] != null)
            //    {
            //        long step5MYTRANSID = Convert.ToInt64(ViewState["step5MYTRANSID"]);
            //        List<ICTR_HD> Listitem = DB.ICTR_HD.Where(p => p.MYTRANSID == step5MYTRANSID).ToList();
            //        foreach (ICTR_HD Items in Listitem)
            //        {
            //            ViewDataDT obj5 = new ViewDataDT();
            //            obj5.MYTRANSID = Items.TRANSDATE.ToShortDateString();
            //            obj5.invoiceno = CompAndUser(Convert.ToInt32(Items.MYTRANSID)).ToString();
            //            obj5.DESCRIPTION = Items.REFERENCE.ToString();
            //            obj5.QUANTITY = CustVendID(Convert.ToInt32(Items.CUSTVENDID)).ToString();
            //            obj5.UNITPRICELocal = "";
            //            obj5.AMOUNTLocal = "";
            //            List.Add(obj5);
            //        }
            //    }
            //}

            ((Sales_Master)this.Master).ExportToExcel<ViewDataDT>(List, "SALInvoice#");
        }

       



    }
    public class ViewData
    {
        public string TransDate { get; set; }
        public string Trnsaction { get; set; }
        public string Invoice { get; set; }
        public string Amount { get; set; }
        public string Paidby { get; set; }
        public string Reference { get; set; }
        public string CusVender { get; set; }
    }
    public class ViewDataDT
    {
        public string Trans { get; set; }
        public string TransDate { get; set; }
        //public string Reference { get; set; }
        public string InvoiceTotal { get; set; }
        public string MYTRANSID { get; set; }
        public string invoiceno { get; set; }
        public string DESCRIPTION { get; set; }
        public string QUANTITY { get; set; }
        public string UNITPRICELocal { get; set; }
        public string Cost { get; set; }
        public string GrossProfit { get; set; }
        public string AMOUNTLocal { get; set; }

    }
}