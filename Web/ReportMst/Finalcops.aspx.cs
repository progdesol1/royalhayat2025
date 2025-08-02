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

namespace Web.ReportMst
{
    public partial class Finalcops : System.Web.UI.Page
    {
        int TID = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
               // BindList();
                complainmonthfrom();

                con.Open();
                SqlCommand command;
                SqlDataAdapter ADB = new SqlDataAdapter();
               int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
               string sql5 = "Delete from Tempdatatable where tenentID=10 and UserID=" + UIN;
               command = new SqlCommand(sql5,con); 
               command.ExecuteNonQuery();
               command.Dispose();

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
            string sql = " select STR(USER_ID) + ' - ' + FIRST_NAME as FN,USER_ID " +
                                    " from  USER_MST " +
                                    " where TenentID =" + TID + " " +
                                    " order by USER_ID  asc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpreportfrom.DataSource = dt;
            drpreportfrom.DataTextField = "FN";
            drpreportfrom.DataValueField = "USER_ID";
            drpreportfrom.DataBind();
            drpreportfrom.Items.Insert(0, new ListItem("--All Reported by--", "0"));
        }
        public void ReportTo()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = " select STR(USER_ID) + ' - ' + FIRST_NAME as FN,USER_ID " +
                                    " from  USER_MST " +
                                    " where TenentID =" + TID + " " +
                                    " order by USER_ID  desc ";
            DataTable dt = DataCon.GetDataTable(sql);
            drpreportto.DataSource = dt;
            drpreportto.DataTextField = "FN";
            drpreportto.DataValueField = "USER_ID";
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
            DateTime startdate = Convert.ToDateTime(txtdateFrom.Text);
            DateTime Enddate = Convert.ToDateTime(txtdateTO.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");
            DateTime end = Convert.ToDateTime(txtdateTO.Text);
            string paramStr = "";
            //validation



            if (drpcategoryfrom.SelectedValue != "0" && drpcategoryto.SelectedValue != "0")
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

            if (chkyear.Checked == true)
            {
                string SQOCommad = " select MyID as 'id' , MasterCODE as MasterCODE ,  TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , REMINDERNOTE as 'REMINDERNOTE', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate', Year(UploadDate) as showyear from CRMMainActivities" +
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
                string SQOCommads = " select MyID as 'id' , MasterCODE as MasterCODE , TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , REMINDERNOTE as 'REMINDERNOTE', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate' from CRMMainActivities" +
                                  " where TenentID = " + TID + " and UploadDate BETWEEN '" + stdate + "' AND '" + etdate + "' " + paramStr;

                //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
                //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

                SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
                SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
                DataSet ds1 = new DataSet();
                ADB1.Fill(ds1);
                DataTable dt1 = ds1.Tables[0];
                ListViewComplaint.DataSource = dt1;
                ListViewComplaint.DataBind();
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
                return DB.USER_MST.SingleOrDefault(p => p.USER_ID == ReportedBy && p.TenentID == TID).FIRST_NAME;
            }

        }


       
        public string GetDept(string ID, string type)
        {
            string Rettype = "";
            if (type == "Department")
            {
                int DID = Convert.ToInt32(ID);
                if (DB.DeptITSupers.Where(p => p.TenentID == TID && p.DeptID == DID).Count() > 0)
                {
                    Rettype = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == DID).DeptName;
                }
                else
                {
                    Rettype = "Not Found";
                }
            }
            else if (type == "PhyLocation")
            {

                if (ID == "PhyLocation")
                {
                    Rettype = "Location";
                }
                else
                {
                    int LID = Convert.ToInt32(ID);
                    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == LID).Count() > 0)
                    {
                        Rettype = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == LID).REFNAME1;
                    }
                    else
                    {
                        Rettype = "Not Found";
                    }
                }

            }
            else if (type == "Category")
            {
                if (ID == "Category")
                {
                    Rettype = "Category";
                }
                else
                {
                    int CID = Convert.ToInt32(ID);
                    if (DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == CID).Count() > 0)
                    {
                        Rettype = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == CID).CATNAME;
                    }
                    else
                    {
                        Rettype = "Not Found";
                    }
                }
            }
            else if (type == "SubCategory")
            {
                if (ID == "SubCategory")
                {
                    Rettype = "SubCategory";
                }
                else
                {
                    int sID = Convert.ToInt32(ID);
                    if (DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == sID).Count() > 0)
                    {
                        Rettype = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == sID).SUBCATNAME;
                    }
                    else
                    {
                        Rettype = "Not Found";
                    }
                }
            }
            return Rettype;
        }
        public string GetLOC(int ID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == ID).Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == ID).REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetCAT(int ID)
        {
            if (DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == ID).Count() > 0)
            {
                return DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == ID).CATNAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetSubCat(int ID)
        {
            if (DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == ID).Count() > 0)
            {
                return DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == ID).SUBCATNAME;
            }
            else
            {
                return "Not Found";
            }
        }

        //protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
            
        //    Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
        //    Label DeptCount = (Label)e.Item.FindControl("Label2");
        //    //if(lblDeptID.Text=="")
        //    //    lblDeptID.Text="99999";
        //    int Deptid = Convert.ToInt32(lblDeptID.Text);
        //    int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickDepartmentID == Deptid).Count();
        //    DeptCount.Text = count.ToString();
        //}

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            Label lbllocid = (Label)e.Item.FindControl("lbllocid");
            Label LOCCount = (Label)e.Item.FindControl("Label4");
            int LOCid = Convert.ToInt32(lbllocid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickPhysicalLocation == LOCid).Count();
            LOCCount.Text = count.ToString();
        }

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblCatid = (Label)e.Item.FindControl("lblCatid");
            Label CatCount = (Label)e.Item.FindControl("Label6");
            int Catid = Convert.ToInt32(lblCatid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickCatID == Catid).Count();
            CatCount.Text = count.ToString();

        }

        protected void ListView4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblSubCatid = (Label)e.Item.FindControl("lblSubCatid");
            Label SubCatCount = (Label)e.Item.FindControl("Label8");
            int subCatid = Convert.ToInt32(lblSubCatid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickSubCatID == subCatid).Count();
            SubCatCount.Text = count.ToString();
        }

               
        public void complainmonthfrom()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = "select distinct FORMAT(UploadDate, 'MMMM,yyyy') as monthyear, Month(UploadDate),Year(UploadDate) from CRMMainActivities where TenentID=" + TID + " and UploadDate is not NULL order by 2,1";
            DataTable dt = DataCon.GetDataTable(sql);
            drpmonthfrom.DataSource = dt;
            drpmonthfrom.DataTextField = "monthyear";
            drpmonthfrom.DataValueField = "monthyear";
            drpmonthfrom.DataBind();
            drpmonthfrom.Items.Insert(0, new ListItem("--All Month--", "0"));
        }

       
        //and Month(UploadDate) BETWEEN '" + drpmonthfrom.SelectedValue + "' AND ' " + drpmonthto.SelectedValue + " ' and Year(UploadDate) BETWEEN '" + drplistyear.SelectedValue + "' AND ' " + drplistyearto.SelectedValue + " '

        protected void Button1_Click(object sender, EventArgs e)
        {    
           
                string SQOCommad = " select MyID as 'id' , MasterCODE as MasterCODE ,  TickComplainType as 'complain' , TickDepartmentID as 'Department' , TickCatID as 'Category' , TickSubCatID as 'SubCategory' , REMINDERNOTE as 'REMINDERNOTE', ReportedBy as 'ReportedBy' , UploadDate as 'UploadDate', Year(UploadDate) as showyear from CRMMainActivities" +
                                 " where TenentID = " + TID + "and Year(UploadDate) = '2015' ";

                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];
                ListViewComplaint.DataSource = dt;
                ListViewComplaint.DataBind();              

            

        }

        protected void btnsub_Click(object sender, EventArgs e)
        {
            binddata();
        }

  
        protected void ListViewComplaint_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkpn1")
            {
                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkpn2")
            {
                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkpn4")
            {
                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            binddata();
        }

        protected void chkyear_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkdates_CheckedChanged(object sender, EventArgs e)
        {

        }     
        
    }
    
}