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

namespace Web.ECOMM
{
    public partial class SupplierWebProd : System.Web.UI.Page
    {
        Database.ERPEntities DB = new Database.ERPEntities();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
                    int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
                    int LID = 1;


                    Session["UserId"] = 1;
                    Session["TenantId"] = 1;
                    Session["Lang"] = "1,2,3";
                    ViewState["Mode"] = null;
                    bindWebAndOfferInterval();
                    if (Request.QueryString["MYPRODID"] != null)
                    {
                        int Program = Convert.ToInt32(Request.QueryString["MYPRODID"]);
                        if (DB.Eco_tblBusProd.Where(p => p.MYPRODID == Program && p.ONLINESALEALLOW == true && p.COMPANYID == UID && p.TenantID == TID).Count() < 0)
                        {

                            string display = "This Product Are Not Onlin Sale!";
                            ClientScript.RegisterStartupScript(this.GetType(), "This Product Are Not Onlin Sale!", "alert('" + display + "');", true);
                            return;
                        }

                    }

                    BindLsit();

                }
                catch (Exception ex)
                {
                    //Session["mess"] = ex.ToString();
                    //Session["PageName"] = "Products Master";
                    //Response.Redirect("~/Error/404Error.aspx", false);
                }
            }
        }
        public void ReadOnly()
        {
            btnProductMaster.Enabled = false;
            btnPushOnWeb.Enabled = false;
            txtoverviewediter.Enabled = false;
            btnfeadeskart.Enabled = false;
            txtfutereditre.Enabled = false;
            Button1.Enabled = false;
            txtspecificaediter.Enabled = false;
            txtfaqediter.Enabled = false;
            ddlWebBusPro.Enabled = false;
            btnWebProduct.Enabled = false;
            ddlwebinterval.Enabled = false;
            txtwebfromdt.Enabled = false;
            txtwebtomdt.Enabled = false;
            txtwebtillmdt.Enabled = false;
            txtwebDisponWeb.Enabled = false;
            txtwebPlal.Enabled = false;
            txtwebparl.Enabled = false;
            txtwebplco.Enabled = false;
            txtweblink2d.Enabled = false;
            txtwebsortn.Enabled = false;
            btnImagUpload.Enabled = false;
            ddlImages.Enabled = false;
            dropMultiColor.Enabled = false;
            dropMultiSize.Enabled = false;
            Button2.Enabled = false;
            ddlOffWebProduct.Enabled = false;
            ddlOfferinterval.Enabled = false;
            txtOffreFromDt.Enabled = false;
            txtOffreToDt.Enabled = false;
            txtOffreTillDt.Enabled = false;
            txtOffreDispOnWeb.Enabled = false;
            txtOffPlac.Enabled = false;
            txtoffPArl.Enabled = false;
            txtoffPCol.Enabled = false;
            txtLink2D.Enabled = false;
            txtoffsortN.Enabled = false;
            ViewState["Mode"] = "ReadMode";
        }
        public void Write()
        {
            btnProductMaster.Enabled = true;
            btnPushOnWeb.Enabled = true;
            txtoverviewediter.Enabled = true;
            btnfeadeskart.Enabled = true;
            txtfutereditre.Enabled = true;
            Button1.Enabled = true;
            txtspecificaediter.Enabled = true;
            txtfaqediter.Enabled = true;
            ddlWebBusPro.Enabled = true;
            btnWebProduct.Enabled = true;
            ddlwebinterval.Enabled = true;
            txtwebfromdt.Enabled = true;
            txtwebtomdt.Enabled = true;
            txtwebtillmdt.Enabled = true;
            txtwebDisponWeb.Enabled = true;
            txtwebPlal.Enabled = true;
            txtwebparl.Enabled = true;
            txtwebplco.Enabled = true;
            txtweblink2d.Enabled = true;
            txtwebsortn.Enabled = true;
            btnImagUpload.Enabled = true;
            ddlImages.Enabled = true;
            dropMultiColor.Enabled = true;
            dropMultiSize.Enabled = true;
            Button2.Enabled = true;
            ddlOffWebProduct.Enabled = true;
            ddlOfferinterval.Enabled = true;
            txtOffreFromDt.Enabled = true;
            txtOffreToDt.Enabled = true;
            txtOffreTillDt.Enabled = true;
            txtOffreDispOnWeb.Enabled = true;
            txtOffPlac.Enabled = true;
            txtoffPArl.Enabled = true;
            txtoffPCol.Enabled = true;
            txtLink2D.Enabled = true;
            txtoffsortN.Enabled = true;
            BtnMstProSave.Text = "Update";
        }
        public string getProdname(int PN)
        {
            return DB.Eco_TBLPRODUCT.SingleOrDefault(p => p.MYPRODID == PN).ProdName1;
        }

        public string getbarcode(int PBC)
        {
            return DB.Eco_TBLPRODUCT.SingleOrDefault(p => p.MYPRODID == PBC).BarCode;
        }
        public string getqty(int PQTY)
        {
            return DB.Eco_TBLPRODUCT.SingleOrDefault(p => p.MYPRODID == PQTY).QTYINHAND.ToString();
        }
        public string getShortname(int PSN)
        {
            if (DB.Eco_TBLPRODUCT.SingleOrDefault(p => p.MYPRODID == PSN).ShortName != null)
            {
                return DB.Eco_TBLPRODUCT.SingleOrDefault(p => p.MYPRODID == PSN).ShortName;
            }
            else
            {
                return null;
            }
        }

        public string getWebProduct(int WPN)
        {
            int OffWebProduct = Convert.ToInt32(ddlOffWebProduct.SelectedValue);
            return DB.Eco_tblBusProd.SingleOrDefault(p => p.MYPRODID == WPN && p.MYID == OffWebProduct).MYTYPE;
        }
        protected void bindWebAndOfferInterval()
        {
            try
            {
                int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
                int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
                int LID = 1;
                Classes.EcommAdminClass.getdropdown(ddlwebinterval, TID, "INTERVAL", "INTERVAL", "", "Eco_REFTABLE");
                Classes.EcommAdminClass.getdropdown(ddlOfferinterval, TID, "INTERVAL", "INTERVAL", "", "Eco_REFTABLE");
                Classes.EcommAdminClass.getdropdown(drpfuchtertype, TID, "", "", "", "Eco_Fetaures_List_Mst");

                //ddlwebinterval.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "INTERVAL" && p.REFTYPE == "INTERVAL");
                //ddlwebinterval.DataTextField = "REFNAME1";
                //ddlwebinterval.DataValueField = "REFID";
                //ddlwebinterval.DataBind();
                //ddlwebinterval.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Web Interval--", "0"));

                //ddlOfferinterval.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "INTERVAL" && p.REFTYPE == "INTERVAL");
                //ddlOfferinterval.DataTextField = "REFNAME1";
                //ddlOfferinterval.DataValueField = "REFID";
                //ddlOfferinterval.DataBind();
                //ddlOfferinterval.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Offer Interval--", "0"));



                //drpfuchtertype.DataSource = DB.Eco_Fetaures_List_Mst.Where(p => p.Active == true);
                //drpfuchtertype.DataTextField = "FetauresName1";
                //drpfuchtertype.DataValueField = "MyID";
                //drpfuchtertype.DataBind();
                //drpfuchtertype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Feature Type--", "0"));


            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }

        private void bindOffProGrid()
        {

            int MYPID = Convert.ToInt32(lblmstproid.Text);
            Repeater1.DataSource = DB.Eco_tblOfferProd.Where(p => p.MYPRODID == MYPID && p.ACTIVE == true);
            Repeater1.DataBind();


        }
        public void BindWebProd()
        {
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            Repeater2.DataSource = DB.Eco_tblWebProd.Where(p => p.MYPRODID == MYPID && p.ACTIVE == true);
            Repeater2.DataBind();
        }
        protected void BtnMstProSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (BtnMstProSave.Text == "Edit")
                {
                    Write();
                    ViewState["Mode"] = "EditMode";
                }
                else
                {
                    int MYPID = Convert.ToInt32(lblmstproid.Text);
                    Database.Eco_TBLPRODDTL objTBLPRODDTL = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == MYPID);
                    //   var over= CKEProDtlOverview.instances.editor1.getData();

                    objTBLPRODDTL.OVERVIEW = txtoverviewediter.Text.ToString();
                    objTBLPRODDTL.FEATURES = txtfutereditre.Text.ToString();
                    objTBLPRODDTL.SPECIFICATIONS = txtspecificaediter.Text.ToString();
                    objTBLPRODDTL.FAQ_DOWNLOAD = txtfaqediter.Text.ToString();
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODDTL");
                    objEco_ERP_WEB_USER_MST.IsCache = true;
                    objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    clen();
                    FrmView.Visible = false;
                    shProductDetails.Attributes.CssStyle.Add("display", "block");
                    shlinkProductDetails.Attributes.Add("Class", "collapse");
                    Database.Eco_TBLPRODUCT objTBLPRODUCT = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID);
                    if (objTBLPRODDTL.OVERVIEW == "" || objTBLPRODDTL.OVERVIEW == "<p><br></p>" || objTBLPRODDTL.OVERVIEW == null || objTBLPRODDTL.FEATURES == "" || objTBLPRODDTL.FEATURES == "<p><br></p>" || objTBLPRODDTL.FEATURES == null || objTBLPRODDTL.SPECIFICATIONS == "" || objTBLPRODDTL.SPECIFICATIONS == "<p><br></p>" || objTBLPRODDTL.SPECIFICATIONS == null)
                        if (DB.Eco_ImageTable.Where(p => p.MYPRODID == MYPID).Count() < 0)
                            objTBLPRODUCT.DevloperActiv = false;
                        else
                            objTBLPRODUCT.DevloperActiv = false;
                    else
                        objTBLPRODUCT.DevloperActiv = true;
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODUCT");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    ViewState["Mode"] = "EditMode";
                }
                //try
                //{

                scope.Complete(); //  To commit.

            }

        }

        protected void lbApproveIss_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdvanshSearch.aspx");
        }
        public void BindLsit()
        {
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            if (Request.QueryString["MYPRODID"] != null)
            {
                int Program = Convert.ToInt32(Request.QueryString["MYPRODID"]);
                List<Database.Eco_tblBusProd> List = Classes.EcommAdminClass.getDataEco_tblBusProd().Where(p => p.ONLINESALEALLOW == true && p.MYPRODID == Program && p.TenantID == TID && p.LOCATION_ID == LID).ToList();
                pMasterGridview.DataSource = List;
                pMasterGridview.DataBind();
                if (Request.QueryString["MODE"] != null)
                {
                    string Mode = Request.QueryString["MODE"].ToString();
                    if (Mode == "EditMode")
                    {
                        string mytype2 = List[0].MYTYPE;
                        int ID = Convert.ToInt32(List[0].MYID);
                        EditCamm(ID, mytype2);
                    }
                    else
                    {
                        string mytype2 = List[0].MYTYPE;
                        int ID = Convert.ToInt32(List[0].MYID);
                        EditCamm(ID, mytype2);
                        ReadOnly();
                        BtnMstProSave.Text = "Edit";
                    }

                }

            }
            else
            {
                if (UID == 7)
                {
                    pMasterGridview.DataSource = Classes.EcommAdminClass.getDataEco_tblBusProd().Where(p => p.ONLINESALEALLOW == true && p.TenantID == TID);
                    pMasterGridview.DataBind();
                }
                else
                {
                    pMasterGridview.DataSource = Classes.EcommAdminClass.getDataEco_tblBusProd().Where(p => p.ONLINESALEALLOW == true  && p.TenantID == TID && p.LOCATION_ID == LID);
                    pMasterGridview.DataBind();
                }

            }

            //foreach (RepeaterItem dataItem in pMasterGridview.Items)
            //{
            //    // DropDownList DropDownListcontrol= ((DropDownList)dataItem.FindControl("DropDownList4"));
            //    CheckBox check = (CheckBox)dataItem.FindControl("chbcheckActiv");
            //    Label Qname = (Label)dataItem.FindControl("Qname");
            //    Label LblMyid = (Label)dataItem.FindControl("LblMyid");
            //    int MYID = Convert.ToInt32(LblMyid.Text);
            //    int ID = Convert.ToInt32(Qname.Text);
            //    int Refid = DB.tblBusProds.Single(p => p.MYPRODID == ID && p.MYID == MYID).REFID;
            //    Database.tblWebProd objtblWebProd = DB.tblWebProds.Single(p => p.ACTIVE == true && p.MYPRODID == ID && p.REFID == Refid);
            //    check.Checked = (objtblWebProd.CheckActive == true) ? true : false;
            //}

        }
        protected void pMasterGridview_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label prodname = (Label)e.Item.FindControl("lblspn");
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            if (e.CommandName == "btndelet")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_tblBusProd objtblBusProd = DB.Eco_tblBusProd.Single(p => p.MYID == ID);
                objtblBusProd.ACTIVE = false;
                objtblBusProd.ONLINESALEALLOW = false;
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblBusProd");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                BindLsit();

            }
            if (e.CommandName == "btnedit")
            {
                // list view close 

                int ID = Convert.ToInt32(e.CommandArgument);

                Label MYTYPE = (Label)e.Item.FindControl("lblMytype");
                string mytype2 = MYTYPE.Text.ToString();
                EditCamm(ID, mytype2);

            }
        }
        public void EditCamm(int ID, string mytype2)
        {
            // Label prodname = (Label)e.Item.FindControl("lblspn");
            shProductDetails.Attributes.CssStyle.Add("display", "none");
            shlinkProductDetails.Attributes.Add("Class", "expand");
            //
            btnpreview123.Visible = true;
            FrmView.Visible = true;
            int TID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).TenantID);
            int UID = Convert.ToInt32(((Eco_TBLCONTACT)Session["USER"]).ContactMyID);
            int LID = 1;
            Database.Eco_tblBusProd objtblBusProd = DB.Eco_tblBusProd.Single(p => p.MYID == ID);
            lblmstproid.Text = objtblBusProd.MYPRODID.ToString();
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            int Priesh = Convert.ToInt32(DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID).price);
            txtoledPrice.Text = Priesh.ToString("N2");
            ViewState["ProdctID"] = MYPID;

            int myid1 = Convert.ToInt32(DB.Eco_tblBusProd.Single(p => p.MYTYPE == mytype2 && p.MYPRODID == MYPID && p.ONLINESALEALLOW == true).MYID);
            ddlOffWebProduct.DataSource = DB.Eco_tblBusProd.Where(p => p.MYPRODID == MYPID && p.ACTIVE == true && p.ONLINESALEALLOW == true && p.MYID == myid1);
            ddlOffWebProduct.DataTextField = "MYTYPE";
            ddlOffWebProduct.DataValueField = "MYID";
            ddlOffWebProduct.DataBind();

            ddlWebBusPro.DataSource = DB.Eco_tblBusProd.Where(p => p.ONLINESALEALLOW == true && p.MYPRODID == MYPID && p.MYTYPE == mytype2 && p.MYID == myid1);
            ddlWebBusPro.DataTextField = "MYTYPE";
            ddlWebBusPro.DataValueField = "MYID";
            ddlWebBusPro.DataBind();
            Boolean Multicoler = Convert.ToBoolean(DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID).MultiColor);
            if (Multicoler == true)
            {
                Classes.EcommAdminClass.getdropdown(ddlImages, TID, "IMAGE", "IMAGE", "", "Eco_REFTABLE");
                //ddlImages.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "IMAGE" && p.REFTYPE == "IMAGE");
                //ddlImages.DataTextField = "REFNAME1";
                //ddlImages.DataValueField = "REFID";
                //ddlImages.DataBind();
                //ddlImages.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select File Type--", "0"));
            }
            else
            {

                ddlImages.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "IMAGE" && p.REFTYPE == "IMAGE" && p.REFID != 98805);
                ddlImages.DataTextField = "REFNAME1";
                ddlImages.DataValueField = "REFID";
                ddlImages.DataBind();
                ddlImages.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select File Type--", "0"));
            }


            Classes.EcommAdminClass.getdropdown(drpOffers, TID, "Offers", "Offers", "", "Eco_REFTABLE");

            //drpOffers.DataSource = DB.Eco_REFTABLE.Where(p => p.ACTIVE == "Y" && p.TenantID == TID && p.REFSUBTYPE == "Offers" && p.REFTYPE == "Offers");
            //drpOffers.DataTextField = "REFNAME1";
            //drpOffers.DataValueField = "REFID";
            //drpOffers.DataBind();
            //drpOffers.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Offers--", "0"));
            string prodname = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID).ProdName1;
            lblprodname.Text = "from" + " " + prodname.ToString();
            Database.Eco_TBLPRODDTL objTBLPRODDTL = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == MYPID);
            //   var over= CKEProDtlOverview.instances.editor1.getData();
            if (objTBLPRODDTL.OVERVIEW == null)
            {
            }
            else
            {
                txtoverviewediter.Text = objTBLPRODDTL.OVERVIEW.ToString();
            }

            if (objTBLPRODDTL.FEATURES == null)
            {
            }
            else
            {
                txtfutereditre.Text = objTBLPRODDTL.FEATURES.ToString();
            }
            if (objTBLPRODDTL.SPECIFICATIONS == null)
            {
            }
            else
            {
                txtspecificaediter.Text = objTBLPRODDTL.SPECIFICATIONS.ToString();
            }
            if (objTBLPRODDTL.FAQ_DOWNLOAD == null)
            {
            }
            else
            {
                txtfaqediter.Text = objTBLPRODDTL.FAQ_DOWNLOAD.ToString();
            }
            MainImgBind();
            BindWebProd();
            bindOffProGrid();
            Bindfuterlist();
            Multisize();
            MultiColoer();
            BindImag();
        }
        public void Bindfuterlist()
        {
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            List<Database.Eco_tblprod_feature> List = Classes.EcommAdminClass.getDataEco_tblprod_feature();
            listprodfucter.DataSource = List.Where(p => p.Productid == MYPID);
            listprodfucter.DataBind();
        }
        protected void btnfeatures_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Product'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            if (ViewState["fucterEdit"] != null)
            {
                int ID = Convert.ToInt32(ViewState["fucterEdit"]);
                Database.Eco_tblprod_feature objtblprod_feature = DB.Eco_tblprod_feature.Single(p => p.MyID == ID);
                objtblprod_feature.Productid = MYPID;
                objtblprod_feature.TenantID = 1;
                objtblprod_feature.FeatureType = lblfutertype1.Text;
                objtblprod_feature.FeatureImage = fucherimg1.ImageUrl;
                objtblprod_feature.FeatureName = drpfuchtertype.SelectedValue;
                //  objtblprod_feature.Fetaures = CKEProDtlFeatures.Text;
                objtblprod_feature.Active = true;
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblprod_feature");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                Bindfuterlist();
                CLENUP();
                ViewState["fucterEdit"] = null;
            }
            else
            {
                Database.Eco_tblprod_feature objtblprod_feature = new Database.Eco_tblprod_feature();
                if (drpfuchtertype.SelectedValue != "0")
                {
                    objtblprod_feature = new Database.Eco_tblprod_feature();
                    objtblprod_feature.MyID = DB.Eco_tblprod_feature.Count() > 0 ? Convert.ToInt32(DB.Eco_tblprod_feature.Max(p => p.MyID) + 1) : 1;
                    objtblprod_feature.Productid = MYPID;
                    objtblprod_feature.TenantID = 1;
                    objtblprod_feature.FeatureType = lblfutertype1.Text;
                    objtblprod_feature.FeatureImage = fucherimg1.ImageUrl;
                    objtblprod_feature.FeatureName = drpfuchtertype.SelectedValue;
                    //  objtblprod_feature.Fetaures = CKEProDtlFeatures.Text;
                    objtblprod_feature.Active = true;
                    DB.Eco_tblprod_feature.AddObject(objtblprod_feature);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblprod_feature");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    Bindfuterlist();
                    CLENUP();
                }
                else
                {
                    string display = "Select Feature Name!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Select Feature Name!", "alert('" + display + "');", true);
                    return;
                }
            }

            drpfuchtertype.Focus();

        }

        public string getfuctername(int FID)
        {
            if (FID == 0 || FID == null)
            {
                return null;
            }
            else
            {
                return DB.Eco_Fetaures_List_Mst.SingleOrDefault(p => p.MyID == FID).FetauresName1;
            }

        }
        protected void btnfeadeskart_Click(object sender, EventArgs e)
        {
            CLENUP();
        }
        public void CLENUP()
        {
            drpfuchtertype.SelectedIndex = 0;
            //  FileUpload5.Dispose();

            //drpfuchtertype2.SelectedIndex = 0;
            //drpfuchtertype3.SelectedIndex = 0;
            fucherimg1.ImageUrl = null;
            //fucherimg2.ImageUrl = null;
            //fucherimg3.ImageUrl = null;
            lblfutertype1.Text = "";
            //lblfutertype2.Text = "";
            //lblfutertype3.Text = "";
        }
        public void clen()
        {
            txtoverviewediter.Text = txtfutereditre.Text = txtspecificaediter.Text = txtfaqediter.Text = "";
            ddlwebinterval.SelectedIndex = 0;
            txtwebfromdt.Text = txtwebtomdt.Text = txtwebtillmdt.Text = txtOffreFromDt.Text = txtOffreToDt.Text = txtOffreTillDt.Text = "";
        }

        protected void drpfuchtertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpfuchtertype.SelectedValue != "0")
            {
                int fucher1 = Convert.ToInt32(drpfuchtertype.SelectedValue);
                string imgmane = DB.Eco_Fetaures_List_Mst.Single(p => p.Active == true && p.MyID == fucher1).FeatureImage;
                fucherimg1.ImageUrl = "Upload/" + imgmane;
                string futertype = DB.Eco_Fetaures_List_Mst.Single(p => p.Active == true && p.MyID == fucher1).FeatureType;
                lblfutertype1.Text = futertype;
            }
            else
            {
                fucherimg1.ImageUrl = null;
                lblfutertype1.Text = "";
            }
            drpfuchtertype.Focus();

        }
        protected void btnpreview_Click(object sender, EventArgs e)
        {
            string Str = "";
            int MYPID = Convert.ToInt32(ViewState["ProdctID"]);
            var obj = DB.Eco_TBLPRODUCT.SingleOrDefault(p => p.MYPRODID == MYPID);
            Str = obj.ProdName1.Replace(' ', '+');
            Response.Redirect("~/Productpriveu.aspx?PN=" + Str + "&PID=" + MYPID);
            ViewState["ProdctID"] = null;
        }

        protected void chbcheckActiv_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem dataItem in pMasterGridview.Items)
            {
                // DropDownList DropDownListcontrol= ((DropDownList)dataItem.FindControl("DropDownList4"));
                CheckBox check = (CheckBox)dataItem.FindControl("chbcheckActiv");
                Label Qname = (Label)dataItem.FindControl("Qname");
                Label LblMyid = (Label)dataItem.FindControl("LblMyid");
                int MYID = Convert.ToInt32(LblMyid.Text);
                int ID = Convert.ToInt32(Qname.Text);
                int Refid = DB.Eco_tblBusProd.Single(p => p.MYPRODID == ID && p.MYID == MYID).REFID;
                if (check.Checked == true)
                {
                    int PRODID = Convert.ToInt32(Qname.Text);
                    Database.Eco_TBLPRODDTL objTBLPRODDTL = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == PRODID);
                    if (objTBLPRODDTL.OVERVIEW == "" || objTBLPRODDTL.OVERVIEW == "<p><br></p>" || objTBLPRODDTL.OVERVIEW == null)
                    {
                        string display = "Overview Are Not complete!";
                        ClientScript.RegisterStartupScript(this.GetType(), "Overview Are Not complete!", "alert('" + display + "');", true);
                        return;
                    }
                    if (objTBLPRODDTL.FEATURES == "" || objTBLPRODDTL.FEATURES == "<p><br></p>" || objTBLPRODDTL.FEATURES == null)
                    {
                        string display = "Features Are Not complete!";
                        ClientScript.RegisterStartupScript(this.GetType(), "Features Are Not Features!", "alert('" + display + "');", true);
                        return;
                    }
                    if (objTBLPRODDTL.SPECIFICATIONS == "" || objTBLPRODDTL.SPECIFICATIONS == "<p><br></p>" || objTBLPRODDTL.SPECIFICATIONS == null)
                    {
                        string display = "Specifications Are Not complete!";
                        ClientScript.RegisterStartupScript(this.GetType(), "Specifications Are Not Features!", "alert('" + display + "');", true);
                        return;
                    }
                    if (DB.Eco_tblprod_feature.Where(p => p.Productid == PRODID).Count() > 0)
                    { }
                    else
                    {
                        string display = "Product Feature is 1 Compulsory!";
                        ClientScript.RegisterStartupScript(this.GetType(), "Product Feature is 1 Compulsory!", "alert('" + display + "');", true);
                        return;
                    }
                    if (DB.Eco_ImageTable.Where(p => p.MYPRODID == PRODID).Count() > 0)
                    {
                        //Database.tblWebProd objtblWebProd = DB.tblWebProds.Single(p => p.ACTIVE == true && p.MYPRODID == ID && p.REFID == Refid);
                        //objtblWebProd.CheckActive = check.Checked ? true : false;
                        //DB.SaveChanges();
                    }
                    else
                    {
                        string display = "Product Image is 1 Compulsory!";
                        ClientScript.RegisterStartupScript(this.GetType(), "Product Image is 1 Compulsory!", "alert('" + display + "');", true);
                        return;
                    }
                }
            }
        }

        protected void listprodfucter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnedit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_tblprod_feature objtblprod_feature = DB.Eco_tblprod_feature.Single(p => p.MyID == ID);
                drpfuchtertype.SelectedValue = objtblprod_feature.FeatureName.ToString();
                if (objtblprod_feature.Fetaures == null || objtblprod_feature.Fetaures == "")
                { }
                else
                {
                    // CKEProDtlFeatures.Text = objtblprod_feature.Fetaures.ToString();
                }
                fucherimg1.ImageUrl = objtblprod_feature.FeatureImage.ToString();
                int Futername = Convert.ToInt32(objtblprod_feature.FeatureName);
                lblfutertype1.Text = DB.Eco_Fetaures_List_Mst.Single(p => p.MyID == Futername).FeatureType;
                ViewState["fucterEdit"] = ID;

            }
            if (e.CommandName == "btndelet")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_tblprod_feature objtblprod_feature = DB.Eco_tblprod_feature.Single(p => p.MyID == ID);
                objtblprod_feature.Active = false;
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblprod_feature");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                Bindfuterlist();

            }
            drpfuchtertype.Focus();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Offer'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_tblOfferProd objtbloffer = DB.Eco_tblOfferProd.Single(p => p.MYID == ID);
                objtbloffer.ACTIVE = false;
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblOfferProd");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                bindOffProGrid();
                ddlOffWebProduct.Focus();
            }
            if (e.CommandName == "btnedit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_tblOfferProd objtbloffer = DB.Eco_tblOfferProd.Single(p => p.MYID == ID);
                ddlOfferinterval.SelectedValue = objtbloffer.INTERVAL.ToString();
                txtOffreFromDt.Text = objtbloffer.FROMDATE.ToString();
                txtOffreToDt.Text = objtbloffer.TODATE.ToString();
                txtOffreTillDt.Text = objtbloffer.TILLDATE.ToString();
                ViewState["EditOffer"] = ID;
            }
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminIndex.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Imagges'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            if (FileUpload1.HasFile)
            {
                List<Database.Eco_ImageTable> TempList = new List<Database.Eco_ImageTable>();
                int CID = Convert.ToInt32(dropMultiColor.SelectedValue);
                int SID = Convert.ToInt32(dropMultiSize.SelectedValue);
                Database.Eco_ImageTable objImageTable = new Database.Eco_ImageTable();
                int MYPID = Convert.ToInt32(lblmstproid.Text);
                Boolean MultiColor = Convert.ToBoolean(DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID).MultiColor);
                if (Request.Files.Count > 0)
                {
                    if (ddlImages.SelectedValue == "98805")
                    {
                        HttpFileCollection attachments = Request.Files;
                        for (int i = 0; i < 1; i++)
                        {
                            HttpPostedFile attachment = attachments[i];
                            if (attachment.ContentLength > 0 && !String.IsNullOrEmpty(attachment.FileName))
                            {
                                objImageTable = new Database.Eco_ImageTable();
                                int img1 = Convert.ToInt32(ddlImages.SelectedValue);
                                objImageTable.ImageID = img1;
                                attachment.SaveAs(Server.MapPath("Upload/") + attachment.FileName);
                                objImageTable.PICTURE = attachment.FileName;
                                objImageTable.MYPRODID = MYPID;
                                TempList.Add(objImageTable);

                            }
                        }

                    }
                    else
                    {
                        HttpFileCollection attachments = Request.Files;
                        for (int i = 0; i < attachments.Count; i++)
                        {
                            HttpPostedFile attachment = attachments[i];
                            if (attachment.ContentLength > 0 && !String.IsNullOrEmpty(attachment.FileName))
                            {
                                objImageTable = new Database.Eco_ImageTable();
                                int img1 = Convert.ToInt32(ddlImages.SelectedValue);
                                objImageTable.ImageID = img1;
                                attachment.SaveAs(Server.MapPath("Upload/") + attachment.FileName);
                                objImageTable.PICTURE = attachment.FileName;
                                objImageTable.MYPRODID = MYPID;
                                TempList.Add(objImageTable);

                            }
                        }

                    }

                    listImag.DataSource = TempList;
                    listImag.DataBind();
                }
            }

            else
            {
                string display = "Required a Image!";
                pnlSuccessMsg123.Visible = true;
                lblMsg123.Text = display;
            }



        }
        public void BindImag()
        {
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            List<Database.Eco_ImageTable> List = Classes.EcommAdminClass.getDataEco_ImageTable();
            BindImagProduct.DataSource = List.Where(p => p.MYPRODID == MYPID);
            BindImagProduct.DataBind();
        }
        public string getimagType(int IID)
        {
            return DB.Eco_REFTABLE.Single(p => p.REFID == IID).REFNAME1;
        }
        public string getcolerName(int CID)
        {

            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.RecTypeID == CID).Count() > 0)
            {
                return DB.Eco_Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == CID).RecValue;
            }
            else
            {
                return null;
            }
        }
        public string getSize(int SID)
        {
            if (DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.RecTypeID == SID).Count() > 0)
            {
                return DB.Eco_Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == SID).RecValue;
            }
            else
            {
                return null;
            }
        }

        protected void listImag_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Imagges'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            if (e.CommandName == "btnedit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_ImageTable objImageTable = DB.Eco_ImageTable.Single(p => p.ID == ID);
                dropMultiColor.SelectedValue = objImageTable.COLORID.ToString();
                dropMultiSize.SelectedValue = objImageTable.SIZECODE.ToString();
                ddlImages.SelectedValue = objImageTable.ImageID.ToString();
                ViewState["ImagID"] = ID;
            }
            if (e.CommandName == "btndelet")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_ImageTable objImageTable = DB.Eco_ImageTable.Single(p => p.ID == ID);
                objImageTable.Active = "0";
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                //  BindImag();

            }
        }



        protected void btnWebReload_Click(object sender, EventArgs e)
        {

        }

        protected void btnPushOnWeb_Click(object sender, EventArgs e)
        {
            int MYPID = Convert.ToInt32(ViewState["ProdctID"]);
            if (DB.Eco_TempDisplayEcomTable.Where(p => p.MYPRODID == MYPID).Count() > 0)
            {
                string display = "This Product All Redy Push On Web!";
                ClientScript.RegisterStartupScript(this.GetType(), "This Product All Redy Push On Web!", "alert('" + display + "');", true);
                return;
            }
            else
            {
                if (DB.Eco_Product_Cat_Mst.Where(p => p.MYPRODID == MYPID).Count() > 0)
                {
                    var PLIST = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID);
                    var WebList = DB.Eco_tblWebProd.Single(p => p.MYPRODID == MYPID && p.ACTIVE == true);
                    var ProdCatList = DB.Eco_Product_Cat_Mst.Single(p => p.MYPRODID == MYPID);
                    Database.Eco_TempDisplayEcomTable obj = new Eco_TempDisplayEcomTable();
                    obj.TenantID = 1;
                    obj.DatePerf = DateTime.Now;
                    obj.BusID = WebList.REFID;
                    obj.BUSTYPE = WebList.REFTYPE;
                    obj.BusSUBTYPE = WebList.REFSUBTYPE;
                    obj.REFNAME2 = DB.Eco_REFTABLE.Single(p => p.REFID == obj.BusID).SWITCH3;
                    obj.MYPRODID = MYPID;
                    obj.CATID = ProdCatList.CATID;
                    obj.CatName = DB.Eco_CAT_MST.Single(p => p.CATID == obj.CATID).CAT_NAME1;
                    obj.PARENT_CATID = ProdCatList.PARENT_CATID;
                    obj.ParentCatName = DB.Eco_CAT_MST.Single(p => p.CATID == obj.PARENT_CATID).CAT_NAME1;
                    obj.Brand = PLIST.Brand;
                    int BID = Convert.ToInt32(obj.Brand);
                    obj.BrandName = DB.Eco_REFTABLE.Single(p => p.REFID == BID).REFNAME1;
                    obj.BarCode = PLIST.BarCode;
                    obj.Remarks = "Privewo";
                    if (DB.Eco_tblOfferProd.Where(p => p.MYPRODID == MYPID).Count() > 0)
                    {
                        obj.OfferProd = true;
                    }
                    if (DB.Eco_tblWebProd.Where(p => p.ACTIVE == true && p.MYPRODID == MYPID).Count() > 0)
                    { obj.WebProd = true; }
                    obj.Active = "1";
                    obj.ProductName = PLIST.ProdName1;
                    obj.Price = PLIST.price;
                    DB.Eco_TempDisplayEcomTable.AddObject(obj);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TempDisplayEcomTable");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    Response.Redirect("~/Productpriveu.aspx?PID=" + MYPID);
                }
                else
                {
                    string display = "This Product Are Not Select Category!";
                    ClientScript.RegisterStartupScript(this.GetType(), "This Product Are Not Select Category!", "alert('" + display + "');", true);
                    return;
                }
            }
        }

        protected void btnOfferProduct_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Offer'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            int myid = Convert.ToInt32(ddlWebBusPro.SelectedValue);
            int OffWebProduct = Convert.ToInt32(ddlOffWebProduct.SelectedValue);
            int refidOFF = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == OffWebProduct).REFID;
            int RefID = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == OffWebProduct).REFID;
            string REFTYPE = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == OffWebProduct).REFTYPE;
            string REFSUBTYPE = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == OffWebProduct).REFSUBTYPE;
            if (ViewState["EditOffer"] != null)
            {
                int ID = Convert.ToInt32(ViewState["EditOffer"]);
                Database.Eco_tblOfferProd objtblOfferProd = DB.Eco_tblOfferProd.Single(p => p.MYID == ID);
                if (txtOffreFromDt.Text == "")
                {
                    objtblOfferProd.FROMDATE = null;
                }
                else
                {
                    objtblOfferProd.FROMDATE = Convert.ToDateTime(txtOffreFromDt.Text);
                }

                if (txtOffreTillDt.Text == "")
                {
                    objtblOfferProd.TILLDATE = null;
                }
                else
                {
                    objtblOfferProd.TILLDATE = Convert.ToDateTime(txtOffreTillDt.Text);
                }
                if (txtOffreToDt.Text == "")
                {
                    objtblOfferProd.TODATE = null;
                }
                else
                {
                    objtblOfferProd.TODATE = Convert.ToDateTime(txtOffreToDt.Text);
                    objtblOfferProd.ACTIVE = true;
                }
                objtblOfferProd.OeledPrice = Convert.ToDecimal(txtoledPrice.Text);
                objtblOfferProd.NewPrice = Convert.ToDecimal(txtnewprice.Text);
                objtblOfferProd.Discount = Convert.ToInt32(txtDiscount.Text);
                objtblOfferProd.OfferID = Convert.ToInt32(drpOffers.SelectedValue);

                objtblOfferProd.INTERVAL = Convert.ToInt32(ddlOfferinterval.SelectedValue);

                if (txtOffPlac.Text != "0" && txtoffPCol.Text != "0" && txtoffsortN.Text != "0")
                {
                    int DIPID = Convert.ToInt32(objtblOfferProd.DISPLAY_ID);
                    if (DB.Eco_ICProdDisplay.Where(p => p.Display_Id == DIPID).Count() > 0)
                    {
                        Database.Eco_ICProdDisplay objICProdDisplay = DB.Eco_ICProdDisplay.Single(p => p.Display_Id == DIPID);
                        objICProdDisplay.TenantID = 1;
                        objICProdDisplay.MYPRODID = MYPID;
                        objICProdDisplay.Display_Id = DIPID;
                        objICProdDisplay.REFID = RefID;
                        objICProdDisplay.REFTYPE = REFTYPE;
                        objICProdDisplay.REFSUBTYPE = REFSUBTYPE;
                        objICProdDisplay.TableName = "tblOfferProduct";
                        objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtOffPlac.Text);
                        objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtoffPArl.Text);
                        objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtoffPCol.Text);
                        objICProdDisplay.LINK2DIRECT = txtLink2D.Text;
                        objICProdDisplay.sortnumber = Convert.ToInt32(txtoffsortN.Text);
                        objICProdDisplay.ACTIVE2 = true;
                        DB.SaveChanges();
                        Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                        objEco_ERP_WEB_USER_MST1.IsCache = true;
                        objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                        DB.SaveChanges();
                        objtblOfferProd.DISPLAY_ID = objICProdDisplay.Display_Id;
                    }

                }
            }
            else
            {
                Database.Eco_tblOfferProd objtblOfferProd = new Eco_tblOfferProd();
                objtblOfferProd.TenantID = 1;
                objtblOfferProd.MYPRODID = MYPID;
                objtblOfferProd.REFID = RefID;
                objtblOfferProd.REFSUBTYPE = REFSUBTYPE;
                objtblOfferProd.REFTYPE = REFTYPE;
                objtblOfferProd.MYID = DB.Eco_tblOfferProd.Count() > 0 ? Convert.ToInt32(DB.Eco_tblOfferProd.Max(p => p.MYID) + 1) : 1;
                if (txtOffreFromDt.Text == "")
                {
                    objtblOfferProd.FROMDATE = null;
                }
                else
                {
                    objtblOfferProd.FROMDATE = Convert.ToDateTime(txtOffreFromDt.Text);
                }

                if (txtOffreTillDt.Text == "")
                {
                    objtblOfferProd.TILLDATE = null;
                }
                else
                {
                    objtblOfferProd.TILLDATE = Convert.ToDateTime(txtOffreTillDt.Text);
                }
                if (txtOffreToDt.Text == "")
                {
                    objtblOfferProd.TODATE = null;
                }
                else
                {
                    objtblOfferProd.TODATE = Convert.ToDateTime(txtOffreToDt.Text);
                    objtblOfferProd.ACTIVE = true;
                }
                objtblOfferProd.INTERVAL = Convert.ToInt32(ddlOfferinterval.SelectedValue);
                objtblOfferProd.OeledPrice = Convert.ToDecimal(txtoledPrice.Text);
                objtblOfferProd.NewPrice = Convert.ToDecimal(txtnewprice.Text);
                objtblOfferProd.Discount = Convert.ToInt32(txtDiscount.Text);
                objtblOfferProd.OfferID = Convert.ToInt32(drpOffers.SelectedValue);
                DB.Eco_tblOfferProd.AddObject(objtblOfferProd);
                DB.SaveChanges();
                if (txtOffPlac.Text != "0" && txtoffPCol.Text != "0" && txtoffsortN.Text != "0")
                {
                    Database.Eco_ICProdDisplay objICProdDisplay = new Database.Eco_ICProdDisplay();
                    objICProdDisplay.TenantID = 1;
                    objICProdDisplay.MYPRODID = MYPID;
                    objICProdDisplay.Display_Id = DB.Eco_ICProdDisplay.Count() > 0 ? Convert.ToInt32(DB.Eco_ICProdDisplay.Max(p => p.Display_Id) + 1) : 1;
                    objICProdDisplay.REFID = RefID;
                    objICProdDisplay.REFTYPE = REFTYPE;
                    objICProdDisplay.REFSUBTYPE = REFSUBTYPE;
                    objICProdDisplay.TableName = "tblOfferProduct";
                    objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtOffPlac.Text);
                    objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtoffPArl.Text);
                    objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtoffPCol.Text);
                    objICProdDisplay.LINK2DIRECT = txtLink2D.Text;
                    objICProdDisplay.sortnumber = Convert.ToInt32(txtoffsortN.Text);
                    objICProdDisplay.ACTIVE2 = true;
                    DB.Eco_ICProdDisplay.AddObject(objICProdDisplay);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    objtblOfferProd.DISPLAY_ID = objICProdDisplay.Display_Id;
                }
            }

            DB.SaveChanges();
            ClenOffer();
            Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblOfferProd");
            objEco_ERP_WEB_USER_MST.IsCache = true;
            objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
            DB.SaveChanges();
            bindOffProGrid();
        }

        protected void btnWebProduct_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Webproduct'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            int myid = Convert.ToInt32(ddlWebBusPro.SelectedValue);
            int refid = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == myid).REFID;
            int RefID = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == myid).REFID;
            string REFTYPE = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == myid).REFTYPE;
            string REFSUBTYPE = DB.Eco_tblBusProd.Single(p => p.ONLINESALEALLOW == true && p.MYID == myid).REFSUBTYPE;
            if (ViewState["WebProdID"] != null)
            {
                int ID = Convert.ToInt32(ViewState["WebProdID"]);
                Database.Eco_tblWebProd objtblWebProd = DB.Eco_tblWebProd.Single(p => p.MYPRODID == MYPID && p.REFID == refid && p.WEBID == ID);
                objtblWebProd.FROMDATE = Convert.ToDateTime(txtwebfromdt.Text);
                objtblWebProd.INTERVAL = Convert.ToInt32(ddlwebinterval.SelectedValue);
                objtblWebProd.TILLDATE = Convert.ToDateTime(txtwebtillmdt.Text);
                objtblWebProd.TODATE = Convert.ToDateTime(txtwebtomdt.Text);
                if (objtblWebProd.DISPLAY_ID != null)
                {
                    int DID = Convert.ToInt32(objtblWebProd.DISPLAY_ID);
                    Database.Eco_ICProdDisplay objICProdDisplay = DB.Eco_ICProdDisplay.Single(p => p.Display_Id == DID);
                    objICProdDisplay.TenantID = 1;
                    objICProdDisplay.MYPRODID = MYPID;
                    //objICProdDisplay.Display_Id = DB.Eco_ICProdDisplay.Count() > 0 ? Convert.ToInt32(DB.Eco_ICProdDisplay.Max(p => p.Display_Id) + 1) : 1;
                    objICProdDisplay.REFID = RefID;
                    objICProdDisplay.REFTYPE = REFTYPE;
                    objICProdDisplay.REFSUBTYPE = REFSUBTYPE;
                    objICProdDisplay.TableName = "tblWebProde";
                    objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtwebPlal.Text);
                    objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtwebparl.Text);
                    objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtwebplco.Text);
                    objICProdDisplay.LINK2DIRECT = txtweblink2d.Text;
                    objICProdDisplay.sortnumber = Convert.ToInt32(txtwebsortn.Text);
                    objICProdDisplay.ACTIVE2 = true;
                    // DB.Eco_ICProdDisplay.AddObject(objICProdDisplay);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                    objEco_ERP_WEB_USER_MST.IsCache = true;
                    objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    ViewState["WebProdID"] = null;
                    // objtblWebProd.DISPLAY_ID = objICProdDisplay.Display_Id;
                }
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblWebProd");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
            }
            else
            {
                Database.Eco_tblWebProd objtblWebProd = new Eco_tblWebProd();
                objtblWebProd.TenantID = 1;
                objtblWebProd.MYPRODID = MYPID;
                objtblWebProd.REFID = RefID;
                objtblWebProd.REFTYPE = REFTYPE;
                objtblWebProd.REFSUBTYPE = REFSUBTYPE;
                objtblWebProd.MYID = DB.Eco_tblWebProd.Count() > 0 ? Convert.ToInt32(DB.Eco_tblWebProd.Max(p => p.MYID) + 1) : 1;
                objtblWebProd.FROMDATE = Convert.ToDateTime(txtwebfromdt.Text);
                objtblWebProd.INTERVAL = Convert.ToInt32(ddlwebinterval.SelectedValue);
                objtblWebProd.TILLDATE = Convert.ToDateTime(txtwebtillmdt.Text);
                objtblWebProd.TODATE = Convert.ToDateTime(txtwebtomdt.Text);
                objtblWebProd.ACTIVE = true;
                DB.Eco_tblWebProd.AddObject(objtblWebProd);
                DB.SaveChanges();
                if (txtwebPlal.Text != "0" && txtwebplco.Text != "0" && txtwebsortn.Text != "0")
                {
                    Database.Eco_ICProdDisplay objICProdDisplay = new Database.Eco_ICProdDisplay();
                    objICProdDisplay.TenantID = 1;
                    objICProdDisplay.MYPRODID = MYPID;
                    objICProdDisplay.Display_Id = DB.Eco_ICProdDisplay.Count() > 0 ? Convert.ToInt32(DB.Eco_ICProdDisplay.Max(p => p.Display_Id) + 1) : 1;
                    objICProdDisplay.REFID = RefID;
                    objICProdDisplay.REFTYPE = REFTYPE;
                    objICProdDisplay.REFSUBTYPE = REFSUBTYPE;
                    objICProdDisplay.TableName = "tblWebProde";
                    objICProdDisplay.PLACEHOLDERLINE = Convert.ToInt32(txtwebPlal.Text);
                    objICProdDisplay.PLACEHOLDERALIRL = Convert.ToInt32(txtwebparl.Text);
                    objICProdDisplay.PLACEHOLDERCOLUMN = Convert.ToInt32(txtwebplco.Text);
                    objICProdDisplay.LINK2DIRECT = txtweblink2d.Text;
                    objICProdDisplay.sortnumber = Convert.ToInt32(txtwebsortn.Text);
                    objICProdDisplay.ACTIVE2 = true;
                    DB.Eco_ICProdDisplay.AddObject(objICProdDisplay);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ICProdDisplay");
                    objEco_ERP_WEB_USER_MST.IsCache = true;
                    objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    objtblWebProd.DISPLAY_ID = objICProdDisplay.Display_Id;
                }
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_tblWebProd");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                BindWebProd();
            }

        }

        protected void ddlImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IMGID = Convert.ToInt32(ddlImages.SelectedValue);
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            Boolean Multicoler = Convert.ToBoolean(DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID).MultiColor);
            Boolean MultiSize = Convert.ToBoolean(DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == MYPID).MultiSize);
            List<Database.Eco_ImageTable> List = DB.Eco_ImageTable.Where(p => p.MYPRODID == MYPID && p.ImageID == IMGID && p.Active == "1").ToList();
            if (List.Count() == 1)
            {
                int REctypid = Convert.ToInt32(List.Single().COLORID);
                int REctypidsiz = Convert.ToInt32(List.Single().SIZECODE);
                string RCCID = REctypid.ToString();
                string RCSID = REctypidsiz.ToString();
                if (REctypid != 0)
                {
                    Classes.EcommAdminClass.getdropdown(dropMultiColor, MYPID, "MultiSize", RCCID, "", "Eco_Tbl_Multi_Color_Size_Mst");
                    //dropMultiColor.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPID && p.RecordType == "MultiColor" && p.Active == true && p.RecTypeID != REctypid);
                    //dropMultiColor.DataTextField = "RecValue";
                    //dropMultiColor.DataValueField = "RecTypeID";
                    //dropMultiColor.DataBind();
                    //dropMultiColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Color--", "0"));
                    if (REctypidsiz != 0)
                    {
                        Classes.EcommAdminClass.getdropdown(dropMultiSize, MYPID, "MultiSize", RCSID, "", "Eco_Tbl_Multi_Color_Size_Mst");
                        //dropMultiSize.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPID && p.RecordType == "MultiSize" && p.Active == true && p.RecTypeID != REctypidsiz);
                        //dropMultiSize.DataTextField = "RecValue";
                        //dropMultiSize.DataValueField = "RecTypeID";
                        //dropMultiSize.DataBind();
                        //dropMultiSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Size--", "0"));
                    }
                    else
                    {
                        Multisize();
                    }
                }
                else
                {

                    if (REctypidsiz != 0)
                    {
                        Classes.EcommAdminClass.getdropdown(dropMultiSize, MYPID, "MultiSize", RCSID, "", "Eco_Tbl_Multi_Color_Size_Mst");
                        //dropMultiSize.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPID && p.RecordType == "MultiSize" && p.Active == true && p.RecTypeID != REctypidsiz);
                        //dropMultiSize.DataTextField = "RecValue";
                        //dropMultiSize.DataValueField = "RecTypeID";
                        //dropMultiSize.DataBind();
                        //dropMultiSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Size--", "0"));
                    }
                    else
                    {
                        Multisize();
                    }
                    MultiColoer();
                }


            }
            else
            {

                MultiColoer();
                Multisize();
            }
            List<Database.Eco_ImageTable> List2 = Classes.EcommAdminClass.getDataEco_ImageTable();
            BindImagProduct.DataSource = List2.Where(p => p.MYPRODID == MYPID && p.ImageID == IMGID);
            BindImagProduct.DataBind();

        }
        public void MultiColoer()
        {
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            Classes.EcommAdminClass.getdropdown(dropMultiColor, MYPID, "MultiColor", "", "", "Eco_Tbl_Multi_Color_Size_Mst");


            //dropMultiColor.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPID && p.RecordType == "MultiColor" && p.Active == true);
            //dropMultiColor.DataTextField = "RecValue";
            //dropMultiColor.DataValueField = "RecTypeID";
            //dropMultiColor.DataBind();
            //dropMultiColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Color--", "0"));



        }
        public void Multisize()
        {
            int MYPID = Convert.ToInt32(lblmstproid.Text);
            Classes.EcommAdminClass.getdropdown(dropMultiSize, MYPID, "MultiSize", "", "", "Eco_Tbl_Multi_Color_Size_Mst");

            //dropMultiSize.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == MYPID && p.RecordType == "MultiSize" && p.Active == true);
            //dropMultiSize.DataTextField = "RecValue";
            //dropMultiSize.DataValueField = "RecTypeID";
            //dropMultiSize.DataBind();
            //dropMultiSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Size--", "0"));
        }

        protected void listImag_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ListdropMultiColor = (DropDownList)e.Item.FindControl("ListdropMultiColor");
                DropDownList ListdropMultiSize = (DropDownList)e.Item.FindControl("ListdropMultiSize");
                Label lblProduct = (Label)e.Item.FindControl("lblProduct");
                int PID = Convert.ToInt32(lblProduct.Text);
                Classes.EcommAdminClass.getdropdown(ListdropMultiColor, PID, "MultiColor", "", "", "Eco_Tbl_Multi_Color_Size_Mst");
                Classes.EcommAdminClass.getdropdown(ListdropMultiSize, PID, "MultiSize", "", "", "Eco_Tbl_Multi_Color_Size_Mst");
                //ListdropMultiColor.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiColor" && p.Active == true);
                //ListdropMultiColor.DataTextField = "RecValue";
                //ListdropMultiColor.DataValueField = "RecTypeID";
                //ListdropMultiColor.DataBind();
                //ListdropMultiColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Color--", "0"));


                //ListdropMultiSize.DataSource = DB.Eco_Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiSize" && p.Active == true);
                //ListdropMultiSize.DataTextField = "RecValue";
                //ListdropMultiSize.DataValueField = "RecTypeID";
                //ListdropMultiSize.DataBind();
                //ListdropMultiSize.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Multi Size--", "0"));

            }
        }

        protected void btnProductMaster_Click(object sender, EventArgs e)
        {
            int MYPODID = Convert.ToInt32(lblmstproid.Text);
            string Mode = ViewState["Mode"].ToString();
            Response.Redirect("SupplierProductMasterTest.aspx?MYPRODID=" + MYPODID + "&MODE=" + Mode);
        }

        protected void btnImagUpload_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int MYPODID = Convert.ToInt32(lblmstproid.Text);
                for (int i = 0; i <= listImag.Items.Count - 1; i++)
                {
                    Label lblIDIMG = (Label)listImag.Items[i].FindControl("lblIDIMG");
                    Label lblImg = (Label)listImag.Items[i].FindControl("lblImg");
                    DropDownList ListdropMultiColor = (DropDownList)listImag.Items[i].FindControl("ListdropMultiColor");
                    DropDownList ListdropMultiSize = (DropDownList)listImag.Items[i].FindControl("ListdropMultiSize");
                    TextBox txtShortBY = (TextBox)listImag.Items[i].FindControl("txtShortBY");
                    Database.Eco_ImageTable objImageTable = new Database.Eco_ImageTable();
                    objImageTable.TenantID = 1;
                    objImageTable.MYPRODID = MYPODID;
                    objImageTable.ImageID = Convert.ToInt32(lblIDIMG.Text);
                    objImageTable.PICTURE = lblImg.Text;
                    objImageTable.sortnumber = Convert.ToInt32(txtShortBY.Text);
                    objImageTable.Active = "1";
                    objImageTable.COLORID = Convert.ToInt32(ListdropMultiColor.SelectedValue);
                    objImageTable.SIZECODE = Convert.ToInt32(ListdropMultiSize.SelectedValue);
                    DB.Eco_ImageTable.AddObject(objImageTable);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }
                for (int j = 0; j <= BindImagProduct.Items.Count - 1; j++)
                {
                    TextBox txtShotName = (TextBox)BindImagProduct.Items[j].FindControl("txtShotName");
                    Label lblID = (Label)BindImagProduct.Items[j].FindControl("lblID");
                    int ID = Convert.ToInt32(lblID.Text);
                    Database.Eco_ImageTable objImageTableedit = DB.Eco_ImageTable.Single(p => p.ID == ID);
                    objImageTableedit.sortnumber = Convert.ToInt32(txtShotName.Text);
                    DB.SaveChanges();

                }
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
                objEco_ERP_WEB_USER_MST.IsCache = true;
                objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                Database.Eco_TBLPRODDTL objTBLPRODDTL = DB.Eco_TBLPRODDTL.Single(p => p.MYPRODID == MYPODID);
                objTBLPRODDTL.OVERVIEW = txtoverviewediter.Text.ToString();
                objTBLPRODDTL.FEATURES = txtfutereditre.Text.ToString();
                objTBLPRODDTL.SPECIFICATIONS = txtspecificaediter.Text.ToString();
                objTBLPRODDTL.FAQ_DOWNLOAD = txtfaqediter.Text.ToString();
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST2 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_TBLPRODDTL");
                objEco_ERP_WEB_USER_MST2.IsCache = true;
                objEco_ERP_WEB_USER_MST2.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                listImag.DataSource = null;
                listImag.DataBind();
                BindImag();
                scope.Complete(); //  To commit.

            }
        }

        protected void BindImagProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Imagges'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            if (e.CommandName == "btnedit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_ImageTable objImageTable = DB.Eco_ImageTable.Single(p => p.ID == ID);
                dropMultiColor.SelectedValue = objImageTable.COLORID.ToString();
                dropMultiSize.SelectedValue = objImageTable.SIZECODE.ToString();
                ddlImages.SelectedValue = objImageTable.ImageID.ToString();
                ViewState["ImagID"] = ID;
            }
            if (e.CommandName == "btndelet")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_ImageTable objImageTable = DB.Eco_ImageTable.Single(p => p.ID == ID);
                objImageTable.Active = "0";
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST2 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
                objEco_ERP_WEB_USER_MST2.IsCache = true;
                objEco_ERP_WEB_USER_MST2.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                BindImag();

            }
        }

        protected void linktoprodMaster_Click(object sender, EventArgs e)
        {
            int MYPODID = Convert.ToInt32(lblmstproid.Text);
            string Mode = ViewState["Mode"].ToString();
            Response.Redirect("SupplierProductMasterTest.aspx?MYPRODID=" + MYPODID + "&MODE=" + Mode);
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            double OLEDPRICE = Convert.ToDouble(txtoledPrice.Text);
            double DISCUNT = Convert.ToDouble(txtDiscount.Text);
            double NewPries = (OLEDPRICE * DISCUNT) / 100;
            double FienPriesh = OLEDPRICE - NewPries;
            txtnewprice.Text = FienPriesh.ToString("N2");
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {

            ClenOffer();
        }
        public void ClenOffer()
        {
            ddlOfferinterval.SelectedIndex = drpOffers.SelectedIndex = 0;
            txtOffreFromDt.Text = txtOffreToDt.Text = txtOffreTillDt.Text = txtDiscount.Text = txtnewprice.Text = "";
        }

        protected void btnAddwebProd_Click(object sender, EventArgs e)
        {
            ClenWeb();
        }
        public void ClenWeb()
        {
            ddlwebinterval.SelectedIndex = 0;
            txtwebfromdt.Text = txtwebtomdt.Text = txtwebtillmdt.Text = txtwebPlal.Text = txtwebparl.Text = txtwebplco.Text = txtweblink2d.Text = txtwebsortn.Text = "";
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btneditWeb")
            {
                string script = "window.onload = function() { showhidepanelWebProd('Webproduct'); };";
                ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
                int ID = Convert.ToInt32(e.CommandArgument);
                Eco_tblWebProd obj = DB.Eco_tblWebProd.Single(p => p.WEBID == ID);
                ddlwebinterval.SelectedValue = obj.INTERVAL.ToString();
                txtwebfromdt.Text = Convert.ToDateTime(obj.FROMDATE).ToString("yyyy/MM/dd");
                txtwebtomdt.Text = Convert.ToDateTime(obj.TODATE).ToString("yyyy/MM/dd");
                txtwebtillmdt.Text = Convert.ToDateTime(obj.TILLDATE).ToString("yyyy/MM/dd");
                ViewState["WebProdID"] = ID;
            }
            if (e.CommandName == "DeleteWeb")
            {
                string script = "window.onload = function() { showhidepanelWebProd('Webproduct'); };";
                ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
                int ID = Convert.ToInt32(e.CommandArgument);
                Eco_tblWebProd obj = DB.Eco_tblWebProd.Single(p => p.WEBID == ID);
                obj.ACTIVE = false;
                DB.SaveChanges();
                BindWebProd();

            }
        }

        protected void btnAddMainImg_Click(object sender, EventArgs e)
        {
            string script = "window.onload = function() { showhidepanelWebProd('Imagges'); };";
            ClientScript.RegisterStartupScript(this.GetType(), "showhidepanelWebProd", script, true);
            int MYPODID = Convert.ToInt32(lblmstproid.Text);
            if (FileUpload2.HasFile)
            {
                Database.Eco_ImageTable objImageTable = new Database.Eco_ImageTable();
                objImageTable.TenantID = 1;
                objImageTable.MYPRODID = MYPODID;
                objImageTable.ImageID = 98801;
                FileUpload2.SaveAs(Server.MapPath("Upload/") + FileUpload2.FileName);
                objImageTable.PICTURE = FileUpload2.FileName;
                objImageTable.sortnumber = 999999999;
                objImageTable.Active = "1";
                objImageTable.COLORID = 0;
                objImageTable.SIZECODE = 0;
                DB.Eco_ImageTable.AddObject(objImageTable);
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                MainImgBind();
            }
        }
        public void MainImgBind()
        {
            int MYPODID = Convert.ToInt32(lblmstproid.Text);
            Repeater4.DataSource = DB.Eco_ImageTable.Where(p => p.TenantID == 1 && p.MYPRODID == MYPODID && p.sortnumber == 999999999);
            Repeater4.DataBind();
        }

        protected void Repeater4_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName =="btnMainDetet")
            {
                int imgID = Convert.ToInt32(e.CommandArgument);
                Database.Eco_ImageTable objImageTable = DB.Eco_ImageTable.Single(p => p.ID == imgID);
                objImageTable.Active = "0";
                DB.SaveChanges();
                Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ImageTable");
                objEco_ERP_WEB_USER_MST1.IsCache = true;
                objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                DB.SaveChanges();
                MainImgBind();
            }
        }



    }
}