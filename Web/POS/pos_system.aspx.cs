using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web.Configuration;
using AjaxControlToolkit;
using System.Transactions;
using System.IO;
namespace Web.Sales
{
    public partial class pos_system : System.Web.UI.Page
    {
        int TID, LID, UID, EMPID, Transid, Transsubid, stMYTRANSID, MYTRANSID, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        CallEntities DB = new CallEntities();
        List<Database.ICTRPayTerms_HD> TempICTRPayTerms_HD = new List<Database.ICTRPayTerms_HD>();
        PropertyFile objProFile = new PropertyFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            SessionLoad();
            if (!IsPostBack)
            {
                FillControl();
                BindData(0);

                Database.ICTRPayTerms_HD objEco_ICCATEGORY = new Database.ICTRPayTerms_HD();
                TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
                ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
                Repeater3.DataSource = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
                Repeater3.DataBind();
            }
        }

        public void SessionLoad()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();

            if (Request.QueryString["transid"] != null && Request.QueryString["transsubid"] != null)
            {
                Transid = Convert.ToInt32(Request.QueryString["transid"]);
                Transsubid = Convert.ToInt32(Request.QueryString["transsubid"]);
                Session["Transid"] = Transid + "," + Transsubid;
            }

        }

        public void FillControl()
        {
            //drpCategory.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.PARENT_CATID == 0 && p.Active == "1");
            //drpCategory.DataTextField = "CAT_NAME1";
            //drpCategory.DataValueField = "CATID";
            //drpCategory.DataBind();
            //drpCategory.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpsubCategory.Items.Insert(0, new ListItem("-- Select --", "0"));

            SqlCommand cmd = new SqlCommand("select * from CAT_MST where TenentID=" + TID + " and CATID in (select MainCategoryID from TBLPRODUCT where TenentID=" + TID + ")", cn);//+ " and  MyProdID in (select MyProdID from ICIT_BR where TenentID=" + TID + " and onhand > 0)"
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            cn.Close();

            ListCategory.DataSource = dt;// DB.CAT_MST.Where(p => p.TenentID == TID && p.PARENT_CATID == 0 && p.Active == "1");
            ListCategory.DataBind();

            List<Database.REFTABLE> ListRaf = DB.REFTABLEs.Where(p => p.TenentID == TID).ToList();

            drpInvoiceType.DataSource = ListRaf.Where(p => p.REFTYPE == "Invoice" && p.REFSUBTYPE == "Type").OrderBy(p => p.REFNAME1);
            drpInvoiceType.DataTextField = "REFNAME1";
            drpInvoiceType.DataValueField = "REFID";
            drpInvoiceType.DataBind();
            drpInvoiceType.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpInvoiceScurce.DataSource = ListRaf.Where(p => p.REFTYPE == "Invoice" && p.REFSUBTYPE == "Source").OrderBy(p => p.REFNAME1);
            drpInvoiceScurce.DataTextField = "REFNAME1";
            drpInvoiceScurce.DataValueField = "REFID";
            drpInvoiceScurce.DataBind();
            drpInvoiceScurce.Items.Insert(0, new ListItem("-- Select --", "0"));

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCounrty(string prefixText, int count)
        {
            int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
            string conStr;
            conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
            string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
            //string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and BUYER = 'true' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@COMPNAME", prefixText);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> custList = new List<string>();
            string custItem = string.Empty;
            while (dr.Read())
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
                custList.Add(custItem);
            }
            conn.Close();
            dr.Close();
            return custList.ToArray();
        }

        protected void BindData(long category)
        {

            //List<Database.ICIT_BR> ListPRoduct = DB.ICIT_BR.Where(p => p.TenentID == TID && p.OnHand > 0).ToList();

            //SqlCommand cmd = new SqlCommand("select * from TBLPRODUCT where TenentID=" + TID + " and MainCategoryID=" + category + " and MyProdID in (select MyProdID from ICIT_BR where TenentID=" + TID + " and onhand > 0)", cn);
            SqlCommand cmd;
            if (category == 0)
            {
                cmd = new SqlCommand("select * from TBLPRODUCT where TenentID=" + TID + " and ACTIVE = '1'", cn);//+ " and  MyProdID in (select MyProdID from ICIT_BR where TenentID=" + TID + " and onhand > 0)"
            }
            else
            {
                cmd = new SqlCommand("select * from TBLPRODUCT where TenentID=" + TID + " and MainCategoryID=" + category + " and ACTIVE = '1'", cn);//+ " and  MyProdID in (select MyProdID from ICIT_BR where TenentID=" + TID + " and onhand > 0)"
            }


            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cn.Close();
            Listview2.DataSource = dt;
            Listview2.DataBind();


        }

        protected void btnBarcode_Click(object sender, EventArgs e)
        {
            string id1 = txtBarCode.Text.Trim().ToString();
            //id1.ToCharArray[0];
            //id1.Substring(0, 1);
            id1 = id1.TrimStart('0');
            var list1 = DB.View_Product_fetch.Where(p => (p.UserProdID.ToUpper().Contains(id1.ToUpper()) || p.BarCode.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode1.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode2.ToUpper().Contains(id1.ToUpper()) || p.ShortName.ToUpper().Contains(id1.ToUpper()) || p.ProdName2.ToUpper().Contains(id1.ToUpper()) || p.ProdName3.ToUpper().Contains(id1.ToUpper()) || p.keywords.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.DescToprint.ToUpper().Contains(id1.ToUpper()) || p.ProdName1.ToUpper().Contains(id1.ToUpper())) && p.ACTIVE == "1" && p.TenentID == TID).OrderBy(p => p.MYPRODID).ToList();
            Listview2.DataSource = list1;
            Listview2.DataBind();
        }
        //protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (drpCategory.SelectedValue != "0")
        //    {
        //        int PARENT_CATID = Convert.ToInt32(drpCategory.SelectedValue);
        //        drpsubCategory.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.PARENT_CATID == PARENT_CATID && p.Active == "1");
        //        drpsubCategory.DataTextField = "CAT_NAME1";
        //        drpsubCategory.DataValueField = "CATID";
        //        drpsubCategory.DataBind();
        //        drpsubCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
        //    }
        //}

        public string getprodnamesub(int SID)
        {
            List<Database.TBLPRODUCT> Listprod = DB.TBLPRODUCTs.Where(p => p.MYPRODID == SID && p.TenentID == TID).ToList();
            if (Listprod.Count() > 0)
            {
                string ProductCode = Listprod.Single(p => p.MYPRODID == SID && p.TenentID == TID).ProdName1;
                int lenth = ProductCode.Length;
                if (lenth > 60)
                {
                    ProductCode = ProductCode.Substring(0, 60);
                }
                return ProductCode;
            }
            else
            {
                return "Record Not Found";
            }
        }

        public string getprodname(int SID)
        {
            List<Database.TBLPRODUCT> Listprod = DB.TBLPRODUCTs.Where(p => p.MYPRODID == SID && p.TenentID == TID).ToList();
            if (Listprod.Count() > 0)
            {
                string ProductCode = Listprod.Single(p => p.MYPRODID == SID && p.TenentID == TID).ProdName1;
                return ProductCode;
            }
            else
            {
                return "Record Not Found";
            }
        }

        public string getimage(int MYPRODID)
        {
            List<Database.ImageTable> ListProduct = DB.ImageTables.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID).ToList();
            if (ListProduct.Count() > 0)
            {
                string img = ListProduct.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID).FirstOrDefault().PICTURE.ToString();
                string path = Server.MapPath("~/ECOMM/Upload/") + img;
                bool falge = File.Exists(path);
                if (falge == true)
                {
                    return "../ECOMM/Upload/" + img;
                }
                else
                {
                    return "../ECOMM/Upload/defolt.png";
                }

            }
            else
            {
                return "../ECOMM/Upload/defolt.png";
            }
        }

        public string getprice(int MYPRODID, int UOM)
        {
            List<Database.ICITEMS_PRICE> ListPrice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID && p.UOM == UOM).ToList();
            if (ListPrice.Count() > 0)
            {
                return ListPrice.Single(p => p.TenentID == TID && p.MYPRODID == MYPRODID && p.UOM == UOM).msrp.ToString();
            }
            else
            {
                return "0.00";
            }
        }
        public string getUom(int UOM)
        {
            List<Database.ICUOM> Listuom = DB.ICUOMs.Where(p => p.UOM == UOM && p.TenentID == TID).ToList();
            if (Listuom.Count() > 0)
            {
                string UomName = Listuom.Single(p => p.UOM == UOM && p.TenentID == TID).UOMNAME1;
                return UomName;
            }
            else
            {
                return "Record Not Found";
            }
        }

        protected void LnkProdAdd_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            if (Session["GetMYTRANSID"] == null)
            {
                panelMsg.Visible = true;
                lblErreorMsg.Text = "Place Add New Invoice";
                return;
            }
            LinkButton btn = (LinkButton)sender;
            ListViewDataItem item = (ListViewDataItem)btn.NamingContainer;
            Label lblProdid = (Label)item.FindControl("lblProdid");
            Label lblPROUOM = (Label)item.FindControl("lblPROUOM");
            ModalPopupExtender ModalPopupExtender10 = (ModalPopupExtender)item.FindControl("ModalPopupExtender10");

            int ID = Convert.ToInt32(lblProdid.Text);
            int UOM = Convert.ToInt32(lblPROUOM.Text);
            int MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);

            List<Database.ICITEMS_PRICE> ListPrice1 = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == ID).ToList();

            if (ListPrice1.Count() > 1)
            {
                ModalPopupExtender10.Show();
                return;
            }
            else
            {
                Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == ID);

                List<posinvoice> ListInvoice = new List<posinvoice>();

                if (ViewState["ListInvoice"] != null)
                {
                    ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];
                }

                if (ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == ID && p.UOM == UOM).Count() < 1)
                {
                    posinvoice Objinvoice = new posinvoice();

                    Objinvoice.TenentID = TID;
                    Objinvoice.MYTRANSID = MYTRANSID;
                    Objinvoice.MYID = ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.MYID) + 1 : 1;
                    Objinvoice.MyProdID = ID;
                    Objinvoice.QUANTITY = 1;
                    Objinvoice.UOM = Convert.ToInt32(obj.UOM);
                    List<Database.ICITEMS_PRICE> ListPrice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == ID && p.UOM == obj.UOM).ToList();
                    if (ListPrice.Count() > 0)
                    {
                        Objinvoice.price = Convert.ToDecimal(ListPrice.Single(p => p.TenentID == TID && p.MYPRODID == ID && p.UOM == obj.UOM).msrp);
                    }
                    else
                    {
                        Objinvoice.price = Convert.ToDecimal(obj.price);
                    }

                    Objinvoice.TotalPrice = Convert.ToDecimal((obj.price) * 1);
                    Objinvoice.AMOUNT = Convert.ToDecimal(obj.price);
                    Objinvoice.Discount = "0";
                    Objinvoice.discAmount = 0;
                    ListInvoice.Add(Objinvoice);

                    ViewState["ListInvoice"] = ListInvoice;

                    Listview1.DataSource = ListInvoice;
                    Listview1.DataBind();
                    UpdatePanel4.Update();
                    ClearItemdetail();
                }
            }

        }
        protected void Listview1_DataBinding(object sender, EventArgs e)
        {
            List<posinvoice> ListInvoice = new List<posinvoice>();
            if (ViewState["ListInvoice"] != null)
            {
                ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];

                decimal TotalPrice = ListInvoice.Sum(p => p.TotalPrice);
                decimal TotalAMOUNT = ListInvoice.Sum(p => p.AMOUNT);
                txtInvoiceAmt.Text = ListInvoice.Sum(p => p.AMOUNT).ToString("N3");

                lblfQty.Text = ListInvoice.Sum(p => p.QUANTITY).ToString();
                lblFPrice.Text = TotalPrice.ToString("N3");
                lblFdisc.Text = (TotalPrice - TotalAMOUNT).ToString("N3");
                lblFamount.Text = ListInvoice.Sum(p => p.AMOUNT).ToString("N3");
                for (int i = 0; i < Repeater3.Items.Count; i++)
                {
                    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    txtammunt.Text = ListInvoice.Sum(p => p.AMOUNT).ToString("N3");
                }
            }

        }

        protected void ListCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DisplayCat")
            {
                long Category = Convert.ToInt32(e.CommandArgument);
                BindData(Category);
            }
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDisp")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int MYTRANSID = Convert.ToInt32(ID[0]);
                int MYID = Convert.ToInt32(ID[1]);
                int MyProdID = Convert.ToInt32(ID[2]);
                List<posinvoice> ListInvoice = new List<posinvoice>();

                if (ViewState["ListInvoice"] != null)
                {
                    ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];

                    posinvoice Objinvoice = ListInvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.MyProdID == MyProdID);

                    txtQty.Text = Objinvoice.QUANTITY.ToString();
                    txtDiscount.Text = Objinvoice.Discount.ToString();
                    txtAmt.Text = Objinvoice.discAmount.ToString();
                    lblPrice.Value = Objinvoice.price.ToString();
                    txtAmount.Text = Objinvoice.AMOUNT.ToString();
                    TotalPriceui();

                    ViewState["prod"] = e.CommandArgument.ToString();

                }

                //UpdatePanel2.Update();
            }

            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                List<posinvoice> ListInvoice = new List<posinvoice>();

                if (ViewState["ListInvoice"] != null)
                {
                    ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];

                    posinvoice Objinvoice = ListInvoice.Single(p => p.MyProdID == ID);

                    txtQty.Text = Objinvoice.QUANTITY.ToString();
                    txtDiscount.Text = Objinvoice.Discount.ToString();
                    txtAmt.Text = Objinvoice.discAmount.ToString();
                    lblPrice.Value = Objinvoice.price.ToString();
                    txtAmount.Text = Objinvoice.AMOUNT.ToString();
                    TotalPriceui();

                    ViewState["prod"] = ID;

                }

                //UpdatePanel2.Update();
            }

            if (e.CommandName == "btnDelete")
            {

                string[] ID = e.CommandArgument.ToString().Split(',');
                int MYTRANSID = Convert.ToInt32(ID[0]);
                int MYID = Convert.ToInt32(ID[1]);
                int MyProdID = Convert.ToInt32(ID[2]);

                List<posinvoice> ListInvoice = new List<posinvoice>();

                if (ViewState["ListInvoice"] != null)
                {
                    ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];
                    posinvoice Objinvoice = ListInvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.MyProdID == MyProdID);
                    ListInvoice.Remove(Objinvoice);

                    Listview1.DataSource = ListInvoice;
                    Listview1.DataBind();

                }

                //UpdatePanel2.Update();
            }
        }

        public void ClearItemdetail()
        {
            txtQty.Text = "";
            txtDiscount.Text = "";
            txtAmt.Text = "";
            txtAmount.Text = "";
        }
        public void TotalPriceui()
        {
            //string str = "";
            //decimal DICUNTOTAL = 0;
            //decimal Total = 0;
            //decimal Priesh = 0;
            //decimal DICUNT = 0;
            //decimal qty = txtQty.Text != "" ? Convert.ToDecimal(txtQty.Text) : 0;

            //string Discount = txtDiscount.Text != "" ? txtDiscount.Text : "0";
            //if (Discount.Contains("%"))
            //{
            //    str = Discount.Replace('%', ' ');
            //    Priesh = lblPrice.Text != "" ? Convert.ToDecimal(lblPrice.Text) : 0;
            //    Total = qty * Priesh;
            //    DICUNT = Convert.ToDecimal(str);
            //    txtAmt.Text = (Total * (DICUNT / 100)).ToString("N3");
            //    DICUNTOTAL = Total - (Total * (DICUNT / 100));
            //    txtAmount.Text = DICUNTOTAL.ToString("N3");
            //}
            //else
            //{
            //    str = Discount;
            //    Priesh = lblPrice.Text != "" ? Convert.ToDecimal(lblPrice.Text) : 0;
            //    Total = qty * Priesh;
            //    DICUNT = Convert.ToDecimal(str);
            //    txtAmt.Text = DICUNT.ToString("N3");
            //    DICUNTOTAL = Total - DICUNT;
            //    txtAmount.Text = DICUNTOTAL.ToString("N3");
            //}
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            TotalPriceui();

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TotalPriceui();

        }
        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            TotalPriceui();
        }

        protected void btnaddamunt_Click(object sender, EventArgs e)
        {
            Database.ICTRPayTerms_HD objEco_ICCATEGORY = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICCATEGORY);
            TempICTRPayTerms_HD = (List<Database.ICTRPayTerms_HD>)ViewState["TempEco_ICCATEGORY"];
            Database.ICTRPayTerms_HD objEco_ICEXTRACOST = new ICTRPayTerms_HD();
            TempICTRPayTerms_HD.Add(objEco_ICEXTRACOST);
            ViewState["TempEco_ICCATEGORY"] = TempICTRPayTerms_HD;
            Repeater3.DataSource = TempICTRPayTerms_HD;
            Repeater3.DataBind();
        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblOHType = (Label)e.Item.FindControl("lblOHType");
                DropDownList drppaymentMeted = (DropDownList)e.Item.FindControl("drppaymentMeted");
                Classes.EcommAdminClass.getdropdown(drppaymentMeted, TID, "Payment", "Method", "Inventeri", "REFTABLE");

                List<Database.REFTABLE> ListRaf = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method" && p.SHORTNAME == "Inventeri" && p.REFNAME1 == "Cash").ToList();
                int PementMethod = ListRaf.Count() > 0 ? Convert.ToInt32(ListRaf.Single(p => p.TenentID == TID && p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method" && p.SHORTNAME == "Inventeri" && p.REFNAME1 == "Cash").REFID) : 0;
                drppaymentMeted.SelectedValue = PementMethod.ToString();
                if (lblOHType.Text != "" && lblOHType.Text != "0")
                {
                    int ID = Convert.ToInt32(lblOHType.Text);
                    drppaymentMeted.SelectedValue = ID.ToString();
                }
            }
        }

        protected void btnCategoryall_Click(object sender, EventArgs e)
        {
            BindData(0);
        }

        public int Pidalcode(DateTime Time)
        {
            int PODID = 0;
            var TBLPERIODS = DB.TBLPERIODS.Where(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).ToList();
            if (TBLPERIODS.Count() > 0)
                PODID = Convert.ToInt32(TBLPERIODS.Single(p => p.PRD_START_DATE <= Time && p.PRD_END_DATE >= Time && p.MYSYSNAME == "SAL" && p.TenentID == TID).PERIOD_CODE);
            return PODID;
        }

        int TransNo = 0;
        public void FinalConfirm()
        {


            if (HiddenField3.Value != null && HiddenField3.Value != "" && HiddenField3.Value != "0")
            { }
            else
            {
                if (DB.tblsetupsaleshes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid && p.DeftCoustomer != null && p.DeftCoustomer != 0).Count() > 0)
                {
                    HiddenField3.Value = DB.tblsetupsaleshes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid && p.DeftCoustomer != null && p.DeftCoustomer != 0).DeftCoustomer.ToString();
                }
                else
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "Kindly please select Customer And Contact To Administator to Set Default Customer in sales Setup";
                    return;
                }
            }


            int MTID = Convert.ToInt32(Session["GetMYTRANSID"]);

            List<posinvoice> ListInvoice = new List<posinvoice>();
            if (ViewState["ListInvoice"] != null)
            {
                ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];
            }

            decimal Amountpayment = 0;
            for (int i = 0; i < Repeater3.Items.Count; i++)
            {
                TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                if (txtammunt.Text != "")
                {
                    Amountpayment += Convert.ToDecimal(txtammunt.Text);
                }
            }
            decimal Amountdt = Convert.ToDecimal(ListInvoice.Sum(p => p.AMOUNT));
            if (Amountpayment != Amountdt)
            {
                panelMsg.Visible = true;
                lblErreorMsg.Text = "Check Your Pamment Total is not match";
                return;
            }

            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            int COMPID = 826667;
            string Status = "";

            string UseID = ((USER_MST)Session["USER"]).LOGIN_ID;
            DateTime TACtionDate = DateTime.Now;// Convert.ToDateTime(txtOrderDate.Text);
            string OICODID = Pidalcode(TACtionDate).ToString();
            if (DB.TBLCOMPANYSETUPs.Where(p => p.USERID == UseID && p.TenentID == TID).Count() == 1)
                COMPID = DB.TBLCOMPANYSETUPs.Single(p => p.USERID == UseID && p.TenentID == TID).COMPID;
            if (Session["Invoice"] != null)
            {

                TransNo = Convert.ToInt32(Session["Invoice"]);
                var List5 = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == TransNo).ToList();
                //var List5 = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
                int TenentID = TID;
                int MYTRANSID = TransNo;
                string PERIOD_CODE = OICODID;
                string MYSYSNAME = "SAL".ToString();
                int JOID = objProFile.JOID;
                int CRUP_ID = Convert.ToInt32(0);
                string GLPOST = objProFile.GLPOST;
                string GLPOST1 = objProFile.GLPOST1;
                string GLPOSTREF = objProFile.GLPOSTREF;
                string GLPOSTREF1 = objProFile.GLPOSTREF1;
                string ICPOST = objProFile.ICPOST;
                string ICPOSTREF = objProFile.ICPOSTREF;
                bool ACTIVE = Convert.ToBoolean(true);
                int COMPANYID = Convert.ToInt32(CURRENCY);
                if (ListInvoice.Where(p => p.MYTRANSID == MYTRANSID && p.TenentID == TID).Count() == 0)
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "Kindly please select minimum one Items; to save Invoice";
                    return;
                }

                string REFERENCE = "";// txtrefreshno.Text;
                //int ToTenentID = objProFile.ToTenentID; 
                int TOLOCATIONID = LID;
                //var List = DB.ICTR_DT.Where(p => p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.TenentID == TID).ToList();
                Database.tbltranssubtype objtbltranssubtype = Classes.EcommAdminClass.tbltranssubtype(TID, Transid, Transsubid);
                string MainTranType = "O";// Out Qty On Product
                string TransType = objProFile.TranType;
                string TranType = objtbltranssubtype.transsubtype1;// Out Qty On Product
                decimal COMPID1 = COMPID;
                decimal CUSTVENDID = Convert.ToInt32(HiddenField3.Value);
                string LF = "L";
                string ACTIVITYCODE = "0";
                decimal MYDOCNO = Convert.ToDecimal(objProFile.MYDOCNO);
                string USERBATCHNO = "";// txtBatchNo.Text;
                //decimal TOTAMT = Convert.ToDecimal(List.Sum(p => p.AMOUNT)); //Convert.ToDecimal(lblTotatotl.Text);
                //decimal TOTQTY1 = Convert.ToInt32(List.Sum(p => p.QUANTITY));
                decimal TOTAMT, TOTQTY1 = 0;


                TOTAMT = Convert.ToDecimal(lblFamount.Text);
                TOTQTY1 = Convert.ToDecimal(lblfQty.Text);


                decimal AmtPaid = objProFile.AmtPaid;
                string PROJECTNO = "99999";
                string CounterID = objProFile.CounterID;
                string PrintedCounterInvoiceNo = objProFile.PrintedCounterInvoiceNo;
                //string orderDate = DateTime.Now.ToShortDateString();
                DateTime TRANSDATE = DateTime.Now;// DateTime.ParseExact(orderDate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string NOTES = "";// txtNoteHD.Text;
                string USERID = UseID.ToString();
                DateTime Curentdatetime = DateTime.Now;
                DateTime ENTRYDATE = Curentdatetime;
                DateTime ENTRYTIME = Curentdatetime;
                DateTime UPDTTIME = Curentdatetime;
                //int InvoiceNO = 0;                    
                Status = "SO";
                decimal Discount = Convert.ToDecimal(0);
                List<Database.REFTABLE> ListRaf = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Terms" && p.REFSUBTYPE == "Terms" && p.REFNAME1 == "Cash").ToList();
                int Terms = ListRaf.Count() > 0 ? Convert.ToInt32(ListRaf.Single(p => p.TenentID == TID && p.REFTYPE == "Terms" && p.REFSUBTYPE == "Terms" && p.REFNAME1 == "Cash").REFID) : 0;

                string Custmerid = "";
                string Swit1 = "";
                //if (rbtPsle.SelectedValue == "1")
                //{
                Custmerid = txtCustomerName.Text;
                Swit1 = "1";
                //}
                //else if (rbtPsle.SelectedValue == "2")
                //{
                //    Custmerid = txtLocationSearch.Text;
                //    Swit1 = "2";
                //}
                string BillNo = "";// drpselsmen.SelectedValue;
                //string InvoiceNo = Classes.EcommAdminClass.TransDocNo(TID, Transid, Transsubid);
                string TransDocNo = "";

                List<Database.ICTR_HD> ListHDD = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                int Doc = ListHDD.Count() > 0 ? Convert.ToInt32(ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo) : 0;
                if (Doc != 0 && Doc != null)
                {
                    TransDocNo = ListHDD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).TransDocNo;
                }
                else
                {
                    var listtbltranssubtypes1 = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                    if (listtbltranssubtypes1.Count() > 0)
                    {
                        int SirialNO1 = (Convert.ToInt32(listtbltranssubtypes1.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1);
                        string SirialNO11 = SirialNO1.ToString();
                        TransDocNo = SirialNO11;
                    }
                    else
                        TransDocNo = "";
                }


                //Dipak
                string InvoiceNo = TransDocNo;
                int InvoiceNO = Convert.ToInt32(TransDocNo);
                int invoice_Type = Convert.ToInt32(drpInvoiceType.SelectedValue);
                int invoice_Source = Convert.ToInt32(drpInvoiceScurce.SelectedValue);
                ////////////////////////*****/////////////////
                DateTime Todate = DateTime.Now;

                ////////////////////////*****/////////////////
                //List<Database.ICTR_HD> ListHDD1 = DB.ICTR_HD.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                Classes.EcommAdminClass.insert_ICTR_HD(TenentID, MYTRANSID, 0, TOLOCATIONID, MainTranType, TranType, Transid, Transsubid, MYSYSNAME, COMPID1, CUSTVENDID, "L", PERIOD_CODE, ACTIVITYCODE, MYDOCNO, USERBATCHNO, TOTQTY1, TOTAMT, AmtPaid, PROJECTNO, CounterID, PrintedCounterInvoiceNo, JOID, TRANSDATE, REFERENCE, NOTES, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, USERID, ACTIVE, COMPANYID, ENTRYDATE, ENTRYTIME, UPDTTIME, InvoiceNO.ToString(), Discount, Status, Terms, Custmerid, Swit1, BillNo, MYTRANSID, "", TransDocNo, 0, invoice_Type, invoice_Source);

                var listtbltranssubtypes = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).ToList();
                string SirialNO = (Convert.ToInt32(listtbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid).serialno) + 1).ToString();
                if (SirialNO == InvoiceNo)
                {
                    Database.tbltranssubtype objtblsubtype = DB.tbltranssubtypes.Single(p => p.TenentID == TID && p.transid == Transid && p.transsubid == Transsubid);
                    objtblsubtype.serialno = SirialNO;
                    DB.SaveChanges();

                    String url = "List new record in tbltranssubtype with " + "TenentID = " + TID + "transid = " + Transid + "MYSYSNAME = " + MYSYSNAME + "transsubid = " + Transsubid;
                    String evantname = "List";
                    String tablename = "tbltranssubtype";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                }

                List<Database.ICTR_DT> ListDTDEL = DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                foreach (Database.ICTR_DT items in ListDTDEL)
                {
                    Database.ICTR_DT objdt = items;
                    DB.ICTR_DT.DeleteObject(objdt);
                    DB.SaveChanges();
                }

                int MyRunningSerial = 0;
                for (int i = 0; i < Listview1.Items.Count; i++)
                {
                    Label lblProdID = (Label)Listview1.Items[i].FindControl("lblProdID");
                    Label lblPRodName = (Label)Listview1.Items[i].FindControl("lblPRodName");
                    Label lblUOM = (Label)Listview1.Items[i].FindControl("lblUOM");
                    Label lblQty = (Label)Listview1.Items[i].FindControl("lblQty");
                    //Label lblTaxDis = (Label)Listview1.Items[i].FindControl("lblTaxDis");
                    Label lblTotalAMOUNT = (Label)Listview1.Items[i].FindControl("lblTotalAMOUNT");
                    Label lblUNITPRICE = (Label)Listview1.Items[i].FindControl("lblUNITPRICE");
                    Label lblDISAMT = (Label)Listview1.Items[i].FindControl("lblDISAMT");
                    Label lblDISPER = (Label)Listview1.Items[i].FindControl("lblDISPER");
                    //Label lblTAXAMT = (Label)Listview1.Items[i].FindControl("lblTAXAMT");
                    Label lblMYID = (Label)Listview1.Items[i].FindControl("lblMYID");
                    decimal DISPER = Convert.ToDecimal(lblDISPER.Text);//lblDISPER.Text
                    decimal DISAMT = Convert.ToDecimal(lblDISAMT.Text);//lblDISPER.Text
                    var listICTR_DT = Classes.EcommAdminClass.getListICTR_DT(TID, TransNo).ToList();
                    //int MYIDDT = listICTR_DT.Count() > 0 ? Convert.ToInt32(listICTR_DT.Max(p => p.MYID) + 1) : 1;
                    //int MYID = MYIDDT;
                    int MyProdID = Convert.ToInt32(lblProdID.Text);//lblProductNameItem.Text
                    int MYID = Convert.ToInt32(lblMYID.Text);
                    string REFTYPE = "LF";
                    string REFSUBTYPE = "LF";
                    int JOBORDERDTMYID = 1;
                    int ACTIVITYID = 0;
                    string DESCRIPTION = lblPRodName.Text;
                    string UOM = lblUOM.Text;
                    int QUANTITY = Convert.ToInt32(lblQty.Text);
                    decimal UNITPRICE = Convert.ToDecimal(lblUNITPRICE.Text);//
                    decimal AMOUNT = Convert.ToDecimal(lblTotalAMOUNT.Text);//lblTotalCurrency.Text
                    string BATCHNO = "1";
                    int BIN_ID = 1;
                    string BIN_TYPE = "Bin";
                    string GRNREF = "2";
                    int COMPANYID1 = COMPID;
                    int locationID = LID;
                    decimal TAXAMT = Convert.ToDecimal(0.0);
                    decimal TAXPER = Convert.ToDecimal(0.0);
                    decimal PROMOTIONAMT = Convert.ToDecimal(10.00);
                    decimal OVERHEADAMOUNT = Convert.ToDecimal(0.000);
                    DateTime EXPIRYDATE = DateTime.Now;
                    string SWITCH1 = "0".ToString();
                    int DelFlag = 0;
                    string ITEMID = "1";
                    int Uom = Convert.ToInt32(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MyProdID).UOM);

                    decimal CostAmount = Classes.EcommAdminClass.CostAmount(TID, MyProdID, LID, Uom);
                    Classes.EcommAdminClass.insert_ICTR_DT(TID, MYTRANSID, LID, MYID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                    //Classes.EcommAdminClass.insertICTR_DT(TenentID, MYTRANSID, LID, MyProdID, REFTYPE, REFSUBTYPE, PERIOD_CODE, MYSYSNAME, JOID, JOBORDERDTMYID, ACTIVITYID, DESCRIPTION, UOM, QUANTITY, UNITPRICE, AMOUNT, OVERHEADAMOUNT, BATCHNO, BIN_ID, BIN_TYPE, GRNREF, DISPER, DISAMT, TAXPER, TAXAMT, PROMOTIONAMT, CRUP_ID, GLPOST, GLPOST1, GLPOSTREF1, GLPOSTREF, ICPOST, ICPOSTREF, EXPIRYDATE, ACTIVE, SWITCH1, COMPANYID1, DelFlag, ITEMID);
                    Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.MYPRODID == MyProdID && p.TenentID == TID);
                    Boolean MultiUOM = Convert.ToBoolean(obj.MultiUOM);
                    Boolean MultiColor = Convert.ToBoolean(obj.MultiColor);
                    Boolean Perishable = Convert.ToBoolean(obj.Perishable);
                    Boolean MultiSize = Convert.ToBoolean(obj.MultiSize);
                    Boolean Serialized = Convert.ToBoolean(obj.Serialized);
                    Boolean MultiBinStore = Convert.ToBoolean(obj.MultiBinStore);

                    Database.ICTR_DT objDT = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.locationID == LID);
                    Database.ICTR_HD Objhd = DB.ICTR_HD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                    string Reference = Objhd.REFERENCE.ToString();

                    int UOM1 = Convert.ToInt32(UOM);
                    if (obj.MultiUOM == true)
                    {
                        string period_code1 = OICODID;
                        int SIZECODE = 999999998;
                        int COLORID = 999999998;
                        int BinID = 999999998;
                        string BatchNo = "999999998";
                        string Serialize = "NO";
                        string RecodName = "UOM";
                        DateTime ProdDate = DateTime.Now;
                        DateTime ExpiryDate = DateTime.Now;
                        DateTime LeadDays2Destroy = DateTime.Now;
                        string Active1 = "D";
                        Classes.EcommAdminClass.insertICIT_BR_TMP(TID, MyProdID, period_code1, MYSYSNAME, UOM1, SIZECODE, COLORID, BIN_ID, BatchNo, Serialize, MYTRANSID, LID, QUANTITY, Reference, RecodName, ProdDate, ExpiryDate, LeadDays2Destroy, Active1, CRUP_ID);
                    }

                    DateTime trnsDate = Convert.ToDateTime(Objhd.TRANSDATE);
                    string MySysName = Objhd.MYSYSNAME.ToString();


                    bool flag1 = Classes.EcommAdminClass.postprocess(TID, LID, Transid, Transsubid, MYTRANSID, MYID, QUANTITY, Reference, trnsDate, MySysName, MyProdID, ICPOST, UNITPRICE, obj, UOM1);
                    if (flag1 == false)
                    {
                        panelMsg.Visible = true;
                        lblErreorMsg.Text = "One of the Posting Parameter is Blank/Null/Zero!";
                        return;

                    }
                    int MyID = MYID;

                    decimal OTHERCURAMOUNT = Convert.ToDecimal(0);
                    int DELIVERDLOCATenentID = Convert.ToInt32(0);
                    int QUANTITYDELIVERD = Convert.ToInt32(0);
                    decimal AMOUNTDELIVERD = Convert.ToDecimal(0);
                    string ACCOUNTID = "1".ToString();
                    string Remark = "Data Insert formDtext".ToString();
                    int TransNo1 = Convert.ToInt32(0);

                    MyRunningSerial++;

                }

                List<Database.ICTRPayTerms_HD> ListHD_TEMP = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                decimal Payment = Convert.ToDecimal(DB.ICTR_DT.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Sum(p => p.AMOUNT));
                decimal Total = 0;

                for (int i = 0; i < Repeater3.Items.Count; i++)
                {
                    TextBox txtammunt = (TextBox)Repeater3.Items[i].FindControl("txtammunt");
                    if (txtammunt.Text != "")
                    {
                        decimal TXT = Convert.ToDecimal(txtammunt.Text);
                        Total = Total + TXT;
                    }
                }

                List<Database.ICTRPayTerms_HD> ListHD_TEMP1 = DB.ICTRPayTerms_HD.Where(p => p.TenentID == TID && p.MyTransID == MYTRANSID).ToList();

                if (Payment == Total)
                {

                    for (int j = 0; j < Repeater3.Items.Count; j++)
                    {
                        DropDownList drppaymentMeted = (DropDownList)Repeater3.Items[j].FindControl("drppaymentMeted");
                        TextBox txtrefresh = (TextBox)Repeater3.Items[j].FindControl("txtrefresh");
                        TextBox txtammunt = (TextBox)Repeater3.Items[j].FindControl("txtammunt");
                        if (txtrefresh.Text != "" && txtammunt.Text != "")
                        {
                            string RFresh = txtrefresh.Text.ToString();
                            string[] id = RFresh.Split(',');
                            string IDRefresh = id[0].ToString();
                            string IdApprouv = id[1].ToString();

                            int PaymentTermsId = Convert.ToInt32(drppaymentMeted.SelectedValue);
                            int AccountID = 0;
                            if (IDRefresh == "")
                                IDRefresh = drppaymentMeted.SelectedItem.ToString();

                            if (IdApprouv == "")
                                IdApprouv = MYTRANSID.ToString();


                            Decimal Amount = Convert.ToDecimal(txtammunt.Text);

                            Classes.EcommAdminClass.insertICTRPayTerms_HD(TID, MYTRANSID, PaymentTermsId, CounterID, LID, 0, Amount, IDRefresh, null, null, null, AccountID, CRUP_ID, IdApprouv, 0, false, false, false, false, null, null, null, null, null);
                        }
                    }
                }
                else
                {
                    //pnlSuccessMsg123.Visible = true;
                    //lblMsg123.Text = "Total Amount not Macth";
                    return;
                }

                //btnSaveData.Text = "Confirm Order";
                //btnConfirmOrder.Text = "Confirm Order";
                Session["Invoice"] = null;
            }

            //    scope.Complete();

            //}
            //readMode();
            //GridData();
        }

        protected void btnnewAdd_Click(object sender, EventArgs e)
        {
            AddnewInvoice();
        }

        public void AddnewInvoice()
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            Session["GetMYTRANSID"] = null;
            Session["Invoice"] = null;
            ViewState["ListInvoice"] = null;

            FillControl();
            int MYTRANSID = DB.ICTR_HD.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICTR_HD.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
            Session["GetMYTRANSID"] = MYTRANSID;
            Session["Invoice"] = MYTRANSID;
            InvoiceNO.Text = MYTRANSID.ToString();
            clear();
            List<posinvoice> ListInvoice = new List<posinvoice>();

            if (ViewState["ListInvoice"] != null)
            {
                ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];
            }
            Listview1.DataSource = ListInvoice;
            Listview1.DataBind();

            UpdatePanel4.Update();
        }
        public void clear()
        {
            txtCustomerName.Text = "";
            drpInvoiceScurce.SelectedIndex = 0;
            drpInvoiceType.SelectedIndex = 0;

            txtQty.Text = "";
            txtDiscount.Text = "";
            txtAmt.Text = "";
            txtAmount.Text = "";

            txtInvoiceAmt.Text = "";
            txtExtra.Text = "";
            txtPaidAmount.Text = "";
            txtReturnCharge.Text = "";

            lblfQty.Text = "";
            lblFPrice.Text = "";
            lblFdisc.Text = "";
            lblFamount.Text = "";
        }


        protected void btnPay_Click(object sender, EventArgs e)
        {
            FinalConfirm();
            Session["GetMYTRANSID"] = null;
            Session["Invoice"] = null;
        }

        protected void btnPayPrint_Click(object sender, EventArgs e)
        {
            FinalConfirm();
            Session["GetMYTRANSID"] = null;
            Session["Invoice"] = null;
        }

        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // DropDownList ddlUOM = (DropDownList)e.Item.FindControl("ddlUOM");
            TextBox txtproQty = (TextBox)e.Item.FindControl("txtproQty");
            TextBox txtprice = (TextBox)e.Item.FindControl("txtprice");
            Label lblProdid = (Label)e.Item.FindControl("lblProdid");
            Label lblPROUOM = (Label)e.Item.FindControl("lblPROUOM");
            Label lblUOMFinal = (Label)e.Item.FindControl("lblUOMFinal");
            ListView ListUOM = (ListView)e.Item.FindControl("ListUOM");

            int MYPRODID = Convert.ToInt32(lblProdid.Text);
            List<Database.ICUOM> ListUOm1 = new List<ICUOM>();
            List<Database.ICITEMS_PRICE> Listprice = DB.ICITEMS_PRICE.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID).ToList();
            foreach (Database.ICITEMS_PRICE Itemuo in Listprice)
            {
                Database.ICUOM objuom = DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == Itemuo.UOM);
                ListUOm1.Add(objuom);
            }

            //ddlUOM.DataSource = ListUOm1.Where(p => p.TenentID == TID);
            //ddlUOM.DataTextField = "UOMNAME1";
            //ddlUOM.DataValueField = "UOM";
            //ddlUOM.DataBind();
            //ddlUOM.Items.Insert(0, new ListItem("-- Select --", "0"));

            ListUOM.DataSource = ListUOm1;
            ListUOM.DataBind();

            if (ListUOm1.Count() > 0)
            {
                //ddlUOM.SelectedIndex = 1;
                int UOM = ListUOm1.FirstOrDefault().UOM;
                lblUOMFinal.Text = UOM.ToString();
                txtprice.Text = getprice(MYPRODID, UOM);
            }
            else
            {
                int UOM = Convert.ToInt32(lblPROUOM.Text);
                txtprice.Text = getprice(MYPRODID, UOM);
            }

            txtproQty.Text = "1";

        }
        protected void Button9_Click(object sender, EventArgs e)
        {

            TotalPriceui();
            if (ViewState["prod"] != null)
            {
                List<posinvoice> ListInvoice = new List<posinvoice>();

                if (ViewState["ListInvoice"] != null)
                {
                    ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];
                }

                string[] ID = ViewState["prod"].ToString().Split(',');
                int MYTRANSID = Convert.ToInt32(ID[0]);
                int MYID = Convert.ToInt32(ID[1]);
                int MyProdID = Convert.ToInt32(ID[2]);
                int qty = txtQty.Text != "" ? Convert.ToInt32(txtQty.Text) : 1;
                if (ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.MyProdID == MyProdID).Count() > 0)
                {
                    posinvoice Objinvoice = ListInvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYID == MYID && p.MyProdID == MyProdID);
                    Objinvoice.MyProdID = MyProdID;
                    Objinvoice.QUANTITY = qty;
                    decimal price = Convert.ToDecimal(lblPrice.Value);
                    Objinvoice.price = Convert.ToDecimal(lblPrice.Value);
                    Objinvoice.AMOUNT = Convert.ToDecimal(txtAmount.Text);
                    Objinvoice.Discount = txtDiscount.Text;
                    Objinvoice.TotalPrice = Convert.ToDecimal((price) * qty);
                    Objinvoice.discAmount = Convert.ToDecimal(txtAmt.Text);

                    ViewState["ListInvoice"] = ListInvoice;

                    Listview1.DataSource = ListInvoice;
                    Listview1.DataBind();
                    UpdatePanel4.Update();
                    ClearItemdetail();
                }
            }

        }
        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Addproduct")
            {
                panelMsg.Visible = false;
                lblErreorMsg.Text = "";
                if (Session["GetMYTRANSID"] == null)
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "Place Add New Invoice";
                    return;
                }
                Label lblProdid = (Label)e.Item.FindControl("lblProdid");
                Label lblPROUOM = (Label)e.Item.FindControl("lblPROUOM");
                Label lblUOMFinal = (Label)e.Item.FindControl("lblUOMFinal");
                //DropDownList ddlUOM = (DropDownList)e.Item.FindControl("ddlUOM");
                TextBox txtproQty = (TextBox)e.Item.FindControl("txtproQty");
                TextBox txtprice = (TextBox)e.Item.FindControl("txtprice");
                ModalPopupExtender ModalPopupExtender10 = (ModalPopupExtender)e.Item.FindControl("ModalPopupExtender10");
                int ID = Convert.ToInt32(lblProdid.Text);
                int UOM = Convert.ToInt32(lblPROUOM.Text);
                int MYTRANSID = Convert.ToInt32(Session["GetMYTRANSID"]);
                //int SelectUOM = Convert.ToInt32(ddlUOM.SelectedValue);
                int SelectUOM = Convert.ToInt32(lblUOMFinal.Text);
                if (lblUOMFinal.Text == "0" || txtprice.Text == "" || txtproQty.Text == "")
                {
                    ModalPopupExtender10.Show();
                    return;
                }

                List<posinvoice> ListInvoice = new List<posinvoice>();

                if (ViewState["ListInvoice"] != null)
                {
                    ListInvoice = (List<posinvoice>)ViewState["ListInvoice"];
                }

                if (ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MyProdID == ID && p.UOM == SelectUOM).Count() < 1)
                {
                    posinvoice Objinvoice = new posinvoice();

                    Objinvoice.TenentID = TID;
                    Objinvoice.MYTRANSID = MYTRANSID;
                    Objinvoice.MYID = ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.MYID) + 1 : 1;
                    Objinvoice.MyProdID = ID;
                    int QUANTITY = Convert.ToInt32(txtproQty.Text);
                    Objinvoice.QUANTITY = QUANTITY;
                    if (lblUOMFinal.Text == "0")
                    {
                        Objinvoice.UOM = Convert.ToInt32(UOM);
                    }
                    else
                    {
                        Objinvoice.UOM = Convert.ToInt32(lblUOMFinal.Text);
                    }
                    decimal price = Convert.ToDecimal(txtprice.Text);
                    Objinvoice.price = price;
                    Objinvoice.TotalPrice = Convert.ToDecimal(price * QUANTITY);
                    Objinvoice.AMOUNT = Convert.ToDecimal(price * QUANTITY);
                    Objinvoice.Discount = "0";
                    Objinvoice.discAmount = 0;
                    ListInvoice.Add(Objinvoice);

                    ViewState["ListInvoice"] = ListInvoice;

                    Listview1.DataSource = ListInvoice;
                    Listview1.DataBind();
                    UpdatePanel4.Update();
                    ClearItemdetail();
                }
            }
        }

        //protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlListFind = (DropDownList)sender;

        //    ListViewItem item1 = (ListViewItem)ddlListFind.NamingContainer;
        //    DropDownList ddlUOM = (DropDownList)item1.FindControl("ddlUOM");
        //    Label lbluprod = (Label)item1.FindControl("lbluprod");
        //    TextBox txtprice = (TextBox)item1.FindControl("txtprice");
        //    int MyProdID = Convert.ToInt32(lbluprod.Text);
        //    int UOM = Convert.ToInt32(ddlUOM.SelectedValue);
        //    if (UOM != 0)
        //    {
        //        txtprice.Text = getprice(MyProdID, UOM);
        //    }
        //}

        protected void ListUOM_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnUOM")
            {
                int btnUOM = Convert.ToInt32(e.CommandArgument);
                for (int i = 0; i <= Listview2.Items.Count - 1; i++)
                {
                    ListView ListUOM = (ListView)Listview2.Items[i].FindControl("ListUOM");
                    TextBox txtprice = (TextBox)Listview2.Items[i].FindControl("txtprice");
                    ModalPopupExtender ModalPopupExtender10 = (ModalPopupExtender)Listview2.Items[i].FindControl("ModalPopupExtender10");
                    Label lbluprod = (Label)Listview2.Items[i].FindControl("lbluprod");
                    Label lblUOMFinal = (Label)Listview2.Items[i].FindControl("lblUOMFinal");
                    int MYPRODID = 0, FUOM = 0;
                    for (int j = 0; j <= ListUOM.Items.Count - 1; j++)
                    {
                        Label lblUOM = (Label)ListUOM.Items[j].FindControl("lblUOM");
                        int UOM = Convert.ToInt32(lblUOM.Text);
                        if (btnUOM == UOM)
                        {
                            MYPRODID = Convert.ToInt32(lbluprod.Text);
                            FUOM = btnUOM;
                        }
                    }
                    if (FUOM != 0)
                    {
                        lblUOMFinal.Text = FUOM.ToString();
                        txtprice.Text = getprice(MYPRODID, FUOM).ToString();
                        ModalPopupExtender10.Show();
                    }

                }
            }
        }


    }
    [Serializable]
    public class posinvoice
    {
        public int TenentID { get; set; }
        public int MYTRANSID { get; set; }
        public int MYID { get; set; }
        public int MyProdID { get; set; }
        public int QUANTITY { get; set; }
        public int UOM { get; set; }
        public decimal price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AMOUNT { get; set; }
        public string Discount { get; set; }
        public decimal discAmount { get; set; }

    }
}