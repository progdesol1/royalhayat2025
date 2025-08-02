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

namespace Web
{
    public partial class silentreport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        int TID = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString != null)
                {
                    DateTime sdt = DateTime.Now.AddDays(-1);
                    DateTime Edt = DateTime.Now.AddDays(0);
                    string SQOCommads = " select distinct NoticeEmail  from  ViewDeptWiseComplainDailyReport where TenentID = 10 and UploadDate BETWEEN ' " + sdt + " ' AND ' " + Edt  + "' and TickDepartmentID BETWEEN '1' AND '99999'" +
                               "and TickCatID BETWEEN '1' AND '99999' and  TickSubCatID BETWEEN '1' AND '99999'"+
                                "and TickComplainType BETWEEN '210001' AND '210006'";

                    SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
                    SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
                    DataSet ds1 = new DataSet();
                    ADB1.Fill(ds1);
                    DataTable dt1 = ds1.Tables[0];
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string email = dt1.Rows[i].ItemArray[0].ToString();
                        secificEmailSend(email);
                    }
                }
        }
        public void secificEmailSend(string Email)
        {
            if (Request.QueryString != null && Request.QueryString["p"] == "s");
                    {
                        DateTime sdt = DateTime.Now.AddDays(-1);
                        DateTime Edt = DateTime.Now.AddDays(0);


                        string SQOCommads = " select ComplaintNumber as 'ComplaintNumber' , DeptName as 'DeptName' , MasterCODE as 'MasterCODE' , TickComplainType as 'complain' , TickDepartmentID as 'Department' , " +
                                " Patient_Name as Name , MRN as 'MRN' , aspcomment as 'aspcomment' , MyStatus as MyStatus , NoticeEmail as 'NoticeEmail' , TickCatID as 'Category' , " +
                                " TickSubCatID as 'SubCategory' , ActivityPerform as 'Remarks', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate' " +
                                " from ViewDeptWiseComplainDailyReport where TenentID = " + TID + "and UploadDate BETWEEN ' " + sdt + " ' AND ' " + Edt + "' and TickDepartmentID BETWEEN '1' AND '99999'" +
                                "and TickCatID BETWEEN '1' AND '99999' and  TickSubCatID BETWEEN '1' AND '99999'" +
                                "and TickComplainType BETWEEN '210001' AND '210006' and NoticeEmail='" + Email + "'  order by TickDepartmentID , NoticeEmail";

                        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
                        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
                        DataSet ds1 = new DataSet();
                        ADB1.Fill(ds1);
                        DataTable dt1 = ds1.Tables[0];
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string email = dt1.Rows[i].ItemArray[0].ToString();
                            secificEmailSend(email);

                        }
                       

                        bool Flag = false;
                        int dID = 0;

                        string Tablecontant = "<table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Complain No </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> MRN </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Date </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Dept Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Action </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Remark </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Status </th></tr></thead><tbody>";
                        //string email = "";
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {

                            string complainno = dt1.Rows[i]["ComplaintNumber"].ToString();
                            int Master = Convert.ToInt32(dt1.Rows[i]["MasterCODE"]);
                            int department = Convert.ToInt32(dt1.Rows[i]["Department"]);
                            DateTime dt = Convert.ToDateTime(dt1.Rows[i]["UploadDate"]);
                            int action = Convert.ToInt32(dt1.Rows[i]["aspcomment"]);
                            string dname = dt1.Rows[i]["DeptName"].ToString();
                            string remark = dt1.Rows[i]["Remarks"].ToString();
                            string MRN = dt1.Rows[i]["MRN"].ToString();
                            string MyStatus = dt1.Rows[i]["MyStatus"].ToString(); 


                            Flag = true;
                            Database.CRMMainActivity obj = DB.CRMMainActivities.Single(p => p.MasterCODE == Master);
                            Tablecontant += "<tr><td>" + complainno + "</td><td>" + MRN + "</td> <td>" + dt + " </td> <td>" + dname + " </td><td>" + action + " </td> <td>" + remark + " </td><td>" + MyStatus + " </td> </tr>";

                        }                       
                        if (Flag)
                        {
                           

                              
                               Tablecontant += " </tbody> </table>";
                               string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\"> Subject :" + sdt + "<b></b>,<br /><br />This email is reference to the Complain recorded on " + sdt + "," + Edt + " ,  <br /><br />" + Tablecontant + "<br /><br /><br /><br /><br /></span>";
                               //string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">Dear Sir, <b></b>,<br /><br />DepartmentManager,<br />This email is reference to the Complain recorded on " + sdt + "," + Edt + ",  <br />" + Tablecontant + "<br /><br /><br /><br />Thanks<br /></span>";

                               //sendEmail(Tocontant, Email);
                               sendEmail(Ourcontant, Email);
                               ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Successfully mail Send.');", true);
                               Response.Redirect("reachsilent.aspx");                    
                            
                               
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
                            return;
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
            msg.From = new System.Net.Mail.MailAddress("digital53marketing@gmail.com");//("supportteam@digital53.com ");
            msg.To.Add(new System.Net.Mail.MailAddress(email));
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            smpt.UseDefaultCredentials = false;
            smpt.Host = "smtp.gmail.com";//for google required smtp.gmail.com and must be check Google Account setting https://myaccount.google.com/lesssecureapps?pli=1 ON//"mail.digital53.com";
            smpt.Port = 587;
            smpt.EnableSsl = true;
            //smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");
            smpt.Credentials = new System.Net.NetworkCredential("digital53marketing@gmail.com", "Digital123$");
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

    }
}