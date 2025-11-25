using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Web.POS
{
  public partial class ClientTiketR : System.Web.UI.Page
  {
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
    SqlCommand command2 = new SqlCommand();
    CallEntities DB = new CallEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        complainyear();

        chklog.Checked = true;
        lblUser.Text = "Pending Issues";
        lbldatesd.Text = DateTime.Now.ToString();
        txtdates.Enabled = true;
        txtdates.Text = DateTime.Now.ToString();
        aspcomment.Enabled = false;
        txtComent.Enabled = false;
        drpStatus.Enabled = false;

        if (Session["USER"] == null || !(Session["USER"] is USER_MST user))
        {
          Session.Abandon();
          Response.Redirect("/ACM/SessionTimeOut.aspx");
          return;
        }

        int TID = user.TenentID;
        int uid1 = user.USER_ID;

        int dt1 = 0;
        if (!int.TryParse(drpyears.SelectedValue, out dt1))
        {
          dt1 = DateTime.Now.Year; 
        }

        if (!string.IsNullOrEmpty(Request.QueryString["status"]) &&
            Request.QueryString["status"] == "pending")
        {
          int admin = 0;
          int UIR = user.USER_ID;

          var company = DB.MYCOMPANYSETUPs.FirstOrDefault(p => p.TenentID == TID && !string.IsNullOrEmpty(p.USERID));
          if (company != null)
            admin = Convert.ToInt32(company.USERID);

          if (admin == UIR)
          {
            BindData();
            ToalCoun();
            ViewState["StatusAll"] = "pending";

            ltsRemainderNotes.DataSource = DB.CRMMainActivities
                .Where(p => p.TenentID == TID &&
                            p.MyStatus == "pending" &&
                            p.ACTIVITYE == "helpdesk" &&
                            p.UploadDate.HasValue &&
                            p.UploadDate.Value.Year == dt1)
                .OrderByDescending(p => p.UploadDate)
                .ToList();

            ltsRemainderNotes.DataBind();
            PnlBindTick.Visible = true;
            pnlTicki.Visible = false;
            return;
          }
        }

        BindData();
        ToalCoun();
        ViewState["StatusAll"] = "pending";

        ltsRemainderNotes.DataSource = DB.CRMMainActivities
            .Where(p => p.TenentID == TID &&
                        p.MyStatus == "pending" &&
                        p.ACTIVITYE == "helpdesk" &&
                        p.UploadDate.HasValue &&
                        p.UploadDate.Value.Year == dt1)
            .OrderByDescending(p => p.UploadDate)
            .ToList();

        ltsRemainderNotes.DataBind();
        PnlBindTick.Visible = true;
        pnlTicki.Visible = false;

        if (!string.IsNullOrEmpty(Request.QueryString["ACT"]))
        {
          if (int.TryParse(Request.QueryString["ACT"], out int ACTID) &&
              !string.IsNullOrEmpty(Request.QueryString["STD"]))
          {
            string STDD = Request.QueryString["STD"];
            ViewState["TIkitNumber"] = ACTID;
            getStatusQUERY(STDD);
            getCommunicatinData();
            panChat.Visible = true;
          }
        }
      }
    }
    public void complainyear()
    {
      if (Session["USER"] == null)
      {
        Response.Redirect("~/ACM/Login.aspx");
        return;
      }

      var user = (USER_MST)Session["USER"];
      if (user == null || user.TenentID == 0)
      {
        Response.Redirect("~/ACM/Login.aspx");
        return;
      }

      int TID = user.TenentID;
      string sql = " select distinct Year(UploadDate) as showyear from CRMMainActivities where TenentID =" + TID + " and UploadDate is not NULL order by showyear desc"; 

      DataTable dt = DataCon.GetDataTable(sql) ?? new DataTable();
      if (drpyears != null)
      {
        drpyears.DataSource = dt;
        drpyears.DataTextField = "showyear";
        drpyears.DataValueField = "showyear";
        drpyears.DataBind();
      }

      string sql1 = " select distinct FORMAT(UploadDate, 'MMMM') as monthyear, Month(UploadDate) as month from CRMMainActivities where TenentID=" + TID + " and UploadDate is not NULL order by 2";

      DataTable dt1 = DataCon.GetDataTable(sql1) ?? new DataTable();
      if (drpmonths != null)
      {
        drpmonths.DataSource = dt1;
        drpmonths.DataTextField = "monthyear";
        drpmonths.DataValueField = "month";
        drpmonths.DataBind();
        drpmonths.Items.Insert(0, new ListItem("-- Select Month--", "0"));
      }

      if (drpusers != null)
      {
        drpusers.DataSource = DB.USER_MST
            .Where(p => p.TenentID == TID && p.ACTIVEUSER == true && p.THEME_NAME == "YES")
            .OrderBy(p => p.FIRST_NAME);
        drpusers.DataTextField = "FIRST_NAME";
        drpusers.DataValueField = "USER_ID";
        drpusers.DataBind();
        drpusers.Items.Insert(0, new ListItem("-- Select User--", "0"));
      }
    }
    public void BindData()
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);

      var Department = (from Dept in DB.DeptITSupers.Where(p => p.TenentID == TID && p.Status == "start").OrderBy(p => p.DeptName)
                        select new
                        {
                          Dep1 = Dept.DeptName,
                          Dep2 = Dept.DeptID
                        }
          );

      drpSDepartment.DataSource = Department;//DB.DeptITSupers.Where(p => p.TenentID == TID);
      drpSDepartment.DataTextField = "Dep1"; //"DeptName";
      drpSDepartment.DataValueField = "Dep2"; //"DeptID";
      drpSDepartment.DataBind();
      drpSDepartment.Items.Insert(0, new ListItem("-- Select --", "0"));

      drpfoloemp.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID && p.Active == true && p.SynID == 1).OrderBy(p => p.firstname);
      drpfoloemp.DataTextField = "firstname";
      drpfoloemp.DataValueField = "employeeID";
      drpfoloemp.DataBind();
      drpfoloemp.Items.Insert(0, new ListItem("-- Select --", "0"));


      drpusermt.DataSource = DB.USER_MST.Where(p => p.TenentID == TID && p.ACTIVEUSER == true).OrderBy(p => p.FIRST_NAME);
      drpusermt.DataTextField = "FIRST_NAME";
      drpusermt.DataValueField = "USER_ID";
      drpusermt.DataBind();
      drpusermt.Items.Insert(0, new ListItem("-- Select --", "0"));

      drpComplainType.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "complain" && p.ACTIVE == "Y").OrderBy(p => p.REFNAME1);
      drpComplainType.DataTextField = "REFNAME1";
      drpComplainType.DataValueField = "REFID";
      drpComplainType.DataBind();
      drpComplainType.Items.Insert(0, new ListItem("-- Select --", "0"));

      DrpPhysicalLocation.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.ACTIVE == "Y").OrderBy(p => p.REFNAME1);
      DrpPhysicalLocation.DataTextField = "REFNAME1";
      DrpPhysicalLocation.DataValueField = "REFID";
      DrpPhysicalLocation.DataBind();
      DrpPhysicalLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
      //Main Category
      DrpTCatSubCate.DataSource = DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.Status == 1).OrderBy(p => p.CATNAME);
      DrpTCatSubCate.DataTextField = "CATNAME";
      DrpTCatSubCate.DataValueField = "CATID";
      DrpTCatSubCate.DataBind();

      DrpSubCat.DataSource = DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.Status == 1).OrderBy(p => p.SUBCATNAME);
      DrpSubCat.DataTextField = "SUBCATNAME";
      DrpSubCat.DataValueField = "SUBCATID";
      DrpSubCat.DataBind();

      //Sub Category
      //int CID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);

      //List<ICCATSUBCAT> list = DB.ICCATSUBCATs.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.CATID == CID).ToList();
      //List<ICSUBCATEGORY> tempList = new List<ICSUBCATEGORY>();
      //foreach (ICCATSUBCAT item in list)
      //{
      //    var obj = DB.ICSUBCATEGORies.FirstOrDefault(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == item.SUBCATID);
      //    tempList.Add(obj);

      //}
      //DrpSubCat.DataSource = DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.Status == 1).OrderBy(p => p.SUBCATNAME);
      //DrpSubCat.DataTextField = "SUBCATNAME";
      //DrpSubCat.DataValueField = "SUBCATID";
      //DrpSubCat.DataBind();
      ////if (list.Count <= 0)
      //DrpSubCat.Items.Insert(0, new ListItem("-- Select --", "0"));

      //var result2 = (from pm in DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE == "Ticket" && p.Active == "1" && p.PARENT_CATID != 0)
      //               select new
      //               {
      //                   p1 = pm.CAT_DESCRIPTION + " - " + pm.CAT_NAME1,
      //                   p2 = pm.CATID// + "." + pm.PARENT_CATID 
      //               });
      //DrpTCatSubCate.DataSource = result2;
      //DrpTCatSubCate.DataTextField = "p1";
      //DrpTCatSubCate.DataValueField = "p2";
      //DrpTCatSubCate.DataBind();
      //DrpTCatSubCate.Items.Insert(0, new ListItem("-- Select --", "0"));

    }
    public void ToalCoun()
    {
      int admin = 0;
      int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      string Userid = UIN.ToString();


      if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
        admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
      if (admin == UIN)
      {
        int dt = Convert.ToInt32(drpyears.SelectedValue);
        int rb = Convert.ToInt32(drpusers.SelectedValue);
        string patientname = txtpatientnames.Text;

        string comno = txtcnose.Text;
        //&& p.Year(UploadDate) == '2019'
        string paramStr = "";
        if (drpusers.SelectedValue != "0" && drpusers.SelectedValue != null)
        {
          paramStr += "and ReportedBy = '" + drpusers.SelectedValue + "'  ";

        }

        if (drpmonths.SelectedValue != "0" && drpmonths.SelectedValue != null)
        {
          paramStr += "and Month(UploadDate) = '" + drpmonths.SelectedValue + "'  ";
        }



        string SQOCommads = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                           " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                           " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                           " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "' and MyStatus ='Pending' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%' and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];
        int count = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
          count++;
        }

        int Pending = count;
        leblANew1.Text = Pending.ToString();


        // int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "Completed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt && p.ReportedBy == rb && p.Patient_Name == patientname  && p.ComplaintNumber == comno).Count();

        string SQOCommadin = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                         " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                         " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                         " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='InProgress' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        SqlCommand CMDin = new SqlCommand(SQOCommadin, con);
        SqlDataAdapter ADBin = new SqlDataAdapter(CMDin);
        DataSet dsin = new DataSet();
        ADBin.Fill(dsin);
        DataTable dtin = dsin.Tables[0];
        int countin = 0;
        for (int i = 0; i < dtin.Rows.Count; i++)
        {
          countin++;
        }

        int Progress = countin;
        leblANew3.Text = Progress.ToString();

        string SQOCommadi = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                         " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                         " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                         " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='Closed' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        SqlCommand CMDi = new SqlCommand(SQOCommadi, con);
        SqlDataAdapter ADBi = new SqlDataAdapter(CMDi);
        DataSet dsi = new DataSet();
        ADBi.Fill(dsi);
        DataTable dti = dsi.Tables[0];
        int counti = 0;
        for (int i = 0; i < dti.Rows.Count; i++)
        {
          counti++;
        }

        int Closed = counti;
        leblANew4.Text = Closed.ToString();
        int Delivered = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "Delivered" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt && p.ReportedBy == rb && p.Patient_Name == patientname && p.ComplaintNumber == comno).Count();
        //  leblANew5.Text = Delivered.ToString();



        return;
      }
      else
      {
        string paramStr = "";


        int dt = Convert.ToInt32(drpyears.SelectedValue);
        if (drpusers.SelectedValue != "0" && drpusers.SelectedValue != null)
        {
          paramStr += "and ReportedBy = '" + drpusers.SelectedValue + "'  ";

        }

        if (drpmonths.SelectedValue != "0" && drpmonths.SelectedValue != null)
        {
          paramStr += "and Month(UploadDate) = '" + drpmonths.SelectedValue + "'  ";
        }

        string SQOCommads = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                           " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                           " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                           " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='Pending' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%' and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];
        int count = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
          count++;
        }

        int Pending = count;
        leblANew1.Text = Pending.ToString();


        //  int Completed = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "Completed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt && p.ReportedBy == rb && p.Patient_Name == patientname && p.MRN == mrn && p.ComplaintNumber == comno).Count();

        string SQOCommadin = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                         " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                         " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                         " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='InProgress' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        SqlCommand CMDin = new SqlCommand(SQOCommadin, con);
        SqlDataAdapter ADBin = new SqlDataAdapter(CMDin);
        DataSet dsin = new DataSet();
        ADBin.Fill(dsin);
        DataTable dtin = dsin.Tables[0];
        int countin = 0;
        for (int i = 0; i < dtin.Rows.Count; i++)
        {
          countin++;
        }

        int Progress = countin;
        leblANew3.Text = Progress.ToString();

        string SQOCommadi = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                         " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                         " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                         " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='Closed' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        SqlCommand CMDi = new SqlCommand(SQOCommadi, con);
        SqlDataAdapter ADBi = new SqlDataAdapter(CMDi);
        DataSet dsi = new DataSet();
        ADBi.Fill(dsi);
        DataTable dti = dsi.Tables[0];
        int counti = 0;
        for (int i = 0; i < dti.Rows.Count; i++)
        {
          counti++;
        }

        int Closed = counti;
        leblANew4.Text = Closed.ToString();

        return;
      }
      if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk" && (p.TickDepartmentID != null)).Count() > 0)
      {
        if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).Count() == 1)
        {
          int dt = Convert.ToInt32(drpyears.SelectedValue);
          int empid = Convert.ToInt32(DB.tbl_Employee.Single(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).employeeID);
          int Pending = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == "Pending" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.Emp_ID == empid && p.MyStatus == "Pending" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
          leblANew1.Text = Pending.ToString();
          int Completed = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == "Completed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.Emp_ID == empid && p.MyStatus == "Completed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
          //   leblANew2.Text = Completed.ToString();
          int Progress = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == "InProgress" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.Emp_ID == empid && p.MyStatus == "Progress" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
          leblANew3.Text = Progress.ToString();
          int Closed = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == "Closed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.Emp_ID == empid && p.MyStatus == "Closed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
          leblANew4.Text = Closed.ToString();
          int Delivered = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == "Delivered" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.Emp_ID == empid && p.MyStatus == "Delivered" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
          //  leblANew5.Text = Delivered.ToString();
        }
      }
      if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").Count() > 0)
      {
        List<Database.CRMMainActivity> GList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").GroupBy(p => p.GROUPCODE).Select(p => p.FirstOrDefault()).ToList();
        foreach (Database.CRMMainActivity item in GList)
        {
          int ITGroup = Convert.ToInt32(item.GROUPCODE);
          if (DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.ITGROUPID == ITGroup && p.USERCODE == Userid).Count() == 1)
          {
            int dt = Convert.ToInt32(drpyears.SelectedValue);
            int Pending = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == "Pending" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == "Pending" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
            leblANew1.Text = Pending.ToString();
            int Completed = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == "Completed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == "Completed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
            //  leblANew2.Text = Completed.ToString();
            int Progress = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == "InProgress" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == "Progress" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
            leblANew3.Text = Progress.ToString();
            int Closed = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == "Closed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == "Closed" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
            leblANew4.Text = Closed.ToString();
            int Delivered = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == "Delivered" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == "Delivered" && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).Count();
            //  leblANew5.Text = Delivered.ToString();
          }
        }
      }
    }
    protected void LinkRefreshTicket_Click(object sender, EventArgs e)
    {
      ToalCoun();
    }
    public void BindRemainderNote()
    {
      int admin = 0;
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      string status = ViewState["StatusAll"].ToString();
      if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
        admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);

      string paramStr = "";
      if (drpusers.SelectedValue != "0" && drpusers.SelectedValue != null)
      {
        paramStr += "and ReportedBy = '" + drpusers.SelectedValue + "'  ";

      }

      if (drpmonths.SelectedValue != "0" && drpmonths.SelectedValue != null)
      {
        paramStr += "and Month(UploadDate) = '" + drpmonths.SelectedValue + "'  ";
      }

      if (admin == UIN)
      {

        int dt = Convert.ToInt32(drpyears.SelectedValue);

        string SQOCommads = " select MasterCODE as 'MasterCODE' , FoloEmp as 'FoloEmp' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                            " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                            " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber' , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                            " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='" + status + "' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
        //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];


        ltsRemainderNotes.DataSource = dt1; // DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == status && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt ).OrderByDescending(p => p.UploadDate);
        ltsRemainderNotes.DataBind();
        return;
      }
      else
      {
        int dt = Convert.ToInt32(drpyears.SelectedValue);
        string SQOCommads = " select MasterCODE as 'MasterCODE' ,  FoloEmp as 'FoloEmp' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                            " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                            " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' ,UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                            " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='" + status + "' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
        //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];

        ltsRemainderNotes.DataSource = dt1; //DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == status && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
        ltsRemainderNotes.DataBind();
      }
      //string Userid = UIN.ToString();
      //if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).Count() == 1)
      //{
      //    int dt = Convert.ToInt32(drpyears.SelectedValue);
      //    int empid = Convert.ToInt32(DB.tbl_Employee.Single(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).employeeID);
      //    ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == status && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.Emp_ID == empid && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).OrderByDescending(p => p.UPDTTIME);
      //    ltsRemainderNotes.DataBind();
      //}
      //if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").Count() > 0)
      //{
      //    List<Database.CRMMainActivity> GList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").GroupBy(p => p.GROUPCODE).Select(p => p.FirstOrDefault()).ToList();
      //    foreach (Database.CRMMainActivity item in GList)
      //    {
      //        int ITGroup = Convert.ToInt32(item.GROUPCODE);
      //        if (DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.ITGROUPID == ITGroup && p.USERCODE == Userid).Count() == 1)
      //        {
      //            int dt = Convert.ToInt32(drpyears.SelectedValue);
      //            ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == status && p.GROUPCODE == ITGroup && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).OrderByDescending(p => p.UPDTTIME);
      //            ltsRemainderNotes.DataBind();
      //        }
      //    }
      //}
    }
    public void getStatusQUERY(string StatusAll)
    {
      int admin = 0;
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      if (ViewState["TIkitNumber"] != null)
      {
        int tick = Convert.ToInt32(ViewState["TIkitNumber"]);
        if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
          admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
        if (admin == UIN)
        {
          int dt = Convert.ToInt32(drpyears.SelectedValue);
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.MasterCODE == tick && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
          ltsRemainderNotes.DataBind();
          PnlBindTick.Visible = true;
          pnlTicki.Visible = false;
          return;
        }
        else
        {
          int dt = Convert.ToInt32(drpyears.SelectedValue);
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk" && p.MasterCODE == tick && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
          ltsRemainderNotes.DataBind();
        }
        string Userid = UIN.ToString();
        if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).Count() == 1)
        {
          int dt = Convert.ToInt32(drpyears.SelectedValue);
          int empid = Convert.ToInt32(DB.tbl_Employee.Single(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).employeeID);
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == StatusAll && p.USERCODE == UIN && p.ACTIVITYE == "helpdesk" && p.MasterCODE == tick && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == StatusAll && p.Emp_ID == empid && p.ACTIVITYE == "helpdesk" && p.MasterCODE == tick && p.UploadDate.Value.Year == dt)).OrderByDescending(p => p.UploadDate);
          ltsRemainderNotes.DataBind();
        }
        if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk").Count() > 0)
        {
          List<Database.CRMMainActivity> GList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.MasterCODE == tick).GroupBy(p => p.GROUPCODE).Select(p => p.FirstOrDefault()).ToList();
          foreach (Database.CRMMainActivity item in GList)
          {
            int ITGroup = Convert.ToInt32(item.GROUPCODE);
            if (DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.ITGROUPID == ITGroup && p.USERCODE == Userid).Count() == 1)
            {
              int dt = Convert.ToInt32(drpyears.SelectedValue);
              ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.MasterCODE == tick && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
              ltsRemainderNotes.DataBind();
            }
          }
        }
        PnlBindTick.Visible = true;
        pnlTicki.Visible = false;
      }
    }
    public void getStatusAll(string StatusAll)
    {
      ViewState["StatusAll"] = StatusAll;


      int admin = 0;
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
        admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);

      string paramStr = "";
      if (drpusers.SelectedValue != "0" && drpusers.SelectedValue != null)
      {
        paramStr += "and ReportedBy = '" + drpusers.SelectedValue + "'  ";

      }

      if (admin == UIN)
      {
        int dt = Convert.ToInt32(drpyears.SelectedValue);
        string SQOCommads = " select MasterCODE as 'MasterCODE' ,  FoloEmp as 'FoloEmp' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                            " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                            " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName', UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                            " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='" + StatusAll + "' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
        //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];




        ltsRemainderNotes.DataSource = dt1;//DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
        ltsRemainderNotes.DataBind();
        PnlBindTick.Visible = true;
        pnlTicki.Visible = false;
        return;
      }
      else
      {
        int dt = Convert.ToInt32(drpyears.SelectedValue);
        string SQOCommads = " select MasterCODE as 'MasterCODE' ,  FoloEmp as 'FoloEmp' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                           " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                           " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                           " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "'and MyStatus ='" + StatusAll + "' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%'  and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

        //+ " and TickComplainType BETWEEN '" + drpcomplaintypefrom.SelectedValue + "' AND  ' " + drpcomplaintypeto.SelectedValue + " ' and UploadDate BETWEEN  '" + stdate + "' AND '" + etdate + "' and TickDepartmentID BETWEEN '" + drpdepartmentfrom.SelectedValue + "' AND  '" + drpdepartmentto.SelectedValue + "' " +
        //" and TickCatID BETWEEN '" + drpcategoryfrom.SelectedValue + "' AND  ' " + drpcategoryto.SelectedValue + " ' and TickSubCatID BETWEEN '" + drpscategoryfrom.SelectedValue + "' AND  '" + drpscategoryto.SelectedValue + "' and ReportedBy BETWEEN '" + drpreportfrom.SelectedValue + "' AND  '" + drpreportto.SelectedValue + " ' and Remarks like '%" + txtcomplain.Text + "%' ";

        SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
        SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB1.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];


        ltsRemainderNotes.DataSource = dt1;//DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
        ltsRemainderNotes.DataBind();
      }
      string Userid = UIN.ToString();
      if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).Count() == 1)
      {
        int dt = Convert.ToInt32(drpyears.SelectedValue);
        int empid = Convert.ToInt32(DB.tbl_Employee.Single(p => p.TenentID == TID && p.userID == Userid && p.employeeID != null && p.employeeID != 0).employeeID);
        ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.MyStatus == StatusAll && p.Emp_ID == empid && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt)).OrderByDescending(p => p.UploadDate);
        ltsRemainderNotes.DataBind();
      }
      if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk").Count() > 0)
      {
        int dt = Convert.ToInt32(drpyears.SelectedValue);
        List<Database.CRMMainActivity> GList = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt).GroupBy(p => p.GROUPCODE).Select(p => p.FirstOrDefault()).ToList();
        foreach (Database.CRMMainActivity item in GList)
        {
          int ITGroup = Convert.ToInt32(item.GROUPCODE);
          if (DB.TBLGROUP_USER.Where(p => p.TenentID == TID && p.ITGROUPID == ITGroup && p.USERCODE == Userid).Count() == 1)
          {

            ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => (p.TenentID == TID && p.GROUPCODE == ITGroup && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt) || (p.TenentID == TID && p.USERCODE == UIN && p.MyStatus == StatusAll && p.ACTIVITYE == "helpdesk")).OrderByDescending(p => p.UploadDate);
            ltsRemainderNotes.DataBind();
          }
        }
      }
      PnlBindTick.Visible = true;
      pnlTicki.Visible = false;
    }
    public void maxComplainID()
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      string maxid = "select max(MyID)+1 AS NEWComplaintNumber from CRMMainActivities where tenentid=10 and year(uploaddate)=year(GETDATE()) and month(uploaddate)=month(GETDATE())";
      DataTable dt = DataCon.GetDataTable(maxid);
      DateTime startdate = DateTime.Now;
      string stdate = startdate.ToString("yyyyMM");
      string num = dt.Rows[0].ItemArray[0].ToString();
      lblcomplainno.Text = stdate + num;
      string cno = lblcomplainno.Text;
      ViewState["ComplaintNumber"] = cno;

    }
    protected void linkAllNew_Click(object sender, EventArgs e)
    {
      // Database.CRMMainActivity objCRMMainActivities = new Database.CRMMainActivity();
      clen();
      maxComplainID();
      getStatusAll("NEW");
      pnlTicki.Visible = true;
      panChat.Visible = false;
      pnlinfo.Visible = false;
      PnlBindTick.Visible = false;
      pnlSuccessMsg.Visible = false;
      btnSave.Text = "Submit";
      btnCancel.Visible = true;
      btncack.Visible = false;
      btndelete.Visible = false;
      btncloseandupdate.Enabled = true;
      btncloseandupdate.Text = "Close Tkt & Save";
    }
    protected void linkAllPending_Click(object sender, EventArgs e)
    {
      getStatusAll("Pending");
      panChat.Visible = true;
      PnlBindTick.Visible = true;
      pnlTicki.Visible = false;
      linkreopen.Visible = false;
      lblUser.Text = "Pending Issues";
      listChet.Visible = true;
      pnlSuccessMsg.Visible = false;
    }
    protected void linkallComplet_Click(object sender, EventArgs e)
    {

      getStatusAll("Completed");
      linkreopen.Visible = false;
      lblUser.Text = "Completed Issues";
      listChet.Visible = false;
      pnlSuccessMsg.Visible = false;
    }
    protected void linkAllProgress_Click(object sender, EventArgs e)
    {
      getStatusAll("InProgress");
      linkreopen.Visible = false;
      lblUser.Text = "In process Issues";
      listChet.Visible = false;
      pnlSuccessMsg.Visible = false;
    }
    protected void linkAllClose_Click(object sender, EventArgs e)
    {
      int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      getStatusAll("Closed");
      if (DB.tblHelpDeskUserSets.Where(p => p.TenentID == 10 && p.USER_ID == UIN && p.reopen == true).Count() == 1)
      {
        linkreopen.Visible = true;
      }
      lblUser.Text = "Closed Issues";
      listChet.Visible = false;
      pnlSuccessMsg.Visible = false;
    }
    protected void linkAllDeliverd_Click(object sender, EventArgs e)
    {
      getStatusAll("Delivered");
      linkreopen.Visible = false;
      listChet.Visible = false;
      pnlSuccessMsg.Visible = false;
    }
    public void sendEmail(string body, string email)
    {
      //if (String.IsNullOrEmpty(email))
      //    return;
      //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
      //msg.Subject = "Thanks you for Visiting our site..";
      //msg.From = new System.Net.Mail.MailAddress("supportteam@digital53.com");
      //msg.To.Add(new System.Net.Mail.MailAddress(email));
      //msg.BodyEncoding = System.Text.Encoding.UTF8;
      //msg.Body = body;
      //msg.IsBodyHtml = true;
      //msg.Priority = MailPriority.High;
      //System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
      //smpt.UseDefaultCredentials = false;
      //smpt.Host = "mail.ayosoftech.com";
      //smpt.Port = 587;
      //smpt.EnableSsl = false;
      //smpt.Credentials = new System.Net.NetworkCredential("info@ayosoftech.com", "P@ssw0rd123");
      //smpt.Send(msg);
    }
    public void clen()
    {
      txtpatientname.Text = "";
      txtMRN.Text = "";
      txtstaffname.Text = "";
      chklist.Checked = false;
      chklog.Checked = false;
      chkirno.Checked = false;
      txtComent.Text = txtMessage.Text = txtSubject.Text = "";
      //  drpActivityName.SelectedValue = "Help Desk";
      drpComplainType.SelectedIndex = 0;
      DrpPhysicalLocation.SelectedIndex = 0;
      DrpTCatSubCate.SelectedIndex = 0;
      //DrpSubCat.SelectedIndex = 0;
      drpSDepartment.SelectedIndex = 0;
      txtcontact.Text = "";
      txtdates.Text = "";
      //  pnlSuccessMsg.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
      //using (TransactionScope scope = new TransactionScope())
      //{

      if (btnSave.Text == "Submit")
      {
        string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
        int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
        Database.CRMMainActivity objCRMMainActivities = new Database.CRMMainActivity();
        // objtbl_DMSTikit_Mst.ID = DB.tbl_DMSTikit_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSTikit_Mst.Max(p => p.ID) + 1) : 1;
        objCRMMainActivities.TenentID = TID;
        objCRMMainActivities.COMPID = 1;
        objCRMMainActivities.Prefix = "ONL";
        string maxid = "select ISNull(max(MyID),0)+1 AS NEWComplaintNumber from CRMMainActivities where tenentid=" + TID + " and year(uploaddate)=year(GETDATE()) and month(uploaddate)=month(GETDATE())";
        DataTable dt = DataCon.GetDataTable(maxid);
        DateTime startdate = DateTime.Now;
        int year = startdate.Year % 100; 
        int month = startdate.Month;
        string num = dt.Rows[0].ItemArray[0].ToString();
        int sequence = Convert.ToInt32(num);
        TimeSpan onlytime = DateTime.Now.TimeOfDay;
        
        int master = (year * 1000000) + (month * 10000) + sequence;
        objCRMMainActivities.MasterCODE = master;
        objCRMMainActivities.LinkMasterCODE = master;

        objCRMMainActivities.LocationID = 1;
        objCRMMainActivities.MyID = Convert.ToInt32(num);
        objCRMMainActivities.RouteID = 1;
        objCRMMainActivities.USERCODE = UID;
        objCRMMainActivities.ACTIVITYA = "Reference";
        objCRMMainActivities.ACTIVITYA2 = "TKT";
        objCRMMainActivities.ACTIVITYE = "helpdesk";
        objCRMMainActivities.Reference = "helpdesk";
        objCRMMainActivities.AMIGLOBAL = true;
        objCRMMainActivities.MYPERSONNEL = false;
        objCRMMainActivities.INTERVALDAYS = 1;
        objCRMMainActivities.REPEATFOREVER = false;
        objCRMMainActivities.REPEATTILL = DateTime.Now;
        objCRMMainActivities.ESTCOST = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.ESTCOST) + 1) : 1;
        objCRMMainActivities.USERCODEENTERED = txtpatientname.Text;
        objCRMMainActivities.UPDTTIME = DateTime.Now; ///Convert.ToDateTime(txtdates.Text);///Convert.ToDateTime(txtdates.Text.Split(' ')[0]+" " + onlytime);
        objCRMMainActivities.USERNAME = strUName;
        objCRMMainActivities.Remarks = txtMessage.Text;
        objCRMMainActivities.Version = txtMessage.Text;
        if (drpinvestresult.SelectedValue != null && drpinvestresult.SelectedValue != "0")
        {
          objCRMMainActivities.Type = Convert.ToInt32(drpinvestresult.SelectedValue);
        }
        else
        {
          objCRMMainActivities.Type = 1;
        }
        objCRMMainActivities.MyStatus = "Pending";
        objCRMMainActivities.UploadDate = DateTime.Now;//Convert.ToDateTime(txtdates.Text);                
        objCRMMainActivities.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
        objCRMMainActivities.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
        objCRMMainActivities.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
        objCRMMainActivities.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
        objCRMMainActivities.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
        objCRMMainActivities.subject = txtSubject.Text;
        int Deptidd = Convert.ToInt32(drpSDepartment.SelectedValue);
        int Suppid = Convert.ToInt32(DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Deptidd).SuperVisorID);
        objCRMMainActivities.Emp_ID = Suppid;
        string ITGroupUser = DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == Suppid).Count() == 1 ? DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == Suppid).userID : "0";
        int ITGroupID = DB.TBLGROUPs.Where(p => p.TenentID == TID && p.USERCODE == ITGroupUser).Count() == 1 ? Convert.ToInt32(DB.TBLGROUPs.Single(p => p.TenentID == TID && p.USERCODE == ITGroupUser).ITGROUPID) : 0;
        objCRMMainActivities.GROUPCODE = ITGroupID;
        objCRMMainActivities.REMINDERNOTE = txtMessage.Text;
        objCRMMainActivities.Description = txtMessage.Text;
        objCRMMainActivities.Active = true;
        objCRMMainActivities.Patient_Name = txtpatientname.Text;
        objCRMMainActivities.FoloEmp = Convert.ToInt32(drpfoloemp.SelectedValue);
        if (txtMRN.Text != "")
        {
          objCRMMainActivities.MRN = txtMRN.Text;
        }
        if (chklog.Checked == true)
        {
          objCRMMainActivities.UseReciepeID = 1;
        }
        else
        {
          objCRMMainActivities.UseReciepeID = 0;
        }
        objCRMMainActivities.StaffInvoice = true;
        objCRMMainActivities.IRDone = true;
        objCRMMainActivities.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
        objCRMMainActivities.ComplaintNumber = master.ToString();
        objCRMMainActivities.Contact = txtcontact.Text;
        objCRMMainActivities.IncidentReport = true;
        String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "COMPID = 1  MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = " + objCRMMainActivities.MasterCODE + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;
        String tablename = "CRMMainActivity";
        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;
        objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno);
        DB.CRMMainActivities.AddObject(objCRMMainActivities);
        DB.SaveChanges();
        lblmsgl.Visible = false;
        lblMsg.Text = "Data Save Successfully";
        pnlSuccessMsg.Visible = true;

        Database.CRMActivity objCRMActivity = new Database.CRMActivity();

        objCRMActivity.TenentID = TID;
        objCRMActivity.COMPID = 1;
        objCRMActivity.Prefix = "ONL";
        objCRMActivity.MasterCODE = master;
        objCRMActivity.MyLineNo = TID;
        int activityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
        objCRMActivity.ActivityID = activityID;
        objCRMActivity.LocationID = 1;
        objCRMActivity.LinkMasterCODE = master;
        objCRMActivity.MenuID = Convert.ToInt32(0);
        objCRMActivity.NextUser = strUName;
        objCRMActivity.ACTIVITYTYPE = "helpdesk";
        objCRMActivity.REFTYPE = "Ticket";
        objCRMActivity.REFSUBTYPE = "CRM";
        objCRMActivity.PerfReferenceNo = "CRM";
        objCRMActivity.EarlierRefNo = "A";
        objCRMActivity.NextRefNo = "A";
        objCRMActivity.GroupBy = "helpdesk";
        objCRMActivity.USERCODE = UID.ToString();
        objCRMActivity.AMIGLOBAL = "Y";
        objCRMActivity.MYPERSONNEL = "Y";
        //string data = txtMessage.Text;
        //string[] words = data.Split(' ');
        //string paramStr = "";

        //foreach (string word in words)
        //{
        //    Console.WriteLine(word + objCRMMainActivities.MyStatus);
        //    paramStr += word +  objCRMMainActivities.MyStatus;

        //}
        objCRMActivity.ActivityPerform = txtMessage.Text;
        objCRMActivity.REMINDERNOTE = "Pending";
        objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.ESTCOST);
        objCRMActivity.GROUPCODE = "A";
        objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.GROUPCODE);
        objCRMActivity.USERCODEENTERED = strUName;
        objCRMActivity.UrlRedirct = Request.Url.AbsolutePath;
        objCRMActivity.UPDTTIME = DateTime.Now;//Convert.ToDateTime(txtdates.Text); ;
        objCRMActivity.UploadDate = DateTime.Now; // Convert.ToDateTime(txtdates.Text); ;
        objCRMActivity.InitialDate = Convert.ToDateTime(txtdates.Text.Split(' ')[0] + " " + onlytime); //DateTime.Now;
        objCRMActivity.DeadLineDate = DateTime.Now;
        objCRMActivity.ExpStartDate = DateTime.Now;
        objCRMActivity.USERNAME = strUName;
        objCRMActivity.RouteID = "helpdesk";
        objCRMActivity.MyStatus = "Pending";
        objCRMActivity.Active = "N";

        if (drpinvestresult.SelectedValue != "0")
        {
          objCRMActivity.investigation = Convert.ToInt32(drpinvestresult.SelectedValue);
        }
        else
        {
          objCRMActivity.investigation = 1;
        }

        objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
        objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
        objCRMActivity.Version = strUName;
        objCRMActivity.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
        objCRMActivity.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
        objCRMActivity.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
        objCRMActivity.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
        objCRMActivity.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
        objCRMActivity.Patient_Name = txtpatientname.Text;
        if (txtMRN.Text != "")
        {
          objCRMActivity.MRN = txtMRN.Text;
        }
        objCRMActivity.Subject = txtSubject.Text;
        objCRMActivity.StaffInvoice = true;
        objCRMActivity.IRDone = true;
        objCRMActivity.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
        objCRMActivity.ComplaintNumber = master.ToString();
        objCRMActivity.Contact = txtcontact.Text;
        objCRMActivity.IncidentReport = true;
        objCRMActivity.aspcomment = 2101401;
        objCRMActivity.CRUP_ID = objCRMMainActivities.CRUP_ID;
        String url2 = "insert new record in CRMActivity with " + "TenentID = " + TID + "COMPID =1 MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE =" + objCRMMainActivities.MasterCODE + "LocationID = 1" + "ActivityID = " + activityID + "Prefix = ONL ";
        String evantname2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;

        int seriea = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == objCRMMainActivities.CRUP_ID).Count() > 0 ? Convert.ToInt32(DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == objCRMMainActivities.CRUP_ID).Max(p => p.MySerial) + 1) : 1;
        string loginUserId2 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int auditno2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;
        int crup = Convert.ToInt32(objCRMMainActivities.CRUP_ID);
        Database.tblAudit objaudits = new Database.tblAudit();
        objaudits.TENANT_ID = TID;
        objaudits.CRUP_ID = crup;
        objaudits.MySerial = seriea;
        objaudits.AuditNo = auditno2;
        objaudits.AuditType = evantname2;
        objaudits.TableName = "CRMActivity";
        objaudits.NewValue = url2;
        objaudits.CreatedDate = DateTime.Now;
        objaudits.CreatedUserName = UID.ToString();
        DB.tblAudits.AddObject(objaudits);
        DB.SaveChanges();
        Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Reply", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
        Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Close", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
        Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Forward", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
        DB.CRMActivities.AddObject(objCRMActivity); //1
        DB.SaveChanges();
        clen();
        ToalCoun();
        BindRemainderNote();

        //scope.Complete();
        int TickitNo = Convert.ToInt32(objCRMMainActivities.MasterCODE);
        string display = "Your Ticket Number Is " + TickitNo;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
        //return;
        string msg = "Your Ticket Number Is " + TickitNo;
        string Function = "openModalsmall2('" + msg + "');";
        ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "openModalsmall2();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
        maxComplainID();
        //}
      }

      else if (btnSave.Text == "Update")
      {

        if (ViewState["Edit"] != null)
        {
          TimeSpan onlytime = DateTime.Now.TimeOfDay;
          string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          string Mystatus = ViewState["stats"].ToString();
          int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
          int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
          int ID = Convert.ToInt32(ViewState["Edit"]);
          Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
          objCRMMainActivities.RouteID = 1;
          objCRMMainActivities.USERCODE = UID;
          objCRMMainActivities.USERNAME = strUName;
          objCRMMainActivities.Remarks = txtMessage.Text;
          //objCRMMainActivities.Version = txtMessage.Text;
          objCRMMainActivities.MyStatus = Mystatus;
          objCRMMainActivities.UploadDate = DateTime.Now; //Convert.ToDateTime(txtdates.Text); ;
          objCRMMainActivities.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
          objCRMMainActivities.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
          objCRMMainActivities.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
          objCRMMainActivities.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
          objCRMMainActivities.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
          int Deptidd = Convert.ToInt32(drpSDepartment.SelectedValue);
          int Suppid = Convert.ToInt32(DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Deptidd).SuperVisorID);
          objCRMMainActivities.Emp_ID = Suppid;
          objCRMMainActivities.Patient_Name = txtpatientname.Text;
          if (txtMRN.Text != "")
          {
            objCRMMainActivities.MRN = txtMRN.Text;
          }
          if (chklog.Checked == true)
          {
            objCRMMainActivities.UseReciepeID = 1;
          }
          else
          {
            objCRMMainActivities.UseReciepeID = 0;
          }
          objCRMMainActivities.subject = txtSubject.Text;
          objCRMMainActivities.REMINDERNOTE = txtMessage.Text;
          objCRMMainActivities.StaffInvoice = true;
          if (drpinvestresult.SelectedValue != null && drpinvestresult.SelectedValue != "0")
          {
            objCRMMainActivities.Type = Convert.ToInt32(drpinvestresult.SelectedValue);
          }
          else
          {
            objCRMMainActivities.Type = 1;
          }
          objCRMMainActivities.IRDone = true;
          objCRMMainActivities.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
          objCRMMainActivities.ComplaintNumber = objCRMMainActivities.MasterCODE.ToString();
          objCRMMainActivities.Contact = txtcontact.Text;
          objCRMMainActivities.IncidentReport = true;
          objCRMMainActivities.FoloEmp = Convert.ToInt32(drpfoloemp.SelectedValue);
          DB.SaveChanges();

          if (Mystatus == "Closed")
          {
            List<Database.CRMActivity> listgate = DB.CRMActivities.Where(p => p.MasterCODE == ID).ToList();
            foreach (Database.CRMActivity item in listgate)
            {
              int gidd = item.ActivityID;
              int Lineno = item.MyLineNo;
              CRMActivity obj = DB.CRMActivities.Single(p => p.MasterCODE == ID && p.ActivityID == gidd && p.MyLineNo == Lineno);
              obj.MyStatus = "Closed";
              DB.SaveChanges();
            }
          }

          String url = "Update record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =1  MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = " + objCRMMainActivities.MasterCODE + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
          String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME3;
          String tablename = "CRMMainActivity";
          string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
          int crupID = Convert.ToInt32(objCRMMainActivities.CRUP_ID);

          Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);

          lblMsg.Text = "Data Update Successfully";
          pnlSuccessMsg.Visible = true;
          string notes = ViewState["remind"].ToString();
          int active = Convert.ToInt32(ViewState["Active"]);
          string rema = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID).Version;
          Database.CRMActivity objCRMActivity = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.ActivityPerform == rema);

          objCRMActivity.USERCODEENTERED = strUName;
          objCRMActivity.UPDTTIME = DateTime.Now; //Convert.ToDateTime(txtdates.Text); ;
          objCRMActivity.UploadDate = DateTime.Now; //Convert.ToDateTime(txtdates.Text);
          objCRMActivity.InitialDate = Convert.ToDateTime(txtdates.Text.Split(' ')[0] + " " + onlytime); //DateTime.Now;
          objCRMActivity.DeadLineDate = DateTime.Now;
          objCRMActivity.USERNAME = strUName;
          objCRMActivity.RouteID = "helpdesk";
          objCRMActivity.Type = 1;
          objCRMActivity.MyStatus = Mystatus;
          if (drpinvestresult.SelectedValue != "0")
          {
            objCRMActivity.investigation = Convert.ToInt32(drpinvestresult.SelectedValue);
          }
          else
          {
            objCRMActivity.investigation = 1;
          }
          objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
          objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
          objCRMActivity.Version = strUName;
          objCRMActivity.UrlRedirct = Request.Url.AbsolutePath;
          objCRMActivity.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
          objCRMActivity.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
          objCRMActivity.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
          objCRMActivity.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
          objCRMActivity.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
          objCRMActivity.Patient_Name = txtpatientname.Text;
          if (txtMRN.Text != "")
          {
            objCRMActivity.MRN = txtMRN.Text;
          }
          objCRMActivity.Subject = txtSubject.Text;
          objCRMActivity.StaffInvoice = true;
          objCRMActivity.IRDone = true;
          objCRMActivity.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
          objCRMActivity.ComplaintNumber = objCRMMainActivities.MasterCODE.ToString();
          objCRMActivity.Contact = txtcontact.Text;
          objCRMActivity.IncidentReport = true;
          objCRMActivity.REMINDERNOTE = "Pending";
          // objCRMActivity.ActivityPerform = txtMessage.Text;
          DB.SaveChanges();

          //String url1 = "Update record in CRMActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMActivity.MasterCODE + "MyLineNo = 2" + "LocationID = 1" + "LinkMasterCODE = 1 " + "ActivityID = 1" + "Prefix = ONL ";
          //String evantname2 = "Update";
          //String tablename3 = "CRMActivity";
          //string loginUserId4 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

          //Classes.GlobleClass.EncryptionHelpers.WriteLog(url1, evantname2, tablename3, loginUserId4.ToString(), 0,0);

          ToalCoun();
          // BindRemainderNote();
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID);
          ltsRemainderNotes.DataBind();
          clen();
          //scope.Complete();
          int TickitNo = Convert.ToInt32(objCRMMainActivities.MasterCODE);
          string display = "Your Ticket Number Is " + TickitNo;
          //Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
          //return;
          string msg = "Your Ticket Number Is " + TickitNo;
          string Function = "openModalsmall2('" + msg + "');";
          ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "openModalsmall2();", true);
          maxComplainID();

        }
      }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      clen();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

      if (btnSubmit.Text == "Submit")
      {
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);

        //Ratting insert

        int admin = 0;
        string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
        int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
        // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

        int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
        // int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
        if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
          admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);

        if (admin == UID)
        {
          int MsterCose = tiki;
          int TenentID = TID;
          int COMPID = 1;
          int MasterCODE = MsterCose;
          int LinkMasterCODE = MsterCose;
          int MenuID = 0;
          int ActivityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
          string ACTIVITYTYPE = "helpdesk";
          string REFTYPE = "Ticket";
          string REFSUBTYPE = "HRM";
          string PerfReferenceNo = "HRM";
          string EarlierRefNo = "A";
          int CRUP_ID = Convert.ToInt16(DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki).CRUP_ID);
          string NextUser = strUName;
          string NextRefNo = "A";
          string AMIGLOBAL = "Y";
          string MYPERSONNEL = "Y";
          string ActivityPerform = txtComent.Text;
          string REMINDERNOTE = txtComent.Text;
          int ESTCOST = 0;
          string GROUPCODE = "A";
          string USERCODEENTERED = "A";
          DateTime UPDTTIME = DateTime.Now;
          DateTime UploadDate = DateTime.Now;
          string USERNAME = strUName;
          DateTime InitialDate = DateTime.Now;
          DateTime DeadLineDate = DateTime.Now;
          string RouteID = "helpdesk";
          string Version1 = strUName + " - " + aspcomment.SelectedItem;
          int Type = 0;
          string MyStatus = drpStatus.SelectedValue;
          string GroupBy = "helpdesk";
          int DocID = 0;
          int ToColumn = 0;
          int FromColumn = 0;
          string Active = "Y";
          int MainSubRefNo = 0;
          string URL = Request.Url.AbsolutePath;
          int HelpDept = 0;
          int HelpComplain = 0;
          int HelpLocation = 0;
          int HelpCatID = 0;
          int HelpSubID = 0;
          int aspcomment1 = Convert.ToInt32(aspcomment.SelectedValue);
          int investigation = 0;
          string Ratting = "";
          if (aspcomment1 == 2101404)
          {
            investigation = Convert.ToInt32(drpinvestigation.SelectedValue);
          }
          else
          {
            if (DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").Count() > 1)
            {

            }
            else if (DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").Count() < 1)
            {
              investigation = 1;
            }
            else if (DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").Count() == 1)
            {
              int inv = Convert.ToInt32(DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").investigation);
              investigation = inv;
            }

          }
          //if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki).Count() == 1)
          //{
          //    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.MasterCODE == tiki);
          //    HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
          //    HelpComplain = Convert.ToInt32(objCRMMainActivities.TickComplainType);
          //    HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
          //    HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
          //    HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
          //    objCRMMainActivities.MyStatus = drpStatus.SelectedValue;

          //    // objCRMMainActivities.Ratting =Convert.ToInt32();
          //    if (drpStatus.SelectedValue == "Closed")
          //    {
          //        objCRMMainActivities.USERNAME = strUName;
          //    }
          //    DB.SaveChanges();




          //int crupID = Convert.ToInt16(DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == MsterCose).CRUP_ID);

          //int CRUP_ID = crupID;
          Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL, HelpDept, HelpComplain, HelpLocation, HelpCatID, HelpSubID, "", aspcomment1, investigation);

          Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
          objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
          if (drpStatus.SelectedValue == "Closed") //2
          {
            objCRMMainActivities.USERNAME = strUName;
            string rj = "";
            string rj1 = " SELECT top(1) DateDiff(dd, ExpStartDate, ExpEndDate) As days  from CRMActivities where MasterCODE = " + tiki + " order by mylineno";
            DataTable dt = DataCon.GetDataTable(rj1);
            string lc = dt.Rows[0].ItemArray[0].ToString();

            string rj2 = "SELECT top(1) DateDiff(hh, ExpStartDate, ExpEndDate) % 24 As hours  from CRMActivities where MasterCODE = " + tiki + " order by mylineno";
            DataTable dt2 = DataCon.GetDataTable(rj2);
            string lc1 = dt2.Rows[0].ItemArray[0].ToString();

            string rj3 = "  SELECT top(1) DateDiff(mi, ExpStartDate, ExpEndDate) % 60 As mins  from CRMActivities where MasterCODE = " + tiki + " order by mylineno";
            DataTable dt3 = DataCon.GetDataTable(rj3);
            string lc3 = dt3.Rows[0].ItemArray[0].ToString();

            if (lc != "0" && lc1 != "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = lc + " days" + lc1 + " hours" + lc3 + " mins";
            }
            else if (lc != "0" && lc1 != "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 == "")
            {
              rj = lc + " days" + lc1 + " hours";
            }
            else if (lc == "0" && lc1 != "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = lc1 + " hours" + lc3 + " mins";
            }
            else if (lc != "0" && lc1 == "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = lc + " days" + lc3 + " mins";
            }
            else if (lc == "0" && lc1 == "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = lc3 + " mins";
            }
            else if (lc == "0" && lc1 != "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = lc1 + " hours";
            }
            else if (lc != "0" && lc1 == "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = lc + " days";
            }
            else if (lc == "0" && lc1 == "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 != "")
            {
              rj = "0";
            }
            else
            {
              rj = "0";
            }
            if (rj == " days hours mins")
            {
              objCRMMainActivities.UseReciepeName = "0";
            }
            else
            {
              objCRMMainActivities.UseReciepeName = rj;
            }

          }
          DB.SaveChanges();


          string Status = drpStatus.SelectedValue;
          getStatusAll(Status);
          getCommunicatinData();
          ClenCat();
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == MsterCose);
          ltsRemainderNotes.DataBind();

          String url = "insert new record in CRMActivity with " + "TenentID = " + TID + "COMPID =1 MASTERCODE = " + MasterCODE + "LinkMasterCODE =" + LinkMasterCODE + "LocationID = 1" + "ActivityID = " + ActivityID + "Prefix = ONL ";
          String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;

          int seriea = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == CRUP_ID).Count() > 0 ? Convert.ToInt32(DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == CRUP_ID).Max(p => p.MySerial) + 1) : 1;
          string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
          int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;

          Database.tblAudit objaudits = new Database.tblAudit();
          objaudits.TENANT_ID = TID;
          objaudits.CRUP_ID = CRUP_ID;
          objaudits.MySerial = seriea;
          objaudits.AuditNo = auditno;
          objaudits.AuditType = evantname;
          objaudits.TableName = "CRMActivity";
          objaudits.NewValue = url;
          objaudits.CreatedDate = DateTime.Now;

          objaudits.CreatedUserName = UID.ToString();
          DB.tblAudits.AddObject(objaudits);
          DB.SaveChanges();

          List<Database.CRMProgHW> HWList = DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == tiki && p.Parameter3 == "helpdesk").ToList();
          if (HWList.Count() > 0)
          {
            foreach (Database.CRMProgHW itemhw in HWList)
            {
              Database.CRMProgHW hwobj = DB.CRMProgHWs.Single(p => p.TenentID == TID && p.ActivityID == tiki && p.RunningSerial == itemhw.RunningSerial);
              hwobj.Parameter2 = drpStatus.SelectedValue;
              DB.SaveChanges();
            }
          }
        }
        else
        {
          int MsterCose = tiki;
          int TenentID = TID;
          int COMPID = 1;
          int MasterCODE = MsterCose;
          int LinkMasterCODE = MsterCose;
          int MenuID = 0;
          int ActivityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
          string ACTIVITYTYPE = "helpdesk";
          string REFTYPE = "Ticket";
          string REFSUBTYPE = "CRM";
          string PerfReferenceNo = "CRM";
          string EarlierRefNo = "A";
          string NextUser = strUName;
          string NextRefNo = "A";
          string AMIGLOBAL = "Y";
          string MYPERSONNEL = "Y";
          string ActivityPerform = txtComent.Text;
          string REMINDERNOTE = txtComent.Text;
          int ESTCOST = 0;
          string GROUPCODE = "A";
          string USERCODEENTERED = "A";
          DateTime UPDTTIME = DateTime.Now;
          DateTime UploadDate = DateTime.Now;
          string USERNAME = strUName;

          DateTime InitialDate = DateTime.Now;
          DateTime DeadLineDate = DateTime.Now;
          string RouteID = "A";
          string Version1 = strUName + " - " + aspcomment.SelectedItem;
          int Type = 0;
          string MyStatus = drpStatus.SelectedValue;
          string GroupBy = "helpdesk";
          int DocID = 0;
          int ToColumn = 0;
          int FromColumn = 0;
          string Active = "Y";
          int MainSubRefNo = 0;
          string URL = Request.Url.AbsolutePath;
          int CRUP_ID = Convert.ToInt16(DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki).CRUP_ID);
          int HelpDept = 0;
          int HelpComplain = 0;
          int HelpLocation = 0;
          int HelpCatID = 0;
          int HelpSubID = 0;
          int investigation = 0;
          int aspcomment1 = Convert.ToInt32(aspcomment.SelectedValue);
          if (aspcomment1 == 2101404)
          {
            investigation = Convert.ToInt32(drpinvestigation.SelectedValue);
          }
          else
          {
            if (DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").Count() > 1)
            {

            }
            else if (DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").Count() < 1)
            {
              investigation = 1;
            }
            else if (DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").Count() == 1)
            {
              int inv = Convert.ToInt32(DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki && p.Subject != "").investigation);
              investigation = inv;
            }
          }

          if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki).Count() == 1)
          {
            Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
            HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
            HelpComplain = Convert.ToInt32(objCRMMainActivities.TickComplainType);
            HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
            HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
            HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);

            objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
            if (drpStatus.SelectedValue == "Closed")  //1
            {
              objCRMMainActivities.USERNAME = strUName;

              string rj = "";
              string rj1 = " SELECT top(1) DateDiff(dd, ExpStartDate, ExpEndDate) As days  from CRMActivities where MasterCODE = " + tiki + " order by mylineno";
              DataTable dt = DataCon.GetDataTable(rj1);
              string lc = dt.Rows[0].ItemArray[0].ToString();

              string rj2 = "SELECT top(1) DateDiff(hh, ExpStartDate, ExpEndDate) % 24 As hours  from CRMActivities where MasterCODE = " + tiki + " order by mylineno";
              DataTable dt2 = DataCon.GetDataTable(rj2);
              string lc1 = dt2.Rows[0].ItemArray[0].ToString();

              string rj3 = "  SELECT top(1) DateDiff(mi, ExpStartDate, ExpEndDate) % 60 As mins  from CRMActivities where MasterCODE = " + tiki + " order by mylineno";
              DataTable dt3 = DataCon.GetDataTable(rj3);
              string lc3 = dt3.Rows[0].ItemArray[0].ToString();

              if (lc != "0" && lc1 != "0" && lc3 != "0")
              {
                rj = lc + " days" + lc1 + " hours" + lc3 + " mins";
              }
              else if (lc != "0" && lc1 != "0" && lc3 == "0")
              {
                rj = lc + " days" + lc1 + " hours";
              }
              else if (lc == "0" && lc1 != "0" && lc3 != "0")
              {
                rj = lc1 + " hours" + lc3 + " mins";
              }
              else if (lc != "0" && lc1 == "0" && lc3 != "0")
              {
                rj = lc + " days" + lc3 + " mins";
              }
              else if (lc == "0" && lc1 == "0" && lc3 != "0")
              {
                rj = lc3 + " mins";
              }
              else if (lc == "0" && lc1 != "0" && lc3 == "0")
              {
                rj = lc1 + " hours";
              }
              else if (lc != "0" && lc1 == "0" && lc3 == "0")
              {
                rj = lc + " days";
              }
              else if (lc == "0" && lc1 == "0" && lc3 == "0")
              {
                rj = "0";
              }
              else
              {
                rj = "0";
              }
              if (rj == "days hours mins")
              {
                objCRMMainActivities.UseReciepeName = "0";
              }
              else
              {
                objCRMMainActivities.UseReciepeName = rj;
              }
              List<Database.CRMActivity> listgate = DB.CRMActivities.Where(p => p.MasterCODE == tiki).ToList();
              foreach (Database.CRMActivity item in listgate)
              {
                int gidd = item.ActivityID;
                int Lineno = item.MyLineNo;
                CRMActivity obj = DB.CRMActivities.Single(p => p.MasterCODE == tiki && p.ActivityID == gidd && p.MyLineNo == Lineno);
                obj.MyStatus = "Closed";
                DB.SaveChanges();
              }

            }
            DB.SaveChanges();

            String url = "insert new record in CRMActivity with " + "TenentID = " + TID + "COMPID =1 MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE =" + objCRMMainActivities.MasterCODE + "LocationID = 1" + "ActivityID = " + ActivityID + "Prefix = ONL ";
            String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;

            int seriea = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == CRUP_ID).Count() > 0 ? Convert.ToInt32(DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == CRUP_ID).Max(p => p.MySerial) + 1) : 1;
            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
            int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;

            Database.tblAudit objaudits = new Database.tblAudit();
            objaudits.TENANT_ID = TID;
            objaudits.CRUP_ID = CRUP_ID;
            objaudits.MySerial = seriea;
            objaudits.AuditNo = auditno;
            objaudits.AuditType = evantname;
            objaudits.TableName = "CRMActivity";
            objaudits.NewValue = url;
            objaudits.CreatedDate = DateTime.Now;

            objaudits.CreatedUserName = UID.ToString();
            DB.tblAudits.AddObject(objaudits);
            DB.SaveChanges();

            objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
            if (drpStatus.SelectedValue == "Closed") //2
            {
              objCRMMainActivities.USERNAME = strUName;


            }
            DB.SaveChanges();


            List<Database.CRMProgHW> HWList = DB.CRMProgHWs.Where(p => p.TenentID == TID && p.ActivityID == tiki && p.Parameter3 == "helpdesk").ToList();
            if (HWList.Count() > 0)
            {
              foreach (Database.CRMProgHW itemhw in HWList)
              {
                Database.CRMProgHW hwobj = DB.CRMProgHWs.Single(p => p.TenentID == TID && p.ActivityID == tiki && p.RunningSerial == itemhw.RunningSerial);
                hwobj.Parameter2 = drpStatus.SelectedValue;
                DB.SaveChanges();
              }
            }
          }
          //int crupID =Convert.ToInt16( DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == MsterCose).CRUP_ID);

          //int CRUP_ID = crupID;
          Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, UploadDate, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL, HelpDept, HelpComplain, HelpLocation, HelpCatID, HelpSubID, "", aspcomment1, investigation);

          string Status = drpStatus.SelectedValue;
          getStatusAll(Status);
          getCommunicatinData();
          ClenCat();

        }

        //Ratting insert

      }

      else if (btnSubmit.Text == "Update")
      {
        if (ViewState["Edit"] != null)
        {
          int ID = Convert.ToInt32(ViewState["Edit"]);
          int active = Convert.ToInt32(ViewState["Active"]);
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
          Database.CRMActivity objtbl_Employee = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.ActivityID == active);
          //string data = txtComent.Text;
          //string[] words = data.Split("Pending");
          //string paramStr = "";

          //foreach (string word in words)
          //{ 
          //    Console.WriteLine(word + objCRMMainActivities.MyStatus);
          //    paramStr += word + objCRMMainActivities.MyStatus;

          //}  
          //Before doing this this thid this this this this this  this this this  this this this this will always happen to me what the situation is this this this this this this this  

          objtbl_Employee.aspcomment = Convert.ToInt32(aspcomment.SelectedValue);
          if (txtComent.Text != null && txtComent.Text != "")
          {

            objtbl_Employee.REMINDERNOTE = txtComent.Text;
          }
          else
          {

            objtbl_Employee.REMINDERNOTE = drpStatus.SelectedValue;
          }


          objtbl_Employee.MyStatus = drpStatus.SelectedValue;
          objtbl_Employee.UploadDate = DateTime.Now;
          objtbl_Employee.InitialDate = DateTime.Now;
          objtbl_Employee.Version = strUName + " - " + aspcomment.SelectedItem;
          DB.SaveChanges();

          //Database.CRMActivity objtbl = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.ActivityID==active);

          ////  objtbl.aspcomment = Convert.ToInt32(aspcomment.SelectedValue);
          //// objtbl.ActivityPerform = txtComent.Text;
          ////objtbl.REMINDERNOTE = txtComent.Text;
          //objtbl.MyStatus = drpStatus.SelectedValue;
          //objtbl.Version = strUName + " - " + aspcomment.SelectedItem; ;
          //DB.SaveChanges();
          string remin = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID).REMINDERNOTE;
          string ap = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.ActivityID == active).ActivityPerform;
          if (remin == ap)
          {
            Database.CRMMainActivity objtblsd = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.REMINDERNOTE == remin);
            objtblsd.MyStatus = drpStatus.SelectedValue;
            DB.SaveChanges();
          }


          int dt1 = Convert.ToInt32(drpyears.SelectedValue);
          ViewState["StatusAll"] = "pending";
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID);
          ltsRemainderNotes.DataBind();
          ToalCoun();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          BindData();
          ViewState["Edit"] = null;
          btnSubmit.Text = "Submit";
          btnSubmit.ValidationGroup = "ss";
          ClenCat();
          lblMsg.Text = "Edit Successfully";
          pnlSuccessMsg.Visible = true;
        }
        else if (btnSubmit.Text == "Delete")
        {

        }
      }
    }
    protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
      if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == tiki).Count() == 1)
      {
        Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
        objCRMMainActivities.Ratting = Convert.ToInt32(e.Value);
        DB.SaveChanges();

        String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = " + objCRMMainActivities.MasterCODE + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;
        String tablename = "CRMMainActivity";
        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;
        objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno);


      }
      lblRatingStatus.Text = "5";
    }
    public void getCommunicatinData()
    {
      int admin = 0;
      int Tikitno = Convert.ToInt32(ViewState["TIkitNumber"]);
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
        admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);

      if (admin == UID)
      {
        listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
        listChet.DataBind();
        ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
        ListHistoy.DataBind();
      }
      else
      {
        List<Database.CRMActivity> CRMACTList = DB.CRMActivities.Where(p => (p.TenentID == TID || p.TenentID == 0) && p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME).ToList();
        listChet.DataSource = CRMACTList;
        listChet.DataBind();
        ListHistoy.DataSource = CRMACTList;
        ListHistoy.DataBind();

      }
    }
    public void ClenCat()
    {
      txtComent.Text = "";
      aspcomment.SelectedIndex = 0;
      drpinvestigation.SelectedIndex = 0;
    }
    protected void ltsRemainderNotes_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      int ID = Convert.ToInt32(e.CommandArgument);


      Label lblremidernotes = (Label)e.Item.FindControl("lblremidernotes");
      Label lblstatus = (Label)e.Item.FindControl("lblstatus");

      string remin = lblremidernotes != null ? lblremidernotes.Text : "N/A";  
      string statu = lblstatus != null ? lblstatus.Text : "Pending";       


      if (e.CommandName == "btnclick123")
      {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

          listChet.Visible = true;
          txtComent.Enabled = true;
          aspcomment.Enabled = true;
          drpStatus.Enabled = true;
          lblinfoDepartment.Visible = true;
          lblinfoLocation.Visible = true;
          lblinfocomplaintype.Visible = true;

          btnSubmit.Visible = true;
          LinkButton btnclick123 = (LinkButton)e.Item.FindControl("btnclick123");
          int linkID = Convert.ToInt32(e.CommandArgument);
          //Label tikitID = (Label)e.Item.FindControl("MasterCODE");
          Label Label11 = (Label)e.Item.FindControl("MyID");
          //Label Label12 = (Label)e.Item.FindControl("Label12");
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);

          //int alternetTENENT = Convert.ToInt32(Label12.Text);
          int myidd = objCRMMainActivities.MyID;
          int Tikitno = ID; //Convert.ToInt32(tikitID.Text);
          ViewState["TIkitNumber"] = Tikitno;
          panChat.Visible = true;

          int admin = 0;
          int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno);
          ltsRemainderNotes.DataBind();
          if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID && p.USERID != null && p.USERID != "").Count() > 0)
            admin = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).USERID);
          if (admin == UID)
          {
            listChet.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
            listChet.DataBind();

            ListHistoy.DataSource = DB.CRMActivities.Where(p => p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
            ListHistoy.DataBind();

            Database.CRMMainActivity infoObj = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MyID == myidd && p.ACTIVITYE == "helpdesk" && p.MasterCODE == Tikitno);
            int did = Convert.ToInt32(infoObj.TickDepartmentID);
            int LOCid = Convert.ToInt32(infoObj.TickPhysicalLocation);
            int compid = Convert.ToInt32(infoObj.TickComplainType);
            lblinfoDepartment.Text = DB.DeptITSupers.Where(p => p.TenentID == TID && p.DeptID == did).Count() == 1 ? DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == did).DeptName : "Not Found";
            lblinfoLocation.Text = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == LOCid).Count() == 1 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == LOCid).REFNAME1 : "Not Found";
            lblinfocomplaintype.Text = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "complain" && p.REFID == compid).Count() == 1 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "helpdesk" && p.REFSUBTYPE == "complain" && p.REFID == compid).REFNAME1 : "Not Found";
            pnlinfo.Visible = true;
          }
          else
          {
            List<Database.CRMActivity> CRMACTList1 = DB.CRMActivities.Where(p => (p.TenentID == TID || p.TenentID == 0) && p.GroupBy == "helpdesk" && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME).ToList();
            listChet.DataSource = CRMACTList1;
            listChet.DataBind();

            ListHistoy.DataSource = CRMACTList1;
            ListHistoy.DataBind();
            pnlinfo.Visible = false;
          }
          //Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == Tikitno);
          //  Rating1.CurrentRating = Convert.ToInt32(objCRMMainActivities.Ratting);
          lblRatingStatus.Text = objCRMMainActivities.Ratting.ToString();

        }

      }
      else if (e.CommandName == "btneditticket")
      {
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID);
        ltsRemainderNotes.DataBind();
        btncloseandupdate.Visible = true;
        btncloseandupdate.Text = "Update Tkt & Close";
        //DrpSubCat.SelectedIndex = 0;

        Database.CRMMainActivity objICCATEGORY = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
        if (objICCATEGORY.ComplaintNumber != "" && objICCATEGORY.ComplaintNumber != null)
          lblcomplainno.Text = objICCATEGORY.ComplaintNumber;
        if (objICCATEGORY.TickComplainType != 0 && objICCATEGORY.TickComplainType != null)
        {
          drpComplainType.SelectedValue = objICCATEGORY.TickComplainType.ToString();
        }
        if (objICCATEGORY.TickDepartmentID != 0 && objICCATEGORY.TickDepartmentID != null)
        {
          drpSDepartment.SelectedValue = objICCATEGORY.TickDepartmentID.ToString();
        }
        if (objICCATEGORY.TickPhysicalLocation != 0 && objICCATEGORY.TickPhysicalLocation != null)
        {
          DrpPhysicalLocation.SelectedValue = objICCATEGORY.TickPhysicalLocation.ToString();
        }
        if (objICCATEGORY.Patient_Name != "" && objICCATEGORY.Patient_Name != null)
        {
          txtpatientname.Text = objICCATEGORY.Patient_Name.ToString();
        }
        if (objICCATEGORY.MRN != "" && objICCATEGORY.MRN != null)
        {

          txtMRN.Text = objICCATEGORY.MRN.ToString();
        }
        if (objICCATEGORY.UseReciepeID == 1)
        {
          chklog.Checked = true;
        }
        if (objICCATEGORY.Contact != "" && objICCATEGORY.Contact != null)
        {
          txtcontact.Text = objICCATEGORY.Contact.ToString();
        }
        if (objICCATEGORY.TickCatID != 0 && objICCATEGORY.TickCatID != null)
        {
          DrpTCatSubCate.SelectedValue = objICCATEGORY.TickCatID.ToString();
        }
        if (objICCATEGORY.TickSubCatID != 0 && objICCATEGORY.TickSubCatID != null)
        {
          DrpSubCat.SelectedValue = objICCATEGORY.TickSubCatID.ToString();
          // drpCountryType.SelectedValue = objtblCOUNTRY.CountryType.ToString();
        }

        if (objICCATEGORY.Version != "" && objICCATEGORY.Version != null)
        {
          txtMessage.Text = objICCATEGORY.Version.ToString();
        }
        if (objICCATEGORY.Remarks != "" && objICCATEGORY.Remarks != null)
        {
          txtMessage.Text = objICCATEGORY.Remarks.ToString();
        }
        if (objICCATEGORY.ReportedBy != 0 && objICCATEGORY.ReportedBy != null)
        {
          drpusermt.SelectedValue = objICCATEGORY.ReportedBy.ToString();
        }
        if (objICCATEGORY.FoloEmp != 0 && objICCATEGORY.FoloEmp != null)
        {
          drpfoloemp.SelectedValue = objICCATEGORY.FoloEmp.ToString();
        }
        if (objICCATEGORY.subject != null && objICCATEGORY.subject != "")
        {
          txtSubject.Text = objICCATEGORY.subject;
        }
        var objact = DB.CRMActivities.FirstOrDefault(p => p.TenentID == TID && p.MasterCODE == ID);

        if (objact != null && objact.investigation != null && objact.investigation != 0)
        {
          drpinvestresult.SelectedValue = objact.investigation.ToString();
        }


        pnlTicki.Visible = true;
        //  PnlBindTick.Visible = false;
        panChat.Visible = false;
        btnSave.Text = "Update";
        ViewState["remind"] = remin;
        ViewState["Edit"] = ID;
        ViewState["stats"] = statu;
        btncack.Visible = true;
        btnCancel.Visible = false;
        int UIR = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
        if (UIR == 11525)
        {
          btndelete.Visible = true;
        }
        else
        {
          btndelete.Visible = false;
        }

      }
      else if (e.CommandName == "btnticketes")
      {
        if (statu == "Closed") //3
        {
          listChet.Visible = false;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btnticketes = (LinkButton)e.Item.FindControl("btnticketes");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }
        else
        {
          listChet.Visible = true;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btnticketes = (LinkButton)e.Item.FindControl("btnticketes");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UPDTTIME);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }

      }
      else if (e.CommandName == "btnnames")
      {

        if (statu == "Closed") //4
        {
          listChet.Visible = false;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btnnames = (LinkButton)e.Item.FindControl("btnnames");
          LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }
        else
        {
          listChet.Visible = true;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btnnames = (LinkButton)e.Item.FindControl("btnnames");
          LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
          //btnEdit.Visible = false;
        }


      }
      else if (e.CommandName == "btnremarks")
      {

        if (statu == "Closed") //5
        {
          listChet.Visible = false;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
          LinkButton btnremarks = (LinkButton)e.Item.FindControl("btnremarks");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }
        else
        {
          listChet.Visible = true;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
          LinkButton btnremarks = (LinkButton)e.Item.FindControl("btnremarks");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = 0; 
          if (tikitID != null && !string.IsNullOrWhiteSpace(tikitID.Text))
          {
            Tikitno = Convert.ToInt32(tikitID.Text);
          }
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }

        //btnEdit.Visible = false;
      }
      else if (e.CommandName == "btndates")
      {
        if (statu == "Closed") //6
        {
          listChet.Visible = false;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btndates = (LinkButton)e.Item.FindControl("btndates");
          LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }
        else
        {
          listChet.Visible = true;
          int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
          LinkButton btndates = (LinkButton)e.Item.FindControl("btndates");
          LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
          Label tikitID = (Label)e.Item.FindControl("tikitID");
          int Tikitno = Convert.ToInt32(tikitID.Text);
          ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          ListHistoy.DataBind();
          listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
          listChet.DataBind();
          ViewState["TIkitNumber"] = Tikitno;
        }

        //btnEdit.Visible = false;
      }
      else if (e.CommandName == "btnpendings")
      {

        listChet.Visible = true;
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        LinkButton btnpendings = (LinkButton)e.Item.FindControl("btnpendings");
        LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
        Label tikitID = (Label)e.Item.FindControl("tikitID");
        int Tikitno = Convert.ToInt32(tikitID.Text);
        ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
        ListHistoy.DataBind();

        listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
        listChet.DataBind();
        ViewState["TIkitNumber"] = Tikitno;
        //  btnEdit.Visible = false;
      }
      else if (e.CommandName == "btninprogress")
      {
        listChet.Visible = true;
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        LinkButton btninprogress = (LinkButton)e.Item.FindControl("btninprogress");
        LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
        Label tikitID = (Label)e.Item.FindControl("tikitID");
        int Tikitno = Convert.ToInt32(tikitID.Text);
        ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
        ListHistoy.DataBind();
        listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
        listChet.DataBind();
        ViewState["TIkitNumber"] = Tikitno;
        // btnEdit.Visible = false;
      }
      else if (e.CommandName == "btnclosed")
      {
        listChet.Visible = false;
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        LinkButton btnclosed = (LinkButton)e.Item.FindControl("btninprogress");
        LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
        Label tikitID = (Label)e.Item.FindControl("tikitID");
        int Tikitno = Convert.ToInt32(tikitID.Text);
        ListHistoy.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
        ListHistoy.DataBind();
        listChet.DataSource = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == Tikitno).OrderBy(p => p.UploadDate);
        listChet.DataBind();
        ViewState["TIkitNumber"] = Tikitno;
        //   btnEdit.Visible = false;
      }
    }
    protected void btnTikitClose_Click(object sender, EventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      string status = ViewState["StatusAll"].ToString();
      int dt = Convert.ToInt32(drpyears.SelectedValue);
      //ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == status && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
      //ltsRemainderNotes.DataBind();
      BindRemainderNote();
      PnlBindTick.Visible = true;
      pnlTicki.Visible = false;
      listChet.Visible = false;
      txtComent.Enabled = false;
      aspcomment.Enabled = false;
      drpStatus.Enabled = false;
      lblinfoDepartment.Visible = false;
      lblinfoLocation.Visible = false;
      lblinfocomplaintype.Visible = false;
      // Response.Redirect("/CRM/HelpDeskSchedule.aspx");

      //string strUName = ((USER_MST)Session["USER"]).FIRST_NAME;
      //int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      //int RoleID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_TYPE);
      //int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
      //int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
      //if (RoleID == 4)
      //{

      //    int MsterCose = tiki;
      //    int TenentID = TID;
      //    int COMPID = CID;
      //    int MasterCODE = MsterCose;
      //    int LinkMasterCODE = 1;
      //    int MenuID = 0;
      //    int ActivityID = 0;
      //    string ACTIVITYTYPE = "helpdesk";
      //    string REFTYPE = "Ticket";
      //    string REFSUBTYPE = "CRM";
      //    string PerfReferenceNo = "CRM";
      //    string EarlierRefNo = "A";
      //    string NextUser = strUName;
      //    string NextRefNo = "A";
      //    string AMIGLOBAL = "Y";
      //    string MYPERSONNEL = "Y";
      //    string ActivityPerform = txtComent.Text;
      //    string REMINDERNOTE = txtComent.Text;
      //    int ESTCOST = 0;
      //    string GROUPCODE = "A";
      //    string USERCODEENTERED = "A";
      //    DateTime UPDTTIME = DateTime.Now;
      //    string USERNAME = strUName;
      //    int CRUP_ID = 0;
      //    DateTime InitialDate = DateTime.Now;
      //    DateTime DeadLineDate = DateTime.Now;
      //    string RouteID = "A";
      //    string Version1 = "Suppot Reply";
      //    int Type = 0;
      //    string MyStatus = drpStatus.SelectedValue;
      //    string GroupBy = "helpdesk";
      //    int DocID = 0;
      //    int ToColumn = 0;
      //    int FromColumn = 0;
      //    string Active = "Y";
      //    int MainSubRefNo = 0;
      //    string URL = Request.Url.AbsolutePath;
      //    int HelpDept = 0;
      //    int HelpComplain = 0;
      //    int HelpLocation = 0;
      //    int HelpCatID = 0;
      //    int HelpSubID = 0;


      //    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
      //    HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
      //    HelpComplain = Convert.ToInt32(objCRMMainActivities.TickComplainType);
      //    HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
      //    HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
      //    HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
      //    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL,HelpDept, HelpComplain, HelpLocation, HelpCatID, HelpSubID);


      //    objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
      //    objCRMMainActivities.USERNAME = strUName;
      //    DB.SaveChanges();
      //    string Status = drpStatus.SelectedValue;
      //    getCommunicatinData();
      //    ClenCat();
      //    getStatusAll("Closed");

      //}
      //else
      //{
      //    int MsterCose = tiki;
      //    int TenentID = TID;
      //    int COMPID = CID;
      //    int MasterCODE = MsterCose;
      //    int LinkMasterCODE = 1;
      //    int MenuID = 0;
      //    int ActivityID = 0;
      //    string ACTIVITYTYPE = "helpdesk";
      //    string REFTYPE = "Ticket";
      //    string REFSUBTYPE = "CRM";
      //    string PerfReferenceNo = "CRM";
      //    string EarlierRefNo = "A";
      //    string NextUser = strUName;
      //    string NextRefNo = "A";
      //    string AMIGLOBAL = "Y";
      //    string MYPERSONNEL = "Y";
      //    string ActivityPerform = txtComent.Text;
      //    string REMINDERNOTE = txtComent.Text;
      //    int ESTCOST = 0;
      //    string GROUPCODE = "A";
      //    string USERCODEENTERED = "A";
      //    DateTime UPDTTIME = DateTime.Now;
      //    string USERNAME = strUName;
      //    int CRUP_ID = 0;
      //    DateTime InitialDate = DateTime.Now;
      //    DateTime DeadLineDate = DateTime.Now;
      //    string RouteID = "A";
      //    string Version1 = "Suppot Reply";
      //    int Type = 0;
      //    string MyStatus = drpStatus.SelectedValue;
      //    string GroupBy = "helpdesk";
      //    int DocID = 0;
      //    int ToColumn = 0;
      //    int FromColumn = 0;
      //    string Active = "Y";
      //    int MainSubRefNo = 0;
      //    string URL = Request.Url.AbsolutePath;

      //    int HelpDept = 0;
      //    int HelpComplain = 0;
      //    int HelpLocation = 0;
      //    int HelpCatID = 0;
      //    int HelpSubID = 0;


      //    Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == tiki);
      //    HelpDept = Convert.ToInt32(objCRMMainActivities.TickDepartmentID);
      //    HelpComplain = Convert.ToInt32(objCRMMainActivities.TickComplainType);
      //    HelpLocation = Convert.ToInt32(objCRMMainActivities.TickPhysicalLocation);
      //    HelpCatID = Convert.ToInt32(objCRMMainActivities.TickCatID);
      //    HelpSubID = Convert.ToInt32(objCRMMainActivities.TickSubCatID);
      //    Classes.ACMClass.InsertACM_CRMActivities(TenentID, COMPID, MasterCODE, LinkMasterCODE, MenuID, ActivityID, ACTIVITYTYPE, REFTYPE, REFSUBTYPE, PerfReferenceNo, EarlierRefNo, NextUser, NextRefNo, AMIGLOBAL, MYPERSONNEL, ActivityPerform, REMINDERNOTE, ESTCOST, GROUPCODE, USERCODEENTERED, UPDTTIME, USERNAME, CRUP_ID, InitialDate, DeadLineDate, RouteID, Version1, Type, MyStatus, GroupBy, DocID, ToColumn, FromColumn, Active, MainSubRefNo, URL, HelpDept, HelpComplain, HelpLocation, HelpCatID, HelpSubID);

      //    objCRMMainActivities.MyStatus = drpStatus.SelectedValue;
      //    objCRMMainActivities.USERNAME = strUName;
      //    DB.SaveChanges();
      //    string Status = drpStatus.SelectedValue;
      //    getCommunicatinData();
      //    ClenCat();
      //    getStatusAll("Closed");
      //}

    }
    public string GetUName(int UID)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == UID).Count() > 0)
      {
        string UName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
        return UName;
      }
      else
      {
        return "Not Found";
      }

    }
    public string GetEmplName(int UID)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == UID).Count() > 0)
      {
        string UName = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == UID).firstname;
        return UName;
      }
      else
      {
        return "Not Found";
      }

    }
    //public string getstatus(int UID)
    //{
    //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
    //    if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "pending"))
    //    {
    //        string UName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
    //        return UName;
    //    }
    //    else
    //    {
    //        return "Not Found";
    //    }
    //}
    protected void lblattachments_Click(object sender, EventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      string UID = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
      if (ViewState["TIkitNumber"] != null)
      {
        int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
        string Type = "Ticket";
        string Url = "../CRM/AttachmentMst.aspx?AttachID=" + tiki + "&DID=" + Type + "&RefNo=" + 11058 + "&UID=" + uid1;//+ "&recodInsert=" + tiki;
        string s = "window.open('" + Url + "', 'popup_window', 'width=950,height=225,left=100,top=100,resizable=yes');";
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", s, true);
      }

      //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

      //string URL = "AttachmentMst.aspx?AttachID=" + Contectid + "&DID=Contact&Mode=" + Mode + "&recodInsert=" + CompnyID;
      //Response.Redirect(URL);
    }
    protected void btnAttch_Click(object sender, EventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      if (ViewState["TIkitNumber"] != null)
      {
        int tiki = Convert.ToInt32(ViewState["TIkitNumber"]);
        List<Database.tbl_DMSAttachmentMst> ttachOBJ = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.CATID == uid1 && p.AttachmentById == tiki).ToList();
        int Coun = ttachOBJ.Count();
        btnAttch.Text = Coun.ToString();
      }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
      getCommunicatinData();
      UpdatePanel1.Update();
    }
    protected void chklist_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (chklist.Checked)
      {
        txtstaffname.Visible = true;
      }
      else
      {
        txtstaffname.Visible = false;
      }

    }
    protected void drpSDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (drpSDepartment.SelectedValue == "99999")
      {
        txtMessage.Text = drpSDepartment.SelectedItem + " = ";
      }
      else
      {
        txtMessage.Text = "";
      }

    }
    protected void aspcomment_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (aspcomment.SelectedValue == "2101404")
      {
        drpinvestigation.Visible = true;
        Label20.Visible = true;
        Label20.Text = "Risk";
        Rating1.Visible = true;
      }
      else
      {
        drpinvestigation.Visible = false;
        Label20.Visible = false;
        Rating1.Visible = false;
      }

    }
    protected void chklist_CheckedChanged(object sender, EventArgs e)
    {
      if (chklist.Checked == true)
      {
        txtstaffname.Visible = true;
      }
      else
      {
        txtstaffname.Visible = false;
      }
    }
    protected void drpinvestigation_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void DrpTCatSubCate_SelectedIndexChanged(object sender, EventArgs e)
    {
      int CID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      List<ICCATSUBCAT> list = DB.ICCATSUBCATs.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.CATID == CID).ToList();
      List<ICSUBCATEGORY> tempList = new List<ICSUBCATEGORY>();
      foreach (ICCATSUBCAT item in list)
      {
        var obj = DB.ICSUBCATEGORies.FirstOrDefault(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == item.SUBCATID && p.Status == 1);
        tempList.Add(obj);

      }
      DrpSubCat.DataSource = tempList;
      DrpSubCat.DataTextField = "SUBCATNAME";
      DrpSubCat.DataValueField = "SUBCATID";
      DrpSubCat.DataBind();
      if (list.Count <= 0)
        DrpSubCat.Items.Insert(0, new ListItem("-- Select --", "0"));


    }
    protected void btnsub2_Click(object sender, EventArgs e)
    {

    }
    protected void btnsub3_Click(object sender, EventArgs e)
    {

    }
    protected void btnsub4_Click(object sender, EventArgs e)
    {

    }
    protected void btnsub5_Click(object sender, EventArgs e)
    {

    }
    protected void btn2015_Click(object sender, EventArgs e)
    {
      //DateTime s = Convert.ToDateTime("2015-01-01 11:28:41.077");
      //DateTime e = Convert.ToDateTime("2015-12-01 11:28:41.077");
      //getStatusAll();
    }
    protected void listChet_DataBound(object sender, EventArgs e)
    {

    }
    protected void listChet_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName == "btnEdit")
      {

        Label lblcompid = (Label)e.Item.FindControl("lblcompid");
        Label lblmastercode = (Label)e.Item.FindControl("lblmastercode");
        Label lblMyLineNo = (Label)e.Item.FindControl("lblMyLineNo");
        Label lblLocationID = (Label)e.Item.FindControl("lblLocationID");
        Label lblLinkMasterCODE = (Label)e.Item.FindControl("lblLinkMasterCODE");
        Label lblActivityID = (Label)e.Item.FindControl("lblActivityID");
        Label lblPrefix = (Label)e.Item.FindControl("lblPrefix");
        int ID = Convert.ToInt32(e.CommandArgument);
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        int ActivityID = Convert.ToInt32(lblActivityID.Text);
        int ComId = Convert.ToInt32(lblcompid.Text);
        int mylineno = Convert.ToInt32(lblMyLineNo.Text);
        int location = Convert.ToInt32(lblLocationID.Text);
        int linkmaster = Convert.ToInt32(lblLinkMasterCODE.Text);
        string prefix = lblPrefix.Text;
        Database.CRMActivity objREFTABLE = DB.CRMActivities.Single(p => p.TenentID == TID && p.COMPID == ComId && p.MasterCODE == ID && p.LocationID == location && p.LinkMasterCODE == linkmaster && p.ActivityID == ActivityID && p.Prefix == prefix);
        if (objREFTABLE.aspcomment != 0 && objREFTABLE.aspcomment != null)
        {
          aspcomment.SelectedValue = objREFTABLE.aspcomment.ToString();
        }
        txtComent.Text = objREFTABLE.REMINDERNOTE.ToString();
        // txtComent.Text = objREFTABLE.REMINDERNOTE.ToString();
        if (drpStatus.SelectedValue != null && drpStatus.SelectedValue != "0")
        {
          drpStatus.SelectedValue = objREFTABLE.MyStatus.ToString();
        }
        btnSubmit.Text = "Update";
        ViewState["Edit"] = ID;
        ViewState["Active"] = ActivityID;
        ViewState["Edit"] = ID;
      }
    }
    protected void ltsRemainderNotes_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
      string status = ViewState["StatusAll"].ToString();
      if (e.Item.ItemType == ListViewItemType.DataItem && status == "Closed")
      {
        LinkButton btnclick123 = (LinkButton)e.Item.FindControl("btnclick123");
        LinkButton btneditticket = (LinkButton)e.Item.FindControl("btneditticket");
        btnclick123.Visible = false;
        btneditticket.Visible = false;
        aspcomment.Visible = false;
        txtComent.Visible = false;
        drpStatus.Visible = false;
        Label6.Visible = false;
        btnAttch.Visible = false;
        btnSubmit.Visible = false;
        btnTikitClose.Visible = false;
        lbllblattachments2.Visible = false;
      }
      else
      {
        LinkButton btnclick123 = (LinkButton)e.Item.FindControl("btnclick123");
        LinkButton btneditticket = (LinkButton)e.Item.FindControl("btneditticket");
        btnclick123.Visible = true;
        btneditticket.Visible = true;
        aspcomment.Visible = true;
        txtComent.Visible = true;
        drpStatus.Visible = true;
        Label6.Visible = true;
        btnAttch.Visible = true;
        btnSubmit.Visible = true;
        btnTikitClose.Visible = true;
        lbllblattachments2.Visible = true;
      }
    }
    protected void ltsRemainderNotes_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
      (ltsRemainderNotes.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
      this.BindRemainderNote();
    }
    protected void linkreopen_Click(object sender, EventArgs e)
    {
      lblticket.Visible = true;
      txtticket.Visible = true;
      lblcomplain.Visible = true;
      txtcomplain.Visible = true;
      btnsearch.Visible = true;
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      if (txtticket.Text != null && txtticket.Text != "" && txtcomplain.Text != null && txtcomplain.Text != "")
      {

        int mastercode = Convert.ToInt32(txtticket.Text);
        string complain = txtcomplain.Text;
        if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == mastercode && p.ComplaintNumber == complain).Count() > 0)
        {
          int dt2 = Convert.ToInt32(drpyears.SelectedValue);
          ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == "Closed" && p.MasterCODE == mastercode && p.ComplaintNumber == complain && p.UploadDate.Value.Year == dt2).OrderByDescending(p => p.UploadDate);
          ltsRemainderNotes.DataBind();
          if (DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == mastercode && p.ComplaintNumber == complain).Count() > 0)
          {
            int ID = mastercode;
            string reminder = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID).REMINDERNOTE;
            string status = "InProgress";
            Database.CRMMainActivity objcrm = DB.CRMMainActivities.Single(p => p.MasterCODE == ID);
            objcrm.MyStatus = status;
            DB.SaveChanges();
            //Database.CRMActivity objlvm = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.REMINDERNOTE == reminder);
            //objlvm.MyStatus = status;
            //DB.SaveChanges();
            ToalCoun();
            //String url = "Update record in CRMMainActivity with " + "TenentID = " + TID + "MASTERCODE = " + ID + "LinkMasterCODE = 1" + "LocationID = 1";
            //String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME3;
            //String tablename = "CRMMainActivity";
            //string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
            //int crupID = Convert.ToInt32(objcrm.CRUP_ID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);
            //Classes.GlobleClass.E
            //Classes.GlobleClass.EncryptionHelpers.Write 4    rereLog(url, evantname,    tablename, loginUserId.ToString(), 0,0);

          }
        }
        else
        {
          ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No Record Found');", true);
        }


      }
    }
    protected void btncack_Click(object sender, EventArgs e)
    {

      BindRemainderNote();
      PnlBindTick.Visible = true;
      pnlTicki.Visible = false;
      listChet.Visible = false;
      txtComent.Enabled = false;
      aspcomment.Enabled = false;
      drpStatus.Enabled = false;
      lblinfoDepartment.Visible = false;
      lblinfoLocation.Visible = false;
      lblinfocomplaintype.Visible = false;

    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
      if (ViewState["Edit"] != null)
      {
        int ID = Convert.ToInt32(ViewState["Edit"]);
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        Database.CRMMainActivity objSOJobDesc = DB.CRMMainActivities.Single(p => p.MasterCODE == ID);
        DB.CRMMainActivities.DeleteObject(objSOJobDesc);
        DB.SaveChanges();
        List<Database.CRMActivity> GList = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID).ToList();
        foreach (Database.CRMActivity item in GList)
        {
          DB.CRMActivities.DeleteObject(item);
          DB.SaveChanges();

        }



        //Database.CRMActivity objai = DB.CRMActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID).FirstOrDefault();
        //DB.CRMActivities.DeleteObject(objai);
        //DB.SaveChanges();
        String url = "delete CRMMainActivity with " + "TenentID = " + TID + "MasterCode =" + ID;
        String evantname = "delete";
        String tablename = "ICSUBCATEGORY";
        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;
        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno);

        lblcomplainno.Text = "";
        drpComplainType.SelectedIndex = 0;
        drpSDepartment.SelectedIndex = 0;
        chklog.Checked = false;
        DrpPhysicalLocation.SelectedIndex = 0;
        txtpatientname.Text = "";
        txtMRN.Text = "";
        chklog.Checked = false;
        txtcontact.Text = "";
        DrpTCatSubCate.SelectedIndex = 0;
        DrpSubCat.SelectedIndex = 0;
        txtSubject.Text = "";
        txtMessage.Text = "";
        drpusermt.SelectedIndex = 0;
        lblMsg.Text = "Deleted Succeessfully";
        pnlSuccessMsg.Visible = true;
        BindData();
        ToalCoun();
      }
    }
    protected void drpyears_SelectedIndexChanged(object sender, EventArgs e)
    {
      ToalCoun();
      BindRemainderNote();
    }
    protected void btndash_Click(object sender, EventArgs e)
    {
      Response.Redirect("/Sales/POSIndex.aspx");
    }
    protected void txtdates_TextChanged(object sender, EventArgs e)
    {
      DateTime dt = DateTime.Now;
      DateTime dl = DateTime.Now.AddDays(-7);
      DateTime dc = DateTime.Now.AddDays(-6);
      DateTime dd = DateTime.Now.AddDays(-5);
      DateTime da = DateTime.Now.AddDays(-4);
      DateTime ds = DateTime.Now.AddDays(-3);
      DateTime dx = DateTime.Now.AddDays(-2);
      DateTime df = DateTime.Now.AddDays(-1);


      string dt1 = dt.ToString("dd/MMM/yyyy");
      string dt2 = dl.ToString("dd/MMM/yyyy");
      string dt3 = dc.ToString("dd/MMM/yyyy");
      string dt4 = dd.ToString("dd/MMM/yyyy");
      string dt5 = da.ToString("dd/MMM/yyyy");
      string dt6 = ds.ToString("dd/MMM/yyyy");
      string dt7 = dx.ToString("dd/MMM/yyyy");
      string dt8 = df.ToString("dd/MMM/yyyy");

      if (txtdates.Text == dt1)
      {
        txtdates.Text = dt1;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt2)
      {
        txtdates.Text = dt2;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt3)
      {
        txtdates.Text = dt3;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt4)
      {
        txtdates.Text = dt4;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt5)
      {
        txtdates.Text = dt5;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt6)
      {
        txtdates.Text = dt6;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt7)
      {
        txtdates.Text = dt7;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt8)
      {
        txtdates.Text = dt8;
        lblmsgl.Visible = false;
      }
      else
      {
        lblmsgl.Visible = true;
        lblmsgl.Text = "Select date between today to Last 7 day!";
      }

      //List<datetime> dateList = new List<datetime>();
      //while (StartDate.AddDays(DayInterval) <= EndDate)
      //{
      //    StartDate = StartDate.AddDays(DayInterval);
      //    dateList.Add(StartDate);
      //    if (StartDate == d1)
      //    {
      //        Response.Write(d1.ToString() + " Exist");
      //    }
      //}   

      //RangeValidator1.ControlToValidate = txtdates.Text;
      //RangeValidator1.Type = ValidationDataType.Date;
      //RangeValidator1.MinimumValue = DateTime.Now.ToShortDateString();
      //RangeValidator1.MaximumValue = DateTime.Now.AddDays(7).ToShortDateString();
      //RangeValidator1.ErrorMessage = "Select date between today to next 7 day!";
    }

    protected void txtdates_TextChanged1(object sender, EventArgs e)
    {
      DateTime dt = DateTime.Now;
      DateTime dl = DateTime.Now.AddDays(-7);
      DateTime dc = DateTime.Now.AddDays(-6);
      DateTime dd = DateTime.Now.AddDays(-5);
      DateTime da = DateTime.Now.AddDays(-4);
      DateTime ds = DateTime.Now.AddDays(-3);
      DateTime dx = DateTime.Now.AddDays(-2);
      DateTime df = DateTime.Now.AddDays(-1);


      string dt1 = dt.ToString("dd/MMM/yyyy");
      string dt2 = dl.ToString("dd/MMM/yyyy");
      string dt3 = dc.ToString("dd/MMM/yyyy");
      string dt4 = dd.ToString("dd/MMM/yyyy");
      string dt5 = da.ToString("dd/MMM/yyyy");
      string dt6 = ds.ToString("dd/MMM/yyyy");
      string dt7 = dx.ToString("dd/MMM/yyyy");
      string dt8 = df.ToString("dd/MMM/yyyy");

      if (txtdates.Text == dt1)
      {
        txtdates.Text = dt1;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt2)
      {
        txtdates.Text = dt2;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt3)
      {
        txtdates.Text = dt3;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt4)
      {
        txtdates.Text = dt4;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt5)
      {
        txtdates.Text = dt5;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt6)
      {
        txtdates.Text = dt6;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt7)
      {
        txtdates.Text = dt7;
        lblmsgl.Visible = false;
      }
      else if (txtdates.Text == dt8)
      {
        txtdates.Text = dt8;
        lblmsgl.Visible = false;
      }
      else
      {
        lblmsgl.Visible = true;
        lblmsgl.Text = "Select date between today to Last 7 day!";
      }
    }

    protected void lblLocalGroup_Click(object sender, EventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      string status = ViewState["StatusAll"].ToString();
      int dt = Convert.ToInt32(drpyears.SelectedValue);
      //ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MyStatus == status && p.ACTIVITYE == "helpdesk" && p.UploadDate.Value.Year == dt).OrderByDescending(p => p.UploadDate);
      //ltsRemainderNotes.DataBind();
      BindRemainderNote();
      PnlBindTick.Visible = true;
      pnlTicki.Visible = false;
      listChet.Visible = false;
      txtComent.Enabled = false;
      aspcomment.Enabled = false;
      drpStatus.Enabled = false;
      lblinfoDepartment.Visible = false;
      lblinfoLocation.Visible = false;
      lblinfocomplaintype.Visible = false;
    }

    protected void drpusers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnoksearch_Click(object sender, EventArgs e)
    {
      btncount.Visible = true;
      ToalCoun();
      BindRemainderNote();
      ToalSearCoun();

    }


    public void ToalSearCoun()
    {
      int dt = Convert.ToInt32(drpyears.SelectedValue);
      int rb = Convert.ToInt32(drpusers.SelectedValue);
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      string patientname = txtpatientnames.Text;

      string comno = txtcnose.Text;
      //&& p.Year(UploadDate) == '2019'
      string paramStr = "";
      if (drpusers.SelectedValue != "0" && drpusers.SelectedValue != null)
      {
        paramStr += "and ReportedBy = '" + drpusers.SelectedValue + "'  ";
      }
      if (drpmonths.SelectedValue != "0" && drpmonths.SelectedValue != null)
      {
        paramStr += "and Month(UploadDate) = '" + drpmonths.SelectedValue + "'  ";
      }
      string SQOCommads = " select MasterCODE as 'MasterCODE' , Remarks as Remarks , USERCODE as 'USERCODE' , MyID as 'MyID' , ACTIVITYE as 'ACTIVITYE' , Patient_Name as 'Patient_Name', MRN as 'MRN', " +
                         " TenentID as 'TenentID' , REMINDERNOTE as 'REMINDERNOTE' , MyStatus as 'MyStatus' , UploadDate  as 'UploadDate', " +
                         " ReportedBy as 'ReportedBy' ,  ComplaintNumber as 'ComplaintNumber'  , UseReciepeName as 'UseReciepeName' , UseReciepeID as 'UseReciepeID' from CRMMainActivities " +
                         " where TenentID = " + TID + " and year(UploadDate) = '" + dt + "' and ACTIVITYE = 'helpdesk'  and Patient_Name like '" + txtpatientnames.Text + "%' and ComplaintNumber like '" + txtcnose.Text + "%' " + paramStr + " ORDER BY  UploadDate desc";

      SqlCommand CMD2 = new SqlCommand(SQOCommads, con);
      SqlDataAdapter ADB1 = new SqlDataAdapter(CMD2);
      DataSet ds1 = new DataSet();
      ADB1.Fill(ds1);
      DataTable dt1 = ds1.Tables[0];
      int count = 0;
      for (int i = 0; i < dt1.Rows.Count; i++)
      {
        count++;
      }

      int Pending = count;
      btncount.Text = Pending.ToString() + " Found";
    }

    protected void btncloseandupdate_Click(object sender, EventArgs e)
    {
      if (ViewState["Edit"] != null)
      {
        TimeSpan onlytime = DateTime.Now.TimeOfDay;
        string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        string Mystatus = ViewState["stats"].ToString();
        int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
        int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
        int ID = Convert.ToInt32(ViewState["Edit"]);
        Database.CRMMainActivity objCRMMainActivities = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
        objCRMMainActivities.RouteID = 1;
        objCRMMainActivities.USERCODE = UID;
        objCRMMainActivities.USERNAME = strUName;
        objCRMMainActivities.Remarks = txtMessage.Text;
        //objCRMMainActivities.Version = txtMessage.Text;
        objCRMMainActivities.MyStatus = "Closed";
        objCRMMainActivities.UploadDate = DateTime.Now; //Convert.ToDateTime(txtdates.Text); ;
        objCRMMainActivities.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
        objCRMMainActivities.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
        objCRMMainActivities.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
        objCRMMainActivities.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
        objCRMMainActivities.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
        int Deptidd = Convert.ToInt32(drpSDepartment.SelectedValue);
        int Suppid = Convert.ToInt32(DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Deptidd).SuperVisorID);
        objCRMMainActivities.Emp_ID = Suppid;
        objCRMMainActivities.Patient_Name = txtpatientname.Text;
        if (txtMRN.Text != "")
        {
          objCRMMainActivities.MRN = txtMRN.Text;
        }
        if (chklog.Checked == true)
        {
          objCRMMainActivities.UseReciepeID = 1;
        }
        else
        {
          objCRMMainActivities.UseReciepeID = 0;
        }

        objCRMMainActivities.REMINDERNOTE = txtMessage.Text;
        objCRMMainActivities.StaffInvoice = true;
        objCRMMainActivities.IRDone = true;
        objCRMMainActivities.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
        objCRMMainActivities.ComplaintNumber = objCRMMainActivities.MasterCODE.ToString();
        objCRMMainActivities.Contact = txtcontact.Text;
        objCRMMainActivities.IncidentReport = true;
        objCRMMainActivities.FoloEmp = Convert.ToInt32(drpfoloemp.SelectedValue);
        objCRMMainActivities.subject = txtSubject.Text;
        DB.SaveChanges();

        List<Database.CRMActivity> listgate = DB.CRMActivities.Where(p => p.MasterCODE == ID).ToList();
        foreach (Database.CRMActivity item in listgate)
        {
          int gidd = item.ActivityID;
          int Lineno = item.MyLineNo;
          CRMActivity obj = DB.CRMActivities.Single(p => p.MasterCODE == ID && p.ActivityID == gidd && p.MyLineNo == Lineno);
          obj.MyStatus = "Closed";
          DB.SaveChanges();
        }

        String url = "Update record in CRMMainActivity with " + "TenentID = " + TID + "COMPID =1  MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = " + objCRMMainActivities.MasterCODE + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME3;
        String tablename = "CRMMainActivity";
        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int crupID = Convert.ToInt32(objCRMMainActivities.CRUP_ID);

        Classes.GlobleClass.EncryptionHelpers.ModifyLog(url, evantname, tablename, loginUserId.ToString(), 0, crupID);

        lblMsg.Text = "Data Update Successfully";
        pnlSuccessMsg.Visible = true;
        string notes = ViewState["remind"].ToString();
        int active = Convert.ToInt32(ViewState["Active"]);
        string rema = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID).Version;
        Database.CRMActivity objCRMActivity = DB.CRMActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID && p.ActivityPerform == rema && p.MyLineNo == 10);

        objCRMActivity.USERCODEENTERED = strUName;
        objCRMActivity.UPDTTIME = DateTime.Now; //Convert.ToDateTime(txtdates.Text); ;
        objCRMActivity.UploadDate = DateTime.Now; //Convert.ToDateTime(txtdates.Text);
        objCRMActivity.InitialDate = Convert.ToDateTime(txtdates.Text.Split(' ')[0] + " " + onlytime); //DateTime.Now;
        objCRMActivity.DeadLineDate = DateTime.Now;
        objCRMActivity.USERNAME = strUName;
        objCRMActivity.RouteID = "helpdesk";
        objCRMActivity.Type = 1;
        objCRMActivity.MyStatus = "Closed";

        objCRMActivity.ExpEndDate = DateTime.Now;

        objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
        objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
        objCRMActivity.Version = strUName;
        objCRMActivity.UrlRedirct = Request.Url.AbsolutePath;
        objCRMActivity.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
        objCRMActivity.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
        objCRMActivity.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
        objCRMActivity.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
        objCRMActivity.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
        objCRMActivity.Patient_Name = txtpatientname.Text;
        if (txtMRN.Text != "")
        {
          objCRMActivity.MRN = txtMRN.Text;
        }
        objCRMActivity.StaffInvoice = true;
        objCRMActivity.IRDone = true;
        objCRMActivity.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
        objCRMActivity.ComplaintNumber = objCRMMainActivities.MasterCODE.ToString();
        objCRMActivity.Contact = txtcontact.Text;
        objCRMActivity.IncidentReport = true;
        objCRMActivity.REMINDERNOTE = "Pending";
        objCRMActivity.Subject = txtSubject.Text;
        // objCRMActivity.ActivityPerform = txtMessage.Text;
        DB.SaveChanges();





        //String url1 = "Update record in CRMActivity with " + "TenentID = " + TID + "COMPID =" + CID + "MASTERCODE = " + objCRMActivity.MasterCODE + "MyLineNo = 2" + "LocationID = 1" + "LinkMasterCODE = 1 " + "ActivityID = 1" + "Prefix = ONL ";
        //String evantname2 = "Update";
        //String tablename3 = "CRMActivity";
        //string loginUserId4 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

        //Classes.GlobleClass.EncryptionHelpers.WriteLog(url1, evantname2, tablename3, loginUserId4.ToString(), 0,0);


        ToalCoun();
        // BindRemainderNote();
        ltsRemainderNotes.DataSource = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MasterCODE == ID);
        ltsRemainderNotes.DataBind();
        clen();

        string rj = "";

        string rj1 = " SELECT top(1) DateDiff(dd, ExpStartDate, ExpEndDate) As days  from CRMActivities where MasterCODE = " + ID + " order by mylineno";
        DataTable dt = DataCon.GetDataTable(rj1);
        string lc = dt.Rows[0].ItemArray[0].ToString();

        string rj2 = "SELECT top(1) DateDiff(hh, ExpStartDate, ExpEndDate) % 24 As hours  from CRMActivities where MasterCODE = " + ID + " order by mylineno";
        DataTable dt2 = DataCon.GetDataTable(rj2);
        string lc1 = dt2.Rows[0].ItemArray[0].ToString();

        string rj3 = "  SELECT top(1) DateDiff(mi, ExpStartDate, ExpEndDate) % 60 As mins  from CRMActivities where MasterCODE = " + ID + " order by mylineno";
        DataTable dt3 = DataCon.GetDataTable(rj3);
        string lc3 = dt3.Rows[0].ItemArray[0].ToString();

        if (lc != "0" && lc1 != "0" && lc3 != "0")
        {
          rj = lc + " days" + lc1 + " hours" + lc3 + " mins";
        }
        else if (lc != "0" && lc1 != "0" && lc3 == "0")
        {
          rj = lc + " days" + lc1 + " hours";
        }
        else if (lc == "0" && lc1 != "0" && lc3 != "0")
        {
          rj = lc1 + " hours" + lc3 + " mins";
        }
        else if (lc != "0" && lc1 == "0" && lc3 != "0")
        {
          rj = lc + " days" + lc3 + " mins";
        }
        else if (lc == "0" && lc1 == "0" && lc3 != "0")
        {
          rj = lc3 + " mins";
        }
        else if (lc == "0" && lc1 != "0" && lc3 == "0")
        {
          rj = lc1 + " hours";
        }
        else if (lc != "0" && lc1 == "0" && lc3 == "0")
        {
          rj = lc + " days";
        }
        else if (lc == "0" && lc1 == "0" && lc3 == "0")
        {
          rj = "0";
        }
        else
        {
          rj = "0";
        }


        Database.CRMMainActivity objSOJobDesc = DB.CRMMainActivities.Single(p => p.MasterCODE == ID);
        objSOJobDesc.MyStatus = "Closed";
        objSOJobDesc.UseReciepeName = rj;
        DB.SaveChanges();
        lblMsg.Text = "ticket Update with Closed Succeessfully";
        pnlSuccessMsg.Visible = true;
        BindData();
        ToalCoun();
        btncloseandupdate.Enabled = false;
        //scope.Complete();
        int TickitNo = Convert.ToInt32(objCRMMainActivities.MasterCODE);
        string display = "Your Ticket Number Is " + TickitNo;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
        //return;
        string msg = "Your Ticket Number Is " + TickitNo;
        string Function = "openModalsmall2('" + msg + "');";
        ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "openModalsmall2();", true);
        maxComplainID();
      }
      else if (ViewState["ComplaintNumber"] != null)
      {
        string strUName = ((USER_MST)Session["USER"]).LOGIN_ID;
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
        int CID = Convert.ToInt32(((USER_MST)Session["USER"]).CompId);
        Database.CRMMainActivity objCRMMainActivities = new Database.CRMMainActivity();
        // objtbl_DMSTikit_Mst.ID = DB.tbl_DMSTikit_Mst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSTikit_Mst.Max(p => p.ID) + 1) : 1;
        objCRMMainActivities.TenentID = TID;
        objCRMMainActivities.COMPID = 1;
        objCRMMainActivities.Prefix = "ONL";
        string maxid = "select ISNull(max(MyID),0)+1 AS NEWComplaintNumber from CRMMainActivities where tenentid=" + TID + " and year(uploaddate)=year(GETDATE()) and month(uploaddate)=month(GETDATE())";
        DataTable dt = DataCon.GetDataTable(maxid);
        DateTime startdate = DateTime.Now;
        string stdate = startdate.ToString("yyyyMM");
        string num = dt.Rows[0].ItemArray[0].ToString();
        TimeSpan onlytime = DateTime.Now.TimeOfDay;
        int master = Convert.ToInt32(stdate + num);

        objCRMMainActivities.MasterCODE = master;
        objCRMMainActivities.LinkMasterCODE = master;
        objCRMMainActivities.LocationID = 1;
        objCRMMainActivities.MyID = Convert.ToInt32(num);
        objCRMMainActivities.RouteID = 1;
        objCRMMainActivities.USERCODE = UID;
        objCRMMainActivities.ACTIVITYA = "Reference";
        objCRMMainActivities.ACTIVITYA2 = "TKT";
        objCRMMainActivities.ACTIVITYE = "helpdesk";
        objCRMMainActivities.Reference = "helpdesk";
        objCRMMainActivities.AMIGLOBAL = true;
        objCRMMainActivities.MYPERSONNEL = false;
        objCRMMainActivities.INTERVALDAYS = 1;
        objCRMMainActivities.REPEATFOREVER = false;
        objCRMMainActivities.REPEATTILL = DateTime.Now;
        objCRMMainActivities.ESTCOST = DB.CRMMainActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Where(p => p.TenentID == TID).Max(p => p.ESTCOST) + 1) : 1;
        objCRMMainActivities.USERCODEENTERED = txtpatientname.Text;
        objCRMMainActivities.UPDTTIME = DateTime.Now; ///Convert.ToDateTime(txtdates.Text);///Convert.ToDateTime(txtdates.Text.Split(' ')[0]+" " + onlytime);
        objCRMMainActivities.USERNAME = strUName;
        objCRMMainActivities.Remarks = txtMessage.Text;
        objCRMMainActivities.Version = txtMessage.Text;
        objCRMMainActivities.Type = 1;
        objCRMMainActivities.MyStatus = "Pending";
        objCRMMainActivities.UploadDate = DateTime.Now;//Convert.ToDateTime(txtdates.Text);                
        objCRMMainActivities.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
        objCRMMainActivities.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
        objCRMMainActivities.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
        objCRMMainActivities.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
        objCRMMainActivities.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
        int Deptidd = Convert.ToInt32(drpSDepartment.SelectedValue);
        int Suppid = Convert.ToInt32(DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Deptidd).SuperVisorID);
        objCRMMainActivities.Emp_ID = Suppid;
        string ITGroupUser = DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == Suppid).Count() == 1 ? DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == Suppid).userID : "0";
        int ITGroupID = DB.TBLGROUPs.Where(p => p.TenentID == TID && p.USERCODE == ITGroupUser).Count() == 1 ? Convert.ToInt32(DB.TBLGROUPs.Single(p => p.TenentID == TID && p.USERCODE == ITGroupUser).ITGROUPID) : 0;
        objCRMMainActivities.GROUPCODE = ITGroupID;
        objCRMMainActivities.REMINDERNOTE = txtMessage.Text;
        objCRMMainActivities.Description = txtMessage.Text;
        objCRMMainActivities.Active = true;
        objCRMMainActivities.Patient_Name = txtpatientname.Text;
        objCRMMainActivities.FoloEmp = Convert.ToInt32(drpfoloemp.SelectedValue);
        if (txtMRN.Text != "")
        {
          objCRMMainActivities.MRN = txtMRN.Text;
        }
        if (chklog.Checked == true)
        {
          objCRMMainActivities.UseReciepeID = 1;
        }
        else
        {
          objCRMMainActivities.UseReciepeID = 0;
        }
        objCRMMainActivities.StaffInvoice = true;
        objCRMMainActivities.IRDone = true;
        objCRMMainActivities.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
        objCRMMainActivities.ComplaintNumber = master.ToString();
        objCRMMainActivities.Contact = txtcontact.Text;
        objCRMMainActivities.IncidentReport = true;
        objCRMMainActivities.subject = txtSubject.Text;
        String url = "insert new record in CRMMainActivity with " + "TenentID = " + TID + "COMPID = 1  MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE = " + objCRMMainActivities.MasterCODE + "LocationID = 1" + "MyID = " + objCRMMainActivities.MyID + "Prefix = ONL ";
        String evantname = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;
        String tablename = "CRMMainActivity";
        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int auditno = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;
        objCRMMainActivities.CRUP_ID = Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, auditno); ;
        DB.CRMMainActivities.AddObject(objCRMMainActivities);
        DB.SaveChanges();
        Database.CRMActivity objCRMActivity = new Database.CRMActivity();

        objCRMActivity.TenentID = TID;
        objCRMActivity.COMPID = 1;
        objCRMActivity.Prefix = "ONL";
        objCRMActivity.MasterCODE = master;
        objCRMActivity.MyLineNo = TID;
        int activityID = DB.CRMActivities.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Where(p => p.TenentID == TID).Max(p => p.ActivityID) + 1) : 1;
        objCRMActivity.ActivityID = activityID;
        objCRMActivity.LocationID = 1;
        objCRMActivity.LinkMasterCODE = master;
        objCRMActivity.MenuID = Convert.ToInt32(0);
        objCRMActivity.NextUser = strUName;
        objCRMActivity.ACTIVITYTYPE = "helpdesk";
        objCRMActivity.REFTYPE = "Ticket";
        objCRMActivity.REFSUBTYPE = "CRM";
        objCRMActivity.PerfReferenceNo = "CRM";
        objCRMActivity.EarlierRefNo = "A";
        objCRMActivity.NextRefNo = "A";
        objCRMActivity.GroupBy = "helpdesk";
        objCRMActivity.USERCODE = UID.ToString();
        objCRMActivity.AMIGLOBAL = "Y";
        objCRMActivity.MYPERSONNEL = "Y";

        //string data = txtMessage.Text;
        //string[] words = data.Split(' ');
        //string paramStr = "";
        //foreach (string word in words)
        //{
        //    Console.WriteLine(word + objCRMMainActivities.MyStatus);
        //    paramStr += word +  objCRMMainActivities.MyStatus;
        //}


        objCRMActivity.ActivityPerform = txtMessage.Text;
        objCRMActivity.REMINDERNOTE = "Pending";
        objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.ESTCOST);
        objCRMActivity.GROUPCODE = "A";
        objCRMActivity.ESTCOST = Convert.ToInt32(objCRMMainActivities.GROUPCODE);
        objCRMActivity.USERCODEENTERED = strUName;
        objCRMActivity.UrlRedirct = Request.Url.AbsolutePath;
        objCRMActivity.UPDTTIME = DateTime.Now;//Convert.ToDateTime(txtdates.Text); ;
        objCRMActivity.UploadDate = DateTime.Now; // Convert.ToDateTime(txtdates.Text); ;
        objCRMActivity.InitialDate = Convert.ToDateTime(txtdates.Text.Split(' ')[0] + " " + onlytime); //DateTime.Now;
        objCRMActivity.ExpStartDate = DateTime.Now;

        objCRMActivity.DeadLineDate = DateTime.Now;
        objCRMActivity.USERNAME = strUName;
        objCRMActivity.RouteID = "helpdesk";
        objCRMActivity.MyStatus = "Closed";
        objCRMActivity.ExpEndDate = DateTime.Now;

        objCRMActivity.Active = "N";
        objCRMActivity.ToColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.ToColumn) + 1) : 1;
        objCRMActivity.FromColumn = DB.CRMActivities.Count() > 0 ? Convert.ToInt32(DB.CRMActivities.Max(p => p.FromColumn) + 1) : 1;
        objCRMActivity.Version = strUName;
        objCRMActivity.TickDepartmentID = Convert.ToInt32(drpSDepartment.SelectedValue);
        objCRMActivity.TickComplainType = Convert.ToInt32(drpComplainType.SelectedValue);
        objCRMActivity.TickPhysicalLocation = Convert.ToInt32(DrpPhysicalLocation.SelectedValue);
        objCRMActivity.TickCatID = Convert.ToInt32(DrpTCatSubCate.SelectedValue);
        objCRMActivity.TickSubCatID = Convert.ToInt32(DrpSubCat.SelectedValue);
        objCRMActivity.Patient_Name = txtpatientname.Text;
        if (txtMRN.Text != "")
        {
          objCRMActivity.MRN = txtMRN.Text;
        }
        objCRMActivity.StaffInvoice = true;
        objCRMActivity.IRDone = true;
        objCRMActivity.ReportedBy = Convert.ToInt32(drpusermt.SelectedValue);
        objCRMActivity.ComplaintNumber = master.ToString();
        objCRMActivity.Contact = txtcontact.Text;
        objCRMActivity.IncidentReport = true;
        objCRMActivity.aspcomment = 2101401;
        objCRMActivity.CRUP_ID = objCRMMainActivities.CRUP_ID;
        objCRMActivity.Subject = txtSubject.Text;
        String url2 = "insert new record in CRMActivity with " + "TenentID = " + TID + "COMPID =1 MASTERCODE = " + objCRMMainActivities.MasterCODE + "LinkMasterCODE =" + objCRMMainActivities.MasterCODE + "LocationID = 1" + "ActivityID = " + activityID + "Prefix = ONL ";
        String evantname2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFNAME1;

        int seriea = DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == objCRMMainActivities.CRUP_ID).Count() > 0 ? Convert.ToInt32(DB.tblAudits.Where(p => p.TENANT_ID == TID && p.CRUP_ID == objCRMMainActivities.CRUP_ID).Max(p => p.MySerial) + 1) : 1;
        string loginUserId2 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();
        int auditno2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Audit" && p.REFSUBTYPE == "Complain").REFID;
        int crup = Convert.ToInt32(objCRMMainActivities.CRUP_ID);
        Database.tblAudit objaudits = new Database.tblAudit();
        objaudits.TENANT_ID = TID;
        objaudits.CRUP_ID = crup;
        objaudits.MySerial = seriea;
        objaudits.AuditNo = auditno2;
        objaudits.AuditType = evantname2;
        objaudits.TableName = "CRMActivity";
        objaudits.NewValue = url2;
        objaudits.CreatedDate = DateTime.Now;
        objaudits.CreatedUserName = UID.ToString();
        DB.tblAudits.AddObject(objaudits);
        DB.SaveChanges();
        Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Reply", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
        Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Close", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
        Classes.ACMClass.InsertDataCRMProgHW(TID, objCRMMainActivities.MasterCODE, 0, "Forward", true, "Pending", "helpdesk", true, DateTime.Now, 0, DateTime.Now, "/POS/ClientTiketR.aspx", "/POS/ClientTiketR.aspx", 0);
        DB.CRMActivities.AddObject(objCRMActivity); //2
        DB.SaveChanges();
        clen();
        ToalCoun();
        BindRemainderNote();

        string rj = "";
        string rj1 = " SELECT top(1) DateDiff(dd, ExpStartDate, ExpEndDate) As days  from CRMActivities where MasterCODE = " + master + " order by mylineno";
        DataTable dts = DataCon.GetDataTable(rj1);
        string lc = dts.Rows[0].ItemArray[0].ToString();

        string rj2 = "SELECT top(1) DateDiff(hh, ExpStartDate, ExpEndDate) % 24 As hours  from CRMActivities where MasterCODE = " + master + " order by mylineno";
        DataTable dt2 = DataCon.GetDataTable(rj2);
        string lc1 = dt2.Rows[0].ItemArray[0].ToString();

        string rj3 = "  SELECT top(1) DateDiff(mi, ExpStartDate, ExpEndDate) % 60 As mins  from CRMActivities where MasterCODE = " + master + " order by mylineno";
        DataTable dt3 = DataCon.GetDataTable(rj3);
        string lc3 = dt3.Rows[0].ItemArray[0].ToString();

        if (lc != "0" && lc1 != "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = lc + " days" + lc1 + " hours" + lc3 + " mins";
        }
        else if (lc != "0" && lc1 != "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 == "")
        {
          rj = lc + " days" + lc1 + " hours";
        }
        else if (lc == "0" && lc1 != "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = lc1 + " hours" + lc3 + " mins";
        }
        else if (lc != "0" && lc1 == "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = lc + " days" + lc3 + " mins";
        }
        else if (lc == "0" && lc1 == "0" && lc3 != "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = lc3 + " mins";
        }
        else if (lc == "0" && lc1 != "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = lc1 + " hours";
        }
        else if (lc != "0" && lc1 == "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = lc + " days";
        }
        else if (lc == "0" && lc1 == "0" && lc3 == "0" && lc != "" && lc1 != "" && lc3 != "")
        {
          rj = "0";
        }
        else
        {
          rj = "0";
        }
        //scope.Complete();               
        Database.CRMMainActivity objSOJobDesc = DB.CRMMainActivities.Single(p => p.MasterCODE == master);
        objSOJobDesc.MyStatus = "Closed";

        if (rj == " days hours mins")
        {
          objSOJobDesc.UseReciepeName = "0";
        }
        else
        {
          objSOJobDesc.UseReciepeName = rj;
        }
        // objSOJobDesc.UseReciepeName = rj;

        DB.SaveChanges();
        List<Database.CRMActivity> listgate = DB.CRMActivities.Where(p => p.MasterCODE == master).ToList();
        foreach (Database.CRMActivity item in listgate)
        {
          int gidd = item.ActivityID;
          int Lineno = item.MyLineNo;
          CRMActivity obj = DB.CRMActivities.Single(p => p.MasterCODE == master && p.ActivityID == gidd && p.MyLineNo == Lineno);
          obj.MyStatus = "Closed";
          DB.SaveChanges();
        }

        lblMsg.Text = "ticket save with close Succeessfully";
        pnlSuccessMsg.Visible = true;
        BindData();
        ToalCoun();
        int TickitNo = Convert.ToInt32(objCRMMainActivities.MasterCODE);
        string display = "Your Ticket Number Is " + TickitNo;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "alert('" + display + "');", true);
        //return;
        string msg = "Your Ticket Number Is " + TickitNo;
        string Function = "openModalsmall2('" + msg + "');";
        ClientScript.RegisterStartupScript(this.GetType(), "Tickit Number", "openModalsmall2();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
        maxComplainID();
      }
    }

    protected void btncount_Click(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      clen();
      maxComplainID();
      getStatusAll("NEW");
      pnlTicki.Visible = true;
      panChat.Visible = false;
      pnlinfo.Visible = false;
      PnlBindTick.Visible = false;
      pnlSuccessMsg.Visible = false;
      btnSave.Text = "Submit";
      btnCancel.Visible = true;
      btncack.Visible = false;
      btndelete.Visible = false;
      btncloseandupdate.Enabled = true;
      btncloseandupdate.Text = "Close Tkt & Save";
    }

    protected void btnattache_Click(object sender, EventArgs e)
    {
      int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
      int uid1 = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
      string UID = ((USER_MST)Session["USER"]).LOGIN_ID.ToString();
      int tiki = Convert.ToInt32(lblcomplainno.Text);
      string Type = "Ticket";
      string Url = "../CRM/AttachmentMst.aspx?AttachID=" + tiki + "&DID=" + Type + "&RefNo=" + 11058 + "&UID=" + uid1;//+ "&recodInsert=" + tiki;
      string s = "window.open('" + Url + "', 'popup_window', 'width=950,height=225,left=100,top=100,resizable=yes');";
      ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "script", s, true);
    }
  }
}
