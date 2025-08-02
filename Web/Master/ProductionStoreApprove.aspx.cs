using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using Classes;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web.Master
{
    public partial class ProductionStoreApprove : System.Web.UI.Page
    {
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        Database.CallEntities DB = new Database.CallEntities();
        DateTime DT = DateTime.Now;
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
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

                FistTimeLoad();
                binndFomat();
                BindList();
                FormRequestConsolidate();
            }
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((AcmMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }
        public void binndFomat()
        {
            DateTime MaxDate = Convert.ToDateTime(DB.View_PlannedMealSum.Where(p => p.TenentID == TID).Max(p => p.ExpectedDelDate));

            string SQOCommad = "select ExpectedDelDate from View_PlannedMealSum where TenentID=" + TID + " and ExpectedDelDate between '" + DT + "' and '" + MaxDate + "' group by ExpectedDelDate order by ExpectedDelDate;";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            Formatedate.DataSource = dt;
            Formatedate.DataTextField = "ExpectedDelDate";
            Formatedate.DataValueField = "ExpectedDelDate";
            Formatedate.DataBind();
            Formatedate.Items.Insert(0, new ListItem("-- Select --", "0"));
            Formatedate.SelectedValue = DT.ToString();
            //Formatedate.SelectedIndex = 1;
            List<Database.TBLLOCATION> Location = new List<Database.TBLLOCATION>();
            List<Database.ICTR_HD> LocationHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store In" && p.transid == 8101 && p.transsubid == 810101 && p.TRANSDATE == DT).ToList();
            foreach (Database.ICTR_HD Litem in LocationHD)
            {
                int LLID = Litem.locationID;
                Database.TBLLOCATION LOBJ = DB.TBLLOCATIONs.Single(p => p.Active == "Y" && p.TenentID == TID && p.LOCATIONID == LLID);
                Location.Add(LOBJ);
            }
            drplocation.DataSource = Location;
            drplocation.DataTextField = "LOCNAME1";
            drplocation.DataValueField = "LOCATIONID";
            drplocation.DataBind();
            if (Location.Count() == 0)
            {
                drplocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Location--", "0"));
            }

        }
        public void BindList()
        {
            pnlStoreiss.Visible = false;
            int RQELocation = Convert.ToInt32(drplocation.SelectedValue);
            if (RQELocation != 0)
            {
                DateTime? FDT = null;//Convert.ToDateTime(Formatedate.SelectedValue);
                if (Formatedate.SelectedValue != "0")
                {
                    FDT = Convert.ToDateTime(Formatedate.SelectedValue);

                    //Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");
                    List<Database.ICIT_BR> BrList = new List<Database.ICIT_BR>();
                    //List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == DT).ToList();
                    List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.locationID == RQELocation && p.TranType == "Store In" && p.transid == 8101 && p.transsubid == 810101 && p.TRANSDATE == FDT).ToList();
                    List<Database.ICTR_DT> newlist = new List<Database.ICTR_DT>();
                    foreach (Database.ICTR_HD HDitem in ListHD)
                    {
                        int MYTRANSIDHD = Convert.ToInt32(HDitem.MYTRANSID);
                        List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDHD && p.EXPIRYDATE == FDT).ToList();
                        foreach (Database.ICTR_DT DTitem in ListDT)
                        {
                            int prodid = Convert.ToInt32(DTitem.MyProdID);
                            int DTUOM = Convert.ToInt32(DTitem.UOM);
                            newlist.Add(DTitem);
                        }
                    }
                    ListICITBR.DataSource = newlist;
                    ListICITBR.DataBind();
                    UpdatePanel7.Update();
                    //Store issue

                    foreach (Database.ICTR_DT ISUItem in newlist)
                    {
                        int BUOM = Convert.ToInt32(ISUItem.UOM);
                        if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == ISUItem.MyProdID && p.UOM == BUOM).Count() == 0)
                        {
                            Database.ICIT_BR BRNOBJ = new Database.ICIT_BR();
                            BRNOBJ.TenentID = TID;
                            BRNOBJ.MyProdID = Convert.ToInt64(ISUItem.MyProdID);
                            BRNOBJ.MySysName = "PUR";
                            BRNOBJ.UOM = BUOM;
                            BrList.Add(BRNOBJ);
                            pnlStoreiss.Visible = true;
                        }
                    }
                    STRIQE.Text = BrList.Count().ToString();
                    StoreIssue.DataSource = BrList;
                    StoreIssue.DataBind();
                }
            }
        }
        public string getprodctname(int PID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == PID && p.TenentID == TID).Count() > 0)
            {
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).ProdName1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getBarcode(int PID)
        {
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).BarCode;
        }
        public string getUomname(int PID)
        {
            if (DB.ICUOMs.Where(p => p.UOM == PID && p.TenentID == TID).Count() > 0)
            {
                return DB.ICUOMs.Single(p => p.UOM == PID && p.TenentID == TID).UOMNAME1;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void drplocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RQELocation = Convert.ToInt32(drplocation.SelectedValue);
            BindList();
            //if (drplocation.SelectedIndex != 0)
            //{
            //    ListICITBR.DataSource = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == location && p.Active == "Y");
            //    ListICITBR.DataBind();
            //}
            //else
            //{
            //    ListICITBR.DataSource = null;
            //    ListICITBR.DataBind();

            //}
        }
        public string getcolorname(int ColorID)
        {
            return DB.TBLCOLORs.Single(p => p.TenentID == TID && p.COLORID == ColorID).COLORDESC1.ToString();
        }

        public string getsizename(int SizeID)
        {
            return DB.TBLSIZEs.Single(p => p.TenentID == TID && p.SIZECODE == SizeID).SIZEDESC1.ToString();
        }

        protected void ListICITBR_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int RQELocation = Convert.ToInt32(drplocation.SelectedValue);
            if (RQELocation != 0)
            {
                Label Label5 = (Label)e.Item.FindControl("Label5");
                Label Label6 = (Label)e.Item.FindControl("Label6");
                decimal onhand = Convert.ToDecimal(Label6.Text);
                decimal RQTY = Convert.ToDecimal(Label5.Text);
                if (e.CommandName == "LinkApprove")
                {
                    string[] ID = e.CommandArgument.ToString().Split('-');
                    int MYTRANSID = Convert.ToInt32(ID[0]);
                    int MYID = Convert.ToInt32(ID[1]);
                    int PID = Convert.ToInt32(ID[2]);
                    string UOM = ID[3].ToString();

                    if (onhand >= RQTY)
                    {
                        Database.ICTR_DT OBJ = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.MyProdID == PID && p.UOM == UOM);

                        OBJ.AMOUNT = Convert.ToDecimal(Label5.Text);
                        OBJ.Stutas = "Approve";
                        DB.SaveChanges();

                        int outPID = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.locationID == RQELocation && p.MYTRANSID == MYTRANSID && p.TranType == "Store In" && p.transid == 8101 && p.transsubid == 810101).LinkTransID);
                        Database.ICTR_DT outOBJ = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == outPID && p.MYID == MYID && p.MyProdID == PID && p.UOM == UOM);
                        outOBJ.AMOUNT = Convert.ToDecimal(Label5.Text);
                        outOBJ.Stutas = "Approve";
                        DB.SaveChanges();
                    }
                    else
                    {
                        pnlSuccessMsg.Visible = true;
                        lblMsg.Text = "Not enough QTY in your stock... ";
                    }
                    BindList();
                }
            }

        }

        protected void ListICITBR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DateTime? FDT = null;//Convert.ToDateTime(Formatedate.SelectedValue);
            if (Formatedate.SelectedValue != "0")
            {
                FDT = Convert.ToDateTime(Formatedate.SelectedValue);
                Label Label14 = (Label)e.Item.FindControl("Label14");
                Label Label20 = (Label)e.Item.FindControl("Label20");
                Label TRANID = (Label)e.Item.FindControl("TRANID");
                Label MY = (Label)e.Item.FindControl("MY");
                LinkButton LinkApprove = (LinkButton)e.Item.FindControl("LinkApprove");
                Label Label2 = (Label)e.Item.FindControl("Label2");

                DropDownList ListPoDrft = (DropDownList)e.Item.FindControl("ListPoDrft");
                TextBox txtfinalFormTOT = (TextBox)e.Item.FindControl("txtfinalFormTOT");

                int pid = Convert.ToInt32(Label14.Text);
                string uom = Label20.Text;
                int BUOM = Convert.ToInt32(uom);
                int mytransid = Convert.ToInt32(TRANID.Text);
                int myid = Convert.ToInt32(MY.Text);

                if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == pid && p.UOM == BUOM).Count() == 1)
                {
                    Database.ICTR_DT OBJ = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == mytransid && p.MYID == myid && p.MyProdID == pid && p.UOM == uom);

                    if (OBJ.Stutas == "Approve" || OBJ.Stutas == "Accepted")
                    {
                        Label2.Text = OBJ.AMOUNT.ToString();
                    }
                    else
                    {
                        LinkApprove.Visible = true;
                    }
                    int APP = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytransid && (p.Stutas == "Approve" || p.Stutas == "Accepted")).Count();
                    if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == mytransid).Count() == APP)
                    {
                        LinkApproveAll.Visible = false;
                    }
                    else
                    {
                        LinkApproveAll.Visible = true;
                    }
                }
                else
                {
                    LinkApprove.Visible = false;
                }

                var ListPO = DB.View_RequestForm.Where(p => p.TenentID == TID).GroupBy(p => p.MYTRANSID).Select(p => p.FirstOrDefault());

                ListPoDrft.DataSource = ListPO;
                ListPoDrft.DataTextField = "COMPNAME1";
                ListPoDrft.DataValueField = "MYTRANSID";
                ListPoDrft.DataBind();

                List<Database.ICTR_HD> MergeFormHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Consolidate form" && p.transid == 9101 && p.transsubid == 910102 && p.TRANSDATE == FDT).ToList();
                if (MergeFormHD.Count() > 0)
                {
                    int MyTransIDDT = Convert.ToInt32(MergeFormHD[0].MYTRANSID);
                    string TOT = DB.ICTR_DT.FirstOrDefault(p => p.TenentID == TID && p.MYTRANSID == MyTransIDDT && p.MyProdID == pid && p.UOM == uom).OVERHEADAMOUNT.ToString();
                    txtfinalFormTOT.Text = TOT;
                }
            }
        }
        public string GetSupp(int ID)
        {
            return DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == ID).COMPNAME1;
        }
        protected void LinkApproveAll_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            int RQELocation = Convert.ToInt32(drplocation.SelectedValue);
            if (RQELocation != 0)
            {
                for (int i = 0; i < ListICITBR.Items.Count; i++)
                {
                    string Pitem = "";
                    Label Label5 = (Label)ListICITBR.Items[i].FindControl("Label5");
                    Label Label6 = (Label)ListICITBR.Items[i].FindControl("Label6");
                    Label TRANID = (Label)ListICITBR.Items[i].FindControl("TRANID");
                    Label MY = (Label)ListICITBR.Items[i].FindControl("MY");
                    Label Label14 = (Label)ListICITBR.Items[i].FindControl("Label14");
                    Label Label20 = (Label)ListICITBR.Items[i].FindControl("Label20");
                    Label lblGRCOHNAME2 = (Label)ListICITBR.Items[i].FindControl("lblGRCOHNAME2");

                    decimal onhand = Convert.ToDecimal(Label6.Text);
                    decimal RQTY = Convert.ToDecimal(Label5.Text);

                    int MYTRANSID = Convert.ToInt32(TRANID.Text);
                    int MYID = Convert.ToInt32(MY.Text);
                    int PID = Convert.ToInt32(Label14.Text);
                    string UOM = Label20.Text.ToString();
                    int BUID = Convert.ToInt32(UOM);
                    if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == BUID).Count() == 1)
                    {
                        if (onhand >= RQTY)
                        {
                            Database.ICTR_DT OBJ = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.MyProdID == PID && p.UOM == UOM);

                            OBJ.AMOUNT = Convert.ToDecimal(Label5.Text);
                            OBJ.Stutas = "Approve";
                            DB.SaveChanges();

                            int outPID = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.locationID == RQELocation && p.MYTRANSID == MYTRANSID && p.TranType == "Store In" && p.transid == 8101 && p.transsubid == 810101).LinkTransID);
                            Database.ICTR_DT outOBJ = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == outPID && p.MYID == MYID && p.MyProdID == PID && p.UOM == UOM);
                            outOBJ.AMOUNT = Convert.ToDecimal(Label5.Text);
                            outOBJ.Stutas = "Approve";
                            DB.SaveChanges();
                        }
                        else
                        {
                            pnlSuccessMsg.Visible = true;
                            lblMsg.Text = "Not enough QTY in your stock for Item " + lblGRCOHNAME2.Text;
                        }
                    }
                    else
                    {
                        Pitem += lblGRCOHNAME2.Text + ",";
                        pnlSuccessMsg.Visible = true;
                        lblMsg.Text = "This Item Is Not Available in your stock " + Pitem;
                    }
                }
            }

        }
        public string getunitcost(int PID, int UOMID)
        {
            if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).Count() > 0)
            {
                return DB.ICIT_BR.Single(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).UnitCost.ToString();

            }
            else
            {
                return "Not Found";
            }
        }
        public string getOnHand(int PID, int UOMID)
        {
            if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).Count() > 0)
            {
                return DB.ICIT_BR.Single(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).OnHand.ToString();

            }
            else
            {
                return "Not Found";
            }
        }
        public string getQTYOUT(int PID, int UOMID)
        {
            if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).Count() > 0)
            {
                return DB.ICIT_BR.Single(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).QtyOut.ToString();

            }
            else
            {
                return "Not Found";
            }
        }
        public string getQTYReceive(int PID, int UOMID)
        {
            if (DB.ICIT_BR.Where(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).Count() > 0)
            {
                return DB.ICIT_BR.Single(p => p.TenentID == TID && p.MyProdID == PID && p.UOM == UOMID).QtyReceived.ToString();

            }
            else
            {
                return "Not Found";
            }
        }

        protected void Formatedate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Formatedate.SelectedValue != "0")
            {
                DateTime FDT = Convert.ToDateTime(Formatedate.SelectedValue);
                BindList(FDT);
            }
            
        }

        public void BindList(DateTime FDT)
        {
            Classes.EcommAdminClass.getdropdown(drplocation, TID, "", "", "", "TBLLOCATION");

            List<Database.ICTR_HD> ListHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Store out" && p.transid == 8101 && p.transsubid == 810102 && p.TRANSDATE == FDT).ToList();
            List<Database.ICTR_DT> newlist = new List<Database.ICTR_DT>();
            foreach (Database.ICTR_HD HDitem in ListHD)
            {
                int MYTRANSIDHD = Convert.ToInt32(HDitem.MYTRANSID);
                List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDHD && p.EXPIRYDATE == FDT).ToList();
                foreach (Database.ICTR_DT DTitem in ListDT)
                {
                    int prodid = Convert.ToInt32(DTitem.MyProdID);
                    int DTUOM = Convert.ToInt32(DTitem.UOM);
                    newlist.Add(DTitem);

                }

            }
            ListICITBR.DataSource = newlist;
            ListICITBR.DataBind();
            UpdatePanel7.Update();
            //ListICITBR.DataSource = DB.ICIT_BR.Where(p => p.TenentID == TID && p.LocationID == LID && p.Active == "Y");
            //ListICITBR.DataBind();
        }
        public void FormRequestConsolidate()
        {
            DateTime FDT = DateTime.Now;
            //DateTime FDT = Convert.ToDateTime(Formatedate.SelectedValue);
            if (Formatedate.SelectedValue != "0")
            {
                FDT = Convert.ToDateTime(Formatedate.SelectedValue);

                List<Database.ICTR_HD> MergeFormHD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Request form" && p.transid == 9101 && p.transsubid == 910101).ToList();
                List<Database.ICTR_DT> MergeFormDT = new List<Database.ICTR_DT>();
                List<Database.ICTR_DT> TempDT = new List<Database.ICTR_DT>();

                foreach (Database.ICTR_HD FormItem in MergeFormHD)
                {
                    int MyTransID = Convert.ToInt32(FormItem.MYTRANSID);
                    MergeFormDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MyTransID && p.Stutas == "Draft").ToList();

                    foreach (Database.ICTR_DT FormDT in MergeFormDT)
                    {
                        if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.transid == 9101 && p.transsubid == 910102 && p.TranType == "Consolidate form" && p.TRANSDATE == FDT).Count() > 0)
                        {
                            int Mytransidfinn = Convert.ToInt32(DB.ICTR_HD.Single(p => p.TenentID == TID && p.transid == 9101 && p.transsubid == 910102 && p.TranType == "Consolidate form" && p.TRANSDATE == FDT).MYTRANSID);
                            if (DB.ICTR_DT.Where(p => p.MYTRANSID == Mytransidfinn && p.TenentID == TID && p.MyProdID == FormDT.MyProdID && p.UOM == FormDT.UOM).Count() < 1)
                            {
                                Database.ICTR_DT ConsolDTobj = FormDT;
                                int MYIDDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == Mytransidfinn).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == Mytransidfinn).Max(p => p.MYID) + 1) : 1;
                                decimal FTotal = Convert.ToDecimal(ConsolDTobj.OVERHEADAMOUNT);
                                int PID = Convert.ToInt32(ConsolDTobj.MyProdID);
                                string UOM = ConsolDTobj.UOM.ToString();
                                decimal Recno = ConsolDTobj.QUANTITY;
                                decimal ReqQTY = Convert.ToDecimal(ConsolDTobj.UNITPRICE);
                                decimal ReceQTY = Convert.ToDecimal(ConsolDTobj.AMOUNT);
                                Classes.EcommAdminClass.insert_ICTR_DT(TID, Mytransidfinn, 1, MYIDDT, PID, "LF", "LF", "0", "IC", 0, 0, 0, "Consolidate Merge QTY", UOM, Recno, ReqQTY, ReceQTY, FTotal, "", 0, "", "", 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", FDT, true, "", 0, 0, "", "Waitting");

                                Database.ICTR_DT Final = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == FormDT.MYTRANSID && p.MYID == FormDT.MYID);
                                Final.Stutas = "Final";
                                DB.SaveChanges();
                            }
                            else
                            {
                                Database.ICTR_DT objICTR_DTfinn = DB.ICTR_DT.Single(p => p.MYTRANSID == Mytransidfinn && p.TenentID == TID && p.MyProdID == FormDT.MyProdID && p.UOM == FormDT.UOM);
                                decimal oldqty = Convert.ToDecimal(objICTR_DTfinn.OVERHEADAMOUNT);
                                decimal newqty = Convert.ToDecimal(FormDT.OVERHEADAMOUNT);
                                decimal final = oldqty + newqty;
                                objICTR_DTfinn.OVERHEADAMOUNT = final;
                                DB.SaveChanges();

                                Database.ICTR_DT Final = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == FormDT.MYTRANSID && p.MYID == FormDT.MYID);
                                Final.Stutas = "Final";
                                DB.SaveChanges();
                            }

                        }
                        else
                        {
                            if (TempDT.Where(p => p.MyProdID == FormDT.MyProdID && p.UOM == FormDT.UOM).Count() < 1)
                            {
                                Database.ICTR_DT objICTR_DT = new Database.ICTR_DT();
                                objICTR_DT.TenentID = FormDT.TenentID;
                                objICTR_DT.MYTRANSID = FormDT.MYTRANSID;
                                objICTR_DT.locationID = FormDT.locationID;
                                objICTR_DT.MYID = FormDT.MYID;
                                objICTR_DT.MyProdID = FormDT.MyProdID;
                                objICTR_DT.REFTYPE = FormDT.REFTYPE;
                                objICTR_DT.REFSUBTYPE = FormDT.REFSUBTYPE;
                                objICTR_DT.PERIOD_CODE = FormDT.PERIOD_CODE;
                                objICTR_DT.MYSYSNAME = FormDT.MYSYSNAME;
                                objICTR_DT.JOID = FormDT.JOID;
                                objICTR_DT.JOBORDERDTMYID = FormDT.JOBORDERDTMYID;
                                objICTR_DT.ACTIVITYID = FormDT.ACTIVITYID;
                                objICTR_DT.DESCRIPTION = FormDT.DESCRIPTION;
                                objICTR_DT.UOM = FormDT.UOM;
                                objICTR_DT.QUANTITY = FormDT.QUANTITY;
                                objICTR_DT.UNITPRICE = FormDT.UNITPRICE;
                                objICTR_DT.AMOUNT = FormDT.AMOUNT;
                                objICTR_DT.OVERHEADAMOUNT = FormDT.OVERHEADAMOUNT;
                                objICTR_DT.BATCHNO = FormDT.BATCHNO;
                                objICTR_DT.BIN_ID = FormDT.BIN_ID;
                                objICTR_DT.BIN_TYPE = FormDT.BIN_TYPE;
                                objICTR_DT.GRNREF = FormDT.GRNREF;
                                objICTR_DT.DISPER = FormDT.DISPER;
                                objICTR_DT.DISAMT = FormDT.DISAMT;
                                objICTR_DT.TAXAMT = FormDT.TAXAMT;
                                objICTR_DT.TAXPER = FormDT.TAXPER;
                                objICTR_DT.PROMOTIONAMT = FormDT.PROMOTIONAMT;
                                objICTR_DT.GLPOST = FormDT.GLPOST;
                                objICTR_DT.GLPOST1 = FormDT.GLPOST1;
                                objICTR_DT.GLPOSTREF1 = FormDT.GLPOSTREF1;
                                objICTR_DT.GLPOSTREF = FormDT.GLPOSTREF;
                                objICTR_DT.ICPOST = FormDT.ICPOST;
                                objICTR_DT.ICPOSTREF = FormDT.ICPOSTREF;
                                objICTR_DT.EXPIRYDATE = FormDT.EXPIRYDATE;
                                objICTR_DT.COMPANYID = FormDT.COMPANYID;
                                objICTR_DT.SWITCH1 = FormDT.SWITCH1;
                                objICTR_DT.ACTIVE = FormDT.ACTIVE;
                                objICTR_DT.DelFlag = FormDT.DelFlag;
                                objICTR_DT.Stutas = FormDT.Stutas;
                                TempDT.Add(objICTR_DT);

                                Database.ICTR_DT Final = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == FormDT.MYTRANSID && p.MYID == FormDT.MYID);
                                Final.Stutas = "Final";
                                DB.SaveChanges();
                            }
                            else
                            {
                                Database.ICTR_DT objICTR_DT = TempDT.Single(p => p.MyProdID == FormDT.MyProdID && p.UOM == FormDT.UOM);
                                decimal oldqty = Convert.ToDecimal(objICTR_DT.OVERHEADAMOUNT);
                                decimal newqty = Convert.ToDecimal(FormDT.OVERHEADAMOUNT);
                                decimal final = oldqty + newqty;

                                TempDT.Where(p => p.MyProdID == FormDT.MyProdID && p.UOM == FormDT.UOM).FirstOrDefault().OVERHEADAMOUNT = final;

                                Database.ICTR_DT Final = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == FormDT.MYTRANSID && p.MYID == FormDT.MYID);
                                Final.Stutas = "Final";
                                DB.SaveChanges();
                            }
                        }

                    }
                }
                if (TempDT.Count() > 0)
                {
                    int MYTRANSIDHD = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                    Classes.EcommAdminClass.insert_ICTR_HD(TID, MYTRANSIDHD, 0, 1, "I", "Consolidate form", 9101, 910102, "IC", 0, 0, "L", "N", "N", 0, "N", 0, 0, 0, "N", "N", "N", 0, FDT, "N", "N", 0, "N", "N", "N", "N", "N", "N", "N", true, 0, DateTime.Now, DateTime.Now, DateTime.Now, "0", 0, "Waitting", LID, "", "", "", 0, "", "", 0, 0, 0);

                    foreach (Database.ICTR_DT itemTDT in TempDT)
                    {
                        Database.ICTR_DT ConsolDTobj = itemTDT;

                        int MYIDDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDHD).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSIDHD).Max(p => p.MYID) + 1) : 1;
                        decimal FTotal = Convert.ToDecimal(ConsolDTobj.OVERHEADAMOUNT);
                        int PID = Convert.ToInt32(ConsolDTobj.MyProdID);
                        string UOM = ConsolDTobj.UOM.ToString();
                        decimal Recno = ConsolDTobj.QUANTITY;
                        decimal ReqQTY = Convert.ToDecimal(ConsolDTobj.UNITPRICE);
                        decimal ReceQTY = Convert.ToDecimal(ConsolDTobj.AMOUNT);
                        Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTRANSIDHD, 1, MYIDDT, PID, "LF", "LF", "0", "IC", 0, 0, 0, "Consolidate Merge QTY", UOM, Recno, ReqQTY, ReceQTY, FTotal, "", 0, "", "", 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", FDT, true, "", 0, 0, "", "Waitting");

                        //Database.ICTR_DT Final = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == itemTDT.MYTRANSID && p.MYID == itemTDT.MYID);
                        //Final.Stutas = "Final";
                        //DB.SaveChanges();
                    }
                    List<Database.ICTR_HD> MergeFormHD1 = DB.ICTR_HD.Where(p => p.TenentID == TID && p.TranType == "Consolidate form" && p.transid == 9101 && p.transsubid == 910102 && p.TRANSDATE == FDT).ToList();
                    if (MergeFormHD.Count() > 0)
                    {
                        BindList();
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime FDT = DateTime.Now;
            if (Formatedate.SelectedValue != "0")
            {
                FDT = Convert.ToDateTime(Formatedate.SelectedValue);
                DropDownList ListPoDrft = (DropDownList)sender;
                ListViewDataItem itemData = (ListViewDataItem)ListPoDrft.NamingContainer;

                DropDownList MItem = (DropDownList)itemData.FindControl("ListPoDrft");
                int MYTID = Convert.ToInt32(MItem.SelectedValue);
                //
                TextBox txtfinalFormTOT = (TextBox)sender;
                ListViewDataItem itemDatat = (ListViewDataItem)txtfinalFormTOT.NamingContainer;

                TextBox OVETOT = (TextBox)itemDatat.FindControl("txtfinalFormTOT");
                int QTY = Convert.ToInt32(OVETOT.Text);
                //
                Label Label14 = (Label)sender;
                ListViewDataItem itemProd = (ListViewDataItem)Label14.NamingContainer;
                Label prodid = (Label)itemDatat.FindControl("Label14");
                int PID = Convert.ToInt32(prodid);
                //
                Label Label20 = (Label)sender;
                ListViewDataItem itemuom = (ListViewDataItem)Label20.NamingContainer;
                Label uomid = (Label)itemuom.FindControl("Label20");
                string uom = uomid.ToString();

                if (DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTID).Count() > 0)
                {
                    if (DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTID && p.MyProdID == PID && p.UOM == uom).Count() > 0)
                    {
                        Database.ICTR_DT Fobj = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTID && p.MyProdID == PID && p.UOM == uom).FirstOrDefault();
                        Fobj.QUANTITY = QTY;
                        DB.SaveChanges();
                    }
                    else
                    {
                        decimal unitPrice = Convert.ToDecimal(0.00);
                        decimal Amount = Convert.ToDecimal(0.00);
                        decimal OverHeadCost = Convert.ToDecimal(0.00);
                        int MYIDDT = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTID).Count() > 0 ? Convert.ToInt32(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTID).Max(p => p.MYID) + 1) : 1;
                        Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTID, 1, MYIDDT, PID, "LF", "LF", "0", "IC", 0, 0, 0, "Consolidate Merge QTY", uom, QTY, unitPrice, Amount, OverHeadCost, "", 0, "", "", 0, 0, 0, 0, 0, 0, "", "", "", "", "", "", FDT, true, "", 0, 0, "", "Waitting");
                    }
                }
            }
        }




    }
}