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
    public partial class tbl_Event_Detail : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        int TID = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                txtSubEventID.Text = (DB.tbl_Event_Detail.Where(p => p.TenantID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Event_Detail.Where(p => p.TenantID == TID).Max(p => p.MyID) + 1) : 1).ToString();
                btnAdd.ValidationGroup = "ss";
                Readonly();
                //Session["LANGUAGE"] = "en-US";
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                BindList();
                GET();

                // FirstData();
                if (Request.QueryString["SEID"] != null && Request.QueryString["MEID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["SEID"]);
                    int MID = Convert.ToInt32(Request.QueryString["MEID"]);

                    Database.tbl_Event_Detail objtbl_Event_Detail = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.MyID == ID && p.EventID == MID);
                    drpEventID.SelectedValue = objtbl_Event_Detail.EventID.ToString();
                    txtevename.Text = drpEventID.SelectedItem.ToString();
                    drpEventID.SelectedValue = objtbl_Event_Detail.EventID.ToString();
                    txtSubEventID.Text = objtbl_Event_Detail.MyID.ToString();
                    txtCategoryID.Text = objtbl_Event_Detail.CategoryID.ToString();
                    drpEventType.SelectedValue = objtbl_Event_Detail.EventType.ToString();
                    drpEventTopic.SelectedValue = objtbl_Event_Detail.EventTopic.ToString();
                    txtRegisterationIDBegin.Text = objtbl_Event_Detail.RegisterationIDBegin.ToString();
                    txtRegNO.Text = objtbl_Event_Detail.RegisterationNumber.ToString();
                    txtTitle2Print.Text = objtbl_Event_Detail.Title2Print.ToString();
                    txtRegURL.Text = objtbl_Event_Detail.RegURL.ToString();
                    txtFromDate.Text = Convert.ToDateTime(objtbl_Event_Detail.FromDate).ToShortDateString();
                    txtToDate.Text = Convert.ToDateTime(objtbl_Event_Detail.ToDate).ToShortDateString();
                    txtDescription1.Text = objtbl_Event_Detail.Description1.ToString();
                    txtDescription2.Text = objtbl_Event_Detail.Description2.ToString();
                    txtDescription3.Text = objtbl_Event_Detail.Description3.ToString();
                    txtDMSid.Text = objtbl_Event_Detail.DMSid.ToString();
                    int viewStateSocialID = Convert.ToInt32(objtbl_Event_Detail.SocMediaID);
                    int ViewStateFloorID = Convert.ToInt32(objtbl_Event_Detail.FloorPlanID);
                    int ViewStateEmailID = Convert.ToInt32(objtbl_Event_Detail.eMailTemplateID);
                    //drpSocMediaID.Text = objtbl_Event_Detail.SocMediaID.ToString();
                    //drpContractID.SelectedValue = objtbl_Event_Detail.ContractID.ToString();
                    //drpFloorPlanID.SelectedValue = objtbl_Event_Detail.FloorPlanID.ToString();
                    //drpeMailTemplateID.SelectedValue = objtbl_Event_Detail.eMailTemplateID.ToString();
                    txtKeyWord.Text = objtbl_Event_Detail.KeyWord.ToString();
                    txtRate.Text = objtbl_Event_Detail.Rate.ToString();
                    txtForcastedVisitor.Text = objtbl_Event_Detail.ForcastedVisitor.ToString();
                    if (objtbl_Event_Detail.Activated == true)
                    {
                        btnActivate.Text = "Deactivate";
                        btnRegister.Enabled = true;
                        lblActivatedStatusDate.Text = "Activated Status and Date / Time " + Convert.ToDateTime(objtbl_Event_Detail.ActivatedDate).ToShortDateString() + " by " + objtbl_Event_Detail.ActivatedBy.ToString() + ". ";
                    }
                    else
                    {
                        if (objtbl_Event_Detail.Activated == false)
                        {
                            lblActivatedStatusDate.Text = "Deactivated Status and Date / Time " + Convert.ToDateTime(objtbl_Event_Detail.ActivatedDate).ToShortDateString() + " by " + objtbl_Event_Detail.ActivatedBy.ToString() + ". ";
                        }
                        btnActivate.Text = "Activate";

                    }
                    //cbDeleted.Checked = (objtbl_Event_Detail.Deleted == true) ? true : false;
                    //cbActivated.Checked = (objtbl_Event_Detail.Activated == true) ? true : false;
                    //txtActivatedDate.Text = objtbl_Event_Detail.ActivatedDate.ToString();
                    //txtActivatedBy.Text = objtbl_Event_Detail.ActivatedBy.ToString();
                    //drpCreatedBy.SelectedValue = objtbl_Event_Detail.CreatedBy.ToString();
                    //txtCreatedDate.Text = objtbl_Event_Detail.CreatedDate.ToString();
                    btnActivate.Enabled = true;
                    btnAdd.Text = "Update";
                    ViewState["Edit"] = ID;
                    ViewState["EVENTID"] = objtbl_Event_Detail.EventID;
                    ViewState["MYID"] = ID;

                    ViewState["Social"] = viewStateSocialID;//for socialMedia
                    ViewState["Floor"] = ViewStateFloorID;//for FloorPlane
                    ViewState["Email"] = ViewStateEmailID;//for EmailTemplate
                    SoiclMediya(viewStateSocialID);//bind Listview socialMedia
                    FloorPlan(ViewStateFloorID);//bind Listview FloorPlane
                    EmailTemplate(ViewStateEmailID);//bind Listview EmailTemplate
                    pnlsocialmedia.Enabled = true;
                    Write();
                }

            }
        }
        public void BindList()
        {
            //var TBLEventMain = from item in DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.Activated == true).ToList()
            //                   select new
            //                   {
            //                       EvevtName = ((Int32)item.EventID).ToString() + " - " + item.Title2Print,
            //                       EventID = ((Int32)item.EventID).ToString()
            //                   };

            //drpEventID.DataSource = TBLEventMain;//DB.Tbl_Event_Main.Where(p => p.TenantID == TID);
            //drpEventID.DataTextField = "EvevtName";
            //drpEventID.DataValueField = "EventID";
            //drpEventID.DataBind();
            //drpEventID.Items.Insert(0, new ListItem("-- Select --", "0"));

            //Classes.EcommAdminClass.getdropdown(drpSomib, TID, "social", "media", "", "REFTABLE");

            Classes.EcommAdminClass.getdropdown(drpEventType, TID, "EVN", "EventType", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpEventTopic, TID, "EVN", "EventTopic", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpSocMediaID, TID, "social", "media", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpFloorPlanID, TID, "Floor", "Plan", "", "REFTABLE");
            Classes.EcommAdminClass.getdropdown(drpeMailTemplateID, TID, "email", "Template", "", "REFTABLE");
            drpContractID.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpFloorPlanID.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpeMailTemplateID.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        public bool ManageDate(DateTime fromTime, DateTime Totime, int Event)
        {
            //Database.Tbl_Event_Main objMain = DB.Tbl_Event_Main.Single(p => p.TenantID == TID && p.EventID == EventID);
            DateTime fromdt = Convert.ToDateTime(txtFromDate.Text);
            DateTime todt = Convert.ToDateTime(txtToDate.Text);
            if (DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.EventID == Event && p.FromDate.Value <= fromdt.Date && p.ToDate.Value >= todt.Date).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void GET()
        {
            if (Request.QueryString["MainEventID"] != null)
            {
                int mid = Convert.ToInt32(Request.QueryString["MainEventID"]);
                string MID1 = mid.ToString();
                var TBLEventMain = from item in DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.Activated == true).ToList()
                                   select new
                                   {
                                       EvevtName = ((Int32)item.EventID).ToString() + " - " + item.Title2Print,
                                       EventID = ((Int32)item.EventID).ToString()
                                   };
                //var obj = DB.Tbl_Event_Main.Where(p => p.EventID == mid);
                drpEventID.DataSource = TBLEventMain.Where(p => p.EventID == MID1);
                drpEventID.DataTextField = "EvevtName";
                drpEventID.DataValueField = "EventID";
                drpEventID.DataBind();
                txtevename.Text = drpEventID.SelectedItem.ToString();
                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "submit123";
            }
            else if (Request.QueryString["SEID"] != null)
            {
                int mid = Convert.ToInt32(Request.QueryString["SEID"]);
                string MID1 = mid.ToString();
                var TBLEventMain = from item in DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.Activated == true).ToList()
                                   select new
                                   {
                                       EvevtName = ((Int32)item.EventID).ToString() + " - " + item.Title2Print,
                                       EventID = ((Int32)item.EventID).ToString()
                                   };
                //var obj = DB.Tbl_Event_Main.Where(p => p.EventID == mid);
                drpEventID.DataSource = TBLEventMain.Where(p => p.EventID == MID1);
                drpEventID.DataTextField = "EvevtName";
                drpEventID.DataValueField = "EventID";
                drpEventID.DataBind();
                txtevename.Text = drpEventID.SelectedItem.ToString();
                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "submit123";
            }
            else
            {
                var TBLEventMain = from item in DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.Activated == true).ToList()
                                   select new
                                   {
                                       EvevtName = ((Int32)item.EventID).ToString() + " - " + item.Title2Print,
                                       EventID = ((Int32)item.EventID).ToString()
                                   };
                //List<Database.Tbl_Event_Main> obj1 = DB.Tbl_Event_Main.Where(p => p.Activated == true).ToList();
                drpEventID.DataSource = TBLEventMain;
                drpEventID.DataTextField = "EvevtName";
                drpEventID.DataValueField = "EventID";
                drpEventID.DataBind();
                drpEventID.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        #region Step2
        public void BindData()
        {
            List<Database.tbl_Event_Detail> List = new List<Database.tbl_Event_Detail>();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = 0;
            if (Request.QueryString["MainEventID"] != null)
            {

                int MEID = Convert.ToInt32(Request.QueryString["MainEventID"]);
                List = DB.tbl_Event_Detail.Where(a => a.EventID == MEID && a.TenantID == TID).OrderBy(m => m.EventID).ToList();
                Totalrec = List.Count();
            }
            else if (Request.QueryString["MEID"] != null)
            {
                int MEID = Convert.ToInt32(Request.QueryString["MEID"]);
                List = DB.tbl_Event_Detail.Where(a => a.EventID == MEID && a.TenantID == TID).OrderBy(m => m.EventID).ToList();
                Totalrec = List.Count();
            }
            else
            {
                List = DB.tbl_Event_Detail.Where(m=>m.TenantID ==TID).OrderBy(m => m.EventID).ToList();
                Totalrec = List.Count();
            }


            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblEventID1s.Attributes["class"] = lblMyID1s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblEventType1s.Attributes["class"] = lblEventTopic1s.Attributes["class"] = lblRegisterationIDBegin1s.Attributes["class"] = lblTitle2Print1s.Attributes["class"] = lblRegURL1s.Attributes["class"] = lblFromDate1s.Attributes["class"] = lblToDate1s.Attributes["class"] = lblDescription11s.Attributes["class"] = lblDescription21s.Attributes["class"] = lblDescription31s.Attributes["class"] = lblDMSid1s.Attributes["class"] = lblSocMediaID1s.Attributes["class"] = lblContractID1s.Attributes["class"] = lblFloorPlanID1s.Attributes["class"] = lbleMailTemplateID1s.Attributes["class"] = lblKeyWord1s.Attributes["class"] = lblRate1s.Attributes["class"] = "control-label col-md-4  getshow";//lblForcastedVisitor1s.Attributes["class"] = 
            lblEventID2h.Attributes["class"] = lblMyID2h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblEventType2h.Attributes["class"] = lblEventTopic2h.Attributes["class"] = lblRegisterationIDBegin2h.Attributes["class"] = lblTitle2Print2h.Attributes["class"] = lblRegURL2h.Attributes["class"] = lblFromDate2h.Attributes["class"] = lblToDate2h.Attributes["class"] = lblDescription12h.Attributes["class"] = lblDescription22h.Attributes["class"] = lblDescription32h.Attributes["class"] = lblDMSid2h.Attributes["class"] = lblSocMediaID2h.Attributes["class"] = lblContractID2h.Attributes["class"] = lblFloorPlanID2h.Attributes["class"] = lbleMailTemplateID2h.Attributes["class"] = lblKeyWord2h.Attributes["class"] = lblRate2h.Attributes["class"] = "control-label col-md-4  gethide";//lblForcastedVisitor2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblEventID1s.Attributes["class"] = lblMyID1s.Attributes["class"] = lblCategoryID1s.Attributes["class"] = lblEventType1s.Attributes["class"] = lblEventTopic1s.Attributes["class"] = lblRegisterationIDBegin1s.Attributes["class"] = lblTitle2Print1s.Attributes["class"] = lblRegURL1s.Attributes["class"] = lblFromDate1s.Attributes["class"] = lblToDate1s.Attributes["class"] = lblDescription11s.Attributes["class"] = lblDescription21s.Attributes["class"] = lblDescription31s.Attributes["class"] = lblDMSid1s.Attributes["class"] = lblSocMediaID1s.Attributes["class"] = lblContractID1s.Attributes["class"] = lblFloorPlanID1s.Attributes["class"] = lbleMailTemplateID1s.Attributes["class"] = lblKeyWord1s.Attributes["class"] = lblRate1s.Attributes["class"] = "control-label col-md-4  gethide";//lblForcastedVisitor1s.Attributes["class"] =
            lblEventID2h.Attributes["class"] = lblMyID2h.Attributes["class"] = lblCategoryID2h.Attributes["class"] = lblEventType2h.Attributes["class"] = lblEventTopic2h.Attributes["class"] = lblRegisterationIDBegin2h.Attributes["class"] = lblTitle2Print2h.Attributes["class"] = lblRegURL2h.Attributes["class"] = lblFromDate2h.Attributes["class"] = lblToDate2h.Attributes["class"] = lblDescription12h.Attributes["class"] = lblDescription22h.Attributes["class"] = lblDescription32h.Attributes["class"] = lblDMSid2h.Attributes["class"] = lblSocMediaID2h.Attributes["class"] = lblContractID2h.Attributes["class"] = lblFloorPlanID2h.Attributes["class"] = lbleMailTemplateID2h.Attributes["class"] = lblKeyWord2h.Attributes["class"] = lblRate2h.Attributes["class"] = "control-label col-md-4  getshow";//lblForcastedVisitor2h.Attributes["class"] =
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
            drpEventID.SelectedIndex = 0;
            // drpMyID.SelectedIndex = 0;
            txtCategoryID.Text = "";
            drpEventType.SelectedIndex = 0;
            drpEventTopic.SelectedIndex = 0;
            txtRegisterationIDBegin.Text = "";
            txtTitle2Print.Text = "";
            txtRegURL.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtDescription1.Text = "";
            txtDescription2.Text = "";
            txtDescription3.Text = "";
            txtDMSid.Text = "1";
            drpSocMediaID.SelectedIndex = 0;
            drpContractID.SelectedIndex = 0;
            drpFloorPlanID.SelectedIndex = 0;
            drpeMailTemplateID.SelectedIndex = 0;
            txtKeyWord.Text = "";
            txtRate.Text = "";
            txtForcastedVisitor.Text = "";
            //txtDeleted.Text = "";
            //txtActivated.Text = "";
            //txtActivatedDate.Text = "";
            //txtActivatedBy.Text = "";
            //drpCreatedBy.SelectedIndex = 0;
            //txtCreatedDate.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                if (btnAdd.Text == "Add New")
                {
                    pnlSuccessMsg.Visible = false;
                    Write();
                    Clear();
                    btnAdd.Text = "Add";
                    btnAdd.ValidationGroup = "submit123";
                }
                else if (btnAdd.Text == "Add")
                {

                    DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtToDate.Text);
                    int EventID = Convert.ToInt32(drpEventID.SelectedValue);
                    string fromandtodatedisplay = "";
                    bool flage = ManageDate(fromDate, Todate, EventID);
                    if (flage == true)
                    {
                        Database.tbl_Event_Detail objtbl_Event_Detail = new Database.tbl_Event_Detail();
                        //Server Content Send data Yogesh
                        objtbl_Event_Detail.TenantID = TID;
                        objtbl_Event_Detail.EventID = EventID;
                        int EventSubID = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Count() > 0 ? Convert.ToInt32(DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Max(p => p.MyID) + 1) : 1; //Convert.ToInt32(drpMyID.SelectedValue);
                        objtbl_Event_Detail.MyID = EventSubID;
                        //generate for socialsite ID
                        string Concat = EventID.ToString() + "" + EventSubID.ToString();
                        int EventsocialSub = Convert.ToInt32(Concat);
                        //
                        //objtbl_Event_Detail.CategoryID = Convert.ToInt32(txtCategoryID.Text);
                        objtbl_Event_Detail.EventType = Convert.ToInt32(drpEventType.SelectedValue);
                        objtbl_Event_Detail.EventTopic = Convert.ToInt32(drpEventTopic.SelectedValue);
                        objtbl_Event_Detail.RegisterationIDBegin = txtRegisterationIDBegin.Text;
                        objtbl_Event_Detail.RegisterationNumber = Convert.ToInt32(txtRegNO.Text);
                        objtbl_Event_Detail.Title2Print = txtTitle2Print.Text;
                        objtbl_Event_Detail.RegURL = txtRegURL.Text;
                        objtbl_Event_Detail.FromDate = Convert.ToDateTime(txtFromDate.Text);
                        objtbl_Event_Detail.ToDate = Convert.ToDateTime(txtToDate.Text);
                        objtbl_Event_Detail.Description1 = txtDescription1.Text;
                        objtbl_Event_Detail.Description2 = txtDescription2.Text;
                        objtbl_Event_Detail.Description3 = txtDescription3.Text;
                        objtbl_Event_Detail.DMSid = Convert.ToInt32(txtDMSid.Text);
                        objtbl_Event_Detail.SocMediaID = EventsocialSub;
                        objtbl_Event_Detail.ContractID = EventsocialSub;
                        objtbl_Event_Detail.FloorPlanID = EventsocialSub;
                        objtbl_Event_Detail.eMailTemplateID = EventsocialSub;
                        objtbl_Event_Detail.KeyWord = txtKeyWord.Text;
                        objtbl_Event_Detail.Rate = Convert.ToDecimal(txtRate.Text);
                        objtbl_Event_Detail.ForcastedVisitor = Convert.ToInt32(txtForcastedVisitor.Text);
                        objtbl_Event_Detail.Deleted = true;
                        //objtbl_Event_Detail.Activated = true;
                        //objtbl_Event_Detail.ActivatedDate = DateTime.Now;
                        //objtbl_Event_Detail.ActivatedBy = txtActivatedBy.Text;
                        objtbl_Event_Detail.CreatedBy = 1;
                        objtbl_Event_Detail.CreatedDate = DateTime.Now;


                        //int EventID = Convert.ToInt32(drpEventID.SelectedValue);
                        //int subID = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Count() > 0 ? Convert.ToInt32(DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Max(p => p.MyID) + 1) : 1;
                        //string Concat = EventID.ToString() + "" + subID.ToString();
                        //int EventSub = Convert.ToInt32(Concat);

                        DB.tbl_Event_Detail.AddObject(objtbl_Event_Detail);
                        DB.SaveChanges();
                        Clear();
                        //lblMsg.Text = "  Data Save Successfully";
                        //pnlSuccessMsg.Visible = true;
                        btnAdd.ValidationGroup = "ss";
                        btnAdd.Text = "Add New";
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        //FirstData();
                    }
                    else
                    {
                        var obj = DB.Tbl_Event_Main.Single(p => p.TenantID == TID && p.EventID == EventID);
                        fromandtodatedisplay = "Choose between From date:" + Convert.ToDateTime(obj.FromDate).ToShortDateString() + " and To Date: " + Convert.ToDateTime(obj.ToDate).ToShortDateString();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "'from date is not valide date ," + fromandtodatedisplay + "'", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                    }
                }
                else if (btnAdd.Text == "Update")
                {
                    if (ViewState["EVENTID"] != null && ViewState["MYID"] != null)
                    {
                        int Eventid = Convert.ToInt32(ViewState["EVENTID"]);
                        int Myid = Convert.ToInt32(ViewState["MYID"]);
                        //int ID = Convert.ToInt32(ViewState["Edit"]);
                        Database.tbl_Event_Detail objtbl_Event_Detail = DB.tbl_Event_Detail.Single(p => p.EventID == Eventid && p.MyID == Myid && p.TenantID == TID);
                        //objtbl_Event_Detail.EventID = Convert.ToInt32(drpEventID.SelectedValue);
                        //objtbl_Event_Detail.MyID = Convert.ToInt32(drpMyID.SelectedValue);
                        //objtbl_Event_Detail.CategoryID = Convert.ToInt32(txtCategoryID.Text);
                        objtbl_Event_Detail.EventType = Convert.ToInt32(drpEventType.SelectedValue);
                        objtbl_Event_Detail.EventTopic = Convert.ToInt32(drpEventTopic.SelectedValue);
                        objtbl_Event_Detail.RegisterationIDBegin = txtRegisterationIDBegin.Text;
                        objtbl_Event_Detail.RegisterationNumber = Convert.ToInt32(txtRegNO.Text);
                        objtbl_Event_Detail.Title2Print = txtTitle2Print.Text;
                        objtbl_Event_Detail.RegURL = txtRegURL.Text;
                        objtbl_Event_Detail.FromDate = Convert.ToDateTime(txtFromDate.Text);
                        objtbl_Event_Detail.ToDate = Convert.ToDateTime(txtToDate.Text);
                        objtbl_Event_Detail.Description1 = txtDescription1.Text;
                        objtbl_Event_Detail.Description2 = txtDescription2.Text;
                        objtbl_Event_Detail.Description3 = txtDescription3.Text;
                        objtbl_Event_Detail.DMSid = Convert.ToInt32(txtDMSid.Text);
                        //objtbl_Event_Detail.SocMediaID = Convert.ToInt32(drpSocMediaID.SelectedValue);
                        //objtbl_Event_Detail.ContractID = Convert.ToInt32(drpContractID.SelectedValue);
                        //objtbl_Event_Detail.FloorPlanID = Convert.ToInt32(drpFloorPlanID.SelectedValue);
                        //objtbl_Event_Detail.eMailTemplateID = Convert.ToInt32(drpeMailTemplateID.SelectedValue);
                        objtbl_Event_Detail.KeyWord = txtKeyWord.Text;
                        objtbl_Event_Detail.Rate = Convert.ToDecimal(txtRate.Text);
                        objtbl_Event_Detail.ForcastedVisitor = Convert.ToInt32(txtForcastedVisitor.Text);

                        //objtbl_Event_Detail.Deleted = cbDeleted.Checked;
                        //objtbl_Event_Detail.Activated = cbActivated.Checked;
                        //objtbl_Event_Detail.ActivatedDate = Convert.ToDateTime(txtActivatedDate.Text);
                        //objtbl_Event_Detail.ActivatedBy = txtActivatedBy.Text;
                        //objtbl_Event_Detail.CreatedBy = Convert.ToInt32(drpCreatedBy.SelectedValue);
                        //objtbl_Event_Detail.CreatedDate = Convert.ToDateTime(txtCreatedDate.Text);
                        ViewState["EVENTID"] = null;
                        ViewState["MYID"] = null;
                        ViewState["Edit"] = null;
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        DB.SaveChanges();
                        Clear();
                        //lblMsg.Text = "  Data Edit Successfully";
                        //pnlSuccessMsg.Visible = true;
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                }
                BindData();

                scope.Complete(); //  To commit.

            }
            //catch (Exception ex)
            //{
            //    throw;
            //}
            // }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCreatedDate.Items.Insert(0, new ListItem("-- Select --", "0"));drpCreatedDate.DataSource = DB.0;drpCreatedDate.DataTextField = "0";drpCreatedDate.DataValueField = "0";drpCreatedDate.DataBind();
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            //FirstData();
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
            drpEventID.SelectedValue = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
            //drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtCategoryID.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
            drpEventType.SelectedValue = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
            drpEventTopic.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
            txtRegisterationIDBegin.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
            txtTitle2Print.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
            txtRegURL.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";
            txtFromDate.Text = Listview1.SelectedDataKey[7] != null ? Listview1.SelectedDataKey[7].ToString() : "";
            txtToDate.Text = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
            txtDescription1.Text = Listview1.SelectedDataKey[9] != null ? Listview1.SelectedDataKey[9].ToString() : "";
            txtDescription2.Text = Listview1.SelectedDataKey[10] != null ? Listview1.SelectedDataKey[10].ToString() : "";
            txtDescription3.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
            txtDMSid.Text = Listview1.SelectedDataKey[12] != null ? Listview1.SelectedDataKey[12].ToString() : "";
            drpSocMediaID.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
            drpContractID.SelectedValue = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
            drpFloorPlanID.SelectedValue = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
            drpeMailTemplateID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";
            txtKeyWord.Text = Listview1.SelectedDataKey[17] != null ? Listview1.SelectedDataKey[17].ToString() : "";
            txtRate.Text = Listview1.SelectedDataKey[18] != null ? Listview1.SelectedDataKey[18].ToString() : "";
            txtForcastedVisitor.Text = Listview1.SelectedDataKey[19] != null ? Listview1.SelectedDataKey[19].ToString() : "";
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtActivatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txtActivatedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                drpEventID.SelectedValue = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
                //drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtCategoryID.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
                drpEventType.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                drpEventTopic.SelectedValue = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtRegisterationIDBegin.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                txtTitle2Print.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";
                txtRegURL.Text = Listview1.SelectedDataKey[7] != null ? Listview1.SelectedDataKey[7].ToString() : "";
                txtFromDate.Text = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
                txtToDate.Text = Listview1.SelectedDataKey[9] != null ? Listview1.SelectedDataKey[9].ToString() : "";
                txtDescription1.Text = Listview1.SelectedDataKey[10] != null ? Listview1.SelectedDataKey[10].ToString() : "";
                txtDescription2.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
                txtDescription3.Text = Listview1.SelectedDataKey[12] != null ? Listview1.SelectedDataKey[12].ToString() : "";
                txtDMSid.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
                drpSocMediaID.Text = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
                drpContractID.SelectedValue = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
                drpFloorPlanID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";
                drpeMailTemplateID.SelectedValue = Listview1.SelectedDataKey[17] != null ? Listview1.SelectedDataKey[17].ToString() : "";
                txtKeyWord.Text = Listview1.SelectedDataKey[18] != null ? Listview1.SelectedDataKey[18].ToString() : "";
                txtRate.Text = Listview1.SelectedDataKey[19] != null ? Listview1.SelectedDataKey[19].ToString() : "";
                txtForcastedVisitor.Text = Listview1.SelectedDataKey[20] != null ? Listview1.SelectedDataKey[20].ToString() : "";
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtActivatedDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txtActivatedBy.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

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
                drpEventID.SelectedValue = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
                //drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtCategoryID.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
                drpEventType.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                drpEventTopic.SelectedValue = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtRegisterationIDBegin.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                txtTitle2Print.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";
                txtRegURL.Text = Listview1.SelectedDataKey[7] != null ? Listview1.SelectedDataKey[7].ToString() : "";
                txtFromDate.Text = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
                txtToDate.Text = Listview1.SelectedDataKey[9] != null ? Listview1.SelectedDataKey[9].ToString() : "";
                txtDescription1.Text = Listview1.SelectedDataKey[10] != null ? Listview1.SelectedDataKey[10].ToString() : "";
                txtDescription2.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
                txtDescription3.Text = Listview1.SelectedDataKey[12] != null ? Listview1.SelectedDataKey[12].ToString() : "";
                txtDMSid.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
                drpSocMediaID.Text = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
                drpContractID.SelectedValue = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
                drpFloorPlanID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";
                drpeMailTemplateID.SelectedValue = Listview1.SelectedDataKey[17] != null ? Listview1.SelectedDataKey[17].ToString() : "";
                txtKeyWord.Text = Listview1.SelectedDataKey[18] != null ? Listview1.SelectedDataKey[18].ToString() : "";
                txtRate.Text = Listview1.SelectedDataKey[19] != null ? Listview1.SelectedDataKey[19].ToString() : "";
                txtForcastedVisitor.Text = Listview1.SelectedDataKey[20] != null ? Listview1.SelectedDataKey[20].ToString() : "";
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtActivatedDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txtActivatedBy.Text = Listview1.SelectedDataKey[0].ToString();
                //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            drpEventID.SelectedValue = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
            //drpMyID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtCategoryID.Text = Listview1.SelectedDataKey[1] != null ? Listview1.SelectedDataKey[1].ToString() : "";
            drpEventType.SelectedValue = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
            drpEventTopic.SelectedValue = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
            txtRegisterationIDBegin.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
            txtTitle2Print.Text = Listview1.SelectedDataKey[6] != null ? Listview1.SelectedDataKey[6].ToString() : "";
            txtRegURL.Text = Listview1.SelectedDataKey[7] != null ? Listview1.SelectedDataKey[7].ToString() : "";
            txtFromDate.Text = Listview1.SelectedDataKey[8] != null ? Listview1.SelectedDataKey[8].ToString() : "";
            txtToDate.Text = Listview1.SelectedDataKey[9] != null ? Listview1.SelectedDataKey[9].ToString() : "";
            txtDescription1.Text = Listview1.SelectedDataKey[10] != null ? Listview1.SelectedDataKey[10].ToString() : "";
            txtDescription2.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
            txtDescription3.Text = Listview1.SelectedDataKey[12] != null ? Listview1.SelectedDataKey[12].ToString() : "";
            txtDMSid.Text = Listview1.SelectedDataKey[13] != null ? Listview1.SelectedDataKey[13].ToString() : "";
            drpSocMediaID.SelectedValue = Listview1.SelectedDataKey[14] != null ? Listview1.SelectedDataKey[14].ToString() : "";
            drpContractID.SelectedValue = Listview1.SelectedDataKey[15] != null ? Listview1.SelectedDataKey[15].ToString() : "";
            drpFloorPlanID.SelectedValue = Listview1.SelectedDataKey[16] != null ? Listview1.SelectedDataKey[16].ToString() : "";
            drpeMailTemplateID.SelectedValue = Listview1.SelectedDataKey[17] != null ? Listview1.SelectedDataKey[17].ToString() : "";
            txtKeyWord.Text = Listview1.SelectedDataKey[18] != null ? Listview1.SelectedDataKey[18].ToString() : "";
            txtRate.Text = Listview1.SelectedDataKey[19] != null ? Listview1.SelectedDataKey[19].ToString() : "";
            txtForcastedVisitor.Text = Listview1.SelectedDataKey[20] != null ? Listview1.SelectedDataKey[20].ToString() : "";
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbActivated.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtActivatedDate.Text = Listview1.SelectedDataKey[0].ToString();
            //txtActivatedBy.Text = Listview1.SelectedDataKey[0].ToString();
            //drpCreatedBy.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtCreatedDate.Text = Listview1.SelectedDataKey[0].ToString();

        }



        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblEventID2h.Visible = lblMyID2h.Visible = lblCategoryID2h.Visible = lblEventType2h.Visible = lblEventTopic2h.Visible = lblRegisterationIDBegin2h.Visible = lblTitle2Print2h.Visible = lblRegURL2h.Visible = lblFromDate2h.Visible = lblToDate2h.Visible = lblDescription12h.Visible = lblDescription22h.Visible = lblDescription32h.Visible = lblDMSid2h.Visible = lblSocMediaID2h.Visible = lblContractID2h.Visible = lblFloorPlanID2h.Visible = lbleMailTemplateID2h.Visible = lblKeyWord2h.Visible = lblRate2h.Visible = false;//lblForcastedVisitor2h.Visible =
                    //2true
                    txtEventID2h.Visible = txtMyID2h.Visible = txtCategoryID2h.Visible = txtEventType2h.Visible = txtEventTopic2h.Visible = txtRegisterationIDBegin2h.Visible = txtTitle2Print2h.Visible = txtRegURL2h.Visible = txtFromDate2h.Visible = txtToDate2h.Visible = txtDescription12h.Visible = txtDescription22h.Visible = txtDescription32h.Visible = txtDMSid2h.Visible = txtSocMediaID2h.Visible = txtContractID2h.Visible = txtFloorPlanID2h.Visible = txteMailTemplateID2h.Visible = txtKeyWord2h.Visible = txtRate2h.Visible = true;//txtForcastedVisitor2h.Visible =

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
                    lblEventID2h.Visible = lblMyID2h.Visible = lblCategoryID2h.Visible = lblEventType2h.Visible = lblEventTopic2h.Visible = lblRegisterationIDBegin2h.Visible = lblTitle2Print2h.Visible = lblRegURL2h.Visible = lblFromDate2h.Visible = lblToDate2h.Visible = lblDescription12h.Visible = lblDescription22h.Visible = lblDescription32h.Visible = lblDMSid2h.Visible = lblSocMediaID2h.Visible = lblContractID2h.Visible = lblFloorPlanID2h.Visible = lbleMailTemplateID2h.Visible = lblKeyWord2h.Visible = lblRate2h.Visible = true;//lblForcastedVisitor2h.Visible =
                    //2false
                    txtEventID2h.Visible = txtMyID2h.Visible = txtCategoryID2h.Visible = txtEventType2h.Visible = txtEventTopic2h.Visible = txtRegisterationIDBegin2h.Visible = txtTitle2Print2h.Visible = txtRegURL2h.Visible = txtFromDate2h.Visible = txtToDate2h.Visible = txtDescription12h.Visible = txtDescription22h.Visible = txtDescription32h.Visible = txtDMSid2h.Visible = txtSocMediaID2h.Visible = txtContractID2h.Visible = txtFloorPlanID2h.Visible = txteMailTemplateID2h.Visible = txtKeyWord2h.Visible = txtRate2h.Visible = false;//txtForcastedVisitor2h.Visible =

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
                    lblEventID1s.Visible = lblMyID1s.Visible = lblCategoryID1s.Visible = lblEventType1s.Visible = lblEventTopic1s.Visible = lblRegisterationIDBegin1s.Visible = lblTitle2Print1s.Visible = lblRegURL1s.Visible = lblFromDate1s.Visible = lblToDate1s.Visible = lblDescription11s.Visible = lblDescription21s.Visible = lblDescription31s.Visible = lblDMSid1s.Visible = lblSocMediaID1s.Visible = lblContractID1s.Visible = lblFloorPlanID1s.Visible = lbleMailTemplateID1s.Visible = lblKeyWord1s.Visible = lblRate1s.Visible = false;//lblForcastedVisitor1s.Visible =
                    //1true
                    txtEventID1s.Visible = txtMyID1s.Visible = txtCategoryID1s.Visible = txtEventType1s.Visible = txtEventTopic1s.Visible = txtRegisterationIDBegin1s.Visible = txtTitle2Print1s.Visible = txtRegURL1s.Visible = txtFromDate1s.Visible = txtToDate1s.Visible = txtDescription11s.Visible = txtDescription21s.Visible = txtDescription31s.Visible = txtDMSid1s.Visible = txtSocMediaID1s.Visible = txtContractID1s.Visible = txtFloorPlanID1s.Visible = txteMailTemplateID1s.Visible = txtKeyWord1s.Visible = txtRate1s.Visible = true;//txtForcastedVisitor1s.Visible =
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
                    lblEventID1s.Visible = lblMyID1s.Visible = lblCategoryID1s.Visible = lblEventType1s.Visible = lblEventTopic1s.Visible = lblRegisterationIDBegin1s.Visible = lblTitle2Print1s.Visible = lblRegURL1s.Visible = lblFromDate1s.Visible = lblToDate1s.Visible = lblDescription11s.Visible = lblDescription21s.Visible = lblDescription31s.Visible = lblDMSid1s.Visible = lblSocMediaID1s.Visible = lblContractID1s.Visible = lblFloorPlanID1s.Visible = lbleMailTemplateID1s.Visible = lblKeyWord1s.Visible = lblRate1s.Visible = true;// lblForcastedVisitor1s.Visible =
                    //1false
                    txtEventID1s.Visible = txtMyID1s.Visible = txtCategoryID1s.Visible = txtEventType1s.Visible = txtEventTopic1s.Visible = txtRegisterationIDBegin1s.Visible = txtTitle2Print1s.Visible = txtRegURL1s.Visible = txtFromDate1s.Visible = txtToDate1s.Visible = txtDescription11s.Visible = txtDescription21s.Visible = txtDescription31s.Visible = txtDMSid1s.Visible = txtSocMediaID1s.Visible = txtContractID1s.Visible = txtFloorPlanID1s.Visible = txteMailTemplateID1s.Visible = txtKeyWord1s.Visible = txtRate1s.Visible = false;//txtForcastedVisitor1s.Visible =
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_Event_Detail").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblEventID1s.ID == item.LabelID)
                    txtEventID1s.Text = lblEventID1s.Text = lblhEventID.Text = item.LabelName;
                else if (lblMyID1s.ID == item.LabelID)
                    txtMyID1s.Text = lblMyID1s.Text = item.LabelName;
                else if (lblCategoryID1s.ID == item.LabelID)
                    txtCategoryID1s.Text = lblCategoryID1s.Text = item.LabelName;
                else if (lblEventType1s.ID == item.LabelID)
                    txtEventType1s.Text = lblEventType1s.Text = lblhEventType.Text = item.LabelName;
                else if (lblEventTopic1s.ID == item.LabelID)
                    txtEventTopic1s.Text = lblEventTopic1s.Text = lblhEventTopic.Text = item.LabelName;
                else if (lblRegisterationIDBegin1s.ID == item.LabelID)
                    txtRegisterationIDBegin1s.Text = lblRegisterationIDBegin1s.Text = item.LabelName;
                else if (lblTitle2Print1s.ID == item.LabelID)
                    txtTitle2Print1s.Text = lblTitle2Print1s.Text = lblhTitle2Print.Text = item.LabelName;
                else if (lblRegURL1s.ID == item.LabelID)
                    txtRegURL1s.Text = lblRegURL1s.Text = item.LabelName;
                else if (lblFromDate1s.ID == item.LabelID)
                    txtFromDate1s.Text = lblFromDate1s.Text = lblhFromDate.Text = item.LabelName;
                else if (lblToDate1s.ID == item.LabelID)
                    txtToDate1s.Text = lblToDate1s.Text = lblhToDate.Text = item.LabelName;
                else if (lblDescription11s.ID == item.LabelID)
                    txtDescription11s.Text = lblDescription11s.Text = lblhDescription1.Text = item.LabelName;
                else if (lblDescription21s.ID == item.LabelID)
                    txtDescription21s.Text = lblDescription21s.Text = item.LabelName;
                else if (lblDescription31s.ID == item.LabelID)
                    txtDescription31s.Text = lblDescription31s.Text = item.LabelName;
                else if (lblDMSid1s.ID == item.LabelID)
                    txtDMSid1s.Text = lblDMSid1s.Text = item.LabelName;
                else if (lblSocMediaID1s.ID == item.LabelID)
                    txtSocMediaID1s.Text = lblSocMediaID1s.Text = item.LabelName;
                else if (lblContractID1s.ID == item.LabelID)
                    txtContractID1s.Text = lblContractID1s.Text = item.LabelName;
                else if (lblFloorPlanID1s.ID == item.LabelID)
                    txtFloorPlanID1s.Text = lblFloorPlanID1s.Text = item.LabelName;
                else if (lbleMailTemplateID1s.ID == item.LabelID)
                    txteMailTemplateID1s.Text = lbleMailTemplateID1s.Text = item.LabelName;
                else if (lblKeyWord1s.ID == item.LabelID)
                    txtKeyWord1s.Text = lblKeyWord1s.Text = item.LabelName;
                else if (lblRate1s.ID == item.LabelID)
                    txtRate1s.Text = lblRate1s.Text = item.LabelName;
                //else if (lblForcastedVisitor1s.ID == item.LabelID)
                //    txtForcastedVisitor.Text = lblForcastedVisitor1s.Text = item.LabelName;

                else if (lblEventID2h.ID == item.LabelID)
                    txtEventID2h.Text = lblEventID2h.Text = lblhEventID.Text = item.LabelName;
                else if (lblMyID2h.ID == item.LabelID)
                    txtMyID2h.Text = lblMyID2h.Text = item.LabelName;
                else if (lblCategoryID2h.ID == item.LabelID)
                    txtCategoryID2h.Text = lblCategoryID2h.Text = item.LabelName;
                else if (lblEventType2h.ID == item.LabelID)
                    txtEventType2h.Text = lblEventType2h.Text = lblhEventType.Text = item.LabelName;
                else if (lblEventTopic2h.ID == item.LabelID)
                    txtEventTopic2h.Text = lblEventTopic2h.Text = lblhEventTopic.Text = item.LabelName;
                else if (lblRegisterationIDBegin2h.ID == item.LabelID)
                    txtRegisterationIDBegin2h.Text = lblRegisterationIDBegin2h.Text = item.LabelName;
                else if (lblTitle2Print2h.ID == item.LabelID)
                    txtTitle2Print2h.Text = lblTitle2Print2h.Text = lblhTitle2Print.Text = item.LabelName;
                else if (lblRegURL2h.ID == item.LabelID)
                    txtRegURL2h.Text = lblRegURL2h.Text = item.LabelName;
                else if (lblFromDate2h.ID == item.LabelID)
                    txtFromDate2h.Text = lblFromDate2h.Text = lblhFromDate.Text = item.LabelName;
                else if (lblToDate2h.ID == item.LabelID)
                    txtToDate2h.Text = lblToDate2h.Text = lblhToDate.Text = item.LabelName;
                else if (lblDescription12h.ID == item.LabelID)
                    txtDescription12h.Text = lblDescription12h.Text = lblhDescription1.Text = item.LabelName;
                else if (lblDescription22h.ID == item.LabelID)
                    txtDescription22h.Text = lblDescription22h.Text = item.LabelName;
                else if (lblDescription32h.ID == item.LabelID)
                    txtDescription32h.Text = lblDescription32h.Text = item.LabelName;
                else if (lblDMSid2h.ID == item.LabelID)
                    txtDMSid2h.Text = lblDMSid2h.Text = item.LabelName;
                else if (lblSocMediaID2h.ID == item.LabelID)
                    txtSocMediaID2h.Text = lblSocMediaID2h.Text = item.LabelName;
                else if (lblContractID2h.ID == item.LabelID)
                    txtContractID2h.Text = lblContractID2h.Text = item.LabelName;
                else if (lblFloorPlanID2h.ID == item.LabelID)
                    txtFloorPlanID2h.Text = lblFloorPlanID2h.Text = item.LabelName;
                else if (lbleMailTemplateID2h.ID == item.LabelID)
                    txteMailTemplateID2h.Text = lbleMailTemplateID2h.Text = item.LabelName;
                else if (lblKeyWord2h.ID == item.LabelID)
                    txtKeyWord2h.Text = lblKeyWord2h.Text = item.LabelName;
                else if (lblRate2h.ID == item.LabelID)
                    txtRate2h.Text = lblRate2h.Text = item.LabelName;
                //else if (lblForcastedVisitor2h.ID == item.LabelID)
                //    txtForcastedVisitor2h.Text = lblForcastedVisitor2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_Event_Detail").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tbl_Event_Detail.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tbl_Event_Detail").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblEventID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventID1s.Text;
                else if (lblMyID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyID1s.Text;
                else if (lblCategoryID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID1s.Text;
                else if (lblEventType1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventType1s.Text;
                else if (lblEventTopic1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventTopic1s.Text;
                else if (lblRegisterationIDBegin1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegisterationIDBegin1s.Text;
                else if (lblTitle2Print1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2Print1s.Text;
                else if (lblRegURL1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegURL1s.Text;
                else if (lblFromDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFromDate1s.Text;
                else if (lblToDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToDate1s.Text;
                else if (lblDescription11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription11s.Text;
                else if (lblDescription21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription21s.Text;
                else if (lblDescription31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription31s.Text;
                else if (lblDMSid1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDMSid1s.Text;
                else if (lblSocMediaID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSocMediaID1s.Text;
                else if (lblContractID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContractID1s.Text;
                else if (lblFloorPlanID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFloorPlanID1s.Text;
                else if (lbleMailTemplateID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txteMailTemplateID1s.Text;
                else if (lblKeyWord1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeyWord1s.Text;
                else if (lblRate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRate1s.Text;
                //else if (lblForcastedVisitor1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtForcastedVisitor1s.Text;

                else if (lblEventID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventID2h.Text;
                else if (lblMyID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMyID2h.Text;
                else if (lblCategoryID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCategoryID2h.Text;
                else if (lblEventType2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventType2h.Text;
                else if (lblEventTopic2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEventTopic2h.Text;
                else if (lblRegisterationIDBegin2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegisterationIDBegin2h.Text;
                else if (lblTitle2Print2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTitle2Print2h.Text;
                else if (lblRegURL2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRegURL2h.Text;
                else if (lblFromDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFromDate2h.Text;
                else if (lblToDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtToDate2h.Text;
                else if (lblDescription12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription12h.Text;
                else if (lblDescription22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription22h.Text;
                else if (lblDescription32h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDescription32h.Text;
                else if (lblDMSid2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDMSid2h.Text;
                else if (lblSocMediaID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSocMediaID2h.Text;
                else if (lblContractID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtContractID2h.Text;
                else if (lblFloorPlanID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtFloorPlanID2h.Text;
                else if (lbleMailTemplateID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txteMailTemplateID2h.Text;
                else if (lblKeyWord2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtKeyWord2h.Text;
                else if (lblRate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRate2h.Text;
                //else if (lblForcastedVisitor2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtForcastedVisitor2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tbl_Event_Detail.xml"));

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
            drpEventID.Enabled = true;
            //drpMyID.Enabled = true;
            txtCategoryID.Enabled = true;
            drpEventType.Enabled = true;
            drpEventTopic.Enabled = true;
            txtRegisterationIDBegin.Enabled = true;
            txtRegNO.Enabled = true;
            txtTitle2Print.Enabled = true;
            txtRegURL.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtDescription1.Enabled = true;
            txtDescription2.Enabled = true;
            txtDescription3.Enabled = true;
            txtDMSid.Enabled = true;
            drpSocMediaID.Enabled = true;
            drpContractID.Enabled = true;
            drpFloorPlanID.Enabled = true;
            drpeMailTemplateID.Enabled = true;
            txtKeyWord.Enabled = true;
            txtRate.Enabled = true;
            txtForcastedVisitor.Enabled = true;
            //cbDeleted.Enabled = true;
            //cbActivated.Enabled = true;
            //txtActivatedDate.Enabled = true;
            //txtActivatedBy.Enabled = true;
            //drpCreatedBy.Enabled = true;
            //txtCreatedDate.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drpEventID.Enabled = false;
            //drpMyID.Enabled = false;
            txtCategoryID.Enabled = false;
            drpEventType.Enabled = false;
            drpEventTopic.Enabled = false;
            txtRegisterationIDBegin.Enabled = false;
            txtRegNO.Enabled = false;
            txtTitle2Print.Enabled = false;
            txtRegURL.Enabled = false;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            txtDescription1.Enabled = false;
            txtDescription2.Enabled = false;
            txtDescription3.Enabled = false;
            txtDMSid.Enabled = false;
            drpSocMediaID.Enabled = false;
            drpContractID.Enabled = false;
            drpFloorPlanID.Enabled = false;
            drpeMailTemplateID.Enabled = false;
            txtKeyWord.Enabled = false;
            txtRate.Enabled = false;
            txtForcastedVisitor.Enabled = false;
            //cbDeleted.Enabled = false;
            //cbActivated.Enabled = false;
            //txtActivatedDate.Enabled = false;
            //txtActivatedBy.Enabled = false;
            //drpCreatedBy.Enabled = false;
            //txtCreatedDate.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Event_Detail.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Detail.OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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

            ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbl_Event_Detail.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Detail.OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
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
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.tbl_Event_Detail.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Detail.OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Event_Detail.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Detail.OrderBy(m => m.EventID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            //FirstData();
        }
        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {


            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "btnDelete")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Eventid = Convert.ToInt32(ID[0]);
                int Myid = Convert.ToInt32(ID[1]);
                //int ID = Convert.ToInt32(e.CommandArgument);

                Database.tbl_Event_Detail objSOJobDesc = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == Eventid && p.MyID == Myid);
                objSOJobDesc.Deleted = false;
                DB.SaveChanges();
                BindData();

            }

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Eventid = Convert.ToInt32(ID[0]);
                int Myid = Convert.ToInt32(ID[1]);

                Database.tbl_Event_Detail objtbl_Event_Detail = DB.tbl_Event_Detail.Single(p => p.TenantID == TID && p.EventID == Eventid && p.MyID == Myid);
                drpEventID.SelectedValue = objtbl_Event_Detail.EventID.ToString();
                txtSubEventID.Text = objtbl_Event_Detail.MyID.ToString();
                txtCategoryID.Text = objtbl_Event_Detail.CategoryID.ToString();
                drpEventType.SelectedValue = objtbl_Event_Detail.EventType.ToString();
                drpEventTopic.SelectedValue = objtbl_Event_Detail.EventTopic.ToString();
                txtRegisterationIDBegin.Text = objtbl_Event_Detail.RegisterationIDBegin.ToString();
                txtRegNO.Text = objtbl_Event_Detail.RegisterationNumber.ToString();
                txtTitle2Print.Text = objtbl_Event_Detail.Title2Print.ToString();
                txtRegURL.Text = objtbl_Event_Detail.RegURL.ToString();
                txtFromDate.Text = Convert.ToDateTime(objtbl_Event_Detail.FromDate).ToShortDateString();
                txtToDate.Text = Convert.ToDateTime(objtbl_Event_Detail.ToDate).ToShortDateString();
                txtDescription1.Text = objtbl_Event_Detail.Description1.ToString();
                txtDescription2.Text = objtbl_Event_Detail.Description2.ToString();
                txtDescription3.Text = objtbl_Event_Detail.Description3.ToString();
                txtDMSid.Text = objtbl_Event_Detail.DMSid.ToString();
                int viewStateSocialID = Convert.ToInt32(objtbl_Event_Detail.SocMediaID);
                int ViewStateFloorID = Convert.ToInt32(objtbl_Event_Detail.FloorPlanID);
                int ViewStateEmailID = Convert.ToInt32(objtbl_Event_Detail.eMailTemplateID);
                //drpSocMediaID.Text = objtbl_Event_Detail.SocMediaID.ToString();
                //drpContractID.SelectedValue = objtbl_Event_Detail.ContractID.ToString();
                //drpFloorPlanID.SelectedValue = objtbl_Event_Detail.FloorPlanID.ToString();
                //drpeMailTemplateID.SelectedValue = objtbl_Event_Detail.eMailTemplateID.ToString();
                txtKeyWord.Text = objtbl_Event_Detail.KeyWord.ToString();
                txtRate.Text = objtbl_Event_Detail.Rate.ToString();
                txtForcastedVisitor.Text = objtbl_Event_Detail.ForcastedVisitor.ToString();
                //cbDeleted.Checked = (objtbl_Event_Detail.Deleted == true) ? true : false;
                //cbActivated.Checked = (objtbl_Event_Detail.Activated == true) ? true : false;
                //txtActivatedDate.Text = objtbl_Event_Detail.ActivatedDate.ToString();
                //txtActivatedBy.Text = objtbl_Event_Detail.ActivatedBy.ToString();
                //drpCreatedBy.SelectedValue = objtbl_Event_Detail.CreatedBy.ToString();
                //txtCreatedDate.Text = objtbl_Event_Detail.CreatedDate.ToString();
                if (objtbl_Event_Detail.Activated == true)
                {
                    btnActivate.Text = "Deactivate";
                    btnRegister.Enabled = true;
                    lblActivatedStatusDate.Text = "Activated Status and Date / Time " + Convert.ToDateTime(objtbl_Event_Detail.ActivatedDate).ToShortDateString() + " by " + objtbl_Event_Detail.ActivatedBy.ToString() + ". ";
                }
                else
                {
                    btnActivate.Text = "Activate";
                    if (objtbl_Event_Detail.Activated == false)
                        lblActivatedStatusDate.Text = "Deactivated Status and Date / Time " + Convert.ToDateTime(objtbl_Event_Detail.ActivatedDate).ToShortDateString() + " by " + objtbl_Event_Detail.ActivatedBy.ToString() + ". ";
                }
                btnActivate.Enabled = true;
                btnAdd.Text = "Update";
                ViewState["Edit"] = Myid;
                ViewState["EVENTID"] = Eventid;
                ViewState["MYID"] = Myid;

                ViewState["Social"] = viewStateSocialID;//for socialMedia
                ViewState["Floor"] = ViewStateFloorID;//for FloorPlane
                ViewState["Email"] = ViewStateEmailID;//for EmailTemplate
                SoiclMediya(viewStateSocialID);//bind Listview socialMedia
                FloorPlan(ViewStateFloorID);//bind Listview FloorPlane
                EmailTemplate(ViewStateEmailID);//bind Listview EmailTemplate
                pnlsocialmedia.Enabled = true;
                Write();
            }



        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.tbl_Event_Detail.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Event_Detail.OrderBy(m => m.EventID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
        protected void drpEventID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtevename.Text = drpEventID.SelectedItem.ToString();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = 0;
            int MEID = Convert.ToInt32(drpEventID.SelectedValue);
            List<Database.tbl_Event_Detail> List = DB.tbl_Event_Detail.Where(a => a.EventID == MEID && a.TenantID == TID).OrderBy(m => m.EventID).ToList(); ;
            Totalrec = List.Count();



            ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (ViewState["Edit"] != null && ViewState["EVENTID"] != null)
            {
                int SEID = Convert.ToInt32(ViewState["Edit"]);
                int MEID = Convert.ToInt32(ViewState["EVENTID"]);
                Response.Redirect("Tbl_Event_Register.aspx?SubEventID=" + SEID + "&MEID=" + MEID);
            }
        }
        protected void btnActivate_Click(object sender, EventArgs e)
        {


            if (ViewState["Edit"] != null && ViewState["EVENTID"] != null)
            {
                int SID = Convert.ToInt32(ViewState["Edit"]);
                int MID = Convert.ToInt32(ViewState["EVENTID"]);
                Database.tbl_Event_Detail objTbl_Event_Detail = DB.tbl_Event_Detail.Single(p => p.MyID == SID && p.EventID == MID && p.EventID == MID && p.TenantID == TID);

                objTbl_Event_Detail.ActivatedDate = DateTime.Now;
                objTbl_Event_Detail.ActivatedBy = ((USER_MST)Session["USER"]).FIRST_NAME;
                if (btnActivate.Text == "Activate")
                {
                    objTbl_Event_Detail.Activated = true;
                    btnActivate.Text = "Deactivate";
                    lblActivatedStatusDate.Text = "Activated Status and Date / Time " + DateTime.Now + " by " + ((USER_MST)Session["USER"]).FIRST_NAME + ". ";
                    btnRegister.Enabled = true;
                }
                else
                {
                    objTbl_Event_Detail.Activated = false;
                    btnRegister.Enabled = false;
                    btnActivate.Text = "Activate";
                    lblActivatedStatusDate.Text = "Deactivated Status and Date / Time " + DateTime.Now + " by " + ((USER_MST)Session["USER"]).FIRST_NAME + ". ";
                }
            }
            else
            {
                btnRegister.Enabled = false;
                btnActivate.Text = "Activate";
                lblActivatedStatusDate.Text = "Deactivated Status and Date / Time " + DateTime.Now + " by " + ((USER_MST)Session["USER"]).FIRST_NAME + ". ";
            }
            DB.SaveChanges();
            BindData();
        }
        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton btnRegister = (LinkButton)e.Item.FindControl("btnRegister");
                Label lblhideMyID = (Label)e.Item.FindControl("lblhideMyID");
                Label lblhideEventID = (Label)e.Item.FindControl("lblhideEventID");
                int SEID = Convert.ToInt32(lblhideMyID.Text);
                int MEID = Convert.ToInt32(lblhideEventID.Text);
                if (DB.tbl_Event_Detail.Where(a => a.MyID == SEID && a.EventID == MEID && a.Activated == true && a.TenantID == TID).Count() > 0)
                {
                    btnRegister.Visible = true;
                }
                else
                {
                    btnRegister.Visible = false;
                }
            }
        }
        public string getEventType(int ID)
        {
            return Classes.EcommAdminClass.getDataREFTABLE(TID).Single(P => P.REFTYPE == "EVN" && P.REFSUBTYPE == "EventType" && P.REFID == ID).REFNAME1;
        }
        public string getEventTopic(int ID)
        {
            return Classes.EcommAdminClass.getDataREFTABLE(TID).Single(P => P.REFTYPE == "EVN" && P.REFSUBTYPE == "EventTopic" && P.REFID == ID).REFNAME1;
        }

        protected void linkAddSocial_Click(object sender, EventArgs e)
        {
            lblerrorSocialMSG.Visible = false;
            int EventID = Convert.ToInt32(drpEventID.SelectedValue);
            //int subID = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Count() > 0 ? Convert.ToInt32(DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Max(p => p.MyID) + 1) : 1;
            //string Concat = EventID.ToString() + "" + subID.ToString();
            if (ViewState["Social"] != null)
            {
                int EventSub = Convert.ToInt32(ViewState["Social"]);
                int SID = Convert.ToInt32(drpSocMediaID.SelectedValue);
                var exist = DB.Tbl_RecordType_Mst.Where(c => c.RecValue == txtSocial.Text && c.Recource == SID && c.TenentID == TID && c.CompniyAndContactID == EventSub);
                if (EventID != 0)
                {
                    if (exist.Count() <= 0)
                    {

                        Tbl_RecordType_Mst obj = new Tbl_RecordType_Mst();
                        obj.TenentID = TID;
                        obj.RecordType = "socialmedia";
                        obj.RecTypeID = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                        obj.CompniyAndContactID = EventSub;
                        obj.RecourceName = "Event";
                        obj.Recource = SID;
                        obj.RunSerial = 1;
                        obj.RecValue = txtSocial.Text;
                        obj.Active = true;
                        // obj.Rremark = "AutomatedProcess";
                        DB.Tbl_RecordType_Mst.AddObject(obj);
                        DB.SaveChanges();
                        SoiclMediya(EventSub);
                    }
                }
                else
                {
                    lblerrorSocialMSG.Visible = true;
                    lblerrorSocialMSG.Text = "Please Select DropDown Event Name";
                }
            }
        }
        public string getshocial(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID && p.TenentID == TID).REFNAME1;
        }
        public string getremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID && p.TenentID == TID).Remarks;
        }
        public void SoiclMediya(int CID)
        {
            listSocialMedia.DataSource = DB.Tbl_RecordType_Mst.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Active == true && p.RecordType == "socialmedia");
            listSocialMedia.DataBind();
        }

        protected void linkFloorPlan_Click(object sender, EventArgs e)
        {
            //tbl_event_floorPlan           
            lblErrorFloorMSG.Visible = false;
            int EventID = Convert.ToInt32(drpEventID.SelectedValue);
            //int subID = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Count() > 0 ? Convert.ToInt32(DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Max(p => p.MyID) + 1) : 1;
            //string Concat = EventID.ToString() + "" + subID.ToString();
            if (ViewState["Floor"] != null)
            {
                int EventSub = Convert.ToInt32(ViewState["Floor"]);
                int SID = Convert.ToInt32(drpFloorPlanID.SelectedValue);
                var exist = DB.tbl_event_floorPlan.Where(c => c.RecValue == txtFloorPlan.Text && c.Recource == SID && c.TenentID == TID && c.CompniyAndContactID == EventSub);
                if (EventID != 0)
                {
                    if (exist.Count() <= 0)
                    {

                        tbl_event_floorPlan obj = new tbl_event_floorPlan();
                        obj.TenentID = TID;
                        obj.RecordType = "FloorPlan";
                        obj.RecTypeID = DB.tbl_event_floorPlan.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_event_floorPlan.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                        obj.CompniyAndContactID = EventSub;
                        obj.RecourceName = "Event";
                        obj.Recource = SID;
                        obj.RunSerial = 1;
                        obj.RecValue = txtFloorPlan.Text;
                        obj.Active = true;
                        // obj.Rremark = "AutomatedProcess";
                        DB.tbl_event_floorPlan.AddObject(obj);
                        DB.SaveChanges();
                        FloorPlan(EventSub);
                    }
                }
                else
                {
                    lblErrorFloorMSG.Visible = false;
                    lblErrorFloorMSG.Text = "Please Select DropDown Event Name";
                }
            }
        }
        public void FloorPlan(int CID)
        {
            ListFloorPlane.DataSource = DB.tbl_event_floorPlan.Where(p => p.TenentID == TID && p.CompniyAndContactID == CID && p.Active == true && p.RecordType == "FloorPlan");
            ListFloorPlane.DataBind();
        }
        public string getsFloor(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID && p.TenentID == TID).REFNAME1;
        }
        public string getFloorremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID && p.TenentID == TID).Remarks;
        }

        protected void LinkemailTemplate_Click(object sender, EventArgs e)
        {
            lblemailErrorMSG.Visible = false;
            int EventID = Convert.ToInt32(drpEventID.SelectedValue);
            //int subID = DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Count() > 0 ? Convert.ToInt32(DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.EventID == EventID).Max(p => p.MyID) + 1) : 1;
            //string Concat = EventID.ToString() + "" + subID.ToString();
            if (ViewState["Email"] != null)
            {
                int EventSub = Convert.ToInt32(ViewState["Email"]);
                int SID = Convert.ToInt32(drpeMailTemplateID.SelectedValue);
                var exist = DB.tbl_event_eMailTemplate.Where(c => c.RecValue == txtemailtemplate.Text && c.Recource == SID && c.TenentID == TID && c.CompniyAndContactID == EventSub);
                if (EventID != 0)
                {
                    if (exist.Count() <= 0)
                    {
                        tbl_event_eMailTemplate obj = new tbl_event_eMailTemplate();
                        obj.TenentID = TID;
                        obj.RecordType = "eMailTemplate";
                        obj.RecTypeID = DB.tbl_event_eMailTemplate.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_event_eMailTemplate.Where(p => p.TenentID == TID).Max(p => p.RecTypeID) + 1) : 1;
                        obj.CompniyAndContactID = EventSub;
                        obj.RecourceName = "Event";
                        obj.Recource = SID;
                        obj.RunSerial = 1;
                        obj.RecValue = txtemailtemplate.Text;
                        obj.Active = true;
                        // obj.Rremark = "AutomatedProcess";
                        DB.tbl_event_eMailTemplate.AddObject(obj);
                        DB.SaveChanges();
                        EmailTemplate(EventSub);
                    }
                }
                else
                {
                    lblemailErrorMSG.Visible = true;
                    lblemailErrorMSG.Text = "Please Select DropDown Event Name";
                }
            }
        }
        public void EmailTemplate(int ETID)
        {
            ListEmailTempate.DataSource = DB.tbl_event_eMailTemplate.Where(p => p.TenentID == TID && p.CompniyAndContactID == ETID && p.Active == true && p.RecordType == "eMailTemplate");
            ListEmailTempate.DataBind();
        }
        public string getsTemplate(int SMID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == SMID && p.TenentID == TID).REFNAME1;
        }
        public string getTemplateremark(int RID)
        {
            return DB.REFTABLEs.SingleOrDefault(p => p.REFID == RID && p.TenentID == TID).Remarks;
        }

        protected void txtRegisterationIDBegin_TextChanged(object sender, EventArgs e)
        {
            string Registration = txtRegisterationIDBegin.Text;
            if (Registration != "")
            {
                if (DB.tbl_Event_Detail.Where(p => p.TenantID == TID && p.RegisterationIDBegin.ToUpper() == Registration.ToUpper()).Count() > 0)
                {
                    txtRegisterationIDBegin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#f44242");
                }
                else
                {
                    txtRegisterationIDBegin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#111111");
                }
            }
        }

    }
}