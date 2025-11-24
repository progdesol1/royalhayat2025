using Classes;
using Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Web.ReportMst
{
    public partial class DayReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {

                DateTime EDT = DateTime.Now.AddDays(0);
                txtdateTO.Text = EDT.ToString("dd/MM/yyyy");
                DateTime SDT = DateTime.Now.AddDays(-1);
                txtdateFrom.Text = SDT.ToString("dd/MM/yyyy");
                bindterminal();
                Department();
                subcategoryto();
                subcategoryfrom();
                categoryfrom();
                categoryto();
                complainfrom();
                complainto();
                ReportTo();
                reportfrom();
                Locationfrom();
                Locationto();


            }
        }


        public void bindterminal()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(DeptID) + ' - ' + DeptName as DN,DeptID  " +
                                    " from  DeptITSuper " +
                                    " where TenentID =" + TID + " " +
                                    " order by DeptID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpdepartmentfrom.DataSource = dt;
            drpdepartmentfrom.DataTextField = "DN";
            drpdepartmentfrom.DataValueField = "DeptID";
            drpdepartmentfrom.DataBind();
            drpdepartmentfrom.Items.Insert(0, new ListItem("--All Department--", "0"));

        }

        public void Department()
        {

            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(DeptID) + ' - ' + DeptName as DN,DeptID   " +
                                    " from  DeptITSuper " +
                                    " where TenentID =" + TID + "  " +
                                    " order by DeptID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpdepartmentto.DataSource = dt;
            drpdepartmentto.DataTextField = "DN";
            drpdepartmentto.DataValueField = "DeptID";
            drpdepartmentto.DataBind();
            drpdepartmentto.Items.Insert(0, new ListItem("--All Department--", "0"));
        }

        public void categoryfrom()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(CATID) + ' - ' + CATNAME as CN,CATID " +
                                    " from  ICCATEGORY " +
                                    " where TenentID =" + TID + " and CATTYPE = 'HelpDesk'  " +
                                    " order by CATID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpcategoryfrom.DataSource = dt;
            drpcategoryfrom.DataTextField = "CN";
            drpcategoryfrom.DataValueField = "CATID";
            drpcategoryfrom.DataBind();
            drpcategoryfrom.Items.Insert(0, new ListItem("--All Category--", "0"));
        }

        public void categoryto()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(CATID) + ' - ' + CATNAME as CN,CATID " +
                                    " from  ICCATEGORY " +
                                    " where TenentID =" + TID + " and CATTYPE = 'HelpDesk'  " +
                                    " order by CATID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpcategoryto.DataSource = dt;
            drpcategoryto.DataTextField = "CN";
            drpcategoryto.DataValueField = "CATID";
            drpcategoryto.DataBind();
            drpcategoryto.Items.Insert(0, new ListItem("--All Category--", "0"));
        }


        public void subcategoryfrom()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(SUBCATID) + ' - ' + SUBCATNAME as SC,SUBCATID  " +
                                    " from  ICSUBCATEGORY " +
                                    " where TenentID =" + TID + " and SUBCATTYPE = 'HelpDesk'  " +
                                    " order by SUBCATID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpscategoryfrom.DataSource = dt;
            drpscategoryfrom.DataTextField = "SC";
            drpscategoryfrom.DataValueField = "SUBCATID";
            drpscategoryfrom.DataBind();
            drpscategoryfrom.Items.Insert(0, new ListItem("--All Sub Category--", "0"));
        }

        public void subcategoryto()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(SUBCATID) + ' - ' + SUBCATNAME as SC,SUBCATID " +
                                    " from  ICSUBCATEGORY " +
                                    " where TenentID =" + TID + " and SUBCATTYPE = 'HelpDesk'  " +
                                    " order by SUBCATID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpscategoryto.DataSource = dt;
            drpscategoryto.DataTextField = "SC";
            drpscategoryto.DataValueField = "SUBCATID";
            drpscategoryto.DataBind();
            drpscategoryto.Items.Insert(0, new ListItem("--All Sub Category--", "0"));
        }


        public void complainfrom()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(REFID) + ' - ' + REFNAME1 as RM,REFID " +
                                    " from  REFTABLE " +
                                    " where TenentID =" + TID + " and REFTYPE = 'HelpDesk' and REFSUBTYPE = 'complain'  " +
                                    " order by REFID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpcomplaintypefrom.DataSource = dt;
            drpcomplaintypefrom.DataTextField = "RM";
            drpcomplaintypefrom.DataValueField = "REFID";
            drpcomplaintypefrom.DataBind();
            drpcomplaintypefrom.Items.Insert(0, new ListItem("--All Complains--", "0"));
        }

        public void complainto()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(REFID) + ' - ' + REFNAME1 as RM,REFID " +
                                    " from  REFTABLE " +
                                    " where TenentID =" + TID + " and REFTYPE = 'HelpDesk' and REFSUBTYPE = 'complain'  " +
                                    " order by REFID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpcomplaintypeto.DataSource = dt;
            drpcomplaintypeto.DataTextField = "RM";
            drpcomplaintypeto.DataValueField = "REFID";
            drpcomplaintypeto.DataBind();
            drpcomplaintypeto.Items.Insert(0, new ListItem("--All Complains--", "0"));
        }

        public void reportfrom()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(employeeID) + ' - ' + firstname as FN,employeeID " +
                                    " from  tbl_Employee " +
                                    " where TenentID =" + TID + " " +
                                    " order by employeeID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpreportfrom.DataSource = dt;
            drpreportfrom.DataTextField = "FN";
            drpreportfrom.DataValueField = "employeeID";
            drpreportfrom.DataBind();
            drpreportfrom.Items.Insert(0, new ListItem("--All Reported by--", "0"));
        }
        public void ReportTo()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(employeeID) + ' - ' + firstname as FN,employeeID " +
                                    " from  tbl_Employee " +
                                    " where TenentID =" + TID + " " +
                                    " order by employeeID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpreportto.DataSource = dt;
            drpreportto.DataTextField = "FN";
            drpreportto.DataValueField = "employeeID";
            drpreportto.DataBind();
            drpreportto.Items.Insert(0, new ListItem("--All Reported by--", "0"));
        }


        public void Locationfrom()
        {


            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(REFID) + ' - ' + REFNAME1 as RM,REFID " +
                                    " from  REFTABLE " +
                                    " where TenentID =" + TID + " and REFTYPE = 'Ticket' and REFSUBTYPE = 'PhysicalLocation' " +
                                    " order by REFID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drplocationfrom.DataSource = dt;
            drplocationfrom.DataTextField = "RM";
            drplocationfrom.DataValueField = "REFID";
            drplocationfrom.DataBind();
            drplocationfrom.Items.Insert(0, new ListItem("--All Location--", "0"));
        }
        public void Locationto()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(REFID) + ' - ' + REFNAME1 as RM,REFID " +
                                    " from  REFTABLE " +
                                    " where TenentID =" + TID + " and REFTYPE = 'Ticket' and REFSUBTYPE = 'PhysicalLocation' " +
                                    " order by REFID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drplocationto.DataSource = dt;
            drplocationto.DataTextField = "RM";
            drplocationto.DataValueField = "REFID";
            drplocationto.DataBind();
            drplocationto.Items.Insert(0, new ListItem("--All Location--", "0"));
        }
        public void binddata()
        {

           string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy", "dd-MMM-yyyy" };
var provider = System.Globalization.CultureInfo.InvariantCulture;

DateTime startdate = DateTime.ParseExact(
    txtdateFrom.Text.Trim(),
    formats,
    provider,
    System.Globalization.DateTimeStyles.None
);

DateTime Enddate = DateTime.ParseExact(
    txtdateTO.Text.Trim(),
    formats,
    provider,
    System.Globalization.DateTimeStyles.None
).AddDays(1);




            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");
           // DateTime end = Convert.ToDateTime(txtdateTO.Text);
            string paramStr = "";
            //validation
            string actionStr = "";


            if (drpscategoryfrom.SelectedValue != "0" && drpscategoryto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpscategoryto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpscategoryfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and CRMActivities.TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  ' " + drpscategoryto.SelectedValue + " '";
                else
                {
                    return;
                }

            }

            if (drpcomplaintypefrom.SelectedValue != "0" && drpcomplaintypeto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpcomplaintypeto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpcomplaintypefrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and CRMActivities.TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " '";
                else
                    return;
            }

            if (drpdepartmentfrom.SelectedValue != "0" && drpdepartmentto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpdepartmentto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpdepartmentfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and CRMActivities.TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  ' " + drpdepartmentto.SelectedValue + " '";
                else
                    return;
            }

            if (drplocationfrom.SelectedValue != "0" && drplocationto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drplocationto.SelectedValue);
                int Fromscate = Convert.ToInt32(drplocationfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and CRMActivities.TickPhysicalLocation BETWEEN '" + drplocationfrom.SelectedValue + "' AND  ' " + drplocationto.SelectedValue + " '";
                else
                    return;
            }

            if (drpreportfrom.SelectedValue != "0" && drpreportto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpreportto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpreportfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and CRMActivities.ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  ' " + drpreportto.SelectedValue + " '";
                else
                    return;
            }

            if (drpactionfrom.SelectedValue != "0" && drpactionTo.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpactionTo.SelectedValue);
                int Fromscate = Convert.ToInt32(drpactionfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    actionStr += "and CRMActivities.aspcomment BETWEEN '" + drpactionfrom.SelectedValue + "' AND  ' " + drpactionTo.SelectedValue + " '";
                else
                    return;
            }


            string SQOCommads = "";

            if (chklog.Checked == false)
            {
                SQOCommads = "SELECT CRMActivities.MasterCODE as ComplaintNumber,(CAST(CRMMainActivities.UploadDate AS VARCHAR(12)) +' - ' +CRMMainActivities.Remarks) as  Reference , CRMActivities.MyLineNo as MyLineNo ,  CRMActivities.Contact as Contact  ," +
                      " CRMActivities.USERNAME as CreatedBy,  CRMActivities.investigation as investigation, CRMActivities.Subject as Subject, CRMActivities.REMINDERNOTE as REMINDERNOTE ,  CRMMainActivities.UseReciepeName as UseReciepeName ,CRMActivities.UploadDate as UploadDate ,REFTABLE.REFNAME1 AS Action, CRMActivities.MyStatus as Status, " +
                        " CRMActivities.UrlRedirct, (DeptITSuper.DeptName + ' For HOD: ' + DeptITSuper.SuperVisorName ) as Header , " +
                        " (CRMActivities.Patient_Name) as Patient, CRMActivities.MRN as MRN , (select firstname  from tbl_Employee  where employeeID=CRMActivities.ReportedBy) as 'ReportedBy', DeptITSuper.DeptName as 'Dept',  CRMActivities.REMINDERNOTE as ActivityNote " +
                      " FROM CRMMainActivities INNER JOIN" +
                       "  CRMActivities ON CRMMainActivities.TenentID = CRMActivities.TenentID AND CRMMainActivities.COMPID = CRMActivities.COMPID AND " +
                        " CRMMainActivities.MasterCODE = CRMActivities.MasterCODE AND CRMMainActivities.LocationID = CRMActivities.LocationID AND CRMMainActivities.Prefix = CRMActivities.Prefix INNER JOIN" +
                        " REFTABLE ON CRMActivities.TenentID = REFTABLE.TenentID AND CRMActivities.aspcomment = REFTABLE.REFID LEFT OUTER JOIN" +
                        " DeptITSuper ON CRMActivities.TickDepartmentID = DeptITSuper.DeptID AND CRMActivities.TenentID = DeptITSuper.TenentID" +
                        " where CRMActivities.TenentID = " + TID + " and  CRMActivities.UploadDate BETWEEN ' " + stdate + " ' AND ' " + etdate + "' " + paramStr + "" +
                          " ORDER BY CRMActivities.TenentID, CRMActivities.ComplaintNumber, CRMActivities.MyLineNo, CRMActivities.TickDepartmentID, CRMActivities.UploadDate, CRMActivities.aspcomment";

            }

            else if (chklog.Checked == true)
            {
                SQOCommads = "SELECT CRMActivities.MasterCODE as ComplaintNumber,(CAST(CRMMainActivities.UploadDate AS VARCHAR(12)) +' - ' +CRMMainActivities.Remarks) as  Reference , CRMActivities.MyLineNo as MyLineNo ,  CRMActivities.Contact as Contact  ," +
                                     " CRMActivities.USERNAME as CreatedBy,  CRMActivities.investigation as investigation, CRMActivities.Subject as Subject, CRMActivities.REMINDERNOTE as REMINDERNOTE ,  CRMMainActivities.UseReciepeName as UseReciepeName ,CRMActivities.UploadDate as UploadDate ,REFTABLE.REFNAME1 AS Action, CRMActivities.MyStatus as Status, " +
                                       " CRMActivities.UrlRedirct, (DeptITSuper.DeptName + ' For HOD: ' + DeptITSuper.SuperVisorName ) as Header , " +
                                       " (CRMActivities.Patient_Name) as Patient, CRMActivities.MRN as MRN , (select firstname  from tbl_Employee  where employeeID=CRMActivities.ReportedBy) as 'ReportedBy', DeptITSuper.DeptName as 'Dept',  CRMActivities.REMINDERNOTE as ActivityNote " +
                                     " FROM CRMMainActivities INNER JOIN" +
                                      "  CRMActivities ON CRMMainActivities.TenentID = CRMActivities.TenentID AND CRMMainActivities.COMPID = CRMActivities.COMPID AND " +
                                       " CRMMainActivities.MasterCODE = CRMActivities.MasterCODE AND CRMMainActivities.LocationID = CRMActivities.LocationID AND CRMMainActivities.Prefix = CRMActivities.Prefix INNER JOIN" +
                                       " REFTABLE ON CRMActivities.TenentID = REFTABLE.TenentID AND CRMActivities.aspcomment = REFTABLE.REFID LEFT OUTER JOIN" +
                                       " DeptITSuper ON CRMActivities.TickDepartmentID = DeptITSuper.DeptID AND CRMActivities.TenentID = DeptITSuper.TenentID" +
                                       " where CRMActivities.TenentID = " + TID + " and UseReciepeID=1  and  CRMActivities.UploadDate BETWEEN ' " + stdate + " ' AND ' " + etdate + "' " + paramStr + "" +
                                         " ORDER BY CRMActivities.TenentID, CRMActivities.ComplaintNumber, CRMActivities.MyLineNo, CRMActivities.TickDepartmentID, CRMActivities.UploadDate, CRMActivities.aspcomment";

            }


            SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
            SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
            DataSet ds1 = new DataSet();
            ADB1.Fill(ds1);

            if (ds1.Tables.Count > 0)
            {
                DataTable dt1 = ds1.Tables[0];

                foreach (DataRow row in dt1.Rows)
                {
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (row.IsNull(col))
                        {
                            if (col.DataType == typeof(string))
                                row[col] = string.Empty;
                            else if (col.DataType == typeof(int) || col.DataType == typeof(long) ||
                                     col.DataType == typeof(short) || col.DataType == typeof(decimal) || col.DataType == typeof(double))
                                row[col] = 0;
                            else if (col.DataType == typeof(DateTime))
                                row[col] = DateTime.MinValue;
                            else
                                row[col] = DBNull.Value; // fallback
                        }
                    }
                }

                // Bind safe DataTable to ListView
                ListView1.DataSource = dt1;
                ListView1.DataBind();
            }
        }

        public string getlink(int attchid)
        {
            if (DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == attchid).Count() == 1)
            {
                return DB.tbl_DMSAttachmentMst.Single(p => p.TenentID == TID && p.AttachmentById == attchid).AttachmentPath;
            }
            else
            {
                return "";
            }
        }

        public string getinvestigation(int investigation)
        {
            if (investigation == 0 || investigation == null)
            {
                return "Irrelevant";
            }
            else
            {
                string inv = "";
                if (investigation == 1)
                {
                    inv = "Relevant";
                }
                else if (investigation == 2)
                {
                    inv = "Irrelevant";
                }
                return inv;

            }


        }

        public void sendEmail(string body, string email)
        {
            if (String.IsNullOrEmpty(email))
                return;
            //try
            //{

            string depart = "";
            if (drpdepartmentfrom.SelectedValue == "0")
            {
                depart = "All Department";
            }
            else
            {
                int id = Convert.ToInt32(drpdepartmentfrom.SelectedValue);
                string name = DB.DeptITSupers.Single(p => p.TenentID == 10 && p.DeptID == id).DeptName;
                depart = name;
            }
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.Subject = "Department " + depart + " Report for " + txtdateFrom.Text + " and " + txtdateTO.Text;
            msg.From = new System.Net.Mail.MailAddress("complaints@royalehayat.com");//("supportteam@digital53.com ");
            msg.To.Add(new System.Net.Mail.MailAddress(email));
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            smpt.UseDefaultCredentials = false;
            smpt.Host = "smtp.siteprotect.com";//for google required smtp.gmail.com and must be check Google Account setting https://myaccount.google.com/lesssecureapps?pli=1 ON//"mail.digital53.com";
            smpt.Port = 587;
            smpt.EnableSsl = true;
            //smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");
            smpt.Credentials = new System.Net.NetworkCredential("complaints@royalehayat.com", "Royal123$");
            smpt.Send(msg);
        }

        public string getcomplainname(int complain)
        {
            if (complain == 0 || complain == null)
            {
                return null;
            }
            else
            {
                return DB.REFTABLEs.SingleOrDefault(p => p.REFID == complain && p.TenentID == TID && p.REFTYPE == "HelpDesk" && p.REFSUBTYPE == "complain").REFNAME1;
            }

        }

        public string getdepartmentname(int Department)
        {
            if (Department == 0 || Department == null)
            {
                return null;
            }
            else
            {
                return DB.DeptITSupers.SingleOrDefault(p => p.DeptID == Department && p.TenentID == TID).DeptName;
            }

        }

        public string getcategoryname(int Category)
        {
            if (Category == 0 || Category == null)
            {
                return null;
            }
            else
            {
                return DB.ICCATEGORies.SingleOrDefault(p => p.CATID == Category && p.TenentID == TID && p.CATTYPE == "HelpDesk").CATNAME;
            }
        }

        public string getsubcategoryname(int SubCategory)
        {
            if (SubCategory == 0 || SubCategory == null)
            {
                return null;
            }
            else
            {
                return DB.ICSUBCATEGORies.SingleOrDefault(p => p.SUBCATID == SubCategory && p.TenentID == TID && p.SUBCATTYPE == "HelpDesk").SUBCATNAME;
            }

        }

        public string getReportname(int ReportedBy)
        {
            if (ReportedBy == 0 || ReportedBy == null)
            {
                return null;
            }
            else
            {
                return DB.USER_MST.SingleOrDefault(p => p.USER_ID == ReportedBy && p.TenentID == TID).FIRST_NAME;
            }
        }


        public string getactionname(int aspcomment)
        {
            if (aspcomment == 0 || aspcomment == null)
            {
                return null;
            }
            else
            {
                return DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFID == aspcomment).REFNAME1;
            }

        }

        protected void btnsubmit_Click1(object sender, EventArgs e)
        {
            binddata();
            //bool Flag = false;
            //int count = 0;
            //string Tablecontant = "<table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Complain No </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> MRN No </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Date </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Action </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Remark </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Status </th></tr></thead><tbody>";

            //for (int i = 0; i < ListView1.Items.Count; i++)
            //{

            //    Label lblID = (Label)ListView1.Items[i].FindControl("lblID");
            //    Label lblcomplain = (Label)ListView1.Items[i].FindControl("lblcomplain");
            //    Label lbltickdk = (Label)ListView1.Items[i].FindControl("lbltickdk");
            //    Label lbltickcat = (Label)ListView1.Items[i].FindControl("lbltickcat");
            //    int id = Convert.ToInt32(lblID.Text);

            //    count++;
            //    Flag = true;
            //    Database.CRMMainActivity obj = DB.CRMMainActivities.Single(p => p.MasterCODE == id);
            //    Tablecontant += "<tr><td>" + lblcomplain.Text + "</td><td>" + obj.UploadDate + "</td> <td>" + lbltickdk.Text + " </td> <td>" + lbltickcat.Text + " </td> <td>" + obj.Remarks + " </td> </tr>";

            //}


            //if (Flag)
            //{
            //    Tablecontant += " </tbody> </table>";
            //    string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">Hello <b></b>,<br /><br />Thank you for visit....<br /><br /><br />We are recived your report successfully ,As soon As Possible we will contact to you regarding your Inquiry... <br /><br />" + Tablecontant + "<br /><br /><br /><br />Thanks<br />Johar Mandavs</span>";
            //    string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\">Hello <b></b>,<br /><br />We are recieve New Inquiry of " + DateTime.Now + "<br />Report List :<br />" + Tablecontant + "<br /><br /><br /><br />Thanks<br />Jalpa Dangi</span>";

            //    sendEmail(Tocontant, txtpmail.Text);
            //    sendEmail(Ourcontant, txtpmail.Text);
            //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Successfully mail Send.');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
            //    return;
            //}

        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkcomplainno")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkuploaddate")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcomment")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremark")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmystatus")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }

        }

        protected void btnmail_Click(object sender, EventArgs e)
        {
            binddata();
            bool Flag = false;
            int count = 0;
            string Tablecontant = "<table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> No </th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> Reference</th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> Created By </th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> Action Status </th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> Patient </th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> MRN </th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> Investigation Result </th><th style='background-color:#940128;font-size:large;font-weight:bold;color: white;'> Attachment Link </th></tr></thead><tbody>";

            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                Label lblID = (Label)ListView1.Items[i].FindControl("lblID");
                Label lblcomplain = (Label)ListView1.Items[i].FindControl("lblcomplain");
                Label lblmrn = (Label)ListView1.Items[i].FindControl("lblmrn");
                Label lbltickdk = (Label)ListView1.Items[i].FindControl("lbltickdk");
                Label lbltickcat = (Label)ListView1.Items[i].FindControl("lbltickcat");
                //Label lblremark = (Label)ListView1.Items[i].FindControl("lblremark");
                Label lblstatus = (Label)ListView1.Items[i].FindControl("lblstatus");
                Label lblinvest = (Label)ListView1.Items[i].FindControl("lblinvest");
                Label lblre = (Label)ListView1.Items[i].FindControl("lblre");
                Label lbluplo = (Label)ListView1.Items[i].FindControl("lbluplo");
                Label lblsub = (Label)ListView1.Items[i].FindControl("lblsub");
                Label lblconta = (Label)ListView1.Items[i].FindControl("lblconta");

                Label lblmylineno = (Label)ListView1.Items[i].FindControl("lblmylineno");
                int ml = Convert.ToInt32(lblmylineno.Text);

                int id = Convert.ToInt32(lblID.Text);
                count++;
                Flag = true;
                string phone = lblconta.Text;
                int MAstercode = Convert.ToInt32(lblcomplain.Text);
                int deptid = Convert.ToInt32(DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).TickDepartmentID);
                int uid = Convert.ToInt32(DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).ReportedBy);
                string dept = DB.DeptITSupers.Single(p => p.TenentID == 10 && p.DeptID == deptid).DeptName;
                string reference = DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).Reference;
                string mystatus = DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).MyStatus;
                string patient = DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).Patient_Name;
                string remindernote = DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).REMINDERNOTE;
                string MRN = DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).MRN;
                // string mobile = DB.CRMActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).;
                string aremind = DB.CRMActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode && p.MyLineNo == ml).REMINDERNOTE;
                string Uname = DB.USER_MST.Single(p => p.TenentID == 10 && p.USER_ID == uid).FIRST_NAME;
                string sdat = DB.CRMMainActivities.Single(p => p.TenentID == 10 && p.MasterCODE == MAstercode).UploadDate.ToString();
                string iname = lblinvest.Text.ToString();
                string invest = "";
                if (iname == "1")
                {
                    invest = "Relevant";
                }
                else if (iname == "2")
                {
                    invest = "Irrelevant";
                }
                else
                {
                    invest = "Relevant";
                }
                string refere = sdat + "-" + remindernote;
                string subject = lblsub.Text;
                string attachmentdetail = "";
                if (DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == MAstercode).Count() > 0)
                {
                    attachmentdetail = DB.tbl_DMSAttachmentMst.Single(p => p.TenentID == TID && p.AttachmentById == MAstercode).AttachmentPath;
                }
                string activity = lblre.Text;
                DateTime dt = Convert.ToDateTime(lbluplo.Text);
                string dat = dt.ToString();
                string active = dat + " - " + activity;

                string statu = dat + " - " + aremind;


                string actives = dat + " - " + activity;
                if (deptid != null && subject != null && subject != "")
                {
                    Tablecontant += "<tr><td>" + MAstercode + "</td><td><a href=http://cfb.royalehayat.com/POS/ViewTicket.aspx?Mastercode=" + MAstercode + ">" + MAstercode + "</a><br/><br/><a href=http://cfb.royalehayat.com/POS/ViewTicket.aspx?Mastercode=" + MAstercode + ">" + refere + "</a> <br/><br/><a href=http://cfb.royalehayat.com/POS/ViewTicket.aspx?Mastercode=" + MAstercode + ">" + statu + "</a></td> <td>" + Uname + " </td> <td>" + mystatus + " </td><td>" + patient + " <br> Mob:- " + phone + "</td> <td>" + MRN + "</td><td>" + invest + "</td><td><a href=http://cfb.royalehayat.com/CRM/AttachmentMst.aspx?MasterID=" + MAstercode + "&DID=Ticket&RefNo=11058>" + attachmentdetail + "</a> </td> </tr>";
                }
                else
                {
                    Tablecontant += "<tr><td></td><td><a href=http://cfb.royalehayat.com/POS/ViewTicket.aspx?Mastercode=" + MAstercode + ">" + MAstercode + "</a><br/><br/><a href=http://cfb.royalehayat.com/POS/ViewTicket.aspx?Mastercode=" + MAstercode + ">" + statu + "</a></td> <td>" + Uname + " </td> <td>" + mystatus + " </td><td>" + patient + " <br> Mob:- " + phone + "</td> <td>" + MRN + "</td><td>" + invest + "</td><td><a href=http://cfb.royalehayat.com/CRM/AttachmentMst.aspx?MasterID=" + MAstercode + "&DID=Ticket&RefNo=11058>" + attachmentdetail + "</a> </td> </tr>";
                }
            }

            if (Flag)
            {
                Tablecontant += " </tbody> </table>";
                string depart = "";
                if (drpdepartmentfrom.SelectedValue == "0")
                {
                    depart = "All Department";
                }
                else
                {
                    int id = Convert.ToInt32(drpdepartmentfrom.SelectedValue);
                    string name = DB.DeptITSupers.Single(p => p.TenentID == 10 && p.DeptID == id).DeptName;
                    depart = name;
                }
                //<span style = \"font-family:Arial;font-size:10pt\"> Subject :" + sdt + "<b></b>,<br /><br />This email is reference to the Complain recorded on " + sdt + "," + Edt + " ,  <br /><br />" + Tablecontant + "<br /><br /><br /><br /><br /></span>

                string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">Daily complaints registered report of '" + depart + "' for the period between '" + txtdateFrom.Text + "' and '" + txtdateTO.Text + "' " + Tablecontant + "<br /><br /><br /><br /></span>";
                string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\">Daily complaints registered report of '" + depart + "' for the period between '" + txtdateFrom.Text + "' and '" + txtdateTO.Text + "' " + Tablecontant + "<br /><br /><br /><br /></span>";

                sendEmail(Tocontant, txtpmail.Text);
                //sendEmail(Ourcontant, "dangijalpa@gmail.com");
                ScriptManager.RegisterClientScriptBlock(
                  Page,
                  this.GetType(),
                  "myscript",
                  $"alert('Email successfully sent to {txtpmail.Text}');",
                  true
                );
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
                return;
            }
        }
    }
}
