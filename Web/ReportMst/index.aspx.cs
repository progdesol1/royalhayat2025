using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.ReportMst
{
    public partial class index : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            List=DB.Appointments.Where(p => p.Deleted == true && p.Active == true).ToList();
            foreach (Database.Appointment item in List)
            {
                DateTime sdt = Convert.ToDateTime(item.StartDt);
                DateTime edt = Convert.ToDateTime(item.EndDt);
                int smnt = Convert.ToInt32(sdt.Month-1);
                int emnt = Convert.ToInt32(edt.Month-1);
                if (item.url != null || item.url != "")
                {
                   
                    if (sdt.Date == edt.Date)
                        str += "{ title: \"" + item.Title + "\", start: new Date(" + sdt.Year + "," + smnt  + "," + sdt.Day + "), backgroundColor: App.getBrandColor(\"" + item.Color + "\"), url: \"" + item.url + "\" },\n";
                    else
                        str += "{ title: \"" + item.Title + "\", start: new Date(" + sdt.Year + ", " + smnt + ",  " + sdt.Day + "), end: new Date(" + edt.Year + ", " + edt.Month + ",  " + edt.Day + "), backgroundColor: App.getBrandColor(\"" + item.Color + "\"), url: \"" + item.url + "\" },\n";
                }
                else
                {
                    if (sdt.Date == edt.Date)
                        str += "{ title: \"" + item.Title + "\", start: new Date(" + sdt.Year + ", " + emnt + ",  " + sdt.Day + "), backgroundColor: App.getBrandColor(\" " + item.Color + "\") },\n";
                    else
                        str += "{ title: \"" + item.Title + "\", start: new Date(" + sdt.Year + ", " + emnt + ",  " + sdt.Day + "), end: new Date(" + edt.Year + ", " + edt.Month + ",  " + edt.Day + "), backgroundColor: App.getBrandColor(\"" + item.Color + "\") },\n";
                }
            }
          
            str += "{ title: \"All Day Event\", start: new Date(n, a, 1), backgroundColor: App.getBrandColor(\"yellow\") },\n";
            str += "{ title: \"Long Event\", start: new Date(n, a, t - 5), end: new Date(n, a, t - 2), backgroundColor: App.getBrandColor(\"blue\") },\n";
            str += "{ title: \"Repeating Event\", start: new Date(n, a, t - 3, 16, 0), allDay: !1, backgroundColor: App.getBrandColor(\"red\") },\n";
            str += "{ title: \"Repeating Event\", start: new Date(n, a, t + 4, 16, 0), allDay: !1, backgroundColor: App.getBrandColor(\"green\") },\n";
            str += "{ title: \"Meeting\", start: new Date(n, a, t, 10, 30), allDay: !1,backgroundColor: App.getBrandColor(\"blue\") },\n";
            str += "{ title: \"Lunch\", start: new Date(n, a, t, 12, 0), end: new Date(n, a, t, 14, 0), backgroundColor: App.getBrandColor(\"grey\"), allDay: !1 },\n";
            str += "{ title: \"Birthday Party\", start: new Date(n, a, t + 1, 19, 0), end: new Date(n, a, t + 1, 22, 30), backgroundColor: App.getBrandColor(\"purple\"), allDay: !1 },\n";
            str += "{ title: \"Click for Google\", start: new Date(n, a, 28), end: new Date(n, a, 29), backgroundColor: App.getBrandColor(\"yellow\"), url: \"http://google.com/\" }\n";
            str += "]\n";
            str += "})\n";
            str += "}\n";
            str += "}\n";
            str += "}\n";
            str += "}(); jQuery(document).ready(function () { AppCalendar.init() });\n";
            str += "</script > \n";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", str);
        }


    }
}