using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using System.Threading;
using Database;
using System.Globalization;

namespace Web.Master
{
    public partial class ProductItemRequest : System.Web.UI.Page
    {
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        CallEntities DB = new CallEntities();


        int TID = 6;
        int LID = 0;
        DateTime DT = DateTime.Now;
        string TDTT = "";
        string Delivery = "";
        int Chiefid = 0;
        List<Database.ICIT_BR> BRList = new List<Database.ICIT_BR>();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            Chiefid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);



            int maxdt = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1)).Day;
            int pulsedt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).Day;
            int Month = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).Month;

            if (maxdt == pulsedt)
            {
                if (Month == 12)
                {
                    DT = new DateTime(DateTime.Today.Year + 1, 1, 1);
                    if (DT.DayOfWeek == DayOfWeek.Friday)
                    {
                        DT = DT.AddDays(1);
                    }
                }
                else
                {
                    DT = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
                    if (DT.DayOfWeek == DayOfWeek.Friday)
                    {
                        DT = DT.AddDays(1);
                    }
                }
            }
            else
            {
                DT = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1);
                if (DT.DayOfWeek == DayOfWeek.Friday)
                {
                    DT = DT.AddDays(1);
                }
            }

            if (!IsPostBack)
            {
                //BRList = DB.ICIT_BR.Where(p => p.TenentID == TID && p.MySysName == "PUR").ToList();
                //foreach (Database.ICIT_BR BRitem in BRList)
                //{
                //    Database.ICIT_BR obj = BRitem;
                //    if (BRitem.ReqDate == null)
                //    {
                //        obj.ReqDate = DT;
                //        obj.ReqQTY = null;
                //        obj.ReqStatus = null;
                //        DB.SaveChanges();
                //    }
                //    else
                //    {
                //        DateTime RDT = Convert.ToDateTime(BRitem.ReqDate);


                //        if (DT != RDT)
                //        {
                //            obj.ReqDate = DT;
                //            obj.ReqQTY = null;
                //            obj.ReqStatus = null;
                //            DB.SaveChanges();
                //        }
                //    }
                //}
                DropDate();
                DateT(DT);
                AllExtraItem();
            }
        }

        public void DateT(DateTime DT)
        {


            string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen_TenentID" + TID + "_" + DateTime.Now.ToString("MMddyyyyhhmmss");
            string str = "Delete  from temp_AMPMMeal where TableName1 like '" + "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen_TenentID" + TID + "_" + "%';";
            str += "INSERT INTO [dbo].[temp_AMPMMeal]([ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],[TableName1],[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID],[ProductionDate],UOM) SELECT [ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],'" + tablename + "',[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID],[ProductionDate],UOM  FROM [View_PlannedMealSum] where [ExpectedDelDate] = '" + DT.ToShortDateString() + "'  AND TenentID=" + TID + ";"; // AND ProductionDate is null//WHERE        (ExpectedDelDate BETWEEN '2017-09-16' AND '2017-09-19')
            str += "update temp_AMPMMeal set NormalQty = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate and WProductName=ProductName  and WWeight not in (100,150,200)),0),  TableName1='" + tablename + "';";
            str += "update temp_AMPMMeal set Qty150 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WProductName=ProductName  and WWeight = 100),0),  TableName1='" + tablename + "';";
            str += "update temp_AMPMMeal set Qty200 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WProductName=ProductName  and WWeight = 150),0),  TableName1='" + tablename + "';";
            str += "update temp_AMPMMeal set Qty250 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WProductName=ProductName  and WWeight = 200),0),  TableName1='" + tablename + "';";
            command2 = new SqlCommand(str, con);
            con.Open();
            command2.ExecuteReader();
            con.Close();
            combine();

        }
        public void combine()
        {
            string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen";
            Listview1.DataSource = DB.temp_AMPMMeal.Where(p => p.TenentID == TID && p.TableName1.Contains(tablename)).GroupBy(i => new { i.ExpectedDelDate, i.ProductName }).Select(p => p.FirstOrDefault()).OrderBy(p => p.ExpectedDelDate);
            Listview1.DataBind();

            Recipy();
        }
        public void Recipy()
        {
            DateTime? EXDate = null;
            if (Dateformate.SelectedValue != "0")
            {
                EXDate = Convert.ToDateTime(Dateformate.SelectedValue);



                pnlissue.Visible = false;
                List<Database.Receipe_Menegement> ListReceipeManage = DB.Receipe_Menegement.Where(p => p.TenentID == TID).OrderBy(p => p.recNo).ToList();
                List<Database.Receipe_Menegement> RECIList = new List<Database.Receipe_Menegement>();
                List<Database.Receipe_Menegement> RECIList2 = new List<Database.Receipe_Menegement>();
                List<Database.Receipe_Menegement> IssueItem = new List<Database.Receipe_Menegement>();
                List<Database.ICTR_HD> HDList = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate).ToList();
                List<Database.ICTR_DT> DTList = new List<Database.ICTR_DT>();
                for (int j = 0; j < Listview1.Items.Count; j++)
                {
                    Label kitchProdid = (Label)Listview1.Items[j].FindControl("kitchProdid");
                    Label KUOM = (Label)Listview1.Items[j].FindControl("KUOM");
                    Label Normal = (Label)Listview1.Items[j].FindControl("lblNormal");
                    Label Qty100 = (Label)Listview1.Items[j].FindControl("lblQty100");//100007
                    Label Qty150 = (Label)Listview1.Items[j].FindControl("lblQty150");//100008
                    Label Qty200 = (Label)Listview1.Items[j].FindControl("lblQty200");//100009

                    int Prod = Convert.ToInt32(kitchProdid.Text);
                    int UOMI = Convert.ToInt32(KUOM.Text);
                    string UOMS = UOMI.ToString();

                    int QT100 = Convert.ToInt32(Qty100.Text);
                    int QT150 = Convert.ToInt32(Qty150.Text);
                    int QT200 = Convert.ToInt32(Qty200.Text);
                    int QTNormal = Convert.ToInt32(Normal.Text);


                    if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.UOM == UOMS && p.IOSwitch == "Output").Count() > 0)
                    {

                        if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS).Count() == 1)
                        {
                            Database.Receipe_Menegement Robj = ListReceipeManage.Single(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS);
                            RECIList.Add(Robj);
                        }
                        else if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS).Count() > 1)
                        {
                            Database.Receipe_Menegement Robj = ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS).FirstOrDefault();
                            RECIList.Add(Robj);
                        }
                        //if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT150).Count() == 1)
                        //{
                        //    Database.Receipe_Menegement Robj = ListReceipeManage.Single(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT150);
                        //    RECIList.Add(Robj);
                        //}
                        //else if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT150).Count() > 1)
                        //{
                        //    Database.Receipe_Menegement Robj = ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT150).FirstOrDefault();
                        //    RECIList.Add(Robj);
                        //}
                        //if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT200).Count() == 1)
                        //{
                        //    Database.Receipe_Menegement Robj = ListReceipeManage.Single(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT200);
                        //    RECIList.Add(Robj);
                        //}
                        //else if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT200).Count() > 1)
                        //{
                        //    Database.Receipe_Menegement Robj = ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == QT200).FirstOrDefault();
                        //    RECIList.Add(Robj);
                        //}

                    }
                    else
                    {
                        //Database.Receipe_Menegement ISUOBJ = ListReceipeManage.Single(p => p.TenentID == TID && p.ItemCode == Prod && p.UOM == UOMS && p.IOSwitch == "Output");
                        //IssueItem.Add(ISUOBJ);
                        Database.Receipe_Menegement ISUOBJ = new Database.Receipe_Menegement();
                        ISUOBJ.TenentID = TID;
                        ISUOBJ.recNo = 999;
                        ISUOBJ.IOSwitch = "Output";
                        ISUOBJ.ItemCode = Prod;
                        ISUOBJ.UOM = UOMS;
                        IssueItem.Add(ISUOBJ);
                        pnlissue.Visible = true;
                    }
                }
                foreach (Database.Receipe_Menegement rec in RECIList)
                {

                    int RecID = rec.recNo;
                    List<Database.Receipe_Menegement> RECIList3 = ListReceipeManage.Where(p => p.TenentID == TID && p.IOSwitch == "Input" && p.recNo == RecID).OrderBy(p => p.recNo).ToList();
                    foreach (Database.Receipe_Menegement item in RECIList3)
                    {
                        RECIList2.Add(item);
                    }
                }
                //add extra items
                foreach (Database.ICTR_HD EXT in HDList)
                {
                    int mytranceid = Convert.ToInt32(EXT.MYTRANSID);
                    DTList = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.QUANTITY == 999).ToList();
                    foreach (Database.ICTR_DT EXTDT in DTList)
                    {
                        Database.Receipe_Menegement ExtOBJ = new Database.Receipe_Menegement();
                        ExtOBJ.TenentID = TID;
                        ExtOBJ.recNo = 999;
                        ExtOBJ.IOSwitch = "Output";
                        ExtOBJ.ItemCode = Convert.ToInt32(EXTDT.MyProdID);
                        ExtOBJ.UOM = EXTDT.UOM;
                        ExtOBJ.Qty = Convert.ToDecimal(EXTDT.OVERHEADAMOUNT);
                        RECIList2.Add(ExtOBJ);
                    }
                }
                Listview2.DataSource = RECIList2;
                Listview2.DataBind();
                //receipy issue           
                ISUCOUNT.Text = IssueItem.Count().ToString();
                ReceipyIssue.DataSource = IssueItem;
                ReceipyIssue.DataBind();
                //all default butoon for request and accepted

                int btnRec = 0;
                for (int i = 0; i < Listview2.Items.Count(); i++)
                {
                    TextBox FinalTot = (TextBox)Listview2.Items[i].FindControl("FinalTot");
                    Label ProdIID = (Label)Listview2.Items[i].FindControl("ProdIID");
                    Label lbluom = (Label)Listview2.Items[i].FindControl("lbluom");
                    Label lblrecNo = (Label)Listview2.Items[i].FindControl("lblrecNo");
                    int pid = Convert.ToInt32(ProdIID.Text);
                    string UOMID = lbluom.Text;
                    int Recno = Convert.ToInt32(lblrecNo.Text);

                    if (HDList.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID).Count() > 0)
                    {
                        Database.ICTR_HD HDobj = HDList.Single(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID);
                        int mytranceid = Convert.ToInt32(HDobj.MYTRANSID);
                        DTList = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid).ToList();
                        if (DTList.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.MyProdID == pid && p.UOM == UOMID && p.QUANTITY == Recno && p.EXPIRYDATE == EXDate).Count() > 0)
                        {

                        }
                        else
                        {
                            btnRec = 1;
                        }
                        if (DTList.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.Stutas == "Approve").Count() > 0)
                        {
                            btnAcceptAll.Visible = true;
                        }
                    }
                    else
                    {
                        btnRec = 2;
                    }
                }
                if (btnRec == 1)
                    btnRequest.Visible = true;
                else if (btnRec == 2)
                    btnRequest.Visible = true;
                else
                    btnRequest.Visible = false;
            }
        }

        public void DropDate()
        {
            DateTime MaxDate = Convert.ToDateTime(DB.View_PlannedMealSum.Where(p => p.TenentID == TID).Max(p => p.ExpectedDelDate));

            string SQOCommad = "select ExpectedDelDate from View_PlannedMealSum where TenentID=" + TID + " and ExpectedDelDate between '" + DT + "' and '" + MaxDate + "' group by ExpectedDelDate order by ExpectedDelDate;";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            Dateformate.DataSource = dt;
            Dateformate.DataTextField = "ExpectedDelDate";
            Dateformate.DataValueField = "ExpectedDelDate";
            Dateformate.DataBind();
            Dateformate.Items.Insert(0, new ListItem("-- Select --", "0"));
            Dateformate.SelectedValue = DT.ToString();
            //var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            //for (int i = 0; i < months.Length; i++)
            //{
            //    Dateformate.Items.Add(new ListItem(months[i], i.ToString()));
            //}
        }
        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblNormal = (Label)e.Item.FindControl("lblNormal");
            Label lblQty100 = (Label)e.Item.FindControl("lblQty100");
            Label lblQty150 = (Label)e.Item.FindControl("lblQty150");
            Label lblQty200 = (Label)e.Item.FindControl("lblQty200");
            Label KitchenQTY = (Label)e.Item.FindControl("KitchenQTY");

            int Normal = Convert.ToInt32(lblNormal.Text);
            int Qty100 = Convert.ToInt32(lblQty100.Text);
            int Qty150 = Convert.ToInt32(lblQty150.Text);
            int Qty200 = Convert.ToInt32(lblQty200.Text);
            int KitQTY = Normal + Qty100 + Qty150 + Qty200;            
            KitchenQTY.Text = KitQTY.ToString();

            if (Normal == 0)
            {
                lblNormal.Attributes["class"] = "btn btn-danger btn-sm";
            }
            else
            {
                lblNormal.Attributes["class"] = "btn btn-success btn-sm";

            }
            if (Qty100 == 0)
            {
                lblQty100.Attributes["class"] = "btn btn-danger btn-sm";
            }
            else
            {
                lblQty100.Attributes["class"] = "btn btn-success btn-sm";
            }
            if (Qty150 == 0)
            {
                lblQty150.Attributes["class"] = "btn btn-danger btn-sm";
            }
            else
            {
                lblQty150.Attributes["class"] = "btn btn-success btn-sm";
            }
            if (Qty200 == 0)
            {
                lblQty200.Attributes["class"] = "btn btn-danger btn-sm";
            }
            else
            {
                lblQty200.Attributes["class"] = "btn btn-success btn-sm";
            }


        }
        public string GetReceipy(int RCP)
        {
            if (DB.tbl_Receipe.Where(p => p.TenentID == TID && p.recNo == RCP).Count() > 0)
            {
                string RCPName = DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == RCP).Receipe_English;
                return RCPName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetProd(int PID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
            {
                string ProdName = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PID).ProdName1;
                return ProdName;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            List<Database.tbl_Receipe> ioMore = new List<Database.tbl_Receipe>();
            List<Database.Receipe_Menegement> RecList = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.IOSwitch == "Output").OrderBy(p => p.recNo).ToList();
            TextBox FinalTot = (TextBox)e.Item.FindControl("FinalTot");
            Label ProdIID = (Label)e.Item.FindControl("ProdIID");
            Label R3 = (Label)e.Item.FindControl("R3");
            Label lbluom = (Label)e.Item.FindControl("lbluom");
            Label Label7 = (Label)e.Item.FindControl("Label7");

            Label lblrecNo = (Label)e.Item.FindControl("lblrecNo");
            Label lblIOswitch = (Label)e.Item.FindControl("lblIOswitch");
            LinkButton LinkAccept = (LinkButton)e.Item.FindControl("LinkAccept");
            Label RequestQTY = (Label)e.Item.FindControl("RequestQTY");
            Label ReceiveQTY = (Label)e.Item.FindControl("ReceiveQTY");
            Label output = (Label)e.Item.FindControl("output");


            string IOswitch = lblIOswitch.Text.ToString();
            int Recno = Convert.ToInt32(lblrecNo.Text);
            int PID = Convert.ToInt32(ProdIID.Text);//Prodid
            decimal QTY = Convert.ToDecimal(R3.Text);
            decimal Final = 0;
            //Extra Item
            int OT = 0;
            string OTUOM = "";
            if (!String.IsNullOrEmpty(output.Text))
            {
                string[] Outitemuom = output.Text.Split('-');
                if (Outitemuom[0] == "Extra Item")
                { }
                else
                {
                    OT = Convert.ToInt32(Outitemuom[0]);
                    OTUOM = Outitemuom[1].ToString();
                }
            }

            int UOM = Convert.ToInt32(lbluom.Text);//UOM
            string UOMID = UOM.ToString();
            FinalTot.Text = QTY.ToString(); //Calculation of final total in receip textbox
            DateTime EXDate = Convert.ToDateTime(Dateformate.SelectedValue);

            //process of more than one receipe dropdown bind
            Label R1 = (Label)e.Item.FindControl("R1");
            DropDownList MoreReceipe = (DropDownList)e.Item.FindControl("MoreReceipe");
            if (RecList.Where(p => p.ItemCode == OT && p.UOM == OTUOM).Count() > 1)
            {
                RecList = RecList.Where(p => p.ItemCode == OT && p.UOM == OTUOM).ToList();
                foreach (Database.Receipe_Menegement Recmore in RecList)
                {
                    int RDID = Recmore.recNo;
                    if (DB.tbl_Receipe.Where(p => p.TenentID == TID && p.recNo == RDID).Count() > 0)
                    {
                        Database.tbl_Receipe obj = DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == RDID);
                        ioMore.Add(obj);
                    }
                    //check DT data
                    if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID).Count() > 0)
                    {
                        Database.ICTR_HD HDobj = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID);
                        int mytranceid = Convert.ToInt32(HDobj.MYTRANSID);
                        if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.MyProdID == PID && p.UOM == UOMID && p.QUANTITY == RDID && p.EXPIRYDATE == EXDate).Count() > 0)
                        {
                            ViewState["More"] = RDID;
                        }
                    }
                }
                MoreReceipe.DataSource = ioMore;
                MoreReceipe.DataTextField = "Receipe_English";
                MoreReceipe.DataValueField = "recNo";
                MoreReceipe.DataBind();

                if (ViewState["More"] != null)
                {
                    string MMore = ViewState["More"].ToString();
                    MoreReceipe.SelectedValue = MMore;
                }
                string MrecNO = MoreReceipe.SelectedValue;

                R1.Visible = false;
                MoreReceipe.Visible = true;
                Recno = Convert.ToInt32(MrecNO);
                lblrecNo.Text = MrecNO;
            }
            //

            for (int i = 0; i < Listview1.Items.Count(); i++)
            {
                
                Label kitchProdid = (Label)Listview1.Items[i].FindControl("kitchProdid");
                Label KitchenQTY = (Label)Listview1.Items[i].FindControl("KitchenQTY");                                
                int KPRODID = Convert.ToInt32(kitchProdid.Text);
                decimal total = Convert.ToDecimal(KitchenQTY.Text);

                if (OT == KPRODID)
                {
                    Final = total * QTY;
                    FinalTot.Text = Final.ToString();
                    btnRequest.Visible = true;
                }
                if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID).Count() > 0)
                {
                    Database.ICTR_HD HDobj = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID);
                    int mytranceid = Convert.ToInt32(HDobj.MYTRANSID);
                    if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.MyProdID == PID && p.UOM == UOMID && p.QUANTITY == Recno && p.EXPIRYDATE == EXDate).Count() > 0)
                    {
                        Database.ICTR_DT DTobj = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.MyProdID == PID && p.UOM == UOMID && p.QUANTITY == Recno && p.EXPIRYDATE == EXDate);
                        if (DTobj.Stutas == "Accepted")
                        {
                            Label7.Text = "Accepted";
                            RequestQTY.Text = DTobj.OVERHEADAMOUNT.ToString();
                            ReceiveQTY.Text = DTobj.AMOUNT.ToString();
                            LinkAccept.Visible = false;
                            //btnRequest.Visible = false;
                        }
                        else if (DTobj.Stutas == "Approve")
                        {
                            LinkAccept.Visible = true;
                            RequestQTY.Text = DTobj.OVERHEADAMOUNT.ToString();
                            ReceiveQTY.Text = DTobj.AMOUNT.ToString();
                            //btnRequest.Visible = false;
                        }
                        else if (DTobj.Stutas == "Waitting")
                        {
                            LinkAccept.Visible = false;
                            Label7.Text = "Waiting";
                            RequestQTY.Text = DTobj.OVERHEADAMOUNT.ToString();
                            //btnRequest.Visible = false;
                        }
                    }


                }
                //}

            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {

            DateTime TRANSDATE = Convert.ToDateTime(Dateformate.SelectedValue);
            int MYTRANSIDout = 0;
            int MYTRANSIDIN = 0;
            int RFormMYTRANSID = 0;
            if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.locationID == LID && p.transid == 8101 && p.transsubid == 810101 && p.TRANSDATE == TRANSDATE).Count() == 1)
            {
                Database.ICTR_HD obje = DB.ICTR_HD.Single(p => p.TenentID == TID && p.locationID == LID && p.transid == 8101 && p.transsubid == 810101 && p.TRANSDATE == TRANSDATE && p.TranType == "Store In");
                //MYTRANSIDIN = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.locationID == LID && p.transid == 8101 && p.transsubid == 810101 && p.TRANSDATE == TRANSDATE && p.TranType == "Store In").MYTRANSID);
                MYTRANSIDIN = Convert.ToInt32(obje.MYTRANSID);
                int OUTID = Convert.ToInt32(obje.LinkTransID);
                MYTRANSIDout = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == TRANSDATE && p.TranType == "Store out" && p.MYTRANSID == OUTID).MYTRANSID);

                RFormMYTRANSID = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.transid == 9101 && p.transsubid == 910101 && p.TRANSDATE == TRANSDATE && p.TranType == "Request form" && p.locationID == LID).MYTRANSID);
            }
            else
            {
                MYTRANSIDout = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                insert_ICTR_HD(TID, MYTRANSIDout, 0, 1, "I", "Store out", 8101, 810102, "STR", 0, 0, "L", "N", "N", 0, "N", 0, 0, 0, "N", "N", "N", 0, TRANSDATE, "N", "N", 0, "N", "N", "N", "N", "N", "N", "N", true, 0, DateTime.Now, DateTime.Now, DateTime.Now, "0", 0, "Waitting", LID, "", "", "", 0, "", "", 0, 0, 0);

                MYTRANSIDIN = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                insert_ICTR_HD(TID, MYTRANSIDIN, 0, LID, "I", "Store In", 8101, 810101, "STR", 0, 0, "L", "N", "N", 0, "N", 0, 0, 0, "N", "N", "N", 0, TRANSDATE, "N", "N", 0, "N", "N", "N", "N", "N", "N", "N", true, 0, DateTime.Now, DateTime.Now, DateTime.Now, "0", 0, "Waitting", 0, "", "", "", 0, "", "", MYTRANSIDout, 0, 0);
                insert_ICTR_HD(TID, MYTRANSIDout, 0, 1, "I", "Store out", 8101, 810102, "STR", 0, 0, "L", "N", "N", 0, "N", 0, 0, 0, "N", "N", "N", 0, TRANSDATE, "N", "N", 0, "N", "N", "N", "N", "N", "N", "N", true, 0, DateTime.Now, DateTime.Now, DateTime.Now, "0", 0, "Waitting", LID, "", "", "", 0, "", "", MYTRANSIDIN, 0, 0);
                
                RFormMYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                insert_ICTR_HD(TID, RFormMYTRANSID, 0, LID, "I", "Request form", 9101, 910101, "IC", 0, 0, "L", "N", "N", 0, "N", 0, 0, 0, "N", "N", "N", 0, TRANSDATE, "N", "N", 0, "N", "N", "N", "N", "N", "N", "N", true, 0, DateTime.Now, DateTime.Now, DateTime.Now, "0", 0, "Draft", 0, "", "", "", 0, "", "", 0, 0, 0);
            }
            for (int i = 0; i < Listview2.Items.Count(); i++)
            {
                TextBox FinalTot = (TextBox)Listview2.Items[i].FindControl("FinalTot");
                Label ProdIID = (Label)Listview2.Items[i].FindControl("ProdIID");
                Label lbluom = (Label)Listview2.Items[i].FindControl("lbluom");
                CheckBox ChkAll = (CheckBox)Listview2.Items[i].FindControl("ChkAll");
                Label lblrecNo = (Label)Listview2.Items[i].FindControl("lblrecNo");
                Label lblIOswitch = (Label)Listview2.Items[i].FindControl("lblIOswitch");
                Label RequestQTY = (Label)Listview2.Items[i].FindControl("RequestQTY");
                Label ReceiveQTY = (Label)Listview2.Items[i].FindControl("ReceiveQTY");


                decimal ReqQTY = Convert.ToDecimal(RequestQTY.Text);//TOTAMT
                decimal ReceQTY = Convert.ToDecimal(ReceiveQTY.Text);//AmtPaid //DT-AMOUNT
                int Recno = Convert.ToInt32(lblrecNo.Text);//Quantity
                int UOM = Convert.ToInt32(lbluom.Text);//UOM
                string Duom = UOM.ToString();
                int PID = Convert.ToInt32(ProdIID.Text);//myprodid
                decimal FTotal = Convert.ToDecimal(FinalTot.Text);//OVERHEADAMOUNT
                string IOswitch = lblIOswitch.Text.ToString();//Description

                DateTime TODATE = DateTime.Now;
                //p.MyProdID == PID && p.UOM == UOMID && p.QUANTITY == Recno && p.EXPIRYDATE == EXDate
                if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDout && p.MyProdID == PID && p.QUANTITY == Recno && p.EXPIRYDATE == TRANSDATE && p.UOM == Duom).Count() == 0) //   && UOM == Duom 
                {
                    int MYIDIN = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDIN).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDIN).Max(p => p.MYID) + 1) : 1;
                    insert_ICTR_DT(TID, MYTRANSIDIN, LID, MYIDIN, PID, "LF", "LF", "0", "STR", 0, 0, 0, IOswitch, UOM.ToString(), Recno, ReqQTY, ReceQTY, FTotal, "", 0, "", "", 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", TRANSDATE, true, "", 0, 0, "", "Waitting");

                    int MYIDOUT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDout).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDout).Max(p => p.MYID) + 1) : 1;
                    insert_ICTR_DT(TID, MYTRANSIDout, 1, MYIDOUT, PID, "LF", "LF", "0", "STR", 0, 0, 0, IOswitch, UOM.ToString(), Recno, ReqQTY, ReceQTY, FTotal, "", 0, "", "", 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", TRANSDATE, true, "", 0, 0, "", "Waitting");

                    int RFormMYID = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == RFormMYTRANSID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == RFormMYTRANSID).Max(p => p.MYID) + 1) : 1;
                    insert_ICTR_DT(TID, RFormMYTRANSID, LID, RFormMYID, PID, "LF", "LF", "0", "IC", 0, 0, 0, IOswitch, UOM.ToString(), Recno, ReqQTY, ReceQTY, FTotal, "", 0, "", "", 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", TRANSDATE, true, "", 0, 0, "", "Draft");
                }
            }

            Recipy();
        }

        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkAccept")
            {
                int rec = 0;
                DropDownList DuplicateRec = (DropDownList)e.Item.FindControl("DuplicateRec");
                Label lblrecNo = (Label)e.Item.FindControl("lblrecNo");
                if (DuplicateRec.Visible == true)
                {
                    rec = Convert.ToInt32(DuplicateRec.SelectedValue);
                }
                else
                {
                    rec = Convert.ToInt32(lblrecNo.Text);
                }
                string[] ID = e.CommandArgument.ToString().Split('-');
                int Tenent = Convert.ToInt32(ID[0]);
                //rec = Convert.ToInt32(ID[1]);
                string IO = (ID[2].ToString());
                int ProdID = Convert.ToInt32(ID[3]);
                string UIDD = ID[4].ToString();
                int BRUOM = Convert.ToInt32(ID[4]);
                DateTime EXDate = Convert.ToDateTime(Dateformate.SelectedValue);
                if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID).Count() > 0)
                {
                    //out
                    Database.ICTR_HD HDobj = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID);
                    int mytranceid = Convert.ToInt32(HDobj.MYTRANSID);
                    Database.ICTR_DT DTobj = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.MyProdID == ProdID && p.UOM == UIDD && p.QUANTITY == rec && p.EXPIRYDATE == EXDate);
                    DTobj.Stutas = "Accepted";
                    DB.SaveChanges();
                    //in
                    int mytranceidIN = Convert.ToInt32(HDobj.LinkTransID);
                    Database.ICTR_DT DTobjin = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytranceidIN && p.MyProdID == ProdID && p.UOM == UIDD && p.QUANTITY == rec && p.EXPIRYDATE == EXDate);
                    DTobjin.Stutas = "Accepted";
                    DB.SaveChanges();
                    //
                    //Post Proccess
                    Database.ICIT_BR BRDATA = DB.ICIT_BR.Single(p => p.TenentID == TID && p.LocationID == 1 && p.MyProdID == ProdID && p.UOM == BRUOM);
                    int MYTRANSID = Convert.ToInt32(BRDATA.MYTRANSID);
                    string period_code = BRDATA.period_code.ToString();
                    int nwQTY = Convert.ToInt32(DTobj.AMOUNT);
                    decimal UnitCost = Convert.ToDecimal(BRDATA.UnitCost);
                    string Bin_Per = BRDATA.Bin_Per.ToString();
                    string Reference = BRDATA.Reference.ToString();
                    string Active = BRDATA.Active.ToString();
                    int CRUP_ID = Convert.ToInt32(BRDATA.CRUP_ID);

                    Database.tbltranssubtype objsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == 8101 && p.transsubid == 810101);
                    Classes.EcommAdminClass.PostICIT_BR(TID, 1, MYTRANSID, ProdID, BRUOM, period_code, nwQTY, UnitCost, Bin_Per, Reference, Active, CRUP_ID, objsubtype);

                    //DB.SaveChanges();
                }
                Recipy();
            }
        }
        protected void btnAcceptAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Listview2.Items.Count; i++)
            {
                DropDownList DuplicateRec = (DropDownList)Listview2.Items[i].FindControl("DuplicateRec");
                Label lblrecNo = (Label)Listview2.Items[i].FindControl("lblrecNo");
                Label lblIOswitch = (Label)Listview2.Items[i].FindControl("lblIOswitch");
                Label ProdIID = (Label)Listview2.Items[i].FindControl("ProdIID");
                Label lbluom = (Label)Listview2.Items[i].FindControl("lbluom");

                int rec = 0;
                if (DuplicateRec.Visible == true)
                {
                    rec = Convert.ToInt32(DuplicateRec.SelectedValue);
                }
                else
                {
                    rec = Convert.ToInt32(lblrecNo.Text);
                }
                string IO = (lblIOswitch.Text);
                int ProdID = Convert.ToInt32(ProdIID.Text);
                string UIDD = lbluom.Text;
                int BRUOM = Convert.ToInt32(lbluom.Text);
                DateTime EXDate = Convert.ToDateTime(Dateformate.SelectedValue);

                if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID).Count() > 0)
                {
                    Database.ICTR_HD HDobj = DB.ICTR_HD.Single(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate && p.Terms == LID);
                    int mytranceid = Convert.ToInt32(HDobj.MYTRANSID);
                    Database.ICTR_DT DTobj = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.MyProdID == ProdID && p.UOM == UIDD && p.QUANTITY == rec && p.EXPIRYDATE == EXDate);
                    if (DTobj.Stutas == "Approve")
                    {
                        DTobj.Stutas = "Accepted";
                        DB.SaveChanges();

                        int mytranceidIN = Convert.ToInt32(HDobj.LinkTransID);
                        Database.ICTR_DT DTobjin = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytranceidIN && p.MyProdID == ProdID && p.UOM == UIDD && p.QUANTITY == rec && p.EXPIRYDATE == EXDate);
                        DTobjin.Stutas = "Accepted";
                        DB.SaveChanges();
                        //Post Proccess
                        Database.ICIT_BR BRDATA = DB.ICIT_BR.Single(p => p.TenentID == TID && p.LocationID == 1 && p.MyProdID == ProdID && p.UOM == BRUOM);
                        int MYTRANSID = Convert.ToInt32(BRDATA.MYTRANSID);
                        string period_code = BRDATA.period_code.ToString();
                        int nwQTY = Convert.ToInt32(DTobj.AMOUNT);
                        decimal UnitCost = Convert.ToDecimal(BRDATA.UnitCost);
                        string Bin_Per = BRDATA.Bin_Per.ToString();
                        string Reference = BRDATA.Reference.ToString();
                        string Active = BRDATA.Active.ToString();
                        int CRUP_ID = Convert.ToInt32(BRDATA.CRUP_ID);

                        Database.tbltranssubtype objsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == 8101 && p.transsubid == 810101);
                        Classes.EcommAdminClass.PostICIT_BR(TID, 1, MYTRANSID, ProdID, BRUOM, period_code, nwQTY, UnitCost, Bin_Per, Reference, Active, CRUP_ID, objsubtype);
                    }

                }
                Recipy();
            }
        }
        public string getOutPut(int RID)
        {
            if (DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RID && p.IOSwitch == "Output").Count() > 0)
            {

                int ID = Convert.ToInt32(DB.Receipe_Menegement.Single(p => p.TenentID == TID && p.recNo == RID && p.IOSwitch == "Output").ItemCode);

                string ProdName = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ID).ProdName1;
                return ProdName;
            }
            else if (RID == 999)
            {
                return "Extra Item";
            }
            else
            {
                return "Not Found";
            }
        }

        public string OutPut(int RID)
        {
            if (DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RID && p.IOSwitch == "Output").Count() > 0)
            {
                Database.Receipe_Menegement outobj = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RID && p.IOSwitch == "Output").FirstOrDefault();
                string PID = outobj.ItemCode.ToString() + "-" + outobj.UOM;
                return PID;
            }
            else if (RID == 999)
            {
                return "Extra Item";
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetUOM(int UID)
        {
            if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UID).Count() > 0)
            {
                string UOMname = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == UID).UOMNAME1;
                return UOMname;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void MoreReceipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList MoreReceipe = (DropDownList)sender;
            ListViewDataItem itemData = (ListViewDataItem)MoreReceipe.NamingContainer;

            DropDownList MItem = (DropDownList)itemData.FindControl("MoreReceipe");
            int MRecNo = Convert.ToInt32(MItem.SelectedValue);
            ViewState["More"] = MRecNo;
            ///////
            List<Database.Receipe_Menegement> ListReceipeManage = DB.Receipe_Menegement.Where(p => p.TenentID == TID).OrderBy(p => p.recNo).ToList();
            List<Database.Receipe_Menegement> RECIList = new List<Database.Receipe_Menegement>();
            List<Database.Receipe_Menegement> RECIList2 = new List<Database.Receipe_Menegement>();
            for (int j = 0; j < Listview1.Items.Count; j++)
            {
                Label kitchProdid = (Label)Listview1.Items[j].FindControl("kitchProdid");
                Label KUOM = (Label)Listview1.Items[j].FindControl("KUOM");

                int Prod = Convert.ToInt32(kitchProdid.Text);
                int UOMI = Convert.ToInt32(KUOM.Text);
                string UOMS = UOMI.ToString();

                if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.UOM == UOMS && p.IOSwitch == "Output").Count() > 0)
                {

                    if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS).Count() == 1)
                    {
                        Database.Receipe_Menegement Robj = ListReceipeManage.Single(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS);
                        RECIList.Add(Robj);
                    }
                    else if (ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS).Count() > 1)
                    {
                        Database.Receipe_Menegement Robj = ListReceipeManage.Where(p => p.TenentID == TID && p.ItemCode == Prod && p.IOSwitch == "Output" && p.UOM == UOMS && p.recNo == MRecNo).FirstOrDefault();
                        RECIList.Add(Robj);
                    }
                }
            }
            foreach (Database.Receipe_Menegement rec in RECIList)
            {
                int RecID = rec.recNo;
                List<Database.Receipe_Menegement> RECIList3 = ListReceipeManage.Where(p => p.TenentID == TID && p.IOSwitch == "Input" && p.recNo == RecID).OrderBy(p => p.recNo).ToList();
                foreach (Database.Receipe_Menegement item in RECIList3)
                {
                    RECIList2.Add(item);
                }
            }
            Listview2.DataSource = RECIList2;
            Listview2.DataBind();
        }

        protected void Dateformate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime FormDT = Convert.ToDateTime(Dateformate.SelectedValue);
            DateT(FormDT);
        }
        public void AllExtraItem()
        {
            ListItemExtra.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID);
            ListItemExtra.DataBind();
        }

        protected void ListExtra_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList drpExtUOM = (DropDownList)e.Item.FindControl("drpExtUOM");
            Label lblextProdID = (Label)e.Item.FindControl("lblextProdID");
            Label lblExtUOMID = (Label)e.Item.FindControl("lblExtUOMID");
            int EXTPID = Convert.ToInt32(lblextProdID.Text);
            int EXTUOM = Convert.ToInt32(lblExtUOMID.Text);

            List<Database.ICUOM> ListTemp = new List<Database.ICUOM>();

            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == EXTPID).Count() > 0)
            {
                int UOMPROD = Convert.ToInt32(DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == EXTPID).FirstOrDefault().UOM);

                if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOMPROD).Count() > 0)
                {
                    Database.ICUOM OnjUON = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOMPROD).FirstOrDefault();
                    ListTemp.Add(OnjUON);
                }
            }
            List<Database.Tbl_Multi_Color_Size_Mst> listmultics = DB.Tbl_Multi_Color_Size_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == EXTPID && p.RecordType == "MultiUOM").ToList();

            foreach (Database.Tbl_Multi_Color_Size_Mst itemcs in listmultics)
            {
                Database.ICUOM listuom123 = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == itemcs.RecTypeID);

                ListTemp.Add(listuom123);
            }

            if (ListTemp.Count() > 0)
            {
                drpExtUOM.DataSource = ListTemp;
                drpExtUOM.DataTextField = "UOMNAME1";
                drpExtUOM.DataValueField = "UOM";
                drpExtUOM.DataBind();
            }
        }
        protected void ListItemExtra_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            DateTime EXDate = Convert.ToDateTime(Dateformate.SelectedValue);
            if (e.CommandName == "Extra")
            {
                string[] ID = e.CommandArgument.ToString().Split('-');
                int ExtPID = Convert.ToInt32(ID[0]);
                string ExtUOM = (ID[1]);
                List<Database.Receipe_Menegement> RECIList2 = new List<Database.Receipe_Menegement>();
                List<Database.ICTR_HD> HDList = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == EXDate).ToList();

                for (int D = 0; D < Listview2.Items.Count; D++)
                {
                    Label lblrecNo = (Label)Listview2.Items[D].FindControl("lblrecNo");
                    Label ProdIID = (Label)Listview2.Items[D].FindControl("ProdIID");
                    Label output = (Label)Listview2.Items[D].FindControl("output");
                    TextBox FinalTot = (TextBox)Listview2.Items[D].FindControl("FinalTot");
                    Label lbluom = (Label)Listview2.Items[D].FindControl("lbluom");

                    int RexNo = Convert.ToInt32(lblrecNo.Text);
                    int ProdID = Convert.ToInt32(ProdIID.Text);
                    string[] OTT = output.Text.Split('-');
                    int outProdID = 0;
                    if(!String.IsNullOrEmpty(OTT[0]))
                    {
                        if (OTT[0] == "Extra Item")
                        { outProdID = 0; }
                        else
                        { outProdID = Convert.ToInt32(OTT[0]); }
                    }
                    decimal QTY = Convert.ToDecimal(FinalTot.Text);
                    string uom = lbluom.Text;

                    if(DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RexNo && p.ItemCode == ProdID && p.UOM == uom).Count() > 0)
                    {
                        Database.Receipe_Menegement ADDEXT = DB.Receipe_Menegement.Single(p => p.TenentID == TID && p.recNo == RexNo && p.ItemCode == ProdID && p.UOM == uom);
                        RECIList2.Add(ADDEXT);
                    }
                }
                foreach (Database.ICTR_HD EXT in HDList)
                {
                    int mytranceid = Convert.ToInt32(EXT.MYTRANSID);
                    List<Database.ICTR_DT> DTList = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytranceid && p.QUANTITY == 999).ToList();
                    foreach (Database.ICTR_DT EXTDT in DTList)
                    {
                        Database.Receipe_Menegement ExtOBJ123 = new Database.Receipe_Menegement();
                        ExtOBJ123.TenentID = TID;
                        ExtOBJ123.recNo = 999;
                        ExtOBJ123.IOSwitch = "Output";
                        ExtOBJ123.ItemCode = Convert.ToInt32(EXTDT.MyProdID);
                        ExtOBJ123.UOM = EXTDT.UOM;
                        ExtOBJ123.Qty = Convert.ToDecimal(EXTDT.OVERHEADAMOUNT);
                        RECIList2.Add(ExtOBJ123);
                    }
                }
                Database.Receipe_Menegement ExtOBJ = new Database.Receipe_Menegement();
                ExtOBJ.TenentID = TID;
                ExtOBJ.recNo = 999;
                ExtOBJ.IOSwitch = "Output";
                ExtOBJ.ItemCode = ExtPID;
                ExtOBJ.UOM = ExtUOM;
                ExtOBJ.Qty = Convert.ToDecimal(1.000);
                RECIList2.Add(ExtOBJ);

                Listview2.DataSource = RECIList2;
                Listview2.DataBind();
            }
        }
        //Classes function ICTR_HD
        public void insert_ICTR_HD(int TenentID = 0, int MYTRANSID = 0, int ToTenentID = 0, int TOLOCATIONID = 0, string MainTranType = null, string TranType = null, int transid = 0, int transsubid = 0, string MYSYSNAME = null, decimal? COMPID = null, decimal? CUSTVENDID = null, string LF = null, string PERIOD_CODE = null, string ACTIVITYCODE = null, decimal? MYDOCNO = null, string USERBATCHNO = null, decimal? TOTQTY1 = null, decimal? TOTAMT = null, decimal? AmtPaid = null, string PROJECTNO = null, string CounterID = null, string PrintedCounterInvoiceNo = null, int JOID = 0, DateTime? TRANSDATE = null, string REFERENCE = null, string NOTES = null, int CRUP_ID = 0, string GLPOST = null, string GLPOST1 = null, string GLPOSTREF1 = null, string GLPOSTREF = null, string ICPOST = null, string ICPOSTREF = null, string USERID = null, bool ACTIVE = false, int COMPANYID = 0, DateTime? ENTRYDATE = null, DateTime? ENTRYTIME = null, DateTime? UPDTTIME = null, string InvoiceNO = "0", decimal? Discount = null, string Status = null, int Terms = 0, string Custmerid = null, string Swit1 = null, string ExtraField2 = null, int RefTransID = 0, string Reason = null, string TransDocNo = null, int LinkTransID = 0, int invoice_Type = 0, int invoice_Source = 0)
        {

            DateTime Todate = DateTime.Now;
            Database.ICTR_HD objICTR_HD = new Database.ICTR_HD();
            bool flag = false;
            if (DB.ICTR_HD.Where(p => p.TenentID == TenentID && p.MYTRANSID == MYTRANSID).Count() < 1)
            {
                objICTR_HD = new Database.ICTR_HD();
                flag = true;
            }
            else
            {
                objICTR_HD = DB.ICTR_HD.Single(p => p.MYTRANSID == MYTRANSID && p.TenentID == TenentID);
            }
            objICTR_HD.TenentID = TenentID;
            objICTR_HD.MYTRANSID = MYTRANSID;
            objICTR_HD.ToTenantID = ToTenentID;
            objICTR_HD.locationID = TOLOCATIONID;
            objICTR_HD.MainTranType = MainTranType;
            objICTR_HD.TransType = TranType;
            objICTR_HD.transid = transid;
            objICTR_HD.transsubid = transsubid;
            objICTR_HD.TranType = TranType;
            objICTR_HD.COMPID = COMPID;
            objICTR_HD.MYSYSNAME = MYSYSNAME;
            objICTR_HD.CUSTVENDID = Convert.ToDecimal(CUSTVENDID);
            objICTR_HD.LF = LF;
            objICTR_HD.PERIOD_CODE = PERIOD_CODE;
            objICTR_HD.ACTIVITYCODE = ACTIVITYCODE;
            objICTR_HD.MYDOCNO = MYDOCNO;
            objICTR_HD.USERBATCHNO = USERBATCHNO;
            objICTR_HD.TOTAMT = TOTAMT;
            objICTR_HD.TOTQTY = TOTQTY1;
            objICTR_HD.AmtPaid = AmtPaid;
            objICTR_HD.PROJECTNO = PROJECTNO;
            objICTR_HD.CounterID = CounterID;
            objICTR_HD.PrintedCounterInvoiceNo = PrintedCounterInvoiceNo;
            objICTR_HD.JOID = JOID;
            objICTR_HD.TRANSDATE = Convert.ToDateTime(TRANSDATE);
            objICTR_HD.REFERENCE = REFERENCE;
            objICTR_HD.NOTES = NOTES;
            objICTR_HD.GLPOST = GLPOST;
            objICTR_HD.GLPOST1 = GLPOST1;
            objICTR_HD.GLPOSTREF = GLPOSTREF;
            objICTR_HD.GLPOSTREF1 = GLPOSTREF1;
            objICTR_HD.ICPOST = ICPOST;
            objICTR_HD.ICPOSTREF = ICPOSTREF;
            objICTR_HD.USERID = USERID;
            objICTR_HD.ACTIVE = true;
            //objICTR_HD.CREATED_BY = Convert.ToInt32(Session["UserId"].ToString());
            //objICTR_HD.UPDATED_BY = Convert.ToInt32(Session["UserId"].ToString());
            objICTR_HD.COMPANYID = COMPANYID;
            objICTR_HD.ENTRYDATE = ENTRYDATE;
            objICTR_HD.ENTRYTIME = ENTRYTIME;
            objICTR_HD.UPDTTIME = UPDTTIME;
            objICTR_HD.Discount = Discount;
            objICTR_HD.Status = Status;
            objICTR_HD.Terms = Terms;
            objICTR_HD.ExtraField1 = Custmerid;
            objICTR_HD.ExtraField2 = ExtraField2;
            objICTR_HD.ExtraSwitch1 = Swit1;
            objICTR_HD.RefTransID = RefTransID;
            objICTR_HD.ExtraSwitch2 = Reason;
            objICTR_HD.TransDocNo = TransDocNo;
            objICTR_HD.LinkTransID = LinkTransID;
            objICTR_HD.InvoiceNO = InvoiceNO.ToString();
            objICTR_HD.invoice_Source = invoice_Source;
            objICTR_HD.invoice_Type = invoice_Type;

            // objICTR_HD.ExtraSwitch2 = "";
            if (flag == true)
            {
                DB.ICTR_HD.AddObject(objICTR_HD);
            }
            DB.SaveChanges();
        }
        //Classes function ICTR_DT
        public void insert_ICTR_DT(int TenentID, int MYTRANSID, int locationID, int MYID, int MyProdID, string REFTYPE, string REFSUBTYPE, string PERIOD_CODE, string MYSYSNAME, int JOID, int JOBORDERDTMYID, int ACTIVITYID, string DESCRIPTION, string UOM, int QUANTITY, decimal UNITPRICE, decimal AMOUNT, decimal OVERHEADAMOUNT, string BATCHNO, int BIN_ID, string BIN_TYPE, string GRNREF, decimal DISPER, decimal DISAMT, decimal TAXPER, decimal TAXAMT, decimal PROMOTIONAMT, int CRUP_ID, string GLPOST, string GLPOST1, string GLPOSTREF1, string GLPOSTREF, string ICPOST, string ICPOSTREF, DateTime EXPIRYDATE, bool ACTIVE, string SWITCH1, int COMPANYID1, int DelFlag, string ITEMID, string Status = null)
        {

            if (DB.ICTR_DT.Where(p => p.TenentID == TenentID && p.MYTRANSID == MYTRANSID && p.locationID == locationID && p.MYID == MYID).Count() > 0)
            {
                Database.ICTR_DT objICTR_DT = DB.ICTR_DT.Single(p => p.TenentID == TenentID && p.MYTRANSID == MYTRANSID && p.locationID == locationID && p.MYID == MYID);

                objICTR_DT.MyProdID = MyProdID;
                objICTR_DT.REFTYPE = REFTYPE;
                objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                objICTR_DT.MYSYSNAME = MYSYSNAME;
                objICTR_DT.JOID = JOID;
                objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                objICTR_DT.ACTIVITYID = ACTIVITYID;
                objICTR_DT.DESCRIPTION = DESCRIPTION;
                objICTR_DT.UOM = UOM;
                objICTR_DT.QUANTITY = QUANTITY;
                objICTR_DT.UNITPRICE = UNITPRICE;
                objICTR_DT.AMOUNT = AMOUNT;
                objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                objICTR_DT.BATCHNO = BATCHNO;
                objICTR_DT.BIN_ID = BIN_ID;
                objICTR_DT.BIN_TYPE = BIN_TYPE;
                objICTR_DT.GRNREF = GRNREF;
                objICTR_DT.DISPER = DISPER;
                objICTR_DT.DISAMT = DISAMT;
                objICTR_DT.TAXAMT = TAXAMT;
                objICTR_DT.TAXPER = TAXPER;
                objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                objICTR_DT.GLPOST = GLPOST;
                objICTR_DT.GLPOST1 = GLPOST1;
                objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                objICTR_DT.GLPOSTREF = GLPOSTREF;
                objICTR_DT.ICPOST = ICPOST;
                objICTR_DT.ICPOSTREF = ICPOSTREF;
                objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                objICTR_DT.COMPANYID = COMPANYID1;
                objICTR_DT.SWITCH1 = SWITCH1;
                objICTR_DT.ACTIVE = ACTIVE;
                objICTR_DT.DelFlag = DelFlag;
                objICTR_DT.Stutas = Status;
                DB.SaveChanges();
            }
            else
            {
                Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();

                objICTR_DT.TenentID = TenentID;
                objICTR_DT.MYTRANSID = MYTRANSID;
                objICTR_DT.locationID = locationID;
                objICTR_DT.MYID = MYID;
                objICTR_DT.MyProdID = MyProdID;
                objICTR_DT.REFTYPE = REFTYPE;
                objICTR_DT.REFSUBTYPE = REFSUBTYPE;
                objICTR_DT.PERIOD_CODE = PERIOD_CODE;
                objICTR_DT.MYSYSNAME = MYSYSNAME;
                objICTR_DT.JOID = JOID;
                objICTR_DT.JOBORDERDTMYID = JOBORDERDTMYID;
                objICTR_DT.ACTIVITYID = ACTIVITYID;
                objICTR_DT.DESCRIPTION = DESCRIPTION;
                objICTR_DT.UOM = UOM;
                objICTR_DT.QUANTITY = QUANTITY;
                objICTR_DT.UNITPRICE = UNITPRICE;
                objICTR_DT.AMOUNT = AMOUNT;
                objICTR_DT.OVERHEADAMOUNT = OVERHEADAMOUNT;
                objICTR_DT.BATCHNO = BATCHNO;
                objICTR_DT.BIN_ID = BIN_ID;
                objICTR_DT.BIN_TYPE = BIN_TYPE;
                objICTR_DT.GRNREF = GRNREF;
                objICTR_DT.DISPER = DISPER;
                objICTR_DT.DISAMT = DISAMT;
                objICTR_DT.TAXAMT = TAXAMT;
                objICTR_DT.TAXPER = TAXPER;
                objICTR_DT.PROMOTIONAMT = PROMOTIONAMT;
                objICTR_DT.GLPOST = GLPOST;
                objICTR_DT.GLPOST1 = GLPOST1;
                objICTR_DT.GLPOSTREF1 = GLPOSTREF1;
                objICTR_DT.GLPOSTREF = GLPOSTREF;
                objICTR_DT.ICPOST = ICPOST;
                objICTR_DT.ICPOSTREF = ICPOSTREF;
                objICTR_DT.EXPIRYDATE = EXPIRYDATE;
                objICTR_DT.COMPANYID = COMPANYID1;
                objICTR_DT.SWITCH1 = SWITCH1;
                objICTR_DT.ACTIVE = ACTIVE;
                objICTR_DT.DelFlag = DelFlag;
                objICTR_DT.Stutas = Status;
                DB.ICTR_DT.AddObject(objICTR_DT);
                DB.SaveChanges();
            }
        }


    }
}