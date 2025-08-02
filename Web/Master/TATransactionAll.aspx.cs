using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Database;

namespace Web.Master
{
    public partial class TATransactionAll : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 9000007;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                Session["DATE"] = DateTime.Now;
                EMPBIND();
                BindLeaveRequests(DateTime.Now);
            }

        }
        public void EMPBIND()
        {
            drpEMP.DataSource = DB.TBLCONTACTs.Where(p => p.TenentID == TID);
            drpEMP.DataTextField = "PersName1";
            drpEMP.DataValueField = "ContactMyID";
            drpEMP.DataBind();
        }
        public void BindLeaveRequests(DateTime date)
        {
            string username = "";

           Label2.Text = date.ToString("MMMM") + " " + date.Year;
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
            ListView111.DataSource = List;
            ListView111.DataBind();
            if (ViewState["FinalTotalbl"] != null)
                lblTotalTimeFinal.Text = ViewState["FinalTotalbl"].ToString();

        }
        public string getDateData(DateTime Date)
        {
            string Content = "";
            int EMPID = Convert.ToInt32(drpEMP.SelectedValue);
            if (EMPID != 0)
            {
                foreach (NewAttandance item in DB.NewAttandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.EmployeeID == EMPID)) //&& p.UserID == UserID
                {
                    Content += item.InTime.Value.ToString("hh:mm") + "-";
                    if (item.OutTime.HasValue)
                    {
                        Content += item.OutTime.Value.ToString("hh:mm") + "&nbsp|&nbsp";

                    }
                }                
            }
            return Content;
        }
        public string GetAbsantData(DateTime Date)
        {
            string Content = "";
            int UserID = 0;
            double Minute = 0;
            bool Flag = false;

            UserID = Convert.ToInt32(drpEMP.SelectedValue);
            if (UserID != 0)
            {
                foreach (NewAttandance item in DB.NewAttandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.EmployeeID == UserID))
                {

                    if (item.OutTime.HasValue)
                    {
                        Flag = true;
                        Minute += item.OutTime.Value.Subtract(item.InTime.Value).TotalMinutes;
                    }
                }
            }
            if (Date <= DateTime.Now)
                return (Flag == true) ? "Present" : "Absent";
            else
                return "";
        }
        public string GetTotalTime(DateTime Date)
        {
            string Content = "";
            int UserID = 0;

            double Minute = 0;
            UserID = Convert.ToInt32(drpEMP.SelectedValue);
            if (UserID != 0)
            {
                foreach (NewAttandance item in DB.NewAttandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.EmployeeID == UserID))
                {

                    if (item.OutTime.HasValue)
                    {
                        Minute += item.OutTime.Value.Subtract(item.InTime.Value).TotalMinutes;

                    }
                }
            }

            int hours = Convert.ToInt32(Minute) / 60; // 2
            int minutes = Convert.ToInt32(Minute % 60); // 1
            if (Date <= DateTime.Now)
                return hours.ToString() + " Hours " + minutes.ToString() + " Mins";
            else
                return "";
        }
        public string GetTotalMinute(DateTime Date)
        {
            string Content = "";
            int UserID = 0;

            double Minute = 0;
             UserID = Convert.ToInt32(drpEMP.SelectedValue);
             if (UserID != 0)
             {
                 foreach (NewAttandance item in DB.NewAttandances.Where(p => p.InTime.Value.Month == Date.Month && p.InTime.Value.Year == Date.Year && p.InTime.Value.Day == Date.Day && p.EmployeeID == UserID))
                 {

                     if (item.OutTime.HasValue)
                     {
                         Minute += item.OutTime.Value.Subtract(item.InTime.Value).TotalMinutes;

                     }
                 }
             }
            return Minute.ToString();
        }

        protected void drpmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int btdrpmonth = Convert.ToInt32(drpmonth.SelectedValue);


            DateTime nowDate = DateTime.Now;
            DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
            Session["DATE"] = firstDayCurrentYear.AddMonths(btdrpmonth - 1);
            BindLeaveRequests(firstDayCurrentYear.AddMonths(btdrpmonth - 1));
        }

        protected void ListView111_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            decimal MonthTotal = 0;
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

        protected void drpEMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Date = (DateTime)Session["DATE"];
            BindLeaveRequests(Date);
        }

    }
}