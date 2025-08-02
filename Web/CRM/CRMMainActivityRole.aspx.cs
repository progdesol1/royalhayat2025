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

namespace Web.CRM
{
    public partial class CRMMainActivityRole : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        ERPEntities DB = new ERPEntities();
        CallEntities DB1 = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Readonly();
                ManageLang();

                // pnlSuccessMsg.Visible = false;
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
            var List = DB1.CRMMainActivityRoles.OrderBy(m => m.TenantID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView2, Totalrec, List);
        }
        #endregion

        public void GetShow()
        {

            lblCOMPID1s.Attributes["class"] = lblMasterCODE1s.Attributes["class"] = lblRoleID1s.Attributes["class"] = lblSystemID1s.Attributes["class"] = lblMenuID1s.Attributes["class"] = lblDatetime1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblCOMPID2h.Attributes["class"] = lblMasterCODE2h.Attributes["class"] = lblRoleID2h.Attributes["class"] = lblSystemID2h.Attributes["class"] = lblMenuID2h.Attributes["class"] = lblDatetime2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCOMPID1s.Attributes["class"] = lblMasterCODE1s.Attributes["class"] = lblRoleID1s.Attributes["class"] = lblSystemID1s.Attributes["class"] = lblMenuID1s.Attributes["class"] = lblDatetime1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblCOMPID2h.Attributes["class"] = lblMasterCODE2h.Attributes["class"] = lblRoleID2h.Attributes["class"] = lblSystemID2h.Attributes["class"] = lblMenuID2h.Attributes["class"] = lblDatetime2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  getshow";
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
            drpCOMPID.SelectedIndex = 0;
            drpMasterCODE.SelectedIndex = 0;
            drpRoleID.SelectedIndex = 0;
            drpSystemID.SelectedIndex = 0;
            drpMenuID.SelectedIndex = 0;
            txtDatetime.Text = "";
            cbActive.Checked = false;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    if (btnAdd.Text == "AddNew")
                    {

                        Write();
                        //Clear();
                        btnAdd.Text = "Add";
                    }
                    else if (btnAdd.Text == "Add")
                    {
                        Database.CRMMainActivityRole objCRMMainActivityRole = new Database.CRMMainActivityRole();
                        //Server Content Send data Yogesh
                        objCRMMainActivityRole.COMPID = Convert.ToInt32(drpCOMPID.SelectedValue);
                        objCRMMainActivityRole.ActivityID = Convert.ToInt32(drpMasterCODE.SelectedValue);
                        objCRMMainActivityRole.RoleID = Convert.ToInt32(drpRoleID.SelectedValue);
                        objCRMMainActivityRole.SystemID = Convert.ToInt32(drpSystemID.SelectedValue);
                        objCRMMainActivityRole.MenuID = Convert.ToInt32(drpMenuID.SelectedValue);
                        objCRMMainActivityRole.Datetime = Convert.ToDateTime(txtDatetime.Text);
                        objCRMMainActivityRole.Active = cbActive.Checked;


                        DB1.CRMMainActivityRoles.AddObject(objCRMMainActivityRole);
                        DB.SaveChanges();
                        Clear();
                        lblMsg.Text = "  Data Save Successfully";
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
                            Database.CRMMainActivityRole objCRMMainActivityRole = DB1.CRMMainActivityRoles.Single(p => p.TenantID == ID);
                            objCRMMainActivityRole.COMPID = Convert.ToInt32(drpCOMPID.SelectedValue);
                            objCRMMainActivityRole.ActivityID = Convert.ToInt32(drpMasterCODE.SelectedValue);
                            objCRMMainActivityRole.RoleID = Convert.ToInt32(drpRoleID.SelectedValue);
                            objCRMMainActivityRole.SystemID = Convert.ToInt32(drpSystemID.SelectedValue);
                            objCRMMainActivityRole.MenuID = Convert.ToInt32(drpMenuID.SelectedValue);
                            objCRMMainActivityRole.Datetime = Convert.ToDateTime(txtDatetime.Text);
                            objCRMMainActivityRole.Active = cbActive.Checked;

                            ViewState["Edit"] = null;
                            btnAdd.Text = "Add New";
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

                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRMMainActivityRole.aspx");
        }
        public void FillContractorID()
        {
            int TID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).TENANT_ID);
            drpCOMPID.DataSource = DB1.CRMMainActivityRoles;
            drpCOMPID.DataTextField = "";
            drpCOMPID.DataValueField = "";
            drpCOMPID.DataBind();
            drpCOMPID.Items.Insert(0,new ListItem("---Select---", "0"));


            drpMasterCODE.DataSource = DB1.CRMMainActivityRoles;
            drpMasterCODE.DataTextField = "";
            drpMasterCODE.DataValueField = "";
            drpMasterCODE.DataBind();
            drpMasterCODE.Items.Insert(0, new ListItem("---Select---", "0"));


            drpRoleID.DataSource = DB1.CRMMainActivityRoles;
            drpRoleID.DataTextField = "";
            drpRoleID.DataValueField = "";
            drpRoleID.DataBind();
            drpRoleID.Items.Insert(0, new ListItem("---Select---", "0"));


            drpSystemID.DataSource = DB1.CRMMainActivityRoles;
            drpSystemID.DataTextField = "";
            drpSystemID.DataValueField = "";
            drpSystemID.DataBind();
            drpSystemID.Items.Insert(0, new ListItem("---Select---", "0"));


            drpMenuID.DataSource = DB1.CRMMainActivityRoles;
            drpMenuID.DataTextField = "";
            drpMenuID.DataValueField = "";
            drpMenuID.DataBind();
            drpMenuID.Items.Insert(0, new ListItem("---Select---", "0"));
            //drpActive.Items.Insert(0, new ListItem("-- Select --", "0"));drpActive.DataSource = DB.0;drpActive.DataTextField = "0";drpActive.DataValueField = "0";drpActive.DataBind();
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
            drpCOMPID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            drpMasterCODE.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpRoleID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            drpSystemID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            drpMenuID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
            txtDatetime.Text = Listview1.SelectedDataKey[6].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[6].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                drpCOMPID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                drpMasterCODE.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpRoleID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                drpSystemID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                drpMenuID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                txtDatetime.Text = Listview1.SelectedDataKey[6].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();

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
                drpCOMPID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
                drpMasterCODE.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpRoleID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
                drpSystemID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
                drpMenuID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
                txtDatetime.Text = Listview1.SelectedDataKey[6].ToString();
                //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            drpCOMPID.SelectedValue = Listview1.SelectedDataKey[1].ToString();
            drpMasterCODE.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpRoleID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            drpSystemID.SelectedValue = Listview1.SelectedDataKey[4].ToString();
            drpMenuID.SelectedValue = Listview1.SelectedDataKey[5].ToString();
            txtDatetime.Text = Listview1.SelectedDataKey[6].ToString();
            //cbActive.Checked = Listview1.SelectedDataKey[0].ToString();

        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblCOMPID2h.Visible = lblMasterCODE2h.Visible = lblRoleID2h.Visible = lblSystemID2h.Visible = lblMenuID2h.Visible = lblDatetime2h.Visible = lblActive2h.Visible = false;
                    //2true
                    txtCOMPID2h.Visible = txtMasterCODE2h.Visible = txtRoleID2h.Visible = txtSystemID2h.Visible = txtMenuID2h.Visible = txtDatetime2h.Visible = txtActive2h.Visible = true;

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
                    lblCOMPID2h.Visible = lblMasterCODE2h.Visible = lblRoleID2h.Visible = lblSystemID2h.Visible = lblMenuID2h.Visible = lblDatetime2h.Visible = lblActive2h.Visible = true;
                    //2false
                    txtCOMPID2h.Visible = txtMasterCODE2h.Visible = txtRoleID2h.Visible = txtSystemID2h.Visible = txtMenuID2h.Visible = txtDatetime2h.Visible = txtActive2h.Visible = false;

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
                    lblCOMPID1s.Visible = lblMasterCODE1s.Visible = lblRoleID1s.Visible = lblSystemID1s.Visible = lblMenuID1s.Visible = lblDatetime1s.Visible = lblActive1s.Visible = false;
                    //1true
                    txtCOMPID1s.Visible = txtMasterCODE1s.Visible = txtRoleID1s.Visible = txtSystemID1s.Visible = txtMenuID1s.Visible = txtDatetime1s.Visible = txtActive1s.Visible = true;
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
                    lblCOMPID1s.Visible = lblMasterCODE1s.Visible = lblRoleID1s.Visible = lblSystemID1s.Visible = lblMenuID1s.Visible = lblDatetime1s.Visible = lblActive1s.Visible = true;
                    //1false
                    txtCOMPID1s.Visible = txtMasterCODE1s.Visible = txtRoleID1s.Visible = txtSystemID1s.Visible = txtMenuID1s.Visible = txtDatetime1s.Visible = txtActive1s.Visible = false;
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((CRMMaster)this.Master).getOwnPage();

            List<Database.CRM_TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("CRMMainActivityRole").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.CRM_TBLLabelDTL item in List)
            {
                if (lblCOMPID1s.ID == item.LabelID)
                    txtCOMPID1s.Text = lblCOMPID1s.Text = lblhCOMPID.Text = item.LabelName;
                else if (lblMasterCODE1s.ID == item.LabelID)
                    txtMasterCODE1s.Text = lblMasterCODE1s.Text = lblhMasterCODE.Text = item.LabelName;
                else if (lblRoleID1s.ID == item.LabelID)
                    txtRoleID1s.Text = lblRoleID1s.Text = lblhRoleID.Text = item.LabelName;
                else if (lblSystemID1s.ID == item.LabelID)
                    txtSystemID1s.Text = lblSystemID1s.Text = lblhSystemID.Text = item.LabelName;
                else if (lblMenuID1s.ID == item.LabelID)
                    txtMenuID1s.Text = lblMenuID1s.Text = lblhMenuID.Text = item.LabelName;
                else if (lblDatetime1s.ID == item.LabelID)
                    txtDatetime1s.Text = lblDatetime1s.Text = lblhDatetime.Text = item.LabelName;
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = lblhActive.Text = item.LabelName;
                
                else if (lblCOMPID2h.ID == item.LabelID)
                    txtCOMPID2h.Text = lblCOMPID2h.Text = lblhCOMPID.Text = item.LabelName;
                else if (lblMasterCODE2h.ID == item.LabelID)
                    txtMasterCODE2h.Text = lblMasterCODE2h.Text = lblhMasterCODE.Text = item.LabelName;
                else if (lblRoleID2h.ID == item.LabelID)
                    txtRoleID2h.Text = lblRoleID2h.Text = lblhRoleID.Text = item.LabelName;
                else if (lblSystemID2h.ID == item.LabelID)
                    txtSystemID2h.Text = lblSystemID2h.Text = lblhSystemID.Text = item.LabelName;
                else if (lblMenuID2h.ID == item.LabelID)
                    txtMenuID2h.Text = lblMenuID2h.Text = lblhMenuID.Text = item.LabelName;
                else if (lblDatetime2h.ID == item.LabelID)
                    txtDatetime2h.Text = lblDatetime2h.Text = lblhDatetime.Text = item.LabelName;
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = lblhActive.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.CRM_TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("CRMMainActivityRole").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\CRMMainActivityRole.xml"));
            foreach (Database.CRM_TBLLabelDTL item in List)
            {

                var obj = ((CRMMaster)this.Master).Bindxml(".xml").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblCOMPID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOMPID1s.Text;
                else if (lblMasterCODE1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMasterCODE1s.Text;
                else if (lblRoleID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRoleID1s.Text;
                else if (lblSystemID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSystemID1s.Text;
                else if (lblMenuID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMenuID1s.Text;
                else if (lblDatetime1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDatetime1s.Text;
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;

               else if (lblCOMPID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOMPID2h.Text;
                else if (lblMasterCODE2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMasterCODE2h.Text;
                else if (lblRoleID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRoleID2h.Text;
                else if (lblSystemID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtSystemID2h.Text;
                else if (lblMenuID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMenuID2h.Text;
                else if (lblDatetime2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDatetime2h.Text;
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\CRMMainActivityRole.xml"));

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
            drpCOMPID.Enabled = true;
            drpMasterCODE.Enabled = true;
            drpRoleID.Enabled = true;
            drpSystemID.Enabled = true;
            drpMenuID.Enabled = true;
            txtDatetime.Enabled = true;
            cbActive.Enabled = true;

        }
        public void Readonly()
        {
            //navigation.Visible = true;
            drpCOMPID.Enabled = false;
            drpMasterCODE.Enabled = false;
            drpRoleID.Enabled = false;
            drpSystemID.Enabled = false;
            drpMenuID.Enabled = false;
            txtDatetime.Enabled = false;
            cbActive.Enabled = false;


        }

        #region Listview
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = DB1.CRMMainActivityRoles.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((CRMMaster)Page.Master).BindList(Listview1, (DB1.CRMMainActivityRoles.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
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

            ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2,Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB1.CRMMainActivityRoles.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((CRMMaster)Page.Master).BindList(Listview1, (DB1.CRMMainActivityRoles.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
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
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }
        protected void btnfirst_Click(object sender, EventArgs e)
        {

            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = DB1.CRMMainActivityRoles.Count();
                take = Showdata;
                Skip = 0;
                ((CRMMaster)Page.Master).BindList(Listview1, (DB1.CRMMainActivityRoles.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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
            int Totalrec = DB1.CRMMainActivityRoles.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((CRMMaster)Page.Master).BindList(Listview1, (DB1.CRMMainActivityRoles.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
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

                        int ID = Convert.ToInt32(e.CommandArgument);


                        Database.CRMMainActivityRole objSOJobDesc = DB1.CRMMainActivityRoles.Single(p => p.TenantID == ID);
                        objSOJobDesc.Active = false;
                        DB.SaveChanges();
                        BindData();
                        int Tvalue = Convert.ToInt32(ViewState["Take"]);
                        int Svalue = Convert.ToInt32(ViewState["Skip"]);
                        ((CRMMaster)Page.Master).BindList(Listview1, (DB1.CRMMainActivityRoles.OrderBy(m => m.COMPID).Take(Tvalue).Skip(Svalue)).ToList());

                    }

                    if (e.CommandName == "btnEdit")
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);


                        Database.CRMMainActivityRole objCRMMainActivityRole = DB1.CRMMainActivityRoles.Single(p => p.TenantID == ID);
                        drpCOMPID.SelectedValue = objCRMMainActivityRole.COMPID.ToString();
                        drpMasterCODE.SelectedValue = objCRMMainActivityRole.ActivityID.ToString();
                        drpRoleID.SelectedValue = objCRMMainActivityRole.RoleID.ToString();
                        drpSystemID.SelectedValue = objCRMMainActivityRole.SystemID.ToString();
                        drpMenuID.SelectedValue = objCRMMainActivityRole.MenuID.ToString();
                        txtDatetime.Text = objCRMMainActivityRole.Datetime.ToString();
                        cbActive.Checked = (objCRMMainActivityRole.Active == true) ? true : false;

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
            int Totalrec = DB1.CRMMainActivityRoles.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((CRMMaster)Page.Master).BindList(Listview1, (DB1.CRMMainActivityRoles.OrderBy(m => m.COMPID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
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