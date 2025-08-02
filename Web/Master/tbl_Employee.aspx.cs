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

namespace Web.Master
{
    public partial class tbl_Employee : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                btnAdd.ValidationGroup = "ss";
                // FirstData();
                Clear();


            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            List<Database.tbl_Employee> List = DB.tbl_Employee.Where(m => m.TenentID == TID).OrderBy(m => m.employeeID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lbluserID1s.Attributes["class"] = lblfirstname1s.Attributes["class"] = lblStuden_LoginID1s.Attributes["class"] = lblPASSWORD1s.Attributes["class"] = lblemp_mobile1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblfirstname2h.Attributes["class"] = lblname22h.Attributes["class"] = lblStuden_LoginID2h.Attributes["class"] = lblPASSWORD2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lbluserID1s.Attributes["class"] = lblfirstname1s.Attributes["class"] = lblname21s.Attributes["class"] = lblStuden_LoginID1s.Attributes["class"] = lblPASSWORD1s.Attributes["class"] = "control-label col-md-4  gethide";
            lbluserID2h.Attributes["class"] = lblfirstname2h.Attributes["class"] = lblname22h.Attributes["class"] = lblStuden_LoginID2h.Attributes["class"] = lblPASSWORD2h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            GetHide();
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetShow();
        }

        public void Clear()
        {
            txtMiddlename.Text = "";
            txtlastname.Text = "";
            txtfirstname.Text = "";
            drpvaliduser.SelectedIndex = 0;
            txtStuden_LoginID.Text = "";
            txtPASSWORD.Text = "";
            txtemp_mobile.Text = "";
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (btnAdd.Text == "Add New")
                {
                    Write();
                    Clear();
                    btnAdd.Text = "Save";
                    btnAdd.ValidationGroup = "submit";
                    pnlSuccessMsg.Visible = false;
                }
                else if (btnAdd.Text == "Save")
                {
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    Database.tbl_Employee objtbl_Employee = new Database.tbl_Employee();
                    int MAXID = DB.tbl_Employee.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Employee.Where(p => p.TenentID == TID).Max(p => p.employeeID) + 1) : 1;

                    objtbl_Employee.TenentID = TID;
                    objtbl_Employee.employeeID = MAXID;
                    objtbl_Employee.firstname = txtfirstname.Text;
                    objtbl_Employee.lastname = txtlastname.Text;
                    objtbl_Employee.middle_name = txtMiddlename.Text;
                    objtbl_Employee.emp_mobile = txtemp_mobile.Text;
                    objtbl_Employee.userID = drpvaliduser.SelectedValue;
                    if (drpvaliduser.SelectedValue != "0")
                        objtbl_Employee.MainHRRoleID = ISSuper.Checked == true ? 1 : 0;
                    else
                        objtbl_Employee.MainHRRoleID = 0;
                   
                    DB.tbl_Employee.AddObject(objtbl_Employee);
                    DB.SaveChanges();

                    String url = "insert new record in tbl_Employee with " + "TenentID = " + TID + "employeeID =" + MAXID;
                    String evantname = "create";
                    String tablename = "tbl_Employee";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);



