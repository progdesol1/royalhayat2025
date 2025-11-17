using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Database;

namespace Web.POS
{
  public partial class ViewTicket : System.Web.UI.Page
  {
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
    SqlCommand command2 = new SqlCommand();
    CallEntities DB = new CallEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        DateTime dt = DateTime.Now;
        txtdates.Text = dt.ToString();
        txtdates.Enabled = false;

        if (Request.QueryString["Mastercode"] != null)
        {
          int TID = 10;//Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          int Location = 1;
          int Master = Convert.ToInt32(Request.QueryString["Mastercode"]);


          List<Database.CRMActivity> CRMACTList = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Master).ToList();
          listChet.DataSource = CRMACTList;
          listChet.DataBind();
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Master).ToList();
          ListHistoy.DataBind();
          int crupID = Convert.ToInt32(DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Master).CRUP_ID);
          listcrupId.DataSource = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == crupID);
          listcrupId.DataBind();
          Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Master); objCRMMainActivities.RouteID = 1;
          //txtFeedback.Text = objCRMMainActivities.FeedbackNumber;
          txtdates.Text = objCRMMainActivities.UploadDate.ToString();
          int deptID = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
          string name = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == deptID).DeptName;
          //int comptype = Convert.ToInt32(objCRMMainActivities.TickFeedbackype);
          //string comname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "Feedback" && p.REFID == comptype).REFNAME1;
          int phID = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
          string phname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == phID).REFNAME1;
          int catID = Convert.ToInt32(objCRMMainActivities.TickCatID);
          string catname = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == catID).CATNAME;
          int subcatID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
          string subcatname = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == subcatID).SUBCATNAME;
          int repID = Convert.ToInt32(objCRMMainActivities.ReportedBy);
          string username = DB.tbl_Employee.First(p => p.TenentID == TID && p.employeeID == repID).firstname;
          string des = DB.CRMMainActivities.First(p => p.TenentID == TID && p.MasterCODE == Master).Remarks;
          if (DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == Master).Count() != 0)
          {
            List<Database.tbl_DMSAttachmentMst> ttachOBJ = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == Master).ToList();
            lblattach.Text = ttachOBJ.Count().ToString();
          }

          int comptype = Convert.ToInt32(objCRMMainActivities.TickComplainType);
          string comname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "complain" && p.REFID == comptype).REFNAME1;

          //  Database.CRMActivity crma = DB.CRMActivities.Where(p=>p.TenentID == TID && p.MasterCODE == Master).OrderBy(p => p.MyLineNo)

          var record = DB.CRMActivities.FirstOrDefault(p => p.TenentID == TID && p.MasterCODE == Master && p.Subject != null);


          if (record != null)
          {
            int cma = Convert.ToInt32(record.investigation);

            if (cma == 1)
              txtrelevant.Text = "Relevant";
            else if (cma == 2)
              txtrelevant.Text = "Irrelevant";
            else
              txtrelevant.Text = "Unknown";
          }
          else
          {
            txtrelevant.Text = "No matching record found";
          }


          txtdepartment.Enabled = false;
          txtlocation.Enabled = false;
          txtpatientname.Enabled = false;
          txtMRN.Enabled = false;
          txtcontact.Enabled = false;
          txtcate.Enabled = false;
          txtsubcat.Enabled = false;
          txtsubcat.Enabled = false;
          //txtFeedback.Enabled = false;
          txtmessage.Enabled = false;
          //txtFeedbackype.Enabled = false;
          txtrelevant.Enabled = false;
          txtreport.Enabled = false;
          lblattach.Enabled = true;
          txtcomplaint.Enabled = false;
          txtcomplaintype.Enabled = false;
          txtcomplaintype.Text = comname;

          txtdepartment.Text = name;
          txtreport.Text = username;
          //txtFeedbackype.Text = comname;
          txtlocation.Text = phname;
          txtpatientname.Text = objCRMMainActivities.Patient_Name;
          txtMRN.Text = objCRMMainActivities.MRN;
          txtcontact.Text = objCRMMainActivities.Contact;
          txtcate.Text = catname;
          txtsubcat.Text = subcatname;
          txtreport.Text = username;
          txtmessage.Text = des;
          txtcomplaint.Text = Convert.ToString(Master);
          //lblUser.Text = "" + objCRMMainActivities.FeedbackNumber + " & MRN No = "+objCRMMainActivities.MRN +" & Date = "+txtdates.Text+"";
        }
      }
    }

    protected void lnkback_Click(object sender, EventArgs e)
    {
      Response.Redirect("/ACM/DemoPOS.aspx");
    }

    protected void lnkattach_Click(object sender, EventArgs e)
    {
      int TID = 10;//Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int Master = Convert.ToInt32(Request.QueryString["Mastercode"]);
      //int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      string Type = "Ticket";
      string Url = "../CRM/AttachmentMst.aspx?MasterID=" + Master + "&DID=" + Type + "&RefNo=" + 11058;//+ "&recodInsert=" + tiki;
      string s = "window.open('" + Url + "', 'popup_window', 'width=950,height=225,left=100,top=100,resizable=yes');";
      ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", s, true);

    }
  }
}
