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
using Database;
using System.Transactions;

namespace Web.CRM
{
    public partial class CRMActivities : System.Web.UI.Page
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
                BindData();
                FillContractorID();
                //if (Request.QueryString.Count > 0)
                //{
                //    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                //   // Database.CRMActivity objtbl_CRMActivities = DB.CRMActivities.Single(p => p.COMPID == ID && p.TenentID == TID);
                //    //Server Content Recived data Yogesh
                //}   
                if (Request.QueryString.Count == null)
                {

                }
                else
                {
                    //if (DB.tbl_DMSroute.Count() > 0)
                    //{
                    //    var objtbl_CRMActivities = DB.CRMActivities.FirstOrDefault();
                    //    drpComid.SelectedValue = objtbl_CRMActivities.COMPID.ToString();
                    //    drpActivitycode.SelectedValue = objtbl_CRMActivities.ActivityPerform.ToString();
                    //    drpActivitytype.SelectedValue = objtbl_CRMActivities.ACTIVITYTYPE.ToString();
                    //    drpReftype.SelectedValue = objtbl_CRMActivities.REFTYPE.ToString();
                    //    drpRefsubtype.SelectedValue = objtbl_CRMActivities.REFSUBTYPE.ToString();
                    //    txtMylineno.Text = objtbl_CRMActivities.MyLineNo.ToString();
                    //    txtUSERCODE.Text = objtbl_CRMActivities.USERCODE.ToString();
                    //    txtReferenceNo.Text = objtbl_CRMActivities.ReferenceNo.ToString();
                    //    txtEarlierRefNo.Text = objtbl_CRMActivities.EarlierRefNo.ToString();
                    //    txtNextUser.Text = objtbl_CRMActivities.NextUser.ToString();
                    //    txtNextRefNo.Text = objtbl_CRMActivities.NextRefNo.ToString();
                    //    ckbAmiglobal.Checked = objtbl_CRMActivities.AMIGLOBAL == "Y" ? true : false;
                    //    ckbMypersonnel.Checked = objtbl_CRMActivities.MYPERSONNEL == "Y" ? true : false;
                    //    txtActivityPerform.Text = objtbl_CRMActivities.ActivityPerform.ToString();
                    //    txtREMINDERNOTE.Text = objtbl_CRMActivities.REMINDERNOTE.ToString();
                    //    txtESTCOST.Text = objtbl_CRMActivities.ESTCOST.ToString();
                    //    txtGROUPCODE.Text = objtbl_CRMActivities.GROUPCODE.ToString();
                    //    txtUSERCODEENTERED.Text = objtbl_CRMActivities.USERCODEENTERED.ToString();
                    //    txtUPDTTIME.Text = objtbl_CRMActivities.UPDTTIME.ToString();
                    //    txtUSERNAME.Text = objtbl_CRMActivities.USERNAME.ToString();
                    //    txtUSERNAME.Text = objtbl_CRMActivities.USERNAME.ToString();
                    //    txtInitialDate.Text = objtbl_CRMActivities.InitialDate.ToString();
                    //    txtDeadLineDate.Text = objtbl_CRMActivities.DeadLineDate.ToString();
                    //    drpReftype.Enabled = false;
                    //    drpRefsubtype.Enabled = false;
                    //}
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
            liscrmactivities.DataSource = DB.CRMActivities.Where(p=> p.TenentID==TID);
            liscrmactivities.DataBind();
        }
        protected void Listcrmactivities_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                //try
                //{
                    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                    if (e.CommandName == "btnDelete")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.CRMActivity objtbl_CRMActivities = DB.CRMActivities.Single(p => p.COMPID == ID && p.TenentID == TID);

