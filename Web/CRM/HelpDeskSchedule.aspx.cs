using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Classes;
using System.Text;
namespace Web.CRM
{
    public partial class HelpDeskSchedule : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        static private string Baseurl = ConfigurationManager.AppSettings["Baseurl"];
        static private string bearerToken = ConfigurationManager.AppSettings["bearerToken"];
        protected void Page_Load(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            ViewState["HelpData"] = null;
            BindCALANDER();
            if (!IsPostBack)
            {
                binddrop();
                ListAppoint();

                drpstares.Items[1].Attributes["Style"] = "background-color:#F3565D;";
                drpstares.Items[2].Attributes["Style"] = "background-color:#1BBC9B;";

                if (Request.QueryString["HelpID"] != null)
                {

                    int Master = Convert.ToInt32(Request.QueryString["HelpID"]);

                    Listview1.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == Master && p.ACTIVITYE == "helpdesk").OrderBy(p => p.MasterCODE);
                    Listview1.DataBind();
                }
            }


        }
        public void BindCALANDER()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.Appointment> List = new List<Database.Appointment>();
            String str = "<script > \n";
            str += "var AppCalendar = function () {\n";
            str += "return {\n";
            str += "init: function () {\n";
            str += "this.initCalendar()\n";
            str += "}, initCalendar: function () {\n";
            str += " if (jQuery().fullCalendar) {\n";
            str += "var e = new Date,\n";
            str += "t = e.getDate(),\n";
            str += "a = e.getMonth(),\n";
            str += "n = e.getFullYear(),\n";
            str += "r = {}; App.isRTL() ? $(\"#calendar\").parents(\".portlet\").width() <= 720 ? ($(\"#calendar\").addClass(\"mobile\"),\n";
            str += "r = { right: \"title, prev, next\", center: \"\", left: \"agendaDay, agendaWeek, month, today\" }) : ($(\"#calendar\").removeClass(\"mobile\"),\n";
            str += "r = { right: \"title\", center: \"\", left: \"agendaDay, agendaWeek, month, today, prev,next\" }) : $(\"#calendar\").parents(\".portlet\").width() <= 720 ? ($(\"#calendar\").addClass(\"mobile\"),\n";
            str += "r = { left: \"title, prev, next\", center: \"\", right: \"today,month,agendaWeek,agendaDay\" }) : ($(\"#calendar\").removeClass(\"mobile\"),\n";
            str += "r = { left: \"title\", center: \"\", right: \"prev,next,today,month,agendaWeek,agendaDay\" });\n";
            str += " var l = function (e) {\n";
            str += " var t = { title: $.trim(e.text()) }; e.data(\"eventObject\", t), e.draggable({ zIndex: 999, revert: !0, revertDuration: 0 })},\n";
            str += "o = function (e) {\n";
            str += "e = 0 === e.length ? \"Untitled Event\" : e;\n";
            str += "var t = $('<div class=\"external -event label label-default\">' + e + \"</div>\"); jQuery(\"#event_box\").append(t), l(t)}; $(\"#external-events div.external-event\").each(function () { l($(this)) }), $(\"#event_add\").unbind(\"click\").click(function () {\n";
            str += "var e = $(\"#event_title\").val(); o(e)}),\n";
            str += "$(\"#event_box\").html(\"\"),\n";
            str += "o(\"My E 1\"),\n";
            str += "o(\"My E 2\"),\n";
            str += "o(\"My E 3\"),\n";
            str += "o(\"My E 4\"),\n";
            str += "o(\"My E 5\"),\n";
            str += "o(\"My E 6\"),\n";
            str += "$(\"#calendar\").fullCalendar(\"destroy\"), $(\"#calendar\").fullCalendar({\n";
            str += "header: r, defaultView: \"month\", slotMinutes: 15, editable: !0, droppable: !0, drop: function (e, t) {\n";
            str += "var a = $(this).data(\"eventObject\"), n = $.extend({}, a);\n";
            str += "n.start = e, n.allDay = t, n.className = $(this).attr(\"data-class\"), $(\"#calendar\").fullCalendar(\"renderEvent\", n, !0),\n";
            str += "$(\"#drop-remove\").is(\":checked\") && $(this).remove()\n";
            str += "},\n";
            str += "events: [\n";//t for Day , a for Month , n for year

            //HELP DESK
            int admin = 0;
            int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string Userid = UIN.ToString();
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
            if (admin == UIN)
            {
                if (ViewState["HelpData"] == null)
                {
                    List<Database.CRMMainActivity> HELPDeskList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").ToList();
                    foreach (Database.CRMMainActivity item in HELPDeskList)
                    {
                        DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                        string Col = Calecolor(item.MyStatus);
                        str += "{ title: \"" + item.Version + "\", start: new Date(" + sdt.Year + "," + sdt.Month + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\"" + Col + "\"), url: \"" + item.REMINDERNOTE + "\" },\n";
                    }
                    ViewState["HelpData"] = HELPDeskList;
                }
            }
            else if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").Count() > 0)
            {
                if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).Count() == 1)
                {
                    if (ViewState["HelpData"] == null)
                    {
                        int empid = Convert.ToInt32(DB.tbl_Employee.Single(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).employeeID);
                        List<Database.CRMMainActivity> HELPDeskListsupp = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk") || (p.TenentID == TID && p.Emp_ID == empid && p.ACTIVITYE == "helpdesk")).ToList();
                        foreach (Database.CRMMainActivity item in HELPDeskListsupp)
                        {
                            DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                            string Col = Calecolor(item.MyStatus);
                            str += "{ title: \"" + item.Version + "\", start: new Date(" + sdt.Year + "," + Convert.ToInt32(sdt.Month - 1).ToString() + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\"" + Col + "\"), url: \"" + item.REMINDERNOTE + "\" },\n";
                        }
                        ViewState["HelpData"] = HELPDeskListsupp;
                    }
                }
                else
                {
                    if (ViewState["HelpData"] == null)
                    {
                        List<Database.CRMMainActivity> HELPDeskList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk").ToList();
                        foreach (Database.CRMMainActivity item in HELPDeskList)
                        {
                            DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                            string Col = Calecolor(item.MyStatus);
                            str += "{ title: \"" + item.Version + "\", start: new Date(" + sdt.Year + "," + Convert.ToInt32(sdt.Month - 1).ToString() + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\""+ Col +"\"), url: \"" + item.REMINDERNOTE + "\" },\n";
                        }
                        ViewState["HelpData"] = HELPDeskList;
                    }
                }
            }

            if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").Count() > 0)
            {
                List<Database.CRMMainActivity> GList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").GroupBy(p => p.GROUPCODE).Select(p => p.FirstOrDefault()).ToList();
                foreach (Database.CRMMainActivity item in GList)
                {
                    int ITGroup = Convert.ToInt32(item.GROUPCODE);
                    if (DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.ITGROUPID == ITGroup && p.USERCODE == Userid).Count() == 1)
                    {
                        if (ViewState["HelpData"] == null)
                        {
                            List<Database.CRMMainActivity> HELPDeskList = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.ACTIVITYE == "helpdesk") || (p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk")).ToList();
                            foreach (Database.CRMMainActivity itemHD in HELPDeskList)
                            {
                                DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                                string Col = Calecolor(item.MyStatus);
                                str += "{ title: \"" + itemHD.Version + "\", start: new Date(" + sdt.Year + "," + Convert.ToInt32(sdt.Month - 1).ToString() + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\""+ Col +"\"), url: \"" + item.REMINDERNOTE + "\" },\n";
                            }
                            ViewState["HelpData"] = HELPDeskList;
                        }
                    }
                }

            }

            //str += "{ title: \"Birthday Party\", start: new Date(n, a, t + 1, 19, 0), end: new Date(n, a, t + 1, 22, 30), backgroundColor: App.getBrandColor(\"purple\"), allDay: !1 },\n";
            //str += "{ title: \"Docter Batra\", start: new Date(2018, 8,  6), end: new Date(2018, 8,  10), backgroundColor: (\"#db6944\"), url: \"/Master/demo\" },\n";
            //{ title: \"Opp_dateCH\", start: new Date(2018, 1,  21), end: new Date(2018, 2,  25), backgroundColor: App.getBrandColor(\"2\"), url: \"/Master/demo\" }
            //str += "{ title: \"All Day Event\", start: new Date(n, a, 1), backgroundColor: App.getBrandColor(\"yellow\") },\n";
            //str += "{ title: \"Long Event\", start: new Date(n, a, t - 5), end: new Date(n, a, t - 2), backgroundColor: App.getBrandColor(\"blue\") },\n";
            //str += "{ title: \"Repeating Event\", start: new Date(n, a, t - 3, 16, 0), allDay: !1, backgroundColor: App.getBrandColor(\"red\") },\n";
            //str += "{ title: \"Repeating Event\", start: new Date(n, a, t + 4, 16, 0), allDay: !1, backgroundColor: App.getBrandColor(\"green\") },\n";
            //str += "{ title: \"Meeting\", start: new Date(n, a, t, 10, 30), allDay: !1,backgroundColor: App.getBrandColor(\"blue\") },\n";
            //str += "{ title: \"Lunch\", start: new Date(n, a, t, 12, 0), end: new Date(n, a, t, 14, 0), backgroundColor: App.getBrandColor(\"grey\"), allDay: !1 },\n";
            //str += "{ title: \"Birthday Party\", start: new Date(n, a, t + 1, 19, 0), end: new Date(n, a, t + 1, 22, 30), backgroundColor: App.getBrandColor(\"purple\"), allDay: !1 },\n";
            //str += "{ title: \"Click for Google\", start: new Date(n, a, 28), end: new Date(n, a, 29), backgroundColor: App.getBrandColor(\"yellow\"), url: \"http://google.com/\" }\n";

            str += "]\n";
            str += "})\n";
            str += "}\n";
            str += "}\n";
            str += "}\n";
            str += "}(); jQuery(document).ready(function () { AppCalendar.init() });\n";
            str += "</script > \n";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", str);


        }
        public void BindOwnDateCalander()
        {
            DateTime Date = DateTime.Now;
            int Day = Date.Day;
            int MON = Date.Month;
            int YER = Date.Year;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.Appointment> List = new List<Database.Appointment>();
            String str = "<script > \n";
            str += "var AppCalendar = function () {\n";
            str += "return {\n";
            str += "init: function () {\n";
            str += "this.initCalendar()\n";
            str += "}, initCalendar: function () {\n";
            str += " if (jQuery().fullCalendar) {\n";
            str += "var e == new Date,\n";
            str += "t = " + Day + ",\n";
            str += "a = " + MON + ",\n";
            str += "n = " + YER + ",\n";
            str += "r = {}; App.isRTL() ? $(\"#calendar\").parents(\".portlet\").width() <= 720 ? ($(\"#calendar\").addClass(\"mobile\"),\n";
            str += "r = { right: \"title, prev, next\", center: \"\", left: \"agendaDay, agendaWeek, month, today\" }) : ($(\"#calendar\").removeClass(\"mobile\"),\n";
            str += "r = { right: \"title\", center: \"\", left: \"agendaDay, agendaWeek, month, today, prev,next\" }) : $(\"#calendar\").parents(\".portlet\").width() <= 720 ? ($(\"#calendar\").addClass(\"mobile\"),\n";
            str += "r = { left: \"title, prev, next\", center: \"\", right: \"today,month,agendaWeek,agendaDay\" }) : ($(\"#calendar\").removeClass(\"mobile\"),\n";
            str += "r = { left: \"title\", center: \"\", right: \"prev,next,today,month,agendaWeek,agendaDay\" });\n";
            str += " var l = function (e) {\n";
            str += " var t = { title: $.trim(e.text()) }; e.data(\"eventObject\", t), e.draggable({ zIndex: 999, revert: !0, revertDuration: 0 })},\n";
            str += "o = function (e) {\n";
            str += "e = 0 === e.length ? \"Untitled Event\" : e;\n";
            str += "var t = $('<div class=\"external -event label label-default\">' + e + \"</div>\"); jQuery(\"#event_box\").append(t), l(t)}; $(\"#external-events div.external-event\").each(function () { l($(this)) }), $(\"#event_add\").unbind(\"click\").click(function () {\n";
            str += "var e = $(\"#event_title\").val(); o(e)}),\n";
            str += "$(\"#event_box\").html(\"\"),\n";
            str += "o(\"My E 1\"),\n";
            str += "o(\"My E 2\"),\n";
            str += "o(\"My E 3\"),\n";
            str += "o(\"My E 4\"),\n";
            str += "o(\"My E 5\"),\n";
            str += "o(\"My E 6\"),\n";
            str += "$(\"#calendar\").fullCalendar(\"destroy\"), $(\"#calendar\").fullCalendar({\n";
            str += "header: r, defaultView: \"month\", slotMinutes: 15, editable: !0, droppable: !0, drop: function (e, t) {\n";
            str += "var a = $(this).data(\"eventObject\"), n = $.extend({}, a);\n";
            str += "n.start = e, n.allDay = t, n.className = $(this).attr(\"data-class\"), $(\"#calendar\").fullCalendar(\"renderEvent\", n, !0),\n";
            str += "$(\"#drop-remove\").is(\":checked\") && $(this).remove()\n";
            str += "},\n";
            str += "events: [\n";//t for Day , a for Month , n for year
            //HELP DESK
            int admin = 0;
            int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string Userid = UIN.ToString();
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
                admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
            if (admin == UIN)
            {
                List<Database.CRMMainActivity> HELPDeskList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").ToList();
                foreach (Database.CRMMainActivity item in HELPDeskList)
                {
                    DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                    str += "{ title: \"" + item.Version + "\", start: new Date(" + sdt.Year + "," + sdt.Month + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\"purple\"), url: \"/Master/demo\" },\n";
                }
            }
            else if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk").Count() > 0)
            {
                if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).Count() == 1)
                {
                    int empid = Convert.ToInt32(DB.tbl_Employee.Single(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).employeeID);
                    List<Database.CRMMainActivity> HELPDeskListsupp = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk") || (p.TenentID == TID && p.Emp_ID == empid && p.ACTIVITYE == "helpdesk")).ToList();
                    foreach (Database.CRMMainActivity item in HELPDeskListsupp)
                    {
                        DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                        str += "{ title: \"" + item.Version + "\", start: new Date(" + sdt.Year + "," + Convert.ToInt32(sdt.Month - 1).ToString() + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\"purple\"), url: \"/Master/demo\" },\n";
                    }
                }
                else
                {
                    List<Database.CRMMainActivity> HELPDeskList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").ToList();
                    foreach (Database.CRMMainActivity item in HELPDeskList)
                    {
                        DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                        str += "{ title: \"" + item.Version + "\", start: new Date(" + sdt.Year + "," + Convert.ToInt32(sdt.Month - 1).ToString() + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\"purple\"), url: \"/Master/demo\" },\n";
                    }
                }
            }

            if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").Count() > 0)
            {
                List<Database.CRMMainActivity> GList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").GroupBy(p => p.GROUPCODE).Select(p => p.FirstOrDefault()).ToList();
                foreach (Database.CRMMainActivity item in GList)
                {
                    int ITGroup = Convert.ToInt32(item.GROUPCODE);
                    if (DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.ITGROUPID == ITGroup && p.USERCODE == Userid).Count() == 1)
                    {
                        List<Database.CRMMainActivity> HELPDeskList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.GROUPCODE == ITGroup && p.ACTIVITYE == "helpdesk").ToList();
                        foreach (Database.CRMMainActivity itemHD in HELPDeskList)
                        {
                            DateTime sdt = Convert.ToDateTime(item.UPDTTIME);
                            str += "{ title: \"" + itemHD.Version + "\", start: new Date(" + sdt.Year + "," + Convert.ToInt32(sdt.Month - 1).ToString() + "," + sdt.Day + "," + sdt.Hour + "," + sdt.Minute + "), backgroundColor: App.getBrandColor(\"purple\"), url: \"/Master/demo\" },\n";
                        }
                    }
                }
            }
            //str += "{ title: \"Birthday Party\", start: new Date(n, a, t + 1, 19, 0), end: new Date(n, a, t + 1, 22, 30), backgroundColor: App.getBrandColor(\"purple\"), allDay: !1 },\n";

            //str += "{ title: \"Docter Batra\", start: new Date(2018, 8,  6), end: new Date(2018, 8,  10), backgroundColor: (\"#db6944\"), url: \"/Master/demo\" },\n";
            //{ title: \"Opp_dateCH\", start: new Date(2018, 1,  21), end: new Date(2018, 2,  25), backgroundColor: App.getBrandColor(\"2\"), url: \"/Master/demo\" }
            //str += "{ title: \"All Day Event\", start: new Date(n, a, 1), backgroundColor: App.getBrandColor(\"yellow\") },\n";
            //str += "{ title: \"Long Event\", start: new Date(n, a, t - 5), end: new Date(n, a, t - 2), backgroundColor: App.getBrandColor(\"blue\") },\n";
            //str += "{ title: \"Repeating Event\", start: new Date(n, a, t - 3, 16, 0), allDay: !1, backgroundColor: App.getBrandColor(\"red\") },\n";
            //str += "{ title: \"Repeating Event\", start: new Date(n, a, t + 4, 16, 0), allDay: !1, backgroundColor: App.getBrandColor(\"green\") },\n";
            //str += "{ title: \"Meeting\", start: new Date(n, a, t, 10, 30), allDay: !1,backgroundColor: App.getBrandColor(\"blue\") },\n";
            //str += "{ title: \"Lunch\", start: new Date(n, a, t, 12, 0), end: new Date(n, a, t, 14, 0), backgroundColor: App.getBrandColor(\"grey\"), allDay: !1 },\n";
            //str += "{ title: \"Birthday Party\", start: new Date(n, a, t + 1, 19, 0), end: new Date(n, a, t + 1, 22, 30), backgroundColor: App.getBrandColor(\"purple\"), allDay: !1 },\n";
            //str += "{ title: \"Click for Google\", start: new Date(n, a, 28), end: new Date(n, a, 29), backgroundColor: App.getBrandColor(\"yellow\"), url: \"http://google.com/\" }\n";

            str += "]\n";
            str += "})\n";
            str += "}\n";
            str += "}\n";
            str += "}\n";
            str += "}(); jQuery(document).ready(function () { AppCalendar.init() });\n";
            str += "</script > \n";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", str);

        }
        public void binddrop()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //string sql = "SELECT  (recNo || ' - ' ||Receipe_English || ' - ' || Receipe_Arabic) as Receipe    FROM tbl_Receipe where TenentID = " + TID + " ";
            //SqlCommand CMD1 = new SqlCommand(sql, con);
            //SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            //DataSet ds = new DataSet();
            //ADB.Fill(ds);
            //DataTable dt = ds.Tables[0];

            drpReceip.DataSource = DB.tbl_Receipe.Where(p => p.TenentID == TID);
            drpReceip.DataTextField = "Receipe_English";
            drpReceip.DataValueField = "recNo";
            drpReceip.DataBind();
            drpReceip.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpjobServiceTem.DataSource = DB.tbl_Receipe.Where(p => p.TenentID == TID);
            drpjobServiceTem.DataTextField = "Receipe_English";
            drpjobServiceTem.DataValueField = "recNo";
            drpjobServiceTem.DataBind();
            drpjobServiceTem.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpCustomer.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.CompanyType == "82003");
            drpCustomer.DataTextField = "COMPNAME1";
            drpCustomer.DataValueField = "COMPID";
            drpCustomer.DataBind();
            drpCustomer.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpEmployee.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID);
            drpEmployee.DataTextField = "firstname";
            drpEmployee.DataValueField = "employeeID";
            drpEmployee.DataBind();
            drpEmployee.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpEmployee.DataSource = DB.USER_MST.Where(p => p.TenentID == TID);
            //drpEmployee.DataTextField = "LOGIN_ID";
            //drpEmployee.DataValueField = "USER_ID";
            //drpEmployee.DataBind();
            //drpEmployee.Items.Insert(0, new ListItem("-- Select --", "0"));

            //Set the Start Time Value
            DateTime start = DateTime.ParseExact("00:00", "HH:mm", null);
            //Set the End Time Value
            DateTime end = DateTime.ParseExact("23:55", "HH:mm", null);
            //Set the interval time 
            int interval = 5;
            //List to hold the values of intervals
            List<string> lstTimeIntervals = new List<string>();
            //Populate the list with 30 minutes interval values
            for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
                lstTimeIntervals.Add(i.ToString("hh:mm tt"));
            //Assign the list to datasource of dropdownlist
            drpTimejon.DataSource = lstTimeIntervals;
            //Databind the dropdownlist
            drpTimejon.DataBind();

        }
        protected void btnsaveAppoint_Click(object sender, EventArgs e)
        {
            //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //string Duscrption = txtTitleAP.Text;
            //string Name = "";
            //string Department = "0";
            //string Priority = (drpColor.SelectedValue);
            //DateTime sdate = Convert.ToDateTime(txtSdateAP.Text);
            //DateTime edate = Convert.ToDateTime(txtEnddateAP.Text);
            //string URL = "/Master/Appoiment";

            //Classes.CRMClass.InsertAppointment(0, TID, 1, Duscrption, sdate, edate, Priority.ToString(), URL, true, true, "Clinic", "Insert");
            AddAppointment();
        }
        public void AddAppointment()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string UNAME = (((USER_MST)Session["USER"]).LOGIN_ID);
            string userid = (((USER_MST)Session["USER"]).USER_ID).ToString();

        }

        public void DeActive(int TID, int CID, int MCID)
        {
            string sqlselect = "select * from CRMActivities where TenentID = " + TID + " and COMPID = " + CID + " and MasterCODE = " + MCID + " and Active = 'Y' ";

            DataTable dt = POS.DataCon.GetDataTable(sqlselect);
            int Count = dt.Rows.Count;
            if (Count > 0)
            {
                int TenentID = Convert.ToInt32(dt.Rows[0]["TenentID"]);
                int COMPID = Convert.ToInt32(dt.Rows[0]["COMPID"]);
                int MasterCODE = Convert.ToInt32(dt.Rows[0]["MasterCODE"]);

                string sqlupdate = " update CRMActivities set Active = 'N' where TenentID = " + TID + " and COMPID = " + CID + " and MasterCODE = " + MCID + " and  Active = 'Y'";
                POS.DataCon.ExecuteSQL(sqlupdate);
            }
        }
        public void ListAppoint()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (ViewState["HelpData"] != null)
            {
                List<Database.CRMMainActivity> HelpDeskdata = ((List<Database.CRMMainActivity>)ViewState["HelpData"]).ToList();
                Listview1.DataSource = HelpDeskdata.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").OrderBy(p => p.MasterCODE);
                Listview1.DataBind();
            }
        }
        public void JobList(int MasterID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            var result = (from CMA in DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == MasterID)
                          join CSA in DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == MasterID) on CMA.MasterCODE equals CSA.MasterCODE
                          where CMA.MasterCODE == MasterID
                          select new
                          {
                              CMA.MasterCODE,
                              CMA.ACTIVITYE,
                              CMA.Reference,
                              CMA.UPDTTIME,
                              CMA.TickDepartmentID,
                              CMA.TickComplainType,
                              CMA.TickPhysicalLocation,
                              CMA.TickCatID,
                              CMA.TickSubCatID,
                              CMA.Emp_ID,
                              CMA.GROUPCODE,
                              CMA.Remarks,
                              CMA.USERNAME,
                              CMA.MyStatus,
                              CSA.Version,
                              CSA.MyLineNo,
                              CSA.ActivityID,
                              CSA.InitialDate,
                              CSA.ActivityPerform
                          }
                             ).ToList();
            ListView2.DataSource = result;
            ListView2.DataBind();

            int Count = result.Count();
            if (Count > 0)
                LinkAddjob.Attributes["style"] = "block;";
        }
        public string gerColor(string ID)
        {
            if (ID == "No Status")
                return "blue";
            else if (ID == "Not Confirmed")
                return "red";
            else if (ID == "Confirmed")
                return "green";
            else if (ID == "No Answer")
                return "yellow";
            else if (ID == "In Waiting")
                return "purple";
            else if (ID == "Visited")
                return "gray";
            else if (ID == "Closed")
                return "indigo";
            else if (ID == "Canceled")
                return "aqua";
            else
                return "";
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "btnviewjob")
            {
                int MasterID = Convert.ToInt32(e.CommandArgument);
                JobList(MasterID);
            }
        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

        }
        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            Label Label14 = (Label)e.Item.FindControl("Label14");
            int MasterID = Convert.ToInt32(Label14.Text);
            Literal ltr = (Literal)e.Item.FindControl("ltr");
            List<Database.CRMProgHW> CRMHW = DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == MasterID).ToList();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < CRMHW.Count(); i++)
            {
                int j = i + 1;
                string URL = CRMHW[i].URLRewrite.ToString() + "?ACT=" + CRMHW[i].ActivityID.ToString() + "&STD=" + CRMHW[i].Parameter2.ToString();
                //str.Append("<asp:LinkButton Style='padding-bottom: 0px; padding-top: 0px; height: auto' ID='Link" + j.ToString() + "' PostBackUrl='" + CRMHW[i].URLRewrite.ToString() + "' runat='server' '>"+ CRMHW[i].ButtionName.ToString() +"</asp:LinkButton>");
                str.Append("<a href='" + URL + "' Style='padding-bottom: 0px; padding-top: 0px; height: auto'>" + CRMHW[i].ButtionName.ToString() + "</a>");
                //<a href="/POS/ClientTiketR.aspx">reply</a>
            }
            ltr.Text = str.ToString();

            //         private void GenerateButtons()
            //{ 
            //        StringBuilder str = new StringBuilder(); 
            //        for (int i = 1; i <= 10; i++)
            //        {
            //            str.Append("<input type='button' id='btn" + i.ToString() + "' value='Button:" + i.ToString() +
            //                       "' onclick='javascript:__doPostBack(\"btnAsp\",\"" + i.ToString() + "\");' >"); 
            //        }
            //        ltr.Text = str.ToString();  
            //}
            //private void btnAsp_Click(object sender, System.EventArgs e)
            //{
            //    Response.Write("You Clicked on "+Request.Form["__EVENTARGUMENT"].ToString());  
            //}   
        }
        protected void jobedit_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string UNAME = (((USER_MST)Session["USER"]).LOGIN_ID);
            string userid = (((USER_MST)Session["USER"]).USER_ID).ToString();
        }
        public void JobClear()
        {
            drpjobServiceTem.SelectedIndex = 0;
            txtjobTitle.Text = "";
            txtjobremark.Text = "";
            jobedit.Text = "Save";
            ViewState["MID"] = null;
        }
        protected void drpReceip_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int RCP = Convert.ToInt32(drpReceip.SelectedValue);
            string remark = "";
            if (RCP != 0)
            {
                txtAppoitmentTitle.Text = drpReceip.SelectedItem.ToString();
            }
            List<Database.Receipe_Menegement> RecList = DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == RCP).ToList();
            foreach (Database.Receipe_Menegement item in RecList)
            {
                string QTY = item.Qty.ToString();
                long prod = Convert.ToInt64(item.ItemCode);
                string Itemname = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == prod).Count() == 1 ? DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == prod).ProdName1 : "Item Not Found";
                remark += "Item= '" + Itemname + "' QTY= '" + QTY + "' ,\n";
            }
            txtRemark.Text = remark;
            ModalPopupExtender5.Show();
        }

        protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            if (CID != 0)
            {
                string Appval = txtAppoitmentTitle.Text;
                Appval = Appval + "_" + drpCustomer.SelectedItem.ToString();
                txtAppoitmentTitle.Text = Appval;
            }
            ModalPopupExtender5.Show();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            ListAppoint();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindOwnDateCalander();
        }
        public string GetUName(int UID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == UID).Count() > 0)
            {
                string UName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
                return UName;
            }
            else
            {
                return "Not Found";
            }

        }

        public string GetLOCDEPT(int Dept, int LOCT)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string DETPLOCT = "";
            if (DB.DeptITSupers.Where(p => p.TenentID == TID && p.DeptID == Dept).Count() > 0)
            {
                DETPLOCT += DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Dept).DeptName;
            }
            else
            {
                DETPLOCT += "";
            }
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == LOCT && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").Count() > 0)
            {
                DETPLOCT += " / " + DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == LOCT && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;
            }
            else
            {
                DETPLOCT += " / ";
            }
            return DETPLOCT;
        }

        public string Performer(int per)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == per).Count() == 1)
            {
                return DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == per).firstname;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label StatusColor = (Label)e.Item.FindControl("lblapp5");
            string STColor = StatusColor.Text.ToString();

            if (STColor == "Pending")
                StatusColor.Attributes["style"] = "background-color:#1bbc9b;";
            else if (STColor == "Closed")
                StatusColor.Attributes["style"] = "background-color:#F3565D;";
            else if (STColor == "Completed")
                StatusColor.Attributes["style"] = "background-color:#9b59b6;";
            else if (STColor == "Progress")
                StatusColor.Attributes["style"] = "background-color:#F8CB00;";
            else if (STColor == "Delivered")
                StatusColor.Attributes["style"] = "background-color:#89C4F4;"; 
            
        }

        public string Calecolor(string stutus)
        {
            if (stutus == "Pending")
                return "green";
            else if (stutus == "Closed")
                return "red";
            else if (stutus == "Completed")
                return "purple";
            else if (stutus == "Progress")
                return "yellow";
            else if (stutus == "Delivered")
                return "blue";
            else
                return "grey";
        }










    }

}