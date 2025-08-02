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
    public partial class Summerizedco : System.Web.UI.Page
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
                ListView1.DataSource = dt;
                ListView1.DataBind();

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
                ListView1.DataSource = dt1;
                ListView1.DataBind();
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



        //public void BindList()
        //{
        //    List<Database.CRMMainActivity> TempList = new List<Database.CRMMainActivity>();
        //    List<Database.CRMMainActivity> Listmain = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE").ToList();

        //    List<Database.CRMMainActivity> DeptList = Listmain.GroupBy(p => p.TickDepartmentID).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> DeptTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity Deptitem in DeptList)
        //    {

        //        DeptTemp.Add(Deptitem);
        //        DeptTemp.Single(p => p.TenentID == Deptitem.TenentID && p.MasterCODE == Deptitem.MasterCODE && p.MyID == Deptitem.MyID).Description = "Department";
        //        Database.CRMMainActivity obj = DeptTemp.Single(p => p.TenentID == Deptitem.TenentID && p.MasterCODE == Deptitem.MasterCODE && p.MyID == Deptitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(Deptitem);
        //    }

        //    List<Database.CRMMainActivity> LOCList = Listmain.GroupBy(p => p.TickPhysicalLocation).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> LOCTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity LOCitem in LOCList)
        //    {

        //        LOCTemp.Add(LOCitem);
        //        LOCTemp.Single(p => p.TenentID == LOCitem.TenentID && p.MasterCODE == LOCitem.MasterCODE && p.MyID == LOCitem.MyID).Description = "PhyLocation";
        //        Database.CRMMainActivity obj = LOCTemp.Single(p => p.TenentID == LOCitem.TenentID && p.MasterCODE == LOCitem.MasterCODE && p.MyID == LOCitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(LOCitem);
        //    }

        //    List<Database.CRMMainActivity> catList = Listmain.GroupBy(p => p.TickCatID).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> CatTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity catitem in catList)
        //    {
        //        CatTemp.Add(catitem);
        //        CatTemp.Single(p => p.TenentID == catitem.TenentID && p.MasterCODE == catitem.MasterCODE && p.MyID == catitem.MyID).Description = "Category";
        //        Database.CRMMainActivity obj = CatTemp.Single(p => p.TenentID == catitem.TenentID && p.MasterCODE == catitem.MasterCODE && p.MyID == catitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(catitem);
        //    }

        //    List<Database.CRMMainActivity> subcatList = Listmain.GroupBy(p => p.TickSubCatID).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> SubCatTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity subcatitem in subcatList)
        //    {
        //        SubCatTemp.Add(subcatitem);
        //        SubCatTemp.Single(p => p.TenentID == subcatitem.TenentID && p.MasterCODE == subcatitem.MasterCODE && p.MyID == subcatitem.MyID).Description = "SubCategory";
        //        Database.CRMMainActivity obj = SubCatTemp.Single(p => p.TenentID == subcatitem.TenentID && p.MasterCODE == subcatitem.MasterCODE && p.MyID == subcatitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(subcatitem);
        //    }
        //    int PhyLocationCOU = 0;
        //    int CategoryCOU = 0;
        //    int SubCategoryCOU = 0;

        //    List<CrmACT> ACTList = new List<CrmACT>();
        //    foreach (Database.CRMMainActivity tempitem in TempList)
        //    {
        //        string typ = tempitem.Description;
        //        if (typ == "Department")
        //        {
        //            CrmACT obj2 = new CrmACT();
        //            obj2.COMMANID = tempitem.TickDepartmentID.ToString();
        //            obj2.typee = tempitem.Description;
        //            ACTList.Add(obj2);
        //        }
        //        else if (typ == "PhyLocation")
        //        {
        //            if (PhyLocationCOU == 0)
        //            {
        //                CrmACT obj1 = new CrmACT();
        //                obj1.Tenent = tempitem.TenentID;
        //                obj1.MasterCode = 0;
        //                obj1.myid = 0;
        //                obj1.datee = tempitem.UploadDate.Value.Date;
        //                obj1.COMMANID = tempitem.TickPhysicalLocation.ToString();
        //                obj1.typee = tempitem.Description;
        //                ACTList.Add(obj1);
        //            }
        //            PhyLocationCOU++;
        //        }
        //        else if (typ == "Category")
        //        {
        //            if (CategoryCOU == 0)
        //            {
        //                CrmACT obj2 = new CrmACT();
        //                obj2.Tenent = tempitem.TenentID;
        //                obj2.MasterCode = 0;
        //                obj2.myid = 0;
        //              //  obj2.datee = tempitem.UploadDate.Value.Date;
        //                obj2.COMMANID = tempitem.TickCatID.ToString();
        //                obj2.typee = tempitem.Description;
        //                ACTList.Add(obj2);
        //            }
        //            CategoryCOU++;
        //        }
        //        else if (typ == "SubCategory")
        //        {
        //            if (SubCategoryCOU == 0)
        //            {
        //                CrmACT obj3 = new CrmACT();
        //                obj3.Tenent = tempitem.TenentID;
        //                obj3.MasterCode = 0;
        //                obj3.myid = 0;
        //                obj3.datee = tempitem.UploadDate.Value.Date;
        //                obj3.COMMANID = tempitem.TickSubCatID.ToString();
        //                obj3.typee = tempitem.Description;
        //                ACTList.Add(obj3);
        //            }
        //            SubCategoryCOU++;
        //        }
        //        CrmACT obj = new CrmACT();
        //        obj.Tenent = tempitem.TenentID;
        //        obj.MasterCode = tempitem.MasterCODE;
        //        obj.myid = tempitem.MyID;
        //        //obj.datee = tempitem.UploadDate.Value.Date;
        //        if (typ == "Department")
        //            obj.COMMANID = tempitem.TickDepartmentID.ToString();
        //        if (typ == "PhyLocation")
        //            obj.COMMANID = tempitem.TickPhysicalLocation.ToString();
        //        if (typ == "Category")
        //            obj.COMMANID = tempitem.TickCatID.ToString();
        //        if (typ == "SubCategory")
        //            obj.COMMANID = tempitem.TickSubCatID.ToString();
        //        obj.typee = tempitem.Description;
        //        ACTList.Add(obj);
        //    }
        //    ListView3.DataSource = ACTList;
        //    ListView3.DataBind();


        //   // old work
        //    //List<Database.CRMMainActivity> DeptList = Listmain.GroupBy(p => p.TickDepartmentID).Select(g => g.FirstOrDefault()).ToList();
        //    //ListView3.DataSource = DeptList;
        //    //ListView3.DataBind();

        //    //List<Database.CRMMainActivity> LOCList = Listmain.GroupBy(p => p.TickPhysicalLocation).Select(g => g.FirstOrDefault()).ToList();            
        //    //ListView1.DataSource = LOCList;
        //    //ListView1.DataBind();

        //    //List<Database.CRMMainActivity> catList = Listmain.GroupBy(p => p.TickCatID).Select(g => g.FirstOrDefault()).ToList();
        //    //ListView2.DataSource = catList;
        //    //ListView2.DataBind();

        //    //List<Database.CRMMainActivity> subcatList = Listmain.GroupBy(p => p.TickSubCatID).Select(g => g.FirstOrDefault()).ToList();
        //    //ListView4.DataSource = subcatList;
        //    //ListView4.DataBind();


        //}
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

        protected void btnmail_Click(object sender, EventArgs e)
        {
            binddata();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            binddata();
        }

        protected void chkyear_CheckedChanged(object sender, EventArgs e)
        {
            if (chkyear.Checked == true)
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
            if (chkdates.Checked == true)
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




    }
    public class CrmACT
    {
        public int Tenent { get; set; }
        public int MasterCode { get; set; }
        public int myid { get; set; }
        public DateTime datee { get; set; }
        public string COMMANID { get; set; }
        public string TickPhysicalLocation { get; set; }
        public string TickCatID { get; set; }
        public string TickSubCatID { get; set; }
        public string typee { get; set; }
    }
}