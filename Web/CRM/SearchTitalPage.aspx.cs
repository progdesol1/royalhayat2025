using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.CRM
{
    public partial class SearchTitalPage : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID11 = 0;
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                if (Request.QueryString["TID"] != null && Request.QueryString["CampID"] != null && Request.QueryString["LeadName"] != null)
                {
                    TID11 = Convert.ToInt32(Request.QueryString["TID"]);
                    int CamID = Convert.ToInt32(Request.QueryString["CampID"]);
                    if (DB.REFTABLEs.Where(p => p.REFID == TID11&&p.TenentID==TID).Count() > 0)
                    {
                        Database.REFTABLE obj_Ref = DB.REFTABLEs.SingleOrDefault(p => p.REFID == TID11&&p.TenentID==TID);
                        if (obj_Ref.REFSUBTYPE == Convert.ToString(SearchManagement.SearchType.Company))
                        {
                            BindCompany(TID11);
                        }
                        else
                        {
                            BindContact(TID11);
                        }
                    }
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
        public void BindCompany(int TiID)
        {
            pnlCompany.Visible = true;
            pnlContact.Visible = false;
            int TitleID = Convert.ToInt32(TiID);

            List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID&&p.TenentID==TID).ToList();
            List<Database.TBLCOMPANYSETUP> Con_List = new List<Database.TBLCOMPANYSETUP>();
            foreach (ISSearchDetail item in Search_List)
            {
                Database.TBLCOMPANYSETUP obj_Contact = DB.TBLCOMPANYSETUPs.Single(p =>p.TenentID==TID&& p.COMPID == item.CompanyID&&p.TenentID==TID);
                Con_List.Add(obj_Contact);
            }
            listCustomerMasterCompany.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
            listCustomerMasterCompany.DataBind();
        }
        public string getcompniy(int CID)
        {
            return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == CID&&p.TenentID==TID).COMPNAME;
        }

        public string getcity(int GCID)
        {
            return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == GCID&&p.TenentID==TID).CITY;
        }
        public void BindContact(int CoTiID)
        {
            pnlCompany.Visible = false;
            pnlContact.Visible = true;
            int TitleID = Convert.ToInt32(CoTiID);


            List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID&&p.TenentID==TID).ToList();
            List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();

            foreach (ISSearchDetail item in Search_List)
            {
                Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactID&&p.TenentID==TID);
                Con_List.Add(obj_Contact);
            }
            listContactMaster.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
            listContactMaster.DataBind();
        }
        public string getStste1(int SID)
        {
            return DB.tblStates.SingleOrDefault(p => p.StateID == SID).MYNAME1;
        }
        public string getStste11(int SID)
        {
            return DB.tblStates.SingleOrDefault(p => p.StateID == SID).MYNAME1;
        }

        protected void listContactMaster_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnNavigate")
            {
                if (Request.QueryString["CampID"] != null)
                {
                    int CamID = Convert.ToInt32(Request.QueryString["CampID"]);
                    HiddenField hidecontactid = (HiddenField)e.Item.FindControl("hidecontactid");
                    Label lblCustomerName = (Label)e.Item.FindControl("lblCustomerName");
                    string CNAM = lblCustomerName.Text;
                    TID11 = Convert.ToInt32(Request.QueryString["TID"]);
                    int ContaID = Convert.ToInt32(hidecontactid.Value);
                    int GrupID = Convert.ToInt32(Request.QueryString["GrupID"]);


                    //(e.CommandArgument);
                    //LinkButton linknavigate = (LinkButton)e.Item.FindControl("LinkButton3");
                    //linknavigate.PostBackUrl = "QuestionList.aspx?ConID=" + ContaID + "&CampID=" + CamID;
                    string Name = Request.QueryString["LeadName"].ToString();
                    Response.Redirect("QuestionList.aspx?ConID=" + ContaID + "&CampID=" + CamID + "&LeadName=" + Name + "&TitleID11=" + TID11 + "&GrupID=" + GrupID + "&ContectName=" + CNAM);
                }

            }
        }

        protected void listCustomerMasterCompany_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {
                if (Request.QueryString["CampID"] != null)
                {
                    int GrupID = Convert.ToInt32(Request.QueryString["GrupID"]);
                    //hidecompanyctid
                    int CamID = Convert.ToInt32(Request.QueryString["CampID"]);
                    HiddenField hidecompanyctid = (HiddenField)e.Item.FindControl("hidecompanyctid");
                    Label lblCustomerName = (Label)e.Item.FindControl("lblCustomerName");
                    string CNAM = lblCustomerName.Text;
                    int CompID = Convert.ToInt32(hidecompanyctid.Value);//(e.CommandArgument);
                    LinkButton linknavigate1 = (LinkButton)e.Item.FindControl("LinkButton1");
                    TID11 = Convert.ToInt32(Request.QueryString["TID"]);
                    string Name = Request.QueryString["LeadName"].ToString();
                    Response.Redirect("QuestionList.aspx?CompID=" + CompID + "&CampID=" + CamID + "&LeadName=" + Name + "&TitleID11=" + TID11 + "&GrupID=" + GrupID + "&Compname=" + CNAM);
                    // linknavigate1.PostBackUrl = "QuestionList.aspx?CompID=" + CompID + "&CampID=" + CamID;
                }

            }
        }
    }
}