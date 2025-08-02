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
    public partial class ViewTick : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DateTime dt = DateTime.Now;
                txtdates.Text = dt.ToString();
                txtdates.Enabled = false;

                if (Request.QueryString["Mastercode"] != null)
                {
                    int TID = 10;
                    int Master = Convert.ToInt32(Request.QueryString["Mastercode"]);
                   

                    List<Database.CRMActivity> CRMACTList = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Master).ToList();
                    listChet.DataSource = CRMACTList;
                    listChet.DataBind();
                    ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Master).ToList();
                    ListHistoy.DataBind();
                    int crupID =Convert.ToInt32(DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Master).CRUP_ID);
                    listcrupId.DataSource = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == crupID);
                    listcrupId.DataBind();                    
                    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Master); objCRMMainActivities.RouteID = 1;
                    txtcomplaint.Text = objCRMMainActivities.ComplaintNumber;
                    txtdates.Text =  objCRMMainActivities.UploadDate.ToString();
                    int deptID =Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
                    string name = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == deptID).DeptName;
                    int comptype = Convert.ToInt32(objCRMMainActivities.TickComplainType);
                    string comname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "complain" && p.REFID == comptype).REFNAME1;
                    int phID = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
                    string phname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == phID).REFNAME1;
                    int catID = Convert.ToInt32(objCRMMainActivities.TickCatID);
                    string catname = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == catID).CATNAME;
                    int subcatID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
                    string subcatname = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == subcatID).SUBCATNAME;
                    int repID = Convert.ToInt32(objCRMMainActivities.ReportedBy);
                    string username = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == repID).firstname;
                    string des = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Master).Remarks;
                    txtdepartment.Enabled = false;
                    txtlocation.Enabled = false;
                    txtpatientname.Enabled = false;
                    txtMRN.Enabled = false;
                    txtcontact.Enabled = false;
                    txtcate.Enabled = false;
                    txtsubcat.Enabled = false;
                    txtsubcat.Enabled = false;
                    txtcomplaint.Enabled = false;
                    txtmessage.Enabled = false;
                    txtcomplaintype.Enabled = false;
                    txtreport.Enabled = false;
                    txtdepartment.Text = name;
                    txtreport.Text = username;
                    txtcomplaintype.Text = comname;
                    txtlocation.Text = phname;
                    txtpatientname.Text = objCRMMainActivities.Patient_Name;
                    txtMRN.Text = objCRMMainActivities.MRN;
                    txtcontact.Text = objCRMMainActivities.Contact;
                    txtcate.Text = catname;
                    txtsubcat.Text = subcatname;
                    txtreport.Text = username;
                    txtmessage.Text = des;
                     //Complaint 1234567 View Only Dated dd-mmm-yyyy MRN #####
                    lblUser.Text = "View Only Complaint " + objCRMMainActivities.ComplaintNumber + " Dated = " + txtdates.Text + " MRN " + objCRMMainActivities.MRN;
                }
            }
        }

       
        
       
        protected void lnkback_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Sales/POSIndex.aspx");
        }
    }
}