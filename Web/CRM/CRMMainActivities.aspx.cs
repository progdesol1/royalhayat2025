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
    public partial class CRMMainActivities : System.Web.UI.Page
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
                //if (Request.QueryString.Count > 0)
                //{
                //    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                //    Database.CRMMainActivity objtbl_CRMMainActivities = DB.CRMMainActivities.Single(p => p.COMPID == ID && p.TenentID ==TID );
                //Server Content Recived data Yogesh
                if (Request.QueryString.Count == null)
                {

                }
                else
                {
                    //if (DB.tbl_DMSroute.Count() > 0)
                    //{
                    //    var objtbl_CRMMainActivities = DB.CRMMainActivities.FirstOrDefault();
                    //    drpComid.Text = objtbl_CRMMainActivities.COMPID.ToString();
                    //    drpActivitycode.Text = objtbl_CRMMainActivities.ACTIVITYCODE.ToString();
                    //    drpReftype.Text = objtbl_CRMMainActivities.REFTYPE.ToString();
                    //    drpRefsubtype.Text = objtbl_CRMMainActivities.REFSUBTYPE.ToString();
                    //    drpActivitytype.Text = objtbl_CRMMainActivities.ACTIVITYTYPE.ToString();
                    //    txtUSERCODE.Text = objtbl_CRMMainActivities.USERCODE.ToString();
                    //    txtACTIVITYE.Text = objtbl_CRMMainActivities.ACTIVITYE.ToString();
                    //    txtACTIVITYA.Text = objtbl_CRMMainActivities.ACTIVITYA.ToString();
                    //    txtACTIVITYA2.Text = objtbl_CRMMainActivities.ACTIVITYA2.ToString();
                    //    txtReference.Text = objtbl_CRMMainActivities.Reference.ToString();
                    //    ckbAmiglobal.Checked = objtbl_CRMMainActivities.AMIGLOBAL == "Y" ? true : false;
                    //    ckbmyPersonnel.Checked = objtbl_CRMMainActivities.MYPERSONNEL == "Y" ? true : false;
                    //    drpINTERVALDAYS.SelectedValue = objtbl_CRMMainActivities.INTERVALDAYS.ToString();
                    //    ckbRepeatforever.Checked = objtbl_CRMMainActivities.REPEATFOREVER == "Y" ? true : false;
                    //    txtREPEATTILL.Text = objtbl_CRMMainActivities.REPEATTILL.ToString();
                    //    txtREMINDERNOTE.Text = objtbl_CRMMainActivities.REMINDERNOTE.ToString();
                    //    txtESTCOST.Text = objtbl_CRMMainActivities.ESTCOST.ToString();
                    //    txtGROUPCODE.Text = objtbl_CRMMainActivities.GROUPCODE.ToString();
                    //    txtUSERCODEENTERED.Text = objtbl_CRMMainActivities.USERCODEENTERED.ToString();
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
            Listview1.DataSource = DB.CRMMainActivities;
            Listview1.DataBind();
        }
        protected void ListProduct_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.CRMMainActivity objtbl_CRMMainActivities = DB.CRMMainActivities.Single(p => p.MasterCODE == ID && p.TenentID == TID);
                drpComid.Text = objtbl_CRMMainActivities.COMPID.ToString();
                //   drpActivitycode.Text = objtbl_CRMMainActivities.ACTIVITYCODE.ToString();
                if (objtbl_CRMMainActivities.RouteID.ToString() != null)
                {
                    //drpReftype.Text = objtbl_CRMMainActivities.REFTYPE.ToString();
                    DrpRoute_Name.SelectedValue = objtbl_CRMMainActivities.RouteID.ToString();
                }
                if (objtbl_CRMMainActivities.GROUPCODE.ToString() != null)
                {
                    //drpReftype.Text = objtbl_CRMMainActivities.REFTYPE.ToString();
                    DrpGroupCode.SelectedValue = objtbl_CRMMainActivities.GROUPCODE.ToString();
                }
                if (objtbl_CRMMainActivities.USERCODE.ToString() != null)
                {
                    //drpReftype.Text = objtbl_CRMMainActivities.REFTYPE.ToString();
                    DrpUserCode.SelectedValue = objtbl_CRMMainActivities.USERCODE.ToString();
                }
                //if (objtbl_CRMMainActivities.REFSUBTYPE != null)
                //{
                //    drpRefsubtype.Text = objtbl_CRMMainActivities.REFSUBTYPE.ToString();
                //}
               
                // txtUSERCODE.Text = objtbl_CRMMainActivities.USERCODE.ToString();
                txtACTIVITYE.Text = objtbl_CRMMainActivities.ACTIVITYE.ToString();
                txtACTIVITYA.Text = objtbl_CRMMainActivities.ACTIVITYA.ToString();
                txtACTIVITYA2.Text = objtbl_CRMMainActivities.ACTIVITYA2.ToString();
                txtReference.Text = objtbl_CRMMainActivities.Reference.ToString();
                ckbAmiglobal.Checked = objtbl_CRMMainActivities.AMIGLOBAL == true ? true : false;
                ckbmyPersonnel.Checked = objtbl_CRMMainActivities.MYPERSONNEL == true ? true : false;
                //if (objtbl_CRMMainActivities.INTERVALDAYS != null)
                //{
                //    drpINTERVALDAYS.SelectedValue = objtbl_CRMMainActivities.INTERVALDAYS.ToString();
                //}

                ckbRepeatforever.Checked = objtbl_CRMMainActivities.REPEATFOREVER == true ? true : false;
                txtREPEATTILL.Text = objtbl_CRMMainActivities.REPEATTILL.ToString();
                txtREMINDERNOTE.Text = objtbl_CRMMainActivities.REMINDERNOTE.ToString();
                txtESTCOST.Text = objtbl_CRMMainActivities.ESTCOST.ToString();
                //    txtGROUPCODE.Text = objtbl_CRMMainActivities.GROUPCODE.ToString();
                txtUSERCODEENTERED.Text = objtbl_CRMMainActivities.USERCODEENTERED.ToString();
                ViewState["Edit"] = ID;
                btnAdd.Text = "Update";

            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRMMainActivities.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                    if (ViewState["Edit"] != null)
                    {
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        Database.CRMMainActivity objtbl_CRMMainActivities = DB.CRMMainActivities.Single(p => p.COMPID == ID && p.TenentID == TID);
                        if (Convert.ToInt32(drpComid.SelectedValue) != 0)
                        {
                            objtbl_CRMMainActivities.COMPID = Convert.ToInt32(drpComid.SelectedValue);
                        }
                        //if (Convert.ToInt32(drpActivitycode.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.ACTIVITYCODE = Convert.ToInt32(drpActivitycode.SelectedValue);
                        //}
                        //if (Convert.ToInt32(drpReftype.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.REFTYPE = drpReftype.Text;
                        //}
                        //if (Convert.ToInt32(drpRefsubtype.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.REFSUBTYPE = drpRefsubtype.Text;
                        //}
                        if (Convert.ToInt32(DrpUserCode.SelectedValue) != 0)
                        {
                            objtbl_CRMMainActivities.USERCODE = Convert.ToInt32(DrpUserCode.SelectedValue);
                        }
                        if (Convert.ToInt32(DrpRoute_Name.SelectedValue) != 0)
                        {
                            objtbl_CRMMainActivities.RouteID = Convert.ToInt32(DrpRoute_Name.SelectedValue);
                        }
                        objtbl_CRMMainActivities.USERNAME = UID;
                        // objtbl_CRMMainActivities.USERCODE = txtUSERCODE.Text;
                        objtbl_CRMMainActivities.ACTIVITYE = txtACTIVITYE.Text;
                        objtbl_CRMMainActivities.ACTIVITYA = txtACTIVITYA.Text;
                        objtbl_CRMMainActivities.ACTIVITYA2 = txtACTIVITYA2.Text;
                        objtbl_CRMMainActivities.Reference = txtReference.Text;
                        objtbl_CRMMainActivities.AMIGLOBAL = ckbAmiglobal.Checked ? false : true;
                        objtbl_CRMMainActivities.MYPERSONNEL = ckbmyPersonnel.Checked ? false : true;
                        //if (Convert.ToInt32(drpINTERVALDAYS.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.INTERVALDAYS = Convert.ToInt32(drpINTERVALDAYS.SelectedValue);
                        //}

                        objtbl_CRMMainActivities.REPEATFOREVER = ckbRepeatforever.Checked ? false : true;
                        objtbl_CRMMainActivities.REPEATTILL = Convert.ToDateTime(txtREPEATTILL.Text);
                        objtbl_CRMMainActivities.REMINDERNOTE = txtREMINDERNOTE.Text;
                        //objtbl_CRMMainActivities.ESTCOST = txtESTCOST.Text;
                        //   objtbl_CRMMainActivities.GROUPCODE = txtGROUPCODE.Text;
                        objtbl_CRMMainActivities.USERCODEENTERED = txtUSERCODEENTERED.Text;
                        //      ((DMSMaster)Page.Master).UpdateLog("From CRM MainActivities for CRMMainActivity,ID:" + objtbl_CRMMainActivities.COMPID.ToString() + objtbl_CRMMainActivities.ACTIVITYCODE.ToString() + objtbl_CRMMainActivities.RouteID.ToString() + objtbl_CRMMainActivities.Reference.ToString(), Convert.ToInt32(objtbl_CRMMainActivities.CRUP_ID), "CRMMainActivity", UID);

                    }
                    else
                    {
                        Database.CRMMainActivity objtbl_CRMMainActivities = new Database.CRMMainActivity();
                        //Server Content Send data Yogesh
                        if (Convert.ToInt32(drpComid.SelectedValue) != 0)
                        {
                            objtbl_CRMMainActivities.COMPID = Convert.ToInt32(drpComid.SelectedValue);
                        }
                        //if (Convert.ToInt32(drpActivitycode.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.ACTIVITYCODE = Convert.ToInt32(drpActivitycode.SelectedValue);
                        //}
                        //if (Convert.ToInt32(drpReftype.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.REFTYPE = drpReftype.Text;
                        //}
                        //if (Convert.ToInt32(drpRefsubtype.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.REFSUBTYPE = drpRefsubtype.Text;
                        //}
                        if (Convert.ToInt32(DrpUserCode.SelectedValue) != 0)
                        {
                            objtbl_CRMMainActivities.USERCODE = Convert.ToInt32(DrpUserCode.SelectedValue);
                        }
                        if (Convert.ToInt32(DrpRoute_Name.SelectedValue) != 0)
                        {
                            objtbl_CRMMainActivities.RouteID = Convert.ToInt32(DrpRoute_Name.SelectedValue);
                        }
                        //if (Convert.ToInt32(drpActivitytype.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.ACTIVITYTYPE = drpActivitytype.Text;
                        //}

                        //  objtbl_CRMMainActivities.USERCODE = txtUSERCODE.Text;
                        objtbl_CRMMainActivities.Prefix = "ONL";
                        objtbl_CRMMainActivities.ACTIVITYE = txtACTIVITYE.Text;
                        objtbl_CRMMainActivities.ACTIVITYA = txtACTIVITYA.Text;
                        objtbl_CRMMainActivities.ACTIVITYA2 = txtACTIVITYA2.Text;
                        objtbl_CRMMainActivities.Reference = txtReference.Text;
                        objtbl_CRMMainActivities.AMIGLOBAL = ckbAmiglobal.Checked ? false : true;
                        objtbl_CRMMainActivities.MYPERSONNEL = ckbmyPersonnel.Checked ? false : true;
                        objtbl_CRMMainActivities.USERNAME = UID;
                        //if (Convert.ToInt32(drpINTERVALDAYS.SelectedValue) != 0)
                        //{
                        //    objtbl_CRMMainActivities.INTERVALDAYS = Convert.ToInt32(drpINTERVALDAYS.SelectedValue);
                        //}
                        objtbl_CRMMainActivities.REPEATFOREVER = ckbRepeatforever.Checked ? false : true;
                        objtbl_CRMMainActivities.REPEATTILL = Convert.ToDateTime(txtREPEATTILL.Text);
                        objtbl_CRMMainActivities.REMINDERNOTE = txtREMINDERNOTE.Text;
                        //objtbl_CRMMainActivities.ESTCOST = txtESTCOST.Text;
                        // objtbl_CRMMainActivities.GROUPCODE = txtGROUPCODE.Text;
                        objtbl_CRMMainActivities.USERCODEENTERED = txtUSERCODEENTERED.Text;
                        objtbl_CRMMainActivities.RouteID = 1;
                       // objtbl_CRMMainActivities.ACTIVITYTYPE = "ACTIVITY";
                        //objtbl_CRMMainActivities.Active = true;
                        //objtbl_CRMMainActivities.DateCreated = DateTime.Now;
                        //objtbl_CRMMainActivities.Deleted = true;
                        DB.CRMMainActivities.AddObject(objtbl_CRMMainActivities);
                        btnSubmit.Visible = false;
                        btnAdd.Visible = true;
                    }
                    DB.SaveChanges();
                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            //Response.Redirect("CRMMainActivitiesList.aspx");
        }
        public void FillContractorID()
        {
            Classes.EcommAdminClass.getdropdown(drpComid, TID, "HLY", "", "", "TBLCOMPANYSETUP");
            //select * from TBLCOMPANYSETUP where TenentID = 1 and PHYSICALLOCID = 'HLY'

            //drpComid.DataSource = DB.TBLCOMPANYSETUPs.Where(P => P.TenentID == TID).OrderBy(a => a.COMPNAME);
            //drpComid.DataTextField = "COMPNAME";
            //drpComid.DataValueField = "CompID";
            //drpComid.DataBind();
            //drpComid.Items.Insert(0, new ListItem("-- Select --", "0"));

            var Data = (from item in DB.USER_MST.Where(p => p.TenentID == TID)
                        select new
                        {
                            Name = item.FIRST_NAME + " " + item.LAST_NAME,
                            ID = item.USER_ID
                        }).ToList();
            DrpUserCode.DataSource = Data;// DB.CRM_TBLCOMPANYSETUP.Where(P => P.TenentID == TID).OrderBy(a => a.COMPNAME);
            DrpUserCode.DataTextField = "Name";
            DrpUserCode.DataValueField = "ID";
            DrpUserCode.DataBind();
            DrpUserCode.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpReftype.DataSource = DB.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "REFERENCE" && P.TenentID == TID).OrderBy(a => a.REFTYPE);
            //drpReftype.DataTextField = "REFTYPE";
            //drpReftype.DataValueField = "REFID";
            //drpReftype.DataBind();
            //drpReftype.Items.Insert(0, new ListItem("-- Select --", "0"));
            //drpActivitycode.DataSource = DB.tbl_DMSdocroute.Where(P => P.Active == true && P.TenentID == TID).OrderBy(a => a.ACTIVITYCODE);
            //drpActivitycode.DataTextField = "ACTIVITYCODE";
            //drpActivitycode.DataValueField = "REFID";
            //drpActivitycode.DataBind();

            Classes.EcommAdminClass.getdropdown(DrpRoute_Name, TID, "", "", "", "Tbl_RouteMST");
            //select * from Tbl_RouteMST where TENANT_ID = 1 

            //DrpRoute_Name.DataSource = DB.Tbl_RouteMST.Where(P => P.TENANT_ID ==TID);
            //DrpRoute_Name.DataTextField = "Name1";
            //DrpRoute_Name.DataValueField = "ID";
            //DrpRoute_Name.DataBind();
            //DrpRoute_Name.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpActivitytype, TID, "ACTVTY", "ACTVTY", "", "REFTABLE");
            //select * from REFTABLE where TenentID = 1 and REFTYPE = 'ACTVTY' and REFSUBTYPE = 'ACTVTY'

            //drpActivitytype.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "ACTVTY" && P.REFSUBTYPE == "ACTVTY" && P.TenentID == TID).OrderBy(a => a.REFNAME1);
            //drpActivitytype.DataTextField = "REFNAME1";
            //drpActivitytype.DataValueField = "REFID";
            //drpActivitytype.DataBind();
            //drpActivitytype.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(DrpGroupCode, TID, "", "", "", "TBLGROUP");

            //DrpGroupCode.DataSource = DB2.TBLGROUPs.Where(p => p.GroupType == "CR");
            //DrpGroupCode.DataTextField = "ITGROUPDESC1";
            //DrpGroupCode.DataValueField = "ITGROUPID";
            //DrpGroupCode.DataBind();
            //DrpGroupCode.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpRefsubtype.DataSource = DB.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "DOCTYPE" && P.TenentID == TID).OrderBy(a => a.REFSUBTYPE);
            //drpRefsubtype.DataTextField = "REFSUBTYPE";
            //drpRefsubtype.DataValueField = "REFID";
            //drpRefsubtype.DataBind();
            //drpRefsubtype.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpINTERVALDAYS.DataSource = DB.REFTABLE.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "DOCTYPE" && P.TenentID == TID).OrderBy(a => a.REFSUBTYPE);
            //drpINTERVALDAYS.DataTextField = "REFSUBTYPE";
            //drpINTERVALDAYS.DataValueField = "REFID";
            //drpINTERVALDAYS.DataBind();
            //drpINTERVALDAYS.Items.Insert(0, new ListItem("-- Select --", "0"));



        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = true;
        }

        protected void ckbRepeatforever_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbRepeatforever.Checked==true)
            {
                pnlintervaldays.Visible = false;
            }
            else
            {
                pnlintervaldays.Visible = true;
            }
        }

    }
}