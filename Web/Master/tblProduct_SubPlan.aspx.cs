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

namespace Web.Master
{
    public partial class tblProduct_SubPlan : System.Web.UI.Page
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
            if (!IsPostBack)
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
                btnAdd.ValidationGroup = "s";
            }
        }
        #region Step2
        public void BindData()
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            Listview1.DataSource = DB.tblProduct_SubPlan.Where(p => p.TenentID == TID && p.active == true);
            Listview1.DataBind();
            //List<tblProduct_SubPlan> List = DB.tblProduct_SubPlan.OrderBy(m => m.JobId).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblplanid1s.Attributes["class"] = lblSubPlanId1s.Attributes["class"] = lblSub_planname11s.Attributes["class"] = lblSub_planname21s.Attributes["class"] = lblSub_planname31s.Attributes["class"] = lblSub_Plan_cost1s.Attributes["class"] = lblSub_Plan_price11s.Attributes["class"] = lblSub_Plan_price21s.Attributes["class"] = lblSub_Plan_price31s.Attributes["class"] = lblccount1s.Attributes["class"] = lblSub_Plan_sale1s.Attributes["class"] = lblaccount1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblplanid2h.Attributes["class"] = lblSubPlanId2h.Attributes["class"] = lblSub_planname12h.Attributes["class"] = lblSub_planname22h.Attributes["class"] = lblSub_planname32h.Attributes["class"] = lblSub_Plan_cost2h.Attributes["class"] = lblSub_Plan_price12h.Attributes["class"] = lblSub_Plan_price22h.Attributes["class"] = lblSub_Plan_price32h.Attributes["class"] = lblccount2h.Attributes["class"] = lblSub_Plan_sale2h.Attributes["class"] = lblaccount2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblplanid1s.Attributes["class"] = lblSubPlanId1s.Attributes["class"] = lblSub_planname11s.Attributes["class"] = lblSub_planname21s.Attributes["class"] = lblSub_planname31s.Attributes["class"] = lblSub_Plan_cost1s.Attributes["class"] = lblSub_Plan_price11s.Attributes["class"] = lblSub_Plan_price21s.Attributes["class"] = lblSub_Plan_price31s.Attributes["class"] = lblccount1s.Attributes["class"] = lblSub_Plan_sale1s.Attributes["class"] = lblaccount1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblplanid2h.Attributes["class"] = lblSubPlanId2h.Attributes["class"] = lblSub_planname12h.Attributes["class"] = lblSub_planname22h.Attributes["class"] = lblSub_planname32h.Attributes["class"] = lblSub_Plan_cost2h.Attributes["class"] = lblSub_Plan_price12h.Attributes["class"] = lblSub_Plan_price22h.Attributes["class"] = lblSub_Plan_price32h.Attributes["class"] = lblccount2h.Attributes["class"] = lblSub_Plan_sale2h.Attributes["class"] = lblaccount2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            drpplanid.SelectedIndex = 0;
            txtSubPlanId.Text = "";
            txtSub_planname1.Text = "";
            txtSub_planname2.Text = "";
            txtSub_planname3.Text = "";
            //txtactive.Text = "";
            txtSub_Plan_cost.Text = "";
            txtSub_Plan_price1.Text = "";
            txtSub_Plan_price2.Text = "";
            txtSub_Plan_price3.Text = "";
            txtccount.Text = "";
            txtSub_Plan_sale.Text = "";
            txtaccount.Text = "";
            txtName.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.tblProduct_SubPlan objtblProduct_SubPlan = new Database.tblProduct_SubPlan();
                        //Server Content Send data Yogesh
                        objtblProduct_SubPlan.TenentID = TID;
                        objtblProduct_SubPlan.locationid = 1;
                        //objtblProduct_SubPlan.locationid = Convert.ToInt32(drplocationid.SelectedValue);
                        objtblProduct_SubPlan.planid = Convert.ToInt32(drpplanid.SelectedValue);
                        objtblProduct_SubPlan.SubPlanId = Convert.ToInt32(txtSubPlanId.Text);
                        objtblProduct_SubPlan.Sub_planname1 = txtSub_planname1.Text;
                        objtblProduct_SubPlan.Sub_planname2 = txtSub_planname2.Text;
                        objtblProduct_SubPlan.Sub_planname3 = txtSub_planname3.Text;
                        //objtblProduct_SubPlan.active = cbactive.Checked;
                        objtblProduct_SubPlan.Sub_Plan_cost = Convert.ToDecimal(txtSub_Plan_cost.Text);
                        objtblProduct_SubPlan.Sub_Plan_price1 = Convert.ToInt32(txtSub_Plan_price1.Text);
                        objtblProduct_SubPlan.Sub_Plan_price2 = Convert.ToInt32(txtSub_Plan_price2.Text);
                        objtblProduct_SubPlan.Sub_Plan_price3 = Convert.ToInt32(txtSub_Plan_price3.Text);
                        objtblProduct_SubPlan.ccount = Convert.ToInt32(txtccount.Text);
                        objtblProduct_SubPlan.Sub_Plan_sale = txtSub_Plan_sale.Text;
                        objtblProduct_SubPlan.account = Convert.ToInt32(txtaccount.Text);
                        objtblProduct_SubPlan.active = true;


                        DB.tblProduct_SubPlan.AddObject(objtblProduct_SubPlan);
                        DB.SaveChanges();
                        Clear();
                        btnAdd.Text = "AddNew";
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                        btnAdd.ValidationGroup = "s";
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.tblProduct_SubPlan objtblProduct_SubPlan = DB.tblProduct_SubPlan.Single(p => p.SubPlanId == ID && p.TenentID==TID);
                            //objtblProduct_SubPlan.locationid = Convert.ToInt32(drplocationid.SelectedValue);
                            objtblProduct_SubPlan.planid = Convert.ToInt32(drpplanid.SelectedValue);
                            objtblProduct_SubPlan.SubPlanId = Convert.ToInt32(txtSubPlanId.Text);
                            objtblProduct_SubPlan.Sub_planname1 = txtSub_planname1.Text;
                            objtblProduct_SubPlan.Sub_planname2 = txtSub_planname2.Text;
                            objtblProduct_SubPlan.Sub_planname3 = txtSub_planname3.Text;
                            //objtblProduct_SubPlan.active = cbactive.Checked;
                            objtblProduct_SubPlan.Sub_Plan_cost = Convert.ToDecimal(txtSub_Plan_cost.Text);
                            objtblProduct_SubPlan.Sub_Plan_price1 = Convert.ToInt32(txtSub_Plan_price1.Text);
                            objtblProduct_SubPlan.Sub_Plan_price2 = Convert.ToInt32(txtSub_Plan_price2.Text);
                            objtblProduct_SubPlan.Sub_Plan_price3 = Convert.ToInt32(txtSub_Plan_price3.Text);
                            objtblProduct_SubPlan.ccount = Convert.ToInt32(txtccount.Text);
                            objtblProduct_SubPlan.Sub_Plan_sale = txtSub_Plan_sale.Text;
                            objtblProduct_SubPlan.account = Convert.ToInt32(txtaccount.Text);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                           // FirstData();
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

        //public string GetName(int ID)
        //{
        //    int TID = (((USER_MST)Session["USER"]).TenentID);
        //    return DB.tblProduct_Plan.Single(p => p.planid == ID && p.TenentID == TID).planname1;
        //}
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            drpplanid.DataSource = DB.tblProduct_Plan.Where(p=>p.TenentID==TID);
            drpplanid.DataTextField = "planname1";
            drpplanid.DataValueField = "planid";
            drpplanid.DataBind();
            drpplanid.Items.Insert(0, new ListItem("-- Select --", "0"));
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
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpplanid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtSubPlanId.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_planname1.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_planname2.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_planname3.Text = Listview1.SelectedDataKey[0].ToString();
            //cbactive.Checked = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_cost.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_price1.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_price2.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_price3.Text = Listview1.SelectedDataKey[0].ToString();
            txtccount.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_sale.Text = Listview1.SelectedDataKey[0].ToString();
            txtaccount.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpplanid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtSubPlanId.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_planname1.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_planname2.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_planname3.Text = Listview1.SelectedDataKey[0].ToString();
                //cbactive.Checked = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_cost.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_price1.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_price2.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_price3.Text = Listview1.SelectedDataKey[0].ToString();
                txtccount.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_sale.Text = Listview1.SelectedDataKey[0].ToString();
                txtaccount.Text = Listview1.SelectedDataKey[0].ToString();

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
                //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpplanid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtSubPlanId.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_planname1.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_planname2.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_planname3.Text = Listview1.SelectedDataKey[0].ToString();
                //cbactive.Checked = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_cost.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_price1.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_price2.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_price3.Text = Listview1.SelectedDataKey[0].ToString();
                txtccount.Text = Listview1.SelectedDataKey[0].ToString();
                txtSub_Plan_sale.Text = Listview1.SelectedDataKey[0].ToString();
                txtaccount.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drplocationid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpplanid.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtSubPlanId.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_planname1.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_planname2.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_planname3.Text = Listview1.SelectedDataKey[0].ToString();
            //cbactive.Checked = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_cost.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_price1.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_price2.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_price3.Text = Listview1.SelectedDataKey[0].ToString();
            txtccount.Text = Listview1.SelectedDataKey[0].ToString();
            txtSub_Plan_sale.Text = Listview1.SelectedDataKey[0].ToString();
            txtaccount.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblplanid2h.Visible = lblSubPlanId2h.Visible = lblSub_planname12h.Visible = lblSub_planname22h.Visible = lblSub_planname32h.Visible = lblSub_Plan_cost2h.Visible = lblSub_Plan_price12h.Visible = lblSub_Plan_price22h.Visible = lblSub_Plan_price32h.Visible = lblccount2h.Visible = lblSub_Plan_sale2h.Visible = lblaccount2h.Visible = false;
                    //2true
                    txtplanid2h.Visible = txtSubPlanId2h.Visible = txtSub_planname12h.Visible = txtSub_planname22h.Visible = txtSub_planname32h.Visible = txtSub_Plan_cost2h.Visible = txtSub_Plan_price12h.Visible = txtSub_Plan_price22h.Visible = txtSub_Plan_price32h.Visible = txtccount2h.Visible = txtSub_Plan_sale2h.Visible = txtaccount2h.Visible = true;

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
                    lblplanid2h.Visible = lblSubPlanId2h.Visible = lblSub_planname12h.Visible = lblSub_planname22h.Visible = lblSub_planname32h.Visible = lblSub_Plan_cost2h.Visible = lblSub_Plan_price12h.Visible = lblSub_Plan_price22h.Visible = lblSub_Plan_price32h.Visible = lblccount2h.Visible = lblSub_Plan_sale2h.Visible = lblaccount2h.Visible = true;
                    //2false
                    txtplanid2h.Visible = txtSubPlanId2h.Visible = txtSub_planname12h.Visible = txtSub_planname22h.Visible = txtSub_planname32h.Visible = txtSub_Plan_cost2h.Visible = txtSub_Plan_price12h.Visible = txtSub_Plan_price22h.Visible = txtSub_Plan_price32h.Visible = txtccount2h.Visible = txtSub_Plan_sale2h.Visible = txtaccount2h.Visible = false;

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
                    lblplanid1s.Visible = lblSubPlanId1s.Visible = lblSub_planname11s.Visible = lblSub_planname21s.Visible = lblSub_planname31s.Visible = lblSub_Plan_cost1s.Visible = lblSub_Plan_price11s.Visible = lblSub_Plan_price21s.Visible = lblSub_Plan_price31s.Visible = lblccount1s.Visible = lblSub_Plan_sale1s.Visible = lblaccount1s.Visible = false;
                    //1true
                    txtplanid1s.Visible = txtSubPlanId1s.Visible = txtSub_planname11s.Visible = txtSub_planname21s.Visible = txtSub_planname31s.Visible = txtSub_Plan_cost1s.Visible = txtSub_Plan_price11s.Visible = txtSub_Plan_price21s.Visible = txtSub_Plan_price31s.Visible = txtccount1s.Visible = txtSub_Plan_sale1s.Visible = txtaccount1s.Visible = true;
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
                    lblplanid1s.Visible = lblSubPlanId1s.Visible = lblSub_planname11s.Visible = lblSub_planname21s.Visible = lblSub_planname31s.Visible = lblSub_Plan_cost1s.Visible = lblSub_Plan_price11s.Visible = lblSub_Plan_price21s.Visible = lblSub_Plan_price31s.Visible = lblccount1s.Visible = lblSub_Plan_sale1s.Visible = lblaccount1s.Visible = true;
                    //1false
                    txtplanid1s.Visible = txtSubPlanId1s.Visible = txtSub_planname11s.Visible = txtSub_planname21s.Visible = txtSub_planname31s.Visible = txtSub_Plan_cost1s.Visible = txtSub_Plan_price11s.Visible = txtSub_Plan_price21s.Visible = txtSub_Plan_price31s.Visible = txtccount1s.Visible = txtSub_Plan_sale1s.Visible = txtaccount1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblProduct_SubPlan").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblplanid1s.ID == item.LabelID)
                    txtplanid1s.Text = lblplanid1s.Text  = item.LabelName;
                else if (lblSubPlanId1s.ID == item.LabelID)
                    txtSubPlanId1s.Text = lblSubPlanId1s.Text  = item.LabelName;
                else if (lblSub_planname11s.ID == item.LabelID)
                    txtSub_planname11s.Text = lblSub_planname11s.Text = item.LabelName;
                else if (lblSub_planname21s.ID == item.LabelID)
                    txtSub_planname21s.Text = lblSub_planname21s.Text  = item.LabelName;
                else if (lblSub_planname31s.ID == item.LabelID)
                    txtSub_planname31s.Text = lblSub_planname31s.Text  = item.LabelName;
                else if (lblSub_Plan_cost1s.ID == item.LabelID)
                    txtSub_Plan_cost1s.Text = lblSub_Plan_cost1s.Text  = item.LabelName;
                else if (lblSub_Plan_price11s.ID == item.LabelID)
                    txtSub_Plan_price11s.Text = lblSub_Plan_price11s.Text = item.LabelName;
                else if (lblSub_Plan_price21s.ID == item.LabelID)
                    txtSub_Plan_price21s.Text = lblSub_Plan_price21s.Text =  item.LabelName;
                else if (lblSub_Plan_price31s.ID == item.LabelID)
                    txtSub_Plan_price31s.Text = lblSub_Plan_price31s.Text = item.LabelName;
                else if (lblccount1s.ID == item.LabelID)
                    txtccount1s.Text = lblccount1s.Text =  item.LabelName;
                else if (lblSub_Plan_sale1s.ID == item.LabelID)
                    txtSub_Plan_sale1s.Text = lblSub_Plan_sale1s.Text =  item.LabelName;
                else if (lblaccount1s.ID == item.LabelID)
                    txtaccount1s.Text = lblaccount1s.Text = item.LabelName;

                else if (lblplanid2h.ID == item.LabelID)
                    txtplanid2h.Text = lblplanid2h.Text =  item.LabelName;
                else if (lblSubPlanId2h.ID == item.LabelID)
                    txtSubPlanId2h.Text = lblSubPlanId2h.Text =  item.LabelName;
                else if (lblSub_planname12h.ID == item.LabelID)
                    txtSub_planname12h.Text = lblSub_planname12h.Text =  item.LabelName;
                else if (lblSub_planname22h.ID == item.LabelID)
                    txtSub_planname22h.Text = lblSub_planname22h.Text = item.LabelName;
                else if (lblSub_planname32h.ID == item.LabelID)
                    txtSub_planname32h.Text = lblSub_planname32h.Text =  item.LabelName;
                else if (lblSub_Plan_cost2h.ID == item.LabelID)
                    txtSub_Plan_cost2h.Text = lblSub_Plan_cost2h.Text = item.LabelName;
                else if (lblSub_Plan_price12h.ID == item.LabelID)
                    txtSub_Plan_price12h.Text = lblSub_Plan_price12h.Text = item.LabelName;
                else if (lblSub_Plan_price22h.ID == item.LabelID)
                    txtSub_Plan_price22h.Text = lblSub_Plan_price22h.Text = item.LabelName;
                else if (lblSub_Plan_price32h.ID == item.LabelID)
                    txtSub_Plan_price32h.Text = lblSub_Plan_price32h.Text = item.LabelName;
                else if (lblccount2h.ID == item.LabelID)
                    txtccount2h.Text = lblccount2h.Text = item.LabelName;
                else if (lblSub_Plan_sale2h.ID == item.LabelID)
                    txtSub_Plan_sale2h.Text = lblSub_Plan_sale2h.Text = item.LabelName;
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
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblProduct_SubPlan").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblProduct_SubPlan.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblProduct_SubPlan").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblplanid1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanid1s.Text;
                else if (lblSubPlanId1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSubPlanId1s.Text;
                else if (lblSub_planname11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_planname11s.Text;
                else if (lblSub_planname21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_planname21s.Text;
                else if (lblSub_planname31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_planname31s.Text;
                else if (lblSub_Plan_cost1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_cost1s.Text;
                else if (lblSub_Plan_price11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_price11s.Text;
                else if (lblSub_Plan_price21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_price21s.Text;
                else if (lblSub_Plan_price31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_price31s.Text;
                else if (lblccount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtccount1s.Text;
                else if (lblSub_Plan_sale1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_sale1s.Text;
                else if (lblaccount1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount1s.Text;

                 else if (lblplanid2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtplanid2h.Text;
                else if (lblSubPlanId2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSubPlanId2h.Text;
                else if (lblSub_planname12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_planname12h.Text;
                else if (lblSub_planname22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_planname22h.Text;
                else if (lblSub_planname32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_planname32h.Text;
                else if (lblSub_Plan_cost2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_cost2h.Text;
                else if (lblSub_Plan_price12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_price12h.Text;
                else if (lblSub_Plan_price22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_price22h.Text;
                else if (lblSub_Plan_price32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_price32h.Text;
                else if (lblccount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtccount2h.Text;
                else if (lblSub_Plan_sale2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSub_Plan_sale2h.Text;
                else if (lblaccount2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtaccount2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblProduct_SubPlan.xml"));

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
            drpplanid.Enabled = true;
            txtSubPlanId.Enabled = true;
            txtSub_planname1.Enabled = true;
            txtSub_planname2.Enabled = true;
            txtSub_planname3.Enabled = true;
            //cbactive.Enabled = true;
            txtSub_Plan_cost.Enabled = true;
            txtSub_Plan_price1.Enabled = true;
            txtSub_Plan_price2.Enabled = true;
            txtSub_Plan_price3.Enabled = true;
            txtccount.Enabled = true;
            txtSub_Plan_sale.Enabled = true;
            txtaccount.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drplocationid.Enabled = false;
            drpplanid.Enabled = false;
            txtSubPlanId.Enabled = false;
            txtSub_planname1.Enabled = false;
            txtSub_planname2.Enabled = false;
            txtSub_planname3.Enabled = false;
            //cbactive.Enabled = false;
            txtSub_Plan_cost.Enabled = false;
            txtSub_Plan_price1.Enabled = false;
            txtSub_Plan_price2.Enabled = false;
            txtSub_Plan_price3.Enabled = false;
            txtccount.Enabled = false;
            txtSub_Plan_sale.Enabled = false;
            txtaccount.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblProduct_SubPlan.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_SubPlan.Where(p=>p.TenentID==TID).OrderBy(m => m.SubPlanId).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            if (take == Totalrec && Skip == (Totalrec - Showdata))
                btnNext1.Enabled = false;
            else
                btnNext1.Enabled = true;
            if (take == Showdata && Skip == 0)
                btnPrevious1.Enabled = false;
            else
                btnPrevious1.Enabled = true;

            ChoiceID = take / Showdata;

            //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblProduct_SubPlan.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_SubPlan.Where(p=>p.TenentID==TID).OrderBy(m => m.SubPlanId).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                if (take == Showdata && Skip == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;

                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;

                ChoiceID = take / Showdata;
                //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tblProduct_SubPlan.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_SubPlan.Where(p=>p.TenentID==TID).OrderBy(m => m.SubPlanId).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblProduct_SubPlan.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_SubPlan.Where(p=>p.TenentID==TID).OrderBy(m => m.SubPlanId).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            //((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }
        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
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
            FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
                    if (e.CommandName == "btnDelete")
                    {

                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.tblProduct_SubPlan objSOJobDesc = DB.tblProduct_SubPlan.Single(p => p.SubPlanId == ID && p.TenentID == TID);
                        objSOJobDesc.active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_SubPlan.OrderBy(m => m.SubPlanId).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.tblProduct_SubPlan objtblProduct_SubPlan = DB.tblProduct_SubPlan.Single(p => p.SubPlanId == ID && p.TenentID == TID);
                        //drplocationid.SelectedValue = objtblProduct_SubPlan.locationid.ToString();
                        drpplanid.SelectedValue = objtblProduct_SubPlan.planid.ToString();
                        txtSubPlanId.Text = objtblProduct_SubPlan.SubPlanId.ToString();
                        txtSub_planname1.Text = objtblProduct_SubPlan.Sub_planname1.ToString();
                        txtSub_planname2.Text = objtblProduct_SubPlan.Sub_planname2.ToString();
                        txtSub_planname3.Text = objtblProduct_SubPlan.Sub_planname3.ToString();
                        //cbactive.Checked = (objtblProduct_SubPlan.active == true) ? true : false;
                        txtSub_Plan_cost.Text = objtblProduct_SubPlan.Sub_Plan_cost.ToString();
                        txtSub_Plan_price1.Text = objtblProduct_SubPlan.Sub_Plan_price1.ToString();
                        txtSub_Plan_price2.Text = objtblProduct_SubPlan.Sub_Plan_price2.ToString();
                        txtSub_Plan_price3.Text = objtblProduct_SubPlan.Sub_Plan_price3.ToString();
                        txtccount.Text = objtblProduct_SubPlan.ccount.ToString();
                        txtSub_Plan_sale.Text = objtblProduct_SubPlan.Sub_Plan_sale.ToString();
                        txtaccount.Text = objtblProduct_SubPlan.account.ToString();

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

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = (((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblProduct_SubPlan.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblProduct_SubPlan.Where(p=>p.TenentID==TID).OrderBy(m => m.SubPlanId).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                if (Tvalue == Showdata && Svalue == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
            }
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        }

        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        #endregion

    }
}