using Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.CRM
{
    public partial class AttachmentMst : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID = 10;
        int TTID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                FillContractorID();
                if (Request.QueryString["MasterID"] != null)
                {
                    int docID = Convert.ToInt32(Request.QueryString["MasterID"]);
                    BindGrid(docID);
                    btnSubmit.Visible = false;
                    FUDoc.Enabled = false;
                    txtAttachmentsDetail.Enabled = false;
                    drpAttachmentType.Enabled = false;


                }
                if (Request.QueryString["AttachID"] != null)
                {
                    int docID = Convert.ToInt32(Request.QueryString["AttachID"]);
                    BindGrid(docID);
                }
                if (Request.QueryString["recodInsert"] != null)
                {
                    int docID = Convert.ToInt32(Request.QueryString["AttachID"]);
                    Listview1.DataSource = DB.tbl_DMSAttachmentMst.Where(p => p.Deleted == true && p.AttachmentById == docID);
                    Listview1.DataBind();
                    pnlform.Visible = false;
                    lblselect.Visible = true;
                    Button1.Visible = true;
                    for (int i = 0; i < Listview1.Items.Count(); i++)
                    {
                        CheckBox cbkselect = (CheckBox)Listview1.Items[i].FindControl("cbkselect");
                        cbkselect.Visible = true;
                    }
                }
            }
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TTID = TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();


        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void BindGrid(int did)
        {
            int REFNO = Convert.ToInt32(Request.QueryString["RefNo"]);
            //int did1 = Convert.ToInt32(Request.QueryString["DID"]);
            //   int set = Convert.ToInt32(drpAttachmentDocument.SelectedValue);
            Listview1.DataSource = DB.tbl_DMSAttachmentMst.Where(p => p.Deleted == true && p.AttachmentById == did && p.ReferenceNo == REFNO);
            Listview1.DataBind();
        }
        public void BindData()
        {
            Listview1.DataSource = DB.tbl_DMSAttachmentMst.Where(p => p.Deleted == true);
            Listview1.DataBind();
        }

        protected void ListAttachmentMst_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ID && p.TenentID == TID);
                        objtbl_DMSAttachmentMst.Deleted = false;
                        DB.SaveChanges();

                        String url = "Delete tbl_DMSAttachmentMst with " + "TenentID = " + TID + "AttachID =" + ID;
                        String evantname = "Delete";
                        String tablename = "tbl_DMSAttachmentMst";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, 0);

                        var List = DB.tbl_DMSAttachmentMst.Where(p => p.ShareID == ID).ToList();
                        for (int i = 0; i < List.Count(); i++)
                        {
                            int ACID = Convert.ToInt32(List[i].AttachID);
                            Database.tbl_DMSAttachmentMst objCRMDMSAttachmentMst = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ACID && p.TenentID == TID);
                            objCRMDMSAttachmentMst.Deleted = false;
                            DB.SaveChanges();
                        }
                        lblMsg.Text = "Data Deleted Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();


                    }
                    if (e.CommandName == "btnEdit")
                    {
                        btnSubmit.Visible = true;
                        btnSubmit.Text = "Update";
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ID && p.TenentID == TID);

                        txtAttachmentsDetail.Text = objtbl_DMSAttachmentMst.AttachmentsDetail.ToString();
                        if (drpAttachmentType.SelectedValue == null || drpAttachmentType.SelectedValue == "")
                        {
                            drpAttachmentType.SelectedValue = "0";
                        }
                        else
                        {
                            drpAttachmentType.SelectedValue = Convert.ToInt32(objtbl_DMSAttachmentMst.AttachmentType).ToString();
                        }


                        String Path = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ID && p.TenentID == TID && p.Deleted == true).AttachmentPath;
                        if (Path == null)
                        {
                            lblPath.Text = null;
                        }
                        else
                        {
                            lblPath.Text = Path.ToString();
                        }

                        ViewState["Edit"] = ID;


                        pnlSuccessMsg.Visible = false;
                    }
                    if (e.CommandName == "btnDownload")
                    {


                        string[] lfillform;
                        lfillform = (e.CommandArgument).ToString().Split(',');
                        int ID = Convert.ToInt32(lfillform[0]);
                        int AttachById1 = Convert.ToInt32(lfillform[1]);
                        int SerialNo = Convert.ToInt32(lfillform[2]);
                        int RefNo = Convert.ToInt32(lfillform[3]);
                        Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ID && p.TenentID == TID && p.AttachmentById == AttachById1 && p.Serialno == SerialNo && p.ReferenceNo == RefNo);

                        if (objtbl_DMSAttachmentMst.AttachmentPath != null)
                        {

                            Label lblAttachmentPath = (Label)e.Item.FindControl("lblName");
                            Response.Clear();
                            Response.Buffer = true;

                            Response.ContentType = "application/x-cdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + lblAttachmentPath.Text.ToString());
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            // Response.BinaryWrite((byte[])dr["data"]);
                            //Response.BinaryWrite((byte[])dr["data"]);

                            Response.TransmitFile(Server.MapPath(lblAttachmentPath.Text.ToString()));

                            Response.End();
                        }
                        else
                        {
                            return;
                        }
                    }

                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttachmentMst.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //string fullpath = "";

                    if (ViewState["Edit"] != null)
                    {
                        string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                        int ID = Convert.ToInt32(ViewState["Edit"]);
                        Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ID && p.TenentID == TID);
                        objtbl_DMSAttachmentMst.AttachmentsDetail = txtAttachmentsDetail.Text;
                        List<Database.tbl_DMSAttachmentMst> HWList = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentPath == FUDoc.FileName).ToList();
                        if (HWList.Count() > 0)
                        {
                            lblMsg.Text = "Duplicate File Name Found";
                            pnlSuccessMsg.Visible = true;
                            return;
                        }

                        if (FUDoc.HasFile)
                        {
                            FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                            objtbl_DMSAttachmentMst.AttachmentPath = FUDoc.FileName;
                        }
                        lblPath.Visible = false;
                        if (drpAttachmentType.SelectedValue == null || drpAttachmentType.SelectedValue == "")
                        {
                            objtbl_DMSAttachmentMst.AttachmentType = "0";
                        }
                        else
                        {
                            objtbl_DMSAttachmentMst.AttachmentType = drpAttachmentType.SelectedValue;

                        }

                        objtbl_DMSAttachmentMst.Deleted = true;
                        objtbl_DMSAttachmentMst.Actived = true;
                        objtbl_DMSAttachmentMst.CreatedDate = DateTime.Now;
                        ViewState["Edit"] = null;
                        DB.SaveChanges();

                        String url = "update tbl_DMSAttachmentMst with " + "TenentID = " + TID + "AttachID =" + ID;
                        String evantname = "Update";
                        String tablename = "tbl_DMSAttachmentMst";
                        string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                        Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, 0);

                        var List = DB.tbl_DMSAttachmentMst.Where(p => p.ShareID == ID).ToList();
                        for (int i = 0; i < List.Count(); i++)
                        {
                            int ACID = Convert.ToInt32(List[i].AttachID);
                            Database.tbl_DMSAttachmentMst objCRMDMSAttachmentMst = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ACID && p.TenentID == TID);
                            objCRMDMSAttachmentMst.AttachmentsDetail = txtAttachmentsDetail.Text;
                            if (FUDoc.HasFile)
                            {
                                FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                                objCRMDMSAttachmentMst.AttachmentPath = FUDoc.FileName;
                            }
                            lblPath.Visible = false;
                            if (drpAttachmentType.SelectedValue == null || drpAttachmentType.SelectedValue == "")
                            {
                                objCRMDMSAttachmentMst.AttachmentType = "0";
                            }
                            else
                            {
                                objCRMDMSAttachmentMst.AttachmentType = drpAttachmentType.SelectedValue;
                            }

                            objCRMDMSAttachmentMst.Deleted = true;
                            objCRMDMSAttachmentMst.Actived = true;
                            objCRMDMSAttachmentMst.CreatedDate = DateTime.Now;
                            DB.SaveChanges();

                            String url2 = "update tbl_DMSAttachmentMst with " + "TenentID = " + TID + "AttachID =" + ACID;
                            String evantname3 = "Update";
                            String tablename3 = "tbl_DMSAttachmentMst";
                            string loginUserId3 = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url2, evantname3, tablename3, loginUserId.ToString(), 0, 0);

                        }
                        lblMsg.Text = "  Data Edit Successfully";
                        pnlSuccessMsg.Visible = true;
                        btnSubmit.Text = "Add";
                    }
                    else
                    {
                        Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = new Database.tbl_DMSAttachmentMst();
                        //Server Content Send data Yogesh
                        string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                        string did1 = Request.QueryString["DID"];
                        int docID = Convert.ToInt32(Request.QueryString["AttachID"]);
                        int REFNO = Convert.ToInt32(Request.QueryString["RefNo"]);
                        int UsarName = Convert.ToInt32(Request.QueryString["UID"]);
                        string front = "111001";
                        string back = "111002";
                        if (DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentById == docID && p.AttachmentByName == did1 && (p.AttachmentType == front || p.AttachmentType == back)).Count() > 0)
                        {
                            //lblMsg.Text = "You Want to insert Fron And Back Image Only One...";
                            //pnlSuccessMsg.Visible = true;
                            Database.tbl_DMSAttachmentMst objattach = DB.tbl_DMSAttachmentMst.Single(p => p.TenentID == TID && p.AttachmentById == docID && p.AttachmentByName == did1 && (p.AttachmentType == front || p.AttachmentType == back));
                            DB.tbl_DMSAttachmentMst.DeleteObject(objattach);
                            DB.SaveChanges();

                            objtbl_DMSAttachmentMst.AttachID = DB.tbl_DMSAttachmentMst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSAttachmentMst.Max(p => p.AttachID) + 1) : 1;
                            objtbl_DMSAttachmentMst.AttachmentsDetail = txtAttachmentsDetail.Text;
                            objtbl_DMSAttachmentMst.AttachmentByName = did1;
                            objtbl_DMSAttachmentMst.ReferenceNo = REFNO;
                            List<Database.tbl_DMSAttachmentMst> HWList = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentPath == FUDoc.FileName).ToList();
                            if (HWList.Count() > 0)
                            {
                                lblMsg.Text = "Duplicate File Name Found";
                                pnlSuccessMsg.Visible = true;
                                return;
                            }
                            if (FUDoc.HasFile)
                            {
                                FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                                objtbl_DMSAttachmentMst.AttachmentPath = FUDoc.FileName;
                            }

                            objtbl_DMSAttachmentMst.AttachmentById = docID;
                            int SeqCount = DB.tbl_DMSAttachmentMst.Where(p => p.AttachmentById == docID && p.ReferenceNo == REFNO).Count();
                            if (SeqCount > 0)
                                objtbl_DMSAttachmentMst.Serialno = SeqCount + 1;
                            else
                                objtbl_DMSAttachmentMst.Serialno = 1;
                            if (drpAttachmentType.SelectedValue == null || drpAttachmentType.SelectedValue == "")
                            {
                                objtbl_DMSAttachmentMst.AttachmentType = "0";
                            }
                            else
                            {
                                objtbl_DMSAttachmentMst.AttachmentType = drpAttachmentType.SelectedValue;
                            }

                            objtbl_DMSAttachmentMst.TenentID = TID;
                            objtbl_DMSAttachmentMst.CATID = UsarName;
                            objtbl_DMSAttachmentMst.Actived = true;
                            objtbl_DMSAttachmentMst.CreatedDate = DateTime.Now;
                            objtbl_DMSAttachmentMst.Deleted = true;
                            DB.tbl_DMSAttachmentMst.AddObject(objtbl_DMSAttachmentMst);
                            lblMsg.Text = "  Data Save Successfully";
                            pnlSuccessMsg.Visible = true;
                            DB.SaveChanges();

                            String url = "insert new record in tbl_DMSAttachmentMst with " + "TenentID = " + TID + "AttachID =" + objtbl_DMSAttachmentMst.AttachID + "Serialno =" + objtbl_DMSAttachmentMst.Serialno + "ReferenceNo =" + REFNO + "AttachmentById =" + docID;
                            String evantname = "create";
                            String tablename = "tbl_DMSAttachmentMst";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, 0);
                            BindGrid(docID);

                        }
                        else
                        {

                            objtbl_DMSAttachmentMst.AttachID = DB.tbl_DMSAttachmentMst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSAttachmentMst.Max(p => p.AttachID) + 1) : 1;
                            objtbl_DMSAttachmentMst.AttachmentsDetail = txtAttachmentsDetail.Text;
                            objtbl_DMSAttachmentMst.AttachmentByName = did1;
                            objtbl_DMSAttachmentMst.ReferenceNo = REFNO;
                            List<Database.tbl_DMSAttachmentMst> HWList = DB.tbl_DMSAttachmentMst.Where(p => p.TenentID == TID && p.AttachmentPath == FUDoc.FileName).ToList();
                            if (HWList.Count() > 0)
                            {
                                lblMsg.Text = "Duplicate File Name Found";
                                pnlSuccessMsg.Visible = true;
                                return;
                            }
                            if (FUDoc.HasFile)
                            {
                                FUDoc.SaveAs(Server.MapPath("~/Gallery/") + FUDoc.FileName);
                                objtbl_DMSAttachmentMst.AttachmentPath = FUDoc.FileName;
                            }

                            objtbl_DMSAttachmentMst.AttachmentById = docID;
                            int SeqCount = DB.tbl_DMSAttachmentMst.Where(p => p.AttachmentById == docID && p.ReferenceNo == REFNO).Count();
                            if (SeqCount > 0)
                                objtbl_DMSAttachmentMst.Serialno = SeqCount + 1;
                            else
                                objtbl_DMSAttachmentMst.Serialno = 1;
                            if (drpAttachmentType.SelectedValue == null || drpAttachmentType.SelectedValue == "")
                            {
                                objtbl_DMSAttachmentMst.AttachmentType = "0";
                            }
                            else
                            {
                                objtbl_DMSAttachmentMst.AttachmentType = drpAttachmentType.SelectedValue;
                            }

                            objtbl_DMSAttachmentMst.TenentID = TID;
                            objtbl_DMSAttachmentMst.CATID = UsarName;
                            objtbl_DMSAttachmentMst.Actived = true;
                            objtbl_DMSAttachmentMst.CreatedDate = DateTime.Now;
                            objtbl_DMSAttachmentMst.Deleted = true;
                            DB.tbl_DMSAttachmentMst.AddObject(objtbl_DMSAttachmentMst);
                            lblMsg.Text = "  Data Save Successfully";
                            pnlSuccessMsg.Visible = true;
                            DB.SaveChanges();

                            String url = "insert new record in tbl_DMSAttachmentMst with " + "TenentID = " + TID + "AttachID =" + objtbl_DMSAttachmentMst.AttachID + "Serialno =" + objtbl_DMSAttachmentMst.Serialno + "ReferenceNo =" + REFNO + "AttachmentById =" + docID;
                            String evantname = "create";
                            String tablename = "tbl_DMSAttachmentMst";
                            string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                            Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, 0);
                            BindGrid(docID);

                        }

                    }

                    scope.Complete(); //  To commit.
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public void FillContractorID()
        {
            int TID = 10;//Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //DID=CompanyMaster
            if (Request.QueryString["DID"] != null)
            {
                drpAttachmentType.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "CRM" && P.REFSUBTYPE == "Attachment" && P.TenentID == TID).OrderBy(a => a.REFNAME1);
                drpAttachmentType.DataTextField = "REFNAME1";
                drpAttachmentType.DataValueField = "REFID";
                drpAttachmentType.DataBind();
            }
            else
            {
                drpAttachmentType.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == "DMS" && P.REFSUBTYPE == "Attachment" && P.TenentID == TID).OrderBy(a => a.REFNAME1);
                drpAttachmentType.DataTextField = "REFNAME1";
                drpAttachmentType.DataValueField = "REFID";
                drpAttachmentType.DataBind();
            }
            //drpAttachmentDocument.DataSource = DB.CRM_REFTABLE.Where(P => P.REFTYPE == "DMS" && P.REFSUBTYPE == "DOCTYPE").OrderBy(a => a.REFNAME1);
            //drpAttachmentDocument.DataTextField = "REFNAME1";
            //drpAttachmentDocument.DataValueField = "REFID";
            //drpAttachmentDocument.DataBind();
        }
        public string getAttachmentType(int PID)
        {
            int TID = 10;//Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (PID != 0)
                return DB.REFTABLEs.Single(p => p.REFID == PID && p.TenentID == TID).REFNAME1;
            else
                return "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string FInrlUrl = "";
            string URL = Session["ADMInPrevious"].ToString();
            if (Request.QueryString["Mode"] != null)
            {
                string Mode = Request.QueryString["Mode"].ToString();
                FInrlUrl = URL + "&Mode=" + Mode;
                Response.Redirect(FInrlUrl);
            }
            else
            {
                Response.Redirect(URL);
            }

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int ACMBYID = Convert.ToInt32(Request.QueryString["recodInsert"]);
            for (int i = 0; i < Listview1.Items.Count(); i++)
            {
                CheckBox cbkselect = (CheckBox)Listview1.Items[i].FindControl("cbkselect");
                bool select = cbkselect.Checked;
                if (select == true)
                {
                    Label lblAttachID = (Label)Listview1.Items[i].FindControl("lblAttachID");
                    int ATID = Convert.ToInt32(lblAttachID.Text);
                    Database.tbl_DMSAttachmentMst objCRMatt = DB.tbl_DMSAttachmentMst.Single(p => p.AttachID == ATID);
                    Database.tbl_DMSAttachmentMst objtbl_DMSAttachmentMst = new Database.tbl_DMSAttachmentMst();
                    string UID = (((USER_MST)Session["USER"]).LOGIN_ID);
                    string did1 = Request.QueryString["DID"];
                    int docID = Convert.ToInt32(Request.QueryString["AttachID"]);
                    int REFNO = Convert.ToInt32(Request.QueryString["RefNo"]);
                    objtbl_DMSAttachmentMst.TenentID = TID;
                    objtbl_DMSAttachmentMst.AttachID = DB.tbl_DMSAttachmentMst.Count() > 0 ? Convert.ToInt32(DB.tbl_DMSAttachmentMst.Max(p => p.AttachID) + 1) : 1;
                    int SeqCount = DB.tbl_DMSAttachmentMst.Where(p => p.AttachmentById == ATID && p.ReferenceNo == REFNO).Count();
                    if (SeqCount > 0)
                        objtbl_DMSAttachmentMst.Serialno = SeqCount + 1;
                    else
                        objtbl_DMSAttachmentMst.Serialno = 1;
                    objtbl_DMSAttachmentMst.ReferenceNo = REFNO;
                    objtbl_DMSAttachmentMst.AttachmentById = ACMBYID;
                    objtbl_DMSAttachmentMst.AttachmentByName = "CompanyMaster";
                    objtbl_DMSAttachmentMst.AttachmentsDetail = objCRMatt.AttachmentsDetail;
                    objtbl_DMSAttachmentMst.AttachmentType = objCRMatt.AttachmentType;
                    objtbl_DMSAttachmentMst.AttachmentPath = objCRMatt.AttachmentPath;
                    objtbl_DMSAttachmentMst.ShareID = ATID;
                    objtbl_DMSAttachmentMst.TenentID = TID;
                    objtbl_DMSAttachmentMst.Actived = true;
                    objtbl_DMSAttachmentMst.CreatedDate = DateTime.Now;
                    objtbl_DMSAttachmentMst.Deleted = true;
                    DB.tbl_DMSAttachmentMst.AddObject(objtbl_DMSAttachmentMst);
                    lblMsg.Text = "  Data Share Successfully";
                    pnlSuccessMsg.Visible = true;
                    DB.SaveChanges();

                    String url = "insert new record in tbl_DMSAttachmentMst with " + "TenentID = " + TID + "AttachID =" + objtbl_DMSAttachmentMst.AttachID + "Serialno =" + objtbl_DMSAttachmentMst.Serialno + "ReferenceNo =" + REFNO + "AttachmentById =" + ACMBYID;
                    String evantname = "create";
                    String tablename = "tbl_DMSAttachmentMst";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0, 0);
                }
            }
        }



        //private string ExtractTextFromImage(string filePath)
        //{
        //    //MODI.Document modiDocument = new MODI.Document();           
        //    //modiDocument.Create(filePath); 

        //    //modiDocument.OCR(MiLANGUAGES.miLANG_ENGLISH, true, true);
        //    //MODI.Image modiImage = (modiDocument.Images[0] as MODI.Image);
        //    //string extractedText = modiImage.Layout.Text;
        //    //modiDocument.Close();
        //    //return extractedText;


        //    IEnumerator files = Directory.GetFiles(filePath).GetEnumerator();
        //    while (files.MoveNext())
        //    {
        //        //get file extension 
        //        string fileExtension = Path.GetExtension(Convert.ToString(files.Current));

        //        //get file name without extenstion 
        //        string fileName = Convert.ToString(files.Current).Replace(fileExtension, string.Empty);

        //        //Check for JPG File Format 
        //        if (fileExtension == ".jpg" || fileExtension == ".JPG" || fileExtension == ".png") // or // ImageFormat.Jpeg.ToString()
        //        {
        //            try
        //            {
        //                //OCR Operations ... 
        //                MODI.Document md = new MODI.Document();
        //                md.Create(Convert.ToString(files.Current));
        //                md.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);
        //                MODI.Image image = (MODI.Image)md.Images[0];
        //                //create text file with the same Image file name 
        //                FileStream createFile = new FileStream(fileName + ".txt", FileMode.CreateNew);

        //                //save the image text in the text file 
        //                StreamWriter writeFile = new StreamWriter(createFile);
        //                writeFile.Write(image.Layout.Text);
        //                writeFile.Close();
        //            }
        //            catch (Exception)
        //            {
        //                //MessageBox.Show("This Image hasn't a text or has a problem",
        //                //"OCR Notifications",
        //                //MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    } 
        //}
    }
}