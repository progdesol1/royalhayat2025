using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Database;

namespace Web.CRM
{
    public partial class Attendance : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        
        bool FirstFlag, ClickFlag = true;
        int TID, TTID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //if (!((CRMMaster)this.Master).GetAccess((int)Web.CRM.Atten))
            //{
            //    Response.Redirect("Default.php");
            //}
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                ((CRMMaster)this.Master).CheckLogin();
                Session["DATE"] = DateTime.Now;
               
                if (((CRMMaster)this.Master).GetHRMSLogginType() == UID)
                {
                    checkabsent.Visible = true;
                    chePresent.Visible = true;
                    drpUsers.Visible = true;
                    BindUser();
                    BindLeaveRequests(DateTime.Now);
                }
                else
                {
                    checkabsent.Visible = false;
                    chePresent.Visible = false;
                    drpUsers.Visible = false;
                    BindLeaveRequests(DateTime.Now);
                }
            }
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
        public void BindUser()
        {
             Classes.EcommAdminClass.getdropdown(drpUsers, TID, UID.ToString(), LID.ToString(), LangID.ToString(), "GlobleEmployee");
        }

        public void BindLeaveRequests(DateTime date)
        {
            string username = "";
            Database.USER_MST LogginUser = (Database.USER_MST)Session["USER"];
            if (((CRMMaster)this.Master).GetHRMSLogginType() == LogginUser.USER_ID && drpUsers.SelectedValue!="0")
            {
                username = drpUsers.SelectedItem.ToString();
            }
            else
            {
                username = ((Database.USER_MST)Session["USER"]).FIRST_NAME;
            }

            
            lblTitlePage.Text = username+"-" + date.ToString("MMMM")+" "+ date.Year;
            //All Attendance List 
            List<Classes.Class1> List = new List<Classes.Class1>();
            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                DateTime newDate = new DateTime(date.Year, date.Month, i);
                Classes.Class1 Obj = new Classes.Class1();
                Obj.ID = i;
                Obj.DayName = newDate.DayOfWeek.ToString();
                Obj.date = newDate;
                List.Add(Obj);
            }


            Listview1.DataSource = List;
            Listview1.DataBind();
            if (ViewState["FinalTotalbl"] != null)
                lblTotalTimeFinal.Text = ViewState["FinalTotalbl"].ToString();

        }

        public string getDateData(DateTime Date)
        {
            string Content = "";
            int UserID = 0;
            Database.USER_MST LogginUser = (Database.USER_MST)Session["USER"];
            if (((CRMMaster)this.Master).GetHRMSLogginType() == LogginUser.USER_ID && drpUsers.SelectedValue != "0")
            {
                UserID = Convert.ToInt32(drpUsers.SelectedValue);
               
            }
            else
            {
                UserID = ((CRMMaster)this.Master).GetLogginID();
            }
            foreach (Attandance item in DB.Attandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.UserID == UserID))
            {
                Content += item.InTime.Value.ToString("hh:mm") + "-";
                if (item.OutTime.HasValue)
                {
                    Content += item.OutTime.Value.ToString("hh:mm") + "&nbsp|&nbsp";

                }
            }
            return Content;
        }

        public string GetTotalTime(DateTime Date)
        {
            string Content = "";
            int UserID = 0;
            Database.USER_MST LogginUser = (Database.USER_MST)Session["USER"];
            if (((CRMMaster)this.Master).GetHRMSLogginType() == LogginUser.USER_ID && drpUsers.SelectedValue != "0")
            {
                UserID = Convert.ToInt32(drpUsers.SelectedValue);
                
            }
            else
            {
                UserID = ((CRMMaster)this.Master).GetLogginID();
            }
            double Minute = 0;
            foreach (Attandance item in DB.Attandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.UserID == UserID))
            {

                if (item.OutTime.HasValue)
                {
                    Minute += item.OutTime.Value.Subtract(item.InTime.Value).TotalMinutes;

                }
            }

            int hours = Convert.ToInt32(Minute) / 60; // 2
            int minutes = Convert.ToInt32(Minute % 60); // 1
            if (Date <= DateTime.Now)
                return hours.ToString() + " Hours " + minutes.ToString() + " Mins";
            else
                return "";
        }

        public string GetAbsantData(DateTime Date)
        {
            string Content = "";
            int UserID = 0;
            Database.USER_MST LogginUser = (Database.USER_MST)Session["USER"];
            if (((CRMMaster)this.Master).GetHRMSLogginType() == LogginUser.USER_ID && drpUsers.SelectedValue != "0")
            {
                UserID = Convert.ToInt32(drpUsers.SelectedValue);
            }
            else
            {
                UserID = ((CRMMaster)this.Master).GetLogginID();
                
            }
            double Minute = 0;
            bool Flag = false;
            foreach (Attandance item in DB.Attandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.UserID == UserID))
            {

                if (item.OutTime.HasValue)
                {
                    Flag = true;
                    Minute += item.OutTime.Value.Subtract(item.InTime.Value).TotalMinutes;
                }
            }
            if (Date <= DateTime.Now)
                return (Flag == true) ? "Present" : "Absent";
            else
                return "";
        }

        public string GetTotalMinute(DateTime Date)
        {
            string Content = "";
            int UserID = 0;
            Database.USER_MST LogginUser = (Database.USER_MST)Session["USER"];
            if (((CRMMaster)this.Master).GetHRMSLogginType() == LogginUser.USER_ID && drpUsers.SelectedValue != "0")
            {
                UserID = Convert.ToInt32(drpUsers.SelectedValue);
            }
            else
            {
                UserID = ((CRMMaster)this.Master).GetLogginID();

            }
            double Minute = 0;
            foreach (Attandance item in DB.Attandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.UserID == UserID))
            {

                if (item.OutTime.HasValue)
                {
                    Minute += item.OutTime.Value.Subtract(item.InTime.Value).TotalMinutes;

                }
            }
            return Minute.ToString();
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    USER_MST obj = new USER_MST();

        //    GridList.DataSource = DB.LeaveRequests.Where(p => p.Deleted == true).OrderByDescending(m => m.ID);
        //    GridList.DataBind();

        //}
        //protected void btnSearchAll_Click(object sender, EventArgs e)
        //{
        //    GridList.DataSource = DB.LeaveRequests.Where(p => p.Deleted == true).OrderByDescending(m => m.ID);
        //    GridList.DataBind();
        //}

        protected void GridList_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Listview1.EditIndex = e.NewPageIndex;
            BindLeaveRequests(DateTime.Now);
        }
        decimal MonthTotal = 0;
        //protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        HiddenField HiddenFieldMinute = (HiddenField)e.Row.FindControl("HiddenField1");
        //        MonthTotal += Convert.ToDecimal(HiddenFieldMinute.Value);
        //        string DayString = (string)DataBinder.Eval(e.Row.DataItem, "DayName");
        //        if (DayString == "Sunday")
        //        {
        //            e.Row.BackColor = Color.LightYellow;
        //        }
        //    }
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        Label labelTotalTime = (Label)e.Row.FindControl("lblTotalTime");
        //        int hours = Convert.ToInt32(MonthTotal) / 60; // 2
        //        int minutes = Convert.ToInt32(MonthTotal % 60); // 1

        //        labelTotalTime.Text = hours.ToString() + " Hours " + minutes.ToString() + " Mins";
        //    }
        //}


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveDetail.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            DateTime Date = (DateTime)Session["DATE"];
            Session["DATE"] = Date.AddMonths(1);
            BindLeaveRequests(Date.AddMonths(1));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            DateTime Date = (DateTime)Session["DATE"];
            Session["DATE"] = Date.AddMonths(-1);
            BindLeaveRequests(Date.AddMonths(-1));
        }

        protected void drpUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Date = (DateTime)Session["DATE"];
            BindLeaveRequests(Date);



        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                HiddenField HiddenFieldMinute = (HiddenField)e.Item.FindControl("HiddenField1");
                MonthTotal += Convert.ToDecimal(HiddenFieldMinute.Value);
                string DayString = (string)DataBinder.Eval(e.Item.DataItem, "DayName");
                if (DayString == "Sunday")
                {
                    //e.Item.BackColor = Color.LightYellow;
                }
            }
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label labelTotalTime = (Label)e.Item.FindControl("lblTotalTime");
                int hours = Convert.ToInt32(MonthTotal) / 60; // 2
                int minutes = Convert.ToInt32(MonthTotal % 60); // 1

                labelTotalTime.Text = hours.ToString() + " Hours " + minutes.ToString() + " Mins";
                ViewState["FinalTotalbl"] = labelTotalTime.Text;
            }
        }

        protected void drpmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int btdrpmonth = Convert.ToInt32(drpmonth.SelectedValue);


            DateTime nowDate = DateTime.Now;
            DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
            Session["DATE"] = firstDayCurrentYear.AddMonths(btdrpmonth - 1);
            BindLeaveRequests(firstDayCurrentYear.AddMonths(btdrpmonth - 1));

        }

        protected void chePresent_CheckedChanged(object sender, EventArgs e)
        {

            if (chePresent.Checked == true)
            {

                {
                    string selectedValues = string.Empty;
                    foreach (ListItem li in drpUsers.Items)
                    {
                        if (li.Selected == true)
                        {
                            selectedValues += li.Text + ",";
                        }
                    }
                    Response.Write(selectedValues.ToString());
                }
                
                //string UserId = System.Web.HttpContext.Current.User.Identity.Name;
                //DateTime nowDate = DateTime.Now.Date;
                //drpUsers.DataSource = DB.Attandances.Where(p => p.isAbsent == false && p.Active == true);
                //drpUsers.DataValueField = "InTime";
                //drpUsers.DataTextField = "UserID";
              
                //drpUsers.DataBind();
                
                
            }

        }
      
        protected void checkabsent_CheckedChanged(object sender, EventArgs e)
        {
            if(checkabsent.Checked == true)
            {
                string UserId = System.Web.HttpContext.Current.User.Identity.Name;
                DateTime nowDate = DateTime.Now.Date;
                drpUsers.DataSource = DB.Attandances.Where(p => p.isAbsent == true && p.Active == true);
                drpUsers.DataValueField = "InTime";
                drpUsers.DataTextField = "UserID";
                drpUsers.DataBind();
                BindUser();
            }
        }

        protected void btnThumRegister_Click(object sender, EventArgs e)
        {
            pnlSuccessMsg.Visible = true;
            lblMsg.Text = "Your Thum Registration is successfull...";
        }
    }
}