                        //  objtbl_CRMActivities.Deleted = false;
                        // ((DMSMaster)Page.Master).UpdateLog("From CRM Activities for CRMActivity,ID:" + objtbl_CRMActivities.MyLineNo.ToString() + objtbl_CRMActivities.ReferenceNo.ToString ()+objtbl_CRMActivities .RouteID .ToString (), Convert.ToInt32(objtbl_CRMActivities.CRUP_ID ), "CRMActivity", UID);
                        DB.SaveChanges();
                        BindData();


                    }
                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.CRMActivity objtbl_CRMActivities = DB.CRMActivities.Single(p => p.COMPID == ID && p.TenentID == TID);
                        //Server Content Recived data Yogesh

                        ViewState["Edit"] = ID;
                        btnAdd.Text = "Update";
                    }

                    scope.Complete(); //  To commit.
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRMActivities.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                //try
                //{
                    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                    if (ViewState["Edit"] != null)
                    {
                        string[] str = txtInitialDate.Text.Split('/');
                        string[] str1 = txtDeadLineDate.Text.Split('/');
                        int postday = Convert.ToInt32(str[0]);
                        int postMonth = Convert.ToInt32(str[1]);
                        int postYear = Convert.ToInt32(str[2]);
                        DateTime Fdate = Convert.ToDateTime(postday + "/" + postMonth + "/" + postYear);
                        int postday1 = Convert.ToInt32(str1[0]);
                        int postMonth1 = Convert.ToInt32(str1[1]);
                        int postYear1 = Convert.ToInt32(str1[2]);
                        DateTime Tdate = Convert.ToDateTime(postday1 + "/" + postMonth1 + "/" + postYear1);

                        if (Tdate.Date < Fdate.Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Deadline Date should be grater than From date');", true);
                            return;
                        }
                        if (Fdate.Date < DateTime.Now.Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Initial Date should be Less than Today date');", true);
                            return;
                        }
                        if (Tdate.Date < DateTime.Now.Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Deadline Date should be Less than Today date');", true);
                            return;
                        }


                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        Database.CRMActivity objtbl_CRMActivities = DB.CRMActivities.Single(p => p.COMPID == ID && p.TenentID == TID);

                        if (Convert.ToInt32(drpComid.SelectedValue)!=0)
                        {
                            objtbl_CRMActivities.COMPID = Convert.ToInt32(drpComid.SelectedValue);
                        }
                        //if (Convert.ToInt32(drpActivitycode.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMActivities.ACTIVITYCODE = Convert.ToInt32(drpActivitycode.SelectedValue);
                        //}
                        if (Convert.ToInt32(drpActivitytype.SelectedValue) != 0)
                        {
                            objtbl_CRMActivities.ACTIVITYTYPE = drpActivitytype.SelectedValue;
                        }
                        if (Convert.ToInt32(drpReftype.SelectedValue) != 0)
                        {
                            objtbl_CRMActivities.REFTYPE = drpReftype.SelectedValue;
                        }
                        if (Convert.ToInt32(drpRefsubtype.SelectedValue) != 0)
                        {
                            objtbl_CRMActivities.REFSUBTYPE = drpRefsubtype.SelectedValue;
                        }
                        if (txtMylineno.Text!="")
                        {
                            objtbl_CRMActivities.MyLineNo = Convert.ToInt32(txtMylineno.Text);
                        }
                        objtbl_CRMActivities.EarlierRefNo = txtEarlierRefNo.Text;
                        objtbl_CRMActivities.NextUser = txtNextUser.Text;
                        objtbl_CRMActivities.NextRefNo = txtNextRefNo.Text;
                        objtbl_CRMActivities.AMIGLOBAL = ckbAmiglobal.Checked ? "Y" : "N";
                        objtbl_CRMActivities.MYPERSONNEL = ckbMypersonnel.Checked ? "Y" : "N";
                        objtbl_CRMActivities.ActivityPerform = txtActivityPerform.Text;
                        objtbl_CRMActivities.REMINDERNOTE = txtREMINDERNOTE.Text;
                        objtbl_CRMActivities.ESTCOST = Convert.ToInt32(txtESTCOST.Text);
                        objtbl_CRMActivities.GROUPCODE = txtGROUPCODE.Text;
                        objtbl_CRMActivities.USERCODEENTERED = txtUSERCODEENTERED.Text;
                        objtbl_CRMActivities.UPDTTIME = Convert.ToDateTime(txtUPDTTIME.Text);
                        objtbl_CRMActivities.USERNAME = txtUSERNAME.Text;
                        objtbl_CRMActivities.InitialDate = Convert.ToDateTime(txtInitialDate.Text);
                        objtbl_CRMActivities.DeadLineDate = Convert.ToDateTime(txtDeadLineDate.Text);
                        ViewState["Edit"] = null;
                        btnAdd.Text = "Add New";
                        //       ((DMSMaster)Page.Master).UpdateLog("From CRM Activities for CRMActivity,ID:" + objtbl_CRMActivities.MyLineNo.ToString() + objtbl_CRMActivities.ReferenceNo.ToString() + objtbl_CRMActivities.RouteID.ToString(), Convert.ToInt32(objtbl_CRMActivities.CRUP_ID), "CRMActivity", UID);
                    }
                    else
                    {
                        Database.CRMActivity objtbl_CRMActivities = new Database.CRMActivity();
                        //Server Content Send data Yogesh
                        if (Convert.ToInt32(drpComid.SelectedValue) != 0)
                        {
                            objtbl_CRMActivities.COMPID = Convert.ToInt32(drpComid.SelectedValue);
                        }
                        //if (Convert.ToInt32(drpActivitycode.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMActivities.ACTIVITYCODE = Convert.ToInt32(drpActivitycode.SelectedValue);
                        //}
                        if (Convert.ToInt32(drpActivitytype.SelectedValue) != 0)
                        {
                            objtbl_CRMActivities.ACTIVITYTYPE = drpActivitytype.SelectedValue;
                        }
                        if (Convert.ToInt32(drpReftype.SelectedValue) != 0)
                        {
                            objtbl_CRMActivities.REFTYPE = drpReftype.SelectedValue;
                        }
                        //if (Convert.ToInt32(drpRefsubtype.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMActivities.REFSUBTYPE = drpRefsubtype.SelectedValue;
                        //}
                        if (txtMylineno.Text != "")
                        {
                            objtbl_CRMActivities.MyLineNo = Convert.ToInt32(txtMylineno.Text);
                        }
                        objtbl_CRMActivities.EarlierRefNo = txtEarlierRefNo.Text;
                        objtbl_CRMActivities.NextUser = txtNextUser.Text;
                        objtbl_CRMActivities.NextRefNo = txtNextRefNo.Text;
                        objtbl_CRMActivities.AMIGLOBAL = ckbAmiglobal.Checked ? "Y" : "N";
                        objtbl_CRMActivities.MYPERSONNEL = ckbMypersonnel.Checked ? "Y" : "N";
                        objtbl_CRMActivities.ActivityPerform = txtActivityPerform.Text;
                        objtbl_CRMActivities.REMINDERNOTE = txtREMINDERNOTE.Text;
                        objtbl_CRMActivities.ESTCOST = Convert.ToInt32(txtESTCOST.Text);
                        objtbl_CRMActivities.GROUPCODE = txtGROUPCODE.Text;
                        objtbl_CRMActivities.USERCODEENTERED = txtUSERCODEENTERED.Text;
                        objtbl_CRMActivities.UPDTTIME = Convert.ToDateTime(txtUPDTTIME.Text);
                        objtbl_CRMActivities.USERNAME = txtUSERNAME.Text;
                        objtbl_CRMActivities.ACTIVITYTYPE = "CRM";
                        objtbl_CRMActivities.REFTYPE = "CRM";
                        objtbl_CRMActivities.REFSUBTYPE = "ACTIVITY";
                        objtbl_CRMActivities.PerfReferenceNo = "99999";
                        objtbl_CRMActivities.InitialDate = Convert.ToDateTime(txtInitialDate.Text);
                        objtbl_CRMActivities.DeadLineDate = Convert.ToDateTime(txtDeadLineDate.Text);
                        objtbl_CRMActivities.RouteID = "1";
                        objtbl_CRMActivities.Active = "Y";
                        //       objtbl_CRMActivities.CRUP_ID = ((DMSMaster)Page.Master).WriteLog("From CRM Activities for CRMActivities,ID:" + objtbl_CRMActivities.MyLineNo.ToString() + objtbl_CRMActivities.ReferenceNo.ToString() + objtbl_CRMActivities.RouteID.ToString(), "From CRM Activities for CRMActivities,ID:", "CRMActivities", UID);
                        DB.CRMActivities.AddObject(objtbl_CRMActivities);
                        btnSubmit.Visible = false;
                        btnAdd.Visible = true;
                    }
                    DB.SaveChanges();
                    scope.Complete(); //  To commit.
                }
                //catch (Exception ex)
                //{
                //    throw;
                //}
            //}

            //Response.Redirect("CRMActivitiesList.aspx");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRMActivities.aspx");
        }
        //public string getActivity(int COMPID)
        //{
        //    return DB.CRM_TBLCOMPANYSETUP.Single(p => p.COMPID == COMPID).PHYSICALLOCID;


        //}
        //public string getCompanyName(int COMPID)
        //{
        //    return DB.CRM_TBLCOMPANYSETUP.Single(p => p.COMPID == COMPID).COMPNAME;


        //}
        public void FillContractorID()
        {
            Classes.EcommAdminClass.getdropdown(drpComid, TID, "HLY", "", "", "TBLCOMPANYSETUP");
            //select * from TBLCOMPANYSETUP where TenentID = 1 and PHYSICALLOCID = 'HLY'

            //drpComid.DataSource = DB.TBLCOMPANYSETUPs.Where(P => P.TenentID == TID).OrderBy(a => a.COMPNAME);
            //drpComid.DataTextField = "COMPNAME";
            //drpComid.DataValueField = "CompID";
            //drpComid.DataBind();
            //drpComid.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpReftype, TID, "CRM", "REFERENCE", "", "REFTABLE");
            //select * from REFTABLE where TenentID = 1 and REFTYPE = 'CRM' and REFSUBTYPE = 'REFERENCE'

            //drpReftype.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "REFERENCE" && P.TenentID == TID).OrderBy(a => a.REFTYPE);
            //drpReftype.DataTextField = "REFTYPE";
            //drpReftype.DataValueField = "REFID";
            //drpReftype.DataBind();
            //drpReftype.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpActivitycode.DataSource = DB.CRM_TBLCOMPANYSETUP.Where(P => P.TenentID == TID).OrderBy(a => a.PHYSICALLOCID);
            //drpActivitycode.DataTextField = "PHYSICALLOCID";
            //drpActivitycode.DataValueField = "COMPID";
            //drpActivitycode.DataBind();
            //drpActivitycode.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpActivitytype, TID, "ACTVTY", "ACTVTY", "", "REFTABLE");
            //select * from REFTABLE where TenentID = 1 and REFTYPE = 'ACTVTY' and REFSUBTYPE = 'ACTVTY'

            //drpActivitytype.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "ACTVTY" && P.REFSUBTYPE == "ACTVTY" && P.TenentID == TID).OrderBy(a => a.REFNAME1);
            //drpActivitytype.DataTextField = "REFNAME1";
            //drpActivitytype.DataValueField = "REFID";
            //drpActivitytype.DataBind();
            //drpActivitytype.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpRefsubtype, TID, "CRM", "DOCTYPE", "", "REFTABLE");
            //select * from REFTABLE where TenentID = 1 and REFTYPE = 'CRM' and REFSUBTYPE = 'DOCTYPE'

            //drpRefsubtype.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "DOCTYPE" && P.TenentID == TID).OrderBy(a => a.REFSUBTYPE);
            //drpRefsubtype.DataTextField = "REFSUBTYPE";
            //drpRefsubtype.DataValueField = "REFID";
            //drpRefsubtype.DataBind();
            //drpRefsubtype.Items.Insert(0, new ListItem("-- Select --", "0"));



        }
    }
}