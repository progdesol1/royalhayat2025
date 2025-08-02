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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using Classes;
using System.Diagnostics;

namespace Web.Master
{
    public partial class REFTABLE : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            PnlGriderror.Visible = false;
            lblGridErrormsg.Text = "";

            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                btnAdd.ValidationGroup = "s";
                //FirstData();
                //var obj = DB.RefLabelMSTs.Single(p => p.RefType == "COMP" && p.RefSubType == "COMPTYPE");
                //LE1.Text = obj.LE1;
                //LE2.Text = obj.LE2;
                //LE3.Text = obj.LE3;
                //LE4.Text = obj.LE4;
                //LE5.Text = obj.LE5;
                //LE6.Text = obj.LE6;
                //LE7.Text = obj.LE7;
                //LE8.Text = obj.LE8;
                //LE9.Text = obj.LE9;
                //LE10.Text = obj.LE10;

                if (Request.QueryString["REFTYPE"] != null && Request.QueryString["REFSUBTYPE"] != null)
                {
                    string reftype = Request.QueryString["REFTYPE"].ToString();
                    string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();

                    if (reftype == "Food")
                    {
                        if (refsubtype == "MealType")
                        {
                            lblHeader.Text = "  Meal ";
                            Label5.Text = "Meal  ";
                        }
                        else if (refsubtype == "DeliveryTime")
                        {
                            lblHeader.Text = "  Delivery Time ";
                            Label5.Text = "Delivery Time  ";
                        }
                        else
                        {
                            lblHeader.Text = " " + refsubtype;
                            Label5.Text = refsubtype + " ";
                        }

                    }
                    else
                    {
                        lblHeader.Text = " " + reftype;
                        Label5.Text = reftype + " ";
                    }

                    if (reftype == "helpdesk" && refsubtype == "complain")
                    {
                        Database.RefLabelMST objj = DB.RefLabelMSTs.Single(p => p.RefType == "helpdesk" && p.RefSubType == "complain" && p.TenentID == TID);
                        LE10.Text = "Short Name";
                        LE1.Text = "Complain ENG";
                        LE2.Text = "Complain ARB";
                        LE3.Text = "Complain FRN";
                        LE4.Text = "Switch1";
                        LE5.Text = "Switch2";
                        LE6.Text = "Switch3";
                        LE7.Text = "Switch4";
                        LE9.Text = "Active";
                        LE8.Text = "Remarks";
                        BindData();
                    }

                    if (reftype == "Ticket" && refsubtype == "PhysicalLocation")
                    {
                        Database.RefLabelMST objj = DB.RefLabelMSTs.Single(p => p.RefType == "helpdesk" && p.RefSubType == "complain" && p.TenentID == TID);
                        LE10.Text = "Short Name";
                        LE1.Text = "Location ENG";
                        LE2.Text = "Location ARB";
                        LE3.Text = "Location FRN";
                        LE4.Text = "Switch1";
                        LE5.Text = "Switch2";
                        LE6.Text = "Switch3";
                        LE7.Text = "Switch4";
                        LE9.Text = "Active";
                        LE8.Text = "Remarks";
                        BindData();
                    }
                    
                }

            }
        }
        #region Step2
        public void BindData()
        {
            string reftype = Request.QueryString["REFTYPE"].ToString();
            string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();
            //List<Database.REFTABLE> List = DB.REFTABLEs.Where(p=>p.ACTIVE == "Y"&&p.TenentID==TID&&p.REFTYPE==reftype&&p.REFSUBTYPE==refsubtype).OrderBy(m => m.REFID).ToList();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
            Listview1.DataSource = DB.REFTABLEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID && p.REFTYPE == reftype && p.REFSUBTYPE == refsubtype).OrderBy(m => m.REFID).OrderByDescending(m => m.REFID);
            Listview1.DataBind();
        }

        #endregion
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((AcmMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }

        #region PAge Genarator
        public void GetShow()
        {

            //lblREFNAME11s.Attributes["class"] = lblREFNAME21s.Attributes["class"] = lblREFNAME31s.Attributes["class"] = lblSWITCH11s.Attributes["class"] = lblSWITCH21s.Attributes["class"] = lblSWITCH31s.Attributes["class"] = lblSWITCH41s.Attributes["class"] = lblRemarks1s.Attributes["class"] = lblACTIVE1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  getshow";
            //lblREFNAME12h.Attributes["class"] = lblREFNAME22h.Attributes["class"] = lblREFNAME32h.Attributes["class"] = lblSWITCH12h.Attributes["class"] = lblSWITCH22h.Attributes["class"] = lblSWITCH32h.Attributes["class"] = lblSWITCH42h.Attributes["class"] = lblRemarks2h.Attributes["class"] = lblACTIVE2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            //lblREFNAME11s.Attributes["class"] = lblREFNAME21s.Attributes["class"] = lblREFNAME31s.Attributes["class"] = lblSWITCH11s.Attributes["class"] = lblSWITCH21s.Attributes["class"] = lblSWITCH31s.Attributes["class"] = lblSWITCH41s.Attributes["class"] = lblRemarks1s.Attributes["class"] = lblACTIVE1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = "control-label col-md-4  gethide";
            //lblREFNAME12h.Attributes["class"] = lblREFNAME22h.Attributes["class"] = lblREFNAME32h.Attributes["class"] = lblSWITCH12h.Attributes["class"] = lblSWITCH22h.Attributes["class"] = lblSWITCH32h.Attributes["class"] = lblSWITCH42h.Attributes["class"] = lblRemarks2h.Attributes["class"] = lblACTIVE2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = "control-label col-md-4  getshow";
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

        #endregion

        public void Clear()
        {
            //drpREFID.SelectedIndex = 0;
            //txtREFTYPE.Text = "";
            //txtREFSUBTYPE.Text = "";
            txtSHORTNAME.Text = "";
            txtREFNAME1.Text = "";
            txtREFNAME2.Text = "";
            txtREFNAME3.Text = "";
            txtSWITCH1.Text = "";
            txtSWITCH2.Text = "";
            txtSWITCH3.Text = "";
            txtSWITCH4.Text = "";
            txtRemarks.Text = "";
            cbACTIVE.Checked = true;
            //drpCRUP_ID.SelectedIndex = 0;
            //txtInfrastructure.Text = "";
            //txtSyncDate.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            string eventname = "";
                try
                {
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Save";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Save")
                    {
                        eventname = "insert";
                        string REFTYPE = Request.QueryString["REFTYPE"].ToString();
                        string REFSUBTYPE = Request.QueryString["REFSUBTYPE"].ToString();

                        if (DB.REFTABLEs.Where(p => p.REFNAME1.ToLower() == txtREFNAME1.Text.ToLower() && p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE).Count() < 1)
                        {
                            Database.REFTABLE objREFTABLE = new Database.REFTABLE();
                            //Server Content Send data Yogesh
                            //objREFTABLE.REFID = Convert.ToInt32(drpREFID.SelectedValue);
                            //objREFTABLE.REFTYPE = txtREFTYPE.Text;
                            //objREFTABLE.REFSUBTYPE = txtREFSUBTYPE.Text;
                            //objREFTABLE.SHORTNAME = txtSHORTNAME.Text;

                            objREFTABLE.REFTYPE = Request.QueryString["REFTYPE"].ToString();
                            objREFTABLE.REFSUBTYPE = Request.QueryString["REFSUBTYPE"].ToString();
                            objREFTABLE.TenentID = TID;
                            objREFTABLE.REFID = DB.REFTABLEs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                            objREFTABLE.SHORTNAME = txtSHORTNAME.Text;
                            objREFTABLE.REFNAME1 = txtREFNAME1.Text;
                            objREFTABLE.REFNAME2 = txtREFNAME2.Text;
                            objREFTABLE.REFNAME3 = txtREFNAME3.Text;
                            objREFTABLE.SWITCH1 = txtSWITCH1.Text;
                            objREFTABLE.SWITCH2 = txtSWITCH2.Text;
                            objREFTABLE.SWITCH3 = txtSWITCH3.Text;
                            if (txtSWITCH4.Text == "")
                            {
                                objREFTABLE.SWITCH4 = 0;
                            }
                            else
                            {
                                objREFTABLE.SWITCH4 = Convert.ToInt32(txtSWITCH4.Text);
                            }
                            objREFTABLE.Remarks = txtRemarks.Text;
                            objREFTABLE.ACTIVE = cbACTIVE.Checked ? "Y" : "N";
                            //objREFTABLE.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            //objREFTABLE.Infrastructure = txtInfrastructure.Text;
                            //objREFTABLE.SyncDate = Convert.ToDateTime(txtSyncDate.Text);


                            DB.REFTABLEs.AddObject(objREFTABLE);
                            DB.SaveChanges();

                            String url = "insert new record in REFTABLE with " + "TenentID = " + TID +  "REFID = " + objREFTABLE.REFID;
                            String evantname = "create";
                            String tablename = "REFTABLE";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                            Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                        }
                        else
                        {
                            if (DB.REFTABLEs.Where(p => p.REFNAME1.ToLower() == txtREFNAME1.Text.ToLower() && p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE).Count() > 0)
                            {
                                Database.REFTABLE objREFTABLE = DB.REFTABLEs.Where(p => p.REFNAME1.ToLower() == txtREFNAME1.Text.ToLower() && p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE).FirstOrDefault();
                                objREFTABLE.ACTIVE = cbACTIVE.Checked ? "Y" : "N";
                                DB.SaveChanges();

                                Classes.EcommAdminClass.update_SubcriptionSetup(TID);
                            }

                            //Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Duplicate Found ", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        }
                        Clear();
                        //lblMsg.Text = "  Data Save Successfully";
                        btnAdd.ValidationGroup = "s";
                        //pnlSuccessMsg.Visible = true;
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        //navigation.Visible = true;
                        btnAdd.Text = "Add New";
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {
                        eventname = "Update";
                        if (ViewState["TIDD"] != null && ViewState["RID"] != null)
                        {

                            int tidd = Convert.ToInt32(ViewState["TIDD"]);
                            int rid = Convert.ToInt32(ViewState["RID"]);
                            Database.REFTABLE objREFTABLE = DB.REFTABLEs.Single(p => p.TenentID == tidd && p.REFID == rid);
                            //objREFTABLE.REFID = Convert.ToInt32(drpREFID.SelectedValue);
                            //objREFTABLE.REFTYPE = txtREFTYPE.Text;
                            //objREFTABLE.REFSUBTYPE = txtREFSUBTYPE.Text;
                            //objREFTABLE.SHORTNAME = txtSHORTNAME.Text;
                            objREFTABLE.SHORTNAME = txtSHORTNAME.Text;
                            objREFTABLE.REFNAME1 = txtREFNAME1.Text;
                            objREFTABLE.REFNAME2 = txtREFNAME2.Text;
                            objREFTABLE.REFNAME3 = txtREFNAME3.Text;
                            objREFTABLE.SWITCH1 = txtSWITCH1.Text;
                            objREFTABLE.SWITCH2 = txtSWITCH2.Text;
                            objREFTABLE.SWITCH3 = txtSWITCH3.Text;
                            if (txtSWITCH4.Text == "")
                            {
                                objREFTABLE.SWITCH4 = 0;
                            }
                            else
                            {
                                objREFTABLE.SWITCH4 = Convert.ToInt32(txtSWITCH4.Text);
                            }
                            objREFTABLE.Remarks = txtRemarks.Text;
                            objREFTABLE.ACTIVE = cbACTIVE.Checked ? "Y" : "N";
                            //objREFTABLE.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            //objREFTABLE.Infrastructure = txtInfrastructure.Text;
                            //objREFTABLE.SyncDate = Convert.ToDateTime(txtSyncDate.Text);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
                            DB.SaveChanges();

                            String url = "update REFTABLE with " + "TenentID = " + TID + "REFID = " + rid;
                            String evantname = "update";
                            String tablename = "REFTABLE";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                            Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                            Clear();
                            //lblMsg.Text = "  Data Edit Successfully";
                            btnAdd.ValidationGroup = "s";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    //scope.Complete(); //  To commit.

                }
                catch (Exception ex)
                {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(ex, true);
                    // Get the top stack frame
                    //var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    //var line = frame.GetFileLineNumber();
                    string st1 = st.ToString();
                    string LineNo = Classes.GlobleClass.LineNo(st1);

                    Classes.GlobleClass.InsertErrorLog1("REFTABLE", "REFTABLE.aspx", eventname, LineNo);
                          //InsertErrorLog(ex);
                    //Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Saving Data !<br>" + ex.ToString(), "Add", Classes.Toastr.ToastPosition.TopCenter);
                    //throw;
                    
                }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            Response.Redirect("REFTABLE.aspx?REFTYPE=Food&REFSUBTYPE=MealType");
        }
        public void FillContractorID()
        {
            //drpSyncDate.Items.Insert(0, new ListItem("-- Select --", "0"));drpSyncDate.DataSource = DB.0;drpSyncDate.DataTextField = "0";drpSyncDate.DataValueField = "0";drpSyncDate.DataBind();
        }

        #region PAge Genarator navigation
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
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            //drpREFID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtREFTYPE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtREFSUBTYPE.Text = Listview1.SelectedDataKey[0].ToString();
            txtSHORTNAME.Text = Listview1.SelectedDataKey[5].ToString();
            txtREFNAME1.Text = Listview1.SelectedDataKey[6].ToString();
            txtREFNAME2.Text = Listview1.SelectedDataKey[7].ToString();
            txtREFNAME3.Text = Listview1.SelectedDataKey[8].ToString();
            txtSWITCH1.Text = Listview1.SelectedDataKey[9].ToString();
            txtSWITCH2.Text = Listview1.SelectedDataKey[10].ToString();
            if (Listview1.SelectedDataKey[11] != null)
                txtSWITCH3.Text = Listview1.SelectedDataKey[11].ToString();
            if (Listview1.SelectedDataKey[12] != null)
                txtSWITCH4.Text = Listview1.SelectedDataKey[12].ToString();
            if (Listview1.SelectedDataKey[13] != null)
                txtRemarks.Text = Listview1.SelectedDataKey[13].ToString();
            if (Listview1.SelectedDataKey[14] == "Y")
            {
                cbACTIVE.Checked = true;
            }
            else
            {
                cbACTIVE.Checked = false;
            }
            //cbACTIVE.Text = Listview1.SelectedDataKey[14].ToString();
            //drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtInfrastructure.Text = Listview1.SelectedDataKey[0].ToString();
            //txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                //drpREFID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtREFTYPE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtREFSUBTYPE.Text = Listview1.SelectedDataKey[0].ToString();
                txtSHORTNAME.Text = Listview1.SelectedDataKey[5].ToString();
                txtREFNAME1.Text = Listview1.SelectedDataKey[6].ToString();
                txtREFNAME2.Text = Listview1.SelectedDataKey[7].ToString();
                txtREFNAME3.Text = Listview1.SelectedDataKey[8].ToString();
                txtSWITCH1.Text = Listview1.SelectedDataKey[9].ToString();
                txtSWITCH2.Text = Listview1.SelectedDataKey[10].ToString();
                txtSWITCH3.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
                if (Listview1.SelectedDataKey[12] != null)
                    txtSWITCH4.Text = Listview1.SelectedDataKey[12].ToString();
                txtRemarks.Text = Listview1.SelectedDataKey[13].ToString();
                if (Listview1.SelectedDataKey[14] == "Y")
                {
                    cbACTIVE.Checked = true;
                }
                else
                {
                    cbACTIVE.Checked = false;
                }
                //drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtInfrastructure.Text = Listview1.SelectedDataKey[0].ToString();
                //txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();

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
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                //drpREFID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtREFTYPE.Text = Listview1.SelectedDataKey[0].ToString();
                //txtREFSUBTYPE.Text = Listview1.SelectedDataKey[0].ToString();
                txtSHORTNAME.Text = Listview1.SelectedDataKey[5].ToString();
                txtREFNAME1.Text = Listview1.SelectedDataKey[6].ToString();
                txtREFNAME2.Text = Listview1.SelectedDataKey[7].ToString();
                txtREFNAME3.Text = Listview1.SelectedDataKey[8].ToString();
                txtSWITCH1.Text = Listview1.SelectedDataKey[9].ToString();
                txtSWITCH2.Text = Listview1.SelectedDataKey[10].ToString();
                txtSWITCH3.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
                if (Listview1.SelectedDataKey[12] != null)
                    txtSWITCH4.Text = Listview1.SelectedDataKey[12].ToString();
                txtRemarks.Text = Listview1.SelectedDataKey[13].ToString();
                if (Listview1.SelectedDataKey[14] == "Y")
                {
                    cbACTIVE.Checked = true;
                }
                else
                {
                    cbACTIVE.Checked = false;
                }
                //drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtInfrastructure.Text = Listview1.SelectedDataKey[0].ToString();
                //txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            //drpREFID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtREFTYPE.Text = Listview1.SelectedDataKey[0].ToString();
            //txtREFSUBTYPE.Text = Listview1.SelectedDataKey[0].ToString();
            txtSHORTNAME.Text = Listview1.SelectedDataKey[5].ToString();
            txtREFNAME1.Text = Listview1.SelectedDataKey[6].ToString();
            txtREFNAME2.Text = Listview1.SelectedDataKey[7].ToString();
            txtREFNAME3.Text = Listview1.SelectedDataKey[8].ToString();
            txtSWITCH1.Text = Listview1.SelectedDataKey[9].ToString();
            txtSWITCH2.Text = Listview1.SelectedDataKey[10].ToString();
            txtSWITCH3.Text = Listview1.SelectedDataKey[11] != null ? Listview1.SelectedDataKey[11].ToString() : "";
            if (Listview1.SelectedDataKey[12] != null)
                txtSWITCH4.Text = Listview1.SelectedDataKey[12].ToString();
            txtRemarks.Text = Listview1.SelectedDataKey[13].ToString();
            if (Listview1.SelectedDataKey[14] == "Y")
            {
                cbACTIVE.Checked = true;
            }
            else
            {
                cbACTIVE.Checked = false;
            }
            //drpCRUP_ID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtInfrastructure.Text = Listview1.SelectedDataKey[0].ToString();
            //txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();

        }

        #endregion

        #region PAge Genarator language

        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    //lblREFNAME12h.Visible = lblREFNAME22h.Visible = lblREFNAME32h.Visible = lblSWITCH12h.Visible = lblSWITCH22h.Visible = lblSWITCH32h.Visible = lblSWITCH42h.Visible = lblRemarks2h.Visible = lblACTIVE2h.Visible = lblCRUP_ID2h.Visible = false;
                    ////2true
                    //txtREFNAME12h.Visible = txtREFNAME22h.Visible = txtREFNAME32h.Visible = txtSWITCH12h.Visible = txtSWITCH22h.Visible = txtSWITCH32h.Visible = txtSWITCH42h.Visible = txtRemarks2h.Visible = txtACTIVE2h.Visible = txtCRUP_ID2h.Visible = true;

                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    //SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    //lblREFNAME12h.Visible = lblREFNAME22h.Visible = lblREFNAME32h.Visible = lblSWITCH12h.Visible = lblSWITCH22h.Visible = lblSWITCH32h.Visible = lblSWITCH42h.Visible = lblRemarks2h.Visible = lblACTIVE2h.Visible = lblCRUP_ID2h.Visible = true;
                    ////2false
                    //txtREFNAME12h.Visible = txtREFNAME22h.Visible = txtREFNAME32h.Visible = txtSWITCH12h.Visible = txtSWITCH22h.Visible = txtSWITCH32h.Visible = txtSWITCH42h.Visible = txtRemarks2h.Visible = txtACTIVE2h.Visible = txtCRUP_ID2h.Visible = false;

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
                    //lblREFNAME11s.Visible = lblREFNAME21s.Visible = lblREFNAME31s.Visible = lblSWITCH11s.Visible = lblSWITCH21s.Visible = lblSWITCH31s.Visible = lblSWITCH41s.Visible = lblRemarks1s.Visible = lblACTIVE1s.Visible = lblCRUP_ID1s.Visible = false;
                    ////1true
                    //txtREFNAME11s.Visible = txtREFNAME21s.Visible = txtREFNAME31s.Visible = txtSWITCH11s.Visible = txtSWITCH21s.Visible = txtSWITCH31s.Visible = txtSWITCH41s.Visible = txtRemarks1s.Visible = txtACTIVE1s.Visible = txtCRUP_ID1s.Visible = true;
                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    //SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    //lblREFNAME11s.Visible = lblREFNAME21s.Visible = lblREFNAME31s.Visible = lblSWITCH11s.Visible = lblSWITCH21s.Visible = lblSWITCH31s.Visible = lblSWITCH41s.Visible = lblRemarks1s.Visible = lblACTIVE1s.Visible = lblCRUP_ID1s.Visible = true;
                    ////1false
                    //txtREFNAME11s.Visible = txtREFNAME21s.Visible = txtREFNAME31s.Visible = txtSWITCH11s.Visible = txtSWITCH21s.Visible = txtSWITCH31s.Visible = txtSWITCH41s.Visible = txtRemarks1s.Visible = txtACTIVE1s.Visible = txtCRUP_ID1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        //public void RecieveLabel(string lang)
        //{
        //    string str = "";
        //    string PID = ((HRMMater)this.Master).getOwnPage();

        //    List<Database.TBLLabelDTL> List = ((HRMMater)this.Master).Bindxml("REFTABLE.xml").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
        //    foreach (Database.TBLLabelDTL item in List)
        //    {
        //        if (lblREFNAME11s.ID == item.LabelID)
        //            txtREFNAME11s.Text = lblREFNAME11s.Text = lblhREFNAME1.Text = item.LabelName;
        //        else if (lblREFNAME21s.ID == item.LabelID)
        //            txtREFNAME21s.Text = lblREFNAME21s.Text = lblhREFNAME2.Text = item.LabelName;
        //        else if (lblREFNAME31s.ID == item.LabelID)
        //            txtREFNAME31s.Text = lblREFNAME31s.Text = lblhREFNAME3.Text = item.LabelName;
        //        else if (lblSWITCH11s.ID == item.LabelID)
        //            txtSWITCH11s.Text = lblSWITCH11s.Text = lblhSWITCH1.Text = item.LabelName;
        //        else if (lblSWITCH21s.ID == item.LabelID)
        //            txtSWITCH21s.Text = lblSWITCH21s.Text = lblhSWITCH2.Text = item.LabelName;
        //        else if (lblSWITCH31s.ID == item.LabelID)
        //            txtSWITCH31s.Text = lblSWITCH31s.Text = lblhSWITCH3.Text = item.LabelName;
        //        else if (lblSWITCH41s.ID == item.LabelID)
        //            txtSWITCH41s.Text = lblSWITCH41s.Text = lblhSWITCH4.Text = item.LabelName;
        //        else if (lblRemarks1s.ID == item.LabelID)
        //            txtRemarks1s.Text = lblRemarks1s.Text = lblhRemarks.Text = item.LabelName;
        //        else if (lblACTIVE1s.ID == item.LabelID)
        //            txtACTIVE1s.Text = lblACTIVE1s.Text = lblhACTIVE.Text = item.LabelName;
        //        else if (lblCRUP_ID1s.ID == item.LabelID)
        //            txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = lblhCRUP_ID.Text = item.LabelName;

        //        if (lblREFNAME12h.ID == item.LabelID)
        //            txtREFNAME12h.Text = lblREFNAME12h.Text = lblhREFNAME1.Text = item.LabelName;
        //        else if (lblREFNAME22h.ID == item.LabelID)
        //            txtREFNAME22h.Text = lblREFNAME22h.Text = lblhREFNAME2.Text = item.LabelName;
        //        else if (lblREFNAME32h.ID == item.LabelID)
        //            txtREFNAME32h.Text = lblREFNAME32h.Text = lblhREFNAME3.Text = item.LabelName;
        //        else if (lblSWITCH12h.ID == item.LabelID)
        //            txtSWITCH12h.Text = lblSWITCH12h.Text = lblhSWITCH1.Text = item.LabelName;
        //        else if (lblSWITCH22h.ID == item.LabelID)
        //            txtSWITCH22h.Text = lblSWITCH22h.Text = lblhSWITCH2.Text = item.LabelName;
        //        else if (lblSWITCH32h.ID == item.LabelID)
        //            txtSWITCH32h.Text = lblSWITCH32h.Text = lblhSWITCH3.Text = item.LabelName;
        //        else if (lblSWITCH42h.ID == item.LabelID)
        //            txtSWITCH42h.Text = lblSWITCH42h.Text = lblhSWITCH4.Text = item.LabelName;
        //        else if (lblRemarks2h.ID == item.LabelID)
        //            txtRemarks2h.Text = lblRemarks2h.Text = lblhRemarks.Text = item.LabelName;
        //        else if (lblACTIVE2h.ID == item.LabelID)
        //            txtACTIVE2h.Text = lblACTIVE2h.Text = lblhACTIVE.Text = item.LabelName;
        //        else if (lblCRUP_ID2h.ID == item.LabelID)
        //            txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = lblhCRUP_ID.Text = item.LabelName;

        //        else
        //            txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
        //    }

        //}
        //public void SaveLabel(string lang)
        //{
        //    string PID = ((HRMMater)this.Master).getOwnPage();
        //    //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
        //    List<Database.TBLLabelDTL> List = ((HRMMater)this.Master).Bindxml("REFTABLE.xml").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(Server.MapPath("\\xml\\REFTABLE.xml"));
        //    foreach (Database.TBLLabelDTL item in List)
        //    {

        //        var obj = ((HRMMater)this.Master).Bindxml().Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
        //        int i = obj.ID - 1;

        //        if (lblREFNAME11s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtREFNAME11s.Text;
        //        else if (lblREFNAME21s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtREFNAME21s.Text;
        //        else if (lblREFNAME31s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtREFNAME31s.Text;
        //        else if (lblSWITCH11s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH11s.Text;
        //        else if (lblSWITCH21s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH21s.Text;
        //        else if (lblSWITCH31s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH31s.Text;
        //        else if (lblSWITCH41s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH41s.Text;
        //        else if (lblRemarks1s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtRemarks1s.Text;
        //        else if (lblACTIVE1s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE1s.Text;
        //        else if (lblCRUP_ID1s.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

        //        if (lblREFNAME12h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtREFNAME12h.Text;
        //        else if (lblREFNAME22h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtREFNAME22h.Text;
        //        else if (lblREFNAME32h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtREFNAME32h.Text;
        //        else if (lblSWITCH12h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH12h.Text;
        //        else if (lblSWITCH22h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH22h.Text;
        //        else if (lblSWITCH32h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH32h.Text;
        //        else if (lblSWITCH42h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtSWITCH42h.Text;
        //        else if (lblRemarks2h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtRemarks2h.Text;
        //        else if (lblACTIVE2h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtACTIVE2h.Text;
        //        else if (lblCRUP_ID2h.ID == item.LabelID)
        //            ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

        //        else
        //            ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
        //    }
        //    ds.WriteXml(Server.MapPath("\\xml\\REFTABLE.xml"));

        //}

        public void ManageLang()
        {
            //for Language

            if (Session["LANGUAGE"] != null)
            {
                //RecieveLabel(Session["LANGUAGE"].ToString());
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

        #endregion
        public void Write()
        {
            //navigation.Visible = false;
            //drpREFID.Enabled = true;
            //txtREFTYPE.Enabled = true;
            //txtREFSUBTYPE.Enabled = true;            
            txtSHORTNAME.Enabled = true;
            txtREFNAME1.Enabled = true;
            txtREFNAME2.Enabled = true;
            txtREFNAME3.Enabled = true;
            txtSWITCH1.Enabled = true;
            txtSWITCH2.Enabled = true;
            txtSWITCH3.Enabled = true;
            txtSWITCH4.Enabled = true;
            txtRemarks.Enabled = true;
            cbACTIVE.Enabled = true;
            //drpCRUP_ID.Enabled = true;
            //txtInfrastructure.Enabled = true;
            //txtSyncDate.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpREFID.Enabled = false;
            //txtREFTYPE.Enabled = false;
            //txtREFSUBTYPE.Enabled = false;
            //txtSHORTNAME.Enabled = false;
            txtSHORTNAME.Enabled = false;
            txtREFNAME1.Enabled = false;
            txtREFNAME2.Enabled = false;
            txtREFNAME3.Enabled = false;
            txtSWITCH1.Enabled = false;
            txtSWITCH2.Enabled = false;
            txtSWITCH3.Enabled = false;
            txtSWITCH4.Enabled = false;
            txtRemarks.Enabled = false;
            cbACTIVE.Enabled = false;
            //drpCRUP_ID.Enabled = false;
            //txtInfrastructure.Enabled = false;
            //txtSyncDate.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    string reftype = Request.QueryString["REFTYPE"].ToString();
        //    string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.REFTABLEs.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.REFTABLEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID && p.REFTYPE == reftype && p.REFSUBTYPE == refsubtype).OrderBy(m => m.REFID).Take(take).Skip(Skip)).ToList());
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

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{
        //    string reftype = Request.QueryString["REFTYPE"].ToString();
        //    string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.REFTABLEs.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.REFTABLEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID && p.REFTYPE == reftype && p.REFSUBTYPE == refsubtype).OrderBy(m => m.REFID).Take(take).Skip(Skip)).ToList());
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
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{
        //    string reftype = Request.QueryString["REFTYPE"].ToString();
        //    string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.REFTABLEs.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.REFTABLEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID && p.REFTYPE == reftype && p.REFSUBTYPE == refsubtype).OrderBy(m => m.REFID).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{
        //    string reftype = Request.QueryString["REFTYPE"].ToString();
        //    string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.REFTABLEs.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.REFTABLEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID && p.REFTYPE == reftype && p.REFSUBTYPE == refsubtype).OrderBy(m => m.REFID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            //Readonly();
            //ManageLang();
            //pnlSuccessMsg.Visible = false;
            //FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            //FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            PnlGriderror.Visible = false;
            lblGridErrormsg.Text = "";

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {

                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int RID = Convert.ToInt32(ID[1]);

                        List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID && p.MealType == RID).ToList();

                        if (ListPlan.Count() > 0)
                        {
                            PnlGriderror.Visible = true;
                            lblGridErrormsg.Text = "This Plan Not to be delete It Used in Plan Meal Setup.";
                            return;
                        }
                        string RefID = RID.ToString();
                        List<Database.tblcontact_addon1_dtl> Listdtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.DeliveryTime == RefID).ToList();

                        if (Listdtl.Count() > 0)
                        {
                            PnlGriderror.Visible = true;
                            lblGridErrormsg.Text = "This Delivery Time Not to be delete It Used in Subcriber Setup.";
                            return;
                        }

                        Database.REFTABLE objj = DB.REFTABLEs.Single(p => p.TenentID == TIDD && p.REFID == RID && p.ACTIVE == "Y");
                        objj.ACTIVE = "N";
                        DB.SaveChanges();

                        String url = "delete REFTABLE with " + "TenentID = " + TID + "REFID = " + RID;
                        String evantname = "delete";
                        String tablename = "REFTABLE";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                        Classes.EcommAdminClass.update_SubcriptionSetup(TID);

                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        //int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        //int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        //((AcmMaster)Page.Master).BindList(Listview1, (DB.REFTABLEs.Where(p=>p.ACTIVE=="Y").OrderBy(m => m.REFID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int RID = Convert.ToInt32(ID[1]);
                        Database.REFTABLE objREFTABLE = DB.REFTABLEs.Single(p => p.TenentID == TIDD && p.REFID == RID && p.ACTIVE == "Y");
                        //drpREFID.SelectedValue = objREFTABLE.REFID.ToString();
                        //txtREFTYPE.Text = objREFTABLE.REFTYPE.ToString();
                        //txtREFSUBTYPE.Text = objREFTABLE.REFSUBTYPE.ToString();
                        txtSHORTNAME.Text = objREFTABLE.SHORTNAME.ToString();
                        txtREFNAME1.Text = objREFTABLE.REFNAME1.ToString();
                        txtREFNAME2.Text = objREFTABLE.REFNAME2.ToString();
                        txtREFNAME3.Text = objREFTABLE.REFNAME3.ToString();
                        txtSWITCH1.Text = objREFTABLE.SWITCH1.ToString();
                        txtSWITCH2.Text = objREFTABLE.SWITCH2.ToString();
                        txtSWITCH3.Text = objREFTABLE.SWITCH3.ToString();
                        if (objREFTABLE.SWITCH4 != null)
                            txtSWITCH4.Text = objREFTABLE.SWITCH4.ToString();
                        txtRemarks.Text = objREFTABLE.Remarks.ToString();
                        if (objREFTABLE.ACTIVE == "Y")
                        {
                            cbACTIVE.Checked = true;
                        }
                        else
                        {
                            cbACTIVE.Checked = false;
                        }
                        //drpCRUP_ID.SelectedValue = objREFTABLE.CRUP_ID.ToString();
                        //txtInfrastructure.Text = objREFTABLE.Infrastructure.ToString();
                        //txtSyncDate.Text = objREFTABLE.SyncDate.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["TIDD"] = TIDD;
                        ViewState["RID"] = RID;
                        Write();
                    }
                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                    throw;
                }
            }
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    string reftype = Request.QueryString["REFTYPE"].ToString();
        //    string refsubtype = Request.QueryString["REFSUBTYPE"].ToString();
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.REFTABLEs.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.REFTABLEs.Where(p => p.ACTIVE == "Y" && p.TenentID == TID && p.REFTYPE == reftype && p.REFSUBTYPE == refsubtype).OrderBy(m => m.REFID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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

        //public void InsertErrorLog(Exception ex)
        //{
        //    string url = string.Empty;
        //    try
        //    {
        //        if (HttpContext.Current != null)
        //        {
        //            string loginUserId = (((USER_MST)Session["USER"]).USER_ID).ToString();

        //            string ipAddress = IPAddress;

        //            string username = HttpContext.Current.Session["Firstname"].ToString();
        //            url = "IP: " + ipAddress + "<br/>" + "UserID: " + loginUserId + " UserName: " + username + "  URL: " +
        //                  HttpContext.Current.Request.Url.ToString();
        //            url += "<br /> UrlReferrer: " + HttpContext.Current.Request.UrlReferrer;

        //            string platfrom, browserName, browserVersion;
        //            GetBrowser(out platfrom, out browserName, out browserVersion);
        //            string browser = "<br />OS: " + platfrom + " Browser: " + browserName + " Version: " +
        //                             browserVersion;
        //            url += browser;

        //            int crup = GlobleClass.EncryptionHelpers.WriteLog(url, "Error", "REFTABLE", UID.ToString(), 0);
        //        }
        //    }
        //    catch { }//Ignore
        //    //ErrorLogBusiness.InsertErrorLog(HttpUtility.HtmlEncode(ex.Message) + "<br/>" + url + "<br/>" + ex.StackTrace);
        //}        
        //public string IPAddress
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (System.ServiceModel.OperationContext.Current != null)
        //            {
        //                System.ServiceModel.OperationContext context = System.ServiceModel.OperationContext.Current;
        //                System.ServiceModel.Channels.MessageProperties properties = context.IncomingMessageProperties;
        //                System.ServiceModel.Channels.RemoteEndpointMessageProperty endpoint = properties[System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name] as System.ServiceModel.Channels.RemoteEndpointMessageProperty;
        //                return endpoint.Address;
        //            }
        //            string result = String.Empty;
        //            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //            if (result != null && result != String.Empty)
        //            {
        //                if (result.IndexOf(".") == -1)
        //                    result = null;
        //                else
        //                {
        //                    if (result.IndexOf(",") != -1)
        //                    {
        //                        //contains ","£¬maybe have more proxy. get the fist proxy inner ip.
        //                        result = result.Replace(" ", "").Replace("\"", "");
        //                        string[] temparyip = result.Split(",;".ToCharArray());
        //                        for (int i = 0; i < temparyip.Length; i++)
        //                        {
        //                            if (IsIPAddress(temparyip[i])
        //                                && temparyip[i].Substring(0, 3) != "10."
        //                                && temparyip[i].Substring(0, 7) != "192.168"
        //                                && temparyip[i].Substring(0, 7) != "172.16.")
        //                            {
        //                                return temparyip[i];    // can't find inner address
        //                            }
        //                        }
        //                    }
        //                    else if (IsIPAddress(result)) //is proxy ip
        //                        return result;
        //                    else
        //                        result = null;
        //                }
        //            }
        //            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //            if (null == result || result == String.Empty)
        //                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //            if (result == null || result == String.Empty)
        //                result = HttpContext.Current.Request.UserHostAddress;
        //            return result;
        //        }
        //        catch
        //        {
        //            return "";
        //        }
        //    }
        //}
        //public bool IsIPAddress(string str1)
        //{
        //    if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
        //    string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
        //    Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
        //    return regex.IsMatch(str1);
        //}
        //public void GetBrowser(out string platForm, out string browserName, out string browserVersion)
        //{
        //    browserName = HttpContext.Current.Request.UserAgent.Contains("Chrome") ? "Chrome" : HttpContext.Current.Request.Browser.Browser;
        //    platForm = HttpContext.Current.Request.UserAgent.ToLower();
        //    if (platForm.Contains("ipod;"))
        //    {
        //        platForm = "iPod";
        //    }
        //    else if (platForm.Contains("iphone;"))
        //    {
        //        platForm = "iPhone";
        //    }
        //    else if (platForm.Contains("iphone simulator;"))
        //    {
        //        platForm = "iPhone";
        //    }
        //    else if (platForm.Contains("macintosh;"))
        //    {
        //        platForm = "Mac";
        //    }
        //    else if (platForm.Contains("ipad;"))
        //    {
        //        platForm = "iPad";
        //    }
        //    else if (platForm.Contains("android"))
        //    {
        //        platForm = "Android";
        //    }
        //    else
        //    {
        //        platForm = HttpContext.Current.Request.Browser.Platform;
        //    }
        //    browserVersion = HttpContext.Current.Request.Browser.Version;
        //}


    }
}