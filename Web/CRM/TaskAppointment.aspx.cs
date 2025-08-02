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

namespace Web.CRM
{
    public partial class TaskAppointment : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                Session["LANGUAGE"] = "en-US";
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                btnAdd.ValidationGroup = "s";
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();

                if (DB.tbl_Task.Count() > 0)
                {
                    //FirstData();
                }
                string LogginID = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
                //txtowner.Text = Session["LoginID"].ToString();
                txtowner.Text = LogginID;
            }
        }
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Listview1.DataSource = DB.tbl_Task.Where(p => p.Active == true && p.TenentID == TID);
            Listview1.DataBind();
        }
        public void GetShow()
        {

            lblSubject1s.Attributes["class"] = "control-label col-md-2  getshow";
            lblReminderDate1s.Attributes["class"] = lblTaskStatus1s.Attributes["class"] = lblStartingDate1s.Attributes["class"] = lblDueDate1s.Attributes["class"] = lblPriority1s.Attributes["class"] = lblCompletedPerctange1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblSubject2h.Attributes["class"] = "control-label col-md-2  gethide";
            lblReminderDate2h.Attributes["class"] = lblTaskStatus2h.Attributes["class"] = lblStartingDate2h.Attributes["class"] = lblDueDate2h.Attributes["class"] = lblPriority2h.Attributes["class"] = lblCompletedPerctange2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblSubject1s.Attributes["class"] = "control-label col-md-2  gethide";
            lblReminderDate1s.Attributes["class"] = lblTaskStatus1s.Attributes["class"] = lblStartingDate1s.Attributes["class"] = lblDueDate1s.Attributes["class"] = lblPriority1s.Attributes["class"] = lblCompletedPerctange1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblSubject2h.Attributes["class"] = "control-label col-md-2  getshow";
            lblReminderDate2h.Attributes["class"] = lblTaskStatus2h.Attributes["class"] = lblStartingDate2h.Attributes["class"] = lblDueDate2h.Attributes["class"] = lblPriority2h.Attributes["class"] = lblCompletedPerctange2h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.tbl_Task objtbl_Task = DB.tbl_Task.Single(p => p.TaskID == ID);
                objtbl_Task.Active = false;
                DB.SaveChanges();
                BindData();

            }
            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.tbl_Task objtbl_Task = DB.tbl_Task.Single(p => p.TaskID == ID);
                //drpLocationID.SelectedValue = objtbl_Task.LocationID.ToString();
                //drpTaskID.SelectedValue = objtbl_Task.TaskID.ToString();
                //drpCerticateNo.SelectedValue = objtbl_Task.CerticateNo.ToString();
                //drpForEmp_ID.SelectedValue = objtbl_Task.ForEmp_ID.ToString();
                drpactivity.SelectedValue = objtbl_Task.ActivityId.ToString();
                drpproject.SelectedValue = objtbl_Task.ProjectId.ToString();
                drptofield.SelectedValue = objtbl_Task.PerformingEmp_ID.ToString();
                txtSubject.Text = objtbl_Task.Subject.ToString();
                txtStartingDate.Text = objtbl_Task.StartingDate.ToString();
                drptasktype.SelectedValue = objtbl_Task.TaskType.ToString();
                drpTaskStatus.SelectedValue = objtbl_Task.TaskStatus.ToString();
                txtDueDate.Text = objtbl_Task.DueDate.ToString();
                drpPriority.SelectedValue = objtbl_Task.Priority.ToString();
                //txtActualCompletionDate.Text = objtbl_Task.ActualCompletionDate.ToString();
                drpCompletedPerctange.SelectedValue = objtbl_Task.CompletedPerctange.ToString();
                txtReminderDate.Text = objtbl_Task.ReminderDate.ToString();
                txtReminder.Text = objtbl_Task.Remarks.ToString();
                //txtCategories.Text = objtbl_Task.Categories.ToString();
                //txtFollowUp.Text = objtbl_Task.FollowUp.ToString();
                //txtCustomFollowUpStartDate.Text = objtbl_Task.CustomFollowUpStartDate.ToString();
                //txtCustomFollowUpEndDate.Text = objtbl_Task.CustomFollowUpEndDate.ToString();
                //txtCustomFollowUpReminderDate.Text = objtbl_Task.CustomFollowUpReminderDate.ToString();
                //drpForwardToEmp_ID.SelectedValue = objtbl_Task.ForwardToEmp_ID.ToString();
                //drpOccurance_ID.SelectedValue = objtbl_Task.Occurance_ID.ToString();
                //txtRemarks.Text = objtbl_Task.Remarks.ToString();
                //cbActive.Checked = (objtbl_Task.Active == true) ? true : false;
                //txtCruipID.Text = objtbl_Task.CruipID.ToString();

                btnAdd.Text = "Update";
                ViewState["Edit"] = ID;
                btnassingtask.Visible = true;
                Write();
            }
        }

        public void Clear()
        {
            //drpLocationID.SelectedIndex = 0;
            //drpTaskID.SelectedIndex = 0;
            //drpCerticateNo.SelectedIndex = 0;
            //drpForEmp_ID.SelectedIndex = 0;
            //drpPerformingEmp_ID.SelectedIndex = 0;
            txtSubject.Text = "";
            txtStartingDate.Text = "";
            drptasktype.SelectedIndex = 0;
            drpTaskStatus.SelectedIndex = 0;
            txtDueDate.Text = "";
            drpPriority.SelectedIndex = 0;
            //txtActualCompletionDate.Text = "";
            drpCompletedPerctange.SelectedIndex = 0;
            txtReminderDate.Text = "";
            // txtReminderTime.SelectedIndex = 0;
            //txtCategories.Text = "";
            //txtFollowUp.Text = "";
            //txtCustomFollowUpStartDate.Text = "";
            //txtCustomFollowUpEndDate.Text = "";
            //txtCustomFollowUpReminderDate.Text = "";
            //drpForwardToEmp_ID.SelectedIndex = 0;
            //drpOccurance_ID.SelectedIndex = 0;
            //txtRemarks.Text = "";
            //txtActive.Text = "";
            //txtCruipID.Text = "";

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "AddNew")
            {

                Write();
                Clear();
                btnAdd.Text = "Add";
                btnAdd.ValidationGroup = "submit";
            }
            else if (btnAdd.Text == "Add")
            {
                Database.tbl_Task objtbl_Task = new Database.tbl_Task();
                //Server Content Send data Yogesh
                //objtbl_Task.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                objtbl_Task.TaskID = DB.tbl_Task.Count() > 0 ? DB.tbl_Task.Max(p => p.TaskID) + 1 : 1;
                //objtbl_Task.CerticateNo = Convert.ToInt32(drpCerticateNo.SelectedValue);
                //objtbl_Task.ForEmp_ID = Convert.ToInt32(drpForEmp_ID.SelectedValue);
                objtbl_Task.ActivityId = drpactivity.SelectedValue;
                objtbl_Task.ProjectId = drpproject.SelectedValue;
                objtbl_Task.PerformingEmp_ID = Convert.ToInt32(drptofield.SelectedValue);
                objtbl_Task.Subject = txtSubject.Text;
                objtbl_Task.StartingDate = Convert.ToDateTime(txtStartingDate.Text);
                objtbl_Task.TenentID = 1;
                objtbl_Task.LocationID = 1;
                objtbl_Task.CerticateNo = 1;
                objtbl_Task.TaskType = drptasktype.SelectedValue;
                objtbl_Task.TaskStatus = drpTaskStatus.SelectedValue;
                objtbl_Task.DueDate = Convert.ToDateTime(txtDueDate.Text);
                objtbl_Task.Priority = drpPriority.SelectedValue;
                //objtbl_Task.ActualCompletionDate = Convert.ToDateTime(txtActualCompletionDate.Text);
                objtbl_Task.CompletedPerctange = Convert.ToInt32(drpCompletedPerctange.SelectedValue);
                objtbl_Task.ReminderDate = Convert.ToDateTime(txtReminderDate.Text);
                // objtbl_Task.ReminderTime = Convert.ToDateTime(txtReminderTime.SelectedValue);
                //objtbl_Task.Categories = txtCategories.Text;
                //objtbl_Task.FollowUp = txtFollowUp.Text;
                //objtbl_Task.CustomFollowUpStartDate = Convert.ToDateTime(txtCustomFollowUpStartDate.Text);
                //objtbl_Task.CustomFollowUpEndDate = Convert.ToDateTime(txtCustomFollowUpEndDate.Text);
                //objtbl_Task.CustomFollowUpReminderDate = Convert.ToDateTime(txtCustomFollowUpReminderDate.Text);
                //objtbl_Task.ForwardToEmp_ID = Convert.ToInt32(drpForwardToEmp_ID.SelectedValue);
                //objtbl_Task.Occurance_ID = Convert.ToInt32(drpOccurance_ID.SelectedValue);
                objtbl_Task.Remarks = txtReminder.Text;
                objtbl_Task.Active = true;
                //objtbl_Task.CruipID = txtCruipID.Text;


                DB.tbl_Task.AddObject(objtbl_Task);
                DB.SaveChanges();
                Clear();
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
                    Database.tbl_Task objtbl_Task = DB.tbl_Task.Single(p => p.TaskID == ID);
                    //objtbl_Task.LocationID = Convert.ToInt32(drpLocationID.SelectedValue);
                    //objtbl_Task.TaskID = Convert.ToInt32(drpTaskID.SelectedValue);
                    //objtbl_Task.CerticateNo = Convert.ToInt32(drpCerticateNo.SelectedValue);
                    //objtbl_Task.ForEmp_ID = Convert.ToInt32(drpForEmp_ID.SelectedValue);
                    //objtbl_Task.PerformingEmp_ID = Convert.ToInt32(drpPerformingEmp_ID.SelectedValue);
                    objtbl_Task.ActivityId = drpactivity.SelectedValue;
                    objtbl_Task.ProjectId = drpproject.SelectedValue;
                    objtbl_Task.PerformingEmp_ID = Convert.ToInt32(drptofield.SelectedValue);
                    objtbl_Task.Subject = txtSubject.Text;
                    objtbl_Task.StartingDate = Convert.ToDateTime(txtStartingDate.Text);
                    objtbl_Task.TaskType = drptasktype.SelectedValue;
                    objtbl_Task.TaskStatus = drpTaskStatus.SelectedValue;
                    objtbl_Task.DueDate = Convert.ToDateTime(txtDueDate.Text);
                    objtbl_Task.Priority = drpPriority.SelectedValue;
                    //objtbl_Task.ActualCompletionDate = Convert.ToDateTime(txtActualCompletionDate.Text);
                    objtbl_Task.CompletedPerctange = Convert.ToInt32(drpCompletedPerctange.Text);
                    objtbl_Task.ReminderDate = Convert.ToDateTime(txtReminderDate.Text);
                    //objtbl_Task.ReminderTime =Convert.ToDateTime( txtReminderTime.Text);
                    //objtbl_Task.Categories = txtCategories.Text;
                    //objtbl_Task.FollowUp = txtFollowUp.Text;
                    //objtbl_Task.CustomFollowUpStartDate = Convert.ToDateTime(txtCustomFollowUpStartDate.Text);
                    //objtbl_Task.CustomFollowUpEndDate = Convert.ToDateTime(txtCustomFollowUpEndDate.Text);
                    //objtbl_Task.CustomFollowUpReminderDate = Convert.ToDateTime(txtCustomFollowUpReminderDate.Text);
                    //objtbl_Task.ForwardToEmp_ID = Convert.ToInt32(drpForwardToEmp_ID.SelectedValue);
                    //objtbl_Task.Occurance_ID = Convert.ToInt32(drpOccurance_ID.SelectedValue);
                    objtbl_Task.Remarks = txtReminder.Text;
                    objtbl_Task.Active = true;
                    //objtbl_Task.CruipID = txtCruipID.Text;

                    ViewState["Edit"] = null;
                    btnAdd.Text = "AddNew";
                    DB.SaveChanges();

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

            //scope.Complete(); //  To commit.

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        public void FillContractorID()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            //  int UID=Convert .ToInt32 (Session["USER_ID"].USER_ID);
            drptofield.DataSource = DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID != UID);
            drptofield.DataTextField = "LOGIN_ID";
            drptofield.DataValueField = "USER_ID";
            drptofield.DataBind();
            drptofield.Items.Insert(0, new ListItem("--Select--", "0"));


            drpactivity.DataSource = DB.HRMMainActivities.Where(p => p.TenentID == 1 && p.ACTIVITYTYPE == "HRM").OrderBy(a => a.ACTIVITYE);
            drpactivity.DataTextField = "ACTIVITYE";
            drpactivity.DataValueField = "ACTIVITYE";
            drpactivity.DataBind();
            drpactivity.Items.Insert(0, new ListItem("--Select--", "0"));


            drpproject.DataSource = DB.tbl_Project_old.Where(p => p.Active == true);
            drpproject.DataTextField = "name";
            drpproject.DataValueField = "ID";
            drpproject.DataBind();
            drpproject.Items.Insert(0, new ListItem("--Select--", "0"));


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
            //drpLocationID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            //drpTaskID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            //drpCerticateNo.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            //drpForEmp_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            //drpPerformingEmp_ID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
            if (Listview1.SelectedDataKey[6] != null)
                txtSubject.Text = Listview1.SelectedDataKey[6].ToString();
            DateTime dt = Convert.ToDateTime(Listview1.SelectedDataKey[7].ToString());
            txtStartingDate.Text = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
            //txtTaskType.Text = Listview1.SelectedDataKey[8].ToString();
            drpTaskStatus.SelectedValue = Listview1.SelectedDataKey[9].ToString();
            DateTime dt1 = Convert.ToDateTime(Listview1.SelectedDataKey[10].ToString());
            txtDueDate.Text = dt1.ToString("MM/dd/yyyy hh:mm:ss tt");
            drpPriority.SelectedValue = Listview1.SelectedDataKey[11].ToString();
            //txtActualCompletionDate.Text = Listview1.SelectedDataKey[12].ToString();
            drpCompletedPerctange.Text = Listview1.SelectedDataKey[13].ToString();
            DateTime rdt = Convert.ToDateTime(Listview1.SelectedDataKey[14].ToString());
            txtReminderDate.Text = rdt.ToString("MM/dd/yyyy hh:mm:ss tt");
            DateTime rtm = Convert.ToDateTime(Listview1.SelectedDataKey[15]);
            // txtReminderTime.Text = rtm.ToString("dd/MM/yyyy");
            //txtCategories.Text = Listview1.SelectedDataKey[16].ToString();
            //txtFollowUp.Text = Listview1.SelectedDataKey[17].ToString();
            //DateTime dt2 = Convert.ToDateTime( Listview1.SelectedDataKey[18].ToString());
            //txtCustomFollowUpStartDate.Text = dt2.ToString("dd/MM/yyyy");
            //DateTime dt3 = Convert.ToDateTime(Listview1.SelectedDataKey[19].ToString());
            //txtCustomFollowUpEndDate.Text = dt3.ToString("dd/MM/yyyy");
            //DateTime dt4 = Convert.ToDateTime(Listview1.SelectedDataKey[20].ToString());
            //txtCustomFollowUpReminderDate.Text = dt4.ToString("dd/MM/yyyy");
            //drpForwardToEmp_ID.SelectedValue = Listview1.SelectedDataKey[21].ToString();
            //drpOccurance_ID.SelectedValue = Listview1.SelectedDataKey[22].ToString();
            txtReminder.Text = Listview1.SelectedDataKey[23].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtCruipID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpLocationID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                //drpTaskID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                //drpCerticateNo.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                //drpForEmp_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                //drpPerformingEmp_ID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                txtSubject.Text = Listview1.SelectedDataKey[6].ToString();
                DateTime dt = Convert.ToDateTime(Listview1.SelectedDataKey[7].ToString());
                txtStartingDate.Text = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
                //txtTaskType.Text = Listview1.SelectedDataKey[8].ToString();
                drpTaskStatus.SelectedValue = Listview1.SelectedDataKey[9].ToString();
                DateTime dt1 = Convert.ToDateTime(Listview1.SelectedDataKey[10].ToString());
                txtDueDate.Text = dt1.ToString("MM/dd/yyyy hh:mm:ss tt");
                drpPriority.SelectedValue = Listview1.SelectedDataKey[11].ToString();
                //txtActualCompletionDate.Text = Listview1.SelectedDataKey[12].ToString();
                drpCompletedPerctange.Text = Listview1.SelectedDataKey[13].ToString();
                DateTime rdt = Convert.ToDateTime(Listview1.SelectedDataKey[14].ToString());
                txtReminderDate.Text = rdt.ToString("MM/dd/yyyy hh:mm:ss tt");
                // DateTime rtm = Convert.ToDateTime(Listview1.SelectedDataKey[15]);
                // txtReminderTime.Text = rtm.ToString("MM/dd/yyyy hh:mm:ss tt");
                //txtCategories.Text = Listview1.SelectedDataKey[16].ToString();
                //txtFollowUp.Text = Listview1.SelectedDataKey[17].ToString();
                //DateTime dt2 = Convert.ToDateTime( Listview1.SelectedDataKey[18].ToString());
                //txtCustomFollowUpStartDate.Text = dt2.ToString("MM/dd/yyyy hh:mm:ss tt");
                //DateTime dt3 = Convert.ToDateTime(Listview1.SelectedDataKey[19].ToString());
                //txtCustomFollowUpEndDate.Text = dt3.ToString("MM/dd/yyyy hh:mm:ss tt");
                //DateTime dt4 = Convert.ToDateTime(Listview1.SelectedDataKey[20].ToString());
                //txtCustomFollowUpReminderDate.Text = dt4.ToString("MM/dd/yyyy hh:mm:ss tt");
                //drpForwardToEmp_ID.SelectedValue = Listview1.SelectedDataKey[21].ToString();
                //drpOccurance_ID.SelectedValue = Listview1.SelectedDataKey[22].ToString();
                txtReminder.Text = Listview1.SelectedDataKey[23].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtCruipID.Text = Listview1.SelectedDataKey[0].ToString();


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
                //drpLocationID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                //drpTaskID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                //drpCerticateNo.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                //drpForEmp_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                //drpPerformingEmp_ID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                txtSubject.Text = Listview1.SelectedDataKey[6].ToString();
                DateTime dt = Convert.ToDateTime(Listview1.SelectedDataKey[7].ToString());
                txtStartingDate.Text = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
                //txtTaskType.Text = Listview1.SelectedDataKey[8].ToString();
                drpTaskStatus.SelectedValue = Listview1.SelectedDataKey[9].ToString();
                DateTime dt1 = Convert.ToDateTime(Listview1.SelectedDataKey[10].ToString());
                txtDueDate.Text = dt1.ToString("MM/dd/yyyy hh:mm:ss tt");
                drpPriority.SelectedValue = Listview1.SelectedDataKey[11].ToString();
                //txtActualCompletionDate.Text = Listview1.SelectedDataKey[12].ToString();
                drpCompletedPerctange.Text = Listview1.SelectedDataKey[13].ToString();
                DateTime rdt = Convert.ToDateTime(Listview1.SelectedDataKey[14].ToString());
                txtReminderDate.Text = rdt.ToString("MM/dd/yyyy hh:mm:ss tt");
                // DateTime rtm = Convert.ToDateTime(Listview1.SelectedDataKey[15]);
                // txtReminderTime.Text = rtm.ToString("MM/dd/yyyy hh:mm:ss tt");
                //txtCategories.Text = Listview1.SelectedDataKey[16].ToString();
                //txtFollowUp.Text = Listview1.SelectedDataKey[17].ToString();
                //DateTime dt2 = Convert.ToDateTime( Listview1.SelectedDataKey[18].ToString());
                //txtCustomFollowUpStartDate.Text = dt2.ToString("MM/dd/yyyy hh:mm:ss tt");
                //DateTime dt3 = Convert.ToDateTime(Listview1.SelectedDataKey[19].ToString());
                //txtCustomFollowUpEndDate.Text = dt3.ToString("MM/dd/yyyy hh:mm:ss tt");
                //DateTime dt4 = Convert.ToDateTime(Listview1.SelectedDataKey[20].ToString());
                //txtCustomFollowUpReminderDate.Text = dt4.ToString("MM/dd/yyyy hh:mm:ss tt");
                //drpForwardToEmp_ID.SelectedValue = Listview1.SelectedDataKey[21].ToString();
                //drpOccurance_ID.SelectedValue = Listview1.SelectedDataKey[22].ToString();
                txtReminder.Text = Listview1.SelectedDataKey[23].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtCruipID.Text = Listview1.SelectedDataKey[0].ToString();


            }
        }
        public void LastData()
        {
            //drpLocationID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            //drpTaskID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            //drpCerticateNo.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            //drpForEmp_ID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            //drpPerformingEmp_ID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
            txtSubject.Text = Listview1.SelectedDataKey[6].ToString();
            DateTime dt = Convert.ToDateTime(Listview1.SelectedDataKey[7].ToString());
            txtStartingDate.Text = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
            //txtTaskType.Text = Listview1.SelectedDataKey[8].ToString();
            drpTaskStatus.SelectedValue = Listview1.SelectedDataKey[9].ToString();
            DateTime dt1 = Convert.ToDateTime(Listview1.SelectedDataKey[10].ToString());
            txtDueDate.Text = dt1.ToString("MM/dd/yyyy hh:mm:ss tt");
            drpPriority.SelectedValue = Listview1.SelectedDataKey[11].ToString();
            //txtActualCompletionDate.Text = Listview1.SelectedDataKey[12].ToString();
            drpCompletedPerctange.Text = Listview1.SelectedDataKey[13].ToString();
            DateTime rdt = Convert.ToDateTime(Listview1.SelectedDataKey[14].ToString());
            txtReminderDate.Text = rdt.ToString("MM/dd/yyyy hh:mm:ss tt");
            //DateTime rtm = Convert.ToDateTime(Listview1.SelectedDataKey[15]);
            // txtReminderTime.Text = rtm.ToString("MM/dd/yyyy hh:mm:ss tt");
            //txtCategories.Text = Listview1.SelectedDataKey[16].ToString();
            //txtFollowUp.Text = Listview1.SelectedDataKey[17].ToString();
            //DateTime dt2 = Convert.ToDateTime( Listview1.SelectedDataKey[18].ToString());
            //txtCustomFollowUpStartDate.Text = dt2.ToString("MM/dd/yyyy hh:mm:ss tt");
            //DateTime dt3 = Convert.ToDateTime(Listview1.SelectedDataKey[19].ToString());
            //txtCustomFollowUpEndDate.Text = dt3.ToString("MM/dd/yyyy hh:mm:ss tt");
            //DateTime dt4 = Convert.ToDateTime(Listview1.SelectedDataKey[20].ToString());
            //txtCustomFollowUpReminderDate.Text = dt4.ToString("MM/dd/yyyy hh:mm:ss tt");
            //drpForwardToEmp_ID.SelectedValue = Listview1.SelectedDataKey[21].ToString();
            //drpOccurance_ID.SelectedValue = Listview1.SelectedDataKey[22].ToString();
            txtReminder.Text = Listview1.SelectedDataKey[23].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtCruipID.Text = Listview1.SelectedDataKey[0].ToString();


        }

        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblReminderDate2h.Visible = lblSubject2h.Visible = lblStartingDate2h.Visible = lblTaskStatus2h.Visible = lblDueDate2h.Visible = lblPriority2h.Visible = lblCompletedPerctange2h.Visible = false;
                    //2true
                    txtReminderDate2h.Visible = txtSubject2h.Visible = txtStartingDate2h.Visible = txtTaskStatus2h.Visible = txtDueDate2h.Visible = txtPriority2h.Visible = txtCompletedPerctange2h.Visible = true;

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
                    lblReminderDate2h.Visible = lblSubject2h.Visible = lblStartingDate2h.Visible = lblTaskStatus2h.Visible = lblDueDate2h.Visible = lblPriority2h.Visible = lblCompletedPerctange2h.Visible = true;
                    //2false
                    txtReminderDate2h.Visible = txtSubject2h.Visible = txtStartingDate2h.Visible = txtTaskStatus2h.Visible = txtDueDate2h.Visible = txtPriority2h.Visible = txtCompletedPerctange2h.Visible = false;

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
                    lblReminderDate1s.Visible = lblSubject1s.Visible = lblStartingDate1s.Visible = lblTaskStatus1s.Visible = lblDueDate1s.Visible = lblPriority1s.Visible = lblCompletedPerctange1s.Visible = false;
                    //1true
                    txtReminderDate1s.Visible = txtSubject1s.Visible = txtStartingDate1s.Visible = txtTaskStatus1s.Visible = txtDueDate1s.Visible = txtPriority1s.Visible = txtCompletedPerctange1s.Visible = true;
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
                    lblReminderDate1s.Visible = lblSubject1s.Visible = lblStartingDate1s.Visible = lblTaskStatus1s.Visible = lblDueDate1s.Visible = lblPriority1s.Visible = lblCompletedPerctange1s.Visible = true;
                    //1false
                    txtReminderDate1s.Visible = txtSubject1s.Visible = txtStartingDate1s.Visible = txtTaskStatus1s.Visible = txtDueDate1s.Visible = txtPriority1s.Visible = txtCompletedPerctange1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }

        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((CRMMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("tbl_Task").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTanentId1s.ID == item.LabelID)
                //    txtTanentId1s.Text = lblTanentId1s.Text = item.LabelName;
                //else if (lblLocationID1s.ID == item.LabelID)
                //    txtLocationID1s.Text = lblLocationID1s.Text =  item.LabelName;
                //else if (lblTaskID1s.ID == item.LabelID)
                //    txtTaskID1s.Text = lblTaskID1s.Text =  item.LabelName;
                //else if (lblCerticateNo1s.ID == item.LabelID)
                //    txtCerticateNo1s.Text = lblCerticateNo1s.Text =  item.LabelName;
                //else if (lblForEmp_ID1s.ID == item.LabelID)
                //    txtForEmp_ID1s.Text = lblForEmp_ID1s.Text =  item.LabelName;
                //else if (lblPerformingEmp_ID1s.ID == item.LabelID)
                //    txtPerformingEmp_ID1s.Text = lblPerformingEmp_ID1s.Text =  item.LabelName;
                if (lblSubject1s.ID == item.LabelID)
                    txtSubject1s.Text = lblSubject1s.Text = lblhSubject.Text = item.LabelName;
                else if (lblStartingDate1s.ID == item.LabelID)
                    txtStartingDate1s.Text = lblStartingDate1s.Text = lblhStartingDate.Text = item.LabelName;
                //else if (lblTaskType1s.ID == item.LabelID)
                //    txtTaskType1s.Text = lblTaskType1s.Text = lblhTaskType.Text = item.LabelName;
                else if (lblTaskStatus1s.ID == item.LabelID)
                    txtTaskStatus1s.Text = lblTaskStatus1s.Text = lblhTaskStatus.Text = item.LabelName;
                else if (lblDueDate1s.ID == item.LabelID)
                    txtDueDate1s.Text = lblDueDate1s.Text = lblhDueDate.Text = item.LabelName;
                else if (lblPriority1s.ID == item.LabelID)
                    txtPriority1s.Text = lblPriority1s.Text = item.LabelName;
                //else if (lblActualCompletionDate1s.ID == item.LabelID)
                //    txtActualCompletionDate1s.Text = lblActualCompletionDate1s.Text =  item.LabelName;
                else if (lblCompletedPerctange1s.ID == item.LabelID)
                    txtCompletedPerctange1s.Text = lblCompletedPerctange1s.Text = lblhCompletedPerctange.Text = item.LabelName;
                else if (lblReminderDate1s.ID == item.LabelID)
                    txtReminderDate1s.Text = lblReminderDate1s.Text = item.LabelName;
                //else if (lblReminderTime1s.ID == item.LabelID)
                //    txtReminderTime1s.Text = lblReminderTime1s.Text = item.LabelName;
                //else if (lblCategories1s.ID == item.LabelID)
                //    txtCategories1s.Text = lblCategories1s.Text = lblhCategories.Text = item.LabelName;
                //else if (lblFollowUp1s.ID == item.LabelID)
                //    txtFollowUp1s.Text = lblFollowUp1s.Text =  item.LabelName;
                //else if (lblCustomFollowUpStartDate1s.ID == item.LabelID)
                //    txtCustomFollowUpStartDate1s.Text = lblCustomFollowUpStartDate1s.Text =  item.LabelName;
                //else if (lblCustomFollowUpEndDate1s.ID == item.LabelID)
                //    txtCustomFollowUpEndDate1s.Text = lblCustomFollowUpEndDate1s.Text =  item.LabelName;
                //else if (lblCustomFollowUpReminderDate1s.ID == item.LabelID)
                //    txtCustomFollowUpReminderDate1s.Text = lblCustomFollowUpReminderDate1s.Text =  item.LabelName;
                //else if (lblForwardToEmp_ID1s.ID == item.LabelID)
                //    txtForwardToEmp_ID1s.Text = lblForwardToEmp_ID1s.Text =  item.LabelName;
                //else if (lblOccurance_ID1s.ID == item.LabelID)
                //    txtOccurance_ID1s.Text = lblOccurance_ID1s.Text = lblhOccurance_ID.Text = item.LabelName;
                //else if (lblRemarks1s.ID == item.LabelID)
                //    txtRemarks1s.Text = lblRemarks1s.Text =  item.LabelName;
                //else if (lblActive1s.ID == item.LabelID)
                //    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblCruipID1s.ID == item.LabelID)
                //    txtCruipID1s.Text = lblCruipID1s.Text =  item.LabelName;

                //else if (lblTanentId2h.ID == item.LabelID)
                //    txtTanentId2h.Text = lblTanentId2h.Text =  item.LabelName;
                //else if (lblLocationID2h.ID == item.LabelID)
                //    txtLocationID2h.Text = lblLocationID2h.Text =  item.LabelName;
                //else if (lblTaskID2h.ID == item.LabelID)
                //    txtTaskID2h.Text = lblTaskID2h.Text =  item.LabelName;
                //else if (lblCerticateNo2h.ID == item.LabelID)
                //    txtCerticateNo2h.Text = lblCerticateNo2h.Text =  item.LabelName;
                //else if (lblForEmp_ID2h.ID == item.LabelID)
                //    txtForEmp_ID2h.Text = lblForEmp_ID2h.Text =  item.LabelName;
                //else if (lblPerformingEmp_ID2h.ID == item.LabelID)
                //    txtPerformingEmp_ID2h.Text = lblPerformingEmp_ID2h.Text =  item.LabelName;
                else if (lblSubject2h.ID == item.LabelID)
                    txtSubject2h.Text = lblSubject2h.Text = lblhSubject.Text = item.LabelName;
                else if (lblStartingDate2h.ID == item.LabelID)
                    txtStartingDate2h.Text = lblStartingDate2h.Text = lblhStartingDate.Text = item.LabelName;
                //else if (lblTaskType2h.ID == item.LabelID)
                //    txtTaskType2h.Text = lblTaskType2h.Text = lblhTaskType.Text = item.LabelName;
                else if (lblTaskStatus2h.ID == item.LabelID)
                    txtTaskStatus2h.Text = lblTaskStatus2h.Text = lblhTaskStatus.Text = item.LabelName;
                else if (lblDueDate2h.ID == item.LabelID)
                    txtDueDate2h.Text = lblDueDate2h.Text = lblhDueDate.Text = item.LabelName;
                else if (lblPriority2h.ID == item.LabelID)
                    txtPriority2h.Text = lblPriority2h.Text = item.LabelName;
                //else if (lblActualCompletionDate2h.ID == item.LabelID)
                //    txtActualCompletionDate2h.Text = lblActualCompletionDate2h.Text =  item.LabelName;
                else if (lblCompletedPerctange2h.ID == item.LabelID)
                    txtCompletedPerctange2h.Text = lblCompletedPerctange2h.Text = lblhCompletedPerctange.Text = item.LabelName;
                else if (lblReminderDate2h.ID == item.LabelID)
                    txtReminderDate2h.Text = lblReminderDate2h.Text = item.LabelName;
                //else if (lblReminderTime2h.ID == item.LabelID)
                //    txtReminderTime2h.Text = lblReminderTime2h.Text = item.LabelName;
                //else if (lblCategories2h.ID == item.LabelID)
                //    txtCategories2h.Text = lblCategories2h.Text = lblhCategories.Text = item.LabelName;
                //else if (lblFollowUp2h.ID == item.LabelID)
                //    txtFollowUp2h.Text = lblFollowUp2h.Text =  item.LabelName;
                //else if (lblCustomFollowUpStartDate2h.ID == item.LabelID)
                //    txtCustomFollowUpStartDate2h.Text = lblCustomFollowUpStartDate2h.Text =  item.LabelName;
                //else if (lblCustomFollowUpEndDate2h.ID == item.LabelID)
                //    txtCustomFollowUpEndDate2h.Text = lblCustomFollowUpEndDate2h.Text =  item.LabelName;
                //else if (lblCustomFollowUpReminderDate2h.ID == item.LabelID)
                //    txtCustomFollowUpReminderDate2h.Text = lblCustomFollowUpReminderDate2h.Text =  item.LabelName;
                //else if (lblForwardToEmp_ID2h.ID == item.LabelID)
                //    txtForwardToEmp_ID2h.Text = lblForwardToEmp_ID2h.Text =  item.LabelName;
                //else if (lblOccurance_ID2h.ID == item.LabelID)
                //    txtOccurance_ID2h.Text = lblOccurance_ID2h.Text = lblhOccurance_ID.Text = item.LabelName;
                //else if (lblRemarks2h.ID == item.LabelID)
                //    txtRemarks2h.Text = lblRemarks2h.Text =  item.LabelName;
                //else if (lblActive2h.ID == item.LabelID)
                //    txtActive2h.Text = lblActive2h.Text = lblhActive.Text = item.LabelName;
                //else if (lblCruipID2h.ID == item.LabelID)
                //    txtCruipID2h.Text = lblCruipID2h.Text =  item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("tbl_Task").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\tbl_Task.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((CRMMaster)this.Master).Bindxml("tbl_Task").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTanentId1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTanentId1s.Text;
                //else if (lblLocationID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLocationID1s.Text;
                //else if (lblTaskID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTaskID1s.Text;
                //else if (lblCerticateNo1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCerticateNo1s.Text;
                //else if (lblForEmp_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtForEmp_ID1s.Text;
                //else if (lblPerformingEmp_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPerformingEmp_ID1s.Text;
                if (lblSubject1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSubject1s.Text;
                else if (lblStartingDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStartingDate1s.Text;
                //else if (lblTaskType1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTaskType1s.Text;
                else if (lblTaskStatus1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTaskStatus1s.Text;
                else if (lblDueDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDueDate1s.Text;
                else if (lblPriority1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPriority1s.Text;
                //else if (lblActualCompletionDate1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActualCompletionDate1s.Text;
                else if (lblCompletedPerctange1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCompletedPerctange1s.Text;
                else if (lblReminderDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReminderDate1s.Text;
                //else if (lblReminderTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtReminderTime1s.Text;
                //else if (lblCategories1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCategories1s.Text;
                //else if (lblFollowUp1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtFollowUp1s.Text;
                //else if (lblCustomFollowUpStartDate1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCustomFollowUpStartDate1s.Text;
                //else if (lblCustomFollowUpEndDate1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCustomFollowUpEndDate1s.Text;
                //else if (lblCustomFollowUpReminderDate1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCustomFollowUpReminderDate1s.Text;
                //else if (lblForwardToEmp_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtForwardToEmp_ID1s.Text;
                //else if (lblOccurance_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtOccurance_ID1s.Text;
                //else if (lblRemarks1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtRemarks1s.Text;
                //else if (lblActive1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblCruipID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCruipID1s.Text;

                //else if (lblTanentId2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTanentId2h.Text;
                //else if (lblLocationID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLocationID2h.Text;
                //else if (lblTaskID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTaskID2h.Text;
                //else if (lblCerticateNo2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCerticateNo2h.Text;
                //else if (lblForEmp_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtForEmp_ID2h.Text;
                //else if (lblPerformingEmp_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtPerformingEmp_ID2h.Text;
                else if (lblSubject2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSubject2h.Text;
                else if (lblStartingDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStartingDate2h.Text;
                //else if (lblTaskType2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTaskType2h.Text;
                else if (lblTaskStatus2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTaskStatus2h.Text;
                else if (lblDueDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDueDate2h.Text;
                else if (lblPriority2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPriority2h.Text;
                //else if (lblActualCompletionDate2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActualCompletionDate2h.Text;
                else if (lblCompletedPerctange2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCompletedPerctange2h.Text;
                else if (lblReminderDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReminderDate2h.Text;
                //else if (lblReminderTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtReminderTime2h.Text;
                //else if (lblCategories2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCategories2h.Text;
                //else if (lblFollowUp2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtFollowUp2h.Text;
                //else if (lblCustomFollowUpStartDate2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCustomFollowUpStartDate2h.Text;
                //else if (lblCustomFollowUpEndDate2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCustomFollowUpEndDate2h.Text;
                //else if (lblCustomFollowUpReminderDate2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCustomFollowUpReminderDate2h.Text;
                //else if (lblForwardToEmp_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtForwardToEmp_ID2h.Text;
                //else if (lblOccurance_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtOccurance_ID2h.Text;
                //else if (lblRemarks2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtRemarks2h.Text;
                //else if (lblActive2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblCruipID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCruipID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\tbl_Task.xml"));

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
            navigation.Visible = false;
            //drpLocationID.Enabled = true;
            //drpTaskID.Enabled = true;
            //drpCerticateNo.Enabled = true;
            //drpForEmp_ID.Enabled = true;
            //drpPerformingEmp_ID.Enabled = true;
            drpactivity.Enabled = true;
            drpproject.Enabled = true;
            txtnoofday.Enabled = true;
            drptasktype.Enabled = true;
            drptofield.Enabled = true;
            txtSubject.Enabled = true;
            txtStartingDate.Enabled = true;
            //txtTaskType.Enabled = true;
            drpTaskStatus.Enabled = true;
            txtDueDate.Enabled = true;
            drpPriority.Enabled = true;
            //txtActualCompletionDate.Enabled = true;
            drpCompletedPerctange.Enabled = true;
            //txtReminderDate.Enabled = true;
            //txtReminderTime.Enabled = true;
            //txtCategories.Enabled = true;
            //txtFollowUp.Enabled = true;
            //txtCustomFollowUpStartDate.Enabled = true;
            //txtCustomFollowUpEndDate.Enabled = true;
            //txtCustomFollowUpReminderDate.Enabled = true;
            //drpForwardToEmp_ID.Enabled = true;
            //drpOccurance_ID.Enabled = true;
            txtReminder.Enabled = true;
            //cbActive.Enabled = true;
            //txtCruipID.Enabled = true;

        }
        public void Readonly()
        {
            navigation.Visible = true;
            //drpLocationID.Enabled = false;
            //drpTaskID.Enabled = false;
            //drpCerticateNo.Enabled = false;
            //drpForEmp_ID.Enabled = false;
            //drpPerformingEmp_ID.Enabled = false;
            drpactivity.Enabled = false;
            drpproject.Enabled = false;
            txtnoofday.Enabled = false;
            drptasktype.Enabled = false;
            drptofield.Enabled = false;
            txtSubject.Enabled = false;
            txtStartingDate.Enabled = false;
            //txtTaskType.Enabled = false;
            drpTaskStatus.Enabled = false;
            txtDueDate.Enabled = false;
            drpPriority.Enabled = false;
            //txtActualCompletionDate.Enabled = false;
            drpCompletedPerctange.Enabled = false;
            //txtReminderDate.Enabled = false;
            //txtReminderTime.Enabled = false;
            //txtCategories.Enabled = false;
            //txtFollowUp.Enabled = false;
            //txtCustomFollowUpStartDate.Enabled = false;
            //txtCustomFollowUpEndDate.Enabled = false;
            //txtCustomFollowUpReminderDate.Enabled = false;
            //drpForwardToEmp_ID.Enabled = false;
            //drpOccurance_ID.Enabled = false;
            txtReminder.Enabled = false;
            //cbActive.Enabled = false;
            //txtCruipID.Enabled = false;


        }

        protected void cbreminder_CheckedChanged(object sender, EventArgs e)
        {
            if (cbreminder.Checked == true)
            {
                txtReminderDate.Enabled = true;
                // txtReminderTime.Enabled = true;
            }
            else
            {
                txtReminderDate.Enabled = false;
                // txtReminderTime.Enabled = false;
            }
        }

        protected void btntaskpanal_Click(object sender, EventArgs e)
        {
            pnltask.Visible = true;
            pnlassigntask.Visible = false;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = false;
            pnlmarkcomplaete.Visible = false;
            pnlrecurrnce.Visible = false;
            pnlsendstatus.Visible = false;
        }

        protected void btndetail_Click(object sender, EventArgs e)
        {
            pnltask.Visible = false;
            pnlassigntask.Visible = false;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = true;
            pnlmarkcomplaete.Visible = false;
            pnlrecurrnce.Visible = false;
            pnlsendstatus.Visible = false;
        }

        protected void btnassingtask_Click(object sender, EventArgs e)
        {
            pnltask.Visible = false;
            pnlassigntask.Visible = true;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = false;
            pnlmarkcomplaete.Visible = false;
            pnlrecurrnce.Visible = false;
            pnlsendstatus.Visible = false;
            if (ViewState["Edit"] != null)
            {
                int ID = Convert.ToInt32(ViewState["Edit"]);
                Database.tbl_Task objtbl_Task = DB.tbl_Task.Single(p => p.TaskID == ID);
                Response.Redirect("AssignTask.aspx?TaskID=" + ID);
            }
        }

        protected void btnsendstatus_Click(object sender, EventArgs e)
        {
            pnltask.Visible = false;
            pnlassigntask.Visible = false;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = false;
            pnlmarkcomplaete.Visible = false;
            pnlrecurrnce.Visible = false;
            pnlsendstatus.Visible = true;
        }

        protected void btnmarkcomplete_Click(object sender, EventArgs e)
        {
            pnltask.Visible = false;
            pnlassigntask.Visible = false;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = false;
            pnlmarkcomplaete.Visible = true;
            pnlrecurrnce.Visible = false;
            pnlsendstatus.Visible = false;
        }

        protected void btnRecurrence_Click(object sender, EventArgs e)
        {
            pnltask.Visible = false;
            pnlassigntask.Visible = false;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = false;
            pnlmarkcomplaete.Visible = false;
            pnlrecurrnce.Visible = true;
            pnlsendstatus.Visible = false;
        }

        protected void btncancleassignment_Click(object sender, EventArgs e)
        {
            pnltask.Visible = true;
            pnlassigntask.Visible = false;
            pnlcancleassignment.Visible = false;
            pnldetail.Visible = false;
            pnlmarkcomplaete.Visible = false;
            pnlrecurrnce.Visible = false;
            pnlsendstatus.Visible = false;
        }

        protected void txtnoofday_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(txtStartingDate.Text);

            DateTime dt2 = dt.AddDays(Convert.ToInt32(txtnoofday.Text));
            txtDueDate.Text = dt2.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        protected void txtDueDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(txtStartingDate.Text);

            DateTime dt2 = Convert.ToDateTime(txtDueDate.Text);

            if (dt > dt2)
            {
                lblMsg.Text = " Due Date less date of StartingDate..";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                pnlSuccessMsg.Visible = true;
                txtDueDate.Text = "";
            }
            else
            {
                pnlSuccessMsg.Visible = false;
            }
        }

        protected void btnassignNew_Click(object sender, EventArgs e)
        {

        }





    }
}