using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace Web.Sales
{
    public partial class DeliverNotes : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        PropertyFile objProFile = new PropertyFile();
        bool FirstFlag = true;
        int TID, LID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                FillContractorID();
                BindDataReciv();
                pnlform.Visible = false;
                pnlGridView.Visible = true;
                if (Request.QueryString["TactionNo"] != null)
                {
                    int str1 = Convert.ToInt32(Request.QueryString["TactionNo"]);
                    BindList(str1);
                    ViewState["TrctionNo"] = str1;

                }

            }
            pnlSuccessMsg123.Visible = false;
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
        public void BindList(int str1)
        {
            int COMPID = 0;
            string UseID = UID.ToString();
            List<TBLLOCATION> ListTID = new List<TBLLOCATION>();
            if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID).Count() == 1)
            {
                COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID).COMPID;
            }
            var list = DB.ICIT_BR.Where(p => p.LocationID == LID &&p.MYTRANSID==str1).ToList();
            var result1 = (from pm in DB.ICTR_DT
                           join
                             ur in DB.TBLPRODUCTs on pm.MyProdID equals ur.MYPRODID
                           where pm.ACTIVE == true && pm.MYTRANSID == str1
                           select new { pm.MYTRANSID, pm.MyProdID, pm.QUANTITY, pm.MYID, pm.COMPANYID, pm.AMOUNT, ur.MultiBinStore, ur.MultiColor, ur.MultiSize, ur.Serialized, ur.Perishable, ur.MultiUOM, ur.UOM }).ToList();
            var List = result1.GroupBy(p => p.MyProdID).Select(p => p.FirstOrDefault()).ToList();

            List<ICTR_DT> TempList = new List<ICTR_DT>();
            for (int i = 0; i < List.Count(); i++)
            {
                for (int j = 0; j < list.Count(); j++)
                {
                    ICTR_DT objICTR_DT = new ICTR_DT();

                    objICTR_DT.COMPANYID = List[i].COMPANYID;
                    objICTR_DT.MyProdID = List[i].MyProdID;
                    objICTR_DT.AMOUNT = List[i].AMOUNT;
                    objICTR_DT.MYTRANSID = List[i].MYTRANSID;
                    objICTR_DT.MYID = List[i].MYID;
                    objICTR_DT.QUANTITY = List[i].QUANTITY;
                    objICTR_DT.DelFlag = LID;
                    TempList.Add(objICTR_DT);
                }

            }
            listresivDelvry.DataSource = TempList;
            listresivDelvry.DataBind();
            for (int i = 0; i < listresivDelvry.Items.Count(); i++)
            {
                TextBox txtDateDievery = (TextBox)listresivDelvry.Items[i].FindControl("txtDateDievery");
                txtDateDievery.Text = DateTime.Now.ToShortDateString();
            }
            ICTR_HD obj = DB.ICTR_HD.Single(p => p.MYTRANSID == str1);
            txtTRANSDATE.Text = obj.TRANSDATE.ToShortDateString();
            drpProjectName.SelectedValue = obj.PROJECTNO;
            txtNOTES.Text = obj.NOTES;
            txtrefrshno.Text = obj.REFERENCE;
            int Qty = Convert.ToInt32(obj.TOTQTY);
            ViewState["Qty"] = Qty;
            drpMainTran.SelectedValue = obj.transid.ToString();
            BindSubTran();
            drpSubTra.SelectedValue = obj.transsubid.ToString();
            pnlform.Visible = true;
            pnlGridView.Visible = false;
        }
        public void FillContractorID()
        {           
            Classes.EcommAdminClass.getdropdown(drpMainTran, TID, "", "", "", "Eco_tbltranstype");
            Classes.EcommAdminClass.getdropdown(drpBatch, TID, "", "", "", "Eco_TBLSYSUSERBATCH");
            Classes.EcommAdminClass.getdropdown(drpProjectName, TID, "", "", "", "Eco_TBLPROJECT");
            //dropRefNo.DataSource = DB.ICIT_BR_ReferenceNo.Where(p => p.Active == true && p.Deleted == true);
            //dropRefNo.DataTextField = "ReferenceNo";
            //dropRefNo.DataValueField = "ID";
            //dropRefNo.DataBind();
            //dropRefNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Reference No--", "0"));
        }
        protected void drpMainTran_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubTran();
        }
        public void BindSubTran()
        {
            int MITID = Convert.ToInt32(drpMainTran.SelectedValue);
            string MTID = MITID.ToString();            
            Classes.EcommAdminClass.getdropdown(drpSubTra, TID, MTID, "", "", "Eco_tbltranssubtype");
        }
        public string getproductName(int PID)
        {
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID).ProdName1;
        }
        public string getitemscode(int PID)
        {
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID).UserProdID;
        }
        public string getconpniname(int PID)
        {            
            //var Locatin = DB.TBLLOCATION.Where (p => p.TenantID == TID).ToList ();
            string LID = PID.ToString();
            return DB.TBLLOCATIONs.Single(p => p.LOCATIONID == PID).LOCNAME1;
        }

        protected void btnsave_Click(object sender, EventArgs e)
         {
             
                 int TCtionNumber = Convert.ToInt32(ViewState["TrctionNo"]);
                
                 int Qty = Convert.ToInt32(ViewState["Qty"]);
                 int Total = 0;
                 for (int i = 0; i < listresivDelvry.Items.Count(); i++)
                 {
                     TextBox txtuomQty = (TextBox)listresivDelvry.Items[i].FindControl("txtuomQty");
                     if (txtuomQty.Text != "")
                     {
                         int TXT = Convert.ToInt32(txtuomQty.Text);
                         Total = Total + TXT;
                     }
                     else
                     {
                         pnlSuccessMsg123.Visible = true;
                         lblMsg123.Text = "Enter The Quantity";
                         return;
                     }

                 }
                 if (Qty == Total)
                 {
                     for (int i = 0; i < listresivDelvry.Items.Count(); i++)
                     {
                         Label lbltrction = (Label)listresivDelvry.Items[i].FindControl("lbltrction");
                         TextBox txtuomQty = (TextBox)listresivDelvry.Items[i].FindControl("txtuomQty");
                         Label lbllocation = (Label)listresivDelvry.Items[i].FindControl("lbllocation");
                         Label lblproduct = (Label)listresivDelvry.Items[i].FindControl("lblproduct");
                         TextBox txtDateDievery = (TextBox)listresivDelvry.Items[i].FindControl("txtDateDievery");
                         Label lbltotal = (Label)listresivDelvry.Items[i].FindControl("lbltotal");
                         int MyProdID = Convert.ToInt32(lblproduct.Text);
                         int TransNo = Convert.ToInt32(lbltrction.Text);
                         var list = DB.ICTR_DT.Single(p => p.MYTRANSID == TransNo && p.MyProdID == MyProdID && p.DelFlag == 0);
                         int TenantID = TID;
                         string period_code = objProFile.PERIOD_CODE;
                         string MySysName = "PUR".ToString();
                         int UOM1 = Convert.ToInt32(list.UOM);
                         decimal UnitCost = Convert.ToDecimal(list.UNITPRICE);
                         int MYTRANSID = TransNo;
                         int NewQty = Convert.ToInt32(txtuomQty.Text);
                         //int OpQty = Convert.ToInt32(txtuomQty.Text);
                         //int OnHand = OpQty;
                         //int QtyOut = OpQty;
                         //int QtyConsumed = OpQty;
                         //int QtyReserved = OpQty;
                         //int MinQty = 0;
                         //int MaxQty = 0;
                         string Bin_Per = "N";
                         string Fuctions = "ADD";
                         string Active = "Y";
                         string Reference = txtrefrshno.Text;
                      //   int LeadTime = 0;
                         int CRUP_ID = 99999999;
                         string pagename = "Sales";
                         Classes.EcommAdminClass.insertICIT_BR(TenantID, MyProdID, period_code, MySysName, UOM1, LID, UnitCost, MYTRANSID, Bin_Per, NewQty,  Reference, Active, CRUP_ID, Fuctions, pagename);

                     }

                     ICTR_HD objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == TCtionNumber);
                     objICTR_HD.ACTIVE = true;
                     objICTR_HD.Status = "RDSOCT";
                     DB.SaveChanges();
                     BindDataReciv();
                     pnlform.Visible = false;
                     pnlGridView.Visible = true;
                 }
                 else
                 {
                     pnlSuccessMsg123.Visible = true;
                     lblMsg123.Text = " Quantity Are Not Same";
                 }
            
         }
        public string getSuplierName(int UID)
        {
            return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == UID).COMPNAME1;
        }
        protected void grdPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditIncom")
            {
                int Str1 = Convert.ToInt32(e.CommandArgument);
                BindList(Str1);
                ViewState["TrctionNo"] = Str1;
            }
        }
        public void BindDataReciv()
        {

            var result1 = (from pm in DB.ICTR_HD
                           join
                             ur in DB.ICTR_DT on pm.MYTRANSID equals ur.MYTRANSID
                           where pm.ACTIVE == true && pm.Status == "RDSO" 
                           select new { pm.CUSTVENDID, pm.TRANSDATE, pm.TOTAMT, pm.MYTRANSID, pm.Status }).ToList();
            var List = result1.GroupBy(p => p.MYTRANSID).Select(p => p.FirstOrDefault()).ToList();
            grdPO.DataSource = List;
            grdPO.DataBind();
            for (int i = 0; i < grdPO.Items.Count; i++)
            {
                LinkButton lnkbtnPQ = (LinkButton)grdPO.Items[i].FindControl("lnkbtnPQ");

                LinkButton btnDarf = (LinkButton)grdPO.Items[i].FindControl("btnDarf");
                Label lblStatus = (Label)grdPO.Items[i].FindControl("lblStatus");
                Label lblspnoo = (Label)grdPO.Items[i].FindControl("lblspnoo");

                string STS = lblStatus.Text;
                if (STS == "DSO")
                {
                    lblspnoo.Visible = false;
                    btnDarf.Visible = true;
                    lnkbtnPQ.Visible = false;

                }
            }
        }
    }
}