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
    public partial class tblSubscriptionSetup : System.Web.UI.Page
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
                Clear();
                btnAdd.ValidationGroup = "ss";
                //FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.tblSubscriptionSetup> List = DB.tblSubscriptionSetups.Where(m => m.TenentID == TID).OrderBy(m => m.LocalID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lbldays_in_week1s.Attributes["class"] = lblWhitch_day_delivery1s.Attributes["class"] = lblWeek_Start_With_Day1s.Attributes["class"] = lblDelivery_in_day1s.Attributes["class"] = lblDelivery_time_begin1s.Attributes["class"] = lblChanges_Allowed1s.Attributes["class"] = lblBefore_how_many_Hours1s.Attributes["class"] = lblRefund_Allowed1s.Attributes["class"] = lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblKitchenRequestingStore1s.Attributes["class"] = lblMainStore1s.Attributes["class"] = lblIncomingKitchenAutoAccept1s.Attributes["class"] = "control-label col-md-4  getshow";
           lbldays_in_week2h.Attributes["class"] = lblWhitch_day_delivery2h.Attributes["class"] = lblWeek_Start_With_Day2h.Attributes["class"] = lblDelivery_in_day2h.Attributes["class"] = lblDelivery_time_begin2h.Attributes["class"] = lblChanges_Allowed2h.Attributes["class"] = lblBefore_how_many_Hours2h.Attributes["class"] = lblRefund_Allowed2h.Attributes["class"] = lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblKitchenRequestingStore2h.Attributes["class"] = lblMainStore2h.Attributes["class"] = lblIncomingKitchenAutoAccept2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
          lbldays_in_week1s.Attributes["class"] = lblWhitch_day_delivery1s.Attributes["class"] = lblWeek_Start_With_Day1s.Attributes["class"] = lblDelivery_in_day1s.Attributes["class"] = lblDelivery_time_begin1s.Attributes["class"] = lblChanges_Allowed1s.Attributes["class"] = lblBefore_how_many_Hours1s.Attributes["class"] = lblRefund_Allowed1s.Attributes["class"] = lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.Attributes["class"] = lblActive1s.Attributes["class"] = lblKitchenRequestingStore1s.Attributes["class"] = lblMainStore1s.Attributes["class"] = lblIncomingKitchenAutoAccept1s.Attributes["class"] = "control-label col-md-4  gethide";
          lbldays_in_week2h.Attributes["class"] = lblWhitch_day_delivery2h.Attributes["class"] = lblWeek_Start_With_Day2h.Attributes["class"] = lblDelivery_in_day2h.Attributes["class"] = lblDelivery_time_begin2h.Attributes["class"] = lblChanges_Allowed2h.Attributes["class"] = lblBefore_how_many_Hours2h.Attributes["class"] = lblRefund_Allowed2h.Attributes["class"] = lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.Attributes["class"] = lblActive2h.Attributes["class"] = lblKitchenRequestingStore2h.Attributes["class"] = lblMainStore2h.Attributes["class"] = lblIncomingKitchenAutoAccept2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            //drplocationID.SelectedIndex = 0;
            // drpLocalID.SelectedIndex = 0;
            drpdays_in_week.SelectedIndex = 0;
            txtWhitch_day_delivery.Text = "";
            drpWeek_Start_With_Day.SelectedIndex = 0;
            drpDelivery_in_day.SelectedIndex = 0;
            txtDelivery_time_begin.Text = "";
            cbChanges_Allowed.Checked = false;
            txtBefore_how_many_Hours.Text = "";
            cbRefund_Allowed.Checked = true;
            txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text = "";
            //drpCreated.SelectedIndex = 0;
            //txtCreatedDate.Text = "";
            cbActive.Checked = true;
            //txtDeleted.Text = "";
          //  drpKitchenRequestingStore.SelectedIndex = 0;
           // drpMainStore.SelectedIndex = 0;
            cbIncomingKitchenAutoAccept.Checked = true;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                //if (btnAdd.Text == "AddNew")
                //{

                //    Write();
                //    //Clear();
                //    btnAdd.Text = "Add";
                //    btnAdd.ValidationGroup = "submit";
                //}
                //else if (btnAdd.Text == "Add")
                //{
                //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //    Database.tblSubscriptionSetup objtblSubscriptionSetup = new Database.tblSubscriptionSetup();
                //    int MAXID = DB.tblSubscriptionSetups.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tblSubscriptionSetups.Where(p => p.TenentID == TID).Max(p => p.LocalID) + 1) : 1;
                //    //Server Content Send data Yogesh
                //    objtblSubscriptionSetup.TenentID = TID;
                //    //objtblSubscriptionSetup.locationID = Convert.ToInt32(drplocationID.SelectedValue);
                //    objtblSubscriptionSetup.LocalID = MAXID;
                //    objtblSubscriptionSetup.days_in_week = Convert.ToInt32(drpdays_in_week.SelectedValue);
                //    objtblSubscriptionSetup.Whitch_day_delivery = txtWhitch_day_delivery.Text;
                //    objtblSubscriptionSetup.Week_Start_With_Day = drpWeek_Start_With_Day.SelectedValue;
                //    objtblSubscriptionSetup.Delivery_in_day = Convert.ToInt32(drpDelivery_in_day.SelectedValue);
                //    objtblSubscriptionSetup.Delivery_time_begin = txtDelivery_time_begin.Text;
                //    objtblSubscriptionSetup.Changes_Allowed = cbChanges_Allowed.Checked;
                //    objtblSubscriptionSetup.Before_how_many_Hours = txtBefore_how_many_Hours.Text;
                //    objtblSubscriptionSetup.Refund_Allowed = cbRefund_Allowed.Checked;
                //    //  objtblSubscriptionSetup.After_Completion_of_how_many_Percentage_of_Delivery = Convert.ToInt32(txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text);
                //    //objtblSubscriptionSetup.Created = Convert.ToInt32(drpCreated.SelectedValue);
                //    //objtblSubscriptionSetup.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
                //    objtblSubscriptionSetup.Active = cbActive.Checked;
                //    //objtblSubscriptionSetup.Deleted = cbDeleted.Checked;
                //    objtblSubscriptionSetup.KitchenRequestingStore = drpKitchenRequestingStore.SelectedValue;
                //    objtblSubscriptionSetup.MainStore = drpMainStore.SelectedValue;
                //    objtblSubscriptionSetup.IncomingKitchenAutoAccept = cbIncomingKitchenAutoAccept.Checked;


                //    DB.tblSubscriptionSetups.AddObject(objtblSubscriptionSetup);
                //    DB.SaveChanges();
                //   Clear();
                //   btnAdd.Text = "AddNew";
                //   btnAdd.ValidationGroup = "ss";
                //    lblMsg.Text = "  Data Save Successfully";
                //    pnlSuccessMsg.Visible = true;
                //    BindData();
                //    //navigation.Visible = true;
                //    Readonly();
                //    //FirstData();
                //}
                if (btnAdd.Text == "Update Setting")
                {

                    if (ViewState["Edit"] != null)
                    {
                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        Database.tblSubscriptionSetup objtblSubscriptionSetup = DB.tblSubscriptionSetups.Single(p => p.LocalID == ID && p.TenentID == TID);
                        //objtblSubscriptionSetup.locationID = Convert.ToInt32(drplocationID.SelectedValue);
                        objtblSubscriptionSetup.TenentID = TID;
                        // objtblSubscriptionSetup.LocalID = MAXID;
                        objtblSubscriptionSetup.days_in_week = Convert.ToInt32(drpdays_in_week.SelectedValue);
                        objtblSubscriptionSetup.Whitch_day_delivery = txtWhitch_day_delivery.Text;
                        objtblSubscriptionSetup.Week_Start_With_Day = drpKitchenRequestingStore.SelectedValue;
                        objtblSubscriptionSetup.Delivery_in_day = Convert.ToInt32(drpDelivery_in_day.SelectedValue);
                        objtblSubscriptionSetup.Delivery_time_begin = txtDelivery_time_begin.Text;
                        objtblSubscriptionSetup.Changes_Allowed = cbChanges_Allowed.Checked;
                        objtblSubscriptionSetup.Before_how_many_Hours = txtBefore_how_many_Hours.Text;
                        objtblSubscriptionSetup.Refund_Allowed = cbRefund_Allowed.Checked;
                        //  objtblSubscriptionSetup.After_Completion_of_how_many_Percentage_of_Delivery = Convert.ToDecimal(txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text);
                        //objtblSubscriptionSetup.Created = Convert.ToInt32(drpCreated.SelectedValue);
                        //objtblSubscriptionSetup.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
                        objtblSubscriptionSetup.Active = cbActive.Checked;
                        //objtblSubscriptionSetup.Deleted = cbDeleted.Checked;
                        objtblSubscriptionSetup.KitchenRequestingStore = drpKitchenRequestingStore.SelectedValue;
                        objtblSubscriptionSetup.MainStore = drpMainStore.SelectedValue;
                        objtblSubscriptionSetup.IncomingKitchenAutoAccept = cbIncomingKitchenAutoAccept.Checked;

                        ViewState["Edit"] = null;
                       // btnAdd.Text = "Add New";
                        DB.SaveChanges();
                        btnAdd.ValidationGroup = "ss"; 
                        Clear();
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        // navigation.Visible = true;
                        Readonly();
                        //FirstData();
                      //  btnAdd.Visible = false;
                    }
                }
                BindData();

                scope.Complete(); //  To commit.


            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpIncomingKitchenAutoAccept.Items.Insert(0, new ListItem("-- Select --", "0"));drpIncomingKitchenAutoAccept.DataSource = DB.0;drpIncomingKitchenAutoAccept.DataTextField = "0";drpIncomingKitchenAutoAccept.DataValueField = "0";drpIncomingKitchenAutoAccept.DataBind();
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
            //drplocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            // drpLocalID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            drpdays_in_week.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtWhitch_day_delivery.Text = Listview1.SelectedDataKey[0].ToString();
            // txtWeek_Start_With_Day.Text = Listview1.SelectedDataKey[0].ToString();
            drpDelivery_in_day.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtDelivery_time_begin.Text = Listview1.SelectedDataKey[0].ToString();
            // cbChanges_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
            txtBefore_how_many_Hours.Text = Listview1.SelectedDataKey[0].ToString();
            // cbRefund_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
            txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreated.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtKitchenRequestingStore.Text = Listview1.SelectedDataKey[0].ToString();
            //txtMainStore.Text = Listview1.SelectedDataKey[0].ToString();
            //cbIncomingKitchenAutoAccept.Checked = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drplocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //  drpLocalID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpdays_in_week.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtWhitch_day_delivery.Text = Listview1.SelectedDataKey[0].ToString();
                drpWeek_Start_With_Day.Text = Listview1.SelectedDataKey[0].ToString();
                drpDelivery_in_day.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtDelivery_time_begin.Text = Listview1.SelectedDataKey[0].ToString();
                //cbChanges_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
                txtBefore_how_many_Hours.Text = Listview1.SelectedDataKey[0].ToString();
                //cbRefund_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
                txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreated.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                drpKitchenRequestingStore.Text = Listview1.SelectedDataKey[0].ToString();
                drpMainStore.Text = Listview1.SelectedDataKey[0].ToString();
                //cbIncomingKitchenAutoAccept.Checked = Listview1.SelectedDataKey[0].ToString();

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
                //drplocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //  drpLocalID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                drpdays_in_week.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtWhitch_day_delivery.Text = Listview1.SelectedDataKey[0].ToString();
                drpWeek_Start_With_Day.Text = Listview1.SelectedDataKey[0].ToString();
                drpDelivery_in_day.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtDelivery_time_begin.Text = Listview1.SelectedDataKey[0].ToString();
                //cbChanges_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
                txtBefore_how_many_Hours.Text = Listview1.SelectedDataKey[0].ToString();
                //cbRefund_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
                txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreated.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                drpKitchenRequestingStore.Text = Listview1.SelectedDataKey[0].ToString();
                drpMainStore.Text = Listview1.SelectedDataKey[0].ToString();
                //cbIncomingKitchenAutoAccept.Checked = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drplocationID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpLocalID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //drpdays_in_week.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtWhitch_day_delivery.Text = Listview1.SelectedDataKey[0].ToString();
            //txtWeek_Start_With_Day.Text = Listview1.SelectedDataKey[0].ToString();
            //drpDelivery_in_day.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtDelivery_time_begin.Text = Listview1.SelectedDataKey[0].ToString();
            //cbChanges_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtBefore_how_many_Hours.Text = Listview1.SelectedDataKey[0].ToString();
            //cbRefund_Allowed.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreated.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtKitchenRequestingStore.Text = Listview1.SelectedDataKey[0].ToString();
            //txtMainStore.Text = Listview1.SelectedDataKey[0].ToString();
            //cbIncomingKitchenAutoAccept.Checked = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                   lbldays_in_week2h.Visible = lblWhitch_day_delivery2h.Visible = lblWeek_Start_With_Day2h.Visible = lblDelivery_in_day2h.Visible = lblDelivery_time_begin2h.Visible = lblChanges_Allowed2h.Visible = lblBefore_how_many_Hours2h.Visible = lblRefund_Allowed2h.Visible = lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.Visible = lblActive2h.Visible = lblKitchenRequestingStore2h.Visible = lblMainStore2h.Visible = lblIncomingKitchenAutoAccept2h.Visible = false;
                    //2true
                    txtdays_in_week2h.Visible = txtWhitch_day_delivery2h.Visible = txtWeek_Start_With_Day2h.Visible = txtDelivery_in_day2h.Visible = txtDelivery_time_begin2h.Visible = txtChanges_Allowed2h.Visible = txtBefore_how_many_Hours2h.Visible = txtRefund_Allowed2h.Visible = txtAfter_Completion_of_how_many_Percentage_of_Delivery2h.Visible = txtActive2h.Visible = txtKitchenRequestingStore2h.Visible = txtMainStore2h.Visible = txtIncomingKitchenAutoAccept2h.Visible = true;

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
                    lbldays_in_week2h.Visible = lblWhitch_day_delivery2h.Visible = lblWeek_Start_With_Day2h.Visible = lblDelivery_in_day2h.Visible = lblDelivery_time_begin2h.Visible = lblChanges_Allowed2h.Visible = lblBefore_how_many_Hours2h.Visible = lblRefund_Allowed2h.Visible = lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.Visible = lblActive2h.Visible = lblKitchenRequestingStore2h.Visible = lblMainStore2h.Visible = lblIncomingKitchenAutoAccept2h.Visible = true;
                    //2false
                   txtdays_in_week2h.Visible = txtWhitch_day_delivery2h.Visible = txtWeek_Start_With_Day2h.Visible = txtDelivery_in_day2h.Visible = txtDelivery_time_begin2h.Visible = txtChanges_Allowed2h.Visible = txtBefore_how_many_Hours2h.Visible = txtRefund_Allowed2h.Visible = txtAfter_Completion_of_how_many_Percentage_of_Delivery2h.Visible = txtActive2h.Visible = txtKitchenRequestingStore2h.Visible = txtMainStore2h.Visible = txtIncomingKitchenAutoAccept2h.Visible = false;

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
                    lbldays_in_week1s.Visible = lblWhitch_day_delivery1s.Visible = lblWeek_Start_With_Day1s.Visible = lblDelivery_in_day1s.Visible = lblDelivery_time_begin1s.Visible = lblChanges_Allowed1s.Visible = lblBefore_how_many_Hours1s.Visible = lblRefund_Allowed1s.Visible = lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.Visible = lblActive1s.Visible = lblKitchenRequestingStore1s.Visible = lblMainStore1s.Visible = lblIncomingKitchenAutoAccept1s.Visible = false;
                    //1true
                    txtdays_in_week1s.Visible = txtWhitch_day_delivery1s.Visible = txtWeek_Start_With_Day1s.Visible = txtDelivery_in_day1s.Visible = txtDelivery_time_begin1s.Visible = txtChanges_Allowed1s.Visible = txtBefore_how_many_Hours1s.Visible = txtRefund_Allowed1s.Visible = txtAfter_Completion_of_how_many_Percentage_of_Delivery1s.Visible = txtActive1s.Visible = txtKitchenRequestingStore1s.Visible = txtMainStore1s.Visible = txtIncomingKitchenAutoAccept1s.Visible = true;
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
                    lbldays_in_week1s.Visible = lblWhitch_day_delivery1s.Visible = lblWeek_Start_With_Day1s.Visible = lblDelivery_in_day1s.Visible = lblDelivery_time_begin1s.Visible = lblChanges_Allowed1s.Visible = lblBefore_how_many_Hours1s.Visible = lblRefund_Allowed1s.Visible = lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.Visible = lblActive1s.Visible = lblKitchenRequestingStore1s.Visible = lblMainStore1s.Visible = lblIncomingKitchenAutoAccept1s.Visible = true;
                    //1false
                   txtdays_in_week1s.Visible = txtWhitch_day_delivery1s.Visible = txtWeek_Start_With_Day1s.Visible = txtDelivery_in_day1s.Visible = txtDelivery_time_begin1s.Visible = txtChanges_Allowed1s.Visible = txtBefore_how_many_Hours1s.Visible = txtRefund_Allowed1s.Visible = txtAfter_Completion_of_how_many_Percentage_of_Delivery1s.Visible = txtActive1s.Visible = txtKitchenRequestingStore1s.Visible = txtMainStore1s.Visible = txtIncomingKitchenAutoAccept1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblSubscriptionSetup").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lbllocationID1s.ID == item.LabelID)
                //    txtlocationID1s.Text = lbllocationID1s.Text = lbllocationID1s.Text = item.LabelName;
                 if (lbldays_in_week1s.ID == item.LabelID)
                    txtdays_in_week1s.Text = lbldays_in_week1s.Text = lbldays_in_week1s.Text = item.LabelName;
                else if (lblWhitch_day_delivery1s.ID == item.LabelID)
                    txtWhitch_day_delivery1s.Text = lblWhitch_day_delivery1s.Text = lblWhitch_day_delivery1s.Text = item.LabelName;
                else if (lblWeek_Start_With_Day1s.ID == item.LabelID)
                    txtWeek_Start_With_Day1s.Text = lblWeek_Start_With_Day1s.Text = lblWeek_Start_With_Day1s.Text = item.LabelName;
                else if (lblDelivery_in_day1s.ID == item.LabelID)
                    txtDelivery_in_day1s.Text = lblDelivery_in_day1s.Text = lblDelivery_in_day1s.Text = item.LabelName;
                else if (lblDelivery_time_begin1s.ID == item.LabelID)
                    txtDelivery_time_begin1s.Text = lblDelivery_time_begin1s.Text = lblDelivery_time_begin1s.Text = item.LabelName;
                else if (lblChanges_Allowed1s.ID == item.LabelID)
                    txtChanges_Allowed1s.Text = lblChanges_Allowed1s.Text = lblChanges_Allowed1s.Text = item.LabelName;
                else if (lblBefore_how_many_Hours1s.ID == item.LabelID)
                    txtBefore_how_many_Hours1s.Text = lblBefore_how_many_Hours1s.Text = lblBefore_how_many_Hours1s.Text = item.LabelName;
                else if (lblRefund_Allowed1s.ID == item.LabelID)
                    txtRefund_Allowed1s.Text = lblRefund_Allowed1s.Text = lblRefund_Allowed1s.Text = item.LabelName;
                else if (lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.ID == item.LabelID)
                    txtAfter_Completion_of_how_many_Percentage_of_Delivery1s.Text = lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.Text = lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblKitchenRequestingStore1s.ID == item.LabelID)
                    txtKitchenRequestingStore1s.Text = lblKitchenRequestingStore1s.Text = lblKitchenRequestingStore1s.Text = item.LabelName;
                else if (lblMainStore1s.ID == item.LabelID)
                    txtMainStore1s.Text = lblMainStore1s.Text = lblMainStore1s.Text = item.LabelName;
                else if (lblIncomingKitchenAutoAccept1s.ID == item.LabelID)
                    txtIncomingKitchenAutoAccept1s.Text = lblIncomingKitchenAutoAccept1s.Text = lblIncomingKitchenAutoAccept1s.Text = item.LabelName;

                //else if (lbllocationID2h.ID == item.LabelID)
                //    txtlocationID2h.Text = lbllocationID2h.Text = lbllocationID2h.Text = item.LabelName;
                else if (lbldays_in_week2h.ID == item.LabelID)
                    txtdays_in_week2h.Text = lbldays_in_week2h.Text = lbldays_in_week2h.Text = item.LabelName;
                else if (lblWhitch_day_delivery2h.ID == item.LabelID)
                    txtWhitch_day_delivery2h.Text = lblWhitch_day_delivery2h.Text = lblWhitch_day_delivery2h.Text = item.LabelName;
                else if (lblWeek_Start_With_Day2h.ID == item.LabelID)
                    txtWeek_Start_With_Day2h.Text = lblWeek_Start_With_Day2h.Text = lblWeek_Start_With_Day2h.Text = item.LabelName;
                else if (lblDelivery_in_day2h.ID == item.LabelID)
                    txtDelivery_in_day2h.Text = lblDelivery_in_day2h.Text = lblDelivery_in_day2h.Text = item.LabelName;
                else if (lblDelivery_time_begin2h.ID == item.LabelID)
                    txtDelivery_time_begin2h.Text = lblDelivery_time_begin2h.Text = lblDelivery_time_begin2h.Text = item.LabelName;
                else if (lblChanges_Allowed2h.ID == item.LabelID)
                    txtChanges_Allowed2h.Text = lblChanges_Allowed2h.Text = lblChanges_Allowed2h.Text = item.LabelName;
                else if (lblBefore_how_many_Hours2h.ID == item.LabelID)
                    txtBefore_how_many_Hours2h.Text = lblBefore_how_many_Hours2h.Text = lblBefore_how_many_Hours2h.Text = item.LabelName;
                else if (lblRefund_Allowed2h.ID == item.LabelID)
                    txtRefund_Allowed2h.Text = lblRefund_Allowed2h.Text = lblRefund_Allowed2h.Text = item.LabelName;
                else if (lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.ID == item.LabelID)
                    txtAfter_Completion_of_how_many_Percentage_of_Delivery2h.Text = lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.Text = lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblKitchenRequestingStore2h.ID == item.LabelID)
                    txtKitchenRequestingStore2h.Text = lblKitchenRequestingStore2h.Text = lblKitchenRequestingStore2h.Text = item.LabelName;
                else if (lblMainStore2h.ID == item.LabelID)
                    txtMainStore2h.Text = lblMainStore2h.Text = lblMainStore2h.Text = item.LabelName;
                else if (lblIncomingKitchenAutoAccept2h.ID == item.LabelID)
                    txtIncomingKitchenAutoAccept2h.Text = lblIncomingKitchenAutoAccept2h.Text = lblIncomingKitchenAutoAccept2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tblSubscriptionSetup").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tblSubscriptionSetup.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tblSubscriptionSetup").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lbllocationID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationID1s.Text;
               if (lbldays_in_week1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtdays_in_week1s.Text;
                else if (lblWhitch_day_delivery1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWhitch_day_delivery1s.Text;
                else if (lblWeek_Start_With_Day1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWeek_Start_With_Day1s.Text;
                else if (lblDelivery_in_day1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDelivery_in_day1s.Text;
                else if (lblDelivery_time_begin1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDelivery_time_begin1s.Text;
                else if (lblChanges_Allowed1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtChanges_Allowed1s.Text;
                else if (lblBefore_how_many_Hours1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBefore_how_many_Hours1s.Text;
                else if (lblRefund_Allowed1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRefund_Allowed1s.Text;
                else if (lblAfter_Completion_of_how_many_Percentage_of_Delivery1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAfter_Completion_of_how_many_Percentage_of_Delivery1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                else if (lblKitchenRequestingStore1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKitchenRequestingStore1s.Text;
                else if (lblMainStore1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMainStore1s.Text;
                else if (lblIncomingKitchenAutoAccept1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIncomingKitchenAutoAccept1s.Text;

               //if (lbllocationID2h.ID == item.LabelID)
               //    ds.Tables[0].Rows[i]["LabelName"] = txtlocationID2h.Text;
                else if (lbldays_in_week2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtdays_in_week2h.Text;
                else if (lblWhitch_day_delivery2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWhitch_day_delivery2h.Text;
                else if (lblWeek_Start_With_Day2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtWeek_Start_With_Day2h.Text;
                else if (lblDelivery_in_day2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDelivery_in_day2h.Text;
                else if (lblDelivery_time_begin2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDelivery_time_begin2h.Text;
                else if (lblChanges_Allowed2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtChanges_Allowed2h.Text;
                else if (lblBefore_how_many_Hours2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBefore_how_many_Hours2h.Text;
                else if (lblRefund_Allowed2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRefund_Allowed2h.Text;
                else if (lblAfter_Completion_of_how_many_Percentage_of_Delivery2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtAfter_Completion_of_how_many_Percentage_of_Delivery2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                else if (lblKitchenRequestingStore2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKitchenRequestingStore2h.Text;
                else if (lblMainStore2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMainStore2h.Text;
                else if (lblIncomingKitchenAutoAccept2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtIncomingKitchenAutoAccept2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tblSubscriptionSetup.xml"));

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
            // navigation.Visible = false;
            //drplocationID.Enabled = true;
            // drpLocalID.Enabled = true;
            drpdays_in_week.Enabled = true;
            txtWhitch_day_delivery.Enabled = true;
            drpWeek_Start_With_Day.Enabled = true;
            drpDelivery_in_day.Enabled = true;
            txtDelivery_time_begin.Enabled = true;
            cbChanges_Allowed.Enabled = true;
            txtBefore_how_many_Hours.Enabled = true;
            cbRefund_Allowed.Enabled = true;
            txtAfter_Completion_of_how_many_Percentage_of_Delivery.Enabled = true;
            //drpCreated.Enabled = true;
            //txtCreatedDate.Enabled = true;
            cbActive.Enabled = true;
            //cbDeleted.Enabled = true;
            drpKitchenRequestingStore.Enabled = true;
            drpMainStore.Enabled = true;
            cbIncomingKitchenAutoAccept.Enabled = true;

        }
        public void Readonly()
        {
            //  navigation.Visible = true;
            //drplocationID.Enabled = false;
            // drpLocalID.Enabled = false;
            drpdays_in_week.Enabled = false;
            txtWhitch_day_delivery.Enabled = false;
            drpWeek_Start_With_Day.Enabled = false;
            drpDelivery_in_day.Enabled = false;
            txtDelivery_time_begin.Enabled = false;
            cbChanges_Allowed.Enabled = false;
            txtBefore_how_many_Hours.Enabled = false;
            cbRefund_Allowed.Enabled = false;
            txtAfter_Completion_of_how_many_Percentage_of_Delivery.Enabled = false;
            //drpCreated.Enabled = false;
            //txtCreatedDate.Enabled = false;
            cbActive.Enabled = false;
            //cbDeleted.Enabled = false;
            drpKitchenRequestingStore.Enabled = false;
            drpMainStore.Enabled = false;
            cbIncomingKitchenAutoAccept.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblSubscriptionSetups.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSubscriptionSetups.Where(m => m.TenentID == TID).OrderBy(m => m.LocalID).Take(take).Skip(Skip)).ToList());
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

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int Totalrec = DB.tblSubscriptionSetups.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSubscriptionSetups.Where(m => m.TenentID == TID).OrderBy(m => m.LocalID).Take(take).Skip(Skip)).ToList());
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
                //((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int Totalrec = DB.tblSubscriptionSetups.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSubscriptionSetups.Where(m=>m.TenentID==TID).OrderBy(m => m.LocalID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                //((HRMMater)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblSubscriptionSetups.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSubscriptionSetups.Where(m=>m.TenentID==TID).OrderBy(m => m.LocalID).Take(take).Skip(Skip)).ToList());
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
            using (TransactionScope scope = new TransactionScope())
            {

                if (e.CommandName == "btnDelete")
                {

                    //string[] ID = e.CommandArgument.ToString().Split(',');
                    //string str1 = ID[0].ToString();
                    //string str2 = ID[1].ToString();
                    int ID = Convert.ToInt32(e.CommandArgument);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    Database.tblSubscriptionSetup objSOJobDesc = DB.tblSubscriptionSetups.Single(p => p.TenentID == TID &&  p.LocalID == ID );
                   // objSOJobDesc.Active = false;
                    DB.tblSubscriptionSetups.DeleteObject(objSOJobDesc);
                    DB.SaveChanges();
                    BindData();
                    //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                    //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                    //((HRMMater)Page.Master).BindList(Listview1, (DB.tblSubscriptionSetup.OrderBy(m => m.JobId).Take(Tvalue).Skip(Svalue)).ToList());

                }

                if (e.CommandName == "btnEdit")
                {
                    //string[] ID = e.CommandArgument.ToString().Split(',');
                    //string str1 = ID[0].ToString();
                    //string str2 = ID[1].ToString();
                    int ID = Convert.ToInt32(e.CommandArgument);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    Database.tblSubscriptionSetup objtblSubscriptionSetup = DB.tblSubscriptionSetups.Single(p => p.TenentID == TID && p.LocalID == ID);
                 //   drplocationID.SelectedValue = objtblSubscriptionSetup.locationID.ToString();
                    //drpLocalID.SelectedValue = objtblSubscriptionSetup.LocalID.ToString();
                    if(objtblSubscriptionSetup.days_in_week != null && objtblSubscriptionSetup.days_in_week != 0)
                    drpdays_in_week.SelectedValue = objtblSubscriptionSetup.days_in_week.ToString();
                    if (objtblSubscriptionSetup.Whitch_day_delivery != null)
                    txtWhitch_day_delivery.Text = objtblSubscriptionSetup.Whitch_day_delivery.ToString();
                    if (objtblSubscriptionSetup.Week_Start_With_Day != null && objtblSubscriptionSetup.Week_Start_With_Day != "")
                    drpWeek_Start_With_Day.SelectedValue = objtblSubscriptionSetup.Week_Start_With_Day.ToString();
                    if (objtblSubscriptionSetup.Delivery_in_day != null && objtblSubscriptionSetup.Delivery_in_day != 0)
                    drpDelivery_in_day.SelectedValue = objtblSubscriptionSetup.Delivery_in_day.ToString();
                    if (objtblSubscriptionSetup.Delivery_time_begin != null && objtblSubscriptionSetup.Delivery_time_begin != "")
                    txtDelivery_time_begin.Text = objtblSubscriptionSetup.Delivery_time_begin.ToString();
                    cbChanges_Allowed.Checked = (objtblSubscriptionSetup.Changes_Allowed == true) ? true : false;
                    if(objtblSubscriptionSetup.Before_how_many_Hours != null)
                    txtBefore_how_many_Hours.Text = objtblSubscriptionSetup.Before_how_many_Hours.ToString();
                    cbRefund_Allowed.Checked = (objtblSubscriptionSetup.Refund_Allowed == true) ? true : false;
                    //  txtAfter_Completion_of_how_many_Percentage_of_Delivery.Text = objtblSubscriptionSetup.After_Completion_of_how_many_Percentage_of_Delivery.ToString();
                    //drpCreated.SelectedValue = objtblSubscriptionSetup.Created.ToString();
                    //txtCreatedDate.Text = objtblSubscriptionSetup.CreatedDate.ToString();
                    cbActive.Checked = (objtblSubscriptionSetup.Active == true) ? true : false;
                    //cbDeleted.Checked = (objtblSubscriptionSetup.Deleted == true) ? true : false;
                    if(objtblSubscriptionSetup.KitchenRequestingStore != null)
                    drpKitchenRequestingStore.SelectedValue = objtblSubscriptionSetup.KitchenRequestingStore.ToString();
                    if (objtblSubscriptionSetup.MainStore !=null)
                    drpMainStore.SelectedValue = objtblSubscriptionSetup.MainStore.ToString();
                    cbIncomingKitchenAutoAccept.Checked = (objtblSubscriptionSetup.IncomingKitchenAutoAccept == true) ? true : false;

                    btnAdd.Text = "Update Setting";
                    ViewState["Edit"] = ID;
                    Write();
                    btnAdd.Visible = true;
                }
                scope.Complete(); //  To commit.

            }
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tblSubscriptionSetups.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tblSubscriptionSetups.Where(m=>m.TenentID==TID).OrderBy(m => m.LocalID).Take(Tvalue).Skip(Svalue)).ToList());
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