using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace Web.Master
{
    public partial class tblProduct_Plan : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            PnlGriderror.Visible = false;
            lblGridErrormsg.Text = "";

            pnlError.Visible = false;
            lblErrorMsg.Text = "";

            if (!IsPostBack)
            {
                int TID = ((USER_MST)Session["USER"]).TenentID;
                string uidd = ((USER_MST)Session["USER"]).USER_ID.ToString();
                bool Isadmin = Classes.CRMClass.ISAdmin(TID, uidd);
                if (Isadmin == false)
                {
                    btnEditLable.Visible = false;
                }
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "s";
            }
        }
        #region Step2
        public void BindData()
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            Listview1.DataSource = DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.active == true).OrderBy(p=>p.SortBy);
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lblplanid1s.Attributes["class"] = lblplanname11s.Attributes["class"] = lblplanname21s.Attributes["class"] = lblplanname31s.Attributes["class"] = lblPlan_cost1s.Attributes["class"] = lblPlan_price11s.Attributes["class"] = lblPlan_price21s.Attributes["class"] = lblPlan_price31s.Attributes["class"] = lblccount1s.Attributes["class"] = lblPlan_sale1s.Attributes["class"] = lblaccount1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblplanid2h.Attributes["class"] = lblplanname12h.Attributes["class"] = lblplanname22h.Attributes["class"] = lblplanname32h.Attributes["class"] = lblPlan_cost2h.Attributes["class"] = lblPlan_price12h.Attributes["class"] = lblPlan_price22h.Attributes["class"] = lblPlan_price32h.Attributes["class"] = lblccount2h.Attributes["class"] = lblPlan_sale2h.Attributes["class"] = lblaccount2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblplanid1s.Attributes["class"] = lblplanname11s.Attributes["class"] = lblplanname21s.Attributes["class"] = lblplanname31s.Attributes["class"] = lblPlan_cost1s.Attributes["class"] = lblPlan_price11s.Attributes["class"] = lblPlan_price21s.Attributes["class"] = lblPlan_price31s.Attributes["class"] = lblccount1s.Attributes["class"] = lblPlan_sale1s.Attributes["class"] = lblaccount1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblplanid2h.Attributes["class"] = lblplanname12h.Attributes["class"] = lblplanname22h.Attributes["class"] = lblplanname32h.Attributes["class"] = lblPlan_cost2h.Attributes["class"] = lblPlan_price12h.Attributes["class"] = lblPlan_price22h.Attributes["class"] = lblPlan_price32h.Attributes["class"] = lblccount2h.Attributes["class"] = lblPlan_sale2h.Attributes["class"] = lblaccount2h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            GetHide();
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetShow();
        }

        public void Clear()
        {
            //drplocationid.SelectedIndex = 0;
            txtplanid.Text = "";
            txtplanname1.Text = "";
            txtplanname2.Text = "";
            txtplanname3.Text = "";
            //txtactive.Text = "";
            txtPlan_cost.Text = "0.00";
            txtPlan_price1.Text = "0";
            txtPlan_price2.Text = "0";
            txtPlan_price3.Text = "0";
            txtccount.Text = "0";
            txtPlan_sale.Text = "";
            txtaccount.Text = "0";
            //drpcrupid.SelectedIndex = 0;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
            lblErrorMsg.Text = "";

            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Plan = DB.tblProduct_Plan.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblProduct_Plan.Where(p => p.TenentID == TID).Max(p => p.planid) + 1) : 1;
            txtplanid.Text = Plan.ToString();
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (btnAdd.Text == "Add New")
            {
                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "submit";

                txtplanid.Text = Plan.ToString();
            }
            else if (btnAdd.Text == "Add")
            {
                if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planname1 == txtplanname1.Text).Count() < 1)
                {

                    Database.tblProduct_Plan objtblProduct_Plan = new Database.tblProduct_Plan();
                    //Server Content Send data Yogesh
                    objtblProduct_Plan.TenentID = TID;
                    objtblProduct_Plan.locationid = 1;
                    objtblProduct_Plan.planid = Plan;//Convert.ToInt32(txtplanid.Text);
                    objtblProduct_Plan.planname1 = txtplanname1.Text;
                    objtblProduct_Plan.planname2 = txtplanname2.Text;
                    objtblProduct_Plan.planname3 = txtplanname3.Text;
                    objtblProduct_Plan.Plan_cost = Convert.ToDecimal(txtPlan_cost.Text);
                    objtblProduct_Plan.Plan_price1 = Convert.ToInt32(txtPlan_price1.Text);
                    objtblProduct_Plan.Plan_price2 = Convert.ToInt32(txtPlan_price2.Text);
                    objtblProduct_Plan.Plan_price3 = Convert.ToInt32(txtPlan_price3.Text);
                    objtblProduct_Plan.ccount = Convert.ToInt32(txtccount.Text);
                    objtblProduct_Plan.Plan_sale = txtPlan_sale.Text;
                    objtblProduct_Plan.account = Convert.ToInt32(txtaccount.Text);
                    objtblProduct_Plan.active = true;

                    objtblProduct_Plan.UploadDate = DateTime.Now;
                    objtblProduct_Plan.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objtblProduct_Plan.SynID = 1;

                    // objtblProduct_Plan.crupid = Convert.ToInt32(txtcrupid.Text);

                    DB.tblProduct_Plan.AddObject(objtblProduct_Plan);
                    DB.SaveChanges();

                    Classes.EcommAdminClass.update_SubcriptionSetup(TID);
                    Clear();
                    btnAdd.Text = "Add New";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //navigation.Visible = true;
                    Readonly();
                    //FirstData();
                    btnAdd.ValidationGroup = "s";
                }
                else
                {
                    pnlError.Visible = true;
                    lblErrorMsg.Text = "plan Already Exist";
                }
            }
            else if (btnAdd.Text == "Update")
            {
                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.tblProduct_Plan objtblProduct_Plan = DB.tblProduct_Plan.Single(p => p.planid == ID && p.TenentID == TID);
                    //objtblProduct_Plan.locationid = Convert.ToInt32(drplocationid.Text);
                    //objtblProduct_Plan.planid = Convert.ToInt32(txtplanid.Text);
                    objtblProduct_Plan.planname1 = txtplanname1.Text;
                    objtblProduct_Plan.planname2 = txtplanname2.Text;
                    objtblProduct_Plan.planname3 = txtplanname3.Text;
                    //objtblProduct_Plan.active = cbactive.Checked;
                    objtblProduct_Plan.Plan_cost = Convert.ToDecimal(txtPlan_cost.Text);
                    objtblProduct_Plan.Plan_price1 = Convert.ToInt32(txtPlan_price1.Text);
                    objtblProduct_Plan.Plan_price2 = Convert.ToInt32(txtPlan_price2.Text);
                    objtblProduct_Plan.Plan_price3 = Convert.ToInt32(txtPlan_price3.Text);
                    objtblProduct_Plan.ccount = Convert.ToInt32(txtccount.Text);
                    objtblProduct_Plan.Plan_sale = txtPlan_sale.Text;
                    objtblProduct_Plan.account = Convert.ToInt32(txtaccount.Text);

                    objtblProduct_Plan.UploadDate = DateTime.Now;
                    objtblProduct_Plan.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objtblProduct_Plan.SynID = 2;

                    //objtblProduct_Plan.crupid = Convert.ToInt32(drpcrupid.Text);

                    ViewState["Edit"] = null;
                    btnAdd.Text = "Add New";
                    DB.SaveChanges();

                    Classes.EcommAdminClass.update_SubcriptionSetup(TID);
                    Clear();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    //navigation.Visible = true;
                    Readonly();
                    //FirstData();
                    btnAdd.ValidationGroup = "s";
                }
            }
            BindData();

            //        scope.Complete(); //  To commit.

            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            Response.Redirect("~/Master/tblProduct_Plan.aspx");
        }
        public void FillContractorID()
        {
            //drpcrupid.DataSource = DB.0;
            //drpcrupid.DataTextField = "0";
            //drpcrupid.DataValueField = "0";
            //drpcrupid.DataBind();
            //drpcrupid.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            FirstData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            NextData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            PrevData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            LastData();
        }
        public void Manage()
        {
            if (Listview1.SelectedDataKey[2] != null)
                txtplanid.Text = Listview1.SelectedDataKey[2].ToString();
            if (Listview1.SelectedDataKey[3] != null)
                txtplanname1.Text = Listview1.SelectedDataKey[3].ToString();
            if (Listview1.SelectedDataKey[4] != null)
                txtplanname2.Text = Listview1.SelectedDataKey[4].ToString();
            if (Listview1.SelectedDataKey[5] != null)
                txtplanname3.Text = Listview1.SelectedDataKey[5].ToString();
            if (Listview1.SelectedDataKey[7] != null)
                txtPlan_cost.Text = Listview1.SelectedDataKey[7].ToString();
            if (Listview1.SelectedDataKey[8] != null)
                txtPlan_price1.Text = Listview1.SelectedDataKey[8].ToString();
            if (Listview1.SelectedDataKey[9] != null)
                txtPlan_price2.Text = Listview1.SelectedDataKey[9].ToString();
            if (Listview1.SelectedDataKey[10] != null)
                txtPlan_price3.Text = Listview1.SelectedDataKey[10].ToString();
            if (Listview1.SelectedDataKey[11] != null)
                txtccount.Text = Listview1.SelectedDataKey[11].ToString();
            if (Listview1.SelectedDataKey[12] != null)
                txtPlan_sale.Text = Listview1.SelectedDataKey[12].ToString();
            if (Listview1.SelectedDataKey[13] != null)
                txtaccount.Text = Listview1.SelectedDataKey[13].ToString();
        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            Manage();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                Manage();
            }

        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                lblMsg.Text = "This is first record";
                pnlSuccessMsg.Visible = true;

            }
            else
            {
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                Manage();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            Manage();
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblplanid2h.Visible = lblplanname12h.Visible = lblplanname22h.Visible = lblplanname32h.Visible = lblPlan_cost2h.Visible = lblPlan_price12h.Visible = lblPlan_price22h.Visible = lblPlan_price32h.Visible = lblccount2h.Visible = lblPlan_sale2h.Visible = lblaccount2h.Visible = false;
                    //2true
                    txtplanid2h.Visible = txtplanname12h.Visible = txtplanname22h.Visible = txtplanname32h.Visible = txtPlan_cost2h.Visible = txtPlan_price12h.Visible = txtPlan_price22h.Visible = txtPlan_price32h.Visible = txtccount2h.Visible = txtPlan_sale2h.Visible = txtaccount2h.Visible = true;

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
                    lblplanid2h.Visible = lblplanname12h.Visible = lblplanname22h.Visible = lblplanname32h.Visible = lblPlan_cost2h.Visible = lblPlan_price12h.Visible = lblPlan_price22h.Visible = lblPlan_price32h.Visible = lblccount2h.Visible = lblPlan_sale2h.Visible = lblaccount2h.Visible = true;
                    //2false
                    txtplanid2h.Visible = txtplanname12h.Visible = txtplanname22h.Visible = txtplanname32h.Visible = txtPlan_cost2h.Visible = txtPlan_price12h.Visible = txtPlan_price22h.Visible = txtPlan_price32h.Visible = txtccount2h.Visible = txtPlan_sale2h.Visible = txtaccount2h.Visible = false;

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
                    lblplanid1s.Visible = lblplanname11s.Visible = lblplanname21s.Visible = lblplanname31s.Visible = lblPlan_cost1s.Visible = lblPlan_price11s.Visible = lblPlan_price21s.Visible = lblPlan_price31s.Visible = lblccount1s.Visible = lblPlan_sale1s.Visible = lblaccount1s.Visible = false;
                    //1true
                    txtplanid1s.Visible = txtplanname11s.Visible = txtplanname21s.Visible = txtplanname31s.Visible = txtPlan_cost1s.Visible = txtPlan_price11s.Visible = txtPlan_price21s.Visible = txtPlan_price31s.Visible = txtccount1s.Visible = txtPlan_sale1s.Visible = txtaccount1s.Visible = true;
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
                    lblplanid1s.Visible = lblplanname11s.Visible = lblplanname21s.Visible = lblplanname31s.Visible = lblPlan_cost1s.Visible = lblPlan_price11s.Visible = lblPlan_price21s.Visible = lblPlan_price31s.Visible = lblccount1s.Visible = lblPlan_sale1s.Visible = lblaccount1s.Visible = true;
                    //1false
                    txtplanid1s.Visible = txtplanname11s.Visible = txtplanname21s.Visible = txtplanname31s.Visible = txtPlan_cost1s.Visible = txtPlan_price11s.Visible = txtPlan_price21s.Visible = txtPlan_price31s.Visible = txtccount1s.Visible = txtPlan_sale1s.Visible = txtaccount1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblProduct_Plan").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblplanid1s.ID == item.LabelID)
                    txtplanid1s.Text = lblplanid1s.Text = item.LabelName;
                else if (lblplanname11s.ID == item.LabelID)
                    txtplanname11s.Text = lblplanname11s.Text = item.LabelName;
                else if (lblplanname21s.ID == item.LabelID)
                    txtplanname21s.Text = lblplanname21s.Text = item.LabelName;
                else if (lblplanname31s.ID == item.LabelID)
                    txtplanname31s.Text = lblplanname31s.Text = item.LabelName;
                else if (lblPlan_cost1s.ID == item.LabelID)
                    txtPlan_cost1s.Text = lblPlan_cost1s.Text = item.LabelName;
                else if (lblPlan_price11s.ID == item.LabelID)
                    txtPlan_price11s.Text = lblPlan_price11s.Text = item.LabelName;
                else if (lblPlan_price21s.ID == item.LabelID)
                    txtPlan_price21s.Text = lblPlan_price21s.Text = item.LabelName;
                else if (lblPlan_price31s.ID == item.LabelID)
                    txtPlan_price31s.Text = lblPlan_price31s.Text = item.LabelName;
                else if (lblccount1s.ID == item.LabelID)
                    txtccount1s.Text = lblccount1s.Text = item.LabelName;
                else if (lblPlan_sale1s.ID == item.LabelID)
                    txtPlan_sale1s.Text = lblPlan_sale1s.Text = item.LabelName;
                else if (lblaccount1s.ID == item.LabelID)
                    txtaccount1s.Text = lblaccount1s.Text = item.LabelName;

                else if (lblplanid2h.ID == item.LabelID)
                    txtplanid2h.Text = lblplanid2h.Text = item.LabelName;
                else if (lblplanname12h.ID == item.LabelID)
                    txtplanname12h.Text = lblplanname12h.Text = item.LabelName;
                else if (lblplanname22h.ID == item.LabelID)
                    txtplanname22h.Text = lblplanname22h.Text = item.LabelName;
                else if (lblplanname32h.ID == item.LabelID)
                    txtplanname32h.Text = lblplanname32h.Text = item.LabelName;
                else if (lblPlan_cost2h.ID == item.LabelID)
                    txtPlan_cost2h.Text = lblPlan_cost2h.Text = item.LabelName;
                else if (lblPlan_price12h.ID == item.LabelID)
                    txtPlan_price12h.Text = lblPlan_price12h.Text = item.LabelName;
                else if (lblPlan_price22h.ID == item.LabelID)
                    txtPlan_price22h.Text = lblPlan_price22h.Text = item.LabelName;
                else if (lblPlan_price32h.ID == item.LabelID)
                    txtPlan_price32h.Text = lblPlan_price32h.Text = item.LabelName;
                else if (lblccount2h.ID == item.LabelID)
                    txtccount2h.Text = lblccount2h.Text = item.LabelName;
                else if (lblPlan_sale2h.ID == item.LabelID)
                    txtPlan_sale2h.Text = lblPlan_sale2h.Text = item.LabelName;
                else if (lblaccount2h.ID == item.LabelID)
                    txtaccount2h.Text = lblaccount2h.Text = item.LabelName;
                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblProduct_Plan").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblProduct_Plan.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblProduct_Plan").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblplanid1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanid1s.Text;
                else if (lblplanname11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanname11s.Text;
                else if (lblplanname21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanname21s.Text;
                else if (lblplanname31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanname31s.Text;
                else if (lblPlan_cost1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_cost1s.Text;
                else if (lblPlan_price11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_price11s.Text;
                else if (lblPlan_price21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_price21s.Text;
                else if (lblPlan_price31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_price31s.Text;
                else if (lblccount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtccount1s.Text;
                else if (lblPlan_sale1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_sale1s.Text;
                else if (lblaccount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount1s.Text;

                else if (lblplanid2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanid2h.Text;
                else if (lblplanname12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanname12h.Text;
                else if (lblplanname22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanname22h.Text;
                else if (lblplanname32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanname32h.Text;
                else if (lblPlan_cost2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_cost2h.Text;
                else if (lblPlan_price12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_price12h.Text;
                else if (lblPlan_price22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_price22h.Text;
                else if (lblPlan_price32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_price32h.Text;
                else if (lblccount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtccount2h.Text;
                else if (lblPlan_sale2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan_sale2h.Text;
                else if (lblaccount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblProduct_Plan.xml"));

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
        public void Write()
        {
            //navigation.Visible = false;
            //drplocationid.Enabled = true;
            //txtplanid.Enabled = true;
            txtplanname1.Enabled = true;
            txtplanname2.Enabled = true;
            txtplanname3.Enabled = true;
            //cbactive.Enabled = true;
            txtPlan_cost.Enabled = true;
            txtPlan_price1.Enabled = true;
            txtPlan_price2.Enabled = true;
            txtPlan_price3.Enabled = true;
            txtccount.Enabled = true;
            txtPlan_sale.Enabled = true;
            txtaccount.Enabled = true;
            // drpcrupid.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drplocationid.Enabled = false;
            //txtplanid.Enabled = false;
            txtplanname1.Enabled = false;
            txtplanname2.Enabled = false;
            txtplanname3.Enabled = false;
            //cbactive.Enabled = false;
            txtPlan_cost.Enabled = false;
            txtPlan_price1.Enabled = false;
            txtPlan_price2.Enabled = false;
            txtPlan_price3.Enabled = false;
            txtccount.Enabled = false;
            txtPlan_sale.Enabled = false;
            txtaccount.Enabled = false;
            //drpcrupid.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    int TID = (((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tblProduct_Plan.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_Plan.Where(p=>p.TenentID==TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    if (take == Totalrec && Skip == (Totalrec - Showdata))
        //        btnNext1.Enabled = false;
        //    else
        //        btnNext1.Enabled = true;
        //    if (take == Showdata && Skip == 0)
        //        btnPrevious1.Enabled = false;
        //    else
        //        btnPrevious1.Enabled = true;

        //    ChoiceID = take / Showdata;

        //    //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{
        //    int TID = (((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tblProduct_Plan.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_Plan.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        if (take == Showdata && Skip == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;

        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;

        //        ChoiceID = take / Showdata;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{
        //    int TID = (((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tblProduct_Plan.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_Plan.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{
        //    int TID = (((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tblProduct_Plan.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_Plan.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    //((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            Readonly();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            //FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            PnlGriderror.Visible = false;
            lblGridErrormsg.Text = "";

            int TID = (((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == ID).ToList();

                if (ListPlan.Count() < 1)
                {
                    Database.tblProduct_Plan objtblProduct_Plan = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == ID);
                    objtblProduct_Plan.active = false;

                    objtblProduct_Plan.UploadDate = DateTime.Now;
                    objtblProduct_Plan.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                    objtblProduct_Plan.SynID = 3;

                    DB.SaveChanges();

                    Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                    BindData();
                }
                else
                {
                    PnlGriderror.Visible = true;
                    lblGridErrormsg.Text = "This Plan Not to be delete It Used in Plan Meal Setup.";
                }
            }

            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.tblProduct_Plan objtblProduct_Plan = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == ID);
                txtplanid.Text = objtblProduct_Plan.planid.ToString();
                txtplanname1.Text = objtblProduct_Plan.planname1.ToString();
                txtplanname2.Text = objtblProduct_Plan.planname2.ToString();
                txtplanname3.Text = objtblProduct_Plan.planname3.ToString();
                //cbactive.Checked = (objtblProduct_Plan.active == true) ? true : false;
                txtPlan_cost.Text = objtblProduct_Plan.Plan_cost.ToString();
                txtPlan_price1.Text = objtblProduct_Plan.Plan_price1.ToString();
                txtPlan_price2.Text = objtblProduct_Plan.Plan_price2.ToString();
                txtPlan_price3.Text = objtblProduct_Plan.Plan_price3.ToString();
                txtccount.Text = objtblProduct_Plan.ccount.ToString();
                txtPlan_sale.Text = objtblProduct_Plan.Plan_sale.ToString();
                txtaccount.Text = objtblProduct_Plan.account.ToString();

                btnAdd.Text = "Update";
                ViewState["Edit"] = ID;
                Write();
            }
            //        scope.Complete(); //  To commit.
            //    }
            //    catch (Exception ex)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
            //        throw;
            //    }
            //}
        }
        public string Translate(string textvalue, string to)
        {
            string appId = "A70C584051881A30549986E65FF4B92B95B353A5";//go to http://msdn.microsoft.com/en-us/library/ff512386.aspx to obtain AppId.
            // string textvalue = "Translate this for me";
            string from = "en";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + textvalue + "&from=" + from + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }

        protected void txtplanname1_TextChanged(object sender, EventArgs e)
        {
            txtplanname2.Text = Translate(txtplanname1.Text, "ar");
            txtplanname3.Text = Translate(txtplanname1.Text, "fr");
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    int TID = (((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tblProduct_Plan.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_Plan.Where(p=>p.TenentID==TID).OrderBy(m => m.planid).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //       // ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (Tvalue == Showdata && Svalue == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //    }
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        //}
        //protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        //protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
        //    ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
        //    control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        //}
        #endregion

    }
}