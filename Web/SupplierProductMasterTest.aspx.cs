using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SaasDAL;
//using SaasBAL;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Database;
using System.Transactions;
using AjaxControlToolkit;
using System.Data.Linq;

namespace Web
{
    public partial class SupplierProductMasterTest : System.Web.UI.Page
    {
        Database.ERPEntities DB = new Database.ERPEntities();
        bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["USER"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
               
                int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
                int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
                int LID = 1;
                BindpMaster();
                ManageLang();
                bindProdType();
                if (flag == false)
                {
                    LastData();
                    flag = true;
                }
                ViewState["Addnew"] = false;
                ViewState["strMulticolor"] = null;
                ViewState["StrMultiSize"] = null;
                ViewState["StrMultiMOU"] = null;
                ViewState["Mode"] = null;
                BindProDdl();
                ReadOnly();
                if (Request.QueryString["MYPRODID"] != null)
                {
                    int Program = Convert.ToInt32(Request.QueryString["MYPRODID"]);
                    List<Database.Eco_TBLPRODUCT> List = Classes.EcommAdminClass.getDataEco_TBLPRODUCT();
                    pMasterGridview.DataSource = List.Where(p => p.MYPRODID == Program && p.COMPANYID == UID && p.TenantID == TID && p.LOCATION_ID == LID);
                    pMasterGridview.DataBind();
                    if (Request.QueryString["MODE"] != null)
                    {
                        string Mode = Request.QueryString["MODE"].ToString();
                        if (Mode == "EditMode")
                        {
                            EditCamm(Program);
                            Write();
                        }
                        else
                        {
                            EditCamm(Program);
                            ReadOnly();
                        }
                    }
                }
                if (Request.QueryString["CatID"] != null)
                {
                   
                    int CatID = Convert.ToInt32(Request.QueryString["CatID"]);
                    ddlSuppCat.SelectedValue = CatID.ToString();
                    ddlSuppCat.Enabled = false;
                    Write();
                    clear();
                    int MYprod = DB.Eco_TBLPRODUCT.Count() > 0 ? Convert.ToInt32(DB.Eco_TBLPRODUCT.Max(p => p.MYPRODID) + 1) : 1;
                    lblmstproid.Text = MYprod.ToString();
                    ProSaleFenProid.Text = MYprod.ToString();
                }
            }
        }
        public void Write()
        {
            BtmBussAddRow.Enabled = true;
            txtcatepl.Enabled = true;
            txtcateparl.Enabled = true;
            txtcatepcol.Enabled = true;
            txtcatl2d.Enabled = true;
            txtcatsornum.Enabled = true;
            btncategory.Enabled = true;
            txtQtnonHand.Enabled = true;
            txtbrandpl.Enabled = true;
            txtbrandparl.Enabled = true;
            txtbrandpcl.Enabled = true;
            txtbrandl2d.Enabled = true;
            txtbrandsort.Enabled = true;
            btnbrand.Enabled = true;
            pm_txtProductName.Enabled = true;
            ddlSuppCat.Enabled = true;
            txtProBar.Enabled = true;
            ddlProdType.Enabled = true;
            txtACode1.Enabled = true;
            txtACode2.Enabled = true;
            txtItemRelSubProName2.Enabled = true;
            txtItemRelSubProName3.Enabled = true;
            txtShortName.Enabled = true;
            txtDescToprint.Enabled = true;
            DDLBussPro.Enabled = true;
            BussProact.Enabled = true;
            txtbppl.Enabled = true;
            txtbprl.Enabled = true;
            txtpbcol.Enabled = true;
            txtbpl2d.Enabled = true;
            txtbpsno.Enabled = true;
            ddlBrandname.Enabled = true;
            rbtnSKUP.Enabled = true;
            dropColoerOne.Enabled = true;
            ddlsize.Enabled = true;
            rdbtnMUOM.Enabled = true;
            ddluom.Enabled = true;
            drpUOM.Enabled = true;
            tags_4.Enabled = true;
            ddlItGrp.Enabled = true;
            ddlDofSale.Enabled = true;
            RadioBtnMoreItenHotItem.Enabled = true;
            RadioBtnMoreItenSerItem.Enabled = true;
            RadioBtnMoreItenMulticolor.Enabled = true;
            RadioBtnMoreItenMultisize.Enabled = true;
            RadioBtnMoreItenPurchAllow.Enabled = true;
            RadioBtnMoreItenSellAllow.Enabled = true;
            rbtnMBSto.Enabled = true;
            rbtPsle.Enabled = true;
            ddlMoreItenProMethod.Enabled = true;
            ddlMoreItenSupplyMethod.Enabled = true;
            txtWarranty.Enabled = true;
            txtnoteprinote.Enabled = true;
            txtnotepricostprice.Enabled = true;
            drpCurrency.Enabled = true;
            txtnoteprisaleori1.Enabled = true;
            txtnotepriwebsalepri.Enabled = true;
            txtnoteprisaleori2.Enabled = true;
            txtnoteprimrp.Enabled = true;
            txtnoteprisaleori3.Enabled = true;
            RatioProSaleFen.Enabled = true;
            txtProSaleFenRemark.Enabled = true;
            txtLINK2DIRECT.Enabled = true;
            tags_3.Enabled = true;
            txtboxshot.Enabled = true;
            txtlarge_boxshot.Enabled = true;
            txtmedium_boxshot.Enabled = true;
            txtsmall_boxshot.Enabled = true;
            txtos_platform.Enabled = true;
            txtcorp_logo.Enabled = true;
            txtlading_page.Enabled = true;
            txtline.Enabled = true;
            txttrial_url.Enabled = true;
            txtcart_link.Enabled = true;
            txtproduct_detail_link.Enabled = true;
            txtlead.Enabled = true;
            txtpromotion_type.Enabled = true;
            txtpayout.Enabled = true;
            RadioProSaleFenAct.Enabled = true;
            txtProSaleFenLaodpage.Enabled = true;
            txtProSaleFenBoxshot.Enabled = true;
            txtProSaleFenTiralURL.Enabled = true;
            txtProSaleFenLBoxshot.Enabled = true;
            txtProSaleFenCartUrl.Enabled = true;
            txtProSaleFenMBoxshot.Enabled = true;
            txtProSaleFenProDtlLink.Enabled = true;
            txtProSaleFenSBoxshot.Enabled = true;
            txtProSaleFenLead.Enabled = true;
            txtProSaleFenOsPlatform.Enabled = true;
            txtProSaleFenOhter.Enabled = true;
            txtProSaleFenCorpLogo.Enabled = true;
            txtProSaleFenProType.Enabled = true;
            txtProSaleFenLink.Enabled = true;
            txtProSaleFenPayout.Enabled = true;
            txtother.Enabled = true;
            for (int i = 0; i <= GridBussPro12.Items.Count - 1; i++)
            {

                LinkButton lnkbtndelete = (LinkButton)GridBussPro12.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkAction = (LinkButton)GridBussPro12.Items[i].FindControl("lnkAction");


                lnkbtndelete.Enabled = true;
                lnkAction.Enabled = true;

            }
        }
        public void ReadOnly()
        {
            BtnMstProSave.Visible = false;
            btnDiscard.Visible = false;
            Button3.Visible = false;
            Button4.Visible = false;
            Button5.Visible = true;
            Button6.Visible = true;
            Button1.Visible = true;
            Button2.Visible = true;
            lblProductmode.Text = lblBusinessMode.Text = lblBusinesslistmode.Text = lblMoreItemMode.Text = lblInterMode.Text = "Read Mode";
            BtmBussAddRow.Enabled = false;
            txtcatepl.Enabled = false;
            txtcateparl.Enabled = false;
            txtcatepcol.Enabled = false;
            txtcatl2d.Enabled = false;
            txtcatsornum.Enabled = false;
            btncategory.Enabled = false;
            txtQtnonHand.Enabled = false;
            txtbrandpl.Enabled = false;
            txtbrandparl.Enabled = false;
            txtbrandpcl.Enabled = false;
            txtbrandl2d.Enabled = false;
            txtbrandsort.Enabled = false;
            btnbrand.Enabled = false;
            pm_txtProductName.Enabled = false;
            ddlSuppCat.Enabled = false;
            txtProBar.Enabled = false;
            ddlProdType.Enabled = false;
            txtACode1.Enabled = false;
            txtACode2.Enabled = false;
            txtItemRelSubProName2.Enabled = false;
            txtItemRelSubProName3.Enabled = false;
            txtShortName.Enabled = false;
            txtDescToprint.Enabled = false;
            DDLBussPro.Enabled = false;
            BussProact.Enabled = false;
            txtbppl.Enabled = false;
            txtbprl.Enabled = false;
            txtpbcol.Enabled = false;
            txtbpl2d.Enabled = false;
            txtbpsno.Enabled = false;
            ddlBrandname.Enabled = false;
            rbtnSKUP.Enabled = false;
            dropColoerOne.Enabled = false;
            ddlsize.Enabled = false;
            rdbtnMUOM.Enabled = false;
            ddluom.Enabled = false;
            drpUOM.Enabled = false;
            tags_4.Enabled = false;
            ddlItGrp.Enabled = false;
            ddlDofSale.Enabled = false;
            RadioBtnMoreItenHotItem.Enabled = false;
            RadioBtnMoreItenSerItem.Enabled = false;
            RadioBtnMoreItenMulticolor.Enabled = false;
            RadioBtnMoreItenMultisize.Enabled = false;
            RadioBtnMoreItenPurchAllow.Enabled = false;
            RadioBtnMoreItenSellAllow.Enabled = false;
            rbtnMBSto.Enabled = false;
            rbtPsle.Enabled = false;
            ddlMoreItenProMethod.Enabled = false;
            ddlMoreItenSupplyMethod.Enabled = false;
            txtWarranty.Enabled = false;
            txtnoteprinote.Enabled = false;
            txtnotepricostprice.Enabled = false;
            drpCurrency.Enabled = false;
            txtnoteprisaleori1.Enabled = false;
            txtnotepriwebsalepri.Enabled = false;
            txtnoteprisaleori2.Enabled = false;
            txtnoteprimrp.Enabled = false;
            txtnoteprisaleori3.Enabled = false;
            RatioProSaleFen.Enabled = false;
            txtProSaleFenRemark.Enabled = false;
            txtLINK2DIRECT.Enabled = false;
            tags_3.Enabled = false;
            txtboxshot.Enabled = false;
            txtlarge_boxshot.Enabled = false;
            txtmedium_boxshot.Enabled = false;
            txtsmall_boxshot.Enabled = false;
            txtos_platform.Enabled = false;
            txtcorp_logo.Enabled = false;
            txtlading_page.Enabled = false;
            txtline.Enabled = false;
            txttrial_url.Enabled = false;
            txtcart_link.Enabled = false;
            txtproduct_detail_link.Enabled = false;
            txtlead.Enabled = false;
            txtpromotion_type.Enabled = false;
            txtpayout.Enabled = false;
            RadioProSaleFenAct.Enabled = false;
            txtProSaleFenLaodpage.Enabled = false;
            txtProSaleFenBoxshot.Enabled = false;
            txtProSaleFenTiralURL.Enabled = false;
            txtProSaleFenLBoxshot.Enabled = false;
            txtProSaleFenCartUrl.Enabled = false;
            txtProSaleFenMBoxshot.Enabled = false;
            txtProSaleFenProDtlLink.Enabled = false;
            txtProSaleFenSBoxshot.Enabled = false;
            txtProSaleFenLead.Enabled = false;
            txtProSaleFenOsPlatform.Enabled = false;
            txtProSaleFenOhter.Enabled = false;
            txtProSaleFenCorpLogo.Enabled = false;
            txtProSaleFenProType.Enabled = false;
            txtProSaleFenLink.Enabled = false;
            txtProSaleFenPayout.Enabled = false;
            txtother.Enabled = false;
            bindBusProGrid();
            for (int i = 0; i <= GridBussPro12.Items.Count - 1; i++)
            {

                LinkButton lnkbtndelete = (LinkButton)GridBussPro12.Items[i].FindControl("lnkbtndelete");
                LinkButton lnkAction = (LinkButton)GridBussPro12.Items[i].FindControl("lnkAction");


                lnkbtndelete.Enabled = false;
                lnkAction.Enabled = false;

            }
            ViewState["Mode"] = "RedMode";
        }
        protected void BindpMaster()
        {

            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            int MYprod = DB.Eco_TBLPRODUCT.Count() > 0 ? Convert.ToInt32(DB.Eco_TBLPRODUCT.Max(p => p.MYPRODID) + 1) : 1;
            lblmstproid.Text = MYprod.ToString();
            ProSaleFenProid.Text = MYprod.ToString();


            List<Database.Eco_TBLPRODUCT> List = Classes.EcommAdminClass.getDataEco_TBLPRODUCT();
            if (UID == 7)
            {
                pMasterGridview.DataSource = List.Where(p => p.TenantID == TID ).OrderByDescending(p => p.MYPRODID);
                pMasterGridview.DataBind();
            }
            else
            {
                pMasterGridview.DataSource = List.Where(p => p.TenantID == TID && p.COMPANYID == UID && p.LOCATION_ID == LID).OrderByDescending(p => p.MYPRODID);
                pMasterGridview.DataBind();
            }
          
            Classes.EcommAdminClass.getdropdown(ddlProdType, TID, "PRODTYPE", "PRODTYPE", "", "Eco_REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpCurrency, TID, "Prics", "", "", "Eco_tblCOUNTRY");
            Classes.EcommAdminClass.getdropdown(ddlsize, TID, "", "", "", "Eco_TBLSIZE");
            Classes.EcommAdminClass.getdropdown(DDLBussPro, TID, "", "", "", "Eco_MYBUS");
            Classes.EcommAdminClass.getdropdown(ddlMoreItenProMethod, TID, "PRODM", "PRODM", "", "Eco_REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlMoreItenSupplyMethod, TID, "SUPPM", "SUPPM", "", "Eco_REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpUOM, TID, "", "", "", "Eco_ICUOM");
            Classes.EcommAdminClass.getdropdown(ddlBrandname, TID, "BRAND", "OTH", "", "Eco_REFTABLE");
            Classes.EcommAdminClass.getdropdown(ddlDofSale, TID, "", "", "", "Eco_DEPTOFSale");
            Classes.EcommAdminClass.getdropdown(dropColoerOne, TID, "", "", "", "Eco_TBLCOLOR");
            Classes.EcommAdminClass.getdropdown(ddlItGrp, TID, "", "", "", "Eco_TBLGROUP");
        }

        public void bindProdType()
        {
            try
            {
                

                //ddlItGrp.DataSource = DB.Eco_TBLGROUP.Where(p => p.ACTIVE_Flag == true && p.ACTIVE == "1"); ;
                //ddlItGrp.DataTextField = "ITGROUPDESC1";
                //ddlItGrp.DataValueField = "ITGROUPID";
                //ddlItGrp.DataBind();
                //ddlItGrp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select IT Group--", "0"));
                //ddlDofSale.DataSource = DB.Eco_DEPTOFSale.Where(p => p.TenantID == TID && p.Active_Flag == true);
                //ddlDofSale.DataTextField = "DeptDesc1";
                //ddlDofSale.DataValueField = "SalDeptID";
                //ddlDofSale.DataBind();
                //ddlDofSale.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Department of Sale--", "0"));

                //dropColoerOne.DataSource = DB.Eco_TBLCOLOR.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(p => p.COLORDESC1);
                //dropColoerOne.DataTextField = "COLORDESC1";
                //dropColoerOne.DataValueField = "COLORID";
                //dropColoerOne.DataBind();
                //dropColoerOne.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Colors--", "0"));

                //ddlBrandname.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.REFTYPE == "BRAND" && p.REFSUBTYPE == "OTH").OrderBy(p => p.REFNAME1);
                //ddlBrandname.DataTextField = "REFNAME1";
                //ddlBrandname.DataValueField = "REFID";
                //ddlBrandname.DataBind();
                //ddlBrandname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Brand--", "0"));

                //ddlMoreItenSupplyMethod.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "SUPPM" && p.REFTYPE == "SUPPM").OrderBy(item => item.REFNAME1);
                //ddlMoreItenSupplyMethod.DataTextField = "REFNAME1";
                //ddlMoreItenSupplyMethod.DataValueField = "REFID";
                //ddlMoreItenSupplyMethod.DataBind();

                //drpUOM.DataSource = DB.Eco_ICUOM.Where(p => p.TenantID == TID && p.Active == "Y");
                //drpUOM.DataTextField = "UOMNAME1";
                //drpUOM.DataValueField = "UOM";
                //drpUOM.DataBind();
                //drpUOM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Unit of Measure--", "0"));
                //ddlMoreItenProMethod.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "PRODM" && p.REFTYPE == "PRODM").OrderBy(item => item.REFNAME1);
                //ddlMoreItenProMethod.DataTextField = "REFNAME1";
                //ddlMoreItenProMethod.DataValueField = "REFID";
                //ddlMoreItenProMethod.DataBind();
                //var data = DB.Eco_MYBUS.Where(p => p.BUSYPE != "0").ToList();
                //DDLBussPro.DataSource = data;
                //DDLBussPro.DataTextField = "BUSNAME1";
                //DDLBussPro.DataValueField = "BUSID";
                //DDLBussPro.DataBind();
                //DDLBussPro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Business Product--", "0"));
                //Session["dtBusProd"] = data;
                //ddlsize.DataSource = DB.Eco_TBLSIZE.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(p => p.SIZEDESC1);
                //ddlsize.DataTextField = "SIZETYPE";
                //ddlsize.DataValueField = "SIZECODE";
                //ddlsize.DataBind();
                //ddlsize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Size--", "0"));

                //ddlProdType.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.REFTYPE == "PRODTYPE" && p.REFSUBTYPE == "PRODTYPE" && p.TenantID == TID);
                //ddlProdType.DataTextField = "REFNAME1";
                //ddlProdType.DataValueField = "REFID";
                //ddlProdType.DataBind();
                //ddlProdType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Product Type--", "0"));


                //drpCurrency.DataSource = DB.Eco_tblCOUNTRY.Where(p => p.Active == "Y" && p.TenantID == TID);
                //drpCurrency.DataTextField = "CURRENCYNAME1";
                //drpCurrency.DataValueField = "CURRENCYNAME1";
                //drpCurrency.DataBind();
                //drpCurrency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Product Type--", "0"));

            }
            catch (Exception ex)
            {
                //  WebMsgBox.Show(ex.Message);
            }
        }
        private void bindBusProGrid()
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            int mid = Convert.ToInt32(lblmstproid.Text);
            List<Database.Eco_tblBusProd> List = Classes.EcommAdminClass.getDataEco_tblBusProd();
            if (UID == 7)
            {
                GridBussPro12.DataSource = List.Where(p => p.MYPRODID == mid && p.ACTIVE == true && p.TenantID == TID);
                GridBussPro12.DataBind();
            }
            else
            {
                GridBussPro12.DataSource = List.Where(p => p.MYPRODID == mid && p.ACTIVE == true && p.COMPANYID == UID && p.TenantID == TID && p.LOCATION_ID == LID);
                GridBussPro12.DataBind();
            }


        }
      
      
        private void clearBusPro()
        {
            try
            {
                DDLBussPro.SelectedIndex = 0;
                BussProact.SelectedIndex = 0;
                txtbppl.Text = "";
                txtbprl.Text = "";
                txtpbcol.Text = "";
                txtbpl2d.Text = "";
                txtbpsno.Text = "";
                //txtbpspa.Text = "";
            }
            catch (Exception ex)
            {
                Session["mess"] = ex.ToString();
                Session["PageName"] = "Products Master";
                Response.Redirect("~/Error/404Error.aspx", false);
            }
        }
        public void clear()
        {
            pm_txtProductName.Text = lblmstproid.Text = txtProBar.Text = txtACode1.Text = txtACode2.Text = txtShortName.Text = txtDescToprint.Text = txtnotepriwebsalepri.Text = txtItemRelSubProName2.Text = txtItemRelSubProName3.Text = tags_4.Text = txtnotepricostprice.Text = txtnoteprisaleori1.Text = txtnoteprisaleori2.Text = txtnoteprimrp.Text = "";
            ddlProdType.SelectedIndex = ddlItGrp.SelectedIndex = ddlsize.SelectedIndex = ddlDofSale.SelectedIndex = ddlBrandname.SelectedIndex = drpUOM.SelectedIndex = dropColoerOne.SelectedIndex = 0;
            // RadioBtnMoreItenSerItem.SelectedValue = RadioBtnMoreItenHotItem.SelectedValue = rbtnSKUP.SelectedValue = RadioBtnMoreItenMultisize.SelectedValue = rbtnMBSto.SelectedValue = rbtPsle.SelectedValue = RadioBtnMoreItenSellAllow.SelectedValue = RadioBtnMoreItenPurchAllow.SelectedValue = "1";
            txtProSaleFenRemark.Text = tags_4.Text = txtLINK2DIRECT.Text = tags_3.Text = txtboxshot.Text = txtlarge_boxshot.Text = txtlarge_boxshot.Text = "";
            txtmedium_boxshot.Text = txtsmall_boxshot.Text = txtos_platform.Text = txtos_platform.Text = txtcorp_logo.Text = txtline.Text = txttrial_url.Text = "";
            txtcart_link.Text = txtproduct_detail_link.Text = txtlead.Text = txtother.Text = txtpromotion_type.Text = txtpayout.Text = txtlading_page.Text = txtnoteprinote.Text = txtnoteprisaleori3.Text = txtWarranty.Text = "";
            drpCurrency.SelectedIndex = 0;
        }
        public void LastData()
        {

           
            int LASTPRODUCTID = Convert.ToInt32(DB.Eco_TBLPRODUCT.Where(p => p.ACTIVE == "1" && p.TenantID == 1).Max(p => p.MYPRODID));
            lblmstproid.Text = LASTPRODUCTID.ToString();
            int PID = LASTPRODUCTID;
            Database.Eco_TBLPRODDTL objoddtl = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == PID);
            ProSaleFenProid.Text = objoddtl.MYPRODID.ToString();
            txtProSaleFenRemark.Text = objoddtl.REMARKS.ToString();
            tags_4.Text = objoddtl.keywords.ToString();
            txtLINK2DIRECT.Text = objoddtl.LINK2DIRECT.ToString();
            tags_3.Text = objoddtl.keywords.ToString();
            txtboxshot.Text = objoddtl.boxshot.ToString();
            txtlarge_boxshot.Text = objoddtl.large_boxshot.ToString();
            txtmedium_boxshot.Text = objoddtl.medium_boxshot.ToString();
            txtsmall_boxshot.Text = objoddtl.small_boxshot.ToString();
            txtos_platform.Text = objoddtl.os_platform.ToString();
            txtcorp_logo.Text = objoddtl.corp_logo.ToString();
            txtline.Text = objoddtl.link.ToString();
            txttrial_url.Text = objoddtl.trial_url.ToString();
            txtcart_link.Text = objoddtl.cart_link.ToString();
            txtproduct_detail_link.Text = objoddtl.product_detail_link.ToString();
            txtlead.Text = objoddtl.lead.ToString();
            txtother.Text = objoddtl.other.ToString();
            txtpromotion_type.Text = objoddtl.promotion_type.ToString();
            txtpayout.Text = objoddtl.payout.ToString();
            txtlading_page.Text = objoddtl.lading_page.ToString();
            Database.Eco_TBLPRODUCT objproduct = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == PID);
            dropColoerOne.SelectedValue = objproduct.COLORID.ToString();
            if (DB.Eco_Product_Cat_Mst.Where(p => p.MYPRODID == PID && p.DefaultProduct == "Y").Count() > 0)
            {
                int maincat = DB.Eco_Product_Cat_Mst.Single(p => p.MYPRODID == PID && p.DefaultProduct == "Y").CATID;
                ddlSuppCat.SelectedValue = maincat.ToString();
            }
            else
            {
                if (objproduct.MainCategoryID == null || objproduct.MainCategoryID == 0)
                { }
                else
                {
                    ddlSuppCat.SelectedValue = objproduct.MainCategoryID.ToString();
                }
            }
            if (objproduct.InternalNotes == null)
            {

            }
            else
            {
                txtnoteprinote.Text = objproduct.InternalNotes.ToString();
            }
            if (objproduct.InternalNotes == null)
            {

            }
            else
            {
                txtnoteprisaleori3.Text = objproduct.onlinesale3.ToString();
            }
            if (objproduct.keywords == null)
            { }
            else
            {
                tags_4.Text = objproduct.keywords.ToString();
            }
            if (objproduct.BarCode == null)
            { }
            else
                txtProBar.Text = objproduct.BarCode.ToString();
            if (objproduct.ProdName1 == null)
            {

            }
            else
            {
                pm_txtProductName.Text = objproduct.ProdName1.ToString();
            }
            if (objproduct.AlternateCode1 == null)
            { }
            else
            {
                txtACode1.Text = objproduct.AlternateCode1.ToString();
            }
            if (objproduct.AlternateCode2 == null)
            { }
            else
            {
                txtACode2.Text = objproduct.AlternateCode2.ToString();
            }
            if (objproduct.QTYINHAND == null)
            {

            }
            else
            {
                txtQtnonHand.Text = objproduct.QTYINHAND.ToString();
            }
            ddlProdType.SelectedValue = objproduct.ProdTypeRefId.ToString();
            ddlItGrp.SelectedValue = objproduct.GROUPID.ToString();
            ddlsize.SelectedValue = objproduct.SIZECODE.ToString();
            ddlMoreItenSupplyMethod.SelectedValue = objproduct.SupplyMethodID.ToString();
            ddlMoreItenProMethod.SelectedValue = objproduct.ProdMethodID.ToString();
            ddlDofSale.SelectedValue = objproduct.SalDeptID.ToString();
            drpUOM.SelectedValue = objproduct.UOM.ToString();
            ddlBrandname.SelectedValue = objproduct.Brand.ToString();
            //tags_3.Text = objproduct.keywords.ToString();
            if (objproduct.Warranty == null)
            {

            }
            else
            {
                txtWarranty.Text = objproduct.Warranty.ToString();
            }
            if (objproduct.ShortName == null)
            { }
            else
            {
                txtShortName.Text = objproduct.ShortName.ToString();
            }
            if (objproduct.DescToprint == null)
            { }
            else
            {
                txtDescToprint.Text = objproduct.DescToprint.ToString();
            }
            if (objproduct.ProdName2 == null)
            { }
            else
            {
                txtItemRelSubProName2.Text = objproduct.ProdName2.ToString();
            }
            if (objproduct.ProdName3 == null)
            { }
            else
            {
                txtItemRelSubProName3.Text = objproduct.ProdName3.ToString();
            }

            if (objproduct.basecost == null)
            { }
            else
            {
                txtnotepricostprice.Text = objproduct.basecost.ToString();
            }
            if (objproduct.onlinesale1 == null)
            { }
            else
            {
                txtnoteprisaleori1.Text = objproduct.onlinesale1.ToString();
            }
            if (objproduct.onlinesale2 == null)
            { }
            else
            {
                txtnoteprisaleori2.Text = objproduct.onlinesale2.ToString();
            }
            if (objproduct.msrp == null)
            { }
            else
            {
                txtnoteprimrp.Text = objproduct.msrp.ToString();
            }
            if (objproduct.price == null)
            { }
            else
            {
                txtnotepriwebsalepri.Text = objproduct.price.ToString();
            }
            if (objproduct.MainCategoryID == null || objproduct.MainCategoryID == 0)
            { }
            else
            {
                ddlSuppCat.SelectedValue = objproduct.MainCategoryID.ToString();
            }
            if (objproduct.currency.Trim() == null || objproduct.currency.Trim() == " " || objproduct.currency.Trim() == "0" || objproduct.currency.Trim() == "$")
            { }
            else
            {
                drpCurrency.SelectedValue = objproduct.currency.ToString();
            }

            if (objproduct.Serialized == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenHotItem.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenHotItem.SelectedValue = "1";
            }
            if (objproduct.SKUProduction == Convert.ToBoolean(0))
            {
                rbtnSKUP.SelectedValue = "0";
            }
            else
            {
                rbtnSKUP.SelectedValue = "1";
            }
            if (objproduct.MultiSize == Convert.ToBoolean(0))
            { RadioBtnMoreItenMultisize.SelectedValue = "0"; }
            else
            {
                RadioBtnMoreItenMultisize.SelectedValue = "1";
            }
            if (objproduct.MultiColor == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenMulticolor.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenMulticolor.SelectedValue = "1";
            }
            if (objproduct.MultiUOM == Convert.ToBoolean(0))
            {
                rdbtnMUOM.SelectedValue = "0";
            }
            else
            {
                rdbtnMUOM.SelectedValue = "1";
            }

            if (objproduct.MultiBinStore == Convert.ToBoolean(0))
            {
                rbtnMBSto.SelectedValue = "0";
            }
            else
            {
                rbtnMBSto.SelectedValue = "1";
            }
            if (objproduct.Perishable == Convert.ToBoolean(0))
            {
                rbtPsle.SelectedValue = "0";
            }
            else
            {
                rbtPsle.SelectedValue = "1";
            }
            if (objproduct.SaleAllow == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenSellAllow.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenSellAllow.SelectedValue = "1";
            }
            if (objproduct.PurAllow == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenPurchAllow.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenPurchAllow.SelectedValue = "1";
            }
            if (objproduct.Serialized == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenSerItem.SelectedValue = "1";
            }
            else
            {
                RadioBtnMoreItenSerItem.SelectedValue = "0";
            }
            //  bindBusProGrid();
            //txtACode1.Text = ds.Tables[0].Rows[MaintCnt]["AlternateCode1"].ToString();
        }

        public void BindProDdl()
        {
            //try
            //{



            //DataTable dt = objCommonSelDel.Get_Common_Pro_Master1("cattype", "Websale", "prc_RecursiveCall1");
            List<testprc_RecursiveCall1_Result> obj = DB.prc_RecursiveCall1("WEBSALE", "cattype").ToList();
            List<Eco_CAT_MST> EcomCateList = new List<Eco_CAT_MST>();
            foreach (testprc_RecursiveCall1_Result dr in obj)
            {

                var hirachy = dr.Hiranchy.ToString();
                string s1 = hirachy;
                int s2 = s1.IndexOf('/') + 1;
                string s3 = s1.Substring(s2, s1.Length - s2);
                dr.Hiranchy = s3;
                Eco_CAT_MST objEco_CAT_MST = new Eco_CAT_MST();
                objEco_CAT_MST.CAT_NAME1 = s3;
                objEco_CAT_MST.CATID = Convert.ToInt32(dr.CaTID);
                EcomCateList.Add(objEco_CAT_MST);
            }

            if (obj.Count > 0)
            {
                Session["temp"] = EcomCateList;
                ddlSuppCat.DataSource = EcomCateList;
                ddlSuppCat.DataTextField = "CAT_NAME1";
                ddlSuppCat.DataValueField = "CATID";
                ddlSuppCat.DataBind();
                ddlSuppCat.Items.Insert(0, "-- Select --");
                //ddlSuppCat.SelectedIndex = -1;
            }
            else
            {
                ddlSuppCat.Items.Insert(0, "-- Select --");
                //ddlSuppCat.SelectedIndex = -1;
            }

            //  }
            //catch (Exception ex)
            //{
            //    Session["mess"] = ex.ToString();
            //    Session["PageName"] = "Products Master";
            //    Response.Redirect("~/Error/404Error.aspx", false);
            //}
        }
        protected void BtmBussAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
                int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
                int LID = 1;
                int mid = Convert.ToInt32(lblmstproid.Text);
                int bus = Convert.ToInt32(DDLBussPro.SelectedValue);
                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.Eco_tblBusProd objtblBusProd = DB.Eco_tblBusProd.Single(p => p.MYID == ID);
                    objtblBusProd.TenantID = TID;
                    objtblBusProd.LOCATION_ID = LID;
                    objtblBusProd.MYPRODID = mid;
                    objtblBusProd.COMPANYID = UID;
                    objtblBusProd.REFID = Convert.ToInt32(DB.Eco_MYBUS.Single(p => p.BUSID == bus).BUSYPE);
                    objtblBusProd.REFTYPE = DB.Eco_REFTABLE.Single(p => p.REFID == objtblBusProd.REFID).REFTYPE;
                    objtblBusProd.REFSUBTYPE = DB.Eco_REFTABLE.Single(p => p.REFID == objtblBusProd.REFID).REFSUBTYPE;
                    // objtblBusProd.MYID = DB.Eco_tblBusProds.Count() > 0 ? Convert.ToInt32(DB.Eco_tblBusProds.Max(p => p.MYID) + 1) : 1;
                    objtblBusProd.MYTYPE = DB.Eco_MYBUS.Single(p => p.BUSID == bus).BUSNAME1;
                    // objtblBusProd.DISPLAY_ID = DB.ICProdDisplays.Count() > 0 ? Convert.ToInt32(DB.ICProdDisplays.Max(p => p.Display_Id) + 1) : 1;
                    int disid = Convert.ToInt32(objtblBusProd.DISPLAY_ID);
                    if (DB.Eco_ICProdDisplay.Where(p => p.Display_Id == disid).Count() > 0)
                    {
                        Database.Eco_ICProdDisplay objICProdDisplay = DB.Eco_ICProdDisplay.Single(p => p.Display_Id == disid);
                        objtblBusProd.ACTIVE = true;
                        objICProdDisplay.TenantID = TID;
                        objICProdDisplay.MYPRODID = mid;

                        objICProdDisplay.REFID = objtblBusProd.REFID;
                        objICProdDisplay.REFTYPE = objtblBusProd.REFTYPE;
                        objICProdDisplay.REFSUBTYPE = objtblBusProd.REFSUBTYPE;
                        objICProdDisplay.Display_Id = Convert.ToInt32(objtblBusProd.DISPLAY_ID);
                        objICProdDisplay.TableName = "tblBusProd";
                        objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtbppl.Text);
                        objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtbprl.Text);
                        objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtpbcol.Text);
                        objICProdDisplay.LINK2DIRECT = txtbpl2d.Text;
                        objICProdDisplay.sortnumber = Convert.ToInt32(txtbpsno.Text);
                        objICProdDisplay.ACTIVE2 = true;
                        DB.SaveChanges();
                        Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                        objEco_ERP_WEB_USER_MST1.IsCache = true;
                        objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                        DB.SaveChanges();
                    }

                    ViewState["Edit"] = null;
                    BtmBussAddRow.Text = "Save";
                }
                else
                {
                    int RefID = Convert.ToInt32(DB.Eco_MYBUS.Single(p => p.BUSID == bus).BUSYPE);
                    string REFTYPE = DB.Eco_REFTABLE.Single(p => p.REFID == RefID).REFTYPE;
                    string REFSUBTYPE = DB.Eco_REFTABLE.Single(p => p.REFID == RefID).REFSUBTYPE;
                    if (DB.Eco_tblBusProd.Where(p => p.MYPRODID == mid && p.REFID == RefID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.ACTIVE == true).Count() > 0)
                    {
                        string display = "This Product All Ready This Bissess Are Used!";
                        pnlSuccessMsg123.Visible = true;
                        lblMsg123.Text = display;
                        //ClientScript.RegisterStartupScript(this.GetType(), "This Product All Ready This Bissess Are Used!", "alert('" + display + "');", true);
                        return;
                    }
                    else
                    {
                        Database.Eco_tblBusProd objtblBusProd = new Database.Eco_tblBusProd();
                        objtblBusProd.TenantID = TID;
                        objtblBusProd.MYPRODID = mid;
                        objtblBusProd.LOCATION_ID = LID;
                        objtblBusProd.COMPANYID = UID;
                        objtblBusProd.REFID = Convert.ToInt32(DB.Eco_MYBUS.Single(p => p.BUSID == bus).BUSYPE);
                        objtblBusProd.REFTYPE = DB.Eco_REFTABLE.Single(p => p.REFID == objtblBusProd.REFID).REFTYPE;
                        objtblBusProd.REFSUBTYPE = DB.Eco_REFTABLE.Single(p => p.REFID == objtblBusProd.REFID).REFSUBTYPE;
                        objtblBusProd.MYID = DB.Eco_tblBusProd.Count() > 0 ? Convert.ToInt32(DB.Eco_tblBusProd.Max(p => p.MYID) + 1) : 1;
                        objtblBusProd.MYTYPE = DB.Eco_MYBUS.Single(p => p.BUSID == bus).BUSNAME1;
                        objtblBusProd.ACTIVE = true;
                        objtblBusProd.ONLINESALEALLOW = false;
                        DB.Eco_tblBusProd.AddObject(objtblBusProd);
                        DB.SaveChanges();
                        Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblBusProd");
                        objEco_ERP_WEB_USER_MST1.IsCache = true;
                        objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                        DB.SaveChanges();
                        if (txtbppl.Text != "0" && txtbprl.Text != "0" && txtpbcol.Text != "0")
                        {
                            Database.Eco_ICProdDisplay objICProdDisplay = new Database.Eco_ICProdDisplay();
                            objICProdDisplay.TenantID = TID;
                            objICProdDisplay.MYPRODID = mid;
                            objICProdDisplay.Display_Id = DB.Eco_ICProdDisplay.Count() > 0 ? Convert.ToInt32(DB.Eco_ICProdDisplay.Max(p => p.Display_Id) + 1) : 1;
                            objICProdDisplay.REFID = objtblBusProd.REFID;
                            objICProdDisplay.REFTYPE = objtblBusProd.REFTYPE;
                            objICProdDisplay.REFSUBTYPE = objtblBusProd.REFSUBTYPE;
                            objICProdDisplay.TableName = "tblBusProd";
                            objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtbppl.Text);
                            objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtbprl.Text);
                            objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtpbcol.Text);
                            objICProdDisplay.LINK2DIRECT = txtbpl2d.Text;
                            objICProdDisplay.sortnumber = Convert.ToInt32(txtbpsno.Text);
                            objICProdDisplay.ACTIVE2 = true;
                            objtblBusProd.DISPLAY_ID = objICProdDisplay.Display_Id;
                            DB.Eco_ICProdDisplay.AddObject(objICProdDisplay);
                            DB.SaveChanges();
                            Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                            objEco_ERP_WEB_USER_MST.IsCache = true;
                            objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                            DB.SaveChanges();
                        }
                        else
                        {

                        }

                    }

                }

                bindBusProGrid();


            }
            catch (Exception ex)
            {
                //Session["mess"] = ex.ToString();
                //Session["PageName"] = "Products Master";
                //Response.Redirect("~/Error/404Error.aspx", false);
            }
        }
        protected void BtnMstProSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
                int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
                int LID = 1;
                if (ViewState["Product"] != null)
                {
                    int myprodid = Convert.ToInt32(ViewState["Product"]);
                    Database.Eco_TBLPRODUCT objproduct = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == myprodid);
                    objproduct.TenantID = TID;
                    objproduct.LOCATION_ID = LID;
                    objproduct.MYPRODID = myprodid;
                    objproduct.BarCode = txtProBar.Text;
                    objproduct.AlternateCode1 = txtACode1.Text;
                    objproduct.AlternateCode2 = txtACode1.Text;
                    objproduct.UOM = Convert.ToInt32(drpUOM.SelectedValue);
                    objproduct.GROUPID = Convert.ToInt32(ddlItGrp.SelectedValue);
                    objproduct.SIZECODE = Convert.ToInt32(ddlsize.SelectedValue);
                    objproduct.ProdTypeRefId = Convert.ToInt32(ddlProdType.SelectedValue);
                    objproduct.SupplyMethodID = Convert.ToInt32(ddlMoreItenSupplyMethod.SelectedValue);
                    objproduct.ProdMethodID = Convert.ToInt32(ddlMoreItenProMethod.SelectedValue);
                    objproduct.SalDeptID = Convert.ToInt32(ddlDofSale.SelectedValue);
                    objproduct.ShortName = txtShortName.Text;
                    objproduct.DescToprint = txtDescToprint.Text;
                    objproduct.ProdName1 = pm_txtProductName.Text;
                    objproduct.ProdName2 = txtItemRelSubProName2.Text;
                    objproduct.ProdName3 = txtItemRelSubProName3.Text;
                    objproduct.COMPANYID = UID;
                    if (ddlBrandname.SelectedValue == "0")
                    {
                        objproduct.Brand = "8881";
                    }
                    else
                        objproduct.Brand = ddlBrandname.SelectedValue;
                    if (txtQtnonHand.Text == "")
                    {
                        objproduct.QTYINHAND = 0;
                    }
                    else
                    {
                        objproduct.QTYINHAND = Convert.ToInt32(txtQtnonHand.Text);
                    }
                    objproduct.REMARKS = "";
                    if (ddlSuppCat.SelectedValue == "0")
                        objproduct.MainCategoryID = null;
                    else
                        objproduct.MainCategoryID = Convert.ToInt32(ddlSuppCat.SelectedValue);
                    if (txtnotepricostprice.Text == "")
                        objproduct.basecost = 0;
                    else
                        objproduct.basecost = Convert.ToDecimal(txtnotepricostprice.Text);
                    if (txtnoteprisaleori1.Text == "")
                        objproduct.onlinesale1 = 0;
                    else
                        objproduct.onlinesale1 = Convert.ToDecimal(txtnoteprisaleori1.Text);

                    if (txtnoteprisaleori2.Text == "")
                        objproduct.onlinesale2 = 0;
                    else
                        objproduct.onlinesale2 = Convert.ToDecimal(txtnoteprisaleori2.Text);

                    if (txtnoteprimrp.Text == "")
                        objproduct.msrp = 0;
                    else
                        objproduct.msrp = Convert.ToDecimal(txtnoteprimrp.Text);

                    if (txtnotepriwebsalepri.Text == "")
                        objproduct.price = 0;
                    else
                        objproduct.price = Convert.ToDecimal(txtnotepriwebsalepri.Text);


                    objproduct.currency = drpCurrency.SelectedValue;

                    //if (txtINFOQntyonhand.Text == "")
                    //    objproduct.QTYINHAND = null;
                    //else
                    //    objproduct.QTYINHAND = Convert.ToInt32(txtINFOQntyonhand.Text);

                    if (txtINFOQntyRes.Text == "")
                        objproduct.QTYRCVD = 0;
                    else
                        objproduct.QTYRCVD = Convert.ToInt32(txtINFOQntyRes.Text);

                    if (txtINFOQntySold.Text == "")
                        objproduct.QTYSOLD = 0;
                    else
                        objproduct.QTYSOLD = Convert.ToInt32(txtINFOQntySold.Text);

                    objproduct.PICTURE = null;
                    objproduct.ACTIVE = "1";
                    objproduct.DIRECTSALE = "1";
                    //  objproduct.LINK2DIRECT = txtMoreItenwarrantyLink2d.Text;
                    objproduct.DISPDATE3 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                    if (txtWarranty.Text == null)
                    {
                        objproduct.Warranty = "";
                    }
                    else
                    {
                        objproduct.Warranty = txtWarranty.Text;
                    }
                    if (txtINFOlastpurchdt.Text == "")
                        objproduct.LASTPURDATE = DateTime.Now;
                    else
                        objproduct.LASTPURDATE = Convert.ToDateTime(txtINFOlastpurchdt.Text);
                    if (txtINFOlastsaledt.Text == "")
                        objproduct.LASTSALDATE = DateTime.Now;
                    else
                        objproduct.LASTSALDATE = Convert.ToDateTime(txtINFOlastsaledt.Text);
                    objproduct.COLORID = dropColoerOne.SelectedValue;
                    if (txtnoteprisaleori3.Text == "")
                    {
                        objproduct.onlinesale3 = 0;
                    }
                    else
                    {
                        objproduct.onlinesale3 = Convert.ToDecimal(txtnoteprisaleori3.Text);
                    }
                    if (txtnoteprinote.Text == "")
                    {
                        objproduct.InternalNotes = "";
                    }
                    else
                    {
                        objproduct.InternalNotes = txtnoteprinote.Text;
                    }
                    //  objproduct.COLORID = txtxitemchcolore.Text;

                    if (tags_4.Text == "")
                    {
                        objproduct.keywords = "";
                    }
                    else
                    {
                        objproduct.keywords = tags_4.Text;
                    }
                    objproduct.Serialized = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenSerItem.SelectedValue));
                    objproduct.HotItem = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenHotItem.SelectedValue));
                    objproduct.SKUProduction = Convert.ToBoolean(Convert.ToInt32(rbtnSKUP.SelectedValue));
                    objproduct.SKU = null;
                    objproduct.MultiSize = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenMultisize.SelectedValue));

                    if (RadioBtnMoreItenMultisize.SelectedValue == "1")
                    {

                        string[] Seperate5 = txtMultiSize.Text.Split('/');
                        int count5 = 0;
                        string Sep5 = "";
                        for (int i = 0; i <= Seperate5.Count() - 1; i++)
                        {
                            Sep5 = Seperate5[i];
                            string Name = Sep5.ToString();
                            count5++;
                            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == myprodid && p.RecValue == Name).Count() > 0)
                            {

                            }
                            else
                            {
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiSize";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = myprodid;
                                obj.RunSerial = count5;
                                obj.Recource = 5008;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate5[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST1.IsCache = true;
                                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                                DB.SaveChanges();


                            }

                        }

                    }
                    objproduct.MultiUOM = Convert.ToBoolean(Convert.ToInt32(rdbtnMUOM.SelectedValue));
                    if (rdbtnMUOM.SelectedValue == "1")
                    {
                        string[] Seperate1 = txtmultiMOU.Text.Split('/');
                        int count1 = 0;
                        string Sep1 = "";
                        for (int i = 0; i <= Seperate1.Count() - 1; i++)
                        {
                            Sep1 = Seperate1[i];
                            string Name = Sep1.ToString();
                            count1++;
                            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == myprodid && p.RecValue == Name).Count() > 0)
                            {

                            }
                            else
                            {
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiUOM";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = myprodid;
                                obj.RunSerial = count1;
                                obj.Recource = 5009;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST1.IsCache = true;
                                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                    }
                    objproduct.MultiColor = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenMulticolor.SelectedValue));
                    if (RadioBtnMoreItenMulticolor.SelectedValue == "1")
                    {

                        string[] Seperate4 = txtMulticolers.Text.Split('/');
                        int count4 = 0;
                        string Sep4 = "";
                        for (int i = 0; i <= Seperate4.Count() - 1; i++)
                        {
                            Sep4 = Seperate4[i];
                            string Name = Sep4.ToString();
                            count4++;
                            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == myprodid && p.RecValue == Name).Count() > 0)
                            {

                            }
                            else
                            {
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiColor";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = myprodid;
                                obj.RunSerial = count4;
                                obj.Recource = 5007;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate4[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST1.IsCache = true;
                                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                    }

                    objproduct.MultiBinStore = Convert.ToBoolean(Convert.ToInt32(rbtnMBSto.SelectedValue));
                    objproduct.Perishable = Convert.ToBoolean(Convert.ToInt32(rbtPsle.SelectedValue));
                    objproduct.SaleAllow = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenSellAllow.SelectedValue));
                    objproduct.PurAllow = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenPurchAllow.SelectedValue));
                    Database.Eco_TBLPRODDTL objoddtl = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == myprodid);
                    objoddtl.TenantID = TID;
                    // objoddtl.MYPRODID = Convert.ToInt32(ProSaleFenProid.Text);
                    if (txtProSaleFenRemark.Text == "")
                    {
                        objoddtl.REMARKS = "";
                    }
                    else
                    {
                        objoddtl.REMARKS = txtProSaleFenRemark.Text;
                    }

                    objoddtl.ACTIVE = true;

                    if (txtLINK2DIRECT.Text == "")
                    {
                        objoddtl.LINK2DIRECT = "";
                    }
                    else
                    {
                        objoddtl.LINK2DIRECT = txtLINK2DIRECT.Text;
                    }
                    if (tags_3.Text == "")
                    {
                        objoddtl.keywords = "";
                    }
                    else
                    {
                        objoddtl.keywords = tags_3.Text;
                    }
                    if (txtboxshot.Text == "")
                    {
                        objoddtl.boxshot = "";
                    }
                    else
                    {
                        objoddtl.boxshot = txtboxshot.Text;
                    }
                    if (txtlarge_boxshot.Text == "")
                    {
                        objoddtl.large_boxshot = "";
                    }
                    else
                    {
                        objoddtl.large_boxshot = txtlarge_boxshot.Text;
                    }
                    if (txtmedium_boxshot.Text == "")
                    {
                        objoddtl.medium_boxshot = "";
                    }
                    else
                    {
                        objoddtl.medium_boxshot = txtmedium_boxshot.Text;
                    }
                    if (txtsmall_boxshot.Text == "")
                    {
                        objoddtl.small_boxshot = "";
                    }
                    else
                    {
                        objoddtl.small_boxshot = txtsmall_boxshot.Text;
                    }
                    if (txtos_platform.Text == "")
                    {
                        objoddtl.os_platform = "";
                    }
                    else
                    {
                        objoddtl.os_platform = txtos_platform.Text;
                    }
                    if (txtcorp_logo.Text == "")
                    {
                        objoddtl.corp_logo = "";
                    }
                    else
                    {
                        objoddtl.corp_logo = txtcorp_logo.Text;
                    }
                    if (txtline.Text == "")
                    {
                        objoddtl.link = "";
                    }
                    else
                    {
                        objoddtl.link = txtline.Text;
                    }
                    if (txttrial_url.Text == "")
                    {
                        objoddtl.trial_url = "";
                    }
                    else
                    {
                        objoddtl.trial_url = txttrial_url.Text;
                    }
                    if (txtcart_link.Text == "")
                    {
                        objoddtl.cart_link = "";
                    }
                    else
                    {
                        objoddtl.cart_link = txtcart_link.Text;
                    }
                    if (txtproduct_detail_link.Text == "")
                    {
                        objoddtl.product_detail_link = "";
                    }
                    else
                    {
                        objoddtl.product_detail_link = txtproduct_detail_link.Text;
                    }
                    if (txtlead.Text == "")
                    {
                        objoddtl.lead = "";
                    }
                    else
                    {
                        objoddtl.lead = txtlead.Text;
                    }
                    if (txtother.Text == "")
                    {
                        objoddtl.other = "";
                    }
                    else
                    {
                        objoddtl.other = txtother.Text;
                    }
                    if (txtpromotion_type.Text == "")
                    {
                        objoddtl.promotion_type = "";
                    }
                    else
                    {
                        objoddtl.promotion_type = txtpromotion_type.Text;
                    }
                    if (txtpayout.Text == "")
                    {
                        objoddtl.payout = "";
                    }
                    else
                    {
                        objoddtl.payout = txtpayout.Text;
                    }
                    if (txtlading_page.Text == "")
                    {
                        objoddtl.lading_page = "";
                    }
                    else
                    {
                        objoddtl.lading_page = txtlading_page.Text;
                    }
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODDTL");
                    objEco_ERP_WEB_USER_MST.IsCache = true;
                    objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                    DB.SaveChanges();

                    ViewState["Product"] = null;
                    //clear();
                    //LastData();
                    ReadOnly();
                    BtnMstProSave.Text = "Submit";
                    Button3.Text = "Submit";
                }
                else
                {
                    var existBar = DB.Eco_TBLPRODUCT.Where(c => c.BarCode == txtProBar.Text || c.UserProdID == txtProBar.Text || c.AlternateCode1 == txtACode1.Text || c.AlternateCode2 == txtACode1.Text);
                    if (existBar.Count() > 0)
                    {
                        string display = "This Product Allready Use On Barcode OR Alternatecode!";
                        panelBarcod.Visible = true;
                        lblBarcodedublicat.Text = display;
                        //ClientScript.RegisterStartupScript(this.GetType(), "This Product All Ready This Bissess Are Used!", "alert('" + display + "');", true);
                        return;
                    }
                    else
                    {
                        int MYPODID = Convert.ToInt32(lblmstproid.Text);
                        Database.Eco_TBLPRODUCT objproduct = new Database.Eco_TBLPRODUCT();
                        objproduct.TenantID = TID;
                        objproduct.LOCATION_ID = LID;
                        objproduct.MYPRODID = MYPODID;
                        objproduct.UserProdID = txtProBar.Text;
                        objproduct.BarCode = txtProBar.Text;
                        objproduct.AlternateCode1 = txtACode1.Text;
                        objproduct.AlternateCode2 = txtACode1.Text;
                        objproduct.UOM = Convert.ToInt32(drpUOM.SelectedValue);
                        objproduct.GROUPID = Convert.ToInt32(ddlItGrp.SelectedValue);
                        objproduct.SIZECODE = Convert.ToInt32(ddlsize.SelectedValue);
                        objproduct.ProdTypeRefId = Convert.ToInt32(ddlProdType.SelectedValue);
                        objproduct.SupplyMethodID = Convert.ToInt32(ddlMoreItenSupplyMethod.SelectedValue);
                        objproduct.ProdMethodID = Convert.ToInt32(ddlMoreItenProMethod.SelectedValue);
                        objproduct.SalDeptID = Convert.ToInt32(ddlDofSale.SelectedValue);
                        objproduct.ShortName = txtShortName.Text;
                        objproduct.DevloperActiv = false;
                        objproduct.COMPANYID = UID;
                        objproduct.Appove = false;
                        if (txtQtnonHand.Text == "")
                        {
                            objproduct.QTYINHAND = 0;
                        }
                        else
                        {
                            objproduct.QTYINHAND = Convert.ToInt32(txtQtnonHand.Text);
                        }
                        objproduct.ProdName1 = pm_txtProductName.Text;
                        if (txtItemRelSubProName2.Text != null)
                        {
                            objproduct.ProdName2 = txtItemRelSubProName2.Text;
                        }
                        else
                        {
                            objproduct.ProdName2 = pm_txtProductName.Text;
                        }
                        if (txtItemRelSubProName3.Text != null)
                        {
                            objproduct.ProdName3 = txtItemRelSubProName3.Text;
                        }
                        else
                        {
                            objproduct.ProdName3 = pm_txtProductName.Text;
                        }
                        if (txtDescToprint.Text != null)
                        {
                            objproduct.DescToprint = txtDescToprint.Text;
                        }
                        else
                        {
                            objproduct.DescToprint = pm_txtProductName.Text;
                        }

                        if (ddlBrandname.SelectedValue == "0")
                        {
                            objproduct.Brand = "8881";
                        }
                        else
                            objproduct.Brand = ddlBrandname.SelectedValue;
                        objproduct.REMARKS = "";
                        objproduct.keywords = tags_4.Text;
                        if (ddlSuppCat.SelectedValue == "0")
                            objproduct.MainCategoryID = null;
                        else
                            objproduct.MainCategoryID = Convert.ToInt32(ddlSuppCat.SelectedValue);
                        if (txtnotepricostprice.Text == "")
                            objproduct.basecost = 0;
                        else
                            objproduct.basecost = Convert.ToDecimal(txtnotepricostprice.Text);
                        if (txtnoteprisaleori1.Text == "")
                            objproduct.onlinesale1 = 0;
                        else
                            objproduct.onlinesale1 = Convert.ToDecimal(txtnoteprisaleori1.Text);

                        if (txtnoteprisaleori2.Text == "")
                            objproduct.onlinesale2 = 0;
                        else
                            objproduct.onlinesale2 = Convert.ToDecimal(txtnoteprisaleori2.Text);

                        if (txtnoteprimrp.Text == "")
                            objproduct.msrp = 0;
                        else
                            objproduct.msrp = Convert.ToDecimal(txtnoteprimrp.Text);

                        if (txtnotepriwebsalepri.Text == "")
                            objproduct.price = 0;
                        else
                            objproduct.price = Convert.ToDecimal(txtnotepriwebsalepri.Text);

                        objproduct.currency = drpCurrency.SelectedValue;

                        //if (txtINFOQntyonhand.Text == "")
                        //    objproduct.QTYINHAND = null;
                        //else
                        //    objproduct.QTYINHAND = Convert.ToInt32(txtINFOQntyonhand.Text);

                        if (txtINFOQntyRes.Text == "")
                            objproduct.QTYRCVD = 0;
                        else
                            objproduct.QTYRCVD = Convert.ToInt32(txtINFOQntyRes.Text);

                        if (txtINFOQntySold.Text == "")
                            objproduct.QTYSOLD = 0;
                        else
                            objproduct.QTYSOLD = Convert.ToInt32(txtINFOQntySold.Text);

                        objproduct.PICTURE = null;
                        objproduct.ACTIVE = "1";
                        objproduct.DIRECTSALE = "1";
                        //  objproduct.LINK2DIRECT = txtMoreItenwarrantyLink2d.Text;
                        objproduct.DISPDATE3 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                        if (txtWarranty.Text == null)
                        {
                            objproduct.Warranty = "";
                        }
                        else
                        {
                            objproduct.Warranty = txtWarranty.Text;
                        }
                        if (txtINFOlastpurchdt.Text == "")
                            objproduct.LASTPURDATE = DateTime.Now;
                        else
                            objproduct.LASTPURDATE = Convert.ToDateTime(txtINFOlastpurchdt.Text);
                        if (txtINFOlastsaledt.Text == "")
                            objproduct.LASTSALDATE = DateTime.Now;
                        else
                            objproduct.LASTSALDATE = Convert.ToDateTime(txtINFOlastsaledt.Text);
                        objproduct.COLORID = dropColoerOne.SelectedValue;
                        if (txtnoteprisaleori3.Text == "")
                        {
                            objproduct.onlinesale3 = 0;
                        }
                        else
                        {
                            objproduct.onlinesale3 = Convert.ToDecimal(txtnoteprisaleori3.Text);
                        }
                        if (txtnoteprinote.Text == "")
                        {
                            objproduct.InternalNotes = "";
                        }
                        else
                        {
                            objproduct.InternalNotes = txtnoteprinote.Text;
                        }
                        objproduct.Serialized = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenSerItem.SelectedValue));
                        objproduct.HotItem = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenHotItem.SelectedValue));
                        objproduct.SKUProduction = Convert.ToBoolean(Convert.ToInt32(rbtnSKUP.SelectedValue));
                        objproduct.SKU = null;
                        objproduct.MultiSize = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenMultisize.SelectedValue));
                        if (RadioBtnMoreItenMultisize.SelectedValue == "1")
                        {
                            string[] Seperate5 = txtMultiSize.Text.Split('/');
                            int count5 = 0;
                            string Sep5 = "";
                            for (int i = 0; i <= Seperate5.Count() - 1; i++)
                            {
                                Sep5 = Seperate5[i];

                                count5++;
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiSize";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = MYPODID;
                                obj.RunSerial = count5;
                                obj.Recource = 5008;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate5[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST.IsCache = true;
                                objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }

                        }
                        objproduct.MultiUOM = Convert.ToBoolean(Convert.ToInt32(rdbtnMUOM.SelectedValue));
                        if (rdbtnMUOM.SelectedValue == "1")
                        {
                            string[] Seperate1 = txtmultiMOU.Text.Split('/');
                            int count1 = 0;
                            string Sep1 = "";
                            for (int i = 0; i <= Seperate1.Count() - 1; i++)
                            {
                                Sep1 = Seperate1[i];

                                count1++;
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiUOM";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = MYPODID;
                                obj.RunSerial = count1;
                                obj.Recource = 5009;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST.IsCache = true;
                                objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                        objproduct.MultiColor = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenMulticolor.SelectedValue));
                        if (RadioBtnMoreItenMulticolor.SelectedValue == "1")
                        {
                            string[] Seperate4 = txtMulticolers.Text.Split('/');
                            int count4 = 0;
                            string Sep4 = "";
                            for (int i = 0; i <= Seperate4.Count() - 1; i++)
                            {
                                Sep4 = Seperate4[i];

                                count4++;
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiColor";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = MYPODID;
                                obj.RunSerial = count4;
                                obj.Recource = 5007;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate4[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST.IsCache = true;
                                objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }


                        }
                        objproduct.MultiBinStore = Convert.ToBoolean(Convert.ToInt32(rbtnMBSto.SelectedValue));
                        objproduct.Perishable = Convert.ToBoolean(Convert.ToInt32(rbtPsle.SelectedValue));
                        objproduct.SaleAllow = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenSellAllow.SelectedValue));
                        objproduct.PurAllow = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenPurchAllow.SelectedValue));
                        DB.Eco_TBLPRODUCT.AddObject(objproduct);
                        DB.SaveChanges();
                        Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODUCT");
                        objEco_ERP_WEB_USER_MST1.IsCache = true;
                        objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                        DB.SaveChanges();

                        Database.Eco_TBLPRODDTL objoddtl = new Database.Eco_TBLPRODDTL();
                        objoddtl.TenantID = TID;
                        objoddtl.MYPRODID = Convert.ToInt32(ProSaleFenProid.Text);
                        if (txtProSaleFenRemark.Text == "")
                        {
                            objoddtl.REMARKS = "";
                        }
                        else
                        {
                            objoddtl.REMARKS = txtProSaleFenRemark.Text;
                        }

                        objoddtl.ACTIVE = true;

                        if (txtLINK2DIRECT.Text == "")
                        {
                            objoddtl.LINK2DIRECT = "";
                        }
                        else
                        {
                            objoddtl.LINK2DIRECT = txtLINK2DIRECT.Text;
                        }
                        if (tags_3.Text == "")
                        {
                            objoddtl.keywords = "";
                        }
                        else
                        {
                            objoddtl.keywords = tags_3.Text;
                        }
                        if (txtboxshot.Text == "")
                            objoddtl.boxshot = "";
                        else
                            objoddtl.boxshot = txtboxshot.Text;
                        if (txtlarge_boxshot.Text == "")
                            objoddtl.large_boxshot = "";
                        else
                            objoddtl.large_boxshot = txtlarge_boxshot.Text;
                        if (txtmedium_boxshot.Text == "")
                            objoddtl.medium_boxshot = "";
                        else
                            objoddtl.medium_boxshot = txtmedium_boxshot.Text;
                        if (txtsmall_boxshot.Text == "")
                            objoddtl.small_boxshot = "";
                        else
                            objoddtl.small_boxshot = txtsmall_boxshot.Text;
                        if (txtos_platform.Text == "")
                            objoddtl.os_platform = "";
                        else
                            objoddtl.os_platform = txtos_platform.Text;
                        if (txtcorp_logo.Text == "")
                            objoddtl.corp_logo = "";
                        else
                            objoddtl.corp_logo = txtcorp_logo.Text;
                        if (txtline.Text == "")
                            objoddtl.link = "";
                        else
                            objoddtl.link = txtline.Text;
                        if (txttrial_url.Text == "")
                            objoddtl.trial_url = "";
                        else
                            objoddtl.trial_url = txttrial_url.Text;
                        if (txtcart_link.Text == "")
                            objoddtl.cart_link = "";
                        else
                            objoddtl.cart_link = txtcart_link.Text;
                        if (txtproduct_detail_link.Text == "")
                            objoddtl.product_detail_link = "";
                        else
                            objoddtl.product_detail_link = txtproduct_detail_link.Text;
                        if (txtlead.Text == "")
                            objoddtl.lead = "";
                        else
                            objoddtl.lead = txtlead.Text;
                        if (txtother.Text == "")
                            objoddtl.other = "";
                        else
                            objoddtl.other = txtother.Text;
                        if (txtpromotion_type.Text == "")
                            objoddtl.promotion_type = "";
                        else
                            objoddtl.promotion_type = txtpromotion_type.Text;
                        if (txtpayout.Text == "")
                            objoddtl.payout = "";
                        else
                            objoddtl.payout = txtpayout.Text;
                        if (txtlading_page.Text == "")
                            objoddtl.lading_page = "";
                        else
                            objoddtl.lading_page = txtlading_page.Text;
                        DB.Eco_TBLPRODDTL.AddObject(objoddtl);
                        DB.SaveChanges();
                        Eco_Cache_Mst objEco_ERP_WEB_USER_MST12 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODDTL");
                        objEco_ERP_WEB_USER_MST12.IsCache = true;
                        objEco_ERP_WEB_USER_MST12.DateAndTime = DateTime.Now;
                        DB.SaveChanges();
                        if (ddlSuppCat.SelectedValue != "0")
                        {
                            int cai = Convert.ToInt32(ddlSuppCat.SelectedValue);
                            string CatName = DB.Eco_CAT_MST.Single(p => p.CATID == cai).CAT_NAME1;

                            int prentid = DB.Eco_CAT_MST.Single(p => p.CATID == cai).PARENT_CATID;
                            if (prentid == 0)
                            {
                                string PrenName = CatName;
                                ViewState["PrenName"] = PrenName;

                            }
                            else
                            {
                                string PrenName = DB.Eco_CAT_MST.Single(p => p.CATID == prentid).CAT_NAME1;
                                ViewState["PrenName"] = PrenName;
                            }
                            string PrentName = ViewState["PrenName"].ToString();
                            Database.Eco_Product_Cat_Mst objProduct_Cat_Mst = new Database.Eco_Product_Cat_Mst();
                            objProduct_Cat_Mst.MYPRODID = Convert.ToInt32(ProSaleFenProid.Text);
                            objProduct_Cat_Mst.TenantID = TID;
                            objProduct_Cat_Mst.PARENT_CATID = prentid;
                            objProduct_Cat_Mst.CATID = cai;
                            objProduct_Cat_Mst.DefaultProduct = "Y";
                            objProduct_Cat_Mst.Active = "1";
                            objProduct_Cat_Mst.CATID_name = CatName;
                            objProduct_Cat_Mst.PARENT_CATIDname = PrentName;
                            objProduct_Cat_Mst.Remarks = objproduct.REMARKS;
                            objProduct_Cat_Mst.BarCode = objproduct.BarCode;
                            DB.Eco_Product_Cat_Mst.AddObject(objProduct_Cat_Mst);
                            DB.SaveChanges();
                            Eco_Cache_Mst objEco_ERP_WEB_USER_MST123 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Product_Cat_Mst");
                            objEco_ERP_WEB_USER_MST123.IsCache = true;
                            objEco_ERP_WEB_USER_MST123.DateAndTime = DateTime.Now;
                            DB.SaveChanges();
                            ViewState["PrenName"] = null;
                        }
                        else
                        {
                            string display = "Select Main Category!";
                            ClientScript.RegisterStartupScript(this.GetType(), "Select Main Category!", "alert('" + display + "');", true);
                            return;
                        }
                    }
                    //clear();
                    //LastData();
                    ReadOnly();

                    //pMasterGridview.DataSource = DB.Eco_TBLPRODUCT.Where(p => p.TenantID == 1 && p.ACTIVE == "1").OrderByDescending(p => p.MYPRODID);
                    //pMasterGridview.DataBind();
                }
                BindpMaster();
                //ListProdcuListPanel.Attributes.CssStyle.Add("display", "block");
                //ListPAnel.Attributes.Add("Class", "collapse");
                //   clearSessionSession();
                hidcheck.Value = "";
                hidTenantId.Value = "";
                hidMyProdid.Value = "";
                //GridBussPro12.DataSource = null;
                //GridBussPro12.DataBind();
                // DDLBussPro.SelectedIndex = 0;
                //Button2.Visible = true;
                //BtnMstProSave.Text = "Save";

                //  
                scope.Complete(); //  To commit.

            }
        }
       
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            AddNew();

        }
        public void AddNew()
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            lblProductmode.Text = lblBusinessMode.Text = lblBusinesslistmode.Text = lblMoreItemMode.Text = lblInterMode.Text = "Add Mode";
            BtnMstProSave.Visible = true;
            btnDiscard.Visible = true;
            Button3.Visible = true;
            Button4.Visible = true;
            Button5.Visible = false;
            Button6.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;
           
            // linkButtonWebProd.Visible = false;
            //  linktoprodMaster.Visible = false;
            Write();
            lblpronamemain.Text = "";
            GridBussPro12.DataSource = null;
            GridBussPro12.DataBind();
            clear();
            BindpMaster();
            ddlMoreItenSupplyMethod.SelectedValue = DB.Eco_REFTABLE.Single(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "SUPPM" && p.REFTYPE == "SUPPM" && p.REFNAME1 == "Buy").REFID.ToString();
            ddlMoreItenProMethod.SelectedValue = DB.Eco_REFTABLE.Single(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "PRODM" && p.REFTYPE == "PRODM" && p.REFNAME1 == "Make 2 Order").REFID.ToString();
            ddlProdType.SelectedValue = DB.Eco_REFTABLE.Single(p => p.ACTIVE == "Y" && p.REFTYPE == "PRODTYPE" && p.REFSUBTYPE == "PRODTYPE" && p.REFNAME1 == "Stockable").REFID.ToString();
            bool AddNew = true;
            ViewState["Addnew"] = AddNew;
            ddlSuppCat.Enabled = false;
        }
        protected void lbApproveIss_Click(object sender, EventArgs e)
        {

            Response.Redirect("AdvanshSearch.aspx");
        }
        protected void pMasterGridview_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                bool TrueValew = Convert.ToBoolean(ViewState["Addnew"]);
                if (TrueValew == true)
                {
                    string display = "Save And Cancel The Product!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Save And Cancel The Product!", "alert('" + display + "');", true);
                    return;
                }
                else
                {
                    if (e.CommandName == "btnedit")
                    {
                        FrmView.Visible = true;
                        btnupcuntinu.Visible = true;
                        Write();
                        //ListProdcuListPanel.Attributes.CssStyle.Add("display", "none");
                        //ListPAnel.Attributes.Add("Class", "expand");
                        lblProductmode.Text = lblBusinessMode.Text = lblBusinesslistmode.Text = lblMoreItemMode.Text = lblInterMode.Text = "Edit Mode";
                        int ID = Convert.ToInt32(e.CommandArgument);
                        ViewState["Product"] = ID;
                        EditCamm(ID);

                        scope.Complete(); //  To commit.
                    }
                    if (e.CommandName == "btnDelet")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.Eco_TBLPRODUCT objoddtl = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == ID);
                        objoddtl.ACTIVE = "N";
                        DB.SaveChanges();
                        Eco_Cache_Mst objEco_ERP_WEB_USER_MST123 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODUCT");
                        objEco_ERP_WEB_USER_MST123.IsCache = true;
                        objEco_ERP_WEB_USER_MST123.DateAndTime = DateTime.Now;
                        DB.SaveChanges();
                        bindBusProGrid();
                    }
                }
            }

        }
        public void EditCamm(int ID)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            lblmstproid.Text = ID.ToString();
            ddlSuppCat.Enabled = false;
            if (DB.Eco_TBLPRODDTL.Where(p => p.MYPRODID == ID).Count() > 0)
            {
                Database.Eco_TBLPRODDTL objoddtl = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == ID);
                ProSaleFenProid.Text = objoddtl.MYPRODID.ToString();
                txtProSaleFenRemark.Text = objoddtl.REMARKS.ToString();
                tags_3.Text = objoddtl.keywords.ToString();
                txtLINK2DIRECT.Text = objoddtl.LINK2DIRECT.ToString();
                //   tags_3.Text = objoddtl.keywords.ToString();
                txtboxshot.Text = objoddtl.boxshot.ToString();
                txtlarge_boxshot.Text = objoddtl.large_boxshot.ToString();
                txtmedium_boxshot.Text = objoddtl.medium_boxshot.ToString();
                txtsmall_boxshot.Text = objoddtl.small_boxshot.ToString();
                txtos_platform.Text = objoddtl.os_platform.ToString();
                txtcorp_logo.Text = objoddtl.corp_logo.ToString();
                txtline.Text = objoddtl.link.ToString();
                txttrial_url.Text = objoddtl.trial_url.ToString();
                txtcart_link.Text = objoddtl.cart_link.ToString();
                txtproduct_detail_link.Text = objoddtl.product_detail_link.ToString();
                txtlead.Text = objoddtl.lead.ToString();
                txtother.Text = objoddtl.other.ToString();
                txtpromotion_type.Text = objoddtl.promotion_type.ToString();
                txtpayout.Text = objoddtl.payout.ToString();
                txtlading_page.Text = objoddtl.lading_page.ToString();
            }
            else
            {
                Database.Eco_TBLPRODDTL objTBLPRODDTL = new Database.Eco_TBLPRODDTL();
                objTBLPRODDTL.MYPRODID = ID;
                objTBLPRODDTL.REMARKS = "";
                objTBLPRODDTL.LINK2DIRECT = "";
                objTBLPRODDTL.boxshot = "";
                objTBLPRODDTL.large_boxshot = "";
                objTBLPRODDTL.medium_boxshot = "";
                objTBLPRODDTL.small_boxshot = "";
                objTBLPRODDTL.os_platform = "";
                objTBLPRODDTL.corp_logo = "";
                objTBLPRODDTL.keywords = "";
                objTBLPRODDTL.link = "";
                objTBLPRODDTL.trial_url = "";
                objTBLPRODDTL.cart_link = "";
                objTBLPRODDTL.product_detail_link = "";
                objTBLPRODDTL.lead = "";
                objTBLPRODDTL.other = "";
                objTBLPRODDTL.promotion_type = "";
                objTBLPRODDTL.payout = "";
                objTBLPRODDTL.lading_page = "";
                objTBLPRODDTL.ACTIVE = true;
                objTBLPRODDTL.TenantID = TID;
                DB.Eco_TBLPRODDTL.AddObject(objTBLPRODDTL);
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST123 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODDTL");
                objEco_ERP_WEB_USER_MST123.IsCache = true;
                objEco_ERP_WEB_USER_MST123.DateAndTime = DateTime.Now;
                DB.SaveChanges();
            }

            Database.Eco_TBLPRODUCT objproduct = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == ID);
            dropColoerOne.SelectedValue = objproduct.COLORID.ToString();
            if (DB.Eco_Product_Cat_Mst.Where(p => p.MYPRODID == ID && p.DefaultProduct == "Y").Count() > 0)
            {
                int maincat = DB.Eco_Product_Cat_Mst.Single(p => p.MYPRODID == ID && p.DefaultProduct == "Y").CATID;
                ddlSuppCat.SelectedValue = maincat.ToString();
            }
            else
            {
                if (objproduct.MainCategoryID == 0 || objproduct.MainCategoryID == 0)
                { }
                else
                {
                    ddlSuppCat.SelectedValue = objproduct.MainCategoryID.ToString();
                }
            }
            if (objproduct.QTYINHAND == 0)
            {

            }
            else
            {
                txtQtnonHand.Text = objproduct.QTYINHAND.ToString();
            }
            if (objproduct.InternalNotes == "")
            {

            }
            else
            {
                txtnoteprinote.Text = objproduct.InternalNotes.ToString();
            }
            if (objproduct.onlinesale3 == 0)
            {

            }
            else
            {
                txtnoteprisaleori3.Text = objproduct.onlinesale3.ToString();
            }
            if (objproduct.keywords == "")
            { }
            else
            {
                tags_4.Text = objproduct.keywords.ToString();
            }
            if (objproduct.BarCode == "")
            { }
            else
                txtProBar.Text = objproduct.BarCode.ToString();
            if (objproduct.ProdName1 == "")
            {

            }
            else
            {
                pm_txtProductName.Text = objproduct.ProdName1.ToString();
            }
            if (objproduct.AlternateCode1 == "")
            { }
            else
            {
                txtACode1.Text = objproduct.AlternateCode1.ToString();
            }
            if (objproduct.AlternateCode2 == "")
            { }
            else
            {
                txtACode2.Text = objproduct.AlternateCode2.ToString();
            }
            ddlProdType.SelectedValue = objproduct.ProdTypeRefId.ToString();
            ddlItGrp.SelectedValue = objproduct.GROUPID.ToString();
            ddlsize.SelectedValue = objproduct.SIZECODE.ToString();
            ddlMoreItenSupplyMethod.SelectedValue = objproduct.SupplyMethodID.ToString();
            ddlMoreItenProMethod.SelectedValue = objproduct.ProdMethodID.ToString();
            ddlDofSale.SelectedValue = objproduct.SalDeptID.ToString();
            drpUOM.SelectedValue = objproduct.UOM.ToString();
            ddlBrandname.SelectedValue = objproduct.Brand.ToString();
            //tags_3.Text = objproduct.keywords.ToString();
            if (objproduct.Warranty == "")
            { }
            else
            {
                txtWarranty.Text = objproduct.Warranty.ToString();
            }
            if (objproduct.ShortName == "")
            { }
            else
            {
                txtShortName.Text = objproduct.ShortName.ToString();
            }
            if (objproduct.DescToprint == "")
            { }
            else
            {
                txtDescToprint.Text = objproduct.DescToprint.ToString();
            }
            if (objproduct.ProdName2 == "")
            { }
            else
            {
                txtItemRelSubProName2.Text = objproduct.ProdName2.ToString();
            }
            if (objproduct.ProdName3 == "")
            { }
            else
            {
                txtItemRelSubProName3.Text = objproduct.ProdName3.ToString();
            }

            if (objproduct.basecost == 0)
            { }
            else
            {
                txtnotepricostprice.Text = objproduct.basecost.ToString();
            }
            if (objproduct.onlinesale1 == 0)
            { }
            else
            {
                txtnoteprisaleori1.Text = objproduct.onlinesale1.ToString();
            }
            if (objproduct.onlinesale2 == 0)
            { }
            else
            {
                txtnoteprisaleori2.Text = objproduct.onlinesale2.ToString();
            }
            if (objproduct.msrp == 0)
            { }
            else
            {
                txtnoteprimrp.Text = objproduct.msrp.ToString();
            }
            if (objproduct.price == 0)
            { }
            else
            {
                txtnotepriwebsalepri.Text = objproduct.price.ToString();
            }

            if (objproduct.currency.Trim() == null || objproduct.currency.Trim() == "" || objproduct.currency.Trim() == "0" || objproduct.currency.Trim() == "$")
            { }
            else
            {
                drpCurrency.SelectedValue = objproduct.currency.ToString();
            }

            if (objproduct.Serialized == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenHotItem.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenHotItem.SelectedValue = "1";
            }
            if (objproduct.SKUProduction == Convert.ToBoolean(0))
            {
                rbtnSKUP.SelectedValue = "0";
            }
            else
            {
                rbtnSKUP.SelectedValue = "1";
            }
            if (objproduct.MultiSize == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenMultisize.SelectedValue = "0";
                panelMultisize.Visible = false;
                dorpMultiSize.Visible = false;
            }
            else
            {
                RadioBtnMoreItenMultisize.SelectedValue = "1";
                dorpMultiSize.Visible = true;
                panelMultisize.Visible = true;
                dorpMultiSize.DataSource = DB.Eco_TBLSIZE.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(p => p.SIZEDESC1);
                dorpMultiSize.DataTextField = "SIZETYPE";
                dorpMultiSize.DataValueField = "SIZECODE";
                dorpMultiSize.DataBind();
                dorpMultiSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Size--", "0"));
                string multisize = "";
                var ListSize = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == ID && p.Active == true && p.RecordType == "MultiSize").ToList();
                foreach (Eco_Tbl_Multi_Color_Size_Mst item in ListSize)
                {
                    if (multisize == "")
                    {
                        multisize += item.RecValue;
                    }
                    else
                    {
                        multisize += "/" + item.RecValue;
                    }

                }
                txtMultiSize.Text = multisize.ToString();
                ViewState["StrMultiSize"] = multisize;
            }
            if (objproduct.MultiColor == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenMulticolor.SelectedValue = "0";
                dropMulticolor.Visible = false;
                pnelmulticooler.Visible = false;
            }
            else
            {
                RadioBtnMoreItenMulticolor.SelectedValue = "1";
                pnelmulticooler.Visible = true;
                dropMulticolor.Visible = true;
                dropMulticolor.DataSource = DB.Eco_TBLCOLOR.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(p => p.COLORDESC1);
                dropMulticolor.DataTextField = "COLORDESC1";
                dropMulticolor.DataValueField = "COLORID";
                dropMulticolor.DataBind();
                dropMulticolor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Colors--", "0"));
                string Multicolo = "";
                var LIst = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == ID && p.Active == true && p.RecordType == "MultiColor").ToList();
                foreach (Eco_Tbl_Multi_Color_Size_Mst item in LIst)
                {
                    if (Multicolo == "")
                    {
                        Multicolo += item.RecValue;
                    }
                    else
                    {
                        Multicolo += "/" + item.RecValue;
                    }

                }
                txtMulticolers.Text = Multicolo.ToString();
                ViewState["strMulticolor"] = Multicolo;


            }
            if (objproduct.MultiUOM == Convert.ToBoolean(0))
            {
                rdbtnMUOM.SelectedValue = "0";
                ddluom.Visible = false;
                panelMultiUOM.Visible = false;
            }
            else
            {
                rdbtnMUOM.SelectedValue = "1";
                ddluom.Visible = true;
                panelMultiUOM.Visible = true;
                ddluom.DataSource = DB.Eco_ICUOM.Where(p => p.TenantID == TID && p.Active == "Y");
                ddluom.DataTextField = "UOMNAME1";
                ddluom.DataValueField = "UOM";
                ddluom.DataBind();
                ddluom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Unit of Measure--", "0"));
                string multiUOM = "";
                var ListUOM = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == ID && p.Active == true && p.RecordType == "MultiUOM").ToList();
                foreach (Eco_Tbl_Multi_Color_Size_Mst item in ListUOM)
                {
                    if (multiUOM == "")
                    {
                        multiUOM += item.RecValue;
                    }
                    else
                    {
                        multiUOM += "/" + item.RecValue;
                    }

                }
                txtmultiMOU.Text = multiUOM.ToString();
                ViewState["StrMultiMOU"] = multiUOM;
            }

            if (objproduct.MultiBinStore == Convert.ToBoolean(0))
            {
                rbtnMBSto.SelectedValue = "0";
            }
            else
            {
                rbtnMBSto.SelectedValue = "1";
            }
            if (objproduct.Perishable == Convert.ToBoolean(0))
            {
                rbtPsle.SelectedValue = "0";
            }
            else
            {
                rbtPsle.SelectedValue = "1";
            }
            if (objproduct.SaleAllow == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenSellAllow.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenSellAllow.SelectedValue = "1";
            }
            if (objproduct.PurAllow == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenPurchAllow.SelectedValue = "0";
            }
            else
            {
                RadioBtnMoreItenPurchAllow.SelectedValue = "1";
            }
            if (objproduct.Serialized == Convert.ToBoolean(0))
            {
                RadioBtnMoreItenSerItem.SelectedValue = "1";
            }
            else
            {
                RadioBtnMoreItenSerItem.SelectedValue = "0";
            }

            CatEdit.Visible = true;
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = true;
            BtnMstProSave.Visible = true;
            Button4.Visible = true;
            btnDiscard.Visible = true;
            Button5.Visible = false;
            Button6.Visible = false;
            Button3.Text = "Update";
            BtnMstProSave.Text = "Update";
            bindBusProGrid();
            ViewState["Mode"] = "EditMode";

        }
        protected void GridBussPro12_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            if (e.CommandName == "btnDelete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Database.Eco_tblBusProd objtblBusProd = DB.Eco_tblBusProd.Single(p => p.MYID == id);
                objtblBusProd.ACTIVE = false;
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST123 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblBusProd");
                objEco_ERP_WEB_USER_MST123.IsCache = true;
                objEco_ERP_WEB_USER_MST123.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                int disid = Convert.ToInt32(objtblBusProd.DISPLAY_ID);
                if (DB.Eco_ICProdDisplay.Where(p => p.Display_Id == disid && p.TableName == "tblBusProd").Count() == 1)
                {
                    Database.Eco_ICProdDisplay objICProdDisplay = DB.Eco_ICProdDisplay.Single(p => p.Display_Id == disid);
                    objICProdDisplay.ACTIVE2 = false;
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST12 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                    objEco_ERP_WEB_USER_MST12.IsCache = true;
                    objEco_ERP_WEB_USER_MST12.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }
                bindBusProGrid();

            }
            if (e.CommandName == "linkAction")
            {

                int id = Convert.ToInt32(e.CommandArgument);
                bool Onlinsal = Convert.ToBoolean(DB.Eco_tblBusProd.Single(p => p.MYID == id).ONLINESALEALLOW);
                int REFID = DB.Eco_tblBusProd.Single(p => p.MYID == id).REFID;
                string REFTYPE = DB.Eco_tblBusProd.Single(p => p.MYID == id).REFTYPE;
                string REFSUBTYPE = DB.Eco_tblBusProd.Single(p => p.MYID == id).REFSUBTYPE;
                if (Onlinsal == true)
                {
                    Database.Eco_tblBusProd objtblBusProd = DB.Eco_tblBusProd.Single(p => p.MYID == id);
                    int myprodid = Convert.ToInt32(objtblBusProd.MYPRODID);
                    objtblBusProd.ONLINESALEALLOW = false;
                    Database.Eco_tblWebProd objtblWebProd = DB.Eco_tblWebProd.Single(p => p.MYPRODID == myprodid && p.REFID == REFID);
                    objtblWebProd.ACTIVE = false;
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST12 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblWebProd");
                    objEco_ERP_WEB_USER_MST12.IsCache = true;
                    objEco_ERP_WEB_USER_MST12.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }
                else
                {
                    Database.Eco_tblBusProd objtblBusProd = DB.Eco_tblBusProd.Single(p => p.MYID == id);
                    int myprodid = Convert.ToInt32(objtblBusProd.MYPRODID);
                    objtblBusProd.ONLINESALEALLOW = true;
                    Database.Eco_tblWebProd objtblWebProd = new Database.Eco_tblWebProd();
                    objtblWebProd.TenantID = TID;
                    objtblWebProd.MYPRODID = myprodid;
                    objtblWebProd.REFID = REFID;
                    objtblWebProd.REFTYPE = REFTYPE;
                    objtblWebProd.REFSUBTYPE = REFSUBTYPE;
                    objtblWebProd.MYID = DB.Eco_tblWebProd.Count() > 0 ? Convert.ToInt32(DB.Eco_tblWebProd.Max(p => p.MYID) + 1) : 1;
                    // objtblWebProd.DISPLAY_ID = DB.ICProdDisplays.Count() > 0 ? Convert.ToInt32(DB.ICProdDisplays.Max(p => p.Display_Id) + 1) : 1;
                    objtblWebProd.ACTIVE = true;
                    DB.Eco_tblWebProd.AddObject(objtblWebProd);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST12 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblBusProd");
                    objEco_ERP_WEB_USER_MST12.IsCache = true;
                    objEco_ERP_WEB_USER_MST12.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    Database.Eco_tblOfferProd objtblOfferProd = new Database.Eco_tblOfferProd();
                    objtblOfferProd.MYID = DB.Eco_tblOfferProd.Count() > 0 ? Convert.ToInt32(DB.Eco_tblOfferProd.Max(p => p.MYID) + 1) : 1;
                    objtblOfferProd.MYPRODID = myprodid;
                    objtblOfferProd.TenantID = TID;
                    objtblOfferProd.REFID = REFID;
                    objtblOfferProd.REFTYPE = REFTYPE;
                    objtblOfferProd.REFSUBTYPE = REFSUBTYPE;
                    DB.Eco_tblOfferProd.AddObject(objtblOfferProd);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblOfferProd");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                 //   linkButtonWebProd.Visible = true;
                  //  linktoprodMaster.Visible = true;
                }
                bindBusProGrid();

            }


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("Adminindex.aspx");
        }
        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            clear();
            Response.Redirect("Adminindex.aspx");
        }

        protected void CatEdit_Click(object sender, EventArgs e)
        {
            int PID = Convert.ToInt32(lblmstproid.Text);
            //  Response.Redirect("DMS_PROD_CAT_MST.aspx?MYPRODID=" + MYPID + "&OrderNo=" + orders.OrderNo);
            CatEdit.Attributes.Add("href", "DMS_PROD_CAT_MST.aspx?MYPRODID=" + PID);
            CatEdit.Attributes.Add("target", "_blank");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            Response.Redirect("Adminindex.aspx");
        }

        public void GetControl(bool save, bool cancel)
        {
            BtnMstProSave.Enabled = save;
            Button6.Enabled = cancel;
            BtnMstProSave.Enabled = save;
        }

        protected void GridBussPro12_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblBusID = (Label)e.Item.FindControl("lblBusID");
            int ID = Convert.ToInt32(lblBusID.Text);
            if (DB.Eco_tblBusProd.Where(p => p.MYID == ID && p.ONLINESALEALLOW == true).Count() > 0)
            {
              //  linkButtonWebProd.Visible = true;
              //  linktoprodMaster.Visible = true;
            }
        }

        protected void RadioBtnMoreItenMulticolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            if (RadioBtnMoreItenMulticolor.SelectedValue == "1")
            {
                int MYPODID = Convert.ToInt32(lblmstproid.Text);
                pnelmulticooler.Visible = true;
                dropMulticolor.Visible = true;
                dropMulticolor.DataSource = DB.Eco_TBLCOLOR.Where(p => p.TenantID == TID && p.Active == "Y").OrderBy(p => p.COLORDESC1);
                dropMulticolor.DataTextField = "COLORDESC1";
                dropMulticolor.DataValueField = "COLORID";
                dropMulticolor.DataBind();
                dropMulticolor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Colors--", "0"));
                if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPODID && p.RunSerial == 0 && p.RecordType == "MultiColor").Count() > 0)
                {

                }
                else
                {
                    Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                    obj.TenantID = TID;
                    obj.RecordType = "MultiColor";
                    obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                    obj.CompniyAndContactID = MYPODID;
                    obj.RunSerial = 0;
                    obj.Recource = 5007;
                    obj.RecourceName = "Product";
                    obj.RecValue = "All Colors";
                    obj.Active = true;
                    // obj.Rremark = "AutomatedProcess";
                    DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }


            }
            else
            {
                dropMulticolor.Visible = false;
                pnelmulticooler.Visible = false;
            }




        }

        protected void btnMulticoler_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            Database.Eco_ImageTable objimage = new Database.Eco_ImageTable();
            objimage.TenantID = TID;
            objimage.sortnumber = null;
            objimage.SIZECODE = 0;
            objimage.PICTURE = null;
            objimage.MYPRODID = Convert.ToInt32(lblmstproid.Text);
            objimage.ImageID = 98801;
            objimage.Active = "Y";
            objimage.COLORID = Convert.ToInt32(dropMulticolor.SelectedValue);
            DB.Eco_ImageTable.AddObject(objimage);
            DB.SaveChanges();
            Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
            objEco_ERP_WEB_USER_MST1.IsCache = true;
            objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
            DB.SaveChanges();


        }

        protected void RadioBtnMoreItenMultisize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            if (RadioBtnMoreItenMultisize.SelectedValue == "1")
            {
                int MYPODID = Convert.ToInt32(lblmstproid.Text);
                dorpMultiSize.Visible = true;
                panelMultisize.Visible = true;
                dorpMultiSize.DataSource = DB.Eco_TBLSIZE.Where(p => p.ACTIVE == "Y" && p.TENANTID == TID).OrderBy(p => p.SIZEDESC1);
                dorpMultiSize.DataTextField = "SIZETYPE";
                dorpMultiSize.DataValueField = "SIZECODE";
                dorpMultiSize.DataBind();
                dorpMultiSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Size--", "0"));
                if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPODID && p.RunSerial == 0 && p.RecordType == "MultiSize").Count() > 0)
                {

                }
                else
                {
                    Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                    obj.TenantID = TID;
                    obj.RecordType = "MultiSize";
                    obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                    obj.CompniyAndContactID = MYPODID;
                    obj.RunSerial = 0;
                    obj.Recource = 5007;
                    obj.RecourceName = "Product";
                    obj.RecValue = "All Size";
                    obj.Active = true;
                    // obj.Rremark = "AutomatedProcess";
                    DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }


            }
            else
            {
                panelMultisize.Visible = false;
                dorpMultiSize.Visible = false;
            }

        }

        protected void btnsaveMultisize_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            Database.Eco_ImageTable objimage = new Database.Eco_ImageTable();
            objimage.TenantID = TID;
            objimage.sortnumber = null;
            objimage.SIZECODE = Convert.ToInt32(dorpMultiSize.SelectedValue);
            objimage.PICTURE = null;
            objimage.MYPRODID = Convert.ToInt32(lblmstproid.Text);
            objimage.ImageID = 98801;
            objimage.Active = "Y";
            objimage.COLORID = 0;
            DB.Eco_ImageTable.AddObject(objimage);
            DB.SaveChanges();
            Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
            objEco_ERP_WEB_USER_MST1.IsCache = true;
            objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
            DB.SaveChanges();
        }

        
        protected void LanguageFrance_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
        }
        protected void LanguageArabic_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }
        protected void LanguageEnglish_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }
        public void ManageLang()
        {
            //for Language

            if (Session["LANGUAGE"] != null)
            {
                RecieveLabel(Session["LANGUAGE"].ToString());
                if (Session["LANGUAGE"].ToString() == "ar-KW")
                    GetHide();
                else
                    GetShow();
            }

        }
        public void GetShow()
        {

            lblProductId1s.Attributes["class"] = lblProductNameMainLanguage1s.Attributes["class"] = lblBarcodeNo1s.Attributes["class"] = lblProductType1s.Attributes["class"] = lblAlternateCode11s.Attributes["class"] = lblAlternateCode21s.Attributes["class"] = lblProductNameLanguage21s.Attributes["class"] = lblProductNameLanguage31s.Attributes["class"] = lblShortName1s.Attributes["class"] = lblDescriptiontoPrint1s.Attributes["class"] = lblBusinessProduct1s.Attributes["class"] = lblPlaceholderLine1s.Attributes["class"] = lblPlaceholderAliRL1s.Attributes["class"] = lblPlaceholderColumn1s.Attributes["class"] = lblLink2Direct1s.Attributes["class"] = lblSortNumber1s.Attributes["class"] = lblColor1s.Attributes["class"] = lblMultiColor1s.Attributes["class"] = lblBrand1s.Attributes["class"] = lblSKUProduction1s.Attributes["class"] = lblSelectSize1s.Attributes["class"] = lblSize1s.Attributes["class"] = lblMultiUOM1s.Attributes["class"] = lblQuantityonHand1s.Attributes["class"] = lblwarranty1s.Attributes["class"] = lblProductStrategicalGroup1s.Attributes["class"] = lblDepartmentofSale1s.Attributes["class"] = lblHotItem1s.Attributes["class"] = lblSerializedItem1s.Attributes["class"] = lblPurchaseAllowed1s.Attributes["class"] = lblSaleAllowed1s.Attributes["class"] = lblMultiBinStore1s.Attributes["class"] = lblPerishable1s.Attributes["class"] = lblProductMethod1s.Attributes["class"] = lblSupplyMethod1s.Attributes["class"] = lblInternalNotes1s.Attributes["class"] = lblCostPrice1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblSalePrice11s.Attributes["class"] = lblWebSalePrice1s.Attributes["class"] = lblSalePrice21s.Attributes["class"] = lblmsrp1s.Attributes["class"] = lblSalePrice31s.Attributes["class"] = lblAllowDirectSale1s.Attributes["class"] = lblProductIDD1s.Attributes["class"] = lblKeywordD1s.Attributes["class"] = lblBoxshot1s.Attributes["class"] = lblLargeBoxshot1s.Attributes["class"] = lblMediumBoxshot1s.Attributes["class"] = lblSmallBoxshot1s.Attributes["class"] = lblOsPlatform1s.Attributes["class"] = lblCorpLogo1s.Attributes["class"] = lblLadingPage1s.Attributes["class"] = lblLine1s.Attributes["class"] = lblTrialUrl1s.Attributes["class"] = lblCartLink1s.Attributes["class"] = lblProductDetailLink1s.Attributes["class"] = lblLead1s.Attributes["class"] = lblOther1s.Attributes["class"] = lblPromotionType1s.Attributes["class"] = lblPayout1s.Attributes["class"] = lblLastDisplayDateonWeb1s.Attributes["class"] = lblSystemRemarks1s.Attributes["class"] = lblLastPurchaseDate1s.Attributes["class"] = lblLastSalesDate1s.Attributes["class"] = lblQuantityRecived1s.Attributes["class"] = lblQuantitySold1s.Attributes["class"] = lblQuantityOnHandDP1s.Attributes["class"] = lblActiveDP1s.Attributes["class"] = lblSupplierList1s.Attributes["class"] = "col-md-4 control-label getshow";
            lblKeywords1s.Attributes["class"] = lblRemarks1s.Attributes["class"] = lblLINK2DIRECTD1s.Attributes["class"] = lblSelectColors1s.Attributes["class"] = lblMultiSizeItem1s.Attributes["class"] = lblMainCategory1s.Attributes["class"] = lblActive1s.Attributes["class"] = "col-md-2 control-label getshow";
            lblProductId2h.Attributes["class"] = lblProductNameMainLanguage2h.Attributes["class"] = lblBarcodeNo2h.Attributes["class"] = lblProductType2h.Attributes["class"] = lblAlternateCode12h.Attributes["class"] = lblAlternateCode22h.Attributes["class"] = lblProductNameLanguage22h.Attributes["class"] = lblProductNameLanguage32h.Attributes["class"] = lblShortName2h.Attributes["class"] = lblDescriptiontoPrint2h.Attributes["class"] = lblBusinessProduct2h.Attributes["class"] = lblPlaceholderLine2h.Attributes["class"] = lblPlaceholderAliRL2h.Attributes["class"] = lblPlaceholderColumn2h.Attributes["class"] = lblLink2Direct2h.Attributes["class"] = lblSortNumber2h.Attributes["class"] = lblColor2h.Attributes["class"] = lblMultiColor2h.Attributes["class"] = lblBrand2h.Attributes["class"] = lblSKUProduction2h.Attributes["class"] = lblSelectSize2h.Attributes["class"] = lblSize2h.Attributes["class"] = lblMultiUOM2h.Attributes["class"] = lblQuantityonHand2h.Attributes["class"] = lblwarranty2h.Attributes["class"] = lblProductStrategicalGroup2h.Attributes["class"] = lblDepartmentofSale2h.Attributes["class"] = lblHotItem2h.Attributes["class"] = lblSerializedItem2h.Attributes["class"] = lblPurchaseAllowed2h.Attributes["class"] = lblSaleAllowed2h.Attributes["class"] = lblMultiBinStore2h.Attributes["class"] = lblPerishable2h.Attributes["class"] = lblProductMethod2h.Attributes["class"] = lblSupplyMethod2h.Attributes["class"] = lblInternalNotes2h.Attributes["class"] = lblCostPrice2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblSalePrice12h.Attributes["class"] = lblWebSalePrice2h.Attributes["class"] = lblSalePrice22h.Attributes["class"] = lblmsrp2h.Attributes["class"] = lblSalePrice32h.Attributes["class"] = lblAllowDirectSale2h.Attributes["class"] = lblProductIDD2h.Attributes["class"] = lblKeywordD2h.Attributes["class"] = lblBoxshot2h.Attributes["class"] = lblLargeBoxshot2h.Attributes["class"] = lblMediumBoxshot2h.Attributes["class"] = lblSmallBoxshot2h.Attributes["class"] = lblOsPlatform2h.Attributes["class"] = lblCorpLogo2h.Attributes["class"] = lblLadingPage2h.Attributes["class"] = lblLine2h.Attributes["class"] = lblTrialUrl2h.Attributes["class"] = lblCartLink2h.Attributes["class"] = lblProductDetailLink2h.Attributes["class"] = lblLead2h.Attributes["class"] = lblOther2h.Attributes["class"] = lblPromotionType2h.Attributes["class"] = lblPayout2h.Attributes["class"] = lblLastDisplayDateonWeb2h.Attributes["class"] = lblSystemRemarks2h.Attributes["class"] = lblLastPurchaseDate2h.Attributes["class"] = lblLastSalesDate2h.Attributes["class"] = lblQuantityRecived2h.Attributes["class"] = lblQuantitySold2h.Attributes["class"] = lblQuantityOnHandDP2h.Attributes["class"] = lblActiveDP2h.Attributes["class"] = lblSupplierList2h.Attributes["class"] = "col-md-4 control-label gethide";
            lblKeywords2h.Attributes["class"] = lblRemarks2h.Attributes["class"] = lblSelectColors2h.Attributes["class"] = lblLINK2DIRECTD2h.Attributes["class"] = lblMultiSizeItem2h.Attributes["class"] = lblMainCategory2h.Attributes["class"] = lblActive2h.Attributes["class"] = "col-md-2 control-label gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblProductId1s.Attributes["class"] = lblProductNameMainLanguage1s.Attributes["class"] = lblBarcodeNo1s.Attributes["class"] = lblProductType1s.Attributes["class"] = lblAlternateCode11s.Attributes["class"] = lblAlternateCode21s.Attributes["class"] = lblProductNameLanguage21s.Attributes["class"] = lblProductNameLanguage31s.Attributes["class"] = lblShortName1s.Attributes["class"] = lblDescriptiontoPrint1s.Attributes["class"] = lblBusinessProduct1s.Attributes["class"] = lblPlaceholderLine1s.Attributes["class"] = lblPlaceholderAliRL1s.Attributes["class"] = lblPlaceholderColumn1s.Attributes["class"] = lblLink2Direct1s.Attributes["class"] = lblSortNumber1s.Attributes["class"] = lblColor1s.Attributes["class"] = lblMultiColor1s.Attributes["class"] = lblBrand1s.Attributes["class"] = lblSKUProduction1s.Attributes["class"] = lblSelectSize1s.Attributes["class"] = lblSize1s.Attributes["class"] = lblMultiUOM1s.Attributes["class"] = lblQuantityonHand1s.Attributes["class"] = lblwarranty1s.Attributes["class"] = lblProductStrategicalGroup1s.Attributes["class"] = lblDepartmentofSale1s.Attributes["class"] = lblHotItem1s.Attributes["class"] = lblSerializedItem1s.Attributes["class"] = lblPurchaseAllowed1s.Attributes["class"] = lblSaleAllowed1s.Attributes["class"] = lblMultiBinStore1s.Attributes["class"] = lblPerishable1s.Attributes["class"] = lblProductMethod1s.Attributes["class"] = lblSupplyMethod1s.Attributes["class"] = lblInternalNotes1s.Attributes["class"] = lblCostPrice1s.Attributes["class"] = lblCurrency1s.Attributes["class"] = lblSalePrice11s.Attributes["class"] = lblWebSalePrice1s.Attributes["class"] = lblSalePrice21s.Attributes["class"] = lblmsrp1s.Attributes["class"] = lblSalePrice31s.Attributes["class"] = lblAllowDirectSale1s.Attributes["class"] = lblProductIDD1s.Attributes["class"] = lblKeywordD1s.Attributes["class"] = lblBoxshot1s.Attributes["class"] = lblLargeBoxshot1s.Attributes["class"] = lblMediumBoxshot1s.Attributes["class"] = lblSmallBoxshot1s.Attributes["class"] = lblOsPlatform1s.Attributes["class"] = lblCorpLogo1s.Attributes["class"] = lblLadingPage1s.Attributes["class"] = lblLine1s.Attributes["class"] = lblTrialUrl1s.Attributes["class"] = lblCartLink1s.Attributes["class"] = lblProductDetailLink1s.Attributes["class"] = lblLead1s.Attributes["class"] = lblOther1s.Attributes["class"] = lblPromotionType1s.Attributes["class"] = lblPayout1s.Attributes["class"] = lblLastDisplayDateonWeb1s.Attributes["class"] = lblSystemRemarks1s.Attributes["class"] = lblLastPurchaseDate1s.Attributes["class"] = lblLastSalesDate1s.Attributes["class"] = lblQuantityRecived1s.Attributes["class"] = lblQuantitySold1s.Attributes["class"] = lblQuantityOnHandDP1s.Attributes["class"] = lblActiveDP1s.Attributes["class"] = lblSupplierList1s.Attributes["class"] = "col-md-4 control-label gethide";
            lblKeywords1s.Attributes["class"] = lblRemarks1s.Attributes["class"] = lblLINK2DIRECTD1s.Attributes["class"] = lblSelectColors1s.Attributes["class"] = lblMultiSizeItem1s.Attributes["class"] = lblMainCategory1s.Attributes["class"] = lblActive1s.Attributes["class"] = "col-md-2 control-label gethide";
            lblProductId2h.Attributes["class"] = lblProductNameMainLanguage2h.Attributes["class"] = lblBarcodeNo2h.Attributes["class"] = lblProductType2h.Attributes["class"] = lblAlternateCode12h.Attributes["class"] = lblAlternateCode22h.Attributes["class"] = lblProductNameLanguage22h.Attributes["class"] = lblProductNameLanguage32h.Attributes["class"] = lblShortName2h.Attributes["class"] = lblDescriptiontoPrint2h.Attributes["class"] = lblBusinessProduct2h.Attributes["class"] = lblPlaceholderLine2h.Attributes["class"] = lblPlaceholderAliRL2h.Attributes["class"] = lblPlaceholderColumn2h.Attributes["class"] = lblLink2Direct2h.Attributes["class"] = lblSortNumber2h.Attributes["class"] = lblColor2h.Attributes["class"] = lblMultiColor2h.Attributes["class"] = lblBrand2h.Attributes["class"] = lblSKUProduction2h.Attributes["class"] = lblSelectSize2h.Attributes["class"] = lblSize2h.Attributes["class"] = lblMultiUOM2h.Attributes["class"] = lblQuantityonHand2h.Attributes["class"] = lblwarranty2h.Attributes["class"] = lblProductStrategicalGroup2h.Attributes["class"] = lblDepartmentofSale2h.Attributes["class"] = lblHotItem2h.Attributes["class"] = lblSerializedItem2h.Attributes["class"] = lblPurchaseAllowed2h.Attributes["class"] = lblSaleAllowed2h.Attributes["class"] = lblMultiBinStore2h.Attributes["class"] = lblPerishable2h.Attributes["class"] = lblProductMethod2h.Attributes["class"] = lblSupplyMethod2h.Attributes["class"] = lblInternalNotes2h.Attributes["class"] = lblCostPrice2h.Attributes["class"] = lblCurrency2h.Attributes["class"] = lblSalePrice12h.Attributes["class"] = lblWebSalePrice2h.Attributes["class"] = lblSalePrice22h.Attributes["class"] = lblmsrp2h.Attributes["class"] = lblSalePrice32h.Attributes["class"] = lblAllowDirectSale2h.Attributes["class"] = lblProductIDD2h.Attributes["class"] = lblKeywordD2h.Attributes["class"] = lblBoxshot2h.Attributes["class"] = lblLargeBoxshot2h.Attributes["class"] = lblMediumBoxshot2h.Attributes["class"] = lblSmallBoxshot2h.Attributes["class"] = lblOsPlatform2h.Attributes["class"] = lblCorpLogo2h.Attributes["class"] = lblLadingPage2h.Attributes["class"] = lblLine2h.Attributes["class"] = lblTrialUrl2h.Attributes["class"] = lblCartLink2h.Attributes["class"] = lblProductDetailLink2h.Attributes["class"] = lblLead2h.Attributes["class"] = lblOther2h.Attributes["class"] = lblPromotionType2h.Attributes["class"] = lblPayout2h.Attributes["class"] = lblLastDisplayDateonWeb2h.Attributes["class"] = lblSystemRemarks2h.Attributes["class"] = lblLastPurchaseDate2h.Attributes["class"] = lblLastSalesDate2h.Attributes["class"] = lblQuantityRecived2h.Attributes["class"] = lblQuantitySold2h.Attributes["class"] = lblQuantityOnHandDP2h.Attributes["class"] = lblActiveDP2h.Attributes["class"] = lblSupplierList2h.Attributes["class"] = "col-md-4 control-label getshow";
            lblKeywords2h.Attributes["class"] = lblRemarks2h.Attributes["class"] = lblLINK2DIRECTD2h.Attributes["class"] = lblSelectColors2h.Attributes["class"] = lblMultiSizeItem2h.Attributes["class"] = lblMainCategory2h.Attributes["class"] = lblActive2h.Attributes["class"] = "col-md-2 control-label getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblProductId2h.Visible = lblProductNameMainLanguage2h.Visible = lblBarcodeNo2h.Visible = lblProductType2h.Visible = lblAlternateCode12h.Visible = lblAlternateCode22h.Visible = lblProductNameLanguage22h.Visible = lblProductNameLanguage32h.Visible = lblShortName2h.Visible = lblDescriptiontoPrint2h.Visible = lblBusinessProduct2h.Visible = lblActive2h.Visible = lblPlaceholderLine2h.Visible = lblPlaceholderAliRL2h.Visible = lblPlaceholderColumn2h.Visible = lblLink2Direct2h.Visible = lblSortNumber2h.Visible = lblColor2h.Visible = lblMultiColor2h.Visible = lblSelectColors2h.Visible = lblBrand2h.Visible = lblSKUProduction2h.Visible = lblSelectSize2h.Visible = lblSize2h.Visible = lblMultiSizeItem2h.Visible = lblMainCategory2h.Visible = lblMultiUOM2h.Visible = lblQuantityonHand2h.Visible = lblwarranty2h.Visible = lblKeywords2h.Visible = lblProductStrategicalGroup2h.Visible = lblDepartmentofSale2h.Visible = lblHotItem2h.Visible = lblSerializedItem2h.Visible = lblPurchaseAllowed2h.Visible = lblSaleAllowed2h.Visible = lblMultiBinStore2h.Visible = lblPerishable2h.Visible = lblProductMethod2h.Visible = lblSupplyMethod2h.Visible = lblInternalNotes2h.Visible = lblCostPrice2h.Visible = lblCurrency2h.Visible = lblSalePrice12h.Visible = lblWebSalePrice2h.Visible = lblSalePrice22h.Visible = lblmsrp2h.Visible = lblSalePrice32h.Visible = lblAllowDirectSale2h.Visible = lblProductIDD2h.Visible = lblRemarks2h.Visible = lblLINK2DIRECTD2h.Visible = lblKeywordD2h.Visible = lblBoxshot2h.Visible = lblLargeBoxshot2h.Visible = lblMediumBoxshot2h.Visible = lblSmallBoxshot2h.Visible = lblOsPlatform2h.Visible = lblCorpLogo2h.Visible = lblLadingPage2h.Visible = lblLine2h.Visible = lblTrialUrl2h.Visible = lblCartLink2h.Visible = lblProductDetailLink2h.Visible = lblLead2h.Visible = lblOther2h.Visible = lblPromotionType2h.Visible = lblPayout2h.Visible = lblLastDisplayDateonWeb2h.Visible = lblSystemRemarks2h.Visible = lblLastPurchaseDate2h.Visible = lblLastSalesDate2h.Visible = lblQuantityRecived2h.Visible = lblQuantitySold2h.Visible = lblQuantityOnHandDP2h.Visible = lblActiveDP2h.Visible = lblSupplierList2h.Visible = false;
                    //2true
                    txtProductId2h.Visible = txtProductNameMainLanguage2h.Visible = txtBarcodeNo2h.Visible = txtProductType2h.Visible = txtAlternateCode12h.Visible = txtAlternateCode22h.Visible = txtProductNameLanguage22h.Visible = txtProductNameLanguage32h.Visible = txtShortName2h.Visible = txtDescriptiontoPrint2h.Visible = txtBusinessProduct2h.Visible = txtActive2h.Visible = txtPlaceholderLine2h.Visible = txtPlaceholderAliRL2h.Visible = txtPlaceholderColumn2h.Visible = txtLink2Direct2h.Visible = txtSortNumber2h.Visible = txtColor2h.Visible = txtMultiColor2h.Visible = txtSelectColors2h.Visible = txtBrand2h.Visible = txtSKUProduction2h.Visible = txtSelectSize2h.Visible = txtSize2h.Visible = txtMultiSizeItem2h.Visible = txtMainCategory2h.Visible = txtMultiUOM2h.Visible = txtQuantityonHand2h.Visible = txtwarranty2h.Visible = txtKeywords2h.Visible = txtProductStrategicalGroup2h.Visible = txtDepartmentofSale2h.Visible = txtHotItem2h.Visible = txtSerializedItem2h.Visible = txtPurchaseAllowed2h.Visible = txtSaleAllowed2h.Visible = txtMultiBinStore2h.Visible = txtPerishable2h.Visible = txtProductMethod2h.Visible = txtSupplyMethod2h.Visible = txtInternalNotes2h.Visible = txtCostPrice2h.Visible = txtCurrency2h.Visible = txtSalePrice12h.Visible = txtWebSalePrice2h.Visible = txtSalePrice22h.Visible = txtmsrp2h.Visible = txtSalePrice32h.Visible = txtAllowDirectSale2h.Visible = txtProductIDD2h.Visible = txtRemarks2h.Visible = txtLINK2DIRECTD2h.Visible = txtKeywordD2h.Visible = txtBoxshot2h.Visible = txtLargeBoxshot2h.Visible = txtMediumBoxshot2h.Visible = txtSmallBoxshot2h.Visible = txtOsPlatform2h.Visible = txtCorpLogo2h.Visible = txtLadingPage2h.Visible = txtLine2h.Visible = txtTrialUrl2h.Visible = txtCartLink2h.Visible = txtProductDetailLink2h.Visible = txtLead2h.Visible = txtOther2h.Visible = txtPromotionType2h.Visible = txtPayout2h.Visible = txtLastDisplayDateonWeb2h.Visible = txtSystemRemarks2h.Visible = txtLastPurchaseDate2h.Visible = txtLastSalesDate2h.Visible = txtQuantityRecived2h.Visible = txtQuantitySold2h.Visible = txtQuantityOnHandDP2h.Visible = txtActiveDP2h.Visible = txtSupplierList2h.Visible = true;

                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblProductId2h.Visible = lblProductNameMainLanguage2h.Visible = lblBarcodeNo2h.Visible = lblProductType2h.Visible = lblAlternateCode12h.Visible = lblAlternateCode22h.Visible = lblProductNameLanguage22h.Visible = lblProductNameLanguage32h.Visible = lblShortName2h.Visible = lblDescriptiontoPrint2h.Visible = lblBusinessProduct2h.Visible = lblActive2h.Visible = lblPlaceholderLine2h.Visible = lblPlaceholderAliRL2h.Visible = lblPlaceholderColumn2h.Visible = lblLink2Direct2h.Visible = lblSortNumber2h.Visible = lblColor2h.Visible = lblMultiColor2h.Visible = lblSelectColors2h.Visible = lblBrand2h.Visible = lblSKUProduction2h.Visible = lblSelectSize2h.Visible = lblSize2h.Visible = lblMultiSizeItem2h.Visible = lblMainCategory2h.Visible = lblMultiUOM2h.Visible = lblQuantityonHand2h.Visible = lblwarranty2h.Visible = lblKeywords2h.Visible = lblProductStrategicalGroup2h.Visible = lblDepartmentofSale2h.Visible = lblHotItem2h.Visible = lblSerializedItem2h.Visible = lblPurchaseAllowed2h.Visible = lblSaleAllowed2h.Visible = lblMultiBinStore2h.Visible = lblPerishable2h.Visible = lblProductMethod2h.Visible = lblSupplyMethod2h.Visible = lblInternalNotes2h.Visible = lblCostPrice2h.Visible = lblCurrency2h.Visible = lblSalePrice12h.Visible = lblWebSalePrice2h.Visible = lblSalePrice22h.Visible = lblmsrp2h.Visible = lblSalePrice32h.Visible = lblAllowDirectSale2h.Visible = lblProductIDD2h.Visible = lblRemarks2h.Visible = lblLINK2DIRECTD2h.Visible = lblKeywordD2h.Visible = lblBoxshot2h.Visible = lblLargeBoxshot2h.Visible = lblMediumBoxshot2h.Visible = lblSmallBoxshot2h.Visible = lblOsPlatform2h.Visible = lblCorpLogo2h.Visible = lblLadingPage2h.Visible = lblLine2h.Visible = lblTrialUrl2h.Visible = lblCartLink2h.Visible = lblProductDetailLink2h.Visible = lblLead2h.Visible = lblOther2h.Visible = lblPromotionType2h.Visible = lblPayout2h.Visible = lblLastDisplayDateonWeb2h.Visible = lblSystemRemarks2h.Visible = lblLastPurchaseDate2h.Visible = lblLastSalesDate2h.Visible = lblQuantityRecived2h.Visible = lblQuantitySold2h.Visible = lblQuantityOnHandDP2h.Visible = lblActiveDP2h.Visible = lblSupplierList2h.Visible = true;
                    //2false
                    txtProductId2h.Visible = txtProductNameMainLanguage2h.Visible = txtBarcodeNo2h.Visible = txtProductType2h.Visible = txtAlternateCode12h.Visible = txtAlternateCode22h.Visible = txtProductNameLanguage22h.Visible = txtProductNameLanguage32h.Visible = txtShortName2h.Visible = txtDescriptiontoPrint2h.Visible = txtBusinessProduct2h.Visible = txtActive2h.Visible = txtPlaceholderLine2h.Visible = txtPlaceholderAliRL2h.Visible = txtPlaceholderColumn2h.Visible = txtLink2Direct2h.Visible = txtSortNumber2h.Visible = txtColor2h.Visible = txtMultiColor2h.Visible = txtSelectColors2h.Visible = txtBrand2h.Visible = txtSKUProduction2h.Visible = txtSelectSize2h.Visible = txtSize2h.Visible = txtMultiSizeItem2h.Visible = txtMainCategory2h.Visible = txtMultiUOM2h.Visible = txtQuantityonHand2h.Visible = txtwarranty2h.Visible = txtKeywords2h.Visible = txtProductStrategicalGroup2h.Visible = txtDepartmentofSale2h.Visible = txtHotItem2h.Visible = txtSerializedItem2h.Visible = txtPurchaseAllowed2h.Visible = txtSaleAllowed2h.Visible = txtMultiBinStore2h.Visible = txtPerishable2h.Visible = txtProductMethod2h.Visible = txtSupplyMethod2h.Visible = txtInternalNotes2h.Visible = txtCostPrice2h.Visible = txtCurrency2h.Visible = txtSalePrice12h.Visible = txtWebSalePrice2h.Visible = txtSalePrice22h.Visible = txtmsrp2h.Visible = txtSalePrice32h.Visible = txtAllowDirectSale2h.Visible = txtProductIDD2h.Visible = txtRemarks2h.Visible = txtLINK2DIRECTD2h.Visible = txtKeywordD2h.Visible = txtBoxshot2h.Visible = txtLargeBoxshot2h.Visible = txtMediumBoxshot2h.Visible = txtSmallBoxshot2h.Visible = txtOsPlatform2h.Visible = txtCorpLogo2h.Visible = txtLadingPage2h.Visible = txtLine2h.Visible = txtTrialUrl2h.Visible = txtCartLink2h.Visible = txtProductDetailLink2h.Visible = txtLead2h.Visible = txtOther2h.Visible = txtPromotionType2h.Visible = txtPayout2h.Visible = txtLastDisplayDateonWeb2h.Visible = txtSystemRemarks2h.Visible = txtLastPurchaseDate2h.Visible = txtLastSalesDate2h.Visible = txtQuantityRecived2h.Visible = txtQuantitySold2h.Visible = txtQuantityOnHandDP2h.Visible = txtActiveDP2h.Visible = txtSupplierList2h.Visible = false;

                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lblProductId1s.Visible = lblProductNameMainLanguage1s.Visible = lblBarcodeNo1s.Visible = lblProductType1s.Visible = lblAlternateCode11s.Visible = lblAlternateCode21s.Visible = lblProductNameLanguage21s.Visible = lblProductNameLanguage31s.Visible = lblShortName1s.Visible = lblDescriptiontoPrint1s.Visible = lblBusinessProduct1s.Visible = lblActive1s.Visible = lblPlaceholderLine1s.Visible = lblPlaceholderAliRL1s.Visible = lblPlaceholderColumn1s.Visible = lblLink2Direct1s.Visible = lblSortNumber1s.Visible = lblColor1s.Visible = lblMultiColor1s.Visible = lblSelectColors1s.Visible = lblBrand1s.Visible = lblSKUProduction1s.Visible = lblSelectSize1s.Visible = lblSize1s.Visible = lblMultiSizeItem1s.Visible = lblMainCategory1s.Visible = lblMultiUOM1s.Visible = lblQuantityonHand1s.Visible = lblwarranty1s.Visible = lblKeywords1s.Visible = lblProductStrategicalGroup1s.Visible = lblDepartmentofSale1s.Visible = lblHotItem1s.Visible = lblSerializedItem1s.Visible = lblPurchaseAllowed1s.Visible = lblSaleAllowed1s.Visible = lblMultiBinStore1s.Visible = lblPerishable1s.Visible = lblProductMethod1s.Visible = lblSupplyMethod1s.Visible = lblInternalNotes1s.Visible = lblCostPrice1s.Visible = lblCurrency1s.Visible = lblSalePrice11s.Visible = lblWebSalePrice1s.Visible = lblSalePrice21s.Visible = lblmsrp1s.Visible = lblSalePrice31s.Visible = lblAllowDirectSale1s.Visible = lblProductIDD1s.Visible = lblRemarks1s.Visible = lblLINK2DIRECTD1s.Visible = lblKeywordD1s.Visible = lblBoxshot1s.Visible = lblLargeBoxshot1s.Visible = lblMediumBoxshot1s.Visible = lblSmallBoxshot1s.Visible = lblOsPlatform1s.Visible = lblCorpLogo1s.Visible = lblLadingPage1s.Visible = lblLine1s.Visible = lblTrialUrl1s.Visible = lblCartLink1s.Visible = lblProductDetailLink1s.Visible = lblLead1s.Visible = lblOther1s.Visible = lblPromotionType1s.Visible = lblPayout1s.Visible = lblLastDisplayDateonWeb1s.Visible = lblSystemRemarks1s.Visible = lblLastPurchaseDate1s.Visible = lblLastSalesDate1s.Visible = lblQuantityRecived1s.Visible = lblQuantitySold1s.Visible = lblQuantityOnHandDP1s.Visible = lblActiveDP1s.Visible = lblSupplierList1s.Visible = false;
                    //1true
                    txtProductId1s.Visible = txtProductNameMainLanguage1s.Visible = txtBarcodeNo1s.Visible = txtProductType1s.Visible = txtAlternateCode11s.Visible = txtAlternateCode21s.Visible = txtProductNameLanguage21s.Visible = txtProductNameLanguage31s.Visible = txtShortName1s.Visible = txtDescriptiontoPrint1s.Visible = txtBusinessProduct1s.Visible = txtActive1s.Visible = txtPlaceholderLine1s.Visible = txtPlaceholderAliRL1s.Visible = txtPlaceholderColumn1s.Visible = txtLink2Direct1s.Visible = txtSortNumber1s.Visible = txtColor1s.Visible = txtMultiColor1s.Visible = txtSelectColors1s.Visible = txtBrand1s.Visible = txtSKUProduction1s.Visible = txtSelectSize1s.Visible = txtSize1s.Visible = txtMultiSizeItem1s.Visible = txtMainCategory1s.Visible = txtMultiUOM1s.Visible = txtQuantityonHand1s.Visible = txtwarranty1s.Visible = txtKeywords1s.Visible = txtProductStrategicalGroup1s.Visible = txtDepartmentofSale1s.Visible = txtHotItem1s.Visible = txtSerializedItem1s.Visible = txtPurchaseAllowed1s.Visible = txtSaleAllowed1s.Visible = txtMultiBinStore1s.Visible = txtPerishable1s.Visible = txtProductMethod1s.Visible = txtSupplyMethod1s.Visible = txtInternalNotes1s.Visible = txtCostPrice1s.Visible = txtCurrency1s.Visible = txtSalePrice11s.Visible = txtWebSalePrice1s.Visible = txtSalePrice21s.Visible = txtmsrp1s.Visible = txtSalePrice31s.Visible = txtAllowDirectSale1s.Visible = txtProductIDD1s.Visible = txtRemarks1s.Visible = txtLINK2DIRECTD1s.Visible = txtKeywordD1s.Visible = txtBoxshot1s.Visible = txtLargeBoxshot1s.Visible = txtMediumBoxshot1s.Visible = txtSmallBoxshot1s.Visible = txtOsPlatform1s.Visible = txtCorpLogo1s.Visible = txtLadingPage1s.Visible = txtLine1s.Visible = txtTrialUrl1s.Visible = txtCartLink1s.Visible = txtProductDetailLink1s.Visible = txtLead1s.Visible = txtOther1s.Visible = txtPromotionType1s.Visible = txtPayout1s.Visible = txtLastDisplayDateonWeb1s.Visible = txtSystemRemarks1s.Visible = txtLastPurchaseDate1s.Visible = txtLastSalesDate1s.Visible = txtQuantityRecived1s.Visible = txtQuantitySold1s.Visible = txtQuantityOnHandDP1s.Visible = txtActiveDP1s.Visible = txtSupplierList1s.Visible = true;
                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lblProductId1s.Visible = lblProductNameMainLanguage1s.Visible = lblBarcodeNo1s.Visible = lblProductType1s.Visible = lblAlternateCode11s.Visible = lblAlternateCode21s.Visible = lblProductNameLanguage21s.Visible = lblProductNameLanguage31s.Visible = lblShortName1s.Visible = lblDescriptiontoPrint1s.Visible = lblBusinessProduct1s.Visible = lblActive1s.Visible = lblPlaceholderLine1s.Visible = lblPlaceholderAliRL1s.Visible = lblPlaceholderColumn1s.Visible = lblLink2Direct1s.Visible = lblSortNumber1s.Visible = lblColor1s.Visible = lblMultiColor1s.Visible = lblSelectColors1s.Visible = lblBrand1s.Visible = lblSKUProduction1s.Visible = lblSelectSize1s.Visible = lblSize1s.Visible = lblMultiSizeItem1s.Visible = lblMainCategory1s.Visible = lblMultiUOM1s.Visible = lblQuantityonHand1s.Visible = lblwarranty1s.Visible = lblKeywords1s.Visible = lblProductStrategicalGroup1s.Visible = lblDepartmentofSale1s.Visible = lblHotItem1s.Visible = lblSerializedItem1s.Visible = lblPurchaseAllowed1s.Visible = lblSaleAllowed1s.Visible = lblMultiBinStore1s.Visible = lblPerishable1s.Visible = lblProductMethod1s.Visible = lblSupplyMethod1s.Visible = lblInternalNotes1s.Visible = lblCostPrice1s.Visible = lblCurrency1s.Visible = lblSalePrice11s.Visible = lblWebSalePrice1s.Visible = lblSalePrice21s.Visible = lblmsrp1s.Visible = lblSalePrice31s.Visible = lblAllowDirectSale1s.Visible = lblProductIDD1s.Visible = lblRemarks1s.Visible = lblLINK2DIRECTD1s.Visible = lblKeywordD1s.Visible = lblBoxshot1s.Visible = lblLargeBoxshot1s.Visible = lblMediumBoxshot1s.Visible = lblSmallBoxshot1s.Visible = lblOsPlatform1s.Visible = lblCorpLogo1s.Visible = lblLadingPage1s.Visible = lblLine1s.Visible = lblTrialUrl1s.Visible = lblCartLink1s.Visible = lblProductDetailLink1s.Visible = lblLead1s.Visible = lblOther1s.Visible = lblPromotionType1s.Visible = lblPayout1s.Visible = lblLastDisplayDateonWeb1s.Visible = lblSystemRemarks1s.Visible = lblLastPurchaseDate1s.Visible = lblLastSalesDate1s.Visible = lblQuantityRecived1s.Visible = lblQuantitySold1s.Visible = lblQuantityOnHandDP1s.Visible = lblActiveDP1s.Visible = lblSupplierList1s.Visible = true;
                    //1false
                    txtProductId1s.Visible = txtProductNameMainLanguage1s.Visible = txtBarcodeNo1s.Visible = txtProductType1s.Visible = txtAlternateCode11s.Visible = txtAlternateCode21s.Visible = txtProductNameLanguage21s.Visible = txtProductNameLanguage31s.Visible = txtShortName1s.Visible = txtDescriptiontoPrint1s.Visible = txtBusinessProduct1s.Visible = txtActive1s.Visible = txtPlaceholderLine1s.Visible = txtPlaceholderAliRL1s.Visible = txtPlaceholderColumn1s.Visible = txtLink2Direct1s.Visible = txtSortNumber1s.Visible = txtColor1s.Visible = txtMultiColor1s.Visible = txtSelectColors1s.Visible = txtBrand1s.Visible = txtSKUProduction1s.Visible = txtSelectSize1s.Visible = txtSize1s.Visible = txtMultiSizeItem1s.Visible = txtMainCategory1s.Visible = txtMultiUOM1s.Visible = txtQuantityonHand1s.Visible = txtwarranty1s.Visible = txtKeywords1s.Visible = txtProductStrategicalGroup1s.Visible = txtDepartmentofSale1s.Visible = txtHotItem1s.Visible = txtSerializedItem1s.Visible = txtPurchaseAllowed1s.Visible = txtSaleAllowed1s.Visible = txtMultiBinStore1s.Visible = txtPerishable1s.Visible = txtProductMethod1s.Visible = txtSupplyMethod1s.Visible = txtInternalNotes1s.Visible = txtCostPrice1s.Visible = txtCurrency1s.Visible = txtSalePrice11s.Visible = txtWebSalePrice1s.Visible = txtSalePrice21s.Visible = txtmsrp1s.Visible = txtSalePrice31s.Visible = txtAllowDirectSale1s.Visible = txtProductIDD1s.Visible = txtRemarks1s.Visible = txtLINK2DIRECTD1s.Visible = txtKeywordD1s.Visible = txtBoxshot1s.Visible = txtLargeBoxshot1s.Visible = txtMediumBoxshot1s.Visible = txtSmallBoxshot1s.Visible = txtOsPlatform1s.Visible = txtCorpLogo1s.Visible = txtLadingPage1s.Visible = txtLine1s.Visible = txtTrialUrl1s.Visible = txtCartLink1s.Visible = txtProductDetailLink1s.Visible = txtLead1s.Visible = txtOther1s.Visible = txtPromotionType1s.Visible = txtPayout1s.Visible = txtLastDisplayDateonWeb1s.Visible = txtSystemRemarks1s.Visible = txtLastPurchaseDate1s.Visible = txtLastSalesDate1s.Visible = txtQuantityRecived1s.Visible = txtQuantitySold1s.Visible = txtQuantityOnHandDP1s.Visible = txtActiveDP1s.Visible = txtSupplierList1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void SaveLabel(string lang)
        {
            string PID = ((UserMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.Eco_TBLLabelDTL> List = ((UserMaster)this.Master).Bindxml("tbl_lbl_productMstNew").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\xml\\tbl_lbl_productMstNew.xml"));
            foreach (Database.Eco_TBLLabelDTL item in List)
            {

                var obj = ((UserMaster)this.Master).Bindxml("tbl_lbl_productMstNew").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblProductId1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductId1s.Text;
                else if (lblProductNameMainLanguage1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductNameMainLanguage1s.Text;
                else if (lblBarcodeNo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBarcodeNo1s.Text;
                else if (lblProductType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductType1s.Text;
                else if (lblAlternateCode11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAlternateCode11s.Text;
                else if (lblAlternateCode21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAlternateCode21s.Text;
                else if (lblProductNameLanguage21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductNameLanguage21s.Text;
                else if (lblProductNameLanguage31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductNameLanguage31s.Text;
                else if (lblShortName1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShortName1s.Text;
                else if (lblDescriptiontoPrint1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescriptiontoPrint1s.Text;
                else if (lblBusinessProduct1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBusinessProduct1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblPlaceholderLine1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlaceholderLine1s.Text;
                else if (lblPlaceholderAliRL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlaceholderAliRL1s.Text;
                else if (lblPlaceholderColumn1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlaceholderColumn1s.Text;
                else if (lblLink2Direct1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLink2Direct1s.Text;
                else if (lblSortNumber1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSortNumber1s.Text;
                else if (lblColor1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtColor1s.Text;
                else if (lblMultiColor1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiColor1s.Text;
                else if (lblSelectColors1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSelectColors1s.Text;
                else if (lblBrand1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBrand1s.Text;
                else if (lblSKUProduction1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSKUProduction1s.Text;
                else if (lblSelectSize1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSelectSize1s.Text;
                else if (lblSize1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSize1s.Text;
                else if (lblMultiSizeItem1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiSizeItem1s.Text;
                else if (lblMainCategory1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMainCategory1s.Text;
                else if (lblMultiUOM1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiUOM1s.Text;
                else if (lblQuantityonHand1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantityonHand1s.Text;
                else if (lblwarranty1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtwarranty1s.Text;
                else if (lblKeywords1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeywords1s.Text;
                else if (lblProductStrategicalGroup1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductStrategicalGroup1s.Text;
                else if (lblDepartmentofSale1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDepartmentofSale1s.Text;
                else if (lblHotItem1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtHotItem1s.Text;
                else if (lblSerializedItem1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSerializedItem1s.Text;
                else if (lblPurchaseAllowed1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPurchaseAllowed1s.Text;
                else if (lblSaleAllowed1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSaleAllowed1s.Text;
                else if (lblMultiBinStore1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiBinStore1s.Text;
                else if (lblPerishable1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPerishable1s.Text;
                else if (lblProductMethod1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductMethod1s.Text;
                else if (lblSupplyMethod1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplyMethod1s.Text;
                else if (lblInternalNotes1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInternalNotes1s.Text;
                else if (lblCostPrice1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCostPrice1s.Text;
                else if (lblCurrency1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCurrency1s.Text;
                else if (lblSalePrice11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalePrice11s.Text;
                else if (lblWebSalePrice1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWebSalePrice1s.Text;
                else if (lblSalePrice21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalePrice21s.Text;
                else if (lblmsrp1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtmsrp1s.Text;
                else if (lblSalePrice31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalePrice31s.Text;
                else if (lblAllowDirectSale1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAllowDirectSale1s.Text;
                else if (lblProductIDD1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductIDD1s.Text;
                else if (lblRemarks1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRemarks1s.Text;
                else if (lblLINK2DIRECTD1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLINK2DIRECTD1s.Text;
                else if (lblKeywordD1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeywordD1s.Text;
                else if (lblBoxshot1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBoxshot1s.Text;
                else if (lblLargeBoxshot1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLargeBoxshot1s.Text;
                else if (lblMediumBoxshot1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMediumBoxshot1s.Text;
                else if (lblSmallBoxshot1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSmallBoxshot1s.Text;
                else if (lblOsPlatform1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOsPlatform1s.Text;
                else if (lblCorpLogo1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCorpLogo1s.Text;
                else if (lblLadingPage1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLadingPage1s.Text;
                else if (lblLine1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLine1s.Text;
                else if (lblTrialUrl1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTrialUrl1s.Text;
                else if (lblCartLink1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCartLink1s.Text;
                else if (lblProductDetailLink1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductDetailLink1s.Text;
                else if (lblLead1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLead1s.Text;
                else if (lblOther1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOther1s.Text;
                else if (lblPromotionType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPromotionType1s.Text;
                else if (lblPayout1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPayout1s.Text;
                else if (lblLastDisplayDateonWeb1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLastDisplayDateonWeb1s.Text;
                else if (lblSystemRemarks1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSystemRemarks1s.Text;
                else if (lblLastPurchaseDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLastPurchaseDate1s.Text;
                else if (lblLastSalesDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLastSalesDate1s.Text;
                else if (lblQuantityRecived1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantityRecived1s.Text;
                else if (lblQuantitySold1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantitySold1s.Text;
                else if (lblQuantityOnHandDP1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantityOnHandDP1s.Text;
                else if (lblActiveDP1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActiveDP1s.Text;
                else if (lblSupplierList1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplierList1s.Text;

                if (lblProductId2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductId2h.Text;
                else if (lblProductNameMainLanguage2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductNameMainLanguage2h.Text;
                else if (lblBarcodeNo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBarcodeNo2h.Text;
                else if (lblProductType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductType2h.Text;
                else if (lblAlternateCode12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAlternateCode12h.Text;
                else if (lblAlternateCode22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAlternateCode22h.Text;
                else if (lblProductNameLanguage22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductNameLanguage22h.Text;
                else if (lblProductNameLanguage32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductNameLanguage32h.Text;
                else if (lblShortName2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtShortName2h.Text;
                else if (lblDescriptiontoPrint2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescriptiontoPrint2h.Text;
                else if (lblBusinessProduct2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBusinessProduct2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblPlaceholderLine2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlaceholderLine2h.Text;
                else if (lblPlaceholderAliRL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlaceholderAliRL2h.Text;
                else if (lblPlaceholderColumn2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlaceholderColumn2h.Text;
                else if (lblLink2Direct2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLink2Direct2h.Text;
                else if (lblSortNumber2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSortNumber2h.Text;
                else if (lblColor2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtColor2h.Text;
                else if (lblMultiColor2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiColor2h.Text;
                else if (lblSelectColors2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSelectColors2h.Text;
                else if (lblBrand2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBrand2h.Text;
                else if (lblSKUProduction2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSKUProduction2h.Text;
                else if (lblSelectSize2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSelectSize2h.Text;
                else if (lblSize2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSize2h.Text;
                else if (lblMultiSizeItem2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiSizeItem2h.Text;
                else if (lblMainCategory2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMainCategory2h.Text;
                else if (lblMultiUOM2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiUOM2h.Text;
                else if (lblQuantityonHand2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantityonHand2h.Text;
                else if (lblwarranty2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtwarranty2h.Text;
                else if (lblKeywords2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeywords2h.Text;
                else if (lblProductStrategicalGroup2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductStrategicalGroup2h.Text;
                else if (lblDepartmentofSale2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDepartmentofSale2h.Text;
                else if (lblHotItem2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtHotItem2h.Text;
                else if (lblSerializedItem2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSerializedItem2h.Text;
                else if (lblPurchaseAllowed2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPurchaseAllowed2h.Text;
                else if (lblSaleAllowed2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSaleAllowed2h.Text;
                else if (lblMultiBinStore2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMultiBinStore2h.Text;
                else if (lblPerishable2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPerishable2h.Text;
                else if (lblProductMethod2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductMethod2h.Text;
                else if (lblSupplyMethod2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplyMethod2h.Text;
                else if (lblInternalNotes2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInternalNotes2h.Text;
                else if (lblCostPrice2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCostPrice2h.Text;
                else if (lblCurrency2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCurrency2h.Text;
                else if (lblSalePrice12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalePrice12h.Text;
                else if (lblWebSalePrice2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWebSalePrice2h.Text;
                else if (lblSalePrice22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalePrice22h.Text;
                else if (lblmsrp2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtmsrp2h.Text;
                else if (lblSalePrice32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSalePrice32h.Text;
                else if (lblAllowDirectSale2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAllowDirectSale2h.Text;
                else if (lblProductIDD2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductIDD2h.Text;
                else if (lblRemarks2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRemarks2h.Text;
                else if (lblLINK2DIRECTD2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLINK2DIRECTD2h.Text;
                else if (lblKeywordD2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeywordD2h.Text;
                else if (lblBoxshot2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBoxshot2h.Text;
                else if (lblLargeBoxshot2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLargeBoxshot2h.Text;
                else if (lblMediumBoxshot2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMediumBoxshot2h.Text;
                else if (lblSmallBoxshot2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSmallBoxshot2h.Text;
                else if (lblOsPlatform2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOsPlatform2h.Text;
                else if (lblCorpLogo2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCorpLogo2h.Text;
                else if (lblLadingPage2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLadingPage2h.Text;
                else if (lblLine2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLine2h.Text;
                else if (lblTrialUrl2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTrialUrl2h.Text;
                else if (lblCartLink2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCartLink2h.Text;
                else if (lblProductDetailLink2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtProductDetailLink2h.Text;
                else if (lblLead2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLead2h.Text;
                else if (lblOther2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOther2h.Text;
                else if (lblPromotionType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPromotionType2h.Text;
                else if (lblPayout2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPayout2h.Text;
                else if (lblLastDisplayDateonWeb2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLastDisplayDateonWeb2h.Text;
                else if (lblSystemRemarks2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSystemRemarks2h.Text;
                else if (lblLastPurchaseDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLastPurchaseDate2h.Text;
                else if (lblLastSalesDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtLastSalesDate2h.Text;
                else if (lblQuantityRecived2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantityRecived2h.Text;
                else if (lblQuantitySold2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantitySold2h.Text;
                else if (lblQuantityOnHandDP2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtQuantityOnHandDP2h.Text;
                else if (lblActiveDP2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActiveDP2h.Text;
                else if (lblSupplierList2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSupplierList2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\xml\\tbl_lbl_productMstNew.xml"));

        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((UserMaster)this.Master).getOwnPage();

            List<Database.Eco_TBLLabelDTL> List = ((UserMaster)this.Master).Bindxml("tbl_lbl_productMstNew").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.Eco_TBLLabelDTL item in List)
            {
                if (lblProductId1s.ID == item.LabelID)
                    txtProductId1s.Text = lblProductId1s.Text = item.LabelName;
                else if (lblProductNameMainLanguage1s.ID == item.LabelID)
                    txtProductNameMainLanguage1s.Text = lblProductNameMainLanguage1s.Text = item.LabelName;
                else if (lblBarcodeNo1s.ID == item.LabelID)
                    txtBarcodeNo1s.Text = lblBarcodeNo1s.Text = item.LabelName;
                else if (lblProductType1s.ID == item.LabelID)
                    txtProductType1s.Text = lblProductType1s.Text = item.LabelName;
                else if (lblAlternateCode11s.ID == item.LabelID)
                    txtAlternateCode11s.Text = lblAlternateCode11s.Text = item.LabelName;
                else if (lblAlternateCode21s.ID == item.LabelID)
                    txtAlternateCode21s.Text = lblAlternateCode21s.Text = item.LabelName;
                else if (lblProductNameLanguage21s.ID == item.LabelID)
                    txtProductNameLanguage21s.Text = lblProductNameLanguage21s.Text = item.LabelName;
                else if (lblProductNameLanguage31s.ID == item.LabelID)
                    txtProductNameLanguage31s.Text = lblProductNameLanguage31s.Text = item.LabelName;
                else if (lblShortName1s.ID == item.LabelID)
                    txtShortName1s.Text = lblShortName1s.Text = item.LabelName;
                else if (lblDescriptiontoPrint1s.ID == item.LabelID)
                    txtDescriptiontoPrint1s.Text = lblDescriptiontoPrint1s.Text = item.LabelName;
                else if (lblBusinessProduct1s.ID == item.LabelID)
                    txtBusinessProduct1s.Text = lblBusinessProduct1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblPlaceholderLine1s.ID == item.LabelID)
                    txtPlaceholderLine1s.Text = lblPlaceholderLine1s.Text = item.LabelName;
                else if (lblPlaceholderAliRL1s.ID == item.LabelID)
                    txtPlaceholderAliRL1s.Text = lblPlaceholderAliRL1s.Text = item.LabelName;
                else if (lblPlaceholderColumn1s.ID == item.LabelID)
                    txtPlaceholderColumn1s.Text = lblPlaceholderColumn1s.Text = item.LabelName;
                else if (lblLink2Direct1s.ID == item.LabelID)
                    txtLink2Direct1s.Text = lblLink2Direct1s.Text = item.LabelName;
                else if (lblSortNumber1s.ID == item.LabelID)
                    txtSortNumber1s.Text = lblSortNumber1s.Text = item.LabelName;
                else if (lblColor1s.ID == item.LabelID)
                    txtColor1s.Text = lblColor1s.Text = item.LabelName;
                else if (lblMultiColor1s.ID == item.LabelID)
                    txtMultiColor1s.Text = lblMultiColor1s.Text = item.LabelName;
                else if (lblSelectColors1s.ID == item.LabelID)
                    txtSelectColors1s.Text = lblSelectColors1s.Text = item.LabelName;
                else if (lblBrand1s.ID == item.LabelID)
                    txtBrand1s.Text = lblBrand1s.Text = item.LabelName;
                else if (lblSKUProduction1s.ID == item.LabelID)
                    txtSKUProduction1s.Text = lblSKUProduction1s.Text = item.LabelName;
                else if (lblSelectSize1s.ID == item.LabelID)
                    txtSelectSize1s.Text = lblSelectSize1s.Text = item.LabelName;
                else if (lblSize1s.ID == item.LabelID)
                    txtSize1s.Text = lblSize1s.Text = item.LabelName;
                else if (lblMultiSizeItem1s.ID == item.LabelID)
                    txtMultiSizeItem1s.Text = lblMultiSizeItem1s.Text = item.LabelName;
                else if (lblMainCategory1s.ID == item.LabelID)
                    txtMainCategory1s.Text = lblMainCategory1s.Text = item.LabelName;
                else if (lblMultiUOM1s.ID == item.LabelID)
                    txtMultiUOM1s.Text = lblMultiUOM1s.Text = item.LabelName;
                else if (lblQuantityonHand1s.ID == item.LabelID)
                    txtQuantityonHand1s.Text = lblQuantityonHand1s.Text = item.LabelName;
                else if (lblwarranty1s.ID == item.LabelID)
                    txtwarranty1s.Text = lblwarranty1s.Text = item.LabelName;
                else if (lblKeywords1s.ID == item.LabelID)
                    txtKeywords1s.Text = lblKeywords1s.Text = item.LabelName;
                else if (lblProductStrategicalGroup1s.ID == item.LabelID)
                    txtProductStrategicalGroup1s.Text = lblProductStrategicalGroup1s.Text = item.LabelName;
                else if (lblDepartmentofSale1s.ID == item.LabelID)
                    txtDepartmentofSale1s.Text = lblDepartmentofSale1s.Text = item.LabelName;
                else if (lblHotItem1s.ID == item.LabelID)
                    txtHotItem1s.Text = lblHotItem1s.Text = item.LabelName;
                else if (lblSerializedItem1s.ID == item.LabelID)
                    txtSerializedItem1s.Text = lblSerializedItem1s.Text = item.LabelName;
                else if (lblPurchaseAllowed1s.ID == item.LabelID)
                    txtPurchaseAllowed1s.Text = lblPurchaseAllowed1s.Text = item.LabelName;
                else if (lblSaleAllowed1s.ID == item.LabelID)
                    txtSaleAllowed1s.Text = lblSaleAllowed1s.Text = item.LabelName;
                else if (lblMultiBinStore1s.ID == item.LabelID)
                    txtMultiBinStore1s.Text = lblMultiBinStore1s.Text = item.LabelName;
                else if (lblPerishable1s.ID == item.LabelID)
                    txtPerishable1s.Text = lblPerishable1s.Text = item.LabelName;
                else if (lblProductMethod1s.ID == item.LabelID)
                    txtProductMethod1s.Text = lblProductMethod1s.Text = item.LabelName;
                else if (lblSupplyMethod1s.ID == item.LabelID)
                    txtSupplyMethod1s.Text = lblSupplyMethod1s.Text = item.LabelName;
                else if (lblInternalNotes1s.ID == item.LabelID)
                    txtInternalNotes1s.Text = lblInternalNotes1s.Text = item.LabelName;
                else if (lblCostPrice1s.ID == item.LabelID)
                    txtCostPrice1s.Text = lblCostPrice1s.Text = item.LabelName;
                else if (lblCurrency1s.ID == item.LabelID)
                    txtCurrency1s.Text = lblCurrency1s.Text = item.LabelName;
                else if (lblSalePrice11s.ID == item.LabelID)
                    txtSalePrice11s.Text = lblSalePrice11s.Text = item.LabelName;
                else if (lblWebSalePrice1s.ID == item.LabelID)
                    txtWebSalePrice1s.Text = lblWebSalePrice1s.Text = item.LabelName;
                else if (lblSalePrice21s.ID == item.LabelID)
                    txtSalePrice21s.Text = lblSalePrice21s.Text = item.LabelName;
                else if (lblmsrp1s.ID == item.LabelID)
                    txtmsrp1s.Text = lblmsrp1s.Text = item.LabelName;
                else if (lblSalePrice31s.ID == item.LabelID)
                    txtSalePrice31s.Text = lblSalePrice31s.Text = item.LabelName;
                else if (lblAllowDirectSale1s.ID == item.LabelID)
                    txtAllowDirectSale1s.Text = lblAllowDirectSale1s.Text = item.LabelName;
                else if (lblProductIDD1s.ID == item.LabelID)
                    txtProductIDD1s.Text = lblProductIDD1s.Text = item.LabelName;
                else if (lblRemarks1s.ID == item.LabelID)
                    txtRemarks1s.Text = lblRemarks1s.Text = item.LabelName;
                else if (lblLINK2DIRECTD1s.ID == item.LabelID)
                    txtLINK2DIRECTD1s.Text = lblLINK2DIRECTD1s.Text = item.LabelName;
                else if (lblKeywordD1s.ID == item.LabelID)
                    txtKeywordD1s.Text = lblKeywordD1s.Text = item.LabelName;
                else if (lblBoxshot1s.ID == item.LabelID)
                    txtBoxshot1s.Text = lblBoxshot1s.Text = item.LabelName;
                else if (lblLargeBoxshot1s.ID == item.LabelID)
                    txtLargeBoxshot1s.Text = lblLargeBoxshot1s.Text = item.LabelName;
                else if (lblMediumBoxshot1s.ID == item.LabelID)
                    txtMediumBoxshot1s.Text = lblMediumBoxshot1s.Text = item.LabelName;
                else if (lblSmallBoxshot1s.ID == item.LabelID)
                    txtSmallBoxshot1s.Text = lblSmallBoxshot1s.Text = item.LabelName;
                else if (lblOsPlatform1s.ID == item.LabelID)
                    txtOsPlatform1s.Text = lblOsPlatform1s.Text = item.LabelName;
                else if (lblCorpLogo1s.ID == item.LabelID)
                    txtCorpLogo1s.Text = lblCorpLogo1s.Text = item.LabelName;
                else if (lblLadingPage1s.ID == item.LabelID)
                    txtLadingPage1s.Text = lblLadingPage1s.Text = item.LabelName;
                else if (lblLine1s.ID == item.LabelID)
                    txtLine1s.Text = lblLine1s.Text = item.LabelName;
                else if (lblTrialUrl1s.ID == item.LabelID)
                    txtTrialUrl1s.Text = lblTrialUrl1s.Text = item.LabelName;
                else if (lblCartLink1s.ID == item.LabelID)
                    txtCartLink1s.Text = lblCartLink1s.Text = item.LabelName;
                else if (lblProductDetailLink1s.ID == item.LabelID)
                    txtProductDetailLink1s.Text = lblProductDetailLink1s.Text = item.LabelName;
                else if (lblLead1s.ID == item.LabelID)
                    txtLead1s.Text = lblLead1s.Text = item.LabelName;
                else if (lblOther1s.ID == item.LabelID)
                    txtOther1s.Text = lblOther1s.Text = item.LabelName;
                else if (lblPromotionType1s.ID == item.LabelID)
                    txtPromotionType1s.Text = lblPromotionType1s.Text = item.LabelName;
                else if (lblPayout1s.ID == item.LabelID)
                    txtPayout1s.Text = lblPayout1s.Text = item.LabelName;
                else if (lblLastDisplayDateonWeb1s.ID == item.LabelID)
                    txtLastDisplayDateonWeb1s.Text = lblLastDisplayDateonWeb1s.Text = item.LabelName;
                else if (lblSystemRemarks1s.ID == item.LabelID)
                    txtSystemRemarks1s.Text = lblSystemRemarks1s.Text = item.LabelName;
                else if (lblLastPurchaseDate1s.ID == item.LabelID)
                    txtLastPurchaseDate1s.Text = lblLastPurchaseDate1s.Text = item.LabelName;
                else if (lblLastSalesDate1s.ID == item.LabelID)
                    txtLastSalesDate1s.Text = lblLastSalesDate1s.Text = item.LabelName;
                else if (lblQuantityRecived1s.ID == item.LabelID)
                    txtQuantityRecived1s.Text = lblQuantityRecived1s.Text = item.LabelName;
                else if (lblQuantitySold1s.ID == item.LabelID)
                    txtQuantitySold1s.Text = lblQuantitySold1s.Text = item.LabelName;
                else if (lblQuantityOnHandDP1s.ID == item.LabelID)
                    txtQuantityOnHandDP1s.Text = lblQuantityOnHandDP1s.Text = item.LabelName;
                else if (lblActiveDP1s.ID == item.LabelID)
                    txtActiveDP1s.Text = lblActiveDP1s.Text = item.LabelName;
                else if (lblSupplierList1s.ID == item.LabelID)
                    txtSupplierList1s.Text = lblSupplierList1s.Text = item.LabelName;

                if (lblProductId2h.ID == item.LabelID)
                    txtProductId2h.Text = lblProductId2h.Text = item.LabelName;
                else if (lblProductNameMainLanguage2h.ID == item.LabelID)
                    txtProductNameMainLanguage2h.Text = lblProductNameMainLanguage2h.Text = item.LabelName;
                else if (lblBarcodeNo2h.ID == item.LabelID)
                    txtBarcodeNo2h.Text = lblBarcodeNo2h.Text = item.LabelName;
                else if (lblProductType2h.ID == item.LabelID)
                    txtProductType2h.Text = lblProductType2h.Text = item.LabelName;
                else if (lblAlternateCode12h.ID == item.LabelID)
                    txtAlternateCode12h.Text = lblAlternateCode12h.Text = item.LabelName;
                else if (lblAlternateCode22h.ID == item.LabelID)
                    txtAlternateCode22h.Text = lblAlternateCode22h.Text = item.LabelName;
                else if (lblProductNameLanguage22h.ID == item.LabelID)
                    txtProductNameLanguage22h.Text = lblProductNameLanguage22h.Text = item.LabelName;
                else if (lblProductNameLanguage32h.ID == item.LabelID)
                    txtProductNameLanguage32h.Text = lblProductNameLanguage32h.Text = item.LabelName;
                else if (lblShortName2h.ID == item.LabelID)
                    txtShortName2h.Text = lblShortName2h.Text = item.LabelName;
                else if (lblDescriptiontoPrint2h.ID == item.LabelID)
                    txtDescriptiontoPrint2h.Text = lblDescriptiontoPrint2h.Text = item.LabelName;
                else if (lblBusinessProduct2h.ID == item.LabelID)
                    txtBusinessProduct2h.Text = lblBusinessProduct2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblPlaceholderLine2h.ID == item.LabelID)
                    txtPlaceholderLine2h.Text = lblPlaceholderLine2h.Text = item.LabelName;
                else if (lblPlaceholderAliRL2h.ID == item.LabelID)
                    txtPlaceholderAliRL2h.Text = lblPlaceholderAliRL2h.Text = item.LabelName;
                else if (lblPlaceholderColumn2h.ID == item.LabelID)
                    txtPlaceholderColumn2h.Text = lblPlaceholderColumn2h.Text = item.LabelName;
                else if (lblLink2Direct2h.ID == item.LabelID)
                    txtLink2Direct2h.Text = lblLink2Direct2h.Text = item.LabelName;
                else if (lblSortNumber2h.ID == item.LabelID)
                    txtSortNumber2h.Text = lblSortNumber2h.Text = item.LabelName;
                else if (lblColor2h.ID == item.LabelID)
                    txtColor2h.Text = lblColor2h.Text = item.LabelName;
                else if (lblMultiColor2h.ID == item.LabelID)
                    txtMultiColor2h.Text = lblMultiColor2h.Text = item.LabelName;
                else if (lblSelectColors2h.ID == item.LabelID)
                    txtSelectColors2h.Text = lblSelectColors2h.Text = item.LabelName;
                else if (lblBrand2h.ID == item.LabelID)
                    txtBrand2h.Text = lblBrand2h.Text = item.LabelName;
                else if (lblSKUProduction2h.ID == item.LabelID)
                    txtSKUProduction2h.Text = lblSKUProduction2h.Text = item.LabelName;
                else if (lblSelectSize2h.ID == item.LabelID)
                    txtSelectSize2h.Text = lblSelectSize2h.Text = item.LabelName;
                else if (lblSize2h.ID == item.LabelID)
                    txtSize2h.Text = lblSize2h.Text = item.LabelName;
                else if (lblMultiSizeItem2h.ID == item.LabelID)
                    txtMultiSizeItem2h.Text = lblMultiSizeItem2h.Text = item.LabelName;
                else if (lblMainCategory2h.ID == item.LabelID)
                    txtMainCategory2h.Text = lblMainCategory2h.Text = item.LabelName;
                else if (lblMultiUOM2h.ID == item.LabelID)
                    txtMultiUOM2h.Text = lblMultiUOM2h.Text = item.LabelName;
                else if (lblQuantityonHand2h.ID == item.LabelID)
                    txtQuantityonHand2h.Text = lblQuantityonHand2h.Text = item.LabelName;
                else if (lblwarranty2h.ID == item.LabelID)
                    txtwarranty2h.Text = lblwarranty2h.Text = item.LabelName;
                else if (lblKeywords2h.ID == item.LabelID)
                    txtKeywords2h.Text = lblKeywords2h.Text = item.LabelName;
                else if (lblProductStrategicalGroup2h.ID == item.LabelID)
                    txtProductStrategicalGroup2h.Text = lblProductStrategicalGroup2h.Text = item.LabelName;
                else if (lblDepartmentofSale2h.ID == item.LabelID)
                    txtDepartmentofSale2h.Text = lblDepartmentofSale2h.Text = item.LabelName;
                else if (lblHotItem2h.ID == item.LabelID)
                    txtHotItem2h.Text = lblHotItem2h.Text = item.LabelName;
                else if (lblSerializedItem2h.ID == item.LabelID)
                    txtSerializedItem2h.Text = lblSerializedItem2h.Text = item.LabelName;
                else if (lblPurchaseAllowed2h.ID == item.LabelID)
                    txtPurchaseAllowed2h.Text = lblPurchaseAllowed2h.Text = item.LabelName;
                else if (lblSaleAllowed2h.ID == item.LabelID)
                    txtSaleAllowed2h.Text = lblSaleAllowed2h.Text = item.LabelName;
                else if (lblMultiBinStore2h.ID == item.LabelID)
                    txtMultiBinStore2h.Text = lblMultiBinStore2h.Text = item.LabelName;
                else if (lblPerishable2h.ID == item.LabelID)
                    txtPerishable2h.Text = lblPerishable2h.Text = item.LabelName;
                else if (lblProductMethod2h.ID == item.LabelID)
                    txtProductMethod2h.Text = lblProductMethod2h.Text = item.LabelName;
                else if (lblSupplyMethod2h.ID == item.LabelID)
                    txtSupplyMethod2h.Text = lblSupplyMethod2h.Text = item.LabelName;
                else if (lblInternalNotes2h.ID == item.LabelID)
                    txtInternalNotes2h.Text = lblInternalNotes2h.Text = item.LabelName;
                else if (lblCostPrice2h.ID == item.LabelID)
                    txtCostPrice2h.Text = lblCostPrice2h.Text = item.LabelName;
                else if (lblCurrency2h.ID == item.LabelID)
                    txtCurrency2h.Text = lblCurrency2h.Text = item.LabelName;
                else if (lblSalePrice12h.ID == item.LabelID)
                    txtSalePrice12h.Text = lblSalePrice12h.Text = item.LabelName;
                else if (lblWebSalePrice2h.ID == item.LabelID)
                    txtWebSalePrice2h.Text = lblWebSalePrice2h.Text = item.LabelName;
                else if (lblSalePrice22h.ID == item.LabelID)
                    txtSalePrice22h.Text = lblSalePrice22h.Text = item.LabelName;
                else if (lblmsrp2h.ID == item.LabelID)
                    txtmsrp2h.Text = lblmsrp2h.Text = item.LabelName;
                else if (lblSalePrice32h.ID == item.LabelID)
                    txtSalePrice32h.Text = lblSalePrice32h.Text = item.LabelName;
                else if (lblAllowDirectSale2h.ID == item.LabelID)
                    txtAllowDirectSale2h.Text = lblAllowDirectSale2h.Text = item.LabelName;
                else if (lblProductIDD2h.ID == item.LabelID)
                    txtProductIDD2h.Text = lblProductIDD2h.Text = item.LabelName;
                else if (lblRemarks2h.ID == item.LabelID)
                    txtRemarks2h.Text = lblRemarks2h.Text = item.LabelName;
                else if (lblLINK2DIRECTD2h.ID == item.LabelID)
                    txtLINK2DIRECTD2h.Text = lblLINK2DIRECTD2h.Text = item.LabelName;
                else if (lblKeywordD2h.ID == item.LabelID)
                    txtKeywordD2h.Text = lblKeywordD2h.Text = item.LabelName;
                else if (lblBoxshot2h.ID == item.LabelID)
                    txtBoxshot2h.Text = lblBoxshot2h.Text = item.LabelName;
                else if (lblLargeBoxshot2h.ID == item.LabelID)
                    txtLargeBoxshot2h.Text = lblLargeBoxshot2h.Text = item.LabelName;
                else if (lblMediumBoxshot2h.ID == item.LabelID)
                    txtMediumBoxshot2h.Text = lblMediumBoxshot2h.Text = item.LabelName;
                else if (lblSmallBoxshot2h.ID == item.LabelID)
                    txtSmallBoxshot2h.Text = lblSmallBoxshot2h.Text = item.LabelName;
                else if (lblOsPlatform2h.ID == item.LabelID)
                    txtOsPlatform2h.Text = lblOsPlatform2h.Text = item.LabelName;
                else if (lblCorpLogo2h.ID == item.LabelID)
                    txtCorpLogo2h.Text = lblCorpLogo2h.Text = item.LabelName;
                else if (lblLadingPage2h.ID == item.LabelID)
                    txtLadingPage2h.Text = lblLadingPage2h.Text = item.LabelName;
                else if (lblLine2h.ID == item.LabelID)
                    txtLine2h.Text = lblLine2h.Text = item.LabelName;
                else if (lblTrialUrl2h.ID == item.LabelID)
                    txtTrialUrl2h.Text = lblTrialUrl2h.Text = item.LabelName;
                else if (lblCartLink2h.ID == item.LabelID)
                    txtCartLink2h.Text = lblCartLink2h.Text = item.LabelName;
                else if (lblProductDetailLink2h.ID == item.LabelID)
                    txtProductDetailLink2h.Text = lblProductDetailLink2h.Text = item.LabelName;
                else if (lblLead2h.ID == item.LabelID)
                    txtLead2h.Text = lblLead2h.Text = item.LabelName;
                else if (lblOther2h.ID == item.LabelID)
                    txtOther2h.Text = lblOther2h.Text = item.LabelName;
                else if (lblPromotionType2h.ID == item.LabelID)
                    txtPromotionType2h.Text = lblPromotionType2h.Text = item.LabelName;
                else if (lblPayout2h.ID == item.LabelID)
                    txtPayout2h.Text = lblPayout2h.Text = item.LabelName;
                else if (lblLastDisplayDateonWeb2h.ID == item.LabelID)
                    txtLastDisplayDateonWeb2h.Text = lblLastDisplayDateonWeb2h.Text = item.LabelName;
                else if (lblSystemRemarks2h.ID == item.LabelID)
                    txtSystemRemarks2h.Text = lblSystemRemarks2h.Text = item.LabelName;
                else if (lblLastPurchaseDate2h.ID == item.LabelID)
                    txtLastPurchaseDate2h.Text = lblLastPurchaseDate2h.Text = item.LabelName;
                else if (lblLastSalesDate2h.ID == item.LabelID)
                    txtLastSalesDate2h.Text = lblLastSalesDate2h.Text = item.LabelName;
                else if (lblQuantityRecived2h.ID == item.LabelID)
                    txtQuantityRecived2h.Text = lblQuantityRecived2h.Text = item.LabelName;
                else if (lblQuantitySold2h.ID == item.LabelID)
                    txtQuantitySold2h.Text = lblQuantitySold2h.Text = item.LabelName;
                else if (lblQuantityOnHandDP2h.ID == item.LabelID)
                    txtQuantityOnHandDP2h.Text = lblQuantityOnHandDP2h.Text = item.LabelName;
                else if (lblActiveDP2h.ID == item.LabelID)
                    txtActiveDP2h.Text = lblActiveDP2h.Text = item.LabelName;
                else if (lblSupplierList2h.ID == item.LabelID)
                    txtSupplierList2h.Text = lblSupplierList2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = item.LabelName;
            }

        }
        protected void dorpMultiSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            int SID = Convert.ToInt32(dorpMultiSize.SelectedValue);
            string SizeName = DB.Eco_TBLSIZE.Single(p => p.SIZECODE == SID).SIZETYPE;
            if (ViewState["StrMultiSize"] != null)
            {
                ViewState["StrMultiSize"] += "/" + SizeName;
                txtMultiSize.Text = ViewState["StrMultiSize"].ToString();

            }
            else
            {
                txtMultiSize.Text = SizeName.ToString();
                ViewState["StrMultiSize"] = SizeName.ToString();
            }


        }
        protected void rdbtnMUOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            if (rdbtnMUOM.SelectedValue == "1")
            {
                ddluom.Visible = true;
                panelMultiUOM.Visible = true;
                ddluom.DataSource = DB.Eco_ICUOM.Where(p => p.TenantID == TID && p.Active == "Y");
                ddluom.DataTextField = "UOMNAME1";
                ddluom.DataValueField = "UOM";
                ddluom.DataBind();
                ddluom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Unit of Measure--", "0"));

            }
            else
            {
                ddluom.Visible = false;
                panelMultiUOM.Visible = false;

            }

        }

        protected void ddluom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            int UID = Convert.ToInt32(ddluom.SelectedValue);
            string ColerName = DB.Eco_ICUOM.Single(p => p.UOM == UID).UOMNAME1;
            if (ViewState["StrMultiMOU"] != null)
            {
                ViewState["StrMultiMOU"] += "/" + ColerName;
                txtmultiMOU.Text = ViewState["StrMultiMOU"].ToString();


            }
            else
            {
                txtmultiMOU.Text = ColerName.ToString();
                ViewState["StrMultiMOU"] = ColerName.ToString();

            }

        }

        protected void btnbrand_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            if (ddlBrandname.SelectedValue == "0")
            {
                string display = "Select The Brand";
                panelBrandmsg.Visible = true;
                lblBrandmsg.Text = display;
                return;
            }
            else
            {
                int Refid = Convert.ToInt32(ddlBrandname.SelectedValue);
                string resubtype = DB.Eco_REFTABLE.Single(p => p.REFID == Refid).REFSUBTYPE;
                string reftype = DB.Eco_REFTABLE.Single(p => p.REFID == Refid).REFTYPE;
                int MYPODID = Convert.ToInt32(lblmstproid.Text);
                if (txtbrandpl.Text != "0" && txtbrandpcl.Text != "0" && txtbrandsort.Text != "0")
                {
                    Database.Eco_ICProdDisplay objICProdDisplay = new Database.Eco_ICProdDisplay();
                    objICProdDisplay.TenantID = TID;
                    objICProdDisplay.MYPRODID = MYPODID;
                    objICProdDisplay.Display_Id = DB.Eco_ICProdDisplay.Count() > 0 ? Convert.ToInt32(DB.Eco_ICProdDisplay.Max(p => p.Display_Id) + 1) : 1;
                    objICProdDisplay.REFID = Refid;
                    objICProdDisplay.REFTYPE = reftype;
                    objICProdDisplay.REFSUBTYPE = resubtype;
                    objICProdDisplay.TableName = "Brand";
                    objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtbrandpl.Text);
                    objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtbrandparl.Text);
                    objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtbrandpcl.Text);
                    objICProdDisplay.LINK2DIRECT = txtbrandl2d.Text;
                    objICProdDisplay.sortnumber = Convert.ToInt32(txtbrandsort.Text);
                    objICProdDisplay.ACTIVE2 = true;
                    DB.Eco_ICProdDisplay.AddObject(objICProdDisplay);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }

            }

        }

        protected void btncategory_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            if (ddlSuppCat.SelectedValue == "0")
            {
                string display = "Select The Category";
                pnelCategorymsg.Visible = true;
                lblCategoryMsg.Text = display;
                return;
            }
            else
            {
                int catid = Convert.ToInt32(ddlSuppCat.SelectedValue);
                string resubtype = DB.Eco_CAT_MST.Single(p => p.CATID == catid).SHORT_NAME;
                string reftype = DB.Eco_CAT_MST.Single(p => p.CATID == catid).SHORT_NAME;
                int MYPODID = Convert.ToInt32(lblmstproid.Text);
                if (txtcatepl.Text != "0" && txtcatepcol.Text != "0" && txtcatsornum.Text != "0")
                {
                    Database.Eco_ICProdDisplay objICProdDisplay = new Database.Eco_ICProdDisplay();
                    objICProdDisplay.TenantID = TID;
                    objICProdDisplay.MYPRODID = MYPODID;
                    objICProdDisplay.Display_Id = DB.Eco_ICProdDisplay.Count() > 0 ? Convert.ToInt32(DB.Eco_ICProdDisplay.Max(p => p.Display_Id) + 1) : 1;
                    objICProdDisplay.REFID = catid;
                    objICProdDisplay.REFTYPE = reftype;
                    objICProdDisplay.REFSUBTYPE = resubtype;
                    objICProdDisplay.TableName = "category";
                    objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtcatepl.Text);
                    objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtcateparl.Text);
                    objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtcatepcol.Text);
                    objICProdDisplay.LINK2DIRECT = txtcatl2d.Text;
                    objICProdDisplay.sortnumber = Convert.ToInt32(txtcatsornum.Text);
                    objICProdDisplay.ACTIVE2 = true;
                    DB.Eco_ICProdDisplay.AddObject(objICProdDisplay);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }

            }

        }



        protected void dropMulticolor_SelectedIndexChanged1(object sender, EventArgs e)
        {


            string script = "window.onload = function() { showhidepanelMoreItem('lst'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelMoreItem", script, true);
            int CID = Convert.ToInt32(dropMulticolor.SelectedValue);
            string ColerName = DB.Eco_TBLCOLOR.Single(p => p.COLORID == CID).COLORDESC1;
            if (ViewState["strMulticolor"] != null)
            {
                ViewState["strMulticolor"] += "/" + ColerName;
                txtMulticolers.Text = ViewState["strMulticolor"].ToString();


            }
            else
            {
                txtMulticolers.Text = ColerName.ToString();
                ViewState["strMulticolor"] = ColerName.ToString();


            }
        }

        protected void linkButtonWebProd_Click(object sender, EventArgs e)
        {
            int MYPODID = Convert.ToInt32(lblmstproid.Text);
            string Mode = ViewState["Mode"].ToString();
            Response.Redirect("SupplierWebProd.aspx?MYPRODID=" + MYPODID + "&MODE=" + Mode);
        }


        protected void linktoprodMaster_Click(object sender, EventArgs e)
        {
            int MYPODID = Convert.ToInt32(lblmstproid.Text);
            string Mode = ViewState["Mode"].ToString();
            Response.Redirect("SupplierWebProd.aspx?MYPRODID=" + MYPODID + "&MODE=" + Mode);
        }

        protected void btnupcuntinu_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
                int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
                int LID = 1;
                if (ViewState["Product"] != null)
                {
                    int myprodid = Convert.ToInt32(ViewState["Product"]);
                    Database.Eco_TBLPRODUCT objproduct = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == myprodid);
                    objproduct.TenantID = TID;
                    objproduct.MYPRODID = myprodid;
                    objproduct.BarCode = txtProBar.Text;
                    objproduct.AlternateCode1 = txtACode1.Text;
                    objproduct.AlternateCode2 = txtACode1.Text;
                    objproduct.UOM = Convert.ToInt32(drpUOM.SelectedValue);
                    objproduct.GROUPID = Convert.ToInt32(ddlItGrp.SelectedValue);
                    objproduct.SIZECODE = Convert.ToInt32(ddlsize.SelectedValue);
                    objproduct.ProdTypeRefId = Convert.ToInt32(ddlProdType.SelectedValue);
                    objproduct.SupplyMethodID = Convert.ToInt32(ddlMoreItenSupplyMethod.SelectedValue);
                    objproduct.ProdMethodID = Convert.ToInt32(ddlMoreItenProMethod.SelectedValue);
                    objproduct.SalDeptID = Convert.ToInt32(ddlDofSale.SelectedValue);
                    objproduct.ShortName = txtShortName.Text;
                    objproduct.DescToprint = txtDescToprint.Text;
                    objproduct.ProdName1 = pm_txtProductName.Text;
                    objproduct.ProdName2 = txtItemRelSubProName2.Text;
                    objproduct.ProdName3 = txtItemRelSubProName3.Text;
                    objproduct.Brand = ddlBrandname.SelectedValue;
                    if (txtQtnonHand.Text == "")
                    {
                        objproduct.QTYINHAND = null;
                    }
                    else
                    {
                        objproduct.QTYINHAND = Convert.ToInt32(txtQtnonHand.Text);
                    }
                    objproduct.REMARKS = null;
                    if (ddlSuppCat.SelectedValue == "0")
                        objproduct.MainCategoryID = null;
                    else
                        objproduct.MainCategoryID = Convert.ToInt32(ddlSuppCat.SelectedValue);
                    if (txtnotepricostprice.Text == "")
                        objproduct.basecost = null;
                    else
                        objproduct.basecost = Convert.ToDecimal(txtnotepricostprice.Text);
                    if (txtnoteprisaleori1.Text == "")
                        objproduct.onlinesale1 = null;
                    else
                        objproduct.onlinesale1 = Convert.ToDecimal(txtnoteprisaleori1.Text);

                    if (txtnoteprisaleori2.Text == "")
                        objproduct.onlinesale2 = null;
                    else
                        objproduct.onlinesale2 = Convert.ToDecimal(txtnoteprisaleori2.Text);

                    if (txtnoteprimrp.Text == "")
                        objproduct.msrp = null;
                    else
                        objproduct.msrp = Convert.ToDecimal(txtnoteprimrp.Text);

                    if (txtnotepriwebsalepri.Text == "")
                        objproduct.price = null;
                    else
                        objproduct.price = Convert.ToDecimal(txtnotepriwebsalepri.Text);


                    objproduct.currency = drpCurrency.SelectedValue;

                    //if (txtINFOQntyonhand.Text == "")
                    //    objproduct.QTYINHAND = null;
                    //else
                    //    objproduct.QTYINHAND = Convert.ToInt32(txtINFOQntyonhand.Text);

                    if (txtINFOQntyRes.Text == "")
                        objproduct.QTYRCVD = null;
                    else
                        objproduct.QTYRCVD = Convert.ToInt32(txtINFOQntyRes.Text);

                    if (txtINFOQntySold.Text == "")
                        objproduct.QTYSOLD = null;
                    else
                        objproduct.QTYSOLD = Convert.ToInt32(txtINFOQntySold.Text);

                    objproduct.PICTURE = null;
                    objproduct.ACTIVE = "1";
                    objproduct.DIRECTSALE = "1";
                    //  objproduct.LINK2DIRECT = txtMoreItenwarrantyLink2d.Text;
                    objproduct.DISPDATE3 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                    if (txtWarranty.Text == null)
                    {
                        objproduct.Warranty = null;
                    }
                    else
                    {
                        objproduct.Warranty = txtWarranty.Text;
                    }
                    if (txtINFOlastpurchdt.Text == "")
                        objproduct.LASTPURDATE = null;
                    else
                        objproduct.LASTPURDATE = Convert.ToDateTime(txtINFOlastpurchdt.Text);
                    if (txtINFOlastsaledt.Text == "")
                        objproduct.LASTSALDATE = null;
                    else
                        objproduct.LASTSALDATE = Convert.ToDateTime(txtINFOlastsaledt.Text);
                    objproduct.COLORID = dropColoerOne.SelectedValue;
                    if (txtnoteprisaleori3.Text == "")
                    {
                        objproduct.onlinesale3 = 0;
                    }
                    else
                    {
                        objproduct.onlinesale3 = Convert.ToDecimal(txtnoteprisaleori3.Text);
                    }
                    if (txtnoteprinote.Text == "")
                    {
                        objproduct.InternalNotes = null;
                    }
                    else
                    {
                        objproduct.InternalNotes = txtnoteprinote.Text;
                    }
                    //  objproduct.COLORID = txtxitemchcolore.Text;
                    if (txtnoteprisaleori3.Text == "")
                    {
                        objproduct.onlinesale3 = 0;
                    }
                    else
                    {
                        objproduct.onlinesale3 = Convert.ToDecimal(txtnoteprisaleori3.Text);
                    }
                    if (txtnoteprinote.Text == "")
                    {
                        objproduct.InternalNotes = null;
                    }
                    else
                    {
                        objproduct.InternalNotes = txtnoteprinote.Text;
                    }
                    if (tags_4.Text == "")
                    {
                        objproduct.keywords = null;
                    }
                    else
                    {
                        objproduct.keywords = tags_4.Text;
                    }
                    objproduct.Serialized = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenSerItem.SelectedValue));
                    objproduct.HotItem = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenHotItem.SelectedValue));
                    objproduct.SKUProduction = Convert.ToBoolean(Convert.ToInt32(rbtnSKUP.SelectedValue));
                    objproduct.SKU = null;
                    objproduct.MultiSize = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenMultisize.SelectedValue));

                    if (RadioBtnMoreItenMultisize.SelectedValue == "1")
                    {

                        string[] Seperate5 = txtMultiSize.Text.Split('/');
                        int count5 = 0;
                        string Sep5 = "";
                        for (int i = 0; i <= Seperate5.Count() - 1; i++)
                        {
                            Sep5 = Seperate5[i];
                            string Name = Sep5.ToString();
                            count5++;
                            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == myprodid && p.RecValue == Name).Count() > 0)
                            {

                            }
                            else
                            {
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiSize";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = myprodid;
                                obj.RunSerial = count5;
                                obj.Recource = 5008;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate5[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST1.IsCache = true;
                                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                                DB.SaveChanges();


                            }

                        }

                    }
                    objproduct.MultiUOM = Convert.ToBoolean(Convert.ToInt32(rdbtnMUOM.SelectedValue));
                    if (rdbtnMUOM.SelectedValue == "1")
                    {
                        string[] Seperate1 = txtmultiMOU.Text.Split('/');
                        int count1 = 0;
                        string Sep1 = "";
                        for (int i = 0; i <= Seperate1.Count() - 1; i++)
                        {
                            Sep1 = Seperate1[i];
                            string Name = Sep1.ToString();
                            count1++;
                            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == myprodid && p.RecValue == Name).Count() > 0)
                            {

                            }
                            else
                            {
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiUOM";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = myprodid;
                                obj.RunSerial = count1;
                                obj.Recource = 5009;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate1[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST1.IsCache = true;
                                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                    }
                    objproduct.MultiColor = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenMulticolor.SelectedValue));
                    if (RadioBtnMoreItenMulticolor.SelectedValue == "1")
                    {

                        string[] Seperate4 = txtMulticolers.Text.Split('/');
                        int count4 = 0;
                        string Sep4 = "";
                        for (int i = 0; i <= Seperate4.Count() - 1; i++)
                        {
                            Sep4 = Seperate4[i];
                            string Name = Sep4.ToString();
                            count4++;
                            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == myprodid && p.RecValue == Name).Count() > 0)
                            {

                            }
                            else
                            {
                                Eco_Tbl_Multi_Color_Size_Mst obj = new Eco_Tbl_Multi_Color_Size_Mst();
                                obj.TenantID = TID;
                                obj.RecordType = "MultiColor";
                                obj.RecTypeID = DB.Eco_Tbl_Multi_Color_Size_Mst.Count() > 0 ? Convert.ToInt32(DB.Eco_Tbl_Multi_Color_Size_Mst.Max(p => p.RecTypeID) + 1) : 1;
                                obj.CompniyAndContactID = myprodid;
                                obj.RunSerial = count4;
                                obj.Recource = 5007;
                                obj.RecourceName = "Product";
                                obj.RecValue = Seperate4[i];
                                obj.Active = true;
                                // obj.Rremark = "AutomatedProcess";
                                DB.Eco_Tbl_Multi_Color_Size_Mst.AddObject(obj);
                                DB.SaveChanges();
                                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_Tbl_Multi_Color_Size_Mst");
                                objEco_ERP_WEB_USER_MST1.IsCache = true;
                                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                    }

                    objproduct.MultiBinStore = Convert.ToBoolean(Convert.ToInt32(rbtnMBSto.SelectedValue));
                    objproduct.Perishable = Convert.ToBoolean(Convert.ToInt32(rbtPsle.SelectedValue));
                    objproduct.SaleAllow = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenSellAllow.SelectedValue));
                    objproduct.PurAllow = Convert.ToBoolean(Convert.ToInt32(RadioBtnMoreItenPurchAllow.SelectedValue));
                    Database.Eco_TBLPRODDTL objoddtl = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == myprodid);
                    objoddtl.TenantID = TID;
                    // objoddtl.MYPRODID = Convert.ToInt32(ProSaleFenProid.Text);
                    if (txtProSaleFenRemark.Text == "")
                    {
                        objoddtl.REMARKS = "";
                    }
                    else
                    {
                        objoddtl.REMARKS = txtProSaleFenRemark.Text;
                    }

                    objoddtl.ACTIVE = true;

                    if (txtLINK2DIRECT.Text == "")
                    {
                        objoddtl.LINK2DIRECT = "";
                    }
                    else
                    {
                        objoddtl.LINK2DIRECT = txtLINK2DIRECT.Text;
                    }
                    if (tags_3.Text == "")
                    {
                        objoddtl.keywords = "";
                    }
                    else
                    {
                        objoddtl.keywords = tags_3.Text;
                    }
                    if (txtboxshot.Text == "")
                    {
                        objoddtl.boxshot = "";
                    }
                    else
                    {
                        objoddtl.boxshot = txtboxshot.Text;
                    }
                    if (txtlarge_boxshot.Text == "")
                    {
                        objoddtl.large_boxshot = "";
                    }
                    else
                    {
                        objoddtl.large_boxshot = txtlarge_boxshot.Text;
                    }
                    if (txtmedium_boxshot.Text == "")
                    {
                        objoddtl.medium_boxshot = "";
                    }
                    else
                    {
                        objoddtl.medium_boxshot = txtmedium_boxshot.Text;
                    }
                    if (txtsmall_boxshot.Text == "")
                    {
                        objoddtl.small_boxshot = "";
                    }
                    else
                    {
                        objoddtl.small_boxshot = txtsmall_boxshot.Text;
                    }
                    if (txtos_platform.Text == "")
                    {
                        objoddtl.os_platform = "";
                    }
                    else
                    {
                        objoddtl.os_platform = txtos_platform.Text;
                    }
                    if (txtcorp_logo.Text == "")
                    {
                        objoddtl.corp_logo = "";
                    }
                    else
                    {
                        objoddtl.corp_logo = txtcorp_logo.Text;
                    }
                    if (txtline.Text == "")
                    {
                        objoddtl.link = "";
                    }
                    else
                    {
                        objoddtl.link = txtline.Text;
                    }
                    if (txttrial_url.Text == "")
                    {
                        objoddtl.trial_url = "";
                    }
                    else
                    {
                        objoddtl.trial_url = txttrial_url.Text;
                    }
                    if (txtcart_link.Text == "")
                    {
                        objoddtl.cart_link = "";
                    }
                    else
                    {
                        objoddtl.cart_link = txtcart_link.Text;
                    }
                    if (txtproduct_detail_link.Text == "")
                    {
                        objoddtl.product_detail_link = "";
                    }
                    else
                    {
                        objoddtl.product_detail_link = txtproduct_detail_link.Text;
                    }
                    if (txtlead.Text == "")
                    {
                        objoddtl.lead = "";
                    }
                    else
                    {
                        objoddtl.lead = txtlead.Text;
                    }
                    if (txtother.Text == "")
                    {
                        objoddtl.other = "";
                    }
                    else
                    {
                        objoddtl.other = txtother.Text;
                    }
                    if (txtpromotion_type.Text == "")
                    {
                        objoddtl.promotion_type = "";
                    }
                    else
                    {
                        objoddtl.promotion_type = txtpromotion_type.Text;
                    }
                    if (txtpayout.Text == "")
                    {
                        objoddtl.payout = "";
                    }
                    else
                    {
                        objoddtl.payout = txtpayout.Text;
                    }
                    if (txtlading_page.Text == "")
                    {
                        objoddtl.lading_page = "";
                    }
                    else
                    {
                        objoddtl.lading_page = txtlading_page.Text;
                    }
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODDTL");
                    objEco_ERP_WEB_USER_MST.IsCache = true;
                    objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    scope.Complete(); //  To commit.

                }
            }
        }
    }
}
