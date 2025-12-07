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
                    ListHistoy.DataSource = CRMACTList;
                    ListHistoy.DataBind();

                    // Use FirstOrDefault instead of Single to avoid "Sequence contains more than one element"
                    var objCRMMainActivities = DB.CRMMainActivities.FirstOrDefault(p => p.TenentID == TID && p.MasterCODE == Master);
                    if (objCRMMainActivities == null)
                    {
                        // No matching main activity found - set defaults and exit
                        txtmessage.Text = string.Empty;
                        txtdepartment.Text = string.Empty;
                        txtlocation.Text = string.Empty;
                        txtpatientname.Text = string.Empty;
                        txtMRN.Text = string.Empty;
                        txtcontact.Text = string.Empty;
                        txtcate.Text = string.Empty;
                        txtsubcat.Text = string.Empty;
                        txtreport.Text = string.Empty;
                        txtcomplaint.Text = Convert.ToString(Master);
                        return;
                    }

                    int crupID = objCRMMainActivities.CRUP_ID != null ? Convert.ToInt32(objCRMMainActivities.CRUP_ID) : 0;
                    listcrupId.DataSource = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == crupID);
                    listcrupId.DataBind();

                    objCRMMainActivities.RouteID = 1;
                    //txtFeedback.Text = objCRMMainActivities.FeedbackNumber;
                    txtdates.Text = objCRMMainActivities.UploadDate != null ? objCRMMainActivities.UploadDate.ToString() : txtdates.Text;

                    int deptID = objCRMMainActivities.TickDepartmentID != null ? Convert.ToInt32(objCRMMainActivities.TickDepartmentID) : 0;
                    var dept = DB.DeptITSupers.FirstOrDefault(p => p.TenentID == TID && p.DeptID == deptID);
                    string name = dept != null ? dept.DeptName : string.Empty;

                    //int comptype = Convert.ToInt32(objCRMMainActivities.TickFeedbackype);
                    //string comname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "Feedback" && p.REFID == comptype).REFNAME1;
                    int phID = objCRMMainActivities.TickPhysicalLocation != null ? Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation) : 0;
                    var phRef = DB.REFTABLEs.FirstOrDefault(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == phID);
                    string phname = phRef != null ? phRef.REFNAME1 : string.Empty;

                    int catID = objCRMMainActivities.TickCatID != null ? Convert.ToInt32(objCRMMainActivities.TickCatID) : 0;
                    var cat = DB.ICCATEGORies.FirstOrDefault(p => p.TenentID == TID && p.CATID == catID);
                    string catname = cat != null ? cat.CATNAME : string.Empty;

                    int subcatID = objCRMMainActivities.TickSubCatID != null ? Convert.ToInt32(objCRMMainActivities.TickSubCatID) : 0;
                    var subcat = DB.ICSUBCATEGORies.FirstOrDefault(p => p.TenentID == TID && p.SUBCATID == subcatID);
                    string subcatname = subcat != null ? subcat.SUBCATNAME : string.Empty;

                    int repID = objCRMMainActivities.ReportedBy != null ? Convert.ToInt32(objCRMMainActivities.ReportedBy) : 0;
                    var emp = DB.tbl_Employee.FirstOrDefault(p => p.TenentID == TID && p.employeeID == repID);
                    string username = emp != null ? emp.firstname : string.Empty;

                    string des = objCRMMainActivities.Remarks ?? string.Empty;

                    if (DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == Master).Any())
                    {
                        List<Database.tbl_DMSAttachmentMst> ttachOBJ = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == Master).ToList();
                        lblattach.Text = ttachOBJ.Count().ToString();
                    }

                    int comptype = objCRMMainActivities.TickComplainType != null ? Convert.ToInt32(objCRMMainActivities.TickComplainType) : 0;
                    var comRef = DB.REFTABLEs.FirstOrDefault(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "complain" && p.REFID == comptype);
                    string comname = comRef != null ? comRef.REFNAME1 : string.Empty;

                    //  Database.CRMActivity crma = DB.CRMActivities.Where(p=>p.TenentID == TID && p.MasterCODE == Master).OrderBy(p => p.MyLineNo)

                    var record = DB.CRMActivities.FirstOrDefault(p => p.TenentID == TID && p.MasterCODE == Master && p.Subject != null);


                    if (record != null)
                    {
                        int cma = record.investigation != null ? Convert.ToInt32(record.investigation) : 0;

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
                    txtpatientname.Text = objCRMMainActivities.Patient_Name ?? string.Empty;
                    txtMRN.Text = objCRMMainActivities.MRN ?? string.Empty;
                    txtcontact.Text = objCRMMainActivities.Contact ?? string.Empty;
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
