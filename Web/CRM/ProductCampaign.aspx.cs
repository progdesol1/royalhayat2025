using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web.CRM
{
    public partial class ProductCampaign : System.Web.UI.Page
    {
        int TID = 1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            if (!IsPostBack)
            {
                Clear();
                fillsubcat();
                fillmaincat();
                radioBind();
                PnlBody1("block", "collapse");
                pnlBody2("none", "expand");
                pnlBody3("none", "expand");
                btnsave.ValidationGroup = "ss";
                // lnlredemption.ValidationGroup = "sm";
                lnkreawrd.ValidationGroup = "sd";
                lblnext1.ValidationGroup = "sj";
                //  lblnext2.ValidationGroup = "sy";

                if (Request.QueryString["CampaignID"] != null)
                {
                    int MAXID = Convert.ToInt32(Request.QueryString["CampaignID"]);
                    Database.Campaign_Master objtblsetupsalesh = DB.Campaign_Master.Single(p => p.CampaignID == MAXID && p.TenentID == TID);
                    //if (objtblsetupsalesh.CampaignType != null)
                    //{
                    //    drpCampaignType.SelectedValue = objtblsetupsalesh.CampaignType.ToString();
                    //}
                    if (objtblsetupsalesh.ProductGoal != null)
                    {
                        drpproductgoal.SelectedValue = objtblsetupsalesh.ProductGoal.ToString();
                    }
                    if (objtblsetupsalesh.CampaignName != null)
                    {
                        txtname.Text = objtblsetupsalesh.CampaignName.ToString();
                    }
                    if (objtblsetupsalesh.RewardType != null)
                    {
                        drprewards.SelectedValue = objtblsetupsalesh.RewardType.ToString();
                    }

                    if (objtblsetupsalesh.PurchaseType != null)
                    {
                        RDO1.SelectedValue = objtblsetupsalesh.PurchaseType.ToString();
                    }
                    if (objtblsetupsalesh.CExpirationDate != null)
                    {
                        txtexpirydate.Text = objtblsetupsalesh.CExpirationDate.ToString();
                    }

                    if (objtblsetupsalesh.CPercentageDiscount != null)
                    {
                        drppercentage.SelectedValue = objtblsetupsalesh.CPercentageDiscount.ToString();
                    }
                    if (objtblsetupsalesh.CIsMultiple != null)
                    {
                        rdo2.SelectedValue = objtblsetupsalesh.CIsMultiple.ToString();
                    }
                    if (objtblsetupsalesh.CEndDate != null)
                    {
                        txtenddate.Text = objtblsetupsalesh.CEndDate.ToString();
                    }
                    if (objtblsetupsalesh.CExpirationDate != null)
                    {
                        txtexpirydate.Text = objtblsetupsalesh.CExpirationDate.ToString();
                    }
                    if (objtblsetupsalesh.CActive != null)
                    {
                        drpstatus.SelectedValue = objtblsetupsalesh.CActive.ToString();
                    }


                }
                else
                {

                }
            }
        }

        public void Clear()
        {
            //  drpCampaignType.SelectedIndex = 0;
            drpproductgoal.SelectedIndex = 0;
            txtskud.Text = "";
            UpdatePanel2.Visible = false;
            UpdatePanel1.Visible = false;
            UpdatePanel3.Visible = false;
            txtsearch.Visible = false;
            txtsku.Visible = false;
            drpcategory.Visible = false;
            drprewards.SelectedIndex = 0;
            drppercentage.SelectedIndex = 0;
            txtenddate.Text = "";
            txtexpirydate.Text = "";
            drppercentage.SelectedIndex = 0;
            drpstatus.SelectedIndex = 0;
            txtCustomPercentage.Text = "0";
            //  lblgoal.Text = "";
            //  lblmsg.Text = "";
            lblmultiple.Text = "";
            lblper.Text = "";
            lblreward.Text = "";
            lblgo.Visible = false;
            lblrewa.Visible = false;
            lblgoals.Visible = false;
            lblending.Visible = false;
            txtname.Text = "";
            lnkserxh.Visible = false;
            ViewState["MultiProd"] = null;
            ViewState["TempCat"] = null;
            List<Database.TBLPRODUCT> List = new List<Database.TBLPRODUCT>();
            Listview1.DataSource = List;
            Listview1.DataBind();
            Listview2.DataSource = List;
            Listview2.DataBind();
            Listview3.DataSource = List;
            Listview3.DataBind();
        }

        public void BindData()
        {
            List<Database.TBLPRODUCT> List = DB.TBLPRODUCTs.Where(m => m.TenentID == TID).OrderBy(m => m.MYPRODID).ToList();
            Listview2.Visible = true;
            Listview2.DataSource = List;
            Listview2.DataBind();
        }


        public void fillsubcat()
        {
            string str1 = drpcategory.SelectedValue;
            drpcategory.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MainCategoryID == 2);
            drpcategory.DataTextField = "ProdName1";
            drpcategory.DataValueField = "MYPRODID";
            drpcategory.DataBind();
            drpcategory.Items.Insert(0, new ListItem("SELECT", "0"));

            DRPCatSearch.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE != null && p.CAT_TYPE != "").GroupBy(p => p.CAT_TYPE).Select(p => p.FirstOrDefault());
            DRPCatSearch.DataTextField = "CAT_TYPE";
            DRPCatSearch.DataValueField = "CAT_TYPE";
            DRPCatSearch.DataBind();
            DRPCatSearch.Items.Insert(0, new ListItem("SELECT", "0"));
        }

        public void fillmaincat()
        {
            string str1 = drpcat.SelectedValue;
            drpcat.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.CATID == 2);
            drpcat.DataTextField = "CAT_NAME1";
            drpcat.DataValueField = "CATID";
            drpcat.DataBind();
            drpcat.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        protected void RDO1_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioBind();
        }
        public void radioBind()
        {
            if (RDO1.SelectedValue == "1")
            {
                txtsearch.Visible = true;
                txtsku.Visible = true;
                Listview2.Visible = true;
                UpdatePanel2.Visible = false;
                UpdatePanel1.Visible = true;
                UpdatePanel3.Visible = false;
                drpcategory.Visible = false;
                lnkdAdd.Visible = false;
                lblsubcat.Visible = false;
                drpcat.Visible = false;
                txtskud.Visible = false;
                lnksub.Visible = false;
                lnkserxh.Visible = true;
                //txtcat.Visible = false;
                DRPCatSearch.Visible = false;
                lnksearches.Visible = false;
                BindData();
            }
            else if (RDO1.SelectedValue == "2")
            {
                txtskud.Visible = true;
                txtsearch.Visible = false;
                txtsku.Visible = false;
                drpcategory.Visible = true;
                txtskud.Visible = true;
                lnksub.Visible = true;
                //  txtskud.Text = 
                lnkdAdd.Visible = true;
                UpdatePanel2.Visible = true;
                UpdatePanel3.Visible = false;
                UpdatePanel1.Visible = false;
                lblsubcat.Visible = false;
                drpcat.Visible = false;
                lnkserxh.Visible = false;
                //txtcat.Visible = false;
                DRPCatSearch.Visible = false;
                lnksearches.Visible = false;
            }
            else if (RDO1.SelectedValue == "3")
            {
                txtsearch.Visible = false;
                txtsku.Visible = false;
                UpdatePanel2.Visible = false;
                UpdatePanel1.Visible = false;
                UpdatePanel3.Visible = true;
                drpcategory.Visible = false;
                lnkdAdd.Visible = false;
                lblsubcat.Visible = true;
                drpcat.Visible = true;
                txtskud.Visible = false;
                lnkserxh.Visible = false;
                lnksub.Visible = false;
                //txtcat.Visible = true;
                DRPCatSearch.Visible = true;
                lnksearches.Visible = true;
            }
        }


        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            lblmsg1.Visible = false;
            if (e.CommandName == "btnselect")
            {
                int MyPRODID = Convert.ToInt32(e.CommandArgument);

                var sdt = Convert.ToDateTime(txtSTDate.Text).ToString("yyyy-MM-dd");
                var edt = Convert.ToDateTime(txtEDDate.Text).ToString("yyyy-MM-dd");
                string SQOCommad = "select * from Campaign_MasterProdCat where TenentID = " + TID + " and ProductID = " + MyPRODID + " and ((StartDate between '" + sdt + "' and '" + edt + "') or (EndDate between '" + sdt + "' and '" + edt + "'));";
                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int CampaignID =Convert.ToInt32(dt.Rows[0]["CampaignID"]);
                    string campainame = DB.Campaign_Master.Single(p => p.TenentID == TID && p.CampaignID == CampaignID).CampaignName;
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "This Product Is Allready exist for campaign '" + campainame + "' between from to Todate";
                }
                else
                {
                    txtsearch.Text = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == MyPRODID).FirstOrDefault().ProdName1;
                    List<Database.TBLPRODUCT> List = DB.TBLPRODUCTs.Where(m => m.TenentID == TID && m.MYPRODID == MyPRODID).OrderBy(m => m.MYPRODID).ToList();
                    Listview2.DataSource = List;
                    Listview2.DataBind();
                }
            }
        }



        protected void drprewards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drprewards.SelectedValue == "1")
            {
                drppercentage.Visible = false;
            }
            else if (drprewards.SelectedValue == "2")
            {
                drppercentage.Visible = true;
            }
            else
            {
                drppercentage.Visible = false;
            }
        }

        protected void btnsave_Click1(object sender, EventArgs e)
        {
            int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID)) : 1;
            Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
            objCampaign_Master.TenentID = TID;
            objCampaign_Master.LocationID = 1;
            objCampaign_Master.CampaignID = ID;
            objCampaign_Master.CampaignType = 2;
            //   objCampaign_Master.CampaignName = txtCampaignName.Text;

            objCampaign_Master.ProductID = 1;
            if (drpproductgoal != null)
            {
                objCampaign_Master.ProductGoal = Convert.ToInt32(drpproductgoal.SelectedValue);
            }

            if (txtcustom.Text != null && txtcustom.Text != "")
            {
                objCampaign_Master.ProductGoal = Convert.ToInt32(txtcustom.Text);
            }
            if (txtname.Text != null)
            {
                objCampaign_Master.CampaignName = txtname.Text;
            }

            if (RDO1.SelectedValue != null)
            {
                objCampaign_Master.PurchaseType = Convert.ToInt32(RDO1.SelectedValue);
                if (RDO1.SelectedValue == "1")
                {
                    txtsearch.Visible = true;
                    txtsku.Visible = true;
                    //BindData();
                }
                else if (RDO1.SelectedValue == "2")
                {
                    drpcategory.Visible = true;
                    txtskud.Visible = true;

                }
                else
                {

                }
            }
            // lblgoal.Visible = true;
            // lblgoal.Text = "Goal :" + drpproductgoal.SelectedItem;
            btnsave.ValidationGroup = "submit";
        }

        public void PnlBody1(string body, string panal)
        {
            body1.Style.Add("display", body);
            pack1.Attributes["class"] = panal;
        }
        public void pnlBody2(string body, string panal)
        {
            body2.Style.Add("display", body);
            pack2.Attributes["class"] = panal;

        }

        public void panelset(string body, string pack)
        {
            //body3.Style.Add("display", body);
            //pack3.Attributes["class"] = pack;
        }
        public void pnlBody3(string body, string panal)
        {
            body3.Style.Add("display", body);
            pack3.Attributes["class"] = panal;
        }
        //protected void btnNext_Click(object sender, EventArgs e)
        //{
        //    PackSetpanel("block", "collapse");
        //    petsetpanel("none", "expand");
        //    panelreset("none", "expand");
        //}

        protected void lnkreawrd_Click(object sender, EventArgs e)
        {
            int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID)) : 1;
            Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
            objCampaign_Master.TenentID = TID;
            objCampaign_Master.LocationID = 1;
            objCampaign_Master.CampaignID = ID;
            if (drprewards.SelectedValue != null)
            {
                objCampaign_Master.RewardType = Convert.ToInt32(drprewards.SelectedValue);
            }
            if (drppercentage.SelectedValue != null)
            {
                objCampaign_Master.CPercentageDiscount = drppercentage.SelectedValue;
            }
            lblreward.Visible = true;
            lblreward.Text = "Reward Type :" + drprewards.SelectedItem;
            lblper.Visible = true;
            lblper.Text = drppercentage.SelectedItem + " discount";
            lnkreawrd.ValidationGroup = "submit";
        }

        protected void lblnext1_Click(object sender, EventArgs e)
        {
            pnlBody2("none", "expand");
            PnlBody1("none", "expand");
            pnlBody3("block", "collapse");
            lblrewa.Text = "Get " + drprewards.SelectedItem + "(" + "Valid for " + rdo2.SelectedItem; ;
        }

        protected void lnksavecampaign_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID) + 1) : 1;
                Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
                objCampaign_Master.TenentID = TID;
                objCampaign_Master.LocationID = 1;
                objCampaign_Master.CampaignID = ID;
                objCampaign_Master.CampaignType = 2;
                //   objCampaign_Master.CampaignName = txtCampaignName.Text;
                if (objCampaign_Master.CampaignType == 1)
                {
                    objCampaign_Master.SpendID = 1;
                }
                else
                {
                    objCampaign_Master.ProductID = 2;
                }
                if (txtname.Text != null)
                {
                    objCampaign_Master.CampaignName = txtname.Text;
                }
                if (drpproductgoal.SelectedValue != null)
                {
                    objCampaign_Master.ProductGoal = Convert.ToInt32(drpproductgoal.SelectedValue);
                }

                if (txtcustom.Text != null && txtcustom.Text != "")
                {
                    objCampaign_Master.ProductGoal = Convert.ToInt32(txtcustom.Text);
                }
                if (RDO1.SelectedValue != null)
                {


                    objCampaign_Master.PurchaseType = Convert.ToInt32(RDO1.SelectedValue);
                    if (RDO1.SelectedValue == "1")
                    {
                        objCampaign_Master.PurchaseType = 1;
                    }
                    else if (RDO1.SelectedValue == "2")
                    {
                        objCampaign_Master.PurchaseType = 2;
                    }
                    else if (RDO1.SelectedValue == "3")
                    {
                        objCampaign_Master.PurchaseType = 3;
                    }
                    objCampaign_Master.RewardType = Convert.ToInt32(drprewards.SelectedValue);
                    if (Convert.ToInt32(drppercentage.SelectedValue) == 1)
                    {
                        objCampaign_Master.CPercentageDiscount = txtCustomPercentage.Text;
                    }
                    else
                    {
                        objCampaign_Master.CPercentageDiscount = drppercentage.SelectedValue;
                    }
                    if (rdo2.SelectedValue == "Y")
                    {
                        objCampaign_Master.CIsMultiple = true;
                        objCampaign_Master.CIsSingle = false;
                    }
                    else
                    {
                        objCampaign_Master.CIsMultiple = false;
                        objCampaign_Master.CIsSingle = true;
                    }
                    if (drpstatus.SelectedValue == "1")
                    {
                        objCampaign_Master.CActive = true;
                    }
                    else
                    {
                        objCampaign_Master.CActive = false;
                    }
                    objCampaign_Master.CStartDate = Convert.ToDateTime(txtenddate.Text);
                    objCampaign_Master.CEndDate = Convert.ToDateTime(txtexpirydate.Text);
                    objCampaign_Master.CExpirationDate = Convert.ToDateTime(txtexpirydate.Text);
                    if (RDO1.SelectedValue == "1")
                        PurchaseTypeSpecific(ID);
                    else if (RDO1.SelectedValue == "2")
                        PurchaseTypeMulti(ID);
                    else if (RDO1.SelectedValue == "3")
                        PurchaseTypeCategory(ID);
                    lblending.Text = "Campaign End Date : " + txtenddate.Text + "Reward Reedemable Until : " + txtexpirydate.Text;
                    DB.Campaign_Master.AddObject(objCampaign_Master);
                    DB.SaveChanges();

                    Clear();
                }
                scope.Complete();
            }
        }
        public void PurchaseTypeSpecific(int CampID)
        {

            for (int i = 0; i < Listview2.Items.Count; i++)
            {
                int count = 0;
                Label lblProdName = (Label)Listview2.Items[i].FindControl("lblProdName");
                Label lblPID = (Label)Listview2.Items[i].FindControl("lblPID");
                Label Label3 = (Label)Listview2.Items[i].FindControl("Label3");
                string subcategoryName = lblProdName.Text;
                int ProdID = Convert.ToInt32(lblPID.Text);
                int CategoryID = Convert.ToInt32(Label3.Text);
                //  DropDownList drpcategory = (DropDownList)e.Item.FindControl("drpsubcat");
                var exist = DB.Campaign_MasterProdCat.Where(c => c.TenentID == TID && c.LocationID == 1 && c.ProductID == ProdID && c.CampaignID == CampID);
                if (exist.Count() < 1)
                {
                    count++;
                    Campaign_MasterProdCat obj = new Campaign_MasterProdCat();
                    obj.TenentID = TID;
                    obj.LocationID = 1;
                    obj.CampaignID = CampID;
                    obj.ProductID = ProdID;
                    obj.MySerialNumber = DB.Campaign_MasterProdCat.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_MasterProdCat.Where(p => p.TenentID == TID).Max(p => p.MySerialNumber) + 1) : 1;
                    obj.CatID = CategoryID;
                    obj.StartDate = Convert.ToDateTime(txtenddate.Text);
                    obj.EndDate = Convert.ToDateTime(txtexpirydate.Text);
                    if (Convert.ToInt32(drppercentage.SelectedValue) == 1)
                    {
                        obj.Petcentage = Convert.ToInt32(txtCustomPercentage.Text);
                    }
                    else
                    {
                        obj.Petcentage = Convert.ToInt32(drppercentage.SelectedValue);
                    }
                    obj.QtyAllotted = 1;
                    obj.QtyConsumed = 1;
                    obj.AmtAllotted = 1;
                    obj.AmtConsumed = 1;
                    obj.CActive = false;
                    // obj.Rremark = "AutomatedProcess";
                    DB.Campaign_MasterProdCat.AddObject(obj);
                    DB.SaveChanges();
                }
                else
                {
                    string display = "Sub Category Is Duplicate!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Sub Category Is Duplicate!", "alert('" + display + "');", true);
                    return;
                }
            }
        }
        public void PurchaseTypeMulti(int CampID)
        {

            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                int count = 0;
                Label lblProdName = (Label)Listview1.Items[i].FindControl("lblProdName");
                Label lblPID = (Label)Listview1.Items[i].FindControl("lblPID");
                Label lbltype = (Label)Listview1.Items[i].FindControl("lbltype");
                string subcategoryName = lblProdName.Text;
                int ProdID = Convert.ToInt32(lblPID.Text);
                int CategoryID = Convert.ToInt32(lbltype.Text);
                //  DropDownList drpcategory = (DropDownList)e.Item.FindControl("drpsubcat");
                var exist = DB.Campaign_MasterProdCat.Where(c => c.TenentID == TID && c.LocationID == 1 && c.ProductID == ProdID && c.CampaignID == CampID);
                if (exist.Count() < 1)
                {
                    count++;
                    Campaign_MasterProdCat obj = new Campaign_MasterProdCat();
                    obj.TenentID = TID;
                    obj.LocationID = 1;
                    obj.CampaignID = CampID;
                    obj.ProductID = ProdID;
                    obj.MySerialNumber = DB.Campaign_MasterProdCat.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_MasterProdCat.Where(p => p.TenentID == TID).Max(p => p.MySerialNumber) + 1) : 1;
                    obj.CatID = CategoryID;
                    obj.StartDate = Convert.ToDateTime(txtenddate.Text);
                    obj.EndDate = Convert.ToDateTime(txtexpirydate.Text);
                    if (Convert.ToInt32(drppercentage.SelectedValue) == 1)
                    {
                        obj.Petcentage = Convert.ToInt32(txtCustomPercentage.Text);
                    }
                    else
                    {
                        obj.Petcentage = Convert.ToInt32(drppercentage.SelectedValue);
                    }
                    obj.QtyAllotted = 1;
                    obj.QtyConsumed = 1;
                    obj.AmtAllotted = 1;
                    obj.AmtConsumed = 1;
                    obj.CActive = false;
                    // obj.Rremark = "AutomatedProcess";
                    DB.Campaign_MasterProdCat.AddObject(obj);
                    DB.SaveChanges();
                }

                else
                {
                    string display = "Sub Category Is Duplicate!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Sub Category Is Duplicate!", "alert('" + display + "');", true);
                    return;
                }
            }
        }
        public void PurchaseTypeCategory(int CampID)
        {
            for (int i = 0; i < Listview3.Items.Count; i++)
            {
                int count = 0;
                Label lblProdName = (Label)Listview3.Items[i].FindControl("lblProdName");
                Label lblPID = (Label)Listview3.Items[i].FindControl("lblPID");
                Label lbltype = (Label)Listview3.Items[i].FindControl("lbltype");
                string subcategoryName = lblProdName.Text;
                int ProdID = Convert.ToInt32(lblPID.Text);
                int CATID = Convert.ToInt32(lbltype.Text);
                //  DropDownList drpcategory = (DropDownList)e.Item.FindControl("drpsubcat");
                var exist = DB.Campaign_MasterProdCat.Where(c => c.TenentID == TID && c.LocationID == 1 && c.ProductID == ProdID && c.CampaignID == CampID);
                if (exist.Count() < 1)
                {
                    count++;
                    Campaign_MasterProdCat obj = new Campaign_MasterProdCat();
                    obj.TenentID = TID;
                    obj.LocationID = 1;
                    obj.CampaignID = CampID;
                    obj.ProductID = ProdID;
                    obj.MySerialNumber = DB.Campaign_MasterProdCat.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_MasterProdCat.Where(p => p.TenentID == TID).Max(p => p.MySerialNumber) + 1) : 1;
                    obj.CatID = CATID;
                    obj.StartDate = Convert.ToDateTime(txtenddate.Text);
                    obj.EndDate = Convert.ToDateTime(txtexpirydate.Text);
                    if (Convert.ToInt32(drppercentage.SelectedValue) == 1)
                    {
                        obj.Petcentage = Convert.ToInt32(txtCustomPercentage.Text);
                    }
                    else
                    {
                        obj.Petcentage = Convert.ToInt32(drppercentage.SelectedValue);
                    }
                    obj.QtyAllotted = 1;
                    obj.QtyConsumed = 1;
                    obj.AmtAllotted = 1;
                    obj.AmtConsumed = 1;
                    obj.CActive = false;
                    // obj.Rremark = "AutomatedProcess";
                    DB.Campaign_MasterProdCat.AddObject(obj);
                    DB.SaveChanges();
                }

                else
                {
                    string display = "Sub Category Is Duplicate!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Sub Category Is Duplicate!", "alert('" + display + "');", true);
                    return;
                }
            }

        }
        protected void lnkjnext_Click(object sender, EventArgs e)
        {
            pnlBody2("block", "collapse");
            PnlBody1("none", "expand");
            pnlBody3("none", "expand");
            lblgo.Text = "Goal : " + drpproductgoal.SelectedItem;
        }

        protected void lblsubcat_Click(object sender, EventArgs e)
        {
            lblmsg1.Visible = false;
            int prodid = Convert.ToInt32(drpcat.SelectedValue);
            List<Database.TBLPRODUCT> TempList = new List<Database.TBLPRODUCT>();
            int MyPRODID = prodid;

            var sdt = Convert.ToDateTime(txtSTDate.Text).ToString("yyyy-MM-dd");
            var edt = Convert.ToDateTime(txtEDDate.Text).ToString("yyyy-MM-dd");
            string SQOCommad = "select * from Campaign_MasterProdCat where TenentID = " + TID + " and ProductID = " + MyPRODID + " and ((StartDate between '" + sdt + "' and '" + edt + "') or (EndDate between '" + sdt + "' and '" + edt + "'));";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                int CampaignID = Convert.ToInt32(dt.Rows[0]["CampaignID"]);
                string campainame = DB.Campaign_Master.Single(p => p.TenentID == TID && p.CampaignID == CampaignID).CampaignName;
                lblmsg1.Visible = true;
                lblmsg1.Text = "This Product Is Allready exist for campaign '" + campainame + "' between from to Todate";
            }
            else
            {
                if (prodid != 0)
                {
                    if (ViewState["TempCat"] == null)
                    {
                        Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == prodid);
                        TempList.Add(obj);
                        ViewState["TempCat"] = TempList;
                    }
                    else
                    {
                        TempList = ((List<Database.TBLPRODUCT>)ViewState["TempCat"]).ToList();
                        if (TempList.Where(p => p.TenentID == TID && p.MYPRODID == prodid).Count() > 0)
                        {
                            Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == prodid);
                            TempList.Add(obj);
                            ViewState["TempCat"] = TempList;
                        }
                    }
                }
            }
            Listview3.DataSource = TempList;
            Listview3.DataBind();
        }
        protected void Listview3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            List<Database.TBLPRODUCT> TempList = new List<Database.TBLPRODUCT>();
            if (e.CommandName == "btnRemove")
            {
                int Prodid = Convert.ToInt32(e.CommandArgument);
                TempList = ((List<Database.TBLPRODUCT>)ViewState["TempCat"]).ToList();
                Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == Prodid);
                TempList.Remove(obj);
                ViewState["TempCat"] = TempList;
            }
            Listview3.DataSource = TempList;
            Listview3.DataBind();
        }
        protected void lnkdAdd_Click(object sender, EventArgs e)
        {
            lblmsg1.Visible = false;
            List<Database.TBLPRODUCT> TempList = new List<Database.TBLPRODUCT>();
            int prodid = Convert.ToInt32(drpcategory.SelectedValue);
            int MyPRODID = prodid;

            var sdt = Convert.ToDateTime(txtSTDate.Text).ToString("yyyy-MM-dd");
            var edt = Convert.ToDateTime(txtEDDate.Text).ToString("yyyy-MM-dd");
            string SQOCommad = "select * from Campaign_MasterProdCat where TenentID = " + TID + " and ProductID = " + MyPRODID + " and ((StartDate between '" + sdt + "' and '" + edt + "') or (EndDate between '" + sdt + "' and '" + edt + "'));";
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                int CampaignID = Convert.ToInt32(dt.Rows[0]["CampaignID"]);
                string campainame = DB.Campaign_Master.Single(p => p.TenentID == TID && p.CampaignID == CampaignID).CampaignName;
                lblmsg1.Visible = true;
                lblmsg1.Text = "This Product Is Allready exist for campaign '" + campainame + "' between from to Todate";
            }
            else
            {
                if (prodid != 0)
                {
                    if (ViewState["MultiProd"] == null)
                    {
                        Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == prodid);
                        TempList.Add(obj);
                        ViewState["MultiProd"] = TempList;
                    }
                    else
                    {
                        TempList = ((List<Database.TBLPRODUCT>)ViewState["MultiProd"]).ToList();
                        if (TempList.Where(p => p.TenentID == TID && p.MYPRODID == prodid).Count() > 0)
                        {
                            Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == prodid);
                            TempList.Add(obj);
                            ViewState["MultiProd"] = TempList;
                        }
                    }
                }
            }
            Listview1.DataSource = TempList;
            Listview1.DataBind();
            UpdatePanel2.Visible = true;
        }
        protected void Listview1_ItemCommand1(object sender, ListViewCommandEventArgs e)
        {
            List<Database.TBLPRODUCT> TempList = new List<Database.TBLPRODUCT>();
            if (e.CommandName == "btnDelete")
            {
                TempList = ((List<Database.TBLPRODUCT>)ViewState["MultiProd"]).ToList();
                int prodid = Convert.ToInt32(e.CommandArgument);
                Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == prodid);
                TempList.Remove(obj);
                ViewState["MultiProd"] = TempList;
            }
            Listview1.DataSource = TempList;
            Listview1.DataBind();
            UpdatePanel2.Visible = true;

        }
        public bool BindAddvansearch(TextBox txtskud, DropDownList drpcategory)
        {
            string id1 = txtskud.Text.Trim().ToString();
            //id1.ToCharArray[0];
            //id1.Substring(0, 1);
            id1 = id1.TrimStart('0');
            var list1 = DB.View_Product_fetch.Where(p => (p.UserProdID.ToUpper().Contains(id1.ToUpper()) || p.BarCode.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode1.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode2.ToUpper().Contains(id1.ToUpper()) || p.ShortName.ToUpper().Contains(id1.ToUpper()) || p.ProdName2.ToUpper().Contains(id1.ToUpper()) || p.ProdName3.ToUpper().Contains(id1.ToUpper()) || p.keywords.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.DescToprint.ToUpper().Contains(id1.ToUpper()) || p.ProdName1.ToUpper().Contains(id1.ToUpper())) && p.ACTIVE == "1" && p.TenentID == TID).OrderBy(p => p.MYPRODID).ToList();

            if (list1.Count() > 0)
            {
                drpcategory.DataSource = list1;
                drpcategory.DataTextField = "ProdName1";
                drpcategory.DataValueField = "MYPRODID";
                drpcategory.DataBind();
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void lnksub_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            if (txtskud.Text != null && txtskud.Text != "")
            {
                bool flag = BindAddvansearch(txtskud, drpcategory);
                if (flag == false)
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "Product not found in search";

                }
                //   return false;
            }

        }

        protected void drpproductgoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpproductgoal.SelectedValue == "11")
            {
                txtcustom.Visible = true;
            }
            else
            {
                txtcustom.Visible = false;
            }

        }


        public bool BindAddvansearches(TextBox txtsku, ListView Listview2)
        {
            string id1 = txtsku.Text.Trim().ToString();
            //id1.ToCharArray[0];
            //id1.Substring(0, 1);
            id1 = id1.TrimStart('0');
            var list1 = DB.View_Product_fetch.Where(p => (p.UserProdID.ToUpper().Contains(id1.ToUpper()) || p.BarCode.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode1.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode2.ToUpper().Contains(id1.ToUpper()) || p.ShortName.ToUpper().Contains(id1.ToUpper()) || p.ProdName2.ToUpper().Contains(id1.ToUpper()) || p.ProdName3.ToUpper().Contains(id1.ToUpper()) || p.keywords.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.DescToprint.ToUpper().Contains(id1.ToUpper()) || p.ProdName1.ToUpper().Contains(id1.ToUpper())) && p.ACTIVE == "1" && p.TenentID == TID).OrderBy(p => p.MYPRODID).ToList();

            if (list1.Count() > 0)
            {
                Listview2.DataSource = list1;
                Listview2.DataBind();
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void lnkserxh_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            if (txtsku.Text != null && txtsku.Text != "")
            {
                bool flag = BindAddvansearches(txtsku, Listview2);
                if (flag == false)
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "Product not found in search";

                }
                //   return false;
            }
        }

        public bool BindAddvansearched(DropDownList drpcat)
        {
            string id1 = "";//txtcat.Text.Trim().ToString();
            //id1.ToCharArray[0];
            //id1.Substring(0, 1);
            id1 = id1.TrimStart('0');
            var list1 = DB.View_Product_fetch.Where(p => (p.UserProdID.ToUpper().Contains(id1.ToUpper()) || p.BarCode.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode1.ToUpper().Contains(id1.ToUpper()) || p.AlternateCode2.ToUpper().Contains(id1.ToUpper()) || p.ShortName.ToUpper().Contains(id1.ToUpper()) || p.ProdName2.ToUpper().Contains(id1.ToUpper()) || p.ProdName3.ToUpper().Contains(id1.ToUpper()) || p.keywords.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.DescToprint.ToUpper().Contains(id1.ToUpper()) || p.ProdName1.ToUpper().Contains(id1.ToUpper())) && p.ACTIVE == "1" && p.TenentID == TID).OrderBy(p => p.MYPRODID).ToList();

            if (list1.Count() > 0)
            {
                drpcat.DataSource = list1;
                drpcat.DataTextField = "ProdName1";
                drpcat.DataValueField = "MYPRODID";
                drpcat.DataBind();
                return true;
            }
            else
            {
                return false;
            }

        }

        protected void lnksearches_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            ////if (txtcat.Text != null && txtcat.Text != "")
            ////{
            //    bool flag = BindAddvansearched(drpcat);
            //    if (flag == false)
            //    {
            //        panelMsg.Visible = true;
            //        lblErreorMsg.Text = "Product not found in search";

            //    }
            ////}
            string Cattype = DRPCatSearch.SelectedValue.ToString();
            if (Cattype != "0")
            {
                List<Database.TBLPRODUCT> CatProd = new List<Database.TBLPRODUCT>();
                List<Database.TBLPRODUCT> TempProd = new List<Database.TBLPRODUCT>();
                List<Database.CAT_MST> ListCatType = DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE == Cattype && p.PARENT_CATID == 0).ToList();
                foreach (Database.CAT_MST item in ListCatType)
                {
                    int catID = Convert.ToInt32(item.CATID);
                    TempProd = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MainCategoryID == catID).ToList();
                    foreach (Database.TBLPRODUCT pitem in TempProd)
                    {
                        CatProd.Add(pitem);
                    }
                }
                drpcat.DataSource = CatProd;
                drpcat.DataTextField = "ProdName1";
                drpcat.DataValueField = "MYPRODID";
                drpcat.DataBind();
                if (CatProd.Count() == 0)
                    drpcat.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            else
            {
                panelMsg.Visible = true;
                lblErreorMsg.Text = "Product not found in search";
            }
        }

        protected void drppercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CP = Convert.ToInt32(drppercentage.SelectedValue);
            Per(CP);
        }
        public void Per(int CP)
        {
            if (CP == 1)
            {
                txtCustomPercentage.Visible = true;
            }
            else
            {
                txtCustomPercentage.Visible = false;
            }
        }

        protected void txtSTDate_TextChanged(object sender, EventArgs e)
        {
            string date = txtSTDate.Text;
            txtenddate.Text = date;
        }

        protected void txtEDDate_TextChanged(object sender, EventArgs e)
        {
            string date = txtEDDate.Text;
            txtexpirydate.Text = date;
        }




    }
}