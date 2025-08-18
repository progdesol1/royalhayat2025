using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web.ReportMst
{
    public partial class HelpDeskReport : System.Web.UI.Page
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
                DateTime EDT = DateTime.Now;
                txtdateTO.Text = EDT.ToString("MM/dd/yyyy");
                DateTime SDT = EDT.AddMonths(-1);
                txtdateFrom.Text = SDT.ToString("MM/dd/yyyy");
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
                complainyear();
                chkyear.Checked = true;
                chkdates.Checked = false;
                txtdateFrom.Enabled = false;
                txtdateTO.Enabled = false;

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

        public void complainyear()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select distinct Year(UploadDate) as showyear  from CRMMainActivities where TenentID =" + TID + " and UploadDate is not NULL order by Year(UploadDate) asc";
            DataTable dt = DataCon.GetDataTable(sql);
            drplistyear.DataSource = dt;
            drplistyear.DataTextField = "showyear";
            drplistyear.DataValueField = "showyear";
            drplistyear.DataBind();
            drplistyear.Items.Insert(0, new ListItem("--Select Year--", "0"));
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
            //DateTime startdate = Convert.ToDateTime(txtdateFrom.Text);
            //DateTime Enddate = Convert.ToDateTime(txtdateTO.Text);

            string stdate = txtdateFrom.Text;
            string etdate = txtdateTO.Text;
            //DateTime end = Convert.ToDateTime(txtdateTO.Text);
            string paramStr = "";
            //validation



            if (drpcategoryfrom.SelectedValue != "0" && drpcategoryto.SelectedValue !="0")
            {
                int Tocate = Convert.ToInt32(drpcategoryto.SelectedValue);
                int Fromcate = Convert.ToInt32(drpcategoryfrom.SelectedValue);
                if (Fromcate <= Tocate)
                    paramStr += "and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " '";
                else
                {
                   return;
                }
                    
            }

            if (drpscategoryfrom.SelectedValue != "0" && drpscategoryto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpscategoryto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpscategoryfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  ' " + drpscategoryto.SelectedValue + " '";
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
                    paramStr += "and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " '";
                else
                    return;
            }

            if (drpdepartmentfrom.SelectedValue != "0" && drpdepartmentto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpdepartmentto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpdepartmentfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  ' " + drpdepartmentto.SelectedValue + " '";
                else
                    return;
            }

            if (drplocationfrom.SelectedValue != "0" && drplocationto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drplocationto.SelectedValue);
                int Fromscate = Convert.ToInt32(drplocationfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and TickPhysicalLocation BETWEEN '" + drplocationfrom.SelectedValue + "' AND  ' " + drplocationto.SelectedValue + " '";
                else
                    return;
            }

            if (drpreportfrom.SelectedValue != "0" && drpreportto.SelectedValue != "0")
            {
                int Toscate = Convert.ToInt32(drpreportto.SelectedValue);
                int Fromscate = Convert.ToInt32(drpreportfrom.SelectedValue);
                if (Fromscate <= Toscate)
                    paramStr += "and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  ' " + drpreportto.SelectedValue + " '";
                else
                    return;
            }

            //if (drplistyear.SelectedValue != "0")
            //{

            //    paramStr += "and Year(UploadDate) = '" + drplistyear.SelectedValue + "'";

            //}


            string SQOCommad = "";

            if (chklog.Checked == false)
            {
                if (chkyear.Checked == true)
                {
                    SQOCommad = " select MyID as 'id' , MasterCODE as MasterCODE ,  TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , Remarks as 'Remarks', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate', ComplaintNumber as 'ComplaintNumber',  Year(UploadDate) as showyear , UseReciepeName as UseReciepeName from CRMMainActivities" +
                                    " where TenentID = " + TID + "and Year(UploadDate) = '" + drplistyear.SelectedValue + "' " + paramStr;
                    SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                    SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                    DataSet ds = new DataSet();
                    ADB.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    ListViewComplaint.DataSource = dt;
                    ListViewComplaint.DataBind();
                }

                else
                {
                    SQOCommad = " select MyID as 'id' , MasterCODE as MasterCODE , TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , Remarks as 'Remarks', ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber', UploadDate as 'UploadDate' , UseReciepeName as UseReciepeName from CRMMainActivities" +
                                     " where TenentID = " + TID + " and UploadDate BETWEEN '" + stdate + "' AND '" + etdate + "' " + paramStr;

                    //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
                    //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

                    SqlCommand CMD2 = new SqlCommand(SQOCommad, con);
                    SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
                    DataSet ds1 = new DataSet();
                    ADB1.Fill(ds1);
                    DataTable dt1 = ds1.Tables[0];
                    ListViewComplaint.DataSource = dt1;
                    ListViewComplaint.DataBind();
                }
            }

            else if (chklog.Checked == true)
            {
                if (chkyear.Checked == true)
                {
                    SQOCommad = " select MyID as 'id' , MasterCODE as MasterCODE ,  TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , Remarks as 'Remarks', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate', ComplaintNumber as 'ComplaintNumber',  Year(UploadDate) as showyear , UseReciepeName as UseReciepeName from CRMMainActivities" +
                                    " where TenentID = " + TID + "  and UseReciepeID=1 and  Year(UploadDate) = '" + drplistyear.SelectedValue + "' " + paramStr;
                    SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                    SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                    DataSet ds = new DataSet();
                    ADB.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    ListViewComplaint.DataSource = dt;
                    ListViewComplaint.DataBind();
                }

                else
                {
                    SQOCommad = " select MyID as 'id' , MasterCODE as MasterCODE , TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , Remarks as 'Remarks', ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber', UploadDate as 'UploadDate' , UseReciepeName as UseReciepeName from CRMMainActivities" +
                                     " where TenentID = " + TID + " and UseReciepeID=1 and UploadDate BETWEEN '" + stdate + "' AND '" + etdate + "' " + paramStr;

                    //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
                    //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

                    SqlCommand CMD2 = new SqlCommand(SQOCommad, con);
                    SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
                    DataSet ds1 = new DataSet();
                    ADB1.Fill(ds1);
                    DataTable dt1 = ds1.Tables[0];
                    ListViewComplaint.DataSource = dt1;
                    ListViewComplaint.DataBind();
                }
            }
                   
           
        }

        public void sendEmail(string body, string email)
        {

            if (String.IsNullOrEmpty(email))
                return;
            //try
            //{
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.Subject = "Thank You";
            msg.From = new System.Net.Mail.MailAddress("complaint@royalehayat.com");//("supportteam@digital53.com ");
            msg.To.Add(new System.Net.Mail.MailAddress(email));
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            smpt.UseDefaultCredentials = false;
            smpt.Host = "smtp.royalehayat.com";//for google required smtp.gmail.com and must be check Google Account setting https://myaccount.google.com/lesssecureapps?pli=1 ON//"mail.digital53.com";
            smpt.Port = 587;
            smpt.EnableSsl = false;
            //smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");
            smpt.Credentials = new System.Net.NetworkCredential("complaint@royalehayat.com", "nt3-rhh-m@1L");
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
                return DB.tbl_Employee.SingleOrDefault(p => p.employeeID == ReportedBy && p.TenentID == TID).firstname;
            }

        }


       
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            binddata();
            
        }

        protected void chkyear_CheckedChanged(object sender, EventArgs e)
        {
            if(chkyear.Checked == true)
            {
                txtdateFrom.Enabled = false;
                txtdateTO.Enabled = false;
                drplistyear.Enabled = true;
                chkdates.Checked = false;
            }
            else
            {
                drplistyear.Enabled = true;

            }
        }

        protected void chkdates_CheckedChanged(object sender, EventArgs e)
        {
            if(chkdates.Checked == true)
            {
                drplistyear.Enabled = false;
                txtdateFrom.Enabled = true;
                txtdateTO.Enabled = true;
                chkyear.Checked = false;
            }
            else
            {
                txtdateFrom.Enabled = true;
                txtdateTO.Enabled = true;
            }
        }

        protected void btnmail_Click(object sender, EventArgs e)
        {
            binddata();
            bool Flag = false;
            int count = 0;
            string Tablecontant = "<table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Complaint Type </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Date</th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Department </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Category </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Remark </th></tr></thead><tbody>";


            for (int i = 0; i < ListViewComplaint.Items.Count; i++)
            {

                Label lblID = (Label)ListViewComplaint.Items[i].FindControl("lblID");
                Label lblcomplain = (Label)ListViewComplaint.Items[i].FindControl("lblcomplain");
                Label lbltickdk = (Label)ListViewComplaint.Items[i].FindControl("lbltickdk");
                Label lbltickcat = (Label)ListViewComplaint.Items[i].FindControl("lbltickcat");
                int id = Convert.ToInt32(lblID.Text);

                count++;
                Flag = true;
                Database.CRMMainActivity obj = DB.CRMMainActivities.Single(p => p.MasterCODE == id);
                Tablecontant += "<tr><td>" + lblcomplain.Text + "</td><td>" + obj.UploadDate + "</td> <td>" + lbltickdk.Text + " </td> <td>" + lbltickcat.Text + " </td> <td>" + obj.Remarks + " </td> </tr>";

            }


            if (Flag)
            {
                Tablecontant += " </tbody> </table>";
                string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">Hello <b></b>,<br /><br />Thank you for visit....<br /><br /><br />We are recived your report successfully ,As soon As Possible we will contact to you regarding your Inquiry... <br /><br />" + Tablecontant + "<br /><br /><br /><br />Thanks<br />Johar Mandavs</span>";
                string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\">Hello <b></b>,<br /><br />We are recieve New Inquiry of " + DateTime.Now + "<br />Report List :<br />" + Tablecontant + "<br /><br /><br /><br />Thanks<br />Jalpa Dangi</span>";

                sendEmail(Tocontant, txtpmail.Text);
                sendEmail(Ourcontant, "dangijalpa@gmail.com");
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Successfully mail Send.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
                return;
            }

        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkdept")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcat")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremind")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkreport")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }


  
        protected void ListView1_ItemCommand4(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkdept")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcat")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremind")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkreport")
            {
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

        protected void ListViewComplaint_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Literal ltlcomno = (Literal)e.Item.FindControl("ltlcomno");
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                ltlcomno.Text = "_blank";
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
                
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkdept")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcat")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremind")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkreport")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }

            //else if (e.CommandName == "lnkcomplainno")
            //{
            //    Label lblcomno = (Label)e.Item.FindControl("lblcomno");
            //    Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
            //    Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
            //    Label lbldate = (Label)e.Item.FindControl("lbldate");
            //    Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
            //    Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
            //    Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
            //    Label lblreports = (Label)e.Item.FindControl("lblreports");
            //    int ID = Convert.ToInt32(e.CommandArgument);
            //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            //    Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
            //    ViewState["Edit"] = ID;
            //    Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            //}
            else if (e.CommandName == "lnksubcat")
            {
                Label lblcomno = (Label)e.Item.FindControl("lblcomno");
                Label lblsubcat = (Label)e.Item.FindControl("lblsubcat");
                Label lblcomplain = (Label)e.Item.FindControl("lblcomplain");
                Label lbldate = (Label)e.Item.FindControl("lbldate");
                Label lbltickdk = (Label)e.Item.FindControl("lbltickdk");
                Label lbltickcat = (Label)e.Item.FindControl("lbltickcat");
                Label lblremidernote = (Label)e.Item.FindControl("lblremidernote");
                Label lblreports = (Label)e.Item.FindControl("lblreports");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }

        }
    }
}