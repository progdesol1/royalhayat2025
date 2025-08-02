using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.CRM
{
    public partial class UserMaster : System.Web.UI.Page
    {
       
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                FillContractorID();
                BindData();
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    Database.USER_MST objtbl_CRMActivities = DB.USER_MST.Single(p => p.USER_ID == ID && p.TenentID == TID);
                    //Server Content Recived data Yogesh


                }
            }

        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
        public void BindData()
        {
            listUserMaster.DataSource = DB.USER_MST.Where(p => p.TenentID == TID && p.ACTIVE_FLAG == "Y");
            listUserMaster.DataBind();

            Classes.EcommAdminClass.getdropdown(drpUsertype, TID, "", "", "", "ROLE_MST");
            //select * from ROLE_MST where TenentID = 0

            //drpUsertype.DataSource = DB.ROLE_MST.Where(p => p.ACTIVE_FLAG == "Y");
            //drpUsertype.DataTextField = "ROLE_NAME";
            //drpUsertype.DataValueField = "ROLE_ID";
            //drpUsertype.DataBind();
            //drpUsertype.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void ListUserMaster_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {
                int USER_ID = Convert.ToInt32(Request.QueryString["USER_ID"]);
                Database.USER_MST objtbl_UserMst = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == USER_ID);
                // txtUserId.Text = objtbl_UserMst.USER_ID.ToString();
                txtFirsName.Text = objtbl_UserMst.FIRST_NAME.ToString();
                txtlastname.Text = objtbl_UserMst.LAST_NAME.ToString();
                txtFirsName1.Text = objtbl_UserMst.FIRST_NAME1.ToString();
                txtlastname1.Text = objtbl_UserMst.LAST_NAME1.ToString();
                txtFirstName2.Text = objtbl_UserMst.FIRST_NAME2.ToString();
                txtLastName2.Text = objtbl_UserMst.LAST_NAME2.ToString();
                txtloginid.Text = objtbl_UserMst.LOGIN_ID.ToString();
                txtpassword.Text = objtbl_UserMst.PASSWORD.ToString();
                drpUsertype.SelectedValue = objtbl_UserMst.USER_TYPE.ToString();
                txtRemark.Text = objtbl_UserMst.REMARKS.ToString();
                ckbActiveFlag.Checked = true;
                txtUserId.Enabled = false;

                //ckbAccLock.Checked = true;
                //ckbFirsttime.Checked = true;
                //txtPasswordChng.Text = objtbl_UserMst.PASSWORD_CHNG.ToString();
                //txtThemeName.Text = objtbl_UserMst.THEME_NAME.ToString();
                //txtApprovalDate.Text = objtbl_UserMst.APPROVAL_DT.ToString();
                //txtVerification.Text = objtbl_UserMst.VERIFICATION_CD.ToString();
                txtTilldate.Text = objtbl_UserMst.Till_DT.ToString();
                txtEmailAddres.Text = objtbl_UserMst.EmailAddress.ToString();
                txtlastlogin.Text = objtbl_UserMst.LAST_LOGIN_DT.ToString();
                btnSubmit.Text = "Update";
                btnSubmit.Visible = true;
                // txtUserId.Enabled = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["USER_ID"]!=null)
            {
                int USER_ID = Convert.ToInt32(Request.QueryString["USER_ID"]);
                Database.USER_MST objtbl_UserMst = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == USER_ID);
                //  objtbl_UserMst.USER_ID = Convert.ToInt32(txtUserId.Text);
                objtbl_UserMst.FIRST_NAME = txtFirsName.Text;
                objtbl_UserMst.LAST_NAME = txtlastname.Text;
                objtbl_UserMst.FIRST_NAME1 = txtFirsName1.Text;
                objtbl_UserMst.LAST_NAME1 = txtlastname1.Text;
                objtbl_UserMst.FIRST_NAME2 = txtFirstName2.Text;
                objtbl_UserMst.LAST_NAME2 = txtLastName2.Text;
                objtbl_UserMst.LOGIN_ID = txtloginid.Text;
                objtbl_UserMst.PASSWORD = txtpassword.Text;
                objtbl_UserMst.USER_TYPE = Convert.ToInt32(drpUsertype.SelectedValue); 
                objtbl_UserMst.REMARKS = txtRemark.Text;
                objtbl_UserMst.ACTIVE_FLAG = ckbActiveFlag.Checked ? "Y" : "N";
                objtbl_UserMst.LAST_LOGIN_DT = Convert.ToDateTime(txtlastlogin.Text);
                if (FUDoc.HasFile)
                {

                    FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                    objtbl_UserMst.SignatureImage = FUDoc.FileName;
                    objtbl_UserMst.IsSignature = true;
                }
                //objtbl_UserMst.USER_DETAIL_ID = Convert.ToInt32(drpUserdetal.SelectedValue);
                //objtbl_UserMst.ACC_LOCK = ckbAccLock.Checked ? "Y" : "N";
                objtbl_UserMst.FIRST_TIME = ckbFirsttime.Checked ? "Y" : "N";
                objtbl_UserMst.PASSWORD_CHNG = txtPasswordChng.Text;
                objtbl_UserMst.THEME_NAME = txtThemeName.Text;
                //objtbl_UserMst.APPROVAL_DT = Convert.ToDateTime(txtApprovalDate.Text);
                objtbl_UserMst.VERIFICATION_CD = txtVerification.Text;
                objtbl_UserMst.Till_DT = Convert.ToDateTime(txtTilldate.Text);
                objtbl_UserMst.EmailAddress = txtEmailAddres.Text;

                //((DMSMaster)Page.Master).UpdateLog("From User Master for USER_MST,ID:" + USER_ID.ToString(), objtbl_UserMst.CRUP_ID, "USER_MST", "");
                // lblMsg.Text = "Data Edit Successfully";
                //pnlSuccessMsg.Visible = true;
                btnSubmit.Text = "Add";
            }

            else
            {
                Database.USER_MST objtbl_UserMst = new Database.USER_MST();
                //objtbl_UserMst.USER_ID = Convert.ToInt32(txtUserId.Text);
                objtbl_UserMst.TenentID = TID;
                objtbl_UserMst.FIRST_NAME = txtFirsName.Text;
                objtbl_UserMst.LAST_NAME = txtlastname.Text;
                objtbl_UserMst.FIRST_NAME1 = txtFirsName1.Text;
                objtbl_UserMst.LAST_NAME1 = txtlastname1.Text;
                objtbl_UserMst.FIRST_NAME2 = txtFirstName2.Text;
                objtbl_UserMst.LAST_NAME2 = txtLastName2.Text;
                objtbl_UserMst.LOGIN_ID = txtloginid.Text;
                objtbl_UserMst.PASSWORD = txtpassword.Text;
                objtbl_UserMst.USER_TYPE = 1;
                objtbl_UserMst.REMARKS = txtRemark.Text;
                objtbl_UserMst.ACTIVE_FLAG = ckbActiveFlag.Checked ? "Y" : "N";
                objtbl_UserMst.LAST_LOGIN_DT = Convert.ToDateTime(txtlastlogin.Text);
                //objtbl_UserMst.USER_DETAIL_ID = Convert.ToInt32(drpUserdetal.SelectedValue);
                if (FUDoc.HasFile)
                {

                    FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                    objtbl_UserMst.SignatureImage = FUDoc.FileName;
                    objtbl_UserMst.IsSignature = true;
                }
                objtbl_UserMst.ACC_LOCK = "N";
                objtbl_UserMst.FIRST_TIME = ckbFirsttime.Checked ? "Y" : "N";
                objtbl_UserMst.PASSWORD_CHNG = txtPasswordChng.Text;
                objtbl_UserMst.THEME_NAME = txtThemeName.Text;
                objtbl_UserMst.APPROVAL_DT = Convert.ToDateTime(txtApprovalDate.Text);
                objtbl_UserMst.VERIFICATION_CD = txtVerification.Text;
                objtbl_UserMst.Till_DT = Convert.ToDateTime(txtTilldate.Text);
                objtbl_UserMst.EmailAddress = txtEmailAddres.Text;
                //  objtbl_UserMst.CRUP_ID = ((DMSMaster)Page.Master).WriteLog("From User Master for USER_MST,ID:" + objtbl_UserMst.TenentID.ToString(), "From User Master for USER_MST,ID:" + objtbl_UserMst.USER_ID.ToString(), "USER_MST", "");

                DB.USER_MST.AddObject(objtbl_UserMst);
                // lblMsg.Text = "Data Save Successfully";
                // pnlSuccessMsg.Visible = true;
            }
            DB.SaveChanges();
            // CompanySetup();
            BindData();


        }
        //public void CompanySetup()
        //{
        //    int USER_ID = Convert.ToInt32("USER_ID");
        //    if (Session["Common"] != null)
        //    {
        //        string[] CommonList1 = Session["Common"].ToString().Split(',');
        //    }
        //    Database.TBLCOMPANYSETUP objtbl_COMPANYSETUP = new Database.TBLCOMPANYSETUP();
        //    string[] CommonList2 = Session["Common"].ToString().Split(',');
        //    objtbl_COMPANYSETUP.TenantID = TID;
        //    objtbl_COMPANYSETUP.USERID = ViewState["USER_ID"].ToString();


        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMaster.aspx");
        }
        public void FillContractorID()
        {

        }
    }
}