using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Windows.Forms;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Web.Master
{
    public partial class ReceipeMaster : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        CallEntities DB = new CallEntities();
        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();

        int TID, LID, UID, EMPID, CID = 0;
        string LangID = "";

        public object lb_item = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            SessionLoad();
            if (!IsPostBack)
            {
                bindListBox();
            }
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
        }

        public void bindListBox()
        {

            //string SQOCommad = "select ExpectedDelDate from View_PlannedMealSum where TenentID=" + TID + " and ExpectedDelDate between '" + DT + "' and '" + MaxDate + "' group by ExpectedDelDate order by ExpectedDelDate;";
            //SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            //SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            //DataSet ds = new DataSet();
            //ADB.Fill(ds);
            //DataTable dt = ds.Tables[0];

            //Dateformate.DataSource = dt;
            //Dateformate.DataTextField = "ExpectedDelDate";
            //Dateformate.DataValueField = "ExpectedDelDate";
            //Dateformate.DataBind();
            //Dateformate.Items.Insert(0, new ListItem("-- Select --", "0"));

            string SQOCommad = "select Receipe_English+' - '+Prefix as 'Title' ,CONVERT(VARCHAR(5),recNo) +'-'+ Prefix as 'RecNOPre' from tbl_Receipe where TenentID=" + TID + ";";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            drpReceipe.DataSource = dt;//DB.tbl_Receipe.Where(p => p.TenentID == TID).ToList();
            drpReceipe.DataTextField = "Title";//"Receipe_English";
            drpReceipe.DataValueField = "RecNOPre";//"recNo";
            drpReceipe.DataBind();
            drpReceipe.Items.Insert(0, new ListItem("-- Select --", "0"));

            var Results = (from Rec in DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.RecipeType != null && p.RecipeType != "").OrderBy(p => new { p.RecipeType, p.ProdName1 })
                           select new
                           {
                               RecName = Rec.ProdName1 + " - " + Rec.RecipeType,
                               MYPRODID = Rec.MYPRODID
                           }).ToList();

            ListBox1.DataSource = Results;//DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.RecipeType != null && p.RecipeType != "").OrderBy(p=>p.ProdName1).ToList();
            ListBox1.DataTextField = "RecName"; //"ProdName1";
            ListBox1.DataValueField = "MYPRODID"; //"MYPRODID";
            ListBox1.DataBind();

        }

        public string GetproductName(int PID)
        {
            string ProdName = "";
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
            {
                ProdName = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().ProdName1.ToString();
            }
            return ProdName;

        }

        public string GetUOMName(int UOM)
        {
            string ProdName = "";
            if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOM).Count() > 0)
            {
                ProdName = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOM).FirstOrDefault().UOMNAME1.ToString();
            }

            return ProdName;

        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                List<Database.TBLPRODUCT> ListProd = new List<Database.TBLPRODUCT>();
                int ItemCount = Listview1.Items.Count;

                for (int i = 0; i < ItemCount; i++)
                {
                    System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview1.Items[i].FindControl("lblProdID");
                    DropDownList drpUOM = (DropDownList)Listview1.Items[i].FindControl("drpUOM");

                    int PID = Convert.ToInt32(lblProdID.Text);
                    if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                    {
                        Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();
                        ListProd.Add(OBJ);

                        int UOM = Convert.ToInt32(drpUOM.SelectedValue);

                        ListProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().UOM = UOM;
                    }

                }

                int DelPID = Convert.ToInt32(e.CommandArgument);

                if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == DelPID).Count() > 0)
                {
                    Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == DelPID).FirstOrDefault();
                    ListProd.Remove(OBJ);
                }

                Listview1.DataSource = ListProd;
                Listview1.DataBind();

            }
        }

        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                List<Database.TBLPRODUCT> ListProd = new List<Database.TBLPRODUCT>();
                int ItemCount = Listview2.Items.Count;

                for (int i = 0; i < ItemCount; i++)
                {
                    System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview2.Items[i].FindControl("lblProdID");
                    DropDownList drpUOM = (DropDownList)Listview2.Items[i].FindControl("drpUOM");

                    int PID = Convert.ToInt32(lblProdID.Text);
                    if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                    {
                        Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();
                        ListProd.Add(OBJ);

                        int UOM = Convert.ToInt32(drpUOM.SelectedValue);
                        ListProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().UOM = UOM;
                    }

                }

                int DelPID = Convert.ToInt32(e.CommandArgument);

                if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == DelPID).Count() > 0)
                {
                    Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == DelPID).FirstOrDefault();
                    ListProd.Remove(OBJ);
                }

                Listview2.DataSource = ListProd;
                Listview2.DataBind();



            }
        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList drpUOM = (DropDownList)e.Item.FindControl("drpUOM");
            System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblProdID");
            System.Web.UI.WebControls.Label lblUOM = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblUOM");

            int PID = Convert.ToInt32(lblProdID.Text);

            List<Database.ICUOM> ListTemp = new List<Database.ICUOM>();

            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
            {
                int UOMPROD = Convert.ToInt32(DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().UOM);

                if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOMPROD).Count() > 0)
                {
                    Database.ICUOM OnjUON = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOMPROD).FirstOrDefault();
                    ListTemp.Add(OnjUON);
                }
            }

            List<Database.Tbl_Multi_Color_Size_Mst> listmultics = DB.Tbl_Multi_Color_Size_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == PID && p.RecordType == "MultiUOM").ToList();

            foreach (Database.Tbl_Multi_Color_Size_Mst itemcs in listmultics)
            {
                Database.ICUOM listuom123 = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == itemcs.RecTypeID);

                ListTemp.Add(listuom123);
            }

            if (ListTemp.Count() > 0)
            {
                drpUOM.DataSource = ListTemp;
                drpUOM.DataTextField = "UOMNAME1";
                drpUOM.DataValueField = "UOM";
                drpUOM.DataBind();
            }

            drpUOM.SelectedValue = lblUOM.Text;

        }

        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList drpUOM = (DropDownList)e.Item.FindControl("drpUOM");
            System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblProdID");

            int PID = Convert.ToInt32(lblProdID.Text);

            List<Database.ICUOM> ListTemp = new List<Database.ICUOM>();

            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
            {
                int UOMPROD = Convert.ToInt32(DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().UOM);

                if (DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOMPROD).Count() > 0)
                {
                    Database.ICUOM OnjUON = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UOMPROD).FirstOrDefault();
                    ListTemp.Add(OnjUON);
                }
            }

            List<Database.Tbl_Multi_Color_Size_Mst> listmultics = DB.Tbl_Multi_Color_Size_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == PID && p.RecordType == "MultiUOM").ToList();

            foreach (Database.Tbl_Multi_Color_Size_Mst itemcs in listmultics)
            {
                Database.ICUOM listuom123 = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == itemcs.RecTypeID);

                ListTemp.Add(listuom123);
            }

            if (ListTemp.Count() > 0)
            {
                drpUOM.DataSource = ListTemp;
                drpUOM.DataTextField = "UOMNAME1";
                drpUOM.DataValueField = "UOM";
                drpUOM.DataBind();
            }
        }

        public List<Database.TBLPRODUCT> GetInputList()
        {
            List<Database.TBLPRODUCT> ListProd = new List<Database.TBLPRODUCT>();
            int ItemCount = Listview1.Items.Count;

            for (int i = 0; i < ItemCount; i++)
            {
                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview1.Items[i].FindControl("lblProdID");
                DropDownList drpUOM = (DropDownList)Listview1.Items[i].FindControl("drpUOM");

                int PID = Convert.ToInt32(lblProdID.Text);
                if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                {
                    Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();
                    ListProd.Add(OBJ);

                    int UOM = Convert.ToInt32(drpUOM.SelectedValue);

                    ListProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().UOM = UOM;
                }
            }

            return ListProd;
        }

        public List<Database.TBLPRODUCT> GetoutputList()
        {
            List<Database.TBLPRODUCT> ListProd = new List<Database.TBLPRODUCT>();
            int ItemCount = Listview2.Items.Count;

            for (int i = 0; i < ItemCount; i++)
            {
                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview2.Items[i].FindControl("lblProdID");

                DropDownList drpUOM = (DropDownList)Listview2.Items[i].FindControl("drpUOM");

                int PID = Convert.ToInt32(lblProdID.Text);
                if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                {
                    Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();
                    ListProd.Add(OBJ);

                    int UOM = Convert.ToInt32(drpUOM.SelectedValue);

                    ListProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault().UOM = UOM;

                }

            }

            return ListProd;
        }

        protected void lnkInput_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex >= 0)
            {
                List<Database.TBLPRODUCT> ListProd = GetInputList();

                for (int i = 0; i < ListBox1.Items.Count; i++)
                {
                    if (ListBox1.Items[i].Selected)
                    {
                        int PID = Convert.ToInt32(ListBox1.Items[i].Value);

                        if (ListProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() < 1)
                        {
                            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                            {
                                Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();

                                ListProd.Add(OBJ);
                            }
                        }
                    }
                }

                Listview1.DataSource = ListProd;
                Listview1.DataBind();


            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool Falginput = CheckInputvalidation();
            if (Falginput == false)
            {
                return;
            }

            bool Falgoutput = CheckOutputvalidation();
            if (Falgoutput == false)
            {
                return;
            }
            string[] FindPref = drpReceipe.SelectedValue.Split('-');
            int RecNO = Convert.ToInt32(FindPref[0]);
            string PRIFIX = FindPref[1].ToString().Trim();
            //int RecNO = Convert.ToInt32(drpReceipe.SelectedValue);
           
            
            List<Database.Receipe_Menegement> ListInput = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNO && p.IOSwitch == "Input" && p.Prefix == PRIFIX).ToList();

            foreach (Database.Receipe_Menegement items in ListInput)
            {
                DB.Receipe_Menegement.DeleteObject(items);
                DB.SaveChanges();
            }

            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                string IOSwitch = "Input";

                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview1.Items[i].FindControl("lblProdID");
                DropDownList drpUOM = (DropDownList)Listview1.Items[i].FindControl("drpUOM");

                System.Web.UI.WebControls.TextBox txtQTY = (System.Web.UI.WebControls.TextBox)Listview1.Items[i].FindControl("txtQTY");

                int ProdID = Convert.ToInt32(lblProdID.Text);
                int UOM = Convert.ToInt32(drpUOM.SelectedValue);
                string UOMStr = UOM.ToString();

                decimal Qty = Convert.ToDecimal(txtQTY.Text);

                if (DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNO && p.IOSwitch == IOSwitch && p.ItemCode == ProdID && p.UOM == UOMStr && p.Prefix == PRIFIX).Count() < 1)
                {
                    Database.Receipe_Menegement ObjRecipe = new Database.Receipe_Menegement();
                    ObjRecipe.TenentID = TID;
                    ObjRecipe.recNo = RecNO;
                    ObjRecipe.Prefix = PRIFIX;
                    ObjRecipe.IOSwitch = IOSwitch;
                    ObjRecipe.ItemCode = ProdID;
                    ObjRecipe.UOM = UOM.ToString();
                    ObjRecipe.RecType = DB.tbl_Receipe.Where(p => p.TenentID == TID && p.recNo == RecNO).Count() == 1 ? DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == RecNO).RecType : "Receipe";
                    ObjRecipe.Qty = Qty;
                    ObjRecipe.msrp = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == ProdID).Count() == 1 ? DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ProdID).msrp : 0;
                    ObjRecipe.CostPrice = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == ProdID).Count() == 1 ? DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ProdID).basecost : 0;
                    ObjRecipe.Perc = 0;
                    ObjRecipe.UploadDate = DateTime.Now;
                    ObjRecipe.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    ObjRecipe.SynID = 1;

                    DB.Receipe_Menegement.AddObject(ObjRecipe);
                    DB.SaveChanges();
                }
                else
                {
                    Database.Receipe_Menegement ObjRecipe = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNO && p.IOSwitch == IOSwitch && p.ItemCode == ProdID && p.UOM == UOMStr && p.Prefix == PRIFIX).FirstOrDefault();

                    ObjRecipe.Qty = Qty;

                    ObjRecipe.UploadDate = DateTime.Now;
                    ObjRecipe.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    ObjRecipe.SynID = 1;

                    DB.SaveChanges();

                }
            }

            List<Database.Receipe_Menegement> ListOutput = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNO && p.IOSwitch == "Output" && p.Prefix == PRIFIX).ToList();

            foreach (Database.Receipe_Menegement items in ListOutput)
            {
                DB.Receipe_Menegement.DeleteObject(items);
                DB.SaveChanges();
            }

            for (int i = 0; i < Listview2.Items.Count; i++)
            {
                string IOSwitch = "Output";

                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview2.Items[i].FindControl("lblProdID");
                DropDownList drpUOM = (DropDownList)Listview2.Items[i].FindControl("drpUOM");

                System.Web.UI.WebControls.TextBox txtQTY = (System.Web.UI.WebControls.TextBox)Listview2.Items[i].FindControl("txtQTY");

                int ProdID = Convert.ToInt32(lblProdID.Text);
                int UOM = Convert.ToInt32(drpUOM.SelectedValue);
                string UOMStr = UOM.ToString();

                decimal Qty = Convert.ToDecimal(txtQTY.Text);

                if (DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNO && p.IOSwitch == IOSwitch && p.ItemCode == ProdID && p.UOM == UOMStr && p.Prefix == PRIFIX).Count() < 1)
                {
                    Database.Receipe_Menegement ObjRecipe = new Database.Receipe_Menegement();
                    ObjRecipe.TenentID = TID;
                    ObjRecipe.recNo = RecNO;
                    ObjRecipe.Prefix = PRIFIX;
                    ObjRecipe.IOSwitch = IOSwitch;
                    ObjRecipe.ItemCode = ProdID;
                    ObjRecipe.UOM = UOM.ToString();
                    ObjRecipe.RecType = DB.tbl_Receipe.Where(p => p.TenentID == TID && p.recNo == RecNO).Count() == 1 ? DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == RecNO).RecType : "Receipe";
                    ObjRecipe.Qty = Qty;
                    ObjRecipe.msrp = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == ProdID).Count() == 1 ? DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ProdID).msrp : 0;
                    ObjRecipe.CostPrice = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == ProdID).Count() == 1 ? DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ProdID).basecost : 0;
                    ObjRecipe.Perc = 0;
                    ObjRecipe.UploadDate = DateTime.Now;
                    ObjRecipe.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    ObjRecipe.SynID = 1;

                    DB.Receipe_Menegement.AddObject(ObjRecipe);
                    DB.SaveChanges();
                }
                else
                {
                    Database.Receipe_Menegement ObjRecipe = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RecNO && p.IOSwitch == IOSwitch && p.ItemCode == ProdID && p.UOM == UOMStr && p.Prefix == PRIFIX).FirstOrDefault();

                    ObjRecipe.Qty = Qty;

                    ObjRecipe.UploadDate = DateTime.Now;
                    ObjRecipe.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    ObjRecipe.SynID = 1;

                    DB.SaveChanges();

                }
            }
            //string msg = "Receipe save sucessfully...";
            //string Function = "openModalsmall2('" + msg + "');";

            ScriptManager.RegisterClientScriptBlock((sender as System.Web.UI.Control), this.GetType(), "alert", "openModalsmall2();", true);
        }

        protected void drpReceipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReceipe.SelectedValue != "0")
            {

                string[] FindPref = drpReceipe.SelectedValue.Split('-');
                int recNo = Convert.ToInt32(FindPref[0]);
                string PRIFIX = FindPref[1].ToString().Trim();

                //int recNo = Convert.ToInt32(drpReceipe.SelectedValue);              
                List<Database.TBLPRODUCT> ListInputProd = new List<Database.TBLPRODUCT>();

                List<Database.Receipe_Menegement> ListInput = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == recNo && p.IOSwitch == "Input" && p.Prefix == PRIFIX).ToList();
                foreach (Database.Receipe_Menegement itemInput in ListInput)
                {
                    int PID = itemInput.ItemCode;

                    if (ListInputProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() < 1)
                    {
                        if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                        {
                            Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();

                            ListInputProd.Add(OBJ);
                        }
                    }
                }

                if (ListInputProd.Count() > 0)
                {
                    Listview1.DataSource = ListInputProd;
                    Listview1.DataBind();

                    for (int i = 0; i < Listview1.Items.Count; i++)
                    {
                        System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview1.Items[i].FindControl("lblProdID");
                        DropDownList drpUOM = (DropDownList)Listview1.Items[i].FindControl("drpUOM");

                        System.Web.UI.WebControls.TextBox txtQTY = (System.Web.UI.WebControls.TextBox)Listview1.Items[i].FindControl("txtQTY");

                        int PID = Convert.ToInt32(lblProdID.Text);

                        string UOMSri = ListInput.Where(p => p.TenentID == TID && p.recNo == recNo && p.ItemCode == PID).FirstOrDefault().UOM;

                        drpUOM.SelectedValue = UOMSri;

                        txtQTY.Text = ListInput.Where(p => p.TenentID == TID && p.recNo == recNo && p.ItemCode == PID).FirstOrDefault().Qty.ToString();

                    }
                }
                else
                {
                    Listview1.DataSource = ListInputProd;
                    Listview1.DataBind();
                }


                List<Database.TBLPRODUCT> ListoutputProd = new List<Database.TBLPRODUCT>();

                List<Database.Receipe_Menegement> ListOutput = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == recNo && p.IOSwitch == "Output" && p.Prefix == PRIFIX).ToList();

                foreach (Database.Receipe_Menegement itemOutput in ListOutput)
                {
                    int PID = itemOutput.ItemCode;

                    if (ListInputProd.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() < 1)
                    {
                        if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).Count() > 0)
                        {
                            Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).FirstOrDefault();

                            ListoutputProd.Add(OBJ);
                        }
                    }
                }

                if (ListoutputProd.Count() > 0)
                {
                    Listview2.DataSource = ListoutputProd;
                    Listview2.DataBind();

                    for (int i = 0; i < Listview2.Items.Count; i++)
                    {
                        System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview2.Items[i].FindControl("lblProdID");
                        DropDownList drpUOM = (DropDownList)Listview2.Items[i].FindControl("drpUOM");
                        System.Web.UI.WebControls.TextBox txtQTY = (System.Web.UI.WebControls.TextBox)Listview2.Items[i].FindControl("txtQTY");

                        int PID = Convert.ToInt32(lblProdID.Text);
                        string UOMSri = ListOutput.Where(p => p.TenentID == TID && p.recNo == recNo && p.ItemCode == PID).FirstOrDefault().UOM;

                        drpUOM.SelectedValue = UOMSri;

                        txtQTY.Text = ListOutput.Where(p => p.TenentID == TID && p.recNo == recNo && p.ItemCode == PID).FirstOrDefault().Qty.ToString();

                    }

                }
                else
                {
                    Listview2.DataSource = ListoutputProd;
                    Listview2.DataBind();
                }



            }
        }

        protected void drpUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpUOM = (DropDownList)sender;
            ListViewDataItem item = (ListViewDataItem)drpUOM.NamingContainer;

            System.Web.UI.WebControls.Label lblUOM = (System.Web.UI.WebControls.Label)item.FindControl("lblUOM");

            lblUOM.Text = drpUOM.SelectedValue;


        }


        public bool CheckInputvalidation()
        {
            int ItemCount = Listview1.Items.Count;

            if (ItemCount < 1)
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Add Atleast One Item In Input Item ";
                return false;
            }

            string MSg = "";

            for (int i = 0; i < ItemCount; i++)
            {
                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview1.Items[i].FindControl("lblProdID");
                DropDownList drpUOM = (DropDownList)Listview1.Items[i].FindControl("drpUOM");

                System.Web.UI.WebControls.TextBox txtQTY = (System.Web.UI.WebControls.TextBox)Listview1.Items[i].FindControl("txtQTY");

                int PID = Convert.ToInt32(lblProdID.Text);
                int UOM = Convert.ToInt32(drpUOM.SelectedValue);

                try
                {
                    decimal Qty = Convert.ToDecimal(txtQTY.Text);
                }
                catch
                {
                    string ProdName = GetproductName(PID);
                    string UOMName = GetUOMName(UOM);

                    MSg = MSg + " Please Correct The Qty in Product <b><u>" + ProdName + " </b></u> and UOM <b><u>" + UOMName + "</b></u> in Input Item <br>";

                }
            }

            if (MSg != "")
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = MSg;
                return false;
            }

            return true;
        }



        protected void lnkOutput_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex >= 0)
            {
                List<Database.TBLPRODUCT> ListProd = GetoutputList();

                for (int i = 0; i < ListBox1.Items.Count; i++)
                {
                    if (ListBox1.Items[i].Selected)
                    {
                        int PID = Convert.ToInt32(ListBox1.Items[i].Value);

                        if (ListProd.Where(p => p.TenentID == TID && p.MYPRODID == PID && p.RecipeType == "Output").Count() < 1)
                        {
                            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID && p.RecipeType == "Output").Count() > 0)
                            {
                                Database.TBLPRODUCT OBJ = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID && p.RecipeType == "Output").FirstOrDefault();
                                ListProd.Add(OBJ);
                            }
                        }

                    }
                }

                Listview2.DataSource = ListProd;
                Listview2.DataBind();


            }
        }

        public bool CheckOutputvalidation()
        {
            int ItemCount = Listview2.Items.Count;

            if (ItemCount < 1)
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Add Atleast One Item In Output Item ";
                return false;
            }

            string MSg = "";

            for (int i = 0; i < ItemCount; i++)
            {
                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview2.Items[i].FindControl("lblProdID");
                DropDownList drpUOM = (DropDownList)Listview2.Items[i].FindControl("drpUOM");

                System.Web.UI.WebControls.TextBox txtQTY = (System.Web.UI.WebControls.TextBox)Listview2.Items[i].FindControl("txtQTY");

                int PID = Convert.ToInt32(lblProdID.Text);
                int UOM = Convert.ToInt32(drpUOM.SelectedValue);

                try
                {
                    decimal Qty = Convert.ToDecimal(txtQTY.Text);
                }
                catch
                {
                    string ProdName = GetproductName(PID);
                    string UOMName = GetUOMName(UOM);
                    MSg = MSg + " Please Correct The Qty in Product <b><u>" + ProdName + " </b></u> and UOM <b><u>" + UOMName + "</b></u> in Output Item <br>";
                }

                if (MSg != "")
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = MSg;
                    return false;
                }


                bool Falge = CheckInputExist(PID, UOM);
                if (Falge == false)
                {
                    return false;
                }

            }

            return true;
        }

        public bool CheckInputExist(int PIDOUTput, int UOMoutput)
        {
            int ItemCount = Listview1.Items.Count;

            string Msg = "";

            for (int i = 0; i < ItemCount; i++)
            {
                System.Web.UI.WebControls.Label lblProdID = (System.Web.UI.WebControls.Label)Listview1.Items[i].FindControl("lblProdID");
                DropDownList drpUOM = (DropDownList)Listview1.Items[i].FindControl("drpUOM");

                int PID = Convert.ToInt32(lblProdID.Text);
                int UOM = Convert.ToInt32(drpUOM.SelectedValue);

                if (PID == PIDOUTput && UOM == UOMoutput)
                {
                    string ProdName = GetproductName(PID);
                    string UOMName = GetUOMName(UOM);

                    Msg = Msg + "Product  <b><U> " + ProdName + " </b></U> and UOM   <b><U> " + UOMName + "  </b></U>  already Found in Input SO Item Not Allow to Add Output Item <BR> ";

                }
            }

            if (Msg != "")
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = Msg;
                return false;
            }

            return true;
        }



    }
}