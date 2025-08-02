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

namespace Web.CRM
{
    public partial class ICEXTRACOST : System.Web.UI.Page
    {
        int CreatedBy = 0;
        int UpdatedBy = 0;
        int TenentID = 0;
        Database.CallEntities DB = new Database.CallEntities();        
        string[] lfillform;
        string popupScript = "";
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Session["Pagename"] = "AdminForm";
                Session["TenentID"] = TID;                
                Session["UserId"] = UID;
                fillGrid();
                
                Session["LANGUAGE"] = "en-US";
                ManageLang();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
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
        //For Label
        #region PAge Genarator
     

        public void GetShow()
        {

            lblOHNAME11s.Attributes["class"] = lblOHNAME21s.Attributes["class"] = lblOHNAME31s.Attributes["class"] = "control-label col-md-4  getshow";
            lblOHNAME12h.Attributes["class"] = lblOHNAME22h.Attributes["class"] = lblOHNAME32h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblOHNAME11s.Attributes["class"] = lblOHNAME21s.Attributes["class"] = lblOHNAME31s.Attributes["class"] = "control-label col-md-4  gethide";
            lblOHNAME12h.Attributes["class"] = lblOHNAME22h.Attributes["class"] = lblOHNAME32h.Attributes["class"] = "control-label col-md-4  getshow";
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

        #region PAge Genarator navigation
        protected void btnFirst_Click1(object sender, EventArgs e)
        {
            FirstData();
        }
        protected void btnNext_Click1(object sender, EventArgs e)
        {
            NextData();
        }
        protected void btnPrev_Click1(object sender, EventArgs e)
        {
            PrevData();
        }
        protected void btnLast_Click1(object sender, EventArgs e)
        {
            LastData();
        }
        public void FirstData()
        {
            int index = Convert.ToInt32(ViewState["Index"]);
            grdmstr.SelectedIndex = 0;
            if (grdmstr.SelectedDataKey[2] != null)
            txtOHNAME1.Text = grdmstr.SelectedDataKey[2].ToString();
            if (grdmstr.SelectedDataKey[3] != null)
            txtOHNAME2.Text = grdmstr.SelectedDataKey[3].ToString();
            if (grdmstr.SelectedDataKey[4] != null)
            txtOHNAME3.Text = grdmstr.SelectedDataKey[4].ToString();
           
        }
        public void NextData()
        {

            if (grdmstr.SelectedIndex != grdmstr.Items.Count - 1)
            {
                grdmstr.SelectedIndex = grdmstr.SelectedIndex + 1;
                if (grdmstr.SelectedDataKey[2] != null)
                txtOHNAME1.Text = grdmstr.SelectedDataKey[2].ToString();
                if (grdmstr.SelectedDataKey[3] != null)
                txtOHNAME2.Text = grdmstr.SelectedDataKey[3].ToString();
                if (grdmstr.SelectedDataKey[4] != null)
                txtOHNAME3.Text = grdmstr.SelectedDataKey[4].ToString();
               
            }

        }
        public void PrevData()
        {
            if (grdmstr.SelectedIndex == 0)
            {
                lblMsg.Text = "This is first record";
                pnlSuccessMsg.Visible = true;

            }
            else
            {
                pnlSuccessMsg.Visible = false;
                grdmstr.SelectedIndex = grdmstr.SelectedIndex - 1;
                if(grdmstr.SelectedDataKey[2] != null)
                txtOHNAME1.Text = grdmstr.SelectedDataKey[2].ToString();
                if (grdmstr.SelectedDataKey[3] != null)
                txtOHNAME2.Text = grdmstr.SelectedDataKey[3].ToString();
                if (grdmstr.SelectedDataKey[4] != null)
                txtOHNAME3.Text = grdmstr.SelectedDataKey[4].ToString();
               
            }
        }
        public void LastData()
        {
            grdmstr.SelectedIndex = grdmstr.Items.Count - 1;
            if (grdmstr.SelectedDataKey[2] != null)
            txtOHNAME1.Text = grdmstr.SelectedDataKey[2].ToString();
            if (grdmstr.SelectedDataKey[3] != null)
            txtOHNAME2.Text = grdmstr.SelectedDataKey[3].ToString();
            if (grdmstr.SelectedDataKey[4] != null)
            txtOHNAME3.Text = grdmstr.SelectedDataKey[4].ToString();
            
        }
         #endregion

        #region PAge Genarator language
        protected void btnEditLable_Click1(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblOHNAME12h.Visible = lblOHNAME22h.Visible = lblOHNAME32h.Visible = false;
                    //2true
                    txtOHNAME12h.Visible = txtOHNAME22h.Visible = txtOHNAME32h.Visible = true;

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
                    lblOHNAME12h.Visible = lblOHNAME22h.Visible = lblOHNAME32h.Visible = true;
                    //2false
                    txtOHNAME12h.Visible = txtOHNAME22h.Visible = txtOHNAME32h.Visible = false;

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
                    lblOHNAME11s.Visible = lblOHNAME21s.Visible = lblOHNAME31s.Visible = false;
                    //1true
                    txtOHNAME11s.Visible = txtOHNAME21s.Visible = txtOHNAME31s.Visible = true;
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
                    lblOHNAME11s.Visible = lblOHNAME21s.Visible = lblOHNAME31s.Visible = true;
                    //1false
                    txtOHNAME11s.Visible = txtOHNAME21s.Visible = txtOHNAME31s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("ICEXTRACOST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                
                if (lblOHNAME11s.ID == item.LabelID)
                    txtOHNAME11s.Text = lblOHNAME11s.Text = item.LabelName;
                else if (lblOHNAME21s.ID == item.LabelID)
                    txtOHNAME21s.Text = lblOHNAME21s.Text = item.LabelName;
                else if (lblOHNAME31s.ID == item.LabelID)
                    txtOHNAME31s.Text = lblOHNAME31s.Text = item.LabelName;                           

                else
                    txtHeader.Text = lblHeader.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((CRMMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((CRMMaster)this.Master).Bindxml("ICEXTRACOST").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\CRM\\xml\\ICEXTRACOST.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((CRMMaster)this.Master).Bindxml("ICEXTRACOST").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

               
                if (lblOHNAME11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOHNAME11s.Text;
                else if (lblOHNAME21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOHNAME21s.Text;
                else if (lblOHNAME31s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtOHNAME31s.Text;               

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\CRM\\xml\\ICEXTRACOST.xml"));

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
        protected void LanguageEnglish_Click1(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }
        protected void LanguageArabic_Click1(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }
        protected void LanguageFrance_Click1(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
        }
        #endregion
        public void Write()
        {
            //navigation.Visible = false;
           
            txtOHNAME1.Enabled = true;
            txtOHNAME2.Enabled = true;
            txtOHNAME3.Enabled = true;
           
        }
        public void Readonly()
        {
            //  navigation.Visible = true;
            
            txtOHNAME1.Enabled = false;
            txtOHNAME2.Enabled = false;
            txtOHNAME3.Enabled = false;
           
        }
        //For Label
        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {
            
            try
            { 
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int OID = Convert.ToInt32(lfillform[1]);
                Database.ICEXTRACOST obj_ICEXTRACOST = DB.ICEXTRACOSTs.SingleOrDefault(p => p.OVERHEADID == OID && p.TenentID == TID);
                obj_ICEXTRACOST.Active = "D";
                DB.SaveChanges();
                //  objCommonSelDel.CommonDeleteWithTenant("prc_acm_common_update", lfillform[0].ToString(), "ICEXTRACOST", "ACTIVE", "D", "OVERHEADID", Convert.ToInt32(lfillform[1].ToString()), "Active");
                //   WebMsgBox.Show("Record deleted Successfully !!");
                fillGrid();
                clear();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int OID = Convert.ToInt32(lfillform[1]);
                Database.ICEXTRACOST obj_ICEXTRACOST = DB.ICEXTRACOSTs.SingleOrDefault(p => p.OVERHEADID == OID && p.TenentID == TID);

                // System.Data.DataTable dt2 = objCommonSelDel.CommonFillConditionGridPQ("[dbo].[prc_acm_common_sel_con]", "c", "TenentID,OVERHEADID,OHNAME1,OHNAME2,OHNAME3,ACCOUNTID,Active,CRUP_ID", "ICEXTRACOST", "OVERHEADID", lfillform[1].ToString(), "");
                txtOHNAME1.Text = obj_ICEXTRACOST.OHNAME1.ToString();//dt2.Rows[0]["OHNAME1"].ToString();
                txtOHNAME2.Text = obj_ICEXTRACOST.OHNAME2.ToString();
                txtOHNAME3.Text = obj_ICEXTRACOST.OHNAME3.ToString();//dt2.Rows[0]["OHNAME3"].ToString();
                hidId.Value = lfillform[1].ToString();
                btnSubmit.Text = "Update";
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
                int ID = Convert.ToInt32(lfillform[0]);
                string flag = lfillform[1];
                if (flag == "Y")
                {
                    flag = "N";
                }
                else
                {
                    flag = "Y";
                }
                int OID = Convert.ToInt32(lfillform[1]);
                Database.ICEXTRACOST obj_ICEXTRACOST = DB.ICEXTRACOSTs.SingleOrDefault(p => p.OVERHEADID == OID && p.TenentID == TID);
                obj_ICEXTRACOST.Active = flag;
                DB.SaveChanges();
                //objCommonSelDel.CommonDeleteWithTenant("prc_acm_common_update", lfillform[0].ToString(), "ICEXTRACOST", "ACTIVE", flag, "OVERHEADID", Convert.ToInt32(lfillform[2].ToString()), "Active");
                // WebMsgBox.Show("Status changed Successfully !!");
                fillGrid();
                clear();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
                //   WebMsgBox.Show(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int i = (string.IsNullOrEmpty(hidId.Value) ? -1 : Convert.ToInt32(hidId.Value));
                Database.ICEXTRACOST obj_ICEXTRA = new Database.ICEXTRACOST();
                obj_ICEXTRA.OHNAME1 = txtOHNAME1.Text;
                obj_ICEXTRA.OHNAME2 = txtOHNAME2.Text;
                obj_ICEXTRA.OHNAME3 = txtOHNAME3.Text;
                if (i == -1)
                {
                    int intIdt = DB.ICEXTRACOSTs.Max(u => (int)u.OVERHEADID) == null ? 1 : DB.ICEXTRACOSTs.Max(u => (int)u.OVERHEADID) + 1;
                    obj_ICEXTRA.OVERHEADID = intIdt;
                    obj_ICEXTRA.Active = "Y";
                }
                else
                {
                    obj_ICEXTRA.OVERHEADID = i;
                }
                TenentID = Convert.ToInt32(Session["TenentID"].ToString());
                CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                DB.ICEXTRACOSTs.AddObject(obj_ICEXTRA);
                DB.SaveChanges();
                int j = 1;
                if (j == 1)
                {
                    popupScript = "<script language='javascript'>" + "showSuccessToast();" + "</script>";
                    clear();
                    hidId.Value = "";
                    fillGrid();
                    btnSubmit.Text = "Submit";
                }
                else if (j == 2)
                {
                    popupScript = "<script language='javascript'>" + "showSuccessToast1();" + "</script>";
                    //WebMsgBox.Show("Role updated successfully.");
                    clear();
                    hidId.Value = "";
                    fillGrid();
                    btnSubmit.Text = "Submit";
                }
                Page.ClientScript.RegisterStartupScript(typeof(Page), "popupScript", popupScript);
            }
            catch (Exception ex)
            {
                // WebMsgBox.Show(ex.Message);
            }

        }

        public void clear()
        {
            try
            {
                txtOHNAME1.Text = txtOHNAME2.Text = txtOHNAME3.Text = "";
            }
            catch (Exception ex)
            {
                // WebMsgBox.Show(ex.Message);
            }
        }

        public void fillGrid()
        {
            //try
            //{
                
                int TID=Convert.ToInt32(Session["TenentID"].ToString());
                List<Database.ICEXTRACOST> List_ExtraCose = DB.ICEXTRACOSTs.Where(p => p.TenentID == TID && p.Active=="Y").ToList();
                //System.Data.DataTable dt = objCommonSelDel.CommonMethodOderFnmTnmObTenant("[dbo].[prc_getAllTable_Act_OdrWise]", "TenentID,OVERHEADID,OHNAME1,OHNAME2,OHNAME3,ACCOUNTID,Active,CRUP_ID", "ICEXTRACOST", "OVERHEADID", Convert.ToInt32(Session["TenentID"].ToString()));
                if (List_ExtraCose.Count() > 0)
                {
                    grdmstr.DataSource = List_ExtraCose;
                    grdmstr.DataBind();
                    for (int i = 0; i < grdmstr.Items.Count; i++)
                    {
                        LinkButton ln = (LinkButton)grdmstr.Items[i].FindControl("lnkAction");
                        Label lbl = (Label)grdmstr.Items[i].FindControl("lblCurrentStatus");
                        if (lbl.Text.Trim().Equals("Y") || lbl.Text.Trim().Equals("1"))
                        {
                            ln.Text = "Inactive";
                        }
                        else
                        {
                            ln.Text = "Active";
                        }
                    }
                }
                else
                {
                    grdmstr.DataBind();
                }
            }
            //catch (Exception ex)
            //{
            //    //  WebMsgBox.Show(ex.Message);
            //}

        }
    }
