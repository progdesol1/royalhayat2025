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
    public partial class Attendance_tna : System.Web.UI.Page
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
                    //checkabsent.Visible = true;
                    //chePresent.Visible = true;
                    drpUsers.Visible = true;
                    BindUser();
                    BindLeaveRequests(DateTime.Now);
                }
                else
                {
                    //checkabsent.Visible = false;
                    //chePresent.Visible = false;
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
        public int GetLogginID()
        {
            return Convert.ToInt32(((Database.USER_MST)Session["USER"]).USER_ID);
        }
        protected void linkCheckIn_Click(object sender, EventArgs e)
        {
            int UserID = GetLogginID();
            Attandance Obj = new Attandance();
            Obj.UserID = GetLogginID();
            Obj.InTime = DateTime.Now;
            Obj.isAbsent = false;
            Obj.Deleted = true;
            Obj.Active = true;
            DB.Attandances.AddObject(Obj);
            DB.SaveChanges();
            linkCheckIn.Visible = false;
            linkCheckOut.Visible = true;
            //linkCheckOut.CssClass = "stdbtn btn_blue";
            //linkCheckIn.CssClass = "stdbtn";
            //linkCheckIn.ForeColor = Color.Black;
            Response.Redirect("Attendance_tna.aspx");
        }

        protected void linkCheckOut_Click(object sender, EventArgs e)
        {
            int UserID = GetLogginID();
            DateTime TodayDate = DateTime.Now.Date;
            List<Attandance> ObjList = (from item in DB.Attandances where item.UserID == UserID && item.InTime.Value.Year == TodayDate.Year && item.InTime.Value.Month == TodayDate.Month && item.InTime.Value.Day == TodayDate.Day && item.OutTime == null select item).ToList();
            if (ObjList.Count() > 0)
            {
                int AttandanceID = ObjList[0].ID;
                Attandance Obj = DB.Attandances.Single(p => p.ID == AttandanceID);
                Obj.UserID = GetLogginID();
                Obj.OutTime = DateTime.Now;
                DB.SaveChanges();
                linkCheckIn.Visible = true;
                linkCheckOut.Visible = false;
                //linkCheckIn.CssClass = "stdbtn btn_red";
                //linkCheckOut.CssClass = "stdbtn";
                //linkCheckOut.ForeColor = Color.Black;
                Response.Redirect("Attendance_tna.aspx");
            }
        }
        protected void drpTimeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpTimeStatus.SelectedValue=="1")
            {
               
            }
            else if (drpTimeStatus.SelectedValue == "2")
            {

            }
            else if (drpTimeStatus.SelectedValue == "3")
            {

            }
            else if (drpTimeStatus.SelectedValue == "4")
            {

            }
            else
            {

            }
        }

       

        protected void BtnTimeStatusProcess_Click(object sender, EventArgs e)
        {

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

       
       
        decimal MonthTotal = 0;
       


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

       

      
        protected void btnThumRegister_Click(object sender, EventArgs e)
        {
           
            pnlSuccessMsg.Visible = true;
            btnThumRegister.Visible = false;

            int UserID = GetLogginID();
            DateTime TodayDate = DateTime.Now.Date;
            List<Attandance> ObjList = (from item in DB.Attandances where item.UserID == UserID && item.InTime.Value.Year == TodayDate.Year && item.InTime.Value.Month == TodayDate.Month && item.InTime.Value.Day == TodayDate.Day select item).ToList();
            if (ObjList.Where(p => p.OutTime == null).Count() > 0)
            {
                linkCheckIn.Visible = false;
                //btnSignin.CssClass = "stdbtn";
                linkCheckOut.Visible = true;
                //btnSignOut.CssClass = "stdbtn btn_blue";
                //linkCheckIn.BackColor = Color.Green;
            }
            else
            {
                if (ObjList.Count() == 0)
                {
                    Attandance Obj = new Attandance();
                    Obj.UserID = GetLogginID();
                    Obj.InTime = DateTime.Now;
                    Obj.isAbsent = false;
                    Obj.Deleted = true;
                    Obj.Active = true;
                    DB.Attandances.AddObject(Obj);
                    DB.SaveChanges();
                    linkCheckIn.Visible = false;
                    linkCheckOut.Visible = true;
                }
                else
                {
                    linkCheckIn.Visible = true;
                    //btnSignin.CssClass = "stdbtn btn_red";
                    //btnSignOut.CssClass = "stdbtn";
                    //linkCheckIn.BackColor = Color.Red;
                    //btnSignOut.ForeColor = Color.Black;
                    linkCheckOut.Visible = false;
                }
            }

            if(linkCheckIn.Visible==true)
                drpTimeStatus.Visible = BtnTimeStatusProcess.Visible = true;
            else
                drpTimeStatus.Visible = BtnTimeStatusProcess.Visible = false;



            lblMsg.Text = "Your Thum Registration is successfull...";
        }

    }
}
