using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Web.CRM
{
    public partial class DashBoard : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                BindLead();
                pnloppertunity.Visible = false;
                pnlLead.Visible = false;
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
        public void BindLead()
        {
            ListViewCampaign.DataSource = DB.tbl_Campaign_Mst.Where(p => p.TenentID == TID && p.Active == true);
            ListViewCampaign.DataBind();

            //lstOpportunity.DataSource = DB.tbl_Opportunity_Mst.Where(p => p.TenentID == TID);
            //lstOpportunity.DataBind();


            //lstLead.DataSource = DB.tbl_Lead_Mst.Where(p => p.TenentID == TID);
            //lstLead.DataBind();
        }

        protected void ListViewCampaign_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnOpportunity")
            {
                int OppID = Convert.ToInt32(e.CommandArgument);
                Label lblTenet = (Label)e.Item.FindControl("lblTenet");
                lblcampinname.Text = " " + lblTenet.Text;
                pnloppertunity.Visible = true;
                pnlLead.Visible = false;
                Session["Tools"] = OppID;
                Session["PageType"] = ActionMaster.Type.Campaign.ToString();
              //  Control setproject = (Control)LoadControl("UserControl/RightPanelUC.ascx");

                Control ctrl = Page.LoadControl("UserControl/RightPanelUC.ascx");
              //  divtools.Controls.Clear();
              //  divtools.Controls.Add(ctrl);
                BindOppertunity(OppID);
               
            }
        }

        protected void lstOpportunity_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnLead")
            {
                string  CID = "";
                int LeadID = Convert.ToInt32(e.CommandArgument);
                Label lblTenet = (Label)e.Item.FindControl("lblTenet");
                Label lblcustomername = (Label)e.Item.FindControl("lblcustomername");
                if (lblcustomername.Text != "")
                    CID = lblcustomername.Text;
                lblOpportname.Text = " " + lblTenet.Text;
                pnloppertunity.Visible = true;
                pnlLead.Visible = true;
                Session["Tools"] = LeadID;
                Session["PageType"] = ActionMaster.Type.Opprtutnity.ToString();
                Control ctrl = Page.LoadControl("UserControl/RightPanelUC.ascx");
              //  divtools.Controls.Clear();
              //  divtools.Controls.Add(ctrl);
                BindLead(LeadID, CID);
            }
        }
        public void BindOppertunity(int OppID)
        {
            int ID = Convert.ToInt32(OppID);
            List<Database.tbl_Opportunity_Mst> Opp_List = DB.tbl_Opportunity_Mst.Where(p => p.Campaign_ID == OppID && p.TenentID == TID).ToList();
            if (Opp_List.Count() > 0)
            {
                lstOpportunity.DataSource = Opp_List;
                lstOpportunity.DataBind();
            }
            else
            {
                lstOpportunity.DataSource = null;
                lstOpportunity.DataBind();
            }
        }

        public void BindLead(int LeadID, string cid)
        {
            List<Database.tbl_Lead_Mst> Lead_List = DB.tbl_Lead_Mst.Where(p => p.Oppertunity_ID == LeadID && p.TenentID == TID && p.Customer_Name == cid).ToList();
            if (Lead_List.Count() > 0)
            {
                lstLead.DataSource = Lead_List;
                lstLead.DataBind();
            }
            else
            {
                lstLead.DataSource = null;
                lstLead.DataBind();
            }
        }

        public string GetOppCount(int ID)
        {
            int CampID = Convert.ToInt32(ID);
            string Count = "0";
            List<Database.tbl_Opportunity_Mst> Opp_List = DB.tbl_Opportunity_Mst.Where(p => p.Campaign_ID == CampID && p.TenentID == TID).ToList();
            if (Opp_List.Count() > 0)
            {
                Count = Opp_List.Count().ToString();
            }
            return Count;
        }
        public string GetLeadCount(int ID)
        {
            int LeadID = Convert.ToInt32(ID);
            string LeadCount = "0";
            List<Database.tbl_Lead_Mst> Lead_List = DB.tbl_Lead_Mst.Where(p => p.Oppertunity_ID == LeadID && p.TenentID == TID).ToList();
            if (Lead_List.Count() > 0)
            {
                LeadCount = Lead_List.Count().ToString();
            }
            return LeadCount;
        }

        public string GetCustomerName(int CustoID)
        {
            if (CustoID != 0 && CustoID != null)
                return DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == CustoID && p.TenentID == TID).COMPNAME1;
            else
                return "";
        }
        public string GetStatusName(int StatusId)
        {
            if (StatusId != 0)
            {
                string Name = DB.REFTABLEs.Single(p => p.REFID == StatusId && p.TenentID == TID).REFNAME1;
                return Name;
            }
            else
            {
                return "";
            }
        }
        public string getCampid(int ID)
        {
            if (ID != 0)
            {
                string Name = DB.tbl_Campaign_Mst.Single(p => p.ID == ID && p.TenentID == TID).Name;
                return Name;
            }
            else
            {
                return "";
            }
        }

        public string GetProjectName(int ProjectID)
        {
            int SID = Convert.ToInt32(ProjectID);
            string ProName = "";
            if (DB.tbl_Lead_Mst.Where(p => p.ID == SID && p.TenentID == TID).Count() > 0)
            {
                int ProID = Convert.ToInt32(DB.tbl_Lead_Mst.Single(p => p.ID == SID && p.TenentID == TID).Project_id);

                List<Database.TBLPROJECT> List = DB.TBLPROJECTs.Where(p => p.PROJECTID == ProID && p.TenentID == TID).ToList();
                if (List.Count() > 0)
                {
                    ProName = List.Single(p => p.PROJECTID == ProID && p.TenentID == TID).PROJECTNAME1;
                }

            }
            return ProName;
        }


     

        public string GetOppProjectName(int ProjectID)
        {
            int SID = Convert.ToInt32(ProjectID);
            string ProName = "";
            if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == SID && p.TenentID == TID).Count() > 0)
            {
                int ProID = Convert.ToInt32(DB.tbl_Opportunity_Mst.Single(p => p.OpportID == SID && p.TenentID == TID).Project_id);

                List<Database.TBLPROJECT> List = DB.TBLPROJECTs.Where(p => p.PROJECTID == ProID && p.TenentID == TID).ToList();
                if (List.Count() > 0)
                {
                    ProName = List.Single(p => p.PROJECTID == ProID && p.TenentID == TID).PROJECTNAME1;
                }

            }
            return ProName;
        }


        //this portion starting with a calander control to display..


        [System.Web.Services.WebMethod(true)]
        public static string UpdateEvent(CalendarEvent cevent)
        {
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
            if (idList != null && idList.Contains(cevent.id))
            {
                if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
                {
                    EventDAO.updateEvent(cevent.id, cevent.title, cevent.description);

                    return "updated event with id:" + cevent.id + " update title to: " + cevent.title +
                    " update description to: " + cevent.description;
                }

            }

            return "unable to update event with id:" + cevent.id + " title : " + cevent.title +
                " description : " + cevent.description;
        }

        //this method only updates start and end time
        //this is called when a event is dragged or resized in the calendar
        [System.Web.Services.WebMethod(true)]
        public static string UpdateEventTime(ImproperCalendarEvent improperEvent)
        {
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
            if (idList != null && idList.Contains(improperEvent.id))
            {
                // FullCalendar 1.x
                //EventDAO.updateEventTime(improperEvent.id,
                //    DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                //    DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));

                // FullCalendar 2.x
                EventDAO.updateEventTime(improperEvent.id,
                                         Convert.ToDateTime(improperEvent.start).ToUniversalTime(),
                                         Convert.ToDateTime(improperEvent.end).ToUniversalTime(),
                                         improperEvent.allDay);  //allDay parameter added for FullCalendar 2.x

                return "updated event with id:" + improperEvent.id + " update start to: " + improperEvent.start +
                    " update end to: " + improperEvent.end;
            }

            return "unable to update event with id: " + improperEvent.id;
        }

        //called when delete button is pressed
        [System.Web.Services.WebMethod(true)]
        public static String deleteEvent(int id)
        {
            //idList is stored in Session by JsonResponse.ashx for security reasons
            //whenever any event is update or deleted, the event id is checked
            //whether it is present in the idList, if it is not present in the idList
            //then it may be a malicious user trying to delete someone elses events
            //thus this checking prevents misuse
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
            if (idList != null && idList.Contains(id))
            {
                EventDAO.deleteEvent(id);
                return "deleted event with id:" + id;
            }

            return "unable to delete event with id: " + id;
        }

        //called when Add button is clicked
        //this is called when a mouse is clicked on open space of any day or dragged 
        //over mutliple days
        [System.Web.Services.WebMethod]
        public static int addEvent(ImproperCalendarEvent improperEvent)
        {
            // FullCalendar 1.x
            //CalendarEvent cevent = new CalendarEvent()
            //{
            //    title = improperEvent.title,
            //    description = improperEvent.description,
            //    start = DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
            //    end = DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            //};

            // FullCalendar 2.x
            CalendarEvent cevent = new CalendarEvent()
            {
                title = improperEvent.title,
                description = improperEvent.description,
                start = Convert.ToDateTime(improperEvent.start).ToUniversalTime(),
                end = Convert.ToDateTime(improperEvent.end).ToUniversalTime(),
                allDay = improperEvent.allDay
            };

            if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
            {
                int key = EventDAO.addEvent(cevent);

                List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

                if (idList != null)
                {
                    idList.Add(key);
                }

                return key; //return the primary key of the added cevent object
            }

            return -1; //return a negative number just to signify nothing has been added
        }

        private static bool CheckAlphaNumeric(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");
        }

        protected void lstOpportunity_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string CID = "";
            Label lblcustomername = (Label)e.Item.FindControl("lblcustomername");
            Label lbloppp = (Label)e.Item.FindControl("lbloppp");
            Label lblcountlead = (Label)e.Item.FindControl("lblcountlead");
            int OPPID = Convert.ToInt32(lbloppp.Text);
            if (lblcustomername.Text != "")
                CID = lblcustomername.Text;
            List<Database.tbl_Lead_Mst> Lead_List = DB.tbl_Lead_Mst.Where(p => p.Oppertunity_ID == OPPID && p.TenentID == TID && p.Customer_Name == CID).ToList();
            int CUNT = Lead_List.Count();
            lblcountlead.Text = CUNT.ToString();
        }

    }


}