                    Clear();
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    // navigation.Visible = true;
                    Readonly();
                    //  FirstData();
                }
                else if (btnAdd.Text == "Update")
                {
                    if (ViewState["Edit"] != null)
                    {
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                        Database.tbl_Employee objtbl_Employee = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID);

                        objtbl_Employee.lastname = txtlastname.Text;
                        objtbl_Employee.firstname = txtfirstname.Text;
                        objtbl_Employee.middle_name = txtMiddlename.Text;
                        objtbl_Employee.emp_mobile = txtemp_mobile.Text;
                        objtbl_Employee.userID = drpvaliduser.SelectedValue;                           
                        if (drpvaliduser.SelectedValue != "0")
                            objtbl_Employee.MainHRRoleID = ISSuper.Checked == true ? 1 : 0;
                        else
                            objtbl_Employee.MainHRRoleID = 0;
                        DB.SaveChanges();

                        String url = "update tbl_Employee with " + "TenentID = " + TID + "employeeID =" + ID;
                        String evantname = "update";
                        String tablename = "tbl_Employee";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                        ViewState["Edit"] = null;
                        btnAdd.Text = "Add New";
                        btnAdd.ValidationGroup = "ss";
                        Clear();

                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        // navigation.Visible = true;
                        Readonly();
                        // FirstData();
                    }
                }
                scope.Complete(); //  To commit.


            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("tbl_Employee.aspx");
        }
        public void FillContractorID()
        {
            //drpDeviceID.Items.Insert(0, new ListItem("-- Select --", "0"));drpDeviceID.DataSource = DB.0;drpDeviceID.DataTextField = "0";drpDeviceID.DataValueField = "0";drpDeviceID.DataBind();
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int AUID = Convert.ToInt32(DB.MYCOMPANYSETUPs.Single(p=>p.TenentID == TID).USERID);
            int CompID = Convert.ToInt32(DB.USER_MST.Single(p=>p.TenentID == TID && p.USER_ID == AUID).CompId);
            drpvaliduser.DataSource = DB.USER_MST.Where(p => p.TenentID == TID && p.CompId == CompID);
            drpvaliduser.DataTextField = "FIRST_NAME";
            drpvaliduser.DataValueField = "USER_ID";
            drpvaliduser.DataBind();
            drpvaliduser.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            FirstData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            NextData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            PrevData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            LastData();
        }
        public void FirstData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            //  drplocation.Text = Listview1.SelectedDataKey[0].ToString();
            // txtemployeeID.Text = Listview1.SelectedDataKey[0].ToString();
            // txtcontactID.Text = Listview1.SelectedDataKey[0].ToString();
            txtMiddlename.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtActiveDirectoryID.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtMainHRRoleID.Text = Listview1.SelectedDataKey[0].ToString();
            txtlastname.Text = Listview1.SelectedDataKey[0].ToString();
            txtfirstname.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtmiddle_name.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtemp_nick_name.Text = Listview1.SelectedDataKey[0].ToString();
            drpvaliduser.Text = Listview1.SelectedDataKey[0].ToString();

            txtStuden_LoginID.Text = Listview1.SelectedDataKey[0].ToString();
            txtPASSWORD.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_smoker.Text = Listview1.SelectedDataKey[0].ToString();
            // txtethnic_race_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_birthday.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtnation_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_gender.Text = Listview1.SelectedDataKey[0].ToString();
            //    txtemp_marital_status.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtemp_ssn_num.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtemp_sin_num.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_other_id.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtemp_dri_lice_num.Text = Listview1.SelectedDataKey[0].ToString();
            // txtemp_dri_lice_exp_date.Text = Listview1.SelectedDataKey[0].ToString();
            // txtemp_military_service.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_status.Text = Listview1.SelectedDataKey[0].ToString();
            //  drpEmployeeType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //  txtjob_title_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txteeo_cat_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtwork_station.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_street1.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_street2.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtcity_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtcoun_code.Text = Listview1.SelectedDataKey[0].ToString();
            // txtprovin_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_zipcode.Text = Listview1.SelectedDataKey[0].ToString();
            //    txtemp_hm_telephone.Text = Listview1.SelectedDataKey[0].ToString();
            txtemp_mobile.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_work_telephone.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_work_email.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtsal_grd_code.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtjoined_date.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemp_oth_email.Text = Listview1.SelectedDataKey[0].ToString();
            //  txttermination_id.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom1.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom2.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom3.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom4.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom5.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom6.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom7.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom8.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom9.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom10.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //  txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            // txtDeviceID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //  drplocation.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemployeeID.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtcontactID.Text = Listview1.SelectedDataKey[0].ToString();
                txtMiddlename.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtActiveDirectoryID.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtMainHRRoleID.Text = Listview1.SelectedDataKey[0].ToString();
                txtlastname.Text = Listview1.SelectedDataKey[0].ToString();
                txtfirstname.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtmiddle_name.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_nick_name.Text = Listview1.SelectedDataKey[0].ToString();
                drpvaliduser.Text = Listview1.SelectedDataKey[0].ToString();
                txtStuden_LoginID.Text = Listview1.SelectedDataKey[0].ToString();
                txtPASSWORD.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_smoker.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtethnic_race_code.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_birthday.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtnation_code.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_gender.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_marital_status.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_ssn_num.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_sin_num.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_other_id.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_dri_lice_num.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemp_dri_lice_exp_date.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemp_military_service.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemp_status.Text = Listview1.SelectedDataKey[0].ToString();
                //   drpEmployeeType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //   txtjob_title_code.Text = Listview1.SelectedDataKey[0].ToString();
                //    txteeo_cat_code.Text = Listview1.SelectedDataKey[0].ToString();
                //    txtwork_station.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemp_street1.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemp_street2.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtcity_code.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtcoun_code.Text = Listview1.SelectedDataKey[0].ToString();
                // txtprovin_code.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_zipcode.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_hm_telephone.Text = Listview1.SelectedDataKey[0].ToString();
                txtemp_mobile.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_work_telephone.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_work_email.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtsal_grd_code.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtjoined_date.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_oth_email.Text = Listview1.SelectedDataKey[0].ToString();
                //   txttermination_id.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom1.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom2.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom3.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom4.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom5.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom6.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom7.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom8.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom9.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom10.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //  txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                // txtDeviceID.Text = Listview1.SelectedDataKey[0].ToString();
            }

        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                lblMsg.Text = "This is first record";
                pnlSuccessMsg.Visible = true;

            }
            else
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                //  drplocation.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemployeeID.Text = Listview1.SelectedDataKey[0].ToString();
                // txtcontactID.Text = Listview1.SelectedDataKey[0].ToString();
                txtMiddlename.Text = Listview1.SelectedDataKey[0].ToString();
                // txtActiveDirectoryID.Text = Listview1.SelectedDataKey[0].ToString();
                // txtMainHRRoleID.Text = Listview1.SelectedDataKey[0].ToString();
                txtlastname.Text = Listview1.SelectedDataKey[0].ToString();
                txtfirstname.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtmiddle_name.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtemp_nick_name.Text = Listview1.SelectedDataKey[0].ToString();
                drpvaliduser.Text = Listview1.SelectedDataKey[0].ToString();
                txtStuden_LoginID.Text = Listview1.SelectedDataKey[0].ToString();
                txtPASSWORD.Text = Listview1.SelectedDataKey[0].ToString();
                //  txtemp_smoker.Text = Listview1.SelectedDataKey[0].ToString();
                //txtethnic_race_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_birthday.Text = Listview1.SelectedDataKey[0].ToString();
                //txtnation_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_gender.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_marital_status.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_ssn_num.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_sin_num.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_other_id.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_dri_lice_num.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_dri_lice_exp_date.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_military_service.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_status.Text = Listview1.SelectedDataKey[0].ToString();
                //drpEmployeeType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtjob_title_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txteeo_cat_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtwork_station.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_street1.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_street2.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcity_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcoun_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtprovin_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_zipcode.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_hm_telephone.Text = Listview1.SelectedDataKey[0].ToString();
                txtemp_mobile.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_work_telephone.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_work_email.Text = Listview1.SelectedDataKey[0].ToString();
                //txtsal_grd_code.Text = Listview1.SelectedDataKey[0].ToString();
                //txtjoined_date.Text = Listview1.SelectedDataKey[0].ToString();
                //txtemp_oth_email.Text = Listview1.SelectedDataKey[0].ToString();
                //txttermination_id.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom1.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom2.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom3.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom4.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom5.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom6.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom7.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom8.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom9.Text = Listview1.SelectedDataKey[0].ToString();
                //txtcustom10.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //    txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //   txtDeviceID.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //  drplocation.Text = Listview1.SelectedDataKey[0].ToString();
            //  txtemployeeID.Text = Listview1.SelectedDataKey[0].ToString();
            // txtcontactID.Text = Listview1.SelectedDataKey[0].ToString();
            txtMiddlename.Text = Listview1.SelectedDataKey[0].ToString();
            //txtActiveDirectoryID.Text = Listview1.SelectedDataKey[0].ToString();
            //txtMainHRRoleID.Text = Listview1.SelectedDataKey[0].ToString();
            txtlastname.Text = Listview1.SelectedDataKey[0].ToString();
            txtfirstname.Text = Listview1.SelectedDataKey[0].ToString();
            //txtmiddle_name.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_nick_name.Text = Listview1.SelectedDataKey[0].ToString();
            drpvaliduser.Text = Listview1.SelectedDataKey[0].ToString();
            txtStuden_LoginID.Text = Listview1.SelectedDataKey[0].ToString();
            txtPASSWORD.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_smoker.Text = Listview1.SelectedDataKey[0].ToString();
            //txtethnic_race_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_birthday.Text = Listview1.SelectedDataKey[0].ToString();
            //txtnation_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_gender.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_marital_status.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_ssn_num.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_sin_num.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_other_id.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_dri_lice_num.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_dri_lice_exp_date.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_military_service.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_status.Text = Listview1.SelectedDataKey[0].ToString();
            //drpEmployeeType.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtjob_title_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txteeo_cat_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtwork_station.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_street1.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_street2.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcity_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcoun_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtprovin_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_zipcode.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_hm_telephone.Text = Listview1.SelectedDataKey[0].ToString();
            txtemp_mobile.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_work_telephone.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_work_email.Text = Listview1.SelectedDataKey[0].ToString();
            //txtsal_grd_code.Text = Listview1.SelectedDataKey[0].ToString();
            //txtjoined_date.Text = Listview1.SelectedDataKey[0].ToString();
            //txtemp_oth_email.Text = Listview1.SelectedDataKey[0].ToString();
            //txttermination_id.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom1.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom2.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom3.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom4.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom5.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom6.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom7.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom8.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom9.Text = Listview1.SelectedDataKey[0].ToString();
            //txtcustom10.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //  txtDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //   txtDeviceID.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lbluserID2h.Visible = lblfirstname2h.Visible = lblname22h.Visible = lblStuden_LoginID2h.Visible = lblPASSWORD2h.Visible = lblemp_mobile2h.Visible = false;
                    //2true
                    txtuserID2h.Visible = txtfirstname2h.Visible = txtname22h.Visible = txtStuden_LoginID2h.Visible = txtPASSWORD2h.Visible = txtemp_mobile2h.Visible = true;

                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblname22h.Visible = lblStuden_LoginID2h.Visible = lblPASSWORD2h.Visible = lblemp_mobile2h.Visible = true;
                    //2false
                    txtuserID2h.Visible = txtfirstname2h.Visible = txtname22h.Visible = txtStuden_LoginID2h.Visible = txtPASSWORD2h.Visible = txtemp_mobile2h.Visible = false;

                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lbluserID1s.Visible = lblfirstname1s.Visible = lblname21s.Visible = lblStuden_LoginID1s.Visible = lblPASSWORD1s.Visible = false;
                    //1true
                    txtuserID1s.Visible = txtfirstname1s.Visible = txtname21s.Visible = txtStuden_LoginID1s.Visible = txtPASSWORD1s.Visible = true;
                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lbluserID1s.Visible = lblfirstname1s.Visible = lblname21s.Visible = lblStuden_LoginID1s.Visible = lblPASSWORD1s.Visible = lblemp_mobile1s.Visible = true;
                    //1false
                    txtuserID1s.Visible = txtfirstname1s.Visible = txtname21s.Visible = txtStuden_LoginID1s.Visible = txtPASSWORD1s.Visible = false;
                    //header
                    txtHeader.Visible = false;
                }
            }
            lblHeader.Visible = true;
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_Employee").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenentID1s.ID == item.LabelID)
                //    txtTenentID1s.Text = lblTenentID1s.Text = lblTenentID1s.Text = item.LabelName;
                //if (lblLocationID1s.ID == item.LabelID)
                //   txtLocationID1s.Text = lblLocationID1s.Text = lblLocationID1s.Text = item.LabelName;
                //else if (lblemployeeID1s.ID == item.LabelID)
                //    txtemployeeID1s.Text = lblemployeeID1s.Text = lblhemployeeID.Text = item.LabelName;
                //else if (lblcontactID1s.ID == item.LabelID)
                //    txtcontactID1s.Text = lblcontactID1s.Text = lblcontactID1s.Text = item.LabelName;
                if (lbluserID1s.ID == item.LabelID)
                    txtuserID1s.Text = lbluserID1s.Text = lbluserID1s.Text = item.LabelName;
                //else if (lblActiveDirectoryID1s.ID == item.LabelID)
                //    txtActiveDirectoryID1s.Text = lblActiveDirectoryID1s.Text = lblActiveDirectoryID1s.Text = item.LabelName;
                else if (lblMainHRRoleID1s.ID == item.LabelID)
                    txtMainHRRoleID1s.Text = lblMainHRRoleID1s.Text = lblMainHRRoleID1s.Text = item.LabelName;
                else if (lbllastname1s.ID == item.LabelID)
                    txtlastname1s.Text = lbllastname1s.Text = lbllastname1s.Text = item.LabelName;
                else if (lblfirstname1s.ID == item.LabelID)
                    txtfirstname1s.Text = lblfirstname1s.Text = lblfirstname1s.Text = item.LabelName;
                //else if (lblmiddle_name1s.ID == item.LabelID)
                //    txtmiddle_name1s.Text = lblmiddle_name1s.Text = lblmiddle_name1s.Text = item.LabelName;
                //else if (lblemp_nick_name1s.ID == item.LabelID)
                //    txtemp_nick_name1s.Text = lblemp_nick_name1s.Text = lblemp_nick_name1s.Text = item.LabelName;
                else if (lblname21s.ID == item.LabelID)
                    txtname21s.Text = lblname21s.Text = lblname21s.Text = item.LabelName;
                else if (lblStuden_LoginID1s.ID == item.LabelID)
                    txtStuden_LoginID1s.Text = lblStuden_LoginID1s.Text = lblStuden_LoginID1s.Text = item.LabelName;
                else if (lblPASSWORD1s.ID == item.LabelID)
                    txtPASSWORD1s.Text = lblPASSWORD1s.Text = lblPASSWORD1s.Text = item.LabelName;
                //else if (lblemp_smoker1s.ID == item.LabelID)
                //    txtemp_smoker1s.Text = lblemp_smoker1s.Text = lblemp_smoker1s.Text = item.LabelName;
                //else if (lblethnic_race_code1s.ID == item.LabelID)
                //    txtethnic_race_code1s.Text = lblethnic_race_code1s.Text = lblethnic_race_code1s.Text = item.LabelName;
                //else if (lblemp_birthday1s.ID == item.LabelID)
                //    txtemp_birthday1s.Text = lblemp_birthday1s.Text = lblhemp_birthday.Text = item.LabelName;
                //else if (lblnation_code1s.ID == item.LabelID)
                //    txtnation_code1s.Text = lblnation_code1s.Text = lblhnation_code.Text = item.LabelName;
                //else if (lblemp_gender1s.ID == item.LabelID)
                //    txtemp_gender1s.Text = lblemp_gender1s.Text = lblemp_gender1s.Text = item.LabelName;
                //else if (lblemp_marital_status1s.ID == item.LabelID)
                //    txtemp_marital_status1s.Text = lblemp_marital_status1s.Text = lblemp_marital_status1s.Text = item.LabelName;
                //else if (lblemp_ssn_num1s.ID == item.LabelID)
                //    txtemp_ssn_num1s.Text = lblemp_ssn_num1s.Text = lblemp_ssn_num1s.Text = item.LabelName;
                //else if (lblemp_sin_num1s.ID == item.LabelID)
                //    txtemp_sin_num1s.Text = lblemp_sin_num1s.Text = lblemp_sin_num1s.Text = item.LabelName;
                //else if (lblemp_other_id1s.ID == item.LabelID)
                //    txtemp_other_id1s.Text = lblemp_other_id1s.Text = lblemp_other_id1s.Text = item.LabelName;
                //else if (lblemp_dri_lice_num1s.ID == item.LabelID)
                //    txtemp_dri_lice_num1s.Text = lblemp_dri_lice_num1s.Text = lblemp_dri_lice_num1s.Text = item.LabelName;
                //else if (lblemp_dri_lice_exp_date1s.ID == item.LabelID)
                //    txtemp_dri_lice_exp_date1s.Text = lblemp_dri_lice_exp_date1s.Text = lblemp_dri_lice_exp_date1s.Text = item.LabelName;
                //else if (lblemp_military_service1s.ID == item.LabelID)
                //    txtemp_military_service1s.Text = lblemp_military_service1s.Text = lblemp_military_service1s.Text = item.LabelName;
                //else if (lblemp_status1s.ID == item.LabelID)
                //    txtemp_status1s.Text = lblemp_status1s.Text = lblemp_status1s.Text = item.LabelName;
                //else if (lblEmployeeType1s.ID == item.LabelID)
                //    txtEmployeeType1s.Text = lblEmployeeType1s.Text = lblEmployeeType1s.Text = item.LabelName;
                //else if (lbljob_title_code1s.ID == item.LabelID)
                //    txtjob_title_code1s.Text = lbljob_title_code1s.Text = lbljob_title_code1s.Text = item.LabelName;
                //else if (lbleeo_cat_code1s.ID == item.LabelID)
                //    txteeo_cat_code1s.Text = lbleeo_cat_code1s.Text = lbleeo_cat_code1s.Text = item.LabelName;
                //else if (lblwork_station1s.ID == item.LabelID)
                //    txtwork_station1s.Text = lblwork_station1s.Text = lblwork_station1s.Text = item.LabelName;
                //else if (lblemp_street11s.ID == item.LabelID)
                //    txtemp_street11s.Text = lblemp_street11s.Text = lblemp_street11s.Text = item.LabelName;
                //else if (lblemp_street21s.ID == item.LabelID)
                //    txtemp_street21s.Text = lblemp_street21s.Text = lblemp_street21s.Text = item.LabelName;
                //else if (lblcity_code1s.ID == item.LabelID)
                //    txtcity_code1s.Text = lblcity_code1s.Text = lblcity_code1s.Text = item.LabelName;
                //else if (lblcoun_code1s.ID == item.LabelID)
                //    txtcoun_code1s.Text = lblcoun_code1s.Text = lblcoun_code1s.Text = item.LabelName;
                //else if (lblprovin_code1s.ID == item.LabelID)
                //    txtprovin_code1s.Text = lblprovin_code1s.Text = lblprovin_code1s.Text = item.LabelName;
                //else if (lblemp_zipcode1s.ID == item.LabelID)
                //    txtemp_zipcode1s.Text = lblemp_zipcode1s.Text = lblemp_zipcode1s.Text = item.LabelName;
                //else if (lblemp_hm_telephone1s.ID == item.LabelID)
                //    txtemp_hm_telephone1s.Text = lblemp_hm_telephone1s.Text = lblemp_hm_telephone1s.Text = item.LabelName;
                else if (lblemp_mobile1s.ID == item.LabelID)
                    txtemp_mobile.Text = lblemp_mobile1s.Text = item.LabelName;
                //else if (lblemp_work_telephone1s.ID == item.LabelID)
                //    txtemp_work_telephone1s.Text = lblemp_work_telephone1s.Text = lblhemp_work_telephone.Text = item.LabelName;
                //else if (lblemp_work_email1s.ID == item.LabelID)
                //    txtemp_work_email1s.Text = lblemp_work_email1s.Text = lblhemp_work_email.Text = item.LabelName;
                //else if (lblsal_grd_code1s.ID == item.LabelID)
                //    txtsal_grd_code1s.Text = lblsal_grd_code1s.Text = lblsal_grd_code1s.Text = item.LabelName;
                //else if (lbljoined_date1s.ID == item.LabelID)
                //    txtjoined_date1s.Text = lbljoined_date1s.Text = lblhjoined_date.Text = item.LabelName;
                //else if (lblemp_oth_email1s.ID == item.LabelID)
                //    txtemp_oth_email1s.Text = lblemp_oth_email1s.Text = lblemp_oth_email1s.Text = item.LabelName;
                //else if (lbltermination_id1s.ID == item.LabelID)
                //    txttermination_id1s.Text = lbltermination_id1s.Text = lbltermination_id1s.Text = item.LabelName;
                //else if (lblActive1s.ID == item.LabelID)
                //    txtActive1s.Text = lblActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    txtDeleted1s.Text = lblDeleted1s.Text = lblDeleted1s.Text = item.LabelName;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    txtDateTime1s.Text = lblDateTime1s.Text = lblDateTime1s.Text = item.LabelName;
                //else if (lblDeviceID1s.ID == item.LabelID)
                //    txtDeviceID1s.Text = lblDeviceID1s.Text = lblDeviceID1s.Text = item.LabelName;

               //else if (lblTenentID2h.ID == item.LabelID)
                //    txtTenentID2h.Text = lblTenentID2h.Text = lblTenentID2h.Text = item.LabelName;
                //else if (lblLocationID2h.ID == item.LabelID)
                //    txtLocationID2h.Text = lblLocationID2h.Text = lblLocationID2h.Text = item.LabelName;
                //else if (lblemployeeID2h.ID == item.LabelID)
                //    txtemployeeID2h.Text = lblemployeeID2h.Text = lblhemployeeID.Text = item.LabelName;
                //else if (lblcontactID2h.ID == item.LabelID)
                //    txtcontactID2h.Text = lblcontactID2h.Text = lblcontactID2h.Text = item.LabelName;
                else if (lbluserID2h.ID == item.LabelID)
                    txtuserID2h.Text = lbluserID2h.Text = lbluserID2h.Text = item.LabelName;
                //else if (lblActiveDirectoryID2h.ID == item.LabelID)
                //    txtActiveDirectoryID2h.Text = lblActiveDirectoryID2h.Text = lblActiveDirectoryID2h.Text = item.LabelName;
                else if (lblMainHRRoleID2h.ID == item.LabelID)
                    txtMainHRRoleID2h.Text = lblMainHRRoleID2h.Text = lblMainHRRoleID2h.Text = item.LabelName;
                else if (lbllastname2h.ID == item.LabelID)
                    txtlastname2h.Text = lbllastname2h.Text = lbllastname2h.Text = item.LabelName;
                else if (lblfirstname2h.ID == item.LabelID)
                    txtfirstname2h.Text = lblfirstname2h.Text = lblfirstname2h.Text = item.LabelName;
                //else if (lblmiddle_name2h.ID == item.LabelID)
                //    txtmiddle_name2h.Text = lblmiddle_name2h.Text = lblmiddle_name2h.Text = item.LabelName;
                //else if (lblemp_nick_name2h.ID == item.LabelID)
                //    txtemp_nick_name2h.Text = lblemp_nick_name2h.Text = lblemp_nick_name2h.Text = item.LabelName;
                else if (lblname22h.ID == item.LabelID)
                    txtname22h.Text = lblname22h.Text = lblname22h.Text = item.LabelName;
                else if (lblStuden_LoginID2h.ID == item.LabelID)
                    txtStuden_LoginID2h.Text = lblStuden_LoginID2h.Text = lblStuden_LoginID2h.Text = item.LabelName;
                else if (lblPASSWORD2h.ID == item.LabelID)
                    txtPASSWORD2h.Text = lblPASSWORD2h.Text = lblPASSWORD2h.Text = item.LabelName;
                //else if (lblemp_smoker2h.ID == item.LabelID)
                //    txtemp_smoker2h.Text = lblemp_smoker2h.Text = lblemp_smoker2h.Text = item.LabelName;
                //else if (lblethnic_race_code2h.ID == item.LabelID)
                //    txtethnic_race_code2h.Text = lblethnic_race_code2h.Text = lblethnic_race_code2h.Text = item.LabelName;
                //else if (lblemp_birthday2h.ID == item.LabelID)
                //    txtemp_birthday2h.Text = lblemp_birthday2h.Text = lblhemp_birthday.Text = item.LabelName;
                //else if (lblnation_code2h.ID == item.LabelID)
                //    txtnation_code2h.Text = lblnation_code2h.Text = lblhnation_code.Text = item.LabelName;
                //else if (lblemp_gender2h.ID == item.LabelID)
                //    txtemp_gender2h.Text = lblemp_gender2h.Text = lblemp_gender2h.Text = item.LabelName;
                //else if (lblemp_marital_status2h.ID == item.LabelID)
                //    txtemp_marital_status2h.Text = lblemp_marital_status2h.Text = lblemp_marital_status2h.Text = item.LabelName;
                //else if (lblemp_ssn_num2h.ID == item.LabelID)
                //    txtemp_ssn_num2h.Text = lblemp_ssn_num2h.Text = lblemp_ssn_num2h.Text = item.LabelName;
                //else if (lblemp_sin_num2h.ID == item.LabelID)
                //    txtemp_sin_num2h.Text = lblemp_sin_num2h.Text = lblemp_sin_num2h.Text = item.LabelName;
                //else if (lblemp_other_id2h.ID == item.LabelID)
                //    txtemp_other_id2h.Text = lblemp_other_id2h.Text = lblemp_other_id2h.Text = item.LabelName;
                //else if (lblemp_dri_lice_num2h.ID == item.LabelID)
                //    txtemp_dri_lice_num2h.Text = lblemp_dri_lice_num2h.Text = lblemp_dri_lice_num2h.Text = item.LabelName;
                //else if (lblemp_dri_lice_exp_date2h.ID == item.LabelID)
                //    txtemp_dri_lice_exp_date2h.Text = lblemp_dri_lice_exp_date2h.Text = lblemp_dri_lice_exp_date2h.Text = item.LabelName;
                //else if (lblemp_military_service2h.ID == item.LabelID)
                //    txtemp_military_service2h.Text = lblemp_military_service2h.Text = lblemp_military_service2h.Text = item.LabelName;
                //else if (lblemp_status2h.ID == item.LabelID)
                //    txtemp_status2h.Text = lblemp_status2h.Text = lblemp_status2h.Text = item.LabelName;
                //else if (lblEmployeeType2h.ID == item.LabelID)
                //    txtEmployeeType2h.Text = lblEmployeeType2h.Text = lblEmployeeType2h.Text = item.LabelName;
                //else if (lbljob_title_code2h.ID == item.LabelID)
                //    txtjob_title_code2h.Text = lbljob_title_code2h.Text = lbljob_title_code2h.Text = item.LabelName;
                //else if (lbleeo_cat_code2h.ID == item.LabelID)
                //    txteeo_cat_code2h.Text = lbleeo_cat_code2h.Text = lbleeo_cat_code2h.Text = item.LabelName;
                //else if (lblwork_station2h.ID == item.LabelID)
                //    txtwork_station2h.Text = lblwork_station2h.Text = lblwork_station2h.Text = item.LabelName;
                //else if (lblemp_street12h.ID == item.LabelID)
                //    txtemp_street12h.Text = lblemp_street12h.Text = lblemp_street12h.Text = item.LabelName;
                //else if (lblemp_street22h.ID == item.LabelID)
                //    txtemp_street22h.Text = lblemp_street22h.Text = lblemp_street22h.Text = item.LabelName;
                //else if (lblcity_code2h.ID == item.LabelID)
                //    txtcity_code2h.Text = lblcity_code2h.Text = lblcity_code2h.Text = item.LabelName;
                //else if (lblcoun_code2h.ID == item.LabelID)
                //    txtcoun_code2h.Text = lblcoun_code2h.Text = lblcoun_code2h.Text = item.LabelName;
                //else if (lblprovin_code2h.ID == item.LabelID)
                //    txtprovin_code2h.Text = lblprovin_code2h.Text = lblprovin_code2h.Text = item.LabelName;
                //else if (lblemp_zipcode2h.ID == item.LabelID)
                //    txtemp_zipcode2h.Text = lblemp_zipcode2h.Text = lblemp_zipcode2h.Text = item.LabelName;
                //else if (lblemp_hm_telephone2h.ID == item.LabelID)
                //    txtemp_hm_telephone2h.Text = lblemp_hm_telephone2h.Text = lblemp_hm_telephone2h.Text = item.LabelName;
                else if (lblemp_mobile2h.ID == item.LabelID)
                    txtemp_mobile2h.Text = lblemp_mobile2h.Text = lblhemp_mobile.Text = item.LabelName;
                //else if (lblemp_work_telephone2h.ID == item.LabelID)
                //    txtemp_work_telephone2h.Text = lblemp_work_telephone2h.Text = lblhemp_work_telephone.Text = item.LabelName;
                //else if (lblemp_work_email2h.ID == item.LabelID)
                //    txtemp_work_email2h.Text = lblemp_work_email2h.Text = lblhemp_work_email.Text = item.LabelName;
                //else if (lblsal_grd_code2h.ID == item.LabelID)
                //    txtsal_grd_code2h.Text = lblsal_grd_code2h.Text = lblsal_grd_code2h.Text = item.LabelName;
                //else if (lbljoined_date2h.ID == item.LabelID)
                //    txtjoined_date2h.Text = lbljoined_date2h.Text = lblhjoined_date.Text = item.LabelName;
                //else if (lblemp_oth_email2h.ID == item.LabelID)
                //    txtemp_oth_email2h.Text = lblemp_oth_email2h.Text = lblemp_oth_email2h.Text = item.LabelName;
                //else if (lbltermination_id2h.ID == item.LabelID)
                //    txttermination_id2h.Text = lbltermination_id2h.Text = lbltermination_id2h.Text = item.LabelName;
                //else if (lblActive2h.ID == item.LabelID)
                //    txtActive2h.Text = lblActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    txtDeleted2h.Text = lblDeleted2h.Text = lblDeleted2h.Text = item.LabelName;
                //else if (lblDateTime2h.ID == item.LabelID)
                //    txtDateTime2h.Text = lblDateTime2h.Text = lblDateTime2h.Text = item.LabelName;
                //else if (lblDeviceID2h.ID == item.LabelID)
                //    txtDeviceID2h.Text = lblDeviceID2h.Text = lblDeviceID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_Employee").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tbl_Employee.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tbl_Employee").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenentID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenentID1s.Text;
                //if (lblLocationID1s.ID == item.LabelID)
                //   ds.Tables[0].Rows[i]["LabelName"] = txtLocationID1s.Text;
                //else if (lblemployeeID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemployeeID1s.Text;
                //else if (lblcontactID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcontactID1s.Text;
                if (lbluserID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtuserID1s.Text;
                //else if (lblActiveDirectoryID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActiveDirectoryID1s.Text;
                else if (lblMainHRRoleID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMainHRRoleID1s.Text;
                else if (lbllastname1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlastname1s.Text;
                else if (lblfirstname1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtfirstname1s.Text;
                //else if (lblmiddle_name1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtmiddle_name1s.Text;
                //else if (lblemp_nick_name1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_nick_name1s.Text;
                else if (lblname21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtname21s.Text;
                else if (lblStuden_LoginID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStuden_LoginID1s.Text;
                else if (lblPASSWORD1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPASSWORD1s.Text;
                //else if (lblemp_smoker1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_smoker1s.Text;
                //else if (lblethnic_race_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtethnic_race_code1s.Text;
                //else if (lblemp_birthday1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_birthday1s.Text;
                //else if (lblnation_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtnation_code1s.Text;
                //else if (lblemp_gender1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_gender1s.Text;
                //else if (lblemp_marital_status1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_marital_status1s.Text;
                //else if (lblemp_ssn_num1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_ssn_num1s.Text;
                //else if (lblemp_sin_num1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_sin_num1s.Text;
                //else if (lblemp_other_id1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_other_id1s.Text;
                //else if (lblemp_dri_lice_num1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_dri_lice_num1s.Text;
                //else if (lblemp_dri_lice_exp_date1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_dri_lice_exp_date1s.Text;
                //else if (lblemp_military_service1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_military_service1s.Text;
                //else if (lblemp_status1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_status1s.Text;
                //else if (lblEmployeeType1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEmployeeType1s.Text;
                //else if (lbljob_title_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtjob_title_code1s.Text;
                //else if (lbleeo_cat_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txteeo_cat_code1s.Text;
                //else if (lblwork_station1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtwork_station1s.Text;
                //else if (lblemp_street11s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_street11s.Text;
                //else if (lblemp_street21s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_street21s.Text;
                //else if (lblcity_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcity_code1s.Text;
                //else if (lblcoun_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcoun_code1s.Text;
                //else if (lblprovin_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtprovin_code1s.Text;
                //else if (lblemp_zipcode1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_zipcode1s.Text;
                //else if (lblemp_hm_telephone1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_hm_telephone1s.Text;
                //else if (lblemp_mobile1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_mobile1s.Text;
                //else if (lblemp_work_telephone1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_work_telephone1s.Text;
                //else if (lblemp_work_email1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_work_email1s.Text;
                //else if (lblsal_grd_code1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtsal_grd_code1s.Text;
                //else if (lbljoined_date1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtjoined_date1s.Text;
                //else if (lblemp_oth_email1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_oth_email1s.Text;
                //else if (lbltermination_id1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttermination_id1s.Text;
                //else if (lblActive1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;
                //else if (lblDateTime1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime1s.Text;
                //else if (lblDeviceID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeviceID1s.Text;

                //else if (lblTenentID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenentID2h.Text;
                //else if (lblLocationID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtLocationID2h.Text;
                //else if (lblemployeeID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemployeeID2h.Text;
                //else if (lblcontactID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcontactID2h.Text;
                else if (lbluserID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtuserID2h.Text;
                //else if (lblActiveDirectoryID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActiveDirectoryID2h.Text;
                else if (lblMainHRRoleID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMainHRRoleID2h.Text;
                else if (lbllastname2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtlastname2h.Text;
                else if (lblfirstname2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtfirstname2h.Text;
                //else if (lblmiddle_name2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtmiddle_name2h.Text;
                //else if (lblemp_nick_name2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_nick_name2h.Text;
                else if (lblname22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtname22h.Text;
                else if (lblStuden_LoginID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStuden_LoginID2h.Text;
                else if (lblPASSWORD2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPASSWORD2h.Text;
                //else if (lblemp_smoker2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_smoker2h.Text;
                //else if (lblethnic_race_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtethnic_race_code2h.Text;
                //else if (lblemp_birthday2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_birthday2h.Text;
                //else if (lblnation_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtnation_code2h.Text;
                //else if (lblemp_gender2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_gender2h.Text;
                //else if (lblemp_marital_status2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_marital_status2h.Text;
                //else if (lblemp_ssn_num2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_ssn_num2h.Text;
                //else if (lblemp_sin_num2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_sin_num2h.Text;
                //else if (lblemp_other_id2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_other_id2h.Text;
                //else if (lblemp_dri_lice_num2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_dri_lice_num2h.Text;
                //else if (lblemp_dri_lice_exp_date2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_dri_lice_exp_date2h.Text;
                //else if (lblemp_military_service2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_military_service2h.Text;
                //else if (lblemp_status2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_status2h.Text;
                //else if (lblEmployeeType2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtEmployeeType2h.Text;
                //else if (lbljob_title_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtjob_title_code2h.Text;
                //else if (lbleeo_cat_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txteeo_cat_code2h.Text;
                //else if (lblwork_station2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtwork_station2h.Text;
                //else if (lblemp_street12h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_street12h.Text;
                //else if (lblemp_street22h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_street22h.Text;
                //else if (lblcity_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcity_code2h.Text;
                //else if (lblcoun_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtcoun_code2h.Text;
                //else if (lblprovin_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtprovin_code2h.Text;
                //else if (lblemp_zipcode2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_zipcode2h.Text;
                //else if (lblemp_hm_telephone2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_hm_telephone2h.Text;
                //else if (lblemp_mobile2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_mobile2h.Text;
                //else if (lblemp_work_telephone2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_work_telephone2h.Text;
                //else if (lblemp_work_email2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_work_email2h.Text;
                //else if (lblsal_grd_code2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtsal_grd_code2h.Text;
                //else if (lbljoined_date2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtjoined_date2h.Text;
                //else if (lblemp_oth_email2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtemp_oth_email2h.Text;
                //else if (lbltermination_id2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txttermination_id2h.Text;
                //else if (lblActive2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;
                //else if (lblDateTime2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDateTime2h.Text;
                //else if (lblDeviceID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeviceID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tbl_Employee.xml"));

        }

        public void ManageLang()
        {
            //for Language

            if (Session["LANGUAGE"] != null)
            {
                RecieveLabel(Session["LANGUAGE"].ToString());
                if (Session["LANGUAGE"].ToString() == "ar-KW")
                    GetHide();
                else
                    GetShow();
            }

        }
        protected void LanguageFrance_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
        }
        protected void LanguageArabic_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }
        protected void LanguageEnglish_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }
        public void Write()
        {
            // navigation.Visible = false;
            //drplocation.Enabled = true;
            // txtemployeeID.Enabled = true;
            // txtcontactID.Enabled = true;
            txtMiddlename.Enabled = true;
            //txtActiveDirectoryID.Enabled = true;
            ISSuper.Enabled = true;
            txtlastname.Enabled = true;
            txtfirstname.Enabled = true;
            //txtmiddle_name.Enabled = true;
            //txtemp_nick_name.Enabled = true;
            drpvaliduser.Enabled = true;
            txtStuden_LoginID.Enabled = true;
            txtPASSWORD.Enabled = true;
            //txtemp_smoker.Enabled = true;
            //txtethnic_race_code.Enabled = true;
            //txtemp_birthday.Enabled = true;
            //txtnation_code.Enabled = true;
            //txtemp_gender.Enabled = true;
            //txtemp_marital_status.Enabled = true;
            //txtemp_ssn_num.Enabled = true;
            //txtemp_sin_num.Enabled = true;
            //txtemp_other_id.Enabled = true;
            //txtemp_dri_lice_num.Enabled = true;
            //txtemp_dri_lice_exp_date.Enabled = true;
            //txtemp_military_service.Enabled = true;
            //txtemp_status.Enabled = true;
            //drpEmployeeType.Enabled = true;
            //txtjob_title_code.Enabled = true;
            //txteeo_cat_code.Enabled = true;
            //txtwork_station.Enabled = true;
            //txtemp_street1.Enabled = true;
            //txtemp_street2.Enabled = true;
            //txtcity_code.Enabled = true;
            //txtcoun_code.Enabled = true;
            //txtprovin_code.Enabled = true;
            //txtemp_zipcode.Enabled = true;
            //txtemp_hm_telephone.Enabled = true;
            txtemp_mobile.Enabled = true;
            //txtemp_work_telephone.Enabled = true;
            //txtemp_work_email.Enabled = true;
            //txtsal_grd_code.Enabled = true;
            //txtjoined_date.Enabled = true;
            //txtemp_oth_email.Enabled = true;
            //drptermination_id.Enabled = true;
            //txtcustom1.Enabled = true;
            //txtcustom2.Enabled = true;
            //txtcustom3.Enabled = true;
            //txtcustom4.Enabled = true;
            //txtcustom5.Enabled = true;
            //txtcustom6.Enabled = true;
            //txtcustom7.Enabled = true;
            //txtcustom8.Enabled = true;
            //txtcustom9.Enabled = true;
            //txtcustom10.Enabled = true;
            //  cbActive.Enabled = true;
            //cbDeleted.Enabled = true;
            //txtDateTime.Enabled = true;
            //   txtDeviceID.Enabled = true;

        }
        public void Readonly()
        {
            //  navigation.Visible = true;
            //drplocation.Enabled = false;
            // txtemployeeID.Enabled = false;
            // txtcontactID.Enabled = false;
            txtMiddlename.Enabled = false;
            //txtActiveDirectoryID.Enabled = false;
            ISSuper.Enabled = false;
            txtlastname.Enabled = false;
            txtfirstname.Enabled = false;
            //txtmiddle_name.Enabled = false;
            //txtemp_nick_name.Enabled = false;
            drpvaliduser.Enabled = false;
            txtStuden_LoginID.Enabled = false;
            txtPASSWORD.Enabled = false;
            //txtemp_smoker.Enabled = false;
            //txtethnic_race_code.Enabled = false;
            //txtemp_birthday.Enabled = false;
            //txtnation_code.Enabled = false;
            //txtemp_gender.Enabled = false;
            //txtemp_marital_status.Enabled = false;
            //txtemp_ssn_num.Enabled = false;
            //txtemp_sin_num.Enabled = false;
            //txtemp_other_id.Enabled = false;
            //txtemp_dri_lice_num.Enabled = false;
            //txtemp_dri_lice_exp_date.Enabled = false;
            //txtemp_military_service.Enabled = false;
            //txtemp_status.Enabled = false;
            //drpEmployeeType.Enabled = false;
            //txtjob_title_code.Enabled = false;
            //txteeo_cat_code.Enabled = false;
            //txtwork_station.Enabled = false;
            //txtemp_street1.Enabled = false;
            //txtemp_street2.Enabled = false;
            //txtcity_code.Enabled = false;
            //txtcoun_code.Enabled = false;
            //txtprovin_code.Enabled = false;
            //txtemp_zipcode.Enabled = false;
            //txtemp_hm_telephone.Enabled = false;
            txtemp_mobile.Enabled = false;
            //txtemp_work_telephone.Enabled = false;
            //txtemp_work_email.Enabled = false;
            //txtsal_grd_code.Enabled = false;
            //txtjoined_date.Enabled = false;
            //txtemp_oth_email.Enabled = false;
            //txttermination_id.Enabled = false;
            //txtcustom1.Enabled = false;
            //txtcustom2.Enabled = false;
            //txtcustom3.Enabled = false;
            //txtcustom4.Enabled = false;
            //txtcustom5.Enabled = false;
            //txtcustom6.Enabled = false;
            //txtcustom7.Enabled = false;
            //txtcustom8.Enabled = false;
            //txtcustom9.Enabled = false;
            //txtcustom10.Enabled = false;
            //cbActive.Enabled = false;
            //  cbDeleted.Enabled = false;
            //txtDateTime.Enabled = false;
            //  txtDeviceID.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Employee.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Employee.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.employeeID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    if (take == Totalrec && Skip == (Totalrec - Showdata))
        //        btnNext1.Enabled = false;
        //    else
        //        btnNext1.Enabled = true;
        //    if (take == Showdata && Skip == 0)
        //        btnPrevious1.Enabled = false;
        //    else
        //        btnPrevious1.Enabled = true;

        //    ChoiceID = take / Showdata;

        //    //  ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //        int Totalrec = DB.tbl_Employee.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Employee.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.employeeID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        if (take == Showdata && Skip == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;

        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;

        //        ChoiceID = take / Showdata;
        //        //  ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //        int Totalrec = DB.tbl_Employee.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Employee.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.employeeID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        //   ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Employee.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Employee.Where(m => m.TenentID == TID && m.Deleted == false).OrderBy(m => m.employeeID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    // ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            Readonly();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            //FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                if (e.CommandName == "btnDelete")
                {

                    int ID = Convert.ToInt32(e.CommandArgument);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    Database.tbl_Employee objSOJobDesc = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID);
                    DB.tbl_Employee.DeleteObject(objSOJobDesc);
                    DB.SaveChanges();
                    BindData();

                }

                if (e.CommandName == "btnEdit")
                {

                    int ID = Convert.ToInt32(e.CommandArgument);
                    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                    Database.tbl_Employee objtbl_Employee = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID);

                    txtlastname.Text = objtbl_Employee.lastname.ToString();
                    txtfirstname.Text = objtbl_Employee.firstname.ToString();                    
                    txtMiddlename.Text = objtbl_Employee.middle_name.ToString();
                    txtemp_mobile.Text = objtbl_Employee.emp_mobile.ToString();
                    drpvaliduser.SelectedValue = objtbl_Employee.userID.ToString();                    
                    if (objtbl_Employee.MainHRRoleID == 1)
                    {
                        ISSuper.Checked = true;
                    }
                    else
                    {
                        ISSuper.Checked = false;
                    }

                    btnAdd.Text = "Update";
                    ViewState["Edit"] = ID;
                    Write();
                }
                scope.Complete(); //  To commit.

            }
        }

        public string GetSuper(int ID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if(DB.USER_MST.Where(p=>p.TenentID == TID && p.USER_ID == ID).Count() > 0)
            {
                string Name = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == ID).FIRST_NAME;
                return Name;
            }
            else
            {
                return "Not Found";
            }
        }
        public string getISSUP(string ID)
        {
            if (ID == "1")
                return "YES";
            else
                return "NO";
        }
        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Employee.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Employee.Where(m => m.TenentID == TID).OrderBy(m => m.employeeID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        //  ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (Tvalue == Showdata && Svalue == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //    }
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        //}

        //protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        //protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
        //    ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
        //    control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        //}
        #endregion

    }
}