using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;


namespace Web
{
    public partial class Summerised : System.Web.UI.Page
    {
        int TID = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString != null)
                {
                    string email = "dangijalpa@gmail.com";
                    secificEmailSend(email);
                }
            }
           
        }


        public void secificEmailSend(string Email)
        {
            if (Request.QueryString != null && Request.QueryString["p"] == "s") ;
            {
                TID = 10;
                con.Open();
                SqlCommand command;
                SqlDataAdapter Ang = new SqlDataAdapter();
                string sql5 = "Delete from Tempdatatable where tenentID=" + TID;
                command = new SqlCommand(sql5, con);
                command.ExecuteNonQuery();
                command.Dispose();
                DateTime EDT = DateTime.Now;
                string stdate = EDT.ToString("MM,yyyy");

                string SQOCommad = " select TickDepartmentID , DeptITSuper.DeptName  , COUNT(*) as DeptCount  FROM  CRMMainActivities , DeptITSuper " +

                                   "  where CRMMainActivities.TenentID = " + TID + "  and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "'  and DeptITSuper.TenentID = CRMMainActivities.TenentID   and DeptITSuper.DeptID = CRMMainActivities.TickDepartmentID and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null" +

                                   " group by TickDepartmentID, DeptITSuper.DeptName";

                List<Tempdatatable> Listtemp = DB.Tempdatatables.Where(p => p.TenentID == TID).ToList();

                List<Tempdatatable> list1 = new List<Tempdatatable>();
                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];

                decimal finalcount = 0;

                for (int a = 0; a <= dt.Rows.Count - 1; a++)
                {
                    finalcount = finalcount + Convert.ToDecimal(dt.Rows[a]["DeptCount"]);
                }


                if (dt.Rows.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            int TickDepartmentID = Convert.ToInt32(dt.Rows[i]["TickDepartmentID"]);
                            decimal DeptCount = Convert.ToDecimal(dt.Rows[i]["DeptCount"]);
                            string deptName = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == TickDepartmentID).DeptName;
                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 1;
                            objprofile.ID = TickDepartmentID;
                            objprofile.Name = deptName;
                            objprofile.UserId = 11525;
                            objprofile.Count = DeptCount;
                            objprofile.TotalCount = Convert.ToInt32(finalcount);
                            decimal percent = (DeptCount / finalcount) * 100;
                            objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();
                        }
                    }
                }


                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='Summerised.aspx';", true);
                }


                //decimal finalTotal = 0;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    finalTotal = finalTotal + Convert.ToDecimal(dt.Rows[i]["DeptCount"]);
                //}
                //lblFinalTotal.Text = finalTotal.ToString();



                string com = " select TickDepartmentID , Tempdatatable.Name  , COUNT(*) as DeptCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                            " and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "' " +
                             " and Tempdatatable.Type=1 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickDepartmentID " +
                             "  and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null group by TickDepartmentID, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs = new SqlCommand(com, con);
                SqlDataAdapter ADBs = new SqlDataAdapter(CMDs);
                DataSet dss = new DataSet();
                ADBs.Fill(dss);
                DataTable dts = dss.Tables[0];
                    
               

                bool Flag = false;
                int dID = 0;


                string Tablecontant = "<h2>Department</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Dept id </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Deptartment name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant1 = "<h2>Location</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> LOC Id  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Location Name  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant2 = "<h2>Category</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Cat ID </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Category Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'>  Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant3 = "<h2>Sub Category</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> SubCat ID  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> SubCategory Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                

                //string email = "";
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    string complainno = dts.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts.Rows[i]["TickDepartmentID"]);
                    int department = Convert.ToInt32(dts.Rows[i]["DeptCount"]);
                    int action = Convert.ToInt32(dts.Rows[i]["percentage"]);              


                    Flag = true;                    
                    Tablecontant += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }


               


                string SQOCommad6 = "select TickPhysicalLocation , REFTABLE.REFNAME1  , COUNT(*) as LocCount  FROM  CRMMainActivities , REFTABLE " +
                                    " where CRMMainActivities.TenentID = " + TID + " and REFTABLE.REFTYPE='Ticket' and REFTABLE.REFSUBTYPE='PhysicalLocation' " +
                                    " and FORMAT(CRMMainActivities.UploadDate, 'MM,yyyy') =  '" + stdate + "' and REFTABLE.TenentID = CRMMainActivities.TenentID  " +
                                    " and REFTABLE.REFID = CRMMainActivities.TickPhysicalLocation and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, REFTABLE.REFNAME1 ";

                SqlCommand CMD7 = new SqlCommand(SQOCommad6, con);
                SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
                DataSet ds7 = new DataSet();
                ADB7.Fill(ds7);
                DataTable dt7 = ds7.Tables[0];

                decimal fl = 0;

                for (int a = 0; a <= dt7.Rows.Count - 1; a++)
                {
                    fl = fl + Convert.ToDecimal(dt7.Rows[a]["LocCount"]);
                }

               

               

                if (dt7.Rows.Count > 0)
                {
                    if (ds7.Tables[0] != null && ds7.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt7.Rows.Count - 1; i++)
                        {

                            int TickPhysicalLocation = Convert.ToInt32(dt7.Rows[i]["TickPhysicalLocation"]);
                            decimal LocCount = Convert.ToDecimal(dt7.Rows[i]["LocCount"]);
                            string REFNAME1 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == TickPhysicalLocation && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;


                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 4;
                            objprofile.ID = TickPhysicalLocation;
                            objprofile.Name = REFNAME1;
                            objprofile.UserId = 11525;
                            objprofile.Count = LocCount;
                            objprofile.TotalCount = Convert.ToInt32(fl);
                            decimal percent = (LocCount / fl) * 100;
                            objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }

                //decimal finallocation = 0;
                //for (int i = 0; i < dt7.Rows.Count; i++)
                //{
                //    finallocation = finallocation + Convert.ToDecimal(dt7.Rows[i]["LocCount"]);
                //}
                //lbllocation.Text = finallocation.ToString();

                string fl1 = " select TickPhysicalLocation , Tempdatatable.Name  , COUNT(*) as LocCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                             " and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "' " +
                             " and Tempdatatable.Type=4 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickPhysicalLocation " +
                             "  and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs3 = new SqlCommand(fl1, con);
                SqlDataAdapter ADBs3 = new SqlDataAdapter(CMDs3);
                DataSet dss3 = new DataSet();
                ADBs3.Fill(dss3);
                DataTable dts3 = dss3.Tables[0];


                for (int i = 0; i < dts3.Rows.Count; i++)
                {

                    string complainno = dts3.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts3.Rows[i]["TickPhysicalLocation"]);
                    int department = Convert.ToInt32(dts3.Rows[i]["LocCount"]);
                    int action = Convert.ToInt32(dts3.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant1 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }

                string SQOCommad4 = " select TickCatID , ICCATEGORY.CATNAME  , COUNT(*) as CatCount  FROM  CRMMainActivities , ICCATEGORY " +
                                       " where CRMMainActivities.TenentID =  " + TID + " and ICCATEGORY.CATTYPE='HelpDesk' " +
                                       " and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "'  and ICCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                       " and ICCATEGORY.CATID = CRMMainActivities.TickCatID and CRMMainActivities.TickCatID is not null  and CRMMainActivities.UploadDate is not null group by TickCatID, ICCATEGORY.CATNAME ";

                SqlCommand CMD5 = new SqlCommand(SQOCommad4, con);
                SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
                DataSet ds5 = new DataSet();
                ADB5.Fill(ds5);
                DataTable dt5 = ds5.Tables[0];

                decimal fc = 0;

                for (int a = 0; a <= dt5.Rows.Count - 1; a++)
                {
                    fc = fc + Convert.ToDecimal(dt5.Rows[a]["CatCount"]);
                }


                if (dt5.Rows.Count > 0)
                {
                    if (ds5.Tables[0] != null && ds5.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                        {

                            int TickCatID = Convert.ToInt32(dt5.Rows[i]["TickCatID"]);
                            decimal CatCount = Convert.ToDecimal(dt5.Rows[i]["CatCount"]);
                            string CATNAME = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == TickCatID).CATNAME;


                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 2;
                            objprofile.ID = TickCatID;
                            objprofile.Name = CATNAME;
                            objprofile.UserId = 11525;
                            objprofile.Count = CatCount;
                            objprofile.TotalCount = Convert.ToInt32(fc);
                            decimal percent = (CatCount / fc) * 100;
                            objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }
                //decimal finalcategory = 0;
                //for (int i = 0; i < dt5.Rows.Count; i++)
                //{
                //    finalcategory = finalcategory + Convert.ToDecimal(dt5.Rows[i]["CatCount"]);
                //}
                //lblcategory.Text = finalcategory.ToString();

                //decimal finalcat = 0;
                //for (int i = 0; i < dt5.Rows.Count; i++)
                //{
                //    finalcat = finalcat + Convert.ToDecimal(dt5.Rows[i]["Tempdatatable.percentage"]);
                //}
                //lblpercategory.Text = finalcat.ToString();

                string fc1 = " select TickCatID , Tempdatatable.Name  , COUNT(*) as CatCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                             " and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "' " +
                             " and Tempdatatable.Type=2 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickCatID " +
                             "  and CRMMainActivities.TickCatID is not null and CRMMainActivities.UploadDate is not null group by TickCatID, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs1 = new SqlCommand(fc1, con);
                SqlDataAdapter ADBs1 = new SqlDataAdapter(CMDs1);
                DataSet dss1 = new DataSet();
                ADBs1.Fill(dss1);
                DataTable dts1 = dss1.Tables[0];
                for (int i = 0; i < dts1.Rows.Count; i++)
                {
                    string complainno = dts1.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts1.Rows[i]["TickCatID"]);
                    int department = Convert.ToInt32(dts1.Rows[i]["CatCount"]);
                    int action = Convert.ToInt32(dts1.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant2 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";
                }


                string SQOCommad5 = " select TickSubCatID , ICSUBCATEGORY.SUBCATNAME  , COUNT(*) as subCatCount  FROM  CRMMainActivities , ICSUBCATEGORY  " +
                                        " where CRMMainActivities.TenentID = " + TID + " and ICSUBCATEGORY.SUBCATTYPE='HelpDesk' " +
                                        " and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "' and ICSUBCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                        " and ICSUBCATEGORY.SUBCATID = CRMMainActivities.TickSubCatID and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, ICSUBCATEGORY.SUBCATNAME ";

                SqlCommand CMD6 = new SqlCommand(SQOCommad5, con);
                SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
                DataSet ds6 = new DataSet();
                ADB6.Fill(ds6);
                DataTable dt6 = ds6.Tables[0];

                decimal fs = 0;

                for (int a = 0; a <= dt6.Rows.Count - 1; a++)
                {
                    fs = fs + Convert.ToDecimal(dt6.Rows[a]["subCatCount"]);
                }


                if (dt6.Rows.Count > 0)
                {
                    if (ds6.Tables[0] != null && ds6.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt6.Rows.Count - 1; i++)
                        {

                            int TickSubCatID = Convert.ToInt32(dt6.Rows[i]["TickSubCatID"]);
                            decimal subCatCount = Convert.ToDecimal(dt6.Rows[i]["subCatCount"]);
                            string SUBCATNAME = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == TickSubCatID).SUBCATNAME;
                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 3;
                            objprofile.ID = TickSubCatID;
                            objprofile.Name = SUBCATNAME;
                            objprofile.UserId = 11525;
                            objprofile.Count = subCatCount;
                            objprofile.TotalCount = Convert.ToInt32(fs);
                            decimal percent = (subCatCount / fs) * 100;
                            objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }
                //decimal finalsubcategory = 0;
                //for (int i = 0; i < dt6.Rows.Count; i++)
                //{
                //    finalsubcategory = finalsubcategory + Convert.ToDecimal(dt6.Rows[i]["subCatCount"]);
                //}
                //lblsubcategory.Text = finalsubcategory.ToString();


                //decimal finalpersubcategory = 0;
                //for (int i = 0; i < dt6.Rows.Count; i++)
                //{
                //    finalpersubcategory = finalpersubcategory + Convert.ToDecimal(dt6.Rows[i]["Tempdatatable.percentage"]);
                //}
                //lblpersubcategory.Text = finalpersubcategory.ToString();

                string fs1 = " select TickSubCatID , Tempdatatable.Name  , COUNT(*) as subCatCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                             " and FORMAT(UploadDate, 'MM,yyyy') =  '" + stdate + "' " +
                             " and Tempdatatable.Type=3 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickSubCatID " +
                             "  and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs2 = new SqlCommand(fs1, con);
                SqlDataAdapter ADBs2 = new SqlDataAdapter(CMDs2);
                DataSet dss2 = new DataSet();
                ADBs2.Fill(dss2);
                DataTable dts2 = dss2.Tables[0];


                for (int i = 0; i < dts2.Rows.Count; i++)
                {

                    string complainno = dts2.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts2.Rows[i]["TickSubCatID"]);
                    int department = Convert.ToInt32(dts2.Rows[i]["subCatCount"]);
                    int action = Convert.ToInt32(dts2.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant3 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }
                


                if (Flag)
                {
                    Tablecontant += " </tbody> </table>";
                    Tablecontant1 += " </tbody> </table>";
                    Tablecontant2 += " </tbody> </table>";
                    Tablecontant3 += " </tbody> </table>";
                    string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\"> Subject :" + stdate + "<b></b>,<br /><br />This email is reference to the Complain recorded on " + stdate + "," + stdate + " ,  <br /><br />" + Tablecontant + "<br /><br />" + Tablecontant1 + "<br /><br />" + Tablecontant2 + "<br /><br />" + Tablecontant3 + "<br /><br /><br /></span>";
                    //string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">Dear Sir, <b></b>,<br /><br />DepartmentManager,<br />This email is reference to the Complain recorded on " + sdt + "," + Edt + ",  <br />" + Tablecontant + "<br /><br /><br /><br />Thanks<br /></span>";

                    //sendEmail(Tocontant, Email);
                    sendEmail(Ourcontant, "dangijalpa@gmail.com");
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
    }
}