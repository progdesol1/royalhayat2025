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

namespace Web.ReportMst
{
    public partial class AMPMDAY : System.Web.UI.Page
    {
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        int TID = 6;
        string FDTT = "";
        string TDTT = "";
        string Delivery = "";
        int Chiefid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Chiefid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;
            if (!IsPostBack)
            {

                //PRDBIND();
                BINDMEAL();
                int maxdt = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1)).Day;
                int pulsedt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).Day;
                DateTime DT;
                if(maxdt == pulsedt)
                    DT = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                else
                    DT = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1);
                txtdateFrom.Text = txtdateTO.Text = DT.ToString("dd/MMM/yyyy"); ;//DateTime.Now.ToString("dd/MMM/yyyy");
                FDTT = Convert.ToDateTime(txtdateFrom.Text).ToShortDateString();
                TDTT = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();

                if (DrpFromDelivery.SelectedItem != null)
                {
                    Delivery = DrpFromDelivery.SelectedItem.ToString();
                }                
                if (string.IsNullOrEmpty(Page.Title))
                {
                    //Page.Title = String.Format(Delivery + " " + "(" + FDTT + "-" + TDTT + ")");
                    Page.Header.Title = String.Format(Delivery + " " + "(" + FDTT + "-" + TDTT + ")");
                }
                if (Request.QueryString["FDT"] != null && Request.QueryString["TDT"] != null)
                {
                    FDTT = Convert.ToDateTime(Request.QueryString["FDT"]).Date.ToString("dd/MMM/yyyy");
                    TDTT = Convert.ToDateTime(Request.QueryString["TDT"]).Date.ToString("dd/MMM/yyyy");
                    txtdateFrom.Text = FDTT;
                    txtdateTO.Text = TDTT;
                    DateTime FDT = Convert.ToDateTime(FDTT);
                    DateTime TDT = Convert.ToDateTime(TDTT);
                    if (TDT < FDT)
                    {
                        pnlWarningMsg.Visible = true;
                        lblWarningMsg.Text = "Please Select Delivery To_date Is Greate Than From_date.....";
                        return;
                    }
                    int FDelTime = DrpFromDelivery.SelectedValue != "" ? Convert.ToInt32(DrpFromDelivery.SelectedValue) : 0;
                    int TDelTime = DrpToDelivery.SelectedValue != "" ? Convert.ToInt32(DrpToDelivery.SelectedValue) : 0;
                    //DateTime EDT = DateTime.Now; //DateTime.Now;
                    DateT(FDT, TDT, FDelTime, TDelTime);
                }
            }
        }
        public void PRDBIND()
        {
            string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen";
            Listview1.DataSource = DB.temp_AMPMMeal.Where(p => p.TenentID == TID && p.TableName1.Contains(tablename)).GroupBy(i => new { i.ExpectedDelDate, i.DeliverTime, i.ProductName }).Select(p => p.FirstOrDefault()).OrderBy(p => p.ExpectedDelDate);
            Listview1.DataBind();
        }
        public void combine()
        {
            string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen";
            Listview2.DataSource = DB.temp_AMPMMeal.Where(p => p.TenentID == TID && p.TableName1.Contains(tablename)).GroupBy(i => new { i.ExpectedDelDate, i.ProductName }).Select(p => p.FirstOrDefault()).OrderBy(p => p.ExpectedDelDate);
            Listview2.DataBind();
        }
        public string getMealType1(int Type1)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Type1 && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").Count() > 0)
            {
                string TypeMeal = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Type1 && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").REFNAME1;
                return TypeMeal;
            }
            else
            {
                return "";
            }
        }
        public void BINDMEAL()
        {
            List<Database.REFTABLE> FinelDeliveryTime = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").ToList();

            List<Database.REFTABLE> ListDeliveriTime = new List<Database.REFTABLE>();
            List<Database.planmealcustinvoice> DeliveryTime = DB.planmealcustinvoices.Where(p => p.TenentID == TID).GroupBy(p => p.DeliveryTime).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.planmealcustinvoice itemEMP in DeliveryTime)
            {
                if (FinelDeliveryTime.Where(p => p.TenentID == TID && p.REFID == itemEMP.DeliveryTime && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").Count() > 0)
                {
                    Database.REFTABLE OBJTIME = FinelDeliveryTime.Single(p => p.TenentID == TID && p.REFID == itemEMP.DeliveryTime && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime");
                    ListDeliveriTime.Add(OBJTIME);
                }
            }
            DrpFromDelivery.DataSource = ListDeliveriTime.OrderBy(p => p.REFID);
            DrpFromDelivery.DataTextField = "REFNAME1";
            DrpFromDelivery.DataValueField = "REFID";
            DrpFromDelivery.DataBind();

            DrpToDelivery.DataSource = ListDeliveriTime.OrderByDescending(p => p.REFID);
            DrpToDelivery.DataTextField = "REFNAME1";
            DrpToDelivery.DataValueField = "REFID";
            DrpToDelivery.DataBind(); 
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlWarningMsg.Visible = false;
            FDTT = Convert.ToDateTime(txtdateFrom.Text).ToShortDateString();
            TDTT = Convert.ToDateTime(txtdateTO.Text).ToShortDateString();

            Delivery = DrpFromDelivery.SelectedItem != null ? DrpFromDelivery.SelectedItem.ToString() : "0";
            Page.Header.Title = String.Format(Delivery + " " + "(" + FDTT + "-" + TDTT + ")");
            //List<Database.View_PlannedMealSum> List = DB.View_PlannedMealSum.ToList();
            //DateTime? FDT = null;
            //DateTime? TDT = null;
            //if (txtdateFrom.Text != "")
            //    FDT = Convert.ToDateTime(txtdateFrom.Text);
            //if (txtdateTO.Text != "")
            //    TDT = Convert.ToDateTime(txtdateTO.Text);
            //List = List.Where(p => p.ExpectedDelDate >= FDT && p.ExpectedDelDate <= TDT).ToList();
            //ViewState["EXPDate"] = List;

            DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
            DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
            if (TDT < FDT)
            {
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = "Please Select Delivery To_date Is Greate Than From_date.....";
                return;
            }
            int FDelTime = DrpFromDelivery.SelectedValue!=""? Convert.ToInt32(DrpFromDelivery.SelectedValue) :0;
            int TDelTime = DrpToDelivery.SelectedValue!=""? Convert.ToInt32(DrpToDelivery.SelectedValue):0;
            //DateTime EDT = DateTime.Now; //DateTime.Now;
            DateT(FDT, TDT, FDelTime, TDelTime);

        }
        public void DateT(DateTime DT, DateTime TDT, int FDelTime, int TDelTime)
        {            
            if (chkcom.Checked != true)
            {
                //string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen" + DateTime.Now.ToString("MMddyyyyhhmmss");
                //string str = "Delete  from temp_AMPMMeal where TableName1 like '" + "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen" + "%';";
                //str += "INSERT INTO [dbo].[temp_AMPMMeal]([ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],[TableName1],[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID]) SELECT [ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],'" + tablename + "',[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID]  FROM [View_PlannedMealSum] where [ExpectedDelDate] BETWEEN '" + DT.ToShortDateString() + "' AND '" + TDT.ToShortDateString() + "' AND DeliverTime BETWEEN " + FDelTime + " AND " + TDelTime + " AND ProductionDate is null;"; //WHERE        (ExpectedDelDate BETWEEN '2017-09-16' AND '2017-09-19') AND DeliverTime BETWEEN 89414 AND 89414
                //str += "update temp_AMPMMeal set NormalQty = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WExpectedDelDate = ExpectedDelDate and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight not in (100,150,200)),0),  TableName1='" + tablename + "';";
                //str += "update temp_AMPMMeal set Qty150 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WExpectedDelDate = ExpectedDelDate  and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight = 100),0),  TableName1='" + tablename + "';";
                //str += "update temp_AMPMMeal set Qty200 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WExpectedDelDate = ExpectedDelDate  and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight = 150),0),  TableName1='" + tablename + "';";
                //str += "update temp_AMPMMeal set Qty250 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WExpectedDelDate = ExpectedDelDate  and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight = 200),0),  TableName1='" + tablename + "';";
                string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen_TenentID"+ TID + "_" + DateTime.Now.ToString("MMddyyyyhhmmss");
                string str = "Delete  from temp_AMPMMeal where TableName1 like '" + "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen_TenentID" + TID + "_" + "%';";
                str += "INSERT INTO [dbo].[temp_AMPMMeal]([ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],[TableName1],[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID],[ProductionDate]) SELECT [ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],'" + tablename + "',[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID],[ProductionDate]  FROM [View_PlannedMealSum] where [ExpectedDelDate] BETWEEN '" + DT.ToShortDateString() + "' AND '" + TDT.ToShortDateString() + "' AND DeliverTime BETWEEN " + FDelTime + " AND " + TDelTime + " AND TenentID=" + TID + ";"; //WHERE        (ExpectedDelDate BETWEEN '2017-09-16' AND '2017-09-19') AND DeliverTime BETWEEN 89414 AND 89414
                str += "update temp_AMPMMeal set NormalQty = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight not in (100,150,200)),0),  TableName1='" + tablename + "';";
                str += "update temp_AMPMMeal set Qty150 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight = 100),0),  TableName1='" + tablename + "';";
                str += "update temp_AMPMMeal set Qty200 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight = 150),0),  TableName1='" + tablename + "';";
                str += "update temp_AMPMMeal set Qty250 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeight   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WDeliverTime = DeliverTime  and WProductName=ProductName  and WWeight = 200),0),  TableName1='" + tablename + "';";
                command2 = new SqlCommand(str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
                PRDBIND();
                PNLcombine.Visible = false;
                PNLseprate.Visible = true;
            }
            else
            {
                string tablename = "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen_TenentID" + TID + "_" + DateTime.Now.ToString("MMddyyyyhhmmss");
                string str = "Delete  from temp_AMPMMeal where TableName1 like '" + "temp_" + (((USER_MST)Session["USER"]).LOGIN_ID).ToString() + "kitchen_TenentID" + TID + "_" + "%';";
                str += "INSERT INTO [dbo].[temp_AMPMMeal]([ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],[TableName1],[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID],[ProductionDate]) SELECT [ExpectedDelDate],[PlanID],[Meal],[DeliverTime],[Delivery],'" + tablename + "',[Product],[ProductName],[Weight],[TOTQTY],[NormalQty],[Qty150],[Qty200],[Qty250],[TenentID],[ProductionDate]  FROM [View_PlannedMealSum] where [ExpectedDelDate] BETWEEN '" + DT.ToShortDateString() + "' AND '" + TDT.ToShortDateString() + "'  AND TenentID=" + TID + " ; "; // AND ProductionDate is null//WHERE        (ExpectedDelDate BETWEEN '2017-09-16' AND '2017-09-19')
                str += "update temp_AMPMMeal set NormalQty = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate and WProductName=ProductName  and WWeight not in (100,150,200)),0),  TableName1='" + tablename + "';";
                str += "update temp_AMPMMeal set Qty150 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WProductName=ProductName  and WWeight = 100),0),  TableName1='" + tablename + "';";
                str += "update temp_AMPMMeal set Qty200 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WProductName=ProductName  and WWeight = 150),0),  TableName1='" + tablename + "';";
                str += "update temp_AMPMMeal set Qty250 = ISnull((select SUM(WQTY) from View_PlanMealSumbyWeightCombine   where WTenentID=TenentID and WExpectedDelDate = ExpectedDelDate  and WProductName=ProductName  and WWeight = 200),0),  TableName1='" + tablename + "';";
                command2 = new SqlCommand(str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
                combine();
                PNLcombine.Visible = true;
                PNLseprate.Visible = false;
            }
            
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "normal")
            {
                //int ID = Convert.ToInt32(e.CommandArgument);
                //for (int i = 0; i < Listview1.Items.Count; i++)
                //{
                //    LinkButton lbkNormalQty = (LinkButton)Listview1.Items[i].FindControl("lbkNormalQty");
                //    Label lblNormalQty = (Label)Listview1.Items[i].FindControl("lblNormalQty");
                //    Label MID = (Label)Listview1.Items[i].FindControl("MID");
                //    int MMID = Convert.ToInt32(MID.Text);
                //    int Normal = Convert.ToInt32(lblNormalQty.Text);
                //    if (MMID == ID && Normal == 1)
                //    {
                //        string normalCSS = lbkNormalQty.Attributes["class"];
                //        if (normalCSS == "label label-danger label-sm")
                //        {
                //            lbkNormalQty.CssClass = "label label-success label-sm";
                //        }
                //        else if (normalCSS == "label label-success label-sm")
                //        {
                //            lbkNormalQty.CssClass = "label label-danger label-sm";
                //        }                            
                //    }
                //}
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
                int plan = Convert.ToInt32(Obj.PlanID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime).ToList();
                foreach (Database.planmealcustinvoice item in ListItem)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            if (e.CommandName == "btnQty150")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
                int plan = Convert.ToInt32(Obj.PlanID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime).ToList();
                foreach (Database.planmealcustinvoice item in ListItem)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            if (e.CommandName == "btnQty200")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
                int plan = Convert.ToInt32(Obj.PlanID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime).ToList();
                foreach (Database.planmealcustinvoice item in ListItem)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            if (e.CommandName == "btnQty250")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
                int plan = Convert.ToInt32(Obj.PlanID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime).ToList();
                foreach (Database.planmealcustinvoice item in ListItem)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }

                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            //if(e.CommandName == "lbkView")
            //{
            //    int ID = Convert.ToInt32(e.CommandArgument);
            //    Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
            //    int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
            //    int plan = Convert.ToInt32(Obj.PlanID);
            //    DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
            //    int MyprodID = Convert.ToInt32(Obj.Product);
            //    string Prod = Obj.ProductName;

            //    ViewState["Popgo"] = DeliverTime + "," + plan + "," + ExpDate + "," + MyprodID + "," + Prod;
            //}
        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView ShowPopView = (ListView)e.Item.FindControl("ShowPopView");
            ListView ListView3 = (ListView)e.Item.FindControl("ListView3");
            Label lblMy = (Label)e.Item.FindControl("lblMy");

            LinkButton lbkNormalQty = (LinkButton)e.Item.FindControl("lbkNormalQty");
            LinkButton LinkbtnQty150 = (LinkButton)e.Item.FindControl("LinkbtnQty150");
            LinkButton LinkbtnQty200 = (LinkButton)e.Item.FindControl("LinkbtnQty200");
            LinkButton LinkbtnQty250 = (LinkButton)e.Item.FindControl("LinkbtnQty250");
            Label lblNormalQty = (Label)e.Item.FindControl("lblNormalQty");
            Label lblQty150 = (Label)e.Item.FindControl("lblQty150");
            Label lblQty200 = (Label)e.Item.FindControl("lblQty200");
            Label lblQty250 = (Label)e.Item.FindControl("lblQty250");
            Label lblProductionDate = (Label)e.Item.FindControl("lblProductionDate");
            string ProdDate = lblProductionDate.Text;

            int Normal = Convert.ToInt32(lblNormalQty.Text);
            if (Normal >= 1)
            {
                lblNormalQty.Attributes["class"] = "label label-danger label-sm";
                lblNormalQty.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                //lbkNormalQty.Enabled = true;
                if (ProdDate != "")
                {
                    //lbkNormalQty.Enabled = false;
                    lblNormalQty.Attributes["class"] = "label label-success label-sm";
                    lblNormalQty.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";                          
                }
            }
            //else
            //    lbkNormalQty.Enabled = false;

            int Qty150 = Convert.ToInt32(lblQty150.Text);
            if (Qty150 >= 1) //Qty100
            {
                lblQty150.Attributes["class"] = "label label-danger label-sm";
                lblQty150.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                //LinkbtnQty150.Enabled = true;
                if (ProdDate != "")
                {
                    //LinkbtnQty150.Enabled = false;
                    lblQty150.Attributes["class"] = "label label-success label-sm";
                    lblQty150.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }
            //else
            //    LinkbtnQty150.Enabled = false;

            int Qty200 = Convert.ToInt32(lblQty200.Text);
            if (Qty200 >= 1)//Qty150
            {
                lblQty200.Attributes["class"] = "label label-danger label-sm";
                lblQty200.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                //LinkbtnQty200.Enabled = true;
                if (ProdDate != "")
                {
                    //LinkbtnQty200.Enabled = false;
                    lblQty200.Attributes["class"] = "label label-success label-sm";
                    lblQty200.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }
            //else
                //LinkbtnQty200.Enabled = false;

            int Qty250 = Convert.ToInt32(lblQty250.Text);
            if (Qty250 >= 1)//Qty200
            {
                lblQty250.Attributes["class"] = "label label-danger label-sm";
                lblQty250.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                //LinkbtnQty250.Enabled = true;
                if (ProdDate != "")
                {
                    //LinkbtnQty250.Enabled = false;
                    lblQty250.Attributes["class"] = "label label-success label-sm";
                    lblQty250.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }
            //else
            //    LinkbtnQty250.Enabled = false;
            Label Statusss = (Label)e.Item.FindControl("Statusss");
            int MY = Convert.ToInt32(lblMy.Text);
            Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == MY);
            int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
            int plan = Convert.ToInt32(Obj.PlanID);
            DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
            int MyprodID = Convert.ToInt32(Obj.Product);
            string Prod = Obj.ProductName;            
            
            //Database.planmealcustinvoice Obj111 = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime);
            List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime).ToList();
            ListView3.DataSource = ListItem;
            ListView3.DataBind();
            //foreach (Database.planmealcustinvoice item in ListItem)
            //{
            //    Database.planmealcustinvoice Obj111 = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
            //    Label Driver = (Label)e.Item.FindControl("Driver");
            //    Label Product = (Label)e.Item.FindControl("Product");
            //    Label Customer = (Label)e.Item.FindControl("Customer");
            //    Label QTY = (Label)e.Item.FindControl("QTY");
            //    int DriverID = Convert.ToInt32(Obj111.DriverID);
            //    int COID = Convert.ToInt32(Obj111.CustomerID);
            //    Driver.Text = DriverName(DriverID);
            //    Product.Text = Prod;//Obj111.MYPRODID.ToString();
            //    Customer.Text = CustomID(COID);//Obj111.CustomerID.ToString();
            //    QTY.Text = Obj111.Qty.ToString();
            //}

        }
        public string DriverName(int ID)
        {
            if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == ID && p.EmployeeType == "Driver").Count() > 0)
            {
                string Name = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID && p.EmployeeType == "Driver").firstname;
                return Name;
            }
            else
            {
                return "Not Found";
            }
        }
        public string CustomID(int ID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == ID).Count() > 0)
            {
                string Name = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID).COMPNAME1;
                return Name;
            }
            else
            {
                return "Not Found";
            }
        }
        
        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox CHKPrint = (CheckBox)Listview1.Items[i].FindControl("CHKPrint");
                if (ChkAll.Checked == true)
                {
                    CHKPrint.Checked = true;
                }
                else
                {
                    CHKPrint.Checked = false;
                }
            }

        }

        protected void lblAtion_Click(object sender, EventArgs e)
        {
            string headForPrint = "";
            List<Database.temp_AMPMMeal> TempList = new List<Database.temp_AMPMMeal>();
            //Combine
            if (chkcom.Checked == true)
            {
                headForPrint = "Kitchen Preparation Report Day Wise";
                if (Checkcombine.Checked == true)
                {
                    List<Database.temp_AMPMMeal> ListCombine = DB.temp_AMPMMeal.GroupBy(i => new { i.ExpectedDelDate, i.ProductName }).Select(p => p.FirstOrDefault()).OrderBy(p => p.ExpectedDelDate).ToList();
                    foreach (Database.temp_AMPMMeal items in ListCombine)
                    {
                        Database.temp_AMPMMeal objtemp = ListCombine.Single(p => p.MyID == items.MyID);
                        TempList.Add(objtemp);
                    }
                }
                else
                {
                    for (int i = 0; i < Listview2.Items.Count; i++)
                    {
                        CheckBox CHKPrint3 = (CheckBox)Listview2.Items[i].FindControl("CHKPrint3");
                        Label lblMy3 = (Label)Listview2.Items[i].FindControl("lblMy3");
                        if (CHKPrint3.Checked == true)
                        {
                            int Myidcombine = Convert.ToInt32(lblMy3.Text);
                            Database.temp_AMPMMeal objtemp = DB.temp_AMPMMeal.Single(p => p.MyID == Myidcombine);
                            TempList.Add(objtemp);
                        }
                    }
                }
            }
            else
            {
                //Seprate
                headForPrint = "Kitchen Preparation Report Delivery Wise";
                if (ChkAll.Checked == true)
                {
                    List<Database.temp_AMPMMeal> List = DB.temp_AMPMMeal.GroupBy(i => new { i.ExpectedDelDate, i.DeliverTime, i.ProductName }).Select(p => p.FirstOrDefault()).OrderBy(p => p.ExpectedDelDate).ToList();
                    foreach (Database.temp_AMPMMeal items in List)
                    {
                        Database.temp_AMPMMeal objtemp = List.Single(p => p.MyID == items.MyID);
                        TempList.Add(objtemp);
                    }
                }
                else
                {
                    for (int i = 0; i < Listview1.Items.Count; i++)
                    {
                        CheckBox CHKPrint = (CheckBox)Listview1.Items[i].FindControl("CHKPrint");
                        Label lblMy = (Label)Listview1.Items[i].FindControl("lblMy");
                        if (CHKPrint.Checked == true)
                        {
                            int Myid = Convert.ToInt32(lblMy.Text);
                            Database.temp_AMPMMeal objtemp = DB.temp_AMPMMeal.Single(p => p.MyID == Myid);
                            TempList.Add(objtemp);
                        }
                    }
                }
            }
            if (TempList.Count() == 0)
            {
                pnlWarningMsg.Visible = true;
                lblWarningMsg.Text = "Please Checked Atlist One Checkbox In Kitchen Preparation List.....";
                return;
            }
            Session["AMPMKichenPrint"] = TempList;
            Response.Redirect("/Master/AMPMDAYkitchenPrint.aspx?Head=" + headForPrint);
            
        }

        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView Listcombine123 = (ListView)e.Item.FindControl("Listcombine123");
            Label lblMy3 = (Label)e.Item.FindControl("lblMy3");

            LinkButton LinkNormalQty3 = (LinkButton)e.Item.FindControl("LinkNormalQty3");
            LinkButton LinkbtnQty1503 = (LinkButton)e.Item.FindControl("LinkbtnQty1503");
            LinkButton LinkbtnQty2003 = (LinkButton)e.Item.FindControl("LinkbtnQty2003");
            LinkButton LinkbtnQty2503 = (LinkButton)e.Item.FindControl("LinkbtnQty2503");
            Label lblNormalQty3 = (Label)e.Item.FindControl("lblNormalQty3");
            Label lblQty1503 = (Label)e.Item.FindControl("lblQty1503");
            Label lblQty2003 = (Label)e.Item.FindControl("lblQty2003");
            Label lblQty2503 = (Label)e.Item.FindControl("lblQty2503");
            Label lblProductionDate3 = (Label)e.Item.FindControl("lblProductionDate3");
            string ProdDatec = lblProductionDate3.Text;

            //int Normal = Convert.ToInt32(lblNormalQty.Text);
            //if (Normal >= 1)
            //{
            //    lbkNormalQty.Enabled = true;
            //    if (ProdDate != "")
            //    {
            //        lbkNormalQty.Enabled = false;
            //        lblNormalQty.Attributes["class"] = "label label-success label-sm";
            //        lblNormalQty.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
            //    }
            //}
            //else
            //    lbkNormalQty.Enabled = false;

            int Normal = Convert.ToInt32(lblNormalQty3.Text);
            if (Normal >= 1)
            {
                LinkNormalQty3.Enabled = true;
                if (ProdDatec != "")
                {
                    LinkNormalQty3.Enabled = false;
                    lblNormalQty3.Attributes["class"] = "label label-success label-sm";
                    lblNormalQty3.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }
            else
                LinkNormalQty3.Enabled = false;

            int Qty150 = Convert.ToInt32(lblQty1503.Text);
            if (Qty150 >= 1)
            {
                LinkbtnQty1503.Enabled = true;
                if (ProdDatec != "")
                {
                    LinkbtnQty1503.Enabled = false;
                    lblQty1503.Attributes["class"] = "label label-success label-sm";
                    lblQty1503.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }                
            else
                LinkbtnQty1503.Enabled = false;

            int Qty200 = Convert.ToInt32(lblQty2003.Text);
            if (Qty200 >= 1)
            {
                LinkbtnQty2003.Enabled = true;
                if (ProdDatec != "")
                {
                    LinkbtnQty2003.Enabled = false;
                    lblQty2003.Attributes["class"] = "label label-success label-sm";
                    lblQty2003.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }
            else
                LinkbtnQty2003.Enabled = false;

            int Qty250 = Convert.ToInt32(lblQty2503.Text);
            if (Qty250 >= 1)
            {
                LinkbtnQty2503.Enabled = true;
                if (ProdDatec != "")
                {
                    LinkbtnQty2503.Enabled = false;
                    lblQty2503.Attributes["class"] = "label label-success label-sm";
                    lblQty2503.Attributes["Style"] = "padding-left: 45%;padding-right: 45%;padding-bottom: 4%;padding-top: 4%;";
                }
            }
            else
                LinkbtnQty2503.Enabled = false;

            Label Statusss = (Label)e.Item.FindControl("Statusss");
            int MY = Convert.ToInt32(lblMy3.Text);
            Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == MY);
            int DeliverTime = Convert.ToInt32(Obj.DeliverTime);            
            DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
            int MyprodID = Convert.ToInt32(Obj.Product);
            string Prod = Obj.ProductName;

            //Database.planmealcustinvoice Obj111 = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.planid == plan && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.DeliveryTime == DeliverTime);
            List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID).ToList();
            Listcombine123.DataSource = ListItem;
            Listcombine123.DataBind();
        }

        protected void Checkcombine_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Listview2.Items.Count; i++)
            {
                CheckBox CHKPrint3 = (CheckBox)Listview2.Items[i].FindControl("CHKPrint3");
                if (Checkcombine.Checked == true)
                {
                    CHKPrint3.Checked = true;
                }
                else
                {
                    CHKPrint3.Checked = false;
                }
            }

        }
        
        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "normal")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                int DeliverTime = Convert.ToInt32(Obj.DeliverTime);
                int plan = Convert.ToInt32(Obj.PlanID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItem = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID).ToList();
                foreach (Database.planmealcustinvoice item in ListItem)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    if (OBJedit.ProductionDate == null)
                    {
                        OBJedit.ProductionDate = DateTime.Now;
                        OBJedit.chiefID = Chiefid;
                        DB.SaveChanges();
                    }
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            if(e.CommandName == "btnQty150")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);                
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItemQty150 = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID).ToList();
                foreach (Database.planmealcustinvoice item in ListItemQty150)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            if(e.CommandName == "btnQty200")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;

                List<Database.planmealcustinvoice> ListItemQty200 = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID).ToList();
                foreach (Database.planmealcustinvoice item in ListItemQty200)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
            if (e.CommandName == "btnQty250")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.temp_AMPMMeal Obj = DB.temp_AMPMMeal.Single(p => p.MyID == ID);
                DateTime ExpDate = Convert.ToDateTime(Obj.ExpectedDelDate);
                int MyprodID = Convert.ToInt32(Obj.Product);
                string Prod = Obj.ProductName;
                List<Database.planmealcustinvoice> ListItemQty250 = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID).ToList();
                foreach (Database.planmealcustinvoice item in ListItemQty250)
                {
                    Database.planmealcustinvoice OBJedit = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.ExpectedDelDate == ExpDate && p.ProdName1 == Prod && p.MYPRODID == MyprodID && p.MYTRANSID == item.MYTRANSID && p.CustomerID == item.CustomerID && p.DeliveryID == item.DeliveryID);
                    OBJedit.ProductionDate = DateTime.Now;
                    OBJedit.chiefID = Chiefid;
                    DB.SaveChanges();
                }
                DateTime FDT = Convert.ToDateTime(txtdateFrom.Text);
                DateTime TDT = Convert.ToDateTime(txtdateTO.Text);
                int FDelTime = Convert.ToInt32(DrpFromDelivery.SelectedValue);
                int TDelTime = Convert.ToInt32(DrpToDelivery.SelectedValue);
                DateT(FDT, TDT, FDelTime, TDelTime);
            }
        }

        protected void chkcom_CheckedChanged(object sender, EventArgs e)
        {
            if(chkcom.Checked == true)
            {
                PNLcombine.Visible = true;
                PNLseprate.Visible = false;
            }
            else
            {
                PNLcombine.Visible = false;
                PNLseprate.Visible = true;
            }
        }
        public string GetSTD(string PDT)
        {
            string SDT = PDT.ToString();
            if (SDT == "1/1/0001")
                return "Pending";
            else
                return "Produced";
        }



    }
}