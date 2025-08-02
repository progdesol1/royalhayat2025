using System;
//using SaasDAL;
using System.Linq;
//using SaasBAL;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Linq.Expressions;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Database;
using System.Net;
namespace Web.CRM
{
    public partial class AdminRefTable : System.Web.UI.Page
    {
       // CallEntities DB1 = new CallEntities();
        CallEntities DB = new CallEntities();
       // CallEntities DB2 = new CallEntities();
        public static DataTable dt;
        string popupScript = "";
        string reftype, refSubtype;
        bool FirstFlag, ClickFlag = true;
        int TID,TTID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Session["Pagename"] = "AdminRefTable";
                bindSystem();
                panlRefEntery.Visible = false;
                panlListRef.Visible = false;
                panlRefAdmin.Visible = false;
                FillContractorID();
                // binddescri();
                //RefGridview.DataSource = ClearGrid();
                //RefGridview.DataBind();
            }
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TTID=TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();

        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void bindSystem()
        {
            try
            {
                //System.Data.DataTable dt = objCommonSelDel.CommonFillCondition("[dbo].[prc_acm_common_sel_con]", "DISTINCT", "MySysName", "dbo.RefTableAdmin", "", "");
                //if (dt.Rows.Count > 0)
                //{
                //ddlSystem.DataSource = dt;
                //ddlSystem.DataTextField = "MySysName";
                //ddlSystem.DataValueField = "MySysName";
                //ddlSystem.DataBind();
                //ddlSystem.Items.Insert(0, "-- Select --");
                //ddlSystem.SelectedIndex = -1;
                //}
                //else
                //{
                //ddlSystem.Items.Clear();
                //ddlSystem.Items.Insert(0, "-- Select --");
                //}
            }
            catch (Exception ex)
            {
                //  ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        public void binddescri(string system)
        {
            try
            {
                //System.Data.DataTable dt = objCommonSelDel.CommonFillCondition("[dbo].[prc_acm_common_sel_con]", "checked_Active", "Descrip", "dbo.RefTableAdmin", "MySysName", system);
                //if (dt.Rows.Count > 0)
                //{
                //ddldescrip.DataSource = dt;
                //ddldescrip.DataTextField = "Descrip";
                //ddldescrip.DataValueField = "Descrip";
                //ddldescrip.DataBind();
                //ddldescrip.Items.Insert(0, "-- Select --");
                //ddldescrip.SelectedIndex = -1;
                //}
                //else
                //{
                //ddldescrip.Items.Clear();
                //ddldescrip.Items.Insert(0, "-- Select --");
                //}
            }
            catch (Exception ex)
            {
                //   ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        //public void fillRefTable(string descri, string System)
        //{
        //    try
        //    {
        //        DataTable dt1 = objCommonSelDel.CommonFillCondition("[dbo].[prc_acm_common_sel_con1]", "c", "RefType,RefSubType", "dbo.RefTableAdmin", "Descrip", descri);
        //        if (dt1.Rows.Count != 0)
        //        {
        //            reftype = dt1.Rows[0]["RefType"].ToString();
        //            refSubtype = dt1.Rows[0]["RefSubType"].ToString();
        //        }
        //        else
        //        { }
        //        dt = objCommonSelDel.CommonFillCondition1("[dbo].[prc_acm_common_sel_con1]", "checked_active", "REFID,REFTYPE,REFSUBTYPE,SHORTNAME,REFNAME1,REFNAME2,REFNAME3,SWITCH1,SWITCH2,SWITCH3,Remarks,ACTIVE", "dbo.REFTABLE", "REFTYPE", reftype, "REFSUBTYPE", refSubtype, "System", System, "", "");
        //        if (dt.Rows.Count > 0)
        //        {
        //            RefGridview.DataSource = dt;
        //            RefGridview.DataBind();

        //            for (int i = 0; i < RefGridview.Items.Count; i++)
        //            {
        //                LinkButton ln = (LinkButton)RefGridview.Items[i].FindControl("lnkAction");
        //                Label lbl = (Label)RefGridview.Items[i].FindControl("lblCurrentStatus");
        //                if (lbl.Text.Trim().Equals("Y") || lbl.Text.Trim().Equals("1"))
        //                {
        //                    ln.Text = "Inactive";
        //                }
        //                else
        //                {
        //                    ln.Text = "Active";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RefGridview.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //   WebMsgBox.Show(ex.Message);
        //    }
        //}
        //}
        public void Clear()
        {
            txtRefType.Text = "";
            txtRefSubType.Text = "";
            txtRefName.Text = "";
            txtRefNameCH.Text = "";
            txtRefNameO.Text = "";
            txtShortname.Text = "";
            txtSwitch1.Text = "";
            txtSwitch2.Text = "";
            txtSortNo.Text = "";
            txtRemarks.Text = "";
            drpactive.SelectedValue = "0";

        }

        [System.Web.Services.WebMethod]
        public static DataTable ClearGrid()
        {
            try
            {
                dt.Rows.Clear();
                //dtclear();
                return dt;
            }
            catch (Exception ex)
            {
                //  WebMsgBox.Show(ex.Message);
                return dt;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                pnlCreateForm.Style.Add("display", "block");
                //    LstView.Style.Add("display", "block");
                //    FrmView.Style.Add("display", "block");
                //fillRefTable(ddldescrip.SelectedItem.Text, ddlSystem.SelectedItem.Text);
                //ddldescrip.Enabled = false;
                //ddlSystem.Enabled = false;
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                //ddldescrip.SelectedIndex = 0;
                //ddlSystem.SelectedIndex = 0;
                if (dt != null)
                {
                    dt.Rows.Clear();
                }
                RefGridview.DataSource = dt;
                RefGridview.DataBind();
                //ddldescrip.Enabled = true;
                //ddlSystem.Enabled = true;
                //     LstView.Style.Add("display", "block");
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }
        protected void lnkAction_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lfillform;
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int RefID = Convert.ToInt32(lfillform[0]);
                string flag = lfillform[1];
                if (flag == "Y")
                {
                    flag = "N";
                }
                else
                {
                    flag = "Y";
                }
                //int refid =Convert.ToInt32((grdRefTable.SelectedRow.FindControl("lblRefId") as Label).Text);

                Database.REFTABLE obj_Reftable = DB.REFTABLEs.SingleOrDefault(p => p.REFID == RefID && p.TenentID == TID);
                obj_Reftable.ACTIVE = flag;
                DB.SaveChanges();
                String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + RefID;
                String evantname = "create";
                String tablename = "REFTABLE";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                // int j = objCommonSelDel.CommonDelete("[dbo].[prc_acm_common_update_status]", "dbo.REFTABLE", "ACTIVE", flag, "REFID", RefID);
                //fillRefTable(ddldescrip.SelectedItem.Text, ddlSystem.SelectedItem.Text);
                pnlCreateForm.Style.Add("display", "block");
                //   LstView.Style.Add("display", "block");
                //   FrmView.Style.Add("display", "block");
            }
            catch (Exception ex)
            {
                // WebMsgBox.Show(ex.Message);
            }
        }
        protected void btnSubmitAd_Click(object sender, EventArgs e)
        {
            try
            {
                Database.REFTABLE objREFTABLE = new Database.REFTABLE();
                if (ViewState["RefEdit"] != null)
                {
                    int Id = Convert.ToInt32(ViewState["RefEdit"]);
                    objREFTABLE = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Id);
                    objREFTABLE.REFTYPE = txtRefType.Text;
                    objREFTABLE.REFSUBTYPE = txtRefSubType.Text;
                    objREFTABLE.REFNAME1 = txtRefName.Text;
                    objREFTABLE.REFNAME2 = txtRefNameO.Text;
                    objREFTABLE.REFNAME3 = txtRefNameCH.Text;
                    objREFTABLE.SHORTNAME = txtShortname.Text;
                    objREFTABLE.SWITCH1 = txtSwitch1.Text;
                    objREFTABLE.SWITCH2 = txtSwitch2.Text;
                    objREFTABLE.SWITCH3 = txtSortNo.Text;
                    objREFTABLE.Remarks = txtRemarks.Text;
                    objREFTABLE.Infrastructure = ckbInfras.Checked == true ? "Y" : "N";
                    if (drpactive.SelectedValue == "1")
                    {
                        objREFTABLE.ACTIVE = "Y";
                    }
                    else
                    {
                        objREFTABLE.ACTIVE = "N";
                    }
                    DB.SaveChanges();

                    String url = "update REFTABLE with " + "TenentID = " + TID + "ID =" + Id;
                    String evantname = "Update";
                    String tablename = "REFTABLE";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                    ViewState["RefEdit"] = null;

                }
                else
                {
                    objREFTABLE.TenentID = TID;
                    // objREFTABLE.CreatedBy = 1;
                    // objREFTABLE.UpdatedBy = 1;
                    string RefTypr = txtRefType.Text.ToString();
                    string Refsub = txtRefSubType.Text.ToString();
                    string ProjectName = txtShortname.Text.ToString();
                    int SID = Convert.ToInt32(DB.RefTableAdmins.Single(p => p.RefType == RefTypr && p.RefSubType == Refsub && p.MySysName == ProjectName && p.TenentID == TID).StartSerial);
                    int Eid = Convert.ToInt32(DB.RefTableAdmins.Single(p => p.RefType == RefTypr && p.RefSubType == Refsub && p.MySysName == ProjectName && p.TenentID == TID).EndSerial);
                    if (DB.REFTABLEs.Where(p => p.REFTYPE == RefTypr && p.REFSUBTYPE == Refsub && p.SHORTNAME == ProjectName && p.TenentID == TID).Count() > 0)
                    {
                        objREFTABLE.REFID = DB.REFTABLEs.Where(a=>a.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Where(a => a.REFTYPE == RefTypr && a.REFSUBTYPE == Refsub && a.TenentID==TID).Max(p => p.REFID) + 1) : 1;
                        if (Eid < objREFTABLE.REFID)
                        {
                            string display = "EndSerial Are Max!";
                            ClientScript.RegisterStartupScript(this.GetType(), "EndSerial Are Max!", "alert('" + display + "');", true);
                            return;
                        }
                        else
                        {
                            objREFTABLE.REFTYPE = txtRefType.Text;
                            objREFTABLE.REFSUBTYPE = txtRefSubType.Text;
                            objREFTABLE.REFNAME1 = txtRefName.Text;
                            objREFTABLE.REFNAME2 = txtRefNameO.Text;
                            objREFTABLE.REFNAME3 = txtRefNameCH.Text;
                            objREFTABLE.SHORTNAME = txtShortname.Text;
                            objREFTABLE.SWITCH1 = txtSwitch1.Text;
                            objREFTABLE.SWITCH2 = txtSwitch2.Text;
                            objREFTABLE.SWITCH3 = txtSortNo.Text;
                            objREFTABLE.Remarks = txtRemarks.Text;
                            objREFTABLE.Infrastructure = ckbInfras.Checked == true ? "Y" : "N";
                            if (drpactive.SelectedValue == "1")
                            {
                                objREFTABLE.ACTIVE = "Y";
                            }
                            else
                            {
                                objREFTABLE.ACTIVE = "N";
                            }
                            DB.REFTABLEs.AddObject(objREFTABLE);
                            DB.SaveChanges();
                            String url = "insert new record in REFTABLE with " + "TenentID = " + TID ;
                            String evantname = "create";
                            String tablename = "REFTABLE";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                        }
                    }
                    else
                    {
                        objREFTABLE.REFID = SID;
                        objREFTABLE.REFTYPE = txtRefType.Text;
                        objREFTABLE.REFSUBTYPE = txtRefSubType.Text;
                        objREFTABLE.REFNAME1 = txtRefName.Text;
                        objREFTABLE.REFNAME2 = txtRefNameO.Text;
                        objREFTABLE.REFNAME3 = txtRefNameCH.Text;
                        objREFTABLE.SHORTNAME = txtShortname.Text;
                        objREFTABLE.SWITCH1 = txtSwitch1.Text;
                        objREFTABLE.SWITCH2 = txtSwitch2.Text;
                        objREFTABLE.SWITCH3 = txtSortNo.Text;
                        objREFTABLE.Remarks = txtRemarks.Text;
                        objREFTABLE.Infrastructure = ckbInfras.Checked == true ? "Y" : "N";
                        if (drpactive.SelectedValue == "1")
                        {
                            objREFTABLE.ACTIVE = "Y";
                        }
                        else
                        {
                            objREFTABLE.ACTIVE = "N";
                        }
                        DB.REFTABLEs.AddObject(objREFTABLE);
                        DB.SaveChanges();

                        String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID = "+ SID;
                        String evantname = "create";
                        String tablename = "REFTABLE";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                    }

                }
                //int i = (string.IsNullOrEmpty(hdnRef.Value) ? -1 : Convert.ToInt32(hdnRef.Value));
                ////objreftbl.TenentID = Convert.ToInt32(Session["Tenant_Id"].ToString());
                ////objreftbl.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                ////objreftbl.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());

                //int ii = objreftbl.SaveRefTable();
                //if (ii == 1)
                //{
                //    popupScript = "<script language='javascript'>" + "showSuccessToast();" + "</script>";
                //    //    WebMsgBox.Show("Ref type created SuccessFully.");
                //}
                //else if (ii == 2)
                //{
                //    popupScript = "<script language='javascript'>" + "showSuccessToast1();" + "</script>";
                //    //     WebMsgBox.Show("Ref type updated SuccessFully.");
                //    btnSubmitAd.Text = "Submit";
                //    //fillRefTable(ddldescrip.SelectedItem.Text, ddlSystem.SelectedItem.Text);
                //}
                //else
                //{
                //    popupScript = "<script language='javascript'>" + "showWarningToast1();" + "</script>";
                //    //   WebMsgBox.Show("Ref type already exists");
                //}
                //Page.ClientScript.RegisterStartupScript(typeof(Page), "popupScript", popupScript);
                Clear();
                hdnRef.Value = "";
                pnlCreateForm.Style.Add("display", "block");
                //   LstView.Style.Add("display", "block");
                //   FrmView.Style.Add("display", "block");
                RefGridview.DataSource = DB.REFTABLEs.Where(p => p.REFTYPE == txtRefTyep.Text && p.REFSUBTYPE == txtSubtype.Text && p.TenentID == TID);
                RefGridview.DataBind();
               
                panlRefEntery.Visible = false;
            }
            catch (Exception ex)
            {
                // WebMsgBox.Show(ex.Message);
            }
        }
        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {
            LinkButton ln = (LinkButton)sender;
            int refid = Convert.ToInt32(ln.CommandArgument);
            //int refid =Convert.ToInt32((grdRefTable.SelectedRow.FindControl("lblRefId") as Label).Text);
            Database.REFTABLE obj_Reftable1 = DB.REFTABLEs.SingleOrDefault(p => p.REFID == refid && p.TenentID == TID);
            obj_Reftable1.ACTIVE = "D";
            DB.SaveChanges();

             String url = "Delete REFTABLE with " + "TenentID = " + TID + "REFID = " + refid;
                        String evantname = "DELETE";
                        String tablename = "REFTABLE";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

              Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
            // int j = objCommonSelDel.CommonDelete("[dbo].[prc_acm_common_update_status]", "dbo.REFTABLE", "ACTIVE", "D", "REFID", refid);
            //fillRefTable(ddldescrip.SelectedItem.Text, ddlSystem.SelectedItem.Text);
            pnlCreateForm.Style.Add("display", "block");
            //    LstView.Style.Add("display", "block");
            //  FrmView.Style.Add("display", "block");
        }
        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            try
            {
                panlRefEntery.Visible = true;
                string[] lfillform;
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int RefID = Convert.ToInt32(lfillform[0]);
                Database.REFTABLE obj_ReftableFill = DB.REFTABLEs.SingleOrDefault(p => p.REFID == RefID && p.TenentID==TID);
                //  System.Data.DataTable dt2 = objCommonSelDel.CommonFillConditionGridPQ("[dbo].[prc_acm_common_sel_con]", "c", "REFID,REFTYPE,REFSUBTYPE,SHORTNAME,REFNAME1,REFNAME2,REFNAME3,SWITCH1,SWITCH2,SWITCH3,Remarks,ACTIVE", "dbo.REFTABLE", "REFID", RefID.ToString(), "");
                txtRefType.Text = obj_ReftableFill.REFTYPE.ToString();
                txtRefSubType.Text = obj_ReftableFill.REFSUBTYPE.ToString(); //dt2.Rows[0]["REFSUBTYPE"].ToString();
                txtShortname.Text = obj_ReftableFill.SHORTNAME.ToString();//dt2.Rows[0]["SHORTNAME"].ToString();
                txtRefName.Text = obj_ReftableFill.REFNAME1.ToString();//dt2.Rows[0]["REFNAME1"].ToString();
                txtRefNameO.Text = obj_ReftableFill.REFNAME2.ToString();//dt2.Rows[0]["REFNAME2"].ToString();
                txtRefNameCH.Text = obj_ReftableFill.REFNAME3.ToString();//dt2.Rows[0]["REFNAME3"].ToString();
                txtSwitch1.Text = obj_ReftableFill.SWITCH1.ToString();//dt2.Rows[0]["SWITCH1"].ToString();
                txtSwitch2.Text = obj_ReftableFill.SWITCH2.ToString(); //dt2.Rows[0]["SWITCH2"].ToString();
                txtSortNo.Text = obj_ReftableFill.SWITCH3.ToString(); //dt2.Rows[0]["SWITCH3"].ToString();
                txtRemarks.Text = obj_ReftableFill.Remarks.ToString();//dt2.Rows[0]["Remarks"].ToString();

                if (lfillform[1] == "Y")
                    drpactive.SelectedValue = "1";
                else
                    drpactive.SelectedValue = "2";

                pnlCreateForm.Style.Add("display", "block");
                //   LstView.Style.Add("display", "block");
                //    FrmView.Style.Add("display", "block");
                txtShortname.Enabled = true;
                txtRefSubType.Enabled = true;
                txtRefType.Enabled = true;
                hdnRef.Value = RefID.ToString();

            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }

        protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            // binddescri(ddlSystem.SelectedItem.Text);
        }

        protected void linkAdminRef_Click(object sender, EventArgs e)
        {
            Database.RefTableAdmin objRefTableAdmin = new Database.RefTableAdmin();
            if (ViewState["Edit"] != null)
            {
                int id = Convert.ToInt32(ViewState["Edit"]);
                objRefTableAdmin = DB.RefTableAdmins.Single(p => p.TenentID == TID && p.RefAdminId == id);
                objRefTableAdmin.TenentID = TID;
                objRefTableAdmin.RefType = txtRefTyep.Text;
                objRefTableAdmin.RefSubType = txtSubtype.Text;
                objRefTableAdmin.MySysName = drpProjectList.SelectedValue;
                objRefTableAdmin.Descrip = txtDescriptionAdmin.Text;
                objRefTableAdmin.Admin = "N";
                objRefTableAdmin.NormalUser = "Y";
                objRefTableAdmin.switch1 = txtswitchadmin.Text;
                objRefTableAdmin.Remarks = txtRemarksadmin.Text;
                objRefTableAdmin.Active = "Y";
                objRefTableAdmin.StartSerial = Convert.ToInt32(txtStartSerial.Text);
                objRefTableAdmin.EndSerial = Convert.ToInt32(txtEndSerial.Text);
                objRefTableAdmin.Infrastructure = ckbInfras.Checked ? "Y" : "N";
                //  objRefTableAdmin.SyncDate = txtSyncDate.Text;
                DB.SaveChanges();

                String url = "update RefTableAdmin with " + "TenentID = " + TID + "RefAdminId = " + id;
                String evantname = "update";
                String tablename = "REFTABLE";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                RefGridview.DataSource = DB.REFTABLEs.Where(p => p.REFTYPE == txtRefTyep.Text && p.REFSUBTYPE == txtSubtype.Text && p.TenentID == TID);
                RefGridview.DataBind();
                ViewState["Edit"] = null;

            }
            else
            {

                objRefTableAdmin.RefAdminId = DB.RefTableAdmins.Where(p=>p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.RefTableAdmins.Where(p=>p.TenentID == TID).Max(p => p.RefAdminId) + 1) : 1;
                objRefTableAdmin.TenentID = TID;
                objRefTableAdmin.RefType = txtRefTyep.Text;
                objRefTableAdmin.RefSubType = txtSubtype.Text;
                objRefTableAdmin.MySysName = drpProjectList.SelectedValue;
                objRefTableAdmin.Descrip = txtDescriptionAdmin.Text;
                objRefTableAdmin.Admin = "N";
                objRefTableAdmin.NormalUser = "Y";
                objRefTableAdmin.switch1 = txtswitchadmin.Text;
                objRefTableAdmin.Remarks = txtRemarksadmin.Text;
                objRefTableAdmin.Active = "Y";
                objRefTableAdmin.StartSerial = Convert.ToInt32(txtStartSerial.Text);
                objRefTableAdmin.EndSerial = Convert.ToInt32(txtEndSerial.Text);
                objRefTableAdmin.Infrastructure = ckbInfras.Checked ? "Y" : "N";
                DB.RefTableAdmins.AddObject(objRefTableAdmin);
                DB.SaveChanges();

                String url = "insert new record in RefTableAdmin with " + "TenentID = " + TID + "RefAdminId = " + objRefTableAdmin.RefAdminId;
                String evantname = "create";
                String tablename = "RefTableAdmin";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                LISTADMIN.DataSource = DB.RefTableAdmins.Where(p => p.RefType == txtRefTyep.Text && p.RefSubType == txtSubtype.Text && p.TenentID == TID);
                LISTADMIN.DataBind();
                Admintebelceln();
                PanllistOfAdmin.Visible = true;
            }

        }

        public void BindData()
        {
            LISTADMIN.DataSource = DB.RefTableAdmins.Where(p => p.TenentID == TID && p.Active == "Y");
            LISTADMIN.DataBind();
        }
        public void FillContractorID()
        {

            Classes.EcommAdminClass.getdropdown(drpSystem, TID, "", "", "", "TBLPROJECT");
            //select * from TBLPROJECT where TenentID = 1

            //drpSystem.DataSource = DB.TBLPROJECTs.Where(p => p.TenentID == TID);
            //drpSystem.DataTextField = "PROJECTNAME1";
            //drpSystem.DataValueField = "PROJECTNAME1";
            //drpSystem.DataBind();
            //drpSystem.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpProjectList, TID, "", "", "", "TBLPROJECT");
            //select * from TBLPROJECT where TenentID = 1

            //drpProjectList.DataSource = DB.TBLPROJECTs.Where(p => p.TenentID == TID);
            //drpProjectList.DataTextField = "PROJECTNAME1";
            //drpProjectList.DataValueField = "PROJECTNAME1";
            //drpProjectList.DataBind();
            //drpProjectList.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void drpSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Project = drpSystem.SelectedValue;
            drpRefType.DataSource = DB.RefTableAdmins.Where(p =>p.MySysName == Project && p.TenentID==TID).GroupBy(a => a.RefType).Select(grp => grp.FirstOrDefault()).ToList(); ;
            drpRefType.DataTextField = "RefType";
            drpRefType.DataValueField = "RefAdminId";
            drpRefType.DataBind();
            drpRefType.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void adminList_Click(object sender, EventArgs e)
        {
            string Project = drpSystem.SelectedValue;
            string REF = drpRefType.SelectedValue;
            LISTADMIN.DataSource = DB.RefTableAdmins.Where(p => p.TenentID == TID && p.Active == "Y" && p.MySysName == Project && p.RefType.ToUpper().Contains(REF.ToUpper()));
            LISTADMIN.DataBind();

        }

        protected void LISTADMIN_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.RefTableAdmin objRefAdmin = DB.RefTableAdmins.Single(p => p.TenentID == TID && p.RefAdminId == ID && p.Active == "Y");
                objRefAdmin.Active = "N";
                DB.SaveChanges();

                String url = "Delete RefTableAdmin with " + "TenentID = " + TID + "RefAdminId =" + ID;
                String evantname = "delete";
                String tablename = "RefTableAdmin";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

            }
            if (e.CommandName == "btnEdit")
            {
                panlRefAdmin.Visible = true;
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.RefTableAdmin objRefAdmin = DB.RefTableAdmins.Single(p => p.TenentID == TID && p.RefAdminId == ID);
                linkAdminRef.Text = "Update";
                txtRefTyep.Text = objRefAdmin.RefType.ToString();
                txtSubtype.Text = objRefAdmin.RefSubType.ToString();
                drpProjectList.SelectedValue = objRefAdmin.MySysName.ToString();
                txtDescriptionAdmin.Text = objRefAdmin.Descrip.ToString();
                txtswitchadmin.Text = objRefAdmin.switch1.ToString();
                txtRemarksadmin.Text = objRefAdmin.Remarks.ToString();
                txtStartSerial.Text = objRefAdmin.StartSerial.ToString();
                txtEndSerial.Text = objRefAdmin.EndSerial.ToString();
                ckbInfras.Checked = (objRefAdmin.Infrastructure == "Y") ? true : false;
                AddNew.Visible = false;
                ViewState["Edit"] = ID;
            }
            if (e.CommandName == "btnadd")
            {
                panlRefAdmin.Visible = true;

                panlListRef.Visible = true;
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.RefTableAdmin objRefAdmin = DB.RefTableAdmins.Single(p => p.TenentID == TID && p.RefAdminId == ID);
                linkAdminRef.Text = "Update";
                txtRefTyep.Text = objRefAdmin.RefType.ToString();
                txtSubtype.Text = objRefAdmin.RefSubType.ToString();
                drpProjectList.SelectedValue = objRefAdmin.MySysName.ToString();
                txtDescriptionAdmin.Text = objRefAdmin.Descrip.ToString();
                txtswitchadmin.Text = objRefAdmin.switch1.ToString();
                txtRemarksadmin.Text = objRefAdmin.Remarks.ToString();
                RefGridview.DataSource = DB.REFTABLEs.Where(p => p.REFTYPE == txtRefTyep.Text && p.REFSUBTYPE == txtSubtype.Text && p.TenentID == TID);
                txtRefType.Text = objRefAdmin.RefType.ToString();
                txtRefSubType.Text = objRefAdmin.RefSubType.ToString();
                txtShortname.Text = objRefAdmin.MySysName.ToString();
                if (objRefAdmin.StartSerial == null || objRefAdmin.StartSerial == 0)
                {
                    txtStartSerial.Text = "";
                }
                else
                {
                    txtStartSerial.Text = objRefAdmin.StartSerial.ToString();
                }
                if (objRefAdmin.EndSerial == null || objRefAdmin.EndSerial == 0)
                {
                    txtEndSerial.Text = "";
                }
                else
                {
                    txtEndSerial.Text = objRefAdmin.EndSerial.ToString();
                }

                RefGridview.DataBind();
                ViewState["Edit"] = ID;
                ckbInfras.Focus();
            }

        }

