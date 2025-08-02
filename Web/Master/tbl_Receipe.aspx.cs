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
    public partial class tbl_Receipe : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        int TID, LID, UID, EMPID, CID = 0;
        string LangID = "";
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            SessionLoad();
            if (!IsPostBack)
            {
                string uidd = ((USER_MST)Session["USER"]).USER_ID.ToString();
                bool Isadmin = Classes.CRMClass.ISAdmin(TID, uidd);
                if (Isadmin == false)
                {
                    btnEditLable.Visible = false;
                }
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                FirstData();

                btnAdd.ValidationGroup = "s";

            }
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
        }
        #region Step2
        public void BindData()
        {
            List<Database.tbl_Receipe> List = DB.tbl_Receipe.Where(p => p.TenentID == TID).OrderByDescending(p=>p.recNo).ToList();

            Listview1.DataSource = List;
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lblReceipe_English1s.Attributes["class"] = lblReceipe_Arabic1s.Attributes["class"] = lblExpireDays1s.Attributes["class"] = "control-label col-md-4  getshow";//lblUploadDate1s.Attributes["class"] = lblUploadby1s.Attributes["class"] =
            lblReceipe_English2h.Attributes["class"] = lblReceipe_Arabic2h.Attributes["class"] = lblExpireDays2h.Attributes["class"] = "control-label col-md-4  gethide";// lblUploadDate2h.Attributes["class"] = lblUploadby2h.Attributes["class"] =
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblReceipe_English1s.Attributes["class"] = lblReceipe_Arabic1s.Attributes["class"] = lblExpireDays1s.Attributes["class"] = "control-label col-md-4  gethide"; // lblUploadDate1s.Attributes["class"] = lblUploadby1s.Attributes["class"] =
            lblReceipe_English2h.Attributes["class"] = lblReceipe_Arabic2h.Attributes["class"] = lblExpireDays2h.Attributes["class"] = "control-label col-md-4  getshow"; // lblUploadDate2h.Attributes["class"] = lblUploadby2h.Attributes["class"] = 
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
            //drprecNo.SelectedIndex = 0;
            txtReceipe_English.Text = "";
            txtReceipe_Arabic.Text = "";
            txtExpireDays.Text = "";
            txtProductionDays.Text = "";
            //txtUploadDate.Text = "";
            //txtUploadby.Text = "";
            //txtSyncDate.Text = "";
            //txtSyncby.Text = "";
            //drpSynID.SelectedIndex = 0;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
           
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
                        Database.tbl_Receipe objtbl_Receipe = new Database.tbl_Receipe();
                        //Server Content Send data Yogesh
                        objtbl_Receipe.TenentID = TID;
                        objtbl_Receipe.recNo = DB.tbl_Receipe.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.tbl_Receipe.Where(p => p.TenentID == TID).Max(p => p.recNo)) + 1 : 1;
                        objtbl_Receipe.Prefix = "ONL";
                        objtbl_Receipe.Receipe_English = txtReceipe_English.Text;
                        objtbl_Receipe.Receipe_Arabic = txtReceipe_Arabic.Text;
                        objtbl_Receipe.RecType = TypeOfReceipe.SelectedValue;
                        objtbl_Receipe.ProdDays = Convert.ToDecimal(txtProductionDays.Text);
                        objtbl_Receipe.ExpireDays = txtExpireDays.Text;
                        objtbl_Receipe.UploadDate = DateTime.Now;
                        objtbl_Receipe.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME; 
                        objtbl_Receipe.SynID = 1;

                        DB.tbl_Receipe.AddObject(objtbl_Receipe);
                        DB.SaveChanges();
                       
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        btnAdd.Text = "Add New";
                        //navigation.Visible = true;
                        Readonly();
                        FirstData();
                        btnAdd.ValidationGroup = "s";

                    }
                    else if (btnAdd.Text == "Update")
                    {
                        if (ViewState["Edit"] != null && ViewState["Prefix"] != null)
                        {
                            int recNo = Convert.ToInt32(ViewState["Edit"]);
                            string PREFIX = ViewState["Prefix"].ToString();

                            if (DB.tbl_Receipe.Where(p => p.TenentID == TID && p.recNo == recNo && p.Prefix == PREFIX).Count() > 0)
                            {
                                Database.tbl_Receipe objtbl_Receipe = DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == recNo && p.Prefix == PREFIX);

                                objtbl_Receipe.Receipe_English = txtReceipe_English.Text;
                                objtbl_Receipe.Receipe_Arabic = txtReceipe_Arabic.Text;
                                objtbl_Receipe.ProdDays = Convert.ToDecimal(txtProductionDays.Text);
                                objtbl_Receipe.RecType = TypeOfReceipe.SelectedValue;
                                objtbl_Receipe.ExpireDays = txtExpireDays.Text;
                                objtbl_Receipe.UploadDate = DateTime.Now;
                                objtbl_Receipe.Uploadby = ((USER_MST)Session["USER"]).FIRST_NAME;
                                objtbl_Receipe.SynID = 2;

                                ViewState["Edit"] = null;
                                ViewState["Prefix"] = null;
                                btnAdd.Text = "Add New";
                                DB.SaveChanges();
                              
                                Clear();
                                lblMsg.Text = "  Data Edit Successfully";
                                pnlSuccessMsg.Visible = true;
                                btnAdd.ValidationGroup = "s";
                            }
                            //navigation.Visible = true;
                            Readonly();
                            FirstData();
                        }

                        btnAdd.ValidationGroup = "s";
                    }
                    BindData();

                    

                }
                catch (Exception ex)
                {
                    throw;
                }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Master/tbl_Receipe.aspx");            
        }
        public void FillContractorID()
        {
            //drpSynID.Items.Insert(0, new ListItem("-- Select --", "0"));drpSynID.DataSource = DB.0;drpSynID.DataTextField = "0";drpSynID.DataValueField = "0";drpSynID.DataBind();
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
        public void ManageFL()
        {
            if (Listview1.Items.Count > 0)
            {
                txtReceipe_English.Text = Listview1.SelectedDataKey[2] != null ? Listview1.SelectedDataKey[2].ToString() : "";
                txtReceipe_Arabic.Text = Listview1.SelectedDataKey[3] != null ? Listview1.SelectedDataKey[3].ToString() : "";
                txtExpireDays.Text = Listview1.SelectedDataKey[4] != null ? Listview1.SelectedDataKey[4].ToString() : "";
                txtProductionDays.Text = Listview1.SelectedDataKey[5] != null ? Listview1.SelectedDataKey[5].ToString() : "";
                //txtUploadDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txtUploadby.Text = Listview1.SelectedDataKey[0].ToString();
                //txtSyncDate.Text = Listview1.SelectedDataKey[0].ToString();
                //txtSyncby.Text = Listview1.SelectedDataKey[0].ToString();
                //drpSynID.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            }
        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            //drprecNo.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            ManageFL();
        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                ManageFL();
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
                ManageFL();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            ManageFL();
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblReceipe_English2h.Visible = lblReceipe_Arabic2h.Visible = lblExpireDays2h.Visible = false; //lblUploadDate2h.Visible = lblUploadby2h.Visible = 
                    //2true
                    txtReceipe_English2h.Visible = txtReceipe_Arabic2h.Visible = txtExpireDays2h.Visible = true; // txtUploadDate2h.Visible = txtUploadby2h.Visible = 

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
                    lblReceipe_English2h.Visible = lblReceipe_Arabic2h.Visible = lblExpireDays2h.Visible = true; // lblUploadDate2h.Visible = lblUploadby2h.Visible = 
                    //2false
                    txtReceipe_English2h.Visible = txtReceipe_Arabic2h.Visible = txtExpireDays2h.Visible = false; // txtUploadDate2h.Visible = txtUploadby2h.Visible = 

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
                    lblReceipe_English1s.Visible = lblReceipe_Arabic1s.Visible = lblExpireDays1s.Visible = false; // lblUploadDate1s.Visible = lblUploadby1s.Visible = 
                    //1true
                    txtReceipe_English1s.Visible = txtReceipe_Arabic1s.Visible = txtExpireDays1s.Visible = true; // txtUploadDate1s.Visible = txtUploadby1s.Visible =
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
                    lblReceipe_English1s.Visible = lblReceipe_Arabic1s.Visible = lblExpireDays1s.Visible = true; //lblUploadDate1s.Visible = lblUploadby1s.Visible = 
                    //1false
                    txtReceipe_English1s.Visible = txtReceipe_Arabic1s.Visible = txtExpireDays1s.Visible = false; //txtUploadDate1s.Visible = txtUploadby1s.Visible =
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_Receipe").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblReceipe_English1s.ID == item.LabelID)
                    txtReceipe_English1s.Text = lblReceipe_English1s.Text = lblhReceipe_English.Text = item.LabelName;
                else if (lblReceipe_Arabic1s.ID == item.LabelID)
                    txtReceipe_Arabic1s.Text = lblReceipe_Arabic1s.Text = lblhReceipe_Arabic.Text = item.LabelName;
                else if (lblExpireDays1s.ID == item.LabelID)
                    txtExpireDays1s.Text = lblExpireDays1s.Text = lblhExpireDays.Text = item.LabelName;
                //else if (lblUploadDate1s.ID == item.LabelID)
                //    txtUploadDate1s.Text = lblUploadDate1s.Text = lblhUploadDate.Text = item.LabelName;
                //else if (lblUploadby1s.ID == item.LabelID)
                //    txtUploadby1s.Text = lblUploadby1s.Text = lblhUploadby.Text = item.LabelName;

                else if (lblReceipe_English2h.ID == item.LabelID)
                    txtReceipe_English2h.Text = lblReceipe_English2h.Text = lblhReceipe_English.Text = item.LabelName;
                else if (lblReceipe_Arabic2h.ID == item.LabelID)
                    txtReceipe_Arabic2h.Text = lblReceipe_Arabic2h.Text = lblhReceipe_Arabic.Text = item.LabelName;
                else if (lblExpireDays2h.ID == item.LabelID)
                    txtExpireDays2h.Text = lblExpireDays2h.Text = lblhExpireDays.Text = item.LabelName;
                //else if (lblUploadDate2h.ID == item.LabelID)
                //    txtUploadDate2h.Text = lblUploadDate2h.Text = lblhUploadDate.Text = item.LabelName;
                //else if (lblUploadby2h.ID == item.LabelID)
                //    txtUploadby2h.Text = lblUploadby2h.Text = lblhUploadby.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("tbl_Receipe").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\tbl_Receipe.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("tbl_Receipe").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblReceipe_English1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReceipe_English1s.Text;
                else if (lblReceipe_Arabic1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReceipe_Arabic1s.Text;
                else if (lblExpireDays1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtExpireDays1s.Text;
                //else if (lblUploadDate1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUploadDate1s.Text;
                //else if (lblUploadby1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUploadby1s.Text;

                else if (lblReceipe_English2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReceipe_English2h.Text;
                else if (lblReceipe_Arabic2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtReceipe_Arabic2h.Text;
                else if (lblExpireDays2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtExpireDays2h.Text;
                //else if (lblUploadDate2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUploadDate2h.Text;
                //else if (lblUploadby2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtUploadby2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\tbl_Receipe.xml"));

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
            //navigation.Visible = false;
            //drprecNo.Enabled = true;
            txtReceipe_English.Enabled = true;
            txtReceipe_Arabic.Enabled = true;
            txtExpireDays.Enabled = true;
            txtProductionDays.Enabled = true;
            TypeOfReceipe.Enabled = true;
            //txtUploadDate.Enabled = true;
            //txtUploadby.Enabled = true;
            //txtSyncDate.Enabled = true;
            //txtSyncby.Enabled = true;
            //drpSynID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drprecNo.Enabled = false;
            txtReceipe_English.Enabled = false;
            txtReceipe_Arabic.Enabled = false;
            txtExpireDays.Enabled = false;
            txtProductionDays.Enabled = false;
            TypeOfReceipe.Enabled = false;
            //txtUploadDate.Enabled = false;
            //txtUploadby.Enabled = false;
            //txtSyncDate.Enabled = false;
            //txtSyncby.Enabled = false;
            //drpSynID.Enabled = false;


        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Receipe.Where(p => p.TenentID == TID).Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Receipe.Where(p => p.TenentID == TID).OrderByDescending(p => p.recNo).Take(take).Skip(Skip)).ToList());
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

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tbl_Receipe.Where(p => p.TenentID == TID).Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Receipe.Where(p => p.TenentID == TID).OrderByDescending(p => p.recNo).Take(take).Skip(Skip)).ToList());
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

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.tbl_Receipe.Where(p => p.TenentID == TID).Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Receipe.Where(p => p.TenentID == TID).OrderByDescending(p => p.recNo).Take(take).Skip(Skip)).ToList());
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

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Receipe.Where(p => p.TenentID == TID).Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Receipe.Where(p => p.TenentID == TID).OrderByDescending(p => p.recNo).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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
            FirstData();
        }


        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {
                        string[] ID = e.CommandArgument.ToString().Split('~');
                        int recNo = Convert.ToInt32(ID[0]); //Convert.ToInt32(e.CommandArgument);
                        string PREFIX = ID[1].ToString();
                        if (DB.Receipe_Menegement.Where(p => p.TenentID == TID && p.recNo == recNo && p.Prefix == PREFIX).Count() < 1)
                        {
                            Database.tbl_Receipe objReceipe = DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == recNo && p.Prefix == PREFIX);
                            DB.tbl_Receipe.DeleteObject(objReceipe);
                            DB.SaveChanges();

                            BindData();                          
                        }
                        else
                        {
                            pnlErrorMsg.Visible = true;
                            lblerrorMsg.Text = "This Receipe Already Used in Receipe Menegement";
                            return;
                        }

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split('~');
                        int recNo = Convert.ToInt32(ID[0]); //Convert.ToInt32(e.CommandArgument);
                        string PREFIX = ID[1].ToString();
                        if (DB.tbl_Receipe.Where(p => p.TenentID == TID && p.recNo == recNo).Count() > 0)
                        {
                            Database.tbl_Receipe objtbl_Receipe = DB.tbl_Receipe.Single(p => p.TenentID == TID && p.recNo == recNo && p.Prefix == PREFIX);
                            // drprecNo.SelectedValue = objtbl_Receipe.recNo.ToString();
                            txtReceipe_English.Text = objtbl_Receipe.Receipe_English.ToString();
                            txtReceipe_Arabic.Text = objtbl_Receipe.Receipe_Arabic.ToString();
                            txtExpireDays.Text = objtbl_Receipe.ExpireDays.ToString();
                            txtProductionDays.Text = objtbl_Receipe.ProdDays.ToString();
                            TypeOfReceipe.SelectedValue = objtbl_Receipe.RecType.ToString();
                            //txtUploadDate.Text = objtbl_Receipe.UploadDate.ToString();
                            //txtUploadby.Text = objtbl_Receipe.Uploadby.ToString();
                            //txtSyncDate.Text = objtbl_Receipe.SyncDate.ToString();
                            //txtSyncby.Text = objtbl_Receipe.Syncby.ToString();
                            //drpSynID.SelectedValue = objtbl_Receipe.SynID.ToString();

                            btnAdd.Text = "Update";
                            ViewState["Edit"] = recNo;
                            ViewState["Prefix"] = PREFIX;
                            Write();
                        }
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

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.tbl_Receipe.Where(p => p.TenentID == TID).Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.tbl_Receipe.Where(p => p.TenentID == TID).OrderByDescending(p => p.recNo).Take(Tvalue).Skip(Svalue)).ToList());
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

        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        #endregion

    }
}