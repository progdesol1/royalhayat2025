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
    public partial class ISCampignStatus : System.Web.UI.Page
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
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Session["LANGUAGE"] = "en-US";
                //Readonly();
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();

            }
        }
        #region Step2
        public void BindData()
        {
            //List<Database.ISCampignStatu> List = DB.ISCampignStatus.Where(p=>p.Active == true).OrderBy(m => m.ID).ToList();
            Listview1.DataSource = DB.ISCampignStatus.Where(p => p.Active == true).OrderBy(m => m.ID).OrderByDescending(p=>p.ID);
            Listview1.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
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
        public void GetShow()
        {

            lblStatus1s.Attributes["class"] = lblCreatedDateTime1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblStatus2h.Attributes["class"] = lblCreatedDateTime2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblStatus1s.Attributes["class"] = lblCreatedDateTime1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblStatus2h.Attributes["class"] = lblCreatedDateTime2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            txtStatus.Text = "";
            txtCreatedDateTime.Text = "";
            //cbActive.Checked = false;
            //cbDeleted.Checked = false;
            //drpCRUP_ID.SelectedIndex = 0;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Add";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.ISCampignStatu objISCampignStatus = new Database.ISCampignStatu();
                        //Server Content Send data Yogesh
                        objISCampignStatus.Status = txtStatus.Text;
                        objISCampignStatus.CreatedDateTime = Convert.ToDateTime(txtCreatedDateTime.Text);
                        objISCampignStatus.Active = true;
                        objISCampignStatus.Deleted = true;
                        objISCampignStatus.CRUP_ID = 1;
                        //objISCampignStatus.Active = cbActive.Checked;
                        //objISCampignStatus.Deleted = cbDeleted.Checked;
                        //objISCampignStatus.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);


                        DB.ISCampignStatus.AddObject(objISCampignStatus);
                        DB.SaveChanges();
                        btnAdd.Text = "AddNew";
                        Clear();
                        lblMsg.Text = "Data Save Successfully";
                        pnlSuccessMsg.Visible = true;
                        BindData();
                        //navigation.Visible = true;
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["Edit"] != null)
                        {
                            int ID = Convert.ToInt32(ViewState["Edit"]);
                            Database.ISCampignStatu objISCampignStatus = DB.ISCampignStatus.Single(p => p.ID == ID);
                            objISCampignStatus.Status = txtStatus.Text;
                            objISCampignStatus.CreatedDateTime = Convert.ToDateTime(txtCreatedDateTime.Text);
                            objISCampignStatus.Active = true;
                            objISCampignStatus.Deleted = true;
                            objISCampignStatus.CRUP_ID = 1;
                            //objISCampignStatus.Active = cbActive.Checked;
                            //objISCampignStatus.Deleted = cbDeleted.Checked;
                            //objISCampignStatus.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);

                            ViewState["Edit"] = null;
                            btnAdd.Text = "AddNew";
                            DB.SaveChanges();
                            Clear();
                            lblMsg.Text = "  Data Edit Successfully";
                            pnlSuccessMsg.Visible = true;
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpCRUP_ID.Items.Insert(0, new ListItem("-- Select --", "0"));drpCRUP_ID.DataSource = DB.0;drpCRUP_ID.DataTextField = "0";drpCRUP_ID.DataValueField = "0";drpCRUP_ID.DataBind();
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
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;
            txtStatus.Text = Listview1.SelectedDataKey[0].ToString();
            txtCreatedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtStatus.Text = Listview1.SelectedDataKey[0].ToString();
                txtCreatedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

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
                txtStatus.Text = Listview1.SelectedDataKey[0].ToString();
                txtCreatedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
                //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
                //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtStatus.Text = Listview1.SelectedDataKey[0].ToString();
            txtCreatedDateTime.Text = Listview1.SelectedDataKey[0].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();
            //cbDeleted.Checked = Listview1.SelectedDataKey[0].ToString();
            //txtCRUP_ID.Text = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblStatus2h.Visible = lblCreatedDateTime2h.Visible = false;
                    //2true
                    txtStatus2h.Visible = txtCreatedDateTime2h.Visible = true;

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
                    lblStatus2h.Visible = lblCreatedDateTime2h.Visible = true;
                    //2false
                    txtStatus2h.Visible = txtCreatedDateTime2h.Visible = false;

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
                    lblStatus1s.Visible = lblCreatedDateTime1s.Visible = false;
                    //1true
                    txtStatus1s.Visible = txtCreatedDateTime1s.Visible = true;
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
                    lblStatus1s.Visible = lblCreatedDateTime1s.Visible = true;
                    //1false
                    txtStatus1s.Visible = txtCreatedDateTime1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ISCampignStatus").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblStatus1s.ID == item.LabelID)
                    txtStatus1s.Text = lblStatus1s.Text = lblhStatus.Text = item.LabelName;
                else if (lblCreatedDateTime1s.ID == item.LabelID)
                    txtCreatedDateTime1s.Text = lblCreatedDateTime1s.Text = lblhCreatedDateTime.Text = item.LabelName;
                //else if (lblActive1s.ID == item.LabelID)
                //    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    txtDeleted1s.Text = lblDeleted1s.Text = item.LabelName;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;

                else if (lblStatus2h.ID == item.LabelID)
                    txtStatus2h.Text = lblStatus2h.Text = lblhStatus.Text = item.LabelName;
                else if (lblCreatedDateTime2h.ID == item.LabelID)
                    txtCreatedDateTime2h.Text = lblCreatedDateTime2h.Text = lblhCreatedDateTime.Text = item.LabelName;
                //else if (lblActive2h.ID == item.LabelID)
                //    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    txtDeleted2h.Text = lblDeleted2h.Text = item.LabelName;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("ISCampignStatus").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\ISCampignStatus.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("ISCampignStatus").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblStatus1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus1s.Text;
                else if (lblCreatedDateTime1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCreatedDateTime1s.Text;
                //else if (lblActive1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;
                //else if (lblDeleted1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted1s.Text;
                //else if (lblCRUP_ID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID1s.Text;

                else if (lblStatus2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtStatus2h.Text;
                else if (lblCreatedDateTime2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCreatedDateTime2h.Text;
                //else if (lblActive2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;
                //else if (lblDeleted2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtDeleted2h.Text;
                //else if (lblCRUP_ID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCRUP_ID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\ISCampignStatus.xml"));

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
            txtStatus.Enabled = true;
            txtCreatedDateTime.Enabled = true;
            //cbActive.Enabled = true;
            //cbDeleted.Enabled = true;
            //drpCRUP_ID.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            txtStatus.Enabled = false;
            txtCreatedDateTime.Enabled = false;
            //cbActive.Enabled = false;
            //cbDeleted.Enabled = false;
            //drpCRUP_ID.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.ISCampignStatus.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((AcmMaster)Page.Master).BindList(Listview1, (DB.ISCampignStatus.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            if (take == Totalrec && Skip == (Totalrec - Showdata))
                btnNext1.Enabled = false;
            else
                btnNext1.Enabled = true;
            if (take == Showdata && Skip == 0)
                btnPrevious1.Enabled = false;
            else
                btnPrevious1.Enabled = true;

            ChoiceID = take / Showdata;

            ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.ISCampignStatus.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.ISCampignStatus.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                if (take == Showdata && Skip == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;

                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;

                ChoiceID = take / Showdata;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB.ISCampignStatus.Count();
                take = Showdata;
                Skip = 0;
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.ISCampignStatus.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.ISCampignStatus.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((AcmMaster)Page.Master).BindList(Listview1, (DB.ISCampignStatus.OrderBy(m => m.ID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }
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
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (e.CommandName == "btnDelete")
                    {

                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int IID = Convert.ToInt32(e.CommandArgument);

                        Database.ISCampignStatu objSOJobDesc = DB.ISCampignStatus.Single(p => p.ID == IID);
                        objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((AcmMaster)Page.Master).BindList(Listview1, (DB.ISCampignStatus.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        //string[] ID = e.CommandArgument.ToString().Split(',');
                        //string str1 = ID[0].ToString();
                        //string str2 = ID[1].ToString();
                        int IID = Convert.ToInt32(e.CommandArgument);

                        Database.ISCampignStatu objISCampignStatus = DB.ISCampignStatus.Single(p => p.ID == IID);
                        txtStatus.Text = objISCampignStatus.Status.ToString();
                        txtCreatedDateTime.Text = objISCampignStatus.CreatedDateTime.ToString();
                        //cbActive.Checked = (objISCampignStatus.Active == true) ? true : false;
                        //cbDeleted.Checked = (objISCampignStatus.Deleted == true) ? true : false;
                        //drpCRUP_ID.SelectedValue = objISCampignStatus.CRUP_ID.ToString();

                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
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

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB.ISCampignStatus.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((AcmMaster)Page.Master).BindList(Listview1, (DB.ISCampignStatus.OrderBy(m => m.ID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                if (Tvalue == Showdata && Svalue == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
            }
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        }

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