        protected void AddNew_Click(object sender, EventArgs e)
        {

            AddNew.Visible = false;
            panlRefEntery.Visible = true;
            txtRefType.Focus();

        }
        public void Admintebelceln()
        {
            txtRefTyep.Text = txtSubtype.Text = txtDescriptionAdmin.Text = txtswitchadmin.Text = txtRemarksadmin.Text = "";
            drpProjectList.SelectedIndex = 0;
        }

        protected void linkAddAdminEntry_Click(object sender, EventArgs e)
        {
            Admintebelceln();
            linkAddAdminEntry.Visible = false;
            linkAdminRef.Text = "Submit";
            panlListRef.Visible = false;
            panlRefAdmin.Visible = true;
            ViewState["Edit"] = null;
        }

        protected void RefGridview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkbtndelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.REFTABLE objRefAdmin = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID && p.ACTIVE == "Y");
                objRefAdmin.ACTIVE = "N";
                DB.SaveChanges();

                String url = "Delete REFTABLE with " + "TenentID = " + TID + "REFID =" + ID;
                String evantname = "Delete";
                String tablename = "REFTABLE";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

            }

            if (e.CommandName == "linkadd")
            {

                panlRefEntery.Visible = true;
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.REFTABLE objRefAdmin = DB.REFTABLEs.Single(p => (p.TenentID == TID || p.TenentID == 360) && p.REFID == ID);

                txtRefType.Text = objRefAdmin.REFTYPE.ToString();
                txtRefSubType.Text = objRefAdmin.REFSUBTYPE.ToString();
                txtShortname.Text = objRefAdmin.SHORTNAME.ToString();
                txtRefName.Text = objRefAdmin.REFNAME1.ToString();
                txtRefNameO.Text = objRefAdmin.REFNAME2.ToString();
                txtRefNameCH.Text = objRefAdmin.REFNAME3.ToString();
                txtSwitch1.Text = objRefAdmin.SWITCH1.ToString();
                txtSwitch2.Text = objRefAdmin.SWITCH2.ToString();
                txtSortNo.Text = objRefAdmin.SWITCH3.ToString();
                if (objRefAdmin.ACTIVE == "Y")
                    drpactive.SelectedValue = "1";
                else
                    drpactive.SelectedValue = "2";
                txtRemarks.Text = objRefAdmin.Remarks.ToString();
                AddNew.Visible = false;
                btnSubmitAd.Text = "Update";
                ViewState["RefEdit"] = ID;
                txtRefType.Focus();
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            //WebClient client = new WebClient();
            //byte[] datasize = null;
            //try
            //{
            //    datasize = client.DownloadData("http://www.google.com");
            //}
            //catch (Exception ex)
            //{
            //}
            //if (datasize != null && datasize.Length > 0)
            //{


            // CRM Reftabels



            //}
            //else
            //{
            //    //lbltxt.ForeColor = System.Drawing.Color.Red;
            //    //lbltxt.Text = "Internet Connection Not Available.";
            //}
        }

        protected void btnSyncro_Click(object sender, EventArgs e)
        {
            
            var List = DB.REFTABLEs.Where(p => p.Infrastructure == "Y" && p.TenentID == TTID).ToList();
            foreach (Database.REFTABLE item in List)
            {
                int TID = Convert.ToInt32(item.TenentID);
                int RID = Convert.ToInt32(item.REFID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == RID).Count() == 0)
                {
                    REFTABLE objREFTABLE = new REFTABLE();
                    objREFTABLE.TenentID = TID;
                    objREFTABLE.REFID = RID;
                    objREFTABLE.REFTYPE = item.REFTYPE;
                    objREFTABLE.REFSUBTYPE = item.REFSUBTYPE;
                    objREFTABLE.SHORTNAME = item.SHORTNAME;
                    objREFTABLE.REFNAME1 = item.REFNAME1;
                    objREFTABLE.REFNAME2 = item.REFNAME2;
                    objREFTABLE.REFNAME3 = item.REFNAME3;
                    objREFTABLE.SWITCH1 = item.SWITCH1;
                    objREFTABLE.SWITCH2 = item.SWITCH2;
                    objREFTABLE.SWITCH3 = item.SWITCH3;
                    objREFTABLE.SWITCH4 = item.SWITCH4;
                    objREFTABLE.Remarks = item.Remarks;
                    objREFTABLE.ACTIVE = item.ACTIVE;
                    objREFTABLE.CRUP_ID = item.CRUP_ID;
                    objREFTABLE.Infrastructure = "N";
                    objREFTABLE.SyncDate = DateTime.Now;
                    DB.REFTABLEs.AddObject(objREFTABLE);
                    DB.SaveChanges();

                    String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + RID;
                    String evantname = "create";
                    String tablename = "REFTABLE";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                }
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == RID).Count() == 0)
               {
                   REFTABLE objREFTABLE = new REFTABLE();
                   objREFTABLE.TenentID = item.TenentID;
                   objREFTABLE.REFID = item.REFID;
                   objREFTABLE.REFTYPE = item.REFTYPE;
                   objREFTABLE.REFSUBTYPE = item.REFSUBTYPE;
                   objREFTABLE.SHORTNAME = item.SHORTNAME;
                   objREFTABLE.REFNAME1 = item.REFNAME1;
                   objREFTABLE.REFNAME2 = item.REFNAME2;
                   objREFTABLE.REFNAME3 = item.REFNAME3;
                   objREFTABLE.SWITCH1 = item.SWITCH1;
                   objREFTABLE.SWITCH2 = item.SWITCH2;
                   objREFTABLE.SWITCH3 = item.SWITCH3;
                   objREFTABLE.SWITCH4 = item.SWITCH4;
                   objREFTABLE.Remarks = item.Remarks;
                   objREFTABLE.ACTIVE = item.ACTIVE;
                   objREFTABLE.CRUP_ID = item.CRUP_ID;
                   objREFTABLE.Infrastructure = "N";
                   objREFTABLE.SyncDate = DateTime.Now;
                   DB.REFTABLEs.AddObject(objREFTABLE);
                   DB.SaveChanges();
                   String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + item.REFID;
                   String evantname = "create";
                   String tablename = "REFTABLE";
                   string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                   Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
               }
            }
            foreach (Database.REFTABLE item1 in List)
            {
                var objtbl_Contact = DB.REFTABLEs.SingleOrDefault(p => p.REFID == item1.REFID);
                objtbl_Contact.Infrastructure = "N";
                DB.SaveChanges();

            }

            // ACM Reftabels


            var List1 = DB.REFTABLEs.Where(p => p.Infrastructure == "Y" && p.TenentID == TTID).ToList();
            foreach (Database.REFTABLE item in List1)
            {
                int TID = Convert.ToInt32(item.TenentID);
                int RID = Convert.ToInt32(item.REFID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == RID).Count() == 0)
                {
                    REFTABLE objREFTABLE = new REFTABLE();
                    objREFTABLE.TenentID = item.TenentID;
                    objREFTABLE.REFID = item.REFID;
                    objREFTABLE.REFTYPE = item.REFTYPE;
                    objREFTABLE.REFSUBTYPE = item.REFSUBTYPE;
                    objREFTABLE.SHORTNAME = item.SHORTNAME;
                    objREFTABLE.REFNAME1 = item.REFNAME1;
                    objREFTABLE.REFNAME2 = item.REFNAME2;
                    objREFTABLE.REFNAME3 = item.REFNAME3;
                    objREFTABLE.SWITCH1 = item.SWITCH1;
                    objREFTABLE.SWITCH2 = item.SWITCH2;
                    objREFTABLE.SWITCH3 = item.SWITCH3;
                    objREFTABLE.SWITCH4 = item.SWITCH4;
                    objREFTABLE.Remarks = item.Remarks;
                    objREFTABLE.ACTIVE = item.ACTIVE;
                    objREFTABLE.CRUP_ID = item.CRUP_ID;
                    objREFTABLE.Infrastructure = "N";
                    objREFTABLE.SyncDate = DateTime.Now;
                    DB.REFTABLEs.AddObject(objREFTABLE);
                    DB.SaveChanges();

                    String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + item.REFID;
                    String evantname = "create";
                    String tablename = "REFTABLE";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                }
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == RID).Count() == 0)
                {
                    REFTABLE objREFTABLE = new REFTABLE();
                    objREFTABLE.TenentID = item.TenentID;
                    objREFTABLE.REFID = item.REFID;
                    objREFTABLE.REFTYPE = item.REFTYPE;
                    objREFTABLE.REFSUBTYPE = item.REFSUBTYPE;
                    objREFTABLE.SHORTNAME = item.SHORTNAME;
                    objREFTABLE.REFNAME1 = item.REFNAME1;
                    objREFTABLE.REFNAME2 = item.REFNAME2;
                    objREFTABLE.REFNAME3 = item.REFNAME3;
                    objREFTABLE.SWITCH1 = item.SWITCH1;
                    objREFTABLE.SWITCH2 = item.SWITCH2;
                    objREFTABLE.SWITCH3 = item.SWITCH3;
                    objREFTABLE.SWITCH4 = item.SWITCH4;
                    objREFTABLE.Remarks = item.Remarks;
                    objREFTABLE.ACTIVE = item.ACTIVE;
                    objREFTABLE.CRUP_ID = item.CRUP_ID;
                    objREFTABLE.Infrastructure = "N";
                    objREFTABLE.SyncDate = DateTime.Now;
                    DB.REFTABLEs.AddObject(objREFTABLE);
                    DB.SaveChanges();
                    String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + item.REFID;
                    String evantname = "create";
                    String tablename = "REFTABLE";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                }
            }
            foreach (Database.REFTABLE item1 in List1)
            {
                var objtbl_Contact = DB.REFTABLEs.SingleOrDefault(p => p.REFID == item1.REFID);
                objtbl_Contact.Infrastructure = "N";
                DB.SaveChanges();
            }

            // Ecommersh Reftebel
            var List2 = DB.REFTABLEs.Where(p => p.Infrastructure == "Y" && p.TenentID == TTID).ToList();
            foreach (Database.REFTABLE item in List2)
            {
                int TID = Convert.ToInt32(item.TenentID);
                int RID = Convert.ToInt32(item.REFID);
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == RID).Count() == 0)
                {
                    REFTABLE objREFTABLE = new REFTABLE();
                    objREFTABLE.TenentID = item.TenentID;
                    objREFTABLE.REFID = item.REFID;
                    objREFTABLE.REFTYPE = item.REFTYPE;
                    objREFTABLE.REFSUBTYPE = item.REFSUBTYPE;
                    objREFTABLE.SHORTNAME = item.SHORTNAME;
                    objREFTABLE.REFNAME1 = item.REFNAME1;
                    objREFTABLE.REFNAME2 = item.REFNAME2;
                    objREFTABLE.REFNAME3 = item.REFNAME3;
                    objREFTABLE.SWITCH1 = item.SWITCH1;
                    objREFTABLE.SWITCH2 = item.SWITCH2;
                    objREFTABLE.SWITCH3 = item.SWITCH3;
                    objREFTABLE.SWITCH4 = item.SWITCH4;
                    objREFTABLE.Remarks = item.Remarks;
                    objREFTABLE.ACTIVE = item.ACTIVE;
                    objREFTABLE.CRUP_ID = item.CRUP_ID;
                    objREFTABLE.Infrastructure = "N";
                    objREFTABLE.SyncDate = DateTime.Now;
                    DB.REFTABLEs.AddObject(objREFTABLE);
                    DB.SaveChanges();
                    String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + item.REFID;
                    String evantname = "create";
                    String tablename = "REFTABLE";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                }
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == RID).Count() == 0)
                {
                    REFTABLE objREFTABLE = new REFTABLE();
                    objREFTABLE.TenentID = item.TenentID;
                    objREFTABLE.REFID = item.REFID;
                    objREFTABLE.REFTYPE = item.REFTYPE;
                    objREFTABLE.REFSUBTYPE = item.REFSUBTYPE;
                    objREFTABLE.SHORTNAME = item.SHORTNAME;
                    objREFTABLE.REFNAME1 = item.REFNAME1;
                    objREFTABLE.REFNAME2 = item.REFNAME2;
                    objREFTABLE.REFNAME3 = item.REFNAME3;
                    objREFTABLE.SWITCH1 = item.SWITCH1;
                    objREFTABLE.SWITCH2 = item.SWITCH2;
                    objREFTABLE.SWITCH3 = item.SWITCH3;
                    objREFTABLE.SWITCH4 = item.SWITCH4;
                    objREFTABLE.Remarks = item.Remarks;
                    objREFTABLE.ACTIVE = item.ACTIVE;
                    objREFTABLE.CRUP_ID = item.CRUP_ID;
                    objREFTABLE.Infrastructure = "N";
                    objREFTABLE.SyncDate = DateTime.Now;
                    DB.REFTABLEs.AddObject(objREFTABLE);
                    DB.SaveChanges();
                    String url = "insert new record in REFTABLE with " + "TenentID = " + TID + "REFID =" + item.REFID;
                    String evantname = "create";
                    String tablename = "REFTABLE";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);
                }
            }
            foreach (Database.REFTABLE item1 in List2)
            {
                var objtbl_Contact = DB.REFTABLEs.SingleOrDefault(p => p.REFID == item1.REFID);
                objtbl_Contact.Infrastructure = "N";
                DB.SaveChanges();
            }

        }
    }
}