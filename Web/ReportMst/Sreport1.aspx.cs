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
    public partial class Sreport1 : System.Web.UI.Page
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
                    //txtdateTO.Text = EDT.ToString("MM/dd/yyyy");
                    //DateTime SDT = DateTime.Now.AddDays(-1);
                    //txtdateFrom.Text = SDT.ToString("MM/dd/yyyy");
                    //bindterminal();
                    //Department();
                    //subcategoryto();
                    //subcategoryfrom();
                    //categoryfrom();
                    //categoryto();
                    //complainfrom();
                    //complainto();
                    //ReportTo();
                    //reportfrom();
                    //Locationfrom();
                    //Locationto();    
                                
                                    
                }
            } 
       
        public void binddata()
        {
            //DateTime startdate = Convert.ToDateTime(txtdateFrom.Text);
            //DateTime Enddate = Convert.ToDateTime(txtdateTO.Text).AddDays(1).AddSeconds(-1);




            //string stdate = startdate.ToString("yyyy-MM-dd hh:mm:ss ttt");
            //string etdate = Enddate.ToString("yyyy-MM-dd hh:mm:ss ttt");
            //DateTime end = Convert.ToDateTime(txtdateTO.Text);
            //string paramStr = "";
            ////validation
            //string actionStr = "";


            //if (drpcategoryfrom.SelectedValue != "0" && drpcategoryto.SelectedValue !="0")
            //{
            //    int Tocate = Convert.ToInt32(drpcategoryto.SelectedValue);
            //    int Fromcate = Convert.ToInt32(drpcategoryfrom.SelectedValue);
            //    if (Fromcate <= Tocate)
            //        paramStr += "and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " '";
            //    else
            //    {
            //       return;
            //    }
                    
            //}

            //if (drpscategoryfrom.SelectedValue != "0" && drpscategoryto.SelectedValue != "0")
            //{
            //    int Toscate = Convert.ToInt32(drpscategoryto.SelectedValue);
            //    int Fromscate = Convert.ToInt32(drpscategoryfrom.SelectedValue);
            //    if (Fromscate <= Toscate)
            //        paramStr += "and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  ' " + drpscategoryto.SelectedValue + " '";
            //    else
            //    {
            //        return;
            //    }

            //}



            //if (drpcomplaintypefrom.SelectedValue != "0" && drpcomplaintypeto.SelectedValue != "0")
            //{
            //    int Toscate = Convert.ToInt32(drpcomplaintypeto.SelectedValue);
            //    int Fromscate = Convert.ToInt32(drpcomplaintypefrom.SelectedValue);
            //    if (Fromscate <= Toscate)
            //        paramStr += "and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " '";
            //    else
            //        return;
            //}

            //if (drpdepartmentfrom.SelectedValue != "0" && drpdepartmentto.SelectedValue != "0")
            //{
            //    int Toscate = Convert.ToInt32(drpdepartmentto.SelectedValue);
            //    int Fromscate = Convert.ToInt32(drpdepartmentfrom.SelectedValue);
            //    if (Fromscate <= Toscate)
            //        paramStr += "and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  ' " + drpdepartmentto.SelectedValue + " '";
            //    else
            //        return;
            //}

            //if (drplocationfrom.SelectedValue != "0" && drplocationto.SelectedValue != "0")
            //{
            //    int Toscate = Convert.ToInt32(drplocationto.SelectedValue);
            //    int Fromscate = Convert.ToInt32(drplocationfrom.SelectedValue);
            //    if (Fromscate <= Toscate)
            //        paramStr += "and TickPhysicalLocation BETWEEN '" + drplocationfrom.SelectedValue + "' AND  ' " + drplocationto.SelectedValue + " '";
            //    else
            //        return;
            //}

            //if (drpreportfrom.SelectedValue != "0" && drpreportto.SelectedValue != "0")
            //{
            //    int Toscate = Convert.ToInt32(drpreportto.SelectedValue);
            //    int Fromscate = Convert.ToInt32(drpreportfrom.SelectedValue);
            //    if (Fromscate <= Toscate)
            //        paramStr += "and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  ' " + drpreportto.SelectedValue + " '";
            //    else
            //        return;
            //}

            //if(drpactionfrom.SelectedValue != "0" && drpactionTo.SelectedValue != "0")
            //{
            //    int Toscate = Convert.ToInt32(drpactionTo.SelectedValue);
            //    int Fromscate = Convert.ToInt32(drpactionfrom.SelectedValue);
            //    if (Fromscate <= Toscate)
            //        actionStr += "and aspcomment BETWEEN '" + drpactionfrom.SelectedValue + "' AND  ' " + drpactionTo.SelectedValue + " '";
            //    else
            //        return;
            //}

            //string SQOCommads = "SELECT CRMActivities.MasterCODE as ComplaintNumber,(CAST(CRMMainActivities.UploadDate AS VARCHAR(12)) +' - ' +CRMMainActivities.Remarks) as  Reference , " +
            //            " CRMActivities.USERNAME as CreatedBy, REFTABLE.REFNAME1 AS Action, CRMActivities.MyStatus as Status, " +
            //              " CRMActivities.UrlRedirct, (DeptITSuper.DeptName + ' For HOD: ' + DeptITSuper.SuperVisorName ) as Header , " +
            //              " (CRMActivities.Patient_Name+' Mob:'+CRMActivities.Contact) as Patient, CRMActivities.MRN as MRN , (select firstname  from tbl_Employee  where employeeID=CRMActivities.ReportedBy) as 'ReportedBy', DeptITSuper.DeptName as 'Dept',  CRMActivities.REMINDERNOTE as ActivityNote" +
            //            " FROM CRMMainActivities INNER JOIN" +
            //             "  CRMActivities ON CRMMainActivities.TenentID = CRMActivities.TenentID AND CRMMainActivities.COMPID = CRMActivities.COMPID AND " +
            //              " CRMMainActivities.MasterCODE = CRMActivities.MasterCODE AND CRMMainActivities.LocationID = CRMActivities.LocationID AND CRMMainActivities.Prefix = CRMActivities.Prefix INNER JOIN" +
            //              " REFTABLE ON CRMActivities.TenentID = REFTABLE.TenentID AND CRMActivities.aspcomment = REFTABLE.REFID LEFT OUTER JOIN" +
            //              " DeptITSuper ON CRMActivities.TickDepartmentID = DeptITSuper.DeptID AND CRMActivities.TenentID = DeptITSuper.TenentID" +
            //              " where CRMActivities.TenentID = " + TID + " and  CRMActivities.UploadDate BETWEEN " 
            //                " ORDER BY CRMActivities.TenentID, CRMActivities.TickDepartmentID, CRMActivities.UploadDate, CRMActivities.aspcomment";
           




            //string SQOCommads = " select ComplaintNumber as 'ComplaintNumber' , MasterCODE as 'MasterCODE' , TickComplainType as 'complain' , TickDepartmentID as 'Department' , " +
            //                     " Patient_Name as Name , MRN as MRN , aspcomment as aspcomment , MyStatus as MyStatus , TickCatID as 'Category' , " +
            //                     " TickSubCatID as 'SubCategory' , REMINDERNOTE as 'Remarks', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate' " +
            //                     " from ViewDeptWiseComplainDailyReport where TenentID = " + TID + "and UploadDate BETWEEN '" + stdate + "' AND '" + etdate + "' " + paramStr +
            //                     " and ComplaintNumber like   '%" + txtID.Text + "%' and MRN like  '%" + txtMRN.Text + "%' " + actionStr + "  and REMINDERNOTE like '%" + txtcomplain.Text + "%' ";

                //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
                //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

                //SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
                //SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
                //DataSet ds1 = new DataSet();
                //ADB1.Fill(ds1);
                //DataTable dt1 = ds1.Tables[0];
                //ListView1.DataSource = dt1;
                //ListView1.DataBind();
                    
           
        }

        public void sendEmail(string body, string email)
        {

            if (String.IsNullOrEmpty(email))
                return;
            //try
            //{
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.Subject = DateTime.Now + " Report";
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

        public string getlink(int attchid)
        {
            if (DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == attchid).Count() == 1)
            {
                return DB.tbl_DMSAttachmentMst.Single(p => p.TenentID == TID && p.AttachmentById == attchid).AttachmentPath;
            }
            else
            {
                return "Not Found";
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
            string Tablecontant = "<table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Complain No </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> MRN No</th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Date </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Action </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Status </th></tr></thead><tbody>";


            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                Label lblID = (Label)ListView1.Items[i].FindControl("lblID");
                Label lblcomplain = (Label)ListView1.Items[i].FindControl("lblcomplain");
                Label lblmrn = (Label)ListView1.Items[i].FindControl("lblmrn");
                Label lbltickdk = (Label)ListView1.Items[i].FindControl("lbltickdk");
                Label lbltickcat = (Label)ListView1.Items[i].FindControl("lbltickcat");
                //Label lblremark = (Label)ListView1.Items[i].FindControl("lblremark");
                Label lblstatus = (Label)ListView1.Items[i].FindControl("lblstatus");
                int id = Convert.ToInt32(lblID.Text);

                count++;
                Flag = true;
               
                Tablecontant += "<tr><td>" + lblcomplain.Text + "</td><td>" + lblmrn.Text + "</td> <td>" + lbltickdk.Text + " </td> <td>" + lbltickcat.Text + " </td>  <td>" + lblstatus.Text + " </td> </tr>";

            }


            if (Flag)
            {
                Tablecontant += " </tbody> </table>";

                
                 //<span style = \"font-family:Arial;font-size:10pt\"> Subject :" + sdt + "<b></b>,<br /><br />This email is reference to the Complain recorded on " + sdt + "," + Edt + " ,  <br /><br />" + Tablecontant + "<br /><br /><br /><br /><br /></span>
                string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">This email is reference to the Complain recorded on " + DateTime.Now   + " ,   <b></b>,<br /><br />" + Tablecontant + "<br /><br /><br /></span>";
                string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\">This email is reference to the Complain recorded on" + DateTime.Now + "<br />Report List :<br />" + Tablecontant + "<br /><br /><br /><br /></span>";

                sendEmail(Tocontant, txtpmail.Text);
                //sendEmail(Ourcontant, "dangijalpa@gmail.com");
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Successfully mail Send.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
                return;
            }
        }
    }
}